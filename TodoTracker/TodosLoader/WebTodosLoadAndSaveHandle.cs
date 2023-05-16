using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTracker.TodosLoader;

/// <summary>
/// Used to load and save Todos through
/// </summary>
internal class WebTodosLoadAndSaveHandle : ITodosLoadAndSaveHandle
{
    private string IP;

    public WebTodosLoadAndSaveHandle(string IP)
    {
        this.IP = IP;
    }

    List<Todo> ITodosLoadAndSaveHandle.LoadTodos()
    {
        throw new NotImplementedException();
    }

    void ITodosLoadAndSaveHandle.SaveTodos(List<Todo> Todos)
    {
        throw new NotImplementedException();
    }
}
