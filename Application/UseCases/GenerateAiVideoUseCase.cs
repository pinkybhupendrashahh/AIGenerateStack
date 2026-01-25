using Application.Interfaces;
using Domain.Models.Enums;
using Infrastructure;
namespace Application.UseCases
{
    public class GenerateAiVideoUseCase
    {
        private readonly IVideoRepository _repository;
        private readonly ITtsService _tts;
        private readonly IVideoGenerator _videoGenerator;
        private readonly IVideoComposer _composer;

        private readonly IAiScriptService _ollamaService;

        public GenerateAiVideoUseCase(
            IVideoRepository repository,
            ITtsService tts,
            IVideoGenerator videoGenerator,
            IVideoComposer composer,
            IAiScriptService ollamaService)
        {
            _repository = repository;
            _tts = tts;
            _videoGenerator = videoGenerator;
            _composer = composer;
            _ollamaService = ollamaService;
        }

        public async Task ExecuteAsync(GenerateVideoCommand command)
        {
            //var job = await _repository.GetAsync(command.JobId)
            //          ?? throw new Exception("Job not found");
            var job = command.Job; // ✅ Job already loaded by worker

            //Console.WriteLine($"[GenerateAiVideoUseCase] Starting job {job.job}");
            Console.WriteLine($"[GenerateAiVideoUseCase] Starting job");
                // {job.Id}");
            try
            {
                await _repository.UpdateStatusAsync(job.Id, VideoStatus.Processing);
                // 1️⃣ Generate script using OLLAMA if not already present
                // 1️⃣ Generate script using Ollama if not already present
               // if (string.IsNullOrWhiteSpace(job.Script))
             //   {
                    Console.WriteLine($"[GenerateAiVideoUseCase] Generating script for job {job.Id}");
                    job.Script = await _ollamaService.GenerateAsync(job.Prompt); // ✅ Generate narration

                    if (string.IsNullOrWhiteSpace(job.Script))
                    {
                        await _repository.FailAsync(job.Id, "Script generation failed");
                        return;
                    }

                    // Optionally save the generated script back to DB
                    //await _repository.UpdateScriptAsync(job.Id, job.Script);
             //   }
                // 1️⃣ Audio
                var audioPath =
                    await _tts.SynthesizeAsync(job.Script, job.Voice.Name);

                await _repository.UpdateProgressAsync(job.Id, 40);

                // 2️⃣ Video clips
                var clips =
                    await _videoGenerator.GenerateAsync(job.Script);

                await _repository.UpdateProgressAsync(job.Id, 70);

                // 3️⃣ Merge
                var finalVideo =
                    await _composer.MergeAsync(clips, audioPath);

                await _repository.CompleteAsync(job.Id, finalVideo);
            }
            catch (Exception ex)
            {
                await _repository.FailAsync(job.Id, ex.Message);
                throw;
            }
        }
    }
}