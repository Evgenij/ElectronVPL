using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Stopwatch : IVisualization
    {
        //компоненты формы для создания амперметра
        private PictureBox picture;
        private TextBox labelValue;
        private CircleAnglePicker arrow;
        private Timer timer;
        private int seconds = 0;
        private int minutes = 0;

        public Stopwatch() 
        {
            picture = new PictureBox();
            labelValue = new TextBox();
            arrow = new CircleAnglePicker();
            timer = new Timer();

            timer.Enabled = true;
        }

        //метод отображения компонента на форме
        public void Visualization(Form form, int x, int y)
        {
            picture.Width = 85;
            picture.Height = 124;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.BackColor = Color.Transparent;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\stopwatch\stopwatch.png");
            form.Controls.Add(picture);

            arrow.Value = 90;
            arrow.Parent = picture;
            arrow.Enabled = false;
            arrow.Height = 45;
            arrow.Width = 45;
            arrow.BackColor = Color.Transparent;
            arrow.Left = 20;
            arrow.Top = 56;
            arrow.Parent = picture;
            arrow.CircleColor = Color.Transparent;
            picture.Controls.Add(arrow);

            GlobalData.LoadFont(13);  //метод загрузки шрифта
            labelValue.ReadOnly = true;
            labelValue.TabStop = false;
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 19;
            labelValue.Top = 23;
            labelValue.BorderStyle = BorderStyle.None;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 47;
            labelValue.ForeColor = Color.DodgerBlue;
            labelValue.TextAlign = HorizontalAlignment.Center;
            labelValue.Cursor = Cursors.Hand;
            picture.Controls.Add(labelValue);

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            picture.SendToBack();
            labelValue.BringToFront();
            arrow.BringToFront();
            form.Controls.Add(picture);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            arrow.Value = arrow.Value - 6;

            if (seconds == 61)
            {
                seconds = 0;
                minutes++;
            }
            else if (minutes > 0) 
            {
                labelValue.Text = Convert.ToString(minutes) + ":" + Convert.ToString(seconds) + "м";
            }
            else
            {
                labelValue.Text = Convert.ToString(seconds) + "с";
            }
        }
    }
}
