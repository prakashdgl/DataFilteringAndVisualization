using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace manualCodes
{
    class FieldValueExtractor
    {
        int numberOfField;
        int numberOfSample;


        public void setNumberOfField(int numberOfField)
        {
            this.numberOfField = numberOfField;

        }
        public void setNumberOfSample(int numberOfSample)
        {
            this.numberOfSample = numberOfSample;
        }

        public string[] parseLine(string line)
        {
            string[] row = new string[numberOfField];
            int field = 0;
            string temp = "";
            bool evenDoubleQuote = true;
            int i = 0;

            while (i < line.Length)
            {
                if (line[i] != '"')
                {
                    for (; i < line.Length; i++)
                    {
                        if (line[i] != ',')
                            temp = temp + line[i];
                        else break;
                    }
                    i++;
                    row[field] = temp;
                    field++;
                    temp = "";

                }
                else
                {
                    //i++;
                    for (; i < line.Length; i++)
                    {
                        if (line[i] != '"')
                        {
                            temp = temp + line[i];
                        }
                        else
                        {
                            if (evenDoubleQuote == true) evenDoubleQuote = false;
                            else evenDoubleQuote = true;

                            if (evenDoubleQuote)
                            {
                                if (i + 1 < line.Length)
                                {
                                    if (line[i + 1] == '"')
                                    {
                                        temp = temp + '"';
                                    }
                                    else if (line[i + 1] == ',')
                                    {
                                        i += 2;
                                        break;
                                    }
                                }
                            }

                        }
                    }
                    row[field] = temp;
                    field++;
                    temp = "";
                }



            }


            return row;
        }

        public void checkFunction()
        {
            this.setNumberOfField(6);
            string li = "hello,\"how\",are,\"you\",\"wat \"\"\"\"about, yu\"";
            string[] mn = this.parseLine(li);
            for (int i = 0; i < mn.Length; i++)
            {
                Console.WriteLine(mn[i]);
            }
        }


    }
}
