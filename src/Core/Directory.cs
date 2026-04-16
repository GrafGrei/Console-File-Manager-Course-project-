namespace  ConsoleFileManager.Core;

using System.IO;


public static class DirectoryManager
{
    public static void LoadCurentDir(AppState state)
    {
        try
        {
            var dirs = Directory.GetDirectories(state.CurrentPath)
                .OrderBy(x => x);

            var files = Directory.GetFiles(state.CurrentPath)
                .OrderBy(x => x);

            state.Files = dirs
                .Concat(files)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading directory: {ex.Message}");
            state.Files = new List<string>();
        }
    }

    public static (string icon, ConsoleColor color) GetFileStyle(string path)
    {
    if (Directory.Exists(path))
        return ("", ConsoleColor.Blue); // folder

    string ext = Path.GetExtension(path).ToLower();

    return ext switch
    {
        // CODE
        ".cs" => ("", ConsoleColor.Cyan),
        ".js" => ("", ConsoleColor.Yellow),
        ".ts" => ("", ConsoleColor.Blue),
        ".cpp" or ".c" or ".h" => ("", ConsoleColor.Blue),
        ".java" => ("", ConsoleColor.Red),
        ".py" => ("", ConsoleColor.Yellow),
        ".go" => ("", ConsoleColor.Cyan),
        ".rs" => ("", ConsoleColor.DarkYellow),

        // WEB
        ".html" => ("", ConsoleColor.Red),
        ".css" => ("", ConsoleColor.Blue),
        ".scss" => ("", ConsoleColor.Magenta),
        ".json" => ("", ConsoleColor.Yellow),
        ".xml" => ("", ConsoleColor.DarkYellow),
        ".yml" or ".yaml" => ("", ConsoleColor.Gray),

        // CONFIG
        ".conf" or ".ini" => ("", ConsoleColor.Gray),

        // DOCS
        ".md" => ("", ConsoleColor.White),
        ".txt" => ("", ConsoleColor.Gray),
        ".pdf" => ("", ConsoleColor.Red),

        // IMAGES
        ".png" or ".jpg" or ".jpeg" or ".gif" or ".webp"
            => ("", ConsoleColor.Magenta),

        // VIDEO
        ".mp4" or ".mkv" or ".avi"
            => ("", ConsoleColor.DarkMagenta),

        // AUDIO
        ".mp3" or ".wav" or ".flac"
            => ("", ConsoleColor.Green),

        // ARCHIVES
        ".zip" or ".tar" or ".gz" or ".rar" or ".7z"
            => ("", ConsoleColor.DarkYellow),

        // EXEC
        ".exe" or ".bin" or ".sh"
            => ("", ConsoleColor.Green),

        // DB
        ".db" or ".sqlite"
            => ("", ConsoleColor.DarkCyan),

        // DEFAULT
        _ => ("", ConsoleColor.Gray)
    };
    }
}