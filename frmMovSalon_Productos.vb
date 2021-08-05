Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO




Public Class frmMovSalon_Productos

    Dim band As Integer
    Dim ds_Producto As Data.DataSet
    'Varible de transaccion
    Dim tran As SqlClient.SqlTransaction
    Dim conn_del_form As SqlClient.SqlConnection = Nothing
    Dim CodAlmacen As Integer
    Dim AperturaPack As Boolean

    Enum ColumnasDelGridItems
        Item = 0
        IdMaterial = 1
        CodigoBarra = 2
        Producto = 3
        IdUnidad = 4
        Unidad = 5
        Cantidad = 6
        Eliminar = 7
    End Enum


#Region "   Eventos"

    Private Sub frmPedidosWEB_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1 'agregar
                If btnAgregar.Enabled Then
                    btnAgregar_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                If BtnGuardar.Enabled Then
                    BtnGuardar_Click(sender, e)
                End If
            Case Keys.F7 'atras
                If btnAtras.Enabled Then
                    btnAtras_Click(sender, e)
                End If
            Case Keys.F8 'siguiente
                If BtnSiguiente.Enabled Then
                    BtnSiguiente_Click(sender, e)
                End If
            Case Keys.F9
                btnCancelar_Click(sender, e)
        End Select
    End Sub

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load
        band = 0
        BtnGuardar.Enabled = False
        btnAgregar.Enabled = False
        lblErrorPack.Visible = False
        lblErrorU.Visible = False
        Try

            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Try
            ds_Producto = SqlHelper.ExecuteDataset(conn_del_form, CommandType.Text, "SELECT codigo,Nombre FROM Almacenes WHERE Nombre like '%Salon%'")
            ds_Producto.Dispose()
            CodAlmacen = ds_Producto.Tables(0).Rows(0)(0)
        Catch ex As Exception
            MsgBox("No se pudo obtener los datos del almacen. Intente más tarde", MsgBoxStyle.Critical)
            Exit Sub
        End Try
        'le paso el texto de las opciones 
        groupProductosPack.Text = frmMovSalon_Opciones.Operacion
        groupProductosU.Text = groupProductosPack.Text

        AperturaPack = True
        If Not groupProductosPack.Text = "Abrir Pack" Then
            lblProductosPack.Text = "Camara"
            lblProductosUnidad.Text = "Salón"
            Label1.Text = "Selecionar Producto:"
            PictureBoxIcono.Image = My.Resources.IconoEnvio
            AperturaPack = False
        End If

        Me.LlenarCombo_ProductosPack()
        Me.LlenarCombo_ProductosUnidad()

        band = 0
        LimpiarFormulario()
        txtCodigoBarraPack.Focus()
    End Sub

    Private Sub chkRelacion_CheckedChanged(sender As Object, e As EventArgs) Handles chkRelacion.CheckedChanged
        lblCodigoBarraRelacion.Visible = chkRelacion.Checked
        lblProdRelacion.Visible = chkRelacion.Checked
        lblUnidadRelacion.Visible = chkRelacion.Checked
        'lblIdMaterialRelacion.Visible = chkRelacion.Checked
        'lblIDunidadRelacion.Visible = chkRelacion.Checked
        btnAgregar.Enabled = chkRelacion.Checked
    End Sub

    '----------------------------pack
    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProductosPack.SelectedValueChanged
        If band = 1 Then

            Dim sqlstring As String = " SELECT m.Codigo, m.Nombre,m.IdUnidad,U.Nombre,m.CantidadPACK,CodigoBarra,UnidadRef,S.Qty " & _
                                      " FROM Materiales m Join Unidades U ON U.codigo = m.idunidad" & _
                                      " join Stock s on s.IdMaterial = m.codigo" & _
                                      " where m.Codigo = '" & cmbProductosPack.SelectedValue & "' and s.idAlmacen = " & CodAlmacen & _
                                      " and (m.IDUnidad = 'HORMA' or m.IDUnidad = 'BOLSA' or m.IDUnidad = 'CJA' or m.IDUnidad = 'TIRA' or m.IDUnidad = 'PACK' or m.IDUnidad = 'ENV')"

            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Producto.Dispose()
            Try
                lblIdUnidadPack.Text = ds_Producto.Tables(0).Rows(0)(2).ToString
                lblUnidadPack.Text = ds_Producto.Tables(0).Rows(0)(3).ToString
                lblCantidadxPack.Text = ds_Producto.Tables(0).Rows(0)(4).ToString
                txtCodigoBarraPack.Text = ds_Producto.Tables(0).Rows(0)(5).ToString
                lblIdUnidadRef.Text = ds_Producto.Tables(0).Rows(0)(6).ToString
                lblStockPack.Text = ds_Producto.Tables(0).Rows(0)(7).ToString

                If AperturaPack Then
                    chkRelacion.Checked = BuscarRelacion(cmbProductosPack.SelectedValue)
                Else
                    sqlstring = "select Nombre from Unidades where Codigo = '" & lblIdUnidadRef.Text & "'"
                    ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Producto.Dispose()
                    lblUnidadRef.Text = ds_Producto.Tables(0).Rows(0)(0).ToString
                End If

            Catch ex As Exception

            End Try




        End If
    End Sub

    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProductosPack.KeyDown
        If e.KeyData = Keys.Enter Then
            'SendKeys.Send("{TAB}")
            txtCantidadPack.Focus()
        End If
    End Sub

    Private Sub cmbProducto_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbProductosPack.KeyUp

        If cmbProductosPack.Text = "" Then
            LimpiarFormulario()
        End If

    End Sub

    Private Sub txtCodigoBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigoBarraPack.KeyDown

        If txtCodigoBarraPack.Text <> "" Then
            Dim sqlstring As String = " SELECT m.Codigo, m.Nombre,m.IdUnidad,U.Nombre,m.CantidadPACK,UnidadRef,S.Qty " & _
                                      " FROM Materiales m Join Unidades U ON U.codigo = m.idunidad" & _
                                      " join Stock s on s.IdMaterial = m.codigo" & _
                                      " where m.CodigoBarra = '" & txtCodigoBarraPack.Text & "' and s.IdAlmacen = " & CodAlmacen & _
                                      " and (m.IDUnidad = 'HORMA' or m.IDUnidad = 'BOLSA' or m.IDUnidad = 'CJA' or m.IDUnidad = 'TIRA' or m.IDUnidad = 'PACK' or m.IDUnidad = 'ENV') "

            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Producto.Dispose()

            Try
                band = 0
                cmbProductosPack.SelectedValue = ds_Producto.Tables(0).Rows(0)(0).ToString
                band = 1
                cmbProductosPack.Text = ds_Producto.Tables(0).Rows(0)(1).ToString
                lblIdUnidadPack.Text = ds_Producto.Tables(0).Rows(0)(2).ToString
                lblUnidadPack.Text = ds_Producto.Tables(0).Rows(0)(3).ToString
                lblCantidadxPack.Text = ds_Producto.Tables(0).Rows(0)(4).ToString
                lblIdUnidadRef.Text = ds_Producto.Tables(0).Rows(0)(5).ToString
                lblStockPack.Text = ds_Producto.Tables(0).Rows(0)(6).ToString
                If AperturaPack Then
                    chkRelacion.Checked = BuscarRelacion(cmbProductosPack.SelectedValue)
                Else
                    sqlstring = "select Nombre from Unidades where Codigo = '" & lblIdUnidadRef.Text & "'"
                    ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Producto.Dispose()
                    lblUnidadRef.Text = ds_Producto.Tables(0).Rows(0)(0).ToString
                End If
                txtCantidadPack.Focus()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtCodigoBarra_GotFocus(sender As Object, e As EventArgs) Handles txtCodigoBarraPack.GotFocus
        txtCodigoBarraPack.BackColor = Color.Aqua
        band = 0
    End Sub

    Private Sub txtCodigoBarra_LostFocus(sender As Object, e As EventArgs) Handles txtCodigoBarraPack.LostFocus
        txtCodigoBarraPack.BackColor = SystemColors.Window
        band = 1
    End Sub

    Private Sub txtCantidadP_GotFocus(sender As Object, e As EventArgs) Handles txtCantidadPack.GotFocus
        txtCantidadPack.BackColor = Color.Aqua
    End Sub

    Private Sub txtCantidad_LostFocus(sender As Object, e As EventArgs) Handles txtCantidadPack.LostFocus
        txtCantidadPack.BackColor = SystemColors.Window
    End Sub

    Private Sub txtCantidadPack_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidadPack.KeyDown
        If e.KeyCode = Keys.Enter Then
            If AperturaPack Then
                If chkRelacion.Checked Then
                    btnAgregar.Focus()
                Else
                    BtnSiguiente_Click(sender, e)
                End If
            Else
                If ControlProducto(True) Then
                    btnAgregar_Click(sender, e)
                End If
            End If
        End If
    End Sub

    Private Sub grdPack_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdPack.CellClick
        If e.ColumnIndex = 7 Then
            If MessageBox.Show("Está seguro que desea eliminar el producto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'borro la fila de la grilla de pack
                grdPack.Rows.RemoveAt(e.RowIndex)
                'guardo el indice
                Dim index As Integer = e.RowIndex
                'borro la fila de la grilla unidad
                grdUnidad.Rows.RemoveAt(index)
                'me fijo si en la grilla hay algo y cambio el orden de los item
                If grdPack.Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To grdPack.Rows.Count - 1
                        grdPack.Rows(i).Cells(0).Value = i + 1
                        grdUnidad.Rows(i).Cells(0).Value = i + 1
                    Next
                Else
                    BtnGuardar.Enabled = False
                End If
            End If
        End If
    End Sub

    '-----------------------------unidad
    Private Sub cmbProductosU_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProductosU.SelectedValueChanged
        If band = 1 Then

            Dim sqlstring As String = " SELECT m.Codigo, m.Nombre,m.IdUnidad,U.Nombre,m.CantidadPACK,CodigoBarra,s.Qty " & _
                                      " FROM Materiales m Join Unidades U ON U.codigo = m.idunidad" & _
                                      " join Stock s on s.IdMaterial = m.codigo" & _
                                      " where m.Codigo = '" & cmbProductosU.SelectedValue & "' and s.IdAlmacen = " & CodAlmacen & _
                                      " and (m.IDUnidad <> 'HORMA' and m.IDUnidad <> 'BOLSA' and m.IDUnidad <> 'CJA' and m.IDUnidad <> 'TIRA' and m.IDUnidad <> 'PACK' and m.IDUnidad <> 'ENV' )"

            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Producto.Dispose()
            Try
                lblIdUnidadU.Text = ds_Producto.Tables(0).Rows(0)(2).ToString
                lblUnidadU.Text = ds_Producto.Tables(0).Rows(0)(3).ToString
                txtCodigoBarraU.Text = ds_Producto.Tables(0).Rows(0)(5).ToString
                lblStockUnidad.Text = ds_Producto.Tables(0).Rows(0)(6).ToString
            Catch ex As Exception

            End Try




        End If
    End Sub

    Private Sub cmbProductosU_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbProductosU.KeyUp
        If cmbProductosU.Text = "" Then
            LimpiarFormulario()
        End If
    End Sub

    Private Sub cmbProductosU_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProductosU.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnAgregar.Enabled = True
            btnAgregar.Focus()
        End If
    End Sub

    Private Sub txtCodigoBarraU_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigoBarraU.KeyDown
        If txtCodigoBarraU.Text <> "" Then
            Dim sqlstring As String = " SELECT m.Codigo, m.Nombre,m.IdUnidad,U.Nombre,m.CantidadPACK,s.qty " & _
                                      " FROM Materiales m Join Unidades U ON U.codigo = m.idunidad" & _
                                      " join Stock s on s.IdMaterial = m.codigo" & _
                                      " where m.CodigoBarra = '" & txtCodigoBarraU.Text & "' and s.IdAlmacen = " & CodAlmacen & _
                                      " and (m.IDUnidad <> 'HORMA' and m.IDUnidad <> 'BOLSA' and m.IDUnidad <> 'CJA' and m.IDUnidad <> 'TIRA' and m.IDUnidad <> 'PACK' and m.IDUnidad <> 'ENV')"

            ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Producto.Dispose()

            Try
                band = 0
                cmbProductosU.SelectedValue = ds_Producto.Tables(0).Rows(0)(0).ToString
                band = 1
                cmbProductosU.Text = ds_Producto.Tables(0).Rows(0)(1).ToString
                lblIdUnidadU.Text = ds_Producto.Tables(0).Rows(0)(2).ToString
                lblUnidadU.Text = ds_Producto.Tables(0).Rows(0)(3).ToString
                lblStockUnidad.Text = ds_Producto.Tables(0).Rows(0)(5).ToString
                cmbProductosU.Focus()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtCodigoBarraU_GotFocus(sender As Object, e As EventArgs) Handles txtCodigoBarraU.GotFocus
        txtCodigoBarraU.BackColor = Color.Aqua
        band = 0
    End Sub

    Private Sub txtCodigoBarraU_LostFocus(sender As Object, e As EventArgs) Handles txtCodigoBarraU.LostFocus
        txtCodigoBarraU.BackColor = SystemColors.Window
        band = 1
    End Sub

    Private Sub grdUnidad_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdUnidad.CellClick
        If e.ColumnIndex = 7 Then
            If MessageBox.Show("Está seguro que desea eliminar el producto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'borro la fila de la grilla de pack
                grdUnidad.Rows.RemoveAt(e.RowIndex)
                'guardo el indice
                Dim index As Integer = e.RowIndex
                'borro la fila de la grilla unidad
                grdPack.Rows.RemoveAt(index)
                'me fijo si en la grilla hay algo y cambio el orden de los item
                If grdUnidad.Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To grdUnidad.Rows.Count - 1
                        grdUnidad.Rows(i).Cells(0).Value = i + 1
                        grdPack.Rows(i).Cells(0).Value = i + 1
                    Next
                Else
                    BtnGuardar.Enabled = False
                End If
            End If
        End If

    End Sub

#End Region

#Region "   Botones"

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim i As Integer
        If AperturaPack Then
            If chkRelacion.Checked Then
                If ControlProducto(True) Then
                    i = grdPack.Rows.Count
                    grdPack.Rows.Add(i + 1, cmbProductosPack.SelectedValue, txtCodigoBarraPack.Text, cmbProductosPack.Text, lblIdUnidadPack.Text, lblUnidadPack.Text, txtCantidadPack.Text, "Eliminar")
                    i = grdUnidad.Rows.Count
                    Dim cantidadU As String = Math.Round(CDbl(txtCantidadPack.Text) * CDbl(lblCantidadxPack.Text), 2).ToString
                    grdUnidad.Rows.Add(i + 1, lblIdMaterialRelacion.Text, lblCodigoBarraRelacion.Text, lblProdRelacion.Text, lblIDunidadRelacion.Text, lblUnidadRelacion.Text, cantidadU, "Eliminar")
                End If
            Else
                If ControlProducto(False) Then
                    i = grdPack.Rows.Count
                    grdPack.Rows.Add(i + 1, lblIdMaterialE.Text, lblCodigobarrapackE.Text, lblProductoPackE.Text, lblIdUnidadPackE.Text, lblUnidadPackE.Text, lblCantidadPackE.Text, "Eliminar")
                    i = grdUnidad.Rows.Count
                    grdUnidad.Rows.Add(i + 1, cmbProductosU.SelectedValue, txtCodigoBarraU.Text, cmbProductosU.Text, lblIdUnidadU.Text, lblUnidadU.Text, lblCantidadU.Text, "Eliminar")
                End If
            End If
        Else
            If ControlProducto(True) Then
                i = grdPack.Rows.Count
                grdPack.Rows.Add(i + 1, cmbProductosPack.SelectedValue, txtCodigoBarraPack.Text, cmbProductosPack.Text, lblIdUnidadPack.Text, lblUnidadPack.Text, txtCantidadPack.Text, "Eliminar")
                i = grdUnidad.Rows.Count
                Dim cantidadU As String = Math.Round(CDbl(txtCantidadPack.Text) * CDbl(lblCantidadxPack.Text), 2).ToString
                grdUnidad.Rows.Add(i + 1, cmbProductosPack.SelectedValue, txtCodigoBarraPack.Text, cmbProductosPack.Text, lblIdUnidadRef.Text, lblUnidadRef.Text, cantidadU, "Eliminar")
            End If
        End If
        BtnGuardar.Enabled = True
        Try
            grdPack.SelectedRows(0).Selected = False
            grdUnidad.SelectedRows(0).Selected = False
        Catch ex As Exception

        End Try
        LimpiarFormulario()
        groupProductosPack.Visible = True
        txtCodigoBarraPack.Focus()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim res As Integer = 0
        res = RealizarMovimiento_Items()
        Select Case res
            Case -1
                Cancelar_Tran()
                MsgBox("No se pudo registrar el movimiento.", MsgBoxStyle.Critical)
                Exit Sub
            Case -3
                Cancelar_Tran()
                MsgBox("No se pudo actualizar la relación entre los productos.", MsgBoxStyle.Critical)
                Exit Sub
            Case -4
                Cancelar_Tran()
                MsgBox("No se pudo insertar la relación entre los productos.", MsgBoxStyle.Critical)
                Exit Sub
            Case -5
                Cancelar_Tran()
                MsgBox("No se pudo registrar el movimiento de producto PACK.", MsgBoxStyle.Critical)
                Exit Sub
            Case -6
                Cancelar_Tran()
                MsgBox("No se pudo actualizar el stock del producto PACK.", MsgBoxStyle.Critical)
                Exit Sub
            Case -7
                Cancelar_Tran()
                MsgBox("No se pudo registrar el movimiento de producto UNIDAD.", MsgBoxStyle.Critical)
                Exit Sub
            Case -8
                Cancelar_Tran()
                MsgBox("No se pudo actualizar el stock del producto UNIDAD.", MsgBoxStyle.Critical)
                Exit Sub
            Case -9
                Cancelar_Tran()
                MsgBox("No se pudo registrar el movimiento del producto a TRANSFERIR.", MsgBoxStyle.Critical)
                Exit Sub
            Case -10
                Cancelar_Tran()
                MsgBox("No se pudo actualizar el stock del producto a TRANSFERIR.", MsgBoxStyle.Critical)
                Exit Sub
            Case 0
                Cancelar_Tran()
                MsgBox("No se logró registrar movimiento.", MsgBoxStyle.Critical)
                Exit Sub
            Case Else
                Cerrar_Tran()
                groupProductosPack.Visible = True
                LimpiarFormulario()
                LimpiarGrillayBtn()
                txtCodigoBarraPack.Focus()
        End Select
    End Sub

    Private Sub btnAtras_Click(sender As Object, e As EventArgs) Handles btnAtras.Click
        If grdPack.Rows.Count = 0 And grdUnidad.Rows.Count = 0 Then
            BtnGuardar.Enabled = False
        End If
        BtnSiguiente.Enabled = True
        If AperturaPack Then
            If groupProductosPack.Visible Then
                If grdPack.Rows.Count > 0 Then
                    If MessageBox.Show("Hay productos cargados para realizar un movimientos. Desea descartar el proceso?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
                Me.Close()
            Else
                LimpiarFormulario()
                If groupProductosU.Visible Then
                    btnAgregar.Enabled = False
                    groupProductosPack.Visible = True
                    txtCodigoBarraPack.Focus()
                Else
                    groupProductosU.Visible = True
                    txtCodigoBarraU.Focus()
                End If
            End If
        Else
            If groupProductosPack.Visible Then
                If grdPack.Rows.Count > 0 Then
                    If MessageBox.Show("Hay productos cargados para realizar un movimientos. Desea descartar el proceso?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
                Me.Close()
            End If
        End If

    End Sub

    Private Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles BtnSiguiente.Click
        If AperturaPack Then
            If groupProductosPack.Visible Then
                If ControlProducto(True) Then
                    groupProductosPack.Visible = False
                    groupProductosU.Visible = True
                    BtnSiguiente.Enabled = False

                    lblIdMaterialE.Text = cmbProductosPack.SelectedValue
                    lblCodigobarrapackE.Text = txtCodigoBarraPack.Text
                    lblProductoPackE.Text = cmbProductosPack.Text
                    lblUnidadPackE.Text = lblUnidadPack.Text
                    lblIdUnidadPackE.Text = lblIdUnidadPack.Text
                    lblCantidadPackE.Text = txtCantidadPack.Text

                    lblCantidadU.Text = Math.Round(CDbl(txtCantidadPack.Text) * CDbl(lblCantidadxPack.Text), 2).ToString
                    'BtnGuardar.Enabled = True
                    txtCodigoBarraU.Focus()
                End If
                'Else
                ''groupProductosU.Visible = False
                'BtnSiguiente.Enabled = False
                'BtnGuardar.Enabled = True
            End If
        Else
            'groupProductosPack.Visible = False
            groupProductosU.Visible = False
            BtnSiguiente.Enabled = False
            BtnGuardar.Enabled = True
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        LimpiarFormulario()
        LimpiarGrillayBtn()
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub LlenarCombo_ProductosPack()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '0' as Codigo,'' as 'Producto' UNION SELECT Codigo, Nombre as 'Producto' FROM Materiales  WHERE nombre not like '%**FR%' and eliminado = 0 and (IDUnidad = 'HORMA' or IDUnidad = 'BOLSA' or IDUnidad = 'CJA' or IDUnidad = 'TIRA' or IDUnidad = 'PACK' ) Order by 'Producto' ASC")
            ds_Equipos.Dispose()

            With Me.cmbProductosPack
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

    Private Sub LlenarCombo_ProductosUnidad()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '0' as Codigo,'' as 'Producto' UNION SELECT Codigo, Nombre as 'Producto' FROM Materiales  WHERE nombre not like '%**FR%' and eliminado = 0 and (IDUnidad <> 'HORMA' and IDUnidad <> 'BOLSA' and IDUnidad <> 'CJA' and IDUnidad <> 'TIRA' and IDUnidad <> 'PACK' and IDUnidad <> 'ENV' ) Order by 'Producto' ASC")
            ds_Equipos.Dispose()

            With Me.cmbProductosU
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

    Private Sub LimpiarFormulario()
        cmbProductosPack.SelectedValue = "0"
        txtCantidadPack.Text = ""
        txtCodigoBarraPack.Text = ""
        lblUnidadPack.Text = ""
        lblCantidadxPack.Text = ""
        lblCodigoBarraRelacion.Text = ""
        lblProdRelacion.Text = ""
        lblUnidadRelacion.Text = ""
        lblStockPack.Text = ""
        chkRelacion.Checked = False
        cmbProductosU.SelectedValue = "0"
        txtCodigoBarraU.Text = ""
        lblUnidadU.Text = ""
        lblStockUnidad.Text = ""
        'lblCantidadU.Text = ""
        'lblCodigobarrapackE.Text = ""
        'lblProductoPackE.Text = ""
        'lblUnidadPackE.Text = ""

        btnAgregar.Enabled = False
        lblErrorPack.Visible = False
    End Sub

    Private Sub LimpiarGrillayBtn()
        grdPack.Rows.Clear()
        grdUnidad.Rows.Clear()
        btnAgregar.Enabled = False
        BtnGuardar.Enabled = False
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

#End Region

#Region "   Funciones"

    Private Function RealizarMovimiento_Items() As Integer

        Dim res As Integer = 0
        Dim i As Integer

        Try
            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        'Abrir una transaccion para guardar y asegurar que se guarda todo
        If Abrir_Tran(conn_del_form) = False Then
            MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        Try
            Try
                i = 0

                Do While i < grdPack.Rows.Count


                    Dim param_idAlmacen As New SqlClient.SqlParameter
                    param_idAlmacen.ParameterName = "@IDALMACEN"
                    param_idAlmacen.SqlDbType = SqlDbType.BigInt
                    param_idAlmacen.Value = CodAlmacen
                    param_idAlmacen.Direction = ParameterDirection.Input

                    Dim param_aperturapack As New SqlClient.SqlParameter
                    param_aperturapack.ParameterName = "@APERTURAPACK"
                    param_aperturapack.SqlDbType = SqlDbType.Bit
                    param_aperturapack.Value = IIf(AperturaPack = True, 1, 0)
                    param_aperturapack.Direction = ParameterDirection.Input

                    Dim param_idmaterial As New SqlClient.SqlParameter
                    param_idmaterial.ParameterName = "@IDPACK"
                    param_idmaterial.SqlDbType = SqlDbType.VarChar
                    param_idmaterial.Size = 25
                    param_idmaterial.Value = grdPack.Rows(i).Cells(ColumnasDelGridItems.IdMaterial).Value
                    param_idmaterial.Direction = ParameterDirection.Input


                    Dim param_idunidad As New SqlClient.SqlParameter
                    param_idunidad.ParameterName = "@IDUNIDADPACK"
                    param_idunidad.SqlDbType = SqlDbType.VarChar
                    param_idunidad.Size = 25
                    param_idunidad.Value = IIf(grdPack.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value Is DBNull.Value, "U", grdPack.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value)
                    param_idunidad.Direction = ParameterDirection.Input


                    Dim param_qtyenviada As New SqlClient.SqlParameter
                    param_qtyenviada.ParameterName = "@QTYPACK"
                    param_qtyenviada.SqlDbType = SqlDbType.Decimal
                    param_qtyenviada.Precision = 18
                    param_qtyenviada.Scale = 2
                    param_qtyenviada.Value = CType(grdPack.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal)
                    param_qtyenviada.Direction = ParameterDirection.Input

                    Dim param_idmaterialUnitario As New SqlClient.SqlParameter
                    param_idmaterialUnitario.ParameterName = "@IDUnitario"
                    param_idmaterialUnitario.SqlDbType = SqlDbType.VarChar
                    param_idmaterialUnitario.Size = 25
                    param_idmaterialUnitario.Value = grdUnidad.Rows(i).Cells(ColumnasDelGridItems.IdMaterial).Value
                    param_idmaterialUnitario.Direction = ParameterDirection.Input


                    Dim param_idunidadunitario As New SqlClient.SqlParameter
                    param_idunidadunitario.ParameterName = "@IDUnidadUnitario"
                    param_idunidadunitario.SqlDbType = SqlDbType.VarChar
                    param_idunidadunitario.Size = 25
                    param_idunidadunitario.Value = IIf(grdUnidad.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value Is DBNull.Value, "U", grdUnidad.Rows(i).Cells(ColumnasDelGridItems.IdUnidad).Value)
                    param_idunidadunitario.Direction = ParameterDirection.Input


                    Dim param_qtyunitario As New SqlClient.SqlParameter
                    param_qtyunitario.ParameterName = "@QTYUnitario"
                    param_qtyunitario.SqlDbType = SqlDbType.Decimal
                    param_qtyunitario.Precision = 18
                    param_qtyunitario.Scale = 2
                    param_qtyunitario.Value = CType(grdUnidad.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value, Decimal)
                    param_qtyunitario.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@userupd"
                    param_useradd.SqlDbType = SqlDbType.Int
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

   
                    Try

                        'If chkVentas.Checked = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_StockMovSalon_Insert", _
                                           param_idAlmacen, param_aperturapack, param_idmaterial, param_idunidad, param_qtyenviada, _
                                           param_idmaterialUnitario, param_idunidadunitario, param_qtyunitario, _
                                           param_useradd, param_res)

                        res = param_res.Value

                        If (res <= 0) Then
                            Exit Do
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                    i = i + 1

                Loop

                RealizarMovimiento_Items = res

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

    Private Function ControlProducto(ByVal desdePack As Boolean) As Boolean

        If desdePack Then
            'control codigobarra y cmbproducto
            If txtCodigoBarraPack.Text = "" And cmbProductosPack.Text = "" Then
                lblErrorPack.Text = "Debe seleccionar un producto pack. Por favor verifique el dato."
                lblErrorPack.Visible = True
                txtCodigoBarraPack.Focus()
                ControlProducto = False
                Exit Function
            End If
            'control cantidad 
            If txtCantidadPack.Text = "" Then
                lblErrorPack.Text = "Debe escribir una cantidad válida. Por favor verifique el dato."
                lblErrorPack.Visible = True
                txtCantidadPack.Focus()
                ControlProducto = False
                Exit Function
            Else
                If CDbl(txtCantidadPack.Text) = 0 Then
                    lblErrorPack.Text = "Debe escribir una cantidad mayor que cero. Por favor verifique el dato."
                    lblErrorPack.Visible = True
                    txtCantidadPack.Focus()
                    ControlProducto = False
                    Exit Function
                End If
            End If
            If lblCantidadxPack.Text = "" Then
                lblErrorPack.Text = "El producto no tiene especificado la cantidad de unidades que posee. Por favor verifique el dato."
                lblErrorPack.Visible = True
                txtCantidadPack.Focus()
                ControlProducto = False
                Exit Function
            Else
                If CDbl(lblCantidadxPack.Text) = 0 Then
                    lblErrorPack.Text = "El producto posee una cantidad de unidades inválida. Por favor verifique el dato."
                    lblErrorPack.Visible = True
                    txtCantidadPack.Focus()
                    ControlProducto = False
                    Exit Function
                End If
            End If
        Else
            'control codigobarra y cmbproducto
            If txtCodigoBarraU.Text = "" And cmbProductosU.Text = "" Then
                lblErrorU.Text = "Debe seleccionar un producto Unidad. Por favor verifique el dato."
                lblErrorU.Visible = True
                txtCodigoBarraPack.Focus()
                ControlProducto = False
                Exit Function
            End If
        End If
        lblErrorPack.Visible = False
        lblErrorU.Visible = False
        ControlProducto = True
    End Function

    Private Function BuscarRelacion(ByVal IdMaterial As String) As Boolean

        ds_Producto = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, " select Mat.IDMatUnidad,M.CodigoBarra,M.Nombre,M.IDUnidad,U.Nombre  " & _
                                                                                " from Materiales_RelacionPack Mat Join Materiales M on Mat.IDMatUnidad = M.Codigo " & _
                                                                                " join Unidades U on u.Codigo = m.IDUnidad  where Mat.IDMatPack = '" & cmbProductosPack.SelectedValue & "' and Mat.Eliminado = 0")
        ds_Producto.Dispose()

        If ds_Producto.Tables(0).Rows.Count > 0 Then

            lblIdMaterialRelacion.Text = ds_Producto.Tables(0).Rows(0)(0)
            lblCodigoBarraRelacion.Text = ds_Producto.Tables(0).Rows(0)(1)
            lblProdRelacion.Text = ds_Producto.Tables(0).Rows(0)(2)
            lblIDunidadRelacion.Text = ds_Producto.Tables(0).Rows(0)(3)
            lblUnidadRelacion.Text = ds_Producto.Tables(0).Rows(0)(4)
            BuscarRelacion = True
        Else
            BuscarRelacion = False
        End If

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









   
End Class