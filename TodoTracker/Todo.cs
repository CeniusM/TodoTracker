using System.Text;
using TaskTracker;

namespace TodoTracker;

/*
 * Todo string format is a line based format, so each line is a different varible.
 * Every line have the name of the varible followed by a ': ' where after the data itself is found.
 */

public class Todo
{
    public string Name { get; internal set; }
    public string Description { get; internal set; }
    public DateTime DoDate { get; internal set; }
    public DateTime LastEdited { get; internal set; }

    public Todo(string name, string description, DateTime doDate)
    {
        Name = name;
        Description = description;
        DoDate = doDate;
        LastEdited = DateTime.Now;
    }

    public void SetNewName(string name)
    {
        Name = name;
        Edited();
    }

    public void SetNetDescription(string description)
    {
        Description = description;
        Edited();
    }

    internal void Edited()
    {
        LastEdited = DateTime.Now;
    }

    public override string ToString()
    {
        TextArgsHelper args = new TextArgsHelper();

        args.AddArg(nameof(Name), Name);
        args.AddArg(nameof(Description), Description);
        args.AddArg(nameof(DoDate), DoDate.ToString());
        args.AddArg(nameof(LastEdited), LastEdited.ToString());

        return args.ToString();
    }

    public static Todo Parse(string input)
    {
        var args = TextArgsHelper.GetStringArgs(input);

        string Name = args[nameof(Name)];
        string Description = args[nameof(Description)];
        DateTime DoDate = DateTime.Parse(args[nameof(DoDate)]);
        DateTime LastEdited = DateTime.Parse(args[nameof(LastEdited)]);

        return new Todo(Name, Description, DoDate);
    }
}
