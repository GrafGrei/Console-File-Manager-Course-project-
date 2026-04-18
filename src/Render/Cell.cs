namespace ConsoleFileManager.Render;

public class Cell
{
    public char Ch;
    public ConsoleColor Fg;
    public ConsoleColor? Bg = null;

    public Cell(char ch = ' ', ConsoleColor fg = ConsoleColor.White, ConsoleColor? bg = null)
    {
        Ch = ch;
        Fg = fg;
        Bg = bg;
    }
}