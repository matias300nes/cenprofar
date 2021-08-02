Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Public Class frmTipoMoneda

    Private Sub frmTipoMoneda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim peso As Double = 0
        Dim dolar As Double = 0
        Dim euro As Double = 0

        ObtenerValoresTipoMoneda(peso, dolar, euro)

        txtPeso.Text = peso
        txtDolar.Text = dolar
        txtEuro.Text = euro

    End Sub

    Private Sub ObtenerValoresTipoMoneda(ByRef peso As Double, ByRef dolar As Double, ByRef euro As Double)
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim param_peso As New SqlClient.SqlParameter
            param_peso.ParameterName = "@peso"
            param_peso.SqlDbType = SqlDbType.Decimal
            param_peso.Precision = 18
            param_peso.Scale = 3
            param_peso.Value = DBNull.Value
            param_peso.Direction = ParameterDirection.InputOutput

            Dim param_dolar As New SqlClient.SqlParameter
            param_dolar.ParameterName = "@dolar"
            param_dolar.SqlDbType = SqlDbType.Decimal
            param_dolar.Precision = 18
            param_dolar.Scale = 3
            param_dolar.Value = DBNull.Value
            param_dolar.Direction = ParameterDirection.InputOutput

            Dim param_euro As New SqlClient.SqlParameter
            param_euro.ParameterName = "@euro"
            param_euro.SqlDbType = SqlDbType.Decimal
            param_euro.Precision = 18
            param_euro.Scale = 3
            param_euro.Value = DBNull.Value
            param_euro.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spObtenerTipoMoneda ", param_peso, param_dolar, param_euro)

                peso = CDec(param_peso.Value)
                dolar = CDec(param_dolar.Value)
                euro = CDec(param_euro.Value)

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

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim res As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim param_peso As New SqlClient.SqlParameter
            param_peso.ParameterName = "@peso"
            param_peso.SqlDbType = SqlDbType.Decimal
            param_peso.Precision = 18
            param_peso.Scale = 3
            param_peso.Value = CDec(txtPeso.Text)
            param_peso.Direction = ParameterDirection.Input

            Dim param_dolar As New SqlClient.SqlParameter
            param_dolar.ParameterName = "@dolar"
            param_dolar.SqlDbType = SqlDbType.Decimal
            param_dolar.Precision = 18
            param_dolar.Scale = 3
            param_dolar.Value = CDec(txtDolar.Text)
            param_dolar.Direction = ParameterDirection.Input

            Dim param_euro As New SqlClient.SqlParameter
            param_euro.ParameterName = "@euro"
            param_euro.SqlDbType = SqlDbType.Decimal
            param_euro.Precision = 18
            param_euro.Scale = 3
            param_euro.Value = CDec(txtEuro.Text)
            param_euro.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMonedas_Update", param_peso, param_dolar, param_euro, param_res)

                res = CInt(param_res.Value)

                Select Case res
                    Case -1
                        MessageBox.Show("Los Datos de Tipo de Moneda no han podido ser dados de Modificados", "Error en Transacción Modificación de Datos de Tipo de Moneda", MessageBoxButtons.OK)
                    Case 0
                        MessageBox.Show("No se Modificaron los Datos de Tipo de Moneda.", "Error al Modificar el Tipo de Moneda", MessageBoxButtons.OK)
                    Case Else
                        MessageBox.Show("Los Datos de Tipo de Moneda se Modificaron.", "Modificación Exitosa", MessageBoxButtons.OK)
                End Select

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
    End Sub
End Class