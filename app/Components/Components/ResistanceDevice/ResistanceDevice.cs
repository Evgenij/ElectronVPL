
namespace Components
{
    class ResistanceDevice : Device, IReturnResistance
    {
        // Значение сопротивления резистивного элемента
        protected int resistanceValue { get; set; }

        // Значение длинны проводника
        protected double l { get; set; }
        // Значение диаметра проводника
        protected double d { get; set; }
        // Значение длинны проводника
        protected double p { get; set; }
        // Значение длинны проводника
        protected double S { get; set; }

        protected ResistanceDevice() 
        {
            SetPositionControls(143, 2, 164, 2);

            labelValue.Text = "";
            this.resistanceValue = 0;

            picture.Controls.Add(contactMinus);
            picture.Controls.Add(contactPlus);
        }

        /// <summary>
        /// Возвращает сопротивление резистивного элемента
        /// </summary>
        public int ReturnResistance() 
        {
            return this.resistanceValue;
        }
    }
}
