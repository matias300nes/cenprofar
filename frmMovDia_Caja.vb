
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmMovDia_Caja

    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim conn_del_form As SqlClient.SqlConnection = Nothing
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
        band = 0
        LlenarcmbAlmacenes()
        'Me.Text = "Mov. del Día"
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        configurarform()
        ' asignarTags()
        'LlenarComboClientes()

        btnNuevo.Visible = False
        btnActivar.Visible = False
        btnEliminar.Visible = False
        btnActualizar.Visible = False

        btnGuardar.Text = "Buscar(F4)"

        'para cargar las cajas------
        SQL = "spMovimientos_Caja_All"
        LlenarGrilla()
        CargarCajas()
        PrepararBotones()
        '---------------------------


        dtpFecha.Value = Date.Now
        Permitir = True

        RealizarConsulta()


        bolModo = False
        If grd.Rows.Count = 0 Then
            Util.MsgStatus(Status1, "No se han registrado movimientos en el día.")
        End If


        grd.Columns(0).Visible = False

        band = 1
    End Sub

#End Region

    Private Sub cmbAlmacenes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacenes.SelectedIndexChanged
        If band = 1 Then
            Try
                txtidAlmacen.Text = cmbAlmacenes.SelectedValue

            Catch ex As Exception

            End Try
        End If
    End Sub

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Mov. de Caja"

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

    Private Sub LlenarcmbAlmacenes()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE codigo <> 4 and Eliminado = 0 ORDER BY Nombre")
            ds_Marcas.Dispose()

            With Me.cmbAlmacenes
                .DataSource = ds_Marcas.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                '.SelectedIndex = 0
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

    Private Sub CalcularTotalMov()
        '0
        Dim apertura As Double = 0
        '1
        Dim ingresos As Double = 0
        '2
        Dim gastos As Double = 0
        '3
        Dim retiros As Double = 0
        Dim total As Double = 0

        For i As Integer = 0 To grd.Rows.Count - 1
            If grd.Rows(i).Cells(0).Value = 0 Then
                apertura = apertura + grd.Rows(i).Cells(9).Value
            ElseIf grd.Rows(i).Cells(0).Value = 1 Then
                ingresos = ingresos + grd.Rows(i).Cells(9).Value
            ElseIf grd.Rows(i).Cells(0).Value = 2 Then
                gastos = gastos + grd.Rows(i).Cells(9).Value
            Else
                retiros = retiros + grd.Rows(i).Cells(9).Value
            End If
        Next

        lblApCaja.Text = FormatNumber(apertura, 2)
        lblIngresos.Text = FormatNumber(ingresos, 2)
        lblGastos.Text = FormatNumber(gastos, 2)
        lblRetiros.Text = FormatNumber(retiros, 2)
        total = apertura + ingresos + gastos + retiros
        lblTotal.Text = FormatNumber(total, 2)

    End Sub

#End Region

#Region "Funciones"

    Private Function RealizarConsulta() As Boolean

        Try

            Dim connection As SqlClient.SqlConnection = Nothing
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            SQL = "spMovimientos_Caja '" & dtpFecha.Value & "'"


            LlenarGrilla()
            Permitir = True
            'CargarCajas()
            'PrepararBotones()

            CalcularTotalMov()

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
   
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Dim res As Integer
        ''If BAJA_FISICA Then

        'If MessageBox.Show("Está seguro que desea eliminar la Unidad seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
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

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Unidades SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()


            If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                Try
                    Dim sqlstring As String

                    sqlstring = "UPDATE [dbo].[Unidades] SET [Eliminado] = 0 WHERE Codigo = '" & grd.CurrentRow.Cells(1).Value & "'"
                    tranWEB.Sql_Set(sqlstring)

                Catch ex As Exception
                    'MsgBox(ex.Message)
                    MsgBox("No se puede Activa en la Web la Unidad seleccionada. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                End Try
            End If

            SQL = "exec spUnidades_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "La Unidad se activó correctamente.", My.Resources.ok.ToBitmap)

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

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        dtpFecha.Value = Date.Now
        cmbAlmacenes.SelectedIndex = 0
        RealizarConsulta()

        If grd.Rows.Count = 0 Then
            Util.MsgStatus(Status1, "No se han registrado movimientos en el día.")
        End If



    End Sub

    Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click



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



End Class

