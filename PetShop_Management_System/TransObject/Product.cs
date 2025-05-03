using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class Product
    {
        public string ProductID { get; set; }
        public string PrName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        public Product(string productID, string prName, decimal price, int stock, string category)
        {
            ProductID = productID;
            PrName = prName;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public Product() { }
    }
}
