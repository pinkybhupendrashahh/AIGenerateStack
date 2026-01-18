using Application.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class VideoRepository : IVideoRepository
    {
        public Task<VideoJob?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(VideoJob job)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusAsync(Guid id, VideoStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
