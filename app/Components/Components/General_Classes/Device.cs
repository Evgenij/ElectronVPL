﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Device
    {
        // Состояние элемента цепи
        protected bool statusDevice;

        // Значение элемента цепи
        protected double Value { get; set; }

        // Координаты центра элемента цепи
        protected int X { get; set; }
        protected int Y { get; set; }

        // Размер элемента цепи
        protected int Width { get; set; }
        protected int Height { get; set; }

        // Компоненты WindowsForm для создания элементов цепи
        protected PictureBox picture;
        protected TextBox labelValue;
        protected PictureBox pictureDelete;
        protected PictureBox pictureMove;

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

            // Установка общих свойств штекеров элементов цепи

                // Установка свойств для штекера минусового контакта "снизу-вверх"
                plugMinusDU.Visible = false;
                plugMinusDU.Enabled = false;
                plugMinusDU.Width = 27;
                plugMinusDU.Height = 27;
                plugMinusDU.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\du.gif");
                plugMinusDU.BackColor = Color.Red;

                // Установка свойств для штекера плюсового контакта "снизу-вверх"
                plugPlusDU.Visible = false;
                plugPlusDU.Enabled = false;
                plugPlusDU.Width = 27;
                plugPlusDU.Height = 27;
                plugPlusDU.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\du.gif");
                plugPlusDU.BackColor = Color.Red;

                // Установка свойств для штекера минусового контакта "вверх-вниз"
                plugMinusUD.Visible = false;
                plugMinusUD.Enabled = false;
                plugMinusUD.Width = 27;
                plugMinusUD.Height = 27;
                plugMinusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud.gif");
                plugMinusUD.BackColor = Color.Red;

                    // Установка свойств для штекеров минусового контакта "вверх-вниз" двойного переключателя
                    plugLeftMinusUD.Visible = false;
                    plugLeftMinusUD.Enabled = false;
                    plugLeftMinusUD.Width = 27;
                    plugLeftMinusUD.Height = 27;
                    plugLeftMinusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud.gif");
                    plugLeftMinusUD.BackColor = Color.Red;

                    plugRightMinusUD.Visible = false;
                    plugRightMinusUD.Enabled = false;
                    plugRightMinusUD.Width = 27;
                    plugRightMinusUD.Height = 27;
                    plugRightMinusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud.gif");
                    plugRightMinusUD.BackColor = Color.Red;

                // Установка свойств для штекера плюсового контакта "вверх-вниз"
                plugPlusUD.Visible = false;
                plugPlusUD.Enabled = false;
                plugPlusUD.Width = 27;
                plugPlusUD.Height = 27;
                plugPlusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud.gif");
                plugPlusUD.BackColor = Color.Red;

                    // Установка свойств для штекеров минусового контакта "вверх-вниз" двойного переключателя
                    plugLeftPlusUD.Visible = false;
                    plugLeftPlusUD.Enabled = false;
                    plugLeftPlusUD.Width = 27;
                    plugLeftPlusUD.Height = 27;
                    plugLeftPlusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud.gif");
                    plugLeftPlusUD.BackColor = Color.Red;

                    plugRightPlusUD.Visible = false;
                    plugRightPlusUD.Enabled = false;
                    plugRightPlusUD.Width = 27;
                    plugRightPlusUD.Height = 27;
                    plugRightPlusUD.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\ud.gif");
                    plugRightPlusUD.BackColor = Color.Red;

                // Установка свойств для штекера минусового контакта "слева-направо"
                plugMinusLR.Visible = false;
                plugMinusLR.Enabled = false;
                plugMinusLR.Width = 27;
                plugMinusLR.Height = 27;
                plugMinusLR.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\lr.gif");
                plugMinusLR.BackColor = Color.Red;

                // Установка свойств для штекера плюсового контакта "слева-направо"
                plugPlusLR.Visible = false;
                plugPlusLR.Enabled = false;
                plugPlusLR.Width = 27;
                plugPlusLR.Height = 27;
                plugPlusLR.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\lr.gif");
                plugPlusLR.BackColor = Color.Red;

                // Установка свойств для штекера минусового контакта "cправа-налево"
                plugMinusRL.Visible = false;
                plugMinusRL.Enabled = false;
                plugMinusRL.Width = 27;
                plugMinusRL.Height = 27;
                plugMinusRL.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\rl.gif");
                plugMinusRL.BackColor = Color.Red;

                // Установка свойств для штекера плюсового контакта "cправа-налево"
                plugPlusRL.Visible = false;
                plugPlusRL.Enabled = false;
                plugPlusRL.Width = 27;
                plugPlusRL.Height = 27;
                plugPlusRL.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\rl.gif");
                plugPlusRL.BackColor = Color.Red;


            picture.SendToBack();
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
            Delete(this);
        }

        protected void SetPositionControls(int leftMove, int topMove, int leftDel, int topDel) 
        {
            pictureMove.Left = leftMove;
            pictureMove.Top = topMove;
            pictureDelete.Left = leftDel;
            pictureDelete.Top = topDel;
        }

        public void Delete(Device device)
        {
            device.picture.Dispose();
            if (device is Ammeter)
            {
                Elements.ammeter = null;
            }
            else if (device is Voltmeter) 
            {
                Elements.voltmeter = null;
            }
            else if (device is VoltageSource)
            {
                Elements.voltageSource = null;
            }
            else if (device is Conductor)
            {
                Elements.conductor = null;
            }
            else if (device is Rheostat)
            {
                Elements.rheostat = null;
            }
            else if (device is Resistor)
            {
                Elements.resistor = null;
            }
            else if (device is Capacitor)
            {
                Elements.capacitor = null;
            }
            else if (device is Lamp)
            {
                Elements.lamp = null;
            }
            else if (device is Toggle)
            {
                Elements.toggle = null;
            }
            else if (device is Stopwatch)
            {
                Elements.stopwatch = null;
            }
            else if (device is Multimeter)
            {
                Elements.multimeter = null;
            }
            else if (device is SingleSwitch)
            {
                Elements.singleSwitch = null;
            }
            else if (device is DoubleSwitch)
            {
                Elements.doubleSwitch = null;
            }
        }

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

        private void ContactRight_Click(object sender, EventArgs e)
        {
            connectSource = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusRL, GlobalData.TimePlugAnimation);
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

        private void ContactLeft_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2");
            if (GlobalData.deviceSource != this)
            {
                connectReceiver = true;
                Design.Animate(plugMinusLR, GlobalData.TimePlugAnimation);
                Design.ConnectionElements(GlobalData.deviceSource, this);
            }
            else
            {
                MessageBox.Show("Подключение невозможно...");
            }
        }

        protected void SetPositionsPlugs(Form form, int posMinus, int posPlus) 
        {
            form.Controls.Add(plugMinusDU);
            plugMinusDU.Top = picture.Top + picture.Height - 4;
            plugMinusDU.Left = picture.Left + posMinus;

            pointMinus = new Point(
                plugMinusDU.Left + (plugMinusDU.Width / 2),
                plugMinusDU.Top + plugMinusDU.Height - 1);

            form.Controls.Add(plugPlusDU);
            plugPlusDU.Top = picture.Top + picture.Height - 4;
            plugPlusDU.Left = picture.Left + posPlus;       

            pointPlus = new Point(
                plugPlusDU.Left + (plugPlusDU.Width / 2),
                plugPlusDU.Top + plugPlusDU.Height - 1);
        }

        private void ContactPlus_Click(object sender, EventArgs e)
        {
            connectSource = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusDU, GlobalData.TimePlugAnimation);
        }

        private void ContactMinus_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                connectReceiver = true;
                Design.Animate(plugMinusDU, GlobalData.TimePlugAnimation);
                Design.ConnectionElements(GlobalData.deviceSource, this);
            }
            else 
            {
                MessageBox.Show("Подключение невозможно...");
            }
        }

        private void ContactPlus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusDU.Visible = false;
            }
        }

        private void ContactPlus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusDU.Visible = true;
        }

        private void ContactMinus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                plugMinusDU.Visible = false;
            }
        }

        private void ContactMinus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugMinusDU.Visible = true;
        }

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
