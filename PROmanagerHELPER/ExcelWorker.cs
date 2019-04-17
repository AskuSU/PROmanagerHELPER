using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;
using PROmanagerHELPER.CoreRussProfil.KompanyType;
using Application = Microsoft.Office.Interop.Excel.Application;


namespace PROmanagerHELPER
{
    class ExcelWorker
    {
        private Application application;
        private Workbook workBook;
        private Worksheet worksheet;

        
        // Передаём данные в Excel
        public void WriteToExcel(List<IKOMPANY> list)
        {
            // Открываем приложение
            application = new Application
            {
                DisplayAlerts = false
            };

            // Файл шаблона
            const string template = "template.xlsx";

            // Открываем книгу
            workBook = application.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));

            // Получаем активную таблицу
            worksheet = workBook.ActiveSheet as Worksheet;

            // Записываем данные
            //worksheet.Range["A2"].Value = DateTime.Now;
            //worksheet.Range["A4"].Value = "Text1";
            //worksheet.Range["A6"].Value = "Text2";
            //for (int i = 0; i < checkedListData3.Items.Count; i++)
            //{
            //    worksheet.Cells[i + 8, 1].Value = checkedListData3.Items[i];
            //    worksheet.Cells[i + 8, 2].Value = checkedListData3.GetItemChecked(i) ? "Checked" : "Unchecked";
            //}

            int i = 0;
            foreach (var item in list)
            {
                worksheet.Cells[i + 2, 12].Value = item.ID;
                worksheet.Cells[i + 2, 5].Value = item.INN;
                worksheet.Cells[i + 2, 2].Value = item.Name;
                worksheet.Cells[i + 2, 11].Value = item.Adress.NotFuulAdress;
                i++;
            }

            // Показываем приложение
            application.Visible = true;
            //TopMost = true;

            //buttonCloseExcel.Enabled = true;
        }

        public void CloseExcelInProgramm()
        {
            // Сохраняем и закрываем 
            try
            {
                string savedFileName = "book1.xlsx";
                workBook.SaveAs(Path.Combine(Environment.CurrentDirectory, savedFileName));
                CloseExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Не сохранено!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseExcel();
            }
        }

        public void CloseExcel()
        {
            if (application != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(application.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.ReleaseComObject(application);

                application = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);
               
    }
}
