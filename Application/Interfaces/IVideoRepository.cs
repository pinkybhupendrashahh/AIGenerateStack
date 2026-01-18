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
            Task SaveAsync(VideoJob job);
            Task<VideoJob?> GetAsync(Guid id);
            Task UpdateStatusAsync(Guid id, VideoStatus status);
        }
    }

