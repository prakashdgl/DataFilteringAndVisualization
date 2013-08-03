using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.DataVisualization.Charting;

namespace DataVisualization.CodeBehind.Chart.SortingChart
{
    public class MyComparer : IComparer


    {

        public int Compare(object a, object b)
        {
            DataPoint pointA = (DataPoint)a;
            DataPoint pointB = (DataPoint)b;

            // Compare axis labels first
            int result = pointA.AxisLabel.CompareTo(pointB.AxisLabel);

            // If axis labels are equal - compare Y values
            if (result == 0)
            {
                result = pointA.YValues[0].CompareTo(pointB.YValues[0]);
            }

            return result;
        }


    }
}
