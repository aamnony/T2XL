using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace T2XL
{
    internal class XL
    {
        private const string LAST_ID_CELL = "K2";
        private const string LAST_ROW_CELL = "K1"; // for every sheet

        private string mPath;
        private Excel._Worksheet mActiveSheet;
        private Excel.Application mApplication;
        private Excel.ListObjects mListObjects;
        private Excel.Range mRange;
        private Excel._Workbook mWorkbook;
        private Excel.Workbooks mWorkbooks;
        private Excel.Sheets mWorksheets;

        public XL()
        {
        }

        public XL(int userId, string path, bool showUi)
        {
            mPath = path;
            //Start Excel and get Application object.
            mApplication = new Excel.Application();
            mApplication.Visible = true;
            mWorkbooks = mApplication.Workbooks;
            mWorkbook = mWorkbooks.Add(Missing.Value);
            mActiveSheet = mWorkbook.ActiveSheet;
            mWorksheets = mWorkbook.Worksheets;
            InitSheet("- " + userId);

            SaveAs();
            
            mApplication.Visible = showUi;
            mApplication.UserControl = showUi;
        }

        public XL(string path, bool showUi)
        {
            mPath = path;
            //Start Excel and get Application object.
            mApplication = new Excel.Application();
            mApplication.Visible = true;
            mWorkbooks = mApplication.Workbooks;
            mWorkbook = mWorkbooks.Open(path);
            mActiveSheet = mWorkbook.ActiveSheet;
            mWorksheets = mWorkbook.Worksheets;
            
            mApplication.Visible = showUi;
            mApplication.UserControl = showUi;
        }

        public void AddMessage(SimpleMessage m)
        {
            var id = "- " + m.Chat.Id.ToString();
            var chat = m.Chat.ToString();
            if (!mActiveSheet.Name.Contains(id))
            {
                FindSheetNameContaining(id);
                if (mActiveSheet == null)
                {
                    NewSheet();
                    InitSheet(chat);
                }
                else if (mActiveSheet.Name != chat)
                {
                    mActiveSheet.Name = chat;
                }
            }
            //int lastRow = oSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row + 1;
            int lastRow = (int)mActiveSheet.Range[LAST_ROW_CELL].Value2++;
            mActiveSheet.Cells[lastRow, 1] = m.Id;
            mActiveSheet.Cells[lastRow, 2] = m.Time;
            mActiveSheet.Cells[lastRow, 3] = m.User.Name;

            mActiveSheet.Cells[lastRow, 4].NumberFormat = "@";
            mActiveSheet.Cells[lastRow, 4] = m.Message;

            mRange = mWorksheets[1].Range[LAST_ID_CELL];
            mRange.Value2 = m.Id;
        }

        private void SaveAs()
        {
            mWorkbook.SaveAs(mPath, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }

        public int GetLastId()
        {
            mRange = mWorksheets[1].Range[LAST_ID_CELL];
            return (int)mRange.Value2;
        }

        public void Close()
        {
            mWorkbook.Close(true, Type.Missing, Type.Missing);
            mApplication.Quit();

            // Cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (mListObjects != null)
                Marshal.FinalReleaseComObject(mListObjects);
            if (mRange != null)
                Marshal.FinalReleaseComObject(mRange);
            if (mActiveSheet != null)
                Marshal.FinalReleaseComObject(mActiveSheet);
            if (mWorksheets != null)
                Marshal.FinalReleaseComObject(mWorksheets);
            if (mWorkbook != null)
                Marshal.FinalReleaseComObject(mWorkbook);
            if (mWorkbooks != null)
                Marshal.FinalReleaseComObject(mActiveSheet);
            if (mApplication != null)
                Marshal.FinalReleaseComObject(mApplication);
        }

        /// <summary>
        /// Find the first sheet that its name contains the given string.
        /// </summary>
        /// <param name="workbook">The workbook to search in</param>
        /// <param name="s">The given string</param>
        /// <returns>The sheet, or null if non is found</returns>
        internal void FindSheetNameContaining(string s)
        {
            foreach (Excel._Worksheet sheet in mWorksheets)
            {
                if (sheet.Name.EndsWith(s))
                {
                    mActiveSheet = sheet;
                    return;
                }
            }
            mActiveSheet = null;
        }

        private void InitSheet(string withName)
        {
            mActiveSheet.Name = withName;
            mActiveSheet.Cells[1, 1] = nameof(SimpleMessage.Id);
            mActiveSheet.Cells[1, 2] = nameof(SimpleMessage.Time);
            mActiveSheet.Cells[1, 3] = nameof(SimpleMessage.User);
            mActiveSheet.Cells[1, 4] = nameof(SimpleMessage.Message);

            mRange = mActiveSheet.Range["B:B"];
            mRange.NumberFormat = "DD.MM.YY hh:mm";

            Tablize();
            mRange = mActiveSheet.Range[LAST_ROW_CELL];
            mRange.Value2 = 2;
        }

        private void NewSheet()
        {
            int n = mWorksheets.Count;
            mWorksheets.Add(After: mWorksheets[n]);
            mActiveSheet = mWorksheets[n + 1];
        }

        private void Tablize()
        {
            mListObjects = mActiveSheet.ListObjects;

            mListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange,
                mActiveSheet.Range["A:D"],
                Type.Missing,
                Excel.XlYesNoGuess.xlYes,
                Type.Missing);
            //.Name = "TestTable";
            //oSheet.ListObjects["TestTable"].TableStyle = "TableStyleMedium3";
        }
    }
}