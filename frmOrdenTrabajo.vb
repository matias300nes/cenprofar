
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient

Public Class frmOrdenTrabajo
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

    Enum ColumnasDelGridPersonal
        IdEmpleado = 0
        Seleccionar = 1
        Empleado = 2
    End Enum

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

        LlenarcmbClientes_App(cmbCliente, ConnStringSEI, llenandoCombo)

        SQL = "exec [spOrdenDeTrabajo_Select_All] @Eliminado = 0"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos

        If bolModo = True Then
            btnNuevo_Click(sender, e)
        End If

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
            LlenarGridMateriales()
            LlenarGridPersonal()
        End If

        grd.Columns(2).Visible = False
        grd.Columns(4).Visible = False
        grd.Columns(8).Visible = False
        grd.Columns(10).Visible = False
        grd.Columns(12).Visible = False

        band = 1

        dtpFECHA.MaxDate = Today.Date

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress, _
        txtObservaciones.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbEquipo_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbOrigen_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbDestino_KeyDown(sender As Object, e As KeyEventArgs)
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
                If txtIdPresupuesto.Text <> "" Then
                    BuscarOCReq()
                Else
                    lblRemito.Text = "0"
                    lblPresupuesto.Text = "0"
                    lblFacturado.Text = "0"
                    lblDiferenciaRemito.Text = "0"
                    lblMoneda.Text = ""
                End If
                LlenarGridPersonal()
                LlenarGridMateriales()
            End If

        End If
    End Sub

    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 2 Then 'Marcar llegada
            grdItems.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged
        If band = 1 And bolModo = True Then
            txtIdCliente.Text = cmbCliente.SelectedValue
            LimpiarGridItems(grdItems)
            LlenarComboPresupuestos()
            If cmbPresupuestos.Items.Count = 0 Then
                lblRemito.Text = "0"
                lblPresupuesto.Text = "0"
                lblFacturado.Text = "0"
                lblDiferenciaRemito.Text = "0"
                lblMoneda.Text = ""
                txtNroOC.Text = ""
            Else
                cmbPresupuestos_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub cmbPresupuestos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPresupuestos.SelectedIndexChanged
        If band = 1 Then
            txtIdPresupuesto.Text = cmbPresupuestos.SelectedValue
            'If cmbPresupuestos.Text.Contains("Matx") Then
            '    'TipoRemito = "Matx"
            'ElseIf cmbPresupuestos.Text.Contains("ManoObrax") Then
            '    'TipoRemito = "ManoObrax"
            'End If
            BuscarOCReq()
            LlenarGridPersonal()
            LlenarGridMateriales()
        End If
    End Sub

    Private Sub grdItemsPersonal_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItemsPersonal.CurrentCellDirtyStateChanged
        If grdItemsPersonal.IsCurrentCellDirty Then
            grdItemsPersonal.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub



#End Region
#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Administración de Órdenes de Trabajo"

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
        cmbCliente.Tag = "2"
        txtIdCliente.Tag = "2"
        txtCliente.Tag = "3"
        cmbPresupuestos.Tag = "4"
        txtIdPresupuesto.Tag = "4"
        txtPresupuestos.Tag = "5"
        txtNroOC.Tag = "6"
        txtTitulo.Tag = "7"
        txtObservaciones.Tag = "8"
        dtpFECHA.Tag = "9"
        txtTiempoEstimado.Tag = "11"
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
                            Util.MsgStatus(Status1, "El Equipo/Herramienta '" & NombreEquipo & "' está repetido en la fila: " & (i + 1).ToString & " y la fila: " & j + 1, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    End If
                End If
            Next
        Next

        bolpoliticas = True

    End Sub

    Private Sub Imprimir()
        nbreformreportes = "Remito"

        'Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        If MessageBox.Show("Desea imprimir 2 Copias directamente en la impresora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", True)
        Else
            Rpt.Remito_App(txtCODIGO.Text, Rpt, My.Application.Info.AssemblyName.ToString, "Equipo", False)
        End If

        'cnn = Nothing

    End Sub

    Private Sub BuscarOCReq()

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idPresupuesto"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtIdPresupuesto.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_TotalPres As New SqlClient.SqlParameter
                param_TotalPres.ParameterName = "@TotalPres"
                param_TotalPres.SqlDbType = SqlDbType.Decimal
                param_TotalPres.Precision = 18
                param_TotalPres.Scale = 2
                param_TotalPres.Value = 0
                param_TotalPres.Direction = ParameterDirection.InputOutput

                Dim param_TotalRem As New SqlClient.SqlParameter
                param_TotalRem.ParameterName = "@TotalRem"
                param_TotalRem.SqlDbType = SqlDbType.Decimal
                param_TotalRem.Precision = 18
                param_TotalRem.Scale = 2
                param_TotalRem.Value = 0
                param_TotalRem.Direction = ParameterDirection.InputOutput

                Dim param_TotalFac As New SqlClient.SqlParameter
                param_TotalFac.ParameterName = "@TotalFac"
                param_TotalFac.SqlDbType = SqlDbType.Decimal
                param_TotalFac.Precision = 18
                param_TotalFac.Scale = 2
                param_TotalFac.Value = 0
                param_TotalFac.Direction = ParameterDirection.InputOutput

                Dim param_TotalDifRem As New SqlClient.SqlParameter
                param_TotalDifRem.ParameterName = "@TotalDifRem"
                param_TotalDifRem.SqlDbType = SqlDbType.Decimal
                param_TotalDifRem.Precision = 18
                param_TotalDifRem.Scale = 2
                param_TotalDifRem.Value = 0
                param_TotalDifRem.Direction = ParameterDirection.InputOutput

                Dim param_Moneda As New SqlClient.SqlParameter
                param_Moneda.ParameterName = "@Moneda"
                param_Moneda.SqlDbType = SqlDbType.VarChar
                param_Moneda.Size = 50
                param_Moneda.Value = ""
                param_Moneda.Direction = ParameterDirection.InputOutput

                Dim param_NroOC As New SqlClient.SqlParameter
                param_NroOC.ParameterName = "@NroOC"
                param_NroOC.SqlDbType = SqlDbType.VarChar
                param_NroOC.Size = 300
                param_NroOC.Value = ""
                param_NroOC.Direction = ParameterDirection.InputOutput

                Dim param_ContactoComp As New SqlClient.SqlParameter
                param_ContactoComp.ParameterName = "@ContactoComp"
                param_ContactoComp.SqlDbType = SqlDbType.VarChar
                param_ContactoComp.Size = 100
                param_ContactoComp.Value = ""
                param_ContactoComp.Direction = ParameterDirection.InputOutput

                Dim param_ContactoUsu As New SqlClient.SqlParameter
                param_ContactoUsu.ParameterName = "@ContactoUsu"
                param_ContactoUsu.SqlDbType = SqlDbType.VarChar
                param_ContactoUsu.Size = 100
                param_ContactoUsu.Value = ""
                param_ContactoUsu.Direction = ParameterDirection.InputOutput

                Dim param_Entregaren As New SqlClient.SqlParameter
                param_Entregaren.ParameterName = "@Entregaren"
                param_Entregaren.SqlDbType = SqlDbType.Bit
                param_Entregaren.Value = False
                param_Entregaren.Direction = ParameterDirection.InputOutput

                Dim param_SitioEntrega As New SqlClient.SqlParameter
                param_SitioEntrega.ParameterName = "@SitioEntrega"
                param_SitioEntrega.SqlDbType = SqlDbType.VarChar
                param_SitioEntrega.Size = 500
                param_SitioEntrega.Value = ""
                param_SitioEntrega.Direction = ParameterDirection.InputOutput

                Dim param_Usuario As New SqlClient.SqlParameter
                param_Usuario.ParameterName = "@Usuario"
                param_Usuario.SqlDbType = SqlDbType.Bit
                param_Usuario.Value = False
                param_Usuario.Direction = ParameterDirection.InputOutput

                Dim param_Comprador As New SqlClient.SqlParameter
                param_Comprador.ParameterName = "@Comprador"
                param_Comprador.SqlDbType = SqlDbType.Bit
                param_Comprador.Value = False
                param_Comprador.Direction = ParameterDirection.InputOutput

                Dim param_IdContactoUsu As New SqlClient.SqlParameter
                param_IdContactoUsu.ParameterName = "@IdContactoUsu"
                param_IdContactoUsu.SqlDbType = SqlDbType.BigInt
                param_IdContactoUsu.Value = 0
                param_IdContactoUsu.Direction = ParameterDirection.InputOutput

                Dim param_IdContactoCom As New SqlClient.SqlParameter
                param_IdContactoCom.ParameterName = "@IdContactoCom"
                param_IdContactoCom.SqlDbType = SqlDbType.BigInt
                param_IdContactoCom.Value = 0
                param_IdContactoCom.Direction = ParameterDirection.InputOutput

                Dim param_IdMoneda As New SqlClient.SqlParameter
                param_IdMoneda.ParameterName = "@IdMoneda"
                param_IdMoneda.SqlDbType = SqlDbType.BigInt
                param_IdMoneda.Value = 0
                param_IdMoneda.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPresupuestos_Gestion_BuscarInfoPres", param_id, _
                                              param_TotalPres, param_TotalRem, param_TotalFac, param_TotalDifRem, param_Moneda, param_NroOC, _
                                              param_ContactoComp, param_ContactoUsu, param_Entregaren, param_SitioEntrega, param_Usuario, _
                                              param_Comprador, param_IdContactoCom, param_IdContactoUsu, param_IdMoneda)

                    lblPresupuesto.Text = param_TotalPres.Value
                    lblMoneda.Text = param_Moneda.Value
                    lblRemito.Text = param_TotalRem.Value
                    lblFacturado.Text = param_TotalFac.Value
                    lblDiferenciaRemito.Text = param_TotalDifRem.Value
                    txtNroOC.Text = param_NroOC.Value

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

    End Sub

    Private Sub LlenarComboPresupuestos()
        Dim ds_Presup As Data.DataSet
        Dim Consulta As String
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            Consulta = "SELECT * FROM (select p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (Matx)') as Codigo " & _
                                " from Presupuestos p where OfertaComercial = 0 and MANOOBRA = 0 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & " AND status = 'P' AND p.eliminado=0" & _
                                " UNION ALL select p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (ManoObrax)') as Codigo " & _
                                " from Presupuestos p where OfertaComercial = 1 and MANOOBRA = 1 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & " AND status = 'P' AND p.eliminado=0" & _
                                " UNION ALL select DISTINCT p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (MatxOferta)') as Codigo " & _
                                " from Presupuestos p JOIN Presupuestos_Det pd ON pd.IDPresupuesto = p.id " & _
                                " where OfertaComercial = 1 and MANOOBRA = 0 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & _
                                " AND pd.status = 'P' AND p.eliminado=0" & _
                                " UNION ALL select DISTINCT p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS + ' (ManoObraxOferta)') as Codigo " & _
                                " from Presupuestos p JOIN Presupuestos_OfertasComerciales poc ON poc.IdPresupuesto = p.ID " & _
                                " JOIN Presupuestos_OfertasComerciales_Det pocd ON pocd.IdPresupuesto_OfertaComercial = poc.Id " & _
                                " where OfertaComercial = 1 and MANOOBRA = 0 AND IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & _
                                " AND pocd.status = 'P' AND p.eliminado=0 ) tt ORDER BY Id Desc"

            ds_Presup = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, Consulta)

            'ds_Presup = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select p.id , (p.codigo + ' - Rev: ' + CAST(nrorev AS VARCHAR(2)) + ' - ' + NOMBREREFERENCIA COLLATE Latin1_General_CI_AS ) as Codigo " & _
            '" from Presupuestos p where IDCLIENTE = " & CType(cmbCliente.SelectedValue, Long) & " AND status = 'P' AND p.eliminado=0 order by id desc") ' " AND Status = '" & IIf(rdPendiente.Checked = True, "p", "c") & "'

            ds_Presup.Dispose()

            band = 0
            With Me.cmbPresupuestos
                .DataSource = ds_Presup.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
            End With
            band = 1

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub LlenarGridPersonal()

        SQL = "exec spOrdenDeTrabajo_Empleados_Select_ALL @IdOT = " & IIf(txtID.Text = "", 0, txtID.Text)

        GetDatasetItems(grdItemsPersonal)

        grdItemsPersonal.Columns(ColumnasDelGridPersonal.IdEmpleado).Visible = False

        grdItemsPersonal.Columns(ColumnasDelGridPersonal.Seleccionar).Width = 80
        grdItemsPersonal.Columns(ColumnasDelGridPersonal.Seleccionar).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItemsPersonal.Columns(ColumnasDelGridPersonal.Empleado).Width = 185
        grdItemsPersonal.Columns(ColumnasDelGridPersonal.Empleado).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        With grdItemsPersonal
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
        End With

        With grdItemsPersonal.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdItemsPersonal.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        SQL = "exec [spOrdenDeTrabajo_Select_All] @Eliminado = 0"

    End Sub

    Private Sub LlenarGridMateriales()

        SQL = "exec spOrdenDeTrabajo_Materiales_Select_All @IdCliente = " & IIf(txtIdCliente.Text = "", 0, txtIdCliente.Text) & ", @IdPresupuesto = " & IIf(txtIdPresupuesto.Text = "", 0, txtIdPresupuesto.Text) & ", @IdOT = " & IIf(txtID.Text = "", 0, txtID.Text)

        GetDatasetItems(grdItems)

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
            .SelectionMode = DataGridViewSelectionMode.CellSelect
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
                param_Codigo.Direction = ParameterDirection.Input

                Dim param_presupuesto As New SqlClient.SqlParameter
                param_presupuesto.ParameterName = "@presupuesto"
                param_presupuesto.SqlDbType = SqlDbType.Bit
                If Not cmbPresupuestos.SelectedValue Is DBNull.Value Then
                    param_presupuesto.Value = True
                Else
                    param_presupuesto.Value = False
                End If
                param_presupuesto.Direction = ParameterDirection.Input

                Dim param_idpresupuesto As New SqlClient.SqlParameter
                param_idpresupuesto.ParameterName = "@idpresupuesto"
                param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                param_idpresupuesto.Value = cmbPresupuestos.SelectedValue
                param_idpresupuesto.Direction = ParameterDirection.Input

                Dim param_idCliente As New SqlClient.SqlParameter
                param_idCliente.ParameterName = "@idCliente"
                param_idCliente.SqlDbType = SqlDbType.BigInt
                param_idCliente.Value = cmbCliente.SelectedValue
                param_idCliente.Direction = ParameterDirection.Input

                Dim param_titulo As New SqlClient.SqlParameter
                param_titulo.ParameterName = "@tituloot"
                param_titulo.SqlDbType = SqlDbType.VarChar
                param_titulo.Size = 300
                param_titulo.Value = txtTitulo.Text
                param_titulo.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@descripcion"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 4000
                param_nota.Value = txtObservaciones.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechainicio"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_tiempo As New SqlClient.SqlParameter
                param_tiempo.ParameterName = "@Tiempoestimado"
                param_tiempo.SqlDbType = SqlDbType.Int
                param_tiempo.Value = txtTiempoEstimado.Value
                param_tiempo.Direction = ParameterDirection.Input

                Dim param_nrooc As New SqlClient.SqlParameter
                param_nrooc.ParameterName = "@nrooc"
                param_nrooc.SqlDbType = SqlDbType.VarChar
                param_nrooc.Size = 300
                param_nrooc.Value = txtNroOC.Text
                param_nrooc.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdenDeTrabajo_Insert", param_id, _
                                                  param_Codigo, param_presupuesto, param_idpresupuesto, param_idCliente, param_titulo, param_nota, _
                                                  param_fecha, param_tiempo, param_nrooc, param_res)

                        txtID.Text = param_id.Value

                    Else

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdenDeTrabajo_Update", param_id, _
                                                param_titulo, param_nota, param_presupuesto, param_fecha, param_tiempo, param_nrooc, param_res)

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

            Dim param_idOT As New SqlClient.SqlParameter
            param_idOT.ParameterName = "@idOT"
            param_idOT.SqlDbType = SqlDbType.BigInt
            param_idOT.Value = txtID.Text
            param_idOT.Direction = ParameterDirection.Input

            Dim param_resDEL As New SqlClient.SqlParameter
            param_resDEL.ParameterName = "@res"
            param_resDEL.SqlDbType = SqlDbType.Int
            param_resDEL.Value = DBNull.Value
            param_resDEL.Direction = ParameterDirection.InputOutput

            If bolModo = False Then
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdenDeTrabajo_Empleados_Delete", param_idOT, param_resDEL)

                res = param_resDEL.Value

                If res <= 0 Then
                    MsgBox("Se produjo un error al eliminar temporalmente los Items", MsgBoxStyle.Critical, "Control de Errores")
                    AgregarActualizar_Registro_Items = -1
                    Exit Function
                End If

            End If

            Dim HayEmpleado As Boolean = False

            For i = 0 To grdItemsPersonal.Rows.Count - 1

                If grdItemsPersonal.Rows(i).Cells(1).Value = True Then

                    Dim param_IdEmpleado As New SqlClient.SqlParameter
                    param_IdEmpleado.ParameterName = "@IdEmpleado"
                    param_IdEmpleado.SqlDbType = SqlDbType.BigInt
                    param_IdEmpleado.Value = grdItemsPersonal.Rows(i).Cells(0).Value
                    param_IdEmpleado.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdenDeTrabajo_Empleados_Insert", _
                                                  param_idOT, param_IdEmpleado, param_res)

                        res = param_res.Value

                        If res <= 0 Then
                            AgregarActualizar_Registro_Items = -1
                            HayEmpleado = False
                            Exit Function
                        End If

                        HayEmpleado = True

                    Catch ex As Exception
                        Throw ex
                    End Try

                End If


            Next

            If HayEmpleado = False Then
                AgregarActualizar_Registro_Items = -4
                Exit Function
            End If

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

#End Region

#Region "   Botones"

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro()
                Select Case res
                    Case Is <= 0
                        Util.MsgStatus(Status1, "No se pudo agregar/actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar/actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                    Case Else
                        res = AgregarActualizar_Registro_Items()
                        Select Case res
                            Case -1
                                Util.MsgStatus(Status1, "No se pueden insertar los Empleados asignados.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pueden insertar los Empleados asignados.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                            Case -4
                                Util.MsgStatus(Status1, "Debe asignar al menos un Empleado a la Orden de Trabajo.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "Debe asignar al menos un Empleado a la Orden de Trabajo.", My.Resources.Resources.stop_error.ToBitmap, True)
                                Cancelar_Tran()
                            Case Else
                                Cerrar_Tran()
                                'Imprimir()
                                bolModo = False
                                PrepararBotones()
                                SQL = "exec [spOrdenDeTrabajo_Select_All] @Eliminado = 0"

                                btnActualizar_Click(sender, e)

                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                        End Select
                End Select
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then

    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        dtpFECHA.Enabled = True

        Util.LimpiarTextBox(Me.Controls)

        Me.Label5.Visible = True
        Me.cmbCliente.Visible = True
        'Me.btnLlenarGrilla.Visible = True

        cmbCliente.Visible = bolModo
        txtCliente.Visible = Not bolModo

        cmbPresupuestos.Visible = bolModo
        txtPresupuestos.Visible = Not bolModo

        grdItems.Enabled = True

        lblRemito.Text = "0"
        lblPresupuesto.Text = "0"
        lblFacturado.Text = "0"
        lblDiferenciaRemito.Text = "0"
        lblMoneda.Text = ""
        txtNroOC.Text = ""

        dtpFECHA.Focus()

        'PrepararGridItems()
        LimpiarGridItems(grdItems)

        band = 1

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

#End Region

End Class