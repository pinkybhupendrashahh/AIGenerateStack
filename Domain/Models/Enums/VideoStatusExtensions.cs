using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public static class VideoStatusExtensions
    {
        public static int ToProgress(this VideoStatus status) =>
            status switch
            {
                VideoStatus.Created => 5,
                VideoStatus.ScriptGenerated => 20,
                VideoStatus.AudioGenerated => 40,
                VideoStatus.VideoGenerated => 70,
                VideoStatus.Composed => 90,
                VideoStatus.Completed => 100,
                VideoStatus.Failed => 0,
                _ => 0
            };
    }

}
