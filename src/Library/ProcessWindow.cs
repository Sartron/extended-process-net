using System.Drawing;
using System.Text;

namespace ExtendedProcess;

/// <summary>
///     Object representing a window for a process.
/// </summary>
public class ProcessWindow
{
    public enum WindowState
    {
        /// <summary>
        ///     All windows.
        /// </summary>
        All,

        /// <summary>
        ///     Open windows.
        /// </summary>
        Open
    }

    /// <summary>
    ///     Window handle.
    /// </summary>
    public IntPtr Handle;

    /// <summary>
    ///     Parent process ID.
    /// </summary>
    public int ProcessId;

    /// <summary>
    ///     Window title.
    /// </summary>
    public string Title;

    /// <summary>
    ///     Window <see cref="Win32.RECT" /> excluding borders.
    /// </summary>
    public Win32.RECT WindowClient;

    /// <summary>
    ///     Window <see cref="Win32.RECT" /> including borders.
    /// </summary>
    public Win32.RECT WindowScreen;

    /// <summary>
    ///     Initializes a new instance of the <see cref='ProcessWindow' /> class.
    /// </summary>
    /// <param name="handle">
    ///     Window handle.
    /// </param>
    /// <param name="title">
    ///     Window title.
    /// </param>
    /// <param name="pid">
    ///     Parent process ID.
    /// </param>
    public ProcessWindow(IntPtr handle, string title, int pid)
    {
        Handle = handle;
        Title = title;
        ProcessId = pid;

        // Get window size.
        GetWindowScreen();
        GetWindowClient();
    }

    /// <summary>
    ///     Returns a list of <see cref='ProcessWindow' /> for all processes.
    /// </summary>
    public static List<ProcessWindow> GetWindows(WindowState state = WindowState.All)
    {
        List<ProcessWindow> windows = new();

        _ = Win32.EnumWindows(delegate(IntPtr enumHwnd, int enumlParam)
        {
            int windowTextLength = Win32.GetWindowTextLength(enumHwnd);
            StringBuilder builder = new(windowTextLength);

            // Filter out windows that are:
            // * the root shell
            // * not visible
            // * no text
            if (state == WindowState.Open && (enumHwnd == Win32.GetShellWindow() || !Win32.IsWindowVisible(enumHwnd) ||
                                              windowTextLength == 0))
            {
                return true;
            }

            _ = Win32.GetWindowThreadProcessId(enumHwnd, out uint enumPid);
            _ = Win32.GetWindowText(enumHwnd, builder, windowTextLength + 1);

            windows.Add(new ProcessWindow(enumHwnd, builder.ToString(), (int)enumPid));
            return true;
        }, 0);

        return windows;
    }

    /// <summary>
    ///     Returns a list of <see cref='ProcessWindow' /> for the given process.
    /// </summary>
    /// <param name="pid">
    ///     Process ID.
    /// </param>
    public static List<ProcessWindow> GetWindows(int pid, WindowState state = WindowState.All)
    {
        List<ProcessWindow> windows = new();

        foreach (ProcessWindow window in GetWindows(state))
        {
            if (window.ProcessId == pid)
            {
                windows.Add(window);
            }
        }

        return windows;
    }

    /// <summary>
    ///     Checks to see if the window still exists.
    /// </summary>
    public bool IsValid() => Win32.IsWindow(Handle);

    /// <summary>
    ///     Gets the window boundaries excluding borders.
    /// </summary>
    public bool GetWindowClient()
    {
        if (!IsValid())
        {
            return false;
        }

        Win32.GetClientRect(Handle, out Win32.RECT rect);
        Win32.ClientToScreen(Handle, out Point point);

        WindowClient = rect;
        WindowClient.left = point.X;
        WindowClient.top = point.Y;

        return true;
    }

    /// <summary>
    ///     Gets the window boundaries including borders.
    /// </summary>
    public bool GetWindowScreen()
    {
        if (!IsValid())
        {
            return false;
        }

        Win32.GetWindowRect(Handle, out Win32.RECT rect);
        WindowScreen = rect;

        return true;
    }

    /// <summary>
    ///     Checks to see if the window is focused in the foreground.
    /// </summary>
    public bool IsFocused() => Handle == Win32.GetForegroundWindow();

    /// <summary>
    ///     Converts this <see cref="ProcessWindow" /> to a human-readable string.
    /// </summary>
    /// <returns>
    ///     A string that represents this <see cref="ProcessWindow" />.
    /// </returns>
    public override string ToString() => $"{Title} ({ProcessId})";
}
