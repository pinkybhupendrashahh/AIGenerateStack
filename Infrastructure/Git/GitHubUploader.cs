using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Git
{
    using Microsoft.Extensions.Options;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    public class GitHubUploader
    {
        private readonly HttpClient _http;
        private readonly GitHubOptions _options;

        public GitHubUploader(HttpClient http, IOptions<GitHubOptions> options)
        {
            _http = http;
            _options = options.Value;

            _http.DefaultRequestHeaders.UserAgent.ParseAdd("AIGenerateStack");
            _http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _options.Token);
        }

        public async Task<string> UploadAsync(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var bytes = await File.ReadAllBytesAsync(filePath);
            var contentBase64 = Convert.ToBase64String(bytes);

            var url =
                $"https://api.github.com/repos/{_options.Owner}/{_options.Repo}/contents/{_options.AudioFolder}/{fileName}";

            var payload = new
            {
                message = $"Add audio {fileName}",
                content = contentBase64,
                branch = _options.Branch
            };

            var response = await _http.PutAsync(
                url,
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            return
                $"https://raw.githubusercontent.com/{_options.Owner}/{_options.Repo}/{_options.Branch}/{_options.AudioFolder}/{fileName}";
        }
    }

}
