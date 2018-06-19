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
        String PartNo, PartDesc, QTY, Alert, Max;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getPartSpec();
            addButton.Visible = true;
            editButton.Visible = false;
            partNoIN.ReadOnly = false;
        }
        protected void getPartSpec()
        {
            String q = "SELECT PART_NO,PART_DESC,QTY_USE,ALERT_COUNT,MAX_COUNT FROM A_NEW_PART_SPEC ORDER BY PART_NO ASC";
            
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

        protected void partData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            partData.PageIndex = e.NewPageIndex;
            getPartSpec();
            partNoIN.Text = String.Empty;
            descIN.Text = String.Empty;
            qtyIN.Text = String.Empty;
            alertIN.Text = String.Empty;
            maxIN.Text = String.Empty;
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
            getPartSpec();
            partNoIN.Text = String.Empty;
            descIN.Text = String.Empty;
            qtyIN.Text = String.Empty;
            alertIN.Text = String.Empty;
            maxIN.Text = String.Empty;
            this.MessageBox(mes);
        }

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
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
                
            }
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
            getPartSpec();
            this.MessageBox(mes);
        }
 
        
    }
}