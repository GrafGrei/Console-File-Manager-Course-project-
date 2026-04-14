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
                state.SelectedIndex--;
                break;

            case ActionType.Down:
                state.SelectedIndex++;
                break;
        }
    }
}

