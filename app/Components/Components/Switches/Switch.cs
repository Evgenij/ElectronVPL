using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Switch : Device, IVisualization
    {
        protected enum Position { Top, Center, Bottom, Left, Right }
        protected Position position;

        // Контакты для переключателей
        protected PictureBox contactTop;
        protected PictureBox contactBottom;

        protected Switch() 
        {
            picture = new PictureBox();
        }

        public virtual void Visualization(Form form, int x, int y)
        {
            contactLeft.BringToFront();
            contactTop.BringToFront();
            contactBottom.BringToFront();
            plugPlusDU.BringToFront();
            plugPlusLR.BringToFront();
            plugPlusUD.BringToFront();
        }
    }
}
