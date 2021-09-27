Imports System.Data.SqlClient
Module ConexionMaestra
    'Variable con la conexion sql
    Public conexion As New SqlConnection("Data Source = DESKTOP-1LA75V6; initial catalog = BASECOLEGIO; integrated security = true")
    'Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
    'funcion que la abre solo cuando su estado sea cero
    Sub abrir()
        If conexion.State = 0 Then
            conexion.Open()
        End If
    End Sub
    'funcion que la cierra cuando su estado sea > 1
    Sub cerrar()
        If conexion.State = 1 Then
            conexion.Close()
        End If
    End Sub

End Module
