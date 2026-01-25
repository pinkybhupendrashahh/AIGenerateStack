
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using System.Diagnostics;
using Application.Interfaces;

namespace Infrastructure.Video
{


    public class VideoComposer : IVideoComposer
    {
        private readonly IFileSystem _fs;
        private readonly IFfmpegService _ffmpeg;
        public VideoComposer(IFileSystem fs, IFfmpegService ffmpeg)
        {
            _fs = fs;
            _ffmpeg = ffmpeg;
        }
        public async Task<string> MergeAsync(List<string> clips, string audioPath)
        {
            var videoDir = Path.Combine(_fs.WebRootPath, "videos");
            Directory.CreateDirectory(videoDir);

            var finalVideoPath = Path.Combine(videoDir, $"{Guid.NewGuid()}.mp4");
            var concatFile = Path.Combine(videoDir, $"{Guid.NewGuid()}.txt");

            await using (var sw = new StreamWriter(concatFile))
            {
                foreach (var clip in clips)
                {
                    sw.WriteLine($"file '{Path.GetFullPath(clip)}'");
                }
            }

            // Merge clips safely with re-encoding
            var mergeArgs = $"-f concat -safe 0 -i \"{concatFile}\" -c:v libx264 -preset fast -crf 23 -pix_fmt yuv420p \"{finalVideoPath}\"";
            await RunFFmpegAsync(mergeArgs);

            // Overlay audio
            var finalWithAudioPath = Path.Combine(videoDir, $"{Guid.NewGuid()}_final.mp4");
            var audioArgs = $"-i \"{finalVideoPath}\" -i \"{Path.GetFullPath(audioPath)}\" -map 0:v:0 -map 1:a:0 -c:v copy -c:a aac -b:a 192k -shortest \"{finalWithAudioPath}\"";
            await RunFFmpegAsync(audioArgs);

            // Cleanup
            File.Delete(finalVideoPath);
            File.Delete(concatFile);

            return finalWithAudioPath;
        }

        private static async Task RunFFmpegAsync(string args)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = args,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            await process!.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                var error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"FFmpeg failed: {error}");
            }
        }

    }
}
    
