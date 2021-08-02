Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Imports System.Threading

Public Class frmOrdenCompra

    Dim bolpoliticas As Boolean
    Dim llenandoCombo As Boolean = False
    Dim llenandoCombo2 As Boolean = False
    Dim CodigoUsuario As String

    Dim permitir_evento_CellChanged As Boolean

    Dim FILA As Integer
    Dim COLUMNA As Integer
    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    'Para el clic derecho sobre la grilla de materiales
    Dim Cell_X As Integer
    Dim Cell_Y As Integer

    'Para el combo de busqueda
    'Dim ID_Buscado As Long
    'Dim Nombre_Buscado As Long

    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing

    '  Public UltBusqueda As String

    Dim band As Integer

    Dim trd As Thread

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número

    Dim btnBand_Copiar As Boolean = True

    Dim ds_Empresa As Data.DataSet


    Dim porceniva As Double = 21

    Enum ColumnasDelGridItems
        IdOrdenDeCompra_Det = 0
        IdMaterial = 1
        Producto = 2
        QtyStockTotal = 3
        Bonif1 = 4
        Bonif2 = 5
        Bonif3 = 6
        PrecioUni = 7
        Cantidad = 8
        Peso = 9
        Subtotal = 10
        Status = 11
        Saldo = 12
        Nota_Det = 13
        IdUnidad = 14
        Iva = 15
        MontoIVA = 16
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

#Region "   Eventos"

    'Private Sub frmOrdenCompra_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    '    'If bolModo = False Then
    '    '    rdTodasOC.Checked = True
    '    '    SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & IIf(rdAnuladas.Checked = True, 1, 0) & ", @Pendientes = " & IIf(rdPendientes.Checked = True, 1, 0) & ", @PendientesyCumplidas = " & rdTodasOC.Checked
    '    '    btnActualizar_Click(sender, e)
    '    'End If

    'End Sub

    Private Sub frmOrdenCompra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado la Orden de Compra Nueva que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer una Orde de Compra Nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
                'Case Keys.F1
                '    If grdItems.Focused = True And grdItems.CurrentCell.ColumnIndex = 4 Then
                '        BuscarProducto()
                '    End If
        End Select
    End Sub

    Private Sub frmOrdenCompra_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Items()
            End If
        End If
    End Sub

    Private Sub frmOrdenCompra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AsignarPermisos(UserID, "ListaPrecioDistribuidor", ALTA, MODIFICA, BAJA, BAJA_FISICA, DESHACER, ConnStringSEI)

        Try
            Dim ds_usarios As Data.DataSet
            Dim connection As SqlClient.SqlConnection = Nothing
            connection = SqlHelper.GetConnection(ConnStringSEI)


            ds_usarios = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo FROM Empleados WHERE id = " & Util.UserID)
            ds_usarios.Dispose()

            CodigoUsuario = ds_usarios.Tables(0).Rows(0).Item(0)

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try



        Cursor = Cursors.WaitCursor

        ToolStrip_lblCodMaterial.Visible = True
        txtBusquedaMAT.Visible = True

        band = 0

        configurarform()
        asignarTags()

        btnEliminar.Text = "Anular OC"

        rdPendientes.Checked = 1

        LlenarcmbCondicionDePago_App(cmbCONDICIONDEPAGO, ConnStringSEI)
        LlenarcmbProveedores_App(cmbPROVEEDORES, ConnStringSEI, 0, 0)

        Me.LlenarCombo_Productos()

        SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        LlenarcmbContactoProveedor()

        permitir_evento_CellChanged = True

        band = 1

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
            txtID.Text = grd.Rows(0).Cells(0).Value
            cmbPROVEEDORES.SelectedValue = grd.Rows(0).Cells(13).Value
            cmbContacto.SelectedText = grd.Rows(0).Cells(9).Value
            cmbCONDICIONDEPAGO.SelectedValue = grd.Rows(0).Cells(12).Value
            txtNota.Text = grd.Rows(0).Cells(6).Value
            txtNotaInterna.Text = grd.Rows(0).Cells(14).Value
            lblStatus.Text = grd.Rows(0).Cells(7).Value
        End If

        If bolModo = True Then
            LlenarGrid_Items()
            btnNuevo_Click(sender, e)
        Else
            LlenarGrid_Items()
        End If


        grd.Columns(0).Visible = False
        'grd.Columns(1).Visible = False
        'grd.Columns(2).Visible = False
        'grd.Columns(3).Visible = False
        'grd.Columns(4).Visible = False
        'grd.Columns(5).Visible = False
        'grd.Columns(6).Visible = False
        'grd.Columns(7).Visible = False
        'grd.Columns(8).Visible = False
        'grd.Columns(9).Visible = False
        grd.Columns(10).Visible = False
        'grd.Columns(11).Visible = False
        grd.Columns(12).Visible = False
        grd.Columns(13).Visible = False
        'grd.Columns(14).Visible = False
        grd.Columns(15).Visible = False
        'grd.Columns(16).Visible = False
        'grd.Columns(17).Visible = False
        'grd.Columns(18).Visible = False
        'grd.Columns(19).Visible = False
        'grd.Columns(20).Visible = False

        dtpFECHA.MaxDate = Today.Date

        txtPrecioCosto.Enabled = MDIPrincipal.ControlUsuarioAutorizado(MDIPrincipal.EmpleadoLogueado)

        Cursor = Cursors.Default

    End Sub

    Private Sub cmbEquipo_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter Then
            txtPrecioCosto.Focus()
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged

        If band = 1 Then

            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT PrecioCompra, m.IdUnidad, SUM(s.qty) FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo where m.Codigo = " & cmbProducto.SelectedValue & "GROUP BY PrecioCompra, M.IDUnidad ")
            ds_Empresa.Dispose()

            txtPrecioCosto.Text = Math.Round(CDbl(ds_Empresa.Tables(0).Rows(0)(0)), 2)
            'txtPrecioUni.Text = FormatNumber(CDbl(ds_Empresa.Tables(0).Rows(0)(0)), 2)
            txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(1)
            lblStock.Text = ds_Empresa.Tables(0).Rows(0)(2)


        End If

    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        Try
            If Not txtIdUnidad.Text.Contains("HORMA") Or Not Not txtIdUnidad.Text.Contains("TIRA") Then
                If txtCantidad.Text = "" Then
                    txtSubtotalParcial.Text = "0"
                Else
                    txtSubtotalParcial.Text = Math.Round(CDbl(txtCantidad.Text) * CDbl(txtPrecioCosto.Text), 2)
                End If
            End If
        Catch ex As Exception

        End Try
   
    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then

            If cmbProducto.SelectedValue Is DBNull.Value Or cmbProducto.SelectedValue = 0 Then
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                '               GoTo Continuar
                Exit Sub
            End If

            If cmbProducto.Text = "" Then
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar un producto VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                '              GoTo Continuar
                Exit Sub
            End If

            If txtCantidad.Text = "" Or txtCantidad.Text = "0" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
                If txtPeso.Text = "" Then
                    Util.MsgStatus(Status1, "Debe ingresar el peso del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Debe ingresar el peso del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
                If CDbl(txtPeso.Text) = 0 Then
                    Util.MsgStatus(Status1, "Debe ingresar un peso válido del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "Debe ingresar un peso válido del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                    Exit Sub
                End If
            End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbProducto.Text = grdItems.Rows(i).Cells(2).Value Then
                    Util.MsgStatus(Status1, "El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (i + 1).ToString & ".", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            Next

            grdItems.Rows.Add(0, cmbProducto.SelectedValue, cmbProducto.Text, lblStock.Text, "", "", "", txtPrecioCosto.Text, txtCantidad.Text, txtPeso.Text, txtSubtotalParcial.Text, "", "", "", txtIdUnidad.Text, porceniva, FormatNumber((((CDbl(txtSubtotalParcial.Text) / (1 + porceniva / 100))) * (porceniva / 100)), 2))

            CalcularSubtotal()
            Contar_Filas()

            txtCantidad.Text = ""
            lblStock.Text = ""
            cmbProducto.Text = ""
            txtPrecioCosto.Text = ""
            txtPeso.Text = ""
            txtSubtotalParcial.Text = ""
            cmbProducto.Focus()

        End If

    End Sub

    Private Sub txtPeso_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPeso.KeyDown
        If txtIdUnidad.Text = "HORMA" Or txtIdUnidad.Text = "TIRA" Then
            txtCantidad_KeyDown(sender, e)
        End If
    End Sub

    Private Sub txtPeso_TextChanged(sender As Object, e As EventArgs) Handles txtPeso.TextChanged
        Try
            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Then
                If txtPeso.Text = "" Then
                    txtSubtotalParcial.Text = "0"
                Else
                    txtSubtotalParcial.Text = Math.Round(CDbl(txtPeso.Text) * CDbl(txtPrecioCosto.Text), 2)
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
    '    If e.KeyChar = ChrW(Keys.Enter) Then
    '        e.Handled = True
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    Private Sub CalcularSubtotal()

        Dim i As Integer
        Dim Subtotal As Double = 0

        For i = 0 To grdItems.Rows.Count - 1
            Subtotal = Subtotal + grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value
        Next

        txtSubtotal.Text = Subtotal

        'Dim Descuento As Double

        'If txtDescuento.Text = "" Then
        '    Descuento = "0"
        '    txtTotalVenta.Text = txtSubtotalVenta.Text
        '    Exit Sub
        'Else
        '    Descuento = txtDescuento.Text
        'End If


        'If rdAbsoluto.Checked = True Then
        '    txtTotalVenta.Text = Math.Round(CDbl(txtSubtotalVenta.Text) - Descuento, 2)
        'Else

        '    Dim ValorDescuento As Double
        '    ValorDescuento = Math.Round(CDbl(txtSubtotalVenta.Text) * (Descuento / 100), 2)

        '    txtTotalVenta.Text = Math.Round(CDbl(txtSubtotalVenta.Text) - ValorDescuento, 2)
        'End If

    End Sub

    'Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
    '    editando_celda = True
    'End Sub

    'Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit

    '    editando_celda = False

    '    'Cuando terminamos la edicion hay que buscar la descripcion del material y las unidad,
    '    'con esos datos completar la grilla. En este caso es la columna 2

    '    If e.ColumnIndex = ColumnasDelGridItems.Cod_material Then
    '        'completar la descripcion del   material
    '        Dim cell As DataGridViewCell = grdItems.CurrentCell

    '        Dim codigo As String, codigo_mat_prov As String = "", codunidad As String = "", codMoneda As String = ""

    '        Dim IdMoneda As Long, id As Long, idunidad As Long, idproveedor As Long, IdMat_Prov As Long

    '        Dim stock As Double = 0, preciovta As Double = 0, ganancia As Double = 0, preciolista As Double = 0, valorcambio As Double, iva As Double, montoiva As Double

    '        Dim proveedor As String = "", unidad As String = "", nombre As String = "", Pasillo As String = "", Estante As String = "", Fila As String = ""

    '        Dim fecha As Date = Nothing

    '        If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then Exit Sub
    '        codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
    '        cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)

    '        If ObtenerMaterial_App(codigo, codigo_mat_prov, id, nombre, idunidad, unidad, codunidad, stock, 0, 0, preciolista, ganancia, preciovta, 0, 0, iva, fecha, idproveedor, proveedor, 0, "", "", IdMoneda, codMoneda, valorcambio, montoiva, IdMat_Prov, 0, ConnStringSEI, "", Pasillo, Estante, Fila) = 0 Then

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMaterial).Value = id
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = nombre
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdUnidad).Value = idunidad
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = codunidad
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad

    '            'grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioProvPesos).Value = preciolista * ValorCambioDO

    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif1).Value = 0.0
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif2).Value = 0.0
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif3).Value = 0.0
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif4).Value = 0.0
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif5).Value = 0.0

    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Qty).Value = 0.0

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Proveedor).Value = proveedor

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.QtyStock).Value = stock

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMaterial_Prov).Value = IdMat_Prov
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value = codigo_mat_prov

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value = iva

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = 0

    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ubicacion).Value = Pasillo + " - " + Estante + " - " + Fila

    '            If Pasillo.Length > 0 Then
    '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_material).Style.BackColor = Color.LightGreen
    '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Style.BackColor = Color.LightGreen
    '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
    '            End If

    '            Contar_Filas()

    '        Else
    '            cell.Value = "NO EXISTE"
    '        End If
    '    End If

    '    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_material).Value Is DBNull.Value Then
    '        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value Then
    '            grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.PrecioLista).Value = FormatNumber(0, 2)
    '            grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Qty).Value = FormatNumber(0, 2)
    '            grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber(CDbl(0.0), 2)
    '        End If
    '    End If

    '    If e.ColumnIndex = ColumnasDelGridItems.Nombre_Material And _
    '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_material).Value Is DBNull.Value Then

    '        Dim cell As DataGridViewCell = grdItems.CurrentCell
    '        Dim cod_unidad As String, nombreUNIDAD As String = "", codunidad As String = ""
    '        Dim idunidad As Long

    '        cod_unidad = "U"

    '        If ObtenerUnidad_App(cod_unidad, idunidad, nombreUNIDAD, codunidad, ConnStringSEI) = 0 Then
    '            grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.IdUnidad).Value = idunidad
    '            grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Cod_Unidad).Value = cod_unidad
    '            grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Unidad).Value = nombreUNIDAD

    '            SendKeys.Send("{TAB}")

    '        Else
    '            cell.Value = "NO EXISTE"
    '            Exit Sub
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif1).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif1).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif1).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif2).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif2).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif2).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif3).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif3).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif3).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif4).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif4).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif4).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif5).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif5).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif5).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Qty).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Qty).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Qty).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Iva).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Iva).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.Iva).Value = 0.0
    '        End If

    '        If grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.MontoIVA).Value Is Nothing Or
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.MontoIVA).Value Is DBNull.Value Then
    '            grdItems.Rows(cell.RowIndex).Cells(frmOrdenCompra.ColumnasDelGridItems.MontoIVA).Value = 0.0
    '        End If


    '    End If

    '    If e.ColumnIndex = ColumnasDelGridItems.Cod_Unidad Then ' And _
    '        'grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_material).Value Is DBNull.Value Then

    '        Dim cell As DataGridViewCell = grdItems.CurrentCell
    '        Dim cod_unidad As String, nombre As String = "", codunidad As String = ""
    '        Dim idunidad As Long

    '        cod_unidad = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

    '        cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
    '        If ObtenerUnidad_App(cod_unidad, idunidad, nombre, codunidad, ConnStringSEI) = 0 Then
    '            cell.Value = nombre
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdUnidad).Value = idunidad
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = cod_unidad
    '            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = nombre

    '            SendKeys.Send("{TAB}")

    '        Else

    '            cell.Value = "NO EXISTE"
    '            Exit Sub
    '        End If
    '    End If

    '    If e.ColumnIndex = ColumnasDelGridItems.PrecioLista Or e.ColumnIndex = ColumnasDelGridItems.Iva Then

    '        Dim cell As DataGridViewCell = grdItems.CurrentCell

    '        If Not (grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value) And _
    '             Not (grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) And _
    '             Not (grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value Is DBNull.Value) Then

    '            CalcularPrecio(cell)

    '        End If

    '        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value

    '    End If

    '    If e.ColumnIndex = ColumnasDelGridItems.Bonif1 Or e.ColumnIndex = ColumnasDelGridItems.Bonif2 Or _
    '           e.ColumnIndex = ColumnasDelGridItems.Bonif3 Or e.ColumnIndex = ColumnasDelGridItems.Bonif4 Or _
    '           e.ColumnIndex = ColumnasDelGridItems.Bonif5 Then

    '        Dim cell As DataGridViewCell = grdItems.CurrentCell

    '        CalcularPrecio(cell)

    '    End If

    '    If e.ColumnIndex = ColumnasDelGridItems.Qty Then

    '        Dim cell As DataGridViewCell = grdItems.CurrentCell

    '        CalcularPrecio(cell)

    '        'Contar_Filas()

    '    End If

    'End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)

        'controlar lo que se ingresa en la grilla
        'en este caso, que no se ingresen letras en el lote
        'If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.PrecioVenta Then
        '    AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
        'Else
        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.PrecioUni Then
            AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
        Else
            If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Cantidad Then
                AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
            Else
                If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Bonif1 Then
                    AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
                Else
                    If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Bonif2 Then
                        AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
                    Else
                        If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Bonif3 Then
                            AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
                            'Else
                            '    If Me.grdItems.CurrentCell.ColumnIndex = ColumnasDelGridItems.Iva Then
                            '        AddHandler e.Control.KeyPress, AddressOf validarNumerosReales
                            '    Else
                            '        AddHandler e.Control.KeyPress, AddressOf NoValidar
                            '    End If
                        End If
                    End If
                End If
            End If
        End If
        'End If

    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        If Not bolModo Then
            cmbPROVEEDORES.Enabled = Not chkEliminado.Checked
            cmbCONDICIONDEPAGO.Enabled = Not chkEliminado.Checked
            grdItems.Enabled = Not chkEliminado.Checked
            dtpFECHA.Enabled = Not chkEliminado.Checked
            txtNota.Enabled = Not chkEliminado.Checked
        End If
    End Sub

    Private Sub PicProveedores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicProveedores.Click
        Dim f As New frmProveedores
        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbPROVEEDORES.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbProveedores_App(cmbPROVEEDORES, ConnStringSEI, 0, 0)
        cmbPROVEEDORES.Text = texto_del_combo
    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles txtID.KeyPress, txtCODIGO.KeyPress, txtNota.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles dtpFECHA.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    'Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
    '    Cell_X = e.ColumnIndex
    '    Cell_Y = e.RowIndex
    'End Sub

    'Private Sub grdItems_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellChanged
    '    If band = 0 Then Exit Sub
    '    Try
    '        Cell_Y = grdItems.CurrentRow.Index
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub grdItems_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim columna As Integer = 0
        Dim Valor As String

        Try
            columna = grdItems.CurrentCell.ColumnIndex
        Catch ex As Exception

        End Try


        Valor = ""
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then 'And grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Status).Value.ToString.ToUpper = "PENDIENTE" Then 'and bolmodo

                'If Not grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Status).Value.ToString.ToUpper = "CUMPLIDO" Then

                'If columna = ColumnasDelGridItems.Cod_material Then
                '    If grdItems.RowCount <> 0 Then
                '        If Cell_Y <> -1 Then
                '            Try
                '                If columna = ColumnasDelGridItems.Cod_material Then
                '                    Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_material).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString
                '                End If
                '            Catch ex As Exception
                '            End Try
                '        End If
                '    End If

                '    Dim p As Point = New Point(e.X, e.Y)

                '    If columna = ColumnasDelGridItems.Cod_material Then
                '        Me.BuscarDescripcionToolStripMenuItem.Visible = True
                '        Me.BuscarDescripcionToolStripMenuItem2.Visible = False
                '        Me.BuscarDescripcionToolStripMenuItem3.Visible = False
                '    Else
                '        lblStatus.Text = "error"
                '    End If

                '    ContextMenuStrip1.Show(grdItems, p)

                '    If Trim(Valor) <> "" Then
                '        ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
                '    End If

                'End If 'fin columnas

                'End If 'fin del or....
            End If ' MouseButtons.Right
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub grdItems_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseDoubleClick

    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        Exit Sub
    '    End If

    '    If Cell_Y < 0 Then
    '        Exit Sub
    '    End If

    '    BuscarProducto()
    '    grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True

    'End Sub

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click

        Dim cell As DataGridViewCell = grdItems.CurrentCell
        Dim res As Integer

        Try
            If bolModo Then
                grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente
                Contar_Filas()
            Else
                'If bolModo = False And grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMaterial).Value Is DBNull.Value Then
                ' grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente
                'Contar_Filas()
                'Else
                If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Status).Value.ToString = "CUMPLIDO" Then
                    Util.MsgStatus(Status1, "No se puede borrar el registro. Ya está Cumplido.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se puede borrar el registro. Ya está Cumplido.", My.Resources.stop_error.ToBitmap, True)
                Else
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value <> IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Saldo).Value Is DBNull.Value, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Saldo).Value) Then
                        Util.MsgStatus(Status1, "No se puede borrar el registro. Ya tiene Recepciones realizadas.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se puede borrar el registro. Ya tiene Recepciones realizadas.", My.Resources.stop_error.ToBitmap, True)
                    Else
                        If MessageBox.Show("Esta acción Eliminará el item de forma permanente." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)

                            Dim item As Long = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det).Value

                            grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente de la grilla..
                            Contar_Filas()

                            res = EliminarRegistroItem(CType(txtID.Text, Long), item)

                            Select Case res
                                Case -1
                                    Util.MsgStatus(Status1, "No se pudo borrar el Item.", My.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo borrar el Item.", My.Resources.stop_error.ToBitmap, True)
                                Case Else
                                    PrepararBotones()
                                    btnActualizar_Click(sender, e)
                                    'Setear_Grilla()
                                    Util.MsgStatus(Status1, "Se ha borrado el Item.", My.Resources.ok.ToBitmap)
                                    Util.MsgStatus(Status1, "Se ha borrado el Item.", My.Resources.ok.ToBitmap, True, True)
                            End Select
                        Else
                            Util.MsgStatus(Status1, "Acción de eliminar Item cancelada.", My.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Acción de eliminar Item cancelada.", My.Resources.stop_error.ToBitmap, True)
                        End If
                    End If
                End If
            End If
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem2.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem2_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BuscarDescripcionToolStripMenuItem3.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            BuscarDescripcionToolStripMenuItem3_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem.SelectedIndexChanged

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(columna)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem.ComboBox.SelectedValue

        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)

    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem2.SelectedIndexChanged

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(columna)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem2.ComboBox.Text

        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)

    End Sub

    Private Sub BuscarDescripcionToolStripMenuItem3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BuscarDescripcionToolStripMenuItem3.SelectedIndexChanged

        Dim columna As Integer = 0
        columna = grdItems.CurrentCell.ColumnIndex

        Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(columna)
        grdItems.CurrentCell = cell
        grdItems.CurrentCell.Value = BuscarDescripcionToolStripMenuItem3.ComboBox.Text

        ContextMenuStrip1.Close()
        grdItems.BeginEdit(True)

    End Sub

    Private Sub cmbCONDICIONDEPAGO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCONDICIONDEPAGO.SelectedIndexChanged
        If band = 1 Then
            If cmbCONDICIONDEPAGO.Text <> "" Then
                txtIdCondicionPago.Text = cmbCONDICIONDEPAGO.SelectedValue.ToString
            Else
                txtIdCondicionPago.Text = 0
            End If
        End If
    End Sub

    Private Sub cmbPROVEEDORES_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPROVEEDORES.SelectedIndexChanged
        If band = 1 Then
            Try

                txtIdProveedor.Text = cmbPROVEEDORES.SelectedValue.ToString

                LlenarcmbContactoProveedor()
            Catch ex As Exception

            End Try

        End If
    End Sub

    '(currentcellchanged)
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        If Permitir Then
            'band = 0
            Try
                Select Case grd.CurrentRow.Cells(7).Value
                    Case "PENDIENTE"
                        btnFinalizar.Enabled = True
                    Case Else
                        btnFinalizar.Enabled = False
                End Select

                LlenarcmbContactoProveedor()

                lblCantidadFilas.Text = grdItems.Rows.Count
                txtID.Text = grd.CurrentRow.Cells(0).Value
                txtSubtotal.Text = grd.CurrentRow.Cells(4).Value
                txtNota.Text = grd.CurrentRow.Cells(6).Value
                lblStatus.Text = grd.CurrentRow.Cells(7).Value
                cmbContacto.Text = grd.CurrentRow.Cells(9).Value
                cmbCONDICIONDEPAGO.SelectedValue = grd.CurrentRow.Cells(12).Value
                cmbPROVEEDORES.SelectedValue = grd.CurrentRow.Cells(13).Value
                txtNotaInterna.Text = grd.CurrentRow.Cells(14).Value

                LlenarGrid_Items()

            Catch ex As Exception
                'band = 1
            End Try
        End If
    End Sub

    Private Sub rdAnuladas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdAnuladas.CheckedChanged
        btnGuardar.Enabled = Not rdAnuladas.Checked
        btnEliminar.Enabled = Not rdAnuladas.Checked
        btnNuevo.Enabled = Not rdAnuladas.Checked
        btnCancelar.Enabled = Not rdAnuladas.Checked

        SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

        Try
            LlenarGrilla()

            If grd.Rows.Count > 0 Then
                grdItems.Columns(16).Visible = False
            End If
        Catch ex As Exception

        End Try
  


    End Sub

    Private Sub rdPendientes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdPendientes.CheckedChanged

        If band = 1 Then
            SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then

                    grd.Rows(0).Selected = True
                    txtID.Text = grd.Rows(0).Cells(0).Value
                    LlenarGrid_Items()
                    grdItems.Columns(16).Visible = True
                End If

            Catch ex As Exception

            End Try
    
        End If

    End Sub

    Private Sub rdTodasOC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdTodasOC.CheckedChanged

        If band = 1 Then
            SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

            Try
                LlenarGrilla()

                If grd.Rows.Count > 0 Then
                    grdItems.Columns(16).Visible = True
                    LlenarGrid_Items()
                End If
            Catch ex As Exception

            End Try
      

        End If

    End Sub

    Private Sub chkGrillaInferior_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGrillaInferior.CheckedChanged
        Dim xgrd As Long, ygrd As Long, hgrd As Long, variableajuste As Long
        xgrd = grd.Location.X
        ygrd = grd.Location.Y
        hgrd = grd.Height

        variableajuste = 150

        If chkGrillaInferior.Checked = True Then
            chkGrillaInferior.Text = "Disminuir Grilla Inferior"
            chkGrillaInferior.Location = New Point(chkGrillaInferior.Location.X, chkGrillaInferior.Location.Y - variableajuste)
            GroupBox1.Height = GroupBox1.Height - variableajuste
            grd.Location = New Point(xgrd, ygrd - variableajuste)
            grd.Height = hgrd + variableajuste
            grdItems.Height = grdItems.Height - variableajuste
            Label19.Location = New Point(Label19.Location.X, Label19.Location.Y - variableajuste)
            lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y - variableajuste)

            rdPendientes.Location = New Point(rdPendientes.Location.X, rdPendientes.Location.Y - variableajuste)
            rdAnuladas.Location = New Point(rdAnuladas.Location.X, rdAnuladas.Location.Y - variableajuste)
            rdTodasOC.Location = New Point(rdTodasOC.Location.X, rdTodasOC.Location.Y - variableajuste)

            Label4.Location = New Point(Label4.Location.X, Label4.Location.Y - variableajuste)
            txtSubtotal.Location = New Point(txtSubtotal.Location.X, txtSubtotal.Location.Y - variableajuste)

            Label18.Location = New Point(Label18.Location.X, Label18.Location.Y - variableajuste)
            txtTotal.Location = New Point(txtTotal.Location.X, txtTotal.Location.Y - variableajuste)

            Label20.Location = New Point(Label20.Location.X, Label20.Location.Y - variableajuste)
            txtMontoIVA.Location = New Point(txtMontoIVA.Location.X, txtMontoIVA.Location.Y - variableajuste)

        Else
            chkGrillaInferior.Text = "Aumentar Grilla Inferior"
            chkGrillaInferior.Location = New Point(chkGrillaInferior.Location.X, chkGrillaInferior.Location.Y + variableajuste)
            GroupBox1.Height = GroupBox1.Height + variableajuste
            grd.Location = New Point(xgrd, ygrd + variableajuste)
            grd.Height = hgrd - variableajuste
            grdItems.Height = grdItems.Height + variableajuste
            Label19.Location = New Point(Label19.Location.X, Label19.Location.Y + variableajuste)
            lblCantidadFilas.Location = New Point(lblCantidadFilas.Location.X, lblCantidadFilas.Location.Y + variableajuste)

            rdPendientes.Location = New Point(rdPendientes.Location.X, rdPendientes.Location.Y + variableajuste)
            rdAnuladas.Location = New Point(rdAnuladas.Location.X, rdAnuladas.Location.Y + variableajuste)
            rdTodasOC.Location = New Point(rdTodasOC.Location.X, rdTodasOC.Location.Y + variableajuste)

            Label4.Location = New Point(Label4.Location.X, Label4.Location.Y + variableajuste)
            txtSubtotal.Location = New Point(txtSubtotal.Location.X, txtSubtotal.Location.Y + variableajuste)

            Label18.Location = New Point(Label18.Location.X, Label18.Location.Y + variableajuste)
            txtTotal.Location = New Point(txtTotal.Location.X, txtTotal.Location.Y + variableajuste)

            Label20.Location = New Point(Label20.Location.X, Label20.Location.Y + variableajuste)
            txtMontoIVA.Location = New Point(txtMontoIVA.Location.X, txtMontoIVA.Location.Y + variableajuste)

        End If

    End Sub

    Private Sub PicFormaPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picFormaPago.Click
        Dim f As New frmCondiciondePago

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbCONDICIONDEPAGO.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbCondicionDePago_App(cmbCONDICIONDEPAGO, ConnStringSEI)
        cmbCONDICIONDEPAGO.Text = texto_del_combo
    End Sub

    Private Sub grdItems_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellContentClick
        Dim cell As DataGridViewCell = grdItems.CurrentCell
        Dim res As Integer

        If e.ColumnIndex = 17 Then
            Try
                If bolModo Then
                    grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente
                    Contar_Filas()
                Else
                    'If bolModo = False And grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMaterial).Value Is DBNull.Value Then
                    ' grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente
                    'Contar_Filas()
                    'Else
                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Status).Value.ToString = "CUMPLIDO" Then
                        Util.MsgStatus(Status1, "No se puede borrar el registro. Ya está Cumplido.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se puede borrar el registro. Ya está Cumplido.", My.Resources.stop_error.ToBitmap, True)
                    Else
                        If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value <> IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Saldo).Value Is DBNull.Value, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Saldo).Value) Then
                            Util.MsgStatus(Status1, "No se puede borrar el registro. Ya tiene Recepciones realizadas.", My.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se puede borrar el registro. Ya tiene Recepciones realizadas.", My.Resources.stop_error.ToBitmap, True)
                        Else
                            If MessageBox.Show("Esta acción Eliminará el item de forma permanente." + vbCrLf + "¿Está seguro que desea eliminar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)

                                Dim item As Long = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det).Value

                                grdItems.Rows.RemoveAt(cell.RowIndex) 'la borramos directamente de la grilla..
                                Contar_Filas()

                                res = EliminarRegistroItem(CType(txtID.Text, Long), item)

                                Select Case res
                                    Case -1
                                        Util.MsgStatus(Status1, "No se pudo borrar el Item.", My.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo borrar el Item.", My.Resources.stop_error.ToBitmap, True)
                                    Case Else
                                        PrepararBotones()
                                        btnActualizar_Click(sender, e)
                                        'Setear_Grilla()
                                        Util.MsgStatus(Status1, "Se ha borrado el Item.", My.Resources.ok.ToBitmap)
                                        Util.MsgStatus(Status1, "Se ha borrado el Item.", My.Resources.ok.ToBitmap, True, True)
                                End Select
                            Else
                                Util.MsgStatus(Status1, "Acción de eliminar Item cancelada.", My.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "Acción de eliminar Item cancelada.", My.Resources.stop_error.ToBitmap, True)
                            End If
                        End If
                    End If
                End If
                CalcularSubtotal()
                'End If
            Catch ex As Exception

            End Try
        End If


    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Emisión de Ordenes de Compra a Proveedores"

        'Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 5)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        'Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 50)
        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 3 - GroupBox1.Size.Height - GroupBox1.Location.Y - 62) '65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "1"
        dtpFECHA.Tag = "2"
        cmbPROVEEDORES.Tag = "3"
        txtSubtotal.Tag = "4"
        cmbCONDICIONDEPAGO.Tag = "5"
        txtNota.Tag = "6"
        lblStatus.Tag = "7"
        chkEliminado.Tag = "8"
        cmbContacto.Tag = "9"
        txtIdCondicionPago.Tag = "12"
        txtIdProveedor.Tag = "13"
        txtNotaInterna.Tag = "14"
        txtTotal.Tag = "15"
        'txtMontoIVA.Tag = "37"

    End Sub

    Private Sub Verificar_Datos()

        Dim i As Integer, j As Integer, filas As Integer ', res As Integer ', state As Integer

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        'Verificar si todos los combox tienen algo válido
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not (cmbPROVEEDORES.SelectedIndex > -1) Then
            Util.MsgStatus(Status1, "Ingrese un valor válido en el campo 'Proveedor'.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Ingrese un valor válido en el campo 'Proveedor'.", My.Resources.Resources.alert.ToBitmap, True)
            cmbPROVEEDORES.Focus()
            Exit Sub
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'verificar que no hay nada en la grilla sin datos
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        j = grdItems.RowCount - 1
        filas = 0
        For i = 0 To j
            'state = grdItems.Rows.GetRowState(i)
            'la fila está vacía ?
            filas = filas + 1

        Next i
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

    Private Sub PrepararGridItems()
        Util.LimpiarGridItems(grdItems)
    End Sub

    Private Sub LlenarGrid_Items()

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

            SQL = "exec spOrdenDeCompra_Det_Select_By_IDOrdenDeCompra @IDOrdenDeCompra = " & IIf(txtID.Text = "", 0, txtID.Text) & ", @Anulado = " & rdAnuladas.Checked

            Dim cmd As New SqlCommand(SQL, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(),
                                  dt.Rows(i)(4).ToString(), dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(),
                                  dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(), dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(),
                                  dt.Rows(i)(12).ToString(), dt.Rows(i)(13).ToString(), dt.Rows(i)(14).ToString(), dt.Rows(i)(15).ToString())
            Next

            CalcularSubtotal()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        'GetDatasetItems()

        'CheckForIllegalCrossThreadCalls = False

        'trd = New Thread(AddressOf HiloOcultarColumnasGridItems)
        'trd.IsBackground = True
        'trd.Start()

        'HiloOcultarColumnasGridItems()

        'SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

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

    Private Sub NoValidar(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim caracter As Char = e.KeyChar
        Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = sender
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString)
        e.Handled = False ' dejar escribir cualquier cosa
    End Sub

    Private Sub validarNumerosReales(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Controlar que el caracter ingresado sea un  numero real
        Dim caracter As Char = e.KeyChar
        Dim obj As System.Windows.Forms.DataGridViewTextBoxEditingControl = sender

        If caracter = "." Or caracter = "," Then
            If CuentaAparicionesDeCaracter(obj.Text, ".") = 0 Then
                If CuentaAparicionesDeCaracter(obj.Text, ",") = 0 Then
                    e.Handled = False ' dejar escribir
                Else
                    e.Handled = True 'no deja escribir
                End If
            Else
                e.Handled = True ' no deja escribir
            End If
        Else
            If caracter = "-" And obj.Text.Trim <> "" Then
                If CuentaAparicionesDeCaracter(obj.Text, caracter) < 1 Then
                    obj.Text = "-" + obj.Text
                    e.Handled = True ' no dejar escribir
                Else
                    obj.Text = Mid(obj.Text, 2, Len(obj.Text))
                    e.Handled = True ' no dejar escribir
                End If
            Else
                If Char.IsNumber(caracter) Or caracter = ChrW(Keys.Back) Or caracter = ChrW(Keys.Delete) Or caracter = ChrW(Keys.Enter) Then
                    e.Handled = False 'dejo escribir
                Else
                    e.Handled = True ' no dejar escribir
                End If
            End If
        End If
    End Sub

    Private Sub validarNumeros(ByVal sender As Object, _
       ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Controlar que el caracter ingresado sea un  numero
        Dim caracter As Char = e.KeyChar
        If caracter = "." Or caracter = "," Then
            e.Handled = True ' no dejar escribir
        Else
            If Char.IsNumber(caracter) Or caracter = ChrW(Keys.Back) Or caracter = ChrW(Keys.Delete) Or caracter = ChrW(Keys.Enter) Then
                e.Handled = False 'dejo escribir
            Else
                e.Handled = True ' no dejar escribir
            End If
        End If
    End Sub

    'Private Sub LlenarComboAyudaMateriales()
    '    Dim ds_Materiales As Data.DataSet
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try
    '        ds_Materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo , nombre FROM Materiales WHERE Eliminado = 0 ORDER BY Nombre")
    '        ds_Materiales.Dispose()

    '        With Me.BuscarDescripcionToolStripMenuItem.ComboBox
    '            .DataSource = ds_Materiales.Tables(0).DefaultView
    '            .DisplayMember = "nombre"
    '            .ValueMember = "Codigo"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
    '            .BindingContext = Me.BindingContext
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

    'Private Sub LlenarcmbUnidadesVta()
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim ds_UnidadesVta As Data.DataSet

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try

    '        ds_UnidadesVta = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo, NOMBRE as unidad FROM Unidades")
    '        ds_UnidadesVta.Dispose()

    '        With Me.cmbUnidadVenta.ComboBox
    '            .DataSource = ds_UnidadesVta.Tables(0).DefaultView
    '            .DisplayMember = "UNIDAD"
    '            .ValueMember = "codigo"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
    '            .BindingContext = Me.BindingContext
    '            .SelectedIndex = 0
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

    '    End Try

    'End Sub

    Private Sub LlenarcmbContactoProveedor()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Contacto As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Contacto = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT * FROM (SELECT contacto as ContactoProveedor from Proveedores where Codigo = " & IIf(txtIdProveedor.Text = "", 0, txtIdProveedor.Text) & " UNION SELECT DISTINCT CONTACTOPROVEEDOR FROM ORDENDECOMPRA WHERE idproveedor = " & IIf(txtIdProveedor.Text = "", 0, txtIdProveedor.Text) & " ) tt  ORDER BY ContactoProveedor")
            ds_Contacto.Dispose()

            With cmbContacto
                .DataSource = ds_Contacto.Tables(0).DefaultView
                .DisplayMember = "ContactoProveedor"
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

    Private Sub Imprimir(ByVal Presupuesto As Boolean)
        nbreformreportes = "Orden de Compra / Presupuesto"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        If Presupuesto Then
            Rpt.NombreArchivoPDF = "Solicitud de Cotización " & txtCODIGO.Text & " - " & cmbPROVEEDORES.Text.ToString
        Else
            Rpt.NombreArchivoPDF = "Orden de Compra " & txtCODIGO.Text & " - " & cmbPROVEEDORES.Text.ToString
        End If

        Rpt.OrdenesDeCompra_Maestro_App(txtCODIGO.Text, 0, Rpt, My.Application.Info.AssemblyName.ToString, Presupuesto)

        cnn = Nothing

    End Sub

    Private Sub Contar_Filas()

        'Dim i As Integer, j As Integer = 0

        'txtSubtotal.Text = 0
        'txtMontoIVA.Text = 0
        'txtTotal.Text = 0

        'For i = 0 To grdItems.Rows.Count - 1 '16
        '    Try
        '        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Producto).Value <> Nothing Then
        '            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value <> Nothing Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value > 0 Then
        '                txtSubtotal.Text = Format(CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value), "####0.00")
        '                'txtSubtotal.Text = FormatNumber(CDbl(txtSubtotal.Text) + CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value), 2)
        '                'txtMontoIVA.Text = CDbl(txtMontoIVA.Text) + grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIVA).Value
        '                'txtTotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtMontoIVA.Text)
        '                j = j + 1
        '            End If
        '        End If
        '    Catch ex As Exception

        '    End Try
        'Next

        lblCantidadFilas.Text = grdItems.Rows.Count '- 1  'j.ToString + " / 16"

        'If j = 16 Then
        '    grdItems.AllowUserToAddRows = False
        'Else
        '    grdItems.AllowUserToAddRows = True
        'End If


    End Sub

    'Private Sub BuscarProducto()

    '    LLAMADO_POR_FORMULARIO = True

    '    Dim f As New frmMaterialesPrecios

    '    f.Width = 1200
    '    f.Height = 650
    '    f.StartPosition = FormStartPosition.CenterScreen
    '    f.grd.Width = 1150
    '    f.grd.Height = 350
    '    f.DesdePre = 3
    '    f.FilaCodigo = Cell_Y 'e.RowIndex

    '    f.ShowDialog(Me)

    '    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ubicacion).Value.ToString.Length > 6 Then
    '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_material).Style.BackColor = Color.LightGreen
    '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material_Prov).Style.BackColor = Color.LightGreen
    '        grdItems.CurrentRow.Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
    '    End If

    '    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Qty, grdItems.CurrentRow.Index)

    'End Sub

    Private Sub CalcularPrecio(ByRef cell As DataGridViewCell)
        Dim Bonif1 As Double, Bonif2 As Double, Bonif3 As Double, Bonif4 As Double, Bonif5 As Double
        'Dim precioxkg As Double, cantxlongitud As Double, pesoxunidad As Double,  precioxmt As Double
        Dim preciolista As Double = 0
        Dim preciobonif1 As Double = 0, preciobonif2 As Double = 0, preciobonif3 As Double = 0, preciobonif4 As Double = 0, preciobonif5 As Double = 0
        Dim preciosinivabonif As Double = 0

        'Dim cell As DataGridViewCell = grdItems.CurrentCell

        Try
            If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value Or _
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value Is Nothing Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value = 0
            End If

            If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value Or _
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value Is Nothing Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value = 0
            End If

            If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value Or _
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value Is Nothing Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value = 0
            End If

            'If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value Or _
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value Is Nothing Then
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value = 0
            'End If

            'If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value Or _
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value Is Nothing Then
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value = 0
            'End If

            If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value Is DBNull.Value Or _
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value Is Nothing Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value = 0.0
            End If

            'If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value Is DBNull.Value Or _
            '   grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value Is Nothing Then
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value = 0.0
            'End If

            Bonif1 = 1 - (CType(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif1).Value, Double)) / 100
            Bonif2 = 1 - (CType(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif2).Value, Double)) / 100
            Bonif3 = 1 - (CType(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif3).Value, Double)) / 100
            'Bonif4 = 1 - (CType(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif4).Value, Double)) / 100
            'Bonif5 = 1 - (CType(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonif5).Value, Double)) / 100

            'preciolista = IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value)

            preciobonif1 = preciolista * Bonif1
            preciobonif1 = preciobonif1 * Bonif2
            preciobonif1 = preciobonif1 * Bonif3
            preciobonif1 = preciobonif1 * Bonif4
            preciobonif1 = preciobonif1 * Bonif5

            preciosinivabonif = preciobonif1 '/ (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.IVA).Value / 100))

            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioUni).Value = CDbl(FormatNumber(preciosinivabonif, 2))

            ' grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIVA).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value * preciosinivabonif * (grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value / 100), "####0.00")

            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = CDbl(FormatNumber(preciosinivabonif * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value, 2))

            Contar_Filas()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub HiloOcultarColumnasGridItems()
        Try
            grdItems.Columns(ColumnasDelGridItems.IdOrdenDeCompra_Det).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.IdOrdenDeCompra_Det).Width = 70
            grdItems.Columns(ColumnasDelGridItems.IdOrdenDeCompra_Det).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.Cod_OrdenDeCompra_Det).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.Cod_OrdenDeCompra_Det).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.Cod_OrdenDeCompra_Det).Visible = False

            grdItems.Columns(ColumnasDelGridItems.IdMaterial).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.Cod_material).Width = 90

            grdItems.Columns(ColumnasDelGridItems.Producto).Width = 260

            grdItems.Columns(ColumnasDelGridItems.QtyStockTotal).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.QtyStockTotal).Width = 50
            grdItems.Columns(ColumnasDelGridItems.QtyStockTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Precio).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.PrecioLista).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.PrecioLista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Iva).Width = 40
            'grdItems.Columns(ColumnasDelGridItems.Iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.MontoIVA).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.MontoIVA).Width = 60
            'grdItems.Columns(ColumnasDelGridItems.MontoIVA).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Bonif1).Width = 45
            grdItems.Columns(ColumnasDelGridItems.Bonif1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Bonif2).Width = 45
            grdItems.Columns(ColumnasDelGridItems.Bonif2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Bonif3).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Bonif3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif4).Width = 50
            'grdItems.Columns(ColumnasDelGridItems.Bonif4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonif5).Width = 50
            'grdItems.Columns(ColumnasDelGridItems.Bonif5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).Width = 65
            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grdItems.Columns(ColumnasDelGridItems.PrecioProvPesos).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).Width = 70
            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grdItems.Columns(ColumnasDelGridItems.PrecioVta).Visible = False

            grdItems.Columns(ColumnasDelGridItems.PrecioUni).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.PrecioUni).Width = 70
            grdItems.Columns(ColumnasDelGridItems.PrecioUni).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            'grdItems.Columns(ColumnasDelGridItems.Qty).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Cantidad).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Cantidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Subtotal).ReadOnly = True 'subtotal
            grdItems.Columns(ColumnasDelGridItems.Subtotal).Width = 80
            grdItems.Columns(ColumnasDelGridItems.Subtotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Status).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Status).Width = 70
            grdItems.Columns(ColumnasDelGridItems.Status).Visible = Not bolModo
            grdItems.Columns(ColumnasDelGridItems.Status).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            grdItems.Columns(ColumnasDelGridItems.Saldo).Visible = Not bolModo
            grdItems.Columns(ColumnasDelGridItems.Saldo).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.Saldo).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Saldo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Nota_Det).Width = 250

            'grdItems.Columns(ColumnasDelGridItems.orden).Visible = False

            With grdItems
                .VirtualMode = False
                .AllowUserToAddRows = True
                .AllowUserToDeleteRows = True
                .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
                .RowsDefaultCellStyle.BackColor = Color.White
                .AllowUserToOrderColumns = False
                .SelectionMode = DataGridViewSelectionMode.CellSelect
                .ForeColor = Color.Black
            End With

            With grdItems.ColumnHeadersDefaultCellStyle
                .BackColor = Color.Black  'Color.BlueViolet
                .ForeColor = Color.White
                .Font = New Font("TAHOMA", 8, FontStyle.Bold)
            End With

            grdItems.Font = New Font("TAHOMA", 8, FontStyle.Regular)

            'Dim i As Integer

            'For i = 0 To grdItems.Rows.Count - 2
            '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.Ubicacion).Value.ToString.Length > 6 Then
            '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_material).Style.BackColor = Color.LightGreen
            '        grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
            '    End If
            'Next

            ' Contar_Filas()

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub LlenarcmbMarcasProductos()
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim ds_Marcas As Data.DataSet

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try

    '        ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, rtrim(nombre) as Marca FROM Marcas")
    '        ds_Marcas.Dispose()

    '        With Me.cmbMarcaCompra.ComboBox
    '            .DataSource = ds_Marcas.Tables(0).DefaultView
    '            .DisplayMember = "Marca"
    '            .ValueMember = "Id"
    '            .AutoCompleteMode = AutoCompleteMode.Suggest
    '            .AutoCompleteSource = AutoCompleteSource.ListItems
    '            .DropDownStyle = ComboBoxStyle.DropDown
    '            .BindingContext = Me.BindingContext
    '            .SelectedIndex = 0
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

    Private Sub LlenarCombo_Productos()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales m JOIN Marcas ma ON m.idmarca = ma.Codigo WHERE m.nombre not like '%**FR%' and m.eliminado = 0 ")
            ds_Equipos.Dispose()

            With Me.cmbProducto
                .DataSource = ds_Equipos.Tables(0).DefaultView
                .DisplayMember = "Producto"
                .ValueMember = "Codigo"
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

#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try

            connection = SqlHelper.GetConnection(ConnStringSEI)
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

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                If bolModo = True Then
                    param_codigo.Value = DBNull.Value
                    param_codigo.Direction = ParameterDirection.InputOutput
                Else
                    param_codigo.Value = txtCODIGO.Text
                    param_codigo.Direction = ParameterDirection.Input
                End If

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.VarChar
                param_idproveedor.Size = 25
                param_idproveedor.Value = IIf(txtIdProveedor.Text = "", cmbPROVEEDORES.SelectedValue, txtIdProveedor.Text)
                param_idproveedor.Direction = ParameterDirection.Input

                Dim param_idcondiciondepago As New SqlClient.SqlParameter
                param_idcondiciondepago.ParameterName = "@idcondicionpago"
                param_idcondiciondepago.SqlDbType = SqlDbType.BigInt
                param_idcondiciondepago.Value = IIf(txtIdCondicionPago.Text = "", 0, txtIdCondicionPago.Text)
                param_idcondiciondepago.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = txtSubtotal.Text
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_montoivatotal As New SqlClient.SqlParameter
                param_montoivatotal.ParameterName = "@MontoIvaTotal"
                param_montoivatotal.SqlDbType = SqlDbType.Decimal
                param_montoivatotal.Precision = 18
                param_montoivatotal.Scale = 2
                param_montoivatotal.Value = 0 'txtMontoIVA.Text
                param_montoivatotal.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@Total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = 0 'txtTotal.Text
                param_total.Direction = ParameterDirection.Input

                Dim param_idComprador As New SqlClient.SqlParameter
                param_idComprador.ParameterName = "@idComprador"
                param_idComprador.SqlDbType = SqlDbType.BigInt
                param_idComprador.Value = UserID 'CType(Me.cmbComprador.SelectedValue, Long)
                param_idComprador.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 150
                param_nota.Value = txtNota.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_contactoproveedor As New SqlClient.SqlParameter
                param_contactoproveedor.ParameterName = "@ContactoProveedor"
                param_contactoproveedor.SqlDbType = SqlDbType.VarChar
                param_contactoproveedor.Size = 100
                param_contactoproveedor.Value = cmbContacto.Text
                param_contactoproveedor.Direction = ParameterDirection.Input

                Dim param_notainterna As New SqlClient.SqlParameter
                param_notainterna.ParameterName = "@notainterna"
                param_notainterna.SqlDbType = SqlDbType.VarChar
                param_notainterna.Size = 4000
                param_notainterna.Value = txtNotaInterna.Text
                param_notainterna.Direction = ParameterDirection.Input

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
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdenDeCompra_Insert", _
                                                param_id, param_codigo, param_fecha, param_idproveedor, _
                                                param_idcondiciondepago, param_nota, param_subtotal, param_montoivatotal, param_total, _
                                                param_idComprador, param_contactoproveedor, _
                                                param_notainterna, param_useradd, param_res)

                        txtID.Text = param_id.Value
                        txtCODIGO.Text = param_codigo.Value
                    Else

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdenDeCompra_Update", param_id, _
                                                param_codigo, param_fecha, param_idproveedor, _
                                                param_idcondiciondepago, param_contactoproveedor, _
                                                param_nota, param_subtotal, param_montoivatotal, param_total, _
                                                param_idComprador, param_notainterna, _
                                                param_useradd, param_res)

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
            'Finally
            '    'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try
    End Function

    Private Function AgregarActualizar_Registro_Items() As Integer
        Dim res As Integer = 0 ', res_del As Integer
        Dim i As Integer
        'Dim ds As Data.DataSet


        Try

            Try

                Dim nuevo As Boolean

                For i = 0 To grdItems.RowCount - 1 'CantidadFilas

                    'Dim IdMat As Long = 0, IdMat_Prov = 0, idproveedor As Long = 0
                    'Dim PrecioLista As Double = 0, Idmarca As Long, MarcaNueva As Long

                    Dim param_id As New SqlClient.SqlParameter
                    param_id.ParameterName = "@id"
                    param_id.SqlDbType = SqlDbType.BigInt
                    If bolModo = False And Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det).Value Is DBNull.Value) Then
                        param_id.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det).Value, Long)
                    Else
                        param_id.Value = 0
                    End If
                    param_id.Direction = ParameterDirection.Input

                    Dim param_IdOrdenCompra As New SqlClient.SqlParameter
                    param_IdOrdenCompra.ParameterName = "@idOrdendeCompra"
                    param_IdOrdenCompra.SqlDbType = SqlDbType.BigInt
                    param_IdOrdenCompra.Value = txtID.Text
                    param_IdOrdenCompra.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@idmaterial"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMaterial).Value 'IdMat 'grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMaterial).Value
                    param_idmaterial.Direction = ParameterDirection.Input

                    Dim param_idmat_prov As New SqlClient.SqlParameter
                    param_idmat_prov.ParameterName = "@idmaterial_prov"
                    param_idmat_prov.SqlDbType = SqlDbType.BigInt
                    param_idmat_prov.Value = 0 'IdMat_Prov 'grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMaterial).Value
                    param_idmat_prov.Direction = ParameterDirection.Input

                    Dim param_precio As New SqlClient.SqlParameter
                    param_precio.ParameterName = "@precio"
                    param_precio.SqlDbType = SqlDbType.Decimal
                    param_precio.Precision = 18
                    param_precio.Scale = 2
                    param_precio.Value = 0 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value, Decimal)
                    param_precio.Direction = ParameterDirection.Input

                    Dim param_iva As New SqlClient.SqlParameter
                    param_iva.ParameterName = "@iva"
                    param_iva.SqlDbType = SqlDbType.Decimal
                    param_iva.Precision = 18
                    param_iva.Scale = 2
                    param_iva.Value = porceniva 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Iva).Value, Decimal)
                    param_iva.Direction = ParameterDirection.Input

                    Dim param_bonificacion1 As New SqlClient.SqlParameter
                    param_bonificacion1.ParameterName = "@bonif1"
                    param_bonificacion1.SqlDbType = SqlDbType.Decimal
                    param_bonificacion1.Precision = 18
                    param_bonificacion1.Scale = 2
                    param_bonificacion1.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value Is DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value Is "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif1).Value)
                    param_bonificacion1.Direction = ParameterDirection.Input

                    Dim param_bonificacion2 As New SqlClient.SqlParameter
                    param_bonificacion2.ParameterName = "@bonif2"
                    param_bonificacion2.SqlDbType = SqlDbType.Decimal
                    param_bonificacion2.Precision = 18
                    param_bonificacion2.Scale = 2
                    param_bonificacion2.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value Is DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value Is "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif2).Value)
                    param_bonificacion2.Direction = ParameterDirection.Input

                    Dim param_bonificacion3 As New SqlClient.SqlParameter
                    param_bonificacion3.ParameterName = "@bonif3"
                    param_bonificacion3.SqlDbType = SqlDbType.Decimal
                    param_bonificacion3.Precision = 18
                    param_bonificacion3.Scale = 2
                    param_bonificacion3.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value Is DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value Is "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif3).Value)
                    param_bonificacion3.Direction = ParameterDirection.Input

                    Dim param_bonificacion4 As New SqlClient.SqlParameter
                    param_bonificacion4.ParameterName = "@bonif4"
                    param_bonificacion4.SqlDbType = SqlDbType.Decimal
                    param_bonificacion4.Precision = 18
                    param_bonificacion4.Scale = 2
                    param_bonificacion4.Value = 0
                    param_bonificacion4.Value = 0 'IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif4).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif4).Value)
                    param_bonificacion4.Direction = ParameterDirection.Input

                    Dim param_bonificacion5 As New SqlClient.SqlParameter
                    param_bonificacion5.ParameterName = "@bonif5"
                    param_bonificacion5.SqlDbType = SqlDbType.Decimal
                    param_bonificacion5.Precision = 18
                    param_bonificacion5.Scale = 2
                    param_bonificacion5.Scale = 0
                    param_bonificacion5.Value = 0 'IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif5).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonif5).Value)
                    param_bonificacion5.Direction = ParameterDirection.Input

                    Dim param_preciocosto As New SqlClient.SqlParameter
                    param_preciocosto.ParameterName = "@preciofinal"
                    param_preciocosto.SqlDbType = SqlDbType.Decimal
                    param_preciocosto.Precision = 18
                    param_preciocosto.Scale = 2
                    param_preciocosto.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioUni).Value, Decimal)
                    param_preciocosto.Direction = ParameterDirection.Input

                    Dim param_peso As New SqlClient.SqlParameter
                    param_peso.ParameterName = "@peso"
                    param_peso.SqlDbType = SqlDbType.Decimal
                    param_peso.Precision = 18
                    param_peso.Scale = 2
                    param_peso.Value = CType(IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value.ToString = "", 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value), Decimal)
                    param_peso.Direction = ParameterDirection.Input

                    Dim param_qty As New SqlClient.SqlParameter
                    param_qty.ParameterName = "@qty"
                    param_qty.SqlDbType = SqlDbType.Decimal
                    param_qty.Precision = 18
                    param_qty.Scale = 2
                    param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Double)
                    param_qty.Direction = ParameterDirection.Input

                    Dim param_subtotal As New SqlClient.SqlParameter
                    param_subtotal.ParameterName = "@subtotal"
                    param_subtotal.SqlDbType = SqlDbType.Decimal
                    param_subtotal.Precision = 18
                    param_subtotal.Scale = 2
                    param_subtotal.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value, Decimal)
                    'grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = param_subtotal.Value  ' Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double) * CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, Double), 2)
                    param_subtotal.Direction = ParameterDirection.Input

                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@idunidad"
                    param_idunidad.SqlDbType = SqlDbType.VarChar
                    param_idunidad.Size = 25
                    param_idunidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value 'grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMaterial).Value
                    param_idunidad.Direction = ParameterDirection.Input

                    Dim param_notadet As New SqlClient.SqlParameter
                    param_notadet.ParameterName = "@nota_det"
                    param_notadet.SqlDbType = SqlDbType.VarChar
                    param_notadet.Size = 100
                    param_notadet.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Nota_Det).Value)
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
                        param_OrdenItem.Value = i + 1 'IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.orden).Value Is DBNull.Value, i + 1, grdItems.Rows(i).Cells(ColumnasDelGridItems.orden).Value)
                    End If
                    param_OrdenItem.Direction = ParameterDirection.Input

                    Dim param_nuevo As New SqlClient.SqlParameter
                    param_nuevo.ParameterName = "@nuevo"
                    param_nuevo.SqlDbType = SqlDbType.Bit
                    param_nuevo.Value = nuevo
                    param_nuevo.Direction = ParameterDirection.Input

                    Dim param_IdProveedor As New SqlClient.SqlParameter
                    param_IdProveedor.ParameterName = "@IdProveedor"
                    param_IdProveedor.SqlDbType = SqlDbType.VarChar
                    param_IdProveedor.Size = 25
                    param_IdProveedor.Value = IIf(txtIdProveedor.Text = "", cmbPROVEEDORES.SelectedValue, txtIdProveedor.Text)
                    param_IdProveedor.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Dim param_MENSAJE As New SqlClient.SqlParameter
                    param_MENSAJE.ParameterName = "@mensaje"
                    param_MENSAJE.SqlDbType = SqlDbType.VarChar
                    param_MENSAJE.Size = 200
                    param_MENSAJE.Value = DBNull.Value
                    param_MENSAJE.Direction = ParameterDirection.InputOutput

                    Try
                        If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdendeCompra_Det_Insert", _
                                                param_id, param_IdOrdenCompra, param_idmaterial, param_idmat_prov, _
                                                param_precio, param_iva, param_peso, _
                                                param_bonificacion1, param_bonificacion2, param_bonificacion3, param_bonificacion4, param_bonificacion5, _
                                                param_preciocosto, param_qty, param_subtotal, param_idunidad, _
                                                param_IdProveedor, param_OrdenItem, param_notadet, param_nuevo, _
                                                param_useradd, param_res)

                            'res = param_res.Value

                        Else
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spOrdendeCompra_Det_Update", _
                                            param_id, param_IdOrdenCompra, param_idmaterial, param_idmat_prov, param_qty, _
                                            param_precio, param_iva, param_peso, _
                                            param_bonificacion1, param_bonificacion2, param_bonificacion3, param_bonificacion4, param_bonificacion5, _
                                            param_preciocosto, param_idunidad, _
                                            param_notadet, param_nuevo, param_IdProveedor, _
                                            param_subtotal, param_useradd, param_res, param_OrdenItem, param_MENSAJE)

                        End If

                        res = param_res.Value

                        If res = -20 Then
                            MsgBox("La cantidad ingresada para el Item " & grdItems.Rows(i).Cells(ColumnasDelGridItems.Producto).Value & " es menor al saldo actual.", MsgBoxStyle.Critical, "Atención")
                        End If

                        If res = -10 Then
                            Util.MsgStatus(Status1, "No se puede modificar el material '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Producto).Value.ToString.Substring(0, 30) & "...'" & vbCrLf & _
                                           "La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                        End If

                        If (res <= 0) Then
                            Exit For
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                    'End If
                    'MsgBox(i)

                Next

                AgregarActualizar_Registro_Items = res

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

    Private Function EliminarRegistro(ByVal Estado As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
            ''Abrir una transaccion para guardar y asegurar que se guarda todo
            'If Abrir_Tran(connection) = False Then
            '    MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Function
            'End If

            Try

                Dim param_idordendecompra As New SqlClient.SqlParameter("@idordendecompra", SqlDbType.BigInt, ParameterDirection.Input)
                param_idordendecompra.Value = CType(txtID.Text, Long)
                param_idordendecompra.Direction = ParameterDirection.Input

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
                    If Estado = "Anular" Then
                        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spOrdenDeCompra_Delete_All", param_idordendecompra, param_userdel, param_res)
                    Else
                        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spOrdenDeCompra_Delete_Finalizar", param_idordendecompra, param_userdel, param_res)
                    End If

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
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Private Function EliminarRegistroItem(ByVal IDoc As Long, ByVal item As Integer) As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Try

                Dim param_idoc As New SqlClient.SqlParameter
                param_idoc.ParameterName = "@idoc"
                param_idoc.SqlDbType = SqlDbType.BigInt
                param_idoc.Value = IDoc
                param_idoc.Direction = ParameterDirection.Input

                Dim param_item As New SqlClient.SqlParameter
                param_item.ParameterName = "@item"
                param_item.SqlDbType = SqlDbType.BigInt
                param_item.Value = item
                param_item.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = txtSubtotal.Text
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_montoivatotal As New SqlClient.SqlParameter
                param_montoivatotal.ParameterName = "@MontoIvaTotal"
                param_montoivatotal.SqlDbType = SqlDbType.Decimal
                param_montoivatotal.Precision = 18
                param_montoivatotal.Scale = 2
                param_montoivatotal.Value = txtMontoIVA.Text
                param_montoivatotal.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@Total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = txtTotal.Text
                param_total.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spOrdenDeCompra_Det_Delete_Item", _
                                              param_idoc, param_item, param_subtotal, _
                                              param_montoivatotal, param_total, param_userdel, param_res)
                    res = param_res.Value

                    EliminarRegistroItem = res

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

    Private Function Agregar_Marca(ByVal NombreMarca As String, ByRef IdMarcaNueva As Long) As Integer

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 10
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 300
            param_nombre.Value = NombreMarca
            param_nombre.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMarcas_Insert", param_id, _
                                          param_codigo, param_nombre, param_res)

                IdMarcaNueva = param_id.Value

                Agregar_Marca = param_res.Value


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

    Private Function Agregar_Material(ByVal Unidad As Long, ByVal Codigo As String, ByVal Nombre As String, _
                                  ByVal PrecioVta As Double, ByVal Stock As Double, ByVal preciolista As Double, _
                                  ByVal Ganancia As Double, ByVal PlazoEntrega As String, ByVal Codigo_Mat_Prov As String, _
                                  ByVal IVA As Double, ByVal IdMoneda As Long, ByVal bonif1 As Double, ByVal bonif2 As Double, ByVal bonif3 As Double, _
                                  ByVal bonif4 As Double, ByVal bonif5 As Double, ByRef IdMat_Prov As Long, ByRef idMarca As Long) As Long
        Dim res As Integer = 0
        Dim ultid As Integer = 0

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_idalmacen As New SqlClient.SqlParameter
            param_idalmacen.ParameterName = "@idalmacen"
            param_idalmacen.SqlDbType = SqlDbType.BigInt
            param_idalmacen.Value = 1
            param_idalmacen.Direction = ParameterDirection.Input

            Dim param_idfamilia As New SqlClient.SqlParameter
            param_idfamilia.ParameterName = "@idfamilia"
            param_idfamilia.SqlDbType = SqlDbType.BigInt
            param_idfamilia.Value = 30
            param_idfamilia.Direction = ParameterDirection.Input

            Dim param_idsubrubro As New SqlClient.SqlParameter
            param_idsubrubro.ParameterName = "@idsubrubro"
            param_idsubrubro.SqlDbType = SqlDbType.BigInt
            param_idsubrubro.Value = 98
            param_idsubrubro.Direction = ParameterDirection.Input

            Dim param_idunidad As New SqlClient.SqlParameter
            param_idunidad.ParameterName = "@idunidad"
            param_idunidad.SqlDbType = SqlDbType.BigInt
            param_idunidad.Value = Unidad
            param_idunidad.Direction = ParameterDirection.Input

            Dim param_idmoneda As New SqlClient.SqlParameter
            param_idmoneda.ParameterName = "@idmoneda"
            param_idmoneda.SqlDbType = SqlDbType.BigInt
            param_idmoneda.Value = IdMoneda
            param_idmoneda.Direction = ParameterDirection.Input

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = Codigo
            param_codigo.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.NVarChar
            param_nombre.Size = 4000
            param_nombre.Value = LTrim(RTrim(Nombre))
            param_nombre.Direction = ParameterDirection.Input

            Dim param_preciolista As New SqlClient.SqlParameter
            param_preciolista.ParameterName = "@preciovtasiniva"
            param_preciolista.SqlDbType = SqlDbType.Decimal
            param_preciolista.Precision = 18
            param_preciolista.Scale = 2
            param_preciolista.Value = PrecioVta
            param_preciolista.Direction = ParameterDirection.Input

            Dim param_ganancia As New SqlClient.SqlParameter
            param_ganancia.ParameterName = "@ganancia"
            param_ganancia.SqlDbType = SqlDbType.Decimal
            param_ganancia.Precision = 18
            param_ganancia.Scale = 2
            param_ganancia.Value = Ganancia
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

            Dim param_CodBarra As New SqlClient.SqlParameter
            param_CodBarra.ParameterName = "@CodBarra"
            param_CodBarra.SqlDbType = SqlDbType.VarChar
            param_CodBarra.Size = 50
            param_CodBarra.Value = ""
            param_CodBarra.Direction = ParameterDirection.Input

            Dim param_Pasillo As New SqlClient.SqlParameter
            param_Pasillo.ParameterName = "@Pasillo"
            param_Pasillo.SqlDbType = SqlDbType.VarChar
            param_Pasillo.Size = 50
            param_Pasillo.Value = ""
            param_Pasillo.Direction = ParameterDirection.Input

            Dim param_Estante As New SqlClient.SqlParameter
            param_Estante.ParameterName = "@Estante"
            param_Estante.SqlDbType = SqlDbType.VarChar
            param_Estante.Size = 50
            param_Estante.Value = ""
            param_Estante.Direction = ParameterDirection.Input

            Dim param_Fila As New SqlClient.SqlParameter
            param_Fila.ParameterName = "@Fila"
            param_Fila.SqlDbType = SqlDbType.VarChar
            param_Fila.Size = 50
            param_Fila.Value = ""
            param_Fila.Direction = ParameterDirection.Input

            Dim param_ControlStock As New SqlClient.SqlParameter
            param_ControlStock.ParameterName = "@ControlStock"
            param_ControlStock.SqlDbType = SqlDbType.Bit
            param_ControlStock.Value = 0
            param_ControlStock.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_Insert", _
                                    param_id, param_idalmacen, param_idfamilia, param_idsubrubro, param_idunidad, _
                                    param_idmoneda, param_codigo, param_nombre, param_preciolista, param_ganancia, _
                                    param_minimo, param_maximo, param_stockinicial, param_CodBarra, param_Pasillo, _
                                    param_Estante, param_Fila, param_ControlStock, param_useradd, _
                                    param_res)

                res = param_res.Value

                If res > 0 Then
                    ultid = param_id.Value
                    res = Agregar_Proveedor(param_id.Value, Codigo_Mat_Prov, Unidad, PrecioVta, preciolista, _
                                            PlazoEntrega, IVA, Ganancia, IdMoneda, _
                                            bonif1, bonif2, bonif3, bonif4, bonif5, IdMat_Prov, idMarca)

                    If res > 0 Then
                        Agregar_Material = ultid
                    Else
                        Agregar_Material = -1
                    End If

                End If

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

    Private Function Agregar_Proveedor(ByVal idmaterial As Long, ByVal CodMatProv As String, ByVal IdUnidad As Long, _
                                    ByVal precio As Double, ByVal preciolista As Double, ByVal plazoentrega As String, _
                                    ByVal IVA As Double, ByVal ganancia As Double, ByVal idmonedaCompra As Long, ByVal bonif1 As Double, ByVal bonif2 As Double, ByVal bonif3 As Double, _
                                    ByVal bonif4 As Double, ByVal bonif5 As Double, ByRef IdMat_Prov As Long, ByRef idMarca As Long) As Integer

        Dim res As Integer = 0

        Dim param_id As New SqlClient.SqlParameter
        param_id.ParameterName = "@id"
        param_id.SqlDbType = SqlDbType.BigInt
        param_id.Value = DBNull.Value
        param_id.Direction = ParameterDirection.InputOutput

        Dim param_idmaterial As New SqlClient.SqlParameter
        param_idmaterial.ParameterName = "@idmaterial"
        param_idmaterial.SqlDbType = SqlDbType.BigInt
        param_idmaterial.Value = idmaterial
        param_idmaterial.Direction = ParameterDirection.Input

        Dim param_CodMatProv As New SqlClient.SqlParameter
        param_CodMatProv.ParameterName = "@Codigo_Mat_Prov"
        param_CodMatProv.SqlDbType = SqlDbType.VarChar
        param_CodMatProv.Size = 50
        param_CodMatProv.Value = CodMatProv
        param_CodMatProv.Direction = ParameterDirection.Input

        Dim param_idProveedor As New SqlClient.SqlParameter
        param_idProveedor.ParameterName = "@idProveedor"
        param_idProveedor.SqlDbType = SqlDbType.BigInt
        param_idProveedor.Value = txtIdProveedor.Text
        param_idProveedor.Direction = ParameterDirection.Input

        Dim param_PlazoEntrega As New SqlClient.SqlParameter
        param_PlazoEntrega.ParameterName = "@PlazoEntrega"
        param_PlazoEntrega.SqlDbType = SqlDbType.VarChar
        param_PlazoEntrega.Size = 50
        param_PlazoEntrega.Value = plazoentrega
        param_PlazoEntrega.Direction = ParameterDirection.Input

        Dim param_idunidadcompra As New SqlClient.SqlParameter
        param_idunidadcompra.ParameterName = "@idunidadcompra"
        param_idunidadcompra.SqlDbType = SqlDbType.BigInt
        param_idunidadcompra.Value = IdUnidad
        param_idunidadcompra.Direction = ParameterDirection.Input

        Dim param_idmonedacompra As New SqlClient.SqlParameter
        param_idmonedacompra.ParameterName = "@idmonedacompra"
        param_idmonedacompra.SqlDbType = SqlDbType.BigInt
        param_idmonedacompra.Value = idmonedaCompra
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

        Dim param_bonif1_dis As New SqlClient.SqlParameter
        param_bonif1_dis.ParameterName = "@bonif1_dis"
        param_bonif1_dis.SqlDbType = SqlDbType.Decimal
        param_bonif1_dis.Precision = 18
        param_bonif1_dis.Scale = 2
        param_bonif1_dis.Value = 0
        param_bonif1_dis.Direction = ParameterDirection.Input

        Dim param_bonif2_dis As New SqlClient.SqlParameter
        param_bonif2_dis.ParameterName = "@bonif2_dis"
        param_bonif2_dis.SqlDbType = SqlDbType.Decimal
        param_bonif2_dis.Precision = 18
        param_bonif2_dis.Scale = 2
        param_bonif2_dis.Value = 0
        param_bonif2_dis.Direction = ParameterDirection.Input

        Dim param_bonificacion3 As New SqlClient.SqlParameter
        param_bonificacion3.ParameterName = "@bonif3"
        param_bonificacion3.SqlDbType = SqlDbType.Decimal
        param_bonificacion3.Precision = 18
        param_bonificacion3.Scale = 2
        param_bonificacion3.Value = bonif3
        param_bonificacion3.Direction = ParameterDirection.Input

        Dim param_bonificacion4 As New SqlClient.SqlParameter
        param_bonificacion4.ParameterName = "@bonif4"
        param_bonificacion4.SqlDbType = SqlDbType.Decimal
        param_bonificacion4.Precision = 18
        param_bonificacion4.Scale = 2
        param_bonificacion4.Value = bonif4
        param_bonificacion4.Direction = ParameterDirection.Input

        Dim param_bonificacion5 As New SqlClient.SqlParameter
        param_bonificacion5.ParameterName = "@bonif5"
        param_bonificacion5.SqlDbType = SqlDbType.Decimal
        param_bonificacion5.Precision = 18
        param_bonificacion5.Scale = 2
        param_bonificacion5.Value = bonif5
        param_bonificacion5.Direction = ParameterDirection.Input

        Dim param_ganancia As New SqlClient.SqlParameter
        param_ganancia.ParameterName = "@ganancia"
        param_ganancia.SqlDbType = SqlDbType.Decimal
        param_ganancia.Precision = 18
        param_ganancia.Scale = 2
        param_ganancia.Value = ganancia
        param_ganancia.Direction = ParameterDirection.Input

        Dim param_ganancia_dist As New SqlClient.SqlParameter
        param_ganancia_dist.ParameterName = "@ganancia_dis"
        param_ganancia_dist.SqlDbType = SqlDbType.Decimal
        param_ganancia_dist.Precision = 18
        param_ganancia_dist.Scale = 2
        param_ganancia_dist.Value = ganancia
        param_ganancia_dist.Direction = ParameterDirection.Input

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
        param_preciolista.Value = 0
        param_preciolista.Direction = ParameterDirection.Input

        Dim param_preciolista_dis As New SqlClient.SqlParameter
        param_preciolista_dis.ParameterName = "@preciolista_distribuidor"
        param_preciolista_dis.SqlDbType = SqlDbType.Decimal
        param_preciolista_dis.Precision = 18
        param_preciolista_dis.Scale = 2
        param_preciolista_dis.Value = 0
        param_preciolista_dis.Direction = ParameterDirection.Input

        Dim param_preciovtasiniva As New SqlClient.SqlParameter
        param_preciovtasiniva.ParameterName = "@PrecioVentaSinIva"
        param_preciovtasiniva.SqlDbType = SqlDbType.Decimal
        param_preciovtasiniva.Precision = 18
        param_preciovtasiniva.Scale = 2
        param_preciovtasiniva.Value = 0
        param_preciovtasiniva.Direction = ParameterDirection.Input

        Dim param_iva As New SqlClient.SqlParameter
        param_iva.ParameterName = "@Iva"
        param_iva.SqlDbType = SqlDbType.Decimal
        param_iva.Precision = 18
        param_iva.Scale = 2
        param_iva.Value = 0
        param_iva.Direction = ParameterDirection.Input

        Dim param_iva_dis As New SqlClient.SqlParameter
        param_iva_dis.ParameterName = "@Iva_dis"
        param_iva_dis.SqlDbType = SqlDbType.Decimal
        param_iva_dis.Precision = 18
        param_iva_dis.Scale = 2
        param_iva_dis.Value = 0
        param_iva_dis.Direction = ParameterDirection.Input

        Dim param_idmarca As New SqlClient.SqlParameter
        param_idmarca.ParameterName = "@idmarca"
        param_idmarca.SqlDbType = SqlDbType.BigInt
        param_idmarca.Value = idMarca
        param_idmarca.Direction = ParameterDirection.Input

        Dim param_res As New SqlClient.SqlParameter
        param_res.ParameterName = "@res"
        param_res.SqlDbType = SqlDbType.Int
        param_res.Value = DBNull.Value
        param_res.Direction = ParameterDirection.InputOutput

        Try
            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_Proveedor_Det_Insert", _
                                      param_id, param_idmaterial, param_CodMatProv, param_idProveedor, param_PlazoEntrega, param_idunidadcompra, param_idmonedacompra, _
                                      param_bonificacion1, param_bonificacion2, param_bonif1_dis, param_bonif2_dis, _
                                      param_bonificacion3, param_bonificacion4, param_bonificacion5, param_ganancia, param_ganancia_dist, _
                                      param_precioxmt, param_precioxkg, param_pesoxmetro, param_cantxlongitud, param_pesoxunidad, _
                                      param_preciolista, param_preciolista_dis, param_preciovtasiniva, _
                                      param_iva, param_iva_dis, param_idmarca, param_res)

            res = CInt(param_res.Value)

            If res > 0 Then
                IdMat_Prov = param_id.Value
            End If

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

    Private Function BuscarIdPorCodigo(ByVal codCC As String, ByVal codUsuario As String, ByRef idCC As Long, ByRef idUsuario As Long) As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Try
                Dim param_codigocc As New SqlClient.SqlParameter
                param_codigocc.ParameterName = "@codigocc"
                param_codigocc.SqlDbType = SqlDbType.VarChar
                param_codigocc.Size = 25
                param_codigocc.Value = codCC
                param_codigocc.Direction = ParameterDirection.Input

                Dim param_param_codigousuario As New SqlClient.SqlParameter
                param_param_codigousuario.ParameterName = "@codigousuario"
                param_param_codigousuario.SqlDbType = SqlDbType.VarChar
                param_param_codigousuario.Size = 25
                param_param_codigousuario.Value = codUsuario
                param_param_codigousuario.Direction = ParameterDirection.Input

                Dim param_idcc As New SqlClient.SqlParameter
                param_idcc.ParameterName = "@idcc"
                param_idcc.SqlDbType = SqlDbType.BigInt
                param_idcc.Value = DBNull.Value
                param_idcc.Direction = ParameterDirection.Output

                Dim param_idusuario As New SqlClient.SqlParameter
                param_idusuario.ParameterName = "@idusuario"
                param_idusuario.SqlDbType = SqlDbType.BigInt
                param_idusuario.Value = 0
                param_idusuario.Direction = ParameterDirection.Output

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBuscarIdPorCodigo", param_codigocc, param_param_codigousuario, param_idcc, param_idusuario, param_res)
                    res = param_res.Value

                    If Not param_idcc.Value Is DBNull.Value Then
                        idCC = param_idcc.Value
                    End If

                    If Not param_idusuario.Value Is DBNull.Value Then
                        idUsuario = param_idusuario.Value
                    End If

                    BuscarIdPorCodigo = res

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

    Private Function CuentaRecepcionesPorOrdenDeCompra(ByVal IDoc As String) As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Try

                Dim param_idoc As New SqlClient.SqlParameter
                param_idoc.ParameterName = "@idoc"
                param_idoc.SqlDbType = SqlDbType.BigInt
                param_idoc.Value = IDoc
                param_idoc.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.Output

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCuentaRecepcionesPorOrdenDeCompra", param_idoc, param_res)
                    res = param_res.Value

                    CuentaRecepcionesPorOrdenDeCompra = res

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

    Private Function BuscarProveedor(ByVal codigoOC As String, ByRef Solicitud As Boolean) As String
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim dsProveedor As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BuscarProveedor = ""
            Exit Function
        End Try

        Try

            dsProveedor = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT p.nombre, solicitud FROM OrdenDeCompra o JOIN Proveedores p	ON P.id = o.IdProveedor WHERE o.codigo = " & codigoOC)

            dsProveedor.Dispose()

            If dsProveedor.Tables(0).Rows.Count > 0 Then
                BuscarProveedor = dsProveedor.Tables(0).Rows(0).Item(0)
                Solicitud = dsProveedor.Tables(0).Rows(0).Item(1)
            Else
                BuscarProveedor = ""
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

            BuscarProveedor = ""

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    ' Capturar los enter del formulario, descartar todos salvo los que 
    ' se dan dentro de la grilla. Es una sobre carga de un metodo existente.

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
                    cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det)
                Else
                    ' Selecionamos la primera columna de la siguiente fila.
                    cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdOrdenDeCompra_Det)
                End If
            Else
                ' Seleccionamos la celda de la derecha de la celda actual.
                cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
            End If
            ' establecer la fila y la columna actual
            columnIndex = cell.ColumnIndex
            rowIndex = cell.RowIndex
        Loop While (cell.Visible = False)

        Try
            grdItems.CurrentCell = cell
        Catch ex As Exception

        End Try

        Try
            'If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Qty Then
            '    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_material, grdItems.CurrentRow.Index + 1)
            '    Return True
            'End If

            'If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Cod_material Then
            '    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Qty, grdItems.CurrentRow.Index)
            '    Return True
            'End If

        Catch ex As Exception

        End Try

        ' ... y la ponemos en modo de edición.
        grdItems.BeginEdit(True)
        Return True

    End Function

#End Region

#Region "   Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click


        If MessageBox.Show("Desea generar una nueva Orden de Compra?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkEliminado.Checked = False
        cmbPROVEEDORES.Enabled = True
        cmbCONDICIONDEPAGO.Enabled = True

        grdItems.Enabled = True
        dtpFECHA.Enabled = True
        txtNota.Enabled = True

        Try
            grdItems.Columns(ColumnasDelGridItems.Status).Visible = Not bolModo

            grdItems.Columns(ColumnasDelGridItems.Saldo).Visible = Not bolModo
        Catch ex As Exception

        End Try

        If btnBand_Copiar = True Then
            Util.LimpiarTextBox(Me.Controls)
        End If

        PrepararGridItems()

        lblCantidadFilas.Text = "0"

        'cmbAutoriza.SelectedValue = 2

        lblStatus.Text = "CONFECCIONANDO"

        btnCopiarOC.Enabled = False
        btnFinalizar.Enabled = False

        dtpFECHA.Focus()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer, res_item As Integer
        Dim registro As Integer

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        'If chkEliminado.Checked = True Then
        '    Util.MsgStatus(Status1, "No se puede actualizar el registo. Está Eliminado", My.Resources.Resources.stop_error.ToBitmap, True)
        '    Exit Sub
        'End If

        ''''''''''''''''para posicionarme en la fila actual...
        If Not (bolModo) Then
            registro = grd.CurrentRow.Index
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de Orden de Compra (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de Orden de Compra (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                    Case Else
                        Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)
                        res_item = AgregarActualizar_Registro_Items()
                        Select Case res_item
                            Case -30
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se puedo el producto en el stock.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se puedo el producto en el stock. '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -10
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se puede modificar el material. La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap)
                            Case -3
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se puedo insertar en stock el codigo '" & cod_aux & "'", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -2
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "El registro ya existe (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar el ajuste (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo registrar el ajuste (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pudo agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)

                            Case Else
                                Cerrar_Tran()

                                'LlenarcmbMarcasProductos()

                                rdPendientes.Checked = 1

                                SQL = "exec spOrdenDeCompra_Select_All @Eliminado = " & rdAnuladas.Checked & ", @Pendientes = " & rdPendientes.Checked & ", @PendientesyCumplidas = " & rdTodasOC.Checked

                                bolModo = False
                                PrepararBotones()
                                MDIPrincipal.NoActualizarBase = False
                                btnActualizar_Click(sender, e)

                                btnCopiarOC.Enabled = True
                                btnFinalizar.Enabled = True

                                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
            End If 'if ALTa
        End If 'If bolpoliticas Then

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        '
        ' Para borrar un vale hay que tener un permiso especial de eliminacion
        ' ademas, no se puede borrar un vale ya eliminado de antes.
        ' La eliminacion es lógica...y se reversan los items para ajustar el inventario
        '
        Dim res As Integer

        If MessageBox.Show("¿Está seguro que desea Anular la OC seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If chkEliminado.Checked = False Then

            'si tiene al menos una recepcion cargada no se puede eliminar ...
            res = CuentaRecepcionesPorOrdenDeCompra(CType(txtID.Text, Long))
            If res >= 1 Then
                Util.MsgStatus(Status1, "No se puede Anular la Orden de Compra ya que tiene 'Recepciones' efectuadas.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se puede Anular la Orden de Compra ya que tiene 'Recepciones' efectuadas.", My.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If MessageBox.Show("Esta acción Anulará todos los items." + vbCrLf + "¿Está seguro que desea Anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro("Anular")
                Select Case res
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo Anular la OC.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Anular la OC.", My.Resources.stop_error.ToBitmap, True)
                    Case 0
                        Util.MsgStatus(Status1, "No se pudo Anular la OC.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Anular la OC.", My.Resources.stop_error.ToBitmap, True)
                    Case Else
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        Util.MsgStatus(Status1, "Se ha Anulado la OC.", My.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se ha Anulado la OC.", My.Resources.ok.ToBitmap, True, True)
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de Anulado cancelada.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Acción de Anulado cancelada.", My.Resources.stop_error.ToBitmap, True)
            End If
        Else
            Util.MsgStatus(Status1, "El registro ya está Anulado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está Anulado.", My.Resources.stop_error.ToBitmap, True)
        End If
    End Sub

    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Dim res As Integer

        If MessageBox.Show("¿Está seguro que desea Finalizar la OC seleccionada?" & vbCrLf & "Todos los items pendientes con saldo MENOR a la cantidad pedida quedarán con el estado Finalizado", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If chkEliminado.Checked = False Then

            'si tiene al menos una recepcion puede finalizar la OC
            res = CuentaRecepcionesPorOrdenDeCompra(CType(txtID.Text, Long))

            If res = 0 Then
                Util.MsgStatus(Status1, "No se puede Finalizar la Orden de Compra ya que no tiene 'Recepciones' efectuadas." & vbCrLf & "Si desea puede Anular la OC seleccionada.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se puede Finalizar la Orden de Compra ya que no tiene 'Recepciones' efectuadas." & vbCrLf & "Si desea puede Anular la OC seleccionada.", My.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            'If MessageBox.Show("Esta acción Finalizará el proceso en todos los items." + vbCrLf + "¿Está seguro que desea Finalizar la OC?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Util.MsgStatus(Status1, "Finalizando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro("Finalizar")
            Select Case res
                Case -1
                    Util.MsgStatus(Status1, "No se pudo Finalizar la OC.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Finalizar la OC.", My.Resources.stop_error.ToBitmap, True)
                Case 0
                    Util.MsgStatus(Status1, "No se pudo Finalizar la OC.", My.Resources.stop_error.ToBitmap)
                    Util.MsgStatus(Status1, "No se pudo Finalizar la OC.", My.Resources.stop_error.ToBitmap, True)
                Case Else
                    PrepararBotones()
                    btnActualizar_Click(sender, e)
                    Util.MsgStatus(Status1, "Se ha Finalizado la OC.", My.Resources.ok.ToBitmap)
                    Util.MsgStatus(Status1, "Se ha Finalizado la OC.", My.Resources.ok.ToBitmap, True, True)
            End Select
            'Else
            '    Util.MsgStatus(Status1, "Acción de Finalizado cancelada.", My.Resources.stop_error.ToBitmap)
            '    Util.MsgStatus(Status1, "Acción de Finalizado cancelada.", My.Resources.stop_error.ToBitmap, True)
            'End If
        Else
            Util.MsgStatus(Status1, "El registro ya está Finalizado.", My.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "El registro ya está Finalizado.", My.Resources.stop_error.ToBitmap, True)
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Solicitud As Boolean

        nbreformreportes = "Ordenes de Compra"

        param.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)

            rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)

            rpt.OrdenesDeCompraPorkys_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString, Solicitud)

            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        rdPendientes.Checked = 1
        btnCopiarOC.Enabled = True
        btnFinalizar.Enabled = True
        bolModo = False
    End Sub

    Private Sub btnCopiarOC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopiarOC.Click
        Dim UltId As Long

        If MessageBox.Show("Desea copiar la Orden de Compra actual, para generar una nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            UltId = txtID.Text

            btnBand_Copiar = False
            btnNuevo_Click(sender, e)

            txtID.Text = UltId

            LlenarGrid_Items()

            txtID.Text = ""
        End If

    End Sub

#End Region









End Class