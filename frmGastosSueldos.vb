Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmGastosSueldos

    Dim bolpoliticas As Boolean
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing


#Region "Componentes Formulario"

    Private Sub frmFacturacion_Manual_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                btnNuevo_Click(sender, e)
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmFacturacion_Manual_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        configurarform()
        asignarTags()

        Me.LlenarcmbEmpleado()
        Me.LlenarcmbOtrosGastos()

        SQL = "exec spFacturacionManual_Select_All"

        LlenarGrilla()

        Permitir = True

        CargarCajas()

        If grd.Rows.Count = 0 Then
            btnNuevo_Click(sender, e)
        End If

        grd.Columns(3).Visible = False
        'grd.Columns(21).Visible = False

        PrepararBotones()

    End Sub

    Private Sub txtSubtotal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotal.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True

            'If txtIVA.Text <> "" And txtSubtotal.Text <> "" Then
            '    txtTotalAnticipo.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
            '    txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
            'Else
            '    txtTotal.Text = FormatNumber(CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)), 2)
            'End If

            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotal.LostFocus
        'If txtIVA.Text <> "" And txtSubtotal.Text <> "" Then
        '    txtTotalAnticipo.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
        '    txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
        'Else
        '    txtTotal.Text = FormatNumber(CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)), 2)
        'End If
    End Sub

    Private Sub txtIva_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True

            'If txtSubtotal.Text <> "" And txtIVA.Text <> "" Then
            '    txtTotalAnticipo.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
            '    txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
            'Else
            '    If txtIVA.Text = "" Then
            '        txtTotalAnticipo.Text = "0"
            '    End If
            '    txtTotal.Text = IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)
            'End If

            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub txtIVA_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        'If txtSubtotal.Text <> "" And txtIVA.Text <> "" Then
        '    txtTotalAnticipo.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
        '    txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
        'Else
        '    If txtIVA.Text = "" Then
        '        txtTotalAnticipo.Text = "0"
        '    End If
        '    txtTotal.Text = IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)
        'End If
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)

        dtpFECHA.Value = Date.Today

        cmbPeriodo.SelectedIndex = 0
        cmbEmpleado.SelectedIndex = 0

        txtCODIGO.Focus()
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
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                            bolModo = False
                            btnActualizar_Click(sender, e)
                    End Select
                    'Else
                    '    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
                    'End If
                Else
                    'If MODIFICA Then
                    If MessageBox.Show("Está seguro que desea modificar la información de la factura seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If grd.CurrentRow.Cells(20).Value = True Or (grd.CurrentRow.Cells(20).Value = False And grd.CurrentRow.Cells(16).Value > 0) Then
                            Util.MsgStatus(Status1, "No se puede modificar esa factura porque está dentro de un proceso de pago.", My.Resources.stop_error.ToBitmap, True)
                            Exit Sub
                        End If

                        res = ActualizarRegistro()
                        Select Case res
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
                                btnActualizar_Click(sender, e)
                        End Select
                        '    Else
                        '    Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
                        'End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer
        'If BAJA_FISICA Then
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
                End If
        End Select
        'Else
        'Util.MsgStatus(Status1, "No tiene permiso para eliminar registros.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'nbreformreportes = "Factura"

        'Dim paramConsumos As New frmParametros
        'Dim cnn As New SqlConnection(ConnStringSEI)
        'Dim codigo As String
        'Dim Rpt As New frmReportes

        'paramConsumos.AgregarParametros("N° de Mov:", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)

        'paramConsumos.ShowDialog()

        'If cerroparametrosconaceptar = True Then
        '    codigo = paramConsumos.ObtenerParametros(0)

        '    Rpt.MostrarReporte_Factura(codigo, Rpt, IIf(chkFacturaAnulada.Checked = False, 0, 1))

        '    cerroparametrosconaceptar = False
        '    paramConsumos = Nothing
        '    cnn = Nothing
        'End If
    End Sub

    Private Sub btnAnular_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim res As Integer
        Dim nc As Boolean = False
        Dim NvaFactura As String = ""

        If CBool(grd.CurrentRow.Cells(20).Value) = True Then
            MsgBox("La factura seleccionada está asociada al Pago Nro " & grd.CurrentRow.Cells(21).Value.ToString & " y no podrá ser anulada", MsgBoxStyle.Information, "Factura Cancelada")
            Exit Sub
        End If


            Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
            res = AnularFisico_Registro(False, True, nc, NvaFactura)
            Select Case res
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado) - 0.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No pudo realizarse la anulación (Encabezado) - 0.", My.Resources.Resources.stop_error.ToBitmap, True)
                Case Else
                    Cerrar_Tran()
                    bolModo = False
                    PrepararBotones()

                    btnActualizar_Click(sender, e)

                    Util.MsgStatus(Status1, "Se actualizó el estado de la factura.", My.Resources.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se actualizó el estado de la factura.", My.Resources.Resources.ok.ToBitmap, True, True)
                    'End Select
            End Select
            '
            ' cerrar la conexion si está abierta.
            '
            If Not conn_del_form Is Nothing Then
                CType(conn_del_form, IDisposable).Dispose()
            End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

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
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                param_codigo.SqlDbType = SqlDbType.BigInt
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechafactura"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbOtrosGastos.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = CDbl(txtTotalAnticipo.Text)
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
                param_total.Value = 0 'CDbl(txtTotal.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_condicionVta As New SqlClient.SqlParameter
                param_condicionVta.ParameterName = "@CondicionVta"
                param_condicionVta.SqlDbType = SqlDbType.VarChar
                param_condicionVta.Size = 20
                param_condicionVta.Value = cmbEmpleado.Text
                param_condicionVta.Direction = ParameterDirection.Input

                Dim param_condicionIVA As New SqlClient.SqlParameter
                param_condicionIVA.ParameterName = "@CondicionIVA"
                param_condicionIVA.SqlDbType = SqlDbType.VarChar
                param_condicionIVA.Size = 25
                param_condicionIVA.Value = cmbPeriodo.Text
                param_condicionIVA.Direction = ParameterDirection.Input

                Dim param_remitos As New SqlClient.SqlParameter
                param_remitos.ParameterName = "@remitos"
                param_remitos.SqlDbType = SqlDbType.VarChar
                param_remitos.Size = 300
                param_remitos.Value = ""
                param_remitos.Direction = ParameterDirection.Input

                Dim param_remitos1 As New SqlClient.SqlParameter
                param_remitos1.ParameterName = "@remitos1"
                param_remitos1.SqlDbType = SqlDbType.VarChar
                param_remitos1.Size = 300
                param_remitos1.Value = ""
                param_remitos1.Direction = ParameterDirection.Input

                Dim param_nrocomprobante As New SqlClient.SqlParameter
                param_nrocomprobante.ParameterName = "@nrocomprobante"
                param_nrocomprobante.SqlDbType = SqlDbType.VarChar
                param_nrocomprobante.Size = 50
                param_nrocomprobante.Value = 0 ' txtComprobante.Text
                param_nrocomprobante.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtNota.Text ', DBNull.Value, txtComprobante.Text)
                param_nota.Direction = ParameterDirection.Input

                Dim param_Manual As New SqlClient.SqlParameter
                param_Manual.ParameterName = "@Manual"
                param_Manual.SqlDbType = SqlDbType.Bit
                param_Manual.Value = True
                param_Manual.Direction = ParameterDirection.Input

                Dim param_FacturadaAnulada As New SqlClient.SqlParameter
                param_FacturadaAnulada.ParameterName = "@FacturaAnulada"
                param_FacturadaAnulada.SqlDbType = SqlDbType.Bit
                param_FacturadaAnulada.Value = False
                param_FacturadaAnulada.Direction = ParameterDirection.Input

                Dim param_idFacturaAnulada As New SqlClient.SqlParameter
                param_idFacturaAnulada.ParameterName = "@idfacturaAnulada"
                param_idFacturaAnulada.SqlDbType = SqlDbType.BigInt
                param_idFacturaAnulada.Value = DBNull.Value
                param_idFacturaAnulada.Direction = ParameterDirection.Input

                Dim param_TextoSobre As New SqlClient.SqlParameter
                param_TextoSobre.ParameterName = "@TextoSobre"
                param_TextoSobre.SqlDbType = SqlDbType.VarChar
                param_TextoSobre.Size = 200
                param_TextoSobre.Value = ""
                param_TextoSobre.Direction = ParameterDirection.Input

                Dim param_MasLineas As New SqlClient.SqlParameter
                param_MasLineas.ParameterName = "@MasLineas"
                param_MasLineas.SqlDbType = SqlDbType.Bit
                param_MasLineas.Value = False
                param_MasLineas.Direction = ParameterDirection.Input

                Dim param_Linea1 As New SqlClient.SqlParameter
                param_Linea1.ParameterName = "@Linea1"
                param_Linea1.SqlDbType = SqlDbType.VarChar
                param_Linea1.Size = 200
                param_Linea1.Value = ""
                param_Linea1.Direction = ParameterDirection.Input

                Dim param_Linea2 As New SqlClient.SqlParameter
                param_Linea2.ParameterName = "@Linea2"
                param_Linea2.SqlDbType = SqlDbType.VarChar
                param_Linea2.Size = 200
                param_Linea2.Value = ""
                param_Linea2.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spFacturacion_Insert", _
                                             param_id, param_codigo, param_fecha, param_idcliente, _
                                           param_remitos, param_remitos1, param_nrocomprobante, param_nota, _
                                            param_Manual, param_FacturadaAnulada, param_idFacturaAnulada, param_TextoSobre, _
                                            param_MasLineas, param_Linea1, param_Linea2, param_useradd, param_res)

                    txtID.Text = param_id.Value

                    txtCODIGO.Text = param_codigo.Value

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
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
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
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = txtCODIGO.Text
                param_codigo.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechafactura"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbOtrosGastos.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = 0 'txtIVA.Text
                param_iva.Direction = ParameterDirection.Input

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = CDbl(txtTotalAnticipo.Text)
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
                param_total.Value = 0 'CDbl(txtTotal.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_condicionVta As New SqlClient.SqlParameter
                param_condicionVta.ParameterName = "@CondicionVta"
                param_condicionVta.SqlDbType = SqlDbType.VarChar
                param_condicionVta.Size = 20
                param_condicionVta.Value = cmbEmpleado.Text
                param_condicionVta.Direction = ParameterDirection.Input

                Dim param_condicionIVA As New SqlClient.SqlParameter
                param_condicionIVA.ParameterName = "@CondicionIVA"
                param_condicionIVA.SqlDbType = SqlDbType.VarChar
                param_condicionIVA.Size = 25
                param_condicionIVA.Value = cmbPeriodo.Text
                param_condicionIVA.Direction = ParameterDirection.Input

                Dim param_remitos As New SqlClient.SqlParameter
                param_remitos.ParameterName = "@remitos"
                param_remitos.SqlDbType = SqlDbType.VarChar
                param_remitos.Size = 300
                param_remitos.Value = ""
                param_remitos.Direction = ParameterDirection.Input

                Dim param_remitos1 As New SqlClient.SqlParameter
                param_remitos1.ParameterName = "@remitos1"
                param_remitos1.SqlDbType = SqlDbType.VarChar
                param_remitos1.Size = 300
                param_remitos1.Value = ""
                param_remitos1.Direction = ParameterDirection.Input

                Dim param_nrocomprobante As New SqlClient.SqlParameter
                param_nrocomprobante.ParameterName = "@nrocomprobante"
                param_nrocomprobante.SqlDbType = SqlDbType.VarChar
                param_nrocomprobante.Size = 50
                param_nrocomprobante.Value = "" 'txtComprobante.Text
                param_nrocomprobante.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtNota.Text ', DBNull.Value, txtComprobante.Text)
                param_nota.Direction = ParameterDirection.Input

                Dim param_Manual As New SqlClient.SqlParameter
                param_Manual.ParameterName = "@Manual"
                param_Manual.SqlDbType = SqlDbType.Bit
                param_Manual.Value = True
                param_Manual.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@userupd"
                param_useradd.SqlDbType = SqlDbType.Int
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spFacturacionManual_Update", _
                                              param_id, param_codigo, param_fecha, param_idcliente, _
                                              param_iva, param_montoiva, param_subtotal, param_total, param_condicionVta, _
                                              param_condicionIVA, param_nrocomprobante, param_nota, _
                                              param_Manual, param_useradd, param_res)

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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spImpuestos_Delete", param_id, param_userdel, param_res)
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

    Private Function AnularFisico_Registro(ByVal logico As Boolean, ByVal fisico As Boolean, ByVal nc As Boolean, ByVal NvaFactura As String) As Integer
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

                Dim param_idFacturacion As New SqlClient.SqlParameter("@idFacturacion", SqlDbType.BigInt, ParameterDirection.Input)
                param_idFacturacion.Value = CType(txtID.Text, Long)
                param_idFacturacion.Direction = ParameterDirection.Input

                Dim param_userdel As New SqlClient.SqlParameter
                param_userdel.ParameterName = "@userdel"
                param_userdel.SqlDbType = SqlDbType.Int
                param_userdel.Value = UserID
                param_userdel.Direction = ParameterDirection.Input

                Dim param_nrofactura As New SqlClient.SqlParameter
                param_nrofactura.ParameterName = "@nrofactura"
                param_nrofactura.SqlDbType = SqlDbType.BigInt
                param_nrofactura.Value = IIf(NvaFactura = "", 0, NvaFactura)
                param_nrofactura.Direction = ParameterDirection.Input

                Dim param_FISICO As New SqlClient.SqlParameter
                param_FISICO.ParameterName = "@fisico"
                param_FISICO.SqlDbType = SqlDbType.Bit
                param_FISICO.Value = fisico
                param_FISICO.Direction = ParameterDirection.Input

                Dim param_logico As New SqlClient.SqlParameter
                param_logico.ParameterName = "@logico"
                param_logico.SqlDbType = SqlDbType.Bit
                param_logico.Value = logico
                param_logico.Direction = ParameterDirection.Input

                Dim param_NC As New SqlClient.SqlParameter
                param_NC.ParameterName = "@NotaCredito"
                param_NC.SqlDbType = SqlDbType.Bit
                param_NC.Value = nc
                param_NC.Direction = ParameterDirection.Input

                Dim param_Cliente As New SqlClient.SqlParameter
                param_Cliente.ParameterName = "@IdCliente"
                param_Cliente.SqlDbType = SqlDbType.BigInt
                param_Cliente.Value = cmbOtrosGastos.SelectedValue
                param_Cliente.Direction = ParameterDirection.Input

                Dim param_Monto As New SqlClient.SqlParameter
                param_Monto.ParameterName = "@Monto"
                param_Monto.SqlDbType = SqlDbType.Decimal
                param_Monto.Precision = 18
                param_Monto.Scale = 2
                param_Monto.Value = 0 'lblTotal.Text
                param_Monto.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtNota.Text ', DBNull.Value, txtComprobante.Text)
                param_nota.Direction = ParameterDirection.Input

                Dim param_MENSAJE As New SqlClient.SqlParameter
                param_MENSAJE.ParameterName = "@MENSAJE"
                param_MENSAJE.SqlDbType = SqlDbType.VarChar
                param_MENSAJE.Size = 300
                param_MENSAJE.Value = DBNull.Value
                param_MENSAJE.Direction = ParameterDirection.InputOutput


                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spFacturacion_Delete", _
                                            param_nrofactura, param_idFacturacion, param_userdel, param_FISICO, _
                                            param_logico, param_NC, param_Cliente, param_Monto, param_nota, _
                                            param_MENSAJE, param_res)

                    ' param_nota, param_res)

                    'MsgBox(param_MENSAJE.Value.ToString)


                    AnularFisico_Registro = param_res.Value

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

    'Private Function AnularFisico_RegistroItems(ByVal IdPres_Ges As Long) As Integer
    '    Dim res As Integer

    '    Try

    '        Dim param_id As New SqlClient.SqlParameter
    '        param_id.ParameterName = "@IdFacturacion"
    '        param_id.SqlDbType = SqlDbType.BigInt
    '        param_id.Value = txtID.Text
    '        param_id.Direction = ParameterDirection.Input

    '        Dim param_res As New SqlClient.SqlParameter
    '        param_res.ParameterName = "@res"
    '        param_res.SqlDbType = SqlDbType.Int
    '        param_res.Value = DBNull.Value
    '        param_res.Direction = ParameterDirection.InputOutput

    '        Try
    '            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spFacturacion_Remitos_Delete", _
    '                                      param_id, param_res)

    '            res = param_res.Value

    '            AnularFisico_RegistroItems = res

    '        Catch ex2 As Exception
    '            Throw ex2
    '        End Try

    '    Catch ex As Exception
    '        Dim errMessage As String = ""
    '        Dim tempException As Exception = ex

    '        While (Not tempException Is Nothing)
    '            errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '            tempException = tempException.InnerException
    '        End While

    '        MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '          + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '          "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function

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

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Facturación Manual"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        'Dim p As New Size(GroupBox1.Size.Width, 400)
        'Me.grd.Size = New Size(p)

        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
        Me.grd.Size = New Size(p)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If
    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbOtrosGastos.Tag = "4"
        cmbEmpleado.Tag = "8"
        cmbPeriodo.Tag = "9"
        txtSubtotal.Tag = "10"
        txtTotalAnticipo.Tag = "12"
        txtNota.Tag = "14"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        If txtSubtotal.Text = "0" Or txtSubtotal.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar un valor para la factura.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar un valor para la factura.", My.Resources.Resources.alert.ToBitmap, True)
            txtSubtotal.Focus()
            Exit Sub
        End If

        bolpoliticas = True

    End Sub

    Private Sub LlenarcmbOtrosGastos()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select id, codigo " & _
                                " from Impuestos where pantalla = 'Otros Gastos' eliminado=0 order by codigo")

            ds_Cli.Dispose()

            With Me.cmbOtrosGastos
                .DataSource = ds_Cli.Tables(0).DefaultView
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

    'Private Sub Imprimir()

    '    If txtIdFacturaAnulada.Text = "" Then
    '        nbreformreportes = "Factura"
    '    Else
    '        nbreformreportes = "Nota de Crédito"
    '    End If

    '    Dim cnn As New SqlConnection(ConnStringSEI)
    '    Dim Rpt As New frmReportes

    '    Rpt.MostrarReporte_Factura(txtCODIGO.Text, Rpt, IIf(txtIdFacturaAnulada.Text = "", 0, txtIdFacturaAnulada.Text))

    '    cnn = Nothing

    'End Sub

    Private Sub LlenarcmbEmpleado()
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, Descripcion FROM Comprobantes WHERE Habilitado = 1")
            ds.Dispose()

            With Me.cmbEmpleado
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                .SelectedIndex = 0
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