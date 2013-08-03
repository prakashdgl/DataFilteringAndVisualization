using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.UserControls;
namespace DataVisualization.Templates
{
    public partial class Template_Three_One : ParentTemplate
    {

        public override void setTemplateFileName()
        {
            templateFileName = "Template_Three_One";
        }

        public override void initializeArray()
        {
            dsuc = new DashboardSectionUserControl[4];
            dsuc[0] = dsucTopLeft;
            dsuc[1] = dsucTopMiddle;
            dsuc[2] = dsucTopRight;
            dsuc[3] = dsucBottom;
        }

        public override void assignWidthHeight()
        {
            for (int i = 0; i < dsuc.Length-1; i++)
            {
                dsuc[i].width = 150;
                dsuc[i].height = 300;
            }
            dsuc[3].width = 500;
            dsuc[3].height = 400;
        }

        protected void DerivedSaveButton_Click(object sender, EventArgs e)
        {
            this.SaveButton_Click(sender, e);
        }
    }
}