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
    public partial class frmRoomView : Form
    {
        SQLUtil db = new SQLUtil();
        public frmRoomView()
        {
            InitializeComponent();
        }

        private void frmRoomView_Load(object sender, EventArgs e)
        {
            PopulateList(GetExtraCriteria());
        }
        private void PopulateList(string criteria)
        {
            lstSchedules.Items.Clear();
            string query = "SELECT su.SubjectID,su.SubjectDescription," +
                "CASE WHEN Days='1' THEN 'M' WHEN Days ='2' THEN 'T' WHEN Days='3' THEN 'W' WHEN Days ='4' THEN 'Th' WHEN Days='5' THEN 'F' WHEN Days='6' THEN 'S' WHEN Days ='7' THEN 'Su' END AS 'Days'," +
                " CONCAT(DATE_FORMAT(FromTime,'%h:%i %p'),' - ',DATE_FORMAT(ToTime,'%h:%i %p')) AS 'Time',CONCAT(p.LastName,', ',p.FirstName,IF(p.MiddleName IS NULL,'',CONCAT(' ',LEFT(p.MiddleName,1),'.'))) AS 'Professor'," +
                "IF(c.YearLevel = 0,'IRREGULAR',CONCAT(co.CourseCode,' ',c.YearLevel,' - ',c.Section,IF(c.MajorID IS NULL,'',CONCAT(' ',m.MajorCode)))) AS 'CYS' FROM (((((tblClass c INNER JOIN tblClassSubjects s ON c.ClassID=s.ClassID)" +
                " INNER JOIN tblClassSchedule sc ON s.ClassSubjectID=sc.ClassSubjectID) INNER JOIN tblSubjects su ON s.SubjectID=su.SubjectID) INNER JOIN tblProfessor p ON s.ProfessorID=p.ProfessorID) LEFT JOIN tblCourse co ON c.CourseID=co.CourseID)" +
                " LEFT JOIN tblMajor m ON c.MajorID=m.MajorID " + (criteria.Trim().Length > 0?"WHERE " + criteria:"");
            DataTable dt = db.SelectTable(query);
            if (dt != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    ListViewItem itm = new ListViewItem(r["SubjectID"].ToString());
                    itm.SubItems.Add(r["SubjectDescription"].ToString());
                    itm.SubItems.Add(r["Days"].ToString());
                    itm.SubItems.Add(r["Time"].ToString());
                    itm.SubItems.Add(r["Professor"].ToString());
                    itm.SubItems.Add(r["CYS"].ToString());
                    lstSchedules.Items.Add(itm);
                }
            }
        }
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
        private string GetExtraCriteria()
        {
            string extra = "c.Semester=" + (opt1st.Checked ? "1" : "0") + " AND c.SchoolYear='" + GetSchoolYear() + "'";
            return extra;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtRoom.Text;
            key = key.Trim().Replace("'","''");
            PopulateList(GetExtraCriteria() + " AND Room LIKE '%" + key + "%'");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string key = txtRoom.Text;
                key = key.Trim().Replace("'", "''");
                PopulateList(GetExtraCriteria() + " AND Room LIKE '%" + key + "%'");
            }
        }
    }
}
