namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;
using ConsoleFileManager.Utils;

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
        screen.Print(1, 0, TextUtils.Trim($"Current Path: {state.CurrentPath}", Console.WindowWidth-1).PadRight(Console.WindowWidth-1), ConsoleColor.White);
        screen.DrawHLine(0, 1, Console.WindowWidth, '─', ConsoleColor.White);
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

        screen.Put(parentBlock, 1, '┬', ConsoleColor.White);
        screen.Put(parentBlock + dirBlock, 1, '┬', ConsoleColor.White);
        screen.Put(parentBlock, Console.WindowHeight - 2, '┴', ConsoleColor.White);
        screen.Put(parentBlock + dirBlock, Console.WindowHeight - 2, '┴', ConsoleColor.White);

        screen.DrawVLine(parentBlock, 2, Console.WindowHeight - 4, '│', ConsoleColor.White);
        screen.DrawVLine(parentBlock + dirBlock, 2, Console.WindowHeight - 4, '│', ConsoleColor.White);
    
    }

    private void DrawParentDirList()
    {
        screen.Print(1, 2, "Work in progress");
    }

    private void DrawDirList()
    {
        int cursor = 2;

        int start = state.ScrollOffset;
        int end = Math.Min(start + state.VisibleHeight, state.CurentDirList.Count);

        screen.ClearZone(parentBlock+1, 2, parentBlock + dirBlock, Console.WindowHeight-2);

        for (int i = start; i < end; i++)
        {   
            cursor++;
            FileStyle style = styleProvider.GetStyle(state.CurentDirList[i]);
            string r_name = " " + Path.GetFileName(state.CurentDirList[i]);
            string name = TextUtils.Trim(r_name, dirBlock-4).PadRight(dirBlock - 4);
            if (state.SelectedFiles.Contains(state.CurentDirList[i]))
            {
                screen.Put(parentBlock, cursor - 1, ' ', ConsoleColor.White, ConsoleColor.Yellow);
            }

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
        screen.Print(parentBlock + dirBlock + 2, 2, "Work in progress");
        }


}