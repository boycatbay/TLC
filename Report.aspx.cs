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
    public partial class Report : System.Web.UI.Page
    {
        static String area,startd,endd,nevi;
        DBA con = new DBA();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                nevi = Session["Neviga"].ToString();
                start.Text = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
                end.Text = DateTime.Today.ToString("MM/dd/yyyy");
                Area();
                btnExport.Visible = false;
            }
        }
        protected void Area()
        {
            String q = "select * from a_new_process_step";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            marea.DataSource = ds.Tables[0];
            marea.DataMember = ds.Tables[0].TableName;
            marea.DataTextField = ds.Tables[0].Columns["PS_DESC"].ColumnName;
            marea.DataValueField = ds.Tables[0].Columns["PS_CODE"].ColumnName;
            marea.DataBind();
        }

        protected void marea_SelectedIndexChanged(object sender, EventArgs e)
        {
            area = marea.SelectedValue;
            Machine();
        }

        protected void Machine()
        {
            string q = "select m_id from a_new_machine where ps_code = '"+area+"'";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            mac.DataSource = ds.Tables[0];
            mac.DataMember = ds.Tables[0].TableName;
            mac.DataTextField = ds.Tables[0].Columns["M_ID"].ColumnName;
            mac.DataValueField = ds.Tables[0].Columns["M_ID"].ColumnName;
            mac.DataBind();
        }

        protected void select_Click(object sender, EventArgs e)
        {
            startd = start.Text;
            endd = end.Text;
            btnExport.Visible = true;
            //DateTime startDate = DateTime.Parse(start.Text);
            //DateTime endDate = DateTime.Parse(end.Text);
            //TimeSpan diff = endDate - startDate;
            //double days = diff.TotalDays;

            if (Session["Neviga"].Equals("Normal Status"))
            {
                Multiview1.ActiveViewIndex = 0;
                getNormal();
            }
            else if (Session["Neviga"].Equals("Extends Status"))
            {
                Multiview1.ActiveViewIndex = 1;
                getExtend();
            }
            else if (Session["Neviga"].Equals("Adjust Count"))
            {
                Multiview1.ActiveViewIndex = 2;
                getAdjust();
            }
        }

        protected void getNormal()
        {
            string q = "select * from v_report_normal where occur_date between '"+startd+"' and '"+endd+"'";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nm.DataSource = ds;
                nm.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                nm.DataSource = ds;
                nm.DataBind();
                int columncount = nm.Rows[0].Cells.Count;
                nm.Rows[0].Cells.Clear();
                nm.Rows[0].Cells.Add(new TableCell());
                nm.Rows[0].Cells[0].ColumnSpan = columncount;
                nm.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void getExtend()
        {
            string q = "select * from v_report_extend where occur_date between '"+startd+"' and '"+endd+"'";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                et.DataSource = ds;
                et.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                et.DataSource = ds;
                et.DataBind();
                int columncount = et.Rows[0].Cells.Count;
                et.Rows[0].Cells.Clear();
                et.Rows[0].Cells.Add(new TableCell());
                et.Rows[0].Cells[0].ColumnSpan = columncount;
                et.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void getAdjust()
        {
            string q = "select * from v_report_adjust where occur_date between '"+startd+"' and '"+endd+"'";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                aj.DataSource = ds;
                aj.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                aj.DataSource = ds;
                aj.DataBind();
                int columncount = aj.Rows[0].Cells.Count;
                aj.Rows[0].Cells.Clear();
                aj.Rows[0].Cells.Add(new TableCell());
                aj.Rows[0].Cells[0].ColumnSpan = columncount;
                aj.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (nevi == "Normal Status")
            {
                //Multiview1.ActiveViewIndex = 0;
                string q = "select * from v_report_normal where occur_date between '" + startd + "' and '" + endd + "'";
                DataSet ds = con.getData(q);

                ToExcel(ds,"Normal",Response);
            }
            else if (nevi == "Extends Status")
            {
                //Multiview1.ActiveViewIndex = 1;
                string q = "select * from v_report_extend where occur_date between '" + startd + "' and '" + endd + "'";
                DataSet ds = con.getData(q);
                ToExcel(ds,"Extend", Response);
            }
            else if (nevi == "Adjust Count")
            {
                //Multiview1.ActiveViewIndex = 2;
                string q = "select * from v_report_adjust where occur_date between '" + startd + "' and '" + endd + "'";
                DataSet ds = con.getData(q);
                ToExcel(ds,"Adjust", Response);
            }
        }

        //public class myExcelHelper
       // {
            //Row limits older Excel version per sheet
            const int rowLimit = 65000;

            private static string getWorkbookTemplate()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<xml version>\r\n<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");
                sb.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n xmlns:x=\"urn:schemas- microsoft-com:office:excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\r\n");
                sb.Append(" <Styles>\r\n <Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n <Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>");
                sb.Append("\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>\r\n <Protection/>\r\n </Style>\r\n <Style ss:ID=\"BoldColumn\">\r\n <Font ");
                sb.Append("x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n <Style ss:ID=\"s62\">\r\n <NumberFormat");
                sb.Append(" ss:Format=\"@\"/>\r\n </Style>\r\n <Style ss:ID=\"Decimal\">\r\n <NumberFormat ss:Format=\"0.0000\"/>\r\n </Style>\r\n ");
                sb.Append("<Style ss:ID=\"Integer\">\r\n <NumberFormat ss:Format=\"0\"/>\r\n </Style>\r\n <Style ss:ID=\"DateLiteral\">\r\n <NumberFormat ");
                sb.Append("ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n <Style ss:ID=\"s28\">\r\n");
                sb.Append("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Top\" ss:ReadingOrder=\"LeftToRight\" ss:WrapText=\"1\"/>\r\n");
                sb.Append("<Font x:CharSet=\"1\" ss:Size=\"9\" ss:Color=\"#808080\" ss:Underline=\"Single\"/>\r\n");
                sb.Append("<Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/> </Style>\r\n</Styles>\r\n {0}</Workbook>");
                return sb.ToString();
            }

            private static string replaceXmlChar(string input)
            {
                input = input.Replace("&", "&");
                input = input.Replace("<", "<");
                input = input.Replace(">", ">");
                //input = input.Replace("\"", """);
                input = input.Replace("'", "&apos;");
                return input;
            }

            private static string getWorksheets(DataSet source)
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                if (source == null || source.Tables.Count == 0)
                {
                    sw.Write("<Worksheet ss:Name=\"Sheet1\"><Table><Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row></Table></Worksheet>");
                    return sw.ToString();
                }
                foreach (DataTable dt in source.Tables)
                {
                    if (dt.Rows.Count == 0)
                        sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\"><Table><Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row></Table></Worksheet>");
                    else
                    {
                        //write each row data
                        int sheetCount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((i % rowLimit) == 0)
                            {
                                //add close tags for previous sheet of the same data table
                                if ((i / rowLimit) > sheetCount)
                                {
                                    sw.Write("</Table></Worksheet>");
                                    sheetCount = (i / rowLimit);
                                }
                                sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + (((i / rowLimit) == 0) ? "" : Convert.ToString(i / rowLimit)) + "\"><Table>");

                                //write column name row
                                sw.Write("<Row>");
                                foreach (DataColumn dc in dt.Columns)
                                    sw.Write(string.Format("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dc.ColumnName)));
                                sw.Write("</Row>\r\n");
                            }
                            sw.Write("<Row>\r\n");

                            foreach (DataColumn dc in dt.Columns)
                                sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>", replaceXmlChar(dt.Rows[i][dc.ColumnName].ToString())));
                            sw.Write("</Row>\r\n");
                        }
                        sw.Write("</Table></Worksheet>");
                    }
                }

                return sw.ToString();
            }
            public static string GetExcelXml(DataTable dtInput, string filename)
            {
                string excelTemplate = getWorkbookTemplate();
                DataSet dsInput = new DataSet();
                dsInput.Tables.Add(dtInput.Copy());
                string worksheets = getWorksheets(dsInput);
                string excelXml = string.Format(excelTemplate, worksheets);
                return excelXml;
            }

            public static string GetExcelXml(DataSet dsInput, string filename)
            {
                string excelTemplate = getWorkbookTemplate();
                string worksheets = getWorksheets(dsInput);
                string excelXml = string.Format(excelTemplate, worksheets);
                return excelXml;
            }

            public static void ToExcel(DataSet dsInput, string filename, HttpResponse response)
            {
                string excelXml = GetExcelXml(dsInput, filename);
                response.Clear();
                response.AppendHeader("Content-Type", "application/vnd.ms-excel");
                response.AppendHeader("Content-disposition", "attachment; filename=" + filename + ".xls");
                response.Write(excelXml);
                response.Flush();
                response.End();
            }

            public static void ToExcel(DataTable dtInput, string filename, HttpResponse response)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dtInput.Copy());
                ToExcel(ds, filename, response);
            }

            protected void nm_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                nm.PageIndex = e.NewPageIndex;
                getNormal();
            }
            protected void et_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                et.PageIndex = e.NewPageIndex;
                getExtend();
            }
            protected void aj_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                aj.PageIndex = e.NewPageIndex;
                getAdjust();
            }
        //}

    }
}