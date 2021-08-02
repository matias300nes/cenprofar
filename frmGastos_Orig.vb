Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmGastos_ORIG

    Dim permitir_evento_CellChanged As Boolean

    Dim codigoconsumovale As String
    Dim llenandoCombo As Boolean = False
    Dim llenandoCombo2 As Boolean = False, bolpoliticas As Boolean
    'Variables para la grilla
    Dim editando_celda As Boolean, RefrescarGrid As Boolean
    Dim FILA As Integer, COLUMNA As Integer
    Private ds_2 As DataSet
    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer, Cell_Y As Integer
    'Para el combo de busqueda
    Dim ID_Buscado As Long, Nombre_Buscado As Long
    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing
    Private ht As New System.Collections.Hashtable() 'usada para almacenar el idCliente de la tabla proyectos

    Public Shadows Event ev_CellChanged As EventHandler


#Region "   Eventos"

    Private Sub frmGastosporObra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()

        dtiFechaGasto.Value = Date.Today
        dtiFechaPago.Value = Date.Today

        LlenarComboClientes(cmbCliente)
        LlenarComboObras()
        LlenarComboProveedores(cmbProveedor)
        'LlenarcmbTarjetas(cmbTarjetas, ConnStringSEI)


        asignarTags()

        SQL = "exec spGastos_Select_All"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        CargarCajasPagos()
        PrepararBotones()

        grd.Columns(1).Visible = False
        grd.Columns(2).Visible = False

        dtiFechaGasto.Focus()

        'ajuste de la grilla de navegación según el tamaño de los datos

    End Sub

    Private Sub frmGastosporObras_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                btnNuevo_Click(sender, e)
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
                'Case Keys.F5 'eliminar
                '    btnEliminar_Click(sender, e)
        End Select
    End Sub

    Private Sub PicClientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicClientes.Click
        Dim f As New frmClientes

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbCliente.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarComboProveedores(cmbCliente)
        cmbCliente.Text = texto_del_combo
    End Sub

    Private Sub PicObras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicObras.Click
        Dim f As New frmObras

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbObras.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarComboObras()
        cmbObras.Text = texto_del_combo
    End Sub

    Private Sub txtNota_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNota.KeyDown, txtNroGasto.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbClientes_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedValueChanged, cmbCliente.SelectedValueChanged
        If llenandoCombo = False Then
            LlenarComboObras()
        End If
    End Sub

    Private Sub txtIVA_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIVA.LostFocus
        txtTotal.Text = CDbl(IIf(txtIVA.Text = "", 0, txtIVA.Text)) + CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text))
    End Sub

    Private Sub txtSubtotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotal.LostFocus
        txtTotal.Text = CDbl(IIf(txtIVA.Text = "", 0, txtIVA.Text)) + CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text))
    End Sub

    Private Sub chkCheque_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheque.CheckedChanged
        'Dim I As Integer

        gpDetallePropios.Enabled = chkCheque.Checked
        rbPropios.Checked = chkCheque.Checked

        lblEntregaCheque.Enabled = chkCheque.Checked

        If chkCheque.Checked = False Then
            lblEntregaCheque.Text = "0"
        End If
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

    Private Sub chkObra_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkObra.CheckedChanged
        cmbObras.Enabled = chkObra.Checked
        cmbCliente.Enabled = chkObra.Checked
        PicClientes.Enabled = chkObra.Checked
        PicObras.Enabled = chkObra.Checked
        lblObra.Enabled = chkObra.Checked
        lblCliente.Enabled = chkObra.Checked
    End Sub

    Private Sub rbPropios_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPropios.CheckedChanged
        gpDetallePropios.Visible = rbPropios.Checked
        gpDetalle.Visible = Not rbPropios.Checked
    End Sub

    Private Sub rbOtros_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbOtros.CheckedChanged
        gpDetallePropios.Visible = Not rbOtros.Checked
        gpDetalle.Visible = rbOtros.Checked
    End Sub

    Private Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            CargarCajasPagos()
        End If
        RaiseEvent ev_CellChanged(sender, e) 'por ahora lo usa el formulario entryline
    End Sub

    Private Sub txtTipoFactura_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTipoFactura.KeyPress, _
      txtNroRemitoFactura.KeyPress, txtIVA.KeyPress, txtSubtotal.KeyPress, txtTotal.KeyPress, txtNota.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        Util.LimpiarTextBox(Me.Controls)
        LimpiarCajasItems()
        PrepararBotones()
        chkObra.Checked = False
        chkCuentaCorriente.Checked = False
        cmbObras.Enabled = False
        cmbCliente.Enabled = False
        PicClientes.Enabled = False
        PicObras.Enabled = False
        lblObra.Enabled = False
        lblCliente.Enabled = False
        rbPropios.Checked = True
        grdChequesAsignados.Rows.Clear()
        grdChequesPropios.Rows.Clear()

        Dim I As Integer
        Dim SQLTXT3 As String
        Dim connection As SqlClient.SqlConnection = Nothing

        SQLTXT3 = "SELECT IDCHEQUE, NROCHEQUE, BANCO, MONTO FROM CHEQUES WHERE UTILIZADO = 0"

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dt1 As New DataTable
        Dim cmd1 As New SqlCommand(SQLTXT3, connection)
        Dim da1 As New SqlDataAdapter(cmd1)

        da1.Fill(dt1)

        For I = 0 To dt1.Rows.Count - 1
            grdChequesPropios.Rows.Add(dt1.Rows(I)("nrocheque").ToString(), dt1.Rows(I)("banco").ToString(), dt1.Rows(I)("monto").ToString(), dt1.Rows(I)("IDCHEQUE").ToString())
        Next

        dtiFechaGasto.Value = Date.Today
        dtiFechaGasto.Focus()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        Util.Logueado_OK = False

        If Not (grd.CurrentRow Is Nothing) Then
            If bolModo = False And CBool(grd.CurrentRow.Cells(24).Value) = True Then
                MsgBox("La Obra ya fue finalizada, no puede realizar modificaciones en los gastos", MsgBoxStyle.Information, "Control de Obras")
                Exit Sub
            End If
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro()
                If bolModo = True Then
                    Select Case res
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo agregar el registro. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case Else
                            If chkCheque.Checked = True Then
                                res = AgregarActualizar_Cheques()
                                Select Case res
                                    Case 0
                                        Util.MsgStatus(Status1, "No se pudo agregar el cheque. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap)
                                        Cancelar_Tran()
                                    Case -1
                                        Util.MsgStatus(Status1, "No se pudo agregar el cheque.", My.Resources.Resources.stop_error.ToBitmap)
                                        Cancelar_Tran()
                                    Case Else
                                        Util.MsgStatus(Status1, "Se ha insertado correctamente el registro.", My.Resources.Resources.ok.ToBitmap)
                                        Cerrar_Tran()
                                        btnActualizar_Click(sender, e)
                                        btnCancelar_Click(sender, e)
                                End Select
                            Else
                                Util.MsgStatus(Status1, "Se ha insertado correctamente el registro.", My.Resources.Resources.ok.ToBitmap)
                                Cerrar_Tran()
                                btnActualizar_Click(sender, e)
                                btnCancelar_Click(sender, e)
                            End If
                    End Select
                Else
                    Select Case res
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap)
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado correctamente el registro.", My.Resources.Resources.ok.ToBitmap)
                            btnActualizar_Click(sender, e)
                            btnCancelar_Click(sender, e)
                    End Select
                End If
            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        ''''''''''''''''para posicionarme en la fila actual...
        Dim registro As Integer 'DataGridViewRow
        registro = grd.CurrentRow.Index


    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        'nbreformreportes = "Listado de Consumos"

        Dim Primer As Date

        Dim paramConsumos As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim consulta As String, proveedor As String
        Dim desde As Date, hasta As Date
        Dim ReporteGastos As New frmReportes

        'es igual el select de LlenarComboRetira()..
        consulta = "SELECT distinct Proveedor as codigo FROM Gastos ORDER BY proveedor"

        Primer = DateSerial(Year(Date.Now), Month(Date.Now) + 0, 1)
        paramConsumos.AgregarParametros("Desde: ", "DATE", "", False, Primer, "", cnn)
        paramConsumos.AgregarParametros("Hasta: ", "DATE", "", False, Date.Now, "", cnn)
        paramConsumos.AgregarParametros("Proveedor: ", "STRING", "", False, , consulta, cnn)

        paramConsumos.ShowDialog()
        If cerroparametrosconaceptar = True Then
            desde = paramConsumos.ObtenerParametros(0)
            hasta = paramConsumos.ObtenerParametros(1)
            proveedor = paramConsumos.ObtenerParametros(2)

            'ReporteGastos.MostrarMovimientosProveedores_SinDetalledePagos(desde, hasta, proveedor, ReporteGastos)

            cerroparametrosconaceptar = False
            paramConsumos = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Sub btnAgregarChequePropio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarChequePropio.Click
        Dim I As Integer

        For I = 0 To grdChequesAsignados.Rows.Count - 1
            If grdChequesPropios.CurrentRow.Cells(0).Value = grdChequesAsignados.Rows(I).Cells(0).Value Then
                MsgBox("El item seleccionado ya fue cargado, verifique por favor", MsgBoxStyle.Exclamation, "Control de Cheques")
                Exit Sub
            End If
        Next
        grdChequesAsignados.Rows.Add(grdChequesPropios.CurrentRow.Cells(0).Value, grdChequesPropios.CurrentRow.Cells(1).Value, grdChequesPropios.CurrentRow.Cells(2).Value, grdChequesPropios.CurrentRow.Cells(3).Value)
        CalcularTotalCheques()
    End Sub

    Private Sub btnEliminarChequePropio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarChequePropio.Click
        Try
            grdChequesAsignados.Rows.Remove(grdChequesAsignados.CurrentRow)
            CalcularTotalCheques()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Gastos - Pagos a Proveedores"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 200) '200'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)
        'Me.grd.Visible = False

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
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

    Private Sub CargarCajasPagos()
        If Not grd.CurrentRow Is Nothing Then

            'chkObra.Checked = CBool(grd.CurrentRow.Cells(5).Value)
            'chkCuentaCorriente.Checked = CBool(grd.CurrentRow.Cells(4).Value)

            'cmbObras.Enabled = CBool(grd.CurrentRow.Cells(5).Value)
            'cmbCliente.Enabled = CBool(grd.CurrentRow.Cells(5).Value)
            'PicClientes.Enabled = CBool(grd.CurrentRow.Cells(5).Value)
            'PicObras.Enabled = CBool(grd.CurrentRow.Cells(5).Value)
            'lblObra.Enabled = CBool(grd.CurrentRow.Cells(5).Value)
            'lblCliente.Enabled = CBool(grd.CurrentRow.Cells(5).Value)
            dtiFechaPago.Value = CDate(grd.CurrentRow.Cells(15).Value)
            chkContado.Checked = CBool(grd.CurrentRow.Cells(16).Value)
            txtEntregaContado.Text = grd.CurrentRow.Cells(17).Value
            chkTarjeta.Checked = CBool(grd.CurrentRow.Cells(18).Value)
            cmbTarjetas.Text = grd.CurrentRow.Cells(19).Value
            txtEntregaTarjeta.Text = grd.CurrentRow.Cells(20).Value
            txtRecargoTarjeta.Text = grd.CurrentRow.Cells(21).Value
            lblRecargo.Text = grd.CurrentRow.Cells(22).Value
            chkCheque.Checked = CBool(grd.CurrentRow.Cells(23).Value)
            gpDetalle.Enabled = True 'CBool(grd.CurrentRow.Cells(23).Value)
            'gpDetallePropios.Enabled = CBool(grd.CurrentRow.Cells(23).Value)

            grdCheques.Rows.Clear()
            grdChequesAsignados.Rows.Clear()

            If chkCheque.Checked Then
                Dim connection As SqlClient.SqlConnection = Nothing
                Try
                    grdChequesPropios.Rows.Clear()

                    Dim dt As New DataTable
                    Dim sqltxt2 As String
                    Dim i As Integer

                    sqltxt2 = "SELECT c.* FROM Cheques c JOIN Gastos_cheques ic ON ic.idcheque = c.idcheque WHERE idgasto = " & grd.CurrentRow.Cells(0).Value

                    Try
                        connection = SqlHelper.GetConnection(ConnStringSEI)
                    Catch ex As Exception
                        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    Dim cmd As New SqlCommand(sqltxt2, connection)
                    Dim da As New SqlDataAdapter(cmd)

                    da.Fill(dt)

                    If bolModo = False Then
                        For i = 0 To dt.Rows.Count - 1
                            grdChequesAsignados.Rows.Add(dt.Rows(i)("nrocheque").ToString(), dt.Rows(i)("banco").ToString(), dt.Rows(i)("monto").ToString(), dt.Rows(i)("IDCHEQUE").ToString())
                        Next
                    End If

                    sqltxt2 = "SELECT IDCHEQUE, NROCHEQUE, BANCO, MONTO FROM CHEQUES WHERE UTILIZADO = 0"

                    Dim dt1 As New DataTable
                    Dim cmd1 As New SqlCommand(sqltxt2, connection)
                    Dim da1 As New SqlDataAdapter(cmd1)

                    da1.Fill(dt1)

                    For i = 0 To dt1.Rows.Count - 1
                        grdChequesPropios.Rows.Add(dt1.Rows(i)("nrocheque").ToString(), dt1.Rows(i)("banco").ToString(), dt1.Rows(i)("monto").ToString(), dt1.Rows(i)("IDCHEQUE").ToString())
                    Next

                    CalcularTotalCheques()

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

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If chkCuentaCorriente.Checked = False Then
            If CDbl(lblEntregado.Text) > CDbl(txtTotal.Text) Then
                MsgBox("El monto entregado es mayor a la deuda actual.")
                Exit Sub
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
        End If

        bolpoliticas = True


    End Sub

    Private Sub asignarTags()
        txtNroGasto.Tag = "0"
        dtiFechaGasto.Tag = "3"
        chkCuentaCorriente.Tag = "4"
        chkObra.Tag = "5"
        cmbCliente.Tag = "7"
        cmbObras.Tag = "8"
        cmbProveedor.Tag = "6"
        txtTipoFactura.Tag = "9"
        txtNroRemitoFactura.Tag = "10"
        txtIVA.Tag = "11"
        txtSubtotal.Tag = "12"
        txtTotal.Tag = "13"
        txtNota.Tag = "14"

    End Sub

    Private Sub LlenarComboProveedores(ByVal cmb As System.Windows.Forms.ComboBox)
        Dim ds_Proveedores As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        llenandoCombo = True

        Try
            ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre as codigo FROM Proveedores WHERE Eliminado = 0 ORDER BY nombre asc")
            ds_Proveedores.Dispose()

            With cmb
                .DataSource = ds_Proveedores.Tables(0).DefaultView
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

    Private Sub LlenarComboClientes(ByVal cmb As System.Windows.Forms.ComboBox)
        Dim ds_Clientes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        llenandoCombo = True

        Try
            ds_Obras = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, (CODIGO + ' - ' +  NOMBRE) AS CODIGO FROM OBRAS WHERE IDCliente = " & CType(cmbCliente.SelectedValue, Long) & " AND FINALIZADO = 0 ORDER BY Codigo")
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

        dtiFechaPago.Value = Date.Today

    End Sub

    Private Sub CalcularTotalCheques()
        Dim i As Integer

        Dim a, b As Double

        For i = 0 To grdChequesAsignados.Rows.Count - 1
            a = a + grdChequesAsignados.Rows(i).Cells(2).Value
        Next

        For i = 0 To grdCheques.Rows.Count - 1
            b = b + grdCheques.Rows(i).Cells(2).Value
        Next

        lblEntregaCheque.Text = a + b

    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

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
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idGasto"
                param_id.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_id.Value = DBNull.Value
                Else
                    param_id.Value = grd.CurrentRow.Cells(0).Value
                End If
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_ctacte As New SqlClient.SqlParameter
                param_ctacte.ParameterName = "@ctacte"
                param_ctacte.SqlDbType = SqlDbType.Bit
                param_ctacte.Value = chkCuentaCorriente.Checked
                param_ctacte.Direction = ParameterDirection.Input

                Dim param_obra As New SqlClient.SqlParameter
                param_obra.ParameterName = "@obra"
                param_obra.SqlDbType = SqlDbType.Bit
                param_obra.Value = chkObra.Checked
                param_obra.Direction = ParameterDirection.Input

                Dim param_fechagasto As New SqlClient.SqlParameter
                param_fechagasto.ParameterName = "@fechagasto"
                param_fechagasto.SqlDbType = SqlDbType.DateTime
                param_fechagasto.Value = dtiFechaGasto.Value
                param_fechagasto.Direction = ParameterDirection.Input

                Dim param_idCliente As New SqlClient.SqlParameter
                param_idCliente.ParameterName = "@idCliente"
                param_idCliente.SqlDbType = SqlDbType.BigInt
                param_idCliente.Value = cmbCliente.SelectedValue
                param_idCliente.Direction = ParameterDirection.Input

                Dim param_idobra As New SqlClient.SqlParameter
                param_idobra.ParameterName = "@idobra"
                param_idobra.SqlDbType = SqlDbType.BigInt
                param_idobra.Value = cmbObras.SelectedValue
                param_idobra.Direction = ParameterDirection.Input

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@proveedor"
                param_proveedor.SqlDbType = SqlDbType.VarChar
                param_proveedor.Size = 50
                param_proveedor.Value = cmbProveedor.Text
                param_proveedor.Direction = ParameterDirection.Input

                'Dim param_fecha As New SqlClient.SqlParameter
                'param_fecha.ParameterName = "@fechafactura"
                'param_fecha.SqlDbType = SqlDbType.DateTime
                ''param_fecha.Value = dtpFECHA.Value
                'param_fecha.Direction = ParameterDirection.Input

                Dim param_tipofactura As New SqlClient.SqlParameter
                param_tipofactura.ParameterName = "@tipofactura"
                param_tipofactura.SqlDbType = SqlDbType.VarChar
                param_tipofactura.Size = 1
                param_tipofactura.Value = txtTipoFactura.Text
                param_tipofactura.Direction = ParameterDirection.Input

                Dim param_nrofactura As New SqlClient.SqlParameter
                param_nrofactura.ParameterName = "@nrofactura"
                param_nrofactura.SqlDbType = SqlDbType.VarChar
                param_nrofactura.Size = 20
                param_nrofactura.Value = txtNroRemitoFactura.Text
                param_nrofactura.Direction = ParameterDirection.Input

                Dim param_IVA As New SqlClient.SqlParameter
                param_IVA.ParameterName = "@IVA"
                param_IVA.SqlDbType = SqlDbType.Decimal
                param_IVA.Size = 18
                param_IVA.Value = IIf(txtIVA.Text = "", 0, txtIVA.Text)
                param_IVA.Direction = ParameterDirection.Input

                Dim param_Subtotal As New SqlClient.SqlParameter
                param_Subtotal.ParameterName = "@Subtotal"
                param_Subtotal.SqlDbType = SqlDbType.Decimal
                param_Subtotal.Size = 18
                param_Subtotal.Value = IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)
                param_Subtotal.Direction = ParameterDirection.Input

                Dim param_Total As New SqlClient.SqlParameter
                param_Total.ParameterName = "@Total"
                param_Total.SqlDbType = SqlDbType.Decimal
                param_Total.Size = 18
                param_Total.Value = IIf(txtTotal.Text = "", 0, txtTotal.Text)
                param_Total.Direction = ParameterDirection.Input

                Dim param_descripcion As New SqlClient.SqlParameter
                param_descripcion.ParameterName = "@descripcion"
                param_descripcion.SqlDbType = SqlDbType.VarChar
                param_descripcion.Size = 200
                param_descripcion.Value = txtNota.Text
                param_descripcion.Direction = ParameterDirection.Input

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
                param_PorcentajeRecargo.ParameterName = "@porcentajetarj"
                param_PorcentajeRecargo.SqlDbType = SqlDbType.Decimal
                param_PorcentajeRecargo.Value = IIf(txtRecargoTarjeta.Text = "", 0, CDbl(txtRecargoTarjeta.Text))
                param_PorcentajeRecargo.Direction = ParameterDirection.Input

                Dim param_Cheque As New SqlClient.SqlParameter
                param_Cheque.ParameterName = "@Cheque"
                param_Cheque.SqlDbType = SqlDbType.Bit
                param_Cheque.Value = chkCheque.Checked
                param_Cheque.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                If bolModo = True Then
                    param_useradd.ParameterName = "@useradd"
                Else
                    param_useradd.ParameterName = "@userupd"
                End If
                param_useradd.SqlDbType = SqlDbType.Int
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Insert", _
                                              param_id, param_ctacte, param_obra, param_fechagasto, param_idCliente, param_idobra, param_proveedor, _
                                              param_tipofactura, param_nrofactura, param_IVA, param_Subtotal, param_Total, _
                                              param_descripcion, param_fechapago, param_Contado, param_MontoContado, param_Tarjeta, _
                                              param_NombreTarjeta, param_MontoTarjeta, param_PorcentajeRecargo, param_Cheque, _
                                              param_useradd, param_res)

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Update", _
                                                param_id, param_ctacte, param_obra, param_fechagasto, param_idCliente, param_idobra, param_proveedor, _
                                              param_tipofactura, param_nrofactura, param_IVA, param_Subtotal, param_Total, _
                                              param_descripcion, param_fechapago, param_Contado, param_MontoContado, param_Tarjeta, _
                                              param_NombreTarjeta, param_MontoTarjeta, param_PorcentajeRecargo, param_Cheque, _
                                              param_useradd, param_res)

                    End If

                    txtID.Text = param_id.Value
                    res = param_res.Value

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            ''Abrir una transaccion para guardar y asegurar que se guarda todo
            'If Abrir_Tran(conn_del_form) = False Then
            '    MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Function
            'End If

            Try

                Dim param_idconsumo As New SqlClient.SqlParameter("@idconsumo", SqlDbType.BigInt, ParameterDirection.Input)
                param_idconsumo.Value = CType(txtNroGasto.Text, Long)
                param_idconsumo.Direction = ParameterDirection.Input

                Dim param_userdel As New SqlClient.SqlParameter
                param_userdel.ParameterName = "@userdel"
                param_userdel.SqlDbType = SqlDbType.Int
                param_userdel.Value = UserID
                param_userdel.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Dim param_msg As New SqlClient.SqlParameter
                param_msg.ParameterName = "@mensaje"
                param_msg.SqlDbType = SqlDbType.VarChar
                param_msg.Size = 250
                param_msg.Value = DBNull.Value
                param_msg.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Delete_All", param_idconsumo, param_userdel, param_res, param_msg)
                    res = param_res.Value
                    msg = param_msg.Value

                    'If res > 0 Then Util.BorrarGrilla(grd)

                    EliminarRegistro = res

                Catch ex As Exception
                    '' 


                    Throw ex
                End Try
            Finally
                ''
            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

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
    End Function

    Private Function ActualizarConsumoImpreso(ByVal idconsumo As Long) As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = idconsumo
                param_id.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Consumos_Update_Impreso", param_id, param_res)
                    res = param_res.Value
                    ActualizarConsumoImpreso = res

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
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
                param_Ingreso.ParameterName = "@IdGasto"
                param_Ingreso.SqlDbType = SqlDbType.BigInt
                param_Ingreso.Value = txtID.Text
                param_Ingreso.Direction = ParameterDirection.Input

                For I = 0 To grdCheques.RowCount - 2

                    Dim param_IngresoCheque As New SqlClient.SqlParameter
                    param_IngresoCheque.ParameterName = "@IdGasto_Cheque"
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
                    param_IdCliente.Value = 0
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

                    Dim param_Utilizado As New SqlClient.SqlParameter
                    param_Utilizado.ParameterName = "@Utilizado"
                    param_Utilizado.SqlDbType = SqlDbType.Bit
                    param_Utilizado.Value = True
                    param_Utilizado.Direction = ParameterDirection.Input

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

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_Insert2", _
                                param_Ingreso, param_IngresoCheque, param_NroCheque, param_IdCliente, _
                                param_ClienteChequeBco, param_FechaCobro, param_Moneda, param_Monto, _
                                param_Banco, param_Observaciones, param_Utilizado, param_useradd, param_res)

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
            'LimpiarCajasItems()
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

        'lblEntregaCheque.Text = CDbl(lblEntregaCheque.Text) + CDbl(txtMontoCheque.Text)
        CalcularTotalCheques()
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


    Private Sub chkCuentaCorriente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCuentaCorriente.CheckedChanged
        gpPago.Enabled = Not chkCuentaCorriente.Checked
    End Sub

    Private Sub txtPeriodo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not (Char.IsControl(e.KeyChar) Or Char.IsDigit(e.KeyChar) Or Char.IsLetter("/")) Then
            e.Handled = True
        End If
    End Sub

End Class


