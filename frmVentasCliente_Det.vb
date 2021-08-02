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




Public Class frmVentasCliente_Det

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load


        lblNro.Text = frmVentasClientes.numvta
        lblTotal.Text = frmVentasClientes.totalvta

        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try


        Dim sqlstring As String = " select Ped.id ," & _
                                  "ROW_NUMBER() OVER ( " & _
                                  "ORDER BY Ped.id desc " & _
                                  ") Item, " & _
                                  "'Producto' = case  " & _
                                  "WHEN Ped.Bonificacion = 1 and Ped.Promo = 1 THEN M.Nombre + '(BONIF.)' + '(PROMO)'" & _
                                  "WHEN Ped.Bonificacion = 1 and Ped.Promo = 0 THEN M.Nombre + '(BONIF.)'" & _
                                  "WHEN Ped.Bonificacion = 0 and Ped.Promo = 1 THEN M.Nombre + '(PROMO)'" & _
                                  "ELSE M.Nombre END ,	" & _
                                  "U.Nombre as Unidad," & _
                                  "Ped.QtyPedida as Cantidad," & _
                                  "Ped.UnidadFac as Peso," & _
                                 "Ped.Precio," & _
                                 "Ped.Descuento as 'Desc.(%)'," & _
                                 "Ped.Subtotal," & _
                                 "Ped.Nota_Det as Nota " & _
                                 "from PedidosWEB_det Ped " & _
                                 "JOIN Materiales M on M.Codigo = Ped.IDMaterial " & _
                                 "JOIN Unidades U ON U.Codigo = Ped.IDUnidad " & _
                                 "where Ped.IDPedidosWEB = '" & frmVentasClientes.numvta & "' " & _
                                 "order by id desc "

        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
        ds.Dispose()

        grd.DataSource = ds.Tables(0).DefaultView
        grd.Columns(0).Visible = False
        grd.Columns(1).Width = 40
        grd.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
        grd.Columns(2).Width = 250
        grd.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grd.Columns(5).Width = 50
        grd.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grd.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grd.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grd.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grd.Columns(9).Width = 200
        grd.Rows(0).Selected = False

        lblCantidad.Text = grd.RowCount


    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub



End Class