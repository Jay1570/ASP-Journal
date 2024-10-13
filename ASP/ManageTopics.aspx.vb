Imports System.Data.SqlClient

Public Class ManageTopics
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
        LoadUserTopics()
    End Sub

    Private Sub LoadUserTopics()
        Try
            Dim id = Session("id")
            Dim cmd As New SqlCommand("SELECT T.[ID], T.[topicName] FROM [Topics] T INNER JOIN [Users] U ON T.[UserId] = U.[ID] WHERE U.[ID] = @id", cn)
            cmd.Parameters.AddWithValue("@id", id)
            cn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            gvTopics.DataSource = reader
            gvTopics.DataBind()
            cn.Close()
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

    Protected Sub btnTopicDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim btnTopicDelete As Button = CType(sender, Button)
            Dim topicId As Integer = Convert.ToInt32(btnTopicDelete.CommandArgument)
            Dim cmd As New SqlCommand("DELETE FROM Topics WHERE [ID] = @topicId", cn)
            Dim masterPage As SiteMaster = CType(Page.Master, SiteMaster)
            cmd.Parameters.AddWithValue("@topicId", topicId)
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                Response.Write("<script>alert('Topic deleted successfully.');</script>")
                masterPage.RefreshNavBar()
                LoadUserTopics()
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

End Class