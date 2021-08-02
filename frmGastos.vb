Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmGastos

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

    Dim band As Integer

    Enum ColumnasDelGridItemsIVA
        id = 0
        Subtotal = 1
        PorcIva = 2
        MontoIVA = 3
    End Enum

    Enum ColumnasDelGridImpuestos
        Id = 0
        codigo = 1
        NroDocumento = 2
        Monto = 3
        IdIngreso = 4
        IdImpuestoxGasto = 5
    End Enum

#Region "   Eventos"

    Private Sub frmGastos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        band = 0

        configurarform()

        dtpFECHA.Value = Date.Today

        'LlenarComboProveedores(cmbProveedor)
        LlenarcmbProveedores_App(cmbProveedor, ConnStringSEI, 0, 0)

        LlenarComboGastos(cmbTipoGasto)
        LlenarcmbTipoFacturas_APP(cmbTipoComprobante, ConnStringSEI)

        asignarTags()

        SQL = "exec spGastos_Select_All @Eliminado = 0"

        LlenarGrilla()

        Permitir = True

        CargarCajas()

        PrepararBotones()

        If bolModo = True Then
            LlenarGrid_IVA(0)
            LlenarGrid_Impuestos()

        Else
            LlenarGrid_IVA(CType(txtID.Text, Long))

            LlenarGrid_Impuestos()

        End If

        grd.Columns(3).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(6).Visible = False
        grd.Columns(7).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(17).Visible = False
        grd.Columns(18).Visible = False
        grd.Columns(20).Visible = False
        grd.Columns(21).Visible = False
        grd.Columns(23).Visible = False
        grd.Columns(24).Visible = False
        grd.Columns(25).Visible = False
        grd.Columns(26).Visible = False
        grd.Columns(28).Visible = False
        grd.Columns(29).Visible = False
        grd.Columns(30).Visible = False
        grd.Columns(31).Visible = False
        grd.Columns(32).Visible = False
        grd.Columns(33).Visible = False
        grd.Columns(34).Visible = False

        If grd.RowCount > 0 Then
            txtCantIVA.Value = grd.CurrentRow.Cells(34).Value
        End If

        dtpFECHA.MaxDate = Today.Date
        dtpFECHA.Focus()

        band = 1

    End Sub

    Private Sub frmGastosporObras_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Gasto Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Gasto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub txtNota_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNota.KeyDown, txtCodigo.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub PicProveedores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picProveedores.Click
        Dim f As New frmProveedores
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbProveedor.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbProveedores_App(cmbProveedor, ConnStringSEI, 0, 0)
        cmbProveedor.Text = texto_del_combo
    End Sub

   Private Sub cmbTipoComprobante_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoComprobante.SelectedIndexChanged

        txtMontoIVA10.Text = "0"
        txtMontoIVA21.Text = "0"
        txtMontoIVA27.Text = "0"

        If cmbTipoComprobante.Text = "FACTURAS A" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO A" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO A" Or _
            cmbTipoComprobante.Text = "RECIBOS A" Or _
            cmbTipoComprobante.Text = "FACTURAS M" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO M" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO M" Or _
            cmbTipoComprobante.Text = "RECIBOS M" Or _
            cmbTipoComprobante.Text = "TIQUE FACTURA A" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415" Then
            txtCantIVA.Enabled = True
            txtCantIVA.Value = 1
            txtMontoIVA10.Enabled = True
            txtMontoIVA21.Enabled = True
            txtMontoIVA27.Enabled = True
        Else
            txtCantIVA.Enabled = False
            txtCantIVA.Value = 0
            txtMontoIVA10.Enabled = False
            txtMontoIVA21.Enabled = False
            txtMontoIVA27.Enabled = False
        End If

        If cmbTipoComprobante.Text.Contains("CREDITO") Then
            LlenarLista_Facturas()
        Else
            lstFacturasPendientes.Items.Clear()
        End If

        'CalcularMontoIVA()

        'txtSubtotal_LostFocus(sender, e)

        'CalcularMontoIVA()

    End Sub

    Private Sub txtsubtotal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotal.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotal.LostFocus
        If band = 1 Then
            If txtSubtotal.Text <> "" Then

                If cmbTipoComprobante.Text = "FACTURAS A" Or _
                    cmbTipoComprobante.Text = "NOTAS DE CREDITO A" Or _
                    cmbTipoComprobante.Text = "NOTAS DE DEBITO A" Or _
                    cmbTipoComprobante.Text = "RECIBOS A" Or _
                    cmbTipoComprobante.Text = "FACTURAS M" Or _
                    cmbTipoComprobante.Text = "NOTAS DE CREDITO M" Or _
                    cmbTipoComprobante.Text = "NOTAS DE DEBITO M" Or _
                    cmbTipoComprobante.Text = "RECIBOS M" Or _
                    cmbTipoComprobante.Text = "TIQUE FACTURA A" Or _
                    cmbTipoComprobante.Text = "NOTAS DE DEBITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415" Then

                    If bolModo = True Then
                        txtMontoIVA21.Text = Format(txtSubtotal.Text * 0.21, "###0.00")
                        CalcularMontoIVA()
                    End If

                End If

                If lblImpuestos.Text = "" Then lblImpuestos.Text = "0"
                If lblMontoIva.Text = "" Then lblMontoIva.Text = "0"
                If txtSubtotal.Text = "" Then txtSubtotal.Text = "0"
                If txtSubtotalExento.Text = "" Then txtSubtotalExento.Text = "0"

                lblTotal.Text = CDbl(txtSubtotalExento.Text) + CDbl(txtSubtotal.Text) + CDbl(lblMontoIva.Text) + CDbl(lblImpuestos.Text)

            End If
        End If
    End Sub

    Private Sub txtsubtotalNoGravado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotalExento.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotalNoGravado_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotalExento.LostFocus
        If band = 1 Then
            If txtSubtotalExento.Text <> "" Then

                If lblImpuestos.Text = "" Then lblImpuestos.Text = "0"
                If lblMontoIva.Text = "" Then lblMontoIva.Text = "0"
                If txtSubtotal.Text = "" Then txtSubtotal.Text = "0"

                lblTotal.Text = CDbl(txtSubtotalExento.Text) + CDbl(txtSubtotal.Text) + CDbl(lblMontoIva.Text) + CDbl(lblImpuestos.Text)
            End If
        End If
    End Sub

    Private Sub cmbProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProveedor.SelectedIndexChanged
        If band = 1 Then '--And bolModo = True
            txtIdProveedor.Text = cmbProveedor.SelectedValue
            LlenarComboRecepciones(cmbRecepcion)
            txtValorCambio.Enabled = chkRecepcion.Checked

            If bolModo = True And (cmbProveedor.Text.ToString.Contains("SCHNEI") Or
                               cmbProveedor.Text.ToString.ToUpper.Contains("FINNI")) Then
                txtValorCambio.Enabled = True
                lblNroRemito.Text = ""
                txtNroRemitoCompleto.Text = ""
                txtIdRecepcion.Text = ""
                BuscarMoneda_OC(False)
            Else
                lblNroRemito.Text = ""
                txtNroRemitoCompleto.Text = ""
                txtIdRecepcion.Text = ""
                txtIdMoneda.Text = "1"
                txtValorCambio.Text = "1"
                txtTipoMoneda.Text = "Pe"
                txtValorCambio.Enabled = chkRecepcion.Checked
            End If
        End If
    End Sub

    Private Sub chkRecepcion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRecepcion.CheckedChanged
        lblNroRecepcion.Enabled = chkRecepcion.Checked
        cmbRecepcion.Enabled = chkRecepcion.Checked
        lblNroRemito.Enabled = chkRecepcion.Checked
        lblNroRemito1.Enabled = chkRecepcion.Checked

        txtPtoVtaRemito.Enabled = Not chkRecepcion.Checked
        txtNroCompRemito.Enabled = Not chkRecepcion.Checked

        If bolModo = True And (cmbProveedor.Text.ToString.Contains("SCHNEI") Or
                               cmbProveedor.Text.ToString.ToUpper.Contains("FINNI")) Then
            txtValorCambio.Enabled = True
        Else
            txtValorCambio.Enabled = chkRecepcion.Checked
        End If

        Label4.Enabled = Not chkRecepcion.Checked

        If chkRecepcion.Checked = False And bolModo = True Then
            txtPtoVtaRemito.Text = ""
            txtNroCompRemito.Text = ""
        End If

        cmbRecepciones_SelectedIndexChanged(sender, e)

    End Sub

    Private Sub cmbRecepciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRecepcion.SelectedIndexChanged
        If band = 1 And chkRecepcion.Checked = True And bolModo = True Then
            BuscarRemito()
            txtIdRecepcion.Text = cmbRecepcion.SelectedValue
            BuscarMoneda_OC(True)
        Else
            If chkRecepcion.Checked = False And bolModo = True And (cmbProveedor.Text.ToString.Contains("SCHNEI") Or cmbProveedor.Text.ToString.ToUpper.Contains("FINNI")) Then
                lblNroRemito.Text = ""
                txtNroRemitoCompleto.Text = ""
                txtIdRecepcion.Text = ""
                BuscarMoneda_OC(False)
            Else
                If chkRecepcion.Checked = False And bolModo = True Then
                    lblNroRemito.Text = ""
                    txtNroRemitoCompleto.Text = ""
                    txtIdRecepcion.Text = ""
                    txtIdMoneda.Text = "1"
                    txtValorCambio.Text = "1"
                    txtTipoMoneda.Text = "Pe"
                End If
            End If
        End If
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        If Permitir Then
            Try
                LlenarGrid_IVA(CLng(txtID.Text))

                LlenarGrid_Impuestos()

                If cmbTipoComprobante.Text.Contains("CREDITO") Then
                    LlenarLista_Facturas()
                Else
                    lstFacturasPendientes.Items.Clear()
                End If

                If txtPtoVta.Text = "" Then
                    'txtNroRemitoCompleto.Enabled = True
                    cmbTipoComprobante.Enabled = True
                    txtPtoVta.Enabled = True
                    txtNroFactura.Enabled = True
                Else
                    'txtNroRemitoCompleto.Enabled = False
                    cmbTipoComprobante.Enabled = False
                    txtPtoVta.Enabled = False
                    txtNroFactura.Enabled = False
                End If

                txtCantIVA.Value = grd.CurrentRow.Cells(34).Value
                txtIdProveedor.Text = grd.CurrentRow.Cells(3).Value
                chkRecepcion.Checked = CBool(grd.CurrentRow.Cells(35).Value)

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub chkPeriodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPeriodo.CheckedChanged
        dtpPeriodo.Visible = chkPeriodo.Checked
    End Sub

    Private Sub chkAnuladas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnulados.CheckedChanged
        btnGuardar.Enabled = Not chkAnulados.Checked
        btnEliminar.Enabled = Not chkAnulados.Checked
        btnNuevo.Enabled = Not chkAnulados.Checked
        btnCancelar.Enabled = Not chkAnulados.Checked

        If chkAnulados.Checked = True Then
            SQL = "exec spGastos_Select_All @Eliminado = 1"
        Else
            SQL = "exec spGastos_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        LlenarGrid_IVA(CType(txtID.Text, Long))

        LlenarGrid_Impuestos()

    End Sub

    Private Sub validar_NumerosReales_Impuestos( _
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

    Private Sub txtPtoVta_LostFocus(sender As Object, e As EventArgs) Handles txtPtoVta.LostFocus
        If band = 1 Then '  And bolModo = True Then
            txtNroFacturaCompleto.Text = txtPtoVta.Text.Trim.PadLeft(4, "0") & "-" & txtNroFactura.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub txtNroFactura_LostFocus(sender As Object, e As EventArgs) Handles txtNroFactura.LostFocus
        If band = 1 Then 'And bolModo = True Then
            txtNroFacturaCompleto.Text = txtPtoVta.Text.Trim.PadLeft(4, "0") & "-" & txtNroFactura.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub txtPtoVtaRemito_LostFocus(sender As Object, e As EventArgs) Handles txtPtoVtaRemito.LostFocus
        If band = 1 Then 'And bolModo = True Then
            txtNroRemitoCompleto.Text = txtPtoVtaRemito.Text.Trim.PadLeft(4, "0") & "-" & txtNroCompRemito.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub txtNroCompRemito_LostFocus(sender As Object, e As EventArgs) Handles txtNroCompRemito.LostFocus
        If band = 1 Then 'And bolModo = True Then
            txtNroRemitoCompleto.Text = txtPtoVtaRemito.Text.Trim.PadLeft(4, "0") & "-" & txtNroCompRemito.Text.Trim.PadLeft(8, "0")
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

                lblImpuestos.Text = "0"

                For i = 0 To grdImpuestos.Rows.Count - 1
                    lblImpuestos.Text = CDbl(lblImpuestos.Text) + CDbl(grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.Monto).Value)
                Next

                lblTotal.Text = CDbl(txtSubtotal.Text) + CDbl(lblImpuestos.Text) + CDbl(lblMontoIva.Text)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdImpuestos_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdImpuestos.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales_Impuestos

    End Sub

    Private Sub txtMontoIVA21_LostFocus(sender As Object, e As EventArgs) Handles txtMontoIVA21.LostFocus
        CalcularMontoIVA()
    End Sub

    Private Sub txtMontoIVA10_LostFocus(sender As Object, e As EventArgs) Handles txtMontoIVA10.LostFocus
        CalcularMontoIVA()
    End Sub

    Private Sub txtMontoIVA27_LostFocus(sender As Object, e As EventArgs) Handles txtMontoIVA27.LostFocus
        CalcularMontoIVA()
    End Sub

    Private Sub txtMontoIVA21_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoIVA21.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            CalcularMontoIVA()
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtMontoIVA10_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoIVA10.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            CalcularMontoIVA()
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtMontoIVA27_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoIVA27.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            CalcularMontoIVA()
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCantIVA_ValueChanged(sender As Object, e As EventArgs) Handles txtCantIVA.ValueChanged
        If cmbTipoComprobante.Text = "FACTURAS A" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO A" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO A" Or _
            cmbTipoComprobante.Text = "RECIBOS A" Or _
            cmbTipoComprobante.Text = "FACTURAS M" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO M" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO M" Or _
            cmbTipoComprobante.Text = "RECIBOS M" Or _
            cmbTipoComprobante.Text = "TIQUE FACTURA A" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO O DOCUMENTO EQUIVALENTE QUE CUMPLAN CON LA R.G. N° 1415" Then

            If txtCantIVA.Value = 0 Then
                txtCantIVA.Value = 1
            End If
        End If
    End Sub

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        Util.LimpiarTextBox(Me.Controls)

        PrepararBotones()

        chkFacturaCancelada.Checked = False
        chkRecepcion.Enabled = True
        chkRecepcion.Checked = False

        lblNroRemito.Text = ""
        txtSubtotal.Text = "0"
        txtSubtotalExento.Text = "0"
        lblTotal.Text = "0"
        lblMontoIva.Text = "0"
        lblImpuestos.Text = "0"
        txtNroRemitoCompleto.Text = ""

        dtpFECHA.Value = Date.Today

        cmbTipoComprobante.SelectedIndex = 0
        chkPeriodo.Checked = False
        dtpPeriodo.Visible = False

        txtPtoVtaRemito.Enabled = bolModo
        txtNroCompRemito.Enabled = bolModo
        cmbTipoComprobante.Enabled = bolModo
        txtPtoVta.Enabled = bolModo
        txtNroFactura.Enabled = bolModo

        LlenarGrid_IVA(0)

        LlenarGrid_Impuestos()

        txtTipoMoneda.Text = "Pe"
        txtValorCambio.Text = 1
        txtIdMoneda.Text = 1

        txtPtoVtaRemito.Text = "0"
        txtNroCompRemito.Text = "0"
        txtNroRemitoCompleto.Text = "0000-00000000"

        txtPtoVta.Text = "0"
        txtNroFactura.Text = "0"
        txtNroFacturaCompleto.Text = "0000-00000000"

        dtpFECHA.Focus()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If bolModo = False Then
            If MessageBox.Show("¿Está seguro que desea modificar el Gasto seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim res As Integer
        Dim ControlFactura As Boolean
        Dim ControlRemito As Boolean

        If txtNroRemitoCompleto.Text <> txtNroRemitoControl.Text Then
            ControlRemito = True
        Else
            ControlRemito = False
        End If

        If txtNroFacturaCompleto.Text <> txtNroFacturaCompletoControl.Text Then
            ControlFactura = True
        Else
            ControlFactura = False
        End If

        'If cmbProveedor.Text.ToString.Contains("SCHNEI") Then

        '    If chkRecepcion.Checked = False Then
        '        Util.MsgStatus(Status1, "Para ingresar un Gasto de este proveedor, debe haber ingresado una Recepción de Materiales previamente.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Para ingresar un Gasto de este proveedor, debe haber ingresado una Recepción de Materiales previamente.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        chkRecepcion.Focus()
        '        Exit Sub
        '    End If

        '    If txtIdRecepcion.Text = "" Then
        '        Util.MsgStatus(Status1, "Para ingresar un Gasto de este proveedor, debe haber ingresado una Recepción de Materiales previamente.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Para ingresar un Gasto de este proveedor, debe haber ingresado una Recepción de Materiales previamente.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        chkRecepcion.Focus()
        '        Exit Sub
        '    End If

        '    If txtPtoVtaRemito.Text = "" Then
        '        Util.MsgStatus(Status1, "Debe cargar el Pto de Vta para continuar", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Debe cargar el Pto de Vta para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
        '        txtPtoVtaRemito.Focus()
        '        Exit Sub
        '    End If

        '    If txtNroCompRemito.Text = "" Then
        '        Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
        '        txtNroCompRemito.Focus()
        '        Exit Sub
        '    End If
        'Else

        If cmbProveedor.Text.ToString.Contains("SCHNEI") And cmbTipoComprobante.Text = "FACTURAS A" And CDbl(lblImpuestos.Text) = 0 Then
            MsgBox("El tipo de comprobante ingresado para SCHNEIDER requiere tener IMPUESTOS asociados", MsgBoxStyle.Critical, "Control de Errores")
            grdImpuestos.Focus()
            Exit Sub
        End If

        If cmbProveedor.Text.ToString.Contains("SCHNEI") And cmbTipoComprobante.Text = "FACTURAS A" Then
            Dim i As Integer = 0
            For i = 0 To grdImpuestos.Rows.Count - 1
                If grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.codigo).Value.ToString.Contains("IIBB") And _
                    grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.Monto).Value = 0 Then
                    MsgBox("El tipo de comprobante ingresado para SCHNEIDER requiere tener IIBB asociados", MsgBoxStyle.Critical, "Control de Errores")
                    grdImpuestos.Focus()
                    Exit Sub
                End If
            Next
        End If

        If txtPtoVtaRemito.Text = "" Then txtPtoVtaRemito.Text = "0"
        If txtNroCompRemito.Text = "" Then txtNroCompRemito.Text = "0"

        If CInt(txtPtoVtaRemito.Text) = 0 And CInt(txtNroCompRemito.Text) > 0 Then
            Util.MsgStatus(Status1, "Debe cargar el Pto de Vta del Remito para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Pto de Vta del Remito para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtPtoVtaRemito.Focus()
            Exit Sub
        End If

        If CInt(txtPtoVtaRemito.Text) > 0 And CInt(txtNroCompRemito.Text) = 0 Then
            Util.MsgStatus(Status1, "Debe cargar el Nro de Comprobante del Remito para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Nro de Comprobante del Remito para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtNroCompRemito.Focus()
            Exit Sub
        End If

        'End If

        If txtPtoVta.Text = "" Then
            Util.MsgStatus(Status1, "Debe cargar el Pto de Vta para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Pto de Vta para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtPtoVta.Focus()
            Exit Sub
        End If

        If txtNroFactura.Text = "" Then
            Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtNroFactura.Focus()
            Exit Sub
        End If

        If txtNroFacturaCompleto.Text = "" Then
            Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtPtoVta.Focus()
            Exit Sub
        End If

        If CInt(txtPtoVta.Text) = 0 And CInt(txtNroFactura.Text) = 0 Then
            Util.MsgStatus(Status1, "Debe cargar la Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar la Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtPtoVta.Focus()
            Exit Sub
        End If

        If CInt(txtPtoVta.Text) = 0 And CInt(txtNroFactura.Text) > 0 Then
            Util.MsgStatus(Status1, "Debe cargar el Pto de Vta de la Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Pto de Vta de la Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtPtoVta.Focus()
            Exit Sub
        End If

        If CInt(txtPtoVta.Text) > 0 And CInt(txtNroFactura.Text) = 0 Then
            Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
            txtNroFactura.Focus()
            Exit Sub
        End If

        If txtSubtotal.Text = "" And txtSubtotalExento.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar un monto para el gasto que está cargando.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar un monto para el gasto que está cargando.", My.Resources.Resources.stop_error.ToBitmap, True)
            txtSubtotal.Focus()
            Exit Sub
        Else
            If CDbl(txtSubtotal.Text) = 0 And CDbl(txtSubtotalExento.Text) = 0 Then
                Util.MsgStatus(Status1, "Debe ingresar un monto para el gasto que está cargando.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar un monto para el gasto que está cargando.", My.Resources.Resources.stop_error.ToBitmap, True)
                txtSubtotal.Focus()
                Exit Sub
            End If
        End If

        If CDbl(txtValorCambio.Text) <= 0 Then
            Util.MsgStatus(Status1, "Debe ingresar un monto válido para el tipo de cambio.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar un monto válido para el tipo de cambio.", My.Resources.Resources.stop_error.ToBitmap, True)
            txtValorCambio.Focus()
            Exit Sub
        End If

        If txtTipoMoneda.Text = "Pe" And CInt(txtValorCambio.Text) <> 1 Then
            Util.MsgStatus(Status1, "El tipo de cambio para la Moneda Pesos debe ser 1.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El tipo de cambio para la Moneda Pesos debe ser 1.", My.Resources.Resources.stop_error.ToBitmap, True)
            txtValorCambio.Focus()
            Exit Sub
        End If

        txtMontoIVA10.Text = IIf(txtMontoIVA10.Text = "", 0, txtMontoIVA10.Text)
        txtMontoIVA21.Text = IIf(txtMontoIVA21.Text = "", 0, txtMontoIVA21.Text)
        txtMontoIVA27.Text = IIf(txtMontoIVA27.Text = "", 0, txtMontoIVA27.Text)

        If txtCantIVA.Value > 0 Or cmbTipoComprobante.Text = "FACTURAS A" Then
            If txtSubtotal.Text <> "" And CDbl(txtSubtotal.Text) > 0 Then
                If CDbl(txtMontoIVA10.Text) = "0" Or CDbl(txtMontoIVA10.Text) = "0.00" Then
                    If CDbl(txtMontoIVA21.Text) = "0" Or CDbl(txtMontoIVA21.Text) = "0.00" Then
                        If CDbl(txtMontoIVA27.Text) = "0" Or CDbl(txtMontoIVA27.Text) = "0.00" Then
                            Util.MsgStatus(Status1, "Debe ingresar el monto del IVA", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Debe ingresar el monto del IVA", My.Resources.Resources.stop_error.ToBitmap, True)
                            txtMontoIVA21.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            End If

            'End If

            'If txtCantIVA.Value >= 1 And (txtSubtotal.Text <> "" And CDbl(txtSubtotal.Text) > 0) Then

            If txtCantIVA.Value = 1 Then
                If CDbl(txtMontoIVA21.Text) > 0 And (CDbl(txtMontoIVA10.Text > 0) Or CDbl(txtMontoIVA27.Text) > 0) Then
                    Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                    txtMontoIVA21.Focus()
                    Exit Sub
                Else
                    If CDbl(txtMontoIVA10.Text) > 0 And (CDbl(txtMontoIVA21.Text > 0) Or CDbl(txtMontoIVA27.Text) > 0) Then
                        Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtMontoIVA10.Focus()
                        Exit Sub
                    Else
                        If CDbl(txtMontoIVA27.Text) > 0 And (CDbl(txtMontoIVA10.Text > 0) Or CDbl(txtMontoIVA21.Text) > 0) Then
                            Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                            txtMontoIVA27.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If txtCantIVA.Value = 2 Then
                If CDbl(txtMontoIVA21.Text) = 0 And CDbl(txtMontoIVA27.Text) = 0 Then
                    Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                    txtMontoIVA21.Focus()
                    Exit Sub
                Else
                    If CDbl(txtMontoIVA21.Text) = 0 And CDbl(txtMontoIVA10.Text) = 0 Then
                        Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtMontoIVA21.Focus()
                        Exit Sub
                    Else
                        If CDbl(txtMontoIVA10.Text) = 0 And CDbl(txtMontoIVA27.Text) = 0 Then
                            Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                            txtMontoIVA21.Focus()
                            Exit Sub
                        Else
                            If CDbl(txtMontoIVA21.Text) > 0 And CDbl(txtMontoIVA10.Text) > 0 And CDbl(txtMontoIVA27.Text) > 0 Then
                                Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                                txtMontoIVA21.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If txtCantIVA.Value = 3 Then
                If CDbl(txtMontoIVA21.Text) > 0 And (CDbl(txtMontoIVA10.Text = 0) Or CDbl(txtMontoIVA27.Text) = 0) Then
                    Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                    txtMontoIVA21.Focus()
                    Exit Sub
                Else
                    If CDbl(txtMontoIVA10.Text) > 0 And (CDbl(txtMontoIVA21.Text = 0) Or CDbl(txtMontoIVA27.Text) = 0) Then
                        Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtMontoIVA10.Focus()
                        Exit Sub
                    Else
                        If CDbl(txtMontoIVA27.Text) > 0 And (CDbl(txtMontoIVA10.Text = 0) Or CDbl(txtMontoIVA21.Text) = 0) Then
                            Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "El campo Cant IVA no coincide con la cantidad de alícuotas ingresadas.", My.Resources.Resources.stop_error.ToBitmap, True)
                            txtMontoIVA27.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            End If

            'If Format(CDbl(txtMontoIVA10.Text), "###0.00") > 0 Then
            '    If (txtMontoIVA10.Text - Format(CDbl(txtSubtotal.Text) / 0.105, "###0.00")) > 0.03 Or (txtMontoIVA10.Text - Format(CDbl(txtSubtotal.Text) / 0.105, "###0.00")) < -0.03 Then
            '        Util.MsgStatus(Status1, "El monto IVA del 10,5% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
            '        Util.MsgStatus(Status1, "El monto IVA del 10,5% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
            '        txtMontoIVA10.Focus()
            '        Exit Sub
            '    End If
            'End If

            'If Format(CDbl(txtMontoIVA21.Text), "###0.00") > 0 Then
            '    If (txtMontoIVA21.Text - Format(CDbl(txtSubtotal.Text) / 0.21, "###0.00")) > 0.05 Or (txtMontoIVA21.Text - Format(CDbl(txtSubtotal.Text) / 0.21, "###0.00")) < -0.05 Then
            '        Util.MsgStatus(Status1, "El monto IVA del 21% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
            '        Util.MsgStatus(Status1, "El monto IVA del 21% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
            '        txtMontoIVA21.Focus()
            '        Exit Sub
            '    End If
            'End If

            'If Format(CDbl(txtMontoIVA27.Text), "###0.00") > 0 Then
            '    If (txtMontoIVA27.Text - Format(CDbl(txtSubtotal.Text) / 0.27, "###0.00")) > 0.03 Or (txtMontoIVA27.Text - Format(CDbl(txtSubtotal.Text) / 0.27, "###0.00")) < -0.05 Then
            '        Util.MsgStatus(Status1, "El monto IVA del 27% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 5 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
            '        Util.MsgStatus(Status1, "El monto IVA del 27% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 5 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
            '        txtMontoIVA27.Focus()
            '        Exit Sub
            '    End If
            'End If
        End If


        Dim a As Double, b As Double, c As Double, d As Double

        a = Format(CDbl(txtMontoIVA10.Text) / 0.105, "###0.00")
        b = Format(CDbl(txtMontoIVA21.Text) / 0.21, "###0.00")
        c = Format(CDbl(txtMontoIVA27.Text) / 0.27, "###0.00")

        d = a + b + c

        d = Format(d - CDbl(txtSubtotal.Text), "###0.00")

        If d > 0.05 Then
            Util.MsgStatus(Status1, "El o los montos imponibles difieren de las alícuotas ingresadas. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El o los montos imponibles difieren de las alícuotas ingresadas. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
            txtMontoIVA10.Focus()
            Exit Sub
        End If

        If txtTipoMoneda.Text = "Do" And CDbl(txtValorCambio.Text) < BuscarDiferenciaCambio(ConnStringSEI) Then
            If MessageBox.Show("El valor del cambio ingresado está por debajo del valor de cambio estipulado por sistema, considerando un porcentaje de variación. ¿Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro(ControlFactura, ControlRemito)
                'If bolModo = True Then
                Select Case res
                    Case -20
                        Util.MsgStatus(Status1, "El nuevo total ingresado para el gasto es menor que el cargado originalmente. " & vbCrLf & "Debe anular el pago asociado para luego modificar este Gasto.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El nuevo total ingresado para el gasto es menor que el cargado originalmente. " & vbCrLf & "Debe anular el pago asociado para luego modificar este Gasto.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                        Exit Sub
                    Case -4
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El número de remito ingresado ya existe para el cliente actual.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El número de remito ingresado ya existe para el cliente actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case -8
                        Util.MsgStatus(Status1, "Ya existe un movimiento que incluye el nro de factura, tipo y cliente que desea ingresar ahora.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "Ya existe un movimiento que incluye el nro de factura, tipo y cliente que desea ingresar ahora.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                        Exit Sub
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo agregar el registro. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                        Exit Sub
                    Case 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                        Exit Sub
                    Case Else
                        res = AgregarActualizar_Registro_Items_IVA()
                        Select Case res
                            Case -20
                                Util.MsgStatus(Status1, "El nuevo total ingresado para el gasto es menor que el cargado originalmente. " & vbCrLf & "Debe anular el pago asociado para luego modificar este Gasto.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "El nuevo total ingresado para el gasto es menor que el cargado originalmente. " & vbCrLf & "Debe anular el pago asociado para luego modificar este Gasto.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                                Exit Sub
                            Case -1
                                Util.MsgStatus(Status1, "No se pudo agregar el registro. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro. Error en la transacción.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                                Exit Sub
                            Case 0
                                Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                                Exit Sub
                            Case Else
                                res = AgregarRegistro_Impuestos()
                                Select Case res
                                    Case Is <= 0
                                        Util.MsgStatus(Status1, "No se pudieron actualizar los registros de los Impuestos.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudieron actualizar los registros de los Impuestos.", My.Resources.Resources.stop_error.ToBitmap, True)
                                        Cancelar_Tran()
                                        Exit Sub
                                    Case Else
                                        Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                End Select

                                If cmbTipoComprobante.Text.Contains("CREDITO") And bolModo = True Then
                                    res = AgregarRegistro_NotasCredito()
                                    Select Case res
                                        Case Is <= 0
                                            Util.MsgStatus(Status1, "No se pudieron actualizar los registros para la Nota de Crédito Actual.", My.Resources.Resources.stop_error.ToBitmap)
                                            Util.MsgStatus(Status1, "No se pudieron actualizar los registros para la Nota de Crédito Actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                                            Cancelar_Tran()
                                            Exit Sub
                                        Case Else
                                            Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                    End Select
                                End If

                                '*************** PARTE DE PAGO
                                'OPCION 1 NO CANCELADA NO CANCELADA NO ENTRA NUNCA A PAGOS
                                'OPCION 2 nuevo CANCELADA, anterior NO CANCELADA

                                If bolModo = False Then
                                    If chkFacturaCancelada.Checked = True And grd.CurrentRow.Cells(19).Value = False Then
                                        bolModo = True
                                        GoTo InsertarPago
                                    Else
                                        'OPCION 3 nuevo NO CANCELADA, anterior CANCELADA
                                        If chkFacturaCancelada.Checked = False And grd.CurrentRow.Cells(19).Value = True Then
                                            If MessageBox.Show("El gasto ingresado figura como cancelado en el sistema. ¿Está seguro que eliminar este pago?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                                res = Eliminar_Pago()
                                                If res = 1 Then
                                                    MsgBox("Se actualizo correctamente el Gasto y se eliminó el pago correspondiente.", MsgBoxStyle.Information, "Información")
                                                    Util.MsgStatus(Status1, "Se actualizo correctamente el Gasto y se eliminó el pago correspondiente.", My.Resources.Resources.ok.ToBitmap)
                                                    Cerrar_Tran()
                                                    bolModo = False
                                                    PrepararBotones()

                                                    SQL = "exec spGastos_Select_All @Eliminado = 0"

                                                    btnActualizar_Click(sender, e)
                                                    btnCancelar_Click(sender, e)
                                                    Exit Sub
                                                Else
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo eliminar el Pago del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                    Util.MsgStatus(Status1, "No se pudo eliminar el Pago del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Exit Sub
                                                End If
                                            End If
                                        Else
                                            If chkFacturaCancelada.Checked = True And grd.CurrentRow.Cells(19).Value = True Then
                                                'OPCION 4 nuevo CANCELADA, anterior CANCELADA entonces se modifica
                                                GoTo InsertarPago
                                            Else
                                                GoTo ConfirmarModificacion
                                            End If
                                        End If
                                    End If
                                Else
                                    If chkFacturaCancelada.Checked = True Then
InsertarPago:
                                        res = AgregarActualizar_Pago()
                                        bolModo = False
                                        Select Case res
                                            Case -1
                                                Cancelar_Tran()
                                                Util.MsgStatus(Status1, "No se pudo registrar el Pago del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                Util.MsgStatus(Status1, "No se pudo registrar el Pago del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                Exit Sub
                                            Case 0
                                                Cancelar_Tran()
                                                Util.MsgStatus(Status1, "No se pudo registrar el Pago del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                Util.MsgStatus(Status1, "No se pudo registrar el Pago del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                Exit Sub
                                            Case Else
                                                Util.MsgStatus(Status1, "Guardando el detalle del pago...", My.Resources.Resources.indicator_white)
                                                res = AgregarRegistro_PagosGastos()
                                                Select Case res
                                                    Case -3
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No se pudo realizar la inserción del Pago (Detalle). Actualización en la tabla Gastos", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No se pudo realizar la inserción del Pago (Detalle). Actualización en la tabla Gastos", My.Resources.Resources.stop_error.ToBitmap, True)
                                                        Exit Sub
                                                    Case -2
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No pudo registrar la inserción del Pago (Detalle).", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No pudo registrar la inserción del Pago (Detalle).", My.Resources.Resources.stop_error.ToBitmap, True)
                                                        Exit Sub
                                                    Case -1
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "Error Desconocido.", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "Error Desconocido.", My.Resources.Resources.stop_error.ToBitmap, True)
                                                        Exit Sub
                                                    Case 0
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No pudo registrar la inserción del Pago (Detalle).", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No pudo registrar la inserción del Pago (Detalle).", My.Resources.Resources.stop_error.ToBitmap, True)
                                                        Exit Sub
                                                    Case Else
                                                        Util.MsgStatus(Status1, "Se registro correctamente el Pago del Gasto", My.Resources.Resources.ok.ToBitmap)
                                                        Cerrar_Tran()
                                                        bolModo = False
                                                        PrepararBotones()

                                                        SQL = "exec spGastos_Select_All @Eliminado = 0"

                                                        btnActualizar_Click(sender, e)
                                                        btnCancelar_Click(sender, e)
                                                        Exit Sub
                                                End Select
                                        End Select
                                    Else
ConfirmarModificacion:
                                        Util.MsgStatus(Status1, "Se ha insertado correctamente el registro.", My.Resources.Resources.ok.ToBitmap)
                                        Cerrar_Tran()
                                        bolModo = False
                                        PrepararBotones()

                                        SQL = "exec spGastos_Select_All @Eliminado = 0"

                                        btnActualizar_Click(sender, e)
                                        btnCancelar_Click(sender, e)
                                        Exit Sub
                                    End If
                                End If
                        End Select
                End Select
            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        If MessageBox.Show("Esta acción anulará el Gasto." + vbCrLf + "Si existen movimientos de Pagos vinculados y no fueron anulados, el sistema no lo dejara continuar hasta anular el movimiento indicado." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro_Gasto()
            Select Case res
                Case -8
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "El gasto está asociado a un Pago. Anule el pago al proveedor para luego anular el gasto.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El gasto está asociado a un Pago. Anule el pago al proveedor para luego anular el gasto.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case Else
                    If txtIdRecepcion.Text <> "" Then
                        If MessageBox.Show("Esta acción reversará las Recepciones de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            res = EliminarRegistro_Recepcion()
                            Select Case res
                                Case -3
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -2
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -1
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case 0
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                            End Select
                        Else
                            Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                            Exit Sub
                        End If
                    End If

                    Cerrar_Tran()
                    bolModo = False
                    PrepararBotones()

                    SQL = "exec spGastos_Select_All @Eliminado = 0"

                    btnActualizar_Click(sender, e)
                    Util.MsgStatus(Status1, "Se anuló correctamente el movimiento de Gasto.", My.Resources.Resources.ok.ToBitmap)

            End Select
            '
            ' cerrar la conexion si está abierta.
            '
            If Not conn_del_form Is Nothing Then
                CType(conn_del_form, IDisposable).Dispose()
            End If
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Proveedor As String, TipoGasto As String, Cancelado As String

        Dim consulta As String = "select nombre from Proveedores where eliminado = 0 order by nombre asc"
        'Dim consulta2 = "select '' as codigo union select 'SI' as codigo union select 'NO' as codigo"
        Dim consulta2 As String = "select nombre from TipoGastos where eliminado = 0 order by nombre asc"
        Dim consulta3 = "select '' as codigo union select 'SI' as codigo union select 'NO' as codigo"


        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("TipoGasto :", "STRING", "", False, "", consulta2, Cnn)
        paramreporte.AgregarParametros("Proveedor :", "STRING", "", False, "", consulta, Cnn)
        paramreporte.AgregarParametros("Cancelados:", "STRING", "", False, "", consulta3, Cnn)


        paramreporte.ShowDialog()

        nbreformreportes = "Detalle de Gastos"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            TipoGasto = paramreporte.ObtenerParametros(2).ToString
            Proveedor = paramreporte.ObtenerParametros(3).ToString
            Cancelado = paramreporte.ObtenerParametros(4).ToString

            rpt.Gastos_App(Inicial, Final, Proveedor, TipoGasto, Cancelado, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        grd_CurrentCellChanged(sender, e)

        'LlenarGrid_IVA(CType(txtID.Text, Long))

        'LlenarGrid_Impuestos()

    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Administración de Gastos"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 60)
        'Me.grd.Size = New Size(p)

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

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        If dtpFECHA.Value.Date > Date.Today Then
            MsgBox("La fecha de pago no puede ser mayor a la fecha actual.", MsgBoxStyle.Critical, "Control de Ingresos")
            Util.MsgStatus(Status1, "La fecha de pago no puede ser mayor a la fecha actual.", My.Resources.Resources.alert.ToBitmap)
            dtpFECHA.Focus()
            Exit Sub
        End If

        bolpoliticas = True

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCodigo.Tag = "1"
        dtpFECHA.Tag = "2"
        txtIdProveedor.Tag = "3"
        cmbProveedor.Tag = "4"
        txtIdTipoGasto.Tag = "5"
        cmbTipoGasto.Tag = "6"
        txtIdComprobante.Tag = "7"
        cmbTipoComprobante.Tag = "8"
        txtPtoVta.Tag = "9"
        txtNroFacturaCompleto.Tag = "10"
        txtNroFacturaCompletoControl.Tag = "10"
        txtTipoMoneda.Tag = "11"
        txtSubtotalExento.Tag = "12"
        txtSubtotal.Tag = "13"
        lblMontoIva.Tag = "14"
        lblImpuestos.Tag = "15"
        lblTotal.Tag = "16"
        txtNota.Tag = "18"
        chkCancelado.Tag = "19"
        chkFacturaCancelada.Tag = "19"
        lblNroRemito.Tag = "20"
        txtNroRemitoCompleto.Tag = "20"
        txtNroRemitoControl.Tag = "20"
        txtNroFactura.Tag = "21"
        cmbRecepcion.Tag = "22"
        txtidpago.Tag = "23"
        chkPeriodo.Tag = "24"
        dtpPeriodo.Tag = "25"
        txtIdRecepcion.Tag = "28"
        txtValorCambio.Tag = "30"
        txtPtoVtaRemito.Tag = "31"
        txtNroCompRemito.Tag = "32"
        txtIdMoneda.Tag = "33"
        txtCantIVA.Tag = "34"
        chkRecepcion.Tag = "35"
    End Sub

    'Private Sub LlenarComboProveedores(ByVal cmb As System.Windows.Forms.ComboBox) ', ByVal flete As Boolean)
    '    Dim ds_Proveedores As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    llenandoCombo = True

    '    Try
    '        'If flete = True Then
    '        ' ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre as codigo FROM Proveedores WHERE flete = 1 and Eliminado = 0 ORDER BY nombre asc")
    '        'Else
    '        ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre as codigo FROM Proveedores WHERE Eliminado = 0 ORDER BY nombre asc")
    '        'End If

    '        ds_Proveedores.Dispose()

    '        With cmb
    '            .DataSource = ds_Proveedores.Tables(0).DefaultView
    '            .DisplayMember = "codigo"
    '            .ValueMember = "id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
    '            .TabStop = True
    '        End With

    '    Catch ex As Exception
    '        Dim errMessage As String = ""
    '        Dim tempException As Exception = ex

    '        llenandoCombo = False

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

    '    llenandoCombo = False

    'End Sub

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

    Private Sub LlenarComboRecepciones(ByVal cmb As System.Windows.Forms.ComboBox)
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

            ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, ('Mov Nro: ' + codigo + ' - OC: ' + Nrooc) AS Codigo FROM Recepciones WHERE PendienteGasto = 1 AND Eliminado = 0 and IdProveedor = " & cmbProveedor.SelectedValue & " ORDER BY id asc") ' AND id NOT IN (select idrecepcion FROM gastos where idrecepcion is not null) ORDER BY id asc")
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

            If ds_Proveedores.Tables(0).Rows.Count > 0 Then
                chkRecepcion.Enabled = True
                chkRecepcion.Checked = True
            Else
                chkRecepcion.Enabled = False
                chkRecepcion.Checked = False
                lblNroRemito.Text = ""
                cmbRecepcion.Text = ""
            End If

            If bolModo = False Then
                cmbRecepcion.Text = grd.CurrentRow.Cells(22).Value
            End If

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

    Private Sub LlenarComboGastos(ByVal cmb As System.Windows.Forms.ComboBox)
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
            ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, (codigo + ' - ' + Nombre) as codigo FROM TipoGastos WHERE Eliminado = 0 ORDER BY codigo asc")
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

    Private Sub BuscarRemito()
        Dim ds_OCReq As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_OCReq = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ISNULL(NroRemito, '') a, ISNULL(nrocomprobanteremito, '0') AS NroCompRemito, ISNULL(ptovtaremito, '0') AS PtovtaRemito FROM Recepciones WHERE id = " & IIf(cmbRecepcion.SelectedValue Is Nothing, 0, cmbRecepcion.SelectedValue))

            ds_OCReq.Dispose()

            If ds_OCReq.Tables(0).Rows.Count > 0 Then
                lblNroRemito.Text = ds_OCReq.Tables(0).Rows(0)(0)
                txtNroRemitoCompleto.Text = ds_OCReq.Tables(0).Rows(0)(0)
                txtNroCompRemito.Text = ds_OCReq.Tables(0).Rows(0)(1)
                txtPtoVtaRemito.Text = ds_OCReq.Tables(0).Rows(0)(2)
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

    Private Sub LlenarGrid_IVA(ByVal Id As Long)

        Dim ds_IVAs As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_IVAs = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT gd.PorcIva, gd.MontoIva " & _
                                                                            " FROM Gastos g JOIN Gastos_det gd ON gd.idgasto = g.id WHERE IdGasto = " & Id)

            ds_IVAs.Dispose()

            Dim i As Integer
            Dim valor As Double

            For i = 0 To ds_IVAs.Tables(0).Rows.Count - 1
                valor = ds_IVAs.Tables(0).Rows(i)(1)
                If CDbl(ds_IVAs.Tables(0).Rows(i)(0)) < 11 Then
                    txtMontoIVA10.Text = valor
                Else
                    If CDbl(ds_IVAs.Tables(0).Rows(i)(0)) = 21 Then
                        txtMontoIVA21.Text = valor
                    Else
                        If CDbl(ds_IVAs.Tables(0).Rows(i)(0)) > 21 Then
                            txtMontoIVA27.Text = valor
                        End If
                    End If
                End If
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
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub LlenarGrid_Impuestos()

        If bolModo = False Then
            If grd.CurrentRow.Cells(10).Value = "0.00" Then
                SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtID.Text = "", 0, txtID.Text) & ", @Bolmodo = 1" '& bolModo
            Else
                SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtID.Text = "", 0, txtID.Text) & ", @Bolmodo = 0" '& bolModo
            End If
        Else
            SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtID.Text = "", 0, txtID.Text) & ", @Bolmodo = " & bolModo
        End If

        'SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtID.Text = "", 0, txtID.Text) & ", @Bolmodo = " & bolModo

        GetDatasetItems(grdImpuestos)

        grdImpuestos.Columns(ColumnasDelGridImpuestos.Id).Visible = False

        grdImpuestos.Columns(ColumnasDelGridImpuestos.codigo).ReadOnly = True
        grdImpuestos.Columns(ColumnasDelGridImpuestos.codigo).Width = 155

        grdImpuestos.Columns(ColumnasDelGridImpuestos.NroDocumento).Width = 85

        grdImpuestos.Columns(ColumnasDelGridImpuestos.Monto).Width = 60
        grdImpuestos.Columns(ColumnasDelGridImpuestos.Monto).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdImpuestos.Columns(ColumnasDelGridImpuestos.IdIngreso).Visible = False

        grdImpuestos.Columns(ColumnasDelGridImpuestos.IdImpuestoxGasto).Visible = False

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
            .Font = New Font("TAHOMA", 7, FontStyle.Bold)
        End With

        grdImpuestos.Font = New Font("TAHOMA", 7, FontStyle.Regular)

        SQL = "exec spGastos_Select_All @Eliminado = 0"

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

    Private Sub CalcularMontoIVA()
        If band = 1 Then
            If txtMontoIVA21.Text = "" Then txtMontoIVA21.Text = "0"
            If txtMontoIVA10.Text = "" Then txtMontoIVA10.Text = "0"
            If txtMontoIVA27.Text = "" Then txtMontoIVA27.Text = "0"
            If txtSubtotal.Text = "" Then txtSubtotal.Text = "0"
            If txtSubtotalExento.Text = "" Then txtSubtotalExento.Text = "0"
            lblMontoIva.Text = Format(CDbl(txtMontoIVA10.Text) + CDbl(txtMontoIVA21.Text) + CDbl(txtMontoIVA27.Text), "###0.00")
            lblTotal.Text = Format(CDbl(txtSubtotalExento.Text) + CDbl(txtSubtotal.Text) + CDbl(lblMontoIva.Text) + CDbl(lblImpuestos.Text), "###0.00")
        End If
    End Sub

    Private Sub BuscarMoneda_OC(ByVal ConOC As Boolean)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim dsMoneda As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            If ConOC = True Then
                dsMoneda = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT mo.Codigo, o.ValorCambio, o.idmoneda FROM OrdenDeCompra o JOIN Monedas mo ON mo.id = o.IdMoneda JOIN Recepciones r ON r.idoc = o.id WHERE r.id = " & txtIdRecepcion.Text)
            Else
                dsMoneda = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, ValorCambio, id FROM Monedas WHERE Codigo = 'Do'")
            End If

            dsMoneda.Dispose()

            txtTipoMoneda.Text = dsMoneda.Tables(0).Rows(0).Item(0)
            txtValorCambio.Text = dsMoneda.Tables(0).Rows(0).Item(1)
            txtIdMoneda.Text = dsMoneda.Tables(0).Rows(0).Item(2)

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

    Private Sub LlenarLista_Facturas()
        Dim ds_FacturasPendientes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            If bolModo = True Then
                ds_FacturasPendientes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select codigo, nrofactura from gastos where ComprobanteTipo not in ('003', '008', '013') and idproveedor = " & IIf(txtIdProveedor.Text = "", 0, txtIdProveedor.Text) & " ORDER BY nrofactura")
            Else
                ds_FacturasPendientes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select Codigo, NroFactura from gastos_notascredito gnc JOIN gastos g ON g.id = idgastoanulado WHERE idgasto = " & grd.CurrentRow.Cells(0).Value)
            End If
            ds_FacturasPendientes.Dispose()

            Dim i As Integer ', j As Integer

            lstFacturasPendientes.Items.Clear()
            Dim item As New ListViewItem

            Try
                For i = 0 To ds_FacturasPendientes.Tables(0).Rows.Count - 1
                    Dim r As DataRow = ds_FacturasPendientes.Tables(0).Rows(i)
                    item = lstFacturasPendientes.Items.Add(CStr(r("codigo")))
                    item.SubItems.Add(CStr(r("nrofactura")))
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
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

    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro(ByVal ControlFactura As Boolean, ByVal ControlRemito As Boolean) As Integer
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
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput
                Else
                    param_id.Value = txtID.Text ' grd.CurrentRow.Cells(0).Value
                    param_id.Direction = ParameterDirection.Input
                End If

                Dim param_idrecepcion As New SqlClient.SqlParameter
                param_idrecepcion.ParameterName = "@idrecepcion"
                param_idrecepcion.SqlDbType = SqlDbType.BigInt
                If chkRecepcion.Checked = True And txtIdRecepcion.Text <> "" Then
                    param_idrecepcion.Value = txtIdRecepcion.Text 'cmbRecepcion.SelectedValue
                Else
                    param_idrecepcion.Value = DBNull.Value
                End If
                param_idrecepcion.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_codigo.Value = DBNull.Value
                    param_codigo.Direction = ParameterDirection.InputOutput
                Else
                    param_codigo.Value = txtCodigo.Text
                    param_codigo.Direction = ParameterDirection.Input
                End If

                Dim param_fechagasto As New SqlClient.SqlParameter
                param_fechagasto.ParameterName = "@fechagasto"
                param_fechagasto.SqlDbType = SqlDbType.DateTime
                param_fechagasto.Value = dtpFECHA.Value
                param_fechagasto.Direction = ParameterDirection.Input

                Dim param_tipogasto As New SqlClient.SqlParameter
                param_tipogasto.ParameterName = "@tipogasto"
                param_tipogasto.SqlDbType = SqlDbType.BigInt
                param_tipogasto.Value = cmbTipoGasto.SelectedValue
                param_tipogasto.Direction = ParameterDirection.Input

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@idproveedor"
                param_proveedor.SqlDbType = SqlDbType.BigInt
                param_proveedor.Value = txtIdProveedor.Text ' IIf(cmbProveedor.SelectedValue = Nothing, txtIdProveedor.Text, cmbProveedor.SelectedValue)
                param_proveedor.Direction = ParameterDirection.Input

                Dim param_IdMoneda As New SqlClient.SqlParameter
                param_IdMoneda.ParameterName = "@idmoneda"
                param_IdMoneda.SqlDbType = SqlDbType.BigInt
                param_IdMoneda.Value = txtIdMoneda.Text
                param_IdMoneda.Direction = ParameterDirection.Input

                Dim param_valorCambio As New SqlClient.SqlParameter
                param_valorCambio.ParameterName = "@ValorCambio"
                param_valorCambio.SqlDbType = SqlDbType.Decimal
                param_valorCambio.Precision = 18
                param_valorCambio.Scale = 3
                param_valorCambio.Value = txtValorCambio.Text
                param_valorCambio.Direction = ParameterDirection.Input

                Dim param_tipofactura As New SqlClient.SqlParameter
                param_tipofactura.ParameterName = "@comprobantetipo"
                param_tipofactura.SqlDbType = SqlDbType.VarChar
                param_tipofactura.Size = 5
                param_tipofactura.Value = cmbTipoComprobante.SelectedValue
                param_tipofactura.Direction = ParameterDirection.Input

                Dim param_nrofactura As New SqlClient.SqlParameter
                param_nrofactura.ParameterName = "@nrofactura"
                param_nrofactura.SqlDbType = SqlDbType.VarChar
                param_nrofactura.Size = 20
                param_nrofactura.Value = LTrim(RTrim(txtNroFacturaCompleto.Text))
                param_nrofactura.Direction = ParameterDirection.Input

                Dim param_remito As New SqlClient.SqlParameter
                param_remito.ParameterName = "@nroremito"
                param_remito.SqlDbType = SqlDbType.VarChar
                param_remito.Size = 20
                If chkRecepcion.Checked = False Then
                    param_remito.Value = LTrim(RTrim(txtNroRemitoCompleto.Text))
                Else
                    param_remito.Value = lblNroRemito.Text
                End If
                param_remito.Direction = ParameterDirection.Input

                Dim param_CantIVA As New SqlClient.SqlParameter
                param_CantIVA.ParameterName = "@cantIVA"
                param_CantIVA.SqlDbType = SqlDbType.SmallInt
                param_CantIVA.Value = txtCantIVA.Value
                param_CantIVA.Direction = ParameterDirection.Input

                Dim param_ptovta As New SqlClient.SqlParameter
                param_ptovta.ParameterName = "@ptovta"
                param_ptovta.SqlDbType = SqlDbType.VarChar
                param_ptovta.Size = 4
                param_ptovta.Value = LTrim(RTrim(txtPtoVta.Text))
                param_ptovta.Direction = ParameterDirection.Input

                Dim param_nrocompr As New SqlClient.SqlParameter
                param_nrocompr.ParameterName = "@nrocompr"
                param_nrocompr.SqlDbType = SqlDbType.VarChar
                param_nrocompr.Size = 8
                param_nrocompr.Value = LTrim(RTrim(txtNroFactura.Text))
                param_nrocompr.Direction = ParameterDirection.Input

                Dim param_MontoIVA As New SqlClient.SqlParameter
                param_MontoIVA.ParameterName = "@MontoIVA"
                param_MontoIVA.SqlDbType = SqlDbType.Decimal
                param_MontoIVA.Size = 18
                param_MontoIVA.Value = IIf(lblMontoIva.Text = "", 0, lblMontoIva.Text)
                param_MontoIVA.Direction = ParameterDirection.Input

                Dim param_Subtotal As New SqlClient.SqlParameter
                param_Subtotal.ParameterName = "@Subtotal"
                param_Subtotal.SqlDbType = SqlDbType.Decimal
                param_Subtotal.Size = 18
                param_Subtotal.Value = IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)
                param_Subtotal.Direction = ParameterDirection.Input

                Dim param_SubtotalExento As New SqlClient.SqlParameter
                param_SubtotalExento.ParameterName = "@SubtotalExento"
                param_SubtotalExento.SqlDbType = SqlDbType.Decimal
                param_SubtotalExento.Size = 18
                param_SubtotalExento.Value = IIf(txtSubtotalExento.Text = "", 0, txtSubtotalExento.Text)
                param_SubtotalExento.Direction = ParameterDirection.Input

                Dim param_Total As New SqlClient.SqlParameter
                param_Total.ParameterName = "@Total"
                param_Total.SqlDbType = SqlDbType.Decimal
                param_Total.Size = 18
                param_Total.Value = IIf(lblTotal.Text = "", 0, lblTotal.Text)
                param_Total.Direction = ParameterDirection.Input

                Dim param_totalPesos As New SqlClient.SqlParameter
                param_totalPesos.ParameterName = "@TotalPesos"
                param_totalPesos.SqlDbType = SqlDbType.Decimal
                param_totalPesos.Precision = 18
                param_totalPesos.Scale = 2
                param_totalPesos.Value = lblTotal.Text
                param_totalPesos.Direction = ParameterDirection.Input

                Dim param_deuda As New SqlClient.SqlParameter
                param_deuda.ParameterName = "@deuda"
                param_deuda.SqlDbType = SqlDbType.Decimal
                param_deuda.Precision = 18
                param_deuda.Scale = 2
                param_deuda.Value = IIf(chkFacturaCancelada.Checked = False, lblTotal.Text, 0)
                param_deuda.Direction = ParameterDirection.Input

                Dim param_cancelada As New SqlClient.SqlParameter
                param_cancelada.ParameterName = "@Cancelado"
                param_cancelada.SqlDbType = SqlDbType.Bit
                If cmbTipoComprobante.Text.Contains("CREDITO") Then
                    param_cancelada.Value = True
                Else
                    param_cancelada.Value = chkFacturaCancelada.Checked
                End If
                param_cancelada.Direction = ParameterDirection.Input

                Dim param_Impuestos As New SqlClient.SqlParameter
                param_Impuestos.ParameterName = "@Impuestos"
                param_Impuestos.SqlDbType = SqlDbType.Decimal
                param_Impuestos.Precision = 18
                param_Impuestos.Scale = 2
                param_Impuestos.Value = IIf(lblImpuestos.Text = "", 0, lblImpuestos.Text)
                param_Impuestos.Direction = ParameterDirection.Input

                Dim param_descripcion As New SqlClient.SqlParameter
                param_descripcion.ParameterName = "@descripcion"
                param_descripcion.SqlDbType = SqlDbType.VarChar
                param_descripcion.Size = 200
                param_descripcion.Value = txtNota.Text
                param_descripcion.Direction = ParameterDirection.Input

                Dim param_imputarotroperiodo As New SqlClient.SqlParameter
                param_imputarotroperiodo.ParameterName = "@ImputaraOtroPeriodo"
                param_imputarotroperiodo.SqlDbType = SqlDbType.Bit
                param_imputarotroperiodo.Value = chkPeriodo.Checked
                param_imputarotroperiodo.Direction = ParameterDirection.Input

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodoimputacion"
                param_periodo.SqlDbType = SqlDbType.DateTime
                param_periodo.Value = IIf(chkPeriodo.Checked = True, dtpPeriodo.Value, DBNull.Value)
                param_periodo.Direction = ParameterDirection.Input

                Dim param_ControlRemito As New SqlClient.SqlParameter
                param_ControlRemito.ParameterName = "@ControlRemito"
                param_ControlRemito.SqlDbType = SqlDbType.Bit
                param_ControlRemito.Value = ControlRemito
                param_ControlRemito.Direction = ParameterDirection.Input

                Dim param_ControlFactura As New SqlClient.SqlParameter
                param_ControlFactura.ParameterName = "@ControlFactura"
                param_ControlFactura.SqlDbType = SqlDbType.Bit
                param_ControlFactura.Value = ControlFactura
                param_ControlFactura.Direction = ParameterDirection.Input

                Dim param_ptovtaRemito As New SqlClient.SqlParameter
                param_ptovtaRemito.ParameterName = "@PtoVtaRemito"
                param_ptovtaRemito.SqlDbType = SqlDbType.VarChar
                param_ptovtaRemito.Size = 4
                param_ptovtaRemito.Value = LTrim(RTrim(IIf(txtPtoVtaRemito.Text = "", "0", txtPtoVtaRemito.Text)))
                param_ptovtaRemito.Direction = ParameterDirection.Input

                Dim param_nrocomprRemito As New SqlClient.SqlParameter
                param_nrocomprRemito.ParameterName = "@NroComprRemito"
                param_nrocomprRemito.SqlDbType = SqlDbType.VarChar
                param_nrocomprRemito.Size = 8
                param_nrocomprRemito.Value = LTrim(RTrim(IIf(txtNroCompRemito.Text = "", "0", txtNroCompRemito.Text)))
                param_nrocomprRemito.Direction = ParameterDirection.Input

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
                                            param_id, param_idrecepcion, param_codigo, param_fechagasto, _
                                            param_tipogasto, param_proveedor, param_IdMoneda, param_valorCambio, param_tipofactura, _
                                            param_nrofactura, param_remito, param_CantIVA, param_MontoIVA, param_Subtotal, param_SubtotalExento, _
                                            param_Total, param_totalPesos, param_deuda, param_cancelada, param_descripcion, _
                                            param_Impuestos, param_imputarotroperiodo, param_periodo, _
                                            param_ptovta, param_nrocompr, param_ptovtaRemito, param_nrocomprRemito, param_useradd, param_res)

                        txtCodigo.Text = param_codigo.Value

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Update", _
                                            param_id, param_fechagasto, _
                                            param_tipogasto, param_proveedor, param_IdMoneda, param_valorCambio, param_tipofactura, _
                                            param_nrofactura, param_remito, param_CantIVA, param_MontoIVA, param_Subtotal, param_SubtotalExento, _
                                            param_Total, param_totalPesos, param_descripcion, _
                                            param_Impuestos, param_imputarotroperiodo, param_periodo, _
                                            param_ptovta, param_nrocompr, param_ptovtaRemito, param_nrocomprRemito, param_ControlFactura, param_ControlRemito, _
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
        End Try

    End Function

    Private Function AgregarActualizar_Registro_Items_IVA() As Integer
        Dim res As Integer = 0 ', res_del As Integer
        Dim i As Integer

        Try

            Try

                For i = 0 To 2 'grdIVA.RowCount - 2

                    Dim param_idGasto As New SqlClient.SqlParameter
                    param_idGasto.ParameterName = "@idgasto"
                    param_idGasto.SqlDbType = SqlDbType.BigInt
                    param_idGasto.Value = txtID.Text
                    param_idGasto.Direction = ParameterDirection.Input

                    Dim param_subtotal As New SqlClient.SqlParameter
                    param_subtotal.ParameterName = "@subtotal"
                    param_subtotal.SqlDbType = SqlDbType.Decimal
                    param_subtotal.Precision = 18
                    param_subtotal.Scale = 2
                    Select Case i
                        Case 0
                            param_subtotal.Value = CDbl(txtMontoIVA10.Text) / 0.105
                        Case 1
                            param_subtotal.Value = CDbl(txtMontoIVA21.Text) / 0.21
                        Case 2
                            param_subtotal.Value = CDbl(txtMontoIVA27.Text) / 0.27
                    End Select
                    param_subtotal.Direction = ParameterDirection.Input

                    Dim param_PorcIva As New SqlClient.SqlParameter
                    param_PorcIva.ParameterName = "@PorcIva"
                    param_PorcIva.SqlDbType = SqlDbType.Decimal
                    param_PorcIva.Precision = 18
                    param_PorcIva.Scale = 2
                    Select Case i
                        Case 0
                            param_PorcIva.Value = 10.5
                        Case 1
                            param_PorcIva.Value = 21
                        Case 2
                            param_PorcIva.Value = 27
                    End Select
                    param_PorcIva.Direction = ParameterDirection.Input

                    Dim param_MontoIva As New SqlClient.SqlParameter
                    param_MontoIva.ParameterName = "@MontoIva"
                    param_MontoIva.SqlDbType = SqlDbType.Decimal
                    param_MontoIva.Precision = 18
                    param_MontoIva.Scale = 2
                    Select Case i
                        Case 0
                            param_MontoIva.Value = CDbl(txtMontoIVA10.Text)
                        Case 1
                            param_MontoIva.Value = CDbl(txtMontoIVA21.Text)
                        Case 2
                            param_MontoIva.Value = CDbl(txtMontoIVA27.Text)
                    End Select
                    param_MontoIva.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Det_Insert", _
                                                 param_idGasto, param_subtotal, param_PorcIva, param_MontoIva, _
                                                 param_res)
                        Else
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Det_Update", _
                                                 param_idGasto, param_subtotal, param_PorcIva, param_MontoIva, _
                                                 param_res)
                        End If

                        res = param_res.Value

                        If res < 0 Then
                            Exit For
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                Next

                AgregarActualizar_Registro_Items_IVA = res

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

    Private Function EliminarItems_Gastos_Det() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_idGasto As New SqlClient.SqlParameter
            param_idGasto.ParameterName = "@idGasto"
            param_idGasto.SqlDbType = SqlDbType.BigInt
            param_idGasto.Value = CLng(txtID.Text)
            param_idGasto.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Det_Delete", _
                                        param_idGasto, param_res)

            EliminarItems_Gastos_Det = param_res.Value

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

    Private Function AgregarActualizar_Pago() As Integer
        Dim res As Integer = 0

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            If bolModo = True Then
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput
            Else
                param_id.Value = IIf(txtidpago.Text = "", DBNull.Value, txtidpago.Text)
                param_id.Direction = ParameterDirection.Input
            End If

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.InputOutput

            Dim param_idGasto As New SqlClient.SqlParameter
            param_idGasto.ParameterName = "@idgasto"
            param_idGasto.SqlDbType = SqlDbType.BigInt
            param_idGasto.Value = txtID.Text
            param_idGasto.Direction = ParameterDirection.Input

            Dim param_fecha As New SqlClient.SqlParameter
            param_fecha.ParameterName = "@fechaPago"
            param_fecha.SqlDbType = SqlDbType.DateTime
            param_fecha.Value = dtpFECHA.Value
            param_fecha.Direction = ParameterDirection.Input

            Dim param_idProveedor As New SqlClient.SqlParameter
            param_idProveedor.ParameterName = "@idProveedor"
            param_idProveedor.SqlDbType = SqlDbType.BigInt
            param_idProveedor.Value = cmbProveedor.SelectedValue
            param_idProveedor.Direction = ParameterDirection.Input

            Dim param_nota As New SqlClient.SqlParameter
            param_nota.ParameterName = "@Nota"
            param_nota.SqlDbType = SqlDbType.VarChar
            param_nota.Size = 300
            param_nota.Value = txtNota.Text
            param_nota.Direction = ParameterDirection.Input

            Dim param_contado As New SqlClient.SqlParameter
            param_contado.ParameterName = "@Contado"
            param_contado.SqlDbType = SqlDbType.Bit
            param_contado.Value = True
            param_contado.Direction = ParameterDirection.Input

            Dim param_montoContado As New SqlClient.SqlParameter
            param_montoContado.ParameterName = "@MontoContado"
            param_montoContado.SqlDbType = SqlDbType.Decimal
            param_montoContado.Precision = 18
            param_montoContado.Scale = 2
            param_montoContado.Value = IIf(lblTotal.Text = "", 0, lblTotal.Text)
            param_montoContado.Direction = ParameterDirection.Input

            Dim param_tarjeta As New SqlClient.SqlParameter
            param_tarjeta.ParameterName = "@tarjeta"
            param_tarjeta.SqlDbType = SqlDbType.Bit
            param_tarjeta.Value = False
            param_tarjeta.Direction = ParameterDirection.Input

            Dim param_nombretarjeta As New SqlClient.SqlParameter
            param_nombretarjeta.ParameterName = "@NombreTarjeta"
            param_nombretarjeta.SqlDbType = SqlDbType.VarChar
            param_nombretarjeta.Size = 50
            param_nombretarjeta.Value = ""
            param_nombretarjeta.Direction = ParameterDirection.Input

            Dim param_montotarjeta As New SqlClient.SqlParameter
            param_montotarjeta.ParameterName = "@montotarjeta"
            param_montotarjeta.SqlDbType = SqlDbType.Decimal
            param_montotarjeta.Precision = 18
            param_montotarjeta.Scale = 2
            param_montotarjeta.Value = 0
            param_montotarjeta.Direction = ParameterDirection.Input

            Dim param_cheque As New SqlClient.SqlParameter
            param_cheque.ParameterName = "@cheque"
            param_cheque.SqlDbType = SqlDbType.Bit
            param_cheque.Value = False
            param_cheque.Direction = ParameterDirection.Input

            Dim param_montocheque As New SqlClient.SqlParameter
            param_montocheque.ParameterName = "@montocheque"
            param_montocheque.SqlDbType = SqlDbType.Decimal
            param_montocheque.Precision = 18
            param_montocheque.Scale = 2
            param_montocheque.Value = 0
            param_montocheque.Direction = ParameterDirection.Input

            Dim param_montochequepropio As New SqlClient.SqlParameter
            param_montochequepropio.ParameterName = "@montochequepropio"
            param_montochequepropio.SqlDbType = SqlDbType.Decimal
            param_montochequepropio.Precision = 18
            param_montochequepropio.Scale = 2
            param_montochequepropio.Value = 0
            param_montochequepropio.Direction = ParameterDirection.Input

            Dim param_transferencia As New SqlClient.SqlParameter
            param_transferencia.ParameterName = "@transferencia"
            param_transferencia.SqlDbType = SqlDbType.Bit
            param_transferencia.Value = False
            param_transferencia.Direction = ParameterDirection.Input

            Dim param_montotransf As New SqlClient.SqlParameter
            param_montotransf.ParameterName = "@montotransf"
            param_montotransf.SqlDbType = SqlDbType.Decimal
            param_montotransf.Precision = 18
            param_montotransf.Scale = 2
            param_montotransf.Value = 0
            param_montotransf.Direction = ParameterDirection.Input

            Dim param_impuestos As New SqlClient.SqlParameter
            param_impuestos.ParameterName = "@impuestos"
            param_impuestos.SqlDbType = SqlDbType.Bit
            param_impuestos.Value = False
            param_impuestos.Direction = ParameterDirection.Input

            Dim param_montoimpuesto As New SqlClient.SqlParameter
            param_montoimpuesto.ParameterName = "@montoimpuesto"
            param_montoimpuesto.SqlDbType = SqlDbType.Decimal
            param_montoimpuesto.Precision = 18
            param_montoimpuesto.Scale = 2
            param_montoimpuesto.Value = 0
            param_montoimpuesto.Direction = ParameterDirection.Input

            Dim param_montoiva As New SqlClient.SqlParameter
            param_montoiva.ParameterName = "@montoiva"
            param_montoiva.SqlDbType = SqlDbType.Decimal
            param_montoiva.Precision = 18
            param_montoiva.Scale = 2
            param_montoiva.Value = IIf(lblMontoIva.Text = "", 0, lblMontoIva.Text)
            param_montoiva.Direction = ParameterDirection.Input

            Dim param_subtotal As New SqlClient.SqlParameter
            param_subtotal.ParameterName = "@subtotal"
            param_subtotal.SqlDbType = SqlDbType.Decimal
            param_subtotal.Precision = 18
            param_subtotal.Scale = 2
            param_subtotal.Value = CDbl(txtSubtotal.Text)
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
            param_Redondeo.Value = 0
            param_Redondeo.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Insert", _
                                            param_id, param_codigo, param_idProveedor, param_fecha, param_contado, param_montoContado, _
                                            param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, param_montochequepropio, _
                                            param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                            param_montoiva, param_subtotal, param_total, param_Redondeo, param_nota, _
                                            param_useradd, param_res)

                    txtidpago.Text = param_id.Value

                Else
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Update", _
                                            param_id, param_codigo, param_idProveedor, param_fecha, param_contado, param_montoContado, _
                                            param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, param_montochequepropio, _
                                            param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                            param_montoiva, param_subtotal, param_total, param_Redondeo, param_nota, _
                                            param_useradd, param_res)

                End If

                res = param_res.Value

                AgregarActualizar_Pago = res

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

    Private Function AgregarRegistro_PagosGastos() As Integer
        Dim res As Integer = 0

        Try

            Dim param_idPago As New SqlClient.SqlParameter
            param_idPago.ParameterName = "@idPago"
            param_idPago.SqlDbType = SqlDbType.BigInt
            param_idPago.Value = txtidpago.Text
            param_idPago.Direction = ParameterDirection.Input

            Dim param_idGasto As New SqlClient.SqlParameter
            param_idGasto.ParameterName = "@idGasto"
            param_idGasto.SqlDbType = SqlDbType.BigInt
            param_idGasto.Value = txtID.Text
            param_idGasto.Direction = ParameterDirection.Input

            Dim param_DEUDA As New SqlClient.SqlParameter
            param_DEUDA.ParameterName = "@Deuda"
            param_DEUDA.SqlDbType = SqlDbType.Decimal
            param_DEUDA.Precision = 18
            param_DEUDA.Scale = 2
            param_DEUDA.Value = 0
            param_DEUDA.Direction = ParameterDirection.Input

            Dim param_CancelarTodo As New SqlClient.SqlParameter
            param_CancelarTodo.ParameterName = "@CancelarTodo"
            param_CancelarTodo.SqlDbType = SqlDbType.Bit
            param_CancelarTodo.Value = 1
            param_CancelarTodo.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Gastos_Insert", _
                                          param_idPago, param_idGasto, param_DEUDA, param_CancelarTodo, param_res)

                res = param_res.Value

            Catch ex As Exception
                Throw ex
            End Try

            AgregarRegistro_PagosGastos = res

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
                    param_Id.Value = grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.IdImpuestoxGasto).Value
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdGasto As New SqlClient.SqlParameter
                    param_IdGasto.ParameterName = "@IdGasto"
                    param_IdGasto.SqlDbType = SqlDbType.BigInt
                    param_IdGasto.Value = txtID.Text
                    param_IdGasto.Direction = ParameterDirection.Input

                    Dim param_CodigoImpuesto As New SqlClient.SqlParameter
                    param_CodigoImpuesto.ParameterName = "@CodigoImpuesto"
                    param_CodigoImpuesto.SqlDbType = SqlDbType.NVarChar
                    param_CodigoImpuesto.Size = 50
                    param_CodigoImpuesto.Value = grdImpuestos.Rows(i).Cells(ColumnasDelGridImpuestos.codigo).Value
                    param_CodigoImpuesto.Direction = ParameterDirection.Input

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

                    Try
                        If bolModo = True Then

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Impuestos_Insert", _
                                param_IdGasto, param_CodigoImpuesto, param_NroDocumento, _
                                 param_Monto, param_res)

                        Else

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Impuestos_Update", _
                                 param_Id, param_NroDocumento, param_Monto, param_res)

                        End If

                        res = param_res.Value

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

    Private Function AgregarRegistro_NotasCredito() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To lstFacturasPendientes.Items.Count - 1

                    If lstFacturasPendientes.Items(i).Checked = True Then

                        Dim param_IdGasto As New SqlClient.SqlParameter
                        param_IdGasto.ParameterName = "@IdGasto"
                        param_IdGasto.SqlDbType = SqlDbType.BigInt
                        param_IdGasto.Value = txtID.Text
                        param_IdGasto.Direction = ParameterDirection.Input

                        Dim param_CodigoGasto As New SqlClient.SqlParameter
                        param_CodigoGasto.ParameterName = "@CodigoGasto"
                        param_CodigoGasto.SqlDbType = SqlDbType.BigInt
                        param_CodigoGasto.Value = lstFacturasPendientes.Items(i).SubItems(0).Text
                        param_CodigoGasto.Direction = ParameterDirection.Input

                        Dim param_res As New SqlClient.SqlParameter
                        param_res.ParameterName = "@res"
                        param_res.SqlDbType = SqlDbType.Int
                        param_res.Value = 0
                        param_res.Direction = ParameterDirection.InputOutput

                        Try
                            If bolModo = True Then
                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_NotasCredito_Insert", _
                                param_IdGasto, param_CodigoGasto, param_res)

                                res = param_res.Value
                            End If

                            
                            If res < 0 Then
                                AgregarRegistro_NotasCredito = -1
                                Exit Function
                            End If

                        Catch ex As Exception
                            Throw ex
                            AgregarRegistro_NotasCredito = -1
                            Exit Function
                        End Try
                    End If

                Next

                AgregarRegistro_NotasCredito = 1

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

    Private Function Eliminar_Pago() As Integer
        Dim res As Integer = 0

        Try
            'Dim param_idgasto As New SqlClient.SqlParameter
            'param_idgasto.ParameterName = "@idgASTO"
            'param_idgasto.SqlDbType = SqlDbType.BigInt
            'param_idgasto.Value = txtID.Text
            'param_idgasto.Direction = ParameterDirection.Input

            Dim param_idpago As New SqlClient.SqlParameter
            param_idpago.ParameterName = "@id"
            param_idpago.SqlDbType = SqlDbType.BigInt
            param_idpago.Value = txtidpago.Text
            param_idpago.Direction = ParameterDirection.Input

            Dim param_userdel As New SqlClient.SqlParameter
            param_userdel.ParameterName = "@userdel"
            param_userdel.SqlDbType = SqlDbType.Int
            param_userdel.Value = UserID
            param_userdel.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Delete", _
                                            param_idpago, param_userdel, param_res)

                Eliminar_Pago = param_res.Value

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

    Private Function EliminarRegistro_Gasto() As Integer
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

                Dim param_idGasto As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_idGasto.Value = CType(txtID.Text, Long)
                param_idGasto.Direction = ParameterDirection.Input

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Delete", _
                                              param_idGasto, param_userdel, param_res)

                    EliminarRegistro_Gasto = param_res.Value

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

    Private Function EliminarRegistro_Recepcion() As Integer
        Dim res As Integer = 0
        Dim msg As String

        Try

            Dim param_idRecepcion As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
            param_idRecepcion.Value = CType(txtIdRecepcion.Text, Long)
            param_idRecepcion.Direction = ParameterDirection.Input

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

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spRecepciones_Delete_All", param_idRecepcion, param_userdel, param_res, param_msg)
                res = param_res.Value

                If Not (param_msg.Value Is System.DBNull.Value) Then
                    msg = param_msg.Value
                Else
                    msg = ""
                End If

                EliminarRegistro_Recepcion = res

            Catch ex As Exception
                '' 
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

  
End Class


