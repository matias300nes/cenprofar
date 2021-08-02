
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmDevoluciones

    Dim llenandoCombo As Boolean = False
    Dim llenandoCombo2 As Boolean = False

    Private ht As New System.Collections.Hashtable() 'usada para almacenar el idcentrocosto de la tabla proyectos

    Dim bolpoliticas As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean
    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    Dim band As Integer

    'Para el combo de busqueda
    Dim ID_Buscado As Long
    Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDDevolucion_Det = 0
        IDMaterial = 1
        Cod_Material = 2
        Nombre_Material = 3
        QtyOrig = 4
        Qty = 5
        IDUnidad = 6
        Unidad = 7
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String


#Region "   Eventos"

    Private Sub frmDevoluciones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la Devolución Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un nueva Devolución?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmDevoluciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        band = 0
        configurarform()
        asignarTags()

        LlenarcmbClientes_App(cmbCliente, ConnStringSEI, llenandoCombo)

        LlenarcmbAlmacenes(cmbAlmacenes, ConnStringSEI)

        LlenarComboMateriales()

        SQL = "exec spDevoluciones_Select_All"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems()
            btnNuevo_Click(sender, e)
        End If

        band = 1

    End Sub

    Private Sub txtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
        If txtID.Text <> "" And bolModo = False Then
            LlenarGridItems()
        End If
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDDevolucion_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDDevolucion_Det)
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

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
            'completar la descripcion del material
            Dim cell As DataGridViewCell = grdItems.CurrentCell
            Dim id As Long
            'im stock As Double = 0,  0, iva As Double = 0, montoiva As Double = 0
            Dim fecha As String = ""
            Dim i As Integer

            Dim nombre As String = ""

            Dim codigo As String, codigo_mat_prov As String = "", unidad As String = "", codunidad As String = "", nombreproveedor As String = "", nombremarca As String = "", plazoentrega As String = ""
            Dim idunidad As Long = 0, idproveedor As Long = 0, idmarca As Long = 0

            If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
                Exit Sub
            End If

            'verificamos si el codigo ya esta en la grilla...
            For i = 0 To grdItems.RowCount - 2
                Dim cuentafilas As Integer
                Dim codigo_mat As String = "", codigo_mat_2 As String = ""
                codigo_mat = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value)
                For cuentafilas = i + 1 To grdItems.RowCount - 2
                    If grdItems.RowCount - 1 > 1 Then
                        'codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
                        codigo_mat_2 = IIf(grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value)
                        If codigo_mat <> "" And codigo_mat_2 <> "" Then
                            If codigo_mat = codigo_mat_2 Then
                                Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex)
                                grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = " "
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            Next

            codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
            cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
            If ObtenerMaterial_App(codigo, codigo_mat_prov, id, nombre, idunidad, unidad, codunidad, 0, 0, 0, 0, 0, 0, 0, 0, 0, fecha, idproveedor, nombreproveedor, idmarca, nombremarca, plazoentrega, 0, 0, 0, 0, 0, 0, ConnStringSEI) = 0 Then
                cell.Value = nombre

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id

                'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value = codigo_mat_prov
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = nombre

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = codunidad
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad

            Else
                cell.Value = "NO EXISTE"
                Exit Sub
            End If
        End If

    End Sub

    'Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing
    '    'controlar lo que se ingresa en la grilla
    '    'en este caso, que no se ingresen letras en el lote
    '    If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Lote Then
    '        AddHandler e.Control.KeyPress, AddressOf validarNumeros
    '    End If
    'End Sub

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
        'SendKeys.Send("{TAB}")
        'SendKeys.Send("{TAB}")
        'If grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.ManejaLote).Value = False Then
        '    SendKeys.Send("{TAB}")
        'End If
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If Not bolModo Then
            chkRemitos.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

    Private Sub chkRemitos_CheckedChanged(sender As Object, e As EventArgs) Handles chkRemitos.CheckedChanged
        cmbRemitos.Enabled = chkRemitos.Checked
        grdItems.Columns(ColumnasDelGridItems.QtyOrig).Visible = chkRemitos.Checked
        If chkRemitos.Checked = True Then
            cmbRemitos_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub cmbRemitos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRemitos.SelectedIndexChanged
        'Dim i As Integer
        If llenandoCombo = False Then
            If bolModo Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
                If Me.cmbCliente.Text <> "" Then
                    PrepararGridItems()
                    LlenarGridItemsporRemito(CType(Me.cmbRemitos.SelectedValue, Long))
                    With (grdItems)
                        .AllowUserToAddRows = False
                        .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
                        .Columns(ColumnasDelGridItems.QtyOrig).ReadOnly = True  'qty                  
                        .Columns(ColumnasDelGridItems.Qty).ReadOnly = False 'qty                  
                    End With
                    Buscar_RemitoFacturado()
                End If
            End If
        End If

    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCliente.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            txtIdCliente.Text = cmbCliente.SelectedValue
            'LimpiarGridItems(grdItems)
            LlenarComboRemitos()
        End If
    End Sub


#End Region

#Region "   Procedimientos"

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

    Private Sub LlenarGridItems()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        SQL = "exec spDevoluciones_Det_Select_By_IDDevolucion @IDDevolucion = " & IIf(txtID.Text = "", 0, txtID.Text)

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IDDevolucion_Det).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDDevolucion_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDDevolucion_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 350

        '------------- Columna de boton para buscar ----------------

        'Dim my_DGVCboColumn As New DataGridViewButtonColumn
        'my_DGVCboColumn.Tag = "Buscar..."
        'my_DGVCboColumn.ToolTipText = "Buscar..."
        'my_DGVCboColumn.UseColumnTextForButtonValue = True
        'my_DGVCboColumn.Text = "..."
        'my_DGVCboColumn.Width = 30
        'grdItems.Columns.Insert(3, my_DGVCboColumn)
        'grdItems.Columns(3).ReadOnly = False

        '-----------------------------------------------------------

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True 'qty
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Qty).Visible = True

        grdItems.Columns(ColumnasDelGridItems.QtyOrig).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).ReadOnly = True 'IDUnidad'
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True 'Unidad'
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = True

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
        SQL = "exec spDevoluciones_Select_All"
    End Sub

    Private Sub GetDatasetItems()
        ' SqlConnection that will be used to execute the sql commands
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se puede establecer la conexión", "Error de Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try


            ' Call ExecuteDataset static method of SqlHelper class that returns a Dataset
            ' We pass in database connection string, command type, stored procedure name and a "1" for CategoryID SqlParameter value 

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
        'grd.Columns(0).Width = 60
        grd.Columns(1).Width = 60 'codigo
        grd.Columns(2).Width = 60 'fecha
        grd.Columns(3).Width = 220 'proyecto

        grd.Columns(4).Width = 200 'cento costo
        'grd.Columns(5).Width = 200 'gerencia
        'grd.Columns(6).Width = 180 'almacen
        'grd.Columns(7).Width = 180 'retira
        'grd.Columns(8).Width = 150 'eliminado Invisible
        'grd.Columns(9).Width = 150 'nota


        '''''se comenta y se cambio por el:      order by C.Codigo Desc en la consulta...
        'ordernar descendente
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

    Private Sub configurarform()
        Me.Text = "Devoluciones"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 150) 'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If
    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        'cmbProyectos.Tag = "3"
        'cmbCENTROSCOSTOS.Tag = "3"
        'cmbObras.Tag = "4"
        'cmbALMACENES.Tag = "6"
        'cmbRetira.Tag = "5"
        'chkEliminado.Tag = "6"
        'txtNota.Tag = "7"
    End Sub

    'Private Sub LlenarComboCentrosCostos()
    '    Dim ds_CentrosCostos As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    llenandoCombo = True

    '    Try
    '        Try

    '            connection = SqlHelper.GetConnection(ConnStringSEI)

    '        Catch ex As Exception
    '            llenandoCombo = False
    '            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try
    '        ds_CentrosCostos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, codigo + '     ' + nombre as codigo FROM CentrosCostos WHERE Eliminado = 0 ORDER BY codigo")

    '        ds_CentrosCostos.Dispose()

    '        With Me.cmbCENTROSCOSTOS
    '            .DataSource = ds_CentrosCostos.Tables(0).DefaultView
    '            .DisplayMember = "codigo"
    '            .ValueMember = "id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
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

    'Private Sub LlenarComboGerencias()
    '    Dim ds_Gerencias As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        'Try

    '        '    connection = SqlHelper.GetConnection(conexion)

    '        'Catch ex As Exception
    '        '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '    Exit Sub
    '        'End Try
    '        'ds_Gerencias = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, codigo + '     ' + nombre as codigo  FROM Gerencias WHERE Eliminado = 0 ORDER BY codigo")
    '        ds_Gerencias = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT g.id, g.codigo + '     ' + g.nombre as codigo from relacionccg r inner join centroscostos cc on cc.id = r.idcentrocosto inner join gerencias g on g.id = r.idgerencia where g.Eliminado = 0 and r.idcentrocosto = " & CType(cmbCENTROSCOSTOS.SelectedValue, Long) & " ORDER BY codigo")

    '        ds_Gerencias.Dispose()

    '        cmbObras.Text = ""

    '        With Me.cmbObras
    '            .DataSource = ds_Gerencias.Tables(0).DefaultView
    '            .DisplayMember = "codigo"
    '            .ValueMember = "id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
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

    'Private Sub LlenarComboProyectos()
    '    Dim ds_Proyectos As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    llenandoCombo2 = True

    '    Try
    '        Try

    '            connection = SqlHelper.GetConnection(ConnStringSEI)

    '        Catch ex As Exception
    '            llenandoCombo2 = False
    '            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try
    '        'ds_Proyectos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre + '(' + codigo + ')' as codigo FROM proyectos WHERE Eliminado = 0 ORDER BY codigo")
    '        ds_Proyectos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre + '(' + codigo + ')' as codigo, idcentrocosto as idcentrocosto FROM proyectos WHERE Eliminado = 0 ORDER BY codigo")

    '        ds_Proyectos.Dispose()

    '        With Me.cmbProyectos
    '            .DataSource = ds_Proyectos.Tables(0).DefaultView
    '            .DisplayMember = "codigo"
    '            .ValueMember = "id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
    '        End With

    '        ht.Clear()
    '        For i As Integer = 0 To ds_Proyectos.Tables(0).Rows.Count - 1
    '            ht.Add(ds_Proyectos.Tables(0).Rows(i).Item("id").ToString, ds_Proyectos.Tables(0).Rows(i).Item("idcentrocosto").ToString)
    '        Next i



    '    Catch ex As Exception
    '        Dim errMessage As String = ""
    '        Dim tempException As Exception = ex

    '        llenandoCombo2 = False

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

    '    llenandoCombo2 = False

    'End Sub

    'Private Sub LlenarComboAlmacenes()
    '    Dim ds_Almacenes As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        Try

    '            connection = SqlHelper.GetConnection(ConnStringSEI)

    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try
    '        ds_Almacenes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre + ' ( ' + codigo + ')' as codigo FROM Almacenes WHERE Eliminado = 0 ORDER BY codigo")
    '        ds_Almacenes.Dispose()

    '        With Me.cmbALMACENES
    '            .DataSource = ds_Almacenes.Tables(0).DefaultView
    '            .DisplayMember = "codigo"
    '            .ValueMember = "id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
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

    'Private Sub LlenarComboRetira()
    '    Dim ds_Retira As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        Try

    '            'connection = SqlHelper.GetConnection(conexion)
    '            connection = SqlHelper.GetConnection(ConnStringSEI)

    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try
    '        ds_Retira = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, apellido + ' ' + nombre + ' (' + codigo + ')'  as codigo FROM Usuarios WHERE Eliminado = 0 and tipo in ('1','2') ORDER BY codigo")
    '        ds_Retira.Dispose()

    '        With Me.cmbRetira
    '            .DataSource = ds_Retira.Tables(0).DefaultView
    '            .DisplayMember = "codigo"
    '            .ValueMember = "id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
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

    Private Sub LlenarComboMateriales()
        Dim ds_Materiales As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ds_Materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo , nombre FROM Materiales WHERE Eliminado = 0 ORDER BY Nombre")
            ds_Materiales.Dispose()

            'With Me.cmbMateriales
            '    .DataSource = ds_Materiales.Tables(0).DefaultView
            '    .DisplayMember = "nombre"
            '    .ValueMember = "Codigo"
            '    .AutoCompleteMode = AutoCompleteMode.Suggest
            '    .AutoCompleteSource = AutoCompleteSource.ListItems
            '    .DropDownStyle = ComboBoxStyle.DropDown
            'End With

            With Me.BuscarDescripcionToolStripMenuItem.ComboBox
                .DataSource = ds_Materiales.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "Codigo"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .BindingContext = Me.BindingContext
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

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer



        'para la funcion BuscarDatosMaterialPorID...
        Dim res As Integer = 0
        'Dim id,
        Dim idunidad = 0, idfamilia As Long = 0
        Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String
        'Dim ManejaLote, eninventario As Boolean
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
        'Verificar si todos los combox tienen algo válido
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'If Not (cmbCENTROSCOSTOS.SelectedIndex > -1) Then
        '    Util.MsgStatus(Status1, "Ingrese un valor en 'Centro de Costos'.", My.Resources.Resources.alert.ToBitmap)
        '    Exit Sub
        'End If
        'If Not (cmbRetira.SelectedIndex > -1) Then
        '    Util.MsgStatus(Status1, "Ingrese un valor en 'Retirado por'.", My.Resources.Resources.alert.ToBitmap)
        '    Exit Sub
        'End If
        'If Not (cmbALMACENES.SelectedIndex > -1) Then
        '    Util.MsgStatus(Status1, "Ingrese un valor en 'Almacén'.", My.Resources.Resources.alert.ToBitmap)
        '    Exit Sub
        'End If
        'If Not (cmbObras.SelectedIndex > -1) Then
        '    Util.MsgStatus(Status1, "Ingrese un valor en 'Area de Imputación'.", My.Resources.Resources.alert.ToBitmap)
        '    Exit Sub
        'End If

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
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Exit Sub
                End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Exit Sub
                End If
                'qty es válida?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la cantidad a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Exit Sub
                End If

                'qty es positiva? solo se permiten valores mayores a cero.
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value <= 0 Then
                    Util.MsgStatus(Status1, "El campo 'Qty' debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Exit Sub
                End If

                'unidad es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Exit Sub
                End If
                ''el lote es un número y es válido?
                'If grdItems.Rows(i).Cells(ColumnasDelGridItems.Lote).Value Is System.DBNull.Value Then
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                '        Util.MsgStatus(Status1, "Falta completar el número de lote en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '        Exit Sub
                '    End If
                'Else
                '    'Controlar que sea un numero
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                '        If Not IsNumeric(grdItems.Rows(i).Cells(ColumnasDelGridItems.Lote).Value) Then
                '            Util.MsgStatus(Status1, "El valor del lote debe ser numerico en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '            Exit Sub
                '        End If
                '    End If
                'End If




                ''verificar si el material esta en inventario......
                'res = BuscarDatosMaterialPorID(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, codigo, nombre, nombrelargo, idfamilia, idunidad)
                'If res = 0 Then
                '    Util.MsgStatus(Status1, "Error al tratar de buscar los datos del material de la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                '    Exit Sub
                'Else
                '    'If eninventario Then
                '    Util.MsgStatus(Status1, "El material de la fila: " & (i + 1).ToString & " esta 'En inventario'", My.Resources.Resources.alert.ToBitmap)
                '    'Exit Sub
                'End If
            End If

        Next i
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If filas > 0 Then
            bolpoliticas = True
        Else
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap)
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

    Private Sub Imprimir()

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        Rpt.SEI_Devoluciones_Remito(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString)

        cnn = Nothing

    End Sub

    Private Sub LlenarGridItemsporRemito(idremito As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        SQL = "exec spDevoluciones_Det_Select_By_IDRemito @IDRemito = " & idremito

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IDDevolucion_Det).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDDevolucion_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDDevolucion_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 350

        '------------- Columna de boton para buscar ----------------

        'Dim my_DGVCboColumn As New DataGridViewButtonColumn
        'my_DGVCboColumn.Tag = "Buscar..."
        'my_DGVCboColumn.ToolTipText = "Buscar..."
        'my_DGVCboColumn.UseColumnTextForButtonValue = True
        'my_DGVCboColumn.Text = "..."
        'my_DGVCboColumn.Width = 30
        'grdItems.Columns.Insert(3, my_DGVCboColumn)
        'grdItems.Columns(3).ReadOnly = False

        '-----------------------------------------------------------

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True 'qty
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Qty).Visible = True

        grdItems.Columns(ColumnasDelGridItems.QtyOrig).ReadOnly = False
        grdItems.Columns(ColumnasDelGridItems.QtyOrig).Width = 70
        grdItems.Columns(ColumnasDelGridItems.QtyOrig).Visible = True

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).ReadOnly = True 'IDUnidad'
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True 'Unidad'
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = True

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
        SQL = "exec spDevoluciones_Select_All"
    End Sub

    Private Sub LlenarComboRemitos()
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select pg.id, pg.Codigo from Presupuestos_Gestion pg" & _
                                          "	JOIN Presupuestos p ON p.id = pg.IDPresupuesto " & _
                                          "WHERE pg.eLIMINADO = 0 AND AnuladoFisico = 0 and IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & " order by pg.id desc")


            ds.Dispose()

            With Me.cmbRemitos
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
            End With

            llenandoCombo = False

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            llenandoCombo = False

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

    Private Sub Buscar_RemitoFacturado()
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Facturado FROM Presupuestos_Gestion WHERE id = " & cmbRemitos.SelectedValue)

            ds.Dispose()

            chkFacturado.Checked = ds.Tables(0).Rows(0)(0)

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

    Private Function AgregarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        'Dim res As Integer = 0

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

                Dim param_remito As New SqlClient.SqlParameter
                param_remito.ParameterName = "@remito"
                param_remito.SqlDbType = SqlDbType.Bit
                param_remito.Value = chkRemitos.Checked
                param_remito.Direction = ParameterDirection.Input

                Dim param_idremito As New SqlClient.SqlParameter
                param_idremito.ParameterName = "@idremito"
                param_idremito.SqlDbType = SqlDbType.BigInt
                param_idremito.Value = cmbRemitos.SelectedValue
                param_idremito.Direction = ParameterDirection.Input

                Dim param_idalmacen As New SqlClient.SqlParameter
                param_idalmacen.ParameterName = "@idalmacen"
                param_idalmacen.SqlDbType = SqlDbType.BigInt
                param_idalmacen.Value = cmbAlmacenes.SelectedValue
                param_idalmacen.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbAlmacenes.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_Insert", _
                                            param_id, param_codigo, param_fecha, param_remito, param_idremito, _
                                            param_idalmacen, param_idcliente, param_nota, param_useradd, param_res)

                    txtID.Text = param_id.Value

                    'txtCODIGO.Text = param_id.Value

                    'res = 

                    AgregarRegistro = param_res.Value

                    'res()

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function AgregarRegistroItems(ByVal idDevolucion As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String = ""
        Dim i As Integer, CantidadFilas As Integer


        Try

            Try

                If chkRemitos.Checked = True Then
                    If grdItems.Rows.Count = 1 Then
                        If grdItems.Rows(0).Cells(ColumnasDelGridItems.QtyOrig).Value = 1 Or _
                            grdItems.Rows(0).Cells(ColumnasDelGridItems.QtyOrig).Value <= grdItems.Rows(0).Cells(ColumnasDelGridItems.Qty).Value Then
                            AgregarRegistroItems = -8
                            Exit Function
                        End If
                    End If
                End If

                If chkRemitos.Checked = False Then
                    CantidadFilas = grdItems.Rows.Count - 2
                Else
                    CantidadFilas = grdItems.Rows.Count - 1
                End If

                For i = 0 To CantidadFilas ' grdItems.Rows.Count - 2

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

                    Dim param_idDevolucion As New SqlClient.SqlParameter
                    param_idDevolucion.ParameterName = "@iddevolucion"
                    param_idDevolucion.SqlDbType = SqlDbType.BigInt
                    param_idDevolucion.Value = idDevolucion
                    param_idDevolucion.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.BigInt
                    param_idmaterial.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)
                    param_idmaterial.Direction = ParameterDirection.Input

                    cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value

                    Dim param_idalmacen As New SqlClient.SqlParameter
                    param_idalmacen.ParameterName = "@idalmacen"
                    param_idalmacen.SqlDbType = SqlDbType.BigInt
                    param_idalmacen.Value = cmbAlmacenes.SelectedValue
                    param_idalmacen.Direction = ParameterDirection.Input

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 4
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Decimal)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.BigInt
                    param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
                    param_idunidad.Direction = ParameterDirection.Input

                    Dim param_remito As New SqlClient.SqlParameter
                    param_remito.ParameterName = "@remito"
                    param_remito.SqlDbType = SqlDbType.Bit
                    param_remito.Value = chkRemitos.Checked
                    param_remito.Direction = ParameterDirection.Input

                    Dim param_idremito As New SqlClient.SqlParameter
                    param_idremito.ParameterName = "@idremito"
                    param_idremito.SqlDbType = SqlDbType.BigInt
                    param_idremito.Value = cmbRemitos.SelectedValue
                    param_idremito.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_Det_Insert", param_id, _
                                                  param_codigo, param_idDevolucion, param_idmaterial, param_idalmacen, _
                                                  param_qty, param_idunidad, param_remito, param_idremito, _
                                                  param_useradd, param_res, param_msg)

                        res = param_res.Value

                        'msg = param_msg.Value

                        'MsgBox(msg)

                        If (res <= 0) Then
                            Exit For
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try
                Next

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

    '            'Dim param_idcentrocosto As New SqlClient.SqlParameter
    '            'param_idcentrocosto.ParameterName = "@idcentrocosto"
    '            'param_idcentrocosto.SqlDbType = SqlDbType.BigInt
    '            'param_idcentrocosto.Value = CType(cmbCENTROSCOSTOS.SelectedValue, Long)
    '            'param_idcentrocosto.Direction = ParameterDirection.Input

    '            Dim param_idgerencia As New SqlClient.SqlParameter
    '            param_idgerencia.ParameterName = "@idgerencia"
    '            param_idgerencia.SqlDbType = SqlDbType.BigInt
    '            param_idgerencia.Value = CType(cmbObras.SelectedValue, Long)
    '            param_idgerencia.Direction = ParameterDirection.Input

    '            'Dim param_idproyecto As New SqlClient.SqlParameter
    '            'param_idproyecto.ParameterName = "@idproyecto"
    '            'param_idproyecto.SqlDbType = SqlDbType.BigInt
    '            'param_idproyecto.Value = cmbProyectos.SelectedValue
    '            'param_idproyecto.Direction = ParameterDirection.Input

    '            'Dim param_idalmacen As New SqlClient.SqlParameter
    '            'param_idalmacen.ParameterName = "@idalmacen"
    '            'param_idalmacen.SqlDbType = SqlDbType.BigInt
    '            'param_idalmacen.Value = CType(cmbALMACENES.SelectedValue, Long)
    '            'param_idalmacen.Direction = ParameterDirection.Input

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
    '                SqlHelper.ExecuteNonQuery(conn_del_form, CommandType.StoredProcedure, "sp_Consumos_Update", param_id, param_codigo, param_fecha, param_idgerencia, param_userupd, param_res)
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

    'Private Function EliminarRegistro() As Integer
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim res As Integer = 0
    '    Dim msg As String
    '    Try
    '        Try
    '            connection = SqlHelper.GetConnection(ConnStringSEI)
    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Function
    '        End Try

    '        'Abrir una transaccion para guardar y asegurar que se guarda todo
    '        If Abrir_Tran(connection) = False Then
    '            MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Function
    '        End If

    '        Try

    '            Dim param_iddevolucionpanol As New SqlClient.SqlParameter("@iddevolucionpanol", SqlDbType.BigInt, ParameterDirection.Input)
    '            param_iddevolucionpanol.Value = CType(txtID.Text, Long)
    '            param_iddevolucionpanol.Direction = ParameterDirection.Input

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

    '            Dim param_msg As New SqlClient.SqlParameter
    '            param_msg.ParameterName = "@mensaje"
    '            param_msg.SqlDbType = SqlDbType.VarChar
    '            param_msg.Size = 250
    '            param_msg.Value = DBNull.Value
    '            param_msg.Direction = ParameterDirection.Output

    '            Try

    '                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_Delete_All", param_iddevolucionpanol, param_userdel, param_res, param_msg)
    '                res = param_res.Value
    '                msg = param_msg.Value

    '                'If res > 0 Then Util.BorrarGrilla(grd)

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

    Private Function fila_vacia(ByVal i) As Boolean
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is Nothing) Then
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

#End Region

#Region "   Botones"

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer, res_item As Integer
        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    'If ALTA Then
                    Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                    res = AgregarRegistro()
                    Select Case res
                        Case -3
                            Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case -2
                            Util.MsgStatus(Status1, "No se pudo actualizar el número de Devolución a SEI (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case Else
                            Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                            res_item = AgregarRegistroItems(txtID.Text)
                            Select Case res_item
                                Case -8
                                    Util.MsgStatus(Status1, "El Remito contiene un solo item con cantidad igual a 1. Anule el Remito desde la pantalla correspondiente", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "El Remito contiene un solo item con cantidad igual a 1. Anule el Remito desde la pantalla correspondiente", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -6
                                    Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap)
                                    Cancelar_Tran()
                                Case -5
                                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                    Cancelar_Tran()
                                Case -4
                                    Util.MsgStatus(Status1, "No se pudo aumentar al stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                    Cancelar_Tran()
                                Case -3
                                    Util.MsgStatus(Status1, "No hay stock suficiente para '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap)
                                    Cancelar_Tran()
                                Case -2
                                    Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap)
                                    Cancelar_Tran()
                                Case -1
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar la devolución a pañol (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Case 0
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Case Else
                                    Cerrar_Tran()

                                    If chkRemitos.Checked = True Then
                                        MsgBox("Revise si es necesario modificar el Presupuesto Original de este remito", MsgBoxStyle.Information, "Atención")
                                    End If

                                    bolModo = False
                                    PrepararBotones()
                                    btnActualizar_Click(sender, e)
                                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                                    Imprimir()
                            End Select
                    End Select
                    '
                    ' cerrar la conexion si está abierta.
                    '
                    If Not conn_del_form Is Nothing Then
                        CType(conn_del_form, IDisposable).Dispose()
                    End If
                    'Else 'if ALTa
                    '    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
                    'End If 'if ALTa
                Else 'If bolModo Then                   
                    Util.MsgStatus(Me.Status1, "Las devoluciones no se pueden modificar.", My.Resources.alert.ToBitmap)
                End If ' If bolModo Then
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If ALTA Then
        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Caso especial, se habilitan porque se inhabilitaron por el caso de un eliminado
        chkEliminado.Checked = False
        chkRemitos.Checked = False
        cmbRemitos.Enabled = False
        chkRemitos.Enabled = True
        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        txtNota.Enabled = True
        ' fin caso especial
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()
        dtpFECHA.Focus()

        band = 1

        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para generar registros nuevos.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    'Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
    '    '
    '    ' Para borrar un vale hay que tener un permiso especial de eliminacion
    '    ' ademas, no se puede borrar un vale ya eliminado de antes.
    '    ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
    '    '
    '    Dim res As Integer
    '    'If BAJA Then
    '    If chkEliminado.Checked = False Then
    '        If MessageBox.Show("Esta acción reversará las devoluciones de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '            Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
    '            res = EliminarRegistro()
    '            Select Case res
    '                Case -6
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "No se registró el consumo.", My.Resources.stop_error.ToBitmap)
    '                Case -5
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "No se registró el detalle del consumo.", My.Resources.stop_error.ToBitmap)
    '                Case -4
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap)
    '                Case -3
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
    '                Case -2
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
    '                Case -1
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
    '                Case 0
    '                    Cancelar_Tran()
    '                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
    '                Case Else
    '                    Cerrar_Tran()
    '                    PrepararBotones()
    '                    btnActualizar_Click(sender, e)
    '                    Setear_Grilla()
    '                    Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
    '            End Select
    '        Else
    '            Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
    '        End If
    '    Else
    '        Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
    '    End If
    '    'Else
    '    'Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
    '    'End If
    'End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes()

        Dim paramDevoluciones As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String

        paramDevoluciones.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)
        paramDevoluciones.ShowDialog()

        If cerroparametrosconaceptar = True Then
            codigo = paramDevoluciones.ObtenerParametros(0)
            rpt.SEI_Devoluciones_Remito(codigo, rpt, My.Application.Info.AssemblyName.ToString)
            cerroparametrosconaceptar = False
            paramDevoluciones = Nothing
            cnn = Nothing
        End If

    End Sub

#End Region

   

   

  
End Class


