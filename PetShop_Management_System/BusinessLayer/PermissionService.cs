using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLayer;
using TransObject;

namespace BusinessLayer
{
    public class PermissionService
    {

        private readonly string position;

        public PermissionService(string position)
        {
            this.position = position.Trim(); // chuẩn hóa so sánh
        }

        public bool CanAccessDashboard()
        {
            return position == "Quản lý" || position == "Kế toán";
        }

        public bool CanAccessCustomer()
        {
            return position != ""; // tất cả các vai trò đều được
        }

        public bool CanAccessEmployee()
        {
            return position == "Quản lý";
        }

        public bool CanAccessProduct()
        {
            return position == "Quản lý" || position == "Kế toán" || position == "Nhân viên bán hàng";
        }

        public bool CanAccessPet()
        {
            return position != ""; // tất cả các vai trò đều được
        }

        public bool CanAccessAppointment()
        {
            return position == "Quản lý" || position == "Nhân viên chăm sóc";
        }

        public bool CanAccessCash()
        {
            return position == "Quản lý" || position == "Kế toán" || position == "Nhân viên bán hàng";
        }

        // Hàm tiện ích dùng chung
        public string ShowAccessDenied()
        {
            return "Bạn không có quyền truy cập thông tin này";
        }
    }
}
