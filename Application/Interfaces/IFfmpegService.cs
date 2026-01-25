using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFfmpegService
    {
        Task<string> ImageToVideoAsync(string imagePath, int durationSec);
        Task<string> MergeAsync(List<string> videoClips, string audioPath);
    }

}
