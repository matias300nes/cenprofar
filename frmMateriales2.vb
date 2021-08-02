Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmMateriales2

    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    Dim editando_celda As Boolean
    Dim llenandoCombo As Boolean = False

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction

    Dim bolpoliticas As Boolean

    'Dim LlenarCombo As Boolean = False
    'Dim preciobonificado As Double
    'Dim precioganancia As Double

    Dim band As Integer

    Dim CantRegistrosImportados As Long
    Dim CantRegistrosActualizados As Long

    Dim permitir_evento_CellChanged As Boolean = False

    Dim CargaContinua As Boolean = False

#Region "   Eventos"

    Private Sub frmMateriales_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If bolModo = False Then
            If chkEliminados.Checked = False Then
                SQL = "exec spMateriales_1_Select_All @Eliminado = 0"
            Else
                SQL = "exec spMateriales_1_Select_All @Eliminado = 1"
            End If
            btnActualizar_Click(sender, e)
        End If
    End Sub

    'Private Sub frmMateriales_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
    '    If permitir_evento_CellChanged Then
    '        If txtID.Text <> "" Then
    '            LlenarGridItems()
    '        End If
    '    End If
    'End Sub

    Private Sub frmMateriales_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIPrincipal.UltBusquedaMat = cmbBusqueda.Text
    End Sub

    Private Sub frmMateriales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Material Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Material?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
            Case Keys.F6 'grabar
                btnCargaContinua_Click(sender, e)
        End Select
    End Sub

    Private Sub frmMateriales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btnCargaContinua.Visible = True

        Cursor = Cursors.WaitCursor

        band = 0
        configurarform()
        asignarTags()

        LlenarcmbUnidades_App(cmbUnidadVta, ConnStringSEI)
        LlenarcmbUnidades_App(cmbUnidadCompra, ConnStringSEI)

        LlenarcmbRubros()

        LlenarcmbProveedores_App(cmbProveedores, ConnStringSEI, 0, 0)

        LlenarcmbAlmacenes(cmbAlmacenes, ConnStringSEI)


        Me.LlenarcmbMarca()

        LlenarcmbMoneda_2(cmbMonedaCompra, ConnStringSEI)
        LlenarcmbMoneda_2(cmbMonedaVta, ConnStringSEI)

        LlenarcmbBusqueda()

        SQL = "exec spMateriales_1_Select_All @Eliminado = 0"
        LlenarGrilla()

        Permitir = True
        CargarCajas()
        PrepararBotones()

        If bolModo = True Then
            btnNuevo_Click(sender, e)
        End If

        grd.Columns(1).Visible = False
        grd.Columns(4).Visible = False
        grd.Columns(6).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(11).Visible = False
        grd.Columns(19).Visible = False
        grd.Columns(25).Visible = False
        grd.Columns(26).Visible = False
        grd.Columns(27).Visible = False
        grd.Columns(28).Visible = False
        grd.Columns(29).Visible = False
        grd.Columns(30).Visible = False
        grd.Columns(31).Visible = False
        grd.Columns(32).Visible = False
        grd.Columns(33).Visible = False
        grd.Columns(34).Visible = False
        grd.Columns(35).Visible = False
        grd.Columns(36).Visible = False
        grd.Columns(37).Visible = False

        permitir_evento_CellChanged = True

        If MDIPrincipal.UltBusquedaMat <> "" Then
            cmbBusqueda.Text = MDIPrincipal.UltBusquedaMat
            chkNombre.Checked = True
            chkNombre_CheckedChanged(sender, e)
        End If

        btnImportarExcel.Visible = True
        btnImagenExcel.Visible = True

        band = 1

        Cursor = Cursors.Default
    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles txtID.KeyPress, txtCODIGO.KeyPress, txtNOMBRE.KeyPress, _
            cmbFAMILIAS.KeyPress, cmbUnidadVta.KeyPress, _
             txtPrecioVtaBulto.KeyPress, txtMinimo.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub PicFamilias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicFAMILIAS.Click
        Dim f As New frmRubros
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbFAMILIAS.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbFamilias_App(cmbFAMILIAS, ConnStringSEI)
        cmbFAMILIAS.Text = texto_del_combo
    End Sub

    Private Sub PicUnidades_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicUNIDADES.Click
        Dim f As New frmUnidades

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbUnidadVta.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbUnidades_App(cmbUnidadVta, ConnStringSEI)
        cmbUnidadVta.Text = texto_del_combo
    End Sub

    Private Sub chkNombre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNombre.CheckedChanged
        cmbBusqueda.Enabled = chkNombre.Checked
        If chkNombre.Checked = True Then
            If cmbBusqueda.Text <> "" Then
                QuitarElFitroToolStripMenuItem_Click(sender, e)
                ColumnName = "Nombre"
                ColumnType = "system.string"
                Filtrarpor(cmbBusqueda.Text, "Materiales")
            End If
        Else
            QuitarElFitroToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub cmbNombre_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBusqueda.SelectedValueChanged
        If band = 1 Then
            If cmbBusqueda.Text <> "" Then
                QuitarElFitroToolStripMenuItem_Click(sender, e)
                ColumnName = "Nombre"
                ColumnType = "system.string"
                Filtrarpor(cmbBusqueda.Text, "Materiales")
                If grd.RowCount > 0 Then
                    cmbMarcas.SelectedValue = grd.CurrentRow.Cells(1).Value
                    cmbFAMILIAS.SelectedValue = grd.CurrentRow.Cells(4).Value
                    cmbSubRubro.SelectedValue = grd.CurrentRow.Cells(6).Value
                    cmbMonedaVta.SelectedValue = grd.CurrentRow.Cells(9).Value
                    cmbUnidadVta.SelectedValue = grd.CurrentRow.Cells(11).Value
                    cmbProveedores.SelectedValue = grd.CurrentRow.Cells(19).Value
                    cmbMonedaCompra.SelectedValue = grd.CurrentRow.Cells(27).Value
                    cmbUnidadCompra.SelectedValue = grd.CurrentRow.Cells(29).Value
                End If
            Else
                QuitarElFitroToolStripMenuItem_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub cmbNombre_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBusqueda.KeyDown
        If band = 1 Then
            If cmbBusqueda.Text <> "" And e.KeyCode = Keys.Enter Then
                QuitarElFitroToolStripMenuItem_Click(sender, e)
                ColumnName = "Nombre"
                ColumnType = "system.string"
                Filtrarpor(cmbBusqueda.Text, "Materiales")
                If grd.RowCount > 0 Then
                    cmbMarcas.SelectedValue = grd.CurrentRow.Cells(1).Value
                    cmbFAMILIAS.SelectedValue = grd.CurrentRow.Cells(4).Value
                    cmbSubRubro.SelectedValue = grd.CurrentRow.Cells(6).Value
                    cmbMonedaVta.SelectedValue = grd.CurrentRow.Cells(9).Value
                    cmbUnidadVta.SelectedValue = grd.CurrentRow.Cells(11).Value
                    cmbProveedores.SelectedValue = grd.CurrentRow.Cells(19).Value
                    cmbMonedaCompra.SelectedValue = grd.CurrentRow.Cells(27).Value
                    cmbUnidadCompra.SelectedValue = grd.CurrentRow.Cells(29).Value
                End If
            End If
        End If
    End Sub


    Private Sub cmbFAMILIAS_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFAMILIAS.SelectedValueChanged
        If llenandoCombo = False Then
            LlenarcmbSubRubros()
        End If
    End Sub

    Private Sub PicGanancia_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picGanancia.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.picGanancia, "Por medio de esta opción se puede ocultar o mostrar las columnas Bonif4 y Bonif5l.")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminados.CheckedChanged
        btnGuardar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spMateriales_1_Select_All @Eliminado = 1"
        Else
            SQL = "exec spMateriales_1_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

    End Sub

    Private Sub cmbFAMILIAS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFAMILIAS.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdRubro.Text = cmbFAMILIAS.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbAlmacenes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAlmacenes.SelectedIndexChanged
        If band = 1 Then
            Try
                txtidAlmacen.Text = cmbAlmacenes.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbSubRubro_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSubRubro.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdSubrubro.Text = cmbSubRubro.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtPrecioLista_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrecioLista.KeyDown, txtBonif1.KeyDown, _
        txtBonif2.KeyDown, txtBonif3.KeyDown, txtBonif4.KeyDown, txtBonif5.KeyDown, txtIva.KeyDown, txtganancia.KeyDown, txtCantxBulto.KeyDown
        If e.KeyCode = Keys.Enter Then
            CalcularPrecioFinal()
        End If
    End Sub

    Private Sub txtPrecioLista_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrecioLista.LostFocus, txtBonif1.LostFocus, _
        txtBonif2.LostFocus, txtBonif3.LostFocus, txtBonif4.LostFocus, txtBonif5.LostFocus, txtIva.LostFocus, txtganancia.LostFocus, txtCantxBulto.LostFocus
        CalcularPrecioFinal()
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Lista de Materiales"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        'Dim p As New Size(GroupBox1.Size.Width, 400)
        'Me.grd.Size = New Size(p)

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
        txtIdMarca.Tag = "1"
        txtCODIGO.Tag = "2"
        txtNOMBRE.Tag = "3"
        txtIdRubro.Tag = "4"
        cmbFAMILIAS.Tag = "5"
        txtIdSubrubro.Tag = "6"
        cmbSubRubro.Tag = "7"
        cmbMarcas.Tag = "8"
        txtIdMonedaVta.Tag = "9"
        cmbMonedaVta.Tag = "10"
        txtIdUnidadVta.Tag = "11"
        cmbUnidadVta.Tag = "12"
        txtPrecioLista.Tag = "13"
        txtganancia.Tag = "14"
        txtIva.Tag = "15"
        txtPrecioVta.Tag = "16"
        lblPrecioIva21.Tag = "17"
        lblPrecioIva10.Tag = "18"
        txtIdProveedor.Tag = "19"
        cmbProveedores.Tag = "20"
        txtMinimo.Tag = "21"
        txtMaximo.Tag = "22"
        lblStockActual.Tag = "23"
        txtFechaUpd.Tag = "24"

        txtIdMonedaCompra.Tag = "27"
        cmbMonedaCompra.Tag = "28"
        txtIdUnidadCompra.Tag = "29"
        cmbUnidadCompra.Tag = "30"

        txtBonif1.Tag = "31"
        txtBonif2.Tag = "32"
        txtBonif3.Tag = "33"
        txtBonif4.Tag = "34"
        txtBonif5.Tag = "35"

        txtCantxBulto.Tag = "36"
        txtPrecioVtaBulto.Tag = "37"

    End Sub

    Private Sub Verificar_Datos()
        bolpoliticas = False

        If txtMinimo.Text = "" Or txtMinimo.Text = "0" Then
            txtMinimo.Text = "1"
        End If

        If txtMaximo.Text = "" Or txtMaximo.Text = "0" Then
            Util.MsgStatus(Status1, "El campo máximo está vacío o en Cero.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El campo máximo está vacío o en Cero.", My.Resources.Resources.alert.ToBitmap, True)
            txtMaximo.Focus()
            Exit Sub
        End If

        If CType(txtMinimo.Text, Long) > CType(txtMaximo.Text, Long) Then
            Util.MsgStatus(Status1, "El campo Mínimo no puede ser mayor al campo Máximo.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El campo Mínimo no puede ser mayor al campo Máximo.", My.Resources.Resources.alert.ToBitmap, True)
            txtMaximo.Focus()
            Exit Sub
        End If


        bolpoliticas = True

    End Sub

    Private Sub LlenarcmbRubros()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Id, (codigo + ' - ' +  rtrim(nombre)) as Nombre FROM Familias WHERE Eliminado = 0 ORDER BY nombre")
            ds_Rubros.Dispose()

            If ds_Rubros.Tables(0).Rows.Count > 0 Then
                With cmbFAMILIAS
                    .DataSource = ds_Rubros.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .ValueMember = "id"
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

    Private Sub LlenarcmbSubRubros()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_SubRubros As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            If CType(cmbFAMILIAS.SelectedValue, Long) = 0 Then
                Exit Sub
            End If

            ds_SubRubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, (codigo + ' - ' + rtrim(nombre)) as Nombre FROM subrubros WHERE idfamilia = " & CType(cmbFAMILIAS.SelectedValue, Long) & " and Eliminado = 0 ORDER BY nombre")

            ds_SubRubros.Dispose()

            With cmbSubRubro
                .DataSource = ds_SubRubros.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
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

    Private Sub LlenarcmbBusqueda()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Distinct Busqueda FROM CriteriosdeBusquedas ORDER BY BUSQUEDA")
            ds.Dispose()

            With cmbBusqueda
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "BUSQUEDA"
                '.ValueMember = "IdUsuario"
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

    Private Function CargarExcel2(ByVal SLibro As String, ByVal sHoja As String) As Boolean

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ListaPrecios]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
        '                    "   DROP TABLE [listaPrecios];" & _
        '                    " SELECT * INTO ListaPrecios FROM OPENROWSET ('Microsoft.Jet.OLEDB.4.0', " & _
        '                    "'Excel 8.0;Database=" + SLibro + ";IMEX=1', " & _
        '                    "'SELECT * FROM [Lista de precios$]'); " & _
        '                    " ALTER TABLE ListaPrecios ADD IdFamilia BIGINT; " & _
        '                    " ALTER TABLE ListaPrecios ADD IdSubRubro BIGINT; " & _
        '                    " ALTER TABLE ListaPrecios ADD IdMoneda BIGINT; " & _
        '                    " ALTER TABLE ListaPrecios ADD IdMaterial BIGINT; " & _
        '                    " ALTER TABLE ListaPrecios ADD IdMarca BIGINT ;" & _
        '                    "   DECLARE @cant as INT; " & _
        '                    "	SELECT @Cant = count(*) " & _
        '                    "	FROM   " & _
        '                    "	sysobjects INNER JOIN " & _
        '                    "	syscolumns ON sysobjects.id = syscolumns.id INNER JOIN " & _
        '                    "	systypes ON syscolumns.xtype = systypes.xtype " & _
        '                    "	WHERE     (sysobjects.xtype = 'U') " & _
        '                    "	and (UPPER(syscolumns.name) like upper('%COS 1%')) " & _
        '                    "	IF @Cant > 0 " & _
        '                    "       BEGIN " & _
        '                    "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[COS 1]', 'Nivel_1_COS', 'COLUMN'; " & _
        '                    "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[COS 2]', 'Nivel_2_COS', 'COLUMN'; " & _
        '                    "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Importe Neto]', 'Precio_Neto', 'COLUMN'; " & _
        '                    "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Importe Lista]', 'Precio_Lista', 'COLUMN'; " & _
        '                    "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Tipo de Stock]', 'Tipo_Stock', 'COLUMN'; " & _
        '                    "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Tiempo de Entrega]', 'Plazo', 'COLUMN'; " & _
        '                    "      End"

        Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ListaPrecios]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
                            "   DROP TABLE [listaPrecios];" & _
                            " SELECT * INTO ListaPrecios FROM OPENROWSET ('Microsoft.ACE.OLEDB.12.0', " & _
                            "'Excel 12.0 Xml;HDR=YES;Database=" + SLibro + "', " & _
                            "'SELECT * FROM [Lista de precios$]'); " & _
                            " ALTER TABLE ListaPrecios ADD IdFamilia BIGINT; " & _
                            " ALTER TABLE ListaPrecios ADD IdSubRubro BIGINT; " & _
                            " ALTER TABLE ListaPrecios ADD IdMoneda BIGINT; " & _
                            " ALTER TABLE ListaPrecios ADD IdMaterial BIGINT; " & _
                            " ALTER TABLE ListaPrecios ADD IdMarca BIGINT ;" & _
                            "   DECLARE @cant as INT; " & _
                            "	SELECT @Cant = count(*) " & _
                            "	FROM   " & _
                            "	sysobjects INNER JOIN " & _
                            "	syscolumns ON sysobjects.id = syscolumns.id INNER JOIN " & _
                            "	systypes ON syscolumns.xtype = systypes.xtype " & _
                            "	WHERE     (sysobjects.xtype = 'U') " & _
                            "	and (UPPER(syscolumns.name) like upper('%COS 1%')) " & _
                            "	IF @Cant > 0 " & _
                            "       BEGIN " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[COS 1]', 'Nivel_1_COS', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[COS 2]', 'Nivel_2_COS', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Importe Neto]', 'Precio_Neto', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Importe Lista]', 'Precio_Lista', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Tipo de Stock]', 'Tipo_Stock', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Tiempo de Entrega]', 'Plazo', 'COLUMN'; " & _
                            "      End"




        '"	-- sysobjects.name AS table_name, syscolumns.name AS column_name, " & _
        '"	-- " & _
        '"	--           systypes.name AS datatype, syscolumns.LENGTH AS LENGTH " & _

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, cs)
            ds.Dispose()

            CargarExcel2 = True

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Verifique los siguiente puntos: " & vbCrLf & vbCrLf & _
              "1) El archivo Excel debe estar cerrado" & vbCrLf & _
              "2) El archivo de Excel debe tener una hoja que se llama ""Lista de Precios""" & vbCrLf & _
              "3) Cumple con los requisitos de nombres en cada columna. " & vbCrLf & _
              " Puede consultar el ejemplo haciendo clic en el ícono que está a la derecha de ""Importar Productos desde Excel""." & vbCrLf & vbCrLf & _
              " Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

            CargarExcel2 = False

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    'Private Sub LlenarcmbPlazo()
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim ds As Data.DataSet

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try

    '        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Distinct PlazoEntrega FROM Materiales ORDER BY PlazoEntrega")
    '        ds.Dispose()

    '        With cmbPlazo
    '            .DataSource = ds.Tables(0).DefaultView
    '            .DisplayMember = "PlazoEntrega"
    '            '.ValueMember = "IdUsuario"
    '        End With

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

#Region "   Funciones"

    Private Function AgregarActualizar_Registro() As Integer

        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        'Abrir una transaccion para guardar y asegurar que se guarda todo
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

            Dim param_idalmacen As New SqlClient.SqlParameter
            param_idalmacen.ParameterName = "@idalmacen"
            param_idalmacen.SqlDbType = SqlDbType.BigInt
            param_idalmacen.Value = IIf(txtidAlmacen.Text = "", cmbAlmacenes.SelectedValue, txtidAlmacen.Text)
            param_idalmacen.Direction = ParameterDirection.Input

            Dim param_idmarca As New SqlClient.SqlParameter
            param_idmarca.ParameterName = "@idmarca"
            param_idmarca.SqlDbType = SqlDbType.BigInt
            param_idmarca.Value = txtIdMarca.Text
            param_idmarca.Direction = ParameterDirection.Input

            Dim param_idfamilia As New SqlClient.SqlParameter
            param_idfamilia.ParameterName = "@idfamilia"
            param_idfamilia.SqlDbType = SqlDbType.BigInt
            param_idfamilia.Value = txtIdRubro.Text
            param_idfamilia.Direction = ParameterDirection.Input

            Dim param_idSubrubro As New SqlClient.SqlParameter
            param_idSubrubro.ParameterName = "@idSUBRUBRO"
            param_idSubrubro.SqlDbType = SqlDbType.BigInt
            param_idSubrubro.Value = txtIdSubrubro.Text
            param_idSubrubro.Direction = ParameterDirection.Input

            Dim param_idunidad As New SqlClient.SqlParameter
            param_idunidad.ParameterName = "@idunidad"
            param_idunidad.SqlDbType = SqlDbType.BigInt
            param_idunidad.Value = txtIdUnidadVta.Text
            param_idunidad.Direction = ParameterDirection.Input

            Dim param_idmoneda As New SqlClient.SqlParameter
            param_idmoneda.ParameterName = "@idmoneda"
            param_idmoneda.SqlDbType = SqlDbType.BigInt
            param_idmoneda.Value = txtIdMonedaVta.Text
            param_idmoneda.Direction = ParameterDirection.Input

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = txtCODIGO.Text
            param_codigo.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 255
            param_nombre.Value = RTrim(LTrim(txtNOMBRE.Text))
            param_nombre.Direction = ParameterDirection.Input

            Dim param_ganancia As New SqlClient.SqlParameter
            param_ganancia.ParameterName = "@ganancia"
            param_ganancia.SqlDbType = SqlDbType.Decimal
            param_ganancia.Precision = 18
            param_ganancia.Scale = 2
            param_ganancia.Value = IIf(txtganancia.Text = "", 0, txtganancia.Text)
            param_ganancia.Direction = ParameterDirection.Input

            Dim param_preciovtasiniva As New SqlClient.SqlParameter
            param_preciovtasiniva.ParameterName = "@preciovtasiniva"
            param_preciovtasiniva.SqlDbType = SqlDbType.Decimal
            param_preciovtasiniva.Precision = 18
            param_preciovtasiniva.Scale = 2
            param_preciovtasiniva.Value = txtPrecioVtaBulto.Text
            param_preciovtasiniva.Direction = ParameterDirection.Input

            Dim param_minimo As New SqlClient.SqlParameter
            param_minimo.ParameterName = "@minimo"
            param_minimo.SqlDbType = SqlDbType.Decimal
            param_minimo.Precision = 18
            param_minimo.Scale = 2
            param_minimo.Value = CType(txtMinimo.Text, Long)
            param_minimo.Direction = ParameterDirection.Input

            Dim param_maximo As New SqlClient.SqlParameter
            param_maximo.ParameterName = "@maximo"
            param_maximo.SqlDbType = SqlDbType.Decimal
            param_maximo.Precision = 18
            param_maximo.Scale = 4
            param_maximo.Value = CType(txtMaximo.Text, Long)
            param_maximo.Direction = ParameterDirection.Input

            Dim param_stockinicial As New SqlClient.SqlParameter
            If bolModo = True Then
                param_stockinicial.ParameterName = "@stockinicial"
                param_stockinicial.SqlDbType = SqlDbType.Decimal
                param_stockinicial.Precision = 18
                param_stockinicial.Scale = 2
                param_stockinicial.Value = IIf(txtStockInicio.Text = "", 0, txtStockInicio.Text)
                param_stockinicial.Direction = ParameterDirection.Input
            End If

            Dim param_plazo As New SqlClient.SqlParameter
            param_plazo.ParameterName = "@plazoentrega"
            param_plazo.SqlDbType = SqlDbType.VarChar
            param_plazo.Size = 50
            param_plazo.Value = "" 'cmbPlazo.Text.ToString
            param_plazo.Direction = ParameterDirection.Input


            Dim param_idProveedor As New SqlClient.SqlParameter
            param_idProveedor.ParameterName = "@idProveedor"
            param_idProveedor.SqlDbType = SqlDbType.BigInt
            param_idProveedor.Value = txtIdProveedor.Text
            param_idProveedor.Direction = ParameterDirection.Input

            Dim param_idunidadcompra As New SqlClient.SqlParameter
            param_idunidadcompra.ParameterName = "@idunidadcompra"
            param_idunidadcompra.SqlDbType = SqlDbType.BigInt
            param_idunidadcompra.Value = txtIdUnidadCompra.Text
            param_idunidadcompra.Direction = ParameterDirection.Input

            Dim param_idmonedacompra As New SqlClient.SqlParameter
            param_idmonedacompra.ParameterName = "@idmonedacompra"
            param_idmonedacompra.SqlDbType = SqlDbType.BigInt
            param_idmonedacompra.Value = txtIdMonedaCompra.Text
            param_idmonedacompra.Direction = ParameterDirection.Input

            Dim param_bonificacion1 As New SqlClient.SqlParameter
            param_bonificacion1.ParameterName = "@bonif1"
            param_bonificacion1.SqlDbType = SqlDbType.Decimal
            param_bonificacion1.Precision = 18
            param_bonificacion1.Scale = 2
            param_bonificacion1.Value = IIf(txtBonif1.Text = "", 0, txtBonif1.Text)
            param_bonificacion1.Direction = ParameterDirection.Input

            Dim param_bonificacion2 As New SqlClient.SqlParameter
            param_bonificacion2.ParameterName = "@bonif2"
            param_bonificacion2.SqlDbType = SqlDbType.Decimal
            param_bonificacion2.Precision = 18
            param_bonificacion2.Scale = 2
            param_bonificacion2.Value = IIf(txtBonif2.Text = "", 0, txtBonif2.Text)
            param_bonificacion2.Direction = ParameterDirection.Input

            Dim param_bonificacion3 As New SqlClient.SqlParameter
            param_bonificacion3.ParameterName = "@bonif3"
            param_bonificacion3.SqlDbType = SqlDbType.Decimal
            param_bonificacion3.Precision = 18
            param_bonificacion3.Scale = 2
            param_bonificacion3.Value = IIf(txtBonif3.Text = "", 0, txtBonif3.Text)
            param_bonificacion3.Direction = ParameterDirection.Input

            Dim param_bonificacion4 As New SqlClient.SqlParameter
            param_bonificacion4.ParameterName = "@bonif4"
            param_bonificacion4.SqlDbType = SqlDbType.Decimal
            param_bonificacion4.Precision = 18
            param_bonificacion4.Scale = 2
            param_bonificacion4.Value = IIf(txtBonif4.Text = "", 0, txtBonif4.Text)
            param_bonificacion4.Direction = ParameterDirection.Input

            Dim param_bonificacion5 As New SqlClient.SqlParameter
            param_bonificacion5.ParameterName = "@bonif5"
            param_bonificacion5.SqlDbType = SqlDbType.Decimal
            param_bonificacion5.Precision = 18
            param_bonificacion5.Scale = 2
            param_bonificacion5.Value = IIf(txtBonif5.Text = "", 0, txtBonif5.Text)
            param_bonificacion5.Direction = ParameterDirection.Input

            Dim param_preciolista As New SqlClient.SqlParameter
            param_preciolista.ParameterName = "@preciolista"
            param_preciolista.SqlDbType = SqlDbType.Decimal
            param_preciolista.Precision = 18
            param_preciolista.Scale = 2
            param_preciolista.Value = txtPrecioLista.Text
            param_preciolista.Direction = ParameterDirection.Input

            Dim param_iva As New SqlClient.SqlParameter
            param_iva.ParameterName = "@IVA"
            param_iva.SqlDbType = SqlDbType.Decimal
            param_iva.Precision = 18
            param_iva.Scale = 2
            param_iva.Value = IIf(txtIva.Text = "", 0, txtIva.Text)
            param_iva.Direction = ParameterDirection.Input

            Dim param_cantxbulto As New SqlClient.SqlParameter
            param_cantxbulto.ParameterName = "@cantxbulto"
            param_cantxbulto.SqlDbType = SqlDbType.Decimal
            param_cantxbulto.Precision = 18
            param_cantxbulto.Scale = 2
            param_cantxbulto.Value = IIf(txtCantxBulto.Text = "", 1, txtCantxBulto.Text)
            param_cantxbulto.Direction = ParameterDirection.Input

            Dim param_preciovtaxbulto As New SqlClient.SqlParameter
            param_preciovtaxbulto.ParameterName = "@PrecioVentaxBulto"
            param_preciovtaxbulto.SqlDbType = SqlDbType.Decimal
            param_preciovtaxbulto.Precision = 18
            param_preciovtaxbulto.Scale = 2
            param_preciovtaxbulto.Value = txtPrecioVtaBulto.Text
            param_preciovtaxbulto.Direction = ParameterDirection.Input

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_1_Insert", _
                                    param_id, param_idalmacen, param_idmarca, param_idfamilia, param_idSubrubro, param_idunidad, _
                                    param_idmoneda, param_codigo, param_nombre, param_ganancia, param_preciovtasiniva, _
                                    param_minimo, param_maximo, param_stockinicial, param_plazo, _
                                    param_idProveedor, param_idunidadcompra, param_idmonedacompra, _
                                    param_bonificacion1, param_bonificacion2, param_bonificacion3, param_bonificacion4, param_bonificacion5, _
                                    param_preciolista, param_iva, param_cantxbulto, param_preciovtaxbulto, _
                                    param_useradd, param_res)

                    txtID.Text = param_id.Value
                    res = param_res.Value

                Else

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_1_Update", _
                                    param_id, param_idalmacen, param_idmarca, param_idfamilia, param_idSubrubro, param_idunidad, _
                                    param_idmoneda, param_codigo, param_nombre, param_ganancia, param_preciovtasiniva, _
                                    param_minimo, param_maximo, param_plazo, _
                                    param_idProveedor, param_idunidadcompra, param_idmonedacompra, _
                                    param_bonificacion1, param_bonificacion2, param_bonificacion3, param_bonificacion4, param_bonificacion5, _
                                    param_preciolista, param_iva, param_cantxbulto, param_preciovtaxbulto, _
                                    param_useradd, param_res)

                    res = param_res.Value

                End If

                AgregarActualizar_Registro = res

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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Delete", param_id, _
                                          param_userdel, param_res)
                res = param_res.Value

                If res > 0 Then Util.BorrarGrilla(grd)

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

    Private Function ImportarRegistros() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.Output

            Dim param_cantNuevo As New SqlClient.SqlParameter
            param_cantNuevo.ParameterName = "@CantNuevo"
            param_cantNuevo.SqlDbType = SqlDbType.BigInt
            param_cantNuevo.Value = DBNull.Value
            param_cantNuevo.Direction = ParameterDirection.Output

            Dim param_cantAct As New SqlClient.SqlParameter
            param_cantAct.ParameterName = "@CantAct"
            param_cantAct.SqlDbType = SqlDbType.BigInt
            param_cantAct.Value = DBNull.Value
            param_cantAct.Direction = ParameterDirection.Output

            Dim param_mensaje As New SqlClient.SqlParameter
            param_mensaje.ParameterName = "@Mensaje"
            param_mensaje.SqlDbType = SqlDbType.VarChar
            param_mensaje.Size = 200
            param_mensaje.Value = DBNull.Value
            param_mensaje.Direction = ParameterDirection.Output

            Try

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Importar", param_res, _
                                          param_cantNuevo, param_cantAct, param_mensaje)

                'MsgBox(param_mensaje.Value)

                ImportarRegistros = param_res.Value

                CantRegistrosImportados = param_cantNuevo.Value

                CantRegistrosActualizados = param_cantAct.Value

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



#End Region

#Region "   Botones"

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If txtID.Text <> "" Then

            btnCancelar.Text = "Cancelar"

            CargaContinua = False

        End If
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        If CargaContinua = False Then
            Util.LimpiarTextBox(Me.Controls)
        Else
            txtID.Text = ""
            txtCODIGO.Text = ""
        End If

        cmbBusqueda.Text = MDIPrincipal.UltBusquedaMat

        'llenandoCombo = True
        'LlenarcmbRubros()
        Label20.Enabled = True
        txtStockInicio.Enabled = True

        Label12.Enabled = True
        cmbAlmacenes.Enabled = True

        lblStockActual.Text = "0"
        lblPrecioIva10.Text = "0"
        lblPrecioIva21.Text = "0"

        txtCODIGO.Focus()
        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If bolModo = True Then
            If ControlarNombreProducto(txtNOMBRE.Text, ConnStringSEI) >= 1 Then
                Util.MsgStatus(Status1, "El producto " & txtNOMBRE.Text & " ya existe. Por favor, verifique la carga que está realizando", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El producto " & txtNOMBRE.Text & " ya existe. Por favor, verifique la carga que está realizando", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If
        End If

        If bolModo = False Then
            If MessageBox.Show("Está seguro que desea modificar el Producto seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El código ingresado ya existe, por favor, cambie el código", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El código ingresado ya existe, por favor, cambie el código", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtNOMBRE.Focus()
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case Else
                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)

                        Cerrar_Tran()
                        If bolModo = True Then
                            Util.MsgStatus(Status1, "Se insertó correctamente la Materia Prima y sus Proveedores.", My.Resources.Resources.ok.ToBitmap)
                        Else
                            Util.MsgStatus(Status1, "Se actualizó correctamente la Materia Prima y sus Proveedores", My.Resources.Resources.ok.ToBitmap)
                        End If
                        bolModo = False
                        band = 0

                        btnActualizar_Click(sender, e)

                        band = 1

                        If chkNombre.Checked = True Then
                            'If band = 1 Then
                            If cmbBusqueda.Text <> "" Then
                                QuitarElFitroToolStripMenuItem_Click(sender, e)
                                ColumnName = "Nombre"
                                ColumnType = "system.string"
                                Filtrarpor(cmbBusqueda.Text, "Materiales")
                                'If grd.RowCount > 0 Then
                                '    cmbFAMILIAS.SelectedValue = grd.CurrentRow.Cells(8).Value
                                '    cmbSubRubro.SelectedValue = grd.CurrentRow.Cells(10).Value
                                '    cmbMonedas.SelectedValue = grd.CurrentRow.Cells(3).Value
                                '    cmbUNIDADES.SelectedValue = grd.CurrentRow.Cells(23).Value
                                'End If
                            End If
                            'End If
                        End If

                        If grd.RowCount > 0 Then
                            cmbMarcas.SelectedValue = grd.CurrentRow.Cells(1).Value
                            cmbFAMILIAS.SelectedValue = grd.CurrentRow.Cells(4).Value
                            cmbSubRubro.SelectedValue = grd.CurrentRow.Cells(6).Value
                            cmbMonedaVta.SelectedValue = grd.CurrentRow.Cells(9).Value
                            cmbUnidadVta.SelectedValue = grd.CurrentRow.Cells(11).Value
                            cmbProveedores.SelectedValue = grd.CurrentRow.Cells(19).Value
                            cmbMonedaCompra.SelectedValue = grd.CurrentRow.Cells(27).Value
                            cmbUnidadCompra.SelectedValue = grd.CurrentRow.Cells(29).Value
                        End If

                End Select
            End If
        End If
    End Sub

    Private Sub btnCargaContinua_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargaContinua.Click
        If btnNuevo.Enabled = False And btnCancelar.Text = "Cancelar" Then
            MsgBox("No se puede Guardar cuando está en Modo Nuevo", MsgBoxStyle.Critical, "Control de Productos")
            Exit Sub
        End If
        CargaContinua = True
        btnCancelar.Text = "Cancelar/Terminar"
        btnGuardar_Click(sender, e)
        btnNuevo_Click(sender, e)
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer
        'If BAJA_FISICA Then

        If MessageBox.Show("Está seguro que desea eliminar el producto: " & grd.CurrentRow.Cells(6).Value & " ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

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

        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim filtro As String
        Dim rpt As New frmReportes

        nbreformreportes = "Productos"

        Dim consulta As String = "select busqueda from CriteriosdeBusquedas ORDER BY Busqueda"

        param.AgregarParametros("Filtro :", "STRING", "", False, "", consulta, cnn)

        param.ShowDialog()

        If cerroparametrosconaceptar = True Then
            filtro = param.ObtenerParametros(0)

            rpt.Materiales_App(filtro, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Sub btnGuardarCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarCriterio.Click
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try

                Dim param_busqueda As New SqlClient.SqlParameter
                param_busqueda.ParameterName = "@texto"
                param_busqueda.SqlDbType = SqlDbType.VarChar
                param_busqueda.Size = 200
                param_busqueda.Value = cmbBusqueda.Text
                param_busqueda.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCriteriosdeBusqueda_Insert", _
                                              param_busqueda)

                    Util.MsgStatus(Status1, "Se guardó correctamente el criterio", My.Resources.Resources.ok.ToBitmap)

                    LlenarcmbBusqueda()

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

    Private Sub btnEliminarCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCriterio.Click
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try

                Dim param_busqueda As New SqlClient.SqlParameter
                param_busqueda.ParameterName = "@texto"
                param_busqueda.SqlDbType = SqlDbType.VarChar
                param_busqueda.Size = 200
                param_busqueda.Value = cmbBusqueda.Text
                param_busqueda.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCriteriosdeBusqueda_Delete", _
                                              param_busqueda)

                    Util.MsgStatus(Status1, "Se eliminó correctamente el criterio", My.Resources.Resources.ok.ToBitmap)

                    LlenarcmbBusqueda()

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

    Protected Overridable Sub btnImportarExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportarExcel.Click

        If MessageBox.Show("Recuerde que el nombre de la hoja donde están los datos debe llamarse Lista de Precios. Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim FileName As String

        Try
            With OpenFileDialog1
                .Filter = "Archivos Excel (*.xls)|*.xls|" & "Todos los archivos|*.*"
            End With
        Catch es As Exception
            MessageBox.Show(es.Message)
        End Try

        OpenFileDialog1.ShowDialog()

        FileName = OpenFileDialog1.FileName

        'CargardeExcel(grd, FileName, "HOJA1")
        Util.MsgStatus(Status1, "Procesando archivo...", My.Resources.Resources.indicator_white)

        Me.Refresh()

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If CargarExcel2(FileName, "[Lista de precios$]") = False Then
            Cursor = Cursors.Default
            Exit Sub
        End If

        Dim res As Integer

        res = ImportarRegistros()
        Select Case res
            Case 0
                Util.MsgStatus(Status1, "Se produjo un error al intentar importar el excel", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Se produjo un error al intentar importar el excel", My.Resources.Resources.stop_error.ToBitmap, True)
            Case Else

                SQL = "exec spMateriales_1_Select_All @Eliminado = 0"

                Me.LlenarcmbBusqueda()
                'Me.LlenarcmbPlazo()
                Me.LlenarcmbRubros()
                Me.LlenarcmbSubRubros()

                bolModo = False

                btnActualizar_Click(sender, e)

                MsgBox("Se importaron " & CantRegistrosImportados & " y se actualizaron " & CantRegistrosActualizados & "  registros.", MsgBoxStyle.Information, "Importación Correcta")

        End Select

        Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Protected Overridable Sub btnImagenExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImagenExcel.Click
        frmMateriales_ImagenExcel.Show()
    End Sub

    Private Sub btnActivarProductoEliminado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivarProductoEliminado.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente el producto: " & grd.CurrentRow.Cells(6).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Materiales SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            MsgBox("El producto se activó correctamente.")

            SQL = "exec spMateriales_1_Select_All @Eliminado = 1"

            LlenarGrilla()

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


    Private Sub CalcularPrecioFinal()

        Dim Bonif1 As Double, Bonif2 As Double, Bonif3 As Double, Bonif4 As Double, Bonif5 As Double
        'Dim precioxkg As Double, cantxlongitud As Double, pesoxunidad As Double,  precioxmt As Double
        Dim preciolista As Double, iva As Double, ganancia As Double
        Dim preciobonif1 As Double = 0, preciobonif2 As Double = 0, preciobonif3 As Double = 0, preciobonif4 As Double = 0, preciobonif5 As Double = 0
        Dim preciosinivabonif As Double = 0
       
        If txtBonif1.Text = "" Then
            Bonif1 = 1
        Else
            If CDbl(txtBonif1.Text) = 0 Then
                Bonif1 = 1
            Else
                Bonif1 = 1 - (CType(txtBonif1.Text, Double)) / 100
            End If
        End If

        If txtBonif2.Text = "" Then
            Bonif2 = 1
        Else
            If CDbl(txtBonif2.Text) = 0 Then
                Bonif2 = 1
            Else
                Bonif2 = 1 - (CType(txtBonif2.Text, Double)) / 100
            End If
        End If

        If txtBonif3.Text = "" Then
            Bonif3 = 1
        Else
            If CDbl(txtBonif3.Text) = 0 Then
                Bonif3 = 1
            Else
                Bonif3 = 1 - (CType(txtBonif3.Text, Double)) / 100
            End If
        End If

        If txtBonif4.Text = "" Then
            Bonif4 = 1
        Else
            If CDbl(txtBonif4.Text) = 0 Then
                Bonif4 = 1
            Else
                Bonif4 = 1 - (CType(txtBonif4.Text, Double)) / 100
            End If
        End If

        If txtBonif5.Text = "" Then
            Bonif5 = 1
        Else
            If CDbl(txtBonif5.Text) = 0 Then
                Bonif5 = 1
            Else
                Bonif5 = 1 - (CType(txtBonif5.Text, Double)) / 100
            End If
        End If

        preciolista = IIf(txtPrecioLista.Text = "", 0, txtPrecioLista.Text)

        preciobonif1 = preciolista * Bonif1
        preciobonif1 = preciobonif1 * Bonif2
        preciobonif1 = preciobonif1 * Bonif3
        preciobonif1 = preciobonif1 * Bonif4
        preciobonif1 = preciobonif1 * Bonif5

        If txtIva.Text = "" Then
            iva = 1
        Else
            If CDbl(txtIva.Text) = 0 Then
                iva = 1
            Else
                iva = 1 + CDbl(txtIva.Text) / 100
            End If
        End If

        If txtganancia.Text = "" Then
            ganancia = 1
        Else
            If CDbl(txtganancia.Text) = 0 Then
                ganancia = 1
            Else
                ganancia = 1 + CDbl(txtganancia.Text) / 100
            End If
        End If

        preciosinivabonif = preciobonif1 * iva

        txtPrecioVtaBulto.Text = Math.Round(preciosinivabonif * ganancia, 2)

        If txtCantxBulto.Text = "" Then
            txtPrecioVta.Text = txtPrecioVtaBulto.Text
        Else
            If CDbl(txtCantxBulto.Text) = 0 Then
                txtPrecioVta.Text = txtPrecioVtaBulto.Text
            Else
                txtPrecioVta.Text = Math.Round(txtPrecioVtaBulto.Text / CDbl(txtCantxBulto.Text), 2)
            End If
        End If

        lblPrecioIva10.Text = FormatNumber(CDbl(txtPrecioVtaBulto.Text) * 1.105, 2)
        lblPrecioIva21.Text = FormatNumber(CDbl(txtPrecioVtaBulto.Text) * 1.21, 2)


    End Sub

    Private Sub LlenarcmbMarca()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet


        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select Id, Nombre From Marcas ORDER BY Nombre")
            ds.Dispose()

            With cmbMarcas
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Id"
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

    Private Sub cmbProveedores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProveedores.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdProveedor.Text = cmbProveedores.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub
    
    Private Sub cmbMarcas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMarcas.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdMarca.Text = cmbMarcas.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbMonedaCompra_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMonedaCompra.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdMonedaCompra.Text = cmbMonedaCompra.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbMonedaVta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMonedaVta.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdMonedaVta.Text = cmbMonedaVta.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbUnidadCompra_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnidadCompra.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdUnidadCompra.Text = cmbUnidadCompra.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbUnidadVta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnidadVta.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdUnidadVta.Text = cmbUnidadVta.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

End Class