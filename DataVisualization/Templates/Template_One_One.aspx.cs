using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.UserControls;
namespace DataVisualization.Templates
{
    public partial class Template_One_One : ParentTemplate
    {

        public override void setTemplateFileName()
        {
            templateFileName = "Template_One_One";
        }

        public override void initializeArray()
        {
            dsuc = new DashboardSectionUserControl[2];
            dsuc[0] = dsucTop;
            dsuc[1] = dsucBottom;
            
        }

        public override void assignWidthHeight()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].width = 500;
                dsuc[i].height = 300;
            }
        }

        protected void DerivedSaveButton_Click(object sender, EventArgs e)
        {
            this.SaveButton_Click(sender, e);
        }
    }
}