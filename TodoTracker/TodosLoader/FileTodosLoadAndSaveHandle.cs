using System.Text;

namespace TodoTracker.TodosLoader;

/// <summary>
/// Used to load and save Todos to a file
/// </summary>
internal class FileTodosLoadAndSaveHandle : ITodosLoadAndSaveHandle
{
    /// <summary>
    /// Used to indicate when a Todo ends and a new start
    /// </summary>
    internal const char TodoSPLIT = (char)6; // DO NOT CHANGE AFTER RELEASE

    private string _path;

    public FileTodosLoadAndSaveHandle(string Path)
    {
        _path = Path;
    }

    List<Todo> ITodosLoadAndSaveHandle.LoadTodos()
    {
        List<Todo> Todos = new List<Todo>();

        if (!File.Exists(_path))
            return Todos;

        List<string> TodosText = File.ReadAllText(_path).Split(TodoSPLIT).ToList();

        for (int i = 0; i < TodosText.Count - 1; i++)
            Todos.Add(Todo.Parse(TodosText[i]));

        return Todos;
    }

    void ITodosLoadAndSaveHandle.SaveTodos(List<Todo> Todos)
    {
        StringBuilder file = new StringBuilder();

        foreach (Todo Todo in Todos)
        {
            file.Append(Todo.ToString());

            file.Append(TodoSPLIT);
        }

        if (!File.Exists(_path))
            using (File.Create(_path)) ;

        File.WriteAllText(_path, file.ToString());
    }
}
