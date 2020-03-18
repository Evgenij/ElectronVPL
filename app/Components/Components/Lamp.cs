﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Lamp : Device, IVisualization
    {
        public void Visualization(Form form, int x, int y)
        {
            picture.Width = 107;
            picture.Height = 103;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\lamp\lamp_off.png");

            // код создания контактов для подключения

            contactLeft.Width = 12;
            contactLeft.Height = 35;
            contactLeft.Left = 0;
            contactLeft.Top = 34;
            picture.Controls.Add(contactLeft);

            contactRight.Width = 12;
            contactRight.Height = 35;
            contactRight.Left = picture.Width - 12;
            contactRight.Top = 34;
            picture.Controls.Add(contactRight);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form);

            // распределение составляющих компонента по слоям

            picture.SendToBack();
            contactLeft.BringToFront();
            contactRight.BringToFront();
            plugMinusLR.BringToFront();
            plugPlusRL.BringToFront();
            form.Controls.Add(picture);
        }

        private void SetPositionsPlugs(Form form)
        {
            form.Controls.Add(plugMinusLR);
            plugMinusLR.Top = picture.Top + picture.Height / 2 - plugMinusLR.Height / 2;
            plugMinusLR.Left = picture.Left - plugMinusLR.Width + 4;

            pointMinus = new Point(
                plugMinusLR.Left - 1,
                plugMinusLR.Top + plugMinusLR.Height / 2 - 2);

            form.Controls.Add(plugPlusRL);
            plugPlusRL.Top = picture.Top + picture.Height / 2 - plugMinusLR.Height / 2;
            plugPlusRL.Left = picture.Left + picture.Width - 4;

            pointPlus = new Point(
                plugPlusRL.Left + plugPlusRL.Width,
                plugPlusRL.Top + plugMinusLR.Height / 2 - 2);
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
