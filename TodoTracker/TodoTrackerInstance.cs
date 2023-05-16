using TodoTracker.TodosLoader;

namespace TodoTracker;

public class TodoTrackerInstance
{
    public int TodosCount => Todos.Count;
    public List<Todo> Todos { get; private set; }
    private TodoTrackerSettings _settings;
    private ITodosLoadAndSaveHandle _todosLoadAndSaveHandle;

    public TodoTrackerInstance()
    {
        if (!Directory.Exists(GetDefaultFolderPath()))
            Directory.CreateDirectory(GetDefaultFolderPath());

        Todos = new List<Todo>();

        _settings = TodoTrackerSettings.LoadSettings(GetDefaultFolderPath());

        _todosLoadAndSaveHandle = new FileTodosLoadAndSaveHandle(_settings.FileSavePath);

        //LoadTodos();
    }

    public void LoadTodos()
    {
        Todos = _todosLoadAndSaveHandle.LoadTodos();
    }

    public void SaveTodos()
    {
        _todosLoadAndSaveHandle.SaveTodos(Todos);
    }

    public void SaveSettings()
    {
        _settings.SaveSettings();
    }

    public void Flush()
    {
        Todos.Clear();
    }

    /// <summary>
    /// Lowest time till todo first.
    /// </summary>
    public void SortByDoDate()
    {
        Todos.Sort((x, y) => x.DoDate < y.DoDate ? 0 : 1);
    }

    /// <summary>
    /// Optional, will by default save to local file.
    /// </summary>
    public void SetSaveModeToFile(string path)
    {
        _settings.SetSaveFormatToLocalFile(path);
        _todosLoadAndSaveHandle = new FileTodosLoadAndSaveHandle(path);

    }

    public Todo this[int index] { get => Todos[index]; }

    private static string GetDefaultFolderPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TodoTracker\\";
    }
}
