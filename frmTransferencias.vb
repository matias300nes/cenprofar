Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports Utiles.compartida
Imports ReportesNet


Public Class frmTransferencias

    Dim bolpoliticas As Boolean
    Dim editando_celda As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Auxiliares para guardar
    Dim cod_aux As String

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing


    'constantes para identificar las columnas de la grilla de los items por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDTransferencia_Det = 0
        Cod_TransferenciaDet = 1
        IDMaterial = 2
        Cod_Material = 3
        Nombre_Material = 4
        Lote = 5
        Qty = 6
        IDUnidad = 7
        Unidad = 8
        IDStock = 9
        ManejaLote = 10
    End Enum


#Region "   Eventos"

    Private Sub frmConsumos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        configurarform()

        LlenarComboAlmacenes()
        LlenarComboAlmacenes2()
        LlenarComboMateriales()

        asignarTags()

        SQL = "exec sp_Transferencias_Select_All"
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos
        Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems()
            btnNuevo_Click(sender, e)
        End If

    End Sub

    Private Sub txtID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
        If txtID.Text <> "" And bolModo = False Then
            LlenarGridItems()
        End If
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        'Cuando terminamos la edicion hay que buscar la descripcion del material y las unidad,
        'con esos datos completar la grilla. En este caso es la columna 2
        If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
            'completar la descripcion del material
            Dim cell As DataGridViewCell = grdItems.CurrentCell
            Dim codigo As String, id As Long, idalmacen As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
            Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0

            If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then Exit Sub
            codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
            cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
            If cmbALMACENORIGEN.SelectedIndex > -1 Then idalmacen = cmbALMACENORIGEN.SelectedValue Else idalmacen = -1 'cp 17/10/2011
            'If ObtenerMaterialAlmacen(codigo, id, nombre, idunidad, unidad, stock, minimo, maximo) = 0 Then
            cell.Value = nombre
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id 'cell.ColumnIndex - 2
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad 'cell.ColumnIndex + 2
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad 'cell.ColumnIndex + 3
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ManejaLote).Value = ManejaLote 'cell.ColumnIndex + 3
            'SendKeys.Send("{TAB}")
            If Not grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Lote).Value = "---"
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Lote).ReadOnly = True
                'SendKeys.Send("{TAB}")
            Else
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Lote).ReadOnly = False
            End If
            'Else
            '    cell.Value = "NO EXISTE"
            'End If
        End If

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing
        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote
        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Lote Then
            AddHandler e.Control.KeyPress, AddressOf validarNumeros
        End If

        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Qty Then
            AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
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
            'cmbALMACENES.Enabled = Not chkEliminado.Checked
            'cmbCENTROSCOSTOS.Enabled = Not chkEliminado.Checked
            'cmbGERENCIAS.Enabled = Not chkEliminado.Checked
            'cmbRetira.Enabled = Not chkEliminado.Checked
            'cmbProyectos.Enabled = Not chkEliminado.Checked
            'CheckBox1.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            'txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

#End Region


#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Maestro de Transferencias"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 200) 'AltoMinimoGrilla)
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
        cmbALMACENORIGEN.Tag = "3"
        cmbALMACENDESTINO.Tag = "4"
        txtOBSERVACIONES2.Tag = "5"
        chkEliminado.Tag = "6"
    End Sub

    Private Sub Setear_Grilla()
        grd.Columns(1).Width = 70
        grd.Columns(2).Width = 90
        grd.Columns(3).Width = 150
        grd.Columns(4).Width = 150
        grd.Columns(5).Width = 250


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
        Dim ds_Almacenes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Almacenes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre + ' ( ' + codigo + ')' as codigo FROM Almacenes WHERE Eliminado = 0 ORDER BY codigo")
            ds_Almacenes.Dispose()

            With Me.cmbALMACENORIGEN
                .DataSource = ds_Almacenes.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
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

    Private Sub LlenarComboAlmacenes2()
        Dim ds_Almacenes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try


            'ds_Almacenes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, codigo as codigo FROM Almacenes WHERE Eliminado = 0 ORDER BY codigo")
            ds_Almacenes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre + ' ( ' + codigo + ')' as codigo FROM Almacenes WHERE Eliminado = 0 ORDER BY codigo")
            ds_Almacenes.Dispose()

            With Me.cmbALMACENDESTINO
                .DataSource = ds_Almacenes.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
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

    Private Sub LlenarGridItems()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        If txtID.Text = "" Then
            SQL = "select ''as 'Id',''as 'Código',''as 'Id Material',''as 'Cod. Material',''as 'Material',''as Lote,''as 'Qty',''as 'Id Unidad',''as 'Unidad',''as 'Id Stock',convert(bit,0) as 'ManejaLote'"
        Else
            SQL = "exec sp_Transferencias_Det_Select_By_IDTransferencia @IDTransferencia = " & txtID.Text
        End If
        '        SQL = "exec sp_Transferencias_Det_Select_By_IDTransferencia @IDTransferencia = " & txtID.Text

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IDTransferencia_Det).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDTransferencia_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDTransferencia_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_TransferenciaDet).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Cod_TransferenciaDet).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Cod_TransferenciaDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False


        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 80
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 350
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Lote).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Lote).Width = 80
        grdItems.Columns(ColumnasDelGridItems.Lote).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Qty).Visible = True

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 100
        grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = True

        grdItems.Columns(ColumnasDelGridItems.IDStock).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.IDStock).Width = 100
        grdItems.Columns(ColumnasDelGridItems.IDStock).Visible = False

        grdItems.Columns(ColumnasDelGridItems.ManejaLote).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.ManejaLote).Width = 100
        grdItems.Columns(ColumnasDelGridItems.ManejaLote).Visible = False


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
        SQL = "exec sp_Transferencias_Select_All"
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

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
        With (grdItems)
            .AllowUserToAddRows = True
            .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
            .Columns(ColumnasDelGridItems.Qty).ReadOnly = False 'qty
        End With
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

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer


        'para la funcion BuscarDatosMaterialPorID...
        Dim res As Integer
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

        'If Not (cmbALMACENES.SelectedIndex > -1) Then
        '    Util.MsgStatus(Status1, "Ingrese un valor en 'Almacén'.", My.Resources.Resources.alert.ToBitmap)
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
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'qty es válida?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la cantidad a transferir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar la cantidad a transferir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                ElseIf grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value <= 0 Then
                    Util.MsgStatus(Status1, "La cantidad a Transferir debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "La cantidad a Transferir debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'unidad es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a transferir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a transferir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'el lote es un número y es válido?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Lote).Value Is System.DBNull.Value Then
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                        Util.MsgStatus(Status1, "Falta completar el número de lote en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "Falta completar el número de lote en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If
                Else
                    'Controlar que sea un numero
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                        If Not IsNumeric(grdItems.Rows(i).Cells(ColumnasDelGridItems.Lote).Value) Then
                            Util.MsgStatus(Status1, "El valor del lote debe ser numerico en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "El valor del lote debe ser numerico en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    End If
                End If


                'verificar si el material esta en inventario......
                'res = BuscarDatosMaterialPorID(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, codigo, nombre, nombrelargo, idfamilia, idunidad)
                If res = 0 Then
                    Util.MsgStatus(Status1, "Error al tratar de buscar los datos del material de la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Error al tratar de buscar los datos del material de la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                Else
                    'If eninventario Then
                    Util.MsgStatus(Status1, "El material de la fila: " & (i + 1).ToString & " esta 'En inventario'", My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El material de la fila: " & (i + 1).ToString & " esta 'En inventario'", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                    'End If
                End If



            End If
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
            txtCODIGO.Text = ""
        End If
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

                Dim param_idalmacenorigen As New SqlClient.SqlParameter
                param_idalmacenorigen.ParameterName = "@idalmacenorigen"
                param_idalmacenorigen.SqlDbType = SqlDbType.BigInt
                param_idalmacenorigen.Value = cmbALMACENORIGEN.SelectedValue
                param_idalmacenorigen.Direction = ParameterDirection.Input

                Dim param_idalmacendestino As New SqlClient.SqlParameter
                param_idalmacendestino.ParameterName = "@idalmacendestino"
                param_idalmacendestino.SqlDbType = SqlDbType.BigInt
                param_idalmacendestino.Value = cmbALMACENDESTINO.SelectedValue
                param_idalmacendestino.Direction = ParameterDirection.Input

                Dim param_observaciones As New SqlClient.SqlParameter
                param_observaciones.ParameterName = "@observaciones"
                param_observaciones.SqlDbType = SqlDbType.VarChar
                param_observaciones.Size = 200
                param_observaciones.Value = txtOBSERVACIONES2.Text
                param_observaciones.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Transferencias_Insert", param_id, param_codigo, param_fecha, param_idalmacenorigen, param_idalmacendestino, param_observaciones, param_useradd, param_res)
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

    Private Function EliminarRegistro() As Integer
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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, _
                    "sp_Transferencias_Delete_All", param_idRecepcion, param_userdel, param_res, param_msg)
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

    Private Function AgregarRegistroItems(ByVal idtransferencia As Long) As Integer
        ' Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer

        Try
            'Try
            '    connection = SqlHelper.GetConnection(conexion)
            'Catch ex As Exception
            '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Function
            'End Try

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

                    Dim param_idtransferencia As New SqlClient.SqlParameter
                    param_idtransferencia.ParameterName = "@idtransferencia"
                    param_idtransferencia.SqlDbType = SqlDbType.BigInt
                    param_idtransferencia.Value = idtransferencia
                    param_idtransferencia.Direction = ParameterDirection.Input

                    cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 4
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Decimal)
                    param_qty.Direction = ParameterDirection.Input

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

                    Dim param_idalmacenorigen As New SqlClient.SqlParameter
                    param_idalmacenorigen.ParameterName = "@idalmacen_origen"
                    param_idalmacenorigen.SqlDbType = SqlDbType.BigInt
                    param_idalmacenorigen.Value = CType(cmbALMACENORIGEN.SelectedValue, Long)
                    param_idalmacenorigen.Direction = ParameterDirection.Input

                    Dim param_Lote As New SqlClient.SqlParameter
                    param_Lote.ParameterName = "@lote"
                    param_Lote.SqlDbType = SqlDbType.BigInt
                    If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.ManejaLote).Value Then
                        param_Lote.Value = DBNull.Value
                    Else
                        param_Lote.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Lote).Value, Long)
                    End If
                    param_Lote.Direction = ParameterDirection.Input

                    Dim param_idalmacendestino As New SqlClient.SqlParameter
                    param_idalmacendestino.ParameterName = "@idalmacen_destino"
                    param_idalmacendestino.SqlDbType = SqlDbType.BigInt
                    param_idalmacendestino.Value = CType(cmbALMACENDESTINO.SelectedValue, Long)
                    param_idalmacendestino.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, _
                        "sp_Transferencias_Det_Insert", param_id, param_codigo, param_idtransferencia, _
                        param_qty, param_idmaterial, param_idunidad, param_idalmacenorigen, param_Lote, _
                        param_idalmacendestino, param_useradd, param_res, param_msg)

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDTransferencia_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDTransferencia_Det)
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
        If ALTA Then
            bolModo = True
            Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
            PrepararBotones()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Caso especial, se habilitan porque se inhabilitaron por el caso de un eliminado
            chkEliminado.Checked = False
            'cmbALMACENES.Enabled = True
            'cmbCENTROSCOSTOS.Enabled = True
            'cmbGERENCIAS.Enabled = True
            'cmbRetira.Enabled = True
            'cmbProyectos.Enabled = True
            'CheckBox1.Enabled = True
            grdItems.Enabled = True
            dtpFECHA.Enabled = True
            'txtNota.Enabled = True
            ' fin caso especial
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Util.LimpiarTextBox(Me.Controls)
            grdItems.Visible = True
            PrepararGridItems()
            dtpFECHA.Focus()
        Else
            Util.MsgStatus(Status1, "No tiene permiso para generar registros nuevos.", My.Resources.stop_error.ToBitmap)
        End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer, res_item As Integer
        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    If ALTA Then
                        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                        res = AgregarRegistro()
                        Select Case res
                            Case -3
                                Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                Cancelar_Tran()
                            Case -2
                                Util.MsgStatus(Status1, "No se pudo actualizar el número de Transferencia (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
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
                                    Case -6
                                        Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap)
                                        Cancelar_Tran()
                                    Case -5
                                        Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                        Cancelar_Tran()
                                    Case -4
                                        Util.MsgStatus(Status1, "No se pudo descontar del stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                        Cancelar_Tran()
                                    Case -3
                                        Util.MsgStatus(Status1, "No hay stock suficiente para '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap)
                                        Cancelar_Tran()
                                    Case -2
                                        Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap)
                                        Cancelar_Tran()
                                    Case -1
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar la transferencia (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                    Case 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                    Case Else
                                        Util.AgregarGrilla(grd, Me, Permitir) 'agregar el nuevo registro a la grilla
                                        Cerrar_Tran()
                                        bolModo = False
                                        PrepararBotones()
                                        btnActualizar_Click(sender, e)
                                        Setear_Grilla()
                                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
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
                    End If 'if ALTa
                Else 'If bolModo Then
                    ' Al momento de desarrollar este sistema no se permite
                    ' hacer modificacion sobre los vales
                    '
                    'If MODIFICA Then
                    '    res = ActualizarRegistro()
                    '    Select Case res
                    '        Case -3
                    '            Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo Código.", My.Resources.stop_error.ToBitmap)
                    '        Case -2
                    '            Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                    '        Case -1
                    '            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    '        Case 0
                    '            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    '        Case Else
                    '            Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                    '    End Select
                    'Else
                    'Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Me.Status1, "Las Transferencias no se pueden modificar.", My.Resources.alert.ToBitmap)
                    'End If 'If MODIFICA Then
                End If ' If bolModo Then
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
        '
        Dim res As Integer

        If BAJA Then
            If chkEliminado.Checked = False Then
                If txtOBSERVACIONES2.Text.Trim = "" Then
                    Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Por favor escriba una nota de anulación.", My.Resources.stop_error.ToBitmap, True)
                    txtOBSERVACIONES2.Focus()
                    Exit Sub
                End If

                Dim Au As New frmUsuarioModo
                Au.ShowDialog()

                If MDIPrincipal.Autorizar = False Then
                    Exit Sub
                End If
                'If MessageBox.Show("Esta acción reversará los Recepciones de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    Case -7
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se eliminó el ajuste porque algunos items quedarían negativos.", My.Resources.stop_error.ToBitmap)
                    Case -6
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el ajuste.", My.Resources.stop_error.ToBitmap)
                    Case -5
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el detalle del ajuste.", My.Resources.stop_error.ToBitmap)
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
                        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                End Select
                'Else
                '    Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
                'End If
            Else
                Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
            End If
        Else
            Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramtransferencias As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim reportetransferencias As New frmReportes

        If chkEliminado.Checked = True Then
            Util.MsgStatus(Status1, "El registro esta 'Eliminado'. No podrá imprimir.", My.Resources.stop_error.ToBitmap)
            Exit Sub
        End If

        'nbreformreportes = "Listado de Instrumentos por Código"

        paramtransferencias.AgregarParametros("Código Transferencia :", "STRING", "", False, CType(txtCODIGO.Text, Long).ToString, "", cnn)
        paramtransferencias.AgregarParametros("Fecha :", "DATE", "", False, (dtpFECHA.Text.ToString), "", cnn)
        paramtransferencias.AgregarParametros("Almacén Origen :", "STRING", "", False, (cmbALMACENORIGEN.Text.ToString), "", cnn)
        paramtransferencias.AgregarParametros("Almacén Destino :", "STRING", "", False, (cmbALMACENDESTINO.Text.ToString), "", cnn)
        paramtransferencias.AgregarParametros("Código Material :", "STRING", "", False, "", "", cnn)
        paramtransferencias.AgregarParametros("Material :", "STRING", "", False, "", "", cnn)

        paramtransferencias.ShowDialog()
        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
        If cerroparametrosconaceptar = True Then
            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR A LA FUNCION..
            codigo = paramtransferencias.ObtenerParametros(0)
            'reportetransferencias.MostrarMaestroTransferencias(codigo, reportetransferencias)
            cerroparametrosconaceptar = False
            paramtransferencias = Nothing
            cnn = Nothing
        End If

    End Sub

#End Region

End Class