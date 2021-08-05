Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmPagodeClientes

    Dim bolpoliticas As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean
    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    Dim NombreTarjeta As String

    Dim NroEleccion As Integer = 0
    Dim UltimoNum As Integer = 0

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Para el combo de busqueda
    Dim ID_Buscado As Long
    Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    Dim band As Integer, bandIVA As Boolean
    'BANDIVA SE UTILIZA PARA SABER SI EXISTEN VARIOS PORCENTAJES DE IVA DIFERENTES EN EL PAGO

    Dim RemitosAsociados As String

    'Dim IVA As Double
    'Dim ValorCambioDolar As Double

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IdPresGes = 0
        Cod_Material = 1
        Nombre_Material = 2
        PrecioUni = 3
        Subtotal = 4
    End Enum

    Enum ColumnasDelGridFacturasConsumos
        Id = 0
        FechaFactura = 1
        Tipo = 2
        NroFactura = 3
        'NroOC = 4
        'Subtotal = 5
        'Iva = 6
        'MontoIva = 7
        Total = 4
        'CondicionVta = 9
        ' CondicionIva = 10
        Seleccionar = 5
        Deuda = 6
        Nota = 7
        NroMov = 8
        'ValorDolarFacturado = 13
    End Enum

    Enum ColumnasDelGridImpuestos
        Id = 0
        codigo = 1
        NroDocumento = 2
        Monto = 3
        IdIngreso = 4
        IdImpuestoxIngreso = 5
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim permitir_evento_CellChanged As Boolean


#Region "   Eventos"

    Private Sub frmFacturacion_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Facturas(CType(cmbClientes.SelectedValue, Long))
            End If
        End If
    End Sub

    Private Sub frmFacturacion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la Factura Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Nueva Factura?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmFacturacion_Gestion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btnEliminar.Text = "Anular Pago"

        band = 0
        configurarform()
        asignarTags()

        LlenarComboRepartidor()
        LlenarComboClientes("00")
        LlenarComboBancos()
        LlenarComboCuentasOrigen()
        LlenarcomboTarjetas()

        SQL = "exec spIngresos_Select_All @Eliminado = 0"

        LlenarGrilla()

        Permitir = True

        CargarCajas()

        PrepararBotones()

        'Setear_Grilla()

        If bolModo = True Then
            LlenarGrid_Facturas(CType(cmbClientes.SelectedValue, Long))
            btnNuevo_Click(sender, e)
        Else
            AgregarRemito_tmp()
            LlenarGrid_Facturas(CType(grd.CurrentRow.Cells(19).Value, Long))
            grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Seleccionar).Visible = False
        End If

        txtCliente.Visible = Not bolModo
        cmbRepartidor.Enabled = bolModo

        chkDescuento.Enabled = bolModo
        txtDescuento.Enabled = bolModo

        btnNuevoCheque.Enabled = bolModo
        btnNuevoTarjeta.Enabled = bolModo
        btnNuevoTransferencia.Enabled = bolModo

        btnAgregarCheque.Enabled = bolModo
        btnAgregarTarjeta.Enabled = bolModo
        btnAgregarTransf.Enabled = bolModo

        btnModificarCheque.Enabled = bolModo
        btnModificarTarjeta.Enabled = bolModo
        btnModificarTransf.Enabled = bolModo

        btnEliminarCheque.Enabled = bolModo
        btnEliminarTarjeta.Enabled = bolModo
        btnEliminarTransf.Enabled = bolModo


        permitir_evento_CellChanged = True

        grd.Columns(4).Visible = False
        grd.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(6).Visible = False
        grd.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(9).Visible = False
        grd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(11).Visible = False
        grd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(13).Visible = False
        grd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(15).Visible = False
        grd.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(17).Visible = False
        'grd.Columns(18).Visible = False
        grd.Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(19).Visible = False
        grd.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(22).Visible = False
        grd.Columns(23).Visible = False
        grd.Columns(24).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd_CurrentCellChanged(sender, e)

        grd.Columns(25).Visible = False

        dtpFECHA.MaxDate = Today.Date
        'dtpFechaCheque.MaxDate = Today.Date
        dtpFechaTransf.MaxDate = Today.Date

        band = 1

    End Sub

    'Private Sub txtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
    '    If txtID.Text <> "" And bolModo = False Then
    '        'LlenarGridFacturas(CType(cmbCliente.SelectedValue, Long))
    '        LlenarGridFacturas(CType(grd.CurrentRow.Cells(19).Value, Long))
    '    End If
    'End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtCODIGO.KeyPress, _
        txtNroCheque.KeyPress, txtNroOpCliente.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtMontoCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoCheque.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAgregarCheque.Focus()
        End If
    End Sub

    Private Sub txtMontoTransf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoTransf.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAgregarTransf.Focus()
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFECHA.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub grdRemitos_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdFacturasConsumos.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged
        If band = 1 Then
            If bolModo = True Then
                NroEleccion = 0
                LlenarGrid_Facturas(CType(cmbClientes.SelectedValue, Long))

                lblTotalaPagarSinIva.Text = "0"
                lblTotalaPagar.Text = "0"
                lblTotal.Text = "0"

                lblIVA.Text = "0"
                lblSubtotal.Text = "0"
            Else
                LlenarGrid_Facturas(CType(grd.CurrentRow.Cells(19).Value, Long))
            End If

        End If
    End Sub

    Private Sub grdFacturasConsumos_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFacturasConsumos.CurrentCellDirtyStateChanged
        If grdFacturasConsumos.IsCurrentCellDirty Then
            grdFacturasConsumos.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub grdRemitos_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFacturasConsumos.CellValueChanged
        Try

            If e.ColumnIndex = ColumnasDelGridFacturasConsumos.Seleccionar Then

                UltimoNum = 0

                AgregarRemito_tmp()

                If bolModo = True And CBool(grdFacturasConsumos.CurrentRow.Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                    NroEleccion = NroEleccion + 1
                    grdFacturasConsumos.CurrentRow.Cells(ColumnasDelGridFacturasConsumos.NroMov).Value = NroEleccion
                ElseIf bolModo = True And CBool(grdFacturasConsumos.CurrentRow.Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = False Then
                    Dim i As Integer
                    Dim nroActual As Integer
                    nroActual = grdFacturasConsumos.CurrentRow.Cells(ColumnasDelGridFacturasConsumos.NroMov).Value
                    grdFacturasConsumos.CurrentRow.Cells(ColumnasDelGridFacturasConsumos.NroMov).Value = DBNull.Value

                    For i = 0 To grdFacturasConsumos.Rows.Count - 1
                        If CBool(grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                            If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroMov).Value = nroActual + 1 Then
                                grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroMov).Value = grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroMov).Value - 1
                                nroActual = nroActual + 1
                            End If
                        End If
                    Next
                    NroEleccion = NroEleccion - 1
                End If
                UltimoNum = NroEleccion
            End If

        Catch ex As Exception
            MsgBox("Error en Sub grdRemitos_CellClick", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Calcular_MontoEntregado()

        Dim redondeo As Double = 0

        Try
            redondeo = CDbl(IIf(txtRedondeo.Text = "", 0, txtRedondeo.Text))

            Dim Anti As Double = 0
            If ChkAyIDep.Checked = True Then
                Anti = CDbl(IIf(txtAyIDepUsado.Text = "", 0, txtAyIDepUsado.Text))
            End If

            lblEntregado.Text = CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(lblEntregaCheques.Text = "", 0, lblEntregaCheques.Text)) + CDbl(IIf(lblEntregaTransferencias.Text = "", 0, lblEntregaTransferencias.Text)) + CDbl(IIf(lblEntregaImpuestos.Text = "", 0, lblEntregaImpuestos.Text)) + CDbl(IIf(lblEntregaTarjetas.Text = "", 0, lblEntregaTarjetas.Text)) + Anti + redondeo

        Catch ex As Exception

        End Try

        If band = 1 Then

            lblResto.Text = Math.Round(CDbl(lblTotalaPagar.Text) - CDbl(lblEntregado.Text), 2)

            If CDec(lblEntregado.Text) > CDec(lblTotalaPagar.Text) Then
                Util.MsgStatus(Status1, "Verifique el último Monto ingresado. El monto Total Entregado ahora es mayor al monto Total a Pagar.", My.Resources.Resources.stop_error.ToBitmap)
            Else
                Util.MsgStatus(Status1, "Pago verificado. Los montos coinciden.", My.Resources.Resources.ok.ToBitmap)
            End If
        End If

    End Sub

    Private Sub txtEntregaContado_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntregaContado.TextChanged
        If band = 1 Then
            Calcular_MontoEntregado()
        End If
    End Sub

    Private Sub txtRedondeo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRedondeo.TextChanged
        If band = 1 Then
            Calcular_MontoEntregado()
        End If
    End Sub

    Private Sub txtDescuento_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescuento.TextChanged
        If band = 1 Then
            Dim Descuento As Double = 0

            If chkDescuento.Checked = True Then
                Descuento = IIf(txtDescuento.Text = "", 0, txtDescuento.Text)
            End If

            lblTotalaPagar.Text = CDbl(lblTotal.Text) - Descuento

            Calcular_MontoEntregado()

        End If
    End Sub

    Private Sub txtAyIDepUsado_TextChanged(sender As Object, e As EventArgs) Handles txtAyIDepUsado.TextChanged
        If band = 1 Then
            Calcular_MontoEntregado()
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Name Is "TabImpuestos" Then
            grdImpuestos.Focus()
        End If
        If TabControl1.SelectedTab.Name Is "TabCheques" Then
            txtNroCheque.Focus()
        End If
        If TabControl1.SelectedTab.Name Is "TabTransferencias" Then
            txtNroOpCliente.Focus()
        End If
        'modificar = 0
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then

            LimpiarGrids()

            'LlenarGridFacturas(cmbCliente.SelectedValue) 'CType(grd.CurrentRow.Cells(19).Value, Long)) '
            Try
                LlenarGrid_Facturas(CType(grd.CurrentRow.Cells(19).Value, Long))
            Catch ex As Exception
                LlenarGrid_Facturas(cmbClientes.SelectedValue)
            End Try

            AgregarRemito_tmp()

            LlenarGrid_Impuestos()

            LlenarGrid_Cheques()

            LlenarGrid_Transferencias()

            LlenarGrid_Tarjetas()

            Calcular_MontoEntregado()

        End If
    End Sub

    Private Sub chkAnulados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnulados.CheckedChanged
        btnGuardar.Enabled = Not chkAnulados.Checked
        btnEliminar.Enabled = Not chkAnulados.Checked
        btnNuevo.Enabled = Not chkAnulados.Checked
        btnCancelar.Enabled = Not chkAnulados.Checked

        If chkAnulados.Checked = True Then
            SQL = "exec spIngresos_Select_All @Eliminado = 1"
        Else
            SQL = "exec spIngresos_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

    End Sub

    Private Sub txtValorCambio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValorCambio.KeyPress
        If bolModo = True Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If txtValorCambio.Text = "" Then
                    txtValorCambio.Text = "1"
                Else
                    If CDbl(txtValorCambio.Text) < 1 Then
                        txtValorCambio.Text = "1"
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub txtValorCambio_LostFocus(sender As Object, e As EventArgs) Handles txtValorCambio.LostFocus
        If bolModo = True Then
            If txtValorCambio.Text = "" Then
                txtValorCambio.Text = "1"
            Else
                If CDbl(txtValorCambio.Text) < 1 Then
                    txtValorCambio.Text = "1"
                End If
            End If
        End If
    End Sub

    Private Sub grdImpuestos_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdImpuestos.CellEndEdit
        Try
            If e.ColumnIndex = ColumnasDelGridImpuestos.Monto Then
                If grdImpuestos.CurrentRow.Cells(ColumnasDelGridImpuestos.Monto).Value Is DBNull.Value Or _
                    grdImpuestos.CurrentRow.Cells(ColumnasDelGridImpuestos.Monto).Value = Nothing Then
                    grdImpuestos.CurrentRow.Cells(ColumnasDelGridImpuestos.Monto).Value = 0
                End If

                Dim i As Integer

                lblEntregaImpuestos.Text = "0"

                For i = 0 To grdImpuestos.Rows.Count - 1
                    lblEntregaImpuestos.Text = CDbl(lblEntregaImpuestos.Text) + CDbl(grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.Monto).Value)
                Next

                Calcular_MontoEntregado()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdImpuestos_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdImpuestos.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales2

    End Sub

    'Private Sub cmbRepartidor_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbRepartidor.DrawItem

    '    Select Case e.Index
    '        Case 0
    '            e.Graphics.FillRectangle(Brushes.LightSkyBlue, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)
    '    End Select
    'End Sub

    Private Sub cmbRepartidor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRepartidor.SelectedIndexChanged
        If band = 1 Then
            LlenarComboClientes(cmbRepartidor.SelectedValue)
        End If

    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Pagos de Clientes"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        'Dim p As New Size(GroupBox1.Size.Width, 300) 'AltoMinimoGrilla)
        'Me.grd.Size = New Size(p)

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

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbClientes.Tag = "3"
        txtCliente.Tag = "3"
        txtOrdenPago.Tag = "4"
        lblSubtotal.Tag = "5"
        lblIVA.Tag = "7"
        lblTotal.Tag = "8"
        lblTotalaPagarSinIva.Tag = "5"
        lblTotalaPagar.Tag = "8"
        txtEntregaContado.Tag = "10"
        lblEntregaCheques.Tag = "14"
        lblEntregaImpuestos.Tag = "12"
        lblEntregaTransferencias.Tag = "16"
        lblEntregaTarjetas.Tag = "18"
        txtRedondeo.Tag = "20"
        txtDescuento.Tag = "21"
        ChkAyIDep.Tag = "23"
        txtAyIDepUsado.Tag = "24"
        chkDescuento.Tag = "25"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        Dim i As Integer
        Dim unremito As Boolean = False
        Dim noChkAyID As Boolean = False

        For i = 0 To grdFacturasConsumos.RowCount - 1
            If CBool(grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                unremito = True

                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "ANTI" Or grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "IDEP" Then
                    If ChkAyIDep.Checked Then
                        If txtAyIDepUsado.Text <> "" Then
                            If CDbl(txtAyIDepUsado.Text) > 0 Then
                                noChkAyID = False
                            Else
                                noChkAyID = True
                            End If
                        Else
                            noChkAyID = True
                        End If
                    End If
                End If
            End If
        Next

        If unremito = False And bolModo = True Then
            Util.MsgStatus(Status1, "Seleccione al menos un remito para facturar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Seleccione al menos un remito para facturar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If



        If noChkAyID = True Then
            Util.MsgStatus(Status1, "Verifique el monto de Anticipo/Ingreso Dep. que desea utilizar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Verifique el monto de Anticipo/Ingreso Dep. que desea utilizar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If


        bolpoliticas = True

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

    Private Sub LlenarGrid_Facturas(ByVal IdCliente As Long)

        SQL = "exec spIngresos_ConsumosyFacturas @IdCliente = " & IdCliente & ", @modo = " & bolModo & ", @IdIngreso = " & IIf(txtID.Text = "", 0, txtID.Text)

        GetDatasetItems(grdFacturasConsumos)

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Id).Visible = False

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Tipo).Width = 37
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Tipo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Tipo).ReadOnly = True

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.NroFactura).Width = 75
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.NroFactura).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.NroFactura).ReadOnly = True

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.FechaFactura).Width = 65
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.FechaFactura).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.FechaFactura).ReadOnly = True

        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Iva).Visible = False
        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Iva).Width = 38
        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.MontoIva).Width = 65
        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.MontoIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Subtotal).Width = 70
        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Subtotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Total).Width = 70
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Total).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Total).ReadOnly = True

        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.CondicionVta).Visible = False
        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.CondicionIva).Visible = False

        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.NroOC).Width = 70

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Seleccionar).Width = 58

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Deuda).Width = 70
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Deuda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Deuda).ReadOnly = True

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Nota).Width = 275
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Nota).ReadOnly = Not bolModo

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.NroMov).Width = 30
        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Nota).ReadOnly = True


        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.ValorDolarFacturado).Width = 53
        'grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.ValorDolarFacturado).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        With grdFacturasConsumos
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black



        End With

        With grdFacturasConsumos.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdFacturasConsumos.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        If chkAnulados.Checked = True Then
            SQL = "spIngresos_Select_All @Eliminado = 1"

        Else
            SQL = "spIngresos_Select_All @Eliminado = 0"

            Dim i As Integer

            For i = 0 To grdFacturasConsumos.RowCount - 1
                grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Style.BackColor = Color.Blue
                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "IDEP" Or grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "ANTI" Then
                    grdFacturasConsumos.Rows(i).DefaultCellStyle.BackColor = Color.LightSkyBlue
                End If
            Next

        End If

    End Sub

    Private Sub LlenarGrid_Impuestos()

        SQL = "exec spImpuestos_Ingresos_Select_by_IdIngreso @IdIngreso = " & IIf(txtID.Text = "", 0, txtID.Text) & ", @Bolmodo = " & bolModo

        GetDatasetItems(grdImpuestos)

        grdImpuestos.Columns(ColumnasDelGridImpuestos.Id).Visible = False

        grdImpuestos.Columns(ColumnasDelGridImpuestos.codigo).ReadOnly = True
        grdImpuestos.Columns(ColumnasDelGridImpuestos.codigo).Width = 75

        grdImpuestos.Columns(ColumnasDelGridImpuestos.NroDocumento).Width = 120

        grdImpuestos.Columns(ColumnasDelGridImpuestos.Monto).Width = 75
        grdImpuestos.Columns(ColumnasDelGridImpuestos.Monto).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdImpuestos.Columns(ColumnasDelGridImpuestos.IdIngreso).Visible = False

        grdImpuestos.Columns(ColumnasDelGridImpuestos.IdImpuestoxIngreso).Visible = False

        With grdImpuestos
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
        End With

        With grdImpuestos.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdImpuestos.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        SQL = "spIngresos_Select_All @Eliminado = 0"

    End Sub

    Private Sub GetDatasetItems(ByVal grdchico As DataGridView)
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()

            grdchico.DataSource = ds_2.Tables(0).DefaultView

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

    End Sub

    'Private Sub LlenarComboClientes()
    '    Dim ds_Cli As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try

    '        ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT c.ID, c.nombre FROM Facturacion F " & _
    '                                                                 " JOIN Clientes c ON c.ID = F.IdCliente WHERE cancelada = 0 and f.eliminado = 0 order by C.NOMBRE")

    '        ds_Cli.Dispose()

    '        With Me.cmbClientes
    '            .DataSource = ds_Cli.Tables(0).DefaultView
    '            .DisplayMember = "nombre"
    '            .ValueMember = "id"
    '        End With

    '    Catch ex As Exception
    '        Dim errMessage As String = ""
    '        Dim tempException As Exception = ex

    '        While (Not tempException Is Nothing)
    '            errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '            tempException = tempException.InnerException
    '        End While

    '        MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '          + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '          "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try

    'End Sub

    Private Sub LlenarComboClientes(ByVal PorRepartidor As String)
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            If PorRepartidor = "Todos" Or PorRepartidor = "00" Then
                ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT C.Codigo, ltrim(rtrim(C.nombre)) as Nombre from PedidosWEB o JOIN Clientes C ON C.Codigo = o.IdCliente where o.eliminado= 0 and cancelado = 0 order by ltrim(rtrim(C.nombre))")
            Else
                ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT C.Codigo, ltrim(rtrim(C.nombre)) as Nombre from PedidosWEB o JOIN Clientes C ON C.Codigo = o.IdCliente where o.eliminado= 0 and cancelado = 0 and c.Repartidor = '" & PorRepartidor & "' order by ltrim(rtrim(C.nombre))")
            End If
            ds_Cli.Dispose()

            With Me.cmbClientes
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
            End With

            If ds_Cli.Tables(0).Rows.Count > 0 Then
                cmbClientes.SelectedIndex = 0
            End If

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

    End Sub

    Public Sub LlenarComboTarjetas()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT (Nombre + ' - ' + CONVERT(VARCHAR(10), PorcenRecar) + '%') Tarjeta, Codigo FROM Tarjetas ORDER BY Nombre")
            ds.Dispose()

            With cmbTarjetas
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Tarjeta"
                .ValueMember = "Codigo"
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarComboBancos()
        Dim ds_Bancos As Data.DataSet
        Dim ds_Bancos_Origen As Data.DataSet
        Dim ds_Bancos_Destino As Data.DataSet

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Bancos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                    " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos.Dispose()

            ds_Bancos_Origen = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                    " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos_Origen.Dispose()

            ds_Bancos_Destino = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                                " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos_Destino.Dispose()

            With Me.cmbBanco
                .DataSource = ds_Bancos.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
            End With

            With Me.cmbBancoDestino
                .DataSource = ds_Bancos_Destino.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
            End With

            With Me.cmbBancoOrigen
                .DataSource = ds_Bancos_Origen.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
            End With

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

    End Sub

    Private Sub LlenarComboCuentasOrigen()
        Dim ds_Cuentas As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cuentas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT CuentaOrigen FROM TransferenciaBancaria ORDER BY CuentaOrigen ")

            ds_Cuentas.Dispose()

            With Me.cmbCuentaOrigen
                .DataSource = ds_Cuentas.Tables(0).DefaultView
                .DisplayMember = "CuentaOrigen"
                .ValueMember = "CuentaOrigen"
            End With

            ds_Cuentas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT CuentaDestino FROM TransferenciaBancaria ORDER BY CuentaDestino ")

            ds_Cuentas.Dispose()

            With Me.cmbCuentaDestino
                .DataSource = ds_Cuentas.Tables(0).DefaultView
                .DisplayMember = "CuentaDestino"
                .ValueMember = "CuentaDestino"
            End With


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

    End Sub

    Private Sub LlenarComboRepartidor()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '00' AS 'CODIGO', '' AS 'Vendedor' union SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 and Repartidor = 1 ORDER BY Vendedor")
            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '00' AS 'CODIGO', 'Todos' AS 'Vendedor' union SELECT E.Codigo , CONCAT(E.Apellido ,' ', E.Nombre) AS 'Vendedor' FROM Empleados E join Clientes C on E.Codigo = C.Repartidor where E.Eliminado = 0 and E.Repartidor = 1 ORDER BY Codigo")
            ds.Dispose()

            With cmbRepartidor
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Vendedor"
                .ValueMember = "Codigo"
            End With






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
    End Sub

    Private Sub Imprimir()
        'nbreformreportes = "Comprobante de Pago"

        'Dim cnn As New SqlConnection(ConnStringSEI)
        'Dim Rpt As New frmReportes

        'Imprimir_LlenarTMPs(txtCODIGO.Text)

        'Rpt.MostrarReporte_PagodeClientes(txtCODIGO.Text, Rpt)

        'cnn = Nothing

    End Sub

    'Private Sub LlenarGrid_Impuestos()

    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try
    '        Dim dt As New DataTable
    '        Dim sqltxt2 As String

    '        sqltxt2 = "SELECT Codigo, NroDocumento, Monto, ISNULL(Ii.Observaciones ,'') AS Observaciones, Ii.IDimpuesto, ii.id FROM ImpuestosxIngreso ii " & _
    '                 " JOIN Impuestos I ON I.Id = ii.IdImpuesto WHERE IdIngreso = " & IIf(txtID.Text = "", 0, txtID.Text)

    '        Dim cmd As New SqlCommand(sqltxt2, connection)
    '        Dim da As New SqlDataAdapter(cmd)
    '        Dim i As Integer

    '        da.Fill(dt)

    '        For i = 0 To dt.Rows.Count - 1
    '            grdImpuestos.Rows.Add(dt.Rows(i)("CODIGO").ToString(), dt.Rows(i)("NRODOCUMENTO").ToString(), dt.Rows(i)("monto").ToString(), dt.Rows(i)("OBSERVACIONES").ToString(), dt.Rows(i)("IdImpuesto").ToString(), dt.Rows(i)("Id").ToString())
    '            'grdImpuestos.Rows.Add(cmbImpuesto.Text, txtNroDocumentoImpuesto.Text, CDec(txtMontoImpuesto.Text), txtObservacionesImpuestos.Text)
    '        Next

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try
    'End Sub

    Private Sub LlenarGrid_Cheques()
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

            sqltxt2 = "SELECT NroCheque, banco, monto, fechacobro, clientechequebco, idmoneda, observaciones, c.id, utilizado " & _
                    " FROM Cheques c JOIN ingresos_cheques ic ON ic.idcheque = c.id " & _
                    " where IdIngreso = " & IIf(txtID.Text = "", 0, txtID.Text)

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdCheques.Rows.Add(dt.Rows(i)("nrocheque").ToString(), dt.Rows(i)("banco").ToString(), dt.Rows(i)("monto").ToString(), dt.Rows(i)("fechacobro").ToString(), dt.Rows(i)("clientechequebco").ToString(), dt.Rows(i)("idmoneda").ToString(), dt.Rows(i)("Observaciones").ToString(), dt.Rows(i)("Id").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarGrid_Transferencias()
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

            sqltxt2 = " SELECT CuentaOrigen, CuentaDestino, BancoOrigen, BancoDestino, " & _
                        " IdMoneda, NroOperacionCliente, MontoTransferencia, Observaciones, FechaTransferencia, T.ID from TransferenciaBancaria t " & _
                        " JOIN TransferenciaxIngresos ti ON ti.IdTransferencia = t.Id " & _
                        " WHERE IdIngreso = " & IIf(txtID.Text = "", 0, txtID.Text)

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdTransferencias.Rows.Add(dt.Rows(i)("NroOperacionCliente").ToString(), dt.Rows(i)("CuentaOrigen").ToString(), _
                                    dt.Rows(i)("MontoTransferencia").ToString(), dt.Rows(i)("CuentaDestino").ToString(), _
                                    dt.Rows(i)("FechaTransferencia").ToString(), dt.Rows(i)("idmoneda").ToString(), _
                                    dt.Rows(i)("BancoOrigen").ToString(), dt.Rows(i)("BancoDestino").ToString(), _
                                    dt.Rows(i)("Observaciones").ToString(), dt.Rows(i)("Id").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarGrid_Tarjetas()
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

            sqltxt2 = " SELECT NombreTarjeta, Porcentaje, Cuotas, Monto, MontoTotal from Ingresos_Tarjetas it " & _
                        " WHERE IdIngreso = " & IIf(txtID.Text = "", 0, txtID.Text)

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdTarjetas.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), _
                                    dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), dt.Rows(i)(4).ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LimpiarGrids()
        'grdImpuestos.Rows.Clear()
        grdCheques.Rows.Clear()
        grdTransferencias.Rows.Clear()
        grdTarjetas.Rows.Clear()

        txtNroCheque.Text = ""
        txtMontoCheque.Text = ""
        dtpFechaCheque.Value = Date.Today  'grdCheques.CurrentRow.Cells(3).Value
        txtPropietario.Text = ""
        'cmbMoneda.SelectedValue = grd.CurrentRow.Cells(5).Value
        txtObservacionesCheque.Text = ""

        txtNroOpCliente.Text = ""
        txtMontoTransf.Text = ""
        cmbCuentaDestino.Text = ""
        cmbCuentaOrigen.Text = ""
        txtObservacionesTransf.Text = ""

    End Sub

    Private Sub Imprimir_LlenarTMPs(ByVal NroMov As String)

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@idmov"
                param_codigo.SqlDbType = SqlDbType.BigInt
                'param_codigo.Size = 10
                param_codigo.Value = CLng(NroMov)
                param_codigo.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spRPT_Ingresos_Tmp", _
                                               param_codigo)

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
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub validar_NumerosReales2( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdImpuestos.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridImpuestos.Monto Then

            Dim caracter As Char = e.KeyChar

            ' referencia a la celda  
            Dim txt As TextBox = CType(sender, TextBox)

            ' comprobar si es un número con isNumber, si es el backspace, si el caracter  
            ' es el separador decimal, y que no contiene ya el separador  
            If (Char.IsNumber(caracter)) Or _
               (caracter = ChrW(Keys.Back)) Or _
               (caracter = ".") And _
               (txt.Text.Contains(".") = False) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarRegistro_Ingreso() As Integer
        Dim res As Integer = 0

        Try
            Try
                conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(conn_del_form) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput
                Else
                    param_id.Value = txtID.Text
                    param_id.Direction = ParameterDirection.Input
                End If

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                'param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_nropago As New SqlClient.SqlParameter
                param_nropago.ParameterName = "@nroordenPago"
                param_nropago.SqlDbType = SqlDbType.VarChar
                param_nropago.Size = 50
                param_nropago.Value = txtOrdenPago.Text
                param_nropago.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbClientes.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                param_fecha.Direction = ParameterDirection.Input

                Dim param_contado As New SqlClient.SqlParameter
                param_contado.ParameterName = "@contado"
                param_contado.SqlDbType = SqlDbType.Bit
                If txtEntregaContado.Text <> "" And txtEntregaContado.Text <> "0" Then
                    param_contado.Value = True
                Else
                    param_contado.Value = False
                End If
                param_contado.Direction = ParameterDirection.Input

                Dim param_montocontado As New SqlClient.SqlParameter
                param_montocontado.ParameterName = "@montocontado"
                param_montocontado.SqlDbType = SqlDbType.Decimal
                param_montocontado.Precision = 18
                param_montocontado.Scale = 2
                param_montocontado.Value = IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)
                param_montocontado.Direction = ParameterDirection.Input

                Dim param_tarjeta As New SqlClient.SqlParameter
                param_tarjeta.ParameterName = "@tarjeta"
                param_tarjeta.SqlDbType = SqlDbType.Bit
                If lblEntregaTarjetas.Text <> "" And lblEntregaTarjetas.Text <> "0" And grdTransferencias.RowCount > 1 Then
                    param_tarjeta.Value = True
                Else
                    param_tarjeta.Value = False
                End If
                param_tarjeta.Direction = ParameterDirection.Input

                Dim param_montotarjeta As New SqlClient.SqlParameter
                param_montotarjeta.ParameterName = "@montotarjeta"
                param_montotarjeta.SqlDbType = SqlDbType.Decimal
                param_montotarjeta.Precision = 18
                param_montotarjeta.Scale = 2
                param_montotarjeta.Value = lblEntregaTarjetas.Text
                param_montotarjeta.Direction = ParameterDirection.Input

                Dim param_cheque As New SqlClient.SqlParameter
                param_cheque.ParameterName = "@cheque"
                param_cheque.SqlDbType = SqlDbType.Bit
                If lblEntregaCheques.Text <> "" And lblEntregaCheques.Text <> "0.00" And grdCheques.RowCount > 1 Then
                    param_cheque.Value = True
                Else
                    param_cheque.Value = False
                End If
                param_cheque.Direction = ParameterDirection.Input

                Dim param_montocheque As New SqlClient.SqlParameter
                param_montocheque.ParameterName = "@montocheque"
                param_montocheque.SqlDbType = SqlDbType.Decimal
                param_montocheque.Precision = 18
                param_montocheque.Scale = 2
                param_montocheque.Value = lblEntregaCheques.Text
                param_montocheque.Direction = ParameterDirection.Input

                Dim param_transferencia As New SqlClient.SqlParameter
                param_transferencia.ParameterName = "@transferencia"
                param_transferencia.SqlDbType = SqlDbType.Bit
                If lblEntregaTransferencias.Text <> "" And lblEntregaTransferencias.Text <> "0" Then
                    param_transferencia.Value = True
                Else
                    param_transferencia.Value = False
                End If
                param_transferencia.Direction = ParameterDirection.Input

                Dim param_montotransf As New SqlClient.SqlParameter
                param_montotransf.ParameterName = "@montotransf"
                param_montotransf.SqlDbType = SqlDbType.Decimal
                param_montotransf.Precision = 18
                param_montotransf.Scale = 2
                param_montotransf.Value = lblEntregaTransferencias.Text
                param_montotransf.Direction = ParameterDirection.Input

                Dim param_impuestos As New SqlClient.SqlParameter
                param_impuestos.ParameterName = "@impuestos"
                param_impuestos.SqlDbType = SqlDbType.Bit
                If lblEntregaImpuestos.Text <> "" And lblEntregaImpuestos.Text <> "0" Then
                    param_impuestos.Value = True
                Else
                    param_impuestos.Value = False
                End If
                param_impuestos.Direction = ParameterDirection.Input

                Dim param_montoimpuesto As New SqlClient.SqlParameter
                param_montoimpuesto.ParameterName = "@montoimpuesto"
                param_montoimpuesto.SqlDbType = SqlDbType.Decimal
                param_montoimpuesto.Precision = 18
                param_montoimpuesto.Scale = 2
                param_montoimpuesto.Value = lblEntregaImpuestos.Text
                param_montoimpuesto.Direction = ParameterDirection.Input

                Dim param_ayid As New SqlClient.SqlParameter
                param_ayid.ParameterName = "@Ayid"
                param_ayid.SqlDbType = SqlDbType.Bit
                If txtAyIDepUsado.Text <> "" And txtAyIDepUsado.Text <> "0" Then
                    param_ayid.Value = True
                Else
                    param_ayid.Value = False
                End If
                param_ayid.Direction = ParameterDirection.Input

                Dim param_montoAyid As New SqlClient.SqlParameter
                param_montoAyid.ParameterName = "@MontoAyid"
                param_montoAyid.SqlDbType = SqlDbType.Decimal
                param_montoAyid.Precision = 18
                param_montoAyid.Scale = 2
                param_montoAyid.Value = IIf(txtAyIDepUsado.Text = "", 0, txtAyIDepUsado.Text)
                param_montoAyid.Direction = ParameterDirection.Input

                'Dim param_iva As New SqlClient.SqlParameter
                'param_iva.ParameterName = "@iva"
                'param_iva.SqlDbType = SqlDbType.Decimal
                'param_iva.Precision = 18
                'param_iva.Scale = 2
                'param_iva.Value = IVA
                'param_iva.Direction = ParameterDirection.Input

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = CDbl(lblIVA.Text)
                param_montoiva.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = CDbl(lblSubtotal.Text)
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = CDbl(lblTotal.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_Redondeo As New SqlClient.SqlParameter
                param_Redondeo.ParameterName = "@Redondeo"
                param_Redondeo.SqlDbType = SqlDbType.Decimal
                param_Redondeo.Precision = 18
                param_Redondeo.Scale = 2
                param_Redondeo.Value = CDbl(IIf(txtRedondeo.Text = "", 0, txtRedondeo.Text))
                param_Redondeo.Direction = ParameterDirection.Input

                Dim param_Descuento As New SqlClient.SqlParameter
                param_Descuento.ParameterName = "@Descuento"
                param_Descuento.SqlDbType = SqlDbType.Decimal
                param_Descuento.Precision = 18
                param_Descuento.Scale = 2
                param_Descuento.Value = CDbl(IIf(txtDescuento.Text = "", 0, txtDescuento.Text))
                param_Descuento.Direction = ParameterDirection.Input

                Dim param_ValorCambio As New SqlClient.SqlParameter
                param_ValorCambio.ParameterName = "@ValorCambio"
                param_ValorCambio.SqlDbType = SqlDbType.Decimal
                param_ValorCambio.Precision = 18
                param_ValorCambio.Scale = 2
                param_ValorCambio.Value = txtValorCambio.Text
                param_ValorCambio.Direction = ParameterDirection.Input

                Dim param_remitos As New SqlClient.SqlParameter
                param_remitos.ParameterName = "@remitosasociados"
                param_remitos.SqlDbType = SqlDbType.VarChar
                param_remitos.Size = 300
                param_remitos.Value = RemitosAsociados
                param_remitos.Direction = ParameterDirection.Input

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

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Insert", _
                                              param_id, param_codigo, param_nropago, param_idcliente, param_fecha, param_contado, param_montocontado, _
                                              param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, _
                                              param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                              param_montoiva, param_subtotal, param_total, param_Redondeo, param_Descuento, param_ValorCambio, _
                                              param_remitos, param_ayid, param_montoAyid, param_useradd, param_res)

                        txtID.Text = param_id.Value

                        txtCODIGO.Text = param_codigo.Value

                    Else

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Update", _
                                              param_id, param_nropago, param_fecha, param_contado, param_montocontado, _
                                              param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, _
                                              param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                              param_montoiva, param_subtotal, param_total, param_Redondeo, _
                                              param_ayid, param_montoAyid, param_useradd, param_res)

                    End If


                    AgregarRegistro_Ingreso = param_res.Value

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

        End Try
    End Function

    Private Function AgregarRegistro_AyID_Det() As Integer

    End Function

    Private Function AgregarRegistro_FacturasConsumos() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try
            Try
                Dim Cancelartodo As Boolean = False
                Dim Deuda As Double, totalentregado As Double, DeudaFactCons As Double, DeudaAyID As Double, totalapagar As Double, Descuento As Double
                Dim j As Integer


                Dim deudanegativa As Double = 0

                For j = 0 To grdFacturasConsumos.Rows.Count - 1
                    If CBool(grdFacturasConsumos.Rows(j).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                        If grdFacturasConsumos.Rows(j).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value < 0 And (grdFacturasConsumos.Rows(j).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "DEV" Or grdFacturasConsumos.Rows(j).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "NC") Then
                            deudanegativa = deudanegativa + grdFacturasConsumos.Rows(j).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value
                        End If
                    End If
                Next

                deudanegativa = deudanegativa * -1


                totalentregado = CDbl(lblEntregado.Text) + deudanegativa

                Descuento = IIf(txtDescuento.Text = "", 0, txtDescuento.Text)

                totalentregado = CDbl(lblEntregado.Text) + Descuento

                'para los anticipos 
                totalapagar = CDbl(lblTotalaPagar.Text) '- CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(lblEntregaCheques.Text = "", 0, lblEntregaCheques.Text)) + CDbl(IIf(lblEntregaTransferencias.Text = "", 0, lblEntregaTransferencias.Text)) + CDbl(IIf(lblEntregaImpuestos.Text = "", 0, lblEntregaImpuestos.Text) + CDbl(IIf(lblEntregaTarjetas.Text = "", 0, lblEntregaTarjetas.Text))) ' + deudanegativa


                Dim NroActual As Integer = 1

                j = 0

                For j = 0 To grdFacturasConsumos.Rows.Count - 1

                    If CBool(grdFacturasConsumos.Rows(j).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then

                        For i = 0 To grdFacturasConsumos.RowCount - 1

                            If (CBool(grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Or bolModo = False) And NroActual = IIf(grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroMov).Value Is DBNull.Value, 0, grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroMov).Value) Then

                                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "ANTI" Or grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "IDEP" Then
                                    DeudaAyID = FormatNumber(totalapagar - grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value * (-1), 2)

                                    If DeudaAyID >= 0 Then
                                        Cancelartodo = True
                                        DeudaFactCons = 0
                                    Else
                                        Cancelartodo = False
                                        DeudaFactCons = DeudaAyID * -1
                                    End If

                                    totalapagar = DeudaAyID

                                Else

                                    Deuda = FormatNumber(totalentregado - grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value, 2)

                                    If Deuda >= 0 Then
                                        Cancelartodo = True
                                        DeudaFactCons = 0
                                    Else
                                        Cancelartodo = False
                                        DeudaFactCons = Deuda * -1
                                    End If

                                    totalentregado = Deuda
                                End If

                                Dim param_idingreso As New SqlClient.SqlParameter
                                param_idingreso.ParameterName = "@idingreso"
                                param_idingreso.SqlDbType = SqlDbType.BigInt
                                param_idingreso.Value = txtID.Text
                                param_idingreso.Direction = ParameterDirection.Input

                                Dim param_idfacturacion As New SqlClient.SqlParameter
                                param_idfacturacion.ParameterName = "@idfacturacion"
                                param_idfacturacion.SqlDbType = SqlDbType.BigInt
                                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "VTA" Or grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "DEV" Then
                                    param_idfacturacion.Value = grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Id).Value
                                Else
                                    param_idfacturacion.Value = DBNull.Value
                                End If
                                param_idfacturacion.Direction = ParameterDirection.Input

                                Dim param_idconsumo As New SqlClient.SqlParameter
                                param_idconsumo.ParameterName = "@idconsumo"
                                param_idconsumo.SqlDbType = SqlDbType.BigInt
                                'If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "OCA" Then
                                ' param_idconsumo.Value = grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Id).Value
                                'Else
                                param_idconsumo.Value = DBNull.Value
                                'End If
                                param_idconsumo.Direction = ParameterDirection.Input

                                Dim param_idNC As New SqlClient.SqlParameter
                                param_idNC.ParameterName = "@idNotaCredito"
                                param_idNC.SqlDbType = SqlDbType.BigInt
                                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "NC" Then
                                    param_idNC.Value = grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Id).Value
                                Else
                                    param_idNC.Value = DBNull.Value
                                End If
                                param_idNC.Direction = ParameterDirection.Input

                                Dim param_idAyID As New SqlClient.SqlParameter
                                param_idAyID.ParameterName = "@IdAnticipo_IngresoDepo"
                                param_idAyID.SqlDbType = SqlDbType.BigInt
                                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "IDEP" Or grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "ANTI" Then
                                    param_idAyID.Value = grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Id).Value
                                Else
                                    param_idAyID.Value = DBNull.Value
                                End If
                                param_idAyID.Direction = ParameterDirection.Input

                                Dim param_DEUDA As New SqlClient.SqlParameter
                                param_DEUDA.ParameterName = "@Deuda"
                                param_DEUDA.SqlDbType = SqlDbType.Decimal
                                param_DEUDA.Precision = 18
                                param_DEUDA.Scale = 2
                                param_DEUDA.Value = DeudaFactCons
                                param_DEUDA.Direction = ParameterDirection.Input

                                Dim param_DeudaActual As New SqlClient.SqlParameter
                                param_DeudaActual.ParameterName = "@DeudaActual"
                                param_DeudaActual.SqlDbType = SqlDbType.Decimal
                                param_DeudaActual.Precision = 18
                                param_DeudaActual.Scale = 2
                                param_DeudaActual.Value = grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value - DeudaFactCons
                                param_DeudaActual.Direction = ParameterDirection.Input

                                Dim param_CancelarTodo As New SqlClient.SqlParameter
                                param_CancelarTodo.ParameterName = "@CancelarTodo"
                                param_CancelarTodo.SqlDbType = SqlDbType.Bit
                                param_CancelarTodo.Value = Cancelartodo
                                param_CancelarTodo.Direction = ParameterDirection.Input

                                Dim param_FechaCanc As New SqlClient.SqlParameter
                                param_FechaCanc.ParameterName = "@FechaCanc"
                                param_FechaCanc.SqlDbType = SqlDbType.DateTime
                                param_FechaCanc.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                                param_FechaCanc.Direction = ParameterDirection.Input

                                Dim param_res As New SqlClient.SqlParameter
                                param_res.ParameterName = "@res"
                                param_res.SqlDbType = SqlDbType.Int
                                param_res.Value = DBNull.Value
                                param_res.Direction = ParameterDirection.InputOutput

                                Try
                                    If bolModo = True Then
                                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_FacturasConsumos_Insert", _
                                                              param_idfacturacion, param_idconsumo, param_idingreso, param_idNC, param_idAyID, param_DEUDA, param_DeudaActual, _
                                                              param_CancelarTodo, param_FechaCanc, param_res)
                                    Else
                                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_FacturasConsumos_Update", _
                                                             param_idfacturacion, param_idconsumo, param_idingreso, param_DEUDA, param_DeudaActual, _
                                                             param_CancelarTodo, param_FechaCanc, param_res)
                                    End If

                                    res = param_res.Value

                                    If (res <= 0) Then
                                        Exit For
                                    End If

                                    NroActual = NroActual + 1

                                    If Deuda < 0 Then
                                        Exit For
                                    End If

                                    'If bolModo = True Then
                                    '    If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value.ToString.Contains("FACTURAS") Then
                                    '        If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.ValorDolarFacturado).Value > 1 And grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.ValorDolarFacturado).Value < txtValorCambio.Text Then
                                    '            If MessageBox.Show("Desea generar una Nota de Débito para el comprobante:" & grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value & " - " & _
                                    '                               grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroFactura).Value & " debido a la diferencia en el Valor de Cambio tomado como referencia?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    '                Dim Resultado As Integer
                                    '                Dim SubtotalNC As Double, MontoIVANC As Double, TotalNC As Double

                                    '                SubtotalNC = Format(((grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Subtotal).Value / grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.ValorDolarFacturado).Value) * txtValorCambio.Text) - grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Subtotal).Value, "####0.00")

                                    '                MontoIVANC = Format(SubtotalNC * (grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Iva).Value / 100), "####0.00")

                                    '                TotalNC = SubtotalNC + MontoIVANC

                                    '                Resultado = AgregarRegistro_NotaDebito(grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Id).Value, _
                                    '                                            grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value, _
                                    '                                            SubtotalNC, _
                                    '                                            grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Iva).Value, _
                                    '                                            MontoIVANC, _
                                    '                                            TotalNC, _
                                    '                                            grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.NroOC).Value, _
                                    '                                            grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.CondicionIva).Value, _
                                    '                                            grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.CondicionVta).Value)

                                    '                If Resultado < 0 Then
                                    '                    AgregarRegistro_FacturasConsumos = -10
                                    '                    Exit Function
                                    '                End If

                                    '            End If
                                    '        End If
                                    '    End If
                                    'End If

                                Catch ex As Exception
                                    Throw ex
                                End Try
                            End If
                        Next
                    End If

                    If Deuda < 0 Then
                        Exit For
                    End If

                Next


                If bolModo = True And CDbl(lblResto.Text) < 0 Then 'And SoloAyID() = False Then
                    Dim Resultado As Integer
                    Dim TotalNC As Double

                    TotalNC = CDbl(lblResto.Text) * -1

                    Resultado = AgregarRegistro_NotaCredito(TotalNC)

                    If Resultado < 0 Then
                        AgregarRegistro_FacturasConsumos = -10
                        Exit Function
                    End If

                End If

                AgregarRegistro_FacturasConsumos = res

            Catch ex2 As Exception
                Throw ex2
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
        End Try
    End Function

    Private Function AgregarRegistro_Cheques() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdCheques.RowCount - 1

                    Dim param_Id As New SqlClient.SqlParameter
                    param_Id.ParameterName = "@Id"
                    param_Id.SqlDbType = SqlDbType.BigInt
                    If grdCheques.Rows(i).Cells(7).Value Is DBNull.Value Or grdCheques.Rows(i).Cells(7).Value Is Nothing Then
                        param_Id.Value = DBNull.Value
                    Else
                        param_Id.Value = grdCheques.Rows(i).Cells(7).Value
                    End If
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_NroCheque As New SqlClient.SqlParameter
                    param_NroCheque.ParameterName = "@NroCheque"
                    param_NroCheque.SqlDbType = SqlDbType.BigInt
                    param_NroCheque.Value = grdCheques.Rows(i).Cells(0).Value
                    param_NroCheque.Direction = ParameterDirection.Input

                    Dim param_IdCliente As New SqlClient.SqlParameter
                    param_IdCliente.ParameterName = "@IdCliente"
                    param_IdCliente.SqlDbType = SqlDbType.Int
                    param_IdCliente.Value = cmbClientes.SelectedValue
                    param_IdCliente.Direction = ParameterDirection.Input

                    Dim param_ClienteChequeBco As New SqlClient.SqlParameter
                    param_ClienteChequeBco.ParameterName = "@ClienteChequeBco"
                    param_ClienteChequeBco.SqlDbType = SqlDbType.NVarChar
                    param_ClienteChequeBco.Size = 50
                    param_ClienteChequeBco.Value = grdCheques.Rows(i).Cells(4).Value
                    param_ClienteChequeBco.Direction = ParameterDirection.Input

                    Dim param_FechaCobro As New SqlClient.SqlParameter
                    param_FechaCobro.ParameterName = "@FechaCobro"
                    param_FechaCobro.SqlDbType = SqlDbType.DateTime
                    param_FechaCobro.Value = grdCheques.Rows(i).Cells(3).Value
                    param_FechaCobro.Direction = ParameterDirection.Input

                    Dim param_Moneda As New SqlClient.SqlParameter
                    param_Moneda.ParameterName = "@IdMoneda"
                    param_Moneda.SqlDbType = SqlDbType.Int
                    param_Moneda.Value = 1 'grdCheques.Rows(i).Cells(5).Value
                    param_Moneda.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdCheques.Rows(i).Cells(2).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_Banco As New SqlClient.SqlParameter
                    param_Banco.ParameterName = "@Banco"
                    param_Banco.SqlDbType = SqlDbType.NVarChar
                    param_Banco.Size = 50
                    param_Banco.Value = grdCheques.Rows(i).Cells(1).Value
                    param_Banco.Direction = ParameterDirection.Input

                    Dim param_Observaciones As New SqlClient.SqlParameter
                    param_Observaciones.ParameterName = "@Observaciones"
                    param_Observaciones.SqlDbType = SqlDbType.VarChar
                    param_Observaciones.Size = 100
                    param_Observaciones.Value = grdCheques.Rows(i).Cells(6).Value
                    param_Observaciones.Direction = ParameterDirection.Input

                    Dim param_Propio As New SqlClient.SqlParameter
                    param_Propio.ParameterName = "@Propio"
                    param_Propio.SqlDbType = SqlDbType.Bit
                    param_Propio.Value = False
                    param_Propio.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    If bolModo = True Then
                        param_useradd.ParameterName = "@useradd"
                    Else
                        param_useradd.ParameterName = "@userupd"
                    End If
                    param_useradd.SqlDbType = SqlDbType.SmallInt
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Dim param_MENSAJE As New SqlClient.SqlParameter
                    param_MENSAJE.ParameterName = "@MENSAJE"
                    param_MENSAJE.SqlDbType = SqlDbType.VarChar
                    param_MENSAJE.Size = 300
                    param_MENSAJE.Value = DBNull.Value
                    param_MENSAJE.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_Insert_Ingreso", _
                                param_IdIngreso, param_NroCheque, param_IdCliente, _
                                param_ClienteChequeBco, param_FechaCobro, param_Moneda, param_Monto, _
                                param_Banco, param_Observaciones, param_Propio, param_useradd, param_res)

                            res = param_res.Value
                        Else
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_Update_Ingreso", _
                                param_Id, param_IdIngreso, param_NroCheque, param_IdCliente, _
                                param_ClienteChequeBco, param_FechaCobro, param_Moneda, param_Monto, _
                                param_Banco, param_Observaciones, param_Propio, param_useradd, param_MENSAJE, param_res)

                            'MsgBox(param_MENSAJE.Value.ToString)

                            res = param_res.Value

                        End If

                        If res < 0 Then
                            AgregarRegistro_Cheques = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Cheques = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Cheques = 1
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

        End Try
    End Function

    Private Function AgregarRegistro_Impuestos() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdImpuestos.RowCount - 1

                    Dim param_Id As New SqlClient.SqlParameter
                    param_Id.ParameterName = "@Id"
                    param_Id.SqlDbType = SqlDbType.BigInt
                    param_Id.Value = grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.IdImpuestoxIngreso).Value
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_IdImpuesto As New SqlClient.SqlParameter
                    param_IdImpuesto.ParameterName = "@IdImpuesto"
                    param_IdImpuesto.SqlDbType = SqlDbType.BigInt
                    param_IdImpuesto.Value = grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.Id).Value
                    param_IdImpuesto.Direction = ParameterDirection.Input

                    Dim param_NroDocumento As New SqlClient.SqlParameter
                    param_NroDocumento.ParameterName = "@NroDocumento"
                    param_NroDocumento.SqlDbType = SqlDbType.NVarChar
                    param_NroDocumento.Size = 30
                    param_NroDocumento.Value = LTrim(RTrim(grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.NroDocumento).Value))
                    param_NroDocumento.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.Monto).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Dim param_MENSAJE As New SqlClient.SqlParameter
                    param_MENSAJE.ParameterName = "@MENSAJE"
                    param_MENSAJE.SqlDbType = SqlDbType.VarChar
                    param_MENSAJE.Size = 200
                    param_MENSAJE.Value = DBNull.Value
                    param_MENSAJE.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Impuestos_Insert", _
                                param_IdIngreso, param_IdImpuesto, param_NroDocumento, _
                                 param_Monto, param_res)

                            res = param_res.Value

                        Else

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Impuestos_Update", _
                                param_Id, param_IdImpuesto, param_NroDocumento, _
                                 param_Monto, param_MENSAJE, param_res)

                            'MsgBox(param_MENSAJE.Value)

                            res = param_res.Value

                        End If

                        If res < 0 Then
                            AgregarRegistro_Impuestos = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Impuestos = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Impuestos = 1
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

        End Try
    End Function

    Private Function AgregarRegistro_Transferencias() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdTransferencias.RowCount - 1

                    Dim param_Id As New SqlClient.SqlParameter
                    param_Id.ParameterName = "@Id"
                    param_Id.SqlDbType = SqlDbType.BigInt
                    If grdTransferencias.Rows(i).Cells(9).Value Is DBNull.Value Or grdTransferencias.Rows(i).Cells(9).Value Is Nothing Then
                        param_Id.Value = DBNull.Value
                    Else
                        param_Id.Value = CLng(grdTransferencias.Rows(i).Cells(9).Value)
                    End If
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_CuentaOrigen As New SqlClient.SqlParameter
                    param_CuentaOrigen.ParameterName = "@CuentaOrigen"
                    param_CuentaOrigen.SqlDbType = SqlDbType.VarChar
                    param_CuentaOrigen.Size = 30
                    param_CuentaOrigen.Value = grdTransferencias.Rows(i).Cells(1).Value
                    param_CuentaOrigen.Direction = ParameterDirection.Input

                    Dim param_CuentaDestino As New SqlClient.SqlParameter
                    param_CuentaDestino.ParameterName = "@CuentaDestino"
                    param_CuentaDestino.SqlDbType = SqlDbType.VarChar
                    param_CuentaDestino.Size = 30
                    param_CuentaDestino.Value = grdTransferencias.Rows(i).Cells(3).Value
                    param_CuentaDestino.Direction = ParameterDirection.Input

                    Dim param_BancoOrigen As New SqlClient.SqlParameter
                    param_BancoOrigen.ParameterName = "@BancoOrigen"
                    param_BancoOrigen.SqlDbType = SqlDbType.VarChar
                    param_BancoOrigen.Size = 30
                    param_BancoOrigen.Value = grdTransferencias.Rows(i).Cells(6).Value
                    param_BancoOrigen.Direction = ParameterDirection.Input

                    Dim param_BancoDestino As New SqlClient.SqlParameter
                    param_BancoDestino.ParameterName = "@BancoDestino"
                    param_BancoDestino.SqlDbType = SqlDbType.VarChar
                    param_BancoDestino.Size = 30
                    param_BancoDestino.Value = grdTransferencias.Rows(i).Cells(7).Value
                    param_BancoDestino.Direction = ParameterDirection.Input

                    Dim param_Fecha As New SqlClient.SqlParameter
                    param_Fecha.ParameterName = "@FechaTransf"
                    param_Fecha.SqlDbType = SqlDbType.DateTime
                    param_Fecha.Value = grdTransferencias.Rows(i).Cells(4).Value
                    param_Fecha.Direction = ParameterDirection.Input

                    Dim param_Moneda As New SqlClient.SqlParameter
                    param_Moneda.ParameterName = "@IdMoneda"
                    param_Moneda.SqlDbType = SqlDbType.Int
                    param_Moneda.Value = 1 'grdTransferencias.Rows(i).Cells(5).Value
                    param_Moneda.Direction = ParameterDirection.Input

                    Dim param_Nroop As New SqlClient.SqlParameter
                    param_Nroop.ParameterName = "@NroOp"
                    param_Nroop.SqlDbType = SqlDbType.VarChar
                    param_Nroop.Size = 30
                    param_Nroop.Value = grdTransferencias.Rows(i).Cells(0).Value
                    param_Nroop.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdTransferencias.Rows(i).Cells(2).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_Observaciones As New SqlClient.SqlParameter
                    param_Observaciones.ParameterName = "@Observaciones"
                    param_Observaciones.SqlDbType = SqlDbType.NVarChar
                    param_Observaciones.Size = 100
                    param_Observaciones.Value = grdTransferencias.Rows(i).Cells(8).Value
                    param_Observaciones.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Dim param_MENSAJE As New SqlClient.SqlParameter
                    param_MENSAJE.ParameterName = "@MENSAJE"
                    param_MENSAJE.SqlDbType = SqlDbType.VarChar
                    param_MENSAJE.Size = 200
                    param_MENSAJE.Value = DBNull.Value
                    param_MENSAJE.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Insert", _
                                param_IdIngreso, param_BancoDestino, param_BancoOrigen, _
                                param_CuentaDestino, param_CuentaOrigen, param_Fecha, param_Moneda, _
                                param_Monto, param_Nroop, param_Observaciones, param_res)

                            res = param_res.Value

                        Else

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Update", _
                                param_Id, param_IdIngreso, param_BancoDestino, param_BancoOrigen, _
                                param_CuentaDestino, param_CuentaOrigen, param_Fecha, param_Moneda, _
                                param_Monto, param_Nroop, param_Observaciones, param_MENSAJE, param_res)

                            'MsgBox(param_MENSAJE.Value)

                            res = param_res.Value

                        End If

                        If res < 0 Then
                            AgregarRegistro_Transferencias = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Transferencias = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Transferencias = 1
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

        End Try
    End Function

    Private Function AgregarRegistro_Tarjetas() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdTarjetas.RowCount - 1

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_NombreTarjeta As New SqlClient.SqlParameter
                    param_NombreTarjeta.ParameterName = "@NombreTarjeta"
                    param_NombreTarjeta.SqlDbType = SqlDbType.VarChar
                    param_NombreTarjeta.Size = 100
                    param_NombreTarjeta.Value = grdTarjetas.Rows(i).Cells(0).Value
                    param_NombreTarjeta.Direction = ParameterDirection.Input

                    Dim param_Cuotas As New SqlClient.SqlParameter
                    param_Cuotas.ParameterName = "@Cuotas"
                    param_Cuotas.SqlDbType = SqlDbType.Int
                    param_Cuotas.Value = grdTarjetas.Rows(i).Cells(2).Value
                    param_Cuotas.Direction = ParameterDirection.Input

                    Dim param_Porcentaje As New SqlClient.SqlParameter
                    param_Porcentaje.ParameterName = "@Recargo"
                    param_Porcentaje.SqlDbType = SqlDbType.Decimal
                    param_Porcentaje.Precision = 18
                    param_Porcentaje.Scale = 2
                    param_Porcentaje.Value = grdTarjetas.Rows(i).Cells(1).Value
                    param_Porcentaje.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdTarjetas.Rows(i).Cells(3).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_MontoFinal As New SqlClient.SqlParameter
                    param_MontoFinal.ParameterName = "@MontoTotal"
                    param_MontoFinal.SqlDbType = SqlDbType.Decimal
                    param_MontoFinal.Precision = 18
                    param_MontoFinal.Scale = 2
                    param_MontoFinal.Value = grdTarjetas.Rows(i).Cells(4).Value
                    param_MontoFinal.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Tarjetas_Insert", _
                            param_IdIngreso, param_NombreTarjeta, param_Cuotas, _
                            param_Porcentaje, param_Monto, param_MontoFinal, param_res)

                        res = param_res.Value

                        AgregarRegistro_Tarjetas = res

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Tarjetas = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Tarjetas = 1
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

        End Try

    End Function

    Private Function AgregarRegistro_NotaCredito(Total_ND As Double) As Integer
        Dim res As Integer = 0

        Try

            Try

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                param_fecha.Direction = ParameterDirection.Input

                Dim param_Cliente As New SqlClient.SqlParameter
                param_Cliente.ParameterName = "@IdCliente"
                param_Cliente.SqlDbType = SqlDbType.BigInt
                param_Cliente.Value = CDbl(cmbClientes.SelectedValue)
                param_Cliente.Direction = ParameterDirection.Input

                Dim param_idingreso As New SqlClient.SqlParameter
                param_idingreso.ParameterName = "@IdINGRESO"
                param_idingreso.SqlDbType = SqlDbType.BigInt
                param_idingreso.Value = CLng(txtID.Text)
                param_idingreso.Direction = ParameterDirection.Input

                Dim param_monto As New SqlClient.SqlParameter
                param_monto.ParameterName = "@monto"
                param_monto.SqlDbType = SqlDbType.Decimal
                param_monto.Precision = 18
                param_monto.Scale = 2
                param_monto.Value = Total_ND
                param_monto.Direction = ParameterDirection.Input

                Dim param_res1 As New SqlClient.SqlParameter
                param_res1.ParameterName = "@res"
                param_res1.SqlDbType = SqlDbType.Int
                param_res1.Value = DBNull.Value
                param_res1.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spNotasCredito_Insert]", _
                                            param_fecha, param_Cliente, param_idingreso, param_monto, param_res1)


                    res = param_res1.Value

                    If res < 0 Then
                        AgregarRegistro_NotaCredito = -9
                    End If

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            AgregarRegistro_NotaCredito = -10

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Private Function BuscarProximaFactura(TipoComprobante As Integer) As Integer
        'Dim res As Integer = 0

        Try
            Dim param_proxfactura As New SqlClient.SqlParameter
            param_proxfactura.ParameterName = "@proxfactura"
            param_proxfactura.SqlDbType = SqlDbType.BigInt
            param_proxfactura.Value = DBNull.Value
            param_proxfactura.Direction = ParameterDirection.InputOutput

            Dim param_tipocomprobante As New SqlClient.SqlParameter
            param_tipocomprobante.ParameterName = "@tipocomprobante"
            param_tipocomprobante.SqlDbType = SqlDbType.Int
            param_tipocomprobante.Value = TipoComprobante
            param_tipocomprobante.Direction = ParameterDirection.Input

            Try
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spFacturacion_Select_All_ProxFactura", _
                                          param_proxfactura, param_tipocomprobante)

                BuscarProximaFactura = param_proxfactura.Value

            Catch ex As Exception
                Throw ex
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
        End Try

    End Function

    Private Function ControlarCantidad_Cheques() As Integer
        ' Dim connection As SqlClient.SqlConnection = Nothing
        Dim i As Integer

        Try
            'connection = SqlHelper.GetConnection(ConnStringSEI)

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, "DELETE FROM TMP_Cheques_Ingreso")

            For i = 0 To grdCheques.RowCount - 1
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idIngreso"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_idcheque As New SqlClient.SqlParameter
                param_idcheque.ParameterName = "@idcheque"
                param_idcheque.SqlDbType = SqlDbType.BigInt
                param_idcheque.Value = grdCheques.Rows(i).Cells(7).Value
                param_idcheque.Direction = ParameterDirection.Input

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_Ingreso_TMP", _
                                            param_id, param_idcheque)

            Next

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function ControlarCantidad_Impuestos() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim i As Integer

        Try
            'connection = SqlHelper.GetConnection(ConnStringSEI)

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, "DELETE FROM TMP_Impuestos_Ingreso")

            For i = 0 To grdImpuestos.RowCount - 1

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = grdImpuestos.Rows(i).Cells(5).Value
                param_id.Direction = ParameterDirection.Input

                Dim param_idIngreso As New SqlClient.SqlParameter
                param_idIngreso.ParameterName = "@idIngreso"
                param_idIngreso.SqlDbType = SqlDbType.BigInt
                param_idIngreso.Value = txtID.Text
                param_idIngreso.Direction = ParameterDirection.Input

                Dim param_idcheque As New SqlClient.SqlParameter
                param_idcheque.ParameterName = "@IdImpuesto"
                param_idcheque.SqlDbType = SqlDbType.BigInt
                param_idcheque.Value = grdImpuestos.Rows(i).Cells(4).Value
                param_idcheque.Direction = ParameterDirection.Input

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spImpuestos_Ingreso_TMP", _
                                            param_id, param_idIngreso, param_idcheque)

            Next

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function ControlarCantidad_Transferencias() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim i As Integer

        Try
            'connection = SqlHelper.GetConnection(ConnStringSEI)

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, "DELETE FROM TMP_Transferencias_Ingreso")

            For i = 0 To grdTransferencias.RowCount - 1
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idIngreso"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_idtransferencia As New SqlClient.SqlParameter
                param_idtransferencia.ParameterName = "@Idtransferencia"
                param_idtransferencia.SqlDbType = SqlDbType.BigInt
                param_idtransferencia.Value = grdTransferencias.Rows(i).Cells(9).Value
                param_idtransferencia.Direction = ParameterDirection.Input

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Ingreso_TMP", _
                                            param_id, param_idtransferencia)

            Next

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    'Private Function EliminarItems_Impuestos() As Integer
    '    'Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        Dim param_id As New SqlClient.SqlParameter
    '        param_id.ParameterName = "@idIngreso"
    '        param_id.SqlDbType = SqlDbType.BigInt
    '        param_id.Value = CLng(txtID.Text)
    '        param_id.Direction = ParameterDirection.Input

    '        Dim param_res As New SqlClient.SqlParameter
    '        param_res.ParameterName = "@Res"
    '        param_res.SqlDbType = SqlDbType.Int
    '        param_res.Value = DBNull.Value
    '        param_res.Direction = ParameterDirection.InputOutput

    '        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spImpuestos_EliminarItems", _
    '                                    param_id, param_res)

    '        EliminarItems_Impuestos = param_res.Value

    '    Catch ex As Exception
    '        Dim errMessage As String = ""
    '        Dim tempException As Exception = ex

    '        While (Not tempException Is Nothing)
    '            errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '            tempException = tempException.InnerException
    '        End While

    '        MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '          + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '          "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'Finally
    '        '    If Not connection Is Nothing Then
    '        '        CType(connection, IDisposable).Dispose()
    '        '    End If
    '    End Try
    'End Function

    Private Function EliminarItems_Cheques() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idIngreso"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_EliminarItems", _
                                        param_id, param_res)

            EliminarItems_Cheques = param_res.Value

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function EliminarItems_Transferencias() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idIngreso"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_EliminarItems", _
                                        param_id, param_res)

            EliminarItems_Transferencias = param_res.Value

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function AgregarRemito_tmp() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim I As Integer
        Dim subtotal As Double = 0, MONTOiva As Double = 0, total As Double = 0

        RemitosAsociados = ""

        Try

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spFacturas_DELETE_tmp")

            Catch ex As Exception
                Throw ex
            End Try

            If bolModo = True Then
                lblSubtotal.Text = "0"
                lblIVA.Text = "0"
                lblTotal.Text = "0"

                lblTotalaPagarSinIva.Text = "0"
                lblTotalaPagar.Text = "0"

                'IVA = 0

                For I = 0 To grdFacturasConsumos.RowCount - 1

                    'If bolModo = True And CBool(grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                    If CBool(grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then


                        'If IVA = 0 Then
                        '    IVA = grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Iva).Value
                        'End If

                        'If IVA <> grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Iva).Value Then
                        '    'MsgBox("Ha seleccionado Facturas con diferentes porcentajes de IVA. Por favor, verifique", MsgBoxStyle.Critical, "Atención")
                        '    bandIVA = False
                        '    'Exit Function
                        'Else
                        '    bandIVA = True
                        'End If

                        If RemitosAsociados = "" Then
                            RemitosAsociados = grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.NroFactura).Value.ToString
                        Else
                            RemitosAsociados = RemitosAsociados + " ; " + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.NroFactura).Value.ToString
                        End If

                        Dim param_id As New SqlClient.SqlParameter
                        param_id.ParameterName = "@idfacturacion"
                        param_id.SqlDbType = SqlDbType.BigInt
                        param_id.Value = grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Id).Value
                        param_id.Direction = ParameterDirection.Input

                        'subtotal = subtotal + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Subtotal).Value
                        'MONTOiva = MONTOiva + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.MontoIva).Value
                        ''total = total + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Total).Value

                        total = total + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value

                        Try
                            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spFacturas_Insert_tmp", _
                                                      param_id)

                        Catch ex As Exception
                            Throw ex
                            Exit For
                        End Try
                        'Else
                        '    'If bolModo = False Then
                        '    '    subtotal = subtotal + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Subtotal).Value
                        '    '    MONTOiva = MONTOiva + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.MontoIva).Value
                        '    '    total = total + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Total).Value
                        '    'End If

                        '    If bolModo = False Then
                        '        'subtotal = subtotal + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Subtotal).Value
                        '        'MONTOiva = MONTOiva + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.MontoIva).Value
                        '        total = total + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Total).Value
                        '        'total = grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Total).Value
                        '        'total = CDbl(lblEntregado.Text)
                        '        Label14.Text = "Total"
                        '    Else
                        '        Label14.Text = "Total por Pagar"
                        '    End If


                    End If

                Next

                txtAyIDepUsado.Text = SumarAyID()

                lblSubtotal.Text = subtotal.ToString
                lblIVA.Text = MONTOiva.ToString
                lblTotal.Text = total.ToString

                'CalcularConAnti()

                lblTotalaPagarSinIva.Text = lblSubtotal.Text
            End If

            Dim Descuento As Double = 0

            If chkDescuento.Checked = True Then
                Descuento = IIf(txtDescuento.Text = "", 0, txtDescuento.Text)
            End If

            lblTotalaPagar.Text = CDbl(lblTotal.Text) - Descuento

            Calcular_MontoEntregado()

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

        Try
            Try
                conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(conn_del_form) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try

                Dim param_idPresGest As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_idPresGest.Value = CType(txtID.Text, Long)
                param_idPresGest.Direction = ParameterDirection.Input

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

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Delete", _
                                              param_idPresGest, param_userdel, param_res)

                    EliminarRegistro = param_res.Value

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

    ' Capturar los enter del formulario, descartar todos salvo los que 
    ' se dan dentro de la grilla. Es una sobre carga de un metodo existente.
    'Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

    '    ' Si la tecla presionada es distinta de la tecla Enter,
    '    ' abandonamos el procedimiento.
    '    '
    '    If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)

    '    ' Igualmente, si el control DataGridView no tiene el foco,
    '    ' y si la celda actual no está siendo editada,
    '    ' abandonamos el procedimiento.
    '    If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then Return MyBase.ProcessCmdKey(msg, keyData)

    '    ' Obtenemos la celda actual
    '    Dim cell As DataGridViewCell = grdItems.CurrentCell
    '    ' Índice de la columna.
    '    Dim columnIndex As Int32 = cell.ColumnIndex
    '    ' Índice de la fila.
    '    Dim rowIndex As Int32 = cell.RowIndex

    '    'Do
    '    '    If columnIndex = grdItems.Columns.Count - 1 Then
    '    '        If rowIndex = grdItems.Rows.Count - 1 Then
    '    '            ' Seleccionamos la primera columna de la primera fila.
    '    '            cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdPres_Ges_Det)
    '    '        Else
    '    '            ' Selecionamos la primera columna de la siguiente fila.
    '    '            cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdPres_Ges_Det)
    '    '        End If
    '    '    Else
    '    '        ' Seleccionamos la celda de la derecha de la celda actual.
    '    '        cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
    '    '    End If
    '    '    ' establecer la fila y la columna actual
    '    '    columnIndex = cell.ColumnIndex
    '    '    rowIndex = cell.RowIndex
    '    'Loop While (cell.Visible = False)

    '    grdItems.CurrentCell = cell

    '    ' ... y la ponemos en modo de edición.
    '    grdItems.BeginEdit(True)
    '    Return True

    'End Function

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        band = 0
        bolModo = True
        NroEleccion = 0
        Label14.Text = "Total por Pagar"

        grdFacturasConsumos.Enabled = bolModo
        cmbClientes.Enabled = bolModo
        txtOrdenPago.Enabled = bolModo
        dtpFECHA.Enabled = bolModo
        cmbRepartidor.Enabled = bolModo
        gpPago.Enabled = bolModo
        'TabControl1.Enabled = bolModo

        btnNuevoCheque.Enabled = bolModo
        btnNuevoTarjeta.Enabled = bolModo
        btnNuevoTransferencia.Enabled = bolModo

        btnAgregarCheque.Enabled = bolModo
        btnAgregarTarjeta.Enabled = bolModo
        btnAgregarTransf.Enabled = bolModo

        btnModificarCheque.Enabled = bolModo
        btnModificarTarjeta.Enabled = bolModo
        btnModificarTransf.Enabled = bolModo

        btnEliminarCheque.Enabled = bolModo
        btnEliminarTarjeta.Enabled = bolModo
        btnEliminarTransf.Enabled = bolModo

        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        Util.LimpiarTextBox(Me.Controls)

        dtpFechaTransf.Value = Date.Today
        dtpFechaCheque.Value = Date.Today

        'cmbCliente.Visible = True
        txtCliente.Visible = False
        cmbClientes.Visible = True
        'cmbCliente.SelectedIndex = 0

        lblTotal.Text = "0"
        lblSubtotal.Text = "0"
        lblIVA.Text = "0"
        lblEntregado.Text = "0"
        lblEntregaCheques.Text = "0"
        lblEntregaImpuestos.Text = "0"
        lblEntregaTarjetas.Text = "0"
        lblEntregaTransferencias.Text = "0"
        lblResto.Text = "0"

        txtEntregaContado.Enabled = bolModo
        txtRedondeo.Enabled = bolModo
        txtAyIDepUsado.Enabled = Not bolModo


        LlenarComboBancos()
        LlenarComboCuentasOrigen()

        grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Seleccionar).Visible = True

        LimpiarGrids()

        dtpFECHA.Value = Date.Today
        'dtpFECHA.Focus()

        chkDescuento.Checked = False
        chkDescuento.Enabled = bolModo
        txtDescuento.Text = ""

        band = 1

        LlenarGrid_Impuestos()

        cmbClientes_SelectedIndexChanged(sender, e)

        cmbRepartidor.SelectedIndex = 0

        cmbClientes.SelectedIndex = 0
        cmbClientes.Focus()

        txtValorCambio.Text = CDbl(ObtenerMoneda_ValorCambioDolar(ConnStringSEI))

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer

        If bolModo = False Then
            MsgBox("No están permitidas las modificaciones en los pagos realizados. Se desea modificar, deberá anular el pago y cargarlo nuevamente", MsgBoxStyle.Information, "Control de Errores")
            Exit Sub
        End If

        Util.MsgStatus(Status1, "Controlando la información...", My.Resources.Resources.indicator_white)

        'If bandIVA = False Then
        '    If MessageBox.Show("Ha seleccionado Facturas con diferentes porcentajes de IVA. ¿Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '        Exit Sub
        '    End If
        'End If

        'If txtOrdenPago.Text = "" Then
        '    If MessageBox.Show("No ha ingresado ninguna Orden de Pago del cliente. ¿Desea continuar sin este dato?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '        txtOrdenPago.Focus()
        '        Exit Sub
        '    End If
        'End If

        If CDbl(lblResto.Text) < 0 Then

            If SoloAyID() Then
                Util.MsgStatus(Status1, "No se puede generar una NC cuando se esta haciendo uso sólo de Anticipos o Ingresos por depósito.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se puede generar una NC cuando se esta haciendo uso sólo de Anticipos o Ingresos por depósito.", My.Resources.Resources.stop_error.ToBitmap, True)

                ' Util.MsgStatus(Status1, "Esta haciendo uso solo de Anticipos o Ingresos por depósito.", My.Resources.Resources.alert.ToBitmap)
                Exit Sub
            Else
                If MessageBox.Show("Se generará una Nota de Crédito por el valor de dinero extra que se ingresa. ¿Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If

        If CDbl(lblResto.Text) = CDbl(lblTotalaPagar.Text) Then
            MsgBox("Debe ingresar un monto para el pago.", MsgBoxStyle.Critical, "Atención")
            txtEntregaContado.Focus()
            Exit Sub
        End If

        If CDbl(lblResto.Text) > 0 Then
            If ControlarUltimaDeuda() Then
                If MessageBox.Show("El siguiente movimiento generará una deuda en alguna factura seleccionada. ¿Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    txtEntregaContado.Focus()
                    Exit Sub
                End If
            Else
                Util.MsgStatus(Status1, "El resto supera a la deuda de la última factura seleccionada. Por favor verifique el dato.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El resto supera a la deuda de la última factura seleccionada. Por favor verifique el dato.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarRegistro_Ingreso()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de Facturación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los movimientos asociados al pago...", My.Resources.Resources.indicator_white)
                        res = AgregarRegistro_FacturasConsumos()
                        Select Case res
                            Case -10
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo insertar la Nota de Débito.", My.Resources.Resources.stop_error.ToBitmap)
                            Case -5
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo actualizar el estado del Anticipo o Ingreso Depósito.", My.Resources.Resources.stop_error.ToBitmap)
                            Case -4
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo actualizar el estado del consumo.", My.Resources.Resources.stop_error.ToBitmap)
                            Case -3
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo actualizar el estado de la factura.", My.Resources.Resources.stop_error.ToBitmap)
                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "Se produjo un error durante la operación.", My.Resources.Resources.stop_error.ToBitmap)
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registra el movimiento.", My.Resources.Resources.stop_error.ToBitmap)
                            Case Else
                                Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                'If lblEntregaCheques.Text <> "" And lblEntregaCheques.Text <> "0" Then
                                If bolModo = False Then
                                    ControlarCantidad_Cheques()
                                End If
                                res = AgregarRegistro_Cheques()
                                Select Case res
                                    Case -3
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo actualizar la OC.", My.Resources.Resources.stop_error.ToBitmap)
                                    Case -1
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                    Case 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                    Case Else
                                        Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)

                                        If bolModo = False And res = 5 Then
                                            EliminarItems_Cheques()
                                        End If

                                End Select
                                'End If
                                'If lblEntregaImpuestos.Text <> "" And lblEntregaImpuestos.Text <> "0" Then

                                If bolModo = False Then
                                    ControlarCantidad_Impuestos()
                                End If

                                res = AgregarRegistro_Impuestos()
                                Select Case res
                                    Case Is <= 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudieron actualizar los registros de los Impuestos.", My.Resources.Resources.stop_error.ToBitmap)
                                    Case Else
                                        Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)

                                        'If bolModo = False Then
                                        ' EliminarItems_Impuestos()
                                        ' End If

                                End Select

                                'End If
                                Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                'If lblEntregaTransferencias.Text <> "" And lblEntregaTransferencias.Text <> "0" Then

                                If bolModo = False Then
                                    ControlarCantidad_Transferencias()
                                End If

                                '************** TRANSFERENCIAS ********************************

                                If grdTransferencias.Rows.Count > 0 Then
                                    res = AgregarRegistro_Transferencias()

                                    Select Case res
                                        Case -3
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo realizar la Transferencia.", My.Resources.Resources.stop_error.ToBitmap)
                                        Case -1
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                        Case 0
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                        Case Else
                                            Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)

                                            If bolModo = False Then
                                                EliminarItems_Transferencias()
                                            End If

                                    End Select
                                    'End If
                                End If

                                '********************* TARJETAS *************************

                                If grdTarjetas.Rows.Count > 0 Then
                                    res = AgregarRegistro_Tarjetas()

                                    Select Case res
                                        Case -1
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                        Case 0
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                        Case Else
                                            Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                    End Select

                                End If
                                
                                Util.MsgStatus(Status1, "Cerrando y generando el comprobante", My.Resources.Resources.indicator_white)
                                Cerrar_Tran()

                                Imprimir()

                                bolModo = False
                                PrepararBotones()
                                btnActualizar_Click(sender, e)

                                txtCliente.Visible = True

                                Me.Label5.Visible = False
                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                                grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Seleccionar).Visible = False
                                txtCODIGO.Enabled = bolModo
                                dtpFECHA.Enabled = bolModo

                                chkDescuento.Enabled = bolModo
                                txtDescuento.Enabled = bolModo

                                btnNuevoCheque.Enabled = bolModo
                                btnNuevoTarjeta.Enabled = bolModo
                                btnNuevoTransferencia.Enabled = bolModo

                                btnAgregarCheque.Enabled = bolModo
                                btnAgregarTarjeta.Enabled = bolModo
                                btnAgregarTransf.Enabled = bolModo

                                btnModificarCheque.Enabled = bolModo
                                btnModificarTarjeta.Enabled = bolModo
                                btnModificarTransf.Enabled = bolModo

                                btnEliminarCheque.Enabled = bolModo
                                btnEliminarTarjeta.Enabled = bolModo
                                btnEliminarTransf.Enabled = bolModo

                                NroEleccion = 0
                                LlenarComboClientes("00")
                                LlenarComboRepartidor()
                                grd_CurrentCellChanged(sender, e)

                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        nbreformreportes = "Comprobante de Pago"

        Dim paramConsumos As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Rpt As New frmReportes

        paramConsumos.AgregarParametros("N° de Mov:", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)

        paramConsumos.ShowDialog()

        If cerroparametrosconaceptar = True Then
            codigo = paramConsumos.ObtenerParametros(0)

            Imprimir_LlenarTMPs(codigo)

            'Rpt.MostrarReporte_PagodeClientes(codigo, Rpt)
            Rpt.PagodeClientes_App(codigo, Rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            paramConsumos = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'LlenarGridFacturas(CType(cmbCliente.SelectedValue, Long))
        Try
            LlenarGrid_Facturas(CType(grd.CurrentRow.Cells(19).Value, Long))
            AgregarRemito_tmp()

            grdFacturasConsumos.Enabled = bolModo

            'cmbCliente.Visible = False

            grdFacturasConsumos.Columns(ColumnasDelGridFacturasConsumos.Seleccionar).Visible = False
            NroEleccion = 0
            txtCliente.Visible = True
            txtOrdenPago.Enabled = bolModo
            cmbRepartidor.Enabled = bolModo
            dtpFECHA.Enabled = bolModo

            chkDescuento.Enabled = bolModo
            txtDescuento.Enabled = bolModo

            btnNuevoCheque.Enabled = bolModo
            btnNuevoTarjeta.Enabled = bolModo
            btnNuevoTransferencia.Enabled = bolModo

            btnAgregarCheque.Enabled = bolModo
            btnAgregarTarjeta.Enabled = bolModo
            btnAgregarTransf.Enabled = bolModo

            btnModificarCheque.Enabled = bolModo
            btnModificarTarjeta.Enabled = bolModo
            btnModificarTransf.Enabled = bolModo

            btnEliminarCheque.Enabled = bolModo
            btnEliminarTarjeta.Enabled = bolModo
            btnEliminarTransf.Enabled = bolModo

        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer

        If MessageBox.Show("Esta acción anulará el Pago y actualizará el estado de las Facturas/Consumos asociados." + vbCrLf + "Si existen Cheques vinculados y no fueron utilizados en otros pagos, serán eliminados, en caso contrario deberá Anular el movimiento donde estén dichos Cheques." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro()
            Select Case res
                Case -8
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "Al menos un cheque asociado al Pago está involucrado en el Pago a un Proveedor. Anule el pago al proveedor para luego anular el pago del cliente.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Al menos un cheque asociado al Pago está involucrado en el Pago a un Proveedor. Anule el pago al proveedor para luego anular el pago del cliente.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case Else
                    Cerrar_Tran()
                    bolModo = False
                    PrepararBotones()
                    btnActualizar_Click(sender, e)
                    'Setear_Grilla()
                    Util.MsgStatus(Status1, "Se anuló correctamente el movimiento de Pago.", My.Resources.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se anuló correctamente el movimiento de Pago.", My.Resources.Resources.ok.ToBitmap, True, True)

                    If grdFacturasConsumos.Rows.Count = 0 Then
                        btnNuevo_Click(sender, e)
                    End If
            End Select
            '
            ' cerrar la conexion si está abierta.
            '
            If Not conn_del_form Is Nothing Then
                CType(conn_del_form, IDisposable).Dispose()
            End If
        End If

    End Sub

#End Region

#Region "   Cheques"

    Private Sub btnNuevoCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCheque.Click
        txtNroCheque.Text = ""
        txtObservacionesCheque.Text = ""
        txtMontoCheque.Text = ""
        txtPropietario.Text = ""
        txtNroCheque.Focus()
    End Sub

    Private Sub btnModificarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarCheque.Click
        'modificar = 1
        'btnAgregarCheque_Click(sender, e)

        Dim I As Integer

        For I = 0 To grdCheques.RowCount - 1
            If I <> grdCheques.CurrentRow.Index Then
                If grdCheques.Rows(I).Cells(0).Value = txtNroCheque.Text Then
                    Util.MsgStatus(Status1, "Ya existe un cheque con este Nro para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next

        grdCheques.CurrentRow.Cells(0).Value = txtNroCheque.Text
        grdCheques.CurrentRow.Cells(1).Value = cmbBanco.Text
        grdCheques.CurrentRow.Cells(2).Value = CDec(txtMontoCheque.Text)
        grdCheques.CurrentRow.Cells(3).Value = dtpFechaCheque.Value
        grdCheques.CurrentRow.Cells(4).Value = txtPropietario.Text
        grdCheques.CurrentRow.Cells(5).Value = 1 'cmbMoneda.SelectedValue
        grdCheques.CurrentRow.Cells(6).Value = txtObservacionesCheque.Text

        lblEntregaCheques.Text = "0"

        For I = 0 To grdCheques.RowCount - 1
            lblEntregaCheques.Text = CDbl(lblEntregaCheques.Text) + grdCheques.Rows(I).Cells(2).Value
        Next

        Calcular_MontoEntregado()

        btnNuevoCheque_Click(sender, e)

    End Sub

    Private Sub btnAgregarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarCheque.Click
        If txtNroCheque.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el número del cheque.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el número del cheque.", My.Resources.Resources.alert.ToBitmap, True)
            txtNroCheque.Focus()
            Exit Sub
        End If

        If cmbBanco.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el nombre del Banco.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el nombre del Banco.", My.Resources.Resources.alert.ToBitmap, True)
            cmbBanco.Focus()
            Exit Sub
        End If

        If txtMontoCheque.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el monto del cheque.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el monto del cheque.", My.Resources.Resources.alert.ToBitmap, True)
            txtMontoCheque.Focus()
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To grdCheques.RowCount - 1
            If grdCheques.Rows(i).Cells(0).Value = txtNroCheque.Text Then
                Util.MsgStatus(Status1, "Ya existe un cheque con este Nro para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        Next

        Try
            grdCheques.Rows.Add(txtNroCheque.Text, cmbBanco.Text, CDec(txtMontoCheque.Text), dtpFechaCheque.Value, txtPropietario.Text, 1, txtObservacionesCheque.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaCheques.Text = CDbl(lblEntregaCheques.Text) + CDbl(txtMontoCheque.Text)

        Calcular_MontoEntregado()


    End Sub

    Private Sub btnEliminarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCheque.Click
        Dim connection As SqlClient.SqlConnection = Nothing

        Try

            If grdCheques.CurrentRow.Cells(8).Value Is DBNull.Value Or grdCheques.CurrentRow.Cells(8).Value = False Then
                connection = SqlHelper.GetConnection(ConnStringSEI)

                If Not grdCheques.CurrentRow.Cells(7).Value Is Nothing Then
                    SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Cheques_Ingreso where idCheque = " & grdCheques.CurrentRow.Cells(7).Value)
                End If

                grdCheques.Rows.Remove(grdCheques.CurrentRow)

                lblEntregaCheques.Text = "0"

                Dim i As Integer

                For i = 0 To grdCheques.RowCount - 1
                    lblEntregaCheques.Text = CDbl(lblEntregaCheques.Text) + grdCheques.Rows(i).Cells(2).Value
                Next

                Calcular_MontoEntregado()

            Else
                MsgBox("El cheque que intenta eliminar se encuentra utilizado en otro proceso")
            End If

        Catch ex As Exception

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub grdCheques_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCheques.SelectionChanged
        If Not grdCheques.CurrentRow Is Nothing Then
            txtNroCheque.Text = grdCheques.CurrentRow.Cells(0).Value
            cmbBanco.Text = grdCheques.CurrentRow.Cells(1).Value
            txtMontoCheque.Text = grdCheques.CurrentRow.Cells(2).Value
            Try
                dtpFechaCheque.Value = grdCheques.CurrentRow.Cells(3).Value
            Catch ex As Exception

            End Try
            txtPropietario.Text = grdCheques.CurrentRow.Cells(4).Value
            cmbMoneda.SelectedValue = grdCheques.CurrentRow.Cells(5).Value
            txtObservacionesCheque.Text = grdCheques.CurrentRow.Cells(6).Value
        Else
            txtNroCheque.Text = ""
            txtMontoCheque.Text = ""
            dtpFechaCheque.Value = Date.Today  'grdCheques.CurrentRow.Cells(3).Value
            txtPropietario.Text = ""
            'cmbMoneda.SelectedValue = grd.CurrentRow.Cells(5).Value
            txtObservacionesCheque.Text = ""
        End If
    End Sub

#End Region

#Region "   Transferencias"

    Private Sub btnNuevoTransferencia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoTransferencia.Click
        txtNroOpCliente.Text = ""
        txtMontoTransf.Text = ""
        cmbCuentaDestino.Text = ""
        cmbCuentaOrigen.Text = ""
        txtObservacionesTransf.Text = ""

        txtNroOpCliente.Focus()
    End Sub

    Private Sub btnModificarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarTransf.Click
        Dim I As Integer

        For I = 0 To grdTransferencias.RowCount - 1
            If I <> grdTransferencias.CurrentRow.Index Then
                If grdTransferencias.Rows(I).Cells(0).Value = txtNroOpCliente.Text Then
                    Util.MsgStatus(Status1, "Ya existe este nro de OpCliente para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next

        grdTransferencias.CurrentRow.Cells(0).Value = txtNroOpCliente.Text
        grdTransferencias.CurrentRow.Cells(1).Value = cmbCuentaOrigen.Text
        grdTransferencias.CurrentRow.Cells(2).Value = CDec(txtMontoTransf.Text)
        grdTransferencias.CurrentRow.Cells(3).Value = cmbCuentaDestino.Text
        grdTransferencias.CurrentRow.Cells(4).Value = dtpFechaTransf.Value
        grdTransferencias.CurrentRow.Cells(5).Value = 1
        grdTransferencias.CurrentRow.Cells(6).Value = cmbBancoOrigen.Text
        grdTransferencias.CurrentRow.Cells(7).Value = cmbBancoDestino.Text
        grdTransferencias.CurrentRow.Cells(8).Value = txtObservacionesTransf.Text

        lblEntregaTransferencias.Text = "0"

        For I = 0 To grdTransferencias.RowCount - 1
            lblEntregaTransferencias.Text = CDbl(lblEntregaTransferencias.Text) + grdTransferencias.Rows(I).Cells(2).Value
        Next

        Calcular_MontoEntregado()

        btnAgregarTransf_Click(sender, e)

    End Sub

    Private Sub btnAgregarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarTransf.Click

        If txtMontoTransf.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el Monto de la Transferencia.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el Monto de la Transferencia.", My.Resources.Resources.alert.ToBitmap, True)
            txtMontoTransf.Focus()
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To grdTransferencias.RowCount - 1
            If grdTransferencias.Rows(i).Cells(0).Value = txtNroOpCliente.Text And txtNroOpCliente.Text <> "" Then
                Util.MsgStatus(Status1, "Ya existe una transfernecia con este Nro de OP para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        Next

        Try
            grdTransferencias.Rows.Add(txtNroOpCliente.Text, cmbCuentaOrigen.Text, CDec(txtMontoTransf.Text), cmbCuentaDestino.Text, dtpFechaTransf.Value, 1, cmbBancoOrigen.Text, cmbBancoDestino.Text, txtObservacionesTransf.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaTransferencias.Text = CDbl(lblEntregaTransferencias.Text) + CDbl(txtMontoTransf.Text)

        Calcular_MontoEntregado()

        btnNuevoTransferencia_Click(sender, e)

    End Sub

    Private Sub btnEliminarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarTransf.Click
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            If Not grdTransferencias.CurrentRow.Cells(9).Value Is Nothing Then
                SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Transferencias_Ingreso where idtransferencia = " & grdTransferencias.CurrentRow.Cells(9).Value)
            End If

            grdTransferencias.Rows.Remove(grdTransferencias.CurrentRow)

            lblEntregaTransferencias.Text = "0"

            Dim i As Integer

            For i = 0 To grdTransferencias.RowCount - 1
                lblEntregaTransferencias.Text = CDbl(lblEntregaTransferencias.Text) + grdTransferencias.Rows(i).Cells(2).Value
            Next

            Calcular_MontoEntregado()

        Catch ex As Exception

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub grdTransferencias_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTransferencias.SelectionChanged
        If Not grdTransferencias.CurrentRow Is Nothing Then
            txtNroOpCliente.Text = grdTransferencias.CurrentRow.Cells(0).Value
            cmbCuentaOrigen.Text = grdTransferencias.CurrentRow.Cells(1).Value
            txtMontoTransf.Text = grdTransferencias.CurrentRow.Cells(2).Value
            cmbCuentaDestino.Text = grdTransferencias.CurrentRow.Cells(3).Value
            dtpFechaTransf.Value = grdTransferencias.CurrentRow.Cells(4).Value
            'cmbMonedaTransf.SelectedValue = grdTransferencias.CurrentRow.Cells(5).Value
            cmbBancoOrigen.Text = grdTransferencias.CurrentRow.Cells(6).Value
            cmbBancoDestino.Text = grdTransferencias.CurrentRow.Cells(7).Value
            txtObservacionesTransf.Text = grdTransferencias.CurrentRow.Cells(8).Value
        Else
            txtNroOpCliente.Text = ""
            txtMontoTransf.Text = ""
            cmbCuentaDestino.Text = ""
            cmbCuentaOrigen.Text = ""
            txtObservacionesTransf.Text = ""
        End If
    End Sub

#End Region



    Private Sub ChkAyID_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAyIDep.CheckedChanged

        txtAyIDepUsado.Enabled = ChkAyIDep.Checked
        If ChkAyIDep.Checked Then
            txtAyIDepUsado.ForeColor = Color.Green
        Else
            txtAyIDepUsado.ForeColor = Color.Black
        End If
        txtAyIDepUsado.Text = SumarAyID()

        'CalcularConAnti()

    End Sub

    Private Function SumarAyID() As Double

        Dim Suma As Double = 0

        For i As Integer = 0 To grdFacturasConsumos.Rows.Count - 1
            If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value Then
                If grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "IDEP" Or grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "ANTI" Then
                    Suma = Suma + grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value * (-1)
                    txtAyIDepUsado.Text = Suma
                End If
            End If
        Next

        Return Suma

    End Function

    Private Sub CalcularConAnti()

        Dim total As Double = 0
        For I As Integer = 0 To grdFacturasConsumos.RowCount - 1

            If bolModo = True And CBool(grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                If ChkAyIDep.Checked Then
                    If Not grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "IDEP" And Not grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Tipo).Value = "ANTI" Then
                        total = total + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value
                    End If
                Else
                    total = total + grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value
                End If
            End If
        Next

        lblTotal.Text = total.ToString
        lblTotalaPagarSinIva.Text = lblSubtotal.Text
        lblTotalaPagar.Text = lblTotal.Text

        Calcular_MontoEntregado()

    End Sub

    Private Function SoloAyID() As Boolean

        If (txtEntregaContado.Text = "" Or txtEntregaContado.Text = "0" Or lblEntregaCheques.Text = "" Or lblEntregaCheques.Text = "0" _
               Or lblEntregaTransferencias.Text = "" Or lblEntregaTransferencias.Text = "0" Or lblEntregaTarjetas.Text = "" Or lblEntregaTarjetas.Text = "0") And _
                    ChkAyIDep.Checked And txtAyIDepUsado.Text <> "" And txtAyIDepUsado.Text <> "0" Then
            SoloAyID = True
        Else
            SoloAyID = False
        End If

    End Function

    Private Sub lblAyIDepUsado_TextChanged(sender As Object, e As EventArgs)
        If band = 1 Then
            Calcular_MontoEntregado()
        End If
    End Sub

    Private Function ControlarUltimaDeuda() As Boolean

        For I As Integer = 0 To grdFacturasConsumos.RowCount - 1

            If bolModo = True And CBool(grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Seleccionar).Value) = True Then
                If grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.NroMov).Value = UltimoNum Then
                    If grdFacturasConsumos.Rows(I).Cells(ColumnasDelGridFacturasConsumos.Deuda).Value > CDbl(lblResto.Text) Then
                        ControlarUltimaDeuda = True
                    Else
                        ControlarUltimaDeuda = False
                    End If
                End If
            End If

        Next

    End Function

    Private Sub chkDescuento_CheckedChanged(sender As Object, e As EventArgs) Handles chkDescuento.CheckedChanged

        chkDescuento.Enabled = bolModo

        If bolModo = False Then
            txtDescuento.Enabled = False
        Else
            txtDescuento.Enabled = chkDescuento.Checked
        End If

        If chkDescuento.Checked = False Then
            txtDescuento.Text = ""
        Else
            txtDescuento.Focus()
        End If

    End Sub

#Region "Tarjetas"

    Private Sub btnNuevoTarjeta_Click(sender As Object, e As EventArgs) Handles btnNuevoTarjeta.Click
        txtMontoTarjeta.Text = ""
        lblRecargoTarjeta.Text = ""
        lblMontoTotalTarjeta.Text = "0"
        cmbTarjetas.Focus()
    End Sub

    Private Sub btnAgregarTarjeta_Click(sender As Object, e As EventArgs) Handles btnAgregarTarjeta.Click
        If cmbTarjetas.Text = "" Then
            MsgBox("Debe ingresar una tarjeta para poder continuar", MsgBoxStyle.Critical, "Control de Errores")
            cmbTarjetas.Focus()
            Exit Sub
        End If

        If txtMontoTarjeta.Text = "0" Or txtMontoTarjeta.Text = "" Then
            MsgBox("Debe ingresar un monto válido para la tarjeta de Crédito", MsgBoxStyle.Critical, "Control de Errores")
            txtMontoTarjeta.Focus()
            Exit Sub
        End If

        'Dim i As Integer

        'For i = 0 To grdTarjetas.RowCount - 1
        '    If grdTarjetas.Rows(i).Cells(0).Value = txtNroCheque.Text Then
        '        Util.MsgStatus(Status1, "Ya existe una con este Nro para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
        '        Exit Sub
        '    End If
        'Next

        Try
            grdTarjetas.Rows.Add(cmbTarjetas.Text, lblRecargoTarjeta.Text, lblCuotasTarjeta.Text, txtMontoTarjeta.Text, lblMontoTotalTarjeta.Text, cmbTarjetas.SelectedValue)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaTarjetas.Text = CDbl(lblEntregaTarjetas.Text) + CDbl(lblMontoTotalTarjeta.Text)

        Calcular_MontoEntregado()

        btnNuevoTarjeta_Click(sender, e)

        cmbTarjetas_SelectedIndexChanged(sender, e)

    End Sub

    Private Sub txtMontoTarjeta_TextChanged(sender As Object, e As EventArgs) Handles txtMontoTarjeta.TextChanged

        If txtMontoTarjeta.Text = "" Then Exit Sub

        If lblRecargoTarjeta.Text = "0" Or lblRecargoTarjeta.Text = "" Then
            lblMontoTotalTarjeta.Text = txtMontoTarjeta.Text
        Else
            Dim PorcTarjeta As Double

            PorcTarjeta = 1 + (lblRecargoTarjeta.Text / 100)

            lblMontoTotalTarjeta.Text = CDbl(txtMontoTarjeta.Text) * PorcTarjeta
        End If
    End Sub

    Private Sub btnEliminarTarjeta_Click(sender As Object, e As EventArgs) Handles btnEliminarTarjeta.Click
        'Dim connection As SqlClient.SqlConnection = Nothing

        If grdTarjetas.Rows.Count = 0 Then
            Exit Sub
        End If

        Try

            'If grdCheques.CurrentRow.Cells(8).Value Is DBNull.Value Or grdCheques.CurrentRow.Cells(8).Value = False Then
            '    connection = SqlHelper.GetConnection(ConnStringSEI)

            '    If Not grdCheques.CurrentRow.Cells(7).Value Is Nothing Then
            '        SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Cheques_Ingreso where idCheque = " & grdCheques.CurrentRow.Cells(7).Value)
            '    End If

            Dim ChequeEliminar As Double

            ChequeEliminar = grdTarjetas.CurrentRow.Cells(4).Value

            grdTarjetas.Rows.Remove(grdTarjetas.CurrentRow)

            'lblEntregaTarjetas.Text = "0"

            'Dim i As Integer

            'For i = 0 To grdTarjetas.RowCount - 1
            lblEntregaTarjetas.Text = CDbl(lblEntregaTarjetas.Text) - ChequeEliminar
            'Next

            Calcular_MontoEntregado()

            'Else
            'MsgBox("El cheque que intenta eliminar se encuentra utilizado en otro proceso")
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try
    End Sub

    Private Sub txtMontoTarjeta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoTarjeta.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAgregarTarjeta.Focus()
        End If
    End Sub

    Private Sub btnModificarTarjeta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarTarjeta.Click
        Dim I As Integer

        grdTarjetas.CurrentRow.Cells(0).Value = cmbTarjetas.Text
        grdTarjetas.CurrentRow.Cells(1).Value = lblRecargoTarjeta.Text
        grdTarjetas.CurrentRow.Cells(2).Value = lblCuotasTarjeta.Text
        grdTarjetas.CurrentRow.Cells(3).Value = txtMontoTarjeta.Text
        grdTarjetas.CurrentRow.Cells(4).Value = lblMontoTotalTarjeta.Text
        grdTarjetas.CurrentRow.Cells(5).Value = cmbTarjetas.SelectedValue

        lblEntregaTarjetas.Text = "0"

        For I = 0 To grdTarjetas.RowCount - 1
            lblEntregaTarjetas.Text = CDbl(lblEntregaTarjetas.Text) + grdTarjetas.Rows(I).Cells(4).Value
        Next

        Calcular_MontoEntregado()

    End Sub

    Private Sub cmbTarjetas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTarjetas.SelectedIndexChanged

        If band = 1 Then

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

                sqltxt2 = " SELECT Cuotas, porcenRecar, Nombre FROM Tarjetas WHERE codigo = '" & cmbTarjetas.SelectedValue & "'"

                Dim cmd As New SqlCommand(sqltxt2, connection)
                Dim da As New SqlDataAdapter(cmd)

                da.Fill(dt)

                lblCuotasTarjeta.Text = dt.Rows(0)(0).ToString
                lblRecargoTarjeta.Text = dt.Rows(0)(1).ToString
                NombreTarjeta = dt.Rows(0)(2).ToString

                If lblRecargoTarjeta.Text = "0" Or lblRecargoTarjeta.Text = "" Then
                    lblMontoTotalTarjeta.Text = txtMontoTarjeta.Text
                Else
                    Dim PorcTarjeta As Double

                    PorcTarjeta = 1 + (CDbl(lblRecargoTarjeta.Text) / 100)

                    If txtMontoTarjeta.Text = "" Then txtMontoTarjeta.Text = "0"

                    lblMontoTotalTarjeta.Text = CDbl(txtMontoTarjeta.Text) * PorcTarjeta
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If Not connection Is Nothing Then
                    CType(connection, IDisposable).Dispose()
                End If
            End Try

        End If

    End Sub


    Private Sub grdTarjetas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdTarjetas.CellContentClick

        If grdTarjetas.Rows.Count = 0 Then Exit Sub

        cmbTarjetas.Text = grdTarjetas.CurrentRow.Cells(0).Value
        lblRecargoTarjeta.Text = grdTarjetas.CurrentRow.Cells(1).Value
        lblCuotasTarjeta.Text = grdTarjetas.CurrentRow.Cells(2).Value
        txtMontoTarjeta.Text = grdTarjetas.CurrentRow.Cells(3).Value
        lblMontoTotalTarjeta.Text = grdTarjetas.CurrentRow.Cells(4).Value
        cmbTarjetas.SelectedValue = grdTarjetas.CurrentRow.Cells(5).Value

    End Sub

#End Region




End Class '