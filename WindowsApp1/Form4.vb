Imports System.Data.SqlClient
Imports WindowsApp1.ojtPract_RiceDataSetTableAdapters
Imports WindowsApp1.DataSet1TableAdapters

Public Class EditUserForm
    Public Property UserID As Integer

    Private usersAdapter As New users1TableAdapter()
    Private dataSet As New ojtPract_RiceDataSet()

    Private Sub EditUserForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load user data via direct SQL
        Dim connectionString As String = "Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True"

        Using conn As New SqlConnection(connectionString)
            Dim query As String = "SELECT username, password FROM Users WHERE ID = @UserId"

            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@UserId", UserID)


                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox1.Text = reader("username").ToString()
                        TextBox2.Text = reader("password").ToString()
                    Else
                        MessageBox.Show("User not found.")
                    End If
                End Using
            End Using
        End Using

        ' Optionally: load roles into ComboBox1 here
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Input validation
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Username cannot be empty.")
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Email cannot be empty.")
            Return
        End If

        If ComboBox1.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a role.")
            Return
        End If

        Dim connectionString As String = "Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True"

        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "
                UPDATE Users
                SET username = @Username,
                    email = @Email,
                    role = @Role
                WHERE ID = @UserID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Username", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@Email", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@Role", Convert.ToInt32(ComboBox1.SelectedValue))
                    cmd.Parameters.AddWithValue("@UserID", UserID)

                    conn.Open()
                    Dim rowsAffected = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("User updated successfully.")
                        Me.Close()
                    Else
                        MessageBox.Show("User not found.")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to update user: " & ex.Message)
        End Try
    End Sub


End Class
