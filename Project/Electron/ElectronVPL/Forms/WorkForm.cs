using ElectronVPL;
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

namespace ElectronVPL.Project_file
{
    public partial class WorkForm : Form
    {
        public WorkForm()
        {
            InitializeComponent();
            GlobalData.SetGlobalForm(this);
            this.Load += new EventHandler(Design.Form_Load);
            this.BackColor = Color.FromArgb(242,251,255);

            GlobalData.workWithReport = new WorkWithReport("Ермоленко", "Евгений", 1);

            LaboratoryLabs.ConfiguringForm(1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalData.workWithReport.AddHeader("1","2","3");
            GlobalData.workWithReport.OpenFile();
        }

        private void WorkForm_Click(object sender, EventArgs e)
        {
            Design.HidePicture();
        }

        private void WorkForm_MouseMove(object sender, MouseEventArgs e)
        {
            GlobalData.X = e.X;
            GlobalData.Y = e.Y;
            Design.SeleсtingPlace(GlobalData.X, GlobalData.Y);
        }
    }
}
