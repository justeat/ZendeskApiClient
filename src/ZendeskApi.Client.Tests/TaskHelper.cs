using System.Threading.Tasks;

namespace ZendeskApi.Client.Tests
{
    public static class TaskHelper
    {
        public static Task<T> CreateTaskFromResult<T>(T result)
        {
            var taskSoruce = new TaskCompletionSource<T>();
            taskSoruce.SetResult(result);
            return taskSoruce.Task;
        }
    }
}
