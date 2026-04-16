namespace ConsoleFileManager.Render;

public static class Elemens
{    
    public static void DrawHorizontal(int x, int y, int width)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(new string('─', width));
    } 

    public static void DrawVertical(int x, int y, int height)
    {
        for (int i = 0; i < height; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write('│');
        }
    }

    public static void DrawUpCorner(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write('┬');
    }

    public static void DrawDownCorner(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write('┴');
    }  
}


//┬
//│
//─
//