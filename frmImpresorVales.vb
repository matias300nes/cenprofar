Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmImpresorVales

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' cp 30-8-2011
    ' Poner en TRUE cuando se larga el nuevo pañol...
    Private Const Panol_Nuevo = True
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim i As Integer, Cant As Integer
        Label1.Text = "Buscando Vales para Imprimir..."

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' cp 30-8-2011
        If Not Panol_Nuevo Then
            Cant = ValesSinImprimir()
        Else
            Cant = ValesSinImprimir_PanolNuevo()
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        If cant <> 0 Then
            If TextBox1.Text.Trim = "" Then
                TextBox1.Text = "1"
            End If
            If CType(TextBox1.Text, Integer) <> 0 Then
                For i = 1 To CType(TextBox1.Text, Integer)
                    ImprimirVales()
                   
                Next i
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' cp 30-8-2011
                If Not Panol_Nuevo Then
                    MarcarValesImpresos()
                Else
                    MarcarValesImpresos_PanolNuevo()
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
        End If
        Label1.Text = "Esperando por Vales..."
    End Sub


    ' Permite obtener datos generales de la tabla usuarios a travez del campo id
    Public Function ValesSinImprimir_PanolNuevo() As Integer


        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, nombre As String, pass_actual As String
        'codigo = "" : nombre = "" : pass_actual = "" 
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@cantidad"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_ValesSinImprimir", param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    Else
                        res = 0
                    End If

                    ValesSinImprimir_PanolNuevo = res

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


    ' Permite obtener datos generales de la tabla usuarios a travez del campo id
    Public Function ValesSinImprimir() As Integer


        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, nombre As String, pass_actual As String
        'codigo = "" : nombre = "" : pass_actual = "" 
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try


                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@cantidad"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_ValesSinImprimir", param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    Else
                        res = 0
                    End If

                    ValesSinImprimir = res

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

    'cp 20-05-2011
    Public Sub MarcarValesImpresos_PanolNuevo()

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, nombre As String, pass_actual As String
        'codigo = "" : nombre = "" : pass_actual = "" 
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try
                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMarcarValesImpresos")

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

    'cp 20-05-2011
    Public Sub MarcarValesImpresos()

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, nombre As String, pass_actual As String
        'codigo = "" : nombre = "" : pass_actual = "" 
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try


                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMarcarValesImpresos")

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

    'cp 20-5-2011
    Private Sub ImprimirVales()
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        'nbreformreportes = "Vales"
        Dim rpt As New frmReportes
        If Not Panol_Nuevo Then
            'rpt.ImprimirVales(rpt)
        Else
            ' rpt.ImprimirVales_PanolNuevo(rpt)
        End If
        rpt.Close()
        rpt.Dispose()
        Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmImpresorVales_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MDIPrincipal.ImpresorValesAbierto = False
    End Sub

    'AL - 11-11-11
    'Se agrega la opción para reeimprimir los vales
    Private Sub btnReImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReImprimir.Click
        Dim Reporte As New frmReportes
        Dim ds As DataSet = Nothing
        Dim connection As SqlClient.SqlConnection = Nothing

        If txtNroVale.Text = "" Or txtNroVale Is Nothing Then
            MsgBox("Debe ingresar el Nro de Vale para continuar", MsgBoxStyle.Information, "Reimpresión de Vales")
            txtNroVale.Focus()
            Exit Sub
        End If

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID FROM CONSUMOS WHERE CODIGO = " & txtNroVale.Text)

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        If ds.Tables(0).Rows.Count = 0 Then
            MsgBox("El nro de vale ingresado no existe", MsgBoxStyle.Critical, "Reimpresión de Vales")
            txtNroVale.Text = ""
            txtNroVale.Focus()
            Exit Sub
        End If

        '1 por pantalla.....2 por impresora.......
        ' Reporte.MostrarVale(Reporte, 2, CType(ds.Tables(0).Rows(0)("id"), Long), "'NO'", TextBox1.Text) 'NO TENGO LA MENOR IDEA DE PORQUE HAY QUE PONERLE LAS COMILLAS..

        txtNroVale.Text = ""
    End Sub

    Private Sub txtNroVale_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroVale.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class