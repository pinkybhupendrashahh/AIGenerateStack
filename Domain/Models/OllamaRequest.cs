using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    using System.Text.Json.Serialization;

    namespace AI.VideoMaker.Infrastructure.Ollama.Models
    {
        public sealed class OllamaRequest
        {
            /// <summary>
            /// Model name installed in Ollama (e.g. llama3, mistral)
            /// </summary>
            [JsonPropertyName("model")]
            public string Model { get; init; }

            /// <summary>
            /// Prompt sent to the LLM
            /// </summary>
            [JsonPropertyName("prompt")]
            public string Prompt { get; init; }

            /// <summary>
            /// Disable streaming for simple request/response
            /// </summary>
            [JsonPropertyName("stream")]
            public bool Stream { get; init; } = false;
        }
    }

}
