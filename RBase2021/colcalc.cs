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
    public partial class colcalc : Form
    {
        private List<double> vals;

        public colcalc()
        {
            InitializeComponent();
        }

        private double get_num(string val)
        {
            try
            {
                return Double.Parse(val);
            }
            catch
            {
                return 0;
            }
        }

        private void colcalc_Load(object sender, EventArgs e)
        {
            cboFunction.Items.Add("Sum");
            cboFunction.Items.Add("Average");
            cboFunction.Items.Add("Max");
            cboFunction.Items.Add("Min");
            cboFunction.Items.Add("Count");

            cboFunction.SelectedIndex = 0;
            //Fill the fields combo box
            for (int x = 0; x < tools.tbl.FieldCount(); x++)
            {
                cboFields.Items.Add(tools.tbl.Field(x));
            }
            if (cboFields.Items.Count > 0)
            {
                cboFields.SelectedIndex = 0;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdCalc_Click(object sender, EventArgs e)
        {
            string sVal = string.Empty;
            vals = new List<double>();
            //Get vals from the field index.
            for (int x = 0; x < tools.tbl.RecordCount(); x++)
            {
                sVal = tools.tbl.GetFieldValue(x, cboFields.SelectedIndex);
                vals.Add(get_num(sVal));
            }

            switch (cboFunction.SelectedIndex)
            {
                case 0:
                    //Calc num
                    txtResult.Text = vals.Sum().ToString();
                    break;
                case 1:
                    //calc average
                    txtResult.Text = vals.Average().ToString();
                    break;
                case 2:
                    //Max
                    txtResult.Text = vals.Max().ToString();
                    break;
                case 3:
                    txtResult.Text = vals.Min().ToString();
                    break;
                case 4:
                    //Calc count
                    txtResult.Text = vals.Count().ToString();
                    break;
            }
            vals.Clear();
        }
    }
}
