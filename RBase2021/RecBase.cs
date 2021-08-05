/*
 * RecBase Beta 2.5
 * 
 * RecBase is a small project I am working on to make my own DBMS the system is very basic
 * it uses plain text files as the tables at the moment it only supports one table
 * it can be used to store very simple data maybe for a simple address book
 * 
 * What I hope to add next
 * Work with multiple tables
 * Add support for encrypted dbases
 * Add support for sorting
 * Add support for searching
 * Clean up bugs and do some tweaking
 * Add support to export to CSV
 * Add support to inport CSV
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RecBase
{
    public class RBaseTable
    {
        private static List<List<string>> _records = new List<List<string>>();
        private static List<string> _fields = new List<string>();

        public int FieldCount()
        {
            //Return field count
            return _fields.Count;
        }

        public int RecordCount()
        {
            //Return record count
            return _records.Count;
        }

        private string get_tagVal(string Tag, string LineSource,
            bool TrimBlank = false)
        {
            string sStart = "[" + Tag + "]";
            string sEnd = "[/" + Tag + "]";
            string tmp = null;

            //EXtract data from a tag eg [tag]data[/tag] return data
            try
            {
                //Check if line starts with start tag
                if (LineSource.StartsWith(sStart, StringComparison.OrdinalIgnoreCase))
                {
                    //Copy eveything from the start tag to end of string
                    tmp = LineSource.Substring(sStart.Length);
                }
                else
                {
                    //Set to null
                    tmp = null;
                }
                //Check if string ends with end tag
                if (tmp.EndsWith(sEnd, StringComparison.OrdinalIgnoreCase))
                {
                    //Start copying from start of string to end tag
                    tmp = tmp.Substring(0, (tmp.Length - sEnd.Length));
                }
                else
                {
                    //Set string to null
                    tmp = null;
                }
                //Check if striping blanks from the new extracted value
                if (TrimBlank)
                {
                    //Trim any white space.
                    tmp = tmp.Trim();
                }
            }
            catch
            {
                //Set tmp to null
                tmp = null;
            }
            //return tmp
            return tmp;
        }

        private int field2index(string f)
        {
            //Locate the index of a field name by checking it's string value.
            
            //Default field index value
            int idx = -1;
            
            for (int x = 0; x < FieldCount(); x++)
            {
                //Compare the field value with f
                if (_fields[x].ToLower() == f.ToLower())
                {
                    //Set the field index found
                    idx = x;
                    //Break here
                    break;
                }
            }
            //Return the value of the field index found
            return idx;
        }

        public void Clean(){
            _records = new List<List<string>>();
            _fields = new List<string>();
        }

        public void ZapRecords()
        {
            _records.Clear();
        }

        public void AddRecord(List<string>values){
            //Add new record
            _records.Add(values);
        }

        public void AddFields(List<string>fields){
            //Clear records
            _records.Clear();
            //Add field names
            _fields = fields;
        }

        public void DeleteField(int index)
        {
            List<string> rec = new List<string>();
            //Check for vaild index
            if (index >= 0 && index < FieldCount())
            {
                //Loop tho the records
                for (int x = 0; x < RecordCount(); x++)
                {
                    //Get record values
                    rec = _records[x];
                    //Remove index element from the list
                    rec.RemoveAt(index);
                    //Set the current record with the updated rec list.
                    _records[x] = rec;
                }
                //Remove the field name from fields list
                _fields.RemoveAt(index);
            }
        }

        public void DeleteField(string f)
        {
            //Get field name index
            int idx = field2index(f);
            //Check for vaild index
            if (idx != -1)
            {
                //Delete the field
                DeleteField(idx);
            }
        }

        public bool FieldExists(string f)
        {
            return field2index(f) != -1;
        }
        
        public string Field(int index)
        {
            if (index >= 0 && index < FieldCount())
            {
                return _fields[index];
            }
            return null;
        }

        public void Field(int index, string value){
            if (index >= 0)
            {
                _fields[index] = value;
            }
        }

        public void Field(string name, string value)
        {
            int idx = field2index(name);

            if (idx != -1)
            {
                _fields[idx] = value;
            }
        }

        public bool Save(string Filename)
        {
            bool is_good = true;

            try
            {
                using (StreamWriter sw = new StreamWriter(Filename))
                {
                    //Save fields
                    sw.Write("[fields]\n");

                    foreach (string f in _fields)
                    {
                        sw.Write(" [field]" + f + "[/field]\n");
                    }

                    sw.Write("[/fields]\n\n");

                    //Write records and field values
                    foreach (List<string> r in _records)
                    {
                        //Write record start
                        sw.Write("[record]\n");
                        //Write field values
                        foreach (string f in r)
                        {
                            //Write record fieldvalue
                            sw.Write(" [value]" + f + "[/value]\n");
                        }
                        //Write record start
                        sw.Write("[/record]\n\n");
                    }
                    //Close file
                    sw.Close();
                }
            }
            catch
            {
                //Error
                is_good = false;
            }

            return is_good;
        }

        public List<string> GetRecord(int record)
        {
            //Returns a records values
            List<string> rec = new List<string>();
            //Check range
            if (record >= 0 && record < RecordCount())
            {
                //Get record
                rec = _records[record];
            }
            //Return record list
            return rec;
        }

        public List<string> GetFields()
        {
            //Return field names.
            return _fields;
        }

        public void DeleteRecord(int record)
        {
            //Delete a record
            if (record >= 0 && record < RecordCount())
            {
                //Remove from records list
                _records.RemoveAt(record);
            }
        }

        public string GetFieldValue(int record, int field)
        {
            //Return a record field value

            if (RecordCount() >= 0)
            {
                //Make new list obj
                List<string> items = new List<string>();
                //Get the record
                items = _records[record];
                //Return the item from the field index
                return items[field];
            }
            else
            {
                //Return null
                return null;
            }
        }

        public string GetFieldValue(int record, string field)
        {
            //Get field index
            int idx = field2index(field);
            //Check for vaild field
            if (idx == -1)
            {
                //Return null
                return null;
            }
            //Return records field value.
            return GetFieldValue(record, idx);
        }

        public void SetFieldValue(int record, int field, string value){
            //Set a records field value
            try
            {
                if (record >= 0 && record < RecordCount())
                {
                    //Get record
                    List<string> rec = _records[record];
                    //Update the record field value
                    rec[field] = value;
                    //Put the new record list back in the records list
                    _records[record] = rec;
                }
            }
            catch { }
        }

        public void SetFieldValue(int record, string field, string value)
        {
            //Get the fields index
            int idx = field2index(field);
            //Check for vaild index
            if (idx != -1)
            {
                //Set the records field value.
                SetFieldValue(record, idx, value);
            }
        }

        public bool Load(string Filename)
        {
            //Load a new database
            int iTyoe = 0;
            int ftype = 0;
            bool is_good = true;
            string sLine = string.Empty;
            List<string> items = new List<string>();

            Clean();

            //Check if file is found
            if (!File.Exists(Filename))
            {
                is_good = false;
            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Filename))
                    {
                        //Loop while not end of file.
                        while (!sr.EndOfStream)
                        {
                            //Read in single line
                            sLine = sr.ReadLine().Trim();
                            //Check line length
                            if (sLine.Length > 0)
                            {
                                if (sLine.Equals("[FIELDS]",StringComparison.OrdinalIgnoreCase))
                                {
                                    _fields = new List<string>();
                                    sLine = string.Empty;
                                    ftype = 1;
                                }

                                if (sLine.Equals("[/FIELDS]",StringComparison.OrdinalIgnoreCase))
                                {
                                    sLine = string.Empty;
                                    ftype = 2;
                                }
                                //Test for fields open tag
                                if (ftype == 1)
                                {
                                    if (sLine.Trim().Length > 0)
                                    {
                                        //Strip value from tag
                                        string s_field = get_tagVal("field", sLine, true);
                                        //If the string is not null add it to the fields list
                                        if (s_field != null)
                                        {
                                            //Add to fields.
                                            _fields.Add(s_field);
                                        }
                                    }
                                }

                                //Check for record start tag
                                if (sLine.Equals("[RECORD]",StringComparison.OrdinalIgnoreCase))
                                {
                                    //Set tyoe to 1
                                    iTyoe = 1;
                                    //Read in the next line
                                    sLine = string.Empty;
                                    //sLine = sr.ReadLine();
                                    //Set new list
                                    items = new List<string>();
                                }
                                //Check for record end tag
                                if (sLine.Equals("[/RECORD]",StringComparison.OrdinalIgnoreCase))
                                {
                                    //set type to 2
                                    iTyoe = 2;
                                    //Read in the next line
                                    sLine = string.Empty;
                                }
                                //Check for start tag
                                if (iTyoe == 1)
                                {
                                    //Trim the string
                                    sLine = sLine.Trim();
                                    string s_rec = get_tagVal("value", sLine);

                                    if (s_rec != null)
                                    {
                                        //Add to items list
                                        items.Add(s_rec);
                                    }
                                }
                                //Check for end tag
                                if (iTyoe == 2)
                                {
                                    //Add the new record with the items list
                                    _records.Add(items);
                                }
                            }
                        }
                        //Close file.
                        sr.Close();
                    }
                }
                catch
                {
                    //Error
                    is_good = false;
                }
            }
            //Return load result
            return is_good;
        }
    }
}
