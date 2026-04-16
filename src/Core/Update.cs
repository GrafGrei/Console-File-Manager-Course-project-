namespace ConsoleFileManager.Core;

using ConsoleFileManager.Input;
using ConsoleFileManager.Core;

public static class StateManager
{
    public static void Update(AppState state, ActionType action)
    {
        switch (action)
        {
            case ActionType.Up:
                if (state.SelectedIndex > 0)
                    state.SelectedIndex--;

                if (state.SelectedIndex < state.ScrollOffset)
                    state.ScrollOffset--;

                break;

            case ActionType.Down:
                if (state.SelectedIndex < state.Files.Count - 1)
                    state.SelectedIndex++;

                if (state.SelectedIndex >= state.ScrollOffset + state.VisibleHeight)
                    state.ScrollOffset++;
                break;
        }
    }
}

