namespace Academic_Solution_NPC
{
    partial class frmClassScheduler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuClassScheduler = new System.Windows.Forms.MenuStrip();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRooms = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCourse = new System.Windows.Forms.ComboBox();
            this.cmbMajor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.gpbSemester = new System.Windows.Forms.GroupBox();
            this.opt2nd = new System.Windows.Forms.RadioButton();
            this.opt1st = new System.Windows.Forms.RadioButton();
            this.btnDisplayRecords = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnAddToSelected = new System.Windows.Forms.Button();
            this.lstSubjects = new System.Windows.Forms.ListView();
            this.clSubjectCode2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSubjectDescription2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clPrerequisite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clNoOfUnits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpbSelectedSubjects = new System.Windows.Forms.GroupBox();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.btnUpdateSchedule = new System.Windows.Forms.Button();
            this.txtMaxClassCount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbProfessors = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gpbClassDaysNTime = new System.Windows.Forms.GroupBox();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstSched = new System.Windows.Forms.ListView();
            this.clID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRoom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.optSun = new System.Windows.Forms.RadioButton();
            this.optSat = new System.Windows.Forms.RadioButton();
            this.optFri = new System.Windows.Forms.RadioButton();
            this.optThu = new System.Windows.Forms.RadioButton();
            this.optWed = new System.Windows.Forms.RadioButton();
            this.OptTue = new System.Windows.Forms.RadioButton();
            this.optMon = new System.Windows.Forms.RadioButton();
            this.lstSelectedSubjects = new System.Windows.Forms.ListView();
            this.clSubjSchedID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSubjectCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSubjectDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clUnits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clHoursAllowed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpbClassType = new System.Windows.Forms.GroupBox();
            this.optIrregular = new System.Windows.Forms.RadioButton();
            this.optRegular = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.menuClassScheduler.SuspendLayout();
            this.gpbSemester.SuspendLayout();
            this.gpbSelectedSubjects.SuspendLayout();
            this.gpbClassDaysNTime.SuspendLayout();
            this.gpbClassType.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuClassScheduler
            // 
            this.menuClassScheduler.AllowMerge = false;
            this.menuClassScheduler.AutoSize = false;
            this.menuClassScheduler.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuClassScheduler.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuClassScheduler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSave,
            this.menuPrint,
            this.menuRooms,
            this.menuExit});
            this.menuClassScheduler.Location = new System.Drawing.Point(0, 0);
            this.menuClassScheduler.Name = "menuClassScheduler";
            this.menuClassScheduler.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuClassScheduler.Size = new System.Drawing.Size(1244, 55);
            this.menuClassScheduler.TabIndex = 0;
            this.menuClassScheduler.Text = "menuStrip1";
            // 
            // menuSave
            // 
            this.menuSave.Image = global::Academic_Solution_NPC.Properties.Resources.save;
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(84, 49);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuPrint
            // 
            this.menuPrint.Image = global::Academic_Solution_NPC.Properties.Resources.print;
            this.menuPrint.Name = "menuPrint";
            this.menuPrint.Size = new System.Drawing.Size(84, 49);
            this.menuPrint.Text = "Print";
            this.menuPrint.Click += new System.EventHandler(this.menuPrint_Click);
            // 
            // menuRooms
            // 
            this.menuRooms.Image = global::Academic_Solution_NPC.Properties.Resources.room;
            this.menuRooms.Name = "menuRooms";
            this.menuRooms.Size = new System.Drawing.Size(105, 49);
            this.menuRooms.Text = "Rooms";
            this.menuRooms.Click += new System.EventHandler(this.menuRooms_Click);
            // 
            // menuExit
            // 
            this.menuExit.Image = global::Academic_Solution_NPC.Properties.Resources.Exit;
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(74, 49);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Course : ";
            // 
            // cmbCourse
            // 
            this.cmbCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCourse.FormattingEnabled = true;
            this.cmbCourse.Location = new System.Drawing.Point(99, 114);
            this.cmbCourse.Name = "cmbCourse";
            this.cmbCourse.Size = new System.Drawing.Size(191, 25);
            this.cmbCourse.TabIndex = 2;
            this.cmbCourse.SelectedIndexChanged += new System.EventHandler(this.cmbCourse_SelectedIndexChanged);
            // 
            // cmbMajor
            // 
            this.cmbMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMajor.FormattingEnabled = true;
            this.cmbMajor.Location = new System.Drawing.Point(99, 145);
            this.cmbMajor.Name = "cmbMajor";
            this.cmbMajor.Size = new System.Drawing.Size(191, 25);
            this.cmbMajor.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Major :";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(99, 176);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(90, 25);
            this.cmbYear.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Year && Sec :";
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.cmbSection.Location = new System.Drawing.Point(200, 176);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(90, 25);
            this.cmbSection.TabIndex = 7;
            // 
            // gpbSemester
            // 
            this.gpbSemester.Controls.Add(this.opt2nd);
            this.gpbSemester.Controls.Add(this.opt1st);
            this.gpbSemester.Location = new System.Drawing.Point(15, 207);
            this.gpbSemester.Name = "gpbSemester";
            this.gpbSemester.Size = new System.Drawing.Size(275, 59);
            this.gpbSemester.TabIndex = 8;
            this.gpbSemester.TabStop = false;
            this.gpbSemester.Text = "Semester";
            // 
            // opt2nd
            // 
            this.opt2nd.AutoSize = true;
            this.opt2nd.Location = new System.Drawing.Point(122, 24);
            this.opt2nd.Name = "opt2nd";
            this.opt2nd.Size = new System.Drawing.Size(106, 21);
            this.opt2nd.TabIndex = 2;
            this.opt2nd.Text = "2nd Semester";
            this.opt2nd.UseVisualStyleBackColor = true;
            // 
            // opt1st
            // 
            this.opt1st.AutoSize = true;
            this.opt1st.Checked = true;
            this.opt1st.Location = new System.Drawing.Point(6, 24);
            this.opt1st.Name = "opt1st";
            this.opt1st.Size = new System.Drawing.Size(101, 21);
            this.opt1st.TabIndex = 1;
            this.opt1st.TabStop = true;
            this.opt1st.Text = "1st Semester";
            this.opt1st.UseVisualStyleBackColor = true;
            // 
            // btnDisplayRecords
            // 
            this.btnDisplayRecords.Location = new System.Drawing.Point(12, 272);
            this.btnDisplayRecords.Name = "btnDisplayRecords";
            this.btnDisplayRecords.Size = new System.Drawing.Size(278, 26);
            this.btnDisplayRecords.TabIndex = 9;
            this.btnDisplayRecords.Text = "Display Records";
            this.btnDisplayRecords.UseVisualStyleBackColor = true;
            this.btnDisplayRecords.Click += new System.EventHandler(this.btnDisplayRecords_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(12, 304);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(278, 26);
            this.btnClearList.TabIndex = 10;
            this.btnClearList.Text = "Clear List";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // btnAddToSelected
            // 
            this.btnAddToSelected.Enabled = false;
            this.btnAddToSelected.Location = new System.Drawing.Point(12, 336);
            this.btnAddToSelected.Name = "btnAddToSelected";
            this.btnAddToSelected.Size = new System.Drawing.Size(278, 26);
            this.btnAddToSelected.TabIndex = 11;
            this.btnAddToSelected.Text = "Add to Selected";
            this.btnAddToSelected.UseVisualStyleBackColor = true;
            this.btnAddToSelected.Click += new System.EventHandler(this.btnAddToSelected_Click);
            // 
            // lstSubjects
            // 
            this.lstSubjects.CheckBoxes = true;
            this.lstSubjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clSubjectCode2,
            this.clSubjectDescription2,
            this.clPrerequisite,
            this.clNoOfUnits});
            this.lstSubjects.FullRowSelect = true;
            this.lstSubjects.GridLines = true;
            this.lstSubjects.Location = new System.Drawing.Point(296, 82);
            this.lstSubjects.Name = "lstSubjects";
            this.lstSubjects.Size = new System.Drawing.Size(936, 280);
            this.lstSubjects.TabIndex = 12;
            this.lstSubjects.UseCompatibleStateImageBehavior = false;
            this.lstSubjects.View = System.Windows.Forms.View.Details;
            this.lstSubjects.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstSubjects_ColumnClick);
            // 
            // clSubjectCode2
            // 
            this.clSubjectCode2.Text = "Subject Code";
            this.clSubjectCode2.Width = 100;
            // 
            // clSubjectDescription2
            // 
            this.clSubjectDescription2.Text = "Subject Description";
            this.clSubjectDescription2.Width = 550;
            // 
            // clPrerequisite
            // 
            this.clPrerequisite.Text = "Prerequisite";
            this.clPrerequisite.Width = 90;
            // 
            // clNoOfUnits
            // 
            this.clNoOfUnits.Text = "No. of Units";
            this.clNoOfUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clNoOfUnits.Width = 90;
            // 
            // gpbSelectedSubjects
            // 
            this.gpbSelectedSubjects.Controls.Add(this.btnDeleteSchedule);
            this.gpbSelectedSubjects.Controls.Add(this.btnUpdateSchedule);
            this.gpbSelectedSubjects.Controls.Add(this.txtMaxClassCount);
            this.gpbSelectedSubjects.Controls.Add(this.label9);
            this.gpbSelectedSubjects.Controls.Add(this.cmbProfessors);
            this.gpbSelectedSubjects.Controls.Add(this.label6);
            this.gpbSelectedSubjects.Controls.Add(this.gpbClassDaysNTime);
            this.gpbSelectedSubjects.Controls.Add(this.lstSelectedSubjects);
            this.gpbSelectedSubjects.Enabled = false;
            this.gpbSelectedSubjects.Location = new System.Drawing.Point(12, 368);
            this.gpbSelectedSubjects.Name = "gpbSelectedSubjects";
            this.gpbSelectedSubjects.Size = new System.Drawing.Size(1220, 318);
            this.gpbSelectedSubjects.TabIndex = 13;
            this.gpbSelectedSubjects.TabStop = false;
            this.gpbSelectedSubjects.Text = "Selected Subjects";
            // 
            // btnDeleteSchedule
            // 
            this.btnDeleteSchedule.Location = new System.Drawing.Point(999, 283);
            this.btnDeleteSchedule.Name = "btnDeleteSchedule";
            this.btnDeleteSchedule.Size = new System.Drawing.Size(215, 26);
            this.btnDeleteSchedule.TabIndex = 25;
            this.btnDeleteSchedule.Text = "Delete Schedule";
            this.btnDeleteSchedule.UseVisualStyleBackColor = true;
            this.btnDeleteSchedule.Click += new System.EventHandler(this.btnDeleteSchedule_Click);
            // 
            // btnUpdateSchedule
            // 
            this.btnUpdateSchedule.Location = new System.Drawing.Point(999, 252);
            this.btnUpdateSchedule.Name = "btnUpdateSchedule";
            this.btnUpdateSchedule.Size = new System.Drawing.Size(215, 26);
            this.btnUpdateSchedule.TabIndex = 24;
            this.btnUpdateSchedule.Text = "Update Schedule";
            this.btnUpdateSchedule.UseVisualStyleBackColor = true;
            this.btnUpdateSchedule.Click += new System.EventHandler(this.btnUpdateSchedule_Click);
            // 
            // txtMaxClassCount
            // 
            this.txtMaxClassCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxClassCount.Location = new System.Drawing.Point(893, 253);
            this.txtMaxClassCount.Name = "txtMaxClassCount";
            this.txtMaxClassCount.Size = new System.Drawing.Size(100, 25);
            this.txtMaxClassCount.TabIndex = 23;
            this.txtMaxClassCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxClassCount_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(775, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Max Class Count :";
            // 
            // cmbProfessors
            // 
            this.cmbProfessors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfessors.FormattingEnabled = true;
            this.cmbProfessors.Location = new System.Drawing.Point(708, 223);
            this.cmbProfessors.Name = "cmbProfessors";
            this.cmbProfessors.Size = new System.Drawing.Size(506, 25);
            this.cmbProfessors.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(631, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Professor :";
            // 
            // gpbClassDaysNTime
            // 
            this.gpbClassDaysNTime.Controls.Add(this.txtRoom);
            this.gpbClassDaysNTime.Controls.Add(this.label7);
            this.gpbClassDaysNTime.Controls.Add(this.lstSched);
            this.gpbClassDaysNTime.Controls.Add(this.btnRemoveSelected);
            this.gpbClassDaysNTime.Controls.Add(this.btnAdd);
            this.gpbClassDaysNTime.Controls.Add(this.dtpTo);
            this.gpbClassDaysNTime.Controls.Add(this.label5);
            this.gpbClassDaysNTime.Controls.Add(this.dtpFrom);
            this.gpbClassDaysNTime.Controls.Add(this.label4);
            this.gpbClassDaysNTime.Controls.Add(this.optSun);
            this.gpbClassDaysNTime.Controls.Add(this.optSat);
            this.gpbClassDaysNTime.Controls.Add(this.optFri);
            this.gpbClassDaysNTime.Controls.Add(this.optThu);
            this.gpbClassDaysNTime.Controls.Add(this.optWed);
            this.gpbClassDaysNTime.Controls.Add(this.OptTue);
            this.gpbClassDaysNTime.Controls.Add(this.optMon);
            this.gpbClassDaysNTime.Location = new System.Drawing.Point(629, 24);
            this.gpbClassDaysNTime.Name = "gpbClassDaysNTime";
            this.gpbClassDaysNTime.Size = new System.Drawing.Size(584, 193);
            this.gpbClassDaysNTime.TabIndex = 15;
            this.gpbClassDaysNTime.TabStop = false;
            this.gpbClassDaysNTime.Text = "Class Days && Time";
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(492, 21);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(75, 25);
            this.txtRoom.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(436, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Room :";
            // 
            // lstSched
            // 
            this.lstSched.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clID,
            this.clDay,
            this.clFrom,
            this.clTo,
            this.clRoom});
            this.lstSched.FullRowSelect = true;
            this.lstSched.GridLines = true;
            this.lstSched.Location = new System.Drawing.Point(19, 86);
            this.lstSched.Name = "lstSched";
            this.lstSched.Size = new System.Drawing.Size(548, 97);
            this.lstSched.TabIndex = 13;
            this.lstSched.UseCompatibleStateImageBehavior = false;
            this.lstSched.View = System.Windows.Forms.View.Details;
            // 
            // clID
            // 
            this.clID.Text = "ID";
            this.clID.Width = 0;
            // 
            // clDay
            // 
            this.clDay.Text = "Day";
            // 
            // clFrom
            // 
            this.clFrom.Text = "From";
            this.clFrom.Width = 150;
            // 
            // clTo
            // 
            this.clTo.Text = "To";
            this.clTo.Width = 150;
            // 
            // clRoom
            // 
            this.clRoom.Text = "Room";
            this.clRoom.Width = 150;
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(422, 52);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(145, 26);
            this.btnRemoveSelected.TabIndex = 12;
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(288, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 26);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "hh:mm tt";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(185, 55);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 25);
            this.dtpTo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "To";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "hh:mm tt";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(60, 55);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 25);
            this.dtpFrom.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "From";
            // 
            // optSun
            // 
            this.optSun.AutoSize = true;
            this.optSun.Location = new System.Drawing.Point(369, 24);
            this.optSun.Name = "optSun";
            this.optSun.Size = new System.Drawing.Size(47, 21);
            this.optSun.TabIndex = 6;
            this.optSun.Text = "Sun";
            this.optSun.UseVisualStyleBackColor = true;
            // 
            // optSat
            // 
            this.optSat.AutoSize = true;
            this.optSat.Location = new System.Drawing.Point(310, 24);
            this.optSat.Name = "optSat";
            this.optSat.Size = new System.Drawing.Size(44, 21);
            this.optSat.TabIndex = 5;
            this.optSat.Text = "Sat";
            this.optSat.UseVisualStyleBackColor = true;
            // 
            // optFri
            // 
            this.optFri.AutoSize = true;
            this.optFri.Location = new System.Drawing.Point(255, 24);
            this.optFri.Name = "optFri";
            this.optFri.Size = new System.Drawing.Size(40, 21);
            this.optFri.TabIndex = 4;
            this.optFri.Text = "Fri";
            this.optFri.UseVisualStyleBackColor = true;
            // 
            // optThu
            // 
            this.optThu.AutoSize = true;
            this.optThu.Location = new System.Drawing.Point(196, 24);
            this.optThu.Name = "optThu";
            this.optThu.Size = new System.Drawing.Size(47, 21);
            this.optThu.TabIndex = 3;
            this.optThu.Text = "Thu";
            this.optThu.UseVisualStyleBackColor = true;
            // 
            // optWed
            // 
            this.optWed.AutoSize = true;
            this.optWed.Location = new System.Drawing.Point(137, 24);
            this.optWed.Name = "optWed";
            this.optWed.Size = new System.Drawing.Size(53, 21);
            this.optWed.TabIndex = 2;
            this.optWed.Text = "Wed";
            this.optWed.UseVisualStyleBackColor = true;
            // 
            // OptTue
            // 
            this.OptTue.AutoSize = true;
            this.OptTue.Location = new System.Drawing.Point(78, 24);
            this.OptTue.Name = "OptTue";
            this.OptTue.Size = new System.Drawing.Size(47, 21);
            this.OptTue.TabIndex = 1;
            this.OptTue.Text = "Tue";
            this.OptTue.UseVisualStyleBackColor = true;
            // 
            // optMon
            // 
            this.optMon.AutoSize = true;
            this.optMon.Checked = true;
            this.optMon.Location = new System.Drawing.Point(19, 24);
            this.optMon.Name = "optMon";
            this.optMon.Size = new System.Drawing.Size(53, 21);
            this.optMon.TabIndex = 0;
            this.optMon.TabStop = true;
            this.optMon.Text = "Mon";
            this.optMon.UseVisualStyleBackColor = true;
            // 
            // lstSelectedSubjects
            // 
            this.lstSelectedSubjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clSubjSchedID,
            this.clSubjectCode,
            this.clSubjectDescription,
            this.clUnits,
            this.clHoursAllowed});
            this.lstSelectedSubjects.FullRowSelect = true;
            this.lstSelectedSubjects.GridLines = true;
            this.lstSelectedSubjects.Location = new System.Drawing.Point(8, 24);
            this.lstSelectedSubjects.Name = "lstSelectedSubjects";
            this.lstSelectedSubjects.Size = new System.Drawing.Size(615, 281);
            this.lstSelectedSubjects.TabIndex = 0;
            this.lstSelectedSubjects.UseCompatibleStateImageBehavior = false;
            this.lstSelectedSubjects.View = System.Windows.Forms.View.Details;
            this.lstSelectedSubjects.SelectedIndexChanged += new System.EventHandler(this.lstSelectedSubjects_SelectedIndexChanged);
            // 
            // clSubjSchedID
            // 
            this.clSubjSchedID.Text = "ID";
            this.clSubjSchedID.Width = 0;
            // 
            // clSubjectCode
            // 
            this.clSubjectCode.Text = "Subject Code";
            this.clSubjectCode.Width = 100;
            // 
            // clSubjectDescription
            // 
            this.clSubjectDescription.Text = "Subject Description";
            this.clSubjectDescription.Width = 340;
            // 
            // clUnits
            // 
            this.clUnits.Text = "Units";
            this.clUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clUnits.Width = 50;
            // 
            // clHoursAllowed
            // 
            this.clHoursAllowed.Text = "Hours Allowed";
            this.clHoursAllowed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clHoursAllowed.Width = 100;
            // 
            // gpbClassType
            // 
            this.gpbClassType.Controls.Add(this.optIrregular);
            this.gpbClassType.Controls.Add(this.optRegular);
            this.gpbClassType.Location = new System.Drawing.Point(12, 58);
            this.gpbClassType.Name = "gpbClassType";
            this.gpbClassType.Size = new System.Drawing.Size(278, 50);
            this.gpbClassType.TabIndex = 1;
            this.gpbClassType.TabStop = false;
            this.gpbClassType.Text = "Class Type";
            // 
            // optIrregular
            // 
            this.optIrregular.AutoSize = true;
            this.optIrregular.Location = new System.Drawing.Point(125, 24);
            this.optIrregular.Name = "optIrregular";
            this.optIrregular.Size = new System.Drawing.Size(76, 21);
            this.optIrregular.TabIndex = 17;
            this.optIrregular.Text = "Irregular";
            this.optIrregular.UseVisualStyleBackColor = true;
            // 
            // optRegular
            // 
            this.optRegular.AutoSize = true;
            this.optRegular.Checked = true;
            this.optRegular.Location = new System.Drawing.Point(9, 24);
            this.optRegular.Name = "optRegular";
            this.optRegular.Size = new System.Drawing.Size(71, 21);
            this.optRegular.TabIndex = 16;
            this.optRegular.TabStop = true;
            this.optRegular.Text = "Regular";
            this.optRegular.UseVisualStyleBackColor = true;
            this.optRegular.CheckedChanged += new System.EventHandler(this.optRegular_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Search Subject :";
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Location = new System.Drawing.Point(402, 55);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(259, 25);
            this.txtSearchKey.TabIndex = 15;
            this.txtSearchKey.Click += new System.EventHandler(this.txtSearchKey_Click);
            this.txtSearchKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchKey_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(667, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(745, 55);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(139, 25);
            this.btnClearSearch.TabIndex = 17;
            this.btnClearSearch.Text = "Clear Search";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // frmClassScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 695);
            this.ControlBox = false;
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchKey);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.gpbClassType);
            this.Controls.Add(this.gpbSelectedSubjects);
            this.Controls.Add(this.lstSubjects);
            this.Controls.Add(this.btnAddToSelected);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnDisplayRecords);
            this.Controls.Add(this.gpbSemester);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMajor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCourse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuClassScheduler);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuClassScheduler;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmClassScheduler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Class Scheduler";
            this.Load += new System.EventHandler(this.frmClassScheduler_Load);
            this.menuClassScheduler.ResumeLayout(false);
            this.menuClassScheduler.PerformLayout();
            this.gpbSemester.ResumeLayout(false);
            this.gpbSemester.PerformLayout();
            this.gpbSelectedSubjects.ResumeLayout(false);
            this.gpbSelectedSubjects.PerformLayout();
            this.gpbClassDaysNTime.ResumeLayout(false);
            this.gpbClassDaysNTime.PerformLayout();
            this.gpbClassType.ResumeLayout(false);
            this.gpbClassType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuClassScheduler;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuPrint;
        private System.Windows.Forms.ToolStripMenuItem menuRooms;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCourse;
        private System.Windows.Forms.ComboBox cmbMajor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.GroupBox gpbSemester;
        private System.Windows.Forms.RadioButton opt2nd;
        private System.Windows.Forms.RadioButton opt1st;
        private System.Windows.Forms.Button btnDisplayRecords;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button btnAddToSelected;
        private System.Windows.Forms.ListView lstSubjects;
        private System.Windows.Forms.GroupBox gpbSelectedSubjects;
        private System.Windows.Forms.ColumnHeader clSubjectCode2;
        private System.Windows.Forms.ColumnHeader clSubjectDescription2;
        private System.Windows.Forms.ColumnHeader clPrerequisite;
        private System.Windows.Forms.ColumnHeader clNoOfUnits;
        private System.Windows.Forms.ListView lstSelectedSubjects;
        private System.Windows.Forms.ColumnHeader clSubjSchedID;
        private System.Windows.Forms.ColumnHeader clSubjectCode;
        private System.Windows.Forms.ColumnHeader clSubjectDescription;
        private System.Windows.Forms.GroupBox gpbClassDaysNTime;
        private System.Windows.Forms.Button btnDeleteSchedule;
        private System.Windows.Forms.Button btnUpdateSchedule;
        private System.Windows.Forms.TextBox txtMaxClassCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbProfessors;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader clUnits;
        private System.Windows.Forms.RadioButton optSun;
        private System.Windows.Forms.RadioButton optSat;
        private System.Windows.Forms.RadioButton optFri;
        private System.Windows.Forms.RadioButton optThu;
        private System.Windows.Forms.RadioButton optWed;
        private System.Windows.Forms.RadioButton OptTue;
        private System.Windows.Forms.RadioButton optMon;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lstSched;
        private System.Windows.Forms.ColumnHeader clDay;
        private System.Windows.Forms.ColumnHeader clTo;
        private System.Windows.Forms.ColumnHeader clRoom;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader clFrom;
        private System.Windows.Forms.GroupBox gpbClassType;
        private System.Windows.Forms.RadioButton optIrregular;
        private System.Windows.Forms.RadioButton optRegular;
        private System.Windows.Forms.ColumnHeader clID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSearchKey;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.ColumnHeader clHoursAllowed;
    }
}