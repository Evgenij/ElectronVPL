using System;
using System.Windows.Forms;
using System.Drawing;

namespace Components
{
    class Ammeter : MeterDevice, IVisualization
    {
        //компоненты формы для создания елемента цепи
        private CircleAnglePicker valueArrow;

        public Ammeter() 
        {
            valueArrow = new CircleAnglePicker();
            
            valueArrow.Value = 180;
            labelValue.Text = "0";
        }

        /// <summary>
        /// Функция подсчета силы тока
        /// </summary>
        /// <param name="volt">Значение напряжения</param>
        /// <param name="resist">Значение сопротивления</param>
        public override double Calculate(double volt, double resist) 
        {
            this.Value = volt / resist;
            labelValue.Text = Convert.ToString(Math.Round(this.Value, 2));
            if (this.Value == 50)
            {
                valueArrow.Value = 0;
            }
            else if (this.Value == 25)
            {
                valueArrow.Value = 90;
            }
            else 
            {
                valueArrow.Value = Convert.ToInt32(180 - this.Value * GlobalData.multiplierValues);
            }

            GlobalData.workWithElements.AddChangesValue(
                this, 
                this.Value);

            return this.Value;
        }

        public void Visualization(Form form, int x, int y) 
        {
            picture.Width = 135;
            picture.Height = 125;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\ammeter\ammeter.png");

            SetPositionControls(95, 0, 115, 10);

            //метод загрузки шрифта
            GlobalData.LoadFont(12);
            labelValue.Text = Convert.ToString(this.Value);
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 43;
            labelValue.Top = 96;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 50;
            labelValue.ForeColor = Color.Red;
            labelValue.TextAlign = HorizontalAlignment.Center;
            picture.Controls.Add(labelValue);

            valueArrow.Parent = picture;
            valueArrow.Enabled = false;
            valueArrow.Height = 115;
            valueArrow.Width = 115;
            valueArrow.BackColor = Color.Transparent;
            valueArrow.Left = 10;
            valueArrow.Top = 9;
            valueArrow.Parent = picture;
            valueArrow.CircleColor = Color.Transparent;
            picture.Controls.Add(valueArrow);

            // Установки свойств контактов для подключения

            contactMinus.Width = 35;
            contactMinus.Height = 12;
            contactMinus.Left = 13;
            contactMinus.Top = 113;

            contactPlus.Width = 35;
            contactPlus.Height = 12;
            contactPlus.Left = 85;
            contactPlus.Top = 113;

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form, 18, 90);

            // Распределение составляющих элемента по слоям
            valueArrow.BringToFront();
            labelValue.BringToFront();
            contactMinus.BringToFront();
            contactPlus.BringToFront();
            plugMinusDU.BringToFront();
            plugPlusDU.BringToFront();
            pictureDelete.BringToFront();
            pictureMove.BringToFront();
            form.Controls.Add(picture);
        }

        public double GetValue() 
        {
            return this.Value;
        }

        private void LabelValue_Click(object sender, EventArgs e)
        {
            Delete(this);
        }
    }
}
