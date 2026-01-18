using Application.Interfaces;
using Infrastructure.Git;
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
        private readonly GitHubUploader _uploader;

        public LocalTtsService(GitHubUploader uploader)
        {
            _uploader = uploader;
        }

        public async Task<string> SynthesizeAsync(string text, string voice)
        {
            var fileName = $"audio_{Guid.NewGuid()}.mp3";
            var localPath = Path.Combine(Path.GetTempPath(), fileName);

            // Call python edge-tts
            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"tts.py \"{text}\" \"{voice}\" \"{localPath}\"",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi)!;
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception(await process.StandardError.ReadToEndAsync());

            // 🔥 Upload to GitHub
            return await _uploader.UploadAsync(localPath);
        }
    }

}
}