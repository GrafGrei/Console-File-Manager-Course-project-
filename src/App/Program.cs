namespace ConsoleFileManager.App;

using Spectre.Console;
using ConsoleFileManager.Core;
using ConsoleFileManager.Input;
using ConsoleFileManager.Render;

class Program
{
    static bool running = true;

    static void Main()
    {
        var state = new AppState();

        DirectoryManager.LoadCurentDir(state);
        state.SelectedIndex = 0;

        while (running)
        {
            DirectoryManager.LoadCurentDir(state);

            Render.Draw(state);
            

            ActionType action = InputHandler.Read();

            if (action == ActionType.None)
                continue;

            if (action == ActionType.Quit)
            {
                running = false;
                continue;
            }       
                

            StateManager.Update(state, action);
        }
    }
}



