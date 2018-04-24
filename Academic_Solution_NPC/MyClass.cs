using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academic_Solution_NPC
{
    class MyClass
    {
        SQLUtil db;
        private string classID;
        private string courseID;
        private string majorID;
        private int yearLevel;
        private string section;
        private bool semester;
        private string schoolYear;
        private string logDate;
        private List<MySubject> subjects;
        private List<string> subjectDeleteIDs;
        private List<string> permaDeleteIDs;
        private string CourseCode;
        private string MajorCode;

        private int subjectIDCounter;//Dont forget to reset when saved and update ID
        public MyClass(string ClassID, string CourseID, string MajorID, int YearLevel, string Section, bool Semester, string SchoolYear, string LogDate)
        {
            this.classID = ClassID;
            this.courseID = CourseID;
            this.majorID = MajorID;
            this.yearLevel = YearLevel;
            this.section = Section;
            this.semester = Semester;
            this.schoolYear = SchoolYear;
            this.logDate = LogDate;
            subjects = new List<MySubject>();
            subjectDeleteIDs = new List<string>();
            permaDeleteIDs = new List<string>();
            subjectIDCounter = 1;
            db = new SQLUtil();
            if (CourseID != null)
                CourseCode = db.DataLookUp("CourseCode", "tblCourse", null, "CourseID=" + CourseID);
            if (MajorID != null)
                MajorCode = db.DataLookUp("MajorCode", "tblMajor", null, "MajorID=" + MajorID);
        }

        //START of Setter/Getter or GetterOnly
        public string ClassID
        {
            set { this.classID = value; }
            get { return this.classID; }
        }
        public string CourseID { get { return this.courseID; } }
        public string MajorID { get { return this.majorID; } }
        public int YearLevel { get { return this.yearLevel; } }
        public string Section { get { return this.section; } }
        public bool Semester { get { return this.semester; } }
        public string SchoolYear { get { return this.schoolYear; } }
        public string Course { get { return this.CourseCode; } }
        public string Major { get { return this.MajorCode; } }
        public List<MySubject> SelectedSubjects {
            get
            {
                List<MySubject> mySubjectCollection = this.subjects.Where(s=>!subjectDeleteIDs.Contains(s.ClassSubjectID)).Select(s=>s).ToList();
                return mySubjectCollection;
            }
        }
        public List<string> PermanentDeleteList { get { return this.permaDeleteIDs; } }
        //END of Setter/Getter or GetterOnly

        /// <summary>
        /// Use for initial subject insertion without schedule
        /// </summary>
        /// <param name="SubjectID">Subject Code/ID</param>
        public void AddSubject(string SubjectID, string SubjectDescription, string Units, double HoursAllowed)
        {
            MySubject subject = new MySubject("temp" + subjectIDCounter, this.classID, SubjectID, null, -1,SubjectDescription,Units,HoursAllowed);
            this.subjects.Add(subject);
            subjectIDCounter++;
        }
        /// <summary>
        /// Use for existing subject with schedule/record
        /// </summary>
        public void AddSubject(string ClassSubjectID, string SubjectID, string ProfessorID, int MaxClassCount, string SubjectDescription, string Units, double HoursAllowed)
        {
            MySubject subject = new MySubject(ClassSubjectID, this.classID, SubjectID, ProfessorID, MaxClassCount,SubjectDescription,Units,HoursAllowed);
            subject.IsSaved = true;
            this.subjects.Add(subject);
        }
        /// <summary>
        /// Takes note of the temporary deletion of selected subject record
        /// </summary>
        public void DeleteSubjectByID(string ClassSubjectID)
        {
            //check if possible first if no enrolled

            if (!HasEnrolledStudent(ClassSubjectID))
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete the selected subject?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    subjectDeleteIDs.Add(ClassSubjectID);
            }
            else
                MessageBox.Show("The subject you want to delete has enrolled student(s). Please make sure to that the subject you want to delete doesn't have any enrolled student.",
                    "Subject Cannot be Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            

        }
        /// <summary>
        /// Accepts ListView and Populate it with existing subjects
        /// </summary>
        public void ViewAllSubjectItems(ListView lst)
        {
            lst.Items.Clear();
            try
            {
                List<MySubject> lms = subjects.Where(s => !subjectDeleteIDs.Contains(s.ClassSubjectID)).ToList();
                foreach (MySubject ms in lms)
                {
                    lst.Items.Add(ms.GetItem());
                }
            }
            catch {  }
        }
        /// <summary>
        /// Accepts ListView and populate it by schedules of specific subject
        /// </summary>
        public void ViewAllScheduleBySubjectID(ListView lst, string ClassSubjectID)
        {
            lst.Items.Clear();
            try
            {
                MySubject ms = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).Select(s => s).SingleOrDefault();
                List<ListViewItem> items = ms.GetAllScheduleItem();
                foreach (ListViewItem i in items)
                {
                    lst.Items.Add(i);
                }
            }
            catch { }
        }
        public void SetProfessorAndMaxClassCount(ComboBox cmb, TextBox txt, string ClassSubjectID)
        {
            MySubject ms = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
            if (ms != null)
            {
                if (ms.ProfessorID != null)
                    cmb.SelectedValue = ms.ProfessorID;
                if (ms.MaxClassCount != -1)
                    txt.Text = ms.MaxClassCount.ToString();
            }
        }
        /// <summary>
        /// Gets all temporary schedule
        /// </summary>
        private List<MySchedule> GetAllSchedulesOfAllSubjects()
        {
            List<MySchedule> allSchedule = new List<MySchedule>();
            foreach (MySubject ms in subjects)
            {
                allSchedule.AddRange(ms.Schedules);
            }
            return allSchedule;
        }
        /// <summary>
        /// First checking without professor and if do not lapse to its own allocated schedule
        /// </summary>
        public bool AddScheduleIfPossible(string ClassSubjectID, string day, DateTime from, DateTime to, string room,ListView lst)
        {//Include Class SY and Sem
            DateTime f = Convert.ToDateTime(from.ToString("hh:mm tt"));
            DateTime t = Convert.ToDateTime(to.ToString("hh:mm tt"));
            double diff = t.Subtract(f).TotalHours;
            if (diff < 0)
            {
                MessageBox.Show("The initial time must be earlier than the end time", "Invalid Time Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (diff < 1 && diff > 0)
            {
                MessageBox.Show("The allotted time must be atleast one(1) hour", "Invalid Time Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                double totalTime = 0;
                MySubject subj = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
                List<MySchedule> lms = subj.Schedules;
                if (lms.Count > 0)
                    totalTime = lms.Sum(s => s.AllocatedHours);
                if (totalTime + diff > subj.HoursAllowed)
                {
                    MessageBox.Show("The total hours you set will exceed the hours allowed by the subject", "Maximum Hours Exceed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //Local start
                List<MySchedule> sameDay = lms.Where(s => s.Days.Equals(day)).Select(s => s).ToList();
                List<MySchedule> lapse = lms.Where(s => (s.FromTime.TimeOfDay <= f.TimeOfDay && f.TimeOfDay < s.ToTime.TimeOfDay) ||
                    (s.FromTime.TimeOfDay < t.TimeOfDay && t.TimeOfDay <= s.ToTime.TimeOfDay) ||
                    (f.TimeOfDay <= s.FromTime.TimeOfDay && s.FromTime.TimeOfDay < t.TimeOfDay) ||
                    (f.TimeOfDay < s.ToTime.TimeOfDay && s.ToTime.TimeOfDay <= t.TimeOfDay)).Select(s => s).ToList();
                bool hasSameDay = (sameDay.Count == 0 ? false : true);
                bool hasLapse = (lapse.Count == 0 ? false : true);

                if (hasSameDay && hasLapse)
                {
                    MessageBox.Show("The schedule you want to set has a conflict to the set of schedule you already have.", "Failed Initial Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //allTempSched
                List<MySchedule> allSchedule = GetAllSchedulesOfAllSubjects();
                List<MySchedule> conflict;
                if (this.yearLevel == 0)
                {
                    conflict = allSchedule.Where(s => ((s.FromTime.TimeOfDay <= f.TimeOfDay && f.TimeOfDay < s.ToTime.TimeOfDay) ||
                        (s.FromTime.TimeOfDay < t.TimeOfDay && t.TimeOfDay <= s.ToTime.TimeOfDay) ||
                    (f.TimeOfDay <= s.FromTime.TimeOfDay && s.FromTime.TimeOfDay < t.TimeOfDay) ||
                    (f.TimeOfDay < s.ToTime.TimeOfDay && s.ToTime.TimeOfDay <= t.TimeOfDay))
                    && (s.Room.Equals(room) && (!s.Room.Equals("TBA", StringComparison.CurrentCultureIgnoreCase) && !s.Room.Equals("QDR", StringComparison.CurrentCultureIgnoreCase))) 
                    && s.Days.Equals(day)).ToList();
                }
                else
                {
                    conflict = allSchedule.Where(s => ((s.FromTime.TimeOfDay <= f.TimeOfDay && f.TimeOfDay < s.ToTime.TimeOfDay) ||
                        (s.FromTime.TimeOfDay < t.TimeOfDay && t.TimeOfDay <= s.ToTime.TimeOfDay) ||
                    (f.TimeOfDay <= s.FromTime.TimeOfDay && s.FromTime.TimeOfDay < t.TimeOfDay) ||
                    (f.TimeOfDay < s.ToTime.TimeOfDay && s.ToTime.TimeOfDay <= t.TimeOfDay)) && s.Days.Equals(day)).ToList();
                }
                if (conflict.Count > 0)
                {
                    string hint = "You have conflict(s) on the following:" + Environment.NewLine;
                    foreach (MySchedule sc in conflict)
                    {
                        hint += subjects.Where(s => s.ClassSubjectID.Equals(sc.ClassSubjectID)).Select(s => s.SubjectDescription).SingleOrDefault() + " " +
                            sc.FromTime.ToString("hh:mm tt") + "-" + sc.ToTime.ToString("hh:mm tt") + " at " + sc.Room + Environment.NewLine;
                    }
                    MessageBox.Show(hint, "Conflict in Temporary Schedule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //Local end
                //Global start
                string query = "SELECT * FROM ((tblClass c INNER JOIN tblClassSubjects s ON c.ClassID=s.ClassID) INNER JOIN tblClassSchedule sc ON s.ClassSubjectID=sc.ClassSubjectID) INNER JOIN tblSubjects su ON s.SubjectID=su.SubjectID WHERE Days = '"
                    + day + "' AND (Room = '" + room + "' AND NOT (Room = 'TBA' OR Room = 'QDR')) AND ((FromTime <= '" + f.ToString("HH:mm") + "' AND '" + f.ToString("HH:mm") + "' < ToTime) OR (FromTime < '" +
                    t.ToString("HH:mm:ss") + "' AND '" + t.ToString("HH:mm:ss") + "' <= ToTime) OR ('" + f.ToString("HH:mm") + "' <= FromTime AND FromTime < '" + 
                    t.ToString("HH:mm:ss") + "') OR ('" + f.ToString("HH:mm") + "' < ToTime AND ToTime <= '" + t.ToString("HH:mm:ss") + 
                    "')) AND Semester=" + this.semester + " AND SchoolYear='" + this.schoolYear + "'" +
                    (!ClassSubjectID.Contains("temp") ? " AND s.ClassSubjectID!=" + ClassSubjectID : ";");
                DataTable dt = db.SelectTable(query);
                string message = "You have conflict(s) on the following:" + Environment.NewLine;
                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        message += r["SubjectDescription"].ToString() + " " + Convert.ToDateTime(r["FromTime"].ToString()).ToString("hh:mm tt") +
                            "-" + Convert.ToDateTime(r["ToTime"].ToString()).ToString("hh:mm tt") + " at " + r["Room"].ToString() +
                            Environment.NewLine;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(message, "Failed Initial Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                subj.AddSchedule(day, f, t, room);
                lst.Items.Clear();
                lst.Items.AddRange(subj.GetAllScheduleItem().ToArray());
                //Global end
                
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            
            return true;
        }
        public void DeleteSubjectScheduleByID(string ClassSubjectID, string ClassScheduleID,ListView lst)
        {
            try
            {
                MySubject sj = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
                if (sj != null)
                {
                    sj.DeleteSpecificScheduleByID(ClassScheduleID);
                    lst.Items.Clear();
                    lst.Items.AddRange(sj.GetAllScheduleItem().ToArray());
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        public bool SaveSubjectScheduleIfPossible(string ClassSubjectID, string ProfID, int MaxClassCount)
        {//Include Class in checking. SY and Sem
            try
            {//must have checking if prof available
                MySubject sj = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
                List<MySchedule> scheds = sj.Schedules;
                if (scheds.Count > 0)
                {
                    if (sj.HoursAllowed != scheds.Sum(s => s.AllocatedHours))
                    {
                        MessageBox.Show("The total hours of schedules you set must be equal to the subject's allowed hours.", "Insufficient Time Allowance",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    foreach (MySchedule ms in scheds)
                    {
                        if (!CanSaveSchedule(ms, sj, ProfID))//Include Professor checking
                            return false;
                    }
                    sj.TempSaveScheduleOfSubject(ProfID,MaxClassCount);
                    sj.SaveDeleteIDs();
                    sj.IsSaved = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("You must set the schedule(s) of the selected subject before Saving.", "Nothing to Save",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            return false;
        }
        private bool CanSaveSchedule(MySchedule ms, MySubject sj, string ProfID)
        {
            //Checks Current Class structure to existing record. Exclude itself if the schedule is already existing
            string ClassSubjectID = sj.ClassSubjectID;
            string query = "SELECT * FROM ((tblClass c INNER JOIN tblClassSubjects s ON c.ClassID=s.ClassID) INNER JOIN tblClassSchedule sc ON s.ClassSubjectID=sc.ClassSubjectID) INNER JOIN tblSubjects su ON s.SubjectID=su.SubjectID WHERE Days = '"
                            + ms.Days + "' AND (Room = '" + ms.Room + "' AND NOT (Room = 'TBA' OR Room = 'QDR')) AND ((FromTime <= '" + ms.FromTime.ToString("HH:mm") + "' AND '" + ms.FromTime.ToString("HH:mm") + "' < ToTime) OR (FromTime < '" +
                    ms.ToTime.ToString("HH:mm:ss") + "' AND '" + ms.ToTime.ToString("HH:mm:ss") + "' <= ToTime) OR ('" + ms.FromTime.ToString("HH:mm") + "' <= FromTime AND FromTime < '" +
                    ms.ToTime.ToString("HH:mm:ss") + "') OR ('" + ms.FromTime.ToString("HH:mm") + "' < ToTime AND ToTime <= '" + ms.ToTime.ToString("HH:mm:ss") +
                    "')) AND Semester=" + this.semester + " AND SchoolYear='" + this.schoolYear + "'" +
                            (!ClassSubjectID.Contains("temp") ? " AND s.ClassSubjectID!=" + ClassSubjectID : ";");
            DataTable dt = db.SelectTable(query);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    string message = "You have conflict of schedule." + Environment.NewLine;
                    message += r["SubjectDescription"].ToString() + " " + Convert.ToDateTime(r["FromTime"].ToString()).ToString("hh:mm tt") +
                        "-" + Convert.ToDateTime(r["ToTime"].ToString()).ToString("hh:mm tt") + " at " + r["Room"].ToString();
                    MessageBox.Show(message, "Conflict in Schedule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            //end
            //Professor Availability Local and Global
            //local start
            foreach (MySubject mySubj in this.subjects.Where(s => !subjectDeleteIDs.Contains(s.ClassSubjectID) && !s.ClassSubjectID.Equals(sj.ClassSubjectID)).Select(s => s).ToList())
            {
                List<MySchedule> scheds = mySubj.Schedules;
                if (scheds.Count > 0 && mySubj.ProfessorID.Equals(ProfID))
                {
                    MySchedule conflictSched = scheds.Where(s => ((s.FromTime.TimeOfDay <= ms.FromTime.TimeOfDay && ms.FromTime.TimeOfDay < s.ToTime.TimeOfDay) ||
                        (s.FromTime.TimeOfDay < ms.ToTime.TimeOfDay && ms.ToTime.TimeOfDay <= s.ToTime.TimeOfDay) ||
                    (ms.FromTime.TimeOfDay <= s.FromTime.TimeOfDay && s.FromTime.TimeOfDay < ms.ToTime.TimeOfDay) ||
                    (ms.FromTime.TimeOfDay < s.ToTime.TimeOfDay && s.ToTime.TimeOfDay <= ms.ToTime.TimeOfDay)) && ms.Days.Equals(s.Days)).SingleOrDefault();
                    if (conflictSched != null)
                    {
                        MessageBox.Show("The professor you selected is not available in the schedule(s) you set. The professor is scheduled also in " +
                            conflictSched.Room + " at " + conflictSched.FromTime.ToString("hh:mm tt") + "-" + conflictSched.ToTime.ToString("hh:mm tt") + " teaching " +
                            mySubj.SubjectDescription + ".", "Professor Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            //local end
            query = "SELECT * FROM ((tblClass c INNER JOIN tblClassSubjects s ON c.ClassID=s.ClassID) INNER JOIN tblClassSchedule sc ON s.ClassSubjectID=sc.ClassSubjectID) INNER JOIN tblSubjects su ON s.SubjectID=su.SubjectID WHERE Days = '"
                            + ms.Days + "' AND ProfessorID =" + ProfID + " AND ((FromTime <= '" + ms.FromTime.ToString("HH:mm") + "' AND '" + ms.FromTime.ToString("HH:mm") + "' < ToTime) OR (FromTime < '" +
                    ms.ToTime.ToString("HH:mm:ss") + "' AND '" + ms.ToTime.ToString("HH:mm:ss") + "' <= ToTime) OR ('" + ms.FromTime.ToString("HH:mm") + "' <= FromTime AND FromTime < '" +
                    ms.ToTime.ToString("HH:mm:ss") + "') OR ('" + ms.FromTime.ToString("HH:mm") + "' < ToTime AND ToTime <= '" + ms.ToTime.ToString("HH:mm:ss") +
                    "')) AND Semester=" + this.semester + " AND SchoolYear='" + this.schoolYear + "'" +
                            (!ClassSubjectID.Contains("temp") ? " AND s.ClassSubjectID!=" + ClassSubjectID : ";");
            dt = db.SelectTable(query);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    string message = "The professor you selected is unavailable. Due to this conflict of his/her schedule. " + Environment.NewLine;
                    message += r["SubjectDescription"].ToString() + " " + Convert.ToDateTime(r["FromTime"].ToString()).ToString("hh:mm tt") +
                        "-" + Convert.ToDateTime(r["ToTime"].ToString()).ToString("hh:mm tt") + " at " + r["Room"].ToString();
                    MessageBox.Show(message, "Professor Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            //end
            return true;
        }
        /// <summary>
        /// unused
        /// </summary>
        public bool HasSchedule(string ClassSubjectID)
        {
            MySubject sj = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
            if (sj != null)
                return sj.IsSaved;
            else
                return false;
        }
        /// <summary>
        /// Checks if there is unsaved changes in the current subject selection
        /// </summary>
        public bool IsScheduleUnsaved(string ClassSubjectID)
        {
            if (ClassSubjectID.Equals("-1"))
                return false;
            try
            {
                MySubject subj = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
                if (subj != null)
                {
                    double totalHoursAllowed = subj.HoursAllowed;
                    List<MySchedule> allSched = subj.Schedules;
                    double schedTotalHours = 0;
                    if (allSched.Count > 0)
                    {
                        foreach (MySchedule ms in allSched)
                        {
                            schedTotalHours += ms.AllocatedHours;
                            if (!ms.IsSaved)
                                return true;
                        }
                    }
                    if (schedTotalHours != totalHoursAllowed && subj.IsSaved)
                        return true;
                }
            }
            catch { }
            return false;
        }
        /// <summary>
        /// Clears temporary unsaved schedule and clears temporary delete IDs
        /// </summary>
        public void ClearUnsavedSchedule(string ClassSubjectID)
        {
            MySubject ms = subjects.Where(s => s.ClassSubjectID.Equals(ClassSubjectID)).SingleOrDefault();
            if (ms != null)
            {
                ms.ClearScheduleIfNotSaved();
            }
        }
        public void DeleteSubject(string ClassSubjectID)
        {
            DataTable dt = db.SelectTable("SELECT * FROM tblStudentSched WHERE ClassSubjectID=" + ClassSubjectID + ";");
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                    subjectDeleteIDs.Add(ClassSubjectID);
                else
                    MessageBox.Show("The subject you want to remove has currently enrolled student. If you still want to delete the subject from this class, make sure that the subject must have no enrolled student.",
                        "Subject cannot be deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void ClearSubjects(ListView lst)
        {
            bool hasUsed = false;
            foreach (MySubject ms in subjects)
            {
                DataTable dt = db.SelectTable("SELECT * FROM tblStudentSched WHERE ClassSubjectID=" + ms.ClassSubjectID + ";");
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                        subjectDeleteIDs.Add(ms.ClassSubjectID);
                    else
                        hasUsed = true;
                }
                else
                    subjectDeleteIDs.Add(ms.ClassSubjectID);
            }
            ViewAllSubjectItems(lst);
            if (hasUsed)
                MessageBox.Show("The following Subject(s) that was not removed has currently enrolled student. If you still want to delete the subject from this class, make sure that the following subject must have no enrolled student.",
                    "Subject cannot be deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// Saves the temporary data gathered from the user.
        /// </summary>
        /// <param name="UserID">Currently Logged in User ID</param>
        public bool SaveClassIfPossible(int UserID)
        {//Check if All SubjectSaved
            //Check if all ready
            foreach (MySubject msj in this.SelectedSubjects)
            {
                double hrs = msj.Schedules.Sum(s => s.AllocatedHours);
                if (msj.HoursAllowed != hrs)
                {
                    MessageBox.Show("Some of the information you provide was not ready for saving. Please make sure that every subject has schedule(s).",
                    "Cannot Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            //end
            List<string> queries = new List<string>();
            string query = string.Empty;
            //START for ClearSelectedSubject
            if (subjects.Where(s => !subjectDeleteIDs.Contains(s.ClassSubjectID)).Select(s => s).ToList().Count == 0)
            {
                queries.AddRange(DeleteSubjectsOnSaving());
                if (!queries.Contains("HAS ERROR"))
                {
                    if (this.classID != null)
                        queries.Add("DELETE FROM tblClass WHERE ClassID=" + this.classID);

                }
                else
                    queries.RemoveAt(queries.Count - 1);
                db.InsertMultiple(queries);
                return true;
            }
            queries.AddRange(DeleteSubjectsOnSaving());
            if (queries.Contains("HAS ERROR"))
                queries.RemoveAt(queries.Count - 1);
            if (!db.InsertMultiple(queries))
            {
                MessageBox.Show("There is a problem encountered saving the class. Please contact the administrator.", "Error in Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string c = (this.courseID == null ? "CourseID IS NULL" : "CourseID=" + this.courseID);
            string m = (this.majorID == null ? "MajorID IS NULL" : this.majorID);
            string y = "YearLevel=" + this.yearLevel;
            string sec = "Section='" + this.section + "'";
            string sem = "Semester=" + (this.semester ? "1" : "0");
            string sy = "SchoolYear='" + this.schoolYear + "'";

            string criteria = c + " AND " + m + " AND " + y + " AND " + sec + " AND " + sem + " AND " + sy;
            queries.Clear();
            if (this.classID == null)
            {

                string CID = db.DataLookUp("ClassID", "tblClass", null, criteria);
                if (CID != null)
                {
                    MessageBox.Show("There is a saved same class recently. You are not allowed to save anymore without refreshing the class information you are modifying.", "Saving Lapses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string sql = "INSERT INTO tblClass(CourseID, MajorID, YearLevel, Section, Semester, SchoolYear, LogBy, LogDate) VALUES(?CourseID, ?MajorID, ?YearLevel, ?Section, ?Semester, ?SchoolYear, ?LogBy, ?LogDate);";
                Dictionary<string, object> dics = new Dictionary<string, object>();
                dics.Add("?CourseID", this.courseID);
                dics.Add("?MajorID", this.majorID);
                dics.Add("?YearLevel", this.yearLevel);
                dics.Add("?Section", this.section);
                dics.Add("?Semester", this.semester);
                dics.Add("?SchoolYear", this.schoolYear);
                dics.Add("?LogBy", UserID);
                dics.Add("?LogDate", db.GetServerDateTime());
                db.GenericCommand(sql, dics);
                CID = db.DataLookUp("ClassID", "tblClass", null, criteria);
                this.classID = CID;
            }
            else
            {
                string dateLog = db.DataLookUp("LogDate", "tblClass", null, criteria);
                if (dateLog != null)
                {
                    if (!dateLog.Equals(this.logDate))
                    {
                        MessageBox.Show("There is a modification done in the class you selected during your update. Please refresh the class information to prevent lapses and start again.", "Modified before Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            //END for ClearSelectedSubject
            //Check if There is no lapses and conflict for less glitch
            List<string> errorMessage = new List<string>();

            foreach (MySubject mySubj in subjects.Where(s => !subjectDeleteIDs.Contains(s.ClassSubjectID)).Select(s => s).ToList())
            {//ClassID, SubjectID, ProfessorID, MaxClassCount
                if (mySubj.MaxClassCount == -1 || mySubj.ProfessorID == null)
                {
                    MessageBox.Show("There is/are unsaved subjects. Please make sure to specify information to each of the subjects you selected.", "Unassigned Value");
                    return false;
                }
                bool hasProblem = false;
                foreach (MySchedule mySched in mySubj.Schedules)
                {
                    if (!CanSaveSchedule(mySched, mySubj,mySubj.ProfessorID))
                    {
                        errorMessage.Add("There is an error in saving the subject " + mySubj.SubjectDescription + ".");
                        hasProblem = true;
                    }
                }
                if (!hasProblem)
                {
                    bool canSaveSubj = false;
                    if (mySubj.ClassSubjectID.Contains("temp"))
                    {
                        string sql = "INSERT INTO tblClassSubjects(ClassID, SubjectID, ProfessorID, MaxClassCount) VALUES(?ClassID, ?SubjectID, ?ProfessorID, ?MaxClassCount);";
                        Dictionary<string, object> ds = new Dictionary<string, object>();
                        ds.Add("?ClassID", this.classID);
                        ds.Add("?SubjectID", mySubj.SubjectID);
                        ds.Add("?ProfessorID", mySubj.ProfessorID);
                        ds.Add("?MaxClassCount", mySubj.MaxClassCount);
                        canSaveSubj = db.GenericCommand(sql, ds);
                        DataRow r = db.GetLastInsertItem("tblClassSubjects", "ClassSubjectID");
                        if (r != null)
                        {
                            mySubj.ClassSubjectID = r["ClassSubjectID"].ToString();
                        }
                    }
                    else {
                        db.InsertQuery("UPDATE tblClassSubjects SET ProfessorID=" + mySubj.ProfessorID + ", MaxClassCount=" +
                            mySubj.MaxClassCount + " WHERE ClassSubjectID=" + mySubj.ClassSubjectID);
                        canSaveSubj = true;
                    }
                    if (canSaveSubj)
                    {
                        DeleteSubjectScheduleInPermanentList(mySubj.PermanentDeleteList);
                        foreach (MySchedule ms in mySubj.Schedules)
                        {//ClassSubjectID, Days, FromTime, ToTime, Room
                            if (ms.ClassScheduleID.Contains("temp"))
                            {
                                string sql = "INSERT INTO tblClassSchedule(ClassSubjectID, Days, FromTime, ToTime, Room) VALUES(?ClassSubjectID, ?Days, ?FromTime, ?ToTime, ?Room);";
                                Dictionary<string, object> ds = new Dictionary<string, object>();
                                ds.Add("?ClassSubjectID", mySubj.ClassSubjectID);
                                ds.Add("?Days", ms.Days);
                                ds.Add("?FromTime", ms.FromTime.ToString("HH:mm:ss"));
                                ds.Add("?ToTime", ms.ToTime.ToString("HH:mm:ss"));
                                ds.Add("?Room", ms.Room);
                                db.GenericCommand(sql, ds);
                                DataRow r = db.GetLastInsertItem("tblClassSchedule", "ClassScheduleID");
                                if (r != null)
                                {
                                    ms.ClassScheduleID = r["ClassScheduleID"].ToString();
                                }
                            }
                        }
                    }
                    //UpdateClassLog
                    db.InsertQuery("UPDATE tblClass SET LogBy=" + UserID + ", LogDate=NOW() WHERE ClassID=" + this.classID + ";");
                    string log = db.DataLookUp("LogDate", "tblClass", null, "ClassID=" + this.classID);
                    this.logDate = log;
                    //end
                }

            }
            //end
            
            return true;
        }
        /// <summary>
        /// Deletes
        /// </summary>
        /// <returns></returns>
        private List<string> DeleteSubjectsOnSaving()
        {
            List<string> queries = new List<string>();
            string query = string.Empty;
            bool hasUsed = false;
            foreach (string s in subjectDeleteIDs)
            {
                if (!s.Contains("temp"))
                {
                    query = "DELETE FROM tblClassSchedule WHERE ClassSubjectID=" + s + ";";
                    queries.Add(query);
                    DataTable dt = db.SelectTable("SELECT * FROM tblStudentSched WHERE ClassSubjectID=" + s + ";");
                    if (dt != null)
                    {
                        if (dt.Rows.Count == 0)
                        {
                            query = "DELETE FROM tblClassSubjects WHERE ClassSubjectID=" + s + ";";
                            queries.Add(query);
                        }
                        else
                            hasUsed = true;
                    }
                }
            }
            if (hasUsed)
            {
                MessageBox.Show("There are subject(s) that cannot be deleted since there are student(s) enrolled. Please make sure that there are no student(s) enrolled in the subject(s) you want to delete.",
                    "Error in Changes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                queries.Add("HAS ERROR");
            }
            return queries;
        }
        /// <summary>
        /// Deletes saved DeleteID
        /// </summary>
        private void DeleteSubjectScheduleInPermanentList(List<string> IDs)
        {
            foreach (string ID in IDs)
            {
                if (!ID.Contains("temp"))
                {
                    try
                    {
                        db.InsertQuery("DELETE FROM tblClassSchedule WHERE ClassScheduleID=" + ID + ";");
                    }
                    catch { }
                }
            }
        }
        public void PopulateSubjectAndSchedule()
        {
            string query = "SELECT *,(LectureHours + LaboratoryHours) AS 'HoursAllowed' FROM (tblClassSubjects s LEFT JOIN tblClassSchedule c ON s.ClassSubjectID=c.ClassSubjectID) LEFT JOIN tblSubjects sj ON s.SubjectID=sj.SubjectID WHERE ClassID=" + this.classID + " ORDER BY 1";
            DataTable dt = db.SelectTable(query);
            List<string> uniqueID = new List<string>();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (!uniqueID.Contains(r["ClassSubjectID"].ToString()))
                        {
                            uniqueID.Add(r["ClassSubjectID"].ToString());
                            AddSubject(r["ClassSubjectID"].ToString(), r["SubjectID"].ToString(), r["ProfessorID"].ToString(),
                                Convert.ToInt32(r["MaxClassCount"].ToString()), r["SubjectDescription"].ToString(), r["NoUnits"].ToString(), Convert.ToDouble(r["HoursAllowed"].ToString()));
                        }
                    }
                }
            }
            foreach(string ID in uniqueID)
            {
                List<DataRow> rs = dt.AsEnumerable().Where(s => s["ClassSubjectID"].ToString().Equals(ID)).OrderBy(s=>Convert.ToDateTime(s["FromTime"].ToString())).Select(s => s).ToList();
                MySubject sj = subjects.Where(s => s.ClassSubjectID.Equals(ID)).SingleOrDefault();
                if (sj != null)
                {
                    foreach (DataRow r in rs)
                    {
                        sj.AddSchedule(r["ClassScheduleID"].ToString(), r["Days"].ToString(), Convert.ToDateTime(r["FromTime"].ToString()), Convert.ToDateTime(r["ToTime"].ToString()), r["Room"].ToString());
                    }
                }
            }
        }
        public void ClearSubjects()
        {
            List<MySubject> subjs = subjects.Where(s => !subjectDeleteIDs.Contains(s.ClassSubjectID)).Select(s => s).ToList();
            foreach (MySubject ms in subjs)
            {
                if (!HasEnrolledStudent(ms.ClassSubjectID))
                    subjectDeleteIDs.Add(ms.ClassSubjectID);
            }
            subjs = subjects.Where(s => !subjectDeleteIDs.Contains(s.ClassSubjectID)).Select(s => s).ToList();
            if (subjs.Count > 0)
                MessageBox.Show("The remaining subject(s) has enrolled student(s). Please make sure to that the subject you want to delete doesn't have any enrolled student.",
                    "List Cannot be Cleared", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private bool HasEnrolledStudent(string ClassSubjectID)
        {
            if (ClassSubjectID.Contains("temp"))
                return false;
            string query = "SELECT * FROM tblStudentSched WHERE ClassSubjectID=" + ClassSubjectID + " LIMIT 1;";
            DataTable dt = db.SelectTable(query);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    return true;
            }
            return false;
        }
        public bool HasUnsaved()
        {
            try
            {
                List<MySubject> subjs = this.SelectedSubjects;
                foreach (MySubject msj in subjs)
                {
                    List<string> reserveTempDeleteID = msj.TempDeleteList.Where(s => !s.Contains("temp")).Select(s => s).ToList();
                    List<string> reserveScheduleDeleteID = msj.PermanentDeleteList.Where(s => !s.Contains("temp")).Select(s => s).ToList();
                    if (reserveTempDeleteID.Count > 0 || reserveScheduleDeleteID.Count > 0)
                        return true;
                    if (!msj.IsSaved || msj.ClassSubjectID.Contains("temp"))
                        return true;
                    foreach (MySchedule msc in msj.Schedules)
                    {
                        if (!msc.IsSaved || msc.ClassScheduleID.Contains("temp"))
                            return true;
                    }
                }

                List<string> reserveSubjectDeleteID = this.subjectDeleteIDs.Where(s => !s.Contains("temp")).Select(s => s).ToList();
                if (reserveSubjectDeleteID.Count > 0)
                    return true;
            }
            catch { }
            return false;
        }
    }
    class MySubject
    {
        private string classSubjectID;
        private string classID;
        private string subjectID;
        private string profID;
        private int maxClassCount;
        private List<MySchedule> schedules;
        private List<string> scheduleDeleteIDs;
        private List<string> permaDeleteIDs;
        private bool isSaved;

        // for printing
        string subjectDescription;
        string units;
        // end
        double hoursAllowed;//for checking of schedhours

        private int scheduleIDCounter;//Dont forget to reset when saved and update ID
        public MySubject(string ClassSubjectID,string ClassID,string SubjectID,string ProfessorID,int MaxClassCount,string SubjectDescription, string Units, double HoursAllowed) 
        {
            this.classSubjectID = ClassSubjectID;
            this.classID = ClassID;
            this.subjectID = SubjectID;
            this.profID = ProfessorID;
            this.maxClassCount = MaxClassCount;
            schedules = new List<MySchedule>();
            scheduleDeleteIDs = new List<string>();
            permaDeleteIDs = new List<string>();
            this.isSaved = false;
            this.subjectDescription = SubjectDescription;
            this.units = Units;
            this.hoursAllowed = HoursAllowed;
            scheduleIDCounter = 1;
        }

        //START of Setter/Getter or GetterOnly or SetterOnly
        public string ClassSubjectID
        {
            set { this.classSubjectID = value; }
            get { return this.classSubjectID; }
        }
        public string ClassID 
        {
            set { this.classID = value; }
        }
        public string SubjectID { get { return this.subjectID; } }
        public string ProfessorID { get { return this.profID; } }
        public int MaxClassCount { get { return this.maxClassCount; } }
        public bool IsSaved
        {
            set { this.isSaved = value; }
            get { return this.isSaved; }
        }
        public double HoursAllowed { get { return this.hoursAllowed; } }
        public List<MySchedule> Schedules {
            set { this.schedules = value; }
            get {
                try
                {
                    List<MySchedule> lms = schedules.Where(s => !scheduleDeleteIDs.Contains(s.ClassScheduleID)).ToList();
                    return lms;
                }
                catch { return new List<MySchedule>(); }
            }
        }
        public string SubjectDescription { get { return this.subjectDescription; } }
        public List<string> PermanentDeleteList { get { return this.permaDeleteIDs; } }
        public List<string> TempDeleteList { get { return this.scheduleDeleteIDs; } }
        //END of Setter/Getter or GetterOnly or SetterOnly

        /// <summary>
        /// Use for initial Schedule
        /// </summary>
        public void AddSchedule(string Days, DateTime FromTime, DateTime ToTime, string Room)
        {//check if available
            MySchedule schedule = new MySchedule("temp" + scheduleIDCounter, this.classSubjectID, Days, FromTime, ToTime, Room);
            this.schedules.Add(schedule);
            scheduleIDCounter++;
        }
        /// <summary>
        /// Use for Existing Schedule
        /// </summary>
        public void AddSchedule(string ClassScheduleID, string Days, DateTime FromTime, DateTime ToTime, string Room) 
        {
            MySchedule schedule = new MySchedule(ClassScheduleID, classSubjectID, Days, FromTime, ToTime, Room);
            schedule.IsSaved = true;
            this.schedules.Add(schedule);
        }
        /// <summary>
        /// Save Subject Schedule information from list and other info.
        /// </summary>
        public void TempSaveScheduleOfSubject(string ProfessorID, int MaxClassCount)
        {
            this.profID = ProfessorID;
            this.maxClassCount = MaxClassCount;
            isSaved = true;
            for (int i = 0; i < schedules.Count; i++)
                schedules[i].IsSaved = true;
            schedules.RemoveAll(s => scheduleDeleteIDs.Contains(s.ClassScheduleID));
        }
        /// <summary>
        /// Use if the user checks other subject without saving
        /// </summary>
        public void ClearScheduleIfNotSaved()
        {
            try
            {
                if (!isSaved)
                {
                    
                }
                schedules.RemoveAll(s => s.ClassScheduleID.Contains("temp") && !s.IsSaved);
                scheduleDeleteIDs.Clear();
            }
            catch (Exception e) { MessageBox.Show(e.Message, "LINQ Error"); }
        }
        /// <summary>
        /// Delete schedule specified by ID
        /// </summary>
        public void DeleteSpecificScheduleByID(string ClassScheduleID)
        {
            scheduleDeleteIDs.Add(ClassScheduleID);
        }
        /// <summary>
        /// Gets all active schedules
        /// </summary>
        /// <returns>List<ListViewItem> of schedule of specific subjectID</returns>
        public List<ListViewItem> GetAllScheduleItem()
        {
            List<ListViewItem> items = new List<ListViewItem>();
            try
            {
                List<MySchedule> lms = schedules.Where(s => !scheduleDeleteIDs.Contains(s.ClassScheduleID)).Select(s => s).ToList();
                foreach (MySchedule ms in lms)
                {
                    items.Add(ms.GetItem());
                }
                return items;
            }
            catch { return null; }
        }
        public ListViewItem GetItem()
        {
            ListViewItem itm = new ListViewItem(this.classSubjectID);
            itm.SubItems.Add(this.subjectID);
            itm.SubItems.Add(this.subjectDescription);
            itm.SubItems.Add(this.units);
            itm.SubItems.Add(this.hoursAllowed.ToString());
            return itm;
        }
        public void SaveDeleteIDs()
        {
            permaDeleteIDs.AddRange(scheduleDeleteIDs);
            scheduleDeleteIDs.Clear();
        }
    }
    class MySchedule
    {
        private string classScheduleID;
        private string classSubjectID;
        private string days;
        private DateTime fromTime;
        private DateTime toTime;
        private string room;
        private bool isSaved;
        private double allocatedHours;
        public MySchedule(string ClassScheduleID, string ClassSubjectID, string Days, DateTime FromTime, DateTime ToTime, string Room)
        {
            this.classScheduleID = ClassScheduleID;
            this.classSubjectID = ClassSubjectID;
            this.days = Days;
            this.fromTime = FromTime;
            this.toTime = ToTime;
            this.room = Room;
            this.isSaved = false;
            allocatedHours = ToTime.Subtract(FromTime).TotalHours;
        }
        //START of Setter/Getter or GetterOnly or SetterOnly
        public string ClassScheduleID
        {
            set { this.classScheduleID = value; }
            get { return this.classScheduleID; }
        }
        public string ClassSubjectID
        {
            set { this.classSubjectID = value; }
            get { return this.classSubjectID; }
        }
        public string Days { get { return this.days; } }
        public DateTime FromTime { get { return this.fromTime; } }
        public DateTime ToTime { get { return this.toTime; } }
        public string Room { get { return this.room; } }
        public bool IsSaved
        {
            set { this.isSaved = value; }
            get { return this.isSaved; }
        }
        public double AllocatedHours { get { return this.allocatedHours; } }
        //END of Setter/Getter or GetterOnly or SetterOnly

        /// <summary>
        /// Create ListViewItem based on the fixed constructed ListView
        /// </summary>
        /// <returns>ListViewItem Cols(id,days,fromtime,totime,room)</returns>
        public ListViewItem GetItem()
        {
            ListViewItem itm = new ListViewItem(this.classScheduleID);
            itm.SubItems.Add(GetDayEquivalent(this.days));
            itm.SubItems.Add(this.fromTime.ToString("hh:mm tt"));
            itm.SubItems.Add(this.toTime.ToString("hh:mm tt"));
            itm.SubItems.Add(this.room);
            return itm;
        }
        private string GetDayEquivalent(string d)
        {
            switch (d)
            {
                case "1": return "Mon";
                case "2": return "Tue";
                case "3": return "Wed";
                case "4": return "Thu";
                case "5": return "Fri";
                case "6": return "Sat";
                case "7": return "Sun";
                default: return null;
            }
        }
    }
}
