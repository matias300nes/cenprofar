Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles.compartida
Imports Utiles.Util
Imports Utiles

Public Class frmCheques

#Region "Declaracion de Variables"
    Dim dtnew As New System.Data.DataTable

    Dim RegistrosPorPagina As Integer = 20
    Dim ini As Integer = 0
    Dim fin As Integer = RegistrosPorPagina - 1
    Dim TotalPaginas As Integer
    Dim PaginaActual As Integer
    'Dim bolPaginar As Boolean = False

    Dim bolpoliticas As Boolean

#End Region

#Region "Formulario y Componentes"

    Private Sub frmAjustes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Cheque Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Cheque?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmEmpleados_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()
        asignarTags()

        SQL = "exec spCheques_Select_All @Eliminado = 0"

        LlenarComboBancos()

        LlenarGrilla()

        Dim i As Integer
        For i = 0 To grd.Columns.Count - 1 Step 1
            grd.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Next

        Permitir = True
        CargarCajas()

        grd.Columns(0).Visible = False
        grd.Columns(14).Visible = False

        PrepararBotones()

    End Sub

    Private Sub chkUtilizado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUtilizado.CheckedChanged
        txtUtilizado.Text = ""
        txtUtilizado.Enabled = chkUtilizado.Checked
        txtUtilizado.Focus()
    End Sub

    Private Sub txtNroCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroCheque.KeyPress, _
        txtMonto.KeyPress, txtObservaciones.KeyPress, txtPropietario.KeyPress, txtUtilizado.KeyPress, cmbBanco.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkAnulados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnulados.CheckedChanged
        btnGuardar.Enabled = Not chkAnulados.Checked
        btnEliminar.Enabled = Not chkAnulados.Checked
        btnNuevo.Enabled = Not chkAnulados.Checked
        btnCancelar.Enabled = Not chkAnulados.Checked

        If chkAnulados.Checked = True Then
            SQL = "exec spCheques_Select_All @Eliminado = 1"
        Else
            SQL = "exec spCheques_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

    End Sub

#End Region

#Region "Botones ABM y Navegacion"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer

        If chkChequesPropios.Checked = True Then
            If txtNroChequeInicio.Text <> "" And txtNroChequeFin.Text <> "" Then
                If CInt(txtNroChequeInicio.Text) < CInt(txtNroChequeFin.Text) Then
                    res = AgregarActualizarRegistro_ChequesPropios()
                    Select Case res
                        Case -1
                            Util.MsgStatus(Status1, "La chequera indicada no ha podido ser dada de alta", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case 0
                            Util.MsgStatus(Status1, "La chequera indicada no ha podido ser dada de alta", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case Else
                            'Util.MsgStatus(Status1, "La chequera indicada no ha podido ser dada de alta - Error Desconocido", My.Resources.Resources.ok.ToBitmap)
                            bolModo = False
                            chkChequesPropios.Checked = False
                            txtNroChequeInicio.Text = ""
                            txtNroChequeFin.Text = ""
                            PrepararBotones()
                            SQL = "exec spCheques_Select_All @Eliminado = 0"
                            LlenarGrilla()
                    End Select
                Else
                    MsgBox("El Nro de Inicio de Cheque no puede ser menor al Nro de Fin de los Cheques por cargar", MsgBoxStyle.Critical, "Control de Errores")
                End If
            Else
                MsgBox("Debe ingresar números para los cheque por cargar", MsgBoxStyle.Critical, "Control de Errores")
            End If
        Else

            If bolModo = False Then
                If CBool(grd.CurrentRow.Cells(1).Value) = True And grd.CurrentRow.Cells(10).Value <> "" Then
                    MsgBox("El cheque ya fue utilizado, por lo tanto, solo se actualizará el campo Observaciones Utilizado.")
                End If
            End If

            If ReglasNegocio() Then
                Verificar_Datos()
                If bolpoliticas Then
                    res = AgregarActualizarRegistro()
                    Select Case res
                        Case -2
                            Util.MsgStatus(Status1, "El Cheque no ha podido ser Modificado", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case -1
                            Util.MsgStatus(Status1, "El Cheque no ha podido ser Modificado, error en transacción", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case 0
                            Util.MsgStatus(Status1, "El Cheque no ha podido ser Modificado, error en transacción", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case Else
                            Util.MsgStatus(Status1, "El Cheque ha sido Modificado", My.Resources.Resources.ok.ToBitmap)

                            bolModo = False
                            PrepararBotones()
                            SQL = "exec spCheques_Select_All @Eliminado = 0"
                            LlenarGrilla()
                    End Select
                End If
            End If
        End If

    End Sub

    Public Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        chkChequesPropios.Checked = False
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        If grd.CurrentRow.Cells(1).Value = True Then
            MsgBox("El cheque seleccionado ya fue utilizado en una operación, no puede ser eliminado.", MsgBoxStyle.Critical, "Control de Errores")
            Exit Sub
        End If

        If MessageBox.Show("Está seguro que desea eliminar el Cheque?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        'If grd.CurrentRow.Cells(2).Value = True Then
        '    MsgBox("El cheque seleccionado pertenece a la Chequera de la empresa, no puede ser eliminado.", MsgBoxStyle.Critical, "Control de Errores")
        '    Exit Sub
        'End If

        Select Case EliminarRegistro()
            Case -1
                Util.MsgStatus(Status1, "El Cheque no ha podido ser Eliminado, error en transacción", My.Resources.Resources.stop_error.ToBitmap)
            Case Else
                Util.MsgStatus(Status1, "Operación exitosa", My.Resources.Resources.ok.ToBitmap)

                bolModo = False

                SQL = "exec spCheques_Select_All @Eliminado = 0"

                LlenarGrilla()
        End Select

    End Sub

    'Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.ClickEventArgs) Handles btnImprimir.Click
    '    Dim reporteEmpleados As New frmReportes
    '    reporteEmpleados.MostrarMaestroEmpleados(reporteEmpleados)
    'End Sub

#End Region

#Region "Funciones"

    Private Function AgregarActualizarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.Int
            If bolModo = False Then
                param_id.Value = CInt(txtID.Text)
            Else
                param_id.Value = DBNull.Value
            End If
            param_id.Direction = ParameterDirection.Input

            Dim param_nrocheque As New SqlClient.SqlParameter
            param_nrocheque.ParameterName = "@nrocheque"
            param_nrocheque.SqlDbType = SqlDbType.BigInt
            param_nrocheque.Value = txtNroCheque.Text
            param_nrocheque.Direction = ParameterDirection.Input

            Dim param_cliente As New SqlClient.SqlParameter
            param_cliente.ParameterName = "@ClienteChequeBco"
            param_cliente.SqlDbType = SqlDbType.NVarChar
            param_cliente.Size = 50
            param_cliente.Value = txtPropietario.Text
            param_cliente.Direction = ParameterDirection.Input

            Dim param_Venc As New SqlClient.SqlParameter
            param_Venc.ParameterName = "@FechaCobro"
            param_Venc.SqlDbType = SqlDbType.Date
            param_Venc.Size = 50
            param_Venc.Value = dtpFECHA.Value
            param_Venc.Direction = ParameterDirection.Input

            Dim param_Monto As New SqlClient.SqlParameter
            param_Monto.ParameterName = "@Monto"
            param_Monto.SqlDbType = SqlDbType.Decimal
            param_Monto.Precision = 18
            param_Monto.Value = txtMonto.Text
            param_Monto.Direction = ParameterDirection.Input

            Dim param_banco As New SqlClient.SqlParameter
            param_banco.ParameterName = "@banco"
            param_banco.SqlDbType = SqlDbType.VarChar
            param_banco.Size = 50
            param_banco.Value = cmbBanco.Text
            param_banco.Direction = ParameterDirection.Input

            Dim param_utilizado As New SqlClient.SqlParameter
            param_utilizado.ParameterName = "@utilizado"
            param_utilizado.SqlDbType = SqlDbType.Bit
            param_utilizado.Value = chkUtilizado.Checked
            param_utilizado.Direction = ParameterDirection.Input

            Dim param_observacionesutilizado As New SqlClient.SqlParameter
            param_observacionesutilizado.ParameterName = "@observacionesutilizado"
            param_observacionesutilizado.SqlDbType = SqlDbType.NVarChar
            param_observacionesutilizado.Size = 100
            param_observacionesutilizado.Value = txtUtilizado.Text
            param_observacionesutilizado.Direction = ParameterDirection.Input

            Dim param_observaciones As New SqlClient.SqlParameter
            param_observaciones.ParameterName = "@observaciones"
            param_observaciones.SqlDbType = SqlDbType.NVarChar
            param_observaciones.Size = 100
            param_observaciones.Value = txtObservaciones.Text
            param_observaciones.Direction = ParameterDirection.Input

            Dim param_NroMov As New SqlClient.SqlParameter
            param_NroMov.ParameterName = "@NroMov"
            param_NroMov.SqlDbType = SqlDbType.NVarChar
            param_NroMov.Size = 20
            param_NroMov.Value = grd.CurrentRow.Cells(10).Value
            param_NroMov.Direction = ParameterDirection.Input

            Dim param_userupd As New SqlClient.SqlParameter
            If bolModo = True Then
                param_userupd.ParameterName = "@useradd"
            Else
                param_userupd.ParameterName = "@userupd"
            End If
            param_userupd.SqlDbType = SqlDbType.SmallInt
            param_userupd.Value = UserID
            param_userupd.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                If bolModo = True Then
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCheques_Insert", param_id, _
                               param_nrocheque, param_cliente, param_Venc, param_Monto, param_banco, param_utilizado, _
                               param_observacionesutilizado, param_userupd, param_res)
                Else

                    Dim param_utilizadoOrig As New SqlClient.SqlParameter
                    param_utilizadoOrig.ParameterName = "@utilizadoOrig"
                    param_utilizadoOrig.SqlDbType = SqlDbType.Bit
                    param_utilizadoOrig.Value = CBool(grd.CurrentRow.Cells(1).Value)
                    param_utilizadoOrig.Direction = ParameterDirection.Input

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCheques_Update2", param_id, _
                                param_utilizado, param_utilizadoOrig, param_observaciones, param_observacionesutilizado, _
                                param_NroMov, param_userupd, param_res)
                End If

                res = param_res.Value
                AgregarActualizarRegistro = res

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

    Private Function AgregarActualizarRegistro_ChequesPropios() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Dim i As Integer


        Try
            For i = 0 To CInt(txtNroChequeFin.Text) - CInt(txtNroChequeInicio.Text)

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.Int
                param_id.Value = CInt(txtID.Text)
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nrocheque As New SqlClient.SqlParameter
                param_nrocheque.ParameterName = "@nrocheque"
                param_nrocheque.SqlDbType = SqlDbType.BigInt
                param_nrocheque.Value = CInt(txtNroChequeInicio.Text) + i
                param_nrocheque.Direction = ParameterDirection.Input

                Dim param_banco As New SqlClient.SqlParameter
                param_banco.ParameterName = "@banco"
                param_banco.SqlDbType = SqlDbType.VarChar
                param_banco.Size = 50
                param_banco.Value = cmbBancoPropio.Text
                param_banco.Direction = ParameterDirection.Input

                Dim param_userupd As New SqlClient.SqlParameter
                param_userupd.ParameterName = "@useradd"
                param_userupd.SqlDbType = SqlDbType.SmallInt
                param_userupd.Value = UserID
                param_userupd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spChequeras_Insert", param_id, _
                                              param_nrocheque, param_banco, param_userupd, param_res)

                    res = param_res.Value

                    If res < 1 Then
                        Exit For
                    End If

                Catch ex As Exception
                    Throw ex
                End Try
            Next

            AgregarActualizarRegistro_ChequesPropios = res

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
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.Int
            param_id.Value = CInt(txtID.Text)
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
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCheques_Delete", param_id, param_userdel, param_res)
                res = param_res.Value
                EliminarRegistro = res
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

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Cheques en Cartera"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        ''Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        'Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))

        'Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
        'Me.grd.Size = New Size(p)

        'If LLAMADO_POR_FORMULARIO Then
        '    LLAMADO_POR_FORMULARIO = False
        '    'Me.Top = ARRIBA
        '    'Me.Left = IZQUIERDA
        '    'Else
        '    '    Me.Top = 0
        '    '    Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        'End If

        'Me.Top = 0
        'Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2

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
        chkUtilizado.Tag = "1"
        'chkChequePropio.Tag = "2"
        txtNroCheque.Tag = "3"
        cmbBanco.Tag = "4"
        txtPropietario.Tag = "5"
        dtpFECHA.Tag = "6"
        txtMonto.Tag = "7"
        txtObservaciones.Tag = "8"
        txtUtilizado.Tag = "9"
        txtMontoCheques.Tag = "14"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        If dtpFECHA.Value < Date.Today And bolModo = True Then
            Util.MsgStatus(Status1, "La Fecha de Vencimiento del cheque no puede ser menor a la Fecha Actual.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "La Fecha de Vencimiento del cheque no puede ser menor a la Fecha Actual.", My.Resources.Resources.alert.ToBitmap, True)
            dtpFECHA.Focus()
            Exit Sub
        End If

        bolpoliticas = True

    End Sub

    Private Sub LlenarComboBancos()
        Dim ds_Bancos As Data.DataSet

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Bancos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                    " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos.Dispose()

            With Me.cmbBanco
                .DataSource = ds_Bancos.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
            End With

            With Me.cmbBancoPropio
                .DataSource = ds_Bancos.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
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

    'Private Sub chkChequePropio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If chkChequePropio.Checked = True Then
    '        txtPropietario.Text = "SEI SRL"
    '    Else
    '        txtPropietario.Text = ""
    '    End If
    'End Sub

  
    Private Sub chkChequesPropios_CheckedChanged(sender As Object, e As EventArgs) Handles chkChequesPropios.CheckedChanged
        btnNuevo.Enabled = Not chkChequesPropios.Checked
        GroupBox1.Enabled = Not chkChequesPropios.Checked
    End Sub
End Class