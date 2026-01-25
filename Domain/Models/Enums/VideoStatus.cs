using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
   
        public enum VideoStatus
        {
        Pending = 0,          // Job created, not yet picked by worker
        Processing = 1,       // Worker started processing
        Created=5,
        ScriptGenerated = 2,
        AudioGenerated = 3,
        VideoGenerated = 4,
        Composed = 90,
        Queued=6,
        Completed = 6,
        Failed = 99
    }
    }



