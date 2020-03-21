using System;
using System.Windows.Forms;
using System.Drawing;

namespace Components
{
    class Voltmeter : MeterDevice, ICalculate, IVisualization
    {
        public Voltmeter()
        {
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltmeter\voltmeter.png");
            labelValue.ForeColor = Color.DodgerBlue;

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
        /// Функция подсчета напряжения
        /// </summary>
        /// <param name="current">Значение силы тока</param>
        /// <param name="resist">Значение сопротивления</param>
        public override double Calculate(double current, double resist)
        {
            this.Value = current * resist;
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

        /// <summary>
        /// Метод отображения компонента на форме
        /// </summary>
        /// <param name="form">Форма на которой будет отображен элемент</param>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        public void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;

            SetPositionControls(95, 0, 115, 10);

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
    }
}
