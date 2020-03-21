using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components 
{
    class Rheostat : ResistanceDevice
    {
        //компоненты формы для создания элемента цепи
        private PictureBox tablo;
        private PictureBox probe;
        private Zeroit.Framework.Metro.ZeroitMetroTrackbar slider; 

        public Rheostat()
        {
            slider = new Zeroit.Framework.Metro.ZeroitMetroTrackbar();
            tablo = new PictureBox();
            probe = new PictureBox();

            picture.Width = 188;
            picture.Height = 103;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\rheostat\rheostat.png");

            slider.Top = 36;
            slider.Left = 37;
            slider.Minimum = 1;
            slider.Maximum = 50;
            slider.Value = 0;
            slider.BorderColor = Color.DimGray;
            slider.RailWidth = 3;
            slider.SliderWidth = 17;
            slider.LeftColor = Color.Silver;
            slider.RightColor = Color.Silver;
            slider.HoverColor = Color.DimGray;
            slider.GradientColor = Color.Silver;
            slider.SliderColor = Color.DimGray;
            slider.Cursor = Cursors.Hand;
            slider.Width = 113;
            slider.Height = 9;
            slider.Scroll += Slider_Scroll;
            slider.MouseUp += Slider_MouseUp;

            tablo.Top = 10;
            tablo.SizeMode = PictureBoxSizeMode.AutoSize;
            tablo.Left = slider.Left - 15 + slider.Value;
            tablo.BackColor = Color.Transparent;
            tablo.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\rheostat\tablo.png");

            probe.Top = 42;
            probe.Width = 7;
            probe.SizeMode = PictureBoxSizeMode.AutoSize;
            probe.Left = tablo.Left + tablo.Width / 2 - probe.Width / 2 - 1;
            probe.BackColor = Color.Transparent;
            probe.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\rheostat\probe.png");

            GlobalData.LoadFont(12);  //метод загрузки шрифта
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = tablo.Left + 5;
            labelValue.Top = 14;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 37;
            labelValue.ForeColor = Color.Orange;
            labelValue.TextAlign = HorizontalAlignment.Center;

            contactMinus.Width = 34;
            contactMinus.Height = 12;
            contactMinus.Left = 60;
            contactMinus.Top = 92;

            contactPlus.Width = 34;
            contactPlus.Height = 12;
            contactPlus.Left = 94;
            contactPlus.Top = 92;

            labelValue.Text = "1";
            this.resistanceValue = 1;
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            
            SetPositionControls(3, 81, 164, 81);

            picture.Controls.Add(slider);
            picture.Controls.Add(tablo);
            picture.Controls.Add(probe);
            picture.Controls.Add(labelValue);
            picture.Controls.Add(contactMinus);
            picture.Controls.Add(contactPlus);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form, 66, 95);

            // распределение составляющих компонента по слоям

            labelValue.BringToFront();
            contactMinus.BringToFront();
            contactPlus.BringToFront();
            plugMinusDU.BringToFront();
            plugPlusDU.BringToFront();
            pictureDelete.BringToFront();
            pictureMove.BringToFront();
        }

        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalData.workWithElements.AddChangesValue(
                   this,
                   this.resistanceValue);
        }

        private void Slider_Scroll(object sender, Zeroit.Framework.Metro.ZeroitMetroTrackbar.TrackbarEventArgs e)
        {
            tablo.Left = Convert.ToInt32((slider.Left - 16 + slider.Value * 2) - 1.4);
            probe.Left = Convert.ToInt32(tablo.Left + tablo.Width / 2 - probe.Width / 2 - 1);
            labelValue.Left = tablo.Left + 5;
            labelValue.Text = Convert.ToString(slider.Value);
            this.resistanceValue = slider.Value;
        }
    }
}
