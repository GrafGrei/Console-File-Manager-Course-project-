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

            state.CurentDirList = dirs
                .Concat(files)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading directory: {ex.Message}");
            state.CurentDirList = new List<string>();
        }
    }
}