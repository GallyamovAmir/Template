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


    }
}