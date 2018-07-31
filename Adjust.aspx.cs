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
    public partial class Adjust : System.Web.UI.Page
    {
        static string mid,badge, action, pkgc,tlsk;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
                mid = Session["M_ID"].ToString();
                badge = Session["BADGE_NO"].ToString();
                action = Session["Action"].ToString();
                pkgc = Session["PKG_CODE"].ToString();

            if (!Page.IsPostBack)
            {
                macid.Text = Session["M_ID"].ToString();
                Tool();
            }
            
            
        }

            
        protected void Tool()
        {
             String q = "select tool_sk , tool_id from a_new_tool_set";
             DataSet ds = con.getData(q);
             ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
             tool.DataSource = ds.Tables[0];
             tool.DataMember = ds.Tables[0].TableName;
             tool.DataTextField = ds.Tables[0].Columns["TOOL_ID"].ColumnName;
             tool.DataValueField = ds.Tables[0].Columns["TOOL_SK"].ColumnName;
             tool.DataBind();

        }
        protected void tool_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            tid.Text = tool.SelectedItem.Text.ToString();

            tlsk = tool.SelectedValue;

            tid.Enabled = false;
            lifet.Enabled = true;
            comment.Enabled = true;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string toolid = tid.Text;
            int newlt = Int32.Parse(lifet.Text);
            string comm = comment.Text;

            if (newlt != null && comm != null)
            {
                string partno = "select part_no from a_new_parts_status where tool_sk = '"+tlsk+"' and m_id = '"+mid+"'";
                DataSet ds = con.getData(partno);
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string pno = Convert.ToString(row["PART_NO"]);
                        string q = "insert into a_new_action_history(quest_no,badge_no,occur_date,remarks,m_id,tool_sk,part_no,action) values(a_new_action_history_seq.nextval,'" + badge + "',sysdate,'" + comm + "','" + mid + "','" + tlsk + "','"+pno+"','" + action + "')";
                        string addhisa = con.querytoDB(q);
                    }
                }

                string qq = "insert into a_new_adjuts_cnt(m_id,tool_sk,adjust_cnt,badge_no,remarks,occur_date) values('" + mid + "','" + tlsk + "','" + newlt + "','" + badge + "','" + comm + "',sysdate)";
                string addadj = con.querytoDB(qq);

                if (newlt < 0 )
                {
                    newlt = -newlt;
                    string qqq = "insert into a_new_barcode_info(br_sk,pkg_code,m_id,unit,status,datetime) values(a_new_barcode_info_seq.nextval,'" + pkgc + "','" + mid + "','" + newlt + "','D',sysdate)";
                    string addbardown = con.querytoDB(qqq);
                }
                else
                {
                    string qqqq = "insert into a_new_barcode_info(br_sk,pkg_code,m_id,unit,status,datetime) values(a_new_barcode_info_seq.nextval,'" + pkgc + "','" + mid + "','" + newlt + "','A',sysdate)";
                    string addbarup = con.querytoDB(qqqq);
                }

                MessageBox("Save!");
                tid.Enabled = false;
                lifet.Enabled = false;
                comment.Enabled = false;
            }
            else
                MessageBox("Please Entry LIFE TIME Or Comment");

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