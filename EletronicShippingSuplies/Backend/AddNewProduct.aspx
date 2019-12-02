<%@ Page Title="New Product" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="AddNewProduct.aspx.cs" Inherits="EletronicShippingSuplies.Backend.AddNewProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h3>New Product Form</h3>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="internalID" CssClass="col-md-2 control-label">Internal ID</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="internalID" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="internalID"
                        CssClass="text-danger" ErrorMessage="The Internal ID is required." />
                </div>
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="name" CssClass="col-md-2 control-label">Name</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="name" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="name"
                        CssClass="text-danger" ErrorMessage="The name field is required." />
                </div>
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="description" CssClass="col-md-2 control-label">Description</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="description" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="description"
                        CssClass="text-danger" ErrorMessage="The description field is required." />
                </div>
            </div>

        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="price" CssClass="col-md-2 control-label">Price</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="price" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="price"
                    CssClass="text-danger" ErrorMessage="The price field is required." />
            </div>
        </div>

    </div>
    <div class="form-group">
        <asp:Label Style="font-weight: bold;" runat="server" CssClass="col-md-2 control-label">Product Picture Upload</asp:Label>
        <div class="col-md-5">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
    </div>

    </br>
    </br>
    </br>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <asp:Button runat="server" CssClass="btn btn-success btn-sm" OnClick="CreateProduct_Click" Text="Add" />
            <asp:Button runat="server" CssClass="btn btn-danger btn-sm" CausesValidation="false" OnClick="Cancel_Click" Text="Cancel" />
        </div>
    </div>
</asp:Content>
