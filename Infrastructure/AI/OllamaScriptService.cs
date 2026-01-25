
using Application.Interfaces;
using Domain.Models;
using Domain.Models.AI.VideoMaker.Infrastructure.Ollama.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace Infrastructure.AI
{
   
 

    public class OllamaService : IAiScriptService
    {
        private readonly HttpClient _http;
        private readonly OllamaOptions _options;

        public OllamaService(HttpClient http, IOptions<OllamaOptions> options)
        {
            _http = http;
            _options = options.Value;
        }

        public async Task<string> GenerateAsync(string topic)
        {
            var prompt =
                $"Write a  narration about {topic}. " +
                $"Keep it suitable for a short AI video.";

            var request = new OllamaRequest
            {
                Model = _options.Model,   // ✅ now exists
                Prompt = prompt,
                Stream = false
            };
            var response = await _http.PostAsJsonAsync("/api/generate", request);
            response.EnsureSuccessStatusCode();

            var ollama = await response.Content.ReadFromJsonAsync<OllamaResponse>();

            if (ollama == null || string.IsNullOrWhiteSpace(ollama.Response))
                throw new ApplicationException("Ollama returned empty response");

            return ollama.Response.Trim();
        }
    }

}


