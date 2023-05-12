

namespace TaskTracker.TasksLoader;

internal interface ITasksLoadAndSaveHandle
{
    internal List<Task> LoadTasks();
    internal void SaveTasks(List<Task> tasks);
}
