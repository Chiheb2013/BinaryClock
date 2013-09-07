using System.Drawing;

namespace BinaryClock
{
    class BinaryGrid
    {
        const int BULB_DIAMETER = BinaryRange.BULB_DIAMETER;

        BinaryRange hours;
        BinaryRange minutes;
        BinaryRange seconds;

        public BinaryGrid(int width, int height)
        {
            Point hPos = new Point(width / 2 - 4 * BULB_DIAMETER, height / 2 - 2 * BULB_DIAMETER);
            Point mPos = new Point(width / 2, height / 2 - 2 * BULB_DIAMETER);
            Point sPos = new Point(width / 2 + 4 * BULB_DIAMETER, height / 2 - 2 * BULB_DIAMETER);
            
            hours = new BinaryRange(RangeType.Hour, hPos);
            minutes = new BinaryRange(RangeType.Minute, mPos);
            seconds = new BinaryRange(RangeType.Second, sPos);
        }

        public void Update(int h, int m, int s)
        {
            string sHours = h.ToString();
            string sMinutes = m.ToString();
            string sSeconds = s.ToString();
            sHours = sHours.Length == 1 ? "0" + sHours : sHours;
            sMinutes = sMinutes.Length == 1 ? "0" + sMinutes : sMinutes;
            sSeconds = sSeconds.Length == 1 ? "0" + sSeconds : sSeconds;

            hours.Update(sHours[0], sHours[1]);
            minutes.Update(sMinutes[0], sMinutes[1]);
            seconds.Update(sSeconds[0], sSeconds[1]);
        }

        public void Render()
        {
            hours.Render();
            minutes.Render();
            seconds.Render();
        }
    }
}
