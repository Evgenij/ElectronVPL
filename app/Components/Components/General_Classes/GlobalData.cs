using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Components
{
    static class GlobalData
    {                                             
        public static Font DigitalFont;
        public const double multiplierValues = 3.6;

        public const double pF = 1000000000000;
        public const double nF = 1000000000; 
        public const double mkF = 1000000;
        public const double mF = 1000;

        //значение электрической постоянной
        public const double e0 = 0.00000000000885; 

        public enum TypeCapacitor { Flat, Cylinder }
        public enum TypeConnectionCapacitors { Sequentially, Parallel }
        public static WorkWithReport workWithReport;
        public static WorkWithElements workWithElements = new WorkWithElements();

        //Создание объекта, для работы с файлом
        public static INIManager iniManager = new INIManager("iniFile.ini");

        public static void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (e.KeyChar == ',') || (e.KeyChar == 8))
            {
                return;
            }
            e.Handled = true;
        }

        /// <summary>
        /// Загружает шрифт
        /// </summary>
        /// <param name="size">Размер шрифта</param>
        public static void LoadFont(int size)
        {
            PrivateFontCollection font = new PrivateFontCollection();
            font.AddFontFile("digital.ttf");
            DigitalFont = new Font(font.Families[0], size, FontStyle.Bold);
        }
    }
}
