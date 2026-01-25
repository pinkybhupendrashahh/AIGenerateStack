using Domain.Models.Enums;
using Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models
{
    public class VideoJob
    {
        public Guid Id { get; init; }
        public string Script { get; set; }
        public Voice Voice { get; set; } = Voice.Create("en-GB-LibbyNeural");
        public string Prompt { get; set; }
        public VideoStatus Status { get;  set; }
        public int Progress { get; set; }
        public string? VideoPath { get;  set; }
        public string? Error { get; set; }

        public DateTime CreatedAt { get; set; }
        public void UpdateProgress(int progress) => Progress = progress;

        public void Complete(string path)
        {
            Status = VideoStatus.Completed;
            Progress = 100;
            VideoPath = path;
        }

        public void Fail(string reason)
        {
            Status = VideoStatus.Failed;
        }
    }



}
