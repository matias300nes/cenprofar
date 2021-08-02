
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet
Imports System.Data.SqlClient

Public Class frmVentasClientes

    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim conn_del_form As SqlClient.SqlConnection = Nothing
    Public numvta As String
    Public totalvta As String


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
        configurarform()
        ' asignarTags()
        LlenarComboClientes()
        llenarcmbRepartidor()

        btnNuevo.Visible = False
        btnEliminar.Visible = False
        btnActualizar.Visible = False

        btnGuardar.Text = "Buscar(F4)"

        dtpDesde.Value = CDate(Today.Date.Year - 1 & "/" & Today.Date.Month & "/" & Today.Day)
        dtpHasta.Value = Date.Today

        'SQL = "spRPT_Ventas_Clientes '" & cmbClientes.SelectedValue & "','" & dtpDesde.Value & "','" & dtpHasta.Value & "'"

        'LlenarGrilla()

        btnGuardar_Click(sender, e)
        'RealizarConsulta()
        'Permitir = True
        'CargarCajas()
        'PrepararBotones()


        'If grd.Rows.Count > 0 Then
        grd.Columns(0).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(6).Visible = False
        grd.Columns(8).Visible = False
        grd.Columns(10).Visible = False
        'grd.Columns(11).Visible = False
        'grd.Columns(12).Visible = False
        'grd.Columns(13).Visible = False
        'grd.Columns(14).Visible = False
        'grd.Columns(15).Visible = False
        'grd.Columns(16).Visible = False
        'grd.Columns(17).Visible = False
        'grd.Columns(18).Visible = False
        'End If



    End Sub

    Protected Overloads Sub grd_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CellContentDoubleClick

        If Permitir Then

            Try

                numvta = grd.CurrentRow.Cells(4).Value
                totalvta = grd.CurrentRow.Cells(7).Value

                Dim VD As New frmVentasCliente_Det
                VD.ShowDialog()


            Catch ex As Exception

            End Try

        End If

    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        'Me.Text = "Unidades"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 60))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 10 - GroupBox1.Size.Height - GroupBox1.Location.Y - 50)
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
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub llenarcmbRepartidor()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT ' ' AS 'Codigo',' ' As 'Vendedor' Union SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 and Repartidor = 1 ORDER BY Vendedor")
            ds.Dispose()

            With cmbRepartidor
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Vendedor"
                .ValueMember = "Codigo"
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

    Private Function RealizarConsulta() As Boolean



        Try

            Dim connection As SqlClient.SqlConnection = Nothing
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            SQL = "spRPT_Ventas_Clientes '" & cmbClientes.SelectedValue & "','" & cmbRepartidor.SelectedValue & "','" & dtpDesde.Value & "','" & dtpHasta.Value & "'"



            bolModo = False
            LlenarGrilla()


            If grd.Rows.Count > 0 Then
                Permitir = True
                CargarCajas()
                PrepararBotones()
                ToolStripPagina.Enabled = True
                btnImprimir.Enabled = True
                RealizarConsulta = True
            Else
                RealizarConsulta = False
            End If


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


        If RealizarConsulta() = False Then
            Util.MsgStatus(Status1, "No se encontraron registros con los parametros seleccionados. Revise los datos", My.Resources.Resources.Info)
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

        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo, repartidor As String
        Dim desde, hasta As DateTime

        Try

        Catch ex As Exception

        End Try

        nbreformreportes = "Ventas por Clientes"

        codigo = IIf(cmbClientes.SelectedValue Is Nothing, "", cmbClientes.SelectedValue)
        repartidor = IIf(cmbRepartidor.SelectedValue Is Nothing, "", cmbRepartidor.SelectedValue)
        desde = dtpDesde.Value
        hasta = dtpHasta.Value

        rpt.VentasClientes_Maestro_App(codigo, repartidor, desde, hasta, rpt, My.Application.Info.AssemblyName.ToString)

        cerroparametrosconaceptar = False
        param = Nothing
        cnn = Nothing


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

        cmbClientes.SelectedIndex = 0
        cmbRepartidor.SelectedIndex = 0
        dtpDesde.Value = CDate(Today.Date.Year - 1 & "/" & Today.Date.Month & "/" & Today.Day)
        dtpHasta.Value = Date.Today
        rdClientes.Checked = False
        rdRepartidor.Checked = False
        'SQL = "spRPT_Ventas_Clientes '" & cmbClientes.SelectedValue & "','" & dtpDesde.Value & "','" & dtpHasta.Value & "'"
        'LlenarGrilla()
        RealizarConsulta()




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



    Private Sub rdClientes_CheckedChanged(sender As Object, e As EventArgs) Handles rdClientes.CheckedChanged
        cmbClientes.Enabled = rdClientes.Checked

        If rdClientes.Checked Then
            cmbRepartidor.Enabled = False
            cmbRepartidor.SelectedIndex = 0
        End If
    End Sub

    Private Sub rdRepartidor_CheckedChanged(sender As Object, e As EventArgs) Handles rdRepartidor.CheckedChanged
        cmbRepartidor.Enabled = rdRepartidor.Checked

        If rdRepartidor.Checked Then
            cmbClientes.Enabled = False
            cmbClientes.SelectedIndex = 0
        End If

    End Sub


End Class

