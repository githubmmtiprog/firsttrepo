Imports System.Data.SqlClient

Public Class CreateForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connectionString As String = "Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True"
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Try
                Dim username As String = TextBox1.Text.Trim()
                Dim password As String = TextBox2.Text.Trim()
                Dim rank As Integer = ComboBox1.SelectedIndex
                If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
                    MessageBox.Show("Username and password are required.")
                    Return
                End If

                Dim checkUserQuery As String = "SELECT COUNT(*) FROM Users WHERE username = @username"
                Using checkCmd As New SqlCommand(checkUserQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", username)

                    Dim userExists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                    If userExists > 0 Then
                        MessageBox.Show("Username already exists. Please choose another.")
                        Return
                    End If
                End Using


                Dim insertQuery As String = "INSERT INTO Users (username, password, rank) VALUES (@username, @password, 1)"
                Using insertCmd As New SqlCommand(insertQuery, conn)
                    insertCmd.Parameters.AddWithValue("@username", username)
                    insertCmd.Parameters.AddWithValue("@password", password)

                    Dim rowsInserted As Integer = insertCmd.ExecuteNonQuery()
                    If rowsInserted > 0 Then
                        MessageBox.Show("User registered successfully.")
                        TextBox1.Text = ""
                        TextBox2.Text = ""
                    Else
                        MessageBox.Show("Registration failed.")
                    End If
                End Using
                MessageBox.Show("User created successfully.")
                Me.Close()

            Catch ex As Exception
                MessageBox.Show("Failed to create user: " & ex.Message)
            End Try
        End Using
    End Sub

    ' Private Sub CreateUserForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '  Me.UsersTableAdapter.Fill(Me.OjtPract_RiceDataSet.users)
    '     cboRank.Items.AddRange({"Regular", "Manager", "Admin"})
    '     cboRank.SelectedIndex = 0
    '   End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
