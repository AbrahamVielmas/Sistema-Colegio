Imports System.Data.SqlClient

Public Class frmSerializacion
    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarToolStripMenuItem.Click
        Try
            Dim cmd As SqlCommand 'Para usar los procedimientos
            abrir()
            cmd = New SqlCommand("insertar_serializacion", conexion)
            cmd.CommandType = 4
            cmd.Parameters.AddWithValue("Serie", txtSerie.Text)
            cmd.Parameters.AddWithValue("NumeroInicio", txtNroInicio.Text)
            cmd.Parameters.AddWithValue("NumeroFin", txtNroFin.Text)
            cmd.Parameters.AddWithValue("TipoComprobante", txtComprobante.Text)
            cmd.ExecuteNonQuery()
            ConexionMaestra.cerrar()
            Mostrar()
            Panel2.Visible = False
        Catch ex As Exception

        End Try

    End Sub
    Sub Mostrar()
        Dim dt As New DataTable
        Dim da As SqlDataAdapter
        Try
            abrir()
            da = New SqlDataAdapter("dbo.mostrar_serializacion_Tipo_Documento_Insertar_Estos_Mismos", conexion)
            da.Fill(dt)
            DataListado.DataSource = dt
            ConexionMaestra.cerrar()
        Catch ex As Exception
            MsgBox(ex.Message, MessageBoxIcon.Error)
        End Try

        Try
            DataListado.Columns(4).Visible = False
        Catch ex As Exception

        End Try
        MultiLinea(DataListado)
    End Sub

    Private Sub GuardarCambiosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarCambiosToolStripMenuItem.Click
        Try
            Dim cmd As New SqlCommand
            abrir()
            cmd = New SqlCommand("editar_serializacion", conexion)
            cmd.CommandType = 4
            cmd.Parameters.AddWithValue("@Serie", txtSerie.Text)
            cmd.Parameters.AddWithValue("@NumeroInicio", txtNroInicio.Text)
            cmd.Parameters.AddWithValue("@NumeroFin", txtNroFin.Text)
            cmd.Parameters.AddWithValue("@TipoComprobante", txtComprobante.Text)
            cmd.Parameters.AddWithValue("@IdSerializacion", DataListado.SelectedCells.Item(4).Value)
            cmd.ExecuteNonQuery()
            ConexionMaestra.cerrar()
            Mostrar()
            Panel2.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Panel2.Visible = True
        GuardarCambiosToolStripMenuItem.Visible = False
        GuardarToolStripMenuItem.Visible = True
        Limpiar()
    End Sub

    Private Sub VolverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VolverToolStripMenuItem.Click
        Panel2.Visible = False
    End Sub

    Private Sub frmSerializacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Mostrar()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Sub Limpiar()
        txtComprobante.Clear()
        txtNroInicio.Clear()
        txtNroFin.Clear()
        txtSerie.Clear()
        txtComprobante.Focus()
    End Sub

    Private Sub DataListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataListado.CellDoubleClick
        Panel2.Visible = True
        Try
            txtComprobante.Text = DataListado.SelectedCells.Item(6).Value
            txtNroInicio.Text = DataListado.SelectedCells.Item(2).Value
            txtNroFin.Text = DataListado.SelectedCells.Item(3).Value
            txtSerie.Text = DataListado.SelectedCells.Item(1).Value
            GuardarCambiosToolStripMenuItem.Visible = True
            GuardarToolStripMenuItem.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cerrar_Click(sender As Object, e As EventArgs) Handles Cerrar.Click
        Close()
        Dispose()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim result As DialogResult
        Dim cmd As SqlCommand
        result = MessageBox.Show("¿Realmente desea eliminar los registros seleccionados?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In DataListado.SelectedRows
                    Dim onekey As Integer = Convert.ToInt32(row.Cells("IdSerializacion").Value)
                    Try
                        conexion.Open()
                        cmd = New SqlCommand("eliminar_serializacion", conexion)
                        cmd.CommandType = 4
                        cmd.Parameters.AddWithValue("IdSerializacion", onekey)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    conexion.Close()
                Next
                Mostrar()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Cancelando eliminacion de registros", "Eliminando Registros", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub txtNroInicio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNroInicio.KeyPress
        NumerosyDecimales(txtNroInicio, e)
    End Sub

    Public Sub NumerosyDecimales(ByVal cajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." And Not cajaTexto.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "," Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = True
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtNroFin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNroFin.KeyPress
        NumerosyDecimales(txtNroFin, e)
    End Sub
End Class