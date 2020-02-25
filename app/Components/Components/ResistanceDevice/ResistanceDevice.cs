
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

        /// <summary>
        /// Возвращает сопротивление резистивного элемента
        /// </summary>
        public int ReturnResistance() 
        {
            return this.resistanceValue;
        }
    }
}
