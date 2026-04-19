namespace ConsoleFileManager.EventHendler;

public enum EventType
{
    None,
    Up,
    Down,
    Left,
    Right,
    Quit
}

public class EventHendler
{
    private int lastW;
    private int lastH;

    public EventType Read()
    {

        if (!Console.KeyAvailable)
            return EventType.None;

        var key = Console.ReadKey(true);

        return key.Key switch
        {
            ConsoleKey.UpArrow => EventType.Up,
            ConsoleKey.DownArrow => EventType.Down,
            ConsoleKey.LeftArrow => EventType.Left,
            ConsoleKey.RightArrow => EventType.Right,
            ConsoleKey.Q => EventType.Quit,
            _ => EventType.None
        };
    }
}