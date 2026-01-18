using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Video
{
    public class AiVideoGenerator : IVideoGenerator
    {
        public async Task<List<string>> GenerateAsync(string script)
        {
            // Split script into scenes
            // Generate image/video per scene
            return new List<string> { "scene1.mp4", "scene2.mp4" };
        }
    }

}
