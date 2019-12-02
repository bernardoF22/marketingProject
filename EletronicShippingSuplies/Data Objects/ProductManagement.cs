using System.IO;
using System.Linq;
using System;
using System.Drawing;

namespace EletronicShippingSuplies
{
    public class ProductManagement
    {
        public static Product GetProductDetails(DB_OSSEntities oss, int iID)
        {
            var product = oss.Product.FirstOrDefault(s => s.ID == iID);
            return product;
        }

        public static IQueryable<Product> GetProducts(DB_OSSEntities oss)
        {
            IQueryable<Product> products = null;
            products = (from pr in oss.Product
                        select pr);
            return products;
        }

        public static decimal GetPriceByID(DB_OSSEntities oss, int id)
        {
            decimal productInternalID = 0;
            productInternalID = (from ao in oss.Product
                                 where ao.ID.Equals(id)
                                 select ao.PRICE).First();

            if (productInternalID == 0)
            {
                return 0;
            }
            else
            {
                return productInternalID;
            }
        }


        public static Product GetProductByInternalID(DB_OSSEntities oss, int prodID)
        {
            var productInternalID = from ao in oss.Product
                                    where ao.INTERNAL_ID.Equals(prodID)
                                    select ao;
            if (productInternalID.Count() == 0)
            {
                return null;
            }
            else
            {
                return productInternalID.First();
            }
        }

        public static Product GetProductByID(DB_OSSEntities oss, int ID)
        {
            var productID = from ao in oss.Product
                            where ao.ID.Equals(ID)
                            select ao;
            if (productID.Count() == 0)
            {
                return null;
            }
            else
            {
                return productID.First();
            }
        }


        public static Product DeleteProduct(DB_OSSEntities oss, int iID)
        {
            var product = oss.Product.First(s => s.ID == iID);
            if (product != null)
            {
                oss.Product.Remove(product);
                oss.SaveChanges();
                return product;
            }
            else
            {
                return null;
            }
        }
    }
}
