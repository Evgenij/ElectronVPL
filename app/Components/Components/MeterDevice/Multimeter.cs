using System;
using System.Windows.Forms;
using System.Drawing;


namespace Components
{
    class Multimeter : MeterDevice, IVisualization
    {
        //компоненты формы для создания елемента цепи
        private PictureBox button;
        private PictureBox status;
        private Zeroit.Framework.Metro.ZeroitMetroKnob knob;
        private enum TypeUnit { Picofarad, Nanofarad, Microfarad, Millifarad }
        private TypeUnit typeUnit;

        public Multimeter()
        {
            button = new PictureBox();
            status = new PictureBox();
            knob = new Zeroit.Framework.Metro.ZeroitMetroKnob();

            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\multimetr.png");

            button.Width = 41;
            button.Height = 18;
            button.Left = 15;
            button.Top = 58;
            button.Cursor = Cursors.Hand;
            button.SizeMode = PictureBoxSizeMode.AutoSize;
            button.BackColor = Color.Transparent;
            button.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\power1.png");
            button.Click += Button_Click;
            button.MouseDown += Button_MouseDown;
            button.MouseUp += Button_MouseUp;

            status.Width = 8;
            status.Height = 8;
            status.Left = 80;
            status.Top = 60;
            status.SizeMode = PictureBoxSizeMode.AutoSize;
            status.BackColor = Color.Transparent;
            status.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\status_off.png");

            //метод загрузки шрифта
            GlobalData.LoadFont(13);
            labelValue.Text = "";
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 22;
            labelValue.Top = 29;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 75;
            labelValue.ForeColor = Color.Silver;
            labelValue.TextAlign = HorizontalAlignment.Right;
            labelValue.Cursor = Cursors.Hand;

            knob.Top = 105;
            knob.Left = 32;
            knob.Width = 59;
            knob.Height = 59;
            knob.BlockedAngle = 0;
            knob.BorderColor = Color.DimGray;
            knob.LineColor = Color.DimGray;
            knob.AccentColor = Color.DimGray;
            knob.FillColor = Color.Black;
            knob.Cursor = Cursors.Hand;
            knob.LineWidth = 7;
            knob.LineLength = 100;
            knob.Value = 20;
            knob.ValueChanged += Knob_ValueChanged;
            knob.LinePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            knob.LinePen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
            knob.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            knob.LinePen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            contactMinus.Width = 33;
            contactMinus.Height = 12;
            contactMinus.Left = 16;
            contactMinus.Top = 192;

            contactPlus.Width = 33;
            contactPlus.Height = 12;
            contactPlus.Left = 49;
            contactPlus.Top = 192;

            statusDevice = false;
            labelValue.Visible = false;
            typeUnit = TypeUnit.Picofarad;
            this.Value = 0;
        }

        /// <summary>
        /// Метод отображения компонента на форме
        /// </summary>
        /// <param name="form">Форма на которой будет отображен элемент</param>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height - 10;

            SetPositionControls(4, 4, picture.Width - 24, 4);

            picture.Controls.Add(button);
            picture.Controls.Add(status);
            picture.Controls.Add(labelValue);
            picture.Controls.Add(knob);

            // код создания контактов для подключения

            picture.Controls.Add(contactMinus);
            picture.Controls.Add(contactPlus);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form, 20, 49);

            // распределение составляющих компонента по слоям

            labelValue.BringToFront();
            contactMinus.BringToFront();
            contactPlus.BringToFront();
            plugMinusDU.BringToFront();
            plugPlusDU.BringToFront();
            pictureDelete.BringToFront();
            pictureMove.BringToFront();
        }

        /// <summary>
        /// Устанавливает и выводит значение емкости конденсатора
        /// </summary>
        /// <param name="value">Значение емкости конденсатора</param>
        public void SetValue(double value)
        {
            this.Value = value;
            //MessageBox.Show(Convert.ToString(this.Value));

            labelValue.Text = Convert.ToString(this.Value * GlobalData.pF);
            GlobalData.workWithElements.AddChangesValueMultimeter(this, labelValue.Text);
        }

        public string GetUnit() 
        {
            if (this.typeUnit == TypeUnit.Picofarad) 
            {
                return "пФ";
            }
            else if (this.typeUnit == TypeUnit.Nanofarad)
            {
                return "нФ";
            }
            else if (this.typeUnit == TypeUnit.Microfarad)
            {
                return "мкФ";
            }
            else
            {
                return "мФ";
            }
        }

        private void Knob_ValueChanged(object sender, EventArgs e)
        {
            labelValue.Text = "";
            if (knob.Value > 17 && knob.Value <= 24)
            {
                labelValue.Text += Convert.ToString(this.Value * GlobalData.pF);
                if (knob.Value == 20)
                {
                    typeUnit = TypeUnit.Picofarad;
                    GlobalData.workWithElements.AddChangesValueMultimeter(this, labelValue.Text);
                }
            }
            else if (knob.Value > 11 && knob.Value <= 17)
            {
                labelValue.Text += Convert.ToString(this.Value * GlobalData.nF);
                if (knob.Value == 14)
                {
                    typeUnit = TypeUnit.Nanofarad;
                    GlobalData.workWithElements.AddChangesValueMultimeter(this, labelValue.Text);
                }
            }
            else if (knob.Value > 4 && knob.Value <= 11)
            {
                labelValue.Text += Convert.ToString(this.Value * GlobalData.mkF);
                if (knob.Value == 7)
                {
                    typeUnit = TypeUnit.Microfarad;
                    GlobalData.workWithElements.AddChangesValueMultimeter(this, labelValue.Text);
                }
            }
            else if ((knob.Value >= 0 && knob.Value <= 4) || (knob.Value <= 100 && knob.Value >= 97))
            {
                labelValue.Text += Convert.ToString(this.Value * GlobalData.mF);
                if (knob.Value == 1)
                {
                    typeUnit = TypeUnit.Millifarad;
                    GlobalData.workWithElements.AddChangesValueMultimeter(this, labelValue.Text);
                }
            }
            else
            {
                labelValue.Text = "-//-";
            }
            
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            button.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\power1.png");
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            button.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\power2.png");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (statusDevice == false)
            {
                status.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\status_on.png");
                this.labelValue.Show();
                statusDevice = true;
            }
            else
            {
                status.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\multimetr\status_off.png");
                this.labelValue.Hide();
                statusDevice = false;
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
