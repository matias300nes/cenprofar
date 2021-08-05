
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient



Public Class frmMateriales_ActualizacionPrecios
    Dim bolpoliticas As Boolean, band As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean

    Dim llenandoCombo As Boolean = False

    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet
    Dim dv As DataView

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
    Dim ActualizarUbicaciones As Boolean

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        ID_Det = 0
        Cod_Det = 1
        Cod_Material = 2
        Nombre_Material = 3
        PrecioCosto = 4

        ganan = 5
        preciov1 = 6
        ganan2 = 7
        preciov2 = 8

        Nota = 9
        Modificando = 10
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String
    'variable global para llenar la grilla
    Dim Sucursal As Integer

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient

#Region "   Eventos"

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click

        '        If UserActual.ToLower <> "administrador" Then
        '            Util.Logueado_OK = False
        '            Dim login As New Utiles.Login

        'logindenuevo:

        '            login.ShowDialog()

        '            If Not Util.Logueado_OK Then
        '                GoTo logindenuevo
        '            End If
        '        End If

    End Sub

    Private Sub frmAjustes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        btnSalir_Click(sender, e)
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

    Private Sub frmAjustes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        'oculto provisoriamente algunos componentes y deshabilito el boton de 
        btnEliminar.Visible = Not grdItems.ReadOnly
        cmbRubros.Visible = False
        cmbSubrubros.Visible = False
        btnEliminar.Visible = False
        btnAplicarMasivo.Enabled = False
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        band = False

        configurarform()
        asignarTags()

        SQL = "exec spMateriales_ActualizarPrecios_Select_All"

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

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
        End If

        GroupBox1.Enabled = bolModo
        txtFiltro.Enabled = bolModo
        dtpFECHA.MaxDate = Today.Date



        band = True

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
        'coloco el chk de modificado en falso
        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Modificando).Value = False
        Dim aux As Double = 0
        Dim valorCal As Double = 0

        'me fijo en la columna de precio de costo 
        If e.ColumnIndex = ColumnasDelGridItems.PrecioCosto Then
            Dim cell As DataGridViewCell = grdItems.CurrentCell

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioCosto).Value Is DBNull.Value Then
                Util.MsgStatus(Status1, "Debe ingresar un precio válido para Costo.", My.Resources.Resources.stop_error.ToBitmap, True)
                'grdItems.CurrentCell.Selected = True
                Exit Sub
            Else
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioCosto).Value = 0 Then
                    Util.MsgStatus(Status1, "Debe ingresar un precio válido para Costo.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            'calculo el valor de Ventas 1
            aux = (CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ganan).Value) / 100) + 1
            valorCal = CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioCosto).Value) * aux
            valorCal = Math.Round(valorCal, 2)
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciov1).Value = Math.Round(valorCal, 2)
            'calculo el valor de Ventas 2
            aux = (CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ganan2).Value) / 100) + 1
            valorCal = CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioCosto).Value) * aux
            valorCal = Math.Round(valorCal, 2)
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciov2).Value = Math.Round(valorCal, 2)

        End If
        If e.ColumnIndex = ColumnasDelGridItems.ganan Then
            Dim cell As DataGridViewCell = grdItems.CurrentCell

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.ganan).Value Is DBNull.Value Then
                Util.MsgStatus(Status1, "Debe ingresar un precio válido para Ganancia V1.", My.Resources.Resources.stop_error.ToBitmap, True)
                'grdItems.CurrentCell.Selected = True
                Exit Sub
            Else
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.ganan).Value = 0 Then
                    Util.MsgStatus(Status1, "Debe ingresar un precio válido para Ganancia V1.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            'calculo el valor de Ventas 1
            aux = (CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ganan).Value) / 100) + 1
            valorCal = CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioCosto).Value) * aux
            valorCal = Math.Round(valorCal, 2)
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciov1).Value = Math.Round(valorCal, 2)

        End If
        If e.ColumnIndex = ColumnasDelGridItems.ganan2 Then
            Dim cell As DataGridViewCell = grdItems.CurrentCell

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.ganan2).Value Is DBNull.Value Then
                Util.MsgStatus(Status1, "Debe ingresar un precio válido para Ganancia V1.", My.Resources.Resources.stop_error.ToBitmap, True)
                'grdItems.CurrentCell.Selected = True
                Exit Sub
            Else
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.ganan2).Value = 0 Then
                    Util.MsgStatus(Status1, "Debe ingresar un precio válido para Ganancia V1.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            'calculo el valor de Ventas 1
            aux = (CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ganan2).Value) / 100) + 1
            valorCal = CDbl(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioCosto).Value) * aux
            valorCal = Math.Round(valorCal, 2)
            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciov2).Value = Math.Round(valorCal, 2)

        End If
        'coloco el chk de modificado en falso
        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Modificando).Value = True



    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote
        'If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Lote Then
        '    AddHandler e.Control.KeyPress, AddressOf validarNumeros
        'End If

        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.PrecioCosto Then
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

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        If bolModo Then
            aplicar_busqueda()
        End If
    End Sub

    Private Sub chkActMasivo_CheckedChanged(sender As Object, e As EventArgs) Handles chkActMasivo.CheckedChanged
        If bolModo Then
            txtPorcentaje.Enabled = chkActMasivo.Checked
            btnAplicarMasivo.Enabled = chkActMasivo.Checked
            If chkActMasivo.Checked Then
                txtPorcentaje.Focus()
            Else
                txtPorcentaje.Text = ""
            End If
        End If
    End Sub

    Private Sub btnAplicarMasivo_Click(sender As Object, e As EventArgs) Handles btnAplicarMasivo.Click
        If txtPorcentaje.Text.ToString <> "" Then
            If CDbl(txtPorcentaje.Text) > 0 Then
                If MessageBox.Show("Está seguro que desea realizar una actualización del precio de costo de forma masiva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
                Dim porcen As Double = 1 + CDbl(txtPorcentaje.Text) / 100
                Dim i As Integer
                Dim aux As Double = 0
                Dim valorCal As Double = 0
            Try
                    For i = 0 To grdItems.Rows.Count - 1

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value * porcen, 2)

                          'calculo el valor de Ventas 1
                        aux = (CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan).Value) / 100) + 1
                        valorCal = CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value) * aux
                        valorCal = Math.Round(valorCal, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov1).Value = Math.Round(valorCal, 2)
                        'calculo el valor de Ventas 2
                        aux = (CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan2).Value) / 100) + 1
                        valorCal = CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value) * aux
                        valorCal = Math.Round(valorCal, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov2).Value = Math.Round(valorCal, 2)
                        'coloco el chk de modificado en true    
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Modificando).Value = True
                    Next
            Catch ex As Exception
                    'coloco el chk de modificado en true    
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Modificando).Value = False
                End Try
            End If
        End If
    End Sub

#End Region

#Region "   Procedimientos"

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
        dtpFECHA.Tag = "2"
        chkActMasivo.Tag = "3"
        txtPorcentaje.Tag = "4"
        txtNota.Tag = "5"
    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer 

        Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String
        'Dim ManejaLote, eninventario As Boolean
        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : ubicacion = "" : observaciones = ""

        bolpoliticas = False
        ActualizarUbicaciones = False

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
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is System.DBNull.Value _
                    Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is Nothing Then
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
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value Is System.DBNull.Value _
                    Or grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value Is Nothing Then
                    Util.MsgStatus(Status1, "Falta completar precio de costo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar precio de costo en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                If grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov1).Value Is System.DBNull.Value _
                   Or grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov1).Value Is Nothing Then
                    Util.MsgStatus(Status1, "Falta completar precio de Venta 1 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar precio de Venta 1  en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                If grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov2).Value Is System.DBNull.Value _
                   Or grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov2).Value Is Nothing Then
                    Util.MsgStatus(Status1, "Falta completar precio de Venta 2 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar precio de Venta 2 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                If grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan).Value Is System.DBNull.Value _
                   Or grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan).Value Is Nothing Then
                    Util.MsgStatus(Status1, "Falta completar ganancia V1 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar ganancia V1 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

                If grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan2).Value Is System.DBNull.Value _
                  Or grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan2).Value Is Nothing Then
                    Util.MsgStatus(Status1, "Falta completar ganancia V1 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar ganancia V2 en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If

            End If
        Next i

        i = 0
        Dim MovGrilla As Boolean = False

        For i = 0 To grdItems.RowCount - 1
            'qty es válida?
            Try
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Modificando).Value Then
                    MovGrilla = True
                    Exit For
                End If

            Catch ex As Exception
                Util.MsgStatus(Status1, "Valor incorrecto: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "Valor incorrecto: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End Try

        Next
        If MovGrilla = False Then
            Util.MsgStatus(Status1, "No se registró ningún movimiento en la grilla. Por favor, verifique", My.Resources.Resources.alert.ToBitmap, False)
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
        If columna = ColumnasDelGridItems.PrecioCosto Then

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
            .Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'Codigo material
        End With
    End Sub

    Private Sub LlenarGridItems()

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        If bolModo = True Then

            SQL = "spMateriales_ActualizarPrecios_Det_Select_All"

        Else
            SQL = "exec spMateriales_ActualizarPrecios_Det_Select_By_IDActPre @idActPre = " & IIf(txtID.Text = "", 0, txtID.Text)
        End If

        GetDatasetItems()

        If grdItems.Rows.Count > 0 Then

         
            grdItems.Columns(ColumnasDelGridItems.ID_Det).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Modificando).Visible = False


            grdItems.Columns(ColumnasDelGridItems.Cod_Det).ReadOnly = True 'Codigo de Ajuste_Det
            grdItems.Columns(ColumnasDelGridItems.Cod_Det).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Cod_Det).Visible = Not bolModo

            grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = True 'id de material
            grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 80
            grdItems.Columns(ColumnasDelGridItems.Cod_Material).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True 'Material
            grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 300

            'grdItems.Columns(ColumnasDelGridItems.ganan).Visible = bolModo
            grdItems.Columns(ColumnasDelGridItems.ganan).Width = 50
            grdItems.Columns(ColumnasDelGridItems.ganan).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            grdItems.Columns(ColumnasDelGridItems.ganan).ReadOnly = False

            'grdItems.Columns(ColumnasDelGridItems.preciov1).Visible = bolModo
            grdItems.Columns(ColumnasDelGridItems.preciov1).Width = 50
            grdItems.Columns(ColumnasDelGridItems.preciov1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            grdItems.Columns(ColumnasDelGridItems.preciov1).ReadOnly = True

            'grdItems.Columns(ColumnasDelGridItems.ganan2).Visible = bolModo
            grdItems.Columns(ColumnasDelGridItems.ganan2).Width = 50
            grdItems.Columns(ColumnasDelGridItems.ganan2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            grdItems.Columns(ColumnasDelGridItems.ganan2).ReadOnly = False

            'grdItems.Columns(ColumnasDelGridItems.preciov2).Visible = bolModo
            grdItems.Columns(ColumnasDelGridItems.preciov2).Width = 50
            grdItems.Columns(ColumnasDelGridItems.preciov2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            grdItems.Columns(ColumnasDelGridItems.preciov2).ReadOnly = True



            grdItems.Columns(ColumnasDelGridItems.PrecioCosto).Width = 60
            grdItems.Columns(ColumnasDelGridItems.PrecioCosto).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

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
        End If


        SQL = "exec spMateriales_ActualizarPrecios_Select_All"

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
            dv = ds_2.Tables(0).DefaultView

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
        grd.Columns(0).Visible = False 'fecha
        grd.Columns(1).Width = 60 'numero
        grd.Columns(2).Width = 100 'numero
        grd.Columns(3).Width = 50 'almacen
        grd.Columns(4).Width = 100 'motivo
        grd.Columns(5).Width = 290 'nota


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

    Private Sub aplicar_busqueda()

        Try
            'limpiar filtro
            dv.RowFilter = ""

            Dim sqlstring As String

            If txtFiltro.Text.ToString = "" Then
                sqlstring = " [Nombre Material] = [Nombre Material]"
            Else
                sqlstring = " [Nombre Material] Like '%" & txtFiltro.Text & "%'"
                '    sqlstring = sqlstring + " Or [Producto] Like '%" & txtFiltro.Text & "%'"
                '    sqlstring = sqlstring + " Or [Unidad] Like '%" & txtFiltro.Text & "%'"
            End If

            'aplico el filtro al final
            dv.RowFilter = sqlstring

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#Region "   Funciones"

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.ID_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.ID_Det)
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
            If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.PrecioCosto Then
                grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material, grdItems.CurrentRow.Index + 1)
                Return True
            End If

            If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Cod_Material Then
                grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material, grdItems.CurrentRow.Index)
                Return True
            End If

        Catch ex As Exception

        End Try

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

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

            If ActualizarUbicaciones = True Then
                AgregarRegistro = 1
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

                Dim param_idmotivo As New SqlClient.SqlParameter
                param_idmotivo.ParameterName = "@masivo"
                param_idmotivo.SqlDbType = SqlDbType.Bit
                param_idmotivo.Value = chkActMasivo.Checked
                param_idmotivo.Direction = ParameterDirection.Input

                Dim param_porcen As New SqlClient.SqlParameter
                param_porcen.ParameterName = "@MasivoPorcen"
                param_porcen.SqlDbType = SqlDbType.Decimal
                param_porcen.Precision = 18
                param_porcen.Scale = 2
                param_porcen.Value = IIf(txtPorcentaje.Text = "", 0, txtPorcentaje.Text)
                param_porcen.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_ActualizacionPrecios_Insert", param_id, param_codigo, _
                                            param_fecha, param_porcen, param_idmotivo, param_nota, param_useradd, param_res)
                    txtID.Text = param_id.Value
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

    Private Function AgregarRegistroItems() As Integer
        ' Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Dim i As Integer


        Try

            Try
                i = 0
                Do While i <= grdItems.Rows.Count - 1

                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Modificando).Value = 1 Then

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
                        param_codigo.Direction = ParameterDirection.Input

                        Dim param_idajuste As New SqlClient.SqlParameter
                        param_idajuste.ParameterName = "@IDActPre"
                        param_idajuste.SqlDbType = SqlDbType.BigInt
                        param_idajuste.Value = IIf(txtID.Text = "", 0, txtID.Text)
                        param_idajuste.Direction = ParameterDirection.Input

                        Dim param_idmaterial As New SqlClient.SqlParameter
                        param_idmaterial.ParameterName = "@idmaterial"
                        param_idmaterial.SqlDbType = SqlDbType.VarChar
                        param_idmaterial.Size = 25
                        param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
                        param_idmaterial.Direction = ParameterDirection.Input

                        Dim param_preciocosto As New SqlClient.SqlParameter
                        param_preciocosto.ParameterName = "@PrecioCompra"
                        param_preciocosto.SqlDbType = SqlDbType.Decimal
                        param_preciocosto.Precision = 18
                        param_preciocosto.Scale = 2
                        param_preciocosto.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value
                        param_preciocosto.Direction = ParameterDirection.Input

                        Dim param_preciomayo As New SqlClient.SqlParameter
                        param_preciomayo.ParameterName = "@PrecioCosto"
                        param_preciomayo.SqlDbType = SqlDbType.Decimal
                        param_preciomayo.Precision = 18
                        param_preciomayo.Scale = 2
                        param_preciomayo.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov1).Value
                        param_preciomayo.Direction = ParameterDirection.Input

                        Dim param_precioreven As New SqlClient.SqlParameter
                        param_precioreven.ParameterName = "@PrecioMayorista"
                        param_precioreven.SqlDbType = SqlDbType.Decimal
                        param_precioreven.Precision = 18
                        param_precioreven.Scale = 2
                        param_precioreven.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov2).Value
                        param_precioreven.Direction = ParameterDirection.Input

                        Dim param_precioyami As New SqlClient.SqlParameter
                        param_precioyami.ParameterName = "@ganancia"
                        param_precioyami.SqlDbType = SqlDbType.Decimal
                        param_precioyami.Precision = 18
                        param_precioyami.Scale = 2
                        param_precioyami.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan).Value
                        param_precioyami.Direction = ParameterDirection.Input

                        Dim param_preciosalon As New SqlClient.SqlParameter
                        param_preciosalon.ParameterName = "@ganancia2"
                        param_preciosalon.SqlDbType = SqlDbType.Decimal
                        param_preciosalon.Precision = 18
                        param_preciosalon.Scale = 2
                        param_preciosalon.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan2).Value
                        param_preciosalon.Direction = ParameterDirection.Input


                        Dim param_nota As New SqlClient.SqlParameter
                        param_nota.ParameterName = "@nota"
                        param_nota.SqlDbType = SqlDbType.VarChar
                        param_nota.Size = 300
                        param_nota.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota).Value
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

                        Dim param_IdStockMov As New SqlClient.SqlParameter
                        param_IdStockMov.ParameterName = "@IdStockMov"
                        param_IdStockMov.SqlDbType = SqlDbType.Int
                        param_IdStockMov.Value = DBNull.Value
                        param_IdStockMov.Direction = ParameterDirection.InputOutput

                        Try
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_ActualizacionPrecios_Det_Insert", param_id, param_codigo, param_idajuste, _
                                                      param_idmaterial, param_preciocosto, param_preciomayo, param_precioreven, param_precioyami, param_preciosalon, _
                                                      param_useradd, param_res)
                            res = param_res.Value


                            'If MDIPrincipal.NoActualizar = False And res > 0 Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then

                            'Dim sqlstring As String
                            'Dim resString As String
                            'Try
                            '    sqlstring = "UPDATE [dbo].[" & NameTable_Materiales & "] SET " & _
                            '             " [PrecioCompra] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value & "," & _
                            '             " [PrecioCosto] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioMayo).Value & "," & _
                            '             " [PrecioMayorista] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioReven).Value & "," & _
                            '             " [PrecioLista3] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioYamila).Value & "," & _
                            '             " [PrecioLista4] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioSalon).Value & "," & _
                            '             " [PrecioPeron] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioPeron).Value & "," & _
                            '             " [PrecioMayoristaPeron] = " & grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioPeronMayo).Value & "," & _
                            '             " [Cambiar1] = " & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.BloquearM).Value = True, 1, 0) & "," & _
                            '             " [Cambiar2] = " & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.BloquearR).Value = True, 1, 0) & "," & _
                            '             " [Cambiar3] = " & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.BloquearY).Value = True, 1, 0) & "," & _
                            '             " [Cambiar4] = " & IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.BloquearS).Value = True, 1, 0) & "," & _
                            '             " [DATEUPD] = '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "'," & _
                            '             " [ActualizadoLocal] = 0 " & _
                            '             " WHERE Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value & "'"

                            '    resString = tranWEB.Sql_Set(sqlstring)

                            'Catch ex As Exception
                            '    MsgBox("No se puede actualizar en la Web el registro.")
                            'End Try
                            'End If


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
            Cursor = Cursors.Default
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
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioCosto).Value Is Nothing) _
            And (grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov1).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov1).Value Is Nothing) _
            And (grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov2).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.preciov2).Value Is Nothing) _
            And (grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan).Value Is Nothing) _
            And (grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan2).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.ganan2).Value Is Nothing) Then
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
                            Util.MsgStatus(Status1, "No se pudo actualizar el número de Actualización (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                        Case Else
                            Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                            Cursor = Cursors.WaitCursor
                            res_item = AgregarRegistroItems()
                            Select Case res_item
                                Case -3
                                    Cursor = Cursors.Default
                                    Util.MsgStatus(Status1, "no se pudo actualizar el material '" & cod_aux & "'", My.Resources.Resources.alert.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -2
                                    Cursor = Cursors.Default
                                    Util.MsgStatus(Status1, "No se pudo hacer la insersión (Items).", My.Resources.Resources.alert.ToBitmap, True)
                                    Cancelar_Tran()
                                Case -1
                                    Cursor = Cursors.Default
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo registrar la actualización (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case 0
                                    Cursor = Cursors.Default
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                                Case Else
                                    Cerrar_Tran()
                                    'Dim sqlstring As String = "update [" & NameTable_NotificacionesWEB & "] set Materiales = 1 where IdAlmacen <> " & Util.numero_almacen
                                    'tranWEB.Sql_Set(sqlstring)
                                    bolModo = False
                                    PrepararBotones()
                                    btnActualizar_Click(sender, e)
                                    Setear_Grilla()
                                    Cursor = Cursors.Default
                                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap, True, True)
                                    btnCancelar_Click(sender, e)
                            End Select
                    End Select
                    '
                    ' cerrar la conexion si está abierta.
                    '
                    If Not conn_del_form Is Nothing Then
                        CType(conn_del_form, IDisposable).Dispose()
                    End If
                Else 'If bolModo Then
                    Util.MsgStatus(Me.Status1, "La actualización de precios no se pueden modificar.", My.Resources.alert.ToBitmap)
                End If ' If bolModo Then
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then

    End Sub

    Public Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'Util.LimpiarTextBox(Me.Controls)
        GroupBox1.Enabled = False
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        If MessageBox.Show("Desea generar una nueva Actualización de precios?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

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
        LlenarGridItems()
        txtFiltro.Enabled = True
        txtPorcentaje.Enabled = False
        txtFiltro.Focus()
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


        param.AgregarParametros("Código :", "STRING", "", False, txtPorcentaje.Text.ToString, "", cnn)
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