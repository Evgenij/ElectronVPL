using System;
using System.Windows.Forms;

namespace Components
{
    class WorkWithElements : ReportManager
    {
        /// <summary>
        /// Добавление в память действие с элементами цепи
        /// </summary>
        /// <param name="component">Название элемента</param>
        /// <param name="typeAction">Тип действия</param>
        public void AddAction<T>(T component, TypeAction typeAction)
        {
            if (typeAction == TypeAction.Add)
            {
                if (component is Ammeter)
                {
                    actions.Append("На схему был добавлен амперметр. ");
                }
                if (component is Voltmeter)
                {
                    actions.Append("На схему был добавлен вольтметр. ");
                }
                if (component is VoltageSource)
                {
                    actions.Append("На схему был добавлен источник напряжения. ");
                }
                if (component is Multimeter)
                {
                    actions.Append("На схему был добавлен мультиметр. ");
                }
                if (component is Capacitor)
                {
                    actions.Append("На схему был добавлен конденсатор. ");
                }
                if (component is Conductor)
                {
                    actions.Append("На схему был добавлен проводник. ");
                }
                if (component is Resistor)
                {
                    actions.Append("На схему был добавлен резистор. ");
                }
                if (component is Rheostat)
                {
                    actions.Append("На схему был добавлен реостат. ");
                }
                if (component is SingleSwitch)
                {
                    actions.Append("На схему был добавлен одиночный переключатель. ");
                }
                if (component is DoubleSwitch)
                {
                    actions.Append("На схему был добавлен двойной переключатель. ");
                }
                if (component is Toggle)
                {
                    actions.Append("На схему был добавлен ключ. ");
                }
                if (component is HeatingArea)
                {
                    actions.Append("На схеме была сформирована область изменения температуры. ");
                }
                if (component is Lamp)
                {
                    actions.Append("На схему была добавлена лампочка. ");
                }
                if (component is Stopwatch)
                {
                    actions.Append("На рабочую область был добавлен секундомер. ");
                }
            }
            else if (typeAction == TypeAction.Delete)
            {
                if (component is Ammeter)
                {
                    actions.Append("Из схемы был удален амперметр. ");
                }
                if (component is Voltmeter)
                {
                    actions.Append("Из схемы был удален вольтметр. ");
                }
                if (component is VoltageSource)
                {
                    actions.Append("Из схемы был удален источник напряжения. ");
                }
                if (component is Multimeter)
                {
                    actions.Append("Из схемы был удален мультиметр. ");
                }
                if (component is Capacitor)
                {
                    actions.Append("Из схемы был удален конденсатор. ");
                }
                if (component is Conductor)
                {
                    actions.Append("Из схемы был удален проводник. ");
                }
                if (component is Resistor)
                {
                    actions.Append("Из схемы был удален резистор. ");
                }
                if (component is Rheostat)
                {
                    actions.Append("Из схемы был удален реостат. ");
                }
                if (component is SingleSwitch)
                {
                    actions.Append("Из схемы был удален одиночный переключатель. ");
                }
                if (component is DoubleSwitch)
                {
                    actions.Append("Из схемы был удален двойной переключатель. ");
                }
                if (component is Toggle)
                {
                    actions.Append("Из схемы был удален ключ. ");
                }
                if (component is HeatingArea)
                {
                    actions.Append("Из схемы была удалена область изменения температуры. ");
                }
                if (component is Lamp)
                {
                    actions.Append("Из схемы была удалена лампочка. ");
                }
                if (component is Stopwatch)
                {
                    actions.Append("Из рабочей области был удален секундомер. ");
                }
            }
        }

        /// <summary>
        /// Добавление в память измененного значения элемента с указанием типа изменения
        /// </summary>
        /// <param name="component">Тип элемента</param>
        /// <param name="typeChanges">Тип действия</param>
        /// <param name="value">Значение</param>
        public void AddChangesValue<T_comp, T_val>(T_comp component, TypeChanges typeChanges, T_val value)
        {
            if (component is Resistor)
            {
                if (typeChanges == TypeChanges.Plus)
                {
                    actions.Append("Сопротивление резистора было увеличено до " + Convert.ToString(value) + " Ом. ");
                }
                else if (typeChanges == TypeChanges.Minus)
                {
                    actions.Append("Сопротивление резистора было уменьшено до " + Convert.ToString(value) + " Ом. ");
                }
                else
                {
                    actions.Append("Сопротивление проводника было изменено до " + Convert.ToString(value) + " Ом. ");
                }
            }
            if (component is Toggle) 
            {
                if (typeChanges == TypeChanges.On)
                {
                    actions.Append("При помощи ключа цепь была замкнута. ");
                }
                else
                {
                    actions.Append("При помощи ключа цепь была разомкнута. ");
                }
            }

        }

        public void AddChangesValue<T_comp>(T_comp component, TypeChanges typeChanges)
        {
            if (component is Toggle)
            {
                if (typeChanges == TypeChanges.On)
                {
                    actions.Append("При помощи ключа цепь была замкнута. ");
                }
                else
                {
                    actions.Append("При помощи ключа цепь была разомкнута. ");
                }
            }

        }

        /// <summary>
        /// Добавление в память измененного значения элемента с указанием типа изменения
        /// </summary>
        /// <param name="component">Тип элемента</param>
        /// <param name="switchingType">Тип переключения</param>
        public void AddChangesValue<T_comp>(T_comp component, SwitchingType switchingType)
        {
            if (component is SingleSwitch)
            {
                if (switchingType == SwitchingType.First)
                {
                    actions.Append("Переключатель был переключен в первое положение. ");
                }
                else if (switchingType == SwitchingType.Second)
                {
                    actions.Append("Переключатель был переключен во второе положение. ");
                }
                else
                {
                    actions.Append("Переключатель был переключен в среднее положение и цепь была разомкнута. ");
                }
            }
            else if (component is DoubleSwitch) 
            {
                if (switchingType == SwitchingType.First)
                {
                    actions.Append("Переключатель был переключен в первое положение, для того, чтобы конденсатор принял заряд. ");
                }
                else
                {
                    actions.Append("Переключатель был переключен во второе положение, " +
                        "для того, чтобы конденсатор отдал заряд. " +
                        "После чего величина заряды была отображена на мультиметре. ");
                }
            }
        }

        /// <summary>
        /// Добавление в память измененных значений элементов цепи
        /// </summary>
        /// <param name="component">Тип элемента</param>
        /// <param name="value">Значение</param>
        public void AddChangesValue<T_comp, T_val>(T_comp component, T_val value)
        {
            if (component is Ammeter)
            {
                actions.Append("Показания на амперметре были изменены до " + Convert.ToString(value) + " A. ");
            }
            if (component is Voltmeter)
            {
                actions.Append("Показания на вольтметре были изменены до " + Convert.ToString(value) + " В. ");
            }
            if (component is Rheostat)
            {
                actions.Append("Сопротивление реостата было изменено до " + Convert.ToString(value) + " Ом. ");
            }
            if (component is VoltageSource)
            {
                actions.Append("Напряжение на источнике напряжения было изменено до " + Convert.ToString(value) + " Ом. ");
            }
            if (component is Thermometr)
            {
                actions.Append("Температура в области нагревания была изменена до " + Convert.ToString(value)+ "°С. ");
            }
            if (component is Stopwatch)
            {
                actions.Append("Продолжительность замыкания цепи " + Convert.ToString(value) + ". ");
            }
        }

        /// <summary>
        /// Добавление в память измененного значения мультиметра
        /// </summary>
        public void AddChangesValueMultimeter(Multimeter multimeter, string value)
        {
            actions.Append("Показания на мультиметре были установлены в " + value + " " + multimeter.GetUnit() + ". ");
        }

        /// <summary>
        /// Запись измененных значений проводника
        /// </summary>
        /// <param name="l">длина проводника</param>
        /// <param name="d">диаметр проводника</param>
        /// <param name="p">удельное электрическое сопротиволение материала</param>
        public void AddChangesValueConductor(double l, double d, double p)
        {
            actions.Append("Длина проводника изменена до " + Convert.ToString(l) + " cм, ");
            actions.Append("также диаметр изменён до " + Convert.ToString(d) + " см, ");
            actions.Append("и удельное электрическое сопротивление материала проводника изменено до " + Convert.ToString(p) + ". ");
        }

        /// <summary>
        /// Запись измененных значений плоского конденсатора
        /// </summary>
        /// <param name="S">площадь пластин конденсатора</param>
        /// <param name="E">значение относительной диэлектрической проницаемости</param>
        /// <param name="d">расстояние между пластинами конденсатора</param>
        public void AddChangesValue(double S, double E, double d)
        {
            actions.Append("Площадь пластин плоского конденсатора изменена до " + Convert.ToString(S) + " cм^2, ");
            actions.Append("также значение относительной диэлектрической проницаемости изменено до " + Convert.ToString(E) + ", ");
            actions.Append("и расстояние между пластинами изменено до " + Convert.ToString(d) + " мм. ");
        }

        /// <summary>
        /// Запись измененных значений цилиндрического конденсатора
        /// </summary>
        /// <param name="R1">внутренний радиус конденсатора</param>
        /// <param name="R2">внешний радиус конденсатора</param>
        /// <param name="E">значение относительной диэлектрической проницаемости</param>
        /// <param name="l">высота конденсатора</param>
        public void AddChangesValue(double R1, double R2, double E, double l)
        {
            actions.Append("Внутренний и внешний радиусы цилиндрического конденсатора изменены до "
                + Convert.ToString(R1) + " см и " + Convert.ToString(R2) + " см соответственно, ");

            actions.Append("также высота конденсатора изменена до " + Convert.ToString(l) + " см, ");

            actions.Append("и значение относительной диэлектрической проницаемости изменено до "
                + Convert.ToString(E) + ". ");
        }
    }
}
