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
        protected PictureBox contactMinus;
        protected PictureBox contactPlus;
        protected PictureBox plugMinusDown;
        protected PictureBox plugPlusDown;
        protected PictureBox plugMinusLeft;
        protected PictureBox plugPlusRight;

        // Общие свойства элементов цепи
        protected bool connectSource = false;
        protected bool connectReceiver = false;
        protected Point pointMinus, pointPlus;

        public Device() 
        {
            picture = new PictureBox();
            labelValue = new TextBox();
            contactMinus = new PictureBox();
            contactPlus = new PictureBox();
            plugMinusDown = new PictureBox();
            plugPlusDown = new PictureBox();
            plugMinusLeft = new PictureBox();
            plugPlusRight = new PictureBox();

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
            plugMinusDown.Visible = false;
            plugMinusDown.Enabled = false;
            plugMinusDown.Width = 27;
            plugMinusDown.Height = 27;
            plugMinusDown.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\du.gif");
            plugMinusDown.BackColor = Color.Transparent;

            plugPlusDown.Visible = false;
            plugPlusDown.Enabled = false;
            plugPlusDown.Width = 27;
            plugPlusDown.Height = 27;
            plugPlusDown.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\du.gif");
            plugPlusDown.BackColor = Color.Transparent;

            plugMinusLeft.Visible = false;
            plugMinusLeft.Enabled = false;
            plugMinusLeft.Width = 27;
            plugMinusLeft.Height = 27;
            plugMinusLeft.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\lr.gif");
            plugMinusLeft.BackColor = Color.Transparent;

            plugPlusRight.Visible = false;
            plugPlusRight.Enabled = false;
            plugPlusRight.Width = 27;
            plugPlusRight.Height = 27;
            plugPlusRight.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\gifs\plugs\rl.gif");
            plugPlusRight.BackColor = Color.Transparent;


            picture.SendToBack();
        }

        public Point GetPointMinus() 
        {
            return pointMinus;
        }
        public Point GetPointPlus()
        {
            return pointPlus;
        }

        protected void SetPositionsPlugs(Form form, int posMinus, int posPlus) 
        {
            plugMinusDown.Parent = form;
            form.Controls.Add(plugMinusDown);
            plugMinusDown.Top = picture.Top + picture.Height - 4;
            plugMinusDown.Left = picture.Left + posMinus;

            pointMinus = new Point(
                plugMinusDown.Left + (plugMinusDown.Width / 2),
                plugMinusDown.Top + plugMinusDown.Height
                );

            plugPlusDown.Parent = form;
            form.Controls.Add(plugPlusDown);
            plugPlusDown.Top = picture.Top + picture.Height - 4;
            plugPlusDown.Left = picture.Left + posPlus;

            pointPlus = new Point(
                plugPlusDown.Left + (plugPlusDown.Width / 2),
                plugPlusDown.Top + plugPlusDown.Height
                );
        }

        protected void SetPositionsPlugs(Form form, int posLeft, int posDown, int posRight)
        {
            SetPositionsPlugs(form, posLeft, posRight);

            form.Controls.Add(plugPlusDown);
            plugPlusDown.Top = picture.Top + picture.Height - 4;
            plugPlusDown.Left = picture.Left + posDown;

            pointPlus = new Point(
                plugPlusDown.Left + (plugPlusDown.Width / 2),
                plugPlusDown.Top + plugPlusDown.Height
                );
        }

        private void ContactPlus_Click(object sender, EventArgs e)
        {
            connectSource = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusDown, 950);
        }

        private void ContactMinus_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                connectReceiver = true;
                Design.Animate(plugMinusDown, 950);
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
                plugPlusDown.Visible = false;
            }
        }

        private void ContactPlus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusDown.Visible = true;
        }

        private void ContactMinus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                plugMinusDown.Visible = false;
            }
        }

        private void ContactMinus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugMinusDown.Visible = true;
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
