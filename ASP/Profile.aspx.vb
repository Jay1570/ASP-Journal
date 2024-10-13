Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities

Public Class Profile
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
        LoadUserProfile()
    End Sub

    Private Sub LoadUserProfile()
        Try
            Dim id = Session("id")
            Dim cmd As New SqlCommand("SELECT name, email FROM Users WHERE [ID] = @id", cn)
            cmd.Parameters.AddWithValue("@id", id)
            cn.Open()
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then
                txtName.Text = reader("name").ToString()
                txtEmail.Text = reader("email").ToString()
            End If
            reader.Close()
            cn.Close()
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

    Private Sub btnEditEmail_Click(sender As Object, e As EventArgs) Handles btnEditEmail.Click
        txtEmail.Enabled = Not txtEmail.Enabled
        txtEmail.Focus()
    End Sub

    Private Sub btnEditName_Click(sender As Object, e As EventArgs) Handles btnEditName.Click
        txtName.Enabled = Not txtName.Enabled
        txtName.Focus()
    End Sub

    Private Sub btnUpdateProfile_Click(sender As Object, e As EventArgs) Handles btnSaveDetails.Click
        Try
            Dim email As String = txtEmail.Text.Trim()
            Dim name As String = txtName.Text.Trim()

            If email.IsNullOrWhiteSpace() Or name.IsNullOrWhiteSpace() Then
                Response.Write("<script>alert('All Fields are mandatory.');</script>")
                Return
            End If

            Dim cmd As New SqlCommand("UPDATE Users SET [name] = @name, [email] = @email WHERE [id] = @id", cn)
            cmd.Parameters.AddWithValue("@name", name)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@id", Session("id"))
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                Response.Write("<script>alert('Profile updated successfully');</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to sign out?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
        If result = MsgBoxResult.Yes Then
            Session.Clear()
            Session.Abandon()
            Dim userCookie As New HttpCookie("id")
            userCookie.Expires = DateTime.Now.AddMinutes(-1)
            Response.Cookies.Add(userCookie)
            MsgBox("Signed Out Successfully", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Response.Redirect("Default.aspx")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to Delete your Account?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
        If result = MsgBoxResult.Yes Then
            Try
                Dim cmd As New SqlCommand("DELETE FROM Users WHERE [ID] = @id", cn)
                cmd.Parameters.AddWithValue("@id", Session("id"))
                cn.Open()
                Dim execute As Integer = cmd.ExecuteNonQuery()
                cn.Close()
                If execute > 0 Then
                    Session.Clear()
                    Session.Abandon()
                    Dim userCookie As New HttpCookie("id")
                    userCookie.Expires = DateTime.Now.AddMinutes(-1)
                    Response.Cookies.Add(userCookie)
                    Response.Write("<script>alert('Profile deleted successfully.');</script>")
                    Response.Redirect("Default.aspx", False)
                Else
                    Response.Write("<script>alert('Error :- Profile not deleted.');</script>")
                End If
            Catch ex As Exception
                Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
                cn.Close()
            End Try
        End If
    End Sub
End Class