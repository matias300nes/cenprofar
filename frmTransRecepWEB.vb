Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmTransRecepWEB

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
        IDMovDev_det = 0
        Orden_Item = 1
        Cod_Material = 2
        Nombre_Material = 3
        Cod_Marca = 4
        Marca = 5
        Cod_Unidad = 6
        Unidad = 7
        QtyEnviada = 8
        CantidadPack = 9
        Precio = 10
        SubtotalRecep = 11
        QtyPedido = 12
        Peso = 13
        SubtotalPedido = 14
        QtySaldo = 15
        Status = 16
        FechaCumplido = 17
        Nota_Det = 18
        Stock = 19

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

        band = 0

        btnEliminar.Text = "Anular Mov."
        btnEliminar.Visible = False

        configurarform()
        asignarTags()

        LlenarComboAlmacenes()

        rdTodasPed.Checked = True

        SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked


        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        Setear_Grilla()

        If bolModo = True Then
            LlenarGrid_Items()
            band = 1
            btnDescargarPedidos_Click(sender, e)
            btnNuevo_Click(sender, e)
        Else
            'btnLlenarGrilla.Enabled = bolModo
            DesbloquearComponentes(bolModo)
            LlenarGrid_Items()
        End If

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        grd.Columns(0).Visible = False
        grd.Columns(4).Visible = False
        grd.Columns(6).Visible = False
        grd.Columns(12).Visible = False

        Contar_Filas()

        dtpFECHA.MaxDate = Today.Date

        band = 1

        PicDescarga.Image = My.Resources.Sincro
        'btnDescargarPedidos_Click(sender, e)

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
    Handles txtID.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

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

        If band = 1 Then
            If llenandoCombo = False Then
                btnLlenarGrilla_Click(sender, e)
                OcultarItemsEliminados()
                CalculoSubtotales()
            End If
        End If
    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrigen.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            txtIDAlmacen.Text = cmbOrigen.SelectedValue
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

                txtID.Text = grd.CurrentRow.Cells(0).Value.ToString
                lblNroMov.Text = grd.CurrentRow.Cells(1).Value.ToString
                lblNroAsoc.Text = grd.CurrentRow.Cells(2).Value.ToString
                lblFechaEmicion.Text = grd.CurrentRow.Cells(3).Value
                cmbOrigen.SelectedValue = grd.CurrentRow.Cells(4).Value
                'cmbOrigen.Text = grd.CurrentRow.Cells(5).Value
                lblTotalPedido.Text = grd.CurrentRow.Cells(8).Value.ToString
                lblTotalRecep.Text = grd.CurrentRow.Cells(9).Value.ToString
                txtNota.Text = grd.CurrentRow.Cells(10).Value.ToString
                lblStatus.Text = grd.CurrentRow.Cells(11).Value.ToString
                lblEncargado.Text = grd.CurrentRow.Cells(13).Value.ToString
                dtpFECHA.Value = grd.CurrentRow.Cells(14).Value.ToString

            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub chkAnuladas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnGuardar.Enabled = Not chkAnuladas.Checked
        btnEliminar.Enabled = Not chkAnuladas.Checked
        btnNuevo.Enabled = Not chkAnuladas.Checked
        btnCancelar.Enabled = Not chkAnuladas.Checked

        If chkAnuladas.Checked = True Then
            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked

        Else
            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked

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

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs)
        If band = 0 Then Exit Sub

        CalculoSubtotales()

    End Sub

    Private Sub rdPendientes_CheckedChanged(sender As Object, e As EventArgs) Handles rdPendientes.CheckedChanged

        If band = 1 Then

            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked


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

        SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked


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

            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked

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

    Private Sub rdEnviados_CheckedChanged(sender As Object, e As EventArgs) Handles rdEnviados.CheckedChanged
        If band = 1 Then

            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked

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
            Dim sqlstringEnc As String = "SELECT count(*) FROM [" & NameTable_TransRecepWEB & "] Where (DescargarEnDestino = 1 and Status = 'P' and idDestino = " & Utiles.numero_almacen & ") or (DescargarEnOrigen = 1 and IDOrigen = " & Utiles.numero_almacen & ")"
            'me fijo la cantidad de filas que haya en el encabezado web
            Dim ds_PedidosWEB As DataSet = tranWEB.Sql_Get(sqlstringEnc)
            Dim EncabezadoWEB_Filas As Integer = CInt(ds_PedidosWEB.Tables(0).Rows(0).Item(0))
            If EncabezadoWEB_Filas > 0 Then 'If EncabezadoWEB_Filas <> EncabezadoLocal_Filas Then
                PicDescarga.Image = My.Resources.SincroPendiente
            Else
                PicDescarga.Image = My.Resources.SincroOK
            End If
            TimerDescargas.Enabled = True
        Catch ex As Exception
            'MsgBox(ex.Message)
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
        Me.Text = "Movimientos entre Depósitos"

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
        lblNroMov.Tag = "1"
        lblNroAsoc.Tag = "2"
        lblFechaEmicion.Tag = "3"
        txtIDAlmacen.Tag = "4"
        cmbOrigen.Tag = "5"
        lblDestino.Tag = "7"
        lblTotalPedido.Tag = "8"
        lblTotalRecep.Tag = "9"
        txtNota.Tag = "10"
        lblStatus.Tag = "11"
        lblEncargado.Tag = "13"
        dtpFECHA.Tag = "14"
        lblTipo.Tag = "15"

    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If lblTotalPedido.Text = "" Or lblTotalRecep.Text = "" Then
            Util.MsgStatus(Status1, "El total o Subtotal debe ser válido.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El total o Subtotal debe ser válido.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(lblTotalPedido.Text) = 0 Or CDbl(lblTotalRecep.Text) = 0 Then
            Util.MsgStatus(Status1, "El total o Subtotal no debe ser igual a cero.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El total o Subtotal no debe ser igual a cero.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
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
            If lblNroMov.Text = "" Then
                SQL = "exec spTransRecepWEB_Det_Select_By_IDPedidosWEB @IDTransRecepWEB  = '1'"
            Else
                SQL = "exec spTransRecepWEB_Det_Select_By_IDPedidosWEB @IDTransRecepWEB  = '" & lblNroMov.Text & "'"
            End If
        Else
            SQL = "exec spTransRecepWEB_Det_Select_By_IDPedidosWEB @IDTransRecepWEB  = '" & IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) & "'"
        End If


        GetDatasetItems(grdItems)


        grdItems.Columns(0).Visible = False
        grdItems.Columns(2).Visible = False
        grdItems.Columns(4).Visible = False
        grdItems.Columns(5).Visible = False
        grdItems.Columns(6).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Orden_Item).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Orden_Item).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Orden_Item).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

        grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Marca).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 80
        grdItems.Columns(ColumnasDelGridItems.Unidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        grdItems.Columns(ColumnasDelGridItems.QtyEnviada).ReadOnly = Not bolModo
        If bolModo = True Then
            grdItems.Columns(ColumnasDelGridItems.QtyEnviada).HeaderText = "Qty. a Recep."
        Else
            grdItems.Columns(ColumnasDelGridItems.QtyEnviada).HeaderText = "Qty. Recep."
        End If
        grdItems.Columns(ColumnasDelGridItems.QtyEnviada).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        grdItems.Columns(ColumnasDelGridItems.QtyEnviada).Width = 80

        grdItems.Columns(ColumnasDelGridItems.QtyPedido).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).Width = 80

        grdItems.Columns(ColumnasDelGridItems.QtySaldo).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).Width = 80
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        grdItems.Columns(ColumnasDelGridItems.Precio).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Precio).Width = 80
        grdItems.Columns(ColumnasDelGridItems.Precio).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

        grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft

        grdItems.Columns(ColumnasDelGridItems.SubtotalPedido).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.SubtotalPedido).Width = 100
        grdItems.Columns(ColumnasDelGridItems.SubtotalPedido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

        grdItems.Columns(ColumnasDelGridItems.SubtotalRecep).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.SubtotalRecep).Width = 100
        grdItems.Columns(ColumnasDelGridItems.SubtotalRecep).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).Width = 100
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        grdItems.Columns(ColumnasDelGridItems.Nota_Det).ReadOnly = Not bolModo
        grdItems.Columns(ColumnasDelGridItems.Nota_Det).Width = 120

        'grdItems.Columns(ColumnasDelGridItems.Peso).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Peso).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Stock).Visible = bolModo
        grdItems.Columns(ColumnasDelGridItems.Stock).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Stock).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        grdItems.Columns(ColumnasDelGridItems.CantidadPack).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        grdItems.Columns(ColumnasDelGridItems.CantidadPack).ReadOnly = True

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

    Private Sub LlenarComboAlmacenes()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT C.Codigo, ltrim(rtrim(C.nombre)) as Nombre from TransRecep_WEB o JOIN Almacenes C ON C.Codigo = o.IDOrigen where STATUS = 'P' order by ltrim(rtrim(C.nombre))")
            ds_Cli.Dispose()

            With Me.cmbOrigen
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
            End With

            If ds_Cli.Tables(0).Rows.Count > 0 Then
                cmbOrigen.SelectedIndex = 0
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

    Private Sub DesbloquearComponentes(ByRef habilitar As Boolean)
        dtpFECHA.Enabled = habilitar
        cmbOrigen.Enabled = habilitar
        txtNota.Enabled = habilitar
        cmbPedidos.Enabled = habilitar
        lblNroMov.Enabled = habilitar
        lblNroMov.Enabled = habilitar
        chkCambiarPrecios.Enabled = habilitar

        btnDescargarPedidos.Enabled = Not habilitar
        rdTodasPed.Enabled = Not habilitar
        rdPendientes.Enabled = Not habilitar
        rdAnuladas.Enabled = Not habilitar
        rdEnviados.Enabled = Not habilitar

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

            ds_OC = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT '' AS 'NroMov' Union select NroMov from TransRecep_WEB " & _
                                                                           " where IDOrigen = '" & IIf(txtIDAlmacen.Text = "", cmbOrigen.SelectedValue, txtIDAlmacen.Text) & "' " & _
                                                                           " and  status in ('P')")
            ds_OC.Dispose()

            With Me.cmbPedidos
                .DataSource = ds_OC.Tables(0).DefaultView
                ''.DisplayMember = "OrdenPedido"
                .ValueMember = "NroMov"
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
        Dim sumaR As Double
        'me fijo si se esta haciendo un nuevo pedido asi calculo los subtotales
        If bolModo = True Then
            For i As Integer = 0 To grdItems.Rows.Count - 1
                'me fijo que el saldo sea mayor a cero 
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value > 0 Then
                    'me fijo si esta eliminado el items o si esta anulado la el pedido
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value <> "A" Or lblStatus.Text.Contains("ANULADO") Then
                        sumaR = sumaR + IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value)
                    End If
                End If
                suma = suma + IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalPedido).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalPedido).Value)
            Next

            lblTotalPedido.Text = Math.Round(suma, 2)
            lblTotalRecep.Text = Math.Round(sumaR, 2)
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

                grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value = 0.0
                grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value = 0.0


                'me fijo si el item no esta cumplido
                If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value.ToString = "CUMPLIDO" Then
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).Style.BackColor = Color.LightBlue
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).ReadOnly = False
                    End If
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Style.BackColor = Color.LightBlue
                Else
                    'dejo la columna de qtyenviada en false
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).ReadOnly = True
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).ReadOnly = True
                End If

                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos para consultar el stock", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT QTY FROM STOCK WHERE IDMaterial = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "' AND IDAlmacen = " & Utiles.numero_almacen & "")
                ds.Dispose()

                grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value = ds.Tables(0).Rows(0).Item(0)

            Next
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Imprimir_Pedido(ByVal OrdenPedido As String)
        'Dim rpt As New frmReportes()
        'Dim param As New frmParametros
        'Dim cnn As New SqlConnection(ConnStringSEI)
        ''Dim codigo As String
        'Dim Solicitud As Boolean

        ''nbreformreportes = "Ordenes de Compra"

        ''param.AgregarParametros("Código :", "STRING", "", False, OrdenPedido, "", cnn)
        '' param.ShowDialog()

        ''If cerroparametrosconaceptar = True Then

        ''codigo = param.ObtenerParametros(0)

        ''rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)

        'rpt.OrdenesDeCompra_Maestro_App(OrdenPedido, 0, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
        'cerroparametrosconaceptar = False
        'param = Nothing
        'cnn = Nothing
        'End If
    End Sub

    Private Sub OcultarItemsEliminados()
        'control para aquellos item que esten eliminados 
        If rdAnuladas.Checked = False Then
            For i As Integer = 0 To grdItems.RowCount - 1
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value = "A" Then
                    grdItems.CurrentCell = Nothing
                    grdItems.Rows(i).Visible = False
                End If
            Next
        End If
    End Sub


#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro_TransRecepWEB() As Integer
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
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.Input

                Dim param_ordenpedido As New SqlClient.SqlParameter
                param_ordenpedido.ParameterName = "@NROMOV"
                param_ordenpedido.SqlDbType = SqlDbType.VarChar
                param_ordenpedido.Size = 25
                param_ordenpedido.Value = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text)
                param_ordenpedido.Direction = ParameterDirection.Input

                Dim param_idAlmacen As New SqlClient.SqlParameter
                param_idAlmacen.ParameterName = "@IDALMACEN"
                param_idAlmacen.SqlDbType = SqlDbType.BigInt
                param_idAlmacen.Value = Utiles.numero_almacen
                param_idAlmacen.Direction = ParameterDirection.Input

                Dim param_SubtotalPedido As New SqlClient.SqlParameter
                param_SubtotalPedido.ParameterName = "@TOTALPEDIDO"
                param_SubtotalPedido.SqlDbType = SqlDbType.Decimal
                param_SubtotalPedido.Precision = 18
                param_SubtotalPedido.Size = 2
                param_SubtotalPedido.Value = CDbl(lblTotalPedido.Text)
                param_SubtotalPedido.Direction = ParameterDirection.Input

                Dim param_SubtotalRecep As New SqlClient.SqlParameter
                param_SubtotalRecep.ParameterName = "@TOTALRECEP"
                param_SubtotalRecep.SqlDbType = SqlDbType.Decimal
                param_SubtotalRecep.Precision = 18
                param_SubtotalRecep.Size = 2
                param_SubtotalRecep.Value = CDbl(lblTotalRecep.Text)
                param_SubtotalRecep.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@userupd"
                param_useradd.SqlDbType = SqlDbType.Int
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransRecepWEB_Update", _
                                              param_id, param_ordenpedido, param_idAlmacen, param_SubtotalPedido, param_SubtotalRecep, _
                                              param_nota, param_useradd, param_res)

                    AgregarActualizar_Registro_TransRecepWEB = param_res.Value



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

    Private Function AgregarRegistro_TransRecepWEB_Items() As Integer
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
                        param_id.Direction = ParameterDirection.Input

                        Dim param_idpedidosweb As New SqlClient.SqlParameter
                        param_idpedidosweb.ParameterName = "@IDNroMov"
                        param_idpedidosweb.SqlDbType = SqlDbType.VarChar
                        param_idpedidosweb.Size = 25
                        param_idpedidosweb.Value = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text) 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
                        param_idpedidosweb.Direction = ParameterDirection.Input

                        Dim param_idAlmacen As New SqlClient.SqlParameter
                        param_idAlmacen.ParameterName = "@IDALMACEN"
                        param_idAlmacen.SqlDbType = SqlDbType.BigInt
                        param_idAlmacen.Value = Utiles.numero_almacen
                        param_idAlmacen.Direction = ParameterDirection.Input

                        Dim param_fechaCan As New SqlClient.SqlParameter
                        param_fechaCan.ParameterName = "@FechaCan"
                        param_fechaCan.SqlDbType = SqlDbType.DateTime
                        param_fechaCan.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                        param_fechaCan.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@IDMATERIAL"
                        param_idmaterial.SqlDbType = SqlDbType.VarChar
                        param_idmaterial.Size = 25
                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                        param_idmaterial.Direction = ParameterDirection.Input

                        Dim param_idunidad As New SqlClient.SqlParameter
                        param_idunidad.ParameterName = "@IDUNIDAD"
                        param_idunidad.SqlDbType = SqlDbType.VarChar
                        param_idunidad.Size = 25
                        param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
                        param_idunidad.Direction = ParameterDirection.Input

                        Dim param_cantidadpack As New SqlClient.SqlParameter
                        param_cantidadpack.ParameterName = "@CantidadPACK"
                        param_cantidadpack.SqlDbType = SqlDbType.Decimal
                        param_cantidadpack.Precision = 18
                        param_cantidadpack.Scale = 2
                        param_cantidadpack.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).Value)
                        param_cantidadpack.Direction = ParameterDirection.Input

                        Dim param_precio As New SqlClient.SqlParameter
                        param_precio.ParameterName = "@PRECIO"
                        param_precio.SqlDbType = SqlDbType.Decimal
                        param_precio.Precision = 18
                        param_precio.Scale = 2
                        param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                        param_precio.Direction = ParameterDirection.Input

                        Dim param_qtyenviada As New SqlClient.SqlParameter
                        param_qtyenviada.ParameterName = "@QTYENVIADA"
                        param_qtyenviada.SqlDbType = SqlDbType.Decimal
                        param_qtyenviada.Precision = 18
                        param_qtyenviada.Scale = 2
                        param_qtyenviada.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
                        param_qtyenviada.Direction = ParameterDirection.Input


                        Dim param_subtotalped As New SqlClient.SqlParameter
                        param_subtotalped.ParameterName = "@SubtotalPedido"
                        param_subtotalped.SqlDbType = SqlDbType.Decimal
                        param_subtotalped.Precision = 18
                        param_subtotalped.Scale = 2
                        param_subtotalped.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalPedido).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalPedido).Value)
                        param_subtotalped.Direction = ParameterDirection.Input

                        Dim param_subtotalrec As New SqlClient.SqlParameter
                        param_subtotalrec.ParameterName = "@SubtotalRecepcionado"
                        param_subtotalrec.SqlDbType = SqlDbType.Decimal
                        param_subtotalrec.Precision = 18
                        param_subtotalrec.Scale = 2
                        param_subtotalrec.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value)
                        param_subtotalrec.Direction = ParameterDirection.Input

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
                        param_useradd.ParameterName = "@userupd"
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
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransRecepWEB_Det_Update", _
                                                    param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
                                                    param_idunidad, param_cantidadpack, param_precio, param_qtyenviada, param_fechaCan, _
                                                    param_subtotalped, param_subtotalrec, param_notadet, param_UnidadFac, param_useradd, _
                                                    param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)

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

                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                    i = i + 1

                Loop

                AgregarRegistro_TransRecepWEB_Items = res

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDMovDev_det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDMovDev_det)
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

        Dim res As Integer = 0
        Dim resweb As Integer = 0

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransRecepWEB_Delete_Finalizar", param_idordendecompra, param_userdel, param_nota, param_res)

                    res = param_res.Value

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
        End Try
    End Function

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        LlenarComboAlmacenes()
        DesbloquearComponentes(True)

        lblNroMov.Text = ""
        'lblFechaEntrega.Text = Date.Now.ToShortDateString

        chkEliminado.Checked = False

        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()



        'lblTotalPedido.Text = "0"
        lblTotalRecep.Text = "0"

        dtpFECHA.Value = Date.Today
        dtpFECHA.Focus()

        band = 1


    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        'si esta en enviados salgo del boton de guardar 
        If rdEnviados.Checked Then
            Exit Sub
        End If

        If ReglasNegocio() Then
            If bolModo Then
                Verificar_Datos()
            Else
                bolpoliticas = True
            End If
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro_TransRecepWEB()
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
                        res = AgregarRegistro_TransRecepWEB_Items()
                        Select Case res
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
                                Util.MsgStatus(Status1, "No se pudo registrar la TransRecep (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar la TransRecep (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo agregar el TransRecep (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el TransRecep (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case Else
                                If SubirModificarTransRecepWEB() Then
                                    Cerrar_Tran()
                                Else
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo Actualizar la Movimiento en la WEB.", My.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo Actualizar la Movimiento en la WEB.", My.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                End If

                                bolModo = False
                                PrepararBotones()

                                MDIPrincipal.NoActualizarBase = False
                                btnActualizar_Click(sender, e)

                                Setear_Grilla()

                                btnEnviarTodos.Enabled = False
                                Util.MsgStatus(Status1, "Se ha actualizado el TransRecep.", My.Resources.Resources.ok.ToBitmap)
                        End Select

                End Select
            
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

                                SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked

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
        rdEnviados.Checked = False

        Dim contadorfilas As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Try

            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        ds_Pedidos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select COUNT(*) from TransRecep_WEB where status = 'P' and idDestino = " & Utiles.numero_almacen)
        ds_Pedidos.Dispose()
        contadorfilas = ds_Pedidos.Tables(0).Rows(0).Item(0)

        If contadorfilas = 0 Then
            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked

            rdTodasPed.Checked = True
            DesbloquearComponentes(bolModo)
            btnEnviarTodos.Enabled = False
            Util.MsgStatus(Status1, "Se ha actualizado el Movimiento.", My.Resources.Resources.ok.ToBitmap)
            Exit Sub
        Else
            SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked
        End If

        DesbloquearComponentes(bolModo)

        LlenarGrilla()

        btnEnviarTodos.Enabled = False

        Util.MsgStatus(Status1, "Se ha actualizado el Movimiento.", My.Resources.Resources.ok.ToBitmap)


    End Sub

    Private Sub btnEnviarTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviarTodos.Click

        If MessageBox.Show("Está seguro que desea copiar los valores de la columna Cant. Saldo a la columna Cant. a Enviar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim i As Integer
            Dim suma As Double = 0

            For i = 0 To grdItems.RowCount - 1
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value > 0 Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value

                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value.ToString.ToUpper = "TIRA" Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value.ToString.ToUpper = "HORMA" Then
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value = (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPack).Value) * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value) * (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value))
                    Else
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value = (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
                    End If
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalRecep).Value, 2)
                End If
            Next
            CalculoSubtotales()
        End If
    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click

        Dim res As Integer

        If rdEnviados.Checked Then
            Exit Sub
        End If

        If MessageBox.Show("¿Está seguro que desea Finalizar el pedido seleccionado?" & vbCrLf & "Todos los items pendientes con saldo MENOR a la cantidad pedida quedarán con el estado Finalizado", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If chkEliminado.Checked = False Then

            If txtNota.Text = "" Then
                MsgBox("Falta completar campo NOTA. Por favor verifique.", MsgBoxStyle.Information, "Control de Acceso")
                txtNota.Focus()
                Exit Sub
            End If

            Util.MsgStatus(Status1, "Finalizando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro()
            Select Case res
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo Finalizar el Movimiento.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Finalizar la Movimiento.", My.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo Finalizar la Movimiento.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Finalizar la Movimiento.", My.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                Case Else

                    If FinalizarTransRecepWEB() Then
                        Cerrar_Tran()
                    Else
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo Finalizar la Movimiento en la WEB.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Finalizar la Movimiento en la WEB.", My.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    End If


                    bolModo = False
                    PrepararBotones()

                    rdPendientes.Checked = False
                    rdAnuladas.Checked = False
                    rdTodasPed.Checked = True
                    rdEnviados.Checked = False

                    SQL = "exec spTransRecepWEB_Select_All @Origen = " & Utiles.numero_almacen & ", @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasPed.Checked & ", @Enviados = " & rdEnviados.Checked


                    DesbloquearComponentes(bolModo)

                    LlenarGrilla()

                    btnEnviarTodos.Enabled = False

                    Util.MsgStatus(Status1, "Se ha Finalizado la Movimiento. Recuerdele al depósito de origen que realice la devolución correspondiente de aquellos productos con saldo positivo(Rein. Stock)", My.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se ha Finalizado la Movimiento. Recuerdele al depósito de origen que realice la devolución correspondiente de aquellos productos con saldo positivo(Rein. Stock).", My.Resources.ok.ToBitmap, True, True)
            End Select
        Else
            Util.MsgStatus(Status1, "El registro ya está Finalizado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está Finalizado.", My.Resources.stop_error.ToBitmap, True)
        End If


    End Sub

    Private Sub btnDescargarPedidos_Click(sender As Object, e As EventArgs) Handles btnDescargarPedidos.Click
        Cursor = Cursors.WaitCursor
        Try
            'If bolModo = False Then
            'coloco el gif de sincronizar
            PicDescarga.Image = My.Resources.Sincro
            'actualizo los pedidos Nuevos que recibí de la web
            DescargarNew_TransRecep_Recibidos()
            'descargo los cambios de los pedidos que mande de la web
            DescargarAlter_TransRecep_Recibidos()
            'actualizo la grilla
            btnActualizar_Click(sender, e)
            'actualizar el combo de clientes 
            LlenarComboAlmacenes()
            'envio una pausa 
            System.Threading.Thread.Sleep(3000)
            'cambio el icono del boton de descargas
            PicDescarga.Image = My.Resources.SincroOK
            'End If
        Catch ex As Exception
            Cursor = Cursors.Default
            PicDescarga.Image = My.Resources.SincroFALLA
        End Try
        Cursor = Cursors.Default
    End Sub

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click

    End Sub

#End Region

#Region "   GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit

        editando_celda = False
        'Dim descuento As Double
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        Try

            If chkCambiarPrecios.Checked Then
                If e.ColumnIndex = ColumnasDelGridItems.Precio Then
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalPedido).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalPedido).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyPedido).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                    End If
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalPedido).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalPedido).Value, 2)
                End If
            End If

            If e.ColumnIndex = ColumnasDelGridItems.QtyEnviada Or e.ColumnIndex = ColumnasDelGridItems.CantidadPack Or e.ColumnIndex = ColumnasDelGridItems.Precio Then
                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalRecep).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantidadPack).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantidadPack).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value * IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalRecep).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                End If

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalRecep).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalRecep).Value, 2)
            End If

                '            If e.ColumnIndex = ColumnasDelGridItems.Descuento Then
                'descuento:
                '                descuento = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value)

                '                If descuento = 0 Then
                '                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
                '                    Else
                '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                '                    End If
                '                Else
                '                    Dim calculo As Double = (descuento * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value) / 100
                '                    calculo = Math.Round(calculo, 2)
                '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - calculo
                '                End If
                '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)

                '                If descuento = 100 Then
                '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value + "(BONIF.)"
                '                End If

                '            End If

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


#End Region

#Region "WEB"
    'controlar siempre ultimo parametro (conStock -> Con mi maquina paso false para no generar cambios en el stock de la WEB)
    Private Function SubirModificarTransRecepWEB() As Boolean

        Dim ds_Enca As Data.DataSet
        Dim ds_Det As Data.DataSet
        Dim sqlstring As String
        Dim resString As String
        Dim Tipo As String
        Try

            '-------------Encabezado
            Dim NroMov As String = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text)
            Dim IDDestino As Long = Utiles.numero_almacen
            Dim UserUpd As Integer = UserID
            Dim FechaCan As String
            ds_Enca = SqlHelper.ExecuteDataset(tran, CommandType.Text, "select TotalPedido,TotalRecepcionado,Userupd,DateUpd,DescargarEnOrigen,Nota,FechaCancelacion,status,Tipo from TransRecep_WEB WHERE NroMov = '" & NroMov & "' and IDDestino =  " & IDDestino & " ")
            ds_Enca.Dispose()

            If ds_Enca.Tables(0).Rows.Count > 0 Then
                'BD Local
                'Dim FechaCan As String = Format(dtpFECHA.Value.Date, "dd/MM/yyyy").ToString + " " + Format(Date.Now, "hh:mm:ss").ToString
                'BD web

                If IsDBNull(ds_Enca.Tables(0).Rows(0).Item(6)) Then
                    FechaCan = "NULL"
                Else
                    FechaCan = Format(ds_Enca.Tables(0).Rows(0).Item(6), "MM/dd/yyyy").ToString + " " + Format(Date.Now, "hh:mm:ss").ToString
                End If
                Dim TotalPedido As Double = ds_Enca.Tables(0).Rows(0).Item(0)
                Dim TotalRecepcionado As Double = ds_Enca.Tables(0).Rows(0).Item(1)
                Dim Nota As String = ds_Enca.Tables(0).Rows(0).Item(5).ToString
                Dim StatusEnca As String = ds_Enca.Tables(0).Rows(0).Item(7)
                Tipo = ds_Enca.Tables(0).Rows(0).Item(8)

                sqlstring = "BEGIN TRY BEGIN TRAN "
                sqlstring = sqlstring + " UPDATE [" & NameTable_TransRecepWEB & "] SET" & _
                                        " TotalPedido = " & TotalPedido & "," &
                                        " TotalRecepcionado = " & TotalRecepcionado & "," & _
                                        " Userupd = " & UserUpd & "," & _
                                        " DateUpd = getdate()," & _
                                        " DescargarEnOrigen = 1," & _
                                        " Nota = '" & Nota & "'" & _
                                        " WHERE NroMov = '" & NroMov & "' and IDDestino = " & IDDestino & ""

            Else
                SubirModificarTransRecepWEB = False
                Exit Function
            End If

            ds_Det = SqlHelper.ExecuteDataset(tran, CommandType.Text, " SELECT [IDNroMov],[IDMaterial],[IDUnidad],[UnidadFac]," & _
                                                                      " [CantidadPACK],[Precio],[QtyEnviada],[SubtotalPedido]," & _
                                                                      " [SubtotalRecepcionado],[Nota_Det]" & _
                                                                      " FROM [TransRecep_WEB_det] where [IDNroMov] = '" & NroMov & "' order by OrdenItem,id desc ")
            ds_Det.Dispose()

            If ds_Det.Tables(0).Rows.Count > 0 Then
                '---------------Variable Detalle
                Dim IDNroMov As String = ds_Det.Tables(0).Rows(0).Item(0).ToString
                Dim IDMaterial As String
                Dim IDUnidad As String
                Dim UnidadFac As Double
                Dim CantidadPACK As Double
                Dim Precio As Double
                Dim QtyEnviada As Double
                Dim SubtotalPedido As Double
                Dim SubtotalRecepcionado As Double
                Dim Nota_Det As String

                sqlstring = sqlstring + " declare @IdStockMov BIGINT declare @Comprob VARCHAR(50) declare @Stock DECIMAL(18,2)"

                For i As Integer = 0 To ds_Det.Tables(0).Rows.Count - 1

                    'me fijo que la cantidad enviada es mayor a cero
                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) > 0 And CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value, Decimal) > 0 Then

                        IDMaterial = ds_Det.Tables(0).Rows(i).Item(1).ToString
                        IDUnidad = ds_Det.Tables(0).Rows(i).Item(2).ToString
                        UnidadFac = ds_Det.Tables(0).Rows(i).Item(3)
                        CantidadPACK = ds_Det.Tables(0).Rows(i).Item(4)
                        Precio = ds_Det.Tables(0).Rows(i).Item(5)
                        QtyEnviada = ds_Det.Tables(0).Rows(i).Item(6)
                        SubtotalPedido = ds_Det.Tables(0).Rows(i).Item(7)
                        SubtotalRecepcionado = ds_Det.Tables(0).Rows(i).Item(8)
                        Nota_Det = ds_Det.Tables(0).Rows(i).Item(9)


                        sqlstring = sqlstring + " Set  @comprob = 'T/R: ' + '" & IDNroMov & "'"
                        sqlstring = sqlstring + " select @stock = qty from [" & NameTable_Stock & "] where idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "'" & _
                                                " and idalmacen = " & IDDestino & ""

                        sqlstring = sqlstring + " IF EXISTS (SELECT * FROM [" & NameTable_TransRecepWEBdet & "] WHERE IDNroMov = '" & IDNroMov & "' and idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "' and status = 'P')" & _
                                                " Begin Update [" & NameTable_TransRecepWEBdet & "] set  qtySaldo =  qtySaldo - " & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & "" & _
                                                " Where idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "' and status = 'P' End"

                        sqlstring = sqlstring + " IF EXISTS (SELECT * FROM [" & NameTable_TransRecepWEBdet & "] WHERE IDNroMov = '" & IDNroMov & "' and qtysaldo <= 0 and status = 'P' and idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "')" & _
                                                " Begin UPDATE [" & NameTable_TransRecepWEBdet & "] set status = 'C', fechacumplido = Getdate() WHERE IDNroMov = '" & IDNroMov & "'and qtysaldo <= 0 and status = 'P' and" & _
                                                " idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "' End"

                        sqlstring = sqlstring + " IF not EXISTS (SELECT * FROM [" & NameTable_TransRecepWEBdet & "] WHERE IDNroMov = '" & IDNroMov & "' and qtysaldo > 0 and status = 'P' )" & _
                                                " Begin UPDATE [" & NameTable_TransRecepWEB & "] SET status = 'C', FechaCancelacion = '" & FechaCan & "' WHERE NroMov = '" & IDNroMov & "' End"


                        sqlstring = sqlstring + " BEGIN UPDATE [dbo].[" & NameTable_TransRecepWEBdet & "] SET [UnidadFac] = " & UnidadFac & ", [CantidadPACK] = " & CantidadPACK & "," & _
                                                " [SubtotalPedido] = " & SubtotalPedido & " ,[SubtotalRecepcionado] = " & SubtotalRecepcionado & " ,[QtyEnviada] = " & QtyEnviada & "," & _
                                                " [Precio] = " & Precio & ", [Nota_Det] = '" & Nota_Det & "', [DateUpd] = GETDATE(), [UserUpd] = " & UserUpd & " WHERE IDNroMov = '" & IDNroMov & "'" & _
                                                " and idmaterial = '" & IDMaterial & "' END "

                        sqlstring = sqlstring + " BEGIN insert into [" & NameTable_StockMov & "] (comprobante,idalmacen, idmotivo,idmaterial, tipo,stock, qty,saldo   ,idunidad ,eliminado,[dateadd],useradd)" & _
                            " select @comprob, " & IDDestino & ", 4 ,'" & IDMaterial & "','I' ,isnull(@stock,0),abs( " & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & ")," & _
                            " isnull(@stock,0)+" & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & ",'" & IDUnidad & "',0,getdate()," & UserUpd & " From [" & NameTable_Stock & "] " & _
                            " where idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "' and idalmacen = " & IDDestino & " " & _
                            " update [" & NameTable_Stock & "] set qty = qty + " & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal) & ", dateupd=getdate(),userupd=" & UserUpd & "," & _
                            " ActualizadoLocal = 1 where idmaterial = '" & IDMaterial & "' and idunidad = '" & IDUnidad & "' and idalmacen = " & IDDestino & " END "
                    End If
                Next
                txtPrueba.Text = sqlstring
                sqlstring = sqlstring + " COMMIT TRAN End Try BEGIN CATCH ROLLBACK TRAN END CATCH"

                resString = tranWEB.Sql_Set(sqlstring)

                If resString = "Funcionó" Then
                    Try
                        Dim dsRes As Data.DataSet
                        dsRes = tranWEB.Sql_Get("Select IdOrigen from  [" & NameTable_TransRecepWEB & "] WHERE NroMov = '" & NroMov & "' and IDDestino = " & IDDestino & "")
                        Dim idorigen As Long = dsRes.Tables(0).Rows(0)(0)
                        If Tipo = "T" Then
                            sqlstring = "update [" & NameTable_NotificacionesWEB & "] set Transferencias = 1 where idalmacen = " & idorigen
                        Else
                            sqlstring = "update [" & NameTable_NotificacionesWEB & "] set Recepciones = 1 where idalmacen = " & idorigen
                        End If
                        tranWEB.Sql_Set(sqlstring)
                    Catch ex As Exception
                        'MsgBox(ex)
                    End Try
                    SubirModificarTransRecepWEB = True
                Else
                    SubirModificarTransRecepWEB = False
                End If

            Else
                SubirModificarTransRecepWEB = False
                Exit Function
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            SubirModificarTransRecepWEB = False
        End Try
    End Function

    Private Function FinalizarTransRecepWEB() As Boolean
        Dim sqlstring As String
        Dim resString As String
        Dim NroMov As String = IIf(txtIDPedido.Text = "", cmbPedidos.SelectedValue, txtIDPedido.Text)
        Dim Nota As String = txtNota.Text
        Dim userdel As Integer = UserID

        Try
            sqlstring = "BEGIN TRY BEGIN TRAN "
            sqlstring = sqlstring + " update [" & NameTable_TransRecepWEBdet & "]  set Status = 'F', UserUpd = " & userdel & "," & _
                                    " DateUpd = GetDate() where IDNroMov =  '" & NroMov & "' AND " & _
                                    " Status = 'P' AND QtySaldo <= QtyPedida " & _
                                    " update [" & NameTable_TransRecepWEB & "] set Status = 'F', UserUpd = " & userdel & ", DateUpd = GetDate() ," & _
                                    " Nota = '" & Nota & "', DescargarEnOrigen = 1 where NroMov = '" & NroMov & "'"

            sqlstring = sqlstring + " COMMIT TRAN End Try BEGIN CATCH ROLLBACK TRAN END CATCH"

            resString = tranWEB.Sql_Set(sqlstring)

            If resString = "Funcionó" Then
                FinalizarTransRecepWEB = True
            Else
                FinalizarTransRecepWEB = False
            End If

        Catch ex As Exception
            FinalizarTransRecepWEB = False
        End Try


    End Function

    Private Sub DescargarNew_TransRecep_Recibidos()

        Dim sqlstring As String
        Dim sqlstring2 As String
        Dim sqlstringEnc As String
        Dim sqlstringDet As String
        Dim ds_Pedidos As Data.DataSet
        Dim ds_Empresa As Data.DataSet
        Dim ds_Marcas As Data.DataSet

        'Dim EncabezadoWEB_Filas As Integer = 0
        'Dim EncabezadoLocal_Filas As Integer = 0

        'Dim DetalleWEB_Filas As Integer = 0
        'Dim DetalleLocal_Filas As Integer = 0

        Try
            'borro las temporales
            ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpTransRecep_WEB ")
            ds_Pedidos.Dispose()
            ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpTransRecep_WEB_det ")
            ds_Pedidos.Dispose()

            'sqlstringEnc = "SELECT count(*) FROM TransRecep_WEB Where DescargarEnDestino = 1 and Status = 'P' and idDestino = " & Utiles.numero_almacen & ""
            ''cuento las filas del encabezado web
            'Dim ds_PedidosWEB As DataSet = tranWEB.Sql_Get(sqlstringEnc)
            'EncabezadoWEB_Filas = CInt(ds_PedidosWEB.Tables(0).Rows(0).Item(0))

            sqlstringEnc = "SELECT * FROM [" & NameTable_TransRecepWEB & "] Where DescargarEnDestino = 1 and Status = 'P' and idDestino = " & Utiles.numero_almacen & ""
            'traigo los valores del encabezado
            Dim ds_FamiliasWEB As DataSet = tranWEB.Sql_Get(sqlstringEnc)
            Dim bulk_familias As New SqlBulkCopy(ConexionWEB, SqlBulkCopyOptions.TableLock, Nothing)

            'alojo los datos en la tabla temporal
            ConexionWEB.Open()
            bulk_familias.DestinationTableName = "tmpTransRecep_WEB"
            bulk_familias.WriteToServer(ds_FamiliasWEB.Tables(0))
            ConexionWEB.Close()
            ''cuento las filas locales 
            'sqlstring = " SELECT count(*) FROM tmpTransRecep_WEB"

            'ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            'ds_Pedidos.Dispose()
            'EncabezadoLocal_Filas = CInt(ds_Pedidos.Tables(0).Rows(0).Item(0))
            'comparo la cantidad de filas de la web con las locales 
            'If EncabezadoWEB_Filas = EncabezadoLocal_Filas Then

            'sqlstringDet = "SELECT count(*) From TransRecep_WEB T Join TransRecep_WEB_det Td on T.NroMov = td.IDNroMov " & _
            '               "where t.DescargarEnDestino = 1 And T.Status = 'P' And t.IDDestino = " & Utiles.numero_almacen & ""
            ''me fijo en el detalle en la web 
            'ds_PedidosWEB = tranWEB.Sql_Get(sqlstringDet)
            'DetalleWEB_Filas = CInt(ds_PedidosWEB.Tables(0).Rows(0).Item(0))

            sqlstringDet = "SELECT td.ID,td.IDNroMov,td.IDMaterial,td.IDMarca,td.IDUnidad,td.UnidadFac," & _
                           "td.CantidadPACK,td.Precio,td.QtyPedida,td.QtyEnviada,td.QtySaldo,td.SubtotalPedido," & _
                           "td.SubtotalRecepcionado,td.Status,td.FechaCumplido,td.Nota_Det,td.OrdenItem,td.Fecha," & _
                           "td.DateUpd,td.UserUpd From [" & NameTable_TransRecepWEB & "] T Join [" & NameTable_TransRecepWEBdet & "] Td on T.NroMov = td.IDNroMov " & _
                           "where t.DescargarEnDestino = 1 and t.Status = 'P' And t.IDDestino = " & Utiles.numero_almacen & ""

            'traigo los valores de los detalles del a web 
            Dim ds_FamiliasWEB_det = tranWEB.Sql_Get(sqlstringDet)
            Dim bulk_familias_det As New SqlBulkCopy(ConexionWEB, SqlBulkCopyOptions.TableLock, Nothing)

            'los guardo en los valores de detalle en la temporal 
            ConexionWEB.Open()
            bulk_familias_det.DestinationTableName = "tmpTransRecep_WEB_det"
            bulk_familias_det.WriteToServer(ds_FamiliasWEB_det.Tables(0))
            ConexionWEB.Close()

            ''cuanto las filas de la tabla temporal 
            'sqlstring = " SELECT count(*) FROM tmpTransRecep_WEB_det "

            'ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            'ds_Pedidos.Dispose()
            'DetalleLocal_Filas = CInt(ds_Pedidos.Tables(0).Rows(0).Item(0))

            'If DetalleWEB_Filas = DetalleLocal_Filas Then

            'pedidos que están en la WEB y no están en la sucursal/central
            sqlstring = " SELECT [NroMov],[NroAsociado],[Tipo],[Fecha],[IDOrigen],[IDDestino],[TotalPedido],[TotalRecepcionado],[Status]," & _
                       " [Nota],[IDEmpleado],[DescargarEnOrigen],[DescargarEnDestino]" & _
                       " FROM tmpTransRecep_WEB WHERE NroMov NOT IN (SELECT NroMov FROM TransRecep_WEB )"

            ds_Marcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Marcas.Dispose()

            sqlstring2 = " SELECT [IDNroMov],[IDMaterial],[IDMarca],[IDUnidad],[UnidadFac],[CantidadPACK],[Precio]," & _
                         " [QtyPedida],[QtyEnviada],[QtySaldo],[SubtotalPedido],[SubtotalRecepcionado],[Status]," & _
                         " [Nota_Det],[OrdenItem],[Fecha]" & _
                         " FROM [dbo].[tmpTransRecep_WEB_det] WHERE IDNroMov NOT IN (SELECT IDNroMov FROM TransRecep_WEB_det ) " 'order by IDNroMov,OrdenItem asc  "

            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring2)
            ds_Empresa.Dispose()

            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim ds_tmpMarcas As Data.DataSet
            Dim sqlstring_pasar As String

            'me fijo haya datos en el encabezado y el detalle 
            If ds_Marcas.Tables(0).Rows.Count > 0 And ds_Empresa.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds_Marcas.Tables(0).Rows.Count - 1

                    sqlstring_pasar = "BEGIN TRY BEGIN TRAN " & _
                        " INSERT INTO [dbo].[TransRecep_WEB] ([NroMov],[NroAsociado],[Tipo],[Fecha],[IDOrigen],[IDDestino],[TotalPedido]," & _
                       " [TotalRecepcionado],[Status],[Nota],[IDEmpleado],[DescargarEnOrigen],[DescargarEnDestino])" & _
                        " VALUES ( '" & ds_Marcas.Tables(0).Rows(i)(0) & "', '" & ds_Marcas.Tables(0).Rows(i)(1) & "', '" & _
                        ds_Marcas.Tables(0).Rows(i)(2) & "','" & Format(ds_Marcas.Tables(0).Rows(i)(3), "dd/MM/yyyy hh:ss") & "'," & _
                        ds_Marcas.Tables(0).Rows(i)(4) & "," & ds_Marcas.Tables(0).Rows(i)(5) & "," & _
                        ds_Marcas.Tables(0).Rows(i)(6) & "," & ds_Marcas.Tables(0).Rows(i)(7) & ",'" & ds_Marcas.Tables(0).Rows(i)(8) & "','" & _
                        ds_Marcas.Tables(0).Rows(i)(9) & "','" & ds_Marcas.Tables(0).Rows(i)(10) & "'," & _
                        IIf(CBool(ds_Marcas.Tables(0).Rows(i)(11)) = True, 1, 0) & " , " & IIf(CBool(ds_Marcas.Tables(0).Rows(i)(12)) = True, 1, 0) & ") "


                    For j = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                        'controlo que el numero de orden coincidan y hago el insert del detalle 
                        If ds_Marcas.Tables(0).Rows(i)(0).ToString = ds_Empresa.Tables(0).Rows(j)(0).ToString Then

                            sqlstring_pasar = sqlstring_pasar + " INSERT INTO [dbo].[TransRecep_WEB_det] ([IDNroMov],[IDMaterial],[IDMarca],[IDUnidad],[UnidadFac],[CantidadPACK],[Precio]," & _
                                                                " [QtyPedida],[QtyEnviada],[QtySaldo],[SubtotalPedido],[SubtotalRecepcionado],[Status],[Nota_Det],[OrdenItem],[Fecha]) " & _
                                                                " VALUES ( '" & ds_Empresa.Tables(0).Rows(j)(0) & "', '" & ds_Empresa.Tables(0).Rows(j)(1) & "','" & _
                                                                ds_Empresa.Tables(0).Rows(j)(2) & "','" & ds_Empresa.Tables(0).Rows(j)(3) & "'," & ds_Empresa.Tables(0).Rows(j)(4) & "," & _
                                                                ds_Empresa.Tables(0).Rows(j)(5) & "," & ds_Empresa.Tables(0).Rows(j)(6) & "," & ds_Empresa.Tables(0).Rows(j)(7) & "," & _
                                                                ds_Empresa.Tables(0).Rows(j)(8) & ", " & ds_Empresa.Tables(0).Rows(j)(9) & "," & ds_Empresa.Tables(0).Rows(j)(10) & "," & _
                                                                ds_Empresa.Tables(0).Rows(j)(11) & ",'" & ds_Empresa.Tables(0).Rows(j)(12) & "','" & ds_Empresa.Tables(0).Rows(j)(13) & "'," & _
                                                                ds_Empresa.Tables(0).Rows(j)(14) & ",'" & Format(ds_Empresa.Tables(0).Rows(i)(15), "dd/MM/yyyy hh:ss") & "') "

                        End If

                    Next

                    sqlstring_pasar = sqlstring_pasar + " COMMIT TRAN End Try BEGIN CATCH ROLLBACK TRAN END CATCH"

                    ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasar)
                    ds_tmpMarcas.Dispose()
                    Try
                        tranWEB.Sql_Set("Update [" & NameTable_TransRecepWEB & "] Set DescargarEnDestino = 0 Where NroMov = '" & ds_Marcas.Tables(0).Rows(i)(0) & "' AND [NroAsociado] = '" & ds_Marcas.Tables(0).Rows(i)(1) & "'")
                    Catch ex As Exception
                        MsgBox("No se pudo actualizar el campo descargado Destino (Encabezado) de la WEB.", MsgBoxStyle.Exclamation)
                    End Try

                Next
                ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "update TransRecep_WEB set DescargarEnDestino = 0,DescargarEnOrigen = 0")
                ds_tmpMarcas.Dispose()
            End If
            'Else
            'MsgBox("No se pudo sincronizar los datos de movimientos (detalle) de la WEB. Por Favor intente más tarde.", MsgBoxStyle.Exclamation)
            'Exit Sub
            'End If
            'Else
            '    MsgBox("No se pudo sincronizar los datos de movimientos (encabezado) de la WEB. Por Favor intente más tarde.", MsgBoxStyle.Exclamation)
            '    Exit Sub
            'End If

        Catch ex As Exception

            MsgBox("Desde Descargas Nuevas de TransRecep_WEB " + ex.Message)
        End Try

    End Sub

    Private Sub DescargarAlter_TransRecep_Recibidos()
        Dim sqlstring As String
        Dim sqlstring2 As String
        Dim sqlstringEnc As String
        Dim sqlstringDet As String
        Dim ds_Pedidos As Data.DataSet
        Dim ds_Empresa As Data.DataSet
        Dim ds_Marcas As Data.DataSet

        Try
            'borro las temporales
            ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpTransRecep_WEB ")
            ds_Pedidos.Dispose()
            ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpTransRecep_WEB_det ")
            ds_Pedidos.Dispose()

            sqlstringEnc = "SELECT * FROM [" & NameTable_TransRecepWEB & "] Where DescargarEnOrigen = 1 and idOrigen = " & Utiles.numero_almacen & ""
            'traigo los valores del encabezado
            Dim ds_FamiliasWEB As DataSet = tranWEB.Sql_Get(sqlstringEnc)
            Dim bulk_familias As New SqlBulkCopy(ConexionWEB, SqlBulkCopyOptions.TableLock, Nothing)

            'alojo los datos en la tabla temporal
            ConexionWEB.Open()
            bulk_familias.DestinationTableName = "tmpTransRecep_WEB"
            bulk_familias.WriteToServer(ds_FamiliasWEB.Tables(0))
            ConexionWEB.Close()


            sqlstringDet = "SELECT td.ID,td.IDNroMov,td.IDMaterial,td.IDMarca,td.IDUnidad,td.UnidadFac," & _
                           "td.CantidadPACK,td.Precio,td.QtyPedida,td.QtyEnviada,td.QtySaldo,td.SubtotalPedido," & _
                           "td.SubtotalRecepcionado,td.Status,td.FechaCumplido,td.Nota_Det,td.OrdenItem,td.Fecha," & _
                           "td.DateUpd,td.UserUpd From [" & NameTable_TransRecepWEB & "] T Join [" & NameTable_TransRecepWEBdet & "] Td on T.NroMov = td.IDNroMov " & _
                           "where t.DescargarEnOrigen = 1 And t.IDOrigen = " & Utiles.numero_almacen & ""

            'traigo los valores de los detalles del a web 
            Dim ds_FamiliasWEB_det = tranWEB.Sql_Get(sqlstringDet)
            Dim bulk_familias_det As New SqlBulkCopy(ConexionWEB, SqlBulkCopyOptions.TableLock, Nothing)

            'los guardo en los valores de detalle en la temporal 
            ConexionWEB.Open()
            bulk_familias_det.DestinationTableName = "tmpTransRecep_WEB_det"
            bulk_familias_det.WriteToServer(ds_FamiliasWEB_det.Tables(0))
            ConexionWEB.Close()

            'pedidos que están la temporal
            sqlstring = " SELECT [NroMov],[NroAsociado],[Fecha],[TotalPedido],[TotalRecepcionado],[Status]," & _
                       " [Nota],isnull([FechaCancelacion],'1900-01-01 00:00:00.000') as FechaCancelacion,[DateUpd],[UserUpd]" & _
                       " FROM tmpTransRecep_WEB "

            ds_Marcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Marcas.Dispose()

            sqlstring2 = " SELECT [IDNroMov],[IDMaterial],[UnidadFac],[CantidadPACK],[Precio]," & _
                         " [QtyEnviada],[QtySaldo],[SubtotalPedido],[SubtotalRecepcionado],[Status]," & _
                         " isnull([FechaCumplido],'1900-01-01 00:00:00.000') as FechaCumplido,[Nota_Det],[DateUpd],[UserUpd]" & _
                         " FROM [dbo].[tmpTransRecep_WEB_det] "

            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring2)
            ds_Empresa.Dispose()

            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim ds_tmpMarcas As Data.DataSet
            Dim sqlstring_pasar As String

            'me fijo haya datos en el encabezado y el detalle 
            If ds_Marcas.Tables(0).Rows.Count > 0 And ds_Empresa.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds_Marcas.Tables(0).Rows.Count - 1

                    sqlstring_pasar = " BEGIN TRY BEGIN TRAN " & _
                                      " UPDATE [dbo].[TransRecep_WEB] SET" & _
                                      " [Fecha] = '" & Format(ds_Marcas.Tables(0).Rows(i)(2), "dd/MM/yyyy hh:ss") & "'," & _
                                      " [TotalPedido] = " & ds_Marcas.Tables(0).Rows(i)(3) & "," & _
                                      " [TotalRecepcionado] = " & ds_Marcas.Tables(0).Rows(i)(4) & "," & _
                                      " [Status] = '" & ds_Marcas.Tables(0).Rows(i)(5) & "'," & _
                                      " [Nota] = '" & ds_Marcas.Tables(0).Rows(i)(6) & "'," & _
                                      " [FechaCancelacion] = '" & Format(ds_Marcas.Tables(0).Rows(i)(7), "dd/MM/yyyy hh:ss") & "'," & _
                                      " [DescargarEnOrigen] = 1," & _
                                      " [DateUpd] = '" & Format(ds_Marcas.Tables(0).Rows(i)(8), "dd/MM/yyyy hh:ss") & "'," & _
                                      " [UserUpd] = " & ds_Marcas.Tables(0).Rows(i)(9) & " " & _
                                      " WHERE [NroMov] = '" & ds_Marcas.Tables(0).Rows(i)(0) & "' AND [NroAsociado] = '" & ds_Marcas.Tables(0).Rows(i)(1) & "'"
           
                    For j = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                        'controlo que el numero de orden coincidan y hago el insert del detalle 
                        If ds_Marcas.Tables(0).Rows(i)(0).ToString = ds_Empresa.Tables(0).Rows(j)(0).ToString Then

                            sqlstring_pasar = sqlstring_pasar + " UPDATE [dbo].[TransRecep_WEB_det] SET " & _
                                                                " [UnidadFac] = " & ds_Empresa.Tables(0).Rows(j)(2) & "," & _
                                                                " [CantidadPACK] = " & ds_Empresa.Tables(0).Rows(j)(3) & "," & _
                                                                " [Precio] = " & ds_Empresa.Tables(0).Rows(j)(4) & "," & _
                                                                " [QtyEnviada] = " & ds_Empresa.Tables(0).Rows(j)(5) & "," & _
                                                                " [QtySaldo] = " & ds_Empresa.Tables(0).Rows(j)(6) & "," & _
                                                                " [SubtotalPedido] = " & ds_Empresa.Tables(0).Rows(j)(7) & "," & _
                                                                " [SubtotalRecepcionado] =" & ds_Empresa.Tables(0).Rows(j)(8) & "," & _
                                                                " [Status] = '" & ds_Empresa.Tables(0).Rows(j)(9) & "'," & _
                                                                " [FechaCumplido] = '" & Format(ds_Empresa.Tables(0).Rows(j)(10), "dd/MM/yyyy hh:ss") & "'," & _
                                                                " [Nota_Det] = '" & ds_Empresa.Tables(0).Rows(j)(11) & "'," & _
                                                                " [DateUpd] = '" & Format(ds_Empresa.Tables(0).Rows(j)(12), "dd/MM/yyyy hh:ss") & "'," & _
                                                                " [UserUpd] = " & ds_Empresa.Tables(0).Rows(j)(13) & " " & _
                                                                " WHERE [IDNroMov] = '" & ds_Empresa.Tables(0).Rows(j)(0) & "'" & _
                                                                " AND [IDMaterial] = '" & ds_Empresa.Tables(0).Rows(j)(1) & "'"
                        End If

                    Next

                    sqlstring_pasar = sqlstring_pasar + " COMMIT TRAN End Try BEGIN CATCH ROLLBACK TRAN END CATCH"

                    ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasar)
                    ds_tmpMarcas.Dispose()
                    Try
                        tranWEB.Sql_Set("Update [" & NameTable_TransRecepWEB & "] Set DescargarEnOrigen = 0 Where NroMov = '" & ds_Marcas.Tables(0).Rows(i)(0) & "' AND [NroAsociado] = '" & ds_Marcas.Tables(0).Rows(i)(1) & "'")
                    Catch ex As Exception
                        MsgBox("No se pudo actualizar el campo descargado Origen (Encabezado) de la WEB.", MsgBoxStyle.Exclamation)
                    End Try

                Next
                ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "update TransRecep_WEB set DescargarEnOrigen = 0,DescargarEnDestino = 0")
                ds_tmpMarcas.Dispose()
            End If

        Catch ex As Exception

            MsgBox("Desde Descargas Modificados de TransRecep_WEB " + ex.Message)
        End Try
    End Sub

#End Region












  
End Class
