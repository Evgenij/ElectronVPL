using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    class Conductor : Resistor, ICalculate
    {
        //компоненты формы для создания элемента цепи
        private PictureBox pictureGear;  //
        private PictureBox picturePanel; //
        private TextBox textBoxL;  //
        private TextBox textBoxD; //

        public Conductor()
        {
            pictureGear = new PictureBox();
            picturePanel = new PictureBox();
            textBoxL = new TextBox();
            textBoxD = new TextBox();

            picture.Width = 188;
            picture.Height = 103;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\conductor\conductor.png");

            GlobalData.LoadFont(12);  //метод загрузки шрифта
            labelValue.ReadOnly = true;
            labelValue.TabStop = false;
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 75;
            labelValue.Top = 15;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 39;
            labelValue.ForeColor = Color.Orange;
            labelValue.TextAlign = HorizontalAlignment.Center;

            pictureGear.Width = 11;
            pictureGear.Height = 12;
            pictureGear.Left = 12;
            pictureGear.Top = 9;
            pictureGear.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureGear.BackColor = Color.Transparent;
            pictureGear.Cursor = Cursors.Hand;
            pictureGear.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\conductor\gear.png");
            pictureGear.Click += PictureGear_Click;

            picturePanel.Visible = false;
            picturePanel.Width = 133;
            picturePanel.Height = 131;
            picturePanel.BackColor = Color.Transparent;
            picturePanel.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\conductor\panel.png");

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxL.TabStop = false;
                textBoxL.Font = GlobalData.DigitalFont;
                textBoxL.Left = 15;
                textBoxL.Top = 33;
                textBoxL.BackColor = Color.White;
                textBoxL.Width = 95;
                textBoxL.ForeColor = Color.Black;
                textBoxL.TextAlign = HorizontalAlignment.Left;
                textBoxL.Cursor = Cursors.IBeam;

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxD.TabStop = false;
                textBoxD.Font = GlobalData.DigitalFont;
                textBoxD.Left = 15;
                textBoxD.Top = 87;
                textBoxD.BackColor = Color.White;
                textBoxD.Width = 95;
                textBoxD.ForeColor = Color.Black;
                textBoxD.TextAlign = HorizontalAlignment.Left;
                textBoxD.Cursor = Cursors.IBeam;

            contactMinus.Width = 34;
            contactMinus.Height = 12;
            contactMinus.Left = 60;
            contactMinus.Top = 92;

            contactPlus.Width = 34;
            contactPlus.Height = 12;
            contactPlus.Left = 94;
            contactPlus.Top = 92;

            labelValue.Text = "0";
            this.resistanceValue = 0;
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            
            picture.Controls.Add(labelValue);
            picture.Controls.Add(pictureGear);

            picturePanel.Left = picture.Left - 130;
            picturePanel.Top = picture.Top - 14;
            form.Controls.Add(picturePanel);
                picturePanel.Controls.Add(textBoxL);
                picturePanel.Controls.Add(textBoxD);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form, 66, 95);

            // распределение состовляющих компонента по слоям

            labelValue.BringToFront();
            contactMinus.BringToFront();
            contactPlus.BringToFront();
            pictureDelete.BringToFront();
            pictureMove.BringToFront();
            //form.Controls.Add(picture);
        }

        public double Calculate(double volt, double current)
        {
            double S = (Math.PI * Math.Pow(d, 2)) / (4 * l);
            return this.p = (volt / current) * S;
        }

        private void PictureGear_Click(object sender, EventArgs e)
        {
            if (picturePanel.Visible == false)
            {
                picturePanel.Visible = true;
            }
            else
            {
                if (textBoxL.Text == "0" || textBoxD.Text == "0")
                {
                    MessageBox.Show("Введите корректные значения проводника");
                    picturePanel.Visible = true;
                }
                else if (textBoxL.Text != "" & textBoxD.Text != "")
                {
                    l = Convert.ToDouble(textBoxL.Text);
                    d = Convert.ToDouble(textBoxD.Text);

                    Calculate(Elements.ammeter.GetValue(), Elements.voltageSource.GetValue());
                    labelValue.Text = Convert.ToString(Math.Round(p, 2));

                    GlobalData.workWithElements.AddChangesValueConductor(l, d, p);
                    GlobalData.workWithElements.AddChangesValue(
                        this,
                        Math.Round(p, 2));

                    picturePanel.Visible = false;
                }
                else if (textBoxL.Text == "" & textBoxD.Text == "")
                {
                    picturePanel.Visible = false;
                }
                else
                {
                    MessageBox.Show("Введите значения проводника"); 
                    picturePanel.Visible = true;
                }
            }
        }
    }
}
