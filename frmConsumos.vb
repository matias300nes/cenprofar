Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmConsumos

    Dim permitir_evento_CellChanged As Boolean

    Dim codigoconsumovale As String
    Dim llenandoCombo As Boolean = False
    Dim llenandoCombo2 As Boolean = False, bolpoliticas As Boolean
    'Variables para la grilla
    Dim editando_celda As Boolean, RefrescarGrid As Boolean
    Dim FILA As Integer, COLUMNA As Integer
    Private ds_2 As DataSet
    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer, Cell_Y As Integer
    'Para el combo de busqueda 
    Dim ID_Buscado As Long, Nombre_Buscado As Long
    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing
    Private ht As New System.Collections.Hashtable() 'usada para almacenar el idCliente de la tabla proyectos

    Public UltBusqueda As String

    Public Band_RecDesc As Boolean

    Public codigoDesdeLista As String

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IdConsumo_Det = 0
        IDMaterial = 1
        Cod_Material = 2
        Nombre_Material = 3
        IDUnidad = 4
        Cod_Unidad = 5
        Unidad = 6
        Minimo = 7
        Maximo = 8
        Stock = 9
        PrecioLista = 10
        Ganancia = 11
        PrecioUni = 12
        PrecioVta = 13
        RecDesc = 14
        PorcRecDesc = 15
        Qty = 16
        SubTotalProd = 17
        dateupd = 18
        preciovtaorig = 19
        gananciaorig = 20
        nota_det = 21
        orden = 22
    End Enum


    'Auxiliares para guardar
    Dim cod_aux As String
    Dim band As Integer, Revision As Integer


#Region "Eventos"

    Private Sub frmConsumos_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bolModo = True Then
            If MessageBox.Show("Tiene un Consumo pendiente de ser guardado. Desea salir de todos modos?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmConsumos_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGridItems(CType(txtID.Text, Long))
            End If
        End If
    End Sub

    Private Sub frmConsumos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        band = 0

        'Me.txtID.Visible = False
        Band_RecDesc = False
        btnEliminar.Text = "Anular Venta"

        configurarform()
        asignarTags()
        LlenarcmbClientes()
        LlenarcmbMateriales()
        LlenarcmbEntregar()
        LlenarcmbUnidadesVta()
        LlenarcmbEmpleados3_App(cmbVendedor, ConnStringSEI)

        LlenarLista()

        SQL = "exec sp_Consumos_Select_All"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        ' ajuste de la grilla de navegación según el tamaño de los datos
        'Setear_Grilla()

        If bolModo = True Then
            LlenarGridItems(0)
            btnNuevo_Click(sender, e)
        Else
            LlenarGridItems(CType(IIf(txtID.Text = "", 0, txtID.Text), Long))
            LlenarcmbClientes_Comprador()
        End If

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
        End If

        permitir_evento_CellChanged = True

        grd.Columns(4).Visible = False
        grd.Columns(10).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(20).Visible = False
        grd.Columns(23).Visible = False
        grd.Columns(25).Visible = False
        grd.Columns(26).Visible = False

        grd.Columns(27).Visible = False
        grd.Columns(28).Visible = False
        grd.Columns(29).Visible = False
        grd.Columns(30).Visible = False
        grd.Columns(31).Visible = False
        grd.Columns(32).Visible = False


        If grd.RowCount > 0 Then
            cmbCliente.SelectedValue = grd.CurrentRow.Cells(10).Value
            cmbComprador.SelectedValue = grd.CurrentRow.Cells(16).Value
            txtporcrecargo.Text = grd.CurrentRow.Cells(14).Value
            cmbVendedor.SelectedValue = grd.CurrentRow.Cells(20).Value
            grdItems.Enabled = Not CBool(grd.CurrentRow.Cells(15).Value)
            GroupBox1.Enabled = Not CBool(grd.CurrentRow.Cells(15).Value)
        End If

        chkOcultarGanancia.Checked = True

        band = 1

        Contar_Filas()

        dtpFECHA.Focus()

    End Sub

    Private Sub frmConsumos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then

                    If MessageBox.Show("No ha guardado " & IIf(rdVenta.Checked = True, "la Venta", "el Consumo") & " que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un nuevo Consumo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            If txtIVA.Text = "" Then
                MsgBox("Debe ingresar el IVA antes de comenzar a cargar los items del Movimiento", MsgBoxStyle.Critical, "Control de IVA")
                Exit Sub
            End If
            'Cuando terminamos la edicion hay que buscar la descripcion del material y las unidad,
            'con esos datos completar la grilla. En este caso es la columna 2
            If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
                'completar la descripcion del material
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim codigo As String, unidad As String = "", nombre As String = "", codunidad As String = ""
                Dim id As Long, idunidad As Long, idproveedor As Long
                Dim stock As Double = 0, minimo As Double = 0, maximo As Double = 0, precio As Double = 0, ganancia As Double = 0, preciovta As Double = 0, preciovtaorig As Double = 0, gananciaorig As Double = 0
                Dim fecha As String = ""
                Dim i As Integer

                If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
                    Exit Sub
                End If

                'verificamos si el codigo ya esta en la grilla...
                For i = 0 To grdItems.RowCount - 2
                    Dim cuentafilas As Integer
                    Dim codigo_mat As String = "", codigo_mat_2 As String = ""
                    codigo_mat = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value)
                    For cuentafilas = i + 1 To grdItems.RowCount - 2
                        If grdItems.RowCount - 1 > 1 Then
                            'codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
                            codigo_mat_2 = IIf(grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value)
                            If codigo_mat <> "" And codigo_mat_2 <> "" Then
                                If codigo_mat = codigo_mat_2 Then
                                    Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                                    cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex)
                                    grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = " "
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                Next

                codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerMaterial_Acer(codigo, id, nombre, idunidad, unidad, codunidad, stock, minimo, maximo, precio, ganancia, preciovta, preciovtaorig, gananciaorig, CDbl(txtIVA.Text), fecha, idproveedor, "") = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = codunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Value = stock
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Minimo).Value = minimo
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Maximo).Value = maximo
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioUni).Value = preciovta
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.RecDesc).Value = 0
                    End If
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0
                    End If
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value = FormatNumber(ganancia, 2)

                    If txtporcrecargo.Text <> "" And txtporcrecargo.Text <> "0" Then
                        If chkDesc.Checked = True Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)

                        End If

                        If chkRecargo.Checked = True Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)

                        End If
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = CDbl(preciovta)
                    End If
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.dateupd).Value = fecha
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = CDbl(preciovtaorig)
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = CDbl(gananciaorig)


                    'si el stock es cero pintar de rojo..
                    'si el stock es mayor a cero y menor o igual al minimo pintar de amarillo..
                    'si el stock es mayor al minimo dejar en blanco..
                    If stock = 0 Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Red
                    ElseIf stock <= minimo Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Yellow
                    Else
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.White
                    End If

                    Contar_Filas()

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                    Exit Sub
                End If
            End If

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value _
                And e.ColumnIndex = ColumnasDelGridItems.Nombre_Material Then

                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0

            End If

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = False
                'grdItems.Columns(ColumnasDelGridItems.PrecioVta).ReadOnly = False
                grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = False
                grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = False
                grdItems.Columns(ColumnasDelGridItems.Ganancia).ReadOnly = False
                grdItems.Columns(ColumnasDelGridItems.Ganancia).Visible = True
                grdItems.Columns(ColumnasDelGridItems.PrecioLista).Visible = True
                'grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0
                'LlenarcmbUnidadesVta()
            Else
                'grdItems.Columns(ColumnasDelGridItems.PrecioVta).ReadOnly = True
                grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = True
                grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = True
                grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = True
                grdItems.Columns(ColumnasDelGridItems.Ganancia).ReadOnly = True
            End If

            If e.ColumnIndex = ColumnasDelGridItems.Cod_Unidad And grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then

                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_unidad As String, nombre As String = "", codunidad As String = ""
                Dim idunidad As Long

                cod_unidad = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerUnidad_App(cod_unidad, idunidad, nombre, codunidad, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = codunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = nombre

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                    Exit Sub
                End If
            End If

            If e.ColumnIndex = ColumnasDelGridItems.PrecioLista Or e.ColumnIndex = ColumnasDelGridItems.Ganancia Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)

                If (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0) And e.ColumnIndex = ColumnasDelGridItems.Ganancia Then
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = 1 + (grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value / 100)
                End If

                If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value) And _
                        Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value) Then
                    'Dim cell As DataGridViewCell = grdItems.CurrentCell
                    'cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)

                    Dim ganancia As Double, preciolista As Double

                    ganancia = 1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100)
                    preciolista = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = ganancia * preciolista


                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100))
                        'grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value = 1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100)
                    End If

                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100))
                    End If

                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                        If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value), 2))
                        End If
                    Else
                        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value Then
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                            Else
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                            End If
                        Else
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value = False Then
                                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                                Else
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                                End If
                            Else
                                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                                Else
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                                End If
                            End If
                        End If
                        If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, 2))
                        End If
                    End If

                End If
            End If


            If e.ColumnIndex = ColumnasDelGridItems.PorcRecDesc Then

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100))
                    'grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value = 1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100)
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100))
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value), 2))
                    End If
                Else
                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value Then
                        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                        Else
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                        End If
                    Else
                        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value = False Then
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                            Else
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                            End If
                        Else
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                            Else
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                            End If
                        End If
                    End If
                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, 2))
                    End If
                End If

                'If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
                '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
                '    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                '    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value), 2))
                '    End If
                'Else
                '    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value Then
                '        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                '            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                '        Else
                '            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                '        End If
                '    Else
                '        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value = False Then
                '            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                '                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                '            Else
                '                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                '            End If
                '        Else
                '            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                '                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                '            Else
                '                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                '            End If
                '        End If
                '    End If
                '    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, 2))
                '    End If
                'End If
            End If


            If e.ColumnIndex = ColumnasDelGridItems.Qty Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value, 2))

                'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * (grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.RecDesc).Value), 2))

                Dim j As Integer = 0

                txtSubtotal.Text = "0"

                For j = 0 To grdItems.Rows.Count - 1
                    txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value)
                Next

                txtIvaTotal.Text = Math.Round(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
                txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtIvaTotal.Text)

                Contar_Filas()

            End If



        Catch ex As Exception
            MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales2

    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
        Contar_Filas()
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub grdItems_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdItems.MouseUp
        Dim Valor As String = ""
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If grdItems.RowCount <> 0 Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            If grdItems.RowCount <> 0 And Cell_X = 5 Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Unidad).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Unidad).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            If Valor <> "" Then
                Dim p As Point = New Point(e.X, e.Y)
                'MyBase.Point_Context = p
                'MyBase.Point_Context.Offset(40, 202)

                ContextMenuStrip1.Show(grdItems, p)
                ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
            End If

            If Valor <> "" And Cell_X = 5 Then
                Dim p As Point = New Point(e.X, e.Y)
                'MyBase.Point_Context = p
                'MyBase.Point_Context.Offset(40, 202)

                ContextMenuStrip2.Show(grdItems, p)
                'ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
            End If

        End If
    End Sub

    Private Sub grdITEMS_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellValueChanged
        Try
            If Band_RecDesc = False Then
                If e.ColumnIndex = ColumnasDelGridItems.RecDesc Then
                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100))
                    End If

                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                        If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value), 2))
                        End If
                    Else
                        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value Then
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                            Else
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                            End If
                        Else
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value = False Then
                                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value '* grdItems.CurrentRow.Cells(ColumnasDelGridItems.RecDesc).Value, 2)
                                Else
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                                End If
                            Else
                                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioUni).Value
                                Else
                                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Math.Round(grdItems.CurrentRow.Cells(ColumnasDelGridItems.preciovtaorig).Value / (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                                End If
                            End If
                        End If
                        If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, 2))
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            MsgBox("Error en Sub grdRemitos_CellClick", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub grdRemitos_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellDirtyStateChanged
        If grdItems.IsCurrentCellDirty Then
            grdItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        cmbCliente.Enabled = Not chkEliminado.Checked
        grdItems.Enabled = Not chkEliminado.Checked
        dtpFECHA.Enabled = Not chkEliminado.Checked
    End Sub

    Private Sub cmbCliente_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedValueChanged
        If llenandoCombo = False Then
            If cmbCliente.Text <> "" Then
                LlenarcmbClientes_Comprador()
                'LlenarcmbClientes_Usuario()
                BuscarPorcentajeRecargo()
            End If
        End If
    End Sub

    Private Sub txtIVA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIVA.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If txtIVA.Text = "" Then
                MsgBox("Debe ingresar el IVA antes de comenzar a cargar los Items del Movimiento", MsgBoxStyle.Critical, "Control de IVA")
                txtIVA.Focus()
            Else
                txtIvaTotal.Text = Math.Round(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
                txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtIvaTotal.Text)
                cmbCliente.Focus()
            End If
        End If
    End Sub

    Private Sub txtIVA_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIVA.LostFocus
        If txtIVA.Text = "" Then
            MsgBox("Debe ingresar el IVA antes de comenzar a cargar los Items del Movimiento", MsgBoxStyle.Critical, "Control de IVA")
            txtIVA.Focus()
        Else
            txtIvaTotal.Text = Math.Round(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
            txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtIvaTotal.Text)
        End If
    End Sub

    Private Sub chkNotas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotas.CheckedChanged
        lstNotas.Enabled = chkNotas.Checked
    End Sub

    Private Sub chkComprador_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRetiradopor.CheckedChanged
        cmbComprador.Enabled = chkRetiradopor.Checked
    End Sub

    Private Sub chkEntrega_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEntrega.CheckedChanged
        cmbEntregaren.Enabled = chkEntrega.Checked
        If chkEntrega.Checked = False Then
            cmbEntregaren.Text = ""
        End If
    End Sub

    Private Sub txtporcrecargo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtporcrecargo.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            Calcular_RecargoDescuento()
        End If
    End Sub

    Private Sub PicClientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicClientes.Click
        Dim f As New frmClientes

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbCliente.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbClientes()
        cmbCliente.Text = texto_del_combo
    End Sub

    Private Sub PicNotas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicNotas.Click
        Dim f As New frmNotas

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        f.ShowDialog()
        LlenarLista()

        If txtID.Text <> "" Then
            ControlarLista()
        End If

    End Sub

    Private Sub PicGanancia_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picGanancia.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.picGanancia, "Por medio de esta opción se puede ocultar las columnas Ganancia y Precio Lista. Es importante recordar " & vbCrLf & "que estas columnas deberán mostrarse cuando ingrese un item que no está codificado.")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            chkOcultarGanancia.Checked = True
            chkOcultarGanancia_CheckedChanged(sender, e)
            Try
                If grd.RowCount > 0 Then
                    cmbComprador.SelectedValue = grd.CurrentRow.Cells(16).Value
                    cmbCliente.SelectedValue = grd.CurrentRow.Cells(10).Value
                    txtporcrecargo.Text = grd.CurrentRow.Cells(14).Value
                    cmbVendedor.SelectedValue = grd.CurrentRow.Cells(20).Value
                End If
                cmbComprador.SelectedValue = grd.CurrentRow.Cells(16).Value
            Catch ex As Exception

            End Try
            Try
                grdItems.Enabled = Not CBool(grd.CurrentRow.Cells(15).Value)
                GroupBox1.Enabled = Not CBool(grd.CurrentRow.Cells(15).Value)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub grdItems_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseDoubleClick

        If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
            LLAMADO_POR_FORMULARIO = True

            Dim f As New frmMaterialesPrecios

            f.Width = 1200
            f.Height = 650
            f.StartPosition = FormStartPosition.CenterScreen
            f.grd.Width = 1150
            f.grd.Height = 350
            f.DesdePre = 2
            f.FilaCodigo = e.RowIndex

            f.ShowDialog(Me)

        End If

    End Sub

    Private Sub chkOcultarGanancia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOcultarGanancia.CheckedChanged
        grdItems.Columns(ColumnasDelGridItems.Ganancia).Visible = Not chkOcultarGanancia.Checked
        grdItems.Columns(ColumnasDelGridItems.PrecioLista).Visible = Not chkOcultarGanancia.Checked

        If chkOcultarGanancia.Checked = True Then
            chkOcultarGanancia.Text = "Mostrar PL y GC"
        Else
            chkOcultarGanancia.Text = "Ocultar PL y GC"
        End If

    End Sub

    Private Sub chkRecargo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRecargo.CheckedChanged
        chkDesc.Checked = Not chkRecargo.Checked
        txtporcrecargo.Focus()
        Calcular_RecargoDescuento()
    End Sub

    Private Sub chkDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDesc.CheckedChanged
        chkRecargo.Checked = Not chkDesc.Checked
        txtporcrecargo.Focus()
        Calcular_RecargoDescuento()
    End Sub

    Private Sub chkRecDescGlobal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRecDescGlobal.CheckedChanged
        chkDesc.Enabled = chkRecDescGlobal.Checked
        chkRecargo.Enabled = chkRecDescGlobal.Checked
        txtporcrecargo.Enabled = chkRecDescGlobal.Checked
        Label4.Enabled = chkRecDescGlobal.Checked

        If chkRecDescGlobal.Checked = True Then
            chkRecargo.Checked = True
            txtporcrecargo.Focus()
        Else
            txtporcrecargo.Text = ""
            chkRecargo.Checked = False
            chkDesc.Checked = False
        End If

        Calcular_RecargoDescuento()

    End Sub

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewRow = grdItems.CurrentRow
        'Dim cell As DataGridViewCell = grdItems.CurrentCell

        'Borrar la fila actual
        'If cell.RowIndex <> 0 Then
        'If cell.RowIndex >= 0 Then 'el de arriba no borraba la fila 0....
        Try
            If cell.Index >= 0 Then 'el de arriba no borraba la fila 0....
                Try
                    If ControlarItemdentrodeRemito(grdItems.CurrentRow.Cells(ColumnasDelGridItems.IDMaterial).Value) > 0 Then
                        MsgBox("El Item seleccionado ya fue entregado y está ingresado en un remito, por lo tanto no se puede eliminar", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                    grdItems.Rows.RemoveAt(cell.Index)
                    grdItems.Refresh()
                    Contar_Filas()
                Catch ex As Exception

                End Try

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then

            Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material)
            Dim cell2 As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Material)
            grdItems.CurrentCell = cell
            grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.Text
            grdItems.CurrentCell = cell2
            grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.ComboBox.SelectedValue

            ContextMenuStrip1.Close()
            grdItems.BeginEdit(True)
        ElseIf e.KeyCode = Keys.Escape Then
            ContextMenuStrip1.Close()

        End If
    End Sub

    Private Sub cmbUnidadVenta_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbUnidadVenta.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then

            Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Unidad)
            Dim cell2 As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Unidad)
            grdItems.CurrentCell = cell
            grdItems.CurrentCell.Value = cmbUnidadVenta.Text
            grdItems.CurrentCell = cell2
            grdItems.CurrentCell.Value = cmbUnidadVenta.ComboBox.SelectedValue

            ContextMenuStrip2.Close()
            grdItems.BeginEdit(True)
        ElseIf e.KeyCode = Keys.Escape Then
            ContextMenuStrip2.Close()

        End If
    End Sub

    Private Sub chkCtaCte_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ChkPago.Checked = Not chkCerrar.Checked
    End Sub

    Private Sub ChkPago_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        chkCerrar.Checked = Not ChkPago.Checked
    End Sub

    Private Sub chkFactura_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFactura.CheckedChanged
        txtFactura.Enabled = chkFactura.Checked
    End Sub

    Private Sub chkOC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOC.CheckedChanged
        txtOC.Enabled = chkOC.Checked
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        If MessageBox.Show("Desea generar una nueva Venta o Consumo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        chkEliminado.Checked = False
        cmbCliente.Enabled = True
        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        Util.LimpiarTextBox(Me.Controls)

        PrepararGridItems()

        txtIVA.Text = "21"
        txtIvaTotal.Text = "0"
        txtTotal.Text = "0"
        txtSubtotal.Text = "0"
        chkRetiradopor.Checked = False
        chkEntrega.Checked = False
        grdItems.AllowUserToAddRows = True
        lblCantidadFilas.Text = "0 / 16"
        chkNotas.Checked = True
        cmbVendedor.SelectedValue = 1

        chkFactura.Checked = False
        txtFactura.Enabled = False

        chkOcultarGanancia.Checked = True

        GroupBox1.Enabled = True

        LlenarLista()
        Dim i As Integer
        For i = 0 To 1
            lstNotas.Items(i).Checked = True
        Next

        rdVenta.Checked = True

        dtpFECHA.Focus()

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer, res_item As Integer, res_notas As Integer

        Util.Logueado_OK = False

        If chkNotas.Checked = False Then
            If MessageBox.Show(IIf(rdVenta.Checked = True, "La Venta", "El Consumo") & " no tiene Notas seleccionadas." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        Else
            Dim i As Integer, SinNotas As Boolean
            For i = 0 To lstNotas.Items.Count - 1
                If lstNotas.Items(i).Checked = True Then
                    SinNotas = True
                    Exit For
                End If
            Next

            If SinNotas = False Then
                If MessageBox.Show(IIf(rdVenta.Checked = True, "La Venta", "El Consumo") & " no tiene Notas seleccionadas." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If

        If chkRecDescGlobal.Checked = True Then
            If MessageBox.Show("La opción de Recargo/Descuento Glogal, está activa. Esto modificará todos los precios del Movimiento." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If chkFactura.Checked = True And txtFactura.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el nro de la factura", My.Resources.Resources.stop_error.ToBitmap, True)
            txtFactura.Focus()
            Exit Sub
        End If

        If chkFactura.Checked = True And cmbCondIVA.Text.ToCharArray = "" Then
            Util.MsgStatus(Status1, "Debe ingresar la condición de IVA del Cliente", My.Resources.Resources.stop_error.ToBitmap, True)
            cmbCondIVA.Focus()
            Exit Sub
        End If

        Dim j As Integer = 0

        txtSubtotal.Text = "0"

        For j = 0 To grdItems.Rows.Count - 1
            txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value)
        Next

        txtIvaTotal.Text = Math.Round(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
        txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtIvaTotal.Text)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando o el registro...", My.Resources.Resources.indicator_white)
                If bolModo = False Then
                    ControlarCantidadRegistros()
                End If
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo Insertar el Consumo (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Insertar el Movimiento (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el Movimiento (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el Movimiento (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 20
                        Cerrar_Tran()
                        Imprimir()
                        band = 0
                        bolModo = False
                        btnActualizar_Click(sender, e)
                        Util.MsgStatus(Status1, "El Movimiento se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                        band = 1
                        Exit Sub
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                        res_item = AgregarRegistro_Items()
                        Select Case res_item
                            Case -30
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Insertar el Material", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Insertar el Material", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -5
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal los items", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal los items", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -6
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de items.", My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de items.", My.Resources.Resources.alert.ToBitmap, True)
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo Agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo Agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case Else
                                If chkNotas.Checked = True Then
                                    res_notas = AgregarRegistro_Notas()
                                    Select Case res_notas
                                        Case -2
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal las notas", My.Resources.Resources.stop_error.ToBitmap)
                                            Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal las notas", My.Resources.Resources.stop_error.ToBitmap, True)
                                        Case -1
                                            Cancelar_Tran()
                                            Util.MsgStatus(Status1, "No se pudo Insertar el detalle de las notas seleccionadas.", My.Resources.Resources.alert.ToBitmap)
                                            Util.MsgStatus(Status1, "No se pudo Insertar el detalle de las notas seleccionadas.", My.Resources.Resources.alert.ToBitmap, True)
                                        Case Else
                                            If bolModo = False Then
                                                EliminarItems_Consumo()
                                            End If

                                            If chkFactura.Checked = True Then
                                                res = AgregarFactura()
                                                Select Case res
                                                    Case -4
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "El nro de Factura ya existe en el sistema, por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "El nro de Factura ya existe en el sistema, por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Case -3
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Case -2
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No se pudo actualizar el número de Facturación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No se pudo actualizar el número de Facturación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Case -1
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Case 0
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Case Else
                                                        Util.MsgStatus(Status1, "Factura generada con éxito", My.Resources.Resources.stop_error.ToBitmap)
                                                        res = ActualizarConsumo_Factura()
                                                        Select Case res
                                                            Case 0
                                                                Cancelar_Tran()
                                                                Util.MsgStatus(Status1, "Se produjo un error al intentar acutalizar el estado del Consumo y la Factura asociada.", My.Resources.Resources.stop_error.ToBitmap)
                                                                Util.MsgStatus(Status1, "Se produjo un error al intentar acutalizar el estado del Consumo y la Factura asociada.", My.Resources.Resources.stop_error.ToBitmap, True)
                                                            Case 1
                                                                Util.MsgStatus(Status1, "Factura asociada a Consumo de manera correcta.", My.Resources.Resources.stop_error.ToBitmap)
                                                        End Select
                                                End Select
                                            End If

                                            Cerrar_Tran()
                                            Imprimir()

                                            If ChkPago.Checked = True Then
                                                frmPagodeClientes_Contado.btnNuevo_Click(sender, e)

                                                frmPagodeClientes_Contado.lblTotalaPagar.Text = txtTotal.Text  ' totalfactura.ToString
                                                frmPagodeClientes_Contado.lblTotalaPagarSinIva.Text = txtSubtotal.Text 'subtotal.ToString
                                                frmPagodeClientes_Contado.IdCliente = cmbCliente.SelectedValue
                                                frmPagodeClientes_Contado.FechaVta = dtpFECHA.Value
                                                frmPagodeClientes_Contado.MontoIva = txtIvaTotal.Text  'totaliva.ToString
                                                frmPagodeClientes_Contado.PorcIva = txtIVA.Text
                                                frmPagodeClientes_Contado.txtIdConsumo.Text = txtID.Text ' idfactura.ToString
                                                frmPagodeClientes_Contado.txtIdFacturacion.Text = txtIdFactura.Text
                                                frmPagodeClientes_Contado.ShowDialog(Me)

                                                res = ActualizarConsumo_Pagado()
                                                Select Case res
                                                    Case 0
                                                        Cancelar_Tran()
                                                        Util.MsgStatus(Status1, "No se pudo actualizar el estado del consumo a Facturado.", My.Resources.Resources.stop_error.ToBitmap)
                                                        Util.MsgStatus(Status1, "No se pudo actualizar el estado del consumo a Facturado.", My.Resources.Resources.stop_error.ToBitmap, True)
                                                    Case Else
                                                        Util.MsgStatus(Status1, "Consumo actualizado correctamente.", My.Resources.Resources.stop_error.ToBitmap)
                                                End Select

                                            End If

                                            band = 0
                                            bolModo = False
                                            btnActualizar_Click(sender, e)
                                            Util.MsgStatus(Status1, "El Movimiento se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                                            band = 1
                                    End Select

                                Else
                                    If bolModo = False Then
                                        EliminarItems_Consumo()
                                    End If

                                    Cerrar_Tran()
                                    Imprimir()

                                    If ChkPago.Checked = True Then
                                        frmPagodeClientes_Contado.btnNuevo_Click(sender, e)

                                        frmPagodeClientes_Contado.lblTotalaPagar.Text = txtTotal.Text  ' totalfactura.ToString
                                        frmPagodeClientes_Contado.lblTotalaPagarSinIva.Text = txtSubtotal.Text 'subtotal.ToString
                                        frmPagodeClientes_Contado.IdCliente = cmbCliente.SelectedValue
                                        frmPagodeClientes_Contado.FechaVta = dtpFECHA.Value
                                        frmPagodeClientes_Contado.MontoIva = txtIvaTotal.Text  'totaliva.ToString
                                        frmPagodeClientes_Contado.PorcIva = txtIVA.Text
                                        frmPagodeClientes_Contado.txtIdFacturacion.Text = txtID.Text ' idfactura.ToString
                                        frmPagodeClientes_Contado.ShowDialog(Me)
                                    End If

                                    band = 0
                                    bolModo = False
                                    btnActualizar_Click(sender, e)
                                    Util.MsgStatus(Status1, "El Movimiento se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                                    band = 1
                                End If
                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If

            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        Dim registro As Integer 'DataGridViewRow
        registro = grd.CurrentRow.Index

        If Not bolModo Then
            If MessageBox.Show("Esta acción reversará los Movimiento de todos los items." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    Case -6
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el Movimiento.", My.Resources.stop_error.ToBitmap)
                    Case -5
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el detalle del Movimiento.", My.Resources.stop_error.ToBitmap)
                    Case -4
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró la actualizacion al stock", My.Resources.stop_error.ToBitmap)
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se registró el mov. de stock.", My.Resources.stop_error.ToBitmap)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                    Case Else
                        Cerrar_Tran()
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        Setear_Grilla()
                        grd.Rows(registro).Selected = True
                        grd.CurrentCell = grd.Rows(registro).Cells(1)
                        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True)
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        nbreformreportes = "Venta - Consumo"

        Dim paramConsumos As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigopres As String
        Dim Rpt As New frmReportes

        paramConsumos.AgregarParametros("N° de Movimiento:", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)

        paramConsumos.ShowDialog()
        If cerroparametrosconaceptar = True Then
            codigopres = paramConsumos.ObtenerParametros(0)

            Rpt.MostrarReporte_VentaConsumo(codigopres, False, Rpt)

            cerroparametrosconaceptar = False
            paramConsumos = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If txtID.Text <> "" Then
            LlenarGridItems(CType(txtID.Text, Long))
            Contar_Filas()
            Try
                grdItems.Enabled = Not CBool(grd.CurrentRow.Cells(15).Value)
                GroupBox1.Enabled = Not CBool(grd.CurrentRow.Cells(15).Value)
            Catch ex As Exception

            End Try
        End If
    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Ventas y Consumos"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        'Dim p As New Size(GroupBox1.Size.Width, 120) '200'AltoMinimoGrilla)
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

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', state As Integer


        Dim codigo, nombre, nombrelargo, tipo, observaciones As String 'ubicacion,
        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : observaciones = ""  'ubicacion = ""

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si se terminó de editar la celda...
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If editando_celda Then
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si todos los combox tienen algo válido
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not (cmbCliente.SelectedIndex > -1) Then
            Util.MsgStatus(Status1, "Ingrese un valor en 'Cliente'.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Ingrese un valor en 'Cliente'.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If chkRecDescGlobal.Checked = True Then
            If txtporcrecargo.Text = "" Then
                Util.MsgStatus(Status1, "Ingrese un valor para el Recargo o Descuento Global.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "Ingrese un valor para el Recargo o Descuento Global.", My.Resources.Resources.alert.ToBitmap, True)
                txtporcrecargo.Focus()
                Exit Sub
            End If
        End If

        If chkEntrega.Checked = True And cmbEntregaren.Text = "" Then
            Util.MsgStatus(Status1, "Ingrese un valor para el sitio de Entrega.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Ingrese un valor para el sitio de Entrega.", My.Resources.Resources.alert.ToBitmap, True)
            cmbEntregaren.Focus()
            Exit Sub
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'verificar que no hay nada en la grilla sin datos
        j = grdItems.RowCount - 1
        filas = 0
        For i = 0 To j
            'state = grdItems.Rows.GetRowState(i)
            'la fila está vacía ?
            If fila_vacia(i) Then
                Try
                    'encotramos una fila vacia...borrarla y ver si hay mas
                    grdItems.Rows.RemoveAt(i)

                    j = j - 1 ' se reduce la cantidad de filas en 1
                    i = i - 1 ' se reduce para recorrer la fila que viene 
                Catch ex As Exception
                End Try

            Else
                filas = filas + 1

                ''idmaterial es valido?
                'If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Then
                '    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                '    Exit Sub
                'End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'qty es válida?
                Try
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Then
                        Util.MsgStatus(Status1, "Falta completar la cantidad a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "Falta completar la cantidad a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    ElseIf grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value <= 0 Then
                        Util.MsgStatus(Status1, "La cantidad a Consumir debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "La cantidad a Consumir debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                        Exit Sub
                    End If
                Catch ex As Exception
                    Util.MsgStatus(Status1, "Ingrese una cantidad válida a Consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Ingrese una cantidad válida a Consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End Try

                'unidad es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar la unidad del material a consumir en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next i



        For i = 0 To grdItems.RowCount - 2
            Dim cuentafilas As Integer
            Dim codigo_mat As String = "", codigo_mat_2 As String = ""
            'codigo_mat = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value
            codigo_mat = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value)
            For cuentafilas = i + 1 To grdItems.RowCount - 2
                If grdItems.RowCount - 1 > 1 Then
                    'codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
                    codigo_mat_2 = IIf(grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value)
                    If codigo_mat <> "" And codigo_mat_2 <> "" Then
                        If codigo_mat = codigo_mat_2 Then
                            Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "El código '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If
                    End If
                End If
            Next
        Next

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If filas > 0 Then
            bolpoliticas = True
        Else
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "3"
        rdVenta.Tag = "4"
        txtSubtotal.Tag = "6"
        txtIVA.Tag = "7"
        txtIvaTotal.Tag = "8"
        txtTotal.Tag = "9"
        cmbCliente.Tag = "11"
        chkRecDescGlobal.Tag = "12"
        chkRecargo.Tag = "13"
        txtporcrecargo.Tag = "14"
        cmbComprador.Tag = "17"
        chkEntrega.Tag = "18"
        cmbEntregaren.Tag = "19"
        cmbVendedor.Tag = "21"
        txtNotaGestion.Tag = "22"
        chkNotas.Tag = "23"
        chkEliminado.Tag = "24"
        chkCerrar.Tag = "25"
        rdConsumoInterno.Tag = "26"

        chkFactura.Tag = "29"
        txtFactura.Tag = "30"
        chkOC.Tag = "31"
        txtOC.Tag = "32"

    End Sub

    Private Sub validar_NumerosReales2( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridItems.Qty Or columna = ColumnasDelGridItems.PrecioVta Or columna = ColumnasDelGridItems.Stock Then

            Dim caracter As Char = e.KeyChar

            ' referencia a la celda  
            Dim txt As TextBox = CType(sender, TextBox)

            ' comprobar si es un número con isNumber, si es el backspace, si el caracter  
            ' es el separador decimal, y que no contiene ya el separador  
            If (Char.IsNumber(caracter)) Or _
               (caracter = ChrW(Keys.Back)) Or _
               (caracter = ".") And _
               (txt.Text.Contains(".") = False) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
    End Sub

    Private Sub LlenarGridItems(ByVal id As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        'If txtID.Text = "" Then
        'If id = 0 Then
        ' SQL = " select '' as 'IDPresup_Det', '' as 'IdMaterial', '' as 'Cod. Material','' as 'Material', '' as 'IDUnidad', '' as 'Unidad', '' as 'Unidad2', 0.00 as 'Minimo', 0.00 as 'Maximo', convert(decimal(18,2),0) as 'Stock', convert(decimal(18,2),0) as 'GC' , 0.00 AS 'Precio Uni.', 0.00 as 'Precio Vta', 0 as 'Rec/Desc', 0.00 as '% Rec/Des', 0.00 as 'Cant.', 0.00 AS 'Subtotal', '' AS 'Ult. Act.', 0.00 AS 'PrecioVtaOrig',0.00 AS 'GananciaOrig', '' AS 'Nota_Det' "
        ' Else
        SQL = "exec sp_Consumos_Det_Select_By_IdConsumo @idConsumo = " & id 'txtID.Text
        'End If

        GetDatasetItems()

        grdItems.Columns(ColumnasDelGridItems.IdConsumo_Det).Visible = False

        grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Material).ReadOnly = False  'Codigo material
        grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 70

        'grdItems.Columns(ColumnasDelGridItems.Nombre_Material).ReadOnly = True 'Material
        grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 260

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True 'qty
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 70
        grdItems.Columns(ColumnasDelGridItems.Qty).Visible = True

        grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.Unidad).ReadOnly = True 'Unidad'
        grdItems.Columns(ColumnasDelGridItems.Unidad).Width = 70
        'grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = True 'stock'
        grdItems.Columns(ColumnasDelGridItems.Stock).Width = 50

        grdItems.Columns(ColumnasDelGridItems.Minimo).Visible = False

        grdItems.Columns(ColumnasDelGridItems.Maximo).Visible = False

        grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.PrecioLista).Width = 55
        grdItems.Columns(ColumnasDelGridItems.PrecioLista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Ganancia).Width = 50
        grdItems.Columns(ColumnasDelGridItems.Ganancia).ReadOnly = True
        grdItems.Columns(ColumnasDelGridItems.Ganancia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.PrecioUni).ReadOnly = True 'precio unitario
        grdItems.Columns(ColumnasDelGridItems.PrecioUni).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioUni).Visible = False
        grdItems.Columns(ColumnasDelGridItems.PrecioUni).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.PrecioVta).ReadOnly = True 'precio unitario
        grdItems.Columns(ColumnasDelGridItems.PrecioVta).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PrecioVta).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.RecDesc).Width = 70
        grdItems.Columns(ColumnasDelGridItems.RecDesc).ReadOnly = False 'precio unitario
        grdItems.Columns(ColumnasDelGridItems.RecDesc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.PorcRecDesc).Width = 60
        grdItems.Columns(ColumnasDelGridItems.PorcRecDesc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = False 'maximo'
        grdItems.Columns(ColumnasDelGridItems.Qty).Width = 60
        grdItems.Columns(ColumnasDelGridItems.Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.SubTotalProd).ReadOnly = True 'subtotal
        grdItems.Columns(ColumnasDelGridItems.SubTotalProd).Width = 70
        grdItems.Columns(ColumnasDelGridItems.SubTotalProd).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        grdItems.Columns(ColumnasDelGridItems.dateupd).ReadOnly = True 'maximo'
        grdItems.Columns(ColumnasDelGridItems.dateupd).Width = 80
        grdItems.Columns(ColumnasDelGridItems.dateupd).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        grdItems.Columns(ColumnasDelGridItems.preciovtaorig).Visible = False
        grdItems.Columns(ColumnasDelGridItems.gananciaorig).Visible = False

        'grdItems.Columns(ColumnasDelGridItems.nota_det).Width = 250

        With grdItems
            .VirtualMode = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
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

        'InicializarGridItems(grdItems)

        If txtID.Text <> "" Then
            ControlarLista()
        End If

        'Contar_Filas()

        'Volver la fuente de datos a como estaba...
        SQL = "exec sp_Consumos_Select_All"
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
            .VirtualMode = False
            .BackgroundColor = SystemColors.ActiveBorder 'Color.DarkGray ' color del fondo del grid...
            .BorderStyle = BorderStyle.Fixed3D
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
            .AllowUserToAddRows = True 'indica si se muestra al usuario la opción de agregar filas
            .AllowUserToDeleteRows = True 'indica si el usuario puede eliminar filas de DataGridView.
            .AllowUserToOrderColumns = False 'indica si el usuario puede cambiar manualmente de lugar las columnas..
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect   'indica cómo se pueden seleccionar las celdas de DataGridView.
            .MultiSelect = False 'indica si el usuario puede seleccionar a la vez varias celdas, filas o columnas del control DataGridView.
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders   'indica cómo se determina el alto de las filas. 
            .AllowUserToResizeColumns = True 'indica si los usuarios pueden cambiar el tamaño de las columnas.
            .AllowUserToResizeRows = True 'indica si los usuarios pueden cambiar el tamaño de las filas.
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize 'indica si el alto de los encabezados de columna es ajustable y si puede ser ajustado por el usuario o automáticamente para adaptarse al contenido de los encabezados. 
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

    Private Sub GetDatasetItems()
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()

            grdItems.DataSource = ds_2.Tables(0).DefaultView

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

    Private Sub Setear_Grilla()
        'ordernar descendente
        grd.Sort(grd.Columns(1), System.ComponentModel.ListSortDirection.Descending)

        ''setear grilla de items
        'With grdItems
        '    .VirtualMode = False
        '    .AllowUserToAddRows = False
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.MintCream
        '    .RowsDefaultCellStyle.BackColor = Color.White
        '    .AllowUserToOrderColumns = True
        '    .SelectionMode = DataGridViewSelectionMode.CellSelect
        'End With
    End Sub

    Private Sub LlenarcmbClientes()
        Dim ds_Clientes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

        Try
            ds_Clientes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, (nombre + ' ( ' + codigo + ' )') as codigo FROM Clientes WHERE Eliminado = 0 ORDER BY nombre")
            ds_Clientes.Dispose()

            With cmbCliente
                .DataSource = ds_Clientes.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .TabStop = True
            End With

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

        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarLista()
        Dim ds_Notas As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

        Try

            ds_Notas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT nota FROM Notas")
            ds_Notas.Dispose()

            Dim i As Integer ', j As Integer

            'For i = 0 To lstNotas.Items.Count - 1
            lstNotas.Items.Clear()
            'Next

            Try
                Dim item As New ListViewItem
                For i = 0 To ds_Notas.Tables(0).Rows.Count - 1
                    Dim r As DataRow = ds_Notas.Tables(0).Rows(i)
                    item = lstNotas.Items.Add(CStr(r("nota")))
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'For i = 0 To lstNotas.Items.Count - 1
            '    For j = 0 To ds_Notas.Tables(0).Rows.Count - 1
            '        If lstNotas.Items(i).Text = ds_Notas.Tables(0).Rows(j).Item(0) Then
            '            lstNotas.Items(i).Checked = True
            '        End If
            '    Next
            'Next

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

        End Try

        llenandoCombo = False

    End Sub

    Private Sub ControlarLista()
        Dim ds_Notas As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

        Try

            ds_Notas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT nota FROM Consumos_Notas WHERE idConsumo = " & txtID.Text)
            ds_Notas.Dispose()

            Dim i As Integer, j As Integer

            For i = 0 To lstNotas.Items.Count - 1
                For j = 0 To ds_Notas.Tables(0).Rows.Count - 1
                    If lstNotas.Items(i).Text = ds_Notas.Tables(0).Rows(j).Item(0) Then
                        lstNotas.Items(i).Checked = True
                    End If
                Next
            Next

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

        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbMateriales()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Materiales As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo, rtrim(nombre) as Nombre FROM Materiales WHERE Eliminado = 0 ORDER BY nombre")
            ds_Materiales.Dispose()

            With Me.BuscarDescripcionToolStripMenuItem.ComboBox
                .DataSource = ds_Materiales.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "Codigo"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .BindingContext = Me.BindingContext
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

        End Try

    End Sub

    Private Sub LlenarcmbUnidadesVta()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_UnidadesVta As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_UnidadesVta = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo, NOMBRE as unidad FROM Unidades")
            ds_UnidadesVta.Dispose()

            With Me.cmbUnidadVenta.ComboBox
                .DataSource = ds_UnidadesVta.Tables(0).DefaultView
                .DisplayMember = "UNIDAD"
                .ValueMember = "codigo"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .BindingContext = Me.BindingContext
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

        End Try

    End Sub

    Private Sub VerGrilla()

        'MsgBox("group location: " + Me.GroupBox1.Location.ToString + " width: " + Me.GroupBox1.Size.Width.ToString + " Height: " + Me.GroupBox1.Size.Height.ToString)

        'Dim p2 As New Size(GroupBox1.Size.Width, GroupBox1.Size.Height - grd.Size.Height - 20) '200'AltoMinimoGrilla)
        Dim p2 As New Size(GroupBox1.Size.Width, 390) '200'AltoMinimoGrilla)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, GroupBox1.Size.Height - 100) '200'AltoMinimoGrilla)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 150) '200'AltoMinimoGrilla)
        Me.grd.Size = New Size(p)

        Me.grd.Visible = True

    End Sub

    Private Sub OcultarGrilla()
        Dim p2 As New Size(GroupBox1.Size.Width, 560) '200'AltoMinimoGrilla)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, 462) '200'AltoMinimoGrilla)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Visible = False
    End Sub

    Private Sub LlenarcmbClientes_Comprador()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT CC.ID as IdComprador, NOMBRE_CONTACTO as Comprador FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE c.eliminado = 0 and cc.ELIMINADO = 0  AND C.ID = " & CType(cmbCliente.SelectedValue, Long))
            ds.Dispose()

            With cmbComprador
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Comprador"
                .ValueMember = "IdComprador"
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

    'Private Sub LlenarcmbClientes_Usuario()
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim ds As Data.DataSet

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try

    '        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT CC.ID as IdUsuario, NOMBRE_CONTACTO as Usuario FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & CType(cmbCliente.SelectedValue, Long))
    '        ds.Dispose()

    '        With cmbUsuario
    '            .DataSource = ds.Tables(0).DefaultView
    '            .DisplayMember = "Usuario"
    '            .ValueMember = "IdUsuario"
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

    Private Sub LlenarcmbEntregar()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Distinct SitioEntrega FROM Consumos ORDER BY SITIOENTREGA")
            ds.Dispose()

            With cmbEntregaren
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "SitioEntrega"
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

    Private Sub BuscarPorcentajeRecargo()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT porcrecargo FROM CLIENTES C WHERE id = " & CType(cmbCliente.SelectedValue, Long))
            ds.Dispose()

            txtporcrecargo.Text = ds.Tables(0).Rows(0).Item(0).ToString

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

    Private Sub Imprimir()
        nbreformreportes = "Consumo"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        Rpt.MostrarReporte_VentaConsumo(txtCODIGO.Text, False, Rpt)

        cnn = Nothing

    End Sub

    Private Sub Contar_Filas()
        Dim i As Integer, j As Integer = 0

        For i = 0 To 16
            Try
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value <> Nothing Then
                    j = j + 1
                End If
            Catch ex As Exception

            End Try
        Next

        lblCantidadFilas.Text = j.ToString + " / 16"

        If j = 16 Then
            grdItems.AllowUserToAddRows = False
        Else
            grdItems.AllowUserToAddRows = True
        End If

    End Sub

    Private Sub Calcular_RecargoDescuento()
        If band = 1 Then
            Dim i As Integer
            If txtporcrecargo.Text <> "" And txtporcrecargo.Text <> "0" Then
                Dim montorecargo As Double ', montoganancia As Double

                txtSubtotal.Text = "0"

                If grdItems.Rows.Count > 1 Then
                    Band_RecDesc = True
                    For i = 0 To grdItems.Rows.Count - 2
                        If chkDesc.Checked = True Then
                            montorecargo = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value / (1 + (CDbl(txtporcrecargo.Text) / 100)), 2)
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = 1 'True
                        Else
                            If chkRecargo.Checked = True Then
                                montorecargo = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value * (1 + (CDbl(txtporcrecargo.Text) / 100)), 2)
                                grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = 0 'False
                            End If
                        End If

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = montorecargo 'FormatNumber(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value + montorecargo, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value = txtporcrecargo.Text
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = Math.Round(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, 2)

                        txtSubtotal.Text = CDbl(txtSubtotal.Text) + grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value

                    Next
                    Band_RecDesc = False
                End If
            Else
                txtSubtotal.Text = "0"

                If grdItems.Rows.Count > 1 Then
                    Band_RecDesc = True

                    For i = 0 To grdItems.Rows.Count - 2
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value * 100
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = 0
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0

                        Try
                            grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = Math.Round(CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value) * CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value), 2)
                        Catch ex As Exception
                            ' MsgBox(Err.Description)
                        End Try

                        Try
                            txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value)
                        Catch ex As Exception
                            'Band_RecDesc = False
                        End Try

                    Next

                    Band_RecDesc = False

                End If
            End If

            txtIvaTotal.Text = Math.Round(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
            txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtIvaTotal.Text)
        End If
    End Sub

#End Region

#Region "Funciones"

    Private Function ControlarVersion() As Integer
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_cant As New SqlClient.SqlParameter
                param_cant.ParameterName = "@cant"
                param_cant.SqlDbType = SqlDbType.Int
                param_cant.Value = DBNull.Value
                param_cant.Direction = ParameterDirection.InputOutput

                Dim param_IVA As New SqlClient.SqlParameter
                param_IVA.ParameterName = "@iva"
                param_IVA.SqlDbType = SqlDbType.Decimal
                param_IVA.Precision = 18
                param_IVA.Scale = 2
                param_IVA.Value = DBNull.Value
                param_IVA.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Consumos_ControlarFilas", _
                                              param_id, param_cant, param_IVA)

                    If param_cant.Value <> grdItems.RowCount - 1 Then
                        ControlarVersion = 1
                        Exit Function
                    End If

                    If param_IVA.Value <> FormatNumber(txtIVA.Text, 2) Then
                        ControlarVersion = 3
                        Exit Function
                    End If

                Catch ex As Exception
                    Throw ex
                End Try

                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT idmaterial, qty, precioVTA FROM Consumos_det WHERE idConsumo = " & txtID.Text & "ORDER BY idmaterial ")
                ds.Dispose()

                Dim i As Integer, j As Integer
                Dim iguales As Boolean

                For i = 0 To grdItems.RowCount - 2
                    For j = 0 To ds.Tables(0).Rows.Count - 1
                        If CLng(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value) = CLng(ds.Tables(0).Rows(j).Item(0)) Then
                            If FormatNumber(CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value), 2) = FormatNumber(CDbl(ds.Tables(0).Rows(j).Item(1)), 2) Then
                                If FormatNumber(CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value), 2) = FormatNumber(CDbl(ds.Tables(0).Rows(j).Item(2)), 2) Then
                                    iguales = True
                                    Exit For
                                Else
                                    iguales = False
                                    Exit For
                                End If
                            Else
                                iguales = False
                            End If
                        Else
                            iguales = False
                        End If
                    Next
                    If iguales = False Then
                        Exit For
                    End If
                Next

                If iguales = False Then
                    ControlarVersion = 1
                Else
                    ControlarVersion = 2
                End If

            Finally

            End Try
        Catch ex As Exception

            ControlarVersion = -1

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

    Private Function BuscarRevision() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_rev As New SqlClient.SqlParameter
                param_rev.ParameterName = "@nrorev"
                param_rev.SqlDbType = SqlDbType.Int
                param_rev.Value = DBNull.Value
                param_rev.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Consumos_BuscarRevision", _
                                              param_id, param_rev)

                    BuscarRevision = param_rev.Value

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

    Private Function AgregarActualizar_Registro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try
                'Dim nuevarevision As Integer
                'Revision = 0

                'If bolModo = False Then
                '    Dim resultadorevision As Integer
                '    resultadorevision = ControlarVersion()
                '    If resultadorevision = 1 Or resultadorevision = 3 Then
                '        If MessageBox.Show("Se han producido cambios en el Consumo." + vbCrLf + "¿Desea generar una nueva revisión?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            nuevarevision = BuscarRevision() + 1
                '            txtRevision.Text = nuevarevision
                '            Revision = 1
                '        End If
                '    End If

                '    If resultadorevision = -1 Then
                '        MessageBox.Show("Se produjo un error al intentar controlar la revisión", "Control de Revisión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        AgregarActualizar_Registro = 20
                '        Exit Function
                '    End If

                'End If

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

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                param_codigo.Value = DBNull.Value
                If bolModo = True Then
                    param_codigo.Direction = ParameterDirection.InputOutput
                Else
                    param_codigo.Direction = ParameterDirection.Input
                End If

                Dim param_idCliente As New SqlClient.SqlParameter
                param_idCliente.ParameterName = "@idCliente"
                param_idCliente.SqlDbType = SqlDbType.BigInt
                param_idCliente.Value = cmbCliente.SelectedValue
                param_idCliente.Direction = ParameterDirection.Input

                Dim param_Comprador As New SqlClient.SqlParameter
                param_Comprador.ParameterName = "@Comprador"
                param_Comprador.SqlDbType = SqlDbType.Bit
                param_Comprador.Value = chkRetiradopor.Checked
                param_Comprador.Direction = ParameterDirection.Input

                Dim param_idComprador As New SqlClient.SqlParameter
                param_idComprador.ParameterName = "@idContacto_Comprador"
                param_idComprador.SqlDbType = SqlDbType.BigInt
                param_idComprador.Value = IIf(chkRetiradopor.Checked = True, cmbComprador.SelectedValue, 0)
                param_idComprador.Direction = ParameterDirection.Input

                Dim param_Entregaren As New SqlClient.SqlParameter
                param_Entregaren.ParameterName = "@Entregaren"
                param_Entregaren.SqlDbType = SqlDbType.Bit
                param_Entregaren.Value = chkEntrega.Checked
                param_Entregaren.Direction = ParameterDirection.Input

                Dim param_sitioentrega As New SqlClient.SqlParameter
                param_sitioentrega.ParameterName = "@sitioentrega"
                param_sitioentrega.SqlDbType = SqlDbType.VarChar
                param_sitioentrega.Size = 25
                param_sitioentrega.Value = cmbEntregaren.Text
                param_sitioentrega.Direction = ParameterDirection.Input

                'Dim param_incluyeremito As New SqlClient.SqlParameter
                'param_incluyeremito.ParameterName = "@incluyeremito"
                'param_incluyeremito.SqlDbType = SqlDbType.Bit
                'param_incluyeremito.Value = chkRemito.Checked
                'param_incluyeremito.Direction = ParameterDirection.Input

                'Dim param_remito As New SqlClient.SqlParameter
                'param_remito.ParameterName = "@nroremito"
                'param_remito.SqlDbType = SqlDbType.VarChar
                'param_remito.Size = 50
                'param_remito.Value = txtRemito.Text
                'param_remito.Direction = ParameterDirection.Input


                Dim param_condicionVta As New SqlClient.SqlParameter
                param_condicionVta.ParameterName = "@CondicionVta"
                param_condicionVta.SqlDbType = SqlDbType.VarChar
                param_condicionVta.Size = 20
                If ChkPago.Checked = True Then
                    param_condicionVta.Value = "Contado/Efectivo"
                Else
                    param_condicionVta.Value = DBNull.Value
                End If
                param_condicionVta.Direction = ParameterDirection.Input

                Dim param_condicionIVA As New SqlClient.SqlParameter
                param_condicionIVA.ParameterName = "@CondicionIVA"
                param_condicionIVA.SqlDbType = SqlDbType.VarChar
                param_condicionIVA.Size = 25
                If ChkPago.Checked = True Then
                    param_condicionIVA.Value = cmbCondIVA.Text
                Else
                    param_condicionIVA.Value = DBNull.Value
                End If
                param_condicionIVA.Direction = ParameterDirection.Input

                'Dim param_incluyefactura As New SqlClient.SqlParameter
                'param_incluyefactura.ParameterName = "@incluyefactura"
                'param_incluyefactura.SqlDbType = SqlDbType.Bit
                'If bolModo = True Then
                'Else

                'End If
                'param_incluyefactura.Value = DBNull.Value
                'param_incluyefactura.Direction = ParameterDirection.Input

                'Dim param_factura As New SqlClient.SqlParameter
                'param_factura.ParameterName = "@nrofactura"
                'param_factura.SqlDbType = SqlDbType.VarChar
                'param_factura.Size = 50
                'param_factura.Value = txtFactura.Text
                'param_factura.Direction = ParameterDirection.Input

                Dim param_oc As New SqlClient.SqlParameter
                param_oc.ParameterName = "@oc"
                param_oc.SqlDbType = SqlDbType.Bit
                param_oc.Value = chkOC.Checked
                param_oc.Direction = ParameterDirection.Input

                Dim param_nrooc As New SqlClient.SqlParameter
                param_nrooc.ParameterName = "@nrooc"
                param_nrooc.SqlDbType = SqlDbType.VarChar
                param_nrooc.Size = 50
                param_nrooc.Value = txtOC.Text
                param_nrooc.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = CType(txtSubtotal.Text, Double)
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = txtIVA.Text
                param_iva.Direction = ParameterDirection.Input

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@Montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = txtIvaTotal.Text
                param_montoiva.Direction = ParameterDirection.Input

                Dim param_totalconsumo As New SqlClient.SqlParameter
                param_totalconsumo.ParameterName = "@Total"
                param_totalconsumo.SqlDbType = SqlDbType.Decimal
                param_totalconsumo.Precision = 18
                param_totalconsumo.Scale = 2
                param_totalconsumo.Value = txtTotal.Text
                param_totalconsumo.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 100
                param_nota.Value = txtNotaGestion.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_venta As New SqlClient.SqlParameter
                param_venta.ParameterName = "@venta"
                param_venta.SqlDbType = SqlDbType.Bit
                param_venta.Value = rdVenta.Checked
                param_venta.Direction = ParameterDirection.Input

                Dim param_ctacte As New SqlClient.SqlParameter
                param_ctacte.ParameterName = "@ctacte"
                param_ctacte.SqlDbType = SqlDbType.Bit
                param_ctacte.Value = chkCerrar.Checked
                param_ctacte.Direction = ParameterDirection.Input

                'Dim param_facturado As New SqlClient.SqlParameter
                'param_facturado.ParameterName = "@facturado"
                'param_facturado.SqlDbType = SqlDbType.Bit
                'param_facturado.Value = ChkPago.Checked
                'param_facturado.Direction = ParameterDirection.Input

                Dim param_idvendedor As New SqlClient.SqlParameter
                param_idvendedor.ParameterName = "@uservendedor"
                param_idvendedor.SqlDbType = SqlDbType.BigInt
                param_idvendedor.Value = cmbVendedor.SelectedValue
                param_idvendedor.Direction = ParameterDirection.Input

                Dim param_RecDescGlobal As New SqlClient.SqlParameter
                param_RecDescGlobal.ParameterName = "@RecDescGobal"
                param_RecDescGlobal.SqlDbType = SqlDbType.Bit
                param_RecDescGlobal.Value = chkRecDescGlobal.Checked
                param_RecDescGlobal.Direction = ParameterDirection.Input

                Dim param_RecargoDesc As New SqlClient.SqlParameter
                param_RecargoDesc.ParameterName = "@RecargoDesc"
                param_RecargoDesc.SqlDbType = SqlDbType.Bit
                If chkRecDescGlobal.Checked = True Then
                    param_RecargoDesc.Value = Not chkRecargo.Checked
                End If
                param_RecargoDesc.Direction = ParameterDirection.Input

                Dim param_porcrecargo As New SqlClient.SqlParameter
                param_porcrecargo.ParameterName = "@porcrecargo"
                param_porcrecargo.SqlDbType = SqlDbType.Decimal
                param_porcrecargo.Precision = 18
                param_porcrecargo.Scale = 2
                param_porcrecargo.Value = IIf(txtporcrecargo.Text = "", 0, txtporcrecargo.Text)
                param_porcrecargo.Direction = ParameterDirection.Input

                Dim param_incluyenotas As New SqlClient.SqlParameter
                param_incluyenotas.ParameterName = "@incluyenotas"
                param_incluyenotas.SqlDbType = SqlDbType.Bit
                param_incluyenotas.Value = chkNotas.Checked
                param_incluyenotas.Direction = ParameterDirection.Input

                Dim param_cerrado As New SqlClient.SqlParameter
                param_cerrado.ParameterName = "@cerrado"
                param_cerrado.SqlDbType = SqlDbType.Bit
                If chkCerrar.Checked = True Or ChkPago.Checked = True Then
                    param_cerrado.Value = chkCerrar.Checked
                Else
                    param_cerrado.Value = False
                End If
                param_cerrado.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Insert", _
                                            param_id, param_fecha, param_codigo, param_idCliente, param_Comprador, _
                                            param_idComprador, param_Entregaren, param_sitioentrega, _
                                            param_subtotal, param_iva, param_montoiva, param_totalconsumo, _
                                            param_oc, param_nrooc, _
                                            param_nota, param_venta, param_ctacte, param_idvendedor, _
                                            param_RecDescGlobal, param_RecargoDesc, param_porcrecargo, param_incluyenotas, _
                                            param_cerrado, param_condicionIVA, param_condicionVta, param_useradd, param_res)


                        txtID.Text = param_id.Value
                        txtCODIGO.Text = param_codigo.Value

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Update", _
                                           param_id, param_fecha, param_codigo, param_idCliente, param_Comprador, _
                                            param_idComprador, param_Entregaren, param_sitioentrega, _
                                            param_subtotal, param_iva, param_montoiva, param_totalconsumo, _
                                            param_oc, param_nrooc, _
                                            param_nota, param_venta, param_ctacte, param_idvendedor, _
                                            param_RecDescGlobal, param_RecargoDesc, param_porcrecargo, param_incluyenotas, _
                                            param_cerrado, param_condicionIVA, param_condicionVta, param_useradd, param_res)

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

    Private Function AgregarRegistro_Items() As Integer
        Dim res As Integer = 0 ', res_del As Integer
        Dim i As Integer

        Try

            Try

                i = 0
                Dim CantidadFilas As Integer

                If grdItems.RowCount = 16 Then
                    CantidadFilas = grdItems.Rows.Count
                Else
                    CantidadFilas = grdItems.Rows.Count - 1
                End If

                Do While i < CantidadFilas
                    Dim id As Long
                    Dim PrecioLista As Double, Ganancia As Double, Bonificacion As Double, GananciaOrig As Double, PrecioVtaOrig As Double

                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                        Dim unidad As Long
                        Dim nombre As String
                        Dim precio As Double ', stock As Double

                        unidad = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value
                        nombre = grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value
                        precio = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value
                        'stock = grdItems.Rows(i).Cells(ColumnasDelGridItems.Stock).Value

                        PrecioLista = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value 'PrecioLista
                        PrecioVtaOrig = grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value 'PrecioLista
                        Ganancia = grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value / 100 '0.4
                        GananciaOrig = grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value '/100 '0.4
                        Bonificacion = 0

                        id = Agregar_Material(unidad, Nothing, nombre, precio, 0, PrecioLista)

                        If id = -1 Then
                            MsgBox("Se produjo un error al insertar el material")
                            AgregarRegistro_Items = -30
                            Exit Function
                        End If

                    Else
                        id = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)
                        Ganancia = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value, Double) / 100
                        GananciaOrig = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value, Double) '/ 100
                        PrecioVtaOrig = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double)
                        PrecioLista = PrecioVtaOrig 'grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value

                    End If

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    If bolModo = False And Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.IdConsumo_Det).Value Is DBNull.Value) Then
                        param_id.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdConsumo_Det).Value, Long)
                    Else
                        param_id.Value = 0
                    End If
                    param_id.Direction = ParameterDirection.Input

                    Dim param_IdConsumo As New SqlClient.SqlParameter
                    param_IdConsumo.ParameterName = "@idConsumo"
                    param_IdConsumo.SqlDbType = SqlDbType.BigInt
                    param_IdConsumo.Value = txtID.Text
                    param_IdConsumo.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.BigInt
                    param_idmaterial.Value = id
                    param_idmaterial.Direction = ParameterDirection.Input

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 2
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.BigInt
                    param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
                    param_idunidad.Direction = ParameterDirection.Input

                    Dim param_preciouni As New SqlClient.SqlParameter
                    param_preciouni.ParameterName = "@preciouni"
                    param_preciouni.SqlDbType = SqlDbType.Decimal
                    param_preciouni.Precision = 18
                    param_preciouni.Scale = 2
                    param_preciouni.Value = 0 'PrecioLista 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioUni).Value, Decimal)
                    param_preciouni.Direction = ParameterDirection.Input

                    Dim param_ganancia As New SqlClient.SqlParameter
                    param_ganancia.ParameterName = "@ganancia"
                    param_ganancia.SqlDbType = SqlDbType.Decimal
                    param_ganancia.Precision = 18
                    param_ganancia.Scale = 2
                    param_ganancia.Value = Ganancia 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value, Decimal)
                    param_ganancia.Direction = ParameterDirection.Input

                    Dim param_gananciaorig As New SqlClient.SqlParameter
                    param_gananciaorig.ParameterName = "@gananciaorig"
                    param_gananciaorig.SqlDbType = SqlDbType.Decimal
                    param_gananciaorig.Precision = 18
                    param_gananciaorig.Scale = 2
                    param_gananciaorig.Value = GananciaOrig 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value, Decimal)
                    param_gananciaorig.Direction = ParameterDirection.Input

                    Dim param_RecargoDesc As New SqlClient.SqlParameter
                    param_RecargoDesc.ParameterName = "@RecargoDesc_Det"
                    param_RecargoDesc.SqlDbType = SqlDbType.Bit
                    If chkRecDescGlobal.Checked = True Then
                        If txtporcrecargo.Text = "" Then
                            txtporcrecargo.Text = 0
                        End If

                        If chkRecargo.Checked = True Then
                            param_RecargoDesc.Value = False
                        Else
                            If chkDesc.Checked = True Then
                                param_RecargoDesc.Value = True
                            End If
                        End If

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = param_RecargoDesc.Value

                    Else
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value Then
                            param_RecargoDesc.Value = False
                        Else
                            If (grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = False) Then
                                param_RecargoDesc.Value = False
                            Else
                                param_RecargoDesc.Value = True
                            End If
                        End If
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = param_RecargoDesc.Value
                    End If

                    param_RecargoDesc.Direction = ParameterDirection.Input

                    Dim param_porcrecargo As New SqlClient.SqlParameter
                    param_porcrecargo.ParameterName = "@porcrecargo_Det"
                    param_porcrecargo.SqlDbType = SqlDbType.Decimal
                    param_porcrecargo.Precision = 18
                    param_porcrecargo.Scale = 2
                    param_porcrecargo.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value)
                    param_porcrecargo.Direction = ParameterDirection.Input

                    Dim param_preciovta As New SqlClient.SqlParameter
                    param_preciovta.ParameterName = "@preciovta"
                    param_preciovta.SqlDbType = SqlDbType.Decimal
                    param_preciovta.Precision = 18
                    param_preciovta.Scale = 2
                    If chkRecDescGlobal.Checked = True Then
                        If txtporcrecargo.Text = "" Then
                            txtporcrecargo.Text = 0
                        End If

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = Not chkRecargo.Checked
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value = txtporcrecargo.Text

                        If chkRecargo.Checked = True Then
                            param_preciovta.Value = Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                        Else
                            param_preciovta.Value = Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) / (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                        End If
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = param_preciovta.Value
                    Else

                        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value Is DBNull.Value) Then
                            param_preciovta.Value = Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                        Else
                            If (grdItems.Rows(i).Cells(ColumnasDelGridItems.RecDesc).Value = False) Then
                                param_preciovta.Value = Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                            Else
                                param_preciovta.Value = Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) / (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                            End If
                        End If
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = param_preciovta.Value 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100))
                        'End If
                        'End If
                    End If
                    param_preciovta.Direction = ParameterDirection.Input

                    Dim param_preciovtaorig As New SqlClient.SqlParameter
                    param_preciovtaorig.ParameterName = "@preciovtaorig"
                    param_preciovtaorig.SqlDbType = SqlDbType.Decimal
                    param_preciovtaorig.Precision = 18
                    param_preciovtaorig.Scale = 2
                    param_preciovtaorig.Value = PrecioVtaOrig 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Decimal)
                    param_preciovtaorig.Direction = ParameterDirection.Input

                    Dim param_subtotal As New SqlClient.SqlParameter
                    param_subtotal.ParameterName = "@subtotal"
                    param_subtotal.SqlDbType = SqlDbType.Decimal
                    param_subtotal.Precision = 18
                    param_subtotal.Scale = 2
                    param_subtotal.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double) * CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, Double)
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = param_subtotal.Value  ' Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double) * CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, Double), 2)
                    param_subtotal.Direction = ParameterDirection.Input

                    Dim param_notadet As New SqlClient.SqlParameter
                    param_notadet.ParameterName = "@nota_det"
                    param_notadet.SqlDbType = SqlDbType.VarChar
                    param_notadet.Size = 100
                    param_notadet.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.nota_det).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.nota_det).Value)
                    param_notadet.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    If bolModo = True Then
                        param_useradd.ParameterName = "@useradd"
                    Else
                        param_useradd.ParameterName = "@userupd"
                    End If
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    Dim param_OrdenItem As New SqlClient.SqlParameter
                    If bolModo = True Then
                        param_OrdenItem.ParameterName = "@OrdenItem"
                    Else
                        param_OrdenItem.ParameterName = "@Orden"
                    End If
                    param_OrdenItem.SqlDbType = SqlDbType.SmallInt
                    If bolModo = True Then
                        param_OrdenItem.Value = i + 1
                    Else
                        param_OrdenItem.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.orden).Value Is DBNull.Value, i + 1, grdItems.Rows(i).Cells(ColumnasDelGridItems.orden).Value)
                    End If
                    param_OrdenItem.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Dim param_MENSAJE As New SqlClient.SqlParameter
                    param_MENSAJE.ParameterName = "@MENSAJE"
                    param_MENSAJE.SqlDbType = SqlDbType.VarChar
                    param_MENSAJE.Size = 200
                    param_MENSAJE.Value = DBNull.Value
                    param_MENSAJE.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Det_Insert", _
                                                  param_IdConsumo, param_idmaterial, param_qty, param_idunidad, _
                                                  param_preciouni, param_ganancia, param_gananciaorig, _
                                                  param_preciovta, param_preciovtaorig, param_subtotal, param_notadet, _
                                                  param_RecargoDesc, param_porcrecargo, param_useradd, param_OrdenItem, param_res)

                            res = param_res.Value

                        Else
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Det_Update", _
                                                param_id, param_IdConsumo, param_idmaterial, param_qty, param_idunidad, _
                                                param_preciouni, param_ganancia, param_gananciaorig, _
                                                param_preciovta, param_preciovtaorig, param_subtotal, param_notadet, _
                                                param_RecargoDesc, param_porcrecargo, param_useradd, param_res, _
                                                param_OrdenItem, param_MENSAJE)

                            'MsgBox(param_MENSAJE.Value)

                            res = param_res.Value

                            If res = -20 Then
                                MsgBox("La cantidad ingresada para el Item " & grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value & " es menor al saldo actual.", MsgBoxStyle.Critical, "Atención")
                            End If

                        End If


                        If (res <= 0) Then
                            Exit Do
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try
                    i = i + 1
                Loop

                AgregarRegistro_Items = res

            Catch ex2 As Exception
                Throw ex2
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

    Private Function AgregarRegistro_Notas() As Integer

        Dim res As Integer = 0, i As Integer

        Try
            Try
                If bolModo = False Then

                    Dim param_idConsumo As New SqlClient.SqlParameter
                    param_idConsumo.ParameterName = "@idConsumo"
                    param_idConsumo.SqlDbType = SqlDbType.BigInt
                    param_idConsumo.Value = txtID.Text
                    param_idConsumo.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.Output

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Notas_Delete", param_idConsumo, param_res)
                        res = CInt(param_res.Value)
                        If (res < 0) Then
                            MsgBox("Existe un problema al intentar eliminar temporalmente las notas", MsgBoxStyle.Critical, "Consumos")
                            AgregarRegistro_Notas = res
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                End If

                If lstNotas.Items.Count = 0 Then
                    AgregarRegistro_Notas = 1
                    Exit Function
                End If

                For i = 0 To lstNotas.Items.Count - 1

                    If lstNotas.Items(i).Checked = True Then

                        'Dim param_orden As New SqlClient.SqlParameter
                        'param_orden.ParameterName = "@Orden"
                        'param_orden.SqlDbType = SqlDbType.SmallInt
                        'param_orden.Value = i
                        'param_orden.Direction = ParameterDirection.Input

                        Dim param_idConsumo As New SqlClient.SqlParameter
                        param_idConsumo.ParameterName = "@idConsumo"
                        param_idConsumo.SqlDbType = SqlDbType.BigInt
                        param_idConsumo.Value = txtID.Text
                        param_idConsumo.Direction = ParameterDirection.Input

                        Dim param_nota As New SqlClient.SqlParameter
                        param_nota.ParameterName = "@nota"
                        param_nota.SqlDbType = SqlDbType.VarChar
                        param_nota.Size = 200
                        param_nota.Value = lstNotas.Items(i).Text
                        param_nota.Direction = ParameterDirection.Input

                        Dim param_res As New SqlClient.SqlParameter
                        param_res.ParameterName = "@res"
                        param_res.SqlDbType = SqlDbType.Int
                        param_res.Value = DBNull.Value
                        param_res.Direction = ParameterDirection.Output

                        Try
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Consumos_Notas_Insert", _
                                                      param_idConsumo, param_nota, param_res)

                            res = CInt(param_res.Value)

                            If (res <= 0) Then
                                Exit For
                            End If

                        Catch ex As Exception
                            Throw ex
                        End Try
                    End If

                Next

                AgregarRegistro_Notas = 1


            Catch ex2 As Exception
                Throw ex2
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

    Private Function ControlarCantidadRegistros() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim i As Integer

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Consumos_Det")

            For i = 0 To grdItems.RowCount - 2
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idConsumo"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = txtID.Text
                param_id.Direction = ParameterDirection.Input

                Dim param_idmaterial As New SqlClient.SqlParameter
                param_idmaterial.ParameterName = "@idmaterial"
                param_idmaterial.SqlDbType = SqlDbType.BigInt
                param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                param_idmaterial.Direction = ParameterDirection.Input

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spConsumos_ControlarCantidadRegistros", _
                                            param_id, param_idmaterial)

            Next

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

    Private Function ControlarItemdentrodeRemito(ByVal idmaterial As Long) As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idConsumo"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = txtID.Text
            param_id.Direction = ParameterDirection.Input

            Dim param_idmaterial As New SqlClient.SqlParameter
            param_idmaterial.ParameterName = "@idmaterial"
            param_idmaterial.SqlDbType = SqlDbType.BigInt
            param_idmaterial.Value = idmaterial
            param_idmaterial.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.BigInt
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput


            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spConsumo_ControlarItemDentrodeRemito", _
                                        param_id, param_idmaterial, param_res)

            ControlarItemdentrodeRemito = param_res.Value


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

    Private Function AgregarFactura() As Integer
        Dim res As Integer = 0

        Try

            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_nrofactura As New SqlClient.SqlParameter
                param_nrofactura.ParameterName = "@nrofactura"
                param_nrofactura.SqlDbType = SqlDbType.BigInt
                param_nrofactura.Value = txtFactura.Text
                param_nrofactura.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechafactura"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbCliente.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = txtIVA.Text
                param_iva.Direction = ParameterDirection.Input

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = CDbl(txtIvaTotal.Text)
                param_montoiva.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = CDbl(txtSubtotal.Text)
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = CDbl(txtTotal.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_condicionVta As New SqlClient.SqlParameter
                param_condicionVta.ParameterName = "@CondicionVta"
                param_condicionVta.SqlDbType = SqlDbType.VarChar
                param_condicionVta.Size = 20
                If ChkPago.Checked = True Then
                    param_condicionVta.Value = "Contado/Efectivo"
                Else
                    param_condicionVta.Value = DBNull.Value
                End If
                param_condicionVta.Direction = ParameterDirection.Input

                Dim param_condicionIVA As New SqlClient.SqlParameter
                param_condicionIVA.ParameterName = "@CondicionIVA"
                param_condicionIVA.SqlDbType = SqlDbType.VarChar
                param_condicionIVA.Size = 25
                If ChkPago.Checked = True Then
                    param_condicionIVA.Value = cmbCondIVA.Text
                Else
                    param_condicionIVA.Value = DBNull.Value
                End If
                param_condicionIVA.Direction = ParameterDirection.Input

                Dim param_remitos As New SqlClient.SqlParameter
                param_remitos.ParameterName = "@remitos"
                param_remitos.SqlDbType = SqlDbType.VarChar
                param_remitos.Size = 300
                param_remitos.Value = DBNull.Value
                param_remitos.Direction = ParameterDirection.Input

                Dim param_remitos1 As New SqlClient.SqlParameter
                param_remitos1.ParameterName = "@remitos1"
                param_remitos1.SqlDbType = SqlDbType.VarChar
                param_remitos1.Size = 300
                param_remitos1.Value = DBNull.Value
                param_remitos1.Direction = ParameterDirection.Input

                Dim param_nrocomprobante As New SqlClient.SqlParameter
                param_nrocomprobante.ParameterName = "@nrocomprobante"
                param_nrocomprobante.SqlDbType = SqlDbType.VarChar
                param_nrocomprobante.Size = 100
                param_nrocomprobante.Value = txtOC.Text
                param_nrocomprobante.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = txtNotaGestion.Text ', DBNull.Value, txtComprobante.Text)
                param_nota.Direction = ParameterDirection.Input

                Dim param_Manual As New SqlClient.SqlParameter
                param_Manual.ParameterName = "@Manual"
                param_Manual.SqlDbType = SqlDbType.Bit
                param_Manual.Value = False
                param_Manual.Direction = ParameterDirection.Input

                Dim param_FacturadaAnulada As New SqlClient.SqlParameter
                param_FacturadaAnulada.ParameterName = "@FacturaAnulada"
                param_FacturadaAnulada.SqlDbType = SqlDbType.Bit
                param_FacturadaAnulada.Value = False
                param_FacturadaAnulada.Direction = ParameterDirection.Input

                Dim param_idFacturaAnulada As New SqlClient.SqlParameter
                param_idFacturaAnulada.ParameterName = "@idfacturaAnulada"
                param_idFacturaAnulada.SqlDbType = SqlDbType.BigInt
                param_idFacturaAnulada.Value = DBNull.Value
                param_idFacturaAnulada.Direction = ParameterDirection.Input

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

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spFacturacion_Insert", _
                                            param_id, param_codigo, param_nrofactura, param_fecha, param_idcliente, _
                                            param_iva, param_montoiva, param_subtotal, param_total, param_condicionVta, param_condicionIVA, _
                                            param_remitos, param_remitos1, param_nrocomprobante, param_nota, _
                                            param_Manual, param_FacturadaAnulada, param_idFacturaAnulada, _
                                            param_useradd, param_res)

                    txtIdFactura.Text = param_id.Value

                    AgregarFactura = param_res.Value

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

    Private Function ActualizarConsumo_Factura() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idconsumo"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_idfacturacion As New SqlClient.SqlParameter
            param_idfacturacion.ParameterName = "@idFacturacion"
            param_idfacturacion.SqlDbType = SqlDbType.BigInt
            If chkFactura.Checked = True Then
                param_idfacturacion.Value = CLng(txtIdFactura.Text)
            Else
                param_idfacturacion.Value = DBNull.Value
            End If
            param_idfacturacion.Direction = ParameterDirection.Input

            Dim param_incluyefactura As New SqlClient.SqlParameter
            param_incluyefactura.ParameterName = "@incluyefactura"
            param_incluyefactura.SqlDbType = SqlDbType.Bit
            param_incluyefactura.Value = chkFactura.Checked
            param_incluyefactura.Direction = ParameterDirection.Input

            Dim param_factura As New SqlClient.SqlParameter
            param_factura.ParameterName = "@nrofactura"
            param_factura.SqlDbType = SqlDbType.VarChar
            param_factura.Size = 50
            param_factura.Value = txtFactura.Text
            param_factura.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spConsumos_Facturas_Actualizar", _
                                        param_id, param_incluyefactura, param_idfacturacion, _
                                        param_factura, param_res)

            ActualizarConsumo_Factura = param_res.Value

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function ActualizarConsumo_Pagado() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idconsumo"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spConsumos_Pagado_Actualizar", _
                                        param_id, param_res)

            ActualizarConsumo_Pagado = param_res.Value

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

    Private Function EliminarItems_Consumo() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idConsumo"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spConsumos_EliminarItems_Det", _
                                        param_id, param_res)

            EliminarItems_Consumo = param_res.Value

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
            'Finally
            '    If Not connection Is Nothing Then
            '        CType(connection, IDisposable).Dispose()
            '    End If
        End Try
    End Function

    Private Function EliminarRegistro_VerificarEstado() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Dim param_idconsumo As New SqlClient.SqlParameter("@idConsumo", SqlDbType.BigInt, ParameterDirection.Input)
            param_idconsumo.Value = CType(txtID.Text, Long)
            param_idconsumo.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.Output

            Try

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Consumos_Delete_VerEstado", _
                        param_idconsumo, param_res)

                res = param_res.Value
                EliminarRegistro_VerificarEstado = res

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

            Try

                Dim param_idconsumo As New SqlClient.SqlParameter("@idConsumo", SqlDbType.BigInt, ParameterDirection.Input)
                param_idconsumo.Value = CType(txtID.Text, Long)
                param_idconsumo.Direction = ParameterDirection.Input

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

                Dim param_msg As New SqlClient.SqlParameter
                param_msg.ParameterName = "@mensaje"
                param_msg.SqlDbType = SqlDbType.VarChar
                param_msg.Size = 250
                param_msg.Value = DBNull.Value
                param_msg.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[sp_Consumos_Delete]", _
                            param_idconsumo, param_userdel, param_res, param_msg)

                    res = param_res.Value

                    EliminarRegistro = res

                Catch ex As Exception
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

    Private Function ActualizarConsumoImpreso(ByVal idconsumo As Long) As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

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
                param_id.Value = idconsumo
                param_id.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Consumos_Update_Impreso", param_id, param_res)
                    res = param_res.Value
                    ActualizarConsumoImpreso = res

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

    Private Function fila_vacia(ByVal i) As Boolean
        Try
            If (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is Nothing) _
                                And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is Nothing) _
                                And (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is Nothing) Then
                fila_vacia = True
            Else
                fila_vacia = False
            End If
        Catch ex As Exception
            fila_vacia = True
        End Try

    End Function

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

        ' Si la tecla presionada es distinta de la tecla Enter,
        ' abandonamos el procedimiento.
        '
        If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)

        ' Igualmente, si el control DataGridView no tiene el foco,
        ' y si la celda actual no está siendo editada,
        ' abandonamos el procedimiento.
        If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then Return MyBase.ProcessCmdKey(msg, keyData)

        ' Obtenemos la celda actual
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        ' Índice de la columna.
        Dim columnIndex As Int32 = cell.ColumnIndex
        ' Índice de la fila.
        Dim rowIndex As Int32 = cell.RowIndex

        Do
            If columnIndex = grdItems.Columns.Count - 1 Then
                If rowIndex = grdItems.Rows.Count - 1 Then
                    ' Seleccionamos la primera columna de la primera fila.
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdConsumo_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdConsumo_Det)
                End If
            Else
                ' Seleccionamos la celda de la derecha de la celda actual.
                cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
            End If
            ' establecer la fila y la columna actual
            columnIndex = cell.ColumnIndex
            rowIndex = cell.RowIndex
        Loop While (cell.Visible = False)

        grdItems.CurrentCell = cell

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

    Private Function Agregar_Material(ByVal Unidad As Long, ByVal Codigo As String, ByVal Nombre As String, _
                                     ByVal PrecioVta As Double, ByVal Stock As Double, ByVal preciolista As Double) As Long
        Dim res As Integer = 0
        Dim ultid As Integer = 0

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_idmarca As New SqlClient.SqlParameter
            param_idmarca.ParameterName = "@idmarca"
            param_idmarca.SqlDbType = SqlDbType.BigInt
            param_idmarca.Value = 0
            param_idmarca.Direction = ParameterDirection.Input

            Dim param_idfamilia As New SqlClient.SqlParameter
            param_idfamilia.ParameterName = "@idfamilia"
            param_idfamilia.SqlDbType = SqlDbType.BigInt
            param_idfamilia.Value = 13
            param_idfamilia.Direction = ParameterDirection.Input

            Dim param_idsubrubro As New SqlClient.SqlParameter
            param_idsubrubro.ParameterName = "@idsubrubro"
            param_idsubrubro.SqlDbType = SqlDbType.BigInt
            param_idsubrubro.Value = 61
            param_idsubrubro.Direction = ParameterDirection.Input

            Dim param_idunidad As New SqlClient.SqlParameter
            param_idunidad.ParameterName = "@idunidad"
            param_idunidad.SqlDbType = SqlDbType.BigInt
            param_idunidad.Value = Unidad
            param_idunidad.Direction = ParameterDirection.Input

            Dim param_idmoneda As New SqlClient.SqlParameter
            param_idmoneda.ParameterName = "@idmoneda"
            param_idmoneda.SqlDbType = SqlDbType.BigInt
            param_idmoneda.Value = 1
            param_idmoneda.Direction = ParameterDirection.Input

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = Codigo
            param_codigo.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 255
            param_nombre.Value = Nombre
            param_nombre.Direction = ParameterDirection.Input

            Dim param_preciolista As New SqlClient.SqlParameter
            param_preciolista.ParameterName = "@preciovtasiniva"
            param_preciolista.SqlDbType = SqlDbType.Decimal
            param_preciolista.Precision = 18
            param_preciolista.Scale = 2
            param_preciolista.Value = PrecioVta
            param_preciolista.Direction = ParameterDirection.Input

            'Dim param_bonificacion As New SqlClient.SqlParameter
            'param_bonificacion.ParameterName = "@bonificacion"
            'param_bonificacion.SqlDbType = SqlDbType.Decimal
            'param_bonificacion.Precision = 18
            'param_bonificacion.Scale = 2
            'param_bonificacion.Value = 0
            'param_bonificacion.Direction = ParameterDirection.Input

            Dim param_ganancia As New SqlClient.SqlParameter
            param_ganancia.ParameterName = "@ganancia"
            param_ganancia.SqlDbType = SqlDbType.Decimal
            param_ganancia.Precision = 18
            param_ganancia.Scale = 2
            param_ganancia.Value = 1.4
            param_ganancia.Direction = ParameterDirection.Input

            Dim param_minimo As New SqlClient.SqlParameter
            param_minimo.ParameterName = "@minimo"
            param_minimo.SqlDbType = SqlDbType.Decimal
            param_minimo.Precision = 18
            param_minimo.Scale = 2
            param_minimo.Value = 0
            param_minimo.Direction = ParameterDirection.Input

            Dim param_maximo As New SqlClient.SqlParameter
            param_maximo.ParameterName = "@maximo"
            param_maximo.SqlDbType = SqlDbType.Decimal
            param_maximo.Precision = 18
            param_maximo.Scale = 4
            param_maximo.Value = 0
            param_maximo.Direction = ParameterDirection.Input

            Dim param_stockinicial As New SqlClient.SqlParameter
            param_stockinicial.ParameterName = "@stockinicial"
            param_stockinicial.SqlDbType = SqlDbType.Decimal
            param_stockinicial.Precision = 18
            param_stockinicial.Scale = 2
            param_stockinicial.Value = 0
            param_stockinicial.Direction = ParameterDirection.Input

            Dim param_nivel1 As New SqlClient.SqlParameter
            param_nivel1.ParameterName = "@nivel1"
            param_nivel1.SqlDbType = SqlDbType.VarChar
            param_nivel1.Size = 100
            param_nivel1.Value = ""
            param_nivel1.Direction = ParameterDirection.Input

            Dim param_nivel2 As New SqlClient.SqlParameter
            param_nivel2.ParameterName = "@nivel2"
            param_nivel2.SqlDbType = SqlDbType.VarChar
            param_nivel2.Size = 100
            param_nivel2.Value = ""
            param_nivel2.Direction = ParameterDirection.Input

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

            Try
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Materiales_Insert", _
                                    param_id, param_idmarca, param_idfamilia, param_idsubrubro, param_idunidad, _
                                    param_idmoneda, param_codigo, param_nombre, param_preciolista, param_ganancia, _
                                    param_minimo, param_maximo, param_stockinicial, param_nivel1, param_nivel2, param_useradd, _
                                    param_res)

                res = param_res.Value

                If res > 0 Then
                    ultid = param_id.Value
                    res = Agregar_Proveedor(param_id.Value, Unidad, PrecioVta, preciolista)

                    If res > 0 Then
                        Agregar_Material = ultid
                    Else
                        Agregar_Material = -1
                    End If

                End If

                'If res = 1 Then
                '    Agregar_Material = param_id.Value
                'Else
                '    'Agregar_Material = res
                '    Agregar_Proveedor(param_codigo.Value)
                'End If

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

    Private Function Agregar_Proveedor(ByVal idmaterial As Long, ByVal IdUnidad As Long, _
                                       ByVal precio As Double, ByVal preciolista As Double) As Integer

        Dim res As Integer = 0

        Dim param_idmaterial As New SqlClient.SqlParameter
        param_idmaterial.ParameterName = "@idmaterial"
        param_idmaterial.SqlDbType = SqlDbType.BigInt
        param_idmaterial.Value = idmaterial
        param_idmaterial.Direction = ParameterDirection.Input

        Dim param_idProveedor As New SqlClient.SqlParameter
        param_idProveedor.ParameterName = "@idProveedor"
        param_idProveedor.SqlDbType = SqlDbType.BigInt
        param_idProveedor.Value = 0
        param_idProveedor.Direction = ParameterDirection.Input

        Dim param_idunidadcompra As New SqlClient.SqlParameter
        param_idunidadcompra.ParameterName = "@idunidadcompra"
        param_idunidadcompra.SqlDbType = SqlDbType.BigInt
        param_idunidadcompra.Value = IdUnidad
        param_idunidadcompra.Direction = ParameterDirection.Input

        Dim param_idmonedacompra As New SqlClient.SqlParameter
        param_idmonedacompra.ParameterName = "@idmonedacompra"
        param_idmonedacompra.SqlDbType = SqlDbType.BigInt
        param_idmonedacompra.Value = 1
        param_idmonedacompra.Direction = ParameterDirection.Input

        Dim param_bonificacion1 As New SqlClient.SqlParameter
        param_bonificacion1.ParameterName = "@bonif1"
        param_bonificacion1.SqlDbType = SqlDbType.Decimal
        param_bonificacion1.Precision = 18
        param_bonificacion1.Scale = 2
        param_bonificacion1.Value = 0
        param_bonificacion1.Direction = ParameterDirection.Input

        Dim param_bonificacion2 As New SqlClient.SqlParameter
        param_bonificacion2.ParameterName = "@bonif2"
        param_bonificacion2.SqlDbType = SqlDbType.Decimal
        param_bonificacion2.Precision = 18
        param_bonificacion2.Scale = 2
        param_bonificacion2.Value = 0
        param_bonificacion2.Direction = ParameterDirection.Input

        Dim param_bonificacion3 As New SqlClient.SqlParameter
        param_bonificacion3.ParameterName = "@bonif3"
        param_bonificacion3.SqlDbType = SqlDbType.Decimal
        param_bonificacion3.Precision = 18
        param_bonificacion3.Scale = 2
        param_bonificacion3.Value = 0
        param_bonificacion3.Direction = ParameterDirection.Input

        Dim param_bonificacion4 As New SqlClient.SqlParameter
        param_bonificacion4.ParameterName = "@bonif4"
        param_bonificacion4.SqlDbType = SqlDbType.Decimal
        param_bonificacion4.Precision = 18
        param_bonificacion4.Scale = 2
        param_bonificacion4.Value = 0
        param_bonificacion4.Direction = ParameterDirection.Input

        Dim param_bonificacion5 As New SqlClient.SqlParameter
        param_bonificacion5.ParameterName = "@bonif5"
        param_bonificacion5.SqlDbType = SqlDbType.Decimal
        param_bonificacion5.Precision = 18
        param_bonificacion5.Scale = 2
        param_bonificacion5.Value = 0
        param_bonificacion5.Direction = ParameterDirection.Input

        Dim param_ganancia As New SqlClient.SqlParameter
        param_ganancia.ParameterName = "@ganancia"
        param_ganancia.SqlDbType = SqlDbType.Decimal
        param_ganancia.Precision = 18
        param_ganancia.Scale = 2
        param_ganancia.Value = 1.4
        param_ganancia.Direction = ParameterDirection.Input

        Dim param_precioxmt As New SqlClient.SqlParameter
        param_precioxmt.ParameterName = "@precioxmt"
        param_precioxmt.SqlDbType = SqlDbType.Decimal
        param_precioxmt.Precision = 18
        param_precioxmt.Scale = 2
        param_precioxmt.Value = 0
        param_precioxmt.Direction = ParameterDirection.Input

        Dim param_precioxkg As New SqlClient.SqlParameter
        param_precioxkg.ParameterName = "@precioxkg"
        param_precioxkg.SqlDbType = SqlDbType.Decimal
        param_precioxkg.Precision = 18
        param_precioxkg.Scale = 2
        param_precioxkg.Value = 0
        param_precioxkg.Direction = ParameterDirection.Input

        Dim param_pesoxmetro As New SqlClient.SqlParameter
        param_pesoxmetro.ParameterName = "@pesoxmetro"
        param_pesoxmetro.SqlDbType = SqlDbType.Decimal
        param_pesoxmetro.Precision = 18
        param_pesoxmetro.Scale = 2
        param_pesoxmetro.Value = 0
        param_pesoxmetro.Direction = ParameterDirection.Input

        Dim param_cantxlongitud As New SqlClient.SqlParameter
        param_cantxlongitud.ParameterName = "@cantxlongitud"
        param_cantxlongitud.SqlDbType = SqlDbType.Decimal
        param_cantxlongitud.Precision = 18
        param_cantxlongitud.Scale = 2
        param_cantxlongitud.Value = 0
        param_cantxlongitud.Direction = ParameterDirection.Input

        Dim param_pesoxunidad As New SqlClient.SqlParameter
        param_pesoxunidad.ParameterName = "@pesoxunidad"
        param_pesoxunidad.SqlDbType = SqlDbType.Decimal
        param_pesoxunidad.Precision = 18
        param_pesoxunidad.Scale = 2
        param_pesoxunidad.Value = 0
        param_pesoxunidad.Direction = ParameterDirection.Input

        Dim param_preciolista As New SqlClient.SqlParameter
        param_preciolista.ParameterName = "@preciolista"
        param_preciolista.SqlDbType = SqlDbType.Decimal
        param_preciolista.Precision = 18
        param_preciolista.Scale = 2
        param_preciolista.Value = preciolista
        param_preciolista.Direction = ParameterDirection.Input

        Dim param_preciovtasiniva As New SqlClient.SqlParameter
        param_preciovtasiniva.ParameterName = "@PrecioVentaSinIva"
        param_preciovtasiniva.SqlDbType = SqlDbType.Decimal
        param_preciovtasiniva.Precision = 18
        param_preciovtasiniva.Scale = 2
        param_preciovtasiniva.Value = precio
        param_preciovtasiniva.Direction = ParameterDirection.Input

        Dim param_res As New SqlClient.SqlParameter
        param_res.ParameterName = "@res"
        param_res.SqlDbType = SqlDbType.Int
        param_res.Value = DBNull.Value
        param_res.Direction = ParameterDirection.InputOutput

        Try
            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Materiales_Proveedor_Det_Insert", _
                                      param_idProveedor, param_idmaterial, param_idunidadcompra, param_idmonedacompra, param_bonificacion1, _
                                      param_bonificacion2, param_bonificacion3, param_bonificacion4, param_bonificacion5, param_ganancia, _
                                      param_precioxmt, param_precioxkg, param_pesoxmetro, param_cantxlongitud, param_pesoxunidad, _
                                      param_preciolista, param_preciovtasiniva, param_res)

            res = CInt(param_res.Value)

            Agregar_Proveedor = res

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            Agregar_Proveedor = -1

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function


#End Region

#Region "Transacciones"

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

#End Region

   
End Class


