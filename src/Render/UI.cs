namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;
using ConsoleFileManager.Utils;

public class UI
{
    private ScreenBuffer screen;
    private AppState state;
    private FileStyleProvider styleProvider = new FileStyleProvider("/home/graf/Projects/Console File Manager (Course project) /src/FileStyles.yaml");

    static private int parentBlock = Console.WindowWidth / 6;
    static private int dirBlock = Console.WindowWidth - parentBlock;

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
    }

    private void DrawHeader()
    {   
        screen.Print(1, 0, TextUtils.Trim($"Current Path: {state.CurrentPath}", Console.WindowWidth-1).PadRight(Console.WindowWidth-1), ConsoleColor.White);
        screen.DrawHLine(0, 1, Console.WindowWidth, '─', ConsoleColor.White);
    }

    private void DrawFooter()
    {
        screen.DrawHLine(0, Console.WindowHeight - 3, Console.WindowWidth, '─', ConsoleColor.White);


        int count = state.CurentDirList.Count;
        int sel = state.SelectedIndex;

        string text = $"  {sel}/{count}  ".PadRight(11); 

        screen.Print(Console.WindowWidth- text.Length, Console.WindowHeight - 2, text, ConsoleColor.White, ConsoleColor.DarkBlue);
        
        
        string[] row1 = ["[↑↓] Рух", "[←] Назад", "[→] Відкрити", "[Space] Вибір"];
        string[] row2 = ["[Y] Копія", "[X] Вирізати", "[P] Вставити", "[R] Перейм.", "[.] Прихов.", "[Q] Вихід"];

        for (int i = 0; i < row1.Length; i++)
        {
            int x = i * Console.WindowWidth / row1.Length;
            screen.Print(x, Console.WindowHeight - 2, row1[i], ConsoleColor.White);
        }

        for (int i = 0; i < row2.Length; i++)
        {
            int x = i * Console.WindowWidth / row2.Length;
            screen.Print(x, Console.WindowHeight - 1, row2[i], ConsoleColor.White);
        }
    }

    private void DrawVerticalLines()
    {
        screen.Put(parentBlock, 1, '┬', ConsoleColor.White);
        screen.Put(parentBlock, Console.WindowHeight - 3, '┴', ConsoleColor.White);
        screen.DrawVLine(parentBlock, 2, Console.WindowHeight - 5, '│', ConsoleColor.White);    
    }

    private void DrawParentDirList()
    {
        int cursor = 2;

        int start = state.dScrollOffset;
        int end = Math.Min(start + state.VisibleHeight, state.ParentDirList.Count);

        screen.ClearZone(0, 2, parentBlock, Console.WindowHeight-3);

        for (int i = start; i < end; i++)
        {   
            cursor++;
            FileStyle style = styleProvider.GetStyle(state.ParentDirList[i]);
            string r_name = " " + Path.GetFileName(state.ParentDirList[i]);
            string name = TextUtils.Trim(r_name, parentBlock-4).PadRight(parentBlock - 4);
            {   
                screen.Put(1, cursor - 1, ' ');
                screen.Put(2, cursor - 1, style.Icon, style.FgColor);
                screen.Print(3, cursor - 1, name, ConsoleColor.White);
                screen.Put(parentBlock -1 , cursor - 1, ' ');
            }
        }

    }

    private void DrawDirList()
    {
        int cursor = 2;

        int start = state.ScrollOffset;
        int end = Math.Min(start + state.VisibleHeight, state.CurentDirList.Count);

        screen.ClearZone(parentBlock+1, 2, parentBlock + dirBlock, Console.WindowHeight-3);

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


    public void DrawInputFeald()
    {
        int fealdWith = NumberUtils.ClampValue(Console.WindowWidth, 20, dirBlock);

        screen.ClearZone((Console.WindowWidth - fealdWith)/2, 6 ,(Console.WindowWidth - fealdWith)/2+ fealdWith, 8);

        screen.Put((Console.WindowWidth - fealdWith)/2, 6 , '╭');
        screen.Print((Console.WindowWidth - fealdWith)/2+1, 6 ,"Rename:");
        screen.DrawHLine((Console.WindowWidth - fealdWith)/2+8, 6, fealdWith-8, '─', ConsoleColor.White);
        screen.Put((Console.WindowWidth - fealdWith)/2+ fealdWith-1, 6 , '╮');
        screen.Put((Console.WindowWidth - fealdWith)/2, 7 , '│');
        screen.Put((Console.WindowWidth - fealdWith)/2+ fealdWith-1, 7 , '│');
        screen.Put((Console.WindowWidth - fealdWith)/2, 8 , '╰');
        screen.DrawHLine((Console.WindowWidth - fealdWith)/2+1, 8, fealdWith-2, '─', ConsoleColor.White);
        screen.Put((Console.WindowWidth - fealdWith)/2+ fealdWith-1, 8 , '╯');
        screen.Print((Console.WindowWidth - fealdWith)/2+1, 7 ,state.InputText);     
    }

}

// ╯ ╮ ╭ ╰ │ ─ ┴ ┬
