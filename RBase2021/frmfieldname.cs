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
    public partial class frmfieldname : Form
    {
        public frmfieldname()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            tools.ButtonPress = 1;

            if (tools.SourceDBFile.Length == 0)
            {
                return;
            }

            tools.tbl.Field(cboName.SelectedIndex, txtNewName.Text);

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tools.ButtonPress = 0;
            Close();
        }

        private void txtNewName_TextChanged(object sender, EventArgs e)
        {
            cmdOK.Enabled = (txtNewName.Text.Trim().Length > 0);
        }

        private void frmfieldname_Load(object sender, EventArgs e)
        {
            //Load field names into combo box.
            cboName.Items.Clear();
            //Get names
            for (int x = 0; x < tools.tbl.FieldCount(); x++)
            {
                cboName.Items.Add(tools.tbl.Field(x));
            }
            if (cboName.Items.Count > 0)
            {
                cboName.SelectedIndex = 0;
            }
        }

        private void txtNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmdOK.Enabled)
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    e.Handled = true;
                    cmdOK_Click(sender, e);
                }
            }
        }
    }
}
