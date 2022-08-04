using System.Drawing;
using System.Runtime.InteropServices;

namespace ExtendedProcess
{
    public static class Cursor
    {
        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
        //Equivalent to System.Windows.Forms.Cursor.Position
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point Point); //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-windowfrompoint

        public static Point GetCursorPosScreen()
        {
            Point cursorPos;
            GetCursorPos(out cursorPos);

            return cursorPos;
        }

        public static ProcessWindow GetWindowAtPoint(Point position)
        {
            var windowHandle = WindowFromPoint(position);
            foreach (var iterWindow in ProcessWindow.GetWindows())
                if (windowHandle == iterWindow.Handle)
                    return iterWindow;

            return null;
        }
    }
}
