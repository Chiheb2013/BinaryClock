using System.Drawing;
using System.Drawing.Drawing2D;

namespace BinaryClock
{
    static class DesktopDrawer
    {
        static Bitmap bmp;
        static Graphics graphics;

        public static Bitmap Image { get { return bmp; } }
        public static Graphics Graphics { get { return graphics; } }

        public static void CreateGraphics(Bitmap bmp)
        {
            DesktopDrawer.bmp = bmp;

            graphics = Graphics.FromImage(bmp);
            
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
    }
}
