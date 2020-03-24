using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Components
{
    partial class MainForm : Form
    {
        StudentManager studentManager;

        public MainForm()
        {
            InitializeComponent();
            
            GlobalData.SetGlobalForm(this);

            GlobalData.workWithReport = new WorkWithReport("Ермоленко", "Евгений", 1);
            studentManager = new StudentManager();
            studentManager.SetCountStudents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Elements.ammeter.Calculate(Elements.voltageSource.GetValue(), Elements.resistor[0].ReturnResistance());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Elements.voltmeter.Calculate(Elements.ammeter.GetValue(), Elements.resistor[0].ReturnResistance());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Записать значение по ключу age и name в секции main
            for (int i = 1; i <= 5; i++)
            {
                GlobalData.iniManager.WriteString("Student_id" + Convert.ToString(i), "surname", "null");
                GlobalData.iniManager.WriteString("Student_id" + Convert.ToString(i), "name", "null");
                GlobalData.iniManager.WriteString("Student_id" + Convert.ToString(i), "group", "null");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!GlobalData.iniManager.KeyExists("Section3", "name"))
            {
                MessageBox.Show("key not found...");
            }
            else
            {
                MessageBox.Show(GlobalData.iniManager.ReadString("Section3", "name"));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GlobalData.iniManager.DeleteKey("Section3", "name");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GlobalData.iniManager.DeleteSection("Section3");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            studentManager.Registration("123", "Ермоленко", "Евгений", "ИВТ-7", @"C:\...");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            studentManager.Authorization("Ермоленко", "123");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            studentManager.Deleting(studentManager.GetId());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            studentManager.Change(studentManager.GetId(), "null", "null", "null", "null", "null");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GlobalData.workWithReport.AddHeader("Ермоленко", "Евгений", "ИВТ-7");
            GlobalData.workWithReport.AddFooter();
            GlobalData.workWithReport.AddReportHead(
                "1", GlobalData.workWithReport.GetLabTheme(1),
                GlobalData.workWithReport.GetLabTarget(1)
                );
            GlobalData.workWithReport.AddСonclusion("Текст вывода аолтоаыволтаолдвытповатямлоав...");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //GlobalData.reportManager.AddActionStringToReport();
        }

        private void zeroitMetroSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (zeroitMetroSwitch1.Checked == false)
            {
                GlobalData.workWithReport.AddActionsToReport();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GlobalData.workWithReport.AddSection("Секция 1");
            GlobalData.workWithElements.AddChangesValue(0.1, 0.3, 0.3, 1);
            GlobalData.workWithReport.AddActionsToReport();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //multimeter.Calculate();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            GlobalData.workWithElements.AddAction(Elements.ammeter, ReportManager.TypeAction.Add);
            GlobalData.workWithReport.AddActionsToReport();
            string[] pole = new string[] { "first", "second", "third" };
            int[] values = new int[] { 1, 2, 3, 4, 5 };
            GlobalData.workWithReport.AddTable(pole);
            GlobalData.workWithReport.AddValuesToTable(values);

            GlobalData.workWithElements.AddAction(Elements.ammeter, ReportManager.TypeAction.Add);
            GlobalData.workWithReport.AddActionsToReport();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string[] questions = new string[] {
                "Вопрос №1",
                "Вопрос №2"
            };
            string[] answers = new string[] {
                "Ответ 1",
                "Ответ 2"
            };
            GlobalData.workWithReport.AddSection("Контрольные вопросы:");
            GlobalData.workWithReport.AddQuestionsToReport(questions, answers);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Elements.resistor[0] = new Resistor();
            Elements.resistor[1] = new Resistor();
            Elements.resistor[0].Visualization(this, 700, 150);

            //Graphics graphics = this.CreateGraphics();
            //graphics.DrawLine(new Pen(Color.Red, 3), 0, 0, 1000, 700);
            //graphics.Dispose();
        }                                                                                                          

        private void button21_Click(object sender, EventArgs e)
        {
            Elements.heatingArea = new HeatingArea();
            Elements.resistor[1].Visualization(this, 700, 300);
            Elements.heatingArea.DrawHeatingArea(this, Elements.resistor);
            GlobalData.workWithElements.AddAction(Elements.heatingArea, ReportManager.TypeAction.Add);
        }

        private void MainForm_MouseMove_1(object sender, MouseEventArgs e)
        {
            GlobalData.X = e.X;
            GlobalData.Y = e.Y;
            if (zeroitMetroSwitch1.Checked == true)
            {

                Design.SeleсtingPlace(GlobalData.X, GlobalData.Y);
            }


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(237, 249, 255);

            Elements.Type[] types = new Elements.Type[13] { 
                Elements.Type.Ammeter, 
                Elements.Type.Voltmeter,
                Elements.Type.Multimeter,
                Elements.Type.Resistor,
                Elements.Type.Conductor,
                Elements.Type.Rheostat,
                Elements.Type.VoltageSource,
                Elements.Type.Capacitor, 
                Elements.Type.SingleSwitch,
                Elements.Type.DoubleSwitch,
                Elements.Type.Toggle,
                Elements.Type.Lamp,
                Elements.Type.Stopwatch};

            Design.CreatePanel(pictureBox3, types);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox1, GlobalData.TimePlugAnimation);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox2, GlobalData.TimePlugAnimation);
        }

        //Graphics graphics;
        //SolidBrush brush = new SolidBrush(Color.FromArgb(65, 65, 65));

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Design.HidePicture();
            //graphics = CreateGraphics();
            //graphics.FillRectangle(brush, e.X - 15, e.Y - 5, 30, 10);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Design.ShowPicture();
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (zeroitMetroSwitch1.Checked == true)
            {
                if (Design.GetType() == Elements.Type.Ammeter)
                {
                    Elements.ammeter = new Ammeter();
                    Elements.ammeter.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.ammeter, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Voltmeter)
                {
                    Elements.voltmeter = new Voltmeter();
                    Elements.voltmeter.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.voltmeter, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Multimeter)
                {
                    Elements.multimeter = new Multimeter();
                    Elements.multimeter.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.multimeter, ReportManager.TypeAction.Add);
                    //Elements.multimeter.SetValue(Elements.capacitor.GetValue());
                }
                else if (Design.GetType() == Elements.Type.Resistor) 
                { 
                    Elements.resistor[0] = new Resistor();
                    Elements.resistor[0].Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.resistor[0], ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Rheostat)
                {
                    Elements.rheostat = new Rheostat();
                    Elements.rheostat.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.rheostat, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.VoltageSource)
                {
                    Elements.voltageSource = new VoltageSource();
                    Elements.voltageSource.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.voltageSource, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Capacitor)
                {
                    Elements.capacitor = new Capacitor();
                    Elements.capacitor.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.capacitor, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.SingleSwitch)
                {
                    Elements.singleSwitch = new SingleSwitch();
                    Elements.singleSwitch.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.singleSwitch, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.DoubleSwitch)
                {
                    Elements.doubleSwitch = new DoubleSwitch();
                    Elements.doubleSwitch.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.doubleSwitch, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Toggle)
                {
                    Elements.toggle = new Toggle();
                    Elements.toggle.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.toggle, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Lamp)
                {
                    Elements.lamp = new Lamp();
                    Elements.lamp.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.lamp, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Stopwatch)
                {
                    Elements.stopwatch = new Stopwatch();
                    Elements.stopwatch.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.stopwatch, ReportManager.TypeAction.Add);
                }
                else if (Design.GetType() == Elements.Type.Conductor)
                {
                    Elements.conductor = new Conductor();
                    Elements.conductor.Visualization(this, GlobalData.X, GlobalData.Y);
                    GlobalData.workWithElements.AddAction(Elements.conductor, ReportManager.TypeAction.Add);
                }
            }
        }
    }
}
