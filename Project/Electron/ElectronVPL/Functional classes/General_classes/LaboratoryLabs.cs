using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronVPL
{
    static class LaboratoryLabs
    {
        private static PictureBox[] panels;
        private static PictureBox[] buttonsClose;
        private static int numLab;

        private static Elements.Type[] types;
        private static Design.Values[] values;

        //= new Elements.Type[13] {
        //        Elements.Type.Ammeter,
        //        Elements.Type.Voltmeter,
        //        Elements.Type.Multimeter,
        //        Elements.Type.Resistor,
        //        Elements.Type.Conductor,
        //        Elements.Type.Rheostat,
        //        Elements.Type.VoltageSource,
        //        Elements.Type.Capacitor,
        //        Elements.Type.SingleSwitch,
        //        Elements.Type.DoubleSwitch,
        //        Elements.Type.Toggle,
        //        Elements.Type.Lamp,
        //        Elements.Type.Stopwatch};

        public static void ConfiguringForm(int numLab) 
        {
            LaboratoryLabs.numLab = numLab;
            CreatePanels(GlobalData.WorkForm);
            //SetElemets();
        }

        private static void SetElemets() 
        {
            if (numLab == 1)
            {
                //Elements.voltageSource = new VoltageSource();
                //Elements.ammeter = new Ammeter();
                //Elements.voltmeter[0] = new Voltmeter();
                //Elements.stopwatch = new Stopwatch();
            }
        }

        private static void CreatePanels(Form form)
        {
            panels = new PictureBox[2];
            buttonsClose = new PictureBox[2];

            for (int i = 0; i < 2;++i) 
            {
                panels[i] = new PictureBox();
                panels[i].SizeMode = PictureBoxSizeMode.AutoSize;
                panels[i].BringToFront();

                buttonsClose[i] = new PictureBox();
                buttonsClose[i].Top = 22;
                buttonsClose[i].Left = 282;
                buttonsClose[i].SizeMode = PictureBoxSizeMode.AutoSize;
                buttonsClose[i].BackColor = Color.Transparent;
                buttonsClose[i].Cursor = Cursors.Hand;
                buttonsClose[i].BringToFront();

                panels[i].Controls.Add(buttonsClose[i]);
            }

            panels[0].Left = 50;
            panels[0].Top = 50;
            panels[0].Image = Properties.Resources.panel_elements;

            panels[1].Left = 400;
            panels[1].Top = 50;
            panels[1].Image = Properties.Resources.panel_values;

            
            buttonsClose[0].Image = Properties.Resources.close_white;
            
            //buttonsClose[1].Left = 30;
            buttonsClose[1].Image = Properties.Resources.close_white;

            
            form.Controls.Add(panels[0]);
            form.Controls.Add(panels[1]);

            SetDataToPanels();
        }

        private static void SetDataToPanels() 
        {
            if (numLab == 1)
            {
                types = new Elements.Type[5]{
                    Elements.Type.Ammeter,
                    Elements.Type.Voltmeter,
                    Elements.Type.VoltageSource,
                    Elements.Type.Lamp,
                    Elements.Type.Stopwatch
                };

                values = new Design.Values[2]{
                    Design.Values.Мощность,
                    Design.Values.Работа_тока
                };

                Design.CreatePanelElements(panels[0], types);
                Design.CreatePanelValues(panels[0], types, values);
            }
        }
    }
}
