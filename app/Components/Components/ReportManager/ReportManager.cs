using System;
using System.Drawing;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.Text;

namespace Components
{
    class ReportManager
    {
        protected DocX document;
        protected Paragraph paragraph;
        protected Table table;
        protected static StringBuilder actions = new StringBuilder();

        public enum TypeAction { Add, Delete }
        public enum TypeChanges { Plus, Minus, On, Off }
        public enum SwitchingType { First, Second, Between }

        protected static string[] themesLabs = new string[5] {
            "тема лабы 1",
            "тема лабы 2",
            "тема лабы 3",
            "тема лабы 4",
            "тема лабы 5"
            };
        protected static string[] targetLabs = new string[5] {
            "цель лабы 1",
            "цель лабы 2",
            "цель лабы 3",
            "цель лабы 4",
            "цель лабы 5"
            };
    }
}
