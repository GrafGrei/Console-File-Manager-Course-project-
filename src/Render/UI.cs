namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;

public class UI
{
    private ScreenBuffer screen;
    private AppState state;

    public UI(ScreenBuffer screen, AppState state)
    {
        this.screen = screen;
        this.state = state;
    }

    public void Draw()
    {
        DrawHeader();
        DrawFileList();
        DrawFooter();
    }

    private void DrawHeader()
    {
        screen.Print(0, 0, $"Current Path: {state.CurrentPath}", ConsoleColor.Yellow);
        screen.DrawHLine(0, 1, Console.WindowWidth, '-', ConsoleColor.DarkGray);
    }

    private void DrawFileList()
    {
        for (int i = 0; i < state.VisibleHeight; i++)
        {
            int fileIndex = state.ScrollOffset + i;
            if (fileIndex >= state.Files.Count)
                break;

            string fileName = Path.GetFileName(state.Files[fileIndex]);
            ConsoleColor fg = (fileIndex == state.SelectedIndex) ? ConsoleColor.Black : ConsoleColor.White;
            ConsoleColor bg = (fileIndex == state.SelectedIndex) ? ConsoleColor.White : ConsoleColor.Black;

            screen.Print(0, 2 + i, fileName.PadRight(Console.WindowWidth), fg, bg);
        }
    }

    private void DrawFooter()
    {
        screen.DrawHLine(0, Console.WindowHeight - 3, Console.WindowWidth, '-', ConsoleColor.DarkGray);
        screen.Print(0, Console.WindowHeight - 2, "Use Arrow Keys to navigate. Enter to open. Backspace to go back.", ConsoleColor.Yellow);
    }
}