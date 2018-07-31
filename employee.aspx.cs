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
    public partial class employee : System.Web.UI.Page
    {
        static String oldbadge,oldNT,oldmail;
        String badge_no,nt_acc,auth,mail;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmploy();
            if (!IsPostBack)
            {
                authSelect();
                psSelect();
                addButton.Visible = true;
                editButton.Visible = false;
            }


            
        }
        protected void getEmploy()
        {
            String q = "SELECT em.badge_no,em.nt_acc,au.authorize_desc,em.authorize_level,em.E_MAIL from a_new_employee em,a_new_authorize au where em.authorize_level = au.authorize_level";


            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                employData.DataSource = ds;
                employData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                employData.DataSource = ds;
                employData.DataBind();
                int columncount = employData.Rows[0].Cells.Count;
                employData.Rows[0].Cells.Clear();
                employData.Rows[0].Cells.Add(new TableCell());
                employData.Rows[0].Cells[0].ColumnSpan = columncount;
                employData.Rows[0].Cells[0].Text = "No Records Found";
            }

        }

        protected void employData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            employData.PageIndex = e.NewPageIndex;
       
         
            addButton.Visible = true;
            editButton.Visible = false;
        }

        protected void authSelect()
        {
            String q = "SELECT authorize_level,authorize_desc from a_new_authorize ";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            authSe.DataSource = ds.Tables[0];
            authSe.DataMember = ds.Tables[0].TableName;
            authSe.DataTextField = ds.Tables[0].Columns["authorize_desc"].ColumnName;
            authSe.DataValueField = ds.Tables[0].Columns["authorize_level"].ColumnName;
            authSe.DataBind();

        }


        protected void psSelect()
        {
            String q = "SELECT PS_CODE,PS_DESC FROM A_NEW_PROCESS_STEP";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            psSel.DataSource = ds.Tables[0];
            psSel.DataMember = ds.Tables[0].TableName;
            psSel.DataTextField = ds.Tables[0].Columns["PS_DESC"].ColumnName;
            psSel.DataValueField = ds.Tables[0].Columns["PS_CODE"].ColumnName;
            psSel.DataBind();

        }


        protected void addButton_Click(object sender, EventArgs e)
        {
            badge_no = bNoin.Text;
            nt_acc = ntIn.Text;
            auth = authSe.SelectedValue;
            mail = emailIn.Text;

            String q = "INSERT INTO A_NEW_EMPLOYEE(BADGE_NO,NT_ACC,authorize_level,e_mail) VALUES ('" + badge_no + "','" + nt_acc + "'," + auth + ",'"+mail+"')";

            String mes = con.querytoDB(q);

            for (int i = 0; i < psSeltd.Items.Count; i++)
            {
                q = "INSERT INTO A_NEW_ALRET_MAIL(ps_code,PS_SUPPT_EMAIL,nt_acc) VALUES ('" +psSeltd.Items[i].ToString() + "','" + mail + "','"+nt_acc+ "')";

                 mes = con.querytoDB(q);
            }

           
            getEmploy();
            authSe.ClearSelection();
            psSel.ClearSelection();
            psSeltd.Items.Clear();
            addButton.Visible = true;
            bNoin.Text = String.Empty;
            ntIn.Text = String.Empty;
            emailIn.Text = String.Empty;
            editButton.Visible = false;
            this.MessageBox(mes);
        }

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        protected void employData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                addButton.Visible = false;
                editButton.Visible = true;
                process.Visible = false;
                bNoin.Enabled = false;
                ntIn.Enabled = false;
                int index = Convert.ToInt32(e.CommandArgument);
                bNoin.Text = employData.DataKeys[index].Values["BADGE_NO"].ToString();
                ntIn.Text = employData.DataKeys[index].Values["NT_ACC"].ToString();
                authSe.SelectedValue = employData.DataKeys[index].Values["AUTHORIZE_LEVEL"].ToString();
                emailIn.Text = employData.DataKeys[index].Values["E_MAIL"].ToString();
                oldbadge = employData.DataKeys[index].Values["BADGE_NO"].ToString();
                oldNT = employData.DataKeys[index].Values["NT_ACC"].ToString();
                oldmail = employData.DataKeys[index].Values["E_MAIL"].ToString();
            }
        }

        protected void edit_Click(object sender, EventArgs e)
        {

            badge_no = bNoin.Text;
            nt_acc = ntIn.Text;
            auth = authSe.SelectedValue;
            mail = emailIn.Text;

            String q = "UPDATE A_NEW_EMPLOYEE SET BADGE_NO='" + badge_no + "',NT_ACC='" + nt_acc +"' ,authorize_level = "+auth + ",e_mail='"+mail+"' where badge_no = '"+oldbadge+"'";

            String mes = con.querytoDB(q);


            if (!mail.Equals(oldmail) || !nt_acc.Equals(oldNT))
            {
                q = "update a_new_alret_mail set PS_SUPPT_EMAIL='" + mail + "' where NT_ACC = '" + oldNT + "' and PS_SUPPT_EMAIL='" + oldmail + "'";


                mes = con.querytoDB(q);
            }
            getEmploy();
            authSe.ClearSelection();
            psSel.ClearSelection();
            psSeltd.Items.Clear();
            bNoin.Text = String.Empty;
            ntIn.Text = String.Empty;
            emailIn.Text = String.Empty;
            addButton.Visible = true;
            editButton.Visible = false;
            process.Visible = true;
            this.MessageBox(mes);
        }

        protected void psSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            psSeltd.Items.Add(psSel.SelectedValue);
        }

        

        protected void resetPS_Click(object sender, EventArgs e)
        {
            psSeltd.Items.Clear();
            psSel.ClearSelection();
        }

        protected void dataShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            employData.PageIndex = e.NewPageIndex;
            getEmploy();
        }
    }
}