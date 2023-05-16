

namespace TodoTracker.TodosLoader;

internal interface ITodosLoadAndSaveHandle
{
    internal List<Todo> LoadTodos();
    internal void SaveTodos(List<Todo> Todos);
}
