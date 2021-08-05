Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Imports System.Threading

Public Class frmDevolucionesProveedor

    Dim bolpoliticas As Boolean
    Dim llenandoCombo As Boolean = False
    Dim llenandoCombo2 As Boolean = False
    'Variables para la grilla
    Dim editando_celda As Boolean
    'Dim CodigoUsuario As String

    Dim permitir_evento_CellChanged As Boolean

    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Para el combo de busqueda
    'Dim ID_Buscado As Long
    'Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    '  Public UltBusqueda As String

    Dim band As Integer

    Dim trd As Thread

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número

    Dim btnBand_Copiar As Boolean = True

    Dim ds_Empresa As Data.DataSet


    Dim porceniva As Double = 21

    Enum ColumnasDelGridItems
        IDDevolucionProveedor_Det = 0
        Cod_DevolucionProveedorDet = 1
        Orden_Item = 2
        IDMaterial = 3
        Producto = 4
        Lote = 5
        Cantidad = 6
        IDUnidad = 7
        Unidad = 8
        LoteProveed = 9
        Remito = 10
        Status = 11
        ConCambio = 12
        Nota_Det = 13
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

#Region "   Eventos"

    Private Sub frmOrdenCompra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la Orden de Compra Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Orde de Compra Nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmOrdenCompra_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Items()
            End If
        End If
    End Sub

    Private Sub frmOrdenCompra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Cursor = Cursors.WaitCursor

        ToolStrip_lblCodMaterial.Visible = True
        txtBusquedaMAT.Visible = True

        band = 0

        configurarform()
        asignarTags()

        btnEliminar.Text = "Anular DP"

        rdPendientes.Checked = 1

        LlenarComboAlmacenes()
        LlenarComboEmpleados()
        LlenarcmbProveedores_App(cmbPROVEEDORES, ConnStringSEI, 0, 0)
        Me.LlenarCombo_Productos("")

        SQL = "exec spDevolucionesProveedor_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked



        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        permitir_evento_CellChanged = True

        band = 1

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
            lblCantidadFilas.Text = grdItems.Rows.Count
            txtID.Text = grd.CurrentRow.Cells(0).Value
            txtCODIGO.Text = grd.CurrentRow.Cells(1).Value
            dtpFECHA.Value = grd.CurrentRow.Cells(2).Value
            cmbAlmacenes.SelectedValue = grd.CurrentRow.Cells(3).Value
            cmbPROVEEDORES.Text = grd.CurrentRow.Cells(5).Value
            cmbEmpleado.SelectedValue = grd.CurrentRow.Cells(6).Value
            txtNota.Text = grd.CurrentRow.Cells(7).Value
            lblStatus.Text = grd.CurrentRow.Cells(8).Value
        End If

        If bolModo = True Then
            LlenarGrid_Items()
            btnNuevo_Click(sender, e)
        Else
            MostrarColumnasyCampos()
            LlenarGrid_Items()
        End If


        grd.Columns(0).Visible = False
        grd.Columns(3).Visible = False
        grd.Columns(9).Visible = False

        'dtpFECHA.MaxDate = Today.Date
        'cmbProducto.Enabled = False
        Cursor = Cursors.Default

    End Sub

    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter Then
            txtCantidad.Focus()
            'SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged

        If band = 1 Then


            Dim sqlstring As String
            sqlstring = "SELECT PrecioCompra, m.IdUnidad, s.qty FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo where m.Codigo = " & cmbProducto.SelectedValue & "and s.IDAlmacen = " & cmbAlmacenes.SelectedValue
            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Empresa.Dispose()

            'txtPrecioUni.Text = FormatNumber(CDbl(ds_Empresa.Tables(0).Rows(0)(0)), 2)
            txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(1)
            lblStock.Text = ds_Empresa.Tables(0).Rows(0)(2)


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

            If txtCantidad.Text = "" Or txtCantidad.Text = "0" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbProducto.Text = grdItems.Rows(i).Cells(3).Value Then
                    Util.MsgStatus(Status1, "El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (i + 1).ToString & ".", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            Next

            grdItems.Rows.Add(0, "", i + 1, cmbProducto.SelectedValue, cmbProducto.Text, 0, txtCantidad.Text, txtIdUnidad.Text, "", "", "", "PENDIENTE", 0, "", "ELIMINAR")

            Contar_Filas()

            txtCantidad.Text = ""
            lblStock.Text = ""
            cmbProducto.Text = ""
            cmbProducto.Focus()

        End If

    End Sub

    Private Sub txtPeso_KeyDown(sender As Object, e As KeyEventArgs)
        If txtIdUnidad.Text = "HORMA" Or txtIdUnidad.Text = "TIRA" Then
            txtCantidad_KeyDown(sender, e)
        End If
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If Not bolModo Then
            cmbPROVEEDORES.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

    Private Sub PicProveedores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicProveedores.Click
        Dim f As New frmProveedores
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbPROVEEDORES.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbProveedores_App(cmbPROVEEDORES, ConnStringSEI, 0, 0)
        cmbPROVEEDORES.Text = texto_del_combo
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

    Private Sub BuscarDescripcionToolStripMenuItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem2.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem2_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem3.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem3_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem.SelectedIndexChanged

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(columna)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.ComboBox.SelectedValue

        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)

    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem2.SelectedIndexChanged

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(columna)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem2.ComboBox.Text

        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)

    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem3.SelectedIndexChanged

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(columna)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem3.ComboBox.Text

        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)

    End Sub

    Private Sub cmbPROVEEDORES_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPROVEEDORES.SelectedIndexChanged
        If band = 1 Then
            Try

                txtIdProveedor.Text = cmbPROVEEDORES.SelectedValue.ToString

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    '(currentcellchanged)
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            'band = 0
            Try
                Select Case grd.CurrentRow.Cells(8).Value
                    Case "PENDIENTE"
                        btnEntregar.Enabled = True
                    Case Else
                        btnEntregar.Enabled = False
                End Select

                lblCantidadFilas.Text = grdItems.Rows.Count
                txtID.Text = grd.CurrentRow.Cells(0).Value
                txtCODIGO.Text = grd.CurrentRow.Cells(1).Value
                dtpFECHA.Value = grd.CurrentRow.Cells(2).Value
                cmbAlmacenes.SelectedValue = grd.CurrentRow.Cells(3).Value
                cmbPROVEEDORES.Text = grd.CurrentRow.Cells(5).Value
                cmbEmpleado.SelectedValue = grd.CurrentRow.Cells(6).Value
                txtNota.Text = grd.CurrentRow.Cells(7).Value
                lblStatus.Text = grd.CurrentRow.Cells(8).Value


                LlenarGrid_Items()

            Catch ex As Exception
                'band = 1
            End Try
        End If
    End Sub

    Private Sub rdAnuladas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdAnuladas.CheckedChanged
        btnGuardar.Enabled = Not rdAnuladas.Checked
        btnEliminar.Enabled = Not rdAnuladas.Checked
        btnNuevo.Enabled = Not rdAnuladas.Checked
        btnCancelar.Enabled = Not rdAnuladas.Checked

        SQL = "exec spDevolucionesProveedor_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked


        Try
            LlenarGrilla()

            If grd.Rows.Count > 0 Then
                ' grdItems.Columns(16).Visible = False
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub rdPendientes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdPendientes.CheckedChanged

        If band = 1 Then
            SQL = "exec spDevolucionesProveedor_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked


            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then

                    grd.Rows(0).Selected = True
                    txtID.Text = grd.Rows(0).Cells(0).Value
                    LlenarGrid_Items()
                    'grdItems.Columns(16).Visible = True
                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub rdTodasOC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdTodas.CheckedChanged

        If band = 1 Then
            SQL = "exec spDevolucionesProveedor_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked


            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then
                    ' grdItems.Columns(16).Visible = True
                    LlenarGrid_Items()
                End If
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
            lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y - variableajuste)

            rdPendientes.Location = New Point(rdPendientes.Location.X, rdPendientes.Location.Y - variableajuste)
            rdAnuladas.Location = New Point(rdAnuladas.Location.X, rdAnuladas.Location.Y - variableajuste)
            rdTodas.Location = New Point(rdTodas.Location.X, rdTodas.Location.Y - variableajuste)


            Label18.Location = New Point(Label18.Location.X, Label18.Location.Y - variableajuste)
            txtTotal.Location = New Point(txtTotal.Location.X, txtTotal.Location.Y - variableajuste)

            Label20.Location = New Point(Label20.Location.X, Label20.Location.Y - variableajuste)
            txtMontoIVA.Location = New Point(txtMontoIVA.Location.X, txtMontoIVA.Location.Y - variableajuste)

        Else
            chkGrillaInferior.Text = "Aumentar Grilla Inferior"
            chkGrillaInferior.Location = New Point(chkGrillaInferior.Location.X, chkGrillaInferior.Location.Y + variableajuste)
            GroupBox1.Height = GroupBox1.Height + variableajuste
            grd.Location = New Point(xgrd, ygrd + variableajuste)
            grd.Height = hgrd - variableajuste
            grdItems.Height = grdItems.Height + variableajuste
            Label19.Location = New Point(Label19.Location.X, Label19.Location.Y + variableajuste)
            lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y + variableajuste)

            rdPendientes.Location = New Point(rdPendientes.Location.X, rdPendientes.Location.Y + variableajuste)
            rdAnuladas.Location = New Point(rdAnuladas.Location.X, rdAnuladas.Location.Y + variableajuste)
            rdTodas.Location = New Point(rdTodas.Location.X, rdTodas.Location.Y + variableajuste)


            Label18.Location = New Point(Label18.Location.X, Label18.Location.Y + variableajuste)
            txtTotal.Location = New Point(txtTotal.Location.X, txtTotal.Location.Y + variableajuste)

            Label20.Location = New Point(Label20.Location.X, Label20.Location.Y + variableajuste)
            txtMontoIVA.Location = New Point(txtMontoIVA.Location.X, txtMontoIVA.Location.Y + variableajuste)

        End If

    End Sub

    Private Sub PicFormaPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picEmpleados.Click
        Dim f As New frmCondiciondePago

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbEmpleado.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarComboEmpleados()
        cmbEmpleado.Text = texto_del_combo
    End Sub

    Private Sub grdItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellContentClick
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        'Dim res As Integer

        If e.ColumnIndex = 14 Then
            Try
                If bolModo Then
                    grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente
                    Contar_Filas()
                Else
                    'If bolModo = False And grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMaterial).Value Is DBNull.Value Then
                    ' grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente
                    'Contar_Filas()
                    'Else
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Status).Value.ToString = "CUMPLIDO" Then
                        Util.MsgStatus(Status1, "No se puede borrar el registro. Ya está Cumplido.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se puede borrar el registro. Ya está Cumplido.", My.Resources.stop_error.ToBitmap, True)
                    Else
                        'If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value <> IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Saldo).Value Is DBNull.Value, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Saldo).Value) Then
                        '    Util.MsgStatus(Status1, "No se puede borrar el registro. Ya tiene Recepciones realizadas.", My.Resources.stop_error.ToBitmap)
                        '    Util.MsgStatus(Status1, "No se puede borrar el registro. Ya tiene Recepciones realizadas.", My.Resources.stop_error.ToBitmap, True)
                        'Else
                        'If MessageBox.Show("Esta acción Eliminará el item de forma permanente." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '    Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)

                        '    Dim item As Long = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det).Value

                        '    grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente de la grilla..
                        '    Contar_Filas()

                        '    res = EliminarRegistroItem(CType(txtID.Text, Long), item)

                        '    Select Case res
                        '        Case -1
                        '            Util.MsgStatus(Status1, "No se pudo borrar el Item.", My.Resources.stop_error.ToBitmap)
                        '            Util.MsgStatus(Status1, "No se pudo borrar el Item.", My.Resources.stop_error.ToBitmap, True)
                        '        Case Else

                        '            rdPendientes.Checked = 1
                        '            SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked

                        '            bolModo = False
                        '            PrepararBotones()
                        '            MDIPrincipal.NoActualizarBase = False
                        '            btnActualizar_Click(sender, e)
                        '            'Setear_Grilla()
                        '            Util.MsgStatus(Status1, "Se ha borrado el Item.", My.Resources.ok.ToBitmap)
                        '            Util.MsgStatus(Status1, "Se ha borrado el Item.", My.Resources.ok.ToBitmap, True, True)
                        '    End Select
                        'Else
                        '    Util.MsgStatus(Status1, "Acción de eliminar Item cancelada.", My.Resources.stop_error.ToBitmap)
                        '    Util.MsgStatus(Status1, "Acción de eliminar Item cancelada.", My.Resources.stop_error.ToBitmap, True)
                        'End If
                        'End If
                    End If
                End If
                'End If
                cmbProducto.Focus()
            Catch ex As Exception

            End Try
        End If


    End Sub

    Private Sub chkMaterialesProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles chkMaterialesProveedor.CheckedChanged

        If chkMaterialesProveedor.Checked Then
            If cmbPROVEEDORES.Text <> "" Then
                LlenarCombo_Productos(cmbPROVEEDORES.SelectedValue.ToString)
            Else
                LlenarCombo_Productos("")
            End If
        Else
            LlenarCombo_Productos("")
        End If
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Emisión de Devoluciones a Proveedores"

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

        'Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 50)
        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbAlmacenes.Tag = "3"
        cmbPROVEEDORES.Tag = "4"
        cmbEmpleado.Tag = "5"
        txtNota.Tag = "6"
        lblStatus.Tag = "7"
        chkEliminado.Tag = "8"

    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', res As Integer ', state As Integer

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        'Verificar si todos los combox tienen algo válido
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not (cmbPROVEEDORES.SelectedIndex > -1) Then
            Util.MsgStatus(Status1, "Ingrese un valor válido en el campo 'Proveedor'.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Ingrese un valor válido en el campo 'Proveedor'.", My.Resources.Resources.alert.ToBitmap, True)
            cmbPROVEEDORES.Focus()
            Exit Sub
        End If

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
            filas = filas + 1

        Next i
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

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
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

            If txtID.Text = "" Then
                SQL = "exec spDevolucionesProveedor_Det_Select_By_IDDevolucion @IDDevolucion = 1"
            Else
                SQL = "exec spDevolucionesProveedor_Det_Select_By_IDDevolucion @IDDevolucion = " & txtID.Text
            End If

            Dim cmd As New SqlCommand(SQL, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(),
                                  dt.Rows(i)(4).ToString(), dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(),
                                  dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(), dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(),
                                  dt.Rows(i)(12).ToString())
            Next

            'grdItems.Columns(ColumnasDelGridItems.Cantidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        'GetDatasetItems()

        'CheckForIllegalCrossThreadCalls = False

        'trd = New Thread(AddressOf HiloOcultarColumnasGridItems)
        'trd.IsBackground = True
        'trd.Start()

        'HiloOcultarColumnasGridItems()

        'SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

    End Sub

    Private Sub GetDatasetItems()
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
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
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub NoValidar(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim caracter As Char = e.KeyChar
        Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = sender
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString)
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

    Private Sub validarNumeros(ByVal sender As Object, _
       ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Controlar que el caracter ingresado sea un  numero
        Dim caracter As Char = e.KeyChar
        If caracter = "." Or caracter = "," Then
            e.Handled = True ' no dejar escribir
        Else
            If Char.IsNumber(caracter) Or caracter = ChrW(Keys.Back) Or caracter = ChrW(Keys.Delete) Or caracter = ChrW(Keys.Enter) Then
                e.Handled = False 'dejo escribir
            Else
                e.Handled = True ' no dejar escribir
            End If
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

    Private Sub Imprimir(ByVal Presupuesto As Boolean)
        nbreformreportes = "Orden de Compra / Presupuesto"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        If Presupuesto Then
            Rpt.NombreArchivoPDF = "Solicitud de Cotización " & txtCODIGO.Text & " - " & cmbPROVEEDORES.Text.ToString
        Else
            Rpt.NombreArchivoPDF = "Orden de Compra " & txtCODIGO.Text & " - " & cmbPROVEEDORES.Text.ToString
        End If

        Rpt.OrdenesDeCompra_Maestro_App(txtCODIGO.Text, 0, Rpt, My.Application.Info.AssemblyName.ToString, Presupuesto)

        cnn = Nothing

    End Sub

    Private Sub Contar_Filas()

        'Dim i As Integer, j As Integer = 0

        'txtSubtotal.Text = 0
        'txtMontoIVA.Text = 0
        'txtTotal.Text = 0

        'For i = 0 To grdItems.Rows.Count - 1 '16
        '    Try
        '        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Producto).Value <> Nothing Then
        '            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value <> Nothing Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value > 0 Then
        '                txtSubtotal.Text = Format(CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value), "####0.00")
        '                'txtSubtotal.Text = FormatNumber(CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value), 2)
        '                'txtMontoIVA.Text = CDbl(txtMontoIVA.Text) + grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIVA).Value
        '                'txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtMontoIVA.Text)
        '                j = j + 1
        '            End If
        '        End If
        '    Catch ex As Exception

        '    End Try
        'Next

        lblCantidadFilas.Text = grdItems.Rows.Count '- 1  'j.ToString + " / 16"

        'If j = 16 Then
        '    grdItems.AllowUserToAddRows = False
        'Else
        '    grdItems.AllowUserToAddRows = True
        'End If


    End Sub

    Private Sub HiloOcultarColumnasGridItems()
        Try
            grdItems.Columns(ColumnasDelGridItems.IDDevolucionProveedor_Det).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.IDDevolucionProveedor_Det).Width = 70
            grdItems.Columns(ColumnasDelGridItems.IDDevolucionProveedor_Det).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.Cod_OrdenDeCompra_Det).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.Cod_OrdenDeCompra_Det).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.Cod_OrdenDeCompra_Det).Visible = False

            grdItems.Columns(ColumnasDelGridItems.IdMaterial).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.Cod_material).Width = 90

            grdItems.Columns(ColumnasDelGridItems.Producto).Width = 260

            'grdItems.Columns(ColumnasDelGridItems.QtyStockTotal).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.QtyStockTotal).Width = 50
            'grdItems.Columns(ColumnasDelGridItems.QtyStockTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Precio).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.PrecioLista).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.PrecioLista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Iva).Width = 40
            'grdItems.Columns(ColumnasDelGridItems.Iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.MontoIVA).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.MontoIVA).Width = 60
            'grdItems.Columns(ColumnasDelGridItems.MontoIVA).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif1).Width = 45
            'grdItems.Columns(ColumnasDelGridItems.Bonif1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif2).Width = 45
            'grdItems.Columns(ColumnasDelGridItems.Bonif2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif3).Width = 50
            'grdItems.Columns(ColumnasDelGridItems.Bonif3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif4).Width = 50
            'grdItems.Columns(ColumnasDelGridItems.Bonif4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif5).Width = 50
            'grdItems.Columns(ColumnasDelGridItems.Bonif5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).Width = 65
            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.PrecioUni).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.PrecioUni).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.PrecioUni).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            'grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Cantidad).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Cantidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Subtotal).ReadOnly = True 'subtotal
            'grdItems.Columns(ColumnasDelGridItems.Subtotal).Width = 80
            'grdItems.Columns(ColumnasDelGridItems.Subtotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Status).Width = 70
            grdItems.Columns(ColumnasDelGridItems.Status).Visible = Not bolModo
            grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'grdItems.Columns(ColumnasDelGridItems.orden).Visible = False

            With grdItems
                .VirtualMode = False
                .AllowUserToAddRows = True
                .AllowUserToDeleteRows = True
                .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
                .RowsDefaultCellStyle.BackColor = Color.White
                .AllowUserToOrderColumns = False
                .SelectionMode = DataGridViewSelectionMode.CellSelect
                .ForeColor = Color.Black
            End With

            With grdItems.ColumnHeadersDefaultCellStyle
                .BackColor = Color.Black  'Color.BlueViolet
                .ForeColor = Color.White
                .Font = New Font("TAHOMA", 8, FontStyle.Bold)
            End With

            grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)

            'Dim i As Integer

            'For i = 0 To grdItems.Rows.Count - 2
            '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Ubicacion).Value.ToString.Length > 6 Then
            '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_material).Style.BackColor = Color.LightGreen
            '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
            '    End If
            'Next

            ' Contar_Filas()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LlenarCombo_Productos(ByVal idProveedor As String)
        Dim ds_Equipos As Data.DataSet
        Dim sqlstring As String
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If idProveedor = "" Then
                sqlstring = "SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales m JOIN Marcas ma ON m.idmarca = ma.Codigo WHERE m.nombre not like '%**FR%' and m.eliminado = 0"
            Else
                sqlstring = "SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales_Proveedor mp JOIN Materiales M ON M.Codigo = MP.IdMaterial JOIN Marcas ma ON mp.idmarca = ma.Codigo WHERE  mp.eliminado = 0 and mp.IdProveedor = '" & idProveedor & "'"
            End If
            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
            If ds_Equipos.Tables(0).Rows.Count = 0 And idProveedor <> "" Then
                sqlstring = "SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales m JOIN Marcas ma ON m.idmarca = ma.Codigo WHERE m.nombre not like '%**FR%' and m.eliminado = 0"
                ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
                Util.MsgStatus(Status1, "No hay productos asociados a este proveedor.", My.Resources.Resources.stop_error.ToBitmap)
            End If
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

    Private Sub LlenarComboAlmacenes()
        Dim ds_Almacenes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ds_Almacenes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, Nombre FROM Almacenes WHERE Eliminado = 0 ORDER BY codigo")
            ds_Almacenes.Dispose()

            With Me.cmbALMACENES
                .DataSource = ds_Almacenes.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Codigo"
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
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub LlenarComboEmpleados()
        Dim ds_Retira As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                'connection = SqlHelper.GetConnection(conexion)
                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ds_Retira = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, concat(apellido,' ',nombre) as Empleado FROM Empleados WHERE Eliminado = 0 ORDER BY codigo")
            ds_Retira.Dispose()

            With Me.cmbEmpleado
                .DataSource = ds_Retira.Tables(0).DefaultView
                .DisplayMember = "Empleado"
                .ValueMember = "codigo"
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

    Private Sub MostrarColumnasyCampos()
        Try
            dtpFECHA.Enabled = bolModo
            cmbPROVEEDORES.Enabled = bolModo
            cmbEmpleado.Enabled = bolModo
            cmbProducto.Enabled = bolModo
            txtNota.ReadOnly = Not bolModo
            txtCantidad.ReadOnly = Not bolModo
            picEmpleados.Enabled = bolModo
            PicProveedores.Enabled = bolModo


            grdItems.Columns(ColumnasDelGridItems.Status).Visible = Not bolModo
            grdItems.Columns(ColumnasDelGridItems.Cod_DevolucionProveedorDet).Visible = Not bolModo
            grdItems.Columns(ColumnasDelGridItems.Orden_Item).Visible = bolModo
            grdItems.Columns(ColumnasDelGridItems.ConCambio).ReadOnly = Not bolModo
            grdItems.Columns(ColumnasDelGridItems.Nota_Det).ReadOnly = Not bolModo
            'muestro el boton de eliminar solo para una nueva DP
            grdItems.Columns(14).Visible = bolModo
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Imprimir_Pedido(ByVal codigo As String)
        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        'Dim codigo As String
        Dim Solicitud As Boolean

        rpt.DevolucionesProveedor_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)

        cerroparametrosconaceptar = False
        param = Nothing
        cnn = Nothing
        'End If
    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarRegistro() As Integer
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
                param_id.Direction = ParameterDirection.InputOutput

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

                Dim param_idalmacen As New SqlClient.SqlParameter
                param_idalmacen.ParameterName = "@idalmacen"
                param_idalmacen.SqlDbType = SqlDbType.BigInt
                param_idalmacen.Value = cmbAlmacenes.SelectedValue
                param_idalmacen.Direction = ParameterDirection.Input

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.VarChar
                param_idproveedor.Size = 25
                param_idproveedor.Value = cmbPROVEEDORES.SelectedValue
                param_idproveedor.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_userdevolvio As New SqlClient.SqlParameter
                param_userdevolvio.ParameterName = "@userdevolvio"
                param_userdevolvio.SqlDbType = SqlDbType.BigInt
                param_userdevolvio.Value = cmbEmpleado.SelectedValue 'UserID
                param_userdevolvio.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@useradd"
                param_useradd.SqlDbType = SqlDbType.BigInt
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevolucionesProveedor_Insert", param_id, param_codigo, param_fecha, param_idproveedor,
                                              param_nota, param_idalmacen, param_userdevolvio, param_useradd, param_res)
                    txtID.Text = param_id.Value
                    txtCODIGO.Text = param_codigo.Value
                    res = param_res.Value

                    'If res = 1 Then Util.AgregarGrilla(grd, Me, Permitir)

                    AgregarRegistro = res

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

        End Try
    End Function

    Private Function AgregarRegistroItems(ByVal idDevolucionProveedor As Long) As Integer
        ' Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim IdStockMov As Long
        Dim Stock As Double
        Dim Comprob As String
        Try
            Try
                i = 0
                Do While i < grdItems.Rows.Count

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput

                    Dim param_codigo As New SqlClient.SqlParameter
                    param_codigo.ParameterName = "@codigo"
                    param_codigo.SqlDbType = SqlDbType.VarChar
                    param_codigo.Size = 25
                    param_codigo.Value = txtCODIGO.Text
                    param_codigo.Direction = ParameterDirection.Input

                    Dim param_iddevolucionproveedor As New SqlClient.SqlParameter
                    param_iddevolucionproveedor.ParameterName = "@idDevolucion"
                    param_iddevolucionproveedor.SqlDbType = SqlDbType.BigInt
                    param_iddevolucionproveedor.Value = idDevolucionProveedor
                    param_iddevolucionproveedor.Direction = ParameterDirection.Input

                    Dim param_ordenitem As New SqlClient.SqlParameter
                    param_ordenitem.ParameterName = "@ORDENITEM"
                    param_ordenitem.SqlDbType = SqlDbType.SmallInt
                    param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
                    param_ordenitem.Direction = ParameterDirection.Input

                    Dim param_idproveedor As New SqlClient.SqlParameter
                    param_idproveedor.ParameterName = "@idproveedor"
                    param_idproveedor.SqlDbType = SqlDbType.VarChar
                    param_idproveedor.Size = 25
                    param_idproveedor.Value = cmbPROVEEDORES.SelectedValue
                    param_idproveedor.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                    param_idmaterial.Direction = ParameterDirection.Input

                    cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Producto).Value

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 2
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.VarChar
                    param_idunidad.Size = 25
                    param_idunidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value
                    param_idunidad.Direction = ParameterDirection.Input

                    Dim param_idalmacen As New SqlClient.SqlParameter
                    param_idalmacen.ParameterName = "@idalmacen"
                    param_idalmacen.SqlDbType = SqlDbType.BigInt
                    param_idalmacen.Value = cmbAlmacenes.SelectedValue
                    param_idalmacen.Direction = ParameterDirection.Input

                    Dim param_Lote As New SqlClient.SqlParameter
                    param_Lote.ParameterName = "@lote"
                    param_Lote.SqlDbType = SqlDbType.BigInt
                    'If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                    '    param_Lote.Value = DBNull.Value
                    'Else
                    param_Lote.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Lote).Value, Long)
                    'End If
                    param_Lote.Direction = ParameterDirection.Input

                    'Dim param_manejalote As New SqlClient.SqlParameter
                    'param_manejalote.ParameterName = "@manejalote"
                    'param_manejalote.SqlDbType = SqlDbType.Bit
                    'param_manejalote.Value = 0 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value, Long)
                    'param_manejalote.Direction = ParameterDirection.Input

                    Dim param_loteprov As New SqlClient.SqlParameter
                    param_loteprov.ParameterName = "@loteproveed"
                    param_loteprov.SqlDbType = SqlDbType.VarChar
                    param_loteprov.Size = 25
                    If IsDBNull(grdItems.Rows(i).Cells(ColumnasDelGridItems.LoteProveed).Value) Then
                        param_loteprov.Value = ""
                    Else
                        param_loteprov.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.LoteProveed).Value
                    End If
                    param_loteprov.Direction = ParameterDirection.Input

                    Dim param_remito As New SqlClient.SqlParameter
                    param_remito.ParameterName = "@remito"
                    param_remito.SqlDbType = SqlDbType.VarChar
                    param_remito.Size = 25
                    If IsDBNull(grdItems.Rows(i).Cells(ColumnasDelGridItems.Remito).Value) Then
                        param_remito.Value = ""
                    Else
                        param_remito.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Remito).Value
                    End If
                    param_remito.Direction = ParameterDirection.Input

                    Dim param_concambio As New SqlClient.SqlParameter
                    param_concambio.ParameterName = "@ConCambio"
                    param_concambio.SqlDbType = SqlDbType.Bit
                    param_concambio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.ConCambio).Value = True, 1, 0)
                    param_concambio.Direction = ParameterDirection.Input

                    Dim param_notadet As New SqlClient.SqlParameter
                    param_notadet.ParameterName = "@Nota_Det"
                    param_notadet.SqlDbType = SqlDbType.VarChar
                    param_notadet.Size = 300
                    param_notadet.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
                    param_notadet.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@useradd"
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

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

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevolucionesProveedor_Det_Insert", param_id, param_codigo, _
                                                param_iddevolucionproveedor, param_idmaterial, param_qty, param_idunidad, param_Lote, param_concambio, _
                                                param_loteprov, param_idalmacen, param_remito, param_idproveedor, param_ordenitem, param_notadet, _
                                                param_Comprob, param_Stock, param_IdStockMov, param_useradd, param_res, param_msg)
                        res = param_res.Value
                        msg = param_msg.Value
                        Stock = param_Stock.Value
                        IdStockMov = param_IdStockMov.Value
                        Comprob = param_Comprob.Value

                        If (res <= 0) Then
                            Exit Do
                        End If

                        If cmbAlmacenes.Text.Contains("PRINCIPAL") Or cmbAlmacenes.Text.Contains("PERON") Then
                            If MDIPrincipal.InsertarMovWEB(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value,
                                                          grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value,
                                                          cmbAlmacenes.SelectedValue, "V", CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal),
                                                          CType(Stock, Decimal), IdStockMov, Comprob, 4, UserID) Then

                                ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                                ds_Empresa.Dispose()
                            End If
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                    i = i + 1
                Loop

                AgregarRegistroItems = res

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

    Private Function EliminarRegistro_DevolucionProveedor() As Integer
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


                 Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

                'Dim param_autorizado As New SqlClient.SqlParameter
                'param_autorizado.ParameterName = "@IDAutoriza"
                'param_autorizado.SqlDbType = SqlDbType.VarChar
                'param_autorizado.Size = 25
                'param_autorizado.Value = MDIPrincipal.IDEmpleadoAutoriza
                'param_autorizado.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevolucionesProveedor_Delete", param_id, param_nota, param_userdel, param_res)
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

                    EliminarRegistro_DevolucionProveedor = res

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

    Private Function EliminarRegistro_DevolucionProveedor_Items(ByVal idDevolucionProveedor As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False
        Dim ds_Empresa As Data.DataSet
        Dim IdStockMov As Long
        Dim Stock As Double


        Dim Comprob As String

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput

                    Dim param_iddevolucionproveedor As New SqlClient.SqlParameter
                    param_iddevolucionproveedor.ParameterName = "@idDevolucion"
                    param_iddevolucionproveedor.SqlDbType = SqlDbType.BigInt
                    param_iddevolucionproveedor.Value = idDevolucionProveedor
                    param_iddevolucionproveedor.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                    param_idmaterial.Direction = ParameterDirection.Input

                    cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Producto).Value

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 2
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.VarChar
                    param_idunidad.Size = 25
                    param_idunidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value
                    param_idunidad.Direction = ParameterDirection.Input

                    Dim param_idalmacen As New SqlClient.SqlParameter
                    param_idalmacen.ParameterName = "@idalmacen"
                    param_idalmacen.SqlDbType = SqlDbType.BigInt
                    param_idalmacen.Value = cmbAlmacenes.SelectedValue
                    param_idalmacen.Direction = ParameterDirection.Input


                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@userdel"
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

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

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevolucionesProveedor_Delete_Det", param_id, _
                                                param_iddevolucionproveedor, param_idmaterial, param_qty, param_idunidad, _
                                                param_idalmacen, param_Comprob, param_Stock, param_IdStockMov, param_useradd, param_res, param_msg)
                        res = param_res.Value
                        msg = param_msg.Value
                        Stock = param_Stock.Value
                        IdStockMov = param_IdStockMov.Value
                        Comprob = param_Comprob.Value

                        If (res <= 0) Then
                            Exit Do
                        End If

                        If cmbAlmacenes.Text.Contains("PRINCIPAL") Or cmbAlmacenes.Text.Contains("PERON") Then
                            If MDIPrincipal.InsertarMovWEB(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value,
                                                          grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value,
                                                          cmbAlmacenes.SelectedValue, "I", CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal),
                                                          CType(Stock, Decimal), IdStockMov, Comprob, 4, UserID) Then

                                ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                                ds_Empresa.Dispose()
                            End If
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                    i = i + 1
                Loop

                EliminarRegistro_DevolucionProveedor_Items = res

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

    Private Function CuentaRecepcionesPorOrdenDeCompra(ByVal IDoc As String) As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Try

                Dim param_idoc As New SqlClient.SqlParameter
                param_idoc.ParameterName = "@idoc"
                param_idoc.SqlDbType = SqlDbType.BigInt
                param_idoc.Value = IDoc
                param_idoc.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCuentaRecepcionesPorOrdenDeCompra", param_idoc, param_res)
                    res = param_res.Value

                    CuentaRecepcionesPorOrdenDeCompra = res

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

    Private Function EntregarRegistro() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            Try

                Dim param_idordendecompra As New SqlClient.SqlParameter("@iddevolucionproveedor", SqlDbType.BigInt, ParameterDirection.Input)
                param_idordendecompra.Value = CType(txtID.Text, Long)
                param_idordendecompra.Direction = ParameterDirection.Input

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

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spDevolucionesProveedor_Entregar_All", param_idordendecompra, _
                                              param_userdel, param_res)
                    res = param_res.Value

                    EntregarRegistro = res

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDDevolucionProveedor_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDDevolucionProveedor_Det)
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

        Try
            'If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Qty Then
            '    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_material, grdItems.CurrentRow.Index + 1)
            '    Return True
            'End If

            'If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Cod_material Then
            '    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Qty, grdItems.CurrentRow.Index)
            '    Return True
            'End If

        Catch ex As Exception

        End Try

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click


        If MessageBox.Show("Desea generar una nueva Devolución a proveedor?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkEliminado.Checked = False
        cmbPROVEEDORES.Enabled = True

        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        txtNota.Enabled = True

        MostrarColumnasyCampos()

        If btnBand_Copiar = True Then
            Util.LimpiarTextBox(Me.Controls)
        End If

        PrepararGridItems()

        lblCantidadFilas.Text = "0"
        cmbAlmacenes.SelectedIndex = 0
        'cmbAutoriza.SelectedValue = 2

        lblStatus.Text = "CONFECCIONANDO"

        btnEntregar.Enabled = False

        dtpFECHA.Focus()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer, res_item As Integer
        Dim registro As Integer

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        'If chkEliminado.Checked = True Then
        '    Util.MsgStatus(Status1, "No se puede actualizar el registo. Está Eliminado", My.Resources.Resources.stop_error.ToBitmap, True)
        '    Exit Sub
        'End If

        ''''''''''''''''para posicionarme en la fila actual...
        If Not (bolModo) Then
            registro = grd.CurrentRow.Index
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarRegistro()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de DP (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de DP (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                        res_item = AgregarRegistroItems(txtID.Text)
                        Select Case res_item
                            Case -6
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo actualizar el stock el codigo '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo actualizar el stock el codigo '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -5
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo insertar el movimiento de stock el codigo '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo insertar el movimiento de stock el codigo '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -4
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo insertar tabla (Items)'" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo insertar tabla (Items'" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -3
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo insertar asociación producto-proveedor (Items)'" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo insertar asociación producto-proveedor (Items'" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -2
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el ajuste (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el ajuste (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)

                            Case Else
                                Cerrar_Tran()

                                'LlenarcmbMarcasProductos()

                                rdPendientes.Checked = 1

                                SQL = "exec spDevolucionesProveedor_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked


                                bolModo = False
                                PrepararBotones()
                                MDIPrincipal.NoActualizarBase = False
                                btnActualizar_Click(sender, e)

                                btnEntregar.Enabled = True
                                MostrarColumnasyCampos()
                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
            End If 'if ALTa
        End If 'If bolpoliticas Then

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
        '
        Dim res As Integer

        If MessageBox.Show("¿Está seguro que desea Anular la DP seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If chkEliminado.Checked = False Then
            'If MessageBox.Show("Esta acción cambiara el estado del registro." + vbCrLf + "¿Está seguro que desea anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            'Verifico que escriba una nota
            If txtNota.Text.Trim = "" Then
                Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap, True)
                txtNota.Focus()
                Exit Sub
            End If

            MDIPrincipal.Autorizar = False
            Dim Au As New frmUsuarioModo
            Au.ShowDialog()

            If MDIPrincipal.Autorizar = False Then
                Exit Sub
            End If

            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro_DevolucionProveedor()
            Select Case res
                Case -2
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo actualizar el estado del registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo actualizar el estado del registro.", My.Resources.stop_error.ToBitmap, True)
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap, True)
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo anular el registro.", My.Resources.stop_error.ToBitmap, True)
                Case Else
                    res = EliminarRegistro_DevolucionProveedor_Items(txtID.Text)
                    Select Case res
                        Case -6
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error. No se puede sumar el stock.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error. No se puede sumar el  stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -5
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error. No se pudo actualizar stockmov.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Error. No se pudo actualizar stockmov.", My.Resources.Resources.stop_error.ToBitmap, True)
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

                            rdTodas.Checked = True
                            SQL = "exec spDevolucionesProveedor_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodas.Checked
                            'coloco este codigo por ahora
                            Me.Cursor = Cursors.WaitCursor
                            LlenarGrilla()
                            Permitir = True
                            CargarCajas()
                            PrepararBotones()
                            Me.Cursor = Cursors.Default

                            'MDIPrincipal.NoActualizarBase = True
                            'btnActualizar_Click(sender, e)

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
    End Sub

    Private Sub btnEntregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntregar.Click
        Dim res As Integer

        If MessageBox.Show("¿Está seguro que desea Entregar la DP seleccionada?" & vbCrLf & "", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If chkEliminado.Checked = False Then
            res = EntregarRegistro()
            Select Case res
                Case -1
                    Util.MsgStatus(Status1, "No se pudo Entregar la DP.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Entregar la DP.", My.Resources.stop_error.ToBitmap, True)
                Case 0
                    Util.MsgStatus(Status1, "No se pudo Entregar la DP.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Entregar la DP.", My.Resources.stop_error.ToBitmap, True)
                Case Else
                    Imprimir_Pedido(txtCODIGO.Text)
                    PrepararBotones()
                    btnActualizar_Click(sender, e)
                    Util.MsgStatus(Status1, "Se ha Entregado la DP.", My.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se ha Entregado la DP.", My.Resources.ok.ToBitmap, True, True)
            End Select
        End If

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

        param.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)

        param.ShowDialog()



        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)
            rpt.NombreArchivoPDF = "Devolución a Proveedor " & codigo & " - " & cmbPROVEEDORES.Text
            rpt.DevolucionesProveedor_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        rdPendientes.Checked = 1
        btnEntregar.Enabled = True
        bolModo = False
        MostrarColumnasyCampos()
    End Sub

#End Region












End Class