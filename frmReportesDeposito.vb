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




Public Class frmReportesDeposito

    Dim llenandoCombo As Boolean = False




    Private Sub RecaudacionRepartidor(sender As Object, e As EventArgs) Handles MyBase.Load

        rdRecaudacion.Checked = True
        dtpFechaReca.Value = Date.Now
        dtpDesde.Enabled = False
        dtpHasta.Enabled = False
        dtpDesde.Value = Date.Now
        dtpHasta.Value = Date.Now
        chkDeposito.Enabled = False
        chkNorte.Enabled = False


        LlenarRepartidor()

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        chkDeposito.Checked = False
        chkNorte.Checked = False
        dtpFechaReca.Value = Date.Now
        dtpDesde.Value = Date.Now
        dtpHasta.Value = Date.Now
        cmbRepartidor.SelectedIndex = 0


    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        Dim rpt As New frmReportes()
        Dim param As New frmParametros
        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim codigo As String
        Dim Fecha As DateTime
        Dim Hasta As DateTime
        Dim Norte As Boolean
        Dim Deposito As Boolean
        Dim ds_Recaudacion As Data.DataSet
        Dim sqlstring As String

        If rdDevolucion.Checked = True Then
            If chkDeposito.Checked = False And chkNorte.Checked = False Then
                MsgBox("Por favor seleccione al menos tipo de Devolución.")
                Exit Sub
            End If
        End If
    

        If rdRecaudacion.Checked = True Then
            Try

                sqlstring = "SELECT Isnull(TotalFacturado,0) FROM PedidosWEB WHERE Presupuesto = 0 AND IDEmpleado = '" & cmbRepartidor.SelectedValue & "' AND convert(varchar(10),Fecha,103) = '" & dtpFechaReca.Value.ToShortDateString & "'"

                ds_Recaudacion = SqlHelper.ExecuteDataset(cnn, CommandType.Text, sqlstring)

                If ds_Recaudacion.Tables(0).Rows.Count = 0 Then
                    MsgBox("No hay envíos realizados por el repartidor " & cmbRepartidor.Text & " en la fecha seleccionada. Por favor verifique.", MsgBoxStyle.Information, "Atención")
                    Exit Sub
                End If


            Catch ex As Exception
                'MsgBox(ex.Message)
                MsgBox("No hay envíos realizados por el repartidor " & cmbRepartidor.Text & " en la fecha seleccionada o bien no se pudo realizar la consulta. Por favor verifique.", MsgBoxStyle.Information, "Atención")

                Exit Sub
            End Try

            nbreformreportes = "Recaucadión por Repartidores"


            codigo = cmbRepartidor.SelectedValue.ToString
            Fecha = dtpFechaReca.Value
            'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)
            rpt.Recaudacion_Maestro_App(codigo, "A", Fecha, rpt, My.Application.Info.AssemblyName.ToString)

        Else


            nbreformreportes = "Devoluciones por Clientes"


            Fecha = dtpDesde.Value
            Hasta = dtpHasta.Value
            Norte = chkNorte.Checked
            Deposito = chkDeposito.Checked

            'rpt.NombreArchivoPDF = "Orden de Compra " & codigo & " - " & BuscarProveedor(codigo, Solicitud)
            rpt.DevolucionesClientes_Maestro_App(Fecha, Hasta, Norte, Deposito, rpt, My.Application.Info.AssemblyName.ToString)

        End If

        cerroparametrosconaceptar = False
        param = Nothing
        cnn = Nothing

    End Sub

    Private Sub LlenarRepartidor()

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select Codigo,CONCAT(Apellido ,' ', Nombre) AS 'Vendedor' FROM Empleados WHERE Eliminado = 0 and Repartidor = 1 ORDER BY Vendedor")
            ds.Dispose()

            With cmbRepartidor
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Vendedor"
                .ValueMember = "Codigo"
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
    End Sub

    Private Sub rdRecaudacion_CheckedChanged(sender As Object, e As EventArgs) Handles rdRecaudacion.CheckedChanged
        rdDevolucion.Checked = Not rdRecaudacion.Checked
        dtpFechaReca.Enabled = rdRecaudacion.Checked
        cmbRepartidor.Enabled = rdRecaudacion.Checked
    End Sub

    Private Sub rdDevolucion_CheckedChanged(sender As Object, e As EventArgs) Handles rdDevolucion.CheckedChanged
        rdRecaudacion.Checked = Not rdDevolucion.Checked
        dtpDesde.Enabled = rdDevolucion.Checked
        dtpHasta.Enabled = rdDevolucion.Checked
        chkDeposito.Enabled = rdDevolucion.Checked
        chkNorte.Enabled = rdDevolucion.Checked
    End Sub


End Class