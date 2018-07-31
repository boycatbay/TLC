using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

namespace Toollife
{
    public partial class extentform : System.Web.UI.Page
    {
        static String mid, toold, toolsk, partno, badge, action,QC_NO,QC_FLAG,QC_ACTION;
        DBA con = new DBA();

        protected void Page_Load(object sender, EventArgs e)
        {
            getQcNO();
            if (!Page.IsCallback || !Page.IsPostBack)
             {
                 mid = Session["M_ID"].ToString();
                 toold = Session["TOOL_ID"].ToString();
                 toolsk = Session["TOOL_SK"].ToString();
                 partno = Session["PART_NO"].ToString();
                 action = Session["Action"].ToString();
                 badge = Session["BADGE_NO"].ToString();

                 macid.Text = Session["M_ID"].ToString();
                 toolid.Text = Session["TOOL_ID"].ToString();
                 part.Text = Session["PART_NO"].ToString();

             }
        }
        protected void getQcNO()
        {
            //if (partno != null)
            
                //string qq = "insert into a_new_qc_crt(part_no,qc_no,qc_desc)";
                string q = "select qc_no from a_new_qc_crt where part_no ='" + partno + "'";
                DataSet ds = con.getData(q);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ExtendF.DataSource = ds;
                    ExtendF.DataBind();
                }
                else if (ds.Tables[0].Rows.Count <= 0)
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    ExtendF.DataSource = ds;
                    ExtendF.DataBind();
                    int columncount = ExtendF.Rows[0].Cells.Count;
                    ExtendF.Rows[0].Cells.Clear();
                    ExtendF.Rows[0].Cells.Add(new TableCell());
                    ExtendF.Rows[0].Cells[0].ColumnSpan = columncount;
                    ExtendF.Rows[0].Cells[0].Text = "No Records Found";

                }
            
        }

        protected void ExtendF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ExtendF.PageIndex = e.NewPageIndex;
            getQcNO();
            qcno.Text = String.Empty;
            qcaction.Text = String.Empty;
        }

        protected void ExtendF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                qcno.Enabled = false;
                qcflag.Enabled = true;
                qcaction.Enabled = true;
                int index = Convert.ToInt32(e.CommandArgument);
                qcno.Text = ExtendF.DataKeys[index].Values["QC_NO"].ToString();
            }
        }

        protected void savebtn_Click(object sender, EventArgs e)
        {
            QC_NO = qcno.Text;
            QC_FLAG = qcflag.SelectedValue;
            QC_ACTION = qcaction.Text;

            if (!string.IsNullOrEmpty(QC_ACTION))
            {
                string addhis = "insert into a_new_action_history(quest_no,action,badge_no,occur_date,remarks,m_id,tool_sk,part_no) values(a_new_action_history_seq.nextval,'" + action + "','" + badge + "',sysdate,'" + QC_ACTION + "','" + mid + "','" + toolsk + "','" + partno + "') ";
                string addhisx = con.querytoDB(addhis);

                string qno = "select quest_no from a_new_action_history where occur_date = (select max (occur_date) from a_new_action_history)";
                DataSet ds = con.getData(qno);
                foreach (DataRow b in ds.Tables[0].Rows)
                {
                    Session["QUEST_NO"] = b["QUEST_NO"].ToString();
                    break;
                }
                string getqno = Session["QUEST_NO"].ToString();

                string ex = "insert into a_new_extented(quest_id,qc_no,qc_flag,qc_action) values('" + getqno + "','" + QC_NO + "','" + QC_FLAG + "','" + QC_ACTION + "')";
                string addx = con.querytoDB(ex);

                string uppart = "update a_new_parts_status set part_status = '004' where m_id='"+ mid +"' and tool_sk='"+toolsk+"' and part_no='"+partno+"'";
                String upexp = con.querytoDB(uppart);

                string upcur = "update a_new_machines_status set CURRENT_STATUS = '004' where m_id='" + mid + "' and tool_sk='" + toolsk + "'";
                String upexm = con.querytoDB(upcur);

                MessageBox("Save!");
                getQcNO();
                qcno.Text = String.Empty;
                qcaction.Text = String.Empty;
                qcaction.Enabled = false;
                Response.Redirect("~/Part_Status.aspx");

            }
            else
                MessageBox("Please entry comment");
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            backButton.PostBackUrl = "~/Part_Status.aspx";
        }

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }
    }
}