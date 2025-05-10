using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataLayer;
using TransObject;

namespace BusinessLayer
{
    public class LoginBL
    {
        private LoginDL loginDL;
        public  LoginBL()
        {
            loginDL = new LoginDL();
        }
        public string Login(Account account)
        {
            try
            {
                return (loginDL.CheckLogin(account));

            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
    }
}
