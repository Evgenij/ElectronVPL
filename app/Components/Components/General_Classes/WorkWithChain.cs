using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    static class WorkWithChain
    {
        public static Form MainForm = new Form();

        public static void SetForm(Form form)
        {
            MainForm = form;
        }
    }
}
