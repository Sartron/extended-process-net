using System.Diagnostics;
using ExtendedProcess;

foreach(var process in Process.GetProcesses())
{
    foreach (var window in ProcessWindow.GetOpenWindows((uint)process.Id))
    {
        Console.WriteLine($"{window}");
        Console.WriteLine($"\t{window.WindowScreen.Size}, {window.WindowClient.Size}");
        Console.WriteLine($"\t{window.WindowScreen.Position}, {window.WindowClient.Position}");
    }
}
