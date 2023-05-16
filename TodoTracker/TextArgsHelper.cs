using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker;

internal class TextArgsHelper
{
    private string str = "";

    public TextArgsHelper()
    {

    }

    internal void AddArg(string name, string value)
    {
        str += name + ": " + value + "\n";
    }

    public override string ToString()
    {
        return str;
    }


    /// <summary>
    /// Gets a text file with difrent args in the format of:
    /// Name: Value\n
    /// Name2: Value2\n.
    /// And so on.
    /// Returns a dictionary with the name as the key.
    /// </summary>
    internal static Dictionary<string, string> GetStringArgsFromFile(string filePath)
    {
        string[] args = File.ReadAllLines(filePath);

        return GetStringArgs(args);
    }

    /// <summary>
    /// Gets an array of stirngs with difrent args in the format of:
    /// Name: Value.
    /// Returns a dictionary with the name as the key.
    /// </summary>
    internal static Dictionary<string, string> GetStringArgs(string args)
    {
        return GetStringArgs(args.Split('\n'));
    }

    /// <summary>
    /// Gets string with difrent args in the format of:
    /// Name: Value\n Name2: Value2. And so on.
    /// Returns a dictionary with the name as the key.
    /// </summary>
    internal static Dictionary<string, string> GetStringArgs(string[] args)
    {
        Dictionary<string, string> argsDictionary = new Dictionary<string, string>();

        for (int i = 0; i < args.Length; i++)
        {
            // Remove name from args
            string arg = args[i];
            if (arg == "") continue;
            string[] name_key = arg.Split(": ", 2);
            if (name_key.Length != 2)
            {
                throw new Exception($"Invalid arg \"{arg}\" at line {i + 1}");
            }
            string name = name_key[0];
            string value = name_key[1];
            if (argsDictionary.ContainsKey(name))
            {
                throw new NotImplementedException($"Key: \"{name}\" is allready defined");
            }
            argsDictionary.Add(name, value);
        }

        return argsDictionary;
    }
}
