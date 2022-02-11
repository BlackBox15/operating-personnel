using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace operating_personnel
{
    public partial class Form1 : Form
    {
        // Constructor
        public Form1()
        {
            InitializeComponent();
			ContentOfWork();
		}

        // Method
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void ContentOfWork() 
        {
            // Получение текущей даты.
            var date = DateTime.Today.ToShortDateString();
            // Получение номера текущего дня.
            var numberDay = Convert.ToString(DateTime.Today.ToShortDateString().Substring(0, 2));

			// Вывод текущей директории. Для отладки.
			//Content_of_work.Text = ("path to the current dir is " + Directory.GetCurrentDirectory());

			// Вывод на лейбл.
			Content_of_work.Text = $"{ Convert.ToString(date)} \n {NumberDayOfMonthByRussian()} месяца \n \n" +
								   $"{SearchWork(numberDay)} \n" +
								   $"{SearchWork(NumberDayOfMonthByRussian())}" +
								   $"{SearchWork(AmOrPm())}";
			//return Content_of_work.Text; 
		}


        // Работа с файлом-эксель.
        public static string SearchWork(string searchTerm)
        {
            // Путь к файлу.
            var file = new FileInfo(@"..\..\Excel file\ex.xlsx");
            List<string> arrCellWithNumber = new List<string>();
            string nameOfColumn = "C";


            // Использование ExcelPackage в некоммерческих целях при использовании его в Отладчике.
            // Для этого нужно указать LicenseContext.NonCommercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Используем using-statement для корректной работы с неуправляемыми ресурсами.
            using (var package = new ExcelPackage(file))
            {
                for (int i = 0; i <= 25; i++)
                {
                    // Формирование номеров полей (1..25).
                    string numOfField = Convert.ToString(i + 1);

                    // Чтение поля из таблицы
                    var collumnC = Convert.ToString(package.Workbook.Worksheets["Лист1"].Cells[nameOfColumn + numOfField].Value);

                    // Проверка наличия в поле аргумента метода.
                    var contentOfCell = collumnC.Contains(searchTerm);
                    if (contentOfCell == true)
                    {
                        // Чтение из таблицы поля B, D.
                        var collumnB = Convert.ToString(package.Workbook.Worksheets["Лист1"].Cells["B" + numOfField].Value);
                        var collumnD = Convert.ToString(package.Workbook.Worksheets["Лист1"].Cells["D" + numOfField].Value);
                        arrCellWithNumber.Add("- " + collumnB + "\n" + "   " + collumnD);
                    }
                }
                return String.Join("\n", arrCellWithNumber);
            }
        }

        private string TranslateDayOfWeekToRussian(string englishDay)
        {
            string[] arrEnglishDays = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
            string[] arrRussianhDays = {"Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье"};
            
            for (int i = 0; i <= arrEnglishDays.Length; i++)
                if (arrEnglishDays[i] == englishDay)
                    return arrRussianhDays[i];
            return "error";
        }

        public static int SearchTheDayOfTheWeekNumber()
        {
            int numberOfDay;
            var numberOfDayOfMonth = Convert.ToInt32(DateTime.Today.ToShortDateString().Substring(0,2));
            for (numberOfDay = 0; numberOfDayOfMonth >= 1; numberOfDay++)
                numberOfDayOfMonth = numberOfDayOfMonth - 7;
            return numberOfDay;
        }

        private string ChoiceOfEnding()
        {
            string[] maleDay = { "понедельник", "вторник", "четверг" };
            string[] maleEnding = { "ый", "ой", "ий", "ый", "ый" };
            string[] femaleDay = { "среда", "пятница", "суббота" };
            string[] femaleEnding = { "ая", "ая", "ья", "ая", "ая" };
            string[] middleEnding = { "ое", "ое", "ье", "ое", "ое" };

            foreach (var i in maleDay)
                if (TranslateDayOfWeekToRussian(Convert.ToString(DateTime.Today.DayOfWeek)).ToLower() == i)
                    return maleEnding[SearchTheDayOfTheWeekNumber() - 1];

            foreach (var i in femaleDay)
                if (TranslateDayOfWeekToRussian(Convert.ToString(DateTime.Today.DayOfWeek)).ToLower() == i)
                    return femaleEnding[SearchTheDayOfTheWeekNumber() - 1];

            if (TranslateDayOfWeekToRussian(Convert.ToString(DateTime.Today.DayOfWeek)).ToLower() == "воскресенье")
                return middleEnding[SearchTheDayOfTheWeekNumber() - 1];
            return "error";
        }

       public string NumberDayOfMonthByRussian()
        {
            var numberOfDay = Convert.ToString(DateTime.Today.ToShortDateString().Substring(0, 2));
            var day = TranslateDayOfWeekToRussian(Convert.ToString(DateTime.Today.DayOfWeek));
            string[] numberOfDayWithoutEndingInRussian = { "Перв", "Втор", "Трет", "Четверт", "Пят" };
            for (int i = 0; SearchTheDayOfTheWeekNumber() <= 5; i++)
            {
                if (i == SearchTheDayOfTheWeekNumber())
                    return numberOfDayWithoutEndingInRussian[i - 1] + ChoiceOfEnding() + " "+ day.ToLower();
            }

            return null;
        }


        public static string AmOrPm()
        {
            var timeNow = Convert.ToInt32(DateTime.Now.ToShortTimeString().Substring(0,2));
            if (timeNow >= 8 && timeNow <= 20) return "Ежедневно в дневную";
            return "Ежедневно в ночную";
        }
    }
}
//var time = Convert.ToString(DateTime.Today.ToShortDateString().Substring(0, 2));