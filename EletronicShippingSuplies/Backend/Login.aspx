<%@ Page Title="" Language="C#" MasterPageFile="~/ESS.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EletronicShippingSuplies.Backend.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="form-horizontal">
                <h4>Use your DHL email to log in.</h4>
                <hr />
                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                    <p class="text-danger">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                </asp:PlaceHolder>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="TextBoxEmail" CssClass="col-md-2 control-label">Email</asp:Label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="TextBoxEmail" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxEmail"
                            CssClass="text-danger" ErrorMessage="The email field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="TextBoxPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="TextBoxPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxPassword" CssClass="text-danger" ErrorMessage="The password field is required." />
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:LinkButton ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Log in" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
            <p>
                <br />
                <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
            </p>
            <p>
            </p>
        </div>
    </div>
</asp:Content>
