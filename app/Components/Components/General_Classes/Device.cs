using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Device : IDisposable, IVisualization
    {
        // Состояние элемента цепи
        protected bool statusDevice = false;

        // Значение элемента цепи
        protected double Value { get; set; }

        // Координаты центра элемента цепи
        protected int X { get; set; }
        protected int Y { get; set; }
        protected Point MouseDownLoc;

        // Размер элемента цепи
        protected int Width { get; set; }
        protected int Height { get; set; }

        // Компоненты WindowsForm для создания элементов цепи
        protected PictureBox picture;
        protected TextBox labelValue;
        protected PictureBox pictureMove;
        protected PictureBox pictureDelete;

        // Контакты для основных элементов цепи
        protected PictureBox contactMinus;
        protected PictureBox contactPlus;
        protected PictureBox contactLeft;
        protected PictureBox contactRight;

        // -----------------
        protected PictureBox plugMinusDU;
        protected PictureBox plugPlusDU;

        protected PictureBox plugMinusUD;
        protected PictureBox plugLeftMinusUD;
        protected PictureBox plugRightMinusUD;
        protected PictureBox plugPlusUD;
        protected PictureBox plugLeftPlusUD;
        protected PictureBox plugRightPlusUD;

        protected PictureBox plugMinusLR;
        protected PictureBox plugPlusLR;

        protected PictureBox plugMinusRL;
        protected PictureBox plugPlusRL;
        // -----------------

        // Общие свойства элементов цепи
        protected bool connectSource = false;
        protected bool connectReceiver = false;

        // Точки соединений для основных элементов цепи
        protected Point pointMinus, pointPlus;
        // Точки соединений для переключателей
        protected Point pointLeft, pointTop, pointBottom;
        protected Point pointLeftMinus, pointLeftPlus,
                        pointRightMinus, pointRightPlus,
                        pointBottomMinus, pointBottomPlus;

        public bool[] contactsSingleSwitch = { false, false, false };
        public bool[,] contactsDoubleSwitch = { // левый верхний контакт
                                                { false, false },
                                                // правый верхний контакт
                                                { false, false },
                                                // нижний контакт
                                                { false, false }};

        private bool successConnect = false;

        public Device() 
        { 
            picture = new PictureBox();
            labelValue = new TextBox();
            pictureDelete = new PictureBox();
            pictureMove = new PictureBox();

            contactMinus = new PictureBox();
            contactPlus = new PictureBox();
            contactLeft = new PictureBox();
            contactRight = new PictureBox();

            // ----------------------
            plugMinusDU = new PictureBox();
            plugPlusDU = new PictureBox();

            plugMinusUD = new PictureBox();
                plugLeftMinusUD = new PictureBox();
                plugRightMinusUD = new PictureBox();
            plugPlusUD = new PictureBox();
                plugLeftPlusUD = new PictureBox();
                plugRightPlusUD = new PictureBox();

            plugMinusLR = new PictureBox();
            plugPlusLR = new PictureBox();

            plugMinusRL = new PictureBox();
            plugPlusRL = new PictureBox();
            // ----------------------

            // Установка общих свойств изображения элементов
            picture.BackColor = Color.Transparent;

            // Установка общих свойств отображения поля вывода значений
            labelValue.Text = "";
            labelValue.ReadOnly = true;
            labelValue.TabStop = false;
            labelValue.BorderStyle = BorderStyle.None;
            labelValue.Cursor = Cursors.Hand;
            labelValue.MouseMove += LabelValue_MouseMove;
            labelValue.TextChanged += LabelValue_TextChanged;

            pictureMove.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureMove.Height = 20;
            pictureMove.Width = 20;
            pictureMove.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\controls\move0.png");
            pictureMove.BackColor = Color.Transparent;
            pictureMove.Cursor = Cursors.SizeAll;
            pictureMove.MouseHover += PictureMove_MouseHover;
            pictureMove.MouseLeave += PictureMove_MouseLeave;
            pictureMove.MouseDown += PictureMove_MouseDown;
            pictureMove.MouseUp += PictureMove_MouseUp;
            pictureMove.MouseMove += PictureMove_MouseMove;
            picture.Controls.Add(pictureMove);

            pictureDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureDelete.Height = 20;
            pictureDelete.Width = 20;
            pictureDelete.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\controls\delete0.png");
            pictureDelete.BackColor = Color.Transparent;
            pictureDelete.Cursor = Cursors.Hand;
            pictureDelete.Click += PictureDelete_Click;
            pictureDelete.MouseHover += PictureDelete_MouseHover;
            pictureDelete.MouseLeave += PictureDelete_MouseLeave;
            picture.Controls.Add(pictureDelete);

            // Установка общих свойств контактов элементов цепи

            CreateContacts();

            // Установка общих свойств штекеров элементов цепи

            CreatePlugs();    

            picture.SendToBack();
            GlobalData.MainForm.Controls.Add(picture);
        }

        ~Device()
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

        private void CreatePlugs()
        {
            // Установка свойств для штекера минусового контакта "снизу-вверх"
            plugMinusDU.Visible = false;
            plugMinusDU.Enabled = false;
            plugMinusDU.Width = 27;
            plugMinusDU.Height = 27;
            plugMinusDU.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\du1.gif");
            plugMinusDU.BackColor = Color.Transparent;
            

            // Установка свойств для штекера плюсового контакта "снизу-вверх"
            plugPlusDU.Visible = false;
            plugPlusDU.Enabled = false;
            plugPlusDU.Width = 27;
            plugPlusDU.Height = 27;
            plugPlusDU.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\du1.gif");
            plugPlusDU.BackColor = Color.Transparent;

            // Установка свойств для штекера минусового контакта "вверх-вниз"
            plugMinusUD.Visible = false;
            plugMinusUD.Enabled = false;
            plugMinusUD.Width = 27;
            plugMinusUD.Height = 27;
            plugMinusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud1.gif");
            plugMinusUD.BackColor = Color.Transparent;

            // Установка свойств для штекеров минусового контакта "вверх-вниз" двойного переключателя
            plugLeftMinusUD.Visible = false;
            plugLeftMinusUD.Enabled = false;
            plugLeftMinusUD.Width = 27;
            plugLeftMinusUD.Height = 27;
            plugLeftMinusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud1.gif");
            plugLeftMinusUD.BackColor = Color.Transparent;

            plugRightMinusUD.Visible = false;
            plugRightMinusUD.Enabled = false;
            plugRightMinusUD.Width = 27;
            plugRightMinusUD.Height = 27;
            plugRightMinusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud1.gif");
            plugRightMinusUD.BackColor = Color.Transparent;

            // Установка свойств для штекера плюсового контакта "вверх-вниз"
            plugPlusUD.Visible = false;
            plugPlusUD.Enabled = false;
            plugPlusUD.Width = 27;
            plugPlusUD.Height = 27;
            plugPlusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud1.gif");
            plugPlusUD.BackColor = Color.Transparent;

            // Установка свойств для штекеров минусового контакта "вверх-вниз" двойного переключателя
            plugLeftPlusUD.Visible = false;
            plugLeftPlusUD.Enabled = false;
            plugLeftPlusUD.Width = 27;
            plugLeftPlusUD.Height = 27;
            plugLeftPlusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud1.gif");
            plugLeftPlusUD.BackColor = Color.Transparent;

            plugRightPlusUD.Visible = false;
            plugRightPlusUD.Enabled = false;
            plugRightPlusUD.Width = 27;
            plugRightPlusUD.Height = 27;
            plugRightPlusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud1.gif");
            plugRightPlusUD.BackColor = Color.Transparent;

            // Установка свойств для штекера минусового контакта "слева-направо"
            plugMinusLR.Visible = false;
            plugMinusLR.Enabled = false;
            plugMinusLR.Width = 27;
            plugMinusLR.Height = 27;
            plugMinusLR.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\lr1.gif");
            plugMinusLR.BackColor = Color.Transparent;

            // Установка свойств для штекера плюсового контакта "слева-направо"
            plugPlusLR.Visible = false;
            plugPlusLR.Enabled = false;
            plugPlusLR.Width = 27;
            plugPlusLR.Height = 27;
            plugPlusLR.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\lr1.gif");
            plugPlusLR.BackColor = Color.Transparent;

            // Установка свойств для штекера минусового контакта "cправа-налево"
            plugMinusRL.Visible = false;
            plugMinusRL.Enabled = false;
            plugMinusRL.Width = 27;
            plugMinusRL.Height = 27;
            plugMinusRL.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\rl1.gif");
            plugMinusRL.BackColor = Color.Transparent;

            // Установка свойств для штекера плюсового контакта "cправа-налево"
            plugPlusRL.Visible = false;
            plugPlusRL.Enabled = false;
            plugPlusRL.Width = 27;
            plugPlusRL.Height = 27;
            plugPlusRL.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\rl1.gif");
            plugPlusRL.BackColor = Color.Transparent;
        }

        private void CreateContacts() 
        {
            contactMinus.Cursor = Cursors.Hand;
            contactMinus.BackColor = Color.Transparent;
            contactMinus.Click += ContactMinus_Click;
            contactMinus.MouseHover += ContactMinus_MouseHover;
            contactMinus.MouseLeave += ContactMinus_MouseLeave;

            contactPlus.Cursor = Cursors.Hand;
            contactPlus.BackColor = Color.Transparent;
            contactPlus.Click += ContactPlus_Click;
            contactPlus.MouseHover += ContactPlus_MouseHover;
            contactPlus.MouseLeave += ContactPlus_MouseLeave;

            contactLeft.Cursor = Cursors.Hand;
            contactLeft.BackColor = Color.Transparent;
            contactLeft.Click += ContactLeft_Click;
            contactLeft.MouseHover += ContactLeft_MouseHover;
            contactLeft.MouseLeave += ContactLeft_MouseLeave;

            contactRight.Cursor = Cursors.Hand;
            contactRight.BackColor = Color.Transparent;
            contactRight.Click += ContactRight_Click;
            contactRight.MouseHover += ContactRight_MouseHover;
            contactRight.MouseLeave += ContactRight_MouseLeave;
        }

        public virtual void Visualization(Form form, int x, int y) { }

        private void PictureMove_MouseUp(object sender, MouseEventArgs e)
        {
            if (picture.Top < 0)
            {
                picture.Top = 10;
            }
            if (picture.Left < 0) 
            {
                picture.Left = 10;
            }

            InitializationPlugs(this);
        }

        private void PictureMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (picture.Top >= 0 || picture.Left >= 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    picture.Left = e.X + picture.Left - MouseDownLoc.X;
                    picture.Top = e.Y + picture.Top - MouseDownLoc.Y;
                    HidePlugs(this);
                }
            }
        }

        private void PictureMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLoc = e.Location;
            }
        }

        private void PictureMove_MouseLeave(object sender, EventArgs e)
        {
            pictureMove.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\controls\move0.png");
        }

        private void PictureMove_MouseHover(object sender, EventArgs e)
        {
            pictureMove.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\controls\move1.png");
        }

        private void PictureDelete_MouseLeave(object sender, EventArgs e)
        {
            pictureDelete.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\controls\delete0.png");
        }

        private void PictureDelete_MouseHover(object sender, EventArgs e)
        {
            pictureDelete.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\controls\delete1.png");
        }

        private void PictureDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void SetPositionControls(int leftMove, int topMove, int leftDel, int topDel) 
        {
            pictureMove.Left = leftMove;
            pictureMove.Top = topMove;
            pictureDelete.Left = leftDel;
            pictureDelete.Top = topDel;
        }

        public void Delete()
        {
            this.picture.Dispose();
            this.Dispose();

            if (this is Ammeter)
            {
                Elements.ammeter = null;
            }
            else if (this is Voltmeter)
            {
                Elements.voltmeter = null;
            }
            else if (this is Multimeter)
            {
                Elements.multimeter = null;
            }
            else if (this is Conductor)
            {
                Elements.conductor = null;
            }
            else if (this is Rheostat)
            {
                Elements.rheostat = null;
            }
            else if (this is Resistor)
            {
                Elements.resistor = null;
            }
            else if (this is Capacitor)
            {
                Elements.capacitor = null;
            }
            else if (this is VoltageSource)
            {
                Elements.voltageSource = null;
            }
            else if (this is Lamp)
            {
                Elements.lamp = null;
            }
            else if (this is Toggle)
            {
                Elements.toggle = null;
            }
            else if (this is SingleSwitch)
            {
                Elements.singleSwitch = null;
            }
            else if (this is DoubleSwitch)
            {
                Elements.doubleSwitch = null;
            }
            else if (this is Stopwatch)
            {
                Elements.stopwatch = null;
            }

            DeletePlugs();
        }

        #region Getting plug points

        // методы возврата координат контактов элементов
        public Point GetPointMinus()
        {
            return pointMinus;
        }
        public Point GetPointPlus()
        {
            return pointPlus;
        }

        // методы возврата координат контактов одинарного переключателя
        public Point GetPointLeft()
        {
            return pointLeft;
        }
        public Point GetPointTop()
        {
            return pointTop;
        }
        public Point GetPointBottom()
        {
            return pointBottom;
        }

        // методы возврата координат контактов двойного переключателя
        public Point GetPointLeftMinus()
        {
            return pointLeftMinus;
        }
        public Point GetPointLeftPlus()
        {
            return pointLeftPlus;
        }
        public Point GetPointRightMinus()
        {
            return pointRightMinus;
        }
        public Point GetPointRightPlus()
        {
            return pointRightPlus;
        }
        public Point GetPointBottomMinus()
        {
            return pointBottomMinus;
        }
        public Point GetPointBottomPlus()
        {
            return pointBottomPlus;
        } 

        #endregion

        #region SetPositions plugs

        protected void SetPositionsPlugs(Form form, int posMinus, int posPlus)
        {
            form.Controls.Add(plugMinusDU);
            plugMinusDU.Top = picture.Top + picture.Height - 4;
            plugMinusDU.Left = picture.Left + posMinus;
            plugMinusDU.BringToFront();

            pointMinus = new Point(
                plugMinusDU.Left + (plugMinusDU.Width / 2),
                plugMinusDU.Top + plugMinusDU.Height);

            form.Controls.Add(plugPlusDU);
            plugPlusDU.Top = picture.Top + picture.Height - 4;
            plugPlusDU.Left = picture.Left + posPlus;
            plugPlusDU.BringToFront();

            pointPlus = new Point(
                plugPlusDU.Left + (plugPlusDU.Width / 2),
                plugPlusDU.Top + plugPlusDU.Height);

            plugMinusDU.BringToFront();
            plugPlusDU.BringToFront();
        }

        protected virtual void SetPositionsPlugs(Form form)
        {
        } 

        #endregion

        #region Connecting elements

        private void Connecting(PictureBox plug) 
        {
            if (successConnect == true)
            {
                connectReceiver = true;
                Design.Animate(plug, GlobalData.TimePlugAnimation);
                Design.ConnectionElements(GlobalData.deviceSource, this);
            }
        }

        private void ContactMinus_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                if (GlobalData.deviceSource is Ammeter && !(this is Multimeter) && !(this is Voltmeter))
                {
                    successConnect = true;
                }
                else if (GlobalData.deviceSource is Multimeter && (this is Capacitor || this is Switch))
                {
                    successConnect = true;
                }
                else if (GlobalData.deviceSource is ResistanceDevice && !(this is Capacitor))
                {
                    successConnect = true;
                }
                else if (GlobalData.deviceSource is VoltageSource &&
                    !(this is Voltmeter) &&
                    !(this is Multimeter) &&
                    !(this is Capacitor) &&
                    !(this is Switch))
                {
                    successConnect = true;
                }
                else if (GlobalData.deviceSource is Capacitor && (this is Switch || this is Multimeter))
                {
                    successConnect = true;
                }
                else if (GlobalData.deviceSource is Lamp && !(this is Multimeter) && !(this is Voltmeter) && !(this is Capacitor)) 
                {
                    successConnect = true;
                }
                else
                {
                    MessageBox.Show("Подключение невозможно...");
                }
            }
            else
            {
                MessageBox.Show("Подключение невозможно...");
            }

            Connecting(plugMinusDU);
        }

        private void ContactLeft_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                if (GlobalData.deviceSource is Lamp && !(this is Multimeter) || !(this is Voltmeter) || !(this is Capacitor))
                {
                    successConnect = true;
                }
                else
                {
                    MessageBox.Show("Подключение невозможно...");
                }
            }
            else
            {
                MessageBox.Show("Подключение невозможно...");
            }

            Connecting(plugMinusLR);
        }

        private void ContactRight_Click(object sender, EventArgs e)
        {
            connectSource = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusRL, GlobalData.TimePlugAnimation);
        }

        private void ContactPlus_Click(object sender, EventArgs e)
        {
            connectSource = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusDU, GlobalData.TimePlugAnimation);
        }

        #endregion

        #region Work with plugs

        private void DeletePlugs()
        {
            if (this is MeterDevice || this is ResistanceDevice || this is VoltageSource || this is Capacitor)
            {
                plugMinusDU.Dispose();
                plugPlusDU.Dispose();
                plugMinusDU = null;
                plugPlusDU = null;
            }
            else if (this is Lamp || this is Toggle)
            {
                plugMinusLR.Dispose();
                plugPlusRL.Dispose();
                plugMinusLR = null;
                plugPlusRL = null;
            }
            else if (this is Switch)
            {
                if (this is SingleSwitch)
                {
                    plugMinusLR.Dispose();
                    plugPlusUD.Dispose();
                    plugPlusDU.Dispose();
                    plugMinusLR = null;
                    plugPlusUD = null;
                    plugPlusDU = null;
                }
                else if (this is DoubleSwitch)
                {
                    plugLeftPlusUD.Dispose();
                    plugLeftMinusUD.Dispose();
                    plugRightMinusUD.Dispose();
                    plugRightPlusUD.Dispose();
                    plugPlusDU.Dispose();
                    plugMinusDU.Dispose();
                    plugLeftPlusUD = null;
                    plugLeftMinusUD = null;
                    plugRightMinusUD = null;
                    plugRightPlusUD = null;
                    plugPlusDU = null;
                    plugMinusDU = null;
                }
            }
        }

        private void HidePlugs(Device device)
        {
            if (device is MeterDevice || device is ResistanceDevice || device is VoltageSource || device is Capacitor)
            {
                plugMinusDU.Hide();
                plugPlusDU.Hide();
            }
            else if (device is Lamp || device is Toggle)
            {
                plugMinusLR.Hide();
                plugPlusRL.Hide();
            }
            else if (device is Switch)
            {
                if (device is SingleSwitch)
                {
                    plugMinusLR.Hide();
                    plugPlusUD.Hide();
                    plugPlusDU.Hide();
                }
                else if (device is DoubleSwitch)
                {
                    plugLeftPlusUD.Hide();
                    plugLeftMinusUD.Hide();
                    plugRightMinusUD.Hide();
                    plugRightPlusUD.Hide();
                    plugPlusDU.Hide();
                    plugMinusDU.Hide();
                }
            }
        }

        private void InitializationPlugs(Device device)
        {
            if (device is MeterDevice)
            {
                if (device is Multimeter)
                {
                    SetPositionsPlugs(GlobalData.MainForm, 20, 49);
                }
                else
                {
                    SetPositionsPlugs(GlobalData.MainForm, 18, 90);
                }
            }
            else if (device is ResistanceDevice)
            {
                if (device is Conductor)
                {
                    SetPositionsPlugs(GlobalData.MainForm, 66, 95);
                }
                else if (device is Resistor)
                {
                    SetPositionsPlugs(GlobalData.MainForm, 66, 95);
                }
                else if (device is Rheostat)
                {
                    SetPositionsPlugs(GlobalData.MainForm, 66, 95);
                }
            }
            else if (device is VoltageSource)
            {
                SetPositionsPlugs(GlobalData.MainForm, 180, 209);
            }
            else if (device is Capacitor)
            {
                SetPositionsPlugs(GlobalData.MainForm, 65, 94);
            }
            else if (device is Lamp)
            {
                SetPositionsPlugs(GlobalData.MainForm);
            }
            else if (device is Toggle)
            {
                SetPositionsPlugs(GlobalData.MainForm);
            }
            else if (device is SingleSwitch)
            {
                SetPositionsPlugs(GlobalData.MainForm);
            }
            else if (device is DoubleSwitch)
            {
                SetPositionsPlugs(GlobalData.MainForm);
            }
        }

        private void ContactRight_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusRL.Visible = false;
            }
        }

        private void ContactRight_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusRL.Visible = true;
        }

        private void ContactLeft_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                plugMinusLR.Visible = false;
            }
        }

        private void ContactLeft_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugMinusLR.Visible = true;
        }

        private void ContactPlus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                if (plugPlusDU != null)
                {
                    plugPlusDU.Visible = false;
                }
            }
        }

        private void ContactPlus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            if (plugPlusDU != null)
            {
                if (plugPlusDU != null)
                {
                    plugPlusDU.Visible = true;
                }
            }
        }

        private void ContactMinus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                if (plugMinusDU != null)
                {
                    plugMinusDU.Visible = false;
                }
            }
        }

        private void ContactMinus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            if (plugMinusDU != null)
            {
                plugMinusDU.Visible = true;
            }
        } 
        #endregion

        //метод для отключения выделения текста в TextBox компонента
        private void LabelValue_TextChanged(object sender, EventArgs e)
        {
            labelValue.SelectionLength = 0;
        }

        //метод для отключения выделения текста в TextBox компонента
        private void LabelValue_MouseMove(object sender, MouseEventArgs e)
        {
            labelValue.SelectionLength = 0;
        }
    }
}
