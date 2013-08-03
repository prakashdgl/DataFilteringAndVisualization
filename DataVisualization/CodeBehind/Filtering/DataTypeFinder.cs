using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace manualCodes
{
    public static class Type
    {
        public const int BOOLEAN10 = 1;
        public const int NUMERIC = 2;
        public const int BOOLEANTF = 3;
        public const int DATE = 4;
        public const int GPS = 5;
        public const int TEXT = 6;
    }
    /// <summary>
    /// It gives the data type of given list of array of string
    /// It provides list of error row number
    /// It provides list of error column number of given row number
    /// </summary>
    class DataTypeFinder
    {
        // it contains the list of data provided to find data type of each column
        private List<string[]> data;
        // number of row and column in the provided data
        private int row, column;
        //least percentage required to say that a column is of a data type other than text(default data type)
        private float leastPercentage;
        //it contains the data type of each column in number
        private int[] dataTypeInNumber;
        //it contains the data type of each column in text value
        private string[] dataType;
        // it contains data type of each field of the provided list
        private int[][] totalDataType;

        /// <summary>
        /// it takes list of array of string and the default value of least percentage is 80
        /// it returns false if length of data is less than or equal to 1 otherwise true
        /// </summary>
        /// <param name="data"></param>
        /// <returns>false if length of data is less than or equal to 1 otherwise true</returns>
        public bool setData(List<string[]> data)
        {
            return this.setData(data, 80.0f);
        }
        /// <summary>
        /// It takes list of array of string and the value of least percentage should be provided and its value should be 0-100
        /// If the value is less than 0 then it becomes 0 and if the value is greater than 100 then it becomes 100
        /// It returns false if length of data is less than or equal to 1 otherwise true
        /// </summary>
        /// <param name="data"></param>
        /// <param name="leastPercentage"></param>
        /// <returns></returns>
        public bool setData(List<string[]> data, float leastPercentage)
        {
            this.data = data;
            if (this.data.Count <= 1) return false;

            if (leastPercentage < 0) this.leastPercentage = 0;
            else if (leastPercentage > 100) this.leastPercentage = 100;
            else this.leastPercentage = leastPercentage;
            //st = new int[this.data.Count];
            this.column = this.data[0].Length;
            this.row = this.data.Count - 1;//assuming that the first row of the list contains the field name
            this.dataTypeInNumber = new int[this.column];
            totalDataType = new int[row][];
            for (int i = 0; i < row; i++)
            {
                totalDataType[i] = new int[column];
            }
            return true;
        }
        /// <summary>
        /// After calling setData function, this function must be called before calling other function
        /// It finds the data type of each column
        /// </summary>
        public void operate()
        {
            for (int i = 0; i < this.column; i++)
            {
                findDataTypeOfEachFieldOfColumn(i);
                findDataTypeOfColumn(i);
            }
            this.setDataType();
        }
        /// <summary>
        /// It returns list of error row number
        /// </summary>
        /// <returns></returns>
        public List<int> getErrorRow()
        {
            List<int> errorRow = new List<int>();
            bool hasError;
            for (int i = 0; i < this.row; i++)
            {
                hasError = false;
                //check row for error
                for (int j = 0; j < this.column; j++)
                {
                    if (dataTypeInNumber[j] != totalDataType[i][j])
                    {
                        if (dataTypeInNumber[j] != Type.TEXT)
                            hasError = true;
                        break;
                    }
                }
                if (hasError == true) errorRow.Add(i + 1);
            }
            return errorRow;
        }
        /// <summary>
        /// It returns list of error column number of given row number
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public List<int> getErrorColumnInRow(int rowNumber)
        {
            List<int> errorColumnInRow = new List<int>();
            for (int i = 0; i < this.column; i++)
            {
                if (dataTypeInNumber[i] != totalDataType[rowNumber + 1][i])
                {
                    if (dataTypeInNumber[i] != Type.TEXT)
                        errorColumnInRow.Add(i);
                }
            }
            return errorColumnInRow;
        }

        private void findDataTypeOfEachFieldOfColumn(int columnNumber)
        {
            //string temp;
            for (int i = 0; i < row; i++)
            {
                //temp=this.data[i][columnNumber];
                totalDataType[i][columnNumber] = findDataTypeOfAField(this.data[i + 1][columnNumber]);//because the first row contains field name
            }
        }
        private int findDataTypeOfAField(string value)
        {
            if (isBoolean10(value)) return Type.BOOLEAN10;
            if (isNumber(value)) return Type.NUMERIC;
            if (isBooleanTF(value)) return Type.BOOLEANTF;
            if (isDate(value)) return Type.DATE;
            if (isGPS(value)) return Type.GPS;
            return Type.TEXT;
        }
        private void findDataTypeOfColumn(int columnNumber)
        {
            int[] frequencyOfEachType = new int[7];//7-1 is the number of type ;; index 0 is not used in it just for programming ease
            //int numberOfIteration = this.row - 1;
            //finding the frequency of each type in the column
            for (int i = 0; i < this.row; i++)
            {
                frequencyOfEachType[this.totalDataType[i][columnNumber]]++;
            }
            //deciding whether 1 and 0 is of boolean type or numeric type
            if (frequencyOfEachType[Type.NUMERIC] == 0 && frequencyOfEachType[Type.BOOLEANTF] == 0)
            {
                frequencyOfEachType[Type.BOOLEANTF] = frequencyOfEachType[Type.BOOLEAN10];
                changeXtoY(Type.BOOLEAN10, Type.BOOLEANTF, columnNumber);
            }
            else
            {
                frequencyOfEachType[Type.NUMERIC] += frequencyOfEachType[Type.BOOLEAN10];
                changeXtoY(Type.BOOLEAN10, Type.NUMERIC, columnNumber);
            }

            //decide the type of the column
            float percentageGot;
            this.dataTypeInNumber[columnNumber] = Type.TEXT;//this is the default data type (TEXT) of every columns
            //Index 0 is not used, 1 contains 1 or 0 which has already been decided as boolean or numeric and 
            //the last index 6 is the default type if 2,3,4,5 are not the data type of the column
            for (int i = 2; i <= 5; i++)
            {
                percentageGot = frequencyOfEachType[i] / (float)this.row * 100;
                if (percentageGot >= leastPercentage)
                {
                    this.dataTypeInNumber[columnNumber] = i;
                    break;
                }
            }
        }
        private void changeXtoY(int x, int y, int columnNumber)
        {
            for (int i = 0; i < this.row; i++)
            {
                if (totalDataType[i][columnNumber] == x) this.totalDataType[i][columnNumber] = y;
            }
        }
        private void setDataType()
        {
            this.dataType = new string[this.dataTypeInNumber.Length];
            for (int i = 0; i < this.dataTypeInNumber.Length; i++)
            {
                switch (this.dataTypeInNumber[i])
                {
                    case Type.NUMERIC:
                        dataType[i] = "numeric";
                        break;
                    case Type.BOOLEANTF:
                        dataType[i] = "boolean";
                        break;
                    case Type.DATE:
                        dataType[i] = "date";
                        break;
                    case Type.GPS:
                        dataType[i] = "GPS";
                        break;
                    case Type.TEXT:
                        dataType[i] = "text";
                        break;
                    default:
                        dataType[i] = "wrong value";
                        break;
                }
            }
        }
        /// <summary>
        /// It returns array of string of data type
        /// </summary>
        /// <returns></returns>
        private string[] getDataType()
        {
            return this.dataType;
        }
        public int[] getDataType2()
        {
            string[] ds = getDataType();
            int[] ans = new int[ds.Length];
            //System.IO.TextWriter t = new StreamWriter("c:\\Users\\power\\Desktop\\rajan.txt");
            for (int i = 0; i < ds.Length; i++)
            {
                if (ds[i] == "text")
                {
                    ans[i] = 2;
                }
                else if (ds[i] == "numeric")
                {
                    ans[i] = 1;
                }
                else if (ds[i] == "boolean")
                {
                    ans[i] = 0;
                }
                else if (ds[i] == "GPS")
                {
                    ans[i] = 4;
                }
                else if (ds[i] == "date")
                {
                    ans[i] = 3;
                }
                //t.WriteLine("datafinder  "+ans[i]);
            }
            System.IO.TextWriter t = new StreamWriter("c:\\Users\\power\\Desktop\\rajan.txt");
            for (int i = 0; i < ds.Length; i++) {
                t.WriteLine(ans[i]);
            }
                t.Close();
            return ans;
        }
        private bool isNumber(string value)
        {
            //number = real number
            string pattern = "^([+-]?\\d+([.]\\d)?\\d*)$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(value);
            //return true;
        }
        private bool isBooleanTF(string value)
        {
            //ture or false
            //string pattern ="^((?i)true|false)$";
            string pattern = "^([tT]rue|[fF]alse)$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(value);
        }
        private bool isBoolean10(string value)
        {
            //1 or 0
            string pattern = "^(1|0)$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(value);
        }
        private bool isDate(string value)
        {
            //mm-dd-yyyy
            //12-21-2012
            string pattern = "^(([0]?[1-9]|[1][0-2])-([0]?[1-9]|[12][0-9]|[3][0-2])-[0-9]{4})$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(value);
        }
        private bool isTime(string value)
        {
            //hh:mm:ss
            //22:56:31
            string pattern = "^(([01][0-9]|[2][0-3]):([0-5][0-9]):([0-5][0-9]))$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(value);
        }
        private bool isGPS(string value)
        {
            //N96 45.235
            string pattern = "^([E|W|N|S]([0-2]?[0-9]|[3][0-5])[0-9]( [0-5]?[0-9](.[0-9]+)?)?)$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(value);
        }

    }
}