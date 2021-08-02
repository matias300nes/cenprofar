
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient



Public Class frmMaqHerramientas_Mov
    Dim bolpoliticas As Boolean, band As Boolean

    'Variables para la grilla
    Dim editando_celda As Boolean

    Dim llenandoCombo As Boolean = False

    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Para el combo de busqueda
    Dim ID_Buscado As Long
    Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    Dim Consulta As String

    Dim TotalPaginas1 As Integer
    Dim bolPaginar1 As Boolean = False
    Dim dtnew1 As New System.Data.DataTable
    Dim ini1 As Integer = 0
    Dim fin1 As Integer = 50 - 1
    Dim quitarfiltro1 As Boolean

    Public UltBusqueda As String

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String


#Region "   Eventos"

    Private Sub frmAjustes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Ajuste Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un nuevo Ajuste?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmMaqHerramientas_Mov_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        band = False

        btnEliminar.Text = "Anular Remito"

        configurarform()
        asignarTags()

        LlenarcmbEmpleados3_App(cmbEmpleado, ConnStringSEI)
        LlenarcmbOrigen_Destino()
        Me.LlenarComboEquipos()

        SQL = "exec spMaquinasHerramientas_Mov_Select_ALL @Eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos

        If bolModo = True Then
            btnNuevo_Click(sender, e)
        End If

        If grd.RowCount > 0 Then
            btnDevolver.Enabled = Not chkDevuelto.Checked
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)

            LlenarGridItems()

        End If

        grd.Columns(7).Visible = False
        grd.Columns(8).Visible = False

        band = True

        dtpFECHA.MaxDate = Today.Date

        Remitos_NO_Devueltos()

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress, _
        txtObservaciones.KeyPress, txtCantidad.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbEquipo_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbEquipo.KeyDown
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbOrigen_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbOrigen.KeyDown
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbDestino_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbDestino.KeyDown
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFECHA.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If bolModo = False Then
            btnGuardar.Enabled = Not chkEliminado.Checked
            btnNuevo.Enabled = Not chkEliminado.Checked
            btnCancelar.Enabled = Not chkEliminado.Checked

            If chkEliminado.Checked = True Then

                btnEliminar.Enabled = False

                SQL = "exec spMaquinasHerramientas_Mov_Select_ALL @Eliminado = 1"

            Else
                btnEliminar.Enabled = True

                SQL = "exec spMaquinasHerramientas_Mov_Select_ALL @Eliminado = 0"

            End If

            LlenarGrilla()

        End If
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then

            If grd.RowCount > 0 Then
                btnDevolver.Enabled = Not chkDevuelto.Checked

                cmbDestino.Enabled = Not chkDevuelto.Checked
                cmbOrigen.Enabled = Not chkDevuelto.Checked
                txtObservaciones.Enabled = Not chkDevuelto.Checked
                txtObservacionesDev.Enabled = Not chkDevuelto.Checked
                cmbEmpleado.Enabled = Not chkDevuelto.Checked
                grdItems.Enabled = Not chkDevuelto.Checked
                cmbEquipo.Enabled = Not chkDevuelto.Checked
                txtCantidad.Enabled = Not chkDevuelto.Checked

                LlenarGridItems()

            End If

        End If
    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then

            If cmbEquipo.Text = "" Then
                Util.MsgStatus(Status1, "Debe ingresar un nombre para el Equipo / Herramienta.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar un nombre para el Equipo / Herramienta.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If txtCantidad.Text = "" Or txtCantidad.Text = "0" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del Equipo / Herramienta.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del Equipo / Herramienta.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbEquipo.Text = grdItems.Rows(i).Cells(0).Value Then
                    Util.MsgStatus(Status1, "El Equipo / Herramienta '" & cmbEquipo.Text & "' está repetido en la fila: " & (i + 1).ToString & ".", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            Next

            'Dim j As Integer
            'For j = 0 To grdItems.RowCount - 1

            'Next

            grdItems.Rows.Add(cmbEquipo.Text, txtCantidad.Text)


            txtCantidad.Text = ""
            cmbEquipo.Text = ""
            txtObservaciones.Focus()
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        If e.ColumnIndex = 2 Then 'Marcar llegada
            grdItems.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Remitos de Salida para Equipos y Herramientas"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

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
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbOrigen.Tag = "3"
        cmbDestino.Tag = "4"
        cmbEmpleado.Tag = "5"
        txtObservaciones.Tag = "6"
        chkDevuelto.Tag = "7"
        txtObservacionesDev.Tag = "8"
    End Sub

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

    Private Sub LlenarComboEquipos()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT DescripcionMaq as Maquina FROM MaquinasHerramientas_Mov_Det UNION SELECT DISTINCT Nombre as Maquina FROM MaquinasHerramientas ")
            ds_Equipos.Dispose()

            With Me.cmbEquipo
                .DataSource = ds_Equipos.Tables(0).DefaultView
                .DisplayMember = "Maquina"
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

    Private Sub LlenarcmbOrigen_Destino()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Origen As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Origen = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT nombre FROM Almacenes where nombre like '%taller%' ORDER BY nombre")
            ds_Origen.Dispose()

            If ds_Origen.Tables(0).Rows.Count > 0 Then
                With cmbOrigen
                    .DataSource = ds_Origen.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .AutoCompleteMode = AutoCompleteMode.Suggest
                    .AutoCompleteSource = AutoCompleteSource.ListItems
                    .DropDownStyle = ComboBoxStyle.DropDown
                    .SelectedIndex = 0
                End With
            End If

            ds_Origen = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT destino FROM ( SELECT DISTINCT destino FROM MaquinasHerramientas_Mov UNION ALL SELECT Nombre as destino FROM Clientes WHERE Eliminado = 0 ) TT ORDER BY destino")
            ds_Origen.Dispose()

            If ds_Origen.Tables(0).Rows.Count > 0 Then
                With cmbDestino
                    .DataSource = ds_Origen.Tables(0).DefaultView
                    .DisplayMember = "destino"
                    .AutoCompleteMode = AutoCompleteMode.Suggest
                    .AutoCompleteSource = AutoCompleteSource.ListItems
                    .DropDownStyle = ComboBoxStyle.DropDown
                    .SelectedIndex = 0
                End With
            End If


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenandoCombo = False

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

        llenandoCombo = False

    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Dim i As Integer
        For i = 0 To grdItems.RowCount - 1
            Dim j As Integer
            Dim NombreEquipo As String = "", NombreEquipo_2 As String = ""
            NombreEquipo = IIf(grdItems.Rows(i).Cells(0).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(0).Value)
            For j = i + 1 To grdItems.RowCount - 1
                If grdItems.RowCount > 1 Then
                    NombreEquipo_2 = IIf(grdItems.Rows(j).Cells(0).Value Is DBNull.Value, "", grdItems.Rows(j).Cells(0).Value)
                    If NombreEquipo <> "" And NombreEquipo_2 <> "" Then
                        If NombreEquipo = NombreEquipo_2 Then
                            Util.MsgStatus(Status1, "El Equipo/Herramienta '" & NombreEquipo & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & j + 1, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    End If
                End If
            Next
        Next

        bolpoliticas = True

    End Sub

    Private Sub Imprimir(ByVal Codigo As Integer)
        nbreformreportes = "Remito"

        'Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        Rpt.NombreArchivoPDF = BuscarNombreArchivo(Codigo)

        If MessageBox.Show("Desea imprimir 2 Copias directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", True)
        Else
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", False)
        End If

        'cnn = Nothing

    End Sub

    Private Sub LlenarGridItems()

        grdItems.Rows.Clear()

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim dt As New DataTable
            Dim sqltxt2 As String

            sqltxt2 = "exec [spMaquinasHerramientas_Mov_Det_Select_By_IdMaqMov] @idMaqMov = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)("DescripcionMaq").ToString(), dt.Rows(i)("Cantidad").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub Remitos_NO_Devueltos()
        Dim i As Integer = 0

        With grd
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
            .RowsDefaultCellStyle.BackColor = Color.White
        End With

        grd.Refresh()

        For i = 0 To grd.Rows.Count
            Try
                If grd.Rows(i).Cells(7).Value = False Then
                    grd.Rows(i).DefaultCellStyle.BackColor = Color.Red
                    'grd.Rows(i).DefaultCellStyle.ForeColor = Color.White
                End If

            Catch ex As Exception

            End Try

        Next

    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try


            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

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

                Dim param_Codigo As New SqlClient.SqlParameter
                param_Codigo.ParameterName = "@Codigo"
                param_Codigo.SqlDbType = SqlDbType.BigInt
                param_Codigo.Value = DBNull.Value
                param_Codigo.Direction = ParameterDirection.InputOutput

                Dim param_origen As New SqlClient.SqlParameter
                param_origen.ParameterName = "@origen"
                param_origen.SqlDbType = SqlDbType.VarChar
                param_origen.Size = 200
                param_origen.Value = cmbOrigen.Text
                param_origen.Direction = ParameterDirection.Input

                Dim param_destino As New SqlClient.SqlParameter
                param_destino.ParameterName = "@destino"
                param_destino.SqlDbType = SqlDbType.VarChar
                param_destino.Size = 200
                param_destino.Value = cmbDestino.Text
                param_destino.Direction = ParameterDirection.Input

                Dim param_empleado As New SqlClient.SqlParameter
                param_empleado.ParameterName = "@idempleado"
                param_empleado.SqlDbType = SqlDbType.BigInt
                param_empleado.Value = cmbEmpleado.SelectedValue
                param_empleado.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechamov"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@observacion"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtObservaciones.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Insert", param_id, _
                                                  param_Codigo, param_origen, param_destino, param_empleado, param_fecha, param_nota, param_res)

                        txtID.Text = param_id.Value
                        txtCODIGO.Text = param_Codigo.Value

                    Else

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Update", param_id, _
                                                param_origen, param_destino, param_empleado, param_fecha, param_nota, param_res)

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

        End Try

    End Function

    Private Function AgregarActualizar_Registro_Items() As Integer
        Dim res As Integer

        Try

            Dim i As Integer

            Dim param_idMaqMov As New SqlClient.SqlParameter
            param_idMaqMov.ParameterName = "@idMaqMov"
            param_idMaqMov.SqlDbType = SqlDbType.BigInt
            param_idMaqMov.Value = txtID.Text
            param_idMaqMov.Direction = ParameterDirection.Input

            Dim param_resDEL As New SqlClient.SqlParameter
            param_resDEL.ParameterName = "@res"
            param_resDEL.SqlDbType = SqlDbType.Int
            param_resDEL.Value = DBNull.Value
            param_resDEL.Direction = ParameterDirection.InputOutput

            If bolModo = False Then
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Det_Delete", param_idMaqMov, param_resDEL)

                res = param_resDEL.Value

                If res <= 0 Then
                    MsgBox("Se produjo un error al eliminar temporalmente los Items", MsgBoxStyle.Critical, "Control de Errores")
                    AgregarActualizar_Registro_Items = -1
                    Exit Function
                End If

            End If

            For i = 0 To grdItems.Rows.Count - 1

                Dim param_Maquina As New SqlClient.SqlParameter
                param_Maquina.ParameterName = "@DescripcionMaq"
                param_Maquina.SqlDbType = SqlDbType.VarChar
                param_Maquina.Size = 4000
                param_Maquina.Value = grdItems.Rows(i).Cells(0).Value
                param_Maquina.Direction = ParameterDirection.Input

                Dim param_cantidad As New SqlClient.SqlParameter
                param_cantidad.ParameterName = "@cantidad"
                param_cantidad.SqlDbType = SqlDbType.BigInt
                param_cantidad.Value = grdItems.Rows(i).Cells(1).Value
                param_cantidad.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Det_Insert", _
                                              param_idMaqMov, param_Maquina, param_cantidad, param_res)

                    res = param_res.Value

                    If res <= 0 Then
                        AgregarActualizar_Registro_Items = -1
                    End If

                Catch ex As Exception
                    Throw ex
                End Try

            Next

            AgregarActualizar_Registro_Items = 1

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

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try

                Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_id.Value = CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Delete_All", param_id, param_res)

                    res = param_res.Value

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

        End Try
    End Function

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

    Private Function BuscarNombreArchivo(ByVal codigo As String) As String
        Dim ds_Archivo As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BuscarNombreArchivo = ""
            Exit Function
        End Try

        Try

            ds_Archivo = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select CONVERT(VARCHAR(10), codigo) + ' - ' + Destino from [dbo].[MaquinasHerramientas_Mov]  " & _
                            " WHERE eliminado = 0 and codigo = " & codigo)

            ds_Archivo.Dispose()

            BuscarNombreArchivo = ds_Archivo.Tables(0).Rows(0)(0)

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            BuscarNombreArchivo = ""

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

#Region "   Botones"

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If grdItems.RowCount = 0 Then
            Util.MsgStatus(Status1, "Debe ingresar al menos una Herramienta o un Equipo.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar al menos una Herramienta o un Equipo.", My.Resources.Resources.stop_error.ToBitmap, True)
            Exit Sub
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro()
                Select Case res
                    Case Is <= 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                    Case Else
                        res = AgregarActualizar_Registro_Items()
                        Select Case res
                            Case Is <= 0
                                Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                            Case Else
                                Cerrar_Tran()
                                Imprimir(txtCODIGO.Text)
                                bolModo = False
                                PrepararBotones()
                                SQL = "exec spMaquinasHerramientas_Mov_Select_ALL @Eliminado = 0"

                                'btnCancelar_Click(sender, e)
                                btnActualizar_Click(sender, e)

                                Remitos_NO_Devueltos()

                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkEliminado.Checked = False

        dtpFECHA.Value = Date.Today
        'txtDescripcion.Enabled = True

        Util.LimpiarTextBox(Me.Controls)

        grdItems.Rows.Clear()

        cmbOrigen.SelectedIndex = 0

        btnDevolver.Enabled = True

        cmbDestino.Enabled = True
        cmbOrigen.Enabled = True
        txtObservaciones.Enabled = True
        txtObservacionesDev.Enabled = True
        cmbEmpleado.Enabled = True
        grdItems.Enabled = True
        cmbEquipo.Enabled = True
        txtCantidad.Enabled = True

        btnDevolver.Enabled = False

        dtpFECHA.Focus()

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        If chkEliminado.Checked = False Then
            If MessageBox.Show("Esta acción anulará el Remito de Salida para Equipos y Herramientas (no podrá usar el mismo nro de remito en el sistema) " + vbCrLf + _
                           "¿Está seguro que desea Anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    Case Is <= 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo anular el Remito de Salida para Equipos y Herramientas.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo anular el Remito de Salida para Equipos y Herramientas.", My.Resources.stop_error.ToBitmap, True)
                    Case Else
                        Cerrar_Tran()
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True, True)
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        nbreformreportes = "Remito Equipamiento"

        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Rpt As New frmReportes

        param.AgregarParametros("N° de Mov:", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)

        param.ShowDialog()
        If cerroparametrosconaceptar = True Then
            codigo = param.ObtenerParametros(0)

            Rpt.NombreArchivoPDF = BuscarNombreArchivo(codigo)

            If MessageBox.Show("Desea imprimir 2 Copias directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Rpt.Remito_App(codigo, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", True)
            Else
                Rpt.Remito_App(codigo, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", False)
            End If

            cerroparametrosconaceptar = False
            param = Nothing
            'cnn = Nothing
        End If

    End Sub

    Private Sub btnDevolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDevolver.Click

        If MessageBox.Show("Está seguro que desea devolver el remito seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim connection As SqlClient.SqlConnection = Nothing
            Dim Res As Integer

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@observaciondev"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtObservacionesDev.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput


                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Delete", _
                                          param_id, param_nota, param_res)

                Res = param_res.Value

                If Res <= 0 Then
                    Util.MsgStatus(Status1, "No se pudo realizar la operación de devolución.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo realizar la operación de devolución.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                Else
                    bolModo = False
                    PrepararBotones()
                    btnActualizar_Click(sender, e)
                    Remitos_NO_Devueltos()
                    Util.MsgStatus(Status1, "Devolución exitosa.", My.Resources.Resources.stop_error.ToBitmap)
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
        End If

    End Sub

    Private Sub btnImprimirVacio_Click(sender As Object, e As EventArgs) Handles btnImprimirVacio.Click

        nbreformreportes = "Remito Equipamiento"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        If MessageBox.Show("Desea imprimir 1 Copia directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Rpt.Remito_App("0", Rpt, My.Application.Info.AssemblyName.ToString, "Equipo Vacio", True)
        Else
            Rpt.Remito_App("0", Rpt, My.Application.Info.AssemblyName.ToString, "Equipo Vacio", False)
        End If

    End Sub

#End Region

End Class