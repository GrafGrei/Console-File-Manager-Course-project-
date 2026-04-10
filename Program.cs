using Terminal.Gui;

class Program
{
    static void Main()
    {
        Application.Init();

        var top = Application.Top;

        var window = new Window("Console File Manager")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        var list = new ListView(new string[]
        {
            "folder1/",
            "folder2/",
            "file1.txt",
            "file2.cs"
        })
        {
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        window.Add(list);
        top.Add(window);

        Application.Run();
        Application.Shutdown();
    }
}