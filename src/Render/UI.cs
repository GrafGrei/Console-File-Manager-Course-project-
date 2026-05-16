namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;

public class UI
{
    private ScreenBuffer screen;
    private AppState state;
    private FileStyleProvider styleProvider = new FileStyleProvider("/home/graf/Projects/Console File Manager (Course project) /src/FileStyles.yaml");

    static private int parentBlock = Console.WindowWidth / 6;
    static private int dirBlock = Console.WindowWidth / 2;
    static private int prviewBlock = Console.WindowWidth - parentBlock - dirBlock;

    public UI(ScreenBuffer screen, AppState state)
    {
        this.screen = screen;
        this.state = state;
    }

    public void Draw()
    {
        styleProvider.Reload();
        DrawHeader();
        DrawFooter();
        DrawVerticalLines();
        DrawParentDirList();
        DrawDirList();
        DrawPreview();
        
    }

    private void DrawHeader()
    {   
        string AppName = "Console File Manager";
        int padding = (Console.WindowWidth - AppName.Length) / 2;
        screen.Print(padding, 0, AppName, ConsoleColor.Cyan);
        screen.DrawHLine(0, 1, Console.WindowWidth, '─', ConsoleColor.White);
        screen.Print(1, 2, $"Current Path: {state.CurrentPath}".PadRight(Console.WindowWidth-1), ConsoleColor.White);
        screen.DrawHLine(0, 3, Console.WindowWidth, '─', ConsoleColor.White);
    }

    private void DrawFooter()
    {
        screen.DrawHLine(0, Console.WindowHeight - 2, Console.WindowWidth, '─', ConsoleColor.White);
    }

    private void DrawVerticalLines()
    {
        parentBlock = Console.WindowWidth / 6;
        dirBlock = Console.WindowWidth / 2;
        prviewBlock = Console.WindowWidth - parentBlock - dirBlock;

        screen.Put(parentBlock, 3, '┬', ConsoleColor.White);
        screen.Put(parentBlock + dirBlock, 3, '┬', ConsoleColor.White);
        screen.Put(parentBlock, Console.WindowHeight - 2, '┴', ConsoleColor.White);
        screen.Put(parentBlock + dirBlock, Console.WindowHeight - 2, '┴', ConsoleColor.White);

        screen.DrawVLine(parentBlock, 4, Console.WindowHeight - 6, '│', ConsoleColor.White);
        screen.DrawVLine(parentBlock + dirBlock, 4, Console.WindowHeight - 6, '│', ConsoleColor.White);
    
    }

    private void DrawParentDirList()
    {
        // int cursor = 4;

        // foreach (var path in state.ParentDirList)
        // {   
        //     cursor++;
        //     FileStyle style = styleProvider.GetStyle(path);
        //     string name = " " + Path.GetFileName(path).PadRight(parentBlock - 5);
        //     screen.Put(1, cursor - 1, style.Icon, style.FgColor);
        //     screen.Print(2, cursor - 1, name, ConsoleColor.White);
        // }
    }

    private void DrawDirList()
    {
        int cursor = 4;

        int start = state.ScrollOffset;
        int end = Math.Min(start + state.VisibleHeight, state.CurentDirList.Count);

        screen.ClearZone(parentBlock+1, 4, parentBlock + dirBlock - 1, Console.WindowHeight-2);
 
        for (int i = start; i < end; i++)
        {   
            cursor++;
            FileStyle style = styleProvider.GetStyle(state.CurentDirList[i]);
            string name = " " + Path.GetFileName(state.CurentDirList[i]).PadRight(dirBlock - 5);
            if (i == state.SelectedIndex)
            {   
                screen.Put(parentBlock + 1, cursor - 1, '', ConsoleColor.DarkBlue);
                screen.Put(parentBlock + 2, cursor - 1, style.Icon, style.FgColor, ConsoleColor.DarkBlue);
                screen.Print(parentBlock + 3, cursor - 1, name, ConsoleColor.White, ConsoleColor.DarkBlue);
                screen.Put(parentBlock + dirBlock -1 , cursor - 1, '', ConsoleColor.DarkBlue);
            }
            else
            {   
                screen.Put(parentBlock + 1, cursor - 1, ' ');
                screen.Put(parentBlock + 2, cursor - 1, style.Icon, style.FgColor);
                screen.Print(parentBlock + 3, cursor - 1, name, ConsoleColor.White);
                screen.Put(parentBlock + dirBlock -1 , cursor - 1, ' ');
            }
        }

    }

    private void DrawPreview()
    {
        // if (state.CurentDirList.Count == 0)
        //     return;

        // string path = state.CurentDirList[state.SelectedIndex];

        // if (!Directory.Exists(path))
        // {
        //     screen.Print(parentBlock + dirBlock + 2, 4, "Directory preview not implemented yet", ConsoleColor.Gray);
        // }
        // else
        // {
        //     try
        //     {
        //         var lines = File.ReadAllLines(path);
        //         int maxLines = Console.WindowHeight - 6;
        //         int maxWidth = prviewBlock - 4;

        //         for (int i = 0; i < Math.Min(lines.Length, maxLines); i++)
        //         {
        //             string line = lines[i].Length > maxWidth ? lines[i].Substring(0, maxWidth - 3) + "..." : lines[i];
        //             screen.Print(parentBlock + dirBlock + 2, 4 + i, line, ConsoleColor.Gray);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         screen.Print(parentBlock + dirBlock + 2, 4, $"Error loading preview: {ex.Message}", ConsoleColor.Red);
        //     }
        // }
        }


}