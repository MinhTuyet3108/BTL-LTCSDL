using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransObject;

namespace BusinessLayer
{
    public class ProductBL
    {
        private ProductDL productDL;
        public ProductBL()
        {
            productDL = new ProductDL();
        }
        public List<Product> GetProducts()
        {
            try
            {
                return productDL.GetProducts();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                productDL.Add(product);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }
        public bool DeleteProduct(string id)
        {
            try
            {
                return productDL.Delete(id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                return productDL.Update(product);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }


        public List<Product> SearchProducts(string keyword)
        {
            try
            {
                return productDL.Search(keyword);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
