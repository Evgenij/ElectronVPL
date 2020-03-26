using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    class Lamp : Device
    {
        public Lamp() 
        {
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\lamp\lamp_off.png");

            contactLeft.Width = 12;
            contactLeft.Height = 35;
            contactLeft.Left = 0;
            contactLeft.Top = 34;  
            
            contactRight.Width = 12;
            contactRight.Height = 35;
            contactRight.Left = picture.Width - 12;
            contactRight.Top = 34;

            this.statusDevice = false;
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height - 10;

            SetPositionControls(2, 2, picture.Width - 22, 2);

            // код создания контактов для подключения
            picture.Controls.Add(contactLeft);
            picture.Controls.Add(contactRight);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form);

            // распределение составляющих компонента по слоям

            picture.SendToBack();
            contactLeft.BringToFront();
            contactRight.BringToFront();
            plugMinusLR.BringToFront();
            plugPlusRL.BringToFront();
        }

        protected override void SetPositionsPlugs(Form form)
        {
            form.Controls.Add(plugMinusLR);
            plugMinusLR.Top = picture.Top + picture.Height / 2 - plugMinusLR.Height / 2;
            plugMinusLR.Left = picture.Left - plugMinusLR.Width + 4;

            pointMinus = new Point(
                plugMinusLR.Left - 2,
                plugMinusLR.Top + plugMinusLR.Height / 2 - 1);

            form.Controls.Add(plugPlusRL);
            plugPlusRL.Top = picture.Top + picture.Height / 2 - plugMinusLR.Height / 2;
            plugPlusRL.Left = picture.Left + picture.Width - 4;

            pointPlus = new Point(
                plugPlusRL.Left + plugPlusRL.Width + 1,
                plugPlusRL.Top + plugMinusLR.Height / 2 - 1);

            plugMinusLR.BringToFront();
            plugPlusRL.BringToFront();
        }

        public double GetResistance(double volt) 
        {
            if (volt >= 0.0 && volt <= 2)
            {
                return 3.6;
            }
            else if (volt > 2 && volt <= 4)
            {
                return 5.7;
            }
            else if (volt > 4 && volt <= 6)
            {
                return 7.1;
            }
            else if (volt > 6 && volt <= 8)
            {
                return 8.2;
            }
            else if (volt > 8 && volt <= 10)
            {
                return 9.3;
            }
            else if (volt > 10 && volt <= 12)
            {
                return 10.1;
            }
            else if (volt > 12 && volt <= 14)
            {
                return 10.9;
            }
            else if (volt > 14 && volt <= 16)
            {
                return 11.6;
            }
            else if (volt > 16 && volt <= 18)
            {
                return 12.2;
            }
            else if (volt > 18 && volt <= 20)
            {
                return 12.9;
            }
            else if (volt > 20 && volt <= 22)
            {
                return 13.5;
            }
            else if (volt > 22 && volt <= 24)
            {
                return 14.1;
            }
            else if (volt > 24 && volt <= 26)
            {
                return 14.7;
            }
            else if (volt > 26 && volt <= 28)
            {
                return 15.2;
            }
            else if (volt > 28 && volt <= 30)
            {
                return 15.6;
            }
            else 
            {
                return 16.0;
            }
        }

        /// <summary>
        /// Метод включения лампы
        /// </summary>
        public void On() 
        {
            this.statusDevice = true;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\lamp\lamp_on.png");
        } 

        /// <summary>
        /// Метод выключения лампы
        /// </summary>
        public void Off()
        {
            this.statusDevice = false;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\lamp\lamp_off.png");
        }
    }
}
