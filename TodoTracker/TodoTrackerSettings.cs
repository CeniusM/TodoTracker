using TaskTracker;

namespace TodoTracker;

/*
 * TodoTrackerSettings string format is a line based format, so each line is a different varible.
 * Every line have the name of the varible followed by a ': ' where after the data itself is found.
 */


internal enum TodosSaveFormat
{
    None,
    LocalFile,
    Web,
}

internal class TodoTrackerSettings
{
    private const string DefaultFileSettingsName = "DefaultSettings.txt";
    private const string DefaultFileSaveName = "DefaultSave.txt";

    internal TodosSaveFormat SaveFormat;
    internal string RootFolderPath;
    internal string FileSavePath;

    private TodoTrackerSettings()
    {

    }

    public void SetSaveFormatToLocalFile(string filePath)
    {
        FileSavePath = filePath;
        SaveFormat = TodosSaveFormat.LocalFile;
    }

    public override string ToString()
    {
        TextArgsHelper args = new TextArgsHelper();

        args.AddArg(nameof(SaveFormat), Enum.GetName<TodosSaveFormat>(SaveFormat) ?? "Null");
        args.AddArg(nameof(FileSavePath), FileSavePath);
        args.AddArg(nameof(RootFolderPath), RootFolderPath);

        return args.ToString();
    }

    public static TodoTrackerSettings LoadSettings(string folderPath)
    {
        TodoTrackerSettings settings = new TodoTrackerSettings();

        string filePath = folderPath + DefaultFileSettingsName;

        bool fileExist = File.Exists(filePath);

        if (!fileExist)
        {
            settings.FileSavePath = folderPath + DefaultFileSaveName;
            settings.SaveFormat = TodosSaveFormat.LocalFile;
            settings.RootFolderPath = folderPath;

            return settings;
        }

        Dictionary<string, string> args = TextArgsHelper.GetStringArgsFromFile(filePath);

        settings.SaveFormat = Enum.Parse<TodosSaveFormat>(args[nameof(SaveFormat)]);
        settings.FileSavePath = args[nameof(FileSavePath)];
        settings.RootFolderPath = args[nameof(RootFolderPath)];

        return settings;
    }

    internal void SaveSettings()
    {
        string args = ToString();

        File.WriteAllText(RootFolderPath + DefaultFileSettingsName, args);
    }
}
