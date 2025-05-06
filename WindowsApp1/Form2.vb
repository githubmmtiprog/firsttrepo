Public Class Form2
    Public Property UserRank As Integer


    Private Const RankRegularUser As Integer = 1
    Private Const RankManager As Integer = 2
    Private Const RankAdmin As Integer = 3

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
        Catch ex As Exception
            MessageBox.Show("Failed to load user data: " & ex.Message)
        End Try


        Select Case UserRank
            Case RankRegularUser

                Button3.Visible = False
                Button1.Visible = False
                Button2.Visible = False
                Button5.Visible = True
                MessageBox.Show("Welcome, Regular User.")

            Case RankManager

                Button3.Visible = False
                Button2.Visible = False
                Button1.Visible = True
                Button5.Visible = True
                MessageBox.Show("Welcome, Manager.")

            Case Else

                Button1.Visible = True
                Button2.Visible = True
                Button3.Visible = True
                Button5.Visible = True
                MessageBox.Show("Welcome, Admin.")

        End Select
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim newUserForm As New CreateForm() 'This
        newUserForm.ShowDialog()
        
        Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Using selectedRow = DataGridView1.CurrentRow
            If selectedRow IsNot Nothing Then
                Dim editForm As New EditUserForm With {
                     .UserID = selectedRow.Cells("UserID").Value
                 } 'then this
                editForm.ShowDialog()
                Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
            Else
                MessageBox.Show("Select a user to edit.")
            End If
        End Using
    End Sub

    ' Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '  And then the query
    '    Dim selectedRow = DataGridView1.CurrentRow
    '    If selectedRow IsNot Nothing Then
    '    Dim userId = selectedRow.Cells("UserID").Value
    '    If MessageBox.Show("Are you sure you want to delete this user?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '                UsersTableAdapter.DeleteQuery(userId)
    '    Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
    '   End If
    '   Else
    '           MessageBox.Show("Select a user to delete.")
    '    End If

    '    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Try
            Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
            MessageBox.Show("User data refreshed.")
        Catch ex As Exception
            MessageBox.Show("Failed to update user data: " & ex.Message)
        End Try
    End Sub
End Class
