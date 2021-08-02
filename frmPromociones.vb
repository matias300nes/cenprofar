
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmPromociones

    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim Permitir2 As Boolean = False
    Dim ds_Producto As Data.DataSet
    Dim band As Integer

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
        band = 0
        configurarform()
        asignarTags()

        LlenarcmbLista()
        Me.LlenarCombo_Productos()

        SQL = "exec spPromocionesPorkys_Select_All @Eliminado = 0"

        LlenarGrilla()
      
        Permitir = True
        CargarCajas()
        PrepararBotones()

        grd.Columns(0).Visible = False
        grd.Columns(3).Visible = False
        grd.Columns(5).Visible = False
        'ajusto las columnas dependiendo el contenido 
        grd.AutoResizeColumns()
        Permitir2 = True
        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
        End If
        band = 1
    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminados.CheckedChanged
        btnNuevo.Enabled = Not chkEliminados.Checked
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spPromocionesPorkys_Select_All @Eliminado = 1"
        Else
            SQL = "exec spPromocionesPorkys_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
        End If
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        Try
            If Permitir2 = True Then
                cmbListaPrecio.SelectedValue = grd.CurrentRow.Cells(3).Value
                cmbProducto.SelectedValue = grd.CurrentRow.Cells(5).Value
                nudMinimo.Value = grd.CurrentRow.Cells(8).Value
                nudMaximo.Value = grd.CurrentRow.Cells(9).Value
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter Then
            'SendKeys.Send("{TAB}")
            txtPrecio.Focus()
        End If
    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged

        If band = 1 Then

            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4 FROM Materiales " & _
                                                                                    "where Codigo = '" & cmbProducto.SelectedValue & "'")
            ds_Producto.Dispose()


            If txtIDLista.Text = 3 Then
                txtPrecio.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(0), 2)
            Else
                txtPrecio.Text = Math.Round(ds_Producto.Tables(0).Rows(0)(1), 2)
            End If

        End If



    End Sub

    Private Sub cmbListaPrecio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbListaPrecio.SelectedIndexChanged
        If band = 1 Then
            txtIDLista.Text = cmbListaPrecio.SelectedValue
            If txtIdMaterial.Text <> "" Then
                cmbProducto_SelectedValueChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub cmbProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedIndexChanged
        If band = 1 Then
            txtIdMaterial.Text = cmbProducto.SelectedValue
        End If
    End Sub


#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Promociones"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 85))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 75)
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
        lblCodigo.Tag = "1"
        txtDescripción.Tag = "2"
        txtIDLista.Tag = "3"
        cmbListaPrecio.Tag = "4"
        txtIdMaterial.Tag = "5"
        cmbProducto.Tag = "6"
        txtPrecio.Tag = "7"
        nudMinimo.Tag = "8"
        nudMaximo.Tag = "9"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If nudMaximo.Value.ToString = "" Or nudMinimo.Value.ToString = "" Then
            Util.MsgStatus(Status1, "La cantidad Mínima / Máxima debe ser válida.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "La cantidad Mínima / Máxima debe ser válida.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(nudMinimo.Value) <= 0 Then
            Util.MsgStatus(Status1, "La cantidad Mínima debe ser mayor a cero.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "La cantidad Mínima debe ser mayor a cero.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(nudMaximo.Value) <= 0 Then
            Util.MsgStatus(Status1, "La cantidad Máxima debe ser mayor a cero.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "La cantidad Máxima debe ser mayor a cero.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If CDbl(nudMinimo.Value) > CDbl(nudMaximo.Value) Then
            Util.MsgStatus(Status1, "La cantidad Máxima debe ser mayor a la Mínima.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "La cantidad Máxima debe ser mayor a la Mínima.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If



        bolpoliticas = True

    End Sub

    Private Sub LlenarcmbLista()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            'ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 AND Codigo = 3 or Codigo = 4 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbListaPrecio
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
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

    Private Sub LlenarCombo_Productos()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, Nombre as 'Producto' FROM Materiales  WHERE eliminado = 0 ")
            ds_Equipos.Dispose()

            With Me.cmbProducto
                .DataSource = ds_Equipos.Tables(0).DefaultView
                .DisplayMember = "Producto"
                .ValueMember = "Codigo"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
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
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@Codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.Output

                Dim param_idlista As New SqlClient.SqlParameter
                param_idlista.ParameterName = "@IDListaPrecio"
                param_idlista.SqlDbType = SqlDbType.BigInt
                param_idlista.Value = IIf(txtIDLista.Text = "", cmbListaPrecio.SelectedValue, txtIDLista.Text)
                param_idlista.Direction = ParameterDirection.Input

                Dim param_idmaterial As New SqlClient.SqlParameter
                param_idmaterial.ParameterName = "@IdMaterial"
                param_idmaterial.SqlDbType = SqlDbType.VarChar
                param_idmaterial.Size = 25
                param_idmaterial.Value = IIf(txtIdMaterial.Text = "", cmbProducto.SelectedValue, txtIdMaterial.Text)
                param_idmaterial.Direction = ParameterDirection.Input

                Dim param_descripcion As New SqlClient.SqlParameter
                param_descripcion.ParameterName = "@Descripcion"
                param_descripcion.SqlDbType = SqlDbType.VarChar
                param_descripcion.Size = 4000
                param_descripcion.Value = txtDescripción.Text.ToUpper
                param_descripcion.Direction = ParameterDirection.Input


                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@Precio"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = txtPrecio.Text
                param_precio.Direction = ParameterDirection.Input

                Dim param_qtyminima As New SqlClient.SqlParameter
                param_qtyminima.ParameterName = "@QtyMinima"
                param_qtyminima.SqlDbType = SqlDbType.Decimal
                param_qtyminima.Precision = 18
                param_qtyminima.Scale = 2
                param_qtyminima.Value = CDbl(nudMinimo.Value)
                param_qtyminima.Direction = ParameterDirection.Input


                Dim param_qtymaxima As New SqlClient.SqlParameter
                param_qtymaxima.ParameterName = "@QtyMaxima"
                param_qtymaxima.SqlDbType = SqlDbType.Decimal
                param_qtymaxima.Precision = 18
                param_qtymaxima.Scale = 2
                param_qtymaxima.Value = CDbl(nudMaximo.Value)
                param_qtymaxima.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPromocionesPorkys_Insert", param_id, param_codigo, param_descripcion, _
                                                  param_idlista, param_idmaterial, param_precio, param_qtyminima, param_qtymaxima, param_useradd, param_res)
                        txtID.Text = param_id.Value
                        lblCodigo.Text = param_codigo.Value
                    Else
                        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPromocionesPorkys_Update", param_id, param_codigo, param_descripcion, _
                                                  param_idlista, param_idmaterial, param_precio, param_qtyminima, param_qtymaxima, param_useradd, param_res)

                    End If

                    AgregarActualizar_Registro = param_res.Value

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

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPromocionesPorkys_Delete", param_id, param_userdel, param_res)
                    res = param_res.Value

                    If res > 0 Then
                        'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                        '    Try
                        '        Dim sqlstring As String

                        '        sqlstring = "UPDATE [dbo].[Unidades] SET [Eliminado] = 1 WHERE Codigo = '" & txtCODIGO.Text & "'"
                        '        tranWEB.Sql_Set(sqlstring)

                        '    Catch ex As Exception
                        '        'MsgBox(ex.Message)
                        '        MsgBox("No se puede actualizar en la Web la lista de Rubros. Ejecute el botón sincronizar para actualizar el servidor WEB.")
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

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If ALTA Then
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        lblCodigo.Text = ""
        cmbListaPrecio.SelectedIndex = 0
        lblCodigo.Focus()
        SendKeys.Send("{TAB}")
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
                    res = AgregarActualizar_Registro()
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
                    res = AgregarActualizar_Registro()
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
                '        MsgBox("No se puede actualizar en la Web la lista de Marcas. Ejecute el botón sincronizar para actualizar el servidor WEB.")
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

        If MessageBox.Show("Está seguro que desea eliminar la Unidad seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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
        'Dim ReporteMaestroUnidades As New frmReportes()
        ' ReporteMaestroUnidades.MostrarMaestroUnidades(ReporteMaestroUnidades)
    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente la Unidad: " & grd.CurrentRow.Cells(2).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Promociones_Porkys SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()


            'If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '    Try
            '        Dim sqlstring As String

            '        sqlstring = "UPDATE [dbo].[Unidades] SET [Eliminado] = 0 WHERE Codigo = '" & grd.CurrentRow.Cells(1).Value & "'"
            '        tranWEB.Sql_Set(sqlstring)

            '    Catch ex As Exception
            '        'MsgBox(ex.Message)
            '        MsgBox("No se puede Activa en la Web la Unidad seleccionada. Ejecute el botón sincronizar para actualizar el servidor WEB.")
            '    End Try
            'End If

            SQL = "spPromocionesPorkys_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "La Promoción se activó correctamente.", My.Resources.ok.ToBitmap)

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

#Region "Componentes Formulario"

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles txtID.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region

    


End Class

