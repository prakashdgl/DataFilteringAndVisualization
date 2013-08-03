using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace manualCodes
{
    class Validator
    {
        private int numberOfColumns;
        private int numberOfRows;
        private bool isHeaderValid;
        private string filename;
        private StreamReader fileReader;
        private List<string[]> data;

        //for invalid data
        string errorType;
        int errorLine;

        public Validator(string filename)
        {
            this.filename = filename;
            fileReader = new StreamReader(this.filename);
            data = new List<string[]>();
        }

        public bool validate()
        {
            
            //checking validity of header
            string header = fileReader.ReadLine();
            this.isHeaderValid = true;
            if (!validHeader(header))
            {
                this.errorType = "HeaderInvalid";
                this.errorLine = 1;
                this.isHeaderValid = false; return false; 
            
            }
            
            Console.WriteLine("the number of columns is "+this.numberOfColumns);
            FormatValidator formatValidator= new FormatValidator(this.numberOfColumns);
            FieldValueExtractor fieldValue = new FieldValueExtractor();
            fieldValue.setNumberOfField(this.numberOfColumns);
            data.Add(fieldValue.parseLine(header));
            //checking validity of data field
            string temp;
            string[] tempRowField;
            while ((temp = fileReader.ReadLine()) != null)
            {
                if (formatValidator.isLineFormatValid(temp) == true)
                {
                    tempRowField = formatValidator.getRowField();
                    if (FieldValidator.isLineFieldValid(tempRowField) == true)
                    {
                        data.Add(fieldValue.parseLine(temp));
                        //return true;
                    }
                    else
                    {
                        errorType = "FieldInvalid";
                        errorLine = data.Count+1;
                        return false;
                    }
                }
                else
                {
                    errorType = "FormatInvalid";
                    errorLine = data.Count+1;
                    return false;
                }
            }
            return true;
        }

        public string getErrorType()
        {
            return errorType;
        }
        public int geterrorLineNumber()
        {
            return errorLine;
        }

        public bool getIsHeaderValid()
        {
            return this.isHeaderValid;
        }

        private bool validHeader(string header)
        {
            int numberOfColumns = 0;
            if (header.Length > 0) numberOfColumns = 1;
            this.isHeaderValid = false;
            for (int i = 0; i < header.Length; i++)
            {
                if (isAlphabetOrNumber(header[i])) { }
                else if (header[i] == ',') { numberOfColumns++; }
                else return false;
            }
            this.numberOfColumns = numberOfColumns;
            this.isHeaderValid = true;
            return true;
        }
        private bool isAlphabetOrNumber(char c)
        {
            if (c >= 'a' && c <= 'z') return true;
            if (c >= 'A' && c <= 'Z') return true;
            if (c >= '0' && c <= '9') return true;
            if (c == '_') return true;
            return false;
        }
        public List<string[]> getData() 
        {
            return this.data;
        }
    }
}
