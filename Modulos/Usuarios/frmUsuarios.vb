Imports System.Data.SqlClient

Public Class frmUsuarios
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel4.Visible = False
        Mostrar()
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Panel4.Visible = True
        txtNombre.Clear()
        txtUsuario.Clear()
        txtContraseña.Clear()
        txtNombre.Focus()
    End Sub

    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarToolStripMenuItem.Click
        'Aseguramos el codifo con un Try Catch
        Try
            'En esta variable vamos a guardar un procedimienton de sql
            'Le agregamos new para que siempre se cree de cero
            Dim cmd As New SqlCommand
            abrir() 'Abrimos la base
            'le pasamos el procedimiento y abrimos la base
            cmd = New SqlCommand("dbo.insertar_usuario", conexion)
            cmd.CommandType = 4 'es importante
            'guardamos en @variable lo que este escrito el txtBox.text
            cmd.Parameters.AddWithValue("@nombres", txtNombre.Text)
            cmd.Parameters.AddWithValue("@login", txtUsuario.Text)
            cmd.Parameters.AddWithValue("@password", txtContraseña.Text)
            If txtNombre.Text = "" Or txtUsuario.Text = "" Or txtContraseña.Text = "" Then
                MessageBox.Show("Faltan compos por llenar", "Control de acceso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                cmd.ExecuteNonQuery() 'Ejecuta los parametros que se acaban de escribir
                cerrar() 'cerramos la conexion
                Mostrar()
                Panel4.Visible = False
            End If
        Catch ex As Exception : MsgBox(ex.Message)

        End Try
    End Sub

    Sub Mostrar()

        Dim dt As New DataTable 'variable para adaptar los datos a un datatable
        Dim da As SqlDataAdapter 'adaptador

        Try
            abrir() 'abrimos la base
            'Le pasamos el procedimiento es ,mysql para mostrar usuarios y abrimos la base
            da = New SqlDataAdapter("dbo.mostrar_usuario", conexion)
            'el adaptador va a ser insertado en un datatable
            da.Fill(dt)
            'DataListado es un DataGridView
            DataListado.DataSource = dt
            cerrar() 'cerramos la base
            MultiLinea(DataListado)
            DataListado.Columns(1).Visible = False
        Catch ex As Exception : MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub DataListado_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataListado.CellContentDoubleClick
        Try
            'Cuando demos dobleclick en el datalistado se nos mestra el panel4 
            'con los datos que le estamos indicando
            GuardarToolStripMenuItem.Visible = False
            GuardarCambiosToolStripMenuItem.Visible = True
            Panel4.Visible = True
            txtNombre.Text = DataListado.SelectedCells.Item(2).Value
            txtUsuario.Text = DataListado.SelectedCells.Item(3).Value
            txtContraseña.Text = DataListado.SelectedCells.Item(4).Value
            lblId.Text = DataListado.SelectedCells.Item(1).Value
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GuardarCambiosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarCambiosToolStripMenuItem.Click
        Try
            'En esta variable vamos a guardar un procedimienton de sql
            'Le agregamos new para que siempre se cree de cero
            Dim cmd As New SqlCommand
            abrir() 'Abrimos la base
            'le pasamos el procedimiento y abrimos la base
            cmd = New SqlCommand("dbo.editar_usuario", conexion)
            cmd.CommandType = 4 'es importante
            'guardamos en @variable lo que este escrito el txtBox.text
            cmd.Parameters.AddWithValue("idusuario", lblId.Text)
            cmd.Parameters.AddWithValue("@nombres", txtNombre.Text)
            cmd.Parameters.AddWithValue("@login", txtUsuario.Text)
            cmd.Parameters.AddWithValue("@password", txtContraseña.Text)
            cmd.ExecuteNonQuery() 'Ejecuta los parametros que se acaban de escribir
            cerrar() 'cerramos la conexion
            Mostrar()
            Panel4.Visible = False
        Catch ex As Exception : MsgBox(ex.Message)

        End Try
    End Sub
    'Cuando hacemos click en una celda
    Private Sub DataListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataListado.CellClick
        'cuando se hace click en la columna que se llama "Eli"
        If e.ColumnIndex = Me.DataListado.Columns.Item("Eli").Index Then
            Dim result As DialogResult
            result = MessageBox.Show("¿Realmente desea eliminar a este usuario?", "Eliminando Registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            'Si el usuario iso click en okey respecto al mensaje de arriba
            If result = DialogResult.OK Then
                Try
                    'En esta variable vamos a guardar un procedimienton de sql
                    'Le agregamos new para que siempre se cree de cero
                    Dim cmd As New SqlCommand
                    abrir() 'Abrimos la base
                    'le pasamos el procedimiento y abrimos la base
                    cmd = New SqlCommand("dbo.eliminar_usuario", conexion)
                    cmd.CommandType = 4 'es importante
                    'guardamos en @variable lo que este escrito el txtBox.text
                    cmd.Parameters.AddWithValue("idusuario", DataListado.SelectedCells.Item(1).Value)
                    cmd.ExecuteNonQuery() 'Ejecuta los parametros que se acaban de escribir
                    cerrar() 'cerramos la conexion
                    Mostrar()
                Catch ex As Exception : MsgBox(ex.Message)

                End Try
            Else
                MessageBox.Show("Cancelado eliminacion de registros", "Eliminando Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Sub Buscar()
        Dim dt As New DataTable 'variable para adaptar los datos a un datatable
        Dim da As SqlDataAdapter 'adaptador

        Try
            abrir() 'abrimos la base
            'Le pasamos el procedimiento es ,mysql para mostrar usuarios y abrimos la base
            da = New SqlDataAdapter("dbo.buscar_usuario", conexion)
            'parametro
            da.SelectCommand.CommandType = 4
            da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscar.Text)

            'el adaptador va a ser insertado en un datatable
            da.Fill(dt)
            'DataListado es un DataGridView
            DataListado.DataSource = dt
            cerrar() 'cerramos la base
        Catch ex As Exception : MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Buscar()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel4.Visible = False
    End Sub
End Class