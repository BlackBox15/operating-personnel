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

        private string ContentOfWork() 
        {
            var date = DateTime.Today.ToShortDateString();
            var numberDay = Convert.ToString(DateTime.Today.ToShortDateString().Substring(0, 2));

            Content_of_work.Text = $"{ Convert.ToString(date)} \n {NumberDayOfMonthByRussian()} месяца \n \n" +
                                   $"{SearchWork(numberDay)} \n" +
                                   $"{SearchWork(NumberDayOfMonthByRussian())}" +
                                   $"{SearchWork(AmOrPm())}";
            return Content_of_work.Text; 
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
        public static string SearchWork(string searchTerm)
        {
            var file = new FileInfo(@"C:\Users\Vasil\source\repos\operating personnel\operating personnel\Excel file\ex.xlsx");
            List <string> arrCellWithNumber = new List<string>();
            string c = "C";
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(file))
            {
                for (int i = 0; i <= 25; i++)
                {
                    string n = Convert.ToString(i+1);
                    var collumnC = Convert.ToString(package.Workbook.Worksheets["Лист1"].Cells[c + n].Value);
                    var contentOfCell = collumnC.Contains(searchTerm);
                    if (contentOfCell == true)
                    {
                        var collumnB = Convert.ToString(package.Workbook.Worksheets["Лист1"].Cells["B"+n].Value);
                        var collumnD = Convert.ToString(package.Workbook.Worksheets["Лист1"].Cells["D"+n].Value);
                        arrCellWithNumber.Add("- " + collumnB + "\n" + "   " + collumnD);
                    }
                }
                return String.Join("\n", arrCellWithNumber);
            }
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