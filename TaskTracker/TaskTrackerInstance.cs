using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker;

/// <summary>
/// Class used for loading and saving tasks, and as an API to change and veiw the tasks at hand.
/// </summary>
public class TaskTrackerInstance
{
    public int TasksCount => _tasks.Count;
    private List<Task> _tasks;

    public TaskTrackerInstance()
    {
        
    }
}
