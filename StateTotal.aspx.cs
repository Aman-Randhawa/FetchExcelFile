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
    public partial class StateTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connection1 = @"Data Source = (localdb)\ProjectsV13; Initial Catalog = demo; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection cmd2;
            cmd2 = new SqlConnection(connection1);
            if (cmd2.State == ConnectionState.Closed)
            {
                cmd2.Open();

            }


            string query2;
            // query2 = "(SELECT substring(Loc_id, 1, 1) as State_Id, CASE WHEN (substring(Loc_id, 1, 1) = '1') THEN SUM(total) WHEN substring(Loc_id, 1, 1) = '2' THEN SUM(total) WHEN substring(Loc_id, 1, 1) = '3' THEN SUM(total) WHEN substring(Loc_id, 1, 1) = '4' THEN SUM(total) WHEN substring(Loc_id, 1, 1) = '7' THEN SUM(total) END AS TOTAL FROM importTodatabase WHERE Loc_id! = '110' AND SUBSTRING(Loc_id, 1, 1) IN('1','2', '3', '4', '7') GROUP BY substring(Loc_id, 1, 1))";
            query2 = "(SELECT  (CASE WHEN (substring(Loc_id, 1, 1) = '1') THEN 'QLD' WHEN substring(Loc_id, 1, 1) = '2' THEN 'NSW' WHEN substring(Loc_id, 1, 1) = '3' THEN 'VIC' WHEN substring(Loc_id, 1, 1) = '4' THEN 'SA' WHEN substring(Loc_id, 1, 1) = '7' THEN 'WA' END) AS STATE_NAME,SUM(total) as TOTAL_PER_STATE FROM importTodatabase WHERE Loc_id! = '110' AND SUBSTRING(Loc_id, 1, 1) IN('1','2', '3', '4', '7') GROUP BY substring(Loc_id, 1, 1))";
            // query2 = "(SELECT substring(Loc_id, 1, 1) as STATE_ID, (CASE WHEN (substring(Loc_id, 1, 1) = '1') THEN 'QLD' WHEN substring(Loc_id, 1, 1) = '2' THEN 'NSW' WHEN substring(Loc_id, 1, 1) = '3' THEN 'VIC' WHEN substring(Loc_id, 1, 1) = '4' THEN 'SA' WHEN substring(Loc_id, 1, 1) = '7' THEN 'WA' END) AS STATE_NAME,SUM(total) as TOTAL_PER_STATE FROM importTodatabase WHERE Loc_id! = '110' AND SUBSTRING(Loc_id, 1, 1) IN('1','2', '3', '4', '7') GROUP BY substring(Loc_id, 1, 1))";

            SqlCommand commands = new SqlCommand(query2, cmd2);
            SqlDataAdapter da1 = new SqlDataAdapter(commands);
            DataSet ds1 = new DataSet();
            DataTable dtTotaldata = new DataTable();
            da1.Fill(ds1);
            da1.Fill(dtTotaldata);
            gvDataFile.DataSource = ds1.Tables[0];
            gvDataFile.DataBind();

            cmd2.Close();
        }
    }
    
}