Imports System.Data.OleDb

Public Class SiteMaster
    Inherits MasterPage

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\journal.accdb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadNavBar()
        End If
        If Request.Cookies("email") IsNot Nothing Then
            LoginOrProfile.HRef = "Profile.aspx"
            LoginOrProfile.InnerText = "Profile"
        Else
            LoginOrProfile.HRef = "Login.aspx"
        End If
    End Sub

    Private Sub LoadNavBar()
        Try
            Dim query As String = "Select ID, topicName FROM Topics ORDER BY ID"
            Dim cmd As New OleDbCommand(query, cn)
            cn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            Dim navHtml As String = "<div Class='scroll-container'><ul>"

            While reader.Read()
                Dim topicId As Integer = Convert.ToInt32(reader("ID"))
                Dim topicName As String = reader("topicName").ToString()
                navHtml &= $"<li><a href='Home.aspx?topicId={topicId}'>{topicName}</a></li>"
            End While
            navHtml &= "</ul></div>"
            NavBarContent.Text = navHtml
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            cn.Close()
        End Try
    End Sub
End Class