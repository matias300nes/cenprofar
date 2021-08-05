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

   

#Region "   Eventos"

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim r As Rectangle = My.Computer.Screen.WorkingArea
        Location = New Point(r.Width - Width - 90, r.Height - Height - 100)

        If MDIPrincipal.sucursal.Contains("PERON") Then
            btnStock.Text = " Descargar Stock Principal"
        Else
            btnStock.Text = "Descargar Stock Perón"
            btnMateriales.Enabled = False
            btnListaPrecios.Enabled = False
            btnRecepciones.Enabled = False
            btnDescargarAjuste.Visible = False
        End If
        btnEmpleados.Enabled = False
        btnClientes.Enabled = False
        'establezco las conexiones
        'MDIPrincipal.EstablecerConexionWEB_ServerLocal()
        'busco las notificaciones
        BuscarNotificaciones()

        Timer1.Enabled = True


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        BuscarNotificaciones()

        Timer1.Enabled = True

    End Sub

    'Private Sub btnTransferencia_Click(sender As Object, e As EventArgs) Handles btnTransferencia.Click
    '    If btnTransferencia.Checked Then
    '        btnTransferencia.Checked = False
    '    Else
    '        btnTransferencia.Checked = True
    '    End If
    'End Sub

    Private Sub btnTransferencia_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnTransferencia.MouseDown
        If e.Clicks = 2 Then
            Dim T As New frmTransRecepWEB
            T.Show()
            'ActualizarSistema(True, False, False, False, False, False, False)
        End If

    End Sub

    'Private Sub btnRecepciones_Click(sender As Object, e As EventArgs) Handles btnRecepciones.Click
    '    If btnRecepciones.Checked Then
    '        btnRecepciones.Checked = False
    '    Else
    '        btnRecepciones.Checked = True
    '    End If
    'End Sub

    Private Sub btnRecepciones_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRecepciones.MouseDown
        If e.Clicks = 2 Then
            Dim R As New frmTransRecepWEB
            R.Show()
            'ActualizarSistema(False, True, False, False, False, False, False)
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
            MDIPrincipal.ActualizarSistema(False, False, True, False, False, False, False)
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
            MDIPrincipal.ActualizarSistema(False, False, False, True, False, False, False)
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
            MDIPrincipal.ActualizarSistema(False, False, False, False, True, False, False)
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
            MDIPrincipal.ActualizarSistema(False, False, False, False, False, True, False)
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
            MDIPrincipal.ActualizarSistema(False, False, False, False, False, False, True)
        End If
    End Sub

    Private Sub btnDescargarAjuste_Click(sender As Object, e As EventArgs) Handles btnDescargarAjuste.Click

        MDIPrincipal.DescargarAjustesStock()
       
    End Sub

    Private Sub btnActualizarSistema_Click(sender As Object, e As EventArgs) Handles btnActualizarSistema.Click

        MDIPrincipal.ActualizarSistema(IIf(btnTransferencia.Enabled = False, False, btnTransferencia.Checked), _
                          IIf(btnRecepciones.Enabled = False, False, btnRecepciones.Checked), _
                          IIf(btnMateriales.Enabled = False, False, btnMateriales.Checked), _
                          IIf(btnListaPrecios.Enabled = False, False, btnListaPrecios.Checked), _
                          IIf(btnClientes.Enabled = False, False, btnClientes.Checked), _
                          IIf(btnEmpleados.Enabled = False, False, btnEmpleados.Checked), _
                          btnStock.Checked)
    End Sub

#End Region

#Region "   Procedimientos"


    Public Sub BuscarNotificaciones()
        Dim ds_Empresa As Data.DataSet
        Dim res As Integer



        Try
            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpNotificaciones_WEB")
            ds_Empresa.Dispose()

            Dim ds_Stock As DataSet = tranWEB.Sql_Get("select * from [" & NameTable_NotificacionesWEB & "]")

            Dim bulk_Stock As New SqlBulkCopy(ConexionWEB, SqlBulkCopyOptions.TableLock, Nothing)

            ConexionWEB.Open()
            bulk_Stock.DestinationTableName = "tmpNotificaciones_WEB"
            bulk_Stock.WriteToServer(ds_Stock.Tables(0))
            ConexionWEB.Close()

            sqlstring = "select BloqueoT,Transferencias,BloqueoR,Recepciones,BloqueoM,Materiales,BloqueoL,ListaPrecios,BloqueoC,Clientes,BloqueoE,Empleados from tmpNotificaciones_WEB where idalmacen = " & Util.numero_almacen

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
                    'res = tranWEB.Sql_Get_Value("SELECT count(*) FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'T' And IDDestino = " & numero_almacen)
                    res = tranWEB.Sql_Get_Value("SELECT count(*) FROM [" & NameTable_TransRecepWEB & "] Where DescargarEnDestino = 1  And Tipo  = 'T' And IDDestino = " & numero_almacen)
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
                    'res = tranWEB.Sql_Get_Value("SELECT count(*) FROM Transferencias_Recepciones_WEB Where Procesado = 0 And Tipo  = 'R' And IDDestino = " & numero_almacen)
                    res = tranWEB.Sql_Get_Value("SELECT count(*) FROM [" & NameTable_TransRecepWEB & "] Where DescargarEnDestino = 1  And Tipo  = 'R' And IDDestino = " & numero_almacen)

                    If Recepciones And res > 0 Then
                        btnRecepciones.SymbolColor = Color.Red
                    Else
                        btnRecepciones.SymbolColor = Color.Green
                    End If
                End If

                'si es peron habilito materiales 
                If MDIPrincipal.sucursal.Contains("PERON") Then

                    btnMateriales.Enabled = Not ds_Empresa.Tables(0).Rows(0).Item(4)
                    Materiales = ds_Empresa.Tables(0).Rows(0).Item(5)

                    If btnMateriales.Enabled = False Then
                        'coloco el simbolo del mundo que deshabilita el boton porque estan usando la pantalla de materiales
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
                Else
                    btnMateriales.Symbol = ChrW("&Hf0ed")
                    btnMateriales.SymbolColor = Color.Gray
                    btnListaPrecios.Symbol = ChrW("&Hf0ed")
                    btnListaPrecios.SymbolColor = Color.Gray
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
            Me.Close()
        End Try

    End Sub


#End Region




End Class