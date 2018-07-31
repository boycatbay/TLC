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
    public partial class Tool_Set_Part : System.Web.UI.Page
    {
        DBA con = new DBA();
        String TOOL_SK, TOOL_ID, PKG_CODE;


        protected void Page_Load(object sender, EventArgs e)
        {
            getTSINFO();

            if (!IsPostBack)
            {
               
                add.Visible = true;
                edit.Visible = false;

            }
            

        }

        protected void getTSINFO()
        {
            String q = "SELECT TS.TOOL_SK,TS.TOOL_ID,TS.PKG_CODE FROM A_NEW_TOOL_SET TS ORDER BY TOOL_ID ASC";
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
            tIDIN.Text = String.Empty;
            pkgCoIN.Text = String.Empty;
            selectPC.Items.Clear();
            
            add.Visible = true;
            edit.Visible = false;
            


        }

        protected void getPartNO()
        {

            String q = "SELECT PKG_CODE,PART_NO,PART_DESC FROM A_NEW_PART_SPEC where pkg_code = '"+pkgCoIN.Text+"'order by part_no";

            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                partNO.DataSource = ds;
                partNO.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                partNO.DataSource = ds;
                partNO.DataBind();
                int columncount = partNO.Rows[0].Cells.Count;
                partNO.Rows[0].Cells.Clear();
                partNO.Rows[0].Cells.Add(new TableCell());
                partNO.Rows[0].Cells[0].ColumnSpan = columncount;
                partNO.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        protected void partNO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            partNO.PageIndex = e.NewPageIndex;
            getPartNO();


        }
        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        protected void tsData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                tIDIN.Text = tsData.DataKeys[index].Values["TOOL_ID"].ToString();
                Toolsk.Text = tsData.DataKeys[index].Values["TOOL_SK"].ToString();
                pkgCoIN.Text = tsData.DataKeys[index].Values["PKG_CODE"].ToString();

                getPartNO();
                partNOSelected(Toolsk.Text);
            }

        }
        protected void partNOSelected(String tsKey)
        {
            String q = "SELECT PART_NO  FROM A_NEW_TOOL_PART  WHERE TOOL_SK='" + tsKey +"'";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                selectPC.DataSource = ds.Tables[0];
                selectPC.DataMember = ds.Tables[0].TableName;
                selectPC.DataTextField = ds.Tables[0].Columns["PART_NO"].ColumnName;
                selectPC.DataValueField = ds.Tables[0].Columns["PART_NO"].ColumnName;
                selectPC.DataBind();
               
                for (int i = 0; i < selectPC.Items.Count; i++)
                {
                    foreach (GridViewRow row in partNO.Rows)
                    {
                        if (row.Cells[0].Text == selectPC.Items[i].ToString())
                        {
                            CheckBox chkRow = (row.Cells[2].FindControl("chkPart") as CheckBox);
                            chkRow.Checked = true;
                            break;
                        }
                    }
                }
                add.Visible = false;
                edit.Visible = true;
            }
            else
            {
                selectPC.Items.Clear();
                foreach (GridViewRow row in partNO.Rows)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkPart") as CheckBox);
                    if (chkRow.Checked)
                    {
                        chkRow.Checked = false;
                    }
                }
                add.Visible = true;
                edit.Visible = false;
            }
        }



        protected void chkPartSelected_click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("PART_NO", typeof(string)));
            foreach (GridViewRow row in partNO.Rows)
            {


                CheckBox chkRow = (row.Cells[2].FindControl("chkPart") as CheckBox);
                if (chkRow.Checked)
                {
                    DataRow dr = dt.NewRow();
                    dr["PART_NO"] = row.Cells[0].Text;
                    dt.Rows.Add(dr);

                }
            }
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                selectPC.DataSource = ds.Tables[0];
                selectPC.DataMember = ds.Tables[0].TableName;
                selectPC.DataTextField = ds.Tables[0].Columns["PART_NO"].ColumnName;
                selectPC.DataValueField = ds.Tables[0].Columns["PART_NO"].ColumnName;
                selectPC.DataBind();

            }
            
        }

        protected void add_Click(object sender, EventArgs e)
        {
            String q, mes, partno;
            int suc = 0;

            this.TOOL_SK = Toolsk.Text;
            for (int i = 0; i < selectPC.Items.Count; i++)
            {
                partno = selectPC.Items[i].ToString();
                q = "INSERT INTO A_NEW_TOOL_PART(TOOL_SK,PART_NO) VALUES (" + TOOL_SK + ",'" + partno + "')";

                mes = con.querytoDB(q);
                suc++;
            }

            if (suc == selectPC.Items.Count)
            {
                mes = "Successful Running With " + suc + " result(s)";
            }
            else
            {
                mes = "An Error Occured";
            }
            getTSINFO();
            tIDIN.Text = String.Empty;
            pkgCoIN.Text = String.Empty;
            selectPC.Items.Clear();
            this.MessageBox(mes);

        }

        protected void edit_Click(object sender, EventArgs e)
        {
            String q, mes, partno;
            int suc = 0;
            
            this.TOOL_SK = Toolsk.Text;

            q = "DELETE FROM A_NEW_TOOL_PART WHERE TOOL_SK =" + TOOL_SK+"";

            mes = con.querytoDB(q);
            for (int i = 0; i < selectPC.Items.Count; i++)
            {
                partno = selectPC.Items[i].ToString();
                q = "INSERT INTO A_NEW_TOOL_PART(TOOL_SK,PART_NO) VALUES (" + TOOL_SK + ",'" + partno + "')";

                mes = con.querytoDB(q);
                suc++;
            }

            if (suc == selectPC.Items.Count)
            {
                mes = "Successful Running With " + suc + " result(s)";
            }
            else
            {
                mes = "An Error Occured";
            }
            getTSINFO();
            tIDIN.Text = String.Empty;
            pkgCoIN.Text = String.Empty;
            selectPC.Items.Clear();
            add.Visible = true;
            edit.Visible = false;
            this.MessageBox(mes);

        }

    }
}