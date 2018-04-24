namespace Academic_Solution_NPC
{
    partial class frmRoomView
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
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lstSchedules = new System.Windows.Forms.ListView();
            this.clSubjectID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSubjectDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDays = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clProfessor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCYS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.opt1st = new System.Windows.Forms.RadioButton();
            this.opt2nd = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(12, 12);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(174, 25);
            this.txtRoom.TabIndex = 0;
            this.txtRoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoom_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 43);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(174, 27);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search Room";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 457);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(174, 27);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lstSchedules
            // 
            this.lstSchedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clSubjectID,
            this.clSubjectDescription,
            this.clDays,
            this.clTime,
            this.clProfessor,
            this.clCYS});
            this.lstSchedules.FullRowSelect = true;
            this.lstSchedules.GridLines = true;
            this.lstSchedules.Location = new System.Drawing.Point(192, 12);
            this.lstSchedules.Name = "lstSchedules";
            this.lstSchedules.Size = new System.Drawing.Size(1050, 472);
            this.lstSchedules.TabIndex = 4;
            this.lstSchedules.UseCompatibleStateImageBehavior = false;
            this.lstSchedules.View = System.Windows.Forms.View.Details;
            // 
            // clSubjectID
            // 
            this.clSubjectID.Text = "Subject ID";
            this.clSubjectID.Width = 80;
            // 
            // clSubjectDescription
            // 
            this.clSubjectDescription.Text = "Description";
            this.clSubjectDescription.Width = 300;
            // 
            // clDays
            // 
            this.clDays.Text = "Days";
            // 
            // clTime
            // 
            this.clTime.Text = "Time";
            this.clTime.Width = 180;
            // 
            // clProfessor
            // 
            this.clProfessor.Text = "Professor";
            this.clProfessor.Width = 300;
            // 
            // clCYS
            // 
            this.clCYS.Text = "CYS";
            this.clCYS.Width = 100;
            // 
            // opt1st
            // 
            this.opt1st.AutoSize = true;
            this.opt1st.Checked = true;
            this.opt1st.Location = new System.Drawing.Point(12, 91);
            this.opt1st.Name = "opt1st";
            this.opt1st.Size = new System.Drawing.Size(101, 21);
            this.opt1st.TabIndex = 2;
            this.opt1st.TabStop = true;
            this.opt1st.Text = "1st Semester";
            this.opt1st.UseVisualStyleBackColor = true;
            // 
            // opt2nd
            // 
            this.opt2nd.AutoSize = true;
            this.opt2nd.Location = new System.Drawing.Point(12, 118);
            this.opt2nd.Name = "opt2nd";
            this.opt2nd.Size = new System.Drawing.Size(106, 21);
            this.opt2nd.TabIndex = 3;
            this.opt2nd.Text = "2nd Semester";
            this.opt2nd.UseVisualStyleBackColor = true;
            // 
            // frmRoomView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 496);
            this.Controls.Add(this.opt2nd);
            this.Controls.Add(this.opt1st);
            this.Controls.Add(this.lstSchedules);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtRoom);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRoomView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Room";
            this.Load += new System.EventHandler(this.frmRoomView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListView lstSchedules;
        private System.Windows.Forms.ColumnHeader clSubjectID;
        private System.Windows.Forms.ColumnHeader clSubjectDescription;
        private System.Windows.Forms.ColumnHeader clDays;
        private System.Windows.Forms.ColumnHeader clTime;
        private System.Windows.Forms.ColumnHeader clProfessor;
        private System.Windows.Forms.ColumnHeader clCYS;
        private System.Windows.Forms.RadioButton opt1st;
        private System.Windows.Forms.RadioButton opt2nd;
    }
}