namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;

public static class Render
{    
    public static void Draw(AppState state)
    {
        Console.Clear();
        
        Console.SetCursorPosition(0, 0);

        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        int treeBlockWidth = width / 6;
        int directoryBlockWidth = (width - treeBlockWidth)/2;
        int previewBlockWidth = width - treeBlockWidth - directoryBlockWidth;
        
        string AppName = "Console File Manager";

        Console.SetCursorPosition((width - AppName.Length) / 2, 0);

        Console.Write(AppName);

        Elemens.DrawHorizontal(0, 1, width);
        
        Console.SetCursorPosition(1, 2);
        Console.WriteLine($"Path: {state.CurrentPath}".PadRight(width));

        Elemens.DrawHorizontal(0, 3, width);

        Elemens.DrawUpCorner(treeBlockWidth, 3);
        Elemens.DrawUpCorner(treeBlockWidth + directoryBlockWidth, 3);

        Elemens.DrawVertical(treeBlockWidth, 4, height - 6);
        Elemens.DrawVertical(treeBlockWidth + directoryBlockWidth, 4, height - 6);

        Elemens.DrawHorizontal(0, height - 2, width);

        Elemens.DrawDownCorner(treeBlockWidth, height - 2);
        Elemens.DrawDownCorner(treeBlockWidth + directoryBlockWidth, height - 2);

    }
}