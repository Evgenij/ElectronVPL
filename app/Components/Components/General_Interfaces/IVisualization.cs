using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    interface IVisualization
    {
        /// <summary>
        /// Метод отображения элемента на форме
        /// </summary>
        /// <param name="form">Форма на которой будет отображен элемент</param>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        void Visualization(Form form, int x, int y);
    }
}
