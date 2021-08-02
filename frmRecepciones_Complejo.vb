Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmRecepciones_Complejo

    Dim bolpoliticas As Boolean

    Dim permitir_evento_CellChanged As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean
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

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IDRecepcion_Det = 0
        Cod_RecepcionDet = 1
        IDMaterial = 2
        Cod_Material = 3
        Nombre_Material = 4
        IdUnidad = 5
        CodUnidad = 6
        Unidad = 7
        IdMoneda = 8
        CodMoneda = 9
        Moneda = 10
        ValorCambio = 11
        Bonif1 = 12
        Bonif2 = 13
        Bonif3 = 14
        Bonif4 = 15
        Bonif5 = 16
        Ganancia = 17
        PrecioxMt = 18
        PrecioxKg = 19
        PesoxUnidad = 20
        CantxLongitud = 21
        IVA = 22
        PrecioLista = 23
        QtyPedido = 24
        PrecioPedido = 25
        QtyRecep = 26
        PrecioReal = 27
        PorcDif = 28
        Remito = 29
        ID_OrdenDeCompra = 30
        ID_OrdenDeCompra_Det = 31
        QtySaldo = 32
        Status = 33
        FechaCumplido = 34
        PrecioEnPesos = 35
        PrecioEnPesosNuevo = 36
        Nuevo = 37
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim band As Integer


#Region "   Eventos"

    Private Sub frmRecepciones_Gestion_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGridItems()
            End If
        End If
    End Sub

    Private Sub frmRecepciones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la Recepción Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Orde de Compra Nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmRecepciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        band = 0

        configurarform()
        asignarTags()

        LlenarComboMateriales()
        LlenarComboProveedores()
        LlenarComboProveedores_Flete()

        SQL = "exec sp_Recepciones_Select_All"
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos
        Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems()
            band = 1
            btnNuevo_Click(sender, e)
        Else
            btnLlenarGrilla.Enabled = bolModo
        End If

        cmbProveedor.Visible = bolModo

        txtProveedor.Visible = Not bolModo
        txtOC.Visible = Not bolModo

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        lblProveedorFlete.Enabled = bolModo
        cmbProveedorFlete.Enabled = bolModo
        lblTipoFactFlete.Enabled = bolModo
        cmbTipoFactFlete.Enabled = bolModo
        lblFacturaFlete.Enabled = bolModo
        txtFacturaFlete.Enabled = bolModo
        lblSubtotalFlete.Enabled = bolModo
        txtSubtotalFlete.Enabled = bolModo
        lblPorcIvaFlete.Enabled = bolModo
        txtPorcIvaFlete.Enabled = bolModo
        lblMontoIvaFlete.Enabled = bolModo
        txtMontoIvaFlete.Enabled = bolModo
        lblTotalFlete.Enabled = bolModo
        txtTotalFlete.Enabled = bolModo
        chkFleteSaldado.Enabled = bolModo

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

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote    
        If Me.grdItems.CurrentCell.ColumnIndex = 7 Then
            AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
        Else
            AddHandler e.Control.KeyPress, AddressOf NoValidar
        End If
        'End If
    End Sub

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
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If Not bolModo Then
            'cmbProveedores.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

    Private Sub grdItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdItems.KeyDown

        'Dim columna As Integer = 0
        'columna = grdItems.CurrentCell.ColumnIndex

        ''If columna = ColumnasDelGridItems.ID_OrdenDeCompra Then
        'If columna = 7 Then
        '    If e.KeyCode = Keys.F5 And bolModo Then
        '        Dim f As New ICYS.frmOrdenCompra
        '        LLAMADO_POR_FORMULARIO = True
        '        ARRIBA = 40
        '        IZQUIERDA = Me.Left - 150
        '        'texto_del_combo = cmbPROVEEDORES.Text.ToUpper.ToString
        '        f.ShowDialog()
        '        'MsgBox(ID_ORDEN_DE_COMPRA_DET.ToString)

        '        If STATUS_ORDEN_DE_COMPRA_DET = "CUMPLIDO" Then
        '            MsgBox("El item seleccionado esta cumplido. NO se puede cargar.", MsgBoxStyle.Information, "Atención")
        '        Else
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.ID_OrdenDeCompra).Value = ID_ORDEN_DE_COMPRA
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Value = ID_ORDEN_DE_COMPRA_DET
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.IDMaterial).Value = ID_MATERIAL
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.Cod_Material).Value = CODIGO_MATERIAL
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.Nombre_Material).Value = NOMBRE_MATERIAL
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.IDUnidad).Value = ID_UNIDAD
        '            Me.grdItems.CurrentRow.Cells.Item(ColumnasDelGridItems.Unidad).Value = UNIDAD_MATERIAL

        '        End If
        '    End If
        'End If

    End Sub

    Private Sub cmbOrdenDeCompra_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrdenDeCompra.SelectedIndexChanged
        If band = 1 Then
            btnLlenarGrilla_Click(sender, e)
        End If
    End Sub

    Private Sub cmbProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProveedor.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            LimpiarGridItems(grdItems)
            LlenarComboOrdenDeCompra()
        End If
    End Sub

    Private Sub chkMostarColumnas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMostarColumnas.CheckedChanged
        grdItems.Columns(ColumnasDelGridItems.Bonif4).Visible = chkMostarColumnas.Checked
        grdItems.Columns(ColumnasDelGridItems.Bonif5).Visible = chkMostarColumnas.Checked

        If chkMostarColumnas.Checked = True Then
            chkMostarColumnas.Text = "Ocultar Bonif4, etc..."
        Else
            chkMostarColumnas.Text = "Mostar Bonif4, etc..."
        End If

    End Sub

    Private Sub chkPrecioxMt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrecioxMt.CheckedChanged
        grdItems.Columns(ColumnasDelGridItems.PrecioxMt).Visible = chkPrecioxMt.Checked
        grdItems.Columns(ColumnasDelGridItems.PrecioxKg).Visible = chkPrecioxMt.Checked
        grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).Visible = chkPrecioxMt.Checked
        grdItems.Columns(ColumnasDelGridItems.CantxLongitud).Visible = chkPrecioxMt.Checked

        If chkPrecioxMt.Checked = True Then
            chkPrecioxMt.Text = "Ocultar PrecioxMt, etc..."
        Else
            chkPrecioxMt.Text = "Mostar PrecioxMt, etc..."
        End If
    End Sub

    Private Sub chkCargarGasto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCargarGasto.CheckedChanged
        lblFactura.Enabled = chkCargarGasto.Checked
        lblMontoIVA.Enabled = chkCargarGasto.Checked
        lblSubtotal.Enabled = chkCargarGasto.Checked
        LblTotal.Enabled = chkCargarGasto.Checked
        txtNroFactura.Enabled = chkCargarGasto.Checked
        txtSubtotal.Enabled = chkCargarGasto.Checked
        txtMontoIVA.Enabled = chkCargarGasto.Checked
        txtTotal.Enabled = chkCargarGasto.Checked
        txtPorcIva.Enabled = chkCargarGasto.Checked
        lblPorcIva.Enabled = chkCargarGasto.Checked
        chkFacturaCancelada.Enabled = chkCargarGasto.Checked
        cmbTipoFact.Enabled = chkCargarGasto.Checked
        lblTipoFact.Enabled = chkCargarGasto.Checked
        lblOtrosImp.Enabled = chkCargarGasto.Checked
        txtOtrosImp.Enabled = chkCargarGasto.Checked
        lblIIBB.Enabled = chkCargarGasto.Checked
        txtIIBB.Enabled = chkCargarGasto.Checked
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Recepciones de Material"

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

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        txtRemito.Tag = "3"
        txtProveedor.Tag = "4"
        txtNota.Tag = "5"
        chkEliminado.Tag = "6"
        txtIdProveedor.Tag = "7"
        cmbTipoFact.Tag = "10"
        txtNroFactura.Tag = "11"
        txtSubtotal.Tag = "12"
        txtPorcIva.Tag = "13"
        txtMontoIva.Tag = "14"
        txtIIBB.Tag = "15"
        txtOtrosImp.Tag = "16"
        txtTotal.Tag = "17"

        chkFlete.Tag = "18"

        cmbProveedorFlete.Tag = "19"
        cmbTipoFactFlete.Tag = "20"
        txtFacturaFlete.Tag = "21"
        txtSubtotalFlete.Tag = "22"
        txtPorcIvaFlete.Tag = "23"
        txtMontoIvaFlete.Tag = "24"
        txtTotalFlete.Tag = "25"


        'chkFacturaCancelada.Tag = "12"
        txtOC.Tag = "9"



    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer

        Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String

        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : ubicacion = "" : observaciones = ""


        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si se terminó de editar la celda...
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If editando_celda Then
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
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                Try
                    'qty es válida?
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value Is System.DBNull.Value Then
                        Util.MsgStatus(Status1, "Falta completar la cantidad a Recepcionar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If

                Catch ex As Exception
                    Util.MsgStatus(Status1, "La cantidad a Recepcionar debe ser válida en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End Try

                'si tiene saldo, controlamos que no se pase..
                If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Is DBNull.Value Then
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value > grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value Then
                        Util.MsgStatus(Status1, "La cantidad a Recepcionar no debe ser mayor al Saldo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If
                End If

            End If
        Next i

        Dim buscandoalgunmov As Boolean = False

        For i = 0 To grdItems.RowCount - 1
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value > 0 Then
                buscandoalgunmov = True
                Exit For
            End If
        Next

        If buscandoalgunmov = False Then
            Util.MsgStatus(Status1, "No realizó ningún movimiento dentro de la grilla. Por favor, verifique antes de guarda.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

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
            '.AllowUserToAddRows = True
            .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material          
        End With
    End Sub

    Private Sub LlenarGridItems()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        'dg 24-2-2011 para que funcione con grilla vacia
        If txtID.Text = "" Then
            SQL = "exec sp_Recepciones_Det_Select_By_IDRecepcion @idRecepcion = 1"
        Else
            SQL = "exec sp_Recepciones_Det_Select_By_IDRecepcion @idRecepcion = " & txtID.Text
        End If

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).ReadOnly = True 'id de Recepcion_det
        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).ReadOnly = True 'Codigo de Recepcion_Det
        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).ReadOnly = True 'id de material
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = True

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True 'Material
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 350

        grdItems.Columns(6).Visible = False
        grdItems.Columns(8).Visible = False
        grdItems.Columns(9).Visible = False
        grdItems.Columns(10).Visible = False

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
        SQL = "exec sp_Recepciones_Select_All"
    End Sub

    Private Sub LlenarGridItemsPorOC(ByVal idoc As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        SQL = "exec sp_Recepciones_Det_Select_By_IDOrdenDeCompra @IdOrdenDeCompra = " & idoc

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Width = 70
        grdItems.Columns(ColumnasDelGridItems.IDRecepcion_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Cod_RecepcionDet).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Width = 80
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True '3
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True '4
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

        grdItems.Columns(ColumnasDelGridItems.IdUnidad).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.IdMoneda).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.CodMoneda).Width = 55
        grdItems.Columns(ColumnasDelGridItems.CodMoneda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Moneda).Width = 55
        grdItems.Columns(ColumnasDelGridItems.Moneda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.CodUnidad).Width = 55
        grdItems.Columns(ColumnasDelGridItems.CodUnidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 60

        grdItems.Columns(ColumnasDelGridItems.ValorCambio).ReadOnly = True '4
        grdItems.Columns(ColumnasDelGridItems.ValorCambio).Width = 55
        grdItems.Columns(ColumnasDelGridItems.ValorCambio).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Bonif1).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Bonif2).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Bonif3).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Bonif4).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Bonif4).Visible = chkMostarColumnas.Checked

        grdItems.Columns(ColumnasDelGridItems.Bonif5).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Bonif5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Bonif5).Visible = chkMostarColumnas.Checked

        grdItems.Columns(ColumnasDelGridItems.IVA).Width = 45
        grdItems.Columns(ColumnasDelGridItems.IVA).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Ganancia).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Ganancia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PrecioxMt).Width = 65
        grdItems.Columns(ColumnasDelGridItems.PrecioxMt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.PrecioxMt).Visible = chkPrecioxMt.Checked

        grdItems.Columns(ColumnasDelGridItems.PrecioxKg).Width = 65
        grdItems.Columns(ColumnasDelGridItems.PrecioxKg).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.PrecioxKg).Visible = chkPrecioxMt.Checked

        grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).Width = 65
        grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.PesoxUnidad).Visible = chkPrecioxMt.Checked

        grdItems.Columns(ColumnasDelGridItems.CantxLongitud).Width = 65
        grdItems.Columns(ColumnasDelGridItems.CantxLongitud).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.CantxLongitud).Visible = chkPrecioxMt.Checked

        grdItems.Columns(ColumnasDelGridItems.QtyPedido).ReadOnly = True 'cantidad pedida 5
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).Width = 60
        grdItems.Columns(ColumnasDelGridItems.QtyPedido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PrecioLista).Visible = False '4

        grdItems.Columns(ColumnasDelGridItems.PrecioPedido).ReadOnly = True  'precio pedido 6
        grdItems.Columns(ColumnasDelGridItems.PrecioPedido).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioPedido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.QtyRecep).ReadOnly = False 'cantidad a recibir 7
        grdItems.Columns(ColumnasDelGridItems.QtyRecep).Width = 50
        grdItems.Columns(ColumnasDelGridItems.QtyRecep).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PorcDif).ReadOnly = True 'cantidad a recibir 7
        grdItems.Columns(ColumnasDelGridItems.PorcDif).Width = 50
        grdItems.Columns(ColumnasDelGridItems.PorcDif).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.ID_OrdenDeCompra).Visible = False
        grdItems.Columns(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Remito).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True 'precio real
        grdItems.Columns(ColumnasDelGridItems.Status).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PrecioReal).ReadOnly = False 'precio real
        grdItems.Columns(ColumnasDelGridItems.PrecioReal).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioReal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.QtySaldo).ReadOnly = True  'precio real
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).Width = 50
        grdItems.Columns(ColumnasDelGridItems.QtySaldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).ReadOnly = True 'precio real
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).Width = 70
        grdItems.Columns(ColumnasDelGridItems.FechaCumplido).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.PrecioEnPesos).ReadOnly = True '4

        grdItems.Columns(ColumnasDelGridItems.PrecioEnPesosNuevo).ReadOnly = True '4

        grdItems.Columns(ColumnasDelGridItems.Nuevo).Visible = False '4

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
        SQL = "exec sp_Recepciones_Select_All"

        Dim i As Integer

        For i = 0 To grdItems.RowCount - 1
            grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Style.BackColor = Color.LightBlue
            grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioReal).Style.BackColor = Color.LightBlue
        Next


    End Sub

    Private Sub GetDatasetItems()
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

    Private Sub LlenarComboProveedores()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT p.id, p.nombre " & _
                        " from OrdendeCompra o JOIN Proveedores p ON p.id = o.IdProveedor where STATUS = 'P' AND o.eliminado=0 order by p.nombre ") 'AND Status = 'P' 
            ds_Cli.Dispose()

            With Me.cmbProveedor
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "id"
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

    Private Sub LlenarComboProveedores_Flete()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT p.id, p.nombre " & _
                        " from Proveedores p where p.eliminado=0 and flete = 1 order by p.nombre ") 'AND Status = 'P' 
            ds_Cli.Dispose()

            With Me.cmbProveedorFlete
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "id"
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

    Private Sub LlenarComboOrdenDeCompra()
        Dim ds_OC As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_OC = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select id , codigo from ordendecompra where " & _
                                            " idproveedor = " & CType(cmbProveedor.SelectedValue, Long) & "  and eliminado=0 and status in ('P') order by id desc")
            ds_OC.Dispose()

            With Me.cmbOrdenDeCompra
                .DataSource = ds_OC.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
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

    Private Sub LlenarComboMateriales()
        Dim ds_Materiales As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
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

    Private Function AgregarRegistroItems(ByVal idRecepcion As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String
        Dim i As Integer
        Dim ActualizarPrecio As Boolean = False

        Try
            Try
                i = 0

                For i = 0 To grdItems.Rows.Count - 1
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcDif).Value <> 0 Then
                        ActualizarPrecio = True
                    End If
                Next

                If ActualizarPrecio = True Then
                    ActualizarPrecio = False
                    If MessageBox.Show("Existen productos cuyos precios son diferentes. Desea actualizarlos de manera individual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        ActualizarPrecio = True
                    End If
                End If

                i = 0

                Do While i < grdItems.Rows.Count

                    If CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value, Decimal) > 0 Then

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

                        Dim param_idRecepcion As New SqlClient.SqlParameter
                        param_idRecepcion.ParameterName = "@idrecepcion"
                        param_idRecepcion.SqlDbType = SqlDbType.BigInt
                        param_idRecepcion.Value = idRecepcion
                        param_idRecepcion.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@idmaterial"
                        param_idmaterial.SqlDbType = SqlDbType.BigInt
                        param_idmaterial.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)
                        param_idmaterial.Direction = ParameterDirection.Input

                        'cod_aux = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value

                        Dim param_qty As New SqlClient.SqlParameter
                        param_qty.ParameterName = "@qty"
                        param_qty.SqlDbType = SqlDbType.Decimal
                        param_qty.Precision = 18
                        param_qty.Scale = 2
                        param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value, Decimal)
                        param_qty.Direction = ParameterDirection.Input

                        Dim param_idunidad As New SqlClient.SqlParameter
                        param_idunidad.ParameterName = "@idunidad"
                        param_idunidad.SqlDbType = SqlDbType.BigInt
                        param_idunidad.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value Is DBNull.Value, 28, grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value)
                        param_idunidad.Direction = ParameterDirection.Input

                        Dim param_remito As New SqlClient.SqlParameter
                        param_remito.ParameterName = "@remito"
                        param_remito.SqlDbType = SqlDbType.VarChar
                        param_remito.Size = 25
                        param_remito.Value = txtRemito.Text
                        param_remito.Direction = ParameterDirection.Input

                        Dim param_useradd As New SqlClient.SqlParameter
                        param_useradd.ParameterName = "@useradd"
                        param_useradd.SqlDbType = SqlDbType.Int
                        param_useradd.Value = UserID
                        param_useradd.Direction = ParameterDirection.Input

                        Dim param_Nuevo As New SqlClient.SqlParameter
                        param_Nuevo.ParameterName = "@Nuevo"
                        param_Nuevo.SqlDbType = SqlDbType.Bit
                        param_Nuevo.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Nuevo).Value, Boolean)
                        param_Nuevo.Direction = ParameterDirection.Input

                        Dim param_idordendecompra As New SqlClient.SqlParameter
                        param_idordendecompra.ParameterName = "@idordendecompra"
                        param_idordendecompra.SqlDbType = SqlDbType.BigInt
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra).Value Is DBNull.Value Then
                            param_idordendecompra.Value = 0
                        Else
                            param_idordendecompra.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra).Value, Long)
                        End If
                        param_idordendecompra.Direction = ParameterDirection.Input

                        Dim param_idordendecompradet As New SqlClient.SqlParameter
                        param_idordendecompradet.ParameterName = "@idordendecompradet"
                        param_idordendecompradet.SqlDbType = SqlDbType.BigInt
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Value Is DBNull.Value Then
                            param_idordendecompradet.Value = 0
                        Else
                            param_idordendecompradet.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_OrdenDeCompra_Det).Value, Long)
                        End If
                        param_idordendecompradet.Direction = ParameterDirection.Input

                        Dim param_material As New SqlClient.SqlParameter
                        param_material.ParameterName = "@material"
                        param_material.SqlDbType = SqlDbType.VarChar
                        param_material.Size = 300
                        param_material.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value
                        param_material.Direction = ParameterDirection.Input

                        Dim param_idmoneda As New SqlClient.SqlParameter
                        param_idmoneda.ParameterName = "@idmoneda"
                        param_idmoneda.SqlDbType = SqlDbType.BigInt
                        param_idmoneda.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value Is DBNull.Value, 1, grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value)
                        param_idmoneda.Direction = ParameterDirection.Input

                        Dim param_bonificacion1 As New SqlClient.SqlParameter
                        param_bonificacion1.ParameterName = "@bonif1"
                        param_bonificacion1.SqlDbType = SqlDbType.Decimal
                        param_bonificacion1.Precision = 18
                        param_bonificacion1.Scale = 2
                        param_bonificacion1.Value = 1 - (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value)) / 100
                        param_bonificacion1.Direction = ParameterDirection.Input

                        Dim param_bonificacion2 As New SqlClient.SqlParameter
                        param_bonificacion2.ParameterName = "@bonif2"
                        param_bonificacion2.SqlDbType = SqlDbType.Decimal
                        param_bonificacion2.Precision = 18
                        param_bonificacion2.Scale = 2
                        param_bonificacion2.Value = 1 - (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value)) / 100
                        param_bonificacion2.Direction = ParameterDirection.Input

                        Dim param_bonificacion3 As New SqlClient.SqlParameter
                        param_bonificacion3.ParameterName = "@bonif3"
                        param_bonificacion3.SqlDbType = SqlDbType.Decimal
                        param_bonificacion3.Precision = 18
                        param_bonificacion3.Scale = 2
                        param_bonificacion3.Value = 1 - (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value)) / 100
                        param_bonificacion3.Direction = ParameterDirection.Input

                        Dim param_bonificacion4 As New SqlClient.SqlParameter
                        param_bonificacion4.ParameterName = "@bonif4"
                        param_bonificacion4.SqlDbType = SqlDbType.Decimal
                        param_bonificacion4.Precision = 18
                        param_bonificacion4.Scale = 2
                        param_bonificacion4.Value = 1 - (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif4).Value)) / 100
                        param_bonificacion4.Direction = ParameterDirection.Input

                        Dim param_bonificacion5 As New SqlClient.SqlParameter
                        param_bonificacion5.ParameterName = "@bonif5"
                        param_bonificacion5.SqlDbType = SqlDbType.Decimal
                        param_bonificacion5.Precision = 18
                        param_bonificacion5.Scale = 2
                        param_bonificacion5.Value = 1 - (IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif5).Value)) / 100
                        param_bonificacion5.Direction = ParameterDirection.Input

                        Dim param_ganancia As New SqlClient.SqlParameter
                        param_ganancia.ParameterName = "@ganancia"
                        param_ganancia.SqlDbType = SqlDbType.Decimal
                        param_ganancia.Precision = 18
                        param_ganancia.Scale = 2
                        param_ganancia.Value = 1 + (CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value, Double)) / 100
                        param_ganancia.Direction = ParameterDirection.Input

                        Dim param_precioxmt As New SqlClient.SqlParameter
                        param_precioxmt.ParameterName = "@precioxmt"
                        param_precioxmt.SqlDbType = SqlDbType.Decimal
                        param_precioxmt.Precision = 18
                        param_precioxmt.Scale = 2
                        param_precioxmt.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioxMt).Value
                        param_precioxmt.Direction = ParameterDirection.Input

                        Dim param_precioxkg As New SqlClient.SqlParameter
                        param_precioxkg.ParameterName = "@precioxkg"
                        param_precioxkg.SqlDbType = SqlDbType.Decimal
                        param_precioxkg.Precision = 18
                        param_precioxkg.Scale = 2
                        param_precioxkg.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioxKg).Value
                        param_precioxkg.Direction = ParameterDirection.Input

                        Dim param_pesoxmetro As New SqlClient.SqlParameter
                        param_pesoxmetro.ParameterName = "@pesoxmetro"
                        param_pesoxmetro.SqlDbType = SqlDbType.Decimal
                        param_pesoxmetro.Precision = 18
                        param_pesoxmetro.Scale = 2
                        param_pesoxmetro.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioxMt).Value
                        param_pesoxmetro.Direction = ParameterDirection.Input

                        Dim param_cantxlongitud As New SqlClient.SqlParameter
                        param_cantxlongitud.ParameterName = "@cantxlongitud"
                        param_cantxlongitud.SqlDbType = SqlDbType.Decimal
                        param_cantxlongitud.Precision = 18
                        param_cantxlongitud.Scale = 2
                        param_cantxlongitud.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.CantxLongitud).Value
                        param_cantxlongitud.Direction = ParameterDirection.Input

                        Dim param_pesoxunidad As New SqlClient.SqlParameter
                        param_pesoxunidad.ParameterName = "@pesoxunidad"
                        param_pesoxunidad.SqlDbType = SqlDbType.Decimal
                        param_pesoxunidad.Precision = 18
                        param_pesoxunidad.Scale = 2
                        param_pesoxunidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PesoxUnidad).Value
                        param_pesoxunidad.Direction = ParameterDirection.Input

                        Dim param_preciolista As New SqlClient.SqlParameter
                        param_preciolista.ParameterName = "@preciolista"
                        param_preciolista.SqlDbType = SqlDbType.Decimal
                        param_preciolista.Precision = 18
                        param_preciolista.Scale = 2
                        param_preciolista.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioReal).Value
                        param_preciolista.Direction = ParameterDirection.Input

                        Dim param_preciovtasiniva As New SqlClient.SqlParameter
                        param_preciovtasiniva.ParameterName = "@PrecioVentaSinIva"
                        param_preciovtasiniva.SqlDbType = SqlDbType.Decimal
                        param_preciovtasiniva.Precision = 18
                        param_preciovtasiniva.Scale = 2
                        param_preciovtasiniva.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value / grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_preciovtasiniva.Direction = ParameterDirection.Input

                        Dim param_iva As New SqlClient.SqlParameter
                        param_iva.ParameterName = "@Iva"
                        param_iva.SqlDbType = SqlDbType.Decimal
                        param_iva.Precision = 18
                        param_iva.Scale = 2
                        param_iva.Value = 1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.IVA).Value / 100)
                        param_iva.Direction = ParameterDirection.Input

                        Dim param_ActualizarPrecio As New SqlClient.SqlParameter
                        param_ActualizarPrecio.ParameterName = "@ActualizarPrecio"
                        param_ActualizarPrecio.SqlDbType = SqlDbType.Bit
                        param_ActualizarPrecio.Value = ActualizarPrecio
                        param_ActualizarPrecio.Direction = ParameterDirection.Input

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
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Recepciones_Det_Insert2", _
                                                    param_id, param_codigo, param_idRecepcion, param_idmaterial, _
                                                    param_qty, param_idunidad, param_remito, param_useradd, param_Nuevo, _
                                                    param_idordendecompra, param_idordendecompradet, param_material, _
                                                    param_idmoneda, param_bonificacion1, param_bonificacion2, param_bonificacion3, _
                                                    param_bonificacion4, param_bonificacion5, param_ganancia, param_precioxmt, _
                                                    param_precioxkg, param_pesoxmetro, param_cantxlongitud, param_pesoxunidad, _
                                                    param_preciolista, param_preciovtasiniva, param_iva, param_ActualizarPrecio, _
                                                    param_res, param_msg)

                            'MsgBox(param_msg.Value.ToString)

                            res = param_res.Value
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

    Private Function AgregarActualizar_Registro() As Integer
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

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_idOC As New SqlClient.SqlParameter
                param_idOC.ParameterName = "@idOC"
                param_idOC.SqlDbType = SqlDbType.BigInt
                param_idOC.Value = cmbOrdenDeCompra.SelectedValue
                param_idOC.Direction = ParameterDirection.Input

                Dim param_NroOC As New SqlClient.SqlParameter
                param_NroOC.ParameterName = "@NroOC"
                param_NroOC.SqlDbType = SqlDbType.VarChar
                param_NroOC.Size = 25
                param_NroOC.Value = cmbOrdenDeCompra.Text
                param_NroOC.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.BigInt
                param_idproveedor.Value = cmbProveedor.SelectedValue
                param_idproveedor.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_remito As New SqlClient.SqlParameter
                param_remito.ParameterName = "@remito"
                param_remito.SqlDbType = SqlDbType.VarChar
                param_remito.Size = 30
                param_remito.Value = txtRemito.Text
                param_remito.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Recepciones_Insert", _
                                              param_id, param_idOC, param_NroOC, param_codigo, param_fecha, param_idproveedor, _
                                              param_nota, param_remito, param_useradd, param_res)

                        txtID.Text = param_id.Value
                        txtCODIGO.Text = param_id.Value
                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Recepciones_Update", _
                                              param_id, param_fecha, _
                                              param_nota, param_remito, param_useradd, param_res)

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

    Private Function Agregar_Gasto(ByVal Flete As Boolean) As Integer
        Dim res As Integer = 0

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_idrecepcion As New SqlClient.SqlParameter
            param_idrecepcion.ParameterName = "@idRecepcion"
            param_idrecepcion.SqlDbType = SqlDbType.BigInt
            param_idrecepcion.Value = txtID.Text
            param_idrecepcion.Direction = ParameterDirection.Input

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.InputOutput

            Dim param_fecha As New SqlClient.SqlParameter
            param_fecha.ParameterName = "@fechagasto"
            param_fecha.SqlDbType = SqlDbType.DateTime
            param_fecha.Value = dtpFECHA.Value
            param_fecha.Direction = ParameterDirection.Input

            Dim param_idproveedor As New SqlClient.SqlParameter
            param_idproveedor.ParameterName = "@idproveedor"
            param_idproveedor.SqlDbType = SqlDbType.BigInt
            If Flete = True Then
                param_idproveedor.Value = cmbProveedorFlete.SelectedValue
            Else
                param_idproveedor.Value = cmbProveedor.SelectedValue
            End If
            param_idproveedor.Direction = ParameterDirection.Input

            Dim param_nota As New SqlClient.SqlParameter
            param_nota.ParameterName = "@descripcion"
            param_nota.SqlDbType = SqlDbType.VarChar
            param_nota.Size = 300
            param_nota.Value = txtNota.Text
            param_nota.Direction = ParameterDirection.Input

            Dim param_factura As New SqlClient.SqlParameter
            param_factura.ParameterName = "@nrofactura"
            param_factura.SqlDbType = SqlDbType.VarChar
            param_factura.Size = 20
            If Flete = True Then
                param_factura.Value = txtFacturaFlete.Text
            Else
                param_factura.Value = txtNroFactura.Text
            End If
            param_factura.Direction = ParameterDirection.Input

            Dim param_tipofactura As New SqlClient.SqlParameter
            param_tipofactura.ParameterName = "@tipofact"
            param_tipofactura.SqlDbType = SqlDbType.VarChar
            param_tipofactura.Size = 1
            If Flete = True Then
                param_tipofactura.Value = cmbTipoFactFlete.Text
            Else
                param_tipofactura.Value = cmbTipoFact.Text
            End If
            param_tipofactura.Direction = ParameterDirection.Input

            Dim param_iva As New SqlClient.SqlParameter
            param_iva.ParameterName = "@iva"
            param_iva.SqlDbType = SqlDbType.Decimal
            param_iva.Precision = 18
            param_iva.Scale = 2
            If Flete = True Then
                param_iva.Value = IIf(txtMontoIvaFlete.Text = "", 0, txtMontoIvaFlete.Text)
            Else
                param_iva.Value = IIf(txtMontoIVA.Text = "", 0, txtMontoIVA.Text)
            End If
            param_iva.Direction = ParameterDirection.Input

            Dim param_subtotal As New SqlClient.SqlParameter
            param_subtotal.ParameterName = "@subtotal"
            param_subtotal.SqlDbType = SqlDbType.Decimal
            param_subtotal.Precision = 18
            param_subtotal.Scale = 2
            If Flete = True Then
                param_subtotal.Value = IIf(txtSubtotalFlete.Text = "", 0, txtSubtotalFlete.Text)
            Else
                param_subtotal.Value = IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)
            End If
            param_subtotal.Direction = ParameterDirection.Input

            Dim param_total As New SqlClient.SqlParameter
            param_total.ParameterName = "@total"
            param_total.SqlDbType = SqlDbType.Decimal
            param_total.Precision = 18
            param_total.Scale = 2
            If Flete = True Then
                param_total.Value = IIf(txtTotalFlete.Text = "", 0, txtTotalFlete.Text)
            Else
                param_total.Value = IIf(txtTotal.Text = "", 0, txtTotal.Text)
            End If
            param_total.Direction = ParameterDirection.Input

            Dim param_deuda As New SqlClient.SqlParameter
            param_deuda.ParameterName = "@deuda"
            param_deuda.SqlDbType = SqlDbType.Decimal
            param_deuda.Precision = 18
            param_deuda.Scale = 2
            If Flete = True Then
                param_deuda.Value = IIf(chkFleteSaldado.Checked = False, txtTotalFlete.Text, 0)
            Else
                param_deuda.Value = IIf(chkFacturaCancelada.Checked = False, txtTotal.Text, 0)
            End If
            param_deuda.Direction = ParameterDirection.Input

            Dim param_cancelada As New SqlClient.SqlParameter
            param_cancelada.ParameterName = "@Cancelado"
            param_cancelada.SqlDbType = SqlDbType.Bit
            If Flete = True Then
                param_cancelada.Value = chkFleteSaldado.Checked
            Else
                param_cancelada.Value = chkFacturaCancelada.Checked
            End If
            param_cancelada.Direction = ParameterDirection.Input

            Dim param_IIBB As New SqlClient.SqlParameter
            param_IIBB.ParameterName = "@IIBB"
            param_IIBB.SqlDbType = SqlDbType.Decimal
            param_IIBB.Precision = 18
            param_IIBB.Scale = 2
            If Flete = True Then
                param_IIBB.Value = 0
            Else
                param_IIBB.Value = IIf(txtIIBB.Text = "", 0, txtIIBB.Text)
            End If
            param_IIBB.Direction = ParameterDirection.Input

            Dim param_OtrosImp As New SqlClient.SqlParameter
            param_OtrosImp.ParameterName = "@OtrosImp"
            param_OtrosImp.SqlDbType = SqlDbType.Decimal
            param_OtrosImp.Precision = 18
            param_OtrosImp.Scale = 2
            If Flete = True Then
                param_OtrosImp.Value = 0
            Else
                param_OtrosImp.Value = IIf(txtOtrosImp.Text = "", 0, txtOtrosImp.Text)
            End If
            param_OtrosImp.Direction = ParameterDirection.Input

            Dim param_TipoGasto As New SqlClient.SqlParameter
            param_TipoGasto.ParameterName = "@TipoGasto"
            param_TipoGasto.SqlDbType = SqlDbType.VarChar
            param_TipoGasto.Size = 50
            If Flete = True Then
                param_TipoGasto.Value = "fle"
            Else
                param_TipoGasto.Value = "mat"
            End If
            param_TipoGasto.Direction = ParameterDirection.Input

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
                If bolModo = True Then
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Insert", _
                                    param_id, param_idrecepcion, param_codigo, param_fecha, param_idproveedor, _
                                    param_nota, param_factura, param_subtotal, param_iva, param_total, _
                                    param_deuda, param_tipofactura, param_cancelada, _
                                    param_IIBB, param_OtrosImp, param_TipoGasto, param_useradd, param_res)

                    txtIdGasto.Text = param_id.Value

                Else

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spGastos_Update", _
                                    param_id, param_idrecepcion, param_codigo, param_fecha, param_idproveedor, _
                                    param_nota, param_factura, param_subtotal, param_iva, param_total, _
                                    param_deuda, param_tipofactura, param_cancelada, _
                                    param_IIBB, param_OtrosImp, param_useradd, param_res)

                    txtIdGasto.Text = param_id.Value


                End If

                Agregar_Gasto = param_res.Value

            Catch ex As Exception
                Throw ex
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

    Private Function Agregar_Pago(ByVal Flete As Boolean) As Integer
        Dim res As Integer = 0

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

            Dim param_idGasto As New SqlClient.SqlParameter
            param_idGasto.ParameterName = "@idgasto"
            param_idGasto.SqlDbType = SqlDbType.BigInt
            param_idGasto.Value = txtIdGasto.Text
            param_idGasto.Direction = ParameterDirection.Input

            Dim param_fecha As New SqlClient.SqlParameter
            param_fecha.ParameterName = "@fechaPago"
            param_fecha.SqlDbType = SqlDbType.DateTime
            param_fecha.Value = dtpFECHA.Value
            param_fecha.Direction = ParameterDirection.Input

            Dim param_idProveedor As New SqlClient.SqlParameter
            param_idProveedor.ParameterName = "@idProveedor"
            param_idProveedor.SqlDbType = SqlDbType.BigInt
            If Flete = True Then
                param_idProveedor.Value = cmbProveedorFlete.SelectedValue
            Else
                param_idProveedor.Value = cmbProveedor.SelectedValue
            End If
            param_idProveedor.Direction = ParameterDirection.Input

            Dim param_nota As New SqlClient.SqlParameter
            param_nota.ParameterName = "@Nota"
            param_nota.SqlDbType = SqlDbType.VarChar
            param_nota.Size = 300
            param_nota.Value = txtNota.Text
            param_nota.Direction = ParameterDirection.Input

            Dim param_contado As New SqlClient.SqlParameter
            param_contado.ParameterName = "@Contado"
            param_contado.SqlDbType = SqlDbType.Bit
            param_contado.Value = True
            param_contado.Direction = ParameterDirection.Input

            Dim param_montoContado As New SqlClient.SqlParameter
            param_montoContado.ParameterName = "@MontoContado"
            param_montoContado.SqlDbType = SqlDbType.Decimal
            param_montoContado.Precision = 18
            param_montoContado.Scale = 2
            param_montoContado.Value = IIf(txtTotal.Text = "", 0, txtTotal.Text)
            param_montoContado.Direction = ParameterDirection.Input

            Dim param_tarjeta As New SqlClient.SqlParameter
            param_tarjeta.ParameterName = "@tarjeta"
            param_tarjeta.SqlDbType = SqlDbType.Bit
            param_tarjeta.Value = False
            param_tarjeta.Direction = ParameterDirection.Input

            Dim param_nombretarjeta As New SqlClient.SqlParameter
            param_nombretarjeta.ParameterName = "@NombreTarjeta"
            param_nombretarjeta.SqlDbType = SqlDbType.VarChar
            param_nombretarjeta.Size = 50
            param_nombretarjeta.Value = ""
            param_nombretarjeta.Direction = ParameterDirection.Input

            Dim param_montotarjeta As New SqlClient.SqlParameter
            param_montotarjeta.ParameterName = "@montotarjeta"
            param_montotarjeta.SqlDbType = SqlDbType.Decimal
            param_montotarjeta.Precision = 18
            param_montotarjeta.Scale = 2
            param_montotarjeta.Value = 0
            param_montotarjeta.Direction = ParameterDirection.Input

            Dim param_cheque As New SqlClient.SqlParameter
            param_cheque.ParameterName = "@cheque"
            param_cheque.SqlDbType = SqlDbType.Bit
            param_cheque.Value = False
            param_cheque.Direction = ParameterDirection.Input

            Dim param_montocheque As New SqlClient.SqlParameter
            param_montocheque.ParameterName = "@montocheque"
            param_montocheque.SqlDbType = SqlDbType.Decimal
            param_montocheque.Precision = 18
            param_montocheque.Scale = 2
            param_montocheque.Value = 0
            param_montocheque.Direction = ParameterDirection.Input

            Dim param_transferencia As New SqlClient.SqlParameter
            param_transferencia.ParameterName = "@transferencia"
            param_transferencia.SqlDbType = SqlDbType.Bit
            param_transferencia.Value = False
            param_transferencia.Direction = ParameterDirection.Input

            Dim param_montotransf As New SqlClient.SqlParameter
            param_montotransf.ParameterName = "@montotransf"
            param_montotransf.SqlDbType = SqlDbType.Decimal
            param_montotransf.Precision = 18
            param_montotransf.Scale = 2
            param_montotransf.Value = 0
            param_montotransf.Direction = ParameterDirection.Input

            Dim param_impuestos As New SqlClient.SqlParameter
            param_impuestos.ParameterName = "@impuestos"
            param_impuestos.SqlDbType = SqlDbType.Bit
            param_impuestos.Value = False
            param_impuestos.Direction = ParameterDirection.Input

            Dim param_montoimpuesto As New SqlClient.SqlParameter
            param_montoimpuesto.ParameterName = "@montoimpuesto"
            param_montoimpuesto.SqlDbType = SqlDbType.Decimal
            param_montoimpuesto.Precision = 18
            param_montoimpuesto.Scale = 2
            param_montoimpuesto.Value = 0
            param_montoimpuesto.Direction = ParameterDirection.Input

            Dim param_montoiva As New SqlClient.SqlParameter
            param_montoiva.ParameterName = "@montoiva"
            param_montoiva.SqlDbType = SqlDbType.Decimal
            param_montoiva.Precision = 18
            param_montoiva.Scale = 2
            param_montoiva.Value = CDbl(txtMontoIVA.Text)
            param_montoiva.Direction = ParameterDirection.Input

            Dim param_subtotal As New SqlClient.SqlParameter
            param_subtotal.ParameterName = "@subtotal"
            param_subtotal.SqlDbType = SqlDbType.Decimal
            param_subtotal.Precision = 18
            param_subtotal.Scale = 2
            param_subtotal.Value = CDbl(txtSubtotal.Text)
            param_subtotal.Direction = ParameterDirection.Input

            Dim param_total As New SqlClient.SqlParameter
            param_total.ParameterName = "@total"
            param_total.SqlDbType = SqlDbType.Decimal
            param_total.Precision = 18
            param_total.Scale = 2
            param_total.Value = CDbl(txtTotal.Text)
            param_total.Direction = ParameterDirection.Input

            Dim param_Redondeo As New SqlClient.SqlParameter
            param_Redondeo.ParameterName = "@Redondeo"
            param_Redondeo.SqlDbType = SqlDbType.Decimal
            param_Redondeo.Precision = 18
            param_Redondeo.Scale = 2
            param_Redondeo.Value = 0
            param_Redondeo.Direction = ParameterDirection.Input

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

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Insert", _
                                            param_id, param_codigo, param_idProveedor, param_fecha, param_contado, param_montoContado, _
                                            param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, _
                                            param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                            param_montoiva, param_subtotal, param_total, param_Redondeo, param_nota, _
                                            param_useradd, param_res)
                'param_id, param_codigo, param_idGasto, param_fecha, param_nota, _
                'param_contado, param_montoContado, param_tarjeta, param_nombretarjeta, _
                'param_montotarjeta, param_cheque, param_useradd, param_res)

                txtidpago.Text = param_id.Value

                Agregar_Pago = param_res.Value

            Catch ex As Exception
                Throw ex
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

    Private Function AgregarRegistro_PagosGastos() As Integer
        Dim res As Integer = 0

        Try

            Dim param_idPago As New SqlClient.SqlParameter
            param_idPago.ParameterName = "@idPago"
            param_idPago.SqlDbType = SqlDbType.BigInt
            param_idPago.Value = txtidpago.Text
            param_idPago.Direction = ParameterDirection.Input

            Dim param_idGasto As New SqlClient.SqlParameter
            param_idGasto.ParameterName = "@idGasto"
            param_idGasto.SqlDbType = SqlDbType.BigInt
            param_idGasto.Value = txtIdGasto.Text
            param_idGasto.Direction = ParameterDirection.Input

            Dim param_DEUDA As New SqlClient.SqlParameter
            param_DEUDA.ParameterName = "@Deuda"
            param_DEUDA.SqlDbType = SqlDbType.Decimal
            param_DEUDA.Precision = 18
            param_DEUDA.Scale = 2
            param_DEUDA.Value = 0
            param_DEUDA.Direction = ParameterDirection.Input

            Dim param_CancelarTodo As New SqlClient.SqlParameter
            param_CancelarTodo.ParameterName = "@CancelarTodo"
            param_CancelarTodo.SqlDbType = SqlDbType.Bit
            param_CancelarTodo.Value = True
            param_CancelarTodo.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPagos_Gastos_Insert", _
                                          param_idPago, param_idGasto, param_DEUDA, param_CancelarTodo, param_res)

                res = param_res.Value

            Catch ex As Exception
                Throw ex
            End Try

            AgregarRegistro_PagosGastos = res

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Recepciones_Delete_All", param_idRecepcion, param_userdel, param_res, param_msg)
                    res = param_res.Value
                    If Not (param_msg.Value Is System.DBNull.Value) Then
                        msg = param_msg.Value
                    Else
                        msg = ""
                    End If

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
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value Is Nothing) _
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

    Private Function LiberarMPNro(ByVal propio As Long) As String
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                LiberarMPNro = "error"
                Return LiberarMPNro
                Exit Function
            End Try

            Try

                Dim param_nro As New SqlClient.SqlParameter
                param_nro.ParameterName = "@nro"
                param_nro.SqlDbType = SqlDbType.BigInt
                param_nro.Value = propio
                param_nro.Direction = ParameterDirection.Input

                Dim param_userupd As New SqlClient.SqlParameter
                param_userupd.ParameterName = "@user"
                param_userupd.SqlDbType = SqlDbType.Int
                param_userupd.Value = Util.UserID
                param_userupd.Direction = ParameterDirection.Input


                Dim param_mensaje As New SqlClient.SqlParameter
                param_mensaje.ParameterName = "@mensaje"
                param_mensaje.SqlDbType = SqlDbType.VarChar
                param_mensaje.Size = 500
                param_mensaje.Value = DBNull.Value
                param_mensaje.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spLiberarStockNro", param_nro, param_userupd, param_mensaje)
                    LiberarMPNro = param_mensaje.Value
                    Return LiberarMPNro

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

            LiberarMPNro = "error"
            Return LiberarMPNro

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IDRecepcion_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IDRecepcion_Det)
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

        band = 0

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Caso especial, se habilitan porque se inhabilitaron por el caso de un eliminado
        chkEliminado.Checked = False

        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        txtNota.Enabled = True
        ' fin caso especial
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()

        cmbProveedor.Visible = bolModo
        txtProveedor.Visible = Not bolModo

        cmbOrdenDeCompra.Visible = bolModo
        txtOC.Visible = Not bolModo

        txtMontoIva.Text = ""
        txtMontoIvaFlete.Text = ""
        txtTotal.Text = ""
        txtTotalFlete.Text = ""

        chkCargarGasto.Enabled = True

        chkFacturaCancelada.Checked = False

        chkFlete.Enabled = True

        chkFlete.Checked = False
        chkFleteSaldado.Checked = False

        dtpFECHA.Focus()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer, res_item As Integer

        If chkCargarGasto.Checked = True Then
            If cmbTipoFact.Text <> "" And txtNroFactura.Text = "" Then
                Util.MsgStatus(Status1, "Debe Ingresar el nro de la factura.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe Ingresar el nro de la factura.", My.Resources.Resources.stop_error.ToBitmap, True)
                txtNroFactura.Focus()
            End If

            If txtSubtotal.Text = "" Then
                Util.MsgStatus(Status1, "Debe Ingresar el monto de la factura.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe Ingresar el monto de la factura.", My.Resources.Resources.stop_error.ToBitmap, True)
                txtSubtotal.Focus()
            End If

        End If


        If chkFlete.Checked = True Then
            If cmbTipoFactFlete.Text <> "" And txtFacturaFlete.Text = "" Then
                Util.MsgStatus(Status1, "Debe Ingresar el nro de la factura del flete.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe Ingresar el nro de la factura del flete.", My.Resources.Resources.stop_error.ToBitmap, True)
                txtFacturaFlete.Focus()
            End If

            If txtSubtotalFlete.Text = "" Then
                Util.MsgStatus(Status1, "Debe Ingresar el monto de la factura del flete.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe Ingresar el monto de la factura del flete.", My.Resources.Resources.stop_error.ToBitmap, True)
                txtSubtotalFlete.Focus()
            End If

            If cmbProveedorFlete.Text = "" Then
                Util.MsgStatus(Status1, "Debe Ingresar el proveedor del flete.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe Ingresar el proveedor del flete.", My.Resources.Resources.stop_error.ToBitmap, True)
                cmbProveedorFlete.Focus()
            End If

        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                    res = AgregarActualizar_Registro()
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
                            If bolModo = True Then
                                res_item = AgregarRegistroItems(txtID.Text)
                                Select Case res_item
                                    Case -7
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo actualizar el precio de un producto", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo actualizar el precio de un producto", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case -6
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo registrar la transaccion (items)", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case -5
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case -4
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case -3
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap)
                                        Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap, True)
                                    Case -2
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap)
                                        Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.alert.ToBitmap, True)
                                    Case -1
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar la recepción (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo registrar la recepción (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case Else
                                        'Util.AgregarGrilla(grd, Me, Permitir) 'agregar el nuevo registro a la grilla

                                        If chkCargarGasto.Checked = True And bolModo = True Then
                                            res = Agregar_Gasto(False)
                                            Select Case res
                                                Case -1
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Exit Sub
                                                Case 0
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Exit Sub
                                                Case Else
                                                    Util.MsgStatus(Status1, "Se registro correctamente el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                            End Select

                                        End If

                                        If chkFlete.Checked = True And bolModo = True Then
                                            res = Agregar_Gasto(True)
                                            Select Case res
                                                Case -1
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Exit Sub
                                                Case 0
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                                    Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Exit Sub
                                                Case Else
                                                    Util.MsgStatus(Status1, "Se registro correctamente el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                            End Select

                                        End If


                                        Cerrar_Tran()

                                        If chkFacturaCancelada.Checked = True Then
                                            frmPagodeGastos_Recepciones.Formulario = "Pago"
                                            frmPagodeGastos_Recepciones.btnNuevo_Click(sender, e)

                                            frmPagodeGastos_Recepciones.lblTotalaPagar.Text = txtTotal.Text  ' totalfactura.ToString
                                            frmPagodeGastos_Recepciones.lblTotalaPagarSinIva.Text = txtSubtotal.Text 'subtotal.ToString
                                            frmPagodeGastos_Recepciones.IdProveedor = cmbProveedor.SelectedValue
                                            frmPagodeGastos_Recepciones.FechaVta = dtpFECHA.Value
                                            frmPagodeGastos_Recepciones.MontoIva = txtMontoIva.Text  'totaliva.ToString
                                            frmPagodeGastos_Recepciones.PorcIva = txtPorcIva.Text
                                            frmPagodeGastos_Recepciones.txtIdGasto.Text = txtID.Text ' idfactura.ToString
                                            frmPagodeGastos_Recepciones.ShowDialog(Me)
                                        End If

                                        If chkFleteSaldado.Checked = True Then
                                            frmPagodeGastos_Recepciones.Formulario = "Flete"
                                            frmPagodeGastos_Recepciones.btnNuevo_Click(sender, e)

                                            frmPagodeGastos_Recepciones.lblTotalaPagar.Text = txtTotalFlete.Text  ' totalfactura.ToString
                                            frmPagodeGastos_Recepciones.lblTotalaPagarSinIva.Text = txtSubtotalFlete.Text 'subtotal.ToString
                                            frmPagodeGastos_Recepciones.IdProveedor = cmbProveedorFlete.SelectedValue
                                            frmPagodeGastos_Recepciones.FechaVta = dtpFECHA.Value
                                            frmPagodeGastos_Recepciones.MontoIva = txtMontoIvaFlete.Text  'totaliva.ToString
                                            frmPagodeGastos_Recepciones.PorcIva = txtPorcIvaFlete.Text
                                            frmPagodeGastos_Recepciones.txtIdGasto.Text = txtID.Text ' idfactura.ToString
                                            frmPagodeGastos_Recepciones.ShowDialog(Me)
                                        End If

                                        bolModo = False
                                        PrepararBotones()
                                        btnActualizar_Click(sender, e)

                                        Setear_Grilla()

                                        chkCargarGasto.Checked = False

                                        'Me.Label5.Visible = False
                                        cmbProveedor.Visible = False
                                        txtProveedor.Visible = True
                                        cmbOrdenDeCompra.Visible = False
                                        txtOC.Visible = True
                                        btnRecibirTodos.Enabled = False

                                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap, True, True)
                                End Select
                            Else
                                If chkFlete.Checked = True Then
                                    res = Agregar_Gasto(True)
                                    Select Case res
                                        Case -1
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                            Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                            Exit Sub
                                        Case 0
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                            Util.MsgStatus(Status1, "No se pudo registrar el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap, True)
                                            Exit Sub
                                        Case Else
                                            Util.MsgStatus(Status1, "Se registro correctamente el ingreso del Gasto", My.Resources.Resources.stop_error.ToBitmap)
                                    End Select

                                End If


                                Cerrar_Tran()

                                bolModo = False
                                PrepararBotones()
                                btnActualizar_Click(sender, e)

                                Setear_Grilla()

                                chkCargarGasto.Checked = False

                                'Me.Label5.Visible = False
                                cmbProveedor.Visible = False
                                txtProveedor.Visible = True
                                cmbOrdenDeCompra.Visible = False
                                txtOC.Visible = True
                                btnRecibirTodos.Enabled = False

                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap, True, True)

                            End If
                    End Select
                    '
                    ' cerrar la conexion si está abierta.
                    '
                    If Not conn_del_form Is Nothing Then
                        CType(conn_del_form, IDisposable).Dispose()
                    End If
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
        'Dim res As Integer

        ''If BAJA Then
        'If chkEliminado.Checked = False Then
        '    If MessageBox.Show("Esta acción reversará los Recepciones de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        '        Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        '        res = EliminarRegistro()
        '        Select Case res
        '            Case -7
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se eliminó el ajuste porque algunos items quedarían negativos.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se eliminó el ajuste porque algunos items quedarían negativos.", My.Resources.stop_error.ToBitmap, True)
        '            Case -6
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se registró el ajuste.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se registró el ajuste.", My.Resources.stop_error.ToBitmap, True)
        '            Case -5
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se registró el detalle del ajuste.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se registró el detalle del ajuste.", My.Resources.stop_error.ToBitmap, True)
        '            Case -4
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap, True)
        '            Case -3
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap, True)
        '            Case -2
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap, True)
        '            Case -1
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
        '            Case 0
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap, True)
        '            Case Else
        '                Cerrar_Tran()
        '                PrepararBotones()
        '                btnActualizar_Click(sender, e)
        '                Setear_Grilla()
        '                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
        '                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True, True)
        '        End Select
        '    Else
        '        Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
        '    End If
        'Else
        '    Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
        '    Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        'End If
        ''Else
        ''Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        ''Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap, True)
        ''End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        'Dim ReporteMaestroRecepciones As New frmReportes()

        'Dim param As New frmParametros
        'Dim Cnn As New SqlConnection(ConnStringSEI)
        'Dim codigo_mat As String = "", codigo_recep As String = ""
        ' ''En esta Variable le paso la fecha actual
        'Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        'Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        'Dim reportealmacenes As New frmReportes

        'nbreformreportes = "Listado de Recepción de Material"

        'param.AgregarParametros("Recepción Nro.:", "STRING", "", False, txtCODIGO.Text.ToString, "", Cnn)
        'param.AgregarParametros("Material:", "STRING", "", False, , "", Cnn)
        'param.AgregarParametros("Desde:", "DATE", "", False, Inicial, "", Cnn)
        'param.AgregarParametros("Hasta:", "DATE", "", False, Final, "", Cnn)

        'param.ShowDialog()
        ' ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
        ' ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
        'If cerroparametrosconaceptar = True Then
        '    ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR A LA FUNCION..
        '    codigo_recep = param.ObtenerParametros(0)
        '    codigo_mat = param.ObtenerParametros(1)
        '    Inicial = param.ObtenerParametros(2)
        '    Final = param.ObtenerParametros(3)
        '    ReporteMaestroRecepciones.MostrarMaestroRecepciones(codigo_recep, codigo_mat, Inicial, Final, ReporteMaestroRecepciones)
        '    cerroparametrosconaceptar = False
        '    param = Nothing
        '    Cnn = Nothing
        'End If



    End Sub

    Private Sub btnLlenarGrilla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlenarGrilla.Click
        'Dim i As Integer

        If bolModo Then 'SOLO LLENA LA GRILLA EN MODO NUEVO...
            If Me.cmbOrdenDeCompra.Text <> "" Then
                PrepararGridItems()
                Try
                    LlenarGridItemsPorOC(CType(Me.cmbOrdenDeCompra.SelectedValue, Long))
                Catch ex As Exception

                End Try
                btnRecibirTodos.Enabled = True
                With (grdItems)
                    '.AllowUserToAddRows = True
                    .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False 'Codigo material
                    '.Columns(ColumnasDelGridItems.QtyRecep).ReadOnly = False 'qty
                    '.Columns(ColumnasDelGridItems.Remito).ReadOnly = False 'Remito
                    '.Columns(ColumnasDelGridItems.LoteProveed).ReadOnly = False 'Remito
                End With
            End If
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btnRecibirTodos.Enabled = False
        'chkAnulados.Enabled = Not bolModo
        cmbProveedor.Visible = bolModo
        cmbOrdenDeCompra.Visible = bolModo
        txtProveedor.Visible = Not bolModo
        txtOC.Visible = Not bolModo

        lblFactura.Enabled = False
        lblMontoIVA.Enabled = False
        lblSubtotal.Enabled = False
        LblTotal.Enabled = False
        chkCargarGasto.Enabled = False

        txtNroFactura.Enabled = False
        txtSubtotal.Enabled = False
        txtMontoIVA.Enabled = False
        txtTotal.Enabled = False

        chkFacturaCancelada.Enabled = False

        chkFleteSaldado.Enabled = False

        chkFlete.Enabled = False

        grd_CurrentCellChanged(sender, e)

        chkFleteSaldado.Enabled = False

    End Sub

#End Region

#Region "   GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            If e.ColumnIndex = ColumnasDelGridItems.CodUnidad Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_unidad As String, nombre As String = "", codunidad As String = ""
                Dim idunidad As Long

                cod_unidad = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerUnidad_App(cod_unidad, idunidad, nombre, codunidad, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodUnidad).Value = codunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = nombre

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                End If

            End If

            If e.ColumnIndex = ColumnasDelGridItems.CodMoneda Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_moneda As String, nombre As String = "", codmoneda As String = ""
                Dim idmoneda As Long
                Dim valorcambio As Double

                cod_moneda = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerMoneda_App(cod_moneda, idmoneda, nombre, codmoneda, valorcambio, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMoneda).Value = idmoneda
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodMoneda).Value = codmoneda
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Moneda).Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value = valorcambio

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                End If

            End If

            If e.ColumnIndex = ColumnasDelGridItems.PrecioxKg Or e.ColumnIndex = ColumnasDelGridItems.CantxLongitud Or _
                e.ColumnIndex = ColumnasDelGridItems.PesoxUnidad Or _
                e.ColumnIndex = ColumnasDelGridItems.Bonif1 Or e.ColumnIndex = ColumnasDelGridItems.Bonif2 Or _
                e.ColumnIndex = ColumnasDelGridItems.Bonif3 Or e.ColumnIndex = ColumnasDelGridItems.Bonif4 Or _
                e.ColumnIndex = ColumnasDelGridItems.Bonif5 Or _
                e.ColumnIndex = ColumnasDelGridItems.Ganancia Or _
                e.ColumnIndex = ColumnasDelGridItems.PrecioxMt Or _
                e.ColumnIndex = ColumnasDelGridItems.PrecioReal Or _
                e.ColumnIndex = ColumnasDelGridItems.IVA Then

                Dim Bonif1 As Double, Bonif2 As Double, Bonif3 As Double, Bonif4 As Double, Bonif5 As Double
                Dim precioxkg As Double, cantxlongitud As Double, pesoxunidad As Double, preciolista As Double, precioxmt As Double
                Dim preciobonif1 As Double = 0, preciobonif2 As Double = 0, preciobonif3 As Double = 0, preciobonif4 As Double = 0, preciobonif5 As Double = 0
                Dim preciosinivabonif As Double = 0

                Dim Ganancia As Double
                Dim cell As DataGridViewCell = grdItems.CurrentCell

                Ganancia = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value), Double)) / 100

                precioxmt = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value)
                precioxkg = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value)
                cantxlongitud = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantxLongitud).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantxLongitud).Value)
                pesoxunidad = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value)

                If precioxkg = 0 And cantxlongitud = 0 And pesoxunidad = 0 And precioxmt = 0 Then

                    Bonif1 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value), Double)) / 100
                    Bonif2 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value), Double)) / 100
                    Bonif3 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value), Double)) / 100
                    Bonif4 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value), Double)) / 100
                    Bonif5 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value), Double)) / 100

                Else

                    Bonif1 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value), Double)) / 100
                    Bonif2 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value), Double)) / 100
                    Bonif3 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value), Double)) / 100
                    Bonif4 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value), Double)) / 100
                    Bonif5 = 1 + (CType(IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value), Double)) / 100

                    Dim CalcPesoxUnidad As Double = 0, Calcprecioxunidad As Double = 0, Calcprecioxkg As Double = 0, CalcPrecioxMt As Double = 0

                    If precioxkg <> 0 Then
                        Calcprecioxkg = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value = Nothing, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxKg).Value)
                        preciobonif1 = precioxkg / Bonif1
                    Else
                        CalcPrecioxMt = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value = Nothing, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value)
                        preciobonif1 = precioxmt / Bonif1
                    End If

                    'MsgBox(preciobonif1)

                    preciobonif1 = preciobonif1 / Bonif2
                    preciobonif1 = preciobonif1 / Bonif3
                    preciobonif1 = preciobonif1 / Bonif4
                    preciobonif1 = preciobonif1 / Bonif5

                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value = 0
                    End If

                    preciosinivabonif = preciobonif1 / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value / 100))

                    'MsgBox(preciobonif1)

                    'MsgBox(preciosinivabonif)

                    If precioxkg <> 0 Then
                        Calcprecioxunidad = preciosinivabonif * IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PesoxUnidad).Value)
                    Else
                        Calcprecioxunidad = preciosinivabonif * IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioxMt).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CantxLongitud).Value)
                    End If

                    'MsgBox(Calcprecioxunidad)

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = Math.Round(Calcprecioxunidad, 2)

                    'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value = Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value * Ganancia, 2)

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value = Math.Round((Math.Round(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value * Ganancia, 2)) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value, 2)

                    Exit Sub
                End If

                preciolista = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value)

                preciobonif1 = preciolista / Bonif1
                preciobonif1 = preciobonif1 / Bonif2
                preciobonif1 = preciobonif1 / Bonif3
                preciobonif1 = preciobonif1 / Bonif4
                preciobonif1 = preciobonif1 / Bonif5

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value Is DBNull.Value Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value = 0
                End If

                preciosinivabonif = preciobonif1 / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value / 100))

                'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value = Math.Round(preciosinivabonif * Ganancia, 2)

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioEnPesosNuevo).Value = Math.Round((Math.Round(preciosinivabonif * Ganancia, 2)) * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value, 2)

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioPedido).Value <> grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Value = FormatNumber(100 - FormatNumber(CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioPedido).Value * 100 / grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioReal).Value), 2), 2)
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Value = 0
                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Value <> 0 Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Style.BackColor = Color.Red
                Else
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcDif).Style.BackColor = Color.White
                End If

            End If

        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub


#End Region


    Private Sub btnRecibirTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecibirTodos.Click

        If MessageBox.Show("Está seguro que desea copiar los valores de la columnas Cant. Saldo y Precio Pedido a las columnas Cant. Recepc y Precio Real?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim i As Integer

            For i = 0 To grdItems.RowCount - 1
                grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyRecep).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value
                grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioReal).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioPedido).Value
            Next

        End If
    End Sub

    Private Sub chkFlete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFlete.CheckedChanged
        lblFacturaFlete.Enabled = chkFlete.Checked
        lblMontoIvaFlete.Enabled = chkFlete.Checked
        lblSubtotalFlete.Enabled = chkFlete.Checked
        lblTotalFlete.Enabled = chkFlete.Checked
        txtFacturaFlete.Enabled = chkFlete.Checked
        txtSubtotalFlete.Enabled = chkFlete.Checked
        txtMontoIvaFlete.Enabled = chkFlete.Checked
        txtTotalFlete.Enabled = chkFlete.Checked
        txtPorcIvaFlete.Enabled = chkFlete.Checked
        lblPorcIvaFlete.Enabled = chkFlete.Checked
        chkFleteSaldado.Enabled = IIf(bolModo = False, False, chkFlete.Checked)
        lblProveedorFlete.Enabled = chkFlete.Checked
        cmbProveedorFlete.Enabled = chkFlete.Checked
        cmbTipoFactFlete.Enabled = chkFlete.Checked
        lblTipoFactFlete.Enabled = chkFlete.Checked
    End Sub

    Private Sub cmbTipoFactFlete_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoFactFlete.SelectedIndexChanged
        If cmbTipoFactFlete.Text = "A" Then
            txtPorcIvaFlete.Text = "21"
        Else
            txtPorcIvaFlete.Text = "0"
        End If
    End Sub

    Private Sub cmbTipoFact_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoFact.SelectedIndexChanged
        If cmbTipoFact.Text = "A" Then
            txtPorcIva.Text = "21"
        Else
            txtPorcIva.Text = "0"
        End If
    End Sub

    Private Sub txtsubtotal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotal.KeyPress, txtPorcIva.KeyPress, _
        txtPorcIva.KeyPress, txtIIBB.KeyPress, txtOtrosImp.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If txtSubtotal.Text <> "" And txtPorcIva.Text <> "" Then
                txtMontoIva.Text = FormatNumber(CDbl(txtSubtotal.Text) * CDbl(txtPorcIva.Text) / 100, 2)
                If txtIIBB.Text = "" Then txtIIBB.Text = "0"
                If txtOtrosImp.Text = "" Then txtOtrosImp.Text = "0"

                txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtMontoIva.Text) + CDbl(txtIIBB.Text) + CDbl(txtOtrosImp.Text)
            End If
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotal.LostFocus, txtPorcIva.LostFocus, _
        txtPorcIva.LostFocus, cmbTipoFact.LostFocus, txtIIBB.LostFocus, txtOtrosImp.LostFocus
        If txtSubtotal.Text <> "" And txtPorcIva.Text <> "" Then
            txtMontoIva.Text = FormatNumber(CDbl(txtSubtotal.Text) * CDbl(txtPorcIva.Text) / 100, 2)
            If txtIIBB.Text = "" Then txtIIBB.Text = "0"
            If txtOtrosImp.Text = "" Then txtOtrosImp.Text = "0"

            txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtMontoIva.Text) + CDbl(txtIIBB.Text) + CDbl(txtOtrosImp.Text)
        End If
    End Sub

    Private Sub txtsubtotalFlete_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotalFlete.KeyPress, txtPorcIvaFlete.KeyPress, _
        txtPorcIvaFlete.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If txtSubtotalFlete.Text <> "" And txtPorcIvaFlete.Text <> "" Then
                txtMontoIvaFlete.Text = FormatNumber(CDbl(txtSubtotalFlete.Text) * CDbl(txtPorcIvaFlete.Text) / 100, 2)
                txtTotalFlete.Text = CDbl(txtSubtotalFlete.Text) + CDbl(txtMontoIvaFlete.Text)
            End If
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotalFlete_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotalFlete.LostFocus, txtPorcIvaFlete.LostFocus, _
        txtPorcIvaFlete.LostFocus, cmbTipoFactFlete.LostFocus
        If txtSubtotalFlete.Text <> "" And txtPorcIvaFlete.Text <> "" Then
            txtMontoIvaFlete.Text = FormatNumber(CDbl(txtSubtotalFlete.Text) * CDbl(txtPorcIvaFlete.Text) / 100, 2)
            txtTotalFlete.Text = CDbl(txtSubtotalFlete.Text) + CDbl(txtMontoIvaFlete.Text)
        End If
    End Sub


End Class