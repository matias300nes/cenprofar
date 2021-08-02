Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmMovDia_Caja_Operacion

    Dim modo As String
    Public FechaMovimiento As Date
    Dim FilaSeleccionada As Integer = -1

#Region "Vinculación al teclado"


    Private Sub Movimientos_de_caja_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Refresh()
    End Sub

    Private Sub Movimientos_de_caja_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'If e.KeyValue = Keys.Escape Then
        '    btnVolver_Click(sender, e)
        'ElseIf e.KeyValue = Keys.F10 Then
        '    Txt_monto.Focus()
        'End If
    End Sub

    Private Sub txt_monto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles Txt_monto.KeyPress, txt_observaciones.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

#End Region

#Region "Formulario"

    Private Sub Ajuste_de_planilla_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblOperacion.Text = MDIPrincipal.OperacionCaja
        FechaMovimiento = Date.Now
        lblFecha.Text = "Fecha: " & FechaMovimiento
        modo = "insert"
        GroupBox1.BackColor = SystemColors.Control
        Txt_monto.Text = ""
        txt_observaciones.Text = ""
        DG_refresh()

        DG_movimientos.Columns(0).Visible = False
        DG_movimientos.Columns(1).Visible = False
        DG_movimientos.Columns(3).Visible = False
        DG_movimientos.Columns(4).Visible = False
        DG_movimientos.Columns(6).Visible = False

        DG_movimientos.Columns(2).Width = 100
        DG_movimientos.Columns(5).Width = 400

        Txt_monto.Focus()

    End Sub

    'Private Sub Movimientos_de_caja_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    '    Dim bandera As Boolean = True
    '    Dim bandera_MovCaja As Boolean = True
    'End Sub

    Private Sub DG_refresh()
        'Dim fecha As String = FechaMovimiento.Date.Year.ToString & "-" & FechaMovimiento.Date.Month.ToString & "-" & FechaMovimiento.Date.Day.ToString
        'DG_movimientos.DataSource = tran.Sql_Get("Select Id, Id_Sucursal, Monto, Movimiento, Fecha, Observacion as Observación, Deleted from Movimientos_de_caja WHERE movimiento = '" & lblOperacion.Text & "' and id_sucursal=" & id_sucursal & " and deleted= " & IIf(chkEliminados.Checked = True, 1, 0) & " and fecha='" & fecha & "'").Tables("t")
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        'Dim param_idAlmacen As New SqlClient.SqlParameter
        'param_idAlmacen.ParameterName = "@idalmacen"
        'param_idAlmacen.SqlDbType = SqlDbType.BigInt
        'param_idAlmacen.Value = IIf(frmVentasWEB.txtidAlmacen.Text = "", frmVentasWEB.cmbAlmacenes.SelectedValue, frmVentasWEB.txtidAlmacen.Text)
        'param_idAlmacen.Direction = ParameterDirection.Input

        'Dim param_movimiento As New SqlClient.SqlParameter
        'param_movimiento.ParameterName = "@movimiento"
        'param_movimiento.SqlDbType = SqlDbType.VarChar
        'param_movimiento.Size = 50
        'param_movimiento.Value = lblOperacion.Text.ToUpper
        'param_movimiento.Direction = ParameterDirection.Input

        'Dim param_fecha As New SqlClient.SqlParameter
        'param_fecha.ParameterName = "@fecha"
        'param_fecha.SqlDbType = SqlDbType.DateTime
        'param_fecha.Value = Date.Now
        'param_fecha.Direction = ParameterDirection.Input

        'Dim param_Eliminado As New SqlClient.SqlParameter
        'param_Eliminado.ParameterName = "@eliminado"
        'param_Eliminado.SqlDbType = SqlDbType.Bit
        'param_Eliminado.Value = chkEliminados.Checked
        'param_Eliminado.Direction = ParameterDirection.Input

        Dim sqlstring As String = "SELECT Id, IdAlmacen, Monto, Movimiento,Fecha , Observacion as Observación,Eliminado FROM Movimientos_Caja " & _
                                  "WHERE movimiento = '" & lblOperacion.Text & "' " & _
                                  "AND idalmacen= " & IIf(frmVentasWEB.txtidAlmacen.Text = "", frmVentasWEB.cmbAlmacenes.SelectedValue, frmVentasWEB.txtidAlmacen.Text) & " " & _
                                  "AND convert(varchar(10),Fecha,103) = convert(varchar(10),GETDATE(),103) " & _
                                  "AND Eliminado = " & IIf(chkEliminados.Checked = True, 1, 0) & " " & _
                                  "Order BY id DESC"

        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
        ds.Dispose()

        DG_movimientos.DataSource = ds.Tables(0).DefaultView

        DG_movimientos.Enabled = True

        If DG_movimientos.Rows.Count = 0 And btnAceptar.Text = "Activar" Then
            btnAceptar.Enabled = False
        Else
            btnAceptar.Enabled = True
        End If

    End Sub

    Private Sub DG_movimientos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_movimientos.CellClick
        If DG_movimientos.RowCount > 0 Then
            modo = "update"
            If chkEliminados.Checked = False Then
                btnAceptar.Text = "Modificar"
            End If
            Txt_monto.Text = DG_movimientos.CurrentRow.Cells(2).Value
            txt_observaciones.Text = DG_movimientos.CurrentRow.Cells(5).Value
        Else
            MsgBox("No hay movimiento seleccionado para editar/activar", vbInformation, "Control de errores")
        End If
    End Sub

    Private Sub DG_Movimientos_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DG_movimientos.CellMouseUp
        FilaSeleccionada = e.RowIndex
    End Sub

    Private Sub chkEliminados_CheckedChanged(sender As Object, e As EventArgs) Handles chkEliminados.CheckedChanged
        If chkEliminados.Checked = True Then
            btnAceptar.Text = "Activar"
        Else
            btnAceptar.Text = "Aceptar"
        End If

        DG_refresh()
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
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_idAlmacen As New SqlClient.SqlParameter
                param_idAlmacen.ParameterName = "@idalmacen"
                param_idAlmacen.SqlDbType = SqlDbType.BigInt
                param_idAlmacen.Value = IIf(frmVentasWEB.txtidAlmacen.Text = "", frmVentasWEB.cmbAlmacenes.SelectedValue, frmVentasWEB.txtidAlmacen.Text)
                param_idAlmacen.Direction = ParameterDirection.Input

                Dim param_monto As New SqlClient.SqlParameter
                param_monto.ParameterName = "@monto"
                param_monto.SqlDbType = SqlDbType.Decimal
                param_monto.Precision = 18
                param_monto.Size = 2
                param_monto.Value = Txt_monto.Text
                param_monto.Direction = ParameterDirection.Input

                Dim param_observacion As New SqlClient.SqlParameter
                param_observacion.ParameterName = "@observacion"
                param_observacion.SqlDbType = SqlDbType.VarChar
                param_observacion.Size = 200
                param_observacion.Value = txt_observaciones.Text.ToUpper
                param_observacion.Direction = ParameterDirection.Input

                Dim param_movimiento As New SqlClient.SqlParameter
                param_movimiento.ParameterName = "@movimiento"
                param_movimiento.SqlDbType = SqlDbType.VarChar
                param_movimiento.Size = 50
                param_movimiento.Value = lblOperacion.Text.ToUpper
                param_movimiento.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = Date.Now
                param_fecha.Direction = ParameterDirection.Input


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

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMovimientos_de_caja_Insert", param_id, param_idAlmacen, param_monto, param_observacion, param_movimiento, param_fecha, param_useradd, param_res)

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
                param_id.Value = CType(DG_movimientos.CurrentRow.Cells(0).Value, Long)
                param_id.Direction = ParameterDirection.Input

                Dim param_idAlmacen As New SqlClient.SqlParameter
                param_idAlmacen.ParameterName = "@idalmacen"
                param_idAlmacen.SqlDbType = SqlDbType.BigInt
                param_idAlmacen.Value = IIf(frmVentasWEB.txtidAlmacen.Text = "", frmVentasWEB.cmbAlmacenes.SelectedValue, frmVentasWEB.txtidAlmacen.Text)
                param_idAlmacen.Direction = ParameterDirection.Input

                Dim param_monto As New SqlClient.SqlParameter
                param_monto.ParameterName = "@monto"
                param_monto.SqlDbType = SqlDbType.Decimal
                param_monto.Precision = 18
                param_monto.Size = 2
                param_monto.Value = Txt_monto.Text
                param_monto.Direction = ParameterDirection.Input

                Dim param_observacion As New SqlClient.SqlParameter
                param_observacion.ParameterName = "@observacion"
                param_observacion.SqlDbType = SqlDbType.VarChar
                param_observacion.Size = 200
                param_observacion.Value = txt_observaciones.Text.ToUpper
                param_observacion.Direction = ParameterDirection.Input

                Dim param_movimiento As New SqlClient.SqlParameter
                param_movimiento.ParameterName = "@movimiento"
                param_movimiento.SqlDbType = SqlDbType.VarChar
                param_movimiento.Size = 50
                param_movimiento.Value = lblOperacion.Text.ToUpper
                param_movimiento.Direction = ParameterDirection.Input

                Dim param_fecha As New SqlClient.SqlParameter
                param_fecha.ParameterName = "@fecha"
                param_fecha.SqlDbType = SqlDbType.DateTime
                param_fecha.Value = Date.Now
                param_fecha.Direction = ParameterDirection.Input

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMovimientos_de_caja_Update", param_id, param_idAlmacen, param_monto, param_observacion, param_movimiento, param_fecha, param_userupd, param_res)

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
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter("@id", SqlDbType.BigInt, ParameterDirection.Input)
                param_id.Value = CType(DG_movimientos.CurrentRow.Cells(0).Value, Long)
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

                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMovimientos_de_caja_Delete", param_id, param_userdel, param_res)
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

#End Region

#Region "Botones"

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim res As Integer
        Cursor = Cursors.WaitCursor

        Try
            If modo = "insert" Then
                For i As Integer = 0 To DG_movimientos.RowCount - 1
                    If DG_movimientos.Rows(i).Cells(3).Value = "Fondos Apertura" Then
                        MsgBox("Ya hay un movimiento del tipo 'Fondos Apertura' en el día seleccionado.", vbInformation, "Control de errores")
                        'Cursor = Cursors.Default
                        Exit Sub
                    End If
                Next
            End If

            If lblOperacion.Text = "Gastos" And LTrim(RTrim(txt_observaciones.Text)) = "" Then
                MsgBox("Es necesario ingresar una observación para este tipo de operación", vbInformation, "Control de errores")
                DG_movimientos.Enabled = True
                txt_observaciones.Focus()
                Cursor = Cursors.Default
                Exit Sub
            End If

            If Txt_monto.Text = "" Then
                MsgBox("Debe Ingresar el Monto para la Operación", vbInformation, "Control de errores")
                Cursor = Cursors.Default
                Exit Sub
            End If

            If CDbl(Txt_monto.Text) = 0 Then
                MsgBox("Debe Ingresar el Monto para la Operación", vbInformation, "Control de errores")
                Cursor = Cursors.Default
                Exit Sub
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try

        If modo = "insert" Then
            'Dim fecha As String = FechaMovimiento.Date.Year.ToString & "-" & FechaMovimiento.Date.Month.ToString & "-" & FechaMovimiento.Date.Day.ToString
            'Dim sql As String = "exec Insert_movimiento_de_caja @id_sucursal= " & 1 & ", @observacion='" & txt_observaciones.Text & "', @monto=" & FormatNumber(CDbl(Txt_monto.Text), 2) & ", @movimiento='" & lblOperacion.Text & "', @fecha='" & fecha & "'"
            res = AgregarRegistro()
            Select Case res
                Case -2
                    MsgBox("Ya existe una apertura de caja.", MsgBoxStyle.Exclamation)
                    Cursor = Cursors.Default
                    Exit Sub
                Case -1
                    MsgBox("No se produjo un error al agregar el registro.", MsgBoxStyle.Exclamation)
                    Cursor = Cursors.Default
                    Exit Sub
                Case 0
                    MsgBox("No se pudo agregar el registro.", MsgBoxStyle.Exclamation)
                    Cursor = Cursors.Default
                    Exit Sub
                Case Else
                    DG_refresh()
                    'MsgBox("Se ha agregado el registro.", MsgBoxStyle.Exclamation)
            End Select
            'Try
            '    ' tran.Sql_Set(sql)
            'Catch ex As Exception
            '    Cursor = Cursors.Default
            '    MsgBox(ex.ToString)
            'End Try
            'DG_refresh()
        Else
            If modo = "update" Then

                'Dim fecha As String
                'Dim sql As String

                If btnAceptar.Text = "Modificar" Then
                    'fecha = FechaMovimiento.Date.Year.ToString & "-" & FechaMovimiento.Date.Month.ToString & "-" & FechaMovimiento.Date.Day.ToString
                    'sql = "exec Editar_movimiento_de_caja @id=" & DG_movimientos.CurrentRow.Cells(0).Value & ", @id_sucursal= " & 1 & ", @observacion='" & txt_observaciones.Text & "', @monto=" & Replace(Txt_monto.Text, ",", ".") & ", @movimiento='" & lblOperacion.Text & "', @fecha='" & fecha & "'"
                    res = ActualizarRegistro()
                    Select Case res
                        Case -1
                            MsgBox("No se produjo un error al actualizar el registro.", MsgBoxStyle.Exclamation)
                            Cursor = Cursors.Default
                            Exit Sub
                        Case 0
                            MsgBox("No se pudo actualizar el registro.", MsgBoxStyle.Exclamation)
                            Cursor = Cursors.Default
                            Exit Sub
                    End Select
                Else
                    'sql = "update movimiento_de_caja set deleted = 0 where @id=" & DG_movimientos.CurrentRow.Cells(0).Value
                    Dim connection As SqlClient.SqlConnection = Nothing
                    Dim ds_Update As Data.DataSet

                    Try
                        connection = SqlHelper.GetConnection(ConnStringSEI)
                    Catch ex As Exception
                        'llenandoCombo = False
                        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    ds_Update = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE Movimientos_Caja SET Eliminado = 0 WHERE id = " & DG_movimientos.CurrentRow.Cells(0).Value)
                    ds_Update.Dispose()
                End If

                'Try
                '    'tran.Sql_Set(sql)
                'Catch ex As Exception
                '    Cursor = Cursors.Default
                '    MsgBox(ex.ToString)
                'End Try
                DG_refresh()
                Cursor = Cursors.Default
                MsgBox("El registro se ha modificado.", MsgBoxStyle.OkOnly)
                ' MsgBox("El registro se ha modificado")
            End If
        End If

        Txt_monto.Text = ""
        txt_observaciones.Text = ""
        Txt_monto.Focus()

        Cursor = Cursors.Default

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        If lblOperacion.Text <> "Fondos Apertura" Then
            modo = "insert"
            btnAceptar.Text = "Aceptar"
            Txt_monto.Text = ""
            txt_observaciones.Text = ""
            chkEliminados.Checked = False
        End If
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        Dim res As Integer
        If lblOperacion.Text = "Fondos Apertura" Then
            MsgBox("No se puede eliminar este movimiento por ser único. Solo puede modificar el valor", MsgBoxStyle.Information, "Control de Errores")
            Exit Sub
        End If

        If DG_movimientos.RowCount > 0 Then

            If FilaSeleccionada < 0 Then
                MsgBox("Debe seleccionar un movimiento para borrar", MsgBoxStyle.Information, "Control de Errores")
                Exit Sub
            End If

            If MsgBox("Está seguro de querer eliminar el registro seleccionado?", vbYesNo) = vbYes Then
                res = EliminarRegistro()
                Select Case res
                    Case -1
                        MsgBox("No se produjo un error al borrar el registro.", MsgBoxStyle.Exclamation)
                        Cursor = Cursors.Default
                        Exit Sub
                    Case 0
                        MsgBox("No se pudo borrar el registro.", MsgBoxStyle.Exclamation)
                        Cursor = Cursors.Default
                        Exit Sub
                End Select

                'Try
                '    'tran.Sql_Set("UPDATE Movimientos_de_caja SET deleted=1 WHERE Id=" & DG_movimientos.CurrentRow.Cells(0).Value)
                'Catch ex As Exception
                '    MsgBox(ex.Message)
                'End Try
                DG_refresh()

                Txt_monto.Text = ""
                txt_observaciones.Text = ""
                lblOperacion.Text = ""

                Txt_monto.Focus()

            End If
        End If
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub

#End Region





End Class