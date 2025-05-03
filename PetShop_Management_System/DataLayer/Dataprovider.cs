using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class Dataprovider
    {
        protected SqlConnection cn;
       // protected SqlCommand cm = new SqlCommand();
        public Dataprovider()
        {
            string cnstr = "Data Source=.;Initial Catalog=PetShop;Integrated Security=True";
            cn = new SqlConnection(cnstr);
        }
        public void Connect()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public void Disconnect()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }
        public object MyExcuteScalar(string sql, CommandType type)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;
                return (cmd.ExecuteScalar());
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
        public SqlDataReader MyExcuteReader(string sql, CommandType type)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

    }
}
