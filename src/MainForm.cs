using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Awake
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        public MainForm() => InitializeComponent();
        void MainForm_Load(object sender, EventArgs e) => SetAwake(true);
        void MainForm_FormClosing(object sender, FormClosingEventArgs e) => SetAwake(false);
        void MainTimer_Tick(object sender, EventArgs e) => SetAwake(true);

        void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        void NotifyIcon_Click(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        void SetAwake(bool keepAwake)
        {
            if (keepAwake)
                SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
}
