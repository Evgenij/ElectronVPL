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
        private PictureBox contactMinusLeft;
        private PictureBox contactPlusLeft;

        private PictureBox contactMinusRight;
        private PictureBox contactPlusRight;

        private PictureBox contactMinusBottom;
        private PictureBox contactPlusBottom;

        private Zeroit.Framework.Metro.ZeroitMetroSwitch _switch;

        public DoubleSwitch()
        {
            contactMinusLeft = new PictureBox();
            contactPlusLeft = new PictureBox();

            contactMinusRight = new PictureBox();
            contactPlusRight = new PictureBox();

            contactMinusBottom = new PictureBox();
            contactPlusBottom = new PictureBox();

            _switch = new Zeroit.Framework.Metro.ZeroitMetroSwitch();

            position = Position.Left;
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Width = 188;
            picture.Height = 84;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\d_switch.png");

            _switch.Width = 41;
            _switch.Height = 20;
            _switch.Left = 73;
            _switch.Top = 34;
            _switch.Cursor = Cursors.Hand;
            _switch.DefaultColor = Color.ForestGreen;
            _switch.CheckColor = Color.DodgerBlue;
            _switch.HoverColor = Color.DodgerBlue;
            _switch.CheckedChanged += _switch_CheckedChanged;
            picture.Controls.Add(_switch);

            // код создания контактов для подключения

            contactMinusLeft.Width = 33;
            contactMinusLeft.Height = 12;
            contactMinusLeft.Left = 19;
            contactMinusLeft.Top = 0;
            contactMinusLeft.Cursor = Cursors.Hand;
            contactMinusLeft.BackColor = Color.Transparent;
            contactMinusLeft.Click += ContactMinusLeft_Click;
            contactMinusLeft.MouseHover += ContactMinusLeft_MouseHover;
            contactMinusLeft.MouseLeave += ContactMinusLeft_MouseLeave;
            picture.Controls.Add(contactMinusLeft);

            contactPlusLeft.Width = 33;
            contactPlusLeft.Height = 12;
            contactPlusLeft.Left = 52;
            contactPlusLeft.Top = 0;
            contactPlusLeft.Cursor = Cursors.Hand;
            contactPlusLeft.BackColor = Color.Transparent;
            contactPlusLeft.Click += ContactPlusLeft_Click;
            //contactLeft.MouseHover += ContactLeft_MouseHover;
            //contactLeft.MouseLeave += ContactLeft_MouseLeave;
            picture.Controls.Add(contactPlusLeft);

            //-----------------------------

            contactMinusRight.Width = 33;
            contactMinusRight.Height = 12;
            contactMinusRight.Left = 103;
            contactMinusRight.Top = 0;
            contactMinusRight.Cursor = Cursors.Hand;
            contactMinusRight.BackColor = Color.Transparent;
            contactMinusRight.Click += ContactMinusRight_Click;
            //contactLeft.MouseHover += ContactLeft_MouseHover;
            //contactLeft.MouseLeave += ContactLeft_MouseLeave;
            picture.Controls.Add(contactMinusRight);

            contactPlusRight.Width = 33;
            contactPlusRight.Height = 12;
            contactPlusRight.Left = 136;
            contactPlusRight.Top = 0;
            contactPlusRight.Cursor = Cursors.Hand;
            contactPlusRight.BackColor = Color.Transparent;
            contactPlusRight.Click += ContactPlusRight_Click;
            //contactLeft.MouseHover += ContactLeft_MouseHover;
            //contactLeft.MouseLeave += ContactLeft_MouseLeave;
            picture.Controls.Add(contactPlusRight);

            //-----------------------------

            contactMinusBottom.Width = 33;
            contactMinusBottom.Height = 12;
            contactMinusBottom.Left = 61;
            contactMinusBottom.Top = picture.Height - 12;
            contactMinusBottom.Cursor = Cursors.Hand;
            contactMinusBottom.BackColor = Color.Transparent;
            contactMinusBottom.Click += ContactMinusBottom_Click;
            //contactLeft.MouseHover += ContactLeft_MouseHover;
            //contactLeft.MouseLeave += ContactLeft_MouseLeave;
            picture.Controls.Add(contactMinusBottom);

            contactPlusBottom.Width = 33;
            contactPlusBottom.Height = 12;
            contactPlusBottom.Left = 94;
            contactPlusBottom.Top = picture.Height - 12;
            contactPlusBottom.Cursor = Cursors.Hand;
            contactPlusBottom.BackColor = Color.Transparent;
            contactPlusBottom.Click += ContactPlusBottom_Click;
            //contactLeft.MouseHover += ContactLeft_MouseHover;
            //contactLeft.MouseLeave += ContactLeft_MouseLeave;
            picture.Controls.Add(contactPlusBottom);

            // Установки свойств штекеров для подключения

            SetPositionsPlugsDoubleSwitch(form);

            // распределение состовляющих компонента по слоям

            contactMinusLeft.BringToFront();
            contactPlusLeft.BringToFront();

            contactMinusRight.BringToFront();
            contactPlusRight.BringToFront();

            contactMinusBottom.BringToFront();
            contactPlusBottom.BringToFront();

            _switch.BringToFront();
            form.Controls.Add(picture);
        }

        private void ContactMinusLeft_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusUD.Visible = false;
            }
        }

        private void ContactMinusLeft_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusUD.Visible = true;
        }

        private void ContactPlusBottom_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[1, 2] = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
        }

        private void ContactMinusBottom_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[0, 2] = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
        }

        private void ContactPlusRight_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsDoubleSwitch[1, 0] = false;
            contactsDoubleSwitch[1, 1] = true;
            contactsDoubleSwitch[1, 2] = false;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
        }

        private void ContactMinusRight_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsDoubleSwitch[0, 0] = false;
            contactsDoubleSwitch[0, 1] = true;
            contactsDoubleSwitch[0, 2] = false;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
        }

        private void ContactPlusLeft_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsDoubleSwitch[1, 0] = true;
            contactsDoubleSwitch[1, 1] = false;
            contactsDoubleSwitch[1, 2] = false;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
        }

        private void ContactMinusLeft_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsDoubleSwitch[0, 0] = true;
            contactsDoubleSwitch[0, 1] = false;
            contactsDoubleSwitch[0, 2] = false;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
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
