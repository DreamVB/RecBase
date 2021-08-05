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
    public partial class frmnew : Form
    {
        public frmnew()
        {
            InitializeComponent();
        }

        private bool IsInList(ListBox lb, string sFind)
        {
            bool found = false;

            foreach (string s in lstFields.Items)
            {
                if (s.ToUpper().Equals(sFind.ToUpper()))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private string dlgSaveName()
        {
            //Show the save dialog box
            SaveFileDialog sd = new SaveFileDialog();
            string lzFile = string.Empty;

            sd.Title = "Save As";
            sd.Filter = tools.dlgFilter;
            if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lzFile = sd.FileName;
            }
            sd.Dispose();
            return lzFile;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            //Close dialog box
            tools.ButtonPress = 0;
            Close();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            frmfield frm = new frmfield();
            //Reset button press.
            tools.ButtonPress = 0;
            //Show new field dialog box
            frm.ShowDialog();
            
            if (tools.ButtonPress == 1)
            {
                //Check if the field name is already in the list.
                if (IsInList(lstFields, tools.m_FieldName))
                {
                    MessageBox.Show("This field is already in the list.",
                        "Add Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Add new field name to listbox.
                    lstFields.Items.Add(tools.m_FieldName);
                }
            }
            //Enable or disable ok button
            cmdSave.Enabled = (lstFields.Items.Count>0);
        }

        private void lstFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Enable delete when an item is clicked.
            cmdDelete.Enabled = true;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete the fieldname.",
                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //Delete the field name from the list,
                lstFields.Items.RemoveAt(lstFields.SelectedIndex);
                //Disable the delete button
                cmdDelete.Enabled = false;
                //Enable or disable ok button.
                cmdSave.Enabled = (lstFields.Items.Count > 0);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            List<string> fields = new List<string>();

            string lzFile = dlgSaveName();
            
            if (lzFile.Length == 0)
            {
                return;
            }

            //Add the field names to the list obj in the tools class.
            for (int x = 0; x < lstFields.Items.Count; x++)
            {
                //Add each field name
                fields.Add(lstFields.Items[x].ToString());
            }
            //Add the fields.
            tools.tbl.AddFields(fields);

            //Try and save the database.
            if (!tools.tbl.Save(lzFile))
            {
                MessageBox.Show("Error Saving Database.",
                    "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tools.SourceDBFile = lzFile;
            tools.ButtonPress = 1;
            //Close new dialog.
            Close();
        }
    }
}
