using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker;

/*
 * Task string format is a line based format, so each line is a different varible.
 * Every line have the name of the varible followed by a ': ' where after the data itself is found.
 */

public class Task
{
    public string Name { get; internal set; }
    public string Description { get; internal set; }
    public DateTime DoDate { get; internal set; }
    public DateTime LastEdited { get; internal set; }

    public Task(string name, string description, DateTime doDate)
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
        StringBuilder output = new StringBuilder();

        output.Append($"Name: {Name}\n");
        output.Append($"Description: {Description}\n");
        output.Append($"DoDate: {DoDate.ToString()}\n");
        output.Append($"LastEdited: {LastEdited.ToString()}\n");

        return output.ToString();
    }

    public static Task Parse(string input)
    {
        string[] args = input.Split('\n');

        for (int i = 0; i < args.Length; i++)
        {
            // Remove name from args
            string arg = args[i];
            if (arg == "") continue;
            int count = arg.IndexOf(':') + 2;
            args[i] = arg.Remove(0, count);
        }

        string Name = args[0];
        string Description = args[1];
        DateTime DoDate = DateTime.Parse(args[2]);
        DateTime LastEdited = DateTime.Parse(args[2]);

        return new Task(Name, Description, DoDate);
    }
}
