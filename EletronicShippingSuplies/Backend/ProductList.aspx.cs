using EletronicShippingSuplies.Data_Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EletronicShippingSuplies.Backend
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindListViewControls();
            }
        }

        private void BindListViewControls()
        {
            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(int));
                table.Columns.Add("internalID", typeof(string));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("description", typeof(string));
                table.Columns.Add("price", typeof(double));
                table.Columns.Add("image", typeof(byte[]));

                IEnumerable<Product> productList = ProductManagement.GetProducts(oss);
                foreach (Product p in productList)
                {
                    if (p.ISAVAILABLE == true)
                    {
                        DataRow row = table.NewRow();

                        row["internalID"] = p.INTERNAL_ID;
                        row["name"] = p.NAME;
                        row["description"] = p.DESCRIPTION;
                        row["price"] = p.PRICE.ToString();
                        row["image"] = p.IMAGE;
                        row["id"] = p.ID;

                        table.Rows.Add(row);
                    }
                }
                ProductListView.DataSource = table;
                ProductListView.DataBind();
            }
        }

        protected string ReturnEncodedBase64UTF8(object rawImg)
        {
            string img = "data:image/gif;base64,{0}";
            byte[] toEncodeAsBytes = (byte[])rawImg;
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return String.Format(img, returnValue);
        }

        protected bool CreateRandomCartID(out int createdNumber)
        {
            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                Random rd = new Random();
                var cartID = CartManagement.GetAllCartsID(oss);
                createdNumber = rd.Next();
                if (!cartID.Contains(createdNumber))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected void ProductListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                int iID = int.Parse(e.CommandArgument.ToString());
                HtmlTableCell row = (HtmlTableCell)((Control)e.CommandSource).Parent;
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        Product p = ProductManagement.GetProductByID(oss, iID);
                        decimal price = ProductManagement.GetPriceByID(oss, iID);
                        UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];
                        int quantity = int.Parse(((HtmlInputControl)row.FindControl("quantity")).Value);
                        int cartexist = CartManagement.GetCartOwnerCount(oss, currentUser.ID);
                        {
                            if (cartexist == 0)
                            {
                                int createdNumber = 0;
                                if (CreateRandomCartID(out createdNumber) == true)
                                {
                                    decimal TotalPrice = price * quantity;
                                    Cart newcart = new Cart();
                                    newcart.PRODUCT_ID = iID;
                                    newcart.OWNER = currentUser.ID;
                                    newcart.PRICE = TotalPrice;
                                    newcart.DATE = DateTime.Now;
                                    newcart.STATE = "OPENED";
                                    newcart.QUANTITY = quantity;
                                    newcart.CARTID = createdNumber;

                                    var validate = oss.GetValidationErrors();
                                    if (validate.Count() == 0)
                                    {
                                        oss.Cart.Add(newcart);
                                    }
                                }
                            }
                            if (cartexist != 0)
                            {
                                Cart existCart = CartManagement.GetCartbyOwnerID(oss, currentUser.ID);
                                if (existCart.PRODUCT_ID == iID)
                                {
                                    decimal TotalPrice = existCart.PRICE + (price * quantity);
                                    existCart.QUANTITY += quantity;
                                    existCart.PRICE = TotalPrice;
                                }
                                else
                                {
                                    decimal TotalPrice = price * quantity;
                                    Cart newcart = new Cart();
                                    newcart.PRODUCT_ID = iID;
                                    newcart.OWNER = currentUser.ID;
                                    newcart.PRICE = TotalPrice;
                                    newcart.DATE = DateTime.Now;
                                    newcart.STATE = "OPENED";
                                    newcart.QUANTITY = quantity;
                                    newcart.CARTID = existCart.CARTID;

                                    var validate = oss.GetValidationErrors();
                                    if (validate.Count() == 0)
                                    {
                                        oss.Cart.Add(newcart);
                                    }
                                }
                            }
                        }
                        oss.SaveChanges();
                    }
                }

                catch (Exception ex)
                {
                }
            }
        }
    }
}