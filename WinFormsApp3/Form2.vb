Public Class Form2

    Private Sub BindingSource1_CurrentChanged(sender As Object, e As EventArgs) Handles BindingSource1.CurrentChanged
        Dim currentRowView As DataRowView = TryCast(BindingSource1.Current, DataRowView)

        If currentRowView IsNot Nothing Then

            Dim id As Integer = CInt(currentRowView("ID"))
            Dim name As String = currentRowView("Name").ToString()

            MessageBox.Show("Selected ID: " & id & vbCrLf & "Name: " & name)
        End If
    End Sub

End Class