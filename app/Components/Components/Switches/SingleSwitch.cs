using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    class SingleSwitch : Switch
    {
        public SingleSwitch() 
        {
            position = Position.Center;

            contactLeft = new PictureBox();
            contactTop = new PictureBox();
            contactBottom = new PictureBox();

            contactLeft.Cursor = Cursors.Hand;
            contactLeft.BackColor = Color.Transparent;
            contactLeft.Click += ContactLeft_Click;
            contactLeft.MouseHover += ContactLeft_MouseHover;
            contactLeft.MouseLeave += ContactLeft_MouseLeave;
            picture.Controls.Add(contactLeft);

            contactTop.Cursor = Cursors.Hand;
            contactTop.BackColor = Color.Transparent;
            contactTop.Click += ContactTop_Click;
            contactTop.MouseHover += ContactTop_MouseHover;
            contactTop.MouseLeave += ContactTop_MouseLeave;
            picture.Controls.Add(contactTop);

            contactBottom.Cursor = Cursors.Hand;
            contactBottom.BackColor = Color.Transparent;
            contactBottom.Click += ContactBottom_Click;
            contactBottom.MouseHover += ContactBottom_MouseHover;
            contactBottom.MouseLeave += ContactBottom_MouseLeave;
            picture.Controls.Add(contactBottom);
        }

        public override void Visualization(Form form, int x, int y)
        {
            picture.Width = 107;
            picture.Height = 103;
            picture.Left = x - picture.Width / 2;
            picture.Top = y - picture.Height / 2;
            picture.MouseMove += Picture_MouseMove;
            picture.Click += Picture_Click;
            picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\s_switch0.png");

            // код создания контактов для подключения

            contactLeft.Width = 12;
            contactLeft.Height = 35;
            contactLeft.Left = 0;
            contactLeft.Top = 34;

            contactTop.Width = 35;
            contactTop.Height = 12;
            contactTop.Left = 57;
            contactTop.Top = 0;

            contactBottom.Width = 35;
            contactBottom.Height = 12;
            contactBottom.Left = 57;
            contactBottom.Top = picture.Height - 12;

            // Установки свойств штекеров для подключения

            SetPositionsPlugsSingleSwitch(form);

            // распределение состовляющих компонента по слоям
            form.Controls.Add(picture);
        }

        private void ContactBottom_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusDU.Visible = false;
            }
        }

        private void ContactBottom_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusDU.Visible = true;
        }

        private void ContactBottom_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsSingleSwitch[0] = false;
            contactsSingleSwitch[1] = false;
            contactsSingleSwitch[2] = true;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusDU, 950);
        }

        private void ContactTop_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusUD.Visible = false;
            }
        }

        private void ContactTop_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusUD.Visible = true;
        }

        private void ContactTop_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsSingleSwitch[0] = false;
            contactsSingleSwitch[1] = true;
            contactsSingleSwitch[2] = false;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusUD, 950);
        }

        private void ContactLeft_MouseHover(object sender, EventArgs e)
        {
            Cursor.Hide();
            plugPlusLR.Visible = true;
        }

        private void ContactLeft_Click(object sender, EventArgs e)
        {
            connectSource = true;
            contactsSingleSwitch[0] = true;
            contactsSingleSwitch[1] = false;
            contactsSingleSwitch[2] = false;
            GlobalData.deviceSource = this;
            Design.Animate(plugPlusLR, 950);
        }

        private void ContactLeft_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            if (connectSource != true)
            {
                plugPlusLR.Visible = false;
            }
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            if (position == Position.Top)
            {
                position = Position.Center;
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\s_switch0.png");
                GlobalData.workWithElements.AddChangesValue(
                    this, 
                    ReportManager.SwitchingType.Between);
            }
            else if (position == Position.Bottom)
            {
                position = Position.Center;
                picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\s_switch0.png");
                GlobalData.workWithElements.AddChangesValue(
                    this,
                    ReportManager.SwitchingType.Between);
            }
            else
            {
                if (picture.Cursor == Cursors.PanNorth)
                {
                    position = Position.Top;
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\s_switch1.png");
                    GlobalData.workWithElements.AddChangesValue(
                        this,
                        ReportManager.SwitchingType.First);
                }
                else if (picture.Cursor == Cursors.PanSouth)
                {
                    position = Position.Bottom;
                    picture.Image = Image.FromFile(@"C:\Users\Evgenij\CourseProject\ElectronVPL\pictures\switches\s_switch2.png");
                    GlobalData.workWithElements.AddChangesValue(
                        this,
                        ReportManager.SwitchingType.Second);
                }
            }
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y <= 40)
            {
                if (position == Position.Top)
                {
                    picture.Cursor = Cursors.PanSouth;
                }
                else if (position == Position.Center)
                {
                    picture.Cursor = Cursors.PanNorth;
                }
                else 
                {
                    picture.Cursor = Cursors.PanNorth;
                }
            }
            else if (e.Y <= 63)
            {
                if (position == Position.Center)
                {
                    picture.Cursor = Cursors.NoMoveVert;
                }
                else if (position == Position.Top)
                {
                    picture.Cursor = Cursors.PanSouth;
                }
                else 
                {
                    picture.Cursor = Cursors.PanNorth;
                }
            }
            else 
            {
                if (position == Position.Bottom)
                {
                    picture.Cursor = Cursors.PanNorth;
                }
                else if (position == Position.Center)
                {
                    picture.Cursor = Cursors.PanSouth;
                }
                else 
                {
                    picture.Cursor = Cursors.PanSouth;
                }
            }
        }
    }
}
