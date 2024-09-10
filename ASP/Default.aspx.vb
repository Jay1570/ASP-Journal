Imports System.Data.OleDb
Imports Microsoft.Ajax.Utilities

Public Class Login
    Inherits System.Web.UI.Page


    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("email") IsNot Nothing Then
            Response.Redirect("Home.aspx")
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim email As String = txtEmail.Text
            Dim pass As String = txtPassword.Text

            If email.IsNullOrWhiteSpace() Or pass.IsNullOrWhiteSpace() Then
                MsgBox("All Fields are mandatory", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            Dim cmd As New OleDbCommand("SELECT * FROM users WHERE [email] = @email AND [password] = @password", cn)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@password", pass)
            cn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                reader.Close()
                Dim emailCookie As New HttpCookie("email")
                emailCookie.Value = email
                emailCookie.Expires = DateTime.Now.AddMinutes(10)
                Response.Cookies.Add(emailCookie)
                Session("email") = email
                Response.Redirect("Home.aspx", False)
            Else
                reader.Close()
                MsgBox("Invalid email or password", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End If
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub
End Class