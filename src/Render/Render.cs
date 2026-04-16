namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;
using System.IO;

public static class Render
{    
    public static void Draw(AppState state)
    {
        Console.Clear();
        
        Console.SetCursorPosition(0, 0);

        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        int treeBlockWidth = width / 6;
        int previewBlockWidth = width/3;
        int directoryBlockWidth = (width - previewBlockWidth - treeBlockWidth);
        
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

        

        int cursor = 4;

        int start = state.ScrollOffset;
        int end = Math.Min(start + state.VisibleHeight, state.Files.Count);

        for (int i = start; i < end; i++)
        {
            Console.SetCursorPosition(treeBlockWidth + 1, cursor++);

            var (icon, color) = DirectoryManager.GetFileStyle(state.Files[i]);

            string name = Path.GetFileName(state.Files[i])
                .PadRight(directoryBlockWidth - 5);

            if (i == state.SelectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = color;
                Console.Write(icon);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" {name}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("");
                Console.ResetColor();
            }
            else
            {   
                Console.ForegroundColor = color;
                Console.Write(" ");
                Console.Write(icon);
                Console.ResetColor();
                Console.Write($" {name}");
            }
        }
    }
}