Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmConsumos_OLD


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

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDConsumo_Det = 0
        Cod_ConsumoDet = 1
        IDMaterial = 2
        Cod_Material = 3
        Nombre_Material = 4
        Qty = 5
        PrecioUni = 6
        SubTotalProd = 7
        IDUnidad = 8
        Unidad = 9
        Stock = 10
        Minimo = 11
        Maximo = 12
        codunidad = 13
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String


#Region "   Eventos"

    Private Sub frmConsumos_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGridItems(CType(txtID.Text, Long))
            End If
        End If
    End Sub

    Private Sub frmConsumos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.txtID.Visible = False

        configurarform()
        asignarTags()
        LlenarcmbClientes(cmbCliente)
        LlenarcmbObras(cmbObras, ConnStringSEI, cmbCliente.SelectedValue)
        LlenarcmbMateriales()
        LlenarcmbRetira(cmbRetira, ConnStringSEI)

        SQL = "exec sp_Consumos_Select_All"
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos
        Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems(0)
            btnNuevo_Click(sender, e)
        Else
            LlenarGridItems(CType(txtID.Text, Long))
        End If

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
        End If

        permitir_evento_CellChanged = True

    End Sub

    Private Sub frmConsumos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Consumo Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Consumo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub PicCentrosCostos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicClientes.Click
        Dim f As New frmClientes

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbCliente.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbClientes(cmbCliente)
        cmbCliente.Text = texto_del_combo
    End Sub

    Private Sub PicGerencias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicObras.Click
        Dim f As New frmObras

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbObras.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbObras(cmbObras, ConnStringSEI, cmbCliente.SelectedValue)
        cmbObras.Text = texto_del_combo
    End Sub

    Private Sub PicAlmacenes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frmAlmacenes

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        'texto_del_combo = cmbAlmacenes.Text.ToUpper.ToString
        f.ShowDialog()
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            If txtIVA.Text = "" Then
                MsgBox("Debe ingresar el IVA antes de comenzar a cargar los items del Movimiento", MsgBoxStyle.Critical, "Control de IVA")
                Exit Sub
            End If
            'Cuando terminamos la edicion hay que buscar la descripcion del material y las unidad,
            'con esos datos completar la grilla. En este caso es la columna 2
            If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
                'completar la descripcion del material
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim codigo As String, unidad As String = "", nombre As String = "", codunidad As String = ""
                Dim id As Long, idunidad As Long, idproveedor As Long
                Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0, precio As Decimal = 0, ganancia As Decimal = 0
                Dim fecha As Date = Nothing
                Dim i As Integer

                If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
                    Exit Sub
                End If

                'verificamos si el codigo ya esta en la grilla...
                For i = 0 To grdItems.RowCount - 2
                    Dim cuentafilas As Integer
                    Dim codigo_mat As String = "", codigo_mat_2 As String = ""
                    codigo_mat = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                    For cuentafilas = i + 1 To grdItems.RowCount - 2
                        If grdItems.RowCount - 1 > 1 Then
                            codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
                            If codigo_mat = codigo_mat_2 Then
                                Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex)
                                grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = " "
                                Exit Sub
                            End If
                        End If
                    Next
                Next

                codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerMaterial_Acer(codigo, id, nombre, idunidad, unidad, codunidad, stock, minimo, maximo, precio, ganancia, 0, 0, 0, 0, fecha, idproveedor, "") = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.codunidad).Value = codunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Value = stock
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Minimo).Value = minimo
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Maximo).Value = maximo
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioUni).Value = precio

                    'si el stock es cero pintar de rojo..
                    'si el stock es mayor a cero y menor o igual al minimo pintar de amarillo..
                    'si el stock es mayor al minimo dejar en blanco..
                    If stock = 0 Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Red
                    ElseIf stock <= minimo Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Yellow
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.White
                    End If

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                End If
            End If

            If e.ColumnIndex = ColumnasDelGridItems.Qty Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioUni).Value, 2)

                Dim j As Integer = 0

                lblTotalVentasinIVA.Text = "0"

                For j = 0 To grdItems.Rows.Count - 1
                    lblTotalVentasinIVA.Text = CDbl(lblTotalVentasinIVA.Text) + grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value
                Next

                lblIVA.Text = Math.Round(CDbl(lblTotalVentasinIVA.Text) * (CDbl(txtIVA.Text) / 100), 2)
                lblTotalconIVA.Text = CDbl(lblTotalVentasinIVA.Text) + CDbl(lblIVA.Text)

            End If

        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing
        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote
        'If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Lote Then
        '    AddHandler e.Control.KeyPress, AddressOf validarNumeros
        'End If
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub grdItems_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdItems.MouseUp
        Dim Valor As String = ""
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
                'MyBase.Point_Context = p
                'MyBase.Point_Context.Offset(40, 202)

                ContextMenuStrip1.Show(grdItems, p)
                ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
            End If
        End If
    End Sub

    Private Sub txtNota_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNota.KeyDown, txtcopias.KeyDown, txtCODIGO.KeyDown, txtID.KeyDown
        If e.KeyCode = Keys.Enter Then
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

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If Not bolModo Then
            cmbCliente.Enabled = Not chkEliminado.Checked
            cmbObras.Enabled = Not chkEliminado.Checked
            cmbRetira.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

    Private Sub cmbALMACENES_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles cmbCliente.KeyDown, cmbObras.KeyDown, cmbRetira.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbCliente_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedValueChanged
        If llenandoCombo = False Then
            LlenarcmbObras(cmbObras, ConnStringSEI, cmbCliente.SelectedValue)
        End If
    End Sub

    Private Sub chkVerGrilla_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVerGrilla.CheckedChanged
        If chkVerGrilla.Checked = True Then
            VerGrilla()
        Else
            OcultarGrilla()
        End If
    End Sub

    Private Sub chkVenta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVenta.CheckedChanged

        lblObra.Visible = Not chkVenta.Checked
        cmbObras.Visible = Not chkVenta.Checked
        PicObras.Visible = Not chkVenta.Checked
        cmbRetira.Enabled = Not chkVenta.Checked
        PicRetira.Enabled = Not chkVenta.Checked

        lblOrdendeCompra.Visible = chkVenta.Checked
        txtOrdendeCompra.Visible = chkVenta.Checked

    End Sub

    Private Sub txtIVA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIVA.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If txtIVA.Text = "" Then
                MsgBox("Debe ingresar el IVA antes de comenzar a cargar los Items del Movimiento", MsgBoxStyle.Critical, "Control de IVA")
                txtIVA.Focus()
            Else
                lblIVA.Text = Math.Round(CDbl(lblTotalVentasinIVA.Text) * (CDbl(txtIVA.Text) / 100), 2)
                lblTotalconIVA.Text = CDbl(lblTotalVentasinIVA.Text) + CDbl(lblIVA.Text)
                cmbRetira.Focus()
            End If
        End If
    End Sub

    Private Sub txtIVA_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIVA.LostFocus
        If txtIVA.Text = "" Then
            MsgBox("Debe ingresar el IVA antes de comenzar a cargar los Items del Movimiento", MsgBoxStyle.Critical, "Control de IVA")
            txtIVA.Focus()
        Else
            lblIVA.Text = Math.Round(CDbl(lblTotalVentasinIVA.Text) * (CDbl(txtIVA.Text) / 100), 2)
            lblTotalconIVA.Text = CDbl(lblTotalVentasinIVA.Text) + CDbl(lblIVA.Text)
        End If
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Ventas y Consumos de Materiales"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 200) '200'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If
    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer


        'para la funcion BuscarDatosMaterialPorID...
        'Dim res As Integer
        'Dim id,
        'Dim idunidad, idfamilia As Long
        Dim codigo, nombre, nombrelargo, tipo, observaciones As String 'ubicacion,
        'Dim ManejaLote, eninventario As Boolean
        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : observaciones = ""  'ubicacion = ""

        bolpoliticas = False

        'verificar las copias
        Try
            If CType(txtcopias.Text, Integer) < 1 Or CType(txtcopias.Text, Integer) >= 10 Then
                Util.MsgStatus(Status1, "Solo puede imprimir hasta 10 copias.", My.Resources.Resources.alert.ToBitmap, True)
                txtcopias.Focus()
                Exit Sub
            End If
        Catch ex As InvalidCastException
            Util.MsgStatus(Status1, "Ingrese solo Números entre 1 y 10 para la cantidad de copias.", My.Resources.Resources.alert.ToBitmap, True)
            txtcopias.Focus()
            Exit Sub
        End Try



        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si se terminó de editar la celda...
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If editando_celda Then
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si todos los combox tienen algo válido
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not (cmbCliente.SelectedIndex > -1) Then
            Util.MsgStatus(Status1, "Ingrese un valor en 'Cliente'.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If chkVenta.Checked = False Then
            If Not (cmbObras.SelectedIndex > -1) Then
                Util.MsgStatus(Status1, "Ingrese un valor en 'Obras'.", My.Resources.Resources.alert.ToBitmap, True)
                cmbObras.Focus()
                Exit Sub
            End If

            If Not (cmbRetira.SelectedIndex > -1) Then
                Util.MsgStatus(Status1, "Ingrese un valor en 'Retirado por'.", My.Resources.Resources.alert.ToBitmap, True)
                cmbRetira.Focus()
                Exit Sub
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'verificar que no hay nada en la grilla sin datos
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
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'qty es válida?
                Try
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Then
                        Util.MsgStatus(Status1, "Falta completar la cantidad a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    ElseIf grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value <= 0 Then
                        Util.MsgStatus(Status1, "La cantidad a Consumir debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If
                Catch ex As Exception
                    Util.MsgStatus(Status1, "Ingrese una cantidad válida a Consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End Try

                'unidad es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next i



        For i = 0 To grdItems.RowCount - 2
            Dim cuentafilas As Integer
            Dim codigo_mat As String = "", codigo_mat_2 As String = ""
            codigo_mat = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
            For cuentafilas = i + 1 To grdItems.RowCount - 2
                If grdItems.RowCount - 1 > 1 Then
                    codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
                    If codigo_mat = codigo_mat_2 Then
                        Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If
                End If
            Next
        Next


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If filas > 0 Then
            bolpoliticas = True
        Else
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If
    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbCliente.Tag = "3"
        cmbObras.Tag = "4"
        cmbRetira.Tag = "5"
        chkEliminado.Tag = "6"
        txtNota.Tag = "7"
        lblTotalVentasinIVA.Tag = "8"
        txtIVA.Tag = "9"
        lblIVA.Tag = "10"
        lblTotalconIVA.Tag = "11"
        chkVenta.Tag = "12"
        txtOrdendeCompra.Tag = "13"
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

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
        With (grdItems)
            .AllowUserToAddRows = True
            .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
            .Columns(ColumnasDelGridItems.Qty).ReadOnly = False 'qty
        End With
    End Sub

    Private Sub LlenarGridItems(ByVal id As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        'If txtID.Text = "" Then
        If id = 0 Then
            'SQL = "select '' as 'ID','' as 'Codigo','' as 'IDMaterial','' as 'Cod. Material','' as 'Material','' as 'Qty','' as 'IDUnidad','' as 'Unidad',convert(decimal(18,4),0) as 'Stock','' as 'Mínimo','' as 'Máximo','' as 'Status'"
            SQL = " select '' as 'ID', '' as 'Codigo', '' as 'IdMaterial',	'' as 'Cod. Material','' as 'Material', '' as 'Qty', '' AS 'PrecioUnitario', '' AS 'Subtotal', '' as 'IDUnidad', '' as 'Unidad', convert(decimal(18,2),0)	as 'Stock', 0 as 'Minimo', 0 as 'Maximo'"
        Else
            SQL = "exec sp_Consumos_Det_Select_By_IDConsumo @idconsumo = " & id 'txtID.Text
        End If
        'SQL = "exec sp_Consumos_Det_Select_By_IDConsumo @idconsumo = " & txtID.Text

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IDConsumo_Det).ReadOnly = True 'id de Consumo_det
        grdItems.Columns(ColumnasDelGridItems.IDConsumo_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDConsumo_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_ConsumoDet).ReadOnly = True 'Codigo de Consumo_Det
        grdItems.Columns(ColumnasDelGridItems.Cod_ConsumoDet).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Cod_ConsumoDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).ReadOnly = True 'id de material
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True 'Material
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 350

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True 'qty
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Qty).Visible = True

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).ReadOnly = True 'IDUnidad'
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True 'Unidad'
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = True 'stock'
        grdItems.Columns(ColumnasDelGridItems.Stock).Width = 80
        grdItems.Columns(ColumnasDelGridItems.Stock).Visible = True
        '''''grdItems.Columns(ColumnasDelGridItems.Stock).DefaultCellStyle.Format = "f2"
        'grdItems.Columns(ColumnasDelGridItems.Stock).DefaultCellStyle.Format = "n2"
        'grdItems.Columns(ColumnasDelGridItems.Stock).DefaultCellStyle.Format = "#.##0,00"

        grdItems.Columns(ColumnasDelGridItems.Minimo).ReadOnly = True 'minimo'
        grdItems.Columns(ColumnasDelGridItems.Minimo).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Minimo).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Maximo).ReadOnly = True 'maximo'
        grdItems.Columns(ColumnasDelGridItems.Maximo).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Maximo).Visible = True

        grdItems.Columns(ColumnasDelGridItems.PrecioUni).ReadOnly = True 'precio unitario
        grdItems.Columns(ColumnasDelGridItems.PrecioUni).Width = 90
        grdItems.Columns(ColumnasDelGridItems.PrecioUni).Visible = True

        grdItems.Columns(ColumnasDelGridItems.SubTotalProd).ReadOnly = True 'subtotal
        grdItems.Columns(ColumnasDelGridItems.SubTotalProd).Width = 60
        grdItems.Columns(ColumnasDelGridItems.SubTotalProd).Visible = True

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

        ''NO DEJA EDITAR LA CELDA DEL valor SI REPRESENTATIVO TIENE LA 'C'...ver si hay mas casos
        'For x As Integer = 0 To grdItems.Rows.Count - 1
        '    If grdItems.Rows(x).Cells(8).Value.ToString.ToUpper = "C" Then
        '        grdItems.Rows(x).Cells(1).ReadOnly = True
        '    Else
        '        grdItems.Rows(x).Cells(1).ReadOnly = False
        '    End If
        '    If grdItems.Rows(x).Cells(11).Value.ToString.ToUpper = "R" Then 'columna de Status
        '        If grdItems.Rows(x).Cells(8).Value.ToString.ToUpper = "R" Then 'columna de representativo
        '            grdItems.Item(1, x).Style.BackColor = Color.Pink
        '        Else
        '            grdItems.Item(1, x).Style.BackColor = Color.Red
        '        End If
        '    ElseIf grdItems.Rows(x).Cells(11).Value.ToString.ToUpper = "L" Then 'columna de Status
        '        If grdItems.Rows(x).Cells(8).Value.ToString.ToUpper = "R" Then 'columna de representativo
        '            grdItems.Item(1, x).Style.BackColor = Color.GreenYellow
        '        Else
        '            grdItems.Item(1, x).Style.BackColor = Color.Green
        '        End If
        '    Else
        '        grdItems.Item(1, x).Style.BackColor = Color.White
        '    End If
        '    If grdItems.Rows(x).Cells(8).Value.ToString.ToUpper = "M" Then
        '        'LlenarComboLotesEnLaGrilla(CType(grdEnsayos.Rows(x).Cells(0).Value.ToString, Long), x)
        '    End If
        'Next

        'Volver la fuente de datos a como estaba...
        SQL = "exec sp_Consumos_Select_All"
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

    Private Sub Setear_Grilla()
        'grd.Columns(ColumnasDelGridItems.Cod_ConsumoDet).Width = 60
        'grd.Columns(ColumnasDelGridItems.IDMaterial).Width = 60
        'grd.Columns(ColumnasDelGridItems.Cod_Material).Width = 280
        'grd.Columns(ColumnasDelGridItems.Nombre_Material).Width = 280
        'grd.Columns(ColumnasDelGridItems.Lote).Width = 100
        'grd.Columns(ColumnasDelGridItems.Qty).Width = 250
        'grd.Columns(ColumnasDelGridItems.IDUnidad).Width = 200 'retiro
        'grd.Columns(ColumnasDelGridItems.Unidad).Width = 200 'nota

        'ordernar descendente
        grd.Sort(grd.Columns(1), System.ComponentModel.ListSortDirection.Descending)

        'setear grilla de items
        'With grdItems
        '    .VirtualMode = False
        '    .AllowUserToAddRows = False
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.MintCream
        '    .RowsDefaultCellStyle.BackColor = Color.White
        '    .AllowUserToOrderColumns = True
        '    .SelectionMode = DataGridViewSelectionMode.CellSelect
        'End With
    End Sub

    Private Sub LlenarcmbClientes(ByVal cmb As System.Windows.Forms.ComboBox)
        Dim ds_Clientes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

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

        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbMateriales()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Materiales As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo, rtrim(nombre) as Nombre FROM Materiales WHERE Eliminado = 0 ORDER BY nombre")
            ds_Materiales.Dispose()

            With Me.BuscarDescripcionToolStripMenuItem.ComboBox
                .DataSource = ds_Materiales.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "Codigo"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .BindingContext = Me.BindingContext
                .SelectedIndex = 0
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

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        'Borrar la fila actual
        'If cell.RowIndex <> 0 Then
        If cell.RowIndex >= 0 Then 'el de arriba no borraba la fila 0....
            Try
                grdItems.Rows.RemoveAt(cell.RowIndex)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then

            Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material)
            Dim cell2 As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material)
            grdItems.CurrentCell = cell
            grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.Text
            grdItems.CurrentCell = cell2
            grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.ComboBox.SelectedValue

            ContextMenuStrip1.Close()
            grdItems.BeginEdit(True)
        ElseIf e.KeyCode = Keys.Escape Then
            ContextMenuStrip1.Close()

        End If
    End Sub

    Private Sub VerGrilla()

        'MsgBox("group location: " + Me.GroupBox1.Location.ToString + " width: " + Me.GroupBox1.Size.Width.ToString + " Height: " + Me.GroupBox1.Size.Height.ToString)

        'Dim p2 As New Size(GroupBox1.Size.Width, GroupBox1.Size.Height - grd.Size.Height - 20) '200'AltoMinimoGrilla)
        Dim p2 As New Size(GroupBox1.Size.Width, 390) '200'AltoMinimoGrilla)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, GroupBox1.Size.Height - 100) '200'AltoMinimoGrilla)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 150) '200'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        Me.grd.Visible = True

        'MsgBox("group location: " + Me.GroupBox1.Location.ToString + " width: " + Me.GroupBox1.Size.Width.ToString + " Height: " + Me.GroupBox1.Size.Height.ToString)
        'MsgBox("grd location: " + Me.grd.Location.ToString + " width: " + Me.grd.Size.Width.ToString + " Height: " + Me.grd.Size.Height.ToString)

    End Sub

    Private Sub OcultarGrilla()
        Dim p2 As New Size(GroupBox1.Size.Width, 560) '200'AltoMinimoGrilla)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, 462) '200'AltoMinimoGrilla)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Visible = False
    End Sub


#End Region

#Region "   Funciones"

    Private Function AgregarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try

                Dim IVA As Decimal

                IVA = CType(txtIVA.Text, Decimal) / 100

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_venta As New SqlClient.SqlParameter
                param_venta.ParameterName = "@venta"
                param_venta.SqlDbType = SqlDbType.Bit
                param_venta.Value = chkVenta.Checked
                param_venta.Direction = ParameterDirection.Input

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

                Dim param_idCliente As New SqlClient.SqlParameter
                param_idCliente.ParameterName = "@idCliente"
                param_idCliente.SqlDbType = SqlDbType.BigInt
                param_idCliente.Value = cmbCliente.SelectedValue
                param_idCliente.Direction = ParameterDirection.Input

                Dim param_idobra As New SqlClient.SqlParameter
                param_idobra.ParameterName = "@idobra"
                param_idobra.SqlDbType = SqlDbType.BigInt
                param_idobra.Value = IIf(chkVenta.Checked = True, DBNull.Value, CType(cmbObras.SelectedValue, Long))
                param_idobra.Direction = ParameterDirection.Input

                Dim param_totalsiniva As New SqlClient.SqlParameter
                param_totalsiniva.ParameterName = "@totalsiniva"
                param_totalsiniva.SqlDbType = SqlDbType.Decimal
                param_totalsiniva.Precision = 18
                param_totalsiniva.Scale = 2
                param_totalsiniva.Value = CType(lblTotalVentasinIVA.Text, Decimal)
                param_totalsiniva.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = IVA
                param_iva.Direction = ParameterDirection.Input

                Dim param_idretira As New SqlClient.SqlParameter
                param_idretira.ParameterName = "@idretira"
                param_idretira.SqlDbType = SqlDbType.BigInt
                param_idretira.Value = IIf(chkVenta.Checked = True, DBNull.Value, CType(cmbRetira.SelectedValue, Long))
                param_idretira.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_ordendecompra As New SqlClient.SqlParameter
                param_ordendecompra.ParameterName = "@ordendecompra"
                param_ordendecompra.SqlDbType = SqlDbType.VarChar
                param_ordendecompra.Size = 25
                param_ordendecompra.Value = txtOrdendeCompra.Text
                param_ordendecompra.Direction = ParameterDirection.Input

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

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Insert", _
                                              param_id, param_codigo, param_fecha, param_idCliente, _
                                              param_idobra, param_idretira, param_nota, param_useradd, _
                                              param_totalsiniva, param_iva, param_venta, param_ordendecompra, param_res)

                    txtID.Text = param_id.Value
                    txtCODIGO.Text = param_id.Value
                    codigoconsumovale = param_id.Value
                    res = param_res.Value

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
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Private Function AgregarRegistroItems(ByVal idconsumo As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer

        Try

            Try
                i = 0
                Do While i < grdItems.Rows.Count - 1

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

                    Dim param_idconsumo As New SqlClient.SqlParameter
                    param_idconsumo.ParameterName = "@idconsumo"
                    param_idconsumo.SqlDbType = SqlDbType.BigInt
                    param_idconsumo.Value = idconsumo
                    param_idconsumo.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.BigInt
                    param_idmaterial.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)
                    param_idmaterial.Direction = ParameterDirection.Input

                    cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 4
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Decimal)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_precio As New SqlClient.SqlParameter
                    param_precio.ParameterName = "@preciouni"
                    param_precio.SqlDbType = SqlDbType.Decimal
                    param_precio.Precision = 18
                    param_precio.Scale = 2
                    param_precio.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioUni).Value, Decimal)
                    param_precio.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.BigInt
                    param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
                    param_idunidad.Direction = ParameterDirection.Input

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

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Det_Insert", _
                                                  param_id, param_codigo, param_idconsumo, param_idmaterial, _
                                                  param_qty, param_idunidad, param_idCliente, param_idobra, param_precio, _
                                                  param_useradd, param_res, param_msg)
                        res = param_res.Value
                        msg = param_msg.Value
                        If (res <= 0) Then
                            Exit Do
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

    'Private Function ActualizarRegistro() As Integer
    '    'Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim res As Integer = 0

    '    Try

    '        Try
    '            Dim param_id As New SqlClient.SqlParameter
    '            param_id.ParameterName = "@id"
    '            param_id.SqlDbType = SqlDbType.BigInt
    '            param_id.Value = CType(txtID.Text, Long)
    '            param_id.Direction = ParameterDirection.Input

    '            Dim param_codigo As New SqlClient.SqlParameter
    '            param_codigo.ParameterName = "@codigo"
    '            param_codigo.SqlDbType = SqlDbType.VarChar
    '            param_codigo.Size = 25
    '            param_codigo.Value = txtCODIGO.Text
    '            param_codigo.Direction = ParameterDirection.Input

    '            Dim param_fecha As New SqlClient.SqlParameter
    '            param_fecha.ParameterName = "@fecha"
    '            param_fecha.SqlDbType = SqlDbType.DateTime
    '            param_fecha.Value = dtpFECHA.Value
    '            param_fecha.Direction = ParameterDirection.Input

    '            Dim param_idCliente As New SqlClient.SqlParameter
    '            param_idCliente.ParameterName = "@idCliente"
    '            param_idCliente.SqlDbType = SqlDbType.BigInt
    '            param_idCliente.Value = IIf(chkVenta.Checked = True, DBNull.Value, CType(cmbCliente.SelectedValue, Long))
    '            param_idCliente.Direction = ParameterDirection.Input

    '            Dim param_idobra As New SqlClient.SqlParameter
    '            param_idobra.ParameterName = "@idobra"
    '            param_idobra.SqlDbType = SqlDbType.BigInt
    '            param_idobra.Value = IIf(chkVenta.Checked = True, DBNull.Value, CType(cmbObras.SelectedValue, Long))
    '            param_idobra.Direction = ParameterDirection.Input


    '            Dim param_userupd As New SqlClient.SqlParameter
    '            param_userupd.ParameterName = "@userupd"
    '            param_userupd.SqlDbType = SqlDbType.Int
    '            param_userupd.Value = UserID
    '            param_userupd.Direction = ParameterDirection.Input

    '            Dim param_res As New SqlClient.SqlParameter
    '            param_res.ParameterName = "@res"
    '            param_res.SqlDbType = SqlDbType.Int
    '            param_res.Value = DBNull.Value
    '            param_res.Direction = ParameterDirection.InputOutput

    '            Try
    '                SqlHelper.ExecuteNonQuery(conn_del_form, CommandType.StoredProcedure, "sp_Consumos_Update", _
    '                                          param_id, param_codigo, param_fecha, param_idCliente, _
    '                                          param_idobra, param_userupd, param_res)
    '                res = param_res.Value

    '                PrepararBotones()
    '                ActualizarRegistro = res
    '                If res > 0 Then ActualizarGrilla(grd, Me)


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

    '    End Try
    'End Function

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

            Try

                Dim param_idconsumo As New SqlClient.SqlParameter("@idconsumo", SqlDbType.BigInt, ParameterDirection.Input)
                param_idconsumo.Value = CType(txtID.Text, Long)
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

                    EliminarRegistro = res

                Catch ex As Exception
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

    Private Function fila_vacia(ByVal i) As Boolean
        Try
            If (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is Nothing) _
                                And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is Nothing) _
                                And (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is Nothing) Then
                fila_vacia = True
            Else
                fila_vacia = False
            End If
        Catch ex As Exception
            fila_vacia = True
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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDConsumo_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDConsumo_Det)
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

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        chkEliminado.Checked = False
        chkVenta.Checked = False
        cmbCliente.Enabled = True
        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        txtNota.Enabled = True
        btnReimprimirVale.Enabled = False
        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()
        txtcopias.Text = "1"
        txtIVA.Text = "21"
        lblIVA.Text = "0"
        lblTotalconIVA.Text = "0"
        lblTotalVentasinIVA.Text = "0"
        dtpFECHA.Focus()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer, res_item As Integer, res_vale As Integer

        Util.Logueado_OK = False

        If chkVenta.Checked = True Then
            cmbRetira.AccessibleName = ""
            cmbRetira.SelectedValue = 1
            cmbObras.AccessibleName = ""
            cmbObras.SelectedValue = 1
        Else
            cmbRetira.AccessibleName = "*Retira"
            cmbObras.AccessibleName = "*Obra"
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                    res = AgregarRegistro()
                    Select Case res
                        Case -3
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                        Case -2
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo actualizar el número de consumo (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo actualizar el número de consumo (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
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
                                    Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case -5
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & Trim(cod_aux) & "'", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & Trim(cod_aux) & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case -4
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo descontar del stock (items) '" & Trim(cod_aux) & "'", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo descontar del stock (items) '" & Trim(cod_aux) & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case -3
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No hay stock suficiente para '" & Trim(cod_aux) & "'", My.Resources.Resources.alert.ToBitmap)
                                    Util.MsgStatus(Status1, "No hay stock suficiente para '" & Trim(cod_aux) & "'", My.Resources.Resources.alert.ToBitmap, True)
                                Case -2
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap)
                                    Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap, True)
                                Case -1
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar el consumo (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo registrar el consumo (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case 0
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case Else
                                    Cerrar_Tran()
                                    Util.AgregarGrilla(grd, Me, Permitir) 'agregar el nuevo registro a la grilla
                                    bolModo = False
                                    PrepararBotones()
                                    btnActualizar_Click(sender, e)
                                    Setear_Grilla()

                                    'Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap, True, True)
                                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                                    ''impresion del vale dg 24-11-2011
                                    'Dim Reporte As New frmReportes

                                    'cp primero le clavamos el impreso en true
                                    res_vale = ActualizarConsumoImpreso(codigoconsumovale)
                                    Select Case res_vale
                                        Case -1
                                            Util.MsgStatus(Status1, "No se pudo actualizar el campo 'Impreso'. Error", My.Resources.Resources.stop_error.ToBitmap, True)
                                        Case -2
                                            Util.MsgStatus(Status1, "No se pudo actualizar el campo 'Impreso'. No existe el id.", My.Resources.Resources.stop_error.ToBitmap, True)
                                        Case Else
                                            Util.MsgStatus(Status1, "Se ha actualizado el registro y el campo 'Impreso'.", My.Resources.Resources.ok.ToBitmap, False, True)
                                    End Select


                                    'dg 06-10-2011 hacemos un bucle por la cantidad de copias
                                    Dim cant_copias As Integer
                                    cant_copias = CType(txtcopias.Text, Integer)
                                    If cant_copias > 0 Then
                                        For i As Integer = 1 To cant_copias
                                            'impresion del vale dg 24-11-2011
                                            Dim Reporte As New frmReportes
                                            '1 por pantalla.....2 por impresora.......
                                            'Reporte.MostrarVale(Reporte, 1, codigoconsumovale, "'SI'", 1)
                                            Reporte = Nothing
                                        Next
                                    End If

                                    btnReimprimirVale.Enabled = True
                                    grd.Rows(0).Selected = True
                                    grd.CurrentCell = grd.Rows(0).Cells(1)

                                    'Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap, True, True)

                                    btnNuevo_Click(sender, e)
                            End Select
                    End Select
                    '
                    ' cerrar la conexion si está abierta.
                    '
                    If Not conn_del_form Is Nothing Then
                        CType(conn_del_form, IDisposable).Dispose()
                    End If
                Else 'if ALTa
                    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap, True)
                End If 'if ALTa
            Else
                Util.MsgStatus(Me.Status1, "Los consumos no se pueden modificar.", My.Resources.alert.ToBitmap)
                Util.MsgStatus(Me.Status1, "Los consumos no se pueden modificar.", My.Resources.alert.ToBitmap, True)
            End If ' If bolModo Then
        End If 'If bolpoliticas Then
        'End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
        '
        Dim res As Integer

        ''''''''''''''''para posicionarme en la fila actual...
        Dim registro As Integer 'DataGridViewRow
        registro = grd.CurrentRow.Index


        If Not bolModo Then
            'If BAJA Then
            If chkEliminado.Checked = False Then
                If MessageBox.Show("Esta acción reversará los consumos de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                    res = EliminarRegistro()
                    Select Case res
                        Case -6
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se registró el consumo.", My.Resources.stop_error.ToBitmap)
                        Case -5
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se registró el detalle del consumo.", My.Resources.stop_error.ToBitmap)
                        Case -4
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap)
                        Case -3
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
                        Case -2
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                        Case -1
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                        Case 0
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                        Case Else
                            Cerrar_Tran()
                            PrepararBotones()
                            btnActualizar_Click(sender, e)
                            Setear_Grilla()
                            grd.Rows(registro).Selected = True
                            grd.CurrentCell = grd.Rows(registro).Cells(1)
                            Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                            Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True)
                    End Select
                Else
                    Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
                End If
            Else
                Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap, True)
        End If

        ' End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        ''nbreformreportes = "Listado de Consumos"

        'Dim Primer As Date

        'Dim paramConsumos As New frmParametros
        'Dim cnn As New SqlConnection(conexion)
        'Dim codigoconsumo As String, material As String, retiro As String, cc As String
        'Dim desde As Date, hasta As Date
        'Dim ReporteMaestroConsumos As New frmReportes


        'Dim codigoretiro As String = ""
        'Dim codigomaterial As String = ""
        'Dim centrodecosto As String = ""

        ''es igual el select de LlenarComboRetira()..
        'codigoretiro = "SELECT apellido + ' ' + nombre + ' (' + codigo + ')'  as codigo FROM tm3.dbo.Usuarios WHERE Eliminado = 0 and Tipo in ('1','2') ORDER BY codigo"
        'codigomaterial = "SELECT codigo + ' - ' +  nombre FROM Materiales WHERE Eliminado = 0 ORDER BY Nombre"
        'centrodecosto = "SELECT codigo + ' - ' + nombre as codigo FROM CentrosCostos WHERE Eliminado = 0 ORDER BY codigo"

        'Primer = DateSerial(Year(Date.Now), Month(Date.Now) + 0, 1)
        'paramConsumos.AgregarParametros("N° de Consumo:", "STRING", "", False, , "", cnn)
        'paramConsumos.AgregarParametros("Material:", "STRING", "", False, , codigomaterial, cnn)
        'paramConsumos.AgregarParametros("Retiró:", "STRING", "", False, , codigoretiro, cnn)
        'paramConsumos.AgregarParametros("Desde:", "DATE", "", False, Primer, "", cnn)
        'paramConsumos.AgregarParametros("Hasta:", "DATE", "", False, Date.Now, "", cnn)
        'paramConsumos.AgregarParametros("Centro de Costo:", "STRING", "", False, , centrodecosto, cnn)

        'paramConsumos.ShowDialog()
        'If cerroparametrosconaceptar = True Then
        '    If paramConsumos.ObtenerParametros(0) <> "" Then
        '        codigoconsumo = CType(paramConsumos.ObtenerParametros(0), Long)
        '    Else
        '        codigoconsumo = paramConsumos.ObtenerParametros(0)
        '    End If
        '    material = Mid(paramConsumos.ObtenerParametros(1), 1, 6)
        '    If Len(paramConsumos.ObtenerParametros(2)) > 0 Then
        '        retiro = Mid(paramConsumos.ObtenerParametros(2), Len(paramConsumos.ObtenerParametros(2)) - 4, 4)
        '    Else
        '        retiro = ""
        '    End If
        '    desde = paramConsumos.ObtenerParametros(3)
        '    hasta = paramConsumos.ObtenerParametros(4)
        '    cc = Mid(paramConsumos.ObtenerParametros(5), 1, 4)

        '    'ReporteMaestroConsumos.MostrarMaestroConsumos(codigoconsumo, material, retiro, desde, hasta, cc, ReporteMaestroConsumos)

        '    cerroparametrosconaceptar = False
        '    paramConsumos = Nothing
        '    cnn = Nothing
        'End If

    End Sub

    Private Sub btnReimprimirVale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReimprimirVale.Click

        Dim Reporte As New frmReportes

        If chkEliminado.Checked = False Then
            '1 por pantalla.....2 por impresora.......
            'Reporte.MostrarVale(Reporte, 1, CType(txtID.Text, Long), "'NO'", CType(txtcopias.Text, Integer)) 'NO TENGO LA MENOR IDEA DE PORQUE HAY QUE PONERLE LAS COMILLAS..
        Else
            Util.MsgStatus(Status1, "No puede reimprimir el vale de consumo ya que está 'Eliminado'.", My.Resources.Resources.stop_error.ToBitmap)
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btnReimprimirVale.Enabled = True
        If txtID.Text <> "" Then
            LlenarGridItems(CType(txtID.Text, Long))
        End If
    End Sub

    Private Sub btnStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStock.Click

        Cursor = System.Windows.Forms.Cursors.WaitCursor
        'nbreformreportes = "Materiales En Stock"
        Dim rpt As New frmReportes
        Dim paramConsumos As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo_mat As String = ""

        paramConsumos.AgregarParametros("Código de material:", "STRING", "", False, , "", cnn)
        paramConsumos.ShowDialog()
        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
        If cerroparametrosconaceptar = True Then
            codigo_mat = paramConsumos.ObtenerParametros(0)
            'rpt.MostrarMaterialesEnStock(rpt, codigo_mat, "")
        End If
        Cursor = System.Windows.Forms.Cursors.Default

    End Sub

#End Region

End Class


