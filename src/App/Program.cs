namespace ConsoleFileManager.App;

using ConsoleFileManager.Core;
using ConsoleFileManager.EventHendler;
using ConsoleFileManager.Render;

class Program
{
    static bool running = true;

    static AppState state = new AppState();
    static ScreenBuffer screen = new ScreenBuffer(state);
    static EventHendler eventHandler = new EventHendler();
    static UI ui = new UI(screen, state);

    static void Main()
    {
        screen.UpdateSize();

        Console.Clear();
        Console.CursorVisible = false;


        while (running)
        {
            HandleEvent(eventHandler.Read());

            screen.UpdateSize();

            DirectoryManager.LoadCurentDir(state);

            ui.Draw();

            screen.Render();
            screen.Swap();
        }
    }

    static void HandleEvent(EventType e)
    {
        switch (e)
        {
            case EventType.Up:
                if (state.SelectedIndex > 0)
                    state.SelectedIndex--;

                if (state.SelectedIndex < state.ScrollOffset)
                    state.ScrollOffset--;

                break;

            case EventType.Down:
                if (state.SelectedIndex < state.CurentDirList.Count - 1)
                    state.SelectedIndex++;

                if (state.SelectedIndex >= state.ScrollOffset + state.VisibleHeight)
                    state.ScrollOffset++;
                break;

            case EventType.Right:
                if (Directory.Exists(state.CurentDirList[state.SelectedIndex]))
                {
                    state.CurrentPath = state.CurentDirList[state.SelectedIndex];
                    state.SelectedIndex = 0;
                    state.ScrollOffset = 0;
                }
                break;

            case EventType.Left:
                state.CurrentPath = Directory.GetParent(state.CurrentPath)?.FullName ?? state.CurrentPath;
                state.SelectedIndex = 0;
                state.ScrollOffset = 0;
                break;

            case EventType.Quit:
                Environment.Exit(0);
                break;
        }
    }
}




//┬
//│
//─
//┴