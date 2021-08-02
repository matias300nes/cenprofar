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




Public Class frmActuliazarSistema
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient


    Dim Transferencias As Boolean
    Dim Recepciones As Boolean
    Dim Materiales As Boolean
    Dim ListaPrecio As Boolean
    Dim Clientes As Boolean
    Dim Empleados As Boolean
    Dim sqlstring As String

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim r As Rectangle = My.Computer.Screen.WorkingArea
        Location = New Point(r.Width - Width - 90, r.Height - Height - 100)

        If MDIPrincipal.sucursal.Contains("PERON") Then
            btnStock.Text = "Stock Principal"
        Else
            btnStock.Text = "Stock Perón"
            btnMateriales.Enabled = False
            btnListaPrecios.Enabled = False
            btnRecepciones.Enabled = False
        End If
        btnEmpleados.Enabled = False
        btnClientes.Enabled = False

        BuscarNotificaciones()

        Timer1.Enabled = True


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        BuscarNotificaciones()

        Timer1.Enabled = True

    End Sub

    Public Sub ActualizarSistema(ByVal Transferencia As Boolean, ByVal Recepciones As Boolean, ByVal Materiales As Boolean, ByVal ListaPrecios As Boolean, Clientes As Boolean, ByVal Empleados As Boolean, ByVal Stock As Boolean)

        Timer1.Enabled = False

        ' Cambiamos el cursor por el de carga
        Me.Cursor = Cursors.WaitCursor

        Dim ds_Empresa As Data.DataSet
        Dim Conexion As SqlConnection
        'llena la tabla temporal que tendrá solo los materiales nuevos que se han agregado al sistema
        Dim sqlstring As String
        'esta variable la utilizo para descargar siempre el stock del otro almacen 
        Dim otroAlmacen As Integer

        If MDIPrincipal.sucursal.Contains("PERON") Then
            ' ---PERÓN---
            Conexion = New SqlConnection("Data Source=PORKIS-SERVER;Initial Catalog=Porkys;User ID=sa;Password=industrial")
            otroAlmacen = 1
        Else
            '---MARCONI---
            Conexion = New SqlConnection("Data Source=servidor;Initial Catalog=Porkys;User ID=sa;Password=industrial")
            otroAlmacen = 2
        End If

        '---MI MAQUINA---
        'Conexion = New SqlConnection("Data Source=SAMBA-PC;Initial Catalog=Porkys;User ID=sa;Password=industrial")
        'otroAlmacen = 1
        'PERON = True
        'PERON = False
        '---Analia---
        'Conexion = New SqlConnection("Data Source=SilvaAnalia-PC;Initial Catalog=Porkys;User ID=sa;Password=industrial")

        Dim prueba As String

        '********************************************************STOCK**********************************************************
        If Stock Then
            Try
                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpStock_Web")
                ds_Empresa.Dispose()

                Dim ds_Stock As DataSet = tranWEB.Sql_Get("SELECT idalmacen, idmaterial, qty,idunidad FROM Stock where idalmacen = " & otroAlmacen)

                Dim bulk_Stock As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_Stock.DestinationTableName = "tmpStock_Web"
                bulk_Stock.WriteToServer(ds_Stock.Tables(0))
                Conexion.Close()

                sqlstring = " UPDATE Stock SET qty = TMP.qty FROM Stock s Join tmpStock_Web tmp ON tmp.idmaterial = s.Idmaterial " & _
                    " and tmp.idalmacen = s.idalmacen and tmp.idunidad = s.idunidad "

                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()


            Catch ex As Exception
                Me.Cursor = Cursors.Arrow
            End Try

   

        End If
        '****************************************************************Transferencias*******************************************
        If Transferencia Then
            Try
                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpTransferencias_Recepciones_WEB")
                ds_Empresa.Dispose()

                Dim ds_Clientes As DataSet = tranWEB.Sql_Get("SELECT * FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'T' And IDDestino = " & numero_almacen)

                Dim bulk_Clientes As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_Clientes.DestinationTableName = "tmpTransferencias_Recepciones_WEB"
                bulk_Clientes.WriteToServer(ds_Clientes.Tables(0))
                Conexion.Close()

                sqlstring = " Select [Codigo],[Fecha],[IDOrigen],[IDDestino],[IDMaterial], " & _
                            "[Qty],[Procesado],[Tipo] from tmpTransferencias_Recepciones_WEB"

                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                Dim i As Integer = 0
                Dim ds_tmpClientes As Data.DataSet

                If ds_Empresa.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                        sqlstring = "UPDATE [dbo].[Stock] SET " & _
                            " [Qty] = iif(qty < 0,0,qty) + " & ds_Empresa.Tables(0).Rows(i)(5) & ", " & _
                            " [DATEUPD] = '" & Format(ds_Empresa.Tables(0).Rows(i)(1), "dd/MM/yyyy") & " " & Format(Date.Now, "hh:mm:ss").ToString & "' " & _
                            " WHERE IDMaterial = '" & ds_Empresa.Tables(0).Rows(i)(4) & "' And [IDAlmacen] = " & numero_almacen
                        '" [ActualizadoWeb] = 1 " & _


                        prueba = ds_Empresa.Tables(0).Rows(i)(3).ToString
                        '-------------------------------------------------------------------------------------------
                        ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpClientes.Dispose()

                        sqlstring = "UPDATE Transferencias_Recepciones_WEB SET Procesado = 1 WHERE CODIGO = '" & ds_Empresa.Tables(0).Rows(i)(0).ToString & "' And IdMaterial = '" & ds_Empresa.Tables(0).Rows(i)(4) & "'"
                        'proceso la transferencia
                        tranWEB.Sql_Get(sqlstring)
                       
                      
                        '------------------------------------------------------------------------------------------
                    Next

                    'hago un join con stock para actualizar la web
                    sqlstring = " select s.Qty,tmp.IDMaterial,tmp.IDDestino from tmpTransferencias_Recepciones_WEB tmp join stock s on s.IDMaterial = tmp.IDMaterial and s.IDAlmacen = tmp.IDDestino
                    'actualizo el stock de la web
                    ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_tmpClientes.Dispose()
                    For i = 0 To ds_tmpClientes.Tables(0).Rows.Count - 1
                        sqlstring = "UPDATE [dbo].[Stock] SET " & _
                     " [Qty] = " & ds_tmpClientes.Tables(0).Rows(i)(0) & ", " & _
                     " [DATEUPD] = getdate()," & _
                     " [ActualizadoLocal] = 1 " & _
                     " WHERE IDMaterial = '" & ds_tmpClientes.Tables(0).Rows(i)(1) & "' And [IDAlmacen] = " & ds_tmpClientes.Tables(0).Rows(i)(2)
                        tranWEB.Sql_Get(sqlstring)
                    Next

                    tranWEB.Sql_Get("UPDATE NotificacionesWEB SET Transferencias = 0 ")
                End If

                btnTransferencia.SymbolColor = Color.Green

                MsgBox("Recuerde hacer click en " & btnStock.Text & " para obtener el stock del otro Almacen.", MsgBoxStyle.Information)

            Catch ex As Exception
                MsgBox(" Desde Actualización Transferencias " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
        End If

        '****************************************************************Recepciones*******************************************
        If Recepciones Then
            Try
                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpTransferencias_Recepciones_WEB")
                ds_Empresa.Dispose()

                Dim ds_Clientes As DataSet = tranWEB.Sql_Get("SELECT * FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'R' And IDDestino = " & numero_almacen)

                Dim bulk_Clientes As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_Clientes.DestinationTableName = "tmpTransferencias_Recepciones_WEB"
                bulk_Clientes.WriteToServer(ds_Clientes.Tables(0))
                Conexion.Close()

                sqlstring = " Select [Codigo],[Fecha],[IDOrigen],[IDDestino],[IDMaterial], " & _
                            "[Qty],[Procesado],[Tipo] from tmpTransferencias_Recepciones_WEB"

                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                Dim i As Integer = 0
                Dim ds_tmpClientes As Data.DataSet

                If ds_Empresa.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                        sqlstring = "UPDATE [dbo].[Stock] SET " & _
                            " [Qty] = iif(qty < 0,0,qty) + " & ds_Empresa.Tables(0).Rows(i)(5) & ", " & _
                            " [DATEUPD] = '" & Format(ds_Empresa.Tables(0).Rows(i)(1), "dd/MM/yyyy") & " " & Format(Date.Now, "hh:mm:ss").ToString & "' " & _
                            " WHERE IDMaterial = '" & ds_Empresa.Tables(0).Rows(i)(4) & "' And [IDAlmacen] = " & numero_almacen
                        '" [ActualizadoWeb] = 1 " & _


                        prueba = ds_Empresa.Tables(0).Rows(i)(3).ToString
                        '-------------------------------------------------------------------------------------------
                        ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpClientes.Dispose()

                        sqlstring = "UPDATE Transferencias_Recepciones_WEB SET Procesado = 1 WHERE CODIGO = '" & ds_Empresa.Tables(0).Rows(i)(0).ToString & "' And IdMaterial = '" & ds_Empresa.Tables(0).Rows(i)(4) & "'"
                        'proceso la transferencia
                        tranWEB.Sql_Get(sqlstring)


                        '------------------------------------------------------------------------------------------
                    Next

                    'hago un join con stock para actualizar la web
                    sqlstring = " select s.Qty,tmp.IDMaterial,tmp.IDDestino from tmpTransferencias_Recepciones_WEB tmp join stock s on s.IDMaterial = tmp.IDMaterial and s.IDAlmacen = tmp.IDDestino"
                    'actualizo el stock de la web
                    ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_tmpClientes.Dispose()
                    For i = 0 To ds_tmpClientes.Tables(0).Rows.Count - 1
                        sqlstring = "UPDATE [dbo].[Stock] SET " & _
                     " [Qty] = " & ds_tmpClientes.Tables(0).Rows(i)(0) & ", " & _
                     " [DATEUPD] = getdate()," & _
                     " [ActualizadoLocal] = 1 " & _
                     " WHERE IDMaterial = '" & ds_tmpClientes.Tables(0).Rows(i)(1) & "' And [IDAlmacen] = " & ds_tmpClientes.Tables(0).Rows(i)(2)
                        tranWEB.Sql_Get(sqlstring)
                    Next

                    tranWEB.Sql_Get("UPDATE NotificacionesWEB SET Recepciones = 0 ")
                End If

                btnRecepciones.SymbolColor = Color.Green

                MsgBox("Recuerde hacer click en " & btnStock.Text & " para obtener el stock del otro Almacen.", MsgBoxStyle.Information)

            Catch ex As Exception
                MsgBox(" Desde Actualización Transferencias " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
        End If


        Dim ds_Marcas As Data.DataSet
        '*****************************************************************MATERIALES**********************************************
        If Materiales Then
            Try

                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpMateriales_Web")
                ds_Empresa.Dispose()

                Dim ds_Clientes As DataSet = tranWEB.Sql_Get("SELECT * FROM Materiales Where ActualizadoLocal = 0 ")

                Dim bulk_Clientes As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_Clientes.DestinationTableName = "tmpMateriales_Web"
                bulk_Clientes.WriteToServer(ds_Clientes.Tables(0))
                Conexion.Close()


                'Dim sqlstring As String
                'Clientes que están en la WEB y no están en la sucursal/central
                sqlstring = " SELECT [IdMarca],[IdFamilia],[IDUnidad],[Codigo],[Nombre], " & _
                        " [Ganancia], [PrecioCosto], [PrecioCompra], [PrecioPeron], [Minimo], [Maximo], " & _
                        " [CodigoBarra], [Eliminado], [Pasillo], [Estante], [Fila], [ControlStock], [CantidadPACK], [DateAdd], " & _
                        " [PrecioMayorista] , [PrecioMayoristaPeron], [IDListaMayorista] , [IDListaMayoristaPeron]," & _
                        " [IDListaMinorista] , [IDListaMinoristaPeron],[PrecioLista3],[IDLista3],[PrecioLista4],[IDLista4],[UnidadRef]," & _
                        " [Cambiar1] ,[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista],[ActualizadoLocal]" & _
                        " FROM tmpMateriales_Web WHERE Codigo NOT IN (SELECT Codigo FROM Materiales ) "

                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                Dim i As Integer = 0
                Dim ds_tmpClientes As Data.DataSet

                If ds_Empresa.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                        sqlstring = " BEGIN TRAN; " & _
                            " INSERT INTO [dbo].[Materiales] ([IdMarca],[IdFamilia],[IDUnidad],[Codigo],[Nombre], " & _
                            " [Ganancia], [PrecioCosto], [PrecioCompra], [PrecioPeron], [Minimo], [Maximo], " & _
                            " [CodigoBarra], [Eliminado], [Pasillo], [Estante], [Fila], [ControlStock], [CantidadPACK], [DateAdd], " & _
                            " [PrecioMayorista] , [PrecioMayoristaPeron], [IDListaMayorista] , [IDListaMayoristaPeron], " & _
                            " [IDListaMinorista] , [IDListaMinoristaPeron],[PrecioLista3],[IDLista3],[PrecioLista4],[IDLista4], " & _
                            " [UnidadRef],[Cambiar1] ,[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista],[ActualizadoWeb]) " & _
                            "  values ( '" & ds_Empresa.Tables(0).Rows(i)(0) & "', '" & ds_Empresa.Tables(0).Rows(i)(1) & "', '" & _
                            ds_Empresa.Tables(0).Rows(i)(2) & "', '" & ds_Empresa.Tables(0).Rows(i)(3) & "', '" & ds_Empresa.Tables(0).Rows(i)(4) & "', " & _
                            ds_Empresa.Tables(0).Rows(i)(5) & ", " & ds_Empresa.Tables(0).Rows(i)(6) & ", " & ds_Empresa.Tables(0).Rows(i)(7) & ", " & _
                            ds_Empresa.Tables(0).Rows(i)(8) & ", " & ds_Empresa.Tables(0).Rows(i)(9) & ", " & ds_Empresa.Tables(0).Rows(i)(10) & ", '" & _
                            ds_Empresa.Tables(0).Rows(i)(11) & "', " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(12)) = True, 1, 0) & ", '" & ds_Empresa.Tables(0).Rows(i)(13) & "', '" & _
                            ds_Empresa.Tables(0).Rows(i)(14) & "', '" & ds_Empresa.Tables(0).Rows(i)(15) & "', " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(16)) = True, 1, 0) & ", " & _
                            ds_Empresa.Tables(0).Rows(i)(17) & ", '" & Format(ds_Empresa.Tables(0).Rows(i)(18), "dd/MM/yyyy hh:ss") & "', " & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(19).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(19)) & ", " & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(20).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(20)) & ", " & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(21).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(21)) & ", " & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(22).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(22)) & ", " & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(23).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(23)) & ", " & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(24).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(24)) & ", " & _
                            ds_Empresa.Tables(0).Rows(i)(25) & "," & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(26).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(26)) & "," & _
                            ds_Empresa.Tables(0).Rows(i)(27) & "," & _
                            IIf(ds_Empresa.Tables(0).Rows(i)(28).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(28)) & ",'" & _
                            ds_Empresa.Tables(0).Rows(i)(29) & "'," & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(30)) = True, 1, 0) & "," & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(31)) = True, 1, 0) & "," & _
                            IIf(CBool(ds_Empresa.Tables(0).Rows(i)(32)) = True, 1, 0) & "," & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(33)) = True, 1, 0) & "," & _
                            IIf(CBool(ds_Empresa.Tables(0).Rows(i)(34)) = True, 1, 0) & ",1); " & _
                            " COMMIT TRAN;"

                        prueba = ds_Empresa.Tables(0).Rows(i)(3).ToString

                        ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpClientes.Dispose()

                    Next

                End If

                sqlstring = " SELECT [IdMarca],[IdFamilia],[IDUnidad],[Codigo],[Nombre], " & _
                        " [Ganancia], [PrecioCosto], [PrecioCompra], [PrecioPeron], [Minimo], [Maximo], " & _
                        " [CodigoBarra], [Eliminado], [Pasillo], [Estante], [Fila], [ControlStock], [CantidadPACK], isnull([DateUPD], '01/01/1900 00:00:00') ," & _
                        " [PrecioMayorista] , [PrecioMayoristaPeron], [IDListaMayorista] , [IDListaMayoristaPeron], [IDListaMinorista] , [IDListaMinoristaPeron], " & _
                        " [PrecioLista3],[IDLista3],[PrecioLista4],[IDLista4],[UnidadRef],[Cambiar1] ,[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista],[ActualizadoLocal] FROM tmpMateriales_Web "

                ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                For i = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                    sqlstring = "UPDATE [dbo].[Materiales] SET " & _
                        " [IdMarca] = '" & ds_Empresa.Tables(0).Rows(i)(0) & "', " & _
                        " [IdFamilia] = '" & ds_Empresa.Tables(0).Rows(i)(1) & "', " & _
                        " [IDUnidad] = '" & ds_Empresa.Tables(0).Rows(i)(2) & "', " & _
                        " [Codigo] = '" & ds_Empresa.Tables(0).Rows(i)(3) & "', " & _
                        " [Nombre] = '" & ds_Empresa.Tables(0).Rows(i)(4) & "', " & _
                        " [Ganancia] = " & ds_Empresa.Tables(0).Rows(i)(5) & ", " & _
                        " [PrecioCosto] = " & ds_Empresa.Tables(0).Rows(i)(6) & ", " & _
                        " [PrecioCompra] = " & ds_Empresa.Tables(0).Rows(i)(7) & ", " & _
                        " [PrecioPeron] = " & ds_Empresa.Tables(0).Rows(i)(8) & ", " & _
                        " [Minimo] = " & ds_Empresa.Tables(0).Rows(i)(9) & ", " & _
                        " [Maximo] = " & ds_Empresa.Tables(0).Rows(i)(10) & ", " & _
                        " [CodigoBarra] = '" & ds_Empresa.Tables(0).Rows(i)(11) & "', " & _
                        " [Eliminado] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(12)) = True, 1, 0) & ", " & _
                        " [Pasillo] = '" & ds_Empresa.Tables(0).Rows(i)(13) & "', " & _
                        " [Estante] = '" & ds_Empresa.Tables(0).Rows(i)(14) & "', " & _
                        " [Fila] = '" & ds_Empresa.Tables(0).Rows(i)(15) & "', " & _
                        " [ControlStock] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(16)) = True, 1, 0) & ", " & _
                        " [CantidadPACK] = " & ds_Empresa.Tables(0).Rows(i)(17) & ", " & _
                        " DATEUPD = '" & Format(ds_Empresa.Tables(0).Rows(i)(18), "dd/MM/yyyy") & "' , " & _
                        " [PrecioMayorista] = " & IIf(ds_Empresa.Tables(0).Rows(i)(19).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(19)) & ", " & _
                        " [PrecioMayoristaPeron] = " & IIf(ds_Empresa.Tables(0).Rows(i)(20).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(20)) & ", " & _
                        " [IDListaMayorista] = " & IIf(ds_Empresa.Tables(0).Rows(i)(21).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(21)) & ", " & _
                        " [IDListaMayoristaPeron] = " & IIf(ds_Empresa.Tables(0).Rows(i)(22).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(22)) & ", " & _
                        " [IDListaMinorista] = " & IIf(ds_Empresa.Tables(0).Rows(i)(23).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(23)) & ", " & _
                        " [IDListaMinoristaPeron] = " & IIf(ds_Empresa.Tables(0).Rows(i)(24).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(24)) & ", " & _
                        " [PrecioLista3] = " & ds_Empresa.Tables(0).Rows(i)(25) & "," & _
                        " [IDLista3] = " & IIf(ds_Empresa.Tables(0).Rows(i)(26).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(26)) & "," & _
                        " [PrecioLista4] = " & ds_Empresa.Tables(0).Rows(i)(27) & "," & _
                        " [IDLista4] = " & IIf(ds_Empresa.Tables(0).Rows(i)(28).ToString = "", 0, ds_Empresa.Tables(0).Rows(i)(28)) & "," & _
                        " [UnidadRef] = '" & ds_Empresa.Tables(0).Rows(i)(29) & "'," & _
                        " [Cambiar1] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(30)) = True, 1, 0) & "," & _
                        " [Cambiar2] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(31)) = True, 1, 0) & "," & _
                        " [Cambiar3] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(32)) = True, 1, 0) & "," & _
                        " [Cambiar4] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(33)) = True, 1, 0) & "," & _
                        " [VentaMayorista] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(34)) = True, 1, 0) & "," & _
                        " [ActualizadoWeb] = 1 " & _
                        " WHERE Codigo = '" & ds_Empresa.Tables(0).Rows(i)(3) & "'"

                    prueba = ds_Empresa.Tables(0).Rows(i)(3).ToString

                    ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_tmpClientes.Dispose()

                Next

                For i = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                    tranWEB.Sql_Get("UPDATE Materiales SET ActualizadoLocal = 1 WHERE CODIGO = '" & ds_Empresa.Tables(0).Rows(i)(3).ToString & "'")
                Next

                tranWEB.Sql_Get("UPDATE NotificacionesWEB SET Materiales = 0 ")


            Catch ex As Exception
                MsgBox(" Desde Actualización Materiales " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
            ''**************************************************************************Unidades****************************************************************************
            Dim ds_Unidades As Data.DataSet
                Try
                    ds_Unidades = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpUnidades_Web")
                    ds_Unidades.Dispose()

                    Dim ds_UnidadesWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM Unidades ")

                    Dim bulk_unidades As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                    Conexion.Open()
                    bulk_unidades.DestinationTableName = "tmpUnidades_Web"
                    bulk_unidades.WriteToServer(ds_UnidadesWEB.Tables(0))
                    Conexion.Close()


                    'Clientes que están en la WEB y no están en la sucursal/central
                    sqlstring = " SELECT [Codigo],[Nombre],[Eliminado] " & _
                                " FROM tmpUnidades_WEB WHERE codigo NOT IN (SELECT codigo FROM Unidades ) "

                    ds_Unidades = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Unidades.Dispose()

                    Dim i As Integer = 0
                    Dim ds_tmpUnidades As Data.DataSet

                    If ds_Unidades.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds_Unidades.Tables(0).Rows.Count - 1
                            sqlstring = " BEGIN TRAN; " & _
                                " INSERT INTO [dbo].[Unidades] ([Codigo],[Nombre],[Eliminado] ) " & _
                                " VALUES ( '" & ds_Unidades.Tables(0).Rows(i)(0) & "', '" & ds_Unidades.Tables(0).Rows(i)(1) & "', " & _
                                IIf(CBool(ds_Unidades.Tables(0).Rows(i)(2)) = True, 1, 0) & " ); " & _
                                " COMMIT TRAN;"

                            ds_tmpUnidades = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                            ds_tmpUnidades.Dispose()

                        Next
                    End If

                    sqlstring = " SELECT [codigo],[Nombre],[Eliminado]" & _
                                " FROM tmpUnidades_Web "

                    ds_Unidades = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Unidades.Dispose()

                    For i = 0 To ds_Unidades.Tables(0).Rows.Count - 1
                        sqlstring = " UPDATE [dbo].[Unidades] SET " & _
                                    " [Codigo] = '" & ds_Unidades.Tables(0).Rows(i)(0) & "', " & _
                                    " [Nombre] = '" & ds_Unidades.Tables(0).Rows(i)(1) & "', " & _
                                    " [Eliminado] = " & IIf(CBool(ds_Unidades.Tables(0).Rows(i)(2)) = True, 1, 0) & " " & _
                                    " WHERE codigo = '" & ds_Unidades.Tables(0).Rows(i)(0) & "'"

                        ds_tmpUnidades = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpUnidades.Dispose()
                    Next
                Catch ex As Exception
                MsgBox("Desde Actualización de Unidades " + ex.Message)
                Me.Cursor = Cursors.Arrow
                End Try


            ''**************************************************************************Rubros****************************************************************************
            Dim ds_Rubros As Data.DataSet
                Try
                    ds_Rubros = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpFamilias_Web")
                    ds_Rubros.Dispose()

                    Dim ds_FamiliasWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM Familias ")

                    Dim bulk_familias As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                    Conexion.Open()
                    bulk_familias.DestinationTableName = "tmpFamilias_Web"
                    bulk_familias.WriteToServer(ds_FamiliasWEB.Tables(0))
                    Conexion.Close()


                    'Clientes que están en la WEB y no están en la sucursal/central
                    sqlstring = " SELECT [Codigo],[Nombre],[Eliminado] " & _
                                " FROM tmpFamilias_WEB WHERE codigo NOT IN (SELECT codigo FROM Familias) "

                    ds_Rubros = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Rubros.Dispose()

                    Dim i As Integer = 0
                    Dim ds_tmpFamilias As Data.DataSet

                    If ds_Rubros.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds_Rubros.Tables(0).Rows.Count - 1
                            sqlstring = " BEGIN TRAN; " & _
                                " INSERT INTO [dbo].[Familias] ([Codigo],[Nombre],[Eliminado] ) " & _
                                " VALUES ('" & ds_Rubros.Tables(0).Rows(i)(0) & "', '" & ds_Rubros.Tables(0).Rows(i)(1) & "', " & _
                                IIf(CBool(ds_Rubros.Tables(0).Rows(i)(2)) = True, 1, 0) & " ); " & _
                                " COMMIT TRAN;"

                            ds_tmpFamilias = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                            ds_tmpFamilias.Dispose()

                        Next
                    End If

                    sqlstring = " SELECT [codigo],[Nombre],[Eliminado]" & _
                                " FROM tmpFamilias_Web "

                    ds_Rubros = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Rubros.Dispose()

                    For i = 0 To ds_Rubros.Tables(0).Rows.Count - 1
                        sqlstring = " UPDATE [dbo].[Familias] SET " & _
                                    " [Codigo] = '" & ds_Rubros.Tables(0).Rows(i)(0) & "', " & _
                                    " [Nombre] = '" & ds_Rubros.Tables(0).Rows(i)(1) & "', " & _
                                    " [Eliminado] = " & IIf(CBool(ds_Rubros.Tables(0).Rows(i)(2)) = True, 1, 0) & " " & _
                                    " WHERE codigo = '" & ds_Rubros.Tables(0).Rows(i)(0) & "'"

                        ds_tmpFamilias = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpFamilias.Dispose()
                    Next
                Catch ex As Exception
                MsgBox("Desde Actualización de Familias " + ex.Message)
                Me.Cursor = Cursors.Arrow
                End Try

            ''**************************************************************************Marcas****************************************************************************

                Try
                    ds_Marcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpMarcas_Web")
                    ds_Marcas.Dispose()

                    Dim ds_MarcasWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM Marcas ")

                    Dim bulk_marcas As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                    Conexion.Open()
                    bulk_marcas.DestinationTableName = "tmpMarcas_Web"
                    bulk_marcas.WriteToServer(ds_MarcasWEB.Tables(0))
                    Conexion.Close()


                    'Clientes que están en la WEB y no están en la sucursal/central
                    sqlstring = " SELECT [Codigo],[Nombre],[Eliminado] " & _
                                " FROM tmpMarcas_WEB WHERE codigo NOT IN (SELECT codigo FROM Marcas ) "

                    ds_Marcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Marcas.Dispose()

                    Dim i As Integer = 0
                    Dim ds_tmpMarcas As Data.DataSet

                    If ds_Marcas.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds_Marcas.Tables(0).Rows.Count - 1
                            sqlstring = " BEGIN TRAN; " & _
                                " INSERT INTO [dbo].[Marcas] ([Codigo],[Nombre],[Eliminado] ) " & _
                                " VALUES ( '" & ds_Marcas.Tables(0).Rows(i)(0) & "', '" & ds_Marcas.Tables(0).Rows(i)(1) & "', " & _
                                IIf(CBool(ds_Marcas.Tables(0).Rows(i)(2)) = True, 1, 0) & " ); " & _
                                " COMMIT TRAN;"

                            ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                            ds_tmpMarcas.Dispose()

                        Next
                    End If

                    sqlstring = " SELECT [codigo],[Nombre],[Eliminado]" & _
                                " FROM tmpMarcas_Web "

                    ds_Marcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_Marcas.Dispose()

                    For i = 0 To ds_Marcas.Tables(0).Rows.Count - 1
                        sqlstring = " UPDATE [dbo].[Marcas] SET " & _
                                    " [Codigo] = '" & ds_Marcas.Tables(0).Rows(i)(0) & "', " & _
                                    " [Nombre] = '" & ds_Marcas.Tables(0).Rows(i)(1) & "', " & _
                                    " [Eliminado] = " & IIf(CBool(ds_Marcas.Tables(0).Rows(i)(2)) = True, 1, 0) & " " & _
                                    " WHERE Codigo = '" & ds_Marcas.Tables(0).Rows(i)(0) & "'"

                        ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpMarcas.Dispose()
                    Next
                Catch ex As Exception
                MsgBox("Desde Actualización de Marcas " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
            btnMateriales.SymbolColor = Color.Green
            End If

        ''**************************************************************************LISTA_PRECIOS****************************************************************************
        If ListaPrecios Then
            Dim ds_Lista As Data.DataSet

            Try

                ds_Lista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpLista_Precios_Web")
                ds_Lista.Dispose()

                Dim ds_ListaWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM Lista_Precios ")

                Dim bulk_Lista As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_Lista.DestinationTableName = "tmpLista_Precios_Web"
                bulk_Lista.WriteToServer(ds_ListaWEB.Tables(0))
                Conexion.Close()

                'ds_Lista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpLista_Precios_WEB")
                'ds_Lista.Dispose()

                'Clientes que están en la WEB y no están en la sucursal/central
                sqlstring = " SELECT [Codigo], [Descripcion],[Porcentaje],[Valor_Cambio],[Eliminado], [DateUpd] " & _
                            " FROM tmpLista_Precios_WEB WHERE Codigo NOT IN (SELECT Codigo FROM Lista_Precios ) "

                ds_Lista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Lista.Dispose()

                Dim i As Integer = 0
                Dim ds_tmpLista As Data.DataSet

                If ds_Lista.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds_Lista.Tables(0).Rows.Count - 1
                        sqlstring = " BEGIN TRAN; " & _
                            " INSERT INTO [dbo].[Lista_Precios] ([Codigo], [Descripcion],[Porcentaje],[Eliminado] ,[DateUpd] ) " & _
                            " VALUES ( " & ds_Lista.Tables(0).Rows(i)(0) & " , '" & ds_Lista.Tables(0).Rows(i)(1) & "', " & ds_Lista.Tables(0).Rows(i)(2) & ", " & _
                            IIf(CBool(ds_Lista.Tables(0).Rows(i)(4)) = True, 1, 0) & " , '" & Format(ds_Lista.Tables(0).Rows(i)(5), "dd/MM/yyyy hh:ss") & "' ); " & _
                            " COMMIT TRAN;"

                        ds_tmpLista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpLista.Dispose()

                    Next
                End If

                sqlstring = " SELECT [Codigo] , [Descripcion],[Porcentaje],[Valor_Cambio], [Eliminado], isnull([DateUPD], '01/01/1900 00:00:00')" & _
                            " FROM tmpLista_Precios_Web "

                ds_Lista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Lista.Dispose()

                For i = 0 To ds_Lista.Tables(0).Rows.Count - 1
                    sqlstring = " UPDATE [dbo].[Lista_Precios] SET " & _
                                " [Codigo] = " & ds_Lista.Tables(0).Rows(i)(0) & ", " & _
                                " [Descripcion] = '" & ds_Lista.Tables(0).Rows(i)(1) & "', " & _
                                " [Porcentaje] = " & ds_Lista.Tables(0).Rows(i)(2) & ", " & _
                                " [Eliminado] = " & IIf(CBool(ds_Lista.Tables(0).Rows(i)(4)) = True, 1, 0) & ", " & _
                                " [DATEUPD] = '" & Format(ds_Lista.Tables(0).Rows(i)(5), "dd/MM/yyyy") & "'  " & _
                                " WHERE Codigo = " & ds_Lista.Tables(0).Rows(i)(0)

                    ds_tmpLista = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_tmpLista.Dispose()
                Next
            Catch ex As Exception
                MsgBox("Desde Actualización de Lista de Precios " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
        End If    

        '***************************************************************************************clientes**********************************************************************
        Dim ds_Client As Data.DataSet

        If Clientes Then
            Try
                ds_Client = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpClientes_Web")
                ds_Client.Dispose()

                Dim ds_ClientWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM Clientes ")

                Dim bulk_client As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_client.DestinationTableName = "tmpClientes_Web"
                bulk_client.WriteToServer(ds_ClientWEB.Tables(0))
                Conexion.Close()


                'Clientes que están en la WEB y no están en la sucursal/central
                sqlstring = " SELECT [IDPrecioLista],[Codigo],[Nombre],[TipoDocumento],[CUIT],[Direccion],[CodPostal]," & _
                            "[Localidad],[Provincia],[Telefono],[Fax],[Email],[Contacto],[Observaciones],[Contrasena]," & _
                            "[Usuario],[UsuarioWEB],[Repartidor],[Eliminado],[DateAdd],[Promo],[CondicionIVA],[MontoMaxCred],[DiasMaxCred],[CtaCte]" & _
                            " FROM tmpClientes_WEB WHERE codigo NOT IN (SELECT codigo FROM Clientes ) "

                ds_Client = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Client.Dispose()

                Dim i As Integer = 0
                Dim ds_tmpClientes As Data.DataSet


                If ds_Client.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds_Client.Tables(0).Rows.Count - 1
                        sqlstring = " BEGIN TRAN; " & _
                         " INSERT INTO [dbo].[Clientes] ([IDPrecioLista],[Codigo],[Nombre],[TipoDocumento],[CUIT],[Direccion],[CodPostal]," & _
                            "[Localidad],[Provincia],[Telefono],[Fax],[Email],[Contacto],[Observaciones],[Contrasena]," & _
                            "[Usuario],[UsuarioWEB],[Repartidor],[Eliminado],[DateAdd],[Promo],[CondicionIVA],[MontoMaxCred],[DiasMaxCred],[CtaCte])" & _
                            " values ( " & ds_Client.Tables(0).Rows(i)(0) & ", '" & ds_Client.Tables(0).Rows(i)(1) & "','" & _
                            ds_Client.Tables(0).Rows(i)(2) & "'," & ds_Client.Tables(0).Rows(i)(3) & ", " & ds_Client.Tables(0).Rows(i)(4) & ",'" & _
                            ds_Client.Tables(0).Rows(i)(5) & "','" & ds_Client.Tables(0).Rows(i)(6) & "','" & ds_Client.Tables(0).Rows(i)(7) & "','" & _
                            ds_Client.Tables(0).Rows(i)(8) & "','" & ds_Client.Tables(0).Rows(i)(9) & "','" & ds_Client.Tables(0).Rows(i)(10) & "','" & _
                            ds_Client.Tables(0).Rows(i)(11) & "','" & ds_Client.Tables(0).Rows(i)(12) & "','" & ds_Client.Tables(0).Rows(i)(13) & "','" & _
                            ds_Client.Tables(0).Rows(i)(14) & "','" & ds_Client.Tables(0).Rows(i)(15) & "'," & IIf(CBool(ds_Client.Tables(0).Rows(i)(16)) = True, 1, 0) & ",'" & _
                            ds_Client.Tables(0).Rows(i)(17) & "'," & IIf(CBool(ds_Client.Tables(0).Rows(i)(18)) = True, 1, 0) & ",'" & _
                            Format(ds_Client.Tables(0).Rows(i)(19), "dd/MM/yyyy hh:ss") & "'," & IIf(CBool(ds_Client.Tables(0).Rows(i)(20)) = True, 1, 0) & ",'" & ds_Client.Tables(0).Rows(i)(21) & "'," & _
                            ds_Client.Tables(0).Rows(i)(22) & "," & ds_Client.Tables(0).Rows(i)(23) & "," & ds_Client.Tables(0).Rows(i)(24) & "); " & _
                            " COMMIT TRAN;"

                        ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpClientes.Dispose()
                    Next
                End If

                sqlstring = " SELECT [IDPrecioLista],[Codigo],[Nombre],[TipoDocumento],[CUIT],[Direccion],[CodPostal]," & _
                            " [Localidad],[Provincia],[Telefono],[Fax],[Email],[Contacto],[Observaciones],[Contrasena]," & _
                            " [Usuario],[UsuarioWEB],[Repartidor],[Eliminado],isnull([DateUPD], '01/01/1900 00:00:00')," & _
                            " [Promo],[CondicionIVA],[MontoMaxCred],[DiasMaxCred],[CtaCte]" & _
                            " FROM tmpClientes_WEB "

                ds_Client = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Client.Dispose()

                For i = 0 To ds_Client.Tables(0).Rows.Count - 1
                    sqlstring = "UPDATE [dbo].[Clientes] SET " & _
                              "[IDPrecioLista] = " & ds_Client.Tables(0).Rows(i)(0) & "," & _
                              "[Nombre] = '" & ds_Client.Tables(0).Rows(i)(2) & "'," & _
                              "[TipoDocumento] = " & ds_Client.Tables(0).Rows(i)(3) & "," & _
                              "[CUIT] = " & ds_Client.Tables(0).Rows(i)(4) & "," & _
                              "[Direccion] = '" & ds_Client.Tables(0).Rows(i)(5) & "'," & _
                              "[CodPostal] = '" & ds_Client.Tables(0).Rows(i)(6) & "'," & _
                              "[Localidad] = '" & ds_Client.Tables(0).Rows(i)(7) & "'," & _
                              "[Provincia] = '" & ds_Client.Tables(0).Rows(i)(8) & "'," & _
                              "[Telefono] = '" & ds_Client.Tables(0).Rows(i)(9) & "'," & _
                              "[Fax] = '" & ds_Client.Tables(0).Rows(i)(10) & "'," & _
                              "[Email] ='" & ds_Client.Tables(0).Rows(i)(11) & "'," & _
                              "[Contacto] = '" & ds_Client.Tables(0).Rows(i)(12) & "'," & _
                              "[Observaciones] = '" & ds_Client.Tables(0).Rows(i)(13) & "'," & _
                              "[Contrasena] = '" & ds_Client.Tables(0).Rows(i)(14) & "'," & _
                              "[Usuario] = '" & ds_Client.Tables(0).Rows(i)(15) & "'," & _
                              "[UsuarioWEB] = " & IIf(CBool(ds_Client.Tables(0).Rows(i)(16)) = True, 1, 0) & "," & _
                              "[Repartidor] = '" & ds_Client.Tables(0).Rows(i)(17) & "'," & _
                              "[Eliminado] = " & IIf(CBool(ds_Client.Tables(0).Rows(i)(18)) = True, 1, 0) & "," & _
                              "[DateUpd] = '" & Format(ds_Client.Tables(0).Rows(i)(19), "dd/MM/yyyy hh:ss") & "'," & _
                              "[Promo] = " & IIf(CBool(ds_Client.Tables(0).Rows(i)(20)) = True, 1, 0) & "," & _
                              "[CondicionIVA] = '" & ds_Client.Tables(0).Rows(i)(21) & "'," & _
                              "[MontoMaxCred] = " & ds_Client.Tables(0).Rows(i)(22) & "," & _
                              "[DiasMaxCred]  = " & ds_Client.Tables(0).Rows(i)(23) & "," & _
                              "[CtaCte] = " & ds_Client.Tables(0).Rows(i)(24) & " " & _
                              " WHERE Codigo = '" & ds_Client.Tables(0).Rows(i)(1) & "'"

                    ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_tmpClientes.Dispose()
                Next



            Catch ex As Exception
                'MsgBox(sqlstring)
                MsgBox(" Desde Actualización clientes " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
        End If



        '*******************************************************************************empleados**********************************************************************
        Dim ds_Empleado As Data.DataSet

        If Empleados Then
            Try
                ds_Empleado = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpEmpleados_Web")
                ds_Empleado.Dispose()

                Dim ds_ClientWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM Empleados ")

                Dim bulk_client As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

                Conexion.Open()
                bulk_client.DestinationTableName = "tmpEmpleados_Web"
                bulk_client.WriteToServer(ds_ClientWEB.Tables(0))
                Conexion.Close()

                sqlstring = "SELECT [Codigo],[Apellido],[Nombre],[Domicilio],[Telefono],[Celular],[Cuit] ," & _
                            "[Email],[UsuarioSistema],[Usuario],[Pass],[Revendedor],[Repartidor],[Eliminado],[DateAdd],[Autoriza],[Vendedor]" & _
                            " FROM tmpEmpleados_WEB WHERE codigo NOT IN (SELECT codigo FROM Empleados ) "

                Dim i As Integer = 0
                ds_Empleado = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empleado.Dispose()

                Dim ds_tmpEmpleados As Data.DataSet

                If ds_Empleado.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds_Empleado.Tables(0).Rows.Count - 1
                        sqlstring = " BEGIN TRAN; " & _
                       " INSERT INTO [dbo].[Empleados] ([Codigo],[Apellido],[Nombre],[Domicilio],[Telefono],[Celular],[Cuit]," & _
                       " [Email],[UsuarioSistema],[Usuario],[Pass],[Revendedor],[Repartidor],[Eliminado],[DateAdd],[Autoriza],[Vendedor]) " & _
                       " VALUES ( '" & ds_Empleado.Tables(0).Rows(i)(0) & "','" & ds_Empleado.Tables(0).Rows(i)(1) & "','" & _
                        ds_Empleado.Tables(0).Rows(i)(2) & "','" & ds_Empleado.Tables(0).Rows(i)(3) & "','" & ds_Empleado.Tables(0).Rows(i)(4) & "','" & _
                        ds_Empleado.Tables(0).Rows(i)(5) & "'," & ds_Empleado.Tables(0).Rows(i)(6) & ",'" & ds_Empleado.Tables(0).Rows(i)(7) & "'," & _
                        IIf(CBool(ds_Empleado.Tables(0).Rows(i)(8)) = True, 1, 0) & ",'" & ds_Empleado.Tables(0).Rows(i)(9) & "','" & ds_Empleado.Tables(0).Rows(i)(10) & "'," & _
                        IIf(CBool(ds_Empleado.Tables(0).Rows(i)(11)) = True, 1, 0) & "," & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(12)) = True, 1, 0) & "," & _
                        IIf(CBool(ds_Empleado.Tables(0).Rows(i)(13)) = True, 1, 0) & ",'" & Format(ds_Empleado.Tables(0).Rows(i)(14), "dd/MM/yyyy hh:ss") & "'," & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(15)) = True, 1, 0) & "," & _
                        IIf(CBool(ds_Empleado.Tables(0).Rows(i)(16)) = True, 1, 0) & "); " & _
                      " COMMIT TRAN;"

                        ds_tmpEmpleados = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpEmpleados.Dispose()

                    Next
                End If

                sqlstring = "SELECT [Codigo],[Apellido],[Nombre],[Domicilio],[Telefono],[Celular],[Cuit] ," & _
                          "[Email],[UsuarioSistema],[Usuario],[Pass],[Revendedor],[Repartidor],[Eliminado],isnull([DateUPD], '01/01/1900 00:00:00'),[Autoriza],[Vendedor]" & _
                          " FROM tmpEmpleados_WEB "

                ds_Empleado = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                ds_Empleado.Dispose()

                '"[Pass] = '" & ds_Empleado.Tables(0).Rows(i)(10) & "'," & _

                For i = 0 To ds_Empleado.Tables(0).Rows.Count - 1
                    sqlstring = "UPDATE [dbo].[Empleados] SET " & _
                    "[Apellido] = '" & ds_Empleado.Tables(0).Rows(i)(1) & "'," & _
                    "[Nombre] = '" & ds_Empleado.Tables(0).Rows(i)(2) & "'," & _
                    "[Domicilio] = '" & ds_Empleado.Tables(0).Rows(i)(3) & "'," & _
                    "[Telefono] = '" & ds_Empleado.Tables(0).Rows(i)(4) & "'," & _
                    "[Celular] = '" & ds_Empleado.Tables(0).Rows(i)(5) & "'," & _
                    "[Cuit] = " & ds_Empleado.Tables(0).Rows(i)(6) & "," & _
                    "[Email] = '" & ds_Empleado.Tables(0).Rows(i)(7) & "'," & _
                    "[UsuarioSistema] = " & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(8)) = True, 1, 0) & "," & _
                    "[Usuario] = '" & ds_Empleado.Tables(0).Rows(i)(9) & "'," & _
                    "[Revendedor] = " & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(11)) = True, 1, 0) & "," & _
                    "[Repartidor] = " & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(12)) = True, 1, 0) & "," & _
                    "[Eliminado] = " & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(13)) = True, 1, 0) & "," & _
                    "[DateUPD] = '" & Format(ds_Empleado.Tables(0).Rows(i)(14), "dd/MM/yyyy hh:ss") & "'," & _
                    "[Autoriza] = " & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(15)) = True, 1, 0) & "," & _
                    "[Vendedor] = " & IIf(CBool(ds_Empleado.Tables(0).Rows(i)(16)) = True, 1, 0) & " " & _
                    " WHERE Codigo = '" & ds_Empleado.Tables(0).Rows(i)(0) & "'"

                    ds_tmpEmpleados = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                    ds_tmpEmpleados.Dispose()

                Next

            Catch ex As Exception
                MsgBox(" Desde Actualización Empleados " + ex.Message)
                Me.Cursor = Cursors.Arrow
            End Try
        End If

        Me.Cursor = Cursors.Arrow
        '------------------------------temporalmente inactivo-----------------------------
        Timer1.Enabled = True
        MDIPrincipal.ToolStripStatusLabel.BackColor = Color.Lime
        MDIPrincipal.ToolStripStatusLabel.Text = "Actualizado"

    End Sub

    Public Sub BuscarNotificaciones()
        Dim ds_Empresa As Data.DataSet
        Dim Conexion As SqlConnection
        Dim res As Integer


        If MDIPrincipal.sucursal.Contains("PERON") Then
            ' ---PERÓN---
            Conexion = New SqlConnection("Data Source=PORKIS-SERVER;Initial Catalog=Porkys;User ID=sa;Password=industrial")
        Else
            '---MARCONI---
            Conexion = New SqlConnection("Data Source=servidor;Initial Catalog=Porkys;User ID=sa;Password=industrial")
        End If
        '---MI MAQUINA---
        'Conexion = New SqlConnection("Data Source=SAMBA-PC;Initial Catalog=Porkys;User ID=sa;Password=industrial")
        Try
            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpNotificaciones_WEB")
            ds_Empresa.Dispose()

            Dim ds_Stock As DataSet = tranWEB.Sql_Get("select * from NotificacionesWEB")

            Dim bulk_Stock As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

            Conexion.Open()
            bulk_Stock.DestinationTableName = "tmpNotificaciones_WEB"
            bulk_Stock.WriteToServer(ds_Stock.Tables(0))
            Conexion.Close()

            sqlstring = "select BloqueoT,Transferencias,BloqueoR,Recepciones,BloqueoM,Materiales,BloqueoL,ListaPrecios,BloqueoC,Clientes,BloqueoE,Empleados from tmpNotificaciones_WEB"

            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds_Empresa.Dispose()

            If ds_Empresa.Tables(0).Rows.Count > 0 Then

                btnTransferencia.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(0)
                Transferencias = ds_Empresa.Tables(0).Rows(0).Item(1)

                If btnTransferencia.Enabled = False Then
                    'coloco el simbolo de actualizacion
                    btnTransferencia.Symbol = ChrW("&Hf021")
                    btnTransferencia.SymbolColor = Color.Black
                Else
                    btnTransferencia.Symbol = ChrW("&Hf0ed")
                    res = tranWEB.Sql_Get_Value("SELECT count(*) FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'T' And IDDestino = " & numero_almacen)
                    If Transferencias And res > 0 Then
                        btnTransferencia.SymbolColor = Color.Red
                    Else
                        btnTransferencia.SymbolColor = Color.Green
                    End If
                End If

                btnRecepciones.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(2)
                Recepciones = ds_Empresa.Tables(0).Rows(0).Item(3)

                If btnRecepciones.Enabled = False Then
                    'coloco el simbolo de actualizacion
                    btnRecepciones.Symbol = ChrW("&Hf021")
                    btnRecepciones.SymbolColor = Color.Black
                Else
                    btnRecepciones.Symbol = ChrW("&Hf0ed")
                    res = tranWEB.Sql_Get_Value("SELECT count(*) FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'R' And IDDestino = " & numero_almacen)

                    If Recepciones And res > 0 Then
                        btnRecepciones.SymbolColor = Color.Red
                    Else
                        btnRecepciones.SymbolColor = Color.Green
                    End If
                End If

                btnMateriales.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(4)
                Materiales = ds_Empresa.Tables(0).Rows(0).Item(5)

                If btnMateriales.Enabled = False Then
                    'coloco el simbolo de actualizacion
                    btnMateriales.Symbol = ChrW("&Hf0ac")
                    btnMateriales.SymbolColor = Color.Black
                Else
                    btnMateriales.Symbol = ChrW("&Hf0ed")
                    If Materiales Then
                        btnMateriales.SymbolColor = Color.Red
                    Else
                        btnMateriales.SymbolColor = Color.Green
                    End If
                End If

                btnListaPrecios.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(6)
                ListaPrecio = ds_Empresa.Tables(0).Rows(0).Item(7)

                If btnListaPrecios.Enabled = False Then
                    'coloco el simbolo de actualizacion
                    btnListaPrecios.Symbol = ChrW("&Hf0ac")
                    btnListaPrecios.SymbolColor = Color.Black
                Else
                    btnListaPrecios.Symbol = ChrW("&Hf0ed")
                    If ListaPrecio Then
                        btnListaPrecios.SymbolColor = Color.Red
                    Else
                        btnListaPrecios.SymbolColor = Color.Green
                    End If
                End If

                'btnClientes.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(8)
                'Clientes = ds_Empresa.Tables(0).Rows(0).Item(9)

                'If btnClientes.Enabled = False Then
                '    'coloco el simbolo de actualizacion
                '    btnClientes.Symbol = ChrW("&Hf0ac")
                '    btnClientes.SymbolColor = Color.Black
                'Else
                '    btnClientes.Symbol = ChrW("&Hf0ed")
                '    If Clientes Then
                '        btnClientes.SymbolColor = Color.Red
                '    Else
                '        btnClientes.SymbolColor = Color.Green
                '    End If
                'End If

                'btnEmpleados.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(10)
                'Empleados = ds_Empresa.Tables(0).Rows(0).Item(11)

                'If btnEmpleados.Enabled = False Then
                '    'coloco el simbolo de actualizacion
                '    btnEmpleados.Symbol = ChrW("&Hf0ac")
                '    btnEmpleados.SymbolColor = Color.Black
                'Else
                '    btnEmpleados.Symbol = ChrW("&Hf0ed")
                '    If Empleados Then
                '        btnEmpleados.SymbolColor = Color.Red
                '    Else
                '        btnEmpleados.SymbolColor = Color.Green
                '    End If
                'End If

            End If
        Catch ex As Exception
            MsgBox("Error no se puedo consultar las Notificaciones WEB. Por favor intente más tarde. " + ex.Message)
        End Try
        
    End Sub

    Private Sub btnTransferencia_Click(sender As Object, e As EventArgs) Handles btnTransferencia.Click
        If btnTransferencia.Checked Then
            btnTransferencia.Checked = False
        Else
            btnTransferencia.Checked = True
        End If
    End Sub

    Private Sub btnTransferencia_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnTransferencia.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(True, False, False, False, False, False, False)
        End If

    End Sub

    Private Sub btnRecepciones_Click(sender As Object, e As EventArgs) Handles btnRecepciones.Click
        If btnRecepciones.Checked Then
            btnRecepciones.Checked = False
        Else
            btnRecepciones.Checked = True
        End If
    End Sub

    Private Sub btnRecepciones_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRecepciones.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(False, True, False, False, False, False, False)
        End If
    End Sub

    Private Sub btnMateriales_Click(sender As Object, e As EventArgs) Handles btnMateriales.Click
        If btnMateriales.Checked Then
            btnMateriales.Checked = False
        Else
            btnMateriales.Checked = True
        End If
    End Sub

    Private Sub btnMateriales_MouseDown(sender As Object, e As MouseEventArgs) Handles btnMateriales.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(False, False, True, False, False, False, False)
        End If
    End Sub

    Private Sub btnListaPrecios_Click(sender As Object, e As EventArgs) Handles btnListaPrecios.Click
        If btnListaPrecios.Checked Then
            btnListaPrecios.Checked = False
        Else
            btnListaPrecios.Checked = True
        End If
    End Sub

    Private Sub btnListaPrecios_MouseDown(sender As Object, e As MouseEventArgs) Handles btnListaPrecios.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(False, False, False, True, False, False, False)
        End If
    End Sub

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        If btnClientes.Checked Then
            btnClientes.Checked = False
        Else
            btnClientes.Checked = True
        End If
    End Sub

    Private Sub btnClientes_MouseDown(sender As Object, e As MouseEventArgs) Handles btnClientes.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(False, False, False, False, True, False, False)
        End If
    End Sub

    Private Sub btnEmpleados_Click(sender As Object, e As EventArgs) Handles btnEmpleados.Click
        If btnEmpleados.Checked Then
            btnEmpleados.Checked = False
        Else
            btnEmpleados.Checked = True
        End If
    End Sub

    Private Sub btnEmpleados_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEmpleados.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(False, False, False, False, False, True, False)
        End If
    End Sub

    Private Sub btnStock_Click(sender As Object, e As EventArgs) Handles btnStock.Click
        If btnStock.Checked Then
            btnStock.Checked = False
        Else
            btnStock.Checked = True
        End If
    End Sub

    Private Sub btnStock_MouseDown(sender As Object, e As MouseEventArgs) Handles btnStock.MouseDown
        If e.Clicks = 2 Then
            ActualizarSistema(False, False, False, False, False, False, True)
        End If
    End Sub

    Private Sub btnActualizarSistema_Click(sender As Object, e As EventArgs) Handles btnActualizarSistema.Click


        ActualizarSistema(IIf(btnTransferencia.Enabled = False, False, btnTransferencia.Checked), _
                          IIf(btnRecepciones.Enabled = False, False, btnRecepciones.Checked), _
                          IIf(btnMateriales.Enabled = False, False, btnMateriales.Checked), _
                          IIf(btnListaPrecios.Enabled = False, False, btnListaPrecios.Checked), _
                          IIf(btnClientes.Enabled = False, False, btnClientes.Checked), _
                          IIf(btnEmpleados.Enabled = False, False, btnEmpleados.Checked), _
                          btnStock.Checked)
    End Sub



End Class