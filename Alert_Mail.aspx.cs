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
    public partial class Alert_Mail : System.Web.UI.Page
    {
        static String oldPSC, oldMail, oldNT;
        String PS_CODE, Email;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmail();
            if (!Page.IsPostBack)
            {
                pslistSelect();
                addButton.Visible = true;
                editButton.Visible = false;
            }
            
            
        }
        protected void getEmail()
        {
            String q = "SELECT am.PS_CODE,ps.PS_DESC,am.PS_SUPPT_EMAIL,am.NT_ACC FROM A_NEW_ALRET_MAIL am LEFT JOIN A_NEW_PROCESS_STEP ps ON am.PS_CODE=ps.PS_CODE ";

            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                alermail.DataSource = ds;
                alermail.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                alermail.DataSource = ds;
                alermail.DataBind();
                int columncount = alermail.Rows[0].Cells.Count;
                alermail.Rows[0].Cells.Clear();
                alermail.Rows[0].Cells.Add(new TableCell());
                alermail.Rows[0].Cells[0].ColumnSpan = columncount;
                alermail.Rows[0].Cells[0].Text = "No Records Found";
            }

        }

        protected void alermail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            alermail.PageIndex = e.NewPageIndex;
            getEmail();
            pslistSelect();
            emailIN.Text = String.Empty;
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

        protected void addButton_Click(object sender, EventArgs e)
        {
            this.PS_CODE = selectPS.SelectedItem.Value;

            this.Email = emailIN.Text;
            String ntacc = ntaccIn.Text;

            String q = "INSERT INTO A_NEW_ALRET_MAIL(PS_CODE,PS_SUPPT_EMAIL,NT_ACC) VALUES ('" + PS_CODE + "','" + Email + "','"+ntacc+"')";

            String mes = con.querytoDB(q);
            getEmail();
            selectPS.ClearSelection();
            emailIN.Text = String.Empty;
            ntaccIn.Text = String.Empty;
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

        protected void alermail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                addButton.Visible = false;
                editButton.Visible = true;
                ntaccIn.Enabled = false;
                int index = Convert.ToInt32(e.CommandArgument);
                emailIN.Text = alermail.DataKeys[index].Values["PS_SUPPT_EMAIL"].ToString();
                selectPS.SelectedValue = alermail.DataKeys[index].Values["PS_CODE"].ToString();
                oldPSC = alermail.DataKeys[index].Values["PS_CODE"].ToString();
                oldMail = alermail.DataKeys[index].Values["PS_SUPPT_EMAIL"].ToString();
                ntaccIn.Text = alermail.DataKeys[index].Values["NT_ACC"].ToString();
                oldNT = alermail.DataKeys[index].Values["NT_ACC"].ToString(); 
            }
        }

        protected void edit_Click(object sender, EventArgs e)
        {

            this.PS_CODE = selectPS.SelectedValue;
            String ntacc = ntaccIn.Text;
            this.Email = emailIN.Text;

            String q = "UPDATE A_NEW_ALRET_MAIL SET PS_CODE='" + PS_CODE + "',PS_SUPPT_EMAIL='"+Email+"', NT_ACC='"+ntacc+"' WHERE PS_CODE='" + oldPSC+"'AND PS_SUPPT_EMAIL='"+oldMail+"'and NT_ACC='"+oldNT+"'"  ;

            String mes = con.querytoDB(q);
            getEmail();
            selectPS.ClearSelection();
            emailIN.Text = String.Empty;
            addButton.Visible = true;
            editButton.Visible = false;
            ntaccIn.Enabled = true;
            ntaccIn.Text = String.Empty;
            this.MessageBox(mes);
        }
    }
}