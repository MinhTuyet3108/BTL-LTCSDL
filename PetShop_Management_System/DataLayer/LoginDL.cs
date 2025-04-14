using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;


namespace DataLayer
{
    public class LoginDL: Dataprovider
    {
            public bool CheckLogin(Account account)
            {
                try
                {
                    Connect();
                    string sql = "SELECT COUNT(*) FROM Employee WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", account.Username);
                    cmd.Parameters.AddWithValue("@Password", account.Password);

                    int result = (int)cmd.ExecuteScalar();
                    return result > 0;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    Disconnect();
                }
            }
        }
    }


