using ConsoleFileManager.Core;
using ConsoleFileManager.Render;
using ConsoleFileManager.Utils;

namespace ConsoleFileManager.EventHendler;

public static class InputHendler
{
    public static void HendleInput(AppState state, ScreenBuffer screen, UI ui)
    {    
        state.InputText = Path.GetFileName(state.CurentDirList[state.SelectedIndex]);

        state.IsInputMode = true;
        while (true)
        {   
            ui.DrawInputFeald();
            screen.UpdateSize();
            screen.Render();
            screen.Swap();

            

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                state.IsInputMode = false;
                break;
            }

            if (key.Key == ConsoleKey.Escape)
            {
                state.IsInputMode = false;
                break;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (state.InputText .Length > 0)
                {
                    state.InputText = state.InputText[..^1];
                }
            }
            else
            {
                state.InputText += key.KeyChar;
            }

        }
    }
}
