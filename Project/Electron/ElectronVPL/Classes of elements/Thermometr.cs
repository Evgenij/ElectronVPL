using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    class Thermometr : Device
    {
        private PictureBox pictureThermometr;
        private Zeroit.Framework.Metro.ZeroitMetroProgressbar scale;
        private string grad = "°С";
        private int temperature = 0;
        int old_temperature = 0;

        public Thermometr(Form form,int x, int y) 
        {
            scale = new Zeroit.Framework.Metro.ZeroitMetroProgressbar();
            pictureThermometr = new PictureBox();
            labelValue = new TextBox();

            pictureThermometr.Width = 50;
            pictureThermometr.Height = 274;
            pictureThermometr.Left = x;
            pictureThermometr.Top = y;
            pictureThermometr.BackColor = Color.Transparent;
            pictureThermometr.MouseWheel += MouseWheel;
            pictureThermometr.MouseHover += MouseHover;
            pictureThermometr.MouseLeave += PictureThermometr_MouseLeave;
            pictureThermometr.MouseEnter += PictureThermometr_MouseEnter;
            pictureThermometr.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\heating_area\thermometr.png");

            scale.Left = pictureThermometr.Left + 12;
            scale.Top = y + 39;
            scale.Width = 220;
            scale.Height = 7;
            scale.Maximum = 80;
            scale.Value = 80;
            scale.DefaultColor = Color.Red;
            scale.ProgressColor = Color.White;
            scale.GradientColor = Color.White;
            scale.Orientation = Orientation.Vertical;
            scale.MouseWheel += MouseWheel;
            scale.MouseHover += MouseHover;

            GlobalData.LoadFont(14);  //метод загрузки шрифта
            labelValue.Text = "0" + grad;
            labelValue.ReadOnly = true;
            labelValue.TabStop = false;
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 5;
            labelValue.Top = 5;
            labelValue.BorderStyle = BorderStyle.None;
            labelValue.BackColor = Color.DarkOrange;
            labelValue.Width = 40;
            labelValue.ForeColor = Color.Maroon;
            labelValue.TextAlign = HorizontalAlignment.Center;
            labelValue.Cursor = Cursors.Hand;
            labelValue.MouseMove += LabelValue_MouseMove;
            labelValue.TextChanged += LabelValue_TextChanged;
            pictureThermometr.Controls.Add(labelValue);

            form.Controls.Add(scale);
            form.Controls.Add(pictureThermometr);
        }

        private void PictureThermometr_MouseEnter(object sender, EventArgs e)
        {
            old_temperature = temperature;
        }

        private void PictureThermometr_MouseLeave(object sender, EventArgs e)
        {
            if (temperature != old_temperature)
            {
                GlobalData.workWithElements.AddChangesValue(this, temperature);
            }
        }

        private void MouseHover(object sender, EventArgs e)
        {
            pictureThermometr.Cursor = Cursors.SizeNS;
            scale.Cursor = Cursors.SizeNS;
        }

        private void MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (scale.Value != 0)
                {
                    scale.Value--;
                }
            }
            else
            {
                if (scale.Value != 80)
                {
                    scale.Value++;
                }
            }
            temperature = 80 - scale.Value;
            labelValue.Text = Convert.ToString(temperature) + grad;
        }

        //метод для отключения выделения текста в TextBox компонента
        private void LabelValue_TextChanged(object sender, EventArgs e)
        {
            labelValue.SelectionLength = 0;
        }

        //метод для отключения выделения текста в TextBox компонента
        private void LabelValue_MouseMove(object sender, MouseEventArgs e)
        {
            labelValue.SelectionLength = 0;
        }
    }
}
