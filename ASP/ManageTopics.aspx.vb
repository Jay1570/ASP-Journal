Imports System.Data.OleDb

Public Class ManageTopics
    Inherits System.Web.UI.Page

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        If Session("id") Is Nothing Then
            MsgBox("You are not logged in...", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Response.Redirect("Default.aspx")
            Return
        End If
        LoadUserTopics()
    End Sub

    Private Sub LoadUserTopics()
        Try
            Dim id = Session("id")
            Dim cmd As New OleDbCommand("SELECT T.ID, T.topicName FROM Topics T INNER JOIN Users U ON T.UserId = U.ID WHERE U.ID = ?", cn)
            cmd.Parameters.AddWithValue("ID", id)
            cn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            gvTopics.DataSource = reader
            gvTopics.DataBind()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub

    Protected Sub btnTopicDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim btnTopicDelete As Button = CType(sender, Button)
            Dim topicId As Integer = Convert.ToInt32(btnTopicDelete.CommandArgument)
            Dim cmd As New OleDbCommand("DELETE FROM Topics WHERE ID = @topicId", cn)
            cmd.Parameters.AddWithValue("@topicId", topicId)
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                MsgBox("Topic Deleted Successfully", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                LoadUserTopics()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub

End Class