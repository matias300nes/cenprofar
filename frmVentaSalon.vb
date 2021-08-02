
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient



Public Class frmVentaSalon
    Dim bolpoliticas As Boolean

    Dim band As Integer

    Public IdVendedor As String
    Public NombreVendedor As String

    Dim ValorListaPrecio As Double

    'Variables para la grilla
    Dim editando_celda As Boolean

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

    Dim ds_Empresa As Data.DataSet
    Private comm As New CommManager

    'Variable para guardar el total de facturas en blanco y compras 
    Dim total_facturado
    Dim total_Compras


    Dim band_facturado As Integer

    '-------------------VARIABLES PARA LA AFIP-------------------
    Dim wsfev1 As Object
    Dim WSAA As Object

    'Dim WSAA_Padron As Object

    Dim Path As String
    Dim tra As String, cms As String, ta As String
    Dim wsdl As String, proxy As String, cache As String = ""
    Dim certificado As String, claveprivada As String
    Dim ok, expiracion, tiempoExpirado
    Dim saveTOKEN, saveSING, saveTA As String

    Public concepto, tipo_doc, nro_doc, tipo_cbte, punto_vta, _
            cbt_desde, cbt_hasta, imp_total, imp_tot_conc, imp_neto, _
            imp_iva, imp_trib, imp_op_ex, fecha_cbte, fecha_venc_pago, _
            fecha_serv_desde, fecha_serv_hasta, _
            moneda_id, moneda_ctz
    Dim tipo, pto_vta, nro, fecha, cbte_nro
    Dim idIVA, Desc, base_imp, alic, importe
    Dim cae
    'almaceno nro de factura 
    Dim nroFactura
    Dim cae2

    Dim banNota As Integer, bandComp As Integer

    'VALORES DE REFERENCIA
    Dim ref_email As String, ref_direccion As String


    Dim HOMO As Boolean '= False
    Dim TicketAccesoBool As Boolean '= False
    Dim PTOVTA As String '= False
    Dim CorreoContador As String

    Dim cuitEmpresa As String = ""
    Dim ModoPagoPredefinido As String
    Dim ClienteModificado As Long
    Dim DesdeCmbCliente As Boolean

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        Id = 0
        CodMaterial = 1
        CodigoBarra = 2
        Producto = 3
        Cantidad = 4
        Peso = 5
        Precio = 6
        PrecioVenta = 7
        PrecioSinIVA = 8
        Descuento = 9
        Subtotal = 10
        IDUnidad = 11
        SubtotalOrig = 12
        ProductoPesable = 13
    End Enum

    Enum TipoComp

        FacturaA = 1
        NotaDebitoA = 2
        NotaCreditoA = 3
        FacturaB = 6
        NotaDebitoB = 7
        NotaCreditoB = 8
        FacturaC = 11
        NotaDebitoC = 12
        NotaCreditoC = 13
        FacturaM = 51
        NotaDebitoM = 52
        NotaCreditoM = 53

    End Enum


    Public producto_unitario As String
    Public idproducto_unitario As String
    Public stock_unitario As String
    Public unidad_unitario As String
    Public almacen_unitario As String
    Dim ValorNorte_cambio As Double = 0.0
    Dim DescLista As String = ""
    Public ClienteAgregado As Boolean

    'Public actualizarstock As Boolean

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim CodBarra_Activado As Boolean = False
    Dim PesoProducto As String
    Dim ProductoPesable As Boolean = False
    Dim DescuentoMaximo As Double
    Dim TopePorcenFac As Double

    Dim asignado As Boolean
    'variable para guardar el idventa para para pasarcelo a ventasdet(esto es cuando hago una seña)
    Dim idventadet As Integer
    Dim chkenfalso As Boolean
    'variable para avisar que el pago lo estoy haciendo desde la ventana de solo pago al contado
    Public desdecontado As Boolean
    Dim contadordescuento As Integer = 0
    'valores de porcentaje de recarga en tarjetas
    Dim porcen1 As Double
    Dim cuotas1 As Double
    Dim porcen2 As Double
    Dim cuotas2 As Double
    'Para iniciar el sistema con el combo cargado de tarjetas
    Dim desdeinicio As Boolean
    'variable global para pasar el codigo de la tarjeta 
    Dim CodigoTarjeta1 As String
    Dim FocoTarjeta1 As Boolean
    Dim seleccionado1 As Boolean
    Dim CodigoTarjeta2 As String
    Dim FocoTarjeta2 As Boolean
    Dim seleccionado2 As Boolean

    Dim IDAlmacenSalon As Integer
    Dim MostrarLogin As Boolean
    'varicable para diferenciar de donde se imprime
    'Public Imprimir_Factura As Boolean
    'Variable para asignar el tipo de documento del cliente
    Dim TipoDoc As Integer
    Dim TopeCuit As Integer
    Public ValorCae As String
    Public ValorFac As String
    Public ValorVen As String






#Region "   Eventos"

    Private Sub frmVentasMostrador_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            'If bolModo = True Then
            '    If MessageBox.Show("Tiene una operación en modo Nuevo. Desea salir de todos modos?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '        If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then 'SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '            Application.Exit()
            '        End If
            '    End If
            'Else
            '    If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then 'SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '        Application.Exit()
            '    End If
            'End If

            'If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then 'SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '    Application.Exit()
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmVentasMostrador_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            'If bolModo = True Then
            '    If MessageBox.Show("Tiene una operación en modo Nuevo. Desea salir de todos modos?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '        If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then 'SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '            Application.Exit()
            '        End If
            '    End If
            'Else
            '    If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then 'SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '        Application.Exit()
            '    End If
            'End If

            'If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then 'SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
            '    Application.Exit()
            'End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmAjustes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    MostrarLogin = True
                    If txtTotal.Text > 0 Then
                        If MessageBox.Show("Esta acción cancelará la venta actual ¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            btnNuevo_Click(sender, e)
                        Else
                            txtCodigoBarra.Focus()
                        End If
                    Else
                        btnNuevo_Click(sender, e)
                    End If
                End If
            Case Keys.F4 'grabar
                'Imprimir_Factura = True
                btnGuardar_Click(sender, e)
            Case Keys.F5
                txtCodigoBarra.Focus()
            Case Keys.F6 'Contado
                chkContado.Focus()
                'si se cargo un producto puedo realizar un pago en contado directamente
                If chkContado.Checked = True Then
                    'If txtContado.Text = "" Or txtContado.Text = "0.00" Then
                    'txtContado.Focus()
                    chkContado.Checked = False
                Else
                    chkContado.Checked = True
                End If
            Case Keys.F7 'Tarjetas
                If chkTarjetas1.Enabled = True Then
                    chkTarjetas1.Focus()
                    If chkTarjetas1.Checked = False Then
                        chkTarjetas1.Checked = True
                    Else
                        chkTarjetas1.Checked = False
                    End If
                End If
            Case Keys.F8 'Cancelar
                If chkTarjetas2.Enabled = True Then
                    chkTarjetas2.Focus()
                    If chkTarjetas2.Checked = False Then
                        chkTarjetas2.Checked = True
                    Else
                        chkTarjetas2.Checked = False
                    End If
                End If
            Case Keys.F9
                'puedo usar solo el sector de descuentos si se cargo al menos un producto
                If grdItems.Rows.Count > 0 Then
                    If contadordescuento = 0 Then
                        chkDescuentoGlobal.Checked = True
                    Else
                        If contadordescuento = 1 Then
                            chkDescuentoParticular.Checked = True
                        Else
                            If contadordescuento = 2 Then
                                chkDescuentoGlobal.Checked = False
                                chkDescuentoParticular.Checked = False
                                chkDescuentoParticular.Checked = False
                                contadordescuento = 0
                                txtCodigoBarra.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                    contadordescuento = contadordescuento + 1
                Else
                    MsgBox("Debe cargar al menor un producto para poder realizar un descuento")
                    txtCodigoBarra.Focus()
                End If
                'Case Keys.F10
                '    If btnActivar.Enabled Then
                '        Imprimir_Factura = False
                '        btnActivar_Click(sender, e)
                '    End If

        End Select

        'atajo para modificar la cantidad del ultimo item
        If e.Control And e.KeyCode = Keys.C Then
            btnModCant_Click(sender, e)
        End If
        'atajo para abrir pantalla para buscar productos
        If e.Control And e.KeyCode = Keys.P Then
            Dim P As New frmBusquedaProducto
            P.ShowDialog()
            If txtCodigoBarra.Text <> "" Then
                txtCodigoBarra.Focus()
                SendKeys.Send("{ENTER}")
            End If
            If txtIdProducto.Text <> "" Then
                txtIdProducto.Focus()
                SendKeys.Send("{ENTER}")
            End If
            'txtCodigoBarra.Focus()
        End If

    End Sub

    Private Sub frmVentas_Salon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        band = 0

        Establecer_CodiReg()

        'btnGuardar.Text = "(F4) Guardar"
        btnEliminar.Text = "Devolución"
        btnImprimir.Visible = False
        btnPrimero.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False
        btnActualizar.Visible = False
        ToolStripPagina.Visible = False
        btnActivar.Visible = False
        'btnActivar.Enabled = True
        'btnActivar.Text = "(F10) S/Factura"
        btnCargaContinua.Visible = False
        ToolStripLabel1.Visible = False
        grd.Visible = False
        GroupBoxPago.Enabled = False
        PanelDescuento.Enabled = False
        'btnSalir.Visible = False

        Me.LlenarcmbCondicionIVA()
        Me.LlenarcmbProductos()
        Me.LlenarcmbComprobantes()

        band = 1
        dtpFECHA.MaxDate = Today.Date
        chkDevolucion.Visible = False
        'btnEliminar.Enabled = False
        MostrarLogin = True
        btnNuevo_Click(sender, e)

        With grdItems
            .AlternatingRowsDefaultCellStyle.BackColor = Color.PaleGreen
            .RowsDefaultCellStyle.BackColor = Color.White
        End With

        'codbarra 101
        grdItems.Columns(2).Width = 100
        'Producto 250
        grdItems.Columns(3).Width = 350
        'cantidad  97
        grdItems.Columns(4).Width = 70
        'peso 69
        grdItems.Columns(5).Width = 60
        'precio 79
        grdItems.Columns(6).Width = 90
        'Desc 90
        grdItems.Columns(9).Width = 90
        'Subtotal 93
        grdItems.Columns(10).Width = 90

        lblFecha.Text = Date.Today

        limpiarGropPago()

        

        '------------------------------------------------------Parametros
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing
        Try

            connection = SqlHelper.GetConnection(ConnStringSEI)

        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Try
            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT PTOVTA,DescuentoMaximo,TopeFacturacion,NombreEmpresaFactura, ModoPagoPredefinido, CUIT, HOMO, TA,ISNULL(CorreoContador,''), TicketAcceso, Token, Sign FROM PARAMETROS")
            'lblPVI.Text = ds_Equipos.Tables(0).Rows(0).Item(0).ToString
            'PTOVTA = ds_Equipos.Tables(0).Rows(0).Item(0).ToString
            DescuentoMaximo = ds_Equipos.Tables(0).Rows(0).Item(1).ToString
            TopePorcenFac = ds_Equipos.Tables(0).Rows(0).Item(2)
            Utiles.Empresa = LTrim(RTrim(ds_Equipos.Tables(0).Rows(0).Item(3)))
            ModoPagoPredefinido = ds_Equipos.Tables(0).Rows(0).Item(4)
            cuitEmpresa = ds_Equipos.Tables(0).Rows(0).Item(5)
            HOMO = CBool(ds_Equipos.Tables(0).Rows(0).Item(6))
            TicketAccesoBool = CBool(ds_Equipos.Tables(0).Rows(0).Item(7))
            CorreoContador = ds_Equipos.Tables(0).Rows(0).Item(8)
            saveTA = ds_Equipos.Tables(0).Rows(0).Item(9)
            saveTOKEN = ds_Equipos.Tables(0).Rows(0).Item(10)
            saveSING = ds_Equipos.Tables(0).Rows(0).Item(11)
            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo FROM Almacenes WHERE Nombre LIKE '%SALON%' ")
            IDAlmacenSalon = ds_Equipos.Tables(0).Rows(0).Item(0)
            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT PtoVta FROM Parametros_PtoVta WHERE NombreEquipo = '" & SystemInformation.ComputerName.ToString.ToUpper & "'")
            lblPVI.Text = ds_Equipos.Tables(0).Rows(0).Item(0).ToString
            PTOVTA = ds_Equipos.Tables(0).Rows(0).Item(0).ToString
            ds_Equipos.Dispose()
            'BuscarValores_Facturados()
            Me.Text = "Facturación Electrónica - " & Empresa
            lblModo.Visible = HOMO
            pathComprobantesAFIP = path_raiz & "\Comprobantes Facturas - " + Utiles.Empresa + "\"

        Catch ex As Exception
            MessageBox.Show("Se produjo un error al leer los datos de la table Parámetros", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        '------------------AFIP-----------------------
        ' Cambiamos el cursor por el de carga
        Me.Cursor = Cursors.WaitCursor
        If ConexionAfip(saveTA, saveTOKEN, saveSING) = True Then
            PicConexion.Image = My.Resources.Green_Ball_icon
            chkConexion.Checked = True
        Else
            MessageBox.Show("La conexión con el servidor de la AFIP no ha sido exitosa, por favor intente nuevamente mas tarde.", "Error de conexión", MessageBoxButtons.OK)
            PicConexion.Image = My.Resources.Red_Ball_icon
            chkConexion.Checked = False
        End If
        'dejo el cursor en flecha
        Me.Cursor = Cursors.Arrow
        '-----------------------------------------------------------------------------

        'coloco com default el icono de OK
        PicSincro.Image = My.Resources.SincroOK
        TimerVentas.Enabled = True

        limpiarGropPago()
        SendKeys.Send("{TAB}")
        SendKeys.Send("{TAB}")
        SendKeys.Send("{TAB}")
        SendKeys.Send("{TAB}")
        SendKeys.Send("{TAB}")
        SendKeys.Send("{TAB}")
        'SendKeys.Send("{TAB}")
        'SendKeys.Send("{TAB}")

    End Sub

    Private Sub chkPrecioMayorista_CheckedChanged(sender As Object, e As EventArgs)

        'If chkPrecioMayorista.Checked = True Then
        'limpio los txt
        'lblStock.Text = "0.00"
        txtPrecio.Text = "0.00"
        txtCantidad.Text = ""
        txtSubtotalItem.Text = "0.00"
        'limpio y hago foco en el combo de productos
        cmbProducto.Text = ""
        cmbProducto.Focus()
        'End If

    End Sub

    'Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown
    '    If e.KeyData = Keys.Enter And bolModo = True Then
    '        SendKeys.Send("{TAB}")
    '        If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Or ProductoPesable Then
    '            txtCantidad.Text = 1
    '            txtCantidad.Enabled = False
    '            txtPeso.Enabled = True
    '            Exit Sub
    '        Else
    '            txtPeso.Text = 0
    '            txtPeso.Enabled = False
    '            txtCantidad.Text = ""
    '            txtCantidad.Enabled = True
    '        End If
    '    End If
    'End Sub

    'Private Sub cmbProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.SelectedValueChanged


    '    If band = 1 And CodBarra_Activado = False Then


    '        ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,m.Codigo" & _
    '                                     " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
    '                                     "JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
    '                                     " where m.Codigo = '" & cmbProducto.SelectedValue & "' AND s.idalmacen = " & Utiles.numero_almacen)



    '        ds_Empresa.Dispose()

    '        Try


    '            If txtIDPrecioLista.Text = 3 Then
    '                txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(6), 2)
    '            ElseIf txtIDPrecioLista.Text = 4 Then
    '                txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(7), 2)
    '            ElseIf txtIDPrecioLista.Text = 5 Then
    '                txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(8), 2)
    '            ElseIf txtIDPrecioLista.Text = 10 Then
    '                txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(13), 2)
    '            ElseIf DescLista.Contains("NORTE") Then
    '                txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(13) * ValorNorte_cambio, 2)
    '            Else
    '                txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(9), 2)
    '            End If
    '            txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(0)
    '            unidad_unitario = txtIdUnidad.Text
    '            'lblStock.Text = ds_Empresa.Tables(0).Rows(0)(1)
    '            'stock_unitario = lblStock.Text
    '            idproducto_unitario = ds_Empresa.Tables(0).Rows(0)(2)
    '            producto_unitario = ds_Empresa.Tables(0).Rows(0)(3)
    '            almacen_unitario = ds_Empresa.Tables(0).Rows(0)(4)
    '            'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
    '            'If (ds_Empresa.Tables(0).Rows(0)(5)).ToString <> "" Then
    '            '    If CDbl(stock_unitario) > CDbl(ds_Empresa.Tables(0).Rows(0)(5)) Then
    '            '        lblStock.BackColor = Color.Green
    '            '    Else
    '            '        lblStock.BackColor = Color.Red
    '            '    End If
    '            'End If
    '            'txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(11)

    '            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Or ProductoPesable Then
    '                Label13.Text = "Peso*"
    '            Else
    '                Label13.Text = "Peso"
    '            End If

    '            ''mando una pausa y despues paso el producto a la grilla
    '            'System.Threading.Thread.Sleep(5000)
    '            'txtPeso.Focus()
    '            'SendKeys.Send("{TAB}")
    '            'txtCantidad_KeyDown(sender, e)


    '        Catch ex As Exception

    '        End Try

    '    End If

    'End Sub

    Private Sub txtIdCliente_TextChanged(sender As Object, e As EventArgs) Handles txtIdCliente.TextChanged

        Dim dsCliente As Data.DataSet
        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select C.IDPrecioLista,C.Provincia,C.Localidad,C.Direccion,C.Repartidor,L.Descripcion,L.Valor_Cambio,C.Promo From Clientes C Join Lista_Precios L On C.IDPrecioLista =L.Codigo Where C.eliminado = 0 and C.Codigo = '" & txtIdCliente.Text & "'")

        If grdItems.Rows.Count > 0 Then
            For i As Integer = 0 To grdItems.Rows.Count - 1
                If txtIDPrecioLista.Text <> "" Then
                    If txtIDPrecioLista.Text = 3 Then
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCosto From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value & "'")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)

                    ElseIf txtIDPrecioLista.Text = 4 Then
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioMayorista From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value & "'")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                    ElseIf txtIDPrecioLista.Text = 5 Then
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioLista3 From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value & "'")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                    ElseIf txtIDPrecioLista.Text = 10 Then
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCompra From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value & "'")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                        'chkTransInterna.Checked = True
                    ElseIf DescLista.Contains("NORTE") Then
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioCompra From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value & "'")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0) * ValorNorte_cambio
                    Else
                        dsCliente = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "Select PrecioLista4 From Materiales Where eliminado = 0 and Codigo = '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value & "'")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value = dsCliente.Tables(0).Rows(0).Item(0)
                    End If
                    'Me fijo si es horma o tira 
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("HORMA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("TIRA") Then
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber((grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
                    Else
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber((grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value), 2)
                    End If
                End If
            Next
            dsCliente.Dispose()
        End If

    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFECHA.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkDevolucion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDevolucion.CheckedChanged

        lblDevolucion.Visible = chkDevolucion.Checked

        LlenarcmbComprobantes()

        If chkDevolucion.Checked Then
            'GroupBox1.Style.BackColor = SystemColors.HotTrack
            'GroupBox1.Style.BackColor2 = SystemColors.Highlight
            GroupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
            GroupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
            GroupBox1.Style.BorderColor = Color.Black
            For Each Control As Object In Me.GroupBox1.Controls
                If TypeOf (Control) Is Label Then
                    Control.Forecolor = Color.Black
                End If
            Next
            chkDescuentoGlobal.ForeColor = Color.Black
            chkDescuentoParticular.ForeColor = Color.Black
        Else
            GroupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
            GroupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
            GroupBox1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
            For Each Control As Object In Me.GroupBox1.Controls
                If TypeOf (Control) Is Label Or TypeOf (Control) Is CheckBox Then
                    Control.Forecolor = Color.Blue
                End If
            Next
            chkDescuentoGlobal.ForeColor = Color.Blue
            chkDescuentoParticular.ForeColor = Color.Blue
        End If
        cmbCondicionIVA.SelectedValue = "5"

        lblPVI.ForeColor = Color.White
    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbCondicionIVA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCondicionIVA.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDCondicionIVA.Text = cmbCondicionIVA.SelectedValue
                If chkDevolucion.Checked = False Then
                    If cmbCondicionIVA.Text = "IVA Responsable Inscripto" Then
                        cmbTipoComprobante.SelectedValue = "001"
                    Else
                        cmbTipoComprobante.SelectedValue = "006"
                    End If
                Else
                    If cmbCondicionIVA.Text = "IVA Responsable Inscripto" Then
                        cmbTipoComprobante.SelectedValue = "003"
                    Else
                        cmbTipoComprobante.SelectedValue = "008"
                    End If
                End If

                If cmbCondicionIVA.Text = "Consumidor Final" Then
                    TipoDoc = 96
                    txtCliente.AccessibleName = ""
                    Label1.Text = "Cliente"
                    txtCuit.AccessibleName = ""
                    Label6.Text = "DNI/CUIT"
                    lblContadorCuit.Text = "8"
                    TopeCuit = 8
                Else
                    TipoDoc = 80
                    txtCliente.AccessibleName = "Cliente*"
                    Label1.Text = "Cliente*"
                    txtCuit.AccessibleName = "DNI/CUIT*"
                    Label6.Text = "DNI/CUIT*"
                    lblContadorCuit.Text = "11"
                    TopeCuit = 11
                End If
                If grdItems.RowCount > 0 Then
                    CalcularSubtotal()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbTipoComprobante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoComprobante.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIDTipoComprobante.Text = cmbTipoComprobante.SelectedValue
            Catch ex As Exception

            End Try

            If cmbTipoComprobante.SelectedValue = "003" Then
                cmbCondicionIVA.SelectedValue = "1"
            End If


        End If
    End Sub

    Private Sub txtCuit_TextChanged(sender As Object, e As EventArgs) Handles txtCuit.TextChanged
        ContarCaracteres(txtCuit, False, TopeCuit)
    End Sub

    Private Sub txtCliente_TextChanged(sender As Object, e As EventArgs) Handles txtCliente.TextChanged
        ContarCaracteres(txtCliente, True, 30)
    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

        If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Or ProductoPesable Then
            'If txtPeso.Text = "" Then
            '    txtSubtotal.Text = "0"
            'Else
            '    txtSubtotal.Text = FormatNumber(CDbl(txtPeso.Text) * CDbl(txtPrecioVta.Text), 2)
            'End If
            Exit Sub
        Else
            If txtCantidad.Text = "" Then
                txtSubtotalItem.Text = "0"
            Else
                txtSubtotalItem.Text = FormatNumber(CDbl(txtCantidad.Text) * CDbl(txtPrecio.Text), 2)
            End If
        End If

    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown

        If e.KeyCode = Keys.Enter Then

            If CodBarra_Activado Then
                System.Threading.Thread.Sleep(500)
                CodBarra_Activado = False
            End If

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

            If txtPrecio.Text = "" Or txtPrecio.Text = "0.00" Then
                Util.MsgStatus(Status1, "El precio del producto no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El precio del producto no es VÁLIDO.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If txtCantidad.Text = "" Or txtCantidad.Text = "0" Then
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Debe ingresar la cantidad del producto a Vender.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Or ProductoPesable Then
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
            Else
                txtPeso.Text = "0"
            End If

            Dim i As Integer
            For i = 0 To grdItems.RowCount - 1
                If cmbProducto.SelectedValue = grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value And ProductoPesable = False Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value + CDbl(txtCantidad.Text)
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalOrig).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalOrig).Value - grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value

                    CalcularSubtotal()

                    txtCodigoBarra.Text = ""
                    txtCantidad.Text = ""
                    txtPeso.Text = ""
                    txtIdProducto.Text = ""
                    'cmbProducto.Text = ""
                    'txtPrecio.Text = "0.00"
                    txtSubtotalItem.Text = "0.00"
                    ProductoPesable = False
                    'chkPrecioMayorista.Checked = False
                    txtClienteDireccion.Focus()
                    SendKeys.Send("{TAB}")
                    'Util.MsgStatus(Status1, "El producto '" & cmbProducto.Text & "' está repetido en la fila: " & (i + 1).ToString & ".", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            Next

            grdItems.Rows.Add(i + 1, cmbProducto.SelectedValue, txtCodigoBarra.Text, cmbProducto.Text, txtCantidad.Text, txtPeso.Text, txtPrecio.Text, txtPrecio.Text, FormatNumber(CDbl(txtPrecio.Text / (1 + (MDIPrincipal.iva) / 100)), 2), "0", txtSubtotalItem.Text, txtIdUnidad.Text, txtSubtotalItem.Text, ProductoPesable, "Eliminado")

            OrdenarFilas()
            CalcularSubtotal()
            Contar_Filas()
            GroupBoxPago.Enabled = True
            PanelDescuento.Enabled = True

            txtCodigoBarra.Text = ""
            txtCantidad.Text = ""
            txtPeso.Text = ""
            txtIdProducto.Text = ""
            'cmbProducto.Text = ""
            'txtPrecio.Text = "0.00"
            txtSubtotalItem.Text = "0.00"
            ProductoPesable = False

            'chkPrecioMayorista.Checked = False
            txtClienteDireccion.Focus()
            SendKeys.Send("{TAB}")



        End If

    End Sub

    Private Sub txtPeso_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPeso.KeyDown

        If e.KeyCode = Keys.Enter Then
            txtCantidad_KeyDown(sender, e)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub txtPeso_TextChanged(sender As Object, e As EventArgs) Handles txtPeso.TextChanged
        Try
            If txtIdUnidad.Text.Contains("HORMA") Or txtIdUnidad.Text.Contains("TIRA") Or ProductoPesable Then
                If txtPeso.Text = "" Then
                    txtSubtotalItem.Text = "0"
                Else
                    txtSubtotalItem.Text = FormatNumber(CDbl(txtPeso.Text) * CDbl(txtPrecio.Text), 2)
                End If
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtCodigoBarra_GotFocus(sender As Object, e As EventArgs) Handles txtCodigoBarra.GotFocus
        txtCodigoBarra.BackColor = Color.Aqua
        'cambio el modo de seleccion
        grdItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'me fijo si hay algo en la grilla para sacar la seleccion del primer item
        If grdItems.Rows.Count > 0 Then
            grdItems.Rows(0).Selected = False
        End If
    End Sub

    Private Sub txtCodigoBarra_LostFocus(sender As Object, e As EventArgs) Handles txtCodigoBarra.LostFocus
        txtCodigoBarra.BackColor = SystemColors.Window
    End Sub

    Private Sub txtCodigoBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigoBarra.KeyDown
        If band = 1 Then
            Try
                If e.KeyCode = Keys.Enter Then
                    BuscarProducto(False, txtCodigoBarra.Text)
                    txtCantidad.Text = "1"
                    txtCantidad.Select(txtCantidad.Text.Length, 0)
                    txtCantidad_KeyDown(sender, e)
                End If
                

            Catch ex As Exception

                ' MsgBox(ex.Message)
                CodBarra_Activado = False

            End Try

        End If
    End Sub

    Private Sub txtIdProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdProducto.KeyDown
        If band = 1 Then
            Try
                If e.KeyCode = Keys.Enter Then
                    BuscarProducto(True, txtIdProducto.Text)
                    txtCantidad.Text = "1"
                    txtCantidad.Select(txtCantidad.Text.Length, 0)
                    txtCantidad_KeyDown(sender, e)
                End If
                

            Catch ex As Exception

                ' MsgBox(ex.Message)
                CodBarra_Activado = False

            End Try

        End If
    End Sub

    Private Sub TimerVentas_Tick(sender As Object, e As EventArgs) Handles TimerVentas.Tick

        TimerVentas.Enabled = False
        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            connection = SqlHelper.GetConnection(ConnStringSEI.ToString)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Dim sqlstring As String = "SELECT Actualizado FROM Balanzas_Notificaciones "
        Dim ds_Balanza As Data.DataSet
        ds_Balanza = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
        ds_Balanza.Dispose()

        If ds_Balanza.Tables(0).Rows(0).Item(0) = 0 Then
            PicSincro.Image = My.Resources.SincroPendiente
        Else
            PicSincro.Image = My.Resources.SincroOK
        End If
        TimerVentas.Enabled = True

    End Sub

    '--------------------------------------Grilla-------------------------------------------------------
    Private Sub grditems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdItems.CellClick
        If e.ColumnIndex = 14 Then 'Marcar llegada
            If MessageBox.Show("Está seguro que desea eliminar el producto de la venta?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                grdItems.Rows.RemoveAt(e.RowIndex)
                CalcularSubtotal()
                PagoMultiple()
                If grdItems.Rows.Count = 0 Then
                    ActivarPago()
                    limpiarGropPago()
                    chkDescuentoGlobal.Checked = False
                    chkDescuentoParticular.Checked = False
                    GroupBoxPago.Enabled = False
                    PanelDescuento.Enabled = False
                End If
                Contar_Filas()
            End If
            txtCodigoBarra.Focus()
        End If
    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItems.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales2

    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItems.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            Dim valorcambio As Double = 0
            Dim ganancia As Double = 0, preciovta As Double = 0
            Dim idmoneda As Long = 0
            Dim nombre As String = "", codmoneda As String = ""
            Dim cell As DataGridViewCell = grdItems.CurrentCell

            If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
                Exit Sub
            End If

            'If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorductoPesable).Value = True Then
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
            'Else
            '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
            'End If

            If e.ColumnIndex = 4 And grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ProductoPesable).Value = False Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalOrig).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Precio).Value
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - IIf(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value, 0, grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value), 2)
                'Else
                '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cantidad).ReadOnly = True
            End If

            If e.ColumnIndex = 9 And chkDescuentoParticular.Checked = True Then
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubtotalOrig).Value
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value - grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Subtotal).Value, 2)
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value = FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value, 2)
                'Else
                '    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Descuento).Value = 0.0
            End If

            'cambio el modo de seleccion
            grdItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            CalcularSubtotal()
            PagoMultiple()
            txtCodigoBarra.Focus()
            'SendKeys.Send("{TAB}")

        Catch ex As Exception
            ' MsgBox(ex.Message)
            'MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub grdItems_LostFocus(sender As Object, e As EventArgs) Handles grdItems.LostFocus
        If grdItems.Rows.Count > 0 Then
            grdItems.Rows(0).Selected = False
        End If
    End Sub

    Private Sub validar_NumerosReales2( _
   ByVal sender As Object, _
   ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = 4 Or columna = 9 Then
            'Or columna = ColumnasDelGridItems.PorcRecDesc Then

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
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItems.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtCodigoBarra.Focus()
        End If
    End Sub

    '--------------------------------Área de descuentos------------------------------------------------------------

    Private Sub chkDesc_CheckedChanged(sender As Object, e As EventArgs) Handles chkDescuentoGlobal.CheckedChanged
        rdAbsoluto.Enabled = chkDescuentoGlobal.Checked
        rdPorcentaje.Enabled = chkDescuentoGlobal.Checked
        txtDescuento.Enabled = chkDescuentoGlobal.Checked


        If chkDescuentoGlobal.Checked = False Then
            txtDescuento.Text = "0"
        Else
            'coloco los chk de pago en falso
            chkContado.Checked = False
            chkTarjetas1.Checked = False
            chkTarjetas2.Checked = False
            chkDescuentoParticular.Checked = False
            rdPorcentaje.Checked = True
            txtDescuento.Focus()
        End If

        CalcularSubtotal()

    End Sub

    Private Sub txtDescuento_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDescuento.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCodigoBarra.Focus()
        End If
    End Sub

    Private Sub txtDescuento_LostFocus(sender As Object, e As EventArgs) Handles txtDescuento.LostFocus

        If txtDescuento.Text <> "" Then
            txtDescuento.Text = FormatNumber(txtDescuento.Text, 2)
        End If

    End Sub

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtDescuento.TextChanged
        If band = 0 Then Exit Sub
        Try

            CalcularSubtotal()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub rdPorcentaje_CheckedChanged(sender As Object, e As EventArgs) Handles rdPorcentaje.CheckedChanged
        If rdPorcentaje.Checked = True Then
            txtDescuento.Text = "0"
            txtDescuento.Focus()
        End If
    End Sub

    Private Sub rdAbsoluto_CheckedChanged(sender As Object, e As EventArgs) Handles rdAbsoluto.CheckedChanged
        If rdAbsoluto.Checked = True Then
            txtDescuento.Text = "0"
            txtDescuento.Focus()
        End If
    End Sub

    Private Sub chkDescuentoParticular_CheckedChanged(sender As Object, e As EventArgs) Handles chkDescuentoParticular.CheckedChanged

        If grdItems.Rows.Count > 0 Then
            If chkDescuentoParticular.Checked Then
                chkDescuentoGlobal.Checked = False
                'coloco los chk de pago en falso
                chkContado.Checked = False
                chkTarjetas1.Checked = False
                chkTarjetas2.Checked = False
                Try
                    'voy a la grilla y cambio la forma de seleccion de los items
                    With Me.grdItems
                        .ReadOnly = False
                        'cambio el modo de seleccion
                        .SelectionMode = DataGridViewSelectionMode.CellSelect
                        'hago foco en la grilla
                        .Focus()
                        'selecciona la fila de descuento
                        .CurrentCell = .Rows(0).Cells(ColumnasDelGridItems.Descuento)
                        'permito la edicion
                        .BeginEdit(True)
                    End With

                Catch ex As Exception

                End Try

            Else

                For i As Integer = 0 To grdItems.Rows.Count - 1
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value = 0
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalOrig).Value
                Next
                CalcularSubtotal()
            End If

        End If


    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Ventas Salón 25"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
            Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))
        End If

        'Me.WindowState = FormWindowState.Maximized

        'Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 100)
        Me.grd.Size = New Size(GroupBox1.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        lblDevolucion.Tag = "1"
        dtpFECHA.Tag = "2"
        txtCliente.Tag = "3"
        lblVendedor.Tag = "4"
        txtSubtotal.Tag = "5"
        txtDescuento.Tag = "6"
        txtTotal.Tag = "7"
        'txtObservaciones.Tag = "8"
        chkDescuentoGlobal.Tag = "9"
        rdPorcentaje.Tag = "10"
        rdAbsoluto.Tag = "11"
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

    Private Sub LlenarcmbProductos()
        Dim ds_Equipos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringSEI)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ' ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT m.Codigo, (m.Nombre + ' - ' + ma.Nombre) as Producto FROM Materiales m JOIN Marcas ma ON m.idmarca = ma.codigo WHERE m.eliminado = 0 ")
            ds_Equipos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, Nombre as Producto FROM Materiales  WHERE Eliminado = 0 ")

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

        '----------------------------------CONTROLO EL TOTAL
        If txtTotalVista.Text = "" Or CDbl(txtTotalVista.Text) <= 0 Then
            MsgBox("El Total de la venta realizado no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            Exit Sub
        End If
        '---------------------------------CONTROLO DESCUENTO
        'me fijo si se plico un descuento global sin monto
        If chkDescuentoGlobal.Checked And txtDescuento.Text = "" Then
            chkDescuentoGlobal.Checked = False
        End If

        'control de Responsables y Comprobante
        If chkDevolucion.Checked = False Then
            If cmbTipoComprobante.SelectedValue.ToString = "001" And cmbCondicionIVA.SelectedValue.ToString <> "1" Then
                MsgBox("La concición de IVA o el tipo de factura seleccionada no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                Exit Sub
            Else
                If cmbTipoComprobante.SelectedValue.ToString = "006" And cmbCondicionIVA.SelectedValue.ToString = "1" Then
                    MsgBox("La concición de IVA o el tipo de factura seleccionada no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                End If
            End If
        Else
            If cmbTipoComprobante.SelectedValue.ToString = "003" And cmbCondicionIVA.SelectedValue.ToString <> "1" Then
                MsgBox("La concición de IVA o el tipo de factura seleccionada no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                Exit Sub
            Else
                If cmbTipoComprobante.SelectedValue.ToString = "008" And cmbCondicionIVA.SelectedValue.ToString = "1" Then
                    MsgBox("La concición de IVA o el tipo de factura seleccionada no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                End If
            End If
        End If

        If cmbCondicionIVA.SelectedValue = "5" And txtCuit.Text = "" Then
            txtCuit.Text = "11111111"
        End If

        '------------------------------controlo datos del cliente
        If cmbTipoComprobante.SelectedValue.ToString = "001" Or cmbTipoComprobante.SelectedValue.ToString = "003" Then
            If txtCuit.Text.Length <> 11 Then
                MsgBox("El nro de CUIT no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                txtCuit.Focus()
                Exit Sub
            End If

            If txtCliente.Text.Length > 30 Or txtCliente.Text.Length = 0 Then
                MsgBox("El Nombre del cliente no cumple con el requisito de 30 caracteres. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                txtCliente.Focus()
                Exit Sub
            End If
        End If


        '--------------------------------ME FIJO CONTROLO PAGO INDIVIDUALES
        'me fijo la forma de pago
        If chkContado.Checked = False And chkTarjetas1.Checked = False And chkTarjetas2.Checked = False Then
            MsgBox("Por favor seleccione una forma de pago.", MsgBoxStyle.Information, "Atención")
            Exit Sub
        Else
            If chkContado.Checked Then
                If txtContado.Text = "" Then
                    MsgBox("El monto de Contado no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                Else
                    If CDbl(txtContado.Text) = 0 Then
                        MsgBox("El monto de Contado no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                        Exit Sub
                    End If
                End If
            End If
            If chkTarjetas1.Checked Then
                If txtTarjetas1Importe.Text = "" Then
                    MsgBox("El monto de tarjeta 1 no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                Else
                    If CDbl(txtTarjetas1Importe.Text) = 0 Then
                        MsgBox("El monto de tarjeta 1 no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                        Exit Sub
                    End If
                End If
            End If
            If chkTarjetas2.Checked Then
                If txtTarjetas2Importe.Text = "" Then
                    MsgBox("El monto de tarjeta 2 no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                Else
                    If CDbl(txtTarjetas2Importe.Text) = 0 Then
                        MsgBox("El monto de tarjeta 2 no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                        Exit Sub
                    End If
                End If
            End If
        End If

        '--------------------------------CONTROLO EL RESTO
        'me fijo que el resto no sea mayor a cero 
        If CDbl(txtResto.Text) > 0 Then
            MsgBox("El Total de la venta realizado no es correcto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            Exit Sub
        End If
        '--------------------------------CONTROLAMOS LOS PAGOS
        Dim Contado As String = ""
        Dim Tarjeta1 As String = ""
        Dim Tarjeta2 As String = ""
        Dim sumapagofinal As Double = FormatNumber(CDbl(txtContado.Text) + CDbl(txtTarjetas1ImporteFinal.Text) + CDbl(txtTarjetas2ImporteFinal.Text), 2)
        'controlo que los txt contado y tarjetas no esten vacios
        For Each Control As Object In Me.GroupBoxPago.Controls
            If TypeOf (Control) Is CheckBox Then
                For k As Integer = 0 To 2
                    'Contado
                    If Control.Tag = 0 Then
                        If Control.checked = True Then
                            Contado = "C"
                            Exit For
                        End If
                    End If
                    'Tarjeta1
                    If Control.Tag = 1 Then
                        If Control.checked = True Then
                            Tarjeta1 = "T1"
                            Exit For
                        End If
                    End If
                    'Tarjeta2
                    If Control.Tag = 2 Then
                        If Control.checked = True Then
                            Tarjeta2 = "T2"
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        'concateno las formas de pago y la igual al txtoculto
        txtFormaPago.Text = Contado + Tarjeta1 + Tarjeta2
        If txtFormaPago.Text.Contains("C") And txtFormaPago.Text.Contains("T") Then
            If txtContado.Text <> "" And txtVuelto.Text <> "" Then
                If CDbl(txtContado.Text) = CDbl(txtVuelto.Text) Then
                    'controlo que el valor sea mayor a cero
                    If CDbl(txtContado.Text) > 0 Then
                        MsgBox("El monto contado no puede ser igual al vuelto. Por favor verifique.", MsgBoxStyle.Information, "Atención")
                        txtContado.Focus()
                        txtContado.SelectAll()
                        Exit Sub
                    End If
                Else
                    If CDbl(txtVuelto.Text) > CDbl(txtContado.Text) Then
                        MsgBox("No se puede dar vuelto si el monto contado es menor al  vuelto. Por favor verifique", MsgBoxStyle.Information, "Atención")
                        txtContado.Focus()
                        txtContado.SelectAll()
                        Exit Sub
                    End If
                End If
            End If
        End If
        If Not txtFormaPago.Text.Contains("C") Then
            If txtVuelto.Text.ToString <> "" Then
                If CDbl(txtVuelto.Text) > 0 Then
                    MsgBox("No se puede entregar vuelto con las formas de pago seleccionadas ", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                End If
            End If
            'controlo que el codigo de tarjetas no sean iguales
            If chkTarjetas1.Checked = True And chkTarjetas2.Checked = True Then
                'controlo los codigos de tarjetas no esten vacios
                If CodigoTarjeta1 = "" Then
                    MsgBox("Por favor vuelva a seleccionar una tarjeta.", MsgBoxStyle.Information, "Atención")
                    txtTarjeta1.Focus()
                    Exit Sub
                End If
                If CodigoTarjeta2 = "" Then
                    MsgBox("Por favor vuelva a seleccionar una tarjeta.", MsgBoxStyle.Information, "Atención")
                    txtTarjeta2.Focus()
                    Exit Sub
                End If
                'me fijo si los codigos de tarjetas no sean iguales
                If CodigoTarjeta1.ToString = CodigoTarjeta2.ToString Then
                    MsgBox("No se puede finalizar la venta con dos tarjetas iguales. Por favor seleccione otra.", MsgBoxStyle.Information, "Atención")
                    txtTarjeta1.Focus()
                    Exit Sub
                End If
                'si se pago con una de las tarjetas controlo que el codigo no llegue vacio
            ElseIf chkTarjetas1.Checked = True Or chkTarjetas2.Checked = False Then
                'me fijo que se pase si o si que se pasen los codigos de tarjetas 
                If chkTarjetas1.Checked = True Then
                    If CodigoTarjeta1 = "" Then
                        MsgBox("Por favor vuelva a seleccionar una tarjeta.", MsgBoxStyle.Information, "Atención")
                        txtTarjeta1.Focus()
                        Exit Sub
                    End If
                End If
                If chkTarjetas2.Checked = True Then
                    If CodigoTarjeta2 = "" Then
                        MsgBox("Por favor vuelva a seleccionar una tarjeta.", MsgBoxStyle.Information, "Atención")
                        txtTarjeta2.Focus()
                        Exit Sub
                    End If
                End If
            End If
        End If
        'controlo la suma de pagos
        If sumapagofinal < CDbl(txtTotalVista.Text) Then
            MsgBox("La forma de pago debe ser mayor o igual al total a pagar.", MsgBoxStyle.Information, "Atención")
            Exit Sub
        End If
        '----------------------------------CONTROLO DATOS DE CLIENTES
        If Not cmbCondicionIVA.Text = "CONSUMIDOR FINAL" Then
            Select Case cmbTipoComprobante.Text
                Case "CUIL"
                    If txtCuit.Text.Length <> 11 Then
                        MsgBox("El nro de CUIL no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                        txtCuit.Focus()
                        Exit Sub
                    End If
                Case "CUIT"
                    If txtCuit.Text.Length <> 11 Then
                        MsgBox("El nro de CUIT no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                        txtCuit.Focus()
                        Exit Sub
                    End If
                Case "DNI"
                    If txtCuit.Text.Length <> 7 Or txtCuit.Text.Length <> 8 Then
                        MsgBox("El nro de DNI no cumple con el requisito de 8 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                        txtCuit.Focus()
                        Exit Sub
                    End If
            End Select
        Else
            If txtCuit.Text <> "" Then
                If txtCuit.Text.Length <> 11 Or txtCuit.Text <> 8 Or txtCuit.Text.Length <> 7 Then
                    MsgBox("El nro de DNI/CUIT no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                    txtCuit.Focus()
                    Exit Sub
                End If
            End If
        End If
        '--------------------------CONTROLO GRILLA
        Dim i As Integer, j As Integer, filas As Integer ', state As Integer

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
        'verificar que no hay nada en la grilla sin datos
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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
                'idmaterial es valido?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value Is System.DBNull.Value Then
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "Falta completar el material en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
                'Descripcion del material es válida ?
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value.ToString.ToLower = "No Existe".ToLower Then
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If

            'controlo los valores de descuento
            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value Then
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value = 0
            End If

        Next i


        If chkDevolucion.Checked Then
            txtTotalVista.Text = "-" + txtTotalVista.Text
            txtSubtotalVista.Text = "-" + txtSubtotalVista.Text
            txtIVAVista.Text = "-" + txtIVAVista.Text
            txtTotalOriginal.Text = "-" + txtTotalOriginal.Text
            lblValorDescontado.Text = IIf(CDbl(lblValorDescontado.Text) > 0, "-" + lblValorDescontado.Text, lblValorDescontado.Text)
            txtContado.Text = IIf(CDbl(txtContado.Text) > 0, "-" + txtContado.Text, txtContado.Text)
            txtTarjetas1ImporteFinal.Text = IIf(CDbl(txtTarjetas1ImporteFinal.Text) > 0, "-" + txtTarjetas1ImporteFinal.Text, txtTarjetas1ImporteFinal.Text)
            txtTarjetas2ImporteFinal.Text = IIf(CDbl(txtTarjetas2ImporteFinal.Text) > 0, "-" + txtTarjetas2ImporteFinal.Text, txtTarjetas2ImporteFinal.Text)
        End If

        bolpoliticas = True

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

            sqltxt2 = "exec [spVentas_Mostrador_Det_Select_By_IdVenta] @idventa = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdItems.Rows.Add(dt.Rows(i)(0).ToString(), dt.Rows(i)(1).ToString(), dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), dt.Rows(i)(4).ToString(),
                                  dt.Rows(i)(5).ToString(), dt.Rows(i)(6).ToString(), dt.Rows(i)(7).ToString(), dt.Rows(i)(8).ToString(), dt.Rows(i)(9).ToString(),
                                  dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(), "Eliminar")
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbComprobantes()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            If chkDevolucion.Checked = False Then
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo,Descripcion FROM Comprobantes WHERE Habilitado = 1 and not Descripcion like '%NOTAS%' ORDER BY Descripcion")
            Else
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo,Descripcion FROM Comprobantes WHERE Habilitado = 1 and Descripcion like '%NOTAS%' ORDER BY Descripcion")
            End If
            ds.Dispose()

            With cmbTipoComprobante
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
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

    Private Sub LlenarcmbCondicionIVA()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo,Descripcion FROM Responsables WHERE Habilitado = 1  ORDER BY Descripcion")
            ds.Dispose()

            With cmbCondicionIVA
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
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

    Private Sub CalcularSubtotal()

        Dim i As Integer
        Dim Subtotal As Double = 0
        Dim Total As Double = 0
        Dim Iva As Double = 0
        Dim DescuentoItems As Double = 0
        Dim PorcenCal As Double = 0

        For i = 0 To grdItems.Rows.Count - 1
            Total = Total + grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalOrig).Value
            DescuentoItems = DescuentoItems + IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value)
        Next

        txtTotalOriginal.Text = FormatNumber(Total, 2)


        Dim Descuento As Double
        If txtDescuento.Text = "" Or txtDescuento.Text = "0" Then
            Descuento = "0"
        Else
            Descuento = CDbl(txtDescuento.Text)
            If rdAbsoluto.Checked Then
                PorcenCal = ((Descuento * 100) / Total)
            Else
                PorcenCal = Descuento
            End If
        End If

        If PorcenCal > DescuentoMaximo Then
            Descuento = 0
            Util.MsgStatus(Status1, "El descuento aplicado es mayor al descuento máximo aplicable(" + DescuentoMaximo.ToString + " %)", My.Resources.Resources.alert.ToBitmap)
        Else
            Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        End If

        If rdAbsoluto.Checked = True Then
            lblValorDescontado.Text = Descuento
            Total = Total - Descuento
        Else
            Dim ValorDescuento As Double
            ValorDescuento = Total * (Descuento / 100)
            lblValorDescontado.Text = ValorDescuento
            Total = Total - ValorDescuento
        End If

        If DescuentoItems > 0 Or chkDescuentoParticular.Checked Then
            lblValorDescontado.Text = DescuentoItems
            Total = Total - DescuentoItems
        End If

        lblValorDescontado.Text = FormatNumber(lblValorDescontado.Text, 2)

        'muestro los valores finales
        If cmbTipoComprobante.Text.Contains("FACTURAS A") Then
            'calculo los valores que voy a mostrar para facturas A
            txtTotal.Text = FormatNumber(Total, 2)
            Subtotal = Total / (1 + MDIPrincipal.iva / 100)
            txtSubtotal.Text = FormatNumber(Subtotal, 2)
            Iva = Subtotal * (MDIPrincipal.iva / 100)
            txtIVA.Text = FormatNumber(Iva, 2)
        Else
            'Coloco los valores finales
            txtSubtotal.Text = FormatNumber(Total, 2)
            txtIVA.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(Total, 2)
        End If
        'me fijo si hay un ck de pago activado
        ActivarPago()

    End Sub

    Private Sub Contar_Filas()

        lblCantidadFilas.Text = grdItems.RowCount

    End Sub

    Private Sub Establecer_CodiReg()
        Try
            Dim culture As New System.Globalization.CultureInfo("es-AR")
            culture.NumberFormat.NumberDecimalSeparator = ","
            culture.NumberFormat.NumberGroupSeparator = "."
            'culture.NumberFormat.CurrencyDecimalSeparator = ","
            'culture.NumberFormat.CurrencyGroupSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture = culture
        Catch ex As Exception
            MsgBox("Se produjo un inconveniente al modificar la configuración regional." & vbCrLf & "Por avise al Dpto. de Sistemas.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub LimpiarDescuentos()
        contadordescuento = 0
        chkDescuentoGlobal.Checked = False
        chkDescuentoParticular.Checked = False
        lblValorDescontado.Text = "0,00"
    End Sub

    Private Sub OrdenarFilas()
        Try
            'acomodo los datos de la grilla
            'grdItems.Sort(grdItems.Columns(1), System.ComponentModel.ListSortDirection.Descending)
            grdItems.Sort(grdItems.Columns(0), System.ComponentModel.ListSortDirection.Descending)
            grdItems.ClearSelection()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ContarCaracteres(ByVal Componente As TextBox, ByVal EsNomCliente As Boolean, ByVal TopeCaracter As Integer)
        Dim Caracter As Integer = 0
        Caracter = TopeCaracter - Componente.Text.Length
        If Caracter < 0 And EsNomCliente = False Then
            lblContadorCuit.Text = "Error"
            Exit Sub
        End If
        If EsNomCliente Then
            lblContadorCliente.Text = Caracter.ToString
        Else
            lblContadorCuit.Text = Caracter.ToString
        End If
    End Sub

    Public Sub Imprimir(ByVal NroComprobante As String, ByVal tipocomprobante As String)

        nbreformreportes = "Comprobante"

        Cursor = Cursors.WaitCursor

        Dim Rpt As New frmReportes
        Dim Rpt1 As New frmReportes

        'Rpt1.NombreArchivoPDF = PTOVTA.ToString + "-" + NroComprobante.PadLeft(10, "0") + " - Comprobante Duplicado"
        'Rpt1.MailDestinatario = ""
        'Rpt1.TieneCodigoBarra = True

        'Rpt1.Factura_App(NroComprobante, Rpt1, 0, My.Application.Info.AssemblyName.ToString, "DUPLICADO", tipocomprobante, Empresa)

        'Rpt.NombreArchivoPDF = PTOVTA.ToString + "-" + NroComprobante.PadLeft(10, "0") + " - Comprobante Original"
        Rpt.NombreArchivoPDF = ValorFac + " - Comprobante Original"
        Rpt.MailDestinatario = ""
        Rpt.TieneCodigoBarra = True

        Rpt.Factura_App(NroComprobante, Rpt, 0, My.Application.Info.AssemblyName.ToString, "ORIGINAL", tipocomprobante, Empresa, False, "", True)

        Cursor = Cursors.Default

    End Sub

    Private Sub BuscarProducto(ByVal desdeID As Boolean, ByVal codigo As String)

        'Dim Codigo As String
        Dim peso As Double = 0
        CodBarra_Activado = True
        Dim BalanzaMayo As Boolean = False
        Dim Identificador As String
        'Producto es pesable
        If Codigo.Substring(0, 2) = "20" And Codigo.Length = 13 Then
            '------------------------------------------------
            'me fijo que bit trae 
            Identificador = Codigo.Substring(6, 1)

            If Identificador = "2" Then
                BalanzaMayo = True
            End If
            PesoProducto = Codigo.Substring(7, 5)
            '----------------------------------------------
            'calculo el peso
            'PesoProducto = Codigo.Substring(6, 6)
            peso = CDbl(PesoProducto) / 1000
            'paso el codigo del producto
            Codigo = Codigo.Substring(2, 4)
            ProductoPesable = True
        Else
            ProductoPesable = False
        End If


        Dim sqlstring As String = ""

        If desdeID Then
            sqlstring = "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,m.Codigo, (m.Nombre + ' - ' + mar.Nombre) as Producto " & _
                               " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                               "JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                               " where m.codigo = " & codigo & " AND s.idalmacen = " & IDAlmacenSalon
        Else
            sqlstring = "SELECT m.IdUnidad, s.qty, m.Codigo, m.Nombre, s.IDAlmacen, m.Minimo, PrecioCosto, PrecioMayorista, PrecioLista3, PrecioLista4, Mar.Nombre, U.Nombre,m.IdMarca,m.PrecioCompra,m.Codigo, (m.Nombre + ' - ' + mar.Nombre) as Producto " & _
                                " FROM Materiales m JOIN Stock s ON s.idmaterial = m.Codigo " & _
                                "JOIN Marcas Mar ON Mar.codigo = m.IdMarca JOIN Unidades U ON U.codigo = m.idunidad" & _
                                " where m.CodigoBarra = '" & Codigo & "' AND s.idalmacen = " & IDAlmacenSalon
        End If

        ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
        ds_Empresa.Dispose()

        If ds_Empresa.Tables(0).Rows.Count = 0 Then
            MsgBox("El código elegido no está asociado a un producto. Por favor verifique.", MsgBoxStyle.Information)
            Exit Sub
        End If
        'MsgBox(ds_Empresa.Tables(0).Rows(0)(2).ToString)
        cmbProducto.SelectedValue = ds_Empresa.Tables(0).Rows(0)(2).ToString
        cmbProducto.Text = ds_Empresa.Tables(0).Rows(0)(15).ToString
        If txtIDPrecioLista.Text = 3 Or BalanzaMayo = True Then
            txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(6), 2)
        ElseIf txtIDPrecioLista.Text = 4 Then
            txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(7), 2)
        ElseIf txtIDPrecioLista.Text = 5 Then
            txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(8), 2)
        ElseIf txtIDPrecioLista.Text = 10 Then
            txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(13), 2)
        ElseIf DescLista.Contains("NORTE") Then
            txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(13) * ValorNorte_cambio, 2)
        Else
            txtPrecio.Text = FormatNumber(ds_Empresa.Tables(0).Rows(0)(9), 2)
        End If
        txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(0)
        unidad_unitario = txtIdUnidad.Text
        'lblStock.Text = ds_Empresa.Tables(0).Rows(0)(1)
        'stock_unitario = lblStock.Text
        idproducto_unitario = ds_Empresa.Tables(0).Rows(0)(2)
        producto_unitario = ds_Empresa.Tables(0).Rows(0)(3)
        almacen_unitario = ds_Empresa.Tables(0).Rows(0)(4)
        'COMPARO EL STOCK QUE HAY DEL PRODUCTO PARA SABER SI ESTA POR DEBAJO DEL MINIMO 
        'If (ds_Empresa.Tables(0).Rows(0)(5)).ToString <> "" Then
        '    If CDbl(stock_unitario) > CDbl(ds_Empresa.Tables(0).Rows(0)(5)) Then
        '        lblStock.BackColor = Color.Green
        '    Else
        '        lblStock.BackColor = Color.Red
        '    End If
        'End If
        'txtIdUnidad.Text = ds_Empresa.Tables(0).Rows(0)(11)


        'txtCantidad.Focus()
        'SendKeys.Send("{TAB}")

        If ProductoPesable Then
            lblPeso.Text = "Peso(Kg)*"
            txtPeso.Text = peso.ToString
        Else
            lblPeso.Text = "Peso"
            txtPeso.Text = peso.ToString
        End If
    End Sub


#End Region

#Region "   Funciones"

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

    Private Function fila_vacia(ByVal i) As Boolean
        If (grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value Is Nothing) _
                    And (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is System.DBNull.Value Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value Is Nothing) Then
            fila_vacia = True
        Else
            fila_vacia = False
        End If
    End Function

    Private Function ActualizarBalanzas() As Integer
        'Dim path As String
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Dim param_res As New SqlClient.SqlParameter
        param_res.ParameterName = "@res"
        param_res.SqlDbType = SqlDbType.Int
        param_res.Value = DBNull.Value
        param_res.Direction = ParameterDirection.InputOutput

        Try

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBalanzas_Exportar_Precios_Mayo", param_res)
            ActualizarBalanzas = param_res.Value


            'MsgBox(ActualizarBalanzas.ToString)
        Catch ex As Exception

            ActualizarBalanzas = -3
            Exit Function
        End Try

        Try

            ' path = TruncarUltimaCarpeta(Application.ExecutablePath.ToString)
            'MsgBox(path & "\arabupdate.exe")
            Shell("C:\Program Files (x86)\Qendra\Qendra.exe -i", AppWinStyle.NormalFocus)

        Catch ex As Exception

            ActualizarBalanzas = -4
            Exit Function
        End Try

    End Function

    Private Function ToSecureString(ByVal source As String) As System.Security.SecureString
        If String.IsNullOrEmpty(source) Then
            Return Nothing
        End If
        Dim result = New System.Security.SecureString
        For Each c As Char In source
            result.AppendChar(c)
        Next
        Return result
    End Function

    Private Function EjecutarQendraLocal() As Integer
        Dim res As Integer = 0
        Try

            'este comando solo funciona si lo ejecuto desde el .exe del proyecto
            'Shell("C:\Program Files (x86)\Qendra\Qendra_Importador.Lnk", AppWinStyle.NormalFocus)

            Dim startInfo As System.Diagnostics.ProcessStartInfo
            Dim pStart As New System.Diagnostics.Process
            ' Cambiamos el cursor por el de carga
            Me.Cursor = Cursors.WaitCursor
            '-----------------------------------------------------------------------------------------------
            'esta configuracion es para decir si voy a ejecutar la aplicacion con permisos de administrador
            'pStart.StartInfo.UseShellExecute = False
            'pStart.StartInfo.UserName = "VENTAS1"
            'pStart.StartInfo.Password = ToSecureString("123456")
            '-----------------------------------------------------------------------------------------------
            'le paso que proceso deseo ejecutar
            'startInfo = New System.Diagnostics.ProcessStartInfo("C:\Program Files (x86)\Qendra\Qendra.exe", "-i")
            startInfo = New System.Diagnostics.ProcessStartInfo("C:\Program Files (x86)\Qendra\Qendra_ImportadorAcc.lnk")
            'startInfo = New System.Diagnostics.ProcessStartInfo("C:\Program Files\VideoLAN\VLC\vlc.exe")
            'le digo que lo ejecute oculto(Hidden) o minimizado (con algunas aplicaicones no funciona, ejemplo notepad++) 
            startInfo.WindowStyle = ProcessWindowStyle.Normal
            'inicio el proceso
            pStart.StartInfo = startInfo
            'abro el ejecutable
            pStart.Start()

            'coloco una cantidad de tiempo (10 min) de espera hasta que cierre el proceso
            pStart.WaitForExit(600000)
            'pasado los el tiempo cierra el proceso
            If Not pStart.HasExited Then
                pStart.CloseMainWindow()
                pStart.Kill()
            End If

            Me.Cursor = Cursors.Arrow

            EjecutarQendraLocal = 1

        Catch ex As Exception
            MsgBox(ex.Message)
            EjecutarQendraLocal = -1
            Me.Cursor = Cursors.Arrow
            Exit Function
        End Try
    End Function

    Private Function BuscarValores_Facturados() As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_total As Data.DataSet

        Dim Compras As Double = 0
        Dim Tolerancia As Double = 0
        Dim TotalCompa As Double = 0

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        'Obtengo el total facturado y lo almaceno en una variable
        ds_total = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT isnull(sum(total),0) FROM ventas_salon WHERE Eliminado = 0 AND isnull(cae,'') <> '' AND month(Fecha) = month(getdate())  AND year(Fecha) =  year(getdate())")
        total_facturado = ds_total.Tables(0).Rows(0).Item(0).ToString
        ds_total = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT isnull(sum(total),0) FROM Gastos WHERE Eliminado = 0 AND month(FechaGasto) = month(getdate())  AND year(FechaGasto) =  year(getdate())")
        Compras = ds_total.Tables(0).Rows(0).Item(0).ToString
        ds_total.Dispose()
        'Calculo la tolerancia
        Tolerancia = 1 + (TopePorcenFac / 100)
        TotalCompa = Compras * Tolerancia

        If total_facturado < TotalCompa Then
            BuscarValores_Facturados = True
        Else
            BuscarValores_Facturados = False
        End If

    End Function

#End Region

#Region "   Botones"

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer
        Dim resTarj As Boolean = False
        Dim resbool As Boolean = False
        Dim Imprimir_Factura As Boolean = False
        'Defino las variables que van a usarse en la invocacion de la funcion GenerarFE
        'Dim tipo_comp, pto_vent, tipo_doc, num_doc, importe_iva, subtot, tot, concep

        If bolModo = False Then
            MsgBox("No se puede modificar una venta realizada", MsgBoxStyle.Critical, "Control de Errores")
            'Imprimir_Factura = True
            Exit Sub
        End If

        If grdItems.RowCount = 0 Then
            Util.MsgStatus(Status1, "Debe ingresar al menos un Producto.", My.Resources.Resources.stop_error.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar al menos un Producto.", My.Resources.Resources.stop_error.ToBitmap, True)
            'Imprimir_Factura = True
            Exit Sub
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                TimerVentas.Enabled = False
                Imprimir_Factura = BuscarValores_Facturados()
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = RealizarModificar_Venta()
                Select Case res
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Cancelar_Tran()
                        TimerVentas.Enabled = True
                        'Imprimir_Factura = True
                        txtCodigoBarra.Focus()
                        Exit Sub
                    Case Is <= 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        TimerVentas.Enabled = True
                        'Imprimir_Factura = True
                        Cancelar_Tran()
                        txtCodigoBarra.Focus()
                        Exit Sub
                    Case Else
                        'me fijo si realizo el pago con tarjeta
                        If chkTarjetas1.Checked Or chkTarjetas2.Checked Then
                            'me fijo si en los montos hay valores mayores que cero
                            If CDbl(IIf(txtTarjetas1ImporteFinal.Text = "", "0", txtTarjetas1ImporteFinal.Text)) > 0 Or CDbl(IIf(txtTarjetas2ImporteFinal.Text = "", "0", txtTarjetas2ImporteFinal.Text)) > 0 Then
                                res = RealizarModificar_VentaTarjeta()
                                Select Case res
                                    Case -1
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro de Tarjeta.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro de Tarjeta.", My.Resources.Resources.stop_error.ToBitmap, True)
                                        TimerVentas.Enabled = True
                                        'Imprimir_Factura = True
                                        Cancelar_Tran()
                                        txtCodigoBarra.Focus()
                                        Exit Sub
                                    Case Is <= 0
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro de Tarjeta.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo agregar el registro de Tarjeta.", My.Resources.Resources.stop_error.ToBitmap, True)
                                        TimerVentas.Enabled = True
                                        'Imprimir_Factura = True
                                        Cancelar_Tran()
                                        txtCodigoBarra.Focus()
                                        Exit Sub
                                    Case Else
                                        resTarj = True
                                End Select
                            End If
                        End If
                        res = RealizarModificar_VentaDetalle()
                        Select Case res
                            Case Is <= 0
                                Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap)
                                Util.MsgStatus(Status1, "No se pueden insertar los items.", My.Resources.Resources.stop_error.ToBitmap, True)
                                TimerVentas.Enabled = True
                                'Imprimir_Factura = True
                                Cancelar_Tran()
                                txtCodigoBarra.Focus()
                                Exit Sub
                            Case Else
                                'aca controlo si debo imprimir
                                If Imprimir_Factura Or resTarj Then
                                    chkConexion.Checked = ConexionAfip(saveTA, saveTOKEN, saveSING)
                                    If chkConexion.Checked Then
                                        'debo comprobar el caso de que eligan tarjeta de credito o tarjeta de debito
                                        resbool = GenerarFE(sender, e, CInt(cmbTipoComprobante.SelectedValue), CInt(PTOVTA), TipoDoc, txtCuit.Text, txtIVAVista.Text, txtSubtotalVista.Text, txtTotalVista.Text, 1, txtID.Text)
                                        If resbool = False Then
                                            Util.MsgStatus(Status1, "No se pudo generar la factura electrónica.", My.Resources.Resources.stop_error.ToBitmap)
                                            Util.MsgStatus(Status1, "No se pudo generar la factura electrónica.", My.Resources.Resources.stop_error.ToBitmap, True)
                                            TimerVentas.Enabled = True
                                            'Imprimir_Factura = True
                                            Cancelar_Tran()
                                            txtCodigoBarra.Focus()
                                            Exit Sub
                                        Else
                                            If resTarj Then
                                                Dim Pop As New frmVentaSalon_Pop
                                                Pop.ShowDialog()
                                            End If
                                            Imprimir(ValorFac, cmbTipoComprobante.Text)
                                            ValorCae = ""
                                            ValorFac = ""
                                            ValorVen = ""
                                        End If
                                    Else
                                        Util.MsgStatus(Status1, "Se perdió la conexión con Servidor de AFIP. Por favor intente más tarde", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "Se perdió la conexión con Servidor de AFIP. Por favor intente más tarde", My.Resources.Resources.stop_error.ToBitmap, True)
                                        TimerVentas.Enabled = True
                                        'Imprimir_Factura = True
                                        Cancelar_Tran()
                                        txtCodigoBarra.Focus()
                                        Exit Sub
                                    End If
                                Else
                                    'cierro la transaccion
                                    Cerrar_Tran()
                                End If
                                'Imprimir_Factura = False
                                bolModo = False
                                PrepararBotones()
                                MostrarLogin = False
                                btnNuevo_Click(sender, e)
                                'Util.MsgStatus(Status1, "Se ha realizado con éxito la venta número " & lblCodigo.Text & ".", My.Resources.Resources.ok.ToBitmap)
                                Util.MsgStatus(Status1, "Se ha realizado con éxito la venta.", My.Resources.Resources.ok.ToBitmap)

                                Dim sqlstring As String = "SELECT Actualizado FROM Balanzas_Notificaciones "
                                Dim ds_Balanza As Data.DataSet
                                ds_Balanza = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                                ds_Balanza.Dispose()
                                If ds_Balanza.Tables(0).Rows(0).Item(0) = 0 Then
                                    If MessageBox.Show("La actualización de balanzas está pendiente, desea realizarlo ahora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                        btnActualizarBalanzas_Click(sender, e)
                                    Else
                                        Exit Sub
                                    End If
                                End If

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
        txtCodigoBarra.Focus()
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        band = 0
        bolModo = True
        Dim estadoconexion As Boolean = chkConexion.Checked
        'Imprimir_Factura = True

        If MostrarLogin = True Then
            frmLoginVendedor.ShowDialog()
            lblVendedor.Text = NombreVendedor
            lblNumVendedor.Text = IdVendedor
        End If

        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkDevolucion.Checked = False

        Util.LimpiarTextBox(Me.Controls)
        LimpiarDescuentos()
        grdItems.Rows.Clear()

        band = 1

        chkConexion.Checked = estadoconexion
        btnEliminar.Enabled = True
        btnNuevo.Enabled = True
        'dtpFECHA.Value = Date.Today
        cmbCondicionIVA.Enabled = True
        cmbTipoComprobante.Enabled = True
        cmbProducto.SelectedIndex = 0
        cmbTipoComprobante.SelectedValue = "006"
        cmbCondicionIVA.SelectedValue = "5"
        TipoDoc = 96
        txtCuit.AccessibleName = ""
        txtCuit.ReadOnly = False
        txtCliente.ReadOnly = False
        txtCliente.AccessibleName = ""
        txtClienteDireccion.ReadOnly = False
        cmbProducto.Text = ""
        txtIDPrecioLista.Text = "2"
        txtIdCliente.Text = "1"
        'txtObservaciones.Enabled = True
        'chkPrecioMayorista.Checked = False
        txtSubtotal.Text = "0,00"
        txtIVA.Text = "0,00"
        txtTotal.Text = "0,00"
        txtTotalOriginal.Text = "0,00"
        Contar_Filas()

        '' txtObservaciones.Focus()
        'SendKeys.Send("{TAB}")

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click


        chkDevolucion.Checked = True

        'Dim res As Integer

        ' If chkDevolucion.Checked = True Then

        '    If MessageBox.Show("Esta acción anulará la venta (no podrá usar el mismo nro de remito en el sistema) " + vbCrLf + _
        '                   "¿Está seguro que desea Anular?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        '        Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        '        res = EliminarRegistro()
        '        Select Case res
        '            Case Is <= 0
        '                Cancelar_Tran()
        '                Util.MsgStatus(Status1, "No se pudo anular el Remito de Ventas.", My.Resources.stop_error.ToBitmap)
        '                Util.MsgStatus(Status1, "No se pudo anular el Remito de Ventas.", My.Resources.stop_error.ToBitmap, True)
        '            Case Else
        '                Cerrar_Tran()
        '                PrepararBotones()
        '                btnActualizar_Click(sender, e)
        '                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap, True, True)
        '        End Select
        '    Else
        '        Util.MsgStatus(Status1, "Acción de eliminar cancelada.", My.Resources.stop_error.ToBitmap, True)
        '    End If
        'Else
        '    Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        ' End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim Cliente As String

        Dim consulta As String = "select '' as Nombre UNION select Nombre from clientes C join Ventas_Salon v ON v.IdCliente = c.id where c.eliminado = 0 order by nombre asc"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        'paramreporte.AgregarParametros("Cta Cte :", "STRING", "", False, "", consulta, Cnn)


        paramreporte.ShowDialog()

        nbreformreportes = "Ventas"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString

            rpt.DeudaClientes_App(Inicial, Final, Cliente, "", rpt, My.Application.Info.AssemblyName.ToString, False)
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click

        'Imprimir_Factura = False
        'btnGuardar_Click(sender, e)

        'Dim connection As SqlClient.SqlConnection = Nothing
        'Dim ds_Update As Data.DataSet

        'If MessageBox.Show("Está por activar nuevamente la venta del cliente: " & grd.CurrentRow.Cells(3).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    llenandoCombo = False
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        'Try

        '    ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Ventas SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
        '    ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Ventas_Det SET Eliminado = 0 WHERE idVenta = " & grd.CurrentRow.Cells(0).Value)

        '    ds_Update.Dispose()

        '    SQL = "exec spVentas_Mostrador_Select_ALL @Eliminado = 1"


        '    LlenarGrilla()


        '    If grd.RowCount = 0 Then
        '        btnActivar.Enabled = False
        '        'limpia la grilla de detalles
        '        grdItems.Rows.Clear()
        '    End If

        '    Util.MsgStatus(Status1, "La venta se activó correctamente.", My.Resources.ok.ToBitmap)




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

    Public Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Application.Exit()
    End Sub

    Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        'lblTipoOperacion.Visible = False
        'chkPrecioMayorista.Checked = False
        'chkPrecioMayorista.Enabled = False
        Util.LimpiarTextBox(Me.Controls)
        limpiarGropPago()
        MostrarLogin = False
        btnNuevo_Click(sender, e)
        txtCodigoBarra.Focus()
        'If grd.Rows.Count > 0 Then
        '    grd.Rows(0).Selected = True
        '    LlenarGridItems()
        'End If

    End Sub

    Private Sub btnModCant_Click(sender As Object, e As EventArgs) Handles btnModCant.Click

        If grdItems.Rows.Count > 0 Then
            'Me fijo si el ultimo item agregado no sea un producto pesable
            If grdItems.Rows(0).Cells(ColumnasDelGridItems.ProductoPesable).Value = False Then
                Try
                    'voy a la grilla y cambio la forma de seleccion de los items
                    With Me.grdItems
                        .ReadOnly = False
                        'cambio el modo de seleccion
                        .SelectionMode = DataGridViewSelectionMode.CellSelect
                        'hago foco en la grilla
                        .Focus()
                        'selecciona la fila de cantidad
                        .CurrentCell = .Rows(0).Cells(ColumnasDelGridItems.Cantidad)
                        'permito la edicion
                        .BeginEdit(True)
                    End With

                Catch ex As Exception

                End Try
            Else
                MsgBox("La cantidad del último producto agregado no puede ser modificado (Producto Pesable). Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            End If
        Else
            txtCodigoBarra.Focus()
        End If


    End Sub

    Private Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        Dim C As New frmBusquedaCliente
        C.ShowDialog()
        CalcularSubtotal()
        PagoMultiple()
        txtCodigoBarra.Focus()
    End Sub

    Private Sub bntVerFactura_Click(sender As Object, e As EventArgs) Handles bntVerFactura.Click
        Dim V As New frmBusquedaFacturas
        V.ShowDialog()
        txtCodigoBarra.Focus()
    End Sub

    Private Sub btnActualizarBalanzas_Click(sender As Object, e As EventArgs) Handles btnSincronizar.Click
        Dim res As Integer

        If MessageBox.Show("Esta seguro que desea actualizar las balanzas ahora?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        PicSincro.Image = My.Resources.Sincro
        res = EjecutarQendraLocal()

        Select Case res
            Case -1
                Util.MsgStatus(Status1, "Error al ejecutado Qendra importador. Por favor verifique datos", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Error al ejecutado Qendra importador. Por favor verifique datos", My.Resources.Resources.stop_error.ToBitmap, True)
                PicSincro.Image = My.Resources.SincroFALLA
                Exit Sub
            Case 1
                Util.MsgStatus(Status1, "Se ha ejecutado Qendra importador VENTAS2. Por favor verifique datos actualizados en balanzas", My.Resources.Resources.ok.ToBitmap)
                Util.MsgStatus(Status1, "Se ha ejecutado Qendra importador VENTAS2. Por favor verifique datos actualizados en balanzas", My.Resources.Resources.ok.ToBitmap)

                Try
                    TimerVentas.Enabled = False
                    Dim connection As SqlClient.SqlConnection = Nothing
                    Try
                        connection = SqlHelper.GetConnection(ConnStringSEI.ToString)
                    Catch ex As Exception
                        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    Dim sqlstring As String = "UPDATE Balanzas_Notificaciones SET Actualizado = 1, Fecha = GETDATE()"
                    Dim ds_Balanza As Data.DataSet
                    ds_Balanza = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
                    ds_Balanza.Dispose()

                    PicSincro.Image = My.Resources.SincroOK
                    TimerVentas.Enabled = True
                Catch ex As Exception
                    TimerVentas.Enabled = True
                    PicSincro.Image = My.Resources.SincroFALLA
                End Try

        End Select

        'res = ActualizarBalanzas()
        'Select Case res
        '    Case -1
        '        Util.MsgStatus(Status1, "Se produjo un error en el control de parametros de la consulta para actualizar las balanzas.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Se produjo un error en el control de parametros de la consulta para actualizar las balanzas.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        Exit Sub
        '    Case -2
        '        Util.MsgStatus(Status1, "Se produjo una excepción al realizar la consulta para actualizar las balanzas.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Se produjo una excepción al realizar la consulta para actualizar las balanzas.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        Exit Sub
        '    Case -3
        '        Util.MsgStatus(Status1, "Se produjo una excepción al llamar a la consulta para actualizar las balanzas.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "Se produjo una excepción al llamar a la consulta para actualizar las balanzas.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        Exit Sub
        '    Case -4
        '        Util.MsgStatus(Status1, "No se pudo ejecutar la aplicación externa(Qendra) para exportar novedades.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "No se pudo ejecutar la aplicación externa(Qendra) para exportar novedades.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        Exit Sub
        '    Case 0
        '        Util.MsgStatus(Status1, "No se pudo realizar la consulta requerida.", My.Resources.Resources.stop_error.ToBitmap)
        '        Util.MsgStatus(Status1, "No se pudo realizar la consulta requerida.", My.Resources.Resources.stop_error.ToBitmap, True)
        '        Exit Sub
        '    Case Else
        '        Util.MsgStatus(Status1, "Se han actualizado exitosamente las balanzas.", My.Resources.Resources.ok.ToBitmap)
        '        Util.MsgStatus(Status1, "Se han actualizado exitosamente las balanzas.", My.Resources.Resources.ok.ToBitmap)
        'End Select
    End Sub

#End Region

#Region "GroupBoxPago"

#Region "Pago - Eventos"

    Private Sub chkContado_CheckedChanged(sender As Object, e As EventArgs) Handles chkContado.CheckedChanged

        'If frmPuntoVenta_ARAB.desde_Atajo = False Then
        txtContado.Enabled = chkContado.Checked
        ActivarPago()
        If chkContado.Checked = True Then
            txtContado.Focus()
            'pongo en negrita las letras del chk
            chkContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
            'muestro candado Abierto
            lblCandadoContado.Image = My.Resources.CandadoAbierto
            'muestro el txt de vuelto y lbl
            txtVuelto.Visible = True
            lblVuelto.Visible = True

        Else
            'pongo en regular las letras del chk
            chkContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
            'muestro candado cerrado
            lblCandadoContado.Image = My.Resources.CandadoCerrado
            txtContado.Text = "0.00"
            txtContado.Enabled = False
            chkenfalso = True
        End If
        PagoMultiple()
        Formas_Pagos()



    End Sub

    Private Sub lblCandadoContado_Click(sender As Object, e As EventArgs) Handles lblCandadoContado.Click
        SendKeys.Send("{F6}")
    End Sub

    Private Sub chkTarjetas1_CheckedChanged(sender As Object, e As EventArgs) Handles chkTarjetas1.CheckedChanged

        'habilito el combo tarjetas con el checkbox
        txtTarjetas1Importe.Enabled = chkTarjetas1.Checked
        txtTarjetas1ImporteFinal.Enabled = chkTarjetas1.Checked
        txtMontoRecarga1.Enabled = chkTarjetas1.Checked
        txtTarjeta1.Enabled = chkTarjetas1.Checked
        'Condicion para habilitar el boton de tarjetas
        ActivarPago()
        If chkTarjetas1.Checked = True Then
            'deshabilito boton de imprimir sin factura
            btnActivar.Enabled = False
            'txtTarjetas1Importe.Focus()
            'pongo en negrita las letras del chk
            chkTarjetas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
            'muestro candado Abierto
            lblCandadoTarjetas1.Image = My.Resources.CandadoAbierto
            txtTarjeta1.Focus()
            'verifico que se haya cargado al menos un producto
            If txtTarjeta1.Text <> "" Then
                Formas_Pagos()
                PagoMultiple()
            End If
            'Checked en falso
        Else
            'habilito el boton de imprimir sin factura pero si no esta tarjeta otra tarjeta habilitada
            If chkTarjetas2.Checked = False Then
                btnActivar.Enabled = True
            End If
            'pongo en regular las letras del chk
            chkTarjetas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
            'muestro candado cerrado
            lblCandadoTarjetas1.Image = My.Resources.CandadoCerrado
            'limpio el txt de tarjetas
            txtTarjeta1.Text = ""
            'limpio el txt
            txtTarjetas1Importe.Text = "0.00"
            txtTarjetas1ImporteFinal.Text = "0.00"
            txtMontoRecarga1.Text = "0.00"
            'limpio el txt de tarjetas
            txtTarjeta1.Text = ""
            porcen1 = 0.0
            cuotas1 = 0.0
            chkenfalso = True
            PagoMultiple()
            Formas_Pagos()
        End If
    End Sub

    Private Sub chkTarjetas1_KeyDown(sender As Object, e As KeyEventArgs) Handles chkTarjetas1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTarjeta1.Focus()
        End If
    End Sub

    Private Sub chkTarjetas1_LostFocus(sender As Object, e As EventArgs) Handles chkTarjetas1.LostFocus
        If chkTarjetas1.Checked = True Then
            'pongo en negrita las letras del chk
            chkTarjetas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
        Else
            'si me voy del chk dejo las letras como regular
            chkTarjetas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
        End If
    End Sub

    Private Sub chkTarjetas2_CheckedChanged(sender As Object, e As EventArgs) Handles chkTarjetas2.CheckedChanged
        txtTarjetas2Importe.Enabled = chkTarjetas2.Checked
        txtTarjetas2ImporteFinal.Enabled = chkTarjetas2.Checked
        txtMontoRecarga2.Enabled = chkTarjetas2.Checked
        txtTarjeta2.Enabled = chkTarjetas2.Checked
        ActivarPago()
        If chkTarjetas2.Checked = True Then
            'deshabilito boton de imprimir sin factura
            btnActivar.Enabled = False
            'frmPuntoVenta_ARAB.txtDescuentoGlobal.Text = "0.00"
            'txtTarjetas2Importe.Focus()
            'pongo en negrita las letras del chk
            chkTarjetas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
            'muestro candado Abierto
            lblCandadoTarjetas1.Image = My.Resources.CandadoAbierto
            txtTarjeta2.Focus()
            If txtTarjeta2.Text <> "" Then
                PagoMultiple()
                Formas_Pagos()
            End If
            'Checked en falso
        Else
            'habilito el boton de imprimir sin factura pero si no esta tarjeta otra tarjeta habilitada
            If chkTarjetas1.Checked = False Then
                btnActivar.Enabled = True
            End If
            'pongo en regular las letras del chk
            chkTarjetas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
            'muestro candado cerrado
            lblCandadoTarjetas2.Image = My.Resources.CandadoCerrado
            'limpio el txt
            txtTarjetas2Importe.Text = "0.00"
            txtTarjetas2ImporteFinal.Text = "0.00"
            txtMontoRecarga2.Text = "0.00"
            'limpio el txt de tarjetas
            txtTarjeta2.Text = ""
            'pongo los valores de cuotas y porcentajerecar en vacios
            porcen2 = 0.0
            cuotas2 = 0.0
            chkenfalso = True
            PagoMultiple()
            Formas_Pagos()
        End If

    End Sub

    Private Sub chkTarjetas2_KeyDown(sender As Object, e As KeyEventArgs) Handles chkTarjetas2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTarjeta2.Focus()
        End If
    End Sub

    Private Sub chkTarjetas2_LostFocus(sender As Object, e As EventArgs) Handles chkTarjetas2.LostFocus
        If chkTarjetas2.Checked = True Then
            'pongo en negrita las letras del chk
            chkTarjetas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
        Else
            'si me voy del chk dejo las letras como regular
            chkTarjetas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
        End If
    End Sub

    Private Sub txtContado_KeyUp(sender As Object, e As KeyEventArgs) Handles txtContado.KeyUp
        If chkContado.Checked = True Then 'And frmPuntoVenta_ARAB.chkSeña.Checked = True And frmPuntoVenta_ARAB.chkSeña.ForeColor = Color.Green Then
            PagoMultiple()
        End If
    End Sub

    Private Sub txtTarjetas1Importe_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTarjetas1Importe.KeyDown
        If e.KeyCode = Keys.Up Then
            txtTarjeta1.Focus()
            txtTarjeta1.SelectAll()
        End If
    End Sub

    Private Sub txtTarjetas1Importe_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTarjetas1Importe.KeyUp
        'If imprimiendo = False Then
        If chkTarjetas1.Checked = True And txtTarjeta1.Text <> "" Then
            PagoMultiple()
        End If
        'End If
    End Sub

    Private Sub txtTarjetas1Importe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTarjetas1Importe.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
        ''control para que cuando escriba siempre primero un numero y despues
        'Dim caracter As Char = e.KeyChar
        'If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
        '    'me fijo que no este vacio 
        '    If txtTarjetas1Importe.Text <> "" Then
        '        'al punto lo tomo como "," y controlo que solo se pueda poner una sola
        '        If caracter = "." And (txtTarjetas1Importe.Text.Contains(",") = False) Then
        '            caracter = ","
        '            e.KeyChar = caracter
        '            e.Handled = False
        '        Else
        '            'me fijo si se coloco una coma  y controlo que solo se pueda poner una sola
        '            If (caracter = ",") And (txtTarjetas1Importe.Text.Contains(",") = False) Then
        '                e.Handled = False
        '            Else
        '                e.KeyChar = Chr(0)
        '                e.Handled = True
        '            End If
        '        End If
        '    Else
        '        e.KeyChar = Chr(0)
        '    End If
        'End If
    End Sub

    Private Sub txtTarjetas2Importe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTarjetas2Importe.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
        'control para que cuando escriba siempre primero un numero y despues
        Dim caracter As Char = e.KeyChar
        'If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
        '    'me fijo que no este vacio 
        '    If txtTarjetas2Importe.Text <> "" Then
        '        'al punto lo tomo como "," y controlo que solo se pueda poner una sola
        '        If caracter = "." And (txtTarjetas2Importe.Text.Contains(",") = False) Then
        '            caracter = ","
        '            e.KeyChar = caracter
        '            e.Handled = False
        '        Else
        '            'me fijo si se coloco una coma  y controlo que solo se pueda poner una sola
        '            If (caracter = ",") And (txtTarjetas2Importe.Text.Contains(",") = False) Then
        '                e.Handled = False
        '            Else
        '                e.KeyChar = Chr(0)
        '                e.Handled = True
        '            End If
        '        End If
        '    Else
        '        e.KeyChar = Chr(0)
        '    End If
        'End If
    End Sub

    Private Sub txtTarjetas2Importe_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTarjetas2Importe.KeyDown
        If e.KeyCode = Keys.Up Then
            txtTarjeta2.Focus()
            txtTarjeta2.SelectAll()
        End If
    End Sub

    Private Sub txtTarjetas2Importe_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTarjetas2Importe.KeyUp
        'If imprimiendo = False Then
        If chkTarjetas2.Checked = True And txtTarjeta2.Text <> "" Then
            PagoMultiple()
        End If
        'End If
    End Sub

    Private Sub lblCandadoTarjetas1_Click(sender As Object, e As EventArgs) Handles lblCandadoTarjetas1.Click
        SendKeys.Send("{F7}")
    End Sub

    Private Sub lblCandadoTarjetas2_Click(sender As Object, e As EventArgs) Handles lblCandadoTarjetas2.Click
        SendKeys.Send("{F8}")
    End Sub

    '--------Lista tarjeta 1---------

    Public Sub LlenarLista_Tarjetas(ByVal chktarjeta1 As Boolean)

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Tarjetas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI.ToString)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            'me fijo de donde viene la consulta

            If chktarjeta1 = True Then
                ds_Tarjetas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, CONCAT(Codigo,' - ',NOMBRE) AS 'CodyNom' FROM Tarjetas WHERE Eliminado = 0 AND CONCAT(Codigo,' - ',NOMBRE) LIKE '%" + txtTarjeta1.Text + "%' ")
                ds_Tarjetas.Dispose()

                With lstTarjetas1
                    .DataSource = ds_Tarjetas.Tables(0).DefaultView
                    .DisplayMember = "CodyNom"
                    .ValueMember = "Codigo"
                End With

                Dim cantITems As Integer = CInt(lstTarjetas1.Items.Count.ToString), TamañoLista As Integer = cantITems * 20 + 4 ' 13 = ItemHeight - 4 = Tamaño base del comp.
                'comparo con la altura maxima que le puedo dar dentro de la ventana 
                If TamañoLista < 244 Then
                    lstTarjetas1.Size = New Size(lstTarjetas1.Width, TamañoLista)
                Else
                    lstTarjetas1.Size = New Size(lstTarjetas1.Width, 244)
                End If

            Else

                ds_Tarjetas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, CONCAT(Codigo,' - ',NOMBRE) AS 'CodyNom' FROM Tarjetas WHERE Eliminado = 0 AND CONCAT(Codigo,' - ',NOMBRE) LIKE '%" + txtTarjeta2.Text + "%' ")
                ds_Tarjetas.Dispose()

                With LstTarjetas2
                    .DataSource = ds_Tarjetas.Tables(0).DefaultView
                    .DisplayMember = "CodyNom"
                    .ValueMember = "Codigo"
                End With

                Dim cantITems As Integer = CInt(LstTarjetas2.Items.Count.ToString), TamañoLista As Integer = cantITems * 20 + 4 ' 13 = ItemHeight - 4 = Tamaño base del comp.
                'comparo con la altura maxima que le puedo dar dentro de la ventana 
                If TamañoLista < 164 Then
                    LstTarjetas2.Size = New Size(LstTarjetas2.Width, TamañoLista)
                Else
                    LstTarjetas2.Size = New Size(LstTarjetas2.Width, 164)
                End If
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

    End Sub

    Private Sub txtTarjeta1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTarjeta1.KeyDown

        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Enter Then
            If lstTarjetas1.Height > 4 And lstTarjetas1.Visible = True Then
                'hago foco en la lista de tarjetas
                lstTarjetas1.Focus()
            End If
        End If

    End Sub

    Private Sub txtTarjeta1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTarjeta1.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTarjeta1_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTarjeta1.KeyUp
        seleccionado1 = False
    End Sub

    Private Sub txtTarjeta1_TextChanged(sender As Object, e As EventArgs) Handles txtTarjeta1.TextChanged
        If txtTarjeta1.Text <> "" And txtTarjeta1.Text <> " " And seleccionado1 = False Then
            lstTarjetas1.Visible = True
            LlenarLista_Tarjetas(True)
        Else
            lstTarjetas1.Visible = False
            txtTarjetas1Importe.Text = ""
            txtTarjetas1ImporteFinal.Text = ""
            lblInteres1.Text = ""
            lblInteres1.Visible = False
            lblMontoPorCuotas1.Text = ""
            lblMontoPorCuotas1.Visible = False
            lblM1.Text = ""
            lblM1.Visible = False
            PagoMultiple()
        End If


    End Sub

    Private Sub lstTarjetas1_GotFocus(sender As Object, e As EventArgs) Handles lstTarjetas1.GotFocus
        'aviso que estoy haciendo foco en la lista de tarjetas
        FocoTarjeta1 = True

    End Sub

    Private Sub lstTarjetas1_KeyDown(sender As Object, e As KeyEventArgs) Handles lstTarjetas1.KeyDown

        If e.KeyCode = Keys.Enter Then
            If lstTarjetas1.Text <> "" Then
                seleccionado1 = True
                txtTarjeta1.Text = lstTarjetas1.Text
                CodigoTarjeta1 = lstTarjetas1.SelectedValue.ToString
                'realizo la consulta
                ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Codigo,PorcenRecar,Cuotas From Tarjetas WHERE Codigo= '" & lstTarjetas1.SelectedValue & "'")
                ds.Dispose()
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim codigo As String = ds.Tables(0).Rows(0).Item(0).ToString
                    If ds.Tables(0).Rows(0).Item(1).ToString <> "" Then
                        porcen1 = CDbl(ds.Tables(0).Rows(0).Item(1).ToString)
                        If porcen1 > 0 Then
                            lblInteres1.Visible = True
                            lblInteres1.Text = porcen1.ToString + "%"
                        End If
                    Else
                        porcen1 = 0.0
                    End If
                    cuotas1 = ds.Tables(0).Rows(0).Item(2).ToString

                End If
                If porcen1 > 0 Then
                    If chkDescuentoGlobal.Checked = True Then 'Or frmPuntoVenta_ARAB.chkDescuentoParticular.Checked = True Then
                        If txtDescuento.Text.ToString <> "" Then
                            If CDbl(txtDescuento.Text) > 0 Then
                                'oculto el interes
                                lblInteres1.Visible = False
                                If MessageBox.Show("No se pueden realizar descuentos con la tarjeta seleccionada. Desea retirar los descuentos? ", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    'muestro el interes
                                    lblInteres1.Visible = True
                                    chkDescuentoGlobal.Checked = False
                                    Dim subtotal As Double
                                    'recorro la grilla y pongo los valres como estaban
                                    For i As Integer = 0 To grdItems.Rows.Count - 1
                                        'If frmPuntoVenta_ARAB.lblTipoComprobante.Text.Contains("FACTURAS A") Then
                                        '    frmPuntoVenta_ARAB.grdProductos.Rows(i).Cells(6).Value = frmPuntoVenta_ARAB.grdProductos.Rows(i).Cells(12).Value
                                        'Else
                                        'frmPuntoVenta_ARAB.grdProductos.Rows(i).Cells(6).Value = frmPuntoVenta_ARAB.grdProductos.Rows(i).Cells(9).Value
                                        'End If

                                        'limpio los descuentos 
                                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value = 0
                                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("HORMA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("HORMA") Then
                                            subtotal = grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value
                                        Else
                                            subtotal = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value
                                        End If
                                        grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber(subtotal, 2)
                                    Next

                                    'borro el valor descontado
                                    lblValorDescontado.Text = "0.00"
                                    'llamo a sumasubtotales para calcular el total
                                    CalcularSubtotal()

                                Else
                                    txtTarjeta1.Focus()
                                    txtTarjeta1.SelectAll()
                                    'limpio el interes
                                    lblInteres1.Text = ""
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If

                'Le envio un enter mas para aplicar bien los metodos
                My.Computer.Keyboard.SendKeys("{ENTER}")
                PagoMultiple()
                Formas_Pagos()
                txtTarjetas1Importe.Focus()
                txtTarjetas1Importe.SelectAll()
                lstTarjetas1.Visible = False



            End If
        End If

        'If e.KeyCode = Keys.Escape Then
        '    txtTarjeta1.Focus()
        'End If

    End Sub

    Private Sub lstTarjetas1_LostFocus(sender As Object, e As EventArgs) Handles lstTarjetas1.LostFocus
        'aviso que ya no estoy haciendo foco en la lista de tarjetas
        FocoTarjeta1 = False
    End Sub

    '--------Lista tarjeta 2---------

    Private Sub txtTarjeta2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTarjeta2.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Enter Then
            If LstTarjetas2.Height > 4 And LstTarjetas2.Visible = True Then
                'hago foco en la lista de tarjetas
                LstTarjetas2.Focus()
            End If
        End If
    End Sub

    Private Sub txtTarjeta2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTarjeta2.KeyUp
        seleccionado2 = False
    End Sub

    Private Sub txtTarjeta2_LostFocus(sender As Object, e As EventArgs) Handles txtTarjeta2.LostFocus
        FocoTarjeta2 = False
    End Sub

    Private Sub txtTarjeta2_TextChanged(sender As Object, e As EventArgs) Handles txtTarjeta2.TextChanged
        If txtTarjeta2.Text <> "" And txtTarjeta2.Text <> " " And seleccionado2 = False Then
            LstTarjetas2.Visible = True
            LlenarLista_Tarjetas(False)
        Else
            LstTarjetas2.Visible = False
            txtTarjetas2Importe.Text = ""
            txtTarjetas2ImporteFinal.Text = ""
            lblInteres2.Visible = False
            lblInteres2.Text = ""
            lblMontoPorCuotas2.Text = ""
            lblMontoPorCuotas2.Visible = False
            lblM2.Text = ""
            lblM2.Visible = False
            PagoMultiple()
        End If

    End Sub

    Private Sub LstTarjetas2_GotFocus(sender As Object, e As EventArgs) Handles LstTarjetas2.GotFocus
        'aviso que estoy haciendo foco en la lista de tarjetas
        FocoTarjeta2 = True
    End Sub

    Private Sub LstTarjetas2_KeyDown(sender As Object, e As KeyEventArgs) Handles LstTarjetas2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If LstTarjetas2.Text <> "" Then
                seleccionado2 = True
                txtTarjeta2.Text = LstTarjetas2.Text
                CodigoTarjeta2 = LstTarjetas2.SelectedValue.ToString
                'realizo la consulta
                ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Codigo,PorcenRecar,Cuotas From Tarjetas WHERE Codigo= '" & LstTarjetas2.SelectedValue & "'")
                ds.Dispose()
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim codigo As String = ds.Tables(0).Rows(0).Item(0).ToString
                    If ds.Tables(0).Rows(0).Item(1).ToString <> "" Then
                        porcen2 = CDbl(ds.Tables(0).Rows(0).Item(1).ToString)
                        If porcen2 > 0 Then
                            lblInteres2.Visible = True
                            lblInteres2.Text = porcen2.ToString + "%"
                        End If
                    Else
                        porcen2 = 0.0
                    End If
                    cuotas2 = ds.Tables(0).Rows(0).Item(2).ToString
                End If
                If porcen2 > 0 Then
                    If CDbl(lblValorDescontado.Text) > 0 Then
                        'oculto el interes
                        lblInteres2.Visible = False
                        If MessageBox.Show("No se pueden realizar descuentos con la tarjeta seleccionada. Desea retirar los descuentos? ", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            'muestro el interes
                            lblInteres2.Visible = True
                            chkDescuentoGlobal.Checked = False
                            Dim subtotal As Double
                            'recorro la grilla y pongo los valres como estaban
                            For i As Integer = 0 To grdItems.Rows.Count - 1
                                'If frmPuntoVenta_ARAB.lblTipoComprobante.Text.Contains("FACTURAS A") Then
                                '    frmPuntoVenta_ARAB.grdProductos.Rows(i).Cells(6).Value = frmPuntoVenta_ARAB.grdProductos.Rows(i).Cells(12).Value
                                'Else
                                'limpio los descuentos 
                                grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value = 0
                                If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("HORMA") Or grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value.ToString.Contains("HORMA") Then
                                    subtotal = grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value
                                Else
                                    subtotal = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.Precio).Value
                                End If
                                grdItems.Rows(i).Cells(ColumnasDelGridItems.Subtotal).Value = FormatNumber(subtotal, 2)
                            Next

                            'borro el valor descontado
                            lblValorDescontado.Text = "0.00"
                            'llamo a sumasubtotales para calcular el total
                            CalcularSubtotal()

                        Else
                            'MsgBox("Debe Seleccionar otra tarjeta")
                            txtTarjeta2.Focus()
                            txtTarjeta2.SelectAll()
                            'limpio el interes
                            lblInteres2.Text = ""
                            Exit Sub
                        End If
                    End If
                End If

                'Le envio un enter mas para aplicar bien los metodos
                My.Computer.Keyboard.SendKeys("{ENTER}")
                PagoMultiple()
                Formas_Pagos()
                txtTarjetas2Importe.Focus()
                txtTarjetas2Importe.SelectAll()
                LstTarjetas2.Visible = False
            End If
        End If
        'If e.KeyCode = Keys.Escape Then
        '    txtTarjeta2.Focus()
        'End If
    End Sub

    Private Sub LstTarjetas2_LostFocus(sender As Object, e As EventArgs) Handles LstTarjetas2.LostFocus
        'aviso que ya no estoy haciendo foco en la lista de tarjetas
        FocoTarjeta2 = False
    End Sub





#End Region

#Region "Pago - Procedimientos"

    Public Sub PagoMultiple()

        Try

            'Dim mostrar_mensaje As Boolean
            Dim sumapagorec As Double
            Dim recargo As Double
            Dim tarjetarec As Double

            If chkContado.Checked = True Or chkTarjetas1.Checked = True Or chkTarjetas2.Checked = True Then
                'Cotrolo que los valores no lleguen nulos a la suma

                If txtContado.Text = "" Then
                    txtContado.Text = "0.00"
                    txtContado.SelectAll()
                End If

                If txtTarjetas1Importe.Text = "" Then
                    txtTarjetas1Importe.Text = "0.00"
                    txtTarjetas1Importe.SelectAll()
                End If

                If txtTarjetas2Importe.Text = "" Then
                    txtTarjetas2Importe.Text = "0.00"
                    txtTarjetas2Importe.SelectAll()
                End If

                If txtTarjetas1ImporteFinal.Text = "" Then
                    txtTarjetas1ImporteFinal.Text = "0.00"
                    txtTarjetas1ImporteFinal.SelectAll()
                End If

                If txtTarjetas2ImporteFinal.Text = "" Then
                    txtTarjetas2ImporteFinal.Text = "0.00"
                    txtTarjetas2ImporteFinal.SelectAll()
                End If

                If txtMontoRecarga1.Text = "" Then
                    txtMontoRecarga1.Text = "0.00"
                End If

                If txtMontoRecarga2.Text = "" Then
                    txtMontoRecarga2.Text = "0.00"
                End If

                If chkenfalso = True Then
                    'controlo si se aplico un descuento
                    If txtDescuento.Text = "" Then
                        CalcularSubtotal()
                    End If
                End If

                If chkTarjetas1.Checked = True Or chkTarjetas2.Checked = True Then
                    'Sumo los subtotales para aplicarlo sobre los valores originales 
                    'If frmPuntoVenta_ARAB.lblValorDescontado.Text.ToString = "" Then
                    '    frmPuntoVenta_ARAB.Suma_Subtotales()
                    'End If
                    'oculto la informacion de las cuotas
                    lblM1.Visible = False
                    lblMontoPorCuotas1.Visible = False
                    lblM2.Visible = False
                    lblMontoPorCuotas2.Visible = False

                    Dim Resto As Double
                    'recargo tarjeta1
                    If chkTarjetas1.Checked = True Then
                        'me fijo si se ha seleccionado una tarjeta
                        If txtTarjeta1.Text.ToString <> "" Then
                            ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Codigo,PorcenRecar,Cuotas From Tarjetas WHERE Codigo= '" & CodigoTarjeta1 & "'")
                            ds.Dispose()

                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim codigo As String = ds.Tables(0).Rows(0).Item(0).ToString
                                If ds.Tables(0).Rows(0).Item(1).ToString <> "" Then
                                    porcen1 = ds.Tables(0).Rows(0).Item(1).ToString
                                Else
                                    porcen1 = 0.0
                                End If
                                cuotas1 = ds.Tables(0).Rows(0).Item(2).ToString
                            End If

                            recargo = CDbl(txtTarjetas1Importe.Text) * porcen1 / 100
                            tarjetarec = CDbl(txtTarjetas1Importe.Text) + recargo
                            txtTarjetas1ImporteFinal.Text = FormatNumber(tarjetarec, 2)
                            Resto = CDbl(txtTarjetas1ImporteFinal.Text) - CDbl(txtTarjetas1Importe.Text)
                            txtMontoRecarga1.Text = FormatNumber(Resto, 2)
                            Try
                                'muestro el monto por cuotas
                                If cuotas1 > 1 Then
                                    Dim Montoapagar As Double = CDbl(txtTarjetas1ImporteFinal.Text) / cuotas1
                                    lblMontoPorCuotas1.Visible = True
                                    lblMontoPorCuotas1.Text = "$" + FormatNumber(Montoapagar, 2)
                                    lblM1.Visible = True
                                    lblM1.Text = cuotas1.ToString + " Ctas. de"
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If

                    'recargo tarjeta2
                    If chkTarjetas2.Checked = True Then
                        If txtTarjeta2.Text.ToString <> "" Then
                            ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT Codigo,PorcenRecar,Cuotas From Tarjetas WHERE Codigo= '" & CodigoTarjeta2 & "'")
                            ds.Dispose()

                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim codigo As String = ds.Tables(0).Rows(0).Item(0).ToString
                                If ds.Tables(0).Rows(0).Item(1).ToString <> "" Then
                                    porcen2 = CDbl(ds.Tables(0).Rows(0).Item(1).ToString)
                                Else
                                    porcen2 = 0
                                End If
                                cuotas2 = ds.Tables(0).Rows(0).Item(2).ToString
                            End If
                            recargo = (CDbl(txtTarjetas2Importe.Text) * porcen2 / 100)
                            tarjetarec = CDbl(txtTarjetas2Importe.Text) + recargo
                            txtTarjetas2ImporteFinal.Text = FormatNumber(tarjetarec, 2)
                            Resto = tarjetarec - CDbl(txtTarjetas2Importe.Text)
                            txtMontoRecarga2.Text = FormatNumber(Resto, 2)
                            Try
                                'muestro el monto por cuotas
                                If cuotas2 > 1 Then
                                    Dim Montoapagar As Double = CDbl(txtTarjetas2ImporteFinal.Text) / cuotas2
                                    lblMontoPorCuotas2.Visible = True
                                    lblMontoPorCuotas2.Text = "$" + FormatNumber(Montoapagar, 2)
                                    lblM2.Visible = True
                                    lblM2.Text = cuotas2.ToString + " Ctas. de"
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If

                    'si se aplico un descuento y no hay recargas paso el total que se esta implementando 
                    Dim total As Double

                    If txtDescuento.Text.ToString <> "" Then
                        If CDbl(txtDescuento.Text) > 0 Then
                            If (porcen1 = 0 And porcen2 = 0) Then 'Or CDbl(frmPuntoVenta_ARAB.lblDescuentoCombo.Text) > 0 Then
                                total = txtTotal.Text
                                'total = frmPuntoVenta_ARAB.txtTotalvista.Text
                            Else
                                'total = frmPuntoVenta_ARAB.txtTotalOriginal.Text
                                total = txtTotal.Text
                            End If
                        Else
                            'total = frmPuntoVenta_ARAB.txtTotalOriginal.Text
                            total = txtTotal.Text

                        End If
                    Else
                        'total = frmPuntoVenta_ARAB.txtTotalOriginal.Text
                        total = txtTotal.Text

                    End If

                    Dim calculototal As Double = total + CDbl(txtMontoRecarga1.Text) + CDbl(txtMontoRecarga2.Text)

                    If cmbTipoComprobante.Text = "FACTURAS A" Then

                        'frmPuntoVenta_ARAB.txtTotal.Text = FormatNumber(calculototal, 2)
                        txtTotalVista.Text = FormatNumber(calculototal, 2)
                        Dim calculosubtotal As Double = calculototal / (1 + ((MDIPrincipal.iva) / 100))
                        'frmPuntoVenta_ARAB.txtSubTotal.Text = FormatNumber(calculosubtotal, 2)
                        txtSubtotalVista.Text = FormatNumber(calculosubtotal, 2)
                        Dim calculoiva As Double = calculototal - calculosubtotal
                        'frmPuntoVenta_ARAB.txtIVA.Text = FormatNumber(calculoiva, 2)
                        txtIVAVista.Text = FormatNumber(calculoiva, 2)
                    Else
                        'Coloco los valores finales
                        'frmPuntoVenta_ARAB.txtSubTotal.Text = FormatNumber(total + CDbl(txtMontoRecarga1.Text) + CDbl(txtMontoRecarga2.Text), 2)
                        'frmPuntoVenta_ARAB.txtIVA.Text = "0.00"
                        'frmPuntoVenta_ARAB.txtTotal.Text = frmPuntoVenta_ARAB.txtSubTotal.Text
                        txtSubtotalVista.Text = FormatNumber(calculototal, 2)
                        txtIVAVista.Text = "0.00"
                        txtTotalVista.Text = txtSubtotalVista.Text
                    End If

                End If

                'sumo los los montos 
                sumapagorec = CDbl(txtContado.Text) + CDbl(txtTarjetas1ImporteFinal.Text) + CDbl(txtTarjetas2ImporteFinal.Text)

                'si la suma es igual a cero no muestro los valores de en los txt
                If sumapagorec <> 0 Then
                    'If sumapagorec <= frmPuntoVenta_ARAB.txtTotal.Text Then
                    If sumapagorec <= txtTotalVista.Text Then
                        txtVuelto.Text = "0.00"
                        'txtResto.Text = FormatNumber((sumapagorec - frmPuntoVenta_ARAB.txtTotal.Text) * (-1), 2)
                        txtResto.Text = FormatNumber((sumapagorec - txtTotalVista.Text) * (-1), 2)
                    Else
                        txtResto.Text = "0.00"
                        'txtVuelto.Text = FormatNumber(sumapagorec - frmPuntoVenta_ARAB.txtTotal.Text, 2)
                        txtVuelto.Text = FormatNumber(sumapagorec - txtTotalVista.Text, 2)
                    End If
                Else
                    txtResto.Text = "0.00"
                    txtVuelto.Text = "0.00"
                End If
            Else
                txtContado.Text = ""
                txtTarjetas1Importe.Text = ""
                txtTarjetas2Importe.Text = ""
                'control si se aplico un descuento
                'If frmPuntoVenta_ARAB.lblValorDescontado.Text.ToString = "" Then
                '    frmPuntoVenta_ARAB.Suma_Subtotales()
                'End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Formas_Pagos()

        Try
            Dim Formasdepago As String
            Dim Contado As String = ""
            Dim Tarjeta1 As String = ""
            Dim Tarjeta2 As String = ""
            Dim Total As String = " "
            For Each Control As Object In Me.GroupBoxPago.Controls
                If TypeOf (Control) Is CheckBox Then
                    For i As Integer = 0 To 2
                        'Contado
                        If Control.tag = 0 Then
                            If Control.checked = True Then
                                Contado = "C"
                                Exit For
                            End If
                        End If
                        'Tarjeta1
                        If Control.tag = 1 Then
                            If Control.checked = True Then
                                Tarjeta1 = "T1"
                                Exit For
                            End If
                        End If
                        'Tarjeta2
                        If Control.tag = 2 Then
                            If Control.checked = True Then
                                Tarjeta2 = "T2"
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
            'concateno las formas de pago
            Formasdepago = Contado + Tarjeta1 + Tarjeta2
            'Controlo que los txttarjetas no esten vacias
            If Formasdepago.Contains("T1") Or Formasdepago.Contains("T2") Then
                If chkTarjetas1.Checked = True Then
                    If txtTarjeta1.Text = "" Then
                        Exit Sub
                    End If
                End If
                If chkTarjetas2.Checked = True Then
                    If txtTarjeta2.Text = "" Then
                        Exit Sub
                    End If
                End If
            End If
            'me fijo si se aplico algun descuento 
            If txtDescuento.Text <> "" Then
                'me fijo sea mayo a cero
                If CDbl(txtDescuento.Text) > 0 Then
                    'si hay un descuento trabajo con el txttotal con descuento
                    Total = txtTotal.Text
                    'Total =  txtTotalvista.Text
                Else
                    ' Total =  txtTotalOriginal.Text
                    Total = txtTotal.Text
                End If
            Else
                'Total =  txtTotalOriginal.Text
                Total = txtTotal.Text
            End If

            'Selecciono el caso de pago(uso este control para seleccionar la forma de pago)
            Select Case Formasdepago <> ""
                'caso1
                Case Formasdepago = Contado
                    txtContado.Text = Total
                    txtContado.Focus()
                    txtContado.SelectAll()
                    'caso2
                Case Formasdepago = Tarjeta1
                    txtTarjetas1Importe.Text = Total
                    txtTarjetas1Importe.Focus()
                    txtTarjetas1Importe.SelectAll()
                    'txtTarjetas1Importe.Select(txtTarjetas1Importe.Text.Length, 0)
                    'caso3
                Case Formasdepago = Tarjeta2
                    txtTarjetas2Importe.Text = Total
                    txtTarjetas2Importe.Focus()
                    txtTarjetas1Importe.SelectAll()
                    'txtTarjetas2Importe.Select(txtTarjetas2Importe.Text.Length, 0)
                    'caso4
                Case Formasdepago = Contado + Tarjeta1
                    If txtContado.Text <> "" And CDbl(txtContado.Text) > 0 And CDbl(txtResto.Text) > 0 Then
                        txtTarjetas1Importe.Text = FormatNumber(CDbl(Total) - CDbl(txtContado.Text), 2)
                        txtTarjetas1Importe.Focus()
                        txtTarjetas1Importe.SelectAll()
                    Else
                        If txtContado.Text = "" Or CDbl(txtContado.Text) = 0 Then
                            If txtTarjetas1Importe.Text = "" Or CDbl(txtTarjetas1Importe.Text) = 0 Then
                                txtTarjetas1Importe.Text = Total
                                txtTarjetas1Importe.Select(txtTarjetas1Importe.Text.Length, 0)
                            Else
                                If CDbl(txtTarjetas1Importe.Text) = CDbl(Total) Then
                                    txtTarjetas1Importe.Text = "0.00"
                                    txtTarjetas1ImporteFinal.Text = txtTarjetas1Importe.Text
                                    txtContado.Focus()
                                    txtContado.SelectAll()
                                Else
                                    If txtTarjetas1Importe.Text < CDbl(Total) Then
                                        txtContado.Focus()
                                        txtContado.SelectAll()
                                    Else
                                        txtTarjetas1Importe.Text = "0.00"
                                        txtTarjetas1ImporteFinal.Text = txtTarjetas1Importe.Text
                                        txtTarjetas1Importe.Focus()
                                    End If
                                End If
                            End If
                        Else
                            If chkenfalso = True Then
                                txtContado.Focus()
                                txtContado.SelectAll()
                            Else
                                'borro lo que tiene el txt(es igual o mayor al total)
                                txtContado.Text = "0.00"
                            End If
                        End If
                    End If
                    'caso5
                Case Formasdepago = Contado + Tarjeta2
                    If txtContado.Text <> "" And CDbl(txtContado.Text) > 0 And CDbl(txtResto.Text) > 0 Then
                        txtTarjetas2Importe.Text = FormatNumber(CDbl(Total) - CDbl(txtContado.Text), 2)
                        txtTarjetas2Importe.Focus()
                        txtTarjetas2Importe.SelectAll()
                    Else
                        If txtContado.Text = "" Or CDbl(txtContado.Text) = 0 Then
                            If txtTarjetas2Importe.Text = "" Or CDbl(txtTarjetas2Importe.Text) = 0 Then
                                txtTarjetas2Importe.Text = Total
                                txtTarjetas2Importe.Select(txtTarjetas2Importe.Text.Length, 0)
                            Else
                                If CDbl(txtTarjetas2Importe.Text) = CDbl(Total) Then
                                    txtTarjetas2Importe.Text = "0.00"
                                    txtTarjetas2ImporteFinal.Text = txtTarjetas2Importe.Text
                                    txtContado.Focus()
                                    txtContado.SelectAll()
                                Else
                                    If txtTarjetas2Importe.Text < CDbl(Total) Then
                                        txtContado.Focus()
                                        txtContado.SelectAll()
                                    Else
                                        txtTarjetas2Importe.Text = "0.00"
                                        txtTarjetas2ImporteFinal.Text = txtTarjetas2Importe.Text
                                        txtTarjetas2Importe.Focus()
                                    End If
                                End If
                            End If
                        Else
                            If chkenfalso = True Then
                                txtContado.Focus()
                                txtContado.SelectAll()
                            Else
                                'borro lo que tiene el txt(es igual o mayor al total)
                                txtContado.Text = "0.00"
                            End If
                        End If
                    End If
                    'caso6
                Case Formasdepago = Tarjeta1 + Tarjeta2
                    If txtTarjetas1Importe.Text <> "" And CDbl(txtTarjetas1Importe.Text) > 0 And CDbl(txtResto.Text) > 0 Then
                        txtTarjetas2Importe.Text = FormatNumber(CDbl(Total) - CDbl(txtTarjetas1Importe.Text), 2)
                        txtTarjetas2Importe.Focus()
                    Else
                        If txtTarjetas1Importe.Text = "" Or CDbl(txtTarjetas1Importe.Text) = 0 Then
                            If txtTarjetas2Importe.Text = "" Or CDbl(txtTarjetas2Importe.Text) = 0 Then
                                txtTarjetas2Importe.Text = Total
                                txtTarjetas2Importe.Select(txtTarjetas2Importe.Text.Length, 0)
                            Else
                                If CDbl(txtTarjetas2Importe.Text) = CDbl(Total) Then
                                    txtTarjetas2Importe.Text = "0.00"
                                    txtTarjetas2ImporteFinal.Text = txtTarjetas2Importe.Text
                                Else
                                    If txtTarjetas2Importe.Text < CDbl(Total) Then
                                        txtTarjetas1Importe.Text = FormatNumber(CDbl(Total) - CDbl(txtTarjetas2Importe.Text), 2)
                                        txtTarjetas1Importe.Select(txtTarjetas1Importe.Text.Length, 0)
                                        txtTarjetas1Importe.Focus()
                                    Else
                                        txtTarjetas2Importe.Text = "0.00"
                                        txtTarjetas2ImporteFinal.Text = txtTarjetas2Importe.Text
                                        txtTarjetas2Importe.Focus()
                                    End If
                                End If
                            End If
                        Else
                            If chkenfalso = True Then
                                txtTarjetas1Importe.Focus()
                                txtTarjetas1Importe.SelectAll()
                            Else
                                'borro lo que tiene el txt(es igual o mayor al total)
                                txtTarjetas1Importe.Text = "0.00"
                            End If
                        End If
                    End If
                    'caso7
                Case Formasdepago = Contado + Tarjeta1 + Tarjeta2
                    If txtContado.Text <> "" And (txtContado.Text) > 0 And txtTarjetas1Importe.Text <> "" And (txtTarjetas1Importe.Text) > 0 And CDbl(txtResto.Text) > 0 Then
                        txtTarjetas2Importe.Text = FormatNumber(CDbl(txtTotalOriginal.Text) - CDbl(txtContado.Text) - CDbl(txtTarjetas1Importe.Text), 2)
                        txtTarjetas2Importe.Select(txtTarjetas2Importe.Text.Length, 0)
                        txtTarjetas2Importe.Focus()
                    Else
                        If txtContado.Text <> "" And (txtContado.Text) > 0 And txtTarjetas2Importe.Text <> "" And (txtTarjetas2Importe.Text) > 0 And CDbl(txtResto.Text) > 0 Then
                            txtTarjetas1Importe.Text = FormatNumber(CDbl(txtTotalOriginal.Text) - CDbl(txtContado.Text) - CDbl(txtTarjetas2Importe.Text), 2)
                            txtTarjetas1Importe.Select(txtTarjetas1Importe.Text.Length, 0)
                            txtTarjetas1Importe.Focus()
                        Else
                            txtContado.Focus()
                            txtContado.SelectAll()
                        End If
                    End If
            End Select
            If Formasdepago = "" Then
                txtTarjetas1ImporteFinal.Text = ""
                txtTarjetas2ImporteFinal.Text = ""
                txtResto.Text = ""
                txtVuelto.Text = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub limpiarGropPago()


        chkContado.Checked = False
        txtContado.Enabled = False
        chkTarjetas1.Checked = False
        txtTarjeta1.Enabled = False
        txtTarjetas1Importe.Enabled = False
        txtTarjetas1ImporteFinal.Enabled = False
        chkTarjetas2.Checked = False
        txtTarjeta2.Enabled = False
        txtTarjetas2Importe.Enabled = False
        txtTarjetas2ImporteFinal.Enabled = False
        txtVuelto.Text = ""
        txtResto.Text = ""
        cuotas1 = 0.0
        porcen1 = 0.0
        cuotas2 = 0.0
        porcen2 = 0.0

    End Sub

    Private Sub ActivarPago()

        txtSubtotalVista.Text = txtSubtotal.Text
        txtIVAVista.Text = txtIVA.Text
        txtTotalVista.Text = txtTotal.Text

        If chkContado.Checked = True Or chkTarjetas1.Checked = True Or chkTarjetas2.Checked = True Then
            txtSubtotalVista.Visible = True
            txtIVAVista.Visible = True
            txtTotalVista.Visible = True
        Else
            txtSubtotalVista.Visible = False
            txtIVAVista.Visible = False
            txtTotalVista.Visible = False
            'CalcularSubtotal()
        End If

    End Sub



#End Region

#Region "Pago - Botones"

    '    Public Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

    '        Dim Contado As String = ""
    '        Dim Tarjeta1 As String = ""
    '        Dim Tarjeta2 As String = ""
    '        Dim sumapagofinal As Double
    '        'variable para hacer consulta de ticketpromedio
    '        Dim dsTicketPromedio As Data.DataSet
    '        'boleano para avisar que siga sin imprimir
    '        Dim SeguirSinimprimir As Boolean = False
    '        'variable para avisar que finalizo bien la venta
    '        Dim finalizar_cambioMuelto As Boolean = False
    '        ''cre una ventana de imprimiendo 
    '        'Dim ventanaIMp As New frmImprimiendo
    '        'Im = ventanaIMp


    '        'control para pasar deciamles cuando solo paga con contado
    '        If txtContado.Text <> "" Then
    '            If CDbl(txtContado.Text) > 0 Then
    '                'control para que el txt tenga siempre dos decimales
    '                Dim cadena As String = txtContado.Text
    '                Dim cadenachar() As Char = cadena.ToCharArray
    '                'si no tiene coma
    '                If Not txtContado.Text.Contains(",") Then
    '                    txtContado.Text = txtContado.Text + ",00"
    '                Else
    '                    For i As Integer = 0 To cadenachar.Length - 1
    '                        If cadena(i) = "," Then
    '                            'si tiene coma y despues nada
    '                            If cadenachar(i) = cadenachar(cadenachar.Length - 1) Then
    '                                txtContado.Text = txtContado.Text + "00"
    '                                Exit For
    '                            Else
    '                                'si tiene coma y un solo decimal
    '                                Dim j As Integer = i + 1
    '                                Dim tamaño As Integer = cadenachar.Length - 1
    '                                If j = tamaño Then
    '                                    txtContado.Text = txtContado.Text + "0"
    '                                    Exit For
    '                                End If
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End If

    '        'verifico que no sea cuenta corriente primero(si lo es no se realiza ningun pago)
    '        If frmPuntoVenta_ARAB.chkCtaCte.Checked = False Then
    '            'verifico si es una seña
    '            If frmPuntoVenta_ARAB.chkSeña.Checked = True And frmPuntoVenta_ARAB.chkSeña.ForeColor = Color.Red Then
    '                'me fijo si es una venta manual
    '                If VentaManual = True Then
    '                    'If frmPuntoVenta_ARAB.txtNroFacturaManual.Text = "" Then
    '                    '    MsgBox("Debe ingresar el nro de Factura Manual antes de Continuar", MsgBoxStyle.Critical, "Control de Errores")
    '                    '    frmPuntoVenta_ARAB.txtNroFacturaManual.Focus()
    '                    '    Exit Sub
    '                    'End If
    '                    SeguirSinimprimir = True
    '                End If
    '                'inserto en la tabla muleto
    '                Select Case RealizarModificar_Venta(False, False, True)
    '                    Case Is = -10
    '                        Cancelar_Tran()
    '                        MsgBox("El nro.de ticket ingresado ya existe. Por favor Verifique.Muleto")
    '                        Finalizar = False
    '                        frmPuntoVenta_ARAB.contado_ok = False
    '                        frmPuntoVenta_ARAB.txtNroFacturaManual.Focus()
    '                        Me.Close()
    '                        Exit Sub
    '                    Case Is <= 0
    '                        Cancelar_Tran()
    '                        MsgBox("Se produjo un error al insertar la información en el sistema (Encabezado)Muleto")
    '                    Case 1
    '                        Select Case RealizarModificar_VentaDetalle(False, False, True)
    '                            Case Is <= 0
    '                                Cancelar_Tran()
    '                                MsgBox("Se produjo un error al insertar la información en el sistema (Detalle)Muleto")
    '                            Case Is >= 1
    '                                Cerrar_Tran()
    '                                'EliminarVenta_Temp()
    '                                ' Finalizar = True
    '                        End Select
    '                End Select
    '                'control para saber si sigo sin imprimir 
    '                If SeguirSinimprimir = True Then
    '                    GoTo ContinuarSinImprimir4
    '                End If
    '                'controlo que se haya impreso el ticket
    '                If Imprimir_Ticket(False, letra_Imp, CODcondicioniva_Imp, Cuit_Imp, Nombre_Imp, Direccion_Imp, "", "") = True Then
    'ContinuarSinImprimir4:
    '                    'inserto la venta
    '                    Select Case RealizarModificar_Venta(False, False, False)
    '                        Case Is = -10
    '                            Cancelar_Tran()
    '                            MsgBox("El nro.de ticket ingresado ya existe. Por favor Verifique.")
    '                            Finalizar = False
    '                            frmPuntoVenta_ARAB.contado_ok = False
    '                            frmPuntoVenta_ARAB.txtNroFacturaManual.Focus()
    '                            Me.Close()
    '                            Exit Sub
    '                        Case Is <= 0
    '                            Cancelar_Tran()
    '                            MsgBox("Se produjo un error al insertar la información en el sistema (Encabezado)")
    '                        Case 1
    '                            Select Case RealizarModificar_VentaDetalle(False, False, False)
    '                                Case Is <= 0
    '                                    Cancelar_Tran()
    '                                    MsgBox("Se produjo un error al insertar la información en el sistema (Detalle)")
    '                                Case Is >= 1

    '                                    Cerrar_Tran()
    '                                    EliminarVenta_Temp()
    '                                    Finalizar = True
    '                            End Select
    '                    End Select
    '                    ''imprimo ticket no fiscal 
    '                    'imprimirTicket_NoFiscal()
    '                    'limpio formmulario de pago
    '                    limpiarformulario()
    '                    'limpio pantalla principal
    '                    frmPuntoVenta_ARAB.limpiarpantalla()
    '                    frmPuntoVenta_ARAB.MenuItemCancelar.Enabled = False
    '                    'salgo de la ventana de pago
    '                    Me.Close()
    '                    'me fijo si se llamo el evento del boton de imprimir desde la ventana de pago al contado
    '                    If desdecontado = True Then
    '                        'cierro la ventana de pago al contado
    '                        frmPuntoVenta_ARAB.contado_ok = True
    '                    End If

    '                    'me fijo si la bandera de actualizacion esta en falsa para actualizar o no le nro de ticket
    '                    If NroActualizado = False Then
    '                        If Actualizar_NroTicket() = False Then
    '                            'si el metodo me devuelve falso cierro el sistema y aviso que se debe revisar el equipo
    '                            MsgBox("ERROR. Se ha perdio la comunicación con el controlador, por favor reinicie el equipo.")
    '                            Application.Exit()
    '                        Else
    '                            Me.Enabled = False
    '                            frmPuntoVenta_ARAB.Enabled = False
    '                        End If
    '                    End If

    '                    'actualizo el ticket promedio 
    '                    Try
    '                        dsTicketPromedio = SqlHelper.ExecuteDataset(Conn, CommandType.Text, "SELECT AVG(Total) AS 'VENTAS_PROMEDIO' FROM Ventas WHERE procesado = 0 AND TOTAL >= " & TotalMin & " AND convert(nvarchar(10),Fecha,103) = convert(nvarchar(10),'" & frmPuntoVenta_ARAB.lblFechaActualDec.Text & "',103) AND NumVendedor =" & frmPuntoVenta_ARAB.lblNumVen.Text)
    '                        dsTicketPromedio.Dispose()
    '                        If dsTicketPromedio.Tables(0).Rows.Count > 0 Then
    '                            If dsTicketPromedio.Tables(0).Rows(0).Item(0).ToString <> "" Then
    '                                frmPuntoVenta_ARAB.lblTicketPromedioMontoCan.Text = "$" + FormatNumber(dsTicketPromedio.Tables(0).Rows(0).Item(0).ToString, 2)
    '                            Else
    '                                frmPuntoVenta_ARAB.lblTicketPromedioMontoCan.Text = "$0.00"
    '                            End If
    '                        Else
    '                            frmPuntoVenta_ARAB.lblTicketPromedioMontoCan.Text = ""
    '                        End If
    '                    Catch ex As Exception

    '                    End Try
    '                    'salgo de boton imprimir
    '                    Exit Sub
    '                Else
    '                    Finalizar = False
    '                    'digo que contado esta false si se uso un atajo(f12) o venta solo contado
    '                    frmPuntoVenta_ARAB.contado_ok = False

    '                    'actualizo el numero de ticket para tener en cuenta el ticket cancelado
    '                    If Actualizar_NroTicket() = False Then
    '                        'si el metodo me devuelve falso cierro el sistema y aviso que se debe revisar el equipo
    '                        MsgBox("ERROR. Se ha perdio la comunicación con el controlador, por favor reinicie el equipo.")
    '                        Application.Exit()
    '                    Else
    '                        Me.Enabled = False
    '                        frmPuntoVenta_ARAB.Enabled = False
    '                    End If
    '                    'salgo del evento
    '                    Exit Sub
    '                End If
    '                '-----------SECCION DE VENTA-----------
    '            Else
    '                'controlo que al menos una forma de pago esta seleccionada
    '                If chkContado.Checked = True Or chkTarjetas1.Checked = True Or chkTarjetas2.Checked = True Then
    '                    If txtTarjetas1ImporteFinal.Text = "" Then
    '                        txtTarjetas1ImporteFinal.Text = "0.00"
    '                    End If
    '                    If txtTarjetas2ImporteFinal.Text = "" Then
    '                        txtTarjetas2ImporteFinal.Text = "0.00"
    '                    End If
    '                    If txtContado.Text = "" Then
    '                        txtContado.Text = "0.00"
    '                    End If
    '                    sumapagofinal = FormatNumber(CDbl(txtContado.Text) + CDbl(txtTarjetas1ImporteFinal.Text) + CDbl(txtTarjetas2ImporteFinal.Text), 2)
    '                    'controlo que los txt contado y tarjetas no esten vacios
    '                    For Each Control As Object In Me.Controls
    '                        If TypeOf (Control) Is CheckBox Then
    '                            For i As Integer = 0 To 2
    '                                'Contado
    '                                If Control.Tag = 0 Then
    '                                    If Control.checked = True Then
    '                                        Contado = "C"
    '                                        Exit For
    '                                    End If
    '                                End If
    '                                'Tarjeta1
    '                                If Control.Tag = 1 Then
    '                                    If Control.checked = True Then
    '                                        Tarjeta1 = "T1"
    '                                        Exit For
    '                                    End If
    '                                End If
    '                                'Tarjeta2
    '                                If Control.Tag = 2 Then
    '                                    If Control.checked = True Then
    '                                        Tarjeta2 = "T2"
    '                                        Exit For
    '                                    End If
    '                                End If
    '                            Next
    '                        End If
    '                    Next
    '                End If
    '                'concateno las formas de pago y la igual al txtoculto
    '                txtFormaPago.Text = Contado + Tarjeta1 + Tarjeta2
    '                If txtFormaPago.Text.Contains("C") And txtFormaPago.Text.Contains("T") Then
    '                    If txtContado.Text <> "" And txtVuelto.Text <> "" Then
    '                        If CDbl(txtContado.Text) = CDbl(txtVuelto.Text) Then
    '                            'controlo que el valor sea mayor a cero
    '                            If CDbl(txtContado.Text) > 0 Then
    '                                MsgBox("El monto contado no puede ser igual al vuelto. Por favor verifique.")
    '                                txtContado.Focus()
    '                                txtContado.SelectAll()
    '                                Exit Sub
    '                            End If
    '                        Else
    '                            If CDbl(txtVuelto.Text) > CDbl(txtContado.Text) Then
    '                                MsgBox("No se puede dar vuelto si el monto contado es menor al  vuelto. Por favor verifique")
    '                                txtContado.Focus()
    '                                txtContado.SelectAll()
    '                                Exit Sub
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '                If Not txtFormaPago.Text.Contains("C") Then
    '                    If txtVuelto.Text.ToString <> "" Then
    '                        If CDbl(txtVuelto.Text) > 0 Then
    '                            MsgBox("No se puede entregar vuelto con las formas de pago seleccionadas ")
    '                            Exit Sub
    '                        End If
    '                    End If
    '                    'controlo que el codigo de tarjetas no sean iguales
    '                    If chkTarjetas1.Checked = True And chkTarjetas2.Checked = True Then
    '                        'controlo los codigos de tarjetas no esten vacios
    '                        If CodigoTarjeta1 = "" Then
    '                            MsgBox("Por favor vuelva a seleccionar una tarjeta.")
    '                            txtTarjeta1.Focus()
    '                            Exit Sub
    '                        End If
    '                        If CodigoTarjeta2 = "" Then
    '                            MsgBox("Por favor vuelva a seleccionar una tarjeta.")
    '                            txtTarjeta2.Focus()
    '                            Exit Sub
    '                        End If
    '                        'me fijo si los codigos de tarjetas no sean iguales
    '                        If CodigoTarjeta1.ToString = CodigoTarjeta2.ToString Then
    '                            MsgBox("No se puede finalizar la venta con dos tarjetas iguales. Por favor seleccione otra. ")
    '                            txtTarjeta1.Focus()
    '                            Exit Sub
    '                        End If
    '                        'si se pago con una de las tarjetas controlo que el codigo no llegue vacio
    '                    ElseIf chkTarjetas1.Checked = True Or chkTarjetas2.Checked = False Then
    '                        'me fijo que se pase si o si que se pasen los codigos de tarjetas 
    '                        If chkTarjetas1.Checked = True Then
    '                            If CodigoTarjeta1 = "" Then
    '                                MsgBox("Por favor vuelva a seleccionar una tarjeta.")
    '                                txtTarjeta1.Focus()
    '                                Exit Sub
    '                            End If
    '                        End If
    '                        If chkTarjetas2.Checked = True Then
    '                            If CodigoTarjeta2 = "" Then
    '                                MsgBox("Por favor vuelva a seleccionar una tarjeta.")
    '                                txtTarjeta2.Focus()
    '                                Exit Sub
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            'If sumapagofinal >= CDbl(frmPuntoVenta_ARAB.txtTotal.Text) Then
    '            If sumapagofinal >= CDbl(frmPuntoVenta_ARAB.txtTotalVista.Text) Then
    '                'me fijo si es una venta manual
    '                If VentaManual = True Then
    '                    'If frmPuntoVenta_ARAB.txtNroFacturaManual.Text = "" Then
    '                    '    MsgBox("Debe ingresar el nro de Factura Manual antes de Continuar", MsgBoxStyle.Critical, "Control de Errores")
    '                    '    frmPuntoVenta_ARAB.txtNroFacturaManual.Focus()
    '                    '    Exit Sub
    '                    'End If
    '                    SeguirSinimprimir = True
    '                End If
    '                'inserto en la tabla muleto
    '                Select Case RealizarModificar_Venta(False, False, True)
    '                    Case Is = -10
    '                        Cancelar_Tran()
    '                        MsgBox("El nro.de ticket ingresado ya existe. Por favor Verifique.Muleto")
    '                        Finalizar = False
    '                        frmPuntoVenta_ARAB.contado_ok = False
    '                        frmPuntoVenta_ARAB.txtNroFacturaManual.Focus()
    '                        Me.Close()
    '                        Exit Sub
    '                    Case Is <= 0
    '                        Cancelar_Tran()
    '                        MsgBox("Se produjo un error al insertar la información en el sistema (Encabezado)Muleto")
    '                    Case 1
    '                        Select Case RealizarModificar_VentaDetalle(False, False, True)
    '                            Case Is <= 0
    '                                Cancelar_Tran()
    '                                MsgBox("Se produjo un error al insertar la información en el sistema (Detalle)Muleto")
    '                            Case Is >= 1
    '                                Cerrar_Tran()
    '                                'EliminarVenta_Temp()
    '                                '  Finalizar = True
    '                                'ME FIJO SI ES UN CAMBIO 
    '                                If frmPuntoVenta_ARAB.btnCamDev.SymbolColor = Color.Red Then
    '                                    finalizar_cambioMuelto = True
    '                                End If
    '                        End Select
    '                End Select

    '                If finalizar_cambioMuelto = True Then
    '                    Dim CADE_muleto As New frmCamDev
    '                    'inserto la devolucion
    '                    Select Case CADE_muleto.Realizar_CambioDevolucion(True, True)
    '                        Case Is <= 0
    '                            CADE_muleto.Cancelar_Tran()
    '                            MsgBox("Se produjo un error al insertar la información en el sistema (Encabezado CamDev)-Muleto")
    '                        Case 1
    '                            Select Case CADE_muleto.Realizar_CambioDevolucionDetalle(True, True)
    '                                Case Is <= 0
    '                                    CADE_muleto.Cancelar_Tran()
    '                                    MsgBox("Se produjo un error al insertar la información en el sistema (Detalle CamDev)_Muleto")
    '                                Case Is >= 1
    '                                    CADE_muleto.Cerrar_Tran()
    '                            End Select
    '                    End Select
    '                End If

    '                'me fijo si sigo sin imprimir o si es una compra interna
    '                If SeguirSinimprimir = True Or frmPuntoVenta_ARAB.chkComprasInterna.Checked = True Then
    '                    GoTo ContinuarSinImprimir5
    '                End If
    '                'me fijo si la funcion de imprimir devuelve true para insertar en la tabla de ventas 
    '                If Imprimir_Ticket(False, letra_Imp, CODcondicioniva_Imp, Cuit_Imp, Nombre_Imp, Direccion_Imp, "", "") = True Then
    'ContinuarSinImprimir5:
    '                    Select Case RealizarModificar_Venta(False, False, False)
    '                        Case Is = -10
    '                            Cancelar_Tran()
    '                            MsgBox("El nro. de ticket ingresado ya existe. Por favor Verifique.")
    '                            Finalizar = False
    '                            frmPuntoVenta_ARAB.contado_ok = False
    '                            frmPuntoVenta_ARAB.txtNroFacturaManual.Focus()
    '                            Me.Close()
    '                            Exit Sub
    '                        Case Is <= 0
    '                            Cancelar_Tran()
    '                            MsgBox("Se produjo un error al insertar la información en el sistema (Encabezado)")
    '                        Case 1
    '                            Select Case RealizarModificar_VentaDetalle(False, False, False)
    '                                Case Is <= 0
    '                                    Cancelar_Tran()
    '                                    MsgBox("Se produjo un error al insertar la información en el sistema (Detalle)")
    '                                Case Is >= 1
    '                                    Cerrar_Tran()
    '                                    'elimino las ventas temporales 
    '                                    EliminarVenta_Temp()
    '                                    'coloco en 1 en eliminado 
    '                                    If frmPuntoVenta_ARAB.chkSeña.Checked = True And frmPuntoVenta_ARAB.chkSeña.ForeColor = Color.Green Then
    '                                        Dim id As Integer
    '                                        If frmPuntoVenta_ARAB.txtIdPreventa.Text <> "" Then
    '                                            id = frmPuntoVenta_ARAB.txtIdPreventa.Text
    '                                        Else
    '                                            id = 0
    '                                        End If
    '                                        Finalizar_preventa(id)
    '                                    End If
    '                                    'coloco los indicadores
    '                                    frmVendedoresLogin.VendedoresActivos()
    '                                    'indico el vendedor actual
    '                                    frmVendedoresLogin.VendedorActual()
    '                                    'paso finalizar como en true para que aparezca la pantalla de log
    '                                    Finalizar = True
    '                            End Select
    '                    End Select
    '                    'me fijo si el boton de cambio y devolucion esta activo(Rojo)
    '                    If frmPuntoVenta_ARAB.btnCamDev.SymbolColor = Color.DarkGreen Then
    '                        frmPuntoVenta_ARAB.limpiarpantalla()
    '                        frmPuntoVenta_ARAB.MenuItemCancelar.Enabled = False
    '                        Finalizar = True
    '                    End If
    '                    'me fijo si la bandera de actualizacion esta en falsa para actualizar o no le nro de ticket
    '                    If NroActualizado = False Then
    '                        If Actualizar_NroTicket() = False Then
    '                            'si el metodo me devuelve falso cierro el sistema y aviso que se debe revisar el equipo
    '                            MsgBox("ERROR. Se ha perdio la comunicación con el controlador, por favor reinicie el equipo.")
    '                            Application.Exit()
    '                        End If
    '                    End If
    '                Else
    '                    'salgo del evento
    '                    Finalizar = False
    '                    'digo que contado esta false si se uso un atajo(f12) o venta solo contado
    '                    frmPuntoVenta_ARAB.contado_ok = False

    '                    'actualizo el numero de ticket para tener en cuenta el ticket cancelado
    '                    If Actualizar_NroTicket() = False Then
    '                        'si el metodo me devuelve falso cierro el sistema y aviso que se debe revisar el equipo
    '                        MsgBox("ERROR. Se ha perdio la comunicación con el controlador, por favor reinicie el equipo.")
    '                        Application.Exit()
    '                    End If

    '                    Exit Sub
    '                End If
    '                'me fijo si se llamo el evento del boton de imprimir desde la ventana de pago al contado
    '                If desdecontado = True Then
    '                    'cierro la ventana de pago al contado
    '                    frmPuntoVenta_ARAB.contado_ok = True
    '                End If
    '                'limpio la ventana de pago
    '                limpiarformulario()
    '                'salgo de la ventana de pago
    '                Me.Close()
    '            Else
    '                MsgBox("La forma de pago debe ser mayor o igual al total a pagar")
    '                Exit Sub
    '                'chkTarjetas1.Checked = False
    '                'chkTarjetas2.Checked = False
    '                'If chkContado.Checked = False Then
    '                '    chkContado.Checked = True
    '                '    txtContado.Focus()
    '                'Else
    '                '    txtContado.Focus()
    '                'End If
    '            End If
    '            'End If
    '            'actualizo el ticket promedio 
    '            Try
    '                dsTicketPromedio = SqlHelper.ExecuteDataset(Conn, CommandType.Text, "SELECT AVG(Total) AS 'VENTAS_PROMEDIO' FROM Ventas WHERE procesado = 0 AND TOTAL >= " & TotalMin & " AND convert(nvarchar(10),Fecha,103) = convert(nvarchar(10),'" & frmPuntoVenta_ARAB.lblFechaActualDec.Text & "',103) AND NumVendedor =" & frmPuntoVenta_ARAB.lblNumVen.Text)
    '                dsTicketPromedio.Dispose()
    '                If dsTicketPromedio.Tables(0).Rows.Count > 0 Then
    '                    If dsTicketPromedio.Tables(0).Rows(0).Item(0).ToString <> "" Then
    '                        frmPuntoVenta_ARAB.lblTicketPromedioMontoCan.Text = "$" + FormatNumber(dsTicketPromedio.Tables(0).Rows(0).Item(0).ToString, 2)
    '                    Else
    '                        frmPuntoVenta_ARAB.lblTicketPromedioMontoCan.Text = "$0.00"
    '                    End If
    '                Else
    '                    frmPuntoVenta_ARAB.lblTicketPromedioMontoCan.Text = ""
    '                End If
    '            Catch ex As Exception
    '            End Try
    '        End If

    '    End Sub


#End Region

#Region "Pago - Funciones"

    Public Function RealizarModificar_Venta() As Integer
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
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_IdCliente As New SqlClient.SqlParameter
                param_IdCliente.ParameterName = "@IdCliente"
                param_IdCliente.SqlDbType = SqlDbType.VarChar
                param_IdCliente.Size = 25
                param_IdCliente.Value = IIf(txtIdCliente.Text = "", "0", txtIdCliente.Text)
                param_IdCliente.Direction = ParameterDirection.Input

                'Dim param_cae As New SqlClient.SqlParameter
                'param_cae.ParameterName = "@CAE"
                'param_cae.SqlDbType = SqlDbType.VarChar
                'param_cae.Size = 100
                'param_cae.Value = "CAE"
                'param_cae.Direction = ParameterDirection.Input

                Dim param_nombrecf As New SqlClient.SqlParameter
                param_nombrecf.ParameterName = "@NombreCF"
                param_nombrecf.SqlDbType = SqlDbType.VarChar
                param_nombrecf.Size = 30
                param_nombrecf.Value = txtCliente.Text
                param_nombrecf.Direction = ParameterDirection.Input

                Dim param_dnicf As New SqlClient.SqlParameter
                param_dnicf.ParameterName = "@DNICF"
                param_dnicf.SqlDbType = SqlDbType.BigInt
                param_dnicf.Value = IIf(txtCuit.Text = "", 0, txtCuit.Text)
                param_dnicf.Direction = ParameterDirection.Input

                Dim param_direccioncf As New SqlClient.SqlParameter
                param_direccioncf.ParameterName = "@DireccionCF"
                param_direccioncf.SqlDbType = SqlDbType.VarChar
                param_direccioncf.Size = 100
                param_direccioncf.Value = txtClienteDireccion.Text
                param_direccioncf.Direction = ParameterDirection.Input

                Dim param_numvendedor As New SqlClient.SqlParameter
                param_numvendedor.ParameterName = "@NumVendedor"
                param_numvendedor.SqlDbType = SqlDbType.VarChar
                param_numvendedor.Size = 25
                param_numvendedor.Value = lblNumVendedor.Text
                param_numvendedor.Direction = ParameterDirection.Input

                Dim param_tipocomprobante As New SqlClient.SqlParameter
                param_tipocomprobante.ParameterName = "@CodComprobante"
                param_tipocomprobante.SqlDbType = SqlDbType.VarChar
                param_tipocomprobante.Size = 50
                param_tipocomprobante.Value = IIf(txtIDTipoComprobante.Text = "", cmbTipoComprobante.SelectedValue, txtIDTipoComprobante.Text)
                param_tipocomprobante.Direction = ParameterDirection.Input

                Dim param_PtoVta As New SqlClient.SqlParameter
                param_PtoVta.ParameterName = "@PtoVta"
                param_PtoVta.SqlDbType = SqlDbType.BigInt
                param_PtoVta.Value = lblPVI.Text
                param_PtoVta.Direction = ParameterDirection.Input

                Dim codigointerno As String = ""
                Dim param_Codigo As New SqlClient.SqlParameter
                param_Codigo.ParameterName = "@Codigo"
                param_Codigo.SqlDbType = SqlDbType.BigInt
                param_Codigo.Value = DBNull.Value
                param_Codigo.Direction = ParameterDirection.InputOutput

                Dim param_nrofac As New SqlClient.SqlParameter
                param_nrofac.ParameterName = "@NroFac"
                param_nrofac.SqlDbType = SqlDbType.VarChar
                param_nrofac.Size = 20
                param_nrofac.Value = " "
                param_nrofac.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@Fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = Date.Now 'dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_condicioniva As New SqlClient.SqlParameter
                param_condicioniva.ParameterName = "@CondicionIVA"
                param_condicioniva.SqlDbType = SqlDbType.VarChar
                param_condicioniva.Size = 50
                param_condicioniva.Value = IIf(txtIDCondicionIVA.Text = "", cmbCondicionIVA.SelectedValue, txtIDCondicionIVA.Text)
                param_condicioniva.Direction = ParameterDirection.Input

                Dim subtotalcalculado As Double = 0.0
                Dim param_totalsiniva As New SqlClient.SqlParameter
                param_totalsiniva.ParameterName = "@totalsiniva"
                param_totalsiniva.SqlDbType = SqlDbType.Decimal
                param_totalsiniva.Precision = 18
                param_totalsiniva.Scale = 2
                If cmbTipoComprobante.Text = "FACTURAS A" Then
                    param_totalsiniva.Value = txtSubtotalVista.Text
                Else
                    subtotalcalculado = CDbl(txtSubtotalVista.Text) / (1 + MDIPrincipal.iva / 100)
                    param_totalsiniva.Value = FormatNumber(subtotalcalculado, 2)
                End If
                param_totalsiniva.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = MDIPrincipal.iva
                param_iva.Direction = ParameterDirection.Input

                Dim ivacalculado As Double = 0.0
                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@MontoIva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                If cmbTipoComprobante.Text = "FACTURAS A" Then
                    param_montoiva.Value = txtIVAVista.Text
                Else
                    ivacalculado = CDbl(param_totalsiniva.Value) * CDbl(MDIPrincipal.iva / 100)
                    param_montoiva.Value = FormatNumber(ivacalculado, 2)
                End If
                param_montoiva.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@Total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = txtTotalVista.Text
                param_total.Direction = ParameterDirection.Input

                Dim param_totaloriginal As New SqlClient.SqlParameter
                param_totaloriginal.ParameterName = "@TotalOriginal"
                param_totaloriginal.SqlDbType = SqlDbType.Decimal
                param_totaloriginal.Precision = 18
                param_totaloriginal.Scale = 2
                param_totaloriginal.Value = txtTotalOriginal.Text
                param_totaloriginal.Direction = ParameterDirection.Input

                Dim param_contado As New SqlClient.SqlParameter
                param_contado.ParameterName = "@Contado"
                param_contado.SqlDbType = SqlDbType.Bit
                param_contado.Value = chkContado.Checked
                param_contado.Direction = ParameterDirection.Input

                Dim param_montocontado As New SqlClient.SqlParameter
                param_montocontado.ParameterName = "@MontoContado"
                param_montocontado.SqlDbType = SqlDbType.Decimal
                param_montocontado.Precision = 18
                param_montocontado.Scale = 2
                param_montocontado.Value = IIf(txtContado.Text = "", "0", txtContado.Text)
                param_montocontado.Direction = ParameterDirection.Input

                Dim param_tarjeta1 As New SqlClient.SqlParameter
                param_tarjeta1.ParameterName = "@Tarjeta1"
                param_tarjeta1.SqlDbType = SqlDbType.Bit
                param_tarjeta1.Value = chkTarjetas1.Checked
                param_tarjeta1.Direction = ParameterDirection.Input

                Dim param_montotarjeta1 As New SqlClient.SqlParameter
                param_montotarjeta1.ParameterName = "@MontoTarjeta1"
                param_montotarjeta1.SqlDbType = SqlDbType.Decimal
                param_montotarjeta1.Precision = 18
                param_montotarjeta1.Scale = 2
                param_montotarjeta1.Value = txtTarjetas1ImporteFinal.Text
                param_montotarjeta1.Direction = ParameterDirection.Input

                Dim param_tarjeta2 As New SqlClient.SqlParameter
                param_tarjeta2.ParameterName = "@Tarjeta2"
                param_tarjeta2.SqlDbType = SqlDbType.Bit
                param_tarjeta2.Value = chkTarjetas2.Checked
                param_tarjeta2.Direction = ParameterDirection.Input

                Dim param_montotarjeta2 As New SqlClient.SqlParameter
                param_montotarjeta2.ParameterName = "@MontoTarjeta2"
                param_montotarjeta2.SqlDbType = SqlDbType.Decimal
                param_montotarjeta2.Precision = 18
                param_montotarjeta2.Scale = 2
                param_montotarjeta2.Value = txtTarjetas2ImporteFinal.Text
                param_montotarjeta2.Direction = ParameterDirection.Input

                Dim param_debito As New SqlClient.SqlParameter
                param_debito.ParameterName = "@Debito"
                param_debito.SqlDbType = SqlDbType.Bit
                param_debito.Value = 0
                param_debito.Direction = ParameterDirection.Input

                Dim param_montodebito As New SqlClient.SqlParameter
                param_montodebito.ParameterName = "@MontoDebito"
                param_montodebito.SqlDbType = SqlDbType.Decimal
                param_montodebito.Precision = 18
                param_montodebito.Scale = 2
                param_montodebito.Value = "0.00"
                param_montodebito.Direction = ParameterDirection.Input

                Dim param_notacredito As New SqlClient.SqlParameter
                param_notacredito.ParameterName = "@NotaCredito"
                param_notacredito.SqlDbType = SqlDbType.Bit
                param_notacredito.Value = 0
                param_notacredito.Direction = ParameterDirection.Input

                Dim param_montocredito As New SqlClient.SqlParameter
                param_montocredito.ParameterName = "@MontoCredito"
                param_montocredito.SqlDbType = SqlDbType.Decimal
                param_montocredito.Precision = 18
                param_montocredito.Scale = 2
                param_montocredito.Value = 0
                param_montocredito.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@nota"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 300
                param_nota.Value = ""
                param_nota.Direction = ParameterDirection.Input

                Dim param_CtaCte As New SqlClient.SqlParameter
                param_CtaCte.ParameterName = "@CtaCte"
                param_CtaCte.SqlDbType = SqlDbType.Bit
                param_CtaCte.Value = chkCtaCte.Checked
                param_CtaCte.Direction = ParameterDirection.Input

                Dim param_recargo1 As New SqlClient.SqlParameter
                param_recargo1.ParameterName = "@Recargo1"
                param_recargo1.SqlDbType = SqlDbType.Bit
                If chkTarjetas1.Checked Then
                    If porcen1 > 0 Then
                        param_recargo1.Value = 1
                    Else
                        param_recargo1.Value = 0
                    End If
                Else
                    param_recargo1.Value = 0
                End If
                param_recargo1.Direction = ParameterDirection.Input

                Dim param_porcenrecargo1 As New SqlClient.SqlParameter
                param_porcenrecargo1.ParameterName = "@PorcenRecargo1"
                param_porcenrecargo1.SqlDbType = SqlDbType.Decimal
                param_porcenrecargo1.Precision = 18
                param_porcenrecargo1.Scale = 2
                If chkTarjetas1.Checked Then
                    If porcen1 > 0 Then
                        param_porcenrecargo1.Value = porcen1
                    Else
                        param_porcenrecargo1.Value = 0
                    End If
                Else
                    param_porcenrecargo1.Value = 0
                End If
                param_porcenrecargo1.Direction = ParameterDirection.Input

                Dim param_recargo2 As New SqlClient.SqlParameter
                param_recargo2.ParameterName = "@Recargo2"
                param_recargo2.SqlDbType = SqlDbType.Bit
                If chkTarjetas2.Checked Then
                    If porcen2 > 0 Then
                        param_recargo2.Value = 1
                    Else
                        param_recargo2.Value = 0
                    End If
                Else
                    param_recargo2.Value = 0
                End If
                param_recargo2.Direction = ParameterDirection.Input

                Dim param_porcenrecargo2 As New SqlClient.SqlParameter
                param_porcenrecargo2.ParameterName = "@PorcenRecargo2"
                param_porcenrecargo2.SqlDbType = SqlDbType.Decimal
                param_porcenrecargo2.Precision = 18
                param_porcenrecargo2.Scale = 2
                If chkTarjetas2.Checked = True Then
                    If porcen2 > 0 Then
                        param_porcenrecargo2.Value = porcen2
                    Else
                        param_porcenrecargo2.Value = 0
                    End If
                Else
                    param_porcenrecargo2.Value = 0
                End If
                param_porcenrecargo2.Direction = ParameterDirection.Input

                Dim param_descuentoglobal As New SqlClient.SqlParameter
                param_descuentoglobal.ParameterName = "@DescuentoGlobal"
                param_descuentoglobal.SqlDbType = SqlDbType.Bit
                param_descuentoglobal.Value = chkDescuentoGlobal.Checked
                param_descuentoglobal.Direction = ParameterDirection.Input

                Dim param_porcenglobaldesc As New SqlClient.SqlParameter
                param_porcenglobaldesc.ParameterName = "@PorcenGlobalDesc"
                param_porcenglobaldesc.SqlDbType = SqlDbType.Decimal
                param_porcenglobaldesc.Precision = 18
                param_porcenglobaldesc.Scale = 2
                If chkDescuentoGlobal.Checked = True And rdPorcentaje.Checked = True Then
                    param_porcenglobaldesc.Value = txtDescuento.Text
                Else
                    param_porcenglobaldesc.Value = 0
                End If
                param_porcenglobaldesc.Direction = ParameterDirection.Input

                Dim param_montoglobaldesc As New SqlClient.SqlParameter
                param_montoglobaldesc.ParameterName = "@MontoGlobalDesc"
                param_montoglobaldesc.SqlDbType = SqlDbType.Decimal
                param_montoglobaldesc.Precision = 18
                param_montoglobaldesc.Scale = 2
                param_montoglobaldesc.Value = IIf(lblValorDescontado.Text = "", 0, lblValorDescontado.Text)
                param_montoglobaldesc.Direction = ParameterDirection.Input

                Dim param_Seña As New SqlClient.SqlParameter
                param_Seña.ParameterName = "@Seña"
                param_Seña.SqlDbType = SqlDbType.Decimal
                param_Seña.Value = 0
                param_Seña.Direction = ParameterDirection.Input

                Dim param_comprasinterna As New SqlClient.SqlParameter
                param_comprasinterna.ParameterName = "@ComprasInterna"
                param_comprasinterna.SqlDbType = SqlDbType.Bit
                param_comprasinterna.Value = 0
                param_comprasinterna.Direction = ParameterDirection.Input

                Dim param_cancelado As New SqlClient.SqlParameter
                param_cancelado.ParameterName = "@Cancelado"
                param_cancelado.SqlDbType = SqlDbType.Bit
                param_cancelado.Value = Not chkCtaCte.Checked
                param_cancelado.Direction = ParameterDirection.Input

                Dim param_fechacanc As New SqlClient.SqlParameter
                param_fechacanc.ParameterName = "@FechaCanc"
                param_fechacanc.SqlDbType = SqlDbType.DateTime
                If chkCtaCte.Checked Then
                    param_fechacanc.Value = DBNull.Value
                Else
                    param_fechacanc.Value = DateTime.Now
                End If
                param_fechacanc.Direction = ParameterDirection.Input

                Dim param_Deuda As New SqlClient.SqlParameter
                param_Deuda.ParameterName = "@Deuda"
                param_Deuda.SqlDbType = SqlDbType.Decimal
                If chkCtaCte.Checked Then
                    param_Deuda.Value = txtTotalVista.Text
                Else
                    param_Deuda.Value = 0
                End If
                param_Deuda.Direction = ParameterDirection.Input

                Dim param_Cambio As New SqlClient.SqlParameter
                param_Cambio.ParameterName = "@Cambio"
                param_Cambio.SqlDbType = SqlDbType.Int
                param_Cambio.Value = IIf(chkDevolucion.Checked = True, 2, 0)
                param_Cambio.Direction = ParameterDirection.Input

                Dim param_nrofacanu As New SqlClient.SqlParameter
                param_nrofacanu.ParameterName = "@NroFacAnulada"
                param_nrofacanu.SqlDbType = SqlDbType.VarChar
                param_nrofacanu.Size = 20
                param_nrofacanu.Value = "0000-00000000"
                param_nrofacanu.Direction = ParameterDirection.Input

                Dim param_procesado As New SqlClient.SqlParameter
                param_procesado.ParameterName = "@Procesado"
                param_procesado.SqlDbType = SqlDbType.Bit
                param_procesado.Value = 0
                param_procesado.Direction = ParameterDirection.Input

                Dim param_CantArt As New SqlClient.SqlParameter
                param_CantArt.ParameterName = "@CantArt"
                param_CantArt.SqlDbType = SqlDbType.Int
                param_CantArt.Value = lblCantidadFilas.Text
                param_CantArt.Direction = ParameterDirection.Input

                Dim param_montotarjetatotal As New SqlClient.SqlParameter
                param_montotarjetatotal.ParameterName = "@MontoTotalTarj"
                param_montotarjetatotal.SqlDbType = SqlDbType.Decimal
                param_montotarjetatotal.Precision = 18
                param_montotarjetatotal.Scale = 2
                param_montotarjetatotal.Value = FormatNumber(CDbl(txtTarjetas1ImporteFinal.Text) + CDbl(txtTarjetas2ImporteFinal.Text), 2)
                param_montotarjetatotal.Direction = ParameterDirection.Input

                Dim param_dateadd As New SqlClient.SqlParameter
                param_dateadd.ParameterName = "@dateadd"
                param_dateadd.SqlDbType = SqlDbType.DateTime
                param_dateadd.Value = DateTime.Now
                param_dateadd.Direction = ParameterDirection.Input

                Dim param_VentaManual As New SqlClient.SqlParameter
                param_VentaManual.ParameterName = "@VentaManual"
                param_VentaManual.SqlDbType = SqlDbType.Bit
                param_VentaManual.Value = 0
                param_VentaManual.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@useradd"
                param_useradd.SqlDbType = SqlDbType.BigInt
                param_useradd.Value = lblNumVendedor.Text
                param_useradd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spVentas_Salon_Insert", param_id, param_IdCliente, _
                                                               param_nombrecf, param_dnicf, param_numvendedor, param_tipocomprobante, param_PtoVta, _
                                                               param_Codigo, param_nrofac, param_fecha, param_condicioniva, param_direccioncf, _
                                                               param_totalsiniva, param_iva, param_montoiva, param_total, param_totaloriginal, _
                                                               param_contado, param_montocontado, param_tarjeta1, param_montotarjeta1,
                                                               param_tarjeta2, param_montotarjeta2, param_debito, param_montodebito, _
                                                               param_notacredito, param_montocredito, param_nota, param_CtaCte, _
                                                               param_recargo1, param_porcenrecargo1, param_recargo2, param_porcenrecargo2, _
                                                               param_descuentoglobal, param_porcenglobaldesc, param_montoglobaldesc, _
                                                               param_comprasinterna, param_Seña, param_cancelado, param_fechacanc, _
                                                               param_Cambio, param_nrofacanu, param_procesado, param_CantArt, _
                                                               param_montotarjetatotal, param_Deuda, param_VentaManual, _
                                                               param_dateadd, param_useradd, param_res)

                        txtID.Text = param_id.Value
                        lblCodigo.Text = param_Codigo.Value

                        'Else

                        '                       SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMaquinasHerramientas_Mov_Update", param_id, _
                        '                                                param_origen, param_destino, param_empleado, param_fecha, param_nota, param_res)

                    End If

                    RealizarModificar_Venta = param_res.Value

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

    Public Function RealizarModificar_VentaDetalle() As Integer

        Dim res As Integer
        'Dim IdStockMov As Long
        'Dim Stock As Double
        'Dim StockReceptor As Double
        'Dim IdStockMovReceptor As Long

        Try
            Dim i As Integer

            Dim param_resdet As New SqlClient.SqlParameter
            param_resdet.ParameterName = "@res"
            param_resdet.SqlDbType = SqlDbType.Int
            param_resdet.Value = DBNull.Value
            param_resdet.Direction = ParameterDirection.InputOutput

            For i = 0 To grdItems.Rows.Count - 1

                Dim param_IdVenta As New SqlClient.SqlParameter
                param_IdVenta.ParameterName = "@IdVenta"
                param_IdVenta.SqlDbType = SqlDbType.BigInt
                param_IdVenta.Value = txtID.Text
                param_IdVenta.Direction = ParameterDirection.Input

                Dim param_IdAlmacen As New SqlClient.SqlParameter
                param_IdAlmacen.ParameterName = "@IdAlmacen"
                param_IdAlmacen.SqlDbType = SqlDbType.BigInt
                param_IdAlmacen.Value = IDAlmacenSalon 'Utiles.numero_almacen
                param_IdAlmacen.Direction = ParameterDirection.Input

                Dim param_idProducto As New SqlClient.SqlParameter
                param_idProducto.ParameterName = "@CodigoProducto"
                param_idProducto.SqlDbType = SqlDbType.VarChar
                param_idProducto.Size = 50
                param_idProducto.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMaterial).Value
                param_idProducto.Direction = ParameterDirection.Input

                Dim param_cantidad As New SqlClient.SqlParameter
                param_cantidad.ParameterName = "@qty"
                param_cantidad.SqlDbType = SqlDbType.Decimal
                param_cantidad.Precision = 18
                param_cantidad.Scale = 2
                param_cantidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Cantidad).Value
                param_cantidad.Direction = ParameterDirection.Input

                Dim param_idUnidad As New SqlClient.SqlParameter
                param_idUnidad.ParameterName = "@IdUnidad"
                param_idUnidad.SqlDbType = SqlDbType.VarChar
                param_idUnidad.Size = 25
                param_idUnidad.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value
                param_idUnidad.Direction = ParameterDirection.Input

                Dim param_Preciovta As New SqlClient.SqlParameter
                param_Preciovta.ParameterName = "@PrecioVta"
                param_Preciovta.SqlDbType = SqlDbType.Decimal
                param_Preciovta.Precision = 18
                param_Preciovta.Scale = 2
                param_Preciovta.Value = IIf(chkDevolucion.Checked = True, grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVenta).Value * (-1), grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVenta).Value)
                param_Preciovta.Direction = ParameterDirection.Input

                Dim param_PrecioSinIVA As New SqlClient.SqlParameter
                param_PrecioSinIVA.ParameterName = "@PrecioSinIVA"
                param_PrecioSinIVA.SqlDbType = SqlDbType.Decimal
                param_PrecioSinIVA.Precision = 18
                param_PrecioSinIVA.Scale = 2
                param_PrecioSinIVA.Value = IIf(chkDevolucion.Checked = True, grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioSinIVA).Value * (-1), grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioSinIVA).Value)
                param_PrecioSinIVA.Direction = ParameterDirection.Input

                Dim param_Subtotal As New SqlClient.SqlParameter
                param_Subtotal.ParameterName = "@Subtotal"
                param_Subtotal.SqlDbType = SqlDbType.Decimal
                param_Subtotal.Precision = 18
                param_Subtotal.Scale = 2
                param_Subtotal.Value = IIf(chkDevolucion.Checked = True, grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalOrig).Value * (-1), grdItems.Rows(i).Cells(ColumnasDelGridItems.SubtotalOrig).Value)
                param_Subtotal.Direction = ParameterDirection.Input

                Dim param_descuento As New SqlClient.SqlParameter
                param_descuento.ParameterName = "@Descuento"
                param_descuento.SqlDbType = SqlDbType.Decimal
                param_descuento.Precision = 18
                param_descuento.Scale = 2
                param_descuento.Value = IIf(chkDevolucion.Checked = True And grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value > 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value * (-1), grdItems.Rows(i).Cells(ColumnasDelGridItems.Descuento).Value)
                param_descuento.Direction = ParameterDirection.Input

                Dim param_peso As New SqlClient.SqlParameter
                param_peso.ParameterName = "@Peso"
                param_peso.SqlDbType = SqlDbType.Decimal
                param_peso.Precision = 18
                param_peso.Scale = 2
                param_peso.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Peso).Value
                param_peso.Direction = ParameterDirection.Input

                Dim param_Cambio As New SqlClient.SqlParameter
                param_Cambio.ParameterName = "@Cambio"
                param_Cambio.SqlDbType = SqlDbType.Int
                param_Cambio.Value = IIf(chkDevolucion.Checked = True, 2, 0)
                param_Cambio.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@Fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = Date.Now 'dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_codigobarra As New SqlClient.SqlParameter
                param_codigobarra.ParameterName = "@CodigoBarra"
                param_codigobarra.SqlDbType = SqlDbType.VarChar
                param_codigobarra.Size = 50
                param_codigobarra.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.CodigoBarra).Value
                param_codigobarra.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                param_useradd.ParameterName = "@useradd"
                param_useradd.SqlDbType = SqlDbType.BigInt
                param_useradd.Value = lblNumVendedor.Text
                param_useradd.Direction = ParameterDirection.Input

                Dim param_dateadd As New SqlClient.SqlParameter
                param_dateadd.ParameterName = "@dateadd"
                param_dateadd.SqlDbType = SqlDbType.DateTime
                param_dateadd.Value = DateTime.Now
                param_dateadd.Direction = ParameterDirection.Input

                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spVentas_Salon_Det_Insert", _
                                           param_IdVenta, param_IdAlmacen, param_idProducto, param_cantidad, _
                                           param_idUnidad, param_Preciovta, param_PrecioSinIVA, param_Subtotal, param_descuento, _
                                           param_peso, param_Cambio, param_fecha, param_codigobarra, param_useradd, param_dateadd, param_resdet)

                res = param_resdet.Value

            Next

            RealizarModificar_VentaDetalle = 1

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

    Private Function RealizarModificar_VentaTarjeta() As Integer

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_idventa As New SqlClient.SqlParameter
            param_idventa.ParameterName = "@idVenta"
            param_idventa.SqlDbType = SqlDbType.BigInt
            param_idventa.Value = txtID.Text
            param_idventa.Direction = ParameterDirection.Input

            Dim param_idtarjeta1 As New SqlClient.SqlParameter
            param_idtarjeta1.ParameterName = "@CodTarjeta1"
            param_idtarjeta1.SqlDbType = SqlDbType.VarChar
            param_idtarjeta1.Size = 50
            If chkTarjetas1.Checked = True Then
                If CodigoTarjeta1 <> "" Then
                    param_idtarjeta1.Value = CodigoTarjeta1
                Else
                    param_idtarjeta1.Value = "0"
                End If
            Else
                param_idtarjeta1.Value = "0"
            End If
            param_idtarjeta1.Direction = ParameterDirection.Input

            Dim param_plantarjeta1 As New SqlClient.SqlParameter
            param_plantarjeta1.ParameterName = "@PlanTarjeta1"
            param_plantarjeta1.SqlDbType = SqlDbType.VarChar
            param_plantarjeta1.Size = 300
            'paso cero por el momento
            param_plantarjeta1.Value = txtTarjeta1.Text
            param_plantarjeta1.Direction = ParameterDirection.Input

            Dim param_cuotas1 As New SqlClient.SqlParameter
            param_cuotas1.ParameterName = "@Cuotas1"
            param_cuotas1.SqlDbType = SqlDbType.Int
            If cuotas1 > 0 Then
                param_cuotas1.Value = CInt(cuotas1)
            Else
                param_cuotas1.Value = 0
            End If
            param_cuotas1.Direction = ParameterDirection.Input

            Dim param_montotarjeta1 As New SqlClient.SqlParameter
            param_montotarjeta1.ParameterName = "@MontoTarjeta1"
            param_montotarjeta1.SqlDbType = SqlDbType.Decimal
            param_montotarjeta1.Precision = 18
            param_montotarjeta1.Scale = 2
            param_montotarjeta1.Value = txtTarjetas1Importe.Text
            param_montotarjeta1.Direction = ParameterDirection.Input

            Dim param_porcenrecar1 As New SqlClient.SqlParameter
            param_porcenrecar1.ParameterName = "@PorcenRecar1"
            param_porcenrecar1.SqlDbType = SqlDbType.Decimal
            param_porcenrecar1.Precision = 18
            param_porcenrecar1.Scale = 2
            If chkTarjetas1.Checked = True Then
                If porcen1 > 0 Then
                    param_porcenrecar1.Value = porcen1
                Else
                    param_porcenrecar1.Value = 0
                End If
            Else
                param_porcenrecar1.Value = 0
            End If
            param_porcenrecar1.Direction = ParameterDirection.Input

            Dim param_montotarjeta1final As New SqlClient.SqlParameter
            param_montotarjeta1final.ParameterName = "@MontoTarjeta1Final"
            param_montotarjeta1final.SqlDbType = SqlDbType.Decimal
            param_montotarjeta1final.Precision = 18
            param_montotarjeta1final.Scale = 2
            param_montotarjeta1final.Value = txtTarjetas1ImporteFinal.Text
            param_montotarjeta1final.Direction = ParameterDirection.Input

            Dim param_idtarjeta2 As New SqlClient.SqlParameter
            param_idtarjeta2.ParameterName = "@CodTarjeta2"
            param_idtarjeta2.SqlDbType = SqlDbType.VarChar
            param_idtarjeta2.Size = 50
            If chkTarjetas2.Checked = True Then
                If CodigoTarjeta2 <> "" Then
                    param_idtarjeta2.Value = CodigoTarjeta2
                Else
                    param_idtarjeta2.Value = "0"
                End If
            Else
                param_idtarjeta2.Value = "0"
            End If
            param_idtarjeta2.Direction = ParameterDirection.Input

            Dim param_plantarjeta2 As New SqlClient.SqlParameter
            param_plantarjeta2.ParameterName = "@PlanTarjeta2"
            param_plantarjeta2.SqlDbType = SqlDbType.VarChar
            param_plantarjeta2.Size = 300
            'paso cero por el momento
            param_plantarjeta2.Value = txtTarjeta2.Text
            param_plantarjeta2.Direction = ParameterDirection.Input

            Dim param_cuotas2 As New SqlClient.SqlParameter
            param_cuotas2.ParameterName = "@Cuotas2"
            param_cuotas2.SqlDbType = SqlDbType.Int
            If cuotas2 > 0 Then
                param_cuotas2.Value = CInt(cuotas2)
            Else
                param_cuotas2.Value = 0
            End If
            param_cuotas2.Direction = ParameterDirection.Input

            Dim param_montotarjeta2 As New SqlClient.SqlParameter
            param_montotarjeta2.ParameterName = "@MontoTarjeta2"
            param_montotarjeta2.SqlDbType = SqlDbType.Decimal
            param_montotarjeta2.Precision = 18
            param_montotarjeta2.Scale = 2
            param_montotarjeta2.Value = txtTarjetas2Importe.Text
            param_montotarjeta2.Direction = ParameterDirection.Input

            Dim param_porcenrecar2 As New SqlClient.SqlParameter
            param_porcenrecar2.ParameterName = "@PorcenRecar2"
            param_porcenrecar2.SqlDbType = SqlDbType.Decimal
            param_porcenrecar2.Precision = 18
            param_porcenrecar2.Scale = 2
            If chkTarjetas2.Checked = True Then
                If porcen2 > 0 Then
                    param_porcenrecar2.Value = porcen2
                Else
                    param_porcenrecar2.Value = 0
                End If
            Else
                param_porcenrecar2.Value = 0
            End If
            param_porcenrecar2.Direction = ParameterDirection.Input

            Dim param_montotarjeta2final As New SqlClient.SqlParameter
            param_montotarjeta2final.ParameterName = "@MontoTarjeta2Final"
            param_montotarjeta2final.SqlDbType = SqlDbType.Decimal
            param_montotarjeta2final.Precision = 18
            param_montotarjeta2final.Scale = 2
            param_montotarjeta2final.Value = txtTarjetas2ImporteFinal.Text
            param_montotarjeta2final.Direction = ParameterDirection.Input

            Dim param_dateadd As New SqlClient.SqlParameter
            param_dateadd.ParameterName = "@dateadd"
            param_dateadd.SqlDbType = SqlDbType.DateTime
            param_dateadd.Value = DateTime.Now
            param_dateadd.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput



            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spVentas_PagoTarjetas_Insert", _
                                 param_id, param_idventa, param_idtarjeta1, param_plantarjeta1, param_cuotas1, _
                                 param_montotarjeta1, param_porcenrecar1, param_montotarjeta1final,
                                 param_idtarjeta2, param_plantarjeta2, param_cuotas2, _
                                 param_montotarjeta2, param_porcenrecar2, param_montotarjeta2final, _
                                 param_dateadd, param_res)

            RealizarModificar_VentaTarjeta = param_res.Value
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'Finally


            'End Try


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

    Public Function Acomodar_Texto(ByVal parametro_txt As String) As String

        Dim cadena_texto() As Char = parametro_txt.ToCharArray
        Dim parametro_texto As String = parametro_txt
        Dim parametro_tamaño As Integer = parametro_txt.Length
        ' Dim parametro_tamaño2 As Integer
        Dim dif As Integer
        ' Dim dif2 As Integer
        Dim texto As String = ""
        Dim texto_tamaño As Integer
        Dim texto_final1 As String = ""
        ' Dim texto_final2 As String = ""
        Dim contador As Integer = 0

        For i As Integer = 0 To cadena_texto.Length - 1
            texto = texto + cadena_texto(i).ToString
            texto_tamaño = texto.ToString.Length
            If cadena_texto(i) = " " Then
                contador = contador + 1
                If contador = 3 And texto_tamaño < 40 Then
                    If texto_tamaño < 40 Then
                        Try
                            texto_final1 = texto.ToString
                            texto_tamaño = texto_final1.Length
                            ' parametro_tamaño2 = parametro_tamaño - texto_tamaño
                            ' texto_final2 = parametro_texto.Substring(texto_tamaño, parametro_tamaño2)
                            Exit For
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Else
                        Try
                            dif = parametro_tamaño / 2
                            texto_tamaño = dif
                            'dif2 = parametro_tamaño - dif
                            ' texto_final1 = parametro_texto.Substring(0, dif)
                            'texto_tamaño = texto_final1.Length
                            'texto_final2 = parametro_texto.Substring(dif, dif2)
                            Exit For
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    End If
                End If
            End If
        Next

        'Linea2DomiComprador = texto_final1
        'Linea3DomiComprador = texto_final2

        Return texto_tamaño

    End Function

    Public Function ConexionAfip(ByVal TicketAcceso As Object, ByVal Token As Object, ByVal Sign As Object) As Boolean

        'FACTURA ELECTRONICA
        '
        ' Crear objeto interface Web Service Autenticación y Autorización
        Try
            WSAA = CreateObject("WSAA")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            ' cargar ticket de acceso previo (si no se mantiene WSAA instanciado en memoria)
            If TicketAcceso <> "" Then
                ok = WSAA.AnalizarXml(TicketAcceso)
            End If


            ' Procedimiento para autenticar con AFIP y reutilizar el ticket de acceso
            ' Llamar antes de utilizar WSAA.Token y WSAA.Sign (WSAA debe estar definido a nivel de módulo)
            Dim solicitar

            ' revisar si el ticket es válido y no ha expirado:
            expiracion = WSAA.ObtenerTagXml("expirationTime")
            Debug.Print("Fecha Expiracion ticket: ", expiracion)
            If IsDBNull(expiracion) Then
                solicitar = True                           ' solicitud inicial
            Else
                solicitar = WSAA.Expirado(expiracion)      ' chequear solicitud previa
            End If

            'Reutilizacion de TA 
            If solicitar Then

                tra = WSAA.CreateTRA("wsfe")

                Path = Application.StartupPath + "\AFIP\"
                ' Certificado: certificado es el firmado por la AFIP
                ' ClavePrivada: la clave privada usada para crear el certificado
                If HOMO = True Then
                    certificado = Empresa + "_homo.crt"
                    claveprivada = Empresa + "_homo.key"
                Else
                    certificado = Empresa + ".crt"
                    claveprivada = Empresa + ".key"
                End If

                cms = WSAA.SignTRA(tra, Path + certificado, Path + claveprivada)

                cache = ""
                proxy = ""

                If HOMO = True Then
                    wsdl = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms?wsdl"
                Else
                    wsdl = "https://wsaa.afip.gov.ar/ws/services/LoginCms?wsdl"

                End If

                WSAA.Conectar(cache, wsdl, proxy)
                'ta = WSAA.LoginCMS(cms)
                saveTA = WSAA.LoginCMS(cms)

                ' Obtener las credenciales del ticket de acceso (desde el XML por si no se conserva el objeto WSAA)
                saveTOKEN = WSAA.ObtenerTagXml("token")
                saveSING = WSAA.ObtenerTagXml("sign")

                Dim connection As SqlClient.SqlConnection = Nothing
                Dim ds As Data.DataSet

                Try
                    connection = SqlHelper.GetConnection(ConnStringSEI)
                Catch ex As Exception
                    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End Try

                Try
                    ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Parametros set TicketAcceso = '" & saveTA & "', Token = '" & saveTOKEN & "', Sign = '" & saveSING & "'")
                    ds.Dispose()
                Catch ex As Exception

                End Try


            Else
                Debug.Print("no expirado!", "Reutilizando!")
            End If



            ' Al retornar se puede utilizar token y sign para WSFEv1 o similar
            ' Devuelvo el ticket de acceso (RETURN) para que el programa principal lo almacene si es necesario:

            'MessageBox.Show(ta, "Ticket de Acceso")
            'If TicketAcceso = True Then
            '    MsgBox(ta.ToString)
            'End If

        Catch
            ' Muestro los errores
            If WSAA.Excepcion <> "" Then
                MsgBox(WSAA.Traceback, vbExclamation, WSAA.Excepcion)
                MsgBox(WSAA.Excepcion, vbExclamation, WSAA.Excepcion)
                ConexionAfip = False
                Exit Function
                'Me.Close()
            End If
        End Try

        wsfev1 = CreateObject("WSFEv1")
        wsfev1.LanzarExcepciones = False

        Try

            ' Setear tocken y sing de autorización (pasos previos)
            'wsfev1.Token = WSAA.Token
            'wsfev1.Sign = WSAA.Sign

            'Token = WSAA.Token
            'Sign = WSAA.Sign

            wsfev1.Token = saveTOKEN
            wsfev1.Sign = saveSING

            'Select Case Empresa
            '    Case "ACER"
            '        wsfev1.Cuit = "20146486569" 'homo 20146486569 prod 30708425733
            '    Case "ANCOA"
            '        wsfev1.Cuit = "30673525845" 'homo 20923232371 prod 30673525845
            '    Case "ALBERTO"
            '        wsfev1.Cuit = "20291813128"
            'End Select
            ' CUIT del emisor (debe estar registrado en la AFIP)
            wsfev1.Cuit = cuitEmpresa

            ' Conectar al Servicio Web de Facturación
            proxy = "" ' "usuario:clave@localhost:8000"

            If HOMO = True Then
                wsdl = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx?WSDL"
            Else
                wsdl = "https://servicios1.afip.gov.ar/wsfev1/service.asmx?WSDL"
            End If

            cache = "" 'Path

            ok = wsfev1.Conectar(cache, wsdl, proxy) ' homologación


            REM ' Llamo a un servicio nulo, para obtener el estado del servidor (opcional)
            Try
                wsfev1.Dummy()
                PicConexion.Image = My.Resources.Green_Ball_icon
            Catch ex As Exception
                PicConexion.Image = My.Resources.Red_Ball_icon
                ConexionAfip = False
                Exit Function
            End Try

            If wsfev1.ErrMsg <> "" Then
                MsgBox(wsfev1.ErrMsg, vbExclamation, "Errores")
                ConexionAfip = False
                Exit Function
            End If

            If wsfev1.Obs <> "" Then
                MsgBox(wsfev1.Obs, vbExclamation, "Observaciones")
                ConexionAfip = False
                Exit Function
            End If

        Catch

            ' Muestro los errores
            If wsfev1.Traceback <> "" Then
                MsgBox(wsfev1.Traceback, vbExclamation, "Error")
                ConexionAfip = False
                Exit Function
            End If

        End Try

        ConexionAfip = True

    End Function

    Public Function GenerarFE(sender As Object, e As EventArgs, ByVal tipo_comprobante As Integer, ByVal punto_venta As Integer, ByVal tipo_documento As Integer, ByVal num_documento As String, ByVal import_iva As String, ByVal subtotal As String, ByVal total As String, ByVal concept As Integer, ByVal IdFactura As Long) As Boolean
        Try


            Dim CaeGenerado As String
            Dim FechaGenerado As String



            tipo_cbte = tipo_comprobante
            ' cbte_nro = "" 'param

            punto_vta = punto_venta 'param

            fecha = Format(dtpFECHA.Value.Date, "yyyyMMdd") 'param
            'concepto en este caso es siempre producto
            concepto = concept 'param
            tipo_doc = tipo_documento 'param
            nro_doc = num_documento 'param

            imp_trib = "0.00"
            imp_op_ex = "0.00"

            'Verifico nro de comp y aumento si es necesario

            cbte_nro = wsfev1.CompUltimoAutorizado(tipo_cbte, punto_vta)

            If cbte_nro = "" Then
                cbte_nro = 0                ' no hay comprobantes emitidos
            Else
                cbte_nro = CLng(cbte_nro) ' convertir a entero largo
            End If


            nroFactura = cbte_nro + 1 'Format(cbte_nro + 1, "0000000000")

            'nro de factura 
            cbt_desde = nroFactura 'param
            cbt_hasta = nroFactura 'param

            ', ByVal num_factura As Integer


            'If cmbTipoComprobante.SelectedValue = TipoComp.FacturaA Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaDebitoA Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaCreditoA Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.FacturaM Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaDebitoM Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaCreditoM Then

            'Codigos correspondientes a la base de datos

            ' 1 Factura A
            ' 2 Nota Debito A
            ' 3 Nota Credito A
            ' 51 Factura M
            ' 52 Nota Debito M 
            ' 53 Nota Credito M

            If tipo_cbte = TipoComp.FacturaA Or _
                tipo_cbte = TipoComp.NotaDebitoA Or _
                tipo_cbte = TipoComp.NotaCreditoA Or _
                tipo_cbte = TipoComp.FacturaM Or _
                tipo_cbte = TipoComp.NotaDebitoM Or _
                tipo_cbte = TipoComp.NotaCreditoM Then

                imp_tot_conc = "0.00"
                imp_neto = FormatNumber(CDec(subtotal), 2) 'param
                imp_neto = Replace(Replace(imp_neto, ".", ""), ",", ".")
            End If

            'If cmbTipoComprobante.SelectedValue = TipoComp.FacturaB Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaDebitoB Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaCreditoB Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.FacturaC Or _
            '    cmbTipoComprobante.SelectedValue = TipoComp.NotaDebitoC Or _
            '     cmbTipoComprobante.SelectedValue = TipoComp.NotaCreditoC Then

            ' 6 Factura B
            ' 7 Nota debito B
            ' 8 Nota credito B
            ' 11 Factura C
            ' 12 Nota debito C
            ' 13 Nota crecito C

            If tipo_cbte = TipoComp.FacturaB Or _
                tipo_cbte = TipoComp.NotaDebitoB Or _
                tipo_cbte = TipoComp.NotaDebitoB Or _
                tipo_cbte = TipoComp.FacturaC Or _
                tipo_cbte = TipoComp.NotaDebitoC Or _
             tipo_cbte = TipoComp.NotaCreditoC Then

                imp_tot_conc = FormatNumber(CDbl(subtotal), 2) 'param
                imp_tot_conc = Replace(Replace(imp_tot_conc, ".", ""), ",", ".")
                imp_neto = "0.00"
            End If

            'imp_iva = FormatNumber(CDec(txtIva21.Text) + CDec(txtIva10.Text), 2) 'param
            'imp_iva = Replace(Replace(imp_iva, ".", ""), ",", ".")
            imp_iva = FormatNumber(CDec(import_iva), 2) 'param
            imp_iva = Replace(Replace(imp_iva, ".", ""), ",", ".")

            'imp_total = FormatNumber(CDbl(txtTotal.Text), 2) 'param
            'imp_total = Replace(Replace(imp_total, ".", ""), ",", ".")
            imp_total = FormatNumber(CDbl(total), 2) 'param
            imp_total = Replace(Replace(imp_total, ".", ""), ",", ".")

            fecha_cbte = Format(dtpFECHA.Value.Date, "yyyyMMdd")

            ' Fechas del período del servicio facturado (solo si concepto = 1?)
            'If CInt(cmbConceptos.SelectedValue) = 2 Or CInt(cmbConceptos.SelectedValue) = 3 Then
            '    If TipoComp.NotaCreditoA Or TipoComp.NotaCreditoB Or TipoComp.NotaDebitoA Or TipoComp.NotaDebitoB Or _
            '        TipoComp.NotaDebitoM Or TipoComp.NotaDebitoM Then
            '        fecha_venc_pago = Format(Today.Date, "yyyyMMdd")
            '    Else
            'fecha_venc_pago = Format(dtpVtoPago.Value.Date, "yyyyMMdd")
            '    End If
            'fecha_serv_desde = Format(dtpDesde.Value.Date, "yyyyMMdd")
            'fecha_serv_hasta = Format(dtpHasta.Value.Date, "yyyyMMdd")
            'Else
            'fecha_venc_pago = ""
            'fecha_serv_desde = ""
            'fecha_serv_hasta = ""
            'End If
            'Como el concepto siempre sera 1 fecha_venc_pago siempre sera 0
            fecha_venc_pago = ""

            'verificar variables
            fecha_serv_desde = ""
            fecha_serv_hasta = ""


            moneda_id = "PES" : moneda_ctz = "1.000"




            ok = wsfev1.CrearFactura(concepto, tipo_doc, nro_doc, tipo_cbte, punto_vta, _
                    cbt_desde, cbt_hasta, imp_total, imp_tot_conc, imp_neto, _
                    imp_iva, imp_trib, imp_op_ex, fecha_cbte, fecha_venc_pago, _
                    fecha_serv_desde, fecha_serv_hasta, _
                    moneda_id, moneda_ctz)

            'Dim i As Integer
            'Dim CantidadFilas As Integer

            'If grdItems.RowCount = 16 Then
            '    CantidadFilas = grdItems.Rows.Count
            'Else
            '    CantidadFilas = grdItems.Rows.Count
            'End If

            'Dim total10 As Decimal
            Dim total21 As Decimal
            'total21 toma el valor del subtotal.
            total21 = subtotal

            ' 1 Factura A
            ' 2 Nota Debito A
            ' 3 Nota Credito A
            ' 51 Factura M
            ' 52 Nota Debito M 
            ' 53 Nota Credito M

            If tipo_cbte = TipoComp.FacturaA Or _
                tipo_cbte = TipoComp.NotaDebitoA Or _
                tipo_cbte = TipoComp.NotaCreditoA Or _
                tipo_cbte = TipoComp.FacturaM Or _
                tipo_cbte = TipoComp.NotaDebitoM Or _
                tipo_cbte = TipoComp.NotaCreditoM Then

                '' Agrego tasas de IVA 
                'i = 0
                'Do While i < CantidadFilas
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.iva).Value = "10,5" Then
                '        total10 += grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value
                '    End If
                '    If grdItems.Rows(i).Cells(ColumnasDelGridItems.iva).Value = "21,0" Then
                '        total21 += grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value
                '    End If
                '    i += 1
                'Loop

                'If total10 > 0 Then
                '    idIVA = 4 ' 10.5%
                '    base_imp = FormatNumber(CDbl(total10.ToString), 2) 'param
                '    base_imp = Replace(Replace(base_imp, ".", ""), ",", ".")
                '    importe = FormatNumber(CDbl(txtIva10.Text), 2) 'param
                '    importe = Replace(Replace(importe, ".", ""), ",", ".")
                '    ok = wsfev1.AgregarIva(idIVA, base_imp, importe)
                'End If

                'total21 es el total con iva. el iva esta sin discriminar
                '------------------------DESCOMENTAR SI ES NECESARIO--------------
                '    If total21 > 0 Then
                '        idIVA = 5 ' 21%
                '        base_imp = FormatNumber(CDbl(total21.ToString), 2) 'param
                '        base_imp = Replace(Replace(base_imp, ".", ""), ",", ".")
                '        importe = FormatNumber(CDbl(txtIva21.Text), 2) 'param
                '        importe = Replace(Replace(importe, ".", ""), ",", ".")
                '        ok = wsfev1.AgregarIva(idIVA, base_imp, importe)
                '    End If
                '--------------------------------------------------------------------

                '-----------------CODIGO ANTERIOR MODIFICADO----------------
                If total21 > 0 Then
                    idIVA = 5 '21% codigo que esta en la tabla condicionIVA
                    base_imp = FormatNumber(CDbl(total21.ToString), 2) 'param
                    'base_imp = FormatNumber(CDbl(total21), 2) 'param
                    base_imp = Replace(Replace(base_imp, ".", ""), ",", ".")
                    'importe = FormatNumber(CDbl(imp_iva.ToString), 2) 'param
                    importe = FormatNumber(CDbl(import_iva.ToString), 2) 'param
                    'importe = FormatNumber(CDbl(imp_iva), 2) 'param
                    'importe = imp_iva 'param
                    importe = Replace(Replace(importe, ".", ""), ",", ".")
                    'importe = Replace(importe, ",", ".")
                    ok = wsfev1.AgregarIva(idIVA, base_imp, importe)
                End If
                '------------------------------------------------------------------------
            End If

            ' Agrego los comprobantes asociados: ' solo nc/nd
            'If CInt(tipo_cbte) = "2" Or _
            '    CInt(tipo_cbte) = "3" Or _
            '    CInt(tipo_cbte) = "7" Or _
            '    CInt(tipo_cbte) = "8" Or _
            '    CInt(tipo_cbte) = "52" Or _
            '    CInt(tipo_cbte) = "53" Then
            '    'cambiar la variable que sigue
            '    'tipo toma el valor del nro del tipo de comprobante
            '    'tipo = CInt(CmbComprobantes.SelectedValue)
            '    tipo = tipo_cbte
            '    'pto_vta = PTOVTA
            '    pto_vta = punto_venta
            '    'NroFactura
            '    'nro = CInt(CmbComprobantes.Text)
            '    'nro = num_factura
            '    ok = wsfev1.AgregarCmpAsoc(tipo, pto_vta, nro)
            'End If


            '-----------------------------------------------------------------------------
            'If CInt(cmbTipoComprobante.SelectedValue) = TipoComp.NotaDebitoA Or _
            '    CInt(cmbTipoComprobante.SelectedValue) = TipoComp.NotaCreditoA Or _
            '    CInt(cmbTipoComprobante.SelectedValue) = TipoComp.NotaDebitoB Or _
            '    CInt(cmbTipoComprobante.SelectedValue) = TipoComp.NotaCreditoB Or _
            '    CInt(cmbTipoComprobante.SelectedValue) = TipoComp.NotaDebitoM Or _
            '    CInt(cmbTipoComprobante.SelectedValue) = TipoComp.NotaCreditoM Then
            '    tipo = CInt(CmbComprobantes.SelectedValue)
            '    pto_vta = PTOVTA
            '    nro = CInt(CmbComprobantes.Text)
            '    ok = wsfev1.AgregarCmpAsoc(tipo, pto_vta, nro)
            'End If

            ' Habilito reprocesamiento automático (predeterminado):
            wsfev1.Reprocesar = True

            ' Solicito CAE:
            cae = wsfev1.CAESolicitar()


            Debug.Print("Resultado", wsfev1.Resultado)
            Debug.Print("CAE", wsfev1.CAE)
            Debug.Print("Numero de comprobante:", wsfev1.CbteNro)
            '------------------------------------------------
            'Dim strCadena As String
            'strCadena = wsfev1.f1GuardarTicketAcceso()
            'guardar strCadena en un archivo temporal
            'wsfev1.f1RestaurarTicketAcceso(strCadena)
            '-----------------------------------------------

            'Retorno valor de ok, booleano (comprobar) 
            'Return ok


            If wsfev1.Resultado = "A" Then

                CaeGenerado = wsfev1.CAE.ToString
                FechaGenerado = wsfev1.Vencimiento.ToString

                'ContinuarFactura:
                'Cierro la Transaccion para guardar la Venta para luego realizar el update
                Cerrar_Tran()

                MsgBox("Factura Aceptada" + Chr(13) + "CAE: " + wsfev1.CAE.ToString + Chr(13) + "Vencimiento: " + wsfev1.Vencimiento.ToString)

                Dim CodigoBarra As String
                CodigoBarra = cuitEmpresa.ToString + cmbTipoComprobante.SelectedValue.ToString.PadLeft(2, "00").ToString + punto_vta + CaeGenerado + FechaGenerado
                CodigoBarra = DigitoVerificador(CodigoBarra)
                Select Case ActualizarFacturacion_FEAFIP(wsfev1.CAE.ToString, wsfev1.Vencimiento.ToString, CodigoBarra, IdFactura, tipo_comprobante)
                    Case Is <= 0
                        MessageBox.Show("Se produjo un error al insertar el CAE y el vencimiento en el sistema local.", "Control de errores", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select

                'Imprimir(nroFactura, cmbTipoComprobante.Text.ToString)

                ValorCae = wsfev1.CAE.ToString
                ValorFac = PTOVTA.ToString.PadLeft(4, "0000") + "-" + nroFactura.ToString.PadLeft(8, "00000000")
                Dim Fecha As String = wsfev1.Vencimiento.ToString
                Fecha = Fecha.Substring(6, 2) + "/" + Fecha.Substring(4, 2) + "/" + Fecha.Substring(0, 4)
                ValorVen = Fecha

                'cmbTipoComprobante_SelectedIndexChanged(sender, e)

                'band = 0
                'bolModo = False
                'btnActualizar_Click(sender, e)

                'chkEnviarCorreo.Enabled = True

                'SQL = "exec sp_Consumos_Select_All @eliminado = 0"
                'LlenarGrilla()

                'CalcularTotales()

                'grdItems.Enabled = bolModo
                'btnGuardar.Enabled = bolModo
                'Util.MsgStatus(Status1, "El comprobante se generó correctamente.", My.Resources.Resources.ok.ToBitmap)
                'band = 1

                'GestionDePaneles()

                'btnPrevisualizar.Enabled = bolModo
                GenerarFE = True
            Else
                Cancelar_Tran()

                If WSAA.Excepcion <> "" Then
                    ' muestro al usuario solo el mensaje de error, no la traza:
                    MsgBox(WSAA.Excepcion, vbCritical, "Excepción")
                End If

                'Error/obs
                If wsfev1.ErrMsg <> "" Then
                    MsgBox(wsfev1.ErrMsg, vbExclamation, "Errores")
                End If

                If wsfev1.Obs <> "" Then
                    MsgBox(wsfev1.Obs, vbExclamation, "Observaciones")
                End If

                GenerarFE = False
                Exit Function

            End If

            'btnConfirmarPago.Enabled = True


        Catch ex As Exception

            GenerarFE = False

        End Try

    End Function

    Public Function ActualizarFacturacion_FEAFIP(ByVal numeroCAE As String, ByVal vtoCAE As String, ByVal CodigoBarra As String, ByVal IdFactura As Long, ByVal CodComprobante As Integer) As Integer
        Dim res As Integer = 0

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try
            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@idFacturacion"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = IdFactura
                param_id.Direction = ParameterDirection.Input

                Dim param_cae As New SqlClient.SqlParameter
                param_cae.ParameterName = "@cae"
                param_cae.SqlDbType = SqlDbType.VarChar
                param_cae.Size = 50
                param_cae.Value = numeroCAE
                param_cae.Direction = ParameterDirection.Input

                Dim param_Venc_CAE As New SqlClient.SqlParameter
                param_Venc_CAE.ParameterName = "@Venc_CAE"
                param_Venc_CAE.SqlDbType = SqlDbType.VarChar
                param_Venc_CAE.Size = 10
                param_Venc_CAE.Value = vtoCAE
                param_Venc_CAE.Direction = ParameterDirection.Input

                Dim param_CodigoBarra As New SqlClient.SqlParameter
                param_CodigoBarra.ParameterName = "@CodigoBarra"
                param_CodigoBarra.SqlDbType = SqlDbType.VarChar
                param_CodigoBarra.Size = 100
                param_CodigoBarra.Value = CodigoBarra
                param_CodigoBarra.Direction = ParameterDirection.Input

                Dim param_ComprobanteNro As New SqlClient.SqlParameter
                param_ComprobanteNro.ParameterName = "@ComprobanteNro"
                param_ComprobanteNro.SqlDbType = SqlDbType.BigInt
                param_ComprobanteNro.Value = nroFactura
                param_ComprobanteNro.Direction = ParameterDirection.Input

                Dim param_ComprobanteTipo As New SqlClient.SqlParameter
                param_ComprobanteTipo.ParameterName = "@ComprobanteTipo"
                param_ComprobanteTipo.SqlDbType = SqlDbType.Int
                param_ComprobanteTipo.Value = CodComprobante
                param_ComprobanteTipo.Direction = ParameterDirection.Input

                Dim param_ConceptoTipo As New SqlClient.SqlParameter
                param_ConceptoTipo.ParameterName = "@ConceptoTipo"
                param_ConceptoTipo.SqlDbType = SqlDbType.Int
                param_ConceptoTipo.Value = 1
                param_ConceptoTipo.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spVentas_Salon_Update_FEAFIP", _
                                              param_id, param_cae, param_Venc_CAE, param_CodigoBarra, param_ComprobanteNro, param_ComprobanteTipo, param_ConceptoTipo, param_res)

                    res = param_res.Value

                Catch ex As Exception
                    Throw ex
                End Try

                ActualizarFacturacion_FEAFIP = res

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
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

#End Region


#End Region












End Class