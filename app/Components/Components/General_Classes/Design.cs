using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    static class Design
    {
        private static Timer timer = new Timer();
        private static PictureBox pictureBox = new PictureBox();

        private static Graphics graphics = GlobalData.MainForm.CreateGraphics();

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
            timer.Dispose();
            pictureBox.Enabled = false;
        }

        public static void ConnectionElements(Device deviceSource, Device deviceReceiver) 
        {
            if (deviceReceiver is SingleSwitch)
            {
                if (deviceReceiver.contactsSingleSwitch[0] == true)
                {
                    DrawWire(deviceSource.GetPointPlus(), deviceReceiver.GetPointLeft());
                }
            }
            else if (deviceReceiver is DoubleSwitch) 
            {
                if (deviceReceiver.contactsDoubleSwitch[0, 1] == true)
                {
                    DrawWire(deviceSource.GetPointPlus(), deviceReceiver.GetPointLeftMinus());
                }
                else if (deviceReceiver.contactsDoubleSwitch[1, 1] == true) 
                {
                    DrawWire(deviceSource.GetPointPlus(), deviceReceiver.GetPointRightMinus());
                }
                else if (deviceReceiver.contactsDoubleSwitch[2, 1] == true)
                {
                    DrawWire(deviceSource.GetPointPlus(), deviceReceiver.GetPointBottomMinus());
                }
            }
            else
            {
                if (deviceSource is SingleSwitch)
                {
                    if (deviceSource.contactsSingleSwitch[1] == true)
                    {
                        DrawWire(deviceSource.GetPointTop(), deviceReceiver.GetPointMinus());
                    }
                    else if (deviceSource.contactsSingleSwitch[2] == true)
                    {
                        DrawWire(deviceSource.GetPointBottom(), deviceReceiver.GetPointMinus());
                    }
                }
                else if (deviceSource is DoubleSwitch)
                {
                    if (deviceSource.contactsDoubleSwitch[0, 0] == true)
                    {
                        DrawWire(deviceSource.GetPointLeftPlus(), deviceReceiver.GetPointMinus());
                    }
                    else if (deviceSource.contactsDoubleSwitch[1, 0] == true)
                    {
                        DrawWire(deviceSource.GetPointRightPlus(), deviceReceiver.GetPointMinus());
                    }
                    else if (deviceSource.contactsDoubleSwitch[2, 0] == true)
                    {
                        DrawWire(deviceSource.GetPointBottomPlus(), deviceReceiver.GetPointMinus());
                    }
                }
                else
                {
                    DrawWire(deviceSource.GetPointPlus(), deviceReceiver.GetPointMinus());
                }
            }
        }

        private static void DrawWire(Point plus, Point minus) 
        {

            if (plus.Y > minus.Y) 
            {
                points[0] = new Point(plus.X, plus.Y);
                points[1] = new Point(plus.X, plus.Y + 2); 
                points[2] = new Point(minus.X, plus.Y + 2);
                points[3] = new Point(minus.X, minus.Y);
            }
            else 
            {
                points[0] = new Point(plus.X, plus.Y);
                points[1] = new Point(plus.X, minus.Y + 2);
                points[2] = new Point(minus.X, minus.Y + 2); 
                points[3] = new Point(minus.X, minus.Y);
            }

            graphics.DrawLines(pen,points);
        }
    }
}
