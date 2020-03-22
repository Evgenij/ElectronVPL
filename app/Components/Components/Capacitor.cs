using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class Capacitor : Device
    {
        //компоненты формы для создания элемента цепи
        private GlobalData.TypeCapacitor typeCapacitor;
        private GlobalData.TypeConnectionCapacitors typeConnectionCapacitors;

        private TextBox textBoxS;
        private TextBox textBoxFlatE;
        private TextBox textBoxCylE;
        private TextBox textBoxD;
        private TextBox textBoxR1;
        private TextBox textBoxR2;
        private TextBox textBoxL;
        private PictureBox pictureValuePlus;
        private PictureBox pictureValueMinus;
        private PictureBox pictureSeq;
        private PictureBox picturePar;
        private PictureBox pictureGearFlat;
        private PictureBox pictureGearCylinder;
        private PictureBox picturePanelFlat;
        private PictureBox picturePanelCyl;
        private Zeroit.Framework.Metro.ZeroitMetroCheckCircle checkCircleFlat;
        private Zeroit.Framework.Metro.ZeroitMetroCheckCircle checkCircleCylinder;
        private Zeroit.Framework.Metro.ZeroitMetroCheckCircle.MainColorScheme colorScheme;

        private int countCapacitors = 1;
        private double capacity;

        // значение относительной диэлектрической проницаемости
        private double E;

        // значения плоского конденсатора
        // площадь пластин конденсатора
        private double S;
        // расстояние между пластинами
        private double d;

        //значения цилиндрического конденсатора
        // значение внутреннего радиуса конденсатора
        private double R1;
        // значение внешнего радиуса конденсатора
        private double R2;
        // значение высоты конденсатора
        private double l;

        public Capacitor()
        {
            textBoxFlatE = new TextBox();
            textBoxCylE = new TextBox();
            textBoxS = new TextBox(); ;
            textBoxD = new TextBox(); ;
            textBoxR1 = new TextBox(); ;
            textBoxR2 = new TextBox(); ;
            textBoxL = new TextBox(); ;

            pictureValuePlus = new PictureBox();
            pictureValueMinus = new PictureBox();
            pictureSeq = new PictureBox();
            picturePar = new PictureBox();
            pictureGearFlat = new PictureBox();
            pictureGearCylinder = new PictureBox();
            picturePanelFlat = new PictureBox();
            picturePanelCyl = new PictureBox();
            checkCircleFlat = new Zeroit.Framework.Metro.ZeroitMetroCheckCircle();
            checkCircleCylinder = new Zeroit.Framework.Metro.ZeroitMetroCheckCircle();
            colorScheme = new Zeroit.Framework.Metro.ZeroitMetroCheckCircle.MainColorScheme();

            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\capacitor.png");

            //метод загрузки шрифта
            GlobalData.LoadFont(14);
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 74;
            labelValue.Top = 62;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 38;
            labelValue.ForeColor = Color.LimeGreen;
            labelValue.TextAlign = HorizontalAlignment.Center;

            pictureValueMinus.Width = 16;
            pictureValueMinus.Height = 16;
            pictureValueMinus.Left = 49;
            pictureValueMinus.Top = 66;
            pictureValueMinus.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureValueMinus.BackColor = Color.Transparent;
            pictureValueMinus.Cursor = Cursors.Hand;
            pictureValueMinus.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\minus0.png");
            pictureValueMinus.MouseEnter += PictureValueMinus_MouseEnter;
            pictureValueMinus.MouseLeave += PictureValueMinus_MouseLeave;
            pictureValueMinus.Click += PictureValueMinus_Click;

            pictureValuePlus.Width = 16;
            pictureValuePlus.Height = 16;
            pictureValuePlus.Left = 121;
            pictureValuePlus.Top = 66;
            pictureValuePlus.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureValuePlus.BackColor = Color.Transparent;
            pictureValuePlus.Cursor = Cursors.Hand;
            pictureValuePlus.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\plus0.png");
            pictureValuePlus.MouseEnter += PictureValuePlus_MouseEnter;
            pictureValuePlus.MouseLeave += PictureValuePlus_MouseLeave;
            pictureValuePlus.Click += PictureValuePlus_Click;

            pictureSeq.Width = 44;
            pictureSeq.Height = 9;
            pictureSeq.Left = 71;
            pictureSeq.Top = 23;
            pictureSeq.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureSeq.BackColor = Color.Transparent;
            pictureSeq.Cursor = Cursors.Hand;
            pictureSeq.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\sequentially0.png");
            pictureSeq.Click += PictureSeq_Click;

            picturePar.Width = 44;
            picturePar.Height = 9;
            picturePar.Left = 71;
            picturePar.Top = 35;
            picturePar.SizeMode = PictureBoxSizeMode.AutoSize;
            picturePar.BackColor = Color.Transparent;
            picturePar.Cursor = Cursors.Hand;
            picturePar.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\parallel0.png");
            picturePar.Click += PicturePar_Click;

            colorScheme.BorderColor = Color.Transparent;
            colorScheme.FillColor = Color.White;
            colorScheme.InnerCirclecolor = Color.White;

            checkCircleFlat.CheckOnClick = true;
            checkCircleFlat.ColorScheme = colorScheme;
            checkCircleFlat.Left = 11;
            checkCircleFlat.Top = 74;
            checkCircleFlat.Size = new Size(20, 20);
            checkCircleFlat.Cursor = Cursors.Hand;
            checkCircleFlat.BackColor = Color.Green;
            checkCircleFlat.Click += CheckCircle_Click;
            checkCircleFlat.DoubleClick += CheckCircle_DoubleClick;

            checkCircleCylinder.CheckOnClick = true;
            checkCircleCylinder.ColorScheme = colorScheme;
            checkCircleCylinder.Left = 155;
            checkCircleCylinder.Top = 74;
            checkCircleCylinder.Size = new System.Drawing.Size(20, 20);
            checkCircleCylinder.Cursor = Cursors.Hand;
            checkCircleCylinder.BackColor = Color.Green;
            checkCircleCylinder.Click += CheckCircle_Click;
            checkCircleCylinder.DoubleClick += CheckCircle_DoubleClick;

            pictureGearFlat.Width = 11;
            pictureGearFlat.Height = 12;
            pictureGearFlat.Left = 11;
            pictureGearFlat.Top = 8;
            pictureGearFlat.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureGearFlat.BackColor = Color.Transparent;
            pictureGearFlat.Cursor = Cursors.Hand;
            pictureGearFlat.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\gear.png");
            pictureGearFlat.Click += PictureGearFlat_Click;

            pictureGearCylinder.Width = 11;
            pictureGearCylinder.Height = 12;
            pictureGearCylinder.Left = 165;
            pictureGearCylinder.Top = 8;
            pictureGearCylinder.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureGearCylinder.BackColor = Color.Transparent;
            pictureGearCylinder.Cursor = Cursors.Hand;
            pictureGearCylinder.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\gear.png");
            pictureGearCylinder.Click += PictureGearCylinder_Click;

            picturePanelFlat.Width = 134;
            picturePanelFlat.Height = 161;
            
            picturePanelFlat.SizeMode = PictureBoxSizeMode.AutoSize;
            picturePanelFlat.BackColor = Color.Transparent;
            picturePanelFlat.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\panelFlat.png");

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxS.TabStop = false;
                textBoxS.Font = GlobalData.DigitalFont;
                textBoxS.Left = 15;
                textBoxS.Top = 33;
                textBoxS.BackColor = Color.White;
                textBoxS.Width = 95;
                textBoxS.ForeColor = Color.Black;
                textBoxS.TextAlign = HorizontalAlignment.Left;
                textBoxS.Cursor = Cursors.IBeam;
                textBoxS.KeyPress += GlobalData.KeyPress;

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxFlatE.TabStop = false;
                textBoxFlatE.Font = GlobalData.DigitalFont;
                textBoxFlatE.Left = 15;
                textBoxFlatE.Top = 77;
                textBoxFlatE.BackColor = Color.White;
                textBoxFlatE.Width = 95;
                textBoxFlatE.ForeColor = Color.Black;
                textBoxFlatE.TextAlign = HorizontalAlignment.Left;
                textBoxFlatE.Cursor = Cursors.IBeam;
                textBoxFlatE.KeyPress += GlobalData.KeyPress;

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxD.TabStop = false;
                textBoxD.Font = GlobalData.DigitalFont;
                textBoxD.Left = 15;
                textBoxD.Top = 122;
                textBoxD.BackColor = Color.White;
                textBoxD.Width = 95;
                textBoxD.ForeColor = Color.Black;
                textBoxD.TextAlign = HorizontalAlignment.Left;
                textBoxD.Cursor = Cursors.IBeam;
                textBoxD.KeyPress += GlobalData.KeyPress;

            picturePanelCyl.Width = 129;
            picturePanelCyl.Height = 200;
            picturePanelCyl.SizeMode = PictureBoxSizeMode.AutoSize;
            picturePanelCyl.BackColor = Color.Transparent;
            picturePanelCyl.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\panelCyl.png");

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxR1.TabStop = false;
                textBoxR1.Font = GlobalData.DigitalFont;
                textBoxR1.Left = 17;
                textBoxR1.Top = 29;
                textBoxR1.BackColor = Color.White;
                textBoxR1.Width = 100;
                textBoxR1.ForeColor = Color.Black;
                textBoxR1.TextAlign = HorizontalAlignment.Left;
                textBoxR1.Cursor = Cursors.IBeam;
                textBoxR1.KeyPress += GlobalData.KeyPress;

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxR2.TabStop = false;
                textBoxR2.Font = GlobalData.DigitalFont;
                textBoxR2.Left = 17;
                textBoxR2.Top = 74;
                textBoxR2.BackColor = Color.White;
                textBoxR2.Width = 100;
                textBoxR2.ForeColor = Color.Black;
                textBoxR2.TextAlign = HorizontalAlignment.Left;
                textBoxR2.Cursor = Cursors.IBeam;
                textBoxR2.KeyPress += GlobalData.KeyPress;

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxCylE.TabStop = false;
                textBoxCylE.Font = GlobalData.DigitalFont;
                textBoxCylE.Left = 17;
                textBoxCylE.Top = 117;
                textBoxCylE.BackColor = Color.White;
                textBoxCylE.Width = 100;
                textBoxCylE.ForeColor = Color.Black;
                textBoxCylE.TextAlign = HorizontalAlignment.Left;
                textBoxCylE.Cursor = Cursors.IBeam;
                textBoxCylE.KeyPress += GlobalData.KeyPress;

                GlobalData.LoadFont(11);  //метод загрузки шрифта
                textBoxL.TabStop = false;
                textBoxL.Font = GlobalData.DigitalFont;
                textBoxL.Left = 17;
                textBoxL.Top = 164;
                textBoxL.BackColor = Color.White;
                textBoxL.Width = 100;
                textBoxL.ForeColor = Color.Black;
                textBoxL.TextAlign = HorizontalAlignment.Left;
                textBoxL.Cursor = Cursors.IBeam;
                textBoxL.KeyPress += GlobalData.KeyPress;


            contactMinus.Width = 34;
            contactMinus.Height = 12;
            contactMinus.Left = 60;
            contactMinus.Top = 92;

            contactPlus.Width = 34;
            contactPlus.Height = 12;
            contactPlus.Left = 94;
            contactPlus.Top = 92;

            labelValue.Text = "1";
            typeCapacitor = GlobalData.TypeCapacitor.Flat;
            checkCircleFlat.Checked = true;
            checkCircleCylinder.Checked = false;
            picturePanelFlat.Visible = false;
            picturePanelCyl.Visible = false;
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height - 10;

            SetPositionControls(73, 0, 94, 0);

            picture.Controls.Add(labelValue);
            picture.Controls.Add(pictureValueMinus);
            picture.Controls.Add(pictureValuePlus);
            picture.Controls.Add(pictureSeq);
            picture.Controls.Add(picturePar);
            picture.Controls.Add(checkCircleFlat);
            picture.Controls.Add(checkCircleCylinder);
            picture.Controls.Add(pictureGearFlat);
            picture.Controls.Add(pictureGearCylinder);

            // код создания панели для ввода данных ПЛОСКОГО конденсатора
            picturePanelFlat.Left = picture.Left - 130;
            picturePanelFlat.Top = picture.Top - 28;
            form.Controls.Add(picturePanelFlat);
                picturePanelFlat.Controls.Add(textBoxS);
                picturePanelFlat.Controls.Add(textBoxFlatE);
                picturePanelFlat.Controls.Add(textBoxD);

            // код создания панели для ввода данных ЦИЛИНДРИЧЕСКОГО кондесатора
            picturePanelCyl.Left = picture.Left + picture.Width - 4;
            picturePanelCyl.Top = picture.Top - 47;
            form.Controls.Add(picturePanelCyl);
                picturePanelCyl.Controls.Add(textBoxR1);
                picturePanelCyl.Controls.Add(textBoxR2);
                picturePanelCyl.Controls.Add(textBoxCylE);
                picturePanelCyl.Controls.Add(textBoxL);

            picture.Controls.Add(contactMinus);
            picture.Controls.Add(contactPlus);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form, 65, 94);

            // распределение составляющих компонента по слоям

            picturePanelFlat.BringToFront();
            pictureSeq.BringToFront();
            labelValue.BringToFront();
            contactMinus.BringToFront();
            contactPlus.BringToFront();
            plugMinusDU.BringToFront();
            plugPlusDU.BringToFront();
            pictureDelete.BringToFront();
            pictureMove.BringToFront();
        }

        public double GetValue()
        {
            return capacity;
        }

        private void Calculate()
        {
            if (typeCapacitor == GlobalData.TypeCapacitor.Flat)
            {
                if (typeConnectionCapacitors == GlobalData.TypeConnectionCapacitors.Sequentially)
                {
                    if (countCapacitors > 1)
                    {
                        double c = 0.0;
                        for (int i = 0; i < countCapacitors; ++i)
                        {
                            //MessageBox.Show(Convert.ToString(c));
                            c += 1/((E * GlobalData.e0 * S) / d);
                        }
                        this.capacity = 1 / c;
                    }
                    else
                    {
                        this.capacity = (E * GlobalData.e0 * S) / d;
                        //MessageBox.Show(
                        //    "пФ: " + Convert.ToString(capacity*GlobalData.pF) + " " + 
                        //    "нФ: " + Convert.ToString(capacity * GlobalData.nF) + " " +
                        //    "мкФ: " + Convert.ToString(capacity * GlobalData.mkF) + " " +
                        //    "мФ: " + Convert.ToString(capacity * GlobalData.mF));
                    }
                }
                else if (typeConnectionCapacitors == GlobalData.TypeConnectionCapacitors.Parallel)
                {
                    if (countCapacitors > 1)
                    {
                        for (int i = 0; i < countCapacitors; ++i)
                        {
                            //MessageBox.Show(Convert.ToString(capacity));
                            this.capacity += (E * GlobalData.e0 * S) / d;
                        }
                    }
                    else
                    {
                        this.capacity = (E * GlobalData.e0 * S) / d;
                    }
                }
            }
            else
            {
                if (typeConnectionCapacitors == GlobalData.TypeConnectionCapacitors.Sequentially)
                {
                    if (countCapacitors > 1)
                    {
                        double c = 0.0;
                        for (int i = 0; i < countCapacitors; ++i)
                        {
                            //MessageBox.Show(Convert.ToString(c));
                            c += 1 / (2 * Math.PI * E * GlobalData.e0 * (l / Math.Log(R2 / R1)));
                        }
                        this.capacity = 1 / c;
                    }
                    else
                    {
                        this.capacity = 2 * Math.PI * E * GlobalData.e0 * (l / Math.Log(R2 / R1));
                    }
                }
                else if (typeConnectionCapacitors == GlobalData.TypeConnectionCapacitors.Parallel)
                {
                    if (countCapacitors > 1)
                    {
                        for (int i = 0; i < countCapacitors; ++i)
                        {
                            //MessageBox.Show(Convert.ToString(capacity));
                            this.capacity += 2 * Math.PI * E * GlobalData.e0 * (l / Math.Log(R2 / R1));
                        }
                    }
                    else
                    {
                        this.capacity = 2 * Math.PI * E * GlobalData.e0 * (l / Math.Log(R2 / R1));
                    }
                }
            }
        } 
       
        private void PictureGearCylinder_Click(object sender, EventArgs e)
        {
            if (picturePanelCyl.Visible == false)
            {
                picturePanelCyl.Visible = true;
            }
            else
            {
                if (textBoxR1.Text == "0" || textBoxR2.Text == "0" || textBoxCylE.Text == "0" || textBoxL.Text == "0")
                {
                    MessageBox.Show("Введите корректные значения цилиндрического конденсатора");
                    picturePanelCyl.Visible = true;
                }
                else if (textBoxR1.Text != "" & textBoxR2.Text != "" & textBoxCylE.Text != "" & textBoxL.Text != "")
                {
                    R1 = Convert.ToDouble(textBoxR1.Text);
                    R2 = Convert.ToDouble(textBoxR2.Text);
                    E = Convert.ToDouble(textBoxCylE.Text);
                    l = Convert.ToDouble(textBoxL.Text);

                    GlobalData.workWithElements.AddChangesValue(R1, R2, E, l);
                    Calculate();

                    picturePanelCyl.Visible = false;
                }
                else if (textBoxR1.Text == "" & textBoxR2.Text == "" & textBoxCylE.Text == "" & textBoxL.Text == "")
                {
                    picturePanelCyl.Visible = false;
                }
                else
                {
                    MessageBox.Show("Введите значения цилиндрического конденсатора");
                    picturePanelCyl.Visible = true;
                }
            }
        }

        private void PictureGearFlat_Click(object sender, EventArgs e)
        {
            if (picturePanelFlat.Visible == false)
            {
                picturePanelFlat.Visible = true;
            }
            else 
            {
                if (textBoxS.Text == "0" || textBoxFlatE.Text == "0" || textBoxD.Text == "0")
                {
                    MessageBox.Show("Введите корректные значения плоского конденсатора");
                    picturePanelFlat.Visible = true;
                }
                else if (textBoxS.Text != "" & textBoxFlatE.Text != "" & textBoxD.Text != "")
                {
                    S = Convert.ToDouble(textBoxS.Text);
                    E = Convert.ToDouble(textBoxFlatE.Text);
                    d = Convert.ToDouble(textBoxD.Text);

                    GlobalData.workWithElements.AddChangesValue(S, E, d);
                    Calculate();
                    MessageBox.Show(Convert.ToString(capacity));

                    picturePanelFlat.Visible = false;
                }
                else if (textBoxS.Text == "" & textBoxFlatE.Text == "" & textBoxD.Text == "")
                {
                    picturePanelFlat.Visible = false;
                }
                else
                {
                    MessageBox.Show("Введите значения плоского конденсатора");
                    picturePanelFlat.Visible = true;
                }
            }
        }

        private void PicturePar_Click(object sender, EventArgs e)
        {
            typeConnectionCapacitors = GlobalData.TypeConnectionCapacitors.Parallel;
            pictureSeq.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\sequentially0.png");
            picturePar.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\parallel1.png");
        }

        private void PictureSeq_Click(object sender, EventArgs e)
        {
            typeConnectionCapacitors = GlobalData.TypeConnectionCapacitors.Sequentially;
            
            pictureSeq.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\sequentially1.png");
            picturePar.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\parallel0.png");
        }

        private void PictureValuePlus_Click(object sender, EventArgs e)
        {
            if (this.countCapacitors != 10)
            {
                ++this.countCapacitors;
                labelValue.Text = Convert.ToString(this.countCapacitors);
                if (this.countCapacitors == 2) 
                {
                    pictureSeq.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\sequentially1.png");
                    picturePar.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\parallel0.png");
                }
            }
        }

        private void PictureValueMinus_Click(object sender, EventArgs e)
        {
            if (this.countCapacitors != 1)
            {
                --this.countCapacitors;
                labelValue.Text = Convert.ToString(this.countCapacitors);
                if (this.countCapacitors < 2) 
                {
                    pictureSeq.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\sequentially0.png");
                    picturePar.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\parallel0.png");
                }
            }
        }

        private void PictureValuePlus_MouseLeave(object sender, EventArgs e)
        {
            pictureValuePlus.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\plus0.png");
        }

        private void PictureValuePlus_MouseEnter(object sender, EventArgs e)
        {
            pictureValuePlus.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\plus1.png");
        }

        private void PictureValueMinus_MouseLeave(object sender, EventArgs e)
        {
            pictureValueMinus.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\minus0.png");
        }

        private void PictureValueMinus_MouseEnter(object sender, EventArgs e)
        {
            pictureValueMinus.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\capacitor\minus1.png");
        }

        private void CheckCircle_DoubleClick(object sender, EventArgs e)
        {
            if (checkCircleFlat.Checked == true)
            {
                checkCircleFlat.Checked = false;
                typeCapacitor = GlobalData.TypeCapacitor.Cylinder;
            }
            else if (checkCircleCylinder.Checked == true)
            {
                checkCircleCylinder.Checked = false;
                typeCapacitor = GlobalData.TypeCapacitor.Flat;
            }
        }

        private void CheckCircle_Click(object sender, EventArgs e)
        {
            if (checkCircleFlat.Checked == true)
            {
                checkCircleFlat.Checked = false;
                typeCapacitor = GlobalData.TypeCapacitor.Cylinder;
            }
            else if(checkCircleCylinder.Checked == true)
            {
                checkCircleCylinder.Checked = false;
                typeCapacitor = GlobalData.TypeCapacitor.Flat;
            }
        }
    }
}
