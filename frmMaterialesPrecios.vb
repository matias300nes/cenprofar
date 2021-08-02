Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet

Imports System.Threading

Public Class frmMaterialesPrecios

    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    Dim editando_celda As Boolean
    Dim llenandoCombo As Boolean = False

    Dim bolpoliticas As Boolean, band As Boolean

    Dim Consulta As String
    Dim TotalPaginas1 As Integer
    'Dim bolPaginar1 As Boolean = False
    Dim dtnew1 As New System.Data.DataTable
    Dim ini1 As Integer = 0
    Dim fin1 As Integer = 1000 - 1
    Dim quitarfiltro1 As Boolean

    Dim tran As SqlClient.SqlTransaction

    Dim conexionGenerica As SqlClient.SqlConnection = Nothing

    Dim presionoAplicar As Boolean

    Dim trd As Thread

    Dim Cell_X As Integer, Cell_Y As Integer

    Public DesdePre As Integer, FilaCodigo As Integer ', PrecioDistribuidor As Boolean
    Public CodMonedaPres As String
    Public ValorCambioDolar As Double

    Enum ColumnasDelGridItems
        Id = 0
        Codigo = 1
        Codigo_Mat_Prov = 2
        Nombre_Material = 3
        Orden = 4
        idfamilia = 5
        rubro = 6
        idsubrubro = 7
        subrubro = 8
        idunidad = 9
        codunidad = 10
        unidad = 11
        preciolista = 12
        preciolista_distribuidor = 13
        ganancia = 14
        ganancia_Distibuidor = 15
        PrecioSinIva = 16
        PrecioSinIva_Distribuidor = 17
        NvoPrecioLista = 18
        NvoPrecio = 19
        PrecioIva21 = 20
        stockactual = 21
        dateupd = 22
        valorcambio = 23
        idmatprov = 24
        iva = 25
        iva_distribuidor = 26
        precioxmt = 27
        precioxkg = 28
        Moneda = 29
        minimo = 30
        maximo = 31
        PrecioIva10 = 32
        IdProveedor = 33
        Proveedor = 34
        IdMoneda = 35
        Cod_Moneda = 36
        PlazoEntrega = 37
        IdMarca = 38
        Marca = 39
        montoIva = 40
        montoIva_Distribuidor = 41

    End Enum


#Region "   Eventos"

    Private Sub frmMaterialesPrecios_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            'Case Keys.F3 'nuevo
            '    If bolModo = True Then
            '        If MessageBox.Show("No ha guardado el Cliente Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Cliente?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '            btnNuevo_Click(sender, e)
            '        End If
            '    Else
            '        btnNuevo_Click(sender, e)
            '    End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmMateriales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Cursor = Cursors.WaitCursor

        Try
            conexionGenerica = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        band = False

        chkAumento.Checked = False

        btnGuardar.Enabled = False
        btnEliminar.Enabled = False

        configurarform()
        asignarTags()

        LlenarcmbRubros()
        LlenarcmbNombre()
        LlenarcmbProveedores()

        If DesdePre = 1 And frmPresupuestos.UltBusqueda <> "" Then
            cmbNombre.Text = frmPresupuestos.UltBusqueda
            chkNombre.Checked = True
        Else
            If DesdePre = 2 And frmConsumos.UltBusqueda <> "" Then
                cmbNombre.Text = frmConsumos.UltBusqueda
                chkNombre.Checked = True
            Else
                'If DesdePre = 3 And frmOrdenCompra.UltBusqueda <> "" Then
                '    cmbNombre.Text = frmOrdenCompra.UltBusqueda
                '    chkNombre.Checked = True
                'Else
                If DesdePre = 4 And frmAjustes.UltBusqueda <> "" Then
                    cmbNombre.Text = frmAjustes.UltBusqueda
                    chkNombre.Checked = True
                Else
                    If DesdePre = 5 And frmOrdenCompra_Abierta.UltBusqueda <> "" Then
                        cmbNombre.Text = frmOrdenCompra_Abierta.UltBusqueda
                        chkNombre.Checked = True
                    Else
                        If MDIPrincipal.UltBusquedaMat <> "" Then
                            cmbNombre.Text = MDIPrincipal.UltBusquedaMat
                            chkNombre.Checked = True
                        Else
                            chkNombre.Checked = False

                            ArmarConsulta()

                            SQL = Consulta '"exec spMateriales_Select_All"
                            LlenarGrilla1()

                        End If
                    End If
                End If
                'End If
            End If
        End If

        band = True

        btnNuevo.Enabled = False

        grd.Enabled = True

        btnImprimir.Enabled = True

        Cursor = Cursors.Default

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles cmbFAMILIAS.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbFAMILIAS_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFAMILIAS.SelectedValueChanged
        If llenandoCombo = False Then
            LlenarcmbSubRubros()
        End If
    End Sub

    'Private Sub txtCODIGO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCODIGO.TextChanged
    '    If band = True Then
    '        ArmarConsulta()

    '        SQL = Consulta '"exec spMateriales_Select_All"
    '        LlenarGrilla1()
    '    End If
    'End Sub

    Private Sub txtCODIGO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCODIGO.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If band = True Then
                ArmarConsulta()

                SQL = Consulta '"exec spMateriales_Select_All"
                LlenarGrilla1()
            End If
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbNombre_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbNombre.KeyDown
        If band = True Then
            If e.KeyValue = Keys.Enter Then
                ArmarConsulta()

                SQL = Consulta '"exec spMateriales_Select_All"
                LlenarGrilla1()

            End If
        End If
    End Sub

    Private Sub chkCodigo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCodigo.CheckedChanged
        txtCODIGO.Enabled = chkCodigo.Checked

        If chkCodigo.Checked = False Then
            txtCODIGO.Text = ""
        End If

        'If chkCodigo.Checked = False Then
        ArmarConsulta()
        SQL = Consulta '"exec spMateriales_Select_All"
        LlenarGrilla1()
        'End If

    End Sub

    Private Sub chkNombre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNombre.CheckedChanged
        'txtNOMBRE.Enabled = chkNombre.Checked
        cmbNombre.Enabled = chkNombre.Checked

        'If chkNombre.Checked = False Then
        ArmarConsulta()
        SQL = Consulta '"exec spMateriales_Select_All"
        LlenarGrilla1()
        'End If

    End Sub

    Private Sub chkRubro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRubro.CheckedChanged
        cmbFAMILIAS.Enabled = chkRubro.Checked

        ArmarConsulta()
        SQL = Consulta '"exec spMateriales_Select_All"
        LlenarGrilla1()

    End Sub

    Private Sub chkSubRubro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSubRubro.CheckedChanged
        cmbSubRubro.Enabled = chkSubRubro.Checked

        ArmarConsulta()
        SQL = Consulta '"exec spMateriales_Select_All"
        LlenarGrilla1()

    End Sub

    Private Sub cmbFAMILIAS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFAMILIAS.SelectedIndexChanged
        If band = True Then
            LlenarcmbSubRubros()

            ArmarConsulta()

            SQL = Consulta '"exec spMateriales_Select_All"
            LlenarGrilla1()
        End If
    End Sub

    Private Sub cmbSubRubro_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSubRubro.SelectedIndexChanged
        If band = True Then
            ArmarConsulta()

            SQL = Consulta '"exec spMateriales_Select_All"
            LlenarGrilla1()
        End If
    End Sub

    Private Sub frmMaterialesPrecios_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If chkNombre.Checked = True Then
            MDIPrincipal.UltBusquedaMat = cmbNombre.Text
        Else
            MDIPrincipal.UltBusquedaMat = ""
        End If

    End Sub

    Private Overloads Sub grd_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd.CellDoubleClick
        Try
            Select Case DesdePre
                Case 1

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Id).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Material_Prov).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo_Mat_Prov).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.idunidad).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.codunidad).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.unidad).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Minimo).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.maximo).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Maximo).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.minimo).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Stock).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.stockactual).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IdMoneda).Value = frmPresupuestos.txtIdMonedaPres.Text 'grd.Rows(CellY).Cells(ColumnasDelGridItems.IdMoneda).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.CodMoneda).Value = frmPresupuestos.txtCodMonedaPres.Text  'grd.Rows(CellY).Cells(ColumnasDelGridItems.Cod_Moneda).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.ValorCambio).Value = frmPresupuestos.ValorCambioPres 'grd.Rows(CellY).Cells(ColumnasDelGridItems.valorcambio).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Iva).Value = 21 'grd.Rows(CellY).Cells(ColumnasDelGridItems.iva).Value

                    If rdPrecioDist.Checked = False Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Ganancia).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia).Value
                    Else
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva_Distribuidor).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Ganancia).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia_Distibuidor).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva_Distribuidor).Value
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia_Distibuidor).Value
                    End If

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Bonificacion1).Value = FormatNumber(0, 2)
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Bonificacion2).Value = FormatNumber(0, 2)
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PorcRecDesc).Value = FormatNumber(0, 2)

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Qty).Value = 0
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.SubTotalProd).Value = 0

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.dateupd).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.dateupd).Value

                    If grd.Rows(CellY).Cells(ColumnasDelGridItems.Cod_Moneda).Value.ToUpper.ToUpper = "PE" And CodMonedaPres.ToUpper = "DO" Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value / ValorCambioDolar, "####0.00")
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value / ValorCambioDolar, "####0.00")
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value / ValorCambioDolar, "####0.00")
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value / ValorCambioDolar, "####0.00")
                    Else
                        If grd.Rows(CellY).Cells(ColumnasDelGridItems.Cod_Moneda).Value.ToUpper = "DO" And CodMonedaPres.ToUpper = "PE" Then
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value * ValorCambioDolar, "####0.00")
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value * ValorCambioDolar, "####0.00")
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value * ValorCambioDolar, "####0.00")
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value * ValorCambioDolar, "####0.00")
                        End If
                    End If

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.MontoIva).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value * (0.21), "####0.00")

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PlazoEntrega).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PlazoEntrega).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IdProveedor).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.IdProveedor).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.NombreProveedor).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Proveedor).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IdMarca).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.IdMarca).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Marca).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Marca).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.idmonedaorig).Value = frmPresupuestos.txtIdMonedaPres.Text 'grd.Rows(CellY).Cells(ColumnasDelGridItems.IdMoneda).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Material).Selected = True

                    'frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Ubicacion).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Ubicacion).Value

                    If chkNombre.Checked = True Then
                        frmPresupuestos.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmPresupuestos.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

                Case 2

                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Id).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.idunidad).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.codunidad).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.unidad).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Minimo).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.maximo).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Maximo).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.minimo).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Stock).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.stockactual).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Ganancia).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.PrecioUni).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.dateupd).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.dateupd).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.PorcRecDesc).Value = 0
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.RecDesc).Value = 0

                    If chkNombre.Checked = True Then
                        frmConsumos.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmConsumos.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

                Case 3

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdMaterial).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Id).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo).Value
                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Producto).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdUnidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.idunidad).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.codunidad).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.unidad).Value

                    'If rdPrecioDist.Checked = False Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista).Value
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    'End If

                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioProvPesos).Value = CDbl(grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista).Value) * CDbl(grd.Rows(CellY).Cells(ColumnasDelGridItems.valorcambio).Value)
                    'If grd.Rows(CellY).Cells(ColumnasDelGridItems.IdProveedor).Value Is DBNull.Value Or grd.Rows(CellY).Cells(ColumnasDelGridItems.IdProveedor).Value = "" Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdProveedor).Value = 0
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdProveedor).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.IdProveedor).Value
                    'End If
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Proveedor).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Proveedor).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdMoneda).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.IdMoneda).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_Moneda).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Cod_Moneda).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.valorcambio).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.valorcambio).Value

                    'If rdPrecioDist.Checked = False Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    'End If

                    If rdPrecioDist.Checked = False Then
                        frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Iva).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.iva).Value
                    Else
                        frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Iva).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.iva_distribuidor).Value
                    End If

                    If rdPrecioDist.Checked = False Then
                        frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.MontoIva).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.montoIva).Value
                    Else
                        frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.MontoIva).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.montoIva_Distribuidor).Value
                    End If

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif1).Value = 0
                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif2).Value = 0
                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif3).Value = 0
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif4).Value = 0
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif5).Value = 0

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cantidad).Value = 0

                    'If rdPrecioDist.Checked = False Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioFinal).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista).Value
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioFinal).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    'End If

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Subtotal).Value = 0

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.QtyStockTotal).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.stockactual).Value

                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdMaterial_Prov).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.idmatprov).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_Material_Prov).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo_Mat_Prov).Value

                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Ubicacion).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Ubicacion).Value

                    'If chkNombre.Checked = True Then
                    '    frmOrdenCompra.UltBusqueda = cmbNombre.Text
                    '    MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    'Else
                    '    frmOrdenCompra.UltBusqueda = ""
                    '    MDIPrincipal.UltBusquedaMat = ""
                    'End If

                    Me.Close()

                Case 4

                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Id).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.idunidad).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.unidad).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.QtyActual).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.stockactual).Value

                    If chkNombre.Checked = True Then
                        frmAjustes.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmAjustes.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

                Case 5

                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Id).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Codigo).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.idunidad).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.codunidad).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Unidad).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.unidad).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Minimo).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.maximo).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Maximo).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.minimo).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Stock).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.stockactual).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Ganancia).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.PrecioUni).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.dateupd).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.dateupd).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.ganancia).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.PorcRecDesc).Value = 0
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.RecDesc).Value = 0

                    If chkNombre.Checked = True Then
                        frmOrdenCompra_Abierta.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmOrdenCompra_Abierta.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Overloads Sub grd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grd.KeyPress
        If e.KeyChar = ChrW(Keys.Space) Then
            Select Case DesdePre
                Case 1
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Id).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Material_Prov).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo_Mat_Prov).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.idunidad).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.codunidad).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.unidad).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Minimo).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.maximo).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Maximo).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.minimo).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Stock).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.stockactual).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IdMoneda).Value = frmPresupuestos.txtIdMonedaPres.Text 'grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMoneda).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.CodMoneda).Value = frmPresupuestos.txtCodMonedaPres.Text  'grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Moneda).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.ValorCambio).Value = frmPresupuestos.txtValorCambioPres.Text 'grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.valorcambio).Value

                    If rdPrecioDist.Checked = False Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista).Value
                    Else
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    End If

                    If rdPrecioDist.Checked = False Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    Else
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva_Distribuidor).Value
                    End If

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Iva).Value = 21 'grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.iva).Value

                    If rdPrecioDist.Checked = False Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Ganancia).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia).Value
                    Else
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Ganancia).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia_Distibuidor).Value
                    End If

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Bonificacion1).Value = FormatNumber(0, 2)
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Bonificacion2).Value = FormatNumber(0, 2)
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PorcRecDesc).Value = FormatNumber(0, 2)

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Qty).Value = 0
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.SubTotalProd).Value = 0

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.dateupd).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.dateupd).Value

                    If rdPrecioDist.Checked = False Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    Else
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva_Distribuidor).Value
                    End If

                    If rdPrecioDist.Checked = False Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia).Value
                    Else
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia_Distibuidor).Value
                    End If

                    If grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Moneda).Value.ToUpper.ToUpper = "PE" And CodMonedaPres.ToUpper = "DO" Then
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value / ValorCambioDolar, "####0.00")
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value / ValorCambioDolar, "####0.00")
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value / ValorCambioDolar, "####0.00")
                        frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value / ValorCambioDolar, "####0.00")
                    Else
                        If grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Moneda).Value.ToUpper = "DO" And CodMonedaPres.ToUpper = "PE" Then
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioLista).Value * ValorCambioDolar, "####0.00")
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value * ValorCambioDolar, "####0.00")
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.preciovtaorig).Value * ValorCambioDolar, "####0.00")
                            frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.gananciaorig).Value * ValorCambioDolar, "####0.00")
                        End If
                    End If

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.MontoIva).Value = Format(frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PrecioVta).Value * (0.21), "####0.00")

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.PlazoEntrega).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PlazoEntrega).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IdProveedor).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdProveedor).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.NombreProveedor).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Proveedor).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.IdMarca).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMarca).Value
                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Marca).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Marca).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.idmonedaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMoneda).Value

                    frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Cod_Material).Selected = True

                    'frmPresupuestos.grdItems.Rows(FilaCodigo).Cells(frmPresupuestos.ColumnasDelGridItems.Ubicacion).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Ubicacion).Value

                    If chkNombre.Checked = True Then
                        frmPresupuestos.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmPresupuestos.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

                Case 2
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Id).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.idunidad).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.codunidad).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.unidad).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Minimo).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.maximo).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Maximo).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.minimo).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Stock).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.stockactual).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.Ganancia).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.PrecioUni).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.dateupd).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.dateupd).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia).Value
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.PorcRecDesc).Value = 0
                    frmConsumos.grdItems.Rows(FilaCodigo).Cells(frmConsumos.ColumnasDelGridItems.RecDesc).Value = 0

                    If chkNombre.Checked = True Then
                        frmConsumos.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmConsumos.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

                Case 3
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdMaterial).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Id).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdUnidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.idunidad).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.codunidad).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.unidad).Value

                    'If rdPrecioDist.Checked = False Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista).Value
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioLista).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    'End If

                    '.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioProvPesos).Value = CDbl(grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista).Value) * CDbl(grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.valorcambio).Value)
                    'If grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdProveedor).Value Is DBNull.Value Or grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdProveedor).Value = "" Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdProveedor).Value = 0
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdProveedor).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdProveedor).Value
                    'End If
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Proveedor).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Proveedor).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdMoneda).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.IdMoneda).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_Moneda).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Cod_Moneda).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.valorcambio).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.valorcambio).Value

                    'If rdPrecioDist.Checked = False Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    'End If

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif1).Value = 0
                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif2).Value = 0
                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif3).Value = 0
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif4).Value = 0
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Bonif5).Value = 0

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cantidad).Value = 0

                    'If rdPrecioDist.Checked = False Then
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioFinal).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista).Value
                    'Else
                    '    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.PrecioFinal).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    'End If

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Subtotal).Value = 0

                    frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.QtyStockTotal).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.stockactual).Value

                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.IdMaterial_Prov).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.idmatprov).Value
                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Cod_Material_Prov).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo_Mat_Prov).Value

                    'frmOrdenCompra.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra.ColumnasDelGridItems.Ubicacion).Value = grd.Rows(CellY).Cells(ColumnasDelGridItems.Ubicacion).Value

                    'If chkNombre.Checked = True Then
                    '    frmOrdenCompra.UltBusqueda = cmbNombre.Text
                    '    MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    'Else
                    '    frmOrdenCompra.UltBusqueda = ""
                    '    MDIPrincipal.UltBusquedaMat = ""
                    'End If

                    Me.Close()

                Case 4

                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Id).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.idunidad).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.unidad).Value
                    frmAjustes.grdItems.Rows(FilaCodigo).Cells(frmAjustes.ColumnasDelGridItems.QtyActual).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.stockactual).Value

                    If chkNombre.Checked = True Then
                        frmAjustes.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmAjustes.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

                Case 5
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.IDMaterial).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Id).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Cod_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Codigo).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Nombre_Material).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.Nombre_Material).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.IDUnidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.idunidad).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Cod_Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.codunidad).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Unidad).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.unidad).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Minimo).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.maximo).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Maximo).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.minimo).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Stock).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.stockactual).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.Ganancia).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.PrecioUni).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.PrecioVta).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.dateupd).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.dateupd).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.preciovtaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.PrecioSinIva).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.gananciaorig).Value = grd.Rows(Cell_Y).Cells(ColumnasDelGridItems.ganancia).Value
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.PorcRecDesc).Value = 0
                    frmOrdenCompra_Abierta.grdItems.Rows(FilaCodigo).Cells(frmOrdenCompra_Abierta.ColumnasDelGridItems.RecDesc).Value = 0

                    If chkNombre.Checked = True Then
                        frmConsumos.UltBusqueda = cmbNombre.Text
                        MDIPrincipal.UltBusquedaMat = cmbNombre.Text
                    Else
                        frmConsumos.UltBusqueda = ""
                        MDIPrincipal.UltBusquedaMat = ""
                    End If

                    Me.Close()

            End Select

        End If
    End Sub

    Private Overloads Sub FiltrarporToolStrip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FiltrarporToolStrip.KeyDown
        'ConfigurarGrilla()
        If e.KeyCode = Keys.Enter Then
            CheckForIllegalCrossThreadCalls = False

            trd = New Thread(AddressOf ConfigurarGrilla)
            trd.IsBackground = True
            trd.Start()
        End If

    End Sub

    Protected Overloads Sub QuitarElFitroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitarElFitroToolStripMenuItem.Click
        'ConfigurarGrilla()

        CheckForIllegalCrossThreadCalls = False

        trd = New Thread(AddressOf ConfigurarGrilla)
        trd.IsBackground = True
        trd.Start()
    End Sub


    Private Overloads Sub grd_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd.CellMouseUp
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

    Private Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged
        Try
            'If bandCellY = True Then
            Cell_Y = grd.CurrentRow.Index
            'End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub chkOcultarGanancia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOcultarGanancia.CheckedChanged
        grd.Columns(ColumnasDelGridItems.ganancia).Visible = chkOcultarGanancia.Checked

        If chkOcultarGanancia.Checked = True Then
            chkOcultarGanancia.Text = "Ocultar GC"
        Else
            chkOcultarGanancia.Text = "Mostrar GC"
        End If

    End Sub

    Private Sub txtAuemntoKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAumento.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbNombre_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNombre.SelectedValueChanged
        If band = True Then
            ArmarConsulta()

            SQL = Consulta '"exec spMateriales_Select_All"
            LlenarGrilla1()

            If chkAumento.Checked = True Then
                chkAumento_CheckedChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub chkAumento_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAumento.CheckedChanged

        presionoAplicar = False

        chkBonif1.Enabled = chkAumento.Checked
        chkBonif2.Enabled = chkAumento.Checked
        chkBonif3.Enabled = chkAumento.Checked
        chkBonif4.Enabled = chkAumento.Checked
        chkBonif5.Enabled = chkAumento.Checked
        chkGanancia.Enabled = chkAumento.Checked
        chkPrecioLista.Enabled = chkAumento.Checked

        btnGuardar.Enabled = chkAumento.Checked

        lblAyudaBonif.Visible = chkAumento.Checked
        lblAyudaPrecioLista.Visible = chkAumento.Checked

        grd.Columns(ColumnasDelGridItems.NvoPrecio).Visible = chkAumento.Checked
        grd.Columns(ColumnasDelGridItems.NvoPrecio).DefaultCellStyle.ForeColor = Color.Red
        grd.Columns(ColumnasDelGridItems.NvoPrecioLista).Visible = chkAumento.Checked
        grd.Columns(ColumnasDelGridItems.NvoPrecioLista).DefaultCellStyle.ForeColor = Color.Red
        'grd.Columns(ColumnasDelGridItems.NvoPrecioLista).Visible = chkAumento.Checked

        If chkAumento.Checked = True Then
            chkBonif1.Checked = False
            chkBonif2.Checked = False
            'chkBonif3.Checked = False
            'chkBonif4.Checked = False
            'chkBonif5.Checked = False
            chkGanancia.Checked = False
            chkPrecioLista.Checked = False

            txtBonif1.ReadOnly = True
            txtBonif2.ReadOnly = True
            'txtBonif3.ReadOnly = True
            'txtBonif4.ReadOnly = True
            'txtBonif5.ReadOnly = True
            txtGanancia.ReadOnly = True
            txtAumento.ReadOnly = True

            Util.MsgStatus(Status1, "Verificando Proveedores...", My.Resources.Resources.indicator_white)
            ArmarConsulta_Proveedor()
            Util.MsgStatus(Status1, "Verificando Bonificacion 1...", My.Resources.Resources.indicator_white)
            ArmarConsulta_Bonif1()
            Util.MsgStatus(Status1, "Verificando Bonificacion 2...", My.Resources.Resources.indicator_white)
            ArmarConsulta_Bonif2()
            'Util.MsgStatus(Status1, "Verificando Bonificacion 3...", My.Resources.Resources.indicator_white)
            'ArmarConsulta_Bonif3()
            'Util.MsgStatus(Status1, "Verificando Bonificacion 4...", My.Resources.Resources.indicator_white)
            'ArmarConsulta_Bonif4()
            'Util.MsgStatus(Status1, "Verificando Bonificacion 5...", My.Resources.Resources.indicator_white)
            'ArmarConsulta_Bonif5()
            Util.MsgStatus(Status1, "Verificando Ganancias...", My.Resources.Resources.indicator_white)
            ArmarConsulta_Ganancia()

            Util.MsgStatus(Status1, "Control de Proveedores, Bonificación y Ganancia finalizado...", My.Resources.Resources.Info)

        Else
            lblMsjBonif1.Visible = False
            lblMsjBonif2.Visible = False
            'lblMsjBonif3.Visible = False
            'lblMsjBonif4.Visible = False
            'lblMsjBonif5.Visible = False
            lblMsjGanancia.Visible = False
            lblMsjProv.Visible = False

            txtBonif1.Text = ""
            txtBonif2.Text = ""
            'txtBonif3.Text = ""
            'txtBonif4.Text = ""
            'txtBonif5.Text = ""
            txtGanancia.Text = ""
            txtAumento.Text = ""

        End If

        btnAplicarAumento.Enabled = chkAumento.Checked
        btnCancelarAumento.Enabled = chkAumento.Checked


    End Sub

    Private Sub chkBonif1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBonif1.CheckedChanged
        txtBonif1.ReadOnly = Not chkBonif1.Checked
    End Sub

    Private Sub chkBonif2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBonif2.CheckedChanged
        txtBonif2.ReadOnly = Not chkBonif2.Checked
    End Sub

    'Private Sub chkBonif3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBonif3.CheckedChanged
    '    txtBonif3.ReadOnly = Not chkBonif3.Checked
    'End Sub

    'Private Sub chkBonif4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBonif4.CheckedChanged
    '    txtBonif4.ReadOnly = Not chkBonif4.Checked
    'End Sub

    'Private Sub chkBonif5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBonif5.CheckedChanged
    '    txtBonif5.ReadOnly = Not chkBonif5.Checked
    'End Sub

    Private Sub chkGanancia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGanancia.CheckedChanged
        txtGanancia.ReadOnly = Not chkGanancia.Checked
    End Sub

    Private Sub chkPrecioLista_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrecioLista.CheckedChanged
        txtAumento.ReadOnly = Not chkPrecioLista.Checked

        If chkPrecioLista.Checked = False Then
            txtAumento.Text = "0"
        End If

    End Sub

    Private Sub cmbProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProveedor.SelectedIndexChanged
        If band = True Then
            ArmarConsulta()

            SQL = Consulta '"exec spMateriales_Select_All"
            LlenarGrilla1()
        End If
    End Sub

    Private Sub chkProveedor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProveedor.CheckedChanged
        cmbProveedor.Enabled = chkProveedor.Checked

        ArmarConsulta()
        SQL = Consulta '"exec spMateriales_Select_All"
        LlenarGrilla1()
    End Sub

    Private Sub PicAumento_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picAumento.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.picAumento, "Para aumentar los precios, debe habilitar la opción correspondiente. Luego de esto en la grilla" & vbCrLf & _
                     " aparecerá una nueva colmuna que le irá mostrando como quedarán los precios según los incrementos que haya elegido." & vbCrLf & _
                     " Para finalizar, haga clic en el botón Guardar")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Private Sub PictureBox1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PicOrden.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.PicOrden, "Para modificar el orden debe activar esta opción. Ingrese el orden en la columna Orden de la planilla, y haga clic en Guardar.")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Private Sub PicAplicarAumento_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PicAplicarAumento.MouseHover
        Dim a As New ToolTip
        a.SetToolTip(Me.PicAplicarAumento, "Luego de Aplicar el aumento, para confirmar la operación, debe hacer clic en el botón Guardar")
        a.IsBalloon = True
        a.UseAnimation = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkModificarOrden.CheckedChanged
        grd.Columns(ColumnasDelGridItems.Orden).ReadOnly = Not chkModificarOrden.Checked
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Lista de Precios"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            'Me.Top = ARRIBA
            'Me.Left = IZQUIERDA
        Else
            'Me.Top = 0
            'Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2

            Me.WindowState = FormWindowState.Maximized

            Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

            'Dim p As New Size(GroupBox1.Size.Width, 500)
            'Me.grd.Size = New Size(p)
        End If

        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub asignarTags()
        txtCODIGO.Tag = "1"
        cmbNombre.Tag = "2"
        cmbFAMILIAS.Tag = "4"
        cmbSubRubro.Tag = "6"
        'cmbNivel1.Tag = "17"
        'cmbNivel2.Tag = "18"
    End Sub

    Private Sub LlenarcmbRubros()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    llenandoCombo = False
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Id, (codigo + ' - ' +  rtrim(nombre)) as Nombre FROM Familias WHERE Eliminado = 0 ORDER BY nombre")
            ds_Rubros.Dispose()

            With cmbFAMILIAS
                .DataSource = ds_Rubros.Tables(0).DefaultView
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
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbSubRubros()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_SubRubros As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds_SubRubros = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT id, (codigo + ' - ' + rtrim(nombre)) as Nombre FROM subrubros WHERE idfamilia = " & CType(cmbFAMILIAS.SelectedValue, Long) & " and Eliminado = 0 ORDER BY nombre")

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub LlenarcmbNombre()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT Distinct Busqueda FROM CriteriosdeBusquedas ORDER BY BUSQUEDA")
            ds.Dispose()

            With cmbNombre
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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub LlenarcmbProveedores()
        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, "SELECT DISTINCT P.ID, NOMBRE FROM Materiales_Proveedor mp JOIN Proveedores p ON p.ID = mp.IdProveedor ORDER BY NOMBRE")
            ds.Dispose()

            With cmbProveedor
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
        Finally
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try

    End Sub

    Private Sub ArmarConsulta()
        Consulta = " SELECT DISTINCT " & _
                " M.id							AS 'ID', " & _
                " M.codigo						AS 'Código', " & _
                " MP.codigo_mat_prov			AS 'Cód Prod Prov', " & _
                " M.nombre						AS 'Nombre', " & _
                " ISNULL(Ordenpormedidas,0)     AS 'Orden', " & _
                " a.id                          AS IdFAMILIA, " & _
                " a.codigo + ' - ' + A.nombre	AS 'Rubro'," & _
                " sb.id                         AS IdSubrubro, " & _
                " sb.codigo + ' - ' + sb.Nombre	AS 'SubRubro'," & _
                " m.idunidad, " & _
                " B.codigo						AS 'Cod Unidad'," & _
                " B.Nombre		                AS 'Unidad'," & _
                " ISNULL(preciolista,0.00)                      AS 'Prec. Lista Tab', " & _
                " ISNULL(preciolista_distribuidor,0.00)         AS 'Prec. Lista Dis', " & _
                " ISNULL(Mp.Ganancia,0.00)     	AS 'Ganancia Tab'," & _
                " ISNULL(Mp.Ganancia_Distribuidor, 0)     	AS 'Ganancia Dis'," & _
                " PrecioventasinIVA             AS 'Precio s/IVA Venta Tab'," & _
                " PrecioventasinIVA_Distribuidor AS 'Precio s/IVA Venta Dist'," & _
                " 0.00                          AS 'Nvo Precio Lista Prov', " & _
                " 0.00                          AS 'Nvo Precio s/IVA Venta', " & _
                " CAST((PrecioventasinIVA * 1.21) AS DECIMAL(18,2))	AS 'Precio IVA 21% Venta'," & _
                " ISNULL(S.Qty,0)				AS 'Stock Actual'," & _
                " CONVERT(VARCHAR(10), ISNULL(m.DATEUPD,m.[DATEADD]),103) AS 'Fecha Act.', " & _
                " valorcambio                   AS 'Cambio', " & _
                " ISNULL(mp.id,0)               as IdMaterialProveedor, " & _
                " ISNULL(MP.IVA,0)              AS 'IVA Tab', " & _
                " ISNULL(MP.IVA_Distribuidor,0) AS 'IVA Dis', " & _
                " MP.PrecioxMT                  AS 'Precio x Mt', " & _
                " MP.PrecioxKG                  AS 'Precio x Kg', " & _
                " mo.nombre + '-' +  convert(varchar(10), valorcambio)   AS 'Moneda Orig'," & _
                " M.Minimo						AS 'Mínimo'," & _
                " M.Maximo						AS 'Máximo'," & _
                " CAST((PrecioventasinIVA * 1.105) AS DECIMAL(18,2))	AS 'Precio IVA 10,5% Venta', " & _
                " ISNULL(p.Id,0)				AS 'IdProveedor'," & _
                " ISNULL(p.Nombre,'')			AS 'Proveedor', " & _
                " mo.id	            			AS 'IdMoneda', " & _
                " mo.codigo						AS 'Cod_Moneda', " & _
                " mp.PlazoEntrega				AS 'PlazoEntrega', " & _
                " ISNULL(mc.Id,0)  				AS 'IdMarca', " & _
                " ISNULL(mc.Nombre,'')          AS 'Marca', " & _
                " CONVERT(DECIMAL(18,2), ISNULL(MP.montoIva,0))       AS 'Montoiva Tab', " & _
                " CONVERT(DECIMAL(18,2), ISNULL(MP.montoIva_Distribuidor,0))       AS 'Montoiva Dis'," & _
                " Pasillo + ' - ' + Estante + ' - ' + Fila	AS Ubicacion" & _
                " FROM Materiales M" & _
                "	JOIN Unidades B ON B.id = M.IDUnidad" & _
                "   JOIN Monedas mo ON mo.Id = M.IdMoneda " & _
                "	LEFT JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
                "	LEFT JOIN Familias A ON A.id = sb.IDFamilia" & _
                "   LEFT JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
                "   LEFT JOIN Proveedores p ON p.Id = mp.idproveedor " & _
                "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
                "	LEFT JOIN Marcas mc ON Mc.id = mp.idmarca" & _
                " WHERE M.eliminado = 0 "

        '" isnull(cast((PrecioventasinIVA * valorcambio) as DECIMAL(18,2)),0)	AS 'Precio s/IVA Venta($)'," & _
        '" CAST((PrecioventasinIVA * 1.21 * valorcambio) AS DECIMAL(18,2))	AS 'Precio IVA 21% Venta($)'," & _

        '" CAST((PrecioventasinIVA * 1.105 * valorcambio) AS DECIMAL(18,2))	AS 'Precio IVA 10,5% Venta($)', " & _

        If chkCodigo.Checked = True Then
            Consulta = Consulta & " and m.codigo like '" & txtCODIGO.Text & "%'"
        End If

        If chkNombre.Checked = True Then
            Dim posicion As Integer, posicion1 As Integer
            'posicion = InStr(txtNOMBRE.Text, "?")
            posicion = InStr(cmbNombre.Text, "?")

            If posicion > 0 Then
                Dim parametro1 As String
                Dim parametro2 As String


                parametro1 = Mid(cmbNombre.Text, IIf(posicion = 0, 1, posicion + 1), cmbNombre.Text.Length)

                If parametro1.Length > 0 Then
                    parametro2 = Mid(cmbNombre.Text, 1, IIf(posicion = 1, posicion, posicion - 1))

                    posicion1 = InStr(parametro1, "?")

                    If posicion1 > 0 Then
                        Dim parametro3 As String

                        parametro3 = Mid(parametro1, IIf(posicion1 = 0, 1, posicion1 + 1), posicion1 - 1)

                        parametro1 = Mid(parametro1, 1, posicion1 - 1)

                        Consulta = Consulta & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' and m.nombre like '%" & parametro3 & "%'"

                    Else
                        Consulta = Consulta & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' "
                    End If
                Else
                    Consulta = Consulta & " and m.nombre like '%" & cmbNombre.Text & "%'"
                End If

            Else
                If cmbNombre.Text <> "" Then
                    Consulta = Consulta & " and m.nombre like '%" & cmbNombre.Text & "%'"
                End If
            End If

        End If


        If chkProveedor.Checked = True Then
            Consulta = Consulta & " and mp.idproveedor = " & cmbProveedor.SelectedValue
        End If

        If chkRubro.Checked = True Then
            Consulta = Consulta & " and a.id = " & cmbFAMILIAS.SelectedValue
        End If

        Try
            If chkSubRubro.Checked = True And cmbSubRubro.Text <> "" Then
                'If cmbSubRubro.Text <> "" Then
                Consulta = Consulta & " and sb.id = " & cmbSubRubro.SelectedValue
            End If
        Catch ex As Exception
            'Consulta = Consulta & " ORDER BY ISNULL(Ordenpormedidas, 0) ASC"
            Consulta = Consulta & " ORDER BY m.Codigo ASC"
        End Try

        'Consulta = Consulta & " ORDER BY ISNULL(Ordenpormedidas, 0) ASC"
        Consulta = Consulta & " ORDER BY m.Codigo ASC"

    End Sub

    Private Sub ArmarConsulta_Proveedor()

        Dim consulta_prov As String

        consulta_prov = " SELECT top 1 " & _
                " mp.IdMaterial, count(mp.Idproveedor) as CantProveedor " & _
                " FROM Materiales M" & _
                "	JOIN Unidades B ON B.id = M.IDUnidad" & _
                "	JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
                "	JOIN Familias A ON A.id = sb.IDFamilia" & _
                "   JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
                "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
                "       WHERE M.eliminado = 0 "


        If chkCodigo.Checked = True Then
            consulta_prov = consulta_prov & " and m.codigo like '" & txtCODIGO.Text & "%'"
        End If

        If chkNombre.Checked = True Then
            Dim posicion As Integer, posicion1 As Integer
            'posicion = InStr(txtNOMBRE.Text, "?")
            posicion = InStr(cmbNombre.Text, "?")

            If posicion > 0 Then
                Dim parametro1 As String
                Dim parametro2 As String


                parametro1 = Mid(cmbNombre.Text, IIf(posicion = 0, 1, posicion + 1), cmbNombre.Text.Length)

                If parametro1.Length > 0 Then
                    parametro2 = Mid(cmbNombre.Text, 1, IIf(posicion = 1, posicion, posicion - 1))

                    posicion1 = InStr(parametro1, "?")

                    If posicion1 > 0 Then
                        Dim parametro3 As String

                        parametro3 = Mid(parametro1, IIf(posicion1 = 0, 1, posicion1 + 1), posicion1 - 1)

                        parametro1 = Mid(parametro1, 1, posicion1 - 1)

                        consulta_prov = consulta_prov & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' and m.nombre like '%" & parametro3 & "%'"

                    Else
                        consulta_prov = consulta_prov & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' "
                    End If
                Else
                    consulta_prov = consulta_prov & " and m.nombre like '%" & cmbNombre.Text & "%'"
                End If

            Else
                consulta_prov = consulta_prov & " and m.nombre like '%" & cmbNombre.Text & "%'"
            End If


        End If

        If chkProveedor.Checked = True Then
            consulta_prov = consulta_prov & " and mp.idproveedor = " & cmbProveedor.SelectedValue
        End If

        If chkRubro.Checked = True Then
            consulta_prov = consulta_prov & " and a.id = " & cmbFAMILIAS.SelectedValue
        End If

        Try
            If chkSubRubro.Checked = True And cmbSubRubro.Text <> "" Then
                consulta_prov = consulta_prov & " and sb.id = " & cmbSubRubro.SelectedValue
            End If
        Catch ex As Exception

        End Try

        consulta_prov = consulta_prov & " GROUP BY mp.idmaterial ORDER BY  count(mp.Idproveedor) DESC"

        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_proveedores As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            ds_proveedores = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, consulta_prov)

            ds_proveedores.Dispose()

            If ds_proveedores.Tables(0).Rows(0).Item(1).ToString <> "1" Then
                lblMsjProv.Visible = True
            Else
                lblMsjProv.Visible = False
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
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try


    End Sub

    Private Sub ArmarConsulta_Bonif1()

        Dim consulta_bonif As String

        'consulta_bonif = " SELECT top 1 " & _
        '        " mp.Bonificacion1,	count(mp.IdMaterial) as CantMaterial " & _
        '        " FROM Materiales M" & _
        '        "	JOIN Unidades B ON B.id = M.IDUnidad" & _
        '        "	JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
        '        "	JOIN Familias A ON A.id = sb.IDFamilia" & _
        '        "   LEFT JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
        '        "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
        '        "       WHERE M.eliminado = 0 "

        consulta_bonif = " SELECT top 1 " & _
                " ISNULL(mp.Bonificacion1, 0),	count(*) as CantMaterial " & _
                " FROM Materiales M" & _
                "	JOIN Unidades B ON B.id = M.IDUnidad" & _
                "	JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
                "	JOIN Familias A ON A.id = sb.IDFamilia" & _
                "   LEFT JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
                "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
                "       WHERE M.eliminado = 0 "


        If chkCodigo.Checked = True Then
            consulta_bonif = consulta_bonif & " and m.codigo like '" & txtCODIGO.Text & "%'"
        End If

        If chkNombre.Checked = True Then
            Dim posicion As Integer, posicion1 As Integer
            'posicion = InStr(txtNOMBRE.Text, "?")
            posicion = InStr(cmbNombre.Text, "?")

            If posicion > 0 Then
                Dim parametro1 As String
                Dim parametro2 As String


                parametro1 = Mid(cmbNombre.Text, IIf(posicion = 0, 1, posicion + 1), cmbNombre.Text.Length)

                If parametro1.Length > 0 Then
                    parametro2 = Mid(cmbNombre.Text, 1, IIf(posicion = 1, posicion, posicion - 1))

                    posicion1 = InStr(parametro1, "?")

                    If posicion1 > 0 Then
                        Dim parametro3 As String

                        parametro3 = Mid(parametro1, IIf(posicion1 = 0, 1, posicion1 + 1), posicion1 - 1)

                        parametro1 = Mid(parametro1, 1, posicion1 - 1)

                        consulta_bonif = consulta_bonif & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' and m.nombre like '%" & parametro3 & "%'"

                    Else
                        consulta_bonif = consulta_bonif & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' "
                    End If
                Else
                    consulta_bonif = consulta_bonif & " and m.nombre like '%" & cmbNombre.Text & "%'"
                End If

            Else
                consulta_bonif = consulta_bonif & " and m.nombre like '%" & cmbNombre.Text & "%'"
            End If

        End If

        If chkProveedor.Checked = True Then
            consulta_bonif = consulta_bonif & " and mp.idproveedor = " & cmbProveedor.SelectedValue
        End If

        If chkRubro.Checked = True Then
            consulta_bonif = consulta_bonif & " and a.id = " & cmbFAMILIAS.SelectedValue
        End If

        Try
            If chkSubRubro.Checked = True And cmbSubRubro.Text <> "" Then
                consulta_bonif = consulta_bonif & " and sb.id = " & cmbSubRubro.SelectedValue
            End If
        Catch ex As Exception

        End Try

        'consulta_bonif = consulta_bonif & " GROUP BY mp.Bonificacion1  ORDER BY  count(mp.Bonificacion1  )DESC"
        consulta_bonif = consulta_bonif & " GROUP BY ISNULL(mp.Bonificacion1,0)  ORDER BY  count(ISNULL(mp.Bonificacion1,0)  )DESC"

        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_bonif1 As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            ds_bonif1 = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, consulta_bonif)

            ds_bonif1.Dispose()

            If CInt(ds_bonif1.Tables(0).Rows(0).Item(1).ToString) <> grd.RowCount Then
                lblMsjBonif1.Visible = True
            Else
                lblMsjBonif1.Visible = False
                txtBonif1.Text = ds_bonif1.Tables(0).Rows(0).Item(0).ToString
            End If

            If txtBonif1.Text <> "" Then
                If CInt(txtBonif1.Text) <> 0 Then
                    chkBonif1.Checked = True
                Else
                    chkBonif1.Checked = False
                End If
            Else
                chkBonif1.Checked = False
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
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try


    End Sub

    Private Sub ArmarConsulta_Bonif2()

        Dim consulta_bonif As String

        'consulta_bonif = " SELECT top 1 " & _
        '        " mp.Bonificacion2,	count(mp.IdMaterial) as CantMaterial " & _
        '        " FROM Materiales M" & _
        '        "	JOIN Unidades B ON B.id = M.IDUnidad" & _
        '        "	JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
        '        "	JOIN Familias A ON A.id = sb.IDFamilia" & _
        '        "   LEFT JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
        '        "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
        '        "       WHERE M.eliminado = 0 "

        consulta_bonif = " SELECT top 1 " & _
               " ISNULL(mp.Bonificacion2, 0),	count(*) as CantMaterial " & _
               " FROM Materiales M" & _
               "	JOIN Unidades B ON B.id = M.IDUnidad" & _
               "	JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
               "	JOIN Familias A ON A.id = sb.IDFamilia" & _
               "   LEFT JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
               "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
               "       WHERE M.eliminado = 0 "



        If chkCodigo.Checked = True Then
            consulta_bonif = consulta_bonif & " and m.codigo like '" & txtCODIGO.Text & "%'"
        End If

        If chkNombre.Checked = True Then
            Dim posicion As Integer, posicion1 As Integer
            'posicion = InStr(txtNOMBRE.Text, "?")
            posicion = InStr(cmbNombre.Text, "?")

            If posicion > 0 Then
                Dim parametro1 As String
                Dim parametro2 As String


                parametro1 = Mid(cmbNombre.Text, IIf(posicion = 0, 1, posicion + 1), cmbNombre.Text.Length)

                If parametro1.Length > 0 Then
                    parametro2 = Mid(cmbNombre.Text, 1, IIf(posicion = 1, posicion, posicion - 1))

                    posicion1 = InStr(parametro1, "?")

                    If posicion1 > 0 Then
                        Dim parametro3 As String

                        parametro3 = Mid(parametro1, IIf(posicion1 = 0, 1, posicion1 + 1), posicion1 - 1)

                        parametro1 = Mid(parametro1, 1, posicion1 - 1)

                        consulta_bonif = consulta_bonif & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' and m.nombre like '%" & parametro3 & "%'"

                    Else
                        consulta_bonif = consulta_bonif & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' "
                    End If
                Else
                    consulta_bonif = consulta_bonif & " and m.nombre like '%" & cmbNombre.Text & "%'"
                End If

            Else
                consulta_bonif = consulta_bonif & " and m.nombre like '%" & cmbNombre.Text & "%'"
            End If


        End If

        If chkProveedor.Checked = True Then
            consulta_bonif = consulta_bonif & " and mp.idproveedor = " & cmbProveedor.SelectedValue
        End If

        If chkRubro.Checked = True Then
            consulta_bonif = consulta_bonif & " and a.id = " & cmbFAMILIAS.SelectedValue
        End If

        Try
            If chkSubRubro.Checked = True And cmbSubRubro.Text <> "" Then
                consulta_bonif = consulta_bonif & " and sb.id = " & cmbSubRubro.SelectedValue
            End If
        Catch ex As Exception

        End Try

        consulta_bonif = consulta_bonif & " GROUP BY ISNULL(mp.Bonificacion2,0)  ORDER BY  count(ISNULL(mp.Bonificacion2,0))DESC"

        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_bonif As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            ds_bonif = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, consulta_bonif)

            ds_bonif.Dispose()

            If CInt(ds_bonif.Tables(0).Rows(0).Item(1).ToString) <> grd.RowCount Then
                lblMsjBonif2.Visible = True
            Else
                lblMsjBonif2.Visible = False
                txtBonif2.Text = ds_bonif.Tables(0).Rows(0).Item(0).ToString
            End If

            If txtBonif2.Text <> "" Then
                If CInt(txtBonif2.Text) <> 0 Then
                    chkBonif2.Checked = True
                Else
                    chkBonif2.Checked = False
                End If
            Else
                chkBonif2.Checked = False
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
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try


    End Sub

    Private Sub ArmarConsulta_Ganancia()

        Dim consulta_ganancia As String

        consulta_ganancia = " SELECT top 1 " & _
                " mp.ganancia, count(mp.IdMaterial) as cantmaterial " & _
                " FROM Materiales M" & _
                "	JOIN Unidades B ON B.id = M.IDUnidad" & _
                "	JOIN SubRubros sb ON sb.ID  = M.IDsubrubro" & _
                "	JOIN Familias A ON A.id = sb.IDFamilia" & _
                "   JOIN Materiales_Proveedor mp ON mp.IdMaterial = M.ID " & _
                "	LEFT JOIN dbo.fn_Stock_Material_Unidad() S ON M.id = s.idmaterial AND M.idunidad = s.idunidad" & _
                "       WHERE M.eliminado = 0 "


        If chkCodigo.Checked = True Then
            consulta_ganancia = consulta_ganancia & " and m.codigo like '" & txtCODIGO.Text & "%'"
        End If

        If chkNombre.Checked = True Then
            Dim posicion As Integer, posicion1 As Integer
            'posicion = InStr(txtNOMBRE.Text, "?")
            posicion = InStr(cmbNombre.Text, "?")

            If posicion > 0 Then
                Dim parametro1 As String
                Dim parametro2 As String


                parametro1 = Mid(cmbNombre.Text, IIf(posicion = 0, 1, posicion + 1), cmbNombre.Text.Length)

                If parametro1.Length > 0 Then
                    parametro2 = Mid(cmbNombre.Text, 1, IIf(posicion = 1, posicion, posicion - 1))

                    posicion1 = InStr(parametro1, "?")

                    If posicion1 > 0 Then
                        Dim parametro3 As String

                        parametro3 = Mid(parametro1, IIf(posicion1 = 0, 1, posicion1 + 1), posicion1 - 1)

                        parametro1 = Mid(parametro1, 1, posicion1 - 1)

                        consulta_ganancia = consulta_ganancia & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' and m.nombre like '%" & parametro3 & "%'"

                    Else
                        consulta_ganancia = consulta_ganancia & " and m.nombre like '%" & parametro2 & "%' and m.nombre like '%" & parametro1 & "%' "
                    End If
                Else
                    consulta_ganancia = consulta_ganancia & " and m.nombre like '%" & cmbNombre.Text & "%'"
                End If

            Else
                consulta_ganancia = consulta_ganancia & " and m.nombre like '%" & cmbNombre.Text & "%'"
            End If


        End If

        If chkProveedor.Checked = True Then
            consulta_ganancia = consulta_ganancia & " and mp.idproveedor = " & cmbProveedor.SelectedValue
        End If

        If chkRubro.Checked = True Then
            consulta_ganancia = consulta_ganancia & " and a.id = " & cmbFAMILIAS.SelectedValue
        End If

        Try
            If chkSubRubro.Checked = True And cmbSubRubro.Text <> "" Then
                consulta_ganancia = consulta_ganancia & " and sb.id = " & cmbSubRubro.SelectedValue
            End If
        Catch ex As Exception

        End Try

        consulta_ganancia = consulta_ganancia & " GROUP BY mp.ganancia  ORDER BY  count(mp.ganancia  )DESC"

        'Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_ganancia As Data.DataSet

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try
            ds_ganancia = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, consulta_ganancia)

            ds_ganancia.Dispose()

            If CInt(ds_ganancia.Tables(0).Rows(0).Item(1).ToString) <> grd.RowCount Then
                lblMsjGanancia.Visible = True
            Else
                lblMsjGanancia.Visible = False
                txtGanancia.Text = ds_ganancia.Tables(0).Rows(0).Item(0).ToString
            End If


            If txtGanancia.Text <> "" Then
                If CInt(txtGanancia.Text) <> 0 Then
                    chkGanancia.Checked = True
                Else
                    chkGanancia.Checked = False
                End If
            Else
                chkGanancia.Checked = False
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
            '    CType(connection, IDisposable).Dispose()
            'End If
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

    Private Sub Verificar_Datos()
        bolpoliticas = False

        Dim i As Integer

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si se terminó de editar la celda...
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If editando_celda Then
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edición, antes de guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        If chkModificarOrden.Checked = False Then
            For i = 0 To grd.RowCount - 1
                If fila_vacia(i) Then
                    Try
                        grd.Rows(i).Cells(ColumnasDelGridItems.PrecioSinIva).Value = 0
                    Catch ex As Exception
                    End Try
                End If
            Next i
        Else
            For i = 0 To grd.RowCount - 1
                If fila_vacia(i) Then
                    Try
                        grd.Rows(i).Cells(ColumnasDelGridItems.Orden).Value = 0
                    Catch ex As Exception
                    End Try
                End If
            Next i
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'verificar si existe al menos un cambio en la grilla

        If chkModificarOrden.Checked = True Then

            Dim todosencero As Boolean = False

            For i = 0 To grd.RowCount - 1
                If grd.Rows(i).Cells(ColumnasDelGridItems.Orden).Value <> 0 Then
                    todosencero = True
                    Exit For
                End If
            Next

            If todosencero = False Then
                Util.MsgStatus(Status1, "No se guardará ningún registro porque todos los valores de la columna Orden, están en Cero.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "No se guardará ningún registro porque todos los valores de la columna Orden, están en Cero.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' controlar si al menos hay 1 fila
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If grd.RowCount > 0 Then
            bolpoliticas = True
        Else
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "No hay filas de materiales para guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        bolpoliticas = True

    End Sub

    Private Function fila_vacia(ByVal i) As Boolean
        Try
            If chkModificarOrden.Checked = False Then
                If grd.Rows(i).Cells(ColumnasDelGridItems.PrecioSinIva).Value Is System.DBNull.Value Then
                    fila_vacia = True
                Else
                    fila_vacia = False
                End If
            Else
                If grd.Rows(i).Cells(ColumnasDelGridItems.Orden).Value Is System.DBNull.Value Then
                    fila_vacia = True
                Else
                    fila_vacia = False
                End If
            End If

        Catch ex As Exception
            fila_vacia = True
        End Try

    End Function


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
            Dim i As Integer

            Dim bonif1 As Double, bonif2 As Double, bonif3 As Double = 0, bonif4 As Double = 0, bonif5 As Double = 0, ganancia As Double

            bonif1 = IIf(txtBonif1.Text = "0", 1, FormatNumber(CDbl(txtBonif1.Text), 2))
            bonif2 = IIf(txtBonif2.Text = "0", 1, FormatNumber(CDbl(txtBonif2.Text), 2))
            'bonif3 = IIf(txtBonif3.Text = "0", 1, FormatNumber(1 - (CDbl(txtBonif3.Text) / 100), 2))
            'bonif4 = IIf(txtBonif4.Text = "0", 1, FormatNumber(1 - (CDbl(txtBonif4.Text) / 100), 2))
            'bonif5 = IIf(txtBonif5.Text = "0", 1, FormatNumber(1 - (CDbl(txtBonif5.Text) / 100), 2))
            ganancia = IIf(txtGanancia.Text = "0", 1, FormatNumber(CDbl(txtGanancia.Text), 2))

            For i = 0 To grd.RowCount - 1
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = grd.Rows(i).Cells(ColumnasDelGridItems.idmatprov).Value
                param_id.Direction = ParameterDirection.Input

                Dim param_tablerista As New SqlClient.SqlParameter
                param_tablerista.ParameterName = "@tablerista"
                param_tablerista.SqlDbType = SqlDbType.Bit
                param_tablerista.Value = rdPrecioTabl.Checked
                param_tablerista.Direction = ParameterDirection.Input

                Dim param_idmaterial As New SqlClient.SqlParameter
                param_idmaterial.ParameterName = "@idMaterial"
                param_idmaterial.SqlDbType = SqlDbType.BigInt
                param_idmaterial.Value = grd.Rows(i).Cells(ColumnasDelGridItems.Id).Value
                param_idmaterial.Direction = ParameterDirection.Input

                Dim param_preciolista As New SqlClient.SqlParameter
                param_preciolista.ParameterName = "@preciolista"
                param_preciolista.SqlDbType = SqlDbType.Decimal
                param_preciolista.Precision = 18
                param_preciolista.Scale = 2
                param_preciolista.Value = grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecioLista).Value
                param_preciolista.Direction = ParameterDirection.Input

                Dim param_bonif1 As New SqlClient.SqlParameter
                param_bonif1.ParameterName = "@bonif1"
                param_bonif1.SqlDbType = SqlDbType.Decimal
                param_bonif1.Precision = 18
                param_bonif1.Scale = 2
                param_bonif1.Value = bonif1
                param_bonif1.Direction = ParameterDirection.Input

                Dim param_bonif2 As New SqlClient.SqlParameter
                param_bonif2.ParameterName = "@bonif2"
                param_bonif2.SqlDbType = SqlDbType.Decimal
                param_bonif2.Precision = 18
                param_bonif2.Scale = 2
                param_bonif2.Value = bonif2
                param_bonif2.Direction = ParameterDirection.Input

                Dim param_bonif3 As New SqlClient.SqlParameter
                param_bonif3.ParameterName = "@bonif3"
                param_bonif3.SqlDbType = SqlDbType.Decimal
                param_bonif3.Precision = 18
                param_bonif3.Scale = 2
                param_bonif3.Value = bonif3
                param_bonif3.Direction = ParameterDirection.Input

                Dim param_bonif4 As New SqlClient.SqlParameter
                param_bonif4.ParameterName = "@bonif4"
                param_bonif4.SqlDbType = SqlDbType.Decimal
                param_bonif4.Precision = 18
                param_bonif4.Scale = 2
                param_bonif4.Value = bonif4
                param_bonif4.Direction = ParameterDirection.Input

                Dim param_bonif5 As New SqlClient.SqlParameter
                param_bonif5.ParameterName = "@bonif5"
                param_bonif5.SqlDbType = SqlDbType.Decimal
                param_bonif5.Precision = 18
                param_bonif5.Scale = 2
                param_bonif5.Value = bonif5
                param_bonif5.Direction = ParameterDirection.Input

                Dim param_ganancia As New SqlClient.SqlParameter
                param_ganancia.ParameterName = "@ganancia"
                param_ganancia.SqlDbType = SqlDbType.Decimal
                param_ganancia.Precision = 18
                param_ganancia.Scale = 2
                param_ganancia.Value = ganancia
                param_ganancia.Direction = ParameterDirection.Input

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@precio"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecio).Value ' / grd.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value
                param_precio.Direction = ParameterDirection.Input

                Dim param_userupd As New SqlClient.SqlParameter
                param_userupd.ParameterName = "@userupd"
                param_userupd.SqlDbType = SqlDbType.Int
                param_userupd.Value = UserID
                param_userupd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_Update_Precio", _
                                    param_id, param_tablerista, param_idmaterial, param_preciolista, _
                                    param_bonif1, param_bonif2, param_bonif3, param_bonif4, param_bonif5, _
                                    param_ganancia, param_precio, param_userupd, param_res)

                    res = param_res.Value

                    If res < 0 Then
                        Exit For
                    End If

                Catch ex As Exception
                    Throw ex
                End Try
            Next

            AgregarActualizar_Registro = res

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

    Private Sub AgregarActualizar_Orden()
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        'Abrir una transaccion para guardar y asegurar que se guarda todo
        If Abrir_Tran(connection) = False Then
            MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Dim i As Integer

            For i = 0 To grd.RowCount - 1
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = grd.Rows(i).Cells(ColumnasDelGridItems.Id).Value
                param_id.Direction = ParameterDirection.Input

                Dim param_orden As New SqlClient.SqlParameter
                param_orden.ParameterName = "@orden"
                param_orden.SqlDbType = SqlDbType.Int
                param_orden.Value = grd.Rows(i).Cells(ColumnasDelGridItems.Orden).Value
                param_orden.Direction = ParameterDirection.Input

                Try

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Materiales_Update_Orden", _
                                    param_id, param_orden)

                Catch ex As Exception
                    Throw ex
                End Try
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

        End Try

    End Sub

#End Region

#Region "   Botones"

    Private Sub btnAplicarAumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplicarAumento.Click

        Dim i As Integer

        Try

            If chkBonif1.Checked = False And chkBonif2.Checked = False And chkBonif3.Checked = False And chkBonif4.Checked = False _
                    And chkBonif5.Checked = False And chkGanancia.Checked = False And chkAumento.Checked = False Then
                MsgBox("Debe seleccionar algún item para realizar la actualización en los precios de los materiales filtrados", MsgBoxStyle.Information, "Control de Aumento")
                Exit Sub
            End If

            If txtBonif1.Text = "" Then txtBonif1.Text = "0"
            If txtBonif2.Text = "" Then txtBonif2.Text = "0"
            'If txtBonif3.Text = "" Then txtBonif3.Text = "0"
            'If txtBonif4.Text = "" Then txtBonif4.Text = "0"
            'If txtBonif5.Text = "" Then txtBonif5.Text = "0"
            If txtGanancia.Text = "" Then txtGanancia.Text = "0"
            If txtAumento.Text = "" Then txtAumento.Text = "0"

            If chkBonif1.Checked = True And CInt(txtBonif1.Text) < 0 Then
                MsgBox("El porcentaje en el campo Bonif1 no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
                txtBonif1.Focus()
                Exit Sub
            End If

            If chkBonif2.Checked = True And CInt(txtBonif2.Text) < 0 Then
                MsgBox("El porcentaje en el campo Bonif2 no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
                txtBonif1.Focus()
                Exit Sub
            End If

            'If chkBonif3.Checked = True And CInt(txtBonif3.Text) < 0 Then
            '    MsgBox("El porcentaje en el campo Bonif3 no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
            '    txtBonif1.Focus()
            '    Exit Sub
            'End If

            'If chkBonif4.Checked = True And CInt(txtBonif4.Text) < 0 Then
            '    MsgBox("El porcentaje en el campo Bonif4 no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
            '    txtBonif1.Focus()
            '    Exit Sub
            'End If

            'If chkBonif5.Checked = True And CInt(txtBonif5.Text) < 0 Then
            '    MsgBox("El porcentaje en el campo Bonif5 no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
            '    txtBonif1.Focus()
            '    Exit Sub
            'End If

            If chkGanancia.Checked = True And CInt(txtGanancia.Text) < 0 Then
                MsgBox("El porcentaje en el campo Ganancia no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
                txtBonif1.Focus()
                Exit Sub
            End If

            'If chkPrecioLista.Checked = True And CInt(txtAumento.Text) < 0 Then
            '    MsgBox("El porcentaje en el campo Precio Lista no puede estar vacío o ser menor a 0 (cero). Por favor, verifique este dato.", MsgBoxStyle.Information, "Control de Aumento")
            '    txtAumento.Focus()
            '    Exit Sub
            'End If

            If grd.RowCount = 0 Then
                MsgBox("Verifique los filtros para que aparezcan registros en la grilla.", MsgBoxStyle.Information, "Control de Aumento")
                Exit Sub
            End If

            Dim aumento As Double, preciolista As Double

            aumento = (CDbl(txtAumento.Text) / 100) + 1

            For i = 0 To grd.RowCount - 1

                If chkPrecioLista.Checked = True Then
                    If rdPrecioTabl.Checked = True Then
                        preciolista = FormatNumber(grd.Rows(i).Cells(ColumnasDelGridItems.preciolista).Value * aumento, 2)
                    Else
                        preciolista = FormatNumber(grd.Rows(i).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value * aumento, 2)
                    End If
                Else
                    If rdPrecioTabl.Checked = True Then
                        preciolista = grd.Rows(i).Cells(ColumnasDelGridItems.preciolista).Value
                    Else
                        preciolista = grd.Rows(i).Cells(ColumnasDelGridItems.preciolista_distribuidor).Value
                    End If
                End If

                grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecioLista).Value = preciolista

                If chkBonif1.Checked = True Then
                    preciolista = preciolista * (1 - (CDbl(txtBonif1.Text) / 100))
                End If

                If chkBonif2.Checked = True Then
                    preciolista = preciolista * (1 - (CDbl(txtBonif2.Text) / 100))
                End If

                'If chkBonif3.Checked = True Then
                '    preciolista = preciolista / (1 + CDbl(txtBonif3.Text) / 100)
                'End If

                'If chkBonif4.Checked = True Then
                '    preciolista = preciolista / (1 + CDbl(txtBonif4.Text) / 100)
                'End If

                'If chkBonif5.Checked = True Then
                '    preciolista = preciolista / (1 + CDbl(txtBonif5.Text) / 100)
                'End If

                'preciolista = preciolista * (grd.Rows(i).Cells(ColumnasDelGridItems.iva).Value / 100)

                If chkGanancia.Checked = True Then
                    preciolista = preciolista * (1 + (CDbl(txtGanancia.Text) / 100))
                Else
                    preciolista = preciolista * (1 + (CDbl(grd.Rows(i).Cells(ColumnasDelGridItems.ganancia).Value) / 100))
                End If

                'grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecio).Value = FormatNumber(preciolista * grd.Rows(i).Cells(ColumnasDelGridItems.valorcambio).Value, 2)
                grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecio).Value = FormatNumber(preciolista, 2)

            Next

            presionoAplicar = True

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

            For i = 0 To grd.RowCount - 1
                grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecioLista).Value = 0

                grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecio).Value = 0
            Next

            presionoAplicar = False

        End Try

    End Sub

    Private Sub btnCancelarAumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelarAumento.Click
        txtAumento.Text = ""

        ArmarConsulta()

        SQL = Consulta '"exec spMateriales_Select_All"

        LlenarGrilla1()

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        Util.MsgStatus(Status1, "Actualizando el registro...", My.Resources.Resources.indicator_white)

        If chkModificarOrden.Checked = False Then
            If chkAumento.Checked = True And presionoAplicar = False Then
                If MessageBox.Show("No ha presionado el botón 'Aplicar Aumento'. Desea que el sistema lo haga automáticamente y continúe con el proceso?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    btnAplicarAumento_Click(sender, e)
                Else
                    Exit Sub
                End If
            End If
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro()
                Select Case res
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        Cerrar_Tran()
                        txtAumento.Text = ""
                        Util.MsgStatus(Status1, "Se actualizó correctamente el o los materiales", My.Resources.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se actualizó correctamente el o los materiales", My.Resources.Resources.ok.ToBitmap, True, True)
                        bolModo = False
                        'btnActualizar_Click(sender, e)

                        chkAumento.Checked = False

                        'chkAumento_CheckedChanged(sender, e)

                        LlenarGrilla1()

                        btnNuevo.Enabled = False
                End Select
            End If
        Else
            Verificar_Datos()
            If bolpoliticas Then
                AgregarActualizar_Orden()
                Cerrar_Tran()
                txtAumento.Text = ""
                Util.MsgStatus(Status1, "Se actualizó correctamente el o los materiales", My.Resources.Resources.ok.ToBitmap)
                bolModo = False
                LlenarGrilla1()
                chkModificarOrden.Checked = False

                btnNuevo.Enabled = False
            End If
        End If

    End Sub

    Public Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btnNuevo.Enabled = False
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim param As New frmParametros
        'Dim cnn As New SqlConnection(ConnStringSEI)
        Dim filtro As String
        Dim rpt As New frmReportes

        nbreformreportes = "Productos"

        Dim consulta As String = "select busqueda from CriteriosdeBusquedas ORDER BY Busqueda"

        param.AgregarParametros("Filtro :", "STRING", "", False, "", consulta, conexionGenerica)

        param.ShowDialog()

        If cerroparametrosconaceptar = True Then
            filtro = param.ObtenerParametros(0)

            rpt.Materiales_App(filtro, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing
            'cnn = Nothing
        End If

    End Sub

    Private Sub btnGuardarCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarCriterio.Click
        Dim res As Integer = 0
        'Dim connection As SqlClient.SqlConnection = Nothing
        Try
            'Try
            '    connection = SqlHelper.GetConnection(ConnStringSEI)
            'Catch ex As Exception
            '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try

            Try

                Dim param_busqueda As New SqlClient.SqlParameter
                param_busqueda.ParameterName = "@texto"
                param_busqueda.SqlDbType = SqlDbType.VarChar
                param_busqueda.Size = 200
                param_busqueda.Value = cmbNombre.Text
                param_busqueda.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spCriteriosdeBusqueda_Insert", _
                                              param_busqueda)

                    Util.MsgStatus(Status1, "Se guardó correctamente el criterio", My.Resources.Resources.ok.ToBitmap)

                    LlenarcmbNombre()

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try
    End Sub

    Private Sub btnEliminarCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCriterio.Click
        Dim res As Integer = 0
        'Dim connection As SqlClient.SqlConnection = Nothing
        Try
            'Try
            '    connection = SqlHelper.GetConnection(ConnStringSEI)
            'Catch ex As Exception
            '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try

            Try

                Dim param_busqueda As New SqlClient.SqlParameter
                param_busqueda.ParameterName = "@texto"
                param_busqueda.SqlDbType = SqlDbType.VarChar
                param_busqueda.Size = 200
                param_busqueda.Value = cmbNombre.Text
                param_busqueda.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(conexionGenerica, CommandType.StoredProcedure, "spCriteriosdeBusqueda_Delete", _
                                              param_busqueda)

                    Util.MsgStatus(Status1, "Se eliminó correctamente el criterio", My.Resources.Resources.ok.ToBitmap)

                    LlenarcmbNombre()

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
            'If Not connection Is Nothing Then
            '    CType(connection, IDisposable).Dispose()
            'End If
        End Try
    End Sub

#End Region

#Region "   Grilla"

    Protected Sub LlenarGrilla1()
        Dim o(1) As String

        GetDataset()

        ' Paginar la consulta para obtener solo los primeros 50 registros
        If TotalPaginas1 = 0 Then
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
            .AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine ' Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        End With

        With grd.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 8, FontStyle.Bold)
        End With

        grd.Font = New Font("TAHOMA", 7, FontStyle.Regular)

        If Filter.Trim <> "" Then
            Dim d As New DataTable
            d = FiltrarGenerico1(ds.Tables(0))
            Establecer_Cant_Paginas(d)
            bolPaginar = False
            ToolStripPagina.Text = "1"
            bolPaginar = True
            paginar1(d)
        Else
            paginar1(ds.Tables(0)) 'Asignar los datos paginados a la grilla
        End If
        'CP 18-03-2012 Fin

        'paginar(ds.Tables(0)) 'Asignar los datos paginados a la grilla
        'grd.Columns(2).Width = 400
        If Not grd.CurrentRow Is Nothing Then
            MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString)
        Else
            bolModo = True
            PrepararBotones()
            'Util.LimpiarTextBox(Me.Controls)
            Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        End If


        'ConfigurarGrilla()

        CheckForIllegalCrossThreadCalls = False

        trd = New Thread(AddressOf ConfigurarGrilla)
        trd.IsBackground = True
        trd.Start()
        'grd.Columns(0).Frozen = True

    End Sub

    Protected Sub GetDataset1()
        'Dim connection As SqlClient.SqlConnection = Nothing

        'Try
        '    connection = SqlHelper.GetConnection(ConnStringSEI)
        'Catch ex As Exception
        '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        Try

            ds = SqlHelper.ExecuteDataset(conexionGenerica, CommandType.Text, SQL)

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

    Private Function FiltrarGenerico1(ByVal d As DataTable) As DataTable
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

    Private Sub paginar1(ByVal d As DataTable)

        Dim o(1) As String
        Dim TEMP As String = Nothing
        Static Flag As Boolean = False

        'esto no sirve porque la grilla queda enlazada y despues no se pueden agregar registros
        'Me.grd.DataSource = paginarDataDridView(d, ini, fin) original

        dtnew1 = paginarDataDridView(d, ini1, fin1)

        ReDim o(dtnew1.Columns.Count * dtnew1.Rows.Count)

        grd.Rows.Clear()
        If Not Flag Then
            SetColumnasGrilla()
            Flag = True
        End If

        'For Each row As DataRow In ds.Tables(0).Rows
        For Each row As DataRow In dtnew1.Rows
            'nuevo
            For i As Integer = 0 To grd.Columns.Count - 1
                Select Case grd.Columns(i).ValueType.Name.ToUpper
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
            grd.Rows.Add(o)
        Next

        'grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        dtnew1.Dispose()

        InformarCantidadRegistros1()

    End Sub

    Private Sub InformarCantidadRegistros1()
        If Not grd.CurrentRow Is Nothing Then
            MsgStatus(Status1, "Registro " & (grd.CurrentRow.Index + 1).ToString & " de " & grd.Rows.Count.ToString & " Total de Páginas: " & TotalPaginas1.ToString)
            ''NUEVO MS 25-10-2011 / 31-10-2011se cambio en aplicacion Seguridad y andubo bien una semana
            If bolModo = True And quitarfiltro1 = True Then
                bolModo = False
                PrepararBotones()
            End If
            ''FIN NUEVO
        Else
            If grd.Rows.Count < 1 Then
                'Util.LimpiarTextBox(Me.Controls)
                bolModo = True
                PrepararBotones()
            End If
        End If
    End Sub

    Private Sub ConfigurarGrilla()
        Try
            grd.Columns(ColumnasDelGridItems.idfamilia).Visible = False
            grd.Columns(ColumnasDelGridItems.idsubrubro).Visible = False
            grd.Columns(ColumnasDelGridItems.idunidad).Visible = False
            grd.Columns(ColumnasDelGridItems.codunidad).Visible = False

            grd.Columns(ColumnasDelGridItems.ganancia).Visible = rdPrecioTabl.Checked
            grd.Columns(ColumnasDelGridItems.ganancia_Distibuidor).Visible = rdPrecioDist.Checked

            grd.Columns(ColumnasDelGridItems.NvoPrecio).Visible = False
            grd.Columns(ColumnasDelGridItems.unidad).Visible = False
            grd.Columns(ColumnasDelGridItems.NvoPrecioLista).Visible = False
            grd.Columns(ColumnasDelGridItems.idmatprov).Visible = False
            grd.Columns(ColumnasDelGridItems.valorcambio).Visible = False
            grd.Columns(ColumnasDelGridItems.rubro).Visible = False
            grd.Columns(ColumnasDelGridItems.subrubro).Visible = False
            grd.Columns(ColumnasDelGridItems.IdProveedor).Visible = False
            grd.Columns(ColumnasDelGridItems.IdMoneda).Visible = False

            grd.Columns(ColumnasDelGridItems.preciolista).Visible = rdPrecioTabl.Checked
            grd.Columns(ColumnasDelGridItems.preciolista_distribuidor).Visible = rdPrecioDist.Checked

            grd.Columns(ColumnasDelGridItems.PrecioSinIva).Visible = rdPrecioTabl.Checked
            grd.Columns(ColumnasDelGridItems.PrecioSinIva_Distribuidor).Visible = rdPrecioDist.Checked

            grd.Columns(ColumnasDelGridItems.PrecioIva21).Visible = False
            grd.Columns(ColumnasDelGridItems.PrecioIva10).Visible = False


            grd.Columns(ColumnasDelGridItems.Cod_Moneda).Visible = False
            grd.Columns(ColumnasDelGridItems.IdMarca).Visible = False
            grd.Columns(ColumnasDelGridItems.montoIva).Visible = False
            grd.Columns(ColumnasDelGridItems.montoIva_Distribuidor).Visible = False

            grd.Columns(ColumnasDelGridItems.precioxkg).Visible = False
            grd.Columns(ColumnasDelGridItems.precioxmt).Visible = False

            grd.Columns(ColumnasDelGridItems.unidad).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grd.Columns(ColumnasDelGridItems.preciolista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grd.Columns(ColumnasDelGridItems.preciolista_distribuidor).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grd.Columns(ColumnasDelGridItems.PrecioSinIva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grd.Columns(ColumnasDelGridItems.PrecioSinIva_Distribuidor).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grd.Columns(ColumnasDelGridItems.PrecioIva21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(ColumnasDelGridItems.PrecioIva10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            grd.Columns(ColumnasDelGridItems.NvoPrecio).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grd.Columns(ColumnasDelGridItems.NvoPrecioLista).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'grd.Columns(ColumnasDelGridItems.ganancia).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(ColumnasDelGridItems.ganancia_Distibuidor).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            grd.Columns(ColumnasDelGridItems.minimo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grd.Columns(ColumnasDelGridItems.maximo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grd.Columns(ColumnasDelGridItems.Moneda).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            grd.Columns(ColumnasDelGridItems.stockactual).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            grd.Columns(ColumnasDelGridItems.Codigo).Width = 80
            grd.Columns(ColumnasDelGridItems.Codigo_Mat_Prov).Width = 80
            grd.Columns(ColumnasDelGridItems.Nombre_Material).Width = 330
            'grd.Columns(ColumnasDelGridItems.Orden).Width = 50
            grd.Columns(ColumnasDelGridItems.Orden).Visible = False

            grd.Columns(ColumnasDelGridItems.preciolista).Width = 60
            grd.Columns(ColumnasDelGridItems.preciolista_distribuidor).Width = 60
            grd.Columns(ColumnasDelGridItems.PrecioSinIva).Width = 75
            grd.Columns(ColumnasDelGridItems.PrecioSinIva_Distribuidor).Width = 75
            grd.Columns(ColumnasDelGridItems.PrecioIva21).Width = 75
            grd.Columns(ColumnasDelGridItems.PrecioIva10).Width = 75
            grd.Columns(ColumnasDelGridItems.NvoPrecio).Width = 80
            grd.Columns(ColumnasDelGridItems.NvoPrecioLista).Width = 80
            grd.Columns(ColumnasDelGridItems.Moneda).Width = 70
            'grd.Columns(ColumnasDelGridItems.ganancia).Width = 65
            grd.Columns(ColumnasDelGridItems.ganancia).Visible = False
            'grd.Columns(ColumnasDelGridItems.ganancia_Distibuidor).Width = 65
            grd.Columns(ColumnasDelGridItems.ganancia).Visible = False

            grd.Columns(ColumnasDelGridItems.Proveedor).Width = 120

            grd.Columns(ColumnasDelGridItems.maximo).Width = 55
            grd.Columns(ColumnasDelGridItems.minimo).Width = 55

            grd.Columns(ColumnasDelGridItems.dateupd).Width = 55

            grd.Columns(ColumnasDelGridItems.stockactual).Width = 50

            'grd.Columns(ColumnasDelGridItems.iva).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(ColumnasDelGridItems.iva).Width = 35
            grd.Columns(ColumnasDelGridItems.iva).Visible = False

            'grd.Columns(ColumnasDelGridItems.iva_distribuidor).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(ColumnasDelGridItems.iva_distribuidor).Width = 35
            grd.Columns(ColumnasDelGridItems.iva_distribuidor).Visible = False

            Dim i As Integer

            grd.ReadOnly = False

            For i = 0 To grd.ColumnCount - 1
                grd.Columns(i).ReadOnly = True
            Next

            grd.Columns(ColumnasDelGridItems.PrecioSinIva).ReadOnly = False
            grd.Columns(ColumnasDelGridItems.NvoPrecio).ReadOnly = False
        Catch ex As Exception

        End Try
        
    End Sub


#End Region

    Private Sub rdPrecioTabl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdPrecioTabl.CheckedChanged
        If band = True Then
            'ConfigurarGrilla()

            CheckForIllegalCrossThreadCalls = False

            trd = New Thread(AddressOf ConfigurarGrilla)
            trd.IsBackground = True
            trd.Start()

            If chkAumento.Checked = True Then
                grd.Columns(ColumnasDelGridItems.NvoPrecio).Visible = chkAumento.Checked
                grd.Columns(ColumnasDelGridItems.NvoPrecio).DefaultCellStyle.ForeColor = Color.Red
                grd.Columns(ColumnasDelGridItems.NvoPrecioLista).Visible = chkAumento.Checked
                grd.Columns(ColumnasDelGridItems.NvoPrecioLista).DefaultCellStyle.ForeColor = Color.Red
                Dim i As Integer
                For i = 0 To grd.RowCount - 1
                    grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecio).Value = 0.0
                    grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecioLista).Value = 0.0
                Next
            End If
        End If
    End Sub

    Private Sub rdPrecioDist_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdPrecioDist.CheckedChanged
        If band = True Then
            'ConfigurarGrilla()

            CheckForIllegalCrossThreadCalls = False

            trd = New Thread(AddressOf ConfigurarGrilla)
            trd.IsBackground = True
            trd.Start()

            If chkAumento.Checked = True Then
                grd.Columns(ColumnasDelGridItems.NvoPrecio).Visible = chkAumento.Checked
                grd.Columns(ColumnasDelGridItems.NvoPrecio).DefaultCellStyle.ForeColor = Color.Red
                grd.Columns(ColumnasDelGridItems.NvoPrecioLista).Visible = chkAumento.Checked
                grd.Columns(ColumnasDelGridItems.NvoPrecioLista).DefaultCellStyle.ForeColor = Color.Red
                Dim i As Integer
                For i = 0 To grd.RowCount - 1
                    grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecio).Value = 0.0
                    grd.Rows(i).Cells(ColumnasDelGridItems.NvoPrecioLista).Value = 0.0
                Next
            End If
        End If
    End Sub

End Class