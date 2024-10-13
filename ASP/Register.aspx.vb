Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities

Public Class Register
    Inherits System.Web.UI.Page

    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Journal").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Try
            Dim name As String = txtName.Text.Trim()
            Dim password As String = txtPassword.Text.Trim()
            Dim email As String = txtEmail.Text.Trim()
            Dim confirmPassword As String = txtConfirmPassword.Text.Trim()

            If password.IsNullOrWhiteSpace() Or email.IsNullOrWhiteSpace() Or name.IsNullOrWhiteSpace() Or confirmPassword.IsNullOrWhiteSpace Then
                Response.Write("<script>alert('All Fields are mandatory.');</script>")
                Return
            End If

            If password <> confirmPassword Then
                Response.Write("<script>alert('Password do not match.');</script>")
                Return
            End If

            Dim cmd As New SqlCommand("SELECT COUNT(ID) FROM Users WHERE [email]=@email", cn)
            cmd.Parameters.AddWithValue("@email", email)
            cn.Open()
            Dim alreadyTaken = CInt(cmd.ExecuteScalar())
            cn.Close()

            If alreadyTaken > 0 Then
                Response.Write("<script>alert('Email has been registered already.');</script>")
                Return
            End If

            cmd = New SqlCommand("INSERT INTO Users ([name],[password], [email]) VALUES (@name,@password, @email)", cn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@name", name)
            cmd.Parameters.AddWithValue("@password", password)
            cmd.Parameters.AddWithValue("@email", email)
            cn.Open()
            cmd.ExecuteNonQuery()
            cmd.CommandText = "SELECT @@IDENTITY"
            Dim id As Integer = CInt(cmd.ExecuteScalar())
            cn.Close()

            Dim userCookie As New HttpCookie("id")
            userCookie.Value = id
            userCookie.Expires = DateTime.Now.AddDays(10)
            Response.Cookies.Add(userCookie)

            Session("id") = id
            Response.Redirect("Default.aspx", False)
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

End Class