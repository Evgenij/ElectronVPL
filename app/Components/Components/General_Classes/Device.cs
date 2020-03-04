using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Device
    {
        protected bool statusDevice;
        protected double Value { get; set; }
        protected int X { get; set; }
        protected int Y { get; set; }


        protected PictureBox picture;
        protected TextBox labelValue;
        protected PictureBox contactMinus;
        protected PictureBox contactPlus;
    }
}
