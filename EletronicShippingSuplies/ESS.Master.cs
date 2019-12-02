using EletronicShippingSuplies.Data_Objects;
using System;
using System.Web;
using System.Web.UI.HtmlControls;

namespace EletronicShippingSuplies
{
    public partial class ESS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];
            if (currentUser != null)
            {
                if (currentUser.isAdmin == true)
                {
                    ((HtmlGenericControl)loginView.FindControl("visibleMenusAdmin")).Visible = true;
                    ((HtmlGenericControl)loginView.FindControl("visibleMenusUser")).Visible = false;

                }
                else
                {
                    ((HtmlGenericControl)loginView.FindControl("visibleMenusAdmin")).Visible = false;
                    ((HtmlGenericControl)loginView.FindControl("visibleMenusUser")).Visible = true;
                }
            }
        }
    }
}