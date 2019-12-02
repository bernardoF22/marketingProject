using System;
using System.Web.UI;
using EletronicShippingSuplies.Data_Objects;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data;

namespace EletronicShippingSuplies.Backend
{
    public partial class Userdash : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadAddresses();
            }
        }

        private void loadAddresses()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("STREET", typeof(string));
            dt.Columns.Add("NUMBER", typeof(string));
            dt.Columns.Add("POSTALCODE", typeof(string));
            dt.Columns.Add("CITY", typeof(string));
            dt.Columns.Add("ISDEFAULT", typeof(string));
            dt.Columns.Add("BTNNAME", typeof(string));

            UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];
            txtNameLabel.Text = currentUser.Name;
            txtEmailLabel.Text = currentUser.Email;
            txtDHLAccountLabel.Text = currentUser.Account;

            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                List<Address> addresses = AddressManagement.GetUserAddress(oss, currentUser.ID);
                if (addresses != null)
                {
                    foreach (Address address in addresses)
                    {
                        DataRow row = dt.NewRow();
                        row["ID"] = address.ID;
                        row["STREET"] = address.STREET;
                        row["NUMBER"] = address.NUMBER;
                        row["POSTALCODE"] = address.POSTALCODE;
                        row["CITY"] = address.CITY;

                        if (address.ISDEFAULT == true)
                        {
                            row["ISDEFAULT"] = "<i style='margin-top:10px;color:green;'class='fa fa-check-circle fa-lg'></i><br/>";
                            row["BTNNAME"] = "Disable";
                            txtAddressStreet.Text = address.STREET;
                            txtAddressNumber.Text = address.NUMBER;
                            txtPostalCode.Text = address.POSTALCODE;
                            txtCity.Text = address.CITY;
                        }
                        else
                        {
                            row["ISDEFAULT"] = "<i style='margin-top:10px;color:red;'class='fa fa-times-circle fa-lg'></i><br/>";
                            row["BTNNAME"] = "Enable";
                        }
                        dt.Rows.Add(row);
                    }
                }
                grdAddresses.DataSource = dt;
                grdAddresses.DataBind();
            }
        }

        protected void grdAddresses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iID = Int32.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "AvailableBtn")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        Address ad = AddressManagement.GetAddressDetails(oss, iID);

                        if (ad.ISDEFAULT == true)
                        {
                            ad.ISDEFAULT = false;
                        }
                        else
                        {
                            ad.ISDEFAULT = true;
                        }
                        oss.Address.Attach(ad);
                        oss.Entry(ad).State = EntityState.Modified;
                        var validate = oss.GetValidationErrors();
                        if (validate.Count() == 0)
                        {
                            oss.SaveChanges();
                            loadAddresses();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            if (e.CommandName == "Deleted")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        Address address = AddressManagement.DeleteAddress(oss, iID);
                        loadAddresses();
                    }
                }
                catch (Exception ex)
                {
                }
            }

            if (e.CommandName == "Update")
            {
                try
                {
                    using (DB_OSSEntities oss = new DB_OSSEntities())
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                        Address ad = AddressManagement.GetAddressDetails(oss, iID);

                        if (ad != null)
                        {
                            ad.STREET = ((TextBox)row.FindControl("txtEditStreet")).Text;
                            ad.NUMBER = ((TextBox)row.FindControl("txtEditNumber")).Text;
                            ad.CITY = ((TextBox)row.FindControl("txtEditCity")).Text;
                            ad.POSTALCODE = ((TextBox)row.FindControl("txtEditPostalCode")).Text;

                            if (ValidateFields(ad))
                            {
                                var validate = oss.GetValidationErrors();
                                if (validate.Count() == 0)
                                {
                                    oss.SaveChanges();
                                }
                            }
                        }
                        grdAddresses.EditIndex = -1;
                        loadAddresses();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected bool ValidateFields(Address ad)
        {
            bool bOK = true;

            if (ad.STREET.Trim() == "" && ad.NUMBER.Trim() == "" && ad.CITY.Trim() == "" && ad.POSTALCODE.Trim() == "")
            {
                bOK = false;
            }
            return bOK;
        }

        protected void addAddressRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewAddress.aspx");
        }

        protected void grdAddresses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAddresses.EditIndex = e.NewEditIndex;
            loadAddresses();
        }

        protected void grdAddresses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAddresses.EditIndex = -1;
            loadAddresses();
        }
    }
}
