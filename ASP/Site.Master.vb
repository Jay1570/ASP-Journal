Imports System.Data.SqlClient

Public Class SiteMaster
    Inherits MasterPage

    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("Journal").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadNavBar()
        End If
        If Request.Cookies("id") IsNot Nothing And Session("id") IsNot Nothing Then
            ManageTopics.Visible = True
            AddTopic.Visible = True
            LoginOrProfile.HRef = "Profile.aspx"
            LoginOrProfile.InnerText = "Profile"
        Else
            AddTopic.Visible = False
            ManageTopics.Visible = False
            LoginOrProfile.HRef = "Login.aspx"
        End If
    End Sub

    Private Sub LoadNavBar()
        Try
            Dim query As String = "Select ID, topicName FROM Topics ORDER BY ID"
            Dim cmd As New SqlCommand(query, cn)
            cn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            Dim navHtml As String = "<div Class='scroll-container'><ul>"

            While reader.Read()
                Dim topicId As Integer = Convert.ToInt32(reader("ID"))
                Dim topicName As String = reader("topicName").ToString()
                navHtml &= $"<li><a href='Default.aspx?topicId={topicId}'>{topicName}</a></li>"
            End While
            navHtml &= "</ul></div>"
            NavBarContent.Text = navHtml
            cn.Close()
        Catch ex As Exception
            Response.Write("<script>alert('Error :- " & ex.Message & "');</script>")
            cn.Close()
        End Try
    End Sub

    Public Sub RefreshNavBar()
        LoadNavBar()
    End Sub
End Class