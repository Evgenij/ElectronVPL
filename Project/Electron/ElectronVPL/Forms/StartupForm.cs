using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    public partial class StartupForm : Form
    {
        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect,
                                                       int nTopRect,
                                                       int nRightRect,
                                                       int nBottomRect,
                                                       int nWidthEllipse,
                                                       int nHeightEllipse);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        void Form_Load(object sender, EventArgs e)
        {
            IntPtr hRgn = CreateRoundRectRgn(-1, -1, 777, 432, 103, 103);
            SetWindowRgn(this.Handle, hRgn, true);
        }

        Timer timerStartup = new Timer();
        Timer timerAutoriz = new Timer();
        Forms.Autorization autorization = new Forms.Autorization();


        Point p;// = new Point(this.Location.X, this.Location.Y - 30);
        double opacityStartup = 1.0;
        double opacityAutorization = 0.0;

        public StartupForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form_Load);
            pictureBox2.Parent = pictureBox1;
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;

            timerStartup.Interval = 20;
            timerStartup.Tick += Timer_Tick;
            timerAutoriz.Interval = 20;
            timerAutoriz.Tick += TimerAutoriz_Tick;
        }

        private void TimerAutoriz_Tick(object sender, EventArgs e)
        {
            if (autorization.Opacity != 1.0)
            {
                autorization.Opacity = opacityAutorization;
                opacityAutorization += 0.1;
            }
            else
            {
                timerAutoriz.Enabled = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity != 0.0)
            {
                p = new Point(this.Location.X, this.Location.Y - 10);
                this.Location = p;
                this.Opacity = opacityStartup;
                opacityStartup -= 0.1;
            }
            else 
            {
                timerStartup.Enabled = false;
                timerAutoriz.Enabled = true;
                this.Hide();
                autorization.Show();
            }

        }

        int score = 0;
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                score++;
            }
            if (score == 3)
            {
                timerStartup.Enabled = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
