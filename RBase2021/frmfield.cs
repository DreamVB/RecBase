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
    public partial class frmfield : Form
    {
        public frmfield()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            //Set button press to 1
            tools.ButtonPress = 1;
            //Store the field name in a the class varibale
            tools.m_FieldName = txtFieldName.Text.Trim();
            //close dialog box.
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            //Close dialog box.
            tools.ButtonPress = 0;
            Close();
        }

        private void txtFieldName_TextChanged(object sender, EventArgs e)
        {
            //Enable ok based on string length
            cmdOK.Enabled = (txtFieldName.Text.Trim().Length > 0);
        }

        private void txtFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Check char code for enter
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                //Check if ok is enabled
                if (cmdOK.Enabled)
                {
                    //Raise the buttons click event.
                    cmdOK_Click(sender, e);
                }
            }
        }

        private void frmfield_Load(object sender, EventArgs e)
        {

        }
    }
}
