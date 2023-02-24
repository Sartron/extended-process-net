using System.Diagnostics;

namespace ExtendedProcess;

public static class ProcessExtensions
{
    public static List<ProcessWindow> GetWindows(this Process process,
        ProcessWindow.WindowState state = ProcessWindow.WindowState.All) => ProcessWindow.GetWindows(process.Id, state);
}
