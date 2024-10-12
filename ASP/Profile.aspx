<%@ Page Title="Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Profile.aspx.vb" Inherits="ASP.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mb-4">
        <div class="col-md-6">
            <h2>Profile Information</h2>
        </div>
    </div>
    <div class="row mb-3">
        <label for="txtName" class="col-sm-2 col-form-label">Name:</label>
        <div class="col-sm-6">
            <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnEditName" runat="server" CssClass="btn btn-primary" Text="Edit" />
        </div>
    </div>
    <div class="row mb-3">
        <label for="txtEmail" class="col-sm-2 col-form-label">Email:</label>
        <div class="col-sm-6">
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnEditEmail" runat="server" CssClass="btn btn-primary" Text="Edit" />
        </div>
    </div>
    <br />
    <div class="row mb-3 justify-content-center">
        <div class="col-sm-2">
            <asp:Button ID="btnSaveDetails" runat="server" CssClass="btn btn-primary" Text="Update Details" />
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnSignOut" runat="server" CssClass="btn btn-warning" Text="Signout" />
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete Account" />
        </div>
        <div class="col-sm-2">
            <a ID="btnUpdatePassword" class="btn btn-warning" href="UpdatePassword.aspx">Update Password</a>
        </div>
    </div>
    <br />
    <div class="row mb-4">
        <div class="col-md-6">
            <h2>Your Topics</h2>
            <asp:GridView ID="gvTopics" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Topic ID" />
                    <asp:BoundField DataField="topicName" HeaderText="Topic Name" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkEdit" runat="server" Text="Edit" NavigateUrl='<%# "Topics.aspx?topicId=" & Eval("ID") %>' CssClass="btn btn-primary btn-sm" />
                            <asp:Button ID="btnTopicDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger btn-sm" OnClick="btnTopicDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
