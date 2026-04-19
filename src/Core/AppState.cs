namespace ConsoleFileManager.Core;

public class AppState
{
    public string CurrentPath { get; set; } = "/home/graf";
    public List<string> CurentDirList { get; set; } = new();
    public List<string> ParentDirList { get; set; } = new();
    public int ParentSelectedIndex { get; set; } = 0;
    public int SelectedIndex { get; set; } = 0;
    public int ScrollOffset {get; set; } = 0;
    public int VisibleHeight { get; set; } = 0;
}