using EletronicShippingSuplies.Data_Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EletronicShippingSuplies.WROrderMgmt;
using System.Net;

namespace EletronicShippingSuplies.Backend
{
    public partial class Checkout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadAllInfo();
        }

        private void loadAllInfo()
        {
            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];
                txtDHLAccountLabel.Text = currentUser.Account;
                List<Address> addresses = AddressManagement.GetUserAddress(oss, currentUser.ID);
                foreach (Address address in addresses)
                {
                    if (address.ISDEFAULT == true)
                    {
                        txtAddressStreet.Text = address.STREET;
                        txtAddressNumber.Text = address.NUMBER;
                        txtPostalCode.Text = address.POSTALCODE;
                        txtCity.Text = address.CITY;
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("UnitPrice", typeof(double));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("ItemTotalPrice", typeof(double));

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
                txtTotalAmount.Text = value.ToString();
                grdCheckoutProducts.DataSource = dt;
                grdCheckoutProducts.DataBind();
            }
        }

        protected void confirmCheckout_Click(object sender, EventArgs e)
        {
            createShipmentRequest();
        }

        private void createShipmentRequest()
        {
            UserManagement.LoggedUser currentUser = (UserManagement.LoggedUser)Session["LOGGEDUSER"];
            string acc = currentUser.Account;
            string name = currentUser.Name;
            string addr1 = txtAddressStreet.Text;
            string addr2 = txtAddressNumber.Text;
            string zip = txtPostalCode.Text;
            string cty = txtCity.Text;

            ICredentials credentials = new NetworkCredential("vasp", "vasp");
            WROrderMgmt.CreateSupplyOrder supplyOrder = new WROrderMgmt.CreateSupplyOrder();

            supplyOrder.Credentials = credentials;
            supplyOrder.PreAuthenticate = true;

            WROrderMgmt.createSupplyOrderRequest request = new WROrderMgmt.createSupplyOrderRequest();
            request.MSG = new WROrderMgmt.createSupplyOrderRequestMSG();
            request.MSG.Hdr = new WROrderMgmt.HDR();

            //<Hdr Id="1234567890" Ver="2.001" Dtm="2019-02-21T11:00:00Z">
            request.MSG.Hdr.Id = "1232445";
            request.MSG.Hdr.Ver = "2.0";
            request.MSG.Hdr.DtmSpecified = true;
            request.MSG.Hdr.Dtm = DateTime.Now;

            //<GI SrcDtm="2019-02-21T11:00:00Z">
            request.MSG.Hdr.GI = new WROrderMgmt.GenInfo();
            request.MSG.Hdr.GI.SrcDtm = DateTime.Now;


            //<TID Src="E2E" TID="1"/>
            TIDType tidtype = new WROrderMgmt.TIDType();
            tidtype.Src = "IVR";
            tidtype.TID = "1";
            request.MSG.Hdr.GI.TID = new TIDType[1];
            request.MSG.Hdr.GI.TID[0] = tidtype;


            //<Sndr AppCd="IVR" CtryCd="PT" AppNm="IVR">
            request.MSG.Hdr.Sndr = new WROrderMgmt.PartIdentification();
            request.MSG.Hdr.Sndr.AppCd = "IVR";
            request.MSG.Hdr.Sndr.CtryCd = "CN";
            request.MSG.Hdr.Sndr.AppNm = "IVR";


            //<Ord Pri="H">
            request.MSG.Bd = new WROrderMgmt.createSupplyOrderRequestMSGBD();
            request.MSG.Bd.Ord = new WROrderMgmt.CdmOrder_Order();
            request.MSG.Bd.Ord.Pri = "H";

            //CdmOrder_OrderCustomerCommunicationDevice SCCDev = new CdmOrder_OrderCustomerCommunicationDevice();
            //SCCDev.CDevNo = "083899552622";
            //SCCDev.CDevTyCd = "TEL";
            //request.MSG.Bd.Ord.sc

            //RECEIVER
            //<CDtl NtwAccNo="961003764" Addr1="AVENIDA DA INDUSTRIA 255" Addr2="VILA FRESCAINHA - SAO MARTINHO" 
            //CtyNm ="BARCELOS" CustNm="ANA SIMOES" CtryCd="PT" Zip="4750-823" 
            //CntNm = "NOCIR S.A" CRlTyCd = "RQ" AddrId = "46"> </CDtl>
            CdmOrder_OrderCustomerDetail CDtl = new WROrderMgmt.CdmOrder_OrderCustomerDetail();
            CDtl.NtwAccNo = acc;
            CDtl.Addr1 = addr1;
            CDtl.Addr2 = addr2;
            CDtl.CtyNm = cty;
            CDtl.CustNm = name;
            CDtl.CtryCd = "PT";
            CDtl.Zip = zip;
            CDtl.CRlTyCd = "RQ";
            CDtl.AddrId = "1";
            request.MSG.Bd.Ord.CDtl = new CdmOrder_OrderCustomerDetail[1];
            request.MSG.Bd.Ord.CDtl[0] = CDtl;


            //ITEMS
            //<OrdLn ItemTy="Envelope" Qty="10"></OrdLn>
            using (DB_OSSEntities oss = new DB_OSSEntities())
            {
                List<Cart> currentCart = CartManagement.GetCart(oss, currentUser.ID);
                request.MSG.Bd.Ord.OrdLn = new CdmOrder_OrderOrdLn[currentCart.Count];
                int i = 0;
                foreach (Cart c in currentCart)
                {
                    CdmOrder_OrderOrdLn OrdLn = new WROrderMgmt.CdmOrder_OrderOrdLn();
                    Product p = ProductManagement.GetProductByID(oss, c.PRODUCT_ID);
                    OrdLn.ItemTy = p.NAME;
                    OrdLn.Qty = c.QUANTITY;
                    OrdLn.QtySpecified = true;
                    request.MSG.Bd.Ord.OrdLn[i] = OrdLn;
                    i++;
                }
            }

            //SHIPPER
            //<Addr CntNm="Daniel Santos" Addr1="Av. Dom João II 51, Piso 4" AddrId="123123" AddrNm="Hello" CtryCd="PT" 
            //CtyNm ="Lisboa" StrNm="Lisboa" StrNo="1" Zip="1990085"> </Addr>
            //request.MSG.Bd.Addr = new WROrderMgmt.CdmAddress_Address();
            //request.MSG.Bd.Addr.CntNm = "ESS DHL Express";
            //request.MSG.Bd.Addr.Addr1 = "Av. Dom João II 51, Piso 4";
            //request.MSG.Bd.Addr.AddrId = "1";
            //request.MSG.Bd.Addr.AddrNm = "51";
            //request.MSG.Bd.Addr.CtryCd = "PT";
            //request.MSG.Bd.Addr.CtyNm = "Lisboa";
            //request.MSG.Bd.Addr.StrNm = "Av. Dom João II";
            //request.MSG.Bd.Addr.StrNo = "51";
            //request.MSG.Bd.Addr.Zip = "1990-085";

            WROrderMgmt.createSupplyOrderResponse response = supplyOrder.createSupplyOrder(request);
            WROrderMgmt.createSupplyOrderResponse resp = new WROrderMgmt.createSupplyOrderResponse();
        }
    }
}