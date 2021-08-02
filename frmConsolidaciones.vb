Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmConsolidaciones

    Dim bolpoliticas As Boolean

    Dim RemitosAsociados As String

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
    'Dim conexionGenerica As SqlClient.SqlConnection = Nothing

    Dim band As Integer, bandIVA As Boolean
    'BANDIVA SE UTILIZA PARA SABER SI EXISTEN VARIOS PORCENTAJES DE IVA DIFERENTES EN EL PAGO

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número

    Enum ColumnasDelGridItems
        Id = 0
        Id_Empleado = 1
        Fecha = 2
        Dia = 3
        Hora_Ingreso = 4
        Hora_Egreso = 5
        Llegada_Tarde = 6
        Justificada = 7
        Nota = 8
        PrecioHora = 9
        Horas_Normales = 10
        Monto_Hs_Normales = 11
        Horas_Extras_50 = 12
        Monto_Hs_Ext_50 = 13
        Horas_Extras_100 = 14
        Monto_Hs_Ext_100 = 15
        Total_dia = 16
        Consolidado = 17
        FechaFeriado = 18
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim permitir_evento_CellChanged As Boolean


#Region "   Eventos"

    Private Sub frmFacturacion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                'If bolModo = True Then
                '    If MessageBox.Show("No ha guardado la Factura Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Nueva Factura?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '        btnNuevo_Click(sender, e)
                '    End If
                'Else
                btnNuevo_Click(sender, e)
                'End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmFacturacion_Gestion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        band = 0
        configurarform()
        asignarTags()

        PeriodosRRHH()

        LlenarComboEmpleados()
        LlenarComboPeriodos()

        SQL = "exec spConsolidaciones_Select_All @Eliminado = 0"

        LlenarGrilla()

        Permitir = True

        CargarCajas()

        PrepararBotones()

        If bolModo = True Then
            LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
            btnNuevo_Click(sender, e)
        End If

        permitir_evento_CellChanged = True

        grd_CurrentCellChanged(sender, e)

        band = 1

        cmbPeriodos.SelectedIndex = 0

        grd.Columns(5).Visible = False

        'grdItems.ReadOnly = Not bolModo

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtCODIGO.KeyPress
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

    Private Sub dtpFECHA_ValueChanged(sender As Object, e As EventArgs)
        If bolModo = True Then
            LimpiarGridItems(grdItems)
            LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
        End If
    End Sub

    Private Sub dtpfechafin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpFECHAfin_ValueChanged(sender As Object, e As EventArgs)
        If bolModo = True Then
            LimpiarGridItems(grdItems)
            LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
        End If
    End Sub


    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEmpleado.SelectedIndexChanged
        If band = 1 Then
            If bolModo = True Then
                LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
                AgregarDiaConsolidado_tmp()
            Else
                lblHsNormales.Text = "0"
                lblHsExtras50.Text = "0"
                lblHsExtras100.Text = "0"

                lblMontoHsNormales.Text = "0"
                lblMontoHsExtras50.Text = "0"
                lblMontoHsExtras100.Text = "0"

                lblTotalConsolidacion.Text = "0"
            End If
        End If

    End Sub

    'Private Sub grdItems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
    '    If e.ColumnIndex = ColumnasDelGridItems.Consolidado + 2 Then
    '        Dim hsTrabajadas As Double, hsCargadas As Double

    '        Select Case ActualizarDiaTrabajado(hsTrabajadas, hsCargadas)
    '            Case -1
    '                Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
    '            Case -5
    '                Util.MsgStatus(Status1, "Existe una diferencia entre las horas marcadas (" & hsTrabajadas & ") y las horas calculadas/ingresadas (" & hsCargadas & ").", My.Resources.Resources.stop_error.ToBitmap)
    '                Util.MsgStatus(Status1, "Existe una diferencia entre las horas marcadas (" & hsTrabajadas & ") y las horas calculadas/ingresadas (" & hsCargadas & ").", My.Resources.Resources.stop_error.ToBitmap, True)
    '            Case 0
    '                Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
    '            Case Else
    '                Util.MsgStatus(Status1, "Se actualizó correctamente la información del día seleccionado.", My.Resources.Resources.ok.ToBitmap)
    '                LlenarGridItems(cmbEmpleado.SelectedValue)
    '        End Select
    '    End If
    'End Sub

    Private Sub grdItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellValueChanged
        Try

            If e.ColumnIndex = ColumnasDelGridItems.Llegada_Tarde Or e.ColumnIndex = ColumnasDelGridItems.Justificada Then
                Dim conexionGenerica As SqlClient.SqlConnection = Nothing

                Try
                    conexionGenerica = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                Dim ds As Data.DataSet

                ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "UPDATE Asistencias SET Llegada_Tarde = " & IIf(CBool(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Llegada_Tarde).Value) = True, 1, 0) & ", " & _
                                                    " Justificada = " & IIf(CBool(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Justificada).Value) = True, 1, 0) & _
                                                    " WHERE id = " & grdItems.CurrentRow.Cells(ColumnasDelGridItems.Id).Value)

                ds.Dispose()

                If Not conexionGenerica Is Nothing Then
                    CType(conexionGenerica, IDisposable).Dispose()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub grdItems_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellDirtyStateChanged
        If grdItems.IsCurrentCellDirty Then
            grdItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then

            Try
                LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
            Catch ex As Exception
                LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
            End Try

        End If
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try
            If e.ColumnIndex = ColumnasDelGridItems.Hora_Ingreso Or e.ColumnIndex = ColumnasDelGridItems.Hora_Egreso Then
                If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Ingreso).Value Is DBNull.Value) And _
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Ingreso).Value <> "" And _
                    Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Egreso).Value Is DBNull.Value) And _
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Egreso).Value <> "" Then

                    If CDate(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Ingreso).Value) > CDate(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Egreso).Value) Then
                        MsgBox("La hora de Entrada no puede ser mayor a la hora de Salida.", MsgBoxStyle.Critical, "Atención")
                        Exit Sub
                    End If

                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Id).Value = 0 Then

                        Select Case AgregarRegistro_DiaTrabajado()
                            Case 0
                                Util.MsgStatus(Status1, "No pudo realizarse la insersión.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No pudo realizarse la insersión.", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -2
                                Util.MsgStatus(Status1, "Se produjo un error desconocido al querer insertar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "Se produjo un error desconocido al querer insertar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -3
                                Util.MsgStatus(Status1, "La fecha ingresada ya tiene un movimiento asignado.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "La fecha ingresada ya tiene un movimiento asignado.", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case Else
                                Util.MsgStatus(Status1, "Se insertó correctamente el movimiento.", My.Resources.Resources.ok.ToBitmap)
                                LlenarGridItems(cmbEmpleado.SelectedValue)
                        End Select
                    Else
                        Dim hsTrabajadas As Double, hsCargadas As Double

                        Select Case Actualizar_HsTrabajadas()
                            Case -1
                                Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Case Else
                                Util.MsgStatus(Status1, "Se actualizó correctamente la información del día seleccionado.", My.Resources.Resources.ok.ToBitmap)
                                Dim id As Integer, i As Integer

                                id = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Id).Value

                                LlenarGridItems(cmbEmpleado.SelectedValue)

                                For i = 0 To grdItems.Rows.Count - 1
                                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Id).Value = id Then
                                        Exit For
                                    End If
                                Next

                                Select Case ActualizarDiaTrabajado(i)
                                    Case -1
                                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                                    Case -5
                                        Util.MsgStatus(Status1, "Existe una diferencia entre las horas marcadas (" & hsTrabajadas & ") y las horas calculadas/ingresadas (" & hsCargadas & ").", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "Existe una diferencia entre las horas marcadas (" & hsTrabajadas & ") y las horas calculadas/ingresadas (" & hsCargadas & ").", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case 0
                                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                                    Case Else
                                        Util.MsgStatus(Status1, "Se actualizó correctamente la información del día seleccionado.", My.Resources.Resources.ok.ToBitmap)
                                        LlenarGridItems(cmbEmpleado.SelectedValue)
                                End Select
                        End Select
                    End If

                    AgregarDiaConsolidado_tmp()

                End If

            End If

            If e.ColumnIndex = ColumnasDelGridItems.Nota Then

                Dim conexionGenerica As SqlClient.SqlConnection = Nothing

                Try
                    conexionGenerica = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                Dim ds As Data.DataSet

                ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "UPDATE Asistencias SET Nota = '" & grdItems.CurrentRow.Cells(ColumnasDelGridItems.Nota).Value & "' WHERE id = " & grdItems.CurrentRow.Cells(ColumnasDelGridItems.Id).Value)

                ds.Dispose()

                If Not conexionGenerica Is Nothing Then
                    CType(conexionGenerica, IDisposable).Dispose()
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales2

    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
        'Contar_Filas()
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub grdItems_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellChanged
        If band = 0 Then Exit Sub
        Try
            Cell_Y = grdItems.CurrentRow.Index
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        cmbEmpleado.Tag = "6"
        lblHsNormales.Tag = "7"
        lblHsExtras50.Tag = "8"
        lblHsExtras100.Tag = "9"
        lblMontoHsNormales.Tag = "11"
        lblMontoHsExtras50.Tag = "12"
        lblMontoHsExtras100.Tag = "13"
        lblTotalConsolidacion.Tag = "14"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If cmbEmpleado.SelectedValue Is Nothing Or cmbEmpleado.SelectedValue Is DBNull.Value Then
            MsgBox("Debe seleccionar un empleado para realizar la Consolidación.", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If

        If grdItems.Rows.Count = 0 Then
            MsgBox("El empleado seleccionado no tiene días trabajados en el periodo especificado.", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        bolpoliticas = True

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

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
    End Sub

    Private Sub LlenarGridItems(ByVal Id_Empleado As Long)

        If Id_Empleado = 0 Then
            Exit Sub
        End If

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        SQL = "exec [spAsistencias_Select_by_IdEmpleado] @IdEmpleado = " & Id_Empleado & ", @Periodo = '" & cmbPeriodos.Text.ToString & "', @Modo = " & bolModo & ", @IdConsolidacion = " & IIf(txtID.Text = "", 0, txtID.Text)

        '", @Inicio = '" & dtpFechaInicio.Value.ToShortDateString & "', @Fin = '" & dtpFechaFin.Value.ToShortDateString 

        GetDatasetItems(grdItems)

        'Dim colBoton As DataGridViewButtonColumn = New DataGridViewButtonColumn()
        'With colBoton
        '    .HeaderText = "Acción"
        '    .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
        '    .UseColumnTextForButtonValue = True
        '    .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        '    .FlatStyle = FlatStyle.Popup
        '    .DefaultCellStyle.BackColor = Color.Red
        '    .CellTemplate.Style.BackColor = Color.Red
        '    .CellTemplate.Style.ForeColor = Color.White
        '    .DefaultCellStyle.SelectionBackColor = Color.Green
        '    .Text = "Guardar Cambio"
        'End With

        'grdItems.Columns.Insert(grdItems.Columns.Count, colBoton)

        'MsgBox(grdItems.Columns.Count)

        grdItems.Columns(ColumnasDelGridItems.Id).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Id_Empleado).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Fecha).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Fecha).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Fecha).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Dia).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Dia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Dia).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Hora_Ingreso).Width = 55
        grdItems.Columns(ColumnasDelGridItems.Hora_Ingreso).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Hora_Egreso).Width = 55
        grdItems.Columns(ColumnasDelGridItems.Hora_Egreso).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Llegada_Tarde).Width = 55

        grdItems.Columns(ColumnasDelGridItems.Justificada).Width = 50

        grdItems.Columns(ColumnasDelGridItems.Nota).Width = 380

        grdItems.Columns(ColumnasDelGridItems.PrecioHora).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioHora).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.PrecioHora).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Horas_Normales).Width = 65
        grdItems.Columns(ColumnasDelGridItems.Horas_Normales).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Horas_Normales).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Horas_Extras_50).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Horas_Extras_50).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Horas_Extras_50).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Horas_Extras_100).Width = 65
        grdItems.Columns(ColumnasDelGridItems.Horas_Extras_100).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdItems.Columns(ColumnasDelGridItems.Horas_Extras_100).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Normales).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Normales).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Normales).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Ext_50).Width = 65
        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Ext_50).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Ext_50).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Ext_100).Width = 65
        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Ext_100).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.Monto_Hs_Ext_100).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Total_dia).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Total_dia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.Total_dia).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.Consolidado).Visible = False
        grdItems.Columns(ColumnasDelGridItems.FechaFeriado).Visible = False

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        With grdItems.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        Dim i As Integer

        For i = 0 To grdItems.Rows.Count - 1
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.FechaFeriado).Value <> "01/01/1900" Then
                grdItems.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue

            End If
        Next

        ' SQL = "exec spIngresos_Select_All @Eliminado = 0"

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

    Private Sub LlenarComboEmpleados()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            'ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT c.ID, c.nombre FROM Asistencias F  " & _
            '                                                         " JOIN Empleados c ON c.ID = F.Id_Empleado WHERE f.eliminado = 0 order by C.NOMBRE")

            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT c.ID, (Apellido + ', ' + c.nombre) as Empleado FROM Empleados c WHERE c.eliminado = 0 order by Empleado")

            ds_Cli.Dispose()

            With Me.cmbEmpleado
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "Empleado"
                .ValueMember = "id"
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

    Private Sub LlenarComboPeriodos()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT PERIODO FROM tmp_Asistencias_Periodos ORDER BY ano desc, mes desc")

            ds_Cli.Dispose()

            With Me.cmbPeriodos
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "Periodo"
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
        If columna = ColumnasDelGridItems.Hora_Egreso Or columna = ColumnasDelGridItems.Hora_Ingreso Or columna = ColumnasDelGridItems.Horas_Extras_100 Or _
                columna = ColumnasDelGridItems.Horas_Extras_50 Or columna = ColumnasDelGridItems.Horas_Normales Or columna = ColumnasDelGridItems.Monto_Hs_Ext_100 Or _
                columna = ColumnasDelGridItems.Monto_Hs_Ext_50 Or columna = ColumnasDelGridItems.Monto_Hs_Normales Or columna = ColumnasDelGridItems.Fecha Then

            Dim caracter As Char = e.KeyChar

            ' referencia a la celda  
            Dim txt As TextBox = CType(sender, TextBox)

            ' comprobar si es un número con isNumber, si es el backspace, si el caracter  
            ' es el separador decimal, y que no contiene ya el separador  
            If (Char.IsNumber(caracter)) Or _
               (caracter = ChrW(Keys.Back)) Or _
               (caracter = ":") And _
               (txt.Text.Contains(":") = False) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub PeriodosRRHH()

        Dim Cnn As New SqlConnection(ConnStringSEI)

        Try

            SqlHelper.ExecuteNonQuery(Cnn, CommandType.StoredProcedure, "spPeriodosRRHH")

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

            Exit Sub

        End Try

    End Sub

    Private Function AgregarDiaConsolidado_tmp() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim I As Integer
        Dim Totaldia As Double = 0, TotalHsNormales As Double = 0, MontoHsNormales As Double = 0
        Dim TotalHs50 As Double = 0, MontoHs50 As Double = 0
        Dim TotalHs100 As Double = 0, MontoHs100 As Double = 0

        Try

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spConsolidaciones_DELETE_tmp")

            Catch ex As Exception
                Throw ex
            End Try

            For I = 0 To grdItems.RowCount - 1

                If CBool(grdItems.Rows(I).Cells(ColumnasDelGridItems.Consolidado).Value) = True Then


                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@IdConsolidacion"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Id).Value
                    param_id.Direction = ParameterDirection.Input

                    TotalHsNormales = TotalHsNormales + grdItems.Rows(I).Cells(ColumnasDelGridItems.Horas_Normales).Value
                    TotalHs50 = TotalHs50 + grdItems.Rows(I).Cells(ColumnasDelGridItems.Horas_Extras_50).Value
                    TotalHs100 = TotalHs100 + grdItems.Rows(I).Cells(ColumnasDelGridItems.Horas_Extras_100).Value

                    MontoHsNormales = MontoHsNormales + grdItems.Rows(I).Cells(ColumnasDelGridItems.Monto_Hs_Normales).Value
                    MontoHs50 = MontoHs50 + grdItems.Rows(I).Cells(ColumnasDelGridItems.Monto_Hs_Ext_50).Value
                    MontoHs100 = MontoHs100 + grdItems.Rows(I).Cells(ColumnasDelGridItems.Monto_Hs_Ext_100).Value

                    Totaldia = Totaldia + grdItems.Rows(I).Cells(ColumnasDelGridItems.Total_dia).Value

                    Try
                        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spConsolidaciones_Insert_tmp", _
                                                  param_id)

                    Catch ex As Exception
                        Throw ex
                        Exit For
                    End Try
                End If

            Next

            lblHsNormales.Text = TotalHsNormales
            lblHsExtras50.Text = TotalHs50
            lblHsExtras100.Text = TotalHs100

            lblMontoHsNormales.Text = MontoHsNormales
            lblMontoHsExtras50.Text = MontoHs50
            lblMontoHsExtras100.Text = MontoHs100

            lblTotalConsolidacion.Text = Totaldia

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

#Region "   Funciones"

    Private Function AgregarRegistro_Consolidacion() As Integer

        'Dim connection As SqlClient.SqlConnection = Nothing
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

                Dim param_idempleado As New SqlClient.SqlParameter
                param_idempleado.ParameterName = "@idEmpleado"
                param_idempleado.SqlDbType = SqlDbType.BigInt
                param_idempleado.Value = cmbEmpleado.SelectedValue
                param_idempleado.Direction = ParameterDirection.Input

                'Dim param_FechaInicio As New SqlClient.SqlParameter
                'param_FechaInicio.ParameterName = "@FechaInicio"
                'param_FechaInicio.SqlDbType = SqlDbType.Date
                'param_FechaInicio.Value = dtpFechaInicio.Value
                'param_FechaInicio.Direction = ParameterDirection.Input

                'Dim param_FechaFin As New SqlClient.SqlParameter
                'param_FechaFin.ParameterName = "@FechaFin"
                'param_FechaFin.SqlDbType = SqlDbType.Date
                'param_FechaFin.Value = dtpFechaFin.Value
                'param_FechaFin.Direction = ParameterDirection.Input

                Dim param_Periodo As New SqlClient.SqlParameter
                param_Periodo.ParameterName = "@Periodo"
                param_Periodo.SqlDbType = SqlDbType.VarChar
                param_Periodo.Size = 50
                param_Periodo.Value = cmbPeriodos.Text.ToString
                param_Periodo.Direction = ParameterDirection.Input

                Dim param_hsnormales As New SqlClient.SqlParameter
                param_hsnormales.ParameterName = "@Horas_Normales"
                param_hsnormales.SqlDbType = SqlDbType.Decimal
                param_hsnormales.Precision = 18
                param_hsnormales.Scale = 2
                param_hsnormales.Value = CDbl(lblHsNormales.Text)
                param_hsnormales.Direction = ParameterDirection.Input

                Dim param_hsextras50 As New SqlClient.SqlParameter
                param_hsextras50.ParameterName = "@Horas_Extras_50"
                param_hsextras50.SqlDbType = SqlDbType.Decimal
                param_hsextras50.Precision = 18
                param_hsextras50.Scale = 2
                param_hsextras50.Value = CDbl(lblHsExtras50.Text)
                param_hsextras50.Direction = ParameterDirection.Input

                Dim param_hsextras100 As New SqlClient.SqlParameter
                param_hsextras100.ParameterName = "@Horas_Extras_100"
                param_hsextras100.SqlDbType = SqlDbType.Decimal
                param_hsextras100.Precision = 18
                param_hsextras100.Scale = 2
                param_hsextras100.Value = CDbl(lblHsExtras100.Text)
                param_hsextras100.Direction = ParameterDirection.Input

                Dim param_montohora As New SqlClient.SqlParameter
                param_montohora.ParameterName = "@Monto_Hora"
                param_montohora.SqlDbType = SqlDbType.Decimal
                param_montohora.Precision = 18
                param_montohora.Scale = 2
                param_montohora.Value = grdItems.Rows(0).Cells(ColumnasDelGridItems.PrecioHora).Value
                param_montohora.Direction = ParameterDirection.Input

                Dim param_total_hs_Normales As New SqlClient.SqlParameter
                param_total_hs_Normales.ParameterName = "@Total_Hs_Normales"
                param_total_hs_Normales.SqlDbType = SqlDbType.Decimal
                param_total_hs_Normales.Precision = 18
                param_total_hs_Normales.Scale = 2
                param_total_hs_Normales.Value = CDbl(lblMontoHsNormales.Text)
                param_total_hs_Normales.Direction = ParameterDirection.Input

                Dim param_total_hs_extras_50 As New SqlClient.SqlParameter
                param_total_hs_extras_50.ParameterName = "@Total_Hs_Extras_50"
                param_total_hs_extras_50.SqlDbType = SqlDbType.Decimal
                param_total_hs_extras_50.Precision = 18
                param_total_hs_extras_50.Scale = 2
                param_total_hs_extras_50.Value = CDbl(lblMontoHsExtras50.Text)
                param_total_hs_extras_50.Direction = ParameterDirection.Input

                Dim param_total_hs_extras_100 As New SqlClient.SqlParameter
                param_total_hs_extras_100.ParameterName = "@Total_Hs_Extras_100"
                param_total_hs_extras_100.SqlDbType = SqlDbType.Decimal
                param_total_hs_extras_100.Precision = 18
                param_total_hs_extras_100.Scale = 2
                param_total_hs_extras_100.Value = CDbl(lblMontoHsExtras100.Text)
                param_total_hs_extras_100.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spConsolidaciones_Insert", _
                                          param_id, param_idempleado, param_hsnormales, _
                                          param_hsextras50, param_hsextras100, param_Periodo, _
                                          param_montohora, param_total_hs_Normales, param_total_hs_extras_50, param_total_hs_extras_100, _
                                          param_res)

                    txtID.Text = param_id.Value

                    AgregarRegistro_Consolidacion = param_res.Value

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
        Dim res As Integer = 0
        Dim i As Integer

        Try
            Try
                For i = 0 To grdItems.RowCount - 1

                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Consolidado).Value = True Then

                        Dim param_idConsolidacion As New SqlClient.SqlParameter
                        param_idConsolidacion.ParameterName = "@idConsolidacion"
                        param_idConsolidacion.SqlDbType = SqlDbType.BigInt
                        param_idConsolidacion.Value = txtID.Text
                        param_idConsolidacion.Direction = ParameterDirection.Input

                        Dim param_IdAsistencia As New SqlClient.SqlParameter
                        param_IdAsistencia.ParameterName = "@IdAsistencia"
                        param_IdAsistencia.SqlDbType = SqlDbType.BigInt
                        param_IdAsistencia.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Id).Value
                        param_IdAsistencia.Direction = ParameterDirection.Input

                        Dim param_hsnormales As New SqlClient.SqlParameter
                        param_hsnormales.ParameterName = "@Horas_Normales"
                        param_hsnormales.SqlDbType = SqlDbType.Decimal
                        param_hsnormales.Precision = 18
                        param_hsnormales.Scale = 2
                        param_hsnormales.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Horas_Normales).Value
                        param_hsnormales.Direction = ParameterDirection.Input

                        Dim param_hsextras50 As New SqlClient.SqlParameter
                        param_hsextras50.ParameterName = "@Horas_Extras_50"
                        param_hsextras50.SqlDbType = SqlDbType.Decimal
                        param_hsextras50.Precision = 18
                        param_hsextras50.Scale = 2
                        param_hsextras50.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Horas_Extras_50).Value
                        param_hsextras50.Direction = ParameterDirection.Input

                        Dim param_hsextras100 As New SqlClient.SqlParameter
                        param_hsextras100.ParameterName = "@Horas_Extras_100"
                        param_hsextras100.SqlDbType = SqlDbType.Decimal
                        param_hsextras100.Precision = 18
                        param_hsextras100.Scale = 2
                        param_hsextras100.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Horas_Extras_100).Value
                        param_hsextras100.Direction = ParameterDirection.Input

                        Dim param_res As New SqlClient.SqlParameter
                        param_res.ParameterName = "@res"
                        param_res.SqlDbType = SqlDbType.Int
                        param_res.Value = DBNull.Value
                        param_res.Direction = ParameterDirection.InputOutput

                        Try

                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spAsistencias_Consolidaciones_Update", _
                                                      param_IdAsistencia, param_idConsolidacion, param_hsnormales, param_hsextras50, _
                                                      param_hsextras100, param_res)

                            res = param_res.Value

                            If (res <= 0) Then
                                Exit For
                            End If

                        Catch ex As Exception
                            Throw ex
                        End Try
                    End If

                Next

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

    'Private Function ActualizarDiaTrabajado(ByRef HsTrabajadas As Double, ByRef HsCargadas As Double, ) As Integer
    Private Function ActualizarDiaTrabajado(ByVal I As Integer) As Integer
        Dim res As Integer = 0

        Try

            Dim connection As SqlClient.SqlConnection = Nothing

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idasistencia"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Id).Value
            param_id.Direction = ParameterDirection.Input

            Dim param_HoraIngreso As New SqlClient.SqlParameter
            param_HoraIngreso.ParameterName = "@Hora_Ingreso"
            param_HoraIngreso.SqlDbType = SqlDbType.Time
            param_HoraIngreso.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Hora_Ingreso).Value
            param_HoraIngreso.Direction = ParameterDirection.Input

            Dim param_HoraEgreso As New SqlClient.SqlParameter
            param_HoraEgreso.ParameterName = "@Hora_Egreso"
            param_HoraEgreso.SqlDbType = SqlDbType.Time
            param_HoraEgreso.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Hora_Egreso).Value
            param_HoraEgreso.Direction = ParameterDirection.Input

            Dim param_LlegadaTarde As New SqlClient.SqlParameter
            param_LlegadaTarde.ParameterName = "@LlegadaTarde"
            param_LlegadaTarde.SqlDbType = SqlDbType.Bit
            param_LlegadaTarde.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Llegada_Tarde).Value
            param_LlegadaTarde.Direction = ParameterDirection.Input

            Dim param_Justificada As New SqlClient.SqlParameter
            param_Justificada.ParameterName = "@Justificada"
            param_Justificada.SqlDbType = SqlDbType.Bit
            param_Justificada.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Justificada).Value
            param_Justificada.Direction = ParameterDirection.Input

            Dim param_HsNormales As New SqlClient.SqlParameter
            param_HsNormales.ParameterName = "@HsNormales"
            param_HsNormales.SqlDbType = SqlDbType.Decimal
            param_HsNormales.Precision = 18
            param_HsNormales.Scale = 2
            param_HsNormales.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Horas_Normales).Value
            param_HsNormales.Direction = ParameterDirection.Input

            Dim param_HsExtras50 As New SqlClient.SqlParameter
            param_HsExtras50.ParameterName = "@HsExtra50"
            param_HsExtras50.SqlDbType = SqlDbType.Decimal
            param_HsExtras50.Precision = 18
            param_HsExtras50.Scale = 2
            param_HsExtras50.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Horas_Extras_50).Value
            param_HsExtras50.Direction = ParameterDirection.Input

            Dim param_HsExtras100 As New SqlClient.SqlParameter
            param_HsExtras100.ParameterName = "@HsExtra100"
            param_HsExtras100.SqlDbType = SqlDbType.Decimal
            param_HsExtras100.Precision = 18
            param_HsExtras100.Scale = 2
            param_HsExtras100.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Horas_Extras_100).Value
            param_HsExtras100.Direction = ParameterDirection.Input

            Dim param_Nota As New SqlClient.SqlParameter
            param_Nota.ParameterName = "@Nota"
            param_Nota.SqlDbType = SqlDbType.VarChar
            param_Nota.Size = 300
            param_Nota.Value = grdItems.Rows(I).Cells(ColumnasDelGridItems.Nota).Value
            param_Nota.Direction = ParameterDirection.Input

            'Dim param_hstrabajadas As New SqlClient.SqlParameter
            'param_hstrabajadas.ParameterName = "@hstrabajadas"
            'param_hstrabajadas.SqlDbType = SqlDbType.Decimal
            'param_hstrabajadas.Precision = 18
            'param_hstrabajadas.Scale = 2
            'param_hstrabajadas.Value = DBNull.Value
            'param_hstrabajadas.Direction = ParameterDirection.InputOutput

            'Dim param_hstrabajadascargadas As New SqlClient.SqlParameter
            'param_hstrabajadascargadas.ParameterName = "@hstrabajadascargadas"
            'param_hstrabajadascargadas.SqlDbType = SqlDbType.Decimal
            'param_hstrabajadascargadas.Precision = 18
            'param_hstrabajadascargadas.Scale = 2
            'param_hstrabajadascargadas.Value = DBNull.Value
            'param_hstrabajadascargadas.Direction = ParameterDirection.InputOutput

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput


            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spAsistencias_Empleados_Update", _
                                        param_id, param_HoraEgreso, param_HoraIngreso, param_LlegadaTarde, _
                                        param_Justificada, param_HsNormales, param_HsExtras50, param_HsExtras100, param_Nota, _
                                        param_res)
            'param_hstrabajadas, param_hstrabajadascargadas, param_res)

            res = param_res.Value

            'HsTrabajadas = param_hstrabajadas.Value
            'HsCargadas = param_hstrabajadascargadas.Value

            ActualizarDiaTrabajado = res


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

    Private Function Actualizar_HsTrabajadas() As Integer
        Dim res As Integer = 0

        Try

            Dim connection As SqlClient.SqlConnection = Nothing

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idasistencia"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Id).Value
            param_id.Direction = ParameterDirection.Input

            Dim param_HoraIngreso As New SqlClient.SqlParameter
            param_HoraIngreso.ParameterName = "@Hora_Ingreso"
            param_HoraIngreso.SqlDbType = SqlDbType.Time
            param_HoraIngreso.Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Ingreso).Value
            param_HoraIngreso.Direction = ParameterDirection.Input

            Dim param_HoraEgreso As New SqlClient.SqlParameter
            param_HoraEgreso.ParameterName = "@Hora_Egreso"
            param_HoraEgreso.SqlDbType = SqlDbType.Time
            param_HoraEgreso.Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Egreso).Value
            param_HoraEgreso.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spAsistencias_Empleados_Update_SoloHoras", _
                                      param_id, param_HoraEgreso, param_HoraIngreso, param_res)

            res = param_res.Value

            Actualizar_HsTrabajadas = res

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

    Private Function AgregarRegistro_DiaTrabajado() As Integer

        Try

            Dim connection As SqlClient.SqlConnection = Nothing

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idempleado"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = cmbEmpleado.SelectedValue
            param_id.Direction = ParameterDirection.Input

            Dim param_Fecha As New SqlClient.SqlParameter
            param_Fecha.ParameterName = "@Fecha"
            param_Fecha.SqlDbType = SqlDbType.Date
            param_Fecha.Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Fecha).Value
            param_Fecha.Direction = ParameterDirection.Input

            Dim param_HoraIngreso As New SqlClient.SqlParameter
            param_HoraIngreso.ParameterName = "@HoraIngreso"
            param_HoraIngreso.SqlDbType = SqlDbType.Time
            param_HoraIngreso.Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Ingreso).Value
            param_HoraIngreso.Direction = ParameterDirection.Input

            Dim param_HoraEgreso As New SqlClient.SqlParameter
            param_HoraEgreso.ParameterName = "@HoraEgreso"
            param_HoraEgreso.SqlDbType = SqlDbType.Time
            param_HoraEgreso.Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Egreso).Value
            param_HoraEgreso.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput


            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spAsistencias_Empleados_Insert", _
                                      param_id, param_Fecha, param_HoraEgreso, param_HoraIngreso, param_res)

            AgregarRegistro_DiaTrabajado = param_res.Value

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

                Dim param_idPresGest As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_idPresGest.Value = CType(txtID.Text, Long)
                param_idPresGest.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spConsolidaciones_Delete", _
                                              param_idPresGest, param_res)

                    EliminarRegistro = param_res.Value

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

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        band = 0
        bolModo = True

        'Label14.Text = "Total por Pagar"

        grdItems.Enabled = bolModo
        cmbEmpleado.Enabled = bolModo

        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        Util.LimpiarTextBox(Me.Controls)
        PrepararGridItems()

        lblHsNormales.Text = "0"
        lblHsExtras50.Text = "0"
        lblHsExtras100.Text = "0"

        lblMontoHsNormales.Text = "0"
        lblMontoHsExtras50.Text = "0"
        lblMontoHsExtras100.Text = "0"

        lblTotalConsolidacion.Text = "0"

        band = 1

        cmbPeriodos.SelectedIndex = 0
        cmbEmpleado.SelectedIndex = 0

        'cmbClientes_SelectedIndexChanged(sender, e)

        'grdItems.ReadOnly = False

        cmbEmpleado.Focus()


    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer

        If MessageBox.Show("Está seguro que desea Consolidar los días del Empleado: " & cmbEmpleado.Text.ToCharArray & "?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        If bolModo = False Then
            MsgBox("No se puede modificar una consolidación", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If

        Util.MsgStatus(Status1, "Controlando la información...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarRegistro_Consolidacion()
                Select Case res
                    Case Is <= 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo insertar el encabezado de la consolidación.", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los movimientos asociados a la Consolidación...", My.Resources.Resources.indicator_white)
                        res = AgregarRegistro_Items()
                        Select Case res
                            Case Is <= 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se puede insertar el detalle de la consolidación.", My.Resources.Resources.stop_error.ToBitmap)
                            Case Else

                                Cerrar_Tran()

                                bolModo = False

                                PrepararBotones()

                                SQL = "exec spConsolidaciones_Select_All @Eliminado = 0"

                                btnActualizar_Click(sender, e)

                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                                'grdItems.ReadOnly = bolModo

                        End Select
                End Select

                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
        'LlenarGridItems(CType(grd.CurrentRow.Cells(19).Value, Long))

        'grdItems.ReadOnly = Not bolModo

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer

        If MessageBox.Show("Esta acción eliminará toda la consolidación del Empleado: " & cmbEmpleado.Text.ToCharArray & " ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro()
            Select Case res
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación de la Consolidación.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación de la Consolidación.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación de la Consolidación.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación de la Consolidación.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case Else
                    Cerrar_Tran()
                    bolModo = False
                    PrepararBotones()

                    SQL = "exec spConsolidaciones_Select_All @Eliminado = 0"

                    btnActualizar_Click(sender, e)
                    'Setear_Grilla()
                    Util.MsgStatus(Status1, "Se anuló correctamente la Consolidación.", My.Resources.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se anuló correctamente la Consolidación.", My.Resources.Resources.ok.ToBitmap, True, True)
            End Select
            '
            ' cerrar la conexion si está abierta.
            '
            If Not conn_del_form Is Nothing Then
                CType(conn_del_form, IDisposable).Dispose()
            End If
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringHOLLMAN)

        Dim Codigo As String

        paramreporte.AgregarParametros("Codigo :", "STRING", "", True, grd.CurrentRow.Cells(1).Value, "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Consolidaciones"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim Band_Excel As Boolean = False

        If MessageBox.Show("¿Desea Generar un Excel con los Datos?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Band_Excel = True
        End If

        If cerroparametrosconaceptar = True Then
            Codigo = paramreporte.ObtenerParametros(0).ToString

            If Band_Excel = True Then
                GenerarExcel_Consolidacion(Codigo)
            Else
                rpt.Consolidaciones_App(Codigo, rpt, My.Application.Info.AssemblyName.ToString)
            End If

            cerroparametrosconaceptar = False
        End If

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

    End Sub

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    'Private Sub grdItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellContentClick
    '    If bolModo = True Then
    '        If e.ColumnIndex = ColumnasDelGridItems.Llegada_Tarde And grdItems.CurrentRow.Cells(ColumnasDelGridItems.Llegada_Tarde).Value = False Then

    '            Dim connection As SqlClient.SqlConnection = Nothing
    '            Dim ds As Data.DataSet

    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringSEI)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Sub
    '            End Try

    '            Try
    '                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "DECLARE @HoraIngreso TIME(0) = (SELECT CONVERT (time(0), Hora_Ingreso) FROM Jornadas J JOIN Jornadas_Det JD ON J.Id = JD.IdJornada JOIN Empleados E ON E.IdJornada = J.Id WHERE E.ID = " + CStr(cmbEmpleado.SelectedValue) + " AND Dia = DATENAME(WEEKDAY, '" + grdItems.CurrentRow.Cells(ColumnasDelGridItems.Fecha).Value + "')) SELECT IIF(@HoraIngreso = '', '', @HoraIngreso)")
    '                ds.Dispose()

    '                Dim HoraIngresoEstrablecida = ds.Tables(0).Rows(0).Item(0).ToString

    '                If HoraIngresoEstrablecida <> "" Then
    '                    If MessageBox.Show("Desea modificar el horario de ingreso?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Hora_Ingreso).Value = HoraIngresoEstrablecida
    '                    Else
    '                        Exit Sub
    '                    End If
    '                End If

    '            Catch ex As Exception
    '                Dim errMessage As String = ""
    '                Dim tempException As Exception = ex

    '                While (Not tempException Is Nothing)
    '                    errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                    tempException = tempException.InnerException
    '                End While

    '                MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '                  + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '                  "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Finally
    '                If Not connection Is Nothing Then
    '                    CType(connection, IDisposable).Dispose()
    '                End If
    '            End Try
    '        Else
    '            Exit Sub
    '        End If
    '    End If

    'End Sub

    Private Sub grdItems_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles grdItems.DataError

    End Sub

    Private Sub cmbPeriodos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriodos.SelectedIndexChanged
        If band = 1 Then
            If bolModo = True Then
                LlenarGridItems(CType(cmbEmpleado.SelectedValue, Long))
                AgregarDiaConsolidado_tmp()
            Else
                lblHsNormales.Text = "0"
                lblHsExtras50.Text = "0"
                lblHsExtras100.Text = "0"

                lblMontoHsNormales.Text = "0"
                lblMontoHsExtras50.Text = "0"
                lblMontoHsExtras100.Text = "0"

                lblTotalConsolidacion.Text = "0"
            End If
        End If
    End Sub

    'Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

    '    Try

    '        ' Si la tecla presionada es distinta de la tecla Enter,
    '        ' abandonamos el procedimiento.


    '        If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)


    '        ' Igualmente, si el control DataGridView no tiene el foco,
    '        ' y si la celda actual no está siendo editada,
    '        ' abandonamos el procedimiento.
    '        '        If ((Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode)) And _
    '        '           ((Not grdOfertaComercial.Focused) AndAlso (Not grdOfertaComercial.IsCurrentCellInEditMode)) Then Return MyBase.ProcessCmdKey(msg, keyData)

    '        Dim cell As DataGridViewCell

    '        Dim columnIndex As Int32
    '        Dim rowIndex As Int32

    '        If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then
    '                Return MyBase.ProcessCmdKey(msg, keyData)
    '        Else

    '            cell = grdItems.CurrentCell

    '            columnIndex = cell.ColumnIndex
    '            rowIndex = cell.RowIndex

    '            Do
    '                If columnIndex = grdItems.Columns.Count - 1 Then
    '                    If rowIndex = grdItems.Rows.Count - 1 Then
    '                        ' Seleccionamos la primera columna de la primera fila.
    '                        cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.Id)
    '                    Else
    '                        ' Selecionamos la primera columna de la siguiente fila.
    '                        cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.Id)
    '                    End If
    '                Else
    '                    ' Seleccionamos la celda de la derecha de la celda actual.
    '                    cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
    '                End If
    '                ' establecer la fila y la columna actual
    '                columnIndex = cell.ColumnIndex
    '                rowIndex = cell.RowIndex
    '            Loop While (cell.Visible = False)

    '            grdItems.CurrentCell = cell

    '            'SendKeys.Send("{TAB}")


    '            ' ... y la ponemos en modo de edición.
    '            grdItems.BeginEdit(True)

    '        End If

    '        grdItems.CurrentCell = cell

    '        'SendKeys.Send("{TAB}")
    '        Try
    '            If grdItems.CurrentCell.ColumnIndex - 2 = ColumnasDelGridItems.Nota Then
    '                grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Hora_Ingreso, grdItems.CurrentRow.Index + 1)
    '                Return True
    '            End If

    '            'If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Nota Then
    '            '    If IIf(grdItems.CurrentCell.Value Is DBNull.Value, "", grdItems.CurrentCell.Value) = "" Then
    '            '        grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material_Prov, grdItems.CurrentRow.Index)
    '            '    Else
    '            '        grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Ganancia, grdItems.CurrentRow.Index)
    '            '    End If

    '            '    Return True
    '            'End If

    '        Catch ex As Exception
    '            Return False
    '        End Try

    '        ' ... y la ponemos en modo de edición.
    '        grdItems.BeginEdit(True)

    '        Return True

    '    Catch ex As Exception
    '        Return False

    '    End Try

    'End Function

    Private Sub GenerarExcel_Consolidacion(ByVal NroConsolidacion As Integer)

        Dim excel As Microsoft.Office.Interop.Excel.Application

        'Dim workbook As Microsoft.Office.Interop.Excel.Workbook
        Dim oBook As Excel.WorkbookClass
        Dim oBooks As Excel.Workbooks

        Dim oSheet As Excel.Worksheet

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Empresa As Data.DataSet
        Dim Empleado As String
        Dim Periodo As String, Inicio As String, Fin As String

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            excel = New Microsoft.Office.Interop.Excel.Application

            Dim ds As Data.DataSet

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select (e.apellido + ', ' + e.nombre) as Nombre, REPLACE(CONVERT(VARCHAR(10), fechainicio, 103),'/','') AS Inicio, REPLACE(CONVERT(VARCHAR(10), fechafin, 103),'/','') AS Fin from consolidaciones c JOIN Empleados e ON e.id = c.id_empleado where c.CODIGO = " & NroConsolidacion)

            ds.Dispose()

            Empleado = LTrim(RTrim(ds.Tables(0).Rows(0).Item(0)))
            Inicio = LTrim(RTrim(ds.Tables(0).Rows(0).Item(1)))
            Fin = LTrim(RTrim(ds.Tables(0).Rows(0).Item(2)))
            Periodo = LTrim(RTrim(ds.Tables(0).Rows(0).Item(1))) & "-" & LTrim(RTrim(ds.Tables(0).Rows(0).Item(2)))

            If File.Exists(Application.StartupPath.ToString & "\Consolidaciones\" & Util.Empresa & " - Empleado - " & LTrim(RTrim(ds.Tables(0).Rows(0).Item(0))) & " - Periodo " & LTrim(RTrim(ds.Tables(0).Rows(0).Item(1))) & "-" & LTrim(RTrim(ds.Tables(0).Rows(0).Item(2))) & ".xlsx") Then File.Delete(Application.StartupPath.ToString & "\Consolidaciones\" & Util.Empresa & " - Empleado - " & LTrim(RTrim(ds.Tables(0).Rows(0).Item(0))) & " - Periodo " & LTrim(RTrim(ds.Tables(0).Rows(0).Item(1))) & "-" & LTrim(RTrim(ds.Tables(0).Rows(0).Item(2))) & ".xlsx")

            oBook = excel.Workbooks.Open(Application.StartupPath.ToString + "\Consolidaciones\Consolidacion.xlsx")

            oBook.SaveAs(Application.StartupPath.ToString & "\Consolidaciones\" & Util.Empresa & " - Empleado - " & LTrim(RTrim(ds.Tables(0).Rows(0).Item(0))) & " - Periodo " & LTrim(RTrim(ds.Tables(0).Rows(0).Item(1))) & "-" & LTrim(RTrim(ds.Tables(0).Rows(0).Item(2))) & ".xlsx")

            Dim celda As String

            oBooks = excel.Workbooks

            oSheet = oBook.Sheets(1)

            celda = "c" + CStr(8)


            Try

                Dim sqlstring As String

                sqlstring = "EXEC spRPT_Consolidaciones " & NroConsolidacion

                ds_Empresa = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                Dim Fila As Integer
                Dim HsNormales As Double = 0, HsExtras50 As Double = 0, hsExtras100 As Double = 0
                Dim TotalHsNormales As Double = 0, TotalHsExtras50 As Double = 0, TotalhsExtras100 As Double = 0
                Dim Total As Double

                Fila = 0

                oSheet.Cells(2, 2) = Empleado
                oSheet.Cells(3, 2) = CStr(Mid(Inicio, 1, 2)) + "/" + CStr(Mid(Inicio, 3, 2)) + "/" + CStr(Mid(Inicio, 5, 4))
                oSheet.Cells(3, 3) = CStr(Mid(Fin, 1, 2)) + "/" + CStr(Mid(Fin, 3, 2)) + "/" + CStr(Mid(Fin, 5, 4))

                oSheet.Range("a6", "p500").Value = ""

                For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                    oSheet.Cells(Fila + 6, 1) = CDate(ds_Empresa.Tables(0).Rows(Fila)(1)) 'fecha factura
                    oSheet.Cells(Fila + 6, 2) = ds_Empresa.Tables(0).Rows(Fila)(2) 'Tipo
                    oSheet.Cells(Fila + 6, 3) = ds_Empresa.Tables(0).Rows(Fila)(3) 'nRO cOMPROBANTE
                    oSheet.Cells(Fila + 6, 4) = ds_Empresa.Tables(0).Rows(Fila)(4) 'cANCE
                    oSheet.Cells(Fila + 6, 5) = ds_Empresa.Tables(0).Rows(Fila)(5) 'CLIENTE
                    oSheet.Cells(Fila + 6, 6) = ds_Empresa.Tables(0).Rows(Fila)(6) 'tipodoc
                    oSheet.Cells(Fila + 6, 7) = ds_Empresa.Tables(0).Rows(Fila)(7) 'nrodoc

                    HsNormales = HsNormales + CDbl(ds_Empresa.Tables(0).Rows(Fila)(8))
                    oSheet.Cells(Fila + 6, 8) = ds_Empresa.Tables(0).Rows(Fila)(8) 'Remitos

                    TotalHsNormales = TotalHsNormales + CDbl(ds_Empresa.Tables(0).Rows(Fila)(9))
                    oSheet.Cells(Fila + 6, 9) = ds_Empresa.Tables(0).Rows(Fila)(9) 'Subtotal

                    HsExtras50 = HsExtras50 + CDbl(ds_Empresa.Tables(0).Rows(Fila)(10))
                    oSheet.Cells(Fila + 6, 10) = ds_Empresa.Tables(0).Rows(Fila)(10) 'IVA10

                    TotalHsExtras50 = TotalHsExtras50 + CDbl(ds_Empresa.Tables(0).Rows(Fila)(11))
                    oSheet.Cells(Fila + 6, 11) = ds_Empresa.Tables(0).Rows(Fila)(11) 'iva21

                    hsExtras100 = hsExtras100 + CDbl(ds_Empresa.Tables(0).Rows(Fila)(12))
                    oSheet.Cells(Fila + 6, 12) = ds_Empresa.Tables(0).Rows(Fila)(12) 'Total

                    TotalhsExtras100 = TotalhsExtras100 + CDbl(ds_Empresa.Tables(0).Rows(Fila)(13))
                    oSheet.Cells(Fila + 6, 13) = ds_Empresa.Tables(0).Rows(Fila)(13) 'nrofacturaorig

                    Total = Total + CDbl(ds_Empresa.Tables(0).Rows(Fila)(14))
                    oSheet.Cells(Fila + 6, 14) = ds_Empresa.Tables(0).Rows(Fila)(14) 'nrofacturaorig
                Next

                oSheet.Cells(Fila + 6, 7) = "Totales"
                oSheet.Cells(Fila + 6, 8) = HsNormales
                oSheet.Cells(Fila + 6, 9) = TotalHsNormales

                oSheet.Cells(Fila + 6, 10) = HsExtras50
                oSheet.Cells(Fila + 6, 11) = TotalHsExtras50

                oSheet.Cells(Fila + 6, 12) = hsExtras100
                oSheet.Cells(Fila + 6, 13) = TotalhsExtras100

                oSheet.Cells(Fila + 6, 14) = Total

                oSheet.Columns.AutoFit()

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

            excel.Visible = True

            oBook.Activate()

        Catch ex As COMException
            MessageBox.Show("Error accessing Excel: " + ex.ToString())

        Catch ex As Exception
            MessageBox.Show("Error: " + ex.ToString())

        End Try

    End Sub

End Class