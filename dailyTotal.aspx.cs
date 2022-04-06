using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSP_Loader
{
    public partial class dailyTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connection2 = @"Data Source = (localdb)\ProjectsV13; Initial Catalog = demo; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection cmd3;
            cmd3 = new SqlConnection(connection2);
            if (cmd3.State == ConnectionState.Closed)
            {
                cmd3.Open();

            }
            string query3;
            // get value by today's date
            query3 = "(SELECT  CONVERT(int, ROUND(SUM(total),0)) as TOTAL_PER_DAY FROM importTodatabase WHERE CONVERT(DATE,today_date)= CONVERT(Date,GETDATE()))";
            // get values by specific date
            // query3 = "(SELECT  SUM(total) as TOTAL_PER_DAY FROM importTodatabase WHERE today_date = '2022-03-22')";
            // round off to int
            // query3 = "(SELECT  CONVERT(int, ROUND(SUM(total),0)) as TOTAL_PER_DAY FROM importTodatabase WHERE today_date = '2022-03-22')";
            SqlCommand commands = new SqlCommand(query3, cmd3);
            SqlDataAdapter da2 = new SqlDataAdapter(commands);
            DataSet ds2 = new DataSet();
            DataTable dtTotal = new DataTable();
            da2.Fill(ds2);
            da2.Fill(dtTotal);
            gvDataFile.DataSource = dtTotal;
            //Txttotal.DataBind(dtTotal);
            gvDataFile.DataBind();
            cmd3.Close();
        }
    }
}