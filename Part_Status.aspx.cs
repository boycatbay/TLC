using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Drawing;

namespace Toollife
{
    public partial class Part_Status : System.Web.UI.Page
    {
        static String mid, toold, ss, toolsk;
        
        DBA con = new DBA();
       


        protected void Page_Load(object sender, EventArgs e)
        {
             mid = Session["M_ID"].ToString();
             toold = Session["TOOL_ID"].ToString();
             ss = Session["STATUS"].ToString();
             toolsk = Session["TOOL_SK"].ToString();

             macid.Text = Session["M_ID"].ToString();
             toolid.Text = Session["TOOL_ID"].ToString();
            
            getPartStatus();
        }             
        protected  void getPartStatus()
        {
            
            String q = "SELECT a.M_ID,a.PART_NO,a.TOOL_SK,a.part_status,b.PART_DESC,b.QTY_USE,a.LIFE_TIME,b.ALERT_COUNT,b.MAX_COUNT FROM A_NEW_PARTS_STATUS a, A_NEW_PART_SPEC b where b.PART_NO = a.PART_NO and a.TOOL_SK ='"+ toolsk + "'and a.M_ID = '" + mid + "'";
            DataSet ds = con.getData(q);         
            partData.DataSource = ds;
            partData.DataBind();
        }
        protected void partData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string macid = partData.DataKeys[index].Values[0].ToString();
            string toolsk = partData.DataKeys[index].Values[1].ToString();
            string partno = partData.DataKeys[index].Values[2].ToString();
            
            Session["M_ID"] = macid;
            Session["TOOL_SK"] = toolsk;
            Session["PART_NO"] = partno;
            

            if (e.CommandName == "New")
            {
                Session["Action"] = "New";

            }
            else if (e.CommandName == "Extend")
            {
                Session["Action"] = "Extend";
            }
            Response.Redirect("~/login.aspx");
        }

      protected void partData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
               DataRowView drv = e.Row.DataItem as DataRowView;
                int colIndex = getColId("STATUS");
                

                if (drv["PART_STATUS"].Equals("002"))
                {
                    e.Row.Cells[colIndex].BackColor = Color.Yellow;
                }
                else if (drv["PART_STATUS"].Equals("003"))
                {
                     e.Row.Cells[colIndex].BackColor = Color.Red;
                }
                else if (drv["PART_STATUS"].Equals("004"))
                {
                    e.Row.Cells[colIndex].BackColor = Color.Orange;
                }
                else if (drv["PART_STATUS"].Equals("001"))
                {
                    e.Row.Cells[colIndex].BackColor = Color.Green;
                }
                
            }


        }

        private int getColId(string colName)
        {
            int index = -1;
            foreach (DataControlField col in partData.Columns)
            {
                if (col.HeaderText == colName)
                {
                    index = partData.Columns.IndexOf(col);
                    break;
                }
            }
            return index;
        }

        protected void partData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            partData.PageIndex = e.NewPageIndex;
            getPartStatus();
        }
    }
        
}
