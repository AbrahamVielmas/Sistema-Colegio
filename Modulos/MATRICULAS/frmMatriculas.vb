Imports System.Data.SqlClient
Public Class frmMatriculas
    Dim IdAlumno As Integer
    Dim IdGrado As Integer
    Dim LocacionPanelMatriculasX As Integer
    Dim LocacionPanelMatriculasy As Integer
    Dim TamañoPanelMatriculasX As Integer
    Dim TamañoPanelMatriculasy As Integer
    Private Sub frmMatriculas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar_Tipo_Documento_Insertar_Matriculas()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        frmSerializacion.ShowDialog()
    End Sub

    Sub mostrar_Tipo_Documento_Insertar_Matriculas()
        Dim dtComprobante As New DataTable
        Dim da As SqlDataAdapter
        Try
            abrir()
            da = New SqlDataAdapter("mostrar_Tipo_Documento_Insertar_Matriculas", conexion)
            da.Fill(dtComprobante)
            cboComprobante.DisplayMember = "COMPROBANTE"
            cboComprobante.ValueMember = "IdSerializacion"
            cboComprobante.DataSource = dtComprobante
            cerrar()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Sub mostrar_MATRICULAS_YA_ECHAS()
        Dim importante As Double
        Dim com As New SqlCommand("mostrar_MATRICULAS_YA_ECHAS", conexion) 'Con esto ejecutamos el procedimiento y le pasamos la variable de la conexion a la BD
        com.CommandType = CommandType.StoredProcedure 'aun nose para que sirve pero es importante, su valor es 4
        com.Parameters.AddWithValue("@Grado", cboGrado.Text) ''Aqui le damos valor a las variables del procedimiento
        com.Parameters.AddWithValue("@Id_Alumno", IdAlumno)
        Try
            abrir() 'Abrimos la DB
            importante = com.ExecuteScalar()
            cerrar()
            IdGrado = importante
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If txtBuscar.Text = "" Then
            lblBusqueda.Visible = True
            DataListado.Visible = True
            DataListado.Size = New Point(PanelMatriculas.Width, PanelMatriculas.Height)
            DataListado.Location = New Point(PanelMatriculas.Location.X, PanelMatriculas.Location.Y)
        ElseIf txtBuscar.Text <> "" Then
            lblBusqueda.Visible = False
            DataListado.Visible = True
            DataListado.Size = New Point(PanelMatriculas.Width, PanelMatriculas.Height)
            DataListado.Location = New Point(PanelMatriculas.Location.X, PanelMatriculas.Location.Y)
        End If
    End Sub
End Class
