Imports System.Data.SqlClient

Public Class frmLogin
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub txtLogin_Click(sender As Object, e As EventArgs) Handles txtLogin.Click
        'Cuando le demos click se borra lo que este escrito
        txtLogin.Clear()
        'Le damos el enfoque
        txtLogin.Focus()
    End Sub
    Private Sub txtPassword_Click(sender As Object, e As EventArgs) Handles txtPassword.Click
        txtPassword.Clear()
        txtPassword.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CargarUsuarios()
        If DataListado.RowCount > 0 Then 'si si encontro algun dato quita el formulario
            Dispose()
            frmMenuPrincipalMatriculas.ShowDialog()
        Else
            MessageBox.Show("Verifica tu usuario y contraseña.", "Control de acceso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Sub CargarUsuarios()
        Dim dt As New DataTable 'para mostrar los datos en un datagridview
        Dim da As SqlDataAdapter 'adapta los datos para que los podamos ver
        Try
            abrir() 'Abrimos la base de datos
            da = New SqlDataAdapter("validar_usuario", conexion) 'llamamos al proceso que vamos a usar y a la conexion
            da.SelectCommand.CommandType = CommandType.StoredProcedure ' ponemos eso o el numero 4
            da.SelectCommand.Parameters.AddWithValue("@Login", txtLogin.Text) 'los parametros son las variables que vamos a usar
            da.SelectCommand.Parameters.AddWithValue("@Password", txtPassword.Text)
            da.Fill(dt) 'le pasamos los datos a la tabla de datos
            DataListado.DataSource = dt 'datasource para preguntarle de donde le traemos los datos
            cerrar()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtLogin_TextChanged(sender As Object, e As EventArgs) Handles txtLogin.TextChanged

    End Sub
End Class
