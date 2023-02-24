using System.Drawing;
using System.Runtime.InteropServices;

namespace ExtendedProcess;

public static class Cursor
{
    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
    // Equivalent to System.Windows.Forms.Cursor.Position
    /// <summary>
    ///     Retrieves the position of the mouse cursor, in screen coordinates.
    /// </summary>
    /// <param name="lpPoint">
    ///     A pointer to a <see cref="Point" /> structure that receives the screen coordinates of the cursor.
    /// </param>
    /// <returns>
    ///     Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.
    /// </returns>
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);

    /// <summary>
    ///     Retrieves a handle to the window that contains the specified point.
    /// </summary>
    /// <param name="Point">
    ///     The point to be checked.
    /// </param>
    /// <returns>
    ///     The return value is a handle to the window that contains the point. If no window exists at the given point,
    ///     the return value is NULL. If the point is over a static text control, the return value is a handle to the window
    ///     under the static text control.
    /// </returns>
    [DllImport("user32.dll")]
    private static extern IntPtr
        WindowFromPoint(
            Point Point); //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-windowfrompoint

    /// <summary>
    ///     Returns the screen coordinates of the cursor.
    /// </summary>
    public static Point GetCursorPosScreen()
    {
        GetCursorPos(out Point cursorPos);

        return cursorPos;
    }

    /// <summary>
    ///     Returns the window at the given screen coordinates.
    /// </summary>
    /// <param name="position">
    /// Screen coordinates.
    /// </param>
    public static ProcessWindow? GetWindowAtPoint(Point position)
    {
        IntPtr windowHandle = WindowFromPoint(position);
        foreach (ProcessWindow iterWindow in ProcessWindow.GetWindows())
        {
            if (windowHandle == iterWindow.Handle)
            {
                return iterWindow;
            }
        }

        return null;
    }
}
