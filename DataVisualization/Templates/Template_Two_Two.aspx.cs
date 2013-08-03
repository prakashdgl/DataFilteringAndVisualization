using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.UserControls;
namespace DataVisualization.Templates
{
    public partial class Template_Two_Two : ParentTemplate
    {

        public override void setTemplateFileName()
        {
            templateFileName = "Template_Two_Two";
        }

        public override void initializeArray()
        {
            dsuc = new DashboardSectionUserControl[4];
            dsuc[0] = dsucTopLeft;
            dsuc[1] = dsucTopRight;
            dsuc[2] = dsucBottomLeft;
            dsuc[3] = dsucBottomRight;
        }

        public override void assignWidthHeight()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].width = 200;
                dsuc[i].height = 200;
            }
        }

        protected void DerivedSaveButton_Click(object sender, EventArgs e)
        {
            this.SaveButton_Click(sender, e);
        }

    }
}