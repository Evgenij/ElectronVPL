using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ElectronVPL
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
        private static Label[] nameValue;
        private static Label[] value;
        private static TextBox[] fields;

        private static Elements.Type type;
        public enum Values { Мощность, Работа_тока };
        private static Values values;

        private static Graphics graphics = GlobalData.WorkForm.CreateGraphics();

        private static Point[] points = new Point[3];

        private static Pen pen = new Pen(Color.Gray, 3);

        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect,
                                                       int nTopRect,
                                                       int nRightRect,
                                                       int nBottomRect,
                                                       int nWidthEllipse,
                                                       int nHeightEllipse);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        public static void Form_Load(object sender, EventArgs e)
        {
            IntPtr hRgn = CreateRoundRectRgn(0, 0, 1300, 700, 80, 80);
            SetWindowRgn(GlobalData.WorkForm.Handle, hRgn, true);
        }

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

        public static void CreatePanelElements(PictureBox picturePanel, Elements.Type[] types) 
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
                    pictureBlock[i].Image = Properties.Resources.ammeter;
                    title[i].Text = "Амперметр";
                }
                else if (types[i] == Elements.Type.Voltmeter)
                {
                    pictureBlock[i].Name = "voltmeter";
                    pictureBlock[i].Image = Properties.Resources.voltmeter;
                    title[i].Text = "Вольтметр";
                }
                else if (types[i] == Elements.Type.Multimeter)
                {
                    pictureBlock[i].Name = "multimeter";
                    pictureBlock[i].Image = Properties.Resources.multimetr;
                    title[i].Text = "Мультиметр";
                }
                else if (types[i] == Elements.Type.Conductor)
                {
                    pictureBlock[i].Name = "conductor";
                    pictureBlock[i].Image = Properties.Resources.conductor;
                    title[i].Text = "Проводник";
                }
                else if (types[i] == Elements.Type.Resistor)
                {
                    pictureBlock[i].Name = "resistor";
                    pictureBlock[i].Image = Properties.Resources.resistor;
                    title[i].Text = "Резистор";
                }
                else if (types[i] == Elements.Type.Rheostat)
                {
                    pictureBlock[i].Name = "rheostat";
                    pictureBlock[i].Image = Properties.Resources.rheostat;
                    title[i].Text = "Реостат";
                }
                else if (types[i] == Elements.Type.VoltageSource)
                {
                    pictureBlock[i].Name = "voltage_source";
                    pictureBlock[i].Image = Properties.Resources.voltage;
                    title[i].Text = "Ист.напряжения";
                }
                else if (types[i] == Elements.Type.Capacitor)
                {
                    pictureBlock[i].Name = "capacitor";
                    pictureBlock[i].Image = Properties.Resources.capacitor;
                    title[i].Text = "Конденсатор";
                }
                else if (types[i] == Elements.Type.SingleSwitch)
                {
                    pictureBlock[i].Name = "single_switch";
                    pictureBlock[i].Image = Properties.Resources.s_switch0;
                    title[i].Text = "Перекл. х1";
                }
                else if (types[i] == Elements.Type.DoubleSwitch)
                {
                    pictureBlock[i].Name = "double_switch";
                    pictureBlock[i].Image = Properties.Resources.d_switch;
                    title[i].Text = "Перекл. х2";
                }
                else if (types[i] == Elements.Type.Toggle)
                {
                    pictureBlock[i].Name = "toggle";
                    pictureBlock[i].Image = Properties.Resources.toggle_off;
                    title[i].Text = "Ключ";
                }
                else if (types[i] == Elements.Type.Lamp)
                {
                    pictureBlock[i].Name = "lamp";
                    pictureBlock[i].Image = Properties.Resources.lamp_off;
                    title[i].Text = "Лампочка";
                }
                else if (types[i] == Elements.Type.Stopwatch)
                {
                    pictureBlock[i].Name = "stopwatch";
                    pictureBlock[i].Image = Properties.Resources.stopwatch;
                    title[i].Text = "Секундомер";
                }

                blocks[i].Controls.Add(pictureBlock[i]);
                blocks[i].Controls.Add(title[i]);
                flowPanel.Controls.Add(blocks[i]);
                flowPanel.BringToFront();
            }

            picturePanel.Controls.Add(flowPanel);
        }

        public static void CreatePanelValues(PictureBox picturePanel, Elements.Type[] types, Values[] values)
        {
            flowPanel = new FlowLayoutPanel();
            flowPanel.Left = 9;
            flowPanel.Top = 43;
            flowPanel.Width = 300;
            flowPanel.Height = 433;
            flowPanel.BackColor = Color.Gray;
            flowPanel.AutoScroll = true;

            blocks = new TableLayoutPanel[types.Length + values.Length];
            title = new Label[types.Length];
            pictureBlock = new PictureBox[types.Length];
            nameValue = new Label[types.Length];
            value = new Label[types.Length];

            for (int i = 0; i < types.Length; i++)
            {
                blocks[i] = new TableLayoutPanel();
                blocks[i].RowCount = 2;
                blocks[i].Width = 135;
                blocks[i].Height = 104;
                blocks[i].BackColor = Color.Gray;
                blocks[i].Margin = new Padding(1, 3, 1, 3);
                blocks[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                blocks[i].RowStyles.Add(new RowStyle());
                blocks[i].RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));

                title[i] = new Label();
                title[i].Dock = DockStyle.Fill;
                title[i].Font = new Font("Calibri", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                title[i].TextAlign = ContentAlignment.MiddleLeft;
                title[i].ForeColor = Color.FromArgb(64, 64, 64);

                pictureBlock[i] = new PictureBox();
                pictureBlock[i].SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBlock[i].Dock = DockStyle.Fill;
                pictureBlock[i].Image = Properties.Resources.field;

                if (types[i] == Elements.Type.Ammeter)
                {
                    title[i].Text = "Амперметр";

                }
                else if (types[i] == Elements.Type.Voltmeter)
                {
                    pictureBlock[i].Name = "voltmeter";
                    title[i].Text = "Вольтметр";
                }
                else if (types[i] == Elements.Type.Multimeter)
                {
                    pictureBlock[i].Name = "multimeter";
                    title[i].Text = "Мультиметр";
                }
                else if (types[i] == Elements.Type.Conductor)
                {
                    pictureBlock[i].Name = "conductor";
                    title[i].Text = "Проводник";
                }
                else if (types[i] == Elements.Type.Resistor)
                {
                    pictureBlock[i].Name = "resistor";
                    title[i].Text = "Резистор";
                }
                else if (types[i] == Elements.Type.Rheostat)
                {
                    pictureBlock[i].Name = "rheostat";
                    title[i].Text = "Реостат";
                }
                else if (types[i] == Elements.Type.VoltageSource)
                {
                    pictureBlock[i].Name = "voltage_source";
                    title[i].Text = "Ист.напряжения";
                }
                else if (types[i] == Elements.Type.Capacitor)
                {
                    pictureBlock[i].Name = "capacitor";
                    title[i].Text = "Конденсатор";
                }
                else if (types[i] == Elements.Type.SingleSwitch)
                {
                    pictureBlock[i].Name = "single_switch";
                    title[i].Text = "Перекл. х1";
                }
                else if (types[i] == Elements.Type.DoubleSwitch)
                {
                    pictureBlock[i].Name = "double_switch";
                    title[i].Text = "Перекл. х2";
                }
                else if (types[i] == Elements.Type.Toggle)
                {
                    pictureBlock[i].Name = "toggle";
                    title[i].Text = "Ключ";
                }
                else if (types[i] == Elements.Type.Lamp)
                {
                    pictureBlock[i].Name = "lamp";
                    title[i].Text = "Лампочка";
                }
                else if (types[i] == Elements.Type.Stopwatch)
                {
                    pictureBlock[i].Name = "stopwatch";
                    title[i].Text = "Секундомер";
                }

                blocks[i].Controls.Add(title[i]);
                blocks[i].Controls.Add(pictureBlock[i]);
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
            GlobalData.WorkForm.Controls.Add(picture);
            picture.Visible = true;
        }

        public static void HidePicture()
        {
            picture.Visible = false;
        } 
        #endregion
    }
}
