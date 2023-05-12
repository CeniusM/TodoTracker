using TaskTracker;
using TaskTracker.TasksLoader;

TaskTrackerInstance taskTracker = new TaskTrackerInstance();
//taskTracker.SetSaveModeToFile("");

var t = new TaskTracker.Task("yeff", "im deff", DateTime.Now.AddHours(1827));

Console.WriteLine(t.ToString());
Console.WriteLine(TaskTracker.Task.Parse(t.ToString()));