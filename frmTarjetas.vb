
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmTarjetas

    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient

#Region "Procedimientos Formularios"

    Private Sub frmUnidades_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                btnNuevo_Click(sender, e)
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmUnidades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        configurarform()
        asignarTags()

        SQL = "exec spTarjetas_Select_All @Eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminados.CheckedChanged
        btnNuevo.Enabled = Not chkEliminados.Checked
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spTarjetas_Select_All @Eliminado = 1"
        Else
            SQL = "exec spTarjetas_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
        End If
    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Tarjetas"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
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
        txtCuotas.Tag = "3"
        txtPorcentaje.Tag = "4"
        txtNota.Tag = "5"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        bolpoliticas = True

    End Sub


#End Region

#Region "Funciones"

    Private Function AgregarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

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
                param_codigo.Value = txtCODIGO.Text.ToUpper
                param_codigo.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 50
                param_nombre.Value = txtNOMBRE.Text.ToUpper
                param_nombre.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 50
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_cuotas As New SqlClient.SqlParameter
                param_cuotas.ParameterName = "@cuotas"
                param_cuotas.SqlDbType = SqlDbType.Decimal
                param_cuotas.Precision = 18
                param_cuotas.Scale = 2
                param_cuotas.Value = txtCuotas.Text
                param_cuotas.Direction = ParameterDirection.Input

                Dim param_porcentaje As New SqlClient.SqlParameter
                param_porcentaje.ParameterName = "@porcenrecar"
                param_porcentaje.SqlDbType = SqlDbType.Decimal
                param_porcentaje.Precision = 18
                param_porcentaje.Scale = 2
                param_porcentaje.Value = txtPorcentaje.Text
                param_porcentaje.Direction = ParameterDirection.Input

                'Dim param_useradd As New SqlClient.SqlParameter
                'param_useradd.ParameterName = "@useradd"
                'param_useradd.SqlDbType = SqlDbType.Int
                'param_useradd.Value = UserID
                'param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spTarjetas_Insert", param_id, param_codigo, param_nombre, param_cuotas, param_porcentaje, param_nota, param_res)
                    'txtID.Text = param_id.Value
                    ' res = param_res.Value
                    'If res = 1 Then
                    '    Util.AgregarGrilla(grd, Me, Permitir)
                    '    bolModo = False
                    '    PrepararBotones()
                    'End If

                    'AgregarRegistro = res

                    txtID.Text = param_id.Value
                    AgregarRegistro = param_res.Value

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci?n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont?ctese con MercedesIt a trav?s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function ActualizarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexi?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = txtCODIGO.Text.ToUpper
                param_codigo.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 50
                param_nombre.Value = txtNOMBRE.Text.ToUpper
                param_nombre.Direction = ParameterDirection.Input

                Dim param_cuotas As New SqlClient.SqlParameter
                param_cuotas.ParameterName = "@cuotas"
                param_cuotas.SqlDbType = SqlDbType.Decimal
                param_cuotas.Precision = 18
                param_cuotas.Scale = 2
                param_cuotas.Value = txtCuotas.Text
                param_cuotas.Direction = ParameterDirection.Input

                Dim param_porcentaje As New SqlClient.SqlParameter
                param_porcentaje.ParameterName = "@porcenrecar"
                param_porcentaje.SqlDbType = SqlDbType.Decimal
                param_porcentaje.Precision = 18
                param_porcentaje.Scale = 2
                param_porcentaje.Value = txtPorcentaje.Text
                param_porcentaje.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 50
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                'Dim param_userupd As New SqlClient.SqlParameter
                'param_userupd.ParameterName = "@userupd"
                'param_userupd.SqlDbType = SqlDbType.Int
                'param_userupd.Value = UserID
                'param_userupd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spTarjetas_Update", param_id, param_codigo, param_nombre, param_cuotas, param_porcentaje, param_nota, param_res)
                    'res = param_res.Value


                    'PrepararBotones()
                    'ActualizarRegistro = res
                    'If res > 0 Then ActualizarGrilla(grd, Me)

                    ActualizarRegistro = param_res.Value

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci?n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont?ctese con MercedesIt a trav?s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function EliminarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexi?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_id.Value = CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

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

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spTarjetas_Delete", param_id, param_userdel, param_res)
                    res = param_res.Value

                    If res > 0 Then
                        'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                        '    Try
                        '        Dim sqlstring As String

                        '        sqlstring = "UPDATE [dbo].[Unidades] SET [Eliminado] = 1 WHERE Codigo = '" & txtCODIGO.Text & "'"
                        '        tranWEB.Sql_Set(sqlstring)

                        '    Catch ex As Exception
                        '        'MsgBox(ex.Message)
                        '        MsgBox("No se puede actualizar en la Web la lista de Rubros. Ejecute el bot?n sincronizar para actualizar el servidor WEB.")
                        '    End Try
                        'End If
                        Util.BorrarGrilla(grd)
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci?n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont?ctese con MercedesIt a trav?s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If ALTA Then
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        txtCODIGO.Focus()
        'Else
        '    Util.MsgStatus(Status1, "No tiene permiso para generar registros nuevos.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    'If ALTA Then
                    res = AgregarRegistro()
                    Select Case res
                        Case -2
                            Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                    End Select
                    'Else
                    '    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
                    'End If
                Else
                    'If MODIFICA Then
                    res = ActualizarRegistro()
                    Select Case res
                        Case -3
                            Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo C?digo.", My.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case -2
                            Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                    End Select
                    'Else
                    '    Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
                    'End If
                End If

                'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                '    Try
                '        Dim sqlstring As String

                '        If bolModo = True Then
                '            sqlstring = "INSERT INTO [dbo].[Unidades] (ID, [Codigo],[Nombre],[Eliminado])" & _
                '                        " values ( " & txtID.Text & ", '" & txtCODIGO.Text.ToUpper & "', '" & txtNOMBRE.Text.ToUpper & "' , 0 )"

                '        Else
                '            sqlstring = "UPDATE [dbo].[Unidades] SET [Codigo] = '" & txtCODIGO.Text.ToUpper & " ', " & _
                '                        " [Nombre] = '" & txtNOMBRE.Text.ToUpper & "' " & _
                '                        " WHERE ID = " & txtID.Text
                '        End If

                '        tranWEB.Sql_Set(sqlstring)

                '    Catch ex As Exception
                '        'MsgBox(ex.Message)
                '        MsgBox("No se puede actualizar en la Web la lista de Marcas. Ejecute el bot?n sincronizar para actualizar el servidor WEB.")
                '    End Try
                'End If

                bolModo = False
                PrepararBotones()
                MDIPrincipal.NoActualizarBase = False
                btnActualizar_Click(sender, e)


            End If
        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer
        'If BAJA_FISICA Then

        If MessageBox.Show("Est? seguro que desea eliminar la Tarjeta seleccionada?", "Atenci?n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        res = EliminarRegistro()
        Select Case res
            Case -2
                Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                Exit Sub
            Case -1
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                Exit Sub
            Case 0
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                Exit Sub
            Case Else
                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                If Me.grd.RowCount = 0 Then
                    bolModo = True
                    PrepararBotones()
                    Util.LimpiarTextBox(Me.Controls)
                End If


        End Select
        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'nbreformreportes = "Unidades"
        Dim ReporteMaestroUnidades As New frmReportes()
        ' ReporteMaestroUnidades.MostrarMaestroUnidades(ReporteMaestroUnidades)
    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Est? por activar nuevamente la Tarjeta: " & grd.CurrentRow.Cells(2).Value.ToString & ". Desea continuar?", "Atenci?n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            'llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Tarjetas SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()


            'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '    Try
            '        Dim sqlstring As String

            '        sqlstring = "UPDATE [dbo].[Unidades] SET [Eliminado] = 0 WHERE Codigo = '" & grd.CurrentRow.Cells(1).Value & "'"
            '        tranWEB.Sql_Set(sqlstring)

            '    Catch ex As Exception
            '        'MsgBox(ex.Message)
            '        MsgBox("No se puede Activa en la Web la Unidad seleccionada. Ejecute el bot?n sincronizar para actualizar el servidor WEB.")
            '    End Try
            'End If

            SQL = "exec spTarjetas_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "La Tarjeta se activ? correctamente.", My.Resources.ok.ToBitmap)

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci?n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont?ctese con MercedesIt a trav?s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci?n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Componentes Formulario"

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles txtID.KeyPress, txtCODIGO.KeyPress, txtNOMBRE.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region


End Class

