using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class HeatingArea
    {
        private Thermometr thermometr;
        private int margins = 30;

        public HeatingArea() 
        {
        }

        public void DrawHeatingArea(Form form, Resistor[] resistor)
        {
            Graphics graphics = form.CreateGraphics();

            Pen pen = new Pen(Brushes.Orange, 3);
            pen.DashStyle = DashStyle.Dash;

            Point[] points = new Point[5]
            {
                new Point(resistor[0].GetX() - margins,resistor[0].GetY() - margins),
                new Point(resistor[1].GetX() + resistor[1].GetWidth() + margins,resistor[0].GetY() - margins),
                new Point(resistor[1].GetX() + resistor[1].GetWidth() + margins,resistor[1].GetY() + resistor[1].GetHeight() + margins),
                new Point(resistor[0].GetX() - margins,resistor[1].GetY() + resistor[1].GetHeight() + margins),
                new Point(resistor[0].GetX() - margins,resistor[0].GetY() - margins)
            };

            graphics.DrawLines(pen, points);
            graphics.Dispose();

            thermometr = new Thermometr(
                form, 
                resistor[1].GetX() + resistor[1].GetWidth() + 10,
                resistor[0].GetY() - 75);

            graphics.Dispose();
        }

    }
}
