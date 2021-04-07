using System;
using System.Threading.Tasks;

namespace SniffExplorer.UI.Misc
{
    public static class TaskExtensions
    {
        public static async void FireAndForget(this Task task, Action<Exception> cb)
        {
            await task.ContinueWith(_ => { cb.Invoke(_.Exception!); }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static async void FireAndForget(this Task task)
        {
            try {
                await task;
            } catch (Exception _) {

            }
        }

    }
}
