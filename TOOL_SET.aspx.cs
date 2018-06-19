using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;

namespace Toollife
{
    public partial class TOOL_SET : System.Web.UI.Page
    {
        String TOOL_SK, TOOL_ID, PKG_CODE, STATUS, pkgGp;
        DBA con = new DBA();

        protected void Page_Load(object sender, EventArgs e)
        {
            getTSINFO();
            if (!IsPostBack)
            {
                pkgGpSelect();
            }
            add.Visible = true;
            edit.Visible = false;
            statusFF.Visible = false;
        }
        protected void getTSINFO()
        {
            String q = "SELECT TS.TOOL_SK,TS.TOOL_ID,PC.PKG_GROUP_SK,TS.PKG_CODE,TS.STATUS_FLAG FROM A_NEW_TOOL_SET TS LEFT JOIN A_NEW_PKG_CODE PC ON TS.PKG_CODE = PC.PKG_CODE ORDER BY TOOL_ID ASC";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tsData.DataSource = ds;
                tsData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                tsData.DataSource = ds;
                tsData.DataBind();
                int columncount = tsData.Rows[0].Cells.Count;
                tsData.Rows[0].Cells.Clear();
                tsData.Rows[0].Cells.Add(new TableCell());
                tsData.Rows[0].Cells[0].ColumnSpan = columncount;
                tsData.Rows[0].Cells[0].Text = "No Records Found";
            }

        }
        protected void tsData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tsData.PageIndex = e.NewPageIndex;
            getTSINFO();
            selectPG.ClearSelection();
            selectPC.Items.Clear();

        }
        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }
        protected void pkgGpSelect()
        {
            String q = "SELECT PKG_GROUP_SK,PKG_GROUP_DESC FROM A_NEW_PKG_GROUP_DESC";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            selectPG.DataSource = ds.Tables[0];
            selectPG.DataMember = ds.Tables[0].TableName;
            selectPG.DataTextField = ds.Tables[0].Columns["PKG_GROUP_DESC"].ColumnName;
            selectPG.DataValueField = ds.Tables[0].Columns["PKG_GROUP_SK"].ColumnName;
            selectPG.DataBind();

        }

        protected void selectPG_SelectedIndexChanged(object sender, EventArgs e)
        {

            pkgGp = selectPG.SelectedValue;
            pkgCoSelect(pkgGp);

        }
        protected void pkgCoSelect(String pkgGp)
        {
            String q = "SELECT PKG_GROUP_SK,PKG_CODE FROM A_NEW_PKG_CODE WHERE PKG_GROUP_SK=" + pkgGp + "ORDER BY PKG_CODE";
            DataSet ds = con.getData(q);
            selectPC.DataSource = ds.Tables[0];
            selectPC.DataMember = ds.Tables[0].TableName;
            selectPC.DataTextField = ds.Tables[0].Columns["PKG_CODE"].ColumnName;
            selectPC.DataValueField = ds.Tables[0].Columns["PKG_CODE"].ColumnName;
            selectPC.DataBind();

        }

        protected void add_Click(object sender, EventArgs e)
        {
            String q, mes;

            this.TOOL_ID = tIDIN.Text;
            this.PKG_CODE = selectPC.SelectedValue;
            this.STATUS = "Active";

            q = "INSERT INTO A_NEW_TOOL_SET(TOOL_ID,PKG_CODE,STATUS_FLAG) VALUES ('" + TOOL_ID + "','" + PKG_CODE + "','" + STATUS + "')";

            mes = con.querytoDB(q);
            getTSINFO();
            tIDIN.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            this.MessageBox(mes);
        }

        protected void tsData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                int index = Convert.ToInt32(e.CommandArgument);

                tIDIN.Text = tsData.DataKeys[index].Values["TOOL_ID"].ToString();
                selectPG.SelectedValue = tsData.DataKeys[index].Values["PKG_GROUP_SK"].ToString();
                pkgCoSelect(tsData.DataKeys[index].Values["PKG_GROUP_SK"].ToString());
                statusF.Text = tsData.DataKeys[index].Values["STATUS_FLAG"].ToString();
                selectPC.SelectedValue = tsData.DataKeys[index].Values["PKG_CODE"].ToString();
                toolskin.Text = tsData.DataKeys[index].Values["TOOL_SK"].ToString();    
                add.Visible = false;
                edit.Visible = true;
                statusFF.Visible = true;
              

            }



        }
        protected void edit_onclick(object sender, EventArgs e) 
        {
            String q,mes;
            this.TOOL_SK = toolskin.Text;
            this.TOOL_ID = tIDIN.Text;
            this.PKG_CODE = selectPC.SelectedValue;
            this.STATUS = statusF.Text;

            q = "UPDATE A_NEW_TOOL_SET SET TOOL_ID='" + TOOL_ID + "',PKG_CODE='" + PKG_CODE + "',STATUS_FLAG='" + STATUS + "' WHERE TOOL_SK = " + TOOL_SK + "";
            mes = con.querytoDB(q);
            getTSINFO();
            statusFF.Visible = false;
            tIDIN.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            this.MessageBox(mes);
        }
    }
}
