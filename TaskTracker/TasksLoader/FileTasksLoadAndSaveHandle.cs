using System.Text;

namespace TaskTracker.TasksLoader;

/// <summary>
/// Used to load and save tasks to a file
/// </summary>
internal class FileTasksLoadAndSaveHandle : ITasksLoadAndSaveHandle
{
    /// <summary>
    /// Used to indicate when a task ends and a new start
    /// </summary>
    internal const char TASKSPLIT = (char)6; // DO NOT CHANGE AFTER RELEASE

    private string _path;

    public FileTasksLoadAndSaveHandle(string Path)
    {
        _path = Path;
    }

    List<Task> ITasksLoadAndSaveHandle.LoadTasks()
    {
        List<Task> tasks = new List<Task>();

        List<string> tasksText = File.ReadAllText(_path).Split(TASKSPLIT).ToList();

        for (int i = 0; i < tasksText.Count - 1; i++)
            tasks.Add(Task.Parse(tasksText[i]));

        return tasks;
    }

    void ITasksLoadAndSaveHandle.SaveTasks(List<Task> tasks)
    {
        StringBuilder file = new StringBuilder();

        foreach (Task task in tasks)
        {
            file.Append(task.ToString());

            file.Append(TASKSPLIT);
        }

        File.WriteAllText(_path, file.ToString());
    }
}
