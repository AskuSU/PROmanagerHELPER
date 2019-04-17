using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelMS
{
    public class ExcelMSsupport
    {
        public void ExcelMSsupportStart()
        {
            //Передача данных по одной ячейке с помощью автоматизации
            //С помощью автоматизации вы можете передавать данные в рабочий лист Exel по одной ячейке

            // Start a new workbook in Excel.
            var m_objExcel = new Excel.Application();
            var m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
            var m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));

            // Add data to cells in the first worksheet in the new workbook.
            m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
            m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
            m_objRange = m_objSheet.get_Range("A1", m_objOpt);
            m_objRange.Value = "Last Name";
            m_objRange = m_objSheet.get_Range("B1", m_objOpt);
            m_objRange.Value = "First Name";
            m_objRange = m_objSheet.get_Range("A2", m_objOpt);
            m_objRange.Value = "Doe";
            m_objRange = m_objSheet.get_Range("B2", m_objOpt);
            m_objRange.Value = "John";

            // Apply bold to cells A1:B1.
            m_objRange = m_objSheet.get_Range("A1", "B1");
            m_objFont = m_objRange.Font;
            m_objFont.Bold = true;

            // Save the Workbook and quit Excel.
            m_objBook.SaveAs(m_strSampleFolder + "Book1.xls", m_objOpt, m_objOpt,
            m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
            m_objOpt, m_objOpt, m_objOpt, m_objOpt);
            m_objBook.Close(false, m_objOpt, m_objOpt);
            m_objExcel.Quit();
        }
    }
}
