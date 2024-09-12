<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="ASP.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <div class="form-group">
                <h2>Login</h2>
            </div>
            <form>
                <div class="form-group mt-3">
                    <label for="txtEmail">Email</label>
                     <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"/>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary form-control mt-3" />
                </div>
            </form>
            <p class="mt-3">
                Don't have an account? <a href="<%= ResolveUrl("~/Register.aspx") %>">Register here</a>.
            </p>
        </div>
    </div>
</asp:Content>
