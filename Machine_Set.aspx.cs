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
    public partial class Machine_Set : System.Web.UI.Page
    {
        String M_ID, PS_CODE, PKG_GROUP_SK, MODEL, M_STATUS;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getMacINFO();
            if (!Page.IsPostBack)
            {
                pslistSelect();
                macStsSelect();
                pkgGpSelect();
            }
            addButton.Visible = true;
            editButton.Visible = false;
            macNO.ReadOnly = false;
        }
        protected void getMacINFO()
        {
            String q = "SELECT MAC.M_ID,MAC.PS_CODE,P.PS_DESC,MAC.PKG_GROUP_SK,PK.PKG_GROUP_DESC,MAC.MODEL,MAC.M_STATUS,MS.M_DESC FROM A_NEW_MACHINE MAC,A_NEW_PROCESS_STEP P,A_NEW_PKG_GROUP_DESC PK,A_NEW_MACHINE_STATUS_CODE MS WHERE MAC.PS_CODE=P.PS_CODE AND MAC.PKG_GROUP_SK = PK.PKG_GROUP_SK AND MAC.M_STATUS = MS.M_STATUS ORDER BY M_ID ASC";
            DataSet ds = con.getData(q);
            if(ds.Tables[0].Rows.Count > 0){
            macData.DataSource = ds;
            macData.DataBind();
            }else {  
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());  
            macData.DataSource = ds;  
            macData.DataBind();  
            int columncount = macData.Rows[0].Cells.Count;  
            macData.Rows[0].Cells.Clear();  
            macData.Rows[0].Cells.Add(new TableCell());  
            macData.Rows[0].Cells[0].ColumnSpan = columncount;  
            macData.Rows[0].Cells[0].Text = "No Records Found";  
            } 
        }
        protected void pslistSelect()
        {
            String q = "SELECT PS_CODE,PS_DESC FROM A_NEW_PROCESS_STEP";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
           selectPS.DataSource = ds.Tables[0];
           selectPS.DataMember = ds.Tables[0].TableName;
           selectPS.DataTextField = ds.Tables[0].Columns["PS_DESC"].ColumnName;
           selectPS.DataValueField = ds.Tables[0].Columns["PS_CODE"].ColumnName;
            selectPS.DataBind();
 
        }


        protected void macStsSelect()
        {
            String q = "SELECT M_STATUS, M_DESC FROM A_NEW_MACHINE_STATUS_CODE";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
           macSts.DataSource = ds;
           macSts.DataMember = ds.Tables[0].TableName;
           macSts.DataTextField = ds.Tables[0].Columns["M_DESC"].ColumnName;
           macSts.DataValueField = ds.Tables[0].Columns["M_STATUS"].ColumnName;
            macSts.DataBind();

        }

        protected void pkgGpSelect()
        {
            String q = "SELECT PKG_GROUP_SK,PKG_GROUP_DESC FROM A_NEW_PKG_GROUP_DESC";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            pkgGp.DataSource = ds.Tables[0];
            pkgGp.DataMember = ds.Tables[0].TableName;
            pkgGp.DataTextField = ds.Tables[0].Columns["PKG_GROUP_DESC"].ColumnName;
            pkgGp.DataValueField = ds.Tables[0].Columns["PKG_GROUP_SK"].ColumnName;
            pkgGp.DataBind();

        }

        protected void macData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            macData.PageIndex = e.NewPageIndex;
            getMacINFO();
            pslistSelect();
            macStsSelect();
            pkgGpSelect();
            macNO.Text = String.Empty;
            mod.Text = String.Empty;
            addButton.Visible = true;
            editButton.Visible = false;

        }

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            
            this.M_ID = macNO.Text;
            this.PS_CODE = selectPS.SelectedItem.Value;
            this.PKG_GROUP_SK = pkgGp.SelectedItem.Value;
            this.MODEL = mod.Text;
            this.M_STATUS = macSts.SelectedItem.Value;

            String q = "INSERT INTO A_NEW_MACHINE(M_ID, PS_CODE, PKG_GROUP_SK,MODEL, M_STATUS) VALUES ('" + M_ID + "','" + PS_CODE + "'," + PKG_GROUP_SK + ",'" + MODEL + "','" + M_STATUS + "')";
 
            String mes = con.querytoDB(q);
            getMacINFO();
            macNO.Text = String.Empty;
            mod.Text = String.Empty;
            
            this.MessageBox(mes);
        }
        protected void macData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                addButton.Visible = false;
                editButton.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow selectedRow = macData.Rows[index];
                macNO.Text = macData.DataKeys[index].Values["M_ID"].ToString();
                macNO.ReadOnly = true;
                selectPS.SelectedValue = macData.DataKeys[index].Values["PS_CODE"].ToString();
               // selectPS.Enabled = false;
                pkgGp.SelectedValue = macData.DataKeys[index].Values["PKG_GROUP_SK"].ToString();
                mod.Text = macData.DataKeys[index].Values["MODEL"].ToString();

                macSts.SelectedValue = macData.DataKeys[index].Values["M_STATUS"].ToString();

            }
        }
        protected void edit_Click(object sender, EventArgs e)
        {
            String pss;
            this.M_ID = macNO.Text;
            this.PS_CODE = selectPS.SelectedItem.Value;
            this.PKG_GROUP_SK = pkgGp.SelectedItem.Value;
            this.MODEL = mod.Text;
            this.M_STATUS = macSts.SelectedItem.Value;
    

            String q = "UPDATE  A_NEW_MACHINE SET PS_CODE='"+PS_CODE+"',PKG_GROUP_SK=" + PKG_GROUP_SK + ",MODEL='" + MODEL + "',M_STATUS='" + M_STATUS + "'WHERE M_ID='" + M_ID + "'";

            String mes = con.querytoDB(q);
           getMacINFO();
           macNO.Text = String.Empty;
           mod.Text = String.Empty;
           pslistSelect();
           macStsSelect();
           pkgGpSelect();
            this.MessageBox(mes);
        }

       
      
    }
}