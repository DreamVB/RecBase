using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace csvbuddy
{
    class dmcsv
    {
        private string m_filename = string.Empty;
        private static List<string> Headers;
        private static List<string> Records;

        string strline = string.Empty;
        static char m_seperator = ',';

        public dmcsv()
        {
            Records = new List<string>();
            Headers = new List<string>();
        }

        public static char Seperator
        {
            get
            {
                return m_seperator;
            }
            set
            {
                m_seperator = value;
            }
        }

        public int RecordCount
        {
            get
            {
                return Records.Count;
            }
        }

        public int FieldCount
        {
            get
            {
                return Headers.Count;
            }
        }

        public void AddReord(List<string> Items)
        {
            string s1 = string.Empty;

            if ((Items.Count < 0) | (Items.Count > FieldCount))
            {
                throw new System.Exception("Index of of range.");
            }
            else
            {
                //Add the records.
                //We need to join the items in the list.
                s1 = string.Join(m_seperator.ToString(), Items.ToArray());
                //Add the record.
                Records.Add(s1);
                //Clear up.
            }
        }

        public void AddField(string Value)
        {
            int x = 0;
            string rec = string.Empty;

            //Add field.
            Headers.Add(Value);
            //Modify the record with the new field.
            for (x = 0; x < RecordCount; x++)
            {
                //Append extra field value to record.
                rec = Records[x] + m_seperator + " ";
                //Replace the record.
                Records[x] = rec;
            }
            //Tidy
            rec = string.Empty;
        }

        public void ClearRecords()
        {
            //Clear all records data.
            Records.Clear();
        }

        public void ClearFields()
        {
            //Clear all fields data.
            Headers.Clear();
        }

        public bool ReadCSV(string Filename)
        {
            bool IsGood = true;
            bool GoHeaders = false;
            FileInfo fi = new FileInfo(Filename);

            if (!fi.Exists)
            {
                IsGood = false;
            }
            else
            {
                //Reset lists.
                ClearFields();
                ClearRecords();
                try
                {
                    using (StreamReader sr = new StreamReader(fi.FullName))
                    {
                        while (!sr.EndOfStream)
                        {
                            //Get line
                            strline = sr.ReadLine().Trim();

                            if (strline.Length > 0)
                            {
                                if (!GoHeaders)
                                {
                                    //Get headers.
                                    Headers = SpliStr(strline,m_seperator);
                                    GoHeaders = true;
                                    //Get next line
                                }
                                else
                                {
                                    Records.Add(strline);
                                }
                            }
                        }
                        //Close file.
                        sr.Close();
                    }
                }
                catch(Exception e)
                {
                    IsGood = true;
                    throw new  System.Exception(e.Message);
                }
            }
            
            return IsGood;
        }

        public bool WriteCSV(string Filename)
        {
            bool IsGood = true;
            StringBuilder sb = new StringBuilder();
            FileInfo fi = new FileInfo(Filename);
            
            //Get headers.
            sb.AppendLine(string.Join(m_seperator.ToString(),Headers.ToArray()));
            
            //Get records.
            for(int x=0;x<RecordCount;x++)
            {
                //Append records.
                sb.AppendLine(Records[x]);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(fi.FullName))
                {
                    //Write data to file.
                    sw.Write(sb.ToString());
                    sw.Close();
                    sb.Clear();
                }
            }
            catch
            {
                IsGood = false;
            }

            return IsGood;
        }

        public List<string> ReadRecordValues(int Index)
        {
            //Return a record as a list.
            if ((Index < 0) | (Index > RecordCount))
            {
                throw new System.Exception("Record index of of range.");
            }
            else
            {
                return SpliStr(Records[Index], m_seperator);
            }
        }

        public string ReadFieldValue(int Record, int Field)
        {
            List<string> Temp = new List<string>();

            if (!CheckRange(Record, Field))
            {
                return string.Empty;
            }
            
            //Split the record.
            return SpliStr(Records[Record],m_seperator)[Field];
        }

        public void WriteFieldValue(int Record, int Field, string Value)
        {
            List<string> Temp = new List<string>();
            string s1 = string.Empty;
            string s2 = Value;
            if (s2.IndexOf(Seperator) != -1)
            {
                s2 = "\"" + s2 + "\"";
            }
            //Check if in range.
            if (!CheckRange(Record, Field)) { return; }

            Temp = SpliStr(Records[Record],m_seperator);
            //Set value
            Temp[Field] = s2;
            //Push the record back.
            s1 = string.Join(m_seperator.ToString(), Temp.ToArray());
            Records[Record] = s1;
            //Clear up
            s1 = string.Empty;
            Temp.Clear();
        }

        public string GetFieldName(int index)
        {
            //Return field name.
            if ((index < 0) | (index > FieldCount))
            {
                throw new System.Exception("Field index of of range.");
            }
            else
            {
                return Headers[index];
            }
        }

        public List<string> FieldNames()
        {
            //Return fields
            return Headers;
        }

        public void SetFields(List<string> Fields)
        {
            //Clear records
            ClearRecords();
            //Clear fields.
            ClearFields();
            //Set the new headers.
            Headers.AddRange(Fields);
        }

        public void SetFieldName(int index, string Value)
        {
            //Return field name.
            if ((index < 0) | (index > FieldCount))
            {
                throw new System.Exception("Field index of of range.");
            }
            else
            {
                Headers[index] = Value;
            }
        }

        private static bool CheckRange(int r, int f)
        {
            bool IsGood = true;
            
            if ((r < 0) | (r > Records.Count))
            {
                IsGood = false;
                throw new System.Exception("Record index out of range.");
            }

            if ((f < 0) | (f > Headers.Count))
            {
                IsGood = false;
                throw new System.Exception("Field index out of range.");
            }
            return IsGood;
        }

        public void DeleteRecord(int record)
        {
            if ((record < 0) | (record > RecordCount))
            {
                throw new System.Exception("Record index of out range.");
            }
            else
            {
                //Delete the record from the list
                Records.RemoveAt(record);
            }
        }

        public void DeleteField(int Index)
        {
            int x = 0;
            List<string> Temp = new List<string>();
            List<string> TempRecs = new List<string>();
            string s1 = string.Empty;

            //Test index
            if ((Index < 0) | (Index > FieldCount))
            {
                throw new System.Exception("Field index out of range.");
            }
            else
            {
                //Delete field
                Headers.RemoveAt(Index);
                //Delete a field name plus the records value.
                //Go tho the record and delete the value.
                for (x = 0; x < RecordCount; x++)
                {
                    //Get each record into the list
                    Temp = SpliStr(Records[x], m_seperator);
                    //Delete the item.
                    Temp.RemoveAt(Index);
                    //Rebuild the record.
                    s1 = string.Join(m_seperator.ToString(), Temp.ToArray());
                    //Add records
                    TempRecs.Add(s1);
                }
            }
            //Clear temp
            Temp.Clear();
            //Clear old records.
            ClearRecords();
            //Add the new records.
            Records.AddRange(TempRecs);
        }

        public int FindFieldValue(int index, int field, string value, bool FindDown=true)
        {
            int RowIdx = -1;
            string retval = string.Empty;

            for (int i = index; i < RecordCount; i++)
            {
                //Read field value.
                retval = ReadFieldValue(i, field).ToLower();
                if (retval.Equals(value))
                {
                    RowIdx = i;
                    break;
                }
            }
            //Clear up
            retval = string.Empty;
            //Return index.
            return RowIdx;
        }

        private static List<string> SpliStr(string src, char seperator)
        {
            List<string> temp = new List<string>();
            string buffer = string.Empty;
            string s = "";
            string s1 = src;
            string[] parts;

            StringReader sr = new StringReader(src);

            using (TextFieldParser parser = new TextFieldParser(sr))
            {
                parser.SetDelimiters(seperator.ToString());
                parser.HasFieldsEnclosedInQuotes = true;

                while (true)
                {
                    parts = parser.ReadFields();
                    if (parts == null)
                    {
                        break;
                    }
                    for (int x = 0; x < parts.Length; x++)
                    {
                        string s2 = parts[x];
                        if (s2.IndexOf(seperator) != -1)
                        {
                            s2 = "\"" + s2 + "\"";
                        }
                        temp.Add(s2);
                        //Clear up
                        s2 = string.Empty;
                    }
                    //Clear up.
                    Array.Clear(parts, 0, parts.Length);
                }
                parser.Close();
            }
            //Return list.
            return temp;
        }
    }
}
