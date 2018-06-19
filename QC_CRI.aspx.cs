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
    public partial class QC_CRI : System.Web.UI.Page
    {
        String partno, qcno,qcdesc;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getQC();
            if (!Page.IsPostBack)
            {
                partnoSelect();
                addButton.Visible = true;
                editButton.Visible = false;
            }

        }
        protected void getQC()
        {
            String q = "SELECT regexp_replace(PART_NO,'*[[:space:]]','') AS PART_NO,QC_NO,QC_DESC FROM A_NEW_QC_CRT";

            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                qcdata.DataSource = ds;
                qcdata.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                qcdata.DataSource = ds;
                qcdata.DataBind();
                int columncount = qcdata.Rows[0].Cells.Count;
                qcdata.Rows[0].Cells.Clear();
                qcdata.Rows[0].Cells.Add(new TableCell());
                qcdata.Rows[0].Cells[0].ColumnSpan = columncount;
                qcdata.Rows[0].Cells[0].Text = "No Records Found";
            }

        }

        protected void qcdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            qcdata.PageIndex = e.NewPageIndex;
            getQC();
            selectPatNo.ClearSelection();
            
            addButton.Visible = true;
            editButton.Visible = false;
        }

        protected void partnoSelect()
        {
            String q = "SELECT regexp_replace(PART_NO,'*[[:space:]]','') AS PART_NO FROM A_NEW_PART_SPEC";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            selectPatNo.DataSource = ds.Tables[0];
            selectPatNo.DataMember = ds.Tables[0].TableName;
            selectPatNo.DataTextField = ds.Tables[0].Columns["PART_NO"].ColumnName;
            selectPatNo.DataValueField = ds.Tables[0].Columns["PART_NO"].ColumnName;
            selectPatNo.DataBind();

        }
        protected void qcnoSelect()
        {
            String partnoInto = selectPatNo.SelectedValue;
            String q = "SELECT QC_NO FROM A_NEW_QC_CRT WHERE regexp_replace(PART_NO,'*[[:space:]]','') ='"+partnoInto+"' ORDER BY QC_NO ASC";
            DataSet ds = con.getData(q);
            selectQC.DataSource = ds.Tables[0];
            selectQC.DataMember = ds.Tables[0].TableName;
            selectQC.DataTextField = ds.Tables[0].Columns["QC_NO"].ColumnName;
            selectQC.DataValueField = ds.Tables[0].Columns["QC_NO"].ColumnName;
            selectQC.DataBind();

        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            this.partno = selectPatNo.SelectedItem.Value;

            this.qcno = QCadd.Text;
            this.qcdesc = qcdescin.Text;


            String q = "INSERT INTO A_NEW_QC_CRT(PART_NO,QC_NO,QC_DESC) VALUES ('" + partno + "','" + qcno + "','"+qcdesc+"')";

            String mes = con.querytoDB(q);
            getQC();
            selectPatNo.ClearSelection();
            QCadd.Text = String.Empty;
            qcdescin.Text = String.Empty;
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

        protected void qcdata_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                addButton.Visible = false;
                editButton.Visible = true;
                selectPatNo.Enabled = false;
                QCadd.Visible = false;
                selectQC.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                selectPatNo.SelectedValue = qcdata.DataKeys[index].Values["PART_NO"].ToString();
                qcnoSelect();

            }
        }

      protected void edit_Click(object sender, EventArgs e)
        {

           

            this.qcno = selectQC.SelectedValue;
            this.qcdesc = qcdescin.Text;

            String q = "UPDATE A_NEW_QC_CRT SET QC_DESC='"+qcdesc+"' WHERE QC_NO='"+qcno+"'";

            String mes = con.querytoDB(q);
            getQC();
            selectPatNo.ClearSelection();
            qcdescin.Text = String.Empty;
            QCadd.Visible = true;
            selectQC.Visible = false;
            addButton.Visible = true;
            editButton.Visible = false;
            this.MessageBox(mes);
        }

        protected void selectQC_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.qcno = selectQC.SelectedValue;
            String desc;
            for(int i = 0;i<qcdata.Rows.Count;i++)
            {
                if (qcdata.DataKeys[i].Values["QC_NO"].ToString().Equals(qcno))
                {
                    desc = qcdata.DataKeys[i].Values["QC_DESC"].ToString();
                    qcdescin.Text = desc;
                    break;
                }
            }

        }
    }
}