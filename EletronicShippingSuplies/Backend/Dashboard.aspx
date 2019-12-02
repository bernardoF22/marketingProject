<%@ Page Title="Account Management" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EletronicShippingSuplies.Backend.Userdash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
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

    <h2><%: Title %></h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>You account details</h4>
                <hr />

                <dl class="dl-horizontal">

                    <dt>Name:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtNameLabel"></asp:Label>
                    </dd>

                    <p />

                    <dt>Email:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtEmailLabel"></asp:Label>
                    </dd>

                    <p />

                    <dt>DHL Account:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtDHLAccountLabel"></asp:Label>
                    </dd>

                    <br />
                    <h3>Default Address</h3>
                    <br />

                    <dt>Street:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtAddressStreet"></asp:Label>
                    </dd>

                    <p />

                    <dt>Number:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtAddressNumber"></asp:Label>
                    </dd>

                    <p />

                    <dt>Postal Code:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtPostalCode"></asp:Label>
                    </dd>

                    <p />

                    <dt>City:</dt>
                    <dd>
                        <asp:Label runat="server" ID="txtCity"></asp:Label>
                    </dd>
                </dl>

                <br />
                <h3>Saved Addresses</h3>
                <br />
                <div class="btn-group">
                    <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="New Address" ID="addAddressRedirect" OnClick="addAddressRedirect_Click"></asp:Button>
                </div>
                <p />
                <div>
                    <asp:UpdatePanel ID="GridviewUpdatePanel" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grdAddresses" EventName="RowCommand" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView CssClass="GridViewStyle" ID="grdAddresses" runat="server" AutoGenerateColumns="false" OnRowCancelingEdit="grdAddresses_RowCancelingEdit" OnRowEditing="grdAddresses_RowEditing" OnRowCommand="grdAddresses_RowCommand" AllowPaging="false" GridLines="Both">
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
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtID" Text='<%# Bind("ID") %>'></asp:TextBox>
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblHdrStreet" Text="Street"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblItmStreet" Text='<%# Bind("STREET") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtEditStreet" Text='<%# Bind("STREET") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblHdrNumber" Text="Number"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblItmNumber" Text='<%# Bind("NUMBER") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtEditNumber" Text='<%# Bind("NUMBER") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblHdrPostalCode" Text="Postal Code"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblItmPostalCode" Text='<%# Bind("POSTALCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtEditPostalCode" Text='<%# Bind("POSTALCODE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblHdrCity" Text="City"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblItmCity" Text='<%# Bind("CITY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtEditCity" Text='<%# Bind("CITY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblHdrIsDefalut" Text="IsDefault?"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblIsDefalut" Text='<%# Bind("ISDEFAULT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" Text="Actions"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" CssClass="btn btn-info btn-sm" ID="btEdit" Text="Edit" CommandArgument='<%#Eval("ID")%>' CommandName="Edit"></asp:Button>
                                            <asp:Button runat="server" CssClass="btn btn-danger btn-sm" ID="btDelete" Text="Delete" CommandArgument='<%#Eval("ID")%>' CommandName="Deleted"></asp:Button>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button runat="server" CssClass="btn btn-danger btn-sm" ID="btCancel" Text="Cancel" CommandArgument='<%#Eval("ID")%>' CommandName="Cancel"></asp:Button>
                                            <asp:Button runat="server" CssClass="btn btn-success btn-sm" ID="btUpdate" Text="Update" CommandArgument='<%#Eval("ID")%>' CommandName="Update"></asp:Button>
                                            <asp:Button runat="server" CssClass="btn btn-light btn-sm" ID="btnAvailable" Text='<%# DataBinder.Eval(Container, "DataItem.BTNNAME") %>' CommandArgument='<%#Eval("id")%>' CommandName="AvailableBtn"></asp:Button>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
