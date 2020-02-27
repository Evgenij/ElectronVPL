﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    class VoltageSource : Device, IVisualization
    {
        //компоненты формы для создания элемента цепи
        private PictureBox picture;
        private PictureBox status;
        private PictureBox contactMinus;
        private PictureBox contactPlus;
        private TextBox labelValue;
        private Zeroit.Framework.Metro.ZeroitMetroKnob knob;
        private Zeroit.Framework.Metro.ZeroitMetroSwitch _switch;
 
        public VoltageSource()
        {
            picture = new PictureBox();
            labelValue = new TextBox();
            status = new PictureBox();
            contactMinus = new PictureBox();
            contactPlus = new PictureBox();
            knob = new Zeroit.Framework.Metro.ZeroitMetroKnob();
            _switch = new Zeroit.Framework.Metro.ZeroitMetroSwitch();

            labelValue.Text = "1";
            this.Value = 1;
            this.statusDevice = false;
        }

        public void Visualization(Form form, int x, int y)
        {
            picture.Width = 252;
            picture.Height = 96;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.SizeMode = PictureBoxSizeMode.AutoSize;
            picture.BackColor = Color.Transparent;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltage_source\voltage.png");

            //метод загрузки шрифта
            GlobalData.LoadFont(12);  
            labelValue.Hide();
            labelValue.ReadOnly = true;
            labelValue.TabStop = false;
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 179;
            labelValue.Top = 39;
            labelValue.BorderStyle = BorderStyle.None;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 50;
            labelValue.ForeColor = Color.DeepSkyBlue;
            labelValue.TextAlign = HorizontalAlignment.Right;
            labelValue.Cursor = Cursors.Hand;
            labelValue.MouseMove += LabelValue_MouseMove;
            labelValue.TextChanged += LabelValue_TextChanged;
            picture.Controls.Add(labelValue);

            knob.Top = 20;
            knob.Left = 90;
            knob.Width = 63;
            knob.Height = 63;
            knob.BlockedAngle = 90;
            knob.Maximum = 50;
            knob.Minimum = 1;
            knob.Value = 1;
            knob.BorderColor = Color.DimGray;
            knob.LineColor = Color.DimGray;
            knob.AccentColor = Color.DimGray;
            knob.FillColor = Color.Black;
            knob.Cursor = Cursors.Hand;
            knob.LineWidth = 9;
            knob.LineLength = 100;
            knob.LinePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            knob.LinePen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
            knob.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            knob.LinePen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
            knob.ValueChanged += Knob_ValueChanged;
            knob.MouseDown += Knob_MouseDown;
            knob.MouseUp += Knob_MouseUp;
            picture.Controls.Add(knob);

            status.Width = 10;
            status.Height = 10;
            status.Left = 20;
            status.Top = 22;
            status.SizeMode = PictureBoxSizeMode.AutoSize;
            status.BackColor = Color.Transparent;
            status.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltage_source\status_off.png");
            picture.Controls.Add(status);

            _switch.Width = 38;
            _switch.Height = 20;
            _switch.Left = 36;
            _switch.Top = 17;
            _switch.Cursor = Cursors.Hand;
            _switch.DefaultColor = Color.DodgerBlue;
            _switch.CheckColor = Color.Silver;
            _switch.HoverColor = Color.DodgerBlue;
            _switch.CheckedChanged += _switch_CheckedChanged;
            picture.Controls.Add(_switch);

            // код создания контактов для подключения

            contactMinus.Width = 34;
            contactMinus.Height = 12;
            contactMinus.Left = 174;
            contactMinus.Top = 90;
            contactMinus.Cursor = Cursors.Hand;
            contactMinus.BackColor = Color.Transparent;
            picture.Controls.Add(contactMinus);

            contactPlus.Width = 34;
            contactPlus.Height = 12;
            contactPlus.Left = 208;
            contactPlus.Top = 90;
            contactPlus.Cursor = Cursors.Hand;
            contactPlus.BackColor = Color.Transparent;
            picture.Controls.Add(contactPlus);

            // распределение составляющих компонента по слоям

            picture.SendToBack();
            knob.BringToFront();
            contactMinus.BringToFront();
            contactPlus.BringToFront();
            _switch.BringToFront();
            form.Controls.Add(picture);
        }

        private void Knob_MouseDown(object sender, MouseEventArgs e)
        {
            if (statusDevice == false)
            {
                MessageBox.Show(" Для изменения значения устройства, включите его.", "Сообщение", MessageBoxButtons.OK);
            }
        }

        private void Knob_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalData.workWithElements.AddChangesValue(
                this,
                this.Value);
        }

        private void _switch_CheckedChanged(object sender, EventArgs e)
        {
            if (statusDevice == false)
            {
                status.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltage_source\status_on.png");
                labelValue.Show();
                statusDevice = true;
            }
            else
            {
                status.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltage_source\status_off.png");
                labelValue.Hide();
                statusDevice = false;
            }
        }

        private void Knob_ValueChanged(object sender, EventArgs e)
        {
            if (statusDevice == true)
            {
                this.Value = knob.Value;
                labelValue.Text = Convert.ToString(this.Value);
            }
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
