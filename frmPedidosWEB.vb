Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmPedidosWEB

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
    Dim desdeload As Boolean = False


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

    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim band As Integer

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient

    Dim ds_Pedidos As Data.DataSet
    Dim sqlstring_ped As String



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

    Private Sub frmPedidosWEB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Cursor = Cursors.WaitCursor

        ToolStrip_lblCodMaterial.Visible = True
        txtBusquedaMAT.Visible = True

        band = 0

        btnEliminar.Text = "Anular Envío"
        btnEliminar.Visible = False

        configurarform()
        asignarTags()

        LlenarcmbAlmacenes()
        LlenarComboClientes()
        llenarcmbRepartidor()

        rdPendientes.Checked = True

        'SQL = "exec spPedidosWEB_Select_All  @Eliminado = 0"
        SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = 0,@Devolucion = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        Setear_Grilla()

        If bolModo = True Then
            LlenarGrid_Items()
            band = 1
            btnNuevo_Click(sender, e)
        Else
            'btnLlenarGrilla.Enabled = bolModo
            DesbloquearComponentes(bolModo)
            LlenarGrid_Items()
        End If

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        grd.Columns(0).Visible = False
        grd.Columns(3).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(15).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(17).Visible = False
        grd.Columns(18).Visible = False
        grd.Columns(24).Visible = False
        'grd.Columns(30).Visible = False
        'grd.Columns(31).Visible = False






        Contar_Filas()

        dtpFECHA.MaxDate = Today.Date

        band = 1

        PicDescarga.Image = My.Resources.Sincro

        TimerDescargas.Enabled = True

        desdeload = True

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
    Handles txtID.KeyPress, txtNota.KeyPress
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

    Private Sub cmbPedidos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPedidos.SelectedIndexChanged

        Dim connection As SqlClient.SqlConnection = Nothing

        If band = 1 Then

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Dim PedidoAnterior As String = IIf(txtIDPedido.Text = "", "0", txtIDPedido.Text)

            If PedidoAnterior <> "0" Then

                sqlstring_ped = "UPDATE PedidosWEB SET EnProceso = 0 Where OrdenPedido = '" & PedidoAnterior & "'"

                ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
                ds_Pedidos.Dispose()
                Try
                    tranWEB.Sql_Set(sqlstring_ped)

                Catch ex As Exception
                    MsgBox("Error al cambiar estado del pedido " + PedidoAnterior)
                End Try

            End If

            Try
                txtIDPedido.Text = cmbPedidos.SelectedValue
                'selecciono el repartido que realizo el pedido
                sqlstring_ped = "select IDEmpleado from PedidosWEB where OrdenPedido = '" & txtIDPedido.Text & "'"
                ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
                ds_Pedidos.Dispose()
                'MsgBox(ds_Pedidos.Tables(0).Rows(0).Item(0).ToString)
                cmbRepartidor.SelectedValue = ds_Pedidos.Tables(0).Rows(0).Item(0).ToString
            Catch ex As Exception
                MsgBox("Error al cargcar repartidor")
            End Try


            If txtIDPedido.Text <> "" Then

                sqlstring_ped = "UPDATE PedidosWEB SET EnProceso = 1 Where OrdenPedido = '" & txtIDPedido.Text & "'"

                ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
                ds_Pedidos.Dispose()

                Try
                    tranWEB.Sql_Set(sqlstring_ped)

                Catch ex As Exception
                    MsgBox("Error al cambiar estado del pedido " + txtIDPedido.Text)
                End Try

            End If

            If llenandoCombo = False Then
                btnLlenarGrilla_Click(sender, e)
                OcultarItemsEliminados()
            End If
        End If
    End Sub

    'Private Sub cmbPedidos_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPedidos.SelectionChangeCommitted
    '    If band = 1 Then
    '        If llenandoCombo = False Then
    '            btnLlenarGrilla_Click(sender, e)
    '        End If
    '    End If
    'End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            txtIdCliente.Text = cmbClientes.SelectedValue
            band = 0
            LlenarcmbPedidos()
            band = 1
            LimpiarGridItems(grdItems)
            btnLlenarGrilla_Click(sender, e)
        End If
    End Sub

    '(currentcellChanged)
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

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
                lblStatus.Text = grd.CurrentRow.Cells(14).Value.ToString
                cmbRepartidor.SelectedValue = grd.CurrentRow.Cells(18).Value



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

            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = 0,@Devolucion = 0"

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

        bolModo = False

        SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = 0,@Devolucion = 0"

        Try
            LlenarGrilla()

            'If grd.Rows.Count > 0 Then
            'LlenarGrid_Items()
            'End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub rdTodasPed_CheckedChanged(sender As Object, e As EventArgs) Handles rdTodasPed.CheckedChanged

        If band = 1 Then

            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = 0,@Devolucion = 0"

            bolModo = False
            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then
                    LlenarGrid_Items()
                End If
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub TimerDescargas_Tick(sender As Object, e As EventArgs) Handles TimerDescargas.Tick

        TimerDescargas.Enabled = False
        Try
            'me fijo la cantidad de filas que haya en el encabezado web
            Dim ds_PedidosWEB As DataSet = tranWEB.Sql_Get("SELECT count(*) FROM PedidosWEB ")
            Dim EncabezadoWEB_Filas As Integer = CInt(ds_PedidosWEB.Tables(0).Rows(0).Item(0))
            'me fijo si la cantidad de filas hay local encabezado
            Dim sqlstring As String = " SELECT count(*) FROM tmpPedidosWEB_WEB"
            ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Pedidos.Dispose()
            Dim EncabezadoLocal_Filas As Integer = CInt(ds_Pedidos.Tables(0).Rows(0).Item(0))

            'me fijo la cantidad de filas que haya en el detalle web
            Dim ds_PedidosWEBdet As DataSet = tranWEB.Sql_Get("SELECT count(*) FROM PedidosWEB_Det ")
            Dim EncabezadoWEB_Filasdet As Integer = CInt(ds_PedidosWEBdet.Tables(0).Rows(0).Item(0))
            'me fijo si la cantidad de filas hay local detalle
            Dim sqlstringdet As String = " SELECT count(*) FROM tmpPedidosWEB_det_web "
            ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstringdet)
            ds_Pedidos.Dispose()
            Dim EncabezadoLocal_Filasdet As Integer = CInt(ds_Pedidos.Tables(0).Rows(0).Item(0))
            'comparo las filas 
            If EncabezadoWEB_Filas <> EncabezadoLocal_Filas Then
                PicDescarga.Image = My.Resources.SincroPendiente
            ElseIf EncabezadoWEB_Filasdet <> EncabezadoLocal_Filasdet Then
                PicDescarga.Image = My.Resources.SincroPendiente
            Else
                PicDescarga.Image = My.Resources.SincroOK
            End If
            TimerDescargas.Enabled = True
        Catch ex As Exception
            TimerDescargas.Enabled = True
        End Try



    End Sub

    Private Sub chkCambiarPrecios_CheckedChanged(sender As Object, e As EventArgs) Handles chkCambiarPrecios.CheckedChanged
        If bolModo = True Then
            Dim i As Integer

            'controlo el chk
            If chkCambiarPrecios.Checked = True Then
                'recorro la grilla y cambio el color de la grilla 
                For i = 0 To grdItems.RowCount - 1
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Style.BackColor = Color.LightBlue
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).ReadOnly = False
                Next
            Else
                For i = 0 To grdItems.RowCount - 1
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Style.BackColor = Color.White
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).ReadOnly = True
                Next
            End If
        End If

    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Envío de Pedidos WEB"

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
        lblStatus.Tag = "14"
        chkDescuento.Tag = "15"
        rdPorcentaje.Tag = "16"
        rdAbsoluto.Tag = "17"
        cmbRepartidor.Tag = "18"


    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

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

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        If bolModo = False Then
            If lblNroPedido.Text = "" Then
                SQL = "exec spPedidosWEB_Det_Select_By_IDPedidosWEB @IdPedidosWEB = '1'"
            Else
                SQL = "exec spPedidosWEB_Det_Select_By_IDPedidosWEB @IdPedidosWEB = '" & lblNroPedido.Text & "'"
            End If
        Else
            SQL = "exec spPedidosWEB_Det_Select_By_IDPedidosWEB @IdPedidosWEB = '" & IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) & "'"
        End If


        GetDatasetItems(grdItems)


        grdItems.Columns(0).Visible = False
        grdItems.Columns(2).Visible = False
        grdItems.Columns(4).Visible = False
        grdItems.Columns(6).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

        grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.QtyEnviada).ReadOnly = Not bolModo
        If bolModo = True Then
            grdItems.Columns(ColumnasDelGridItems.QtyEnviada).HeaderText = "Qty. a envíar"
            grdItems.Columns(ColumnasDelGridItems.QtyEnviada).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        Else
            grdItems.Columns(ColumnasDelGridItems.QtyEnviada).HeaderText = "Qty. enviada"
            grdItems.Columns(ColumnasDelGridItems.QtyEnviada).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End If



        'grdItems.Columns(ColumnasDelGridItems.QtyPedido).Visible = bolModo
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.QtySaldo).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.IVA).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Precio).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Subtotal).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Nota_Det).ReadOnly = Not bolModo

        grdItems.Columns(ColumnasDelGridItems.Peso).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Peso).HeaderText = "Peso (Kg)"

        grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Stock).Visible = bolModo

        grdItems.Columns(ColumnasDelGridItems.Eliminado).Visible = False
        grdItems.Columns(ColumnasDelGridItems.CantidadPaCK).Visible = False


        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).Visible = rdTodasPed.Checked

        grdItems.Columns(ColumnasDelGridItems.Descuento).HeaderText = "Descuento(%)"

        grdItems.Columns(ColumnasDelGridItems.Reintegrar_Stock).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Promo).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Bonificacion).Visible = False



        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = False
            If bolModo Then
                .RowsDefaultCellStyle.BackColor = Color.White
                .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue

            Else
                .RowsDefaultCellStyle.BackColor = Color.LightBlue
                .AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue
            End If
            .ForeColor = Color.Black
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect

        End With

        With grdItems.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        'Volver la fuente de datos a como estaba...
        'SQL = "exec spPedidosWEB_Select_All @Eliminado = 0"

        'Dim connection As SqlClient.SqlConnection = Nothing
        If bolModo = True Then
            ControlItem()
        Else
            OcultarItemsEliminados()
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
        txtNota.Enabled = habilitar
        cmbPedidos.Enabled = habilitar
        lblNroPedido.Enabled = habilitar
        PanelDescuento.Enabled = habilitar
        lblNroPedido.Enabled = habilitar
        cmbRepartidor.Enabled = habilitar
        chkCambiarPrecios.Enabled = habilitar

        btnDescargarPedidos.Enabled = Not habilitar
        rdTodasPed.Enabled = Not habilitar
        rdPendientes.Enabled = Not habilitar
        rdAnuladas.Enabled = Not habilitar

    End Sub

    Private Sub LlenarcmbPedidos()
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

            ds_OC = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '' AS 'OrdenPedido' Union select OrdenPedido from PedidosWEB where " & _
                                            " idCliente = '" & cmbClientes.SelectedValue & "'  and eliminado= 0 and status in ('P')")
            ds_OC.Dispose()

            With Me.cmbPedidos
                .DataSource = ds_OC.Tables(0).DefaultView
                ''.DisplayMember = "OrdenPedido"
                .ValueMember = "OrdenPedido"
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

    Private Sub CalculoSubtotales()

        Dim suma As Double = 0
        'me fijo si se esta haciendo un nuevo pedido asi calculo los subtotales
        If bolModo = True Then
            For i As Integer = 0 To grdItems.Rows.Count - 1
                'me fijo que el saldo sea mayor a cero 
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value > 0 Then
                    'me fijo si esta eliminado el items o si esta anulado la el pedido
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value = False Or lblStatus.Text.Contains("ANULADO") Then
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value.ToString.ToUpper = "TIRA" Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value.ToString.ToUpper = "HORMA" Then
                            suma = suma + (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                        Else
                            suma = suma + (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                        End If
                    End If
                End If
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
                Else

                    Dim ValorDescuento As Double
                    ValorDescuento = Math.Round(suma * (Descuento / 100), 2)

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

        'nbreformreportes = "Ordenes de Compra"

        'param.AgregarParametros("Código :", "STRING", "", False, OrdenPedido, "", cnn)
        ' param.ShowDialog()

        'If cerroparametrosconaceptar = True Then

        'codigo = param.ObtenerParametros(0)

        'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)

        rpt.OrdenesDeCompra_Maestro_App(OrdenPedido, 0, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
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

    Private Sub DevolverEstado()

        Dim connection As SqlClient.SqlConnection = Nothing
        Try

            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        sqlstring_ped = "UPDATE PedidosWEB SET EnProceso = 0 Where OrdenPedido = '" & cmbPedidos.Text.ToString & "'"
        ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring_ped)
        Try
            tranWEB.Sql_Set(sqlstring_ped)
        Catch ex As Exception
            MsgBox("Error al cambiar estado del pedido " + cmbPedidos.Text.ToString)
        End Try
    End Sub

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
                param_ordenpedido.Value = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text)
                param_ordenpedido.Direction = ParameterDirection.Input

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
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

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

                Dim param_Descuento As New SqlClient.SqlParameter
                param_Descuento.ParameterName = "@Descuento"
                param_Descuento.SqlDbType = SqlDbType.Bit
                param_Descuento.Value = chkDescuento.Checked
                param_Descuento.Direction = ParameterDirection.Input

                Dim param_Porcentaje As New SqlClient.SqlParameter
                param_Porcentaje.ParameterName = "@Porcentaje"
                param_Porcentaje.SqlDbType = SqlDbType.Bit
                param_Porcentaje.Value = rdPorcentaje.Checked
                param_Porcentaje.Direction = ParameterDirection.Input

                Dim param_valorDescuento As New SqlClient.SqlParameter
                param_valorDescuento.ParameterName = "@valordescuento"
                param_valorDescuento.SqlDbType = SqlDbType.Decimal
                param_valorDescuento.Precision = 18
                param_valorDescuento.Scale = 2
                param_valorDescuento.Value = IIf(txtDescuento.Text = "", 0, txtDescuento.Text)
                param_valorDescuento.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_cancelado As New SqlClient.SqlParameter
                param_cancelado.ParameterName = "@CANCELADO"
                param_cancelado.SqlDbType = SqlDbType.Bit
                param_cancelado.Value = 0
                param_cancelado.Direction = ParameterDirection.Input

                Dim param_deuda As New SqlClient.SqlParameter
                param_deuda.ParameterName = "@DEUDA"
                param_deuda.SqlDbType = SqlDbType.Decimal
                param_deuda.Precision = 18
                param_total.Size = 2
                If param_cancelado.Value = 1 Then
                    param_deuda.Value = 0
                Else
                    param_deuda.Value = lblTotal.Text
                End If
                param_deuda.Direction = ParameterDirection.Input

                Dim param_idusuario As New SqlClient.SqlParameter
                param_idusuario.ParameterName = "@IDEMPLEADO"
                param_idusuario.SqlDbType = SqlDbType.Int
                param_idusuario.Value = IIf(txtIDRepartidor.Text = "", cmbRepartidor.SelectedValue.ToString.Replace("0", ""), txtIDRepartidor.Text.Replace("0", ""))
                param_idusuario.Direction = ParameterDirection.Input

                Dim param_control As New SqlClient.SqlParameter
                param_control.ParameterName = "@CONTROL"
                param_control.SqlDbType = SqlDbType.SmallInt
                param_control.Value = DBNull.Value
                param_control.Direction = ParameterDirection.Output

                Dim param_useradd As New SqlClient.SqlParameter
                'If bolModo = True Then
                'param_useradd.ParameterName = "@useradd"
                'Else
                param_useradd.ParameterName = "@userupd"
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
                    'If bolModo = True Then
                    '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spRecepciones_Insert", _
                    '                          param_id, param_codigo, param_idAlmacen, param_idMoneda, param_idOC, param_NroOC, _
                    '                          param_PendienteGasto, param_fecha, param_idproveedor, _
                    '                          param_tipofactura, param_ptovtaRemito, param_nrocomprRemito, param_remito, _
                    '                          param_factura, param_nota, param_asociargasto, param_IdGastoAsociar, param_useradd, param_res)

                    '    txtID.Text = param_id.Value
                    '    txtCODIGO.Text = param_id.Value
                    'Else
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Update", _
                                              param_id, param_ordenpedido, param_idAlmacen, param_idcliente, param_fecha, param_cancelado, param_deuda, _
                                              param_Subtotal, param_iva, param_total, param_Descuento, param_Porcentaje, param_valorDescuento, _
                                              param_nota, param_idusuario, param_useradd, param_control, param_res)
                    'End If

                    CONTROL = param_control.Value
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

        Dim ValorActual As Double
        Dim IdStockMov As Long
        Dim Stock As Double


        Dim Comprob As String

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count

                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) > 0 And CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value, Decimal) > 0 Then

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
                        param_idpedidosweb.Value = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
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

                        Dim param_UnidadFac As New SqlClient.SqlParameter
                        param_UnidadFac.ParameterName = "@UnidadFac"
                        param_UnidadFac.SqlDbType = SqlDbType.Decimal
                        param_UnidadFac.Precision = 18
                        param_UnidadFac.Scale = 2
                        param_UnidadFac.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value)
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

                        Dim param_control As New SqlClient.SqlParameter
                        param_control.ParameterName = "@CONTROL"
                        param_control.SqlDbType = SqlDbType.SmallInt
                        param_control.Value = CONTROL
                        param_control.Direction = ParameterDirection.Input
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
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Det_Update", _
                                                    param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
                                                    param_marca, param_idunidad, param_qtyenviada, param_UnidadFac, param_precio, _
                                                    param_subtotal, param_iva, param_notadet, param_useradd, param_control, _
                                                    param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)

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

                            '    If ValorActual > 0 Then ' And Not cmbAlmacen.Text.Contains("PRINCIPAL") Then

                            '        Stock = param_Stock.Value
                            '        IdStockMov = param_IdStockMov.Value

                            '        Try
                            '            Dim sqlstring As String
                            '            Dim ds_Empresa As Data.DataSet

                            '            'sqlstring = "update stock set qty = " & ValorActual & ", dateupd=getdate(),userupd= " & UserID & _
                            '            '    " where idmaterial= " & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & _
                            '            '    "  and idunidad= " & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value & _
                            '            '    " and idalmacen = " & cmbAlmacen.SelectedValue

                            '            sqlstring = "exec spStock_Insert " & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & ", " & _
                            '                grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value & ", " & cmbAlmacenes.SelectedValue & ", 'I', " & _
                            '                grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value & ", " & Stock & ", " & IdStockMov & ", '" & Comprob & "', " & 4 & ", " & UserID

                            '            '

                            '            If tranWEB.Sql_Get_Value(sqlstring) > 0 Then
                            '                ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                            '                ds_Empresa.Dispose()

                            '                If cmbAlmacenes.SelectedValue = 1 Then
                            '                    sqlstring = "UPDATE Materiales SET Preciocompra = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.Preciofinal).Value & _
                            '                                ", PrecioCosto = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVenta).Value & " WHERE id = " & param_idmaterial.Value
                            '                Else
                            '                    sqlstring = "UPDATE Materiales SET Preciocompra = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.Preciofinal).Value & _
                            '                               ", PrecioPeron = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVenta).Value & " WHERE id = " & param_idmaterial.Value
                            '                End If

                            '                tranWEB.Sql_Set(sqlstring)
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

    Private Function EliminarRegistro() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Dim resweb As Integer = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            ''Abrir una transaccion para guardar y asegurar que se guarda todo
            'If Abrir_Tran(connection) = False Then
            '    MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Function
            'End If

            Try

                Dim param_idordendecompra As New SqlClient.SqlParameter("@OrdenPedido", SqlDbType.VarChar, ParameterDirection.Input)
                param_idordendecompra.Size = 25
                param_idordendecompra.Value = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text)
                param_idordendecompra.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
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

                Try

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPedidosWEB_Delete_Finalizar", param_idordendecompra, param_userdel, param_nota, param_res)

                    res = param_res.Value

                    If res > 0 Then

                        Try
                            Dim sqlstring As String

                            sqlstring = "exec spPedidosWEB_Delete_Finalizar '" & IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) & "','" & txtNota.Text & "'," & UserID & ""

                            resweb = tranWEB.Sql_Get_Value(sqlstring)

                            If resweb < 0 Then
                                res = -1
                            End If

                        Catch ex As Exception

                            MsgBox("Desde spPedidosWEB_Delete_Finalizar : " + ex.Message)
                            res = -1

                        End Try


                    End If

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

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0

        Try
            If desdeload = False Then
                MDIPrincipal.ActualizarSistema(True)
            End If
            desdeload = False
        Catch ex As Exception
            MsgBox("Error al Descargar pedidos")
            desdeload = False
        End Try

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        LlenarComboClientes()
        DesbloquearComponentes(True)

        lblNroPedido.Text = ""
        lblStatus.Text = "En Proceso"
        'lblFechaEntrega.Text = Date.Now.ToShortDateString

        chkEliminado.Checked = False

        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()



        lblSubtotal.Text = "0"
        lblIVA.Text = "0"
        lblTotal.Text = "0"
        cmbAlmacenes.SelectedIndex = 1
        chkDescuento.Checked = False
        txtDescuento.Text = "0"

        dtpFECHA.Value = Date.Today
        dtpFECHA.Focus()

        band = 1


    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        'If bolModo = False Then
        '    'MsgBox("No se permite la modificación de una recepción. Para modificar la factura vaya a Administración de Gastos en el menú Contabilidad", MsgBoxStyle.Information, "Control de Acceso")
        '    If MessageBox.Show("¿Está seguro que desea modificar la Recepción seleccionada (solo Nro de Remito y Fecha)?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '        Exit Sub
        '    End If
        'End If

        Dim res As Integer, res_item As Integer

        If ReglasNegocio() Then
            If bolModo Then
                Verificar_Datos()
            Else
                bolpoliticas = True
            End If
            If bolpoliticas Then
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
                                Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar la PedidoWEB (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar la PedidoWEB (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case Else
                                ' Cerrar_Tran()

                                '*******************************************************************************************************************
                                'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then

                                If res > 0 And res_item > 0 Then

                                    Dim sqlstring As String
                                    Dim sqlstring2 As String

                                    Try
                                        sqlstring = "exec spPedidosWEB_Update '" & IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) & "'," & IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text) & ",'" & _
                                                    IIf(txtIdCliente.Text = "", cmbClientes.SelectedValue, txtIdCliente.Text) & "','" & Format(dtpFECHA.Value, "MM/dd/yyyy").ToString & " " & Format(dtpFECHA.Value, "hh:mm:ss").ToString & "'," & _
                                                    lblSubtotal.Text & "," & lblIVA.Text & ", " & lblTotal.Text & ", '" & IIf(txtNota.Text = "", " ", txtNota.Text) & "', 0 , 0 , '" & UserID & "', " & IIf(chkDescuento.Checked = True, 1, 0) & "," & _
                                                    IIf(rdPorcentaje.Checked = True, 1, 0) & ", " & IIf(txtDescuento.Text = "", 0, txtDescuento.Text) & "," & UserID & ""


                                        res = tranWEB.Sql_Get_Value(sqlstring)


                                        If res > 0 Then
                                            Try
                                                For i As Integer = 0 To grdItems.Rows.Count - 1
                                                    'controlo que la qty enviada sea mayo a cero y el saldo tambien
                                                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) > 0 Then

                                                        sqlstring2 = "exec spPedidosWEB_Det_Update " & IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text) & "," & _
                                                      "'" & IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) & "'," & _
                                                      "'" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString & "'," & _
                                                      "'" & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value.ToString) & "'," & _
                                                      "'" & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value) & "'," & _
                                                      "" & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & "," & _
                                                      "" & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value) & "," & _
                                                      "" & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value) & "," & _
                                                      "'" & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value.ToString) & "'," & _
                                                      "" & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value, Decimal)) & "," & _
                                                      "" & UserID & "," & _
                                                      "null,null,null"

                                                        res_item = tranWEB.Sql_Get_Value(sqlstring2)

                                                        If Not res_item > 0 Then
                                                            res_item = -1
                                                            Exit For
                                                        End If
                                                    End If
                                                Next
                                            Catch ex As Exception
                                                res_item = -1
                                                MsgBox("Desde UpdWEB_det : " + ex.Message)
                                            End Try
                                        Else
                                            res = -1
                                        End If

                                    Catch ex As Exception
                                        res = -1
                                        MsgBox("Desde UpdWEB_Enca : " + ex.Message)
                                    End Try
                                End If

                                'End If

                                If res > 0 And res_item > 0 Then
                                    Cerrar_Tran()
                                    Imprimir_Pedido(txtIDPedido.Text.ToString)
                                Else
                                    Cancelar_Tran()
                                    Exit Sub
                                End If

                                'saco el bit de enproceso
                                DevolverEstado()

                                bolModo = False
                                PrepararBotones()

                                'rdPendientes.Checked = True
                                'rdAnuladas.Checked = False
                                'rdTodasPed.Checked = False

                                'SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked

                                'DesbloquearComponentes(bolModo)

                                'MDIPrincipal.NoActualizarBase = True
                                btnActualizar_Click(sender, e)

                                Setear_Grilla()

                                btnEnviarTodos.Enabled = False
                                Util.MsgStatus(Status1, "Se ha actualizado el PedidoWEB.", My.Resources.Resources.ok.ToBitmap)
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
                        'res = EliminarRegistro_Gasto()
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
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Solicitud As Boolean

        Try

        Catch ex As Exception

        End Try

        nbreformreportes = "Ordenes de Compra"

        param.AgregarParametros("Código :", "STRING", "", False, txtIDPedido.Text.ToString, "", cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)

            'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)

            rpt.OrdenesDeCompra_Maestro_App(codigo, 0, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)

            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If



    End Sub

    Private Sub btnLlenarGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlenarGrilla.Click
        'Dim i As Integer

        If bolModo Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
            If Me.cmbPedidos.Text <> "" Then
                PrepararGridItems()
                Try
                    LlenarGrid_Items()
                    Contar_Filas()

                Catch ex As Exception
                    'MsgBox(ex.Message)
                End Try
                btnEnviarTodos.Enabled = True
                With (grdItems)
                    .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
                End With
            End If
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            'me fijo si se esta cancelando un envio de pedido para que vuelva el estado orginal del pedido 
            If cmbPedidos.SelectedValue.ToString = "" Then
                DevolverEstado()
            End If
        Catch ex As Exception

        End Try

        btnEnviarTodos.Enabled = False
        DesbloquearComponentes(False)
        grd_CurrentCellChanged(sender, e)
        bolModo = False
        rdPendientes.Checked = True
    End Sub

    Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click

        MDIPrincipal.NoActualizarBase = True

        bolModo = False
        PrepararBotones()

        rdPendientes.Checked = True
        rdAnuladas.Checked = False
        rdTodasPed.Checked = False

        Dim contadorfilas As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Try

            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select COUNT(*) from pedidosweb where status = 'P'")
        ds_Pedidos.Dispose()
        contadorfilas = ds_Pedidos.Tables(0).Rows(0).Item(0)

        If contadorfilas = 0 Then
            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = 1, @Ventas_Norte = 0,@Devolucion = 0"
            rdTodasPed.Checked = True
            DesbloquearComponentes(bolModo)
            btnEnviarTodos.Enabled = False
            Util.MsgStatus(Status1, "Se ha actualizado el PedidoWEB.", My.Resources.Resources.ok.ToBitmap)
            Exit Sub
        Else
            SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = 1, @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = 0,@Devolucion = 0"
            'SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = 1, @PendientesyCumplidas = " & rdTodasPed.Checked
        End If

        'SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked
        'SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = 1, @PendientesyCumplidas = " & rdTodasPed.Checked

        DesbloquearComponentes(bolModo)

        LlenarGrilla()

        btnEnviarTodos.Enabled = False

        Util.MsgStatus(Status1, "Se ha actualizado el PedidoWEB.", My.Resources.Resources.ok.ToBitmap)


    End Sub

    Private Sub btnEnviarTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviarTodos.Click

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

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click

        Dim res As Integer

        If MessageBox.Show("¿Está seguro que desea Finalizar el pedido seleccionado?" & vbCrLf & "Todos los items pendientes con saldo MENOR a la cantidad pedida quedarán con el estado Finalizado", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If chkEliminado.Checked = False Then

            If txtNota.Text = "" Then
                MsgBox("Falta completar campo NOTA. Por favor verifique.", MsgBoxStyle.Information, "Control de Acceso")
                txtNota.Focus()
                Exit Sub
            End If

            'si tiene al menos una recepcion puede finalizar la OC
            'res = CuentaRecepcionesPorOrdenDeCompra(CType(txtID.Text, Long))

            'If res = 0 Then
            '    Util.MsgStatus(Status1, "No se puede Finalizar la Orden de Compra ya que no tiene 'Recepciones' efectuadas." & vbCrLf & "Si desea puede Anular la OC seleccionada.", My.Resources.stop_error.ToBitmap)
            '    Util.MsgStatus(Status1, "No se puede Finalizar la Orden de Compra ya que no tiene 'Recepciones' efectuadas." & vbCrLf & "Si desea puede Anular la OC seleccionada.", My.Resources.stop_error.ToBitmap, True)
            '    Exit Sub
            'End If

            'If MessageBox.Show("Esta acción Finalizará el proceso en todos los items." + vbCrLf + "¿Está seguro que desea Finalizar la OC?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Util.MsgStatus(Status1, "Finalizando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro()
            Select Case res
                Case -1
                    Util.MsgStatus(Status1, "No se pudo Finalizar el Envío.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Finalizar la Envío.", My.Resources.stop_error.ToBitmap, True)
                Case 0
                    Util.MsgStatus(Status1, "No se pudo Finalizar la Envío.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Finalizar la Envío.", My.Resources.stop_error.ToBitmap, True)
                Case Else
                    bolModo = False
                    PrepararBotones()

                    rdPendientes.Checked = False
                    rdAnuladas.Checked = False
                    rdTodasPed.Checked = True

                    SQL = "exec spPedidosWEB_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Ventas_Norte = 0,@Devolucion = 0"
                    'SQL = "exec spPedidosWEB_Select_All  @Eliminado = 0"

                    DesbloquearComponentes(bolModo)

                    'btnActualizar_Click(sender, e)

                    LlenarGrilla()

                    'Setear_Grilla()

                    btnEnviarTodos.Enabled = False

                    Util.MsgStatus(Status1, "Se ha Finalizado la Envío.", My.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se ha Finalizado la Envío.", My.Resources.ok.ToBitmap, True, True)
            End Select
            'Else
            '    Util.MsgStatus(Status1, "Acción de Finalizado cancelada.", My.Resources.stop_error.ToBitmap)
            '    Util.MsgStatus(Status1, "Acción de Finalizado cancelada.", My.Resources.stop_error.ToBitmap, True)
            'End If
        Else
            Util.MsgStatus(Status1, "El registro ya está Finalizado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está Finalizado.", My.Resources.stop_error.ToBitmap, True)
        End If


    End Sub

    Private Sub btnDescargarPedidos_Click(sender As Object, e As EventArgs) Handles btnDescargarPedidos.Click
        Try
            'If bolModo = False Then
            'coloco el gif de sincronizar
            PicDescarga.Image = My.Resources.Sincro
            'actualizo los pedidos de la web
            MDIPrincipal.ActualizarSistema(True)
            'actualizo la grilla
            btnActualizar_Click(sender, e)
            'actualizar el combo de clientes 
            LlenarComboClientes()
            'envio una pausa 
            System.Threading.Thread.Sleep(3000)
            'cambio el icono del boton de descargas
            PicDescarga.Image = My.Resources.SincroOK
            'End If
        Catch ex As Exception
            PicDescarga.Image = My.Resources.SincroFALLA
        End Try

    End Sub

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            If cmbPedidos.SelectedValue.ToString <> "" Then
                DevolverEstado()
            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "   GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit

        editando_celda = False
        Dim descuento As Double
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        Try

            'me fijo si esta cambiando los precios 
            If chkCambiarPrecios.Checked = True Then
                If e.ColumnIndex = ColumnasDelGridItems.Precio Then
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                    End If

                    Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)
                    'voy a descuento para ver si hay algun valor cargado
                    GoTo descuento
                End If


            End If

            If e.ColumnIndex = ColumnasDelGridItems.QtyEnviada Or e.ColumnIndex = ColumnasDelGridItems.Peso Then
                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                End If

                Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)
                'voy a descuento para ver si hay algun valor cargado
                GoTo descuento
            End If

            If e.ColumnIndex = ColumnasDelGridItems.Descuento Then
descuento:
                descuento = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value)

                If descuento = 0 Then
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                    End If
                Else
                    Dim calculo As Double = (descuento * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value) / 100
                    calculo = Math.Round(calculo, 2)
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - calculo
                End If
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)

                If descuento = 100 Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value + "(BONIF.)"
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

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

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



#End Region












End Class
