using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class VideoJob
    {
        public Guid Id { get; set; }
        public string? Topic { get; set; }
        public string? Script { get; set; }
        public string? VideoUrl { get; set; }
        public VideoStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
