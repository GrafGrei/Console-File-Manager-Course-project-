namespace ConsoleFileManager.Core;

public class AppState
{
    public string CurrentPath { get; set; } = "/home/graf";
    public List<string> CurentDirList { get; set; } = new();
    public List<string> ParentDirList { get; set; } = new();
    public List<string> SelectDirList { get; set; } = new();
    public int ParentSelectedIndex { get; set; } = 0;
    public int SelectedIndex { get; set; } = 0;
    public int ScrollOffset {get; set; } = 0;
    public int VisibleHeight { get; set; } = 0;
    public bool visibleHiden{ get; set; } = false;
    public List<string> SelectedFiles{ get; set; } = new();
    public List<string> YankedList{ get; set; } = new(); 
    public bool CutYank{ get; set; } = false;
}