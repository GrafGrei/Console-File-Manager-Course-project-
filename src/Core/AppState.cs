namespace ConsoleFileManager.Core;

public class AppState
{
    public string CurrentPath { get; set; } = "/";
    public List<string> Files { get; set; } = new();
    public int SelectedIndex { get; set; } = 0;
}