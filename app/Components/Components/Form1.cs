using System;
using System.Drawing;
using System.Runtime.InteropServices;
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
            Elements.heatingArea = new HeatingArea();
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
            this.BackColor = Color.FromArgb(246, 252, 255);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox2, GlobalData.TimePlugAnimation) ;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox1, GlobalData.TimePlugAnimation);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox3, GlobalData.TimePlugAnimation);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Design.Animate(pictureBox4, GlobalData.TimePlugAnimation);
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            //if(e.Button == MouseButtons.Left) 
            //{
            //    var color = GetColor(new Point(x, y));
            //    MessageBox.Show(color.ToString());
            //}
            //else if (e.Button == MouseButtons.Right) 
            //{
            //    MessageBox.Show(GetPixelColor(new Point(x,y)).ToString());
            //}
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Elements.ammeter.Delete(Elements.ammeter);
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (zeroitMetroSwitch1.Checked == true)
            {
                if (radioButton1.Checked == true)
                {
                    Elements.ammeter = new Ammeter();
                    Elements.ammeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.ammeter, ReportManager.TypeAction.Add);
                }
                else if (radioButton2.Checked == true)
                {
                    Elements.voltmeter = new Voltmeter();
                    Elements.voltmeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.voltmeter, ReportManager.TypeAction.Add);
                }
                else if (radioButton3.Checked == true)
                {
                    Elements.multimeter = new Multimeter();
                    Elements.multimeter.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.multimeter, ReportManager.TypeAction.Add);
                    Elements.multimeter.SetValue(Elements.capacitor.GetValue());
                }
                else if (radioButton4.Checked == true)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        Elements.resistor[i] = new Resistor();
                    }
                    Elements.resistor[0].Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.resistor, ReportManager.TypeAction.Add);
                }
                else if (radioButton5.Checked == true)
                {
                    Elements.rheostat = new Rheostat();
                    Elements.rheostat.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.rheostat, ReportManager.TypeAction.Add);
                }
                else if (radioButton6.Checked == true)
                {
                    Elements.voltageSource = new VoltageSource();
                    Elements.voltageSource.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.voltageSource, ReportManager.TypeAction.Add);
                }
                else if (radioButton7.Checked == true)
                {
                    Elements.capacitor = new Capacitor();
                    Elements.capacitor.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.capacitor, ReportManager.TypeAction.Add);
                }
                else if (radioButton8.Checked == true)
                {
                    Elements.singleSwitch = new SingleSwitch();
                    Elements.singleSwitch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.singleSwitch, ReportManager.TypeAction.Add);
                }
                else if (radioButton9.Checked == true)
                {
                    Elements.doubleSwitch = new DoubleSwitch();
                    Elements.doubleSwitch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.doubleSwitch, ReportManager.TypeAction.Add);
                }
                else if (radioButton10.Checked == true)
                {
                    Elements.toggle = new Toggle();
                    Elements.toggle.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.toggle, ReportManager.TypeAction.Add);
                }
                else if (radioButton12.Checked == true)
                {
                    Elements.lamp = new Lamp();
                    Elements.lamp.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.lamp, ReportManager.TypeAction.Add);
                }
                else if (radioButton13.Checked == true)
                {
                    Elements.stopwatch = new Stopwatch();
                    Elements.stopwatch.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.stopwatch, ReportManager.TypeAction.Add);
                }
                else if (radioButton14.Checked == true)
                {
                    Elements.conductor = new Conductor();
                    Elements.conductor.Visualization(this, x, y);
                    GlobalData.workWithElements.AddAction(Elements.conductor, ReportManager.TypeAction.Add);
                }
            }
        }
    }
}
