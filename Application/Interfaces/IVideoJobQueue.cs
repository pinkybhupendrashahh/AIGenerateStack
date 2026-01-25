using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    using Application.background;
    using System.Threading.Channels;

    public interface IVideoJobQueue
    {
        ValueTask EnqueueAsync(VideoJobMessage message);
        ValueTask<VideoJobMessage> DequeueAsync(CancellationToken token);
        //ValueTask EnqueueAsync(Guid jobId);
        //ValueTask<Guid> DequeueAsync(CancellationToken cancellationToken);
    }


}
