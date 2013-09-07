using System.Drawing;
using System.Collections.Generic;

namespace BinaryClock
{
    class BinaryRange
    {
        public const int BULB_DIAMETER = 40;

        char left;
        char right;

        bool[] rightArray;
        bool[] leftArray;

        Point position;

        static Dictionary<char, bool[]> digitCouples = new Dictionary<char, bool[]>();

        public static void PopulateCouples()
        {
            digitCouples.Add('0', new bool[] { false, false, false, false });
            digitCouples.Add('1', new bool[] { false, false, false, true });
            digitCouples.Add('2', new bool[] { false, false, true, false });
            digitCouples.Add('3', new bool[] { false, false, true, true });
            digitCouples.Add('4', new bool[] { false, true, false, false });
            digitCouples.Add('5', new bool[] { false, true, false, true });
            digitCouples.Add('6', new bool[] { false, true, true, false });
            digitCouples.Add('7', new bool[] { false, true, true, true });
            digitCouples.Add('8', new bool[] { true, false, false, false });
            digitCouples.Add('9', new bool[] { true, false, false, true });
        }

        public BinaryRange(RangeType type, Point position)
        {
            this.position = position;
        }

        public void Update(char left, char right)
        {
            this.left = left;
            this.right = right;

            ComputeActiveBulbs();
        }

        public void Render()
        {
            DrawLeft();
            DrawRight();
        }

        private void DrawLeft()
        {
            Point origin = new Point(position.X, position.Y);
            DrawColumn(origin, leftArray);   
        }

        private void DrawRight()
        {
            Point origin = new Point(position.X + BULB_DIAMETER, position.Y);
            DrawColumn(origin, rightArray);
        }

        private void DrawColumn(Point origin, bool[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Point relative = new Point(origin.X, origin.Y  + i * BULB_DIAMETER);

                if (array[i])
                    DrawCircle(relative, Color.Red);
                else
                    DrawCircle(relative, Color.Blue);
            }
        }

        private void DrawCircle(Point pos, Color color)
        {
            Rectangle rect = new Rectangle(pos, new Size(BULB_DIAMETER, BULB_DIAMETER));
            SolidBrush brush = new SolidBrush(color);

            DesktopDrawer.Graphics.FillEllipse(brush, rect);
        }

        private void ComputeActiveBulbs()
        {
            leftArray = digitCouples[left];
            rightArray = digitCouples[right];
        }
    }
}
