Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmRemitos

    Dim bolpoliticas As Boolean
    Dim llenandoCombo As Boolean = False

    Dim permitir_evento_CellChanged As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean
    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    'Dim Cell_X As Integer
    'Dim Cell_Y As Integer

    'Para el combo de busqueda
    Dim ID_Buscado As Long
    Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction

    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    Dim conexionGenerica As SqlClient.SqlConnection = Nothing

    Dim band As Integer
    Dim TipoRemito As String

    Public Origen As Integer ' = 0
    Public Origen_IdCliente As Long
    Public Origen_IdPresupuesto As Long
    Public Origen_Comprador As Boolean
    Public Origen_IdComprador As Long
    Public Origen_Usuario As Boolean
    Public Origen_IdUsuario As Long
    Public Origen_Presupuesto As Integer
    Public Origen_OC As String

    Dim ValorcambioDolar As Double

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IdPres_Ges_Det = 0
        Cod_Pres_Ges_Det = 1
        IDMaterial = 2
        Cod_Material = 3
        Nombre_Material = 4
        IdMoneda = 5
        CodMoneda = 6
        Precio = 7
        Iva = 8
        MontoIva = 9
        Subtotal = 10
        QtyPedida = 11
        Qty = 12
        IDUnidad = 13
        Unidad = 14
        Remito = 15
        Id_Pres = 16
        ID_Pres_Det = 17
        QtySaldo = 18
        Status = 19
        Fechacumplido = 20
        NroDevolucion = 21
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

#Region "   Eventos"

    Private Sub frmPresupuestos_Gestion_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then

                If chkFacturado.Checked = True And chkFacturaAnuladaNC.Checked = False Then
                    lblRemitoFacturado.Text = "REMITO FACTURADO"
                    lblRemitoFacturado2.Text = "No se puede modificar un remito ya facturado"
                    btnEliminar.Enabled = False
                Else
                    If chkFacturado.Checked = True And chkFacturaAnuladaNC.Checked = True Then
                        lblRemitoFacturado.Text = "ANULAR REMITO"
                        lblRemitoFacturado2.Text = "Pertenece a una Factura Anulada"
                        btnEliminar.Enabled = True
                    Else
                        btnEliminar.Enabled = True
                    End If
                End If

                If chkAnulados.Checked = False Then
                    lblRemitoFacturado.Visible = chkFacturado.Checked
                    lblRemitoFacturado2.Visible = chkFacturado.Checked
                Else
                    lblRemitoFacturado.Visible = False
                    lblRemitoFacturado2.Visible = False
                End If
                
                grdItems.Enabled = Not chkFacturado.Checked
                btnAnular.Enabled = Not chkFacturado.Checked

                If chkAnulados.Checked = True Then
                    btnEliminar.Enabled = False
                End If

                BtnFactura.Visible = Not chkFacturado.Checked
                BtnFactura.Enabled = Not bolModo

                If chkRemitoEspecial.Checked = True Then
                    LlenarGridItems_Especiales()
                Else
                    LlenarGridItems()
                End If

            End If
        End If

    End Sub

    Private Sub frmPresupuestos_Gestion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Remito nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Remito?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
            Case Keys.F7 'grabar
                BtnFactura_Click(sender, e)
            Case Keys.F9 'grabar
                btnEntregarTodos_Click(sender, e)
        End Select
    End Sub

    Private Sub frmPresupuestos_Gestion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            conexionGenerica = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        btnEliminar.Text = "Anular Remito"

        band = 0
        configurarform()
        asignarTags()

        LlenarComboClientes()
        LlenarcmbEntregar()
        LlenarcmbMonedas_App(cmbMonedas, ConnStringSEI)

        SQL = "exec spPresupuestos_Gestion_Select_All @Eliminado = 0"

        LlenarGrilla()

        Permitir = True

        CargarCajas()

        PrepararBotones()

        Setear_Grilla()

        If Origen = 0 Then

            If chkRemitoEspecial.Checked = True Then
                LlenarGridItems_Especiales()
            Else
                LlenarGridItems()
            End If

            If bolModo = True Then
                band = 1
                btnNuevo_Click(sender, e)
            Else
                btnLlenarGrilla.Enabled = bolModo
                LlenarcmbClientes_Comprador()
                LlenarcmbClientes_Usuario()
            End If
        Else
            If chkRemitoEspecial.Checked = True Then
                LlenarGridItems_Especiales()
            Else
                LlenarGridItems()
            End If
            band = 1
            btnNuevo_Click(sender, e)
            bolModo = True
            cmbCliente.SelectedValue = Origen_IdCliente
            cmbPresupuestos.SelectedValue = Origen_IdPresupuesto
            chkComprador.Checked = Origen_Comprador
            If Origen_Comprador = True Then
                cmbComprador.SelectedValue = Origen_IdComprador
            End If
            chkUsuario.Checked = Origen_Usuario
            If Origen_Usuario = True Then
                cmbUsuario.SelectedValue = Origen_IdUsuario
            End If
            If Origen_Presupuesto = 0 Then
                chkOCA.Checked = True
            Else
                chkOCA.Checked = False
            End If
            txtNroOC.Text = Origen_OC
            btnEntregarTodos_Click(sender, e)
        End If

        'permitir_evento_CellChanged = True

        cmbCliente.Visible = bolModo
        cmbPresupuestos.Visible = bolModo

        txtCliente.Visible = Not bolModo
        txtPresupuestos.Visible = Not bolModo

        cmbIVA.Visible = bolModo
        txtIVA.Visible = Not bolModo

        grd.Columns(7).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(10).Visible = False
        grd.Columns(11).Visible = False
        grd.Columns(12).Visible = False
        grd.Columns(13).Visible = False
        grd.Columns(14).Visible = False
        grd.Columns(15).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(17).Visible = False
        grd.Columns(18).Visible = False
        grd.Columns(20).Visible = False

        grd.Columns(21).Visible = False
        grd.Columns(22).Visible = False
        grd.Columns(23).Visible = False

        grd.Columns(25).Visible = False
        grd.Columns(27).Visible = False
        grd.Columns(29).Visible = False

        grd.Columns(30).Visible = False
        grd.Columns(31).Visible = False

        grd.Columns(34).Visible = False

        permitir_evento_CellChanged = True

        If Origen = 0 Then
            Try
                If grd.RowCount > 0 Then
                    cmbCliente.SelectedValue = grd.CurrentRow.Cells(23).Value
                    txtIdCliente.Text = grd.CurrentRow.Cells(23).Value
                    cmbComprador.SelectedValue = grd.CurrentRow.Cells(9).Value
                    cmbUsuario.SelectedValue = grd.CurrentRow.Cells(11).Value
                    chkRemitoEspecial.Enabled = False
                End If
            Catch ex As Exception

            End Try

            If chkFacturado.Checked = True And chkFacturaAnuladaNC.Checked = False Then
                lblRemitoFacturado.Text = "REMITO FACTURADO"
                lblRemitoFacturado2.Text = "No se puede modificar un remito ya facturado"
                btnEliminar.Enabled = False
            Else
                If chkFacturado.Checked = True And chkFacturaAnuladaNC.Checked = True Then
                    lblRemitoFacturado.Text = "ANULAR REMITO"
                    lblRemitoFacturado2.Text = "Pertenece a una Factura Anulada"
                    btnEliminar.Enabled = True
                Else
                    btnAnular.Enabled = Not chkFacturado.Checked
                    lblRemitoFacturado.Visible = chkFacturado.Checked
                    lblRemitoFacturado2.Visible = chkFacturado.Checked
                    grdItems.Enabled = Not chkFacturado.Checked
                    BtnFactura.Visible = Not chkFacturado.Checked
                End If
            End If

            grd_CurrentCellChanged(sender, e)

        End If

        BtnFactura.Enabled = Not bolModo

        BuscarOCReq()

        chkRemitoEspecial_CheckedChanged(sender, e)

        chkPresupuesto_CheckedChanged(sender, e)

        band = 1

    End Sub

    'Private Sub txtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
    '    If txtID.Text <> "" And bolModo = False Then
    '        If chkRemitoEspecial.Checked = True Then
    '            LlenarGridItems_Especiales()
    '        Else
    '            LlenarGridItems()
    '        End If
    '    End If
    'End Sub

    Private Sub grdItems_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellDirtyStateChanged
        If grdItems.IsCurrentCellDirty Then
            grdItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub grdItems_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellValueChanged
        Try

            If e.ColumnIndex = ColumnasDelGridItems.Qty And TipoRemito = "ManoObrax" Then

                AgregarRemito_tmp()
                grdItems.Refresh()

                editando_celda = False

            End If

        Catch ex As Exception
            MsgBox("Error en Sub grdRemitos_CellClick", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            If e.ColumnIndex = ColumnasDelGridItems.Qty And TipoRemito = "Matx" Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value Then
                    MsgBox("Debe ingresar un valor en la columna de Cantidad por Entregar", MsgBoxStyle.Information, "Atención")
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value = 0
                    Exit Sub
                Else
                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value > grdItems.CurrentRow.Cells(ColumnasDelGridItems.QtySaldo).Value Then
                        MsgBox("La Cantidad por Entregar no puede ser mayor a la Cantidad en Saldo", MsgBoxStyle.Information, "Atención")
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value = 0
                        Exit Sub
                    End If
                End If

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value, "####0.00")

                ObtenerTotales()

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("Error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        '' referencia a la celda  
        'Dim validar As TextBox = CType(e.Control, TextBox)

        '' agregar el controlador de eventos para el KeyPress  
        'AddHandler validar.KeyPress, AddressOf validarNumerosReales

        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Qty Then
            AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
        Else
            AddHandler e.Control.KeyPress, AddressOf NoValidar
        End If
        'End If


    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtID.KeyPress, txtCODIGO.KeyPress
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

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        'Borrar la fila actual
        If cell.RowIndex <> 0 Then
            grdItems.Rows.RemoveAt(cell.RowIndex)
        End If
    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            txtIdCliente.Text = cmbCliente.SelectedValue
            LimpiarGridItems(grdItems)
            LlenarcmbClientes_Comprador()
            LlenarcmbClientes_Usuario()
            LlenarComboPresupuestos()
            cmbPresupuestos_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub cmbPresupuestos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPresupuestos.SelectedIndexChanged
        If band = 1 Then
            If chkPresupuesto.Checked = True Then
                txtIdPresupuesto.Text = cmbPresupuestos.SelectedValue
            Else
                txtIdPresupuesto.Text = "0"
            End If

            If cmbPresupuestos.Text.Contains("Matx") Then
                TipoRemito = "Matx"
            ElseIf cmbPresupuestos.Text.Contains("ManoObrax") Then
                TipoRemito = "ManoObrax"
            End If
            LlenarComboIVA()
            If chkRemitoEspecial.Checked = False Then
                btnLlenarGrilla_Click(sender, e)
            End If
            BuscarOCReq()
            txtSubtotalDO.Text = "0"
            txtMontoIvaDO.Text = "0"
            txtTotalDO.Text = "0"
        End If
    End Sub

    Private Sub cmbIVA_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIVA.SelectedIndexChanged
        If band = 1 Then
            btnLlenarGrilla_Click(sender, e)
            txtIdPresupuesto.Text = cmbPresupuestos.SelectedValue
            txtSubtotalDO.Text = "0"
            txtMontoIvaDO.Text = "0"
            txtTotalDO.Text = "0"
        End If
    End Sub

    Private Sub chkUsuario_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUsuario.CheckedChanged
        cmbUsuario.Enabled = chkUsuario.Checked
    End Sub

    Private Sub chkComprador_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComprador.CheckedChanged
        cmbComprador.Enabled = chkComprador.Checked
    End Sub

    Private Sub chkEntrega_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEntrega.CheckedChanged
        cmbEntregaren.Enabled = chkEntrega.Checked
        If chkEntrega.Checked = False Then
            cmbEntregaren.Text = ""
        End If
    End Sub

    Private Sub chkAnulados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnulados.CheckedChanged
        btnGuardar.Enabled = Not chkAnulados.Checked
        btnNuevo.Enabled = Not chkAnulados.Checked
        btnCancelar.Enabled = Not chkAnulados.Checked

        If chkAnulados.Checked = True Then
            btnEliminar.Enabled = False
            SQL = "exec spPresupuestos_Gestion_Select_All @Eliminado = 1"
        Else
            btnEliminar.Enabled = True
            SQL = "exec spPresupuestos_Gestion_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

    End Sub

    Private Sub PicClientes_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PicAnularRemito.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.PicAnularRemito, "El sistema permite la anulación física o lógica del Remito. " & vbCrLf & "La primera implica que el nro de remito ingresado no podrá volver a ser utilizado, " & vbCrLf & "mientras que con la segunda, el nro de remito sigue disponible." & vbCrLf & "Para la eliminación lógica del registro utilice la opción Anular Remito en la parte superior de la pantalla")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Private Sub picEntregartodos_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picEntregartodos.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.picEntregartodos, "Permite copiar los valores de la columna Cant. Pedida a la columna Cant. a Entregar.")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            Try
                If grd.RowCount > 0 Then
                    'cmbCliente.SelectedValue = grd.CurrentRow.Cells(23).Value
                    txtIdCliente.Text = grd.CurrentRow.Cells(23).Value
                    cmbComprador.SelectedValue = grd.CurrentRow.Cells(9).Value
                    cmbUsuario.SelectedValue = grd.CurrentRow.Cells(11).Value

                    BuscarOCReq()

                End If
            Catch ex As Exception
                'txtIdCliente.Text = grd.CurrentRow.Cells(23).Value
            End Try
        End If
    End Sub

    Private Sub chkRemitoEspecial_CheckedChanged(sender As Object, e As EventArgs) Handles chkRemitoEspecial.CheckedChanged
        cmbDescripcionRemito.Enabled = chkRemitoEspecial.Checked
        lblDescripcionRemito.Enabled = chkRemitoEspecial.Checked
        txtSubtotalItem.Enabled = chkRemitoEspecial.Checked
        lblSubtotalItem.Enabled = chkRemitoEspecial.Checked
        grdItemsEspeciales.Visible = chkRemitoEspecial.Checked
        grdItems.Visible = Not chkRemitoEspecial.Checked

        'chkParaFacturar.Enabled = chkRemitoEspecial.Checked

        'If chkRemitoEspecial.Checked = False Then
        '    chkParaFacturar.Checked = True
        'End If

    End Sub

    Private Sub txtSubtotalItem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSubtotalItem.KeyDown
        If e.KeyCode = Keys.Enter Then

            If cmbDescripcionRemito.Text = "" Then
                Util.MsgStatus(Status1, "Debe ingresar el texto para la descripción del Remito.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar el texto para la descripción del Remito.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If txtIdPresupuesto.Text = "" And chkPresupuesto.Checked = True Then
                Util.MsgStatus(Status1, "Debe seleccionar un Presupuesto para el Remito actual.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe seleccionar un Presupuesto para el Remito actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If


            If bolModo = True Then
                If cmbIVA.Text = "" Then
                    Util.MsgStatus(Status1, "Debe ingresar el IVA para el remito actual.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Debe ingresar el IVA para el remito actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            Else
                If txtIVA.Text = "" Then
                    Util.MsgStatus(Status1, "Debe ingresar el IVA para el remito actual.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Debe ingresar el IVA para el remito actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            If txtSubtotalItem.Text = "" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad para la descripción del Remito, puede ser 0 (Cero).", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad para la descripción del Remito, puede ser 0 (Cero).", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            Dim i As Integer
            For i = 0 To grdItemsEspeciales.RowCount - 1
                If cmbDescripcionRemito.Text = grdItemsEspeciales.Rows(i).Cells(0).Value Then
                    Util.MsgStatus(Status1, "La descripción '" & cmbDescripcionRemito.Text & "' esta repetida en la fila: " & (i + 1).ToString & ".", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            Next

            grdItemsEspeciales.Rows.Add(cmbDescripcionRemito.Text, txtSubtotalItem.Text)

            CalcularMontos()

            cmbDescripcionRemito.Text = ""
            txtSubtotalItem.Text = ""
            cmbDescripcionRemito.Focus()

        End If

    End Sub

    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItemsEspeciales.CellClick
        If e.ColumnIndex = 2 Then 'Eliminar Item
            If MessageBox.Show("Está seguro que desea Eliminar el Item del Remito?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItemsEspeciales.Rows.RemoveAt(e.RowIndex)

                CalcularMontos()

            End If
        End If
    End Sub


#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Gestión de Remitos"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        'Dim p As New Size(GroupBox1.Size.Width, 220) 'AltoMinimoGrilla)
        'Me.grd.Size = New Size(p)

        Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)

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
        cmbCliente.Tag = "3"
        txtCliente.Tag = "3"

        txtIdCliente.Tag = "23"

        cmbPresupuestos.Tag = "4"
        txtPresupuestos.Tag = "4"
        txtNroOC.Tag = "5"
        txtRemito.Tag = "6"
        txtNota.Tag = "7"
        cmbComprador.Tag = "10"
        cmbUsuario.Tag = "12"
        chkEntrega.Tag = "13"
        cmbEntregaren.Tag = "14"
        chkUsuario.Tag = "15"
        chkComprador.Tag = "16"
        txtfactura.Tag = "17"
        txtIdPresupuesto.Tag = "18"
        chkFacturado.Tag = "19"
        chkEntregaPendiente.Tag = "20"
        chkOCA.Tag = "21"
        txtIVA.Tag = "22"

        txtSubtotalDO.Tag = "24"
        txtMontoIvaDO.Tag = "26"
        txtTotalDO.Tag = "28"

        chkRemitoEspecial.Tag = "30"
        chkParaFacturar.Tag = "31"

        chkFacturaAnuladaNC.Tag = "32"

        chkPresupuesto.Tag = "33"

        txtIdMoneda.Tag = "34"
        cmbMonedas.Tag = "35"

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

        If bolModo = True Then
            If cmbPresupuestos.Text = "" And chkPresupuesto.Checked = True Then
                Util.MsgStatus(Status1, "Debe seleccionar un presupuesto antes de continuar.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "Debe seleccionar un presupuesto antes de continuar.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'verificar que no hay nada en la grilla sin datos
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If chkRemitoEspecial.Checked = False And TipoRemito = "Matx" Then
            j = grdItems.RowCount - 1
            filas = 0
            For i = 0 To j
                If fila_vacia(i) Then
                    Try
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value = 0
                    Catch ex As Exception

                    End Try
                Else
                    filas = filas + 1
                    Try
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value < 0 Then
                            Util.MsgStatus(Status1, "La cantidad a Entregar debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "La cantidad a Entregar debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    Catch ex As Exception
                        Util.MsgStatus(Status1, "La cantidad a Entregar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "La cantidad a Entregar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End Try

                    If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Is DBNull.Value Then
                        If bolModo = True Then
                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                                Util.MsgStatus(Status1, "La cantidad a Entregar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "La cantidad a Entregar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                                Exit Sub
                            End If
                        Else
                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyPedida).Value Then
                                Util.MsgStatus(Status1, "La cantidad a Entregar no debe ser mayor a la cantidad solicitada originalmente en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "La cantidad a Entregar no debe ser mayor a la cantidad solicitada originalmente en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                                Exit Sub
                            End If
                        End If

                    End If

                End If

            Next i

        End If

        Dim buscandoalgunmov As Boolean = False

        If TipoRemito = "Matx" Then
            If chkRemitoEspecial.Checked = False Then
                For i = 0 To grdItems.RowCount - 1
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value > 0 Then
                        buscandoalgunmov = True
                        filas = 1
                        Exit For
                    End If
                Next
            Else
                If grdItemsEspeciales.RowCount > 0 Then
                    buscandoalgunmov = True
                    filas = 1
                End If
            End If
        Else
            If chkRemitoEspecial.Checked = False Then
                If bolModo = False Then
                    buscandoalgunmov = True
                    filas = 1
                Else
                    For i = 0 To grdItems.RowCount - 1
                        If CBool(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value) = True Then
                            buscandoalgunmov = True
                            filas = 1
                            Exit For
                        End If
                    Next
                End If
            Else
                If grdItemsEspeciales.RowCount > 0 Then
                    buscandoalgunmov = True
                    filas = 1
                End If
            End If


        End If

        If buscandoalgunmov = False Then
            Util.MsgStatus(Status1, "No realizó ningún movimiento dentro de la grilla. Por favor, verifique antes de guarda.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No realizó ningún movimiento dentro de la grilla. Por favor, verifique antes de guarda.", My.Resources.Resources.alert.ToBitmap, True)
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

    'Private Sub validarNumerosReales(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    'Controlar que el caracter ingresado sea un  numero real
    '    Dim caracter As Char = e.KeyChar
    '    Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = sender

    '    If caracter = "." Or caracter = "," Then
    '        If CuentaAparicionesDeCaracter(obj.Text, ".") = 0 Then
    '            If CuentaAparicionesDeCaracter(obj.Text, ",") = 0 Then
    '                e.Handled = False ' dejar escribir
    '            Else
    '                e.Handled = True 'no deja escribir
    '            End If
    '        Else
    '            e.Handled = True ' no deja escribir
    '        End If
    '    Else
    '        If caracter = "-" And obj.Text.Trim <> "" Then
    '            If CuentaAparicionesDeCaracter(obj.Text, caracter) < 1 Then
    '                obj.Text = "-" + obj.Text
    '                e.Handled = True ' no dejar escribir
    '            Else
    '                obj.Text = Mid(obj.Text, 2, Len(obj.Text))
    '                e.Handled = True ' no dejar escribir
    '            End If
    '        Else
    '            If Char.IsNumber(caracter) Or caracter = ChrW(Keys.Back) Or caracter = ChrW(Keys.Delete) Or caracter = ChrW(Keys.Enter) Then
    '                e.Handled = False 'dejo escribir
    '            Else
    '                e.Handled = True ' no dejar escribir
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub validarNumeros(ByVal sender As Object, _
    '   ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    'Controlar que el caracter ingresado sea un  numero
    '    Dim caracter As Char = e.KeyChar
    '    If caracter = "." Or caracter = "," Then
    '        e.Handled = True ' no dejar escribir
    '    Else
    '        If Char.IsNumber(caracter) Or caracter = ChrW(Keys.Back) Or caracter = ChrW(Keys.Delete) Or caracter = ChrW(Keys.Enter) Then
    '            e.Handled = False 'dejo escribir
    '        Else
    '            e.Handled = True ' no dejar escribir
    '        End If
    '    End If
    'End Sub

    'Private Sub validar_NumerosReales2(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    ' obtener indice de la columna  
    '    Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

    '    ' comprobar si la celda en edición corresponde a la columna 1 o 3  
    '    If columna = 7 Then

    '        Dim caracter As Char = e.KeyChar

    '        ' referencia a la celda  
    '        Dim txt As TextBox = CType(sender, TextBox)

    '        ' comprobar si es un número con isNumber, si es el backspace, si el caracter  
    '        ' es el separador decimal, y que no contiene ya el separador  
    '        If (Char.IsNumber(caracter)) Or _
    '           (caracter = ChrW(Keys.Back)) Or _
    '           (caracter = ".") And _
    '           (txt.Text.Contains(".") = False) Then
    '            e.Handled = False
    '        Else
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub

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
            .AllowUserToAddRows = False
            '.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material          
        End With
    End Sub

    Private Sub LlenarGridItems()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        If txtPresupuestos.Text.Contains("Matx") Then
            TipoRemito = "Matx"
        ElseIf txtPresupuestos.Text.Contains("ManoObrax") Then
            TipoRemito = "ManoObrax"
        End If

        If txtID.Text = "" Then
            SQL = "exec spPresupuestos_Gestion_Det_Select_By_IDPresupuesto_Gestion @idPresupuesto_Gestion = 0, @Tipo = '" & TipoRemito & "'"
        Else
            SQL = "exec spPresupuestos_Gestion_Det_Select_By_IDPresupuesto_Gestion @idPresupuesto_Gestion = " & txtID.Text & ", @Tipo = '" & TipoRemito & "'"
        End If

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IdPres_Ges_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Pres_Ges_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True 'Material
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

        grdItems.Columns(ColumnasDelGridItems.IdMoneda).Visible = False

        grdItems.Columns(ColumnasDelGridItems.CodMoneda).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.CodMoneda).Width = 60
        grdItems.Columns(ColumnasDelGridItems.CodMoneda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Precio).ReadOnly = True 'PRECIO
        grdItems.Columns(ColumnasDelGridItems.Precio).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Precio).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Iva).ReadOnly = True 'PRECIO
        grdItems.Columns(ColumnasDelGridItems.Iva).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.MontoIva).ReadOnly = True 'PRECIO
        grdItems.Columns(ColumnasDelGridItems.MontoIva).Width = 70
        grdItems.Columns(ColumnasDelGridItems.MontoIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Subtotal).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Subtotal).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Subtotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).Visible = True
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).Width = 70
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).Visible = False
        End If

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.Qty).Visible = True
            grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
            grdItems.Columns(ColumnasDelGridItems.Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.Qty).Visible = False
        End If

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False 'IDUNIDAD

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = True
            grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 70
            grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Unidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = False
        End If

        grdItems.Columns(ColumnasDelGridItems.Remito).Visible = False 'REMITO

        grdItems.Columns(ColumnasDelGridItems.Id_Pres).Visible = False

        grdItems.Columns(ColumnasDelGridItems.ID_Pres_Det).Visible = False

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).Visible = True
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).ReadOnly = True 'qtysaldo
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).Width = 70
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).Visible = False
        End If

        grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True 'status
        grdItems.Columns(ColumnasDelGridItems.Status).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Fechacumplido).ReadOnly = True 'status
        grdItems.Columns(ColumnasDelGridItems.Fechacumplido).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Fechacumplido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.NroDevolucion).Visible = False

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
        SQL = "exec spPresupuestos_Gestion_Select_All @Eliminado = 0"

    End Sub

    Private Sub LlenarGridItemsPorPresup(ByVal idPresupuesto As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        Dim IVA As Double

        If cmbIVA.Text.ToString = "" Then
            IVA = 0
        Else
            IVA = CDbl(cmbIVA.Text)
        End If

        If cmbPresupuestos.Text.Contains("Matx") Then
            TipoRemito = "Matx"
        ElseIf cmbPresupuestos.Text.Contains("ManoObrax") Then
            TipoRemito = "ManoObrax"
        End If

        SQL = "exec spPresupuestos_Gestion_Det_Select_By_IDPresupuesto @IdPresupuesto = " & idPresupuesto & ", @IVA = " & IVA & ", @Tipo = '" & TipoRemito & "'"

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IdPres_Ges_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Pres_Ges_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True '3
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True '4
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

        grdItems.Columns(ColumnasDelGridItems.IdMoneda).Visible = False

        grdItems.Columns(ColumnasDelGridItems.CodMoneda).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.CodMoneda).Width = 60
        grdItems.Columns(ColumnasDelGridItems.CodMoneda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Precio).ReadOnly = True  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.Precio).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Precio).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Iva).ReadOnly = True  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.Iva).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.MontoIva).ReadOnly = True  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.MontoIva).Width = 70
        grdItems.Columns(ColumnasDelGridItems.MontoIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Subtotal).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Subtotal).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Subtotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).Visible = True
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).Width = 70
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.QtyPedida).Visible = False
        End If

        grdItems.Columns(ColumnasDelGridItems.Precio).ReadOnly = True  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.Precio).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Precio).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = True
            grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 70
            grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Unidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = False
        End If

        grdItems.Columns(ColumnasDelGridItems.Remito).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Id_Pres).Visible = False

        grdItems.Columns(ColumnasDelGridItems.ID_Pres_Det).Visible = False 'sald

        If TipoRemito = "Matx" Then
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).Visible = True
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).ReadOnly = True 'saldo
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).Width = 70
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.QtySaldo).Visible = False
        End If

        grdItems.Columns(ColumnasDelGridItems.Status).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Fechacumplido).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Fechacumplido).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.NroDevolucion).Visible = False

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
        SQL = "exec spPresupuestos_Gestion_Select_All @Eliminado = 0"

        Dim i As Integer

        For i = 0 To grdItems.RowCount - 1
            grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Style.BackColor = Color.LightBlue
        Next

    End Sub

    Private Sub LlenarGridItems_Especiales()

        grdItemsEspeciales.Rows.Clear()

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

            sqltxt2 = "exec spPresupuestos_Gestion_Especiales_Det_Select_By_IdPresGes @IdPresGes = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItemsEspeciales.Rows.Add(dt.Rows(i)("DescripcionRemito").ToString(), dt.Rows(i)("Subtotal").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub GetDatasetItems()
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            ds_2 = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, SQL)
            ds_2.Dispose()

            grdItems.DataSource = ds_2.Tables(0).DefaultView

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
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

    Private Sub LlenarComboClientes()
        Dim ds_Cli As Data.DataSet
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            If chkPresupuesto.Checked = True Then
                ds_Cli = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select DISTINCT c.id, c.nombre " & _
                                " from Presupuestos p JOIN Clientes c ON c.id = p.idcliente where p.eliminado=0 and Status = 'P' order by c.nombre ") 'AND Status = 'P' 
            Else
                ds_Cli = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select DISTINCT c.id, c.nombre " & _
                                " from Clientes c  where c.eliminado=0 order by c.nombre ") 'AND Status = 'P' 
            End If
            
            ds_Cli.Dispose()

            With Me.cmbCliente
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "id"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
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
            'If Not connection Is Nothing Then
            ' CType(connection, IDisposable).Dispose()
            ' End If
        End Try

    End Sub

    Private Sub LlenarComboPresupuestos()
        Dim ds_Presup As Data.DataSet
        Dim Consulta As String

        Try

            Consulta = "SELECT * FROM (select p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (Matx)') as Codigo " & _
                                " from Presupuestos p where OfertaComercial = 0 and MANOOBRA = 0 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & " AND status = 'P' AND p.eliminado=0" & _
                                " UNION ALL select p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (ManoObrax)') as Codigo " & _
                                " from Presupuestos p where OfertaComercial = 1 and MANOOBRA = 1 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & " AND status = 'P' AND p.eliminado=0" & _
                                " UNION ALL select DISTINCT p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (MatxOferta)') as Codigo " & _
                                " from Presupuestos p JOIN Presupuestos_Det pd ON pd.IDPresupuesto = p.id " & _
                                " where OfertaComercial = 1 and MANOOBRA = 0 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & _
                                " AND pd.status = 'P' AND p.eliminado=0" & _
                                " UNION ALL select DISTINCT p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (ManoObraxOferta)') as Codigo " & _
                                " from Presupuestos p JOIN Presupuestos_OfertasComerciales poc ON poc.IdPresupuesto = p.ID " & _
                                " JOIN Presupuestos_OfertasComerciales_Det pocd ON pocd.IdPresupuesto_OfertaComercial = poc.Id " & _
                                " where OfertaComercial = 1 and MANOOBRA = 0 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & _
                                " AND pocd.status = 'P' AND p.eliminado=0 ) tt ORDER BY Id Desc"

            ds_Presup = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, Consulta)

            ds_Presup.Dispose()

            band = 0
            With Me.cmbPresupuestos
                .DataSource = ds_Presup.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
            End With
            band = 1

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

    Private Sub LlenarComboIVA()
        Dim ds_IVA As Data.DataSet

        Try
            If chkPresupuesto.Checked = True Then

                If TipoRemito = "Matx" Then
                    ds_IVA = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select DISTINCT pd.IVA " & _
                        " from Presupuestos p JOIN Presupuestos_det pd ON pd.idpresupuesto = p.id " & _
                        " where IdMaterial > 0 and pd.status = 'P' and pd.eliminado = 0 AND P.id =  " & txtIdPresupuesto.Text)  'cmbPresupuestos.SelectedValue) 'AND Status = 'P' 
                Else
                    ds_IVA = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select IVA " & _
                        " from Presupuestos_OfertasComerciales where idPresupuesto =  " & txtIdPresupuesto.Text) 'cmbPresupuestos.SelectedValue) 'AND Status = 'P' 
                End If

                ds_IVA.Dispose()

                band = 0
                With Me.cmbIVA
                    .DataSource = ds_IVA.Tables(0).DefaultView
                    .DisplayMember = "IVA"
                End With
            Else

                ds_IVA = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select 21.00 AS IVA UNION Select 10.50 AS IVA")  'cmbPresupuestos.SelectedValue) 'AND Status = 'P' 

                ds_IVA.Dispose()

                band = 0
                With Me.cmbIVA
                    .DataSource = ds_IVA.Tables(0).DefaultView
                    .DisplayMember = "IVA"
                End With

            End If

            band = 1


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

    Private Sub BuscarOCReq()

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idPresupuesto"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtIdPresupuesto.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_TotalPres As New SqlClient.SqlParameter
                param_TotalPres.ParameterName = "@TotalPres"
                param_TotalPres.SqlDbType = SqlDbType.Decimal
                param_TotalPres.Precision = 18
                param_TotalPres.Scale = 2
                param_TotalPres.Value = 0
                param_TotalPres.Direction = ParameterDirection.InputOutput

                Dim param_TotalRem As New SqlClient.SqlParameter
                param_TotalRem.ParameterName = "@TotalRem"
                param_TotalRem.SqlDbType = SqlDbType.Decimal
                param_TotalRem.Precision = 18
                param_TotalRem.Scale = 2
                param_TotalRem.Value = 0
                param_TotalRem.Direction = ParameterDirection.InputOutput

                Dim param_TotalFac As New SqlClient.SqlParameter
                param_TotalFac.ParameterName = "@TotalFac"
                param_TotalFac.SqlDbType = SqlDbType.Decimal
                param_TotalFac.Precision = 18
                param_TotalFac.Scale = 2
                param_TotalFac.Value = 0
                param_TotalFac.Direction = ParameterDirection.InputOutput

                Dim param_TotalDifRem As New SqlClient.SqlParameter
                param_TotalDifRem.ParameterName = "@TotalDifRem"
                param_TotalDifRem.SqlDbType = SqlDbType.Decimal
                param_TotalDifRem.Precision = 18
                param_TotalDifRem.Scale = 2
                param_TotalDifRem.Value = 0
                param_TotalDifRem.Direction = ParameterDirection.InputOutput

                Dim param_Moneda As New SqlClient.SqlParameter
                param_Moneda.ParameterName = "@Moneda"
                param_Moneda.SqlDbType = SqlDbType.VarChar
                param_Moneda.Size = 50
                param_Moneda.Value = ""
                param_Moneda.Direction = ParameterDirection.InputOutput

                Dim param_NroOC As New SqlClient.SqlParameter
                param_NroOC.ParameterName = "@NroOC"
                param_NroOC.SqlDbType = SqlDbType.VarChar
                param_NroOC.Size = 300
                param_NroOC.Value = ""
                param_NroOC.Direction = ParameterDirection.InputOutput

                Dim param_ContactoComp As New SqlClient.SqlParameter
                param_ContactoComp.ParameterName = "@ContactoComp"
                param_ContactoComp.SqlDbType = SqlDbType.VarChar
                param_ContactoComp.Size = 100
                param_ContactoComp.Value = ""
                param_ContactoComp.Direction = ParameterDirection.InputOutput

                Dim param_ContactoUsu As New SqlClient.SqlParameter
                param_ContactoUsu.ParameterName = "@ContactoUsu"
                param_ContactoUsu.SqlDbType = SqlDbType.VarChar
                param_ContactoUsu.Size = 100
                param_ContactoUsu.Value = ""
                param_ContactoUsu.Direction = ParameterDirection.InputOutput

                Dim param_Entregaren As New SqlClient.SqlParameter
                param_Entregaren.ParameterName = "@Entregaren"
                param_Entregaren.SqlDbType = SqlDbType.Bit
                param_Entregaren.Value = False
                param_Entregaren.Direction = ParameterDirection.InputOutput

                Dim param_SitioEntrega As New SqlClient.SqlParameter
                param_SitioEntrega.ParameterName = "@SitioEntrega"
                param_SitioEntrega.SqlDbType = SqlDbType.VarChar
                param_SitioEntrega.Size = 500
                param_SitioEntrega.Value = ""
                param_SitioEntrega.Direction = ParameterDirection.InputOutput

                Dim param_Usuario As New SqlClient.SqlParameter
                param_Usuario.ParameterName = "@Usuario"
                param_Usuario.SqlDbType = SqlDbType.Bit
                param_Usuario.Value = False
                param_Usuario.Direction = ParameterDirection.InputOutput

                Dim param_Comprador As New SqlClient.SqlParameter
                param_Comprador.ParameterName = "@Comprador"
                param_Comprador.SqlDbType = SqlDbType.Bit
                param_Comprador.Value = False
                param_Comprador.Direction = ParameterDirection.InputOutput

                Dim param_IdContactoUsu As New SqlClient.SqlParameter
                param_IdContactoUsu.ParameterName = "@IdContactoUsu"
                param_IdContactoUsu.SqlDbType = SqlDbType.BigInt
                param_IdContactoUsu.Value = 0
                param_IdContactoUsu.Direction = ParameterDirection.InputOutput

                Dim param_IdContactoCom As New SqlClient.SqlParameter
                param_IdContactoCom.ParameterName = "@IdContactoCom"
                param_IdContactoCom.SqlDbType = SqlDbType.BigInt
                param_IdContactoCom.Value = 0
                param_IdContactoCom.Direction = ParameterDirection.InputOutput

                Dim param_IdMoneda As New SqlClient.SqlParameter
                param_IdMoneda.ParameterName = "@IdMoneda"
                param_IdMoneda.SqlDbType = SqlDbType.BigInt
                param_IdMoneda.Value = 0
                param_IdMoneda.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPresupuestos_Gestion_BuscarInfoPres", param_id, _
                                              param_TotalPres, param_TotalRem, param_TotalFac, param_TotalDifRem, param_Moneda, param_NroOC, _
                                              param_ContactoComp, param_ContactoUsu, param_Entregaren, param_SitioEntrega, param_Usuario, _
                                              param_Comprador, param_IdContactoCom, param_IdContactoUsu, param_IdMoneda)

                    lblPresupuesto.Text = param_TotalPres.Value
                    lblMoneda.Text = param_Moneda.Value
                    lblRemito.Text = param_TotalRem.Value
                    lblFacturado.Text = param_TotalFac.Value
                    lblDiferenciaRemito.Text = param_TotalDifRem.Value
                    txtNroOC.Text = param_NroOC.Value
                    txtIdMoneda.Text = param_IdMoneda.Value

                    cmbEntregaren.Text = param_SitioEntrega.Value
                    chkEntrega.Checked = param_Entregaren.Value
                    chkUsuario.Checked = param_Usuario.Value
                    chkComprador.Checked = param_Comprador.Value
                    cmbComprador.SelectedValue = param_IdContactoCom.Value
                    cmbUsuario.SelectedValue = param_IdContactoUsu.Value

                    Dim SENDER As Object = Nothing
                    Dim E As System.EventArgs = Nothing

                    chkUsuario_CheckedChanged(SENDER, E)

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

    Private Sub LlenarcmbClientes_Comprador()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT CC.ID as IdComprador, NOMBRE_CONTACTO as Comprador FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & CType(cmbCliente.SelectedValue, Long) & " order by NOMBRE_CONTACTO")

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT CC.ID as IdComprador, NOMBRE_CONTACTO as Comprador FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & CLng(txtIdCliente.Text) & " order by  NOMBRE_CONTACTO asc")

            ds.Dispose()

            With cmbComprador
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Comprador"
                .ValueMember = "IdComprador"
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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub LlenarcmbClientes_Usuario()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT CC.ID as IdUsuario, NOMBRE_CONTACTO as Usuario FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & CType(cmbCliente.SelectedValue, Long) & " order by NOMBRE_CONTACTO")
            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT CC.ID as IdUsuario, NOMBRE_CONTACTO as Usuario FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & CType(txtIdCliente.Text, Long) & " order by NOMBRE_CONTACTO")
            ds.Dispose()

            With cmbUsuario
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Usuario"
                .ValueMember = "IdUsuario"
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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub LlenarcmbEntregar()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct SitioEntrega FROM Presupuestos ORDER BY SITIOENTREGA")
            ds.Dispose()

            With cmbEntregaren
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "SitioEntrega"
                '.ValueMember = "IdUsuario"
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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub Imprimir(ByVal Codigo As String)
        nbreformreportes = "Remito"

        Dim Rpt As New frmReportes

        Rpt.NombreArchivoPDF = BuscarNombreArchivo(Codigo)

        If MessageBox.Show("Desea imprimir 2 Copias directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Material", True)
        Else
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Material", False)
        End If

        'cnn = Nothing

    End Sub

    Private Sub ObtenerTotales()

        Dim j As Integer = 0

        txtSubtotalDO.Text = "0"
        txtMontoIvaDO.Text = "0"
        txtTotalDO.Text = "0"

        For j = 0 To grdItems.Rows.Count - 1
            If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value Is DBNull.Value) Then
                If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value = "") Then
                    txtSubtotalDO.Text = CDbl(txtSubtotalDO.Text) + CDbl(grdItems.Rows(j).Cells(ColumnasDelGridItems.Subtotal).Value) ' * grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value)
                    txtMontoIvaDO.Text = FormatNumber(CDbl(txtMontoIvaDO.Text) + (grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(j).Cells(ColumnasDelGridItems.MontoIva).Value), 2)
                End If
            End If
        Next

        txtTotalDO.Text = FormatNumber(CDbl(txtSubtotalDO.Text) + CDbl(txtMontoIvaDO.Text), 2)

    End Sub

    Private Sub CalcularMontos()
        Dim subtotal As Double = 0
        Dim i As Integer

        txtSubtotalDO.Text = "0"
        txtTotalDO.Text = "0"
        txtMontoIvaDO.Text = "0"

        For i = 0 To grdItemsEspeciales.RowCount - 1
            subtotal = subtotal + CDbl(grdItemsEspeciales.Rows(i).Cells(1).Value)
        Next

        txtSubtotalDO.Text = FormatNumber(subtotal, 2)

        If bolModo = True Then
            txtMontoIvaDO.Text = FormatNumber(CDbl(txtSubtotalDO.Text) * (CDbl(cmbIVA.Text) / 100), 2)
        Else
            txtMontoIvaDO.Text = FormatNumber(CDbl(txtSubtotalDO.Text) * (CDbl(txtIVA.Text) / 100), 2)
        End If

        txtTotalDO.Text = FormatNumber(CDbl(txtSubtotalDO.Text) + CDbl(txtMontoIvaDO.Text), 2)

    End Sub

    Private Function AgregarRemito_tmp() As Integer
        Dim subtotal As Double = 0, total As Double = 0, MONTOiva As Double = 0

        Dim j As Integer

        Try
            For j = 0 To grdItems.RowCount - 1
                If CBool(grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value) = True Then
                    subtotal = subtotal + Format(grdItems.Rows(j).Cells(ColumnasDelGridItems.Precio).Value, "####0.00")
                    MONTOiva = MONTOiva + Format(grdItems.Rows(j).Cells(ColumnasDelGridItems.MontoIva).Value, "####0.00")
                    total = total + Format(grdItems.Rows(j).Cells(ColumnasDelGridItems.Subtotal).Value, "####0.00")
                End If
            Next

            txtSubtotalDO.Text = Format(subtotal, "####0.00")
            txtMontoIvaDO.Text = Format(MONTOiva, "####0.00")
            txtTotalDO.Text = Format(total, "####0.00")

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

#End Region

#Region "   Funciones"

    '
    'Agregar uno a uno los registros de la grilla a la tabla en la bd
    '
    Private Function AgregarActualizar_Registro() As Integer
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

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idpresupuesto As New SqlClient.SqlParameter
                param_idpresupuesto.ParameterName = "@idpresupuesto"
                param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                If chkPresupuesto.Checked = True Then
                    If bolModo = True Then
                        param_idpresupuesto.Value = cmbPresupuestos.SelectedValue
                    Else
                        param_idpresupuesto.Value = txtIdPresupuesto.Text
                    End If
                Else
                    param_idpresupuesto.Value = 0
                End If
                param_idpresupuesto.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_idcliente.Value = cmbCliente.SelectedValue
                Else
                    param_idcliente.Value = txtIdCliente.Text
                End If
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_idmoneda As New SqlClient.SqlParameter
                param_idmoneda.ParameterName = "@idmoneda"
                param_idmoneda.SqlDbType = SqlDbType.BigInt
                param_idmoneda.Value = txtIdMoneda.Text
                param_idmoneda.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 200
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_nroOC As New SqlClient.SqlParameter
                param_nroOC.ParameterName = "@nroOC"
                param_nroOC.SqlDbType = SqlDbType.VarChar
                param_nroOC.Size = 30
                param_nroOC.Value = txtNroOC.Text
                param_nroOC.Direction = ParameterDirection.Input

                Dim param_Comprador As New SqlClient.SqlParameter
                param_Comprador.ParameterName = "@Comprador"
                param_Comprador.SqlDbType = SqlDbType.Bit
                param_Comprador.Value = chkComprador.Checked
                param_Comprador.Direction = ParameterDirection.Input

                Dim param_idComprador As New SqlClient.SqlParameter
                param_idComprador.ParameterName = "@idContacto_Comprador"
                param_idComprador.SqlDbType = SqlDbType.BigInt
                param_idComprador.Value = IIf(chkComprador.Checked = True, cmbComprador.SelectedValue, 0)
                param_idComprador.Direction = ParameterDirection.Input

                Dim param_Usuario As New SqlClient.SqlParameter
                param_Usuario.ParameterName = "@Usuario"
                param_Usuario.SqlDbType = SqlDbType.Bit
                param_Usuario.Value = chkUsuario.Checked
                param_Usuario.Direction = ParameterDirection.Input

                Dim param_idUsuario As New SqlClient.SqlParameter
                param_idUsuario.ParameterName = "@idContacto_Usuario"
                param_idUsuario.SqlDbType = SqlDbType.BigInt
                param_idUsuario.Value = IIf(chkUsuario.Checked = True, cmbUsuario.SelectedValue, 0)
                param_idUsuario.Direction = ParameterDirection.Input

                Dim param_Entregaren As New SqlClient.SqlParameter
                param_Entregaren.ParameterName = "@Entregaren"
                param_Entregaren.SqlDbType = SqlDbType.Bit
                param_Entregaren.Value = chkEntrega.Checked
                param_Entregaren.Direction = ParameterDirection.Input

                Dim param_sitioentrega As New SqlClient.SqlParameter
                param_sitioentrega.ParameterName = "@sitioentrega"
                param_sitioentrega.SqlDbType = SqlDbType.VarChar
                param_sitioentrega.Size = 25
                param_sitioentrega.Value = cmbEntregaren.Text
                param_sitioentrega.Direction = ParameterDirection.Input

                Dim param_EntregaPendiente As New SqlClient.SqlParameter
                param_EntregaPendiente.ParameterName = "@EntregaPendiente"
                param_EntregaPendiente.SqlDbType = SqlDbType.Bit
                param_EntregaPendiente.Value = chkEntregaPendiente.Checked
                param_EntregaPendiente.Direction = ParameterDirection.Input

                Dim param_subtotalDO As New SqlClient.SqlParameter
                param_subtotalDO.ParameterName = "@subtotalDO"
                param_subtotalDO.SqlDbType = SqlDbType.Decimal
                param_subtotalDO.Precision = 18
                param_subtotalDO.Scale = 2
                param_subtotalDO.Value = CType(txtSubtotalDO.Text, Double)
                param_subtotalDO.Direction = ParameterDirection.Input

                Dim param_MontoIvaDO As New SqlClient.SqlParameter
                param_MontoIvaDO.ParameterName = "@MontoIvaDO"
                param_MontoIvaDO.SqlDbType = SqlDbType.Decimal
                param_MontoIvaDO.Precision = 18
                param_MontoIvaDO.Scale = 2
                param_MontoIvaDO.Value = CType(txtMontoIvaDO.Text, Double)
                param_MontoIvaDO.Direction = ParameterDirection.Input

                Dim param_totalDO As New SqlClient.SqlParameter
                param_totalDO.ParameterName = "@totalDO"
                param_totalDO.SqlDbType = SqlDbType.Decimal
                param_totalDO.Precision = 18
                param_totalDO.Scale = 2
                param_totalDO.Value = CType(txtTotalDO.Text, Double)
                param_totalDO.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                If bolModo = True Then
                    param_iva.Value = cmbIVA.Text
                Else
                    param_iva.Value = txtIVA.Text
                End If
                param_iva.Direction = ParameterDirection.Input

                Dim param_RemitoEspecial As New SqlClient.SqlParameter
                param_RemitoEspecial.ParameterName = "@RemitoEspecial"
                param_RemitoEspecial.SqlDbType = SqlDbType.Bit
                param_RemitoEspecial.Value = chkRemitoEspecial.Checked
                param_RemitoEspecial.Direction = ParameterDirection.Input

                Dim param_ParaFacturar As New SqlClient.SqlParameter
                param_ParaFacturar.ParameterName = "@ParaFacturar"
                param_ParaFacturar.SqlDbType = SqlDbType.Bit
                param_ParaFacturar.Value = chkParaFacturar.Checked
                param_ParaFacturar.Direction = ParameterDirection.Input

                Dim param_TipoRemito As New SqlClient.SqlParameter
                param_TipoRemito.ParameterName = "@TipoRemito"
                param_TipoRemito.SqlDbType = SqlDbType.VarChar
                param_TipoRemito.Size = 25
                param_TipoRemito.Value = TipoRemito
                param_TipoRemito.Direction = ParameterDirection.Input

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

                Dim param_mensaje As New SqlClient.SqlParameter
                param_mensaje.ParameterName = "@MENSAJE"
                param_mensaje.SqlDbType = SqlDbType.VarChar
                param_mensaje.Size = 100
                param_mensaje.Value = DBNull.Value
                param_mensaje.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Insert", _
                                              param_id, param_codigo, param_fecha, param_idpresupuesto, param_idcliente, param_idmoneda, _
                                              param_nota, param_nroOC, _
                                              param_Comprador, param_idComprador, param_Usuario, param_idUsuario, param_Entregaren, _
                                              param_sitioentrega, param_EntregaPendiente, _
                                              param_subtotalDO, param_iva, param_MontoIvaDO, param_totalDO, _
                                              param_RemitoEspecial, param_ParaFacturar, param_TipoRemito, param_useradd, param_res, param_mensaje)

                        txtID.Text = param_id.Value

                        txtCODIGO.Text = param_codigo.Value

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Update", _
                                              param_id, param_fecha, param_idpresupuesto, param_idmoneda, _
                                              param_nota, param_nroOC, _
                                              param_Comprador, param_idComprador, param_Usuario, param_idUsuario, param_Entregaren, _
                                              param_subtotalDO, param_iva, param_MontoIvaDO, param_totalDO, _
                                              param_sitioentrega, param_EntregaPendiente, _
                                              param_ParaFacturar, param_useradd, param_res)

                    End If

                    AgregarActualizar_Registro = param_res.Value

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

    Private Function Actualizar_Subtotal() As Integer
        'Dim res As Integer = 0

        Try
            Try
                Dim param_idpres_gest As New SqlClient.SqlParameter
                param_idpres_gest.ParameterName = "@idpres_ges"
                param_idpres_gest.SqlDbType = SqlDbType.BigInt
                param_idpres_gest.Value = txtID.Text
                param_idpres_gest.Direction = ParameterDirection.Input

                Dim param_idpresupuesto As New SqlClient.SqlParameter
                param_idpresupuesto.ParameterName = "@idpresupuesto"
                param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                param_idpresupuesto.Value = cmbPresupuestos.SelectedValue
                param_idpresupuesto.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_ActualizarSubtotal", _
                                              param_idpres_gest, param_idpresupuesto, param_res)

                    Actualizar_Subtotal = param_res.Value

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

    Private Function AgregarRegistro_Items(ByVal IdPres_Ges As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer

        Try
            Try
                i = 0
                Do While i < grdItems.Rows.Count

                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Decimal) > 0 Then

                        Dim param_id As New SqlClient.SqlParameter
                        param_id.ParameterName = "@id"
                        param_id.SqlDbType = SqlDbType.BigInt
                        'If bolModo = True Then
                        param_id.Value = DBNull.Value
                        param_id.Direction = ParameterDirection.InputOutput
                        'Else
                        '    param_id.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IdPres_Ges_Det).Value
                        '    param_id.Direction = ParameterDirection.Input
                        'End If

                        Dim param_codigo As New SqlClient.SqlParameter
                        param_codigo.ParameterName = "@codigo"
                        param_codigo.SqlDbType = SqlDbType.VarChar
                        param_codigo.Size = 25
                        param_codigo.Value = DBNull.Value
                        param_codigo.Direction = ParameterDirection.InputOutput

                        Dim param_idpres_ges As New SqlClient.SqlParameter
                        param_idpres_ges.ParameterName = "@idpres_ges"
                        param_idpres_ges.SqlDbType = SqlDbType.BigInt
                        param_idpres_ges.Value = IdPres_Ges
                        param_idpres_ges.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@idmaterial"
                        param_idmaterial.SqlDbType = SqlDbType.BigInt
                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                        param_idmaterial.Direction = ParameterDirection.Input

                        'cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value

                        Dim param_qty As New SqlClient.SqlParameter
                        param_qty.ParameterName = "@qty"
                        param_qty.SqlDbType = SqlDbType.Decimal
                        param_qty.Precision = 18
                        param_qty.Scale = 2
                        param_qty.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value
                        param_qty.Direction = ParameterDirection.Input

                        Dim param_preciouni As New SqlClient.SqlParameter
                        param_preciouni.ParameterName = "@preciouni"
                        param_preciouni.SqlDbType = SqlDbType.Decimal
                        param_preciouni.Precision = 18
                        param_preciouni.Scale = 2
                        param_preciouni.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value, Decimal)
                        param_preciouni.Direction = ParameterDirection.Input

                        Dim param_subtotal As New SqlClient.SqlParameter
                        param_subtotal.ParameterName = "@subtotal"
                        param_subtotal.SqlDbType = SqlDbType.Decimal
                        param_subtotal.Precision = 18
                        param_subtotal.Scale = 2
                        param_subtotal.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value, Decimal)
                        param_subtotal.Direction = ParameterDirection.Input

                        Dim param_idunidad As New SqlClient.SqlParameter
                        param_idunidad.ParameterName = "@idunidad"
                        param_idunidad.SqlDbType = SqlDbType.BigInt
                        param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
                        param_idunidad.Direction = ParameterDirection.Input

                        Dim param_remito As New SqlClient.SqlParameter
                        param_remito.ParameterName = "@remito"
                        param_remito.SqlDbType = SqlDbType.VarChar
                        param_remito.Size = 25
                        param_remito.Value = txtCODIGO.Text ' txtRemito.Text
                        param_remito.Direction = ParameterDirection.Input

                        Dim param_idpresupuesto As New SqlClient.SqlParameter
                        param_idpresupuesto.ParameterName = "@idpresupuesto"
                        param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                        param_idpresupuesto.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Id_Pres).Value, Long)
                        param_idpresupuesto.Direction = ParameterDirection.Input

                        Dim param_idpresupuesto_det As New SqlClient.SqlParameter
                        param_idpresupuesto_det.ParameterName = "@idpres_det"
                        param_idpresupuesto_det.SqlDbType = SqlDbType.BigInt
                        param_idpresupuesto_det.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_Pres_Det).Value, Long)
                        param_idpresupuesto_det.Direction = ParameterDirection.Input

                        Dim param_useradd As New SqlClient.SqlParameter
                        If bolModo = True Then
                            param_useradd.ParameterName = "@useradd"
                        Else
                            param_useradd.ParameterName = "@userUPD"
                        End If
                        param_useradd.SqlDbType = SqlDbType.Int
                        param_useradd.Value = UserID
                        param_useradd.Direction = ParameterDirection.Input

                        Dim param_oca As New SqlClient.SqlParameter
                        param_oca.ParameterName = "@OCA"
                        param_oca.SqlDbType = SqlDbType.Bit
                        param_oca.Value = chkOCA.Checked
                        param_oca.Direction = ParameterDirection.Input

                        Dim param_Fecha As New SqlClient.SqlParameter
                        param_Fecha.ParameterName = "@Fecha"
                        param_Fecha.SqlDbType = SqlDbType.Date
                        param_Fecha.Value = dtpFECHA.Value
                        param_Fecha.Direction = ParameterDirection.Input

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
                            'If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Det_Insert", _
                                                  param_id, param_codigo, param_idpres_ges, param_idmaterial, _
                                                  param_qty, param_preciouni, param_subtotal, param_idunidad, param_remito, _
                                                  param_idpresupuesto, param_idpresupuesto_det, param_useradd, param_oca, param_Fecha, _
                                                  param_res, param_msg)

                            'Else
                            '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Det_Update", _
                            '                          param_id, param_idpresupuesto_det, param_idpresupuesto, param_idmaterial, _
                            '                          param_idunidad, param_qty, param_remito, param_oca, param_useradd, param_res)

                            'End If

                            res = param_res.Value

                            If Not (param_msg.Value Is System.DBNull.Value) Then
                                msg = param_msg.Value
                            Else
                                msg = ""
                            End If

                            If (res <= 0) Then
                                Exit Do
                            End If

                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                    i = i + 1
                Loop

                AgregarRegistro_Items = res

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

    Private Function AgregarActualizar_Registro_Items_Especiales() As Integer
        Dim res As Integer

        Try

            Dim i As Integer

            Dim param_idPresGes As New SqlClient.SqlParameter
            param_idPresGes.ParameterName = "@idPresGes"
            param_idPresGes.SqlDbType = SqlDbType.BigInt
            param_idPresGes.Value = txtID.Text
            param_idPresGes.Direction = ParameterDirection.Input

            Dim param_resDEL As New SqlClient.SqlParameter
            param_resDEL.ParameterName = "@res"
            param_resDEL.SqlDbType = SqlDbType.Int
            param_resDEL.Value = DBNull.Value
            param_resDEL.Direction = ParameterDirection.InputOutput

            If bolModo = False Then
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Especiales_Det_Delete", param_idPresGes, param_resDEL)

                res = param_resDEL.Value

                If res <= 0 Then
                    MsgBox("Se produjo un error al eliminar temporalmente los Items", MsgBoxStyle.Critical, "Control de Errores")
                    AgregarActualizar_Registro_Items_Especiales = -1
                    Exit Function
                End If

            End If

            For i = 0 To grdItemsEspeciales.Rows.Count - 1

                Dim param_SubTotal As New SqlClient.SqlParameter
                param_SubTotal.ParameterName = "@SubTotal"
                param_SubTotal.SqlDbType = SqlDbType.Decimal
                param_SubTotal.Precision = 18
                param_SubTotal.Scale = 2
                param_SubTotal.Value = grdItemsEspeciales.Rows(i).Cells(1).Value
                param_SubTotal.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@DescripcionRemito"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 4000
                param_nota.Value = grdItemsEspeciales.Rows(i).Cells(0).Value
                param_nota.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Especiales_Det_Insert", param_idPresGes, _
                                                param_SubTotal, param_nota, param_res)

                    res = param_res.Value

                    If res <= 0 Then
                        AgregarActualizar_Registro_Items_Especiales = -1
                        Exit Function
                    End If

                Catch ex As Exception
                    Throw ex
                End Try

            Next

            AgregarActualizar_Registro_Items_Especiales = 1

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

    Private Function AgregarItems_ManoObra() As Integer
        Dim res As Integer

        Try

            Dim i As Integer

            For i = 0 To grdItems.Rows.Count - 1

                If CBool(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value) = True Then

                    Dim param_IdPresGes As New SqlClient.SqlParameter
                    param_IdPresGes.ParameterName = "@IdPresGes"
                    param_IdPresGes.SqlDbType = SqlDbType.BigInt
                    param_IdPresGes.Value = txtID.Text
                    param_IdPresGes.Direction = ParameterDirection.Input

                    Dim param_IdPresupuesto As New SqlClient.SqlParameter
                    param_IdPresupuesto.ParameterName = "@IdPresupuesto"
                    param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                    param_IdPresupuesto.Value = txtIdPresupuesto.Text
                    param_IdPresupuesto.Direction = ParameterDirection.Input

                    Dim param_IdPresOfertaComercial As New SqlClient.SqlParameter
                    param_IdPresOfertaComercial.ParameterName = "@IdPresOfertaComercial"
                    param_IdPresOfertaComercial.SqlDbType = SqlDbType.BigInt
                    param_IdPresOfertaComercial.Value = grdItems.Rows(i).Cells(0).Value
                    param_IdPresOfertaComercial.Direction = ParameterDirection.Input

                    Dim param_IdPresOfertaComercial_DET As New SqlClient.SqlParameter
                    param_IdPresOfertaComercial_DET.ParameterName = "@IdPresOfertaComercial_det"
                    param_IdPresOfertaComercial_DET.SqlDbType = SqlDbType.BigInt
                    param_IdPresOfertaComercial_DET.Value = grdItems.Rows(i).Cells(2).Value
                    param_IdPresOfertaComercial_DET.Direction = ParameterDirection.Input

                    Dim param_Fecha As New SqlClient.SqlParameter
                    param_Fecha.ParameterName = "@Fecha"
                    param_Fecha.SqlDbType = SqlDbType.Date
                    param_Fecha.Value = dtpFECHA.Value
                    param_Fecha.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_ManoObra_Insert", _
                                                  param_IdPresGes, param_IdPresOfertaComercial, param_IdPresOfertaComercial_DET, _
                                                  param_Fecha, param_IdPresupuesto, param_res)

                        res = param_res.Value

                        If res <= 0 Then
                            AgregarItems_ManoObra = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                End If

            Next

            AgregarItems_ManoObra = 1

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

    Private Function AnularFisico_Registro(ByVal logico As Boolean, ByVal fisico As Boolean) As Integer
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

                Dim param_idPresGest As New SqlClient.SqlParameter("@idPres_Ges", SqlDbType.BigInt, ParameterDirection.Input)
                param_idPresGest.Value = CType(txtID.Text, Long)
                param_idPresGest.Direction = ParameterDirection.Input

                Dim param_userdel As New SqlClient.SqlParameter
                param_userdel.ParameterName = "@userdel"
                param_userdel.SqlDbType = SqlDbType.Int
                param_userdel.Value = UserID
                param_userdel.Direction = ParameterDirection.Input

                Dim param_FISICO As New SqlClient.SqlParameter
                param_FISICO.ParameterName = "@fisico"
                param_FISICO.SqlDbType = SqlDbType.Bit
                param_FISICO.Value = fisico
                param_FISICO.Direction = ParameterDirection.Input

                Dim param_logico As New SqlClient.SqlParameter
                param_logico.ParameterName = "@logico"
                param_logico.SqlDbType = SqlDbType.Bit
                param_logico.Value = logico
                param_logico.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Delete", _
                                              param_idPresGest, param_userdel, param_FISICO, param_logico, param_res)

                    AnularFisico_Registro = param_res.Value

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

    Private Function AnularFisico_RegistroItems(ByVal IdPres_Ges As Long) As Integer
        Dim i As Integer, res As Integer

        Try
            Try
                i = 0
                Do While i < grdItems.Rows.Count

                    Dim param_IdPresOfertaComercial As New SqlClient.SqlParameter
                    param_IdPresOfertaComercial.ParameterName = "@IdPresOfertaComercial"
                    param_IdPresOfertaComercial.SqlDbType = SqlDbType.BigInt
                    param_IdPresOfertaComercial.Value = grdItems.Rows(i).Cells(0).Value
                    param_IdPresOfertaComercial.Direction = ParameterDirection.Input

                    Dim param_IdPresOfertaComercial_DET As New SqlClient.SqlParameter
                    param_IdPresOfertaComercial_DET.ParameterName = "@IdPresOfertaComercial_det"
                    param_IdPresOfertaComercial_DET.SqlDbType = SqlDbType.BigInt
                    param_IdPresOfertaComercial_DET.Value = grdItems.Rows(i).Cells(2).Value
                    param_IdPresOfertaComercial_DET.Direction = ParameterDirection.Input

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@idpres_ges"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = IdPres_Ges
                    param_id.Direction = ParameterDirection.Input

                    Dim param_idpres_ges As New SqlClient.SqlParameter
                    param_idpres_ges.ParameterName = "@idpres_ges_det"
                    param_idpres_ges.SqlDbType = SqlDbType.BigInt
                    param_idpres_ges.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdPres_Ges_Det).Value, Long)
                    param_idpres_ges.Direction = ParameterDirection.Input

                    Dim param_idpresupuesto As New SqlClient.SqlParameter
                    param_idpresupuesto.ParameterName = "@idpresupuesto"
                    param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                    param_idpresupuesto.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Id_Pres).Value, Long)
                    param_idpresupuesto.Direction = ParameterDirection.Input

                    Dim param_idpresupuesto_det As New SqlClient.SqlParameter
                    param_idpresupuesto_det.ParameterName = "@idpres_det"
                    param_idpresupuesto_det.SqlDbType = SqlDbType.BigInt
                    param_idpresupuesto_det.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_Pres_Det).Value, Long)
                    param_idpresupuesto_det.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.BigInt
                    param_idmaterial.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)
                    param_idmaterial.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.BigInt
                    param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
                    param_idunidad.Direction = ParameterDirection.Input

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 2
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyPedida).Value, Decimal) - CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value, Decimal)
                    param_qty.Direction = ParameterDirection.Input

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

                        If TipoRemito = "Matx" Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_Det_Delete", _
                                                  param_id, param_idpres_ges, param_idpresupuesto, param_idpresupuesto_det, _
                                                  param_idmaterial, param_idunidad, param_qty, param_userdel, param_res)
                        Else

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Gestion_ManoObra_Delete", _
                                                  param_IdPresOfertaComercial, param_IdPresOfertaComercial_DET, _
                                                  param_idpresupuesto, param_res)

                        End If

                        res = param_res.Value

                        If (res <= 0) Then
                            Exit Do
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                    i = i + 1
                Loop

                AnularFisico_RegistroItems = res

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

    Private Function fila_vacia(ByVal i) As Boolean
        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Then
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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdPres_Ges_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdPres_Ges_Det)
                End If
            Else
                ' Seleccionamos la celda de la derecha de la celda actual.
                cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
            End If
            ' establecer la fila y la columna actual
            columnIndex = cell.ColumnIndex
            rowIndex = cell.RowIndex
        Loop While (cell.Visible = False)

        grdItems.CurrentCell = cell

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

    Private Function BuscarNombreArchivo(ByVal codigo As String) As String
        Dim ds_Archivo As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BuscarNombreArchivo = ""
            Exit Function
        End Try

        Try

            ds_Archivo = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select 'Remito ' + pg.codigo + ' - ' + c.nombre + ' - Presup ' + ISNULL(p.codigo,'') + ' - OC ' + isnull(P.NROOC,'')  " & _
                                                    " from presupuestos_gestion pg LEFT JOIN Presupuestos p ON p.id = pg.idpresupuesto JOIN Clientes C ON c.id = pg.idcliente " & _
                                                    " where pg.codigo = " & codigo)

            ds_Archivo.Dispose()

            BuscarNombreArchivo = ds_Archivo.Tables(0).Rows(0)(0)

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            BuscarNombreArchivo = ""

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

#End Region

#Region "   Botones"

    Protected Friend Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        chkAnulados.Checked = False
        chkAnulados_CheckedChanged(sender, e)
        chkAnulados.Enabled = Not bolModo

        'GroupBox1.Enabled = True

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        dtpFECHA.Enabled = True
        txtNota.Enabled = True

        Util.LimpiarTextBox(Me.Controls)

        Me.Label5.Visible = True
        Me.cmbCliente.Visible = True
        'Me.btnLlenarGrilla.Visible = True

        chkPresupuesto.Checked = True

        cmbCliente.Visible = bolModo
        txtCliente.Visible = Not bolModo

        cmbPresupuestos.Visible = bolModo
        txtPresupuestos.Visible = Not bolModo

        cmbIVA.Visible = bolModo
        txtIVA.Visible = Not bolModo

        btnLlenarGrilla.Enabled = bolModo

        chkFacturado.Checked = False
        lblRemitoFacturado.Visible = False
        lblRemitoFacturado2.Visible = False
        chkParaFacturar.Checked = True
        chkRemitoEspecial.Checked = False
        chkRemitoEspecial.Enabled = True
        grdItems.Enabled = True

        chkOCA.Checked = False

        lblRemito.Text = "0"
        lblPresupuesto.Text = "0"
        lblFacturado.Text = "0"
        lblDiferenciaRemito.Text = "0"
        lblMoneda.Text = ""

        grdItemsEspeciales.Rows.Clear()

        dtpFECHA.Focus()

        PrepararGridItems()
        LimpiarGridItems(grdItems)

        band = 1
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer, res_item As Integer

        If chkPresupuesto.Checked = False And cmbMonedas.SelectedIndex < 0 Then
            MsgBox("Debe seleccionar una Moneda para hacer el Remito Especial", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If

        If txtIdCliente.Text = "" Then
            MsgBox("Debe seleccionar un Cliente para hacer el Remito", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo generar el código para la operación actual.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo generar el código para la operación actual.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "Se produjo un error desconocido. Consulte con el administrador del sistema.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "Se produjo un error desconocido. Consulte con el administrador del sistema.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case Else
                        If chkFacturado.Checked = False Then
                            Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                            If chkRemitoEspecial.Checked = True Then
                                res_item = AgregarActualizar_Registro_Items_Especiales()
                                Select Case res_item
                                    Case Is <= 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap, True)
                                        Exit Sub
                                End Select
                            Else
                                If bolModo = True Then

                                    If TipoRemito = "Matx" Then
                                        res_item = AgregarRegistro_Items(txtID.Text)
                                    Else
                                        res_item = AgregarItems_ManoObra()
                                    End If

                                    Select Case res_item

                                        Case -7
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo actualizar el precio de un producto", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case -6
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case -5
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case -4
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case -3
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap)
                                            Exit Sub
                                        Case -2
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap)
                                            Exit Sub
                                        Case -1
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar el remito (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case 0
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case Else

                                    End Select
                                End If
                            End If
                        End If

                        Cerrar_Tran()
                        Util.MsgStatus(Status1, "Remito generado correctamente.", My.Resources.Resources.ok.ToBitmap)
                        bolModo = False
                        PrepararBotones()

                        Imprimir(txtCODIGO.Text)

                        btnActualizar_Click(sender, e)
                        Me.Label5.Visible = False
                        Me.cmbCliente.Visible = False
                        txtCliente.Visible = True
                        cmbPresupuestos.Visible = False
                        txtPresupuestos.Visible = True
                        cmbIVA.Visible = False
                        txtIVA.Visible = True
                        btnEntregarTodos.Enabled = False
                        chkRemitoEspecial.Enabled = False

                        BuscarOCReq()

                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                End Select

                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If

            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        nbreformreportes = "Remito"

        Dim param As New frmParametros
        'Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Rpt As New frmReportes

        param.AgregarParametros("N° de Mov:", "STRING", "", False, txtCODIGO.Text.ToString, "", conexionGenerica)

        param.ShowDialog()
        If cerroparametrosconaceptar = True Then
            codigo = param.ObtenerParametros(0)

            Rpt.NombreArchivoPDF = BuscarNombreArchivo(codigo)

            If MessageBox.Show("Desea imprimir 2 Copias directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Rpt.Remito_App(codigo, Rpt, My.Application.Info.AssemblyName.ToString, "Material", True)
            Else
                Rpt.Remito_App(codigo, Rpt, My.Application.Info.AssemblyName.ToString, "Material", False)
            End If

            cerroparametrosconaceptar = False
            param = Nothing
            'cnn = Nothing
        End If

    End Sub

    Private Sub btnLlenarGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlenarGrilla.Click
        'Dim i As Integer

        If bolModo And llenandoCombo = False Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
            If Me.cmbCliente.Text <> "" Then
                PrepararGridItems()
                LlenarGridItemsPorPresup(txtIdPresupuesto.Text) 'CType(Me.cmbPresupuestos.SelectedValue, Long))
                btnEntregarTodos.Enabled = True
                With (grdItems)
                    .AllowUserToAddRows = False
                    .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
                    .Columns(ColumnasDelGridItems.Qty).ReadOnly = False 'qty
                    '.Columns(ColumnasDelGridItems.Remito).ReadOnly = False 'Remito
                    '.Columns(ColumnasDelGridItems.LoteProveed).ReadOnly = False 'Remito
                End With
            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer, res_item As Integer

        If chkRemitoEspecial.Checked = False And TipoRemito = "Matx" Then
            If MessageBox.Show("Esta acción anulará el Remito Físico (no podrá usar el mismo nro de remito en el sistema) " + vbCrLf + _
                           "y actualizará el Stock y el estado de los items a 'P'." + vbCrLf + "¿Está seguro que desea Anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

        Else
            If MessageBox.Show("Esta acción anulará el Remito Físico (no podrá usar el mismo nro de remito en el sistema) " + vbCrLf + _
                           "¿Está seguro que desea Anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If txtNota.Text = "" Then
            If MessageBox.Show("No ha ingresado ninguna Nota de Gestión, desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                txtNota.Focus()
            End If
        End If

        Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
        res = AnularFisico_Registro(False, True)
        Select Case res
            Case -3
                Cancelar_Tran()
                Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
            Case 0
                Cancelar_Tran()
                Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado) - 0.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado) - 0.", My.Resources.Resources.stop_error.ToBitmap, True)
            Case Else
                If chkRemitoEspecial.Checked = True Then
                    GoTo ContinuarAnulacion
                End If
                Util.MsgStatus(Status1, "Actualizando los items...", My.Resources.Resources.indicator_white)
                res_item = AnularFisico_RegistroItems(CType(txtID.Text, Long))
                Select Case res_item
                    Case -4
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks Mov (Items) - 4.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks Mov (Items) - 4.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks (Items) - 3.", My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks (Items) - 3.", My.Resources.Resources.alert.ToBitmap, True)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No pudo realizarse la anulación (Detalle Item) - 2.", My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "No pudo realizarse la anulación (Detalle Item) - 2.", My.Resources.Resources.alert.ToBitmap, True)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks (Items) - 1.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks (Items) - 1.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks (Items) - 0.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudieron Actualizar los Stocks (Items) - 0.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case Else
ContinuarAnulacion:
                        Cerrar_Tran()
                        bolModo = False
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        Util.MsgStatus(Status1, "Se ha actualizado el Remito.", My.Resources.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se ha actualizado el Remito.", My.Resources.Resources.ok.ToBitmap, True, True)
                End Select
        End Select
        '
        ' cerrar la conexion si está abierta.
        '
        If Not conn_del_form Is Nothing Then
            CType(conn_del_form, IDisposable).Dispose()
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        btnEntregarTodos.Enabled = False
        chkAnulados.Enabled = Not bolModo
        cmbCliente.Visible = bolModo
        cmbPresupuestos.Visible = bolModo
        txtCliente.Visible = Not bolModo
        'txtPresupuestos.Visible = Not bolModo
        cmbIVA.Visible = bolModo
        txtIVA.Visible = Not bolModo
        BtnFactura.Enabled = Not bolModo
        chkRemitoEspecial.Enabled = False

        grd_CurrentCellChanged(sender, e)

        chkPresupuesto_CheckedChanged(sender, e)

    End Sub

    Private Sub btnEntregarTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntregarTodos.Click
        If TipoRemito = "Matx" Then
            If MessageBox.Show("Está seguro que desea copiar los valores de la columna Cant. Pedida a la columna Cant. Saldo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim i As Integer

                For i = 0 To grdItems.RowCount - 1
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value, "####0.00")
                Next

                ObtenerTotales()

            End If
        Else
            If MessageBox.Show("Está seguro que desea Entregar todos los items?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim i As Integer

                For i = 0 To grdItems.RowCount - 1
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value = True
                Next

            End If
        End If


    End Sub

    Private Sub BtnFactura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFactura.Click
        Dim f As New frmFacturacion
        f.Origen = 1
        f.Origen_IdCliente = cmbCliente.SelectedValue
        f.Origen_IdPresupuesto = txtIdPresupuesto.Text
        f.Origen_NroRemito = txtRemito.Text
        f.MdiParent = MDIPrincipal
        f.Show()
    End Sub

#End Region

  
    Private Sub chkPresupuesto_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresupuesto.CheckedChanged

        If band = 1 Then
            chkRemitoEspecial.Checked = Not chkPresupuesto.Checked
        End If

        cmbPresupuestos.Visible = chkPresupuesto.Checked
        If bolModo = False Then
            txtPresupuestos.Visible = chkPresupuesto.Checked
        End If
        'cmbIVA.Visible = chkPresupuesto.Checked
        'lblIVA.Visible = chkPresupuesto.Checked

        cmbMonedas.Visible = Not chkPresupuesto.Checked

        LlenarComboClientes()

        If chkPresupuesto.Checked = False Then
            LlenarComboIVA()
        End If

    End Sub

    Private Sub cmbMonedas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedas.SelectedIndexChanged
        If band = 1 Then
            txtIdMoneda.Text = cmbMonedas.SelectedValue.ToString
        End If
    End Sub
End Class