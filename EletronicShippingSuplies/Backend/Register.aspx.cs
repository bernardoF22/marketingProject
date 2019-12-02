using EletronicShippingSuplies.Data_Objects;
using System;

namespace EletronicShippingSuplies.Backend
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            bool success = true;

            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                var user = new User()
                {
                    NAME = Name.Text,
                    EMAIL = Email.Text,
                    PHONE = Phone.Text,
                    PASSWORD = UserManagement.ComputePassword(Password.Text, "SHA512", null),
                    ISVALID = true,
                    ISADMIN = false,
                    SIGNUPDATE = DateTime.Now,
                    DHLACCOUNT = DHLACCOUNT.Text
                };
                oss.User.Add(user);
                oss.SaveChanges();
                if (success)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}