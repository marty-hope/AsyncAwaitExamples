using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleWPFApp.Commands
{
   

    public static class AsyncExtensions

    {

        public static IEnumerable<Task<T>> 
            InCompletionOrder<T>(this IEnumerable<Task<T>> tasks)
        {
            var inputs = tasks;
            var results = inputs.Select(i => new TaskCompletionSource<T>()).ToList();

            int index = -1;
            foreach (var task in inputs)
            {
                task.ContinueWith((t, state) =>
                {
                    var nextResult = results[Interlocked.Increment(ref index)];
                    nextResult.TrySetResult(t.Result);
                }, TaskContinuationOptions.ExecuteSynchronously);
            }
            return results.Select(r => r.Task);
        }

    }

}

