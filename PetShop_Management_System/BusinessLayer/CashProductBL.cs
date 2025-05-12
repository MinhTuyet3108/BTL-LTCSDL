using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using DataLayer;
using System.Data.SqlClient;

namespace BusinessLayer
{
  public class CashProductBL
    {
        private CashProductDL cashproductDL;

        public CashProductBL()
        {
            cashproductDL = new CashProductDL();
        }
        public List<Product> GetProducts()
        {
            try
            {
                return cashproductDL.GetProducts();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public void AddCash(Cash cash)
        {
            // Kiểm tra dữ liệu đầu vào
            if (cash == null)
                throw new ArgumentNullException(nameof(cash), "Đối tượng Cash không được null.");

            // Kiểm tra các tham số bắt buộc
            if (string.IsNullOrWhiteSpace(cash.Transno))
                throw new ArgumentException("Transno không được để trống.", nameof(cash.Transno));
            if (string.IsNullOrWhiteSpace(cash.Pcode))
                throw new ArgumentException("Pcode không được để trống.", nameof(cash.Pcode));
            if (string.IsNullOrWhiteSpace(cash.Cid))
                throw new ArgumentException("Cid không được để trống.", nameof(cash.Cid));
            if (string.IsNullOrWhiteSpace(cash.Cashier))
                throw new ArgumentException("Cashier không được để trống.", nameof(cash.Cashier));
            if (cash.Price <= 0)
                throw new ArgumentException("Price phải lớn hơn 0.", nameof(cash.Price));

            
        }
    }
}
