using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

using System.Diagnostics;
namespace Infrastructure.Video
{


   
        public class FfmpegService : IFfmpegService
        {
        private readonly IFileSystem _fs;


        public FfmpegService(IFileSystem fs)
            {
            _fs = fs;
            }

            public async Task<string> ImageToVideoAsync(string imagePath, int durationSec)
            {
                var outputDir = Path.Combine(_fs.WebRootPath, "scenes");
                Directory.CreateDirectory(outputDir);

                var outputPath = Path.Combine(
                    outputDir,
                    $"{Guid.NewGuid()}.mp4");

                var args =
                    $"-y -loop 1 -i \"{imagePath}\" " +
                    $"-t {durationSec} -vf scale=1280:720 " +
                    $"-pix_fmt yuv420p \"{outputPath}\"";

                await RunFfmpegAsync(args);
                return outputPath;
            }

            public async Task<string> MergeAsync(List<string> videoClips, string audioPath)
            {
                var tempFile = Path.Combine(
                    Path.GetTempPath(),
                    $"concat_{Guid.NewGuid()}.txt");

                await File.WriteAllLinesAsync(
                    tempFile,
                    videoClips.Select(v => $"file '{v.Replace("\\", "/")}'"));

                var outputDir = Path.Combine(_fs.WebRootPath, "final");
                Directory.CreateDirectory(outputDir);

                var outputPath = Path.Combine(
                    outputDir,
                    $"final_{Guid.NewGuid()}.mp4");

                var args =
                    $"-y -f concat -safe 0 -i \"{tempFile}\" " +
                    $"-i \"{audioPath}\" -c:v copy -c:a aac " +
                    $"\"{outputPath}\"";

                await RunFfmpegAsync(args);
                return outputPath;
            }

            private static async Task RunFfmpegAsync(string arguments)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg",
                        Arguments = arguments,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                var stderr = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                    throw new Exception($"FFmpeg failed: {stderr}");
            }
        }
    }


