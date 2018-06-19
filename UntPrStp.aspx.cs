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
    public partial class UntPrStp : System.Web.UI.Page
    {
        String frameNo, frameNoOld, deviceNo, pkgCo, upsI;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getUPS();
            if (!IsPostBack)
            {
                pkgGpSelect();
                addButton.Visible = true;
                editButton.Visible = false;
            }
            
        }
        protected void getUPS()
        {
            String q = "SELECT ups.FRAME_NO,ups.DEVICE_NO,ups.PKG_CODE,pc.PKG_GROUP_SK,ups.UNIT_PER_STRP FROM A_NEW_UNIT_PER_STRIP ups LEFT JOIN A_NEW_PKG_CODE pc ON ups.PKG_CODE = pc.PKG_CODE ORDER BY ups.FRAME_NO ASC"; 

            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                upsData.DataSource = ds;
                upsData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                upsData.DataSource = ds;
                upsData.DataBind();
                int columncount = upsData.Rows[0].Cells.Count;
                upsData.Rows[0].Cells.Clear();
                upsData.Rows[0].Cells.Add(new TableCell());
                upsData.Rows[0].Cells[0].ColumnSpan = columncount;
                upsData.Rows[0].Cells[0].Text = "No Records Found";
            }

        }
        protected void pkgGpSelect()
        {
            String q = "SELECT PKG_GROUP_SK,PKG_GROUP_DESC FROM A_NEW_PKG_GROUP_DESC ORDER BY PKG_GROUP_DESC ASC";
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

            String pkgGp = selectPG.SelectedValue;
            pkgCoSelect(pkgGp);

        }
        protected void pkgCoSelect(String pkgGp)
        {
            String q = "SELECT PKG_CODE FROM A_NEW_PKG_CODE WHERE PKG_GROUP_SK=" + pkgGp + "ORDER BY PKG_CODE";
            DataSet ds = con.getData(q);
            selectPC.DataSource = ds.Tables[0];
            selectPC.DataMember = ds.Tables[0].TableName;
            selectPC.DataTextField = ds.Tables[0].Columns["PKG_CODE"].ColumnName;
            selectPC.DataValueField = ds.Tables[0].Columns["PKG_CODE"].ColumnName;
            selectPC.DataBind();

        }

        protected void upsData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            upsData.PageIndex = e.NewPageIndex;
            getUPS();
            frameIn.Text = String.Empty;
            deviceIn.Text = String.Empty;
            unitIn.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            addButton.Visible = true;
            editButton.Visible = false;
        }



        protected void add_Click(object sender, EventArgs e)
        {
            String q, mes;

            this.frameNo = frameIn.Text;
            this.deviceNo = deviceIn.Text;
            this.pkgCo = selectPC.SelectedValue;
            this.upsI = unitIn.Text;

            q = "INSERT INTO A_NEW_UNIT_PER_STRIP(FRAME_NO,DEVICE_NO,PKG_CODE,UNIT_PER_STRP) VALUES ('" + frameNo+ "','" + deviceNo + "','" + pkgCo + "',"+upsI+")";

            mes = con.querytoDB(q);
            getUPS();
            frameIn.Text = String.Empty;
            deviceIn.Text = String.Empty;
            unitIn.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            addButton.Visible = true;
            editButton.Visible = false;
            this.MessageBox(mes);
        }

        protected void upsData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                int index = Convert.ToInt32(e.CommandArgument);

                frameIn.Text = upsData.DataKeys[index].Values["FRAME_NO"].ToString();
                selectPG.SelectedValue = upsData.DataKeys[index].Values["PKG_GROUP_SK"].ToString();
                pkgCoSelect(upsData.DataKeys[index].Values["PKG_GROUP_SK"].ToString());
                deviceIn.Text = upsData.DataKeys[index].Values["DEVICE_NO"].ToString();
                selectPC.SelectedValue = upsData.DataKeys[index].Values["PKG_CODE"].ToString();
                unitIn.Text = upsData.DataKeys[index].Values["UNIT_PER_STRP"].ToString();
                addButton.Visible = false;
                editButton.Visible = true;
                TextBox1.Text = upsData.DataKeys[index].Values["FRAME_NO"].ToString();


            }



        }
        protected void edit_onclick(object sender, EventArgs e)
        {
            String q, mes;

            this.frameNo = frameIn.Text;
            this.deviceNo = deviceIn.Text;
            this.pkgCo = selectPC.SelectedValue;
            this.upsI = unitIn.Text;
            this.frameNoOld = TextBox1.Text;

            q = "UPDATE A_NEW_UNIT_PER_STRIP SET FRAME_NO='" + frameNo + "',DEVICE_NO='" + deviceNo + "',PKG_CODE='" + pkgCo + "',UNIT_PER_STRP="+upsI+" WHERE FRAME_NO = " + frameNoOld + "";
            mes = con.querytoDB(q);
            getUPS();
            frameIn.Text = String.Empty;
            deviceIn.Text = String.Empty;
            unitIn.Text = String.Empty;
            selectPG.ClearSelection();
            selectPC.Items.Clear();
            addButton.Visible = true;
            editButton.Visible = false;
            this.MessageBox(mes);
        }

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        
    }
}