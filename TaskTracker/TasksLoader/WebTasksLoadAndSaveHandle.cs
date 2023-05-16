using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.TasksLoader;

/// <summary>
/// Used to load and save tasks through
/// </summary>
internal class WebTasksLoadAndSaveHandle : ITasksLoadAndSaveHandle
{
    private string IP;

    public WebTasksLoadAndSaveHandle(string IP)
    {
        this.IP = IP;
    }

    List<Task> ITasksLoadAndSaveHandle.LoadTasks()
    {
        throw new NotImplementedException();
    }

    void ITasksLoadAndSaveHandle.SaveTasks(List<Task> tasks)
    {
        throw new NotImplementedException();
    }
}
