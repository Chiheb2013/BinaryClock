using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinaryClock
{
    class BinaryClock
    {
        BinaryGrid grid;

        public BinaryClock(int width, int height, PictureBox ozone)
        {
            BinaryRange.PopulateCouples();
            ozone.Parent.Invoke(new Action(() =>
                {
                    DesktopDrawer.CreateGraphics(new Bitmap(ozone.Width, ozone.Height));
                }
            ));

            grid = new BinaryGrid(width, height);
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            
            grid.Update(now.Hour, now.Minute, now.Second);
        }

        public void Render()
        {
            DesktopDrawer.Graphics.Clear(Color.White);
            grid.Render();
        }
    }
}
