using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace RBase2021
{
    public partial class frmabout : Form
    {
        public frmabout()
        {
            InitializeComponent();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmabout_Load(object sender, EventArgs e)
        {
            lblTitle.Text = tools.AppTitle;
            Text = "About " + tools.AppTitle;
        }

        private void lnkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process p = new Process();
            //
            try
            {
                p.StartInfo.FileName = "https://github.com/DreamVB/RecBase";
                p.Start();
                p.Dispose();
            }
            catch { }
        }
    }
}
