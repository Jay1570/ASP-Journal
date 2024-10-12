Imports System.Data.OleDb
Imports Microsoft.Ajax.Utilities

Public Class Login
    Inherits System.Web.UI.Page


    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim email As String = txtEmail.Text
            Dim pass As String = txtPassword.Text

            If email.IsNullOrWhiteSpace() Or pass.IsNullOrWhiteSpace() Then
                MsgBox("All Fields are mandatory", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            Dim cmd As New OleDbCommand("SELECT ID FROM Users WHERE [email] = @email AND [password] = @password", cn)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@password", pass)
            cn.Open()
            Dim id As Integer
            If Integer.TryParse(cmd.ExecuteScalar, id) Then
                Dim userCookie As New HttpCookie("id")
                userCookie.Value = id
                userCookie.Expires = DateTime.Now.AddDays(1)
                Response.Cookies.Add(userCookie)
                Session("id") = id
                cn.Close()
                Response.Redirect("Default.aspx", False)
            Else
                cn.Close()
                MsgBox("Invalid email or password", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub
End Class