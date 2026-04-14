namespace ConsoleFileManager.Core;

using ConsoleFileManager.Input;

public static class InputHandler
{
    public static ActionType Read()
    {
        var key = Console.ReadKey(true);

        return key.Key switch
        {
            ConsoleKey.UpArrow => ActionType.Up,
            ConsoleKey.DownArrow => ActionType.Down,
            ConsoleKey.Enter => ActionType.Enter,
            ConsoleKey.Backspace => ActionType.Back,
            ConsoleKey.Q => ActionType.Quit,
            _ => ActionType.None
        };
    }
}