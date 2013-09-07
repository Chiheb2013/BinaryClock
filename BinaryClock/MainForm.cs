using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BinaryClock
{
    public partial class MainForm : Form
    {
        Thread updateThread;

        BinaryClock clock;

        public MainForm()
        {
            InitializeComponent();
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
    }
}
