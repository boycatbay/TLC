using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

namespace Toollife
{
    public class DBA
    {
        public OracleConnection connect()
        {
            string constr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            OracleConnection con = new OracleConnection(constr);
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
            return con;
        }
        public void disconnect(OracleConnection con)
        {
            if (con.State == ConnectionState.Open || con.State == ConnectionState.Connecting)
                con.Close();
        }
        public DataSet getData(String q)
        {
            OracleConnection con = this.connect();
            OracleCommand cmd = new OracleCommand(q,con);
            DataSet ds = new DataSet();
            OracleDataAdapter adp = new OracleDataAdapter(cmd);
            adp.Fill(ds);
            disconnect(con);
            return ds;
        }
        public String querytoDB(String q)
        {
            String mes;
            int rv = 0; ;
            OracleConnection con1 = this.connect();
            OracleCommand cmd = new OracleCommand(q, con1);
            

            try
            {
                rv = cmd.ExecuteNonQuery();

            }
            catch (OracleException e)
            {
                mes = "Code: " + e.Code + "Message: " + e.Message;
               
                disconnect(con1);
                return mes;
                
            }
            finally
            {
                if (rv > 0)
                {
                    mes = "Successful Running ";
                }
                else 
                {
                    mes = "Can not find matching data";
                }

                
                
            }
            disconnect(con1);
            return mes;
        }
    }
}
/* Coding By Boycatbay */