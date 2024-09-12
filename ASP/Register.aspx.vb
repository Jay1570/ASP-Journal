Imports System.Data.OleDb
Imports Microsoft.Ajax.Utilities

Public Class Register
    Inherits System.Web.UI.Page

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Try
            Dim name As String = txtName.Text
            Dim password As String = txtPassword.Text
            Dim email As String = txtEmail.Text
            Dim confirmPassword As String = txtConfirmPassword.Text

            If password.IsNullOrWhiteSpace() Or email.IsNullOrWhiteSpace() Or name.IsNullOrWhiteSpace() Or confirmPassword.IsNullOrWhiteSpace Then
                MsgBox("All Fields are mandatory", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            If password <> confirmPassword Then
                MsgBox("Passwords do not match", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            Dim cmd As New OleDbCommand("SELECT COUNT(ID) FROM Users WHERE [email]=@email", cn)
            cmd.Parameters.AddWithValue("@email", email)
            cn.Open()
            Dim alreadyTaken = CInt(cmd.ExecuteScalar())
            cn.Close()

            If alreadyTaken > 0 Then
                MsgBox("Email has been registered already ", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            cmd = New OleDbCommand("INSERT INTO Users ([name],[password], [email]) VALUES (@name,@password, @email)", cn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@name", name)
            cmd.Parameters.AddWithValue("@password", password)
            cmd.Parameters.AddWithValue("@email", email)
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

            Dim emailCookie As New HttpCookie("email")
            emailCookie.Value = email
            emailCookie.Expires = DateTime.Now.AddMinutes(10)
            Response.Cookies.Add(emailCookie)

            Session("email") = email
            Response.Redirect("Default.aspx", False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub

End Class