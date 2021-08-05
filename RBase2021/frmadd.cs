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
    public partial class frmadd : Form
    {
        public frmadd()
        {
            InitializeComponent();
        }

        private void SetFirstFocus()
        {
            int x = 0;

            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    if (x == 0)
                    {
                        c.Focus();
                        break;
                    }
                    x++;
                }
            }
        }

        private void LoadEditControls()
        {
            //Show edit feilds
            int t = 0;
            TextBox tb = new TextBox();
            Label lb = new Label();
            //Set properties for label and text
            lb.Height = 13;
            tb.Height = 20;
            tb.Width = 320;
            //Clear any old controls from the panel
            panel1.Controls.Clear();
            //Create as many edits as the field count in the database
            for (int x = 0; x < tools.tbl.FieldCount(); x++)
            {
                //Create new text box obj
                tb = new TextBox();
                //Create new label obj
                lb = new Label();
                lb.Name = "ln" + x.ToString();
                tb.Name = "tb" + x.ToString();
                lb.Text = tools.tbl.Field(x);
                tb.ReadOnly = false;

                if (tools.EditOp != tools.TEditOp.opAdd)
                {
                    tb.Text = tools.tbl.GetFieldValue(tools.SelectedRecord,
                        x);
                }

                if (tools.EditOp.Equals(tools.TEditOp.opCopy))
                {
                    tb.ReadOnly = true;
                }

                tb.Left = 13;
                tb.Width = (panel1.Width - tb.Left) - 20;
                tb.Parent = panel1;
                lb.Parent = panel1;
                lb.AutoSize = true;

                if (x == 0)
                {
                    t = 20;
                }
                else
                {
                    t += tb.Height + 30;
                }
                //Set label and text field props
                lb.Left = tb.Left;
                lb.Top = t - 15;
                tb.Top = t;
            }
        }

        private void DoAddRecord()
        {
            int x = 0;
            List<string> rec = new List<string>();
            tools.lv_add = new ListViewItem();

            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text.Trim().Length == 0)
                    {
                        c.Text = "null";
                    }

                    if (x == 0)
                    {
                        tools.lv_add.Text = c.Text;
                    }
                    else
                    {
                        tools.lv_add.SubItems.Add(c.Text);
                    }
                    rec.Add(c.Text);
                    x++;
                }
            }
            tools.tbl.AddRecord(rec);
        }

        private void DoEditRecord()
        {
            int x = 0;

            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text.Trim().Length == 0)
                    {
                        c.Text = "null";
                    }

                    tools.tbl.SetFieldValue(tools.SelectedRecord,
                        x, c.Text);

                    x++;
                }
            }
        }

        private void frmadd_Load(object sender, EventArgs e)
        {
            LoadEditControls();

            Text = "Copy";

            if (tools.EditOp.Equals(tools.TEditOp.opEdit))
            {
                Text = "Edit Record";
            }
            if (tools.EditOp.Equals(tools.TEditOp.opAdd))
            {
                Text = "Add Record";
            }

            SetFirstFocus();
        }

        private void frmadd_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel1.Controls.Clear();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            //Check if we are adding a record.
            if (tools.EditOp.Equals(tools.TEditOp.opAdd))
            {
                DoAddRecord();
            }
            //Check if we are editing
            if (tools.EditOp.Equals(tools.TEditOp.opEdit))
            {
                DoEditRecord();
            }

            tools.ButtonPress = 1;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tools.ButtonPress = 0;
            Close();
        }

    
    }
}
