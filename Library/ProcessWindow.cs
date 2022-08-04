using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ExtendedProcess
{
    public class ProcessWindow
    {
        public int ProcessId;
        public IntPtr Handle;
        public string Title;
        public RECT WindowScreen;
        public RECT WindowClient;
        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtexta
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, int lParam); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumwindows
        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow(); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getshellwindow
        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, RECT pvAttribute, int cbAttribute); // https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow(); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);  // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);  // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclientrect
        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, out Point lpPoint);  // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-clienttoscreen

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT // https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public int Width
            {
                get { return right - left; }
                set { right = value + left; }
            }

            public int Height
            {
                get { return bottom - top; }
                set { bottom = value + top; }
            }

            public Point Position
            {
                get { return new Point(left, top); }
            }

            public Size Size
            {
                get { return new Size(Width, Height); }
            }
        }

        [Flags]
        private enum DWMWINDOWATTRIBUTE : uint // https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_PASSIVE_UPDATE_MODE,
            DWMWA_USE_HOSTBACKDROPBRUSH,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR,
            DWMWA_CAPTION_COLOR,
            DWMWA_TEXT_COLOR,
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
            DWMWA_SYSTEMBACKDROP_TYPE,
            DWMWA_LAST
        }

        public ProcessWindow(IntPtr handle, string title, int pid)
        {
            Handle = handle;
            Title = title;
            ProcessId = pid;

            // Get window size.
            GetWindowScreen();
            GetWindowClient();
        }

        public static List<ProcessWindow> GetWindows()
        {
            var windowList = new List<ProcessWindow>();

            EnumWindows(delegate (IntPtr enumHwnd, int enumlParam)
            {
                var length = GetWindowTextLength(enumHwnd);

                // Filter out windows that are not owned by the passed PID.
                uint enumPid;
                GetWindowThreadProcessId(enumHwnd, out enumPid);

                var builder = new StringBuilder(length);
                GetWindowText(enumHwnd, builder, length + 1);

                windowList.Add(new ProcessWindow(enumHwnd, builder.ToString(), (int)enumPid));
                return true;

            }, 0);

            return windowList;
        }

        public static List<ProcessWindow> GetOpenWindows(uint pid)
        {
            // https://www.tcx.be/blog/2006/list-open-windows/

            var shellWindow = GetShellWindow();
            var windowList = new List<ProcessWindow>();

            EnumWindows(delegate (IntPtr enumHwnd, int enumlParam)
            {
                var length = GetWindowTextLength(enumHwnd);

                // Filter out windows that are:
                // * the root shell
                // * not visible
                // * no text
                if ((enumHwnd == shellWindow)
                    || (!IsWindowVisible(enumHwnd))
                    || (length == 0))
                        return true;

                // Filter out windows that are not owned by the passed PID.
                uint enumPid;
                GetWindowThreadProcessId(enumHwnd, out enumPid);
                if (pid != enumPid) return true;

                var builder = new StringBuilder(length);
                GetWindowText(enumHwnd, builder, length + 1);

                windowList.Add(new ProcessWindow(enumHwnd, builder.ToString(), (int)pid));
                return true;

            }, 0);

            return windowList;
        }

        public bool IsValid()
        {
            var handleExists = false;

            EnumWindows(delegate (IntPtr enumHwnd, int enumlParam)
            {
                // Filter out all windows aside from the handle for this object.
                if (enumHwnd != Handle) return true;

                handleExists = true;
                return true;
            }, 0);

            return handleExists;
        }

        public void GetWindowClient()
        {
            RECT rect;
            Point point;
            GetClientRect(Handle, out rect);
            ClientToScreen(Handle, out point);

            WindowClient = rect;
            WindowClient.left = point.X;
            WindowClient.top = point.Y;
        }

        public void GetWindowScreen()
        {
            RECT rect;
            GetWindowRect(Handle, out rect);
            WindowScreen = rect;
        }

        public bool IsFocused()
        {
            return Handle == GetForegroundWindow();
        }

        public override string ToString()
        {
            return $"{Title} ({ProcessId})";
        }
    }
}
