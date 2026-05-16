namespace ConsoleFileManager.EventHendler;

public enum EventType
{
    Dot,
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
            ConsoleKey.OemPeriod => EventType.Dot,
            ConsoleKey.UpArrow => EventType.Up,
            ConsoleKey.DownArrow => EventType.Down,
            ConsoleKey.LeftArrow => EventType.Left,
            ConsoleKey.RightArrow => EventType.Right,
            ConsoleKey.Q => EventType.Quit,
            _ => EventType.None
        };
    }
}