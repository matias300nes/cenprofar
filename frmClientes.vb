Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet
Imports Microsoft.Office.Interop



Public Class frmClientes

    Dim bolpoliticas As Boolean
    Private ds_2 As DataSet
    Dim permitir_evento_CellChanged As Boolean
    Dim tran As SqlClient.SqlTransaction
    Dim Cell_X As Integer, Cell_Y As Integer
    Public Origen As Integer
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim Band As Integer


    Enum ColumnasDelGridItems
        ID_Contacto = 0
        IdCliente = 1
        Codigo_Contacto = 2
        Nombre_Contacto = 3
        Telefono_Contacto = 4
        Email_Contacto = 5
        Celular_Contacto = 6



    End Enum

#Region "Componentes Formulario"

    Private Sub frmClientes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3 'nuevo
                If bolModo = True Then
                    If MessageBox.Show("No ha guardado el Cliente Nuevo que está realizando. ¿Está seguro que desea continuar sin Grabar y hacer un Nuevo Cliente?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        btnNuevo_Click(sender, e)
                    End If
                Else
                    btnNuevo_Click(sender, e)
                End If
            Case Keys.F4 'grabar
                btnGuardar_Click(sender, e)
        End Select
    End Sub

    Private Sub frmClientes_ev_CellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ev_CellChanged
        'If permitir_evento_CellChanged Then
        '    If txtID.Text <> "" Then
        '        LlenarGridItems()
        '    End If
        'End If
    End Sub

    Private Sub frmClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()
        asignarTags()
        Band = 0

        LlenarCondicionIVA()
        LlenarLocalidad()
        LlenarProvincia()
        LLenarPrecioLista()
        LlenarRepartidor()
        LlenarcmbTipoDocumento_App(Me.cmbDocTipo, ConnStringSEI)


        SQL = "exec spClientes_Select_All @Eliminado = 0"

        LlenarGrilla()
        Permitir = True

        CargarCajas()

        PrepararBotones()

        'Setear_Grilla()
        'InicializarGridItems(grdItems)

        If bolModo = True Then
            'LlenarGridItems()
            btnNuevo_Click(sender, e)
        Else
            ' LlenarGridItems()
        End If

        If grd.RowCount > 0 Then
            grd.Rows(0).Selected = True
            grd.CurrentCell = grd.Rows(0).Cells(1)
        End If

        grd.Columns(3).Visible = False
        grd.Columns(5).Visible = False
        grd.Columns(6).Visible = False
        grd.Columns(13).Visible = False
        grd.Columns(14).Visible = False
        grd.Columns(16).Visible = False
        grd.Columns(18).Visible = False

        ''ContarCaracNom()

        Band = 1
        permitir_evento_CellChanged = True

    End Sub

    Private Sub txtid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
     Handles txtID.KeyPress, txtCODIGO.KeyPress, txtNOMBRE.KeyPress, txtDIRECCION.KeyPress, txtCODPOSTAL.KeyPress, _
      txtTELEFONO.KeyPress, txtFAX.KeyPress, _
      txtCuit.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub chkEliminados_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEliminados.CheckedChanged

        btnNuevo.Enabled = Not chkEliminados.Checked
        btnGuardar.Enabled = Not chkEliminados.Checked
        btnCancelar.Enabled = Not chkEliminados.Checked
        btnEliminar.Enabled = Not chkEliminados.Checked

        If chkEliminados.Checked = True Then
            SQL = "exec spClientes_Select_All @Eliminado = 1"
        Else
            SQL = "exec spClientes_Select_All @Eliminado = 0"
        End If

        LlenarGrilla()

        'LlenarGridItems()

        If grd.RowCount = 0 Then
            btnActivar.Enabled = False
        Else
            btnActivar.Enabled = chkEliminados.Checked
        End If

    End Sub

    'Private Sub cmbListaPrecio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbListaPrecio.SelectedIndexChanged

    'Me fijo si el combo seleccionado es mayorita para que se tome como campo obligatorio de email

    'If cmbListaPrecio.Text.Contains("MAYORISTA") Then
    '    txtEmail.AccessibleName = "EMAIL*"
    '    'lblEmail.Text = "Email/Usuario PPO*"
    '    lblEmail.Text = "Email PPO*"
    '    txtusuario.Enabled = True
    'Else
    '    txtEmail.AccessibleName = ""
    '    lblEmail.Text = "Email"
    '    txtusuario.Enabled = False
    'End If

    'End Sub

    Private Sub PicExcelExportar_Click(sender As Object, e As EventArgs) Handles PicExcelExportar.Click

        Dim texto As String = ""
        Dim ds_Empresa As Data.DataSet
        ' Cambiamos el cursor por el de carga
        Me.Cursor = Cursors.WaitCursor

        ' Conexión con la base de datos
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ' Creamos todo lo necesario para un excel
            Dim appXL As Excel.Application
            Dim wbXl As Excel.Workbook
            Dim shXL As Excel.Worksheet
            Dim indice As Integer = 2
            appXL = CreateObject("Excel.Application")
            appXL.Visible = False 'Para que no se muestre mientras se crea
            wbXl = appXL.Workbooks.Add
            shXL = wbXl.ActiveSheet
            ' Añadimos las cabeceras de las columnas con formato en negrita
            Dim formatRange As Excel.Range
            formatRange = shXL.Range("a1")
            formatRange.EntireRow.Font.Bold = True

            shXL.Cells(1, 1).Value = "CODIGO"
            shXL.Cells(1, 2).Value = "NOMBRE"
            shXL.Cells(1, 3).Value = "NRODOC"
            shXL.Cells(1, 4).Value = "DIRECCION"
            shXL.Cells(1, 5).Value = "CODPOSTAL"
            shXL.Cells(1, 6).Value = "PROVINCIA"
            shXL.Cells(1, 7).Value = "LOCALIDAD"
            shXL.Cells(1, 8).Value = "TELEFONO"
            shXL.Cells(1, 9).Value = "EMAIL"
            shXL.Cells(1, 10).Value = "PRECIOLISTA"
            shXL.Cells(1, 11).Value = "REPARTIDOR"

            Dim sqlstring As String = "SELECT P.codigo	AS 'Código',P.nombre AS 'Nombre',P.CUIT	AS 'NroDoc',P.direccion	AS 'Dirección'," & _
                                      "P.codpostal	AS 'CodPostal',	P.provincia	AS 'Provincia',	P.localidad	AS 'Localidad',	P.telefono	AS 'Teléfono'," & _
                                      "P.email AS 'Email', L.Descripcion	AS 'PrecioLista',CONCAT(E.apellido,' ',E.Nombre) AS 'Repartidor' " & _
                                      "FROM Clientes P INNER JOIN Lista_Precios L  ON L.ID = P.IDPrecioLista INNER JOIN Empleados E on e.Codigo = p.Repartidor " & _
                                      "INNER JOIN Responsables R on r.Codigo = p.CondicionIVA WHERE P.eliminado = 0 AND P.CODIGO <> 1 ORDER BY P.Nombre ASC"

            ds_Empresa = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
            ds_Empresa.Dispose()

            Dim Fila As Integer

            Fila = 0

            ' Cargamos la información en el excel
            For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1

                shXL.Cells(indice, 1).Value = ds_Empresa.Tables(0).Rows(Fila)(0)
                shXL.Cells(indice, 2).Value = ds_Empresa.Tables(0).Rows(Fila)(1)
                shXL.Cells(indice, 3).Value = ds_Empresa.Tables(0).Rows(Fila)(2)
                shXL.Cells(indice, 4).Value = ds_Empresa.Tables(0).Rows(Fila)(3)
                shXL.Cells(indice, 5).Value = ds_Empresa.Tables(0).Rows(Fila)(4)
                shXL.Cells(indice, 6).Value = ds_Empresa.Tables(0).Rows(Fila)(5)
                shXL.Cells(indice, 7).Value = ds_Empresa.Tables(0).Rows(Fila)(6)
                shXL.Cells(indice, 8).Value = ds_Empresa.Tables(0).Rows(Fila)(7)
                shXL.Cells(indice, 9).Value = ds_Empresa.Tables(0).Rows(Fila)(8)
                shXL.Cells(indice, 10).Value = ds_Empresa.Tables(0).Rows(Fila)(9)
                shXL.Cells(indice, 11).Value = ds_Empresa.Tables(0).Rows(Fila)(10)

                indice += 1
            Next

            'ajusto el tamaño de todas las columnas 
            shXL.Columns("A:K").AutoFit()
            ' Mostramos un dialog para que el usuario indique donde quiere guardar el excel
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Title = "Guardar documento Excel"
            saveFileDialog1.Filter = "Excel File|*.xlsx"
            saveFileDialog1.FileName = "Clientes " + texto + " " + Format(Date.Now, "dd-MM-yyyy").ToString
            saveFileDialog1.ShowDialog()
            ' Guardamos el excel en la ruta que ha especificado el usuario
            wbXl.SaveAs(saveFileDialog1.FileName)
            ' Cerramos el workbook
            appXL.Workbooks.Close()
            ' Eliminamos el objeto excel
            appXL.Quit()
        Catch ex As Exception
            MessageBox.Show("Error al exportar los datos a excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Cerramos la conexión y ponemos el cursor por defecto de nuevo
            ' conexion.Close()
            Me.Cursor = Cursors.Arrow
        End Try

        'llamo a la funcion que mata los porcesos de excel
        KillAllExcels()

    End Sub

    Private Sub PicExcelExportar_MouseHover(sender As Object, e As EventArgs) Handles PicExcelExportar.MouseHover
        ToolTip1.Show("Haga click para exportar la lista de clientes a un archivo excel", PicExcelExportar)
    End Sub

    Private Sub txtNOMBRE_TextChanged(sender As Object, e As EventArgs) Handles txtNOMBRE.TextChanged
        ContarCaracNom()
    End Sub

#End Region

#Region "Botones"

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        bolModo = True
        Util.MsgStatus(Status1, "Haga click en [Guardar] despues de completar los datos.")
        PrepararBotones()
        Util.LimpiarTextBox(Me.Controls)
        LlenarLocalidad()
        LlenarProvincia()
        chkUsuarioWEB.Checked = False
        'PrepararGridItems()

        cmbDocTipo.SelectedIndex = 0

        txtCODIGO.Focus()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim res As Integer

        Dim ModoActual As Boolean

        ModoActual = bolModo

        If bolModo = False Then
            If MessageBox.Show("¿Está seguro que desea modificar el Cliente seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Util.MsgStatus(Status1, "Guardando el registro...", My.Resources.Resources.indicator_white)

        If ReglasNegocio() Then
            Verificar_Datos()
            If bolpoliticas Then
                If bolModo Then
                    res = AgregarRegistro()
                    Select Case res
                        Case -10
                            Util.MsgStatus(Status1, "Está intentando ingresar un Cliente que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Está intentando ingresar un Cliente que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                            txtCuit.Focus()
                        Case -2
                            Util.MsgStatus(Status1, "Ya existe otro Cliente con este mismo Código.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el Cliente.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el Cliente.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case Else
                            Util.MsgStatus(Status1, "Se agregó el Cliente.", My.Resources.Resources.ok.ToBitmap)
                            Cerrar_Tran()
                            bolModo = False
                            'btnActualizar_Click(sender, e)
                    End Select
                Else
                    res = ActualizarRegistro()
                    Select Case res
                        Case -10
                            Util.MsgStatus(Status1, "Está intentando ingresar un CUIT que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap)
                            Util.MsgStatus(Status1, "Está intentando ingresar un CUIT que ya existe en el sistema. Por favor, verifique esta información.", My.Resources.Resources.stop_error.ToBitmap, True)
                            Cancelar_Tran()
                            txtCuit.Focus()
                        Case -3
                            Util.MsgStatus(Status1, "Ya existe otro Contacto con este mismo Código.", My.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case -2
                            Util.MsgStatus(Status1, "El registro ya existe.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case -1
                            Util.MsgStatus(Status1, "No se pudo actualizar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case 0
                            Util.MsgStatus(Status1, "No se pudo agregar el registro.", My.Resources.Resources.stop_error.ToBitmap)
                            Cancelar_Tran()
                        Case Else
                            Util.MsgStatus(Status1, "Se ha actualizado el Cliente.", My.Resources.Resources.ok.ToBitmap)
                            Cerrar_Tran()
                            bolModo = False
                            'btnActualizar_Click(sender, e)
                    End Select
                End If

                If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then

                    Try

                        Dim sqlstring As String

                        If ModoActual = True Then

                            sqlstring = "INSERT INTO [dbo].[Clientes]([ID],[IDPrecioLista],[Codigo],[Nombre],[TipoDocumento],[CUIT],[Direccion],[CodPostal]," & _
                                        "[Localidad],[Provincia],[Telefono],[Fax],[Email],[Contacto],[Observaciones],[Contrasena],[Usuario],[UsuarioWEB]," & _
                                        "[Eliminado] ,[DateAdd], [Repartidor],[Promo],[CondicionIVA],[MontoMaxCred],[DiasMaxCred],[CtaCte]) values ( " & txtID.Text & "," & cmbListaPrecio.SelectedValue & ",'" & txtCODIGO.Text & "','" & _
                                         txtNOMBRE.Text & "'," & cmbDocTipo.SelectedValue & "," & txtCuit.Text & ",'" & txtDIRECCION.Text & "','" & _
                                         txtCODPOSTAL.Text & "','" & cmbLocalidad.Text & "','" & cmbProvincia.Text & "','" & txtTELEFONO.Text & "','" & _
                                         txtFAX.Text & "','" & txtEmail.Text & "',' ',' ','123456','" & txtusuario.Text & "'," & IIf(chkUsuarioWEB.Checked = True, 1, 0) & "," & _
                                         "0,'" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "','" & cmbRepartidor.SelectedValue & "'," & _
                                         IIf(chkPromo.Checked = True, 1, 0) & ",'" & cmbCondicionIVA.SelectedValue & "'," & txtMontoMax.Text & "," & txtDiasMax.Text & "," & chkCtaCte.Checked & ")"

                        Else

                            sqlstring = "UPDATE [dbo].[Clientes] SET [IDPrecioLista] = " & cmbListaPrecio.SelectedValue & "," & _
                                        "[Codigo] = '" & txtCODIGO.Text & "'," & _
                                        "[Nombre] = '" & txtNOMBRE.Text & "'," & _
                                        "[TipoDocumento] = " & cmbDocTipo.SelectedValue & "," & _
                                        "[CUIT] = " & txtCuit.Text & "," & _
                                        "[Direccion] = '" & txtDIRECCION.Text & "'," & _
                                        "[CodPostal] = '" & txtCODPOSTAL.Text & "'," & _
                                        "[Localidad] = '" & cmbLocalidad.Text & "'," & _
                                        "[Provincia] = '" & cmbProvincia.Text & "'," & _
                                        "[Telefono] = '" & txtTELEFONO.Text & "'," & _
                                        "[Fax] = '" & txtFAX.Text & "'," & _
                                        "[Email] = '" & txtEmail.Text & "'," & _
                                        "[Usuario] = '" & txtusuario.Text & "'," & _
                                        "[UsuarioWEB] = " & IIf(chkUsuarioWEB.Checked = True, 1, 0) & "," & _
                                        "[DateUpd] = '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "'," & _
                                        "[Repartidor] = '" & cmbRepartidor.SelectedValue & "'," & _
                                        "[Promo] = " & IIf(chkPromo.Checked = True, 1, 0) & "," & _
                                        "[CondicionIVA] = '" & cmbCondicionIVA.SelectedValue & "'," & _
                                        "[MontoMaxCred] = " & txtMontoMax.Text & "," & _
                                        "[DiasMaxCred]  = " & txtDiasMax.Text & "," & _
                                        "[CtaCte] = " & chkCtaCte.Checked & " " & _
                                        " WHERE Codigo = '" & txtCODIGO.Text & "'"
                        End If

                        tranWEB.Sql_Set(sqlstring)

                    Catch ex As Exception
                        MsgBox("No se puede sincronizar en la Web el cliente actual. Ejecute el botón sincronizar para actualizar el servidor WEB. " + ex.Message)
                    End Try
                End If
                MDIPrincipal.NoActualizarBase = False
                'actualizo
                btnActualizar_Click(sender, e)
                chkCredito.Checked = False
            End If
        End If

        If Origen = 1 Then
            Me.Close()
        End If

    End Sub

    'Private Overloads Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
    '    chkCredito.Checked = False
    'End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim res As Integer

        If MessageBox.Show("Está seguro que desea eliminar el Cliente seleccionado?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Util.MsgStatus(Status1, "Eliminando el registro...", My.Resources.Resources.indicator_white)
        res = EliminarRegistro()
        Select Case res
            Case -20
                Util.MsgStatus(Status1, "El Cliente seleccionado no se puede eliminar porque tiene movimientos en Facturación.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El Cliente seleccionado no se puede eliminar porque tiene movimientos en Facturación.", My.Resources.stop_error.ToBitmap, True)
            Case -30
                Util.MsgStatus(Status1, "El Cliente seleccionado no se puede eliminar porque tiene movimientos en Presupuestos.", My.Resources.stop_error.ToBitmap)
                Util.MsgStatus(Status1, "El Cliente seleccionado no se puede eliminar porque tiene movimientos en Presupuestos.", My.Resources.stop_error.ToBitmap, True)
            Case -1
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
            Case 0
                Util.MsgStatus(Status1, "No se pudo borrar el registro.", My.Resources.stop_error.ToBitmap)
            Case Else
                Util.MsgStatus(Status1, "Se ha borrado el registro.", My.Resources.ok.ToBitmap)

                If Me.grd.RowCount = 0 Then
                    bolModo = True
                    PrepararBotones()
                    Util.LimpiarTextBox(Me.Controls)
                End If
        End Select
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim rpt As New frmReportes

        nbreformreportes = "Listado de Clientes por Código"

        param.AgregarParametros("Código :", "STRING", "", False, txtCODIGO.Text.ToString, "", cnn)
        param.ShowDialog()

        If cerroparametrosconaceptar = True Then

            codigo = param.ObtenerParametros(0)

            rpt.Clientes_Maestro_App(codigo, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
            param = Nothing
            cnn = Nothing
        End If

    End Sub

    Private Overloads Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'If txtID.Text <> "" Then
        '    LlenarGridItems()
        'End If
        chkCredito.Checked = False

    End Sub

    Private Sub btnActivar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivar.Click
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Update As Data.DataSet

        If MessageBox.Show("Está por activar nuevamente el Cliente: " & grd.CurrentRow.Cells(2).Value.ToString & ". Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            'llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Clientes SET Eliminado = 0 WHERE id = " & grd.CurrentRow.Cells(0).Value)
            ds_Update.Dispose()

            If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                Try
                    Dim sqlstring As String

                    sqlstring = "UPDATE [dbo].[Clientes] SET [Eliminado] = 0, [DateUpd] = '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "' WHERE Codigo = '" & txtCODIGO.Text & "'"
                    tranWEB.Sql_Set(sqlstring)

                Catch ex As Exception
                    'MsgBox(ex.Message)
                    MsgBox("No se puede sincronizar en la Web el cliente actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                End Try
            End If


            SQL = "exec spClientes_Select_All @Eliminado = 1"

            LlenarGrilla()

            If grd.RowCount = 0 Then
                btnActivar.Enabled = False
            End If

            Util.MsgStatus(Status1, "El Cliente se activó correctamente.", My.Resources.ok.ToBitmap)

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

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Clientes"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(IIf(Me.Size.Width <= AnchoMinimoForm, AnchoMinimoForm, Me.Size.Width), Me.grd.Location.Y + Me.grd.Size.Height + 65)
        Me.Size = New Size(Me.Size.Width, (Screen.PrimaryScreen.WorkingArea.Height - 65))

        Dim p As New Size(GroupBox1.Size.Width, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 7)
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
        txtCODIGO.Tag = "1"
        txtNOMBRE.Tag = "2"
        cmbCondicionIVA.Tag = "4"
        cmbDocTipo.Tag = "6"
        txtCuit.Tag = "7"
        txtDIRECCION.Tag = "8"
        txtCODPOSTAL.Tag = "9"
        cmbProvincia.Tag = "10"
        cmbLocalidad.Tag = "11"
        txtTELEFONO.Tag = "12"
        txtFAX.Tag = "13"
        txtEmail.Tag = "14"
        cmbListaPrecio.Tag = "15"
        chkUsuarioWEB.Tag = "16"
        cmbRepartidor.Tag = "17"
        chkPromo.Tag = "18"
        txtMontoMax.Tag = "19"
        txtDiasMax.Tag = "20"
        chkCtaCte.Tag = "21"
    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        If lblContaNom.Text.Length > 30 Then
            MsgBox("El nombre del cliente no puede superar los 30 caracteres. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            Exit Sub
        End If

        If cmbCondicionIVA.SelectedValue = "1" And cmbDocTipo.Text.Contains("DNI") Then
            MsgBox("El tipo de documento no es válido para un responsable incripto. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            cmbDocTipo.Focus()
            Exit Sub
        End If

        If txtMontoMax.Text = "" Then
            txtMontoMax.Text = "0"
        End If
        If txtDiasMax.Text = "" Then
            txtDiasMax.Text = "0"
        End If


        If CDbl(txtMontoMax.Text) = 0 And CDbl(txtDiasMax.Text) = 0 Then
            'MsgBox("El monto y los días máximos de crédito están en cero, no se aplicará restricciones a la hora de realizar una venta al cliente seleccionado.", MsgBoxStyle.Information, "Atención")
            MsgBox("El monto y los días máximos de crédito están en cero, se necesitara de una autorización de un empleado autorizado a la hora de realizar una venta al cliente seleccionado.", MsgBoxStyle.Information, "Atención")
        End If

        If CDbl(txtMontoMax.Text) = 0 And CDbl(txtDiasMax.Text) > 0 Then
            MsgBox("La cantidad de días máximo de crédito no es válido. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            txtMontoMax.Focus()
            Exit Sub
        End If

        If CDbl(txtMontoMax.Text) > 0 And CDbl(txtDiasMax.Text) = 0 Then
            MsgBox("El monto máximo de crédito no es válido. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
            txtDiasMax.Focus()
            Exit Sub
        End If

        'Select Case cmbDocTipo.Text
        '    Case "CUIL"
        '        If txtCuit.Text.Length <> 11 Then
        '            MsgBox("El nro de CUIL no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
        '            txtCuit.Focus()
        '            Exit Sub
        '        End If
        '    Case "CUIT"
        '        If txtCuit.Text.Length <> 11 Then
        '            MsgBox("El nro de CUIT no cumple con el requisito de 11 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
        '            txtCuit.Focus()
        '            Exit Sub
        '        End If
        '    Case "DNI"
        '        If txtCuit.Text.Length <> 8 Then
        '            MsgBox("El nro de DNI no cumple con el requisito de 8 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
        '            txtCuit.Focus()
        '            Exit Sub
        '        End If
        'End Select

        'If txtCuit.Text = "" Then
        '    txtCuit.Text = "0"
        'End If


        If chkUsuarioWEB.Checked = True Then
            If txtEmail.Text.ToString = "" Then
                If txtCuit.Text = "0" Then
                    MsgBox("Por favor ingrese un número de Documento o Email para poder generar un usuario PPO.", MsgBoxStyle.Information, "Atención")
                    txtEmail.Focus()
                    Exit Sub
                Else
                    Select Case cmbDocTipo.Text
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
                            If txtCuit.Text.Length < 7 Or txtCuit.Text.Length > 8 Then
                                MsgBox("El nro de DNI no cumple con el requisito de 7/8 dígitos. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                                txtCuit.Focus()
                                Exit Sub
                            End If
                    End Select
                End If
            End If
        End If

        'controlo que el mail contenga un @
        If txtEmail.Text.Length > 0 Then
            If Not txtEmail.Text.Contains("@") Then
                '("El email ingresado no contiene @. Por favor, controle el dato.")
                MsgBox("El email ingresado no contiene @. Por favor, controle el dato.", MsgBoxStyle.Information, "Atención")
                txtEmail.Focus()
                Exit Sub
            End If
        End If


        bolpoliticas = True

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
            .BackgroundColor = SystemColors.ActiveBorder 'Color.DarkGray ' color del fondo del grid...
            .BorderStyle = BorderStyle.Fixed3D
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
            .AllowUserToAddRows = True 'indica si se muestra al usuario la opción de agregar filas
            .AllowUserToDeleteRows = True 'indica si el usuario puede eliminar filas de DataGridView.
            .AllowUserToOrderColumns = False 'indica si el usuario puede cambiar manualmente de lugar las columnas..
            .ReadOnly = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect 'indica cómo se pueden seleccionar las celdas de DataGridView.
            .MultiSelect = False 'indica si el usuario puede seleccionar a la vez varias celdas, filas o columnas del control DataGridView.
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells     'indica cómo se determina el alto de las filas. 
            .AllowUserToResizeColumns = True 'indica si los usuarios pueden cambiar el tamaño de las columnas.
            .AllowUserToResizeRows = True 'indica si los usuarios pueden cambiar el tamaño de las filas.
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize 'indica si el alto de los encabezados de columna es ajustable y si puede ser ajustado por el usuario o automáticamente para adaptarse al contenido de los encabezados. 
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
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

    Private Sub LlenarLocalidad()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT localidad FROM Clientes ORDER BY Localidad")
            ds.Dispose()

            With cmbLocalidad
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "localidad"
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

    Private Sub LlenarProvincia()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT provincia FROM Clientes ORDER BY provincia")
            ds.Dispose()

            With cmbProvincia
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "provincia"
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

    Private Sub LLenarPrecioLista()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo , Descripcion FROM Lista_Precios WHERE Eliminado = 0 ORDER BY Descripcion")
            ds.Dispose()

            With cmbListaPrecio
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub LlenarRepartidor()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 and Repartidor = 1 ORDER BY Vendedor")
            ds.Dispose()

            With cmbRepartidor
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Vendedor"
                .ValueMember = "Codigo"
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

    Private Sub LlenarCondicionIVA()
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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub KillAllExcels()
        Try

            Dim proc As System.Diagnostics.Process

            For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
                If proc.MainWindowTitle.Trim.Length = 0 Then
                    'proc.GetCurrentProcess.StartInfo
                    proc.Kill()
                End If
            Next
        Catch ex As Exception
            My.Computer.FileSystem.WriteAllText("C:\errores.log", Format(Now, "01/MM/yyy HH:mm") & " - " & ex.Message & vbCrLf, True)
        End Try
    End Sub

    Private Sub ContarCaracNom()
        Dim nombre As String = txtNOMBRE.Text.ToString.Replace(" ", "")
        lblContaNom.Text = nombre.Length.ToString
        If nombre.Length > 30 Then
            lblContaNom.ForeColor = Color.Red
        Else
            lblContaNom.ForeColor = Color.Black
        End If
    End Sub

#End Region

#Region "Funciones"

    Private Function AgregarRegistro() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        'Dim res As Integer = 0

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
            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = DBNull.Value
            param_id.Direction = ParameterDirection.InputOutput

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = DBNull.Value
            param_codigo.Direction = ParameterDirection.InputOutput

            Dim param_idpreciolista As New SqlClient.SqlParameter
            param_idpreciolista.ParameterName = "@idPrecioLista"
            param_idpreciolista.SqlDbType = SqlDbType.BigInt
            param_idpreciolista.Value = cmbListaPrecio.SelectedValue
            param_idpreciolista.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.Char
            param_nombre.Size = 30
            param_nombre.Value = txtNOMBRE.Text
            param_nombre.Direction = ParameterDirection.Input

            Dim param_direccion As New SqlClient.SqlParameter
            param_direccion.ParameterName = "@direccion"
            param_direccion.SqlDbType = SqlDbType.VarChar
            param_direccion.Size = 100
            param_direccion.Value = txtDIRECCION.Text
            param_direccion.Direction = ParameterDirection.Input

            Dim param_codpostal As New SqlClient.SqlParameter
            param_codpostal.ParameterName = "@codpostal"
            param_codpostal.SqlDbType = SqlDbType.VarChar
            param_codpostal.Size = 10
            param_codpostal.Value = txtCODPOSTAL.Text
            param_codpostal.Direction = ParameterDirection.Input

            Dim param_localidad As New SqlClient.SqlParameter
            param_localidad.ParameterName = "@localidad"
            param_localidad.SqlDbType = SqlDbType.VarChar
            param_localidad.Size = 50
            param_localidad.Value = cmbLocalidad.Text
            param_localidad.Direction = ParameterDirection.Input

            Dim param_provincia As New SqlClient.SqlParameter
            param_provincia.ParameterName = "@provincia"
            param_provincia.SqlDbType = SqlDbType.VarChar
            param_provincia.Size = 50
            param_provincia.Value = cmbProvincia.Text
            param_provincia.Direction = ParameterDirection.Input

            Dim param_condicioniva As New SqlClient.SqlParameter
            param_condicioniva.ParameterName = "@condicioniva"
            param_condicioniva.SqlDbType = SqlDbType.VarChar
            param_condicioniva.Size = 3
            param_condicioniva.Value = cmbCondicionIVA.SelectedValue
            param_condicioniva.Direction = ParameterDirection.Input

            Dim param_tipodocumento As New SqlClient.SqlParameter
            param_tipodocumento.ParameterName = "@tipodoc"
            param_tipodocumento.SqlDbType = SqlDbType.Int
            param_tipodocumento.Value = CInt(cmbDocTipo.SelectedValue)
            param_tipodocumento.Direction = ParameterDirection.Input

            Dim param_cuit As New SqlClient.SqlParameter
            param_cuit.ParameterName = "@cuit"
            param_cuit.SqlDbType = SqlDbType.BigInt
            param_cuit.Value = txtCuit.Text
            param_cuit.Direction = ParameterDirection.Input

            Dim param_telefono As New SqlClient.SqlParameter
            param_telefono.ParameterName = "@telefono"
            param_telefono.SqlDbType = SqlDbType.VarChar
            param_telefono.Size = 50
            param_telefono.Value = txtTELEFONO.Text
            param_telefono.Direction = ParameterDirection.Input

            Dim param_fax As New SqlClient.SqlParameter
            param_fax.ParameterName = "@fax"
            param_fax.SqlDbType = SqlDbType.VarChar
            param_fax.Size = 30
            param_fax.Value = txtFAX.Text
            param_fax.Direction = ParameterDirection.Input

            Dim param_email As New SqlClient.SqlParameter
            param_email.ParameterName = "@email"
            param_email.SqlDbType = SqlDbType.VarChar
            param_email.Size = 100
            param_email.Value = txtEmail.Text
            param_email.Direction = ParameterDirection.Input


            Dim param_usuario As New SqlClient.SqlParameter
            param_usuario.ParameterName = "@usuario"
            param_usuario.SqlDbType = SqlDbType.VarChar
            param_usuario.Size = 100
            param_usuario.Value = txtusuario.Text
            param_usuario.Direction = ParameterDirection.Input

            Dim param_usuarioweb As New SqlClient.SqlParameter
            param_usuarioweb.ParameterName = "@usuarioweb"
            param_usuarioweb.SqlDbType = SqlDbType.Bit
            param_usuarioweb.Value = chkUsuarioWEB.Checked
            param_usuarioweb.Direction = ParameterDirection.Input

            Dim param_repartidor As New SqlClient.SqlParameter
            param_repartidor.ParameterName = "@repartidor"
            param_repartidor.SqlDbType = SqlDbType.VarChar
            param_repartidor.Size = 10
            param_repartidor.Value = cmbRepartidor.SelectedValue
            param_repartidor.Direction = ParameterDirection.Input

            Dim param_promo As New SqlClient.SqlParameter
            param_promo.ParameterName = "@promo"
            param_promo.SqlDbType = SqlDbType.Bit
            param_promo.Value = chkPromo.Checked
            param_promo.Direction = ParameterDirection.Input

            Dim param_montomaxcred As New SqlClient.SqlParameter
            param_montomaxcred.ParameterName = "@montomaxcred"
            param_montomaxcred.SqlDbType = SqlDbType.Decimal
            param_montomaxcred.Precision = 18
            param_montomaxcred.Scale = 2
            param_montomaxcred.Value = IIf(txtMontoMax.Text = "", 0, txtMontoMax.Text)
            param_montomaxcred.Direction = ParameterDirection.Input

            Dim param_diasmaxcred As New SqlClient.SqlParameter
            param_diasmaxcred.ParameterName = "@diasmaxcred"
            param_diasmaxcred.SqlDbType = SqlDbType.BigInt
            param_diasmaxcred.Value = IIf(txtDiasMax.Text = "", 0, txtDiasMax.Text)
            param_diasmaxcred.Direction = ParameterDirection.Input

            Dim param_ctacte As New SqlClient.SqlParameter
            param_ctacte.ParameterName = "@ctacte"
            param_ctacte.SqlDbType = SqlDbType.Bit
            param_ctacte.Value = chkCtaCte.Checked
            param_ctacte.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spClientes_Insert", param_id, _
                                          param_tipodocumento, param_cuit, param_nombre, param_idpreciolista, _
                                          param_direccion, param_codpostal, param_localidad, param_codigo, param_condicioniva, _
                                          param_provincia, param_telefono, param_fax, param_email, param_usuario, _
                                          param_usuarioweb, param_repartidor, param_promo, param_ctacte, _
                                          param_montomaxcred, param_diasmaxcred, param_useradd, param_res)

                txtID.Text = param_id.Value
                txtCODIGO.Text = param_codigo.Value

                AgregarRegistro = param_res.Value


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

            If ex.Message.ToString.Contains("UNIQUE KEY") Or ex.Message.ToString.Contains("clave duplicada") Then
                AgregarRegistro = -10
            Else
                MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
                  + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
                  "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End Try

    End Function

    Private Function ActualizarRegistro() As Integer
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

            Dim param_id As New SqlClient.SqlParameter
            param_id.ParameterName = "@id"
            param_id.SqlDbType = SqlDbType.BigInt
            param_id.Value = txtID.Text
            param_id.Direction = ParameterDirection.Input

            Dim param_idpreciolista As New SqlClient.SqlParameter
            param_idpreciolista.ParameterName = "@idPrecioLista"
            param_idpreciolista.SqlDbType = SqlDbType.BigInt
            param_idpreciolista.Value = cmbListaPrecio.SelectedValue
            param_idpreciolista.Direction = ParameterDirection.Input

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.Char
            param_nombre.Size = 30
            param_nombre.Value = txtNOMBRE.Text
            param_nombre.Direction = ParameterDirection.Input

            Dim param_direccion As New SqlClient.SqlParameter
            param_direccion.ParameterName = "@direccion"
            param_direccion.SqlDbType = SqlDbType.VarChar
            param_direccion.Size = 100
            param_direccion.Value = txtDIRECCION.Text
            param_direccion.Direction = ParameterDirection.Input

            Dim param_codpostal As New SqlClient.SqlParameter
            param_codpostal.ParameterName = "@codpostal"
            param_codpostal.SqlDbType = SqlDbType.VarChar
            param_codpostal.Size = 10
            param_codpostal.Value = txtCODPOSTAL.Text
            param_codpostal.Direction = ParameterDirection.Input

            Dim param_localidad As New SqlClient.SqlParameter
            param_localidad.ParameterName = "@localidad"
            param_localidad.SqlDbType = SqlDbType.VarChar
            param_localidad.Size = 50
            param_localidad.Value = cmbLocalidad.Text
            param_localidad.Direction = ParameterDirection.Input

            Dim param_provincia As New SqlClient.SqlParameter
            param_provincia.ParameterName = "@provincia"
            param_provincia.SqlDbType = SqlDbType.VarChar
            param_provincia.Size = 50
            param_provincia.Value = cmbProvincia.Text
            param_provincia.Direction = ParameterDirection.Input

            Dim param_condicioniva As New SqlClient.SqlParameter
            param_condicioniva.ParameterName = "@condicioniva"
            param_condicioniva.SqlDbType = SqlDbType.VarChar
            param_condicioniva.Size = 3
            param_condicioniva.Value = cmbCondicionIVA.SelectedValue
            param_condicioniva.Direction = ParameterDirection.Input

            Dim param_tipodocumento As New SqlClient.SqlParameter
            param_tipodocumento.ParameterName = "@tipodoc"
            param_tipodocumento.SqlDbType = SqlDbType.Int
            param_tipodocumento.Value = CInt(cmbDocTipo.SelectedValue)
            param_tipodocumento.Direction = ParameterDirection.Input

            Dim param_cuit As New SqlClient.SqlParameter
            param_cuit.ParameterName = "@cuit"
            param_cuit.SqlDbType = SqlDbType.BigInt
            param_cuit.Value = txtCuit.Text
            param_cuit.Direction = ParameterDirection.Input

            Dim param_telefono As New SqlClient.SqlParameter
            param_telefono.ParameterName = "@telefono"
            param_telefono.SqlDbType = SqlDbType.VarChar
            param_telefono.Size = 50
            param_telefono.Value = txtTELEFONO.Text
            param_telefono.Direction = ParameterDirection.Input

            Dim param_fax As New SqlClient.SqlParameter
            param_fax.ParameterName = "@fax"
            param_fax.SqlDbType = SqlDbType.VarChar
            param_fax.Size = 30
            param_fax.Value = txtFAX.Text
            param_fax.Direction = ParameterDirection.Input

            Dim param_email As New SqlClient.SqlParameter
            param_email.ParameterName = "@email"
            param_email.SqlDbType = SqlDbType.VarChar
            param_email.Size = 100
            param_email.Value = txtEmail.Text
            param_email.Direction = ParameterDirection.Input

            Dim param_usuario As New SqlClient.SqlParameter
            param_usuario.ParameterName = "@usuario"
            param_usuario.SqlDbType = SqlDbType.VarChar
            param_usuario.Size = 100
            param_usuario.Value = txtusuario.Text
            param_usuario.Direction = ParameterDirection.Input

            Dim param_montomaxcred As New SqlClient.SqlParameter
            param_montomaxcred.ParameterName = "@montomaxcred"
            param_montomaxcred.SqlDbType = SqlDbType.Decimal
            param_montomaxcred.Precision = 18
            param_montomaxcred.Scale = 2
            param_montomaxcred.Value = IIf(txtMontoMax.Text = "", 0, txtMontoMax.Text)
            param_montomaxcred.Direction = ParameterDirection.Input

            Dim param_diasmaxcred As New SqlClient.SqlParameter
            param_diasmaxcred.ParameterName = "@diasmaxcred"
            param_diasmaxcred.SqlDbType = SqlDbType.BigInt
            param_diasmaxcred.Value = IIf(txtDiasMax.Text = "", 0, txtDiasMax.Text)
            param_diasmaxcred.Direction = ParameterDirection.Input

            Dim param_usuarioweb As New SqlClient.SqlParameter
            param_usuarioweb.ParameterName = "@usuarioweb"
            param_usuarioweb.SqlDbType = SqlDbType.Bit
            param_usuarioweb.Value = chkUsuarioWEB.Checked
            param_usuarioweb.Direction = ParameterDirection.Input

            Dim param_repartidor As New SqlClient.SqlParameter
            param_repartidor.ParameterName = "@repartidor"
            param_repartidor.SqlDbType = SqlDbType.VarChar
            param_repartidor.Size = 10
            param_repartidor.Value = cmbRepartidor.SelectedValue
            param_repartidor.Direction = ParameterDirection.Input

            Dim param_promo As New SqlClient.SqlParameter
            param_promo.ParameterName = "@promo"
            param_promo.SqlDbType = SqlDbType.Bit
            param_promo.Value = chkPromo.Checked
            param_promo.Direction = ParameterDirection.Input

            Dim param_ctacte As New SqlClient.SqlParameter
            param_ctacte.ParameterName = "@ctacte"
            param_ctacte.SqlDbType = SqlDbType.Bit
            param_ctacte.Value = chkCtaCte.Checked
            param_ctacte.Direction = ParameterDirection.Input

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
                SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "spClientes_Update", param_id, _
                                          param_tipodocumento, param_cuit, param_nombre, _
                                          param_direccion, param_codpostal, param_idpreciolista, param_condicioniva, _
                                          param_localidad, param_provincia, param_telefono, param_fax, param_email, _
                                          param_usuario, param_usuarioweb, param_repartidor, param_promo, _
                                          param_montomaxcred, param_diasmaxcred, param_ctacte, param_userupd, param_res)

                ActualizarRegistro = param_res.Value

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
                ActualizarRegistro = -10
            Else
                MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
                  + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
                  "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End Try

    End Function

    Private Function EliminarRegistro() As Integer
        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spClientes_Delete", param_id, param_userdel, param_res)
                res = param_res.Value

                If res > 0 Then

                    If MDIPrincipal.NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
                        Try
                            Dim sqlstring As String

                            sqlstring = "UPDATE [dbo].[Clientes] SET [Eliminado] = 1, [DateDel] = '" & Format(Date.Now, "MM/dd/yyyy").ToString & " " & Format(Date.Now, "hh:mm:ss").ToString & "' WHERE Codigo = '" & txtCODIGO.Text & "'"
                            tranWEB.Sql_Set(sqlstring)

                        Catch ex As Exception
                            'MsgBox(ex.Message)
                            MsgBox("No se puede sincronizar en la Web el cliente actual. Ejecute el botón sincronizar para actualizar el servidor WEB.")
                        End Try
                    End If

                    Util.BorrarGrilla(grd)
                    EliminarRegistro = res

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
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
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

    Private Sub txtCuit_LostFocus(sender As Object, e As EventArgs) Handles txtCuit.LostFocus



        Try
            'Dim Cuit As String = txtCuit.Text.ToString
            'Dim maximo As Integer = Cuit.Length
            Dim minimo As Integer = txtCuit.Text.Length - 3

            txtusuario.Text = txtNOMBRE.Text.Substring(0, 3).ToLower + txtCuit.Text.Substring(minimo).ToLower

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub chkUsuarioWEB_CheckedChanged(sender As Object, e As EventArgs) Handles chkUsuarioWEB.CheckedChanged
        txtusuario.Enabled = chkUsuarioWEB.Checked
        If chkUsuarioWEB.Checked = True Then
            'txtEmail.AccessibleName = "EMAIL*"
            'lblEmail.Text = "Email/Usuario PPO*"
            lblEmail.Text = "Email PPO*"
            lblUsuarioPP0.Text = "Usuario PPO"
            'hago focus en el mail
            txtEmail.Focus()
        Else
            txtEmail.AccessibleName = ""
            lblEmail.Text = "Email"
            lblUsuarioPP0.Text = "Usuario"
        End If

    End Sub

    Private Sub cmbListaPrecio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbListaPrecio.SelectedIndexChanged

        If Band = 1 Then
            Try
                If bolModo = True Then
                    If cmbListaPrecio.SelectedValue = "3" Then
                        chkPromo.Checked = True
                    Else
                        chkPromo.Checked = False
                    End If
                End If
            Catch ex As Exception

            End Try
        End If


    End Sub

    Private Sub chkCredito_CheckedChanged(sender As Object, e As EventArgs) Handles chkCredito.CheckedChanged

        txtMontoMax.Enabled = chkCredito.Checked
        txtDiasMax.Enabled = chkCredito.Checked

        If chkCredito.Checked = True Then

            Dim habilitar As Boolean = MDIPrincipal.ControlUsuarioAutorizado(MDIPrincipal.EmpleadoLogueado)
            If habilitar = False Then
                MDIPrincipal.Autorizar = False
                Dim Au As New frmUsuarioModo
                Au.ShowDialog()
                'me fijo si autorizó
                If MDIPrincipal.Autorizar = False Then
                    chkCredito.Checked = False
                    Exit Sub
                End If
            End If
            txtMontoMax.Focus()
        End If
    End Sub



End Class