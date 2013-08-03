using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.UserControls;
namespace DataVisualization.Templates
{
    public partial class Template_Two : ParentTemplate
    {

        public override void setTemplateFileName()
        {
            templateFileName = "Template_Two";
        }

        public override void initializeArray()
        {
            dsuc = new DashboardSectionUserControl[2];
            dsuc[0] = dsucLeft;
            dsuc[1] = dsucRight;

        }

        public override void assignWidthHeight()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].width = 300;
                dsuc[i].height = 600;
            }
        }

        protected void DerivedSaveButton_Click(object sender, EventArgs e)
        {
            this.SaveButton_Click(sender, e);
        }
    }
}