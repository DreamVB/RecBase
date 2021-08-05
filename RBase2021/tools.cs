using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RBase2021;

namespace RBase2021
{
    class tools
    {
        public enum TEditOp
        {
            opEdit = 0,
            opAdd = 1,
            opCopy = 2
        };
        //
        public const string AppTitle = "RecBase XE";

        public static int ButtonPress = 0;
        public static TEditOp EditOp = TEditOp.opEdit;
        //
        //Dialog const string file fillter
        public static string dlgFilter = "RecBase Files(*.rbf)|*.rbf";
        //
        //
        public static string SourceDBFile = string.Empty;
        //
        public static string m_FieldName = string.Empty;
        public static int SelectedRecord = -1;
        public static int GotoRecIndex = 0;
        public static ListViewItem lv_add = new ListViewItem();
        //
        public static string SearchFindStr = string.Empty;
        public static int SearchFindIndex = 0;
        //

        public static RecBase.RBaseTable tbl;

        public static string fixpath(string lPath)
        {
            if (!lPath.EndsWith("\\"))
            {
                return lPath + "\\";
            }
            else
            {
                return lPath;
            }
        }
    }
}
