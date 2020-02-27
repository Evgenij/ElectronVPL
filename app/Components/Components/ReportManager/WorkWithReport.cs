using System;
using System.Linq;
using System.Drawing;
using Xceed.Document.NET;
using System.Text;
using Xceed.Words.NET;
using System.Windows.Forms;

namespace Components
{
    class WorkWithReport : ReportManager
    {
        public WorkWithReport()
        {
            string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "fileExample.docx";

            // создаём документ
            document = DocX.Create(pathDocument);
            // Добавление объекта для работы с верхним колонтитулом
            document.AddHeaders();
            // Добавление объекта для работы с нижним колонтитулом
            document.AddFooters();
            // Установка в документе разбиение колонтитулов по страницам
            document.DifferentFirstPage = true;
        }

        /*рефакторинг - добавить группу студента как параметр 
        и в этом конструкторе вызывать методы создания колонтитулов */
        public WorkWithReport(string surname, string name, int numLab)
        {
            string pathDocument = AppDomain.CurrentDomain.BaseDirectory +
                surname + "_" +
                name + "_Lab" +
                Convert.ToString(numLab) + ".docx";

            // создаём документ
            document = DocX.Create(pathDocument);
            // Добавление объекта для работы с верхним колонтитулом
            document.AddHeaders();
            // Добавление объекта для работы с нижним колонтитулом
            document.AddFooters();
            // Установка в документе разбиение колонтитулов по страницам
            document.DifferentFirstPage = true;
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
        /// Добавление в отчет нижнего колонтитула
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
            for (int i = 0; i < columns.Length; ++i)
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

            for (int i = 0; i < table.ColumnCount; ++i)
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
        /// Запись строки действий в отчет
        /// </summary>
        public void AddActionsToReport()
        {
            paragraph = document.InsertParagraph(Convert.ToString(WorkWithElements.actions)).
                Font("Times New Roman").
                FontSize(14).
                Color(Color.Black).
                SpacingLine(15.5).
                Kerning(14);
            paragraph.IndentationFirstLine = 1.5f;
            paragraph.Alignment = Alignment.both;

            document.Save();
            WorkWithElements.actions.Clear();
        }

        /// <summary>
        /// Запись ответов на контрольные вопросы в отчет
        /// </summary>
        public void AddQuestionsToReport(string[] questions, string[] answers)
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
