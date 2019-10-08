using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Test_SQLMerge
{
    class Program
    {
        static DataSet dtSet;
        static void Main(string[] args)
        {
            CreateCustomersTable() ;
            
            callSp();
        }


        static public void callSp()
        {
            using (SqlConnection con = new SqlConnection("Data Source=STP-PC\\SQLEXPRESS;Initial Catalog=cricket;User ID=sa;Password=root"))
            {
                using (SqlCommand cmd = new SqlCommand("update_mirror", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Add("@playerdata", SqlDbType.Da).Value = txtFirstName.Text;


                    cmd.Parameters.AddWithValue("@playerdata", dtSet.Tables["playerdata"]);

                 
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void CreateCustomersTable()  
        {  
            // Create a new DataTable.    
            DataTable custTable = new DataTable("playerdata");  
            DataColumn dtColumn;  
            DataRow myDataRow;  
  
            // Create id column  
            dtColumn = new DataColumn();  
            dtColumn.DataType = typeof(Int32);  
            dtColumn.ColumnName = "id";  
            dtColumn.Unique = true;  
            // Add column to the DataColumnCollection.  
            custTable.Columns.Add(dtColumn);  
  
            // Create Name column.    
            dtColumn = new DataColumn();  
            dtColumn.DataType = typeof(String);  
            dtColumn.ColumnName = "name";  
             dtColumn.ReadOnly = false;  
            dtColumn.Unique = false;  
            /// Add column to the DataColumnCollection.  
            custTable.Columns.Add(dtColumn);  
  

  
            // Make id column the primary key column.    
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];  
            PrimaryKeyColumns[0] = custTable.Columns["id"];  
            custTable.PrimaryKey = PrimaryKeyColumns;  
  
            // Create a new DataSet  
            dtSet = new DataSet();  
  
            // Add custTable to the DataSet.    
            dtSet.Tables.Add(custTable);  
  
            // Add data rows to the custTable using NewRow method    
            // I add three customers with their addresses, names and ids   
            myDataRow = custTable.NewRow();  
            myDataRow["id"] = 1001;  
            myDataRow["name"] = "George Bishop";  
            custTable.Rows.Add(myDataRow);  
            myDataRow = custTable.NewRow();  
            myDataRow["id"] = 1002;  
            myDataRow["name"] = "Rock joe";  

            custTable.Rows.Add(myDataRow);  
            myDataRow = custTable.NewRow();  
            myDataRow["id"] = 1003;  
            myDataRow["name"] = "Miranda";  
            custTable.Rows.Add(myDataRow);  
        }  


    }
}
