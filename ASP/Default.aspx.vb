Imports System.Data.SqlClient

Public Class _Default
    Inherits Page

    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Journal").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.Cookies("id") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            Else
                Session("id") = Request.Cookies("id").Value
            End If
            Dim topicId As Integer = 1
            If Integer.TryParse(Request.QueryString("topicId"), topicId) Then
                LoadTopicContent(topicId)
            Else
                LoadTopicContent()
            End If
        End If
    End Sub

    Private Sub LoadTopicContent(ByVal topicId As Integer)
        Try
            Dim cmd As New SqlCommand("SELECT content FROM Topics WHERE ID=" & topicId, cn)
            cn.Open()
            Dim contentText As String = cmd.ExecuteScalar()
            cn.Close()
            content.Text = contentText
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

    Private Sub LoadTopicContent()
        Try
            Dim cmd As New SqlCommand("SELECT TOP 1 [content] FROM Topics ORDER BY [ID]", cn)
            cn.Open()
            Dim contentText As String = cmd.ExecuteScalar()
            cn.Close()
            content.Text = contentText
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub
End Class