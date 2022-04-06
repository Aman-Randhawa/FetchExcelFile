using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSP_Loader
{
    public partial class UploadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            // Coneection String by default empty
            string ConStr = "";
            //Extantion of the file upload control saving into ext because
            //there are two types of extation .xls and .xlsx of Excel
            string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
            //getting the path of the file
            String path;
            path = Server.MapPath("~/MyFolder/" + FileUpload1.FileName);
            //saving the file inside the MyFolder of the server
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(path);
                // Label1.Text = FileUpload1.FileName + "\'s Data showing into the GridView";
                //checking that extantion is .xls or .xlsx
                if (ext.Trim() == ".xls")
                {
                    //connection string for that file which extantion is .xls
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                }
                else if (ext.Trim() == ".xlsx")
                {
                    //connection string for that file which extantion is .xlsx
                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                }
                //  Making Interop objects for working with excel file
                Microsoft.Office.Interop.Excel.Application appEx1 = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook xlWorkBook = appEx1.Workbooks.Open(path, ReadOnly: true);
                // Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = xlWorkBook.ActiveSheet;  // Second method of displaying excel sheet
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = xlWorkBook.Sheets[1];
                Microsoft.Office.Interop.Excel.XlDirection goUp = Microsoft.Office.Interop.Excel.XlDirection.xlUp;
                // Count columns upto AF Column
                int columnCount = xlWorkSheet.Cells[xlWorkSheet.Rows.Count, "AF"].End(goUp).Row;
                // just for my reference showing outpu
                // Response.Write(columnCount);  

                string test = columnCount.ToString();

                string cellValue = ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[105, 18]).Value.ToString();

                //Label1.Text = cellValue;
                xlWorkSheet.Columns.AutoFit();
                xlWorkBook.Save();
                xlWorkBook.Close();

                appEx1.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(appEx1);


                string query = "select * from [Sheet1$Q79:AF" + test + "]";


                // Providing connection
                OleDbConnection conn = new OleDbConnection(ConStr);
                //checking that connection state is closed or not if closed the
                //open the connection
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //create command object
                OleDbCommand cmd = new OleDbCommand(query, conn);
                // create a data adapter and get the data into dataadapter
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dtExcelData = new DataTable();
                //fill the Excel data to data set
                da.Fill(ds);
                da.Fill(dtExcelData);
                //set data source of the grid view
                conn.Close();
                // Connection to database

                string connection = @"Data Source = (localdb)\ProjectsV13; Initial Catalog = demo; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                SqlConnection cmd1;
                cmd1 = new SqlConnection(connection);
                if (cmd1.State == System.Data.ConnectionState.Closed)
                {
                    cmd1.Open();

                }

                using (SqlBulkCopy sqlbc = new SqlBulkCopy(cmd1))
                {
                    // Mapping to database
                    sqlbc.DestinationTableName = "importTodatabase";
                    sqlbc.ColumnMappings.Add("F1", "Loc_name");
                    sqlbc.ColumnMappings.Add("F2", "Loc_id");
                    sqlbc.ColumnMappings.Add("40002", "acc_1");
                    sqlbc.ColumnMappings.Add("40003", "acc_2");
                    sqlbc.ColumnMappings.Add("40004", "acc_3");
                    sqlbc.ColumnMappings.Add("40005", "acc_4");
                    sqlbc.ColumnMappings.Add("40006", "acc_5");
                    sqlbc.ColumnMappings.Add("40007", "acc_6");
                    sqlbc.ColumnMappings.Add("40008", "acc_7");
                    sqlbc.ColumnMappings.Add("40009", "acc_8");
                    sqlbc.ColumnMappings.Add("F11", "acc_9");
                    sqlbc.ColumnMappings.Add("42100", "acc_10");
                    sqlbc.ColumnMappings.Add("42200", "acc_11");
                    sqlbc.ColumnMappings.Add("42300", "acc_12");
                    sqlbc.ColumnMappings.Add("F15", "acc_13");
                    sqlbc.ColumnMappings.Add("Total", "total");


                    // Write data to database
                    sqlbc.WriteToServer(dtExcelData);
                    if (Label1.Visible == false)
                    {
                        Label1.Visible = true;
                        Label1.Text = "Data stored successfully";
                    }
                }

            }
            else
            {
                Label2.Visible = true;
                Label2.ForeColor = System.Drawing.Color.Red;
                Label2.Text = "Please upload a file";
            }
        }

    }
   
    
}