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
    public partial class newform : System.Web.UI.Page
    {
        static String mid, toold, toolsk ,partno ,badge,action;
        DBA con = new DBA();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                pno.Text = Session["PART_NO"].ToString();
            }
        }


        protected void savebtn_Click(object sender, EventArgs e)
        {
            string comm = comment.Text;

            if (!string.IsNullOrEmpty(comm))
            {
                string addhis = "insert into a_new_action_history(quest_no,action,badge_no,occur_date,remarks,m_id,tool_sk,part_no) values(a_new_action_history_seq.nextval,'"+action+"','"+badge+"',sysdate,'"+comm+"','"+mid+"','"+toolsk+"','"+partno+"')";
                string his = con.querytoDB(addhis);

                string upmac = "update a_new_machines_status set shutdown_flag = 'N',current_status = '001' where m_id='" + mid + "' and tool_sk='" + toolsk + "' and shutdown_flag = 'Y'";
                String upnewm = con.querytoDB(upmac);

                string uppart = "update a_new_parts_status set life_time = '0', part_status = '001', mail_flag = 'N' where m_id='" + mid + "' and tool_sk='" + toolsk + "' and part_no='" + partno + "'";
                String upnewp = con.querytoDB(uppart);

                MessageBox("Save!");
                //comment.Text = String.Empty;
                comment.Enabled = false;
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