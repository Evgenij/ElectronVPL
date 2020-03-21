using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Toggle : Device, IVisualization
    {
        private PictureBox handle;
        private bool status;


        public Toggle()
        {
            handle = new PictureBox();
            status = false;

            picture.Width = 166;
            picture.Height = 85;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\toggle\toggle_off.png");

            handle.Width = 23;
            handle.Height = 19;
            handle.Left = 84;
            handle.Top = 15;
            handle.Cursor = Cursors.Hand;
            handle.BackColor = Color.Transparent;
            handle.Cursor = Cursors.PanSouth;
            handle.Click += Handle_Click;

            contactRight.Width = 12;
            contactRight.Height = 35;
            contactRight.Left = picture.Width - 12;
            contactRight.Top = 25;

            contactLeft.Width = 12;
            contactLeft.Height = 35;
            contactLeft.Left = 0;
            contactLeft.Top = 25;
        }

        public void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;

            SetPositionControls(62, 65, 83, 65);

            picture.Controls.Add(handle);

            // код создания контактов для подключения
            picture.Controls.Add(contactLeft);
            picture.Controls.Add(contactRight);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form);

            // распределение составляющих компонента по слоям

            handle.BringToFront();
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
                plugMinusLR.Left - 1,
                plugMinusLR.Top + plugMinusLR.Height / 2 - 2);

            form.Controls.Add(plugPlusRL);
            plugPlusRL.Top = picture.Top + picture.Height / 2 - plugMinusLR.Height / 2;
            plugPlusRL.Left = picture.Left + picture.Width - 4;

            pointPlus = new Point(
                plugPlusRL.Left + plugPlusRL.Width,
                plugPlusRL.Top + plugMinusLR.Height / 2 - 2);

            plugMinusLR.BringToFront();
            plugPlusRL.BringToFront();
        }

        private void Handle_Click(object sender, EventArgs e)
        {
            if (status == false)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\toggle\toggle_on.png");
                handle.Left = 88;
                handle.Top = 28;
                status = true;
                handle.Cursor = Cursors.PanNorth;
                GlobalData.workWithElements.AddChangesValue(
                    this, 
                    ReportManager.TypeChanges.On);
            }
            else
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\toggle\toggle_off.png");
                handle.Left = 84;
                handle.Top = 15;
                status = false;
                handle.Cursor = Cursors.PanSouth;
                GlobalData.workWithElements.AddChangesValue(
                    this,
                    ReportManager.TypeChanges.Off);
            }
        }
    }
}
