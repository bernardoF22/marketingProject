using EletronicShippingSuplies.Data_Objects;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace EletronicShippingSuplies.Backend
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (DB_OSSEntities ESS = new DB_OSSEntities())
            {
                User user = null;

                // Get Data from Logged User
                if (UserManagement.AuthenticateUser(ESS, TextBoxEmail.Text, TextBoxPassword.Text, out user))
                {
                    UserManagement.LoggedUser loggedUser = new UserManagement.LoggedUser()
                    {
                        Name = user.NAME,
                        Email = user.EMAIL,
                        Account = user.DHLACCOUNT,
                        ID = user.ID,
                        isAdmin = user.ISADMIN
                    };

                    List<UserManagement.LoggedUserAddress> listOfAddresses = new List<UserManagement.LoggedUserAddress>();

                    foreach (Address a in user.Address)
                    {
                        UserManagement.LoggedUserAddress newAddress = new UserManagement.LoggedUserAddress()
                        {
                            ID = a.ID,
                            Street = a.STREET,
                            Number = a.NUMBER,
                            ZipCode = a.POSTALCODE,
                            City = a.CITY,
                            IsDefault = a.ISDEFAULT
                        };

                        listOfAddresses.Add(newAddress);
                    }

                    loggedUser.Addresses = listOfAddresses;

                    // Put Logged User data in Session
                    Session["LOGGEDUSER"] = loggedUser;
                    FormsAuthentication.RedirectFromLoginPage(TextBoxEmail.Text, false);
                }
            }
        }
    }
}