using Application.background;
using Application.Interfaces;
using Application.UseCases;
using Infrastructure;
using System.Threading.Channels;
using Application.UseCases;
using Microsoft.Extensions.Hosting;
namespace AIGenerateStack.Server.Backgroundservices
{





    public class VideoJobWorker : BackgroundService
    {
        private readonly IVideoJobQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
       
        public VideoJobWorker(
            IVideoJobQueue queue,
            IServiceScopeFactory scopeFactory )
        {
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
          
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        var jobId = await _queue.DequeueAsync(stoppingToken);

        //        using var scope = _scopeFactory.CreateScope();
        //        var useCase = scope.ServiceProvider
        //            .GetRequiredService<GenerateAiVideoUseCase>();

        //        var message = await _queue.DequeueAsync(stoppingToken);

        //        await useCase.ExecuteAsync(
        //            new GenerateVideoCommand(message.JobId)
        //        );

        //    }
        //}

               protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Backround process Starts ");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var message = await _queue.DequeueAsync(stoppingToken);

                    using var scope = _scopeFactory.CreateScope();

                    var repo = scope.ServiceProvider
                        .GetRequiredService<IVideoRepository>();

                    var useCase = scope.ServiceProvider
                        .GetRequiredService<GenerateAiVideoUseCase>();

                    var job = await repo.GetByIdAsync(message.JobId);
                  
                    if (job == null)
                    {
                        Console.WriteLine($"Job not found: {message.JobId}");
                        continue;
                    }

                    Console.WriteLine($"Processing job {message.JobId}");

                    await useCase.ExecuteAsync(
                        new GenerateVideoCommand(job)
                    );
                }
                catch (OperationCanceledException)
                {
                    // graceful shutdown
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
  

    }
}

