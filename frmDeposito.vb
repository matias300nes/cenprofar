
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient



Public Class frmDeposito
    Dim bolpoliticas As Boolean, band As Boolean

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

    Dim Consulta As String

    Dim TotalPaginas1 As Integer
    Dim bolPaginar1 As Boolean = False
    Dim dtnew1 As New System.Data.DataTable
    Dim ini1 As Integer = 0
    Dim fin1 As Integer = 50 - 1
    Dim quitarfiltro1 As Boolean

    Public UltBusqueda As String

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDAjuste_Det = 0
        Cod_AjusteDet = 1
        IDMaterial = 2
        Cod_Material = 3
        CodBarra = 4
        Pasillo = 5
        Estante = 6
        Fila = 7
        Nombre_Material = 8
        IDUnidad = 9
        Unidad = 10
        QtyActual = 11
        QtyNva = 12
        QtyDif = 13
        Nota = 14
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String


#Region "   Eventos"

    Private Sub frmAjustes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Ajuste Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un nuevo Ajuste?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmAjustes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        'band = False

        'configurarform()
        'asignarTags()
        'LlenarComboMotivos()
        ''LlenarComboMateriales()

        'SQL = "exec spAjustes_Select_All"

        'LlenarGrilla()
        'Permitir = True
        'CargarCajas()
        'PrepararBotones()
        '' ajuste de la grilla de navegación según el tamaño de los datos
        'Setear_Grilla()

        'If bolModo = True Then
        '    LlenarGridItems()
        '    btnNuevo_Click(sender, e)
        'End If

        'If grd.RowCount > 0 Then
        '    grd.Rows(0).Selected = True
        '    grd.CurrentCell = grd.Rows(0).Cells(1)
        'End If

        'GroupBox1.Enabled = bolModo

        'dtpFECHA.MaxDate = Today.Date

        'band = True

    End Sub

    Private Sub txtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
        If txtID.Text <> "" And bolModo = False Then
            LlenarGridItems()
        End If
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        '    'Cuando terminamos la edicion hay que buscar la descripcion del material y las unidad,
        '    'con esos datos completar la grilla. En este caso es la columna 2
        If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
            'completar la descripcion del material
            Dim cell As DataGridViewCell = grdItems.CurrentCell
            Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = ""
            Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
            Dim codunidad As String = ""
            Dim idproveedor As Long
            Dim CodBarra As String = "", Pasillo As String = "", Estante As String = "", fila As String = ""

            If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then Exit Sub

            codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

            cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
            If ObtenerMaterial_App(codigo, "", id, nombre, idunidad, unidad, codunidad, stock, minimo, maximo, 0, 0, 0, 0, 0, 0, "", idproveedor, "", 0, "", "", 0, "", 0, 0, 0, 0, ConnStringSEI, CodBarra, Pasillo, Estante, fila) = 0 Then
                cell.Value = nombre
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id 'cell.ColumnIndex - 2
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad 'cell.ColumnIndex + 2
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad 'cell.ColumnIndex + 3
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = nombre  'cell.ColumnIndex + 3

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodBarra).Value = CodBarra  'cell.ColumnIndex + 3
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Pasillo).Value = Pasillo  'cell.ColumnIndex + 3
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Estante).Value = Estante 'cell.ColumnIndex + 3
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Fila).Value = fila  'cell.ColumnIndex + 3

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyActual).Value = stock  'cell.ColumnIndex + 3

                grdItems.CurrentCell = grdItems(ColumnasDelGridItems.CodBarra, grdItems.CurrentRow.Index)

            Else
                cell.Value = "NO EXISTE"
            End If

        End If

        If e.ColumnIndex = ColumnasDelGridItems.QtyNva Then
            Dim cell As DataGridViewCell = grdItems.CurrentCell

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.QtyNva).Value Is DBNull.Value Then
                'Util.MsgStatus(Status1, "Debe ingresar una cantidad válida.", My.Resources.Resources.stop_error.ToBitmap, True)
                'grdItems.CurrentCell.Selected = True
                Exit Sub
            End If

            'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyDif).Value = CDbl(FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value, 2))

            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyDif).Value = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyNva).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyNva).Value) - grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyActual).Value

        End If

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote
        'If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Lote Then
        '    AddHandler e.Control.KeyPress, AddressOf validarNumeros
        'End If

        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.QtyNva Then
            AddHandler e.Control.KeyPress, AddressOf validar_NumerosReales2
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

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFECHA.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        'Borrar la fila actual
        Try
            If cell.RowIndex <> 0 Then
                grdItems.Rows.RemoveAt(cell.RowIndex)
            End If
        Catch ex As Exception

        End Try
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
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

#End Region

#Region "   Procedimientos"

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDAjuste_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDAjuste_Det)
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

        Try
            If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.QtyNva Then
                grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material, grdItems.CurrentRow.Index + 1)
                Return True
            End If

            If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Cod_Material Then
                grdItems.CurrentCell = grdItems(ColumnasDelGridItems.CodBarra, grdItems.CurrentRow.Index)
                Return True
            End If

        Catch ex As Exception

        End Try

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

    Private Sub configurarform()
        Me.Text = "Ajustes de Inventario"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
        Me.grd.Size = New Size(p)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 3 - GroupBox1.Size.Height - GroupBox1.Location.Y - 62) '65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        txtNota.Tag = "4"
        chkEliminado.Tag = "5"
    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer 

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
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'unidad es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Ingrese un valor correcto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Ingrese un valor correcto en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If


                'unidad es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a ajustar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a ajustar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                ''verificar si el material esta en inventario......
                'res = BuscarDatosMaterialPorID_ACER(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, codigo, nombre, idunidad)
                'If res = 0 Then
                '    Util.MsgStatus(Status1, "Error al tratar de buscar los datos del material de la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '    Exit Sub
                'Else
                '    'If eninventario Then
                '    Util.MsgStatus(Status1, "El material de la fila: " & (i + 1).ToString & " esta 'En inventario'", My.Resources.Resources.alert.ToBitmap, True)
                '    'Exit Sub
                'End If
            End If
        Next i

        i = 0
        Dim MovGrilla As Boolean = False

        For i = 0 To grdItems.RowCount - 1
            'qty es válida?
            Try
                'If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is System.DBNull.Value) Then
                '    If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is System.DBNull.Value) Or (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value <> 0) Then
                '        MovGrilla = True
                '        Exit For
                '    End If
                'End If
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyDif).Value <> 0 Then
                    'If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is System.DBNull.Value) Or (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value <> 0) Then
                    MovGrilla = True
                    Exit For
                    'End If
                End If

            Catch ex As Exception
                Util.MsgStatus(Status1, "Ingrese una cantidad válida a ajustar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "Ingrese una cantidad válida a ajustar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End Try

        Next

        If MovGrilla = False Then
            Util.MsgStatus(Status1, "No se registró ningún movimiento en la grilla. Por favor, verifique", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No se registró ningún movimiento en la grilla. Por favor, verifique", My.Resources.Resources.alert.ToBitmap, True)
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

    Private Sub validar_NumerosReales2( _
      ByVal sender As Object, _
      ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridItems.QtyNva Then

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

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
        With (grdItems)
            .AllowUserToAddRows = False
            .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
            .Columns(ColumnasDelGridItems.QtyNva).ReadOnly = False 'qty
        End With
    End Sub

    Private Sub LlenarGridItems()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        If bolModo = True Then

                SQL = "exec spAjustes_Det_Select_By_RubroSubrubro @idrubro = 0, @idsubrubro = 0"

        Else
            SQL = "exec spAjustes_Det_Select_By_IDAjuste @idajuste = " & txtID.Text
        End If

        GetDatasetItems()

        'If chkBusqueda.Checked = False Then
        grdItems.Columns(ColumnasDelGridItems.IDAjuste_Det).ReadOnly = True 'id de Ajuste_det
        grdItems.Columns(ColumnasDelGridItems.IDAjuste_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDAjuste_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_AjusteDet).ReadOnly = True 'Codigo de Ajuste_Det
        grdItems.Columns(ColumnasDelGridItems.Cod_AjusteDet).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Cod_AjusteDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).ReadOnly = True 'id de material
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Not chkPorCodigo.Checked ' True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.CodBarra).ReadOnly = False 'Not chkPorCodigo.Checked ' True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.CodBarra).Width = 130
        grdItems.Columns(ColumnasDelGridItems.CodBarra).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Pasillo).ReadOnly = False 'Not chkPorCodigo.Checked ' True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Pasillo).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Pasillo).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Estante).ReadOnly = False 'Not chkPorCodigo.Checked ' True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Estante).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Estante).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Fila).ReadOnly = False 'Not chkPorCodigo.Checked ' True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Fila).Width = 40
        grdItems.Columns(ColumnasDelGridItems.Fila).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True 'Material
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 350

        grdItems.Columns(ColumnasDelGridItems.QtyActual).Visible = True
        grdItems.Columns(ColumnasDelGridItems.QtyActual).ReadOnly = True 'qty
        grdItems.Columns(ColumnasDelGridItems.QtyActual).Width = 60

        grdItems.Columns(ColumnasDelGridItems.QtyNva).ReadOnly = Not bolModo
        grdItems.Columns(ColumnasDelGridItems.QtyNva).Width = 60
        grdItems.Columns(ColumnasDelGridItems.QtyNva).Visible = bolModo

        grdItems.Columns(ColumnasDelGridItems.QtyDif).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.QtyDif).Width = 60
        grdItems.Columns(ColumnasDelGridItems.QtyDif).Visible = bolModo

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).ReadOnly = True 'IDUnidad'
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True 'Unidad'
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nota).ReadOnly = Not bolModo 'Unidad'
        grdItems.Columns(ColumnasDelGridItems.Nota).Width = 250
        grdItems.Columns(ColumnasDelGridItems.Nota).Visible = True

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = False
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

        SQL = "exec spAjustes_Select_All"

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

        ' ajustar la columna del base
        '
        grd.Columns(1).Width = 60 'codigo
        grd.Columns(2).Width = 60 'fecha
        grd.Columns(3).Width = 180 'almacen
        grd.Columns(4).Width = 290 'motivo
        grd.Columns(5).Width = 150 'nota


        'ordernar descendente
        'grd.Sort(grd.Columns(1), System.ComponentModel.ListSortDirection.Descending)

        'setear grilla de items
        'With grdItems
        '    .VirtualMode = False
        '    .AllowUserToAddRows = False
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        '    .RowsDefaultCellStyle.BackColor = Color.White
        '    .AllowUserToOrderColumns = True
        '    .SelectionMode = DataGridViewSelectionMode.CellSelect

        'End With
    End Sub

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

#End Region

#Region "   Funciones"

    ' Capturar los enter del formulario, descartar todos salvo los que 
    ' se dan dentro de la grilla. Es una sobre carga de un metodo existente.
    'Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

    '    ' Si la tecla presionada es distinta de la tecla Enter,
    '    ' abandonamos el procedimiento.
    '    '
    '    If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)

    '    ' Igualmente, si el control DataGridView no tiene el foco,
    '    ' y si la celda actual no está siendo editada,
    '    ' abandonamos el procedimiento.
    '    If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then Return MyBase.ProcessCmdKey(msg, keyData)

    '    ' Obtenemos la celda actual
    '    Dim cell As DataGridViewCell = grdItems.CurrentCell
    '    ' Índice de la columna.
    '    Dim columnIndex As Int32 = cell.ColumnIndex
    '    ' Índice de la fila.
    '    Dim rowIndex As Int32 = cell.RowIndex

    '    Do
    '        If columnIndex = grdItems.Columns.Count - 1 Then
    '            If rowIndex = grdItems.Rows.Count - 1 Then
    '                ' Seleccionamos la primera columna de la primera fila.
    '                cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDAjuste_Det)
    '            Else
    '                ' Selecionamos la primera columna de la siguiente fila.
    '                cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDAjuste_Det)
    '            End If
    '        Else
    '            ' Seleccionamos la celda de la derecha de la celda actual.
    '            cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
    '        End If
    '        ' establecer la fila y la columna actual
    '        columnIndex = cell.ColumnIndex
    '        rowIndex = cell.RowIndex
    '    Loop While (cell.Visible = False)

    '    grdItems.CurrentCell = cell

    '    ' ... y la ponemos en modo de edición.
    '    grdItems.BeginEdit(True)
    '    Return True

    'End Function

    Private Function AgregarRegistroItems(ByVal idajuste As Long) As Integer
        ' Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer

        Try

            Try
                i = 0
                Do While i < grdItems.Rows.Count


                    If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is DBNull.Value) Then

                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyDif).Value <> 0 Then

                            grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyDif).Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value) - grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyActual).Value

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

                            Dim param_idajuste As New SqlClient.SqlParameter
                            param_idajuste.ParameterName = "@idajuste"
                            param_idajuste.SqlDbType = SqlDbType.BigInt
                            param_idajuste.Value = idajuste
                            param_idajuste.Direction = ParameterDirection.Input

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
                            param_qty.Scale = 2
                            param_qty.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyDif).Value
                            param_qty.Direction = ParameterDirection.Input

                            Dim param_idunidad As New SqlClient.SqlParameter
                            param_idunidad.ParameterName = "@idunidad"
                            param_idunidad.SqlDbType = SqlDbType.BigInt
                            param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
                            param_idunidad.Direction = ParameterDirection.Input

                            Dim param_nota As New SqlClient.SqlParameter
                            param_nota.ParameterName = "@nota"
                            param_nota.SqlDbType = SqlDbType.VarChar
                            param_nota.Size = 300
                            param_nota.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota).Value
                            param_nota.Direction = ParameterDirection.Input

                            Dim param_CodBarra As New SqlClient.SqlParameter
                            param_CodBarra.ParameterName = "@CodBarra"
                            param_CodBarra.SqlDbType = SqlDbType.VarChar
                            param_CodBarra.Size = 50
                            param_CodBarra.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.CodBarra).Value
                            param_CodBarra.Direction = ParameterDirection.Input

                            Dim param_Pasillo As New SqlClient.SqlParameter
                            param_Pasillo.ParameterName = "@Pasillo"
                            param_Pasillo.SqlDbType = SqlDbType.VarChar
                            param_Pasillo.Size = 50
                            param_Pasillo.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Pasillo).Value
                            param_Pasillo.Direction = ParameterDirection.Input

                            Dim param_Estante As New SqlClient.SqlParameter
                            param_Estante.ParameterName = "@Estante"
                            param_Estante.SqlDbType = SqlDbType.VarChar
                            param_Estante.Size = 50
                            param_Estante.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Estante).Value
                            param_Estante.Direction = ParameterDirection.Input

                            Dim param_Fila As New SqlClient.SqlParameter
                            param_Fila.ParameterName = "@Fila"
                            param_Fila.SqlDbType = SqlDbType.VarChar
                            param_Fila.Size = 50
                            param_Fila.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Fila).Value
                            param_Fila.Direction = ParameterDirection.Input

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
                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spAjustes_Det_Insert", param_id, _
                                                          param_codigo, param_idajuste, param_idmaterial, param_qty, param_idunidad, _
                                                          param_nota, param_CodBarra, param_Pasillo, param_Estante, _
                                                          param_Fila, param_useradd, param_res, param_msg)
                                res = param_res.Value
                                msg = param_msg.Value
                                If (res <= 0) Then
                                    Exit Do
                                End If

                            Catch ex As Exception
                                Throw ex
                            End Try

                        End If

                    End If

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

    Private Function AgregarRegistro() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

        Dim res As Integer = 0

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try


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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spAjustes_Insert", param_id, param_codigo, param_fecha, _
                                              param_nota, param_useradd, param_res)
                    txtID.Text = param_id.Value
                    txtCODIGO.Text = param_id.Value
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

        End Try
    End Function

    'Private Function ActualizarRegistro() As Integer
    '    Dim connection As SqlClient.SqlConnection = Nothing
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
    '                SqlHelper.ExecuteNonQuery(conn_del_form, CommandType.StoredProcedure, "sp_Consumos_Update", param_id, param_codigo, param_fecha, param_userupd, param_res)
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
        Dim connection As SqlClient.SqlConnection = Nothing

        Dim res As Integer = 0
        Dim msg As String
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try

                Dim param_idajuste As New SqlClient.SqlParameter("@idajuste", SqlDbType.BigInt, ParameterDirection.Input)
                param_idajuste.Value = CType(txtID.Text, Long)
                param_idajuste.Direction = ParameterDirection.Input

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spAjustes_Delete_All", param_idajuste, param_userdel, param_res, param_msg)
                    res = param_res.Value
                    msg = param_msg.Value

                    'If res > 0 Then Util.BorrarGrilla(grd)

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

    Private Function fila_vacia(ByVal i) As Boolean
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyNva).Value Is Nothing) _
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
                    Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                    res = AgregarRegistro()
                    Select Case res
                        Case -3
                            Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case -2
                            Util.MsgStatus(Status1, "No se pudo actualizar el número de consumo (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case Else
                            Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                            res_item = AgregarRegistroItems(txtID.Text)
                            Select Case res_item
                                Case -8
                                    Util.MsgStatus(Status1, "No se pudo actualizar el producto.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -6
                                    Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -5
                                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -4
                                    Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -3
                                    Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -2
                                    Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -1
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar el ajuste (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case 0
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case Else
                                    Cerrar_Tran()
                                    bolModo = False
                                    PrepararBotones()
                                    MDIPrincipal.NoActualizarBase = False
                                    btnActualizar_Click(sender, e)
                                    Setear_Grilla()
                                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap, True, True)
                            End Select
                    End Select
                    '
                    ' cerrar la conexion si está abierta.
                    '
                    If Not conn_del_form Is Nothing Then
                        CType(conn_del_form, IDisposable).Dispose()
                    End If
                Else 'If bolModo Then
                    Util.MsgStatus(Me.Status1, "Los ajustes no se pueden modificar.", My.Resources.alert.ToBitmap)
                End If ' If bolModo Then
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Caso especial, se habilitan porque se inhabilitaron por el caso de un eliminado
        chkEliminado.Checked = False

        GroupBox1.Enabled = True

        ' fin caso especial
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Util.LimpiarTextBox(Me.Controls)
        'PrepararGridItems()

        dtpFECHA.Focus()

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
        '
        Dim res As Integer

        'If BAJA Then
        If chkEliminado.Checked = False Then
            If MessageBox.Show("Esta acción reversará los ajustes de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    Case -7
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se eliminó el ajuste porque algunos items quedarían negativos.", My.Resources.stop_error.ToBitmap, True)
                    Case -6
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el ajuste.", My.Resources.stop_error.ToBitmap, True)
                    Case -5
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el detalle del ajuste.", My.Resources.stop_error.ToBitmap, True)
                    Case -4
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap, True)
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap, True)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap, True)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
                    Case Else
                        Cerrar_Tran()
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        Setear_Grilla()
                        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True, True)
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If
        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap, True)
        'End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim rpt As New frmReportes()

        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String

        nbreformreportes = "Ajustes"


        param.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)

            rpt.Ajustes_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing

            cnn = Nothing
        End If

    End Sub


#End Region


End Class