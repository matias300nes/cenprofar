
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles.Util
Imports Utiles
Imports Utiles.compartida
Imports ReportesNet


Public Class frmCierredeObras

#Region "Declaracion de Variables"
    Dim dtnew As New System.Data.DataTable

    Dim RegistrosPorPagina As Integer = 20
    Dim ini As Integer = 0
    Dim fin As Integer = RegistrosPorPagina - 1
    Dim TotalPaginas As Integer
    Dim PaginaActual As Integer
    'Dim bolPaginar As Boolean = False

    Dim bolpoliticas As Boolean, inicio As Boolean

    Private ds_2 As DataSet
    Dim Permitir2 As Boolean = False

    Public Shadows Event ev_CellChanged As EventHandler

#End Region

#Region "Formulario y Componentes"

    Private Sub frmCierredeObras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()
        'asignarTags()

        SQL = "exec spCierreObras_Select_All"
        inicio = True
        LlenarGrilla()

        Permitir = True
        CargarCajas()

        PrepararBotones()

        inicio = False
    End Sub

    Private Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            CargarCajas()
        End If
        RaiseEvent ev_CellChanged(sender, e)
    End Sub


#End Region

#Region "Botones"

    Private Sub btnCerrarObra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrarObra.Click
        If lblDeuda.Text = "0" Then
            If MessageBox.Show("Esta acción cierra todas las operaciones para la obra seleccionada. " + vbCrLf + "¿Está seguro que desea continuar?", "Control de Obras", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Cerrando Obra...", My.Resources.Resources.indicator_white)
                Select Case ActualizarRegistro()
                    Case -1
                        Util.MsgStatus(Status1, "La Obra no ha podido ser Cerrada", My.Resources.Resources.stop_error.ToBitmap)
                        Exit Sub
                    Case 0
                        Util.MsgStatus(Status1, "La Obra no ha podido ser Cerrada, error en transacción", My.Resources.Resources.stop_error.ToBitmap)
                        Exit Sub
                    Case Else
                        Util.MsgStatus(Status1, "La Obra ha sido Cerrada", My.Resources.Resources.ok.ToBitmap)
                        btnActualizar_Click(sender, e)
                End Select
            End If
        Else
            MsgBox("Para Cerrar la Obra, la deuda debería ser 0 (cero).", MsgBoxStyle.Exclamation, "Control de Obras")
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramcierreObra As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim reporteCierreObra As New frmReportes

        nbreformreportes = "Comprobante de Pago"

        'reporteCierreObra.MostrarCierredeObra(grd.CurrentRow.Cells(0).Value.ToString, grd.CurrentRow.Cells(0).Value.ToString & " - " & grd.CurrentRow.Cells(1).Value.ToString, reporteCierreObra)
        cerroparametrosconaceptar = False
        reporteCierreObra = Nothing
        cnn = Nothing

    End Sub

#End Region

#Region "Funciones"

    Private Function ActualizarRegistro() As Integer
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
            param_id.ParameterName = "@Id"
            param_id.SqlDbType = SqlDbType.Int
            param_id.Value = grd.CurrentRow.Cells(0).Value
            param_id.Direction = ParameterDirection.Input

            Dim param_userupd As New SqlClient.SqlParameter
            param_userupd.ParameterName = "@userupd"
            param_userupd.SqlDbType = SqlDbType.SmallInt
            param_userupd.Value = UserID
            param_userupd.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCierreObras_Update", param_id, _
                            param_userupd, param_res)

                res = param_res.Value
                ActualizarRegistro = res

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
        Me.Text = "Cierre de Obras"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 450) '200'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Overloads Sub CargarCajas()
        If Not grd.CurrentRow Is Nothing Then
            If grd.CurrentRow.Cells(6).Value = "Abierta" Then
                btnCerrarObra.Enabled = True
                lblTitulo.Visible = False
            Else
                btnCerrarObra.Enabled = False
                lblTitulo.Visible = True
            End If
            LlenarGridItems(1)
            LlenarGridItems(2)
            'LlenarGridItems(3)
        End If
    End Sub

#End Region

#Region "Grilla Items"

    Private Sub LlenarGridItems(ByVal a As Integer)
        Dim i As Integer = 0
        Dim b As Double, c As Double, d As Double

        Permitir2 = False

        Select Case a
            Case 1

                SQL = " SELECT * FROM (select id as NroMov, 'Consumo' as TipoMov, convert(varchar(10), fecha, 103) as Fecha, " + _
                   " 'ACER' as Proveedor,Id as 'Remito/Factura', cast(MONTOIVA as decimal(18,2)) AS IVA, " + _
                   " CAST(totalconsumo AS DECIMAL(18,2)) as TOTAL, IdIngreso as NroIngreso from consumos c " + _
                   " LEFT JOIN ingresos_consumos ic ON ic.idmovimiento = c.id and tipomov = 'Consumo' " + _
                   " where idobra = " & grd.CurrentRow.Cells(0).Value + _
                    " UNION ALL " + _
                    " SELECT IdGasto as NroMov, 'Gasto' as TipoMov, convert(varchar(10), FechaGasto,103) as Fecha, " + _
                    " Proveedor, nroFactura  as 'Remito/Factura', IVA, CAST(total AS DECIMAL(18,2)) as Total, IdIngreso as NroIngreso " + _
                    " FROM Gastos g LEFT JOIN ingresos_consumos ic ON ic.idmovimiento = g.idgasto and tipomov = 'Gasto' " + _
                    " WHERE IDOBRA = " & grd.CurrentRow.Cells(0).Value & ") AS Z order by fecha"

                GetDatasetItems(grdItems)
                paginar_items(ds_2.Tables(0), grdItems) 'Asignar los datos paginados a la grilla
                If inicio = True Then
                    InicializarGridItems(grdItems)
                End If

                For i = 0 To grdItems.Rows.Count - 1
                    If grdItems.Rows(i).Cells(1).Value.ToString = "Consumo" Then
                        b = b + grdItems.Rows(i).Cells(6).Value
                    Else
                        c = c + grdItems.Rows(i).Cells(6).Value
                    End If

                Next

                lblConsumos.Text = b
                lblGastos.Text = c

            Case 2
                SQL = "SELECT IdIngreso as NroIngreso, Intereses, Retensiones, Total, Entregado, Deuda FROM INGRESOS where obra = '" & grd.CurrentRow.Cells(0).Value & " - " & grd.CurrentRow.Cells(1).Value & "'"
                GetDatasetItems(grdItems1)
                paginar_items(ds_2.Tables(0), grdItems1) 'Asignar los datos paginados a la grilla
                If inicio = True Then
                    InicializarGridItems(grdItems1)
                End If

                For i = 0 To grdItems1.Rows.Count - 1
                    c = c + grdItems1.Rows(i).Cells(1).Value
                    d = d + grdItems1.Rows(i).Cells(2).Value
                    b = b + grdItems1.Rows(i).Cells(4).Value
                Next

                lblTotalIngresos.Text = b
                lblIntereses.Text = c
                lblRetensiones.Text = d

                lblTotalObra.Text = CDbl(IIf(lblConsumos.Text = "", 0, lblConsumos.Text)) + CDbl(IIf(lblGastos.Text = "", 0, lblGastos.Text))
                lblDeuda.Text = CDbl(IIf(lblTotalObra.Text = "", 0, lblTotalObra.Text)) - CDbl(IIf(lblTotalIngresos.Text = "", 0, lblTotalIngresos.Text)) + CDbl(IIf(lblIntereses.Text = "", 0, lblIntereses.Text)) - CDbl(IIf(lblRetensiones.Text = "", 0, lblRetensiones.Text))

        End Select

        Permitir2 = True

        SQL = "exec spCierreObras_Select_All"

    End Sub

    Private Sub GetDatasetItems(ByVal GRD As DataGridView)
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se puede establecer la conexión", "Error de Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()

            GRD.DataSource = ds_2.Tables(0).DefaultView

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
             + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
             "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub InicializarGridItems(ByVal Grd As DataGridView)

        Dim style As New DataGridViewCellStyle
        Grd.EnableHeadersVisualStyles = False

        'da formato al encabezado...
        With Grd.ColumnHeadersDefaultCellStyle
            .BackColor = Color.CadetBlue
            .ForeColor = Color.Purple
            .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        ' Inicialice propiedades básicas.
        With Grd
            '.Dock = DockStyle.Fill ' lo coloca al tope del formulario..
            .BackgroundColor = SystemColors.ActiveBorder 'Color.DarkGray ' color del fondo del grid...
            .BorderStyle = BorderStyle.Fixed3D
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
            .AllowUserToAddRows = False 'indica si se muestra al usuario la opción de agregar filas
            .AllowUserToDeleteRows = False 'indica si el usuario puede eliminar filas de DataGridView.
            .AllowUserToOrderColumns = False 'indica si el usuario puede cambiar manualmente de lugar las columnas..
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect 'indica cómo se pueden seleccionar las celdas de DataGridView.
            '.MultiSelect = False 'indica si el usuario puede seleccionar a la vez varias celdas, filas o columnas del control DataGridView.
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders   'indica cómo se determina el alto de las filas. 
            .AllowUserToResizeColumns = True 'indica si los usuarios pueden cambiar el tamaño de las columnas.
            .AllowUserToResizeRows = False 'indica si los usuarios pueden cambiar el tamaño de las filas.
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize 'indica si el alto de los encabezados de columna es ajustable y si puede ser ajustado por el usuario o automáticamente para adaptarse al contenido de los encabezados. 
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        'Setear el color de seleccion de fondo de la celda actual...
        Grd.DefaultCellStyle.SelectionBackColor = Color.White
        Grd.DefaultCellStyle.SelectionForeColor = Color.Blue

        'generamos el formato para las celdas...
        With style
            .BackColor = Color.Lavender   'Color.LightGray
            .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .ForeColor = Color.Black
        End With
        Grd.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Aplicamos el estilo a todas las celdas del control DataGridView
        Grd.RowsDefaultCellStyle = style



    End Sub

    Protected Sub SetColumnasGrillaiTEMS(ByVal grd As DataGridView)

        grd.Columns.Clear()

        For Each dc As DataColumn In dtnew.Columns
            Select Case dc.DataType.Name.ToUpper
                Case "BOOLEAN"
                    Dim Column = New DataGridViewCheckBoxColumn()
                    Column.DataPropertyName = dc.ColumnName
                    Column.HeaderText = dc.ColumnName
                    Column.Name = dc.ColumnName
                    Column.SortMode = DataGridViewColumnSortMode.Automatic
                    Column.ValueType = dc.DataType
                    If dc.ColumnName.ToLower = "eliminado" Then
                        Column.Visible = False
                    End If
                    grd.Columns.Add(Column)
                Case Else
                    Dim Column = New DataGridViewTextBoxColumn()
                    Column.DataPropertyName = dc.ColumnName
                    Column.HeaderText = dc.ColumnName
                    Column.Name = dc.ColumnName
                    Column.SortMode = DataGridViewColumnSortMode.Automatic
                    Column.ValueType = dc.DataType
                    grd.Columns.Add(Column)
            End Select
        Next

    End Sub

    Private Sub paginar_items(ByVal d As DataTable, ByVal grd As DataGridView)

        Dim o(1) As String
        Dim TEMP As String = ""
        Static Flag As Boolean = False
        TEMP = ""

        dtnew = d.Clone 'paginarDataDridView(d, ini, fin)

        ReDim o(dtnew.Columns.Count * dtnew.Rows.Count)

        'grdItems.Rows.Clear()
        If Not Flag Then
            SetColumnasGrillaiTEMS(grd)
            Flag = True
        End If

        'For Each row As DataRow In ds.Tables(0).Rows
        For Each row As DataRow In dtnew.Rows
            'nuevo
            For i As Integer = 0 To grdItems.Columns.Count - 1
                Select Case grdItems.Columns(i).ValueType.Name.ToUpper
                    Case "DECIMAL"
                        If row.Item(i).ToString <> "" Then 'And Not (IsDBNull(row.Item(i).ToString)) Then
                            TEMP = CType(row.Item(i).ToString, Decimal).ToString("0.00")
                        End If

                    Case "DATETIME"
                        If row.Item(i).ToString <> "" And Not (IsDBNull(row.Item(i).ToString)) Then
                            TEMP = CType(row.Item(i).ToString, Date).ToString("dd/MM/yyyy")
                        End If

                    Case Else
                        TEMP = row.Item(i).ToString
                End Select
                o(i) = TEMP
            Next i
            'fin nuevo
            grdItems.Rows.Add(o)
        Next

        'grdItems.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        dtnew.Dispose()

    End Sub

#End Region

End Class