using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Toollife
{
    public partial class Current_Mac : System.Web.UI.Page
    {
        DBA con = new DBA();
        string AutoRefresh = System.Configuration.ConfigurationManager.AppSettings["pagerefresh"];
        //static String area;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Area();
            }
            this.Response.AppendHeader("Refresh", this.AutoRefresh);
        }

        protected void Area()
        {
            String q = "select * from a_new_process_step";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            marea.DataSource = ds.Tables[0];
            marea.DataMember = ds.Tables[0].TableName;
            marea.DataTextField = ds.Tables[0].Columns["PS_DESC"].ColumnName;
            marea.DataValueField = ds.Tables[0].Columns["PS_CODE"].ColumnName;
            marea.DataBind();

        }

        protected void marea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string q = "select cur.*,b.pkg_code from v_current cur , a_new_machine mac , a_new_pkg_code b , a_new_pkg_group_desc c  where mac.m_id = CUR.M_ID and MAC.PS_CODE = '" + marea.SelectedValue + "' and cur.PKG_GROUP_DESC = C.PKG_GROUP_DESC and B.PKG_GROUP_SK = C.PKG_GROUP_SK";
            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                partData.DataSource = ds;
                partData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                partData.DataSource = ds;
                partData.DataBind();
                int columncount = partData.Rows[0].Cells.Count;
                partData.Rows[0].Cells.Clear();
                partData.Rows[0].Cells.Add(new TableCell());
                partData.Rows[0].Cells[0].ColumnSpan = columncount;
                partData.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        /*protected void getMachineW()
        {
            string q = "select a.* , b.pkg_code from v_current a , a_new_pkg_code b , a_new_pkg_group_desc c where A.PKG_GROUP_DESC = C.PKG_GROUP_DESC and B.PKG_GROUP_SK = C.PKG_GROUP_SK";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                partData.DataSource = ds;
                partData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                partData.DataSource = ds;
                partData.DataBind();
                int columncount = partData.Rows[0].Cells.Count;
                partData.Rows[0].Cells.Clear();
                partData.Rows[0].Cells.Add(new TableCell());
                partData.Rows[0].Cells[0].ColumnSpan = columncount;
                partData.Rows[0].Cells[0].Text = "No Records Found";
            }
        }*/




        protected void partData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                int index = Convert.ToInt32(e.CommandArgument);

                string macid = partData.DataKeys[index].Values[0].ToString();
                string toold = partData.DataKeys[index].Values[1].ToString();
                string status = partData.DataKeys[index].Values[2].ToString();
                string toolsk = partData.DataKeys[index].Values[3].ToString();
                string pkgc = partData.DataKeys[index].Values[4].ToString();

                Session["M_ID"] = macid;
                Session["TOOL_ID"] = toold;
                Session["STATUS"] = status;
                Session["TOOL_SK"] = toolsk;
                Session["PKG_CODE"] = pkgc;

            if (e.CommandName == "More")
            {
                Response.Redirect("~/Part_Status.aspx");
            }
            else if (e.CommandName == "Adjust")
            {
                Session["Action"] = "Adjust Count";
                Response.Redirect("~/login.aspx");
                
            }

        }

        protected void partData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                int colIndex = getColId("STATUS");
                if (drv["STATUS"].Equals("Alert"))
                {
                    e.Row.Cells[colIndex].BackColor = Color.Yellow;
                }
                else if (drv["STATUS"].Equals("Max"))
                {
                    e.Row.Cells[colIndex].BackColor = Color.Red;
                }
                else if (drv["STATUS"].Equals("Extend"))
                {
                    e.Row.Cells[colIndex].BackColor = Color.Orange;
                }
                else if (drv["STATUS"].Equals("Normal"))
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
            //getMachineW();
        }
    }

}
