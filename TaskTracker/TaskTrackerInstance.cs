using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.TasksLoader;

namespace TaskTracker;

/// <summary>
/// Class used for loading and saving tasks, and as an API to change and veiw the tasks at hand.
/// </summary>
public class TaskTrackerInstance
{
    public int TasksCount => Tasks.Count;
    public List<Task> Tasks;
    private TaskTrackerSettings _settings;
    private ITasksLoadAndSaveHandle _tasksLoadAndSaveHandle;

    public TaskTrackerInstance()
    {
        Tasks = new List<Task>();
        _tasksLoadAndSaveHandle = new FileTasksLoadAndSaveHandle("");

        LoadSettings();

        LoadTasks();
    }

    public void LoadTasks()
    {
        Tasks = _tasksLoadAndSaveHandle.LoadTasks();
    }

    public void SaveTasks()
    {
        _tasksLoadAndSaveHandle.SaveTasks(Tasks);
    }

    public void SetSaveModeToFile(string path)
    {
        _settings.FilePath = path;
        _settings.SaveFormat = TasksSaveFormat.LocalFile;

    }


    private void LoadSettings()
    {
        string SettingsPath = GetDefaultFolderPath() + "Settings.txt";

        // Check if first instance
        bool exists = File.Exists(SettingsPath);

        if (!exists)
        {
            // First time
            _settings = TaskTrackerSettings.GetDefault(GetDefaultFolderPath() + "\\DefaultSave.txt");
            SetSaveModeToFile(_settings.FilePath);
            return;
        }

        _settings = TaskTrackerSettings.Parse(File.ReadAllText(SettingsPath));
    }

    private static string GetDefaultFolderPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TaskTracker\\";
    }
}
