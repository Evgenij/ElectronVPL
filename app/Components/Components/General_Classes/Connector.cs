using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Connector : IVisualization
    {
        private PictureBox picture;

        public Connector() 
        {
            picture = new PictureBox();
            picture.Width = 40;
            picture.Height = 10;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\connector\conn.png");
        }

        public void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            form.Controls.Add(picture);
        }
    }
}
