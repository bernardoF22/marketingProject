<%@ Page Title="" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="AdminProductDash.aspx.cs" Inherits="EletronicShippingSuplies.Backend.AdminProductDash" %>

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
    <br />
    <h2>Admin Products Overview Dashboard</h2>
    <br />
    <div>

        <div class="btn-group">
            <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="New Product" ID="addProductRedirect" OnClick="addProductRedirect_Click"></asp:Button>
        </div>
        <br />
        <br />

        <asp:UpdatePanel ID="GridviewUpdatePanelAdmin" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdProductsAdmin" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView CssClass="GridViewStyle" ID="grdProductsAdmin" runat="server" AutoGenerateColumns="false" AllowPaging="false" GridLines="Both" OnRowCommand="grdProductsAdmin_RowCommand" OnRowCancelingEdit="grdProductsAdmin_RowCancelingEdit" OnRowEditing="grdProductsAdmin_RowEditing">
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
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtProdID" Text='<%# Bind("id") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrInternalProdID" Text="InternalID"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInternalProdID" Text='<%# Bind("internalID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtInternalProdID" Text='<%# Bind("internalID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrProdName" Text="Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblProdName" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtProdName" Text='<%# Bind("name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrProdDescription" Text="Description"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblProdDescription" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtProdDescription" Text='<%# Bind("description") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrProdPrice" Text="Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblItmProdPrice" Text='<%# Bind("price") %>'></asp:Label><a>€</a>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtProdPrice" Text='<%# Bind("price") %>'></asp:TextBox><a>€</a>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblHdrProdAvailable" Text="Available"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblProdAvailable" Text='<%# Bind("isAvailable") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Actions"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button CssClass="btn btn-info btn-sm" runat="server" ID="btEdit" Text="Edit" CommandArgument='<%#Eval("id")%>' CommandName="Edit"></asp:Button>
                                <asp:Button CssClass="btn btn-danger btn-sm" runat="server" ID="btDelete" Text="Delete" CommandArgument='<%#Eval("id")%>' CommandName="DeleteBtn"></asp:Button>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button CssClass="btn btn-danger btn-sm" runat="server" ID="btCancel" Text="Cancel" CommandArgument='<%#Eval("id")%>' CommandName="Cancel"></asp:Button>
                                <asp:Button CssClass="btn btn-success btn-sm" runat="server" ID="btUpdate" Text="Update" CommandArgument='<%#Eval("id")%>' CommandName="UpdateBtn"></asp:Button>
                                <asp:Button CssClass="btn btn-light btn-sm" runat="server" ID="btnAvailable" Text='<%# DataBinder.Eval(Container, "DataItem.BTNNAME") %>' CommandArgument='<%#Eval("id")%>' CommandName="AvailableBtn"></asp:Button>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
