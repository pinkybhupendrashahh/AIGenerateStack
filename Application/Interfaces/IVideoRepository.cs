using Domain.Models;
using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{


    public interface IVideoRepository
    {
        Task<VideoJob> CreateAsync(VideoJob job);
        Task<List<VideoJob>> SaveAsync(VideoJob job);
        Task<VideoJob?> GetAsync(Guid id);
        Task UpdateStatusAsync(Guid id, VideoStatus status);

        Task UpdateProgressAsync(Guid id, int progress);

        Task CompleteAsync(Guid id, string? videoPath = null);
        
        Task FailAsync(Guid id, string reason);
        Task<VideoJob?> GetByIdAsync(Guid id);



    }

}

