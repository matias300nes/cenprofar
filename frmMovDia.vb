
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmMovDia

    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

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

        'Me.Text = "Mov. del D�a"
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        configurarform()
        ' asignarTags()
        'LlenarComboClientes()

        btnNuevo.Visible = False
        btnActivar.Visible = False
        btnEliminar.Visible = False
        btnActualizar.Visible = False

        btnGuardar.Text = "Buscar(F4)"

        'cmbClientes.SelectedIndex = 0
        dtpDesde.Value = Date.Now
        dtpHasta.Value = Date.Now
        Permitir = True

        RealizarConsulta()



        bolModo = False
        If grd.Rows.Count = 0 Then

            Util.MsgStatus(Status1, "No se han registrado movimientos en el d�a.")

            'grd.Columns(0).Visible = False
            'grd.Columns(3).Visible = False
            'grd.Columns(7).Visible = False
            'grd.Columns(9).Visible = False
        End If

    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Mov. del D�a"

        Me.grd.Location = New Size(GroupBox1.Location.X + 2, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        Me.Size = New Size(Me.Size.Width + 57, (Screen.PrimaryScreen.WorkingArea.Height - 90) + 17)

        Dim p As New Size(GroupBox1.Size.Width + 55, Me.Size.Height - StatusStrip1.Height - GroupBox1.Size.Height - ToolMenu.Height - 50)
        Me.grd.Size = New Size(p)

        Me.Width = Me.Width + 20

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


    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        bolpoliticas = True

    End Sub

    Private Sub LlenarComboClientes()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select '' as 'Codigo' , '' as 'Clientes' union Select codigo, Nombre as 'Clientes' From Clientes Where eliminado = 0 ORDER BY Clientes")
            ds.Dispose()

            With cmbClientes
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Clientes"
                .ValueMember = "Codigo"
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provoc� un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub



#End Region

#Region "Funciones"

    Private Function RealizarConsulta() As Boolean



        Try

            Dim connection As SqlClient.SqlConnection = Nothing
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            SQL = "spMovimientosdelDia '" & dtpDesde.Value & "','" & dtpHasta.Value & "'"


            LlenarGrilla()
            Permitir = True
            CargarCajas()
            PrepararBotones()

            'Dim param_idcliente As New SqlClient.SqlParameter
            'param_idcliente.ParameterName = "@IDCLIENTE"
            'param_idcliente.SqlDbType = SqlDbType.VarChar
            'param_idcliente.Size = 25
            'param_idcliente.Value = cmbClientes.SelectedValue
            'param_idcliente.Direction = ParameterDirection.Input

            'Dim param_desde As New SqlClient.SqlParameter
            'param_desde.ParameterName = "@desde"
            'param_desde.SqlDbType = SqlDbType.DateTime
            'param_desde.Value = dtpDesde.Value
            'param_desde.Direction = ParameterDirection.Input

            'Dim param_hasta As New SqlClient.SqlParameter
            'param_hasta.ParameterName = "@hasta"
            'param_hasta.SqlDbType = SqlDbType.DateTime
            'param_hasta.Value = dtpDesde.Value
            'param_hasta.Direction = ParameterDirection.Input

            'Dim param_res As New SqlClient.SqlParameter
            'param_res.ParameterName = "@res"
            'param_res.SqlDbType = SqlDbType.Int
            'param_res.Value = DBNull.Value
            'param_res.Direction = ParameterDirection.InputOutput

            'SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spFiltrar_Ventas_Clientes_Select_All", param_desde, param_hasta, param_res)

            'RealizarConsulta = param_res.Value


        Catch ex As Exception

            MsgBox(ex.Message + "Desde de filtro")

        End Try


    End Function

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'If ALTA Then
        'bolModo = True
        'Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        'PrepararBotones()
        'Util.LimpiarTextBox(Me.Controls)
        'txtCODIGO.Focus()
        'Else
        '    Util.MsgStatus(Status1, "No tiene permiso para generar registros nuevos.", My.Resources.stop_error.ToBitmap)
        'End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        'Dim res As Integer

        Util.MsgStatus(Status1, "Buscanto Registros...", My.Resources.Resources.indicator_white)


        If ReglasNegocio() Then
            RealizarConsulta()
        End If
        '    Verificar_Datos()
        '    If bolpoliticas Then
        '        If bolModo Then
        '            'If ALTA Then
        '            res = AgregarRegistro()
        '            Select Case res
        '                Case -2
        '                    Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case -1
        '                    Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case 0
        '                    Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case Else
        '                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
        '            End Select
        '            'Else
        '            '    Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
        '            'End If
        '        Else
        '            'If MODIFICA Then
        '            res = ActualizarRegistro()
        '            Select Case res
        '                Case -3
        '                    Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo C�digo.", My.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case -2
        '                    Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case -1
        '                    Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case 0
        '                    Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
        '                    Exit Sub
        '                Case Else
        '                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
        '            End Select
        '            'Else
        '            '    Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
        '            'End If
        '        End If

        '        If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
        '            Try
        '                Dim sqlstring As String

        '                If bolModo = True Then
        '                    sqlstring = "INSERT INTO [dbo].[Unidades] (ID, [Codigo],[Nombre],[Eliminado])" & _
        '                                " values ( " & txtID.Text & ", '" & txtCODIGO.Text.ToUpper & "', '" & txtNOMBRE.Text.ToUpper & "' , 0 )"

        '                Else
        '                    sqlstring = "UPDATE [dbo].[Unidades] SET [Codigo] = '" & txtCODIGO.Text.ToUpper & " ', " & _
        '                                " [Nombre] = '" & txtNOMBRE.Text.ToUpper & "' " & _
        '                                " WHERE ID = " & txtID.Text
        '                End If

        '                tranWEB.Sql_Set(sqlstring)

        '            Catch ex As Exception
        '                'MsgBox(ex.Message)
        '                MsgBox("No se puede actualizar en la Web la lista de Marcas. Ejecute el bot�n sincronizar para actualizar el servidor WEB.")
        '            End Try
        '        End If

        '        bolModo = False
        '        PrepararBotones()
        '        MDIPrincipal.NoActualizarBase = False
        '        btnActualizar_Click(sender, e)


        '    End If
        'End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Dim res As Integer
        ''If BAJA_FISICA Then

        'If MessageBox.Show("Est� seguro que desea eliminar la Unidad seleccionada?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If

        'Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        'res = EliminarRegistro()
        'Select Case res
        '    Case -2
        '        Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
        '        Exit Sub
        '    Case -1
        '        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
        '        Exit Sub
        '    Case 0
        '        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
        '        Exit Sub
        '    Case Else
        '        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
        '        If Me.grd.RowCount = 0 Then
        '            bolModo = True
        '            PrepararBotones()
        '            Util.LimpiarTextBox(Me.Controls)
        '        End If




        'End Select
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
       
    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        'cmbClientes.SelectedIndex = 0
        dtpDesde.Value = Date.Now
        dtpHasta.Value = Date.Now
        RealizarConsulta()

        If grd.Rows.Count = 0 Then

            Util.MsgStatus(Status1, "No se han registrado movimientos en el d�a.")

            'grd.Columns(0).Visible = False
            'grd.Columns(3).Visible = False
            'grd.Columns(7).Visible = False
            'grd.Columns(9).Visible = False
        End If



    End Sub

    'Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click



    'End Sub

#End Region

#Region "Componentes Formulario"

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region


End Class

