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
    public partial class MCxTS : System.Web.UI.Page
    {
        static String mid, toolsk, q, mes;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getMxtInfo();
            
            if (!IsPostBack)
            {
                gettsData();
                pslistSelect();
                addButton.Visible = true;
                editButton.Visible = false;
                
            }
        }

        protected void getMxtInfo()
        {
            String q = "SELECT f.m_id,F.TOOL_SK,d.tool_id,M.PKG_GROUP_SK,PK.PKG_GROUP_DESC,M.PS_CODE,N.PS_DESC,f.QUAT_CNT,f.WEKLY_CNT FROM a_new_machines_status f LEFT JOIN a_new_tool_set d ON d.tool_sk = f.tool_sk JOIN a_new_machine m ON m.m_id = f.m_id JOIN a_new_pkg_group_desc pk ON PK.PKG_GROUP_SK = M.PKG_GROUP_SK join a_new_process_step n on N.PS_CODE = M.PS_CODE ORDER BY m_id asc";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                mxtsData.DataSource = ds;
                mxtsData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                mxtsData.DataSource = ds;
                mxtsData.DataBind();
                int columncount = mxtsData.Rows[0].Cells.Count;
                mxtsData.Rows[0].Cells.Clear();
                mxtsData.Rows[0].Cells.Add(new TableCell());
                mxtsData.Rows[0].Cells[0].ColumnSpan = columncount;
               mxtsData.Rows[0].Cells[0].Text = "No Records Found";
            }
 
        }
        protected void getMxtInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           mxtsData.PageIndex = e.NewPageIndex;
            getMxtInfo();
        }

        protected void gettsData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tsData.PageIndex = e.NewPageIndex;
            gettsData();
        }
        protected void gettsData()
        {

            String q = "select a.tool_sk,a.tool_id,a.pkg_code,c.pkg_group_desc from a_new_tool_set a,a_new_pkg_code b, a_new_pkg_group_desc c where a.pkg_code = b.pkg_code and b.pkg_group_sk = c.pkg_group_sk order by tool_sk asc";

            DataSet ds = con.getData(q);

            if (ds.Tables[0].Rows.Count > 0)
            {
                tsData.DataSource = ds;
                tsData.DataBind();
                tsData.Columns[0].Visible = false;
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

        protected void macSelect(String pscode)
        {
            
            String q = "SELECT m.PS_CODE,m.M_ID,M.M_ID || ' ----- '|| pg.PKG_GROUP_DESC AS Title FROM A_NEW_MACHINE m,A_NEW_PKG_GROUP_DESC pg WHERE m.PS_CODE='"+pscode+"' AND M.PKG_GROUP_SK = PG.PKG_GROUP_SK";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            selectMac.DataSource = ds.Tables[0];
            selectMac.DataMember = ds.Tables[0].TableName;
            selectMac.DataTextField = ds.Tables[0].Columns["Title"].ColumnName ;
            selectMac.DataValueField = ds.Tables[0].Columns["M_ID"].ColumnName;
            selectMac.DataBind();

        }

        protected void selectPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            String pscode = selectPS.SelectedValue;
            macSelect(pscode);
        }


        protected void tsSelected_click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("TOOL_SK", typeof(string)));
            dt.Columns.Add(new DataColumn("TOOL_ID", typeof(string)));
            foreach (GridViewRow row in tsData.Rows)
            {


                CheckBox chkRow = (row.Cells[4].FindControl("chkTS") as CheckBox);
                if (chkRow.Checked)
                {
                  

                    dt.Rows.Add(row.Cells[0].Text, row.Cells[1].Text);

                }
            }
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                selectPC.DataSource = ds.Tables[0];
                selectPC.DataMember = ds.Tables[0].TableName;
                selectPC.DataTextField = ds.Tables[0].Columns["TOOL_ID"].ColumnName;
                selectPC.DataValueField = ds.Tables[0].Columns["TOOL_SK"].ColumnName;
                selectPC.DataBind();

            }

        }

        protected void add_Click(object sender, EventArgs e)
        {
           
            mid = selectMac.SelectedValue;
            
            for (int i = 0; i < selectPC.Items.Count; i++)
            {
                toolsk = selectPC.Items[i].Value.ToString();
                q = "INSERT INTO A_NEW_MACHINES_STATUS(M_ID,TOOL_SK,CURRENT_STATUS) VALUES ('" + mid + "','" + toolsk + "','001')";

                mes = con.querytoDB(q);
                this.MessageBox(mes);
             
            }

         
            getMxtInfo();
           selectMac.ClearSelection();
           selectPS.ClearSelection();
           selectPC.Items.Clear();
            gettsData();
            

        }
        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        protected void mxtsData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                addButton.Visible = false;
                editButton.Visible = true;
              
                selectPS.SelectedValue = mxtsData.DataKeys[index].Values["PS_CODE"].ToString();
                macSelect(mxtsData.DataKeys[index].Values["PS_CODE"].ToString());
                selectMac.SelectedValue = mxtsData.DataKeys[index].Values["M_ID"].ToString();
                selectPS.Enabled = false;
                selectMac.Enabled = false;
                tsMulSelected(mxtsData.DataKeys[index].Values["TOOL_SK"].ToString());

            }

        }

        protected void tsMulSelected(String tsKey)
        {
            String q = "SELECT TOOL_SK,TOOL_ID  FROM A_NEW_TOOL_SET  WHERE TOOL_SK=" + tsKey + "  ORDER BY TOOL_ID ASC ";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                selectPC.DataSource = ds.Tables[0];
                selectPC.DataMember = ds.Tables[0].TableName;
                selectPC.DataTextField = ds.Tables[0].Columns["TOOL_ID"].ColumnName;
                selectPC.DataValueField = ds.Tables[0].Columns["TOOL_SK"].ColumnName;
                selectPC.DataBind();

                oldTS.DataSource = ds.Tables[0];
                oldTS.DataMember = ds.Tables[0].TableName;
                oldTS.DataTextField = ds.Tables[0].Columns["TOOL_ID"].ColumnName;
                oldTS.DataValueField = ds.Tables[0].Columns["TOOL_SK"].ColumnName;
                oldTS.DataBind();

                for (int i = 0; i < selectPC.Items.Count; i++)
                {
                    foreach (GridViewRow row in tsData.Rows)
                    {
                        if (row.Cells[1].Text == selectPC.Items[i].ToString())
                        {
                            CheckBox chkRow = (row.Cells[4].FindControl("chkTS") as CheckBox);
                            chkRow.Checked = true;
                            break;
                        }
                    }
                }
                
            }
        
        }

        protected void edit_click(object sender, EventArgs e)
        {
          
            

            mid = selectMac.SelectedValue;

            

            for (int i = 0; i < selectPC.Items.Count; i++)
            {
                for (int k = 0; k < oldTS.Items.Count; k++)
                {
                    if (selectPC.Items[i].Value.ToString() != selectPC.Items[k].Value.ToString())
                    {
                        toolsk = selectPC.Items[i].Value.ToString();
                        
                    }
                }
                q = "INSERT INTO A_NEW_MACHINES_STATUS(M_ID,TOOL_SK,CURRENT_STATUS) VALUES ('" + mid + "','" + toolsk + "',)";

                mes = con.querytoDB(q);
                

            }
            this.MessageBox(mes);

            getMxtInfo();
            pslistSelect();
            gettsData();
            addButton.Visible = true;
            editButton.Visible = false;
            selectPS.Enabled = true;
            selectMac.Enabled = true;

        }
 
        



        
    }
}