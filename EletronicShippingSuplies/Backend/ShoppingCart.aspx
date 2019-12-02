<%@ Page Title="" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="EletronicShippingSuplies.Backend.ShoppingCart" %>

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
    <p></p>
    <h2>Shopping Cart</h2>
    <br />

    <asp:UpdatePanel ID="GridViewShoppingCart" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdShoppingCart" EventName="RowCommand" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView ID="grdShoppingCart" CssClass="GridViewStyle" runat="server" AutoGenerateColumns="false" AllowPaging="false" GridLines="Both" OnRowCommand="grdShoppingCart_RowCommand" OnRowEditing="grdShoppingCart_RowEditing" OnRowCancelingEdit="grdShoppingCart_RowCancelingEdit">
                <HeaderStyle CssClass="HeaderStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblHdrProdID" Text="ID"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProdID" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblHdrProdName" Text="Name"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProdName" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblHdrProdDescription" Text="Description"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProdDescription" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblHdrProdUnitPrice" Text="Unit Price"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblItmProdUnitPrice" Text='<%# Bind("UnitPrice") %>'></asp:Label><a>€</a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblHdrProdQuantity" Text="Quantity"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProdQuantity" Text='<%# Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox type="number" runat="server" ID="txtProdQuantity" min="0" placeholder="0" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblHdrProdTotalPrice" Text="Total Price"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblItmProdTotalPrice" Text='<%# Bind("ItemTotalPrice") %>'></asp:Label><a>€</a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label runat="server" Text="Actions"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" CssClass="btn btn-info btn-sm" ID="btEdit" CommandArgument='<%#Eval("id")%>' Text="Edit" CommandName="Edit"></asp:Button>
                            <asp:Button runat="server" CssClass="btn btn-danger btn-sm" ID="btDelete" CommandArgument='<%#Eval("id")%>' Text="Delete" CommandName="DeleteBtn"></asp:Button>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button runat="server" CssClass="btn btn-danger btn-sm" ID="btCancel" CommandArgument='<%#Eval("id")%>' Text="Cancel" CommandName="Cancel"></asp:Button>
                            <asp:Button runat="server" CssClass="btn btn-success btn-sm" ID="btUpdate" CommandArgument='<%#Eval("id")%>' Text="Update" CommandName="UpdateBtn"></asp:Button>

                        </EditItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <div class="btn-group">
                <asp:Button CssClass="btn btn-success btn-sm" runat="server" Text="Checkout" ID="cartCheckout" OnClick="cartCheckout_Click"></asp:Button>
            </div>

            <dl class="dl-horizontal" style="margin-left: 25%;">

                <dt>Total Amount</dt>
                <dd>
                    <asp:Label runat="server" ID="txtTotalAmount"></asp:Label><a>€</a>
                </dd>

            </dl>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
