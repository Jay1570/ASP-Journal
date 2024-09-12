<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UpdatePassword.aspx.vb" Inherits="ASP.UpdatePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <div class="form-group">
                <h2>Update Password</h2>
            </div>
            <form>
                <div class="form-group mt-3">
                    <label for="txtOldPass">Old Password</label>
                    <asp:TextBox ID="txtOldPass" TextMode="Password" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtNewPass">New Password</label>
                    <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="txtConfirmPasswordd">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnUpdatePass" runat="server" Text="Update Password" CssClass="btn btn-primary mt-3 form-control" />
                </div>
            </form>
            <p class="mt-3">
                Already have an account? <a href="<%= ResolveUrl("~/Default.aspx") %>">Login here</a>.
            </p>
        </div>
    </div>
</asp:Content>