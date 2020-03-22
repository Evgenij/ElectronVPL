using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Components
{
    static class GlobalData
    {
        public static Form MainForm = new Form();
        public static Font DigitalFont;
        public static PictureBox picture = new PictureBox();

        public static int TimePlugAnimation = 500;

        // Создание объектов для работы с отчетом и элементами цепи
        public static WorkWithReport workWithReport;
        public static WorkWithElements workWithElements = new WorkWithElements();

        // Создание объекта, для работы с файлом
        public static INIManager iniManager = new INIManager("iniFile.ini");

        // Создание временного объекта-источника для подключения элементов цепи
        public static Device deviceSource = new Device();
        public enum TypeElement
        {
            Ammeter,
            Voltmeter,
            Multimeter,
            Resistor,
            Conductor,
            Rheostat,
            VoltageSource,
            Capacitor,
            SingleSwitch,
            DoubleSwitch,
            Toggle,
            Lamp,
            Stopwatch
        }

        public static void ShowPicture(TypeElement typeElement, int x, int y) 
        {
            picture.SizeMode = PictureBoxSizeMode.AutoSize;
            MainForm.Controls.Add(picture);
            if (typeElement == TypeElement.Ammeter)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\ammeter.png");
            }
            else if (typeElement == TypeElement.Voltmeter) 
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\voltmeter.png");
            }
            else if (typeElement == TypeElement.Multimeter)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\multimeter.png");
            }
            else if (typeElement == TypeElement.Conductor)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\cond.png");
            }
            else if (typeElement == TypeElement.Resistor)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\resistor.png");
            }
            else if (typeElement == TypeElement.Rheostat)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\rheostat.png");
            }
            else if (typeElement == TypeElement.VoltageSource)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\volt_source.png");
            }
            else if (typeElement == TypeElement.Capacitor)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\capacitor.png");
            }
            else if (typeElement == TypeElement.SingleSwitch)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\s_switch.png");
            }
            else if (typeElement == TypeElement.DoubleSwitch)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\d_switch.png");
            }
            else if (typeElement == TypeElement.Toggle)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\toggle.png");
            }
            else if (typeElement == TypeElement.Lamp)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\lamp.png");
            }
            else if (typeElement == TypeElement.Stopwatch)
            {
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\for_add\stopwatch.png");
            }
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height - 10;
            picture.Show();
        }

        public static void HidePicture() 
        {
            picture.Hide();
        }

        // Множитель для корректного отображения значений стрелки амперметра и вольтметра
        public const double multiplierValues = 3.6;

        public const double pF = 1000000000000;
        public const double nF = 1000000000; 
        public const double mkF = 1000000;
        public const double mF = 1000;

        //значение электрической постоянной
        public const double e0 = 0.00000000000885; 

        public enum TypeCapacitor { Flat, Cylinder }
        public enum TypeConnectionCapacitors { Sequentially, Parallel }
        

        public static void SetGlobalForm(Form form)
        {
            MainForm = form;
        }

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
