Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmRequirimientos

    Dim bolpoliticas As Boolean
    Dim llenandoCombo As Boolean = False
    Dim Band As Integer

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
        Band = 0
        configurarform()
        asignarTags()

        LlenarcmbClientes_App(cmbClientes, ConnStringSEI, llenandoCombo)

        SQL = "exec spRequerimientos_Select_ALL @Eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        Band = 1

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
            SQL = "exec spRequerimientos_Select_ALL @Eliminado = 1"
        Else
            SQL = "exec spRequerimientos_Select_ALL @Eliminado = 0"
        End If

        LlenarGrilla()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
        End If
    End Sub

    Private Sub cmbCliente_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedValueChanged
        If llenandoCombo = False And Band = 1 Then
            If cmbClientes.Text <> "" Then
                txtIdCliente.Text = cmbClientes.SelectedValue
                LlenarcmbPresupuestos()
                cmbPresupuestos_SelectedValueChanged(sender, e)
            End If
        End If
    End Sub

    'Private Sub cmbPresupuestos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPresupuestos.SelectedIndexChanged
    '    If llenandoCombo = False Then

    '    End If
    'End Sub

    Private Sub cmbPresupuestos_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPresupuestos.SelectedValueChanged
        If llenandoCombo = False Then
            If cmbClientes.Text <> "" Then
                txtIdPresupuesto.Text = cmbPresupuestos.SelectedValue
                If chkPresupuestos.Checked = True Then
                    BuscarCostos_Presupuestos()
                End If
            End If
        End If
    End Sub

    Private Sub txtHsCotizacion_LostFocus(sender As Object, e As EventArgs) Handles txtHsCotizacion.LostFocus, txtMontoCotizacion.LostFocus, _
        txtMontoMO.LostFocus, txtMontoSeg.LostFocus, txtMontoSeg.LostFocus, txtOtrosGastos.LostFocus

        If txtHsCotizacion.Text = "" Then
            txtHsCotizacion.Text = "0"
        End If

        If txtMontoCotizacion.Text = "" Then
            txtMontoCotizacion.Text = "0"
        End If

        If txtHsMO.Text = "" Then
            txtHsMO.Text = "0"
        End If

        If txtMontoMO.Text = "" Then
            txtMontoMO.Text = "0"
        End If

        If txtMontoMAT.Text = "" Then
            txtMontoMAT.Text = "0"
        End If

        If txtMontoSeg.Text = "" Then
            txtMontoSeg.Text = "0"
        End If

        If txtOtrosGastos.Text = "" Then
            txtOtrosGastos.Text = "0"
        End If

        lblTotalGastos.Text = CDbl(txtMontoCotizacion.Text) + CDbl(txtMontoMAT.Text) + CDbl(txtMontoMO.Text) + CDbl(txtMontoSeg.Text)

    End Sub

    Private Sub txtNroReq_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNroReq.KeyDown
        If e.KeyCode = Keys.Enter Then
            LlenarcmbPresupuestos()
        End If
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        Band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")

        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)

        Band = 1

        cmbClientes.SelectedIndex = 0
        LlenarcmbPresupuestos()

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If bolModo = False Then
            If MessageBox.Show("Está seguro que desea modificar el Requerimiento seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo insertar / actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    Case 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        Util.MsgStatus(Status1, "Se insertó correctamente el Requerimiento.", My.Resources.Resources.ok.ToBitmap)
                        bolModo = False
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                End Select
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

            SQL = "exec spRequerimientos_Select_ALL @Eliminado = 1"

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

    Private Function AgregarActualizar_Registro() As Integer
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
            If bolModo = True Then
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput
            Else
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input
            End If
            
            Dim param_nroreq As New SqlClient.SqlParameter
            param_nroreq.ParameterName = "@NroReq"
            param_nroreq.SqlDbType = SqlDbType.VarChar
            param_nroreq.Size = 100
            param_nroreq.Value = txtNroReq.Text
            param_nroreq.Direction = ParameterDirection.Input

            Dim param_idpresupuesto As New SqlClient.SqlParameter
            param_idpresupuesto.ParameterName = "@idpresupuesto"
            param_idpresupuesto.SqlDbType = SqlDbType.BigInt
            param_idpresupuesto.Value = IIf(txtIdPresupuesto.Text = "", 0, txtIdPresupuesto.Text)
            param_idpresupuesto.Direction = ParameterDirection.Input

            Dim param_idcliente As New SqlClient.SqlParameter
            param_idcliente.ParameterName = "@idcliente"
            param_idcliente.SqlDbType = SqlDbType.BigInt
            param_idcliente.Value = txtIdCliente.Text
            param_idcliente.Direction = ParameterDirection.Input

            Dim param_fechasolicitud As New SqlClient.SqlParameter
            param_fechasolicitud.ParameterName = "@FechaPedido"
            param_fechasolicitud.SqlDbType = SqlDbType.DateTime
            param_fechasolicitud.Value = dtpFechaSolicitud.Value
            param_fechasolicitud.Direction = ParameterDirection.Input

            Dim param_fechaEntrega As New SqlClient.SqlParameter
            param_fechaEntrega.ParameterName = "@FechaEntrega"
            param_fechaEntrega.SqlDbType = SqlDbType.DateTime
            param_fechaEntrega.Value = dtpFechaEntrega.Value
            param_fechaEntrega.Direction = ParameterDirection.Input

            Dim param_contacto As New SqlClient.SqlParameter
            param_contacto.ParameterName = "@contacto"
            param_contacto.SqlDbType = SqlDbType.VarChar
            param_contacto.Size = 300
            param_contacto.Value = txtSolicitante.Text
            param_contacto.Direction = ParameterDirection.Input

            Dim param_MontoFinal As New SqlClient.SqlParameter
            param_MontoFinal.ParameterName = "@MontoFinal"
            param_MontoFinal.SqlDbType = SqlDbType.Decimal
            param_MontoFinal.Size = 18
            param_MontoFinal.Precision = 2
            param_MontoFinal.Value = IIf(lblTotalGastos.Text = "", 0, CDbl(lblTotalGastos.Text))
            param_MontoFinal.Direction = ParameterDirection.Input

            Dim param_personal As New SqlClient.SqlParameter
            param_personal.ParameterName = "@personal"
            param_personal.SqlDbType = SqlDbType.Int
            param_personal.Value = txtPersonal.Value
            param_personal.Direction = ParameterDirection.Input

            Dim param_HsCampo As New SqlClient.SqlParameter
            param_HsCampo.ParameterName = "@HsCampo"
            param_HsCampo.SqlDbType = SqlDbType.Decimal
            param_HsCampo.Size = 18
            param_HsCampo.Precision = 2
            param_HsCampo.Value = IIf(txtHsMO.Text = "", 0, CDbl(txtHsMO.Text))
            param_HsCampo.Direction = ParameterDirection.Input

            Dim param_HsCotizacion As New SqlClient.SqlParameter
            param_HsCotizacion.ParameterName = "@HsCotizacion"
            param_HsCotizacion.SqlDbType = SqlDbType.Decimal
            param_HsCotizacion.Size = 18
            param_HsCotizacion.Precision = 2
            param_HsCotizacion.Value = IIf(txtHsCotizacion.Text = "", 0, CDbl(txtHsCotizacion.Text))
            param_HsCotizacion.Direction = ParameterDirection.Input

            Dim param_MontoMO As New SqlClient.SqlParameter
            param_MontoMO.ParameterName = "@MontoManoObra"
            param_MontoMO.SqlDbType = SqlDbType.Decimal
            param_MontoMO.Size = 18
            param_MontoMO.Precision = 2
            param_MontoMO.Value = IIf(txtMontoMO.Text = "", 0, CDbl(txtMontoMO.Text))
            param_MontoMO.Direction = ParameterDirection.Input

            Dim param_MontoCotizacion As New SqlClient.SqlParameter
            param_MontoCotizacion.ParameterName = "@MontoCotizacion"
            param_MontoCotizacion.SqlDbType = SqlDbType.Decimal
            param_MontoCotizacion.Size = 18
            param_MontoCotizacion.Precision = 2
            param_MontoCotizacion.Value = IIf(txtMontoCotizacion.Text = "", 0, CDbl(txtMontoCotizacion.Text))
            param_MontoCotizacion.Direction = ParameterDirection.Input

            Dim param_MontoMateriales As New SqlClient.SqlParameter
            param_MontoMateriales.ParameterName = "@MontoMateriales"
            param_MontoMateriales.SqlDbType = SqlDbType.Decimal
            param_MontoMateriales.Size = 18
            param_MontoMateriales.Precision = 2
            param_MontoMateriales.Value = IIf(txtMontoMAT.Text = "", 0, CDbl(txtMontoMAT.Text))
            param_MontoMateriales.Direction = ParameterDirection.Input

            Dim param_OtrosGastos As New SqlClient.SqlParameter
            param_OtrosGastos.ParameterName = "@OtrosGastos"
            param_OtrosGastos.SqlDbType = SqlDbType.Decimal
            param_OtrosGastos.Size = 18
            param_OtrosGastos.Precision = 2
            param_OtrosGastos.Value = IIf(txtOtrosGastos.Text = "", 0, CDbl(txtOtrosGastos.Text))
            param_OtrosGastos.Direction = ParameterDirection.Input

            Dim param_MontoSeguridad As New SqlClient.SqlParameter
            param_MontoSeguridad.ParameterName = "@MontoSeguridad"
            param_MontoSeguridad.SqlDbType = SqlDbType.Decimal
            param_MontoSeguridad.Size = 18
            param_MontoSeguridad.Precision = 2
            param_MontoSeguridad.Value = IIf(txtMontoSeg.Text = "", 0, CDbl(txtMontoSeg.Text))
            param_MontoSeguridad.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spRequerimientos_Insert", param_id, _
                                          param_nroreq, param_idcliente, param_idpresupuesto, param_fechaEntrega, param_fechasolicitud, _
                                          param_contacto, param_MontoFinal, param_personal, param_HsCampo, param_HsCotizacion, _
                                          param_MontoMO, param_MontoCotizacion, param_MontoMateriales, param_OtrosGastos, _
                                          param_MontoSeguridad, param_useradd, param_res)

                    txtID.Text = param_id.Value
                    res = param_res.Value

                Else

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spRequerimientos_Update", param_id, _
                                          param_nroreq, param_idcliente, param_idpresupuesto, param_fechaEntrega, param_fechasolicitud, _
                                          param_contacto, param_MontoFinal, param_personal, param_HsCampo, param_HsCotizacion, _
                                          param_MontoMO, param_MontoCotizacion, param_MontoMateriales, param_OtrosGastos, _
                                          param_MontoSeguridad, param_useradd, param_res)

                    res = param_res.Value

                End If

                AgregarActualizar_Registro = res

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
        Me.Text = "Requerimientos"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 5)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = 0 ' (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 3 - GroupBox1.Size.Height - GroupBox1.Location.Y - 62) '65)

    End Sub

    Private Sub asignarTags()
        txtid.tag = "0"
        txtCODIGO.Tag = "1"
        cmbClientes.Tag = "3"
        txtIdCliente.Tag = "2"
        dtpFechaSolicitud.Tag = "4"
        dtpFechaEntrega.Tag = "5"
        txtSolicitante.Tag = "6"
        lblTotalGastos.Tag = "7"
        txtPersonal.Tag = "8"
        txtHsMO.Tag = "9"
        txtMontoMO.Tag = "10"
        txtHsCotizacion.Tag = "11"
        txtMontoCotizacion.Tag = "12"
        txtMontoMAT.Tag = "13"
        txtOtrosGastos.Tag = "14"
        txtMontoSeg.Tag = "15"
        cmbPresupuestos.Tag = "16"
        txtIdPresupuesto.Tag = "17"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If txtHsCotizacion.Text = "" Then txtHsCotizacion.Text = "0"
        If txtMontoCotizacion.Text = "" Then txtMontoCotizacion.Text = "0"
        If txtHsMO.Text = "" Then txtHsMO.Text = "0"
        If txtMontoMO.Text = "" Then txtMontoMO.Text = "0"
        If lblMatPresup.Text = "" Then lblMatPresup.Text = "0"
        If lblMOPresup.Text = "" Then lblMOPresup.Text = "0"
        If txtMontoMAT.Text = "" Then txtMontoMAT.Text = "0"
        If txtMontoSeg.Text = "" Then txtMontoSeg.Text = "0"
        If txtOtrosGastos.Text = "" Then txtOtrosGastos.Text = "0"
        If lblCantDias.Text = "" Then lblCantDias.Text = "0"

        bolpoliticas = True

    End Sub

    Private Sub LlenarcmbPresupuestos()
        Dim ds As Data.DataSet

        llenandoCombo = True

        Try
            If txtNroReq.Text = "" Then
                ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Id, (p.codigo + ' - ' + p.nombrereferencia) as Nombre FROM Presupuestos p WHERE p.eliminado = 0 AND idCliente = " & txtIdCliente.Text)
                ds.Dispose()

                If ds.Tables(0).Rows.Count > 0 Then
                    With cmbPresupuestos
                        .DataSource = ds.Tables(0).DefaultView
                        .DisplayMember = "Nombre"
                        .ValueMember = "Id"
                    End With
                    'Else
                    '    ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Idcliente FROM Presupuestos p WHERE p.eliminado = 0 and NroReq = '" & txtNroReq.Text & "'")
                    '    ds.Dispose()

                    '    If ds.Tables(0).Rows.Count > 0 Then
                    '        cmbClientes.SelectedValue = ds.Tables(0).Rows(0).Item(0).ToString
                    '    End If

                End If
            Else

            End If

            'ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Id, (p.codigo + ' - ' + p.nombrereferencia) as Nombre FROM Presupuestos p WHERE p.eliminado = 0 and NroReq = '" & txtNroReq.Text & "' AND idCliente = " & txtIdCliente.Text)
            'ds.Dispose()

            'If ds.Tables(0).Rows.Count > 0 Then
            '    With cmbPresupuestos
            '        .DataSource = ds.Tables(0).DefaultView
            '        .DisplayMember = "Nombre"
            '        .ValueMember = "Id"
            '    End With
            'Else
            '    ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Idcliente FROM Presupuestos p WHERE p.eliminado = 0 and NroReq = '" & txtNroReq.Text & "'")
            '    ds.Dispose()

            '    If ds.Tables(0).Rows.Count > 0 Then
            '        cmbClientes.SelectedValue = ds.Tables(0).Rows(0).Item(0).ToString
            '    End If

            'End If

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

        llenandoCombo = False

    End Sub

    Private Sub BuscarCostos_Presupuestos()
        Dim ds As Data.DataSet

        llenandoCombo = True

        Try

            ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select totaldo, m.codigo, ofertacomercial " & _
                                        " from presupuestos p " & _
                                        " JOIN Monedas m ON m.id = p.idmonedapres " & _
                                        " where p.id = " & cmbPresupuestos.SelectedValue)
            ds.Dispose()

            If ds.Tables(0).Rows.Count > 0 Then
                lblTotalPresup.Text = ds.Tables(0).Rows(0).Item(0)
                lblMoneda.Text = ds.Tables(0).Rows(0).Item(1)

                If CBool(ds.Tables(0).Rows(0).Item(2)) Then
                    ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select total from Presupuestos_OfertasComerciales where idpresupuesto = " & cmbPresupuestos.SelectedValue)
                    ds.Dispose()

                    If ds.Tables(0).Rows.Count > 0 Then
                        lblMOPresup.Text = ds.Tables(0).Rows(0).Item(0)
                    Else
                        lblMOPresup.Text = "0"
                    End If
                Else
                    lblMOPresup.Text = "0"
                End If

                ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select CONVERT(DECIMAL(18,2), sum(preciolista * qty)) as Total, IdPresupuesto" & _
                                              " from presupuestos_det pd " & _
                                              " where idpresupuesto = " & cmbPresupuestos.SelectedValue & " group by idpresupuesto")
                ds.Dispose()

                If ds.Tables(0).Rows.Count > 0 Then
                    lblMatPresup.Text = ds.Tables(0).Rows(0).Item(0)
                Else
                    lblMatPresup.Text = "0"
                End If
            End If

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

        llenandoCombo = False

    End Sub

#End Region

    Private Sub chkPresupuestos_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresupuestos.CheckedChanged
        cmbPresupuestos.Enabled = chkPresupuestos.Checked
        If chkPresupuestos.Checked = True Then
            cmbPresupuestos_SelectedValueChanged(sender, e)
        Else
            lblMoneda.Text = "Pe"
            lblMatPresup.Text = "0"
            lblMOPresup.Text = "0"
        End If
    End Sub

End Class