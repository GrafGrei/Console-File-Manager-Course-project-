namespace ConsoleFileManager.Render;

using ConsoleFileManager.Core;
using Spectre.Console;

public static class Render
{    
    public static void Draw(AppState state)
    {
        AnsiConsole.Clear();
        
        Console.SetCursorPosition(0, 0);

        AnsiConsole.Write(
            new Rows(
                new Panel(
                    "File Manager"
                    )
                    .NoBorder()
                    .Padding(0, 0)
                    .Expand(),
                new Panel(
                    $"""Current Path: {state.CurrentPath}"""
                    )
                    .NoBorder()
                    .Padding(1, 1)
                    .Expand(),
                new Columns(
                    new Panel("LEFT")
                        .Header("L")
                        .Border(BoxBorder.Rounded),

                    new Panel("MIDDLE")
                        .Header("M")
                        .Border(BoxBorder.Rounded),

                    new Panel("RIGHT")
                        .Header("R")
                        .Border(BoxBorder.Rounded)
                ),
                new Panel("BOTTOM")
                    .Header("B")
                    .Border(BoxBorder.Rounded)
            )
        );



    }
}