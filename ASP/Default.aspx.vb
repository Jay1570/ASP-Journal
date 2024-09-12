Imports System.Data.OleDb

Public Class _Default
    Inherits Page

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Request.Cookies("email") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Session("email") = Request.Cookies("email").Value.ToString()
        End If
        If Not IsPostBack Then
            Dim topicId As Integer = 1
            If Integer.TryParse(Request.QueryString("topicId"), topicId) Then
                LoadTopicContent(topicId)
            Else
                LoadTopicContent(1)
            End If
        End If
    End Sub

    Private Sub LoadTopicContent(ByVal topicId As Integer)
        Try
            Dim cmd As New OleDbCommand("SELECT content FROM Topics WHERE ID=" & topicId, cn)
            cn.Open()
            Dim contentText As String = cmd.ExecuteScalar()
            cn.Close()
            content.Text = contentText
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub
End Class