Imports System.Data.OleDb
Imports Microsoft.Ajax.Utilities

Public Class Profile
    Inherits System.Web.UI.Page

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        Dim email As String = Session("email").ToString()
        Dim cmd As New OleDbCommand("SELECT name, email FROM Users WHERE [email] = @email", cn)
        cmd.Parameters.AddWithValue("@email", email)
        cn.Open()
        Dim reader = cmd.ExecuteReader()
        If reader.Read() Then
            txtName.Text = reader("name").ToString()
            txtEmail.Text = reader("email").ToString()
        End If
        reader.Close()
        cn.Close()
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
            Dim email As String = txtEmail.Text
            Dim name As String = txtName.Text

            If email.IsNullOrWhiteSpace() Or name.IsNullOrWhiteSpace() Then
                MsgBox("All Fields are mandatory", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return
            End If

            Dim cmd As New OleDbCommand("UPDATE Users SET [name] = @name, [email] = @email WHERE [email] = @oldEmail", cn)
            cmd.Parameters.AddWithValue("@name", name)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@oldEmail", Session("email").ToString())
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                Session("email") = email
                Dim emailCookie As New HttpCookie("email")
                emailCookie.Value = email
                emailCookie.Expires = DateTime.Now.AddMinutes(10)
                Response.Cookies.Add(emailCookie)
                MsgBox("Profile Updated Successfully", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub

    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to sign out?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
        If result = MsgBoxResult.Yes Then
            Session.Clear()
            Session.Abandon()
            Dim emailCookie As New HttpCookie("email")
            emailCookie.Expires = DateTime.Now.AddMinutes(-1)
            Response.Cookies.Add(emailCookie)
            MsgBox("Signed Out Successfully", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Response.Redirect("Default.aspx")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to Delete your Account?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
        If result = MsgBoxResult.Yes Then
            Dim cmd As New OleDbCommand("DELETE FROM Users WHERE [email] = @email", cn)
            cmd.Parameters.AddWithValue("@email", Session("email").ToString())
            cn.Open()
            Dim execute As Integer = cmd.ExecuteNonQuery()
            cn.Close()
            If execute > 0 Then
                Session.Clear()
                Session.Abandon()
                Dim emailCookie As New HttpCookie("email")
                emailCookie.Expires = DateTime.Now.AddMinutes(-1)
                Response.Cookies.Add(emailCookie)
                MsgBox("Profile Deleted Successfully", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub
End Class