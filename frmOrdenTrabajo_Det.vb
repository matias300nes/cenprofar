
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient



Public Class frmOrdenTrabajo_Det
    Dim bolpoliticas As Boolean, band As Integer

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
    'Dim conn_del_form As SqlClient.SqlConnection = Nothing
    Dim conexionGenerica As SqlClient.SqlConnection = Nothing


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

    Enum ColumnasDelGridMateriales
        Codigo = 0
        Material = 1
        Qty = 2
        QtySaldo = 3
        IdPres_Det = 4
        IdMaterial = 5
        IdPresupuesto = 6
        PrecioVta = 7
        IdUnidad = 8
        Presup = 9
    End Enum

    Enum ColumnasDelGridOT
        Id = 0
        Codigo = 1
        IdCliente = 2
        Cliente = 3
        IdPresupuesto = 4
        NroPresupuesto = 5
        NroOC = 6
        TituloOT = 7
        Trabajo = 8
        FechaInicio = 9
        FechaFin = 10
        TiempoEstimado = 11
        Finalizado = 12
    End Enum

#Region "   Eventos"

    'Private Sub frmAjustes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.F3 'nuevo
    '            If bolModo = True Then
    '                If MessageBox.Show("No ha guardado el Ajuste Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un nuevo Ajuste?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                    btnNuevo_Click(sender, e)
    '                End If
    '            Else
    '                btnNuevo_Click(sender, e)
    '            End If
    '        Case Keys.F4 'grabar
    '            btnGuardar_Click(sender, e)
    '    End Select
    'End Sub

    Private Sub frmOrdenTrabajo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)
        Try
            conexionGenerica = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        band = 0

        configurarform()
        asignarTags()

        Permitir = True
        CargarCajas()
        PrepararBotones()

        grd.Visible = False

        LlenarGridOT()

        If grdItemsOT.RowCount > 0 Then
            txtObservaciones.Text = grdItemsOT.Rows(0).Cells(ColumnasDelGridOT.Trabajo).Value
            txtNroOC.Text = grdItemsOT.Rows(0).Cells(ColumnasDelGridOT.NroOC).Value
            txtTiempoEstimado.Text = grdItemsOT.Rows(0).Cells(ColumnasDelGridOT.TiempoEstimado).Value
            txtTitulo.Text = grdItemsOT.Rows(0).Cells(ColumnasDelGridOT.TituloOT).Value
        End If

        band = 1

    End Sub

    Private Sub txtBusqNombreMaterial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBusqNombreMaterial.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnModificar.Enabled = False
            btnEliminarProducto.Enabled = False
            LlenarCmbBusqMateriales()
        End If
    End Sub

    'Private Sub txtBusqNombreMaterial_KeyUp(sender As Object, e As KeyEventArgs) Handles txtBusqNombreMaterial.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        cmbBusqNombreMaterial.Focus()
    '    End If
    'End Sub

    'Private Sub txtBusqNombreMaterial_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBusqNombreMaterial.KeyDown
    '    If e.KeyCode = Keys.Up Then
    '        cmbBusqNombreMaterial.Focus()
    '    End If
    'End Sub

    'Private Sub txtBusqNombreMaterial_TextChanged(sender As Object, e As EventArgs) Handles txtBusqNombreMaterial.TextChanged
    '    LlenarCmbBusqMateriales()
    'End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            Dim i As Integer
            Dim QtyOT As Integer, qty As Integer, res_item As Integer, MatExiste As Boolean

            If txtidmaterial.Text = "0" Then
                Exit Sub
            End If

            For i = 0 To grdItems.RowCount - 1
                If txtidmaterial.Text = grdItems.Rows(i).Cells(ColumnasDelGridMateriales.IdMaterial).Value Then
                    If MessageBox.Show("El producto seleccionado ya existe, desea incrementar la cantidad asociada a la OT?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If grdItems.Rows(i).Cells(ColumnasDelGridMateriales.IdPres_Det).Value > 0 And grdItems.Rows(i).Cells(ColumnasDelGridMateriales.Presup).Value = "SI" Then
                            If txtQty.Text > grdItems.Rows(i).Cells(ColumnasDelGridMateriales.QtySaldo).Value Then
                                If MessageBox.Show("La cantidad Ingresada es Mayor a lo presupuestado. Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    qty = grdItems.Rows(i).Cells(ColumnasDelGridMateriales.QtySaldo).Value
                                    QtyOT = txtQty.Text - grdItems.Rows(i).Cells(ColumnasDelGridMateriales.QtySaldo).Value
                                    MatExiste = True
                                    Exit For
                                Else
                                    Exit Sub
                                End If
                            Else
                                qty = txtQty.Text
                                QtyOT = 0
                                MatExiste = True
                                Exit For
                            End If
                        Else
                            qty = 0
                            QtyOT = txtQty.Text
                            MatExiste = True
                            Exit For
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    MatExiste = False
                    qty = 0
                    QtyOT = txtQty.Text
                    Exit For
                End If
            Next

            If MatExiste = True Then
                res_item = AgregarRegistroItems(grdItems.Rows(i).Cells(ColumnasDelGridMateriales.IdPres_Det).Value, grdItems.Rows(i).Cells(ColumnasDelGridMateriales.IdPresupuesto).Value, _
                                                     grdItems.Rows(i).Cells(ColumnasDelGridMateriales.IdMaterial).Value, qty, QtyOT, grdItems.Rows(i).Cells(ColumnasDelGridMateriales.PrecioVta).Value, _
                                                     grdItems.Rows(i).Cells(ColumnasDelGridMateriales.IdUnidad).Value, txtIdCliente.Text)
            Else
                res_item = AgregarRegistroItems(0, 0, txtidmaterial.Text, qty, QtyOT, txtPrecioVta.Text, _
                                                     txtIdUnidad.Text, txtIdCliente.Text)
            End If

            Select Case res_item

                Case -5
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo registrar el movimiento de stock (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case -4
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No hay stock suficiente para descontar (items) '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case -1
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo registrar el remito (Items).", My.Resources.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case 0
                    Cancelar_Tran()
                    Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case Else

                    LlenarGridMateriales()

                    txtQty.Text = ""
                    txtIdUnidad.Text = ""
                    txtidmaterial.Text = ""
                    txtPrecioVta.Text = ""
                    txtMaterial.Text = ""
                    txtBusqNombreMaterial.Text = ""

                    Exit Sub
            End Select


        End If
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Administración de Órdenes de Trabajo - Detalle de Materiales"

        'Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        'Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
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

    Private Sub LlenarGridOT()

        SQL = "exec [spOrdenDeTrabajo_Select_All] @Eliminado = 0"

        GetDatasetItems(grdItemsOT)

        grdItemsOT.Columns(ColumnasDelGridOT.Id).Visible = False

        grdItemsOT.Columns(ColumnasDelGridOT.Codigo).Width = 50

        grdItemsOT.Columns(ColumnasDelGridOT.IdCliente).Visible = False

        grdItemsOT.Columns(ColumnasDelGridOT.Cliente).Width = 250

        grdItemsOT.Columns(ColumnasDelGridOT.IdPresupuesto).Visible = False

        grdItemsOT.Columns(ColumnasDelGridOT.NroPresupuesto).Width = 300

        grdItemsOT.Columns(ColumnasDelGridOT.NroOC).Visible = False

        grdItemsOT.Columns(ColumnasDelGridOT.TituloOT).Visible = False
        grdItemsOT.Columns(ColumnasDelGridOT.TiempoEstimado).Visible = False
        grdItemsOT.Columns(ColumnasDelGridOT.Trabajo).Visible = False
        grdItemsOT.Columns(ColumnasDelGridOT.FechaInicio).Visible = False
        grdItemsOT.Columns(ColumnasDelGridOT.FechaFin).Visible = False
        grdItemsOT.Columns(ColumnasDelGridOT.Finalizado).Visible = False

        With grdItemsOT
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ForeColor = Color.Black
            .ReadOnly = True
        End With

        With grdItemsOT.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdItemsOT.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        'SQL = "exec [spOrdenDeTrabajo_Select_All] @Eliminado = 0"

    End Sub

    Private Sub LlenarGridMateriales()

        SQL = "exec spOrdenDeTrabajo_Materiales_Select_ALL @IdCliente = " & grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.IdCliente).Value & ", @IdPresupuesto = " & grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.IdPresupuesto).Value & ", @IdOT = " & grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.Id).Value

        GetDatasetItems(grdItems)

        grdItems.Columns(ColumnasDelGridMateriales.PrecioVta).Visible = False

        grdItems.Columns(ColumnasDelGridMateriales.Codigo).ReadOnly = True
        grdItems.Columns(ColumnasDelGridMateriales.Codigo).Width = 100

        grdItems.Columns(ColumnasDelGridMateriales.Material).Width = 240
        grdItems.Columns(ColumnasDelGridMateriales.Material).ReadOnly = True

        grdItems.Columns(ColumnasDelGridMateriales.Qty).Width = 48
        grdItems.Columns(ColumnasDelGridMateriales.Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridMateriales.Qty).ReadOnly = True

        grdItems.Columns(ColumnasDelGridMateriales.QtySaldo).Width = 48
        grdItems.Columns(ColumnasDelGridMateriales.QtySaldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdItems.Columns(ColumnasDelGridMateriales.QtySaldo).ReadOnly = True

        grdItems.Columns(ColumnasDelGridMateriales.Presup).Width = 50
        grdItems.Columns(ColumnasDelGridMateriales.Presup).ReadOnly = True

        grdItems.Columns(ColumnasDelGridMateriales.IdPres_Det).Visible = False
        grdItems.Columns(ColumnasDelGridMateriales.IdUnidad).Visible = False
        grdItems.Columns(ColumnasDelGridMateriales.PrecioVta).Visible = False
        grdItems.Columns(ColumnasDelGridMateriales.IdMaterial).Visible = False
        grdItems.Columns(ColumnasDelGridMateriales.IdPresupuesto).Visible = False

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ForeColor = Color.Black
        End With

        With grdItems.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        SQL = "exec [spOrdenDeTrabajo_Select_All] @Eliminado = 0"

    End Sub

    Private Sub GetDatasetItems(ByVal grdchico As DataGridView)
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()

            grdchico.DataSource = ds_2.Tables(0).DefaultView

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

    Public Sub LlenarCmbBusqMateriales()
        Dim ConsSQL As String = Nothing

        Dim cnn As New SqlConnection(ConnStringSEI)

        If rdCodigo.Checked = True Then
            ConsSQL = "exec spMateriales_Select_By_Nombre '','" + txtBusqNombreMaterial.Text + "', ''"
        End If

        If rdCodBarra.Checked = True Then
            ConsSQL = "exec spMateriales_Select_By_Nombre '','','" + txtBusqNombreMaterial.Text + "'"
        End If

        If rdNombre.Checked = True Then
            ConsSQL = "exec spMateriales_Select_By_Nombre '" + txtBusqNombreMaterial.Text + "', '',''"
        End If

        Dim dsMaterial As New DataSet

        dsMaterial = SqlHelper.ExecuteDataset(cnn, CommandType.Text, ConsSQL)

        dsMaterial.Dispose()

        If dsMaterial.Tables(0).Rows.Count > 0 Then
            txtidmaterial.Text = dsMaterial.Tables(0).Rows(0)(0)
            txtMaterial.Text = dsMaterial.Tables(0).Rows(0)(1)
            txtIdUnidad.Text = dsMaterial.Tables(0).Rows(0)(2)
            txtPrecioVta.Text = dsMaterial.Tables(0).Rows(0)(3)
            txtQty.Focus()
        Else
            txtMaterial.Text = "El Código ingresado no corresponde a un Material válido o activo"
            txtidmaterial.Text = "0"
            txtBusqNombreMaterial.Focus()
        End If

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
                'param_origen.Value = cmbPresupuestos.Text
                param_origen.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechamov"
                param_fecha.SqlDbType = SqlDbType.DateTime
                ' param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@observacion"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                'param_nota.Value = txtObservaciones.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Insert", param_id, _
                                                  param_Codigo, param_origen, param_fecha, param_nota, param_res)

                        txtID.Text = param_id.Value
                        'txtCODIGO.Text = param_Codigo.Value

                    Else

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Update", param_id, _
                                                param_origen, param_fecha, param_nota, param_res)

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

    Private Function AgregarRegistroItems(ByVal IdPres_Det As Long, ByVal IdPresupuesto As Long, ByVal IdMaterial As Long, _
                                          ByVal qty As Double, ByVal qtyOT As Double, ByVal preciovta As Double, _
                                          ByVal IdUnidad As Long, ByVal idCliente As Long) As Integer
        Dim res As Integer = 0
        Dim msg As String

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
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_idot As New SqlClient.SqlParameter
            param_idot.ParameterName = "@idot"
            param_idot.SqlDbType = SqlDbType.BigInt
            param_idot.Value = txtID.Text
            param_idot.Direction = ParameterDirection.Input

            Dim param_idpresupuesto As New SqlClient.SqlParameter
            param_idpresupuesto.ParameterName = "@idpresupuesto"
            param_idpresupuesto.SqlDbType = SqlDbType.BigInt
            param_idpresupuesto.Value = IdPresupuesto
            param_idpresupuesto.Direction = ParameterDirection.Input

            Dim param_idpres_det As New SqlClient.SqlParameter
            param_idpres_det.ParameterName = "@idpres_det"
            param_idpres_det.SqlDbType = SqlDbType.BigInt
            param_idpres_det.Value = IdPres_Det
            param_idpres_det.Direction = ParameterDirection.Input

            Dim param_NroOT As New SqlClient.SqlParameter
            param_NroOT.ParameterName = "@NroOT"
            param_NroOT.SqlDbType = SqlDbType.Int
            param_NroOT.Value = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.Codigo).Value
            param_NroOT.Direction = ParameterDirection.Input

            Dim param_idmaterial As New SqlClient.SqlParameter
            param_idmaterial.ParameterName = "@idmaterial"
            param_idmaterial.SqlDbType = SqlDbType.BigInt
            param_idmaterial.Value = IdMaterial
            param_idmaterial.Direction = ParameterDirection.Input

            Dim param_qty As New SqlClient.SqlParameter
            param_qty.ParameterName = "@qty"
            param_qty.SqlDbType = SqlDbType.Decimal
            param_qty.Precision = 18
            param_qty.Scale = 2
            param_qty.Value = qty
            param_qty.Direction = ParameterDirection.Input

            Dim param_qtyot As New SqlClient.SqlParameter
            param_qtyot.ParameterName = "@qtyot"
            param_qtyot.SqlDbType = SqlDbType.Decimal
            param_qtyot.Precision = 18
            param_qtyot.Scale = 2
            param_qtyot.Value = qtyOT
            param_qtyot.Direction = ParameterDirection.Input

            Dim param_preciovta As New SqlClient.SqlParameter
            param_preciovta.ParameterName = "@preciovta"
            param_preciovta.SqlDbType = SqlDbType.Decimal
            param_preciovta.Precision = 18
            param_preciovta.Scale = 2
            param_preciovta.Value = preciovta
            param_preciovta.Direction = ParameterDirection.Input

            Dim param_idunidad As New SqlClient.SqlParameter
            param_idunidad.ParameterName = "@idunidad"
            param_idunidad.SqlDbType = SqlDbType.BigInt
            param_idunidad.Value = IdUnidad
            param_idunidad.Direction = ParameterDirection.Input

            Dim param_idcliente As New SqlClient.SqlParameter
            param_idcliente.ParameterName = "@idcliente"
            param_idcliente.SqlDbType = SqlDbType.BigInt
            param_idcliente.Value = idCliente
            param_idcliente.Direction = ParameterDirection.Input

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

            Dim param_msg As New SqlClient.SqlParameter
            param_msg.ParameterName = "@mensaje"
            param_msg.SqlDbType = SqlDbType.VarChar
            param_msg.Size = 150
            param_msg.Value = DBNull.Value
            param_msg.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spOrdenDeTrabajo_Materiales_Insert", _
                                param_id, param_idot, param_idpresupuesto, param_idpres_det, param_NroOT, _
                                param_idmaterial, param_qty, param_qtyot, param_preciovta, param_idunidad, param_idcliente, _
                                param_useradd, param_res, param_msg)


            res = param_res.Value

            If Not (param_msg.Value Is System.DBNull.Value) Then
                msg = param_msg.Value
            Else
                msg = ""
            End If

            AgregarRegistroItems = res

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

#End Region

    Private Sub grdItemsOT_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItemsOT.CellClick

        txtObservaciones.Text = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.Trabajo).Value
        txtNroOC.Text = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.NroOC).Value
        txtTiempoEstimado.Text = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.TiempoEstimado).Value
        txtTitulo.Text = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.TituloOT).Value
        txtIdCliente.Text = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.IdCliente).Value
        txtID.Text = grdItemsOT.CurrentRow.Cells(ColumnasDelGridOT.Id).Value

        LlenarGridMateriales()

    End Sub

    Private Sub grdItems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        txtidmaterial.Text = grdItems.CurrentRow.Cells(ColumnasDelGridMateriales.IdMaterial).Value
        txtMaterial.Text = grdItems.CurrentRow.Cells(ColumnasDelGridMateriales.Material).Value
        txtIdUnidad.Text = grdItems.CurrentRow.Cells(ColumnasDelGridMateriales.IdUnidad).Value
        txtPrecioVta.Text = grdItems.CurrentRow.Cells(ColumnasDelGridMateriales.PrecioVta).Value
        txtBusqNombreMaterial.Text = grdItems.CurrentRow.Cells(ColumnasDelGridMateriales.Codigo).Value
        txtQty.Text = grdItems.CurrentRow.Cells(ColumnasDelGridMateriales.Qty).Value

        btnEliminarProducto.Enabled = True
        btnModificar.Enabled = True

    End Sub

End Class