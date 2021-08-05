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
using System.IO;
using RBase2021;

namespace RBase2021
{
    public partial class frmmain : Form
    {
        private MouseButtons MouseButtonPress = MouseButtons.None;
        private ListViewItem lv_item;
        private int lv_itemIdx = 0;

        private bool ErrorFlag = false;

        public frmmain()
        {
            InitializeComponent();
        }

        private void ColorItems()
        {
            ListViewItem lvi = new ListViewItem();

            if (lstRecords.Items.Count > 0)
            {
                for (int x = 0; x < lstRecords.Items.Count; x++)
                {
                    lvi = lstRecords.Items[x];

                    if (x % 2 == 0)
                    {
                        lvi.BackColor = Color.FromArgb(255, 218, 255);
                    }
                    else
                    {
                        lvi.BackColor = Color.FromArgb(245, 245, 245);
                    }
                }
            }
        }


        private void DoUpdateDB()
        {
            ErrorFlag = false;
            if (!tools.tbl.Save(tools.SourceDBFile))
            {
                ErrorFlag = true;
                MessageBox.Show("Error While Updateing Database.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusBar1()
        {
            FileInfo fi;
            if (lstRecords.Items.Count == 0)
            {
                tipBsrRecord.Text = "Records 0";
            }
            else
            {
                tipBsrRecord.Text = "Record " + (lv_item.Index + 1).ToString() + "/" +
                 lstRecords.Items.Count.ToString();
            }
            //
            fi = new FileInfo(tools.SourceDBFile);
            tipFilesize.Text = fi.Length.ToString() + " Bytes";
        }

        private void EnableNav()
        {
            mnuRecFirst.Enabled = (lv_itemIdx != 0);
            mnuRecNext.Enabled = (lv_itemIdx >= 0);
            mnuRecPrev.Enabled = (lv_itemIdx != 0);
            mnuRecLast.Enabled = (lv_itemIdx != lstRecords.Items.Count);

            if (lv_itemIdx == lstRecords.Items.Count - 1)
            {
                mnuRecNext.Enabled = false;
                mnuRecLast.Enabled = false;
            }
            else
            {
                mnuRecNext.Enabled = true;
                mnuRecLast.Enabled = true;
            }
            cmdNavFirst.Enabled = mnuRecFirst.Enabled;
            cmdNavNext.Enabled = mnuRecNext.Enabled;
            cmdNavPrev.Enabled = mnuRecPrev.Enabled;
            cmdNavLast.Enabled = mnuRecLast.Enabled;
            //

            if (lstRecords.Items.Count == 0)
            {
                cmdNavFirst.Enabled = false;
                cmdNavNext.Enabled = false;
                cmdNavPrev.Enabled = false;
                cmdNavLast.Enabled = false;
                mnuRecFirst.Enabled = false;
                mnuRecNext.Enabled = false;
                mnuRecPrev.Enabled = false;
                mnuRecLast.Enabled = false;
            }

        }

        private void UpdateButtons(bool Update)
        {
            mnuRecEdit.Enabled = Update;
            mnuRecDelete.Enabled = Update;
            cmdEditRec.Enabled = Update;
            cmdDelRec.Enabled = Update;
            mnuCopyVals.Enabled = Update;
            mnuCopyRecord.Enabled = Update;
            mnuRecDup.Enabled = Update;
            cmdRecCpyVals.Enabled = Update;
            cmdRecCpySelected.Enabled = Update;
            cmdCpyRecords.Enabled = Update;
        }

        private void db_FillList()
        {
            //Clear list
            ListViewItem lv;
            lstRecords.Items.Clear();
            //Add the headers
            lstRecords.Columns.Clear();

            for (int col = 0; col < tools.tbl.FieldCount(); col++)
            {
                lstRecords.Columns.Add(tools.tbl.Field(col));
            }

            //Enable rename table menu item
            mnuRenameField.Enabled = (lstRecords.Columns.Count > 0);
            cmdRenameTable.Enabled = mnuRenameField.Enabled;

            //lstRecords.Columns[lstRecords.Columns.Count - 1].Width = -2;

            //Add records to the list view
            for (int r = 0; r < tools.tbl.RecordCount(); r++)
            {
                lv = new ListViewItem();
                lv.Text = tools.tbl.GetFieldValue(r, 0);

                for (int x = 1; x < tools.tbl.FieldCount(); x++)
                {
                    lv.SubItems.Add(tools.tbl.GetFieldValue(r, x));
                }

                lstRecords.Items.Add(lv);
            }

            lv_itemIdx = 0;
            lv_item = null;
            //
            mnuZapRecords.Enabled = (lstRecords.Items.Count > 0);
            mnuCalcFields.Enabled = mnuZapRecords.Enabled;
            cmdCalcFields.Enabled = mnuCalcFields.Enabled;
            //
            if (lstRecords.Items.Count != 0)
            {
                lstRecords.Items[0].Selected = true;
            }
        }

        private string dlgOpenName(){
            //Show the open dialog box.
            OpenFileDialog od = new OpenFileDialog();
            string lzFile = string.Empty;

            od.Title = "Open";
            od.Filter = tools.dlgFilter;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lzFile = od.FileName;
            }
            od.Dispose();
            return lzFile;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get open file name
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmmain_Load(object sender, EventArgs e)
        {
            tools.tbl = new RecBase.RBaseTable();
            this.Text = tools.AppTitle;
            EnableNav();
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            //Create new fields dialog
            frmnew frm = new frmnew();

            //Resset the button press to 0
            tools.ButtonPress = 0;
            //Show new dialog.
            frm.ShowDialog();
            //Test for ok press.
            if (tools.ButtonPress == 1)
            {
                if (!tools.tbl.Load(tools.SourceDBFile))
                {

                }
                else
                {
                    db_FillList();
                    mnuRecAdd.Enabled = true;
                    cmdAddRec.Enabled = true;
                    mnuCopyAllRecs.Enabled = false;
                    //
                    cmdRecCpyVals.Enabled = false;
                    cmdRecCpySelected.Enabled = false;
                    cmdCpyRecords.Enabled = false;
                    //
                    cmdDBInfo.Enabled = true;
                    mnuDbInfo.Enabled = true;
                    mnuZapRecords.Enabled = false;
                    //Disable buttons
                    UpdateButtons(false);
                    EnableNav();
                    UpdateStatusBar1();
                }
            }
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            mnuNew_Click(sender, e);
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            frmabout frm = new frmabout();
            frm.ShowDialog();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            mnuOpen_Click(sender, e);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            tools.SourceDBFile = dlgOpenName();

            //Check filename length
            if (tools.SourceDBFile.Length > 0)
            {
                //Open  the database.
                if (!tools.tbl.Load(tools.SourceDBFile))
                {

                }
                else
                {
                    db_FillList();
                    mnuRecAdd.Enabled = true;
                    cmdAddRec.Enabled = true;
                    //
                    cmdDBInfo.Enabled = true;
                    mnuDbInfo.Enabled = true;
                    mnuGoto.Enabled = true;
                    //Disable buttons
                    UpdateButtons(false);

                    if (lstRecords.Items.Count == 0)
                    {
                        UpdateStatusBar1();
                    }
                    else
                    {
                        mnuCopyAllRecs.Enabled = true;
                        cmdCpyRecords.Enabled = true;
                    }

                }
            }
            ColorItems();
        }

        private void lstRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo fi;
            foreach (ListViewItem lv in lstRecords.SelectedItems)
            {
                lv_item = lv;
            }

            //Set the selected record.
            lv_itemIdx = lv_item.Index;
            tools.SelectedRecord = lv_item.Index;
            UpdateButtons(true);
            EnableNav();
            //Update status bar.
            UpdateStatusBar1();
            
        }

        private void mnuRecEdit_Click(object sender, EventArgs e)
        {
            frmadd frm = new frmadd();
            tools.ButtonPress = 0;
            tools.EditOp = tools.TEditOp.opEdit;

            if (lv_item != null)
            {
                frm.ShowDialog();
                if (tools.ButtonPress == 1)
                {
                    try
                    {
                        //Update the record.
                        lv_item.Text = tools.tbl.GetFieldValue(tools.SelectedRecord, 0);

                        //Update the database file.
                        DoUpdateDB();

                        if (ErrorFlag)
                        {
                            return;
                        }
                        else
                        {
                            for (int x = 1; x < tools.tbl.FieldCount(); x++)
                            {
                                lv_item.SubItems[x].Text =
                                    tools.tbl.GetFieldValue(tools.SelectedRecord, x);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            UpdateStatusBar1();
        }

        private void lstRecords_DoubleClick(object sender, EventArgs e)
        {
            if (MouseButtonPress == MouseButtons.Left)
            {
                if (lv_item != null)
                {
                    mnuRecEdit_Click(sender, e);
                }
            }
        }

        private void lstRecords_MouseClick(object sender, MouseEventArgs e)
        {
            MouseButtonPress = e.Button;
        }

        private void lstRecords_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lv_item != null)
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    mnuRecEdit_Click(sender, e);
                }
            }
        }

        private void mnuRecDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.None;

            if (lv_item != null)
            {

                dr = MessageBox.Show("Are you sure you want to delete this record.",
                    "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //Delete record.
                    try
                    {
                        //Delete from the database
                        tools.tbl.DeleteRecord(tools.SelectedRecord);
                        //Update the database file.
                        DoUpdateDB();

                        if (ErrorFlag)
                        {
                            return;
                        }
                        else
                        {
                            //Delete the item from the listview
                            lstRecords.Items.RemoveAt(lv_item.Index);
                            //Set item to null
                            if (lstRecords.Items.Count > 0)
                            {
                                lv_item = lstRecords.Items[0];
                                lv_item.Selected = true;
                            }
                            else
                            {
                                lv_item = null;
                            }
                            //Enable or disable copy all records button
                            mnuCopyAllRecs.Enabled = (lstRecords.Items.Count > 0);
                            cmdCpyRecords.Enabled = mnuCopyAllRecs.Enabled;
                            //Disable buttons
                            UpdateButtons(false);
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            mnuCalcFields.Enabled = (lstRecords.Items.Count > 0);
            cmdCalcFields.Enabled = mnuCalcFields.Enabled;

            UpdateStatusBar1();
            ColorItems();
        }

        private void mnuRecAdd_Click(object sender, EventArgs e)
        {
            frmadd frm = new frmadd();
            tools.ButtonPress = 0;
            tools.EditOp = tools.TEditOp.opAdd;
            frm.ShowDialog();

            if (tools.ButtonPress == 1)
            {
                if (tools.lv_add != null)
                {
                    //Update the database file.
                    DoUpdateDB();

                    if (ErrorFlag)
                    {
                        return;
                    }
                    else
                    {
                        lstRecords.Items.Add(tools.lv_add);
                        mnuZapRecords.Enabled = true;
                        mnuCalcFields.Enabled = true;
                        cmdCalcFields.Enabled = true;
                        mnuCopyAllRecs.Enabled = true;
                        //
                        cmdCpyRecords.Enabled = true;
                        //Seleft the last item
                        lstRecords.Items[lstRecords.Items.Count - 1].Selected = true;
                        tools.lv_add = null;
                    }
                }
            }
            UpdateStatusBar1();
            ColorItems();
        }

        private void cmdAddRec_Click(object sender, EventArgs e)
        {
            mnuRecAdd_Click(sender, e);
        }

        private void cmdEditRec_Click(object sender, EventArgs e)
        {
            mnuRecEdit_Click(sender, e);
        }

        private void cmdDelRec_Click(object sender, EventArgs e)
        {
            mnuRecDelete_Click(sender, e);
            mnuZapRecords.Enabled = (lstRecords.Items.Count > 0);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {

        }

        private void cmdRecCopy_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuRecFirst_Click(object sender, EventArgs e)
        {
            if (lstRecords.Items.Count > 0)
            {
                lstRecords.Items[0].Selected = true;
                lv_itemIdx = 0;
                mnuRecFirst.Enabled = false;
                mnuRecPrev.Enabled = false;
                //
                mnuRecNext.Enabled = true;
                mnuRecLast.Enabled = true;
            }
        }

        private void mnuRecLast_Click(object sender, EventArgs e)
        {
            if (lstRecords.Items.Count > 0)
            {
                lv_itemIdx = lstRecords.Items.Count - 1;
                lstRecords.Items[lv_itemIdx].Selected = true;
                //
                mnuRecNext.Enabled = false;
                mnuRecLast.Enabled = false;
                mnuRecPrev.Enabled = true;
                mnuRecFirst.Enabled = true;
            }
        }

        private void mnuRecNext_Click(object sender, EventArgs e)
        {
            if (lstRecords.Items.Count == 0)
            {
                return;
            }

            lv_itemIdx++;

            mnuRecFirst.Enabled = true;
            mnuRecPrev.Enabled = true;

            if (lv_itemIdx + 1 >= lstRecords.Items.Count )
            {
                mnuRecNext.Enabled = false;
                mnuRecLast.Enabled = false;

                lv_itemIdx = lstRecords.Items.Count - 1;
            }

            lstRecords.Items[lv_itemIdx].Selected = true;
        }

        private void mnuRecPrev_Click(object sender, EventArgs e)
        {
            if (lstRecords.Items.Count == 0)
            {
                return;
            }
            mnuRecNext.Enabled = true;
            mnuRecLast.Enabled = true;

            lv_itemIdx--;

            if (lv_itemIdx  <=0)
            {
                mnuRecPrev.Enabled = false;
                mnuRecFirst.Enabled = false;
                lv_itemIdx = 0;
            }

            lstRecords.Items[lv_itemIdx].Selected = true;
        }

        private void cmdNavFirst_Click(object sender, EventArgs e)
        {
            mnuRecFirst_Click(sender, e);
        }

        private void cmdNavPrev_Click(object sender, EventArgs e)
        {
            mnuRecPrev_Click(sender, e);
        }

        private void cmdNavNext_Click(object sender, EventArgs e)
        {
            mnuRecNext_Click(sender, e);
        }

        private void cmdNavLast_Click(object sender, EventArgs e)
        {
            mnuRecLast_Click(sender, e);
        }

        private void mnuFitColWidth_Click(object sender, EventArgs e)
        {
            //Autosize Column headers.
            foreach (ColumnHeader ch in lstRecords.Columns)
            {
                ch.Width = -1;
            }
        }

        private void mnuNewWindow_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            //Exe file to load
            string exef = tools.fixpath(Environment.CurrentDirectory) + "RBase2021.exe";

            try
            {
                //Set process filename
                p.StartInfo.FileName = exef;
                //Start process.
                p.Start();
                //Destory process.
                p.Dispose();
            }
            catch (Exception ex)
            {
                //Error
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            int o_idx = 0;

            if (lv_item != null)
            {
                o_idx = lv_item.Index;
            }

            db_FillList();

            if (lv_item != null)
            {
                lstRecords.Items[o_idx].Selected = true;
            }

            ColorItems();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            mnuRefresh_Click(sender, e);
        }

        private void mnuNewTable_Click(object sender, EventArgs e)
        {
            mnuNew_Click(sender, e);
        }

        private void mnuRenameField_Click(object sender, EventArgs e)
        {
            frmfieldname frm = new frmfieldname();
            tools.ButtonPress = 0;
            frm.ShowDialog();
            if (tools.ButtonPress == 1)
            {
                //Update datanase
                DoUpdateDB();
                //Refresh data.
                db_FillList();
                ColorItems();
            }
        }

        private void tipBsrRecord_MouseHover(object sender, EventArgs e)
        {
            tipBsrRecord.ToolTipText = tipBsrRecord.Text;
        }

        private void lstRecords_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            lstRecords.FocusedItem.Focused = false;
        }

        private void mnuViewToolbar_Click(object sender, EventArgs e)
        {
            mnuViewToolbar.Checked = !mnuViewToolbar.Checked;
            tBarMain.Visible = mnuViewToolbar.Checked;
        }

        private void mnuViewStatusbar_Click(object sender, EventArgs e)
        {
            mnuViewStatusbar.Checked = !mnuViewStatusbar.Checked;
            sBar1.Visible = mnuViewStatusbar.Checked;
        }

        private void mnuDbInfo_Click(object sender, EventArgs e)
        {
            frminfo frm = new frminfo();

            if (tools.SourceDBFile.Length > 0)
            {
                frm.ShowDialog();
            }
        }

        private void cmdDBInfo_Click(object sender, EventArgs e)
        {
            mnuDbInfo_Click(sender, e);
        }

        private void mnuGoto_Click(object sender, EventArgs e)
        {
            frmgoto frm = new frmgoto();
            if (lstRecords.Items.Count == 0)
            {
                return;
            }
            tools.ButtonPress = 0;
            frm.ShowDialog();
            if (tools.ButtonPress != 0)
            {
                lstRecords.Items[tools.GotoRecIndex].Selected = true;
            }
        }

        private void cmdRenameTable_Click(object sender, EventArgs e)
        {
            mnuRenameField_Click(sender, e);
        }

        private void mnuGridLines_Click(object sender, EventArgs e)
        {
            mnuGridLines.Checked = !mnuGridLines.Checked;
            lstRecords.GridLines = mnuGridLines.Checked;
        }

        private void mnuRecDup_Click(object sender, EventArgs e)
        {
            //Get current record valules.
            List<string> vals = new List<string>();
            ListViewItem lv = new ListViewItem();

            if (lv_item != null)
            {
                vals = tools.tbl.GetRecord(lv_itemIdx);
                //Apend the record to the database.
                tools.tbl.AddRecord(vals);
                //Update database.
                DoUpdateDB();
                //Check error flag
                if (!ErrorFlag)
                {
                    //Get first item
                    lv.Text = vals[0];
                    //Get sub items
                    for (int x = 1; x < vals.Count; x++)
                    {
                        lv.SubItems.Add(vals[x]);
                    }

                    //Add the record to the litview.
                    lstRecords.Items.Add(lv);
                    //Select the last item.
                    lstRecords.Items[lstRecords.Items.Count - 1].Selected = true;
                }
            }
        }

        private void mnuZapRecords_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.None;

            if (lstRecords.Items.Count == 0)
            {
                return;
            }

            dr = MessageBox.Show("Are you sure you want to delete all records.",
                "Zap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                tools.tbl.ZapRecords();
                //Update database
                DoUpdateDB();
                //Test for error flag.
                if (!ErrorFlag)
                {
                    //Clear all the items in the listview.
                    lstRecords.Items.Clear();
                    //Update buttons and menus
                    UpdateButtons(false);
                    EnableNav();
                    mnuZapRecords.Enabled = false;
                    mnuCalcFields.Enabled = false;
                    cmdCalcFields.Enabled = false;
                    mnuCopyAllRecs.Enabled = false;
                    cmdCpyRecords.Enabled = false;
                    //Enable nav buttons
                    EnableNav();
                }
            }
            UpdateStatusBar1();
        }

        private void mnuCalcFields_Click(object sender, EventArgs e)
        {
            colcalc frm = new colcalc();

            frm.ShowDialog();

        }

        private void cmdCalcFields_Click(object sender, EventArgs e)
        {
            mnuCalcFields_Click(sender, e);
        }

        private void mnuCopyVals_Click(object sender, EventArgs e)
        {
            frmadd frm = new frmadd();
            tools.ButtonPress = 0;
            tools.EditOp = tools.TEditOp.opCopy;
            frm.ShowDialog();
        }

        private void mnuCopyRecord_Click(object sender, EventArgs e)
        {
            string sRec = lv_item.Text + "\t";

            for (int x = 1; x < lv_item.SubItems.Count; x++)
            {
                //Copy sub items text
                sRec += lv_item.SubItems[x].Text + "\t";
            }
            //Trim the records last tab
            sRec = sRec.Trim();
            //Send to clipboard.
            Clipboard.Clear();
            Clipboard.SetText(sRec, TextDataFormat.Text);
            MessageBox.Show("Record was copiyed to the clipboard.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuCopyAllRecs_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            List<string> rec = new List<string>();

            string sRec = string.Empty;


            for (int i = 0; i < tools.tbl.RecordCount(); i++)
            {
                rec = tools.tbl.GetRecord(i);

                for (int x = 0; x < rec.Count; x++)
                {
                    //Copy sub items text
                    sRec += rec[x] + "\t";
                }
                sRec = sRec.TrimEnd();

                sb.AppendLine(sRec);
                sRec = string.Empty;
            }
            //Trim the records last tab
            sRec = sb.ToString().Trim();
            sb.Clear();

            //Send to clipboard.
            Clipboard.Clear();
            Clipboard.SetText(sRec, TextDataFormat.Text);
            MessageBox.Show(tools.tbl.RecordCount().ToString() +
                " records were copiyed to the clipboard.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmdRecCpyVals_Click(object sender, EventArgs e)
        {
            mnuCopyVals_Click(sender, e);
        }

        private void cmdRecCpySelected_Click(object sender, EventArgs e)
        {
            mnuCopyRecord_Click(sender, e);
        }

        private void cmdCpyRecords_Click(object sender, EventArgs e)
        {
            mnuCopyAllRecs_Click(sender, e);
        }

        private void mnuRecFind_Click(object sender, EventArgs e)
        {
            frmfind frm = new frmfind();
            ListViewItem li = new ListViewItem();
            bool iFound = false;
            if (lstRecords.Items.Count == 0)
            {
                return;
            }
            //Reset tools button press.
            tools.ButtonPress = 0;
            //Show find dialog.
            frm.ShowDialog();

            //Check buttons press
            if (tools.ButtonPress == 1)
            {
                //Loop the records in the listview,
                for (int x = 0; x < lstRecords.Items.Count; x++)
                {
                    //Get listview item
                    li = lstRecords.Items[x];
                    //Check field index
                    if (tools.SearchFindIndex == 0)
                    {
                        //Check for string in selected item.
                        if (li.Text.Contains(tools.SearchFindStr))
                        {
                            iFound = true;
                            break;
                        }
                    }
                    else
                    {
                        if (li.SubItems[tools.SearchFindIndex].Text.Contains(tools.SearchFindStr))
                        {
                            iFound = true;
                            break;
                        }
                    }
                }
                if (!iFound)
                {
                    MessageBox.Show("String not found.",
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Select the found index.
                    lstRecords.Items[li.Index].Selected = true;
                    li = null;
                }
            }
        }
    }
}
