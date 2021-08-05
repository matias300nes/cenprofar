Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmVentasWEB

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
    Public stockactualizado As Double

    Public producto_unitario As String
    Public idproducto_unitario As String
    Public stock_unitario As String
    Public unidad_unitario As String
    Public almacen_unitario As String
    Public stocknuevo As String
    Public FinalizarPresupuesto As Boolean = False
    Dim desdeload As Boolean = False

    'Dim subSQL As String

    Dim CONTROL As Integer


    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDPedidosWEB_det = 0
        Orden_Item = 1
        Cod_Material = 2
        Nombre_Material = 3
        Cod_Marca = 4
        Marca = 5
        Cod_Unidad = 6
        Unidad = 7
        QtyEnviada = 8
        Peso = 9
        Precio = 10
        IVA = 11
        Descuento = 12
        Subtotal = 13
        QtyPedido = 14
        QtySaldo = 15
        Status = 16
        FechaCumplido = 17
        Nota_Det = 18
        Stock = 19
        Eliminado = 20
        CantidadPaCK = 21
        Reintegrar_Stock = 22
        Bonificacion = 23
        Promo = 24
        Procesado = 25


    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim band As Integer

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient

    Dim ds_Pedidos As Data.DataSet
    Dim ds_Producto As Data.DataSet
    Dim sqlstring_ped As String
    Dim estado_chk As Boolean
    Dim estado_chkNorte As Boolean
    Dim desde_valor As Boolean
    Dim ValorNorte_cambio As Double = 0.0
    Dim DescLista As String = ""
    Dim Habilitar_Promo As Boolean
    Dim DescuentoPromo As Double
    Dim PrecioPromo As Double
    Dim DescripcionPromo As String
    Dim Principal As Boolean
    Dim IDAlmacenTrans As Integer
    Dim DesdeAnular As Boolean = False







#Region "   Eventos"

    Private Sub frmPedidosWEB_Gestion_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Items()
            End If
        End If
    End Sub

    Private Sub frmPedidosWEB_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el envío nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Orde de Compra Nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmAjustes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        btnSalir_Click(sender, e)
    End Sub

    Private Sub frmPedidosWEB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Cursor = Cursors.WaitCursor

        ToolStrip_lblCodMaterial.Visible = False
        txtBusquedaMAT.Visible = False

        band = 0


        configurarform()
        asignarTags()

        LlenarcmbAlmacenes()
        LlenarcmbClientes()
        llenarcmbRepartidor()

        Me.LlenarCombo_Productos()

        desdeload = True
        chkVentas.Checked = True
        rdTodasPed.Checked = True

    
        SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ",@Devolucion = " & chkDevolucion.Checked



        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()


        'Setear_Grilla()

        If bolModo = True Then
            chkVentas.Checked = True
            'LlenarGrid_Items()
            band = 1
            btnNuevo_Click(sender, e)
        Else
            'btnLlenarGrilla.Enabled = bolModo
            DesbloquearComponentes(bolModo)
            LlenarGrid_Items()
            BuscarSaldo()
        End If

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        grd.Columns(0).Visible = False
        grd.Columns(3).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(14).Visible = False
        grd.Columns(15).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(17).Visible = False
        grd.Columns(18).Visible = False
        grd.Columns(19).Visible = False
        grd.Columns(20).Visible = False
        grd.Columns(21).Visible = False
        grd.Columns(22).Visible = False
        grd.Columns(23).Visible = False
        grd.Columns(24).Visible = False
        'grd.Columns(30).Visible = False
        'grd.Columns(31).Visible = False

        'habilito el txt precio solo si es un usuario habilitado (por ahora dejo que el mati cambie de precio)
        'If MDIPrincipal.EmpleadoLogueado = "4" Then
        '    txtPrecioVta.ReadOnly = False
        'Else
        '    txtPrecioVta.ReadOnly = Not MDIPrincipal.ControlUsuarioAutorizado(MDIPrincipal.EmpleadoLogueado)
        'End If


        Contar_Filas()
        'pongo para que busque por codigo
        chkCodigos.Checked = True
        'dtpFECHA.MaxDate = Today.Date

        'desdeload = False

        band = 1

        'TimerDescargas.Enabled = True

        If MDIPrincipal.sucursal.Contains("PERON") Then
            Principal = False
            PanelTotales.Location = New Point(355, 375)
        Else
            Principal = False
            PanelTotales.Location = New Point(462, 375)
        End If
        'muestro la columna de peso
        grdItems.Columns(9).Visible = Principal
        btnXNorte.Visible = Principal
        chkPresupuesto.Visible = Principal
        lblSaldo.Visible = Principal
        Label22.Visible = Principal

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

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtNota.KeyPress
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

    'Private Sub cmbPedidos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    If band = 1 Then

    '        Try
    '            connection = SqlHelper.GetConnection(ConnStringSEI)
    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try

    '        Dim PedidoAnterior As String = IIf(txtIDPedido.Text = "", "0", txtIDPedido.Text)

    '        If PedidoAnterior <> "0" Then

    '            sqlstring_ped = "UPDATE PedidosWEB SET EnProceso = 0 Where OrdenPedido = '" & PedidoAnterior & "'"

    '            ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
    '            ds_Pedidos.Dispose()
    '            Try
    '                tranWEB.Sql_Set(sqlstring_ped)

    '            Catch ex As Exception
    '                MsgBox("Error al cambiar estado del pedido " + PedidoAnterior)
    '            End Try

    '        End If

    '        Try
    '            'txtIDPedido.Text = cmbPedidos.SelectedValue
    '            'selecciono el repartido que realizo el pedido
    '            sqlstring_ped = "select IDEmpleado from PedidosWEB where OrdenPedido = '" & txtIDPedido.Text & "'"
    '            ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
    '            ds_Pedidos.Dispose()
    '            'MsgBox(ds_Pedidos.Tables(0).Rows(0).Item(0).ToString)
    '            cmbRepartidor.SelectedValue = ds_Pedidos.Tables(0).Rows(0).Item(0).ToString
    '        Catch ex As Exception
    '            MsgBox("Error al cargcar repartidor")
    '        End Try


    '        If txtIDPedido.Text <> "" Then

    '            sqlstring_ped = "UPDATE PedidosWEB SET EnProceso = 1 Where OrdenPedido = '" & txtIDPedido.Text & "'"

    '            ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
    '            ds_Pedidos.Dispose()

    '            Try
    '                tranWEB.Sql_Set(sqlstring_ped)

    '            Catch ex As Exception
    '                MsgBox("Error al cambiar estado del pedido " + txtIDPedido.Text)
    '            End Try

    '        End If

    '        If llenandoCombo = False Then
    '            btnLlenarGrilla_Click(sender, e)
    '            OcultarItemsEliminados()
    '        End If
    '    End If
    'End Sub

    'Private Sub cmbPedidos_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPedidos.SelectionChangeCommitted
    '    If band = 1 Then
    '        If llenandoCombo = False Then
    '            btnLlenarGrilla_Click(sender, e)
    '        End If
    '    End If
    'End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged
        Dim dsCliente As Data.DataSet

        If band = 1 And bolModo = True Then
            txtIdCliente.Text = cmbClientes.SelectedValue

            Try
                dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select C.IDPrecioLista,C.Provincia,C.Localidad,C.Direccion,C.Repartidor,L.Descripcion,L.Valor_Cambio,C.Promo From Clientes C Join Lista_Precios L On C.IDPrecioLista =L.Codigo Where C.eliminado = 0 and C.Codigo = '" & cmbClientes.SelectedValue & "'")
                txtIDPrecioLista.Text = dsCliente.Tables(0).Rows(0).Item(0)
                lblLugarEntrega.Text = dsCliente.Tables(0).Rows(0).Item(1).ToString + " " + dsCliente.Tables(0).Rows(0).Item(2).ToString + " " + dsCliente.Tables(0).Rows(0).Item(3).ToString
                cmbRepartidor.SelectedValue = dsCliente.Tables(0).Rows(0).Item(4)
                'DescLista = dsCliente.Tables(0).Rows(0).Item(5)
                'ValorNorte_cambio = dsCliente.Tables(0).Rows(0).Item(6)
                'Habilitar_Promo = dsCliente.Tables(0).Rows(0).Item(7)
                'chkTransferencia.Checked = BuscarAlmacen_Cliente()

            Catch ex As Exception

            End Try

            'If Principal Then
            '    Try
            '        If grdItems.Rows.Count > 0 Then

            '            For i As Integer = 0 To grdItems.Rows.Count - 1
            '                If txtIDPrecioLista.Text = 3 Then
            '                    dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCosto From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)

            '                ElseIf txtIDPrecioLista.Text = 4 Then
            '                    dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioMayorista From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
            '                ElseIf txtIDPrecioLista.Text = 5 Then
            '                    dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioLista3 From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
            '                ElseIf txtIDPrecioLista.Text = 10 Then
            '                    dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCompra From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
            '                    'chkTransferencia.Checked = True
            '                ElseIf DescLista.Contains("NORTE") Then
            '                    dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCompra From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0) * ValorNorte_cambio
            '                Else
            '                    dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioLista4 From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
            '                End If
            '                'Me fijo si es horma o tira 
            '                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Then
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
            '                Else
            '                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round((grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
            '                End If

            '                dsCliente.Dispose()
            '            Next
            '            CalculoSubtotales()
            '        End If
            '    Catch ex As Exception

            '    End Try

            'cmbProducto.Enabled = True
            'band = 0
            'LlenarcmbPedidos()
            'band = 1
            'LimpiarGridItems(grdItems)
            ' btnLlenarGrilla_Click(sender, e)
            BuscarSaldo()
            'End If
            cmbProducto.Enabled = True
        End If


    End Sub

    Private Sub cmbClientes_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbClientes.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbClientes.Text <> "" Then
                cmbProducto.Focus()
            End If
        End If
    End Sub

    '(currentcellChanged)
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If Permitir Then
            Try

                Contar_Filas()


                lblNroPedido.Text = grd.CurrentRow.Cells(1).Value.ToString
                dtpFECHA.Value = grd.CurrentRow.Cells(2).Value
                cmbAlmacenes.SelectedValue = grd.CurrentRow.Cells(3).Value
                cmbClientes.SelectedValue = grd.CurrentRow.Cells(5).Value
                'cmbPedidos.SelectedValue = grd.CurrentRow.Cells(1).Value
                lblSubtotal.Text = grd.CurrentRow.Cells(7).Value.ToString
                chkDescuento.Checked = grd.CurrentRow.Cells(15).Value
                rdPorcentaje.Checked = grd.CurrentRow.Cells(16).Value
                rdAbsoluto.Checked = grd.CurrentRow.Cells(17).Value
                txtDescuento.Text = grd.CurrentRow.Cells(8).Value
                lblTotal.Text = grd.CurrentRow.Cells(10).Value.ToString
                txtNota.Text = grd.CurrentRow.Cells(11).Value.ToString
                lblFechaEntrega.Text = grd.CurrentRow.Cells(12).Value.ToString
                'lblStatus.Text = grd.CurrentRow.Cells(14).Value.ToString
                cmbRepartidor.SelectedValue = grd.CurrentRow.Cells(18).Value
                chkTransferencia.Checked = grd.CurrentRow.Cells(21).Value
                lblAutorizado.Text = grd.CurrentRow.Cells(23).Value
                chkPresupuesto.Checked = IIf(grd.CurrentRow.Cells(25).Value.ToString = "Si", True, False)
                BuscarSaldo()

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

        'LlenarGrid_IVA(CType(txtIdGasto.Text, Long))
        'LlenarGrid_Impuestos()

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

    Private Sub cmbAlmacenes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacenes.SelectedIndexChanged
        If band = 1 Then
            Try
                txtidAlmacen.Text = cmbAlmacenes.SelectedValue
                If bolModo And grdItems.Rows.Count > 0 Then
                    ControlItem()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbRepartidor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRepartidor.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDRepartidor.Text = cmbRepartidor.SelectedValue

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub chkDesc_CheckedChanged(sender As Object, e As EventArgs) Handles chkDescuento.CheckedChanged
        rdAbsoluto.Enabled = chkDescuento.Checked
        rdPorcentaje.Enabled = chkDescuento.Checked
        txtDescuento.Enabled = chkDescuento.Checked

        If chkDescuento.Checked = False Then
            txtDescuento.Text = "0"
            lblTotal.Text = lblTotal.Text
        Else
            rdPorcentaje.Checked = True
        End If

    End Sub

    Private Sub rdPorcentaje_CheckedChanged(sender As Object, e As EventArgs) Handles rdPorcentaje.CheckedChanged
        If rdPorcentaje.Checked = True Then
            txtDescuento.Text = "0"
            txtDescuento.Focus()
        End If
    End Sub

    Private Sub rdAbsoluto_CheckedChanged(sender As Object, e As EventArgs) Handles rdAbsoluto.CheckedChanged
        If rdAbsoluto.Checked = True Then
            txtDescuento.Text = "0"
            txtDescuento.Focus()
        End If
    End Sub

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtDescuento.TextChanged
        If band = 0 Then Exit Sub

        CalculoSubtotales()

    End Sub

    Private Sub rdPendientes_CheckedChanged(sender As Object, e As EventArgs) Handles rdPendientes.CheckedChanged

        If band = 1 Then

            'SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Cheked
            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ", @Devolucion = " & chkDevolucion.Checked

            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then
                    grd.Rows(0).Selected = True
                    txtID.Text = grd.Rows(0).Cells(0).Value
                    LlenarGrid_Items()
                End If

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub rdAnuladas_CheckedChanged(sender As Object, e As EventArgs) Handles rdAnuladas.CheckedChanged

        btnGuardar.Enabled = Not rdAnuladas.Checked
        btnEliminar.Enabled = Not rdAnuladas.Checked
        btnNuevo.Enabled = Not rdAnuladas.Checked
        btnCancelar.Enabled = Not rdAnuladas.Checked

        Label21.Visible = rdAnuladas.Checked
        lblAutorizado.Visible = rdAnuladas.Checked

        bolModo = False


        'If chkVentas.Checked = True Then
        '    subSQL = "spPedidosWEB_Select_All"
        'Else
        '    subSQL = "spDevoluciones_PedidosWEB_Select_All"
        'End If

        'SQL = "exec " + subSQL + " @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Cheked

        SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ", @Devolucion = " & chkDevolucion.Checked

        Try
            LlenarGrilla()

            'If grd.Rows.Count > 0 Then
            'LlenarGrid_Items()
            'End If
            CambiarColores()

        Catch ex As Exception

        End Try


    End Sub

    Private Sub rdTodasPed_CheckedChanged(sender As Object, e As EventArgs) Handles rdTodasPed.CheckedChanged

        If band = 1 Then

            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ",@Devolucion = " & chkDevolucion.Checked

            bolModo = False
            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then
                    LlenarGrid_Items()
                End If
                CambiarColores()
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter Then
            'SendKeys.Send("{TAB}")
            txtCantidad.Focus()
        End If
    End Sub

    Private Sub cmbProducto_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyUp

        If cmbProducto.Text = "" Then
            lblStock.Text = ""
            txtPrecioVta.Text = ""
            txtCantidad.Text = ""
            txtPeso.Text = ""
            txtSubtotal.Text = ""
        End If

    End Sub

    Private Sub txtPeso_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPeso.KeyDown
        If txtUnidad.Text = "HORMA" Or txtUnidad.Text = "TIRA" Then
            txtCantidad_KeyDown(sender, e)
        End If
    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then

            If cmbProducto.SelectedValue Is DBNull.Value Or cmbProducto.SelectedValue = 0 Then
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                '               GoTo Continuar
                Exit Sub
            End If

            If cmbProducto.Text = "" Then
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                '              GoTo Continuar
                Exit Sub
            End If

            If txtPrecioVta.Text = "" Or txtPrecioVta.Text = "0.00" Then
                Util.MsgStatus(Status1, "El precio del producto no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El precio del producto no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If CDbl(txtPrecioVta.Text) = 0 Then
                Util.MsgStatus(Status1, "El precio del producto no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El precio del producto no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If lblPromo.Visible = True Then
                If txtValorPromo.Text = "" Or txtValorPromo.Text = "0.00" Then
                    Util.MsgStatus(Status1, "El precio del producto no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El precio del producto no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
                If CDbl(txtValorPromo.Text) = 0 Then
                    Util.MsgStatus(Status1, "El precio del producto no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El precio del producto no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            If txtCantidad.Text = "" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If CDbl(txtCantidad.Text) = 0 Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            'If MDIPrincipal.sucursal.Contains("PERON") Then
            'If CDbl(lblStock.Text < 0) Or CDbl(txtCantidad.Text) > CDbl(lblStock.Text) Then

            '    If txtIdUnidad.Text <> "PACK" And txtIdUnidad.Text <> "BOLSA" Then
            '        If MessageBox.Show("No hay Stock suficiente, desea abrir un pack?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '            MDIPrincipal.DesdePedidos = False
            '            Dim pack As New frmAbriPack
            '            pack.ShowDialog()
            '            If MDIPrincipal.actualizarstock = True Then
            '                cmbProducto_SelectedValueChanged(sender, e)
            '            End If
            '            Exit Sub
            '        End If
            '    End If
            '    If MessageBox.Show("No hay Stock suficiente, desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            '        Exit Sub
            '    End If
            'End If
            'End If


            If txtUnidad.Text.Contains("HORMA") Or txtUnidad.Text.Contains("TIRA") Then
                If txtPeso.Text = "" Then
                    Util.MsgStatus(Status1, "Debe ingresar el peso del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Debe ingresar el peso del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
                If CDbl(txtPeso.Text) = 0 Then
                    Util.MsgStatus(Status1, "Debe ingresar un peso válido del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Debe ingresar un peso válido del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbProducto.Text = grdItems.Rows(i).Cells(2).Value Then
                    If MessageBox.Show("El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (i + 1).ToString & ". Desea cargar el producto igual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            Next

            'grdItems.Rows.Add(0, cmbProducto.SelectedValue, cmbProducto.Text, txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtPrecioVta.Text, 0, txtSubtotal.Text, txtCantidad.Text, txtCantidad.Text, txtPeso.Text, "CUMPLIDO", 0, Date.Now.ToString, i + 1, "", False, "Eliminado")
            Dim promo As String = ""
            Dim bitpromo As Boolean = False
            If lblPromo.Visible = True Then
                'agrego la leyenda de promo
                promo = "(PROMO)"
                'coloco el bit de promo
                bitpromo = True
                'igualo el precio de venta con el de promo
                txtPrecioVta.Text = txtValorPromo.Text
            End If

            If chkVentas.Checked = True Then

                '----------------version (1)------------------------
                ''me fijo si el cliente pertenece a la lista mayorista
                'If txtIDPrecioLista.Text = "3" Then
                '    'llamo a la funcion para buscar promo 
                '    Dim res As Integer = BuscarPromoPorkys()
                '    'controlo los valores que devuelve la funcion 
                '    If res = 1 Then
                '        grdItems.Rows.Add(0, i + 1, cmbProducto.SelectedValue, cmbProducto.Text + "(PROMO)", txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtCantidad.Text, txtPeso.Text, Math.Round((PrecioPromo - DescuentoPromo), 2), 0, "", txtSubtotal.Text, txtCantidad.Text, 0, "CUMPLIDO", Date.Now.ToString, "", 0, False, 0, False, "Eliminado")
                '    ElseIf res = -1 Then
                '        If MessageBox.Show("Error al consultar promoción. Desea cargar el producto igual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            grdItems.Rows.Add(0, i + 1, cmbProducto.SelectedValue, cmbProducto.Text, txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtCantidad.Text, txtPeso.Text, txtPrecioVta.Text, 0, "", txtSubtotal.Text, txtCantidad.Text, 0, "CUMPLIDO", Date.Now.ToString, "", 0, False, 0, False, "Eliminado")
                '            Exit Sub
                '        End If
                '    Else
                '        grdItems.Rows.Add(0, i + 1, cmbProducto.SelectedValue, cmbProducto.Text, txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtCantidad.Text, txtPeso.Text, txtPrecioVta.Text, 0, "", txtSubtotal.Text, txtCantidad.Text, 0, "CUMPLIDO", Date.Now.ToString, "", 0, False, 0, False, "Eliminado")
                '    End If
                'Else
                grdItems.Rows.Add(0, i + 1, cmbProducto.SelectedValue, cmbProducto.Text + promo, txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtCantidad.Text, txtPeso.Text, txtPrecioVta.Text, 0, "", txtSubtotal.Text, txtCantidad.Text, 0, "CUMPLIDO", Date.Now.ToString, "", 0, False, 0, False, False, bitpromo, False, "Eliminado")
                'End If
                '----------------------------------------------------------------
            Else
                grdItems.Rows.Add(0, i + 1, cmbProducto.SelectedValue, cmbProducto.Text, txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtCantidad.Text, txtPeso.Text, txtPrecioVta.Text, 0, "", txtSubtotal.Text, txtCantidad.Text, 0, "CUMPLIDO", Date.Now.ToString, "", 0, False, 0, chkReintegrarStock.Checked, False, bitpromo, False, "Eliminado")
            End If


            Contar_Filas()
            CalculoSubtotales()

            OrdenarFilas()

            txtCodigoBarra.Text = ""
            txtCantidad.Text = ""
            cmbProducto.Text = ""
            txtPeso.Text = ""
            txtPrecioVta.Text = "0.00"
            lblStock.Text = "0.00"
            txtSubtotal.Text = "0.00"
            chkReintegrarStock.Checked = False
            lblPromo.Visible = False
            lblDescripcionPromo.Visible = False

            'chkPrecioMayorista.Checked = False
            'Continuar:
            'cmbProducto.SelectedIndex = 0
            txtNota.Focus()
            SendKeys.Send("{TAB}")
            If MDIPrincipal.sucursal.ToString.Contains("PRINCIPAL") Then
                txtCodigoBarra.Focus()
                SendKeys.Send("{TAB}")
            End If
            'cmbProducto.Focus()


        End If

    End Sub

    Private Sub txtPeso_TextChanged(sender As Object, e As EventArgs) Handles txtPeso.TextChanged
        Try
            If txtUnidad.Text.Contains("HORMA") Or txtUnidad.Text.Contains("TIRA") Then
                If txtPeso.Text = "" Then
                    txtSubtotal.Text = "0"
                Else
                    txtSubtotal.Text = Math.Round(CDbl(txtPeso.Text) * CDbl(txtPrecioVta.Text), 2)
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        Try
            If Not txtUnidad.Text.Contains("HORMA") And Not txtUnidad.Text.Contains("TIRA") Then
                If txtCantidad.Text = "" Then
                    txtSubtotal.Text = "0"
                Else
                    txtSubtotal.Text = Math.Round(CDbl(txtCantidad.Text) * CDbl(txtPrecioVta.Text), 2)
                End If
            End If



            'me fijo si el cliente pertenece a la lista mayorista
            If Habilitar_Promo = True And txtCantidad.Text <> "" Then 'If Habilitar_Promo = True And chkVentas.Checked = True And txtCantidad.Text <> "" Then
                If CDbl(txtCantidad.Text) > 0 Then
                    'llamo a la funcion para buscar promo 
                    BuscarPromoPorkys()
                Else
                    lblPromo.Visible = False
                    lblDescripcionPromo.Visible = False
                    txtValorPromo.Visible = False
                End If
            Else
                lblPromo.Visible = False
                lblDescripcionPromo.Visible = False
                txtValorPromo.Visible = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged
        If band = 1 Then

            Dim sqlstring As String = "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,PrecioMayoristaPeron,PrecioPeron " & _
                                         " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                         " JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                         " where m.Codigo = '" & cmbProducto.SelectedValue & "' AND s.idalmacen = " & Utiles.numero_almacen

            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)



            ds_Producto.Dispose()

            Try
                'If Principal Then
                '    If txtIDPrecioLista.Text = 3 Then
                '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(6), 2)
                '    ElseIf txtIDPrecioLista.Text = 4 Then
                '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(7), 2)
                '    ElseIf txtIDPrecioLista.Text = 5 Then
                '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(8), 2)
                '    ElseIf txtIDPrecioLista.Text = 10 Then
                '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(13), 2)
                '    ElseIf DescLista.Contains("NORTE") Then
                '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(13) * ValorNorte_cambio, 2)
                '    Else
                '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(9), 2)
                '    End If
                'Else
                If chkVentas2.Checked Then
                    txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(7), 2)
                Else
                    txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(6), 2)
                End If
                'End If

                txtIdUnidad.Text = ds_Producto.Tables(0).Rows(0)(0)
                unidad_unitario = txtIdUnidad.Text
                lblStock.Text = ds_Producto.Tables(0).Rows(0)(1)
                stock_unitario = lblStock.Text
                idproducto_unitario = ds_Producto.Tables(0).Rows(0)(2)
                producto_unitario = ds_Producto.Tables(0).Rows(0)(3)
                almacen_unitario = ds_Producto.Tables(0).Rows(0)(4)
                'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
                If (ds_Producto.Tables(0).Rows(0)(5)).ToString <> "" Then
                    If CDbl(stock_unitario) > CDbl(ds_Producto.Tables(0).Rows(0)(5)) Then
                        lblStock.BackColor = Color.Green
                    Else
                        lblStock.BackColor = Color.Red
                    End If
                End If
                txtMarca.Text = ds_Producto.Tables(0).Rows(0)(10)
                txtUnidad.Text = ds_Producto.Tables(0).Rows(0)(11)

                If txtUnidad.Text.Contains("HORMA") Or txtUnidad.Text.Contains("TIRA") Then
                    Label18.Text = "Peso*"
                Else
                    Label18.Text = "Peso"
                End If
                txtIDMarca.Text = ds_Producto.Tables(0).Rows(0)(12)

                'If MDIPrincipal.sucursal.Contains("PERON") Then
                '    If lblStock.BackColor = Color.Red And Not txtUnidad.Text.Contains("PACKBOLSAHORMATIRA") Then

                '        If MessageBox.Show("No hay Stock suficiente del producto " + cmbProducto.Text.ToString + ", desea abrir un PACK/BOLSA?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            'paso los valores a las variables globales de MDIPrincipal
                '            MDIPrincipal.DesdePedidos = False
                '            'abro la ventana de abrir pack
                '            Dim pack As New frmAbriPack
                '            pack.ShowDialog()
                '            'me fijo si se actualizo el stock para pasarle el valor al item
                '            If MDIPrincipal.actualizarstock Then
                '                'lblStock.Text = CDbl(stocknuevo)
                '                cmbProducto_SelectedValueChanged(sender, e)
                '            End If

                '        End If

                '    End If
                'End If


            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub txtPrecioVta_TextChanged(sender As Object, e As EventArgs) Handles txtPrecioVta.TextChanged
        Try
            If txtPrecioVta.Text = "" Then
                txtSubtotal.Text = 0
            Else
                If txtUnidad.Text.Contains("HORMA") Or Not Not txtUnidad.Text.Contains("TIRA") Then
                    If txtPeso.Text <> "" Then
                        txtSubtotal.Text = Math.Round(CDbl(txtPeso.Text) * CDbl(txtPrecioVta.Text), 2)
                    End If
                Else
                    If txtCantidad.Text <> "" Then
                        txtSubtotal.Text = Math.Round(CDbl(txtCantidad.Text) * CDbl(txtPrecioVta.Text), 2)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PicClientes_Click(sender As Object, e As EventArgs) Handles PicClientes.Click
        Dim C As New frmClientes
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbClientes.Text
        C.ShowDialog()
        'LlenarcmbFamilias_App(cmbFAMILIAS, ConnStringSEI)
        LlenarcmbClientes()
        cmbClientes.Text = texto_del_combo

        'cmbClientes_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub PicRepartidor_Click(sender As Object, e As EventArgs) Handles PicRepartidor.Click
        Dim R As New frmEmpleados
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbRepartidor.Text
        R.ShowDialog()
        'LlenarcmbFamilias_App(cmbFAMILIAS, ConnStringSEI)
        llenarcmbRepartidor()
        cmbRepartidor.Text = texto_del_combo
    End Sub

    Private Sub btnActualizarMat_Click(sender As Object, e As EventArgs) Handles btnActualizarMat.Click
        'Dim estadobol As Boolean = bolModo
        'Dim M As New frmMateriales
        'LLAMADO_POR_FORMULARIO = True
        'ARRIBA = 90
        'IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbProducto.Text
        'bolModo = False
        'M.ShowDialog()
        'LlenarcmbFamilias_App(cmbFAMILIAS, ConnStringSEI)
        LlenarCombo_Productos()
        cmbProducto.Text = texto_del_combo
        MsgBox("Se actualizó la lista de productos correctamente.")
        'bolModo = estadobol
    End Sub

    Private Sub btnActualizarMat_MouseHover(sender As Object, e As EventArgs) Handles btnActualizarMat.MouseHover
        ToolTip1.Show("Haga click para actualizar la lista de productos.", btnActualizarMat)
    End Sub

    Private Sub chkVenta_CheckedChanged(sender As Object, e As EventArgs) Handles chkVentas.CheckedChanged

        chkDevolucion.Checked = Not chkVentas.Checked
        chkPresupuesto.Enabled = chkVentas.Checked

        If chkVentas.Checked = True Then
            Try

                btnEliminar.Text = "Anular Envío"
                'limpio la grilla de items
                grdItems.Rows.Clear()
                'oculto la columna de reintegro a stock
                grdItems.Columns(22).Visible = False
                If desdeload = False And bolModo = False Then
                    rdTodasPed.Checked = True
                    'SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Cheked
                    SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ",@Devolucion = " & chkDevolucion.Checked
                    ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, SQL)
                    If ds.Tables(0).Rows.Count > 0 Then
                        LlenarGrilla()
                    Else
                        btnNuevo_Click(sender, e)
                    End If
                End If

            Catch ex As Exception

            End Try
            CambiarColores()
        End If
        desdeload = False
    End Sub

    Private Sub chkDevolucion_CheckedChanged(sender As Object, e As EventArgs) Handles chkDevolucion.CheckedChanged
        chkVentas.Checked = Not chkDevolucion.Checked
        chkReintegrarStock.Enabled = chkDevolucion.Checked
        chkPresupuesto.Enabled = Not chkDevolucion.Checked
        If chkDevolucion.Checked = True Then
            Try
                CambiarColores()
                btnEliminar.Text = "Anular Devolución"
                'limpio la grilla de items
                grdItems.Rows.Clear()
                'muestro la columna de reintegro a stock
                grdItems.Columns(22).Visible = True
                If desdeload = False And bolModo = False Then
                    rdTodasPed.Checked = True
                    SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ", @Devolucion = " & chkDevolucion.Checked
                    ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, SQL)
                    If ds.Tables(0).Rows.Count > 0 Then
                        LlenarGrilla()
                    Else
                        btnNuevo_Click(sender, e)
                    End If
                End If

            Catch ex As Exception

            End Try
            CambiarColores()
        End If

    End Sub

    Private Sub chkReintegrarStock_MouseHover(sender As Object, e As EventArgs) Handles chkReintegrarStock.MouseHover
        ToolTip1.Show("Haga click para que el producto que agregue a la lista se sume al stock.", chkReintegrarStock)
    End Sub

    Private Sub chkCodigos_CheckedChanged(sender As Object, e As EventArgs) Handles chkCodigos.CheckedChanged
        If chkCodigos.Checked Then
            chkCodigos.Text = "Por Código"
        Else
            chkCodigos.Text = "Por Cód.Barra"
        End If
        txtCodigoBarra.Focus()
    End Sub

    Private Sub chkCodigoss_MouseHover(sender As Object, e As EventArgs) Handles chkCodigos.MouseHover
        ToolTip1.Show("Seleccione como desea realizar la búsqueda.", chkCodigos)
    End Sub


    'Private Sub chkNorte.ChekedChanged(sender As Object, e As EventArgs)

    'If btnXNorte.Cheked = True Then
    '    btnXNorte.SymbolColor = Color.Green
    '    'GroupBox1.BackColor = SystemColors.GradientActiveCaption
    '    chkNorte.ForeColor = Color.DarkBlue
    '    'le agrego dos dias mas 
    '    dtpFECHA.MaxDate = Today.Date.AddDays(2)
    'Else
    '    btnXNorte.SymbolColor = Color.Red
    '    'GroupBox1.BackColor = SystemColors.Control
    '    chkNorte.ForeColor = SystemColors.HotTrack
    '    'como maximo el dia de hoy
    '    dtpFECHA.MaxDate = Today.Date
    'End If
    ''le aviso que recargue la grilla 
    'desdeload = False
    ''me fijo que operacion esta seleccionada
    'If chkVentas.Checked = True Then
    '    chkVenta_CheckedChanged(sender, e)
    'Else
    '    chkDevolucion_CheckedChanged(sender, e)
    'End If



    'End Sub

    Private Sub btnXNorte_Click(sender As Object, e As EventArgs) Handles btnXNorte.Click
        btnXNorte.Checked = Not btnXNorte.Checked

        If btnXNorte.Checked = True Then
            btnXNorte.SymbolColor = Color.Green
            'GroupBox1.BackColor = SystemColors.GradientActiveCaption
            'chkNorte.ForeColor = Color.DarkBlue
            'le agrego dos dias mas 
            dtpFECHA.MaxDate = Today.Date.AddDays(2)
        Else
            btnXNorte.SymbolColor = Color.Red
            'GroupBox1.BackColor = SystemColors.Control
            'chkNorte.ForeColor = SystemColors.HotTrack
            'como maximo el dia de hoy
            dtpFECHA.MaxDate = Today.Date
        End If
        'le aviso que recargue la grilla 
        desdeload = False
        'me fijo que operacion esta seleccionada
        If chkVentas.Checked = True Then
            chkVenta_CheckedChanged(sender, e)
        Else
            chkDevolucion_CheckedChanged(sender, e)
        End If
    End Sub

    Private Sub btnXNorte_MouseHover(sender As Object, e As EventArgs) Handles btnXNorte.MouseHover
        If btnXNorte.Checked = False Then
            ToolTip1.Show("Haga click para realizar una Venta Norte.", btnXNorte)
        Else
            ToolTip1.Show("Haga click para realizar una Venta Depósito.", btnXNorte)
        End If
    End Sub

    Private Sub chkPresupuesto_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresupuesto.CheckedChanged

        CambiarColores()
        FinalizarPresupuesto = False
        If bolModo = False Then
            btnActualizarMat.Enabled = chkPresupuesto.Checked
            txtValorPromo.Enabled = chkPresupuesto.Checked
            txtPrecioVta.Enabled = chkPresupuesto.Checked
            txtCantidad.Enabled = chkPresupuesto.Checked
            txtPeso.Enabled = chkPresupuesto.Checked
            cmbProducto.Enabled = chkPresupuesto.Checked
            PanelDescuento.Enabled = chkPresupuesto.Checked
        End If


    End Sub

    '--------------------------CAJA

    Private Sub btnIngresosDep_Click(sender As Object, e As EventArgs) Handles btnIngresos.Click
        Dim I As New frmAnticiposIngresosDep
        I.ShowDialog()
    End Sub

    Private Sub btnIngresos_MouseHover(sender As Object, e As EventArgs) Handles btnIngresos.MouseHover
        ToolTip1.Show("Haga click para realizar un ingreso.", btnIngresos)
    End Sub

    Private Sub btnApCaja_Click(sender As Object, e As EventArgs) Handles btnApCaja.Click
        Dim MP As New frmMovDia_Caja_Operacion
        MDIPrincipal.OperacionCaja = "Ap. Caja"
        MP.ShowDialog()
    End Sub

    Private Sub btnApCaja_MouseHover(sender As Object, e As EventArgs) Handles btnApCaja.MouseHover
        ToolTip1.Show("Haga click para realizar una apertura de caja.", btnApCaja)
    End Sub

    Private Sub btnGastos_Click(sender As Object, e As EventArgs) Handles btnGastos.Click
        Dim MP As New frmMovDia_Caja_Operacion
        MDIPrincipal.OperacionCaja = "Gastos"
        MP.ShowDialog()
    End Sub

    Private Sub btnGastos_MouseHover(sender As Object, e As EventArgs) Handles btnGastos.MouseHover
        ToolTip1.Show("Haga click para ingresar un gasto.", btnGastos)
    End Sub

    Private Sub btnRetiros_Click(sender As Object, e As EventArgs) Handles btnRetiros.Click
        Dim MP As New frmMovDia_Caja_Operacion
        MDIPrincipal.OperacionCaja = "Retiros"
        MP.ShowDialog()
    End Sub

    Private Sub btnRetiros_MouseHover(sender As Object, e As EventArgs) Handles btnRetiros.MouseHover
        ToolTip1.Show("Haga click para realizar un retiro.", btnGastos)
    End Sub

    '---------------------------

    Private Sub chkMayorista_CheckedChanged(sender As Object, e As EventArgs) Handles chkVentas2.CheckedChanged
        If bolModo Then
            Dim dsCliente As Data.DataSet
            Try
                If grdItems.Rows.Count > 0 Then
                    For i As Integer = 0 To grdItems.Rows.Count - 1
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioMayorista,PrecioCosto from Materiales where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'")
                        If chkVentas2.Checked Then
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                        Else
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(1)
                        End If

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value) - IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value, 2)
                        ''Me fijo si es horma o tira 
                        'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Then
                        '    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
                        'Else
                        '    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round((grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
                        'End If
                    Next
                    CalculoSubtotales()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtCodigoBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigoBarra.KeyDown

        If txtCodigoBarra.Text <> "" Then
            If e.KeyCode = Keys.Enter Then


                Dim sqlstring As String = ""
                If chkCodigos.Checked Then
                    sqlstring = "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,PrecioMayoristaPeron,PrecioPeron " & _
                                          " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                          " JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                          " where m.Codigo = '" & txtCodigoBarra.Text & "' AND m.Eliminado = 0 AND s.idalmacen = " & Utiles.numero_almacen
                Else
                    sqlstring = "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,PrecioMayoristaPeron,PrecioPeron " & _
                                           " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                           " JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                           " where m.CodigoBarra = '" & txtCodigoBarra.Text & "' AND m.Eliminado = 0 AND s.idalmacen = " & Utiles.numero_almacen
                End If


                ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Producto.Dispose()

                Try
                    band = 0
                    cmbProducto.SelectedValue = ds_Producto.Tables(0).Rows(0)(2).ToString
                    band = 1
                    cmbProducto.Text = ds_Producto.Tables(0).Rows(0)(3).ToString
                    'If Principal Then
                    '    If txtIDPrecioLista.Text = 3 Then
                    '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(6), 2)
                    '    ElseIf txtIDPrecioLista.Text = 4 Then
                    '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(7), 2)
                    '    ElseIf txtIDPrecioLista.Text = 5 Then
                    '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(8), 2)
                    '    ElseIf txtIDPrecioLista.Text = 10 Then
                    '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(13), 2)
                    '    ElseIf DescLista.Contains("NORTE") Then
                    '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(13) * ValorNorte_cambio, 2)
                    '    Else
                    '        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(9), 2)
                    '    End If
                    'Else
                    If chkVentas2.Checked Then
                        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(7), 2)
                    Else
                        txtPrecioVta.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(6), 2)
                    End If
                    'End If

                    txtIdUnidad.Text = ds_Producto.Tables(0).Rows(0)(0)
                    unidad_unitario = txtIdUnidad.Text
                    lblStock.Text = ds_Producto.Tables(0).Rows(0)(1)
                    stock_unitario = lblStock.Text
                    idproducto_unitario = ds_Producto.Tables(0).Rows(0)(2)
                    producto_unitario = ds_Producto.Tables(0).Rows(0)(3)
                    almacen_unitario = ds_Producto.Tables(0).Rows(0)(4)
                    'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
                    If (ds_Producto.Tables(0).Rows(0)(5)).ToString <> "" Then
                        If CDbl(stock_unitario) > CDbl(ds_Producto.Tables(0).Rows(0)(5)) Then
                            lblStock.BackColor = Color.Green
                        Else
                            lblStock.BackColor = Color.Red
                        End If
                    End If
                    txtMarca.Text = ds_Producto.Tables(0).Rows(0)(10)
                    txtUnidad.Text = ds_Producto.Tables(0).Rows(0)(11)

                    If txtUnidad.Text.Contains("HORMA") Or txtUnidad.Text.Contains("TIRA") Then
                        Label18.Text = "Peso*"
                    Else
                        Label18.Text = "Peso"
                    End If
                    txtIDMarca.Text = ds_Producto.Tables(0).Rows(0)(12)

                    'If MDIPrincipal.sucursal.Contains("PERON") Then
                    '    If lblStock.BackColor = Color.Red And Not txtUnidad.Text.Contains("PACKBOLSAHORMATIRA") Then

                    '        If MessageBox.Show("No hay Stock suficiente del producto " + cmbProducto.Text.ToString + ", desea abrir un PACK/BOLSA?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '            'paso los valores a las variables globales de MDIPrincipal
                    '            MDIPrincipal.DesdePedidos = False
                    '            'abro la ventana de abrir pack
                    '            Dim pack As New frmAbriPack
                    '            pack.ShowDialog()
                    '            'me fijo si se actualizo el stock para pasarle el valor al item
                    '            If MDIPrincipal.actualizarstock Then
                    '                'lblStock.Text = CDbl(stocknuevo)
                    '                cmbProducto_SelectedValueChanged(sender, e)
                    '            End If

                    '        End If

                    '    End If
                    'End If
                    txtCantidad.Focus()

                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub txtCodigoBarra_GotFocus(sender As Object, e As EventArgs) Handles txtCodigoBarra.GotFocus
        txtCodigoBarra.BackColor = Color.Aqua
        band = 0
        'cambio el modo de seleccion
        grdItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'me fijo si hay algo en la grilla para sacar la seleccion del primer item
        If grdItems.Rows.Count > 0 Then
            grdItems.Rows(0).Selected = False
        End If
    End Sub

    Private Sub txtCodigoBarra_LostFocus(sender As Object, e As EventArgs) Handles txtCodigoBarra.LostFocus
        txtCodigoBarra.BackColor = SystemColors.Window
        band = 1
    End Sub

    Private Sub chkTransferencia_CheckedChanged(sender As Object, e As EventArgs) Handles chkTransferencia.CheckedChanged

        If chkTransferencia.Checked And bolModo Then
            'Try

            '    Dim sqlstring As String = "update [" & NameTable_NotificacionesWEB & "] set BloqueoT = 1"
            '    tranWEB.Sql_Set(sqlstring)
            'Catch ex As Exception

            'End Try
        Else
            'Try
            '    Dim sqlstring As String = "update [" & NameTable_NotificacionesWEB & "] set BloqueoT = 0"
            '    tranWEB.Sql_Set(sqlstring)
            'Catch ex As Exception

            'End Try
        End If

    End Sub

    Private Sub chkHabilitar_EditGrilla_CheckedChanged(sender As Object, e As EventArgs) Handles chkHabilitar_EditGrilla.CheckedChanged
        grdItems.Columns(ColumnasDelGridItems.QtyEnviada).ReadOnly = Not chkHabilitar_EditGrilla.Checked
        grdItems.Columns(ColumnasDelGridItems.Peso).ReadOnly = Not chkHabilitar_EditGrilla.Checked
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).Visible = chkHabilitar_EditGrilla.Checked
        txtNroProcesado.Visible = chkHabilitar_EditGrilla.Checked
        If chkHabilitar_EditGrilla.Checked = False Then
            txtNroProcesado.Text = ""
        End If
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Ventas Depósito"

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
        lblNroPedido.Tag = "1"
        dtpFECHA.Tag = "2"
        txtidAlmacen.Tag = "3"
        cmbAlmacenes.Tag = "4"
        txtIdCliente.Tag = "5"
        cmbClientes.Tag = "6"
        lblSubtotal.Tag = "7"
        txtDescuento.Tag = "8"
        lblIVA.Tag = "9"
        lblTotal.Tag = "10"
        txtNota.Tag = "11"
        lblFechaEntrega.Tag = "12"
        lblLugarEntrega.Tag = "13"
        'lblStatus.Tag = "14"
        chkDescuento.Tag = "15"
        rdPorcentaje.Tag = "16"
        rdAbsoluto.Tag = "17"
        cmbRepartidor.Tag = "18"
        chkTransferencia.Tag = "21"
        chkFacturaCancelada.Tag = "24"
        chkPresupuesto.Tag = "25"

    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If cmbClientes.Text.Trim = "Deposito Peron" And cmbAlmacenes.SelectedValue = 2 Then
            Util.MsgStatus(Status1, "No se puede realizar una venta al mismo Depósito.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No se puede realizar una venta al mismo Depósito.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If cmbClientes.Text.Trim = "Deposito Principal" And cmbAlmacenes.SelectedValue = 1 Then
            Util.MsgStatus(Status1, "No se puede realizar una venta al mismo Depósito.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No se puede realizar una venta al mismo Depósito.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If


        If lblSubtotal.Text = "" Or lblTotal.Text = "" Then
            Util.MsgStatus(Status1, "El total o Subtotal debe ser válido.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El total o Subtotal debe ser válido.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(lblSubtotal.Text) = 0 Or CDbl(lblTotal.Text) = 0 Then
            Util.MsgStatus(Status1, "El total o Subtotal no debe ser igual a cero.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El total o Subtotal no debe ser igual a cero.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If chkDescuento.Checked = True Then
            If txtDescuento.Text = "" Then
                Util.MsgStatus(Status1, "El descuento debe ser válido.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "El descuento debe ser válido.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
            If CDbl(txtDescuento.Text) = 0 Then
                Util.MsgStatus(Status1, "El descuento no debe ser igual a cero.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "El descuento no debe ser igual a cero.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        End If

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer

        Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String

        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : ubicacion = "" : observaciones = ""

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
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is System.DBNull.Value Then
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

                'controlo el precio 
                Try
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value Then
                        Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    Else
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = 0 Then
                            Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    End If
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value Then
                        Util.MsgStatus(Status1, "El subtotal es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "El subtotal es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If


                    'control cuando se hace una devolucion en base de un nro de orden 
                    If chkDevolucion.Checked And chkHabilitar_EditGrilla.Checked Then
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                                Util.MsgStatus(Status1, "El peso especificado supera al saldo: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "El peso especificado supera al saldo: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                                Exit Sub
                            End If
                        Else
                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                                Util.MsgStatus(Status1, "La cantidad especificada supera al saldo: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "La cantidad especificada supera al saldo: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                                Exit Sub
                            End If
                        End If

                    End If


                Catch ex As Exception
                    Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End Try


                'control con aquellos productos que se facturan por peso 
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then

                    Try
                        'qty es válida?
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value Is System.DBNull.Value Then
                            Util.MsgStatus(Status1, "Falta completar el peso en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "Falta completar el peso en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Util.MsgStatus(Status1, "El peso debe ser válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "El peso debe ser válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End Try
                End If

                Try
                    'qty es válida?
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is System.DBNull.Value Then
                        Util.MsgStatus(Status1, "Falta completar la cantidad a enviar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "Falta completar la cantidad a enviar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If

                Catch ex As Exception
                    Util.MsgStatus(Status1, "La cantidad a enviar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "La cantidad a enviar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End Try

                'si tiene saldo, controlamos que no se pase..
                If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value.ToString.Contains("CUMPLIDO") Then
                    If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Is DBNull.Value Then
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                            Util.MsgStatus(Status1, "La cantidad a enviar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "La cantidad a enviar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    End If
                End If

                If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value Is DBNull.Value Then
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value <= 0 Then
                        'If MessageBox.Show("¿El stock del producto en la fila: " + (i + 1).ToString + " no es suficiente para realizar el envío, desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '    GoTo saltar
                        'Else
                        '    Util.MsgStatus(Status1, "El stock del producto  a enviar no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        '    Util.MsgStatus(Status1, "El stock del producto a enviar no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        '    Exit Sub
                        'End If
                    End If
                    'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value < grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Then
                    '    Util.MsgStatus(Status1, "El stock del producto no es suficiente para realizar el envío en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    '    Util.MsgStatus(Status1, "El stock del producto no es suficiente para realizar el envío en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    '    Exit Sub
                    'End If
                    'saltar:
                End If



            End If
        Next i

        Dim buscandoalgunmov As Boolean = False

        For i = 0 To grdItems.RowCount - 1
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value > 0 Then
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
        End With
    End Sub

    Private Sub LlenarGrid_Items()

        grdItems.Rows.Clear()

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

            If lblNroPedido.Text = "" Then
                sqltxt2 = "exec spPedidosWEB_Det_Select_By_IDPedidosWEB @IdPedidosWEB = '1'"
            Else
                sqltxt2 = "exec spPedidosWEB_Det_Select_By_IDPedidosWEB @IdPedidosWEB = '" & lblNroPedido.Text & "'"
            End If

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), dt.Rows(i)(4).ToString(), dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(), dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(), dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(), dt.Rows(i)(12).ToString(), dt.Rows(i)(13).ToString(), dt.Rows(i)(14).ToString(), dt.Rows(i)(15).ToString(), dt.Rows(i)(16).ToString(), dt.Rows(i)(17).ToString(), dt.Rows(i)(18).ToString(), dt.Rows(i)(19).ToString(), dt.Rows(i)(20).ToString(), dt.Rows(i)(21).ToString(), dt.Rows(i)(22).ToString(), dt.Rows(i)(23).ToString(), dt.Rows(i)(24).ToString())
            Next

            ControlBonif_Promo()


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        'cambio el fonde de la grilla de items
        If btnXNorte.Checked = True Then
            With grdItems
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128)
                .RowsDefaultCellStyle.BackColor = Color.White
            End With
        Else
            With grdItems
                .AlternatingRowsDefaultCellStyle.BackColor = Color.PaleGreen
                .RowsDefaultCellStyle.BackColor = Color.White
            End With
        End If


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

    Private Sub LlenarComboClientes()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT C.Codigo, ltrim(rtrim(C.nombre)) as Nombre from PedidosWEB o JOIN Clientes C ON C.Codigo = o.IdCliente where STATUS = 'P' AND o.eliminado= 0 order by ltrim(rtrim(C.nombre))")
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

    Private Sub Contar_Filas()

        lblCantidadFilas.Text = grdItems.RowCount

    End Sub

    Private Sub CalcularMontoIVA()
        'If band = 1 Then
        '    If txtIVA.Text = "" Then txtIVA.Text = "0"
        '    If txtMontoIVA10.Text = "" Then txtMontoIVA10.Text = "0"
        '    If txtMontoIVA27.Text = "" Then txtMontoIVA27.Text = "0"
        '    If txtSubtotal.Text = "" Then txtSubtotal.Text = "0"
        '    If txtSubtotalExento.Text = "" Then txtSubtotalExento.Text = "0"
        '    lblMontoIva.Text = Format(CDbl(txtMontoIVA10.Text) + CDbl(txtMontoIVA21.Text) + CDbl(txtMontoIVA27.Text), "###0.00")
        '    lblTotal.Text = Format(CDbl(txtSubtotalExento.Text) + CDbl(txtSubtotal.Text) + CDbl(lblMontoIva.Text) + CDbl(lblImpuestos.Text), "###0.00")
        'End If
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

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT 0 AS 'Codigo', '' AS 'Nombre' Union SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE Eliminado = 0 ORDER BY Nombre")
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

    Private Sub DesbloquearComponentes(ByRef habilitar As Boolean)
        dtpFECHA.Enabled = habilitar
        cmbAlmacenes.Enabled = habilitar
        cmbClientes.Enabled = habilitar
        'txtNota.Enabled = True
        'cmbPedidos.Enabled = habilitar
        'lblNroPedido.Enabled = habilitar
        PanelDescuento.Enabled = habilitar
        cmbRepartidor.Enabled = habilitar
        'chkTransferencia.Enabled = habilitar
        'chkPresupuesto.AutoCheck = habilitar
        ' chkCambiarPrecios.Enabled = habilitar
        'chkCodigos.Enabled = habilitar
        'cmbProducto.Enabled = habilitar
        'btnActualizarMat.Enabled = habilitar
        'txtValorPromo.Enabled = habilitar
        'txtPrecioVta.Enabled = habilitar
        'txtCantidad.Enabled = habilitar
        'txtPeso.Enabled = habilitar
        'txtCodigoBarra.Enabled = habilitar
        'btnDescargarPedidos.Enabled = Not habilitar
        rdTodasPed.Enabled = Not habilitar
        rdPendientes.Enabled = Not habilitar
        rdAnuladas.Enabled = Not habilitar

        PicClientes.Enabled = habilitar
        'PicRepartidor.Enabled = habilitar

        chkVentas.AutoCheck = Not habilitar
        chkDevolucion.AutoCheck = Not habilitar
        btnXNorte.Enabled = Not habilitar

        lblPromo.Visible = habilitar
        lblDescripcionPromo.Visible = habilitar
        txtValorPromo.Visible = habilitar
    End Sub

    'Private Sub LlenarcmbPedidos()
    '    Dim ds_OC As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    llenandoCombo = True

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        llenandoCombo = False
    '        Exit Sub
    '    End Try

    '    Try

    '        ds_OC = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '' AS 'OrdenPedido' Union select OrdenPedido from PedidosWEB where " & _
    '                                        " idCliente = '" & cmbClientes.SelectedValue & "'  and eliminado= 0 and status in ('P')")
    '        ds_OC.Dispose()

    '        With Me.cmbPedidos
    '            .DataSource = ds_OC.Tables(0).DefaultView
    '            ''.DisplayMember = "OrdenPedido"
    '            .ValueMember = "OrdenPedido"
    '            '.AutoCompleteMode = AutoCompleteMode.Suggest
    '            '.AutoCompleteSource = AutoCompleteSource.ListItems
    '            '.DropDownStyle = ComboBoxStyle.DropDown
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

    Private Sub CalculoSubtotales()

        Dim suma As Double = 0

        If bolModo = True Or chkPresupuesto.Checked = True Then
            For i As Integer = 0 To grdItems.Rows.Count - 1

                suma = suma + grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value

            Next

            Dim Descuento As Double
            Dim Total As Double

            lblSubtotal.Text = Math.Round(suma, 2)
            'me fijo si se aplico un descuento
            If chkDescuento.Checked = True Then
                If txtDescuento.Text = "" Or txtDescuento.Text = "0" Then
                    Descuento = "0"
                Else
                    Descuento = txtDescuento.Text
                End If

                If rdAbsoluto.Checked = True Then
                    Total = Math.Round(suma - Descuento, 2)
                    lblDescuento.Text = Descuento
                Else

                    Dim ValorDescuento As Double
                    ValorDescuento = Math.Round(suma * (Descuento / 100), 2)
                    lblDescuento.Text = ValorDescuento
                    Total = Math.Round(suma - ValorDescuento, 2)
                End If
            Else
                Total = suma
            End If
            lblTotal.Text = Math.Round(Total, 2)
        End If

    End Sub

    Private Sub validar_NumerosReales2( _
      ByVal sender As Object, _
      ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridItems.QtyPedido Then

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

    Private Sub ControlItem()

        Dim connection As SqlClient.SqlConnection = Nothing

        Try

            For i As Integer = 0 To grdItems.RowCount - 1

                'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value.ToString = "PENDIENTE" Then
                grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value = 0.0
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = 0.0
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value = 0.0
                'End If

                'me fijo si el item no esta cumplido
                If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value.ToString = "CUMPLIDO" Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Style.BackColor = Color.LightBlue
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        Dim preciocalculado As Double
                        preciocalculado = grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value / IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPaCK).Value = 0, 1, grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPaCK).Value)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = Math.Round(preciocalculado, 2)
                        grdItems.Columns(ColumnasDelGridItems.Peso).Visible = True
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value = 0.0
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Style.BackColor = Color.LightBlue
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).ReadOnly = False
                        grdItems.Columns(ColumnasDelGridItems.Peso).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
                    Else
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).ReadOnly = True
                    End If
                Else
                    'dejo la columna de qtyenviada en false
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).ReadOnly = True
                End If

                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos para consultar el stock", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT QTY FROM STOCK WHERE IDMaterial = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "' AND IDAlmacen = " & IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text) & "")
                ds.Dispose()

                grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value = ds.Tables(0).Rows(0).Item(0)

                'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value < grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then

                '    If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("PACKBOLSA") Then

                '        If MessageBox.Show("No hay Stock suficiente del producto " + grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString + ", desea abrir un PACK/BOLSA?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            'paso los valores a las variables globales de MDIPrincipal
                '            MDIPrincipal.DesdePedidos = True
                '            producto_unitario = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value
                '            idproducto_unitario = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                '            stock_unitario = grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value
                '            unidad_unitario = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value
                '            almacen_unitario = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
                '            'abro la ventana de abrir pack
                '            Dim pack As New frmAbriPack
                '            pack.ShowDialog()
                '            'me fijo si se actualizo el stock para pasarle el valor al item
                '            If MDIPrincipal.actualizarstock Then
                '                grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value = CDbl(stocknuevo)
                '            End If

                '        End If
                '    End If
                'End If

                'coloco la cantidad enviada en cero
                'grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value = 0
            Next
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Imprimir_Pedido(ByVal OrdenPedido As String)
        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        'Dim codigo As String
        Dim Solicitud As Boolean

        Dim saldo As Double = CDbl(lblSaldo.Text.Replace("$", ""))
        rpt.OrdenesDeCompra_Maestro_App(OrdenPedido, saldo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
  
        cerroparametrosconaceptar = False
        param = Nothing
        cnn = Nothing
        'End If
    End Sub

    Private Sub OcultarItemsEliminados()
        'control para aquellos item que esten eliminados 
        If rdAnuladas.Checked = False Then
            For i As Integer = 0 To grdItems.RowCount - 1
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value = True Then
                    grdItems.CurrentCell = Nothing
                    grdItems.Rows(i).Visible = False
                End If
            Next
        End If
    End Sub

    'Private Sub DevolverEstado()

    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Try

    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try
    '    sqlstring_ped = "UPDATE PedidosWEB SET EnProceso = 0 Where OrdenPedido = '" & lblNroPedido.Text.ToString & "'"
    '    ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
    '    Try
    '        tranWEB.Sql_Set(sqlstring_ped)
    '    Catch ex As Exception
    '        MsgBox("Error al cambiar estado del pedido " + lblNroPedido.Text.ToString)
    '    End Try
    'End Sub

    Private Sub llenarcmbRepartidor()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT '00' AS 'Codigo',' ' As 'Vendedor' Union SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 and Repartidor = 1 ORDER BY Vendedor")
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

    Public Sub LlenarcmbClientes()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select codigo, Nombre as 'Clientes' From Clientes Where eliminado = 0 ORDER BY Clientes")
            ds.Dispose()

            With cmbClientes
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Clientes"
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

    Private Sub LlenarCombo_Productos()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, (Nombre + ' ** $ ' + CONVERT(VARCHAR(10), PrecioCosto)) as 'Producto' FROM Materiales  WHERE nombre not like '%**FR%' and eliminado = 0 ")
            ds_Equipos.Dispose()

            With Me.cmbProducto
                .DataSource = ds_Equipos.Tables(0).DefaultView
                .DisplayMember = "Producto"
                .ValueMember = "Codigo"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
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

    Private Sub OrdenarFilas()
        Try
            'acomodo los datos de la grilla
            'grdItems.Sort(grdItems.Columns(1), System.ComponentModel.ListSortDirection.Descending)
            grdItems.Sort(grdItems.Columns(ColumnasDelGridItems.Orden_Item), System.ComponentModel.ListSortDirection.Descending)
            grdItems.ClearSelection()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ControlBonif_Promo()

        For i As Integer = 0 To grdItems.Rows.Count - 1
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonificacion).Value = "True" Then
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value + "(BONIF.)"
            End If
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Promo).Value = "True" Then
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value + "(PROMO)"
            End If

        Next

    End Sub

    Private Sub CambiarColores()

        If btnXNorte.Checked = True Then

            GroupBox1.ForeColor = Color.Black
            Label17.ForeColor = Color.Black
            Label4.ForeColor = Color.Black
            Label14.ForeColor = Color.Black
            Label16.ForeColor = Color.Black
            Label18.ForeColor = Color.Black
            Label13.ForeColor = Color.Black
            lblFechaEntrega.ForeColor = Color.Black
            lblLugarEntrega.ForeColor = Color.Black
            lblDescripcionPromo.ForeColor = Color.Blue
            lblPromo.ForeColor = Color.Blue

            If chkVentas.Checked = True Then
                If chkPresupuesto.Checked = True Then
                    GroupBox1.BackColor = Color.FromArgb(192, 255, 192)
                Else
                    GroupBox1.BackColor = Color.LemonChiffon
                End If
            Else
                GroupBox1.BackColor = Color.NavajoWhite
            End If

            With grd
                .AlternatingRowsDefaultCellStyle.BackColor = Color.Khaki
                .RowsDefaultCellStyle.BackColor = Color.White
            End With

        Else


            GroupBox1.ForeColor = Color.Blue
            Label17.ForeColor = Color.Blue
            Label4.ForeColor = Color.Blue
            Label14.ForeColor = Color.Blue
            Label16.ForeColor = Color.Blue
            Label18.ForeColor = Color.Blue
            Label13.ForeColor = Color.Blue
            lblFechaEntrega.ForeColor = Color.Blue
            lblLugarEntrega.ForeColor = Color.Blue
            lblDescripcionPromo.ForeColor = Color.Green
            lblPromo.ForeColor = Color.Green

            If chkVentas.Checked = True Then
                If chkPresupuesto.Checked = True Then
                    GroupBox1.BackColor = Color.FromArgb(192, 255, 192)
                Else
                    GroupBox1.BackColor = SystemColors.Control
                End If
            Else
                GroupBox1.BackColor = SystemColors.GradientActiveCaption
            End If

                With grd
                    .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
                    .RowsDefaultCellStyle.BackColor = Color.White
                End With

            End If

    End Sub

    Private Sub Borrar_tmpStockmov()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Truncate Table tmpStockMov_TransRecepWEB")
        ds.Dispose()

    End Sub


#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro_PedidosWEB() As Integer
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

                Dim param_ordenpedido As New SqlClient.SqlParameter
                param_ordenpedido.ParameterName = "@ORDENPEDIDO"
                param_ordenpedido.SqlDbType = SqlDbType.VarChar
                param_ordenpedido.Size = 25
                If bolModo Then
                    param_ordenpedido.Value = DBNull.Value
                Else
                    param_ordenpedido.Value = lblNroPedido.Text
                End If
                param_ordenpedido.Direction = ParameterDirection.InputOutput

                Dim param_idAlmacen As New SqlClient.SqlParameter
                param_idAlmacen.ParameterName = "@IDALMACEN"
                param_idAlmacen.SqlDbType = SqlDbType.BigInt
                param_idAlmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
                param_idAlmacen.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@IDCLIENTE"
                param_idcliente.SqlDbType = SqlDbType.VarChar
                param_idcliente.Size = 25
                param_idcliente.Value = IIf(txtIdCliente.Text = "", cmbClientes.SelectedValue, txtIdCliente.Text)
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@FECHA"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                param_fecha.Direction = ParameterDirection.Input

                Dim param_fechaentrega As New SqlClient.SqlParameter
                param_fechaentrega.ParameterName = "@FECHAENTREGA"
                param_fechaentrega.SqlDbType = SqlDbType.DateTime
                param_fechaentrega.Value = dtpFECHA.Value
                param_fechaentrega.Direction = ParameterDirection.Input

                Dim param_lugarentrega As New SqlClient.SqlParameter
                param_lugarentrega.ParameterName = "@LUGARENTREGA"
                param_lugarentrega.SqlDbType = SqlDbType.VarChar
                param_lugarentrega.Size = 500
                param_lugarentrega.Value = lblLugarEntrega.Text
                param_lugarentrega.Direction = ParameterDirection.Input

                Dim param_Subtotal As New SqlClient.SqlParameter
                param_Subtotal.ParameterName = "@SUBTOTAL"
                param_Subtotal.SqlDbType = SqlDbType.Decimal
                param_Subtotal.Precision = 18
                param_Subtotal.Size = 2
                param_Subtotal.Value = lblSubtotal.Text
                param_Subtotal.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@IVA"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Size = 2
                param_iva.Value = lblIVA.Text
                param_iva.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@TOTAL"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Size = 2
                param_total.Value = lblTotal.Text
                param_total.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 250
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_cancelado As New SqlClient.SqlParameter
                param_cancelado.ParameterName = "@CANCELADO"
                param_cancelado.SqlDbType = SqlDbType.Bit
                param_cancelado.Value = 0 'MDIPrincipal.RealizarPago_Completo 'chkFacturaCancelada.Checked
                param_cancelado.Direction = ParameterDirection.Input

                Dim param_fechacancelado As New SqlClient.SqlParameter
                param_fechacancelado.ParameterName = "@FECHACANCELADO"
                param_fechacancelado.SqlDbType = SqlDbType.DateTime
                'If param_cancelado.Value = True Then
                '    param_fechacancelado.Value = Date.Now
                'End If
                param_fechacancelado.Value = DBNull.Value
                param_fechacancelado.Direction = ParameterDirection.Input

                Dim param_deuda As New SqlClient.SqlParameter
                param_deuda.ParameterName = "@DEUDA"
                param_deuda.SqlDbType = SqlDbType.Decimal
                param_deuda.Precision = 18
                param_total.Size = 2
                'If param_cancelado.Value = True Then
                '    param_deuda.Value = 0
                'Else
                param_deuda.Value = lblTotal.Text
                'End If
                param_deuda.Direction = ParameterDirection.Input

                Dim param_Descuento As New SqlClient.SqlParameter
                param_Descuento.ParameterName = "@DESCUENTO"
                param_Descuento.SqlDbType = SqlDbType.Bit
                param_Descuento.Value = chkDescuento.Checked
                param_Descuento.Direction = ParameterDirection.Input

                Dim param_Porcentaje As New SqlClient.SqlParameter
                param_Porcentaje.ParameterName = "@PORCENTAJE"
                param_Porcentaje.SqlDbType = SqlDbType.Bit
                param_Porcentaje.Value = rdPorcentaje.Checked
                param_Porcentaje.Direction = ParameterDirection.Input

                Dim param_valorDescuento As New SqlClient.SqlParameter
                param_valorDescuento.ParameterName = "@VALORDESCUENTO"
                param_valorDescuento.SqlDbType = SqlDbType.Decimal
                param_valorDescuento.Precision = 18
                param_valorDescuento.Scale = 2
                param_valorDescuento.Value = IIf(txtDescuento.Text = "", 0, txtDescuento.Text)
                param_valorDescuento.Direction = ParameterDirection.Input

                Dim param_idusuario As New SqlClient.SqlParameter
                param_idusuario.ParameterName = "@IDEMPLEADO"
                param_idusuario.SqlDbType = SqlDbType.Int
                param_idusuario.Value = IIf(txtIDRepartidor.Text = "", cmbRepartidor.SelectedValue.ToString.Replace("0", ""), txtIDRepartidor.Text.Replace("0", ""))
                param_idusuario.Direction = ParameterDirection.Input

                Dim param_transferencia As New SqlClient.SqlParameter
                param_transferencia.ParameterName = "@TRANSFERENCIA"
                param_transferencia.SqlDbType = SqlDbType.Bit
                param_transferencia.Value = chkTransferencia.Checked
                param_transferencia.Direction = ParameterDirection.Input

                Dim param_ventasnorte As New SqlClient.SqlParameter
                param_ventasnorte.ParameterName = "@VENTAS_NORTE"
                param_ventasnorte.SqlDbType = SqlDbType.Bit
                param_ventasnorte.Value = btnXNorte.Checked
                param_ventasnorte.Direction = ParameterDirection.Input

                Dim param_devolucion As New SqlClient.SqlParameter
                param_devolucion.ParameterName = "@DEVOLUCION"
                param_devolucion.SqlDbType = SqlDbType.Bit
                param_devolucion.Value = chkDevolucion.Checked
                param_devolucion.Direction = ParameterDirection.Input


                Dim param_presupuesto As New SqlClient.SqlParameter
                param_presupuesto.ParameterName = "@PRESUPUESTO"
                param_presupuesto.SqlDbType = SqlDbType.Bit
                param_presupuesto.Value = chkPresupuesto.Checked
                param_presupuesto.Direction = ParameterDirection.Input

                Dim param_finpresu As New SqlClient.SqlParameter
                param_finpresu.ParameterName = "@FINPRESU"
                param_finpresu.SqlDbType = SqlDbType.Bit
                param_finpresu.Value = FinalizarPresupuesto
                param_finpresu.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                'If bolModo = True Then
                param_useradd.ParameterName = "@useradd"
                'Else
                '    param_useradd.ParameterName = "@userupd"
                'End If
                param_useradd.SqlDbType = SqlDbType.Int
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try


                    'If chkVentas.Checked = True Then
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Insert", _
                                    param_id, param_ordenpedido, param_idAlmacen, param_idcliente, param_fecha, param_cancelado, param_fechacancelado, param_deuda, param_finpresu, _
                                    param_Subtotal, param_iva, param_total, param_Descuento, param_Porcentaje, param_valorDescuento, param_ventasnorte, param_devolucion, param_presupuesto, _
                                    param_nota, param_idusuario, param_fechaentrega, param_lugarentrega, param_transferencia, param_useradd, param_res)
                    'Else
                    '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_PedidosWEB_Insert", _
                    '            param_id, param_ordenpedido, param_idAlmacen, param_idcliente, param_fecha, param_cancelado, param_deuda, _
                    '            param_Subtotal, param_iva, param_total, param_Descuento, param_Porcentaje, param_valorDescuento, param_ventasnorte, _
                    '            param_nota, param_idusuario, param_fechaentrega, param_lugarentrega, param_transferencia, param_useradd, param_res)
                    'End If
                    If FinalizarPresupuesto = False Then
                        txtID.Text = param_id.Value
                    End If
                    lblNroPedido.Text = param_ordenpedido.Value
                    AgregarActualizar_Registro_PedidosWEB = param_res.Value







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

    Private Function AgregarRegistro_PedidosWEB_Items() As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False
        'Dim ds_Empresa As Data.DataSet

        Dim ValorActual As Double
        Dim IdStockMov As Long
        Dim Stock As Double


        Dim Comprob As String

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count

                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) > 0 Then

                        ValorActual = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value

                        Dim param_id As New SqlClient.SqlParameter
                        param_id.ParameterName = "@id"
                        param_id.SqlDbType = SqlDbType.BigInt
                        param_id.Value = DBNull.Value
                        param_id.Direction = ParameterDirection.InputOutput

                        Dim param_idpedidosweb As New SqlClient.SqlParameter
                        param_idpedidosweb.ParameterName = "@IDPEDIDOSWEB"
                        param_idpedidosweb.SqlDbType = SqlDbType.VarChar
                        param_idpedidosweb.Size = 25
                        param_idpedidosweb.Value = lblNroPedido.Text 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
                        param_idpedidosweb.Direction = ParameterDirection.Input

                        Dim param_idAlmacen As New SqlClient.SqlParameter
                        param_idAlmacen.ParameterName = "@IDALMACEN"
                        param_idAlmacen.SqlDbType = SqlDbType.BigInt
                        param_idAlmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
                        param_idAlmacen.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@IDMATERIAL"
                        param_idmaterial.SqlDbType = SqlDbType.VarChar
                        param_idmaterial.Size = 25
                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                        param_idmaterial.Direction = ParameterDirection.Input
                        'MsgBox(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString)

                        Dim param_marca As New SqlClient.SqlParameter
                        param_marca.ParameterName = "@IDMARCA"
                        param_marca.SqlDbType = SqlDbType.VarChar
                        param_marca.Size = 25
                        param_marca.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value)
                        param_marca.Direction = ParameterDirection.Input

                        Dim param_idunidad As New SqlClient.SqlParameter
                        param_idunidad.ParameterName = "@IDUNIDAD"
                        param_idunidad.SqlDbType = SqlDbType.VarChar
                        param_idunidad.Size = 25
                        param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
                        param_idunidad.Direction = ParameterDirection.Input

                        Dim param_precio As New SqlClient.SqlParameter
                        param_precio.ParameterName = "@PRECIO"
                        param_precio.SqlDbType = SqlDbType.Decimal
                        param_precio.Precision = 18
                        param_precio.Scale = 2
                        'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        'param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value) * ((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) / grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                        'Else
                        param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                        'End If
                        param_precio.Direction = ParameterDirection.Input


                        Dim param_qtyenviada As New SqlClient.SqlParameter
                        param_qtyenviada.ParameterName = "@QTYENVIADA"
                        param_qtyenviada.SqlDbType = SqlDbType.Decimal
                        param_qtyenviada.Precision = 18
                        param_qtyenviada.Scale = 2
                        param_qtyenviada.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
                        param_qtyenviada.Direction = ParameterDirection.Input

                        'esta variable es necesaria para las devoluciones que se hacen en base a un nro de venta
                        Dim param_qtysaldo As New SqlClient.SqlParameter
                        param_qtysaldo.ParameterName = "@QTYSALDO"
                        param_qtysaldo.SqlDbType = SqlDbType.Decimal
                        param_qtysaldo.Precision = 18
                        param_qtysaldo.Scale = 2
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                            param_qtysaldo.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value, Decimal)
                        Else
                            param_qtysaldo.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
                        End If
                        param_qtysaldo.Direction = ParameterDirection.Input

                        Dim param_subtotal As New SqlClient.SqlParameter
                        param_subtotal.ParameterName = "@SUBTOTAL"
                        param_subtotal.SqlDbType = SqlDbType.Decimal
                        param_subtotal.Precision = 18
                        param_subtotal.Scale = 2
                        param_subtotal.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value)
                        param_subtotal.Direction = ParameterDirection.Input

                        Dim param_iva As New SqlClient.SqlParameter
                        param_iva.ParameterName = "@IVA"
                        param_iva.SqlDbType = SqlDbType.Decimal
                        param_iva.Precision = 18
                        param_iva.Scale = 2
                        param_iva.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value)
                        param_iva.Direction = ParameterDirection.Input

                        Dim param_notadet As New SqlClient.SqlParameter
                        param_notadet.ParameterName = "@NOTA_DET"
                        param_notadet.SqlDbType = SqlDbType.VarChar
                        param_notadet.Size = 300
                        param_notadet.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
                        param_notadet.Direction = ParameterDirection.Input

                        Dim param_ordenitem As New SqlClient.SqlParameter
                        param_ordenitem.ParameterName = "@ORDENITEM"
                        param_ordenitem.SqlDbType = SqlDbType.SmallInt
                        param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
                        param_ordenitem.Direction = ParameterDirection.Input

                        Dim param_fechacumplido As New SqlClient.SqlParameter
                        param_fechacumplido.ParameterName = "@FECHACUMPLIDO"
                        param_fechacumplido.SqlDbType = SqlDbType.DateTime
                        param_fechacumplido.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                        param_fechacumplido.Direction = ParameterDirection.Input

                        Dim param_UnidadFac As New SqlClient.SqlParameter
                        param_UnidadFac.ParameterName = "@UnidadFac"
                        param_UnidadFac.SqlDbType = SqlDbType.Decimal
                        param_UnidadFac.Precision = 18
                        param_UnidadFac.Scale = 2
                        param_UnidadFac.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value)
                        param_UnidadFac.Direction = ParameterDirection.Input

                        Dim param_useradd As New SqlClient.SqlParameter
                        param_useradd.ParameterName = "@useradd"
                        param_useradd.SqlDbType = SqlDbType.Int
                        param_useradd.Value = UserID
                        param_useradd.Direction = ParameterDirection.Input

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

                        Dim param_valorDescuento As New SqlClient.SqlParameter
                        param_valorDescuento.ParameterName = "@DESCUENTO"
                        param_valorDescuento.SqlDbType = SqlDbType.Decimal
                        param_valorDescuento.Precision = 18
                        param_valorDescuento.Scale = 2
                        param_valorDescuento.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value)
                        param_valorDescuento.Direction = ParameterDirection.Input

                        Dim param_reinstock As New SqlClient.SqlParameter
                        param_reinstock.ParameterName = "@REINTEGRAR_STOCK"
                        param_reinstock.SqlDbType = SqlDbType.Bit
                        param_reinstock.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value = True, 1, 0)
                        param_reinstock.Direction = ParameterDirection.Input

                        Dim param_bonificacion As New SqlClient.SqlParameter
                        param_bonificacion.ParameterName = "@BONIFICACION"
                        param_bonificacion.SqlDbType = SqlDbType.Bit
                        param_bonificacion.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonificacion).Value = True, 1, 0)
                        param_bonificacion.Direction = ParameterDirection.Input

                        Dim param_promo As New SqlClient.SqlParameter
                        param_promo.ParameterName = "@PROMO"
                        param_promo.SqlDbType = SqlDbType.Bit
                        param_promo.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Promo).Value = True, 1, 0)
                        param_promo.Direction = ParameterDirection.Input

                        Dim param_devolucion As New SqlClient.SqlParameter
                        param_devolucion.ParameterName = "@DEVOLUCION"
                        param_devolucion.SqlDbType = SqlDbType.Bit
                        param_devolucion.Value = chkDevolucion.Checked
                        param_devolucion.Direction = ParameterDirection.Input

                        Dim param_presupuesto As New SqlClient.SqlParameter
                        param_presupuesto.ParameterName = "@PRESUPUESTO"
                        param_presupuesto.SqlDbType = SqlDbType.Bit
                        param_presupuesto.Value = chkPresupuesto.Checked
                        param_presupuesto.Direction = ParameterDirection.Input

                        Dim param_finpresu As New SqlClient.SqlParameter
                        param_finpresu.ParameterName = "@FINPRESU"
                        param_finpresu.SqlDbType = SqlDbType.Bit
                        param_finpresu.Value = FinalizarPresupuesto
                        param_finpresu.Direction = ParameterDirection.Input

                        Dim param_procesado As New SqlClient.SqlParameter
                        param_procesado.ParameterName = "@PROCESADO"
                        param_procesado.SqlDbType = SqlDbType.Bit
                        param_procesado.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Procesado).Value = True, 1, 0)
                        param_procesado.Direction = ParameterDirection.Input

                        Dim param_nroprocesado As New SqlClient.SqlParameter
                        param_nroprocesado.ParameterName = "@ORDENPROCESADO"
                        param_nroprocesado.SqlDbType = SqlDbType.VarChar
                        param_nroprocesado.Size = 25
                        param_nroprocesado.Value = txtNroProcesado.Text
                        param_nroprocesado.Direction = ParameterDirection.Input

                        'Dim param_control As New SqlClient.SqlParameter
                        'param_control.ParameterName = "@CONTROL"
                        'param_control.SqlDbType = SqlDbType.SmallInt
                        'param_control.Value = CONTROL
                        'param_control.Direction = ParameterDirection.Input
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

                            'If chkVentas.Checked = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Det_Insert", _
                                               param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, param_presupuesto, param_finpresu, _
                                               param_marca, param_idunidad, param_qtyenviada, param_UnidadFac, param_precio, param_valorDescuento, param_devolucion, _
                                               param_subtotal, param_iva, param_notadet, param_useradd, param_ordenitem, param_fechacumplido, param_bonificacion, param_promo, _
                                               param_qtysaldo, param_procesado, param_nroprocesado, param_reinstock, param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)
                            'Else
                            '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_PedidosWEB_Det_Insert", _
                            '                             param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
                            '                             param_marca, param_idunidad, param_qtyenviada, param_UnidadFac, param_precio, param_valorDescuento, _
                            '                             param_subtotal, param_iva, param_notadet, param_useradd, param_ordenitem, param_fechacumplido, param_reinstock, param_bonificacion, param_promo, _
                            '                             param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)
                            'End If


                            'MsgBox(param_msg.Value.ToString)

                            res = param_res.Value
                            Comprob = param_Comprob.Value
                            Stock = param_Stock.Value
                            IdStockMov = param_IdStockMov.Value

                            If Not (param_msg.Value Is System.DBNull.Value) Then
                                msg = param_msg.Value
                            Else
                                msg = ""
                            End If
                            If (res <= 0) Then
                                Exit Do
                            End If

                            'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                            'Dim tipo As String
                            'If chkVentas.Checked Then
                            '    tipo = "V"
                            'Else
                            '    tipo = "I"
                            'End If
                            'If chkTransferencia.Checked = False Then
                            '    If tipo = "V" Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value = True Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value = 1 Then
                            '        ' si devuelve true actualizo local
                            '        If MDIPrincipal.InsertarMovWEB(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value,
                            '                          grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value,
                            '                          Utiles.numero_almacen, tipo, CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal),
                            '                          CType(Stock, Decimal), IdStockMov, Comprob, 4, UserID) Then

                            '            ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                            '            ds_Empresa.Dispose()

                            '        End If
                            '    End If
                            'Else
                            '    If tipo = "V" Then
                            '        Dim sqlstring As String = " INSERT INTO [dbo].[tmpStockMov_TransRecepWEB]([NroMov],[IDAlmacen],[IDMaterial],[IDUnidad]," & _
                            '                           " [IDMotivo],[IDStockMov],[Tipo],[Stock],[Qty],[Comprobante],[Eliminado],[DateAdd],[UserAdd])VALUES('" & _
                            '                            lblNroPedido.Text & "'," & Utiles.numero_almacen & ",'" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "','" & _
                            '                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value & "',4," & IdStockMov & ",'" & tipo & "'," & CType(Stock, Decimal) & "," & _
                            '                            CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & ",'" & Comprob & "',0, GETDATE()," & UserID & ")"

                            '        ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, sqlstring)
                            '        ds_Empresa.Dispose()
                            '    End If
                            'End If

                            'End If

                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                    i = i + 1

                Loop

                AgregarRegistro_PedidosWEB_Items = res

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

    Private Function EliminarRegistro_PedidosWEB() As Integer
        Dim res As Integer = 0
        'Dim msg As String
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

                'Dim param_idRecepcion As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                'param_idRecepcion.Value = CType(txtID.Text, Long)
                'param_idRecepcion.Direction = ParameterDirection.Input


                Dim param_ordenpedido As New SqlClient.SqlParameter
                param_ordenpedido.ParameterName = "@ORDENPEDIDO"
                param_ordenpedido.SqlDbType = SqlDbType.VarChar
                param_ordenpedido.Size = 25
                param_ordenpedido.Value = lblNroPedido.Text
                param_ordenpedido.Direction = ParameterDirection.Input

                Dim param_autorizado As New SqlClient.SqlParameter
                param_autorizado.ParameterName = "@IDAutoriza"
                param_autorizado.SqlDbType = SqlDbType.VarChar
                param_autorizado.Size = 25
                param_autorizado.Value = MDIPrincipal.IDEmpleadoAutoriza
                param_autorizado.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 250
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

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

                'Dim param_msg As New SqlClient.SqlParameter
                'param_msg.ParameterName = "@mensaje"
                'param_msg.SqlDbType = SqlDbType.VarChar
                'param_msg.Size = 250
                'param_msg.Value = DBNull.Value
                'param_msg.Direction = ParameterDirection.Output

                Try
                    'If chkVentas.Checked = True Then
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Delete", param_ordenpedido, param_nota, param_autorizado, param_userdel, param_res)
                    res = param_res.Value
                    'Else
                    '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_PedidosWEB_Delete", param_ordenpedido, param_userdel, param_res)
                    '    res = param_res.Value
                    'End If


                    'If Not (param_msg.Value Is System.DBNull.Value) Then
                    '    msg = param_msg.Value
                    'Else
                    '    msg = ""
                    'End If

                    EliminarRegistro_PedidosWEB = res

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

    Private Function EliminarRegistro_PediosWEB_Items() As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False
        Dim ds_Empresa As Data.DataSet
        Dim ValorActual As Double
        Dim IdStockMov As Long
        Dim Stock As Double


        Dim Comprob As String

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count

                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) > 0 Then

                        ValorActual = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value

                        Dim param_id As New SqlClient.SqlParameter
                        param_id.ParameterName = "@id"
                        param_id.SqlDbType = SqlDbType.BigInt
                        param_id.Value = DBNull.Value
                        param_id.Direction = ParameterDirection.InputOutput

                        Dim param_idpedidosweb As New SqlClient.SqlParameter
                        param_idpedidosweb.ParameterName = "@IDPEDIDOSWEB"
                        param_idpedidosweb.SqlDbType = SqlDbType.VarChar
                        param_idpedidosweb.Size = 25
                        param_idpedidosweb.Value = lblNroPedido.Text 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
                        param_idpedidosweb.Direction = ParameterDirection.Input

                        Dim param_idAlmacen As New SqlClient.SqlParameter
                        param_idAlmacen.ParameterName = "@IDALMACEN"
                        param_idAlmacen.SqlDbType = SqlDbType.BigInt
                        param_idAlmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
                        param_idAlmacen.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@IDMATERIAL"
                        param_idmaterial.SqlDbType = SqlDbType.VarChar
                        param_idmaterial.Size = 25
                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                        param_idmaterial.Direction = ParameterDirection.Input

                        'Dim param_marca As New SqlClient.SqlParameter
                        'param_marca.ParameterName = "@IDMARCA"
                        'param_marca.SqlDbType = SqlDbType.VarChar
                        'param_marca.Size = 25
                        'param_marca.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value)
                        'param_marca.Direction = ParameterDirection.Input

                        Dim param_idunidad As New SqlClient.SqlParameter
                        param_idunidad.ParameterName = "@IDUNIDAD"
                        param_idunidad.SqlDbType = SqlDbType.VarChar
                        param_idunidad.Size = 25
                        param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
                        param_idunidad.Direction = ParameterDirection.Input

                        'Dim param_precio As New SqlClient.SqlParameter
                        'param_precio.ParameterName = "@PRECIO"
                        'param_precio.SqlDbType = SqlDbType.Decimal
                        'param_precio.Precision = 18
                        'param_precio.Scale = 2
                        ''If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        ''param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value) * ((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) / grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                        ''Else
                        'param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                        ''End If
                        'param_precio.Direction = ParameterDirection.Input


                        Dim param_qtyenviada As New SqlClient.SqlParameter
                        param_qtyenviada.ParameterName = "@QTYENVIADA"
                        param_qtyenviada.SqlDbType = SqlDbType.Decimal
                        param_qtyenviada.Precision = 18
                        param_qtyenviada.Scale = 2
                        param_qtyenviada.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
                        param_qtyenviada.Direction = ParameterDirection.Input

                        'Dim param_subtotal As New SqlClient.SqlParameter
                        'param_subtotal.ParameterName = "@SUBTOTAL"
                        'param_subtotal.SqlDbType = SqlDbType.Decimal
                        'param_subtotal.Precision = 18
                        'param_subtotal.Scale = 2
                        'param_subtotal.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value)
                        'param_subtotal.Direction = ParameterDirection.Input

                        'Dim param_iva As New SqlClient.SqlParameter
                        'param_iva.ParameterName = "@IVA"
                        'param_iva.SqlDbType = SqlDbType.Decimal
                        'param_iva.Precision = 18
                        'param_iva.Scale = 2
                        'param_iva.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value)
                        'param_iva.Direction = ParameterDirection.Input

                        Dim param_notadet As New SqlClient.SqlParameter
                        param_notadet.ParameterName = "@NOTA_DET"
                        param_notadet.SqlDbType = SqlDbType.VarChar
                        param_notadet.Size = 300
                        param_notadet.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
                        param_notadet.Direction = ParameterDirection.Input

                        'Dim param_ordenitem As New SqlClient.SqlParameter
                        'param_ordenitem.ParameterName = "@ORDENITEM"
                        'param_ordenitem.SqlDbType = SqlDbType.SmallInt
                        'param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
                        'param_ordenitem.Direction = ParameterDirection.Input

                        'Dim param_fechacumplido As New SqlClient.SqlParameter
                        'param_fechacumplido.ParameterName = "@FECHACUMPLIDO"
                        'param_fechacumplido.SqlDbType = SqlDbType.DateTime
                        'param_fechacumplido.Value = dtpFECHA.Value
                        'param_fechacumplido.Direction = ParameterDirection.Input

                        'Dim param_UnidadFac As New SqlClient.SqlParameter
                        'param_UnidadFac.ParameterName = "@UnidadFac"
                        'param_UnidadFac.SqlDbType = SqlDbType.Decimal
                        'param_UnidadFac.Precision = 18
                        'param_UnidadFac.Scale = 2
                        'param_UnidadFac.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value)
                        'param_UnidadFac.Direction = ParameterDirection.Input

                        Dim param_devolucion As New SqlClient.SqlParameter
                        param_devolucion.ParameterName = "@DEVOLUCION"
                        param_devolucion.SqlDbType = SqlDbType.Bit
                        param_devolucion.Value = chkDevolucion.Checked
                        param_devolucion.Direction = ParameterDirection.Input

                        Dim param_reinstock As New SqlClient.SqlParameter
                        param_reinstock.ParameterName = "@REINTEGRAR_STOCK"
                        param_reinstock.SqlDbType = SqlDbType.Bit
                        param_reinstock.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value
                        param_reinstock.Direction = ParameterDirection.Input

                        Dim param_presupuesto As New SqlClient.SqlParameter
                        param_presupuesto.ParameterName = "@PRESUPUESTO"
                        param_presupuesto.SqlDbType = SqlDbType.Bit
                        param_presupuesto.Value = chkPresupuesto.Checked
                        param_presupuesto.Direction = ParameterDirection.Input

                        Dim param_useradd As New SqlClient.SqlParameter
                        param_useradd.ParameterName = "@userdel"
                        param_useradd.SqlDbType = SqlDbType.Int
                        param_useradd.Value = UserID
                        param_useradd.Direction = ParameterDirection.Input

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

                        'Dim param_valorDescuento As New SqlClient.SqlParameter
                        'param_valorDescuento.ParameterName = "@DESCUENTO"
                        'param_valorDescuento.SqlDbType = SqlDbType.Decimal
                        'param_valorDescuento.Precision = 18
                        'param_valorDescuento.Scale = 2
                        'param_valorDescuento.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value)
                        'param_valorDescuento.Direction = ParameterDirection.Input

                        'Dim param_control As New SqlClient.SqlParameter
                        'param_control.ParameterName = "@CONTROL"
                        'param_control.SqlDbType = SqlDbType.SmallInt
                        'param_control.Value = CONTROL
                        'param_control.Direction = ParameterDirection.Input
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

                            'If chkVentas.Checked = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Delete_Det", _
                                               param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
                                               param_idunidad, param_qtyenviada, param_reinstock, _
                                               param_notadet, param_useradd, param_devolucion, param_presupuesto, _
                                               param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)
                            'Else
                            '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_PedidosWEB_Delete_Det", _
                            '                       param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
                            '                       param_idunidad, param_qtyenviada, param_reinstock, _
                            '                       param_notadet, param_useradd, _
                            '                       param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)
                            'End If


                            'MsgBox(param_msg.Value.ToString)

                            res = param_res.Value
                            Comprob = param_Comprob.Value
                            Stock = param_Stock.Value
                            IdStockMov = param_IdStockMov.Value

                            If Not (param_msg.Value Is System.DBNull.Value) Then
                                msg = param_msg.Value
                            Else
                                msg = ""
                            End If
                            If (res <= 0) Then
                                Exit Do
                            End If

                            Dim tipo As String
                            'como es una anulación ahora los valores van invertidos (Venta = ingreso y Devolucion = Egreso)
                            If chkVentas.Checked Then
                                tipo = "I"
                            Else
                                tipo = "V"
                            End If
                            If chkTransferencia.Checked = False And DesdeAnular = False Then
                                If tipo = "I" Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value = True Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value = 1 Then
                                    ' si devuelve true actualizo local
                                    If MDIPrincipal.InsertarMovWEB(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value,
                                                      grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value,
                                                      Utiles.numero_almacen, tipo, CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal),
                                                      CType(Stock, Decimal), IdStockMov, Comprob, 4, UserID) Then

                                        ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                                        ds_Empresa.Dispose()

                                    End If
                                End If
                                'Else
                                '    If tipo = "V" Then
                                '        Dim sqlstring As String = " INSERT INTO [dbo].[tmpStockMov_TransRecepWEB]([NroMov],[IDAlmacen],[IDMaterial],[IDUnidad]," & _
                                '                           " [IDMotivo],[IDStockMov],[Tipo],[Stock],[Qty],[Comprobante],[Eliminado],[DateAdd],[UserAdd])VALUES('" & _
                                '                            lblNroPedido.Text & "'," & Utiles.numero_almacen & ",'" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "','" & _
                                '                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value & "',4," & IdStockMov & ",'" & tipo & "'," & CType(Stock, Decimal) & "," & _
                                '                            CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & ",'" & Comprob & "',0, GETDATE()," & UserID & ")"

                                '        ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, sqlstring)
                                '        ds_Empresa.Dispose()
                                '    End If
                            End If

                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                    i = i + 1

                Loop

                EliminarRegistro_PediosWEB_Items = res

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

    Private Function BuscarPromoPorkys() As Integer

        Dim res As Integer
        Try
            Try
                conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Dim param_idmaterial As New SqlClient.SqlParameter
            param_idmaterial.ParameterName = "@IDMATERIAL"
            param_idmaterial.SqlDbType = SqlDbType.VarChar
            param_idmaterial.Size = 25
            param_idmaterial.Value = cmbProducto.SelectedValue
            param_idmaterial.Direction = ParameterDirection.Input

            Dim param_cantidad As New SqlClient.SqlParameter
            param_cantidad.ParameterName = "@CANTIDAD"
            param_cantidad.SqlDbType = SqlDbType.Decimal
            param_cantidad.Precision = 18
            param_cantidad.Scale = 2
            param_cantidad.Value = txtCantidad.Text
            param_cantidad.Direction = ParameterDirection.Input

            Dim param_precio As New SqlClient.SqlParameter
            param_precio.ParameterName = "@PRECIO"
            param_precio.SqlDbType = SqlDbType.Decimal
            param_precio.Precision = 18
            param_precio.Scale = 2
            param_precio.Value = DBNull.Value
            param_precio.Direction = ParameterDirection.Output

            Dim param_descuento As New SqlClient.SqlParameter
            param_descuento.ParameterName = "@DESCUENTO"
            param_descuento.SqlDbType = SqlDbType.Decimal
            param_descuento.Precision = 18
            param_descuento.Scale = 2
            param_descuento.Value = DBNull.Value
            param_descuento.Direction = ParameterDirection.Output

            Dim param_descripcion As New SqlClient.SqlParameter
            param_descripcion.ParameterName = "@DESCRIPCION"
            param_descripcion.SqlDbType = SqlDbType.NVarChar
            param_descripcion.Size = 4000
            param_descripcion.Value = DBNull.Value
            param_descripcion.Direction = ParameterDirection.Output

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(conn_del_form, CommandType.StoredProcedure, "spPromociones_Porkys_Buscar", param_idmaterial, param_cantidad, param_precio, param_descuento, param_descripcion, param_res)
            DescuentoPromo = param_descuento.Value
            PrecioPromo = param_precio.Value
            DescripcionPromo = param_descripcion.Value
            res = param_res.Value

            'controlo los valores que devuelve la funcion 
            If res = 1 Then
                lblPromo.Visible = True
                lblDescripcionPromo.Visible = True
                txtValorPromo.Visible = True
                lblDescripcionPromo.Text = DescripcionPromo
                txtValorPromo.Text = Math.Round((PrecioPromo - DescuentoPromo), 2)

            ElseIf res = -1 Then
                Util.MsgStatus(Status1, "Error al consultar promoción.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Error al consultar promoción.", My.Resources.Resources.stop_error.ToBitmap, True)
            Else
                lblPromo.Visible = False
                lblDescripcionPromo.Visible = False
                txtValorPromo.Visible = False
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
        End Try
    End Function

    Private Function fila_vacia(ByVal i) As Boolean
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is Nothing) Then
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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDPedidosWEB_det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDPedidosWEB_det)
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

    'Private Function EliminarRegistro() As Integer

    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim res As Integer = 0
    '    Dim resweb As Integer = 0

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '        ''Abrir una transaccion para guardar y asegurar que se guarda todo
    '        'If Abrir_Tran(connection) = False Then
    '        '    MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '    Exit Function
    '        'End If

    '        Try

    '            Dim param_idordendecompra As New SqlClient.SqlParameter("@OrdenPedido", SqlDbType.VarChar, ParameterDirection.Input)
    '            param_idordendecompra.Size = 25
    '            param_idordendecompra.Value = lblNroPedido.Text
    '            param_idordendecompra.Direction = ParameterDirection.Input

    '            Dim param_nota As New SqlClient.SqlParameter
    '            param_nota.ParameterName = "@NOTA"
    '            param_nota.SqlDbType = SqlDbType.VarChar
    '            param_nota.Size = 150
    '            param_nota.Value = txtNota.Text
    '            param_nota.Direction = ParameterDirection.Input

    '            Dim param_userdel As New SqlClient.SqlParameter
    '            param_userdel.ParameterName = "@userdel"
    '            param_userdel.SqlDbType = SqlDbType.Int
    '            param_userdel.Value = UserID
    '            param_userdel.Direction = ParameterDirection.Input

    '            Dim param_res As New SqlClient.SqlParameter
    '            param_res.ParameterName = "@res"
    '            param_res.SqlDbType = SqlDbType.Int
    '            param_res.Value = DBNull.Value
    '            param_res.Direction = ParameterDirection.Output

    '            Try

    '                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPedidosWEB_Delete_Finalizar", param_idordendecompra, param_userdel, param_nota, param_res)

    '                res = param_res.Value

    '                If res > 0 Then

    '                    Try
    '                        Dim sqlstring As String

    '                        sqlstring = "exec spPedidosWEB_Delete_Finalizar '" & lblNroPedido.Text & "','" & txtNota.Text & "'," & UserID & ""

    '                        resweb = tranWEB.Sql_Get_Value(sqlstring)

    '                        If resweb < 0 Then
    '                            res = -1
    '                        End If

    '                    Catch ex As Exception

    '                        MsgBox("Desde spPedidosWEB_Delete_Finalizar : " + ex.Message)
    '                        res = -1

    '                    End Try


    '                End If

    '                EliminarRegistro = res

    '            Catch ex As Exception
    '                '' 
    '                Throw ex
    '            End Try
    '        Finally
    '            ''
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
    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try
    'End Function

    Private Function CalcularNroPedido() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        Try

            Dim param_ordenpedido As New SqlClient.SqlParameter
            param_ordenpedido.ParameterName = "@ORDENPEDIDO"
            param_ordenpedido.SqlDbType = SqlDbType.VarChar
            param_ordenpedido.Size = 25
            param_ordenpedido.Value = DBNull.Value
            param_ordenpedido.Direction = ParameterDirection.Output

            Dim param_devolucion As New SqlClient.SqlParameter
            param_devolucion.ParameterName = "@DEVOLUCION"
            param_devolucion.SqlDbType = SqlDbType.Bit
            param_devolucion.Value = chkDevolucion.Checked
            param_devolucion.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput


            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCalcular_NumeroPedidoWEB", param_ordenpedido, param_devolucion, param_res)

            lblNroPedido.Text = param_ordenpedido.Value
            CalcularNroPedido = param_res.Value

        Catch ex As Exception

            MsgBox(ex.Message + "Desde de Calcular Pedidos")

        End Try
    End Function

    Private Function AgregarRegistro_Ingreso() As Integer
        Dim res As Integer = 0

        Try

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                'If bolModo = True Then
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput
                'Else
                '    param_id.Value = txtID.Text
                '    param_id.Direction = ParameterDirection.Input
                'End If

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
                param_nropago.Value = ""
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
                param_contado.Value = True
                param_contado.Direction = ParameterDirection.Input

                Dim param_montocontado As New SqlClient.SqlParameter
                param_montocontado.ParameterName = "@montocontado"
                param_montocontado.SqlDbType = SqlDbType.Decimal
                param_montocontado.Precision = 18
                param_montocontado.Scale = 2
                param_montocontado.Value = IIf(lblTotal.Text = "", 0, lblTotal.Text)
                param_montocontado.Direction = ParameterDirection.Input

                Dim param_tarjeta As New SqlClient.SqlParameter
                param_tarjeta.ParameterName = "@tarjeta"
                param_tarjeta.SqlDbType = SqlDbType.Bit
                param_tarjeta.Value = False
                param_tarjeta.Direction = ParameterDirection.Input

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

                Dim param_ayid As New SqlClient.SqlParameter
                param_ayid.ParameterName = "@Ayid"
                param_ayid.SqlDbType = SqlDbType.Bit
                param_ayid.Value = False
                param_ayid.Direction = ParameterDirection.Input

                Dim param_montoAyid As New SqlClient.SqlParameter
                param_montoAyid.ParameterName = "@MontoAyid"
                param_montoAyid.SqlDbType = SqlDbType.Decimal
                param_montoAyid.Precision = 18
                param_montoAyid.Scale = 2
                param_montoAyid.Value = 0
                param_montoAyid.Direction = ParameterDirection.Input

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
                param_subtotal.Value = 0
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

                Dim param_Descuento As New SqlClient.SqlParameter
                param_Descuento.ParameterName = "@Descuento"
                param_Descuento.SqlDbType = SqlDbType.Decimal
                param_Descuento.Precision = 18
                param_Descuento.Scale = 2
                param_Descuento.Value = CDbl(IIf(lblDescuento.Text = "", 0, lblDescuento.Text))
                param_Descuento.Direction = ParameterDirection.Input

                Dim param_ValorCambio As New SqlClient.SqlParameter
                param_ValorCambio.ParameterName = "@ValorCambio"
                param_ValorCambio.SqlDbType = SqlDbType.Decimal
                param_ValorCambio.Precision = 18
                param_ValorCambio.Scale = 2
                param_ValorCambio.Value = 17.8
                param_ValorCambio.Direction = ParameterDirection.Input

                Dim param_remitos As New SqlClient.SqlParameter
                param_remitos.ParameterName = "@remitosasociados"
                param_remitos.SqlDbType = SqlDbType.VarChar
                param_remitos.Size = 300
                param_remitos.Value = lblNroPedido.Text
                param_remitos.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                'If bolModo = True Then
                param_useradd.ParameterName = "@useradd"
                'Else
                '    param_useradd.ParameterName = "@userupd"
                'End If
                param_useradd.SqlDbType = SqlDbType.Int
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    'SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Insert", _
                    '                      param_id, param_codigo, param_nropago, param_idcliente, param_fecha, param_contado, param_montocontado, _
                    '                      param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, _
                    '                      param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                    '                      param_montoiva, param_subtotal, param_total, param_Redondeo, param_ValorCambio, _
                    '                      param_remitos, param_useradd, param_res)

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Insert", _
                                             param_id, param_codigo, param_nropago, param_idcliente, param_fecha, param_contado, param_montocontado, _
                                             param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, _
                                             param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                             param_montoiva, param_subtotal, param_total, param_Redondeo, param_Descuento, param_ValorCambio, _
                                             param_remitos, param_ayid, param_montoAyid, param_useradd, param_res)

                    txtIDIngreso.Text = param_id.Value
                    txtCodigoIngreso.Text = param_codigo.Value

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

    Private Function AgregarRegistro_FacturasConsumos() As Integer
        Dim res As Integer = 0

        Try
            Try

                Dim param_idingreso As New SqlClient.SqlParameter
                param_idingreso.ParameterName = "@idingreso"
                param_idingreso.SqlDbType = SqlDbType.BigInt
                param_idingreso.Value = txtIDIngreso.Text
                param_idingreso.Direction = ParameterDirection.Input

                Dim param_idfacturacion As New SqlClient.SqlParameter
                param_idfacturacion.ParameterName = "@idfacturacion"
                param_idfacturacion.SqlDbType = SqlDbType.BigInt
                param_idfacturacion.Value = txtID.Text
                param_idfacturacion.Direction = ParameterDirection.Input

                Dim param_idconsumo As New SqlClient.SqlParameter
                param_idconsumo.ParameterName = "@idconsumo"
                param_idconsumo.SqlDbType = SqlDbType.BigInt
                param_idconsumo.Value = DBNull.Value
                param_idconsumo.Direction = ParameterDirection.Input

                Dim param_DEUDA As New SqlClient.SqlParameter
                param_DEUDA.ParameterName = "@Deuda"
                param_DEUDA.SqlDbType = SqlDbType.Decimal
                param_DEUDA.Precision = 18
                param_DEUDA.Scale = 2
                param_DEUDA.Value = 0
                param_DEUDA.Direction = ParameterDirection.Input

                Dim param_DeudaActual As New SqlClient.SqlParameter
                param_DeudaActual.ParameterName = "@DeudaActual"
                param_DeudaActual.SqlDbType = SqlDbType.Decimal
                param_DeudaActual.Precision = 18
                param_DeudaActual.Scale = 2
                param_DeudaActual.Value = lblTotal.Text
                param_DeudaActual.Direction = ParameterDirection.Input

                Dim param_CancelarTodo As New SqlClient.SqlParameter
                param_CancelarTodo.ParameterName = "@CancelarTodo"
                param_CancelarTodo.SqlDbType = SqlDbType.Bit
                param_CancelarTodo.Value = True
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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_FacturasConsumos_Insert", _
                                          param_idfacturacion, param_idconsumo, param_idingreso, param_DEUDA, param_DeudaActual, _
                                          param_CancelarTodo, param_FechaCanc, param_res)


                    res = param_res.Value

                Catch ex As Exception
                    Throw ex
                End Try

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

    Private Function BuscarSaldo() As Integer

        Dim Ventas As Double = 0
        Dim Devolucion As Double = 0
        Dim NC As Double = 0
        Dim Saldo As Double = 0


        Dim connection As SqlClient.SqlConnection = Nothing
        'Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            'ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT SUM(Deuda) FROM pedidosweb WHERE Eliminado = 0 AND Presupuesto = 0 And Devolucion = 0 AND Idcliente = '" & IIf(txtIdCliente.Text = "", cmbClientes.SelectedValue, cmbClientes.SelectedValue) & "'")
            'ds_Marcas.Dispose()

            'Ventas = IIf(ds_Marcas.Tables(0).Rows(0).Item(0) Is DBNull.Value, 0, ds_Marcas.Tables(0).Rows(0).Item(0))

            ''--------------------------------------------------------------------------------------------------------
            'ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT SUM(Deuda) FROM pedidosweb WHERE Eliminado = 0 AND Presupuesto = 0 And Devolucion = 1 AND Idcliente = '" & IIf(txtIdCliente.Text = "", cmbClientes.SelectedValue, cmbClientes.SelectedValue) & "'")
            'ds_Marcas.Dispose()
            'Devolucion = IIf(ds_Marcas.Tables(0).Rows(0).Item(0) Is DBNull.Value, 0, ds_Marcas.Tables(0).Rows(0).Item(0))


            'ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select sum(Total) from NotasCredito where  Eliminado = 0 and Utilizada = 0 and IdCliente = '" & IIf(txtIdCliente.Text = "", cmbClientes.SelectedValue, cmbClientes.SelectedValue) & "'")
            'ds_Marcas.Dispose()
            'NC = IIf(ds_Marcas.Tables(0).Rows(0).Item(0) Is DBNull.Value, 0, ds_Marcas.Tables(0).Rows(0).Item(0))


            'Saldo = Math.Round(Ventas - Devolucion - NC, 2)
            '--------------------------------------------------------------------------------------------------------


            Dim param_idcliente As New SqlClient.SqlParameter
            param_idcliente.ParameterName = "@Cliente"
            param_idcliente.SqlDbType = SqlDbType.VarChar
            param_idcliente.Size = 25
            param_idcliente.Value = IIf(txtIdCliente.Text = "", cmbClientes.SelectedValue, txtIdCliente.Text)
            param_idcliente.Direction = ParameterDirection.Input

            Dim param_total As New SqlClient.SqlParameter
            param_total.ParameterName = "@SALDO"
            param_total.SqlDbType = SqlDbType.Decimal
            param_total.Precision = 18
            param_total.Scale = 2
            param_total.Size = 2
            param_total.Value = DBNull.Value
            param_total.Direction = ParameterDirection.InputOutput

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spClientes_ControlSaldos", param_idcliente, param_total, param_res)

            If param_res.Value < 1 Then
                lblSaldo.ForeColor = Color.Red
            Else
                lblSaldo.ForeColor = Color.Green
            End If
            lblSaldo.Text = "$" + param_total.Value.ToString
            BuscarSaldo = param_res.Value

        Catch ex As Exception
            BuscarSaldo = 0
            lblSaldo.Text = "$0,00"
        End Try

    End Function

    Private Function AutorizarVenta(ByVal msj As String) As Boolean
        MDIPrincipal.Autorizar = False
        Dim Au As New frmUsuarioModo
        Au.ShowDialog()
        'me fijo si autorizó
        If MDIPrincipal.Autorizar = False Then
            Util.MsgStatus(Status1, msj)
            AutorizarVenta = False
        Else
            Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
            AutorizarVenta = True
        End If
    End Function

    Private Function BuscarAlmacen_Cliente() As Boolean
        'Verifico que el clientes sea otro distinto al almacen de ventas
        If cmbClientes.Text.Trim = "Deposito Peron" And cmbAlmacenes.SelectedValue <> 2 Then
            BuscarAlmacen_Cliente = True
            IDAlmacenTrans = 2
        ElseIf cmbClientes.Text.Trim = "Deposito Principal" And cmbAlmacenes.SelectedValue <> 1 Then
            IDAlmacenTrans = 1
            BuscarAlmacen_Cliente = True
        Else
            BuscarAlmacen_Cliente = False
            IDAlmacenTrans = 0
        End If
    End Function

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click


        band = 0

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        DesbloquearComponentes(True)

        lblNroPedido.Text = ""

        'lblStatus.Text = "En Proceso"
        'lblFechaEntrega.Text = Date.Now.ToShortDateString

        chkEliminado.Checked = False

        estado_chk = chkVentas.Checked
        estado_chkNorte = btnXNorte.Checked

        Util.LimpiarTextBox(Me.Controls)
        grdItems.Rows.Clear()


        btnXNorte.Checked = estado_chkNorte
        chkVentas.Checked = estado_chk

        'Dim res As Integer = 0
        'res = CalcularNroPedido()
        'If Not res > 0 Then
        '    lblNroPedido.Text = "Error"
        '    Exit Sub
        'End If

        Contar_Filas()

        lblSubtotal.Text = "0"
        lblIVA.Text = "0"
        lblTotal.Text = "0"
        'selecciono el almacen principal como defecto
        cmbAlmacenes.SelectedValue = Utiles.numero_almacen
        cmbProducto.Enabled = False
        chkDescuento.Checked = False
        txtDescuento.Text = "0"
        lblPromo.Visible = False
        lblDescripcionPromo.Visible = False
        txtValorPromo.Visible = False
        lblSaldo.ForeColor = Color.Black
        lblSaldo.Text = "$0"
        chkCodigos.Checked = True

        dtpFECHA.Value = Date.Today
        dtpFECHA.Focus()
        lblFechaEntrega.Text = dtpFECHA.Value.ToString

        If Principal = False Then
            Try
                cmbClientes.SelectedValue = "227"
                cmbRepartidor.SelectedValue = "14"
                cmbProducto.Enabled = True
                txtCodigoBarra.Focus()
            Catch ex As Exception

            End Try
        End If

        band = 1

        If chkDevolucion.Checked Then
            Dim D As New frmVentasWEB_PedirOrden
            D.ShowDialog()
            If grdItems.Rows.Count > 0 Then
                CalculoSubtotales()
                chkHabilitar_EditGrilla.Checked = True
            End If

        End If



    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If txtIdCliente.Text = "" Then
            MsgBox("Seleccione un Cliente para poder realizar la Venta", MsgBoxStyle.Critical, "Control de Errores")
            Exit Sub
        End If


        If bolModo = False And chkPresupuesto.Checked = True Then
            If MessageBox.Show("¿Está seguro que desea realizar una venta desde un presupuesto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                FinalizarPresupuesto = True
            Else
                FinalizarPresupuesto = False
                Exit Sub
            End If
        Else
            FinalizarPresupuesto = False
            If bolModo = False Then
                Exit Sub
            End If
        End If


        Dim res As Integer, res_item As Integer, res_ingreso As Integer, res_conyfac As Integer, ControlSaldo As Integer
        If ReglasNegocio() Then
            If bolModo Or chkPresupuesto.Checked = True Then
                Verificar_Datos()
            Else
                bolpoliticas = True
            End If
            If bolpoliticas Then
                If chkDevolucion.Checked = True Then
                    'Verifico que escriba una nota
                    If txtNota.Text.Trim = "" Then
                        Util.MsgStatus(Status1, "Por favor escriba el motivo de la devolución.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "Por favor escriba el motivo de la devolución.", My.Resources.stop_error.ToBitmap, True)
                        txtNota.Focus()
                        Exit Sub
                    End If
                End If
                'me fijo que sucursal es (por el momento no aplico el control de saldos
                'If Principal Then
                '    ControlSaldo = BuscarSaldo()
                'Else
                ControlSaldo = 1
                'End If
                If chkVentas.Checked Then
                    Dim texto As String
                    Select Case ControlSaldo
                        Case -1
                            'Util.MsgStatus(Status1, "El cliente seleccionado ha superado el monto máximo de su límite de crédito.", My.Resources.Resources.stop_error.ToBitmap)
                            'Util.MsgStatus(Status1, "El cliente seleccionado ha superado el monto máximo de su límite de crédito.", My.Resources.Resources.stop_error.ToBitmap, True)
                            'Exit Sub
                            If MessageBox.Show("El cliente seleccionado ha superado el monto máximo de su límite de crédito. Desea realizar la venta igual?(De ser positiva la respuesta, será necesario la autorización de un empleado autorizado.)", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                Util.MsgStatus(Status1, "El cliente seleccionado ha superado el monto máximo de su límite de crédito.")
                                Exit Sub
                            Else
                                texto = "El cliente seleccionado ha superado el monto máximo de su límite de crédito."
                                If AutorizarVenta(texto) = False Then
                                    Exit Sub
                                End If
                            End If
                        Case -2
                            'Util.MsgStatus(Status1, "El cliente seleccionado ha superado la cantidad de días máximo de pago.", My.Resources.Resources.stop_error.ToBitmap)
                            'Util.MsgStatus(Status1, "El cliente seleccionado ha superado la cantidad de días máximo de pago.", My.Resources.Resources.stop_error.ToBitmap, True)
                            'Exit Sub
                            If MessageBox.Show("El cliente seleccionado ha superado la cantidad de días máximo de pago. Desea realizar la venta igual?(De ser positiva la respuesta, será necesario la autorización de un empleado autorizado.)", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                Util.MsgStatus(Status1, "El cliente seleccionado ha superado la cantidad de días máximo de pago.")
                                Exit Sub
                            Else
                                texto = "El cliente seleccionado ha superado la cantidad de días máximo de pago."
                                If AutorizarVenta(texto) = False Then
                                    Exit Sub
                                End If
                            End If
                        Case -3
                            'Util.MsgStatus(Status1, "El cliente seleccionado ha superado el monto y días máximo de su límite de crédito.", My.Resources.Resources.stop_error.ToBitmap)
                            'Util.MsgStatus(Status1, "El cliente seleccionado ha superado el monto y días máximo de su límite de crédito.", My.Resources.Resources.stop_error.ToBitmap, True)
                            'Exit Sub
                            If MessageBox.Show("El cliente seleccionado ha superado el monto y días máximo de su límite de crédito. Desea realizar la venta igual?(De ser positiva la respuesta, será necesario la autorización de un empleado autorizado.)", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                Util.MsgStatus(Status1, "El cliente seleccionado ha superado el monto y días máximo de su límite de crédito.")
                                Exit Sub
                            Else
                                texto = "El cliente seleccionado ha superado el monto y días máximo de su límite de crédito."
                                If AutorizarVenta(texto) = False Then
                                    Exit Sub
                                End If
                            End If
                        Case -4
                            If MessageBox.Show("El cliente seleccionado no posee parámetros para el control de crédito. Desea realizar la venta igual?(De ser positiva la respuesta, será necesario la autorización de un empleado autorizado.)", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                Util.MsgStatus(Status1, "El cliente seleccionado no posee parámetros para el control de crédito.")
                                Exit Sub
                            Else
                                texto = "El cliente seleccionado no posee parámetros para el control de crédito."
                                If AutorizarVenta(texto) = False Then
                                    Exit Sub
                                End If
                            End If
                        Case -10
                            If MessageBox.Show("No se pudo realizar la consulta del saldo del cliente seleccionado. Desea realizar la venta igual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                Util.MsgStatus(Status1, "No se pudo realizar la consulta del saldo del cliente seleccionado.")
                                Exit Sub
                            Else
                                texto = "No se pudo realizar la consulta del saldo del cliente seleccionado."
                                If AutorizarVenta(texto) = False Then
                                    Exit Sub
                                End If
                            End If
                    End Select
                End If
                'muestro el resumen
                Dim Resumen As New frmVentasWEB_ResumenEnvio
                Resumen.ShowDialog()
                If MDIPrincipal.ConfirResPed = False Then
                    Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
                    Exit Sub
                    'Else
                    '    CalcularNroPedido()
                End If

                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro_PedidosWEB()
                Select Case res
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
                        res_item = AgregarRegistro_PedidosWEB_Items()
                        Select Case res_item
                            Case -5
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case -4
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No hay stock suficiente (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No hay stock suficiente (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el PedidoWEB/Devolución (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el PedidoWEB/Devolución (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case Else

                                'controlo si cancelo la venta
                                If MDIPrincipal.RealizarPago_Completo = True Then
                                    Util.MsgStatus(Status1, "Guardando ingreso...", My.Resources.Resources.indicator_white)
                                    res_ingreso = AgregarRegistro_Ingreso()
                                    Select Case res_ingreso
                                        Case -3
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case -2
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo actualizar el número de Facturación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case -1
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case 0
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                            Exit Sub
                                        Case Else
                                            Util.MsgStatus(Status1, "Guardando los movimientos asociados al pago...", My.Resources.Resources.indicator_white)
                                            res_conyfac = AgregarRegistro_FacturasConsumos()
                                            Select Case res_conyfac
                                                Case -10
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo insertar la Nota de Débito.", My.Resources.Resources.stop_error.ToBitmap)
                                                    Exit Sub
                                                Case -4
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo actualizar el estado del consumo.", My.Resources.Resources.stop_error.ToBitmap)
                                                    Exit Sub
                                                Case -3
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo actualizar el estado de la factura.", My.Resources.Resources.stop_error.ToBitmap)
                                                    Exit Sub
                                                Case -1
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "Se produjo un error durante la operación.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case 0
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registra el movimiento.", My.Resources.Resources.stop_error.ToBitmap)
                                                    Exit Sub
                                                Case Else
                                                    Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                            End Select
                                    End Select
                                End If
                                'Me fijo transferencia entre sucursales (WEB)
                                'If chkTransferencia.Checked = True And chkDevolucion.Checked = False Then
                                '    If InsertarTransRecepWEB() Then
                                '        Dim ds_TranRecep As Data.DataSet
                                '        Dim ds_Empresa As Data.DataSet
                                '        Dim sqltranrec As String = "select IDMaterial,IDUnidad,IDAlmacen,tipo,Qty,Stock," & _
                                '                                   "IDStockMov,Comprobante,IDMotivo,UserAdd from " & _
                                '                                   "tmpStockMov_TransRecepWEB where nromov = '" & lblNroPedido.Text & "'"
                                '        ds_TranRecep = SqlHelper.ExecuteDataset(tran, CommandType.Text, sqltranrec)
                                '        ds_TranRecep.Dispose()
                                '        If ds_TranRecep.Tables(0).Rows.Count > 0 Then
                                '            For i As Integer = 0 To ds_TranRecep.Tables(0).Rows.Count - 1
                                '                If MDIPrincipal.InsertarMovWEB(ds_TranRecep.Tables(0).Rows(i).Item(0), ds_TranRecep.Tables(0).Rows(i).Item(1), ds_TranRecep.Tables(0).Rows(i).Item(2),
                                '                               ds_TranRecep.Tables(0).Rows(i).Item(3), ds_TranRecep.Tables(0).Rows(i).Item(4), ds_TranRecep.Tables(0).Rows(i).Item(5),
                                '                                ds_TranRecep.Tables(0).Rows(i).Item(6), ds_TranRecep.Tables(0).Rows(i).Item(7), ds_TranRecep.Tables(0).Rows(i).Item(8),
                                '                                ds_TranRecep.Tables(0).Rows(i).Item(9)) Then
                                '                    ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & ds_TranRecep.Tables(0).Rows(i).Item(6))
                                '                    ds_Empresa.Dispose()
                                '                Else
                                '                    Cancelar_Tran()
                                '                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de Stock  entre sucursales en la Base de Datos WEB. Por favor intente más tarde.", My.Resources.Resources.stop_error.ToBitmap)
                                '                    Exit Sub
                                '                End If
                                '            Next
                                '        End If                               
                                '        Cerrar_Tran()
                                '        'borro la tabla temporal por eso cierro la transacción antes (esta contiene un truncate)
                                '        Borrar_tmpStockmov()
                                '        'inserto de manera local el envio
                                '        ds_Empresa = tranWEB.Sql_Get("Select NroMov from [" & NameTable_TransRecepWEB & "] where nroasociado = '" & lblNroPedido.Text & "'")
                                '        If ds_Empresa.Tables(0).Rows.Count > 0 Then
                                '            res = Agregar_Registro_TransRecepWEB_Enviado(ds_Empresa.Tables(0).Rows(0).Item(0).ToString)
                                '            Select Case res
                                '                Case -1
                                '                    Cancelar_Tran()
                                '                    Util.MsgStatus(Status1, "No se pudo insertar La transferencia Localmente (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                '                Case 0
                                '                    Cancelar_Tran()
                                '                    Util.MsgStatus(Status1, "No se pudo registra el movimiento local (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                '                Case Else
                                '                    res_item = AgregarRegistro_TransRecepWEB_Items_Enviado(ds_Empresa.Tables(0).Rows(0).Item(0).ToString)
                                '                    Select Case res_item
                                '                        Case -1
                                '                            Cancelar_Tran()
                                '                            Util.MsgStatus(Status1, "No se pudo insertar La transferencia Localmente (Detalle).", My.Resources.Resources.stop_error.ToBitmap)
                                '                        Case 0
                                '                            Cancelar_Tran()
                                '                            Util.MsgStatus(Status1, "No se pudo registra el movimiento local (Detalle).", My.Resources.Resources.stop_error.ToBitmap)
                                '                        Case Else
                                '                            Cerrar_Tran()
                                '                    End Select
                                '            End Select
                                '        Else
                                '            Util.MsgStatus(Status1, "No se pudo obtener el nro de transferencia de la WEB.", My.Resources.Resources.stop_error.ToBitmap)
                                '        End If
                                '    Else
                                '        Cancelar_Tran()
                                '        Util.MsgStatus(Status1, "No se pudo registrar la transferencias entre sucursales en la Base de Datos WEB. Por favor intente más tarde.", My.Resources.Resources.stop_error.ToBitmap)
                                '        Exit Sub
                                '    End If
                                'Else
                                Cerrar_Tran()
                                'End If
                                'If Principal Then
                                Imprimir_Pedido(lblNroPedido.Text)
                                'End If
                                bolModo = False
                                chkHabilitar_EditGrilla.Checked = False
                                PrepararBotones()

                                DesbloquearComponentes(bolModo)

                                'me fijo si el chk de presupuesto esta activo
                                If chkPresupuesto.Checked = True Then
                                    btnActualizarMat.Enabled = chkPresupuesto.Checked
                                    txtValorPromo.Enabled = chkPresupuesto.Checked
                                    txtPrecioVta.Enabled = chkPresupuesto.Checked
                                    txtCantidad.Enabled = chkPresupuesto.Checked
                                    txtPeso.Enabled = chkPresupuesto.Checked
                                    cmbProducto.Enabled = chkPresupuesto.Checked
                                    PanelDescuento.Enabled = chkPresupuesto.Checked
                                End If

                                MDIPrincipal.NoActualizarBase = False
                                btnActualizar_Click(sender, e)

                                Setear_Grilla()
                                'btnEnviarTodos.Enabled = False
                                Util.MsgStatus(Status1, "Se ha actualizado el Registro.", My.Resources.Resources.ok.ToBitmap)
                        End Select

                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
            End If
        End If 'If bolpoliticas Then
        'If ReglasNegocio() Then

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
        DesdeAnular = True
        If chkFacturaCancelada.Checked = True Then
            Util.MsgStatus(Status1, "No se puede anular esta venta porque está asociada a un pago que se efectuó. Anule el pago asociado y luego anule esta venta.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "No se puede anular esta venta porque está asociada a un pago que se efectuó." & vbCrLf & "Anule el pago asociado y luego anule esta venta.", My.Resources.stop_error.ToBitmap, True)
            DesdeAnular = False
            Exit Sub
        End If

        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim sqlstring_ped As String = "select Deuda from pedidosweb where Deuda > 0 and Deuda < Total and Cancelado = 0 and OrdenPedido = '" & lblNroPedido.Text & "'"

            ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
            ds_Pedidos.Dispose()


            If ds_Pedidos.Tables(0).Rows.Count > 0 Then
                Util.MsgStatus(Status1, "No se puede anular esta venta porque está asociada parcialmente a un pago que se efectuó. Anule el pago asociado y luego anule esta venta.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se puede anular esta venta porque está asociada parcialmente a un pago que se efectuó." & vbCrLf & "Anule el pago asociado y luego anule esta venta.", My.Resources.stop_error.ToBitmap, True)
                DesdeAnular = False
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo verificar el estado del registro. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try




        Dim res As Integer

        ''If BAJA Then
        If chkEliminado.Checked = False Then
            'If MessageBox.Show("Esta acción cambiara el estado del registro." + vbCrLf + "¿Está seguro que desea anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            'Verifico que escriba una nota
            If txtNota.Text.Trim = "" Then
                Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap, True)
                DesdeAnular = False
                txtNota.Focus()
                Exit Sub
            End If

            MDIPrincipal.Autorizar = False
            Dim Au As New frmUsuarioModo
            Au.ShowDialog()

            If MDIPrincipal.Autorizar = False Then
                DesdeAnular = False
                Exit Sub
            End If

            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro_PedidosWEB()
            Select Case res
                Case -3
                    DesdeAnular = False
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap, True)
                Case -2
                    DesdeAnular = False
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap, True)
                Case -1
                    DesdeAnular = False
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap, True)
                Case 0
                    DesdeAnular = False
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap, True)
                Case Else
                    res = EliminarRegistro_PediosWEB_Items()
                    Select Case res
                        Case -9
                            DesdeAnular = False
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error al no modificar stock.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error al no modificar stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -8
                            DesdeAnular = False
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error. No se puede restar el stock.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error. No se puede restar el  stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -7
                            DesdeAnular = False
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error. No se pudo actualizar stockmov.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error. No se pudo actualizar stockmov.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case 0
                            DesdeAnular = False
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -1
                            DesdeAnular = False
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case Else
                            Cerrar_Tran()
                            PrepararBotones()

                            bolModo = False
                            PrepararBotones()


                            DesbloquearComponentes(bolModo)

                            rdTodasPed.Checked = True
                            ' SQL = "exec " + subSQL + " @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & "@Ventas_Norte = " & btnXNorte.Cheked
                            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ",@Devolucion = " & chkDevolucion.Checked

                            'coloco este codigo por ahora
                            Me.Cursor = Cursors.WaitCursor
                            LlenarGrilla()
                            Permitir = True
                            CargarCajas()
                            PrepararBotones()
                            Me.Cursor = Cursors.Default

                            'MDIPrincipal.NoActualizarBase = True
                            'btnActualizar_Click(sender, e)

                            Setear_Grilla()

                            Util.MsgStatus(Status1, "Se ha anulado el registro.", My.Resources.ok.ToBitmap)
                            Util.MsgStatus(Status1, "Se ha anulado el registro.", My.Resources.ok.ToBitmap, True, True)
                            DesdeAnular = False
                    End Select
            End Select
            'Else
            '    Util.MsgStatus(Status1, "Acción de anular cancelada.", My.Resources.stop_error.ToBitmap)
            '    Util.MsgStatus(Status1, "Acción de anular cancelada.", My.Resources.stop_error.ToBitmap, True)
            'End If
        Else
            Util.MsgStatus(Status1, "El registro ya está anulado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está anulado.", My.Resources.stop_error.ToBitmap, True)
        End If
        ''Else
        ''Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        ''Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap, True)
        ''End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Solicitud As Boolean

        'Try

        'Catch ex As Exception

        'End Try

        nbreformreportes = "Remito"

        param.AgregarParametros("Código :", "STRING", "", False, lblNroPedido.Text.ToString, "", cnn)

        param.ShowDialog()

        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)
            Dim saldo As Double = CDbl(lblSaldo.Text.Replace("$", ""))
            'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)
            'If chkVentas.Checked = True Then
            rpt.OrdenesDeCompra_Maestro_App(codigo, saldo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
            'Else
            '    rpt.DevolucionePedidosWEB_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
            'End If

            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If



    End Sub

    Private Sub btnLlenarGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlenarGrilla.Click
        'Dim i As Integer

        If bolModo Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
            ' If Me.cmbPedidos.Text <> "" Then
            PrepararGridItems()
            Try
                LlenarGrid_Items()
                Contar_Filas()

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
            'btnEnviarTodos.Enabled = True
            With (grdItems)
                .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
            End With
            'End If
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        'Dim connection As SqlClient.SqlConnection = Nothing
        'Try
        '    'me fijo si se esta cancelando un envio de pedido para que vuelva el estado orginal del pedido 
        '    If cmbPedidos.SelectedValue.ToString = "" Then
        '        DevolverEstado()
        '    End If
        'Catch ex As Exception

        'End Try

        ' btnEnviarTodos.Enabled = False
        chkHabilitar_EditGrilla.Checked = False
        DesbloquearComponentes(False)
        grd_CurrentCellChanged(sender, e)
        bolModo = False
        rdTodasPed.Checked = True

        'me fijo si el chk de presupuesto esta activo
        'If chkPresupuesto.Checked = True Then
        '    btnActualizarMat.Enabled = chkPresupuesto.Checked
        '    txtValorPromo.Enabled = chkPresupuesto.Checked
        '    txtPrecioVta.Enabled = chkPresupuesto.Checked
        '    txtCantidad.Enabled = chkPresupuesto.Checked
        '    txtPeso.Enabled = chkPresupuesto.Checked
        '    cmbProducto.Enabled = chkPresupuesto.Checked
        '    PanelDescuento.Enabled = chkPresupuesto.Checked
        'End If
        FinalizarPresupuesto = False
    End Sub

    Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click

        rdTodasPed.Checked = True
        ' SQL = "exec " + subSQL + " @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & "@Ventas_Norte = " & btnXNorte.Cheked
        SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = " & btnXNorte.Checked & ",@Devolucion = " & chkDevolucion.Checked

        FinalizarPresupuesto = False

        CambiarColores()
        BuscarSaldo()

    End Sub

    Private Sub btnEnviarTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If MessageBox.Show("Está seguro que desea copiar los valores de la columna Cant. Saldo a la columna Cant. a Enviar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim i As Integer
            Dim suma As Double = 0

            For i = 0 To grdItems.RowCount - 1
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value > 0 Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value

                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value.ToString.ToUpper = "TIRA" Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value.ToString.ToUpper = "HORMA" Then
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                    Else
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                    End If
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value, 2)
                End If
            Next
            CalculoSubtotales()
        End If
    End Sub

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        chkTransferencia.Checked = False
    End Sub

#End Region

#Region "   GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit

        editando_celda = False
        Dim descuento As Double
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        Try

            'me fijo si esta cambiando los precios 
            ' If chkCambiarPrecios.Checked = True Then
            'If e.ColumnIndex = ColumnasDelGridItems.Precio Then
            '    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
            '    Else
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
            '    End If

            '    Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)

            'End If


            '' End If

            'If e.ColumnIndex = ColumnasDelGridItems.QtyEnviada Or e.ColumnIndex = ColumnasDelGridItems.Peso Then
            '    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
            '    Else
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
            '    End If

            '    Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)

            'End If

            If chkHabilitar_EditGrilla.Checked Then
                If e.ColumnIndex = ColumnasDelGridItems.QtyEnviada Or e.ColumnIndex = ColumnasDelGridItems.Peso Then
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                    End If
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)
                End If
            End If

            If e.ColumnIndex = ColumnasDelGridItems.Descuento Then

                descuento = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value)

                'If descuento > 100 Then
                '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value = 0
                '    descuento = 0

                'End If

                'If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                'Else
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                'End If

                'Dim calculo As Double = (descuento * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value) / 100
                'calculo = Math.Round(calculo, 2)

                Dim calculo As Double = Math.Round(CDbl(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value = "", 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value)), 2)

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - calculo
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)

                If descuento = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value + "(BONIF.)"
                    'coloco el bit de bonificacion en uno
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion).Value = True
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.Replace("(BONIF.)", "")
                    'coloco el bit de bonificacion en 0
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion).Value = False
                End If

            End If

            CalculoSubtotales()

        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.QtyPedido Then
            AddHandler e.Control.KeyPress, AddressOf validar_NumerosReales2
        End If

    End Sub

    Private Sub grdItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdItems.KeyDown

        Try
            Dim columna As Integer = 0
            columna = grdItems.CurrentCell.ColumnIndex
        Catch ex As Exception

        End Try


        'If columna = ColumnasDelGridItems.ID_OrdenDeCompra Then
        'If columna = 7 Then
        '    If e.KeyCode = Keys.Enter And bolModo Then


        '    End If
        'End If

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

    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        If e.ColumnIndex = 26 And (bolModo = True Or chkPresupuesto.Checked = True) Then 'Marcar llegada
            If MessageBox.Show("Está seguro que desea eliminar el producto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItems.Rows.RemoveAt(e.RowIndex)
                CalculoSubtotales()
                'me fijo si en la grilla hay algo y cambio el orden de los item
                If grdItems.Rows.Count > 0 Then
                    For i As Integer = 0 To grdItems.Rows.Count - 1
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value = i + 1
                    Next
                End If
            End If
        End If
    End Sub

#End Region




    '#Region "WEB"
    '    '-------------------- Transferencia WEB
    '    Private Function InsertarTransRecepWEB() As Boolean

    '        Dim sqlstring As String
    '        Dim sqlstringNotif As String
    '        Dim resString As String

    '        '-------------Variable Encabezado
    '        Dim NroAsociado As String = lblNroPedido.Text
    '        Dim Tipo As String = "T"
    '        'BD Local
    '        'Dim Fecha As String = Format(dtpFECHA.Value.Date, "dd/MM/yyyy").ToString + " " + Format(Date.Now, "hh:mm:ss").ToString
    '        'BD web 
    '        Dim Fecha As String = Format(dtpFECHA.Value.Date, "MM/dd/yyyy").ToString + " " + Format(Date.Now, "hh:mm:ss").ToString
    '        Dim IDOrigen As Integer = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
    '        Dim IDDestino As Integer = IDAlmacenTrans
    '        Dim TotalPedido As Double = lblTotal.Text
    '        Dim TotalRecepcionado As Double = 0
    '        Dim StatusEnca As String = "P"
    '        Dim Nota As String = txtNota.Text
    '        Dim IDEmpleado As String = UserID.ToString
    '        Dim DescargarEnOrigen As Boolean = False
    '        Dim DescargarEnDestino As Boolean = True
    '        '---------------Variable Detalle
    '        Dim IDMaterial As String
    '        Dim IDMarca As String
    '        Dim IDUnidad As String
    '        Dim UnidadFac As Double
    '        Dim CantidadPACK As Double
    '        Dim Precio As Double
    '        Dim QtyPedida As Double
    '        Dim QtyEnviada As Double
    '        Dim QtySaldo As Double
    '        Dim SubtotalPedido As Double
    '        Dim SubtotalRecepcionado As Double
    '        Dim StatusDet As String
    '        Dim Nota_Det As String
    '        Dim OrdenItem As Integer

    '        Try
    '            sqlstring = "BEGIN TRY BEGIN TRAN "
    '            sqlstring = sqlstring + " Declare @nromov as varchar(25)"
    '            sqlstring = sqlstring + " exec " & NameSP_spTransRecepWEB_Insert & " NUll,@nromov OUTPUT,'" & NroAsociado & "','" & Tipo & "','" & Fecha & "'," & _
    '                                      IDOrigen & "," & IDDestino & "," & TotalPedido & "," & TotalRecepcionado & ",'" & StatusEnca & "','" & Nota & "','" & _
    '                                      IDEmpleado & "'," & IIf(DescargarEnOrigen = False, 0, 1) & "," & IIf(DescargarEnDestino = False, 0, 1) & ", NULL "

    '            For i As Integer = 0 To grdItems.Rows.Count - 1

    '                IDMaterial = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
    '                IDMarca = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value)
    '                IDUnidad = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
    '                UnidadFac = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value)
    '                CantidadPACK = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) / CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
    '                Precio = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
    '                QtyPedida = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
    '                QtyEnviada = 0
    '                QtySaldo = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
    '                SubtotalPedido = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value)
    '                SubtotalRecepcionado = 0
    '                StatusDet = "P"
    '                Nota_Det = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
    '                OrdenItem = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value

    '                sqlstring = sqlstring + "INSERT INTO [dbo].[" & NameTable_TransRecepWEBdet & "] ([IDNroMov],[IDMaterial],[IDMarca],[IDUnidad]," & _
    '                                        "[UnidadFac],[CantidadPACK],[Precio],[QtyPedida],[QtyEnviada],[QtySaldo],[SubtotalPedido]," & _
    '                                        "[SubtotalRecepcionado],[Status],[Nota_Det],[OrdenItem],[Fecha]) VALUES (@nromov,'" & IDMaterial & "','" & _
    '                                        IDMarca & "','" & IDUnidad & "'," & UnidadFac & "," & CantidadPACK & "," & Precio & "," & QtyPedida & "," & _
    '                                        QtyEnviada & "," & QtySaldo & "," & SubtotalPedido & "," & SubtotalRecepcionado & ",'" & StatusDet & "','" & _
    '                                        Nota_Det & "'," & OrdenItem & ",'" & Fecha & "')"

    '            Next

    '            sqlstring = sqlstring + " COMMIT TRAN End Try BEGIN CATCH ROLLBACK TRAN END CATCH"

    '            resString = tranWEB.Sql_Set(sqlstring)

    '            If resString = "Funcionó" Then
    '                Try
    '                    sqlstringNotif = "update [" & NameTable_NotificacionesWEB & "] set Transferencias = 1 where idalmacen = " & IDDestino
    '                    tranWEB.Sql_Set(sqlstringNotif)

    '                Catch ex As Exception
    '                    'MsgBox(ex)
    '                End Try
    '                InsertarTransRecepWEB = True
    '            Else
    '                InsertarTransRecepWEB = False
    '            End If

    '        Catch ex As Exception
    '            InsertarTransRecepWEB = False
    '        End Try
    '    End Function

    '    Private Function Agregar_Registro_TransRecepWEB_Enviado(ByVal NroMov As String) As Integer
    '        Dim res As Integer = 0

    '        Try
    '            Try
    '                conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End Try

    '            'Abrir una transaccion para guardar y asegurar que se guarda todo
    '            If Abrir_Tran(conn_del_form) = False Then
    '                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End If

    '            Try
    '                Dim param_id As New SqlClient.SqlParameter
    '                param_id.ParameterName = "@id"
    '                param_id.SqlDbType = SqlDbType.BigInt
    '                param_id.Value = DBNull.Value
    '                param_id.Direction = ParameterDirection.Input

    '                Dim param_ordenpedido As New SqlClient.SqlParameter
    '                param_ordenpedido.ParameterName = "@NROMOV"
    '                param_ordenpedido.SqlDbType = SqlDbType.VarChar
    '                param_ordenpedido.Size = 25
    '                param_ordenpedido.Value = NroMov
    '                param_ordenpedido.Direction = ParameterDirection.Input

    '                Dim param_ordenpedidoAsoc As New SqlClient.SqlParameter
    '                param_ordenpedidoAsoc.ParameterName = "@NroAsociado"
    '                param_ordenpedidoAsoc.SqlDbType = SqlDbType.VarChar
    '                param_ordenpedidoAsoc.Size = 25
    '                param_ordenpedidoAsoc.Value = lblNroPedido.Text
    '                param_ordenpedidoAsoc.Direction = ParameterDirection.Input

    '                Dim param_tipo As New SqlClient.SqlParameter
    '                param_tipo.ParameterName = "@Tipo"
    '                param_tipo.SqlDbType = SqlDbType.VarChar
    '                param_tipo.Size = 25
    '                param_tipo.Value = "T"
    '                param_tipo.Direction = ParameterDirection.Input

    '                Dim param_fecha As New SqlClient.SqlParameter
    '                param_fecha.ParameterName = "@FECHA"
    '                param_fecha.SqlDbType = SqlDbType.DateTime
    '                param_fecha.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
    '                param_fecha.Direction = ParameterDirection.Input

    '                Dim param_idAlmacen As New SqlClient.SqlParameter
    '                param_idAlmacen.ParameterName = "@IDOrigen"
    '                param_idAlmacen.SqlDbType = SqlDbType.BigInt
    '                param_idAlmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
    '                param_idAlmacen.Direction = ParameterDirection.Input

    '                Dim param_destino As New SqlClient.SqlParameter
    '                param_destino.ParameterName = "@IDDestino"
    '                param_destino.SqlDbType = SqlDbType.BigInt
    '                param_destino.Value = IDAlmacenTrans
    '                param_destino.Direction = ParameterDirection.Input

    '                Dim param_SubtotalPedido As New SqlClient.SqlParameter
    '                param_SubtotalPedido.ParameterName = "@TOTALPEDIDO"
    '                param_SubtotalPedido.SqlDbType = SqlDbType.Decimal
    '                param_SubtotalPedido.Precision = 18
    '                param_SubtotalPedido.Size = 2
    '                param_SubtotalPedido.Value = lblTotal.Text
    '                param_SubtotalPedido.Direction = ParameterDirection.Input

    '                Dim param_nota As New SqlClient.SqlParameter
    '                param_nota.ParameterName = "@NOTA"
    '                param_nota.SqlDbType = SqlDbType.VarChar
    '                param_nota.Size = 150
    '                param_nota.Value = txtNota.Text
    '                param_nota.Direction = ParameterDirection.Input

    '                Dim param_useradd As New SqlClient.SqlParameter
    '                param_useradd.ParameterName = "@IDEmpleado"
    '                param_useradd.SqlDbType = SqlDbType.Int
    '                param_useradd.Value = UserID
    '                param_useradd.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@res"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = DBNull.Value
    '                param_res.Direction = ParameterDirection.InputOutput

    '                Try
    '                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransRecepWEB_Enviado_Insert", _
    '                                              param_id, param_ordenpedido, param_ordenpedidoAsoc, param_idAlmacen, _
    '                                              param_destino, param_SubtotalPedido, param_fecha, param_tipo, _
    '                                              param_nota, param_useradd, param_res)

    '                    Agregar_Registro_TransRecepWEB_Enviado = param_res.Value



    '                Catch ex As Exception
    '                    Throw ex
    '                End Try
    '            Finally

    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        End Try
    '    End Function

    '    Private Function AgregarRegistro_TransRecepWEB_Items_Enviado(ByVal idNroMov As String) As Integer
    '        Dim res As Integer = 0
    '        Dim i As Integer
    '        Dim ActualizarPrecio As Boolean = False
    '        Dim ValorActual As Double

    '        Try
    '            Try
    '                i = 0

    '                Do While i < grdItems.Rows.Count

    '                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) > 0 Then

    '                        ValorActual = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value

    '                        Dim param_id As New SqlClient.SqlParameter
    '                        param_id.ParameterName = "@id"
    '                        param_id.SqlDbType = SqlDbType.BigInt
    '                        param_id.Value = DBNull.Value
    '                        param_id.Direction = ParameterDirection.Input

    '                        Dim param_idpedidosweb As New SqlClient.SqlParameter
    '                        param_idpedidosweb.ParameterName = "@IDNroMov"
    '                        param_idpedidosweb.SqlDbType = SqlDbType.VarChar
    '                        param_idpedidosweb.Size = 25
    '                        param_idpedidosweb.Value = idNroMov
    '                        param_idpedidosweb.Direction = ParameterDirection.Input

    '                        Dim param_idmaterial As New SqlClient.SqlParameter
    '                        param_idmaterial.ParameterName = "@IDMATERIAL"
    '                        param_idmaterial.SqlDbType = SqlDbType.VarChar
    '                        param_idmaterial.Size = 25
    '                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
    '                        param_idmaterial.Direction = ParameterDirection.Input

    '                        Dim param_idmarca As New SqlClient.SqlParameter
    '                        param_idmarca.ParameterName = "@IDMARCA"
    '                        param_idmarca.SqlDbType = SqlDbType.VarChar
    '                        param_idmarca.Size = 25
    '                        param_idmarca.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value
    '                        param_idmarca.Direction = ParameterDirection.Input

    '                        Dim param_idunidad As New SqlClient.SqlParameter
    '                        param_idunidad.ParameterName = "@IDUNIDAD"
    '                        param_idunidad.SqlDbType = SqlDbType.VarChar
    '                        param_idunidad.Size = 25
    '                        param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
    '                        param_idunidad.Direction = ParameterDirection.Input

    '                        Dim param_cantidadpack As New SqlClient.SqlParameter
    '                        param_cantidadpack.ParameterName = "@CantidadPACK"
    '                        param_cantidadpack.SqlDbType = SqlDbType.Decimal
    '                        param_cantidadpack.Precision = 18
    '                        param_cantidadpack.Scale = 2
    '                        param_cantidadpack.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) / CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
    '                        param_cantidadpack.Direction = ParameterDirection.Input

    '                        Dim param_precio As New SqlClient.SqlParameter
    '                        param_precio.ParameterName = "@PRECIO"
    '                        param_precio.SqlDbType = SqlDbType.Decimal
    '                        param_precio.Precision = 18
    '                        param_precio.Scale = 2
    '                        param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
    '                        param_precio.Direction = ParameterDirection.Input

    '                        Dim param_qtypedida As New SqlClient.SqlParameter
    '                        param_qtypedida.ParameterName = "@QTYPEDIDA"
    '                        param_qtypedida.SqlDbType = SqlDbType.Decimal
    '                        param_qtypedida.Precision = 18
    '                        param_qtypedida.Scale = 2
    '                        param_qtypedida.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
    '                        param_qtypedida.Direction = ParameterDirection.Input

    '                        Dim param_subtotalped As New SqlClient.SqlParameter
    '                        param_subtotalped.ParameterName = "@SubtotalPedido"
    '                        param_subtotalped.SqlDbType = SqlDbType.Decimal
    '                        param_subtotalped.Precision = 18
    '                        param_subtotalped.Scale = 2
    '                        param_subtotalped.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value)
    '                        param_subtotalped.Direction = ParameterDirection.Input

    '                        Dim param_notadet As New SqlClient.SqlParameter
    '                        param_notadet.ParameterName = "@NOTA_DET"
    '                        param_notadet.SqlDbType = SqlDbType.VarChar
    '                        param_notadet.Size = 300
    '                        param_notadet.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
    '                        param_notadet.Direction = ParameterDirection.Input

    '                        Dim param_UnidadFac As New SqlClient.SqlParameter
    '                        param_UnidadFac.ParameterName = "@UnidadFac"
    '                        param_UnidadFac.SqlDbType = SqlDbType.Decimal
    '                        param_UnidadFac.Precision = 18
    '                        param_UnidadFac.Scale = 2
    '                        param_UnidadFac.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value)
    '                        param_UnidadFac.Direction = ParameterDirection.Input

    '                        Dim param_ordenitem As New SqlClient.SqlParameter
    '                        param_ordenitem.ParameterName = "@ORDENITEM"
    '                        param_ordenitem.SqlDbType = SqlDbType.SmallInt
    '                        param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
    '                        param_ordenitem.Direction = ParameterDirection.Input

    '                        Dim param_res As New SqlClient.SqlParameter
    '                        param_res.ParameterName = "@res"
    '                        param_res.SqlDbType = SqlDbType.Int
    '                        param_res.Value = DBNull.Value
    '                        param_res.Direction = ParameterDirection.InputOutput


    '                        Try
    '                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransRecepWEB_Det_Enviado_Insert", _
    '                                                    param_id, param_idpedidosweb, param_idmaterial, param_idmarca, _
    '                                                    param_idunidad, param_cantidadpack, param_precio, param_qtypedida, _
    '                                                    param_subtotalped, param_notadet, param_UnidadFac, param_ordenitem, param_res)

    '                            res = param_res.Value

    '                            If (res <= 0) Then
    '                                Exit Do
    '                            End If

    '                        Catch ex As Exception
    '                            Throw ex
    '                        End Try

    '                    End If

    '                    i = i + 1

    '                Loop

    '                AgregarRegistro_TransRecepWEB_Items_Enviado = res

    '            Catch ex2 As Exception
    '                Throw ex2
    '            Finally

    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End Function


    '#End Region


End Class
