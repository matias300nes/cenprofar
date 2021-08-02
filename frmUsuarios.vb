Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
'Imports WinreportNet

Public Class frmUsuarios

    Private permiso_reset_pass As Boolean
    Dim ItemSelected As Integer

    Private Sub configurarform()

        Me.Text = "Usuarios"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)

    End Sub

    Private Sub frmUsuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()
        asignarTags()

        SQL = "exec sp_Usuarios_Select_All"
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        Me.grd.Columns.Item(4).Visible = False 'PASS
        'AsignarPermisos(UserID, "USUARIOS_RESET_PASS", permiso_reset_pass, modifica1, baja1, bajafisica1)

        'Dim ItemObject(1) As System.Object
        'ItemObject(0) = "Solo para retirar de pañol"
        'ItemObject(1) = "Usuario de los sistemas y retira de pañol"

        'cmbTipo.Items.AddRange(ItemObject)

        grd.Columns(6).Visible = False

        rbSoloRetira.Checked = CBool(grd.CurrentRow.Cells(6).Value)
        rbIngresaSistema.Checked = Not CBool(grd.CurrentRow.Cells(6).Value)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        txtNOMBRE.Tag = "2"
        txtAPELLIDO.Tag = "3"
        txtPASS.Tag = "4"
        txtPASS2.Tag = "4"
        txtEMAIL.Tag = "5"
        rbSoloRetira.Tag = "6"
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'nbreformreportes = "Maestro de Usuarios"
        'Dim reportemaestrodeUsuarios As New frmReportes ' frmReportes
        'reportemaestrodeUsuarios.MostrarMaestroDeUsuarios(reportemaestrodeUsuarios)
    End Sub

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

            Try

                'txtPASS.Text = Util.generarClaveSHA1(txtPASS.Text)

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = txtCODIGO.Text
                param_codigo.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 30
                param_nombre.Value = txtNOMBRE.Text
                param_nombre.Direction = ParameterDirection.Input

                Dim param_apellido As New SqlClient.SqlParameter
                param_apellido.ParameterName = "@apellido"
                param_apellido.SqlDbType = SqlDbType.VarChar
                param_apellido.Size = 30
                param_apellido.Value = txtAPELLIDO.Text
                param_apellido.Direction = ParameterDirection.Input

                Dim param_pass As New SqlClient.SqlParameter
                param_pass.ParameterName = "@password"
                param_pass.SqlDbType = SqlDbType.VarChar
                param_pass.Size = 50
                param_pass.Value = Util.generarClaveSHA1(txtPASS.Text) 'txtPASS.Text
                param_pass.Direction = ParameterDirection.Input

                Dim param_email As New SqlClient.SqlParameter
                param_email.ParameterName = "@email"
                param_email.SqlDbType = SqlDbType.VarChar
                param_email.Size = 50
                param_email.Value = txtEMAIL.Text
                param_email.Direction = ParameterDirection.Input

                Dim param_tipo As New SqlClient.SqlParameter
                param_tipo.ParameterName = "@tipo"
                param_tipo.SqlDbType = SqlDbType.Bit
                param_tipo.Value = rbSoloRetira.Checked
                param_tipo.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Usuarios_Insert", param_id, _
                                              param_codigo, param_nombre, param_apellido, param_pass, param_email, _
                                              param_tipo, param_useradd, param_res)

                    txtID.Text = param_id.Value
                    res = param_res.Value

                    If res = 1 Then Util.AgregarGrilla(grd, Me, Permitir)

                    bolModo = False
                    PrepararBotones()
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

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Private Function ActualizarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Dim nueva_pass As String
        Try

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try
                If txtPASS.Text <> txtPASS2.Text Then
                    nueva_pass = Util.generarClaveSHA1(txtPASS.Text)
                    txtPASS.Text = nueva_pass
                Else
                    nueva_pass = txtPASS.Text
                End If

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = txtCODIGO.Text
                param_codigo.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 30
                param_nombre.Value = txtNOMBRE.Text
                param_nombre.Direction = ParameterDirection.Input

                Dim param_apellido As New SqlClient.SqlParameter
                param_apellido.ParameterName = "@apellido"
                param_apellido.SqlDbType = SqlDbType.VarChar
                param_apellido.Size = 30
                param_apellido.Value = txtAPELLIDO.Text
                param_apellido.Direction = ParameterDirection.Input

                Dim param_pass As New SqlClient.SqlParameter
                param_pass.ParameterName = "@password"
                param_pass.SqlDbType = SqlDbType.VarChar
                param_pass.Size = 50
                param_pass.Value = nueva_pass 'txtPASS.Text
                param_pass.Direction = ParameterDirection.Input

                Dim param_email As New SqlClient.SqlParameter
                param_email.ParameterName = "@email"
                param_email.SqlDbType = SqlDbType.VarChar
                param_email.Size = 50
                param_email.Value = txtEMAIL.Text
                param_email.Direction = ParameterDirection.Input

                Dim param_tipo As New SqlClient.SqlParameter
                param_tipo.ParameterName = "@tipo"
                param_tipo.SqlDbType = SqlDbType.Bit
                param_tipo.Value = rbSoloRetira.Checked
                param_tipo.Direction = ParameterDirection.Input

                Dim param_userupd As New SqlClient.SqlParameter
                param_userupd.ParameterName = "@userupd"
                param_userupd.SqlDbType = SqlDbType.Int
                param_userupd.Value = UserID
                param_userupd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Usuarios_Update", param_id, _
                                              param_codigo, param_nombre, param_apellido, param_pass, param_email, _
                                              param_tipo, param_userupd, param_res)

                    res = param_res.Value

                    PrepararBotones()
                    ActualizarRegistro = res
                    If res > 0 Then ActualizarGrilla(grd, Me)


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

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try


            Try

                Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_id.Value = CType(txtid.Text, Long)
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

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Usuarios_Delete", param_id, param_userdel, param_res)
                    res = param_res.Value

                    If res > 0 Then Util.BorrarGrilla(grd)

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

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If BAJA_FISICA Then
            Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.indicator_white)
            If EliminarRegistro() Then
                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitMap)

                'ver si no hay mas datos en la grilla establecer bolmodo en verdadero..
                If Me.grd.RowCount = 0 Then
                    bolModo = True
                    PrepararBotones()
                    Util.LimpiarTextBox(Me.Controls)
                    txtCODIGO.Focus()
                End If


            Else
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitMap)
            End If
        Else
            Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitMap)
        End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer
        Dim usuario As Long
        Dim registro As Integer 'DataGridViewRow

        registro = grd.CurrentRow.Index
        usuario = grd.Rows(registro).Cells(0).Value

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.indicator_white)

        If ReglasNegocio() Then
            If bolModo Then
                res = AgregarRegistro()
                Select Case res
                    Case -2
                        Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.stop_error.ToBitmap)
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.stop_error.ToBitmap)
                    Case 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.stop_error.ToBitmap)
                    Case Else
                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.ok.ToBitmap)
                End Select
            Else
                'If MODIFICA Then
                'If permiso_reset_pass = False Then
                '    If usuario <> UserID Then  'solo deja modificar el registro del mismo usuario..
                '        Util.MsgStatus(Status1, "No Puede Modificar este Usuario.", My.Resources.stop_error.ToBitmap)
                '        Exit Sub
                '    End If
                'End If
                res = ActualizarRegistro()
                Select Case res
                    Case -2
                        Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.stop_error.ToBitmap)
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.stop_error.ToBitmap)
                    Case 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.stop_error.ToBitmap)
                    Case Else
                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.ok.ToBitmap)
                End Select
                'Else
                'Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
                'End If
            End If
        End If
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If ALTA Then
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        txtCODIGO.Focus()
        'Else
        '    Util.MsgStatus(Status1, "No tiene permiso para generar registros nuevos.", My.Resources.stop_error.ToBitMap)
        'End If
    End Sub

    Private Sub txtcodigo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtCODIGO.KeyPress, txtNOMBRE.KeyPress, txtAPELLIDO.KeyPress, txtPASS.KeyPress, txtEMAIL.KeyPress, txtID.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        txtPASS.Text = "123456"
        btnGuardar_Click(sender, e)

    End Sub

End Class