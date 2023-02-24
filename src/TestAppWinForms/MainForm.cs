using System.Diagnostics;
using ExtendedProcess;
using Cursor = System.Windows.Forms.Cursor;

namespace TestAppWinForms;

public partial class MainForm : Form
{
    public MainForm() => InitializeComponent();

    private void cbFollowMouse_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox senderCb = (CheckBox)sender;
        if (senderCb.CheckState == CheckState.Checked)
        {
            timerFollow.Start();
        }
        else
        {
            timerFollow.Stop();
        }
    }

    private void timerFollow_Tick(object sender, EventArgs e)
    {
        ProcessWindow? window = ExtendedProcess.Cursor.GetWindowAtPoint(Cursor.Position);

        tbWindowInfo.Text = string.Join(Environment.NewLine,
            $"{(window != null ? window.Title : "null")}",
            $"exe: {(window != null ? $"{Process.GetProcessById(window.ProcessId).ProcessName}.exe" : "null")}",
            $"pid: {(window != null ? window.ProcessId : "null")}",
            $"id {(window != null ? window.Handle : "null")}");
        tbMousePos.Text = string.Join(Environment.NewLine,
            $"Screen: {Cursor.Position.X}," +
            $"{Cursor.Position.Y}");
        tbWindowPos.Text = string.Join(Environment.NewLine,
            string.Join("\t",
                "Screen:",
                $"x: {(window != null ? window.WindowScreen.Position.X : "null")}",
                $"y: {(window != null ? window.WindowScreen.Position.Y : "null")}",
                $"w: {(window != null ? window.WindowScreen.Width : "null")}",
                $"h: {(window != null ? window.WindowScreen.Height : "null")}"),
            string.Join("\t",
                "Client:",
                $"x: {(window != null ? window.WindowClient.Position.X : "null")}",
                $"y: {(window != null ? window.WindowClient.Position.Y : "null")}",
                $"w: {(window != null ? window.WindowClient.right : "null")}",
                $"h: {(window != null ? window.WindowClient.bottom : "null")}")
        );
    }
}
