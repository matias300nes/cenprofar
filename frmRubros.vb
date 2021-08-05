Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmRubros
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

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient

    Dim permitir_evento_CellChanged As Boolean
    Dim codigo As String

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
                    If MessageBox.Show("No ha guardado el Rubro Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Rubro?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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

    Private Sub frmFamilias_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()
        asignarTags()
        SQL = "exec spFamilias_Select_All @Eliminado = 0"
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        'Setear_Grilla()

        'If bolModo = True Then
        '    LlenarGridItems()
        '    btnNuevo_Click(sender, e)
        'Else
        '    LlenarGridItems()
        'End If

        permitir_evento_CellChanged = True

        'btnEliminar.Enabled = False

    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEliminados.CheckedChanged

        btnNuevo.Enabled = Not chkEliminados.Checked
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spFamilias_Select_All @Eliminado = 1"
        Else
            SQL = "exec spFamilias_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        'LlenarGridItems()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
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

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        'PrepararGridItems()
        txtCODIGO.Focus()
        'grdItems.Columns(ColumnasDelGridItems.Eliminado).Visible = False

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If bolModo = False Then
            If MessageBox.Show("¿Está seguro que desea modificar el Rubro seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim res As Integer

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    res = AgregarRegistro()
                    Select Case res
                        Case -2
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "El codigo del rubro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case -1
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo actualizar el rubro.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case 0
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo agregar el rubro.", My.Resources.Resources.stop_error.ToBitmap)
                            Exit Sub
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado el rubro.", My.Resources.Resources.ok.ToBitmap)
              
                    End Select

                Else
                    'If MODIFICA Then
                    res = ActualizarRegistro()
                    Select Case res
                        Case -3
                            Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo Código.", My.Resources.stop_error.ToBitmap)
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
                'Try
                '    Dim sqlstring As String

                '    If bolModo = True Then
                '        sqlstring = "INSERT INTO [dbo].[" & NameTable_Familias & "] (ID, [Codigo],[Nombre],[Eliminado])" & _
                '                    " values ( " & txtID.Text & ", '" & codigo & "', '" & txtNOMBRE.Text.ToUpper & "' , 0 )"

                '    Else
                '        sqlstring = "UPDATE [dbo].[" & NameTable_Familias & "] SET [Codigo] = '" & txtCODIGO.Text & " ', " & _
                '                    " [Nombre] = '" & txtNOMBRE.Text.ToUpper & "' " & _
                '                    " WHERE ID = " & txtID.Text
                '    End If

                '    tranWEB.Sql_Set(sqlstring)

                'Catch ex As Exception
                '    'MsgBox(ex.Message)
                '    MsgBox("No se puede actualizar en la Web la lista de Marcas. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                'End Try
                'End If

                bolModo = False
                Cerrar_Tran()
                btnActualizar_Click(sender, e)

            End If
        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        If MessageBox.Show("Está seguro que desea eliminar El Rubro seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
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

        Dim param As New frmParametros
        Dim Cnn As New SqlConnection(ConnStringSEI)
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

    'Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    '    If txtID.Text <> "" Then
    '        LlenarGridItems()
    '    End If
    'End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente el Rubro: " & grd.CurrentRow.Cells(2).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Familias SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            'Try
            '    Dim sqlstring As String

            '    sqlstring = "UPDATE [dbo].[" & NameTable_Familias & "] SET [Eliminado] = 0 WHERE Codigo = '" & grd.CurrentRow.Cells(1).Value & "'"
            '    tranWEB.Sql_Set(sqlstring)

            'Catch ex As Exception
            '    'MsgBox(ex.Message)
            '    MsgBox("No se puede Activa en la Web el Rubro seleccionado. Ejecute el botón sincronizar para actualizar el servidor WEB.")
            'End Try
            'End If

            SQL = "exec spFamilias_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "El Rubro se activó correctamente.", My.Resources.ok.ToBitmap)

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
        Me.Text = "Rubros"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 65))
        '65-7-65
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
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        bolpoliticas = True

    End Sub

    'Private Function fila_vacia(ByVal i) As Boolean
    '    If (grdItems.Rows(i).Cells(ColumnasDelGridItems.Codigo).Value Is Nothing) Then
    '        fila_vacia = True
    '    Else
    '        fila_vacia = False
    '    End If
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

#Region "Funciones"

    Private Function AgregarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
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
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.InputOutput

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 300
            param_nombre.Value = txtNOMBRE.Text.ToUpper
            param_nombre.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spFamilias_Insert", param_id, param_codigo, param_nombre, param_useradd, param_res)

                txtID.Text = param_id.Value
                codigo = param_codigo.Value

                res = param_res.Value


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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Function

    Private Function ActualizarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
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
            param_nombre.Size = 300
            param_nombre.Value = txtNOMBRE.Text.ToUpper
            param_nombre.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spFamilias_Update", param_id, param_codigo, param_nombre, param_userupd, param_res)
                res = param_res.Value


                PrepararBotones()
                ActualizarRegistro = res
                If res > 0 Then ActualizarGrilla(grd, Me)


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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function EliminarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spFamilias_Delete", param_id, param_userdel, param_res)
                res = param_res.Value

                If res > 0 Then

                    'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                    'Try
                    '    Dim sqlstring As String

                    '    sqlstring = "UPDATE [dbo].[" & NameTable_Familias & "] SET [Eliminado] = 1 WHERE Codigo = '" & txtCODIGO.Text & "'"
                    '    tranWEB.Sql_Set(sqlstring)

                    'Catch ex As Exception
                    '    'MsgBox(ex.Message)
                    '    MsgBox("No se puede actualizar en la Web la lista de Rubros. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                    'End Try
                    'End If

                    Util.BorrarGrilla(grd)

                End If

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

    'Private Function AgregarRegistroItems(ByVal IdRubro As Long) As Integer
    '    Dim res As Integer = 0
    '    Dim i As Integer

    '    Try
    '        Try
    '            For i = 0 To grdItems.Rows.Count - 2

    '                Dim param_id As New SqlClient.SqlParameter
    '                param_id.ParameterName = "@id"
    '                param_id.SqlDbType = SqlDbType.BigInt
    '                If bolModo = True Then
    '                    param_id.Value = DBNull.Value
    '                    param_id.Direction = ParameterDirection.InputOutput
    '                Else
    '                    param_id.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Id).Value
    '                    param_id.Direction = ParameterDirection.Input
    '                End If

    '                Dim param_idfamilia As New SqlClient.SqlParameter
    '                param_idfamilia.ParameterName = "@idfamilia"
    '                param_idfamilia.SqlDbType = SqlDbType.BigInt
    '                param_idfamilia.Value = IdRubro
    '                param_idfamilia.Direction = ParameterDirection.Input

    '                Dim param_codigo As New SqlClient.SqlParameter
    '                param_codigo.ParameterName = "@codigo"
    '                param_codigo.SqlDbType = SqlDbType.VarChar
    '                param_codigo.Size = 25
    '                param_codigo.Value = grdItems.Rows(i).Cells(1).Value
    '                param_codigo.Direction = ParameterDirection.Input

    '                Dim param_nombre As New SqlClient.SqlParameter
    '                param_nombre.ParameterName = "@nombre"
    '                param_nombre.SqlDbType = SqlDbType.VarChar
    '                param_nombre.Size = 300
    '                param_nombre.Value = grdItems.Rows(i).Cells(2).Value
    '                param_nombre.Direction = ParameterDirection.Input

    '                Dim param_useradd As New SqlClient.SqlParameter
    '                If bolModo = True Then
    '                    param_useradd.ParameterName = "@useradd"
    '                Else
    '                    param_useradd.ParameterName = "@userupd"
    '                End If
    '                param_useradd.SqlDbType = SqlDbType.Int
    '                param_useradd.Value = UserID
    '                param_useradd.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@res"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = DBNull.Value
    '                param_res.Direction = ParameterDirection.InputOutput

    '                Try
    '                    If bolModo = True Then
    '                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spSubrubros_Insert", _
    '                                              param_id, param_idfamilia, param_codigo, param_nombre, param_useradd, _
    '                                              param_res)

    '                        res = param_res.Value

    '                        'If (res <= 0) Then
    '                        '    Exit For
    '                        'End If
    '                    Else
    '                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value Is DBNull.Value Then
    '                            param_useradd.ParameterName = "@useradd"
    '                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spSubrubros_Insert", _
    '                                          param_id, param_idfamilia, param_codigo, param_nombre, param_useradd, _
    '                                          param_res)

    '                            res = param_res.Value

    '                        Else
    '                            If CBool(grdItems.Rows(i).Cells(ColumnasDelGridItems.Eliminado).Value) = True Then
    '                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spSubrubros_Delete", _
    '                                     param_id, param_useradd, param_res)

    '                                res = param_res.Value

    '                                'If (res <= 0) Then
    '                                '    Exit For
    '                                'End If
    '                            Else
    '                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spSubrubros_Update", _
    '                                      param_id, param_idfamilia, param_codigo, param_nombre, param_useradd, _
    '                                      param_res)

    '                                res = param_res.Value

    '                                'If (res <= 0) Then
    '                                '    Exit For
    '                                'End If

    '                            End If
    '                        End If

    '                    End If

    '                    If (res <= 0) Then
    '                        Exit For
    '                    End If

    '                Catch ex As Exception
    '                    Throw ex
    '                End Try

    '            Next

    '            AgregarRegistroItems = res

    '        Catch ex2 As Exception
    '            Throw ex2
    '        Finally

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

#End Region

#Region "GridItems"

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        editando_celda = False
    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs)
        editando_celda = True
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    'Private Sub grdItems_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Dim Valor As String = ""
    '    If e.Button = Windows.Forms.MouseButtons.Right And bolModo = False Then
    '        If grdItems.RowCount <> 0 Then
    '            If Cell_Y <> -1 Then
    '                Try
    '                    Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.NombreSubrubro).Value.ToString
    '                Catch ex As Exception
    '                End Try
    '            End If
    '        End If
    '        If Valor <> "" Then
    '            Dim p As Point = New Point(e.X, e.Y)
    '            'MyBase.Point_Context = p
    '            'MyBase.Point_Context.Offset(40, 202)

    '            ContextMenuStrip1.Show(grdItems, p)
    '            ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
    '        End If
    '    End If
    'End Sub


    Private Sub Setear_Grilla()

        'ordernar descendente
        'grd.Sort(grd.Columns(1), System.ComponentModel.ListSortDirection.Descending)

        'setear grilla de items
        'With grdItems
        '    .VirtualMode = False
        '    .AllowUserToAddRows = True
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.MintCream
        '    .RowsDefaultCellStyle.BackColor = Color.White
        '    .AllowUserToOrderColumns = True
        '    .SelectionMode = DataGridViewSelectionMode.CellSelect
        'End With
    End Sub

    'Private Sub PrepararGridItems()
    '    Util.LimpiarGridItems(grdItems)
    'End Sub

    'Private Sub LlenarGridItems()

    '    If grdItems.Columns.Count > 0 Then
    '        grdItems.Columns.Clear()
    '    End If

    '    If txtID.Text = "" Then
    '        SQL = "exec spSubRubros_Select_By_IdFamilia @IdFamilia = 0"
    '    Else
    '        SQL = "exec spSubRubros_Select_By_IdFamilia @IdFamilia = " & CType(txtID.Text, Long)
    '    End If

    '    GetDatasetItems()

    '    grdItems.Columns(ColumnasDelGridItems.Id).Visible = False 'ID

    '    grdItems.Columns(ColumnasDelGridItems.Codigo).Width = 100
    '    grdItems.Columns(ColumnasDelGridItems.Codigo).ReadOnly = True

    '    grdItems.Columns(ColumnasDelGridItems.NombreSubrubro).Width = 250

    '    grdItems.Columns(ColumnasDelGridItems.Eliminado).Width = 100

    '    With grdItems
    '        .VirtualMode = False
    '        .AllowUserToAddRows = True
    '        .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
    '        .RowsDefaultCellStyle.BackColor = Color.White
    '        .AllowUserToOrderColumns = True
    '        .SelectionMode = DataGridViewSelectionMode.CellSelect
    '        .ForeColor = Color.Black
    '    End With
    '    With grdItems.ColumnHeadersDefaultCellStyle
    '        .BackColor = Color.Black  'Color.BlueViolet
    '        .ForeColor = Color.White
    '        .Font = New Font("TAHOMA", 8, FontStyle.Bold)
    '    End With
    '    grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)
    '    'grdEnsayos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    '    'Volver la fuente de datos a como estaba...
    '    If chkEliminados.Checked = True Then
    '        SQL = "exec spFamilias_Select_All @Eliminado = 1"
    '    Else
    '        SQL = "exec spFamilias_Select_All @Eliminado = 0"
    '    End If

    'End Sub

    'Private Sub GetDatasetItems()
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try
    '        ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
    '        ds_2.Dispose()

    '        grdItems.DataSource = ds_2.Tables(0).DefaultView

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
    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try

    'End Sub


#End Region

 
End Class