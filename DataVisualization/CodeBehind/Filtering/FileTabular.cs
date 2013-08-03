using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace manualCodes
{
    interface FileTabular
    {
        

        int getDataSize();//return number of rows in the data
        int getColumnCount();//number of fields
        string getFieldName(int columnIndex);
        string getData(int row, int column);
        int getType(int column);
        FileTable getSubData(int[] columns);
        void storeInTable(string userId,string tableName);
        //string getFilteringForm();
        string getHtmlTable(int count);
    }
}
