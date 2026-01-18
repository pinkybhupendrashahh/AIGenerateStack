
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;

namespace Infrastructure.Video
{
    public class VideoComposer : IVideoComposer
    {
        public async Task<string> MergeAsync(
            List<string> clips,
            string audioPath)
        {
            // ffmpeg concat + audio overlay
            return "final_video.mp4";
        }
    }

}
