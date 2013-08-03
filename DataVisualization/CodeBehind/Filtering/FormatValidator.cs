using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace manualCodes
{
    class FormatValidator
    {
        private int numberOfColumns;
        private string[] rowField;

        public FormatValidator(int numberOfColumns)
        {
            this.numberOfColumns = numberOfColumns;
            rowField = new string[numberOfColumns];
        }

        public bool isLineFormatValid(string row)
        {
            bool evenDoubleQuote = true;
            int tempNumberOfColumns = 0;
            if (row.Length >= 0) { tempNumberOfColumns = 1; rowField[0] = ""; }
            else return false;
            for (int i = 0, j = 0; i < row.Length; i++)
            {
                if (row[i] == ',' && evenDoubleQuote == true) 
                { 
                    j++;
                    try
                    {
                        rowField[j] = "";
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    tempNumberOfColumns++; 
                }
                else
                {
                    rowField[j] += row[i];
                }
            }
            if (numberOfColumns == tempNumberOfColumns) return true;
            else return false;
        }

        public string[] getRowField()
        {
            return rowField;
        }
    }
}
