Imports System.Data.SqlClient
Imports Microsoft.Ajax.Utilities

Public Class Login
    Inherits System.Web.UI.Page


    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Journal").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim email As String = txtEmail.Text.Trim()
            Dim pass As String = txtPassword.Text.Trim()

            If email.IsNullOrWhiteSpace() Or pass.IsNullOrWhiteSpace() Then
                Response.Write("<script>alert('All Fields are mandatory.');</script>")
                Return
            End If

            Dim cmd As New SqlCommand("SELECT ID FROM Users WHERE [email] = @email AND [password] = @password", cn)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@password", pass)
            cn.Open()
            Dim id As Object = cmd.ExecuteScalar()
            If id IsNot Nothing Then
                Dim userId = Convert.ToInt32(id)
                Dim userCookie As New HttpCookie("id")
                userCookie.Value = userId
                userCookie.Expires = DateTime.Now.AddDays(1)
                Response.Cookies.Add(userCookie)
                Session("id") = userId
                cn.Close()
                Response.Redirect("Default.aspx", False)
            Else
                cn.Close()
                Response.Write("<script>alert('Invalid email or password.');</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub
End Class