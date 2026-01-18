using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
   
        public enum VideoStatus
        {
            Created = 0,        // Job accepted
            ScriptGenerated = 1,// Ollama completed
            AudioGenerated = 2, // TTS completed
            VideoGenerated = 3, // Visual clips ready
            Composed = 4,       // Audio + video merged
            Completed = 5,      // Final video ready
            Failed = 99         // Any failure
        }
    }



