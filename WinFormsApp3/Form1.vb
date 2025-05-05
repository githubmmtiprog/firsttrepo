Imports System.Data.SqlClient


Public Class Form1


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim password As String = TextBox1.Text
        Dim username As String = TextBox2.Text


        Dim connectionString As String = "Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True"

        Using conn As New SqlConnection(connectionString)
            Dim query As String = "SELECT COUNT(*) FROM Users WHERE password = @password AND username = @username"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@password", password)
                cmd.Parameters.AddWithValue("@username", username)

                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If count > 0 Then
                    Show(Form2)
                Else
                    MessageBox.Show("Values do not exist in the database.")
                End If
            End Using
        End Using
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()


        If username = "" OrElse password = "" Then
            MessageBox.Show("Please enter both username and password.")
            Return
        End If


        Dim connectionString As String = "Data Source=localhost\sqlexpress;Initial Catalog=ojtPract_Rice;Integrated Security=True"

        Using conn As New SqlConnection(connectionString)
            conn.Open()


            Dim checkUserQuery As String = "SELECT COUNT(*) FROM Users WHERE Username = @username"
            Using checkCmd As New SqlCommand(checkUserQuery, conn)
                checkCmd.Parameters.AddWithValue("@username", username)

                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If count > 0 Then
                    MessageBox.Show("Username already exists.")
                    Return
                End If
            End Using


            Dim insertQuery As String = "INSERT INTO Users (Username, Password, rank) VALUES (@username, @password, 1)"
            Using insertCmd As New SqlCommand(insertQuery, conn)
                insertCmd.Parameters.AddWithValue("@username", username)
                insertCmd.Parameters.AddWithValue("@password", password)

                Dim rowsInserted As Integer = insertCmd.ExecuteNonQuery()
                If rowsInserted > 0 Then
                    MessageBox.Show("User registered successfully.")
                Else
                    MessageBox.Show("Registration failed.")
                End If
            End Using
        End Using
    End Sub


    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class
