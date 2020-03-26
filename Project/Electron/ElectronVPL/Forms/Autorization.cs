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

namespace ElectronVPL.Forms
{
    public partial class Autorization : Form
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
            IntPtr hRgn = CreateRoundRectRgn(-1, -1, 777, 432, 82, 82);
            SetWindowRgn(this.Handle, hRgn, true);
        }

        public Autorization()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form_Load);
            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;
            pictureBox4.Parent = pictureBox1;
            pictureBox6.Parent = pictureBox1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (((PictureBox)sender).AccessibleName == "off")
            {
                ((PictureBox)sender).AccessibleName = "on";
                ((PictureBox)sender).Image = Properties.Resources.ellipse_on;

            }
            else
            {
                ((PictureBox)sender).AccessibleName = "off";
                ((PictureBox)sender).Image = Properties.Resources.ellipse_off;
            }
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e) 
        { 
            ((PictureBox)sender).Image = Properties.Resources.reg_butt_on;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.reg_butt_off;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.enter_on;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.enter_off;
        }
    }
}
