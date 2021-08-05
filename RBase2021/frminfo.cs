using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RBase2021
{
    public partial class frminfo : Form
    {
        private StringBuilder sb = new StringBuilder();

        public frminfo()
        {
            InitializeComponent();
        }

        private string dlgSaveName()
        {
            //Show the save dialog box
            SaveFileDialog sd = new SaveFileDialog();
            string lzFile = string.Empty;

            sd.Title = "Save As";
            sd.Filter = "Text Files(*.txt)|*.txt";
            if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lzFile = sd.FileName;
            }
            sd.Dispose();
            return lzFile;
        }

        private void frminfo_Load(object sender, EventArgs e)
        {
            //Show info.
            sb.AppendLine(tools.SourceDBFile);
            sb.AppendLine("File size (bytes): " + new FileInfo(tools.SourceDBFile).Length.ToString());
            sb.AppendLine("Fields: \t" + tools.tbl.FieldCount().ToString());
            sb.AppendLine("Records:\t" + tools.tbl.RecordCount().ToString());
            sb.AppendLine();

            for (int x = 0; x < tools.tbl.FieldCount(); x++)
            {
                sb.AppendLine((x+1).ToString() + "\t" + tools.tbl.Field(x));
            }
            //Set text
            txtInfo.Text = sb.ToString();
            txtInfo.SelectionStart = 0;
            sb.Clear();
        }

        private void frminfo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string lzFile = dlgSaveName();
            if (lzFile.Length > 0)
            {
                //Save file
                try
                {
                    using (StreamWriter sw = new StreamWriter(lzFile))
                    {
                        sw.Write(txtInfo.Text);
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
