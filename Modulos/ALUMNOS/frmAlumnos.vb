Imports System.Data.SqlClient

Public Class frmAlumnos
    Dim IdAlumno As Integer = 0
    Dim estado As String = ""
    Private Sub frmAlumnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Mostrar()
        Panel4.Visible = False
        txtBuscar.Text = "Buscar Alumno..."
    End Sub

    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarToolStripMenuItem.Click

        'Aseguramos el codifo con un Try Catch
        Try
            'En esta variable vamos a guardar un procedimienton de sql
            'Le agregamos new para que siempre se cree de cero
            Dim cmd As New SqlCommand

            abrir() 'Abrimos la base
            'le pasamos el procedimiento y abrimos la base
            cmd = New SqlCommand("dbo.insertar_alumno", conexion)

            cmd.CommandType = CommandType.StoredProcedure 'es importante
            'guardamos en @variable lo que este escrito el txtBox.text
            cmd.Parameters.AddWithValue("@Apellido_Paterno", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Apellido_Materno", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Nombres", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Fecha_Nacimiento", txtDocumento.Text)
            cmd.Parameters.AddWithValue("@Nro_Documento", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Estado_Civil", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Sexo", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Nacionalidad", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Telefono", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Departamento", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Provincia", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Distrito", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Direccion", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Departamento2", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Provincia2", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Distrito2", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Telefono1", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Telefono2", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Correo", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Profesion", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Local_Studio", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Fecha_Matricula", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Fecha_Inicio", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Codigo", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Estado", "ACTIVO")

            If txtApellidoPaterno.Text = "" Or txtApellidoMaterno.Text = "" Or txtNombres.Text = "" Or txtDocumento.Text = "" Then
                MessageBox.Show("Faltan compos por llenar", "Control de acceso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                cmd.ExecuteNonQuery() 'Ejecuta los parametros que se acaban de escribir
                cerrar() 'cerramos la conexion
                Mostrar()
                cmd.Parameters.Clear()
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
            da = New SqlDataAdapter("dbo.mostrar_alumno", conexion)
            'el adaptador va a ser insertado en un datatable
            da.Fill(dt)
            'DataListado es un DataGridView
            DataListado.DataSource = dt
            cerrar() 'cerramos la base
            MultiLinea(DataListado)
            DataListado.Columns(1).Visible = True
        Catch ex As Exception : MsgBox(ex.Message)

        End Try

        For Each row As DataGridViewRow In DataListado.Rows
            If row.Cells("Estado").Value = "BAJA" Then
                row.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Strikeout Or FontStyle.Bold)
                row.DefaultCellStyle.ForeColor = Color.Red
            End If
        Next

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Panel4.Visible = True
        txtApellidoMaterno.Clear()
        txtApellidoPaterno.Clear()
        txtNombres.Clear()
        txtDocumento.Clear()
        GuardarToolStripMenuItem.Visible = True
        GuardarCambiosToolStripMenuItem.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel4.Visible = False
    End Sub

    Private Sub GuardarCambiosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarCambiosToolStripMenuItem.Click
        Try
            'En esta variable vamos a guardar un procedimienton de sql
            'Le agregamos new para que siempre se cree de cero
            Dim cmd As New SqlCommand
            abrir() 'Abrimos la base
            'le pasamos el procedimiento y abrimos la base
            cmd = New SqlCommand("dbo.editar_alumno", conexion)
            cmd.CommandType = 4 'es importante
            'guardamos en @variable lo que este escrito el txtBox.text
            cmd.Parameters.AddWithValue("@Apellido_Paterno", txtApellidoPaterno.Text)
            cmd.Parameters.AddWithValue("@Apellido_Materno", txtApellidoMaterno.Text)
            cmd.Parameters.AddWithValue("@Nombres", txtNombres.Text)
            cmd.Parameters.AddWithValue("@Nro_Documento", txtDocumento.Text)
            cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno)
            If txtApellidoPaterno.Text = "" Or txtApellidoMaterno.Text = "" Or txtNombres.Text = "" Or txtDocumento.Text = "" Then
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

    Private Sub DataListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataListado.CellDoubleClick
        GuardarToolStripMenuItem.Visible = False
        GuardarCambiosToolStripMenuItem.Visible = True
        Try

            IdAlumno = DataListado.SelectedCells.Item(1).Value
            txtApellidoPaterno.Text = DataListado.SelectedCells.Item(2).Value
            txtApellidoMaterno.Text = DataListado.SelectedCells.Item(3).Value
            txtNombres.Text = DataListado.SelectedCells.Item(4).Value
            txtDocumento.Text = DataListado.SelectedCells.Item(5).Value
            estado = DataListado.SelectedCells.Item(6).Value
            If estado = "BAJA" Then
                restaurarAlumno()
            Else
                Panel4.Visible = True
            End If
        Catch ex As Exception : MsgBox(ex.Message)

        End Try
    End Sub

    Sub restaurarAlumno()
        Dim result As DialogResult
        result = MessageBox.Show("Este Alumno se elimino, ¿Desea volver a darlo de ALTA?", "Alumnos BAJA", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Dim cmd As SqlCommand
            Try
                'Recorre todo el DataListado
                For Each row As DataGridViewRow In DataListado.SelectedRows
                    Dim IdAlumnoSeleccionado As Integer = Convert.ToInt32(row.Cells("IdAlumno").Value)
                    Try
                        abrir()
                        cmd = New SqlCommand("restaurar_alumno", conexion)
                        cmd.CommandType = 4
                        cmd.Parameters.AddWithValue("@IdAlumno", IdAlumnoSeleccionado)
                        cmd.ExecuteNonQuery()
                        cerrar()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                Next

                Call Mostrar()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub DataListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataListado.CellClick
        'Si la click en columna que se llama Eli
        If e.ColumnIndex = Me.DataListado.Columns.Item("Eli").Index Then
            Dim resultado As DialogResult 'Para guardar lo que se selecciona en un MessageBox
            'resultado va a ser igual a lo que seleccionemos aqui
            resultado = MessageBox.Show("¿Deseas eliminar este Alumno?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            'Si el usuario da click en ok en el mensaje que se le mostrara
            If resultado = DialogResult.OK Then
                Dim cmd As SqlCommand
                Try
                    'Recorre todo el DataListado
                    For Each row As DataGridViewRow In DataListado.SelectedRows
                        Dim IdAlumnoSeleccionado As Integer = Convert.ToInt32(row.Cells("IdAlumno").Value)
                        Try
                            abrir()
                            cmd = New SqlCommand("eliminar_alumno", conexion)
                            cmd.CommandType = 4
                            cmd.Parameters.AddWithValue("@IdAlumno", IdAlumnoSeleccionado)
                            cmd.ExecuteNonQuery()
                            cerrar()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    Next
                    Call Mostrar()
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub RestaurarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestaurarToolStripMenuItem.Click
        Try
            IdAlumno = DataListado.SelectedCells.Item(1).Value
            txtApellidoPaterno.Text = DataListado.SelectedCells.Item(2).Value
            txtApellidoMaterno.Text = DataListado.SelectedCells.Item(3).Value
            txtNombres.Text = DataListado.SelectedCells.Item(4).Value
            txtDocumento.Text = DataListado.SelectedCells.Item(5).Value
            estado = DataListado.SelectedCells.Item(6).Value
            If estado = "BAJA" Then
                restaurarAlumno()
            End If
        Catch ex As Exception : MsgBox(ex.Message)

        End Try
    End Sub

    Sub Buscar()
        Dim dt As New DataTable 'variable para adaptar los datos a un datatable
        Dim da As SqlDataAdapter 'adaptador

        Try
            abrir() 'abrimos la base
            'Le pasamos el procedimiento es ,mysql para mostrar usuarios y abrimos la base
            da = New SqlDataAdapter("dbo.buscar_alumno", conexion)
            'parametro
            da.SelectCommand.CommandType = 4
            da.SelectCommand.Parameters.AddWithValue("@letra", txtBuscar.Text)

            'el adaptador va a ser insertado en un datatable
            da.Fill(dt)
            'DataListado es un DataGridView
            DataListado.DataSource = dt
            cerrar() 'cerramos la base

            MultiLinea(DataListado)
            DataListado.Columns(1).Visible = True

            For Each row As DataGridViewRow In DataListado.Rows
                If row.Cells("Estado").Value = "BAJA" Then
                    row.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Strikeout Or FontStyle.Bold)
                    row.DefaultCellStyle.ForeColor = Color.Red
                End If
            Next


        Catch ex As Exception

        End Try

    End Sub
    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If txtBuscar.Text <> "" And txtBuscar.Text <> "Buscar Alumno..." Then
            Buscar()
        ElseIf txtBuscar.Text = "" Then
            Mostrar()
        End If
    End Sub

    Private Sub txtBuscar_Click(sender As Object, e As EventArgs) Handles txtBuscar.Click
        txtBuscar.SelectAll()
    End Sub
End Class