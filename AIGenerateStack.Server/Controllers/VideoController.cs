using Application.background;
using Application.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/video")]
public class VideoController : ControllerBase
{
    private readonly IVideoRepository _repository;
    private readonly IVideoJobQueue _queue;

    public VideoController(
        IVideoRepository repository,
        IVideoJobQueue queue)
    {
        _repository = repository;
        _queue = queue;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] VideoRequest request)
    {
        var job = new VideoJob
        {
            Id = Guid.NewGuid(),
            Prompt = request.Topic,
            Voice = request.Voice,
            Status = VideoStatus.Created,
            Progress = 0,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(job);
        await _repository.SaveAsync(job);
        // enqueue job
        await _queue.EnqueueAsync(new VideoJobMessage(job.Id));

        return Ok(new { jobId = job.Id });
    }

    [HttpGet("{id}/status")]
    public async Task<IActionResult> Status(Guid id)
    {
        var job = await _repository.GetByIdAsync(id);
        //  var job = await _repository.GetAsync(id);
        if (job == null) return NotFound();

        return Ok(new
        {
            job.Id,
            job.Status,
            job.Progress,
            job.VideoPath,
            job.Error
        });
    }
}
