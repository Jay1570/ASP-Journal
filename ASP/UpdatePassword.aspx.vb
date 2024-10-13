Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities

Public Class UpdatePassword
    Inherits System.Web.UI.Page

    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Journal").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id") Is Nothing Then
            Response.Write("<script>alert('You are not logged in.');</script>")
            Response.Redirect("Default.aspx")
        End If
    End Sub

    Private Sub btnUpdatePass_Click(sender As Object, e As EventArgs) Handles btnUpdatePass.Click
        Try
            Dim id = Session("id")
            Dim oldPass As String = txtOldPass.Text.Trim()
            Dim newPass As String = txtNewPass.Text.Trim()
            Dim confirmPass As String = txtConfirmPassword.Text.Trim()

            If oldPass.IsNullOrWhiteSpace() Or newPass.IsNullOrWhiteSpace() Or confirmPass.IsNullOrWhiteSpace() Then
                Response.Write("<script>alert('All Fields are mandatory.');</script>")
                Return
            End If

            If newPass <> confirmPass Then
                Response.Write("<script>alert('New and Confirm Password do not match.');</script>")
                Return
            End If

            Dim cmd As New SqlCommand("SELECT COUNT(ID) FROM Users WHERE [ID] = @id AND [password] = @password", cn)

            cmd.Parameters.AddWithValue("@id", id)
            cmd.Parameters.AddWithValue("@password", oldPass)
            cn.Open()
            Dim rowCount = CInt(cmd.ExecuteScalar())
            cn.Close()

            If rowCount = 0 Then
                Response.Write("<script>alert('Old Password is incorrect.');</script>")
                Return
            End If

            If newPass = oldPass Then
                Response.Write("<script>alert('New Password cannot be same as old password');</script>")
                Return
            End If

            cmd.Parameters.Clear()
            cmd = New SqlCommand("UPDATE users SET [password] = @newpassword WHERE [ID] = @id;", cn)
            cmd.Parameters.AddWithValue("@newpassword", newPass)
            cmd.Parameters.AddWithValue("@id", id)
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                Response.Write("<script>alert('Password updated successfully');</script>")
                Response.Redirect("Profile.aspx", False)
            Else
                MsgBox("Unknown Error happened" & cmd.CommandText, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub
End Class