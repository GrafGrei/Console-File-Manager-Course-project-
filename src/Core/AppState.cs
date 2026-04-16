namespace ConsoleFileManager.Core;

public class AppState
{
    public string CurrentPath { get; set; } = "/home/graf";
    public List<string> Files { get; set; } = new();
    public int SelectedIndex { get; set; } = 0;
    public int ScrollOffset {get; set; } = 0;
    public int VisibleHeight { get; set; } = Console.WindowHeight - 6;
}