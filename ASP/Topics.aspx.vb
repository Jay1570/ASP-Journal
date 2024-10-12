Imports System.Data.OleDb
Public Class Topics
    Inherits System.Web.UI.Page

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        If Session("email") Is Nothing Then
            MsgBox("You are not logged in...", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Response.Redirect("Default.aspx")
            Return
        End If
        Dim topicId As Integer = 1
        If Integer.TryParse(Request.QueryString("topicId"), topicId) Then
            btnSave.Text = "Update"
            LoadTopicContent(topicId)
        Else
            btnSave.Text = "Add Topic"
        End If
    End Sub

    Private Sub LoadTopicContent(ByVal topicId As Integer)
        Try
            Dim cmd As New OleDbCommand
            Dim email = Session("email").ToString()
            cmd = New OleDbCommand("SELECT ID FROM Users WHERE email='" & email & "'", cn)
            cn.Open()
            Dim id = CInt(cmd.ExecuteScalar())
            cn.Close()
            cmd = New OleDbCommand("SELECT topicName,content,UserId FROM Topics WHERE ID=" & topicId & "AND UserId=" & id, cn)
            cn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                txtTitle.Text = reader("topicName").ToString()
                txtDescription.Text = reader("content").ToString()
            Else
                MsgBox("Topic not found Or You are not authorised to edit this topic", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Response.Redirect("Default.aspx", False)
            End If
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim topicId = Request.QueryString("topicId")
            Dim cmd As New OleDbCommand
            Dim email = Session("email").ToString()
            cmd = New OleDbCommand("SELECT ID FROM Users WHERE email='" & email & "'", cn)
            cn.Open()
            Dim id = CInt(cmd.ExecuteScalar())
            cn.Close()
            If btnSave.Text = "Add Topic" Then
                cmd = New OleDbCommand("INSERT INTO Topics (UserId,topicName,content) VALUES (?,?,?)", cn)
                cmd.Parameters.AddWithValue("UserId", id)
            Else
                cmd = New OleDbCommand("UPDATE Topics SET topicName=?,content=? WHERE ID=" & topicId, cn)
            End If
            cmd.Parameters.AddWithValue("topicName", txtTitle.Text)
            cmd.Parameters.AddWithValue("content", txtDescription.Text)
            cn.Open()
            cmd.ExecuteNonQuery()
            If btnSave.Text = "Add Topic" Then
                cmd.CommandText = "SELECT @@IDENTITY"
                topicId = CInt(cmd.ExecuteScalar())
            End If
            cn.Close()
                Response.Redirect("Default.aspx?topicId=" & topicId, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub
End Class