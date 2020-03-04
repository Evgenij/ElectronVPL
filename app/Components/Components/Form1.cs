using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    public partial class MainForm : Form
    {
        Ammeter ammeter;
        Voltmeter voltmeter;
        Multimeter multimeter;
        Resistor[] resistor = new Resistor[2];
        Conductor conductor;
        Rheostat rheostat;
        VoltageSource voltageSource;
        Capacitor capacitor;
        SingleSwitch singleSwitch;
        DoubleSwitch doubleSwitch;
        Toggle toggle;
        HeatingArea heatingArea;
        Lamp lamp;
        Stopwatch stopwatch;

        StudentManager studentManager;
        
        int x, y;

        public MainForm()
        {
            InitializeComponent();
            voltmeter = new Voltmeter();
            multimeter = new Multimeter();
            for (int i = 0; i < 2; ++i) 
            {
                resistor[i] = new Resistor();
            }
            conductor = new Conductor();
            rheostat = new Rheostat();
            voltageSource = new VoltageSource();
            capacitor = new Capacitor();
            singleSwitch = new SingleSwitch();
            doubleSwitch = new DoubleSwitch();
            toggle = new Toggle();
            heatingArea = new HeatingArea();
            lamp = new Lamp();
            stopwatch = new Stopwatch();

            studentManager = new StudentManager();
            GlobalData.workWithReport = new WorkWithReport("Ермоленко", "Евгений", 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ammeter.Calculate(voltageSource.GetValue(), resistor[0].ReturnResistance());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            voltmeter.Calculate(ammeter.GetValue(), resistor[0].ReturnResistance());
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
            studentManager.Registration("123","Ермоленко", "Евгений", "ИВТ-7", @"C:\...");
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
            studentManager.Change(studentManager.GetId(),"null", "null", "null", "null", "null");
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
            GlobalData.workWithElements.AddAction(ammeter, ReportManager.TypeAction.Add);
            GlobalData.workWithReport.AddActionsToReport();
            string[] pole = new string[] { "first", "second", "third" };
            int[] values = new int[] { 1, 2, 3, 4, 5 };
            GlobalData.workWithReport.AddTable(pole);
            GlobalData.workWithReport.AddValuesToTable(values);

            GlobalData.workWithElements.AddAction(ammeter, ReportManager.TypeAction.Add);
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

        private void button19_Click(object sender, EventArgs e)
        {
            if (ammeter != null)
            {
                MessageBox.Show("1");
            }
            else 
            {
                MessageBox.Show("0");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            resistor[0].Visualization(this, 500, 150);

            //Graphics graphics = this.CreateGraphics();
            //graphics.DrawLine(new Pen(Color.Red, 3), 0, 0, 1000, 700);
            //graphics.Dispose();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            resistor[1].Visualization(this, 500, 250);
            heatingArea.DrawHeatingArea(this, resistor);
            GlobalData.workWithElements.AddAction(heatingArea, ReportManager.TypeAction.Add);
        }

        private void MainForm_MouseMove_1(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 250, 255);
           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            pictureBox2.Width = 10;
            graphics.DrawLine(
                new Pen(Color.Black, 3),
                pictureBox2.Location.X - 10,
                pictureBox2.Location.Y + 13,
                pictureBox1.Location.X+170,
                pictureBox2.Location.Y+13);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox2,960);
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (zeroitMetroSwitch1.Checked == true)
            {
                if (radioButton1.Checked == true)
                {
                    ammeter = new Ammeter();
                    ammeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(ammeter, ReportManager.TypeAction.Add);
                }
                else if (radioButton2.Checked == true)
                {
                    voltmeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(voltmeter, ReportManager.TypeAction.Add);
                }
                else if (radioButton3.Checked == true)
                {
                    multimeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(multimeter, ReportManager.TypeAction.Add);
                    multimeter.SetValue(capacitor.GetValue());
                }
                else if (radioButton4.Checked == true)
                {
                    resistor[0].Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(resistor, ReportManager.TypeAction.Add);
                }
                else if (radioButton5.Checked == true)
                {
                    rheostat.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(rheostat, ReportManager.TypeAction.Add);
                }
                else if (radioButton6.Checked == true)
                {
                    voltageSource.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(voltageSource, ReportManager.TypeAction.Add);
                }
                else if (radioButton7.Checked == true)
                {
                    capacitor.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(capacitor, ReportManager.TypeAction.Add);
                }
                else if (radioButton8.Checked == true)
                {
                    singleSwitch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(singleSwitch, ReportManager.TypeAction.Add);
                }
                else if (radioButton9.Checked == true)
                {
                    doubleSwitch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(doubleSwitch, ReportManager.TypeAction.Add);
                }
                else if (radioButton10.Checked == true)
                {
                    toggle.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(toggle, ReportManager.TypeAction.Add);
                }
                else if (radioButton12.Checked == true)
                {
                    lamp.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(lamp, ReportManager.TypeAction.Add);
                }
                else if (radioButton13.Checked == true)
                {
                    stopwatch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(stopwatch, ReportManager.TypeAction.Add);
                }
                else if (radioButton14.Checked == true)
                {
                    conductor.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(conductor, ReportManager.TypeAction.Add);
                }
            }
        }
    }
}
