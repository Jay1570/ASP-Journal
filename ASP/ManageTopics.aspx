<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManageTopics.aspx.vb" Inherits="ASP.ManageTopics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .gridview {
            margin-top: 20px;
            width: 100%;
            overflow-y: auto;
            align-content: center;
            align-items: center;
        }
    </style>
    <div class="row mb-4">
        <div class="col-md-12">
            <h2>Your Topics</h2>
            <asp:GridView ID="gvTopics" runat="server" AutoGenerateColumns="False" CssClass="table table-striped gridview">
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
                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
