using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBase2021
{
    public partial class frmgoto : Form
    {
        public frmgoto()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            //If the record option
            if (rRecord.Checked)
            {
                //Set index to spinner value.
                tools.GotoRecIndex = (int)txtRecNum.Value;
            }

            //Set button press to 1
            tools.ButtonPress = 1;
            //Close form
            Close();

        }

        private void frmgoto_Load(object sender, EventArgs e)
        {
            //Set to first record index
            tools.GotoRecIndex = 0;
            //Set the spinner max value.
            txtRecNum.Maximum = (tools.tbl.RecordCount() - 1);
        }

        private void rFirst_CheckedChanged(object sender, EventArgs e)
        {
            //Set index to first record.
            tools.GotoRecIndex = 0;
        }

        private void rLast_CheckedChanged(object sender, EventArgs e)
        {
            //Set index to last record.
            tools.GotoRecIndex = tools.tbl.RecordCount() - 1;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tools.ButtonPress = 0;
            Close();
        }
    }
}
