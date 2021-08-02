Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmTableros

    Dim scAutoComplete As New AutoCompleteStringCollection

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

    Public UltBusqueda As String

    Public Band_RecDesc As Boolean

    Public codigoDesdeLista As String

    Dim GuardarContadoEfectivo As Boolean

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IdTablero_Det = 0
        Id_Tablero = 1
        IDMaterial = 2
        Cod_Material = 3
        Nombre_Material = 4
        Qty = 5
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String
    Dim band As Integer, Revision As Integer


#Region "Eventos"

    Private Sub frmConsumos_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGridItems(CType(txtID.Text, Long))
            End If
        End If
    End Sub

    Public bandera_boton As Boolean

    Private Sub frmConsumos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        band = 0

        'Me.txtID.Visible = False
        Band_RecDesc = False

        btnActivar.Text = "Modificar"
        btnActivar.Enabled = True

        configurarform()
        asignarTags()

        SQL = "exec spTABLEROS_Select_All @eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        ' ajuste de la grilla de navegación según el tamaño de los datos
        Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems(0)

            grd.Columns(0).Visible = False

            btnNuevo_Click(sender, e)

        Else
            LlenarGridItems(CType(txtID.Text, Long))
        End If

        If grd.RowCount > 0 Then

            grd.Rows(0).Selected = True
            SendKeys.Send("{TAB}")
            SendKeys.Send("{TAB}")
            SendKeys.Send("{TAB}")
        Else
            txtCliente.Focus()
        End If

        permitir_evento_CellChanged = True

        grd.Columns(0).Visible = False

        band = 1

        Contar_Filas()

        grdItems.Enabled = bolModo
        btnGuardar.Enabled = bolModo


        If bandera_boton = True Then
            btnAsignar_A_Presupuesto.Visible = True
            If bolModo = True Then
                bolModo = False
                PrepararBotones()
            End If
        End If

        'grd.Focus()

    End Sub

    Private Sub frmConsumos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Tablero nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Nuevo Tablero?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                If bolModo = False Then Exit Sub
                GuardarContadoEfectivo = False
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Function ExisteEnLista() As Boolean
        Dim i As Integer
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        For i = 0 To grdItems.RowCount - 2
            Dim cuentafilas As Integer
            Dim codigo_mat As String = "", codigo_mat_2 As String = ""
            codigo_mat = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value)
            For cuentafilas = i + 1 To grdItems.RowCount - 2
                If grdItems.RowCount - 1 > 1 Then
                    codigo_mat_2 = IIf(grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value)
                    If codigo_mat <> "" And codigo_mat_2 <> "" Then
                        If codigo_mat = codigo_mat_2 Then
                            ExisteEnLista = True
                        End If
                    End If
                End If
            Next
        Next
    End Function

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            Dim valorcambio As Double = 0, ganancia As Double = 0, preciovta As Double = 0
            Dim idmoneda As Long = 0
            Dim nombre As String = "", codmoneda As String = ""

            If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
                'completar la descripcion del material
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim id As Long
                Dim stock As Double = 0, minimo As Double = 0, maximo As Double = 0, preciolista As Double = 0, preciovtaorig As Double = 0, gananciaorig As Double = 0, iva As Double = 0, montoiva As Double = 0
                Dim fecha As String = ""
                Dim i As Integer

                Dim codigo As String, codigo_mat_prov As String = "", unidad As String = "", codunidad As String = "", nombreproveedor As String = "", nombremarca As String = "", plazoentrega As String = ""
                Dim idunidad As Long = 0, idproveedor As Long = 0, idmarca As Long = 0

                If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
                    Exit Sub
                End If

                If ExisteEnLista() = False Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Selected = True
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value = 1
                    'SendKeys.Send("{TAB}")
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
                                    If MessageBox.Show("¿El codigo: " + codigo_mat_2.ToString + " ya esta cargado, desea incrementar la cantidad?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                        'Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                                        'cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex)
                                        'grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = " "
                                        grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = ""
                                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Selected = True
                                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value += 1
                                        SendKeys.Send("+{TAB}")

                                        Contar_Filas()
                                        Exit Sub
                                    Else
                                        grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = ""
                                        SendKeys.Send("+{TAB}")
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next

                codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerMaterial_App(codigo, codigo_mat_prov, id, nombre, idunidad, unidad, codunidad, stock, minimo, maximo, preciolista, ganancia, preciovta, preciovtaorig, gananciaorig, iva, fecha, idproveedor, nombreproveedor, idmarca, nombremarca, plazoentrega, idmoneda, codmoneda, valorcambio, montoiva, 0, False, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id

                    Contar_Filas()

                    SendKeys.Send("{TAB}")
                    'SendKeys.Send("{TAB}")
                    'SendKeys.Send("{TAB}")
                    'SendKeys.Send("{TAB}")
                    'SendKeys.Send("{TAB}")
                    'SendKeys.Send("{TAB}")

                Else

                    cell.Value = "NO EXISTE"
                    SendKeys.Send("{HOME}")
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            'MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
        Contar_Filas()

    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewRow = grdItems.CurrentRow

        Try
            If cell.Index >= 0 Then 'el de arriba no borraba la fila 0....
                Try
                    grdItems.Rows.RemoveAt(cell.Index)
                    grdItems.Refresh()
                    Contar_Filas()
                Catch ex As Exception

                End Try

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdItems_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellChanged
        If band = 0 Then Exit Sub
        Try
            Cell_Y = grdItems.CurrentRow.Index
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Botones"

    Public Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        'If MessageBox.Show("Desea generar una nueva Venta?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()
        grdItems.AllowUserToAddRows = True

        grdItems.Enabled = bolModo
        btnGuardar.Enabled = bolModo

        band = 1

    End Sub

    Public Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer, res_item As Integer ', res_notas As Integer

        Util.Logueado_OK = False

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando o el registro...", My.Resources.Resources.indicator_white)
                'If bolModo = False Then
                'ControlarCantidadRegistros()
                'End If
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo Insertar el (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Insertar el (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 20
                        Cerrar_Tran()
                        'Imprimir()
                        band = 0
                        bolModo = False
                        SQL = "exec [spTABLEROS_Select_All] @eliminado = 0"
                        btnActualizar_Click(sender, e)
                        Setear_Grilla()
                        grdItems.Enabled = bolModo
                        btnGuardar.Enabled = bolModo
                        Util.MsgStatus(Status1, "El tablero se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                        band = 1
                        Exit Sub
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                        res_item = AgregarRegistro_Items()
                        Select Case res_item
                            Case -30
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Insertar el Producto", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Insertar el Producto", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -5
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal los items", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal los items", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -6
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de items.", My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de items.", My.Resources.Resources.alert.ToBitmap, True)
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case Else
                                Cerrar_Tran()
                                'Imprimir()

                                band = 0
                                bolModo = False
                                SQL = "exec [spTABLEROS_Select_All] @eliminado = 0"
                                btnActualizar_Click(sender, e)
                                Setear_Grilla()
                                grdItems.Enabled = bolModo
                                btnGuardar.Enabled = bolModo

                                Util.MsgStatus(Status1, "El tablero se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                                band = 1
                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If

            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        Dim registro As Integer 'DataGridViewRow
        registro = grd.CurrentRow.Index

        If Not bolModo Then

            If MessageBox.Show("¿Está seguro que desea eliminar el tablero?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    'Case -6
                    '    Cancelar_Tran()
                    '    Util.MsgStatus(Status1, "No se registró la venta.", My.Resources.stop_error.ToBitmap)
                    'Case -5
                    '    Cancelar_Tran()
                    '    Util.MsgStatus(Status1, "No se registró el detalle la venta.", My.Resources.stop_error.ToBitmap)
                    'Case -4
                    '    Cancelar_Tran()
                    '    Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap)
                    'Case -3
                    '    Cancelar_Tran()
                    '    Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                    Case -20
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se puede eliminar el tablero seleccionado.", My.Resources.stop_error.ToBitmap)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                    Case Else
                        Cerrar_Tran()
                        PrepararBotones()
                        SQL = "exec [spTABLEROS_Select_All] @eliminado = 0"
                        btnActualizar_Click(sender, e)
                        Setear_Grilla()
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
        SQL = "exec [spTABLEROS_Select_All] @eliminado = 0"
        btnActualizar_Click(sender, e)
        Setear_Grilla()
    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If txtID.Text <> "" Then
            LlenarGridItems(CType(txtID.Text, Long))
            Contar_Filas()

            grdItems.Enabled = bolModo
            btnGuardar.Enabled = bolModo

        End If
    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Tableros..."

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 60)
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

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer


        Dim codigo, nombre, nombrelargo, tipo, observaciones As String 'ubicacion,
        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : observaciones = ""  'ubicacion = ""

        bolpoliticas = False

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

                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El producto ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
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

            End If
        Next i

        For i = 0 To grdItems.RowCount - 2
            Dim cuentafilas As Integer
            Dim codigo_mat As String = "", codigo_mat_2 As String = ""
            'codigo_mat = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
            codigo_mat = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value)
            For cuentafilas = i + 1 To grdItems.RowCount - 2
                If grdItems.RowCount - 1 > 1 Then
                    'codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
                    codigo_mat_2 = IIf(grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value)
                    If codigo_mat <> "" And codigo_mat_2 <> "" Then
                        If codigo_mat = codigo_mat_2 Then
                            Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
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
            Util.MsgStatus(Status1, "No hay filas de productos para guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        txtCliente.Tag = "2"
    End Sub

    Private Sub validar_NumerosReales2( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridItems.Qty Then

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
    End Sub

    Private Sub LlenarGridItems(ByVal id As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        SQL = "exec spTABLEROS_DET_Select_By_IdTablero @idtablero = " & id 'txtID.Text

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IdTablero_Det).Visible = False
        grdItems.Columns(ColumnasDelGridItems.Id_Tablero).Visible = False
        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False  'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 90

        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 260

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = False 'maximo'
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
            .AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders)
        End With

        With grdItems.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)


        grdItems.Columns(ColumnasDelGridItems.Qty).DefaultCellStyle.ForeColor = Color.Red

        'Volver la fuente de datos a como estaba...
        SQL = "exec spTABLEROS_Select_All @eliminado = 0"
    End Sub

    Private Sub InicializarGridItems(ByVal Grd As DataGridView)

        Dim style As New DataGridViewCellStyle
        Grd.EnableHeadersVisualStyles = False

        'da formato al encabezado...
        With Grd.ColumnHeadersDefaultCellStyle
            .BackColor = Color.CadetBlue
            .ForeColor = Color.Purple
            .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        ' Inicialice propiedades básicas.
        With Grd
            '.Dock = DockStyle.Fill ' lo coloca al tope del formulario..
            .VirtualMode = False
            .BackgroundColor = SystemColors.ActiveBorder 'Color.DarkGray ' color del fondo del grid...
            .BorderStyle = BorderStyle.Fixed3D
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
            .AllowUserToAddRows = True 'indica si se muestra al usuario la opción de agregar filas
            .AllowUserToDeleteRows = True 'indica si el usuario puede eliminar filas de DataGridView.
            .AllowUserToOrderColumns = False 'indica si el usuario puede cambiar manualmente de lugar las columnas..
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect   'indica cómo se pueden seleccionar las celdas de DataGridView.
            .MultiSelect = False 'indica si el usuario puede seleccionar a la vez varias celdas, filas o columnas del control DataGridView.
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders   'indica cómo se determina el alto de las filas. 
            .AllowUserToResizeColumns = True 'indica si los usuarios pueden cambiar el tamaño de las columnas.
            .AllowUserToResizeRows = True 'indica si los usuarios pueden cambiar el tamaño de las filas.
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize 'indica si el alto de los encabezados de columna es ajustable y si puede ser ajustado por el usuario o automáticamente para adaptarse al contenido de los encabezados. 
        End With

        'Setear el color de seleccion de fondo de la celda actual...
        Grd.DefaultCellStyle.SelectionBackColor = Color.White
        Grd.DefaultCellStyle.SelectionForeColor = Color.Blue

        'generamos el formato para las celdas...
        With style
            .BackColor = Color.Lavender   'Color.LightGray
            .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .ForeColor = Color.Black
        End With
        Grd.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Aplicamos el estilo a todas las celdas del control DataGridView
        Grd.RowsDefaultCellStyle = style
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
        grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
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

    End Sub

    Private Sub OcultarGrilla()
        Dim p2 As New Size(GroupBox1.Size.Width, 560) '200'AltoMinimoGrilla)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, 462) '200'AltoMinimoGrilla)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Visible = False
    End Sub

    Private Sub Contar_Filas()
        Dim i As Integer, j As Integer = 0

        For i = 0 To 16
            Try
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value <> Nothing Then
                    j = j + 1
                End If
            Catch ex As Exception

            End Try
        Next

        'lblCantidadFilas.Text = j.ToString + " / 16"

        If j = 16 Then
            grdItems.AllowUserToAddRows = False
        Else
            grdItems.AllowUserToAddRows = True
        End If

    End Sub

#End Region

#Region "Funciones"

    Private Function AgregarActualizar_Registro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
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
                    param_id.Direction = ParameterDirection.Output
                Else
                    param_id.Value = txtID.Text
                    param_id.Direction = ParameterDirection.Input
                End If
                'param_id.Direction = ParameterDirection.InputOutput

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar

                If bolModo = True Then
                    param_codigo.Value = DBNull.Value
                    param_codigo.Direction = ParameterDirection.Input
                Else
                    param_codigo.Value = txtCODIGO.Text
                    param_codigo.Direction = ParameterDirection.Input
                End If

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 50
                param_nombre.Value = txtCliente.Text
                param_nombre.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTABLEROS_Insert", _
                                            param_id, param_codigo, param_nombre, param_useradd, param_res)


                        txtID.Text = param_id.Value
                        'txtCODIGO.Text = param_codigo.Value

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTABLEROS_Update", _
                                            param_id, param_codigo, param_nombre, param_useradd, param_res)

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

    Private Function AgregarRegistro_Items() As Integer
        Dim res As Integer = 0 ', res_del As Integer
        Dim i As Integer

        Try

            Try

                If bolModo = False Then
                    Dim param_idmpdelete As New SqlClient.SqlParameter
                    param_idmpdelete.ParameterName = "@idMP"
                    param_idmpdelete.SqlDbType = SqlDbType.BigInt
                    param_idmpdelete.Value = grd.CurrentRow.Cells(0).Value
                    param_idmpdelete.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.Output

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTABLEROS_DET_Delete", _
                                                  param_idmpdelete, param_res)

                        res = CInt(param_res.Value)

                    Catch ex As Exception
                        Throw ex
                    End Try
                End If

                i = 0
                Dim CantidadFilas As Integer

                If grdItems.RowCount = 16 Then
                    CantidadFilas = grdItems.Rows.Count
                Else
                    CantidadFilas = grdItems.Rows.Count - 1
                End If

                Do While i < CantidadFilas
                    Dim id As Long

                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then

                    Else
                        id = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)

                    End If

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    If bolModo = False And Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.IdTablero_Det).Value Is DBNull.Value) Then
                        param_id.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdTablero_Det).Value, Long)
                    Else
                        param_id.Value = 0
                    End If
                    param_id.Direction = ParameterDirection.Input

                    Dim param_IdConsumo As New SqlClient.SqlParameter
                    param_IdConsumo.ParameterName = "@idtablero"
                    param_IdConsumo.SqlDbType = SqlDbType.BigInt
                    param_IdConsumo.Value = txtID.Text
                    param_IdConsumo.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.BigInt
                    param_idmaterial.Value = id
                    param_idmaterial.Direction = ParameterDirection.Input

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 2
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    'If bolModo = True Then
                    param_useradd.ParameterName = "@useradd"
                    'Else
                    '    param_useradd.ParameterName = "@userupd"
                    'End If
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try
                        'If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTABLEROS_DET_Insert", _
                                              param_IdConsumo, param_idmaterial, param_qty, param_useradd, param_res)

                        res = param_res.Value

                        'Else
                        'SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTABLEROS_DET_Update", _
                        '                      param_IdConsumo, param_idmaterial, param_qty, param_useradd, param_res)

                        'res = param_res.Value

                        'End If

                        If (res <= 0) Then
                            Exit Do
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try
                    i = i + 1
                Loop

                AgregarRegistro_Items = res

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

    Private Function EliminarItems_Consumo() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idTablero"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTableros_Det_Delete", _
                                        param_id, param_res)

            EliminarItems_Consumo = param_res.Value

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

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_idconsumo As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
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

                Try

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[spTableros_Delete]", _
                            param_idconsumo, param_userdel, param_res)

                    res = param_res.Value
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

    Private Function fila_vacia(ByVal i) As Boolean
        Try
            If (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is Nothing) _
                                And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is Nothing) Then
                fila_vacia = True
            Else
                fila_vacia = False
            End If
        Catch ex As Exception
            fila_vacia = True
        End Try

    End Function

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then

            Try
                If grd.RowCount > 0 Then
                    LlenarGridItems(txtID.Text)
                End If
            Catch ex As Exception

            End Try
            Try
            Catch ex As Exception

            End Try
        End If
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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdTablero_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdTablero_Det)
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

#Region "Transacciones"

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

#End Region

    Private Sub btnCopiarTablero_Click(sender As Object, e As EventArgs) Handles btnCopiarTablero.Click
        Dim UltId As Long

        If MessageBox.Show("Desea copiar los materiales de tablero actual para generar uno nuevo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            UltId = txtID.Text
            btnNuevo_Click(sender, e)
            txtID.Text = UltId

            LlenarGridItems(UltId)

            txtID.Text = ""
            txtCliente.Focus()
        End If
    End Sub

    Private Overloads Sub BtnActivar_Click(sender As Object, e As EventArgs) Handles btnActivar.Click
        grdItems.Enabled = True
        btnGuardar.Enabled = True
        txtCliente.Focus()
    End Sub

    Private Sub grdItems_MouseUp(sender As Object, e As MouseEventArgs) Handles grdItems.MouseUp
        Dim Valor As String = ""
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If grdItems.RowCount <> 0 Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            If grdItems.RowCount <> 0 And Cell_X = 5 Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            If Valor <> "" Then
                Dim p As Point = New Point(e.X, e.Y)

                ContextMenuStrip1.Show(grdItems, p)
                ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
            End If

            If Valor <> "" And Cell_X = 5 Then
                Dim p As Point = New Point(e.X, e.Y)

                ContextMenuStrip2.Show(grdItems, p)
            End If

        End If
    End Sub




    Private Sub btnAsignar_A_Presupuesto_Click(sender As Object, e As EventArgs) Handles btnAsignar_A_Presupuesto.Click

        Dim i As Integer = 0

        frmPresupuestos.cantidadItemsTablero = grdItems.Rows.Count

        While i <= grdItems.Rows.Count - 1
            'frmPresupuestos.AgregarMateriales(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value, grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, i)
            frmPresupuestos.lista(i, 0) = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
            frmPresupuestos.lista(i, 1) = grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value
            i += 1
        End While

        Me.Close()

    End Sub
End Class

