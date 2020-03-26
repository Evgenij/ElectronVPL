using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    class MeterDevice : Device, ICalculate
    {
        //компоненты формы для создания елемента цепи
        protected CircleAnglePicker valueArrow = new CircleAnglePicker();

        protected MeterDevice() 
        {
            //метод загрузки шрифта
            GlobalData.LoadFont(12);
            labelValue.Text = Convert.ToString(this.Value);
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 43;
            labelValue.Top = 96;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 50;
            labelValue.TextAlign = HorizontalAlignment.Center;

            contactMinus.Width = 35;
            contactMinus.Height = 12;
            contactMinus.Left = 13;
            contactMinus.Top = 113;

            contactPlus.Width = 35;
            contactPlus.Height = 12;
            contactPlus.Left = 85;
            contactPlus.Top = 113;

            valueArrow.Value = 180;
            labelValue.Text = "0";
        }

        public virtual double Calculate(double volt, double resist) 
        {
            return 0;
        }
    }
}
