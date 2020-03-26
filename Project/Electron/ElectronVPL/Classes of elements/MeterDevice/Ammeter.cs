using System;
using System.Windows.Forms;
using System.Drawing;

namespace ElectronVPL
{
    class Ammeter : MeterDevice
    {
        public Ammeter() 
        {
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\ammeter\ammeter.png");
            labelValue.ForeColor = Color.Red;

            valueArrow.Parent = picture;
            valueArrow.Enabled = false;
            valueArrow.Height = 115;
            valueArrow.Width = 115;
            valueArrow.BackColor = Color.Transparent;
            valueArrow.Left = 10;
            valueArrow.Top = 9;
            valueArrow.Parent = picture;
            valueArrow.CircleColor = Color.Transparent;
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

        public override void Visualization(Form form, int x, int y) 
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height - 10;

            SetPositionControls(96, 0, 115, 11);

            picture.Controls.Add(labelValue);
            picture.Controls.Add(valueArrow);
            picture.Controls.Add(contactMinus);
            picture.Controls.Add(contactPlus);

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
            //form.Controls.Add(picture);
        }

        public double GetValue() 
        {
            return this.Value;
        }
    }
}
