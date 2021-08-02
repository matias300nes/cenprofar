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

Public Class frmMateriales

    Private RefrescarGrid As Boolean
    Private ds_2 As DataSet

    Private ds_cmblista As Data.DataSet

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
    Dim precioganancia As Double

    Dim band As Integer

    Dim CantRegistrosImportados As Long
    Dim CantRegistrosActualizados As Long

    Dim permitir_evento_CellChanged As Boolean = False

    Dim CargaContinua As Boolean = False

    Dim trd As Thread

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim desdekeydown As Boolean = False
    Dim ValorCambio As Double

    Dim ValorMayo As Double
    Dim PorcenMayo As String

    Dim ValorReven As Double
    Dim PorcenReven As String

    Dim ValorYami As Double
    Dim PorcenYami As String

    Dim Valor4 As Double
    Dim Porcen4 As String




    'Enum ColumnasDelGridItems
    '    id = 0
    '    Codigo_Mat_Prov = 1
    '    CodigoProv = 2
    '    NombreProv = 3
    '    PlazoEntrega = 4
    '    IdUniCompra = 5
    '    CodUnidad = 6
    '    Unidad = 7
    '    IdMoneda = 8
    '    CodMoneda = 9
    '    Moneda = 10
    '    ValorCambio = 11
    '    IdMarca = 12
    '    CodMarca = 13
    '    Marca = 14
    '    PrecioxMt = 15
    '    PrecioxKg = 16
    '    PesoxMetro = 17
    '    CantxLongitud = 18
    '    PesoxUnidad = 19
    '    PrecioLista = 20
    '    PrecioLista_Distribuidor = 21
    '    Bonif1 = 22
    '    Bonif2 = 23
    '    Bonif1_Distribuidor = 24
    '    Bonif2_Distribuidor = 25
    '    Bonif3 = 26
    '    Bonif4 = 27
    '    Bonif5 = 28
    '    Ganancia = 29
    '    Ganancia_Distribuidor = 30
    '    IVA = 31
    '    IVA_distribuidor = 32
    '    PrecioVentaxBulto = 33
    '    PrecioVentaxBulto_distribuidor = 34
    '    CantxBulto = 35
    '    PrecioVentasinIVa = 36
    '    PrecioVentasinIVa_distribuidor = 37
    '    IdProv = 38
    '    PrecioEnPesos = 39
    'End Enum

#Region "   Eventos"

    Private Sub frmMateriales_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        If permitir_evento_CellChanged Then
            If txtID.Text <> "" Then
                'LlenarGridItems()
                LlenarGridItemStock()
            End If
        End If
    End Sub

    Private Sub frmAjustes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        btnSalir_Click(sender, e)
    End Sub

    Private Sub frmMateriales_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If bolModo = False Then
            If chkEliminados.Checked = False Then
                SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = 0"
            Else
                SQL = "exec spMateriales_Select_All @Eliminado = 1,@Fraccionados = 0"
            End If
            btnActualizar_Click(sender, e)
        End If
    End Sub

    Private Sub frmMateriales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Material Nuevo que est� realizando. �Est� seguro que desea continuar sin Grabar y hacer un Nuevo Material?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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

      
    

        If MDIPrincipal.sucursal.ToUpper.Contains("PERON") Then
            PicFAMILIAS.Enabled = False
            PicMARCAS.Enabled = False
            PicUNIDADES.Enabled = False
        Else
            Try
                If MDIPrincipal.EmpleadoLogueado = "12" Or MDIPrincipal.EmpleadoLogueado = "13" Or MDIPrincipal.EmpleadoLogueado = "2" Then
                    Dim sqlstring As String = "update NotificacionesWEB set BloqueoM = 1"
                    tranWEB.Sql_Set(sqlstring)
                End If
              
            Catch ex As Exception

            End Try
        End If

        band = 0
        configurarform()
        asignarTags()

        'LlenarcmbUnidades_App(cmbUNIDADES, ConnStringSEI)
        LlenarcmbUnidades()
        LlenarcmbRubros()
        LlenarcmbMarcas()
        CargarlistaPrincipales()
        'LlenarcmbListaMayorista()
        'LlenarcmbListaMinorista()
        LlenarcmbListaMinoristaPeron()
        LlenarcmbListaMayoristaPeron()
        'LlenarcmbLista3()
        LlenarcmbLista4()
        'Traer_ValorCambio()

        SQL = "exec spMateriales_Select_All @Eliminado = 0, @Fraccionados = 0"
        LlenarGrilla()

        Permitir = True
        CargarCajas()
        PrepararBotones()

        If bolModo = True Then
            'LlenarGridItems()
            LlenarGridItemStock()
            btnNuevo_Click(sender, e)
        Else
            'LlenarGridItems()
            LlenarGridItemStock()
            grdItemsStock.ReadOnly = True
        End If
        Try
            If grd.RowCount > 0 Then
                'cmbAlmacenes.SelectedValue = grd.CurrentRow.Cells(2).Value
                cmbFAMILIAS.SelectedValue = grd.CurrentRow.Cells(4).Value
                cmbUNIDADES.SelectedValue = grd.CurrentRow.Cells(6).Value
                'txtPrecioCompra.Text = grd.CurrentRow.Cells(8).Value
                'txtPrecioMinorista.Text = grd.CurrentRow.Cells(9).Value
                'txtPrecioMayorista.Text = grd.CurrentRow.Cells(10).Value
                'txtPrecio3.Text = grd.CurrentRow.Cells(11).Value
                'txtPrecio4.Text = grd.CurrentRow.Cells(12).Value
                'txtPrecioMinoristaPeron.Text = grd.CurrentRow.Cells(13).Value
                'txtPrecioMayoristaPeron.Text = grd.CurrentRow.Cells(14).Value
                cmbMarca.SelectedValue = grd.CurrentRow.Cells(24).Value
                'cmbMayorista.SelectedValue = grd.CurrentRow.Cells(26).Value
                cmbMayoristaPeron.SelectedValue = grd.CurrentRow.Cells(27).Value
                'cmbMinorista.SelectedValue = grd.CurrentRow.Cells(28).Value
                cmbMinoristaPeron.SelectedValue = grd.CurrentRow.Cells(29).Value
                'cmbLista3.SelectedValue = grd.CurrentRow.Cells(30).Value
                'ME FIJO SI TIENE UNA UNIDAD DE REFERENCIA
                Select Case grd.CurrentRow.Cells(32).Value.ToString <> ""
                    Case grd.CurrentRow.Cells(32).Value.ToString.Contains("KILOGRAMO")
                        chkUniRef.Checked = True
                        rdKilo.Checked = True
                        Label15.Text = "Precio de Costo (KG)*"
                    Case grd.CurrentRow.Cells(32).Value.ToString.Contains("LITROS")
                        chkUniRef.Checked = True
                        rdLitros.Checked = True
                        Label15.Text = "Precio de Costo (L)*"
                    Case grd.CurrentRow.Cells(32).Value.ToString.Contains("UNIDAD")
                        chkUniRef.Checked = True
                        rdUnidad.Checked = True
                        Label15.Text = "Precio de Costo (U)*"
                    Case Else
                        chkUniRef.Checked = False
                End Select
                chk1.Checked = grd.CurrentRow.Cells(33).Value
                chk2.Checked = grd.CurrentRow.Cells(34).Value
                chk3.Checked = grd.CurrentRow.Cells(35).Value
                chk4.Checked = grd.CurrentRow.Cells(36).Value
                chkVentaMayorista.Checked = grd.CurrentRow.Cells(37).Value
                cmbLista4.SelectedValue = grd.CurrentRow.Cells(31).Value
                cmbLista4.Enabled = Not chk4.Checked
                ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select Valor_Cambio,Porcentaje from lista_precios where Codigo =" & cmbLista4.SelectedValue)
                Valor4 = ds_cmblista.Tables(0).Rows(0).Item(0)
                Porcen4 = Math.Round(ds_cmblista.Tables(0).Rows(0).Item(1), 2)
                CalcularPorcentaje_Precio(0)
                ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select Valor_Cambio,Porcentaje from lista_precios where Codigo =" & cmbMinoristaPeron.SelectedValue)
                lblPorcenPeronMin.Text = Math.Round(ds_cmblista.Tables(0).Rows(0).Item(1), 2)
                ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select Valor_Cambio,Porcentaje from lista_precios where Codigo =" & cmbMayoristaPeron.SelectedValue)
                ds_cmblista.Dispose()
                lblPorcenPeronMay.Text = Math.Round(ds_cmblista.Tables(0).Rows(0).Item(1), 2)
            End If
        Catch ex As Exception

        End Try



        grd.Columns(0).Visible = False
        grd.Columns(1).Visible = False
        'grd.Columns(2).Visible = False
        ''grd.Columns(3).Visible = False
        grd.Columns(4).Visible = False
        ''grd.Columns(5).Visible = False
        grd.Columns(6).Visible = False

        'grd.Columns(9).Width = 200
        'grd.Columns(10).Width = 200
        'grd.Columns(11).Width = 200
        'grd.Columns(12).Width = 200
        'grd.Columns(13).Width = 200
        'grd.Columns(14).Width = 200


        grd.Columns(15).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(17).Visible = False
        grd.Columns(18).Visible = False
        grd.Columns(19).Visible = False
        grd.Columns(20).Visible = False
        grd.Columns(21).Visible = False
        grd.Columns(22).Visible = False
        grd.Columns(23).Visible = False
        grd.Columns(24).Visible = False
        grd.Columns(25).Visible = False
        grd.Columns(26).Visible = False
        grd.Columns(27).Visible = False
        grd.Columns(28).Visible = False
        grd.Columns(29).Visible = False
        grd.Columns(30).Visible = False
        grd.Columns(31).Visible = False

        grd.Columns(33).Visible = False
        grd.Columns(34).Visible = False
        grd.Columns(35).Visible = False
        grd.Columns(36).Visible = False
        grd.Columns(37).Visible = False

        permitir_evento_CellChanged = True

        'If MDIPrincipal.UltBusquedaMat <> "" Then
        '    cmbNombre.Text = MDIPrincipal.UltBusquedaMat
        '    chkNombre.Checked = True
        '    chkNombre_CheckedChanged(sender, e)
        'End If

        btnImportarExcel.Visible = True
        btnImagenExcel.Visible = True

        Buscar_GananciaxDefecto()

        band = 1

        Cursor = Cursors.Default

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles txtID.KeyPress, txtCODIGO.KeyPress, txtNOMBRE.KeyPress, _
            cmbFAMILIAS.KeyPress, cmbUNIDADES.KeyPress, txtPrecioCompra.KeyPress, _
            txtMinimo.KeyPress, txtPrecioMayorista.KeyPress, txtPrecioMinorista.KeyPress, txtPrecio3.KeyPress, txtPrecio4.KeyPress

        Dim strAcentos As String = "����������������������������������������������'"

        If strAcentos.IndexOf(e.KeyChar) > 0 Then
            e.Handled = True
        End If


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
        'LlenarcmbFamilias_App(cmbFAMILIAS, ConnStringSEI)
        LlenarcmbRubros()
        cmbFAMILIAS.Text = texto_del_combo
    End Sub

    Private Sub PicUnidades_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicUNIDADES.Click
        Dim f As New frmUnidades

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbUNIDADES.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbUnidades()
        'LlenarcmbUnidades_App(cmbUNIDADES, ConnStringSEI)
        cmbUNIDADES.Text = texto_del_combo
    End Sub

    Private Sub PicMARCAS_Click(sender As Object, e As EventArgs) Handles PicMARCAS.Click

        Dim f As New frmMarcas

        LLAMADO_POR_FORMULARIO = True
        ARRIBA = 90
        IZQUIERDA = Me.Left + 20
        texto_del_combo = cmbMarca.Text.ToUpper.ToString
        f.ShowDialog()
        LlenarcmbMarcas()
        'LlenarcmbMarcas_App(cmbMarca, ConnStringSEI)
        cmbMarca.Text = texto_del_combo

    End Sub

    '(CurrentCellChanged)
    Protected Overloads Sub grd_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.CurrentCellChanged

        If Permitir Then

            'LlenarGridItems()
            LlenarGridItemStock()
            'chkOcultarBonif.Checked = True
            'chkOcultarBonif_CheckedChanged(sender, e)
            Try
                If grd.RowCount > 0 Then
                    cmbFAMILIAS.SelectedValue = grd.CurrentRow.Cells(4).Value
                    cmbUNIDADES.SelectedValue = grd.CurrentRow.Cells(6).Value
                    cmbMarca.SelectedValue = grd.CurrentRow.Cells(24).Value
                    ' cmbMayorista.SelectedValue = grd.CurrentRow.Cells(26).Value
                    cmbMayoristaPeron.SelectedValue = grd.CurrentRow.Cells(27).Value
                    ' cmbMinorista.SelectedValue = grd.CurrentRow.Cells(28).Value
                    cmbMinoristaPeron.SelectedValue = grd.CurrentRow.Cells(29).Value
                    ' cmbLista3.SelectedValue = grd.CurrentRow.Cells(30).Value
                    'ME FIJO SI TIENE UNA UNIDAD DE REFERENCIA
                    Select Case grd.CurrentRow.Cells(32).Value.ToString <> ""
                        Case grd.CurrentRow.Cells(32).Value.ToString.Contains("KILOGRAMO")
                            chkUniRef.Checked = True
                            rdKilo.Checked = True
                        Case grd.CurrentRow.Cells(32).Value.ToString.Contains("LITROS")
                            chkUniRef.Checked = True
                            rdLitros.Checked = True
                        Case grd.CurrentRow.Cells(32).Value.ToString.Contains("UNIDAD")
                            chkUniRef.Checked = True
                            rdUnidad.Checked = True
                        Case Else
                            chkUniRef.Checked = False
                    End Select
                    chk1.Checked = grd.CurrentRow.Cells(33).Value
                    chk2.Checked = grd.CurrentRow.Cells(34).Value
                    chk3.Checked = grd.CurrentRow.Cells(35).Value
                    chk4.Checked = grd.CurrentRow.Cells(36).Value
                    chkVentaMayorista.Checked = grd.CurrentRow.Cells(37).Value
                    cmbLista4.SelectedValue = grd.CurrentRow.Cells(31).Value
                    CalcularPorcentaje_Precio(0)

                End If
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEliminados.CheckedChanged
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnActivar.Enabled = chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spMateriales_Select_All @Eliminado = 1,@Fraccionados = " & IIf(chkFraccionados.Checked, 1, 0)
        Else
            SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = " & IIf(chkFraccionados.Checked, 1, 0)
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

    Private Sub cmbMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMarca.SelectedIndexChanged
        If band = 1 Then
            Try
                txtIdMarca.Text = cmbMarca.SelectedValue
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbUNIDADES_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUNIDADES.SelectedIndexChanged

        If band = 1 Then
            Try
                txtIdUnidad.Text = cmbUNIDADES.SelectedValue
            Catch ex As Exception

            End Try
        End If
        If cmbUNIDADES.Text.Contains("HORMA") Or cmbUNIDADES.Text.Contains("KG") Or cmbUNIDADES.Text.Contains("TIRA") Then
            txtCodBarra.Enabled = False
        Else
            txtCodBarra.Enabled = True
        End If
        'me fijo si es un pack o una bolsa
        If cmbUNIDADES.Text.Contains("PACK") Or cmbUNIDADES.Text.Contains("PAQUETE") Then
            chkUniRef.Checked = True
            rdUnidad.Checked = True
        Else
            If cmbUNIDADES.Text.Contains("BOLSA") Then
                chkUniRef.Checked = True
                rdKilo.Checked = True
            Else
                chkUniRef.Checked = False
            End If
        End If

    End Sub

    Private Sub grdItems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        editando_celda = False

        '        Try

        '            Dim valorcambio As Double = 0
        '            Dim ganancia As Double = 0, preciovta As Double = 0
        '            Dim idmoneda As Long = 0
        '            Dim nombre As String = "", codmoneda As String = ""

        '            If e.ColumnIndex = ColumnasDelGridItems.Cod_Material Then
        '                'completar la descripcion del material
        '                Dim cell As DataGridViewCell = grdItems.CurrentCell
        '                Dim id As Long
        '                Dim stock As Double = 0, minimo As Double = 0, maximo As Double = 0, preciolista As Double = 0, preciovtaorig As Double = 0, gananciaorig As Double = 0, iva As Double = 0, montoiva As Double = 0
        '                Dim fecha As String = ""
        '                Dim i As Integer

        '                Dim codigo As String, codigo_mat_prov As String = "", unidad As String = "", codunidad As String = "", nombreproveedor As String = "", nombremarca As String = "", plazoentrega As String = ""
        '                Dim idunidad As Long = 0, idproveedor As Long = 0, idmarca As Long = 0
        '                Dim Pasillo As String = "", Estante As String = "", Fila As String = ""

        '                If grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value Is DBNull.Value Then
        '                    Exit Sub
        '                End If

        '                'verificamos si el codigo ya esta en la grilla...
        '                For i = 0 To grdItems.RowCount - 2
        '                    Dim cuentafilas As Integer
        '                    Dim codigo_mat As String = "", codigo_mat_2 As String = ""
        '                    codigo_mat = IIf(grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(i).Cells(ColumnasDelGridItems.Cod_Material).Value)
        '                    For cuentafilas = i + 1 To grdItems.RowCount - 2
        '                        If grdItems.RowCount - 1 > 1 Then
        '                            'codigo_mat_2 = grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value
        '                            codigo_mat_2 = IIf(grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value, "", grdItems.Rows(cuentafilas).Cells(ColumnasDelGridItems.Cod_Material).Value)
        '                            If codigo_mat <> "" And codigo_mat_2 <> "" Then
        '                                If codigo_mat = codigo_mat_2 Then
        '                                    Util.MsgStatus(Status1, "El c�digo '" & codigo_mat & "' esta repetido en la fila: " & (i + 1).ToString & " y la fila: " & cuentafilas + 1, My.Resources.Resources.alert.ToBitmap, True)
        '                                    cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex)
        '                                    grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value = " "
        '                                    Exit Sub
        '                                End If
        '                            End If
        '                        End If
        '                    Next
        '                Next

        '                codigo = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value
        '                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
        '                If ObtenerMaterial_App(codigo, codigo_mat_prov, id, nombre, idunidad, unidad, codunidad, stock, minimo, maximo, preciolista, ganancia, preciovta, preciovtaorig, gananciaorig, iva, fecha, idproveedor, nombreproveedor, idmarca, nombremarca, plazoentrega, idmoneda, codmoneda, valorcambio, montoiva, 0, chkPrecioDistribuidor.Checked, ConnStringSEI, "", Pasillo, Estante, Fila) = 0 Then
        '                    cell.Value = nombre

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDMaterial).Value = id

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Value = codigo_mat_prov
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Value = nombre

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = codunidad
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = unidad
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Value = Math.Round(stock, 2)
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Minimo).Value = minimo
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Maximo).Value = maximo

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMoneda).Value = txtIdMonedaPres.Text
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.idmonedaorig).Value = txtIdMonedaPres.Text
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.CodMoneda).Value = txtCodMonedaPres.Text
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.ValorCambio).Value = ValorCambioPres

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdProveedor).Value = idproveedor
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.NombreProveedor).Value = nombreproveedor

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMarca).Value = idmarca
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Marca).Value = nombremarca

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PlazoEntrega).Value = plazoentrega

        '                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Then
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0
        '                    End If

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ganancia).Value = FormatNumber(ganancia, 2)

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion1).Value = FormatNumber(0, 2)
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Bonificacion2).Value = FormatNumber(0, 2)

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value = FormatNumber(0, 2)
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = FormatNumber(0, 2)

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Iva).Value = 21

        '                    If codmoneda.ToUpper = "PE" And txtCodMonedaPres.Text.ToUpper = "DO" Then
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = Format(preciolista / ValorcambioDolar, "####0.00")
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta / ValorcambioDolar, "####0.00")
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = Format(preciovtaorig / ValorcambioDolar, "####0.00")
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = Format(gananciaorig / ValorcambioDolar, "####0.00")
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value * (iva / 100), "####0.00")
        '                    Else
        '                        If codmoneda.ToUpper = "DO" And txtCodMonedaPres.Text.ToUpper = "PE" Then
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = Format(preciolista * ValorcambioDolar, "####0.00")
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * ValorcambioDolar, "####0.00")
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = Format(preciovtaorig * ValorcambioDolar, "####0.00")
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = Format(gananciaorig * ValorcambioDolar, "####0.00")
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value * (iva / 100), "####0.00")
        '                        Else
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value = preciolista
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = CDbl(preciovta)
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = CDbl(preciovtaorig)
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.gananciaorig).Value = CDbl(gananciaorig)
        '                            grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.MontoIva).Value = montoiva
        '                        End If
        '                    End If

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Ubicacion).Value = Pasillo + " - " + Estante + " - " + Fila

        '                    If Pasillo.Length > 0 Then
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material).Style.BackColor = Color.LightGreen
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Material_Prov).Style.BackColor = Color.LightGreen
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Nombre_Material).Style.BackColor = Color.LightGreen
        '                    End If

        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.dateupd).Value = fecha

        '                    'si el stock es cero pintar de rojo..
        '                    'si el stock es mayor a cero y menor o igual al minimo pintar de amarillo..
        '                    'si el stock es mayor al minimo dejar en blanco..

        '                    If stock = 0 Then
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Red
        '                    ElseIf stock <= minimo Then
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.Yellow
        '                    Else
        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Stock).Style.BackColor = Color.White
        '                    End If

        '                    grdItems.Columns(ColumnasDelGridItems.Marca).ReadOnly = True

        '                    If grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = 0 Then
        '                        GoTo SinProveedor
        '                    End If

        '                    grdItems.CurrentCell = grdItems(ColumnasDelGridItems.Ganancia, grdItems.CurrentRow.Index)

        '                Else
        '                    cell.Value = "NO EXISTE"
        '                    Exit Sub
        '                End If
        '            End If

        '            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then
        '                'grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = False
        '                grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = False
        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value Then
        '                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.PrecioLista).Value = FormatNumber(0, 2)
        '                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Qty).Value = FormatNumber(0, 2)
        '                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.SubTotalProd).Value = FormatNumber(CDbl(0.0), 2)
        '                End If
        '                'Else
        '                'grdItems.Columns(ColumnasDelGridItems.Cod_Unidad).ReadOnly = True
        '                'grdItems.Columns(ColumnasDelGridItems.PrecioLista).ReadOnly = True
        '            End If

        '            If e.ColumnIndex = ColumnasDelGridItems.Nombre_Material And _
        '                grdItems.CurrentRow.Cells(ColumnasDelGridItems.Cod_Material).Value Is DBNull.Value Then

        '                Dim cell As DataGridViewCell = grdItems.CurrentCell
        '                Dim cod_unidad As String, nombreUNIDAD As String = "", codunidad As String = ""
        '                Dim idunidad As Long

        '                cod_unidad = "U"

        '                If ObtenerUnidad_App(cod_unidad, idunidad, nombreUNIDAD, codunidad, ConnStringSEI) = 0 Then
        '                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
        '                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Cod_Unidad).Value = cod_unidad
        '                    grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Unidad).Value = nombreUNIDAD

        '                    SendKeys.Send("{TAB}")

        '                Else
        '                    cell.Value = "NO EXISTE"
        '                    Exit Sub
        '                End If

        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.IdMoneda).Value = txtIdMonedaPres.Text
        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.CodMoneda).Value = txtCodMonedaPres.Text

        '                SendKeys.Send("{TAB}")

        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Iva).Value = 21
        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Bonificacion1).Value = 0
        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Bonificacion2).Value = 0
        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.Ganancia).Value = 0
        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.gananciaorig).Value = 0
        '                grdItems.Rows(grdItems.CurrentRow.Index).Cells(ColumnasDelGridItems.preciovtaorig).Value = 0
        '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0

        '            End If

        '            If e.ColumnIndex = ColumnasDelGridItems.Cod_Unidad Then

        '                Dim cell As DataGridViewCell = grdItems.CurrentCell
        '                Dim cod_unidad As String, codunidad As String = ""
        '                Dim idunidad As Long

        '                cod_unidad = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex).Value

        '                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)
        '                If ObtenerUnidad_App(cod_unidad, idunidad, nombre, codunidad, ConnStringSEI) = 0 Then
        '                    cell.Value = nombre
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IDUnidad).Value = idunidad
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Cod_Unidad).Value = cod_unidad
        '                    grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Unidad).Value = nombre

        '                    SendKeys.Send("{TAB}")

        '                Else
        '                    cell.Value = "NO EXISTE"
        '                    Exit Sub
        '                End If
        '            End If

        '            If e.ColumnIndex = ColumnasDelGridItems.PrecioLista _
        '                    Or e.ColumnIndex = ColumnasDelGridItems.Ganancia _
        '                    Or e.ColumnIndex = ColumnasDelGridItems.Bonificacion1 _
        '                    Or e.ColumnIndex = ColumnasDelGridItems.Bonificacion2 _
        '                    Or e.ColumnIndex = ColumnasDelGridItems.PorcRecDesc _
        '                    Or e.ColumnIndex = ColumnasDelGridItems.CodMoneda _
        '                    Or e.ColumnIndex = ColumnasDelGridItems.Iva Then
        'SinProveedor:

        '                Dim cell As DataGridViewCell = grdItems.CurrentCell

        '                nombre = ""
        '                ganancia = 0

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value Or _
        '                  grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value = 0
        '                End If

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion1).Value Is DBNull.Value Or _
        '                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion1).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion1).Value = 0
        '                End If

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion2).Value Is DBNull.Value Or _
        '                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion2).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Bonificacion2).Value = 0
        '                End If

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
        '                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0
        '                End If

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is DBNull.Value Or _
        '                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioLista).Value = 0
        '                End If

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value Is DBNull.Value Or _
        '                      grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value = 0
        '                End If

        '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioLista).Value
        '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.idmonedaorig).Value = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.IdMoneda).Value

        '                preciovta = grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.preciovtaorig).Value

        '                cell = grdItems.Rows(cell.RowIndex).Cells(cell.ColumnIndex + 1)

        '                If IIf(grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value Is DBNull.Value, 0, grdItems.CurrentRow.Cells(ColumnasDelGridItems.gananciaorig).Value Is DBNull.Value) <> IIf(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value, 0, grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value) Then
        '                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value Is DBNull.Value) And _
        '                                           Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value Is DBNull.Value) Then

        '                        ganancia = 1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Ganancia).Value / 100)

        '                        grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value = Format(ganancia * preciovta, "####0.00")

        '                        preciovta = grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value

        '                    End If

        '                End If


        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is DBNull.Value Or _
        '                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value Is Nothing Then
        '                    grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = preciovta
        '                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
        '                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * preciovta, "####0.00") 'CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * preciovta, 2))
        '                    End If
        '                Else
        '                    If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value = 0 Then
        '                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = preciovta
        '                    Else
        '                        If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value > 0 Then
        '                            grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), "####0.00") 'Math.Round(preciovta * (1 + (grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value / 100)), 2)
        '                        Else
        '                            If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value < 0 Then
        '                                grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value = Format(preciovta * (1 - ((grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value * -1) / 100)), "####0.00") ' Math.Round(preciovta * (1 - ((grdItems.CurrentRow.Cells(ColumnasDelGridItems.PorcRecDesc).Value * -1) / 100)), 2)
        '                            End If
        '                        End If
        '                    End If

        '                    If Not (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value) Then
        '                        grdItems.CurrentRow.Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, "####0.00") ' CDbl(FormatNumber(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value * grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value, 2))
        '                    End If
        '                End If

        '                grdItems.CurrentRow.Cells(ColumnasDelGridItems.MontoIva).Value = Format(grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value * (grdItems.CurrentRow.Cells(ColumnasDelGridItems.Iva).Value / 100), "####0.00")

        '                Contar_Filas()

        '            End If


        '            If e.ColumnIndex = ColumnasDelGridItems.Qty Then
        '                Dim cell As DataGridViewCell = grdItems.CurrentCell

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value Is DBNull.Value Then
        '                    Exit Sub
        '                Else
        '                    If CDbl(grdItems.CurrentRow.Cells(ColumnasDelGridItems.Qty).Value) = 0.0 Then
        '                        Exit Sub
        '                    End If
        '                End If

        '                If grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value Is DBNull.Value Then
        '                    Exit Sub
        '                Else
        '                    If CDbl(grdItems.CurrentRow.Cells(ColumnasDelGridItems.PrecioVta).Value) = 0.0 Then
        '                        Exit Sub
        '                    End If
        '                End If

        '                grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.SubTotalProd).Value = Format(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value, "####0.00") ' CDbl(FormatNumber(grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.Qty).Value * grdItems.Rows(cell.RowIndex).Cells(ColumnasDelGridItems.PrecioVta).Value, 2))

        '                Dim j As Integer = 0

        '                txtSubtotal.Text = "0"
        '                'txtSubtotalDO.Text = "0"

        '                For j = 0 To grdItems.Rows.Count - 2
        '                    If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value Is DBNull.Value) Then
        '                        If Not (grdItems.Rows(j).Cells(ColumnasDelGridItems.CodMoneda).Value = "") Then
        '                            txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(IIf(grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value Is DBNull.Value, 0, grdItems.Rows(j).Cells(ColumnasDelGridItems.SubTotalProd).Value))
        '                        End If
        '                    End If
        '                Next

        '                Contar_Filas()

        '            End If

        '        Catch ex As Exception
        '            ' MsgBox(ex.Message)
        '            'MsgBox("error en Sub grdItems_CellEndEdit", MsgBoxStyle.Critical, "Error")
        '        End Try

    End Sub

    Private Sub grdItems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)

        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        ' AddHandler validar.KeyPress, AddressOf validar_NumerosReales2

    End Sub

    Private Sub grdItems_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs)
        editando_celda = True
    End Sub

    Private Sub txtPrecioCosto_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecioCompra.KeyUp

        Try

            If txtPrecioCompra.Text <> "" Then

                CalcularPorcentaje_Precio(0)
                'cmbMayorista_SelectedIndexChanged(sender, e)
                'cmbMinorista_SelectedIndexChanged(sender, e)
                cmbMayoristaPeron_SelectedIndexChanged(sender, e)
                cmbMinoristaPeron_SelectedIndexChanged(sender, e)
            Else
                If chk2.Checked = False Then
                    txtPrecioMayorista.Text = "0.00"
                End If
                If chk1.Checked = False Then
                    txtPrecioMinorista.Text = "0.00"
                End If
                If chk3.Checked = False Then
                    txtPrecio3.Text = "0.00"
                End If
                If chk4.Checked = False Then
                    txtPrecio4.Text = "0.00"
                End If
                txtPrecioMinoristaPeron.Text = "0.00"
                txtPrecioMayoristaPeron.Text = "0.00"
            End If
        Catch ex As Exception

        End Try


    End Sub

    'Private Sub cmbMinorista_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMinorista.SelectedIndexChanged
    '    If band = 1 Then
    '        Try
    '            lblPorcen1.Text = ""
    '            txtIDMinorista.Text = cmbMinorista.SelectedValue

    '            ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT  Valor_Cambio,Porcentaje FROM Lista_Precios WHERE Codigo =  " & IIf(txtIDMinorista.Text = "", cmbMinorista.SelectedValue, txtIDMinorista.Text))
    '            ds_cmblista.Dispose()

    '            txtPrecioMinorista.Text = Math.Round(CDbl(txtPrecioCompra.Text) * CDbl(ds_cmblista.Tables(0).Rows(0).Item(0)), 2)
    '            lblPorcen1.Text = CInt(ds_cmblista.Tables(0).Rows(0).Item(1)).ToString

    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub

    'Private Sub cmbMayorista_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMayorista.SelectedIndexChanged

    '    If band = 1 Then
    '        Try
    '            lblPorcen2.Text = ""
    '            txtIDMayorista.Text = cmbMayorista.SelectedValue

    '            ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT  Valor_Cambio,Porcentaje FROM Lista_Precios WHERE Codigo =  " & IIf(txtIDMayorista.Text = "", cmbMayorista.SelectedValue, txtIDMayorista.Text))
    '            ds_cmblista.Dispose()

    '            txtPrecioMayorista.Text = Math.Round(CDbl(txtPrecioCompra.Text) * CDbl(ds_cmblista.Tables(0).Rows(0).Item(0)), 2)
    '            lblPorcen2.Text = CInt(ds_cmblista.Tables(0).Rows(0).Item(1)).ToString


    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub

    Private Sub cmbMinoristaPeron_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMinoristaPeron.SelectedIndexChanged
        If band = 1 Then
            Try

                lblPorcenPeronMin.Text = ""
                txtIDMinoristaPeron.Text = cmbMinoristaPeron.SelectedValue


                ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT  Valor_Cambio, Porcentaje FROM Lista_Precios WHERE Codigo =  " & IIf(txtIDMinoristaPeron.Text = "", cmbMinoristaPeron.SelectedValue, txtIDMinoristaPeron.Text))
                ds_cmblista.Dispose()

                txtPrecioMinoristaPeron.Text = Math.Round(CDbl(txtPrecioCompra.Text) * ds_cmblista.Tables(0).Rows(0).Item(0), 2)
                lblPorcenPeronMin.Text = Math.Round(ds_cmblista.Tables(0).Rows(0).Item(1), 2)

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmbMayoristaPeron_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMayoristaPeron.SelectedIndexChanged
        If band = 1 Then
            Try

                lblPorcenPeronMay.Text = ""
                txtIDMayoristaPeron.Text = cmbMayoristaPeron.SelectedValue

                ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT  Valor_Cambio,Porcentaje FROM Lista_Precios WHERE Codigo =  " & IIf(txtIDMayoristaPeron.Text = "", cmbMayoristaPeron.SelectedValue, txtIDMayoristaPeron.Text))
                ds_cmblista.Dispose()

                txtPrecioMayoristaPeron.Text = Math.Round(CDbl(txtPrecioCompra.Text) * ds_cmblista.Tables(0).Rows(0).Item(0), 2)
                lblPorcenPeronMay.Text = Math.Round(ds_cmblista.Tables(0).Rows(0).Item(1), 2)

            Catch ex As Exception

            End Try
        End If
    End Sub

    'Private Sub cmbLista3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLista3.SelectedIndexChanged
    '    If band = 1 Then
    '        Try
    '            lblPorcen3.Text = ""
    '            txtIDLista3.Text = cmbLista3.SelectedValue

    '            ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT  Valor_Cambio,Porcentaje FROM Lista_Precios WHERE Codigo =  " & IIf(txtIDLista3.Text = "", cmbLista3.SelectedValue, txtIDLista3.Text))
    '            ds_cmblista.Dispose()

    '            txtPrecio3.Text = Math.Round(CDbl(txtPrecioCompra.Text) * ds_cmblista.Tables(0).Rows(0).Item(0), 2)
    '            lblPorcen3.Text = CInt(ds_cmblista.Tables(0).Rows(0).Item(1)).ToString

    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub

    Private Sub cmbLista4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLista4.SelectedIndexChanged
        If band = 1 Then
            Try
                lblPorcen4.Text = ""
                txtIDLista4.Text = cmbLista4.SelectedValue

                ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "SELECT  Valor_Cambio,Porcentaje FROM Lista_Precios WHERE Codigo =  " & IIf(txtIDLista4.Text = "", cmbLista4.SelectedValue, txtIDLista4.Text))
                ds_cmblista.Dispose()


                Valor4 = ds_cmblista.Tables(0).Rows(0).Item(0)
                If chk4.Checked = False Then
                    txtPrecio4.Text = Math.Round(CDbl(txtPrecioCompra.Text) * ds_cmblista.Tables(0).Rows(0).Item(0), 2)
                End If
                Porcen4 = Math.Round(ds_cmblista.Tables(0).Rows(0).Item(1), 2)
                lblPorcen4.Text = Porcen4

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub chkUniRef_CheckedChanged(sender As Object, e As EventArgs) Handles chkUniRef.CheckedChanged

        rdKilo.Enabled = chkUniRef.Checked
        rdLitros.Enabled = chkUniRef.Checked
        rdUnidad.Enabled = chkUniRef.Checked
        Label8.Enabled = chkUniRef.Checked
        txtCantidadPACK.Enabled = chkUniRef.Checked
        chkCrearFR.Enabled = chkUniRef.Checked

        If chkUniRef.Checked = False Then
            Label15.Text = "Precio de Costo*"
            chkCrearFR.Checked = False
            rdKilo.Checked = False
            rdLitros.Checked = False
            rdUnidad.Checked = False
            txtCantidadPACK.Text = ""
        Else
            If rdKilo.Checked = True Then
                Label15.Text = "Precio de Costo (KG)*"
            Else
                If rdLitros.Checked = True Then
                    Label15.Text = "Precio de Costo (L)*"
                Else
                    If rdUnidad.Checked = True Then
                        Label15.Text = "Precio de Costo (U)*"
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub chk1_CheckedChanged(sender As Object, e As EventArgs) Handles chk1.CheckedChanged
        txtPrecioMinorista.Enabled = chk1.Checked
        'lista 1
        If chk1.Checked = True Then
            txtPrecioMinorista.BackColor = Color.Aquamarine
            txtPrecioMinorista.Focus()
        Else
            txtPrecioMinorista.BackColor = Color.White
        End If
        CalcularPorcentaje_Precio(1)
    End Sub

    Private Sub chk2_CheckedChanged(sender As Object, e As EventArgs) Handles chk2.CheckedChanged
        txtPrecioMayorista.Enabled = chk2.Checked
        'lista 2
        If chk2.Checked = True Then
            txtPrecioMayorista.BackColor = Color.Aquamarine
            txtPrecioMayorista.Focus()
        Else
            txtPrecioMayorista.BackColor = Color.White
        End If
        CalcularPorcentaje_Precio(2)
    End Sub

    Private Sub chk3_CheckedChanged(sender As Object, e As EventArgs) Handles chk3.CheckedChanged
        txtPrecio3.Enabled = chk3.Checked
        'lista 3
        If chk3.Checked = True Then
            txtPrecio3.BackColor = Color.Aquamarine
            txtPrecio3.Focus()
        Else
            txtPrecio3.BackColor = Color.White
        End If
        CalcularPorcentaje_Precio(3)
    End Sub

    Private Sub chk4_CheckedChanged(sender As Object, e As EventArgs) Handles chk4.CheckedChanged
        txtPrecio4.Enabled = chk4.Checked
        cmbLista4.Enabled = Not chk4.Checked
        'lista 4
        If chk4.Checked = True Then
            txtPrecio4.BackColor = Color.Aquamarine
            txtPrecio4.Focus()
        Else
            txtPrecio4.BackColor = Color.White
        End If
        CalcularPorcentaje_Precio(4)
    End Sub

    Private Sub chk1_MouseHover(sender As Object, e As EventArgs) Handles chk1.MouseHover
        ToolTip1.Show("Haga click para cambiar el precio de forma manual.", chk1)
    End Sub

    Private Sub chk2_MouseHover(sender As Object, e As EventArgs) Handles chk2.MouseHover
        ToolTip1.Show("Haga click para cambiar el precio de forma manual.", chk2)
    End Sub

    Private Sub chk3_MouseHover(sender As Object, e As EventArgs) Handles chk3.MouseHover
        ToolTip1.Show("Haga click para cambiar el precio de forma manual.", chk3)
    End Sub

    Private Sub chk4_MouseHover(sender As Object, e As EventArgs) Handles chk4.MouseHover
        ToolTip1.Show("Haga click para cambiar el precio de forma manual.", chk4)
    End Sub

    Private Sub txtPrecioMinorista_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecioMinorista.KeyUp
        If txtPrecioMinorista.Text <> "" Then
            If CDbl(txtPrecioMinorista.Text) >= CDbl(txtPrecioCompra.Text) Then
                CalcularPorcentaje_Precio(1)
            End If
        End If
    End Sub

    Private Sub txtPrecioMayorista_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecioMayorista.KeyUp
        If txtPrecioMayorista.Text <> "" Then
            If CDbl(txtPrecioMayorista.Text) >= CDbl(txtPrecioCompra.Text) Then
                CalcularPorcentaje_Precio(2)
            End If
        End If
    End Sub

    Private Sub txtPrecio3_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio3.KeyUp
        If txtPrecio3.Text <> "" Then
            If CDbl(txtPrecio3.Text) >= CDbl(txtPrecioCompra.Text) Then
                CalcularPorcentaje_Precio(3)
            End If
        End If
    End Sub

    Private Sub txtPrecio4_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio4.KeyUp
        If txtPrecio4.Text <> "" Then
            If CDbl(txtPrecio4.Text) >= CDbl(txtPrecioCompra.Text) Then
                CalcularPorcentaje_Precio(4)
            End If
        End If
    End Sub

    Private Sub rdKilo_CheckedChanged(sender As Object, e As EventArgs) Handles rdKilo.CheckedChanged

        If rdKilo.Checked = True Then
            Label15.Text = "Precio de Costo (KG)*"

        End If

    End Sub

    Private Sub rdLitros_CheckedChanged(sender As Object, e As EventArgs) Handles rdLitros.CheckedChanged

        If rdLitros.Checked = True Then
            Label15.Text = "Precio de Costo (L)*"
        End If

    End Sub

    Private Sub rdUnidad_CheckedChanged(sender As Object, e As EventArgs) Handles rdUnidad.CheckedChanged
        If rdUnidad.Checked = True Then
            Label15.Text = "Precio de Costo (U)*"
        End If
    End Sub

    Private Sub PicExcelExportar_Click(sender As Object, e As EventArgs) Handles PicExcelExportar.Click

        Dim Expo As New frmMaterialesExport
        Expo.ShowDialog()

        '  Dim ds_Empresa As Data.DataSet
        '' Cambiamos el cursor por el de carga
        '  Me.Cursor = Cursors.WaitCursor

        '  ' Conexi�n con la base de datos
        '  Dim connection As SqlClient.SqlConnection = Nothing

        '  Try
        '      connection = SqlHelper.GetConnection(ConnStringSEI)
        '  Catch ex As Exception
        '      MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '      Exit Sub
        '  End Try

        '  Try
        '      ' Creamos todo lo necesario para un excel
        '      Dim appXL As Excel.Application
        '      Dim wbXl As Excel.Workbook
        '      Dim shXL As Excel.Worksheet
        '      Dim indice As Integer = 2
        '      appXL = CreateObject("Excel.Application")
        '      appXL.Visible = False 'Para que no se muestre mientras se crea
        '      wbXl = appXL.Workbooks.Add
        '      shXL = wbXl.ActiveSheet
        '      ' A�adimos las cabeceras de las columnas con formato en negrita
        '      Dim formatRange As Excel.Range
        '      formatRange = shXL.Range("a1")
        '      formatRange.EntireRow.Font.Bold = True
        '      shXL.Cells(1, 1).Value = "CODIGO"
        '      shXL.Cells(1, 2).Value = "MARCA"
        '      shXL.Cells(1, 3).Value = "NOMBRE"
        '      shXL.Cells(1, 4).Value = "RUBRO"
        '      shXL.Cells(1, 5).Value = "UNIDAD"
        '      shXL.Cells(1, 6).Value = "COSTO"
        '      shXL.Cells(1, 7).Value = "MAYORISTA"
        '      shXL.Cells(1, 8).Value = "REVENDEDOR"
        '      shXL.Cells(1, 9).Value = "YAMILA"
        '      shXL.Cells(1, 10).Value = "LISTA 4"
        '      shXL.Cells(1, 11).Value = "CANTIDAD X PAQUETE"
        '      shXL.Cells(1, 12).Value = "UNIDAD DE REF."

        '      ' Obtenemos los datos que queremos exportar desde base de datos.
        '      Dim sqlstring As String = "exec spMateriales_Select_All_Excel 0,0,0,0"
        '      ds_Empresa = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
        '      ds_Empresa.Dispose()

        '      Dim Fila As Integer

        '      Fila = 0

        '      For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1
        '          ' Cargamos la informaci�n en el excel
        '          shXL.Cells(indice, 1).Value = ds_Empresa.Tables(0).Rows(Fila)(0)
        '          shXL.Cells(indice, 2).Value = ds_Empresa.Tables(0).Rows(Fila)(1)
        '          shXL.Cells(indice, 3).Value = ds_Empresa.Tables(0).Rows(Fila)(2)
        '          shXL.Cells(indice, 4).Value = ds_Empresa.Tables(0).Rows(Fila)(3)
        '          shXL.Cells(indice, 5).Value = ds_Empresa.Tables(0).Rows(Fila)(4)
        '          shXL.Cells(indice, 6).Value = ds_Empresa.Tables(0).Rows(Fila)(5)
        '          shXL.Cells(indice, 7).Value = ds_Empresa.Tables(0).Rows(Fila)(6)
        '          shXL.Cells(indice, 8).Value = ds_Empresa.Tables(0).Rows(Fila)(7)
        '          shXL.Cells(indice, 9).Value = ds_Empresa.Tables(0).Rows(Fila)(8)
        '          shXL.Cells(indice, 10).Value = ds_Empresa.Tables(0).Rows(Fila)(9)
        '          shXL.Cells(indice, 11).Value = ds_Empresa.Tables(0).Rows(Fila)(10)
        '          shXL.Cells(indice, 12).Value = ds_Empresa.Tables(0).Rows(Fila)(11)

        '          indice += 1
        '      Next


        '      ' Mostramos un dialog para que el usuario indique donde quiere guardar el excel
        '      Dim saveFileDialog1 As New SaveFileDialog()
        '      saveFileDialog1.Title = "Guardar documento Excel"
        '      saveFileDialog1.Filter = "Excel File|*.xls"
        '      saveFileDialog1.FileName = "Materiales " + Format(Date.Now, "dd-MM-yyyy").ToString
        '      saveFileDialog1.ShowDialog()
        '      ' Guardamos el excel en la ruta que ha especificado el usuario
        '      wbXl.SaveAs(saveFileDialog1.FileName)
        '      ' Cerramos el workbook
        '      appXL.Workbooks.Close()
        '      ' Eliminamos el objeto excel
        '      appXL.Quit()
        '  Catch ex As Exception
        '      MessageBox.Show("Error al exportar los datos a excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '  Finally
        '      ' Cerramos la conexi�n y ponemos el cursor por defecto de nuevo
        '      ' conexion.Close()
        '      Me.Cursor = Cursors.Arrow
        '  End Try

    End Sub

    Private Sub PicExcelExportar_MouseHover(sender As Object, e As EventArgs) Handles PicExcelExportar.MouseHover
        'ToolTip1.Show("Haga click para exportar la lista de producto a un archivo excel", PicExcelExportar)
    End Sub

#End Region



#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Lista de Productos"

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

        txtCODIGO.Tag = "1"
        cmbMarca.Tag = "2"
        txtNOMBRE.Tag = "3"
        txtFamilia.Tag = "4"
        'txtidAlmacen.Tag = "2"

        cmbFAMILIAS.Tag = "5"
        cmbUNIDADES.Tag = "7"

        txtPrecioCompra.Tag = "8"
        txtPrecioMinorista.Tag = "9"
        txtPrecioMayorista.Tag = "10"
        txtPrecio3.Tag = "11"
        txtPrecio4.Tag = "12"
        txtPrecioMinoristaPeron.Tag = "13"
        txtPrecioMayoristaPeron.Tag = "14"

        txtganancia.Tag = "15"
        txtMinimo.Tag = "16"
        txtMaximo.Tag = "17"
        'lblStockActual.Tag = "16"
        txtFechaUpd.Tag = "18"

        txtCodBarra.Tag = "19"
        txtPasillo.Tag = "20"
        txtEstante.Tag = "21"
        txtFila.Tag = "22"
        chkControlStock.Tag = "23"


        txtIdUnidad.Tag = "6"
        txtIdMarca.Tag = "24"
        txtCantidadPACK.Tag = "25"

        chk1.Tag = "33"
        chk2.Tag = "34"
        chk3.Tag = "35"
        chk4.Tag = "36"
        chkVentaMayorista.Tag = "37"



    End Sub

    Private Sub Verificar_Datos()
        bolpoliticas = False

        If txtMinimo.Text = "" Then 'Or txtMinimo.Text = "0" Then
            txtMinimo.Text = "1"
        Else
            If CDbl(txtMinimo.Text) = 0 Then
                txtMinimo.Text = "1"
            End If
        End If

        If txtMaximo.Text = "" Then 'Or txtMaximo.Text = "0" Then
            txtMaximo.Text = "1"
        Else
            If CDbl(txtMaximo.Text) = 0 Then
                txtMaximo.Text = "1"
            End If
        End If

        If chkUniRef.Checked = True Then
            If rdKilo.Checked = False And rdLitros.Checked = False And rdUnidad.Checked = False Then
                Util.MsgStatus(Status1, "Por favor seleccione una unidad de referencia.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "Por favor seleccione una unidad de referencia.", My.Resources.Resources.alert.ToBitmap, True)
                rdKilo.Focus()
                Exit Sub
            End If

            If txtCantidadPACK.Enabled = False Then
                If txtCantidadPACK.Text = "" Then
                    txtCantidadPACK.Text = "0"
                End If
            Else
                If CDbl(txtCantidadPACK.Text) = 0 Then
                    Util.MsgStatus(Status1, "El campo Cantidad de referencia debe ser mayor a cero.", My.Resources.Resources.alert.ToBitmap)
                    Util.MsgStatus(Status1, "El campo Cantidad de referencia debe ser mayor a cero.", My.Resources.Resources.alert.ToBitmap, True)
                    txtCantidadPACK.Focus()
                    Exit Sub
                End If
            End If
        Else
            txtCantidadPACK.Text = "0"
        End If

        If chkCrearFR.Checked Then
            If cmbUNIDADES.SelectedValue = "KG" Or cmbUNIDADES.SelectedValue = "LITROS" Then
                Util.MsgStatus(Status1, "No se puede crear un fragmentado con la unidad seleccionada. Por favor verifique el dato.", My.Resources.Resources.alert.ToBitmap)
                Util.MsgStatus(Status1, "No se puede crear un fragmentado con la unidad seleccionada. Por favor verifique el dato.", My.Resources.Resources.alert.ToBitmap, True)
                cmbUNIDADES.Focus()
                Exit Sub
            End If
        End If
        'If txtMaximo.Text = "" Or txtMaximo.Text = "0" Then
        '    Util.MsgStatus(Status1, "El campo m�ximo est� vac�o o en Cero.", My.Resources.Resources.alert.ToBitmap)
        '    Util.MsgStatus(Status1, "El campo m�ximo est� vac�o o en Cero.", My.Resources.Resources.alert.ToBitmap, True)
        '    txtMaximo.Focus()
        '    Exit Sub
        'End If

        'If CType(txtMinimo.Text, Long) > CType(txtMaximo.Text, Long) Then
        '    Util.MsgStatus(Status1, "El campo M�nimo no puede ser mayor al campo M�ximo.", My.Resources.Resources.alert.ToBitmap)
        '    Util.MsgStatus(Status1, "El campo M�nimo no puede ser mayor al campo M�ximo.", My.Resources.Resources.alert.ToBitmap, True)
        '    txtMaximo.Focus()
        '    Exit Sub
        'End If

        'Dim i As Integer, j As Integer, filas As Integer ', state As Integer
        'Dim filas As Integer

        Dim codigo, nombre, nombrelargo, tipo, observaciones As String 'ubicacion,
        codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : observaciones = ""  'ubicacion = ""

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Verificar si se termin� de editar la celda...
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If editando_celda Then
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edici�n, antes de guardar.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Use [Enter] o [Tab] para salir del modo edici�n, antes de guardar.", My.Resources.Resources.alert.ToBitmap, True)
            Exit Sub
        End If

        'controlo que el precio de compra no este vac�o
        If txtPrecioCompra.Text.ToString = "" Then
            Util.MsgStatus(Status1, "El precio de costo es no puede estar vac�o. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio de costo es no puede estar vac�o.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecioCompra.Focus()
            Exit Sub
        End If
        'controlo los valores de precio
        If CDbl(txtPrecioCompra.Text) <= 0 Then
            Util.MsgStatus(Status1, "El precio de costo es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio de costo es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecioCompra.Focus()
            Exit Sub
        End If
        If CDbl(txtPrecioMinorista.Text) <= 0 Then
            Util.MsgStatus(Status1, "El precio Lista 1 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio Lista 1 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecioMinorista.Focus()
            Exit Sub
        End If
        If CDbl(txtPrecioMayorista.Text) <= 0 Then
            Util.MsgStatus(Status1, "El precio Lista 2 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio Lista 2 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecioMayorista.Focus()
            Exit Sub
        End If
        If CDbl(txtPrecioMinoristaPeron.Text) <= 0 Then
            Util.MsgStatus(Status1, "El precio Minorista Per�n es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio Minorista Per�n es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecioMinoristaPeron.Focus()
            Exit Sub
        End If
        If CDbl(txtPrecioMayoristaPeron.Text) <= 0 Then
            Util.MsgStatus(Status1, "El precio Mayorista Per�n es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio Mayorista Per�n es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecioMayoristaPeron.Focus()
            Exit Sub
        End If
        If txtPrecio3.Text <= 0 Then
            Util.MsgStatus(Status1, "El precio Lista 3 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio Lista 3 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecio3.Focus()
            Exit Sub
        End If
        If txtPrecio4.Text <= 0 Then
            Util.MsgStatus(Status1, "El precio Lista 4 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "El precio Lista 4 es incorrecto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
            txtPrecio4.Focus()
            Exit Sub
        End If


        ''controlo solo si se esta cargando un producto nuevo 
        'If bolModo = True Then
        '    'control que el stock no este vacio
        '    If (grdItemsStock.Rows(0).Cells(0).Value Is DBNull.Value) And grdItemsStock.Rows(0).Cells(1).Value Is DBNull.Value Then
        '        Util.MsgStatus(Status1, "El stock principal y per�n ingresado no es correcto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
        '        Util.MsgStatus(Status1, "El stock principal y per�n ingresado no es correcto. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
        '        grdItemsStock.Rows(0).Cells(0).Selected = True
        '        Exit Sub
        '    End If

        '    If grdItemsStock.Rows(0).Cells(0).Value = 0 And grdItemsStock.Rows(0).Cells(1).Value = 0 Then
        '        Util.MsgStatus(Status1, "El stock principal y per�n ingresado es igual Cero. Por favor verifique.", My.Resources.Resources.alert.ToBitmap)
        '        Util.MsgStatus(Status1, "El stock principal y per�n ingresado es igual Cero. Por favor verifique.", My.Resources.Resources.alert.ToBitmap, True)
        '        grdItemsStock.Rows(0).Selected = True
        '        Exit Sub
        '    End If
        'End If





        bolpoliticas = True

    End Sub

    Private Sub LlenarcmbProveedores()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Proveedores As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Proveedores = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo, rtrim(nombre) as Nombre FROM Proveedores WHERE Eliminado = 0 ORDER BY nombre")
            ds_Proveedores.Dispose()

            If ds_Proveedores.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            With Me.cmbProveedoresCompra.ComboBox
                .DataSource = ds_Proveedores.Tables(0).DefaultView
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub LlenarcmbMarcasCompra()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT codigo, rtrim(nombre) as Marca FROM Marcas")
            ds_Marcas.Dispose()

            With Me.cmbMarcaCompra.ComboBox
                .DataSource = ds_Marcas.Tables(0).DefaultView
                .DisplayMember = "Marca"
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub LlenarcmbListaMinorista()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbMinorista
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbListaMayorista()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbMayorista
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbListaMinoristaPeron()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbMinoristaPeron
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbListaMayoristaPeron()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbMayoristaPeron
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbLista3()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbLista3
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbLista4()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_listas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_listas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Descripcion) as Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds_listas.Dispose()

            With Me.cmbLista4
                .DataSource = ds_listas.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarcmbRubros()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, (codigo + ' - ' +  rtrim(nombre)) as Nombre FROM Familias WHERE Eliminado = 0 ORDER BY nombre")
            ds_Rubros.Dispose()

            If ds_Rubros.Tables(0).Rows.Count > 0 Then
                With cmbFAMILIAS
                    .DataSource = ds_Rubros.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .ValueMember = "Codigo"
                    '.AutoCompleteMode = AutoCompleteMode.Suggest
                    '.AutoCompleteSource = AutoCompleteSource.ListItems
                    '.DropDownStyle = ComboBoxStyle.DropDown
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbUnidades()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, (codigo + ' - ' +  rtrim(nombre)) as Nombre FROM Unidades WHERE Eliminado = 0 ORDER BY nombre")
            ds_Rubros.Dispose()

            If ds_Rubros.Tables(0).Rows.Count > 0 Then
                With cmbUNIDADES
                    .DataSource = ds_Rubros.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .ValueMember = "Codigo"
                    '.AutoCompleteMode = AutoCompleteMode.Suggest
                    '.AutoCompleteSource = AutoCompleteSource.ListItems
                    '.DropDownStyle = ComboBoxStyle.DropDown
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbMarcas()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(nombre) as Nombre FROM Marcas WHERE Eliminado = 0 ORDER BY nombre")
            ds_Rubros.Dispose()

            If ds_Rubros.Tables(0).Rows.Count > 0 Then
                With cmbMarca
                    .DataSource = ds_Rubros.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .ValueMember = "Codigo"
                    '.AutoCompleteMode = AutoCompleteMode.Suggest
                    '.AutoCompleteSource = AutoCompleteSource.ListItems
                    '.DropDownStyle = ComboBoxStyle.DropDown
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

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

        ' 24/11/15
        'Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ListaPrecios]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
        '                    "   DROP TABLE [listaPrecios];" & _
        '                    " SELECT * INTO ListaPrecios FROM OPENROWSET ('Microsoft.ACE.OLEDB.12.0', " & _
        '                    "'Excel 12.0 Xml;HDR=YES;Database=" + SLibro + "', " & _
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
        '                    "      End" & _
        '                    "   SET @Cant = 0; " & _
        '                    "   select @Cant = count(*) FROM INFORMATION_SCHEMA.COLUMNS AS c1 " & _
        '                    "       where c1.column_name = 'descripci�n' and c1.table_name = 'listaprecios' " & _
        '                    "   IF @Cant > 0 " & _
        '                    "       BEGIN " & _
        '                    "           EXEC sp_rename 'sei.dbo.listaprecios.descripci�n', 'Descripcion', 'COLUMN';	 " & _
        '                    "       END "


        'Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ListaPrecios]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
        '                    "   DROP TABLE [listaPrecios];" & _
        '                    " SELECT * INTO ListaPrecios FROM OPENROWSET ('Microsoft.Jet.OLEDB.4.0', " & _
        '                    "'Excel 8.0;Database=" + SLibro + ";IMEX=1', " & _
        '                    "'SELECT * FROM [Lista =S=]'); " & _
        '                    " ALTER TABLE ListaPrecios ADD IdMoneda BIGINT; " & _
        '                    " ALTER TABLE ListaPrecios ADD IdMaterial BIGINT; " & _
        '                    " ALTER TABLE ListaPrecios ADD IdMarca BIGINT ;"

        Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ListaPrecios]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
                            "   DROP TABLE [ListaPrecios];" & _
                            " SELECT * INTO ListaPrecios FROM OPENROWSET ('Microsoft.ACE.OLEDB.12.0', " & _
                            "'Excel 12.0 Xml;HDR=YES;Database=" + SLibro + "', " & _
                            "'SELECT * FROM [Lista =S=$]'); " & _
                            " ALTER TABLE ListaPrecios ADD IdMoneda BIGINT; " & _
                            " ALTER TABLE ListaPrecios ADD IdMaterial BIGINT; " & _
                            " ALTER TABLE ListaPrecios ADD IdMarca BIGINT ;" & _
                            "   DECLARE @cant as INT; " & _
                            "	SELECT @Cant = count(*) " & _
                            "	FROM  [listaPrecios] " & _
                            "	IF @Cant > 0 " & _
                            "       BEGIN " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Schneider Electric]', 'Referencia', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F2]', 'Descripcion', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F3]', 'QI', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F4]', 'IVA', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F5]', 'Moneda', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Lista de Precios]', 'Precio_Lista', 'COLUMN'; " & _
                            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F7]', 'Precio_Neto', 'COLUMN'; " & _
                            "           UPDATE LISTAPRECIOS SET Precio_Lista = REPLACE(Precio_Lista, ',', '.'); " & _
                            "           UPDATE LISTAPRECIOS SET Precio_Neto = REPLACE(Precio_Neto, ',', '.'); " & _
                            "           DELETE FROM sei.dbo.listaprecios WHERE UPPER(Referencia) = 'REFERENCIA' and UPPER(Descripcion) = 'DESCRIPCI�N'; " & _
                            "           ALTER TABLE ListaPrecios ALTER COLUMN precio_neto DECIMAL(18,2); " & _
                            "           ALTER TABLE ListaPrecios ALTER COLUMN precio_lista DECIMAL(18,2); " & _
                            "      End"




        'Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ListaPrecios]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
        '            "   DROP TABLE [ListaPrecios];" & _
        '            " SELECT * INTO ListaPrecios FROM OPENROWSET ('Microsoft.Jet.OLEDB.4.0', " & _
        '            "'Excel 8.0;Database=" + SLibro + ";IMEX=1', " & _
        '            "'SELECT * FROM [Lista =S=$]'); " & _
        '            " ALTER TABLE ListaPrecios ADD IdMoneda BIGINT; " & _
        '            " ALTER TABLE ListaPrecios ADD IdMaterial BIGINT; " & _
        '            " ALTER TABLE ListaPrecios ADD IdMarca BIGINT ;" & _
        '            "   DECLARE @cant as INT; " & _
        '            "	SELECT @Cant = count(*) " & _
        '            "	FROM  [listaPrecios] " & _
        '            "	IF @Cant > 0 " & _
        '            "       BEGIN " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Schneider Electric]', 'Referencia', 'COLUMN'; " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F2]', 'Descripcion', 'COLUMN'; " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F3]', 'QI', 'COLUMN'; " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F4]', 'IVA', 'COLUMN'; " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F5]', 'Moneda', 'COLUMN'; " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[Lista de Precios]', 'Precio_Lista', 'COLUMN'; " & _
        '            "           EXEC sp_rename '[sei].[dbo].[ListaPrecios].[F7]', 'Precio_Neto', 'COLUMN'; " & _
        '            "           DELETE FROM sei.dbo.listaprecios WHERE Referencia = 'Referencia' and Descripcion = 'descripci�n'; " & _
        '            "      End"

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Verifique los siguiente puntos: " & vbCrLf & vbCrLf & _
              "1) El archivo Excel debe estar cerrado" & vbCrLf & _
              "2) El archivo de Excel debe tener una hoja que se llama ""Lista de Precios""" & vbCrLf & _
              "3) Cumple con los requisitos de nombres en cada columna. " & vbCrLf & _
              " Puede consultar el ejemplo haciendo clic en el �cono que est� a la derecha de ""Importar Productos desde Excel""." & vbCrLf & vbCrLf & _
              " Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)

            CargarExcel2 = False

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function CargarExcel_ActualizarPreciosOCSch(ByVal SLibro As String, ByVal sHoja As String) As Boolean

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

        Dim cs As String = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PreciosActOCSch]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" & _
                            "   DROP TABLE [PreciosActOCSch];" & _
                            " SELECT * INTO PreciosActOCSch FROM OPENROWSET ('Microsoft.ACE.OLEDB.12.0', " & _
                            "'Excel 12.0 Xml;HDR=YES;Database=" + SLibro + "', " & _
                            "'SELECT * FROM [Lista =S=$]'); " & _
                            " ALTER TABLE PreciosActOCSch ADD IdMoneda BIGINT; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[Schneider Electric]', 'Referencia', 'COLUMN'; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[F2]', 'Descripcion', 'COLUMN'; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[F3]', 'QI', 'COLUMN'; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[F4]', 'IVA', 'COLUMN'; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[F5]', 'Moneda', 'COLUMN'; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[Lista de Precios]', 'PrecioLista', 'COLUMN'; " & _
                            " EXEC sp_rename '[sei].[dbo].[PreciosActOCSch].[F7]', 'PrecioNeto', 'COLUMN'; " & _
                            " DELETE FROM PreciosActOCSch WHERE REFERENCIA IS NULL AND DESCRIPCION IS NULL; " & _
                            " DELETE FROM PreciosActOCSch WHERE REFERENCIA = 'REFERENCIA'" & _
                            " UPDATE PreciosActOCSch SET QI = REPLACE(QI, ',', '.'); " & _
                            " UPDATE PreciosActOCSch SET IVA = REPLACE(IVA, ',', '.'); " & _
                            " UPDATE PreciosActOCSch SET PrecioLista = REPLACE(PRECIOLISTA, ',', '.'); " & _
                            " UPDATE PreciosActOCSch SET PRECIONETO = REPLACE(PRECIONETO, ',', '.'); " & _
                            " ALTER TABLE PreciosActOCSch ALTER COLUMN precioneto DECIMAL(18,2); " & _
                            " ALTER TABLE PreciosActOCSch ALTER COLUMN QI DECIMAL(18,2); " & _
                            " ALTER TABLE PreciosActOCSch ALTER COLUMN IVA DECIMAL(18,2); " & _
                            " ALTER TABLE PreciosActOCSch ALTER COLUMN preciolista DECIMAL(18,2) "

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, cs)
            ds.Dispose()

            CargarExcel_ActualizarPreciosOCSch = True

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Verifique los siguiente puntos: " & vbCrLf & vbCrLf & _
              "1) El archivo Excel debe estar cerrado" & vbCrLf & _
              "2) El archivo de Excel debe tener una hoja que se llama ""Lista de Precios""" & vbCrLf & _
              "3) Cumple con los requisitos de nombres en cada columna. " & vbCrLf & _
              " Puede consultar el ejemplo haciendo clic en el �cono que est� a la derecha de ""Importar Productos desde Excel""." & vbCrLf & vbCrLf & _
              " Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)

            CargarExcel_ActualizarPreciosOCSch = False

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Sub Buscar_GananciaxDefecto()
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ISNULL(gananciaxdefecto,0) AS GananciaxDefecto FROM Parametros")

            txtgananciaoriginal.Text = IIf(ds.Tables(0).Rows(0)(0) Is Nothing, 0, ds.Tables(0).Rows(0)(0))
            txtganancia.Text = IIf(ds.Tables(0).Rows(0)(0) Is Nothing, 0, ds.Tables(0).Rows(0)(0))

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub Traer_ValorCambio()

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_ValorCambio As Data.DataSet


        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try


        ds_ValorCambio = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Valor_Cambio FROM Lista_Precios WHERE Descripcion = 'REVENDEDOR' AND Eliminado = 0")
        ds_ValorCambio.Dispose()

        If ds_ValorCambio.Tables(0).Rows.Count = 0 Then
            ValorCambio = 0.0
            Exit Sub
        Else
            ValorCambio = ds_ValorCambio.Tables(0).Rows(0).Item(0)
        End If

    End Sub

    Private Sub LlenarGridItemStock()

        If grdItemsStock.Columns.Count > 0 Then
            grdItemsStock.Columns.Clear()
        End If

        If txtID.Text = "" Then
            SQL = "SELECT 0.00 as StockPrincipal,  0.00 as StockPeron"

            Try
                grdItemsStock.ReadOnly = False

            Catch ex As Exception

            End Try
        

        Else
            'SQL = "exec spMateriales_Stock_ALmacen @IdMaterial = '" & txtCODIGO.Text & "'"
            SQL = "SELECT A.Nombre as 'ALMACEN', Qty as 'STOCK' FROM STOCK S JOIN Almacenes A ON S.IDALMACEN = A.Codigo WHERE IDMaterial ='" & txtCODIGO.Text & "'"

            Try
                grdItemsStock.Columns(0).Width = 120
                grdItemsStock.Columns(0).ReadOnly = True
                grdItemsStock.Columns(1).Width = 120
            Catch ex As Exception

            End Try

        End If

        GetDatasetItems()

        Try
            'grdItemsStock.Columns(0).Visible = False
            ''grdItemsStock.Columns(1).Visible = False
            ''grdItemsStock.Columns(2).Visible = False
            'grdItemsStock.Columns(3).Visible = False

            'grdItemsStock.Columns(1).Width = 120
            'grdItemsStock.Columns(2).Width = 120

            If bolModo Then
                grdItemsStock.Rows(1).ReadOnly = True
            End If

        Catch ex As Exception

        End Try





        With grdItemsStock
            .VirtualMode = False
            '.AllowUserToAddRows = True
            .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.BackColor = Color.White
            .AllowUserToOrderColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ForeColor = Color.Black
        End With

        With grdItemsStock.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Black  'Color.BlueViolet
            .ForeColor = Color.White
            .Font = New Font("TAHOMA", 10, FontStyle.Bold)
        End With

        grdItemsStock.Font = New Font("TAHOMA", 10, FontStyle.Regular)


        'If grdItemsStock.Rows.Count > 0 Then
        '    grdItemsStock.Columns(0).Width = 200
        '    grdItemsStock.Columns(1).Width = 200
        'End If

        'grdEnsayos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        'Volver la fuente de datos a como estaba...

        SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = 0"

    End Sub

    Private Sub GetDatasetItems()
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()
            'If desdegrid = True Then
            'grdItems.DataSource = ds_2.Tables(0).DefaultView
            'Else
            grdItemsStock.DataSource = ds_2.Tables(0).DefaultView
            'End If

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub CargarlistaPrincipales()

        ds_cmblista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "select Codigo,Porcentaje,Valor_Cambio from lista_precios where Codigo in (3,4,5)")
        ds_cmblista.Dispose()

        txtIDMinorista.Text = ds_cmblista.Tables(0).Rows(0)(0).ToString
        PorcenMayo = Math.Round(ds_cmblista.Tables(0).Rows(0)(1), 2)
        lblPorcen1.Text = PorcenMayo
        ValorMayo = ds_cmblista.Tables(0).Rows(0)(2).ToString

        txtIDMayorista.Text = ds_cmblista.Tables(0).Rows(1)(0).ToString
        PorcenReven = Math.Round(ds_cmblista.Tables(0).Rows(1)(1), 2)
        lblPorcen2.Text = PorcenReven
        ValorReven = ds_cmblista.Tables(0).Rows(1)(2)

        txtIDLista3.Text = ds_cmblista.Tables(0).Rows(2)(0)
        PorcenYami = Math.Round(ds_cmblista.Tables(0).Rows(2)(1), 2)
        lblPorcen3.Text = PorcenYami
        ValorYami = ds_cmblista.Tables(0).Rows(2)(2)

    End Sub

    Private Sub CalcularPorcentaje_Precio(ByVal chk As Integer)
        Dim calculo As Double
        Try
            'me fijo si selecciono algun chk
            Select Case chk
                Case 1
                    If chk1.Checked = False Then
                        txtPrecioMinorista.Text = Math.Round(ValorMayo * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen1.Text = PorcenMayo
                    Else
                        calculo = Math.Round((CDbl(txtPrecioMinorista.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen1.Text = Math.Round(calculo - 100, 2)
                    End If
                Case 2
                    If chk2.Checked = False Then
                        txtPrecioMayorista.Text = Math.Round(ValorReven * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen2.Text = PorcenReven
                    Else
                        calculo = Math.Round((CDbl(txtPrecioMayorista.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen2.Text = Math.Round(calculo - 100, 2)
                    End If
                Case 3
                    If chk3.Checked = False Then
                        txtPrecio3.Text = Math.Round(ValorYami * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen3.Text = PorcenYami
                    Else
                        calculo = Math.Round((CDbl(txtPrecio3.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen3.Text = Math.Round(calculo - 100, 2)
                    End If
                Case 4
                    If chk4.Checked = False Then
                        txtPrecio4.Text = Math.Round(Valor4 * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen4.Text = Porcen4
                    Else
                        calculo = Math.Round((CDbl(txtPrecio4.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen4.Text = Math.Round(calculo - 100, 2)
                    End If
                Case Else
                    'Aplico a todos los chk
                    If chk1.Checked = False Then
                        txtPrecioMinorista.Text = Math.Round(ValorMayo * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen1.Text = PorcenMayo
                    Else
                        calculo = Math.Round((CDbl(txtPrecioMinorista.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen1.Text = Math.Round(calculo - 100, 2)
                    End If
                    If chk2.Checked = False Then
                        txtPrecioMayorista.Text = Math.Round(ValorReven * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen2.Text = PorcenReven
                    Else
                        calculo = Math.Round((CDbl(txtPrecioMayorista.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen2.Text = Math.Round(calculo - 100, 2)
                    End If
                    If chk3.Checked = False Then
                        txtPrecio3.Text = Math.Round(ValorYami * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen3.Text = PorcenYami
                    Else
                        calculo = Math.Round((CDbl(txtPrecio3.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen3.Text = Math.Round(calculo - 100, 2)
                    End If
                    If chk4.Checked = False Then
                        txtPrecio4.Text = Math.Round(Valor4 * CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen4.Text = Porcen4
                    Else
                        calculo = Math.Round((CDbl(txtPrecio4.Text) * 100) / CDbl(txtPrecioCompra.Text), 2)
                        lblPorcen4.Text = Math.Round(calculo - 100, 2)
                    End If
            End Select

        Catch ex As Exception

        End Try


    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarActualizar_Registro(ByVal CrearFR As Boolean) As Integer

        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        If CrearFR = False Then
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            'Abrir una transaccion para guardar y asegurar que se guarda todo
            If Abrir_Tran(connection) = False Then
                MessageBox.Show("No se pudo abrir una transaccion", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
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

            Dim param_idfamilia As New SqlClient.SqlParameter
            param_idfamilia.ParameterName = "@idfamilia"
            param_idfamilia.SqlDbType = SqlDbType.VarChar
            param_idfamilia.Size = 25
            param_idfamilia.Value = IIf(txtIdRubro.Text = "", cmbFAMILIAS.SelectedValue, txtIdRubro.Text)
            param_idfamilia.Direction = ParameterDirection.Input

            Dim param_idmarca As New SqlClient.SqlParameter
            param_idmarca.ParameterName = "@idmarca"
            param_idmarca.SqlDbType = SqlDbType.VarChar
            param_idmarca.Size = 25
            param_idmarca.Value = IIf(txtIdMarca.Text = "", cmbMarca.SelectedValue, txtIdMarca.Text)
            param_idmarca.Direction = ParameterDirection.Input

            Dim param_idAlmacen As New SqlClient.SqlParameter
            param_idAlmacen.ParameterName = "@idAlmacen"
            param_idAlmacen.SqlDbType = SqlDbType.BigInt
            param_idAlmacen.Value = Utiles.numero_almacen
            param_idAlmacen.Direction = ParameterDirection.Input

            Dim param_idunidad As New SqlClient.SqlParameter
            param_idunidad.ParameterName = "@idunidad"
            param_idunidad.SqlDbType = SqlDbType.VarChar
            param_idunidad.Size = 25
            If CrearFR = False Then
                param_idunidad.Value = IIf(txtIdUnidad.Text = "", cmbUNIDADES.SelectedValue, txtIdUnidad.Text)
            Else
                param_idunidad.Value = IIf(rdKilo.Checked, "KG", "LITROS")
            End If
            param_idunidad.Direction = ParameterDirection.Input

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            If bolModo = True Then
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.Output
            Else
                param_codigo.Value = txtCODIGO.Text
                param_codigo.Direction = ParameterDirection.Input
            End If


            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.NVarChar
            param_nombre.Size = 4000
            If CrearFR Then
                txtNOMBRE.Text = txtNOMBRE.Text.ToString + "**FR"
            End If
            param_nombre.Value = RTrim(LTrim(txtNOMBRE.Text))
            param_nombre.Direction = ParameterDirection.Input

            Dim param_preciocosto As New SqlClient.SqlParameter
            param_preciocosto.ParameterName = "@preciocosto"
            param_preciocosto.SqlDbType = SqlDbType.Decimal
            param_preciocosto.Precision = 18
            param_preciocosto.Scale = 2
            param_preciocosto.Value = txtPrecioMinorista.Text
            param_preciocosto.Direction = ParameterDirection.Input

            Dim param_preciocompra As New SqlClient.SqlParameter
            param_preciocompra.ParameterName = "@preciocompra"
            param_preciocompra.SqlDbType = SqlDbType.Decimal
            param_preciocompra.Precision = 18
            param_preciocompra.Scale = 2
            param_preciocompra.Value = txtPrecioCompra.Text
            param_preciocompra.Direction = ParameterDirection.Input

            Dim param_precioPeron As New SqlClient.SqlParameter
            param_precioPeron.ParameterName = "@precioPeron"
            param_precioPeron.SqlDbType = SqlDbType.Decimal
            param_precioPeron.Precision = 18
            param_precioPeron.Scale = 2
            param_precioPeron.Value = txtPrecioMinoristaPeron.Text
            param_precioPeron.Direction = ParameterDirection.Input

            Dim param_preciomayorista As New SqlClient.SqlParameter
            param_preciomayorista.ParameterName = "@preciomayorista"
            param_preciomayorista.SqlDbType = SqlDbType.Decimal
            param_preciomayorista.Precision = 18
            param_preciomayorista.Scale = 2
            param_preciomayorista.Value = txtPrecioMayorista.Text
            param_preciomayorista.Direction = ParameterDirection.Input


            Dim param_preciomayoristaperon As New SqlClient.SqlParameter
            param_preciomayoristaperon.ParameterName = "@preciomayoristaperon"
            param_preciomayoristaperon.SqlDbType = SqlDbType.Decimal
            param_preciomayoristaperon.Precision = 18
            param_preciomayoristaperon.Scale = 2
            param_preciomayoristaperon.Value = txtPrecioMayoristaPeron.Text
            param_preciomayoristaperon.Direction = ParameterDirection.Input


            Dim param_idListaMayorista As New SqlClient.SqlParameter
            param_idListaMayorista.ParameterName = "@idlistamayorista"
            param_idListaMayorista.SqlDbType = SqlDbType.BigInt
            param_idListaMayorista.Value = txtIDMayorista.Text
            ' param_idListaMayorista.Value = IIf(txtIDMayorista.Text = "", cmbMayorista.SelectedValue, txtIDMayorista.Text)
            param_idListaMayorista.Direction = ParameterDirection.Input

            Dim param_idListaMayoristaPeron As New SqlClient.SqlParameter
            param_idListaMayoristaPeron.ParameterName = "@idlistamayoristaperon"
            param_idListaMayoristaPeron.SqlDbType = SqlDbType.BigInt
            param_idListaMayoristaPeron.Value = IIf(txtIDMayoristaPeron.Text = "", cmbMayoristaPeron.SelectedValue, txtIDMayoristaPeron.Text)
            param_idListaMayoristaPeron.Direction = ParameterDirection.Input

            Dim param_idListaMinorista As New SqlClient.SqlParameter
            param_idListaMinorista.ParameterName = "@idlistaminorista"
            param_idListaMinorista.SqlDbType = SqlDbType.BigInt
            param_idListaMinorista.Value = txtIDMinorista.Text
            'param_idListaMinorista.Value = IIf(txtIDMinorista.Text = "", cmbMinorista.SelectedValue, txtIDMinorista.Text)
            param_idListaMinorista.Direction = ParameterDirection.Input

            Dim param_idListaMinoristaPeron As New SqlClient.SqlParameter
            param_idListaMinoristaPeron.ParameterName = "@idlistaminoristaperon"
            param_idListaMinoristaPeron.SqlDbType = SqlDbType.BigInt
            param_idListaMinoristaPeron.Value = IIf(txtIDMinoristaPeron.Text = "", cmbMinoristaPeron.SelectedValue, txtIDMinoristaPeron.Text)
            param_idListaMinoristaPeron.Direction = ParameterDirection.Input

            Dim param_ganancia As New SqlClient.SqlParameter
            param_ganancia.ParameterName = "@ganancia"
            param_ganancia.SqlDbType = SqlDbType.Decimal
            param_ganancia.Precision = 18
            param_ganancia.Scale = 2
            param_ganancia.Value = txtganancia.Text
            param_ganancia.Direction = ParameterDirection.Input

            Dim param_minimo As New SqlClient.SqlParameter
            param_minimo.ParameterName = "@minimo"
            param_minimo.SqlDbType = SqlDbType.Decimal
            param_minimo.Precision = 18
            param_minimo.Scale = 2
            param_minimo.Value = txtMinimo.Text
            param_minimo.Direction = ParameterDirection.Input

            Dim param_maximo As New SqlClient.SqlParameter
            param_maximo.ParameterName = "@maximo"
            param_maximo.SqlDbType = SqlDbType.Decimal
            param_maximo.Precision = 18
            param_maximo.Scale = 4
            param_maximo.Value = txtMaximo.Text
            param_maximo.Direction = ParameterDirection.Input

            Dim param_stockinicial As New SqlClient.SqlParameter
            Dim param_stockinicialperon As New SqlClient.SqlParameter
            If bolModo = True Then
                param_stockinicial.ParameterName = "@stockinicial"
                param_stockinicial.SqlDbType = SqlDbType.Decimal
                param_stockinicial.Precision = 18
                param_stockinicial.Scale = 2

                param_stockinicialperon.ParameterName = "@stockinicialPeron"
                param_stockinicialperon.SqlDbType = SqlDbType.Decimal
                param_stockinicialperon.Precision = 18
                param_stockinicialperon.Scale = 2

                If CrearFR = False Then
                    param_stockinicial.Value = IIf(grdItemsStock.Rows(0).Cells(0).Value.ToString = "", 0, grdItemsStock.Rows(0).Cells(0).Value)
                    param_stockinicialperon.Value = IIf(grdItemsStock.Rows(0).Cells(1).Value.ToString = "", 0, grdItemsStock.Rows(0).Cells(1).Value)
                Else
                    param_stockinicial.Value = 0
                    param_stockinicialperon.Value = 0
                End If

                param_stockinicial.Direction = ParameterDirection.Input
                param_stockinicialperon.Direction = ParameterDirection.Input
            End If

            Dim param_CodBarra As New SqlClient.SqlParameter
            param_CodBarra.ParameterName = "@CodBarra"
            param_CodBarra.SqlDbType = SqlDbType.VarChar
            param_CodBarra.Size = 50
            param_CodBarra.Value = txtCodBarra.Text
            param_CodBarra.Direction = ParameterDirection.Input

            Dim param_Pasillo As New SqlClient.SqlParameter
            param_Pasillo.ParameterName = "@Pasillo"
            param_Pasillo.SqlDbType = SqlDbType.VarChar
            param_Pasillo.Size = 50
            param_Pasillo.Value = txtPasillo.Text
            param_Pasillo.Direction = ParameterDirection.Input

            Dim param_Estante As New SqlClient.SqlParameter
            param_Estante.ParameterName = "@Estante"
            param_Estante.SqlDbType = SqlDbType.VarChar
            param_Estante.Size = 50
            param_Estante.Value = txtEstante.Text
            param_Estante.Direction = ParameterDirection.Input

            Dim param_Fila As New SqlClient.SqlParameter
            param_Fila.ParameterName = "@Fila"
            param_Fila.SqlDbType = SqlDbType.VarChar
            param_Fila.Size = 50
            param_Fila.Value = txtFila.Text
            param_Fila.Direction = ParameterDirection.Input

            Dim param_ControlStock As New SqlClient.SqlParameter
            param_ControlStock.ParameterName = "@ControlStock"
            param_ControlStock.SqlDbType = SqlDbType.Bit
            param_ControlStock.Value = chkControlStock.Checked
            param_ControlStock.Direction = ParameterDirection.Input

            Dim param_cantidadpack As New SqlClient.SqlParameter
            param_cantidadpack.ParameterName = "@cantidadpack"
            param_cantidadpack.SqlDbType = SqlDbType.Decimal
            param_cantidadpack.Precision = 18
            param_cantidadpack.Scale = 2
            param_cantidadpack.Value = txtCantidadPACK.Text
            param_cantidadpack.Direction = ParameterDirection.Input

            Dim param_preciolista3 As New SqlClient.SqlParameter
            param_preciolista3.ParameterName = "@preciolista3"
            param_preciolista3.SqlDbType = SqlDbType.Decimal
            param_preciolista3.Precision = 18
            param_preciolista3.Scale = 2
            param_preciolista3.Value = txtPrecio3.Text
            param_preciolista3.Direction = ParameterDirection.Input

            Dim param_idlista3 As New SqlClient.SqlParameter
            param_idlista3.ParameterName = "@idlista3"
            param_idlista3.SqlDbType = SqlDbType.BigInt
            param_idlista3.Value = txtIDLista3.Text
            'param_idlista3.Value = IIf(txtIDLista3.Text = "", cmbLista3.SelectedValue, txtIDLista3.Text)
            param_idlista3.Direction = ParameterDirection.Input

            Dim param_preciolista4 As New SqlClient.SqlParameter
            param_preciolista4.ParameterName = "@preciolista4"
            param_preciolista4.SqlDbType = SqlDbType.Decimal
            param_preciolista4.Precision = 18
            param_preciolista4.Scale = 2
            param_preciolista4.Value = txtPrecio4.Text
            param_preciolista4.Direction = ParameterDirection.Input

            Dim param_idlista4 As New SqlClient.SqlParameter
            param_idlista4.ParameterName = "@idlista4"
            param_idlista4.SqlDbType = SqlDbType.BigInt
            param_idlista4.Value = IIf(txtIDLista4.Text = "", cmbLista4.SelectedValue, txtIDLista4.Text)
            param_idlista4.Direction = ParameterDirection.Input

            Dim param_unidadref As New SqlClient.SqlParameter
            param_unidadref.ParameterName = "@unidadref"
            param_unidadref.SqlDbType = SqlDbType.VarChar
            param_unidadref.Size = 25
            If chkUniRef.Checked And CrearFR = False Then
                If rdKilo.Checked Then
                    param_unidadref.Value = "KG"
                End If
                If rdLitros.Checked Then
                    param_unidadref.Value = "LITROS"
                End If
                If rdUnidad.Checked Then
                    param_unidadref.Value = "U"
                End If
            Else
                param_unidadref.Value = ""
            End If
            param_unidadref.Direction = ParameterDirection.Input

            Dim param_cambiar1 As New SqlClient.SqlParameter
            param_cambiar1.ParameterName = "@cambiar1"
            param_cambiar1.SqlDbType = SqlDbType.Bit
            param_cambiar1.Value = chk1.Checked
            param_cambiar1.Direction = ParameterDirection.Input

            Dim param_cambiar2 As New SqlClient.SqlParameter
            param_cambiar2.ParameterName = "@cambiar2"
            param_cambiar2.SqlDbType = SqlDbType.Bit
            param_cambiar2.Value = chk2.Checked
            param_cambiar2.Direction = ParameterDirection.Input

            Dim param_cambiar3 As New SqlClient.SqlParameter
            param_cambiar3.ParameterName = "@cambiar3"
            param_cambiar3.SqlDbType = SqlDbType.Bit
            param_cambiar3.Value = chk3.Checked
            param_cambiar3.Direction = ParameterDirection.Input

            Dim param_cambiar4 As New SqlClient.SqlParameter
            param_cambiar4.ParameterName = "@cambiar4"
            param_cambiar4.SqlDbType = SqlDbType.Bit
            param_cambiar4.Value = chk4.Checked
            param_cambiar4.Direction = ParameterDirection.Input

            Dim param_ventamayorista As New SqlClient.SqlParameter
            param_ventamayorista.ParameterName = "@VentaMayorista"
            param_ventamayorista.SqlDbType = SqlDbType.Bit
            param_ventamayorista.Value = chkVentaMayorista.Checked
            param_ventamayorista.Direction = ParameterDirection.Input

            Dim param_mensaje As New SqlClient.SqlParameter
            param_mensaje.ParameterName = "@MENSAJE"
            param_mensaje.SqlDbType = SqlDbType.VarChar
            param_mensaje.Size = 50
            param_mensaje.Value = DBNull.Value
            param_mensaje.Direction = ParameterDirection.InputOutput

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

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_Insert", _
                                    param_id, param_idmarca, param_idfamilia, param_idunidad, param_unidadref, _
                                    param_codigo, param_nombre, param_ganancia, param_preciocosto, param_preciocompra, param_precioPeron, _
                                    param_preciomayorista, param_preciomayoristaperon, param_idListaMayorista, param_idListaMayoristaPeron, _
                                    param_idListaMinorista, param_idListaMinoristaPeron, param_preciolista3, param_idlista3, param_preciolista4, param_idlista4, _
                                    param_minimo, param_maximo, param_stockinicial, param_stockinicialperon, param_CodBarra, param_Pasillo, _
                                    param_cambiar1, param_cambiar2, param_cambiar3, param_cambiar4, param_ventamayorista, _
                                    param_Estante, param_Fila, param_ControlStock, param_cantidadpack, param_useradd, param_idAlmacen, param_res)

                    txtID.Text = param_id.Value
                    txtCODIGO.Text = param_codigo.Value
                    res = param_res.Value

                Else

                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spMateriales_Update", _
                                  param_id, param_idmarca, param_idfamilia, param_idunidad, param_unidadref, _
                                  param_codigo, param_nombre, param_ganancia, param_preciocosto, param_preciocompra, param_precioPeron, _
                                  param_preciomayorista, param_preciomayoristaperon, param_idListaMayorista, param_idListaMayoristaPeron, _
                                  param_idListaMinorista, param_idListaMinoristaPeron, param_preciolista3, param_idlista3, param_preciolista4, param_idlista4, _
                                  param_minimo, param_maximo, param_CodBarra, param_Pasillo, _
                                  param_Estante, param_Fila, param_ControlStock, param_cantidadpack, _
                                  param_cambiar1, param_cambiar2, param_cambiar3, param_cambiar4, param_ventamayorista, _
                                  param_useradd, param_mensaje, param_res)

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

            If ex.Message.ToString.Contains("UNIQUE KEY") Then
                AgregarActualizar_Registro = -11
            Else
                MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Function

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                If res > 0 Then

                    If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then

                        Try
                            Dim sqlstring As String

                            sqlstring = "UPDATE [dbo].[Materiales] SET [Eliminado] = 1 WHERE Codigo = '" & txtCODIGO.Text & "'"
                            tranWEB.Sql_Set(sqlstring)

                        Catch ex As Exception
                            'MsgBox(ex.Message)
                            MsgBox("No se puede actualizar en la Web la lista de Materiales. Ejecute el bot�n sincronizar para actualizar el servidor WEB.")
                        End Try

                    End If

                    Util.BorrarGrilla(grd)

                End If


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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function ImportarRegistros(ByVal ListaPrecio As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                If ListaPrecio = "Tablerista" Then
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Importar_Tablerista", param_res, _
                                              param_cantNuevo, param_cantAct, param_mensaje)
                Else
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Importar_Distribuidor", param_res, _
                                          param_cantNuevo, param_cantAct, param_mensaje)
                End If

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Private Function ImportarRegistros_ActualizarPreciosOCSch() As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.Output

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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_ActualizarPrecios_OCSch", param_res, _
                                              param_cantAct, param_mensaje)

                'MsgBox(param_mensaje.Value)

                ImportarRegistros_ActualizarPreciosOCSch = param_res.Value

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

#End Region

#Region "   Botones"

    Protected Overloads Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click

        '        If UserActual.ToLower <> "administrador" Then
        '            Util.Logueado_OK = False
        '            Dim login As New Utiles.Login

        'logindenuevo:

        '            login.ShowDialog()

        '            If Not Util.Logueado_OK Then
        '                GoTo logindenuevo
        '            End If
        '        End If
        Try
            If MDIPrincipal.sucursal.ToUpper.Contains("PRINCIPAL") And (MDIPrincipal.EmpleadoLogueado = "12" Or MDIPrincipal.EmpleadoLogueado = "13" Or MDIPrincipal.EmpleadoLogueado = "2") Then
                Dim sqlstring As String = "update NotificacionesWEB set BloqueoM = 0"
                tranWEB.Sql_Set(sqlstring)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If txtID.Text <> "" Then

            btnCancelar.Text = "Cancelar"

            CargaContinua = False

            ' LlenarGridItems()
            LlenarGridItemStock()
        End If
        'limpio stockdeinicio y lo deshabilito
        txtStockInicio.Text = ""
        txtStockInicio.Enabled = False
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        grdItemsStock.Enabled = True

        Dim gananciaoriginal As Double

        If txtgananciaoriginal.Text = "" Then
            gananciaoriginal = 0
        Else
            gananciaoriginal = txtgananciaoriginal.Text
        End If

        If CargaContinua = False Then
            Util.LimpiarTextBox(Me.Controls)
        Else
            txtID.Text = ""
            txtCODIGO.Text = ""
        End If



        txtgananciaoriginal.Text = gananciaoriginal

        'cmbNombre.Text = MDIPrincipal.UltBusquedaMat

        Label20.Enabled = True
        txtStockInicio.Enabled = True

        'Label12.Enabled = True
        'cmbAlmacenes.Enabled = True

        lblStockActual.Text = "0"
        'lblMejorGanancia.Text = "0"
        'lblMejorPrecio.Text = "0"
        'lblPrecioIva10.Text = "0"
        'lblPrecioIva21.Text = "0"

        txtCodBarra.Text = ""

        'If CargaContinua = False Then
        '    PrepararGridItems()
        'End If

        'chkOcultarBonif.Checked = True

        If CargaContinua = False Then
            'cmbAlmacenes.SelectedValue = 1
            cmbMarca.SelectedIndex = 0
            cmbUNIDADES.SelectedValue = "U"
            chkControlStock.Checked = False
            cmbFAMILIAS.SelectedIndex = 0
            'cmbMinorista.SelectedIndex = 0
            'cmbMayorista.SelectedIndex = 0
            'cmbLista3.SelectedIndex = 0
            cmbLista4.SelectedIndex = 0
            'en peron dejo como predeterminado revendedor
            cmbMinoristaPeron.SelectedValue = 4
            cmbMayoristaPeron.SelectedIndex = 0
        End If


        CargarlistaPrincipales()
        'LlenarGridItems()
        LlenarGridItemStock()
        txtCODIGO.Focus()
        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        If bolModo = True Then
            If ControlarNombreProducto(txtNOMBRE.Text, ConnStringSEI) >= 1 Then
                Util.MsgStatus(Status1, "El producto """ & txtNOMBRE.Text & """ ya existe. Por favor, verifique la carga que est� realizando", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El producto """ & txtNOMBRE.Text & """ ya existe. Por favor, verifique la carga que est� realizando", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If
        End If

        If bolModo = False Then
            If MessageBox.Show("Est� seguro que desea modificar el Producto seleccionado?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        'If cmbUNIDADES.SelectedValue = "KG" Then 'Or cmbUNIDADES.SelectedValue = "TIRA" Then
        '    If MessageBox.Show("Est�  modificando o creando un Producto fragmentado?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        '        txtNOMBRE.Text = txtNOMBRE.Text.ToString + "**FR"
        '    End If
        'End If
        If chkFraccionados.Checked Then
            If Not txtNOMBRE.Text.Contains("**FR") Then
                txtNOMBRE.Text = txtNOMBRE.Text.ToString + "**FR"
            End If
        End If

        If txtganancia.Text = "" Then
            txtganancia.Text = "18"
            'If MessageBox.Show("El sistema ingresara el valor predeterminado para el campo Ganancia ( " & txtgananciaoriginal.Text & "%). Desea continuar?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '    txtganancia.Text = txtgananciaoriginal.Text
            'Else
            '    Exit Sub
            'End If
        End If

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        Dim ModoActual As Boolean

        ModoActual = bolModo

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarActualizar_Registro(False)
                Select Case res
                    Case -15
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo modificar el producto fragmentado asociado. Por favor, verifique esta informaci�n.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo modificar el producto fragmentado asociado. Por favor, verifique esta informaci�n.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case -11
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "Est� intentando ingresar un Material cuyo c�digo ya existe en el sistema. Por favor, verifique esta informaci�n.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "Est� intentando ingresar un Material cuyo c�digo ya existe en el sistema. Por favor, verifique esta informaci�n.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case -10
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se puede modificar el material. La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se puede modificar el material. La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                        cmbUNIDADES.Focus()
                        Exit Sub
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "El c�digo ingresado ya existe, por favor, cambie el c�digo", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "El c�digo ingresado ya existe, por favor, cambie el c�digo", My.Resources.Resources.stop_error.ToBitmap, True)
                        txtNOMBRE.Focus()
                        Exit Sub
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case Else
                        If chkCrearFR.Checked = False Then
                            Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                            Cerrar_Tran()
                        Else
                            res = AgregarActualizar_Registro(True)
                            Select Case res
                                Case -11
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "Est� intentando ingresar un Material cuyo c�digo ya existe en el sistema. Por favor, verifique esta informaci�n.", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "Est� intentando ingresar un Material cuyo c�digo ya existe en el sistema. Por favor, verifique esta informaci�n.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case -10
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se puede modificar el material. La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se puede modificar el material. La unidad ingresada es diferente a la unidad dentro de los movimientos de Stock.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    cmbUNIDADES.Focus()
                                    Exit Sub
                                Case -3
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "El c�digo ingresado ya existe, por favor, cambie el c�digo", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "El c�digo ingresado ya existe, por favor, cambie el c�digo", My.Resources.Resources.stop_error.ToBitmap, True)
                                    txtNOMBRE.Focus()
                                    Exit Sub
                                Case -1
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case 0
                                    Cancelar_Tran()
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                                    Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Exit Sub
                                Case Else
                                    Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                                    Cerrar_Tran()
                            End Select
                        End If

                        bolModo = False

                        If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then

                            Try
                                If chkUniRef.Checked = True Then
                                    If rdKilo.Checked = True Then
                                        txtUnidadRef.Text = "KG"
                                    End If
                                    If rdLitros.Checked = True Then
                                        txtUnidadRef.Text = "LITROS"
                                    End If
                                    If rdUnidad.Checked = True Then
                                        txtUnidadRef.Text = "U"
                                    End If
                                Else
                                    txtUnidadRef.Text = ""
                                End If

                                Dim sqlstring As String

                                If ModoActual = True Then
                                    sqlstring = "INSERT INTO [dbo].[Materiales] (ID, [IdMarca],[IdFamilia],[IDUnidad],[Codigo],[Nombre], " & _
                                            " [Ganancia], [PrecioCosto], [PrecioCompra], [PrecioPeron], [Minimo], [Maximo], " & _
                                            " [PrecioMayorista], [PrecioMayoristaPeron], [IDListaMayorista] , [IDListaMayoristaPeron] , [IDListaMinorista] , [IDListaMinoristaPeron] ," & _
                                            " [CodigoBarra], [Eliminado], [Pasillo], [Estante], [Fila], [ControlStock], [CantidadPACK], [DATEADD],[PrecioLista3],[IDLista3],[PrecioLista4],[IDLista4],[UnidadRef], " & _
                                            "[Cambiar1],[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista],[ActualizadoLocal])" & _
                                            "  values ( " & txtID.Text & ", '" & IIf(txtIdMarca.Text = "", cmbMarca.SelectedValue.ToString, txtIdMarca.Text) & "', '" & _
                                            IIf(txtIdRubro.Text = "", cmbFAMILIAS.SelectedValue.ToString, txtIdRubro.Text) & "', '" & IIf(txtIdUnidad.Text = "", cmbUNIDADES.SelectedValue.ToString, txtIdUnidad.Text) & "', '" & _
                                            txtCODIGO.Text & "', '" & txtNOMBRE.Text & "', " & IIf(txtganancia.Text = "", 0, txtganancia.Text) & ", " & txtPrecioMinorista.Text & ", " & _
                                            txtPrecioCompra.Text & ", " & txtPrecioMinoristaPeron.Text & ", " & IIf(txtMinimo.Text = "", 0, txtMinimo.Text) & ", " & IIf(txtMaximo.Text = "", 0, txtMaximo.Text) & ", " & _
                                            txtPrecioMayorista.Text & ", " & txtPrecioMayoristaPeron.Text & ", " & txtIDMayorista.Text & ", " & _
                                            IIf(txtIDMayoristaPeron.Text = "", cmbMayoristaPeron.SelectedValue, txtIDMayoristaPeron.Text) & ", " & txtIDMinorista.Text & ", " & _
                                            IIf(txtIDMinoristaPeron.Text = "", cmbMinoristaPeron.SelectedValue, txtIDMinoristaPeron.Text) & ", '" & txtCodBarra.Text & "', 0, '" & txtPasillo.Text & "', '" & txtEstante.Text & "', '" & txtFila.Text & "', " & _
                                            IIf(chkControlStock.Checked = True, 1, 0) & ", " & IIf(txtCantidadPACK.Text = "", 0, txtCantidadPACK.Text) & ", '" & _
                                            Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "', " & txtPrecio3.Text & "," & txtIDLista3.Text & "," & _
                                            txtPrecio4.Text & "," & IIf(txtIDLista4.Text = "", cmbLista4.SelectedValue, txtIDLista4.Text) & ",'" & txtUnidadRef.Text & "'," & IIf(chk1.Checked = True, 1, 0) & "," & IIf(chk2.Checked = True, 1, 0) & "," & _
                                            IIf(chk3.Checked = True, 1, 0) & "," & IIf(chk4.Checked = True, 1, 0) & "," & IIf(chkVentaMayorista.Checked = True, 1, 0) & ",0)"

                                Else
                                    sqlstring = "UPDATE [dbo].[Materiales] SET [IdMarca] = '" & IIf(txtIdMarca.Text = "", cmbMarca.SelectedValue.ToString, txtIdMarca.Text) & "', " & _
                                            " [IdFamilia] = '" & IIf(txtIdRubro.Text = "", cmbFAMILIAS.SelectedValue.ToString, txtIdRubro.Text) & "', " & _
                                            " [IDUnidad] = '" & IIf(txtIdUnidad.Text = "", cmbUNIDADES.SelectedValue.ToString, txtIdUnidad.Text) & "', " & _
                                            " [Codigo] = '" & txtCODIGO.Text & "', " & _
                                            " [Nombre] = '" & txtNOMBRE.Text & "', " & _
                                            " [Ganancia] = " & IIf(txtganancia.Text = "", 0, txtganancia.Text) & ", " & _
                                            " [PrecioCosto] = " & txtPrecioMinorista.Text & ", " & _
                                            " [PrecioCompra] = " & txtPrecioCompra.Text & ", " & _
                                            " [PrecioPeron] = " & txtPrecioMinoristaPeron.Text & ", " & _
                                            " [PrecioMayorista] = " & txtPrecioMayorista.Text & "," & _
                                            " [PrecioMayoristaPeron] = " & txtPrecioMayoristaPeron.Text & "," & _
                                            " [IDListaMayorista] = " & txtIDMayorista.Text & ", " & _
                                            " [IDListaMayoristaPeron] = " & IIf(txtIDMayoristaPeron.Text = "", cmbMayoristaPeron.SelectedValue, txtIDMayoristaPeron.Text) & ", " & _
                                            " [IDListaMinorista] = " & txtIDMinorista.Text & ", " & _
                                            " [IDListaMinoristaPeron] = " & IIf(txtIDMinoristaPeron.Text = "", cmbMinoristaPeron.SelectedValue, txtIDMinoristaPeron.Text) & ", " & _
                                            " [Minimo] = " & IIf(txtMinimo.Text = "", 0, txtMinimo.Text) & ", " & _
                                            " [Maximo] = " & IIf(txtMaximo.Text = "", 0, txtMaximo.Text) & ", " & _
                                            " [CodigoBarra] = '" & txtCodBarra.Text & "', " & _
                                            " [Pasillo] = '" & txtPasillo.Text & "', " & _
                                            " [Estante] = '" & txtEstante.Text & "', " & _
                                            " [Fila] = '" & txtFila.Text & "', " & _
                                            " [ControlStock] = " & IIf(chkControlStock.Checked = True, 1, 0) & ", " & _
                                            " [CantidadPACK] = " & IIf(txtCantidadPACK.Text = "", 0, txtCantidadPACK.Text) & ", " & _
                                            " [DATEUPD] = '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "'," & _
                                            " [PrecioLista3] = " & txtPrecio3.Text & "," & _
                                            " [IDLista3] = " & txtIDLista3.Text & "," & _
                                            " [PrecioLista4] = " & txtPrecio4.Text & "," & _
                                            " [IDLista4] = " & IIf(txtIDLista4.Text = "", cmbLista4.SelectedValue, txtIDLista4.Text) & "," & _
                                            " [UnidadRef] = '" & txtUnidadRef.Text & "'," & _
                                            " [Cambiar1] = " & IIf(chk1.Checked = True, 1, 0) & "," & _
                                            " [Cambiar2] = " & IIf(chk2.Checked = True, 1, 0) & "," & _
                                            " [Cambiar3] = " & IIf(chk3.Checked = True, 1, 0) & "," & _
                                            " [Cambiar4] = " & IIf(chk4.Checked = True, 1, 0) & "," & _
                                            " [VentaMayorista] = " & IIf(chkVentaMayorista.Checked = True, 1, 0) & "," & _
                                            " [ActualizadoLocal] = 0 " & _
                                            " WHERE Codigo = '" & txtCODIGO.Text & "'"
                                End If

                                tranWEB.Sql_Set(sqlstring)

                                If ModoActual = True Then
                                    sqlstring = " insert into [Stock] ( idmaterial, idalmacen, qty, idunidad) values ( '" & _
                                                 txtCODIGO.Text & "', 1, " & IIf(grdItemsStock.Rows(0).Cells(0).Value.ToString = "", 0, grdItemsStock.Rows(0).Cells(0).Value) & ", '" & _
                                                 IIf(txtIdUnidad.Text = "", cmbUNIDADES.SelectedValue.ToString, txtIdUnidad.Text.ToString) & "') "

                                    tranWEB.Sql_Set(sqlstring)

                                    sqlstring = " insert into [Stock] ( idmaterial, idalmacen, qty, idunidad) values ( '" & _
                                                  txtCODIGO.Text & "', 2, " & IIf(grdItemsStock.Rows(0).Cells(1).Value.ToString = "", 0, grdItemsStock.Rows(0).Cells(1).Value) & ", '" & _
                                                  IIf(txtIdUnidad.Text = "", cmbUNIDADES.SelectedValue.ToString, txtIdUnidad.Text.ToString) & "') "

                                    tranWEB.Sql_Set(sqlstring)

                                Else

                                    sqlstring = "update stock set idunidad = m.IDUnidad from stock s join materiales m on s.idmaterial = m.Codigo where IDMaterial = '" & txtCODIGO.Text & "'"

                                    tranWEB.Sql_Set(sqlstring)

                                End If

                                sqlstring = "update notificacionesWEB set Materiales = 1"
                                tranWEB.Sql_Set(sqlstring)

                                'MsgBox("Al Toque Roque!")

                            Catch ex As Exception
                                'MsgBox(ex.Message)
                                MsgBox("No se puede actualizar en la Web la Lista de Precios actual. Ejecute el bot�n sincronizar para actualizar el servidor WEB." + ex.Message)
                            End Try

                        End If
                        MDIPrincipal.NoActualizarBase = False
                        btnActualizar_Click(sender, e)
                        grd.Rows(0).Selected = True
                        grd.Rows(0).Cells(1).Value = txtCODIGO.Text

                End Select
                'borro txt de stock de inicio y lo deshabilito 
                txtStockInicio.Text = ""
                txtStockInicio.Enabled = False
                grdItemsStock.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCargaContinua_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargaContinua.Click
        If btnNuevo.Enabled = False And btnCancelar.Text = "Cancelar" Then
            MsgBox("No se puede Guardar cuando est� en Modo Nuevo", MsgBoxStyle.Critical, "Control de Productos")
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

        If MessageBox.Show("Est� seguro que desea eliminar el producto: " & grd.CurrentRow.Cells(3).Value & " ?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
            res = EliminarRegistro()
            Select Case res
                Case -6
                    Util.MsgStatus(Status1, "No se pudo borrar el registro **FR asociado.", My.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case -5
                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case -2
                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                    Exit Sub
                Case 0
                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                    Exit Sub
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

    'Private Sub btnGuardarCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim res As Integer = 0
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Try
    '        Try
    '            connection = SqlHelper.GetConnection(ConnStringSEI)
    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try

    '        Try

    '            Dim param_busqueda As New SqlClient.SqlParameter
    '            param_busqueda.ParameterName = "@texto"
    '            param_busqueda.SqlDbType = SqlDbType.VarChar
    '            param_busqueda.Size = 200
    '            param_busqueda.Value = cmbNombre.Text
    '            param_busqueda.Direction = ParameterDirection.Input

    '            Try
    '                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCriteriosdeBusqueda_Insert", _
    '                                          param_busqueda)

    '                Util.MsgStatus(Status1, "Se guard� correctamente el criterio", My.Resources.Resources.ok.ToBitmap)

    '                LlenarcmbNombre()

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

    '        MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '          + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
    '          "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try
    'End Sub

    'Private Sub btnEliminarCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim res As Integer = 0
    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Try
    '        Try
    '            connection = SqlHelper.GetConnection(ConnStringSEI)
    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try

    '        Try

    '            Dim param_busqueda As New SqlClient.SqlParameter
    '            param_busqueda.ParameterName = "@texto"
    '            param_busqueda.SqlDbType = SqlDbType.VarChar
    '            param_busqueda.Size = 200
    '            param_busqueda.Value = cmbNombre.Text
    '            param_busqueda.Direction = ParameterDirection.Input

    '            Try
    '                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spCriteriosdeBusqueda_Delete", _
    '                                          param_busqueda)

    '                Util.MsgStatus(Status1, "Se elimin� correctamente el criterio", My.Resources.Resources.ok.ToBitmap)

    '                LlenarcmbNombre()

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

    '        MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '          + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
    '          "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try
    'End Sub

    Protected Overridable Sub btnImportarExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportarExcel.Click

        Dim TipoLista As String

        If MessageBox.Show("Recuerde que el nombre de la hoja donde est�n los datos debe ser ""Lista =S="". Desea continuar?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If MessageBox.Show("Desea actualizar la lista de Precio de Tableristas?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            If MessageBox.Show("Desea actualizar la lista de Precio VERDE?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                TipoLista = "Distribuidor"
            Else
                Exit Sub
            End If
        Else
            TipoLista = "Tablerista"
        End If


        Dim FileName As String

        Try
            With OpenFileDialog1
                .Filter = "Archivos Excel (*.xlsm)|*.xlsm|" & "Archivos Excel (*.xls)|*.xls|" & "Todos los archivos|*.*"
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

        'If CargarExcel2(FileName, "[Lista de precios$]") = False Then
        If CargarExcel2(FileName, "[Lista =S=$]") = False Then

            Cursor = Cursors.Default
            Util.MsgStatus(Status1, "Proceso finalizado por un error...", My.Resources.stop_error.ToBitmap)
            Exit Sub
        End If

        'Exit Sub

        Dim res As Integer

        res = ImportarRegistros(TipoLista)
        Select Case res
            Case 0
                Util.MsgStatus(Status1, "Se produjo un error al intentar importar el excel", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Se produjo un error al intentar importar el excel", My.Resources.Resources.stop_error.ToBitmap, True)
            Case Else

                SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = 0"

                'Me.LlenarcmbNombre()
                'Me.LlenarcmbPlazo()
                Me.LlenarcmbRubros()
                'Me.LlenarcmbSubRubros()

                bolModo = False

                btnActualizar_Click(sender, e)

                MsgBox("Se importaron " & CantRegistrosImportados & " y se actualizaron " & CantRegistrosActualizados & "  registros.", MsgBoxStyle.Information, "Importaci�n Correcta")

        End Select

        Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Protected Overridable Sub btnImagenExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImagenExcel.Click
        frmMateriales_ImagenExcel.Show()
    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Est� por activar nuevamente el producto: " & grd.CurrentRow.Cells(5).Value.ToString & ". Desea continuar?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim sqlstring As String

        Try

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Materiales SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            sqlstring = " declare @nombreNoFR varchar(4000) " & _
                        " declare @idFR integer " & _
                        " SELECT @nombreNoFR  = nombre FROM Materiales WHERE id = " & grd.CurrentRow.Cells(0).Value & "" & _
                        " IF EXISTS (SELECT 1 FROM Materiales WHERE Nombre = @nombreNoFR + '**FR') " & _
                        " begin" & _
                        " SELECT @idFR = id FROM Materiales WHERE Nombre = @nombreNoFR + '**FR' " & _
                        " update [Materiales] set Eliminado=0  where id = @idFR " & _
                        " end"
            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
            ds_Update.Dispose()

            MsgBox("El producto se activ� correctamente.")

            If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                Try

                    sqlstring = "UPDATE [dbo].[Materiales] SET [Eliminado] = 0 WHERE Codigo = '" & grd.CurrentRow.Cells(1).Value & "'"
                    tranWEB.Sql_Set(sqlstring)

                Catch ex As Exception
                        'MsgBox(ex.Message)
                    MsgBox("No se puede Activa en la Web el producto seleccionado. Ejecute el bot�n sincronizar para actualizar el servidor WEB.")
                    End Try
                End If

            SQL = "exec spMateriales_Select_All @Eliminado = 1,@Fraccionados = 0"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
                End If

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la informaci�n en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste cont�ctese con MercedesIt a trav�s del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        'If MDIPrincipal.NoActualizarBase = False Then
        Me.Cursor = Cursors.WaitCursor
        chkFraccionados.Checked = False
        LlenarGrilla()
        Permitir = True
        CargarCajas()
        PrepararBotones()
        Me.Cursor = Cursors.Default
        'GrillaActualizar()
        'End If
        CargarlistaPrincipales()
    End Sub

#End Region

    Private Sub btnActualizarPreciosOCSch_Click(sender As Object, e As EventArgs)

        If MessageBox.Show("Recuerde que el nombre de la hoja donde est�n los datos debe ser ""Lista =S="". Desea continuar?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim FileName As String

        Try
            With OpenFileDialog1
                .Filter = "Archivos Excel (*.xls)|*.xls|(*.xlsx)|*.xlsx|" & "Todos los archivos|*.*"
            End With
        Catch es As Exception
            MessageBox.Show(es.Message)
        End Try

        OpenFileDialog1.ShowDialog()

        FileName = OpenFileDialog1.FileName

        Util.MsgStatus(Status1, "Procesando archivo...", My.Resources.Resources.indicator_white)

        Me.Refresh()

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If CargarExcel_ActualizarPreciosOCSch(FileName, "[PreciosOCSch$]") = False Then
            Cursor = Cursors.Default
            Util.MsgStatus(Status1, "Proceso finalizado por un error...", My.Resources.stop_error.ToBitmap)
            Exit Sub
        End If

        Dim res As Integer

        res = ImportarRegistros_ActualizarPreciosOCSch()
        Select Case res
            Case 0
                Util.MsgStatus(Status1, "Se produjo un error al intentar actualizar desde el excel", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Se produjo un error al intentar actualizar desde el excel", My.Resources.Resources.stop_error.ToBitmap, True)
            Case Else

                SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = 0"

                ' Me.LlenarcmbNombre()
                'Me.LlenarcmbPlazo()
                Me.LlenarcmbRubros()
                'Me.LlenarcmbSubRubros()

                bolModo = False

                btnActualizar_Click(sender, e)

                MsgBox("Se Actualizaron " & CantRegistrosActualizados & "  registros.", MsgBoxStyle.Information, "Actualizaci�n Correcta")

        End Select

        Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    'Private Sub PrepararGridItems()
    '    Util.LimpiarGridItems(grdItems)
    'End Sub

    'Private Sub LlenarGridItems()

    '    If grdItems.Columns.Count > 0 Then
    '        grdItems.Columns.Clear()
    '    End If

    '    If txtID.Text = "" Then
    '        SQL = "SELECT 0.00 as PrecioPrincipal,  0.00 as PrecioPeron"  'WHERE id  = 0"
    '    Else
    '        SQL = "SELECT PrecioCosto AS 'PrecioPrincipal', PrecioPeron FROM Materiales WHERE id  = " & CType(txtID.Text, Long)
    '    End If

    '    GetDatasetItems(True)

    '    grdItems.Columns(0).Width = 120
    '    grdItems.Columns(1).Width = 120

    '    With grdItems
    '        .VirtualMode = False
    '        '.AllowUserToAddRows = True
    '        .AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
    '        .RowsDefaultCellStyle.BackColor = Color.White
    '        .AllowUserToOrderColumns = True
    '        .SelectionMode = DataGridViewSelectionMode.CellSelect
    '        .ForeColor = Color.Black
    '    End With

    '    With grdItems.ColumnHeadersDefaultCellStyle
    '        .BackColor = Color.Black  'Color.BlueViolet
    '        .ForeColor = Color.White
    '        .Font = New Font("TAHOMA", 10, FontStyle.Bold)
    '    End With

    '    grdItems.Font = New Font("TAHOMA", 10, FontStyle.Regular)

    '    'If grdItems.Rows.Count > 0 Then
    '    '    grdItems.Columns(0).Width = 200
    '    '    grdItems.Columns(1).Width = 200
    '    'End If

    '    'grdEnsayos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    '    'Volver la fuente de datos a como estaba...

    '    SQL = "exec spMateriales_Select_All @Eliminado = 0"

    'End Sub



    'Private Sub validar_NumerosReales2( _
    '    ByVal sender As Object, _
    '    ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    ' obtener indice de la columna  
    '    Dim columna As Integer = grdItems.CurrentCell.ColumnIndex

    '    ' comprobar si la celda en edici�n corresponde a la columna 1 o 3  
    '    If columna = 0 Or columna = 1 Then

    '        Dim caracter As Char = e.KeyChar

    '        ' referencia a la celda  
    '        Dim txt As TextBox = CType(sender, TextBox)

    '        ' comprobar si es un n�mero con isNumber, si es el backspace, si el caracter  
    '        ' es el separador decimal, y que no contiene ya el separador  
    '        If (Char.IsNumber(caracter)) Or _
    '           (caracter = ChrW(Keys.Back)) Or _
    '           (caracter = ".") And _
    '           (txt.Text.Contains(".") = False) Then
    '            e.Handled = False
    '        Else
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub


    Private Function Crear_CSVPrecios(ByVal Mayo As Boolean) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim Result As Integer

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Dim param_mayo As New SqlClient.SqlParameter
        param_mayo.ParameterName = "@Mayo"
        param_mayo.SqlDbType = SqlDbType.Bit
        param_mayo.Value = Mayo
        param_mayo.Direction = ParameterDirection.Input

        Dim param_res As New SqlClient.SqlParameter
        param_res.ParameterName = "@res"
        param_res.SqlDbType = SqlDbType.Int
        param_res.Value = DBNull.Value
        param_res.Direction = ParameterDirection.InputOutput

        Try

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBalanzas_Exportar_Precios", param_mayo, param_res)
            Result = param_res.Value
            If Result > 0 And Mayo = False Then
                'Hago recursividad para ejecutar el SP para mayorista
                Crear_CSVPrecios = Crear_CSVPrecios(True)
            Else
                Crear_CSVPrecios = param_res.Value
            End If

        Catch ex As Exception

            Crear_CSVPrecios = -3
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
            'pStart.StartInfo.UserName = "silvarodrigo2590@hotmail.com"
            'pStart.StartInfo.Password = ToSecureString("ch1ch1n4yruu")
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

    Private Function EjecutarQendraRemoto() As Integer
        Dim res As Integer = 0
        Try

            'Dim startInfo As System.Diagnostics.ProcessStartInfo
            Dim pStart As New System.Diagnostics.Process
            ' Cambiamos el cursor por el de carga
            Me.Cursor = Cursors.WaitCursor
            '-----------------------------------------------------------------------------------------------
            'esta configuracion es para decir si voy a ejecutar la aplicacion con permisos de administrador
            pStart.StartInfo.UseShellExecute = False
            pStart.StartInfo.UserName = "silvarodrigo2590@hotmail.com"
            pStart.StartInfo.Password = ToSecureString("ch1ch1n4yruu")
            '-----------------------------------------------------------------------------------------------
            'EJECUTO .vbs para abrir qendra en pc remota
            pStart.StartInfo.RedirectStandardOutput = True
            'le digo que ejecutable voy a utilizar
            pStart.StartInfo.FileName = "cscript.exe"
            'le paso los argumentos (en este caso el .vbs que quiero que se ejecute)
            pStart.StartInfo.Arguments = "\\Samba-pc\mit\runQendraVBS_Server.vbs"
            'elijo la forma en que debe abrirse el ejecutable
            pStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'abro el ejecutable
            pStart.Start()
            'aca puedo ver que devolvio el .vbs
            Dim salida As String = pStart.StandardOutput.ReadToEnd()
            'coloco una cantidad de tiempo (15 seg) de espera hasta que cierre el proceso
            pStart.WaitForExit(15000)
            'pasado los el tiempo cierra el proceso
            If Not pStart.HasExited Then
                pStart.CloseMainWindow()
                pStart.Kill()
            End If
            'Dim procID As Integer
            'procID = Shell("\\Samba-pc\mit\EjecutarRemoto_Servidor.bat", AppWinStyle.NormalFocus)
            'coloco el cursor con flecha de nuevo
            Me.Cursor = Cursors.Arrow
            EjecutarQendraRemoto = 1

        Catch ex As Exception
            MsgBox(ex.Message)
            EjecutarQendraRemoto = -1
            Me.Cursor = Cursors.Arrow
            Exit Function
        End Try
    End Function

    Private Sub PicActualizarBalanzas_Click(sender As Object, e As EventArgs) Handles PicActualizarBalanzas.Click
        Dim res As Integer

        If MessageBox.Show("Esta seguro que desea actualizar las balanzas ahora?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        'Ejecuto por defecto la funcion para crear el CSV Consumidor Final 
        res = Crear_CSVPrecios(False)
        Select Case res
            Case -1
                Util.MsgStatus(Status1, "Se produjo un error en el control de parametros de la consulta para Crear CSV Cosumidor Final.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Se produjo un error en el control de parametros de la consulta para Crear CSV Cosumidor Final.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            Case -2
                Util.MsgStatus(Status1, "Se produjo un error en el control de parametros de la consulta para Crear CSV Mayorista.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Se produjo un error en el control de parametros de la consulta para Crear CSV Mayorista.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            Case -3
                Util.MsgStatus(Status1, "Se produjo una excepci�n al llamar a la consulta para Crear CSV .", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "Se produjo una excepci�n al llamar a la consulta para Crear CSV .", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            Case 0
                Util.MsgStatus(Status1, "No se pudo realizar la consulta requerida.", My.Resources.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "No se pudo realizar la consulta requerida.", My.Resources.Resources.stop_error.ToBitmap, True)
                Exit Sub
            Case Else
                Util.MsgStatus(Status1, "Se han creado los archivos CSV de forma exitosa.", My.Resources.Resources.ok.ToBitmap)
                Util.MsgStatus(Status1, "Se han creado los archivos CSV de forma exitosa.", My.Resources.Resources.ok.ToBitmap)
                'System.Threading.Thread.Sleep(15000)
                res = EjecutarQendraLocal()
                Select Case res
                    Case -1
                        Util.MsgStatus(Status1, "Error al ejecutado Qendra importador. Por favor verifique datos", My.Resources.Resources.stop_error.ToBitmap)
                        Util.MsgStatus(Status1, "Error al ejecutado Qendra importador. Por favor verifique datos", My.Resources.Resources.stop_error.ToBitmap, True)
                        Exit Sub
                    Case 1
                        Util.MsgStatus(Status1, "Se ha ejecutado Qendra importador Local. Por favor verifique datos actualizados en balanzas", My.Resources.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se ha ejecutado Qendra importador Local. Por favor verifique datos actualizados en balanzas", My.Resources.Resources.ok.ToBitmap)


                        Dim sqlstring As String = "UPDATE Balanzas_Notificaciones SET Actualizado = 0, Fecha = GETDATE()"
                        Dim ds_Balanza As Data.DataSet
                        Dim connection As SqlClient.SqlConnection = Nothing
                        Try
                            connection = SqlHelper.GetConnection(ConnStringSEI.ToString)
                        Catch ex As Exception
                            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        ds_Balanza = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
                        ds_Balanza.Dispose()

                        Util.MsgStatus(Status1, "Se ha notificado a VENTAS1 que actualice las balanzas...", My.Resources.Resources.ok.ToBitmap)
                        Util.MsgStatus(Status1, "Se ha notificado a VENTAS1 que actualice las balanzas...", My.Resources.Resources.ok.ToBitmap)

                        'res = EjecutarQendraRemoto()
                        'Select Case res
                        '    Case -1
                        '        Util.MsgStatus(Status1, "Error al ejecutado Qendra importador. Por favor verifique datos", My.Resources.Resources.stop_error.ToBitmap)
                        '        Util.MsgStatus(Status1, "Error al ejecutado Qendra importador. Por favor verifique datos", My.Resources.Resources.stop_error.ToBitmap, True)
                        '        Exit Sub
                        '    Case 1
                        '        Util.MsgStatus(Status1, "Se ha ejecutado Qendra importador Remoto. Por favor verifique datos actualizados en balanzas", My.Resources.Resources.ok.ToBitmap)
                        '        Util.MsgStatus(Status1, "Se ha ejecutado Qendra importador Remoto. Por favor verifique datos actualizados en balanzas", My.Resources.Resources.ok.ToBitmap)
                        'End Select

                End Select

        End Select
    End Sub

    Private Sub PicActualizarBalanzas_MouseHover(sender As Object, e As EventArgs) Handles PicActualizarBalanzas.MouseHover
        ' ToolTip1.Show("Haga click para enviar novedades a las balanzas del sal�n.", PicExcelExportar)
    End Sub

    Private Sub chkFraccionados_CheckedChanged(sender As Object, e As EventArgs) Handles chkFraccionados.CheckedChanged

        If chkFraccionados.Checked = True Then
            SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = 1"
        Else
            SQL = "exec spMateriales_Select_All @Eliminado = 0,@Fraccionados = 0"
        End If

        LlenarGrilla()
    End Sub

    Private Sub chkCrearFR_CheckedChanged(sender As Object, e As EventArgs) Handles chkCrearFR.CheckedChanged
        rdUnidad.Enabled = Not chkCrearFR.Checked
        If rdUnidad.Checked Then
            rdKilo.Checked = True
        End If
    End Sub

End Class