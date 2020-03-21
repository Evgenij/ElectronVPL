using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class DoubleSwitch : Switch
    {
        private PictureBox contactLeftMinus;
        private PictureBox contactLeftPlus;

        private PictureBox contactRightMinus;
        private PictureBox contactRightPlus;

        private PictureBox contactBottomMinus;
        private PictureBox contactBottomPlus;

        private Zeroit.Framework.Metro.ZeroitMetroSwitch _switch;

        public DoubleSwitch()
        {
            picture.Width = 188;
            picture.Height = 84;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\d_switch.png");

            contactLeftMinus = new PictureBox();
            contactLeftPlus = new PictureBox();

            contactRightMinus = new PictureBox();
            contactRightPlus = new PictureBox();

            contactBottomMinus = new PictureBox();
            contactBottomPlus = new PictureBox();

            _switch = new Zeroit.Framework.Metro.ZeroitMetroSwitch();
            _switch.Width = 41;
            _switch.Height = 20;
            _switch.Left = 73;
            _switch.Top = 34;
            _switch.Cursor = Cursors.Hand;
            _switch.DefaultColor = Color.ForestGreen;
            _switch.CheckColor = Color.DodgerBlue;
            _switch.HoverColor = Color.DodgerBlue;
            _switch.CheckedChanged += _switch_CheckedChanged;

            contactLeftPlus.Width = 33;
            contactLeftPlus.Height = 12;
            contactLeftPlus.Left = 19;
            contactLeftPlus.Top = 0;
            contactLeftPlus.Cursor = Cursors.Hand;
            contactLeftPlus.BackColor = Color.Transparent;
            contactLeftPlus.Click += ContactLeftPlus_Click;
            contactLeftPlus.MouseHover += ContactLeftPlus_MouseHover;
            contactLeftPlus.MouseLeave += ContactLeftPlus_MouseLeave;
            
            contactLeftMinus.Width = 33;
            contactLeftMinus.Height = 12;
            contactLeftMinus.Left = 52;
            contactLeftMinus.Top = 0;
            contactLeftMinus.Cursor = Cursors.Hand;
            contactLeftMinus.BackColor = Color.Transparent;
            contactLeftMinus.Click += ContactLeftMinus_Click;
            contactLeftMinus.MouseHover += ContactLeftMinus_MouseHover;
            contactLeftMinus.MouseLeave += ContactLeftMinus_MouseLeave;
           
            //-----------------------------

            contactRightPlus.Width = 33;
            contactRightPlus.Height = 12;
            contactRightPlus.Left = 103;
            contactRightPlus.Top = 0;
            contactRightPlus.Cursor = Cursors.Hand;
            contactRightPlus.BackColor = Color.Transparent;
            contactRightPlus.Click += ContactRightPlus_Click;
            contactRightPlus.MouseHover += ContactRightPlus_MouseHover;
            contactRightPlus.MouseLeave += ContactRightPlus_MouseLeave;
           
            contactRightMinus.Width = 33;
            contactRightMinus.Height = 12;
            contactRightMinus.Left = 136;
            contactRightMinus.Top = 0;
            contactRightMinus.Cursor = Cursors.Hand;
            contactRightMinus.BackColor = Color.Transparent;
            contactRightMinus.Click += ContactRightMinus_Click;
            contactRightMinus.MouseHover += ContactRightMinus_MouseHover;
            contactRightMinus.MouseLeave += ContactRightMinus_MouseLeave;

            //-----------------------------

            contactBottomPlus.Width = 33;
            contactBottomPlus.Height = 12;
            contactBottomPlus.Left = 61;
            contactBottomPlus.Top = picture.Height - 12;
            contactBottomPlus.Cursor = Cursors.Hand;
            contactBottomPlus.BackColor = Color.Transparent;
            contactBottomPlus.Click += ContactBottomPlus_Click;
            contactBottomPlus.MouseHover += ContactBottomPlus_MouseHover;
            contactBottomPlus.MouseLeave += ContactBottomPlus_MouseLeave;
            
            contactBottomMinus.Width = 33;
            contactBottomMinus.Height = 12;
            contactBottomMinus.Left = 94;
            contactBottomMinus.Top = picture.Height - 12;
            contactBottomMinus.Cursor = Cursors.Hand;
            contactBottomMinus.BackColor = Color.Transparent;
            contactBottomMinus.Click += ContactBottomMinus_Click;
            contactBottomMinus.MouseHover += ContactBottomMinus_MouseHover;
            contactBottomMinus.MouseLeave += ContactBottomMinus_MouseLeave;

            position = Position.Left;
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;

            SetPositionControls(3, 61, 24, 61);

            picture.Controls.Add(_switch);

            // код создания контактов для подключения

            picture.Controls.Add(contactLeftPlus);
            picture.Controls.Add(contactLeftMinus);
            picture.Controls.Add(contactRightPlus);
            picture.Controls.Add(contactRightMinus);
            picture.Controls.Add(contactBottomPlus);
            picture.Controls.Add(contactBottomMinus);

            // Установки свойств штекеров для подключения

            SetPositionsPlugs(form);

            // распределение состовляющих компонента по слоям

            _switch.BringToFront();

            // --------------------

            contactLeftMinus.BringToFront();
            contactLeftPlus.BringToFront();

            contactRightMinus.BringToFront();
            contactRightPlus.BringToFront();

            contactBottomMinus.BringToFront();
            contactBottomPlus.BringToFront();

            // --------------------

            plugLeftMinusUD.BringToFront();
            plugLeftPlusUD.BringToFront();

            plugRightMinusUD.BringToFront();
            plugRightPlusUD.BringToFront();

            plugMinusDU.BringToFront();
            plugPlusDU.BringToFront();
        }

        private void ContactBottomMinus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                plugMinusDU.Visible = false;
            }
        }

        private void ContactBottomMinus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugMinusDU.Visible = true;
            // подключение правого минусового контакта
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[2, 0] = false;
            contactsDoubleSwitch[2, 1] = true;
        }

        private void ContactBottomMinus_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                connectReceiver = true;
                Design.Animate(plugMinusDU, GlobalData.TimePlugAnimation);
                Design.ConnectionElements(GlobalData.deviceSource, this);
            }
            else
            {
                MessageBox.Show("Подключение невозможно...");
            }
        }

        private void ContactRightPlus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugRightPlusUD.Visible = false;
            }
        }
        private void ContactRightPlus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugRightPlusUD.Visible = true;
        }
        private void ContactRightPlus_Click(object sender, EventArgs e)
        {
            connectSource = true;
            Design.Animate(plugRightPlusUD, GlobalData.TimePlugAnimation);
            GlobalData.deviceSource = this;
            // подключение правого плюсового контакта
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[1, 0] = true;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[2, 0] = false;
            contactsDoubleSwitch[2, 1] = false;
        }

        private void ContactBottomPlus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusDU.Visible = false;
            }
        }
        private void ContactBottomPlus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusDU.Visible = true;
        }
        private void ContactBottomPlus_Click(object sender, EventArgs e)
        {
            connectSource = true;
            Design.Animate(plugPlusDU, GlobalData.TimePlugAnimation);
            GlobalData.deviceSource = this;
            // подключение правого плюсового контакта
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[2, 0] = true;
            contactsDoubleSwitch[2, 1] = false;
        }

        private void ContactRightMinus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                plugRightMinusUD.Visible = false;
            }
        }
        private void ContactRightMinus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugRightMinusUD.Visible = true;
            // подключение правого минусового контакта
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = true;
            contactsDoubleSwitch[2, 0] = false;
            contactsDoubleSwitch[2, 1] = false;
        }
        private void ContactRightMinus_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                connectReceiver = true;
                Design.Animate(plugRightMinusUD, GlobalData.TimePlugAnimation);
                Design.ConnectionElements(GlobalData.deviceSource, this);
            }
            else
            {
                MessageBox.Show("Подключение невозможно...");
            }
        }

        private void ContactLeftMinus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectReceiver != true)
            {
                plugLeftMinusUD.Visible = false;
            }
        }
        private void ContactLeftMinus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugLeftMinusUD.Visible = true;
            // подключение левого минусового контакта
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = true;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[2, 0] = false;
            contactsDoubleSwitch[2, 1] = false;
        }
        private void ContactLeftMinus_Click(object sender, EventArgs e)
        {
            if (GlobalData.deviceSource != this)
            {
                connectReceiver = true;
                Design.Animate(plugLeftMinusUD, GlobalData.TimePlugAnimation);
                Design.ConnectionElements(GlobalData.deviceSource, this);
            }
            else
            {
                MessageBox.Show("Подключение невозможно...");
            }
        }

        private void ContactLeftPlus_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugLeftPlusUD.Visible = false;
            }
        }

        private void ContactLeftPlus_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugLeftPlusUD.Visible = true;
        }

        private void ContactLeftPlus_Click(object sender, EventArgs e)
        {
            connectSource = true;
            Design.Animate(plugLeftPlusUD, GlobalData.TimePlugAnimation);
            GlobalData.deviceSource = this;
            // подключение левого плюсового контакта
            contactsDoubleSwitch[0, 0] = true;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[2, 0] = false;
            contactsDoubleSwitch[2, 1] = false;
        }

        protected override void SetPositionsPlugs(Form form)
        {
            form.Controls.Add(plugLeftPlusUD);
            plugLeftPlusUD.Top = picture.Top - plugLeftPlusUD.Height + 4;
            plugLeftPlusUD.Left = picture.Left + 24;

            pointLeftPlus = new Point(
                plugLeftPlusUD.Left + plugLeftPlusUD.Width / 2,
                plugLeftPlusUD.Top - 3);

            form.Controls.Add(plugLeftMinusUD);
            plugLeftMinusUD.Top = picture.Top - plugLeftMinusUD.Height + 4;
            plugLeftMinusUD.Left = picture.Left + 53;

            pointLeftMinus = new Point(
                plugLeftMinusUD.Left + plugMinusUD.Width / 2,
                plugLeftMinusUD.Top - 3);

            // -----------------------------

            form.Controls.Add(plugRightMinusUD);
            plugRightMinusUD.Top = picture.Top - plugRightMinusUD.Height + 4;
            plugRightMinusUD.Left = picture.Left + 137;

            pointRightMinus = new Point(
                plugRightMinusUD.Left + plugRightMinusUD.Width / 2,
                plugRightMinusUD.Top - 3);

            form.Controls.Add(plugRightPlusUD);
            plugRightPlusUD.Top = picture.Top - plugRightPlusUD.Height + 4;
            plugRightPlusUD.Left = picture.Left + 108;

            pointRightPlus = new Point(
                plugRightPlusUD.Left + plugRightPlusUD.Width / 2,
                plugRightPlusUD.Top - 3);

            // -----------------------------

            form.Controls.Add(plugPlusDU);
            plugPlusDU.Top = picture.Top + picture.Height - 4;
            plugPlusDU.Left = picture.Left + 66;

            pointBottomPlus = new Point(
                plugPlusDU.Left + plugPlusDU.Width / 2,
                plugPlusDU.Top + plugPlusDU.Height);

            form.Controls.Add(plugMinusDU);
            plugMinusDU.Top = picture.Top + picture.Height - 4;
            plugMinusDU.Left = picture.Left + 95;

            pointBottomMinus = new Point(
                plugMinusDU.Left + plugMinusDU.Width / 2,
                plugMinusDU.Top + plugMinusDU.Height);



            plugLeftPlusUD.BringToFront();
            plugLeftMinusUD.BringToFront();

            plugRightMinusUD.BringToFront();
            plugRightPlusUD.BringToFront();

            plugPlusDU.BringToFront();
            plugMinusDU.BringToFront();
        }

        private void _switch_CheckedChanged(object sender, EventArgs e)
        {
            if (position == Position.Left)
            {
                position = Position.Right;
                GlobalData.workWithElements.AddChangesValue(
                        this,
                        ReportManager.SwitchingType.First);
            }
            else 
            {
                position = Position.Left;
                GlobalData.workWithElements.AddChangesValue(
                        this,
                        ReportManager.SwitchingType.Second);
            }
        }
    }
}
