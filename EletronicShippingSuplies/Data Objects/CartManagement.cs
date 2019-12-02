using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EletronicShippingSuplies.Data_Objects
{
    public class CartManagement
    {

        public static List<Cart> GetCart(DB_OSSEntities oss, int iID)
        {
            List<Cart> cart = (from ct in oss.Cart
                        where ct.OWNER == iID
                        select ct).ToList();
            return cart;
        }

        public static int GetCartOwnerCount(DB_OSSEntities oss, int ownerID)
        {
            IQueryable<Cart> cart = null;
            cart = (from ct in oss.Cart
                    where ct.OWNER == ownerID
                    select ct);
            return cart.Count();
        }

        public static Cart GetCartbyOwnerID(DB_OSSEntities oss, int ownerID)
        {
            Cart cart = null;
            cart = (from c in oss.Cart
                    where c.OWNER == ownerID
                    select c).First();
            return cart;
        }

        public static IQueryable<Cart> GetOwnersCart(DB_OSSEntities oss, int ownerID)
        {
            IQueryable<Cart> cart = null;
            cart = (from ct in oss.Cart
                    where ct.OWNER == ownerID
                    select ct);
            return cart;
        }

        public static int GetCartRandomID(DB_OSSEntities oss, int ownerID)
        {
            int cart = 0;
            cart = (from c in oss.Cart
                    where c.OWNER == ownerID
                    select c.CARTID).First();
            return cart;
        }

        public static IQueryable<int> GetAllCartsID(DB_OSSEntities oss)
        {
            IQueryable<int> cartIDS = null;

            cartIDS = (from ct in oss.Cart
                       select ct.CARTID);

            return cartIDS;
        }

        public static Cart GetProductInCartByID(DB_OSSEntities oss, int prodID)
        {
            var productInCartInternalID = from ao in oss.Cart
                                          where ao.PRODUCT_ID.Equals(prodID)
                                          select ao;
            if (productInCartInternalID.Count() == 0)
            {
                return null;
            }
            else
            {
                return productInCartInternalID.First();
            }
        }

        public static IQueryable<Cart> GetCart(DB_OSSEntities oss)
        {
            IQueryable<Cart> cart = null;
            cart = (from ct in oss.Cart
                    select ct);
            return cart;
        }

        public static Cart DeleteCart(DB_OSSEntities oss, int iID)
        {
            var cart = oss.Cart.First(s => s.PRODUCT_ID == iID);
            if (cart != null)
            {
                oss.Cart.Remove(cart);
                oss.SaveChanges();
                return cart;
            }
            else
            {
                return null;
            }
        }
    }
}

