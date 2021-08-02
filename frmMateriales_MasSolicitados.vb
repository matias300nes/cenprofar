Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO




Public Class frmMateriales_MasSolicitados

    Dim dv As DataView

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load

        dtpDesde.Value = Date.Today
        dtpHasta.Value = Date.Today

        btnBuscar_Click(sender, e)

    End Sub

    Private Sub txtFiltro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFiltro.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFiltro_KeyUp(sender As Object, e As KeyEventArgs) Handles txtFiltro.KeyUp
        'llamo al procedimiento para aplicar el filtro
        aplicar_filtro()
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        RealizarBusqueda()
    End Sub

    Private Sub RealizarBusqueda()

        Dim ds As Data.DataSet

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            Dim desde As DateTime = dtpDesde.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
            Dim hasta As DateTime = dtpHasta.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
            Dim sqlstring As String = "exec spMateriales_Mas_Solicitados '" & desde & "','" & hasta & "'"

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
            ds.Dispose()

            If ds.Tables(0).Rows.Count > 0 Then
                lblRegistros.Visible = False
                'lleno la grilla con la consulta dada
                grd.DataSource = ds.Tables(0).DefaultView
                'igualo el dv con el dataset
                dv = ds.Tables(0).DefaultView

                grd.DataSource = ds.Tables(0).DefaultView
                grd.Columns(0).Width = 20
                grd.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
                grd.Columns(1).Width = 250
                grd.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
                grd.Columns(2).Width = 30
                grd.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
                grd.Columns(3).Width = 80
                grd.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
                grd.Rows(0).Selected = False
            Else
                lblRegistros.Visible = True
            End If
         

        Catch ex As Exception

        End Try




    End Sub

    Private Sub aplicar_filtro()

        Try
            'limpiar filtro
            dv.RowFilter = ""

            Dim sqlstring As String

            If txtFiltro.Text.ToString = "" Then
                sqlstring = " [Producto] = [Producto]"
            Else
                sqlstring = " [Producto] Like '%" & txtFiltro.Text & "%'"
            End If

            'aplico el filtro al final
            dv.RowFilter = sqlstring

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
 




End Class