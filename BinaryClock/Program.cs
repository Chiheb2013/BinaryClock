using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryClock
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 2)
                StartPreviewMode(args);
            else if (args.Length == 5)
                StartAsDesktopGadget(args);
            else
                StartFullscreenMode(args);
        }

        private static void StartPreviewMode(string[] args)
        {
            string first = args[0];
            string second = args[1];

            if (first == "/p" && second != string.Empty)
            {
                IntPtr previewWinHandle = new IntPtr(long.Parse(second));
                Application.Run(new MainForm(previewWinHandle));
            }
            else
                MessageBox.Show("Or preview was not provided, or the handle was not given.",
                    "BinaryClockSS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void StartAsDesktopGadget(string[] args)
        {
            try
            {
                if (args[0] == "/g")
                {
                    int locX = int.Parse(args[1]);
                    int locY = int.Parse(args[2]);
                    int width = int.Parse(args[3]);
                    int height = int.Parse(args[4]);

                    MainForm form = new MainForm();
                    form.Size = new System.Drawing.Size(width, height);
                    form.Location = new System.Drawing.Point(locX, locY);

                    Application.Run(form);
                }
                else
                    MessageBox.Show("You have asked for '/g', but unfortunatly, something got wrong somewhere.\n" +
                    "Whether it is that you didn't pass integers are parameters in the order :\n" +
                    "\tbynaryclock.exe /g [location x] [location y] [width] [height]\n" +
                    "Check that these arguments were provided to the BinaryClock.", "BinaryClock Gadget",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("You have asked for '/g', but unfortunatly, something got wrong somewhere.\n" +
                    "Whether it is that you didn't pass integers are parameters in the order :\n" +
                    "\tbynaryclock.exe /g [location x] [location y] [width] [height]\n" +
                    "Check that these arguments were provided to the BinaryClock.", "BinaryClock Gadget",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void StartFullscreenMode(string[] args)
        {
            if (args[0] == "/s")
                Application.Run(new MainForm());
            else
                MessageBox.Show("You may asked for '/c', but this SS doesn't provide any.\n"+
                                "If you didn't ask for '/c', then consider this some un-understandable error PC's always give.",
                    "BinaryClockSS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
