using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

namespace Toollife
{
    public partial class SettingMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateDisplay.Text = DateTime.Now.ToString("MMMM dd , yyyy HH:MM");
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (NavigationMenu.SelectedValue.Equals("Normal Status"))
            {
                Session["Neviga"] = "Normal Status";
            }
            else if (NavigationMenu.SelectedValue.Equals("Extends Status"))
            {
                Session["Neviga"] = "Extends Status";
            }
            else if (NavigationMenu.SelectedValue.Equals("Adjust Count"))
            {
                Session["Neviga"] = "Adjust Count";
            }

            Response.Redirect("~/Report.aspx");
      
        }
    }
}