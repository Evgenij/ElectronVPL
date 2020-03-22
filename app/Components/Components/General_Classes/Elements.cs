using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Components
{
    static class Elements
    {
        public static Ammeter ammeter;
        public static Voltmeter voltmeter;
        public static Multimeter multimeter;
        public static Resistor[] resistor = new Resistor[2];
        public static Conductor conductor;
        public static Rheostat rheostat;
        public static VoltageSource voltageSource;
        public static Capacitor capacitor;
        public static SingleSwitch singleSwitch;
        public static DoubleSwitch doubleSwitch;
        public static Toggle toggle;
        public static HeatingArea heatingArea;
        public static Lamp lamp;
        public static Stopwatch stopwatch;
    }
}
