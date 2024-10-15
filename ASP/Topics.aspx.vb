Imports System.Data.SqlClient
Public Class Topics
    Inherits System.Web.UI.Page

    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Journal").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        If Session("id") Is Nothing Then
            Response.Write("<script>alert('You are not logged in.');</script>")
            Response.Redirect("Default.aspx")
            Return
        End If
        Dim topicId As Integer = 1
        If Integer.TryParse(Request.QueryString("topicId"), topicId) Then
            btnSave.Text = "Update"
            LoadTopicContent(topicId)
        Else
            btnSave.Text = "Add Topic"
        End If
    End Sub

    Private Sub LoadTopicContent(ByVal topicId As Integer)
        Try
            Dim cmd As New SqlCommand
            Dim id = Session("id")
            cmd = New SqlCommand("SELECT topicName,content,UserId FROM Topics WHERE ID=" & topicId & "AND UserId=" & id, cn)
            cn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                txtTitle.Text = reader("topicName").ToString()
                txtDescription.Text = reader("content").ToString()
            Else
                Response.Write("<script>alert('Topic not found Or You are not authorised to edit this topic');</script>")
                Response.Redirect("Default.aspx", False)
            End If
            cn.Close()
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim topicId = Request.QueryString("topicId")
            Dim cmd As New SqlCommand
            Dim id = Session("id")
            If btnSave.Text = "Add Topic" Then
                cmd = New SqlCommand("INSERT INTO Topics ([UserId],[topicName],[content]) VALUES (@userId,@topicName,@content)", cn)
                cmd.Parameters.AddWithValue("@userId", id)
            Else
                cmd = New SqlCommand("UPDATE Topics SET [topicName]=@topicName,[content]=@content WHERE [ID]=" & topicId, cn)
            End If
            cmd.Parameters.AddWithValue("@topicName", txtTitle.Text)
            cmd.Parameters.AddWithValue("@content", txtDescription.Text)
            cn.Open()
            cmd.ExecuteNonQuery()
            If btnSave.Text = "Add Topic" Then
                cmd.CommandText = "SELECT @@IDENTITY"
                topicId = CInt(cmd.ExecuteScalar())
            End If
            cn.Close()
            Response.Redirect("Default.aspx?topicId=" & topicId, False)
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub
End Class