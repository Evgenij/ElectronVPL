using System;
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

        // Контакты для основных элементов цепи
        protected PictureBox contactMinus;
        protected PictureBox contactPlus;

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

            contactMinus = new PictureBox();
            contactPlus = new PictureBox();

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

            // Установка общих свойств контактов элементов цепи
            contactMinus.Cursor = Cursors.Hand;
            contactMinus.BackColor = Color.Transparent;
            contactMinus.Click += ContactMinus_Click;
            contactMinus.MouseHover += ContactMinus_MouseHover;
            contactMinus.MouseLeave += ContactMinus_MouseLeave;
            picture.Controls.Add(contactMinus);

            contactPlus.Cursor = Cursors.Hand;
            contactPlus.BackColor = Color.Transparent;
            contactPlus.Click += ContactPlus_Click;
            contactPlus.MouseHover += ContactPlus_MouseHover;
            contactPlus.MouseLeave += ContactPlus_MouseLeave;
            picture.Controls.Add(contactPlus);

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
