<%@ Page Title="" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="EletronicShippingSuplies.Backend.ProductList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <asp:ListView ID="ProductListView" GroupItemCount="4" OnItemCommand="ProductListView_ItemCommand" runat="server">
                <EmptyDataTemplate>
                    <table runat="server">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td runat="server" />
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>

                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <img style="width: 70%;" src="<%# ReturnEncodedBase64UTF8(Eval("image")) %>" />
                                        <p />
                                        <%# Eval("name")%>
                                        <p />
                                        <%# Eval("description")%>
                                        <p />
                                        <%# Eval("price")%>€ 
                                    </div>
                                    <div>
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                    </div>
                                    <input runat="server" type="number" id="quantity" min="0" placeholder="0" style="width: 25%; border-radius: 4px; margin: 1%;" />
                                    <asp:Button CssClass="btn btn-light btn-sm" runat="server" ID="AddToCart" Text="Add to Cart" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('Added to Cart successfully');" CommandName="AddToCart"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server" style="width: 100%;">
                        <tbody>
                            <tr runat="server">
                                <td runat="server">
                                    <table id="groupPlaceholderContainer" runat="server" style="width: 100%">
                                        <tr id="groupPlaceholder" runat="server"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server"></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>
