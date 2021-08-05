Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmTransferenciasPorkys

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


    Dim CONTROL As Integer


    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems

        IDTrans_det = 0
        Orden_Item = 1
        Cod_Material = 2
        Nombre_Material = 3
        Cod_Marca = 4
        Marca = 5
        Cod_Unidad = 6
        Unidad = 7
        Cantidad = 8
        Nota_Det = 9
        Stock = 10
        Eliminado = 11

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



#Region "   Eventos"

    Private Sub frmPedidosWEB_Gestion_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Items()
            End If
        End If
    End Sub

    Private Sub frmAjustes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        btnSalir_Click(sender, e)
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
        btnActivar.Visible = False

        'Try

        '    Dim sqlstring As String = "update NotificacionesWEB set BloqueoT = 1"
        '    tranWEB.Sql_Set(sqlstring)
        'Catch ex As Exception

        'End Try

        band = 0

        configurarform()
        asignarTags()

        LlenarcmbOrigen()
        LlenarcmbDestino()
        llenarcmbEncargados()

        Me.LlenarCombo_Productos()


        SQL = "exec spTransferencias_Porkys_Select_All @Eliminado = 0 "


        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        'Setear_Grilla()

        If bolModo = True Then
            band = 1
            btnNuevo_Click(sender, e)
        Else
            DesbloquearComponentes(bolModo)
            LlenarGrid_Items()
        End If

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        grd.Columns(0).Visible = False
        grd.Columns(3).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(7).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(12).Visible = False
        grd.Columns(13).Visible = False

        dtpFECHA.MinDate = Today.Date.AddDays(-2)
        dtpFECHA.MaxDate = Today.Date

        'grdItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect

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

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtNota.KeyPress
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

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminados.CheckedChanged
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked
        btnNuevo.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        Label5.Visible = chkEliminados.Checked
        lblAutorizado.Visible = chkEliminados.Checked

        bolModo = False

        SQL = "exec spTransferencias_Porkys_Select_All @Eliminado = " & chkEliminados.Checked

        Try
            LlenarGrilla()

        Catch ex As Exception

        End Try
    End Sub

    'grd_CurrentCellChanged
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        If Permitir Then
            Try
                txtID.Text = grd.CurrentRow.Cells(0).Value.ToString
                lblNroTrans.Text = grd.CurrentRow.Cells(1).Value.ToString
                dtpFECHA.Value = grd.CurrentRow.Cells(2).Value
                cmbOrigen.SelectedValue = grd.CurrentRow.Cells(3).Value.ToString
                cmbDestino.SelectedValue = grd.CurrentRow.Cells(5).Value.ToString
                cmbEncargado.SelectedValue = grd.CurrentRow.Cells(9).Value
                txtNota.Text = grd.CurrentRow.Cells(11).Value.ToString
                lblAutorizado.Text = grd.CurrentRow.Cells(13).Value.ToString

            Catch ex As Exception

            End Try
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
            ' lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y - variableajuste)

        Else
            chkGrillaInferior.Text = "Aumentar Grilla Inferior"
            chkGrillaInferior.Location = New Point(chkGrillaInferior.Location.X, chkGrillaInferior.Location.Y + variableajuste)
            GroupBox1.Height = GroupBox1.Height + variableajuste
            grd.Location = New Point(xgrd, ygrd + variableajuste)
            grd.Height = hgrd - variableajuste
            grdItems.Height = grdItems.Height + variableajuste
            Label19.Location = New Point(Label19.Location.X, Label19.Location.Y + variableajuste)
            '  lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y + variableajuste)

        End If

    End Sub

    Private Sub cmbOrigen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrigen.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDOrigen.Text = cmbOrigen.SelectedValue
                If cmbProducto.Text <> "" Then
                    cmbProducto_SelectedValueChanged(sender, e)
                End If
                If bolModo And grdItems.Rows.Count > 0 Then
                    ControlItem()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbDestino_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDestino.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDDestino.Text = cmbDestino.SelectedValue
                If bolModo And grdItems.Rows.Count > 0 Then
                    ControlItem()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbRepartidor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEncargado.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDEncargado.Text = cmbEncargado.SelectedValue

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
            txtCantidad.Text = ""
        End If

    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged
        If band = 1 Then


            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen," & _
                                        " m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra " & _
                                         " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                         "JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                         " where m.Codigo = '" & cmbProducto.SelectedValue & "' AND s.idalmacen = " & IIf(txtIDOrigen.Text.ToString = "", cmbOrigen.SelectedValue, txtIDOrigen.Text))

            ds_Producto.Dispose()

            Try

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
                txtIDMarca.Text = ds_Producto.Tables(0).Rows(0)(12)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter And bolModo Then

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

            'If CDbl(lblStock.Text < 0) Or CDbl(txtCantidad.Text) > CDbl(lblStock.Text) Then

            '    If txtIdUnidad.Text <> "PACK" Then
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

            'If txtUnidad.Text.Contains("HORMA") Or txtUnidad.Text.Contains("TIRA") Then
            '    If txtPeso.Text = "" Then
            '        Util.MsgStatus(Status1, "Debe ingresar el peso del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
            '        Util.MsgStatus(Status1, "Debe ingresar el peso del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
            '        Exit Sub
            '    End If
            '    If CDbl(txtPeso.Text) = 0 Then
            '        Util.MsgStatus(Status1, "Debe ingresar un peso válido del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
            '        Util.MsgStatus(Status1, "Debe ingresar un peso válido del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
            '        Exit Sub
            '    End If
            'End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbProducto.Text = grdItems.Rows(i).Cells(2).Value Then
                    If MessageBox.Show("El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (i + 1).ToString & ". Desea cargar el producto igual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            Next


            grdItems.Rows.Add(0, i + 1, cmbProducto.SelectedValue, cmbProducto.Text, txtIDMarca.Text, txtMarca.Text, txtIdUnidad.Text, txtUnidad.Text, txtCantidad.Text, "", 0, False, "Eliminado")

            OrdenarFilas()
            CantidadTotal()

            txtCantidad.Text = ""
            cmbProducto.Text = ""
            lblStock.Text = "0.00"


            txtNota.Focus()
            SendKeys.Send("{TAB}")

        End If

    End Sub

    Private Sub PicRepartidor_Click(sender As Object, e As EventArgs) Handles PicEncargado.Click
        Dim R As New frmEmpleados
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbEncargado.Text
        R.ShowDialog()
        'LlenarcmbFamilias_App(cmbFAMILIAS, ConnStringSEI)
        llenarcmbEncargados()
        cmbEncargado.Text = texto_del_combo
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

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Transferencias"

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

        Me.WindowState = FormWindowState.Normal

        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 100))

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)


    End Sub

    Private Sub asignarTags()

        txtID.Tag = "0"
        lblNroTrans.Tag = "1"
        dtpFECHA.Tag = "2"
        txtIDOrigen.Tag = "3"
        cmbOrigen.Tag = "4"
        txtIDDestino.Tag = "5"
        cmbDestino.Tag = "6"
        txtIDEncargado.Tag = "9"
        cmbEncargado.Tag = "10"
        txtNota.Tag = "11"
        lblAutorizado.Tag = "13"


    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        'verifico que el orgen y destino sean distintos 
        If cmbOrigen.SelectedValue = cmbDestino.SelectedValue Then
            Util.MsgStatus(Status1, "El Origen y Destino no pueden coincidir. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El Origen y Destino no pueden coincidir. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
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

                ''controlo el precio 
                'Try
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value Then
                '        Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '        Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '        Exit Sub
                '    Else
                '        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = 0 Then
                '            Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '            Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '            Exit Sub
                '        End If
                '    End If

                'Catch ex As Exception
                '    Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '    Util.MsgStatus(Status1, "El precio es incorrecto del producto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '    Exit Sub
                'End Try


                ''control con aquellos productos que se facturan por peso 
                'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then

                '    Try
                '        'qty es válida?
                '        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value Is System.DBNull.Value Then
                '            Util.MsgStatus(Status1, "Falta completar el peso en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '            Util.MsgStatus(Status1, "Falta completar el peso en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '            Exit Sub
                '        End If

                '    Catch ex As Exception
                '        Util.MsgStatus(Status1, "El peso debe ser válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '        Util.MsgStatus(Status1, "El peso debe ser válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '        Exit Sub
                '    End Try
                'End If

                'Try
                '    'qty es válida?
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is System.DBNull.Value Then
                '        Util.MsgStatus(Status1, "Falta completar la cantidad a enviar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '        Util.MsgStatus(Status1, "Falta completar la cantidad a enviar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '        Exit Sub
                '    End If

                'Catch ex As Exception
                '    Util.MsgStatus(Status1, "La cantidad a enviar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '    Util.MsgStatus(Status1, "La cantidad a enviar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '    Exit Sub
                'End Try

                ''si tiene saldo, controlamos que no se pase..
                'If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value.ToString.Contains("CUMPLIDO") Then
                '    If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Is DBNull.Value Then
                '        If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                '            Util.MsgStatus(Status1, "La cantidad a enviar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '            Util.MsgStatus(Status1, "La cantidad a enviar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '            Exit Sub
                '        End If
                '    End If
                'End If

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
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value > 0 Then
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

            If lblNroTrans.Text = "" Then
                sqltxt2 = "exec spTransferencias_Porkys_Det_Select_By_IDTrans @IdTransferencia = '1'"
            Else
                sqltxt2 = "exec spTransferencias_Porkys_Det_Select_By_IDTrans @IdTransferencia = '" & lblNroTrans.Text & "'"
            End If

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), dt.Rows(i)(4).ToString(), dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(), dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(), dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString())
            Next

            CantidadTotal()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try



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

    Private Sub LlenarcmbOrigen()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT 0 AS 'Codigo', '' AS 'Nombre' Union SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE Eliminado = 0 ORDER BY Codigo")
            ds_Marcas.Dispose()

            With Me.cmbOrigen
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

    Private Sub LlenarcmbDestino()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT 0 AS 'Codigo', '' AS 'Nombre' Union SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE Eliminado = 0 ORDER BY Codigo")
            ds_Marcas.Dispose()

            With Me.cmbDestino
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
        cmbOrigen.Enabled = habilitar
        cmbDestino.Enabled = habilitar
        'txtNota.Enabled = habilitar
        lblNroTrans.Enabled = habilitar
        cmbEncargado.Enabled = habilitar
        cmbProducto.Enabled = habilitar
        txtCantidad.Enabled = habilitar
        PicEncargado.Enabled = habilitar
        btnActualizarMat.Enabled = habilitar

    End Sub

    Private Sub validar_NumerosReales2( _
      ByVal sender As Object, _
      ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridItems.Cantidad Then

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
                'grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value = 0.0
                'End If

                ''me fijo si el item no esta cumplido
                'If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.Status).Value.ToString = "CUMPLIDO" Then
                '    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Style.BackColor = Color.LightBlue
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
                '        Dim preciocalculado As Double
                '        preciocalculado = grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value / IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPaCK).Value = 0, 1, grdItems.Rows(i).Cells(ColumnasDelGridItems.CantidadPaCK).Value)
                '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = Math.Round(preciocalculado, 2)
                '        grdItems.Columns(ColumnasDelGridItems.Peso).Visible = True
                '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value = 0.0
                '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Style.BackColor = Color.LightBlue
                '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).ReadOnly = False
                '        grdItems.Columns(ColumnasDelGridItems.Peso).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
                '    Else
                '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).ReadOnly = True
                '    End If
                'Else
                '    'dejo la columna de qtyenviada en false
                '    grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).ReadOnly = True
                'End If

                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos para consultar el stock", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT QTY FROM STOCK WHERE IDMaterial = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "' AND IDAlmacen = " & IIf(txtIDOrigen.Text = "", cmbOrigen.SelectedValue, txtIDOrigen.Text) & "")
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

        Dim saldo As Double = 0
        rpt.Transferencias_Maestro_App(OrdenPedido, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)

        cerroparametrosconaceptar = False
        param = Nothing
        cnn = Nothing
        'End If
    End Sub

    'Private Sub OcultarItemsEliminados()
    'control para aquellos item que esten eliminados 
    '    If rdAnuladas.Checked = False Then
    '        For i As Integer = 0 To grdItems.RowCount - 1
    '            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value = True Then
    '                grdItems.CurrentCell = Nothing
    '                grdItems.Rows(i).Visible = False
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub llenarcmbEncargados()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT '00' AS 'Codigo',' ' As 'Encargado' Union SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Encargado' FROM Empleados WHERE Eliminado = 0 ORDER BY Encargado")
            ds.Dispose()

            With cmbEncargado
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Encargado"
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

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, Nombre as 'Producto' FROM Materiales  WHERE eliminado = 0 ")
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

    Private Sub CantidadTotal()
        Dim suma As Double
        For i As Integer = 0 To grdItems.Rows.Count - 1
            suma = suma + CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value)
        Next
        suma = FormatNumber(suma, 2)
        lblTotal.Text = suma.ToString
    End Sub


#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro_TransferenciaPorkys() As Integer
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
                    param_id.Direction = ParameterDirection.Input
                Else
                    param_id.Value = txtID.Text
                    param_id.Direction = ParameterDirection.Input
                End If

                Dim param_ordentrans As New SqlClient.SqlParameter
                param_ordentrans.ParameterName = "@OrdenTrans"
                param_ordentrans.SqlDbType = SqlDbType.VarChar
                param_ordentrans.Size = 25
                param_ordentrans.Value = DBNull.Value
                param_ordentrans.Direction = ParameterDirection.Output

                Dim param_idOrigen As New SqlClient.SqlParameter
                param_idOrigen.ParameterName = "@IDOrigen"
                param_idOrigen.SqlDbType = SqlDbType.BigInt
                param_idOrigen.Value = IIf(txtIDOrigen.Text = "", cmbOrigen.SelectedValue, txtIDOrigen.Text)
                param_idOrigen.Direction = ParameterDirection.Input

                Dim param_idDestino As New SqlClient.SqlParameter
                param_idDestino.ParameterName = "@IDDestino"
                param_idDestino.SqlDbType = SqlDbType.BigInt
                param_idDestino.Value = IIf(txtIDDestino.Text = "", cmbDestino.SelectedValue, txtIDDestino.Text)
                param_idDestino.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@FECHA"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value.ToShortDateString + " " + Now.ToString("HH:mm:ss")
                param_fecha.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input


                Dim param_idusuario As New SqlClient.SqlParameter
                param_idusuario.ParameterName = "@IDEmpleado"
                param_idusuario.SqlDbType = SqlDbType.VarChar
                param_idusuario.Size = 10
                param_idusuario.Value = UserID.ToString
                param_idusuario.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@IDEncargado"
                param_idcliente.SqlDbType = SqlDbType.VarChar
                param_idcliente.Size = 10
                param_idcliente.Value = IIf(txtIDEncargado.Text = "", cmbEncargado.SelectedValue, txtIDEncargado.Text)
                param_idcliente.Direction = ParameterDirection.Input


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


                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Porkys_Insert", _
                                    param_id, param_ordentrans, param_idOrigen, param_idDestino, param_idcliente, param_fecha, _
                                    param_nota, param_idusuario, param_useradd, param_res)

                    lblNroTrans.Text = param_ordentrans.Value
                    AgregarActualizar_Registro_TransferenciaPorkys = param_res.Value

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

    Private Function AgregarRegistro_TransferenciaPorkys_Items() As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False
        'Dim ds_Empresa As Data.DataSet
        Dim ValorActual As Double
        Dim IdStockMov_Orig As Long
        Dim Stock_Orig As Double

        Dim IdStockMov_Des As Long
        Dim Stock_Des As Double


        Dim Comprob As String

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count

                    ValorActual = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput

                    Dim param_idtrans As New SqlClient.SqlParameter
                    param_idtrans.ParameterName = "@IDTransferencia"
                    param_idtrans.SqlDbType = SqlDbType.VarChar
                    param_idtrans.Size = 25
                    param_idtrans.Value = lblNroTrans.Text 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
                    param_idtrans.Direction = ParameterDirection.Input

                    Dim param_idOrigen As New SqlClient.SqlParameter
                    param_idOrigen.ParameterName = "@IDOrigen"
                    param_idOrigen.SqlDbType = SqlDbType.BigInt
                    param_idOrigen.Value = IIf(txtIDOrigen.Text = "", cmbOrigen.SelectedValue, txtIDOrigen.Text)
                    param_idOrigen.Direction = ParameterDirection.Input

                    Dim param_idDestino As New SqlClient.SqlParameter
                    param_idDestino.ParameterName = "@IDDestino"
                    param_idDestino.SqlDbType = SqlDbType.BigInt
                    param_idDestino.Value = IIf(txtIDDestino.Text = "", cmbDestino.SelectedValue, txtIDDestino.Text)
                    param_idDestino.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@IDMaterial"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                    param_idmaterial.Direction = ParameterDirection.Input
                    'MsgBox(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString)

                    Dim param_marca As New SqlClient.SqlParameter
                    param_marca.ParameterName = "@IDMarca"
                    param_marca.SqlDbType = SqlDbType.VarChar
                    param_marca.Size = 25
                    param_marca.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value)
                    param_marca.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@IDUnidad"
                    param_idunidad.SqlDbType = SqlDbType.VarChar
                    param_idunidad.Size = 25
                    param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
                    param_idunidad.Direction = ParameterDirection.Input


                    Dim param_qtyenviada As New SqlClient.SqlParameter
                    param_qtyenviada.ParameterName = "@Cantidad"
                    param_qtyenviada.SqlDbType = SqlDbType.Decimal
                    param_qtyenviada.Precision = 18
                    param_qtyenviada.Scale = 2
                    param_qtyenviada.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal)
                    param_qtyenviada.Direction = ParameterDirection.Input

                    Dim param_notadet As New SqlClient.SqlParameter
                    param_notadet.ParameterName = "@Nota_Det"
                    param_notadet.SqlDbType = SqlDbType.VarChar
                    param_notadet.Size = 300
                    param_notadet.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
                    param_notadet.Direction = ParameterDirection.Input

                    Dim param_ordenitem As New SqlClient.SqlParameter
                    param_ordenitem.ParameterName = "@OrdenItem"
                    param_ordenitem.SqlDbType = SqlDbType.SmallInt
                    param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
                    param_ordenitem.Direction = ParameterDirection.Input


                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@useradd"
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    '---------------------------------------agregue--------------------------------------------'
                    Dim param_IdStockMov As New SqlClient.SqlParameter
                    param_IdStockMov.ParameterName = "@IdStockMov_Orig"
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
                    param_Stock.ParameterName = "@Stock_Orig"
                    param_Stock.SqlDbType = SqlDbType.Decimal
                    param_Stock.Precision = 18
                    param_Stock.Scale = 2
                    param_Stock.Value = DBNull.Value
                    param_Stock.Direction = ParameterDirection.InputOutput

                    Dim param_IdStockMov_Des As New SqlClient.SqlParameter
                    param_IdStockMov_Des.ParameterName = "@IdStockMov_Des"
                    param_IdStockMov_Des.SqlDbType = SqlDbType.Int
                    param_IdStockMov_Des.Value = DBNull.Value
                    param_IdStockMov_Des.Direction = ParameterDirection.InputOutput

                    Dim param_Stock_Des As New SqlClient.SqlParameter
                    param_Stock_Des.ParameterName = "@Stock_Des"
                    param_Stock_Des.SqlDbType = SqlDbType.Decimal
                    param_Stock_Des.Precision = 18
                    param_Stock_Des.Scale = 2
                    param_Stock_Des.Value = DBNull.Value
                    param_Stock_Des.Direction = ParameterDirection.InputOutput

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

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Porkys_Det_Insert", _
                                           param_id, param_idtrans, param_idOrigen, param_idDestino, param_idmaterial, _
                                           param_marca, param_idunidad, param_qtyenviada, param_notadet, param_useradd, param_ordenitem, _
                                           param_IdStockMov, param_Comprob, param_Stock, param_IdStockMov_Des, param_Stock_Des, param_res, param_msg)

                        'MsgBox(param_msg.Value.ToString)

                        res = param_res.Value
                        Comprob = param_Comprob.Value
                        Stock_Orig = param_Stock.Value
                        IdStockMov_Orig = param_IdStockMov.Value
                        Stock_Des = param_Stock_Des.Value
                        IdStockMov_Des = param_IdStockMov_Des.Value

                        If Not (param_msg.Value Is System.DBNull.Value) Then
                            msg = param_msg.Value
                        Else
                            msg = ""
                        End If
                        If (res <= 0) Then
                            Exit Do
                        End If

         

                        'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                        'If OrigDes_WEB() = True Then
                        '    Try

                        '        Dim sqlstringOr As String = "exec spStock_Insert '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "', '" & _
                        '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value & "', " & IIf(txtIDOrigen.Text = "", cmbOrigen.SelectedValue, txtIDOrigen.Text) & ", 'V', " & _
                        '        CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal) & ", " & Stock_Orig & ", " & IdStockMov_Orig & ", '" & Comprob & "',4, " & UserID

                        '        Dim sqlstringDes As String = "exec spStock_Insert '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "', '" & _
                        '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value & "', " & IIf(txtIDDestino.Text = "", cmbDestino.SelectedValue, txtIDDestino.Text) & ", 'I', " & _
                        '        CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal) & ", " & Stock_Des & ", " & IdStockMov_Des & ", '" & Comprob & "',4, " & UserID



                        '        If tranWEB.Sql_Get_Value(sqlstringOr) > 0 Then
                        '            If tranWEB.Sql_Get_Value(sqlstringDes) > 0 Then
                        '                ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov_Orig & " or id =" & IdStockMov_Des)
                        '                ds_Empresa.Dispose()

                        '                Dim sqlstring As String = "INSERT INTO [dbo].[transferencias_Recepciones_WEB] ([Codigo],[Fecha],[IDOrigen],[IDDestino],[IDMaterial], " & _
                        '                "[Qty],[Procesado],[Tipo])" & _
                        '                "  values ('" & lblNroTrans.Text & "', '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "'," & _
                        '                IIf(txtIDOrigen.Text = "", cmbOrigen.SelectedValue, txtIDOrigen.Text) & ", " & IIf(txtIDDestino.Text = "", cmbDestino.SelectedValue, txtIDDestino.Text) & ",'" & _
                        '                grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'," & CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal) & ",0,'T')"

                        '                tranWEB.Sql_Set(sqlstring)

                        '                sqlstring = "update notificacionesWEB set Transferencias = 1"
                        '                tranWEB.Sql_Set(sqlstring)

                        '            End If
                        '        End If



                        '    Catch ex As Exception

                        '    End Try
                        'End If
                        'End If

                    Catch ex As Exception
                        Throw ex
                    End Try



                    i = i + 1

                Loop

                AgregarRegistro_TransferenciaPorkys_Items = res

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

    Private Function EliminarRegistro_Transferencia() As Integer
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
                param_ordenpedido.Value = lblNroTrans.Text
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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Porkys_Delete", param_ordenpedido, param_nota, param_autorizado, param_userdel, param_res)
                    res = param_res.Value

                    'If Not (param_msg.Value Is System.DBNull.Value) Then
                    '    msg = param_msg.Value
                    'Else
                    '    msg = ""
                    'End If

                    EliminarRegistro_Transferencia = res

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

    Private Function EliminarRegistro_Transferencia_Items() As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False

        Dim ValorActual As Double
        Dim IdStockMov_Orig As Long
        Dim Stock_Orig As Double

        Dim IdStockMov_Des As Long
        Dim Stock_Des As Double

        Dim Comprob As String

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count

                    ValorActual = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput

                    Dim param_idtrans As New SqlClient.SqlParameter
                    param_idtrans.ParameterName = "@IDTransferencia"
                    param_idtrans.SqlDbType = SqlDbType.VarChar
                    param_idtrans.Size = 25
                    param_idtrans.Value = lblNroTrans.Text 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
                    param_idtrans.Direction = ParameterDirection.Input

                    Dim param_idOrigen As New SqlClient.SqlParameter
                    param_idOrigen.ParameterName = "@IDOrigen"
                    param_idOrigen.SqlDbType = SqlDbType.BigInt
                    param_idOrigen.Value = IIf(txtIDOrigen.Text = "", cmbOrigen.SelectedValue, txtIDOrigen.Text)
                    param_idOrigen.Direction = ParameterDirection.Input

                    Dim param_idDestino As New SqlClient.SqlParameter
                    param_idDestino.ParameterName = "@IDDestino"
                    param_idDestino.SqlDbType = SqlDbType.BigInt
                    param_idDestino.Value = IIf(txtIDDestino.Text = "", cmbDestino.SelectedValue, txtIDDestino.Text)
                    param_idDestino.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@IDMaterial"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                    param_idmaterial.Direction = ParameterDirection.Input
                    'MsgBox(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString)

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@IDUnidad"
                    param_idunidad.SqlDbType = SqlDbType.VarChar
                    param_idunidad.Size = 25
                    param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
                    param_idunidad.Direction = ParameterDirection.Input


                    Dim param_qtyenviada As New SqlClient.SqlParameter
                    param_qtyenviada.ParameterName = "@Cantidad"
                    param_qtyenviada.SqlDbType = SqlDbType.Decimal
                    param_qtyenviada.Precision = 18
                    param_qtyenviada.Scale = 2
                    param_qtyenviada.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal)
                    param_qtyenviada.Direction = ParameterDirection.Input

                    Dim param_notadet As New SqlClient.SqlParameter
                    param_notadet.ParameterName = "@Nota_Det"
                    param_notadet.SqlDbType = SqlDbType.VarChar
                    param_notadet.Size = 300
                    param_notadet.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
                    param_notadet.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@userdel"
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    '---------------------------------------agregue--------------------------------------------'
                    Dim param_IdStockMov As New SqlClient.SqlParameter
                    param_IdStockMov.ParameterName = "@IdStockMov_Orig"
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
                    param_Stock.ParameterName = "@Stock_Orig"
                    param_Stock.SqlDbType = SqlDbType.Decimal
                    param_Stock.Precision = 18
                    param_Stock.Scale = 2
                    param_Stock.Value = DBNull.Value
                    param_Stock.Direction = ParameterDirection.InputOutput

                    Dim param_IdStockMov_Des As New SqlClient.SqlParameter
                    param_IdStockMov_Des.ParameterName = "@IdStockMov_Des"
                    param_IdStockMov_Des.SqlDbType = SqlDbType.Int
                    param_IdStockMov_Des.Value = DBNull.Value
                    param_IdStockMov_Des.Direction = ParameterDirection.InputOutput

                    Dim param_Stock_Des As New SqlClient.SqlParameter
                    param_Stock_Des.ParameterName = "@Stock_Des"
                    param_Stock_Des.SqlDbType = SqlDbType.Decimal
                    param_Stock_Des.Precision = 18
                    param_Stock_Des.Scale = 2
                    param_Stock_Des.Value = DBNull.Value
                    param_Stock_Des.Direction = ParameterDirection.InputOutput

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

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Porkys_Delete_Det", _
                                           param_id, param_idtrans, param_idOrigen, param_idDestino, param_idmaterial, _
                                           param_idunidad, param_qtyenviada, param_notadet, param_useradd, _
                                           param_IdStockMov, param_Comprob, param_Stock, param_IdStockMov_Des, param_Stock_Des, param_res, param_msg)

                        'MsgBox(param_msg.Value.ToString)

                        res = param_res.Value
                        Comprob = param_Comprob.Value
                        Stock_Orig = param_Stock.Value
                        IdStockMov_Orig = param_IdStockMov.Value
                        Stock_Des = param_Stock_Des.Value
                        IdStockMov_Des = param_IdStockMov_Des.Value

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



                    i = i + 1

                Loop

                EliminarRegistro_Transferencia_Items = res

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
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value Is Nothing) _
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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDTrans_det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDTrans_det)
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
    '            param_idordendecompra.Value = lblNroTrans.Text
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

    '                        sqlstring = "exec spPedidosWEB_Delete_Finalizar '" & lblNroTrans.Text & "','" & txtNota.Text & "'," & UserID & ""

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

    Private Function OrigDes_WEB() As Boolean
        If cmbOrigen.Text.Contains("PRINCIPAL") And cmbDestino.Text.Contains("PERON") Then
            OrigDes_WEB = True
        ElseIf cmbOrigen.Text.Contains("PERON") And cmbDestino.Text.Contains("PRINCIPAL") Then
            OrigDes_WEB = True
        Else
            OrigDes_WEB = False
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

        lblNroTrans.Text = ""
        chkEliminados.Checked = False

        Util.LimpiarTextBox(Me.Controls)
        grdItems.Rows.Clear()


        lblTotal.Text = "0"
        cmbOrigen.SelectedIndex = Utiles.numero_almacen
        If MDIPrincipal.sucursal.Contains("PERON") Then
            cmbDestino.SelectedIndex = 1
        Else
            cmbDestino.SelectedIndex = 3
        End If

        band = 1


    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click


        If bolModo = False Then
            Exit Sub
        End If

        Dim res As Integer, res_item As Integer

        Dim orig As Boolean = OrigDes_WEB()
        If orig Then
            Try
                Dim resweb As Integer = tranWEB.Sql_Get_Value("SELECT count(*) FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'T' And IDDestino = " & numero_almacen)
                If resweb > 0 Then
                    MsgBox("Posee Transferencias pendientes. Por favor actualice(Transferencias) y luego continue con la operación.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Catch ex As Exception

            End Try
         
        End If


        If ReglasNegocio() Then
            If bolModo Then
                Verificar_Datos()
            Else
                bolpoliticas = True
            End If
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro_TransferenciaPorkys()
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
                        res_item = AgregarRegistro_TransferenciaPorkys_Items()
                        Select Case res_item
                            Case -5
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock de Origen (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock de Origen(items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case -6
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No hay stock de Origen suficiente (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No hay stock de Origen suficiente (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case -7
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock de Destino (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock de Destino (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case -8
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No hay stock de Destino suficiente (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No hay stock de Destino suficiente (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub

                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el Transferencia (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el Transferencia (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Exit Sub
                            Case Else
                                Cerrar_Tran()

                                Imprimir_Pedido(lblNroTrans.Text)

                                bolModo = False
                                PrepararBotones()

                                DesbloquearComponentes(bolModo)

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
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        ''If BAJA Then
        If chkEliminados.Checked = False Then
            'If MessageBox.Show("Esta acción cambiara el estado del registro." + vbCrLf + "¿Está seguro que desea anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If txtNota.Text.Trim = "" Then
                Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap, True)
                txtNota.Focus()
                Exit Sub
            End If

            Dim Au As New frmUsuarioModo
            Au.ShowDialog()

            If MDIPrincipal.Autorizar = False Then
                Exit Sub
            End If

            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro_Transferencia()
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
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap, True)
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap, True)
                Case Else
                    res = EliminarRegistro_Transferencia_Items()
                    Select Case res
                        Case -9
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error al no modificar stock de Origen.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error al no modificar stock de Origen.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -10
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error. No se puede sumar el stock de Origen.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error. No se puede sumar el stock de Origen.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -11
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error al no modificar stock de Destino.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error al no modificar stock de Destino.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -12
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error. No se puede restar el stock de Destino.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error. No se puede restar el stock de Destino.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case 0
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -1
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No pudo realizarse la anulación(ítems).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case Else
                            Cerrar_Tran()
                            PrepararBotones()

                            bolModo = False
                            PrepararBotones()


                            DesbloquearComponentes(bolModo)

                            SQL = "exec spTransferencias_Porkys_Select_All @Eliminado = 0 "

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

        Try

        Catch ex As Exception

        End Try

        nbreformreportes = "Ordenes de Compra"

        param.AgregarParametros("Código :", "STRING", "", False, lblNroTrans.Text.ToString, "", cnn)

        param.ShowDialog()



        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)
            Dim saldo As Double = 0
            'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)

            'If chkVentas.Checked = True Then
            rpt.Transferencias_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
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

        DesbloquearComponentes(False)
        grd_CurrentCellChanged(sender, e)
        bolModo = False

    End Sub

    Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click

        SQL = "exec spTransferencias_Porkys_Select_All @Eliminado = 0 "

        bolModo = False

        Try
            LlenarGrilla()

            'If grd.Rows.Count > 0 Then
            '    LlenarGrid_Items()
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click

        'Try
        '    Dim sqlstring As String = "update NotificacionesWEB set BloqueoT = 0"
        '    tranWEB.Sql_Set(sqlstring)


        'Catch ex As Exception

        'End Try

    End Sub


#End Region

#Region "   GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit

        editando_celda = False
        'Dim descuento As Double
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

            'If e.ColumnIndex = ColumnasDelGridItems.Descuento Then

            '    descuento = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value)

            '    If descuento = 0 Then
            '        If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
            '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value '* IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value)
            '        Else
            '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyEnviada).Value) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
            '        End If
            '    Else
            '        Dim calculo As Double = (descuento * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value) / 100
            '        calculo = Math.Round(calculo, 2)
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - calculo
            '    End If
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)

            '    If descuento = 100 Then
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value + "(BONIF.)"
            '        'coloco el bit de bonificacion en uno
            '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion).Value = True
            '    End If

            'End If

        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Cantidad Then
            AddHandler e.Control.KeyPress, AddressOf validar_NumerosReales2
        End If

    End Sub

    Private Sub grdItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdItems.KeyDown

        'Dim columna As Integer = 0
        'columna = grdItems.CurrentCell.ColumnIndex

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
        If e.ColumnIndex = 12 And bolModo = True Then 'Marcar llegada
            If MessageBox.Show("Está seguro que desea eliminar el producto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItems.Rows.RemoveAt(e.RowIndex)
                'me fijo si en la grilla hay algo y cambio el orden de los item
                If grdItems.Rows.Count > 0 Then
                    For i As Integer = 0 To grdItems.Rows.Count - 1
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value = i + 1
                    Next
                End If
                CantidadTotal()

                txtNota.Focus()
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

#End Region

    



End Class
