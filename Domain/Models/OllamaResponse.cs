using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    using System.Text.Json.Serialization;

   
        public class OllamaResponse
        {
            [JsonPropertyName("model")]
            public string Model { get; set; }

            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonPropertyName("response")]
            public string Response { get; set; }

            [JsonPropertyName("done")]
            public bool Done { get; set; }
        }
    }


