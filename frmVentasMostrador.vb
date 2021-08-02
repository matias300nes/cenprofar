
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient



Public Class frmVentasMostrador
    Dim bolpoliticas As Boolean

    Dim band As Integer

    Public IdVendedor As String
    Public NombreVendedor As String

    'Dim ValorListaPrecio As Double

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

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim ds_Empresa As Data.DataSet

    Private comm As New CommManager

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        Id = 0
        Codigo = 1
        Nombre_Material = 2
        Cantidad = 3
        Peso = 4
        Precio = 5
        Subtotal = 6
        Desc_Unit = 7
        Desc_Total = 8
        SubtotalFinal = 9
        IDMaterial = 10
        IDUnidad = 11

    End Enum


    Public producto_unitario As String
    Public idproducto_unitario As String
    Public stock_unitario As String
    Public unidad_unitario As String
    Public almacen_unitario As String
    Dim ValorNorte_cambio As Double = 0.0
    Dim DescLista As String = ""
    'Public actualizarstock As Boolean

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim CodBarra_Activado As Boolean = False

#Region "   Eventos"

    Private Sub frmVentasMostrador_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            comm.ClosePort()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmVentasMostrador_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            comm.ClosePort()

        Catch ex As Exception

        End Try

    End Sub

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

    Private Sub frmVentas_Peron_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)

        band = 0

        'If MDIPrincipal.sucursal.Contains("PERON") Then
        '    cmbCliente.DropDownStyle = ComboBoxStyle.DropDown
        'Else
        '    cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList
        'End If

        'ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Valor_Cambio FROM Lista_Precios WHERE Descripcion = 'Revendedor'")
        'ds_Empresa.Dispose()

        'ValorListaPrecio = ds_Empresa.Tables(0).Rows(0)(0)

        btnEliminar.Text = "Anular Venta"
        grd.Visible = False

        configurarform()
        asignarTags()

        'LlenarcmbClientes_App(cmbCliente, ConnStringSEI)
        LlenarcmbClientes()
        'llenarcmbRepartidor()

        Me.LlenarCombo_Productos()


        SQL = "exec [spVentas_Mostrador_Select_ALL] @Eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos
        chkPrecioMayorista.Enabled = False

        'If bolModo = True Then
        '    btnNuevo_Click(sender, e)
        'End If

        'If grd.RowCount > 0 Then
        '    grd.Rows(0).Selected = True
        '    grd.CurrentCell = grd.Rows(0).Cells(1)

        '    LlenarGridItems()

        '    txtDescuento.Text = grd.CurrentRow.Cells(6).Value
        '    lblFacturado.Text = grd.CurrentRow.Cells(12).Value
        '    lblContado.Text = grd.CurrentRow.Cells(13).Value
        '    lblCtaCte.Text = grd.CurrentRow.Cells(14).Value
        'End If

        'grd.Columns(9).Visible = False
        'grd.Columns(10).Visible = False
        'grd.Columns(11).Visible = False
        'grd.Columns(12).Visible = False
        'grd.Columns(13).Visible = False
        'grd.Columns(14).Visible = False

        band = 1

        dtpFECHA.MaxDate = Today.Date
        chkEliminado.Visible = False
        btnEliminar.Enabled = False
        '-----------------------------------------
        'comm.PortName = "COM3"
        'comm.BaudRate = "9600"
        'comm.Parity = "None"
        'comm.StopBits = "One"
        'comm.DataBits = "8"
        'comm.DisplayWindow = txtLectura
        'comm.CurrentTransmissionType = CommManager.TransmissionType.Hex

        'If comm.OpenPort() Then
        '    'MsgBox("Puerto: " & comm.PortName & ", Velocidad: " & comm.BaudRate.ToString & ", Paridad: " & comm.Parity & ", Bit Stop: " & comm.StopBits & ", Bit Data: " & comm.DataBits)
        'End If
        '------------------------------------------------
        btnNuevo_Click(sender, e)


        txtCodigoBarra.Focus()
        SendKeys.Send("{TAB}")

        'frmLoginVendedor.MdiParent = MDIPrincipal
        'frmLoginVendedor.ShowDialog()

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress, _
        txtObservaciones.KeyPress, txtCantidad.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbEquipo_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter And bolModo = True Then
            SendKeys.Send("{TAB}")
            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
                Timer1.Enabled = True
                txtCantidad.Text = 1
                txtCantidad.Enabled = False
                txtPeso.Enabled = True
                Exit Sub
            Else
                Timer1.Enabled = False
                txtPeso.Text = 0
                txtPeso.Enabled = False
                txtCantidad.Text = ""
                txtCantidad.Enabled = True
            End If
        End If
    End Sub

    Private Sub cmbOrigen_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCliente.KeyDown
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFECHA.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If bolModo = False Then
            btnGuardar.Enabled = Not chkEliminado.Checked
            btnNuevo.Enabled = Not chkEliminado.Checked
            btnCancelar.Enabled = Not chkEliminado.Checked

            If chkEliminado.Checked = True Then

                btnEliminar.Enabled = False
                btnActivar.Enabled = True

                SQL = "exec [spVentas_Mostrador_Select_ALL] @Eliminado = 1"

            Else
                btnEliminar.Enabled = True

                SQL = "exec [spVentas_Mostrador_Select_ALL] @Eliminado = 0"


            End If

            LlenarGrilla()


            If grd.Rows.Count > 0 Then
                grd.Rows(0).Selected = True
            Else
                grdItems.Rows.Clear()
            End If
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

            If txtCantidad.Text = "" Or txtCantidad.Text = "0" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
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

            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
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
            Else
                txtPeso.Text = "0"
            End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbProducto.SelectedValue = grdItems.Rows(i).Cells(10).Value Then
                    Util.MsgStatus(Status1, "El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (i + 1).ToString & ".", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            Next

            grdItems.Rows.Add(0, txtCodigoBarra.Text, cmbProducto.Text, txtCantidad.Text, txtPeso.Text, txtPrecioVta.Text, txtSubtotal.Text, 0, 0, txtSubtotal.Text, cmbProducto.SelectedValue, txtIdUnidad.Text, "Eliminado")

            CalcularSubtotal()


            Timer1.Enabled = False
            txtCantidad.Text = ""
            txtPeso.Text = ""
            cmbProducto.Text = ""
            txtPrecioVta.Text = "0.00"
            lblStock.Text = "0.00"
            txtSubtotal.Text = "0.00"
            chkPrecioMayorista.Checked = False
            'Continuar:
            'cmbProducto.SelectedIndex = 0
            txtCodigoBarra.Focus()
            SendKeys.Send("{TAB}")



        End If

    End Sub

    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        If e.ColumnIndex = 12 Then 'Marcar llegada
            If MessageBox.Show("Está seguro que desea eliminar el producto de la venta?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItems.Rows.RemoveAt(e.RowIndex)
                CalcularSubtotal()
            End If
        End If
    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales2

    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            Dim valorcambio As Double = 0
            Dim ganancia As Double = 0, preciovta As Double = 0
            Dim idmoneda As Long = 0
            Dim nombre As String = "", codmoneda As String = ""

            If e.ColumnIndex = 7 Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell

                If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
                    Exit Sub
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = "HORMA" Or grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = "TIRA" Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Desc_Total).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Desc_Unit).Value
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalFinal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Desc_Total).Value
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Desc_Total).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Desc_Unit).Value
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalFinal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Desc_Total).Value
                End If


                CalcularSubtotal()

            End If

            SendKeys.Send("{TAB}")

        Catch ex As Exception
            ' MsgBox(ex.Message)
            'MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    'CurrentCellChanged
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        If Permitir Then
            Try

                txtDescuento.Text = grd.CurrentRow.Cells(6).Value
                lblFacturado.Text = grd.CurrentRow.Cells(12).Value
                lblContado.Text = grd.CurrentRow.Cells(13).Value
                lblCtaCte.Text = grd.CurrentRow.Cells(14).Value

                LlenarGridItems()
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If

    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Ventas"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
            Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))
        End If

        'Me.WindowState = FormWindowState.Maximized

        'Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 100)
        Me.grd.Size = New Size(GroupBox1.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbCliente.Tag = "3"
        lblVendedor.Tag = "4"
        txtSubtotalVenta.Tag = "5"
        txtDescuento.Tag = "6"
        txtTotalVenta.Tag = "7"
        txtObservaciones.Tag = "8"
        chkDescuento.Tag = "9"
        rdPorcentaje.Tag = "10"
        rdAbsoluto.Tag = "11"
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

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales m JOIN Marcas ma ON m.idmarca = ma.codigo WHERE m.eliminado = 0 ")
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

    Private Sub Verificar_Datos()

        bolpoliticas = False

        'Dim i As Integer
        'For i = 0 To grdItems.RowCount - 1
        '    Dim j As Integer
        '    Dim NombreEquipo As String = "", NombreEquipo_2 As String = ""
        '    NombreEquipo = IIf(grdItems.Rows(i).Cells(0).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(0).Value)
        '    For j = i + 1 To grdItems.RowCount - 1
        '        If grdItems.RowCount > 1 Then
        '            NombreEquipo_2 = IIf(grdItems.Rows(j).Cells(0).Value Is DBNull.Value, "", grdItems.Rows(j).Cells(0).Value)
        '            If NombreEquipo <> "" And NombreEquipo_2 <> "" Then
        '                If NombreEquipo = NombreEquipo_2 Then
        '                    Util.MsgStatus(Status1, "El producto '" & NombreEquipo & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & j + 1, My.Resources.Resources.alert.ToBitmap, True)
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    Next
        'Next

        bolpoliticas = True

    End Sub

    Private Sub Imprimir(ByVal Codigo As Integer)
        nbreformreportes = "Remito"

        'Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        Rpt.NombreArchivoPDF = BuscarNombreArchivo(Codigo)

        If MessageBox.Show("Desea imprimir 2 Copias directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", True)
        Else
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", False)
        End If

        'cnn = Nothing

    End Sub

    Private Sub LlenarGridItems()

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

            sqltxt2 = "exec [spVentas_Mostrador_Det_Select_By_IdVenta] @idventa = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), dt.Rows(i)(4).ToString(),
                                  dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(), dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(),
                                  dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(), "Eliminar")
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
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

            With cmbCliente
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

    Private Sub CalcularSubtotal()

        Dim i As Integer
        Dim Subtotal As Double = 0
        Dim Total As Double = 0
        For i = 0 To grdItems.Rows.Count - 1
            Subtotal = Subtotal + grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value
            Total = Total + grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalFinal).Value
        Next

        txtSubtotalVenta.Text = Subtotal
        txtTotalVenta.Text = Total

        Dim Descuento As Double

        If txtDescuento.Text = "" Then
            Descuento = "0"
            txtTotalVenta.Text = Total
            Exit Sub
        Else
            Descuento = txtDescuento.Text
        End If


        If rdAbsoluto.Checked = True Then
            txtTotalVenta.Text = Math.Round(CDbl(txtTotalVenta.Text) - Descuento, 2)
        Else

            Dim ValorDescuento As Double
            ValorDescuento = Math.Round(CDbl(txtTotalVenta.Text) * (Descuento / 100), 2)

            txtTotalVenta.Text = Math.Round(CDbl(txtTotalVenta.Text) - ValorDescuento, 2)
        End If

    End Sub


#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro(ByVal PagoContado As Boolean) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

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
                If bolModo = True Then
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.InputOutput
                Else
                    param_id.Value = txtID.Text
                    param_id.Direction = ParameterDirection.Input
                End If

                Dim param_IdCliente As New SqlClient.SqlParameter
                param_IdCliente.ParameterName = "@IdCliente"
                param_IdCliente.SqlDbType = SqlDbType.VarChar
                param_IdCliente.Size = 25
                param_IdCliente.Value = IIf(txtIdCliente.Text = "", "0", txtIdCliente.Text)
                param_IdCliente.Direction = ParameterDirection.Input

                Dim param_Cliente As New SqlClient.SqlParameter
                param_Cliente.ParameterName = "@Cliente"
                param_Cliente.SqlDbType = SqlDbType.VarChar
                param_Cliente.Size = 200
                param_Cliente.Value = cmbCliente.Text.ToString
                param_Cliente.Direction = ParameterDirection.Input

                If cmbCliente.Text.Contains("PRINCIPAL") Or cmbCliente.Text.Contains("PERON") Then
                    chkTransInterna.Checked = True
                End If

                Dim param_IdEmpleado As New SqlClient.SqlParameter
                param_IdEmpleado.ParameterName = "@IdVendedor"
                param_IdEmpleado.SqlDbType = SqlDbType.VarChar
                param_IdEmpleado.Size = 25
                param_IdEmpleado.Value = IdVendedor
                param_IdEmpleado.Direction = ParameterDirection.Input

                Dim param_vendedor As New SqlClient.SqlParameter
                param_vendedor.ParameterName = "@vendedor"
                param_vendedor.SqlDbType = SqlDbType.VarChar
                param_vendedor.Size = 200
                param_vendedor.Value = lblVendedor.Text
                param_vendedor.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_Subtotal As New SqlClient.SqlParameter
                param_Subtotal.ParameterName = "@Subtotal"
                param_Subtotal.SqlDbType = SqlDbType.Decimal
                param_Subtotal.Precision = 18
                param_Subtotal.Scale = 2
                param_Subtotal.Value = txtSubtotalVenta.Text
                param_Subtotal.Direction = ParameterDirection.Input

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

                Dim param_Total As New SqlClient.SqlParameter
                param_Total.ParameterName = "@total"
                param_Total.SqlDbType = SqlDbType.Decimal
                param_Total.Precision = 18
                param_Total.Scale = 2
                param_Total.Value = txtTotalVenta.Text
                param_Total.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@observacion"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtObservaciones.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_Contado As New SqlClient.SqlParameter
                param_Contado.ParameterName = "@PagoContado"
                param_Contado.SqlDbType = SqlDbType.Bit
                param_Contado.Value = PagoContado
                param_Contado.Direction = ParameterDirection.Input

                Dim param_TransInterna As New SqlClient.SqlParameter
                param_TransInterna.ParameterName = "@TransInterna"
                param_TransInterna.SqlDbType = SqlDbType.Bit
                param_TransInterna.Value = chkTransInterna.Checked
                param_TransInterna.Direction = ParameterDirection.Input

                'Dim param_repartidor As New SqlClient.SqlParameter
                'param_repartidor.ParameterName = "@Repartidor"
                'param_repartidor.SqlDbType = SqlDbType.VarChar
                'param_repartidor.Size = 200
                'param_repartidor.Value = cmbRepartidor.SelectedValue
                'param_repartidor.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spVentas_Mostrador_Insert]", param_id, _
                                                 param_IdCliente, param_Cliente, param_IdEmpleado, param_vendedor, param_fecha, _
                                                 param_Subtotal, param_Descuento, param_Porcentaje, param_valorDescuento, param_Total, _
                                                 param_nota, param_Contado, param_TransInterna, param_useradd, param_res)

                        txtID.Text = param_id.Value

                    Else

                        '                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Update", param_id, _
                        '                                                param_origen, param_destino, param_empleado, param_fecha, param_nota, param_res)

                    End If

                    AgregarActualizar_Registro = param_res.Value

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

    Private Function AgregarActualizar_Registro_Items() As Integer
        Dim res As Integer
        Dim IdStockMov As Long
        Dim Stock As Double
        Dim StockReceptor As Double
        Dim IdStockMovReceptor As Long

        Try

            Dim i As Integer

            Dim param_idMaqMov As New SqlClient.SqlParameter
            param_idMaqMov.ParameterName = "@idMaqMov"
            param_idMaqMov.SqlDbType = SqlDbType.BigInt
            param_idMaqMov.Value = txtID.Text
            param_idMaqMov.Direction = ParameterDirection.Input

            Dim param_resDEL As New SqlClient.SqlParameter
            param_resDEL.ParameterName = "@res"
            param_resDEL.SqlDbType = SqlDbType.Int
            param_resDEL.Value = DBNull.Value
            param_resDEL.Direction = ParameterDirection.InputOutput

            'If bolModo = False Then
            '    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Det_Delete", param_idMaqMov, param_resDEL)

            '    res = param_resDEL.Value

            '    If res <= 0 Then
            '        MsgBox("Se produjo un error al eliminar temporalmente los Items", MsgBoxStyle.Critical, "Control de Errores")
            '        AgregarActualizar_Registro_Items = -1
            '        Exit Function
            '    End If

            'End If

            For i = 0 To grdItems.Rows.Count - 1

                Dim param_IdVenta As New SqlClient.SqlParameter
                param_IdVenta.ParameterName = "@IdVenta"
                param_IdVenta.SqlDbType = SqlDbType.BigInt
                param_IdVenta.Value = txtID.Text
                param_IdVenta.Direction = ParameterDirection.Input

                Dim param_IdAlmacen As New SqlClient.SqlParameter
                param_IdAlmacen.ParameterName = "@IdAlmacen"
                param_IdAlmacen.SqlDbType = SqlDbType.BigInt
                param_IdAlmacen.Value = Utiles.numero_almacen
                param_IdAlmacen.Direction = ParameterDirection.Input

                Dim param_codigobarra As New SqlClient.SqlParameter
                param_codigobarra.ParameterName = "@CodigoBarra"
                param_codigobarra.SqlDbType = SqlDbType.VarChar
                param_codigobarra.Size = 25
                param_codigobarra.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Codigo).Value
                param_codigobarra.Direction = ParameterDirection.Input

                Dim param_idProducto As New SqlClient.SqlParameter
                param_idProducto.ParameterName = "@IdProducto"
                param_idProducto.SqlDbType = SqlDbType.VarChar
                param_idProducto.Size = 25
                param_idProducto.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                param_idProducto.Direction = ParameterDirection.Input

                Dim param_idUnidad As New SqlClient.SqlParameter
                param_idUnidad.ParameterName = "@IdUnidad"
                param_idUnidad.SqlDbType = SqlDbType.VarChar
                param_idUnidad.Size = 25
                param_idUnidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value
                param_idUnidad.Direction = ParameterDirection.Input

                Dim param_Producto As New SqlClient.SqlParameter
                param_Producto.ParameterName = "@Producto"
                param_Producto.SqlDbType = SqlDbType.VarChar
                param_Producto.Size = 300
                param_Producto.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value
                param_Producto.Direction = ParameterDirection.Input

                Dim param_cantidad As New SqlClient.SqlParameter
                param_cantidad.ParameterName = "@Cantidad"
                param_cantidad.SqlDbType = SqlDbType.BigInt
                param_cantidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value
                param_cantidad.Direction = ParameterDirection.Input

                Dim param_Preciouni As New SqlClient.SqlParameter
                param_Preciouni.ParameterName = "@PrecioUni"
                param_Preciouni.SqlDbType = SqlDbType.Decimal
                param_Preciouni.Precision = 18
                param_Preciouni.Scale = 2
                param_Preciouni.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value
                param_Preciouni.Direction = ParameterDirection.Input

                Dim param_Subtotal As New SqlClient.SqlParameter
                param_Subtotal.ParameterName = "@Subtotal"
                param_Subtotal.SqlDbType = SqlDbType.Decimal
                param_Subtotal.Precision = 18
                param_Subtotal.Scale = 2
                param_Subtotal.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value
                param_Subtotal.Direction = ParameterDirection.Input

                Dim param_DescxUnidad As New SqlClient.SqlParameter
                param_DescxUnidad.ParameterName = "@DescxUnidad"
                param_DescxUnidad.SqlDbType = SqlDbType.Decimal
                param_DescxUnidad.Precision = 18
                param_DescxUnidad.Scale = 2
                param_DescxUnidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Desc_Unit).Value
                param_DescxUnidad.Direction = ParameterDirection.Input

                Dim param_TotalDesc As New SqlClient.SqlParameter
                param_TotalDesc.ParameterName = "@TotalDesc"
                param_TotalDesc.SqlDbType = SqlDbType.Decimal
                param_TotalDesc.Precision = 18
                param_TotalDesc.Scale = 2
                param_TotalDesc.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Desc_Total).Value
                param_TotalDesc.Direction = ParameterDirection.Input

                Dim param_SubtotalFinal As New SqlClient.SqlParameter
                param_SubtotalFinal.ParameterName = "@SubtotalFinal"
                param_SubtotalFinal.SqlDbType = SqlDbType.Decimal
                param_SubtotalFinal.Precision = 18
                param_SubtotalFinal.Scale = 2
                param_SubtotalFinal.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalFinal).Value
                param_SubtotalFinal.Direction = ParameterDirection.Input

                Dim param_IdStockMov As New SqlClient.SqlParameter
                param_IdStockMov.ParameterName = "@IdStockMov"
                param_IdStockMov.SqlDbType = SqlDbType.Int
                param_IdStockMov.Value = DBNull.Value
                param_IdStockMov.Direction = ParameterDirection.InputOutput

                Dim param_Stock As New SqlClient.SqlParameter
                param_Stock.ParameterName = "@Stock"
                param_Stock.SqlDbType = SqlDbType.Decimal
                param_Stock.Precision = 18
                param_Stock.Scale = 2
                param_Stock.Value = DBNull.Value
                param_Stock.Direction = ParameterDirection.InputOutput

                Dim param_StockReceptor As New SqlClient.SqlParameter
                param_StockReceptor.ParameterName = "@StockReceptor"
                param_StockReceptor.SqlDbType = SqlDbType.Decimal
                param_StockReceptor.Precision = 18
                param_StockReceptor.Scale = 2
                param_StockReceptor.Value = DBNull.Value
                param_StockReceptor.Direction = ParameterDirection.InputOutput

                Dim param_IdStockMovReceptor As New SqlClient.SqlParameter
                param_IdStockMovReceptor.ParameterName = "@IdStockMovReceptor"
                param_IdStockMovReceptor.SqlDbType = SqlDbType.Int
                param_IdStockMovReceptor.Value = DBNull.Value
                param_IdStockMovReceptor.Direction = ParameterDirection.InputOutput

                Dim param_TransInterna As New SqlClient.SqlParameter
                param_TransInterna.ParameterName = "@TransInterna"
                param_TransInterna.SqlDbType = SqlDbType.Bit
                param_TransInterna.Value = chkTransInterna.Checked
                param_TransInterna.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spVentas_Mostrador_Det_Insert", _
                                              param_IdVenta, param_IdAlmacen, param_idProducto, param_idUnidad, param_Producto, _
                                              param_cantidad, param_Preciouni, param_Subtotal, param_DescxUnidad, param_codigobarra, _
                                              param_TotalDesc, param_SubtotalFinal, param_TransInterna, param_IdStockMov, param_Stock, _
                                              param_StockReceptor, param_IdStockMovReceptor, param_res)

                    res = param_res.Value
                    IdStockMov = param_IdStockMov.Value
                    Stock = param_Stock.Value

                    Try
                        IdStockMovReceptor = param_IdStockMovReceptor.Value
                        StockReceptor = param_StockReceptor.Value
                    Catch ex As Exception

                    End Try

                    If res <= 0 Then
                        AgregarActualizar_Registro_Items = -1
                    End If

                    If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                        Try
                            Dim sqlstring As String

                            'sqlstring = "update stock set qty = qty - " & grdItems.Rows(i).Cells(1).Value & ", dateupd=getdate(),userupd= " & UserID & _
                            '    " where idmaterial= " & grdItems.Rows(i).Cells(9).Value & _
                            '    "  and idunidad= " & grdItems.Rows(i).Cells(10).Value & _
                            '    " and idalmacen = " & Utiles.numero_almacen

                            sqlstring = "exec spStock_Insert '" & grdItems.Rows(i).Cells(9).Value & "', '" & _
                                grdItems.Rows(i).Cells(10).Value & "', " & Utiles.numero_almacen & ", 'V', " & _
                                grdItems.Rows(i).Cells(1).Value & ", " & Stock & ", " & IdStockMov & ", '', 4, " & UserID


                            If tranWEB.Sql_Get_Value(sqlstring) > 0 Then
                                ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                                ds_Empresa.Dispose()
                            End If

                            If chkTransInterna.Checked = True Then
                                Dim Sucursal As Integer
                                If cmbCliente.Text.Contains("PRINCIPAL") Then
                                    Sucursal = 1
                                Else
                                    Sucursal = 2
                                End If

                                sqlstring = "exec spStock_Insert '" & grdItems.Rows(i).Cells(9).Value & "', '" & _
                                 grdItems.Rows(i).Cells(10).Value & "', " & Sucursal & ", 'I', " & _
                                 grdItems.Rows(i).Cells(1).Value & ", " & StockReceptor & ", " & IdStockMovReceptor & ", '', 4, " & UserID


                                If tranWEB.Sql_Get_Value(sqlstring) > 0 Then
                                    ds_Empresa = SqlHelper.ExecuteDataset(tran, CommandType.Text, "UPDATE StockMov SET ActualizadoWEB = 1 WHERE id = " & IdStockMov)
                                    ds_Empresa.Dispose()
                                End If
                            End If

                        Catch ex As Exception
                            MsgBox(ex.Message)
                            'MsgBox("No se puede actualizar en la Web la Lista de Precios actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                        End Try
                    End If


                Catch ex As Exception
                    Throw ex
                End Try

            Next

            AgregarActualizar_Registro_Items = 1

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
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

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

                Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_id.Value = CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try

                    'SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Delete_All", param_id, param_res)
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spVentas_Mostrador_Delete_All", param_id, param_res)
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

    Private Function BuscarNombreArchivo(ByVal codigo As String) As String
        Dim ds_Archivo As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BuscarNombreArchivo = ""
            Exit Function
        End Try

        Try

            ds_Archivo = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select CONVERT(VARCHAR(10), codigo) + ' - ' + Destino from [dbo].[MaquinasHerramientas_Mov]  " & _
                            " WHERE eliminado = 0 and codigo = " & codigo)

            ds_Archivo.Dispose()

            BuscarNombreArchivo = ds_Archivo.Tables(0).Rows(0)(0)

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            BuscarNombreArchivo = ""

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function CalcularPeso(ByVal texto As String) As String

        Dim Salida As String = ""
        Dim Salida2 As Integer = 0
        Dim Salida3 As String = ""
        Dim SalidaFinal As Double = 0
        texto = Trim(texto).Replace(vbCrLf, "")
        'MsgBox(texto)

        For i As Integer = 0 To texto.Length - 1

            'controlo que no tenga espacios
            If Not texto.Chars(i) = " " Then
                Salida = Salida + texto.Chars(i).ToString
                'controlo si es un 02 lo borro
                If Salida = "02" Then
                    Salida = ""
                End If
                'si es 03 termino la cadena
                If Salida = "03" Then
                    Exit For
                End If
                'me fijo el tamaño del caracter y voy descifrando el número
                If Salida.Length = 2 Then
                    Salida2 = CInt(Salida) - 30
                    Salida3 = Salida3 + Salida2.ToString
                    Salida = ""
                End If
            End If

        Next

        'divido la salida final por 1000
        SalidaFinal = FormatNumber(CDbl(Salida3) / 1000, 3)

        'entrego el string de Peso
        Return SalidaFinal.ToString

    End Function


#End Region

#Region "   Botones"

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If bolModo = False Then
            MsgBox("No se puede modificar una venta realizada", MsgBoxStyle.Critical, "Control de Errores")
            Exit Sub
        End If

        If grdItems.RowCount = 0 Then
            Util.MsgStatus(Status1, "Debe ingresar al menos un Producto.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar al menos un Producto.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Dim PagoContado As Boolean = False

                'If MessageBox.Show("Confirma MONTO Pago CONTADO / EFECTIVO para la operación ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                PagoContado = True
                'End If

                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro(PagoContado)
                Select Case res
                    Case -20
                        Util.MsgStatus(Status1, "No se pudo agregar el cliente.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el cliente.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                    Case Is <= 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                    Case Else
                        res = AgregarActualizar_Registro_Items()
                        Select Case res
                            Case Is <= 0
                                Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                            Case Else
                                Cerrar_Tran()


                                'Imprimir(txtCODIGO.Text)
                                bolModo = False

                                If txtIdCliente.Text = "" Or txtIdCliente.Text = "0" Then
                                    LlenarcmbClientes()
                                End If

                                PrepararBotones()
                                SQL = "exec [spVentas_Mostrador_Select_ALL] @Eliminado = 0"

                                MDIPrincipal.NoActualizarBase = False
                                'btnCancelar_Click(sender, e)
                                btnActualizar_Click(sender, e)

                                'If grd.Rows.Count > 0 Then
                                '    grd.Rows(0).Selected = True
                                '    LlenarGridItems()
                                'End If

                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                                btnNuevo_Click(sender, e)

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

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        band = 0
        bolModo = True
        chkPrecioMayorista.Enabled = True
        lblTipoOperacion.Visible = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkEliminado.Checked = False

        dtpFECHA.Value = Date.Today

        Util.LimpiarTextBox(Me.Controls)

        grdItems.Rows.Clear()

        band = 1

        cmbProducto.SelectedIndex = 0
        cmbProducto.Text = ""
        cmbCliente.SelectedValue = 1
        txtObservaciones.Enabled = True
        grdItems.Enabled = True
        cmbProducto.Enabled = True
        txtCantidad.Enabled = True
        chkPrecioMayorista.Checked = False
        txtTotalVenta.Text = ""



        ' ''ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT PrecioCosto, IdUnidad FROM Materiales where id = " & cmbProducto.SelectedValue)
        'ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT PrecioPeron, m.IdUnidad, qty FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo where m.Codigo = " & cmbProducto.SelectedValue)
        'ds_Empresa.Dispose()

        'txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(0), 2)
        'txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(1)
        'lblStock.Text = ds_Empresa.Tables(0).Rows(0)(2)

        frmLoginVendedor.ShowDialog()

        lblVendedor.Text = NombreVendedor

        txtObservaciones.Focus()
        SendKeys.Send("{TAB}")

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        If chkEliminado.Checked = False Then
            If MessageBox.Show("Esta acción anulará la venta (no podrá usar el mismo nro de remito en el sistema) " + vbCrLf + _
                           "¿Está seguro que desea Anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    Case Is <= 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo anular el Remito de Ventas.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo anular el Remito de Ventas.", My.Resources.stop_error.ToBitmap, True)
                    Case Else
                        Cerrar_Tran()
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True, True)
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim Cliente As String

        Dim consulta As String = "select '' as Nombre UNION select Nombre from clientes C join Ventas v ON v.IdCliente = c.id where c.eliminado = 0 order by nombre asc"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        'paramreporte.AgregarParametros("Cta Cte :", "STRING", "", False, "", consulta, Cnn)


        paramreporte.ShowDialog()

        nbreformreportes = "Ventas"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString

            rpt.DeudaClientes_App(Inicial, Final, Cliente, "", rpt, My.Application.Info.AssemblyName.ToString, False)
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente la venta del cliente: " & grd.CurrentRow.Cells(3).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Ventas SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Ventas_Det SET Eliminado = 0 WHERE idVenta = " & grd.CurrentRow.Cells(0).Value)

            ds_Update.Dispose()

            SQL = "exec spVentas_Mostrador_Select_ALL @Eliminado = 1"


            LlenarGrilla()


            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
                'limpia la grilla de detalles
                grdItems.Rows.Clear()
            End If

            Util.MsgStatus(Status1, "La venta se activó correctamente.", My.Resources.ok.ToBitmap)




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

    'Public Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
    '    ' comm.ClosePort()
    'End Sub

    Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        lblTipoOperacion.Visible = False
        chkPrecioMayorista.Checked = False
        chkPrecioMayorista.Enabled = False
        Timer1.Enabled = False
        btnNuevo_Click(sender, e)
        'If grd.Rows.Count > 0 Then
        '    grd.Rows(0).Selected = True
        '    LlenarGridItems()
        'End If

    End Sub

#End Region



    Private Sub chkDesc_CheckedChanged(sender As Object, e As EventArgs) Handles chkDescuento.CheckedChanged
        rdAbsoluto.Enabled = chkDescuento.Checked
        rdPorcentaje.Enabled = chkDescuento.Checked
        txtDescuento.Enabled = chkDescuento.Checked

        If chkDescuento.Checked = False Then
            txtDescuento.Text = "0"
        Else
            rdPorcentaje.Checked = True
        End If

        CalcularSubtotal()

    End Sub

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtDescuento.TextChanged
        If band = 0 Then Exit Sub

        Dim Descuento As Double
        If txtDescuento.Text = "" Or txtDescuento.Text = "0" Then
            Descuento = "0"
        Else
            Descuento = txtDescuento.Text
        End If

        CalcularSubtotal()

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

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged


        If band = 1 And CodBarra_Activado = False Then


            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,m.Codigo" & _
                                         " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                         "JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                         " where m.Codigo = '" & cmbProducto.SelectedValue & "' AND s.idalmacen = " & Utiles.numero_almacen)



            ds_Empresa.Dispose()

            Try


                If txtIDPrecioLista.Text = 3 Then
                    txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(6), 2)
                ElseIf txtIDPrecioLista.Text = 4 Then
                    txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(7), 2)
                ElseIf txtIDPrecioLista.Text = 5 Then
                    txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(8), 2)
                ElseIf txtIDPrecioLista.Text = 10 Then
                    txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(13), 2)
                ElseIf DescLista.Contains("NORTE") Then
                    txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(13) * ValorNorte_cambio, 2)
                Else
                    txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(9), 2)
                End If
                txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(0)
                unidad_unitario = txtIdUnidad.Text
                lblStock.Text = ds_Empresa.Tables(0).Rows(0)(1)
                stock_unitario = lblStock.Text
                idproducto_unitario = ds_Empresa.Tables(0).Rows(0)(2)
                producto_unitario = ds_Empresa.Tables(0).Rows(0)(3)
                almacen_unitario = ds_Empresa.Tables(0).Rows(0)(4)
                'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
                If (ds_Empresa.Tables(0).Rows(0)(5)).ToString <> "" Then
                    If CDbl(stock_unitario) > CDbl(ds_Empresa.Tables(0).Rows(0)(5)) Then
                        lblStock.BackColor = Color.Green
                    Else
                        lblStock.BackColor = Color.Red
                    End If
                End If
                'txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(11)

                If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
                    Label13.Text = "Peso*"
                Else
                    Label13.Text = "Peso"
                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

        If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
            'If txtPeso.Text = "" Then
            '    txtSubtotal.Text = "0"
            'Else
            '    txtSubtotal.Text = Math.Round(CDbl(txtPeso.Text) * CDbl(txtPrecioVta.Text), 2)
            'End If
            Exit Sub
        Else
            If txtCantidad.Text = "" Then
                txtSubtotal.Text = "0"
            Else
                txtSubtotal.Text = Math.Round(CDbl(txtCantidad.Text) * CDbl(txtPrecioVta.Text), 2)
            End If
        End If

    End Sub

    Private Sub validar_NumerosReales2( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = 4 Then
            'Or columna = ColumnasDelGridItems.PorcRecDesc Then

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

    Private Sub cmbCliente_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCliente.KeyPress
        If bolModo = True Then
            If e.KeyChar = ChrW(Keys.Back) Then
                txtIdCliente.Text = ""
            End If
        End If
    End Sub

    Private Sub cmbCliente_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbCliente.SelectedValueChanged
        Dim dsCliente As Data.DataSet

        If band = 1 And bolModo = True Then
            txtIdCliente.Text = cmbCliente.SelectedValue
            Try
                dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select C.IDPrecioLista,C.Provincia,C.Localidad,C.Direccion,C.Repartidor,L.Descripcion,L.Valor_Cambio,C.Promo From Clientes C Join Lista_Precios L On C.IDPrecioLista =L.Codigo Where C.eliminado = 0 and C.Codigo = '" & cmbCliente.SelectedValue & "'")
                txtIDPrecioLista.Text = dsCliente.Tables(0).Rows(0).Item(0)
                ' lblLugarEntrega.Text = dsCliente.Tables(0).Rows(0).Item(1).ToString + " " + dsCliente.Tables(0).Rows(0).Item(2).ToString + " " + dsCliente.Tables(0).Rows(0).Item(3).ToString
                ' cmbRepartidor.SelectedValue = dsCliente.Tables(0).Rows(0).Item(4)
                DescLista = dsCliente.Tables(0).Rows(0).Item(5)
                ValorNorte_cambio = dsCliente.Tables(0).Rows(0).Item(6)
                ' Habilitar_Promo = dsCliente.Tables(0).Rows(0).Item(7)
            Catch ex As Exception

            End Try



            Try
                If grdItems.Rows.Count > 0 Then

                    For i As Integer = 0 To grdItems.Rows.Count - 1
                        If txtIDPrecioLista.Text = 3 Then
                            dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCosto From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "'")
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)

                        ElseIf txtIDPrecioLista.Text = 4 Then
                            dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioMayorista From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "'")
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                        ElseIf txtIDPrecioLista.Text = 5 Then
                            dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioLista3 From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "'")
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                        ElseIf txtIDPrecioLista.Text = 10 Then
                            dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCompra From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "'")
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                            chkTransInterna.Checked = True
                        ElseIf DescLista.Contains("NORTE") Then
                            dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCompra From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "'")
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0) * ValorNorte_cambio
                        Else
                            dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioLista4 From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value & "'")
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                        End If
                        'Me fijo si es horma o tira 
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("HORMA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("TIRA") Then
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
                        Else
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = Math.Round((grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
                        End If

                        dsCliente.Dispose()
                    Next
                    CalcularSubtotal()
                End If
            Catch ex As Exception

            End Try


            cmbProducto.Enabled = True

            band = 0
            'LlenarcmbPedidos()
            band = 1
            'LimpiarGridItems(grdItems)
            ' btnLlenarGrilla_Click(sender, e)
        End If
    End Sub

    Private Sub cmbCliente_TextChanged(sender As Object, e As EventArgs) Handles cmbCliente.TextChanged
        If bolModo = True Then
            If cmbCliente.Text = "" Then
                txtIdCliente.Text = ""
            End If
        End If
    End Sub

    Private Sub chkPrecioMayorista_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrecioMayorista.CheckedChanged

        'If chkPrecioMayorista.Checked = True Then
        'limpio los txt
        lblStock.Text = "0.00"
        txtPrecioVta.Text = "0.00"
        txtCantidad.Text = ""
        txtSubtotal.Text = "0.00"
        'limpio y hago foco en el combo de productos
        cmbProducto.Text = ""
        cmbProducto.Focus()
        'End If

    End Sub

    Private Sub txtPeso_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPeso.KeyDown

        If e.KeyCode = Keys.Enter Then
            txtCantidad_KeyDown(sender, e)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub txtPeso_TextChanged(sender As Object, e As EventArgs) Handles txtPeso.TextChanged

        If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
            If txtPeso.Text = "" Then
                txtSubtotal.Text = "0"
            Else
                txtSubtotal.Text = Math.Round(CDbl(txtPeso.Text) * CDbl(txtPrecioVta.Text), 2)
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'If Timer1.Enabled = True Then
        '    comm.WriteData("05")
        'End If
    End Sub

    Private Sub txtLectura_TextChanged(sender As Object, e As EventArgs) Handles txtLectura.TextChanged

        ''Si esta en movimiento la balanza que ponga un cero
        'If Mid(Trim(txtLectura.Text).Replace(vbCrLf, ""), 1, 2) = "11" Then
        '    txtPeso.Text = "0"
        'ElseIf txtLectura.Text.Contains("2D") Then
        '    txtPeso.Text = "0"
        'ElseIf txtLectura.Text.Contains("03") And Not txtLectura.Text.Contains("Port") Then
        '    'MsgBox("entro")
        '    txtPeso.Text = CalcularPeso(txtLectura.Text)
        'Else
        '    Exit Sub
        'End If

    End Sub

    Private Sub txtCodigoBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigoBarra.KeyDown
        If band = 1 Then
            Try
                If e.KeyCode = Keys.Enter Then

                    CodBarra_Activado = True

                    ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,m.Codigo, (m.Nombre + ' - ' + mar.Nombre) as Producto " & _
                                                 " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                                 "JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                                 " where m.CodigoBarra = '" & txtCodigoBarra.Text & "' AND s.idalmacen = " & Utiles.numero_almacen)



                    ds_Empresa.Dispose()


                    'MsgBox(ds_Empresa.Tables(0).Rows(0)(2).ToString)
                    cmbProducto.SelectedValue = ds_Empresa.Tables(0).Rows(0)(2).ToString
                    cmbProducto.Text = ds_Empresa.Tables(0).Rows(0)(15).ToString
                    If txtIDPrecioLista.Text = 3 Then
                        txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(6), 2)
                    ElseIf txtIDPrecioLista.Text = 4 Then
                        txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(7), 2)
                    ElseIf txtIDPrecioLista.Text = 5 Then
                        txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(8), 2)
                    ElseIf txtIDPrecioLista.Text = 10 Then
                        txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(13), 2)
                    ElseIf DescLista.Contains("NORTE") Then
                        txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(13) * ValorNorte_cambio, 2)
                    Else
                        txtPrecioVta.Text = Math.Round(ds_Empresa.Tables(0).Rows(0)(9), 2)
                    End If
                    txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(0)
                    unidad_unitario = txtIdUnidad.Text
                    lblStock.Text = ds_Empresa.Tables(0).Rows(0)(1)
                    stock_unitario = lblStock.Text
                    idproducto_unitario = ds_Empresa.Tables(0).Rows(0)(2)
                    producto_unitario = ds_Empresa.Tables(0).Rows(0)(3)
                    almacen_unitario = ds_Empresa.Tables(0).Rows(0)(4)
                    'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
                    If (ds_Empresa.Tables(0).Rows(0)(5)).ToString <> "" Then
                        If CDbl(stock_unitario) > CDbl(ds_Empresa.Tables(0).Rows(0)(5)) Then
                            lblStock.BackColor = Color.Green
                        Else
                            lblStock.BackColor = Color.Red
                        End If
                    End If
                    'txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(11)

                    If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
                        Label13.Text = "Peso*"
                    Else
                        Label13.Text = "Peso"
                    End If

                    txtPeso.Text = "1"

                    CodBarra_Activado = False

                    txtCantidad.Focus()
                    'SendKeys.Send("{TAB}")
                End If

 

            Catch ex As Exception

                ' MsgBox(ex.Message)
                CodBarra_Activado = False

            End Try

        End If
    End Sub

 
End Class