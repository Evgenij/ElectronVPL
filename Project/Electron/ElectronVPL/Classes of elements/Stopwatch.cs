using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    class Stopwatch : Device
    {
        //компоненты формы для создания амперметра
        private CircleAnglePicker arrow;
        private Timer timer;
        private int seconds = 0;
        private int minutes = 0;

        public Stopwatch() 
        {
            arrow = new CircleAnglePicker();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\stopwatch\stopwatch.png");

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
        }

        //метод отображения компонента на форме
        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height - 10;

            SetPositionControls(0, picture.Height - 20, picture.Width - 20, picture.Height - 20);

            picture.Controls.Add(arrow);
            picture.Controls.Add(labelValue);

            picture.SendToBack();
            labelValue.BringToFront();
            arrow.BringToFront();
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

        public void Reset() 
        {
            minutes = 0;
            seconds = 0;
            arrow.Value = 90;
            labelValue.Text = Convert.ToString(seconds) + "с";
        }

        public string GetTime() 
        {
            if (minutes != 0)
            {
                return Convert.ToString(minutes) + " мин " + Convert.ToString(seconds) + " сек";
            }
            else 
            {
                return Convert.ToString(seconds) + " сек";
            }
        }

        public void Start() 
        {
            timer.Enabled = true;
        }

        public void Stop()
        {
            timer.Enabled = false;
            GlobalData.workWithElements.AddChangesValue(this, GetTime());
        }

    }
}
