using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    class HeatingArea : IDisposable
    {
        private Thermometr thermometr;
        private int margins = 30;

        public HeatingArea() 
        {
        }
        ~HeatingArea()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
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

            thermometr = new Thermometr(
                form, 
                resistor[1].GetX() + resistor[1].GetWidth() + 10,
                resistor[0].GetY() - 75);

            graphics.Dispose();
        }

    }
}
