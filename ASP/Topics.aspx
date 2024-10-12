<%@ Page Title="Topic" ValidateRequest="false" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Topics.aspx.vb" Inherits="ASP.Topics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .content-textbox {
            height: 40vh;
            width: 100%;
            max-width: 800px;
            min-width: 300px;
            max-height: 500px;
            min-height: 200px;
            resize: none;
        }
        .title-textbox {
            width: 100%;
            max-width: 800px;
            min-width: 300px;
        }
    </style>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-12">
                <form>
                    <div class="form-group mt-3">
                        <label for="txtTitle">Title</label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control title-textbox" />
                    </div>
                    <div class="form-group">
                        <label for="txtDescription">Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control content-textbox" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnSave" runat="server" Text="" CssClass="btn btn-primary form-control mt-3" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>