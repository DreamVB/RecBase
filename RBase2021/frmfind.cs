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
    public partial class frmfind : Form
    {
        public frmfind()
        {
            InitializeComponent();
        }

        private void frmfind_Load(object sender, EventArgs e)
        {
            //Add field names
            for (int x = 0; x < tools.tbl.FieldCount(); x++)
            {
                cboFields.Items.Add(tools.tbl.Field(x));
            }
            if (cboFields.Items.Count > 0)
            {
                cboFields.SelectedIndex = 0;
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            cmdOK.Enabled = (txtValue.Text.Trim().Length > 0);
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmdOK.Enabled)
            {
                if(e.KeyChar==(char)Keys.Return){
                    e.Handled = true;
                    cmdOK_Click(sender, e);
                }
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            tools.ButtonPress = 1;
            tools.SearchFindIndex = cboFields.SelectedIndex;
            tools.SearchFindStr = txtValue.Text;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tools.ButtonPress = 0;
            Close();
        }
    }
}
