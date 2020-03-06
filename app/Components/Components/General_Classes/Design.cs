using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    static class Design
    {
        private static Timer timer = new Timer();
        private static PictureBox pictureBox = new PictureBox();
        private static Graphics graphics = WorkWithChain.MainForm.CreateGraphics();
        private static Point plus, minus;
        private static Point[] points = new Point[4];

        private static Pen pen = new Pen(Color.Gray, 3);

        /// <summary>
        /// Метод запуска анимации в PictureBox
        /// </summary>
        /// <param name="picture">PictureBox для анимации</param>
        /// <param name="time">Длительность анимации в миллисекундах</param>
        public static void Animate(PictureBox picture, int time)
        {
            pictureBox = picture;
            pictureBox.Enabled = true;

            timer.Enabled = false;
            timer.Tick += Timer_Tick;
            timer.Interval = time;
            timer.Enabled = true;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            pictureBox.Enabled = false;

            timer.Dispose();
        }

        public static void ConnectionElements(Device deviceSource, Device deviceReceiver) 
        {
            DrawWire(deviceSource.GetPointPlus(), deviceReceiver.GetPointMinus());
        }

        public static void SetPointPlus(int x, int y) 
        {
            plus.X = x;
            plus.Y = y;
        }
        public static void SetPointMinus(int x, int y)
        {
            minus.X = x;
            minus.Y = y;
        }

        private static void DrawWire(Point plus, Point minus) 
        {
            if (plus.Y > minus.Y) 
            {
                points[0] = new Point(plus.X, plus.Y);
                points[1] = new Point(plus.X, plus.Y + 10); 
                points[2] = new Point(minus.X, plus.Y + 10);
                points[3] = new Point(minus.X, minus.Y);
            }
            else 
            {
                points[0] = new Point(plus.X, plus.Y);
                points[1] = new Point(plus.X, minus.Y + 10);
                points[2] = new Point(minus.X, minus.Y + 10); 
                points[3] = new Point(minus.X, minus.Y);
            }

            graphics.DrawLines(pen,points);
            //graphics.Dispose();
        }
    }
}
