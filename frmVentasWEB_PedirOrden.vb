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




Public Class frmVentasWEB_PedirOrden
    Dim ds_Marcas As Data.DataSet

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load
        LimpiarSeleccion()
        txtPtoVta.Focus()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        LimpiarSeleccion()
        txtPtoVta.Focus()
    End Sub

    Private Sub btnOmitir_Click(sender As Object, e As EventArgs) Handles btnOmitir.Click
        Me.Close()
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If lblNro.Text <> "" Then

            frmVentasWEB.cmbClientes.SelectedValue = lblIDCliente.Text
            frmVentasWEB.txtNroProcesado.Text = lblNro.Text

            For i As Integer = 0 To ds_Marcas.Tables(0).Rows.Count - 1
                frmVentasWEB.grdItems.Rows.Add(ds_Marcas.Tables(0).Rows(i)(9), ds_Marcas.Tables(0).Rows(i)(10), _
                                               ds_Marcas.Tables(0).Rows(i)(11), ds_Marcas.Tables(0).Rows(i)(12), _
                                               ds_Marcas.Tables(0).Rows(i)(13), ds_Marcas.Tables(0).Rows(i)(14), _
                                               ds_Marcas.Tables(0).Rows(i)(15), ds_Marcas.Tables(0).Rows(i)(16), _
                                               ds_Marcas.Tables(0).Rows(i)(17), ds_Marcas.Tables(0).Rows(i)(18), _
                                               ds_Marcas.Tables(0).Rows(i)(19), ds_Marcas.Tables(0).Rows(i)(20), _
                                               ds_Marcas.Tables(0).Rows(i)(21), ds_Marcas.Tables(0).Rows(i)(22), _
                                               ds_Marcas.Tables(0).Rows(i)(23), ds_Marcas.Tables(0).Rows(i)(24), _
                                               ds_Marcas.Tables(0).Rows(i)(25), ds_Marcas.Tables(0).Rows(i)(26), _
                                               ds_Marcas.Tables(0).Rows(i)(27), ds_Marcas.Tables(0).Rows(i)(28), _
                                               ds_Marcas.Tables(0).Rows(i)(29), ds_Marcas.Tables(0).Rows(i)(30), _
                                               ds_Marcas.Tables(0).Rows(i)(31), ds_Marcas.Tables(0).Rows(i)(32), _
                                               ds_Marcas.Tables(0).Rows(i)(33), ds_Marcas.Tables(0).Rows(i)(34), _
                                               ds_Marcas.Tables(0).Rows(i)(35))
            Next

            Me.Close()
        End If
    End Sub

    Private Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        If txtPtoVta.Text <> "" And txtNro.Text <> "" Then
            Dim nrocompleto As String = txtPtoVta.Text.ToString.PadLeft(2, "0") + "-" + txtNro.Text.ToString.PadLeft(8, "0")
            TraerDetalle(nrocompleto, frmVentasWEB.btnXNorte.Checked)
        End If
    End Sub



    Private Sub LimpiarSeleccion()
        txtPtoVta.Text = ""
        txtNro.Text = ""
        lblNro.Text = ""
        lblCliente.Text = ""
        lblIDCliente.Text = ""
        lblRepartidor.Text = ""
        lblFecha.Text = ""
        lblHora.Text = ""
        lblSubtotal.Text = ""
        lblDescuento.Text = ""
        lbltotal.Text = ""
    End Sub

    Private Sub TraerDetalle(ByVal nro As String, ByVal norte As Boolean)

        Dim connection As SqlClient.SqlConnection = Nothing
        'Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim sqlstring As String = "exec [spPedidosWEB_Det_GetDetalle_Devolucion] '" & nro & "'," & norte & ""

        Try
            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
            ds_Marcas.Dispose()

            If ds_Marcas.Tables(0).Rows.Count > 0 Then
                lblNro.Text = ds_Marcas.Tables(0).Rows(0)(0).ToString
                lblFecha.Text = ds_Marcas.Tables(0).Rows(0)(1).ToString
                lblHora.Text = ds_Marcas.Tables(0).Rows(0)(2).ToString
                lblCliente.Text = ds_Marcas.Tables(0).Rows(0)(3).ToString
                lblRepartidor.Text = ds_Marcas.Tables(0).Rows(0)(4).ToString
                lblNota.Text = ds_Marcas.Tables(0).Rows(0)(5).ToString
                lblSubtotal.Text = ds_Marcas.Tables(0).Rows(0)(6).ToString
                lblDescuento.Text = ds_Marcas.Tables(0).Rows(0)(7).ToString
                lbltotal.Text = ds_Marcas.Tables(0).Rows(0)(8).ToString
                lblIDCliente.Text = ds_Marcas.Tables(0).Rows(0)(36).ToString
            Else
                LimpiarSeleccion()
                MsgBox("El nro de venta no es correcto o está procesado. Por favor verifique el dato.", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception

        End Try
    End Sub



   
End Class