Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmPagodeClientes_Fletes

    Dim bolpoliticas As Boolean

    Dim RemitosAsociados As String

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

    Dim band As Integer, modificar As Integer, bandIVA As Boolean
    'BANDIVA SE UTILIZA PARA SABER SI EXISTEN VARIOS PORCENTAJES DE IVA DIFERENTES EN EL PAGO

    'Dim IVA As Double

    Public IdCliente As Long, IdPresGes As Long
    Public MontoIva As Double, PorcIva As Double
    Public FechaVta As Date

    'constantes para identificar las columnas de la grilla por nombre 
    ' en vez de número
    'Enum ColumnasDelGridItems
    '    IdPresGes = 0
    '    Cod_Material = 1
    '    Nombre_Material = 2
    '    PrecioUni = 3
    '    Subtotal = 4
    'End Enum

    'Enum ColumnasDelGridFacturasConsumos
    '    Id = 0
    '    NroFactura = 1
    '    FechaFactura = 2
    '    Iva = 3
    '    MontoIva = 4
    '    Subtotal = 5
    '    Total = 6
    '    CondicionIva = 7
    '    CondicionVta = 8
    '    Seleccionar = 9
    '    RemitosAsociados = 10
    '    Deuda = 11
    'End Enum

    'Auxiliares para guardar
    Dim cod_aux As String

    'Auxiliares para chequear lo digitado en la columna cantidad
    Dim qty_digitada As String

    Dim permitir_evento_CellChanged As Boolean


#Region "   Eventos"

    Private Sub frmFacturacion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmPagodeClientes_Contado_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btnEliminar.Visible = False
        btnNuevo.Visible = False
        btnGuardar.Visible = True
        btnActualizar.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False
        btnPrimero.Visible = False
        btnImprimir.Visible = False
        btnImportarExcel.Visible = False
        grd.Visible = False
        ToolStripPagina.Visible = False
        ToolStripSeparator1.Visible = False
        ToolStripSeparator2.Visible = False
        btnExcel.Visible = False
        StatusStrip1.Visible = False

        band = 0
        configurarform()
        'asignarTags()

        LlenarComboImpuestos()
        LlenarComboBancos()
        LlenarComboCuentasOrigen()


        Permitir = True

        band = 1

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtNroDocumentoImpuesto.KeyPress, _
        txtNroCheque.KeyPress, txtNroOpCliente.KeyPress, cmbImpuesto.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtMontoImpuesto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoImpuesto.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAgregarImpuesto.Focus()
        End If
    End Sub

    Private Sub txtMontoCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoCheque.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAgregarCheque.Focus()
        End If
    End Sub

    Private Sub txtMontoTransf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoTransf.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnAgregarTransf.Focus()
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub grdRemitos_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        Cell_X = e.ColumnIndex
        Cell_Y = e.RowIndex
    End Sub

#End Region

#Region "   Procedimientos"

    Private Sub configurarform()
        Me.Text = "Registro de Pago"

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.WindowState = FormWindowState.Normal

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        lblTotalaPagarSinIva.Tag = "5"
        lblTotalaPagar.Tag = "8"
        txtEntregaContado.Tag = "10"
        lblEntregaCheques.Tag = "14"
        lblEntregaImpuestos.Tag = "12"
        lblEntregaTransferencias.Tag = "16"
        lblEntregaTarjetas.Tag = "18"
        txtRedondeo.Tag = "20"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        Util.MsgStatus(Status1, "Verificando los datos...", My.Resources.Resources.indicator_white)

        'Dim i As Integer
        'Dim unremito As Boolean = False

        'If unremito = False Then
        '    Util.MsgStatus(Status1, "Seleccione al menos un remito para facturar.", My.Resources.Resources.alert.ToBitmap, True)
        '    Exit Sub
        'End If

        bolpoliticas = True

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


    Private Sub GetDatasetItems(ByVal grdchico As DataGridView)
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_2 = SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL)
            ds_2.Dispose()

            grdchico.DataSource = ds_2.Tables(0).DefaultView

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

    Private Sub LlenarComboImpuestos()
        Dim ds_Impuestos As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Impuestos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, CODIGO from Impuestos where eliminado=0 order by codigo")
            ds_Impuestos.Dispose()

            With Me.cmbImpuesto
                .DataSource = ds_Impuestos.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
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

    Private Sub LlenarComboBancos()
        Dim ds_Bancos As Data.DataSet
        Dim ds_Bancos_Origen As Data.DataSet
        Dim ds_Bancos_Destino As Data.DataSet

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Bancos = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                    " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos.Dispose()

            ds_Bancos_Origen = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                    " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos_Origen.Dispose()

            ds_Bancos_Destino = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT * FROM (SELECT UPPER(Banco) as Banco FROM CHEQUES UNION SELECT UPPER(BancoOrigen) as Banco FROM TransferenciaBancaria " & _
                                                                " UNION SELECT UPPER(BancoDestino) as Banco  FROM TransferenciaBancaria ) A ORDER BY Banco")
            ds_Bancos_Destino.Dispose()

            With Me.cmbBanco
                .DataSource = ds_Bancos.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
            End With

            With Me.cmbBancoDestino
                .DataSource = ds_Bancos_Destino.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
            End With

            With Me.cmbBancoOrigen
                .DataSource = ds_Bancos_Origen.Tables(0).DefaultView
                .DisplayMember = "Banco"
                .ValueMember = "Banco"
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

    Private Sub LlenarComboCuentasOrigen()
        Dim ds_Cuentas As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ds_Cuentas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT CuentaOrigen FROM TransferenciaBancaria ORDER BY CuentaOrigen ")

            ds_Cuentas.Dispose()

            With Me.cmbCuentaOrigen
                .DataSource = ds_Cuentas.Tables(0).DefaultView
                .DisplayMember = "CuentaOrigen"
                .ValueMember = "CuentaOrigen"
            End With

            ds_Cuentas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT CuentaDestino FROM TransferenciaBancaria ORDER BY CuentaDestino ")

            ds_Cuentas.Dispose()

            With Me.cmbCuentaDestino
                .DataSource = ds_Cuentas.Tables(0).DefaultView
                .DisplayMember = "CuentaDestino"
                .ValueMember = "CuentaDestino"
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


    Private Sub Imprimir()
        nbreformreportes = "Comprobante de Pago"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        Imprimir_LlenarTMPs(txtCODIGO.Text)

        Rpt.MostrarReporte_PagodeClientes(txtCODIGO.Text, Rpt)

        cnn = Nothing

    End Sub

    Private Sub LlenarGrid_Impuestos()

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

            sqltxt2 = "SELECT Codigo, NroDocumento, Monto, ISNULL(Ii.Observaciones ,'') AS Observaciones FROM ImpuestosxIngreso ii " & _
                     " JOIN Impuestos I ON I.Id = ii.IdImpuesto WHERE IdIngreso = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdImpuestos.Rows.Add(dt.Rows(i)("CODIGO").ToString(), dt.Rows(i)("NRODOCUMENTO").ToString(), dt.Rows(i)("monto").ToString(), dt.Rows(i)("OBSERVACIONES").ToString())
                'grdImpuestos.Rows.Add(cmbImpuesto.Text, txtNroDocumentoImpuesto.Text, CDec(txtMontoImpuesto.Text), txtObservacionesImpuestos.Text)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarGrid_Cheques()
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

            sqltxt2 = "SELECT NroCheque, banco, monto, fechacobro, clientechequebco, idmoneda, observaciones, c.id, utilizado " & _
                    " FROM Cheques c JOIN ingresos_cheques ic ON ic.idcheque = c.id " & _
                    " where IdIngreso = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdCheques.Rows.Add(dt.Rows(i)("nrocheque").ToString(), dt.Rows(i)("banco").ToString(), dt.Rows(i)("monto").ToString(), dt.Rows(i)("fechacobro").ToString(), dt.Rows(i)("clientechequebco").ToString(), dt.Rows(i)("idmoneda").ToString(), dt.Rows(i)("Observaciones").ToString(), dt.Rows(i)("Id").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarGrid_Transferencias()
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

            sqltxt2 = " SELECT CuentaOrigen, CuentaDestino, BancoOrigen, BancoDestino, " & _
                        " IdMoneda, NroOperacionCliente, MontoTransferencia, Observaciones, FechaTransferencia, T.ID from TransferenciaBancaria t " & _
                        " JOIN TransferenciaxIngresos ti ON ti.IdTransferencia = t.Id " & _
                        " WHERE IdIngreso = " & txtID.Text

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)
            Dim i As Integer

            da.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                grdTransferencias.Rows.Add(dt.Rows(i)("NroOperacionCliente").ToString(), dt.Rows(i)("CuentaOrigen").ToString(), _
                                    dt.Rows(i)("MontoTransferencia").ToString(), dt.Rows(i)("CuentaDestino").ToString(), _
                                    dt.Rows(i)("FechaTransferencia").ToString(), dt.Rows(i)("idmoneda").ToString(), _
                                    dt.Rows(i)("BancoOrigen").ToString(), dt.Rows(i)("BancoDestino").ToString(), _
                                    dt.Rows(i)("Observaciones").ToString(), dt.Rows(i)("Id").ToString())
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LimpiarGrids()
        grdImpuestos.Rows.Clear()
        grdCheques.Rows.Clear()
        grdTransferencias.Rows.Clear()
        grdTarjetas.Rows.Clear()

        txtNroDocumentoImpuesto.Text = ""
        txtMontoImpuesto.Text = "0"
        txtObservacionesImpuestos.Text = ""

        txtNroCheque.Text = ""
        txtMontoCheque.Text = "0"
        dtpFechaCheque.Value = Date.Today  'grdCheques.CurrentRow.Cells(3).Value
        txtPropietario.Text = ""
        'cmbMoneda.SelectedValue = grd.CurrentRow.Cells(5).Value
        txtObservacionesCheque.Text = ""

        txtNroOpCliente.Text = ""
        txtMontoTransf.Text = "0"
        cmbCuentaDestino.Text = ""
        cmbCuentaOrigen.Text = ""
        txtObservacionesTransf.Text = ""

    End Sub

    Private Sub Imprimir_LlenarTMPs(ByVal NroMov As String)

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@idmov"
                param_codigo.SqlDbType = SqlDbType.BigInt
                'param_codigo.Size = 10
                param_codigo.Value = CLng(NroMov)
                param_codigo.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spRPT_Ingresos_Tmp", _
                                               param_codigo)

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
    End Sub

#End Region

#Region "   Funciones"

    Private Function AgregarRegistro_Ingreso() As Integer
        Dim res As Integer = 0

        Try
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
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                'param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_nropago As New SqlClient.SqlParameter
                param_nropago.ParameterName = "@nroordenPago"
                param_nropago.SqlDbType = SqlDbType.VarChar
                param_nropago.Size = 50
                param_nropago.Value = "" 'txtOrdenPago.Text
                param_nropago.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = IdCliente
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = FechaVta
                param_fecha.Direction = ParameterDirection.Input

                Dim param_contado As New SqlClient.SqlParameter
                param_contado.ParameterName = "@contado"
                param_contado.SqlDbType = SqlDbType.Bit
                If txtEntregaContado.Text <> "" And txtEntregaContado.Text <> "0" Then
                    param_contado.Value = True
                Else
                    param_contado.Value = False
                End If
                param_contado.Direction = ParameterDirection.Input

                Dim param_montocontado As New SqlClient.SqlParameter
                param_montocontado.ParameterName = "@montocontado"
                param_montocontado.SqlDbType = SqlDbType.Decimal
                param_montocontado.Precision = 18
                param_montocontado.Scale = 2
                param_montocontado.Value = IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)
                param_montocontado.Direction = ParameterDirection.Input

                Dim param_tarjeta As New SqlClient.SqlParameter
                param_tarjeta.ParameterName = "@tarjeta"
                param_tarjeta.SqlDbType = SqlDbType.Bit
                If lblEntregaTarjetas.Text <> "" And lblEntregaTarjetas.Text <> "0" Then
                    param_tarjeta.Value = True
                Else
                    param_tarjeta.Value = False
                End If
                param_tarjeta.Direction = ParameterDirection.Input

                Dim param_montotarjeta As New SqlClient.SqlParameter
                param_montotarjeta.ParameterName = "@montotarjeta"
                param_montotarjeta.SqlDbType = SqlDbType.Decimal
                param_montotarjeta.Precision = 18
                param_montotarjeta.Scale = 2
                param_montotarjeta.Value = lblEntregaTarjetas.Text
                param_montotarjeta.Direction = ParameterDirection.Input

                Dim param_cheque As New SqlClient.SqlParameter
                param_cheque.ParameterName = "@cheque"
                param_cheque.SqlDbType = SqlDbType.Bit
                If lblEntregaCheques.Text <> "" And lblEntregaCheques.Text <> "0.00" Then
                    param_cheque.Value = True
                Else
                    param_cheque.Value = False
                End If
                param_cheque.Direction = ParameterDirection.Input

                Dim param_montocheque As New SqlClient.SqlParameter
                param_montocheque.ParameterName = "@montocheque"
                param_montocheque.SqlDbType = SqlDbType.Decimal
                param_montocheque.Precision = 18
                param_montocheque.Scale = 2
                param_montocheque.Value = lblEntregaCheques.Text
                param_montocheque.Direction = ParameterDirection.Input

                Dim param_transferencia As New SqlClient.SqlParameter
                param_transferencia.ParameterName = "@transferencia"
                param_transferencia.SqlDbType = SqlDbType.Bit
                If lblEntregaTransferencias.Text <> "" And lblEntregaTransferencias.Text <> "0" Then
                    param_transferencia.Value = True
                Else
                    param_transferencia.Value = False
                End If
                param_transferencia.Direction = ParameterDirection.Input

                Dim param_montotransf As New SqlClient.SqlParameter
                param_montotransf.ParameterName = "@montotransf"
                param_montotransf.SqlDbType = SqlDbType.Decimal
                param_montotransf.Precision = 18
                param_montotransf.Scale = 2
                param_montotransf.Value = lblEntregaTransferencias.Text
                param_montotransf.Direction = ParameterDirection.Input

                Dim param_impuestos As New SqlClient.SqlParameter
                param_impuestos.ParameterName = "@impuestos"
                param_impuestos.SqlDbType = SqlDbType.Bit
                If lblEntregaImpuestos.Text <> "" And lblEntregaImpuestos.Text <> "0" Then
                    param_impuestos.Value = True
                Else
                    param_impuestos.Value = False
                End If
                param_impuestos.Direction = ParameterDirection.Input

                Dim param_montoimpuesto As New SqlClient.SqlParameter
                param_montoimpuesto.ParameterName = "@montoimpuesto"
                param_montoimpuesto.SqlDbType = SqlDbType.Decimal
                param_montoimpuesto.Precision = 18
                param_montoimpuesto.Scale = 2
                param_montoimpuesto.Value = lblEntregaImpuestos.Text
                param_montoimpuesto.Direction = ParameterDirection.Input

                'Dim param_iva As New SqlClient.SqlParameter
                'param_iva.ParameterName = "@iva"
                'param_iva.SqlDbType = SqlDbType.Decimal
                'param_iva.Precision = 18
                'param_iva.Scale = 2
                'param_iva.Value = PorcIva
                'param_iva.Direction = ParameterDirection.Input

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = MontoIva
                param_montoiva.Direction = ParameterDirection.Input

                Dim param_subtotal As New SqlClient.SqlParameter
                param_subtotal.ParameterName = "@subtotal"
                param_subtotal.SqlDbType = SqlDbType.Decimal
                param_subtotal.Precision = 18
                param_subtotal.Scale = 2
                param_subtotal.Value = lblTotalaPagarSinIva.Text
                param_subtotal.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@total"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = lblTotalaPagar.Text
                param_total.Direction = ParameterDirection.Input

                Dim param_Redondeo As New SqlClient.SqlParameter
                param_Redondeo.ParameterName = "@Redondeo"
                param_Redondeo.SqlDbType = SqlDbType.Decimal
                param_Redondeo.Precision = 18
                param_Redondeo.Scale = 2
                param_Redondeo.Value = CDbl(txtRedondeo.Text)
                param_Redondeo.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Insert", _
                                              param_id, param_codigo, param_nropago, param_idcliente, param_fecha, param_contado, param_montocontado, _
                                              param_tarjeta, param_montotarjeta, param_cheque, param_montocheque, _
                                              param_transferencia, param_montotransf, param_impuestos, param_montoimpuesto, _
                                              param_montoiva, param_subtotal, param_total, param_Redondeo, _
                                              param_useradd, param_res)

                    txtID.Text = param_id.Value

                    txtCODIGO.Text = param_codigo.Value

                    AgregarRegistro_Ingreso = param_res.Value

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

    Private Function AgregarRegistro_FacturasConsumos() As Integer
        Dim res As Integer = 0
        'Dim i As Integer

        Try
            Try
                Dim Cancelartodo As Boolean = False
                Dim Deuda As Double, totalentregado As Double ', DeudaFactCons As Double

                totalentregado = CDbl(lblEntregado.Text)

                Deuda = lblTotalaPagar.Text - totalentregado    'grdFacturasConsumos.Rows(i).Cells(ColumnasDelGridFacturasConsumos.Total).Value

                If Deuda = 0 Then
                    Cancelartodo = True
                    'DeudaFactCons = 0
                Else
                    Cancelartodo = False
                    'DeudaFactCons = Deuda
                End If

                'totalentregado = Deuda

                Dim param_idingreso As New SqlClient.SqlParameter
                param_idingreso.ParameterName = "@idingreso"
                param_idingreso.SqlDbType = SqlDbType.BigInt
                param_idingreso.Value = txtID.Text
                param_idingreso.Direction = ParameterDirection.Input

                Dim param_idfacturacion As New SqlClient.SqlParameter
                param_idfacturacion.ParameterName = "@idfacturacion"
                param_idfacturacion.SqlDbType = SqlDbType.BigInt
                param_idfacturacion.Value = txtIdFacturacion.Text
                param_idfacturacion.Direction = ParameterDirection.Input

                Dim param_DEUDA As New SqlClient.SqlParameter
                param_DEUDA.ParameterName = "@Deuda"
                param_DEUDA.SqlDbType = SqlDbType.Decimal
                param_DEUDA.Precision = 18
                param_DEUDA.Scale = 2
                param_DEUDA.Value = Deuda 'DeudaFactCons
                param_DEUDA.Direction = ParameterDirection.Input

                Dim param_CancelarTodo As New SqlClient.SqlParameter
                param_CancelarTodo.ParameterName = "@CancelarTodo"
                param_CancelarTodo.SqlDbType = SqlDbType.Bit
                param_CancelarTodo.Value = Cancelartodo
                param_CancelarTodo.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Facturas_Insert", _
                                              param_idfacturacion, param_idingreso, param_DEUDA, param_CancelarTodo, param_res)

                    res = param_res.Value

                Catch ex As Exception
                    Throw ex
                End Try

                AgregarRegistro_FacturasConsumos = res

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

    Private Function AgregarRegistro_Cheques() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdCheques.RowCount - 1

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_NroCheque As New SqlClient.SqlParameter
                    param_NroCheque.ParameterName = "@NroCheque"
                    param_NroCheque.SqlDbType = SqlDbType.BigInt
                    param_NroCheque.Value = grdCheques.Rows(i).Cells(0).Value
                    param_NroCheque.Direction = ParameterDirection.Input

                    Dim param_IdCliente As New SqlClient.SqlParameter
                    param_IdCliente.ParameterName = "@IdCliente"
                    param_IdCliente.SqlDbType = SqlDbType.Int
                    param_IdCliente.Value = IdCliente
                    param_IdCliente.Direction = ParameterDirection.Input

                    Dim param_ClienteChequeBco As New SqlClient.SqlParameter
                    param_ClienteChequeBco.ParameterName = "@ClienteChequeBco"
                    param_ClienteChequeBco.SqlDbType = SqlDbType.NVarChar
                    param_ClienteChequeBco.Size = 50
                    param_ClienteChequeBco.Value = grdCheques.Rows(i).Cells(4).Value
                    param_ClienteChequeBco.Direction = ParameterDirection.Input

                    Dim param_FechaCobro As New SqlClient.SqlParameter
                    param_FechaCobro.ParameterName = "@FechaCobro"
                    param_FechaCobro.SqlDbType = SqlDbType.DateTime
                    param_FechaCobro.Value = grdCheques.Rows(i).Cells(3).Value
                    param_FechaCobro.Direction = ParameterDirection.Input

                    Dim param_Moneda As New SqlClient.SqlParameter
                    param_Moneda.ParameterName = "@IdMoneda"
                    param_Moneda.SqlDbType = SqlDbType.Int
                    param_Moneda.Value = grdCheques.Rows(i).Cells(5).Value
                    param_Moneda.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdCheques.Rows(i).Cells(2).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_Banco As New SqlClient.SqlParameter
                    param_Banco.ParameterName = "@Banco"
                    param_Banco.SqlDbType = SqlDbType.NVarChar
                    param_Banco.Size = 50
                    param_Banco.Value = grdCheques.Rows(i).Cells(1).Value
                    param_Banco.Direction = ParameterDirection.Input

                    Dim param_Observaciones As New SqlClient.SqlParameter
                    param_Observaciones.ParameterName = "@Observaciones"
                    param_Observaciones.SqlDbType = SqlDbType.NVarChar
                    param_Observaciones.Size = 100
                    param_Observaciones.Value = grdCheques.Rows(i).Cells(6).Value
                    param_Observaciones.Direction = ParameterDirection.Input

                    Dim param_useradd As New SqlClient.SqlParameter
                    param_useradd.ParameterName = "@useradd"
                    param_useradd.SqlDbType = SqlDbType.SmallInt
                    param_useradd.Value = UserID
                    param_useradd.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCheques_Insert", _
                                param_IdIngreso, param_NroCheque, param_IdCliente, _
                                param_ClienteChequeBco, param_FechaCobro, param_Moneda, param_Monto, _
                                param_Banco, param_Observaciones, param_useradd, param_res)

                        res = param_res.Value

                        If res < 0 Then
                            AgregarRegistro_Cheques = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Cheques = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Cheques = 1
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

    Private Function AgregarRegistro_Impuestos() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdImpuestos.RowCount - 1

                    Dim param_Id As New SqlClient.SqlParameter
                    param_Id.ParameterName = "@Id"
                    param_Id.SqlDbType = SqlDbType.BigInt
                    param_Id.Value = grdImpuestos.Rows(i).Cells(5).Value 'cmbImpuesto.SelectedValue
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_IdImpuesto As New SqlClient.SqlParameter
                    param_IdImpuesto.ParameterName = "@IdImpuesto"
                    param_IdImpuesto.SqlDbType = SqlDbType.BigInt
                    param_IdImpuesto.Value = grdImpuestos.Rows(i).Cells(4).Value 'cmbImpuesto.SelectedValue
                    param_IdImpuesto.Direction = ParameterDirection.Input

                    Dim param_NroDocumento As New SqlClient.SqlParameter
                    param_NroDocumento.ParameterName = "@NroDocumento"
                    param_NroDocumento.SqlDbType = SqlDbType.NVarChar
                    param_NroDocumento.Size = 30
                    param_NroDocumento.Value = LTrim(RTrim(grdImpuestos.Rows(i).Cells(1).Value))
                    param_NroDocumento.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdImpuestos.Rows(i).Cells(2).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spIngresos_Impuestos_Insert", _
                                param_IdIngreso, param_IdImpuesto, param_NroDocumento, _
                                 param_Monto, param_res)

                        res = param_res.Value

                        If res < 0 Then
                            AgregarRegistro_Impuestos = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Impuestos = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Impuestos = 1
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

    Private Function AgregarRegistro_Transferencias() As Integer
        Dim res As Integer = 0
        Dim i As Integer

        Try

            Try

                For i = 0 To grdTransferencias.RowCount - 1

                    Dim param_Id As New SqlClient.SqlParameter
                    param_Id.ParameterName = "@Id"
                    param_Id.SqlDbType = SqlDbType.BigInt
                    If grdTransferencias.Rows(i).Cells(9).Value Is DBNull.Value Or grdTransferencias.Rows(i).Cells(9).Value Is Nothing Then
                        param_Id.Value = DBNull.Value
                    Else
                        param_Id.Value = CLng(grdTransferencias.Rows(i).Cells(9).Value)
                    End If
                    param_Id.Direction = ParameterDirection.Input

                    Dim param_IdIngreso As New SqlClient.SqlParameter
                    param_IdIngreso.ParameterName = "@IdIngreso"
                    param_IdIngreso.SqlDbType = SqlDbType.BigInt
                    param_IdIngreso.Value = txtID.Text
                    param_IdIngreso.Direction = ParameterDirection.Input

                    Dim param_CuentaOrigen As New SqlClient.SqlParameter
                    param_CuentaOrigen.ParameterName = "@CuentaOrigen"
                    param_CuentaOrigen.SqlDbType = SqlDbType.VarChar
                    param_CuentaOrigen.Size = 30
                    param_CuentaOrigen.Value = grdTransferencias.Rows(i).Cells(1).Value
                    param_CuentaOrigen.Direction = ParameterDirection.Input

                    Dim param_CuentaDestino As New SqlClient.SqlParameter
                    param_CuentaDestino.ParameterName = "@CuentaDestino"
                    param_CuentaDestino.SqlDbType = SqlDbType.VarChar
                    param_CuentaDestino.Size = 30
                    param_CuentaDestino.Value = grdTransferencias.Rows(i).Cells(3).Value
                    param_CuentaDestino.Direction = ParameterDirection.Input

                    Dim param_BancoOrigen As New SqlClient.SqlParameter
                    param_BancoOrigen.ParameterName = "@BancoOrigen"
                    param_BancoOrigen.SqlDbType = SqlDbType.VarChar
                    param_BancoOrigen.Size = 30
                    param_BancoOrigen.Value = grdTransferencias.Rows(i).Cells(6).Value
                    param_BancoOrigen.Direction = ParameterDirection.Input

                    Dim param_BancoDestino As New SqlClient.SqlParameter
                    param_BancoDestino.ParameterName = "@BancoDestino"
                    param_BancoDestino.SqlDbType = SqlDbType.VarChar
                    param_BancoDestino.Size = 30
                    param_BancoDestino.Value = grdTransferencias.Rows(i).Cells(7).Value
                    param_BancoDestino.Direction = ParameterDirection.Input

                    Dim param_Fecha As New SqlClient.SqlParameter
                    param_Fecha.ParameterName = "@FechaTransf"
                    param_Fecha.SqlDbType = SqlDbType.DateTime
                    param_Fecha.Value = grdTransferencias.Rows(i).Cells(4).Value
                    param_Fecha.Direction = ParameterDirection.Input

                    Dim param_Moneda As New SqlClient.SqlParameter
                    param_Moneda.ParameterName = "@IdMoneda"
                    param_Moneda.SqlDbType = SqlDbType.Int
                    param_Moneda.Value = 1 'grdTransferencias.Rows(i).Cells(5).Value
                    param_Moneda.Direction = ParameterDirection.Input

                    Dim param_Nroop As New SqlClient.SqlParameter
                    param_Nroop.ParameterName = "@NroOp"
                    param_Nroop.SqlDbType = SqlDbType.VarChar
                    param_Nroop.Size = 30
                    param_Nroop.Value = grdTransferencias.Rows(i).Cells(0).Value
                    param_Nroop.Direction = ParameterDirection.Input

                    Dim param_Monto As New SqlClient.SqlParameter
                    param_Monto.ParameterName = "@Monto"
                    param_Monto.SqlDbType = SqlDbType.Decimal
                    param_Monto.Precision = 18
                    param_Monto.Scale = 2
                    param_Monto.Value = grdTransferencias.Rows(i).Cells(2).Value
                    param_Monto.Direction = ParameterDirection.Input

                    Dim param_Observaciones As New SqlClient.SqlParameter
                    param_Observaciones.ParameterName = "@Observaciones"
                    param_Observaciones.SqlDbType = SqlDbType.NVarChar
                    param_Observaciones.Size = 100
                    param_Observaciones.Value = grdTransferencias.Rows(i).Cells(8).Value
                    param_Observaciones.Direction = ParameterDirection.Input

                    Dim param_res As New SqlClient.SqlParameter
                    param_res.ParameterName = "@res"
                    param_res.SqlDbType = SqlDbType.Int
                    param_res.Value = 0
                    param_res.Direction = ParameterDirection.InputOutput

                    Try

                        SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spTransferencias_Insert", _
                                param_IdIngreso, param_BancoDestino, param_BancoOrigen, _
                                param_CuentaDestino, param_CuentaOrigen, param_Fecha, param_Moneda, _
                                param_Monto, param_Nroop, param_Observaciones, param_res)

                        res = param_res.Value

                        If res < 0 Then
                            AgregarRegistro_Transferencias = -1
                            Exit Function
                        End If

                    Catch ex As Exception
                        Throw ex
                        AgregarRegistro_Transferencias = -1
                        Exit Function
                    End Try

                Next

                AgregarRegistro_Transferencias = 1
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

    Private Function EliminarRegistro() As Integer
        'Dim res As Integer = 0

        'Try
        '    Try
        '        conn_del_form = SqlHelper.GetConnection(ConnStringSEI)
        '    Catch ex As Exception
        '        MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Function
        '    End Try

        '    'Abrir una transaccion para guardar y asegurar que se guarda todo
        '    If Abrir_Tran(conn_del_form) = False Then
        '        MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Function
        '    End If

        '    Try

        '        Dim param_idPresGest As New SqlClient.SqlParameter("@idPres_Ges", SqlDbType.BigInt, ParameterDirection.Input)
        '        param_idPresGest.Value = CType(txtID.Text, Long)
        '        param_idPresGest.Direction = ParameterDirection.Input

        '        Dim param_userdel As New SqlClient.SqlParameter
        '        param_userdel.ParameterName = "@userdel"
        '        param_userdel.SqlDbType = SqlDbType.Int
        '        param_userdel.Value = UserID
        '        param_userdel.Direction = ParameterDirection.Input

        '        Dim param_res As New SqlClient.SqlParameter
        '        param_res.ParameterName = "@res"
        '        param_res.SqlDbType = SqlDbType.Int
        '        param_res.Value = DBNull.Value
        '        param_res.Direction = ParameterDirection.Output

        '        Try

        '            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Presupuestos_Gestion_Delete", _
        '                                      param_idPresGest, param_userdel, param_res)

        '            EliminarRegistro = param_res.Value

        '        Catch ex As Exception
        '            '' 


        '            Throw ex
        '        End Try
        '    Finally
        '        ''
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

        'End Try
    End Function

    Private Function EliminarRegistroItems(ByVal IdPres_Ges As Long) As Integer
        'Dim i As Integer, res As Integer

        'Try
        '    Try
        '        i = 0
        '        Do While i < grdItems.Rows.Count

        '            Dim param_id As New SqlClient.SqlParameter
        '            param_id.ParameterName = "@idpres_ges"
        '            param_id.SqlDbType = SqlDbType.BigInt
        '            param_id.Value = IdPres_Ges
        '            param_id.Direction = ParameterDirection.Input

        '            Dim param_idpres_ges As New SqlClient.SqlParameter
        '            param_idpres_ges.ParameterName = "@idpres_ges_det"
        '            param_idpres_ges.SqlDbType = SqlDbType.BigInt
        '            param_idpres_ges.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdPres_Ges_Det).Value, Long)
        '            param_idpres_ges.Direction = ParameterDirection.Input

        '            Dim param_idpresupuesto As New SqlClient.SqlParameter
        '            param_idpresupuesto.ParameterName = "@idpresupuesto"
        '            param_idpresupuesto.SqlDbType = SqlDbType.BigInt
        '            param_idpresupuesto.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.Id_Pres).Value, Long)
        '            param_idpresupuesto.Direction = ParameterDirection.Input

        '            Dim param_idpresupuesto_det As New SqlClient.SqlParameter
        '            param_idpresupuesto_det.ParameterName = "@idpres_det"
        '            param_idpresupuesto_det.SqlDbType = SqlDbType.BigInt
        '            param_idpresupuesto_det.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.ID_Pres_Det).Value, Long)
        '            param_idpresupuesto_det.Direction = ParameterDirection.Input

        '            Dim param_idmaterial As New SqlClient.SqlParameter
        '            param_idmaterial.ParameterName = "@idmaterial"
        '            param_idmaterial.SqlDbType = SqlDbType.BigInt
        '            param_idmaterial.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IdPres_Ges_Det).Value, Long)
        '            param_idmaterial.Direction = ParameterDirection.Input

        '            Dim param_idunidad As New SqlClient.SqlParameter
        '            param_idunidad.ParameterName = "@idunidad"
        '            param_idunidad.SqlDbType = SqlDbType.BigInt
        '            param_idunidad.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.IDUnidad).Value, Long)
        '            param_idunidad.Direction = ParameterDirection.Input

        '            Dim param_qty As New SqlClient.SqlParameter
        '            param_qty.ParameterName = "@qty"
        '            param_qty.SqlDbType = SqlDbType.Decimal
        '            param_qty.Precision = 18
        '            param_qty.Scale = 2
        '            param_qty.Value = CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtyPedida).Value, Decimal) - CType(grdItems.Rows(i).Cells(ColumnasDelGridItems.QtySaldo).Value, Decimal)
        '            param_qty.Direction = ParameterDirection.Input

        '            Dim param_userdel As New SqlClient.SqlParameter
        '            param_userdel.ParameterName = "@userdel"
        '            param_userdel.SqlDbType = SqlDbType.Int
        '            param_userdel.Value = UserID
        '            param_userdel.Direction = ParameterDirection.Input

        '            Dim param_res As New SqlClient.SqlParameter
        '            param_res.ParameterName = "@res"
        '            param_res.SqlDbType = SqlDbType.Int
        '            param_res.Value = DBNull.Value
        '            param_res.Direction = ParameterDirection.InputOutput

        '            Try
        '                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_Presupuestos_Gestion_Det_Delete", _
        '                                          param_id, param_idpres_ges, param_idpresupuesto, param_idpresupuesto_det, _
        '                                          param_idmaterial, param_idunidad, param_qty, param_userdel, param_res)

        '                res = param_res.Value

        '                If (res <= 0) Then
        '                    Exit Do
        '                End If

        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            i = i + 1
        '        Loop

        '        EliminarRegistroItems = res

        '    Catch ex2 As Exception
        '        Throw ex2
        '    Finally

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
        'End Try
    End Function

    Private Function AgregarRegistro_CtaCte() As Integer
        Dim res As Integer = 0

        Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = IdCliente
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_tipooperacion As New SqlClient.SqlParameter
                param_tipooperacion.ParameterName = "@tipooperacion"
                param_tipooperacion.SqlDbType = SqlDbType.VarChar
                param_tipooperacion.Size = 25
                param_tipooperacion.Value = "PAGO"
                param_tipooperacion.Direction = ParameterDirection.Input

                Dim param_IDFACTURACIONPAGO As New SqlClient.SqlParameter
                param_IDFACTURACIONPAGO.ParameterName = "@IdFacturacionPago"
                param_IDFACTURACIONPAGO.SqlDbType = SqlDbType.BigInt
                param_IDFACTURACIONPAGO.Value = txtID.Text
                param_IDFACTURACIONPAGO.Direction = ParameterDirection.Input

                Dim param_fechaFACTURA As New SqlClient.SqlParameter
                param_fechaFACTURA.ParameterName = "@FechaMovCtaCte"
                param_fechaFACTURA.SqlDbType = SqlDbType.VarChar
                param_fechaFACTURA.Size = 10
                param_fechaFACTURA.Value = FechaVta.ToString
                param_fechaFACTURA.Direction = ParameterDirection.Input

                Dim param_montoMov As New SqlClient.SqlParameter
                param_montoMov.ParameterName = "@montomovctacte"
                param_montoMov.SqlDbType = SqlDbType.Decimal
                param_montoMov.Precision = 18
                param_montoMov.Scale = 2
                param_montoMov.Value = CDbl(lblEntregado.Text)
                param_montoMov.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spCtaCteMovimientos_Insert", _
                                              param_codigo, param_idcliente, param_tipooperacion, param_IDFACTURACIONPAGO, _
                                              param_fechaFACTURA, param_montoMov, param_useradd, param_res)


                    AgregarRegistro_CtaCte = param_res.Value

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

    ' Capturar los enter del formulario, descartar todos salvo los que 
    ' se dan dentro de la grilla. Es una sobre carga de un metodo existente.
    'Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean

    '    ' Si la tecla presionada es distinta de la tecla Enter,
    '    ' abandonamos el procedimiento.
    '    '
    '    If keyData <> Keys.Return Then Return MyBase.ProcessCmdKey(msg, keyData)

    '    ' Igualmente, si el control DataGridView no tiene el foco,
    '    ' y si la celda actual no está siendo editada,
    '    ' abandonamos el procedimiento.
    '    If (Not grdItems.Focused) AndAlso (Not grdItems.IsCurrentCellInEditMode) Then Return MyBase.ProcessCmdKey(msg, keyData)

    '    ' Obtenemos la celda actual
    '    Dim cell As DataGridViewCell = grdItems.CurrentCell
    '    ' Índice de la columna.
    '    Dim columnIndex As Int32 = cell.ColumnIndex
    '    ' Índice de la fila.
    '    Dim rowIndex As Int32 = cell.RowIndex

    '    'Do
    '    '    If columnIndex = grdItems.Columns.Count - 1 Then
    '    '        If rowIndex = grdItems.Rows.Count - 1 Then
    '    '            ' Seleccionamos la primera columna de la primera fila.
    '    '            cell = grdItems.Rows(0).Cells(ColumnasDelGridItems.IdPres_Ges_Det)
    '    '        Else
    '    '            ' Selecionamos la primera columna de la siguiente fila.
    '    '            cell = grdItems.Rows(rowIndex + 1).Cells(ColumnasDelGridItems.IdPres_Ges_Det)
    '    '        End If
    '    '    Else
    '    '        ' Seleccionamos la celda de la derecha de la celda actual.
    '    '        cell = grdItems.Rows(rowIndex).Cells(columnIndex + 1)
    '    '    End If
    '    '    ' establecer la fila y la columna actual
    '    '    columnIndex = cell.ColumnIndex
    '    '    rowIndex = cell.RowIndex
    '    'Loop While (cell.Visible = False)

    '    grdItems.CurrentCell = cell

    '    ' ... y la ponemos en modo de edición.
    '    grdItems.BeginEdit(True)
    '    Return True

    'End Function

#End Region

#Region "   Botones"

    Public Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0

        'gpPago.Enabled = bolModo
        'TabControl1.Enabled = bolModo

        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()

        Util.LimpiarTextBox(Me.Controls)

        dtpFechaTransf.Value = Date.Today
        dtpFechaCheque.Value = Date.Today

        lblEntregado.Text = "0"
        lblEntregaCheques.Text = "0"
        lblEntregaImpuestos.Text = "0"
        lblEntregaTarjetas.Text = "0"
        lblEntregaTransferencias.Text = "0"
        lblResto.Text = "0"

        'txtEntregaContado.Enabled = bolModo

        LlenarComboBancos()
        LlenarComboCuentasOrigen()

        LimpiarGrids()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer

        Util.MsgStatus(Status1, "Controlando la información...", My.Resources.Resources.indicator_white)

        'If bandIVA = False Then
        '    MsgBox("Ha seleccionado Facturas con diferentes porcentajes de IVA. Por favor, verifique", MsgBoxStyle.Critical, "Atención")
        '    Exit Sub
        'End If

        'If txtOrdenPago.Text = "" Then
        ' If MessageBox.Show("No ha ingresado ninguna Orden de Pago del cliente. ¿Desea continuar sin este dato?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
        ' txtOrdenPago.Focus()
        ' Exit Sub
        ' End If
        'End If

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                'If bolModo Then
                Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)
                res = AgregarRegistro_Ingreso()
                Select Case res
                    Case -3
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No pudo realizarse la insersión (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case -2
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el número de Facturación (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case -1
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case 0
                        Cancelar_Tran()
                        Util.MsgStatus(Status1, "No se pudo agregar el registro (Encabezado).", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        'Util.MsgStatus(Status1, "Guardando los remitos asociados...", My.Resources.Resources.indicator_white)
                        res = AgregarRegistro_FacturasConsumos()
                        Select Case res
                            Case -3
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo actualizar la OC.", My.Resources.Resources.stop_error.ToBitmap)
                            Case -1
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                            Case 0
                                Cancelar_Tran()
                                Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                            Case Else
                                Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                res = AgregarRegistro_CtaCte()
                                Select Case res
                                    Case -1
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar la información en la cuenta.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo registrar la información en la cuenta.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case 0
                                        Cancelar_Tran()
                                        Util.MsgStatus(Status1, "No se pudo registrar la información en la cuenta.", My.Resources.Resources.stop_error.ToBitmap)
                                        Util.MsgStatus(Status1, "No se pudo registrar la información en la cuenta.", My.Resources.Resources.stop_error.ToBitmap, True)
                                    Case Else
                                        If lblEntregaCheques.Text <> "" And lblEntregaCheques.Text <> "0" Then
                                            res = AgregarRegistro_Cheques()
                                            Select Case res
                                                Case -3
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo actualizar la OC.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case -1
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case 0
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case Else
                                                    Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                            End Select
                                        End If
                                        If lblEntregaImpuestos.Text <> "" And lblEntregaImpuestos.Text <> "0" Then
                                            res = AgregarRegistro_Impuestos()
                                            Select Case res
                                                Case -3
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo actualizar la OC.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case -1
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case 0
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case Else
                                                    Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                            End Select
                                        End If
                                        Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                        If lblEntregaTransferencias.Text <> "" And lblEntregaTransferencias.Text <> "0" Then
                                            res = AgregarRegistro_Transferencias()
                                            Select Case res
                                                Case -3
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo actualizar la OC.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case -1
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo registrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case 0
                                                    Cancelar_Tran()
                                                    Util.MsgStatus(Status1, "No se pudo regitrar los remitos.", My.Resources.Resources.stop_error.ToBitmap)
                                                Case Else
                                                    Util.MsgStatus(Status1, "Guardando la información en la cuenta...", My.Resources.Resources.indicator_white)
                                            End Select
                                        End If

                                        Util.MsgStatus(Status1, "Cerrando y generando el comprobante", My.Resources.Resources.indicator_white)
                                        Cerrar_Tran()
                                        Imprimir()

                                        Me.Close()

                                End Select
                        End Select
                End Select
                '
                ' cerrar la conexion si está abierta.
                '
                If Not conn_del_form Is Nothing Then
                    CType(conn_del_form, IDisposable).Dispose()
                End If
                'End If ' If bolModo Then
            End If 'If bolpoliticas Then
        End If 'If ReglasNegocio() Then
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        nbreformreportes = "Comprobante de Pago"

        Dim paramConsumos As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Rpt As New frmReportes

        paramConsumos.AgregarParametros("N° de Mov:", "STRING", "", False, , "", cnn)

        paramConsumos.ShowDialog()

        If cerroparametrosconaceptar = True Then
            codigo = paramConsumos.ObtenerParametros(0)

            Imprimir_LlenarTMPs(codigo)

            Rpt.MostrarReporte_PagodeClientes(codigo, Rpt)

            cerroparametrosconaceptar = False
            paramConsumos = Nothing
            cnn = Nothing
        End If

    End Sub

#End Region

#Region "   Cheques"

    Private Sub btnNuevoCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCheque.Click
        txtNroCheque.Text = ""
        txtObservacionesCheque.Text = ""
        txtMontoCheque.Text = ""
        txtPropietario.Text = ""
        txtNroCheque.Focus()
    End Sub

    Private Sub btnModificarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarCheque.Click
        'modificar = 1
        'btnAgregarCheque_Click(sender, e)

        Dim I As Integer

        For I = 0 To grdCheques.RowCount - 1
            If I <> grdCheques.CurrentRow.Index Then
                If grdCheques.Rows(I).Cells(0).Value = txtNroCheque.Text Then
                    Util.MsgStatus(Status1, "Ya existe un cheque con este Nro para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next

        grdCheques.CurrentRow.Cells(0).Value = txtNroCheque.Text
        grdCheques.CurrentRow.Cells(1).Value = cmbBanco.Text
        grdCheques.CurrentRow.Cells(2).Value = CDec(txtMontoCheque.Text)
        grdCheques.CurrentRow.Cells(3).Value = dtpFechaCheque.Value
        grdCheques.CurrentRow.Cells(4).Value = txtPropietario.Text
        grdCheques.CurrentRow.Cells(5).Value = 1 'cmbMoneda.SelectedValue
        grdCheques.CurrentRow.Cells(6).Value = txtObservacionesCheque.Text

        lblEntregaCheques.Text = "0"

        For I = 0 To grdCheques.RowCount - 1
            lblEntregaCheques.Text = CDbl(lblEntregaCheques.Text) + grdCheques.Rows(I).Cells(2).Value
        Next

        Calcular_MontoEntregado()

        btnNuevoImpuesto_Click(sender, e)

    End Sub

    Private Sub btnAgregarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarCheque.Click
        If txtNroCheque.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el número del cheque.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el número del cheque.", My.Resources.Resources.alert.ToBitmap, True)
            txtNroCheque.Focus()
            Exit Sub
        End If

        If cmbBanco.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el nombre del Banco.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el nombre del Banco.", My.Resources.Resources.alert.ToBitmap, True)
            cmbBanco.Focus()
            Exit Sub
        End If

        If txtMontoCheque.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el monto del cheque.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el monto del cheque.", My.Resources.Resources.alert.ToBitmap, True)
            txtMontoCheque.Focus()
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To grdCheques.RowCount - 1
            If grdCheques.Rows(i).Cells(0).Value = txtNroCheque.Text Then
                Util.MsgStatus(Status1, "Ya existe un cheque con este Nro para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        Next

        Try
            grdCheques.Rows.Add(txtNroCheque.Text, cmbBanco.Text, CDec(txtMontoCheque.Text), dtpFechaCheque.Value, txtPropietario.Text, 1, txtObservacionesCheque.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaCheques.Text = CDbl(lblEntregaCheques.Text) + CDbl(txtMontoCheque.Text)

        Calcular_MontoEntregado()


    End Sub

    Private Sub btnEliminarCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarCheque.Click
        Dim connection As SqlClient.SqlConnection = Nothing

        Try

            If grdCheques.CurrentRow.Cells(8).Value Is DBNull.Value Or grdCheques.CurrentRow.Cells(8).Value = False Then
                connection = SqlHelper.GetConnection(ConnStringSEI)

                If Not grdCheques.CurrentRow.Cells(7).Value Is Nothing Then
                    SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Cheques_Ingreso where idCheque = " & grdCheques.CurrentRow.Cells(7).Value)
                End If

                grdCheques.Rows.Remove(grdCheques.CurrentRow)

                lblEntregaCheques.Text = "0"

                Dim i As Integer

                For i = 0 To grdCheques.RowCount - 1
                    lblEntregaCheques.Text = CDbl(lblEntregaCheques.Text) + grdCheques.Rows(i).Cells(2).Value
                Next

                Calcular_MontoEntregado()

            Else
                MsgBox("El cheque que intenta eliminar se encuentra utilizado en otro proceso")
            End If

        Catch ex As Exception

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub grdCheques_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not grdCheques.CurrentRow Is Nothing Then
            txtNroCheque.Text = grdCheques.CurrentRow.Cells(0).Value
            cmbBanco.Text = grdCheques.CurrentRow.Cells(1).Value
            txtMontoCheque.Text = grdCheques.CurrentRow.Cells(2).Value
            Try
                dtpFechaCheque.Value = grdCheques.CurrentRow.Cells(3).Value
            Catch ex As Exception

            End Try
            txtPropietario.Text = grdCheques.CurrentRow.Cells(4).Value
            cmbMoneda.SelectedValue = grdCheques.CurrentRow.Cells(5).Value
            txtObservacionesCheque.Text = grdCheques.CurrentRow.Cells(6).Value
        Else
            txtNroCheque.Text = ""
            txtMontoCheque.Text = "0"
            dtpFechaCheque.Value = Date.Today  'grdCheques.CurrentRow.Cells(3).Value
            txtPropietario.Text = ""
            'cmbMoneda.SelectedValue = grd.CurrentRow.Cells(5).Value
            txtObservacionesCheque.Text = ""
        End If
    End Sub

#End Region

#Region "   Impuestos"

    Private Sub btnNuevoImpuesto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoImpuesto.Click

        txtNroDocumentoImpuesto.Text = ""
        cmbImpuesto.Text = ""
        txtMontoImpuesto.Text = ""
        txtObservacionesImpuestos.Text = ""
        txtNroDocumentoImpuesto.Focus()

    End Sub

    Private Sub btnModificarImpuesto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarImpuesto.Click
        Dim I As Integer

        For I = 0 To grdImpuestos.RowCount - 1
            If I <> grdImpuestos.CurrentRow.Index Then
                If grdImpuestos.Rows(I).Cells(1).Value = txtNroDocumentoImpuesto.Text And grdImpuestos.Rows(I).Cells(0).Value = cmbImpuesto.Text Then
                    Util.MsgStatus(Status1, "Ya existe este impuesto (Impuesto + Nro de Documento) para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next

        grdImpuestos.CurrentRow.Cells(0).Value = cmbImpuesto.Text
        grdImpuestos.CurrentRow.Cells(1).Value = txtNroDocumentoImpuesto.Text
        grdImpuestos.CurrentRow.Cells(2).Value = CDec(txtMontoImpuesto.Text)
        grdImpuestos.CurrentRow.Cells(3).Value = txtObservacionesImpuestos.Text
        grdImpuestos.CurrentRow.Cells(4).Value = cmbImpuesto.SelectedValue

        lblEntregaImpuestos.Text = "0"

        For i = 0 To grdImpuestos.RowCount - 1
            lblEntregaImpuestos.Text = CDbl(lblEntregaImpuestos.Text) + grdImpuestos.Rows(I).Cells(2).Value
        Next

        Calcular_MontoEntregado()

        btnNuevoImpuesto_Click(sender, e)

    End Sub

    Private Sub btnAgregarImpuesto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarImpuesto.Click

        If cmbImpuesto.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el Tipo de Impuesto.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el Tipo de Impuesto.", My.Resources.Resources.alert.ToBitmap, True)
            cmbImpuesto.Focus()
            Exit Sub
        End If

        If txtMontoImpuesto.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el Monto del Impuesto.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el Monto del Impuesto.", My.Resources.Resources.alert.ToBitmap, True)
            txtMontoImpuesto.Focus()
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To grdImpuestos.RowCount - 1
            If grdImpuestos.Rows(i).Cells(1).Value = txtNroDocumentoImpuesto.Text And grdImpuestos.Rows(i).Cells(0).Value = cmbImpuesto.Text Then
                Util.MsgStatus(Status1, "Ya existe este impuesto (Impuesto + Nro de Documento) para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        Next

        Try
            grdImpuestos.Rows.Add(cmbImpuesto.Text, txtNroDocumentoImpuesto.Text, CDec(txtMontoImpuesto.Text), txtObservacionesImpuestos.Text, cmbImpuesto.SelectedValue)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaImpuestos.Text = CDbl(lblEntregaImpuestos.Text) + CDbl(txtMontoImpuesto.Text)

        Calcular_MontoEntregado()

        btnNuevoImpuesto_Click(sender, e)

    End Sub

    Private Sub btnEliminarImpuesto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEliminarImpuesto.Click
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            If Not grdImpuestos.CurrentRow.Cells(5).Value Is Nothing Then
                SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Impuestos_Ingreso where id = " & grdImpuestos.CurrentRow.Cells(5).Value)
            End If

            grdImpuestos.Rows.Remove(grdImpuestos.CurrentRow)

            lblEntregaImpuestos.Text = "0"

            Dim i As Integer

            For i = 0 To grdImpuestos.RowCount - 1
                lblEntregaImpuestos.Text = CDbl(lblEntregaImpuestos.Text) + grdImpuestos.Rows(i).Cells(2).Value
            Next

            Calcular_MontoEntregado()

            'If modificar = 0 Then
            '    btnNuevoImpuesto_Click(sender, e)
            'End If


        Catch ex As Exception

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

    Private Sub grdImpuestos_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not grdImpuestos.CurrentRow Is Nothing Then
            txtNroDocumentoImpuesto.Text = grdImpuestos.CurrentRow.Cells(1).Value
            cmbImpuesto.Text = grdImpuestos.CurrentRow.Cells(0).Value
            txtMontoImpuesto.Text = grdImpuestos.CurrentRow.Cells(2).Value
            txtObservacionesImpuestos.Text = grdImpuestos.CurrentRow.Cells(3).Value
        Else
            txtNroDocumentoImpuesto.Text = ""
            txtMontoImpuesto.Text = "0"
            txtObservacionesImpuestos.Text = ""
        End If
    End Sub

#End Region

#Region "   Transferencias"

    Private Sub btnNuevoTransferencia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoTransferencia.Click
        txtNroOpCliente.Text = ""
        txtMontoTransf.Text = "0"
        cmbCuentaDestino.Text = ""
        cmbCuentaOrigen.Text = ""
        txtObservacionesTransf.Text = ""

        txtNroOpCliente.Focus()
    End Sub

    Private Sub btnModificarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarTransf.Click
        Dim I As Integer

        For I = 0 To grdTransferencias.RowCount - 1
            If I <> grdTransferencias.CurrentRow.Index Then
                If grdTransferencias.Rows(I).Cells(1).Value = txtNroDocumentoImpuesto.Text And grdImpuestos.Rows(I).Cells(0).Value = cmbImpuesto.Text Then
                    Util.MsgStatus(Status1, "Ya existe este impuesto (Impuesto + Nro de Documento) para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                    Exit Sub
                End If
            End If
        Next

        grdTransferencias.CurrentRow.Cells(0).Value = txtNroOpCliente.Text
        grdTransferencias.CurrentRow.Cells(1).Value = cmbCuentaOrigen.Text
        grdTransferencias.CurrentRow.Cells(2).Value = CDec(txtMontoTransf.Text)
        grdTransferencias.CurrentRow.Cells(3).Value = cmbCuentaDestino.Text
        grdTransferencias.CurrentRow.Cells(4).Value = dtpFechaTransf.Value
        grdTransferencias.CurrentRow.Cells(5).Value = 1
        grdTransferencias.CurrentRow.Cells(6).Value = cmbBancoOrigen.Text
        grdTransferencias.CurrentRow.Cells(7).Value = cmbBancoDestino.Text
        grdTransferencias.CurrentRow.Cells(8).Value = txtObservacionesTransf.Text

        lblEntregaTransferencias.Text = "0"

        For I = 0 To grdTransferencias.RowCount - 1
            lblEntregaTransferencias.Text = CDbl(lblEntregaTransferencias.Text) + grdTransferencias.Rows(I).Cells(2).Value
        Next

        Calcular_MontoEntregado()

        btnAgregarTransf_Click(sender, e)

    End Sub


    Private Sub btnAgregarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarTransf.Click

        If txtMontoTransf.Text = "" Then
            Util.MsgStatus(Status1, "Debe ingresar el Monto de la Transferencia.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar el Monto de la Transferencia.", My.Resources.Resources.alert.ToBitmap, True)
            txtMontoTransf.Focus()
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To grdTransferencias.RowCount - 1
            If grdTransferencias.Rows(i).Cells(0).Value = txtNroOpCliente.Text And txtNroOpCliente.Text <> "" Then
                Util.MsgStatus(Status1, "Ya existe una transfernecia con este Nro de OP para este pago. Por favor, revise esta información.", My.Resources.Resources.alert.ToBitmap, True)
                Exit Sub
            End If
        Next

        Try
            grdTransferencias.Rows.Add(txtNroOpCliente.Text, cmbCuentaOrigen.Text, CDec(txtMontoTransf.Text), cmbCuentaDestino.Text, dtpFechaTransf.Value, 1, cmbBancoOrigen.Text, cmbBancoDestino.Text, txtObservacionesTransf.Text)
        Catch ex As Exception
            Exit Sub
        End Try

        lblEntregaTransferencias.Text = CDbl(lblEntregaTransferencias.Text) + CDbl(txtMontoTransf.Text)

        Calcular_MontoEntregado()

        btnNuevoTransferencia_Click(sender, e)

    End Sub

    Private Sub btnEliminarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarTransf.Click
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)

            If Not grdTransferencias.CurrentRow.Cells(9).Value Is Nothing Then
                SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "DELETE FROM TMP_Transferencias_Ingreso where idtransferencia = " & grdTransferencias.CurrentRow.Cells(9).Value)
            End If

            grdTransferencias.Rows.Remove(grdTransferencias.CurrentRow)

            lblEntregaTransferencias.Text = "0"

            Dim i As Integer

            For i = 0 To grdTransferencias.RowCount - 1
                lblEntregaTransferencias.Text = CDbl(lblEntregaTransferencias.Text) + grdTransferencias.Rows(i).Cells(2).Value
            Next

            Calcular_MontoEntregado()

        Catch ex As Exception

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub grdTransferencias_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not grdTransferencias.CurrentRow Is Nothing Then
            txtNroOpCliente.Text = grdTransferencias.CurrentRow.Cells(0).Value
            cmbCuentaOrigen.Text = grdTransferencias.CurrentRow.Cells(1).Value
            txtMontoTransf.Text = grdTransferencias.CurrentRow.Cells(2).Value
            cmbCuentaDestino.Text = grdTransferencias.CurrentRow.Cells(3).Value
            dtpFechaTransf.Value = grdTransferencias.CurrentRow.Cells(4).Value
            cmbMonedaTransf.SelectedValue = grdTransferencias.CurrentRow.Cells(5).Value
            cmbBancoOrigen.Text = grdTransferencias.CurrentRow.Cells(6).Value
            cmbBancoDestino.Text = grdTransferencias.CurrentRow.Cells(7).Value
            txtObservacionesTransf.Text = grdTransferencias.CurrentRow.Cells(8).Value
        Else
            txtNroOpCliente.Text = ""
            txtMontoTransf.Text = "0"
            cmbCuentaDestino.Text = ""
            cmbCuentaOrigen.Text = ""
            txtObservacionesTransf.Text = ""
        End If
    End Sub

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Calcular_MontoEntregado()

        Dim redondeo As Double = 0

        Try
            redondeo = CDbl(IIf(txtRedondeo.Text = "", 0, txtRedondeo.Text))

            lblEntregado.Text = CDbl(IIf(txtEntregaContado.Text = "", 0, txtEntregaContado.Text)) + CDbl(IIf(lblEntregaCheques.Text = "", 0, lblEntregaCheques.Text)) + CDbl(IIf(lblEntregaTransferencias.Text = "", 0, lblEntregaTransferencias.Text)) + CDbl(IIf(lblEntregaImpuestos.Text = "", 0, lblEntregaImpuestos.Text)) + redondeo

        Catch ex As Exception

        End Try

        If band = 1 Then

            lblResto.Text = Math.Round(CDbl(lblTotalaPagar.Text) - CDbl(lblEntregado.Text), 2)

            If CDec(lblEntregado.Text) > CDec(lblTotalaPagar.Text) Then
                Util.MsgStatus(Status1, "Verifique el último Monto ingresado. El monto Total Entregado ahora es mayor al monto Total a Pagar.", My.Resources.Resources.stop_error.ToBitmap)
            Else
                Util.MsgStatus(Status1, "Pago verificado. Los montos coinciden.", My.Resources.Resources.ok.ToBitmap)
            End If
        End If

    End Sub
End Class