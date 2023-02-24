using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ExtendedProcess;

public class Win32
{
    /// <summary>
    ///     Retrieves information about the specified window.
    /// </summary>
    /// <param name="hwnd">
    ///     A handle to the window whose information is to be retrieved.
    /// </param>
    /// <param name="pwi">
    ///     A pointer to a <see cref="WINDOWINFO" /> structure to receive the information. Note that you must set the cbSize
    ///     member to sizeof(<see cref="WINDOWINFO" />) before calling this function.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is nonzero.
    ///     </para>
    ///     <para>
    ///         If the function fails, the return value is zero.
    ///     </para>
    ///     <para>
    ///         To get extended error information, call GetLastError.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool GetWindowInfo(IntPtr hwnd, WINDOWINFO pwi);

    /// <summary>
    ///     Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a
    ///     control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another
    ///     application.
    /// </summary>
    /// <param name="hWnd">
    ///     A handle to the window or control containing the text.
    /// </param>
    /// <param name="lpString">
    ///     The buffer that will receive the text. If the string is as long or longer than the buffer, the
    ///     string is truncated and terminated with a null character.
    /// </param>
    /// <param name="nMaxCount">
    ///     The maximum number of characters to copy to the buffer, including the null character. If the
    ///     text exceeds this limit, it is truncated.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is the length, in characters, of the copied string, not including
    ///         the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the
    ///         window or control handle is invalid, the return value is zero. To get extended error information, call
    ///         GetLastError.
    ///     </para>
    ///     <para>
    ///         This function cannot retrieve the text of an edit control in another application.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern int
        GetWindowText(IntPtr hWnd, StringBuilder lpString,
            int nMaxCount); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtexta / https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw

    /// <summary>
    ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the
    ///     process that created the window.
    /// </summary>
    /// <param name="hWnd">
    ///     A handle to the window.
    /// </param>
    /// <param name="lpdwProcessId">
    ///     A pointer to a variable that receives the process identifier. If this parameter is not
    ///     NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.
    /// </param>
    /// <returns>
    ///     The return value is the identifier of the thread that created the window.
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern uint
        GetWindowThreadProcessId(IntPtr hWnd,
            out uint lpdwProcessId); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid

    /// <summary>
    ///     Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). If
    ///     the specified window is a control, the function retrieves the length of the text within the control. However,
    ///     GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
    /// </summary>
    /// <param name="hWnd">
    ///     A handle to the window or control.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is the length, in characters, of the text. Under certain conditions,
    ///         this value might be greater than the length of the text (see Remarks).
    ///     </para>
    ///     <para>
    ///         If the window has no text, the return value is zero.
    ///     </para>
    ///     <para>
    ///         Function failure is indicated by a return value of zero and a GetLastError result that is nonzero.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern int
        GetWindowTextLength(
            IntPtr hWnd); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha

    /// <summary>
    ///     Determines whether the specified window handle identifies an existing window.
    /// </summary>
    /// <param name="hWnd">
    ///     A handle to the window to be tested.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the window handle identifies an existing window, the return value is nonzero.
    ///     </para>
    ///     <para>
    ///         If the window handle does not identify an existing window, the return value is zero.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool
        IsWindow(IntPtr hWnd); // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindow

    /// <summary>
    ///     Determines the visibility state of the specified window.
    /// </summary>
    /// <param name="hWnd">
    ///     A handle to the window to be tested.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE
    ///         style, the return value is nonzero. Otherwise, the return value is zero.
    ///     </para>
    ///     <para>
    ///         Because the return value specifies whether the window has the WS_VISIBLE style, it may be nonzero even if the
    ///         window is totally obscured by other windows.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool
        IsWindowVisible(
            IntPtr hWnd); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible

    /// <summary>
    ///     Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an
    ///     application-defined callback function. EnumWindows continues until the last top-level window is enumerated or the
    ///     callback function returns FALSE.
    /// </summary>
    /// <param name="lpEnumFunc">
    ///     A pointer to an application-defined callback function. For more information, see
    ///     EnumWindowsProc.
    /// </param>
    /// <param name="lParam">
    ///     An application-defined value to be passed to the callback function.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is nonzero.
    ///     </para>
    ///     <para>
    ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
    ///     </para>
    ///     <para>
    ///         If EnumWindowsProc returns zero, the return value is also zero. In this case, the callback function should call
    ///         SetLastError to obtain a meaningful error code to be returned to the caller of EnumWindows.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool
        EnumWindows(EnumWindowsProc lpEnumFunc,
            int lParam); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumwindows

    /// <summary>
    ///     Retrieves a handle to the Shell's desktop window.
    /// </summary>
    /// <returns>
    ///     The return value is the handle of the Shell's desktop window. If no Shell process is present, the return value
    ///     is NULL.
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern IntPtr
        GetShellWindow(); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getshellwindow

    /// <summary>
    ///     Retrieves the current value of a specified Desktop Window Manager (DWM) attribute applied to a window. For
    ///     programming guidance, and code examples, see Controlling non-client region rendering.
    /// </summary>
    /// <param name="hwnd">The handle to the window from which the attribute value is to be retrieved.</param>
    /// <param name="dwAttribute">
    ///     A flag describing which value to retrieve, specified as a value of the <see cref="DWMWINDOWATTRIBUTE" />
    ///     enumeration. This parameter specifies which attribute to retrieve, and the pvAttribute parameter points to an
    ///     object into which the attribute value is retrieved.
    /// </param>
    /// <param name="pvAttribute">
    ///     A pointer to a value which, when this function returns successfully, receives the current
    ///     value of the attribute. The type of the retrieved value depends on the value of the dwAttribute parameter. The
    ///     <see cref="DWMWINDOWATTRIBUTE" /> enumeration topic indicates, in the row for each flag, what type of value you
    ///     should pass a
    ///     pointer to in the pvAttribute parameter.
    /// </param>
    /// <param name="cbAttribute">
    ///     The size, in bytes, of the attribute value being received via the pvAttribute parameter. The
    ///     type of the retrieved value, and therefore its size in bytes, depends on the value of the dwAttribute parameter.
    /// </param>
    /// <returns>If the function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
    [DllImport("dwmapi.dll")]
    internal static extern int
        DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, RECT pvAttribute,
            int cbAttribute); // https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute

    /// <summary>
    ///     Retrieves a handle to the foreground window (the window with which the user is currently working). The system
    ///     assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
    /// </summary>
    /// <returns>
    ///     The return value is a handle to the foreground window. The foreground window can be NULL in certain
    ///     circumstances, such as when a window is losing activation.
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern IntPtr
        GetForegroundWindow(); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow

    /// <summary>
    ///     Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen
    ///     coordinates that are relative to the upper-left corner of the screen.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="lpRect">
    ///     A pointer to a <see cref='RECT' /> structure that receives the screen coordinates of the
    ///     upper-left and lower-right corners of the window.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is nonzero.
    ///     </para>
    ///     <para>
    ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool
        GetWindowRect(IntPtr hWnd,
            out RECT lpRect); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect

    /// <summary>
    ///     Retrieves the coordinates of a window's client area. The client coordinates specify the upper-left and lower-right
    ///     corners of the client area. Because client coordinates are relative to the upper-left corner of a window's client
    ///     area, the coordinates of the upper-left corner are (0,0).
    /// </summary>
    /// <param name="hWnd">A handle to the window whose client coordinates are to be retrieved.</param>
    /// <param name="lpRect">
    ///     A pointer to a RECT structure that receives the client coordinates. The left and top members are
    ///     zero. The right and bottom members contain the width and height of the window.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is nonzero.
    ///     </para>
    ///     <para>
    ///         If the function fails, the return value is zero. To get extended error information, call GetLastError.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool
        GetClientRect(IntPtr hWnd,
            out RECT lpRect); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclientrect

    /// <summary>
    ///     The ClientToScreen function converts the client-area coordinates of a specified point to screen coordinates.
    /// </summary>
    /// <param name="hWnd">A handle to the window whose client area is used for the conversion.</param>
    /// <param name="lpPoint">
    ///     A pointer to a POINT structure that contains the client coordinates to be converted. The new
    ///     screen coordinates are copied into this structure if the function succeeds.
    /// </param>
    /// <returns>
    ///     <para>
    ///         If the function succeeds, the return value is nonzero.
    ///     </para>
    ///     <para>
    ///         If the function fails, the return value is zero.
    ///     </para>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool
        ClientToScreen(IntPtr hWnd,
            out Point lpPoint); // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-clienttoscreen

    /// <summary>
    ///     An application-defined callback function used with the <see cref="Win32.EnumWindows" /> or EnumDesktopWindows
    ///     function. It receives top-level window handles. The WNDENUMPROC type defines a pointer to this callback function.
    ///     EnumWindowsProc is a placeholder for the application-defined function name.
    /// </summary>
    /// <param name="hWnd">
    ///     A handle to a top-level window.
    /// </param>
    /// <param name="lParam">
    ///     The application-defined value given in EnumWindows or EnumDesktopWindows.
    /// </param>
    /// <returns>
    ///     To continue enumeration, the callback function must return TRUE; to stop enumeration, it must return FALSE.
    /// </returns>
    internal delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

    /// <summary>
    ///     Options used by the DwmGetWindowAttribute and DwmSetWindowAttribute functions.
    /// </summary>
    [Flags]
    internal enum DWMWINDOWATTRIBUTE :
        uint // https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
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

    /// <summary>
    ///     Contains window information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-windowinfo
    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(bool? filler) :
            this() // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            =>
                cbSize = (uint)Marshal.SizeOf(typeof(WINDOWINFO));
    }

    /// <summary>
    ///     The RECT structure defines a rectangle by the coordinates of its upper-left and lower-right corners.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT // https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect
    {
        /// <summary>
        ///     Specifies the x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int left;

        /// <summary>
        ///     Specifies the y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int top;

        /// <summary>
        ///     Specifies the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int right;

        /// <summary>
        ///     Specifies the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
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
            get => right - left;
            set => right = value + left;
        }

        public int Height
        {
            get => bottom - top;
            set => bottom = value + top;
        }

        /// <summary>
        ///     Returns the top-left coordinates.
        /// </summary>
        public Point Position => new(left, top);

        /// <summary>
        ///     Returns the size of the rectangle.
        /// </summary>
        public Size Size => new(Width, Height);

        //public Rectangle ToRectangle() => new(left, top, Width, Height);
    }
}
