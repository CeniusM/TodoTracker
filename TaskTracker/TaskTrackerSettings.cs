using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskTracker;

/*
 * TaskTrackerSettings string format is a line based format, so each line is a different varible.
 * Every line have the name of the varible followed by a ': ' where after the data itself is found.
 */


internal enum TasksSaveFormat
{
    None,
    LocalFile,
    Web,
}

internal class TaskTrackerSettings
{
    internal TasksSaveFormat SaveFormat;
    internal string FilePath;

    public TaskTrackerSettings()
    {
        SaveFormat = TasksSaveFormat.None;
        FilePath = "";
    }

    public override string ToString()
    {
        StringBuilder output = new StringBuilder();

        throw new NotImplementedException();

        return output.ToString();
    }

    public static TaskTrackerSettings GetDefault(string filePath)
    {
        var settings = new TaskTrackerSettings();
        settings.SaveFormat = TasksSaveFormat.LocalFile;
        settings.FilePath = filePath;
        return settings;
    }

    public static TaskTrackerSettings Parse(string input)
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

        throw new NotImplementedException();

        return new TaskTrackerSettings();
    }
}
