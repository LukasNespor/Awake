using System;
using System.Drawing;
using System.Windows.Forms;

namespace Awake
{
    public partial class MainForm : Form
    {
        int inactiveSeconds = 0;
        int inactiveSecondsAll = 0;
        Point lastPosition = Cursor.Position;

        public MainForm()
        {
            InitializeComponent();
            Cursor = new Cursor(Cursor.Current.Handle);
        }

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

        void MainTimer_Tick(object sender, EventArgs e)
        {
            var currentPosition = Cursor.Position;

            if (lastPosition.X == currentPosition.X && lastPosition.Y == currentPosition.Y)
            {
                inactiveSecondsAll++;
                SetInactiveSecondsAndText(++inactiveSeconds);
            }
            else
            {
                lastPosition = currentPosition;
                inactiveSecondsAll = 0;
                SetInactiveSecondsAndText(0);
                return;
            }

            if (inactiveSeconds >= (5 * 60))
                MoveCursor();
        }

        void SetInactiveSecondsAndText(int value)
        {
            message.Text = $"Neaktivita: {inactiveSecondsAll / 60} min";
            inactiveSeconds = value;
        }

        void MoveCursor()
        {
            var currentPosition = Cursor.Position;
            Cursor.Position = new Point(currentPosition.X + 10, currentPosition.Y + 10);
            Cursor.Position = new Point(currentPosition.X, currentPosition.Y);
            inactiveSeconds = 0;
        }
    }
}
