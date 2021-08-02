Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmMaqHerramientas

    Dim bolpoliticas As Boolean
    Private ds_2 As DataSet
    Dim permitir_evento_CellChanged As Boolean
    Dim tran As SqlClient.SqlTransaction
    Dim Cell_X As Integer, Cell_Y As Integer


    Dim editando_celda As Boolean
    Dim llenandoCombo As Boolean = False

    Private bs As New BindingSource

    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    Dim band As Integer

    Dim dtpGrilla As DateTimePicker

#Region "Componentes Formulario"

    Private Sub frmClientes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Conjunto de Herramientas Nuevas que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Conjunto de Herramientas?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Enum ColumnasDelGridItems
        idMH = 0
        descripcion = 1
        fechaMantenimiento = 2
        fechaProxMantenimiento = 3
        idProv = 4
        Proveedor = 5
        idEmp = 6
        empleado = 7
        precioHora = 8
        horas = 9
        costoMO = 10
        costoInsumo = 11
        subtotalcostomto = 12
    End Enum

    Private Sub frmClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        'BtnActivar.Enabled = True

        band = 0

        configurarform()
        asignarTags()

        Me.LlenarMotivoBaja()
        LlenarcmbMarcas_App(cmbMarcas, ConnStringSEI, llenandoCombo)

        SQL = "exec spMaquinasHerramientas_Select_All @Eliminado = 0"

        LlenarGrilla()
        Permitir = True

        CargarCajas()

        PrepararBotones()

        'Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems()
            btnNuevo_Click(sender, e)
        Else
            LlenarGridItems()
        End If

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
        End If

        grd.Columns(4).Visible = False

        dtpGrilla = New DateTimePicker
        dtpGrilla.Format = DateTimePickerFormat.Short
        dtpGrilla.Visible = False
        dtpGrilla.Width = grdItems.Columns(ColumnasDelGridItems.fechaMantenimiento).Width

        AddHandler dtpGrilla.Leave, AddressOf dtpGrilla_Leave


        grdItems.Controls.Add(dtpGrilla)

        permitir_evento_CellChanged = True

    End Sub

    Public Sub dtpGrilla_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        dtpGrilla.Visible = False
    End Sub

    Private Sub frmMaqHerramientas_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            'If txtID.Text <> "" Then
            LlenarGridItems()
            'End If
        End If
    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEliminados.CheckedChanged

        btnNuevo.Enabled = Not chkEliminados.Checked
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spMaquinasHerramientas_Select_All @Eliminado = 1"
        Else
            SQL = "exec spMaquinasHerramientas_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        LlenarGridItems()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
        End If

    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then

            LlenarGridItems()

            'Try
            '    If grd.RowCount > 0 Then

            '    End If
            'Catch ex As Exception

            'End Try

        End If

    End Sub

    Private Sub PicMarcas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicMarcas.Click
        Dim f As New frmMarcas

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbMarcas.Text.ToUpper.ToString
        f.Origen = 1
        f.ShowDialog()
        LlenarcmbMarcas_App(cmbMarcas, ConnStringSEI, llenandoCombo)

    End Sub

    Private Sub cmbMarcas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMarcas.SelectedIndexChanged
        Try
            txtidMarca.Text = cmbMarcas.SelectedValue
        Catch ex As Exception

        End Try

    End Sub

#End Region

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.idMH)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.idMH)
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
            'MsgBox(ex.ToString)
        End Try

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        Me.LlenarMotivoBaja()

        PrepararGridItems()

        dtpFechaBaja.Enabled = False
        cmbMotivoBaja.Enabled = False
        chkCertificado.Checked = False

        txtCODIGO.Focus()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer, res_item As Integer ', res_notas As Integer

        Util.Logueado_OK = False

        If bolModo = False Then
            If MessageBox.Show("¿Está seguro que desea modificar la máquina o herramienta seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo Insertar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Insertar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 20
                        Cerrar_Tran()
                        band = 0
                        bolModo = False
                        SQL = "exec [spMaquinasHerramientas_Select_All] @eliminado = 0"
                        btnActualizar_Click(sender, e)
                        Util.MsgStatus(Status1, "El registro se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                        band = 1
                        Exit Sub
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                        res_item = AgregarRegistro_Items()
                        Select Case res_item
                            Case -30
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Insertar el registro", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Insertar el registro", My.Resources.Resources.stop_error.ToBitmap, True)
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

                                band = 0
                                bolModo = False
                                SQL = "exec [spMaquinasHerramientas_Select_All] @eliminado = 0"
                                btnActualizar_Click(sender, e)

                                dtpFechaBaja.Enabled = True
                                cmbMotivoBaja.Enabled = True

                                Util.MsgStatus(Status1, "El registro se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                                band = 1
                        End Select
                End Select
            End If
        End If


    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer

        If MessageBox.Show("Está seguro que desea eliminar el Equipo seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        res = EliminarRegistro()
        Select Case res
            Case -2
                Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
            Case -1
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
            Case 0
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
            Case Else
                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                If Me.grd.RowCount = 0 Then
                    bolModo = True
                    PrepararBotones()
                    Util.LimpiarTextBox(Me.Controls)
                Else
                    bolModo = False
                    btnActualizar_Click(sender, e)

                    dtpFechaBaja.Enabled = True
                    cmbMotivoBaja.Enabled = True
                End If
        End Select
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes

        nbreformreportes = "Maquinas y Herramientas SGI"

        Cursor = Cursors.WaitCursor

        rpt.SEI_MaquinasHerramientas_SGI(rpt)

        Cursor = Cursors.Default

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If txtID.Text <> "" Then
            LlenarGridItems()

            dtpFechaBaja.Enabled = True
            cmbMotivoBaja.Enabled = True
        End If

    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente el registro: " & grd.CurrentRow.Cells(4).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            'llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE MaquinasHerramientas SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            SQL = "exec spMaquinasHerramientas_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "El registro se activó correctamente.", My.Resources.ok.ToBitmap)

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

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Máquinas y Herramientas"

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

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 70)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFechaAlta.Tag = "2"
        txtNOMBRE.Tag = "3"
        txtidMarca.Tag = "4"
        cmbMarcas.Tag = "5"
        txtModelo.Tag = "6"
        txtNroSerie.Tag = "7"
        txtDescripcion.Tag = "8"
        chkCertificado.Tag = "9"
        dtpFechaBaja.Tag = "10"
        cmbMotivoBaja.Tag = "11"
        txtTotal.Tag = "12"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        bolpoliticas = True

    End Sub

    Private Sub LlenarMotivoBaja()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT MotivoBaja FROM MaquinasHerramientas ORDER BY MotivoBaja")
            ds.Dispose()

            With cmbMotivoBaja
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "motivobaja"
                '.ValueMember = "IdUsuario"
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

#Region "Funciones"

    Private Function AgregarActualizar_Registro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        'Dim res As Integer = 0

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
            If bolModo = True Then
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput
            Else
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input
            End If

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.BigInt
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.Input

            Dim param_fecha As New SqlClient.SqlParameter
            param_fecha.ParameterName = "@fechaalta"
            param_fecha.SqlDbType = SqlDbType.DateTime
            param_fecha.Value = dtpFechaAlta.Value
            param_fecha.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 100
            param_nombre.Value = txtNOMBRE.Text
            param_nombre.Direction = ParameterDirection.Input

            Dim param_descripcion As New SqlClient.SqlParameter
            param_descripcion.ParameterName = "@descripcion"
            param_descripcion.SqlDbType = SqlDbType.VarChar
            param_descripcion.Size = 300
            param_descripcion.Value = txtDescripcion.Text
            param_descripcion.Direction = ParameterDirection.Input

            Dim param_idmarca As New SqlClient.SqlParameter
            param_idmarca.ParameterName = "@idMarca"
            param_idmarca.SqlDbType = SqlDbType.BigInt
            param_idmarca.Value = IIf(txtidMarca.Text = "", 0, txtidMarca.Text)
            param_idmarca.Direction = ParameterDirection.Input

            Dim param_modelo As New SqlClient.SqlParameter
            param_modelo.ParameterName = "@modelo"
            param_modelo.SqlDbType = SqlDbType.VarChar
            param_modelo.Size = 100
            param_modelo.Value = txtModelo.Text
            param_modelo.Direction = ParameterDirection.Input

            Dim param_NroSerie As New SqlClient.SqlParameter
            param_NroSerie.ParameterName = "@nroserie"
            param_NroSerie.SqlDbType = SqlDbType.VarChar
            param_NroSerie.Size = 100
            param_NroSerie.Value = txtNroSerie.Text
            param_NroSerie.Direction = ParameterDirection.Input

            Dim param_certificado As New SqlClient.SqlParameter
            param_certificado.ParameterName = "@certificadocalibracion"
            param_certificado.SqlDbType = SqlDbType.Bit
            param_certificado.Value = chkCertificado.Checked
            param_certificado.Direction = ParameterDirection.Input

            Dim param_fechabaja As New SqlClient.SqlParameter
            param_fechabaja.ParameterName = "@fechabaja"
            param_fechabaja.SqlDbType = SqlDbType.DateTime
            param_fechabaja.Value = dtpFechaAlta.Value
            param_fechabaja.Direction = ParameterDirection.Input

            Dim param_MotivoBaja As New SqlClient.SqlParameter
            param_MotivoBaja.ParameterName = "@motivobaja"
            param_MotivoBaja.SqlDbType = SqlDbType.VarChar
            param_MotivoBaja.Size = 300
            param_MotivoBaja.Value = cmbMotivoBaja.Text
            param_MotivoBaja.Direction = ParameterDirection.Input

            Dim TotalCostoMto As New SqlClient.SqlParameter
            TotalCostoMto.ParameterName = "@TotalCostoMto"
            TotalCostoMto.SqlDbType = SqlDbType.Decimal
            TotalCostoMto.Precision = 18
            TotalCostoMto.Scale = 2
            TotalCostoMto.Value = IIf(txtTotal.Text = "", "0", txtTotal.Text)
            TotalCostoMto.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Insert", param_id, _
                                          param_codigo, param_fecha, param_nombre, param_descripcion, param_idmarca, param_modelo, _
                                          param_NroSerie, param_certificado, TotalCostoMto, param_useradd, param_res)

                    txtID.Text = param_id.Value

                Else
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_update", param_id, _
                                            param_fecha, param_nombre, param_descripcion, param_idmarca, param_modelo, _
                                            param_NroSerie, param_certificado, TotalCostoMto, param_fechabaja, param_MotivoBaja, param_useradd, param_res)
                End If


                AgregarActualizar_Registro = param_res.Value


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

    Private Function AgregarRegistro_Items() As Integer

        Dim res As Integer = 0 ', res_del As Integer
        Dim i As Integer

        Try

            Try

                If grdItems.RowCount - 1 = 0 Then
                    AgregarRegistro_Items = 1
                    Exit Function
                End If

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Det_Delete", _
                                                  param_idmpdelete, param_res)

                        res = CInt(param_res.Value)

                    Catch ex As Exception
                        Throw ex
                    End Try
                End If

                i = 0

                Do While i < grdItems.Rows.Count - 1
                    'Dim id As Long

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = DBNull.Value
                    param_id.Direction = ParameterDirection.Input

                    Dim IdMaqHerramienta As New SqlClient.SqlParameter
                    IdMaqHerramienta.ParameterName = "@IdMaqHerramienta"
                    IdMaqHerramienta.SqlDbType = SqlDbType.BigInt
                    IdMaqHerramienta.Value = txtID.Text
                    IdMaqHerramienta.Direction = ParameterDirection.Input

                    Dim fechamantenimiento As New SqlClient.SqlParameter
                    fechamantenimiento.ParameterName = "@fechamantenimiento"
                    fechamantenimiento.SqlDbType = SqlDbType.Date
                    fechamantenimiento.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.fechaMantenimiento).Value, Date)
                    fechamantenimiento.Direction = ParameterDirection.Input

                    Dim descripcion As New SqlClient.SqlParameter
                    descripcion.ParameterName = "@descripcion"
                    descripcion.SqlDbType = SqlDbType.NVarChar
                    descripcion.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.descripcion).Value
                    descripcion.Direction = ParameterDirection.Input

                    Dim idempleado As New SqlClient.SqlParameter
                    idempleado.ParameterName = "@idempleado"
                    idempleado.SqlDbType = SqlDbType.BigInt
                    idempleado.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.idEmp).Value
                    idempleado.Direction = ParameterDirection.Input

                    Dim idProveedor As New SqlClient.SqlParameter
                    idProveedor.ParameterName = "@idProveedor"
                    idProveedor.SqlDbType = SqlDbType.BigInt
                    idProveedor.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.idProv).Value
                    idProveedor.Direction = ParameterDirection.Input

                    Dim horas As New SqlClient.SqlParameter
                    horas.ParameterName = "@horas"
                    horas.SqlDbType = SqlDbType.Decimal
                    horas.Precision = 18
                    horas.Scale = 2
                    horas.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.horas).Value
                    horas.Direction = ParameterDirection.Input

                    Dim preciohora As New SqlClient.SqlParameter
                    preciohora.ParameterName = "@preciohora"
                    preciohora.SqlDbType = SqlDbType.Decimal
                    preciohora.Precision = 18
                    preciohora.Scale = 2
                    preciohora.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.precioHora).Value
                    preciohora.Direction = ParameterDirection.Input

                    Dim fechaproximomantenimiento As New SqlClient.SqlParameter
                    fechaproximomantenimiento.ParameterName = "@fechaproximomantenimiento"
                    fechaproximomantenimiento.SqlDbType = SqlDbType.Date
                    fechaproximomantenimiento.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.fechaProxMantenimiento).Value, Date)
                    fechaproximomantenimiento.Direction = ParameterDirection.Input

                    Dim costoMO As New SqlClient.SqlParameter
                    costoMO.ParameterName = "@costoMO"
                    costoMO.SqlDbType = SqlDbType.Decimal
                    costoMO.Precision = 18
                    costoMO.Scale = 2
                    costoMO.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.costoMO).Value
                    costoMO.Direction = ParameterDirection.Input

                    Dim costoinsumo As New SqlClient.SqlParameter
                    costoinsumo.ParameterName = "@costoinsumo"
                    costoinsumo.SqlDbType = SqlDbType.Decimal
                    costoinsumo.Precision = 18
                    costoinsumo.Scale = 2
                    costoinsumo.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.costoInsumo).Value
                    costoinsumo.Direction = ParameterDirection.Input

                    Dim subtotalcostomto As New SqlClient.SqlParameter
                    subtotalcostomto.ParameterName = "@subtotalcostomto"
                    subtotalcostomto.SqlDbType = SqlDbType.Decimal
                    subtotalcostomto.Precision = 18
                    subtotalcostomto.Scale = 2
                    subtotalcostomto.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.subtotalcostomto).Value
                    subtotalcostomto.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spMaquinasHerramientas_Det_Insert]", _
                                              IdMaqHerramienta, fechamantenimiento, descripcion, idempleado, idProveedor, horas, preciohora, _
                                              fechaproximomantenimiento, costoMO, costoinsumo, subtotalcostomto, param_useradd, param_res)

                        res = param_res.Value

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

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
            param_id.Value = CType(txtID.Text, Long)
            param_id.Direction = ParameterDirection.Input

            Dim param_motivo As New SqlClient.SqlParameter
            param_motivo.ParameterName = "@motivo"
            param_motivo.SqlDbType = SqlDbType.VarChar
            param_motivo.Size = 300
            param_motivo.Value = cmbMotivoBaja.Text
            param_motivo.Direction = ParameterDirection.Input

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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMaquinasherramientas_Delete", param_id, _
                                          param_motivo, param_userdel, param_res)

                EliminarRegistro = param_res.Value

            Catch ex As Exception
                '' 
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
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

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


#Region "   GridItems"

    Private Sub grdItems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        Dim dsGrilla As Data.DataSet

        If e.ColumnIndex = ColumnasDelGridItems.empleado Then

            Try
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value = "" Or _
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value Is DBNull.Value Then

                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.idEmp).Value = 0
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.precioHora).Value = 0

                    grdItems.CurrentCell = grdItems.CurrentRow.Cells(ColumnasDelGridItems.costoInsumo)

                Else
                    dsGrilla = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT id, PrecioHora, Apellido + ' ' + Nombre  FROM Empleados WHERE Apellido + ' ' + Nombre = '" & grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value & "'")
                    dsGrilla.Dispose()

                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.idEmp).Value = dsGrilla.Tables(0).Rows(0).Item(0).ToString
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.precioHora).Value = IIf(dsGrilla.Tables(0).Rows(0).Item(1).ToString <> "", dsGrilla.Tables(0).Rows(0).Item(1).ToString, 0)

                    grdItems.CurrentCell = grdItems.CurrentRow.Cells(ColumnasDelGridItems.horas)

                End If
                
            Catch ex As Exception
                Dim errMessage As String = ""
                Dim tempException As Exception = ex

                While (Not tempException Is Nothing)
                    errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                    tempException = tempException.InnerException
                End While
            End Try

        End If

        If e.ColumnIndex = ColumnasDelGridItems.Proveedor Then
            Try
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value = "" Or _
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value Is DBNull.Value Then

                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.idProv).Value = 0

                    grdItems.CurrentCell = grdItems.CurrentRow.Cells(ColumnasDelGridItems.costoInsumo)

                Else
                    dsGrilla = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT id FROM Proveedores WHERE Nombre = '" & grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value & "'")
                    dsGrilla.Dispose()

                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.idProv).Value = dsGrilla.Tables(0).Rows(0).Item(0).ToString

                    grdItems.CurrentCell = grdItems.CurrentRow.Cells(ColumnasDelGridItems.horas)

                End If

            Catch ex As Exception
                Dim errMessage As String = ""
                Dim tempException As Exception = ex

                While (Not tempException Is Nothing)
                    errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                    tempException = tempException.InnerException
                End While
            End Try

        End If

    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Dim cell As DataGridViewCell = grdItems.CurrentCell

        Try
            If grdItems.Focused = True And grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.fechaMantenimiento Or grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.fechaProxMantenimiento Then
                grdItems.CurrentCell.Value = dtpGrilla.Value.Date
            End If


            If grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.horas Or _
                grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.empleado Or _
                grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.costoInsumo Then

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoMO).Value Is Nothing Or _
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoMO).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoMO).Value = 0
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value Is DBNull.Value Or _
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value Is Nothing Then

                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.idEmp).Value = 0
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.precioHora).Value = 0

                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value Is DBNull.Value Or _
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value Is Nothing Then

                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.idProv).Value = 0

                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.horas).Value Is DBNull.Value Or _
                 grdItems.CurrentRow.Cells(ColumnasDelGridItems.horas).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.horas).Value = 0

                End If

                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoInsumo).Value Is Nothing Or _
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoInsumo).Value Is DBNull.Value Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoInsumo).Value = 0
                End If

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoMO).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.precioHora).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.horas).Value
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.subtotalcostomto).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoInsumo).Value + grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.costoMO).Value

                txtTotal.Text = 0

                Dim i As Integer

                For i = 0 To grdItems.Rows.Count - 1 '16
                    Try
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.descripcion).Value <> Nothing Then
                            txtTotal.Text = FormatNumber(CDbl(txtTotal.Text) + grdItems.Rows(i).Cells(ColumnasDelGridItems.subtotalcostomto).Value, 2)
                        End If
                    Catch ex As Exception

                    End Try
                Next


            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing
        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.empleado Then
            Dim dgv As DataGridView = Me.grdItems
            If (TypeOf e.Control Is ComboBox) Then
                Dim cbo As ComboBox = DirectCast(e.Control, ComboBox)
                cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                cbo.AutoCompleteSource = AutoCompleteSource.ListItems
                cbo.DropDownStyle = ComboBoxStyle.DropDown
                AddHandler e.Control.KeyPress, AddressOf NoValidar
                dgv.NotifyCurrentCellDirty(True)
            End If
        Else
            If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Proveedor Then
                Dim dgv As DataGridView = Me.grdItems
                If (TypeOf e.Control Is ComboBox) Then
                    Dim cbo As ComboBox = DirectCast(e.Control, ComboBox)
                    cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    cbo.AutoCompleteSource = AutoCompleteSource.ListItems
                    cbo.DropDownStyle = ComboBoxStyle.DropDown
                    AddHandler e.Control.KeyPress, AddressOf NoValidar
                    dgv.NotifyCurrentCellDirty(True)
                End If
            Else

                AddHandler e.Control.KeyPress, AddressOf NoValidar
            End If
        End If

        If grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.empleado Then
            Dim tb As ComboBox = CType(e.Control, ComboBox)
            AddHandler tb.Enter, AddressOf GrdItems_CmbFocus
        End If

        If grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Proveedor Then
            Dim tb As ComboBox = CType(e.Control, ComboBox)
            AddHandler tb.Enter, AddressOf GrdItems_CmbFocus
        End If

        If grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.fechaMantenimiento Or _
            grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.fechaProxMantenimiento Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            AddHandler tb.KeyPress, AddressOf FM_KeyPress
        End If


    End Sub


    Private Sub FM_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Char.IsLetterOrDigit(e.KeyChar) Or Char.IsSymbol(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Or Char.IsSeparator(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub

    Private Sub grdItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellValueChanged
        Dim dsGrilla As Data.DataSet

        If e.ColumnIndex = ColumnasDelGridItems.empleado Then

            Try

                dsGrilla = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT id, PrecioHora, (Apellido + ' ' + Nombre) as empleado FROM Empleados WHERE id = '" & grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value & "'")
                dsGrilla.Dispose()

                grdItems.CurrentRow.Cells(ColumnasDelGridItems.idEmp).Value = dsGrilla.Tables(0).Rows(0).Item(0).ToString
                grdItems.CurrentRow.Cells(ColumnasDelGridItems.precioHora).Value = IIf(dsGrilla.Tables(0).Rows(0).Item(1).ToString <> "", dsGrilla.Tables(0).Rows(0).Item(1).ToString, 0)

                grdItems.CurrentRow.Cells(ColumnasDelGridItems.empleado).Value = dsGrilla.Tables(0).Rows(0).Item(2).ToString

            Catch ex As Exception
                Dim errMessage As String = ""
                Dim tempException As Exception = ex

                While (Not tempException Is Nothing)
                    errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                    tempException = tempException.InnerException
                End While

            End Try

        End If

        If e.ColumnIndex = ColumnasDelGridItems.Proveedor Then

            Try

                dsGrilla = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT id, Nombre FROM Proveedores WHERE id = '" & grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value & "'")
                dsGrilla.Dispose()

                grdItems.CurrentRow.Cells(ColumnasDelGridItems.idProv).Value = dsGrilla.Tables(0).Rows(0).Item(0).ToString

                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Proveedor).Value = dsGrilla.Tables(0).Rows(0).Item(1).ToString

            Catch ex As Exception
                Dim errMessage As String = ""
                Dim tempException As Exception = ex

                While (Not tempException Is Nothing)
                    errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                    tempException = tempException.InnerException
                End While

            End Try

        End If


    End Sub

    Private Sub grdItems_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles grdItems.DataError
        e.ThrowException = False
    End Sub

    Private Sub GrdItems_CmbFocus(ByVal sender As Object, e As EventArgs)
        Dim tb As ComboBox = TryCast(sender, ComboBox)
        tb.DroppedDown = True
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True


        Try
            If grdItems.Focused = True And grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.fechaMantenimiento Or grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.fechaProxMantenimiento Then


                dtpGrilla.Location = grdItems.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False).Location
                dtpGrilla.Visible = True

                If grdItems.CurrentCell.Value <> DBNull.Value Then
                    dtpGrilla.Value = CDate(grdItems.CurrentCell.Value)
                Else
                    dtpGrilla.Value = Date.Today
                End If

            Else

                dtpGrilla.Visible = False

            End If

        Catch ex As Exception

        End Try

       
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub NoValidar(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim caracter As Char = e.KeyChar
            Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString.ToUpper)
            e.Handled = False ' dejar escribir cualquier cosa
        Catch ex As Exception
            Dim obj2 As System.Windows.Forms.DataGridViewComboBoxEditingControl = CType(sender, DataGridViewComboBoxEditingControl)
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString.ToUpper)
            e.Handled = False ' dejar escribir cualquier cosa
        End Try
    End Sub

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
    End Sub

    Private Sub LlenarGridItems()

        If txtID.Text = "" Then
            SQL = "exec [spMaquinasHerramientas_Det_Select_By_IdMaquinaHerramienta] @IdMaquinaHerramienta = 0"
        Else
            SQL = "exec [spMaquinasHerramientas_Det_Select_By_IdMaquinaHerramienta] @IdMaquinaHerramienta = " & CType(txtID.Text, Long)
        End If

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.idMH).Visible = False

        grdItems.Columns(ColumnasDelGridItems.fechaMantenimiento).Width = 98

        grdItems.Columns(ColumnasDelGridItems.descripcion).Width = 400

        grdItems.Columns(ColumnasDelGridItems.idEmp).Visible = False
        grdItems.Columns(ColumnasDelGridItems.idProv).Visible = False

        grdItems.Columns(ColumnasDelGridItems.precioHora).Width = 60
        grdItems.Columns(ColumnasDelGridItems.precioHora).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.precioHora).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.horas).Width = 50
        grdItems.Columns(ColumnasDelGridItems.horas).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.fechaProxMantenimiento).Width = 98

        grdItems.Columns(ColumnasDelGridItems.costoMO).Width = 60
        grdItems.Columns(ColumnasDelGridItems.costoMO).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.costoMO).ReadOnly = True

        grdItems.Columns(ColumnasDelGridItems.costoInsumo).Width = 60
        grdItems.Columns(ColumnasDelGridItems.costoInsumo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.subtotalcostomto).Width = 80
        grdItems.Columns(ColumnasDelGridItems.subtotalcostomto).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridItems.subtotalcostomto).ReadOnly = True


        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = True
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
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

        '' Borramos la columna enlazada...
        'grdItems.Columns.Remove("Nombre Prov")
        grdItems.Columns.RemoveAt(ColumnasDelGridItems.empleado)

        ''Agregamos la nueva columna Nota y Motivo....
        AgregarComboBoxColumns("Empleado", ColumnasDelGridItems.empleado, 170)

        grdItems.Columns.RemoveAt(ColumnasDelGridItems.Proveedor)


        AgregarComboBoxColumns("Proveedor", ColumnasDelGridItems.Proveedor, 170)


        'Volver la fuente de datos a como estaba...
        If chkEliminados.Checked = True Then
            SQL = "exec spMaquinasHerramientas_Select_All @Eliminado = 1"
        Else
            SQL = "exec spMaquinasHerramientas_Select_All @Eliminado = 0"
        End If
    End Sub

    Private Sub AgregarComboBoxColumns(ByVal Campo As String, ByVal Indice As Integer, ByVal tamano As Integer)
        Dim comboboxColumn As DataGridViewComboBoxColumn

        Dim bs As New BindingSource

        comboboxColumn = CrearComboBoxColumn(Campo, tamano)

        Dim ds_Notas As Data.DataSet

        Try
            If Campo = "Empleado" Then

                ds_Notas = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT * FROM (SELECT 0 as ID, 0 as PrecioHora, '' as Empleado UNION " + _
                                        " SELECT id, preciohora, (apellido + ' ' + nombre ) as Empleado FROM Empleados Where eliminado = 0) tt Order by empleado ")

                With comboboxColumn
                    .DataSource = ds_Notas.Tables(0)
                    .DisplayMember = "Empleado"
                    .ValueMember = "id"
                End With
            Else

                ds_Notas = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT 0 AS ID, '' AS Nombre UNION SELECT id, Nombre FROM Proveedores WHERE eliminado = 0 ORDER BY Nombre")

                With comboboxColumn
                    .DataSource = ds_Notas.Tables(0)
                    .DisplayMember = "nOMBRE"
                    .ValueMember = "id"
                End With

            End If

            comboboxColumn.HeaderText = Campo
            grdItems.Columns.Insert(Indice, comboboxColumn)

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor valide el siguiente mensaje de error: {0}" _
               + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
               "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Function CrearComboBoxColumn(ByVal Campo As String, ByVal tamano As Integer) As DataGridViewComboBoxColumn
        Dim column As New DataGridViewComboBoxColumn()

        With column
            .DataPropertyName = Campo '"Nota"
            .HeaderText = Campo '"Nota"
            .DropDownWidth = 160
            .Width = tamano
            .MaxDropDownItems = 10
            .FlatStyle = FlatStyle.Flat
            .AutoComplete = True
            .ReadOnly = False
        End With
        Return column

    End Function

    Private Sub GetDatasetItems()
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringMOTO_HOLLMAN)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            ds_2 = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, SQL)
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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try

    End Sub

#End Region

End Class