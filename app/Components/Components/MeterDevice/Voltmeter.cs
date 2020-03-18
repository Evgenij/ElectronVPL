using System;
using System.Windows.Forms;
using System.Drawing;

namespace Components
{
    class Voltmeter : MeterDevice, ICalculate, IVisualization
    {
        //компоненты формы для создания элемента цепи
        private CircleAnglePicker valueArrow;
        
        public Voltmeter()
        {
            valueArrow = new CircleAnglePicker();

            valueArrow.Value = 180;
            labelValue.Text = "0";
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
            picture.Width = 135;
            picture.Height = 125;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltmeter\voltmeter.png");

            SetPositionControls(95, 0, 115, 10);

            GlobalData.LoadFont(12);  //метод загрузки шрифта
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 43;
            labelValue.Top = 96;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 50;
            labelValue.ForeColor = Color.DeepSkyBlue;
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

            // код создания контактов для подключения

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

            // распределение составляющих компонента по слоям

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
    }
}
