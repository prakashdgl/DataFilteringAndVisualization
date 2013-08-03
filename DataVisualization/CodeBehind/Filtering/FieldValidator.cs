using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace manualCodes
{
    class FieldValidator
    {
        public static bool isLineFieldValid(string[] rowField)
        {

            for (int i = 0; i < rowField.Length; i++)
            {
                if (rowField[i].Length != 0)
                {
                    if (rowField[i][0] == '"')
                    {
                        if (rowField[i][rowField.Length - 1] != '"') return false;
                        for (int j = 1; j < rowField[i].Length - 1; j++)
                        {
                            if (rowField[i][j] == '"')
                            {
                                if (rowField[i][j + 1] != '"') return false;
                                else
                                {
                                    j++;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < rowField[i].Length; j++)
                        {
                            if (rowField[i][j] == '"' || rowField[i][j] == ',') return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
