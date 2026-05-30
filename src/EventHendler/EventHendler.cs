namespace ConsoleFileManager.EventHendler;

public enum EventType
{
    THiden,
    Rename,
    Select,
    Yank,
    UnYank,
    CutYank,
    Paste,
    None,
    Up,
    Down,
    Left,
    Right,
    Quit
}

public class EventHendler
{
    public EventType Read()
    {

        if (!Console.KeyAvailable)
            return EventType.None;

        var key = Console.ReadKey(true);

        return key.Key switch
        {
            ConsoleKey.Spacebar => EventType.Select,
            ConsoleKey.Y => EventType.Yank,
            ConsoleKey.R => EventType.Rename,
            ConsoleKey.X => EventType.CutYank,
            ConsoleKey.P => EventType.Paste,
            ConsoleKey.OemPeriod => EventType.THiden,
            ConsoleKey.UpArrow => EventType.Up,
            ConsoleKey.DownArrow => EventType.Down,
            ConsoleKey.LeftArrow => EventType.Left,
            ConsoleKey.RightArrow => EventType.Right,
            ConsoleKey.Q => EventType.Quit,
            _ => EventType.None
        };
    }
}