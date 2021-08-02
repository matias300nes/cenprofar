Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports System.Threading
Imports ReportesNet

Public Class frmIngresos

#Region "Declaraciones"

    ' Public NroPresupuesto As Integer
    Public TipoTrabajo As String

    Dim tran As SqlClient.SqlTransaction
    Dim Ingreso As Long
    Dim IngresoDetalle As Long
    Dim i As Integer

#End Region

#Region "Procedimientos Formularios y Componentes"

    Private Sub frmIngresos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LlenarcmbBancos(cmbBanco, ConnStringPERFO)
        'LlenarcmbMoneda(cmbMoneda, ConnStringPERFO)
        'LlenarcmbTarjetas(cmbTarjetas, ConnStringPERFO)
        dtiVencimientoCheque.Value = Date.Today.Date
        dtiPrimerCuota.Value = Date.Today.Date
    End Sub

    Private Sub chkCuotas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCuotas.CheckedChanged
        lblCantidad.Enabled = chkCuotas.Checked
        lblPrimeraCuota.Enabled = chkCuotas.Checked
        lblTipo.Enabled = chkCuotas.Checked
        iiCantidadCuotas.Enabled = chkCuotas.Checked
        cmbTipoCuota.Enabled = chkCuotas.Checked
        dtiPrimerCuota.Enabled = chkCuotas.Checked
        dtiPrimerCuota.Value = Date.Today.Date
        lblMontoCuotas.Enabled = chkCuotas.Checked
        lblCantCuotas.Enabled = chkCuotas.Checked
        lblMontoCuotas.Text = Math.Round(CDbl(lblTotalaPagar.Text) / iiCantidadCuotas.Value, 2)
    End Sub

    Private Sub chkCheque_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheque.CheckedChanged
        gpCheques.Enabled = chkCheque.Checked
        lblEntregaCheque.Enabled = chkCheque.Checked
        If chkCheque.Checked = False Then
            lblEntregaCheque.Text = "0"
        Else
            If grd.RowCount > 1 Then
                For i = 0 To grd.RowCount - 1
                    lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) + CDbl(grd.Rows(i).Cells(2).Value)
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

    Private Sub chkRetensiones_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRetensiones.CheckedChanged
        If chkRetensiones.Checked = False Then
            txtRetension.Text = "0"
        End If
        If chkCuotas.Checked = True Then
            lblMontoCuotas.Text = Math.Round(CDbl(lblTotalaPagar.Text) / iiCantidadCuotas.Value, 2)
        End If
        txtRetension.Enabled = chkRetensiones.Checked
        txtRetension.Focus()
    End Sub

    Private Sub chkIVA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAplicarIvaParcial.CheckedChanged
        txtMontoIva.Enabled = chkAplicarIvaParcial.Checked
        lblMontoAplicaIVA.Enabled = chkAplicarIvaParcial.Checked
        lblIva.Enabled = chkAplicarIvaParcial.Checked
        lblMontoIVA.Enabled = chkAplicarIvaParcial.Checked
        lblSubtotalsinIVA.Enabled = chkAplicarIvaParcial.Checked
        lblMontoSinIVA.Enabled = chkAplicarIvaParcial.Checked
        If chkAplicarIvaParcial.Checked = False And chkRetensiones.Checked = False Then
            txtMontoIva.Text = "0"
            txtRetension.Text = "0"
            lblTotalaPagar.Text = lblMontoReal.Text
        End If
    End Sub

    Private Sub txtPorcIVA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtMontoIVA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoIva.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtMontoIVA_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMontoIva.TextChanged
        'lblMontoIVA.Text = (CDbl(IIf(txtPorcIVA.Text = "", 0, txtPorcIVA.Text)) / 100) * CDbl(IIf(txtMontoIva.Text = "", 0, txtMontoIva.Text))
        lblMontoSinIVA.Text = CDbl(IIf(lblMontoReal.Text = "", 0, lblMontoReal.Text)) - CDbl(IIf(txtMontoIva.Text = "", 0, txtMontoIva.Text))
        lblTotalaPagar.Text = CDbl(IIf(lblMontoSinIVA.Text = "", 0, lblMontoSinIVA.Text)) + CDbl(IIf(lblMontoIVA.Text = "", 0, lblMontoIVA.Text)) + CDbl(IIf(txtMontoIva.Text = "", 0, txtMontoIva.Text)) - CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
        If chkCuotas.Checked = True Then
            lblMontoCuotas.Text = Math.Round(CDbl(lblTotalaPagar.Text) / iiCantidadCuotas.Value, 2)
        End If
    End Sub

    Private Sub txtPorcIVA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lblMontoIVA.Text = (CDbl(IIf(txtPorcIVA.Text = "", 0, txtPorcIVA.Text)) / 100) * CDbl(IIf(txtMontoIva.Text = "", 0, txtMontoIva.Text))
        lblTotalaPagar.Text = CDbl(IIf(lblMontoSinIVA.Text = "", 0, lblMontoSinIVA.Text)) + CDbl(IIf(lblMontoIVA.Text = "", 0, lblMontoIVA.Text)) + CDbl(IIf(txtMontoIva.Text = "", 0, txtMontoIva.Text)) - CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
    End Sub

    Private Sub txtEntregaContado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEntregaContado.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtEntregaContado_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntregaContado.TextChanged
        lblEntregado.Text = CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text))
    End Sub

    Private Sub txtEntregaTarjeta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEntregaTarjeta.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtEntregaTarjeta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntregaTarjeta.TextChanged
        lblMontoRecargo.Text = Math.Round(CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) * CDbl(IIf(txtRecargoTarjeta.Text = "", 0, txtRecargoTarjeta.Text)) / 100, 2)
        lblEntregado.Text = Math.Round(CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text)), 2) '- CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
    End Sub

    Private Sub txtRecargoTarjeta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRecargoTarjeta.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtRecargoTarjeta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRecargoTarjeta.TextChanged
        lblMontoRecargo.Text = Math.Round(CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) * CDbl(IIf(txtRecargoTarjeta.Text = "", 0, txtRecargoTarjeta.Text)) / 100, 2)
        lblEntregado.Text = Math.Round(CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text)), 2) '- CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
    End Sub

    Private Sub txtRetension_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRetension.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub txtRetension_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRetension.TextChanged
        lblTotalaPagar.Text = CDbl(IIf(lblMontoSinIVA.Text = "", 0, lblMontoSinIVA.Text)) + CDbl(IIf(lblMontoIVA.Text = "", 0, lblMontoIVA.Text)) + CDbl(IIf(txtMontoIva.Text = "", 0, txtMontoIva.Text)) - CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
        If chkCuotas.Checked = True Then
            lblMontoCuotas.Text = Math.Round(CDbl(lblTotalaPagar.Text) / iiCantidadCuotas.Value, 2)
        End If
    End Sub

    Private Sub lblEntregaCheque_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblEntregaCheque.TextChanged
        lblEntregado.Text = CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(txtEntregaTarjeta.Text = "", 0, txtEntregaTarjeta.Text)) + CDbl(IIf(lblEntregaCheque.Text = "", 0, lblEntregaCheque.Text)) + CDbl(IIf(lblMontoRecargo.Text = "", 0, lblMontoRecargo.Text)) '- CDbl(IIf(txtRetension.Text = "", 0, txtRetension.Text))
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

    Private Sub cmbTarjetas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTarjetas.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
            Exit Sub
        End If
    End Sub

    Private Sub iiCantidadCuotas_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles iiCantidadCuotas.ValueChanged
        lblMontoCuotas.Text = Math.Round(CDbl(lblTotalaPagar.Text) / iiCantidadCuotas.Value, 2)
    End Sub

#End Region

#Region "Botones"

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
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
            grd.Rows.Add(txtNroCheque.Text, cmbBanco.Text, txtMontoCheque.Text, dtiVencimientoCheque.Value, txtPropietario.Text, cmbMoneda.SelectedValue, txtObservaciones.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) + CDbl(txtMontoCheque.Text)
        txtNroCheque.Text = ""
        txtMontoCheque.Text = ""
        txtPropietario.Text = ""
        txtObservaciones.Text = ""

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) - grd.CurrentRow.Cells(2).Value
            grd.Rows.Remove(grd.CurrentRow)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim cancelado As Boolean = False

        If chkCuotas.Checked = True Then
            If CDbl(lblEntregado.Text) <> CDbl(lblMontoCuotas.Text) Then
                MsgBox("Existe una diferencia entre el Monto Total a Pagar y el monto entregado por el Cliente para la Cuota 1.", MsgBoxStyle.Critical, "Control de Ingresos")
                Exit Sub
            End If
        Else
            If CDbl(lblEntregado.Text) < CDbl(lblTotalaPagar.Text) Then
                If (MsgBox("Existe una diferencia entre el Monto Total a Pagar y el monto entregado por el Cliente. Esto generará una deuda en el cliente. Desea Continuar?.", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Control de Ingresos") = MsgBoxResult.No) Then
                    Exit Sub
                End If
            End If
        End If

        'If dtiFechaDatosPerfo.Value > Date.Today Then
        '    MsgBox("La fecha de pago no puede ser mayor a la fecha actual.", MsgBoxStyle.Critical, "Control de Ingresos")
        '    dtiFechaDatosPerfo.Focus()
        '    Exit Sub
        'End If

        If chkCuotas.Checked = True Then
            Try
                If cmbTipoCuota.SelectedItem.ToString = "" Then
                    MsgBox("Debe ingresar el tipo de cuota para el pago actual", MsgBoxStyle.Critical, "Control de Ingresos")
                    Exit Sub
                End If
            Catch ex As Exception
                MsgBox("Debe ingresar el tipo de cuota para el pago actual", MsgBoxStyle.Critical, "Control de Ingresos")
                Exit Sub
            End Try
        End If

        If CDbl(lblEntregado.Text) > CDbl(lblTotalaPagar.Text) Then
            MsgBox("El Monto Entregado es mayor al Monto Total a Pagar.", MsgBoxStyle.Critical, "Control de Ingresos")
            Exit Sub
        End If

        If CDbl(lblEntregado.Text) = CDbl(lblTotalaPagar.Text) Then
            cancelado = True
        End If


        Select Case AgregarActualizar_Registro(cancelado)
            Case 0
                Cancelar_Tran()
                Exit Sub
            Case -1
                Cancelar_Tran()
                Exit Sub
        End Select

        Select Case AgregarActualizar_DetallePago()
            Case 0
                Cancelar_Tran()
                Exit Sub
            Case -1
                Cancelar_Tran()
                Exit Sub
        End Select

        If chkCuotas.Checked = True Then
            Select Case AgregarActualizar_Cuotas()
                Case 0
                    Cancelar_Tran()
                    Exit Sub
                Case -1
                    Cancelar_Tran()
                    Exit Sub
            End Select
        End If

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

        Select Case ActualizarDatos()
            Case -1
                MsgBox("Error desconocido al actualizar los datos de la perforación para cerrar el trabajo")
                Cancelar_Tran()
                Exit Sub
            Case -2
                MsgBox("No se pudo realizar la actualización en la tabla Presupuestos")
                Cancelar_Tran()
                Exit Sub
            Case -3
                MsgBox("No se pudo realizar la actualización en la tabla Perforaciones")
                Cancelar_Tran()
                Exit Sub
            Case 1
                MsgBox("Se realizó correctamente el Cierre del trabajo")
                Cerrar_Tran()
                Imprimir_Reporte()
                Me.Close()
        End Select

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'frmDatosPerforacion.IngresosOK = False
        Me.Close()
    End Sub

#End Region

#Region "Funciones"

    Private Function AgregarActualizar_Registro(ByVal Trabcancelado As Boolean) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transacción para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transacción", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try
                Dim param_IdIngreso As New SqlClient.SqlParameter
                param_IdIngreso.ParameterName = "@IdIngreso"
                param_IdIngreso.SqlDbType = SqlDbType.BigInt
                param_IdIngreso.Value = DBNull.Value
                param_IdIngreso.Direction = ParameterDirection.Output

                Dim param_IdPresupuesto As New SqlClient.SqlParameter
                param_IdPresupuesto.ParameterName = "@idpresupuesto"
                param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                param_IdPresupuesto.Value = 0 'CLng(lblNroPresupuesto.Text)
                param_IdPresupuesto.Direction = ParameterDirection.Input

                Dim param_TipoTrabajo As New SqlClient.SqlParameter
                param_TipoTrabajo.ParameterName = "@tipotrabajo"
                param_TipoTrabajo.SqlDbType = SqlDbType.VarChar
                param_TipoTrabajo.Size = 50
                param_TipoTrabajo.Value = TipoTrabajo
                param_TipoTrabajo.Direction = ParameterDirection.Input

                Dim param_TipoCuotas As New SqlClient.SqlParameter
                param_TipoCuotas.ParameterName = "@tipocuotas"
                param_TipoCuotas.SqlDbType = SqlDbType.VarChar
                param_TipoCuotas.Size = 50
                If chkCuotas.Checked = True Then
                    param_TipoCuotas.Value = cmbTipoCuota.SelectedItem.ToString
                Else
                    param_TipoCuotas.Value = ""
                End If
                param_TipoCuotas.Direction = ParameterDirection.Input

                Dim param_Cuotas As New SqlClient.SqlParameter
                param_Cuotas.ParameterName = "@Cuotas"
                param_Cuotas.SqlDbType = SqlDbType.Bit
                param_Cuotas.Value = CBool(chkCuotas.Checked)
                param_Cuotas.Direction = ParameterDirection.Input

                Dim param_CantCuotas As New SqlClient.SqlParameter
                param_CantCuotas.ParameterName = "@CantidadCuotas"
                param_CantCuotas.SqlDbType = SqlDbType.SmallInt
                param_CantCuotas.Value = IIf(chkCuotas.Checked = True, iiCantidadCuotas.Value, 0)
                param_CantCuotas.Direction = ParameterDirection.Input

                Dim param_Retensiones As New SqlClient.SqlParameter
                param_Retensiones.ParameterName = "@Retensiones"
                param_Retensiones.SqlDbType = SqlDbType.Bit
                param_Retensiones.Value = chkRetensiones.Checked
                param_Retensiones.Direction = ParameterDirection.Input

                Dim param_MontoRetension As New SqlClient.SqlParameter
                param_MontoRetension.ParameterName = "@montoretension"
                param_MontoRetension.SqlDbType = SqlDbType.Decimal
                param_MontoRetension.Size = 18
                param_MontoRetension.Value = IIf(txtRetension.Text = "", 0, CDbl(txtRetension.Text))
                param_MontoRetension.Direction = ParameterDirection.Input

                Dim param_IVA As New SqlClient.SqlParameter
                param_IVA.ParameterName = "@IVA"
                param_IVA.SqlDbType = SqlDbType.Bit
                param_IVA.Value = chkAplicarIvaParcial.Checked
                param_IVA.Direction = ParameterDirection.Input

                'Dim param_PorcentajeIVA As New SqlClient.SqlParameter
                'param_PorcentajeIVA.ParameterName = "@porcentajeIVA"
                'param_PorcentajeIVA.SqlDbType = SqlDbType.Decimal
                'param_PorcentajeIVA.Size = 18
                'param_PorcentajeIVA.Value = IIf(chkAplicarIvaParcial.Checked = True, IIf(txtPorcIVA.Text = "", 0, CDbl(txtPorcIVA.Text)), 0)
                'param_PorcentajeIVA.Direction = ParameterDirection.Input

                Dim param_MontoIVA As New SqlClient.SqlParameter
                param_MontoIVA.ParameterName = "@montoIVA"
                param_MontoIVA.SqlDbType = SqlDbType.Decimal
                param_MontoIVA.Size = 18
                param_MontoIVA.Value = IIf(txtMontoIva.Text = "", 0, CDbl(txtMontoIva.Text))
                param_MontoIVA.Direction = ParameterDirection.Input

                Dim param_Cancelado As New SqlClient.SqlParameter
                param_Cancelado.ParameterName = "@TrabajoCancelado"
                param_Cancelado.SqlDbType = SqlDbType.Bit
                param_Cancelado.Value = Trabcancelado
                param_Cancelado.Direction = ParameterDirection.Input

                Dim param_totalapagar As New SqlClient.SqlParameter
                param_totalapagar.ParameterName = "@TotalaPagar"
                param_totalapagar.SqlDbType = SqlDbType.Decimal
                param_totalapagar.Size = 18
                param_totalapagar.Value = IIf(lblTotalaPagar.Text = "", 0, CDbl(lblTotalaPagar.Text))
                param_totalapagar.Direction = ParameterDirection.Input

                Dim param_entregado As New SqlClient.SqlParameter
                param_entregado.ParameterName = "@entregado"
                param_entregado.SqlDbType = SqlDbType.Decimal
                param_entregado.Size = 18
                param_entregado.Value = IIf(lblEntregado.Text = "", 0, CDbl(lblEntregado.Text))
                param_entregado.Direction = ParameterDirection.Input

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
                            param_IdIngreso, param_IdPresupuesto, param_TipoTrabajo, _
                            param_Cuotas, param_TipoCuotas, param_CantCuotas, param_Retensiones, param_MontoRetension, _
                            param_IVA, param_MontoIVA, param_totalapagar, param_entregado, param_Cancelado, _
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

    Private Function AgregarActualizar_DetallePago() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try

            Try
                Dim param_IdIngresoDetalle As New SqlClient.SqlParameter
                param_IdIngresoDetalle.ParameterName = "@IdIngresoDetalle"
                param_IdIngresoDetalle.SqlDbType = SqlDbType.BigInt
                param_IdIngresoDetalle.Value = DBNull.Value
                param_IdIngresoDetalle.Direction = ParameterDirection.Output

                Dim param_IdIngreso As New SqlClient.SqlParameter
                param_IdIngreso.ParameterName = "@IdIngreso"
                param_IdIngreso.SqlDbType = SqlDbType.BigInt
                param_IdIngreso.Value = Ingreso 'CLng(lblNroPresupuesto.Text)
                param_IdIngreso.Direction = ParameterDirection.Input

                Dim param_fechapago As New SqlClient.SqlParameter
                param_fechapago.ParameterName = "@fechapago"
                param_fechapago.SqlDbType = SqlDbType.DateTime
                param_fechapago.Value = "01/01/1900" 'dtiFechaDatosPerfo.Value
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
                param_MontoTarjeta.Size = 18
                param_MontoTarjeta.Value = IIf(txtEntregaTarjeta.Text = "", 0, CDbl(txtEntregaTarjeta.Text))
                param_MontoTarjeta.Direction = ParameterDirection.Input

                Dim param_PorcentajeRecargo As New SqlClient.SqlParameter
                param_PorcentajeRecargo.ParameterName = "@porcentajerecargotarjeta"
                param_PorcentajeRecargo.SqlDbType = SqlDbType.Decimal
                param_PorcentajeRecargo.Size = 18
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
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Detalles_Insert", _
                            param_IdIngreso, param_IdIngresoDetalle, param_fechapago, param_Contado, _
                            param_MontoContado, param_Tarjeta, param_NombreTarjeta, param_MontoTarjeta, _
                            param_PorcentajeRecargo, param_Cheque, _
                            param_useradd, param_res)

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

    Private Function AgregarActualizar_Cuotas() As Integer

        Dim res As Integer = 0
        Dim MontoCuota As Double
        Dim FechaVenc As Date

        Try
            Try
                MontoCuota = CDbl(lblTotalaPagar.Text) / iiCantidadCuotas.Value
                FechaVenc = dtiPrimerCuota.Value

                For i = 0 To iiCantidadCuotas.Value - 1

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = Ingreso
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_nrocuota As New SqlClient.SqlParameter
                    param_nrocuota.ParameterName = "@NroCuota"
                    param_nrocuota.SqlDbType = SqlDbType.SmallInt
                    param_nrocuota.Value = i + 1
                    param_nrocuota.Direction = ParameterDirection.Input

                    Dim param_MontoCuota As New SqlClient.SqlParameter
                    param_MontoCuota.ParameterName = "@montoCuota"
                    param_MontoCuota.SqlDbType = SqlDbType.Decimal
                    param_MontoCuota.Size = 18
                    param_MontoCuota.Value = MontoCuota
                    param_MontoCuota.Direction = ParameterDirection.Input

                    Dim param_FechaVenc As New SqlClient.SqlParameter
                    param_FechaVenc.ParameterName = "@FechaVencimiento"
                    param_FechaVenc.SqlDbType = SqlDbType.DateTime
                    param_FechaVenc.Value = FechaVenc
                    param_FechaVenc.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Cuotas_Insert", _
                                param_IdIngreso, param_nrocuota, param_MontoCuota, param_FechaVenc, param_res)

                        If param_res.Value < 0 Then
                            AgregarActualizar_Cuotas = -1
                            Exit Function

                        End If

                        If cmbTipoCuota.SelectedItem.ToString = "Mensual" Then
                            FechaVenc = DateAdd(DateInterval.Day, 30, FechaVenc)
                        End If

                        If cmbTipoCuota.SelectedItem.ToString = "Quincenal" Then
                            FechaVenc = DateAdd(DateInterval.Day, 15, FechaVenc)
                        End If


                    Catch ex As Exception
                        Throw ex
                        AgregarActualizar_Cuotas = -1
                        Exit Function
                    End Try
                Next

                If dtiPrimerCuota.Value = Date.Today.Date Then

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = Ingreso
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_IdIngresoDetalle As New SqlClient.SqlParameter
                    param_IdIngresoDetalle.ParameterName = "@IdIngresoDetalle"
                    param_IdIngresoDetalle.SqlDbType = SqlDbType.BigInt
                    param_IdIngresoDetalle.Value = IngresoDetalle
                    param_IdIngresoDetalle.Direction = ParameterDirection.Input

                    Dim param_nrocuota As New SqlClient.SqlParameter
                    param_nrocuota.ParameterName = "@NroCuota"
                    param_nrocuota.SqlDbType = SqlDbType.SmallInt
                    param_nrocuota.Value = 1
                    param_nrocuota.Direction = ParameterDirection.Input

                    Dim param_Cancelada As New SqlClient.SqlParameter
                    param_Cancelada.ParameterName = "@Cancelada"
                    param_Cancelada.SqlDbType = SqlDbType.Bit
                    param_Cancelada.Value = True
                    param_Cancelada.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Cuotas_Update", _
                                param_IdIngreso, param_IdIngresoDetalle, param_nrocuota, param_Cancelada, param_res)

                        If param_res.Value < 0 Then
                            AgregarActualizar_Cuotas = -1
                            Exit Function

                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarActualizar_Cuotas = -1
                        Exit Function
                    End Try

                End If

                AgregarActualizar_Cuotas = 1

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

    Private Function AgregarActualizar_Cheques() As Integer

        Dim res As Integer = 0

        Try
            Try
                For i = 0 To grd.RowCount - 2

                    Dim param_IngresoDetalle As New SqlClient.SqlParameter
                    param_IngresoDetalle.ParameterName = "@Ingreso_Detalle"
                    param_IngresoDetalle.SqlDbType = SqlDbType.Bit
                    param_IngresoDetalle.Value = IngresoDetalle
                    param_IngresoDetalle.Direction = ParameterDirection.Input

                    Dim param_PerteneceaCuota As New SqlClient.SqlParameter
                    param_PerteneceaCuota.ParameterName = "@Ingreso_Cheque"
                    param_PerteneceaCuota.SqlDbType = SqlDbType.Bit
                    param_PerteneceaCuota.Value = chkCheque.Checked
                    param_PerteneceaCuota.Direction = ParameterDirection.Input

                    Dim param_nrocuota As New SqlClient.SqlParameter
                    param_nrocuota.ParameterName = "@Cuota"
                    param_nrocuota.SqlDbType = SqlDbType.SmallInt
                    If chkCuotas.Checked = True Then
                        param_nrocuota.Value = 1
                    Else
                        param_nrocuota.Value = DBNull.Value
                    End If
                    param_nrocuota.Direction = ParameterDirection.Input

                    Dim param_NroCheque As New SqlClient.SqlParameter
                    param_NroCheque.ParameterName = "@NroCheque"
                    param_NroCheque.SqlDbType = SqlDbType.BigInt
                    param_NroCheque.Value = grd.Rows(i).Cells(0).Value
                    param_NroCheque.Direction = ParameterDirection.Input

                    Dim param_IdCliente As New SqlClient.SqlParameter
                    param_IdCliente.ParameterName = "@IdCliente"
                    param_IdCliente.SqlDbType = SqlDbType.Int
                    param_IdCliente.Value = 0 'grd.Rows(i).Cells(0).Value
                    param_IdCliente.Direction = ParameterDirection.Input

                    Dim param_ClienteChequeBco As New SqlClient.SqlParameter
                    param_ClienteChequeBco.ParameterName = "@ClienteChequeBco"
                    param_ClienteChequeBco.SqlDbType = SqlDbType.NVarChar
                    param_ClienteChequeBco.Size = 50
                    param_ClienteChequeBco.Value = grd.Rows(i).Cells(4).Value
                    param_ClienteChequeBco.Direction = ParameterDirection.Input

                    Dim param_FechaCobro As New SqlClient.SqlParameter
                    param_FechaCobro.ParameterName = "@FechaCobro"
                    param_FechaCobro.SqlDbType = SqlDbType.DateTime
                    param_FechaCobro.Value = grd.Rows(i).Cells(3).Value
                    param_FechaCobro.Direction = ParameterDirection.Input

                    Dim param_Moneda As New SqlClient.SqlParameter
                    param_Moneda.ParameterName = "@IdMoneda"
                    param_Moneda.SqlDbType = SqlDbType.Int
                    param_Moneda.Value = grd.Rows(i).Cells(5).Value
                    param_Moneda.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Int
                    param_Monto.Value = grd.Rows(i).Cells(2).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_Banco As New SqlClient.SqlParameter
                    param_Banco.ParameterName = "@Banco"
                    param_Banco.SqlDbType = SqlDbType.NVarChar
                    param_Banco.Size = 50
                    param_Banco.Value = grd.Rows(i).Cells(1).Value
                    param_Banco.Direction = ParameterDirection.Input

                    Dim param_Observaciones As New SqlClient.SqlParameter
                    param_Observaciones.ParameterName = "@Observaciones"
                    param_Observaciones.SqlDbType = SqlDbType.NVarChar
                    param_Observaciones.Size = 100
                    param_Observaciones.Value = grd.Rows(i).Cells(6).Value
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
                                param_PerteneceaCuota, param_IngresoDetalle, param_nrocuota, param_NroCheque, param_IdCliente, _
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

    Private Function ActualizarDatos() As Integer

        Try
            Dim param_idpresupuesto As New SqlClient.SqlParameter
            param_idpresupuesto.ParameterName = "@idpresupuesto"
            param_idpresupuesto.SqlDbType = SqlDbType.BigInt
            param_idpresupuesto.Value = 0 'lblNroPresupuesto.Text
            param_idpresupuesto.Direction = ParameterDirection.Input

            Dim param_montofinal As New SqlClient.SqlParameter
            param_montofinal.ParameterName = "@montofinal"
            param_montofinal.SqlDbType = SqlDbType.Int
            param_montofinal.Value = CDbl(lblTotalaPagar.Text)
            param_montofinal.Direction = ParameterDirection.Input

            Dim param_useradd As New SqlClient.SqlParameter
            param_useradd.ParameterName = "@userupd"
            param_useradd.SqlDbType = SqlDbType.SmallInt
            param_useradd.Value = UserID
            param_useradd.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = 0
            param_res.Direction = ParameterDirection.InputOutput

            Try
                'If rbPerforacion.Checked = True Then
                '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPerforaciones_CierreTrabajo", _
                '        param_idpresupuesto, param_montofinal, param_useradd, param_res)
                'End If

                'If rbHidrogrua.Checked = True Then
                '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGrua_CierreTrabajo", _
                '        param_idpresupuesto, param_montofinal, param_useradd, param_res)
                'End If

                'If rbPala.Checked = True Then
                '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPala_CierreTrabajo", _
                '        param_idpresupuesto, param_montofinal, param_useradd, param_res)
                'End If

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
        'Cierra o finaliza la transacción
        If Not (tran Is Nothing) Then
            tran.Commit()
            tran.Dispose()
            tran = Nothing
        End If
    End Sub

    Private Sub Cancelar_Tran()
        'Cancela la transacción
        If Not (tran Is Nothing) Then
            tran.Rollback()
            tran.Dispose()
            tran = Nothing
        End If
    End Sub

#End Region

#Region "Procedimientos"

    Private Sub Imprimir_Reporte()
        nbreformreportes = "Comprobante de Pago"
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim reporte_presupuestoperfo As New frmReportes

        'reporte_presupuestoperfo.MostrarReporte_ComprobantePago(0, reporte_presupuestoperfo, TipoTrabajo) 'grd.CurrentRow.Cells(2).Value) 'grd.CurrentRow.Cells(2).Value.ToString.ToUpper)
        Cursor = System.Windows.Forms.Cursors.Default
    End Sub

#End Region

End Class