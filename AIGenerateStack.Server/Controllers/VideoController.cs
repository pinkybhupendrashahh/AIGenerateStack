using Application.UseCases;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIGenerateStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : Controller
    {
        private readonly GenerateAiVideoUseCase _useCase;

        public VideoController(GenerateAiVideoUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate(VideoRequest req)
        {
            var videoUrl = await _useCase.ExecuteAsync(req);
            return Ok(new { videoUrl });
        }
    }
}
