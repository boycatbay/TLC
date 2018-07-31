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
    public partial class Part_Set : System.Web.UI.Page
    {
        static String pkgsk;
        String PartNo, PartDesc, QTY, Alert, Max, pkgGp;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                getPkgG();
                pkgGpSelect();
            }
            addButton.Visible = true;
            editButton.Visible = false;
            partNoIN.ReadOnly = false;

        }

        protected void getPkgG()
        {
            string q = "select PKG_GROUP_DESC,PKG_GROUP_SK from a_new_pkg_group_desc";
            DataSet ds = con.getData(q);

            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            pkgg.DataSource = ds.Tables[0];
            pkgg.DataMember = ds.Tables[0].TableName;
            pkgg.DataTextField = ds.Tables[0].Columns["PKG_GROUP_DESC"].ColumnName;
            pkgg.DataValueField = ds.Tables[0].Columns["PKG_GROUP_SK"].ColumnName;
            pkgg.DataBind();

            
        }

        protected void pkgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            pkgsk = pkgg.SelectedValue;
            getPartSpec();
        }

        protected void getPartSpec()
        {
            string q = "select a.*,b.pkg_group_sk from a_new_part_spec a , a_new_pkg_code b where b.pkg_code = a.pkg_code and b.pkg_group_sk = '" +pkgsk+ "'"; 
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

        protected void partData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                addButton.Visible = false;
                editButton.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = partData.Rows[index];
                partNoIN.Text = selectedRow.Cells[0].Text;
                partNoIN.ReadOnly = true;
                descIN.Text = selectedRow.Cells[1].Text;
                qtyIN.Text = selectedRow.Cells[2].Text;
                alertIN.Text = selectedRow.Cells[3].Text;
                maxIN.Text = selectedRow.Cells[4].Text;
                selectPG.SelectedValue = partData.DataKeys[index].Values["PKG_GROUP_SK"].ToString();
                pkgCoSelect(partData.DataKeys[index].Values["PKG_GROUP_SK"].ToString());
                selectPC.SelectedValue = partData.DataKeys[index].Values["PKG_CODE"].ToString();
            }
        }

        protected void partData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            partData.PageIndex = e.NewPageIndex;
            getPartSpec();
            partNoIN.Text = String.Empty;
            descIN.Text = String.Empty;
            qtyIN.Text = String.Empty;
            alertIN.Text = String.Empty;
            maxIN.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            addButton.Visible = true;
            editButton.Visible = false;

        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            this.PartNo = partNoIN.Text;
            this.PartDesc = descIN.Text;
            this.QTY = qtyIN.Text;
            this.Alert = alertIN.Text;
            this.Max = maxIN.Text;

            String q = "INSERT INTO A_NEW_PART_SPEC(PART_NO,PART_DESC,QTY_USE,ALERT_COUNT,MAX_COUNT) VALUES ('"+PartNo+"','"+PartDesc+"',"+QTY+","+Alert+","+Max+")";

            String mes = con.querytoDB(q);
          
            partNoIN.Text = String.Empty;
            descIN.Text = String.Empty;
            qtyIN.Text = String.Empty;
            alertIN.Text = String.Empty;
            maxIN.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            this.MessageBox(mes);
        }

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            this.PartNo = partNoIN.Text;
            this.PartDesc = descIN.Text;
            this.QTY = qtyIN.Text;
            this.Alert = alertIN.Text;
            this.Max = maxIN.Text;

            String q = "UPDATE A_NEW_PART_SPEC SET PART_DESC='"+PartDesc+"',QTY_USE="+QTY+",ALERT_COUNT="+Alert+",MAX_COUNT="+Max+"WHERE PART_NO='"+PartNo+"'";
           
            String mes = con.querytoDB(q);
            partNoIN.Text = String.Empty;
            descIN.Text = String.Empty;
            qtyIN.Text = String.Empty;
            alertIN.Text = String.Empty;
            maxIN.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            this.MessageBox(mes);
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

        
    }
}