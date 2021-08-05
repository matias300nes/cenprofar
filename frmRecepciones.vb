Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmRecepciones

    Dim bolpoliticas As Boolean

    Dim permitir_evento_CellChanged As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean

    Dim llenandoCombo As Boolean = False

    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Para el combo de busqueda
    Dim ID_Buscado As Long
    Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDRecepcion_Det = 0
        Cod_RecepcionDet = 1
        IDMaterial = 2
        Cod_Material = 3
        Cod_Mat_Prov = 4
        Nombre_Material = 5
        IdUnidad = 6
        CodUnidad = 7
        Unidad = 8
        IdMoneda = 9
        CodMoneda = 10
        Moneda = 11
        ValorCambio = 12
        PrecioLista = 13
        IVA = 14
        Bonif1 = 15
        Bonif2 = 16
        Bonif3 = 17
        Bonif4 = 18
        Bonif5 = 19
        Ganancia = 20
        PrecioListaReal = 21
        QtyPedido = 22
        QtyRecep = 23
        PrecioCosto = 24
        PorcDif = 25
        Remito = 26
        ID_OrdenDeCompra = 27
        ID_OrdenDeCompra_Det = 28
        QtySaldo = 29
        Status = 30
        FechaCumplido = 31
        PrecioEnPesos = 32
        PrecioEnPesosNuevo = 33
        Nuevo = 34
        IdMaterial_Prov = 35
        item = 36
        PrecioMayorista = 37
        PrecioRevendedor = 38
        PrecioYamila = 39
        PrecioLista4 = 40
        PrecioPeron = 41
        PrecioMayoPeron = 42
        CantidadPack = 43
        IdMarca = 44
    End Enum

    'Enum ColumnasDelGridItemsIVA
    '    id = 0
    '    Subtotal = 1
    '    PorcIva = 2
    '    MontoIVA = 3
    'End Enum

    Enum ColumnasDelGridImpuestos
        Id = 0
        codigo = 1
        NroDocumento = 2
        Monto = 3
        IdIngreso = 4
        IdImpuestoxGasto = 5
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim band As Integer

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient


#Region "   Eventos"

    Private Sub frmRecepciones_Gestion_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Items()
            End If
        End If
    End Sub

    Private Sub frmAjustes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        btnSalir_Click(sender, e)
    End Sub

    Private Sub frmRecepciones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la Recepción Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Orde de Compra Nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmRecepciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Cursor = Cursors.WaitCursor

        ToolStrip_lblCodMaterial.Visible = True
        txtBusquedaMAT.Visible = True

        'Try

        '    Dim sqlstring As String = "update [" & NameTable_NotificacionesWEB & "] set BloqueoR = 1 where idalmacen <> " & Util.numero_almacen
        '    tranWEB.Sql_Set(sqlstring)
        'Catch ex As Exception

        'End Try

        band = 0

        btnEliminar.Text = "Anular Recepción"

        configurarform()
        asignarTags()

        LlenarcmbAlmacenes()
        LlenarcmbUsuarioGasto()
        LlenarComboProveedores()
        LlenarcmbTipoFacturas_APP(cmbTipoComprobante, ConnStringSEI)

        SQL = "exec spRecepciones_Select_All  @Eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        Setear_Grilla()

        If bolModo = True Then
            LlenarGrid_Items()
            LlenarGrid_IVA(0)
            LlenarGrid_Impuestos()
            band = 1
            btnNuevo_Click(sender, e)
        Else
            btnLlenarGrilla.Enabled = bolModo

            'chkAsociarFacturaCargada.Enabled = bolModo
            'cmbAsociarFacturaCargada.Enabled = bolModo

            Try
                LlenarGrid_IVA(CType(txtIdGasto.Text, Long))

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            LlenarGrid_Impuestos()

        End If

        cmbProveedor.Visible = bolModo
        'cmbProveedor.SelectedIndex = 0



        txtProveedor.Visible = Not bolModo
        txtOC.Visible = Not bolModo

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        grd.Columns(3).Visible = False
        grd.Columns(4).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(10).Visible = False
        grd.Columns(23).Visible = False
        grd.Columns(26).Visible = False
        grd.Columns(27).Visible = False
        grd.Columns(30).Visible = False
        grd.Columns(31).Visible = False



        If grd.RowCount > 0 Then
            txtCantIVA.Value = grd.CurrentRow.Cells(31).Value
            chkCargarFactura.Enabled = False
            chkCargarFactura_CheckedChanged(sender, e)
        End If

        Contar_Filas()

        dtpFECHA.MaxDate = Today.Date

        band = 1

        Cursor = Cursors.Default

    End Sub

    Private Sub txtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
        If txtID.Text <> "" And bolModo = False Then
            LlenarGrid_Items()
        End If
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote    
        If Me.grdItems.CurrentCell.ColumnIndex = 7 Then
            AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
        Else
            AddHandler e.Control.KeyPress, AddressOf NoValidar
        End If
        'End If
    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtID.KeyPress, txtCODIGO.KeyPress, txtNota.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles dtpFECHA.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub grdItems_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdItems.MouseUp
        Dim Valor As String
        Valor = ""
        If e.Button = Windows.Forms.MouseButtons.Right And bolModo Then
            If grdItems.RowCount <> 0 Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If
            If Valor <> "" Then
                Dim p As Point = New Point(e.X, e.Y)
                ContextMenuStrip1.Show(grdItems, p)
                ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
            End If
        End If
    End Sub

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        'Borrar la fila actual
        If cell.RowIndex <> 0 Then
            grdItems.Rows.RemoveAt(cell.RowIndex)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem.SelectedIndexChanged
        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.ComboBox.SelectedValue
        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If Not bolModo Then
            'cmbProveedores.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

    Private Sub grdItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdItems.KeyDown

        'Dim columna As Integer = 0
        'columna = grdItems.CurrentCell.ColumnIndex

        ''If columna = ColumnasDelGridItems.ID_OrdenDeCompra Then
        'If columna = 7 Then
        '    If e.KeyCode = Keys.F5 And bolModo Then
        '        Dim f As New ICYS.frmOrdenCompra
        '        LLAMADO_POR_FORMULARIO = True
        '        ARRIBA = 40
        '        IZQUIERDA = Me.Left - 150
        '        'texto_del_combo = cmbPROVEEDORES.Text.ToUpper.ToString
        '        f.ShowDialog()
        '        'MsgBox(ID_ORDEN_DE_COMPRA_DET.ToString)

        '        If STATUS_ORDEN_DE_COMPRA_DET = "CUMPLIDO" Then
        '            MsgBox("El item seleccionado esta cumplido. NO se puede cargar.", MsgBoxStyle.Information, "Atención")
        '        Else
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.ID_OrdenDeCompra).Value = ID_ORDEN_DE_COMPRA
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Value = ID_ORDEN_DE_COMPRA_DET
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.IDMaterial).Value = ID_MATERIAL
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.Cod_Material).Value = CODIGO_MATERIAL
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.Nombre_Material).Value = NOMBRE_MATERIAL
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.IDUnidad).Value = ID_UNIDAD
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.Unidad).Value = UNIDAD_MATERIAL

        '        End If
        '    End If
        'End If

    End Sub

    Private Sub cmbOrdenDeCompra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrdenDeCompra.SelectedIndexChanged
        If band = 1 Then
            If llenandoCombo = False Then
                btnLlenarGrilla_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub cmbOrdenDeCompra_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOrdenDeCompra.SelectionChangeCommitted
        If band = 1 Then
            If llenandoCombo = False Then
                btnLlenarGrilla_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub cmbProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProveedor.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            LimpiarGridItems(grdItems)
            LlenarComboOrdenDeCompra()
            LlenarcomboGastos(cmbGastos)
            btnLlenarGrilla_Click(sender, e)
        End If
    End Sub

    Private Sub cmbTipoComprobante_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        txtMontoIVA10.Text = "0"
        txtMontoIVA21.Text = "0"
        txtMontoIVA27.Text = "0"
        txtSubtotal.Text = "0"
        txtSubtotalExento.Text = "0"
        lblTotal.Text = "0"
        txtValorCambio.Text = "1"

        If cmbTipoComprobante.Text = "FACTURAS A" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO A" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO A" Or _
            cmbTipoComprobante.Text = "RECIBOS A" Or _
            cmbTipoComprobante.Text = "FACTURAS M" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO M" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO M" Or _
            cmbTipoComprobante.Text = "RECIBOS M" Or _
            cmbTipoComprobante.Text = "TIQUE FACTURA A" Then
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

    End Sub

    Private Sub txtSubtotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
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
                    cmbTipoComprobante.Text = "TIQUE FACTURA A" Then

                    txtMontoIVA21.Text = Format(txtSubtotal.Text * 0.21, "###0.00")

                    CalcularMontoIVA()

                End If

                If lblImpuestos.Text = "" Then lblImpuestos.Text = "0"
                If lblMontoIva.Text = "" Then lblMontoIva.Text = "0"
                If txtSubtotal.Text = "" Then txtSubtotal.Text = "0"
                If txtSubtotalExento.Text = "" Then txtSubtotalExento.Text = "0"

                lblTotal.Text = CDbl(txtSubtotalExento.Text) + CDbl(txtSubtotal.Text) + CDbl(lblMontoIva.Text) + CDbl(lblImpuestos.Text)

            End If

        End If
    End Sub

    Private Sub txtsubtotalNoGravado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotalNoGravado_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If band = 1 Then
            If txtSubtotalExento.Text <> "" Then

                If lblImpuestos.Text = "" Then lblImpuestos.Text = "0"
                If lblMontoIva.Text = "" Then lblMontoIva.Text = "0"
                If txtSubtotal.Text = "" Then txtSubtotal.Text = "0"

                lblTotal.Text = CDbl(txtSubtotalExento.Text) + CDbl(txtSubtotal.Text) + CDbl(lblMontoIva.Text) + CDbl(lblImpuestos.Text)
            End If
        End If
    End Sub


    '(currentcellChanged)
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If Permitir Then
            Try
                chkFacturaCancelada.Enabled = Not CBool(grd.CurrentRow.Cells(20).Value)

                If bolModo = False Then
                    If cmbTipoComprobante.SelectedIndex = -1 Then
                        cmbTipoComprobante.SelectedIndex = 0
                    End If
                End If

                LlenarGrid_IVA(CLng(txtIdGasto.Text))

                LlenarGrid_Impuestos()

                Contar_Filas()

                txtProveedor.Enabled = bolModo
                cmbTipoComprobante.Enabled = bolModo
                txtNroRemitoCompleto.Enabled = bolModo
                txtPtoVta.Enabled = bolModo
                txtNroFactura.Enabled = bolModo
                txtSubtotalExento.Enabled = bolModo
                txtSubtotal.Enabled = bolModo
                txtMontoIVA10.Enabled = bolModo
                txtMontoIVA21.Enabled = bolModo
                txtMontoIVA27.Enabled = bolModo
                grdImpuestos.Enabled = bolModo



            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub chkAnuladas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnuladas.CheckedChanged
        btnGuardar.Enabled = Not chkAnuladas.Checked
        btnEliminar.Enabled = Not chkAnuladas.Checked
        btnNuevo.Enabled = Not chkAnuladas.Checked
        btnCancelar.Enabled = Not chkAnuladas.Checked

        If chkAnuladas.Checked = True Then
            SQL = "exec spRecepciones_Select_All @Eliminado = 1"
        Else
            SQL = "exec spRecepciones_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        LlenarGrid_IVA(CType(txtIdGasto.Text, Long))
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

    Private Sub txtPtoVta_LostFocus(sender As Object, e As EventArgs)
        If band = 1 And bolModo = True Then
            txtNroFacturaCompleto.Text = txtPtoVta.Text.Trim.PadLeft(4, "0") & "-" & txtNroFactura.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub txtNroFactura_LostFocus(sender As Object, e As EventArgs)
        If band = 1 And bolModo = True Then
            txtNroFacturaCompleto.Text = txtPtoVta.Text.Trim.PadLeft(4, "0") & "-" & txtNroFactura.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub txtPtoVtaRemito_LostFocus(sender As Object, e As EventArgs)
        If band = 1 Then 'And bolModo = True Then
            txtNroRemitoCompleto.Text = txtPtoVtaRemito.Text.Trim.PadLeft(4, "0") & "-" & txtNroCompRemito.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub txtNroCompRemito_LostFocus(sender As Object, e As EventArgs)
        If band = 1 Then 'And bolModo = True Then
            txtNroRemitoCompleto.Text = txtPtoVtaRemito.Text.Trim.PadLeft(4, "0") & "-" & txtNroCompRemito.Text.Trim.PadLeft(8, "0")
        End If
    End Sub

    Private Sub chkGrillaInferior_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGrillaInferior.CheckedChanged
        Dim xgrd As Long, ygrd As Long, hgrd As Long, variableajuste As Long
        xgrd = grd.Location.X
        ygrd = grd.Location.Y
        hgrd = grd.Height

        variableajuste = 150

        If chkGrillaInferior.Checked = True Then
            chkGrillaInferior.Text = "Disminuir Grilla Inferior"
            chkGrillaInferior.Location = New Point(chkGrillaInferior.Location.X, chkGrillaInferior.Location.Y - variableajuste)
            GroupBox1.Height = GroupBox1.Height - variableajuste
            grd.Location = New Point(xgrd, ygrd - variableajuste)
            grd.Height = hgrd + variableajuste
            grdItems.Height = grdItems.Height - variableajuste
            Label19.Location = New Point(Label19.Location.X, Label19.Location.Y - variableajuste)
            lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y - variableajuste)

        Else
            chkGrillaInferior.Text = "Aumentar Grilla Inferior"
            chkGrillaInferior.Location = New Point(chkGrillaInferior.Location.X, chkGrillaInferior.Location.Y + variableajuste)
            GroupBox1.Height = GroupBox1.Height + variableajuste
            grd.Location = New Point(xgrd, ygrd + variableajuste)
            grd.Height = hgrd - variableajuste
            grdItems.Height = grdItems.Height + variableajuste
            Label19.Location = New Point(Label19.Location.X, Label19.Location.Y + variableajuste)
            lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y + variableajuste)

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

    Private Sub txtMontoIVA21_LostFocus(sender As Object, e As EventArgs)
        CalcularMontoIVA()
    End Sub

    Private Sub txtMontoIVA10_LostFocus(sender As Object, e As EventArgs)
        CalcularMontoIVA()
    End Sub

    Private Sub txtMontoIVA27_LostFocus(sender As Object, e As EventArgs)
        CalcularMontoIVA()
    End Sub

    Private Sub txtMontoIVA21_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            CalcularMontoIVA()
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtMontoIVA10_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            CalcularMontoIVA()
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtMontoIVA27_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            CalcularMontoIVA()
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCantIVA_ValueChanged(sender As Object, e As EventArgs)
        If cmbTipoComprobante.Text = "FACTURAS A" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO A" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO A" Or _
            cmbTipoComprobante.Text = "RECIBOS A" Or _
            cmbTipoComprobante.Text = "FACTURAS M" Or _
            cmbTipoComprobante.Text = "NOTAS DE CREDITO M" Or _
            cmbTipoComprobante.Text = "NOTAS DE DEBITO M" Or _
            cmbTipoComprobante.Text = "RECIBOS M" Or _
            cmbTipoComprobante.Text = "TIQUE FACTURA A" Then

            If txtCantIVA.Value = 0 Then
                txtCantIVA.Value = 1
            End If
        End If
    End Sub

    Private Sub chkCargarFactura_CheckedChanged(sender As Object, e As EventArgs)

        cmbTipoComprobante.Enabled = chkCargarFactura.Checked
        Label10.Enabled = chkCargarFactura.Checked
        txtPtoVta.Enabled = chkCargarFactura.Checked
        Label14.Enabled = chkCargarFactura.Checked
        Label11.Enabled = chkCargarFactura.Checked
        txtNroFactura.Enabled = chkCargarFactura.Checked
        Label13.Enabled = chkCargarFactura.Checked
        txtNroFacturaCompleto.Enabled = chkCargarFactura.Checked
        Label17.Enabled = chkCargarFactura.Checked
        txtTipoMoneda.Enabled = chkCargarFactura.Enabled
        Label20.Enabled = chkCargarFactura.Checked
        'If txtTipoMoneda.Text = "Pe" Then
        '    txtValorCambio.Enabled = False
        'Else
        '    txtValorCambio.Enabled = True
        'End If
        lblSubtotalExento.Enabled = chkCargarFactura.Checked
        txtSubtotalExento.Enabled = chkCargarFactura.Checked
        Label12.Enabled = chkCargarFactura.Checked
        txtSubtotal.Enabled = chkCargarFactura.Checked
        Label21.Enabled = chkCargarFactura.Checked
        Label15.Enabled = chkCargarFactura.Checked
        Label18.Enabled = chkCargarFactura.Checked
        Label1.Enabled = chkCargarFactura.Checked
        label40.Enabled = chkCargarFactura.Checked
        Label7.Enabled = chkCargarFactura.Checked
        Label4.Enabled = chkCargarFactura.Checked

        txtCantIVA.Enabled = chkCargarFactura.Checked
        txtMontoIVA10.Enabled = chkCargarFactura.Checked
        txtMontoIVA21.Enabled = chkCargarFactura.Checked
        txtMontoIVA27.Enabled = chkCargarFactura.Checked
        lblImpuestos.Enabled = chkCargarFactura.Checked
        lblTotal.Enabled = chkCargarFactura.Checked
        grdImpuestos.Enabled = chkCargarFactura.Checked

        chkFacturaCancelada.Enabled = chkCargarFactura.Checked

    End Sub

    Private Sub chkRecepcion_CheckedChanged(sender As Object, e As EventArgs)
        cmbGastos.Enabled = chkGastos.Checked
        txtPtoVtaRemito.Enabled = Not chkGastos.Checked
        txtNroCompRemito.Enabled = Not chkGastos.Checked

        chkCargarFactura.Enabled = Not chkGastos.Checked


        'txtValorCambio.Enabled = chkGastos.Checked

        'Label4.Enabled = Not chkGastos.Checked

        If chkGastos.Checked = False And bolModo = True Then
            txtPtoVtaRemito.Text = ""
            txtNroCompRemito.Text = ""
            txtNroRemitoCompleto.Text = ""
        End If

        cmbGastos_SelectedIndexChanged(sender, e)

    End Sub

    Private Sub cmbGastos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If band = 1 And chkGastos.Checked = True And bolModo = True Then
            BuscarRemito()
            txtIdGastoAsociar.Text = cmbGastos.SelectedValue
        Else
            If chkGastos.Checked = False And bolModo = True Then
                txtNroRemitoCompleto.Text = ""
                txtIdGastoAsociar.Text = ""
                txtIdMoneda.Text = "1"
                txtValorCambio.Text = "1"
                txtTipoMoneda.Text = "Pe"
            End If
        End If
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Recepciones de Material"

        'Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 5)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        'Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 3 - GroupBox1.Size.Height - GroupBox1.Location.Y - 62) '65)

    End Sub

    Private Sub asignarTags()

        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbAlmacenes.Tag = "4"
        txtIdProveedor.Tag = "5"
        cmbProveedor.Tag = "6"
        txtProveedor.Tag = "6"
        txtOC.Tag = "7"
        cmbOrdenDeCompra.Tag = "7"
        txtNroRemitoCompleto.Tag = "8"
        txtNroRemitoControl.Tag = "8"
        cmbTipoComprobante.Tag = "11"
        txtIdComprobante.Tag = "10"
        txtNroFacturaCompleto.Tag = "12"
        txtNroFacturaCompletoControl.Tag = "12"
        txtSubtotalExento.Tag = "13"
        txtSubtotal.Tag = "14"
        lblMontoIva.Tag = "15"
        lblImpuestos.Tag = "16"
        lblTotal.Tag = "17"
        txtNota.Tag = "18"
        chkEliminado.Tag = "19"
        chkFacturaCancelada.Tag = "20"
        txtIdGasto.Tag = "23"
        txtPtoVta.Tag = "24"
        txtNroFactura.Tag = "25"
        txtValorCambio.Tag = "26"
        txtTipoMoneda.Tag = "27"
        txtPtoVtaRemito.Tag = "28"
        txtNroCompRemito.Tag = "29"
        txtIdMoneda.Tag = "30"

    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer

        Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String

        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : ubicacion = "" : observaciones = ""


        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si se terminó de editar la celda...
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If editando_celda Then
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'verificar que no hay nada en la grilla sin datos
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        j = grdItems.RowCount - 1
        filas = 0
        For i = 0 To j
            'state = grdItems.Rows.GetRowState(i)
            'la fila está vacía ?
            If fila_vacia(i) Then
                Try
                    'encotramos una fila vacia...borrarla y ver si hay mas
                    grdItems.Rows.RemoveAt(i)

                    j = j - 1 ' se reduce la cantidad de filas en 1
                    i = i - 1 ' se reduce para recorrer la fila que viene 
                Catch ex As Exception
                End Try

            Else
                filas = filas + 1
                'idmaterial es valido?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                Try
                    'qty es válida?
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value Is System.DBNull.Value Then
                        Util.MsgStatus(Status1, "Falta completar la cantidad a Recepcionar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "Falta completar la cantidad a Recepcionar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If

                Catch ex As Exception
                    Util.MsgStatus(Status1, "La cantidad a Recepcionar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "La cantidad a Recepcionar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End Try

                'si tiene saldo, controlamos que no se pase..
                If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Is DBNull.Value Then
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                        Util.MsgStatus(Status1, "La cantidad a Recepcionar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "La cantidad a Recepcionar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If
                End If

            End If
        Next i

        Dim buscandoalgunmov As Boolean = False

        For i = 0 To grdItems.RowCount - 1
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value > 0 Then
                buscandoalgunmov = True
                Exit For
            End If
        Next

        If buscandoalgunmov = False Then
            Util.MsgStatus(Status1, "No realizó ningún movimiento dentro de la grilla. Por favor, verifique antes de guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No realizó ningún movimiento dentro de la grilla. Por favor, verifique antes de guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If filas > 0 Then
            bolpoliticas = True
        Else
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If
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

    Private Sub NoValidar(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim caracter As Char = e.KeyChar
        Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = sender
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString.ToUpper)
        e.Handled = False ' dejar escribir cualquier cosa
    End Sub

    Private Sub validarNumerosReales(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Controlar que el caracter ingresado sea un  numero real
        Dim caracter As Char = e.KeyChar
        Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = sender

        If caracter = "." Or caracter = "," Then
            If CuentaAparicionesDeCaracter(obj.Text, ".") = 0 Then
                If CuentaAparicionesDeCaracter(obj.Text, ",") = 0 Then
                    e.Handled = False ' dejar escribir
                Else
                    e.Handled = True 'no deja escribir
                End If
            Else
                e.Handled = True ' no deja escribir
            End If
        Else
            If caracter = "-" And obj.Text.Trim <> "" Then
                If CuentaAparicionesDeCaracter(obj.Text, caracter) < 1 Then
                    obj.Text = "-" + obj.Text
                    e.Handled = True ' no dejar escribir
                Else
                    obj.Text = Mid(obj.Text, 2, Len(obj.Text))
                    e.Handled = True ' no dejar escribir
                End If
            Else
                If Char.IsNumber(caracter) Or caracter = ChrW(Keys.Back) Or caracter = ChrW(Keys.Delete) Or caracter = ChrW(Keys.Enter) Then
                    e.Handled = False 'dejo escribir
                Else
                    e.Handled = True ' no dejar escribir
                End If
            End If
        End If
    End Sub

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
        With (grdItems)
            '.AllowUserToAddRows = True
            .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material  
            .ScrollBars = ScrollBars.Both
        End With
    End Sub

    Private Sub LlenarGrid_Items()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        If txtID.Text = "" Then
            SQL = "exec spRecepciones_Det_Select_By_IDRecepcion @idRecepcion = 1"
        Else
            SQL = "exec spRecepciones_Det_Select_By_IDRecepcion @idRecepcion = " & txtID.Text
        End If

        GetDatasetItems(grdItems)

        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Visible = False

        'grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        'grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
        'grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 110

        'grdItems.Columns(4).ReadOnly = True
        'grdItems.Columns(4).Width = 400

        'grdItems.Columns(5).ReadOnly = True
        'grdItems.Columns(5).Width = 60

        grdItems.Columns(6).Visible = False
        'grdItems.Columns(8).Visible = False
        grdItems.Columns(9).Visible = False
        grdItems.Columns(10).Visible = False

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
        End With
        With grdItems.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With
        grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)
        'grdEnsayos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        'Volver la fuente de datos a como estaba...
        SQL = "exec spRecepciones_Select_All @Eliminado = 0"
    End Sub

    Private Sub LlenarGridItemsPorOC(ByVal idoc As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        SQL = "exec spRecepciones_Det_Select_By_IDOrdenDeCompra @IdOrdenDeCompra = " & idoc

        GetDatasetItems(grdItems)



        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False


        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = False '3
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True '3
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70

        grdItems.Columns(ColumnasDelGridItems.Cod_Mat_Prov).Visible = False '3
        grdItems.Columns(ColumnasDelGridItems.Cod_Mat_Prov).ReadOnly = True '3

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True '4
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

        grdItems.Columns(ColumnasDelGridItems.IdUnidad).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.IdMoneda).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.CodMoneda).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.Moneda).Visible = False

        grdItems.Columns(ColumnasDelGridItems.CodUnidad).Visible = False
        grdItems.Columns(ColumnasDelGridItems.CodUnidad).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.CodUnidad).Width = 55
        grdItems.Columns(ColumnasDelGridItems.CodUnidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 60

        grdItems.Columns(ColumnasDelGridItems.ValorCambio).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.Bonif1).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Bonif2).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Bonif2).Visible = chkMostarColumnas.Checked

        grdItems.Columns(ColumnasDelGridItems.Bonif3).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Bonif3).Visible = chkMostarColumnas.Checked

        grdItems.Columns(ColumnasDelGridItems.Bonif4).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Bonif4).Visible = chkMostarColumnas.Checked

        grdItems.Columns(ColumnasDelGridItems.Bonif5).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Bonif5).Visible = chkMostarColumnas.Checked

        grdItems.Columns(ColumnasDelGridItems.IVA).Visible = False
        grdItems.Columns(ColumnasDelGridItems.IVA).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IVA).Width = 45
        grdItems.Columns(ColumnasDelGridItems.IVA).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        grdItems.Columns(ColumnasDelGridItems.Ganancia).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Ganancia).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Ganancia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'grdItems.Columns(ColumnasDelGridItems.PrecioxMt).Width = 65
        'grdItems.Columns(ColumnasDelGridItems.PrecioxMt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'grdItems.Columns(ColumnasDelGridItems.PrecioxMt).Visible = False

        'grdItems.Columns(ColumnasDelGridItems.PrecioxKg).Width = 65
        'grdItems.Columns(ColumnasDelGridItems.PrecioxKg).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'grdItems.Columns(ColumnasDelGridItems.PrecioxKg).Visible = False

        'grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).Width = 65
        'grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).Visible = False

        'grdItems.Columns(ColumnasDelGridItems.CantxLongitud).Width = 65
        'grdItems.Columns(ColumnasDelGridItems.CantxLongitud).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'grdItems.Columns(ColumnasDelGridItems.CantxLongitud).Visible = False

        grdItems.Columns(ColumnasDelGridItems.QtyPedido).ReadOnly = True 'cantidad pedida 5
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).Width = 80
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PrecioLista).Visible = False '4
        grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.PrecioLista).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioLista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.PrecioCosto).ReadOnly = False  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.PrecioCosto).Width = 80
        grdItems.Columns(ColumnasDelGridItems.PrecioCosto).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.QtyRecep).ReadOnly = False 'cantidad a recibir 7
        grdItems.Columns(ColumnasDelGridItems.QtyRecep).Width = 80
        grdItems.Columns(ColumnasDelGridItems.QtyRecep).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PorcDif).ReadOnly = True 'cantidad a recibir 7
        grdItems.Columns(ColumnasDelGridItems.PorcDif).Width = 50
        grdItems.Columns(ColumnasDelGridItems.PorcDif).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.ID_OrdenDeCompra).Visible = False
        grdItems.Columns(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Remito).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True 'precio real
        grdItems.Columns(ColumnasDelGridItems.Status).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PrecioListaReal).ReadOnly = True 'precio real
        grdItems.Columns(ColumnasDelGridItems.PrecioListaReal).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioListaReal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.PrecioListaReal).Visible = False

        grdItems.Columns(ColumnasDelGridItems.QtySaldo).ReadOnly = True  'precio real
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).Width = 60
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).ReadOnly = True 'precio real
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).Width = 80
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        grdItems.Columns(ColumnasDelGridItems.item).ReadOnly = True 'precio real
        grdItems.Columns(ColumnasDelGridItems.item).Width = 60
        grdItems.Columns(ColumnasDelGridItems.item).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PrecioEnPesos).Visible = False '4
        grdItems.Columns(ColumnasDelGridItems.PrecioEnPesos).ReadOnly = True '4

        grdItems.Columns(ColumnasDelGridItems.PrecioEnPesosNuevo).Visible = False '4
        grdItems.Columns(ColumnasDelGridItems.PrecioEnPesosNuevo).ReadOnly = True '4

        grdItems.Columns(ColumnasDelGridItems.Nuevo).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.IdMaterial_Prov).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.PrecioMayorista).ReadOnly = False  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.PrecioMayorista).Width = 100
        grdItems.Columns(ColumnasDelGridItems.PrecioMayorista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.PrecioPeron).ReadOnly = False  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.PrecioPeron).Width = 100
        grdItems.Columns(ColumnasDelGridItems.PrecioPeron).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.PrecioMayoPeron).ReadOnly = False  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.PrecioMayoPeron).Width = 100
        grdItems.Columns(ColumnasDelGridItems.PrecioMayoPeron).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.CantidadPack).HeaderText = "QtyxPack"
        grdItems.Columns(ColumnasDelGridItems.CantidadPack).ReadOnly = False  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.CantidadPack).Width = 50
        grdItems.Columns(ColumnasDelGridItems.CantidadPack).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.IdMarca).Visible = False  'precio pedido 6

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
            .ScrollBars = ScrollBars.Both
        End With
        With grdItems.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With
        grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)
        'grdEnsayos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        'Volver la fuente de datos a como estaba...
        SQL = "exec spRecepciones_Select_All  @Eliminado = 0"

        Dim i As Integer

        For i = 0 To grdItems.RowCount - 1
            grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Style.BackColor = Color.LightGreen
            grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Style.BackColor = Color.LightBlue
            grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioListaReal).Style.BackColor = Color.LightBlue
        Next

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
                                                                            " FROM Gastos g JOIN Gastos_det gd ON gd.idgasto = g.id WHERE IdGasto = " & IIf(txtIdGasto.Text = "", 0, txtIdGasto.Text))

            ds_IVAs.Dispose()

            Dim i As Integer
            Dim valor As Double

            For i = 0 To ds_IVAs.Tables(0).Rows.Count - 1
                valor = ds_IVAs.Tables(0).Rows(i)(1)
                If CDbl(ds_IVAs.Tables(0).Rows(i)(0)) < 11 Then
                    txtMontoIVA10.Text = valor
                Else
                    If CDbl(ds_IVAs.Tables(0).Rows(i)(0)) = 21 Then
                        txtMontoIVA21.Text = ds_IVAs.Tables(0).Rows(i)(1)
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
            If grd.CurrentRow.Cells(14).Value = "0.00" Then
                SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtIdGasto.Text = "", 0, txtIdGasto.Text) & ", @Bolmodo = 1" '& bolModo
            Else
                SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtIdGasto.Text = "", 0, txtIdGasto.Text) & ", @Bolmodo = 0" '& bolModo
            End If
        Else
            SQL = "exec spImpuestos_Gastos_Select_by_IdGasto @IdGasto = " & IIf(txtIdGasto.Text = "", 0, txtIdGasto.Text) & ", @Bolmodo = " & bolModo
        End If

        GetDatasetItems(grdImpuestos)

        grdImpuestos.Columns(ColumnasDelGridImpuestos.Id).Visible = False

        grdImpuestos.Columns(ColumnasDelGridImpuestos.codigo).ReadOnly = True
        grdImpuestos.Columns(ColumnasDelGridImpuestos.codigo).Width = 180

        grdImpuestos.Columns(ColumnasDelGridImpuestos.NroDocumento).Width = 110

        grdImpuestos.Columns(ColumnasDelGridImpuestos.Monto).Width = 70
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

    Private Sub Setear_Grilla()

        ' ajustar la columna del base
        grd.Columns(1).Width = 60 ' codigo
        grd.Columns(2).Width = 60 ' fecha
        grd.Columns(3).Width = 180 ' almacen

        'ordenar descendente
        'grd.Sort(grd.Columns(1), System.ComponentModel.ListSortDirection.Descending)

        ''setear grilla de items
        'With grdItems
        '    .VirtualMode = False
        '    .AllowUserToAddRows = False
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.MintCream
        '    .RowsDefaultCellStyle.BackColor = Color.White
        '    .AllowUserToOrderColumns = True
        '    .SelectionMode = DataGridViewSelectionMode.CellSelect
        'End With
    End Sub

    Private Sub LlenarComboProveedores()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT p.Codigo, ltrim(rtrim(p.nombre)) as Nombre " & _
                        " from OrdendeCompra o JOIN Proveedores p ON p.Codigo = o.IdProveedor where STATUS = 'P' AND o.eliminado=0 order by ltrim(rtrim(p.nombre)) ") 'AND Status = 'P' 
            ds_Cli.Dispose()

            With Me.cmbProveedor
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
            End With

            If ds_Cli.Tables(0).Rows.Count > 0 Then
                cmbProveedor.SelectedIndex = 0
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

    Private Sub LlenarcmbUsuarioGasto()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 ORDER BY Vendedor")
            ds.Dispose()

            With cmbUsuarioGasto
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Vendedor"
                '.ValueMember = "Codigo"
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

    Private Sub LlenarComboOrdenDeCompra()
        Dim ds_OC As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

        Try

            ds_OC = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select id , codigo from ordendecompra where " & _
                                            " idproveedor = " & cmbProveedor.SelectedValue & "  and eliminado=0 and solicitud = 0 and status in ('P') order by id desc")
            ds_OC.Dispose()

            With Me.cmbOrdenDeCompra
                .DataSource = ds_OC.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
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

    'Private Sub LlenarComboFacturasAsociadas()

    '    Dim ds_FacturasCargadas As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try

    '        ds_FacturasCargadas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT g.id, (c.descripcion + ' / ' + CONVERT(VARCHAR(50),NroFactura) + ' / ' + CONVERT(VARCHAR(10),Total)) as Factura FROM gastos g JOIN Comprobantes c ON c.id = g.comprobantetipo WHERE " & _
    '                                        " idproveedor = " & CType(cmbProveedor.SelectedValue, Long) & "  AND eliminado=0 order by g.id desc")
    '        ds_FacturasCargadas.Dispose()

    '        With Me.cmbAsociarFacturaCargada
    '            .DataSource = ds_FacturasCargadas.Tables(0).DefaultView
    '            .DisplayMember = "factura"
    '            .ValueMember = "id"
    '            '.AutoCompleteMode = AutoCompleteMode.Suggest
    '            '.AutoCompleteSource = AutoCompleteSource.ListItems
    '            '.DropDownStyle = ComboBoxStyle.DropDown
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

    Private Sub Contar_Filas()

        lblCantidadFilas.Text = grdItems.RowCount

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

    Private Sub BuscarMoneda_OC()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim dsMoneda As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            dsMoneda = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT mo.Codigo, o.ValorCambio, o.idmoneda FROM OrdenDeCompra o JOIN Monedas mo	ON mo.id = o.IdMoneda WHERE o.codigo = " & cmbOrdenDeCompra.Text.ToString)

            dsMoneda.Dispose()

            If dsMoneda.Tables(0).Rows.Count > 0 Then
                txtTipoMoneda.Text = dsMoneda.Tables(0).Rows(0).Item(0)
                txtValorCambio.Text = dsMoneda.Tables(0).Rows(0).Item(1)
                txtIdMoneda.Text = dsMoneda.Tables(0).Rows(0).Item(2)

                If chkCargarFactura.Checked = True And txtTipoMoneda.Text = "Do" Then
                    txtValorCambio.Enabled = True
                Else
                    txtValorCambio.Enabled = False
                End If
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

    Private Sub LlenarcmbAlmacenes()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT 0 AS 'Codigo', '' AS 'Nombre' Union SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE Codigo <> 4 AND Eliminado = 0 ORDER BY Nombre")
            ds_Marcas.Dispose()

            With Me.cmbAlmacenes
                .DataSource = ds_Marcas.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                '.SelectedIndex = 0
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

    Private Sub LlenarcomboGastos(ByVal cmb As System.Windows.Forms.ComboBox)
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

            ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, ('Mov Nro: ' + CONVERT(VARCHAR(50),codigo) + ' - Remito: ' + Nroremito) AS Codigo from gastos where IdRecepcion is null and convert(int, ptovtaremito) > 0 and convert(int, nrocomprobanteremito) >0 and IdProveedor = " & cmbProveedor.SelectedValue & " ORDER BY id asc")
            ds_Proveedores.Dispose()

            With cmb
                .DataSource = ds_Proveedores.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDownList
                .TabStop = True
            End With

            If ds_Proveedores.Tables(0).Rows.Count > 0 Then
                chkGastos.Enabled = True
                'chkGastos.Checked = True
            Else
                chkGastos.Enabled = False
                chkGastos.Checked = False
                txtNroRemitoCompleto.Text = ""
                cmbGastos.Text = ""
            End If

            'If bolModo = False Then
            '    cmbGastos.Text = grd.CurrentRow.Cells(22).Value
            'End If

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

            ds_OCReq = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ISNULL(NroRemito, '') a, ISNULL(nrocomprobanteremito, '0') AS NroCompRemito, ISNULL(ptovtaremito, '0') AS PtovtaRemito FROM Gastos WHERE id = " & IIf(cmbGastos.SelectedValue Is Nothing, 0, cmbGastos.SelectedValue))

            ds_OCReq.Dispose()

            If ds_OCReq.Tables(0).Rows.Count > 0 Then
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

#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro_Recepciones(ByVal ControlFactura As Boolean, ByVal ControlRemito As Boolean) As Integer
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
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_idAlmacen As New SqlClient.SqlParameter
                param_idAlmacen.ParameterName = "@IdAlmacen"
                param_idAlmacen.SqlDbType = SqlDbType.BigInt
                param_idAlmacen.Value = cmbAlmacenes.SelectedValue
                param_idAlmacen.Direction = ParameterDirection.Input

                Dim param_idMoneda As New SqlClient.SqlParameter
                param_idMoneda.ParameterName = "@IdMoneda"
                param_idMoneda.SqlDbType = SqlDbType.BigInt
                param_idMoneda.Value = If(txtIdMoneda.Text = "", 0, txtIdMoneda.Text)
                param_idMoneda.Direction = ParameterDirection.Input

                Dim param_idOC As New SqlClient.SqlParameter
                param_idOC.ParameterName = "@idOC"
                param_idOC.SqlDbType = SqlDbType.BigInt
                param_idOC.Value = cmbOrdenDeCompra.SelectedValue
                param_idOC.Direction = ParameterDirection.Input

                Dim param_NroOC As New SqlClient.SqlParameter
                param_NroOC.ParameterName = "@NroOC"
                param_NroOC.SqlDbType = SqlDbType.VarChar
                param_NroOC.Size = 25
                param_NroOC.Value = cmbOrdenDeCompra.Text
                param_NroOC.Direction = ParameterDirection.Input

                Dim param_PendienteGasto As New SqlClient.SqlParameter
                param_PendienteGasto.ParameterName = "@PendienteGasto"
                param_PendienteGasto.SqlDbType = SqlDbType.BigInt
                param_PendienteGasto.Value = Not chkCargarFactura.Checked
                param_PendienteGasto.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.VarChar
                param_idproveedor.Size = 25
                param_idproveedor.Value = cmbProveedor.SelectedValue
                param_idproveedor.Direction = ParameterDirection.Input

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

                Dim param_remito As New SqlClient.SqlParameter
                param_remito.ParameterName = "@Nroremito"
                param_remito.SqlDbType = SqlDbType.VarChar
                param_remito.Size = 30
                param_remito.Value = txtPtoVtaRemito.Text.Trim.PadLeft(4, "0") & "-" & txtNroCompRemito.Text.Trim.PadLeft(8, "0")
                'txtNroRemitoCompleto.Text
                param_remito.Direction = ParameterDirection.Input

                Dim param_tipofactura As New SqlClient.SqlParameter
                param_tipofactura.ParameterName = "@comprobantetipo"
                param_tipofactura.SqlDbType = SqlDbType.VarChar
                param_tipofactura.Size = 5
                param_tipofactura.Value = cmbTipoComprobante.SelectedValue
                param_tipofactura.Direction = ParameterDirection.Input

                Dim param_factura As New SqlClient.SqlParameter
                param_factura.ParameterName = "@Nrofactura"
                param_factura.SqlDbType = SqlDbType.VarChar
                param_factura.Size = 30
                param_factura.Value = txtPtoVta.Text.Trim.PadLeft(4, "0") & "-" & txtNroFactura.Text.Trim.PadLeft(8, "0")
                'txtNroFacturaCompleto.Text
                param_factura.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_asociargasto As New SqlClient.SqlParameter
                param_asociargasto.ParameterName = "@AsociarGasto"
                param_asociargasto.SqlDbType = SqlDbType.Bit
                param_asociargasto.Value = chkGastos.Checked
                param_asociargasto.Direction = ParameterDirection.Input

                Dim param_IdGastoAsociar As New SqlClient.SqlParameter
                param_IdGastoAsociar.ParameterName = "@IdGastoAsociar"
                param_IdGastoAsociar.SqlDbType = SqlDbType.BigInt
                param_IdGastoAsociar.Value = IIf(txtIdGastoAsociar.Text = "", 0, txtIdGastoAsociar.Text)
                param_IdGastoAsociar.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spRecepciones_Insert", _
                                              param_id, param_codigo, param_idAlmacen, param_idMoneda, param_idOC, param_NroOC, _
                                              param_PendienteGasto, param_fecha, param_idproveedor, _
                                              param_tipofactura, param_ptovtaRemito, param_nrocomprRemito, param_remito, _
                                              param_factura, param_nota, param_asociargasto, param_IdGastoAsociar, param_useradd, param_res)

                        txtID.Text = param_id.Value
                        txtCODIGO.Text = param_codigo.Value
                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spRecepciones_Update", _
                                              param_id, param_idproveedor, param_ControlRemito, param_ControlFactura, _
                                              param_fecha, param_ptovtaRemito, param_nrocomprRemito, param_remito, _
                                              param_factura, param_nota, param_useradd, param_res)
                    End If

                    AgregarActualizar_Registro_Recepciones = param_res.Value

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

    Private Function AgregarRegistro_Recepciones_Items(ByVal idRecepcion As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False

        Dim ValorActual As Double
        'Dim IdStockMov As Long
        'Dim Stock As Double


        Dim Comprob As String

        Try
            Try
                i = 0

                For i = 0 To grdItems.Rows.Count - 1
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcDif).Value <> 0 Then
                        ActualizarPrecio = True
                    End If
                Next

                If ActualizarPrecio = True Then
                    ActualizarPrecio = False
                    If MessageBox.Show("Existen productos cuyos precios son diferentes. Desea actualizarlos de manera individual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        ActualizarPrecio = True
                    End If
                End If

                i = 0

                Do While i < grdItems.Rows.Count

                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value, Decimal) > 0 Then

                        ValorActual = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value


                        Dim param_id As New SqlClient.SqlParameter
                        param_id.ParameterName = "@id"
                        param_id.SqlDbType = SqlDbType.BigInt
                        param_id.Value = DBNull.Value
                        param_id.Direction = ParameterDirection.InputOutput

                        Dim param_codigo As New SqlClient.SqlParameter
                        param_codigo.ParameterName = "@codigo"
                        param_codigo.SqlDbType = SqlDbType.VarChar
                        param_codigo.Size = 25
                        param_codigo.Value = DBNull.Value
                        param_codigo.Direction = ParameterDirection.InputOutput

                        Dim param_idAlmacen As New SqlClient.SqlParameter
                        param_idAlmacen.ParameterName = "@IdAlmacen"
                        param_idAlmacen.SqlDbType = SqlDbType.BigInt
                        param_idAlmacen.Value = cmbAlmacenes.SelectedValue
                        param_idAlmacen.Direction = ParameterDirection.Input

                        Dim param_idRecepcion As New SqlClient.SqlParameter
                        param_idRecepcion.ParameterName = "@idrecepcion"
                        param_idRecepcion.SqlDbType = SqlDbType.BigInt
                        param_idRecepcion.Value = idRecepcion
                        param_idRecepcion.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@idmaterial"
                        param_idmaterial.SqlDbType = SqlDbType.VarChar
                        param_idmaterial.Size = 25
                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                        param_idmaterial.Direction = ParameterDirection.Input

                        Dim param_idmaterial_prov As New SqlClient.SqlParameter
                        param_idmaterial_prov.ParameterName = "@idmaterial_prov"
                        param_idmaterial_prov.SqlDbType = SqlDbType.BigInt
                        param_idmaterial_prov.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMaterial_Prov).Value, Long)
                        param_idmaterial_prov.Direction = ParameterDirection.Input

                        Dim param_Codigo_Mat_Prov As New SqlClient.SqlParameter
                        param_Codigo_Mat_Prov.ParameterName = "@codigo_mat_prov"
                        param_Codigo_Mat_Prov.SqlDbType = SqlDbType.VarChar
                        param_Codigo_Mat_Prov.Size = 25
                        param_Codigo_Mat_Prov.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Mat_Prov).Value
                        param_Codigo_Mat_Prov.Direction = ParameterDirection.Input

                        Dim param_idproveedor As New SqlClient.SqlParameter
                        param_idproveedor.ParameterName = "@idproveedor"
                        param_idproveedor.SqlDbType = SqlDbType.VarChar
                        param_idproveedor.Size = 25
                        param_idproveedor.Value = cmbProveedor.SelectedValue
                        param_idproveedor.Direction = ParameterDirection.Input

                        Dim param_qty As New SqlClient.SqlParameter
                        param_qty.ParameterName = "@qty"
                        param_qty.SqlDbType = SqlDbType.Decimal
                        param_qty.Precision = 18
                        param_qty.Scale = 2
                        param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value, Decimal)
                        param_qty.Direction = ParameterDirection.Input

                        Dim param_idunidad As New SqlClient.SqlParameter
                        param_idunidad.ParameterName = "@idunidad"
                        param_idunidad.SqlDbType = SqlDbType.VarChar
                        param_idunidad.Size = 25
                        param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value)
                        param_idunidad.Direction = ParameterDirection.Input

                        Dim param_remito As New SqlClient.SqlParameter
                        param_remito.ParameterName = "@remito"
                        param_remito.SqlDbType = SqlDbType.VarChar
                        param_remito.Size = 25
                        param_remito.Value = txtNroRemitoCompleto.Text
                        param_remito.Direction = ParameterDirection.Input

                        Dim param_useradd As New SqlClient.SqlParameter
                        param_useradd.ParameterName = "@useradd"
                        param_useradd.SqlDbType = SqlDbType.Int
                        param_useradd.Value = UserID
                        param_useradd.Direction = ParameterDirection.Input

                        Dim param_Nuevo As New SqlClient.SqlParameter
                        param_Nuevo.ParameterName = "@Nuevo"
                        param_Nuevo.SqlDbType = SqlDbType.Bit
                        param_Nuevo.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Nuevo).Value, Boolean)
                        param_Nuevo.Direction = ParameterDirection.Input

                        Dim param_idordendecompra As New SqlClient.SqlParameter
                        param_idordendecompra.ParameterName = "@idordendecompra"
                        param_idordendecompra.SqlDbType = SqlDbType.BigInt
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra).Value Is DBNull.Value Then
                            param_idordendecompra.Value = 0
                        Else
                            param_idordendecompra.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra).Value, Long)
                        End If
                        param_idordendecompra.Direction = ParameterDirection.Input

                        Dim param_idordendecompradet As New SqlClient.SqlParameter
                        param_idordendecompradet.ParameterName = "@idordendecompradet"
                        param_idordendecompradet.SqlDbType = SqlDbType.BigInt
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Value Is DBNull.Value Then
                            param_idordendecompradet.Value = 0
                        Else
                            param_idordendecompradet.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Value, Long)
                        End If
                        param_idordendecompradet.Direction = ParameterDirection.Input

                        Dim param_material As New SqlClient.SqlParameter
                        param_material.ParameterName = "@material"
                        param_material.SqlDbType = SqlDbType.VarChar
                        param_material.Size = 300
                        param_material.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value
                        param_material.Direction = ParameterDirection.Input

                        Dim param_idmoneda As New SqlClient.SqlParameter
                        param_idmoneda.ParameterName = "@idmoneda"
                        param_idmoneda.SqlDbType = SqlDbType.BigInt
                        param_idmoneda.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value Is DBNull.Value, 1, grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value)
                        param_idmoneda.Direction = ParameterDirection.Input

                        Dim param_bonificacion1 As New SqlClient.SqlParameter
                        param_bonificacion1.ParameterName = "@bonif1"
                        param_bonificacion1.SqlDbType = SqlDbType.Decimal
                        param_bonificacion1.Precision = 18
                        param_bonificacion1.Scale = 2
                        param_bonificacion1.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value)
                        param_bonificacion1.Direction = ParameterDirection.Input

                        Dim param_bonificacion2 As New SqlClient.SqlParameter
                        param_bonificacion2.ParameterName = "@bonif2"
                        param_bonificacion2.SqlDbType = SqlDbType.Decimal
                        param_bonificacion2.Precision = 18
                        param_bonificacion2.Scale = 2
                        param_bonificacion2.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value)
                        param_bonificacion2.Direction = ParameterDirection.Input

                        Dim param_bonificacion3 As New SqlClient.SqlParameter
                        param_bonificacion3.ParameterName = "@bonif3"
                        param_bonificacion3.SqlDbType = SqlDbType.Decimal
                        param_bonificacion3.Precision = 18
                        param_bonificacion3.Scale = 2
                        param_bonificacion3.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value)
                        param_bonificacion3.Direction = ParameterDirection.Input

                        Dim param_bonificacion4 As New SqlClient.SqlParameter
                        param_bonificacion4.ParameterName = "@bonif4"
                        param_bonificacion4.SqlDbType = SqlDbType.Decimal
                        param_bonificacion4.Precision = 18
                        param_bonificacion4.Scale = 2
                        param_bonificacion4.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif4).Value)
                        param_bonificacion4.Direction = ParameterDirection.Input

                        Dim param_bonificacion5 As New SqlClient.SqlParameter
                        param_bonificacion5.ParameterName = "@bonif5"
                        param_bonificacion5.SqlDbType = SqlDbType.Decimal
                        param_bonificacion5.Precision = 18
                        param_bonificacion5.Scale = 2
                        param_bonificacion5.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif5).Value)
                        param_bonificacion5.Direction = ParameterDirection.Input

                        Dim param_ganancia As New SqlClient.SqlParameter
                        param_ganancia.ParameterName = "@ganancia"
                        param_ganancia.SqlDbType = SqlDbType.Decimal
                        param_ganancia.Precision = 18
                        param_ganancia.Scale = 2
                        param_ganancia.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value, Double)
                        param_ganancia.Direction = ParameterDirection.Input

                        Dim param_precioventa As New SqlClient.SqlParameter
                        param_precioventa.ParameterName = "@precioventa"
                        param_precioventa.SqlDbType = SqlDbType.Decimal
                        param_precioventa.Precision = 18
                        param_precioventa.Scale = 2
                        param_precioventa.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioMayorista).Value
                        param_precioventa.Direction = ParameterDirection.Input

                        'Dim param_precioxkg As New SqlClient.SqlParameter
                        'param_precioxkg.ParameterName = "@precioxkg"
                        'param_precioxkg.SqlDbType = SqlDbType.Decimal
                        'param_precioxkg.Precision = 18
                        'param_precioxkg.Scale = 2
                        'param_precioxkg.Value = 0 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioxKg).Value
                        'param_precioxkg.Direction = ParameterDirection.Input

                        'Dim param_pesoxmetro As New SqlClient.SqlParameter
                        'param_pesoxmetro.ParameterName = "@pesoxmetro"
                        'param_pesoxmetro.SqlDbType = SqlDbType.Decimal
                        'param_pesoxmetro.Precision = 18
                        'param_pesoxmetro.Scale = 2
                        'param_pesoxmetro.Value = 0 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioxMt).Value
                        'param_pesoxmetro.Direction = ParameterDirection.Input

                        'Dim param_cantxlongitud As New SqlClient.SqlParameter
                        'param_cantxlongitud.ParameterName = "@cantxlongitud"
                        'param_cantxlongitud.SqlDbType = SqlDbType.Decimal
                        'param_cantxlongitud.Precision = 18
                        'param_cantxlongitud.Scale = 2
                        'param_cantxlongitud.Value = 0 'grdItems.Rows(i).Cells(ColumnasDelGridItems.CantxLongitud).Value
                        'param_cantxlongitud.Direction = ParameterDirection.Input

                        Dim param_pesoxunidad As New SqlClient.SqlParameter
                        param_pesoxunidad.ParameterName = "@pesoxunidad"
                        param_pesoxunidad.SqlDbType = SqlDbType.Decimal
                        param_pesoxunidad.Precision = 18
                        param_pesoxunidad.Scale = 2
                        param_pesoxunidad.Value = 0 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PesoxUnidad).Value
                        param_pesoxunidad.Direction = ParameterDirection.Input

                        Dim param_preciolista As New SqlClient.SqlParameter
                        param_preciolista.ParameterName = "@preciolista"
                        param_preciolista.SqlDbType = SqlDbType.Decimal
                        param_preciolista.Precision = 18
                        param_preciolista.Scale = 2
                        param_preciolista.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioListaReal).Value
                        param_preciolista.Direction = ParameterDirection.Input

                        Dim param_preciovtasiniva As New SqlClient.SqlParameter
                        param_preciovtasiniva.ParameterName = "@PrecioCosto"
                        param_preciovtasiniva.SqlDbType = SqlDbType.Decimal
                        param_preciovtasiniva.Precision = 18
                        param_preciovtasiniva.Scale = 2
                        param_preciovtasiniva.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_preciovtasiniva.Direction = ParameterDirection.Input

                        Dim param_preciorevendedor As New SqlClient.SqlParameter
                        param_preciorevendedor.ParameterName = "@PrecioRevendedor"
                        param_preciorevendedor.SqlDbType = SqlDbType.Decimal
                        param_preciorevendedor.Precision = 18
                        param_preciorevendedor.Scale = 2
                        param_preciorevendedor.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioRevendedor).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_preciorevendedor.Direction = ParameterDirection.Input

                        Dim param_precioyamila As New SqlClient.SqlParameter
                        param_precioyamila.ParameterName = "@PrecioYamila"
                        param_precioyamila.SqlDbType = SqlDbType.Decimal
                        param_precioyamila.Precision = 18
                        param_precioyamila.Scale = 2
                        param_precioyamila.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioYamila).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_precioyamila.Direction = ParameterDirection.Input

                        Dim param_preciolista4 As New SqlClient.SqlParameter
                        param_preciolista4.ParameterName = "@PrecioLista4"
                        param_preciolista4.SqlDbType = SqlDbType.Decimal
                        param_preciolista4.Precision = 18
                        param_preciolista4.Scale = 2
                        param_preciolista4.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista4).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_preciolista4.Direction = ParameterDirection.Input

                        Dim param_precioperon As New SqlClient.SqlParameter
                        param_precioperon.ParameterName = "@PrecioPeron"
                        param_precioperon.SqlDbType = SqlDbType.Decimal
                        param_precioperon.Precision = 18
                        param_precioperon.Scale = 2
                        param_precioperon.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioPeron).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_precioperon.Direction = ParameterDirection.Input

                        Dim param_precioperonmayo As New SqlClient.SqlParameter
                        param_precioperonmayo.ParameterName = "@PrecioMayoristaPeron"
                        param_precioperonmayo.SqlDbType = SqlDbType.Decimal
                        param_precioperonmayo.Precision = 18
                        param_precioperonmayo.Scale = 2
                        param_precioperonmayo.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioMayoPeron).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_precioperonmayo.Direction = ParameterDirection.Input

                        Dim param_iva As New SqlClient.SqlParameter
                        param_iva.ParameterName = "@Iva"
                        param_iva.SqlDbType = SqlDbType.Decimal
                        param_iva.Precision = 18
                        param_iva.Scale = 2
                        param_iva.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value
                        param_iva.Direction = ParameterDirection.Input

                        Dim param_ActualizarPrecio As New SqlClient.SqlParameter
                        param_ActualizarPrecio.ParameterName = "@ActualizarPrecio"
                        param_ActualizarPrecio.SqlDbType = SqlDbType.Bit
                        param_ActualizarPrecio.Value = ActualizarPrecio
                        param_ActualizarPrecio.Direction = ParameterDirection.Input
                        '---------------------------------------agregue--------------------------------------------'
                        Dim param_IdStockMov As New SqlClient.SqlParameter
                        param_IdStockMov.ParameterName = "@IdStockMov"
                        param_IdStockMov.SqlDbType = SqlDbType.Int
                        param_IdStockMov.Value = DBNull.Value
                        param_IdStockMov.Direction = ParameterDirection.InputOutput

                        Dim param_Comprob As New SqlClient.SqlParameter
                        param_Comprob.ParameterName = "@Comprob"
                        param_Comprob.SqlDbType = SqlDbType.VarChar
                        param_Comprob.Size = 50
                        param_Comprob.Value = DBNull.Value
                        param_Comprob.Direction = ParameterDirection.InputOutput

                        Dim param_Stock As New SqlClient.SqlParameter
                        param_Stock.ParameterName = "@Stock"
                        param_Stock.SqlDbType = SqlDbType.Decimal
                        param_Stock.Precision = 18
                        param_Stock.Scale = 2
                        param_Stock.Value = DBNull.Value
                        param_Stock.Direction = ParameterDirection.InputOutput
                        '------------------------------------agregue-----------------------------------------'
                        Dim param_res As New SqlClient.SqlParameter
                        param_res.ParameterName = "@res"
                        param_res.SqlDbType = SqlDbType.Int
                        param_res.Value = DBNull.Value
                        param_res.Direction = ParameterDirection.InputOutput

                        Dim param_msg As New SqlClient.SqlParameter
                        param_msg.ParameterName = "@mensaje"
                        param_msg.SqlDbType = SqlDbType.VarChar
                        param_msg.Size = 150
                        param_msg.Value = DBNull.Value
                        param_msg.Direction = ParameterDirection.InputOutput

                        Try
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spRecepciones_Det_Insert2", _
                                                    param_id, param_codigo, param_idAlmacen, param_idRecepcion, param_idmaterial, param_idmaterial_prov, _
                                                    param_Codigo_Mat_Prov, param_idproveedor, param_preciorevendedor, param_precioyamila, param_preciolista4, _
                                                    param_qty, param_idunidad, param_remito, param_useradd, param_Nuevo, _
                                                    param_idordendecompra, param_idordendecompradet, param_material, _
                                                    param_idmoneda, param_bonificacion1, param_bonificacion2, param_bonificacion3, _
                                                    param_bonificacion4, param_bonificacion5, param_ganancia, param_pesoxunidad, param_precioventa, _
                                                    param_preciolista, param_preciovtasiniva, param_iva, param_ActualizarPrecio, _
                                                    param_precioperon, param_precioperonmayo, _
                                                    param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)

                            'MsgBox(param_msg.Value.ToString)

                            res = param_res.Value
                            If Not (param_msg.Value Is System.DBNull.Value) Then
                                msg = param_msg.Value
                            Else
                                msg = ""
                            End If
                            If (res <= 0) Then
                                Exit Do
                            End If
                            Comprob = param_Comprob.Value

                            'If MDIPrincipal.NoActualizar = False Then

                            '    If ValorActual > 0 Then

                            '        Stock = param_Stock.Value
                            '        IdStockMov = param_IdStockMov.Value

                            '        Try
                            '            Dim sqlstring As String
                            '            Dim ds_Empresa As Data.DataSet

                            '            'sqlstring = "update stock set qty = " & ValorActual & ", dateupd=getdate(),userupd= " & UserID & _
                            '            '    " where idmaterial= " & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & _
                            '            '    "  and idunidad= " & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value & _
                            '            '    " and idalmacen = " & cmbAlmacen.SelectedValue

                            '            sqlstring = "exec spStock_Insert '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "', '" & _
                            '                grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value & "', " & cmbAlmacenes.SelectedValue & ", 'I', " & _
                            '                grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value & ", " & Stock & ", " & IdStockMov & ", '" & Comprob & "', " & 4 & ", " & UserID


                            '            'me fijo que sea distinto al salon 25
                            '            If tranWEB.Sql_Get_Value(sqlstring) > 0 And cmbAlmacenes.SelectedValue <> 3 Then
                            '                ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                            '                ds_Empresa.Dispose()

                            '                If cmbAlmacenes.SelectedValue = 1 Then
                            '                    sqlstring = "UPDATE Materiales SET Preciocompra = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value & _
                            '                                ", PrecioCosto = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioMayorista).Value & ", PrecioMayorista = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioRevendedor).Value & _
                            '                                ", Preciolista3 = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioYamila).Value & ", Preciolista4 = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista4).Value & ", ActualizadoLocal = 0 WHERE codigo = '" & param_idmaterial.Value & "'"
                            '                Else
                            '                    sqlstring = "UPDATE Materiales SET Preciocompra = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value & _
                            '                               ", PrecioPeron = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioPeron).Value & ", PrecioMayoristaPeron = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioMayoPeron).Value & ", ActualizadoLocal = 0 WHERE codigo = '" & param_idmaterial.Value & "'"
                            '                End If

                            '                tranWEB.Sql_Set(sqlstring)


                            '                'si el almacen donde estoy es distinto al que estoy cargando y al salon 25 entonces notifico
                            '                If numero_almacen <> cmbAlmacenes.SelectedValue And cmbAlmacenes.SelectedValue <> 3 Then

                            '                    sqlstring = "INSERT INTO [dbo].[transferencias_Recepciones_WEB] ([Codigo],[Fecha],[IDOrigen],[IDDestino],[IDMaterial], " & _
                            '                    "[Qty],[Procesado],[Tipo])" & _
                            '                    "  values ('" & txtCODIGO.Text & "', '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "'," & _
                            '                     cmbAlmacenes.SelectedValue & ", " & cmbAlmacenes.SelectedValue & ",'" & _
                            '                     grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'," & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value, Decimal) & ",0,'R')"

                            '                    tranWEB.Sql_Set(sqlstring)

                            '                    sqlstring = "update notificacionesWEB set Recepciones = 1, Materiales = 1"
                            '                    tranWEB.Sql_Set(sqlstring)

                            '                End If

                            '            End If

                            '        Catch ex As Exception
                            '            'MsgBox(ex.Message)
                            '            MsgBox("No se puede actualizar en la Web el movimiento de stock actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                            '        End Try
                            '    End If

                            'End If




                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                    i = i + 1

                Loop

                AgregarRegistro_Recepciones_Items = res

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

    Private Function AgregarActualizar_Registro_Gastos(ByVal ControlFactura As Boolean, ByVal ControlRemito As Boolean) As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput
                Else
                    param_id.Value = txtIdGasto.Text
                    param_id.Direction = ParameterDirection.Input
                End If

                Dim param_idrecepcion As New SqlClient.SqlParameter
                param_idrecepcion.ParameterName = "@idrecepcion"
                param_idrecepcion.SqlDbType = SqlDbType.BigInt
                param_idrecepcion.Value = txtID.Text
                param_idrecepcion.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_codigo.Value = DBNull.Value
                    param_codigo.Direction = ParameterDirection.InputOutput
                Else
                    param_codigo.Value = txtCODIGO.Text
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
                param_tipogasto.Value = 2
                param_tipogasto.Direction = ParameterDirection.Input

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@idproveedor"
                param_proveedor.SqlDbType = SqlDbType.BigInt
                param_proveedor.Value = IIf(cmbProveedor.SelectedValue = Nothing, txtIdProveedor.Text, cmbProveedor.SelectedValue)
                param_proveedor.Direction = ParameterDirection.Input

                Dim param_IdMoneda As New SqlClient.SqlParameter
                param_IdMoneda.ParameterName = "@idmoneda"
                param_IdMoneda.SqlDbType = SqlDbType.BigInt
                param_IdMoneda.Value = IIf(txtIdMoneda.Text = "", 0, txtIdMoneda.Text)
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
                param_remito.Value = LTrim(RTrim(txtNroRemitoCompleto.Text))
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
                param_cancelada.Value = chkFacturaCancelada.Checked
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
                param_imputarotroperiodo.Value = 0
                param_imputarotroperiodo.Direction = ParameterDirection.Input

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodoimputacion"
                param_periodo.SqlDbType = SqlDbType.DateTime
                param_periodo.Value = DBNull.Value
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

                Dim param_UsuarioGasto As New SqlClient.SqlParameter
                param_UsuarioGasto.ParameterName = "@UsuarioGasto"
                param_UsuarioGasto.SqlDbType = SqlDbType.VarChar
                param_UsuarioGasto.Size = 200
                param_UsuarioGasto.Value = cmbUsuarioGasto.Text
                param_UsuarioGasto.Direction = ParameterDirection.Input

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
                                            param_ptovta, param_nrocompr, param_ptovtaRemito, param_nrocomprRemito, param_UsuarioGasto, _
                                            param_useradd, param_res)

                        txtCODIGO.Text = param_codigo.Value

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

                    txtIdGasto.Text = param_id.Value
                    res = param_res.Value

                    AgregarActualizar_Registro_Gastos = res

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

    Private Function AgregarActualizar_Pago() As Integer
        Dim res As Integer = 0

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            If bolModo = True Then
                param_id.Value = DBNull.Value
            Else
                param_id.Value = IIf(txtidpago.Text = "", DBNull.Value, txtidpago.Text)
            End If
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.InputOutput

            Dim param_idGasto As New SqlClient.SqlParameter
            param_idGasto.ParameterName = "@idgasto"
            param_idGasto.SqlDbType = SqlDbType.BigInt
            param_idGasto.Value = txtIdGasto.Text
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

                Else
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Update", _
                                            param_id, param_codigo, param_idProveedor, param_fecha, param_contado, param_montoContado, _
                                            param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, param_montochequepropio, _
                                            param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                            param_montoiva, param_subtotal, param_total, param_Redondeo, param_nota, _
                                            param_useradd, param_res)

                End If

                txtidpago.Text = param_id.Value

                AgregarActualizar_Pago = param_res.Value

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
            param_idGasto.Value = txtIdGasto.Text
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
                    param_IdGasto.Value = txtIdGasto.Text
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

                    Dim param_MENSAJE As New SqlClient.SqlParameter
                    param_MENSAJE.ParameterName = "@MENSAJE"
                    param_MENSAJE.SqlDbType = SqlDbType.VarChar
                    param_MENSAJE.Size = 200
                    param_MENSAJE.Value = DBNull.Value
                    param_MENSAJE.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Impuestos_Insert", _
                                param_IdGasto, param_CodigoImpuesto, param_NroDocumento, _
                                 param_Monto, param_res)

                            res = param_res.Value

                        Else

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Impuestos_Update", _
                                 param_Id, param_NroDocumento, param_Monto, param_MENSAJE, param_res)

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

    Private Function EliminarRegistro_Recepcion() As Integer
        Dim res As Integer = 0
        Dim msg As String
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

                Dim param_idRecepcion As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_idRecepcion.Value = CType(txtID.Text, Long)
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

    Private Function EliminarRegistro_Gasto() As Integer
        Dim res As Integer = 0

        Try

            Dim param_idGasto As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
            param_idGasto.Value = CType(txtIdGasto.Text, Long)
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

                For i = 0 To 2

                    Dim param_idGasto As New SqlClient.SqlParameter
                    param_idGasto.ParameterName = "@idgasto"
                    param_idGasto.SqlDbType = SqlDbType.BigInt
                    param_idGasto.Value = txtIdGasto.Text
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
                                                 param_idGasto, param_PorcIva, param_MontoIva, _
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
            param_idGasto.Value = CLng(txtIdGasto.Text)
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

    Private Function fila_vacia(ByVal i) As Boolean
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value Is Nothing) Then
            fila_vacia = True
        Else
            fila_vacia = False
        End If
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

    'Private Function LiberarMPNro(ByVal propio As Long) As String
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        Try
    '            connection = SqlHelper.GetConnection(ConnStringSEI)
    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            LiberarMPNro = "error"
    '            Return LiberarMPNro
    '            Exit Function
    '        End Try

    '        Try

    '            Dim param_nro As New SqlClient.SqlParameter
    '            param_nro.ParameterName = "@nro"
    '            param_nro.SqlDbType = SqlDbType.BigInt
    '            param_nro.Value = propio
    '            param_nro.Direction = ParameterDirection.Input

    '            Dim param_userupd As New SqlClient.SqlParameter
    '            param_userupd.ParameterName = "@user"
    '            param_userupd.SqlDbType = SqlDbType.Int
    '            param_userupd.Value = Util.UserID
    '            param_userupd.Direction = ParameterDirection.Input


    '            Dim param_mensaje As New SqlClient.SqlParameter
    '            param_mensaje.ParameterName = "@mensaje"
    '            param_mensaje.SqlDbType = SqlDbType.VarChar
    '            param_mensaje.Size = 500
    '            param_mensaje.Value = DBNull.Value
    '            param_mensaje.Direction = ParameterDirection.InputOutput

    '            Try
    '                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spLiberarStockNro", param_nro, param_userupd, param_mensaje)
    '                LiberarMPNro = param_mensaje.Value
    '                Return LiberarMPNro

    '            Catch ex As Exception
    '                Throw ex
    '            End Try
    '        Finally

    '        End Try
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

    '        LiberarMPNro = "error"
    '        Return LiberarMPNro

    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If

    '    End Try

    'End Function

    ' Capturar los enter del formulario, descartar todos salvo los que 
    ' se dan dentro de la grilla. Es una sobre carga de un metodo existente.

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

        ' Si la tecla presionada es distinta de la tecla Enter,
        ' abandonamos el procedimiento.
        '
        If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)

        ' Igualmente, si el control DataGridView no tiene el foco,
        ' y si la celda actual no está siendo editada,
        ' abandonamos el procedimiento.
        If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then Return MyBase.ProcessCmdKey(msg, keyData)

        ' Obtenemos la celda actual
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        ' Índice de la columna.
        Dim columnIndex As Int32 = cell.ColumnIndex
        ' Índice de la fila.
        Dim rowIndex As Int32 = cell.RowIndex

        Do
            If columnIndex = grdItems.Columns.Count - 1 Then
                If rowIndex = grdItems.Rows.Count - 1 Then
                    ' Seleccionamos la primera columna de la primera fila.
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDRecepcion_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDRecepcion_Det)
                End If
            Else
                ' Seleccionamos la celda de la derecha de la celda actual.
                cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
            End If

            ' establecer la fila y la columna actual
            columnIndex = cell.ColumnIndex
            rowIndex = cell.RowIndex
        Loop While (cell.Visible = False)

        Try
            grdItems.CurrentCell = cell
        Catch ex As Exception

        End Try

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkEliminado.Checked = False

        chkFacturaCancelada.Enabled = True
        chkFacturaCancelada.Checked = False

        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        txtNota.Enabled = True

        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()

        cmbProveedor.Visible = bolModo
        txtProveedor.Visible = Not bolModo

        cmbOrdenDeCompra.Visible = bolModo
        txtOC.Visible = Not bolModo

        txtSubtotal.Text = "0"
        txtSubtotalExento.Text = "0"
        txtMontoIVA10.Text = "0"
        txtMontoIVA21.Text = "0"
        txtMontoIVA27.Text = "0"
        lblMontoIva.Text = "0"
        lblTotal.Text = "0"
        lblImpuestos.Text = "0"
        txtNroRemitoCompleto.Text = ""
        txtNroFacturaCompleto.Text = ""

        chkCargarFactura.Enabled = True
        chkCargarFactura.Checked = False
        chkCargarFactura_CheckedChanged(sender, e)

        txtNroRemitoCompleto.Enabled = True

        txtProveedor.Enabled = True

        LlenarGrid_Impuestos()

        cmbAlmacenes.SelectedIndex = 0
        cmbTipoComprobante.SelectedIndex = 0

        txtCantIVA.Enabled = False
        txtMontoIVA10.Enabled = False
        txtMontoIVA21.Enabled = False
        txtMontoIVA27.Enabled = False

        chkGastos.Enabled = True
        chkGastos.Checked = False

        dtpFECHA.Value = Date.Today
        dtpFECHA.Focus()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If bolModo = False Then
            'MsgBox("No se permite la modificación de una recepción. Para modificar la factura vaya a Administración de Gastos en el menú Contabilidad", MsgBoxStyle.Information, "Control de Acceso")
            If MessageBox.Show("¿Está seguro que desea modificar la Recepción seleccionada (solo Nro de Remito y Fecha)?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim res As Integer, res_item As Integer
        Dim ControlRemito As Boolean
        Dim ControlFactura As Boolean

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

        If cmbProveedor.Text.ToString.Contains("SCHNEI") Or cmbProveedor.Text.ToString.ToUpper.Contains("FINNI") Then

            If txtPtoVtaRemito.Text = "" Then
                Util.MsgStatus(Status1, "Debe cargar el Pto de Vta para continuar", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe cargar el Pto de Vta para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
                txtPtoVtaRemito.Focus()
                Exit Sub
            End If

            If txtNroCompRemito.Text = "" Then
                Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe cargar el Nro de Factura para continuar", My.Resources.Resources.stop_error.ToBitmap, True)
                txtNroCompRemito.Focus()
                Exit Sub
            End If
        Else
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

        End If

        If chkCargarFactura.Checked = True Then

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
            End If

            If txtCantIVA.Value = 1 And (txtSubtotal.Text <> "" And CDbl(txtSubtotal.Text) > 0) Then
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

                If Format(CDbl(txtMontoIVA10.Text), "###0.00") > 0 Then
                    If (txtMontoIVA10.Text - Format(CDbl(txtSubtotal.Text) * 0.105, "###0.00")) > 0.03 Or (txtMontoIVA10.Text - Format(CDbl(txtSubtotal.Text) * 0.105, "###0.00")) < -0.03 Then
                        Util.MsgStatus(Status1, "El monto IVA del 10,5% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El monto IVA del 10,5% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtMontoIVA10.Focus()
                        Exit Sub
                    End If
                End If

                If Format(CDbl(txtMontoIVA21.Text), "###0.00") > 0 Then
                    If (txtMontoIVA21.Text - Format(CDbl(txtSubtotal.Text) * 0.21, "###0.00")) > 0.03 Or (txtMontoIVA21.Text - Format(CDbl(txtSubtotal.Text) * 0.21, "###0.00")) < -0.03 Then
                        Util.MsgStatus(Status1, "El monto IVA del 21% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El monto IVA del 21% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtMontoIVA21.Focus()
                        Exit Sub
                    End If
                End If

                If Format(CDbl(txtMontoIVA27.Text), "###0.00") > 0 Then
                    If (txtMontoIVA27.Text - Format(CDbl(txtSubtotal.Text) * 0.27, "###0.00")) > 0.03 Or (txtMontoIVA27.Text - Format(CDbl(txtSubtotal.Text) * 0.27, "###0.00")) < -0.03 Then
                        Util.MsgStatus(Status1, "El monto IVA del 27% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El monto IVA del 27% no corresponde a la base imponible ingresada. Hay una diferencia superior a los 3 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtMontoIVA27.Focus()
                        Exit Sub
                    End If
                End If
            End If

            Dim a As Double, b As Double, c As Double, d As Double

            a = Format(CDbl(txtMontoIVA10.Text) / 0.105, "###0.00")
            b = Format(CDbl(txtMontoIVA21.Text) / 0.21, "###0.00")
            c = Format(CDbl(txtMontoIVA27.Text) / 0.27, "###0.00")

            d = a + b + c

            d = Format(d - CDbl(txtSubtotal.Text), "###0.00")

            If d > 0.05 Then
                Util.MsgStatus(Status1, "El o los montos imponibles difieren de las alícuotas ingresadas. Hay una diferencia superior a los 5 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El o los montos imponibles difieren de las alícuotas ingresadas. Hay una diferencia superior a los 5 centavos. Por favor, revise estos datos", My.Resources.Resources.stop_error.ToBitmap, True)
                txtMontoIVA21.Focus()
                Exit Sub
            End If

        End If

        If txtTipoMoneda.Text = "Do" And CDbl(txtValorCambio.Text) < BuscarDiferenciaCambio(ConnStringSEI) Then
            If MessageBox.Show("El valor del cambio ingresado está por debajo del valor de cambio estipulado por sistema, considerando un porcentaje de variación. ¿Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If ReglasNegocio() Then
            If bolModo Then
                Verificar_Datos()
            Else
                bolpoliticas = True
            End If
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro_Recepciones(ControlFactura, ControlRemito)
                Select Case res
                    Case -8
                        Util.MsgStatus(Status1, "Ya existe un movimiento que incluye el nro de factura, tipo y cliente que desea ingresar ahora.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "Ya existe un movimiento que incluye el nro de factura, tipo y cliente que desea ingresar ahora.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                        Exit Sub
                    Case -4
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El número de remito ingresado ya existe para el cliente actual.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El número de remito ingresado ya existe para el cliente actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de consumo (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de consumo (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                        If bolModo = True Then
                            res_item = AgregarRegistro_Recepciones_Items(txtID.Text)
                            Select Case res_item
                                Case -7
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo actualizar el precio de un producto", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo actualizar el precio de un producto", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -6
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -5
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -4
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -3
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap)
                                    Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap, True)
                                    Exit Sub
                                Case -2
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap)
                                    Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap, True)
                                    Exit Sub
                                Case -1
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar la recepción (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo registrar la recepción (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case 0
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case Else
                            End Select
                        Else
                            GoTo ContinuarTransaccion

                        End If

                        If chkCargarFactura.Checked = True Then

                            res = AgregarActualizar_Registro_Gastos(ControlFactura, ControlRemito)
                            Select Case res
                                Case -20
                                    Util.MsgStatus(Status1, "El nuevo total ingresado para el gasto es menor que el cargado originalmente. " & vbCrLf & "Debe anular el pago asociado para luego modificar este Gasto.", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "El nuevo total ingresado para el gasto es menor que el cargado originalmente. " & vbCrLf & "Debe anular el pago asociado para luego modificar este Gasto.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Cancelar_Tran()
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
                                    Cancelar_Tran()
                                    Exit Sub
                                Case Else
                                    If bolModo = False Then
                                        EliminarItems_Gastos_Det()
                                    End If
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

                                            If chkFacturaCancelada.Checked = True Then 'And grd.CurrentRow.Cells(14).Value = False Then
                                                res = AgregarActualizar_Pago()
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
                                                                Util.MsgStatus(Status1, "Se guardó correctamente el movimiento", My.Resources.Resources.stop_error.ToBitmap)

                                                                GoTo ContinuarTransaccion

                                                        End Select
                                                End Select
                                            End If
                                    End Select
                            End Select

                        End If

ContinuarTransaccion:
                        'Me fijo si la recepción es para otra sucursales (WEB)
                        'If cmbAlmacenes.Text.Contains("PERON") Then
                        '    If InsertarTransRecepWEB() Then
                        '        'Dim ds_Empresa As Data.DataSet
                        '        ''inserto de manera local el envio
                        '        'ds_Empresa = tranWEB.Sql_Get("Select NroMov from [" & NameTable_TransRecepWEB & "] where nroasociado = '" & txtCODIGO.Text & "' and IDDestino = " & cmbAlmacenes.SelectedValue)
                        '        'If ds_Empresa.Tables(0).Rows.Count > 0 Then
                        '        '    res = Agregar_Registro_TransRecepWEB_Enviado(ds_Empresa.Tables(0).Rows(0).Item(0).ToString)
                        '        '    Select Case res
                        '        '        Case -1
                        '        '            Cancelar_Tran()
                        '        '            Util.MsgStatus(Status1, "No se pudo insertar La Recepción Localmente (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        '        '        Case 0
                        '        '            Cancelar_Tran()
                        '        '            Util.MsgStatus(Status1, "No se pudo registra el movimiento local (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        '        '        Case Else
                        '        '            res_item = AgregarRegistro_TransRecepWEB_Items_Enviado(ds_Empresa.Tables(0).Rows(0).Item(0).ToString)
                        '        '            Select Case res_item
                        '        '                Case -1
                        '        '                    Cancelar_Tran()
                        '        '                    Util.MsgStatus(Status1, "No se pudo insertar La recepción Localmente (Detalle).", My.Resources.Resources.stop_error.ToBitmap)
                        '        '                Case 0
                        '        '                    Cancelar_Tran()
                        '        '                    Util.MsgStatus(Status1, "No se pudo registra el movimiento local (Detalle).", My.Resources.Resources.stop_error.ToBitmap)
                        '        '                Case Else
                        '        '                    Cerrar_Tran()
                        '        '            End Select
                        '        '    End Select
                        '        'Else
                        '        '    Cancelar_Tran()
                        '        '    Util.MsgStatus(Status1, "No se pudo obtener el nro de transferencia de la WEB.", My.Resources.Resources.stop_error.ToBitmap)
                        '        'End If
                        '    Else
                        '        Cancelar_Tran()
                        '        Util.MsgStatus(Status1, "No se pudo registrar la Recepción de la sucursal en la Base de Datos WEB. Por favor intente más tarde.", My.Resources.Resources.stop_error.ToBitmap)
                        '        Exit Sub
                        '    End If
                        'Else
                        Cerrar_Tran()
                        'End If

                        bolModo = False
                        PrepararBotones()

                        SQL = "exec spRecepciones_Select_All  @Eliminado = 0"

                        MDIPrincipal.NoActualizarBase = False
                        btnActualizar_Click(sender, e)

                        Setear_Grilla()
                        chkCargarFactura.Enabled = False
                        cmbProveedor.Visible = False
                        txtProveedor.Visible = True
                        cmbOrdenDeCompra.Visible = False
                        txtOC.Visible = True
                        btnRecibirTodos.Enabled = False

                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

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

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario

        If chkFacturaCancelada.Checked = True Then
            Util.MsgStatus(Status1, "No se puede anular la recepción porque está asociada a un pago que se efectuó. Anule el pago asociado y luego anule esta recepción.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "No se puede anular la recepción porque está asociada a un pago que se efectuó." & vbCrLf & "Anule el pago asociado y luego anule esta recepción.", My.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        Dim res As Integer

        ''If BAJA Then
        If chkEliminado.Checked = False Then
            If MessageBox.Show("Esta acción reversará las Recepciones de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro_Recepcion()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap, True)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap, True)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
                    Case Else
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
                                Cerrar_Tran()
                                PrepararBotones()

                                SQL = "exec spRecepciones_Select_All  @Eliminado = 0"

                                btnActualizar_Click(sender, e)
                                Setear_Grilla()
                                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True, True)
                        End Select
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If
        ''Else
        ''Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        ''Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap, True)
        ''End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes()

        Dim param As New frmParametros
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim codigo_recep As String = ""
        ''En esta Variable le paso la fecha actual

        nbreformreportes = "Recepción de Material"

        param.AgregarParametros("Recepción Nro.:", "STRING", "", False, txtCODIGO.Text.ToString, "", Cnn)

        param.ShowDialog()

        If cerroparametrosconaceptar = True Then
            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR A LA FUNCION..
            codigo_recep = param.ObtenerParametros(0)

            rpt.Recepciones_Maestro_App(codigo_recep, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing
            Cnn = Nothing
        End If



    End Sub

    Private Sub btnLlenarGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlenarGrilla.Click
        'Dim i As Integer

        If bolModo Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
            If Me.cmbOrdenDeCompra.Text <> "" Then
                PrepararGridItems()
                Try
                    LlenarGridItemsPorOC(CType(Me.cmbOrdenDeCompra.SelectedValue, Long))

                    BuscarMoneda_OC()

                    Contar_Filas()

                Catch ex As Exception

                End Try
                btnRecibirTodos.Enabled = True
                With (grdItems)
                    .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
                End With
            End If
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btnRecibirTodos.Enabled = False
        chkCargarFactura.Checked = False
        chkCargarFactura.Enabled = False

        'chkAnulados.Enabled = Not bolModo
        cmbProveedor.Visible = bolModo
        cmbOrdenDeCompra.Visible = bolModo
        txtProveedor.Visible = Not bolModo
        txtOC.Visible = Not bolModo

        cmbGastos.Enabled = False
        chkGastos.Enabled = False

        grd_CurrentCellChanged(sender, e)

        LlenarGrid_IVA(CType(txtIdGasto.Text, Long))



        LlenarGrid_Impuestos()

    End Sub

    Private Sub btnRecibirTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecibirTodos.Click

        'If MessageBox.Show("Está seguro que desea copiar los valores de la columna Cant. Saldo y Precio Pedido a las columnas Cant. Recepc y Precio Real?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        If MessageBox.Show("Está seguro que desea copiar los valores de la columna Cant. Saldo a la columna Cant. Recepc?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim i As Integer

            For i = 0 To grdItems.RowCount - 1
                grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value
                'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioReal).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioPedido).Value
            Next

        End If
    End Sub

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click

        'Try
        '    Dim sqlstring As String = "update [" & NameTable_NotificacionesWEB & "] set BloqueoR = 0"
        '    tranWEB.Sql_Set(sqlstring)


        'Catch ex As Exception

        'End Try

    End Sub

#End Region

#Region "   GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            If e.ColumnIndex = ColumnasDelGridItems.CodUnidad Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_unidad As String, nombre As String = "", codunidad As String = ""
                Dim idunidad As Long

                cod_unidad = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerUnidad_App(cod_unidad, idunidad, nombre, codunidad, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodUnidad).Value = codunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = nombre

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                End If

            End If

            If e.ColumnIndex = ColumnasDelGridItems.CodMoneda Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_moneda As String, nombre As String = "", codmoneda As String = ""
                Dim idmoneda As Long
                Dim valorcambio As Double

                cod_moneda = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerMoneda_App(cod_moneda, idmoneda, nombre, codmoneda, valorcambio, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMoneda).Value = idmoneda
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodMoneda).Value = codmoneda
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Moneda).Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value = valorcambio

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                End If

            End If

            If e.ColumnIndex = ColumnasDelGridItems.Bonif1 Or e.ColumnIndex = ColumnasDelGridItems.Bonif2 Or _
                e.ColumnIndex = ColumnasDelGridItems.Bonif3 Or e.ColumnIndex = ColumnasDelGridItems.Bonif4 Or _
                e.ColumnIndex = ColumnasDelGridItems.Bonif5 Or _
                e.ColumnIndex = ColumnasDelGridItems.Ganancia Or _
                e.ColumnIndex = ColumnasDelGridItems.PrecioListaReal Then

                Dim Bonif1 As Double, Bonif2 As Double, Bonif3 As Double, Bonif4 As Double, Bonif5 As Double
                Dim preciolista As Double
                Dim preciobonif1 As Double = 0, preciobonif2 As Double = 0, preciobonif3 As Double = 0, preciobonif4 As Double = 0, preciobonif5 As Double = 0
                Dim preciosinivabonif As Double = 0

                Dim Ganancia As Double
                Dim cell As DataGridViewCell = grdItems.CurrentCell

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value = 0
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value = 0
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value = 0
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value = 0
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value = 0
                End If

                Ganancia = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value), Double)) / 100

                'precioxmt = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value)
                'precioxkg = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value)
                'cantxlongitud = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantxLongitud).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantxLongitud).Value)
                'pesoxunidad = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value)

                'If precioxkg = 0 And cantxlongitud = 0 And pesoxunidad = 0 And precioxmt = 0 Then

                '    Bonif1 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value), Double)) / 100
                '    Bonif2 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value), Double)) / 100
                '    Bonif3 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value), Double)) / 100
                '    Bonif4 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value), Double)) / 100
                '    Bonif5 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value), Double)) / 100

                'Else

                '    Bonif1 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value), Double)) / 100
                '    Bonif2 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value), Double)) / 100
                '    Bonif3 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value), Double)) / 100
                '    Bonif4 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value), Double)) / 100
                '    Bonif5 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value), Double)) / 100

                '    Dim CalcPesoxUnidad As Double = 0, Calcprecioxunidad As Double = 0, Calcprecioxkg As Double = 0, CalcPrecioxMt As Double = 0

                '    If precioxkg <> 0 Then
                '        Calcprecioxkg = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value = Nothing, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value)
                '        preciobonif1 = precioxkg / Bonif1
                '    Else
                '        CalcPrecioxMt = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value = Nothing, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value)
                '        preciobonif1 = precioxmt / Bonif1
                '    End If

                '    'MsgBox(preciobonif1)

                '    preciobonif1 = preciobonif1 / Bonif2
                '    preciobonif1 = preciobonif1 / Bonif3
                '    preciobonif1 = preciobonif1 / Bonif4
                '    preciobonif1 = preciobonif1 / Bonif5

                '    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value Then
                '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value = 0
                '    End If

                '    preciosinivabonif = preciobonif1 / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value / 100))

                '    'MsgBox(preciobonif1)

                '    'MsgBox(preciosinivabonif)

                '    If precioxkg <> 0 Then
                '        Calcprecioxunidad = preciosinivabonif * IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value)
                '    Else
                '        Calcprecioxunidad = preciosinivabonif * IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantxLongitud).Value)
                '    End If

                '    'MsgBox(Calcprecioxunidad)

                '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = Math.Round(Calcprecioxunidad, 2)

                '    'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value * Ganancia, 2)

                '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value = Math.Round((Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value * Ganancia, 2)) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value, 2)

                '    Exit Sub
                'End If

                Bonif1 = 1 - (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value), Double)) / 100
                Bonif2 = 1 - (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value), Double)) / 100
                Bonif3 = 1 - (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value), Double)) / 100
                Bonif4 = 1 - (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value), Double)) / 100
                Bonif5 = 1 - (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value), Double)) / 100


                preciolista = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioCosto).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioCosto).Value)

                preciobonif1 = preciolista * Bonif1
                preciobonif1 = preciobonif1 * Bonif2
                preciobonif1 = preciobonif1 * Bonif3
                preciobonif1 = preciobonif1 * Bonif4
                preciobonif1 = preciobonif1 * Bonif5

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value = 0
                End If

                preciosinivabonif = preciobonif1 ' / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value / 100))

                'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value = Math.Round(preciosinivabonif * Ganancia, 2)

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value = Math.Round((Math.Round(preciosinivabonif * Ganancia, 2)) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value, 2)

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value <> grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioListaReal).Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Value = Format(100 - Format(CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value * 100 / grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioListaReal).Value), "###0.00"), "###0.00")
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Value = 0
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Value <> 0 Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Style.BackColor = Color.Red
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Style.BackColor = Color.White
                End If

            End If

        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

#End Region








End Class
