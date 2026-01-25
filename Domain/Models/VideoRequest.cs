using Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   
        public sealed class VideoRequest
        {
            /// <summary>
            /// Main topic for AI video generation
            /// </summary>
            public string Topic { get; set; }

            /// <summary>
            /// Narration style (professional, casual, storytelling, etc.)
            /// </summary>
            public string Style { get; set; }

            /// <summary>
            /// Desired video length in seconds
            /// </summary>
            public int LengthSec { get; set; }

            /// <summary>
            /// Voice identifier for TTS
            /// </summary>
            public Voice Voice { get; set; }

            /// <summary>
            /// Optional user-provided script (overrides AI script)
            /// </summary>
            public string Script { get; set; }
        }
    }


