<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.vb" Inherits="ASP.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-4">
            <center><h2>Register</h2></center>
            <form>
                <div class="form-group mt-3">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtConfirmPasswordd">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary mt-3 form-control" OnClick="btnRegister_Click" />
                </div>
            </form>
            <p class="mt-3">
                Already have an account? <a href="<%= ResolveUrl("~/Default.aspx") %>">Login here</a>.
            </p>
        </div>
    </div>

</asp:Content>
