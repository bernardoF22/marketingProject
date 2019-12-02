using EletronicShippingSuplies.Data_Objects;
using System;
using System.Web.UI;

namespace EletronicShippingSuplies.Backend
{
    public partial class AddNewAddress : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CreateAdress_Click(object sender, EventArgs e)
        {
            try
            {
                using (DB_OSSEntities oss = new DB_OSSEntities())
                {
                    UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];

                    /* Session Address*/
                    UserManagement.LoggedUserAddress userAddress = new UserManagement.LoggedUserAddress()
                    {
                        Street = Street.Text,
                        Number = Number.Text,
                        ZipCode = PostalCode.Text,
                        City = City.Text,
                    };

                    /* Database Address */
                    var Address = new Address()
                    {
                        STREET = Street.Text,
                        NUMBER = Number.Text,
                        POSTALCODE = PostalCode.Text,
                        CITY = City.Text,
                        OWNER = currentUser.ID
                    };
                    oss.Address.Add(Address);
                    int a = oss.SaveChanges();
                    /* Address ID after insert in database object Address*/

                    ((UserManagement.LoggedUser)Session["LOGGEDUSER"]).Addresses.Add(userAddress);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Sucesso", "SuccessMessageAddress();", true);
                }
                ShowNotification("Morada adicionada com sucesso ! A redirecionar .", WarningType.Success);
            }
            catch (Exception ex)
            {
                ShowNotification("Erro !", WarningType.Danger);
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}