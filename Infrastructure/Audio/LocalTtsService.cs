using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Audio
{
 

    public class LocalTtsService : ITtsService
    {
        public async Task<string> SynthesizeAsync(string text, string voice)
        {
            var fileName = $"audio_{Guid.NewGuid()}.mp3";
            var outputPath = Path.Combine("wwwroot", "audio", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"tts.py \"{text}\" \"{voice}\" \"{outputPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi)!;
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                var error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"TTS failed: {error}");
            }

            return $"/audio/{fileName}";
        }
    }
}