Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles.compartida
Imports Utiles
Imports ReportesNet

Public Class frmIngresos_postTrabajo

#Region "Declaracion de Variables"
   
    Dim dtnew As New System.Data.DataTable

    Dim RegistrosPorPagina As Integer = 20
    Dim ini As Integer = 0
    Dim fin As Integer = RegistrosPorPagina - 1
    Dim TotalPaginas As Integer
    Dim PaginaActual As Integer
    'Dim bolPaginar As Boolean = False

    Private ds_2 As DataSet

    Dim tran As SqlClient.SqlTransaction

    Dim MontoChequesOriginal As Double
    Dim llenandoCombo As Boolean = False

    Dim Ingreso As Long
    Dim IngresoDetalle As Long

    Dim Permitir2 As Boolean = False

    Public Shadows Event ev_CellChanged As EventHandler

#End Region

#Region "Procedimientos y Componentes Formulario"

    Private Sub frmIngresos_postTrabajo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        configurarform()
        dtiFechaPago.Value = Date.Today

        LlenarComboClientes(cmbCliente)
        LlenarComboObras()
        'LlenarcmbBancos(cmbBanco, ConnStringSEI)
        'LlenarcmbMoneda(cmbMoneda, ConnStringSEI)
        'LlenarcmbTarjetas(cmbTarjetas, ConnStringSEI)

        SQL = "exec [spIngresos_Select_All]"

        LlenarGrilla()

        Permitir = True

        CargarCajas()
        bolModo = False

        PrepararBotones()

        If grd.CurrentRow Is Nothing Then
            Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo Código.", My.Resources.stop_error.ToBitmap)
            btnNuevo.Enabled = False
            btnGuardar.Enabled = True
            gpPago.Enabled = True
            bolModo = True
        Else
            Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo Código.", My.Resources.stop_error.ToBitmap)
            bolModo = False
        End If

        'Permitir2 = True

        ControlarModo()

        '  Permitir = True

    End Sub

    Private Sub chkCheque_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheque.CheckedChanged
        Dim I As Integer

        If bolModo = True Then
            gpDetalle.Enabled = chkCheque.Checked
        Else
            gpDetalle.Enabled = False
        End If
        lblEntregaCheque.Enabled = chkCheque.Checked
        If chkCheque.Checked = False Then
            lblEntregaCheque.Text = "0"
        Else
            If grdCheques.RowCount >= 1 Then
                For I = 0 To grdCheques.RowCount - 1
                    lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) + CDbl(grdCheques.Rows(I).Cells(2).Value)
                Next
            End If
        End If
        txtNroCheque.Focus()
    End Sub

    Private Sub chkContado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkContado.CheckedChanged
        If chkContado.Checked = False Then
            txtEntregaContado.Text = "0"
        End If
        txtEntregaContado.Enabled = chkContado.Checked
        txtEntregaContado.Focus()
    End Sub

    Private Sub chkTarjeta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarjeta.CheckedChanged
        If chkTarjeta.Checked = False Then
            txtEntregaTarjeta.Text = "0"
            txtRecargoTarjeta.Text = "0"
        End If
        txtEntregaTarjeta.Enabled = chkTarjeta.Checked
        cmbTarjetas.Enabled = chkTarjeta.Checked
        lblMontoRecargo.Enabled = chkTarjeta.Checked
        txtRecargoTarjeta.Enabled = chkTarjeta.Checked
        lblPorcRecargo.Enabled = chkTarjeta.Checked
        lblPorcentaje.Enabled = chkTarjeta.Checked
        lblRecargo.Enabled = chkTarjeta.Checked
        cmbTarjetas.Focus()
    End Sub

    Private Sub txtEntregaContado_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntregaContado.TextChanged
        lblEntregado.Text = CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text))
    End Sub

    Private Sub txtEntregaTarjeta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntregaTarjeta.TextChanged
        lblMontoRecargo.Text = Math.Round(CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) * CDbl(IIf(txtRecargoTarjeta.Text = "", 0, txtRecargoTarjeta.Text)) / 100, 2)
        lblEntregado.Text = Math.Round(CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text)), 2) '- CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
    End Sub

    Private Sub txtRecargoTarjeta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRecargoTarjeta.TextChanged
        lblMontoRecargo.Text = Math.Round(CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) * CDbl(IIf(txtRecargoTarjeta.Text = "", 0, txtRecargoTarjeta.Text)) / 100, 2)
        lblEntregado.Text = Math.Round(CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text)), 2) '- CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
    End Sub

    Private Sub lblEntregaCheque_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblEntregaCheque.TextChanged
        lblEntregado.Text = CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text)) '- CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
    End Sub

    Private Sub cmbTarjetas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTarjetas.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtNroCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroCheque.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtMontoCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoCheque.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtRecargoTarjeta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRecargoTarjeta.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtEntregaTarjeta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEntregaTarjeta.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtEntregaContado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEntregaContado.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub cmbCliente_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedValueChanged
        If llenandoCombo = False Then
            grdConsumosRealizados.Rows.Clear()
            LlenarComboObras()
        End If
    End Sub

    Private Sub cmbObras_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbObras.SelectedValueChanged
        If llenandoCombo = False Then
            If cmbCliente.Text.ToString <> "" Then
                Dim connection As SqlClient.SqlConnection = Nothing

                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                Try
                    Dim dt As New DataTable
                    Dim sqltxt2 As String

                    grdConsumosRealizados.Rows.Clear()

                    If cmbObras.Text.ToString = "0 - Ventas" Then
                        sqltxt2 = "select 'Venta' as Tipo, id as nro, convert(varchar(10), fecha, 103) as fecha, cast(MONTOIVA as decimal(18,2)) AS IVA, CAST(totalconsumo AS DECIMAL(18,2)) as TOTAL, ID " + _
                                " from consumos where venta = 1 and idcliente = " & cmbCliente.SelectedValue & " and id not in (select idmovimiento from ingresos_consumos where tipomov = 'Venta')"
                    Else
                        sqltxt2 = "select 'Consumo' as Tipo, id as nro, convert(varchar(10), fecha, 103) as fecha, cast(MONTOIVA as decimal(18,2)) AS IVA, CAST(totalconsumo AS DECIMAL(18,2)) as TOTAL, ID" + _
                                " from consumos where idobra = " & cmbObras.SelectedValue.ToString & " and id not in (select idmovimiento from ingresos_consumos where tipomov = 'Consumo') " + _
                                " UNION ALL " + _
                                " select 'Gasto' as Tipo, NroFactura AS Nro, convert(varchar(10), fechagasto, 103) as fecha, Iva, Total, IdGasto AS ID from gastos where idobra =  " & cmbObras.SelectedValue.ToString + _
                                " and idgasto not in (select idmovimiento from ingresos_consumos where tipomov = 'Gasto')"
                    End If

                    Dim cmd As New SqlCommand(sqltxt2, connection)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim i As Integer

                    da.Fill(dt)

                    For i = 0 To dt.Rows.Count - 1
                        grdConsumosRealizados.Rows.Add(dt.Rows(i)("Tipo").ToString(), dt.Rows(i)("nro").ToString(), dt.Rows(i)("fecha").ToString(), dt.Rows(i)("iva").ToString(), dt.Rows(i)("total").ToString(), dt.Rows(i)("ID").ToString())
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If Not connection Is Nothing Then
                        CType(connection, IDisposable).Dispose()
                    End If
                End Try
            End If
        End If
    End Sub

    Private Sub txtRetensiones_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRetensiones.TextChanged
        CalcularTotalAPagar()
    End Sub

    Private Sub txtIntereses_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIntereses.TextChanged
        CalcularTotalAPagar()
    End Sub

    Private Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            CargarCajas()
        End If
        RaiseEvent ev_CellChanged(sender, e) 'por ahora lo usa el formulario entryline
    End Sub

    Private Sub grdItems_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.SelectionChanged
        If Permitir2 Then
            LimpiarCajasItems()
            CargarCajasItems()
            Permitir2 = True
            cmbObras.Visible = False
            LabelX9.Visible = False
        End If
        RaiseEvent ev_CellChanged(sender, e)
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        LimpiarCajasItems()
        txtIntereses.Text = ""
        txtRetensiones.Text = ""
        lblSubtotal.Text = ""
        txtTotalAPagar.Text = ""
        ControlarModo()
        PrepararBotones()
        btnGuardar.Enabled = True
        gpPago.Enabled = True
        chkCheque.Checked = False
        dtiFechaPago.Value = Date.Today

        grdConsumosRealizados.Rows.Clear()
        grdConsumosAPagar.Rows.Clear()

        btnNuevaEntrega.Visible = False
        btnGuardarEntrega.Visible = False
        btnCancelarEntrega.Visible = False
        btnEliminarEntrega.Visible = False
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim cancelado As Boolean = False

        If btnCancelarEntrega.Visible = False Then
            If CDbl(lblEntregado.Text) > CDbl(txtTotalAPagar.Text) Then
                MsgBox("El monto entregado es mayor a la deuda actual.")
                Exit Sub
            End If
        Else
            If CDbl(lblEntregado.Text) > CDbl(grd.CurrentRow.Cells(4).Value) Then
                MsgBox("El monto entregado es mayor a la deuda actual.")
                Exit Sub
            End If
        End If


        If CDbl(lblEntregado.Text) = 0 Then
            MsgBox("El monto entregado ha guardar no puede ser cero.")
            Exit Sub
        End If

        If dtiFechaPago.Value > Date.Today Then
            MsgBox("La fecha de pago no puede ser mayor a la fecha actual.", MsgBoxStyle.Critical, "Control de Ingresos")
            dtiFechaPago.Focus()
            Exit Sub
        End If

        If btnCancelarEntrega.Visible = False Then
            If CDbl(lblEntregado.Text) = CDbl(txtTotalAPagar.Text) Then
                cancelado = True
            End If
        Else
            If CDbl(lblEntregado.Text) = grd.CurrentRow.Cells(4).Value Then
                cancelado = True
            End If
        End If


        If bolModo = True Then
            Select Case AgregarActualizar_Registro(cancelado)
                Case 0
                    Cancelar_Tran()
                    Exit Sub
                Case -1
                    Cancelar_Tran()
                    Exit Sub
            End Select

            Select Case AgregarActualizar_Consumos()
                Case 0
                    Cancelar_Tran()
                    Exit Sub
                Case -1
                    Cancelar_Tran()
                    Exit Sub
            End Select
        End If

        Select Case AgregarActualizar_DetallePago()
            Case 0
                Cancelar_Tran()
                Exit Sub
            Case -1
                Cancelar_Tran()
                Exit Sub
        End Select

        If chkCheque.Checked = True Then
            Select Case AgregarActualizar_Cheques()
                Case 0
                    Cancelar_Tran()
                    Exit Sub
                Case -1
                    Cancelar_Tran()
                    Exit Sub
            End Select
        End If

        If bolModo = True Then
            Cerrar_Tran()
            bolModo = False
            PrepararBotones()
            SQL = "exec spIngresos_Select_All"
            LlenarGrilla()
            ControlarModo()
            btnGuardar.Enabled = False
            gpDetalle.Enabled = False
            gpPago.Enabled = False
        Else
            Select Case ActualizarDatos(cancelado)
                Case -1
                    Cancelar_Tran()
                    Exit Sub
                Case 0
                    Cancelar_Tran()
                    Exit Sub
                Case 1
                    Cerrar_Tran()
                    bolModo = False
                    PrepararBotones()
                    SQL = "exec spIngresos_Select_All"
                    LlenarGrilla()
                    ControlarModo()
                    btnGuardar.Enabled = False
                    gpDetalle.Enabled = False
                    gpPago.Enabled = False
                    btnNuevaEntrega.Enabled = True
            End Select
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        ControlarModo()
        CargarCajas()
        gpPago.Enabled = False
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.ClickEventArgs)
        If MsgBox("Está seguro que desea Eliminar el registro de pago seleccionado por un total de $ " & lblEntregado.Text & " ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Eliminar registro de pago") = MsgBoxResult.Yes Then
            Select Case EliminarEntrega()
                Case -1
                    Cancelar_Tran()
                    Exit Sub
                Case Else
                    Cerrar_Tran()
                    bolModo = False
                    PrepararBotones()
                    LimpiarCajasItems()
                    SQL = "exec spIngresos_Select_All"
                    LlenarGrilla()
                    btnGuardar.Enabled = False
                    gpDetalle.Enabled = False
                    gpPago.Enabled = False
            End Select
        End If
    End Sub

    Private Sub btnAgregarConsumo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarConsumo.Click
        Dim I As Integer

        For I = 0 To grdConsumosAPagar.Rows.Count - 1
            If (grdConsumosRealizados.CurrentRow.Cells(5).Value = grdConsumosAPagar.Rows(I).Cells(5).Value) And (grdConsumosRealizados.CurrentRow.Cells(0).Value = grdConsumosAPagar.Rows(I).Cells(0).Value) Then
                MsgBox("El item seleccionado ya fue cargado, verifique por favor", MsgBoxStyle.Exclamation, "Control de Pagos")
                Exit Sub
            End If
        Next
        grdConsumosAPagar.Rows.Add(grdConsumosRealizados.CurrentRow.Cells(0).Value, grdConsumosRealizados.CurrentRow.Cells(1).Value, grdConsumosRealizados.CurrentRow.Cells(2).Value, grdConsumosRealizados.CurrentRow.Cells(3).Value, grdConsumosRealizados.CurrentRow.Cells(4).Value, grdConsumosRealizados.CurrentRow.Cells(5).Value)
        CalcularTotalAPagar()
    End Sub

    Private Sub btnEliminarConsumo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarConsumo.Click
        Try
            grdConsumosAPagar.Rows.Remove(grdConsumosAPagar.CurrentRow)
            CalcularTotalAPagar()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNuevaEntrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevaEntrega.Click
        btnNuevaEntrega.Enabled = False
        bolModo = True
        LimpiarCajasItems()
        btnGuardarEntrega.Visible = True
        btnCancelarEntrega.Visible = True
        btnEliminarEntrega.Visible = False
        gpPago.Enabled = True
    End Sub

    Private Sub btnCancelarEntrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelarEntrega.Click
        bolModo = False
        gpPago.Enabled = False
        btnCancelar_Click(sender, e)
        btnNuevaEntrega.Enabled = True
    End Sub

    Private Sub btnEliminarEntrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarEntrega.Click
        If MsgBox("Está seguro que desea Eliminar la entrega seleccionada ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Eliminar registro de pago") = MsgBoxResult.Yes Then
            If grdItems.CurrentRow.Cells(9).Value = True Then
                If MsgBox("El registro seleccionado tiene Cheques afectados al Pago." & vbCrLf & "Si elimina este registro los cheques serán eliminados. Desea Continuar?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Eliminar registro de pago") = MsgBoxResult.Yes Then


                End If


            End If

            'Select Case EliminarEntrega()
            '    Case -1
            '        Cancelar_Tran()
            '        Exit Sub
            '    Case Else
            '        Cerrar_Tran()
            '        bolModo = False
            '        PrepararBotones()
            '        LimpiarCajasItems()
            '        SQL = "exec spIngresos_Select_All"
            '        LlenarGrilla()
            '        btnGuardar.Enabled = False
            '        gpDetalle.Enabled = False
            '        gpPago.Enabled = False
            'End Select
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramcomprobante As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim reporteComprobantedePago As New frmReportes

        nbreformreportes = "Comprobante de Pago"

        paramcomprobante.AgregarParametros("Movimiento :", "STRING", "", False, grd.CurrentRow.Cells(0).Value.ToString, "", cnn)
        paramcomprobante.ShowDialog()

        If cerroparametrosconaceptar = True Then
            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR A LA FUNCION..
            codigo = paramcomprobante.ObtenerParametros(0)
            'reporteComprobantedePago.MostrarComprobantedePago(codigo, reporteComprobantedePago)
            cerroparametrosconaceptar = False
            reporteComprobantedePago = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Sub btnGuardarEntrega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarEntrega.Click
        bolModo = False
        btnGuardar_Click(sender, e)
    End Sub

#End Region

#Region "Funciones"

    Private Function AgregarActualizar_Registro(ByVal Operacioncancelada As Boolean) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try
                Dim param_IdIngreso As New SqlClient.SqlParameter
                param_IdIngreso.ParameterName = "@IdIngreso"
                param_IdIngreso.SqlDbType = SqlDbType.BigInt
                param_IdIngreso.Value = DBNull.Value
                param_IdIngreso.Direction = ParameterDirection.Output

                Dim param_IdCliente As New SqlClient.SqlParameter
                param_IdCliente.ParameterName = "@IdCliente"
                param_IdCliente.SqlDbType = SqlDbType.BigInt
                param_IdCliente.Value = cmbCliente.SelectedValue
                param_IdCliente.Direction = ParameterDirection.Input

                Dim param_Obra As New SqlClient.SqlParameter
                param_Obra.ParameterName = "@Obra"
                param_Obra.SqlDbType = SqlDbType.VarChar
                param_Obra.Size = 50
                param_Obra.Value = cmbObras.Text
                param_Obra.Direction = ParameterDirection.Input

                Dim param_Retensiones As New SqlClient.SqlParameter
                param_Retensiones.ParameterName = "@Retensiones"
                param_Retensiones.SqlDbType = SqlDbType.Decimal
                param_Retensiones.Size = 18
                param_Retensiones.Value = IIf(txtRetensiones.Text = "", 0, txtRetensiones.Text)
                param_Retensiones.Direction = ParameterDirection.Input

                Dim param_Intereses As New SqlClient.SqlParameter
                param_Intereses.ParameterName = "@Intereses"
                param_Intereses.SqlDbType = SqlDbType.Decimal
                param_Intereses.Size = 18
                param_Intereses.Value = IIf(txtIntereses.Text = "", 0, txtIntereses.Text)
                param_Intereses.Direction = ParameterDirection.Input

                Dim param_MontoIVA As New SqlClient.SqlParameter
                param_MontoIVA.ParameterName = "@montoIVA"
                param_MontoIVA.SqlDbType = SqlDbType.Decimal
                param_MontoIVA.Size = 18
                param_MontoIVA.Value = IIf(lblMontoIva.Text = "", 0, lblMontoIva.Text)
                param_MontoIVA.Direction = ParameterDirection.Input

                Dim param_SubtotalConsumo As New SqlClient.SqlParameter
                param_SubtotalConsumo.ParameterName = "@SubtotalConsumo"
                param_SubtotalConsumo.SqlDbType = SqlDbType.Decimal
                param_SubtotalConsumo.Size = 18
                param_SubtotalConsumo.Value = IIf(lblSubtotal.Text = "", 0, lblSubtotal.Text)
                param_SubtotalConsumo.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Size = 18
                param_total.Value = IIf(txtTotalAPagar.Text = "", 0, txtTotalAPagar.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_entregado As New SqlClient.SqlParameter
                param_entregado.ParameterName = "@entregado"
                param_entregado.SqlDbType = SqlDbType.Decimal
                param_entregado.Size = 18
                param_entregado.Value = IIf(lblEntregado.Text = "", 0, lblEntregado.Text)
                param_entregado.Direction = ParameterDirection.Input

                Dim param_Cancelado As New SqlClient.SqlParameter
                param_Cancelado.ParameterName = "@Cancelado"
                param_Cancelado.SqlDbType = SqlDbType.Bit
                param_Cancelado.Value = Operacioncancelada
                param_Cancelado.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@useradd"
                param_useradd.SqlDbType = SqlDbType.SmallInt
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Insert", _
                            param_IdIngreso, param_IdCliente, param_Obra, param_Retensiones, param_Intereses, _
                            param_MontoIVA, param_SubtotalConsumo, param_total, param_entregado, param_Cancelado, _
                            param_useradd, param_res)

                    res = param_res.Value
                    Ingreso = param_IdIngreso.Value

                    AgregarActualizar_Registro = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally
            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function AgregarActualizar_Consumos() As Integer

        Dim res As Integer = 0
        Dim I As Integer

        Try
            Try
                Dim param_Ingreso As New SqlClient.SqlParameter
                param_Ingreso.ParameterName = "@IdIngreso"
                param_Ingreso.SqlDbType = SqlDbType.BigInt
                param_Ingreso.Value = Ingreso
                param_Ingreso.Direction = ParameterDirection.Input

                For I = 0 To grdConsumosAPagar.RowCount - 2

                    Dim param_IdConsumo As New SqlClient.SqlParameter
                    param_IdConsumo.ParameterName = "@IdMovimiento"
                    param_IdConsumo.SqlDbType = SqlDbType.BigInt
                    param_IdConsumo.Value = grdConsumosAPagar.Rows(I).Cells(5).Value
                    param_IdConsumo.Direction = ParameterDirection.Input

                    Dim param_TipoMov As New SqlClient.SqlParameter
                    param_TipoMov.ParameterName = "@TipoMov"
                    param_TipoMov.SqlDbType = SqlDbType.VarChar
                    param_TipoMov.Size = 10
                    param_TipoMov.Value = grdConsumosAPagar.Rows(I).Cells(0).Value
                    param_TipoMov.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Consumos_Insert", _
                                param_Ingreso, param_TipoMov, param_IdConsumo, param_res)

                        res = param_res.Value

                        If res < 0 Then
                            AgregarActualizar_Consumos = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarActualizar_Consumos = -1
                        Exit Function
                    End Try

                Next

                AgregarActualizar_Consumos = 1

            Finally
            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function AgregarActualizar_DetallePago() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            If btnCancelarEntrega.Visible = True Then
                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End Try

                'Abrir una transaccion para guardar y asegurar que se guarda todo
                If Abrir_Tran(connection) = False Then
                    MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If

            Try
                Dim param_IdIngresoDetalle As New SqlClient.SqlParameter
                param_IdIngresoDetalle.ParameterName = "@IdIngresoDetalle"
                param_IdIngresoDetalle.SqlDbType = SqlDbType.BigInt
                param_IdIngresoDetalle.Value = DBNull.Value
                param_IdIngresoDetalle.Direction = ParameterDirection.Output

                Dim param_IdIngreso As New SqlClient.SqlParameter
                param_IdIngreso.ParameterName = "@IdIngreso"
                param_IdIngreso.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_IdIngreso.Value = Ingreso
                Else
                    param_IdIngreso.Value = CType(grd.CurrentRow.Cells(0).Value, Long)
                End If
                param_IdIngreso.Direction = ParameterDirection.Input

                Dim param_fechapago As New SqlClient.SqlParameter
                param_fechapago.ParameterName = "@fechapago"
                param_fechapago.SqlDbType = SqlDbType.DateTime
                param_fechapago.Value = dtiFechaPago.Value
                param_fechapago.Direction = ParameterDirection.Input

                Dim param_Contado As New SqlClient.SqlParameter
                param_Contado.ParameterName = "@contado"
                param_Contado.SqlDbType = SqlDbType.Bit
                param_Contado.Value = chkContado.Checked
                param_Contado.Direction = ParameterDirection.Input

                Dim param_MontoContado As New SqlClient.SqlParameter
                param_MontoContado.ParameterName = "@montocontado"
                param_MontoContado.SqlDbType = SqlDbType.Decimal
                param_MontoContado.Size = 18
                param_MontoContado.Value = IIf(txtEntregaContado.Text = "", 0, CDbl(txtEntregaContado.Text))
                param_MontoContado.Direction = ParameterDirection.Input

                Dim param_Tarjeta As New SqlClient.SqlParameter
                param_Tarjeta.ParameterName = "@tarjeta"
                param_Tarjeta.SqlDbType = SqlDbType.Bit
                param_Tarjeta.Value = chkTarjeta.Checked
                param_Tarjeta.Direction = ParameterDirection.Input

                Dim param_NombreTarjeta As New SqlClient.SqlParameter
                param_NombreTarjeta.ParameterName = "@nombretarjeta"
                param_NombreTarjeta.SqlDbType = SqlDbType.VarChar
                param_NombreTarjeta.Size = 50
                param_NombreTarjeta.Value = IIf(chkTarjeta.Checked = False, "", cmbTarjetas.Text)
                param_NombreTarjeta.Direction = ParameterDirection.Input

                Dim param_MontoTarjeta As New SqlClient.SqlParameter
                param_MontoTarjeta.ParameterName = "@montotarjeta"
                param_MontoTarjeta.SqlDbType = SqlDbType.Decimal
                param_MontoTarjeta.Value = IIf(txtEntregaTarjeta.Text = "", 0, CDbl(txtEntregaTarjeta.Text))
                param_MontoTarjeta.Direction = ParameterDirection.Input

                Dim param_PorcentajeRecargo As New SqlClient.SqlParameter
                param_PorcentajeRecargo.ParameterName = "@porcentajerecargotarjeta"
                param_PorcentajeRecargo.SqlDbType = SqlDbType.Decimal
                param_PorcentajeRecargo.Value = IIf(txtRecargoTarjeta.Text = "", 0, CDbl(txtRecargoTarjeta.Text))
                param_PorcentajeRecargo.Direction = ParameterDirection.Input

                Dim param_Cheque As New SqlClient.SqlParameter
                param_Cheque.ParameterName = "@Cheque"
                param_Cheque.SqlDbType = SqlDbType.Bit
                param_Cheque.Value = chkCheque.Checked
                param_Cheque.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@useradd"
                param_useradd.SqlDbType = SqlDbType.SmallInt
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Detalles_Insert", _
                        param_IdIngreso, param_IdIngresoDetalle, param_fechapago, param_Contado, _
                        param_MontoContado, param_Tarjeta, param_NombreTarjeta, param_MontoTarjeta, _
                        param_PorcentajeRecargo, param_Cheque, param_useradd, param_res)

                    res = param_res.Value
                    IngresoDetalle = param_IdIngresoDetalle.Value

                    AgregarActualizar_DetallePago = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally
            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function AgregarActualizar_Cheques() As Integer

        Dim res As Integer = 0
        Dim res1 As Integer = 0
        Dim I As Integer

        Try
            Try
                Dim param_Ingreso As New SqlClient.SqlParameter
                param_Ingreso.ParameterName = "@Ingreso_Detalle"
                param_Ingreso.SqlDbType = SqlDbType.BigInt
                param_Ingreso.Value = IngresoDetalle
                param_Ingreso.Direction = ParameterDirection.Input

                For I = 0 To grdCheques.RowCount - 2

                    Dim param_IngresoCheque As New SqlClient.SqlParameter
                    param_IngresoCheque.ParameterName = "@Ingreso_Cheque"
                    param_IngresoCheque.SqlDbType = SqlDbType.Bit
                    param_IngresoCheque.Value = chkCheque.Checked
                    param_IngresoCheque.Direction = ParameterDirection.Input

                    Dim param_NroCheque As New SqlClient.SqlParameter
                    param_NroCheque.ParameterName = "@NroCheque"
                    param_NroCheque.SqlDbType = SqlDbType.BigInt
                    param_NroCheque.Value = CType(grdCheques.Rows(I).Cells(0).Value, Long)
                    param_NroCheque.Direction = ParameterDirection.Input

                    Dim param_IdCliente As New SqlClient.SqlParameter
                    param_IdCliente.ParameterName = "@IdCliente"
                    param_IdCliente.SqlDbType = SqlDbType.Int
                    param_IdCliente.Value = cmbCliente.SelectedValue
                    param_IdCliente.Direction = ParameterDirection.Input

                    Dim param_ClienteChequeBco As New SqlClient.SqlParameter
                    param_ClienteChequeBco.ParameterName = "@ClienteChequeBco"
                    param_ClienteChequeBco.SqlDbType = SqlDbType.NVarChar
                    param_ClienteChequeBco.Size = 50
                    param_ClienteChequeBco.Value = grdCheques.Rows(I).Cells(4).Value
                    param_ClienteChequeBco.Direction = ParameterDirection.Input

                    Dim param_FechaCobro As New SqlClient.SqlParameter
                    param_FechaCobro.ParameterName = "@FechaCobro"
                    param_FechaCobro.SqlDbType = SqlDbType.DateTime
                    param_FechaCobro.Value = grdCheques.Rows(I).Cells(3).Value
                    param_FechaCobro.Direction = ParameterDirection.Input

                    Dim param_Moneda As New SqlClient.SqlParameter
                    param_Moneda.ParameterName = "@IdMoneda"
                    param_Moneda.SqlDbType = SqlDbType.Int
                    param_Moneda.Value = CType(grdCheques.Rows(I).Cells(5).Value, Integer)
                    param_Moneda.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Size = 18
                    param_Monto.Value = CType(grdCheques.Rows(I).Cells(2).Value, Double)
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_Banco As New SqlClient.SqlParameter
                    param_Banco.ParameterName = "@Banco"
                    param_Banco.SqlDbType = SqlDbType.NVarChar
                    param_Banco.Size = 50
                    param_Banco.Value = grdCheques.Rows(I).Cells(1).Value
                    param_Banco.Direction = ParameterDirection.Input

                    Dim param_Observaciones As New SqlClient.SqlParameter
                    param_Observaciones.ParameterName = "@Observaciones"
                    param_Observaciones.SqlDbType = SqlDbType.NVarChar
                    param_Observaciones.Size = 100
                    param_Observaciones.Value = grdCheques.Rows(I).Cells(6).Value
                    param_Observaciones.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@useradd"
                    param_useradd.SqlDbType = SqlDbType.SmallInt
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_Insert", _
                                param_Ingreso, param_IngresoCheque, param_NroCheque, param_IdCliente, _
                                param_ClienteChequeBco, param_FechaCobro, param_Moneda, param_Monto, _
                                param_Banco, param_Observaciones, param_useradd, param_res)

                        res = param_res.Value

                        If res < 0 Then
                            AgregarActualizar_Cheques = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarActualizar_Cheques = -1
                        Exit Function
                    End Try

                Next

                AgregarActualizar_Cheques = 1

            Finally
            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function ActualizarDatos(ByVal Trabajocancelado As Boolean) As Integer

        Try
            Dim param_idingreso As New SqlClient.SqlParameter
            param_idingreso.ParameterName = "@idingreso"
            param_idingreso.SqlDbType = SqlDbType.BigInt
            param_idingreso.Value = grd.CurrentRow.Cells(0).Value
            param_idingreso.Direction = ParameterDirection.Input

            Dim param_entregado As New SqlClient.SqlParameter
            param_entregado.ParameterName = "@entregado"
            param_entregado.SqlDbType = SqlDbType.Decimal
            param_entregado.Size = 18
            param_entregado.Value = IIf(lblEntregado.Text = "", 0, CDbl(lblEntregado.Text))
            param_entregado.Direction = ParameterDirection.Input

            Dim param_cancelado As New SqlClient.SqlParameter
            param_cancelado.ParameterName = "@cancelado"
            param_cancelado.SqlDbType = SqlDbType.Bit
            param_cancelado.Value = Trabajocancelado
            param_cancelado.Direction = ParameterDirection.Input

            Dim param_useradd As New SqlClient.SqlParameter
            param_useradd.ParameterName = "@userupd"
            param_useradd.SqlDbType = SqlDbType.SmallInt
            param_useradd.Value = UserID
            param_useradd.Direction = ParameterDirection.Input

            Dim param_modo As New SqlClient.SqlParameter
            param_modo.ParameterName = "@modo"
            param_modo.SqlDbType = SqlDbType.Bit
            param_modo.Value = bolModo
            param_modo.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = 0
            param_res.Direction = ParameterDirection.InputOutput

            Try

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Update", _
                    param_idingreso, param_entregado, param_cancelado, param_useradd, param_res)

                ActualizarDatos = param_res.Value

            Catch ex As Exception
                Throw ex
                ActualizarDatos = -1
                Exit Function
            End Try

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function EliminarEntrega() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try
                Dim param_IdIngresoDetalle As New SqlClient.SqlParameter
                param_IdIngresoDetalle.ParameterName = "@IdIngresoDETALLE"
                param_IdIngresoDetalle.SqlDbType = SqlDbType.BigInt
                param_IdIngresoDetalle.Value = CType(grdItems.CurrentRow.Cells(0).Value, Long)
                param_IdIngresoDetalle.Direction = ParameterDirection.Input

                Dim param_IdIngreso As New SqlClient.SqlParameter
                param_IdIngreso.ParameterName = "@idingreso"
                param_IdIngreso.SqlDbType = SqlDbType.BigInt
                param_IdIngreso.Value = CType(grd.CurrentRow.Cells(0).Value, Long)
                param_IdIngreso.Direction = ParameterDirection.Input

                Dim param_Cuota As New SqlClient.SqlParameter
                param_Cuota.ParameterName = "@cuota"
                param_Cuota.SqlDbType = SqlDbType.SmallInt
                If CBool(grd.CurrentRow.Cells(8).Value) = False Then
                    param_Cuota.Value = DBNull.Value
                Else
                    param_Cuota.Value = grdItems.CurrentRow.Cells(16).Value
                End If
                param_Cuota.Direction = ParameterDirection.Input

                Dim param_MontoTotal As New SqlClient.SqlParameter
                param_MontoTotal.ParameterName = "@MONTOTOTAL"
                param_MontoTotal.SqlDbType = SqlDbType.Decimal
                param_MontoTotal.Value = CDbl(lblEntregado.Text)
                param_MontoTotal.Direction = ParameterDirection.Input

                Dim param_userdel As New SqlClient.SqlParameter
                param_userdel.ParameterName = "@userdel"
                param_userdel.SqlDbType = SqlDbType.SmallInt
                param_userdel.Value = UserID
                param_userdel.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    'If modo = True Then
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Detalles_Delete", _
                        param_IdIngreso, param_IdIngresoDetalle, param_MontoTotal, param_Cuota, param_userdel, param_res)

                    res = param_res.Value

                    EliminarEntrega = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally
            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Cierre de Obras e Ingresos"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 450) '200'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Overloads Sub CargarCajas()
        If Not grd.CurrentRow Is Nothing Then
            If CBool(grd.CurrentRow.Cells(1).Value) = True Then
                btnGuardar.Enabled = False
                btnNuevaEntrega.Visible = False
                btnGuardarEntrega.Visible = False
                btnCancelarEntrega.Visible = False
                btnEliminarEntrega.Location = New System.Drawing.Point(341, 16)
                btnEliminarEntrega.Visible = True
            Else
                LabelX9.Visible = False
                cmbObras.Visible = False
                btnNuevaEntrega.Visible = True
                btnGuardarEntrega.Visible = True
                btnCancelarEntrega.Visible = True
                btnEliminarEntrega.Location = New System.Drawing.Point(644, 16)
                btnEliminarEntrega.Visible = True
            End If
            txtIntereses.Text = grd.CurrentRow.Cells(5).Value
            txtRetensiones.Text = grd.CurrentRow.Cells(6).Value
            txtTotalAPagar.Text = grd.CurrentRow.Cells(3).Value

            LlenarGridItems()
        End If
    End Sub

    Private Sub CargarCajasItems()

        If grdItems.RowCount >= 1 Then

            grdCheques.Rows.Clear()

            chkContado.Checked = CBool(grdItems.CurrentRow.Cells(2).Value)
            txtEntregaContado.Text = grdItems.CurrentRow.Cells(3).Value
            chkTarjeta.Checked = CBool(grdItems.CurrentRow.Cells(4).Value)
            cmbTarjetas.Text = grdItems.CurrentRow.Cells(5).Value
            txtEntregaTarjeta.Text = grdItems.CurrentRow.Cells(6).Value
            txtRecargoTarjeta.Text = grdItems.CurrentRow.Cells(7).Value
            lblMontoRecargo.Text = grdItems.CurrentRow.Cells(8).Value

            If CBool(grdItems.CurrentRow.Cells(9).Value) = True Then
                Dim connection As SqlClient.SqlConnection = Nothing

                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                Try
                    Dim dt As New DataTable
                    Dim sqltxt2 As String

                    sqltxt2 = "SELECT c.* FROM Cheques c JOIN ingresos_cheques ic ON ic.idcheque = c.idcheque WHERE idingresodetalle = " & grdItems.CurrentRow.Cells(0).Value

                    Dim cmd As New SqlCommand(sqltxt2, connection)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim i As Integer

                    da.Fill(dt)

                    For i = 0 To dt.Rows.Count - 1
                        grdCheques.Rows.Add(dt.Rows(i)("nrocheque").ToString(), dt.Rows(i)("banco").ToString(), dt.Rows(i)("monto").ToString(), dt.Rows(i)("fechacobro").ToString(), dt.Rows(i)("clientechequebco").ToString(), dt.Rows(i)("idmoneda").ToString(), dt.Rows(i)("Observaciones").ToString())
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If Not connection Is Nothing Then
                        CType(connection, IDisposable).Dispose()
                    End If
                End Try

            End If

            chkCheque.Checked = False
            chkCheque.Checked = CBool(grdItems.CurrentRow.Cells(9).Value)

            dtiFechaPago.Value = grdItems.CurrentRow.Cells(1).Value

            Permitir2 = False

        End If

    End Sub

    Private Sub LimpiarCajasItems()
        txtNroCheque.Text = ""
        txtObservaciones.Text = ""
        txtPropietario.Text = ""
        txtMontoCheque.Text = ""
        grdCheques.Rows.Clear()

        chkCheque.Checked = False
        chkContado.Checked = False
        chkTarjeta.Checked = False

        cmbTarjetas.Enabled = False
        txtEntregaTarjeta.Enabled = False

    End Sub

    Private Sub ControlarModo()
        grdItems.Visible = Not bolModo
        btnAgregarConsumo.Visible = bolModo
        btnEliminarConsumo.Visible = bolModo
        grdConsumosAPagar.Visible = bolModo
        LabelX11.Visible = bolModo
        LabelX9.Visible = bolModo
        cmbObras.Visible = bolModo
    End Sub

    Private Sub LlenarComboClientes(ByVal cmb As System.Windows.Forms.ComboBox)
        Dim ds_Clientes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        llenandoCombo = True

        Try
            ds_Clientes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, codigo + ' - ' + nombre as codigo FROM Clientes WHERE Eliminado = 0 ORDER BY codigo")
            ds_Clientes.Dispose()

            With cmb
                .DataSource = ds_Clientes.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .TabStop = True
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenandoCombo = False

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarComboObras()
        Dim ds_Obras As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        llenandoCombo = True

        Try
            ds_Obras = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, (CODIGO + ' - ' +  NOMBRE) AS CODIGO FROM OBRAS WHERE  FINALIZADO = 0 and IDCliente = " & CType(cmbCliente.SelectedValue, Long) & " UNION SELECT 0, '0 - Ventas' AS Codigo ORDER BY CODIGO")
            ds_Obras.Dispose()

            cmbObras.Text = ""

            With Me.cmbObras
                .DataSource = ds_Obras.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .TabStop = True
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenandoCombo = False

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub CalcularTotalAPagar()
        Dim i As Integer

        Dim a, b As Double

        For i = 0 To grdConsumosAPagar.Rows.Count - 1
            a = a + grdConsumosAPagar.Rows(i).Cells(4).Value
            b = b + grdConsumosAPagar.Rows(i).Cells(3).Value
        Next

        lblSubtotal.Text = a
        lblMontoIva.Text = b

        txtTotalAPagar.Text = a - CDbl(IIf(txtRetensiones.Text = "", 0, txtRetensiones.Text)) + CDbl(IIf(txtIntereses.Text = "", 0, txtIntereses.Text))

    End Sub

#End Region

#Region "Grilla Items"

    Private Sub LlenarGridItems()
        Permitir2 = False

        grdItems.Columns.Clear()

        SQL = " exec spIngresos_Select_All_ByID " & grd.CurrentRow.Cells(0).Value

        GetDatasetItems(grdItems)

        paginar_items(ds_2.Tables(0)) 'Asignar los datos paginados a la grilla

        InicializarGridItems(grdItems)

        'grdItems.Columns(0).Visible = False
        grdItems.Columns(2).Visible = False
        grdItems.Columns(4).Visible = False

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim dt As New DataTable
            Dim sqltxt2 As String

            grdConsumosRealizados.Rows.Clear()

            sqltxt2 = " select tipomov, id as nro, convert(varchar(10), fecha, 103) as fecha, cast(MONTOIVA as decimal(18,2)) AS IVA, CAST(totalconsumo AS DECIMAL(18,2)) as TOTAL, id " + _
                    " from consumos c join ingresos_consumos ic on ic.idmovimiento = c.id where idingreso = " & grd.CurrentRow.Cells(0).Value & " and tipomov IN ('Consumo', 'Venta') " + _
                    " UNION ALL " + _
                    " select TIPOMOV , NroFactura AS Nro, convert(varchar(10), fechagasto, 103) as fecha, Iva, Total, IdGasto AS ID " + _
                    " from gastos g JOIN ingresos_consumos ic on ic.idmovimiento = g.idgasto " + _
                    "where  idingreso = " & grd.CurrentRow.Cells(0).Value & " and tipomov = 'Gasto'"

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdConsumosRealizados.Rows.Add(dt.Rows(i)("tipomov").ToString(), dt.Rows(i)("nro").ToString(), dt.Rows(i)("fecha").ToString(), dt.Rows(i)("iva").ToString(), dt.Rows(i)("total").ToString(), dt.Rows(i)("id").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        Permitir2 = True

        SQL = "exec spIngresos_Select_All"

    End Sub

    Private Sub GetDatasetItems(ByVal GRD As DataGridView)
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se puede establecer la conexión", "Error de Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()

            GRD.DataSource = ds_2.Tables(0).DefaultView

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
             + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
             "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub InicializarGridItems(ByVal Grd As DataGridView)

        Dim style As New DataGridViewCellStyle
        Grd.EnableHeadersVisualStyles = False

        'da formato al encabezado...
        With Grd.ColumnHeadersDefaultCellStyle
            .BackColor = Color.CadetBlue
            .ForeColor = Color.Purple
            .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        ' Inicialice propiedades básicas.
        With Grd
            '.Dock = DockStyle.Fill ' lo coloca al tope del formulario..
            .BackgroundColor = SystemColors.ActiveBorder 'Color.DarkGray ' color del fondo del grid...
            .BorderStyle = BorderStyle.Fixed3D
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
            .AllowUserToAddRows = False 'indica si se muestra al usuario la opción de agregar filas
            .AllowUserToDeleteRows = False 'indica si el usuario puede eliminar filas de DataGridView.
            .AllowUserToOrderColumns = False 'indica si el usuario puede cambiar manualmente de lugar las columnas..
            .ReadOnly = True
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'indica cómo se pueden seleccionar las celdas de DataGridView.
            '.MultiSelect = False 'indica si el usuario puede seleccionar a la vez varias celdas, filas o columnas del control DataGridView.
            '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders   'indica cómo se determina el alto de las filas. 
            .AllowUserToResizeColumns = False 'indica si los usuarios pueden cambiar el tamaño de las columnas.
            .AllowUserToResizeRows = False 'indica si los usuarios pueden cambiar el tamaño de las filas.
            '.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize 'indica si el alto de los encabezados de columna es ajustable y si puede ser ajustado por el usuario o automáticamente para adaptarse al contenido de los encabezados. 
        End With

        'Setear el color de seleccion de fondo de la celda actual...
        Grd.DefaultCellStyle.SelectionBackColor = Color.White
        Grd.DefaultCellStyle.SelectionForeColor = Color.Blue

        'generamos el formato para las celdas...
        With style
            .BackColor = Color.Lavender   'Color.LightGray
            .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .ForeColor = Color.Black
        End With
        Grd.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Aplicamos el estilo a todas las celdas del control DataGridView
        'Grd.RowsDefaultCellStyle = style

        Grd.Columns(0).Width = 40
        Grd.Columns(1).Width = 70
        Grd.Columns(3).Width = 70
        Grd.Columns(5).Width = 80
        Grd.Columns(6).Width = 60
        Grd.Columns(7).Width = 45
        Grd.Columns(8).Width = 60
        Grd.Columns(9).Width = 70

    End Sub

    Protected Sub SetColumnasGrillaiTEMS()

        grdItems.Columns.Clear()

        For Each dc As DataColumn In dtnew.Columns
            Select Case dc.DataType.Name.ToUpper
                Case "BOOLEAN"
                    Dim Column = New DataGridViewCheckBoxColumn()
                    Column.DataPropertyName = dc.ColumnName
                    Column.HeaderText = dc.ColumnName
                    Column.Name = dc.ColumnName
                    Column.SortMode = DataGridViewColumnSortMode.Automatic
                    Column.ValueType = dc.DataType
                    If dc.ColumnName.ToLower = "eliminado" Then
                        Column.Visible = False
                    End If
                    grdItems.Columns.Add(Column)
                Case Else
                    Dim Column = New DataGridViewTextBoxColumn()
                    Column.DataPropertyName = dc.ColumnName
                    Column.HeaderText = dc.ColumnName
                    Column.Name = dc.ColumnName
                    Column.SortMode = DataGridViewColumnSortMode.Automatic
                    Column.ValueType = dc.DataType

                    grdItems.Columns.Add(Column)
            End Select
        Next

    End Sub

    Private Sub paginar_items(ByVal d As DataTable)

        Dim o(1) As String
        Dim TEMP As String = ""
        Static Flag As Boolean = False
        TEMP = ""

        dtnew = d.Clone 'paginarDataDridView(d, ini, fin)

        ReDim o(dtnew.Columns.Count * dtnew.Rows.Count)

        'grdItems.Rows.Clear()
        If Not Flag Then
            SetColumnasGrillaiTEMS()
            Flag = True
        End If

        'For Each row As DataRow In ds.Tables(0).Rows
        For Each row As DataRow In dtnew.Rows
            'nuevo
            For i As Integer = 0 To grdItems.Columns.Count - 1
                Select Case grdItems.Columns(i).ValueType.Name.ToUpper
                    Case "DECIMAL"
                        If row.Item(i).ToString <> "" Then 'And Not (IsDBNull(row.Item(i).ToString)) Then
                            TEMP = CType(row.Item(i).ToString, Decimal).ToString("0.00")
                        End If

                    Case "DATETIME"
                        If row.Item(i).ToString <> "" And Not (IsDBNull(row.Item(i).ToString)) Then
                            TEMP = CType(row.Item(i).ToString, Date).ToString("dd/MM/yyyy")
                        End If

                    Case Else
                        TEMP = row.Item(i).ToString
                End Select
                o(i) = TEMP
            Next i
            'fin nuevo
            grdItems.Rows.Add(o)
        Next

        grdItems.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        dtnew.Dispose()

        'InformarCantidadRegistros()

    End Sub

#End Region

#Region "Grilla Cheques"

    Private Sub grdCheques_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCheques.SelectionChanged
        If Not grdCheques.CurrentRow Is Nothing Then
            txtNroCheque.Text = grdCheques.CurrentRow.Cells(0).Value
            cmbBanco.Text = grdCheques.CurrentRow.Cells(1).Value
            txtMontoCheque.Text = grdCheques.CurrentRow.Cells(2).Value
            dtiVencimientoCheque.Value = grdCheques.CurrentRow.Cells(3).Value
            txtPropietario.Text = grdCheques.CurrentRow.Cells(4).Value
            'cmbMoneda.SelectedValue = grd.CurrentRow.Cells(5).Value
            txtObservaciones.Text = grdCheques.CurrentRow.Cells(6).Value
        Else
            LimpiarCajasItems()
        End If
    End Sub

    Private Sub btnAgregarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarCheque.Click
        If txtNroCheque.Text = "" Then
            MsgBox("Debe ingresar el número del cheque.", MsgBoxStyle.Critical, "Control de Cheques")
            Exit Sub
        End If

        If cmbBanco.Text = "" Then
            MsgBox("Debe ingresar el nombre del Banco.", MsgBoxStyle.Critical, "Control de Cheques")
            Exit Sub
        End If

        If txtMontoCheque.Text = "" Then
            MsgBox("Debe ingresar el monto del cheque.", MsgBoxStyle.Critical, "Control de Cheques")
            Exit Sub
        End If

        Try
            grdCheques.Rows.Add(txtNroCheque.Text, cmbBanco.Text, txtMontoCheque.Text, dtiVencimientoCheque.Value, txtPropietario.Text, cmbMoneda.SelectedValue, txtObservaciones.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) + CDbl(txtMontoCheque.Text)
        txtNroCheque.Text = ""
        txtMontoCheque.Text = ""
        txtPropietario.Text = ""
        txtObservaciones.Text = ""

    End Sub

    Private Sub btnEliminarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCheque.Click
        Try
            lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) - grdCheques.CurrentRow.Cells(2).Value
            grdCheques.Rows.Remove(grdCheques.CurrentRow)
        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Transacciones"

    Private Function Abrir_Tran(ByRef cnn As SqlClient.SqlConnection) As Boolean
        If tran Is Nothing Then
            Try
                tran = cnn.BeginTransaction
                Abrir_Tran = True
            Catch ex As Exception
                Abrir_Tran = False
                Exit Function
            End Try
        End If
    End Function

    Private Sub Cerrar_Tran()
        'Cierra o finaliza la transaccion
        If Not (tran Is Nothing) Then
            tran.Commit()
            tran.Dispose()
            tran = Nothing
        End If
    End Sub

    Private Sub Cancelar_Tran()
        'Cancela la transaccion
        If Not (tran Is Nothing) Then
            tran.Rollback()
            tran.Dispose()
            tran = Nothing
        End If
    End Sub

#End Region

   
End Class