using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class StationManager
    {
        ConcurrentQueue<TaskCompletionSource<bool>> queue = new ConcurrentQueue<TaskCompletionSource<bool>>();

        public async Task Do(Plane plane)
        {
            var mytcs = new TaskCompletionSource<bool>();
            if (queue.TryDequeue(out var tcs))
            {
                queue.Enqueue(mytcs);
                await tcs.Task;
            }

            else queue.Enqueue(mytcs);

            await Task.Delay(plane.waitingTime);

            mytcs.SetResult(true);
        }
    }
}
