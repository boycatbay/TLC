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
    public partial class PKG_GUP_CDE : System.Web.UI.Page
    {
        String PKG_CODE, PKG_GROUP_SK, PKG_GROUP_DESC, PKG_GROUP_DESC_OLD;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            getpkgINFO();
            if (MultiView1.ActiveViewIndex.Equals(-1))
                adEdBut.Visible = false;
            
        }
        protected void getpkgINFO()
        {
            String q = "SELECT PC.PKG_GROUP_SK,PG.PKG_GROUP_DESC,PC.PKG_CODE FROM A_NEW_PKG_GROUP_DESC PG,A_NEW_PKG_CODE PC WHERE PC.PKG_GROUP_SK = PG.PKG_GROUP_SK ORDER BY PG.PKG_GROUP_DESC ASC";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pkgData.DataSource = ds;
                pkgData.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                pkgData.DataSource = ds;
                pkgData.DataBind();
                int columncount = pkgData.Rows[0].Cells.Count;
                pkgData.Rows[0].Cells.Clear();
                pkgData.Rows[0].Cells.Add(new TableCell());
                pkgData.Rows[0].Cells[0].ColumnSpan = columncount;
                pkgData.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        protected void pkgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pkgData.PageIndex = e.NewPageIndex;
            getpkgINFO();
            selectAct.SelectedIndex = -1;
            MultiView1.ActiveViewIndex = -1;

        }
        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
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
     
        
        protected void selectAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectAct.SelectedValue.Equals("0"))
            {
                MultiView1.ActiveViewIndex = 1;
                adEdBut.Visible = true;
                add.Visible = true;
                edit.Visible = false;

            }
            else
            {
                MultiView1.ActiveViewIndex = 0;
                pkgGpSelect();
                adEdBut.Visible = true;
                add.Visible = true;
                edit.Visible = false;
            }
        }
        protected void pkgData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
              
                int index = Convert.ToInt32(e.CommandArgument);

              
                    if (selectAct.SelectedValue.Equals("0"))
                    {
                        pkgSK.Text = pkgData.DataKeys[index].Values["PKG_GROUP_SK"].ToString(); 
                        pkgGIn.Text = pkgData.DataKeys[index].Values["PKG_GROUP_DESC"].ToString();
                        add.Visible = false;
                        edit.Visible = true;
                    }
                    else if (selectAct.SelectedValue.Equals("1"))
                    {
                       
                        selectPG.SelectedValue = pkgData.DataKeys[index].Values["PKG_GROUP_SK"].ToString();
                        selectPG.Enabled = false;
                        pkgCoIn.Text = pkgData.DataKeys[index].Values["PKG_CODE"].ToString();
                        add.Visible = false;
                        edit.Visible = true;
                    }

                   
            }
            
        }
        

        protected void add_Click(object sender, EventArgs e)
        {
            String q,mes;
            if(selectAct.SelectedValue.Equals("0"))
            {
                this.PKG_GROUP_DESC = pkgGIn.Text;

                q = "INSERT INTO A_NEW_PKG_GROUP_DESC(PKG_GROUP_SK,PKG_GROUP_DESC) VALUES (a_new_action_history_seq.nextval ,'" + PKG_GROUP_DESC + "')";

            }else
            {
                this.PKG_GROUP_SK = selectPG.SelectedItem.Value;
                this.PKG_CODE = pkgCoIn.Text;
                q = "INSERT INTO A_NEW_PKG_CODE(PKG_GROUP_SK,PKG_CODE) VALUES ("+PKG_GROUP_SK+",'" + PKG_CODE + "')";
            }
           
            mes = con.querytoDB(q);
            getpkgINFO();
            selectAct.SelectedIndex = -1;
            MultiView1.ActiveViewIndex = -1;
            this.MessageBox(mes);
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            String q, mes;
            if (selectAct.SelectedValue.Equals("0"))
            {
                this.PKG_GROUP_SK = pkgSK.Text;
                this.PKG_GROUP_DESC = pkgGIn.Text;
                q = "UPDATE A_NEW_PKG_GROUP_DESC SET PKG_GROUP_DESC='" + PKG_GROUP_DESC + "' WHERE PKG_GROUP_SK ="+PKG_GROUP_SK+"";

            }
            else
            {
                this.PKG_GROUP_SK = selectPG.SelectedItem.Value;
                this.PKG_CODE = pkgCoIn.Text;
                q = "UPDATE A_NEW_PKG_CODE SET PKG_CODE='"+PKG_CODE+"'WHERE PKG_GROUP_SK ="+PKG_GROUP_SK+"";
            }

            mes = con.querytoDB(q);
            getpkgINFO();
            selectAct.SelectedIndex = -1;
            MultiView1.ActiveViewIndex = -1;
            this.MessageBox(mes);
        }

        
       
    

        

       
    }
}