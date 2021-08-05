Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

'Reference to Microsoft.Office.Interop.Excel

Public Class frmBase

#Region "   DECLARACIONES"


    Protected AnchoMinimoForm As Integer = 616
    Protected AltoMinimoGrilla As Integer = 250
    Protected bolModo As Boolean        'Modo de Actualizacion
    Protected Permitir As Boolean
    Protected ds As DataSet             'Dataset de uso general
    Protected SQL As String = ""            'Consulta SQL para el formulario
    Protected RegAfectados As Integer
    Protected CellX As Integer
    Protected CellY As Integer
    'Public bandCellY As Boolean
    Protected ColumnName As String = ""
    Protected ColumnType As String = ""
    Protected FilterValue As String = ""
    Protected Filter As String = ""
    'Dim excelApp As Microsoft.Office.Interop.Excel.Application
    'Dim workBook As Microsoft.Office.Interop.Excel.Workbook

    ''Protected Const SQL_CONNECTION_STRING As String = _
    ''   "Server=desarrollo04\test;user id=sa;password=avestruz;DataBase=TM3;"
    ''& _
    ''      "Integrated Security=SSPI;Connect Timeout=5"

    ''Public Const servidor As String = "desarrollo04\test"
    ''Public Const bd As String = "TM3"
    ''Public Const usuario As String = "sa"
    ''Public Const contrasena As String = "avestruz"

    'Protected Const SQL_CONNECTION_STRING As String = _
    '    "Server=CPONTE\TEST;user id=sa;password=avestruz;" & _
    '    "DataBase=RRHH;" & _
    '    "Integrated Security=SSPI;Connect Timeout=5"

    'Protected Const SQL_CONNECTION_STRING As String = _
    '    "Server=srvt03;user id=sa;password=pelicano;" & _
    '    "DataBase=PLC01;" & _
    '    "Persist Security Info=False;Connect Timeout=5"

    Protected ALTA As Boolean = False
    Protected MODIFICA As Boolean = False
    Protected BAJA As Boolean = False
    Protected BAJA_FISICA As Boolean = False
    Protected DESHACER As Boolean = False


    'paginado de la grilla
    Dim RegistrosPorPagina As Integer = 50
    Dim TotalPaginas As Integer
    Dim PaginaActual As Integer
    Dim ini As Integer = 0
    Dim fin As Integer = RegistrosPorPagina - 1
    'Dim bolPaginar As Boolean = False

    Protected bolPaginar As Boolean = False

    Dim conexion As New Data.SqlClient.SqlConnection(ConnStringUSUARIOS)

    Dim dtnew As New System.Data.DataTable

    Protected Bandera_filtro As Boolean 'dg 29-07-10
    Protected dt_aux As DataTable 'dg 29-07-10
    Protected colum_frozen As Integer 'dg 29-07-10
    ''nuevo ms 07-11-2011
    Dim quitarfiltro As Boolean
    ''fin nuevo
    Dim desdepedidos As Boolean

#End Region


    Public Event ev_CellChanged As EventHandler

    Private Sub paginar(ByVal d As DataTable)

        Dim o(1) As String
        Dim TEMP As String = Nothing
        Static Flag As Boolean = False

        'esto no sirve porque la grilla queda enlazada y despues no se pueden agregar registros
        'Me.grd.DataSource = paginarDataDridView(d, ini, fin) original

        dtnew = paginarDataDridView(d, ini, fin)

        ReDim o(dtnew.Columns.Count * dtnew.Rows.Count)

        grd.Rows.Clear()
        If Not Flag Then
            SetColumnasGrilla()
            Flag = True
        End If

        'For Each row As DataRow In ds.Tables(0).Rows
        For Each row As DataRow In dtnew.Rows
            'nuevo
            For i As Integer = 0 To grd.Columns.Count - 1
                Select Case grd.Columns(i).ValueType.Name.ToUpper
                    Case "DECIMAL"
                        If row.Item(i).ToString <> "" Then 'And Not (IsDBNull(row.Item(i).ToString)) Then
                            TEMP = CType(row.Item(i).ToString, Decimal).ToString("0.00")
                        End If

                    Case "DATETIME"
                        Try
                            If row.Item(i).ToString <> "" And Not (IsDBNull(row.Item(i).ToString)) Then
                                TEMP = CType(row.Item(i).ToString, Date).ToString("dd/MM/yyyy")
                            End If
                        Catch ex As Exception

                        End Try

                    Case Else
                        Try
                            TEMP = row.Item(i).ToString
                        Catch ex As Exception

                        End Try
                End Select
                Try
                    o(i) = TEMP
                Catch ex As Exception

                End Try
            Next i
            'fin nuevo
            grd.Rows.Add(o)
        Next

        grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        dtnew.Dispose()

        InformarCantidadRegistros()

    End Sub

    Protected Sub SetColumnasGrilla()

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


                    If dc.ColumnName.ToLower = "id" Or dc.ColumnName.ToLower = "contraseña" Or _
                        dc.ColumnName.ToLower = "usuario de pañol" Or _
                        dc.ColumnName.ToLower = "datedel" Or _
                        dc.ColumnName.ToLower = "dateupd" Or _
                        dc.ColumnName.ToLower = "dateadd" Or _
                        dc.ColumnName.ToLower = "userupd" Or _
                        dc.ColumnName.ToLower = "useradd" Or _
                        dc.ColumnName.ToLower = "relleno" Or _
                        Mid(dc.ColumnName.ToLower, 1, 1) = "_" Or _
                        dc.ColumnName.ToLower = "userdel" Then
                        Column.Visible = False
                    Else
                        Column.Visible = True
                    End If
                    grd.Columns.Add(Column)
            End Select
        Next

    End Sub


    'Protected Sub cbxPaginas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxPaginas.SelectedIndexChanged
    '    cambiar_pagina()
    '    paginar(ds.Tables(0))
    'End Sub

    Protected Sub cambiar_pagina()
        ini = 0
        fin = 0
        If ToolStripPagina.Text = "1" Then
            'If cbxPaginas.Text = "1" Then
            fin = RegistrosPorPagina - 1
        Else
            ini = ((Convert.ToInt32(ToolStripPagina.Text) - 1) * RegistrosPorPagina) - 1
            fin = ini + RegistrosPorPagina
        End If

        If Bandera_filtro = True Then
            If fin > dt_aux.Rows.Count Then
                fin = dt_aux.Rows.Count - 1
            End If
        Else
            If fin > ds.Tables(0).Rows.Count Then
                fin = ds.Tables(0).Rows.Count - 1
            End If
        End If

    End Sub

    Protected Sub LlenarGrilla()
        Dim o(1) As String

        GetDataset()

        ' Paginar la consulta para obtener solo los primeros 50 registros
        If TotalPaginas = 0 Then
            If ds.Tables.Count <> 0 Then
                Establecer_Cant_Paginas(ds.Tables(0))
            Else
                MsgBox("No hay datos registrados.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Atención")
                Exit Sub
            End If

        End If

        bolPaginar = False
        ToolStripPagina.Text = "1"
        bolPaginar = True

        With grd
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = False
            '.SelectionMode = DataGridViewSelectionMode.CellSelect
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        End With

        With grd.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        End With

        grd.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)

        'CP 18-03-2012, si la grilla está filtrada no hay que perder los filtros
        'Try
        'así que los conservamos
        If Filter.Trim <> "" Then
            Dim d As New DataTable
            d = FiltrarGenerico(ds.Tables(0))
            Establecer_Cant_Paginas(d)
            bolPaginar = False
            ToolStripPagina.Text = "1"
            bolPaginar = True
            paginar(d)
        Else
            'sino, se mantiene la grilla sin filtros como era antes de este cambio
            paginar(ds.Tables(0)) 'Asignar los datos paginados a la grilla
        End If
        'Catch ex As Exception

        'End Try

        'CP 18-03-2012 Fin
        'paginar(ds.Tables(0)) 'Asignar los datos paginados a la grilla
        'grd.Columns(2).Width = 400
        If Not grd.CurrentRow Is Nothing Then
            MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        Else
            bolModo = True
            PrepararBotones()
            Util.LimpiarTextBox(Me.Controls)
            Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        End If

        'grd.Columns(0).Frozen = True
    End Sub

    Protected Sub GetDataset()
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)

            ds.Dispose()

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
    End Sub

    Private Sub grd_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd.CellMouseUp
        CellX = e.ColumnIndex
        CellY = e.RowIndex
    End Sub

    Private Sub InformarCantidadRegistros()
        If Not grd.CurrentRow Is Nothing Then
            MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString & " Total de Páginas: " & TotalPaginas.ToString)
            ''NUEVO MS 25-10-2011 / 31-10-2011se cambio en aplicacion Seguridad y andubo bien una semana
            If bolModo = True And quitarfiltro = True Then
                bolModo = False
                PrepararBotones()
            End If
            ''FIN NUEVO
        Else
            'If grd.Rows.Count < 1 Then
            '    Util.LimpiarTextBox(Me.Controls)
            '    'bolModo = True
            '    PrepararBotones()
            'End If
        End If
    End Sub

    Protected Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            CargarCajas()
        End If
        InformarCantidadRegistros()
        RaiseEvent ev_CellChanged(sender, e) 'por ahora lo usa el formulario entryline
    End Sub

    Protected Sub CargarCajas()
        If Not (grd.CurrentRow Is Nothing) Then
            Util.GrillaATextBox(Me.Controls, grd)
        End If
    End Sub

#Region "   MOVIMIENTO EN LOS REGISTROS"

    Protected Sub btnPrimero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrimero.Click

        Try
            PaginaPrimera()
            grd.CurrentCell = grd.Rows(0).Cells(0)
        Catch ex As Exception
            Exit Sub
        End Try


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Esto mueve registro a registro dentro de la pagina
        ' dehabilitar lo anterior si se quiere nevagar 
        ' registro a registro o por pagina
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'If Not grd.CurrentCell Is Nothing Then
        '    grd.CurrentCell = grd.Rows(0).Cells(0)
        '    Util.MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        'Else
        '    Util.MsgStatus(Status1, "No hay registros para navegar", My.Resources.alert.ToBitmap)
        'End If
    End Sub

    Protected Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnterior.Click
        Try
            PaginaAnterior()
            grd.CurrentCell = grd.Rows(0).Cells(0)
        Catch ex As Exception
            Exit Sub
        End Try


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Esto mueve registro a registro dentro de la pagina
        ' dehabilitar lo anterior si se quiere nevagar 
        ' registro a registro o por pagina
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'If Not grd.CurrentCell Is Nothing Then
        '    If grd.CurrentCell.RowIndex <> 0 Then
        '        grd.CurrentCell = grd.Rows(grd.CurrentRow.Index - 1).Cells(0)

        '    End If
        '    Util.MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        'Else
        '    Util.MsgStatus(Status1, "No hay registros para navegar", My.Resources.alert.ToBitmap)
        'End If
    End Sub

    Protected Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUltimo.Click
        Try
            PaginaUltima()
            grd.CurrentCell = grd.Rows(0).Cells(0)
        Catch ex As Exception
            Exit Sub
        End Try

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Esto mueve registro a registro dentro de la pagina
        ' dehabilitar lo anterior si se quiere nevagar 
        ' registro a registro o por pagina
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'If Not grd.CurrentCell Is Nothing Then
        '    grd.CurrentCell = grd.Rows(grd.Rows.Count - 1).Cells(0)
        '    Util.MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        'Else
        '    Util.MsgStatus(Status1, "No hay registros para navegar", My.Resources.alert.ToBitmap)
        'End If
    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiguiente.Click

        Try
            PaginaSiguiente()
            grd.CurrentCell = grd.Rows(0).Cells(0)
        Catch ex As Exception
            Exit Sub
        End Try

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Esto mueve registro a registro dentro de la pagina
        ' dehabilitar lo anterior si se quiere nevagar 
        ' registro a registro o por pagina
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'If Not grd.CurrentCell Is Nothing Then
        '    If grd.CurrentCell.RowIndex < grd.Rows.Count - 1 Then
        '        grd.CurrentCell = grd.Rows(grd.CurrentRow.Index + 1).Cells(0)
        '    End If
        '    Util.MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        'Else
        '    Util.MsgStatus(Status1, "No hay registros para navegar", My.Resources.alert.ToBitmap)
        'End If
    End Sub

#End Region

    Protected Sub PrepararBotones()
        Me.btnImprimir.Enabled = Not bolModo
        Me.btnEliminar.Enabled = Not bolModo
        Me.btnNuevo.Enabled = Not bolModo
        Me.btnActualizar.Enabled = Not bolModo
        Me.btnExcel.Enabled = Not bolModo
        If Not (grd.Rows.Count < 1) Then
            Me.grd.Enabled = Not bolModo
        Else
            If Bandera_filtro = True Then Me.grd.Enabled = True
        End If
        Me.ToolStripPagina.Enabled = Not bolModo
    End Sub

    Protected Function ReglasNegocio() As Boolean
        Dim msg As String
        ReglasNegocio = False

        msg = CamposObligatorios(Me.Controls)

        If msg <> "" Then
            Beep()
            Util.MsgStatus(Status1, "Falta completar el campo " & msg, My.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Falta completar el campo " & msg, My.Resources.alert.ToBitmap, True)
            Exit Function
        End If

        ReglasNegocio = True
    End Function

    Public Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If Not grd.CurrentRow Is Nothing Then
            bolModo = False
            PrepararBotones()
            CargarCajas()
            Util.MsgStatus(Status1, "Se canceló la última acción.")
        Else
            Util.MsgStatus(Status1, "No se puede cancelar, porque no hay ningún registro guardado", My.Resources.alert.ToBitmap)
        End If
    End Sub


    Public Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        If MDIPrincipal.NoActualizarBase = False Then
            Me.Cursor = Cursors.WaitCursor
            LlenarGrilla()
            Permitir = True
            CargarCajas()
            PrepararBotones()
            Me.Cursor = Cursors.Default
            'GrillaActualizar()
        End If
  

    End Sub

    ''' <summary>
    ''' Establecer la cantidad de paginas en la grilla
    ''' </summary>
    ''' <param name="d"></param>
    ''' <remarks></remarks>
    Protected Sub Establecer_Cant_Paginas(ByVal d As DataTable)

        If d.Rows.Count Mod RegistrosPorPagina = 0 Then
            TotalPaginas = d.Rows.Count / RegistrosPorPagina
        Else
            Dim value As Double = d.Rows.Count / RegistrosPorPagina
            'TotalPaginas = Convert.ToInt32(value) + 1
            TotalPaginas = Int(value) + 1
        End If
        'cbxPaginas.Items.Clear()
        ToolStripPagina.Items.Clear()
        For i As Integer = 0 To TotalPaginas - 1
            'cbxPaginas.Items.Add(i + 1)
            ToolStripPagina.Items.Add(i + 1)
        Next

        Me.btnSiguiente.Enabled = TotalPaginas > 1
        Me.btnAnterior.Enabled = TotalPaginas > 1
        Me.btnUltimo.Enabled = TotalPaginas > 1
        Me.btnPrimero.Enabled = TotalPaginas > 1

    End Sub

    Protected Function paginarDataDridView(ByVal d As DataTable, ByVal inicial As Integer, ByVal final As Integer) As System.Data.DataTable
     
        dtnew.Dispose()
        dtnew = d.Clone
        If d.Rows.Count - 1 < final Then
            final = d.Rows.Count - 1
            inicial = 0
        End If
        For i As Integer = inicial To final
            dtnew.ImportRow(d.Rows(i))
        Next
        Return dtnew
    End Function


    Private Sub grd_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grd.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If grd.RowCount <> 0 Then
                If CellY <> -1 Then FilterValue = grd.Rows(CellY).Cells(CellX).Value.ToString
            End If
            Dim p As Point = New Point(e.X, e.Y)

            FiltrarporToolStrip.Text = ""
            ContextMenuStrip1.Show(grd, p)
            ContextMenuStrip1.Items(0).Text = "Filtro por selección"
            ContextMenuStrip1.Items(0).ToolTipText = Me.Filter
            ContextMenuStrip1.Items(1).Text = "Filtro excluyendo la seleccion"
            ContextMenuStrip1.Items(1).ToolTipText = Me.Filter
            FiltrarporToolStrip.Text = "Filtrar por:"
            ToolStripTextBox1.Text = "Inmovilizar columnas"
            ColumnName = grd.Columns(CellX).HeaderText
            ColumnType = grd.Columns(CellX).ValueType.ToString
        End If

    End Sub


    Private Function FiltrarExcluyendo(ByVal d As DataTable) As DataTable

        Dim rows As DataRow()
        Dim dtNew As DataTable

        dtNew = d.Clone

        If Filter.Trim = "" Then
            Filter = "[" & ColumnName & "] <> '" & FilterValue.Trim.ToUpper & "'"
        Else
            Filter += " AND [" & ColumnName & "] <> '" & FilterValue.Trim.ToUpper & "'"
        End If

        rows = d.Select(Filter, "id")

        Bandera_filtro = True 'dg 29-07-10
        dt_aux = dtNew.Clone 'dg 29-07-10

        ' fill dtNew with selected rows
        For Each dr As DataRow In rows
            dtNew.ImportRow(dr)
            dt_aux.ImportRow(dr) 'dg 29-07-10
        Next

        ' return filtered dt
        Return dtNew
    End Function

    ''' <summary>
    ''' Filtrar los datos exluyendo el datos seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private Function FiltrarGenerico(ByVal d As DataTable) As DataTable
        'Dim Filter As String
        ''COMENTADA MS 30-06-2010 POR WARNING
        ''Dim sort As String
        ''FIN COMENTADA
        Dim rows As DataRow()
        Dim dtNew As DataTable

        ' copy table structure
        dtNew = d.Clone

        ' aplicar el filtro que ya tiene otra vez
        rows = d.Select(Filter, "id")

        ' fill dtNew with selected rows

        Bandera_filtro = True 'dg 29-07-10
        dt_aux = dtNew.Clone 'dg 29-07-10


        For Each dr As DataRow In rows
            dtNew.ImportRow(dr)
            dt_aux.ImportRow(dr)
        Next

        Return dtNew
    End Function


    Private Function FiltrarIncluyendo(ByVal d As DataTable) As DataTable

        Dim rows As DataRow()
        Dim dtNew As DataTable


        dtNew = d.Clone

        If Filter.Trim = "" Then
            Filter = "[" & ColumnName & "] = '" & FilterValue.Trim.ToUpper & "'"
        Else
            Filter += " AND [" & ColumnName & "] = '" & FilterValue.Trim.ToUpper & "'"
        End If
        'Dim nombre As String = d.Columns.Item(6).ColumnName
        rows = d.Select(Filter, "id")

        ' fill dtNew with selected rows

        Bandera_filtro = True 'dg 29-07-10
        dt_aux = dtNew.Clone 'dg 29-07-10


        For Each dr As DataRow In rows
            dtNew.ImportRow(dr)
            dt_aux.ImportRow(dr)
        Next

        Return dtNew
    End Function


    Private Function Filtrar(ByVal d As DataTable, ByVal s As String, Optional ByVal Form As String = "") As DataTable

        If Form = "Materiales" Then
            Dim VIEW As DataView = New DataView(d)

            VIEW.Sort = "OrdenporMedidas"
        End If

        Dim pos As Integer
        Dim rows As DataRow()
        Dim Simbolo As String = "="
        Dim ComillaAbre As String = "'", ComillaCierre As String = "'"
        Dim dtNew As DataTable

        dtNew = d.Clone
        s = UCase(s)

        pos = InStr(s, "LIKE")
        If pos <> 0 Then
            s = Mid(s, pos + 5, Len(s))
        End If

        Select Case ColumnType.ToLower
            Case "system.string"
                ComillaAbre = "'%"
                ComillaCierre = "%'"
                Simbolo = "LIKE"
            Case "system.datetime"
                ComillaAbre = "'"
                ComillaCierre = "'"
                Simbolo = "="
            Case "system.int16" : Case "system.int32" : Case "system.int64" : Case "system.int" : Case "system.long"
                ComillaAbre = ""
                ComillaCierre = ""
                Simbolo = "="
            Case Else
                ComillaAbre = "'"
                ComillaCierre = "'"
        End Select


        Dim posicion As Integer, posicion1 As Integer
        Dim parametro1 As String, parametro2 As String, parametro3 = ""

        posicion = InStr(s, "?")

        If posicion > 0 Then

            parametro1 = Mid(s, IIf(posicion = 0, 1, posicion + 1), s.Length)

            If parametro1.Length > 0 Then
                parametro2 = Mid(s, 1, IIf(posicion = 1, posicion, posicion - 1))

                posicion1 = InStr(parametro1, "?")

                If posicion1 > 0 Then

                    parametro3 = Mid(parametro1, IIf(posicion1 = 0, 1, posicion1 + 1), posicion1 - 1)

                    parametro1 = Mid(parametro1, 1, posicion1 - 1)

                    ' sort and filter data
                    If Filter.Trim = "" Then
                        Filter = "[" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro2 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro1 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro3 & ComillaCierre

                    Else
                        Filter += "AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro2 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro1 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro3 & ComillaCierre

                    End If

                Else

                    If Filter.Trim = "" Then
                        Filter = "[" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro2 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro1 & ComillaCierre

                    Else
                        Filter += " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro2 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro1 & ComillaCierre & _
                            " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & parametro3 & ComillaCierre

                    End If

                End If
            Else
                ' sort and filter data
                If Filter.Trim = "" Then
                    Filter = "[" & ColumnName & "] " & Simbolo & " " & ComillaAbre & s & ComillaCierre
                Else
                    Filter += " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & s & ComillaCierre
                End If

            End If

        Else

            ' sort and filter data
            If Filter.Trim = "" Then
                Filter = "[" & ColumnName & "] " & Simbolo & " " & ComillaAbre & s & ComillaCierre
            Else
                Filter += " AND [" & ColumnName & "] " & Simbolo & " " & ComillaAbre & s & ComillaCierre
            End If

        End If

        'MsgBox(Filter)

        'Filter = Filter & " ORDER BY [OrdenporMedidas]"
        'MsgBox(d.Columns(0).Caption)
        ''MsgBox(d.Columns(1).Caption)
        'MsgBox(d.Columns().Count)

        rows = d.Select(Filter)

        Bandera_filtro = True
        dt_aux = dtNew.Clone

        For Each dr As DataRow In rows
            dtNew.ImportRow(dr)
            dt_aux.ImportRow(dr)
        Next

        Return dtNew
    End Function


    Private Sub FiltroExcluyendoLaSelecciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiltroExcluyendoLaSelecciónToolStripMenuItem.Click
        Dim d As New DataTable
        d = FiltrarExcluyendo(ds.Tables(0))
        Establecer_Cant_Paginas(d)
        bolPaginar = False
        ToolStripPagina.Text = "1"
        bolPaginar = True
        paginar(d)
    End Sub


    Protected Sub QuitarElFitroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitarElFitroToolStripMenuItem.Click
        quitarfiltro = True
        Bandera_filtro = False 'dg 29-07-10
        Try
            dt_aux.Dispose()
        Catch ex As Exception

        End Try

        Filter = ""
        Establecer_Cant_Paginas(ds.Tables(0))
        bolPaginar = False
        ToolStripPagina.Text = "1"
        bolPaginar = True
        paginar(ds.Tables(0))
        quitarfiltro = False
    End Sub


    Private Sub FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FToolStripMenuItem.Click
        Dim d As New DataTable
        d = FiltrarIncluyendo(ds.Tables(0))
        Establecer_Cant_Paginas(d)
        bolPaginar = False
        ToolStripPagina.Text = "1"
        bolPaginar = True
        paginar(d)
    End Sub

    Private Sub FiltrarporToolStrip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FiltrarporToolStrip.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim d As New DataTable
            d = Filtrar(ds.Tables(0), FiltrarporToolStrip.Text.Trim.ToUpper)
            Establecer_Cant_Paginas(d)
            bolPaginar = False
            ToolStripPagina.Text = "1"
            bolPaginar = True
            paginar(d)
            ContextMenuStrip1.Close()
            grd.ReadOnly = True
        End If
    End Sub

    Public Sub Filtrarpor(ByVal filtro As String, Optional ByVal Form As String = "")
        Dim d As New DataTable
        d = Filtrar(ds.Tables(0), filtro.ToUpper, Form)
        Establecer_Cant_Paginas(d)
        bolPaginar = False
        ToolStripPagina.Text = "1"
        bolPaginar = True
        paginar(d)
        ContextMenuStrip1.Close()
    End Sub
    
    Private Sub PaginaAnterior()
        If ToolStripPagina.SelectedIndex > 0 Then
            ToolStripPagina.SelectedIndex = ToolStripPagina.SelectedIndex - 1
            cambiar_pagina()
        End If
    End Sub

  
    Private Sub PaginaSiguiente()
        If Not ToolStripPagina.SelectedIndex + 1 > ToolStripPagina.Items.Count - 1 Then
            ToolStripPagina.SelectedIndex = ToolStripPagina.SelectedIndex + 1
            cambiar_pagina()
        End If
    End Sub

    Private Sub PaginaPrimera()
        ToolStripPagina.SelectedIndex = 0
        cambiar_pagina()
    End Sub

   
    Private Sub PaginaUltima()
        ToolStripPagina.SelectedIndex = ToolStripPagina.Items.Count - 1
        cambiar_pagina()
    End Sub

    Private Sub ToolStripPagina_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripPagina.SelectedIndexChanged
        If bolPaginar = True Then
            cambiar_pagina()
            If Bandera_filtro = True Then
                paginar(dt_aux)
            Else
                paginar(ds.Tables(0))
            End If
        End If
    End Sub

    Private Sub FiltrarporToolStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiltrarporToolStrip.Click
        FiltrarporToolStrip.Text = ""
    End Sub

    Public Sub DataTableToExcel(ByVal pDataTable As DataTable)
        Dim vFileName As String = System.IO.Path.GetTempFileName()
        FileOpen(1, vFileName, OpenMode.Output)
        Dim sb As String = ""
        Dim dc As DataColumn
        For Each dc In pDataTable.Columns
            sb &= dc.Caption.ToLower & Microsoft.VisualBasic.ControlChars.Tab
        Next
        PrintLine(1, sb)
        Dim i As Integer = 0
        Dim dr As DataRow
        For Each dr In pDataTable.Rows
            i = 0 : sb = ""
            For Each dc In pDataTable.Columns
                If Not IsDBNull(dr(i)) Then
                    sb &= CStr(dr(i)) & Microsoft.VisualBasic.ControlChars.Tab
                Else
                    sb &= Microsoft.VisualBasic.ControlChars.Tab
                End If
                i += 1
            Next
            PrintLine(1, sb)
        Next
        FileClose(1)
        TextToExcel(vFileName)
    End Sub

    Public Sub TextToExcel(ByVal pFileName As String)

        Dim vFormato As Microsoft.Office.Interop.Excel.XlRangeAutoFormat
        Dim Exc As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
        Exc.Workbooks.OpenText(pFileName, , , , Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, , True)
        Dim Wb As Microsoft.Office.Interop.Excel.Workbook = Exc.ActiveWorkbook
        Dim Ws As Microsoft.Office.Interop.Excel.Worksheet = CType(Wb.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)
        'Se le indica el formato al que queremos exportarlo
        Dim valor As Integer = 10

        Ws.Name = "Hoja1"

        Try
            If valor > -1 Then
                Select Case (valor)
                    Case 10 : vFormato = Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1
                End Select
                Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).AutoFormat(vFormato)
                pFileName = System.IO.Path.GetTempFileName.Replace("tmp", "xls")
                System.IO.File.Delete(pFileName)
                Exc.ActiveWorkbook.SaveAs(pFileName, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone - 1)
            End If
            Exc.Quit()
            Ws = Nothing
            Wb = Nothing
            Exc = Nothing
            GC.Collect()
            If valor > -1 Then
                Dim p As System.Diagnostics.Process = New System.Diagnostics.Process
                p.EnableRaisingEvents = False
                System.Diagnostics.Process.Start(pFileName)
            End If
        Catch ex As Exception
            Exit Sub
        End Try



    End Sub

    Protected Overridable Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'DataTableToExcel(grd.DataSource)
        DataTableToExcel(dtnew)
    End Sub

    ''' <summary>
    ''' Permite importar datos desde una planilla excel
    ''' no se implementa por ahora pero queda la funcion hecha
    ''' </summary>
    ''' <param name="dgView"></param>
    ''' <param name="SLibro"></param>
    ''' <param name="sHoja"></param>
    ''' <remarks></remarks>
    Private Sub CargardeExcel( _
         ByVal dgView As DataGridView, _
         ByVal SLibro As String, _
         ByVal sHoja As String)

        'HDR=YES : Con encabezado  
        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                           "Data Source=" & SLibro & ";" & _
                           "Extended Properties=""Excel 8.0;HDR=YES"""
        Try
            ' cadena de conexión  
            Dim cn As New System.Data.OleDb.OleDbConnection(cs)

            If Not System.IO.File.Exists(SLibro) Then
                MsgBox("No se encontró el Libro: " & _
                        SLibro, MsgBoxStyle.Critical, _
                        "Ruta inválida")
                Exit Sub
            End If

            ' se conecta con la hoja sheet 1  
            Dim dAdapter As New System.Data.OleDb.OleDbDataAdapter("Select * From [" & sHoja & "$]", cs)

            Dim datos As New DataSet

            ' agrega los datos  
            dAdapter.Fill(datos)

            With grd
                ' llena el DataGridView  
                .DataSource = datos.Tables(0)

                ' DefaultCellStyle: formato currency   
                'para los encabezados 1,2 y 3 del DataGrid  
                '.Columns(1).DefaultCellStyle.Format = "c"
                '.Columns(2).DefaultCellStyle.Format = "c"
                '.Columns(3).DefaultCellStyle.Format = "c"
            End With
        Catch oMsg As Exception
            MsgBox(oMsg.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ToolStripTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.Click
        ToolStripTextBox1.Text = ""
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(ToolStripTextBox1.Text) Then
                colum_frozen = CType(ToolStripTextBox1.Text, Integer) - 1

                If colum_frozen < grd.Columns.Count And colum_frozen >= 0 Then
                    grd.Columns(colum_frozen).Frozen = True

                    grd.Columns(colum_frozen).DividerWidth = 2

                    ToolMovilizar.Visible = True
                    ToolStripTextBox1.Visible = False
                    ContextMenuStrip1.Close()
                End If
            End If
        End If
    End Sub

    Private Sub ToolMovilizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMovilizar.Click

        Dim i As Integer

        ToolMovilizar.Visible = False
        ToolStripTextBox1.Visible = True
        ToolStripTextBox1.Text = "Inmovilizar columnas"

        For i = 0 To colum_frozen
            grd.Columns(i).Frozen = False
            grd.Columns(i).DividerWidth = 0
        Next



    End Sub

    'Protected Overridable Sub btnImportarExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportarExcel.Click
    '    Dim FileName As String
    '    Try
    '        With OpenFileDialog1
    '            .Filter = "Archivos Excel (*.xls)|*.xls|" & "Todos los archivos|*.*"
    '        End With
    '    Catch es As Exception
    '        MessageBox.Show(es.Message)
    '    End Try
    '    OpenFileDialog1.ShowDialog()
    '    FileName = OpenFileDialog1.FileName
    '    CargardeExcel(grd, FileName, "HOJA1")
    'End Sub

    
    Public Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click

        If bolModo = True Then
            If MessageBox.Show("Tiene una operación en modo Nuevo. Desea salir de todos modos?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.Dispose()
                Me.Close()
            End If
        Else
            'Me.Dispose()
            Me.Close()
        End If
    End Sub

    Private Sub frmBase_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Me.ToolStrip_lblCambio.Text = " 1 u$$ = $ " & ObtenerMoneda_ValorCambioDolar(ConnStringSEI)

        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        'Try
        '    Dim param_peso As New SqlClient.SqlParameter
        '    param_peso.ParameterName = "@peso"
        '    param_peso.SqlDbType = SqlDbType.Decimal
        '    param_peso.Precision = 18
        '    param_peso.Scale = 3
        '    param_peso.Value = DBNull.Value
        '    param_peso.Direction = ParameterDirection.InputOutput

        '    Dim param_dolar As New SqlClient.SqlParameter
        '    param_dolar.ParameterName = "@dolar"
        '    param_dolar.SqlDbType = SqlDbType.Decimal
        '    param_dolar.Precision = 18
        '    param_dolar.Scale = 3
        '    param_dolar.Value = DBNull.Value
        '    param_dolar.Direction = ParameterDirection.InputOutput

        '    Dim param_euro As New SqlClient.SqlParameter
        '    param_euro.ParameterName = "@euro"
        '    param_euro.SqlDbType = SqlDbType.Decimal
        '    param_euro.Precision = 18
        '    param_euro.Scale = 3
        '    param_euro.Value = DBNull.Value
        '    param_euro.Direction = ParameterDirection.InputOutput

        '    Try
        '        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spObtenerTipoMoneda ", param_peso, param_dolar, param_euro)

        '        Me.ToolStrip_lblCambio.Text = " 1 u$$ = $ " & CDec(param_dolar.Value)
        '        'euro = CDec(param_euro.Value)

        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'Catch ex As Exception
        '    Dim errMessage As String = ""
        '    Dim tempException As Exception = ex
        '    While (Not tempException Is Nothing)
        '        errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
        '        tempException = tempException.InnerException
        '    End While
        '    MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
        '      + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
        '      "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    If Not connection Is Nothing Then
        '        CType(connection, IDisposable).Dispose()
        '    End If
        'End Try
    End Sub

    Public Sub frmBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F12 'nuevo
                btnSalir_Click(sender, e)
                'Case Keys.F5
                'btnActualizar_Click(sender, e)
            Case Keys.F11
                txtBusquedaMAT.Focus()
        End Select
    End Sub

    Private Sub txtBusquedaMAT_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBusquedaMAT.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If bolModo = True Then
            '    MsgBox("No puede ejecutar este filtro si está en modo Nuevo", MsgBoxStyle.Information, "Atención")
            'Else
            '    If txtBusquedaMAT.Text <> "" Then
            '        SQL = "exec spBuscarMaterial '" + ActiveForm.ActiveMdiChild.AccessibleName + "', '" + txtBusquedaMAT.Text + "'"
            '        Llenar_BuscarMAT()
            '    Else
            '        Select Case ActiveForm.ActiveMdiChild.AccessibleName
            '            Case "OrdenDeCompra"
            '                SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & frmOrdenCompra.rdAnuladas.Checked & ", @Pendientes = " & frmOrdenCompra.rdPendientes.Checked & ", @PendientesyCumplidas = " & frmOrdenCompra.rdTodasOC.Checked
            '            Case "Presupuestos"
            '                SQL = "exec spPresupuestos_Select_All  @Eliminado = 0, @Estado = ''"
            '        End Select
            '        LlenarGrilla()
            '    End If
            'End If

            nbreformreportes = "Historia del Producto - Ult 10 Movimientos"

            Cursor = Cursors.WaitCursor

            'Dim cnn As New SqlConnection(ConnStringSEI)
            Dim Rpt As New frmReportes

            Rpt.HistoriaProducto_App(txtBusquedaMAT.Text, Rpt, ActiveForm.ActiveMdiChild.AccessibleName, My.Application.Info.AssemblyName.ToString)

            Cursor = Cursors.Default

            'cnn = Nothing

        End If
    End Sub

    Protected Sub Llenar_BuscarMAT()
        Dim o(1) As String

        GetDataset()

        If TotalPaginas = 0 Then
            If ds.Tables.Count <> 0 Then
                Establecer_Cant_Paginas(ds.Tables(0))
            Else
                MsgBox("No hay datos registrados.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Atención")
                Exit Sub
            End If

        End If

        bolPaginar = False
        ToolStripPagina.Text = "1"
        bolPaginar = True

        With grd
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        End With

        With grd.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grd.Font = New Font("TAHOMA", 7, FontStyle.Regular)

        If Filter.Trim <> "" Then
            Dim d As New DataTable
            d = FiltrarGenerico(ds.Tables(0))
            Establecer_Cant_Paginas(d)
            bolPaginar = False
            ToolStripPagina.Text = "1"
            bolPaginar = True
            paginar(d)
        Else
            paginar(ds.Tables(0)) 'Asignar los datos paginados a la grilla
        End If

        If Not grd.CurrentRow Is Nothing Then
            MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        End If
    End Sub

End Class