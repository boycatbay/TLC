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
    public partial class conversion : System.Web.UI.Page
    {
        String ps, pkgc, unitst, unitu, unita, unith,multis;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getUniCon();
            if (!IsPostBack)
            {
                pslistSelect();
                pkgGpSelect();
                addButton.Visible = true;
                editButton.Visible = false;
            }

        }


        protected void getUniCon()
        {
            String q = "SELECT a.*,b.ps_desc,c.pkg_group_sk FROM A_NEW_UNITS_CONVS a,a_new_process_step b,a_new_pkg_code c where b.ps_code = a.ps_code and c.pkg_code = a.pkg_code ";

            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                unicorn.DataSource = ds;
                unicorn.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                unicorn.DataSource = ds;
                unicorn.DataBind();
                int columncount = unicorn.Rows[0].Cells.Count;
                unicorn.Rows[0].Cells.Clear();
                unicorn.Rows[0].Cells.Add(new TableCell());
                unicorn.Rows[0].Cells[0].ColumnSpan = columncount;
                unicorn.Rows[0].Cells[0].Text = "No Records Found";
            }

        }


        protected void unicorn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            unicorn.PageIndex = e.NewPageIndex;
            getUniCon();
            pslistSelect();
        
            addButton.Visible = true;
            editButton.Visible = false;
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


            pkgCoSelect(selectPG.SelectedValue);

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


        protected void addButton_Click(object sender, EventArgs e)
        {
            ps = selectPS.SelectedValue;
            pkgc = selectPC.SelectedValue;
            unitst = unipesi.Text;
            unitu = unipetu.Text;
            unita = unipeta.Text;
            unith = unipehi.Text;
        

            multis = multi.Text;
           
            String q = "INSERT INTO a_new_units_convs(PS_CODE, PKG_CODE, UNIT_STRIP,UNIT_TUBE, UNIT_TRAY, UNIT_HIT,  MULTIPLIER) values  ('"+ps+"','"+pkgc+"',"+unitst+","+unitu+","+unita+","+unith+","+multis+")";


            String mes = con.querytoDB(q);
            getUniCon();
            selectPS.ClearSelection();
            selectPG.ClearSelection();
            selectPC.Items.Clear();
          
             unipesi.Text = String.Empty;
             unipetu.Text = String.Empty;
             unipeta.Text = String.Empty;
             unipehi.Text = String.Empty;
             multi.Text = String.Empty;

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

        protected void unicorn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                addButton.Visible = false;
                editButton.Visible = true;
                selectPS.Enabled = false;
                selectPG.Enabled = false;
                selectPC.Enabled = false;

                int index = Convert.ToInt32(e.CommandArgument);
                selectPS.SelectedValue = unicorn.DataKeys[index].Values["PS_CODE"].ToString();
                selectPG.SelectedValue = unicorn.DataKeys[index].Values["PKG_GROUP_SK"].ToString();
                pkgCoSelect(unicorn.DataKeys[index].Values["PKG_GROUP_SK"].ToString());
                selectPC.SelectedValue = unicorn.DataKeys[index].Values["PKG_CODE"].ToString();
                multi.Text = unicorn.DataKeys[index].Values["MULTIPLIER"].ToString();

                unipesi.Text = unicorn.DataKeys[index].Values["UNIT_STRIP"].ToString();
                unipetu.Text = unicorn.DataKeys[index].Values["UNIT_TUBE"].ToString();
                unipeta.Text = unicorn.DataKeys[index].Values["UNIT_TRAY"].ToString();
                unipehi.Text = unicorn.DataKeys[index].Values["UNIT_HIT"].ToString();

            }
        }

        protected void edit_Click(object sender, EventArgs e)
        {

            ps = selectPS.SelectedValue;
            pkgc = selectPC.SelectedValue;
            unitst= unipesi.Text;
            unitu=unipetu.Text;
            unita=unipeta.Text;
            unith = unipehi.Text;
            

            multis = multi.Text;

            String q = "UPDATE a_new_units_convs SET unit_strip=" + unitst + ",UNIT_TUBE  =" + unitu + ",UNIT_TRAY  =" + unita + ",UNIT_HIT   = " + unith + ",MULTIPLIER =" + multis + " WHERE  PS_CODE    = '" + ps + "' AND    PKG_CODE   = '" + pkgc + "'";
            String mes = con.querytoDB(q);
            getUniCon();
            selectPS.ClearSelection();
            selectPG.ClearSelection();
            selectPC.Items.Clear();
           
            unipesi.Text = String.Empty;
            unipetu.Text = String.Empty;
            unipeta.Text = String.Empty;
            unipehi.Text = String.Empty;
            multi.Text = String.Empty;

            addButton.Visible = true;
            editButton.Visible = false;
            this.MessageBox(mes);
        }

       
    }
}