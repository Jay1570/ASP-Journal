Imports System.Data.OleDb

Public Class SiteMaster
    Inherits MasterPage

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadNavBar()
        End If
    End Sub

    Private Sub LoadNavBar()
        Try
            Dim query As String = "SELECT ID, topicName FROM Topics"
            Dim cmd As New OleDbCommand(query, cn)
            cn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            Dim navHtml As String = String.Empty

            While reader.Read()
                Dim topicId As Integer = Convert.ToInt32(reader("ID"))
                Dim topicName As String = reader("topicName").ToString()
                navHtml &= $"<li><a href='Home.aspx?topicId={topicId}'>{topicName}</a></li>"
            End While

            NavBarContent.Text = navHtml
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub
End Class