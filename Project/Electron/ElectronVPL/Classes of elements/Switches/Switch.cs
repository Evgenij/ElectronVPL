using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    class Switch : Device
    {
        protected enum Position { Top, Center, Bottom, Left, Right }
        protected Position position;

        // Контакты для переключателей
        protected PictureBox contactTop;
        protected PictureBox contactBottom;
    }
}
