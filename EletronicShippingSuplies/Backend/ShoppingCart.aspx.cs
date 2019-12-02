using EletronicShippingSuplies.Data_Objects;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EletronicShippingSuplies.Backend
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showProductsinCart();
            }
        }

        public void showProductsinCart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(double));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("ItemTotalPrice", typeof(double));

            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];
                IQueryable<Cart> cart = CartManagement.GetOwnersCart(oss, currentUser.ID);
                decimal totalAmount = 0;
                decimal value = 0;
                foreach (Cart c in cart)
                {
                    Product p = ProductManagement.GetProductByID(oss, c.PRODUCT_ID);
                    DataRow row = dt.NewRow();

                    row["id"] = c.PRODUCT_ID.ToString();
                    row["Name"] = p.NAME;
                    row["Description"] = p.DESCRIPTION;
                    row["UnitPrice"] = p.PRICE;
                    row["Quantity"] = c.QUANTITY;
                    row["ItemTotalPrice"] = (c.QUANTITY * p.PRICE);
                    totalAmount += c.QUANTITY * p.PRICE;
                    dt.Rows.Add(row);
                }
                value = Math.Round(totalAmount, 2);
                txtTotalAmount .Text = value.ToString();
                grdShoppingCart.DataSource = dt;
                grdShoppingCart.DataBind();
            }
        }

        protected void grdShoppingCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iID = Int32.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DeleteBtn")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        Cart cr = CartManagement.DeleteCart(oss, iID);
                        showProductsinCart();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (e.CommandName == "UpdateBtn")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                        Cart c = CartManagement.GetProductInCartByID(oss, iID);
                        if (c != null)
                        {
                            c.QUANTITY = Int32.Parse(((TextBox)row.FindControl("txtProdQuantity")).Text);
                        }
                        if (Int32.Parse(((TextBox)row.FindControl("txtProdQuantity")).Text) == 0)
                        {
                            Cart cr = CartManagement.DeleteCart(oss, iID);
                        }
                        oss.SaveChanges();
                        grdShoppingCart.EditIndex = -1;
                        showProductsinCart();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void grdShoppingCart_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdShoppingCart.EditIndex = e.NewEditIndex;
            showProductsinCart();
        }

        protected void grdShoppingCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdShoppingCart.EditIndex = -1;
            showProductsinCart();
        }

        protected void cartCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }
    }
}
