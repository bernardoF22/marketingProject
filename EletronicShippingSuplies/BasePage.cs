using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EletronicShippingSuplies
{
    public enum WarningType
    {
        Success,
        Info,
        Warning,
        Danger
    }

    public class BasePage : System.Web.UI.Page
    {

        public void ShowNotification(string message, WarningType type)
        {
            string sIcon = "";
            switch (type)
            {
                case WarningType.Success:
                    sIcon = "fas fa-check-circle";
                    break;
                case WarningType.Info:
                    sIcon = "fas fa-info-circle";
                    break;
                case WarningType.Warning:
                    sIcon = "fas fa-exclamation-circle";
                    break;
                case WarningType.Danger:
                    sIcon = "fas fa-times";
                    break;
                default:
                    sIcon = "fas fa-exclamation-triangle";
                    break;

            }
            string notifyScript = "$.notify(  ";
            notifyScript += "               { message: '" + message + "', icon: '" + sIcon + "' },";
            notifyScript += "               { type: '" + type.ToString().ToLower() + "', offset: 5, z_index: 2147483647, placement: {from: 'top', align: 'center' } }";
            notifyScript += "      );";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), System.Guid.NewGuid().ToString(), notifyScript, true);
        }
    }
}