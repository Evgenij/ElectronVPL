using System;
using System.Drawing;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.Linq;
using System.Text;

namespace Components
{
    class ReportManager
    {
        private DocX document;
        private Paragraph paragraph;
        private Table table;
        private StringBuilder actions;

        public enum TypeAction { Add, Delete }
        public enum TypeChanges { Plus, Minus, DefautChange }

        private static string[] themesLabs = new string[5] {
            "тема лабы 1",
            "тема лабы 2",
            "тема лабы 3",
            "тема лабы 4",
            "тема лабы 5"
            };
        private static string[] targetLabs = new string[5] {
            "цель лабы 1",
            "цель лабы 2",
            "цель лабы 3",
            "цель лабы 4",
            "цель лабы 5"
            };

        public ReportManager()
        {
            string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "fileExample.docx";
            actions = new StringBuilder();

            // создаём документ
            document = DocX.Create(pathDocument);
            // Добавление объекта для работы с верхним колонтитулом
            document.AddHeaders();
            // Добавление объекта для работы с нижним колонтитулом
            document.AddFooters();
            // Установка в документе разбиение колонтитулов по страницам
            document.DifferentFirstPage = true;

            actions.Clear();
        }

        /*рефакторинг - добавить группу студента как параметр 
        и в этом конструкторе вызывать методы создания колонтитулов */
        public ReportManager(string surname, string name, int numLab)
        {
            string pathDocument = AppDomain.CurrentDomain.BaseDirectory + surname + "_" + name +"_Lab"+Convert.ToString(numLab)+".docx";
            actions = new StringBuilder();

            // создаём документ
            document = DocX.Create(pathDocument);
            // Добавление объекта для работы с верхним колонтитулом
            document.AddHeaders();
            // Добавление объекта для работы с нижним колонтитулом
            document.AddFooters();
            // Установка в документе разбиение колонтитулов по страницам
            document.DifferentFirstPage = true;

            actions.Clear();
        }

        /// <summary>
        /// Получение текста лабораторной работы
        /// </summary>
        /// <param name="numLab">номер лабораторной</param>
        public string GetLabTheme(int numLab)
        {
            if (numLab >= 5)
            {
                return "none";
            }
            else
            {
                return themesLabs[numLab - 1];
            }
        }

        /// <summary>
        /// Получение темы лабораторной работы
        /// </summary>
        /// <param name="numLab">номер лабораторной</param>
        public string GetLabTarget(int numLab)
        {
            if (numLab >= 5)
            {
                return "none";
            }
            else
            {
                return targetLabs[numLab - 1];
            }
        }

        /// <summary>
        /// Добавление в отчет верхнего колонтитула
        /// </summary>
        /// <param name="surname">Фамилия студента</param>
        /// <param name="name">Имя студента</param>
        /// <param name="group">Группа студента</param>
        public void AddHeader(string surname, string name, string group)
        {
            document.Headers.First.InsertParagraph(surname + " " + name + " " + group).
                Font("Times New Roman").
                FontSize(12);
            document.Headers.Even.InsertParagraph(surname + " " + name + " " + group).
                Font("Times New Roman").
                FontSize(12);
            document.Headers.Odd.InsertParagraph(surname + " " + name + " " + group).
                Font("Times New Roman").
                FontSize(12);

            document.Save();
        }

        /// <summary>
        /// Добавление в отчет верхнего колонтитула
        /// </summary>
        public void AddFooter()
        {
            document.Footers.First.InsertParagraph("стр ").
                Font("Times New Roman").
                FontSize(12).
                AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.right;

            document.Footers.Even.InsertParagraph("стр ").
                Font("Times New Roman").
                FontSize(12).
                AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.right;

            document.Footers.Odd.InsertParagraph("стр ").
                Font("Times New Roman").
                FontSize(12).
                AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.right;

            document.Save();
        }

        /// <summary>
        /// Добавление в отчет заголовка
        /// </summary>
        /// <param name="numLab">Номер лабораторной работы</param>
        /// <param name="theme">Тема лабораторной работы</param>
        /// <param name="target">Цель лабораторной работы</param>
        public void AddReportHead(string numLab, string theme, string target)
        {
            // вставляем параграф и передаём текст
            document.InsertParagraph("Лабораторная работа №" + numLab).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14).
                Alignment = Alignment.center;

            document.InsertParagraph("Тема: " + theme).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14).
                Alignment = Alignment.center;

            document.InsertParagraph("Цель: " + target).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14).
                Alignment = Alignment.center;

            // вставляем параграф и добавляем текст
            document.InsertParagraph("Ход работы:").
                SpacingBefore(20).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14).
                Alignment = Alignment.center;

            document.Save();
        }

        /// <summary>
        /// Добавление в отчет заголовка первого уровня
        /// </summary>
        /// <param name="sectionName">Название заголовка</param>
        public void AddSection(string sectionName) 
        {
            //var headingTypes = Enum.GetValues(typeof(HeadingType));

            HeadingType heading = HeadingType.Heading1;
            var text = string.Format(sectionName, heading.EnumDescription());
            var paragraph = document.InsertParagraph(text);
            // Set the paragraph's heading type.
            paragraph.Heading(heading).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                Bold().
                SpacingLine(22).
                Kerning(14);
            paragraph.IndentationFirstLine = 1.5f;
            paragraph.Alignment = Alignment.left;
            
            document.Save();
        }

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
                else if (component is Voltmeter)
                {
                    actions.Append("На схему был добавлен вольтметр. ");
                }
                else if (component is VoltageSource)
                {
                    actions.Append("На схему был добавлен источник напряжения. ");
                }
                else if (component is Multimeter)
                {
                    actions.Append("На схему был добавлен мультиметр. ");
                }
                else if (component is Capacitor)
                {
                    actions.Append("На схему был добавлен конденсатор. ");
                }
                else if (component is Conductor)
                {
                    actions.Append("На схему был добавлен проводник. ");
                }
                else if (component is Resistor)
                {
                    actions.Append("На схему был добавлен резистор. ");
                }
                else if (component is Rheostat)
                {
                    actions.Append("На схему был добавлен реостат. ");
                }
                else if (component is SingleSwitch)
                {
                    actions.Append("На схему был добавлен одиночный переключатель. ");
                }
                else if (component is DoubleSwitch)
                {
                    actions.Append("На схему был добавлен двойной переключатель. ");
                }
                else if (component is Toggle)
                {
                    actions.Append("На схему был добавлен ключ. ");
                }
                else if (component is HeatingArea)
                {
                    actions.Append("На схеме была сформирована область изменения температуры. ");
                }
                else if (component is Lamp)
                {
                    actions.Append("На схему была добавлена лампочка. ");
                }
                else if (component is Stopwatch)
                {
                    actions.Append("На раюочую область был добавлен секундомер. ");
                }
            }
            else if (typeAction == TypeAction.Delete)
            {
                if (component is Ammeter)
                {
                    actions.Append("Из схемы был удален амперметр. ");
                }
                else if (component is Voltmeter)
                {
                    actions.Append("Из схемы был удален вольтметр. ");
                }
                else if (component is VoltageSource)
                {
                    actions.Append("Из схемы был удален источник напряжения. ");
                }
                else if (component is Multimeter)
                {
                    actions.Append("Из схемы был удален мультиметр. ");
                }
                else if (component is Capacitor)
                {
                    actions.Append("Из схемы был удален конденсатор. ");
                }
                else if (component is Conductor)
                {
                    actions.Append("Из схемы был удален проводник. ");
                }
                else if (component is Resistor)
                {
                    actions.Append("Из схемы был удален резистор. ");
                }
                else if (component is Rheostat)
                {
                    actions.Append("Из схемы был удален реостат. ");
                }
                else if (component is SingleSwitch)
                {
                    actions.Append("Из схемы был удален одиночный переключатель. ");
                }
                else if (component is DoubleSwitch)
                {
                    actions.Append("Из схемы был удален двойной переключатель. ");
                }
                else if (component is Toggle)
                {
                    actions.Append("Из схемы был удален ключ. ");
                }
                else if (component is HeatingArea)
                {
                    actions.Append("Из схемы была удалена область изменения температуры. ");
                }
                else if (component is Lamp)
                {
                    actions.Append("Из схемы была удалена лампочка. ");
                }
                else if (component is Stopwatch)
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
        }

        /// <summary>
        /// Добавление в память измененного значения мультиметра
        /// </summary>
        public void AddChangesValueMultimeter(Multimeter multimeter, string value)
        {
            actions.Append("Показания на мультиметре были установленны в " + value + " " + multimeter.GetUnit() + ". ");
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

        /// <summary>
        /// Добавление таблицы в отчет
        /// </summary>
        /// <param name="columns">Массив названий столбцов</param>
        public void AddTable(string[] columns)
        {
            int heightRows = 25;

            document.InsertParagraph("Таблица значений элементов цепи").
                SpacingBefore(15).
                SpacingAfter(10).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                Bold().
                SpacingLine(15.5).
                Kerning(14).
                Alignment = Alignment.center;

            // Добавление заголовков столбцов в таблицу
            var columnWidths = new float[columns.Length];
            for (int i = 0; i< columns.Length; ++i) 
            {
                columnWidths[i] = 150f;           
            }
            table = document.InsertTable(1, columns.Length);

            table.SetWidths(columnWidths);
            table.AutoFit = AutoFit.Contents;

            var row = table.Rows.First();
            row.Height = heightRows;

            // заполнение заголовков столбцов
            for (int i = 0; i < row.Cells.Count; ++i)
            {
                row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[i].Paragraphs.
                    First().
                    Append(columns[i]).
                    Bold().
                    Font("Times New Roman").
                    FontSize(14).
                    Alignment = Alignment.left;
            }

            document.InsertParagraph().
                SpacingBefore(10).
                SpacingAfter(5);

            document.Save();
        }

        /// <summary>
        /// Запись значений в таблицу в отчете
        /// </summary>
        /// <param name="value">Массив значений столбцов</param>
        public void AddValuesToTable<T>(T[] value)
        {
            int heightRows = 25;
            var newRow = table.InsertRow();
            newRow.Height = heightRows;

            for (int i = 0; i<table.ColumnCount; ++i)
            {
                var newCell = newRow.Cells[i];
                newCell.Paragraphs.
                            First().
                            Append(Convert.ToString(value[i])).
                            Font("Times New Roman").
                            FontSize(14).
                            Alignment = Alignment.left;
                // Remove the left margin of the new cells.
                newCell.VerticalAlignment = VerticalAlignment.Center;
            }

            document.Save();
        }

        /// <summary>
        /// Запись строки действий в отчет
        /// </summary>
        public void AddActionStringToReport() 
        {
            paragraph = document.InsertParagraph(Convert.ToString(actions)).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14);
            paragraph.IndentationFirstLine = 1.5f;
            paragraph.Alignment = Alignment.both;

            document.Save();
            actions.Clear();
        }

        /// <summary>
        /// Добавление вывода в отчет
        /// </summary>
        /// <param name="resultString">Текст вывода</param>
        public void AddСonclusion(string resultString) 
        {
            document.InsertParagraph("Вывод:").
                SpacingBefore(20).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14).
                Alignment = Alignment.center;

            var p2 = document.
                InsertParagraph(resultString).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14);
            p2.IndentationFirstLine = 1.5f;
            p2.Alignment = Alignment.both;

            document.Save();
        }

        /// <summary>
        /// Запись ответов на контрольные вопросы в отчет
        /// </summary>
        public void AddControlQuestionsToReport(string[] questions, string[] answers)
        {
            for (int i = 0; i < questions.Length; ++i)
            {
                paragraph = document.
                    InsertParagraph(questions[i]).
                    Font("Times New Roman").
                    FontSize(14).
                    Color(Color.Black).
                    SpacingLine(15.5).
                    Kerning(14);
                paragraph.IndentationFirstLine = 1.5f;
                paragraph.Alignment = Alignment.left;

                paragraph = document.
                    InsertParagraph(answers[i]).
                    SpacingAfter(10).
                    Font("Times New Roman").
                    FontSize(14).
                    Color(Color.Black).
                    SpacingLine(15.5).
                    Kerning(14);
                paragraph.IndentationFirstLine = 1.5f;
                paragraph.Alignment = Alignment.both;

                document.Save();
            }
        }
    }
}
