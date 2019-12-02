<%@ Page Title="" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="AddNewAddress.aspx.cs" Inherits="EletronicShippingSuplies.Backend.AddNewAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Add a new Address to your account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Street" CssClass="col-md-2 control-label">Street</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="Street" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Street"
                        CssClass="text-danger" ErrorMessage="The street field is required." />
                </div>
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Number" CssClass="col-md-2 control-label">Number</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="Number" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Number"
                        CssClass="text-danger" ErrorMessage="The number field is required." />
                </div>
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="PostalCode" CssClass="col-md-2 control-label">PostalCode</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="PostalCode" CssClass="form-control zip-code" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="PostalCode"
                        CssClass="text-danger" ErrorMessage="The postal code field is required." />
                </div>
            </div>

        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-5">
                <asp:TextBox runat="server" ID="City" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="City"
                    CssClass="text-danger" ErrorMessage="The city field is required." />
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" CssClass="btn btn-success btn-sm" OnClick="CreateAdress_Click" Text="Add" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" CausesValidation="false" OnClick="Cancel_Click" Text="Cancel" />
            </div>
        </div>
    </div>
</asp:Content>
