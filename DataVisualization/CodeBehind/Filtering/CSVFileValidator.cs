using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Collections.Generic;

namespace manualcodes
{
    class CSVFileValidator
    {
        private string errorMessage = "";
        private List<String[]> data;
        private String[] parseLine(String line)
        {
            List<string> l=new List<String>();
            String temp = "";
            char c;
            bool doublequote = false;

            for (int i = 0; i < line.Count(); i++)
            {
                c = line.ElementAt(i);
                if (c == ',' && !doublequote)
                {
                    //l.Add(temp.Trim()==""?" ":temp.Trim());
                    l.Add(temp.Trim());
                    temp = "";
                    continue;
                }
                else
                {
                    if (c == '"')
                    {
                        if (doublequote)
                        {
                            temp += c;
                            doublequote = false;
                        }
                        else
                        {
                            temp += c;
                            doublequote = true;
                        }
                    }
                    else
                    {
                        temp += c;
                    }
                }

            }
            //if (temp != "")
            //{
            //    l.Add(temp.Trim());
            //}
            l.Add(temp.Trim());
            return l.ToArray();
        }
        private List<String[]> parseFile(String path)
        {
            List<String[]> data=new List<String[]>();
            try
            {
                using(StreamReader input=new StreamReader(path))
                {
                    String line;
                    String[] row;
                    String[] temp;
                    while ((line = input.ReadLine()) != null && line.Trim()!="" )
                    {//&& line.Trim()!=""
                        row = this.parseLine(line);
                        data.Add(row);
                    }//while
                }//using
            }//try
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
            }
            return data;
        }
        /// <summary>
        /// simple csv files are only valid, ie,
        /// number of fields in each lines should be same,
        /// header field should not contain any spaces
        /// there should not be empty lines in the file or the program will stop there
        /// one field may have spaces or comma but should be enclosed with double quotes
        /// the file should contain at least two rows
        /// fields should have some name
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Boolean isValid(String Path)
        {
            List<String[]> data = this.parseFile(Path);
            if (data == null||data.Count<2)
            {
                errorMessage = "Either the Data is null or it contains less then two lines of data in format";
                return false;
            }
            int count = data.ElementAt(0).Count();
            //Console.WriteLine("The number of fields is "+count+" \nsize of data is "+data.Count());
            foreach(String[] element in data)
            {
                //Console.WriteLine(element.Count());
                if (count != element.Count())
                {
                    errorMessage = "number of data in each field is not equal to " + count+" on line "+data.IndexOf(element);
                    Console.WriteLine(errorMessage);
                    return false;
                }
            }
            if (data.Count() < 2)
            {
                errorMessage = "the number of rows is less then 2 so returning false";
                Console.WriteLine(errorMessage);
                return false;
            }
            foreach(String field in data.ElementAt(0))
            {
                int j=field.Split(' ').Count();
                if(j!=1){
                    errorMessage = "The first line should be field name and its value cannot have spaces";
                   // Console.WriteLine(errorMessage);
                    return false;
                }
                char[] illegalChars={'!','@','#','$','%','"','\''};
                //List<char> ill = illegalChars.ToList();
                foreach(char ill in illegalChars )
                {
                    if(field.Contains(ill))
                    {
                        errorMessage = "The Header field (First line of the document) contains illegal characters...";
                        //Console.WriteLine(errorMessage);
                        return false;
                    }
                }
            }
            this.data = data;
            return true;
        }
        public List<String[]> getData()
        {
            return this.data;
        }

        private String[] parseLine2(String line)
        {
            List<string> l = new List<String>();
            String temp = "";
            char c;
            bool doublequote = false;

            for (int i = 0; i < line.Count(); i++)
            {
                c = line.ElementAt(i);
                if (c == ',' && !doublequote)
                {
                    l.Add(temp.Trim());
                    temp = "";
                    continue;
                }
                else
                {
                    if (c == '"')
                    {
                        if (doublequote)
                        {
                            temp += c;
                            doublequote = false;
                        }
                        else
                        {
                            temp += c;
                            doublequote = true;
                        }
                    }
                    else
                    {
                        temp += c;
                    }
                }

            }
            if (temp != "")
            {
                l.Add(temp.Trim());
            }
            return l.ToArray();
        }

        public string getErrorMessage()
        {
            return this.errorMessage;
        }
    }
}
