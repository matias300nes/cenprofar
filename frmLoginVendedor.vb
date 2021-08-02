Imports System.Windows.Forms
Imports System.Net
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util

Public Class frmLoginVendedor

    'Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    frmVentas_Peron.IdVendedor = cmbVendedor.SelectedValue
    '    frmVentas_Peron.NombreVendedor = cmbVendedor.Text.ToString

    '    Me.Hide()

    'End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If MDIPrincipal.sucursal.Contains("PERON") Then
            LlenarcmbEmpleados3_App(cmbVendedor, ConnStringSEI)
        Else
            llenarcmbRepartidor()
        End If
    End Sub

    Private Sub cmbVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendedor.KeyDown
        If e.KeyData = Keys.Enter Then
            If MDIPrincipal.sucursal.Contains("PERON") Then
                frmVentas.IdVendedor = cmbVendedor.SelectedValue
                frmVentas.NombreVendedor = cmbVendedor.Text.ToString
            Else
                frmVentaSalon.IdVendedor = cmbVendedor.SelectedValue
                frmVentaSalon.NombreVendedor = cmbVendedor.Text.ToString
            End If
      

            Me.Hide()

        End If
    End Sub

    Private Sub llenarcmbRepartidor()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 and Vendedor = 1 ORDER BY Vendedor")
            ds.Dispose()

            With cmbVendedor
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


End Class
