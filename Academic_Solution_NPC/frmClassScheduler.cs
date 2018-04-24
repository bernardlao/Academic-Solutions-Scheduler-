using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academic_Solution_NPC
{
    public partial class frmClassScheduler : Form
    {
        public int userID = -2;
        private string currentClassID = "-1";
        private string currentSubjectIndex = "-1";
        private double totalTime = 0;
        private SQLUtil db = new SQLUtil();
        private bool isRegular = true;
        private ListViewColumnSorter colSort;

        private MyClass mc;
        
        public frmClassScheduler()
        {
            InitializeComponent();
            colSort = new ListViewColumnSorter();
            lstSubjects.ListViewItemSorter = colSort;
        }
        /// <summary>
        /// Close Form
        /// </summary>
        private void menuExit_Click(object sender, EventArgs e)
        {
            if (mc == null)
            {
                this.Close();
                return;
            }
            if (mc.HasUnsaved())
            {
                if (DialogResult.Yes == MessageBox.Show("You have unsaved changes. Are you sure you want to leave without saving?", "Has Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void frmClassScheduler_Load(object sender, EventArgs e)
        {
            cmbSection.SelectedItem = cmbSection.Items[0];
            PopulateCourse();
            PopulateSubjects("");
            PopulateProfessors();
            mc = null;
        }
        /// <summary>
        /// Populates Course Offered
        /// </summary>
        private void PopulateCourse()
        {
            cmbCourse.DataSource = null;
            db.BindComboboxItems("SELECT * FROM tblCourse ORDER BY 1", cmbCourse, "CourseID", "CourseCode");
            PopulateMajor();
            PopulateYear();
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateMajor();
            PopulateYear();
        }
        private void ClassSelectionChange()
        {
            ClearDisableSelectedSubject();
            mc = null;
        }
        /// <summary>
        /// Populates Major per Course
        /// </summary>
        private void PopulateMajor()
        {
            string id = cmbCourse.SelectedValue.ToString();
            cmbMajor.DataSource = null;
            db.BindComboboxItems("SELECT * FROM tblMajor WHERE CourseID='" + id + "' ORDER BY 1", cmbMajor, "MajorID", "MajorCode");
        }
        /// <summary>
        /// Populates year per Course
        /// </summary>
        private void PopulateYear()
        {
            string id = cmbCourse.SelectedValue.ToString();
            int count = Convert.ToInt32(db.DataLookUp("NoOfYears", "tblCourse", "4", "CourseID='" + id + "'"));
            cmbYear.Items.Clear();
            for (int i = 1; i <= count; i++)
                cmbYear.Items.Add(i.ToString());
            cmbYear.SelectedItem = cmbYear.Items[0];
        }
        /// <summary>
        /// Load Subjects in Selection
        /// </summary>
        private void PopulateSubjects(string key)
        {
            lstSubjects.Items.Clear();
            DataTable dt = db.SelectTable("SELECT * FROM tblSubjects WHERE SubjectID LIKE '%" + key + "%' OR SubjectDescription LIKE '%" + key + "%' ORDER BY 1");
            lstSubjects.View = View.Details;
            foreach (DataRow r in dt.Rows)
            {
                ListViewItem itm = new ListViewItem(r["SubjectID"].ToString());
                itm.SubItems.Add(r["SubjectDescription"].ToString());
                itm.SubItems.Add(r["Prerequisite"].ToString());
                itm.SubItems.Add(r["NoUnits"].ToString());
                lstSubjects.Items.Add(itm);
            }
        }
        /// <summary>
        /// Populates Professor Selection
        /// </summary>
        private void PopulateProfessors()
        {
            cmbProfessors.DataSource = null;
            db.BindComboboxItems("SELECT ProfessorID, CONCAT(LastName,', ',FirstName,' ',IF(MiddleName IS Null,'',MiddleName)) AS 'FullName' FROM tblProfessor ORDER BY 1", cmbProfessors, "ProfessorID", "FullName");
        }
        /// <summary>
        /// Get Current School Year in System Variable
        /// </summary>
        /// <returns>####-####</returns>
        private string GetSchoolYear()
        {
            string tempSY = db.DataLookUp("Value", "tblSystemVariable", "", "`[Key]`='SchoolYear'");
            if (tempSY.Equals(""))
            {
                MessageBox.Show("Please consider setting the School Year in your SytemVariable.", "No School Year Set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
                return tempSY;
        }
        /// <summary>
        /// Clears Selected Subjects and Schedule List. Set Current class as none and disables functionalities.
        /// </summary>
        private void ClearDisableSelectedSubject()
        {
            lstSelectedSubjects.Items.Clear();
            lstSched.Items.Clear();
            gpbSelectedSubjects.Enabled = false;
            btnAddToSelected.Enabled = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(IsSchedValid())
            {
                string ID = lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text;
                mc.AddScheduleIfPossible(ID, GetSelectedDay(), dtpFrom.Value, dtpTo.Value, txtRoom.Text, lstSched);
            }
        }
        private string GetSelectedDay()
        {
            string day = (optMon.Checked ? "1" : (OptTue.Checked ? "2" : (optWed.Checked ? "3" : (optThu.Checked ? "4" : (optFri.Checked ? "5" : (optSat.Checked ? "6" : (optSun.Checked ? "7" : null)))))));
            return day;
        }
        private bool IsSchedValid()
        {
            if (txtRoom.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please specify the room of the schedule you are setting.", "Insufficient Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (currentSubjectIndex.Equals("-1"))
            {
                MessageBox.Show("Please select a subject first.", "No Class Subject Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void optRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (optRegular.Checked)
            {
                cmbCourse.Enabled = true;
                cmbMajor.Enabled = true;
                cmbSection.Enabled = true;
                cmbYear.Enabled = true;
                isRegular = true;
                lstSelectedSubjects.Items.Clear();//Clears Selected Subject Listview when opted as Regular from Irregular
                btnDisplayRecords.Enabled = true;
            }
            else if(optIrregular.Checked)
            {
                cmbCourse.Enabled = false;
                cmbMajor.Enabled = false;
                cmbSection.Enabled = false;
                cmbYear.Enabled = false;
                isRegular = false;
                btnDisplayRecords.Enabled = true;
            }
            ClassSelectionChange();
        }
        
        /// <summary>
        /// For Column Sort
        /// </summary>
        private void lstSubjects_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == colSort.SortColumn)
            {
                if (colSort.Order == SortOrder.Ascending)
                {
                    colSort.Order = SortOrder.Descending;
                }
                else
                {
                    colSort.Order = SortOrder.Ascending;
                }
            }
            else
            {
                colSort.SortColumn = e.Column;
                colSort.Order = SortOrder.Ascending;
            }
            this.lstSubjects.Sort();
        }

        private void btnAddToSelected_Click(object sender, EventArgs e)
        {
            if (lstSubjects.CheckedItems.Count > 0)
            {
                GetPossibleSubject();
            }
            else
                MessageBox.Show("Please select subject(s) first.", "No subject selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Get all Possible Subject. Can be duplicated for Irregular
        /// </summary>
        private void GetPossibleSubject() 
        {
            ListView.CheckedListViewItemCollection items = lstSubjects.CheckedItems;
            if (lstSelectedSubjects.Items.Count == 0 || !isRegular)
            {
                foreach (ListViewItem i in items)
                {
                    //ListViewItem itm = new ListViewItem("0");
                    //itm.SubItems.Add(i.SubItems[0].Text);
                    //itm.SubItems.Add(i.SubItems[1].Text);
                    //itm.SubItems.Add(i.SubItems[3].Text);
                    //lstSelectedSubjects.Items.Add(itm);
                    mc.AddSubject(i.SubItems[0].Text, i.SubItems[1].Text, i.SubItems[3].Text,
                        Convert.ToInt32(db.DataLookUp("(LectureHours + LaboratoryHours)", "tblSubjects", "0", "SubjectID='" + i.SubItems[0].Text + "'")));
                    mc.ViewAllSubjectItems(lstSelectedSubjects);
                }
            }
            else
            {
                List<MySubject> selectedSubjects = mc.SelectedSubjects;
                foreach (ListViewItem i in items)
                {
                    //Check every CheckedSubjects in Subjects does not exist in Selected Subjects
                    if (selectedSubjects.Where(s => s.SubjectID.Equals(i.SubItems[0].Text)).Select(s => s).ToList().Count == 0)
                    {
                        mc.AddSubject(i.SubItems[0].Text, i.SubItems[1].Text, i.SubItems[3].Text,
                        Convert.ToInt32(db.DataLookUp("(LectureHours + LaboratoryHours)", "tblSubjects", "0", "SubjectID='" + i.SubItems[0].Text + "'")));
                        mc.ViewAllSubjectItems(lstSelectedSubjects);
                    }
                }
            }
            //Clears Check in Subjects Listview after Getting its possible entry
            ListView.CheckedIndexCollection indexCol = lstSubjects.CheckedIndices;
            foreach (int x in indexCol)
                lstSubjects.Items[x].Checked = false;
        }

        private void btnDisplayRecords_Click(object sender, EventArgs e)
        {
            if (mc == null)
                DisplayRecordIfExist();
            if (mc.HasUnsaved())
            {
                if (DialogResult.Yes == MessageBox.Show("You have unsaved changes. Are you sure you want to leave without saving?", "Has Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    DisplayRecordIfExist();
            }
            else
                DisplayRecordIfExist();

        }
        private void DisplayRecordIfExist()
        {
            ClearDisableSelectedSubject();
            mc = null;
            CreateClassIfNotExist();
            btnAddToSelected.Enabled = true;
            gpbSelectedSubjects.Enabled = true;
            currentSubjectIndex = "-1";
        }
        /// <summary>
        /// Creates a Record in ClassTable if not existing yet
        /// </summary>
        private void CreateClassIfNotExist()
        {
            string course = (!isRegular ? "CourseID IS NULL" : "CourseID=" + cmbCourse.SelectedValue);
            string major = (!isRegular ? "MajorID IS NULL" : (cmbMajor.SelectedValue == null ? "MajorID IS NULL" : "MajorID=" + cmbMajor.SelectedValue));
            string year = "YearLevel=" + (!isRegular ? "0" : cmbYear.SelectedItem.ToString());
            string section = "Section='" + (!isRegular ? "IRR" : cmbSection.SelectedItem.ToString()) + "'";
            string sem = "Semester=" + (opt1st.Checked ? "1" : "0");
            string sy = "SchoolYear='" + GetSchoolYear() + "'";

            string criteria = course + " AND " + major + " AND " + year + " AND " + section + " AND " + sem + " AND " + sy;
            string classID = db.DataLookUp("ClassID", "tblClass", null, criteria);
            if (classID == null)
            {
                //course = (!isRegular ? "NULL" : cmbCourse.SelectedValue.ToString());
                //major = (!isRegular ? "NULL" : (cmbMajor.SelectedValue == null ? "NULL" : cmbMajor.SelectedValue.ToString()));
                //year = (!isRegular ? "0" : cmbYear.SelectedItem.ToString());
                //section = (!isRegular ? "'IRR'" : "'" + cmbSection.SelectedItem.ToString() + "'");
                //sem = (opt1st.Checked ? "1" : "0");
                //sy = "'" + GetSchoolYear() + "'";
                //db.InsertQuery("INSERT INTO tblClass (CourseID,MajorID,YearLevel,Section,Semester,SchoolYear,LogBy,LogDate) VALUES(" +
                //    course + "," + major + "," + year + "," + section + "," + sem + "," + sy + "," + userID + ",NOW());");
                //DataRow r = db.GetLastInsertItem("tblClass", "ClassID");
                //currentClassID = r["ClassID"].ToString();
                mc = new MyClass(null, (!isRegular ? null : cmbCourse.SelectedValue.ToString()), (!isRegular ? null : (cmbMajor.SelectedValue == null ? null : cmbMajor.SelectedValue.ToString())),
                    Convert.ToInt32((!isRegular ? "0" : cmbYear.SelectedItem.ToString())), (!isRegular ? "IRR" : cmbSection.SelectedItem.ToString()),
                    (opt1st.Checked ? true : false), GetSchoolYear(),null);
            }
            else
            {
                //query
                PopulateHasRecord(classID);
            }
        }
        private void PopulateHasRecord(string classID)
        {
            string query = "SELECT * FROM tblClass WHERE ClassID=" + classID;
            DataTable dt = db.SelectTable(query);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    mc = new MyClass(r["ClassID"].ToString(), (r["CourseID"].ToString().Equals("") ? null : r["CourseID"].ToString()),
                        (r["MajorID"].ToString().Equals("") ? null : r["MajorID"].ToString()), Convert.ToInt32(r["YearLevel"].ToString()), r["Section"].ToString(),
                        (r["Semester"].ToString().Equals("1") ? true : false), r["SchoolYear"].ToString(), r["LogDate"].ToString());
                    mc.PopulateSubjectAndSchedule();
                    mc.ViewAllSubjectItems(lstSelectedSubjects);
                }
            }
        }
        private void lstSelectedSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSelectedSubjects.SelectedIndices.Count != 0)
            {
                if (mc.IsScheduleUnsaved((!currentSubjectIndex.Equals("-1") ? lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text : currentSubjectIndex)))
                {
                    DialogResult res = MessageBox.Show("The schedule of the selected subject is not yet Saved. Do you want to save before you proceed?", "Unsaved Progress",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        if (!txtMaxClassCount.Text.Trim().Equals(""))
                        {
                            if (mc.SaveSubjectScheduleIfPossible(lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text,
                                cmbProfessors.SelectedValue.ToString(), Convert.ToInt32(txtMaxClassCount.Text)))
                            {
                                currentSubjectIndex = lstSelectedSubjects.SelectedIndices[0].ToString();
                                SetSubjectScheduleIfExist();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Max class count specified.", "Cannot save data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        SetSelectedSubjectHighlights();
                    }
                    else if (res == DialogResult.No)
                    {
                        mc.ClearUnsavedSchedule(lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text);
                        currentSubjectIndex = lstSelectedSubjects.SelectedIndices[0].ToString();
                        SetSubjectScheduleIfExist();
                        SetSelectedSubjectHighlights();
                    }
                }
                else
                {
                    currentSubjectIndex = lstSelectedSubjects.SelectedIndices[0].ToString();
                    SetSubjectScheduleIfExist();
                    SetSelectedSubjectHighlights();
                }
            }
        }
        private void SetSelectedSubjectHighlights()
        {
            foreach (ListViewItem i in lstSelectedSubjects.Items)
                i.BackColor = SystemColors.Window;
            lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].BackColor = SystemColors.Highlight;
        }
        private void PreventUnSafeLeave()
        {
            if (mc.IsScheduleUnsaved((!currentSubjectIndex.Equals("-1") ? lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text : currentSubjectIndex)))
            {
                DialogResult res = MessageBox.Show("The schedule of the selected subject is not yet Saved. Do you want to save before you proceed?", "Unsaved Progress",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    //save true false. if false prevent leave.
                }
            }
        }
        private void SetSubjectScheduleIfExist()
        {
            string ID = lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].Text;
            if (mc.HasSchedule(ID))
            {
                btnUpdateSchedule.Text = "Update Schedule";
                mc.ViewAllScheduleBySubjectID(lstSched, ID);
                mc.SetProfessorAndMaxClassCount(cmbProfessors, txtMaxClassCount,ID);
            }
            else
            {
                btnUpdateSchedule.Text = "Save Schedule";
                lstSched.Items.Clear();
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lstSched.SelectedIndices.Count > 0)
            {
                string cSuID = lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text;
                string cScID = lstSched.Items[lstSched.SelectedIndices[0]].SubItems[0].Text;
                mc.DeleteSubjectScheduleByID(cSuID, cScID, lstSched);
            }
        }

        private void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            if (!currentSubjectIndex.Equals("-1"))
            {
                if (!txtMaxClassCount.Text.Trim().Equals(""))
                {
                    string classSubjectID = lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].Text;
                    mc.SaveSubjectScheduleIfPossible(classSubjectID, cmbProfessors.SelectedValue.ToString(), Convert.ToInt32(txtMaxClassCount.Text));
                }
                else
                {
                    MessageBox.Show("Please specify the Maximum student allowed per class.", "No Max Class Count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtMaxClassCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            if (mc != null)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to clear your selected subjects?", "Clear Selection", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    mc.ClearSubjects();
                    mc.ViewAllSubjectItems(lstSelectedSubjects);
                }
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            //MySubject m = mc.SelectedSubjects[0];
            //m.Schedules[0].ClassScheduleID = "1";
            //MessageBox.Show(mc.SelectedSubjects[0].Schedules[0].ClassScheduleID);
            if (mc != null)
            {
                if (mc.SaveClassIfPossible(userID))
                {
                    string cID = mc.ClassID;
                    PopulateHasRecord(cID);
                    mc.ViewAllSubjectItems(lstSelectedSubjects);
                    lstSched.Items.Clear();
                    currentSubjectIndex = "-1";
                }
            }
        }

        private void btnDeleteSchedule_Click(object sender, EventArgs e)
        {
            if (!currentSubjectIndex.Equals("-1"))
            {
                string ID = lstSelectedSubjects.Items[Convert.ToInt32(currentSubjectIndex)].SubItems[0].Text;
                mc.DeleteSubjectByID(ID);
                mc.ViewAllSubjectItems(lstSelectedSubjects);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string text = txtSearchKey.Text;
            text = text.Trim().Replace("'","''");
            PopulateSubjects(text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            PopulateSubjects("");
        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = txtSearchKey.Text;
                text = text.Trim().Replace("'", "''");
                PopulateSubjects(text);
            }
        }

        private void txtSearchKey_Click(object sender, EventArgs e)
        {
            txtSearchKey.SelectAll();
        }

        private void menuRooms_Click(object sender, EventArgs e)
        {
            frmRoomView rv = new frmRoomView();
            rv.ShowDialog();
        }

        private void menuPrint_Click(object sender, EventArgs e)
        {
            if (mc == null)
            {
                MessageBox.Show("There is no current Class to be printed.");
                return;
            }
            if (mc.YearLevel == 0)
            {
                MessageBox.Show("You can only print Regular Classes.");
                return;
            }
            if (mc.HasUnsaved())
            {
                MessageBox.Show("There are unsaved changes detected. Please be sure that the class is already saved/updated before printing","Unsaved Class");
                return;
            }
            MyExcelClass.MyExcel me = new MyExcelClass.MyExcel();
            me.PrintReport(mc);
        }
    }
}
