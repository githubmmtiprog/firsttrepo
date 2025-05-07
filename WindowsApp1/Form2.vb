Imports System.Data.SqlClient
Imports System.Net
Imports WindowsApp1.ojtPract_RiceDataSetTableAdapters



Public Class Form2
    Public Property UserRank As Integer
    Dim log As Int16

    Private Const RankRegularUser As Integer = 1
    Private Const RankManager As Integer = 2
    Private Const RankAdmin As Integer = 3

    Private connectionString As String = "Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True"

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DataSet1.user_rank' table. You can move, or remove it, as needed.
        Me.User_rankTableAdapter.Fill(Me.DataSet1.user_rank)
        ReloadForm(UserRank)
    End Sub

    Private Sub ReloadForm(mode As Int16)
        If (mode = 0) Then
            Button1.Visible = False
            Button2.Visible = True
            Button3.Visible = False
            Button5.Visible = False
            Button6.Visible = True
            Button7.Visible = True
        Else
            Try
                Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
                ApplyUserPermissions()
            Catch ex As Exception
                MessageBox.Show("Failed to load user data: " & ex.Message)
            End Try

        End If
    End Sub

    Private Sub ApplyUserPermissions()
        Select Case UserRank
            Case RankRegularUser
                Button1.Visible = False
                Button2.Visible = False
                Button3.Visible = False
                Button5.Visible = True
                If (log = 0) Then
                    MessageBox.Show("Welcome, Regular User.")
                    log = +1
                End If

            Case RankManager
                Button1.Visible = True
                Button2.Visible = False
                Button3.Visible = False
                Button5.Visible = True
                If (log = 0) Then
                    MessageBox.Show("Welcome, Manager.")
                    log = +1
                End If

            Case Else ' Admin
                Button1.Visible = True
                Button2.Visible = True
                Button3.Visible = True
                Button5.Visible = True
                If (log = 0) Then
                    MessageBox.Show("Welcome, Admin.")
                    log = +1
                End If

        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Create 
        Dim newUserForm As New CreateForm()
        newUserForm.ShowDialog()
        ReloadForm(UserRank)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Delete 
        Dim selectedRow = DataGridView1.CurrentRow
        If selectedRow IsNot Nothing Then
            Dim userId = selectedRow.Cells("ID").Value
            If MessageBox.Show("Are you sure you want to delete this user?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Dim deleteQuery As String = "DELETE FROM Users WHERE ID = @ID"
                    Using deleteCmd As New SqlCommand(deleteQuery, conn)
                        deleteCmd.Parameters.AddWithValue("@ID", userId)
                        deleteCmd.ExecuteNonQuery()
                    End Using
                End Using
                ReloadForm(UserRank)
            End If
        Else
            MessageBox.Show("Select a user to delete.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Edit mode +__+
        ReloadForm(0)



    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Logout
        log = -1
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Refresh 
        Try
            Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
            MessageBox.Show("User data refreshed.")
        Catch ex As Exception
            MessageBox.Show("Failed to update user data: " & ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        ' Convert 
        Dim dt As New DataTable()
        For Each col As DataGridViewColumn In DataGridView1.Columns
            dt.Columns.Add(col.Name)
        Next

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                Dim dr As DataRow = dt.NewRow()
                For Each col As DataGridViewColumn In DataGridView1.Columns
                    dr(col.Name) = row.Cells(col.Name).Value
                Next
                dt.Rows.Add(dr)
            End If
        Next

        ' Bulk cuz im lazy
        Using conn As New SqlConnection("Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True")
            conn.Open()
            Using transaction = conn.BeginTransaction()
                Try
                    Dim deleteCmd As New SqlCommand("DELETE FROM users", conn, transaction)
                    deleteCmd.ExecuteNonQuery()

                    Dim reseedCmd As New SqlCommand("DBCC CHECKIDENT ('users', RESEED, 0)", conn, transaction)
                    reseedCmd.ExecuteNonQuery()

                    Using bulkCopy As New SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction)
                        bulkCopy.DestinationTableName = "users"
                        bulkCopy.WriteToServer(dt)
                    End Using

                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Transaction error no Dupplicates Please")


                End Try
            End Using
        End Using



        'save
        Me.Controls.Clear()
        InitializeComponent()
        ReloadForm(UserRank)

    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'cancel
        Me.Controls.Clear()
        InitializeComponent()
        ReloadForm(UserRank)
    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved

    End Sub
End Class
