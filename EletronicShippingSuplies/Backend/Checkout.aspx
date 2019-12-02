<%@ Page Title="" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="EletronicShippingSuplies.Backend.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .GridViewStyle {
            border: 1px solid #ddd;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
            table-layout: auto;
            font-size: 14px;
        }

        .HeaderStyle {
            border: 1px, solid, #ddd;
            background-color: #FFCC00;
        }

            .HeaderStyle th {
                padding: 5px 0px 5px 0px;
                color: #333;
                text-align: center;
            }

        tr.RowStyle {
            text-align: center;
            background-color: #ffffff;
        }
    </style>

    <div>
        <h3>Checkout Confirmation</h3>
        <br />

        <dt>Street</dt>
        <dd>
            <asp:Label runat="server" ID="txtAddressStreet"></asp:Label>
        </dd>

        <p />

        <dt>Number</dt>
        <dd>
            <asp:Label runat="server" ID="txtAddressNumber"></asp:Label>
        </dd>

        <p />

        <dt>Postal Code</dt>
        <dd>
            <asp:Label runat="server" ID="txtPostalCode"></asp:Label>
        </dd>

        <p />

        <dt>City</dt>
        <dd>
            <asp:Label runat="server" ID="txtCity"></asp:Label>
        </dd>

        <p />

        <dt>DHL Account:</dt>
        <dd>
            <asp:Label runat="server" ID="txtDHLAccountLabel"></asp:Label>
        </dd>

        </dl>

    </div>
    <br />
    <br />

    <div>
        <asp:UpdatePanel ID="GridviewUpdatePanel" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdCheckoutProducts" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView CssClass="GridViewStyle" ID="grdCheckoutProducts" runat="server" AutoGenerateColumns="false" AllowPaging="false" GridLines="Both">
                    <HeaderStyle CssClass="HeaderStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrID" Text="ID"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrName" Text="Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblName" Text='<%# Bind("NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrDescription" Text="Description"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDescription" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrUnitPrice" Text="Unit Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblItmUnitPrice" Text='<%# Bind("UNITPRICE") %>'></asp:Label><a>€</a>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrQuantity" Text="Quantity"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblQuantity" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <br />
    <dl class="dl-horizontal">

        <dt>Total Amount</dt>
        <dd>
            <asp:Label runat="server" ID="txtTotalAmount"></asp:Label><a>€</a>
        </dd>

    </dl>

    <br />

    <div class="btn-group">
        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Confirm Checkout" ID="confirmCheckout" OnClick="confirmCheckout_Click"></asp:Button>
    </div>

</asp:Content>
