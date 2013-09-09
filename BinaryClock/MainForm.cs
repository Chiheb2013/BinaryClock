using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BinaryClock
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr child, IntPtr parent);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr handle, int index, IntPtr newHandle);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr handle, int index);
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr handle, out Rectangle rect);
        bool previewMode;

        Thread updateThread;
        BinaryClock clock;

        public MainForm()
        {
            InitializeComponent();
            InitializeForm_Standard();
        }

        public MainForm(IntPtr parentHandle)
        {
            InitializeComponent();

            InitializeForm_Preview(parentHandle);
            InitializeForm_Standard();
        }

        private void InitializeForm_Preview(IntPtr parentHandle)
        {
            SetThisAsPreviewChild(parentHandle);
            PlaceThisInParent(parentHandle);
            previewMode = true;
        }

        private void PlaceThisInParent(IntPtr parentHandle)
        {
            Rectangle parentZone;
            GetClientRect(parentHandle, out parentZone);
            Size = parentZone.Size;
            Location = new Point(0, 0);
        }

        const int WINDOW_STYLE = -16;
        const long AS_CHILD = 0x40000000;

        private void SetThisAsPreviewChild(IntPtr parentHandle)
        {
            SetParent(this.Handle, parentHandle); //Sets the parent of this Form to 'parentHandle's owner.

            //The following tells this Form that it is now a child, and it has to be displayed and act like a child.
            //This means, that when the parent window will be closed, this will die too.
            SetWindowLong(this.Handle, WINDOW_STYLE, new IntPtr(GetWindowLong(this.Handle, WINDOW_STYLE) | AS_CHILD));
        }

        private void InitializeForm_Standard()
        {
            TransparencyKey = Color.White;
            updateThread = new Thread(new ThreadStart(UpdateClock));
        }

        private void UpdateClock()
        {
            clock = new BinaryClock(Width, Height, pb_Ozone);

            while (true)
            {
                DoClockUpdate();
                Render();

                Thread.Sleep(30);
            }
        }

        private void DoClockUpdate()
        {
            clock.Update();
        }

        private void Render()
        {
            clock.Render();

            Invoke(new Action(() =>
                {
                    pb_Ozone.Image = DesktopDrawer.Image;
                    pb_Ozone.Refresh();
                }
            ));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updateThread.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            updateThread.Abort();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }
    }
}
