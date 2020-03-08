using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    partial class MainForm : Form
    {
        int x, y;
        StudentManager studentManager;

        public MainForm()
        {
            InitializeComponent();
            GlobalData.SetGlobalForm(this);
            Elements.ammeter = new Ammeter();
            Elements.voltmeter = new Voltmeter();
            Elements.multimeter = new Multimeter();
            for (int i = 0; i < 2; ++i) 
            {
                Elements.resistor[i] = new Resistor();
            }
            Elements.conductor = new Conductor();
            Elements.rheostat = new Rheostat();
            Elements.voltageSource = new VoltageSource();
            Elements.capacitor = new Capacitor();
            Elements.singleSwitch = new SingleSwitch();
            Elements.doubleSwitch = new DoubleSwitch();
            Elements.toggle = new Toggle();
            Elements.heatingArea = new HeatingArea();
            Elements.lamp = new Lamp();
            Elements.stopwatch = new Stopwatch();

            GlobalData.workWithReport = new WorkWithReport("Ермоленко", "Евгений", 1);
            studentManager = new StudentManager();
            studentManager.SetCountStudents();

            Elements.ammeter.Visualization(this, 300, 150);
            Elements.voltmeter.Visualization(this, 530, 200);
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

        private void button19_Click(object sender, EventArgs e)
        {
            if (Elements.ammeter != null)
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
            Elements.resistor[0].Visualization(this, 500, 150);

            //Graphics graphics = this.CreateGraphics();
            //graphics.DrawLine(new Pen(Color.Red, 3), 0, 0, 1000, 700);
            //graphics.Dispose();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Elements.resistor[1].Visualization(this, 500, 250);
            Elements.heatingArea.DrawHeatingArea(this, Elements.resistor);
            GlobalData.workWithElements.AddAction(Elements.heatingArea, ReportManager.TypeAction.Add);
        }

        private void MainForm_MouseMove_1(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Elements.stopwatch.Start();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Elements.stopwatch.Stop();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 250, 255);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox2, 950);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox1, 950);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox3, 950);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox4, 950);
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) 
            {
                //Design.SetPointPlus(x, y);
            }
            else if (e.Button == MouseButtons.Right) 
            {
                //Design.SetPointMinus(x, y);
                //Design.ConnectionElements(ElementsChain.ammeter, ElementsChain.voltmeter);
            }
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (zeroitMetroSwitch1.Checked == true)
            {
                if (radioButton1.Checked == true)
                {
                    Elements.ammeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.ammeter, ReportManager.TypeAction.Add);
                }
                else if (radioButton2.Checked == true)
                {
                    Elements.voltmeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.voltmeter, ReportManager.TypeAction.Add);
                }
                else if (radioButton3.Checked == true)
                {
                    Elements.multimeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.multimeter, ReportManager.TypeAction.Add);
                    Elements.multimeter.SetValue(Elements.capacitor.GetValue());
                }
                else if (radioButton4.Checked == true)
                {
                    Elements.resistor[0].Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.resistor, ReportManager.TypeAction.Add);
                }
                else if (radioButton5.Checked == true)
                {
                    Elements.rheostat.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.rheostat, ReportManager.TypeAction.Add);
                }
                else if (radioButton6.Checked == true)
                {
                    Elements.voltageSource.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.voltageSource, ReportManager.TypeAction.Add);
                }
                else if (radioButton7.Checked == true)
                {
                    Elements.capacitor.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.capacitor, ReportManager.TypeAction.Add);
                }
                else if (radioButton8.Checked == true)
                {
                    Elements.singleSwitch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.singleSwitch, ReportManager.TypeAction.Add);
                }
                else if (radioButton9.Checked == true)
                {
                    Elements.doubleSwitch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.doubleSwitch, ReportManager.TypeAction.Add);
                }
                else if (radioButton10.Checked == true)
                {
                    Elements.toggle.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.toggle, ReportManager.TypeAction.Add);
                }
                else if (radioButton12.Checked == true)
                {
                    Elements.lamp.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.lamp, ReportManager.TypeAction.Add);
                }
                else if (radioButton13.Checked == true)
                {
                    Elements.stopwatch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.stopwatch, ReportManager.TypeAction.Add);
                }
                else if (radioButton14.Checked == true)
                {
                    Elements.conductor.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.conductor, ReportManager.TypeAction.Add);
                }
            }
        }
    }
}
