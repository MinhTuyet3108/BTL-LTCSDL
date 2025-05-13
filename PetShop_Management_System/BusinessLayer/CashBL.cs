using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using static TransObject.Cash;
using DataLayer;

    public class CashBL
    {
        private CashDL cashDL;
        public CashBL()
        {
            cashDL = new CashDL();
        }
        public List<Cash> GetCash()
        {
            try
            {
                return cashDL.GetCash();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public void SaveTransaction(List<Cash> cart, string transno)
        {
            try
            {
            var distinctItems = cart
            .GroupBy(c => new { c.Transno, c.Pcode, c.Price })
            .Select(g => new Cash
            {
                Transno = g.Key.Transno,
                Pcode = g.Key.Pcode,
                Pname = g.First().Pname,
                Qty = g.Sum(c => c.Qty ?? 0),
                Price = g.Key.Price,
                Total = g.Sum(c => c.Qty ?? 0) * g.Key.Price,
                Cid = g.First().Cid,
                Cashier = g.First().Cashier
            })
            .ToList();
            //lưu từng giao dịch
            foreach (var cash in cart)
                {

                    if (string.IsNullOrEmpty(cash.Cashier))
                    {
                        throw new ArgumentNullException("Cashier không được null");
                    }

                    cashDL.Add(cash); // Lưu trực tiếp mỗi giao dịch
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu giao dịch: {ex.Message}");
            }
        }


        public void AddCash(Cash cash)
        {
            if (string.IsNullOrWhiteSpace(cash.CashID))
                throw new ArgumentException("Mã giao dịch là bắt buộc.");
            if (string.IsNullOrWhiteSpace(cash.Transno))
                throw new ArgumentException("Số giao dịch là bắt buộc.");
            if (cash.Price <= 0)
                throw new ArgumentException("Giá phải lớn hơn 0.");

            cashDL.Add(cash);
        }

        public bool UpdateCash(Cash cash)
        {
            if (string.IsNullOrWhiteSpace(cash.CashID))
                throw new ArgumentException("Mã giao dịch là bắt buộc.");
            if (string.IsNullOrWhiteSpace(cash.Transno))
                throw new ArgumentException("Số giao dịch là bắt buộc.");
            if (cash.Price <= 0)
                throw new ArgumentException("Giá phải lớn hơn 0.");

            return cashDL.Update(cash);
        }

        public void DeleteCash(string cashId) // Thay int thành string
        {
            if (string.IsNullOrWhiteSpace(cashId))
                throw new ArgumentException("Mã giao dịch là bắt buộc.");
            cashDL.Delete(cashId);
        }

        public List<Cash> SearchCash(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Cash>();
            return cashDL.Search(keyword);
        }

        public void DeleteCash(object cashId)
        {
            throw new NotImplementedException();
        }


    }





