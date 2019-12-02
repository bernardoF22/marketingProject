using System;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace EletronicShippingSuplies.Backend
{
    public partial class AddNewProduct : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void CreateProduct_Click(object sender, EventArgs e)
        {
            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                if (FileUpload1.HasFile)
                {
                    string strname = FileUpload1.FileName.ToString();
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/upload/") + strname);

                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                    }

                    Product p = new Product();
                    p.INTERNAL_ID = internalID.Text;
                    p.NAME = name.Text;
                    p.DESCRIPTION = description.Text;
                    p.PRICE = decimal.Parse(price.Text);
                    p.ISAVAILABLE = true;
                    p.IMAGE = fileData;

                    var validate = oss.GetValidationErrors();
                    if (validate.Count() == 0)
                    {
                        oss.Product.Add(p);
                        oss.SaveChanges();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Sucesso", "SuccessMessageProduct();", true);
                    }
                }
            }
            ShowNotification("Produto adicionada com sucesso ! A redirecionar.", WarningType.Success);
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminProductDash.aspx");
        }
    }
}