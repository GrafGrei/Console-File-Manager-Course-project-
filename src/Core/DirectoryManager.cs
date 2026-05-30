namespace  ConsoleFileManager.Core;

using System.IO;


public static class DirectoryManager
{
    public static void LoadCurentDir(AppState state)
    {
        try
        {   
            state.CurentDirList = new List<string>();

            var dirs = Directory.GetDirectories(state.CurrentPath)
                .OrderBy(x => x);

            var files = Directory.GetFiles(state.CurrentPath)
                .OrderBy(x => x);

            List<String> list = dirs
                .Concat(files)
                .ToList();

            foreach (var item in list){
                if (!(IsHidden(item) & !state.visibleHiden))
                {
                    state.CurentDirList.Add(item);
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading directory: {ex.Message}");
            state.CurentDirList = new List<string>();
        }
    }

    public static void LoadSelectDir(AppState state)
    {
        try
        {
            var path = state.CurentDirList[state.SelectedIndex];
            var dirs = Directory.GetDirectories(path)
                .OrderBy(x => x);

            var files = Directory.GetFiles(path)
                .OrderBy(x => x);

            state.SelectDirList = dirs
                .Concat(files)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading directory: {ex.Message}");
            state.SelectDirList = new List<string>();
        }
    }

    public static void LoadParentDir(AppState state)
    {
        try
        {
            var parentPath = Directory.GetParent(state.CurrentPath)?.FullName;
            if (parentPath == null)
            {
                state.ParentDirList = new List<string>();
                return;
            }

            var dirs = Directory.GetDirectories(parentPath)
                .OrderBy(x => x);

            var files = Directory.GetFiles(parentPath)
                .OrderBy(x => x);

            state.ParentDirList = dirs
                .Concat(files)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading directory: {ex.Message}");
            state.ParentDirList = new List<string>();
        }
    }

    public static bool IsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    public static bool IsHidden(string path)
    {
        string name = Path.GetFileName(path);

        if (name.StartsWith('.'))
            return true;

        return (File.GetAttributes(path) & FileAttributes.Hidden) != 0; 
    }

    public static void CopyFile(string targetDir, string file, bool overwrite)
    {
        string dest = Path.Combine(
            targetDir,
            Path.GetFileName(file)
        );

        File.Copy(file, dest, overwrite: overwrite);
    }

    public static void MoveFile(string targetDir, string file, bool overwrite)
    {
        string dest = Path.Combine(
            targetDir,
            Path.GetFileName(file)
        );

        File.Move(file, dest, overwrite: overwrite);
    }

    public static void CopyFiles(string targetDir, List<string> filesList, bool overwrite)
    {
        foreach (string file in filesList)
        {
            DirectoryManager.CopyFile(targetDir, file, overwrite);
        }
    }

    public static void MoveFiles(string targetDir, List<string> filesList, bool overwrite)
    {
        foreach (string file in filesList)
        {
            DirectoryManager.MoveFile(targetDir, file, overwrite);
        }
    }
}