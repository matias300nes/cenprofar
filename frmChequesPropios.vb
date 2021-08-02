Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles.compartida
Imports Utiles.Util
Imports Utiles

Public Class frmChequesPropios

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
                    If MessageBox.Show("No ha guardado la Chequera que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Nueva Chequera?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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

        LlenarGrilla()

        Dim i As Integer
        For i = 0 To grd.Columns.Count - 1 Step 1
            grd.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Next

        Permitir = True
        CargarCajas()

        grd.Columns(0).Visible = False
        grd.Columns(10).Visible = False

        PrepararBotones()

    End Sub


    Private Sub txtNroCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroChequeInicio.KeyPress, _
         cmbBancoPropio.KeyPress
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
                    Case Else
                        Util.MsgStatus(Status1, "El Cheque ha sido Modificado", My.Resources.Resources.ok.ToBitmap)
                End Select
                bolModo = False
                PrepararBotones()
                SQL = "exec spCheques_Select_All @Eliminado = 0"
                LlenarGrilla()
            End If
        End If
    End Sub

    'Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.ClickEventArgs) Handles btnEliminar.Click
    '    Dim flag As Integer = 0

    '    Select Case EliminarRegistro()
    '        Case -2
    '            MessageBox.Show("El Empleado no ha podido ser Eliminado, el Empleado no Existe", "Error al Eliminar el Empleado", MessageBoxButtons.OK)
    '        Case -1
    '            MessageBox.Show("El Empleado no ha podido ser Eliminado, error en transaccion", "Error al Eliminar el Empleado", MessageBoxButtons.OK)
    '        Case Else
    '            MessageBox.Show("El Empleado ha sido Eliminado", "Eliminacion Exitosa", MessageBoxButtons.OK)
    '            lblInfo.Text = "Modo: Consulta o Modificacion"
    '            SQL = "exec spEmpleados_Select_All"
    '            LlenarGrilla()
    '    End Select
    'End Sub

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

        Dim i As Integer


        Try
            For i = 0 To CInt(txtNroChequeFin.Text) - CInt(txtNroChequeInicio.Text)

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idcheque"
                param_id.SqlDbType = SqlDbType.Int
                param_id.Value = CInt(txtID.Text)
                param_id.Direction = ParameterDirection.Input

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

            AgregarActualizarRegistro = res

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
            param_id.ParameterName = "@idempleado"
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
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spEmpleados_Delete", param_id, param_userdel, param_res)
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
        txtNroChequeInicio.Tag = "1"
        txtNroChequeFin.Tag = "2"
        cmbBancoPropio.Tag = "3"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        If CInt(txtNroChequeInicio.Text) < CInt(txtNroChequeFin.Text) Then
            Util.MsgStatus(Status1, "El Nro de Cheque inicial no puede ser menor que el Nro de Cheque Final.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El Nro de Cheque inicial no puede ser menor que el Nro de Cheque Final.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        bolpoliticas = True

    End Sub

#End Region

End Class