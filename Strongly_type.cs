using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemo
{
    internal class Strongly_type
    {
        private string _connectionString;
        private object sqlDbType;

        public Strongly_type(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public SqlConnection getconnection()
        {
            SqlConnection sqlconn = new SqlConnection();
            sqlconn.ConnectionString = _connectionString;
            return sqlconn;
        }
        public int AddData(Customer c)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd;
            int record = 0;
            try
            {
                sqlconn = getconnection();
                sqlcmd = new SqlCommand("storedata", sqlconn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                //sqlcmd.Parameters.Add("@pname", sqlDbType.NVarChar).Value = c.Name;
                sqlcmd.Parameters.Add("@pname", SqlDbType.NVarChar).Value = c.Name;
                sqlcmd.Parameters.Add("@paddress", SqlDbType.NVarChar).Value = c.Address;
                sqlcmd.Parameters.Add("@pmobile", SqlDbType.NVarChar).Value = c.Mobile_No;
                sqlconn.Open();
                record = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (sqlconn != null)
                {
                    sqlconn.Close();
                }
            }
            return record;

        }

        public List<Customer> GetList()
        {
            var listCustomer = new List<Customer>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_cus_GET_LIST", con);//SP_emp_GET_LIST
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listCustomer.Add(new Customer
                        {
                            ID = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                            Address = rdr["Address"].ToString(),
                            Mobile_No = rdr["Mobile_No"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCustomer;
        }
    }
}
