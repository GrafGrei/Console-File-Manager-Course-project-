namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;


public class ScreenBuffer
{
    private int W;
    private int H;
    private AppState _state;

    Cell[,] curr = null!;
    Cell[,] prev = null!;

    public ScreenBuffer(AppState state)
    {   
        _state = state;
        UpdateSize(force: true);
    }

    public void UpdateSize(bool force = false)
    {
        int newW = Console.WindowWidth;
        int newH = Console.WindowHeight;

        if (!force && newW == W && newH == H)
            return;

        W = newW;
        H = newH;

        curr = new Cell[H, W];
        prev = new Cell[H, W];

        _state.VisibleHeight = H - 6;

        Console.Clear();

        Clear(curr);
        Clear(prev);
    }

    private void Clear(Cell[,] buffer)
    {
        for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++)
            buffer[y, x] = new Cell(' ', ConsoleColor.White, null);
    }

    public void Put(int x, int y, char c, ConsoleColor fg = ConsoleColor.White, ConsoleColor? bg = null)
    {
        if (x < 0 || x >= W || y < 0 || y >= H)
            return;

        curr[y, x] = new Cell(c, fg, bg);
    }

    public void Print(int x, int y, string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor? bg = null)
    {
        for (int i = 0; i < text.Length; i++)
        {
            Put(x + i, y, text[i], fg, bg);
        }
    }

    public void DrawHLine(int x, int y, int length,char c, ConsoleColor fg = ConsoleColor.White, ConsoleColor? bg = null)
    {
        if (y < 0 || y >= H) return;

        for (int i = 0; i < length; i++)
        {
            int nx = x + i;
            if (nx < 0 || nx >= W) continue;

            curr[y, nx] = new Cell(c, fg, bg);
        }
    }

    public void DrawVLine(int x, int y, int length, char c, ConsoleColor fg = ConsoleColor.White, ConsoleColor? bg = null)
    {
        if (x < 0 || x >= W) return;

        for (int i = 0; i < length; i++)
        {
            int ny = y + i;
            if (ny < 0 || ny >= H) continue;

            curr[ny, x] = new Cell(c, fg, bg);
        }
    }
    
    public void Render()
    {
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                if (!CellsEqual(curr[y, x], prev[y, x]))
                {
                    Console.SetCursorPosition(x, y);

                    Console.ForegroundColor = curr[y, x].Fg;
                    if (curr[y, x].Bg.HasValue)
                    {
                        Console.BackgroundColor = curr[y, x].Bg.Value;
                    }
                    else
                    {
                        Console.Write("\x1b[49m");
                    }

                    Console.Write(curr[y, x].Ch);
                }
            }
        }
        Console.ResetColor();
    }

    private bool CellsEqual(Cell a, Cell b)
    {
        return a.Ch == b.Ch &&
            a.Fg == b.Fg &&
            a.Bg == b.Bg;
    }

    public void Swap()
    {
        for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++)
            prev[y, x] = curr[y, x];
    }
}

