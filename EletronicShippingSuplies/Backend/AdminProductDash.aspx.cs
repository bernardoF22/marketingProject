using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using System.Data.Entity;

namespace EletronicShippingSuplies.Backend
{
    public partial class AdminProductDash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showProducts();
                GridviewUpdatePanelAdmin.Update();
            }
        }

        private void showProducts()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("internalID", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("price", typeof(double));
            dt.Columns.Add("isAvailable", typeof(string));
            dt.Columns.Add("BTNNAME", typeof(string));

            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                IQueryable<Product> productList = ProductManagement.GetProducts(oss);

                foreach (Product p in productList)
                {
                    DataRow row = dt.NewRow();
                    row["id"] = p.ID.ToString();
                    row["internalID"] = p.INTERNAL_ID;
                    row["name"] = p.NAME;
                    row["description"] = p.DESCRIPTION;
                    row["price"] = p.PRICE.ToString();
                    if (p.ISAVAILABLE == true)
                    {
                        row["isAvailable"] = "<i style='margin-top:10px;color:green;'class='fa fa-check-circle fa-lg'></i><br/>";
                        row["BTNNAME"] = "Disable";
                    }
                    else
                    {
                        row["isAvailable"] = "<i style='margin-top:10px;color:red;'class='fa fa-times-circle fa-lg'></i><br/>";
                        row["BTNNAME"] = "Enable";
                    }
                    dt.Rows.Add(row);
                }
                grdProductsAdmin.DataSource = dt;
                grdProductsAdmin.DataBind();
            }
        }
        protected void addProductRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewProduct.aspx");
        }

        protected void grdProductsAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iID = Int32.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "AvailableBtn")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        Product pr = ProductManagement.GetProductDetails(oss, iID);
                        if (pr.ISAVAILABLE == true)
                        {
                            pr.ISAVAILABLE = false;
                        }
                        else
                        {
                            pr.ISAVAILABLE = true;
                        }
                        oss.Product.Attach(pr);
                        oss.Entry(pr).State = EntityState.Modified;
                        var validate = oss.GetValidationErrors();
                        if (validate.Count() == 0)
                        {
                            oss.SaveChanges();
                            showProducts();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            if (e.CommandName == "DeleteBtn")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        Product pr = ProductManagement.DeleteProduct(oss, iID);
                        showProducts();
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
                        Product prd = ProductManagement.GetProductDetails(oss, iID);

                        if (prd != null)
                        {
                            prd.INTERNAL_ID = ((TextBox)row.FindControl("txtInternalProdID")).Text;
                            prd.NAME = ((TextBox)row.FindControl("txtProdName")).Text;
                            prd.DESCRIPTION = ((TextBox)row.FindControl("txtProdDescription")).Text;
                            prd.PRICE = Decimal.Parse(((TextBox)row.FindControl("txtProdPrice")).Text);
                            if (ValidateFields(prd))
                            {
                                var validate = oss.GetValidationErrors();
                                if (validate.Count() == 0)
                                {
                                    oss.SaveChanges();
                                }
                            }
                        }
                        grdProductsAdmin.EditIndex = -1;
                        showProducts();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected bool ValidateFields(Product pr)
        {
            bool bOK = true;

            if (pr.INTERNAL_ID.Trim() == "" && pr.NAME.Trim() == "" && pr.DESCRIPTION.Trim() == "" && pr.PRICE <= 0)
            {
                bOK = false;
            }
            return bOK;
        }

        protected void grdProductsAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            grdProductsAdmin.EditIndex = e.NewEditIndex;
            showProducts();
        }

        protected void grdProductsAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            grdProductsAdmin.EditIndex = -1;
            showProducts();
        }
    }
}