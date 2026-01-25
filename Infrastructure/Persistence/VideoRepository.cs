using Application.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
namespace Infrastructure.Persistence
{
    public class InMemoryVideoRepository : IVideoRepository
    {
        private readonly ConcurrentDictionary<Guid, VideoJob> _store = new();
        private readonly string _connectionString;

        public InMemoryVideoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Task<VideoJob> CreateAsync(VideoJob job)
        {
            _store[job.Id] = job;
            return Task.FromResult(job);
        }
        public async Task<List<VideoJob>> SaveAsync(VideoJob job)
        {
            const string sql = @"
            INSERT INTO VideoJobs (Id, Topic, Status, CreatedAt)
            VALUES (@Id, @Topic, @Status, @CreatedAt)";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", job.Id);
            cmd.Parameters.AddWithValue("@Topic", job.Prompt);
            cmd.Parameters.AddWithValue("@Status", job.Status);
            cmd.Parameters.AddWithValue("@CreatedAt", job.CreatedAt);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            // Return a list containing the single job, matching the method's return type
            return new List<VideoJob> { job };
        }
        public async Task<VideoJob?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM VideoJobs WHERE Id = @Id";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (!reader.Read()) return null;

            return new VideoJob
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Script = reader.GetString(reader.GetOrdinal("Topic")),
               Status = (VideoStatus)Enum.Parse(typeof(VideoStatus), reader.GetString(reader.GetOrdinal("Status"))),
               CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
             };
             
            
        }


        public Task<VideoJob?> GetAsync(Guid id)
        {
            _store.TryGetValue(id, out var job);
            return Task.FromResult(job);
        }

        public Task UpdateStatusAsync(Guid id, VideoStatus status)
        {
            if (_store.TryGetValue(id, out var job))
            {
                job.Status = status;
                job.Progress = status.ToProgress();
            }
            return Task.CompletedTask;
        }

        public  Task UpdateProgressAsync(Guid id, int progress)
        {
            if (_store.TryGetValue(id, out var job))
            {
                job.Progress = progress;
            }
           return Task.CompletedTask;
        }

        public Task CompleteAsync(Guid id, string? videoPath = null)
        {
            if (_store.TryGetValue(id, out var job))
            {
                job.Status = VideoStatus.Completed;
                job.Progress = 100;
                job.VideoPath = videoPath;
            }
            return Task.CompletedTask;
        }
        public Task FailAsync(Guid id, string reason)
        {
            if (_store.TryGetValue(id, out var job))
            {
                job.Status = VideoStatus.Failed;
                job.Error = reason;
            }
            return Task.CompletedTask;
        }

       
    }


}
