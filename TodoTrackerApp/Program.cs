using System;
using System.Reflection;
using TaskTrackerApp;
using TodoTracker;
using static System.Console;
using static System.ConsoleColor;
using static TaskTrackerApp.TodoVaribleSelected;

TodoTrackerInstance todoTracker = new TodoTrackerInstance();
bool Running = true;
CursorVisible = false;

string AddCharIfValid(string str, char c)
{
    if (c > 31 && c < 255 && c != 127)
        return str + c;
    return str;
}

void FillRestOfLineWithEmpty()
{
    int totalWidth = BufferWidth;
    int cursorX = GetCursorPosition().Left;
    WriteLine(new string(' ', totalWidth - cursorX));
}

void WriteFilledLine(string str)
{
    Write(str);
    FillRestOfLineWithEmpty();
}

int LoadTodoMenu()
{
    bool Choosing = true;
    int index = 0;
    int notAvalibleIndex = 4;

    void DrawLine(string str, int l)
    {
        if (l == -1)
            ForegroundColor = Blue;
        else if (l == index)
            ForegroundColor = DarkGreen;
        else
            ForegroundColor = Gray;
        WriteFilledLine(str);
    }

    while (Choosing)
    {
        SetCursorPosition(0, 0);

        DrawLine("Loadble todos", -1);
        DrawLine("Todo from local files", 0);
        DrawLine("Todos from the internet ( Not avalible )", 1);
        DrawLine("New empty todos", 2);

        var key = ReadKey();
        if (key.Key == ConsoleKey.DownArrow)
        {
            index++;
            if (index > 2)
                index = 2;
        }
        else if (key.Key == ConsoleKey.UpArrow)
        {
            index--;
            if (index < 0)
                index = 0;
        }
        else if (key.Key == ConsoleKey.Tab)
        {
            index++;
            if (index > 2)
                index = 0;
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            if (index == 1)
            {
                SetCursorPosition(0, notAvalibleIndex++);
                WriteFilledLine("Not avalible");
                continue;
            }
            Clear();
            return index;
        }
    }
    Clear();
    return -1;
}

int loadType = LoadTodoMenu();
WriteFilledLine("Loading");

if (loadType == 0)
{
    todoTracker.LoadTodos();
}
else if (loadType == 1)
{
    throw new NotImplementedException();
}
else if (loadType == 2)
{
    todoTracker.Flush();
}
else
{
    throw new NotImplementedException();
}

todoTracker.SortByDoDate();

Clear();

TodoVaribleSelected selectedArg = Description;
int todoIndex = 0;
void ClampIndex()
{
    if (todoTracker.TodosCount == 0)
    {
        todoIndex = 0;
        return;
    }
    todoIndex = Math.Clamp(todoIndex, 0, todoTracker.TodosCount - 1);
}
WriteLine(selectedArg);

//Print names and options
void Print()
{
    SetCursorPosition(0, 0);

    ForegroundColor = White;

    WriteFilledLine("Press esc to exit");

    WriteFilledLine("Press 'v' to create new todo");
    WriteFilledLine("Press 'x' to delete current todo");
    WriteFilledLine("Press 's' to save all todos");

    if (todoTracker.TodosCount == 0)
    {
        ForegroundColor = Green;
        WriteFilledLine("No todos, good job :)");
    }

    for (int i = 0; i < todoTracker.TodosCount; i++)
    {
        if (todoIndex == i)
            ForegroundColor = Green;
        else
            ForegroundColor = White;
        WriteFilledLine(todoTracker[i].Name);
    }
    WriteFilledLine("");
    SetCursorPosition(0, 0);
}

Todo CreateNewTodo()
{
    Clear();
    string name = "";
    string description = "";
    DateTime doDate = DateTime.Today;

    int index = 0;
    while (true)
    {
        SetCursorPosition(0, 0);
        if (index == 0)
            ForegroundColor = Green;
        else
            ForegroundColor = White;

        WriteFilledLine("Name: " + name);
        if (index == 1)
            ForegroundColor = Green;
        else
            ForegroundColor = White;
        WriteFilledLine("Description: " + description);

        if (index == 2)
            ForegroundColor = Green;
        else
            ForegroundColor = White;
        WriteFilledLine("DateTime: " + doDate);
        SetCursorPosition(0, 0);

        var key = ReadKey();

        if (key.Key == ConsoleKey.DownArrow)
        {
            index++;
            if (index > 2)
                index = 2;
        }
        else if (key.Key == ConsoleKey.UpArrow)
        {
            index--;
            if (index < 0)
                index = 0;
        }
        else if (key.Key == ConsoleKey.UpArrow)
        {
            index--;
            if (index < 0)
                index = 0;
        }
        else if (key.Key == ConsoleKey.Tab)
        {
            index++;
            if (index > 2)
                index = 0;
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            if (name == "" || description == "")
            {
                SetCursorPosition(0, 6);
                ForegroundColor = Red;
                WriteLine("Can't have empty name or description");
                SetCursorPosition(0, 0);
            }
            else
            {
                Clear();
                return new Todo(name, description, doDate);
            }
        }
        else
        {
            if (index == 0)
            {
                if (key.Key == ConsoleKey.Backspace && name.Length > 0)
                {
                    if (((int)key.Modifiers & (int)ConsoleModifiers.Control) != 0)
                        name = "";
                    else
                        name = name.Remove(name.Length - 1);
                }
                if (name.Length < 16)
                    name = AddCharIfValid(name, key.KeyChar);

            }
            if (index == 1)
            {
                if (key.Key == ConsoleKey.Backspace && description.Length > 0)
                {
                    if (((int)key.Modifiers & (int)ConsoleModifiers.Control) != 0)
                        description = "";
                    else
                        description = description.Remove(description.Length - 1);
                }
                if (description.Length < 64)
                    description = AddCharIfValid(description, key.KeyChar);
            }
            if (index == 2)
            {
            }
        }
    }
}

while (Running)
{
    Print();

    var key = ReadKey();

    if (key.Key == ConsoleKey.DownArrow)
    {
        todoIndex++;
    }
    else if (key.Key == ConsoleKey.Tab)
    {
        todoIndex++;
        if (todoIndex > todoTracker.TodosCount)
            todoIndex = 0;
    }
    else if (key.Key == ConsoleKey.UpArrow)
    {
        todoIndex--;
    }
    else if (key.Key == ConsoleKey.X)
    {
        if (todoTracker.TodosCount > 0)
            todoTracker.Todos.RemoveAt(todoIndex);
    }
    else if (key.Key == ConsoleKey.S)
    {
        todoTracker.SaveTodos();
    }
    else if (key.Key == ConsoleKey.V)
    {
        todoTracker.Todos.Add(CreateNewTodo());
    }
    else if (key.Key == ConsoleKey.Escape)
    {
        return;
    }
    ClampIndex();
}