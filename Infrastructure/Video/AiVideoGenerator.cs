using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;



namespace Infrastructure.Video
{
   
    public class AiVideoGenerator : IVideoGenerator

    {
        private readonly IFileSystem _fs;

        public AiVideoGenerator(IFileSystem fs)
        {
            _fs = fs;
        }

        public async Task<List<string>> GenerateAsync(string script)
        {
            var scenes = script
                .Split("Scene", StringSplitOptions.RemoveEmptyEntries);

            var clips = new List<string>();

            foreach (var scene in scenes)
            {
                var imagePath = await GenerateImage(scene);
                var videoPath = await ImageToVideo(imagePath);
                clips.Add(videoPath);
            }

            return clips;
        }

        // STEP 1: Generate AI Image (Mock for now)
        private async Task<string> GenerateImage(string prompt)
        {
            var imageDir = Path.Combine(_fs.WebRootPath, "images");
            Directory.CreateDirectory(imageDir);

            var imagePath = Path.Combine(imageDir, $"{Guid.NewGuid()}.png");

            // TEMP: Placeholder image
            File.Copy("Assets/placeholder.png", imagePath, true);

            // 🔥 Later replace with DALL·E / SD API call
            return imagePath;
        }

        // STEP 2: Convert Image → Video using FFmpeg
        private async Task<string> ImageToVideo(string imagePath)
        {
            var sceneDir = Path.Combine(_fs.WebRootPath, "scenes");
            Directory.CreateDirectory(sceneDir);

            var videoPath = Path.Combine(sceneDir, $"{Guid.NewGuid()}.mp4");

            var args =
                $"-y -loop 1 -i \"{imagePath}\" " +
                "-c:v libx264 -t 5 -pix_fmt yuv420p " +
                $"\"{videoPath}\"";

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = args,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            await process!.WaitForExitAsync();

            if (!File.Exists(videoPath))
                throw new Exception("FFmpeg failed to generate scene video");

            return videoPath;
        }
    }


}
