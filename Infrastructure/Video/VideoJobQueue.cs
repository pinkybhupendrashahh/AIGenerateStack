using Application.background;
 using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    using System.Threading.Channels;
using System.Threading.Tasks;
namespace Infrastructure.Video
{

    public class VideoJobQueue : IVideoJobQueue
    {
        private readonly Channel<VideoJobMessage> _channel;
        public VideoJobQueue()
        {
            _channel = Channel.CreateUnbounded<VideoJobMessage>();
        }
        public async ValueTask EnqueueAsync(VideoJobMessage message)
        {
            await _channel.Writer.WriteAsync(message);
        }

        public async ValueTask<VideoJobMessage> DequeueAsync(CancellationToken token)
        {
            return await _channel.Reader.ReadAsync(token);
        }
    }


}
