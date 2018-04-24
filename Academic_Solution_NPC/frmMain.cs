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
    public partial class frmMain : Form
    {
        private frmClassScheduler classScheduler = null;
        public frmMain()
        {
            InitializeComponent();
            //Dictionary<string, string> dics = new Dictionary<string, string>();
            //dics.Add("@column", "value");
            //MessageBox.Show(dics.Keys.ToList()[0]);
            //MessageBox.Show(dics[dics.Keys.ToList()[0]]);
            //string a = "abcabacasdasd";
            //MessageBox.Show(a.ToCharArray().Count(s => s.Equals('a')).ToString());
        }

        private void classSchedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classScheduler != null)
            {
                classScheduler.WindowState = FormWindowState.Normal;
                classScheduler.Focus();
            }
            else
            {
                classScheduler = new frmClassScheduler();
                classScheduler.MdiParent = this;
                classScheduler.FormClosed += (o, ea) => classScheduler = null;
                classScheduler.Show();
            }
        }
    }
}
