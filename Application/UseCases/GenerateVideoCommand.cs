using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GenerateVideoCommand
    {
        public VideoJob Job { get; }

        public GenerateVideoCommand(VideoJob job)
        {
            Job = job;
        }
    }


}
