Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmProveedores

    Dim bolpoliticas As Boolean

#Region "Componentes Formulario"

    Private Sub frmProveedores_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Proveedor Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Proveedor?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmProveedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        configurarform()
        asignarTags()

        LlenarProvincia()
        LlenarLocalidad()
        LlenarcmbTipoDocumento_App(Me.cmbDocTipo, ConnStringSEI)

        SQL = "exec spProveedores_Select_All @Eliminado = 0"
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        grd.Columns(3).Visible = False
        grd.Columns(14).Visible = False
        grd.Columns(15).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(17).Visible = False
        grd.Columns(18).Visible = False
        grd.Columns(19).Visible = False

        txtNOMBRE.Focus()

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
            SQL = "exec spProveedores_Select_All @Eliminado = 1"
        Else
            SQL = "exec spProveedores_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
        End If
    End Sub

    Private Sub chkControlGasto_CheckedChanged(sender As Object, e As EventArgs) Handles chkControlGasto.CheckedChanged
        txtMontoControl.Enabled = chkControlGasto.Checked

        If chkControlGasto.Checked = False Then
            txtMontoControl.Text = "0"
        Else
            txtMontoControl.Focus()
        End If

    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If ALTA Then
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        LlenarProvincia()
        LlenarLocalidad()
        txtBonificacion.Text = 0
        txtGanancia.Text = 0
        cmbDocTipo.SelectedIndex = 0
        chkControlGasto.Checked = False
        txtMontoControl.Text = "0"
        txtNOMBRE.Focus()
        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para generar registros nuevos.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If bolModo = False Then
            If MessageBox.Show("Está seguro que desea modificar el Proveedor seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If chkControlGasto.Checked = True And txtMontoControl.Text = "" Then
            chkControlGasto.Checked = False
            txtMontoControl.Text = "0"
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    'If ALTA Then
                    res = AgregarRegistro()
                    Select Case res
                        Case -10
                            Util.MsgStatus(Status1, "Está intentando ingresar un CUIT que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Está intentando ingresar un CUIT que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap, True)
                            txtCuit.Focus()
                        Case -2
                            Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case Else
                            Util.MsgStatus(Status1, "Se insertó correctamente el proveedor.", My.Resources.Resources.ok.ToBitmap)
                            bolModo = False
                            PrepararBotones()
                            btnActualizar_Click(sender, e)
                    End Select
                    'Else
                    '    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
                    'End If
                Else
                    'If MODIFICA Then
                    res = ActualizarRegistro()
                    Select Case res
                        Case -10
                            Util.MsgStatus(Status1, "Está intentando ingresar un CUIT que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Está intentando ingresar un CUIT que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap, True)
                            txtCuit.Focus()
                        Case -3
                            Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo Código.", My.Resources.stop_error.ToBitmap)
                        Case -2
                            Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                            bolModo = False
                            PrepararBotones()
                            btnActualizar_Click(sender, e)
                    End Select
                    '    Else
                    '    Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
                    'End If
                End If
            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer

        If MessageBox.Show("Está seguro que desea eliminar el Proveedor seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        'If BAJA_FISICA Then
        Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        res = EliminarRegistro()
        Select Case res
            Case -20
                Util.MsgStatus(Status1, "El Proveedor seleccionado no se puede eliminar porque tiene movimientos en Gastos.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El Proveedor seleccionado no se puede eliminar porque tiene movimientos en Gastos.", My.Resources.stop_error.ToBitmap, True)
            Case -30
                Util.MsgStatus(Status1, "El Proveedor seleccionado no se puede eliminar porque tiene movimientos en Recepciones.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El Proveedor seleccionado no se puede eliminar porque tiene movimientos en Recepciones.", My.Resources.stop_error.ToBitmap, True)
            Case -40
                Util.MsgStatus(Status1, "El Proveedor seleccionado no se puede eliminar porque tiene movimientos en Ordenes de Compra.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El Proveedor seleccionado no se puede eliminar porque tiene movimientos en Ordenes de Compra.", My.Resources.stop_error.ToBitmap, True)
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
                End If
        End Select
        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim param As New frmParametros
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim rpt As New frmReportes

        nbreformreportes = "Proveedores"

        param.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", Cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then
            codigo = param.ObtenerParametros(0)

            rpt.Proveedores_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing
            Cnn = Nothing
        End If

    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente el Proveedor: " & grd.CurrentRow.Cells(2).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Proveedores SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            SQL = "exec spProveedores_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "El Proveedor se activó correctamente.", My.Resources.ok.ToBitmap)

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

    Private Function AgregarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_tipodocumento As New SqlClient.SqlParameter
            param_tipodocumento.ParameterName = "@tipodoc"
            param_tipodocumento.SqlDbType = SqlDbType.Int
            param_tipodocumento.Value = CInt(cmbDocTipo.SelectedValue)
            param_tipodocumento.Direction = ParameterDirection.Input

            Dim param_cuit As New SqlClient.SqlParameter
            param_cuit.ParameterName = "@cuit"
            param_cuit.SqlDbType = SqlDbType.BigInt
            param_cuit.Value = txtCuit.Text
            param_cuit.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 30
            param_nombre.Value = txtNOMBRE.Text
            param_nombre.Direction = ParameterDirection.Input

            Dim param_direccion As New SqlClient.SqlParameter
            param_direccion.ParameterName = "@direccion"
            param_direccion.SqlDbType = SqlDbType.VarChar
            param_direccion.Size = 50
            param_direccion.Value = txtDIRECCION.Text
            param_direccion.Direction = ParameterDirection.Input

            Dim param_codpostal As New SqlClient.SqlParameter
            param_codpostal.ParameterName = "@codpostal"
            param_codpostal.SqlDbType = SqlDbType.VarChar
            param_codpostal.Size = 10
            param_codpostal.Value = txtCODPOSTAL.Text
            param_codpostal.Direction = ParameterDirection.Input

            Dim param_localidad As New SqlClient.SqlParameter
            param_localidad.ParameterName = "@localidad"
            param_localidad.SqlDbType = SqlDbType.VarChar
            param_localidad.Size = 50
            param_localidad.Value = cmbLocalidad.Text
            param_localidad.Direction = ParameterDirection.Input

            Dim param_provincia As New SqlClient.SqlParameter
            param_provincia.ParameterName = "@provincia"
            param_provincia.SqlDbType = SqlDbType.VarChar
            param_provincia.Size = 50
            param_provincia.Value = cmbProvincia.Text
            param_provincia.Direction = ParameterDirection.Input

            Dim param_telefono As New SqlClient.SqlParameter
            param_telefono.ParameterName = "@telefono"
            param_telefono.SqlDbType = SqlDbType.VarChar
            param_telefono.Size = 30
            param_telefono.Value = txtTELEFONO.Text
            param_telefono.Direction = ParameterDirection.Input

            Dim param_fax As New SqlClient.SqlParameter
            param_fax.ParameterName = "@fax"
            param_fax.SqlDbType = SqlDbType.VarChar
            param_fax.Size = 30
            param_fax.Value = txtFAX.Text
            param_fax.Direction = ParameterDirection.Input

            Dim param_email As New SqlClient.SqlParameter
            param_email.ParameterName = "@email"
            param_email.SqlDbType = SqlDbType.VarChar
            param_email.Size = 100
            param_email.Value = txtEMAIL.Text
            param_email.Direction = ParameterDirection.Input

            Dim param_contacto As New SqlClient.SqlParameter
            param_contacto.ParameterName = "@contacto"
            param_contacto.SqlDbType = SqlDbType.VarChar
            param_contacto.Size = 100
            param_contacto.Value = txtCONTACTO.Text
            param_contacto.Direction = ParameterDirection.Input

            Dim param_bonificacion As New SqlClient.SqlParameter
            param_bonificacion.ParameterName = "@bonificacion"
            param_bonificacion.SqlDbType = SqlDbType.Decimal
            param_bonificacion.Size = 18
            param_bonificacion.Precision = 2
            param_bonificacion.Value = IIf(txtBonificacion.Text = "", 0, FormatNumber(CDbl(txtBonificacion.Text), 2))
            param_bonificacion.Direction = ParameterDirection.Input

            Dim param_ganancia As New SqlClient.SqlParameter
            param_ganancia.ParameterName = "@ganancia"
            param_ganancia.SqlDbType = SqlDbType.Decimal
            param_ganancia.Size = 18
            param_ganancia.Precision = 2
            param_ganancia.Value = IIf(txtGanancia.Text = "", 0, FormatNumber(CDbl(txtGanancia.Text), 2))
            param_ganancia.Direction = ParameterDirection.Input

            Dim param_servicio As New SqlClient.SqlParameter
            param_servicio.ParameterName = "@deservicio"
            param_servicio.SqlDbType = SqlDbType.Bit
            param_servicio.Value = chkServicio.Checked
            param_servicio.Direction = ParameterDirection.Input

            Dim param_flete As New SqlClient.SqlParameter
            param_flete.ParameterName = "@flete"
            param_flete.SqlDbType = SqlDbType.Bit
            param_flete.Value = chkFlete.Checked
            param_flete.Direction = ParameterDirection.Input

            Dim param_ControlarGasto As New SqlClient.SqlParameter
            param_ControlarGasto.ParameterName = "@ControlarGasto"
            param_ControlarGasto.SqlDbType = SqlDbType.Bit
            param_ControlarGasto.Value = chkControlGasto.Checked
            param_ControlarGasto.Direction = ParameterDirection.Input

            Dim param_MontoControl As New SqlClient.SqlParameter
            param_MontoControl.ParameterName = "@MontoControl"
            param_MontoControl.SqlDbType = SqlDbType.Decimal
            param_MontoControl.Size = 18
            param_MontoControl.Precision = 2
            param_MontoControl.Value = IIf(txtMontoControl.Text = "", 0, FormatNumber(CDbl(txtMontoControl.Text), 2))
            param_MontoControl.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spProveedores_Insert", param_id, _
                                          param_tipodocumento, param_cuit, param_nombre, param_direccion, param_codpostal, param_localidad, _
                                          param_provincia, param_telefono, param_fax, param_email, param_contacto, _
                                          param_bonificacion, param_ganancia, param_servicio, param_flete,
                                          param_ControlarGasto, param_MontoControl, param_useradd, param_res)

                txtID.Text = param_id.Value
                res = param_res.Value


                If res >= 1 Then
                    Util.AgregarGrilla(grd, Me, Permitir)
                    bolModo = False
                    PrepararBotones()
                End If

                AgregarRegistro = res


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

            If ex.Message.ToString.Contains("UNIQUE KEY") Then
                AgregarRegistro = -10
            Else
                MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
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
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CType(txtID.Text, Long)
            param_id.Direction = ParameterDirection.Input

            Dim param_tipodocumento As New SqlClient.SqlParameter
            param_tipodocumento.ParameterName = "@tipodoc"
            param_tipodocumento.SqlDbType = SqlDbType.Int
            param_tipodocumento.Value = CInt(cmbDocTipo.SelectedValue)
            param_tipodocumento.Direction = ParameterDirection.Input

            Dim param_cuit As New SqlClient.SqlParameter
            param_cuit.ParameterName = "@cuit"
            param_cuit.SqlDbType = SqlDbType.BigInt
            param_cuit.Value = txtCuit.Text
            param_cuit.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 30
            param_nombre.Value = txtNOMBRE.Text
            param_nombre.Direction = ParameterDirection.Input

            Dim param_direccion As New SqlClient.SqlParameter
            param_direccion.ParameterName = "@direccion"
            param_direccion.SqlDbType = SqlDbType.VarChar
            param_direccion.Size = 50
            param_direccion.Value = txtDIRECCION.Text
            param_direccion.Direction = ParameterDirection.Input

            Dim param_codpostal As New SqlClient.SqlParameter
            param_codpostal.ParameterName = "@codpostal"
            param_codpostal.SqlDbType = SqlDbType.VarChar
            param_codpostal.Size = 10
            param_codpostal.Value = txtCODPOSTAL.Text
            param_codpostal.Direction = ParameterDirection.Input

            Dim param_localidad As New SqlClient.SqlParameter
            param_localidad.ParameterName = "@localidad"
            param_localidad.SqlDbType = SqlDbType.VarChar
            param_localidad.Size = 50
            param_localidad.Value = cmbLocalidad.Text
            param_localidad.Direction = ParameterDirection.Input

            Dim param_provincia As New SqlClient.SqlParameter
            param_provincia.ParameterName = "@provincia"
            param_provincia.SqlDbType = SqlDbType.VarChar
            param_provincia.Size = 50
            param_provincia.Value = cmbProvincia.Text
            param_provincia.Direction = ParameterDirection.Input

            Dim param_telefono As New SqlClient.SqlParameter
            param_telefono.ParameterName = "@telefono"
            param_telefono.SqlDbType = SqlDbType.VarChar
            param_telefono.Size = 30
            param_telefono.Value = txtTELEFONO.Text
            param_telefono.Direction = ParameterDirection.Input

            Dim param_fax As New SqlClient.SqlParameter
            param_fax.ParameterName = "@fax"
            param_fax.SqlDbType = SqlDbType.VarChar
            param_fax.Size = 30
            param_fax.Value = txtFAX.Text
            param_fax.Direction = ParameterDirection.Input

            Dim param_email As New SqlClient.SqlParameter
            param_email.ParameterName = "@email"
            param_email.SqlDbType = SqlDbType.VarChar
            param_email.Size = 100
            param_email.Value = txtEMAIL.Text
            param_email.Direction = ParameterDirection.Input

            Dim param_contacto As New SqlClient.SqlParameter
            param_contacto.ParameterName = "@contacto"
            param_contacto.SqlDbType = SqlDbType.VarChar
            param_contacto.Size = 100
            param_contacto.Value = txtCONTACTO.Text
            param_contacto.Direction = ParameterDirection.Input

            Dim param_bonificacion As New SqlClient.SqlParameter
            param_bonificacion.ParameterName = "@bonificacion"
            param_bonificacion.SqlDbType = SqlDbType.Decimal
            param_bonificacion.Precision = 18
            param_bonificacion.Scale = 2
            param_bonificacion.Value = IIf(txtBonificacion.Text = "", 0, FormatNumber(CDbl(txtBonificacion.Text), 2))
            param_bonificacion.Direction = ParameterDirection.Input

            Dim param_ganancia As New SqlClient.SqlParameter
            param_ganancia.ParameterName = "@ganancia"
            param_ganancia.SqlDbType = SqlDbType.Decimal
            param_ganancia.Precision = 18
            param_ganancia.Scale = 2
            param_ganancia.Value = IIf(txtGanancia.Text = "", 0, FormatNumber(CDbl(txtGanancia.Text), 2))
            param_ganancia.Direction = ParameterDirection.Input

            Dim param_servicio As New SqlClient.SqlParameter
            param_servicio.ParameterName = "@deservicio"
            param_servicio.SqlDbType = SqlDbType.Bit
            param_servicio.Value = chkServicio.Checked
            param_servicio.Direction = ParameterDirection.Input

            Dim param_flete As New SqlClient.SqlParameter
            param_flete.ParameterName = "@flete"
            param_flete.SqlDbType = SqlDbType.Bit
            param_flete.Value = chkFlete.Checked
            param_flete.Direction = ParameterDirection.Input

            Dim param_ControlarGasto As New SqlClient.SqlParameter
            param_ControlarGasto.ParameterName = "@ControlarGasto"
            param_ControlarGasto.SqlDbType = SqlDbType.Bit
            param_ControlarGasto.Value = chkControlGasto.Checked
            param_ControlarGasto.Direction = ParameterDirection.Input

            Dim param_MontoControl As New SqlClient.SqlParameter
            param_MontoControl.ParameterName = "@MontoControl"
            param_MontoControl.SqlDbType = SqlDbType.Decimal
            param_MontoControl.Precision = 18
            param_MontoControl.Scale = 2
            If chkControlGasto.Checked = True Then
                param_MontoControl.Value = IIf(txtMontoControl.Text = "", 0, FormatNumber(CDbl(txtMontoControl.Text), 2))
            Else
                param_MontoControl.Value = 0
            End If
            param_MontoControl.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spProveedores_Update", param_id, _
                                          param_tipodocumento, param_cuit, param_nombre, param_direccion, param_codpostal, _
                                          param_localidad, param_provincia, param_telefono, param_fax, param_email, _
                                          param_contacto, param_bonificacion, param_ganancia, param_servicio, _
                                          param_ControlarGasto, param_MontoControl, param_flete, param_userupd, param_res)

                res = param_res.Value

                ActualizarRegistro = res
                'If res > 0 Then ActualizarGrilla(grd, Me)


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

            If ex.Message.ToString.Contains("UNIQUE KEY") Then
                ActualizarRegistro = -10
            Else
                MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
                  + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
                  "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing


        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spProveedores_Delete", param_id, param_userdel, param_res)
                res = param_res.Value

                If res > 0 Then Util.BorrarGrilla(grd)

                EliminarRegistro = res

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

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Proveedores"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 90))

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
        txtid.tag = "0"
        txtCODIGO.Tag = "1"
        txtNOMBRE.Tag = "2"
        cmbDocTipo.Tag = "4"
        txtCuit.Tag = "5"
        txtCONTACTO.Tag = "6"
        txtDIRECCION.Tag = "7"
        txtCODPOSTAL.Tag = "8"
        cmbLocalidad.Tag = "9"
        cmbProvincia.Tag = "10"
        txtTELEFONO.Tag = "11"
        txtFAX.Tag = "12"
        txtEMAIL.Tag = "13"
        txtGanancia.Tag = "14"
        txtBonificacion.Tag = "15"
        chkServicio.Tag = "16"
        chkFlete.Tag = "17"
        chkControlGasto.Tag = "18"
        txtMontoControl.Tag = "19"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Select Case cmbDocTipo.Text
            Case "CUIL"
                If txtCuit.Text.Length <> 11 Then
                    MsgBox("El nro de CUIL no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    txtCuit.Focus()
                    Exit Sub
                End If
            Case "CUIT"
                If txtCuit.Text.Length <> 11 Then
                    MsgBox("El nro de CUIT no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    txtCuit.Focus()
                    Exit Sub
                End If
            Case "DNI"
                If txtCuit.Text.Length < 7 Or txtCuit.Text.Length > 8 Then
                    MsgBox("El nro de DNI no cumple con el requisito de 8 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    txtCuit.Focus()
                    Exit Sub
                End If
        End Select

        If txtCuit.Text = "" Then
            txtCuit.Text = "0"
        End If


        If txtBonificacion.Text = "" Then
            txtBonificacion.Text = "0"
        End If

        If txtGanancia.Text = "" Then
            txtGanancia.Text = "0"
        End If

        If txtMontoControl.Text = "" Then
            txtMontoControl.Text = "0"
        End If

        'controlo que el mail contenga un @
        If txtEMAIL.Text.Length > 0 Then
            If Not txtEMAIL.Text.Contains("@") Then
                '("El email ingresado no contiene @. Por favor, controle el dato.")
                MsgBox("El email ingresado no contiene @. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                txtEMAIL.Focus()
                Exit Sub
            End If
        End If

        bolpoliticas = True

    End Sub

    Private Sub LlenarLocalidad()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT localidad FROM Proveedores ORDER BY Localidad")
            ds.Dispose()

            With cmbLocalidad
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "localidad"
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

    Private Sub LlenarProvincia()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT provincia FROM Proveedores ORDER BY provincia")
            ds.Dispose()

            With cmbProvincia
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "provincia"
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

End Class