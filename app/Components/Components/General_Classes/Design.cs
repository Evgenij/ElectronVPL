using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    class Design
    {
        private static Timer timer = new Timer();
        private static PictureBox pictureBox = new PictureBox();
        public static PictureBox picture = new PictureBox();

        private static FlowLayoutPanel flowPanel;
        private static TableLayoutPanel[] blocks;
        private static PictureBox[] pictureBlock;     
        private static Label[] title;

        private static Elements.Type type;

        private static Graphics graphics = GlobalData.MainForm.CreateGraphics();

        private static Point[] points = new Point[3];

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
            if (plus.X < minus.X)
            {
                // минусовой контакт справа вверху
                if (plus.Y > minus.Y)
                {
                    points[0] = new Point(plus.X - 1, plus.Y + 1);
                    points[1] = new Point(minus.X, plus.Y + 1);
                    points[2] = new Point(minus.X, minus.Y);
                }
                //минусовой контакт справа внизу
                else if (plus.Y < minus.Y)
                {
                    points[0] = new Point(plus.X, plus.Y);
                    points[1] = new Point(plus.X, minus.Y + 1);
                    points[2] = new Point(minus.X + 2, minus.Y + 1);
                }
            }
            else if (plus.X > minus.X)
            {
                // минусовой контакт слева вверху
                if (plus.Y > minus.Y)
                {
                    points[0] = new Point(plus.X + 2, plus.Y + 1);
                    points[1] = new Point(minus.X, plus.Y + 1);
                    points[2] = new Point(minus.X, minus.Y);
                }
                //минусовой контакт слева внизу
                else if (plus.Y < minus.Y)
                {
                    points[0] = new Point(plus.X, plus.Y);
                    points[1] = new Point(plus.X, minus.Y + 1);
                    points[2] = new Point(minus.X - 1, minus.Y + 1);
                }
            }
            graphics.DrawLines(pen,points);
        }

        public static void CreatePanel(PictureBox picturePanel, Elements.Type[] types) 
        {
            flowPanel = new FlowLayoutPanel();
            flowPanel.Left = 9;
            flowPanel.Top = 43;
            flowPanel.Width = 300;
            flowPanel.Height = 433;
            flowPanel.BackColor = Color.Transparent;
            flowPanel.AutoScroll = true;

            blocks = new TableLayoutPanel[types.Length];
            pictureBlock = new PictureBox[types.Length];
            title = new Label[types.Length];

            for (int i = 0; i < types.Length; i++) 
            {
                blocks[i] = new TableLayoutPanel();
                blocks[i].RowCount = 2;
                blocks[i].Width = 135;
                blocks[i].Height = 104;
                blocks[i].BackColor = Color.Transparent;
                blocks[i].Margin = new Padding(1,3,1,3);
                blocks[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                blocks[i].RowStyles.Add(new RowStyle());
                blocks[i].RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));

                pictureBlock[i] = new PictureBox();
                pictureBlock[i].SizeMode = PictureBoxSizeMode.Zoom;
                pictureBlock[i].Dock = DockStyle.Fill;
                pictureBlock[i].Click += Click;
                pictureBlock[i].MouseEnter += MouseEnter;
                pictureBlock[i].MouseLeave += MouseLeave;

                title[i] = new Label();
                title[i].Dock = DockStyle.Fill;
                title[i].Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                title[i].TextAlign = ContentAlignment.MiddleCenter;
                title[i].ForeColor = Color.FromArgb(64, 64, 64);

                if (types[i] == Elements.Type.Ammeter)
                {
                    pictureBlock[i].Name = "ammeter";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\ammeter\ammeter.png");
                    title[i].Text = "Амперметр";
                }
                else if (types[i] == Elements.Type.Voltmeter)
                {
                    pictureBlock[i].Name = "voltmeter";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltmeter\voltmeter.png");
                    title[i].Text = "Вольтметр";
                }
                else if (types[i] == Elements.Type.Multimeter)
                {
                    pictureBlock[i].Name = "multimeter";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\multimetr.png");
                    title[i].Text = "Мультиметр";
                }
                else if (types[i] == Elements.Type.Conductor)
                {
                    pictureBlock[i].Name = "conductor";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\conductor\conductor.png");
                    title[i].Text = "Проводник";
                }
                else if (types[i] == Elements.Type.Resistor)
                {
                    pictureBlock[i].Name = "resistor";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\resistor\resistor.png");
                    title[i].Text = "Резистор";
                }
                else if (types[i] == Elements.Type.Rheostat)
                {
                    pictureBlock[i].Name = "rheostat";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\rheostat\rheostat.png");
                    title[i].Text = "Реостат";
                }
                else if (types[i] == Elements.Type.VoltageSource)
                {
                    pictureBlock[i].Name = "voltage_source";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltage_source\voltage.png");
                    title[i].Text = "Ист.напряжения";
                }
                else if (types[i] == Elements.Type.Capacitor)
                {
                    pictureBlock[i].Name = "capacitor";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\capacitor.png");
                    title[i].Text = "Конденсатор";
                }
                else if (types[i] == Elements.Type.SingleSwitch)
                {
                    pictureBlock[i].Name = "single_switch";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\s_switch0.png");
                    title[i].Text = "Перекл. х1";
                }
                else if (types[i] == Elements.Type.DoubleSwitch)
                {
                    pictureBlock[i].Name = "double_switch";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\d_switch.png");
                    title[i].Text = "Перекл. х2";
                }
                else if (types[i] == Elements.Type.Toggle)
                {
                    pictureBlock[i].Name = "toggle";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\toggle\toggle_off.png");
                    title[i].Text = "Ключ";
                }
                else if (types[i] == Elements.Type.Lamp)
                {
                    pictureBlock[i].Name = "lamp";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\lamp\lamp_off.png");
                    title[i].Text = "Лампочка";
                }
                else if (types[i] == Elements.Type.Stopwatch)
                {
                    pictureBlock[i].Name = "stopwatch";
                    pictureBlock[i].Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\stopwatch\stopwatch.png");
                    title[i].Text = "Секундомер";
                }

                blocks[i].Controls.Add(pictureBlock[i]);
                blocks[i].Controls.Add(title[i]);
                flowPanel.Controls.Add(blocks[i]);
            }

            picturePanel.Controls.Add(flowPanel);
        }

        private static void Click(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "ammeter")
            {
                type = Elements.Type.Ammeter;
            }
            else if (((Control)sender).Name == "voltmeter") 
            {
                type = Elements.Type.Voltmeter;
            }
            else if (((Control)sender).Name == "multimeter")
            {
                type = Elements.Type.Multimeter;
            }
            else if (((Control)sender).Name == "conductor")
            {
                type = Elements.Type.Conductor;
            }
            else if (((Control)sender).Name == "resistor")
            {
                type = Elements.Type.Resistor;
            }
            else if (((Control)sender).Name == "rheostat")
            {
                type = Elements.Type.Rheostat;
            }
            else if (((Control)sender).Name == "voltage_source")
            {
                type = Elements.Type.VoltageSource;
            }
            else if (((Control)sender).Name == "capacitor")
            {
                type = Elements.Type.Capacitor;
            }
            else if (((Control)sender).Name == "single_switch")
            {
                type = Elements.Type.SingleSwitch;
            }
            else if (((Control)sender).Name == "double_switch")
            {
                type = Elements.Type.DoubleSwitch;
            }
            else if (((Control)sender).Name == "toggle")
            {
                type = Elements.Type.Toggle;
            }
            else if (((Control)sender).Name == "lamp")
            {
                type = Elements.Type.Lamp;
            }
            else if (((Control)sender).Name == "stopwatch")
            {
                type = Elements.Type.Stopwatch;
            }


            ShowPicture();
        }

        private static void MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).Cursor = Cursors.Default;
            ((Control)sender).BackColor = Color.FromArgb(237, 249, 255);
        }

        private static void MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).Cursor = Cursors.Hand;
            ((Control)sender).BackColor = Color.FromArgb(211,240,255);
        }

        public static new Elements.Type GetType() 
        {
            return type;
        }

        #region Visualization adding elements
        public static void SeleсtingPlace(int x, int y)
        {
            if (picture.Visible == true)
            {
                if (type == Elements.Type.Ammeter)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\ammeter.png");
                }
                else if (type == Elements.Type.Voltmeter)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\voltmeter.png");
                }
                else if (type == Elements.Type.Multimeter)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\multimeter.png");
                }
                else if (type == Elements.Type.Conductor)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\cond.png");
                }
                else if (type == Elements.Type.Resistor)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\resistor.png");
                }
                else if (type == Elements.Type.Rheostat)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\rheostat.png");
                }
                else if (type == Elements.Type.VoltageSource)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\volt_source.png");
                }
                else if (type == Elements.Type.Capacitor)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\capacitor.png");
                }
                else if (type == Elements.Type.SingleSwitch)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\s_switch.png");
                }
                else if (type == Elements.Type.DoubleSwitch)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\d_switch.png");
                }
                else if (type == Elements.Type.Toggle)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\toggle.png");
                }
                else if (type == Elements.Type.Lamp)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\lamp.png");
                }
                else if (type == Elements.Type.Stopwatch)
                {
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\stopwatch.png");
                }

                picture.Left = x - picture.Width / 2;
                picture.Top = y - picture.Height - 10;
            }
        }

        public static void ShowPicture()
        {
            picture.SizeMode = PictureBoxSizeMode.AutoSize;
            GlobalData.MainForm.Controls.Add(picture);
            picture.Visible = true;
        }

        public static void HidePicture()
        {
            picture.Visible = false;
        } 
        #endregion
    }
}
