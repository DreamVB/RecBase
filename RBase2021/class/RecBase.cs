/*
 * RecBase Beta 1
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

namespace RecBase
{
    class RBaseTable
    {
        private static List<List<string>> _records = new List<List<string>>();
        private static List<string> _fields = new List<string>();

        private static int field2index(string f)
        {
            //Locate the index of a field name by checking it's string value.
            
            //Default field index value
            int idx = -1;
            
            for (int x = 0; x < FieldCount; x++)
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

        public static void Clean(){
            _records = new List<List<string>>();
            _fields = new List<string>();
        }

        public static void AddRecord(List<string>values){
            //Add new record
            _records.Add(values);
        }

        public static void AddFields(List<string>fields){
            //Add field names
            _fields = fields;
        }

        public static void DeleteField(int index)
        {
            List<string> rec = new List<string>();
            //Check for vaild index
            if (index >= 0 && index < FieldCount)
            {
                //Loop tho the records
                for (int x = 0; x < RecordCount; x++)
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

        public static void DeleteField(string f)
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

        public static bool FieldExists(string f)
        {
            return field2index(f) != -1;
        }
        
        public static string Field(int index)
        {
            if (index >= 0 && index < FieldCount)
            {
                return _fields[index];
            }
            return null;
        }

        public static void Field(int index, string value){
            if (index >= 0)
            {
                _fields[index] = value;
            }
        }

        public static void Field(string name, string value)
        {
            int idx = field2index(name);

            if (idx != -1)
            {
                _fields[idx] = value;
            }
        }

        public static int FieldCount
        {
            //Return field count
            get
            {
                return _fields.Count;
            }
        }

        public static int RecordCount
        {
            //Return record count
            get
            {
                return _records.Count;
            }
        }

        public static bool Save(string Filename)
        {
            string s_field = string.Empty;
            bool is_good = true;

            try
            {
                using (StreamWriter sw = new StreamWriter(Filename))
                {
                    //Save fields top line
                    foreach (string f in _fields)
                    {
                        s_field += f + ",";
                    }
                    
                    //Trim last field seperator
                    sw.Write(s_field.TrimEnd(','));
                    //Add blank line
                    sw.WriteLine();
                    sw.WriteLine();
                    //Write records and field values
                    foreach (List<string> r in _records)
                    {
                        //Write record start
                        sw.WriteLine("[rec]");
                        //Write field values
                        foreach (string f in r)
                        {
                            //Write record fieldvalue
                            sw.WriteLine(f);
                        }
                        //Write record start
                        sw.WriteLine("[/rec]");
                        //Add blank line
                        sw.WriteLine();
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

        public static List<string> GetRecord(int record)
        {
            //Returns a records values
            List<string> rec = new List<string>();
            //Check range
            if (record >= 0 && record < RecordCount)
            {
                //Get record
                rec = _records[record];
            }
            //Return record list
            return rec;
        }

        public static List<string> GetFields()
        {
            //Return field names.
            return _fields;
        }

        public static void DeleteRecord(int record)
        {
            //Delete a record
            if (record >= 0 && record < RecordCount)
            {
                //Remove from records list
                _records.RemoveAt(record);
            }
        }

        public static string GetFieldValue(int record, int field)
        {
            //Return a record field value

            if (RecordCount >= 0)
            {
                //Make new list obj
                List<string> items = new List<string>();
                //Get the record
                items = _records[0];
                //Return the item from the field index
                return items[field];
            }
            else
            {
                //Return null
                return null;
            }
        }

        public static string GetFieldValue(int record, string field)
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

        public static void SetFieldValue(int record, int field, string value){
            //Set a records field value
            try
            {
                if (record >= 0 && record < RecordCount)
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

        public static void SetFieldValue(int record, string field, string value)
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

        public static bool Load(string Filename)
        {
            //Load a new database
            int iTyoe = 0;
            bool is_good = true;
            bool iFirst = false;
            string sLine = string.Empty;
            List<string> items = new List<string>();

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
                                //Check if we have a first line
                                if (!iFirst)
                                {
                                    //Create fields list
                                    _fields = new List<string>();
                                    //Split the sLine string into the list
                                    _fields.AddRange(sLine.Split(','));
                                    //Set first line true
                                    iFirst = true;
                                }
                                else
                                {
                                    //Check for record start tag
                                    if (sLine.ToUpper().Equals("[REC]"))
                                    {
                                        //Set tyoe to 1
                                        iTyoe = 1;
                                        //Read in the next line
                                        sLine = sr.ReadLine();
                                        //Set new list
                                        items = new List<string>();
                                    }
                                    //Check for record end tag
                                    if (sLine.ToUpper().Equals("[/REC]"))
                                    {
                                        //set type to 2
                                        iTyoe = 2;
                                        //Read in the next line
                                        sLine = sr.ReadLine();
                                    }
                                    //Check for start tag
                                    if (iTyoe == 1)
                                    {
                                        //Trim the string
                                        sLine = sLine.Trim();
                                        //Add to items list
                                        items.Add(sLine);
                                    }
                                    //Check for end tag
                                    if (iTyoe == 2)
                                    {
                                        //Add the new record with the items list
                                        _records.Add(items);

                                    }
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
