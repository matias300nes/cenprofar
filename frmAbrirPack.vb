Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient

Public Class frmAbriPack

    Dim ds_Empresa As Data.DataSet
    Dim Band As Boolean
    Dim idproductopack As String
    Dim almacenpack As String
    Dim unidadpack As String
    Dim cantXpack As Double
    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient

 
#Region "   Eventos"
    Private Sub AbriPack_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Band = False

        If MDIPrincipal.DesdePedidos = False Then
            'TRAIGO LOS VALORES DEL PRODUCTO UNITARIO 
            lblProductoUnitario.Text = frmVentasWEB.producto_unitario
            txtIdProductoUnitario.Text = frmVentasWEB.idproducto_unitario
            txtAlmacenUnitario.Text = frmVentasWEB.almacen_unitario
            txtStockUnitario.Text = frmVentasWEB.stock_unitario
            txtUnidadUnitario.Text = frmVentasWEB.unidad_unitario
            almacenpack = Utiles.numero_almacen
        Else
            lblProductoUnitario.Text = frmPedidosWEB.producto_unitario
            txtIdProductoUnitario.Text = frmPedidosWEB.idproducto_unitario
            txtAlmacenUnitario.Text = frmPedidosWEB.almacen_unitario
            txtStockUnitario.Text = frmPedidosWEB.stock_unitario
            txtUnidadUnitario.Text = frmPedidosWEB.unidad_unitario
            almacenpack = frmPedidosWEB.almacen_unitario
        End If

        Me.LlenarCombo_Productos()

        'ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT s.qty,m.Minimo,M.CantidadPACK FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo where s.idalmacen = " & almacenpack & " and m.Codigo = " & cmbProducto.SelectedValue)
        'ds_Empresa.Dispose()

        'lblStock.Text = ds_Empresa.Tables(0).Rows(0)(0)

        'ControlStock(ds_Empresa.Tables(0).Rows(0)(0))

        'lblUnidades.Text = ds_Empresa.Tables(0).Rows(0)(2).ToString


        Band = True




    End Sub

    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged

        If Band = True Then

            lblUnidades.Visible = True
            txtUnidades.Visible = False

            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT s.qty,m.Codigo,m.idAlmacen,m.idunidad,m.CantidadPACK,m.Minimo FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo where s.idalmacen = " & almacenpack & " and m.Codigo = " & cmbProducto.SelectedValue)
            ds_Empresa.Dispose()

            lblStock.Text = ds_Empresa.Tables(0).Rows(0)(0)
            idproductopack = ds_Empresa.Tables(0).Rows(0)(1)
            ' almacenpack = frmPedidosWEB.almacen_unitario
            unidadpack = ds_Empresa.Tables(0).Rows(0)(3)
            Label1.Text = txtUnidadUnitario.Text + "/ " + unidadpack
            Label9.Text = "Cant. " + unidadpack
            cantXpack = ds_Empresa.Tables(0).Rows(0)(4)
            lblUnidades.Text = (ds_Empresa.Tables(0).Rows(0)(4)).ToString
            'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
            ControlStock(ds_Empresa.Tables(0).Rows(0)(5))

            If cantXpack = 0 Then
                If CDbl(lblStock.Text) > 0 Then
                    lblUnidades.Visible = False
                    txtUnidades.Visible = True
                    txtUnidades.Focus()
                End If
            End If

        End If

    End Sub

    Private Sub txtCantidad_KeyUp(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyUp

        Try
            If txtCantidad.Text <> "" Then
                If txtUnidades.Visible = True Then
                    lblUnidadesNew.Text = (FormatNumber(CDbl(txtCantidad.Text) * CDbl(txtUnidades.Text), 2)).ToString
                Else
                    lblUnidadesNew.Text = (FormatNumber(CDbl(txtCantidad.Text) * CDbl(lblUnidades.Text), 2)).ToString
                End If
            Else
                lblUnidadesNew.Text = "0.00"
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtUnidades_LostFocus(sender As Object, e As EventArgs) Handles txtUnidades.LostFocus

        Try
            If txtUnidades.Visible = True Then
                cantXpack = CDbl(txtUnidades.Text)
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Botones"
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        MDIPrincipal.actualizarstock = False
        'cierro la ventana de abrirpack
        Me.Close()
    End Sub

    Private Sub btnAbrirPack_Click(sender As Object, e As EventArgs) Handles btnAbrirPack.Click

        Verificar_Datos()
        If bolpoliticas = True Then
            If AbrirPack() = True Then
                MDIPrincipal.actualizarstock = True
                'CIERRO LA VENTANA DE ABRIRPACK
                Me.Close()
            Else
                MDIPrincipal.actualizarstock = False
            End If
        End If

    End Sub

#End Region


#Region "Procedimientos"

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

            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT '' AS Codigo, '' AS Producto UNION SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales m JOIN Marcas ma ON m.idmarca = ma.Codigo WHERE m.eliminado = 0 and idunidad = 'PACK' or idunidad = 'BOLSA' ")
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

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If cmbProducto.SelectedValue = "" Then
            MsgBox("Por favor seleccione un producto.", MsgBoxStyle.Information, "Atención")
            cmbProducto.Focus()
            Exit Sub
        End If


        If txtCantidad.Text <> "" Then
            If CDbl(txtCantidad.Text) = 0 Then
                MsgBox("la cantidad ingresada no puede ser igual a cero. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                txtCantidad.Focus()
                Exit Sub
            Else
                If CDbl(txtCantidad.Text) > CDbl(lblStock.Text) Then
                    MsgBox("la cantidad ingresada no puede superar al stock. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    txtCantidad.Focus()
                    Exit Sub
                End If
            End If
        Else
            MsgBox("la cantidad ingresada no es válida. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            txtCantidad.Focus()
            Exit Sub
        End If


        If txtUnidades.Visible = True Then
            If txtUnidades.Text = "" Then
                MsgBox("Por favor ingrese las cantidades que hay el PACK/BOLSA.", MsgBoxStyle.Information, "Atención")
                Exit Sub
            End If
            If CDbl(txtUnidades.Text) = 0 Then
                MsgBox("Por favor ingrese un valor válido de cantidades que hay el PACK/BOLSA.", MsgBoxStyle.Information, "Atención")
                Exit Sub
            End If
        End If

        'If lblStock.Text <> "" Then
        '    If CDbl(lblStock.Text) <= 0 Then
        '        If MessageBox.Show("No hay Stock suficiente, desea abrir el pack igual?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '            txtCantidad.Focus()
        '            Exit Sub
        '        End If
        '    End If
        'Else
        '    MsgBox("El Stock no es válido. Por favor, seleccione otro pack.", MsgBoxStyle.Information, "Atención")
        '    txtCantidad.Focus()
        '    Exit Sub
        'End If

        bolpoliticas = True

    End Sub

    Private Sub ControlStock(ByVal minimo As Double)

        If lblStock.Text <> "" Then
            'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
            If CDbl(lblStock.Text) > minimo Then
                lblStock.BackColor = Color.Green
            Else
                lblStock.BackColor = Color.Red
            End If
            'CONTROLO QUE EL STOCK NO SEA IGUAL O MENOR A CERO
            If CDbl(lblStock.Text) <= 0 Then
                txtCantidad.Text = ""
                txtCantidad.Enabled = False
            Else
                txtCantidad.Enabled = True
            End If
        End If
    End Sub


#End Region

#Region "   Funciones"

    Private Function AbrirPack() As Boolean

        AbrirPack = False

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Dim ds_materiales As Data.DataSet
        Dim sqlstringPack As String
        Dim sqlstringUni As String
        Dim actualizarweb As Boolean = True

        If MDIPrincipal.NoActualizar = False Then
            actualizarweb = True
        Else
            actualizarweb = False
        End If

        Try

            'CALCULO EL RESTO DEL PAQUETE
            Dim resto As Double = CDbl(lblStock.Text) - CDbl(txtCantidad.Text)
            'HAGO EL INSERT EN LA TABLA DE STOCKMOV DEL PAQUETE QUE ABRIO
            sqlstringPack = "insert into [StockMov] (IDAlmacen, IdMaterial, IDMotivo, Tipo, Stock, Qty, Saldo, IDUnidad, Comprobante ) " & _
                                            "  values ( '" & almacenpack & "', '" & idproductopack & "', '" & "1" & "','" & "E" & "', '" & lblStock.Text & "','" & txtCantidad.Text & "','" & resto.ToString & "','" & unidadpack & "','" & "Apertura Pack" & "')"
            ds_materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstringPack)

            If actualizarweb Then
                Try
                    tranWEB.Sql_Set(sqlstringPack)
                Catch ex As Exception
                    MsgBox("No se puede actualizar en la Web el movimiento de stock del PACK actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                End Try
            End If
       


            'HAGO EL UPDATE EN LA TABLA DE STOCK DEL PAQUETE
            sqlstringPack = "Update [Stock] SET Qty = '" & resto & "' WHERE IDMaterial = '" & idproductopack & "' and IDAlmacen = " & almacenpack
            ds_materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstringPack)

            If actualizarweb Then
                Try
                    tranWEB.Sql_Set(sqlstringPack)
                Catch ex As Exception
                    MsgBox("No se puede actualizar en la Web el Stock la apertura de PACK actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                End Try
            End If
       

            'CALCULO LA CANTIDAD DE UNIDADES A AUMENTAR
            Dim nuevacantidad As Double
            nuevacantidad = (CDbl(txtCantidad.Text) * cantXpack)
            resto = CDbl(txtStockUnitario.Text) + nuevacantidad
            'HAGO EL INSERT EN LA TABLA DE STOCKMOV DONDE MUESTRO COMO AUMENTA EL MATERIAL UNITARIO
            sqlstringUni = "insert into [StockMov] (IDAlmacen, IdMaterial, IDMotivo, Tipo, Stock, Qty, Saldo, IDUnidad, Comprobante ) " & _
                                            "  values ( '" & txtAlmacenUnitario.Text & "', '" & txtIdProductoUnitario.Text & "', '" & "1" & "','" & "I" & "', '" & txtStockUnitario.Text & "','" & nuevacantidad & "','" & resto.ToString & "','" & txtUnidadUnitario.Text & "','" & "Apertura Pack" & "')"
            ds_materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstringUni)

            If actualizarweb Then
                Try
                    tranWEB.Sql_Set(sqlstringUni)
                Catch ex As Exception
                    MsgBox("No se puede actualizar en la Web el movimiento de stock del producto unitario actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                End Try
            End If



            'HAGO EL UPDATE EN LA TABLA DE STOCK DEL MATERIAL UNITARIO
            sqlstringUni = "Update [Stock] SET Qty = '" & resto & "' WHERE IDMaterial = '" & txtIdProductoUnitario.Text & "' and IDAlmacen = " & txtAlmacenUnitario.Text
            ds_materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstringUni)

            If actualizarweb Then
                Try
                    tranWEB.Sql_Set(sqlstringUni)
                Catch ex As Exception
                    MsgBox("No se puede actualizar en la Web el Stock del producto unitario actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                End Try
            End If

            If MDIPrincipal.DesdePedidos = True Then
                sqlstringUni = "Select Qty from Stock where idmaterial = '" & txtIdProductoUnitario.Text & " ' and IDAlmacen = " & txtAlmacenUnitario.Text
                ds_materiales = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstringUni)

                Try
                    frmPedidosWEB.stocknuevo = ds_materiales.Tables(0).Rows(0).Item(0).ToString
                Catch ex As Exception

                End Try

            End If

            ds_materiales.Dispose()

            AbrirPack = True

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


#End Region









End Class