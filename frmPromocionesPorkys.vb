Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmPromocionesPorkys

    Dim bolpoliticas As Boolean
    Dim datosform As Boolean
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

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDPromociones_Porkys = 0
        Orden_Item = 1
        Cod_Material = 2
        Nombre_Material = 3
        IDLista1 = 4
        lista1 = 5
        Cantidad1 = 6
        Descuento1 = 7
        IDLista2 = 8
        Lista2 = 9
        Cantidad2 = 10
        Descuento2 = 11
        Eliminado = 12

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
    Dim Costo As Double
    Dim Mayorista As Double
    Dim Revendedor As Double
    Dim Yamila As Double
    Dim Precio4 As Double
    Dim indice As Integer





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

        ToolStrip_lblCodMaterial.Visible = False
        txtBusquedaMAT.Visible = False

        band = 0

        configurarform()
        btnEliminar.Text = "Eliminar Promo"

        configurarform()
        asignarTags()

        LlenarcmbAlmacenes()
        LLenarcmbLista1()
        LLenarcmbLista2()
        Me.LlenarCombo_Productos()

        SQL = "exec spPromociones_Porkys_Select_All 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        If bolModo = True Then
            band = 1
            btnNuevo_Click(sender, e)
        Else
            LlenarGrid_Items()
        End If

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        'grd.Columns(0).Visible = False
        grd.Columns(2).Visible = False

        'ajusto las columnas dependiendo el contenido 
        grd.AutoResizeColumns()

        Contar_Filas()

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
    Handles txtDescripcion.KeyPress
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
        btnNuevo.Enabled = Not chkEliminado.Checked
        btnGuardar.Enabled = Not chkEliminado.Checked
        btnCancelar.Enabled = Not chkEliminado.Checked
        btnEliminar.Enabled = Not chkEliminado.Checked
        txtDescripcion.Enabled = Not chkEliminado.Checked
        cmbAlmacenes.Enabled = Not chkEliminado.Checked
        cmbProducto.Enabled = Not chkEliminado.Checked
        btnAgregar.Enabled = Not chkEliminado.Checked
        btnXEliminar.Enabled = Not chkEliminado.Checked
        btnModificar.Enabled = Not chkEliminado.Checked
        GroupPanelRestricciones.Enabled = Not chkEliminado.Checked

        If chkEliminado.Checked = True Then
            SQL = "exec spPromociones_Porkys_Select_All @Eliminado = 1"
        Else
            SQL = "exec spPromociones_Porkys_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()


        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminado.Checked
        End If

    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        If Permitir Then
            Try

                Contar_Filas()

                'txtID.Text = grd.CurrentRow.Cells(0).Value.ToString
                lblCodigo.Text = grd.CurrentRow.Cells(1).Value.ToString
                cmbAlmacenes.SelectedValue = grd.CurrentRow.Cells(3).Value
                txtDescripcion.Text = grd.CurrentRow.Cells(4).Value.ToString

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
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbListaPrecio1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbListaPrecio1.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDLista1.Text = cmbListaPrecio1.SelectedValue
                ElegirPrecio(True, False)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbListaPrecio2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbListaPrecio2.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDLista2.Text = cmbListaPrecio2.SelectedValue
                ElegirPrecio(False, True)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter Then
            'SendKeys.Send("{TAB}")
            txtCantidad1.Focus()
        End If
    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged
        If band = 1 Then


            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT PrecioCompra,PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4 FROM Materiales " & _
                                                                                       "where Codigo = '" & cmbProducto.SelectedValue & "'")
            ds_Producto.Dispose()

            Costo = Math.Round(ds_Producto.Tables(0).Rows(0)(0), 2)
            Mayorista = Math.Round(ds_Producto.Tables(0).Rows(0)(1), 2)
            Revendedor = Math.Round(ds_Producto.Tables(0).Rows(0)(2), 2)
            Yamila = Math.Round(ds_Producto.Tables(0).Rows(0)(3), 2)
            Precio4 = Math.Round(ds_Producto.Tables(0).Rows(0)(4), 2)
            ElegirPrecio(True, True)
            'hago foco en txtcantidad1
            txtCantidad1.Focus()

        End If
    End Sub


#End Region

#Region "   Procedimientos"


    Private Sub configurarform()
        Me.Text = "Promociones Porky's"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 85))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 75)
        Me.grd.Size = New Size(p)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            'Me.Top = ARRIBA
            'Me.Left = IZQUIERDA
            'Else
            '    Me.Top = 0
            '    Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.Top = 0
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2

    End Sub

    Private Sub asignarTags()

        txtID.Tag = "0"
        lblCodigo.Tag = "1"
        txtidAlmacen.Tag = "2"
        cmbAlmacenes.Tag = "3"
        txtDescripcion.Tag = "4"
        lblFechaAct.Tag = "5"

    End Sub

    Private Sub Verificar_Datos()
        bolpoliticas = False
        bolpoliticas = True
    End Sub

    Private Sub Verificar_Datos_Formulario()

        datosform = False

        'controlo el codio de producto
        If cmbProducto.SelectedValue Is DBNull.Value Or cmbProducto.SelectedValue = 0 Then
            Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If cmbProducto.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If txtCantidad1.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 1 no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 1 no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(txtCantidad1.Text) = 0 Then
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 1 no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 1 no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If txtCantidad2.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 2 no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 2 no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(txtCantidad2.Text) = 0 Then
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 2 no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar la cantidad 2 no puede ser igual a cero.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(txtCantidad1.Text) >= CDbl(txtCantidad2.Text) Then
            Util.MsgStatus(Status1, "La cantidad 1 no puede ser mayor o igual a la cantidad 2.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "La cantidad 1 no puede ser mayor o igual a la cantidad 2.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If txtDescuento1.Text = " " Then
            txtDescuento1.Text = "0"
        End If

        If txtDescuento2.Text = " " Then
            txtDescuento2.Text = "0"
        End If

        datosform = True

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


            If lblCodigo.Text = "" Then
                sqltxt2 = "exec spPromociones_Det_Select_By_IDPromociones @IdPromociones = '1'"
            Else
                sqltxt2 = "exec spPromociones_Det_Select_By_IDPromociones @IdPromociones = '" & lblCodigo.Text & "'"
            End If

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), dt.Rows(i)(4).ToString(), dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(), dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(), dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(), dt.Rows(i)(12).ToString())
            Next


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

    Private Sub Contar_Filas()

        lblCantidadFilas.Text = grdItems.RowCount

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

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE Eliminado = 0 ORDER BY Nombre")
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

    Private Sub validar_NumerosReales2( _
      ByVal sender As Object, _
      ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        'If columna = ColumnasDelGridItems.QtyPedido Then

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
        'End If
    End Sub

    Private Sub Imprimir_Pedido(ByVal OrdenPedido As String)
        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        'Dim codigo As String
        'Dim Solicitud As Boolean

        'nbreformreportes = "Ordenes de Compra"

        'param.AgregarParametros("Código :", "STRING", "", False, OrdenPedido, "", cnn)
        ' param.ShowDialog()

        'If cerroparametrosconaceptar = True Then

        'codigo = param.ObtenerParametros(0)

        'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)
        'If chkVentas.Checked = True Then
        'rpt.OrdenesDeCompra_Maestro_App(OrdenPedido, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
        'Else
        'rpt.DevolucionePedidosWEB_Maestro_App(OrdenPedido, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
        'End If
        cerroparametrosconaceptar = False
        param = Nothing
        cnn = Nothing
        'End If
    End Sub

    Private Sub OcultarItemsEliminados()
        'control para aquellos item que esten eliminados 
        'If rdAnuladas.Checked = False Then
        For i As Integer = 0 To grdItems.RowCount - 1
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value = True Then
                grdItems.CurrentCell = Nothing
                grdItems.Rows(i).Visible = False
            End If
        Next
        'End If
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

    Private Sub LLenarcmbLista1()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo , Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds.Dispose()

            With cmbListaPrecio1
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
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

    Private Sub LLenarcmbLista2()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo , Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds.Dispose()

            With cmbListaPrecio2
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
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

    Private Sub ElegirPrecio(ByVal desde1 As Boolean, ByVal desde2 As Boolean)
        If desde1 Then
            Select Case txtIDLista1.Text <> ""
                Case txtIDLista1.Text = "3"
                    lblPrecio1.Text = Mayorista
                Case txtIDLista1.Text = "4"
                    lblPrecio1.Text = Revendedor
                Case txtIDLista1.Text = "5"
                    lblPrecio1.Text = Yamila
                Case txtIDLista1.Text = "10"
                    lblPrecio1.Text = Costo
                Case Else
                    lblPrecio1.Text = Precio4
            End Select
        End If

        If desde2 Then
            Select Case txtIDLista2.Text <> ""
                Case txtIDLista2.Text = "3"
                    lblPrecio2.Text = Mayorista
                Case txtIDLista2.Text = "4"
                    lblPrecio2.Text = Revendedor
                Case txtIDLista2.Text = "5"
                    lblPrecio2.Text = Yamila
                Case txtIDLista2.Text = "10"
                    lblPrecio2.Text = Costo
                Case Else
                    lblPrecio2.Text = Precio4
            End Select

        End If


    End Sub

    Private Function ControlIndice() As Boolean
        'controlo repetidos y entrego el indice
        For indice = 0 To grdItems.RowCount - 1
            If cmbProducto.Text = grdItems.Rows(indice).Cells(3).Value Then
                Util.MsgStatus(Status1, "El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (indice + 1).ToString & ".", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (indice + 1).ToString & ".", My.Resources.Resources.stop_error.ToBitmap)
                ControlIndice = False
                Exit Function
            End If
        Next
        ControlIndice = True

    End Function

    'Private Sub OrdenarFilas()
    '    Try
    '        'acomodo los datos de la grilla
    '        'grdItems.Sort(grdItems.Columns(1), System.ComponentModel.ListSortDirection.Descending)
    '        grdItems.Sort(grdItems.Columns(ColumnasDelGridItems.Orden_Item), System.ComponentModel.ListSortDirection.Descending)
    '        grdItems.ClearSelection()
    '    Catch ex As Exception

    '    End Try

    'End Sub



#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro_PromoPorkys() As Integer
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

                Dim param_ordenpedido As New SqlClient.SqlParameter
                param_ordenpedido.ParameterName = "@Codigo"
                param_ordenpedido.SqlDbType = SqlDbType.VarChar
                param_ordenpedido.Size = 25
                If bolModo = True Then
                    param_ordenpedido.Value = DBNull.Value
                    param_ordenpedido.Direction = ParameterDirection.Output
                Else
                    param_ordenpedido.Value = lblCodigo.Text
                    param_ordenpedido.Direction = ParameterDirection.Input
                End If


                Dim param_idAlmacen As New SqlClient.SqlParameter
                param_idAlmacen.ParameterName = "@IDAlmacen"
                param_idAlmacen.SqlDbType = SqlDbType.BigInt
                param_idAlmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
                param_idAlmacen.Direction = ParameterDirection.Input

                Dim param_descripcion As New SqlClient.SqlParameter
                param_descripcion.ParameterName = "@Descripcion"
                param_descripcion.SqlDbType = SqlDbType.NVarChar
                param_descripcion.Size = 4000
                param_descripcion.Value = txtDescripcion.Text
                param_descripcion.Direction = ParameterDirection.Input


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

                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPromociones_Porkys_Insert", _
                                        param_id, param_ordenpedido, param_idAlmacen, param_descripcion, param_useradd, param_res)

                        lblCodigo.Text = param_ordenpedido.Value
                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPromociones_Porkys_Update", _
                                        param_id, param_ordenpedido, param_idAlmacen, param_descripcion, param_useradd, param_res)
                    End If

                    AgregarActualizar_Registro_PromoPorkys = param_res.Value


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

    Private Function AgregarRegistro_PromoPorkys_Items() As Integer
        Dim res As Integer = 0
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False

        Try
            Try
                i = 0

                Do While i < grdItems.Rows.Count


                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    If bolModo = True Then
                        param_id.Value = DBNull.Value
                        param_id.Direction = ParameterDirection.Input
                    Else
                        param_id.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDPromociones_Porkys).Value
                        param_id.Direction = ParameterDirection.Input
                    End If

                    Dim param_idpedidosweb As New SqlClient.SqlParameter
                    param_idpedidosweb.ParameterName = "@IDPROMOCIONESPORKYS"
                    param_idpedidosweb.SqlDbType = SqlDbType.VarChar
                    param_idpedidosweb.Size = 25
                    param_idpedidosweb.Value = lblCodigo.Text 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
                    param_idpedidosweb.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@IDMATERIAL"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                    param_idmaterial.Direction = ParameterDirection.Input

                    Dim param_idlista1 As New SqlClient.SqlParameter
                    param_idlista1.ParameterName = "@IDLISTA1"
                    param_idlista1.SqlDbType = SqlDbType.BigInt
                    param_idlista1.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDLista1).Value
                    param_idlista1.Direction = ParameterDirection.Input

                    Dim param_cantidad1 As New SqlClient.SqlParameter
                    param_cantidad1.ParameterName = "@CANTIDAD1"
                    param_cantidad1.SqlDbType = SqlDbType.Decimal
                    param_cantidad1.Precision = 18
                    param_cantidad1.Scale = 2
                    param_cantidad1.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad1).Value
                    param_cantidad1.Direction = ParameterDirection.Input

                    Dim param_descuento1 As New SqlClient.SqlParameter
                    param_descuento1.ParameterName = "@DESCUENTO1"
                    param_descuento1.SqlDbType = SqlDbType.Decimal
                    param_descuento1.Precision = 18
                    param_descuento1.Scale = 2
                    param_descuento1.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento1).Value
                    param_descuento1.Direction = ParameterDirection.Input

                    Dim param_idlista2 As New SqlClient.SqlParameter
                    param_idlista2.ParameterName = "@IDLISTA2"
                    param_idlista2.SqlDbType = SqlDbType.BigInt
                    param_idlista2.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDLista2).Value
                    param_idlista2.Direction = ParameterDirection.Input

                    Dim param_cantidad2 As New SqlClient.SqlParameter
                    param_cantidad2.ParameterName = "@CANTIDAD2"
                    param_cantidad2.SqlDbType = SqlDbType.Decimal
                    param_cantidad2.Precision = 18
                    param_cantidad2.Scale = 2
                    param_cantidad2.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad2).Value
                    param_cantidad1.Direction = ParameterDirection.Input

                    Dim param_descuento2 As New SqlClient.SqlParameter
                    param_descuento2.ParameterName = "@DESCUENTO2"
                    param_descuento2.SqlDbType = SqlDbType.Decimal
                    param_descuento2.Precision = 18
                    param_descuento2.Scale = 2
                    param_descuento2.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento2).Value
                    param_descuento2.Direction = ParameterDirection.Input

                    Dim param_ordenitem As New SqlClient.SqlParameter
                    param_ordenitem.ParameterName = "@ORDENITEM"
                    param_ordenitem.SqlDbType = SqlDbType.SmallInt
                    param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
                    param_ordenitem.Direction = ParameterDirection.Input


                    Dim param_eliminado As New SqlClient.SqlParameter
                    param_eliminado.ParameterName = "@Eliminado"
                    param_eliminado.SqlDbType = SqlDbType.Bit
                    param_eliminado.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value
                    param_eliminado.Direction = ParameterDirection.Input

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

                        If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPromociones_Porkys_Det_Insert", _
                                               param_id, param_idpedidosweb, param_idmaterial, param_idlista1, param_cantidad1, param_descuento1, _
                                               param_idlista2, param_cantidad2, param_descuento2, param_ordenitem, param_useradd, param_res)
                        Else
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPromociones_Porkys_Det_Update", _
                                                     param_id, param_idpedidosweb, param_idmaterial, param_idlista1, param_cantidad1, param_descuento1, _
                                                     param_idlista2, param_cantidad2, param_descuento2, param_ordenitem, param_eliminado, param_useradd, param_res)
                        End If


                        'MsgBox(param_msg.Value.ToString)

                        res = param_res.Value

                        If (res <= 0) Then
                            Exit Do
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try



                    i = i + 1

                Loop

                AgregarRegistro_PromoPorkys_Items = res

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

    Private Function EliminarRegistro_PromoPorkys() As Integer
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

                Dim param_ordenpedido As New SqlClient.SqlParameter
                param_ordenpedido.ParameterName = "@Codigo"
                param_ordenpedido.SqlDbType = SqlDbType.VarChar
                param_ordenpedido.Size = 25
                param_ordenpedido.Value = lblCodigo.Text
                param_ordenpedido.Direction = ParameterDirection.Input

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPromociones_Porkys_Delete", param_ordenpedido, param_userdel, param_res)
                    res = param_res.Value

                    EliminarRegistro_PromoPorkys = res

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

    Private Function ActivarRegistro_PromoPorkys() As Integer
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

                Dim param_ordenpedido As New SqlClient.SqlParameter
                param_ordenpedido.ParameterName = "@Codigo"
                param_ordenpedido.SqlDbType = SqlDbType.VarChar
                param_ordenpedido.Size = 25
                param_ordenpedido.Value = lblCodigo.Text
                param_ordenpedido.Direction = ParameterDirection.Input

                Dim param_userdel As New SqlClient.SqlParameter
                param_userdel.ParameterName = "@userupd"
                param_userdel.SqlDbType = SqlDbType.Int
                param_userdel.Value = UserID
                param_userdel.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPromociones_Porkys_Activar", param_ordenpedido, param_userdel, param_res)
                    res = param_res.Value

                    ActivarRegistro_PromoPorkys = res

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

    'Private Function EliminarRegistro_PromoPorkys_Items() As Integer
    '    Dim res As Integer = 0
    '    Dim msg As String
    '    Dim i As Integer
    '    Dim ActualizarPrecio As Boolean = False

    '    Dim ValorActual As Double
    '    Dim IdStockMov As Long
    '    Dim Stock As Double


    '    Dim Comprob As String

    '    Try
    '        Try
    '            i = 0

    '            Do While i < grdItems.Rows.Count


    '                Dim param_id As New SqlClient.SqlParameter
    '                param_id.ParameterName = "@id"
    '                param_id.SqlDbType = SqlDbType.BigInt
    '                param_id.Value = DBNull.Value
    '                param_id.Direction = ParameterDirection.InputOutput

    '                Dim param_idpedidosweb As New SqlClient.SqlParameter
    '                param_idpedidosweb.ParameterName = "@IDPEDIDOSWEB"
    '                param_idpedidosweb.SqlDbType = SqlDbType.VarChar
    '                param_idpedidosweb.Size = 25
    '                param_idpedidosweb.Value = lblCodigo.Text 'IIf(txtID.Text = "", cmbPedidos.SelectedValue, txtID.Text)
    '                param_idpedidosweb.Direction = ParameterDirection.Input

    '                Dim param_idAlmacen As New SqlClient.SqlParameter
    '                param_idAlmacen.ParameterName = "@IDALMACEN"
    '                param_idAlmacen.SqlDbType = SqlDbType.BigInt
    '                param_idAlmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
    '                param_idAlmacen.Direction = ParameterDirection.Input

    '                Dim param_idmaterial As New SqlClient.SqlParameter
    '                param_idmaterial.ParameterName = "@IDMATERIAL"
    '                param_idmaterial.SqlDbType = SqlDbType.VarChar
    '                param_idmaterial.Size = 25
    '                param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
    '                param_idmaterial.Direction = ParameterDirection.Input

    '                'Dim param_marca As New SqlClient.SqlParameter
    '                'param_marca.ParameterName = "@IDMARCA"
    '                'param_marca.SqlDbType = SqlDbType.VarChar
    '                'param_marca.Size = 25
    '                'param_marca.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value Is DBNull.Value, " ", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Marca).Value)
    '                'param_marca.Direction = ParameterDirection.Input

    '                Dim param_idunidad As New SqlClient.SqlParameter
    '                param_idunidad.ParameterName = "@IDUNIDAD"
    '                param_idunidad.SqlDbType = SqlDbType.VarChar
    '                param_idunidad.Size = 25
    '                param_idunidad.Value = 0 'IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is DBNull.Value, "U", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value)
    '                param_idunidad.Direction = ParameterDirection.Input

    '                'Dim param_precio As New SqlClient.SqlParameter
    '                'param_precio.ParameterName = "@PRECIO"
    '                'param_precio.SqlDbType = SqlDbType.Decimal
    '                'param_precio.Precision = 18
    '                'param_precio.Scale = 2
    '                ''If grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("TIRA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Unidad).Value.ToString.Contains("HORMA") Then
    '                ''param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value) * ((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value) / grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value)
    '                ''Else
    '                'param_precio.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value)
    '                ''End If
    '                'param_precio.Direction = ParameterDirection.Input


    '                Dim param_qtyenviada As New SqlClient.SqlParameter
    '                param_qtyenviada.ParameterName = "@QTYENVIADA"
    '                param_qtyenviada.SqlDbType = SqlDbType.Decimal
    '                param_qtyenviada.Precision = 18
    '                param_qtyenviada.Scale = 2
    '                param_qtyenviada.Value = 0 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value, Decimal)
    '                param_qtyenviada.Direction = ParameterDirection.Input

    '                'Dim param_subtotal As New SqlClient.SqlParameter
    '                'param_subtotal.ParameterName = "@SUBTOTAL"
    '                'param_subtotal.SqlDbType = SqlDbType.Decimal
    '                'param_subtotal.Precision = 18
    '                'param_subtotal.Scale = 2
    '                'param_subtotal.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value)
    '                'param_subtotal.Direction = ParameterDirection.Input

    '                'Dim param_iva As New SqlClient.SqlParameter
    '                'param_iva.ParameterName = "@IVA"
    '                'param_iva.SqlDbType = SqlDbType.Decimal
    '                'param_iva.Precision = 18
    '                'param_iva.Scale = 2
    '                'param_iva.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value)
    '                'param_iva.Direction = ParameterDirection.Input

    '                Dim param_notadet As New SqlClient.SqlParameter
    '                param_notadet.ParameterName = "@NOTA_DET"
    '                param_notadet.SqlDbType = SqlDbType.VarChar
    '                param_notadet.Size = 300
    '                param_notadet.Value = 0 'grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value
    '                param_notadet.Direction = ParameterDirection.Input

    '                'Dim param_ordenitem As New SqlClient.SqlParameter
    '                'param_ordenitem.ParameterName = "@ORDENITEM"
    '                'param_ordenitem.SqlDbType = SqlDbType.SmallInt
    '                'param_ordenitem.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value
    '                'param_ordenitem.Direction = ParameterDirection.Input

    '                'Dim param_fechacumplido As New SqlClient.SqlParameter
    '                'param_fechacumplido.ParameterName = "@FECHACUMPLIDO"
    '                'param_fechacumplido.SqlDbType = SqlDbType.DateTime
    '                'param_fechacumplido.Value = dtpFECHA.Value
    '                'param_fechacumplido.Direction = ParameterDirection.Input

    '                'Dim param_UnidadFac As New SqlClient.SqlParameter
    '                'param_UnidadFac.ParameterName = "@UnidadFac"
    '                'param_UnidadFac.SqlDbType = SqlDbType.Decimal
    '                'param_UnidadFac.Precision = 18
    '                'param_UnidadFac.Scale = 2
    '                'param_UnidadFac.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value)
    '                'param_UnidadFac.Direction = ParameterDirection.Input

    '                Dim param_reinstock As New SqlClient.SqlParameter
    '                param_reinstock.ParameterName = "@REINTEGRAR_STOCK"
    '                param_reinstock.SqlDbType = SqlDbType.Bit
    '                param_reinstock.Value = 0 'grdItems.Rows(i).Cells(ColumnasDelGridItems.Reintegrar_Stock).Value
    '                param_reinstock.Direction = ParameterDirection.Input

    '                Dim param_useradd As New SqlClient.SqlParameter
    '                param_useradd.ParameterName = "@userdel"
    '                param_useradd.SqlDbType = SqlDbType.Int
    '                param_useradd.Value = UserID
    '                param_useradd.Direction = ParameterDirection.Input

    '                '---------------------------------------agregue--------------------------------------------'
    '                Dim param_IdStockMov As New SqlClient.SqlParameter
    '                param_IdStockMov.ParameterName = "@IdStockMov"
    '                param_IdStockMov.SqlDbType = SqlDbType.Int
    '                param_IdStockMov.Value = DBNull.Value
    '                param_IdStockMov.Direction = ParameterDirection.InputOutput

    '                Dim param_Comprob As New SqlClient.SqlParameter
    '                param_Comprob.ParameterName = "@Comprob"
    '                param_Comprob.SqlDbType = SqlDbType.VarChar
    '                param_Comprob.Size = 50
    '                param_Comprob.Value = DBNull.Value
    '                param_Comprob.Direction = ParameterDirection.InputOutput

    '                Dim param_Stock As New SqlClient.SqlParameter
    '                param_Stock.ParameterName = "@Stock"
    '                param_Stock.SqlDbType = SqlDbType.Decimal
    '                param_Stock.Precision = 18
    '                param_Stock.Scale = 2
    '                param_Stock.Value = DBNull.Value
    '                param_Stock.Direction = ParameterDirection.InputOutput

    '                'Dim param_valorDescuento As New SqlClient.SqlParameter
    '                'param_valorDescuento.ParameterName = "@DESCUENTO"
    '                'param_valorDescuento.SqlDbType = SqlDbType.Decimal
    '                'param_valorDescuento.Precision = 18
    '                'param_valorDescuento.Scale = 2
    '                'param_valorDescuento.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value)
    '                'param_valorDescuento.Direction = ParameterDirection.Input

    '                'Dim param_control As New SqlClient.SqlParameter
    '                'param_control.ParameterName = "@CONTROL"
    '                'param_control.SqlDbType = SqlDbType.SmallInt
    '                'param_control.Value = CONTROL
    '                'param_control.Direction = ParameterDirection.Input
    '                '------------------------------------agregue-----------------------------------------'
    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@res"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = DBNull.Value
    '                param_res.Direction = ParameterDirection.InputOutput

    '                Dim param_msg As New SqlClient.SqlParameter
    '                param_msg.ParameterName = "@mensaje"
    '                param_msg.SqlDbType = SqlDbType.VarChar
    '                param_msg.Size = 150
    '                param_msg.Value = DBNull.Value
    '                param_msg.Direction = ParameterDirection.InputOutput

    '                Try

    '                    'If chkVentas.Checked = True Then
    '                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPedidosWEB_Delete_Det", _
    '                                       param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
    '                                       param_idunidad, param_qtyenviada, _
    '                                       param_notadet, param_useradd, _
    '                                       param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)
    '                    'Else
    '                    '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spDevoluciones_PedidosWEB_Delete_Det", _
    '                    '                       param_id, param_idpedidosweb, param_idAlmacen, param_idmaterial, _
    '                    '                       param_idunidad, param_qtyenviada, param_reinstock, _
    '                    '                       param_notadet, param_useradd, _
    '                    '                       param_IdStockMov, param_Comprob, param_Stock, param_res, param_msg)
    '                    'End If


    '                    'MsgBox(param_msg.Value.ToString)

    '                    res = param_res.Value
    '                    Comprob = param_Comprob.Value
    '                    Stock = param_Stock.Value
    '                    IdStockMov = param_IdStockMov.Value

    '                    If Not (param_msg.Value Is System.DBNull.Value) Then
    '                        msg = param_msg.Value
    '                    Else
    '                        msg = ""
    '                    End If
    '                    If (res <= 0) Then
    '                        Exit Do
    '                    End If

    '                Catch ex As Exception
    '                    Throw ex
    '                End Try

    '                i = i + 1

    '            Loop

    '            EliminarRegistro_PromoPorkys_Items = res

    '        Catch ex2 As Exception
    '            Throw ex2
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

    Private Function fila_vacia(ByVal i) As Boolean
        'If (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is Nothing) _
        '            And (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyEnviada).Value Is Nothing) _
        '            And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Unidad).Value Is Nothing) Then
        '    fila_vacia = True
        'Else
        '    fila_vacia = False
        'End If
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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDPromociones_Porkys)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDPromociones_Porkys)
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
                param_idordendecompra.Value = lblCodigo.Text
                param_idordendecompra.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@NOTA"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtDescripcion.Text
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

                            sqlstring = "exec spPedidosWEB_Delete_Finalizar '" & lblCodigo.Text & "','" & txtDescripcion.Text & "'," & UserID & ""

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

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        lblCodigo.Text = ""
        lblItemActual.Text = "0"
        cmbAlmacenes.SelectedValue = 1
        cmbListaPrecio1.SelectedValue = 3
        cmbListaPrecio2.SelectedValue = 4
        grdItems.Rows.Clear()
        lblCodigo.Focus()
        SendKeys.Send("{TAB}")


    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If bolModo = False Then
            If MessageBox.Show("¿Está seguro que desea modificar la Promoción seleccionada ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim res As Integer, res_item As Integer

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro_PromoPorkys()
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
                        res_item = AgregarRegistro_PromoPorkys_Items()
                        Select Case res_item
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
                                Cerrar_Tran()

                                'Imprimir_Pedido(lblCodigo.Text)



                                bolModo = False
                                PrepararBotones()

                                SQL = "exec spPromociones_Porkys_Select_All 0"

                                MDIPrincipal.NoActualizarBase = False
                                btnActualizar_Click(sender, e)

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

        If chkEliminado.Checked = False Then
            If MessageBox.Show("Esta acción eliminara el registro." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro_PromoPorkys()
                Select Case res

                    Case Is < 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo eliminar la promoción.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo eliminar la promoción.", My.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo eliminar la promoción.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo eliminar la promoción.", My.Resources.stop_error.ToBitmap, True)
                    Case Else

                        Cerrar_Tran()
                        PrepararBotones()

                        bolModo = False
                        PrepararBotones()

                        SQL = "exec spPromociones_Porkys_Select_All 0"

                        MDIPrincipal.NoActualizarBase = False
                        btnActualizar_Click(sender, e)

                        Setear_Grilla()

                        Util.MsgStatus(Status1, "Se ha eliminado la promoción.", My.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se ha eliminado la promoción.", My.Resources.ok.ToBitmap, True, True)
                        'End Select
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If

    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim res As Integer

        res = ActivarRegistro_PromoPorkys()
        Select Case res

            Case Is < 0
                Cancelar_Tran()
                Util.MsgStatus(Status1, "No se pudo activar la promoción.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se pudo activar la promoción.", My.Resources.stop_error.ToBitmap, True)
            Case 0
                Cancelar_Tran()
                Util.MsgStatus(Status1, "No se pudo activar la promoción.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se pudo activar la promoción.", My.Resources.stop_error.ToBitmap, True)
            Case Else

                Cerrar_Tran()
                PrepararBotones()

                bolModo = False
                PrepararBotones()

                SQL = "exec spPromociones_Porkys_Select_All 1"

                MDIPrincipal.NoActualizarBase = False
                btnActualizar_Click(sender, e)

                Setear_Grilla()

                Util.MsgStatus(Status1, "Se ha activado la promoción.", My.Resources.ok.ToBitmap)
                Util.MsgStatus(Status1, "Se ha activado la promoción.", My.Resources.ok.ToBitmap, True, True)
                'End Select
        End Select

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        'Dim Solicitud As Boolean

        Try

        Catch ex As Exception

        End Try

        nbreformreportes = "Ordenes de Compra"

        param.AgregarParametros("Código :", "STRING", "", False, lblCodigo.Text.ToString, "", cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)
            'rpt.OrdenesDeCompra_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)
            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If



    End Sub

    Private Sub btnLlenarGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlenarGrilla.Click
        'Dim i As Integer

        'If bolModo Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
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
        'End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        chkMantenerDatos1.Checked = False
        chkMantenerDatos2.Checked = False
        Try
            LlenarGrid_Items()
        Catch ex As Exception

        End Try
        grd_CurrentCellChanged(sender, e)
        grdItems_CurrentCellChanged(sender, e)
        bolModo = False

    End Sub

    'Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
    'End Sub

    'Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
    '    'Try
    '    '    If cmbPedidos.SelectedValue.ToString <> "" Then
    '    '        DevolverEstado()
    '    '    End If
    '    'Catch ex As Exception

    '    'End Try

    'End Sub

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

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        'verifico los datos y controlo que no vaya a cargar un producto repetido
        Verificar_Datos_Formulario()
        If datosform = False Or ControlIndice() = False Then
            Exit Sub
        End If

        grdItems.Rows.Add(0, indice + 1, cmbProducto.SelectedValue, cmbProducto.Text, IIf(txtIDLista1.Text = "", cmbListaPrecio1.SelectedValue, txtIDLista1.Text), cmbListaPrecio1.Text, txtCantidad1.Text, txtDescuento1.Text, IIf(txtIDLista2.Text = "", cmbListaPrecio2.SelectedValue, txtIDLista2.Text), cmbListaPrecio2.Text, txtCantidad2.Text, txtDescuento2.Text)

        Contar_Filas()

        ' OrdenarFilas()

        cmbProducto.Text = ""
        If chkMantenerDatos1.Checked = False Then
            cmbListaPrecio1.SelectedValue = 3
            txtCantidad1.Text = ""
            txtDescuento1.Text = ""
        End If
        If chkMantenerDatos2.Checked = False Then
            cmbListaPrecio2.SelectedValue = 4
            txtCantidad2.Text = ""
            txtDescuento2.Text = ""
        End If
        lblPrecio1.Text = "0.00"
        lblPrecio2.Text = "0.00"
        cmbProducto.Focus()
        'SendKeys.Send("{TAB}")


    End Sub

    Private Sub btnXEliminar_Click_1(sender As Object, e As EventArgs) Handles btnXEliminar.Click

        'controlo que la promo al menos contenga un solo items
        If grdItems.Rows.Count > 1 Then
            'If MessageBox.Show("Está seguro que desea eliminar el ítem seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Util.MsgStatus(Status1, "Haga click en [Guardar] despues para confirmar la eliminación del Ítem de forma permanente.")
            'coloco en eliminado = true en la fila 
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.Eliminado).Value = 1
            'oculto la fila eliminado
            grdItems.CurrentRow.Visible = False
            'calculo de nuevo el orden de los item
            Dim contador As Integer = 1
            For indice = 0 To grdItems.Rows.Count - 1
                If grdItems.Rows(indice).Visible = True Then
                    grdItems.Rows(indice).Cells(ColumnasDelGridItems.Orden_Item).Value = contador
                    contador = contador + 1
                End If
            Next
            'End If
        Else
            If MessageBox.Show(" No se puede eliminar todos los ítems de una promoción ¿Desea Eliminar la promoción completamente?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                btnEliminar_Click(sender, e)
            End If
        End If


    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

        Util.MsgStatus(Status1, "Haga click en [Guardar] despues para confirmar la modificación de los datos.")

        Verificar_Datos_Formulario()
        If datosform = False Then
            Exit Sub
        End If

        Dim i As Integer
        'modifico los datos del items seleccionado
        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value = cmbProducto.SelectedValue
        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Nombre_Material).Value = cmbProducto.Text
        'me fijo si mantiene los datos 
        If chkMantenerDatos1.Checked = False Then
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.IDLista1).Value = cmbListaPrecio1.SelectedValue
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.lista1).Value = cmbListaPrecio1.Text.ToUpper
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cantidad1).Value = FormatNumber(txtCantidad1.Text, 2)
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.Descuento2).Value = FormatNumber(txtDescuento1.Text, 2)
        Else
            For i = 0 To grdItems.Rows.Count - 1
                grdItems.Rows(i).Cells(ColumnasDelGridItems.IDLista1).Value = cmbListaPrecio1.SelectedValue
                grdItems.Rows(i).Cells(ColumnasDelGridItems.lista1).Value = cmbListaPrecio1.Text.ToUpper
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad1).Value = FormatNumber(txtCantidad1.Text, 2)
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento2).Value = FormatNumber(txtDescuento1.Text, 2)
            Next
        End If

        If chkMantenerDatos2.Checked = False Then
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.IDLista2).Value = cmbListaPrecio2.SelectedValue
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.Lista2).Value = cmbListaPrecio2.Text.ToUpper
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cantidad2).Value = FormatNumber(txtCantidad2.Text, 2)
            grdItems.CurrentRow.Cells(ColumnasDelGridItems.Descuento2).Value = FormatNumber(txtDescuento2.Text, 2)
        Else
            For i = 0 To grdItems.Rows.Count - 1
                grdItems.Rows(i).Cells(ColumnasDelGridItems.IDLista2).Value = cmbListaPrecio2.SelectedValue
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Lista2).Value = cmbListaPrecio2.Text.ToUpper
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad2).Value = FormatNumber(txtCantidad2.Text, 2)
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento2).Value = FormatNumber(txtDescuento2.Text, 2)
            Next
        End If


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
            '    End If

            'End If



        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        'If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.QtyPedido Then
        '    AddHandler e.Control.KeyPress, AddressOf validar_NumerosReales2
        'End If

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

    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        If e.ColumnIndex = 23 And bolModo = True Then 'Marcar llegada
            If MessageBox.Show("Está seguro que desea eliminar el producto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItems.Rows.RemoveAt(e.RowIndex)
                'me fijo si en la grilla hay algo y cambio el orden de los item
                If grdItems.Rows.Count > 0 Then
                    For i As Integer = 0 To grdItems.Rows.Count - 1
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Orden_Item).Value = i + 1
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub grdItems_CurrentCellChanged(sender As Object, e As EventArgs) Handles grdItems.CurrentCellChanged
        If Permitir Then
            Try

                'Contar_Filas()

                lblItemActual.Text = grdItems.CurrentRow.Cells(1).Value.ToString
                cmbProducto.SelectedValue = grdItems.CurrentRow.Cells(2).Value
                If chkMantenerDatos1.Checked = False Then
                    cmbListaPrecio1.SelectedValue = grdItems.CurrentRow.Cells(4).Value
                    txtCantidad1.Text = grdItems.CurrentRow.Cells(6).Value
                    txtDescuento1.Text = grdItems.CurrentRow.Cells(7).Value
                End If
                If chkMantenerDatos2.Checked = False Then
                    cmbListaPrecio2.SelectedValue = grdItems.CurrentRow.Cells(8).Value
                    txtCantidad2.Text = grdItems.CurrentRow.Cells(10).Value
                    txtDescuento2.Text = grdItems.CurrentRow.Cells(11).Value
                End If


            Catch ex As Exception

            End Try
        End If

    End Sub



#End Region





End Class
