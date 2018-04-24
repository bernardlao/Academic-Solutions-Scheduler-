using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using Academic_Solution_NPC;

namespace MyExcelClass
{
    class MyExcel
    {
        //User-defined values for printing.
        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd);

        public MyExcel()
        {

        }

        public void PrintReport(MyClass mc)
        {
            Excel.Application oXL;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;
            Excel.Range oRng;
            object oMissing = Missing.Value;

            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = oXL.Workbooks._Open(Application.StartupPath + "\\Template.xlsx", oMissing, oMissing, oMissing,
                                          oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                                          oMissing, oMissing, oMissing);

                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                //oSheet.Cells[row,col] = " data "
                oSheet.Cells[3, 1] = oSheet.Cells[3, 1].Text + " " + mc.Course + (mc.Major != null ? " " + mc.Major : "");
                oSheet.Cells[4, 1] = oSheet.Cells[4, 1].Text + " " + mc.YearLevel + mc.Section;
                oSheet.Cells[3, 8] = oSheet.Cells[3, 8].Text + " " + mc.SchoolYear;
                oSheet.Cells[4, 8] = oSheet.Cells[4, 8].Text + " " + (mc.Semester ? "1st Sem" : "2nd Sem");
                List<ListViewItem> items = new List<ListViewItem>();
                foreach (MySubject msj in mc.SelectedSubjects)
                    items.AddRange(GetSchedules(msj));
                int counter = 6;
                for (int i = 0; i < items.Count; i++ )
                {
                    ListViewItem itm = items[i];
                    oSheet.Cells[counter, 1] = itm.Text;
                    oSheet.Cells[counter, 3] = itm.SubItems[1].Text;
                    oSheet.Cells[counter, 8] = itm.SubItems[2].Text;
                    oSheet.Cells[counter, 10] = itm.SubItems[3].Text;
                    oSheet.Cells[counter, 12] = itm.SubItems[4].Text;
                    oSheet.Cells[counter, 12].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    counter++;
                    if ((i + 2) < items.Count)
                    {
                        Excel.Range r = oSheet.get_Range("A" + counter, "L" + counter);
                        Excel.Range r2 = oSheet.get_Range("A" + (counter + 1), "L" + (counter + 1));
                        r2.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        r2.Copy(r);
                    }
                    if ((i + 1) < items.Count)
                    {
                        if (items[i + 1].Text.Equals(itm.Text))
                        {
                            Excel.Range r = oSheet.get_Range("C" + counter, "C" + (counter - 1));
                            r.Merge(Missing.Value);
                            r.VerticalAlignment = HorizontalAlignment.Center;
                            r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            
                            Excel.Range r2 = oSheet.get_Range("A" + counter, "A" + (counter - 1));
                            r2.Merge(Missing.Value);
                            r2.VerticalAlignment = HorizontalAlignment.Center;
                            r2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                    }
                }

                if (!System.IO.Directory.Exists("C:\\Academic Solution\\"))
                    System.IO.Directory.CreateDirectory("C:\\Academic Solution\\");
                File.WriteAllText("C:\\Academic Solution\\Readme.txt", "The schedule print-outs soft copy will be saved in this destination.");
                oWB.SaveAs("C:\\Academic Solution" + "\\ClassSchedule_" + DateTime.Now.ToShortDateString().Replace("/", "") + DateTime.Now.ToLongTimeString().Replace(":", "") + ".xlsx",
                          oMissing, oMissing, oMissing, oMissing,
                          oMissing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                          oMissing, oMissing, oMissing, oMissing, oMissing);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private List<ListViewItem> GetSchedules(MySubject msj)
        {
            List<MySchedule> msc = msj.Schedules;
            List<MySchedule> unique = new List<MySchedule>();
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (MySchedule m in msc)
            {
                List<MySchedule> inUnique = unique.Where(s => s.FromTime.TimeOfDay == m.FromTime.TimeOfDay && s.ToTime.TimeOfDay == m.ToTime.TimeOfDay && s.Room.Equals(m.Room)).Select(s => s).ToList();
                if (inUnique.Count == 0)
                {
                    unique.Add(m);
                    List<MySchedule> getAllSameTime = msc.Where(s => s.FromTime.TimeOfDay == m.FromTime.TimeOfDay && s.ToTime.TimeOfDay == m.ToTime.TimeOfDay && s.Room.Equals(m.Room)).OrderBy(s=>s.Days).Select(s => s).ToList();
                    string subjectCode = msj.SubjectID;
                    string subjectDesctiption = msj.SubjectDescription;
                    string days = string.Empty;
                    string time = getAllSameTime[0].FromTime.ToString("hh:mm tt") + " - " + getAllSameTime[0].ToTime.ToString("hh:mm tt");
                    string room = getAllSameTime[0].Room;
                    if (getAllSameTime.Count > 1)
                    {
                        foreach (MySchedule sched in getAllSameTime)
                            days += GetDayEquivalent(sched.Days, true);
                    }
                    else
                        days = GetDayEquivalent(m.Days, false);
                    ListViewItem itm = new ListViewItem(subjectCode);
                    itm.SubItems.Add(subjectDesctiption);
                    itm.SubItems.Add(days);
                    itm.SubItems.Add(time);
                    itm.SubItems.Add(room);
                    items.Add(itm);
                }
            }
            return items;
        }
        private string GetDayEquivalent(string day, bool isMultiple)
        {
            switch (day)
            {
                case "1": return (isMultiple ? "M" : "MON");
                case "2": return (isMultiple ? "T" : "TUE");
                case "3": return (isMultiple ? "W" : "WED");
                case "4": return (isMultiple ? "TH" : "THU");
                case "5": return (isMultiple ? "F" : "FRI");
                case "6": return (isMultiple ? "S" : "SAT");
                case "7": return (isMultiple ? "Su" : "SUN");
                default: return null;
            }
        }
        //private void PrintReportToExcel()
        //{
        //    Excel.Application oXL;
        //    Excel.Workbook oWB;
        //    Excel.Worksheet oSheet;
        //    Excel.Range oRng;
        //    object oMissing = Missing.Value;

        //    try
        //    {
        //        //Start Excel and get Application object.
        //        oXL = new Excel.Application();
        //        oXL.Visible = true;

        //        //Get a new workbook.
        //        oWB = oXL.Workbooks._Open(@"C:\reports\report.xlsx", oMissing, oMissing, oMissing,
        //                                  oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
        //                                  oMissing, oMissing, oMissing);

        //        oSheet = (Excel.Worksheet)oWB.ActiveSheet;
        //        //oSheet.Cells[row,col] = " data "
        //        oSheet.Cells[4, 5] = DateTime.Now.ToLongDateString(); //Print Date on excel row 4, column F (6)

        //        int counter = 8;

        //        //foreach (DataRow row in dataTable.Rows)
        //        //{
        //        //    oSheet.Cells[counter, 2] = row["studentno"].ToString();
        //        //    oSheet.Cells[counter, 3] = row["fname"].ToString() + " " + row["lname"].ToString();
        //        //    oSheet.Cells[counter, 6] = row["course"].ToString();
        //        //    counter++;
        //        //    Excel.Range r = oSheet.get_Range("A" + counter, "F" + counter).EntireRow;
        //        //    r.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
        //        //}

        //        oWB.SaveAs(@"C:\reports\report_" + DateTime.Now.Second + ".xlsx", oMissing, oMissing, oMissing, oMissing,
        //                  oMissing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        //                  oMissing, oMissing, oMissing, oMissing, oMissing);

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}
        public void Print(string path)
        {
            ShellExecute(IntPtr.Zero, "print", path, "", "", ShowCommands.SW_SHOWNOACTIVATE);
        }
    }
}
