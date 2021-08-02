Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmJornadas
    Dim bolpoliticas As Boolean

    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    Dim editando_celda As Boolean

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing


    Dim permitir_evento_CellChanged As Boolean

    Enum ColumnasDelGridItems
        Id = 0
        Codigo = 1
        NombreSubrubro = 2
        Eliminado = 3
    End Enum

#Region "Procedimientos Formularios"

    Private Sub frmFamilias_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la jornada que está realizando. ¿Está seguro que desea continuar sin grabar y hacer una nueva jornada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    'Private Sub frmRubros_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
    '    If permitir_evento_CellChanged Then
    '        If txtID.Text <> "" Then
    '            LlenarGridItems()
    '        End If
    '    End If
    'End Sub

    Private Sub frmJornadas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()
        asignarTags()

        SQL = "exec spJornadas_Select_All @Eliminado = 0"

        LlenarGrilla()

        Permitir = True

        CargarCajas()
        PrepararBotones()

        If bolModo = True Then
            btnNuevo_Click(sender, e)
        Else

            LlenarGridItems()
        End If

        grd.Focus()

        'grd.Columns(4).Visible = False

        permitir_evento_CellChanged = True

        btnEliminar.Enabled = False

    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEliminados.CheckedChanged

        btnNuevo.Enabled = Not chkEliminados.Checked
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spJornadas_Select_All @Eliminado = 1"
        Else
            SQL = "exec spJornadas_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        LlenarGridItems()

        If grd.RowCount = 0 Then
            BtnActivar.Enabled = False
        Else
            BtnActivar.Enabled = chkEliminados.Checked
        End If

    End Sub

#End Region

#Region "Componentes Formulario"

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            LlenarGridItems()
        End If
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        'PrepararGridItems()

        txtCODIGO.Focus()
        grdItems.Rows.Clear()
        grdItems.Columns(ColumnasDelGridItems.Eliminado).Visible = False

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If bolModo = False Then
            If MessageBox.Show("Desea modificar la información de la Jornada: " & grd.CurrentRow.Cells(2).Value.ToString & "?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro()
                Select Case res
                    Case Is <= 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo guardar la información de la Jornada (encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        res = AgregarRegistroItems()
                        Select Case res
                            Case Is <= 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo guardar la información de la Jornada (detalle).", My.Resources.Resources.stop_error.ToBitmap)
                            Case Else
                                Util.MsgStatus(Status1, "Se guardó la información de la jornada.", My.Resources.Resources.ok.ToBitmap)
                                bolModo = False
                                Cerrar_Tran()

                                SQL = "exec spJornadas_Select_All @Eliminado = 0"

                                btnActualizar_Click(sender, e)
                        End Select
                End Select
            End If
        End If

        If Not conn_del_form Is Nothing Then
            CType(conn_del_form, IDisposable).Dispose()
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer
        'If BAJA_FISICA Then

        If MessageBox.Show("Desea eliminar la jornada: " & grd.CurrentRow.Cells(2).Value.ToString & "?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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
                End If

                SQL = "exec spJornadas_Select_All @Eliminado = 0"

                btnActualizar_Click(sender, e)

        End Select
        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim param As New frmParametros
        Dim Cnn As New SqlConnection(ConnStringACER)
        Dim codigo As String

        Dim rpt As New frmReportes

        nbreformreportes = "Rubros y Subrubros"

        param.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", Cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then
            codigo = param.ObtenerParametros(0)

            rpt.Rubros_Subrubros_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing
            Cnn = Nothing
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If txtID.Text <> "" Then
            LlenarGridItems()
        End If
    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente la Jornada: " & grd.CurrentRow.Cells(2).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Jornadas SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            SQL = "exec spJornadas_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "La Jornada se activó correctamente.", My.Resources.ok.ToBitmap)

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

    Private Sub btnAgregarDia_Click(sender As Object, e As EventArgs) Handles btnAgregarDia.Click

        If cmbDia.Text.ToString = "" Then
            MsgBox("Debe seleccionar un día para agregar a la jornada", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To grdItems.Rows.Count - 1
            If grdItems.Rows(i).Cells(0).Value = cmbDia.Text.ToString Then
                MsgBox("El día seleccionado ya existe", MsgBoxStyle.Critical, "Atención")
                Exit Sub
            End If
        Next

        grdItems.Rows.Add(cmbDia.Text.ToString, dtpHoraIngreso.Value.ToShortTimeString, dtpHoraEgreso.Value.ToShortTimeString)

        cmbDia.Focus()

    End Sub


#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Jornadas"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 90))
        '65-7-65
        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 15 - GroupBox1.Size.Height - GroupBox1.Location.Y - 90)
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
        txtCODIGO.Tag = "1"
        txtNOMBRE.Tag = "2"
        txtTolerancia.Tag = "3"
    End Sub

    Private Sub Verificar_Datos()

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

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If grdItems.Rows.Count > 0 Then
            bolpoliticas = True
        Else
            Util.MsgStatus(Status1, "La jornada no tiene días asignados.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "La jornada no tiene días asignados.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If
    End Sub

    Private Function fila_vacia(ByVal i) As Boolean
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.Codigo).Value Is Nothing) Then
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

    Private Sub LlenarGridItems()
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim dt As New DataTable
            Dim sqltxt2 As String

            sqltxt2 = "SELECT Dia, Hora_ingreso, Hora_Egreso FROM Jornadas_det WHERE idjornada = " & IIf(txtID.Text = "", 0, txtID.Text) 'grd.CurrentRow.Cells(0).Value

            Dim cmd As New SqlCommand(sqltxt2, conn_del_form)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            grdItems.Rows.Clear()

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)("dia").ToString(), dt.Rows(i)("Hora_Ingreso").ToString(), dt.Rows(i)("Hora_Egreso").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not conn_del_form Is Nothing Then
                CType(conn_del_form, IDisposable).Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function AgregarActualizar_Registro() As Integer

        Try
            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo

            If Abrir_Tran(conn_del_form) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            If bolModo = True Then
                param_id.Value = DBNull.Value
            Else
                param_id.Value = txtID.Text
            End If
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 200
            param_nombre.Value = txtNOMBRE.Text
            param_nombre.Direction = ParameterDirection.Input

            Dim param_tolerancia As New SqlClient.SqlParameter
            param_tolerancia.ParameterName = "@tolerancia"
            param_tolerancia.SqlDbType = SqlDbType.Int
            param_tolerancia.Value = txtTolerancia.Text
            param_tolerancia.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                If bolModo = True Then
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spJornadas_Insert", param_id, _
                                            param_nombre, param_tolerancia, param_res)

                    txtID.Text = param_id.Value

                Else
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spJornadas_Update", param_id, _
                                            param_nombre, param_tolerancia, param_res)
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

    Private Function EliminarRegistro() As Integer

        Try
            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
            param_id.Value = CType(txtID.Text, Long)
            param_id.Direction = ParameterDirection.Input

            'Dim param_userdel As New SqlClient.SqlParameter
            'param_userdel.ParameterName = "@userdel"
            'param_userdel.SqlDbType = SqlDbType.Int
            'param_userdel.Value = UserID
            'param_userdel.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.Output

            Try

                SqlHelper.ExecuteNonQuery(conn_del_form, CommandType.StoredProcedure, "spJornadas_Delete", param_id, param_res)

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
            If Not conn_del_form Is Nothing Then
                CType(conn_del_form, IDisposable).Dispose()
            End If
        End Try
    End Function

    Private Function AgregarRegistroItems() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try
            Try

                If bolModo = False Then

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@idJornada"
                    param_id.SqlDbType = SqlDbType.BigInt
                    param_id.Value = txtID.Text
                    param_id.Direction = ParameterDirection.Input

                    Dim param_res1 As New SqlClient.SqlParameter
                    param_res1.ParameterName = "@res"
                    param_res1.SqlDbType = SqlDbType.Int
                    param_res1.Value = DBNull.Value
                    param_res1.Direction = ParameterDirection.InputOutput

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spJornadas_Det_Delete", param_id, param_res1)

                    If param_res1.Value < 0 Then
                        AgregarRegistroItems = -5
                        Exit Function
                    End If

                End If


                For i = 0 To grdItems.Rows.Count - 1

                    Dim param_idJornada As New SqlClient.SqlParameter
                    param_idJornada.ParameterName = "@idJornada"
                    param_idJornada.SqlDbType = SqlDbType.BigInt
                    param_idJornada.Value = txtID.Text
                    param_idJornada.Direction = ParameterDirection.Input

                    Dim param_dia As New SqlClient.SqlParameter
                    param_dia.ParameterName = "@Dia"
                    param_dia.SqlDbType = SqlDbType.VarChar
                    param_dia.Size = 20
                    param_dia.Value = grdItems.Rows(i).Cells(0).Value
                    param_dia.Direction = ParameterDirection.Input

                    Dim param_HoraIngreso As New SqlClient.SqlParameter
                    param_HoraIngreso.ParameterName = "@HoraIngreso"
                    param_HoraIngreso.SqlDbType = SqlDbType.Time
                    param_HoraIngreso.Value = grdItems.Rows(i).Cells(1).Value
                    param_HoraIngreso.Direction = ParameterDirection.Input

                    Dim param_HoraSalida As New SqlClient.SqlParameter
                    param_HoraSalida.ParameterName = "@HoraSalida"
                    param_HoraSalida.SqlDbType = SqlDbType.Time
                    param_HoraSalida.Value = grdItems.Rows(i).Cells(2).Value
                    param_HoraSalida.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spJornadas_Det_Insert", _
                                                  param_idJornada, param_dia, param_HoraIngreso, param_HoraSalida, param_res)

                        res = param_res.Value

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

#End Region

#Region "GridItems"

    Private Sub grdItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellContentClick
        If e.ColumnIndex = 3 Then
            If MessageBox.Show("Está por eliminar el día seleccionado. Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItems.Rows.Remove(grdItems.CurrentRow)
            End If
        End If
    End Sub

#End Region

    
End Class