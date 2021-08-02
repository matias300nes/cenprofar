Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmNotaCredito

    Dim bolpoliticas As Boolean
    Dim llenandoCombo As Boolean = False
    Dim band As Integer


#Region "Componentes Formulario"

    Private Sub frmProveedores_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                btnNuevo_Click(sender, e)
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmProveedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AsignarPermisos(UserID, Me.Name, ALTA, MODIFICA, BAJA, BAJA_FISICA)

        band = 0

        configurarform()
        asignarTags()

        LlenarComboClientes()
        LlenarComboFacturas()

        SQL = "exec spNotasCredito_Select_ALL"

        LlenarGrilla()

        Permitir = True

        CargarCajas()

        PrepararBotones()

        band = 1

    End Sub

    Private Sub txtSubtotal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotal.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True

            If txtIVA.Text <> "" And txtSubtotal.Text <> "" Then
                txtMontoIVA.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
                txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
            Else
                txtTotal.Text = FormatNumber(CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)), 2)
            End If

            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSubtotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubtotal.LostFocus
        If txtIVA.Text <> "" And txtSubtotal.Text <> "" Then
            txtMontoIVA.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
            txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
        Else
            txtTotal.Text = FormatNumber(CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)), 2)
        End If
    End Sub

    Private Sub txtIva_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIVA.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True

            If txtSubtotal.Text <> "" And txtIVA.Text <> "" Then
                txtMontoIVA.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
                txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
            Else
                If txtIVA.Text = "" Then
                    txtMontoIVA.Text = "0"
                End If
                txtTotal.Text = CDbl(txtSubtotal.Text)
            End If

            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtIVA_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIVA.LostFocus
        If txtSubtotal.Text <> "" And txtIVA.Text <> "" Then
            txtMontoIVA.Text = FormatNumber(CDbl(txtSubtotal.Text) * (CDbl(txtIVA.Text) / 100), 2)
            txtTotal.Text = FormatNumber(CDbl(txtSubtotal.Text) * (1 + (CDbl(txtIVA.Text) / 100)), 2)
        Else
            If txtIVA.Text = "" Then
                txtMontoIVA.Text = "0"
            End If
            txtTotal.Text = CDbl(txtSubtotal.Text)
        End If
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        band = 0

        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)

        txtNroFactura.Visible = False
        txtCliente.Visible = False

        cmbCliente.Visible = True
        cmbFactura.Visible = True

        txtCODIGO.Focus()

        band = 1

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                res = AgregarRegistro()
                Select Case res
                    Case -3
                        Util.MsgStatus(Status1, "Se produjo un error al intentar insertar la NC.", My.Resources.Resources.stop_error.ToBitmap)
                    Case -2
                        Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                    Case -1
                        Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    Case 0
                        Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                    Case Else
                        Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
                        bolModo = False

                        cmbCliente.Visible = False
                        cmbFactura.Visible = False

                        txtNroFactura.Visible = True
                        txtCliente.Visible = True

                        btnActualizar_Click(sender, e)
                End Select
            Else
                Util.MsgStatus(Status1, "No tiene permiso para Agregar registros.", My.Resources.stop_error.ToBitmap)
            End If
            'Else
            '    'If MODIFICA Then
            '    If MessageBox.Show("Está seguro que desea modificar la información de la  seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            '        If Not (grd.CurrentRow.Cells(10).Value Is DBNull.Value) Then
            '            Util.MsgStatus(Status1, "No se puede modificar esa Nota de Crédito porque está dentro de un proceso de pago.", My.Resources.stop_error.ToBitmap, True)
            '            Exit Sub
            '        End If

            '        res = ActualizarRegistro()
            '        Select Case res
            '            Case -3
            '                Util.MsgStatus(Status1, "Ya existe otro Registro con este mismo Código.", My.Resources.stop_error.ToBitmap)
            '            Case -2
            '                Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
            '            Case -1
            '                Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
            '            Case 0
            '                Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
            '            Case Else
            '                Util.MsgStatus(Status1, "Se ha actualizado el registro.", My.Resources.Resources.ok.ToBitmap)
            '                bolModo = False
            '                btnActualizar_Click(sender, e)
            '        End Select
            '        '    Else
            '        '    Util.MsgStatus(Status1, "No tiene permiso para modificar registros.", My.Resources.stop_error.ToBitmap)
            '        'End If
            '    End If

        End If

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer
        'If BAJA_FISICA Then

        If MessageBox.Show("Está seguro que desea eliminar la Nota de Crédito seleccionada?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            If grd.CurrentRow.Cells(10).Value <> 0 Then
                Util.MsgStatus(Status1, "No se puede eliminar la Nota de Crédito porque está dentro de un proceso de pago.", My.Resources.stop_error.ToBitmap, True)
                Exit Sub
            End If

            Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)


            res = EliminarRegistro()
            Select Case res
                Case -2
                    Util.MsgStatus(Status1, "El registro no existe.", My.Resources.stop_error.ToBitmap)
                Case -1
                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                Case 0
                    Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
                Case Else
                    Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)
                    bolModo = False
                    btnActualizar_Click(sender, e)
            End Select

        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'Dim paramproveedores As New frmParametros
        'Dim Cnn As New SqlConnection(ConnStringSEI)
        'Dim codigo As String
        'Dim reporteproveedores As New frmReportes


        ''nbreformreportes = "Listado de Proveedores por Código"

        'paramproveedores.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", Cnn)
        'paramproveedores.ShowDialog()
        ' ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
        ' ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
        'If cerroparametrosconaceptar = True Then
        '    ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR A LA FUNCION..
        '    codigo = paramproveedores.ObtenerParametros(0)
        '    reporteproveedores.MostrarMaestroProveedores(codigo, reporteproveedores, "ACER")
        '    cerroparametrosconaceptar = False
        '    paramproveedores = Nothing
        '    Cnn = Nothing
        'End If
    End Sub

#End Region

#Region "Funciones"

    Private Function AgregarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try


            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_NotaCredito As New SqlClient.SqlParameter
                param_NotaCredito.ParameterName = "@NotaCredito"
                param_NotaCredito.SqlDbType = SqlDbType.BigInt
                param_NotaCredito.Value = DBNull.Value
                param_NotaCredito.Direction = ParameterDirection.InputOutput

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbCliente.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_idfactura As New SqlClient.SqlParameter
                param_idfactura.ParameterName = "@idfacturacion"
                param_idfactura.SqlDbType = SqlDbType.BigInt
                param_idfactura.Value = cmbFactura.SelectedValue
                param_idfactura.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@monto"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = CDbl(txtTotal.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spNotasCredito_Insert", _
                                              param_id, param_NotaCredito, param_idcliente, _
                                              param_idfactura, param_total, param_res)

                    txtID.Text = param_id.Value

                    txtCODIGO.Text = param_NotaCredito.Value

                    AgregarRegistro = param_res.Value

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

    Private Function ActualizarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try


            Try
                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.Input

                Dim param_idcliente As New SqlClient.SqlParameter
                param_idcliente.ParameterName = "@idcliente"
                param_idcliente.SqlDbType = SqlDbType.BigInt
                param_idcliente.Value = cmbCliente.SelectedValue
                param_idcliente.Direction = ParameterDirection.Input

                Dim param_idfactura As New SqlClient.SqlParameter
                param_idfactura.ParameterName = "@idfacturacion"
                param_idfactura.SqlDbType = SqlDbType.BigInt
                param_idfactura.Value = cmbFactura.SelectedValue
                param_idfactura.Direction = ParameterDirection.Input

                Dim param_total As New SqlClient.SqlParameter
                param_total.ParameterName = "@monto"
                param_total.SqlDbType = SqlDbType.Decimal
                param_total.Precision = 18
                param_total.Scale = 2
                param_total.Value = CDbl(txtTotal.Text)
                param_total.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spNotasCredito_Update", _
                                               param_id, param_idcliente, _
                                               param_idfactura, param_total, param_res)

                    ActualizarRegistro = param_res.Value

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

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing


        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
            param_id.Value = CType(txtID.Text, Long)
            param_id.Direction = ParameterDirection.Input

            Dim param_idfacturacion As New SqlClient.SqlParameter("@idfacturacion", SqlDbType.BigInt, ParameterDirection.Input)
            param_id.Value = CType(txtID.Text, Long)
            param_id.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.Output

            Try

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spNotasCredito_Delete", param_id, _
                                          param_idfacturacion, param_res)
                res = param_res.Value

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

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Notas de Crédito"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 75))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)
        Me.grd.Size = New Size(p)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            'Me.Top = ARRIBA
            'Me.Left = IZQUIERDA
            'Else
            '    Me.Top = 0
            '    Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.Top = 0
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2

    End Sub

    Private Sub asignarTags()
        txtID.Tag = "0"
        txtCODIGO.Tag = "3"
        'cmbCliente.Tag = "2"
        txtCliente.Tag = "2"
        'cmbFactura.Tag = "4"
        txtNroFactura.Tag = "4"
        txtSubtotal.Tag = "5"
        txtIVA.Tag = "6"
        txtMontoIVA.Tag = "7"
        txtTotal.Tag = "8"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If txtSubtotal.Text = "0" Then
            Util.MsgStatus(Status1, "Debe ingresar un valor para la factura.", My.Resources.Resources.alert.ToBitmap)
            Util.MsgStatus(Status1, "Debe ingresar un valor para la factura.", My.Resources.Resources.alert.ToBitmap, True)
            txtSubtotal.Focus()
            Exit Sub
        End If

        bolpoliticas = True

    End Sub

    Private Sub LlenarComboClientes()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select DISTINCT c.id, c.nombre " & _
                                " from Clientes c JOIN Facturacion f ON f.idcliente = c.id where c.eliminado=0 " & _
                                " and anuladofisico = 1 and notacredito = 0 and f.id NOT IN (select idfacturacion FROM " & _
                                " notascredito where eliminado = 0) order by C.NOMBRE")

            ds_Cli.Dispose()

            With Me.cmbCliente
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "id"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
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
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub LlenarComboFacturas()
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenandoCombo = False
            Exit Sub
        End Try

        Try
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select id, nrofactura " & _
                                " from Facturacion where idcliente = " & CType(cmbCliente.SelectedValue, Long) & " and anuladofisico = 1 " & _
                                " and id NOT IN (select idfacturacion FROM " & _
                                " notascredito where eliminado = 0) order by nrofactura")

            ds_Cli.Dispose()

            With Me.cmbFactura
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nrofactura"
                .ValueMember = "id"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
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
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        llenandoCombo = False

    End Sub

    Private Sub BuscarMontoFacturas()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim dt As New DataTable
        Dim sqltxt2 As String

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            sqltxt2 = " select total, subtotal, montoiva, iva" & _
                                " from Facturacion where id = " & CType(cmbFactura.SelectedValue, Long)

            Dim cmd As New SqlCommand(sqltxt2, connection)
            Dim da As New SqlDataAdapter(cmd)

            da.Fill(dt)

            txtSubtotal.Text = dt.Rows(0)("subtotal").ToString()
            txtIVA.Text = dt.Rows(0)("iva").ToString()
            txtMontoIVA.Text = dt.Rows(0)("montoiva").ToString()
            txtTotal.Text = dt.Rows(0)("total").ToString()

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


    Private Sub cmbCliente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged
        If llenandoCombo = False Then
            LlenarComboFacturas()
        End If
    End Sub

    Private Sub cmbFactura_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFactura.SelectedIndexChanged
        If band = 1 Then
            BuscarMontoFacturas()
        End If
    End Sub
End Class