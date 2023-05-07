using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Text.Json;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Template_4333
{
    /// <summary>
    /// Логика взаимодействия для _4333_Gallyamov.xaml
    /// </summary>
    public partial class _4333_Gallyamov : Window
    {
        public _4333_Gallyamov()
        {
            InitializeComponent();
        }
        //Вторая лабораторная работа ИСРПО
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                DefaultExt = "*.xls;*.xlsx",
                Filter = "файл Excel (Spisok.xlsx)|*.xlsx",
                Title = "Выберите файл"
            };

            if (!(ofd.ShowDialog() == true))
                return;

            string[,] list;
            Excel.Application ObjWorkExcel = new Excel.Application();
            Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(ofd.FileName);
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
            var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            int _columns = (int)lastCell.Column;
            int _rows = (int)lastCell.Row;
            list = new string[_rows, _columns];
            for (int j = 0; j < _columns; j++)
                for (int i = 0; i < _rows; i++)
                    list[i, j] = ObjWorkSheet.Cells[i + 1, j + 1].Text;
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
            GC.Collect();

            using (isrpo2Entities2 isrpoEntities = new isrpo2Entities2())
            {
                for (int i = 0; i < _rows; i++)
                {
                    if (i == 0 || string.IsNullOrWhiteSpace(list[i, 0]))
                        continue;
                    isrpoEntities.tableispro2.Add(new tableispro2()
                    {
                        Айди = list[i, 0],
                        КодЗаказ = list[i, 1],
                        Датасоздания = list[i, 2],
                        Времязаказ = list[i, 3],
                        АйдиКлиент = list[i, 4],
                        Услуга = list[i, 5],
                        Статус = list[i, 6],
                        Датазакрытия = list[i, 7],
                        Времяпроката = list[i, 8]
                    });

                    isrpoEntities.SaveChanges();

                }
                MessageBox.Show("Успешный импорт");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<tableispro2> category_1;
            List<tableispro2> category_2;
            List<tableispro2> category_3;
            List<tableispro2> category_4;
            List<tableispro2> category_5;
            List<tableispro2> category_6;
            List<tableispro2> category_7;
            List<tableispro2> category_8;
            List<tableispro2> category_9;


            using (isrpo2Entities2 isrpoEntities = new isrpo2Entities2())
            {

                category_1 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "2 часа").ToList();
                category_2 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "4 часа").ToList();
                category_3 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "6 часов").ToList();
                category_4 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "320 минут").ToList();
                category_5 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "480 минут").ToList();
                category_6 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "10 часов").ToList();
                category_7 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "12 часов").ToList();
                category_8 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "120 минут").ToList();
                category_9 = isrpoEntities.tableispro2.Where(x => x.Времяпроката == "600 минут").ToList();
            }

            var allCategories = new List<List<tableispro2>>()
            {
                category_1,
                category_2,
                category_3,
                category_4,
                category_5,
                category_6,
                category_7,
                category_8,
                category_9
            };

            var app = new Excel.Application();
            app.SheetsInNewWorkbook = 9;
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);

            for (int i = 0; i < 9; i++)
            {
                int startRowIndex = 1;
                Excel.Worksheet worksheet = app.Worksheets.Item[i + 1];
                worksheet.Name = $"Категория {i + 1}";
                worksheet.Cells[1][startRowIndex] = "Айди";
                worksheet.Cells[1][startRowIndex].Font.Bold = true;
                worksheet.Cells[2][startRowIndex] = "КодЗаказ";
                worksheet.Cells[2][startRowIndex].Font.Bold = true;
                worksheet.Cells[3][startRowIndex] = "Датасоздания";
                worksheet.Cells[3][startRowIndex].Font.Bold = true;
                worksheet.Cells[4][startRowIndex] = "АйдиКлиент";
                worksheet.Cells[4][startRowIndex].Font.Bold = true;
                worksheet.Cells[5][startRowIndex] = "Услуга";
                worksheet.Cells[5][startRowIndex].Font.Bold = true;

                foreach (var person in allCategories[i])
                {
                    startRowIndex++;
                    worksheet.Cells[1][startRowIndex] = person.Айди;
                    worksheet.Cells[2][startRowIndex] = person.КодЗаказ;
                    worksheet.Cells[3][startRowIndex] = person.Датасоздания;
                    worksheet.Cells[4][startRowIndex] = person.АйдиКлиент;
                    worksheet.Cells[5][startRowIndex] = person.Услуга;
                }
                Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex]];
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle =
                    Excel.XlLineStyle.xlContinuous;

                worksheet.Columns.AutoFit();
            }

            app.Visible = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (isrpo2Entities2 isrpoEntities = new isrpo2Entities2())
            {
                foreach (var item in isrpoEntities.tableispro2)
                {
                    isrpoEntities.tableispro2.Remove(item);
                }
                isrpoEntities.SaveChanges();
            }
            MessageBox.Show("Удалено");
        }

        class gg
        {
            public int Id { get; set; }
            public string CodeOrder { get; set; }
            public string CreateDate { get; set; }
            public string CreateTime { get; set; }
            public string CodeClient { get; set; }
            public string Services { get; set; }
            public string Status { get; set; }
            public string ClosedDate { get; set; }
            public string ProkatTime { get; set; }

        }
        //Третья лабораторная работа по ИСРПО
    }
}