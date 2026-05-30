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
            if(!state.IsInputMode)
                HandleEvent(eventHandler.Read());
            else
                InputHendler.HendleInput(state, screen, ui);

            screen.UpdateSize();

            DirectoryManager.LoadCurentDir(state);
            DirectoryManager.LoadParentDir(state);

            ui.Draw();

            screen.Render();
            screen.Swap();
        }
    }

    static void HandleEvent(EventType e)
    {
        switch (e)
        {
            case EventType.Rename:
                state.IsInputMode = true;
                break;

            case EventType.Select:
                string file = state.CurentDirList[state.SelectedIndex];
                if (!state.SelectedFiles.Contains(file))
                {
                    state.SelectedFiles.Add(file);
                } else
                {
                    state.SelectedFiles.Remove(file);
                }
                if (state.SelectedIndex < state.CurentDirList.Count - 1)
                    state.SelectedIndex++;

                if (state.SelectedIndex >= state.ScrollOffset + state.VisibleHeight)
                    state.ScrollOffset++;
            break;

            case EventType.Yank:
                state.YankedList.Clear();
                state.CutYank = false;
                state.YankedList.AddRange(state.SelectedFiles);
            break;

            case EventType.UnYank:
                state.YankedList.Clear();
            break;

            case EventType.CutYank:
                state.YankedList.Clear();
                state.CutYank = true;
                state.YankedList.AddRange(state.SelectedFiles);
            break;

            case EventType.Paste:
                if (state.CutYank)
                {
                    DirectoryManager.MoveFiles(state.CurrentPath, state.YankedList, true);
                    state.YankedList.Clear();
                    state.SelectedFiles.Clear();
                } else
                {
                    DirectoryManager.CopyFiles(state.CurrentPath, state.YankedList, true);
                    state.YankedList.Clear();
                    state.SelectedFiles.Clear();
                }
            break;

            case EventType.THiden:
                state.visibleHiden = !state.visibleHiden;
                break;

            case EventType.Up:
                if (state.SelectedIndex <= 0)
                {
                    state.SelectedIndex = state.CurentDirList.Count-1;
                    state.ScrollOffset = state.CurentDirList.Count - state.VisibleHeight;
                } else
                {

                    if (state.SelectedIndex > 0)
                        state.SelectedIndex--;

                    if (state.SelectedIndex - 3 < state.ScrollOffset && state.SelectedIndex - 3 >= 0)
                        state.ScrollOffset--;
                }
                break;

            case EventType.Down:
                if (state.SelectedIndex >= state.CurentDirList.Count-1)
                {
                    state.SelectedIndex = 0;
                    state.ScrollOffset = 0;
                } else
                {
                    if (state.SelectedIndex < state.CurentDirList.Count - 1)
                        state.SelectedIndex++;

                    if (state.SelectedIndex + 3 >= state.ScrollOffset + state.VisibleHeight && state.SelectedIndex + 3 < state.CurentDirList.Count )
                        state.ScrollOffset++;
                }
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




// ╯ ╮ ╭ ╰ │ ─ ┴ ┬
//╭─┬╮
//│ ││ 
//╰─┴╯

