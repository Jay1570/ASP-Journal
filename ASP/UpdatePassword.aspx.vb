Imports System.Data.OleDb
Imports Microsoft.Ajax.Utilities

Public Class UpdatePassword
    Inherits System.Web.UI.Page

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("email") Is Nothing Then
            MsgBox("You are not logged in...", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Response.Redirect("Default.aspx")
        End If
    End Sub

    Private Sub btnUpdatePass_Click(sender As Object, e As EventArgs) Handles btnUpdatePass.Click
        Try
            Dim email As String = Session("email").ToString()
            Dim oldPass As String = txtOldPass.Text
            Dim newPass As String = txtNewPass.Text
            Dim confirmPass As String = txtConfirmPassword.Text

            If oldPass.IsNullOrWhiteSpace() Or newPass.IsNullOrWhiteSpace() Or confirmPass.IsNullOrWhiteSpace() Then
                MsgBox("All Fields are mandatory", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            If newPass <> confirmPass Then
                MsgBox("New Password and Confirm Password do not match", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            Dim cmd As New OleDbCommand("SELECT COUNT(ID) FROM Users WHERE [email] = @email AND [password] = @password", cn)

            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@password", oldPass)
            cn.Open()
            Dim rowCount = CInt(cmd.ExecuteScalar())
            cn.Close()

            If rowCount = 0 Then
                MsgBox("Old password is incorrect...", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            If newPass = oldPass Then
                MsgBox("New password cannot be the same as the old password...", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            cmd.Parameters.Clear()
            cmd = New OleDbCommand("UPDATE users SET [password] = @newpassword WHERE [email] = @email;", cn)
            cmd.Parameters.AddWithValue("@newpassword", newPass)
            cmd.Parameters.AddWithValue("@email", email)
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                MsgBox("Password Updated Successfully", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                Response.Redirect("Profile.aspx", False)
            Else
                MsgBox("Unknown Error happened" & cmd.CommandText, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        End Try
    End Sub

End Class