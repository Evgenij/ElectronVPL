using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace Components
{
    class Voltmeter : MeterDevice, ICalculate, IVisualization
    {
        //компоненты формы для создания вольтеметра
        private PictureBox picture;
        private PictureBox contactMinus;
        private PictureBox contactPlus;
        private TextBox labelValue;
        private CircleAnglePicker valueArrow;
        
        public Voltmeter()
        {
            picture = new PictureBox();
            labelValue = new TextBox();
            valueArrow = new CircleAnglePicker();
            contactMinus = new PictureBox();
            contactPlus = new PictureBox();

            valueArrow.Value = 180;
            labelValue.Text = "0";
        }

        /// <summary>
        /// Функция подсчета напряжения
        /// </summary>
        /// <param name="current">Значение силы тока</param>
        /// <param name="resist">Значение сопротивления</param>
        public override double Calculate(double current, double resist)
        {
            this.Value = current * resist;
            labelValue.Text = Convert.ToString(Math.Round(this.Value, 2));
            if (this.Value == 50)
            {
                valueArrow.Value = 0;
            }
            else if (this.Value == 25)
            {
                valueArrow.Value = 90;
            }
            else
            {
                valueArrow.Value = Convert.ToInt32(180 - this.Value * GlobalData.multiplierValues);
            }

            GlobalData.workWithElements.AddChangesValue(
                this, 
                ReportManager.TypeChanges.DefautChange,
                this.Value);

            ChainValues.volt = this.Value;
            return this.Value;
        }

        /// <summary>
        /// Метод отображения компонента на форме
        /// </summary>
        /// <param name="form">Форма на которой будет отображен элемент</param>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        public void Visualization(Form form, int x, int y)
        {
            picture.Width = 135;
            picture.Height = 125;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.SizeMode = PictureBoxSizeMode.AutoSize;
            picture.BackColor = Color.Transparent;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\voltmeter\voltmeter.png");
 
            GlobalData.LoadFont(12);  //метод загрузки шрифта
            labelValue.ReadOnly = true;
            labelValue.TabStop = false;
            labelValue.Font = GlobalData.DigitalFont;
            labelValue.Left = 43;
            labelValue.Top = 96;
            labelValue.BorderStyle = BorderStyle.None;
            labelValue.BackColor = Color.Black;
            labelValue.Width = 50;
            labelValue.ForeColor = Color.DeepSkyBlue;
            labelValue.TextAlign = HorizontalAlignment.Center;
            labelValue.Cursor = Cursors.Hand;
            labelValue.MouseMove += LabelValue_MouseMove;
            labelValue.TextChanged += LabelValue_TextChanged;
            picture.Controls.Add(labelValue);

            valueArrow.Parent = picture;
            valueArrow.Enabled = false;
            valueArrow.Height = 115;
            valueArrow.Width = 115;
            valueArrow.BackColor = Color.Transparent;
            valueArrow.Left = 10;
            valueArrow.Top = 9;
            valueArrow.Parent = picture;
            valueArrow.CircleColor = Color.Transparent;
            picture.Controls.Add(valueArrow);

            // код создания контактов для подключения

            contactMinus.Width = 35;
            contactMinus.Height = 12;
            contactMinus.Left = 13;
            contactMinus.Top = 113;
            contactMinus.Cursor = Cursors.Hand;
            contactMinus.BackColor = Color.Transparent;
            picture.Controls.Add(contactMinus);

            contactPlus.Width = 35;
            contactPlus.Height = 12;
            contactPlus.Left = 85;
            contactPlus.Top = 113;
            contactPlus.Cursor = Cursors.Hand;
            contactPlus.BackColor = Color.Transparent;
            picture.Controls.Add(contactPlus);

            // распределение составляющих компонента по слоям

            picture.SendToBack();
            valueArrow.BringToFront();
            labelValue.BringToFront();
            contactMinus.BringToFront();
            form.Controls.Add(picture);
        }

        //метод для отключения выделения текста в TextBox компонента
        private void LabelValue_TextChanged(object sender, EventArgs e)
        {
            labelValue.SelectionLength = 0;
        }

        //метод для отключения выделения текста в TextBox компонента
        private void LabelValue_MouseMove(object sender, MouseEventArgs e)
        {
            labelValue.SelectionLength = 0;
        }
    }
}
