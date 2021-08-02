Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet
Imports System.Threading

Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO


Public Class frmPresupuestos

    Dim permitir_evento_CellChanged As Boolean

    Dim llenandoCombo As Boolean = False
    Dim bolpoliticas As Boolean

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

    Dim conexionGenerica As SqlClient.SqlConnection = Nothing

    Private ht As New System.Collections.Hashtable() 'usada para almacenar el idCliente de la tabla proyectos

    Dim variableajuste As Integer '= 100
    Dim EsOfertaComercial As Boolean
    Dim EsTrafo As Boolean

    Public UltBusqueda As String = ""

    Public Band_RecDesc As Boolean

    Dim bandTrafo As Integer = 0

    Public codigoDesdeLista As String

    'Variables destinadas para cuando se viene una de una Orden de Compra Abierta
    Public Origen_Presupuesto As Integer

    Dim variableOT As Integer = 220

    '0 Presupuesto Normal
    '1 OCA
    '2 OC

    Public Origen As Integer = 0
    Public Origen_IdCliente As Long
    Public Origen_Cliente As String
    Public Origen_Codigo As String
    Public Origen_Id As Long
    Public Origen_OC As String
    Public GuardarDesdeTimer As Boolean = True

    Private trd As Thread

    Public lista(100, 2) As String
    Public cantidadItemsTablero As Integer

    Dim btnBand_Copiar As Boolean = True

    'Dim CodMonedaPres As String
    'Dim IdMonedaPres As Long
    Public ValorCambioPres As Double
    Dim ValorcambioDolar As Double

    Dim CodMonedaOrig As String

    Dim AbrirWord As Boolean

    'Dim ValorCambioDO As Decimal

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    Enum ColumnasDelGridItems
        IdPresup_Det = 0
        IDMaterial = 1
        Cod_Material = 2
        Cod_Material_Prov = 3
        Nombre_Material = 4
        IDUnidad = 5
        Cod_Unidad = 6
        Unidad = 7
        Minimo = 8
        Maximo = 9
        Stock = 10
        IdMoneda = 11
        CodMoneda = 12
        ValorCambio = 13
        PrecioLista = 14
        PrecioVta = 15
        Iva = 16
        MontoIva = 17
        Ganancia = 18
        Bonificacion1 = 19
        Bonificacion2 = 20
        PorcRecDesc = 21
        Qty = 22
        SubTotalProd = 23
        dateupd = 24
        preciovtaorig = 25
        gananciaorig = 26
        PlazoEntrega = 27
        IdProveedor = 28
        NombreProveedor = 29
        IdMarca = 30
        Marca = 31
        nota_det = 32
        orden = 33
        idmonedaorig = 34
        marcanueva = 35
        Ubicacion = 36
    End Enum

    Enum ColumnasDelGrid
        Id_p = 0
        Codigo_p = 1
        Revision_p = 2
        Fecha_p = 3
        Nombre_p = 4
        IdCliente_p = 5
        Cliente_p = 6
        Comprador_p = 7
        IdComprador_p = 8
        NombreComprador_p = 9
        Usuario_p = 10
        IdUsuario_p = 11
        NombreUsuario_p = 12

        SubtotalDO_p = 13
        Iva21DO_p = 14
        Iva10DO_p = 15
        TotalDO_p = 16

        subtotalPre = 17
        Iva21Pre = 18
        Iva10Pre = 19
        Totalpre = 20

        IdFormaPago_p = 21
        FormaPago_p = 22
        OC_p = 23
        NroReq_p = 24
        Status_p = 25
        Estado_p = 26
        Entregaren_p = 27
        SitioEntrega_p = 28
        NotaPresupuesto_p = 29
        Eliminado_p = 30
        IncluyeNotas_p = 31
        validez_p = 32
        RescDescGlobal_p = 33
        PorcRecDescGlogal_p = 34
        IdAutoriza_p = 35
        NombreAutoriza_p = 36
        IdVendedor_p = 37
        NombreVendedor_p = 38
        Anticipo_p = 39
        PorcAnticipo_p = 40
        OCA_p = 41
        OfertaComercial = 42
        MostrarCodigo = 43
        Vencido = 44
        Trafo = 45
        Trafo_Cabecera = 46
        Trafo_Observaciones = 47
        Trafo_HorasTrabajo = 48
        Trafo_SubtotalEnsayos = 49
        Mail_Comprador = 50
        IdMonedaPres = 51
        CodMonedaPres = 52
        ValorCambioPres = 53
        MostrarPrecioMat = 54
        MostrarSubtotalMatOC = 55
        MostrarSubtotalMOOC = 56
        ManoObra = 57

        SubtotalOfertaComercial = 58
        MostrarPrecioManoObra = 59

        PrecioDistribuidor = 60
        MonedaPres = 61
        MostrarTotal = 62
        MostrarPlazoEntrega = 63

        PresupuestoconWord = 64

        Garantia = 65

    End Enum

    Enum ColumnasDelGridOfertaComercial
        IdPresupOT = 0
        Subtotal = 1
        Iva = 2
        MontoIva = 3
        Total = 4
        Certificacion = 5
        Certificacion_Texto = 6
        Ajuste = 7
        Ajuste_Texto = 8
        PlazoEntrega = 9
        PlazoEntrega_Texto = 10
        PlazoEntregaProvision = 11
        PlazoEntregaProvision_Texto = 12
        IdPresupOT_Det = 13
        Descripcion = 14
        PreciosinIva = 15
        Orden = 16
        AlcanceBreve = 17
        IdDet = 18
    End Enum

    Enum ColumnasDelGridTrafo_Det
        Id = 0
        IdPresupuesto = 1
        Descripcion = 2
    End Enum

    Enum ColumnasDelGridTrafo_Ensayos
        Id = 0
        IdPresupuesto = 1
        Item = 2
        Descripcion = 3
        Marcar = 4
        Precio = 5
    End Enum

    'Auxiliares para guardar
    Dim cod_aux As String
    Dim band As Integer, Revision As Integer

    Dim Guardando As Boolean 'se utuliza para evitar la ejecuciòn del sub presupuestos vencidos cuando se està guardando


#Region "Eventos"

    Private Sub frmPresupuestos_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bolModo = True Then
            If MessageBox.Show("Tiene un presupuesto pendiente de ser guardado. Desea salir de todos modos?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmPresupuestos_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                LlenarGrid_Items(CType(txtID.Text, Long))
                If chkAgregarOfertaComercial.Checked = True Then
                    LlenarGrid_OfertaComercial(CType(txtID.Text, Long))
                End If
            End If
        End If
    End Sub

    Private Sub frmPresupuestos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            conexionGenerica = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        AsignarPermisos(UserID, "ListaPrecioDistribuidor", ALTA, MODIFICA, BAJA, BAJA_FISICA, DESHACER, ConnStringSEI)

        'Cursor = Cursors.WaitCursor

        txtBusquedaMAT.Visible = True
        ToolStrip_lblCodMaterial.Visible = True

        Resolucion()

        bolModo = False

        band = 0

        Band_RecDesc = False
        btnEliminar.Text = "Anular Pres."

        configurarform()
        Asignar_Tags()

        LlenarcmbClientes_App(cmbClientes, ConnStringSEI, llenandoCombo)
        LlenarcmbEntregar()
        LlenarcmbUnidadesVta()
        LlenarcmbCondicionDePago_App(cmbFormaPago, ConnStringSEI)
        LlenarcmbEmpleados3_App(cmbAutoriza, ConnStringSEI, "Select Id, (Nombre  + ', ' + Apellido) as 'Empleado' From Empleados Where autorizante = 1 and eliminado = 0 ORDER BY Empleado DESC")
        LlenarcmbMarcasProductos()

        LlenarcmbEstados()

        LlenarcmbMonedas_App(cmbMonedas, ConnStringSEI)

        LlenarcmbOfertaComercial()

        SQL = "exec spPresupuestos_Select_All  @Eliminado = 0, @Estado = '', @Codigo = ''"

        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()

        ' LlenarLista_Notas()

        Select Case Origen
            Case 0 'presupuesto normal
                If bolModo = True Then
                    LlenarGrid_Items(0)
                    btnNuevo_Click(sender, e)
                Else
                    LlenarGrid_Items(CType(txtID.Text, Long))
                    LlenarcmbClientes_Comprador()
                    LlenarcmbClientes_Usuario()
                End If
            Case 1 'presupuesto desde OCA
                band = 1
                btnNuevo_Click(sender, e)
                txtIdCliente.Text = Origen_IdCliente
                cmbClientes.SelectedValue = Origen_IdCliente
                txtCliente.Visible = True
                cmbClientes.Visible = False
                txtCliente.Text = Origen_Cliente
                chkOCA.Checked = True
                txtNombre.Text = "OC Abierta: " & Origen_Codigo
                txtNroOC.Text = Origen_OC
                LlenarGrid_Items(Origen_Id)
                grdItems.Enabled = Not chkOCA.Checked
                bolModo = True
            Case 2 'presupuesto desde OC
                band = 1
                btnNuevo_Click(sender, e)
                txtIdCliente.Text = Origen_IdCliente
                cmbClientes.SelectedValue = Origen_IdCliente
                txtCliente.Visible = False
                cmbClientes.Visible = True
                txtCliente.Text = Origen_Cliente
                chkOC.Checked = True
                txtNombre.Text = "OC: " & Origen_Codigo
                txtNroOC.Text = LTrim(RTrim(Origen_OC))
                LlenarGrid_Items(Origen_Id)
                'grdItems.Enabled = Not chkOCA.Checked
                bolModo = True
        End Select

        LlenarLista_Notas()

        If grd.RowCount > 0 Then
            ControlarLista_Notas()
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
            If Origen = 0 Then
                'txtCliente.Visible = chkOCA.Checked
                cmbClientes.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdCliente_p).Value
                txtIdCliente.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdCliente_p).Value
                txtIdComprador.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdComprador_p).Value
                'cmbComprador.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdComprador_p).Value
                cmbCompradores.Text = grd.CurrentRow.Cells(ColumnasDelGrid.NombreComprador_p).Value

                txtIdUsuario.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdUsuario_p).Value
                'cmbUsuario.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdUsuario_p).Value
                cmbUsuarios.Text = grd.CurrentRow.Cells(ColumnasDelGrid.NombreUsuario_p).Value

                txtporcrecargo.Text = grd.CurrentRow.Cells(ColumnasDelGrid.PorcRecDescGlogal_p).Value
                cmbAutoriza.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdAutoriza_p).Value
                'cmbVendedores.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdVendedor_p).Value
                BtnRemito.Enabled = Not CBool(grd.CurrentRow.Cells(ColumnasDelGrid.Status_p).Value)
                lblEstado.Visible = CBool(grd.CurrentRow.Cells(ColumnasDelGrid.Status_p).Value)

                CodMonedaOrig = grd.CurrentRow.Cells(ColumnasDelGrid.CodMonedaPres).Value
                'grdItems.Enabled = Not chkOCA.Checked
            End If
            'Else
            ' LlenarLista_Notas()
        End If

        permitir_evento_CellChanged = True

        CheckForIllegalCrossThreadCalls = False

        trd = New Thread(AddressOf HiloOcultarColumnasGrid)
        trd.IsBackground = True
        trd.Start()

        band = 1

        If rdTrafo.Checked = True Then
            rdTrafo_CheckedChanged(sender, e)
        Else
            chkAgregarOfertaComercial_CheckedChanged(sender, e)
        End If

        dtpFECHA.Focus()

        Presupuestos_Vencidos()

        EsOfertaComercial = chkAgregarOfertaComercial.Checked
        EsTrafo = rdTrafo.Checked

        dtpFECHA.MaxDate = Today.Date

        'Cursor = Cursors.Default
        'BuscarMoneda("DOLAR", txtCodMonedaPres, ValorcambioDolar)

        TimerGuardar.Enabled = True

    End Sub

    Private Sub frmPresupuestos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el presupuesto Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un nuevo Presupuesto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
            Case Keys.F6
                BtnRemito_Click(sender, e)
            Case Keys.F8
                btnAsignarTablero_Click(sender, e)
            Case Keys.F1
                If grdItems.Focused = True And grdItems.CurrentCell.ColumnIndex = 2 Then
                    BuscarProducto()
                End If
        End Select
    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItems.CellEndEdit
        editando_celda = False

        Try

            Dim valorcambio As Double = 0
            Dim ganancia As Double = 0, preciovta As Double = 0
            Dim idmoneda As Long = 0
            Dim nombre As String = "", codmoneda As String = ""

            If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
                'completar la descripcion del material
                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim id As Long
                Dim stock As Double = 0, minimo As Double = 0, maximo As Double = 0, preciolista As Double = 0, preciovtaorig As Double = 0, gananciaorig As Double = 0, iva As Double = 0, montoiva As Double = 0
                Dim fecha As String = ""
                Dim i As Integer

                Dim codigo As String, codigo_mat_prov As String = "", unidad As String = "", codunidad As String = "", nombreproveedor As String = "", nombremarca As String = "", plazoentrega As String = ""
                Dim idunidad As Long = 0, idproveedor As Long = 0, idmarca As Long = 0
                Dim Pasillo As String = "", Estante As String = "", Fila As String = ""

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
                If ObtenerMaterial_App(codigo, codigo_mat_prov, id, nombre, idunidad, unidad, codunidad, stock, minimo, maximo, preciolista, ganancia, preciovta, preciovtaorig, gananciaorig, iva, fecha, idproveedor, nombreproveedor, idmarca, nombremarca, plazoentrega, idmoneda, codmoneda, valorcambio, montoiva, 0, chkPrecioDistribuidor.Checked, ConnStringSEI, "", Pasillo, Estante, Fila) = 0 Then
                    cell.Value = nombre

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value = codigo_mat_prov
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = nombre

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = codunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Value = Math.Round(stock, 2)
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Minimo).Value = minimo
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Maximo).Value = maximo

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMoneda).Value = txtIdMonedaPres.Text
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.idmonedaorig).Value = txtIdMonedaPres.Text
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodMoneda).Value = txtCodMonedaPres.Text
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value = ValorCambioPres

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdProveedor).Value = idproveedor
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.NombreProveedor).Value = nombreproveedor

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMarca).Value = idmarca
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Marca).Value = nombremarca

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PlazoEntrega).Value = plazoentrega

                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0
                    End If

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value = FormatNumber(ganancia, 2)

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion1).Value = FormatNumber(0, 2)
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion2).Value = FormatNumber(0, 2)

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value = FormatNumber(0, 2)
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = FormatNumber(0, 2)

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value = 21

                    If codmoneda.ToUpper = "PE" And txtCodMonedaPres.Text.ToUpper = "DO" Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = Format(preciolista / ValorcambioDolar, "####0.00")
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta / ValorcambioDolar, "####0.00")
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = Format(preciovtaorig / ValorcambioDolar, "####0.00")
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = Format(gananciaorig / ValorcambioDolar, "####0.00")
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value * (iva / 100), "####0.00")
                    Else
                        If codmoneda.ToUpper = "DO" And txtCodMonedaPres.Text.ToUpper = "PE" Then
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = Format(preciolista * ValorcambioDolar, "####0.00")
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * ValorcambioDolar, "####0.00")
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = Format(preciovtaorig * ValorcambioDolar, "####0.00")
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = Format(gananciaorig * ValorcambioDolar, "####0.00")
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value * (iva / 100), "####0.00")
                        Else
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = preciolista
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = CDbl(preciovta)
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = CDbl(preciovtaorig)
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = CDbl(gananciaorig)
                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIva).Value = montoiva
                        End If
                    End If

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ubicacion).Value = Pasillo + " - " + Estante + " - " + Fila

                    If Pasillo.Length > 0 Then
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material).Style.BackColor = Color.LightGreen
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Style.BackColor = Color.LightGreen
                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
                    End If

                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.dateupd).Value = fecha

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

                    grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True

                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = 0 Then
                        GoTo SinProveedor
                    End If

                    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Ganancia, grdItems.CurrentRow.Index)

                Else
                    cell.Value = "NO EXISTE"
                    Exit Sub
                End If
            End If

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                'grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = False
                grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = False
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value Then
                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.PrecioLista).Value = FormatNumber(0, 2)
                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Qty).Value = FormatNumber(0, 2)
                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.SubTotalProd).Value = FormatNumber(CDbl(0.0), 2)
                End If
                'Else
                'grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = True
                'grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = True
            End If

            If e.ColumnIndex = ColumnasDelGridItems.Nombre_Material And _
                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then

                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_unidad As String, nombreUNIDAD As String = "", codunidad As String = ""
                Dim idunidad As Long

                cod_unidad = "U"

                If ObtenerUnidad_App(cod_unidad, idunidad, nombreUNIDAD, codunidad, ConnStringSEI) = 0 Then
                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Cod_Unidad).Value = cod_unidad
                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Unidad).Value = nombreUNIDAD

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                    Exit Sub
                End If

                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.IdMoneda).Value = txtIdMonedaPres.Text
                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.CodMoneda).Value = txtCodMonedaPres.Text

                SendKeys.Send("{TAB}")

                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Iva).Value = 21
                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Bonificacion1).Value = 0
                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Bonificacion2).Value = 0
                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Ganancia).Value = 0
                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.gananciaorig).Value = 0
                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.preciovtaorig).Value = 0
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0

            End If

            If e.ColumnIndex = ColumnasDelGridItems.Cod_Unidad Then

                Dim cell As DataGridViewCell = grdItems.CurrentCell
                Dim cod_unidad As String, codunidad As String = ""
                Dim idunidad As Long

                cod_unidad = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
                If ObtenerUnidad_App(cod_unidad, idunidad, nombre, codunidad, ConnStringSEI) = 0 Then
                    cell.Value = nombre
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = cod_unidad
                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = nombre

                    SendKeys.Send("{TAB}")

                Else
                    cell.Value = "NO EXISTE"
                    Exit Sub
                End If
            End If

            If e.ColumnIndex = ColumnasDelGridItems.PrecioLista _
                    Or e.ColumnIndex = ColumnasDelGridItems.Ganancia _
                    Or e.ColumnIndex = ColumnasDelGridItems.Bonificacion1 _
                    Or e.ColumnIndex = ColumnasDelGridItems.Bonificacion2 _
                    Or e.ColumnIndex = ColumnasDelGridItems.PorcRecDesc _
                    Or e.ColumnIndex = ColumnasDelGridItems.CodMoneda _
                    Or e.ColumnIndex = ColumnasDelGridItems.Iva Then
SinProveedor:

                Dim cell As DataGridViewCell = grdItems.CurrentCell

                nombre = ""
                ganancia = 0

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value Or _
                  grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value = 0
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion1).Value Is DBNull.Value Or _
                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion1).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion1).Value = 0
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion2).Value Is DBNull.Value Or _
                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion2).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion2).Value = 0
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value Or _
                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value = 0
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value Is DBNull.Value Or _
                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value = 0
                End If

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value
                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.idmonedaorig).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMoneda).Value

                preciovta = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value

                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)

                If IIf(grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value Is DBNull.Value, 0, grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value Is DBNull.Value) <> IIf(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value, 0, grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value) Then
                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value) And _
                                           Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value Is DBNull.Value) Then

                        ganancia = 1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100)

                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(ganancia * preciovta, "####0.00")

                        preciovta = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value

                    End If

                End If


                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = preciovta
                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * preciovta, "####0.00") 'CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * preciovta, 2))
                    End If
                Else
                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0 Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = preciovta
                    Else
                        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value > 0 Then
                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), "####0.00") 'Math.Round(preciovta * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
                        Else
                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value < 0 Then
                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * (1 - ((grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value * -1) / 100)), "####0.00") ' Math.Round(preciovta * (1 - ((grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value * -1) / 100)), 2)
                            End If
                        End If
                    End If

                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, "####0.00") ' CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, 2))
                    End If
                End If

                grdItems.CurrentRow.Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value * (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value / 100), "####0.00")

                Contar_Filas()

            End If


            If e.ColumnIndex = ColumnasDelGridItems.Qty Then
                Dim cell As DataGridViewCell = grdItems.CurrentCell

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value Then
                    Exit Sub
                Else
                    If CDbl(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value) = 0.0 Then
                        Exit Sub
                    End If
                End If

                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value Is DBNull.Value Then
                    Exit Sub
                Else
                    If CDbl(grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value) = 0.0 Then
                        Exit Sub
                    End If
                End If

                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value, "####0.00") ' CDbl(FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value, 2))

                Dim j As Integer = 0

                txtSubtotal.Text = "0"
                'txtSubtotalDO.Text = "0"

                For j = 0 To grdItems.Rows.Count - 2
                    If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value Is DBNull.Value) Then
                        If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value = "") Then
                            txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value))
                        End If
                    End If
                Next

                Contar_Filas()

            End If

        Catch ex As Exception
            ' MsgBox(ex.Message)
            'MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
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
    End Sub

    Private Sub grdTrafos_Det_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles grdTrafos_Det.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdTrafos_Det_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdTrafos_Det.CellEndEdit
        editando_celda = False
    End Sub

    Private Sub grditems_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub grdItems_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellChanged
        If band = 0 Then Exit Sub
        Try
            Cell_Y = grdItems.CurrentRow.Index
        Catch ex As Exception

        End Try

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

            If grdItems.RowCount <> 0 And Cell_X = ColumnasDelGridItems.Cod_Unidad Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Unidad).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Unidad).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            If grdItems.RowCount <> 0 And Cell_X = ColumnasDelGridItems.Marca Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Marca).Value.ToString & " " & grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Marca).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            If Valor <> "" Then
                Dim p As Point = New Point(e.X, e.Y)

                ContextMenuStrip1.Show(grdItems, p)
                ContextMenuStrip1.Items(0).Text = "Borrar el Item " & Valor
            End If

            If Valor <> "" And Cell_X = ColumnasDelGridItems.Cod_Unidad Then
                Dim p As Point = New Point(e.X, e.Y)

                ContextMenuUnidades.Show(grdItems, p)
            End If

            If Valor <> "" And Cell_X = ColumnasDelGridItems.Marca Then
                Dim p As Point = New Point(e.X, e.Y)

                ContextMenuMarcas.Show(grdItems, p)
            End If

        End If
    End Sub

    Private Sub grditems_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItems.CurrentCellDirtyStateChanged
        If grdItems.IsCurrentCellDirty Then
            grdItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub chkEliminado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminado.CheckedChanged
        cmbClientes.Enabled = Not chkEliminado.Checked
        grdItems.Enabled = Not chkEliminado.Checked
        dtpFECHA.Enabled = Not chkEliminado.Checked
    End Sub

    Private Sub cmbCliente_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedValueChanged
        If llenandoCombo = False Then
            If cmbClientes.Text <> "" Then
                txtIdCliente.Text = cmbClientes.SelectedValue
                LlenarcmbClientes_Comprador()
                LlenarcmbClientes_Usuario()
                BuscarPorcentajeRecargo()
            End If
        End If
    End Sub

    Private Sub chkNotas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotas.CheckedChanged
        lstNotas.Enabled = chkNotas.Checked
    End Sub

    Private Sub chkUsuario_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUsuario.CheckedChanged
        cmbUsuarios.Enabled = chkUsuario.Checked
        Try
            txtIdUsuario.Text = cmbUsuarios.SelectedValue
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkComprador_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComprador.CheckedChanged
        cmbCompradores.Enabled = chkComprador.Checked
        Try
            txtIdComprador.Text = cmbCompradores.SelectedValue
        Catch ex As Exception

        End Try
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
            'e.Handled = True
            'SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtNombre_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombre.KeyPress, _
        txtNroOC.KeyPress, txtReq.KeyPress, cmbEntregaren.KeyPress, txtValidez.KeyPress, txtAnticipo.KeyPress, txtNotaGestion.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub PicClientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicClientes.Click
        Dim f As New frmClientes

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbClientes.Text.ToUpper.ToString
        f.Origen = 1
        f.ShowDialog()
        LlenarcmbClientes_App(cmbClientes, ConnStringSEI, llenandoCombo)
        LlenarcmbClientes_Comprador()
        LlenarcmbClientes_Usuario()

        'cmbCliente.Text = texto_del_combo
    End Sub

    Private Sub PicEmpleados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicEmpleados.Click
        Dim f As New frmEmpleados

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbautoriza.Text.ToUpper.ToString
        f.ShowDialog()
        'LlenarcmbEmpleados3_App(cmbVendedores, ConnStringSEI)
        LlenarcmbEmpleados3_App(cmbAutoriza, ConnStringSEI, "Select Id, (Nombre  + ', ' + Apellido) as 'Empleado' From Empleados Where eliminado = 0 ORDER BY Empleado")
        cmbAutoriza.Text = texto_del_combo
    End Sub

    Private Sub PicFormaPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicFormaPago.Click
        Dim f As New frmCondiciondePago

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbFormaPago.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbCondicionDePago_App(cmbFormaPago, ConnStringSEI)
        cmbFormaPago.Text = texto_del_combo
    End Sub

    Private Sub PicNotas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicNotas.Click
        Dim f As New frmNotas

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        f.ShowDialog()
        'LlenarLista_Notas()

        ControlarLista_Notas()

    End Sub

    Private Sub PicGanancia_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picGanancia.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.picGanancia, "Por medio de esta opción se puede ocultar las columnas Ganancia y Precio Lista. Es importante recordar " & vbCrLf & "que estas columnas deberán mostrarse cuando ingrese un item que no está codificado.")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        If Permitir Then
            BtnFactura.Enabled = Not bolModo

            Try
                If grd.RowCount > 0 Then
                    'txtCliente.Visible = chkOCA.Checked

                    cmbClientes.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdCliente_p).Value
                    txtIdCliente.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdCliente_p).Value

                    txtIdComprador.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdComprador_p).Value
                    cmbCompradores.Text = grd.CurrentRow.Cells(ColumnasDelGrid.NombreComprador_p).Value

                    txtIdUsuario.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdUsuario_p).Value
                    cmbUsuarios.Text = grd.CurrentRow.Cells(ColumnasDelGrid.NombreUsuario_p).Value

                    txtporcrecargo.Text = grd.CurrentRow.Cells(ColumnasDelGrid.PorcRecDescGlogal_p).Value
                    cmbAutoriza.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdAutoriza_p).Value
                    'cmbVendedores.SelectedValue = grd.CurrentRow.Cells(ColumnasDelGrid.IdVendedor_p).Value
                    BtnRemito.Enabled = Not CBool(grd.CurrentRow.Cells(ColumnasDelGrid.Status_p).Value)
                    lblEstado.Visible = CBool(grd.CurrentRow.Cells(ColumnasDelGrid.Status_p).Value)

                    CodMonedaOrig = grd.CurrentRow.Cells(ColumnasDelGrid.CodMonedaPres).Value

                    lblListaPrecio.Visible = Not rdTrafo.Checked

                    lblCantidadFilas.Visible = Not rdTrafo.Checked
                    Label19.Visible = Not rdTrafo.Checked

                    If rdTrafo.Checked = False Then
                        chkAgregarOfertaComercial.Checked = CBool(grd.CurrentRow.Cells(ColumnasDelGrid.OfertaComercial).Value)

                        ControlarLista_Notas()
                    Else
                        rdTrafo_CheckedChanged(sender, e)
                    End If

                    EsTrafo = rdTrafo.Checked

                    CaclularMontosFinalesGrillaItems()

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub grdItems_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItems.CellMouseDoubleClick

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Exit Sub
        End If

        If Cell_Y < 0 Then
            Exit Sub
        End If

        BuscarProducto()
        grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True




    End Sub

    Private Sub chkRecDescGlobal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRecDescGlobal.CheckedChanged
        txtporcrecargo.Enabled = chkRecDescGlobal.Checked
        Label4.Enabled = chkRecDescGlobal.Checked

        If chkRecDescGlobal.Checked = True Then
            txtporcrecargo.Focus()
        Else
            txtporcrecargo.Text = ""
        End If

        Calcular_RecargoDescuento()

    End Sub

    Private Sub BorrarElItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItemToolStripMenuItem.Click
        Dim cell As DataGridViewRow = grdItems.CurrentRow
        Try
            If cell.Index >= 0 Then 'el de arriba no borraba la fila 0....
                Try
                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.IDMaterial).Value Is DBNull.Value Then
                        grdItems.Rows.RemoveAt(cell.Index)
                        grdItems.Refresh()
                        Contar_Filas()
                    Else
                        If bolModo = False Then
                            If ControlarItemdentrodeRemito(grdItems.CurrentRow.Cells(ColumnasDelGridItems.IDMaterial).Value) > 0 Then
                                MsgBox("El Item seleccionado ya fue entregado y está ingresado en un remito, por lo tanto no se puede eliminar", MsgBoxStyle.Critical)
                                Exit Sub
                            End If
                        End If
                        grdItems.Rows.RemoveAt(cell.Index)
                        grdItems.Refresh()
                        Contar_Filas()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BorrarElItem_OfertaComercial_ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorrarElItem_OfertaComercial_ToolStripMenuItem.Click
        Dim cell As DataGridViewRow = grdOfertaComercial.CurrentRow
        Try
            If cell.Index >= 0 Then 'el de arriba no borraba la fila 0....
                Try
                    If MessageBox.Show("Está seguro que desea eliminar el item?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If bolModo = True Then
                            grdOfertaComercial.Rows.RemoveAt(cell.Index)
                            grdOfertaComercial.Refresh()

                            CaclularMontosFinalesGrillaItems()

                        Else
                            If Not (grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.IdPresupOT_Det).Value Is DBNull.Value) Then
                                'verificar si existe en un remito
                                If EliminarRegistro_OfertaComercial(grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.IdPresupOT_Det).Value) > 0 Then
                                    grdOfertaComercial.Rows.RemoveAt(cell.Index)
                                    grdOfertaComercial.Refresh()

                                    CaclularMontosFinalesGrillaItems()

                                    If ActualizarPresupuesto_DesdeOfertaComercial() <= 0 Then
                                        MsgBox("Ocurrió un error al actualizar el presupuesto. Antes de terminar, presione el botón Guardar.", MsgBoxStyle.Information, "Atención")
                                    End If

                                Else
                                    MsgBox("El ítem seleccionado está dentro de un remito. No se puede eliminar", MsgBoxStyle.Information, "Atención")
                                End If
                            Else
                                grdOfertaComercial.Rows.RemoveAt(cell.Index)
                                grdOfertaComercial.Refresh()
                                CaclularMontosFinalesGrillaItems()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
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

    Private Sub cmbMarcasCompra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMarcaCompra.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then

            Dim cell As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.Marca)
            Dim cell2 As DataGridViewCell = grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMarca)
            grdItems.CurrentCell = cell
            grdItems.CurrentCell.Value = cmbMarcaCompra.Text
            grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMarca).Value = cmbMarcaCompra.ComboBox.SelectedValue

            ContextMenuMarcas.Close()
            grdItems.BeginEdit(True)
        ElseIf e.KeyCode = Keys.Escape Then
            ContextMenuMarcas.Close()
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

            ContextMenuUnidades.Close()
            grdItems.BeginEdit(True)
        ElseIf e.KeyCode = Keys.Escape Then
            ContextMenuUnidades.Close()

        End If
    End Sub

    Private Sub chkAnulados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnulados.CheckedChanged
        btnGuardar.Enabled = Not chkAnulados.Checked
        btnEliminar.Enabled = Not chkAnulados.Checked
        btnNuevo.Enabled = Not chkAnulados.Checked
        btnCancelar.Enabled = Not chkAnulados.Checked

        If chkAnulados.Checked = True Then
            SQL = "exec spPresupuestos_Select_All @Eliminado = 1, @Estado = '', @Codigo = ''"
        Else
            If chkPresupuestosCumplidos.Checked = False Then
                SQL = "exec spPresupuestos_Select_All @Eliminado = 0, @Estado = '', @Codigo = ''"
            Else
                SQL = "exec spPresupuestos_Select_All @Eliminado = 0, @Estado = 'Cumplido', @Codigo = ''"
            End If
        End If

        LlenarGrilla()

        Presupuestos_Vencidos()

    End Sub

    Private Sub chkAnticipo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnticipo.CheckedChanged
        txtAnticipo.Enabled = chkAnticipo.Checked
        If chkAnticipo.Checked = False Then
            txtAnticipo.Text = "0"
        End If
    End Sub

    Private Sub Resolucion()
        Dim ANCHO As String
        Dim alto As String
        Dim tamano As String

        ANCHO = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width.ToString
        alto = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height.ToString
        tamano = ANCHO + "x" + alto
        Select Case tamano
            'Case "800x600"
            '    cambiarResolucion(form, 110.0F, 110.0F)
            Case "1280x1024"
                cmbCompradores.Width = 140
                cmbUsuarios.Width = 140
                cmbUsuarios.Location = New Point(887, 32)
                chkUsuario.Location = New Point(887, 32)
                txtNroOC.Location = New Point(1037, 32)
                txtReq.Location = New Point(1144, 32)
                txtNotaGestion.Width = 316
                lstNotas.Location = New Point(990, 137)
                lstNotas.Width = 252
                chkNotas.Location = New Point(990, 114)
                PicNotas.Location = New Point(1091, 114)
                grdItems.Width = 965
                chkAnulados.Location = New Point(800, 98)
                'Case Else
                'Case "1366x768"
                '    grdItems.Width = grdItems.Width - 100
                '    BtnRemito.Left = BtnRemito.Left - 100
                '    'lstNotas.Left = lstNotas.Left - 100
                '    chkNotas.Left = chkNotas.Left - 100
                '    PicNotas.Left = PicNotas.Left - 100
                '    chkAmpliarNotas.Left = chkAmpliarNotas.Left - 100
        End Select

    End Sub

    Private Sub chkAmpliarGrillaInferior_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAmpliarGrillaInferior.CheckedChanged

        Dim xgrd As Long, ygrd As Long, hgrd As Long
        xgrd = grd.Location.X
        ygrd = grd.Location.Y
        hgrd = grd.Height


        If chkAmpliarGrillaInferior.Checked = True Then
            chkAmpliarGrillaInferior.Text = "Disminuir Grilla Inferior"

            chkAmpliarGrillaInferior.Location = New Point(chkAmpliarGrillaInferior.Location.X, chkAmpliarGrillaInferior.Location.Y - 100)

            GroupBox1.Height = GroupBox1.Height - 100

            grd.Location = New Point(xgrd, ygrd - 100)
            grd.Height = hgrd + 100
        Else
            chkAmpliarGrillaInferior.Text = "Aumentar Grilla Inferior"

            chkAmpliarGrillaInferior.Location = New Point(chkAmpliarGrillaInferior.Location.X, chkAmpliarGrillaInferior.Location.Y + 100)

            GroupBox1.Height = GroupBox1.Height + 100

            grd.Location = New Point(xgrd, ygrd + 100)
            grd.Height = hgrd - 100
        End If

    End Sub

    Private Sub chkBuscarClientes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBuscarClientes.CheckedChanged
        cmbClientes2.Enabled = chkBuscarClientes.Checked
        If chkBuscarClientes.Checked = True Then
            If cmbClientes2.Text <> "" Then
                QuitarElFitroToolStripMenuItem_Click(sender, e)
                ColumnName = "Cliente"
                ColumnType = "system.string"
                Filtrarpor(cmbClientes2.Text)
            End If
        Else
            QuitarElFitroToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub cmbCliente2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes2.SelectedIndexChanged
        If band = 1 Then
            If cmbClientes2.Text <> "" Then
                QuitarElFitroToolStripMenuItem_Click(sender, e)
                ColumnName = "Cliente"
                ColumnType = "system.string"
                Filtrarpor(cmbClientes2.Text)
            Else
                QuitarElFitroToolStripMenuItem_Click(sender, e)
            End If
        End If
    End Sub

    'Private Sub grdItems_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdItems.DataError
    '    'Try
    '    '    MsgBox("Error en la columna: " & grdItems.Columns(e.ColumnIndex).Name)
    '    'Catch ex As Exception

    '    'End Try
    'End Sub

    Private Sub cmbUsuario_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUsuarios.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdUsuario.Text = cmbUsuarios.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbComprador_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompradores.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdComprador.Text = cmbCompradores.SelectedValue
                BuscarCorreoComprador()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbFormaPago_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        If band = 1 Then
            txtIdFormaPago.Text = cmbFormaPago.SelectedValue
        End If
    End Sub

    Private Sub chkAmpliarNotas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAmpliarNotas.CheckedChanged

        Dim wgrditems As Long, VariableAjusteNotas As Long
        'xgrd = grdItems.Location.X
        'ygrd = grdItems.Location.Y
        wgrditems = grdItems.Width

        VariableAjusteNotas = 200

        If chkAmpliarNotas.Checked = True Then
            chkAmpliarNotas.Text = "Disminuir Lista Notas"

            chkNotas.Location = New Point(chkNotas.Location.X - VariableAjusteNotas, chkNotas.Location.Y)

            grdItems.Size = New Size(grdItems.Size.Width - VariableAjusteNotas, grdItems.Size.Height)

            lstNotas.Location = New Point(lstNotas.Location.X - VariableAjusteNotas, lstNotas.Location.Y)
            lstNotas.Size = New Size(lstNotas.Size.Width + VariableAjusteNotas, lstNotas.Size.Height)

            lblEstado.Location = New Point(lblEstado.Location.X - VariableAjusteNotas, lblEstado.Location.Y)

            PicNotas.Location = New Point(PicNotas.Location.X - VariableAjusteNotas, PicNotas.Location.Y)

        Else
            chkAmpliarNotas.Text = "Ampliar Lista Notas"

            chkNotas.Location = New Point(chkNotas.Location.X + VariableAjusteNotas, chkNotas.Location.Y)

            grdItems.Size = New Size(grdItems.Size.Width + VariableAjusteNotas, grdItems.Size.Height)

            lstNotas.Location = New Point(lstNotas.Location.X + VariableAjusteNotas, lstNotas.Location.Y)
            lstNotas.Size = New Size(lstNotas.Size.Width - VariableAjusteNotas, lstNotas.Size.Height)

            lblEstado.Location = New Point(lblEstado.Location.X + VariableAjusteNotas, lblEstado.Location.Y)

            PicNotas.Location = New Point(PicNotas.Location.X + VariableAjusteNotas, PicNotas.Location.Y)

        End If
    End Sub

    'Private Sub chkPrecioDistribuidor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrecioDistribuidor.CheckedChanged
    '    If chkPrecioDistribuidor.Checked = True Then
    '        lblListaPrecio.Text = "Lista de Precios: VERDE"
    '    Else
    '        lblListaPrecio.Text = "Lista de Precios: TABLERISTA"
    '    End If
    'End Sub

    Private Sub chkAgregarOfertaComercial_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAgregarOfertaComercial.CheckedChanged

        If band = 1 Then

            chkMostrarSubtotalMaterialOC.Enabled = chkAgregarOfertaComercial.Checked
            chkMostrarSubtotalMOOC.Enabled = chkAgregarOfertaComercial.Checked
            chkMostrarPrecioManoObra.Enabled = chkAgregarOfertaComercial.Checked

            grdOfertaComercial.Visible = chkAgregarOfertaComercial.Checked

            chkCertificaciones.Visible = chkAgregarOfertaComercial.Checked
            cmbCertificaciones.Visible = chkAgregarOfertaComercial.Checked

            chkAjustes.Visible = chkAgregarOfertaComercial.Checked
            cmbAjustes.Visible = chkAgregarOfertaComercial.Checked

            chkPlazoEntregaProvision.Visible = chkAgregarOfertaComercial.Checked
            cmbPlazoEntregaProvision.Visible = chkAgregarOfertaComercial.Checked

            cmbPlazoEntrega.Visible = chkAgregarOfertaComercial.Checked

            chkPlazoEntrega.Visible = chkAgregarOfertaComercial.Checked
            lblTotalOferta.Visible = chkAgregarOfertaComercial.Checked
            lblMontoIvaOferta.Visible = chkAgregarOfertaComercial.Checked

            txtTotalOferta.Visible = chkAgregarOfertaComercial.Checked
            txtSubtotalOferta.Visible = chkAgregarOfertaComercial.Checked
            txtMontoIvaOferta.Visible = chkAgregarOfertaComercial.Checked

            lblSubtotalOferta.Visible = chkAgregarOfertaComercial.Checked
            lblIvaOferta.Visible = chkAgregarOfertaComercial.Checked
            txtIvaOferta.Visible = chkAgregarOfertaComercial.Checked

            lblSubtotalPre.Visible = chkAgregarOfertaComercial.Checked
            txtSubtotalPre.Visible = chkAgregarOfertaComercial.Checked
            lblIva21Pre.Visible = chkAgregarOfertaComercial.Checked
            txtIva21Pre.Visible = chkAgregarOfertaComercial.Checked
            lblTotalPre.Visible = chkAgregarOfertaComercial.Checked
            txtTotalPre.Visible = chkAgregarOfertaComercial.Checked

            lblAlcanceBreve.Visible = chkAgregarOfertaComercial.Checked
            txtAlcanceBreve.Visible = chkAgregarOfertaComercial.Checked

            lblGarantia.Visible = chkAgregarOfertaComercial.Checked
            txtgarantia.Visible = chkAgregarOfertaComercial.Checked
            lblGarantiaMeses.Visible = chkAgregarOfertaComercial.Checked

            If chkAgregarOfertaComercial.Checked = True Then

                If bolModo = True Then
                    LlenarGrid_OfertaComercial(0)
                    btnAbrirWord.Enabled = False
                Else
                    LlenarGrid_OfertaComercial(grd.CurrentRow.Cells(0).Value)
                    btnAbrirWord.Enabled = True
                End If

                grdItems.Height = 170

                txtIva21.Location = New System.Drawing.Point(txtIva21.Location.X, 271) 'txtIva21PE.Location.Y - variableOT)
                lblIVA21PE.Location = New System.Drawing.Point(lblIVA21PE.Location.X, 274) 'lblIVA21PE.Location.Y - variableOT)

                txtIva10.Location = New System.Drawing.Point(txtIva10.Location.X, 271) ' txtIva10PE.Location.Y - variableOT)
                lblIVA10PE.Location = New System.Drawing.Point(lblIVA10PE.Location.X, 274) 'lblIVA10PE.Location.Y - variableOT)

                lblSubtotalPE.Location = New System.Drawing.Point(lblSubtotalPE.Location.X, 274) ' lblSubtotalPE.Location.Y - variableOT)
                txtSubtotal.Location = New System.Drawing.Point(txtSubtotal.Location.X, 271) 'txtSubtotalPE.Location.Y - variableOT)

                lblTotalDO.Location = New System.Drawing.Point(lblTotalDO.Location.X, 274) ' lblTotalDO.Location.Y - variableOT)
                txtTotal.Location = New System.Drawing.Point(txtTotal.Location.X, 271) 'txtTotalDO.Location.Y - variableOT)

                lblCantidadFilas.Location = New System.Drawing.Point(lblCantidadFilas.Location.X, 274) 'lblCantidadFilas.Location.Y - variableOT + 10)
                Label19.Location = New System.Drawing.Point(Label19.Location.X, 274) ' Label19.Location.Y - variableOT + 10)

                lblListaPrecio.Location = New System.Drawing.Point(lblListaPrecio.Location.X, 274)

                lblEstado.Location = New System.Drawing.Point(lblEstado.Location.X, 275) 'lblEstado.Location.Y - variableOT + 10)


                MontoFinalPresupuesto()

            Else

                If band = 1 Then

                    btnAbrirWord.Enabled = False

                    grdItems.Height = 385

                    txtIva21.Location = New System.Drawing.Point(txtIva21.Location.X, 491)
                    lblIVA21PE.Location = New System.Drawing.Point(lblIVA21PE.Location.X, 494)

                    txtIva10.Location = New System.Drawing.Point(txtIva10.Location.X, 491)
                    lblIVA10PE.Location = New System.Drawing.Point(lblIVA10PE.Location.X, 494)

                    lblSubtotalPE.Location = New System.Drawing.Point(lblSubtotalPE.Location.X, 494)
                    txtSubtotal.Location = New System.Drawing.Point(txtSubtotal.Location.X, 491)

                    lblTotalDO.Location = New System.Drawing.Point(lblTotalDO.Location.X, 494)
                    txtTotal.Location = New System.Drawing.Point(txtTotal.Location.X, 491)

                    lblCantidadFilas.Location = New System.Drawing.Point(lblCantidadFilas.Location.X, 494)
                    Label19.Location = New System.Drawing.Point(Label19.Location.X, 494)

                    lblListaPrecio.Location = New System.Drawing.Point(lblListaPrecio.Location.X, 494)

                    lblEstado.Location = New System.Drawing.Point(lblEstado.Location.X, 492)

                    chkManoObra.Checked = False

                End If
            End If
        End If

    End Sub

    Private Sub chkPlazoEntrega_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlazoEntrega.CheckedChanged
        If chkAgregarOfertaComercial.Checked = True Then
            cmbPlazoEntrega.Enabled = chkPlazoEntrega.Checked
        End If
    End Sub

    Private Sub chkPlazoEntregaProvision_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlazoEntregaProvision.CheckedChanged
        If chkAgregarOfertaComercial.Checked = True Then
            cmbPlazoEntregaProvision.Enabled = chkPlazoEntregaProvision.Checked
        End If
    End Sub

    Private Sub chkCertificaciones_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCertificaciones.CheckedChanged
        If chkAgregarOfertaComercial.Checked = True Then
            cmbCertificaciones.Enabled = chkCertificaciones.Checked
        End If
    End Sub

    Private Sub chkAjustes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAjustes.CheckedChanged
        If chkAgregarOfertaComercial.Checked = True Then
            cmbAjustes.Enabled = chkAjustes.Checked
        End If
    End Sub

    Private Sub grdOfertaComercial_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOfertaComercial.CellEndEdit
        editando_celda = False

        Try

            If e.ColumnIndex = ColumnasDelGridOfertaComercial.Descripcion Then
                If grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value Is DBNull.Value Then
                    grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value = 0
                    Exit Sub
                End If

                If grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value.ToString = "" Then
                    grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value = 0
                    Exit Sub
                End If

                If grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value Is Nothing Then
                    grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value = 0
                    Exit Sub
                End If

            End If

            If e.ColumnIndex = ColumnasDelGridOfertaComercial.PreciosinIva Then

                If grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.Descripcion).Value Is DBNull.Value Then
                    MsgBox("Debe ingresar una descripción al trabajo en esta fila")
                    Exit Sub
                End If

                If grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value Is DBNull.Value Then
                    grdOfertaComercial.CurrentRow.Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value = 0
                    'Exit Sub
                End If

                CaclularMontosFinalesGrillaItems()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdOfertaComercial_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdOfertaComercial.EditingControlShowing

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosReales2_OfertaTecnica

    End Sub

    Private Sub grdOfertaComercial_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdOfertaComercial.CellBeginEdit
        editando_celda = True
    End Sub

    Private Sub grdOfertaComercial_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdOfertaComercial.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Sub grdOfertaComercial_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOfertaComercial.CurrentCellChanged
        If band = 0 Then Exit Sub
        Try
            Cell_Y = grdOfertaComercial.CurrentRow.Index
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdOfertaComercial_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdOfertaComercial.MouseUp
        Dim Valor As String = ""
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If grdOfertaComercial.RowCount <> 0 Then
                If Cell_Y <> -1 Then
                    Try
                        Valor = grdOfertaComercial.Rows(Cell_Y).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value.ToString
                    Catch ex As Exception
                    End Try
                End If
            End If

            Dim p As Point = New Point(e.X, e.Y)

            ContextMenuOfertaComercial.Show(grdOfertaComercial, p)
            ContextMenuOfertaComercial.Items(0).Text = "Borrar el Item " & Valor

        End If
    End Sub

    Private Sub chkPresupuestosCumplidos_CheckedChanged(sender As Object, e As EventArgs) Handles chkPresupuestosCumplidos.CheckedChanged

        QuitarElFitroToolStripMenuItem_Click(sender, e)

        chkAnulados.Checked = False

        chkAnulados_CheckedChanged(sender, e)

    End Sub

    Private Sub grdTrafos_Ensayos_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTrafos_Ensayos.CurrentCellDirtyStateChanged
        If grdTrafos_Ensayos.IsCurrentCellDirty Then
            grdTrafos_Ensayos.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub grdTrafos_Ensayos_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdTrafos_Ensayos.CellValueChanged

        Dim i As Integer

        Dim numItem As String = grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value.ToString

        'Para cambiar el valor del precio
        If numItem.Length <> 1 Then
            If grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(grdTrafos_Ensayos.CurrentCell.ColumnIndex).Value = False Then
                txtTrafo_SubtotalEnsayos.Text -= CDbl(grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value)
                grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value = CDec("0.00")
            End If
        Else
            If grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(grdTrafos_Ensayos.CurrentCell.ColumnIndex).Value = False Then
                For i = 0 To grdTrafos_Ensayos.Rows.Count - 1
                    txtTrafo_SubtotalEnsayos.Text -= CDbl(grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value)
                    grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value = CDec("0.00")
                Next
            End If
        End If
        '''''''

        If bandTrafo = 0 Then

            If grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True Then

                If numItem.Length = 1 Then
                    For i = 0 To grdTrafos_Ensayos.RowCount - 1
                        If Mid(grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value, 1, 1) = numItem Then
                            grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True
                        End If
                    Next
                Else
                    For i = 0 To grdTrafos_Ensayos.RowCount - 1
                        If Mid(numItem, 1, 1) = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value Then
                            grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True
                        End If
                    Next
                End If

            Else

                If numItem.Length = 1 Then
                    For i = 0 To grdTrafos_Ensayos.RowCount - 1
                        If Mid(grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value, 1, 1) = numItem Then
                            grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = False
                        End If
                    Next
                Else
                    Dim pos As Integer
                    Dim j As Integer
                    Dim titulo As Integer = Mid(numItem, 1, 1)
                    For i = 0 To grdTrafos_Ensayos.RowCount - 1

                        If titulo = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value Then
                            pos = i

                            For j = i + 1 To grdTrafos_Ensayos.RowCount - 1
                                If titulo = Mid(grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value, 1, 1) Then
                                    If grdTrafos_Ensayos.Rows(j).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True Then
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End If
                    Next
                    bandTrafo = 1
                    grdTrafos_Ensayos.Rows(pos).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = False
                    bandTrafo = 0
                End If

            End If

        End If

    End Sub

    Private Sub grdTrafos_Ensayos_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdTrafos_Ensayos.CellEndEdit

        If e.ColumnIndex = ColumnasDelGridTrafo_Ensayos.Precio Then

            Dim i As Integer

            If txtTrafo_SubtotalEnsayos.Text = "" Then
                txtTrafo_SubtotalEnsayos.Text = "0"
            End If

            If grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True Then
                Dim total As Decimal

                For i = 0 To grdTrafos_Ensayos.Rows.Count - 1
                    total += CDbl(grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value())
                Next
                txtTrafo_SubtotalEnsayos.Text = total
            Else
                If MessageBox.Show("Ha ingresado un precio pero no ha marcado la casilla [Añadir], ¿desea hacerlo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True
                    txtTrafo_SubtotalEnsayos.Text += CDbl(grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value)
                Else
                    grdTrafos_Ensayos.Rows(grdTrafos_Ensayos.CurrentRow.Index).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value = "0.00"
                End If
            End If

        End If

    End Sub

    Private Sub txtSubtotalPE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSubtotal.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If rdTrafo.Checked = True Then

                txtIva21.Text = FormatNumber(CDbl(txtSubtotal.Text) * 0.21, 2) 'CDbl(IIf(txtIVA.Text = "", 0, txtIVA.Text)) / 100, 2)

                txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) + CDbl(txtIva21.Text), 2)

            End If
        End If
    End Sub

    Private Sub rdTrafo_CheckedChanged(sender As Object, e As EventArgs) Handles rdTrafo.CheckedChanged
        If bolModo = False And band = 1 Then
            grdItems.Visible = Not rdTrafo.Checked 'False
            grdOfertaComercial.Visible = False
            lblTrafo_Cabecera.Visible = rdTrafo.Checked 'True
            txtTrafo_Cabecera.Visible = rdTrafo.Checked 'True
            lblTrafo_Observaciones.Visible = rdTrafo.Checked 'True
            txtTrafo_Observaciones.Visible = rdTrafo.Checked 'True
            grdTrafos_Ensayos.Visible = rdTrafo.Checked 'True
            grdTrafos_Det.Visible = rdTrafo.Checked 'True

            txtTrafo_SubtotalEnsayos.Visible = rdTrafo.Checked 'True
            lblTrafo_SubtotalEnsayos.Visible = rdTrafo.Checked 'True

            txtTrafo_CantHoras.Visible = rdTrafo.Checked 'True
            lblTrafo_CantHoras.Visible = rdTrafo.Checked 'True

            chkAgregarOfertaComercial.Enabled = Not rdTrafo.Checked

            lblListaPrecio.Visible = Not rdTrafo.Checked

            lblCantidadFilas.Visible = Not rdTrafo.Checked
            Label19.Visible = Not rdTrafo.Checked

            If rdTrafo.Checked = True Then
                LimpiarGridItems(grdTrafos_Det)

                LlenarGrid_Trafos_Ensayos()
                LlenarGrid_Trafos_Det()

            End If

            txtSubtotal.ReadOnly = False
        End If

    End Sub

    Private Sub grdTrafos_Ensayos_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdTrafos_Ensayos.EditingControlShowing
        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_NumerosRealesTrafos
    End Sub

    Private Overloads Sub grd_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd.CellDoubleClick

        nbreformreportes = "Presupuesto"

        Dim codigopres As String
        Dim Rpt As New frmReportes
        Dim Rpt1 As New frmReportes
        Dim Rpt2 As New frmReportes
        Dim Rpt3 As New frmReportes
        Dim OfertacomercialBIT As Boolean, trafo As Boolean, ManoObra As Boolean

        codigopres = grd.CurrentRow.Cells(ColumnasDelGrid.Codigo_p).Value

        Cursor = Cursors.WaitCursor

        Rpt.NombreArchivoPDF = BuscarNombreArchivo(codigopres, OfertacomercialBIT, trafo, ManoObra)
        Rpt.MailDestinatario = txtMailComprador.Text

        If trafo = True Then
            nbreformreportes = "Presupuesto Mantenimiento Transformador"
            Rpt.SEI_Trafos(codigopres, Rpt1)
            nbreformreportes = "Presupuesto Mantenimiento Transformador - Ensayos"
            Rpt2.SEI_Trafos_Ensayos(codigopres, Rpt3)
        Else
            If OfertacomercialBIT = True Then
                If ManoObra = False Then
                    nbreformreportes = "Oferta Comercial"
                    Rpt.SEI_OfertaComercial(codigopres, Rpt1, ManoObra)
                Else
                    nbreformreportes = "Mano de Obra"
                    Rpt.SEI_OfertaComercial(codigopres, Rpt1, ManoObra)
                End If
            Else
                Rpt.Presupuesto_App(codigopres, Rpt, My.Application.Info.AssemblyName.ToString)
            End If
        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub ActivarNuevaMarcaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActivarNuevaMarcaToolStripMenuItem.Click
        grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = False
        grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMarca).Value = DBNull.Value
        grdItems.Rows(Cell_Y).Cells(ColumnasDelGridItems.marcanueva).Value = 1
    End Sub

    Private Sub txtValorCambioPres_TextChanged(sender As Object, e As EventArgs) Handles txtValorCambioPres.TextChanged
        Try
            ValorCambioPres = CDbl(txtValorCambioPres.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CopiarSelecciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarSelecciónToolStripMenuItem.Click
        Dim d As DataObject

        d = grdItems.GetClipboardContent
        Clipboard.SetDataObject(d)

    End Sub

    Private Sub chkManoObra_CheckedChanged(sender As Object, e As EventArgs) Handles chkManoObra.CheckedChanged

        If chkManoObra.Checked = True Then
            If chkAgregarOfertaComercial.Checked = False Then
                chkAgregarOfertaComercial.Checked = True
            End If
            grdItems.Enabled = False
        Else
            grdItems.Enabled = True
        End If

    End Sub

    Private Sub cmbMonedas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedas.SelectedIndexChanged
        If band = 1 Then
            txtIdMonedaPres.Text = cmbMonedas.SelectedValue.ToString
            'BuscarMoneda(cmbMonedas.Text.ToString, txtCodMonedaPres, ValorcambioDolar)
            Actualizar_PrecioSegunMoneda()
            CodMonedaOrig = txtCodMonedaPres.Text
            ControlarLista_Notas()
        End If
    End Sub

#End Region

#Region "Botones"

    Public Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        TimerGuardar.Enabled = False

        lblEstado.Visible = False

        AbrirWord = False

        If btnBand_Copiar = True Then
            Util.LimpiarTextBox(Me.Controls)
            PrepararGridItems()
        End If

        'If chkAmpliarGrillaInferior.Checked = True Then
        chkAmpliarGrillaInferior.Checked = False
        'End If

        If btnBand_Copiar = True Then
            If Origen = 2 Then
                frmPresupuestos_Tipos.rdBaja.Enabled = False
                frmPresupuestos_Tipos.rdMedia.Enabled = False
                frmPresupuestos_Tipos.rdTableros.Enabled = False
            End If
            frmPresupuestos_Tipos.ShowDialog()
        End If

        If rdMateriales.Checked = True Then
            If MessageBox.Show("Desea agregar al presupuesto como una Oferta Comercial?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                bolModo = True
                chkAgregarOfertaComercial.Checked = True
                'chkSubtotalOfertaComercial.Checked = False
                txtAlcanceBreve.Text = " La presente propuesta comprende la provisión de mano de obra especializada y materiales, de acuerdo a la descripción presentada en la oferta Técnico / Comercial."
            Else
                bolModo = True
                chkAgregarOfertaComercial.Checked = False
                txtAlcanceBreve.Text = ""
            End If
        End If

        If rdTrafo.Checked = True Then
            grdItems.Visible = False
            grdOfertaComercial.Visible = False
            lblTrafo_Cabecera.Visible = True
            txtTrafo_Cabecera.Visible = True
            lblTrafo_Observaciones.Visible = True
            txtTrafo_Observaciones.Visible = True
            grdTrafos_Ensayos.Visible = True
            grdTrafos_Det.Visible = True

            txtTrafo_SubtotalEnsayos.Visible = True
            lblTrafo_SubtotalEnsayos.Visible = True

            txtTrafo_CantHoras.Visible = True
            lblTrafo_CantHoras.Visible = True

            bolModo = True

            LimpiarGridItems(grdTrafos_Det)

            LlenarGrid_Trafos_Ensayos()
            LlenarGrid_Trafos_Det()

            txtSubtotal.ReadOnly = False

        Else
            If btnBand_Copiar = True Then
                'If MessageBox.Show("¿Desea realizar el presupuesto con la lista de Precio de Tableristas?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                '    chkPrecioDistribuidor.Checked = True
                '    lblListaPrecio.Text = "Lista de Precios: VERDE"
                'Else
                chkPrecioDistribuidor.Checked = False
                lblListaPrecio.Text = "Lista de Precios: TABLERISTA"
                'End If
            End If
        End If

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        chkEliminado.Checked = False
        cmbClientes.Enabled = True

        grdItems.Enabled = True
        dtpFECHA.Enabled = True

        If btnBand_Copiar = True Then

            txtIva21.Text = "0"
            txtIva10.Text = "0"
            txtSubtotal.Text = "0"
            txtTotal.Text = "0"

            txtSubtotalPre.Text = "0"
            txtIva21Pre.Text = "0"
            txtTotalPre.Text = "0"

            txtSubtotalOferta.Text = "0"
            txtMontoIvaOferta.Text = "0"
            txtTotalOferta.Text = "0"

            txtIVA.Text = "0"

            txtValidez.Text = "5"
            txtIvaOferta.Text = "21"
            chkUsuario.Checked = False
            chkComprador.Checked = False
            chkEntrega.Checked = False
            
            lblCantidadFilas.Text = "0"
            chkNotas.Checked = True
            cmbFormaPago.SelectedValue = 7
            txtIdFormaPago.Text = 7
            cmbAutoriza.SelectedIndex = 0
            txtporcrecargo.Text = "0.00"

            txtgarantia.Text = "18"

            chkMostrarPrecioManoObra.Checked = False
            chkMostrarPrecioMaterial.Checked = False
            chkMostrarSubtotalMaterialOC.Checked = False
            chkMostrarSubtotalMOOC.Checked = False
            chkMostrarCodigoMaterial.Checked = False
            ChkMostrarTotal.Checked = True

            chkManoObra.Checked = False

            txtTrafo_Cabecera.Text = ""
            txtTrafo_CantHoras.Text = "0"
            txtTrafo_Observaciones.Text = ""
            txtTrafo_SubtotalEnsayos.Text = "0"

            txtAnticipo.Text = "0"

            cmbEstado.Text = "Pendiente"

            chkOcultarGanancia.Checked = False

            LlenarLista_Notas()

            cmbMonedas.SelectedIndex = 1
            'BuscarMoneda("DOLAR", txtCodMonedaPres, ValorcambioDolar)
            ValorCambioPres = ValorcambioDolar
            txtIdMonedaPres.Text = cmbMonedas.SelectedValue.ToString
            CodMonedaOrig = txtCodMonedaPres.Text

        End If

        txtCliente.Visible = False
        cmbClientes.Visible = True

        dtpFECHA.Value = Date.Today
        dtpFECHA.Focus()

        BtnRemito.Enabled = False
        lblEstado.Visible = False

        BtnFactura.Enabled = False

        chkAmpliarGrillaInferior.Enabled = False
        chkAnulados.Enabled = False
        chkPresupuestosCumplidos.Enabled = False

        btnGenerarOC.Enabled = False
        btnCopiarPres.Enabled = False
        btnAbrirWord.Enabled = True

        band = 1

        TimerGuardar.Enabled = True

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        TimerGuardar.Enabled = False

        Dim res As Integer, res_item As Integer, res_notas As Integer, res_itemsOfertaComercial As Integer, res_OfertaComercial As Integer

        Util.Logueado_OK = False

        Guardando = True

        'If chkNotas.Checked = False Then
        '    If MessageBox.Show("El presupuesto no tiene Notas seleccionadas." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '        Guardando = False
        '        Exit Sub
        '    End If
        'Else
        '    Dim i As Integer, SinNotas As Boolean
        '    For i = 0 To lstNotas.Items.Count - 1
        '        If lstNotas.Items(i).Checked = True Then
        '            SinNotas = True
        '            Exit For
        '        End If
        '    Next

        '    If SinNotas = False Then
        '        If MessageBox.Show("El presupuesto no tiene Notas seleccionadas." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        '            Guardando = False
        '            Exit Sub
        '        End If
        '    End If
        'End If

        If chkRecDescGlobal.Checked = True Then
            If MessageBox.Show("La opción de Recargo/Descuento Glogal, está activa. Esto modificará todos los precios del presupuesto." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Guardando = False
                Exit Sub
            End If
        End If

        If bolModo = False And GuardarDesdeTimer = False Then
            If grd.CurrentRow.Cells(5).Value <> cmbClientes.SelectedValue Then
                If MessageBox.Show("El cliente seleccionado es diferente al cliente que existía orginalmente." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Guardando = False
                    Exit Sub
                End If
            End If
        End If

        If rdTrafo.Checked = True Then

            Dim i As Integer
            Dim Marcado As Boolean = False

            For i = 0 To grdTrafos_Ensayos.RowCount - 1
                If grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True Then
                    Marcado = True
                    Exit For
                End If
            Next

            If Marcado = False Then
                If MessageBox.Show("No ha seleccionado ningún ensayo el Presupuesto de Trafo." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Guardando = False
                    Exit Sub
                End If
            End If

            If txtTrafo_Cabecera.Text = "" Then
                If MessageBox.Show("No ha ingresado un texto para la cabecera del Presupuesto de Trafo." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    txtTrafo_Cabecera.Focus()
                    Guardando = False
                    Exit Sub
                End If
            End If

            If txtTrafo_Observaciones.Text = "" Then
                If MessageBox.Show("No ha ingresado un texto en el campo Observaciones del Presupuesto de Trafo." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    txtTrafo_Observaciones.Focus()
                    Guardando = False
                    Exit Sub
                End If
            End If

            If txtTrafo_CantHoras.Text = "" Then
                If MessageBox.Show("No ha ingresado un texto en el campo Horas de Trabajo del Presupuesto de Trafo." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    txtTrafo_CantHoras.Focus()
                    Guardando = False
                    Exit Sub
                End If
            End If

            If txtSubtotal.Text = "0" Or txtSubtotal.Text = "" Then
                If MessageBox.Show("No ha ingresado el monto para el Presupuesto de Trafo." + vbCrLf + "¿Está seguro que desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    txtSubtotal.Focus()
                    Guardando = False
                    Exit Sub
                End If
            End If

        End If

        If chkManoObra.Checked = True And CDbl(txtSubtotal.Text) > 0 And chkPresupuestoConWord.Checked = False Then
            MsgBox("Considerando que tiene materiales con Precios en el presupuesto de Mano de Obra, " & _
                    "no se mostrará el valor individual de cada ítem de la Mano de Obra. " & vbCrLf & _
                    "Si desea lo contrario, destilde la opción Mano de Obra.", MsgBoxStyle.Information, "Control de Presupuestos")
            chkMostrarPrecioManoObra.Checked = False
        End If

        CaclularMontosFinalesGrillaItems()

        If GuardarDesdeTimer = False And chkPresupuestoConWord.Checked = False Then
            If MessageBox.Show("El monto final del presupuesto es: " & vbCrLf & vbCrLf & _
                           "    Subtotal: " & txtSubtotalPre.Text & vbCrLf & _
                           "    MontoIVA: " & txtIva21Pre.Text & vbCrLf & _
                           "    Total: " & txtTotalPre.Text & vbCrLf & _
                           "    Moneda: " & txtCodMonedaPres.Text & vbCrLf & vbCrLf & _
                           "Desea Continuar?", "Control de Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                If bolModo = False Then
                    ControlarCantidadRegistros()
                End If
                res = AgregarActualizar_Registro()
                Select Case res
                    Case Is <= 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo Insertar el presupuesto (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Insertar el presupuesto (Encabezado).", My.Resources.Resources.stop_error.ToBitmap, True)
                        TerminarGuardar()
                        Exit Sub
                    Case 100
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se puede Insertar el Contacto/Usuario.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se puede Insertar el Contacto/Usuario.", My.Resources.Resources.stop_error.ToBitmap, True)
                        TerminarGuardar()
                        Exit Sub
                End Select

                Util.MsgStatus(Status1, "Guardando los items...", My.Resources.Resources.indicator_white)

                If rdTrafo.Checked = True Then

                    res_item = AgregarRegistro_Trafos_Det()

                    If res_item <= 0 Then
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo Insertar el detalle de los trabajos para el Trafo.", My.Resources.Resources.alert.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo Insertar el detalle de los trabajos para el Trafo.", My.Resources.Resources.alert.ToBitmap, True)
                        TerminarGuardar()
                        Exit Sub
                    Else
                        Util.MsgStatus(Status1, "Guardando los ensayos...", My.Resources.Resources.indicator_white)

                        res_item = AgregarAcutalizar_Registro_Trafos_Ensayos()

                        If res_item <= 0 Then
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Modificar el detalle de los ensayos para el Trafo.", My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Modificar el detalle de los ensayos para el Trafo.", My.Resources.Resources.alert.ToBitmap, True)
                            TerminarGuardar()
                            Exit Sub
                        End If

                    End If

                Else

                    'If chkManoObra.Checked = True Then
                    If grdItems.Rows.Count > 0 Then
                        res_item = AgregarRegistro_Items()
                    Else
                        res_item = 1
                    End If
                    'Else
                    'End If

                    Select Case res_item
                        Case -10
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se puede modificar el material. La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap)
                            TerminarGuardar()
                            Exit Sub
                        Case -20
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "Error al actualizar las cantidades. La nueva cantidad es menor al saldo actual", My.Resources.Resources.stop_error.ToBitmap)
                            TerminarGuardar()
                            Exit Sub
                        Case -30
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Insertar el Material", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Insertar el Material", My.Resources.Resources.stop_error.ToBitmap, True)
                            TerminarGuardar()
                            Exit Sub
                        Case -40
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Insertar la nueva Marca", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Insertar la nueva Marca", My.Resources.Resources.stop_error.ToBitmap, True)
                            TerminarGuardar()
                            Exit Sub
                        Case -5
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal los items", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal los items", My.Resources.Resources.stop_error.ToBitmap, True)
                            TerminarGuardar()
                            Exit Sub
                        Case -6
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Insertar el detalle de items.", My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Insertar el detalle de items.", My.Resources.Resources.alert.ToBitmap, True)
                            TerminarGuardar()
                            Exit Sub
                        Case Is <= 0
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Agregar el registro (Items).", My.Resources.Resources.stop_error.ToBitmap, True)
                            TerminarGuardar()
                            Exit Sub
                        Case Else

                            If chkAgregarOfertaComercial.Checked = True Then
                                res_OfertaComercial = AgregarRegistro_OfertaComercial()
                                Select Case res_OfertaComercial
                                    Case -2
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo Insertar el encabezado de la oferta técnica", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo Insertar el encabezado de la oferta técnica", My.Resources.Resources.stop_error.ToBitmap, True)
                                        TerminarGuardar_2()
                                        Exit Sub
                                    Case -1
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo Insertar el encabezado de la oferta técnica.", My.Resources.Resources.alert.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo Insertar el encabezado de la oferta técnica.", My.Resources.Resources.alert.ToBitmap, True)
                                        TerminarGuardar_2()
                                        Exit Sub
                                    Case 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo Insertar el encabezado de la oferta técnica.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo Insertar el encabezado de la oferta técnica.", My.Resources.Resources.stop_error.ToBitmap, True)
                                        TerminarGuardar_2()
                                        Exit Sub
                                    Case Else
                                        res_itemsOfertaComercial = AgregarRegistro_Items_OfertaComercial()
                                        Select Case res_itemsOfertaComercial
                                            Case -20
                                                Cancelar_Tran()
                                                TerminarGuardar_2()
                                                Exit Sub
                                            Case -2
                                                Cancelar_Tran()
                                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de la oferta técnica", My.Resources.Resources.stop_error.ToBitmap)
                                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de la oferta técnica", My.Resources.Resources.stop_error.ToBitmap, True)
                                                TerminarGuardar_2()
                                                Exit Sub
                                            Case -1
                                                Cancelar_Tran()
                                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de la oferta técnica.", My.Resources.Resources.alert.ToBitmap)
                                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de la oferta técnica.", My.Resources.Resources.alert.ToBitmap, True)
                                                TerminarGuardar_2()
                                                Exit Sub
                                            Case 0
                                                Cancelar_Tran()
                                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de la oferta técnica.", My.Resources.Resources.stop_error.ToBitmap)
                                                Util.MsgStatus(Status1, "No se pudo Insertar el detalle de la oferta técnica.", My.Resources.Resources.stop_error.ToBitmap, True)
                                                TerminarGuardar_2()
                                                Exit Sub
                                        End Select
                                End Select

                            End If
                    End Select

                End If

                If chkNotas.Checked = True Then
                    res_notas = AgregarRegistro_Notas()
                    Select Case res_notas
                        Case -2
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal las notas", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Eliminar de manera temporal las notas", My.Resources.Resources.stop_error.ToBitmap, True)
                            TerminarGuardar_2()
                            Exit Sub
                        Case -1
                            Cancelar_Tran()
                            Util.MsgStatus(Status1, "No se pudo Insertar el detalle de las notas seleccionadas.", My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "No se pudo Insertar el detalle de las notas seleccionadas.", My.Resources.Resources.alert.ToBitmap, True)
                            TerminarGuardar_2()
                            Exit Sub
                    End Select
                End If

                If bolModo = False Then
                    If rdTrafo.Checked = False Then
                        EliminarItems_Presupuesto()
                        If grd.CurrentRow.Cells(42).Value = True Then ' And chkAgregarOfertaComercial.Checked = True Then
                            EliminarItems_Presupuesto_OfertaComercial()
                        End If
                    Else
                        EliminarItems_Presupuesto_Trafos()
                    End If
                End If


                If AbrirWord = True Then

                    Util.MsgStatus(Status1, "Generando el archivo de WORD, espere un instante por favor.", My.Resources.Resources.ok.ToBitmap)

                    Dim MSWord As New Microsoft.Office.Interop.Word.Application
                    Dim Documento As Microsoft.Office.Interop.Word.Document

                    'If bolModo = True Then
                    '    FileCopy(Application.StartupPath.ToString & "\Plataforma Presupuesto Original\Plataforma de Presupuestos.docx", Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                    'End If

                    Try
                        If My.Computer.FileSystem.FileExists(Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx") Then
                            If IsFileOpen(Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx") Then
                                MsgBox("El archivo se encuentra abierto", MsgBoxStyle.Critical, "Archivo ABIERTO")
                                Cancelar_Tran()
                                Exit Sub
                            Else
                                Documento = MSWord.Documents.Open(Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                            End If
                        Else
                            If MessageBox.Show("¿El archivo Word no existe. Desea generarlo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                FileCopy(Application.StartupPath.ToString & "\Plataforma Presupuesto Original\Plataforma de Presupuestos.docx", Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                                Documento = MSWord.Documents.Open(Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                            Else
                                Cancelar_Tran()
                                Exit Sub
                            End If
                        End If
                    Catch ex As Exception
                        If ex.Message.Contains("no hemos podido encontrar") And bolModo = False Then
                            If MessageBox.Show("¿El archivo Word no existe. Desea generarlo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                FileCopy(Application.StartupPath.ToString & "\Plataforma Presupuesto Original\Plataforma de Presupuestos.docx", Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                                Documento = MSWord.Documents.Open(Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                            Else
                                Cancelar_Tran()
                                Exit Sub
                            End If
                        Else
                            If bolModo = True Then
                                FileCopy(Application.StartupPath.ToString & "\Plataforma Presupuesto Original\Plataforma de Presupuestos.docx", Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                                Documento = MSWord.Documents.Open(Application.StartupPath.ToString & "\Presupuestos desde Plataforma SEI\" & txtCODIGO.Text & " - " & IIf(txtCliente.Text = "", LTrim(RTrim(cmbClientes.Text.ToString)), LTrim(RTrim(txtCliente.Text))) & ".docx")
                            Else
                                MsgBox(ex.Message)
                                Cancelar_Tran()
                                Exit Sub
                            End If
                        End If
                    End Try

                    Dim bmRange As Microsoft.Office.Interop.Word.Range = Nothing

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("NroPresupuesto").Range
                        bmRange.Text = txtCODIGO.Text.ToString
                        Documento.Bookmarks.Add("NroPresupuesto", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("NroPresupuesto2").Range
                        bmRange.Text = txtCODIGO.Text.ToString
                        Documento.Bookmarks.Add("NroPresupuesto2", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Cliente").Range
                        bmRange.Text = IIf(txtCliente.Text = "", cmbClientes.Text.ToString, txtCliente.Text)
                        Documento.Bookmarks.Add("Cliente", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Cliente2").Range
                        bmRange.Text = IIf(txtCliente.Text = "", cmbClientes.Text.ToString, txtCliente.Text)
                        Documento.Bookmarks.Add("Cliente2", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Referencia").Range
                        bmRange.Text = txtNombre.Text
                        Documento.Bookmarks.Add("Referencia", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Referencia2").Range
                        bmRange.Text = txtNombre.Text
                        Documento.Bookmarks.Add("Referencia2", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Referencia3").Range
                        bmRange.Text = txtNombre.Text
                        Documento.Bookmarks.Add("Referencia3", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Referencia4").Range
                        bmRange.Text = txtNombre.Text
                        Documento.Bookmarks.Add("Referencia4", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Usuario").Range
                        bmRange.Text = cmbUsuarios.Text
                        Documento.Bookmarks.Add("Usuario", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Req").Range
                        bmRange.Text = txtReq.Text
                        Documento.Bookmarks.Add("Req", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("dia").Range
                        bmRange.Text = dtpFECHA.Value.Day
                        Documento.Bookmarks.Add("dia", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Mes").Range
                        bmRange.Text = MonthName(dtpFECHA.Value.Month, False)
                        Documento.Bookmarks.Add("Mes", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("ano").Range
                        bmRange.Text = dtpFECHA.Value.Year
                        Documento.Bookmarks.Add("ano", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("LugarEntrega").Range
                        bmRange.Text = cmbEntregaren.Text
                        Documento.Bookmarks.Add("LugarEntrega", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("LugarEntrega2").Range
                        bmRange.Text = cmbEntregaren.Text
                        Documento.Bookmarks.Add("LugarEntrega2", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Garantia").Range
                        bmRange.Text = txtgarantia.Text
                        Documento.Bookmarks.Add("Garantia", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("TextoGarantia").Range
                        bmRange.Text = letras(txtgarantia.Text)
                        Documento.Bookmarks.Add("TextoGarantia", bmRange)
                    Catch ex As Exception

                    End Try


                    If cmbMonedas.Text.ToString = "DOLAR" Then
                        Try
                            bmRange = Nothing
                            bmRange = Documento.Bookmarks.Item("CodigoMoneda").Range
                            bmRange.Text = "u$$"
                            Documento.Bookmarks.Add("CodigoMoneda", bmRange)
                        Catch ex As Exception

                        End Try

                        Try
                            bmRange = Nothing
                            bmRange = Documento.Bookmarks.Item("TextoMoneda").Range
                            bmRange.Text = "dólares estadounidenses"
                            Documento.Bookmarks.Add("TextoMoneda", bmRange)
                        Catch ex As Exception

                        End Try

                        Try
                            bmRange = Nothing
                            bmRange = Documento.Bookmarks.Item("TextoMoneda2").Range
                            bmRange.Text = "dólares estadounidenses"
                            Documento.Bookmarks.Add("TextoMoneda2", bmRange)
                        Catch ex As Exception

                        End Try

                    Else
                        Try
                            bmRange = Nothing
                            bmRange = Documento.Bookmarks.Item("CodigoMoneda").Range
                            bmRange.Text = "$"
                            Documento.Bookmarks.Add("CodigoMoneda", bmRange)
                        Catch ex As Exception

                        End Try

                        Try
                            bmRange = Nothing
                            bmRange = Documento.Bookmarks.Item("TextoMoneda").Range
                            bmRange.Text = "pesos argentinos"
                            Documento.Bookmarks.Add("TextoMoneda", bmRange)
                        Catch ex As Exception

                        End Try

                        Try
                            bmRange = Nothing
                            bmRange = Documento.Bookmarks.Item("TextoMoneda2").Range
                            bmRange.Text = "pesos argentinos"
                            Documento.Bookmarks.Add("TextoMoneda2", bmRange)
                        Catch ex As Exception

                        End Try

                    End If

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("Subtotal").Range
                        bmRange.Text = txtSubtotalPre.Text
                        Documento.Bookmarks.Add("Subtotal", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("TextoPrecio").Range
                        bmRange.Text = letras(txtSubtotalPre.Text)
                        Documento.Bookmarks.Add("TextoPrecio", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("FormadePago").Range
                        bmRange.Text = cmbFormaPago.Text
                        Documento.Bookmarks.Add("FormadePago", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("ValidezOferta").Range
                        bmRange.Text = txtValidez.Text
                        Documento.Bookmarks.Add("ValidezOferta", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("TextoValidezOferta").Range
                        bmRange.Text = letras(txtValidez.Text)
                        Documento.Bookmarks.Add("TextoValidezOferta", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("NombreUsuario").Range
                        bmRange.Text = Util.NombreUsuario
                        Documento.Bookmarks.Add("NombreUsuario", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("NombreUsuario1").Range
                        bmRange.Text = Util.NombreUsuario
                        Documento.Bookmarks.Add("NombreUsuario1", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("NombreUsuario2").Range
                        bmRange.Text = Util.NombreUsuario
                        Documento.Bookmarks.Add("NombreUsuario2", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("CelularUsuario").Range
                        bmRange.Text = Util.CelularUsuario
                        Documento.Bookmarks.Add("CelularUsuario", bmRange)
                    Catch ex As Exception

                    End Try


                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("EmailUsuario").Range
                        bmRange.Text = Util.EmailUsuario
                        Documento.Bookmarks.Add("EmailUsuario", bmRange)
                    Catch ex As Exception

                    End Try

                    Try
                        bmRange = Nothing
                        bmRange = Documento.Bookmarks.Item("EmailUsuario1").Range
                        bmRange.Text = Util.EmailUsuario
                        Documento.Bookmarks.Add("EmailUsuario1", bmRange)
                    Catch ex As Exception

                    End Try

                    Documento.Save()

                    MSWord.Visible = True

                    AbrirWord = False

                    Util.MsgStatus(Status1, "Operación Finalizada", My.Resources.Resources.alert.ToBitmap)

                End If

                Cerrar_Tran()

                LlenarcmbMarcasProductos()

                If rdTrafo.Checked = False Then
                    ActualizarSubtotal()
                End If

                band = 0

                btnBand_Copiar = True

                If rdTrafo.Checked = False Then

                    SQL = "exec spPresupuestos_Select_All  @Eliminado = 0, @Estado = '', @Codigo = '" & txtCODIGO.Text & "'"

                    Dim posicion As Integer

                    posicion = grd.CurrentRow.Index

                    If bolModo = False Then
                        'bolModo = False
                        If GuardarDesdeTimer = True Or chkPresupuestoConWord.Checked = True Then
                            btnActualizar_Click(sender, e)
                        Else
                            Imprimir(txtCODIGO.Text)
                            btnActualizar_Click(sender, e)
                        End If
                        grd.CurrentCell = grd(6, posicion)
                        CargarCajas()
                    Else
                        bolModo = False
                        If GuardarDesdeTimer = True Or chkPresupuestoConWord.Checked = True Then
                            btnActualizar_Click(sender, e)
                            grd.CurrentCell = grd(6, posicion)
                            CargarCajas()
                        Else
                            btnActualizar_Click(sender, e)
                            grd.CurrentCell = grd(6, posicion)
                            CargarCajas()
                            Imprimir(txtCODIGO.Text)
                        End If

                    End If
                Else
                    bolModo = False
                End If

                Permitir = True
                PrepararBotones()

                chkAmpliarGrillaInferior.Enabled = True
                chkAnulados.Enabled = True
                chkPresupuestosCumplidos.Enabled = True

                btnGenerarOC.Enabled = True

                Presupuestos_Vencidos()

                Util.MsgStatus(Status1, "El Presupuesto se generó correctamente el " & Date.Today.Date & " a las " & Format(TimeOfDay, "hh:mm:ss"), My.Resources.Resources.ok.ToBitmap)

                LlenarcmbClientes_Comprador()

                Try
                    txtIdComprador.Text = grd.CurrentRow.Cells(ColumnasDelGrid.IdComprador_p).Value
                    cmbCompradores.Text = grd.CurrentRow.Cells(ColumnasDelGrid.NombreComprador_p).Value
                Catch ex As Exception

                End Try

                band = 1

            End If
        End If

        TimerGuardar.Enabled = True

    End Sub

    Private Function IsFileOpen(filePath As String) As Boolean
        Dim rtnvalue As Boolean = False
        Try
            Dim fs As System.IO.FileStream = System.IO.File.OpenWrite(filePath)
            fs.Close()
        Catch ex As System.IO.IOException
            rtnvalue = True
        End Try
        Return rtnvalue
    End Function

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim res As Integer

        Dim registro As Integer 'DataGridViewRow
        registro = grd.CurrentRow.Index

        If Not bolModo Then
            If MessageBox.Show("¿Está seguro que desea Anular este presupuesto?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Util.MsgStatus(Status1, "Anulando el registro...", My.Resources.Resources.indicator_white)
                res = EliminarRegistro()
                Select Case res
                    Case -1
                        Util.MsgStatus(Status1, "No se puede anular el presupuesto solicitado.", My.Resources.stop_error.ToBitmap)
                    Case -0
                        Util.MsgStatus(Status1, "No se puede anular el presupuesto solicitado.", My.Resources.stop_error.ToBitmap)
                    Case -8
                        Util.MsgStatus(Status1, "El presupuesto tiene remitos asociados, debe anular primero los remitos para después anular este presupuesto. Utilice Historia de Presupuestos, dentro del menú Informes, para ver como está formado el Presupuesto.", My.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El presupuesto tiene remitos asociados, debe anular primero los remitos para después anular este presupuesto. Utilice Historia de Presupuestos, dentro del menú Informes, para ver como está formado el Presupuesto.", My.Resources.stop_error.ToBitmap, True)
                    Case Else
                        PrepararBotones()
                        btnActualizar_Click(sender, e)
                        'Setear_Grilla()
                        grd.Rows(registro).Selected = True
                        grd.CurrentCell = grd.Rows(registro).Cells(1)
                        Util.MsgStatus(Status1, "Se anuló correctamente el presupuesto.", My.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se anuló correctamente el presupuesto.", My.Resources.ok.ToBitmap, True)
                End Select
            Else
                Util.MsgStatus(Status1, "Acción de Anular cancelada.", My.Resources.stop_error.ToBitmap)
            End If
            'Else
            '    Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap)
            '    Util.MsgStatus(Status1, "El registro ya está eliminado.", My.Resources.stop_error.ToBitmap, True)
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        TimerGuardar.Enabled = False

        nbreformreportes = "Presupuesto"

        Dim param As New frmParametros
        Dim codigopres As String
        Dim Rpt As New frmReportes
        Dim Rpt1 As New frmReportes
        Dim OfertacomercialBIT As Boolean, trafo As Boolean, ManoObra As Boolean

        param.AgregarParametros("N° de Presup.:", "STRING", "", False, txtCODIGO.Text.ToString, "", conexionGenerica)

        param.ShowDialog()
        If cerroparametrosconaceptar = True Then
            codigopres = param.ObtenerParametros(0)

            Cursor = Cursors.WaitCursor

            Rpt.NombreArchivoPDF = BuscarNombreArchivo(codigopres, OfertacomercialBIT, trafo, ManoObra)
            Rpt.MailDestinatario = txtMailComprador.Text

            If trafo = True Then
                nbreformreportes = "Presupuesto Mantenimiento Transformador"
                Rpt.NombreArchivoPDF = Rpt.NombreArchivoPDF + " - Trafo"
                Rpt.SEI_Trafos(codigopres, Rpt)
                nbreformreportes = "Presupuesto Mantenimiento Transformador - Ensayos"
                Rpt1.NombreArchivoPDF = Rpt.NombreArchivoPDF + " - Trafo Ensayos"
                Rpt1.SEI_Trafos_Ensayos(codigopres, Rpt1)
            Else
                If OfertacomercialBIT = True Then
                    If ManoObra = False Then
                        nbreformreportes = "Oferta Comercial"
                        Rpt.NombreArchivoPDF = Rpt.NombreArchivoPDF + " - Oferta Comercial"
                        Rpt.SEI_OfertaComercial(codigopres, Rpt, ManoObra)
                    Else
                        nbreformreportes = "Mano Obra"
                        Rpt.NombreArchivoPDF = Rpt.NombreArchivoPDF + " - Mano Obra"
                        Rpt.SEI_OfertaComercial(codigopres, Rpt, ManoObra)
                    End If
                Else
                    Rpt.Presupuesto_App(codigopres, Rpt, My.Application.Info.AssemblyName.ToString)
                End If
            End If

            cerroparametrosconaceptar = False
            param = Nothing

            Cursor = Cursors.Default

        End If

        TimerGuardar.Enabled = True

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        TimerGuardar.Enabled = False

        If txtID.Text <> "" Then

            LlenarGrid_Items(CType(txtID.Text, Long))
            Contar_Filas()
            lblEstado.Visible = CBool(grd.CurrentRow.Cells(ColumnasDelGrid.Status_p).Value)
            BtnFactura.Enabled = Not bolModo

            chkAmpliarGrillaInferior.Enabled = True
            chkAnulados.Enabled = True

            btnGenerarOC.Enabled = True
            btnCopiarPres.Enabled = True

            cmbClientes.Visible = False
            txtCliente.Visible = True

            btnBand_Copiar = True

        End If

        TimerGuardar.Enabled = True

    End Sub

    Private Sub btnAsignarTablero_Click(sender As Object, e As EventArgs) Handles btnAsignarTablero.Click
        grdItems.Focus()
        If grdItems.RowCount = 1 Then
            grdItems.Rows(0).Cells(ColumnasDelGridItems.Cod_Material).Selected = True
        Else
            grdItems.Rows(grdItems.RowCount - 1).Cells(ColumnasDelGridItems.Cod_Material).Selected = True
        End If
        grdItems.EditMode = DataGridViewEditMode.EditOnEnter
        frmTableros.bandera_boton = True
        frmTableros.ShowDialog()
        Agregar_Materiales_Tablero()
        grdItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
    End Sub

    Private Sub btnCopiarPres_Click(sender As Object, e As EventArgs) Handles btnCopiarPres.Click
        Dim UltId As Long, IdCliente As Long

        If MessageBox.Show("Desea copiar el Presupuesto actual, para generar uno nuevo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            UltId = txtID.Text
            IdCliente = txtIdCliente.Text

            btnBand_Copiar = False
            btnNuevo_Click(sender, e)

            txtID.Text = ""
            txtCODIGO.Text = ""

            cmbClientes.SelectedValue = idcliente

        End If

    End Sub

    Private Sub btnAbrirWord_Click(sender As Object, e As EventArgs) Handles btnAbrirWord.Click

        Cursor = Cursors.WaitCursor

        AbrirWord = True
        chkPresupuestoConWord.Checked = True

        btnGuardar_Click(sender, e)

        AbrirWord = False

        Cursor = Cursors.Default

    End Sub

    Private Sub BtnRemito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRemito.Click

        btnGuardar_Click(sender, e)
        Dim f As New frmRemitos
        f.Origen = 1
        f.Origen_IdCliente = cmbClientes.SelectedValue
        f.Origen_IdPresupuesto = txtID.Text
        f.Origen_Comprador = chkComprador.Checked
        f.chkRemitoEspecial.Enabled = False
        If chkComprador.Checked = True Then
            f.Origen_IdComprador = txtIdComprador.Text 'cmbComprador.SelectedValue
        End If
        f.Origen_Usuario = chkUsuario.Checked
        If chkUsuario.Checked = True Then
            f.Origen_IdUsuario = txtIdUsuario.Text ' cmbUsuario.SelectedValue
        End If

        f.Origen_Presupuesto = Origen_Presupuesto

        f.Origen_OC = LTrim(RTrim(txtNroOC.Text))
        f.MdiParent = MDIPrincipal
        f.Show()

    End Sub

    Private Sub btnGenerarOC_Click(sender As Object, e As EventArgs) Handles btnGenerarOC.Click
        'Dim f As New frmOrdenCompra
        'f.Origen = 1
        'f.Origen_IdCliente = IIf(txtIdCliente.Text = "", 0, txtIdCliente.Text) ''cmbCliente.SelectedValue
        'f.Origen_Id = txtID.Text
        'f.Origen_Referencia = txtNombre.Text
        'f.Origen_IdMoneda = txtIdMonedaPres.Text
        'f.Origen_CodMoneda = txtCodMonedaPres.Text
        'f.TotalPre = txtTotal.Text
        'f.NroPresupuesto = txtCODIGO.Text
        'f.MdiParent = MDIPrincipal
        'f.Show()
    End Sub

#End Region

#Region "Procedimientos"

    Public Sub New()

        InitializeComponent()

    End Sub

    Private Sub configurarform()
        Me.Text = "Generador de Presupuestos..."

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 5)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = 0 ' (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 3 - GroupBox1.Size.Height - GroupBox1.Location.Y - 62) '65)

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
        If Not (cmbClientes.SelectedIndex > -1) Then
            Util.MsgStatus(Status1, "Ingrese un valor en 'Cliente'.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Ingrese un valor en 'Cliente'.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If chkAnticipo.Checked = True Then
            If CDbl(IIf(txtAnticipo.Text = "", 0, txtAnticipo.Text)) = 0 Then
                Util.MsgStatus(Status1, "Ingrese un valor para el 'Anticipo'.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "Ingrese un valor para el 'Anticipo'.", My.Resources.Resources.alert.ToBitmap, True)
                txtAnticipo.Focus()
                Exit Sub
            End If
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

        If txtIdCliente.Text = "" Then
            Util.MsgStatus(Status1, "Seleccione nuevamente el Cliente. Se ha producido un error.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Seleccione nuevamente el Cliente. Se ha producido un error.", My.Resources.Resources.alert.ToBitmap, True)
            cmbClientes.Focus()
            Exit Sub
        End If

        If txtIdFormaPago.Text = "" Then
            Util.MsgStatus(Status1, "Seleccione nuevamente la Forma de Pago. Se ha producido un error.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Seleccione nuevamente la Forma de Pago. Se ha producido un error.", My.Resources.Resources.alert.ToBitmap, True)
            cmbFormaPago.Focus()
            Exit Sub
        End If

        If rdTrafo.Checked = True Then

            For i = 0 To grdTrafos_Det.RowCount - 2
                'la fila está vacía ?
                'MsgBox(grdTrafos_Det.Rows(i).Cells(ColumnasDelGridTrafo_Det.Descripcion).Value.ToString)
                If grdTrafos_Det.Rows(i).Cells(ColumnasDelGridTrafo_Det.Descripcion).Value Is DBNull.Value Or _
                    grdTrafos_Det.Rows(i).Cells(ColumnasDelGridTrafo_Det.Descripcion).Value Is Nothing Then
                    Try
                        'encotramos una fila vacia...borrarla y ver si hay mas
                        grdTrafos_Det.Rows.RemoveAt(i)

                        j = j - 1 ' se reduce la cantidad de filas en 1
                        i = i - 1 ' se reduce para recorrer la fila que viene 
                    Catch ex As Exception
                    End Try

                Else
                    filas = filas + 1
                End If
            Next

        Else

            If chkManoObra.Checked = False Then
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
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.ToLower = "NO EXISTE" Then
                            Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If

                        'Descripcion del material es válida ?
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value Is DBNull.Value Then
                            Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If

                        'Descripcion del material es válida ?
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value = "" Then
                            Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "El material ingresado no es válido en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End If

                        'qty es válida?
                        Try
                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is System.DBNull.Value Then
                                Util.MsgStatus(Status1, "Falta completar la cantidad a Presupuestar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "Falta completar la cantidad a Presupuestar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                                Exit Sub
                            ElseIf grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value <= 0 Then
                                Util.MsgStatus(Status1, "La cantidad a Presupuestar debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                                Util.MsgStatus(Status1, "La cantidad a Presupuestar debe ser mayor a cero en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                                Exit Sub
                            End If
                        Catch ex As Exception
                            Util.MsgStatus(Status1, "Ingrese una cantidad válida a Presupuestar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "Ingrese una cantidad válida a Presupuestar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
                            Exit Sub
                        End Try

                        Try
                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value Is System.DBNull.Value Then
                                If MessageBox.Show("La fila " & i + 1 & " no tiene Ganancia asignada. Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value = 0
                                Else
                                    Exit Sub
                                End If
                            ElseIf grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value <= 0 Then
                                If MessageBox.Show("La fila " & i + 1 & " no tiene Ganancia asignada. Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                    Exit Sub
                                End If
                            End If
                        Catch ex As Exception
                            Util.MsgStatus(Status1, "Ingrese una Ganancia válida a Presupuestar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap)
                            Util.MsgStatus(Status1, "Ingrese una Ganancia válida a Presupuestar en la fila: " & (i + 1).ToString, My.Resources.Resources.alert.ToBitmap, True)
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
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If filas > 0 Then
            bolpoliticas = True
        Else

            If AbrirWord = True And bolModo = True Then
                bolpoliticas = True
                Exit Sub
            Else
                If chkPresupuestoConWord.Checked = True And bolModo = False Then
                    bolpoliticas = True
                    Exit Sub
                End If
            End If

            If chkManoObra.Checked = False And GuardarDesdeTimer = False Then
                If MessageBox.Show("No hay materiales cargados. ¿Desea generar un presupuesto solo de Mano de Obra?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    chkManoObra.Checked = True
                    'chkAgregarOfertaComercial.Checked = True
                    bolpoliticas = True
                    Exit Sub
                End If
            Else
                bolpoliticas = True
                Exit Sub
            End If

            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap, True)

            Exit Sub
        End If

    End Sub

    Private Sub Asignar_Tags()
        txtID.Tag = CStr(ColumnasDelGrid.Id_p)
        txtCODIGO.Tag = CStr(ColumnasDelGrid.Codigo_p)
        txtRevision.Tag = CStr(ColumnasDelGrid.Revision_p)
        dtpFECHA.Tag = CStr(ColumnasDelGrid.Fecha_p)
        txtNombre.Tag = CStr(ColumnasDelGrid.Nombre_p)
        txtIdCliente.Tag = CStr(ColumnasDelGrid.IdCliente_p)
        cmbClientes.Tag = CStr(ColumnasDelGrid.Cliente_p)
        txtCliente.Tag = CStr(ColumnasDelGrid.Cliente_p)
        txtIdComprador.Tag = CStr(ColumnasDelGrid.IdComprador_p)
        cmbCompradores.Tag = CStr(ColumnasDelGrid.NombreComprador_p)
        txtIdUsuario.Tag = CStr(ColumnasDelGrid.IdUsuario_p)
        cmbUsuarios.Tag = CStr(ColumnasDelGrid.NombreUsuario_p)
        cmbEntregaren.Tag = CStr(ColumnasDelGrid.SitioEntrega_p)

        txtIva21.Tag = CStr(ColumnasDelGrid.Iva21DO_p)
        txtIva10.Tag = CStr(ColumnasDelGrid.Iva10DO_p)
        txtSubtotal.Tag = CStr(ColumnasDelGrid.SubtotalDO_p)
        txtTotal.Tag = CStr(ColumnasDelGrid.TotalDO_p)

        chkEliminado.Tag = CStr(ColumnasDelGrid.Eliminado_p)

        chkNotas.Tag = CStr(ColumnasDelGrid.IncluyeNotas_p)
        txtNroOC.Tag = CStr(ColumnasDelGrid.OC_p)
        chkEntrega.Tag = CStr(ColumnasDelGrid.Entregaren_p)
        chkUsuario.Tag = CStr(ColumnasDelGrid.Usuario_p)
        txtReq.Tag = CStr(ColumnasDelGrid.NroReq_p)
        txtValidez.Tag = CStr(ColumnasDelGrid.validez_p)
        chkRecDescGlobal.Tag = CStr(ColumnasDelGrid.RescDescGlobal_p)

        txtporcrecargo.Tag = CStr(ColumnasDelGrid.PorcRecDescGlogal_p)
        cmbAutoriza.Tag = CStr(ColumnasDelGrid.NombreAutoriza_p)
        'cmbVendedores.Tag = CStr(ColumnasDelGrid.NombreVendedor_p)
        txtNotaGestion.Tag = CStr(ColumnasDelGrid.NotaPresupuesto_p)
        chkComprador.Tag = CStr(ColumnasDelGrid.Comprador_p)
        chkAnticipo.Tag = CStr(ColumnasDelGrid.Anticipo_p)
        txtAnticipo.Tag = CStr(ColumnasDelGrid.PorcAnticipo_p)
        chkOCA.Tag = CStr(ColumnasDelGrid.OCA_p)
        txtIdFormaPago.Tag = CStr(ColumnasDelGrid.IdFormaPago_p)
        cmbFormaPago.Tag = CStr(ColumnasDelGrid.FormaPago_p)

        chkAgregarOfertaComercial.Tag = CStr(ColumnasDelGrid.OfertaComercial)

        chkMostrarCodigoMaterial.Tag = CStr(ColumnasDelGrid.MostrarCodigo)

        cmbEstado.Tag = CStr(ColumnasDelGrid.Estado_p)

        rdTrafo.Tag = CStr(ColumnasDelGrid.Trafo)

        txtTrafo_Cabecera.Tag = CStr(ColumnasDelGrid.Trafo_Cabecera)
        txtTrafo_Observaciones.Tag = CStr(ColumnasDelGrid.Trafo_Observaciones)
        txtTrafo_CantHoras.Tag = CStr(ColumnasDelGrid.Trafo_HorasTrabajo)
        txtTrafo_SubtotalEnsayos.Tag = CStr(ColumnasDelGrid.Trafo_SubtotalEnsayos)

        txtMailComprador.Tag = CStr(ColumnasDelGrid.Mail_Comprador)

        txtIdMonedaPres.Tag = CStr(ColumnasDelGrid.IdMonedaPres)

        cmbMonedas.Tag = CStr(ColumnasDelGrid.MonedaPres)

        txtCodMonedaPres.Tag = CStr(ColumnasDelGrid.CodMonedaPres)
        txtValorCambioPres.Tag = CStr(ColumnasDelGrid.ValorCambioPres)

        chkMostrarPrecioMaterial.Tag = CStr(ColumnasDelGrid.MostrarPrecioMat)
        chkMostrarSubtotalMaterialOC.Tag = CStr(ColumnasDelGrid.MostrarSubtotalMatOC)
        chkMostrarSubtotalMOOC.Tag = CStr(ColumnasDelGrid.MostrarSubtotalMOOC)

        chkManoObra.Tag = CStr(ColumnasDelGrid.ManoObra)

        chkMostrarPrecioManoObra.Tag = CStr(ColumnasDelGrid.MostrarPrecioManoObra)

        chkPrecioDistribuidor.Tag = CStr(ColumnasDelGrid.PrecioDistribuidor)
        ChkMostrarTotal.Tag = CStr(ColumnasDelGrid.MostrarTotal)

        chkMostrarPlazoEntrega.Tag = CStr(ColumnasDelGrid.MostrarPlazoEntrega)

        chkPresupuestoConWord.Tag = CStr(ColumnasDelGrid.PresupuestoconWord)

        txtGarantia.Tag = CStr(ColumnasDelGrid.Garantia)

    End Sub

    Private Sub validar_NumerosReales2( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3  
        If columna = ColumnasDelGridItems.Qty Or columna = ColumnasDelGridItems.PrecioVta Or columna = ColumnasDelGridItems.Stock _
            Or columna = ColumnasDelGridItems.PrecioLista Or columna = ColumnasDelGridItems.Iva Or columna = ColumnasDelGridItems.Ganancia Then
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
        End If
    End Sub

    Private Sub validar_NumerosReales2_OfertaTecnica( _
       ByVal sender As Object, _
       ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = grdOfertaComercial.CurrentCell.ColumnIndex

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
        Util.LimpiarGridItems(grdOfertaComercial)
    End Sub

    Private Sub LlenarGrid_Items(ByVal id As Long)

        If grdItems.Columns.Count > 0 Then
            grdItems.Columns.Clear()
        End If

        Select Case Origen_Presupuesto
            Case 0
                SQL = "exec spPresupuestos_Det_Select_By_IDPresupuesto @idpresupuesto = " & id 'txtID.Text
            Case 1
                SQL = "exec spPresupuestos_Det_Select_By_IdConsumo @idconsumo = " & Origen_Id 'txtID.Text
            Case 2
                SQL = "exec spPresupuestos_Det_Select_By_IDOC @idOC = " & Origen_Id 'txtID.Text
        End Select

        GetDatasetItems(grdItems)

        CheckForIllegalCrossThreadCalls = False

        trd = New Thread(AddressOf HiloOcultarColumnasGridItems)
        trd.IsBackground = True
        trd.Start()

        'If txtID.Text <> "" Then
        '    ControlarLista_Notas()
        'End If

        'Volver la fuente de datos a como estaba...
        SQL = "exec spPresupuestos_Select_All @Eliminado = 0, @Estado = '', @Codigo = '" & txtCODIGO.Text & "' "
    End Sub

    Private Sub LlenarGrid_OfertaComercial(ByVal id As Long)

        If grdOfertaComercial.Columns.Count > 0 Then
            grdOfertaComercial.Columns.Clear()
        End If

        SQL = "exec [spPresupuestos_OfertasComerciales_Select_by_Idpresupuesto] @idpresupuesto = " & id 'txtID.Text

        GetDatasetItems(grdOfertaComercial)

        CheckForIllegalCrossThreadCalls = False

        trd = New Thread(AddressOf HiloOcultarColumnasGritOT)
        trd.IsBackground = True
        trd.Start()

        txtAlcanceBreve.Text = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.AlcanceBreve).Value


        chkAjustes.Checked = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Ajuste).Value
        cmbAjustes.Text = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Ajuste_Texto).Value

        chkCertificaciones.Checked = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Certificacion).Value
        cmbCertificaciones.Text = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Certificacion_Texto).Value

        chkPlazoEntrega.Checked = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.PlazoEntrega).Value
        cmbPlazoEntrega.Text = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.PlazoEntrega_Texto).Value

        chkPlazoEntregaProvision.Checked = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.PlazoEntregaProvision).Value
        cmbPlazoEntregaProvision.Text = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.PlazoEntregaProvision_Texto).Value

        txtSubtotalOferta.Text = IIf(grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Subtotal).Value Is Nothing, "0", grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Subtotal).Value)
        txtTotalOferta.Text = IIf(grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Total).Value Is Nothing, "0", grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Total).Value)
        txtIvaOferta.Text = IIf(grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Iva).Value Is Nothing, "21", grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.Iva).Value)
        txtMontoIvaOferta.Text = IIf(grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.MontoIva).Value Is Nothing, "0", grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.MontoIva).Value)

        txtID_OfertaComercial.Text = IIf(grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.IdPresupOT).Value Is Nothing, 0, grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.IdPresupOT).Value)

        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.IdPresupOT).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.IdPresupOT_Det).Visible = False

        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Ajuste).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Ajuste_Texto).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Certificacion).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Certificacion_Texto).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Iva).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.MontoIva).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Orden).Visible = False

        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntrega).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntrega_Texto).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntregaProvision).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntregaProvision_Texto).Visible = False

        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Subtotal).Visible = False
        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Total).Visible = False

        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Descripcion).Width = 600

        'grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PreciosinIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'With grdOfertaComercial
        '    .VirtualMode = False
        '    .AllowUserToAddRows = True
        '    .AllowUserToDeleteRows = True
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        '    .RowsDefaultCellStyle.BackColor = Color.White
        '    .AllowUserToOrderColumns = True
        '    .SelectionMode = DataGridViewSelectionMode.CellSelect
        '    .ForeColor = Color.Black
        'End With
        'With grdOfertaComercial.ColumnHeadersDefaultCellStyle
        '    .BackColor = Color.Black  'Color.BlueViolet
        '    .ForeColor = Color.White
        '    .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        'End With
        'grdOfertaComercial.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        'Volver la fuente de datos a como estaba...

        'If txtIvaOferta.Text <> "" Then
        '    If CDbl(txtIvaOferta.Text) = 0 Then
        '        If txtIva10.Text <> "" Then
        '            txtIva10Pre.Text = txtIva10.Text
        '        End If
        '        If txtIva21.Text <> "" Then
        '            txtIva21Pre.Text = txtIva21.Text
        '        End If
        '    Else
        '        If CDbl(txtIvaOferta.Text) = 10.5 Then
        '            txtIva10Pre.Text = CDbl(txtIva10.Text) + CDbl(txtMontoIvaOferta.Text)
        '        Else

        If txtMontoIvaOferta.Text = "" Then txtMontoIvaOferta.Text = "0"
        If txtIva21.Text = "" Then txtIva21.Text = "0"

        txtIva21Pre.Text = CDbl(txtIva21.Text) + CDbl(txtMontoIvaOferta.Text)
        '    End If
        'End If
        'End If

        If band = 0 Then
            txtSubtotalPre.Text = CDbl(txtSubtotal.Text) + CDbl(txtSubtotalOferta.Text)
            txtTotalPre.Text = CDbl(txtTotal.Text) + CDbl(txtTotalOferta.Text)
        End If

        SQL = "exec spPresupuestos_Select_All @Eliminado = 0, @Estado = '', @Codigo = '" & txtCODIGO.Text & "'"

    End Sub

    Public Sub LlenarGrid_Trafos_Ensayos()
        If grdTrafos_Ensayos.Columns.Count > 0 Then
            grdTrafos_Ensayos.Columns.Clear()
        End If

        If bolModo = True Then
            SQL = "exec spPresupuestos_Trafos_Ensayos_Select_By_IdPresupuesto @IdPresupuesto = 0"
        Else
            SQL = "exec spPresupuestos_Trafos_Ensayos_Select_By_IdPresupuesto @IdPresupuesto = " & grd.CurrentRow.Cells(ColumnasDelGrid.Id_p).Value
        End If

        GetDatasetItems(grdTrafos_Ensayos)

        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Id).Visible = False
        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.IdPresupuesto).Visible = False

        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Item).Width = 50
        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Item).ReadOnly = True

        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Descripcion).ReadOnly = True
        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Descripcion).Width = 550

        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Marcar).Width = 50

        grdTrafos_Ensayos.Columns(ColumnasDelGridTrafo_Ensayos.Precio).Width = 80

        With grdTrafos_Ensayos
            .VirtualMode = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
        End With
        With grdTrafos_Ensayos.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdTrafos_Ensayos.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        Dim i As Integer
        Dim numItem As String

        Dim style As New DataGridViewCellStyle
        style.Font = New Font("TAHOMA", 8, FontStyle.Bold)

        For i = 0 To grdTrafos_Ensayos.RowCount - 1
            numItem = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value.ToString
            If numItem.Length = 1 Then
                grdTrafos_Ensayos.Rows(i).DefaultCellStyle = style
                grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Precio).ReadOnly = True
            End If
        Next

    End Sub

    Public Sub LlenarGrid_Trafos_Det()
        If grdTrafos_Det.Columns.Count > 0 Then
            grdTrafos_Det.Columns.Clear()
        End If

        If txtID.Text = "" Then
            SQL = "exec spPresupuestos_Trafos_Det_Select_By_IdPresupuesto @IdPresupuesto = 0"
        Else
            SQL = "exec spPresupuestos_Trafos_Det_Select_By_IdPresupuesto @IdPresupuesto = " & grd.CurrentRow.Cells(ColumnasDelGrid.Id_p).Value
        End If

        GetDatasetItems(grdTrafos_Det)

        grdTrafos_Det.Columns(ColumnasDelGridTrafo_Det.Id).Visible = False
        grdTrafos_Det.Columns(ColumnasDelGridTrafo_Det.IdPresupuesto).Visible = False

        grdTrafos_Det.Columns(ColumnasDelGridTrafo_Det.Descripcion).Width = 320

        With grdTrafos_Det
            .VirtualMode = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
        End With

        With grdTrafos_Det.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grdTrafos_Det.Font = New Font("TAHOMA", 8, FontStyle.Regular)

    End Sub

    Private Sub GetDatasetItems(ByVal g As DataGridView)
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds_2 = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, SQL)
            ds_2.Dispose()

            'grdItems.DataSource = ds_2.Tables(0).DefaultView
            g.DataSource = ds_2.Tables(0).DefaultView

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

    Private Sub LlenarcmbClientes2()
        Dim ds_Clientes As Data.DataSet
        'Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    llenandoCombo = False
        '    Exit Sub
        'End Try

        Try
            ds_Clientes = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT DISTINCT c.id, RTRIM(LTRIM(c.nombre)) AS NOMBRE FROM Clientes c JOIN Presupuestos p on P.IDCLIENTE = C.ID WHERE p.eliminado = 0 and c.Eliminado = 0 ORDER BY nombre")
            ds_Clientes.Dispose()

            With cmbClientes2
                .DataSource = ds_Clientes.Tables(0).DefaultView
                .DisplayMember = "nombre"
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
        Finally
            'If Not connection Is Nothing Then
            ' CType(connection, IDisposable).Dispose()
            ' End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarLista_Notas()
        Dim ds_Notas As Data.DataSet

        Try

            If txtCodMonedaPres.Text = "PE" Then
                ds_Notas = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT nota, obligatorio, solopeso, solodolar FROM Notas WHERE (((solopeso = 0 and solodolar = 0) " & _
                                                                                        " or obligatorio= 1 ) or SoloPeso = 1) " & _
                                                                                        " and eliminado = 0 ORDER BY Obligatorio DESC, Nota ASC")
            Else
                ds_Notas = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT nota, obligatorio, solopeso, solodolar FROM Notas WHERE (((solopeso = 0 and solodolar = 0) " & _
                                                                                        " or obligatorio= 1 ) or SoloDolar = 1) " & _
                                                                                        " and eliminado = 0 ORDER BY Obligatorio DESC, Nota ASC")
            End If

            ds_Notas.Dispose()

            Dim i As Integer

            lstNotas.Items.Clear()

            Try
                Dim item As New ListViewItem
                For i = 0 To ds_Notas.Tables(0).Rows.Count - 1
                    Dim r As DataRow = ds_Notas.Tables(0).Rows(i)
                    item = lstNotas.Items.Add(CStr(r("nota")))
                    lstNotas.Items(i).Checked = CBool(r("obligatorio"))
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

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

    Private Sub ControlarLista_Notas()
        Dim ds_Notas As Data.DataSet

        Try

            Dim i As Integer, j As Integer

            LlenarLista_Notas()

            If bolModo = False Then
                ds_Notas = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT nota FROM Presupuestos_Notas WHERE idpresupuesto  = " & txtID.Text)
            Else
                ds_Notas = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT nota FROM Notas WHERE obligatorio = 1 and eliminado = 0 ORDER BY Obligatorio DESC, ORDEN asc, Nota ASC ")
            End If

            ds_Notas.Dispose()

            For i = 0 To lstNotas.Items.Count - 1
                lstNotas.Items(i).Checked = False
            Next

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
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Materiales As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds_Materiales = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT codigo, rtrim(nombre) as Nombre FROM Materiales WHERE Eliminado = 0 ORDER BY nombre")
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
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_UnidadesVta As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds_UnidadesVta = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT codigo, NOMBRE as unidad FROM Unidades")
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

    Private Sub LlenarcmbMarcasProductos()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, rtrim(nombre) as Marca FROM Marcas")
            ds_Marcas.Dispose()

            With Me.cmbMarcaCompra.ComboBox
                .DataSource = ds_Marcas.Tables(0).DefaultView
                .DisplayMember = "Marca"
                .ValueMember = "Id"
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
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub VerGrilla()

        Dim p2 As New Size(GroupBox1.Size.Width, 390)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, GroupBox1.Size.Height - 100)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)
        Dim p As New Size(GroupBox1.Size.Width, 150)
        Me.grd.Size = New Size(p)

        Me.grd.Visible = True

    End Sub

    Private Sub OcultarGrilla()
        Dim p2 As New Size(GroupBox1.Size.Width, 560)
        Me.GroupBox1.Size = New Size(p2)

        Dim p3 As New Size(grdItems.Size.Width, 462)
        Me.grdItems.Size = New Size(p3)

        Me.grd.Visible = False
    End Sub

    Private Sub LlenarcmbClientes_Comprador()
        Dim ds As Data.DataSet

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT CC.ID as IdComprador, NOMBRE_CONTACTO as Comprador FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & IIf(txtIdCliente.Text = "", 0, txtIdCliente.Text) & " order by  NOMBRE_CONTACTO asc")

            ds.Dispose()

            With cmbCompradores
                If ds.Tables(0).Rows.Count > 0 Then
                    .DataSource = ds.Tables(0).DefaultView
                    .DisplayMember = "Comprador"
                    .ValueMember = "IdComprador"
                Else
                    .DataSource = Nothing
                End If

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

    Private Sub LlenarcmbClientes_Usuario()
        Dim ds As Data.DataSet

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT CC.ID as IdUsuario, NOMBRE_CONTACTO as Usuario FROM CLIENTES C JOIN CLIENTES_CONTACTO CC ON CC.IDCLIENTE = C.ID WHERE cc.ELIMINADO = 0  AND C.ID = " & IIf(txtIdCliente.Text = "", 0, txtIdCliente.Text) & " order by  NOMBRE_CONTACTO asc")

            ds.Dispose()

            With cmbUsuarios
                If ds.Tables(0).Rows.Count > 0 Then
                    .DataSource = ds.Tables(0).DefaultView
                    .DisplayMember = "Usuario"
                    .ValueMember = "IdUsuario"
                Else
                    .DataSource = Nothing
                End If

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

    Private Sub LlenarcmbEntregar()
        Dim ds As Data.DataSet

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct SitioEntrega FROM Presupuestos ORDER BY SITIOENTREGA")
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
        End Try

    End Sub

    Private Sub LlenarcmbEstados()
        Dim ds As Data.DataSet

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Id, Nombre FROM Estados WHERE Nombre <> 'Cumplido' ORDER BY Nombre")
            ds.Dispose()

            With cmbEstado
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LlenarcmbOfertaComercial()
        Dim ds As Data.DataSet

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct PlazoEntrega_Texto FROM Presupuestos_OfertasComerciales ORDER BY PlazoEntrega_Texto")
            ds.Dispose()

            With cmbPlazoEntrega
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "PlazoEntrega_Texto"
            End With

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct PlazoEntregaProvision_Texto FROM Presupuestos_OfertasComerciales ORDER BY PlazoEntregaProvision_texto")
            ds.Dispose()

            With cmbPlazoEntregaProvision
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "PlazoEntregaProvision_texto"
            End With

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct Certificacion_texto FROM Presupuestos_OfertasComerciales ORDER BY Certificacion_texto")
            ds.Dispose()

            With cmbCertificaciones
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Certificacion_texto"
            End With

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct Ajuste_texto FROM Presupuestos_OfertasComerciales ORDER BY Ajuste_texto")
            ds.Dispose()

            With cmbAjustes
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Ajuste_texto"
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

    Private Sub BuscarPorcentajeRecargo()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT porcrecargo FROM CLIENTES C WHERE id = " & CType(cmbCliente.SelectedValue, Long))

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT porcrecargo FROM CLIENTES C WHERE id = " & CType(txtIdCliente.Text, Long))

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub BuscarCorreoComprador()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT porcrecargo FROM CLIENTES C WHERE id = " & CType(cmbCliente.SelectedValue, Long))

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select ISNULL(email_contacto,'') from clientes c Join Clientes_Contacto CC on cc.idcliente = c.id where cc.id = " & CType(txtIdComprador.Text, Long))

            ds.Dispose()

            txtMailComprador.Text = ds.Tables(0).Rows(0).Item(0).ToString

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

    Private Sub Imprimir(ByVal codigo As String)
        nbreformreportes = "Presupuesto"

        Cursor = Cursors.WaitCursor

        'Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes
        Dim Rpt1 As New frmReportes
        'Dim Rpt2 As New frmReportes
        'Dim Rpt3 As New frmReportes

        Rpt.NombreArchivoPDF = BuscarNombreArchivo(txtCODIGO.Text, 0, 0, 0)
        Rpt.MailDestinatario = txtMailComprador.Text

        If rdTrafo.Checked = True Then
            nbreformreportes = "Presupuesto Mantenimiento Transformador"
            Rpt.NombreArchivoPDF = Rpt.NombreArchivoPDF + " - Trafo"
            Rpt.SEI_Trafos(codigo, Rpt1)
            nbreformreportes = "Presupuesto Mantenimiento Transformador - Ensayos"
            Rpt1.NombreArchivoPDF = Rpt.NombreArchivoPDF + " - Trafo Ensayos"
            Rpt1.SEI_Trafos_Ensayos(codigo, Rpt1)
        Else
            If chkAgregarOfertaComercial.Checked = True Then
                If chkManoObra.Checked = False Then
                    nbreformreportes = "Oferta Comercial"
                    Rpt.SEI_OfertaComercial(codigo, Rpt, False)
                Else
                    nbreformreportes = "Mano de Obra"
                    Rpt.SEI_OfertaComercial(codigo, Rpt, True)
                End If
            Else
                Rpt.Presupuesto_App(codigo, Rpt, My.Application.Info.AssemblyName.ToString)
            End If

        End If

        Cursor = Cursors.Default

        'cnn = Nothing

    End Sub

    Private Sub Contar_Filas()

        If rdTrafo.Checked = True Then Exit Sub

        Dim j As Integer = 0

        If grdItems.RowCount = 0 Then Exit Sub

        txtTotal.Text = "0"
        txtSubtotal.Text = "0"
        txtIva21.Text = "0"
        txtIva10.Text = "0"

        For j = 0 To grdItems.Rows.Count - 2
            If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value Is DBNull.Value) Then
                If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value = "") Then
                    If Not (IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.Nombre_Material).Value Is DBNull.Value, "", grdItems.Rows(j).Cells(ColumnasDelGridItems.Nombre_Material).Value) = "") Then
                        txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value))
                        If grdItems.Rows(j).Cells(ColumnasDelGridItems.Iva).Value = 21 Then
                            txtIva21.Text = CDbl(txtIva21.Text) + CDbl(IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.MontoIva).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.MontoIva).Value * IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value)))
                        Else
                            txtIva10.Text = CDbl(txtIva10.Text) + CDbl(IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.MontoIva).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.MontoIva).Value * IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.Qty).Value)))
                        End If
                        txtTotal.Text = Math.Round(CDbl(txtSubtotal.Text) + CDbl(txtIva10.Text) + CDbl(txtIva21.Text), 2)
                    End If
                End If
            End If
        Next

        'If txtIvaOferta.Text <> "" Then
        '    If CDbl(txtIvaOferta.Text) = 0 Then
        '        If txtIva10.Text <> "" Then
        '            txtIva10Pre.Text = txtIva10.Text
        '        End If
        '        If txtIva21.Text <> "" Then
        '            txtIva21Pre.Text = txtIva21.Text
        '        End If
        '    Else
        '        If CDbl(txtIvaOferta.Text) = 10.5 Then
        '            txtIva10Pre.Text = CDbl(txtIva10.Text) + CDbl(txtMontoIvaOferta.Text)
        '        Else
        txtIva21Pre.Text = CDbl(txtIva21.Text) + CDbl(txtMontoIvaOferta.Text)
        '        End If
        '    End If
        'End If

        txtSubtotalPre.Text = CDbl(txtSubtotal.Text) + CDbl(txtSubtotalOferta.Text)
        txtTotalPre.Text = CDbl(txtTotal.Text) + CDbl(txtTotalOferta.Text)

        lblCantidadFilas.Text = grdItems.Rows.Count - 1 'j.ToString '+ " / 16"

    End Sub

    Private Sub Calcular_RecargoDescuento()
        If band = 1 Then
            If txtporcrecargo.Text = "" Then
                txtporcrecargo.Text = "0"
            End If
            If grdItems.Rows.Count > 1 Then
                Band_RecDesc = True
                CaclularMontosFinalesGrillaItems()
                Band_RecDesc = False
            End If
        End If
    End Sub

    Private Sub ActualizarSubtotal()

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idPresupuesto"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = txtID.Text
            param_id.Direction = ParameterDirection.Input

            Try
                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_ActualizarSubtotales", _
                                          param_id)

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

    End Sub

    Private Sub BuscarProducto()
        'If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
        LLAMADO_POR_FORMULARIO = True

        Dim f As New frmMaterialesPrecios

        f.Width = 1200
        f.Height = 650
        f.StartPosition = FormStartPosition.CenterScreen
        f.grd.Width = 1150
        f.grd.Height = 350
        f.DesdePre = 1
        f.FilaCodigo = Cell_Y 'e.RowIndex
        f.rdPrecioDist.Checked = chkPrecioDistribuidor.Checked
        f.CodMonedaPres = txtCodMonedaPres.Text
        f.ValorCambioDolar = ValorcambioDolar

        f.ShowDialog(Me)

        chkOcultarGanancia.Checked = True

        grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Ganancia, grdItems.CurrentRow.Index)

        Try
            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Stock).Value = 0 Then
                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Red
            Else
                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Stock).Value <= grdItems.CurrentRow.Cells(ColumnasDelGridItems.Minimo).Value Then
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Yellow
                Else
                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.White
                End If
            End If

            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ubicacion).Value.ToString.Length > 6 Then
                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Style.BackColor = Color.LightGreen
                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material_Prov).Style.BackColor = Color.LightGreen
                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub HiloOcultarColumnasGrid()

        Try

            'Contar_Filas()

            'BtnRemito.Enabled = Not bolModo
            'BtnFactura.Enabled = Not bolModo

            grd.Columns(ColumnasDelGrid.IdCliente_p).Visible = False
            grd.Columns(ColumnasDelGrid.Id_p).Visible = False
            grd.Columns(ColumnasDelGrid.IdAutoriza_p).Visible = False
            grd.Columns(ColumnasDelGrid.IdComprador_p).Visible = False
            grd.Columns(ColumnasDelGrid.IdFormaPago_p).Visible = False
            grd.Columns(ColumnasDelGrid.IdUsuario_p).Visible = False
            grd.Columns(ColumnasDelGrid.IdVendedor_p).Visible = False
            grd.Columns(ColumnasDelGrid.IncluyeNotas_p).Visible = False

            grd.Columns(ColumnasDelGrid.Iva10DO_p).Visible = False
            grd.Columns(ColumnasDelGrid.Iva21DO_p).Visible = False
            grd.Columns(ColumnasDelGrid.SubtotalDO_p).Visible = False
            grd.Columns(ColumnasDelGrid.TotalDO_p).Visible = False

            grd.Columns(ColumnasDelGrid.Iva10Pre).Visible = False
            grd.Columns(ColumnasDelGrid.FormaPago_p).Visible = False

            grd.Columns(ColumnasDelGrid.PorcRecDescGlogal_p).Visible = False
            grd.Columns(ColumnasDelGrid.PorcAnticipo_p).Visible = False
            grd.Columns(ColumnasDelGrid.RescDescGlobal_p).Visible = False

            grd.Columns(ColumnasDelGrid.Comprador_p).Visible = False
            grd.Columns(ColumnasDelGrid.Usuario_p).Visible = False

            grd.Columns(ColumnasDelGrid.NombreAutoriza_p).Visible = False
            grd.Columns(ColumnasDelGrid.NombreVendedor_p).Visible = False

            grd.Columns(ColumnasDelGrid.Anticipo_p).Visible = False
            grd.Columns(ColumnasDelGrid.OCA_p).Visible = False

            grd.Columns(ColumnasDelGrid.Status_p).Visible = False
            grd.Columns(ColumnasDelGrid.Entregaren_p).Visible = False

            grd.Columns(ColumnasDelGrid.Vencido).Visible = False

            grd.Columns(ColumnasDelGrid.Trafo_Cabecera).Visible = False
            grd.Columns(ColumnasDelGrid.Trafo_Observaciones).Visible = False
            grd.Columns(ColumnasDelGrid.Trafo_HorasTrabajo).Visible = False
            grd.Columns(ColumnasDelGrid.Trafo_SubtotalEnsayos).Visible = False

            grd.Columns(ColumnasDelGrid.IdMonedaPres).Visible = False
            grd.Columns(ColumnasDelGrid.Mail_Comprador).Visible = False

            grd.Columns(ColumnasDelGrid.Trafo).Visible = False
            grd.Columns(ColumnasDelGrid.OfertaComercial).Visible = False
            grd.Columns(ColumnasDelGrid.MostrarCodigo).Visible = False

            grd.Columns(ColumnasDelGrid.NombreUsuario_p).Visible = False

            grd.Columns(ColumnasDelGrid.OfertaComercial).Visible = False

            grd.Columns(ColumnasDelGrid.SitioEntrega_p).Visible = False

            grd.Columns(ColumnasDelGrid.MostrarPrecioMat).Visible = False
            grd.Columns(ColumnasDelGrid.MostrarSubtotalMatOC).Visible = False
            grd.Columns(ColumnasDelGrid.MostrarSubtotalMOOC).Visible = False

            grd.Columns(ColumnasDelGrid.ManoObra).Visible = False
            grd.Columns(ColumnasDelGrid.SubtotalOfertaComercial).Visible = False
            grd.Columns(ColumnasDelGrid.MostrarPrecioManoObra).Visible = False
            grd.Columns(ColumnasDelGrid.PrecioDistribuidor).Visible = False

            grd.Columns(ColumnasDelGrid.Revision_p).Visible = False

            grd.Columns(ColumnasDelGrid.PresupuestoconWord).Visible = False

            lblCantidadFilas.Text = grdItems.Rows.Count - 1

        Catch ex As Exception

        End Try

    End Sub

    Private Sub HiloOcultarColumnasGridItems()
        Try

            grdItems.Columns(ColumnasDelGridItems.IdPresup_Det).Visible = False

            grdItems.Columns(ColumnasDelGridItems.IDMaterial).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Cod_Material).Width = 80

            grdItems.Columns(ColumnasDelGridItems.Cod_Material_Prov).Width = 80

            grdItems.Columns(ColumnasDelGridItems.Nombre_Material).Width = 250
            'grdItems.Columns(ColumnasDelGridItems.Nombre_Material).SortMode = DataGridViewColumnSortMode.NotSortable

            grdItems.Columns(ColumnasDelGridItems.IDUnidad).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).Width = 45
            grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            grdItems.Columns(ColumnasDelGridItems.Unidad).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Stock).ReadOnly = True 'stock'
            grdItems.Columns(ColumnasDelGridItems.Stock).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Stock).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grdItems.Columns(ColumnasDelGridItems.Stock).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Minimo).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Maximo).Visible = False

            grdItems.Columns(ColumnasDelGridItems.IdMoneda).Visible = False

            'grdItems.Columns(ColumnasDelGridItems.CodMoneda).ReadOnly = True
            'grdItems.Columns(ColumnasDelGridItems.CodMoneda).Width = 45
            'grdItems.Columns(ColumnasDelGridItems.CodMoneda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grdItems.Columns(ColumnasDelGridItems.CodMoneda).Visible = False

            grdItems.Columns(ColumnasDelGridItems.ValorCambio).Visible = False

            grdItems.Columns(ColumnasDelGridItems.PrecioLista).Width = 55
            grdItems.Columns(ColumnasDelGridItems.PrecioLista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Iva).Width = 40
            grdItems.Columns(ColumnasDelGridItems.Iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.MontoIva).ReadOnly = True
            grdItems.Columns(ColumnasDelGridItems.MontoIva).Width = 55
            grdItems.Columns(ColumnasDelGridItems.MontoIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Ganancia).Width = 40
            grdItems.Columns(ColumnasDelGridItems.Ganancia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grdItems.Columns(ColumnasDelGridItems.Bonificacion1).Width = 40
            'grdItems.Columns(ColumnasDelGridItems.Bonificacion1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Bonificacion1).Visible = False
            grdItems.Columns(ColumnasDelGridItems.Bonificacion2).Visible = False

            grdItems.Columns(ColumnasDelGridItems.PrecioVta).ReadOnly = True 'precio unitario
            grdItems.Columns(ColumnasDelGridItems.PrecioVta).Width = 60
            grdItems.Columns(ColumnasDelGridItems.PrecioVta).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.PorcRecDesc).Width = 60
            grdItems.Columns(ColumnasDelGridItems.PorcRecDesc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.Qty).Width = 50
            grdItems.Columns(ColumnasDelGridItems.Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            grdItems.Columns(ColumnasDelGridItems.SubTotalProd).ReadOnly = True 'subtotal
            grdItems.Columns(ColumnasDelGridItems.SubTotalProd).Width = 70
            grdItems.Columns(ColumnasDelGridItems.SubTotalProd).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grdItems.Columns(ColumnasDelGridItems.dateupd).ReadOnly = True 'maximo'
            grdItems.Columns(ColumnasDelGridItems.dateupd).Width = 70
            grdItems.Columns(ColumnasDelGridItems.dateupd).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            grdItems.Columns(ColumnasDelGridItems.preciovtaorig).Visible = False
            grdItems.Columns(ColumnasDelGridItems.gananciaorig).Visible = False

            grdItems.Columns(ColumnasDelGridItems.IdProveedor).Visible = False

            grdItems.Columns(ColumnasDelGridItems.NombreProveedor).Width = 100

            grdItems.Columns(ColumnasDelGridItems.IdMarca).Visible = False

            grdItems.Columns(ColumnasDelGridItems.Marca).Width = 100
            grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True

            grdItems.Columns(ColumnasDelGridItems.PlazoEntrega).Width = 60

            grdItems.Columns(ColumnasDelGridItems.nota_det).Width = 150

            grdItems.Columns(ColumnasDelGridItems.orden).Visible = False
            grdItems.Columns(ColumnasDelGridItems.marcanueva).Visible = False

            grdItems.Columns(ColumnasDelGridItems.idmonedaorig).Visible = False

            Dim i As Integer

            For i = 0 To grdItems.Columns.Count - 1
                grdItems.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

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

            For i = 0 To grdItems.Rows.Count - 2
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Ubicacion).Value.ToString.Length > 6 Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Style.BackColor = Color.LightGreen
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material_Prov).Style.BackColor = Color.LightGreen
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
                End If
            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub HiloOcultarColumnasGritOT()
        Try


            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.IdPresupOT).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.IdPresupOT_Det).Visible = False

            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.AlcanceBreve).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Ajuste).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Ajuste_Texto).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Certificacion).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Certificacion_Texto).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Iva).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.MontoIva).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Orden).Visible = False

            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntrega).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntrega_Texto).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntregaProvision).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PlazoEntregaProvision_Texto).Visible = False

            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Subtotal).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Total).Visible = False
            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.IdDet).Visible = False

            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.Descripcion).Width = 600

            grdOfertaComercial.Columns(ColumnasDelGridOfertaComercial.PreciosinIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim i As Integer

            For i = 0 To grdOfertaComercial.Columns.Count - 1
                grdOfertaComercial.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            With grdOfertaComercial
                .VirtualMode = False
                .AllowUserToAddRows = True
                .AllowUserToDeleteRows = True
                .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
                .RowsDefaultCellStyle.BackColor = Color.White
                .AllowUserToOrderColumns = False
                .SelectionMode = DataGridViewSelectionMode.CellSelect
                .ForeColor = Color.Black
            End With
            With grdOfertaComercial.ColumnHeadersDefaultCellStyle
                .BackColor = Color.Black  'Color.BlueViolet
                .ForeColor = Color.White
                .Font = New Font("TAHOMA", 8, FontStyle.Bold)
            End With
            grdOfertaComercial.Font = New Font("TAHOMA", 8, FontStyle.Regular)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Agregar_Materiales_Tablero() 'ByVal codigoMaterial As String, ByVal qty As String, ByVal filas As Integer)

        Dim a As Integer

        a = grdItems.CurrentCell.RowIndex

        'txtNombre.Text = a.ToString

        For i As Integer = a To a + cantidadItemsTablero - 2

            If i = a Then
                grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value = lista(i - a, 0)
            Else

                For J As Integer = 0 To lista(i - a, 0).Length - 1

                    SendKeys.Send("{" & lista(i - a, 0).Chars(J) & "}")

                Next
            End If


            Try

                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")

                If lista(i - a, 1) > 9 Then
                    For G As Integer = 0 To lista(i - a, 1).Length - 1

                        SendKeys.Send("{" & lista(i - a, 1).Chars(G) & "}")

                    Next

                Else
                    SendKeys.Send("{" & lista(i - a, 1).ToString & "}")
                End If

                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")

            Catch ex As Exception
                MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
            End Try

        Next

        'txtNombre.Text = grdItems.Rows.Count.ToString

    End Sub

    Private Sub Presupuestos_Vencidos()
        Dim i As Integer = 0

        With grd
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine
            .RowsDefaultCellStyle.BackColor = Color.White
        End With

        grd.Refresh()

        For i = 0 To grd.Rows.Count
            Try
                If grd.Rows(i).Cells(ColumnasDelGrid.Vencido).Value = True And grd.Rows(i).Cells(ColumnasDelGrid.Estado_p).Value = "Pendiente" Then
                    grd.Rows(i).DefaultCellStyle.BackColor = Color.Red
                    grd.Rows(i).DefaultCellStyle.ForeColor = Color.White
                    'Else
                    '    If grd.Rows(i).Cells(ColumnasDelGrid.Estado_p).Value = "Cumplido" Then

                    '    End If
                End If

            Catch ex As Exception

            End Try

        Next

    End Sub

    Private Sub validar_NumerosRealesTrafos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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
    End Sub

    'Private Sub BuscarMoneda(ByVal Moneda As String)
    '    'Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim ds As Data.DataSet

    '    'Try
    '    '    connection = SqlHelper.GetConnection(ConnStringSEI)
    '    'Catch ex As Exception
    '    '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    Exit Sub
    '    'End Try

    '    Try

    '        ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select id, codigo from Monedas where nombre = '" & Moneda & "'")

    '        ds.Dispose()

    '        txtIdMonedaPres.Text = ds.Tables(0).Rows(0).Item(0)
    '        txtCodMonedaPres.Text = ds.Tables(0).Rows(0).Item(1)
    '        'txtValorCambioPres.Text = CDbl(ds.Tables(0).Rows(0).Item(2))

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
    '        'If Not connection Is Nothing Then
    '        '    CType(connection, IDisposable).Dispose()
    '        'End If
    '    End Try

    'End Sub

    Private Sub MontoFinalPresupuesto()
        If txtTotalOferta.Text = "" Then txtTotalOferta.Text = "0"
        If txtSubtotalOferta.Text = "" Then txtSubtotalOferta.Text = "0"

        'If txtIvaOferta.Text = "" Then
        '    txtIva10Pre.Text = txtIva10.Text
        '    txtIva21Pre.Text = txtIva21.Text
        'Else
        '    If CDbl(txtIvaOferta.Text) > 20 Then
        txtIva21Pre.Text = CDbl(txtIva21.Text) + CDbl(txtMontoIvaOferta.Text)
        '    Else
        'If CDbl(txtIvaOferta.Text) > 10 Then
        '    txtIva10Pre.Text = CDbl(txtIva10.Text) + CDbl(txtMontoIvaOferta.Text)
        'End If
        '    End If
        'End If

        Try
            txtSubtotalPre.Text = CDbl(txtSubtotal.Text) + CDbl(txtSubtotalOferta.Text)
            txtTotalPre.Text = CDbl(txtTotal.Text) + CDbl(txtTotalOferta.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CaclularMontosFinalesGrillaItems()
        Dim i As Integer

        Dim preciolista As Double
        Dim preciovta As Double
        Dim ganancia As Double

        txtSubtotal.Text = "0"
        txtTotal.Text = "0"
        txtIva21.Text = "0"
        txtIva10.Text = "0"
        txtSubtotalOferta.Text = "0"
        txtMontoIvaOferta.Text = "0"
        txtTotalOferta.Text = "0"

        txtIva21Pre.Text = "0"
        txtSubtotalPre.Text = "0"
        txtTotalPre.Text = "0"

        For i = 0 To grdItems.RowCount - 2

            If Not grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value Then

                preciolista = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value

                ganancia = 1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value / 100)

                grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(ganancia * preciolista, "####0.00")

                preciovta = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value

                If Band_RecDesc = True Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value = txtporcrecargo.Text
                End If

                If grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value > 0 Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), "####0.00")
                Else
                    If grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value < 0 Then
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * (1 - ((grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value * -1) / 100)), "####0.00")
                    End If
                End If

                If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
                    grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, "####0.00")
                End If

                grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value * (grdItems.Rows(i).Cells(ColumnasDelGridItems.Iva).Value / 100), "####0.00")

                txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value))
                If grdItems.Rows(i).Cells(ColumnasDelGridItems.Iva).Value = 21 Then
                    txtIva21.Text = CDbl(txtIva21.Text) + CDbl(IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value Is DBNull.Value, 0, Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value * IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value), "####0.00")))
                Else
                    txtIva10.Text = CDbl(txtIva10.Text) + CDbl(IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value * IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value)))
                End If

                txtTotal.Text = Math.Round(CDbl(txtSubtotal.Text) + CDbl(txtIva10.Text) + CDbl(txtIva21.Text), 2)
            End If

        Next

        If chkAgregarOfertaComercial.Checked = True Then

            Dim j As Integer = 0
            txtSubtotalOferta.Text = "0"

            For j = 0 To grdOfertaComercial.Rows.Count - 2
                'txtSubtotalOferta.Text = CDbl(txtSubtotalOferta.Text) + IIf(grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value Is DBNull.Value, 0, grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value)
                If (Not (grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value Is DBNull.Value) And
                       grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value.ToString <> "") And
                       (Not (grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value Is DBNull.Value) And
                        grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value.ToString <> "") Then

                    txtSubtotalOferta.Text = CDbl(txtSubtotalOferta.Text) + grdOfertaComercial.Rows(j).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value

                End If
            Next

            txtIvaOferta.Text = IIf(txtIvaOferta.Text = "", 21, txtIvaOferta.Text)

            txtMontoIvaOferta.Text = FormatNumber((txtSubtotalOferta.Text) * CDbl(txtIvaOferta.Text) / 100, 2)

            txtTotalOferta.Text = FormatNumber(CDbl(txtSubtotalOferta.Text) + CDbl(txtMontoIvaOferta.Text), 2)

        End If

        txtIva21Pre.Text = CDbl(txtIva21.Text) + CDbl(txtMontoIvaOferta.Text)

        txtSubtotalPre.Text = CDbl(txtSubtotal.Text) + CDbl(txtSubtotalOferta.Text)
        txtTotalPre.Text = CDbl(txtTotal.Text) + CDbl(txtTotalOferta.Text)

    End Sub

    Private Sub TerminarGuardar()
        Guardando = False
        If Not conn_del_form Is Nothing Then
            CType(conn_del_form, IDisposable).Dispose()
        End If
        btnBand_Copiar = True
    End Sub

    Private Sub TerminarGuardar_2()
        If Not conn_del_form Is Nothing Then
            CType(conn_del_form, IDisposable).Dispose()
        End If
        Guardando = False
        chkAmpliarGrillaInferior.Enabled = True
        chkAnulados.Enabled = True
        chkPresupuestosCumplidos.Enabled = True
        btnBand_Copiar = True
    End Sub

    Private Sub Actualizar_PrecioSegunMoneda()
        Dim i As Integer

        If grdItems.RowCount > 1 Then
            For i = 0 To grdItems.RowCount - 2
                If txtCodMonedaPres.Text.ToUpper = "PE" And CodMonedaOrig.ToUpper = "DO" Then ' grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMoneda).Value.ToString.ToUpper = "DO" Then
                    Try

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value * ValorcambioDolar, "####0.00") 'grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value * ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value * ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value * ValorcambioDolar, "####0.00")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value * ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value * ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Else
                    If txtCodMonedaPres.Text.ToUpper = "DO" And CodMonedaOrig.ToUpper = "PE" Then 'grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMoneda).Value.ToString.ToUpper = "PE" Then

                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value / ValorcambioDolar, "####0.00") 'grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value / ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value / ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value / ValorcambioDolar, "####0.00")
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.MontoIva).Value / ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value / ValorcambioDolar, "####0.00") '  grdItems.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)

                    End If

                End If

                grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value = txtIdMonedaPres.Text 'Idmoneda
                grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMoneda).Value = txtCodMonedaPres.Text 'Idmoneda

            Next

        End If

        If chkAgregarOfertaComercial.Checked = True Then
            i = 0

            For i = 0 To grdOfertaComercial.RowCount - 2
                If txtCodMonedaPres.Text.ToUpper = "PE" And CodMonedaOrig.ToUpper = "DO" Then 'grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMoneda).Value.ToString.ToUpper = "DO" Then
                    Try

                        grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value = Format(grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value * ValorcambioDolar, "####0.00")

                    Catch ex As Exception
                        ' MsgBox(ex.Message)
                    End Try
                Else
                    If txtCodMonedaPres.Text.ToUpper = "DO" And CodMonedaOrig.ToUpper = "PE" Then ' grdItems.Rows(i).Cells(ColumnasDelGridItems.CodMoneda).Value.ToString.ToUpper = "PE" Then

                        grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value = Format(grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value / ValorcambioDolar, "####0.00")

                    End If

                End If
            Next

        End If

        Try
            CaclularMontosFinalesGrillaItems() ' (grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material))
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Funciones"

    Private Function BuscarValor_CambioORIG(ByVal IdMoneda As Long) As Decimal
        Dim ds_Clientes As Data.DataSet

        Try
            ds_Clientes = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT valorcambio FROM Monedas WHERE id = " & IdMoneda)
            ds_Clientes.Dispose()

            BuscarValor_CambioORIG = ds_Clientes.Tables(0).Rows(0).Item(0).ToString

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

    Private Function ControlarVersion() As Integer
        Dim ds As Data.DataSet

        If txtValidez.Text <> grd.CurrentRow.Cells(ColumnasDelGrid.validez_p).Value Then
            ControlarVersion = 1
            Exit Function
        End If

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

            Try
                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_ControlarFilas", _
                                          param_id, param_cant)

                If param_cant.Value <> grdItems.RowCount - 1 Then
                    ControlarVersion = 1
                    Exit Function
                End If

            Catch ex As Exception
                Throw ex
            End Try

            If chkManoObra.Checked = False Then

                ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT idmaterial, qty, precioVTA FROM Presupuestos_det WHERE idpresupuesto = " & txtID.Text & "ORDER BY idmaterial ")
                ds.Dispose()

                Dim i As Integer, j As Integer
                Dim iguales As Boolean

                For i = 0 To grdItems.RowCount - 2
                    For j = 0 To ds.Tables(0).Rows.Count - 1
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is DBNull.Value Then
                            iguales = False
                            Exit For
                        End If
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

            End If

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
        End Try

    End Function

    Private Function BuscarRevision() As Integer
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
                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_BuscarRevision", _
                                          param_id, param_rev)

                BuscarRevision = param_rev.Value

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

    Private Function AgregarActualizar_Registro() As Integer

        Try
            conn_del_form = SqlHelper.GetConnection(ConnStringSEI)

            Dim Compradores As Integer, Usuarios As Integer

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(conn_del_form) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            Try
                Dim nuevarevision As Integer
                Revision = 0

                If bolModo = False And GuardarDesdeTimer = False And chkPresupuestoConWord.Checked = False Then
                    Dim resultadorevision As Integer
                    resultadorevision = ControlarVersion()
                    If resultadorevision = 1 Then 'Or resultadorevision = 3 Then
                        If MessageBox.Show("Se han producido cambios en el presupuesto." + vbCrLf + "¿Desea generar una nueva revisión?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            nuevarevision = BuscarRevision() + 1
                            txtRevision.Text = nuevarevision
                            Revision = 1
                        End If
                    End If

                    If resultadorevision = -1 Then
                        MessageBox.Show("Se produjo un error al intentar controlar la revisión. Se continua sin esa información-", "Control de Revisión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        'AgregarActualizar_Registro = 20
                        'Exit Function
                    End If
                End If

                BuscarContacto(Compradores, Usuarios)

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                If bolModo = True Then
                    param_id.Value = DBNull.Value
                Else
                    param_id.Value = txtID.Text
                End If
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fechapresupuesto"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = dtpFECHA.Value
                param_fecha.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                If bolModo = True Then
                    param_codigo.Direction = ParameterDirection.InputOutput
                Else
                    param_codigo.Direction = ParameterDirection.Input
                End If

                Dim param_Nombre As New SqlClient.SqlParameter
                param_Nombre.ParameterName = "@NombreReferencia"
                param_Nombre.SqlDbType = SqlDbType.VarChar
                param_Nombre.Size = 400
                param_Nombre.Value = txtNombre.Text
                param_Nombre.Direction = ParameterDirection.Input

                Dim param_idCliente As New SqlClient.SqlParameter
                param_idCliente.ParameterName = "@idCliente"
                param_idCliente.SqlDbType = SqlDbType.BigInt
                param_idCliente.Value = txtIdCliente.Text  'cmbCliente.SelectedValue
                param_idCliente.Direction = ParameterDirection.Input

                Dim param_Usuario As New SqlClient.SqlParameter
                param_Usuario.ParameterName = "@Usuario"
                param_Usuario.SqlDbType = SqlDbType.Bit
                param_Usuario.Value = chkUsuario.Checked
                param_Usuario.Direction = ParameterDirection.Input

                Dim param_idUsuario As New SqlClient.SqlParameter
                param_idUsuario.ParameterName = "@idContacto_Usuario"
                param_idUsuario.SqlDbType = SqlDbType.BigInt
                If chkUsuario.Checked = True Then
                    If Usuarios > 0 Then
                        param_idUsuario.Value = IIf(txtIdUsuario.Text = "", 0, txtIdUsuario.Text)
                    Else
                        If AgregarRegistro_Contactos(param_idUsuario.Value, cmbUsuarios.Text.ToString) < 0 Then
                            AgregarActualizar_Registro = -1
                            Exit Function
                        End If
                    End If
                Else
                    param_idUsuario.Value = 0
                End If
                param_idUsuario.Direction = ParameterDirection.Input

                Dim param_Comprador As New SqlClient.SqlParameter
                param_Comprador.ParameterName = "@Comprador"
                param_Comprador.SqlDbType = SqlDbType.Bit
                param_Comprador.Value = chkComprador.Checked
                param_Comprador.Direction = ParameterDirection.Input

                Dim param_idComprador As New SqlClient.SqlParameter
                param_idComprador.ParameterName = "@idContacto_Comprador"
                param_idComprador.SqlDbType = SqlDbType.BigInt
                If chkComprador.Checked = True Then
                    If Compradores > 0 Then
                        param_idComprador.Value = IIf(txtIdComprador.Text = "", 0, txtIdComprador.Text)
                    Else
                        If AgregarRegistro_Contactos(param_idComprador.Value, cmbCompradores.Text.ToString) < 0 Then
                            AgregarActualizar_Registro = -1
                            Exit Function
                        End If
                    End If
                Else
                    param_idComprador.Value = 0
                End If
                param_idComprador.Direction = ParameterDirection.Input

                Dim param_Entregaren As New SqlClient.SqlParameter
                param_Entregaren.ParameterName = "@Entregaren"
                param_Entregaren.SqlDbType = SqlDbType.Bit
                param_Entregaren.Value = chkEntrega.Checked
                param_Entregaren.Direction = ParameterDirection.Input

                Dim param_sitioentrega As New SqlClient.SqlParameter
                param_sitioentrega.ParameterName = "@sitioentrega"
                param_sitioentrega.SqlDbType = SqlDbType.VarChar
                param_sitioentrega.Size = 500
                param_sitioentrega.Value = cmbEntregaren.Text
                param_sitioentrega.Direction = ParameterDirection.Input

                Dim param_idMonedaPres As New SqlClient.SqlParameter
                param_idMonedaPres.ParameterName = "@idMonedaPres"
                param_idMonedaPres.SqlDbType = SqlDbType.BigInt
                param_idMonedaPres.Value = txtIdMonedaPres.Text  'cmbCliente.SelectedValue
                param_idMonedaPres.Direction = ParameterDirection.Input

                Dim param_subtotalDO As New SqlClient.SqlParameter
                param_subtotalDO.ParameterName = "@subtotalDO"
                param_subtotalDO.SqlDbType = SqlDbType.Decimal
                param_subtotalDO.Precision = 18
                param_subtotalDO.Scale = 2
                param_subtotalDO.Value = CType(txtSubtotal.Text, Double)
                param_subtotalDO.Direction = ParameterDirection.Input

                'Dim param_subtotalPE As New SqlClient.SqlParameter
                'param_subtotalPE.ParameterName = "@subtotalPE"
                'param_subtotalPE.SqlDbType = SqlDbType.Decimal
                'param_subtotalPE.Precision = 18
                'param_subtotalPE.Scale = 2
                'param_subtotalPE.Value = 0 'CType(txtSubtotal.Text, Double)
                'param_subtotalPE.Direction = ParameterDirection.Input

                Dim param_totalDO As New SqlClient.SqlParameter
                param_totalDO.ParameterName = "@totalDO"
                param_totalDO.SqlDbType = SqlDbType.Decimal
                param_totalDO.Precision = 18
                param_totalDO.Scale = 2
                param_totalDO.Value = CType(txtTotal.Text, Double)
                param_totalDO.Direction = ParameterDirection.Input

                Dim param_iva21DO As New SqlClient.SqlParameter
                param_iva21DO.ParameterName = "@iva21DO"
                param_iva21DO.SqlDbType = SqlDbType.Decimal
                param_iva21DO.Precision = 18
                param_iva21DO.Scale = 2
                param_iva21DO.Value = txtIva21.Text
                param_iva21DO.Direction = ParameterDirection.Input

                Dim param_iva10DO As New SqlClient.SqlParameter
                param_iva10DO.ParameterName = "@iva10DO"
                param_iva10DO.SqlDbType = SqlDbType.Decimal
                param_iva10DO.Precision = 18
                param_iva10DO.Scale = 2
                param_iva10DO.Value = txtIva10.Text
                param_iva10DO.Direction = ParameterDirection.Input

                Dim param_incluyenotas As New SqlClient.SqlParameter
                param_incluyenotas.ParameterName = "@incluyenotas"
                param_incluyenotas.SqlDbType = SqlDbType.Bit
                param_incluyenotas.Value = chkNotas.Checked
                param_incluyenotas.Direction = ParameterDirection.Input

                Dim param_validez As New SqlClient.SqlParameter
                param_validez.ParameterName = "@validez"
                param_validez.SqlDbType = SqlDbType.Int
                param_validez.Value = txtValidez.Text
                param_validez.Direction = ParameterDirection.Input

                Dim param_formapago As New SqlClient.SqlParameter
                param_formapago.ParameterName = "@idformapago"
                param_formapago.SqlDbType = SqlDbType.BigInt
                param_formapago.Value = txtIdFormaPago.Text  'cmbFormaPago.SelectedValue
                param_formapago.Direction = ParameterDirection.Input

                Dim param_NroOC As New SqlClient.SqlParameter
                param_NroOC.ParameterName = "@NroOC"
                param_NroOC.SqlDbType = SqlDbType.VarChar
                param_NroOC.Size = 50
                param_NroOC.Value = LTrim(RTrim(txtNroOC.Text))
                param_NroOC.Direction = ParameterDirection.Input

                Dim param_nroreq As New SqlClient.SqlParameter
                param_nroreq.ParameterName = "@NroReq"
                param_nroreq.SqlDbType = SqlDbType.VarChar
                param_nroreq.Size = 25
                param_nroreq.Value = txtReq.Text
                param_nroreq.Direction = ParameterDirection.Input

                Dim param_nrorev As New SqlClient.SqlParameter
                param_nrorev.ParameterName = "@NroRev"
                param_nrorev.SqlDbType = SqlDbType.SmallInt
                If bolModo = True Then
                    param_nrorev.Value = DBNull.Value
                Else
                    param_nrorev.Value = txtRevision.Text
                End If
                param_nrorev.Direction = ParameterDirection.Input

                Dim param_RecDescGlobal As New SqlClient.SqlParameter
                param_RecDescGlobal.ParameterName = "@RecDescGobal"
                param_RecDescGlobal.SqlDbType = SqlDbType.Bit
                param_RecDescGlobal.Value = chkRecDescGlobal.Checked
                param_RecDescGlobal.Direction = ParameterDirection.Input

                Dim param_porcrecargo As New SqlClient.SqlParameter
                param_porcrecargo.ParameterName = "@porcrecargo"
                param_porcrecargo.SqlDbType = SqlDbType.Decimal
                param_porcrecargo.Precision = 18
                param_porcrecargo.Scale = 2
                param_porcrecargo.Value = IIf(txtporcrecargo.Text = "", 0, txtporcrecargo.Text)
                param_porcrecargo.Direction = ParameterDirection.Input

                Dim param_idautoriza As New SqlClient.SqlParameter
                param_idautoriza.ParameterName = "@userautoriza"
                param_idautoriza.SqlDbType = SqlDbType.BigInt
                param_idautoriza.Value = cmbAutoriza.SelectedValue
                param_idautoriza.Direction = ParameterDirection.Input

                Dim param_idvendedor As New SqlClient.SqlParameter
                param_idvendedor.ParameterName = "@uservendedor"
                param_idvendedor.SqlDbType = SqlDbType.BigInt
                param_idvendedor.Value = UserID 'cmbVendedores.SelectedValue
                param_idvendedor.Direction = ParameterDirection.Input

                Dim param_nota As New SqlClient.SqlParameter
                param_nota.ParameterName = "@notapresupuesto"
                param_nota.SqlDbType = SqlDbType.VarChar
                param_nota.Size = 100
                param_nota.Value = txtNotaGestion.Text
                param_nota.Direction = ParameterDirection.Input

                Dim param_Anticipo As New SqlClient.SqlParameter
                param_Anticipo.ParameterName = "@Anticipo"
                param_Anticipo.SqlDbType = SqlDbType.Bit
                param_Anticipo.Value = chkAnticipo.Checked
                param_Anticipo.Direction = ParameterDirection.Input

                Dim param_PorcAnticipo As New SqlClient.SqlParameter
                param_PorcAnticipo.ParameterName = "@PorcAnticipo"
                param_PorcAnticipo.SqlDbType = SqlDbType.VarChar
                param_PorcAnticipo.Size = 25
                param_PorcAnticipo.Value = txtAnticipo.Text
                param_PorcAnticipo.Direction = ParameterDirection.Input

                Dim param_OCA As New SqlClient.SqlParameter
                param_OCA.ParameterName = "@Oca"
                param_OCA.SqlDbType = SqlDbType.Bit
                param_OCA.Value = chkOCA.Checked
                param_OCA.Direction = ParameterDirection.Input

                Dim param_IdConsumo As New SqlClient.SqlParameter
                param_IdConsumo.ParameterName = "@idConsumo"
                param_IdConsumo.SqlDbType = SqlDbType.BigInt
                If chkOCA.Checked = True Then
                    param_IdConsumo.Value = Origen_Id
                Else
                    param_IdConsumo.Value = 0
                End If
                param_IdConsumo.Direction = ParameterDirection.Input

                Dim param_OC As New SqlClient.SqlParameter
                param_OC.ParameterName = "@Oc"
                param_OC.SqlDbType = SqlDbType.Bit
                param_OC.Value = chkOC.Checked
                param_OC.Direction = ParameterDirection.Input

                Dim param_IdOC As New SqlClient.SqlParameter
                param_IdOC.ParameterName = "@idOC"
                param_IdOC.SqlDbType = SqlDbType.BigInt
                If chkOC.Checked = True Then
                    param_IdOC.Value = Origen_Id
                Else
                    param_IdOC.Value = 0
                End If
                param_IdOC.Direction = ParameterDirection.Input

                Dim param_OfertaComercial As New SqlClient.SqlParameter
                param_OfertaComercial.ParameterName = "@OfertaComercial"
                param_OfertaComercial.SqlDbType = SqlDbType.Bit
                param_OfertaComercial.Value = chkAgregarOfertaComercial.Checked
                param_OfertaComercial.Direction = ParameterDirection.Input

                Dim param_useradd As New SqlClient.SqlParameter
                If bolModo = True Then
                    param_useradd.ParameterName = "@useradd"
                Else
                    param_useradd.ParameterName = "@userupd"
                End If
                param_useradd.SqlDbType = SqlDbType.Int
                param_useradd.Value = UserID
                param_useradd.Direction = ParameterDirection.Input

                Dim param_MostrarCodigo As New SqlClient.SqlParameter
                param_MostrarCodigo.ParameterName = "@MostrarCodigo"
                param_MostrarCodigo.SqlDbType = SqlDbType.Bit
                param_MostrarCodigo.Value = chkMostrarCodigoMaterial.Checked
                param_MostrarCodigo.Direction = ParameterDirection.Input

                Dim param_MostrarPrecioMat As New SqlClient.SqlParameter
                param_MostrarPrecioMat.ParameterName = "@MostrarPrecioMat"
                param_MostrarPrecioMat.SqlDbType = SqlDbType.Bit
                param_MostrarPrecioMat.Value = chkMostrarPrecioMaterial.Checked
                param_MostrarPrecioMat.Direction = ParameterDirection.Input

                Dim param_MostrarPrecioManoObra As New SqlClient.SqlParameter
                param_MostrarPrecioManoObra.ParameterName = "@MostrarPrecioManoObra"
                param_MostrarPrecioManoObra.SqlDbType = SqlDbType.Bit
                param_MostrarPrecioManoObra.Value = chkMostrarPrecioManoObra.Checked
                param_MostrarPrecioManoObra.Direction = ParameterDirection.Input

                Dim param_MostrarSubtotalMatOC As New SqlClient.SqlParameter
                param_MostrarSubtotalMatOC.ParameterName = "@MostrarSubtotalMatOC"
                param_MostrarSubtotalMatOC.SqlDbType = SqlDbType.Bit
                param_MostrarSubtotalMatOC.Value = chkMostrarSubtotalMaterialOC.Checked
                param_MostrarSubtotalMatOC.Direction = ParameterDirection.Input

                Dim param_MostrarSubtotalMOOC As New SqlClient.SqlParameter
                param_MostrarSubtotalMOOC.ParameterName = "@MostrarSubtotalMOOC"
                param_MostrarSubtotalMOOC.SqlDbType = SqlDbType.Bit
                param_MostrarSubtotalMOOC.Value = chkMostrarSubtotalMOOC.Checked
                param_MostrarSubtotalMOOC.Direction = ParameterDirection.Input

                Dim param_MostrarPlazoEntrega As New SqlClient.SqlParameter
                param_MostrarPlazoEntrega.ParameterName = "@Mostrarplazoentrega"
                param_MostrarPlazoEntrega.SqlDbType = SqlDbType.Bit
                param_MostrarPlazoEntrega.Value = chkMostrarPlazoEntrega.Checked
                param_MostrarPlazoEntrega.Direction = ParameterDirection.Input

                Dim param_SubtotalOfertaComercial As New SqlClient.SqlParameter
                param_SubtotalOfertaComercial.ParameterName = "@SubtotalOfertaComercial"
                param_SubtotalOfertaComercial.SqlDbType = SqlDbType.Bit
                param_SubtotalOfertaComercial.Value = False 'chkSubtotalOfertaComercial.Checked
                param_SubtotalOfertaComercial.Direction = ParameterDirection.Input

                Dim param_MostrarTotal As New SqlClient.SqlParameter
                param_MostrarTotal.ParameterName = "@MostrarTotal"
                param_MostrarTotal.SqlDbType = SqlDbType.Bit
                param_MostrarTotal.Value = ChkMostrarTotal.Checked
                param_MostrarTotal.Direction = ParameterDirection.Input

                Dim param_ManoObra As New SqlClient.SqlParameter
                param_ManoObra.ParameterName = "@ManoObra"
                param_ManoObra.SqlDbType = SqlDbType.Bit
                param_ManoObra.Value = chkManoObra.Checked
                param_ManoObra.Direction = ParameterDirection.Input

                Dim param_NombreEstado As New SqlClient.SqlParameter
                param_NombreEstado.ParameterName = "@NombreEstado"
                param_NombreEstado.SqlDbType = SqlDbType.VarChar
                param_NombreEstado.Size = 50
                param_NombreEstado.Value = cmbEstado.Text.ToString
                param_NombreEstado.Direction = ParameterDirection.Input

                Dim param_trafo As New SqlClient.SqlParameter
                param_trafo.ParameterName = "@Trafo"
                param_trafo.SqlDbType = SqlDbType.Bit
                param_trafo.Value = rdTrafo.Checked
                param_trafo.Direction = ParameterDirection.Input

                Dim param_Trafo_Cabecera As New SqlClient.SqlParameter
                param_Trafo_Cabecera.ParameterName = "@CabeceraTrafo"
                param_Trafo_Cabecera.SqlDbType = SqlDbType.VarChar
                param_Trafo_Cabecera.Size = 500
                param_Trafo_Cabecera.Value = txtTrafo_Cabecera.Text
                param_Trafo_Cabecera.Direction = ParameterDirection.Input

                Dim param_Trafo_Observaciones As New SqlClient.SqlParameter
                param_Trafo_Observaciones.ParameterName = "@Observaciones"
                param_Trafo_Observaciones.SqlDbType = SqlDbType.VarChar
                param_Trafo_Observaciones.Size = 1000
                param_Trafo_Observaciones.Value = txtTrafo_Observaciones.Text
                param_Trafo_Observaciones.Direction = ParameterDirection.Input

                Dim param_Trafo_HorasTrabajo As New SqlClient.SqlParameter
                param_Trafo_HorasTrabajo.ParameterName = "@HorasTrabajo"
                param_Trafo_HorasTrabajo.SqlDbType = SqlDbType.Int
                param_Trafo_HorasTrabajo.Value = IIf(txtTrafo_CantHoras.Text = "", 0, txtTrafo_CantHoras.Text)
                param_Trafo_HorasTrabajo.Direction = ParameterDirection.Input

                Dim param_Trafo_SubtotalEnsayos As New SqlClient.SqlParameter
                param_Trafo_SubtotalEnsayos.ParameterName = "@subtotalensayos"
                param_Trafo_SubtotalEnsayos.SqlDbType = SqlDbType.Decimal
                param_Trafo_SubtotalEnsayos.Precision = 18
                param_Trafo_SubtotalEnsayos.Scale = 2
                param_Trafo_SubtotalEnsayos.Value = IIf(txtTrafo_SubtotalEnsayos.Text = "", 0, txtTrafo_SubtotalEnsayos.Text)
                param_Trafo_SubtotalEnsayos.Direction = ParameterDirection.Input

                Dim param_PrecioDistribuidor As New SqlClient.SqlParameter
                param_PrecioDistribuidor.ParameterName = "@PrecioDistribuidor"
                param_PrecioDistribuidor.SqlDbType = SqlDbType.Bit
                param_PrecioDistribuidor.Value = chkPrecioDistribuidor.Checked
                param_PrecioDistribuidor.Direction = ParameterDirection.Input

                Dim param_PresupuestoconWord As New SqlClient.SqlParameter
                param_PresupuestoconWord.ParameterName = "@PresupuestoconWord"
                param_PresupuestoconWord.SqlDbType = SqlDbType.Bit
                param_PresupuestoconWord.Value = chkPresupuestoConWord.Checked
                param_PresupuestoconWord.Direction = ParameterDirection.Input

                Dim param_Garantia As New SqlClient.SqlParameter
                param_Garantia.ParameterName = "@garantia"
                param_Garantia.SqlDbType = SqlDbType.Int
                param_Garantia.Value = txtGarantia.Text
                param_Garantia.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Insert", _
                                            param_id, param_codigo, param_fecha, param_idCliente, param_Comprador, param_idComprador, _
                                            param_Usuario, param_idUsuario, param_Entregaren, param_sitioentrega, param_idMonedaPres, _
                                            param_iva21DO, param_iva10DO, _
                                            param_subtotalDO, param_totalDO, _
                                            param_incluyenotas, param_validez, _
                                            param_formapago, param_NroOC, param_nroreq, param_nrorev, _
                                            param_RecDescGlobal, param_porcrecargo, param_idvendedor, _
                                            param_idautoriza, param_Anticipo, param_PorcAnticipo, param_useradd, _
                                            param_Nombre, param_nota, param_OCA, param_IdConsumo, _
                                            param_OC, param_IdOC, param_OfertaComercial, param_MostrarCodigo, _
                                            param_MostrarPrecioMat, param_MostrarPrecioManoObra, param_MostrarSubtotalMatOC, _
                                            param_MostrarSubtotalMOOC, param_SubtotalOfertaComercial, param_MostrarTotal, param_MostrarPlazoEntrega, _
                                            param_ManoObra, param_NombreEstado, _
                                            param_trafo, param_Trafo_Cabecera, _
                                            param_Trafo_Observaciones, param_Trafo_HorasTrabajo, param_Trafo_SubtotalEnsayos, _
                                            param_PrecioDistribuidor, param_PresupuestoconWord, param_Garantia, param_res)

                        txtID.Text = param_id.Value
                        txtCODIGO.Text = param_codigo.Value.ToString

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Update", _
                                            param_id, param_codigo, param_fecha, param_idCliente, param_Comprador, param_idComprador, _
                                            param_Usuario, param_idUsuario, param_Entregaren, param_sitioentrega, param_idMonedaPres, _
                                            param_iva21DO, param_iva10DO, _
                                            param_subtotalDO, param_totalDO, _
                                            param_incluyenotas, param_validez, _
                                            param_formapago, param_NroOC, param_nroreq, param_nrorev, _
                                            param_RecDescGlobal, param_porcrecargo, param_idvendedor, _
                                            param_idautoriza, param_Anticipo, param_PorcAnticipo, param_useradd, _
                                            param_Nombre, param_nota, param_OfertaComercial, param_MostrarCodigo, _
                                            param_MostrarPrecioMat, param_MostrarPrecioManoObra, param_MostrarSubtotalMatOC, _
                                            param_MostrarSubtotalMOOC, param_SubtotalOfertaComercial, param_MostrarTotal, param_MostrarPlazoEntrega, _
                                            param_ManoObra, param_NombreEstado, _
                                            param_trafo, param_Trafo_Cabecera, param_Trafo_Observaciones, param_Trafo_HorasTrabajo, param_Trafo_SubtotalEnsayos, _
                                            param_PresupuestoconWord, param_Garantia, param_res)

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

    Private Function AgregarRegistro_OfertaComercial() As Integer
        Dim res As Integer = 0

        Try

            Try

                Dim param_Id_OfertaComercial As New SqlClient.SqlParameter
                param_Id_OfertaComercial.ParameterName = "@ID"
                param_Id_OfertaComercial.SqlDbType = SqlDbType.BigInt
                If bolModo = True Or txtID_OfertaComercial.Text = "0" Then
                    param_Id_OfertaComercial.Value = DBNull.Value
                    param_Id_OfertaComercial.Direction = ParameterDirection.InputOutput
                Else
                    param_Id_OfertaComercial.Value = txtID_OfertaComercial.Text
                    param_Id_OfertaComercial.Direction = ParameterDirection.Input
                End If

                Dim param_IdPresupuesto As New SqlClient.SqlParameter
                param_IdPresupuesto.ParameterName = "@idpresupuesto"
                param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                param_IdPresupuesto.Value = txtID.Text
                param_IdPresupuesto.Direction = ParameterDirection.Input

                Dim param_idMonedaPres As New SqlClient.SqlParameter
                param_idMonedaPres.ParameterName = "@idMonedaOferta"
                param_idMonedaPres.SqlDbType = SqlDbType.BigInt
                param_idMonedaPres.Value = txtIdMonedaPres.Text  'cmbCliente.SelectedValue
                param_idMonedaPres.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = txtSubtotalOferta.Text
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@IVA"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = txtIvaOferta.Text
                param_iva.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = txtTotalOferta.Text
                param_total.Direction = ParameterDirection.Input

                Dim param_Alcance As New SqlClient.SqlParameter
                param_Alcance.ParameterName = "@alcance"
                param_Alcance.SqlDbType = SqlDbType.NVarChar
                param_Alcance.Size = 4000
                param_Alcance.Value = txtAlcanceBreve.Text
                param_Alcance.Direction = ParameterDirection.Input

                Dim param_certificacion As New SqlClient.SqlParameter
                param_certificacion.ParameterName = "@certificacion"
                param_certificacion.SqlDbType = SqlDbType.Bit
                param_certificacion.Value = chkCertificaciones.Checked
                param_certificacion.Direction = ParameterDirection.Input

                Dim param_certificacion_texto As New SqlClient.SqlParameter
                param_certificacion_texto.ParameterName = "@certificacion_texto"
                param_certificacion_texto.SqlDbType = SqlDbType.NVarChar
                param_certificacion_texto.Size = 4000
                param_certificacion_texto.Value = cmbCertificaciones.Text.ToString
                param_certificacion_texto.Direction = ParameterDirection.Input

                Dim param_ajuste As New SqlClient.SqlParameter
                param_ajuste.ParameterName = "@ajuste"
                param_ajuste.SqlDbType = SqlDbType.Bit
                param_ajuste.Value = chkAjustes.Checked
                param_ajuste.Direction = ParameterDirection.Input

                Dim param_ajuste_texto As New SqlClient.SqlParameter
                param_ajuste_texto.ParameterName = "@ajuste_texto"
                param_ajuste_texto.SqlDbType = SqlDbType.NVarChar
                param_ajuste_texto.Size = 4000
                param_ajuste_texto.Value = cmbAjustes.Text.ToString
                param_ajuste_texto.Direction = ParameterDirection.Input

                Dim param_plazoentrega As New SqlClient.SqlParameter
                param_plazoentrega.ParameterName = "@plazoentrega"
                param_plazoentrega.SqlDbType = SqlDbType.Bit
                param_plazoentrega.Value = chkPlazoEntrega.Checked
                param_plazoentrega.Direction = ParameterDirection.Input

                Dim param_plazoentrega_texto As New SqlClient.SqlParameter
                param_plazoentrega_texto.ParameterName = "@plazoentrega_texto"
                param_plazoentrega_texto.SqlDbType = SqlDbType.NVarChar
                param_plazoentrega_texto.Size = 4000
                param_plazoentrega_texto.Value = cmbPlazoEntrega.Text.ToString
                param_plazoentrega_texto.Direction = ParameterDirection.Input

                Dim param_plazoentrega_provision As New SqlClient.SqlParameter
                param_plazoentrega_provision.ParameterName = "@plazoentregaprovision"
                param_plazoentrega_provision.SqlDbType = SqlDbType.Bit
                param_plazoentrega_provision.Value = chkPlazoEntregaProvision.Checked
                param_plazoentrega_provision.Direction = ParameterDirection.Input

                Dim param_plazoentregaprovision_texto As New SqlClient.SqlParameter
                param_plazoentregaprovision_texto.ParameterName = "@plazoentregaprovision_texto"
                param_plazoentregaprovision_texto.SqlDbType = SqlDbType.NVarChar
                param_plazoentregaprovision_texto.Size = 4000
                param_plazoentregaprovision_texto.Value = cmbPlazoEntregaProvision.Text.ToString
                param_plazoentregaprovision_texto.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    If bolModo = True Or txtID_OfertaComercial.Text = "0" Then
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_OfertasComerciales_Insert", _
                                              param_Id_OfertaComercial, param_IdPresupuesto, param_idMonedaPres, param_subtotal, param_iva, param_total, _
                                              param_Alcance, param_certificacion, param_certificacion_texto, param_ajuste, param_ajuste_texto, _
                                              param_plazoentrega, param_plazoentrega_texto, param_plazoentrega_provision, param_plazoentregaprovision_texto, _
                                              param_res)

                        res = param_res.Value

                        Me.txtID_OfertaComercial.Text = param_Id_OfertaComercial.Value

                    Else
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_OfertasComerciales_Update", _
                                              param_Id_OfertaComercial, param_IdPresupuesto, param_subtotal, param_iva, param_total, _
                                              param_Alcance, param_certificacion, param_certificacion_texto, param_ajuste, param_ajuste_texto, _
                                              param_plazoentrega, param_plazoentrega_texto, param_plazoentrega_provision, param_plazoentregaprovision_texto, _
                                              param_res)

                        res = param_res.Value
                    End If


                Catch ex As Exception
                    Throw ex
                End Try

                AgregarRegistro_OfertaComercial = res

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

    Private Function AgregarRegistro_Items() As Integer
        Dim res As Integer = 1
        Dim i As Integer

        Try

            Try

                For i = 0 To grdItems.RowCount - 2

                    If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value Is DBNull.Value) Then

                        Dim id As Long, Idmarca As Long, MarcaNueva As Long
                        Dim PrecioLista As Double, Ganancia As Double, Bonificacion As Double, PrecioVtaOrig As Double

                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
                            Dim cod_mat_prov As String, PlazoEntrega As String

                            'If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value Is DBNull.Value) And _
                            '    Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value Is Nothing) Then
                            '    cod_mat_prov = ""
                            'Else
                            cod_mat_prov = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value)
                            'End If

                            PrecioLista = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value
                            PlazoEntrega = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.PlazoEntrega).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.PlazoEntrega).Value)

                            Bonificacion = 0

                            If grdItems.Rows(i).Cells(ColumnasDelGridItems.marcanueva).Value Is DBNull.Value Then
                                MarcaNueva = 0
                            Else
                                MarcaNueva = 1
                            End If

                            If MarcaNueva = 1 Then
                                If Agregar_Marca(grdItems.Rows(i).Cells(ColumnasDelGridItems.Marca).Value, Idmarca) <= 0 Then
                                    AgregarRegistro_Items = -40
                                    Exit Function
                                End If
                            Else
                                Idmarca = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMarca).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMarca).Value)
                            End If

                            id = Agregar_Material(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value, grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, 0, PrecioLista, Ganancia, PlazoEntrega, cod_mat_prov, CDbl(grdItems.Rows(i).Cells(ColumnasDelGridItems.Iva).Value), grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value, Idmarca)

                            If id = -1 Then
                                MsgBox("Se produjo un error al insertar el material")
                                AgregarRegistro_Items = -30
                                Exit Function
                            End If

                        Else
                            id = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value, Long)
                            Idmarca = CType(IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMarca).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMarca).Value), Long)
                            PrecioLista = PrecioVtaOrig
                        End If

                        Dim param_id As New SqlClient.SqlParameter
                        param_id.ParameterName = "@id"
                        param_id.SqlDbType = SqlDbType.BigInt
                        If bolModo = False And Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.IdPresup_Det).Value Is DBNull.Value) Then
                            param_id.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdPresup_Det).Value, Long)
                        Else
                            param_id.Value = 0
                        End If
                        param_id.Direction = ParameterDirection.Input

                        Dim param_IdPresupuesto As New SqlClient.SqlParameter
                        param_IdPresupuesto.ParameterName = "@idpresupuesto"
                        param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                        param_IdPresupuesto.Value = txtID.Text
                        param_IdPresupuesto.Direction = ParameterDirection.Input

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
                        param_ganancia.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value, Double) ' Ganancia 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Ganancia).Value, Decimal)
                        param_ganancia.Direction = ParameterDirection.Input

                        Dim param_gananciaorig As New SqlClient.SqlParameter
                        param_gananciaorig.ParameterName = "@gananciaorig"
                        param_gananciaorig.SqlDbType = SqlDbType.Decimal
                        param_gananciaorig.Precision = 18
                        param_gananciaorig.Scale = 2
                        param_gananciaorig.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value, Double) 'GananciaOrig 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.gananciaorig).Value, Decimal)
                        param_gananciaorig.Direction = ParameterDirection.Input

                        Dim param_RecargoDesc As New SqlClient.SqlParameter
                        param_RecargoDesc.ParameterName = "@RecargoDesc_Det"
                        param_RecargoDesc.SqlDbType = SqlDbType.Bit
                        param_RecargoDesc.Value = False
                        param_RecargoDesc.Direction = ParameterDirection.Input

                        Dim param_porcrecargo As New SqlClient.SqlParameter
                        param_porcrecargo.ParameterName = "@porcrecargo_Det"
                        param_porcrecargo.SqlDbType = SqlDbType.Decimal
                        param_porcrecargo.Precision = 18
                        param_porcrecargo.Scale = 2
                        param_porcrecargo.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value)
                        param_porcrecargo.Direction = ParameterDirection.Input

                        Dim param_preciolista As New SqlClient.SqlParameter
                        param_preciolista.ParameterName = "@preciolista"
                        param_preciolista.SqlDbType = SqlDbType.Decimal
                        param_preciolista.Precision = 18
                        param_preciolista.Scale = 2
                        param_preciolista.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioLista).Value  'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100))
                        param_preciolista.Direction = ParameterDirection.Input

                        Dim param_preciovta As New SqlClient.SqlParameter
                        param_preciovta.ParameterName = "@preciovta"
                        param_preciovta.SqlDbType = SqlDbType.Decimal
                        param_preciovta.Precision = 18
                        param_preciovta.Scale = 2
                        param_preciovta.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value  'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) * (1 + (grdItems.Rows(i).Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100))
                        param_preciovta.Direction = ParameterDirection.Input

                        Dim param_preciovtaorig As New SqlClient.SqlParameter
                        param_preciovtaorig.ParameterName = "@preciovtaorig"
                        param_preciovtaorig.SqlDbType = SqlDbType.Decimal
                        param_preciovtaorig.Precision = 18
                        param_preciovtaorig.Scale = 2
                        param_preciovtaorig.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Double) 'PrecioVtaOrig 'CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.preciovtaorig).Value, Decimal)
                        param_preciovtaorig.Direction = ParameterDirection.Input

                        Dim param_subtotal As New SqlClient.SqlParameter
                        param_subtotal.ParameterName = "@subtotal"
                        param_subtotal.SqlDbType = SqlDbType.Decimal
                        param_subtotal.Precision = 18
                        param_subtotal.Scale = 2
                        param_subtotal.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double) * CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, Double)
                        grdItems.Rows(i).Cells(ColumnasDelGridItems.SubTotalProd).Value = param_subtotal.Value  ' Math.Round(CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Qty).Value, Double) * CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.PrecioVta).Value, Double), 2)
                        param_subtotal.Direction = ParameterDirection.Input

                        Dim param_plazoentrega As New SqlClient.SqlParameter
                        param_plazoentrega.ParameterName = "@plazoentrega"
                        param_plazoentrega.SqlDbType = SqlDbType.VarChar
                        param_plazoentrega.Size = 50
                        param_plazoentrega.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.PlazoEntrega).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.PlazoEntrega).Value)
                        param_plazoentrega.Direction = ParameterDirection.Input

                        Dim param_idproveedor As New SqlClient.SqlParameter
                        param_idproveedor.ParameterName = "@idproveedor"
                        param_idproveedor.SqlDbType = SqlDbType.BigInt
                        param_idproveedor.Value = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdProveedor).Value Is DBNull.Value, 0, grdItems.Rows(i).Cells(ColumnasDelGridItems.IdProveedor).Value)
                        param_idproveedor.Direction = ParameterDirection.Input

                        Dim param_idmarca As New SqlClient.SqlParameter
                        param_idmarca.ParameterName = "@idmarca"
                        param_idmarca.SqlDbType = SqlDbType.BigInt
                        param_idmarca.Value = Idmarca
                        param_idmarca.Direction = ParameterDirection.Input

                        Dim param_idmoneda As New SqlClient.SqlParameter
                        param_idmoneda.ParameterName = "@idmoneda"
                        param_idmoneda.SqlDbType = SqlDbType.BigInt
                        param_idmoneda.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IdMoneda).Value
                        param_idmoneda.Direction = ParameterDirection.Input

                        Dim param_valorcambio As New SqlClient.SqlParameter
                        param_valorcambio.ParameterName = "@valorcambio"
                        param_valorcambio.SqlDbType = SqlDbType.Decimal
                        param_valorcambio.Precision = 18
                        param_valorcambio.Scale = 2
                        param_valorcambio.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.ValorCambio).Value
                        param_valorcambio.Direction = ParameterDirection.Input

                        Dim param_iva As New SqlClient.SqlParameter
                        param_iva.ParameterName = "@IVA"
                        param_iva.SqlDbType = SqlDbType.Decimal
                        param_iva.Precision = 18
                        param_iva.Scale = 2
                        param_iva.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Iva).Value
                        param_iva.Direction = ParameterDirection.Input

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

                        Dim param_bonif1 As New SqlClient.SqlParameter
                        param_bonif1.ParameterName = "@bonif1"
                        param_bonif1.SqlDbType = SqlDbType.Decimal
                        param_bonif1.Precision = 18
                        param_bonif1.Scale = 2
                        param_bonif1.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonificacion1).Value
                        param_bonif1.Direction = ParameterDirection.Input

                        Dim param_bonif2 As New SqlClient.SqlParameter
                        param_bonif2.ParameterName = "@bonif2"
                        param_bonif2.SqlDbType = SqlDbType.Decimal
                        param_bonif2.Precision = 18
                        param_bonif2.Scale = 2
                        param_bonif2.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.Bonificacion2).Value
                        param_bonif2.Direction = ParameterDirection.Input

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
                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Det_Insert", _
                                                      param_IdPresupuesto, param_idmaterial, param_qty, param_idunidad, _
                                                      param_preciouni, param_ganancia, param_gananciaorig, _
                                                      param_preciolista, param_preciovta, param_preciovtaorig, param_subtotal, param_notadet, _
                                                      param_RecargoDesc, param_porcrecargo, param_plazoentrega, param_idproveedor, _
                                                      param_idmarca, param_idmoneda, param_valorcambio, param_iva, param_bonif1, param_bonif2, _
                                                      param_useradd, param_OrdenItem, param_res)

                                res = param_res.Value

                            Else
                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Det_Update", _
                                                    param_id, param_IdPresupuesto, param_idmaterial, param_qty, param_idunidad, _
                                                    param_preciouni, param_ganancia, param_gananciaorig, _
                                                    param_preciolista, param_preciovta, param_preciovtaorig, param_subtotal, param_notadet, _
                                                    param_RecargoDesc, param_porcrecargo, param_plazoentrega, param_idproveedor, _
                                                    param_idmarca, param_idmoneda, param_valorcambio, param_iva, param_bonif1, param_bonif2, _
                                                    param_useradd, param_OrdenItem, param_res, param_MENSAJE)

                                res = param_res.Value

                                If res = -20 Then
                                    MsgBox("La cantidad ingresada para el Item " & grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value & " es menor al saldo actual.", MsgBoxStyle.Critical, "Atención")
                                End If

                            End If

                            If res = -10 Then
                                Util.MsgStatus(Status1, "No se puede modificar el material '" & grdItems.Rows(i).Cells(ColumnasDelGridItems.Nombre_Material).Value.ToString.Substring(0, 30) & "...'" & vbCrLf & _
                                               "La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                            End If

                            If (res <= 0) Then
                                Exit For
                            End If

                        Catch ex As Exception
                            Throw ex
                            AgregarRegistro_Items = -1
                        End Try

                    End If

                Next

                AgregarRegistro_Items = res

            Catch ex2 As Exception
                Throw ex2
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            AgregarRegistro_Items = -1

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function AgregarRegistro_Items_OfertaComercial() As Integer
        Dim res As Integer = 1
        Dim i As Integer

        Try
            Try

                For i = 0 To grdOfertaComercial.RowCount - 2

                    If Not (grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value Is DBNull.Value) Then
                        If grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value <> "" Then

                            Dim param_Id As New SqlClient.SqlParameter
                            param_Id.ParameterName = "@Id"
                            param_Id.SqlDbType = SqlDbType.BigInt
                            If bolModo = True Then
                                param_Id.Value = DBNull.Value
                            Else
                                param_Id.Value = grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.IdDet).Value
                            End If
                            param_Id.Direction = ParameterDirection.Input

                            Dim param_IdPresupuesto_OfertaComercial As New SqlClient.SqlParameter
                            param_IdPresupuesto_OfertaComercial.ParameterName = "@IdPresupuesto_OfertaComercial"
                            param_IdPresupuesto_OfertaComercial.SqlDbType = SqlDbType.BigInt
                            param_IdPresupuesto_OfertaComercial.Value = txtID_OfertaComercial.Text
                            param_IdPresupuesto_OfertaComercial.Direction = ParameterDirection.Input

                            Dim param_orden As New SqlClient.SqlParameter
                            param_orden.ParameterName = "@orden"
                            param_orden.SqlDbType = SqlDbType.Int
                            param_orden.Value = i + 1
                            param_orden.Direction = ParameterDirection.Input

                            Dim param_descripcion As New SqlClient.SqlParameter
                            param_descripcion.ParameterName = "@descripcion"
                            param_descripcion.SqlDbType = SqlDbType.NVarChar
                            param_descripcion.Size = 4000
                            param_descripcion.Value = grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value
                            param_descripcion.Direction = ParameterDirection.Input

                            Dim param_preciosiniva As New SqlClient.SqlParameter
                            param_preciosiniva.ParameterName = "@preciosiniva"
                            param_preciosiniva.SqlDbType = SqlDbType.Decimal
                            param_preciosiniva.Precision = 18
                            param_preciosiniva.Scale = 2
                            param_preciosiniva.Value = CType(grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.PreciosinIva).Value, Double)
                            param_preciosiniva.Direction = ParameterDirection.Input

                            Dim param_IdPresupuesto As New SqlClient.SqlParameter
                            param_IdPresupuesto.ParameterName = "@IdPresupuesto"
                            param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                            param_IdPresupuesto.Value = txtID.Text
                            param_IdPresupuesto.Direction = ParameterDirection.Input

                            Dim param_res As New SqlClient.SqlParameter
                            param_res.ParameterName = "@res"
                            param_res.SqlDbType = SqlDbType.Int
                            param_res.Value = DBNull.Value
                            param_res.Direction = ParameterDirection.InputOutput

                            Try
                                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spPresupuestos_OfertasComerciales_Det_Insert]", _
                                                      param_Id, param_IdPresupuesto_OfertaComercial, param_orden, param_preciosiniva, param_descripcion, _
                                                      param_IdPresupuesto, param_res)

                                res = param_res.Value

                                If (res <= 0) Then
                                    If res = -20 Then
                                        Util.MsgStatus(Status1, "El item " & i + 1 & " está en un remito. No se puede modificar", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "El item " & i + 1 & " está en un remito. No se puede modificar", My.Resources.Resources.stop_error.ToBitmap, True)
                                    End If
                                    'Exit For
                                End If

                            Catch ex As Exception
                                Throw ex
                                AgregarRegistro_Items_OfertaComercial = -1
                            End Try

                        End If
                    End If

                Next

                AgregarRegistro_Items_OfertaComercial = res

            Catch ex2 As Exception
                Throw ex2
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            AgregarRegistro_Items_OfertaComercial = -1

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

                    Dim param_idpresupuesto As New SqlClient.SqlParameter
                    param_idpresupuesto.ParameterName = "@idpresupuesto"
                    param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                    param_idpresupuesto.Value = txtID.Text
                    param_idpresupuesto.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.Output

                    Try
                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Notas_Delete", param_idpresupuesto, param_res)
                        res = CInt(param_res.Value)
                        If (res < 0) Then
                            MsgBox("Existe un problema al intentar eliminar temporalmente las notas", MsgBoxStyle.Critical, "Presupuestos")
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

                'Dim ds_Notas As Data.DataSet

                'If txtCodMonedaPres.Text = "PE" Then
                '    ds_Notas = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "insert into Presupuestos_Notas (IdPresupuesto, nota) select " & txtID.Text & ", nota from notas where SoloPeso = 1 and eliminado = 0 ")
                'Else
                '    ds_Notas = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "insert into Presupuestos_Notas (IdPresupuesto, nota) select " & txtID.Text & ", nota from notas where SoloDolar = 1 and eliminado = 0 ")
                'End If

                'ds_Notas.Dispose()

                For i = 0 To lstNotas.Items.Count - 1

                    If lstNotas.Items(i).Checked = True Then

                        Dim param_idpresupuesto As New SqlClient.SqlParameter
                        param_idpresupuesto.ParameterName = "@idpresupuesto"
                        param_idpresupuesto.SqlDbType = SqlDbType.BigInt
                        param_idpresupuesto.Value = txtID.Text
                        param_idpresupuesto.Direction = ParameterDirection.Input

                        Dim param_nota As New SqlClient.SqlParameter
                        param_nota.ParameterName = "@nota"
                        param_nota.SqlDbType = SqlDbType.VarChar
                        param_nota.Size = 3000
                        param_nota.Value = lstNotas.Items(i).Text
                        param_nota.Direction = ParameterDirection.Input

                        Dim param_res As New SqlClient.SqlParameter
                        param_res.ParameterName = "@res"
                        param_res.SqlDbType = SqlDbType.Int
                        param_res.Value = DBNull.Value
                        param_res.Direction = ParameterDirection.Output

                        Try
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Notas_Insert", _
                                                      param_idpresupuesto, param_nota, param_res)

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

    Private Function AgregarRegistro_Contactos(ByRef IdContacto As Long, ByVal NombreContacto As String) As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim i As Integer = 0

        Try

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_idcliente As New SqlClient.SqlParameter
            param_idcliente.ParameterName = "@idcliente"
            param_idcliente.SqlDbType = SqlDbType.BigInt
            param_idcliente.Value = txtIdCliente.Text
            param_idcliente.Direction = ParameterDirection.Input

            Dim param_codigo_contacto As New SqlClient.SqlParameter
            param_codigo_contacto.ParameterName = "@codigo_contacto"
            param_codigo_contacto.SqlDbType = SqlDbType.VarChar
            param_codigo_contacto.Size = 25
            param_codigo_contacto.Value = ""
            param_codigo_contacto.Direction = ParameterDirection.Input

            Dim param_nombre_contacto As New SqlClient.SqlParameter
            param_nombre_contacto.ParameterName = "@nombre_contacto"
            param_nombre_contacto.SqlDbType = SqlDbType.VarChar
            param_nombre_contacto.Size = 50
            param_nombre_contacto.Value = NombreContacto 'cmbCompradores.Text.ToString
            param_nombre_contacto.Direction = ParameterDirection.Input

            Dim param_telefono_contacto As New SqlClient.SqlParameter
            param_telefono_contacto.ParameterName = "@telefono_contacto"
            param_telefono_contacto.SqlDbType = SqlDbType.VarChar
            param_telefono_contacto.Size = 30
            param_telefono_contacto.Value = ""
            param_telefono_contacto.Direction = ParameterDirection.Input

            Dim param_email_contacto As New SqlClient.SqlParameter
            param_email_contacto.ParameterName = "@email_contacto"
            param_email_contacto.SqlDbType = SqlDbType.VarChar
            param_email_contacto.Size = 50
            param_email_contacto.Value = ""
            param_email_contacto.Direction = ParameterDirection.Input

            Dim param_celular_contacto As New SqlClient.SqlParameter
            param_celular_contacto.ParameterName = "@celular_contacto"
            param_celular_contacto.SqlDbType = SqlDbType.VarChar
            param_celular_contacto.Size = 30
            param_celular_contacto.Value = ""
            param_celular_contacto.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spClientes_Contactos_Insert", param_id, param_idcliente, _
                                          param_codigo_contacto, param_nombre_contacto, param_telefono_contacto, param_celular_contacto, _
                                          param_email_contacto, param_useradd, param_res)

                If param_res.Value = -1 Then
                    AgregarRegistro_Contactos = param_res.Value
                    Exit Function
                End If

                IdContacto = param_id.Value
                AgregarRegistro_Contactos = param_res.Value

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


    Private Function ControlarCantidadRegistros() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim i As Integer
        'Dim CantRegistros As Integer

        Try
            'connection = SqlHelper.GetConnection(ConnStringSEI)

            If rdTrafo.Checked = False Then

                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.Text, "DELETE FROM TMP_Presupuestos_Det WHERE IdPresupuesto = " & txtID.Text)

                If chkAgregarOfertaComercial.Checked = True Then
                    SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.Text, "DELETE FROM TMP_Presupuestos_OfertasComerciales_Det WHERE IdPresupuesto_OfertaComercial = " & txtID_OfertaComercial.Text) 'grd.CurrentRow.Cells(0).Value)

                    For i = 0 To grdOfertaComercial.RowCount - 2
                        If Not (grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value Is DBNull.Value) Then
                            If grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value <> "" Then

                                Dim param_id As New SqlClient.SqlParameter
                                param_id.ParameterName = "@idpresupuesto_OfertaComercial"
                                param_id.SqlDbType = SqlDbType.BigInt
                                param_id.Value = txtID_OfertaComercial.Text
                                param_id.Direction = ParameterDirection.Input

                                Dim param_descripcion As New SqlClient.SqlParameter
                                param_descripcion.ParameterName = "@descripcion"
                                param_descripcion.SqlDbType = SqlDbType.NVarChar
                                param_descripcion.Size = 4000
                                param_descripcion.Value = grdOfertaComercial.Rows(i).Cells(ColumnasDelGridOfertaComercial.Descripcion).Value
                                param_descripcion.Direction = ParameterDirection.Input

                                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_ControlarCantidadRegistros_OfertaComercial", _
                                                            param_id, param_descripcion)

                            End If
                        End If

                    Next


                End If

                i = 0


                For i = 0 To grdItems.RowCount - 1
                    If Not (grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value Is DBNull.Value) Then
                        If grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value > 0 Then

                            Dim param_id As New SqlClient.SqlParameter
                            param_id.ParameterName = "@idpresupuesto"
                            param_id.SqlDbType = SqlDbType.BigInt
                            param_id.Value = txtID.Text
                            param_id.Direction = ParameterDirection.Input

                            Dim param_idmaterial As New SqlClient.SqlParameter
                            param_idmaterial.ParameterName = "@idmaterial"
                            param_idmaterial.SqlDbType = SqlDbType.BigInt
                            param_idmaterial.Value = grdItems.Rows(i).Cells(ColumnasDelGridItems.IDMaterial).Value
                            param_idmaterial.Direction = ParameterDirection.Input

                            SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_ControlarCantidadRegistros", _
                                                        param_id, param_idmaterial)

                        End If
                    End If

                Next
            Else

                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.Text, "DELETE FROM TMP_Presupuestos_Trafos_Det WHERE IdPresupuesto = " & txtID.Text) 'grd.CurrentRow.Cells(0).Value)

                For i = 0 To grdTrafos_Det.RowCount - 2
                    If Not (grdTrafos_Det.Rows(i).Cells(ColumnasDelGridTrafo_Det.Descripcion).Value Is DBNull.Value) Then
                        If grdTrafos_Det.Rows(i).Cells(ColumnasDelGridTrafo_Det.Descripcion).Value <> "" Then

                            Dim param_id As New SqlClient.SqlParameter
                            param_id.ParameterName = "@idpresupuesto"
                            param_id.SqlDbType = SqlDbType.BigInt
                            param_id.Value = txtID.Text
                            param_id.Direction = ParameterDirection.Input

                            Dim param_descripcion As New SqlClient.SqlParameter
                            param_descripcion.ParameterName = "@descripcion_trabajo"
                            param_descripcion.SqlDbType = SqlDbType.VarChar
                            param_descripcion.Size = 500
                            param_descripcion.Value = grdTrafos_Det.Rows(i).Cells(ColumnasDelGridTrafo_Det.Descripcion).Value
                            param_descripcion.Direction = ParameterDirection.Input

                            SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_ControlarCantidadRegistros_Trafos_Det", _
                                                        param_id, param_descripcion)

                        End If
                    End If

                Next

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
        Finally
            'If Not connection Is Nothing Then
            ' CType(connection, IDisposable).Dispose()
            ' End If
        End Try
    End Function

    Private Function ControlarItemdentrodeRemito(ByVal idmaterial As Long) As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            'connection = SqlHelper.GetConnection(ConnStringSEI)

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idpresupuesto"
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


            SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_ControlarItemDentrodeRemito", _
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
            'If Not connection Is Nothing Then
            ' CType(connection, IDisposable).Dispose()
            ' End If
        End Try
    End Function

    Private Function EliminarItems_Presupuesto() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idpresupuesto"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_EliminarItems_Det", _
                                        param_id, param_res)

            EliminarItems_Presupuesto = param_res.Value

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

    Private Function EliminarItems_Presupuesto_OfertaComercial() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idpresupuesto_ofertacomercial"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID_OfertaComercial.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_OfertasComerciales_EliminarItems_Det", _
                                        param_id, param_res)

            EliminarItems_Presupuesto_OfertaComercial = param_res.Value

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

    Private Function EliminarItems_Presupuesto_Trafos() As Integer
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@idpresupuesto"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = CLng(txtID.Text)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spPresupuestos_Trafo_EliminarItems_Det", _
                                        param_id, param_res)

            EliminarItems_Presupuesto_Trafos = param_res.Value

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

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        'Dim connection As SqlClient.SqlConnection = Nothing

        Try
            'connection = SqlHelper.GetConnection(ConnStringSEI)

            Try

                Dim param_idconsumo As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
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

                'Dim param_msg As New SqlClient.SqlParameter
                'param_msg.ParameterName = "@mensaje"
                'param_msg.SqlDbType = SqlDbType.VarChar
                'param_msg.Size = 250
                'param_msg.Value = DBNull.Value
                'param_msg.Direction = ParameterDirection.Output

                Try

                    SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "[spPresupuestos_Delete]", _
                            param_idconsumo, param_userdel, param_res)

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Function

    'Private Function ActualizarConsumoImpreso(ByVal idconsumo As Long) As Integer
    '    'Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim res As Integer = 0

    '    Try
    '        Try
    '            connection = SqlHelper.GetConnection(ConnStringSEI)
    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Function
    '        End Try

    '        Try

    '            Dim param_id As New SqlClient.SqlParameter
    '            param_id.ParameterName = "@id"
    '            param_id.SqlDbType = SqlDbType.BigInt
    '            param_id.Value = idconsumo
    '            param_id.Direction = ParameterDirection.Input

    '            Dim param_res As New SqlClient.SqlParameter
    '            param_res.ParameterName = "@res"
    '            param_res.SqlDbType = SqlDbType.Int
    '            param_res.Value = DBNull.Value
    '            param_res.Direction = ParameterDirection.InputOutput

    '            Try
    '                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spConsumos_Update_Impreso", param_id, param_res)
    '                res = param_res.Value
    '                ActualizarConsumoImpreso = res

    '            Catch ex As Exception
    '                Throw ex
    '            End Try
    '        Finally

    '        End Try
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
    'End Function

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

    Private Sub BuscarContacto(ByRef Compradores As Integer, ByRef Usuarios As Integer)
        Dim dsContacto As Data.DataSet

        Try
            dsContacto = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select count(*) from clientes c Join Clientes_Contacto CC on cc.idcliente = c.id where c.id = " & txtIdCliente.Text & " and cc.nombre_contacto = '" & cmbCompradores.Text.ToString & "'")
            dsContacto.Dispose()

            If dsContacto.Tables(0).Rows.Count > 0 Then
                Compradores = dsContacto.Tables(0).Rows(0).Item(0)
            Else
                Compradores = 0
            End If

            dsContacto = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "select count(*) from clientes c Join Clientes_Contacto CC on cc.idcliente = c.id where c.id = " & txtIdCliente.Text & " and cc.nombre_contacto = '" & cmbUsuarios.Text.ToString & "'")

            If dsContacto.Tables(0).Rows.Count > 0 Then
                Usuarios = dsContacto.Tables(0).Rows(0).Item(0)
            Else
                Usuarios = 0
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
            'Finally
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

        Try

            ' Si la tecla presionada es distinta de la tecla Enter,
            ' abandonamos el procedimiento.


            If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)


            ' Igualmente, si el control DataGridView no tiene el foco,
            ' y si la celda actual no está siendo editada,
            ' abandonamos el procedimiento.
            '        If ((Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode)) And _
            '           ((Not grdOfertaComercial.Focused) AndAlso (Not grdOfertaComercial.IsCurrentCellInEditMode)) Then Return MyBase.ProcessCmdKey(msg, keyData)

            Dim cell As DataGridViewCell

            Dim columnIndex As Int32
            Dim rowIndex As Int32

            If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then
                If (Not grdOfertaComercial.Focused) AndAlso (Not grdOfertaComercial.IsCurrentCellInEditMode) Then
                    Return MyBase.ProcessCmdKey(msg, keyData)
                Else
                    cell = grdOfertaComercial.CurrentCell

                    columnIndex = cell.ColumnIndex
                    rowIndex = cell.RowIndex

                    Do
                        If columnIndex = grdOfertaComercial.Columns.Count - 1 Then
                            If rowIndex = grdOfertaComercial.Rows.Count - 1 Then
                                ' Seleccionamos la primera columna de la primera fila.
                                cell = grdOfertaComercial.Rows(0).Cells(ColumnasDelGridOfertaComercial.IdPresupOT)
                            Else
                                ' Selecionamos la primera columna de la siguiente fila.
                                cell = grdOfertaComercial.Rows(rowIndex + 1).Cells(ColumnasDelGridOfertaComercial.IdPresupOT_Det)
                            End If
                        Else
                            ' Seleccionamos la celda de la derecha de la celda actual.
                            cell = grdOfertaComercial.Rows(rowIndex).Cells(columnIndex + 1)
                        End If
                        ' establecer la fila y la columna actual
                        columnIndex = cell.ColumnIndex
                        rowIndex = cell.RowIndex
                    Loop While (cell.Visible = False)

                    grdOfertaComercial.CurrentCell = cell

                    'SendKeys.Send("{TAB}")

                    If grdOfertaComercial.CurrentCell.ColumnIndex - 2 = ColumnasDelGridOfertaComercial.PreciosinIva Then
                        grdOfertaComercial.CurrentCell = grdOfertaComercial(ColumnasDelGridOfertaComercial.Descripcion, grdOfertaComercial.CurrentRow.Index + 1)
                        Return True
                    End If

                    ' ... y la ponemos en modo de edición.
                    grdOfertaComercial.BeginEdit(True)

                End If
            Else
                cell = grdItems.CurrentCell

                columnIndex = cell.ColumnIndex
                rowIndex = cell.RowIndex

                Do
                    If columnIndex = grdItems.Columns.Count - 1 Then
                        If rowIndex = grdItems.Rows.Count - 1 Then
                            ' Seleccionamos la primera columna de la primera fila.
                            cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdPresup_Det)
                        Else
                            ' Selecionamos la primera columna de la siguiente fila.
                            cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdPresup_Det)
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

                'SendKeys.Send("{TAB}")
                Try
                    If grdItems.CurrentCell.ColumnIndex - 2 = ColumnasDelGridItems.PlazoEntrega Then
                        grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material, grdItems.CurrentRow.Index + 1)
                        Return True
                    End If

                    If grdItems.CurrentCell.ColumnIndex - 1 = ColumnasDelGridItems.Cod_Material Then
                        If IIf(grdItems.CurrentCell.Value Is DBNull.Value, "", grdItems.CurrentCell.Value) = "" Then
                            grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material_Prov, grdItems.CurrentRow.Index)
                        Else
                            grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Ganancia, grdItems.CurrentRow.Index)
                        End If

                        Return True
                    End If

                Catch ex As Exception
                    MsgBox("Debe ingresar el Codigo, Descripción del Material o Cantidad a Presupuestar", MsgBoxStyle.Critical)
                    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Cod_Material, grdItems.CurrentRow.Index)
                    Return False
                End Try

                ' ... y la ponemos en modo de edición.
                grdItems.BeginEdit(True)

            End If

            Return True

        Catch ex As Exception
            Return False

        End Try

    End Function

    Private Function Agregar_Material(ByVal Unidad As Long, ByVal Codigo As String, ByVal Nombre As String, _
                                     ByVal PrecioVta As Double, ByVal Stock As Double, ByVal preciolista As Double, _
                                     ByVal Ganancia As Double, ByVal PlazoEntrega As String, ByVal Codigo_Mat_Prov As String, _
                                     ByVal IVA As Double, ByVal IdMoneda As Long, ByVal idMarca As Long) As Long
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
                                    param_Estante, param_Fila, param_ControlStock, param_useradd, param_res)

                res = param_res.Value

                If res > 0 Then
                    ultid = param_id.Value
                    res = Agregar_Proveedor(param_id.Value, Codigo_Mat_Prov, Unidad, PrecioVta, preciolista, PlazoEntrega, IVA, Ganancia, IdMoneda)

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
                                       ByVal IVA As Double, ByVal ganancia As Double, ByVal idmonedaCompra As Long) As Integer

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
        param_idProveedor.Value = 0 'txtIdProveedor.Text
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
        param_bonificacion1.Value = 0 '0IIf(chkPrecioDistribuidor.Checked = False, bonif1, 0)
        param_bonificacion1.Direction = ParameterDirection.Input

        Dim param_bonificacion2 As New SqlClient.SqlParameter
        param_bonificacion2.ParameterName = "@bonif2"
        param_bonificacion2.SqlDbType = SqlDbType.Decimal
        param_bonificacion2.Precision = 18
        param_bonificacion2.Scale = 2
        param_bonificacion2.Value = 0 'IIf(chkPrecioDistribuidor.Checked = False, bonif2, 0)
        param_bonificacion2.Direction = ParameterDirection.Input

        Dim param_bonif1_dis As New SqlClient.SqlParameter
        param_bonif1_dis.ParameterName = "@bonif1_dis"
        param_bonif1_dis.SqlDbType = SqlDbType.Decimal
        param_bonif1_dis.Precision = 18
        param_bonif1_dis.Scale = 2
        param_bonif1_dis.Value = 0 'IIf(chkPrecioDistribuidor.Checked = True, bonif1, 0)
        param_bonif1_dis.Direction = ParameterDirection.Input

        Dim param_bonif2_dis As New SqlClient.SqlParameter
        param_bonif2_dis.ParameterName = "@bonif2_dis"
        param_bonif2_dis.SqlDbType = SqlDbType.Decimal
        param_bonif2_dis.Precision = 18
        param_bonif2_dis.Scale = 2
        param_bonif2_dis.Value = 0 'IIf(chkPrecioDistribuidor.Checked = True, bonif2, 0)
        param_bonif2_dis.Direction = ParameterDirection.Input

        Dim param_bonificacion3 As New SqlClient.SqlParameter
        param_bonificacion3.ParameterName = "@bonif3"
        param_bonificacion3.SqlDbType = SqlDbType.Decimal
        param_bonificacion3.Precision = 18
        param_bonificacion3.Scale = 2
        param_bonificacion3.Value = 0 ' bonif3
        param_bonificacion3.Direction = ParameterDirection.Input

        Dim param_bonificacion4 As New SqlClient.SqlParameter
        param_bonificacion4.ParameterName = "@bonif4"
        param_bonificacion4.SqlDbType = SqlDbType.Decimal
        param_bonificacion4.Precision = 18
        param_bonificacion4.Scale = 2
        param_bonificacion4.Value = 0 'bonif4
        param_bonificacion4.Direction = ParameterDirection.Input

        Dim param_bonificacion5 As New SqlClient.SqlParameter
        param_bonificacion5.ParameterName = "@bonif5"
        param_bonificacion5.SqlDbType = SqlDbType.Decimal
        param_bonificacion5.Precision = 18
        param_bonificacion5.Scale = 2
        param_bonificacion5.Value = 0 'bonif5
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
        param_preciolista.Value = IIf(chkPrecioDistribuidor.Checked = False, preciolista, 0)
        param_preciolista.Direction = ParameterDirection.Input

        Dim param_preciolista_dis As New SqlClient.SqlParameter
        param_preciolista_dis.ParameterName = "@preciolista_distribuidor"
        param_preciolista_dis.SqlDbType = SqlDbType.Decimal
        param_preciolista_dis.Precision = 18
        param_preciolista_dis.Scale = 2
        param_preciolista_dis.Value = IIf(chkPrecioDistribuidor.Checked = True, preciolista, 0)
        param_preciolista_dis.Direction = ParameterDirection.Input

        Dim param_preciovtasiniva As New SqlClient.SqlParameter
        param_preciovtasiniva.ParameterName = "@PrecioVentaSinIva"
        param_preciovtasiniva.SqlDbType = SqlDbType.Decimal
        param_preciovtasiniva.Precision = 18
        param_preciovtasiniva.Scale = 2
        param_preciovtasiniva.Value = IIf(chkPrecioDistribuidor.Checked = False, precio, 0)
        param_preciovtasiniva.Direction = ParameterDirection.Input

        Dim param_preciovtaxbulto As New SqlClient.SqlParameter
        param_preciovtaxbulto.ParameterName = "@PrecioVentaxBulto"
        param_preciovtaxbulto.SqlDbType = SqlDbType.Decimal
        param_preciovtaxbulto.Precision = 18
        param_preciovtaxbulto.Scale = 2
        param_preciovtaxbulto.Value = IIf(chkPrecioDistribuidor.Checked = False, precio, 0)
        param_preciovtaxbulto.Direction = ParameterDirection.Input

        Dim param_preciovtasiniva_dis As New SqlClient.SqlParameter
        param_preciovtasiniva_dis.ParameterName = "@PrecioVentaSinIva_Dis"
        param_preciovtasiniva_dis.SqlDbType = SqlDbType.Decimal
        param_preciovtasiniva_dis.Precision = 18
        param_preciovtasiniva_dis.Scale = 2
        param_preciovtasiniva_dis.Value = IIf(chkPrecioDistribuidor.Checked = True, precio, 0)
        param_preciovtasiniva_dis.Direction = ParameterDirection.Input

        Dim param_preciovtaxbulto_dis As New SqlClient.SqlParameter
        param_preciovtaxbulto_dis.ParameterName = "@PrecioVentaxBulto_dis"
        param_preciovtaxbulto_dis.SqlDbType = SqlDbType.Decimal
        param_preciovtaxbulto_dis.Precision = 18
        param_preciovtaxbulto_dis.Scale = 2
        param_preciovtaxbulto_dis.Value = IIf(chkPrecioDistribuidor.Checked = True, precio, 0)
        param_preciovtaxbulto_dis.Direction = ParameterDirection.Input

        Dim param_cantxbulto As New SqlClient.SqlParameter
        param_cantxbulto.ParameterName = "@CantxBulto"
        param_cantxbulto.SqlDbType = SqlDbType.Int
        param_cantxbulto.Value = 1
        param_cantxbulto.Direction = ParameterDirection.Input

        Dim param_iva As New SqlClient.SqlParameter
        param_iva.ParameterName = "@Iva"
        param_iva.SqlDbType = SqlDbType.Decimal
        param_iva.Precision = 18
        param_iva.Scale = 2
        param_iva.Value = IIf(chkPrecioDistribuidor.Checked = False, IVA, 0)
        param_iva.Direction = ParameterDirection.Input

        Dim param_iva_dis As New SqlClient.SqlParameter
        param_iva_dis.ParameterName = "@Iva_dis"
        param_iva_dis.SqlDbType = SqlDbType.Decimal
        param_iva_dis.Precision = 18
        param_iva_dis.Scale = 2
        param_iva_dis.Value = IIf(chkPrecioDistribuidor.Checked = True, IVA, 0)
        param_iva_dis.Direction = ParameterDirection.Input

        Dim param_idmarca As New SqlClient.SqlParameter
        param_idmarca.ParameterName = "@idmarca"
        param_idmarca.SqlDbType = SqlDbType.BigInt
        param_idmarca.Value = 0
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
                                      param_preciolista, param_preciolista_dis, param_preciovtasiniva, param_preciovtasiniva_dis, _
                                      param_preciovtaxbulto, param_preciovtaxbulto_dis, param_cantxbulto, _
                                      param_iva, param_iva_dis, param_idmarca, param_res)


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

    Private Function BuscarNombreArchivo(ByVal codigo As String, ByRef OfertaComercial As Boolean, ByRef Trafo As Boolean, ByRef ManoObra As Boolean) As String
        Dim ds_Archivo As Data.DataSet
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    BuscarNombreArchivo = ""
        '    Exit Function
        'End Try

        Try

            ds_Archivo = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT OfertaComercial, p.codigo collate Latin1_General_CI_AS + ' - ' + c.nombre + ' - ' + nombrereferencia " & _
                            ", trafo, ISNULL(manoobra,0) AS ManoObra FROM Presupuestos p JOIN Clientes c ON c.id = p.idclientE WHERE p.eliminado = 0 and p.codigo = '" & codigo & "'")

            ds_Archivo.Dispose()

            OfertaComercial = CBool(ds_Archivo.Tables(0).Rows(0)(0))
            BuscarNombreArchivo = ds_Archivo.Tables(0).Rows(0)(1)
            Trafo = CBool(ds_Archivo.Tables(0).Rows(0)(2))
            ManoObra = CBool(ds_Archivo.Tables(0).Rows(0)(3))

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            BuscarNombreArchivo = ""

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Function

    Private Function AgregarAcutalizar_Registro_Trafos_Ensayos() As Integer
        Dim res As Integer = 1
        Dim i As Integer

        Try

            Try

                For i = 0 To grdTrafos_Ensayos.RowCount - 1

                    'If grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value = True Then

                    Dim param_Id As New SqlClient.SqlParameter
                    param_Id.ParameterName = "@Id"
                    param_Id.SqlDbType = SqlDbType.BigInt
                    If bolModo = False Then
                        param_Id.Value = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Id).Value
                    Else
                        param_Id.Value = DBNull.Value
                    End If
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdPresupuesto As New SqlClient.SqlParameter
                    param_IdPresupuesto.ParameterName = "@IdPresupuesto"
                    param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                    param_IdPresupuesto.Value = txtID.Text
                    param_IdPresupuesto.Direction = ParameterDirection.Input

                    Dim param_Item As New SqlClient.SqlParameter
                    param_Item.ParameterName = "@Item"
                    param_Item.SqlDbType = SqlDbType.Float
                    param_Item.Value = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Item).Value
                    param_Item.Direction = ParameterDirection.Input

                    Dim param_DescripcionEnsayo As New SqlClient.SqlParameter
                    param_DescripcionEnsayo.ParameterName = "@DescripcionEnsayo"
                    param_DescripcionEnsayo.SqlDbType = SqlDbType.NVarChar
                    param_DescripcionEnsayo.Size = 500
                    param_DescripcionEnsayo.Value = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Descripcion).Value
                    param_DescripcionEnsayo.Direction = ParameterDirection.Input

                    Dim param_Marcado As New SqlClient.SqlParameter
                    param_Marcado.ParameterName = "@Marcado"
                    param_Marcado.SqlDbType = SqlDbType.Bit
                    param_Marcado.Value = grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Marcar).Value
                    param_Marcado.Direction = ParameterDirection.Input

                    Dim param_PrecioUnitario As New SqlClient.SqlParameter
                    param_PrecioUnitario.ParameterName = "@PrecioUnitario"
                    param_PrecioUnitario.SqlDbType = SqlDbType.Decimal
                    param_PrecioUnitario.Precision = 18
                    param_PrecioUnitario.Scale = 2
                    param_PrecioUnitario.Value = CType(grdTrafos_Ensayos.Rows(i).Cells(ColumnasDelGridTrafo_Ensayos.Precio).Value, Double)
                    param_PrecioUnitario.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        If bolModo = True Then
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spPresupuestos_Trafos_Ensayos_Insert]", _
                                              param_IdPresupuesto, param_Item, param_DescripcionEnsayo, param_Marcado, param_PrecioUnitario, param_res)

                            res = param_res.Value
                        Else
                            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spPresupuestos_Trafos_Ensayos_Update]", _
                                          param_Id, param_IdPresupuesto, param_Item, param_DescripcionEnsayo, param_Marcado, param_PrecioUnitario, param_res)

                            res = param_res.Value
                        End If

                        If (res <= 0) Then
                            Exit For
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                    'End If
                Next

                AgregarAcutalizar_Registro_Trafos_Ensayos = res

            Catch ex2 As Exception
                Throw ex2
                AgregarAcutalizar_Registro_Trafos_Ensayos = -1

            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            AgregarAcutalizar_Registro_Trafos_Ensayos = -1

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function AgregarRegistro_Trafos_Det() As Integer
        Dim res As Integer = 1
        Dim i As Integer

        Try

            Try

                For i = 0 To grdTrafos_Det.RowCount - 2

                    Dim param_IdPresupuesto As New SqlClient.SqlParameter
                    param_IdPresupuesto.ParameterName = "@IdPresupuesto"
                    param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                    param_IdPresupuesto.Value = txtID.Text
                    param_IdPresupuesto.Direction = ParameterDirection.Input

                    Dim param_Descripcion_trabajo As New SqlClient.SqlParameter
                    param_Descripcion_trabajo.ParameterName = "@Descripcion_trabajo"
                    param_Descripcion_trabajo.SqlDbType = SqlDbType.NVarChar
                    param_Descripcion_trabajo.Size = 500
                    param_Descripcion_trabajo.Value = grdTrafos_Det.Rows(i).Cells(2).Value
                    param_Descripcion_trabajo.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = DBNull.Value
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[spPresupuestos_Trafos_Det_Insert]", _
                                              param_IdPresupuesto, param_Descripcion_trabajo, param_res)

                        res = param_res.Value

                        If (res <= 0) Then
                            Exit For
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                Next

                AgregarRegistro_Trafos_Det = res

            Catch ex2 As Exception
                Throw ex2
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            AgregarRegistro_Trafos_Det = -1

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function EliminarRegistro_OfertaComercial(ByVal IdOferta_Det As Long) As Integer
        Dim res As Integer = 1

        Try

            Try

                Dim param_IdOfertaComercial_Det As New SqlClient.SqlParameter
                param_IdOfertaComercial_Det.ParameterName = "@IdOfertaComercial_Det"
                param_IdOfertaComercial_Det.SqlDbType = SqlDbType.BigInt
                param_IdOfertaComercial_Det.Value = IdOferta_Det
                param_IdOfertaComercial_Det.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_OfertasComerciales_Det_BuscarEnRemitos", _
                                      param_IdOfertaComercial_Det, param_res)

                res = param_res.Value

                EliminarRegistro_OfertaComercial = res

            Catch ex2 As Exception
                Throw ex2
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            EliminarRegistro_OfertaComercial = -1

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function ActualizarPresupuesto_DesdeOfertaComercial() As Integer
        Dim res As Integer

        Try

            Try

                Dim param_IdPresupuesto As New SqlClient.SqlParameter
                param_IdPresupuesto.ParameterName = "@IdPresupuesto"
                param_IdPresupuesto.SqlDbType = SqlDbType.BigInt
                param_IdPresupuesto.Value = txtID.Text
                param_IdPresupuesto.Direction = ParameterDirection.Input

                Dim param_IdOfertaComercial As New SqlClient.SqlParameter
                param_IdOfertaComercial.ParameterName = "@IdPresupuestoOf"
                param_IdOfertaComercial.SqlDbType = SqlDbType.BigInt
                param_IdOfertaComercial.Value = txtID_OfertaComercial.Text
                param_IdOfertaComercial.Direction = ParameterDirection.Input

                Dim param_subtotalDO As New SqlClient.SqlParameter
                param_subtotalDO.ParameterName = "@subtotalPre"
                param_subtotalDO.SqlDbType = SqlDbType.Decimal
                param_subtotalDO.Precision = 18
                param_subtotalDO.Scale = 2
                param_subtotalDO.Value = CType(txtSubtotal.Text, Double)
                param_subtotalDO.Direction = ParameterDirection.Input

                Dim param_totalDO As New SqlClient.SqlParameter
                param_totalDO.ParameterName = "@totalPre"
                param_totalDO.SqlDbType = SqlDbType.Decimal
                param_totalDO.Precision = 18
                param_totalDO.Scale = 2
                param_totalDO.Value = CType(txtTotal.Text, Double)
                param_totalDO.Direction = ParameterDirection.Input

                Dim param_iva21DO As New SqlClient.SqlParameter
                param_iva21DO.ParameterName = "@iva21DO"
                param_iva21DO.SqlDbType = SqlDbType.Decimal
                param_iva21DO.Precision = 18
                param_iva21DO.Scale = 2
                param_iva21DO.Value = txtIva21.Text
                param_iva21DO.Direction = ParameterDirection.Input

                Dim param_iva10DO As New SqlClient.SqlParameter
                param_iva10DO.ParameterName = "@iva10DO"
                param_iva10DO.SqlDbType = SqlDbType.Decimal
                param_iva10DO.Precision = 18
                param_iva10DO.Scale = 2
                param_iva10DO.Value = txtIva10.Text
                param_iva10DO.Direction = ParameterDirection.Input

                Dim param_subtotalof As New SqlClient.SqlParameter
                param_subtotalof.ParameterName = "@subtotalof"
                param_subtotalof.SqlDbType = SqlDbType.Decimal
                param_subtotalof.Precision = 18
                param_subtotalof.Scale = 2
                param_subtotalof.Value = txtSubtotalOferta.Text
                param_subtotalof.Direction = ParameterDirection.Input

                Dim param_ivaof As New SqlClient.SqlParameter
                param_ivaof.ParameterName = "@IVAof"
                param_ivaof.SqlDbType = SqlDbType.Decimal
                param_ivaof.Precision = 18
                param_ivaof.Scale = 2
                param_ivaof.Value = txtIvaOferta.Text
                param_ivaof.Direction = ParameterDirection.Input

                Dim param_totalof As New SqlClient.SqlParameter
                param_totalof.ParameterName = "@totalof"
                param_totalof.SqlDbType = SqlDbType.Decimal
                param_totalof.Precision = 18
                param_totalof.Scale = 2
                param_totalof.Value = txtTotalOferta.Text
                param_totalof.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spPresupuestos_Update_Totales", _
                                      param_IdPresupuesto, param_IdOfertaComercial, param_subtotalDO, param_iva21DO, _
                                      param_iva10DO, param_totalDO, param_subtotalof, param_ivaof, param_totalof, param_res)

                res = param_res.Value

                ActualizarPresupuesto_DesdeOfertaComercial = res

            Catch ex2 As Exception
                Throw ex2
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            ActualizarPresupuesto_DesdeOfertaComercial = -1

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


    Private Sub TimerGuadar_Tick(sender As Object, e As EventArgs) Handles TimerGuardar.Tick
        GuardarDesdeTimer = True
        btnGuardar_Click(sender, e)
        'If chkAgregarOfertaComercial.Checked = True Then
        '    LlenarGrid_OfertaComercial(grd.CurrentRow.Cells(0).Value)
        'End If
        GuardarDesdeTimer = False
    End Sub


    ' Esta función recibe un número y devuelve una cadena de caracteres conteniendo el texto correspondiente al número recibido.
    ' Los decimales (centavos) se colocan literalmente al final de la cadena con el formato xx/100 (xx son los dígitos del valor decimal).
    ' La función "habla" sobre todo en número de más de miles de millones.
    Public Function letras(ByVal nCifra As Object) As String
        ' Defino variables
        Dim cifra, bloque, decimales, cadena As String
        Dim posision, unidadmil As Byte

        ' En caso de que unidadmil sea:
        ' 0 = cientos
        ' 1 = miles
        ' 2 = millones
        ' 3 = miles de millones
        ' 4 = billones
        ' 5 = miles de billones

        ' Reemplazo el símbolo decimal por un punto (.) y luego guardo la parte entera y la decimal por separado
        ' Es necesario poner el cero a la izquierda del punto así si el valor es de sólo decimales, se lo fuerza
        ' a colocar el cero para que no genere error
        cifra = Format(CType(nCifra, Decimal), "###############0.#0")
        decimales = Mid(cifra, Len(cifra) - 1, 2)
        'cifra = Mid(cifra, Len(cifra) - 3)
        cifra = cifra.Substring(0, Len(cifra) - 3)

        ' Verifico que el valor no sea cero
        If cifra = "0" Then
            Return IIf(decimales = "00", "cero", "cero con " & decimales & "/100")
        End If

        ' Evaluo su longitud (como mínimo una cadena debe tener 3 dígitos)
        If Len(cifra) < 3 Then
            cifra = Rellenar(cifra, 3)
        End If

        ' Invierto la cadena
        cifra = Invertir(cifra)

        ' Inicializo variables
        posision = 1
        unidadmil = 0
        cadena = ""

        ' Selecciono bloques de a tres cifras empezando desde el final (de la cadena invertida)
        Do While posision <= Len(cifra)
            ' Selecciono una porción del numero
            bloque = Mid(cifra, posision, 3)

            ' Transformo el número a cadena
            cadena = Convertir(bloque, unidadmil) & " " & cadena.Trim

            ' Incremento la cantidad desde donde seleccionar la subcadena
            posision = posision + 3

            ' Incremento la posisión de la unidad de mil
            unidadmil = unidadmil + 1
        Loop

        ' Cargo la función
        Return IIf(decimales = "00", cadena.Trim.ToLower, cadena.Trim.ToLower & " con " & decimales & "/100")
    End Function

    ' Esta función es complemento de la función de conversión.
    ' En los arrays se agrega una posisión inicial vacía ya que VB.NET empieza de la posisión cero
    Private Function Convertir(ByVal cadena As String, ByVal unidadmil As Byte) As String
        ' Defino variables
        Dim centena, decena, unidad As Byte

        ' Invierto la subcadena (la original habia sido invertida en el procedimiento NumeroATexto)
        cadena = Invertir(cadena)

        ' Determino la longitud de la cadena
        If Len(cadena) < 3 Then
            cadena = Rellenar(cadena, 3)
        End If

        ' Verifico que la cadena no esté vacía (000)
        If cadena = "000" Then
            Return ""
        End If

        ' Desarmo el numero (empiezo del dígito cero por el manejo de cadenas de VB.NET)
        centena = CType(cadena.Substring(0, 1), Byte)
        decena = CType(cadena.Substring(1, 1), Byte)
        unidad = CType(cadena.Substring(2, 1), Byte)
        cadena = ""

        ' Calculo las centenas
        If centena <> 0 Then
            Dim centenas() As String = {"", IIf(decena = 0 And unidad = 0, "cien", "ciento"), "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"}
            cadena = centenas(centena)
        End If

        ' Calculo las decenas
        If decena <> 0 Then
            Dim decenas() As String = {"", IIf(unidad = 0, "diez", IIf(unidad >= 6, "dieci", IIf(unidad = 1, "once", IIf(unidad = 2, "doce", IIf(unidad = 3, "trece", IIf(unidad = 4, "catorce", "quince")))))), IIf(unidad = 0, "veinte", "venti"), "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"}
            cadena = cadena & " " & decenas(decena)
        End If

        ' Calculo las unidades (no pregunten por que este IF es necesario ... simplemente funciona)
        If decena = 1 And unidad < 6 Then
        Else
            Dim unidades() As String = {"", IIf(decena <> 1, IIf(unidadmil = 1, "un", "uno"), ""), "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve"}
            If decena >= 3 And unidad <> 0 Then
                cadena = cadena.Trim & " y "
            End If

            If decena = 0 Then
                cadena = cadena.Trim & " "
            End If
            cadena = cadena & unidades(unidad)
        End If

        ' Evaluo la posision de miles, millones, etc
        If unidadmil <> 0 Then
            Dim agregado() As String = {"", "mil", IIf((centena = 0) And (decena = 0) And (unidad = 1), "millón", "millones"), "mil millones", "billones", "mil billones"}
            If (centena = 0) And (decena = 0) And (unidad = 1) And unidadmil = 2 Then
                cadena = "un"
            End If
            cadena = cadena & " " & agregado(unidadmil)
        End If

        ' Cargo la función
        Return cadena.Trim
    End Function

    ' Esta función recibe una cadena de caracteres y la devuelve "invertida".
    Public Function Invertir(ByVal cadena As String) As String
        ' Defino variables
        Dim retornar As String = ""

        ' Inviero la cadena
        For posision As Short = cadena.Length To 1 Step -1
            retornar = retornar & cadena.Substring(posision - 1, 1)
        Next

        ' Retorno la cadena invertida
        Return retornar
    End Function

    ' Esta función rellena con ceros a la izquierda un número pasado como parámetro. Con el parámetro "cifras" se especifica la cantidad de dígitos a la izquierda.
    Public Function Rellenar(ByVal valor As Object, ByVal cifras As Byte) As String
        ' Defino variables
        Dim cadena As String

        ' Verifico el valor pasado
        If Not IsNumeric(valor) Then
            valor = 0
        Else
            valor = CType(valor, Integer)
        End If

        ' Cargo la cadena
        cadena = valor.ToString.Trim

        ' Relleno con los ceros que sean necesarios para llenar los dígitos pedidos
        For puntero As Byte = (Len(cadena) + 1) To cifras
            cadena = "0" & cadena
        Next puntero

        ' Cargo la función
        Return cadena
    End Function

    Private Sub btnExportarItems_Click(sender As Object, e As EventArgs) Handles btnExportarItems.Click

        Dim excel As Microsoft.Office.Interop.Excel.Application

        'Dim workbook As Microsoft.Office.Interop.Excel.Workbook
        Dim oBook As Excel.WorkbookClass
        'Dim oBooks As Excel.Workbooks

        Dim oSheet As Excel.Worksheet

        Dim paramreporte As New frmParametros
        Dim Connection As New SqlConnection(ConnStringSEI)

        Dim ds_Empresa As Data.DataSet

        Try

            excel = New Microsoft.Office.Interop.Excel.Application

            'If File.Exists(Application.StartupPath.ToString + "\Gastos\" & Util.Empresa & " Gastos - Periodo " & Periodo & ".xlsx") Then File.Delete(Application.StartupPath.ToString + "\Gastos\" & Util.Empresa & " Gastos - Periodo " & Periodo & ".xlsx")


            'oBook = excel.Workbooks.Open(Application.StartupPath.ToString + "\Gastos\SEI - Gastos.xlsx")


            'oBook.SaveAs(Application.StartupPath.ToString + "\Gastos\" & Util.Empresa & " Gastos - Periodo " & Periodo & ".xlsx")


            Dim celda As String
            oBook = excel.Workbooks.Add
            'oBooks = excel.Workbooks.Add

            oSheet = oBook.Sheets(1)

            celda = "c" + CStr(8)

            'Try
            '    connection = SqlHelper.GetConnection(ConnStringFEAFIP)
            'Catch ex As Exception
            '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try

            Try

                Dim sqlstring As String

                sqlstring = "exec [spPresupuestos_Det_Select_By_IDPresupuesto] @idpresupuesto = " & txtID.Text

                ds_Empresa = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                Dim Fila As Integer

                Fila = 0

                oSheet.Range("a2", "k500").Value = ""

                oSheet.Cells(1, 1) = "Codigo Int"
                oSheet.Cells(1, 2) = "Cod Prod Prov"
                oSheet.Cells(1, 3) = "Producto"
                oSheet.Cells(1, 4) = "Unidad"
                oSheet.Cells(1, 5) = "Cod. Mon"
                oSheet.Cells(1, 6) = "NetoU."
                oSheet.Cells(1, 7) = "NetoU. Final"
                oSheet.Cells(1, 8) = "IVA"
                oSheet.Cells(1, 9) = "Monto IVA"
                'oSheet.Cells(1, 11) = "Bonif"
                'oSheet.Cells(1, 12) = "% Rec(+) / Desc (-)"
                oSheet.Cells(1, 10) = "Cant."
                oSheet.Cells(1, 11) = "NetoTotal"
                oSheet.Cells(1, 12) = "P. Entrega"
                oSheet.Cells(1, 13) = "Proveedor"
                oSheet.Cells(1, 14) = "Marca"

                For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                    oSheet.Cells(Fila + 2, 1) = ds_Empresa.Tables(0).Rows(Fila)(2) 'fecha factura
                    oSheet.Cells(Fila + 2, 2) = ds_Empresa.Tables(0).Rows(Fila)(3) 'Proveedor
                    oSheet.Cells(Fila + 2, 3) = ds_Empresa.Tables(0).Rows(Fila)(4) 'CUIT
                    oSheet.Cells(Fila + 2, 4) = ds_Empresa.Tables(0).Rows(Fila)(7) 'Nro Comp
                    oSheet.Cells(Fila + 2, 5) = ds_Empresa.Tables(0).Rows(Fila)(12) 'Cant IVA
                    oSheet.Cells(Fila + 2, 6) = ds_Empresa.Tables(0).Rows(Fila)(14) 'SubtotalNogravado
                    oSheet.Cells(Fila + 2, 7) = ds_Empresa.Tables(0).Rows(Fila)(15) 'sUBTOTAL
                    oSheet.Cells(Fila + 2, 8) = ds_Empresa.Tables(0).Rows(Fila)(16) 'IVA10
                    oSheet.Cells(Fila + 2, 9) = ds_Empresa.Tables(0).Rows(Fila)(17) 'iva21
                    'oSheet.Cells(Fila + 2, 10) = ds_Empresa.Tables(0).Rows(Fila)(19) 'iva27
                    'oSheet.Cells(Fila + 2, 11) = ds_Empresa.Tables(0).Rows(Fila)(21) 'conceptosNoGravados
                    oSheet.Cells(Fila + 2, 10) = ds_Empresa.Tables(0).Rows(Fila)(22) 'IIBB
                    oSheet.Cells(Fila + 2, 11) = ds_Empresa.Tables(0).Rows(Fila)(23) 'IMPUESTOS INTERNOS
                    oSheet.Cells(Fila + 2, 12) = ds_Empresa.Tables(0).Rows(Fila)(27) 'Percep IIBB
                    oSheet.Cells(Fila + 2, 13) = ds_Empresa.Tables(0).Rows(Fila)(29) 'total
                    oSheet.Cells(Fila + 2, 14) = ds_Empresa.Tables(0).Rows(Fila)(31) 'total
                Next

                oSheet.Columns.AutoFit()

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
                If Not Connection Is Nothing Then
                    CType(Connection, IDisposable).Dispose()
                End If
            End Try

            excel.Visible = True

            oBook.Activate()

        Catch ex As COMException
            MessageBox.Show("Error accessing Excel: " + ex.ToString())

        Catch ex As Exception
            MessageBox.Show("Error: " + ex.ToString())

        End Try
    End Sub

    Private Sub grdItems_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles grdItems.DataError

    End Sub
End Class



