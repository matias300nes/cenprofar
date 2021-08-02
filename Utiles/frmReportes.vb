Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports System.Windows.Forms

Public Class frmReportes

    'dg 4/02/2011
    Public Sub MostrarMaestroDevolucionesProveedor(ByVal codigo As String, ByVal ReporteMaestroDevolucionesProveedor As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_DevolucionesProveedor.rpt"
        If (LoguearReporte(ReporteMaestroDevolucionesProveedor, nbrereporte)) Then
            Try
                ReporteMaestroDevolucionesProveedor.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroDevolucionesProveedor.rdReportes.SetParameterValue("@codigo_DevolucionesProveedor", codigo)
                ReporteMaestroDevolucionesProveedor.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    '' dg 24-02-2011
    ''impresion del vale de consumo
    'Public Sub MostrarVale(ByVal ReporteVale As frmReportes, ByVal salida As Integer, ByVal codigo As String, ByVal primeravez As String)
    '    Dim nbrereporte As String = "Panol_Vale.rpt"
    '    If (LoguearReporte(ReporteVale, nbrereporte)) Then
    '        Try
    '            ReporteVale.rdReportes.DataDefinition.FormulaFields.Reset()
    '            ReporteVale.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
    '            ReporteVale.rdReportes.DataDefinition.FormulaFields("primeravezformula").Text = primeravez
    '            ReporteVale.rdReportes.SetParameterValue("@codigo_Consumos", codigo)

    '            If salida = 1 Then 'por pantalla...
    '                ReporteVale.Show()
    '            Else 'directo a la impresora...
    '                ReporteVale.rdReportes.PrintToPrinter(1, False, 0, 0)
    '            End If
    '        Catch ex As Exception
    '            ControladorDeExepcionesReportes(ex, nbrereporte)
    '        Finally
    '            filtradopor = ""
    '        End Try

    '    End If
    'End Sub
    ' dg 24-02-2011
    'impresion del vale de consumo
    Public Sub MostrarVale(ByVal ReporteVale As frmReportes, ByVal salida As Integer, ByVal codigo As String, ByVal primeravez As String, ByVal copias As Integer)
        Dim nbrereporte As String = "Panol_Vale.rpt"
        If (LoguearReporte(ReporteVale, nbrereporte)) Then
            Try
                ReporteVale.rdReportes.DataDefinition.FormulaFields.Reset()
                ReporteVale.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteVale.rdReportes.DataDefinition.FormulaFields("primeravezformula").Text = primeravez
                ReporteVale.rdReportes.SetParameterValue("@codigo_Consumos", codigo)

                If salida = 1 Then 'por pantalla...
                    ReporteVale.Show()
                Else 'directo a la impresora...
                    ReporteVale.rdReportes.PrintToPrinter(copias, False, 0, 0)
                End If
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    Public Sub MostrarReporteAsociacionControlesMateriales(ByVal codigo As String, ByVal reporteasoccontrolesMateriales As frmReportes)
        Dim nbrereporte As String = "PANOL_AsociarControlesMateriales.rpt"
        If (LoguearReporte(reporteasoccontrolesMateriales, nbrereporte)) Then
            Try
                'reporteasoccontrolesMateriales.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteasoccontrolesMateriales.rdReportes.SetParameterValue("@codigo_ControlesMateriales", codigo)
                reporteasoccontrolesMateriales.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub MostrarReporteAsociacionControlesMP(ByVal codigo As String, ByVal reporteasoccontrolesMP As frmReportes)
        Dim nbrereporte As String = "TM3_AsociacionCotrolesMP.rpt"
        If (LoguearReporte(reporteasoccontrolesMP, nbrereporte)) Then
            Try
                reporteasoccontrolesMP.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteasoccontrolesMP.rdReportes.SetParameterValue("@codigo", codigo)
                reporteasoccontrolesMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarReporteAsociacionControlesPaquetes(ByVal codigo As String, ByVal reporteasoccontrolespaq As frmReportes)
        Dim nbrereporte As String = "TM3_AsociacionCotrolesPaquetes.rpt"
        If (LoguearReporte(reporteasoccontrolespaq, nbrereporte)) Then
            Try
                reporteasoccontrolespaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteasoccontrolespaq.rdReportes.SetParameterValue("@codigo", codigo)
                reporteasoccontrolespaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroTransferencias(ByVal codigo As String, ByVal ReporteMaestroTransferencias As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Transferencias.rpt"
        If (LoguearReporte(ReporteMaestroTransferencias, nbrereporte)) Then
            Try
                ReporteMaestroTransferencias.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroTransferencias.rdReportes.SetParameterValue("@codigo_Transferencias", codigo)
                ReporteMaestroTransferencias.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    Public Sub ObtenerResultadoReporteOC(ByVal oc As String, ByVal reporteoc As frmReportes)

        Me.Cursor = Cursors.WaitCursor
        Dim nbrereporte As String = "TM3_OrdenCorte.rpt"
        If (LoguearReporte(reporteoc, nbrereporte)) Then
            Try
                reporteoc.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteoc.rdReportes.SetParameterValue("@oc", oc)
                ''LE DIGO QUE EL FORM PADRE ES EL MdiForm
                ''reporteoc.MdiParent = WinreportNet.MdiForm
                ''MUESTRO EL REPORTE
                reporteoc.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try

        End If
        Me.Cursor = Cursors.Default
    End Sub
    Public Sub ObtenerResultadoReporteOCPendiente(ByVal ocpendiente As String, ByVal reporteocpendiente As frmReportes)
        Dim nbrereporte As String = "TM3_OrdenCortePendientes.rpt"
        If (LoguearReporte(reporteocpendiente, nbrereporte)) Then
            Try
                reporteocpendiente.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteocpendiente.rdReportes.SetParameterValue("@idoc", ocpendiente)
                ''LE DIGO QUE EL FORM PADRE ES EL MdiForm
                ''reporteocpendiente.MdiParent = WinreportNet.MdiForm
                ''MUESTRO EL REPORTE
                reporteocpendiente.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarFlejesConsumidos(ByVal op As String, ByVal reporteflejescons As frmReportes)
        ''COMENTADO MS 30-09-2010 A PEDIDO DE LUCAS MARTOS
        ''Dim nbrereporte As String = "TM3_ConsumosFlejes.rpt"
        ''FIN COMENTADO
        ''NUEVO MS 30-09-2010
        Dim nbrereporte As String = "TM3_ListadoDeFlejesPorOP.rpt"
        ''IFN NUEVO
        If (LoguearReporte(reporteflejescons, nbrereporte)) Then
            Try
                reporteflejescons.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteflejescons.rdReportes.SetParameterValue("@op", op)
                ''reporteflejescons.MdiParent = WinreportNet.MdiForm
                reporteflejescons.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarEnsayosMP(ByVal codmp As String, ByVal reporteensayosMP As frmReportes)
        Dim nbrereporte As String = "TM3_EnsayosMateriaPrimas.rpt"
        If (LoguearReporte(reporteensayosMP, nbrereporte)) Then
            Try
                reporteensayosMP.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteensayosMP.rdReportes.SetParameterValue("@cod_mp", codmp)
                ''reporteensayosMP.MdiParent = WinreportNet.MdiForm
                reporteensayosMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarEnsayosPaquetes(ByVal op As String, ByVal paq As String, ByVal reporteensayospaq As frmReportes)
        Dim nbrereporte As String = "TM3_EnsayosPaquetes.rpt"
        If (LoguearReporte(reporteensayospaq, nbrereporte)) Then
            Try
                reporteensayospaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteensayospaq.rdReportes.SetParameterValue("@op", op)
                reporteensayospaq.rdReportes.SetParameterValue("@paq", paq)
                ''reporteensayospaq.MdiParent = WinreportNet.MdiForm
                reporteensayospaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarGruposDeOPs(ByVal op As String, ByVal reportegruposdeops As frmReportes)
        Dim nbrereporte As String = "TM3_OPsPorGrupo.rpt"
        If (LoguearReporte(reportegruposdeops, nbrereporte)) Then
            Try
                reportegruposdeops.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportegruposdeops.rdReportes.SetParameterValue("@p_op", op)
                ''reportegruposdeops.MdiParent = WinreportNet.MdiForm
                reportegruposdeops.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarProduccionPaquetes(ByVal op As String, ByVal fechadesde As String, ByVal fechahasta As String, ByVal reporteprodpaq As frmReportes)
        Dim nbrereporte As String = "TM3_PaquetesProduccion.rpt"
        If (LoguearReporte(reporteprodpaq, nbrereporte)) Then
            Try
                reporteprodpaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteprodpaq.rdReportes.SetParameterValue("@op", op)
                reporteprodpaq.rdReportes.SetParameterValue("@FechaD", fechadesde)
                reporteprodpaq.rdReportes.SetParameterValue("@FechaH", fechahasta)
                ''reporteprodpaq.MdiParent = WinreportNet.MdiForm
                reporteprodpaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarPaquetesDespachados(ByVal reporteprodpaq As frmReportes, Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        Dim nbrereporte As String = "Tm3_DESPACHO_DetalleDespacho115Totales_1.rpt" 'definir despues

        LlamadaAStoreQueGeneraTablaQueUsaElReporteDespachados(conexion)

        If (LoguearReporte(reporteprodpaq, nbrereporte)) Then
            Try

                'reporteprodpaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteprodpaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If

    End Sub

    Public Sub MostrarPaquetesParaDespachar(ByVal sNivel As String, ByVal reporteprodpaq As frmReportes, Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        Dim nbrereporte As String = "Tm3_DESPACHO_DetalleDespachoParaDespachar_1.rpt" 'definir despues

        LlamadaAStoreQueGeneraTablaQueUsaElReporteDespachosParaReservar(sNivel, conexion)

        If (LoguearReporte(reporteprodpaq, nbrereporte)) Then
            Try
                reporteprodpaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteprodpaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub MostrarMovDeStock(ByVal id As String, ByVal fechadesde As String, ByVal reportemovdestock As frmReportes)
        Dim nbrereporte As String = "TM3_MovimientosStock.rpt"
        If (LoguearReporte(reportemovdestock, nbrereporte)) Then
            Try
                reportemovdestock.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportemovdestock.rdReportes.SetParameterValue("@id", id)
                reportemovdestock.rdReportes.SetParameterValue("@desde", fechadesde)
                ''reportemovdestock.MdiParent = WinreportNet.MdiForm
                reportemovdestock.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub MostrarMovDeStockBobinas(ByVal id As String, ByVal fechadesde As String, ByVal reportemovdestockbob As frmReportes)
        Dim nbrereporte As String = "TM3_MovimientosStockBobinas.rpt"
        If (LoguearReporte(reportemovdestockbob, nbrereporte)) Then
            Try
                reportemovdestockbob.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportemovdestockbob.rdReportes.SetParameterValue("@id", id)
                reportemovdestockbob.rdReportes.SetParameterValue("@desde", fechadesde)
                ''reportemovdestockbob.MdiParent = WinreportNet.MdiForm
                reportemovdestockbob.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarMovDeStockFlejes(ByVal id As String, ByVal fechadesde As String, ByVal reportemostockfle As frmReportes)
        Dim nbrereporte As String = "TM3_MovimientosStockFlejes.rpt"
        If (LoguearReporte(reportemostockfle, nbrereporte)) Then
            Try
                reportemostockfle.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportemostockfle.rdReportes.SetParameterValue("@id", id)
                reportemostockfle.rdReportes.SetParameterValue("@desde", fechadesde)
                ''reportemostockfle.MdiParent = WinreportNet.MdiForm
                reportemostockfle.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarMovDeStockMP(ByVal id As String, ByVal fechadesde As String, ByVal reportestockMP As frmReportes)
        Dim nbrereporte As String = "TM3_MovimientosStockMateriaPrima.rpt"
        If (LoguearReporte(reportestockMP, nbrereporte)) Then
            Try
                reportestockMP.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportestockMP.rdReportes.SetParameterValue("@id", id)
                reportestockMP.rdReportes.SetParameterValue("@desde", fechadesde)
                ''reportestockMP.MdiParent = WinreportNet.MdiForm
                reportestockMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarReporteStockBobinas(ByVal periodo As String, ByVal reportestockdebob As frmReportes)
        Dim nbrereporte As String = "TM3_StockBobinas.rpt"
        If (LoguearReporte(reportestockdebob, nbrereporte)) Then
            Try
                reportestockdebob.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportestockdebob.rdReportes.SetParameterValue("@periodo", periodo)
                '''''ESTA LINEA IMPRIME DIRECTAMENTE EN LA IMPRESORA    
                '''''reportestockdebob.rdReportes.PrintToPrinter(1, False, 0, 0)
                ''reportestockdebob.MdiParent = WinreportNet.MdiForm
                reportestockdebob.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarReporteStockFlejes(ByVal periodo As String, ByVal reportestockdefle As frmReportes)
        Dim nbrereporte As String = "TM3_StockFlejes.rpt"
        If (LoguearReporte(reportestockdefle, nbrereporte)) Then
            Try
                reportestockdefle.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportestockdefle.rdReportes.SetParameterValue("@periodo", periodo)
                ''reportestockdefle.MdiParent = WinreportNet.MdiForm
                reportestockdefle.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarReporteMP(ByVal periodo As String, ByVal reportestockMP As frmReportes)
        Dim nbrereporte As String = "TM3_StockMateriaPrima.rpt"
        If (LoguearReporte(reportestockMP, nbrereporte)) Then
            Try
                reportestockMP.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportestockMP.rdReportes.SetParameterValue("@periodo", periodo)
                ''reportestockMP.MdiParent = WinreportNet.MdiForm
                reportestockMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarReporteStockPaquetes(ByVal diseno As String, ByVal estado As String, ByVal periodo As String, ByVal reportestockpaq As frmReportes)
        Dim nbrereporte As String = "TM3_StockPaquetes.rpt"
        If (LoguearReporte(reportestockpaq, nbrereporte)) Then
            Try
                reportestockpaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportestockpaq.rdReportes.SetParameterValue("@cod_paq", diseno)
                reportestockpaq.rdReportes.SetParameterValue("@status", estado)
                reportestockpaq.rdReportes.SetParameterValue("@periodo", periodo)
                ''reportestockpaq.MdiParent = WinreportNet.MdiForm
                reportestockpaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub LlamadaAStoreQueGeneraTablaQueUsaElReporteDespachosParaReservar(ByVal sNivel As String, Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        Try
            'CnnParadas.Open()
            conexion.Open()
            Dim param As SqlParameter
            Dim commandcntanterior As SqlCommand = New SqlCommand("PaquetesTm3_DetalleDespachosParaReservarTotales_Ins", conexion)
            commandcntanterior.CommandType = CommandType.StoredProcedure

            Dim adapter As SqlDataAdapter = New SqlDataAdapter(commandcntanterior)

            param = New SqlParameter
            param.ParameterName = "@sNivel"
            param.Value = sNivel
            param.Size = 50
            commandcntanterior.Parameters.Add(param)

            commandcntanterior.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("No se pudo conectar a la Base de Datos " + ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Advertencia")
        Finally
            ''cierro la conexion                                                
            'CnnParadas.Close()
            conexion.Close()
        End Try
    End Sub
    Public Sub LlamadaAStoreQueGeneraTablaQueUsaElReporteDespachados(Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        Try
            conexion.Open()
            Dim commandcntanterior As SqlCommand = New SqlCommand("PaquetesTm3_DetalleDespachadosTotales_Ins", conexion)
            commandcntanterior.CommandType = CommandType.StoredProcedure

            commandcntanterior.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("No se pudo conectar a la Base de Datos " + ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Advertencia")
        Finally
            ''cierro la conexion                                                
            'CnnParadas.Close()
            conexion.Close()
        End Try
    End Sub
    Public Sub MostrarMaestroDeDisenos(ByVal reportemaestrodedisenos As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_Disenos.rpt"
        If (LoguearReporte(reportemaestrodedisenos, nbrereporte)) Then
            Try
                reportemaestrodedisenos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroDeMotivos(ByVal reportemotivos As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_Motivos.rpt"
        If (LoguearReporte(reportemotivos, nbrereporte)) Then
            Try
                ''''frmParametros.Dispose()
                ''reportemotivos.MdiParent = WinreportNet.MdiForm
                reportemotivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroDeSectores(ByVal reportesectores As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_Sectores.rpt"
        If (LoguearReporte(reportesectores, nbrereporte)) Then
            Try
                ''''frmParametros.Dispose()
                ''reportesectores.MdiParent = WinreportNet.MdiForm
                reportesectores.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroTipoDeControles(ByVal reportetipocontroles As frmReportes)
        Dim nbrereportetipocontroles As String = "TM3_Maestro_TipoCotroles.rpt"
        If (LoguearReporte(reportetipocontroles, nbrereportetipocontroles)) Then
            Try
                ''''frmParametros.Dispose()
                ''reportetipocontroles.MdiParent = WinreportNet.MdiForm
                reportetipocontroles.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereportetipocontroles)
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroDeUnidades(ByVal reporteunidades As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_Unidades.rpt"
        If (LoguearReporte(reporteunidades, nbrereporte)) Then
            Try
                ''frmParametros.Dispose()
                ''reporteunidades.MdiParent = WinreportNet.MdiForm
                reporteunidades.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Sub MostrarIngresoMP(ByVal idingreso As String, ByVal reporteingresoMP As frmReportes)
        Dim nbrereporte As String = "TM3_IngresosMateriaPrima.rpt"
        If (LoguearReporte(reporteingresoMP, nbrereporte)) Then

            Try
                reporteingresoMP.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteingresoMP.rdReportes.SetParameterValue("@ingreso", idingreso)
                ''reporteingresoMP.MdiParent = WinreportNet.MdiForm
                reporteingresoMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarFallasPaquetes(ByVal op As String, ByVal reportefallaspaq As frmReportes)
        Dim nbrereporte As String = "TM3_PaquetesFallas.rpt"
        If (LoguearReporte(reportefallaspaq, nbrereporte)) Then
            Try
                reportefallaspaq.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportefallaspaq.rdReportes.SetParameterValue("@op", op)
                ''reportefallaspaq.MdiParent = WinreportNet.MdiForm
                reportefallaspaq.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Private Sub frmReportes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = nbreformreportes
    End Sub
    Public Sub ObtenerPaquetesPorOP(ByVal op As String, ByVal reporteop As frmReportes)
        Dim nbrereporte As String = "TM3_ListadoDePaquetesPorOP.rpt"
        If (LoguearReporte(reporteop, nbrereporte)) Then
            Try
                reporteop.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteop.rdReportes.SetParameterValue("@op", op)
                ''LE DIGO QUE EL FORM PADRE ES EL MdiForm
                ''reporteop.MdiParent = WinreportNet.MdiForm
                ''MUESTRO EL REPORTE
                reporteop.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub ObtenerFlejesPorOP(ByVal op As String, ByVal reportefle As frmReportes)
        Dim nbrereporte As String = "TM3_ListadoDeFlejesPorOP.rpt"
        If (LoguearReporte(reportefle, nbrereporte)) Then
            Try
                reportefle.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportefle.rdReportes.SetParameterValue("@op", op)
                ''LE DIGO QUE EL FORM PADRE ES EL MdiForm
                ''reportefle.MdiParent = WinreportNet.MdiForm
                ''MUESTRO EL REPORTE
                reportefle.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroDeMP(ByVal reportemaestrodeMP As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_MateriasPrimas.rpt"
        If (LoguearReporte(reportemaestrodeMP, nbrereporte)) Then
            Try
                ''''frmParametros.Dispose()
                ''reportemaestrodeMP.MdiParent = WinreportNet.MdiForm
                reportemaestrodeMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Function LoguearReporte(ByVal reporte As frmReportes, ByVal nbrereporte As String) As Boolean
        ''VARIABLE DE PARAMETROS DE CONECCION
        Dim myConnectionInfo As New ConnectionInfo
        ''RUTA DEL REPORTE
        Dim reportFilePath As String = pathrpt & nbrereporte
        ''DEFINO LOS PARAMETROS DE CONECCION

        ''''''''''''''''''''''''''''''''''''''''''
        ''LEVANTARLOS DE UN INI
        ''''''''''''''''''''''''''''''''''''''''''
        ''NUEVO MS 19-07-2010
        Dim strLocation As String
        ''FIN NUEVO

        ''NUEVO MS 13-04-2011
        Dim nombre As String
        nombre = Mid$(nbrereporte, 1, InStr(Trim$(nbrereporte), "_") - 1).ToUpper
        If nombre <> "RN" And nombre <> "TM3" Then
            nombre = ""
            nombre = Mid$(nbrereporte, 1, 4)

            myConnectionInfo = ObtenermyConnectionInfo(nombre)
        Else
            myConnectionInfo = ObtenermyConnectionInfo(nombre)
        End If


        'With myConnectionInfo
        '    .ServerName = TipoConexionWinreportNet.server
        '    .DatabaseName = TipoConexionWinreportNet.base
        '    .UserID = TipoConexionWinreportNet.usuario
        '    .Password = TipoConexionWinreportNet.contrasena
        'End With



        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim CrTables As Tables
        Dim CrTable As Table


        Try
            reporte.rdReportes.Load(reportFilePath)
        Catch
            MsgBox("No se pudo encontrar el Reporte en  " + reportFilePath & vbCrLf & _
                    "Comuniquese con el Departamento de Sistemas", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Advertencia")
            Return False
            Exit Function
        End Try

        Try
            CrTables = reporte.rdReportes.Database.Tables

            For Each CrTable In CrTables

                crtableLogoninfo = CrTable.LogOnInfo

                crtableLogoninfo.ConnectionInfo = myConnectionInfo

                CrTable.ApplyLogOnInfo(crtableLogoninfo)
                ''NUEVO MS DG 19-07-2010
                Dim aux As String
                aux = Replace(CrTable.Location.ToString.ToLower, "proc(", "")
                aux = Replace(aux, ";1)", "")
                aux = Replace(aux, ",1)", "")

                strLocation = myConnectionInfo.DatabaseName & ".dbo." & aux
                Try
                    CrTable.Location = strLocation
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
                ''FIN NUEVO
            Next
            'SETEA EL LOGON INFO PARA CUALQUIER SUBREPORTE DEL REPORTE PRINCIPAL
            For Each subReportDocument As ReportDocument In reporte.rdReportes.Subreports
                For Each subReportTable As CrystalDecisions.CrystalReports.Engine.Table In subReportDocument.Database.Tables
                    Try
                        subReportTable.ApplyLogOnInfo(crtableLogoninfo)
                        ''NUEVO MS DG 19-07-2010
                        Dim aux As String
                        aux = Replace(subReportTable.Location.ToString.ToLower, "proc(", "")
                        aux = Replace(aux, ";1)", "")
                        aux = Replace(aux, ",1)", "")

                        strLocation = myConnectionInfo.DatabaseName & ".dbo." & aux
                        Try
                            subReportTable.Location = strLocation
                            subReportDocument.VerifyDatabase()

                        Catch ex As Exception
                            MsgBox(ex.Message.ToString)
                        End Try
                        ''FIN NUEVO
                    Catch ex As Exception
                        '
                    End Try
                Next
            Next
            ''SI NO SE PONE EL VERIFIDATABASE NO FILTRA Y APARECE EL REPORTE EN BLANCO
            ''NUEVO
            ''Try
            reporte.rdReportes.VerifyDatabase()
            ''Catch ex As Exception
            ''    MsgBox("Verify DataBase")
            ''    MsgBox(ex.Message.ToString)
            ''End Try


            ''LE ASIGNA EL DATA SOURCE  AL CRYSTAL REPORT VIEWER
            reporte.crvReportes.ReportSource = reporte.rdReportes
            ''NUEVO MS 02-09-2010
            ' ''Try
            Utiles.LoguearUsoDelReporte(nbrereporte, UserActual)
            ''Catch ex As Exception
            ''    MsgBox("Loguear Uso de Reporte")
            ''    MsgBox(ex.Message.ToString)
            ''End Try


            ''FIN NUEVO



            Return True
        Catch ex2 As Exception
            ' '' ''MsgBox("Catch Grande ex2")

            MsgBox(ex2.Message.ToString)
        End Try
    End Function
    Public Function ObtenermyConnectionInfo(ByVal nombre As String) As ConnectionInfo
        Dim myConnectionInfo As New ConnectionInfo


        If nombre = "RN" Then
            With myConnectionInfo
                .ServerName = TipoConexionRN.server
                .DatabaseName = TipoConexionRN.base
                .UserID = TipoConexionRN.usuario
                .Password = TipoConexionRN.contrasena
            End With
        ElseIf nombre = "TM3" Then
            With myConnectionInfo
                .ServerName = TipoConexionTM3.server
                .DatabaseName = TipoConexionTM3.base
                .UserID = TipoConexionTM3.usuario
                .Password = TipoConexionTM3.contrasena
            End With
            ''PANOL
        ElseIf nombre = "PANO" Then
            With myConnectionInfo
                .ServerName = TipoConexionPanol.server
                .DatabaseName = TipoConexionPanol.base
                .UserID = TipoConexionPanol.usuario
                .Password = TipoConexionPanol.contrasena
            End With
            ''INSTRUMENTAL
        ElseIf nombre = "INST" Then
            With myConnectionInfo
                .ServerName = TipoConexionInstrumental.server
                .DatabaseName = TipoConexionInstrumental.base
                .UserID = TipoConexionInstrumental.usuario
                .Password = TipoConexionInstrumental.contrasena
            End With
            ''PARADAS
        ElseIf nombre = "PARA" Then
            With myConnectionInfo
                .ServerName = TipoConexionParadas.server
                .DatabaseName = TipoConexionParadas.base
                .UserID = TipoConexionParadas.usuario
                .Password = TipoConexionParadas.contrasena
            End With
            ''DESPACHO
        ElseIf nombre = "DESP" Then
            With myConnectionInfo
                .ServerName = TipoConexionDespacho.server
                .DatabaseName = TipoConexionDespacho.base
                .UserID = TipoConexionDespacho.usuario
                .Password = TipoConexionDespacho.contrasena
            End With
            ''PESADAS
        ElseIf nombre = "PESA" Then
            With myConnectionInfo
                .ServerName = TipoConexionPesadas.server
                .DatabaseName = TipoConexionPesadas.base
                .UserID = TipoConexionPesadas.usuario
                .Password = TipoConexionPesadas.contrasena
            End With


        End If

        ObtenermyConnectionInfo = myConnectionInfo

    End Function
    Public Sub MostrarFlejesBaseOP(ByVal op As String, ByVal reporteflebaseop As frmReportes)
        Dim nbrereporte As String = "TM3_OpFlejeBase.rpt"
        If (LoguearReporte(reporteflebaseop, nbrereporte)) Then

            Try
                reporteflebaseop.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteflebaseop.rdReportes.SetParameterValue("@idop", op)
                ''reporteflebaseop.MdiParent = WinreportNet.MdiForm
                reporteflebaseop.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarFlejesOptimosOP(ByVal op As String, ByVal reportefleoptimoop As frmReportes)
        Dim nbrereporte As String = "TM3_OpFlejesOptimos.rpt"
        If (LoguearReporte(reportefleoptimoop, nbrereporte)) Then
            Try
                reportefleoptimoop.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportefleoptimoop.rdReportes.SetParameterValue("@idop", op)
                ''reportefleoptimoop.MdiParent = WinreportNet.MdiForm
                reportefleoptimoop.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'dg..
    Public Sub MostrarOCsPendientes(ByVal oc As String, ByVal reporteOCsPendientes As frmReportes)
        Dim nbrereporte As String = "TM3_OrdenCortePendientes.rpt"
        If (LoguearReporte(reporteOCsPendientes, nbrereporte)) Then
            Try
                reporteOCsPendientes.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteOCsPendientes.rdReportes.SetParameterValue("@idoc", oc)
                ''reporteOCsPendientes.MdiParent = MDIPrincipal
                reporteOCsPendientes.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'dg..
    Public Sub MostrarOpFlejeBase(ByVal op As String, ByVal reporteOpFlejeBase As frmReportes)
        Dim nbrereporte As String = "TM3_OpFlejeBase.rpt"
        If (LoguearReporte(reporteOpFlejeBase, nbrereporte)) Then
            Try
                reporteOpFlejeBase.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteOpFlejeBase.rdReportes.SetParameterValue("@idop", op)
                ''reporteOpFlejeBase.MdiParent = MDIPrincipal
                reporteOpFlejeBase.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'dg..
    Public Sub MostrarOpFlejesOptimos(ByVal op As String, ByVal reporteOpFlejesOptimos As frmReportes)
        Dim nbrereporte As String = "TM3_OpFlejesOptimos.rpt"
        If (LoguearReporte(reporteOpFlejesOptimos, nbrereporte)) Then
            Try
                reporteOpFlejesOptimos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor

                reporteOpFlejesOptimos.rdReportes.SetParameterValue("@idop", op)
                ''reporteOpFlejesOptimos.MdiParent = MDIPrincipal
                reporteOpFlejesOptimos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'dg..
    Public Sub MostrarDiseñosFlejeBase(ByVal codigo_diseno As String, ByVal reporteDiseñosFlejeBase As frmReportes)
        Dim nbrereporte As String = "TM3_DisenosFlejes.rpt"
        If (LoguearReporte(reporteDiseñosFlejeBase, nbrereporte)) Then
            Try
                reporteDiseñosFlejeBase.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reporteDiseñosFlejeBase.rdReportes.SetParameterValue("@cod_diseno", codigo_diseno)
                ''reporteDiseñosFlejeBase.MdiParent = MDIPrincipal
                reporteDiseñosFlejeBase.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'DG 28-01-11
    Public Sub MostrarMaestroMateriales(ByVal codigo As String, ByVal ReporteMaestroMateriales As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Materiales.rpt"
        If (LoguearReporte(ReporteMaestroMateriales, nbrereporte)) Then
            Try
                ReporteMaestroMateriales.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroMateriales.rdReportes.SetParameterValue("@codigo_Materiales", codigo)
                ReporteMaestroMateriales.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'dg..
    Public Sub MostrarMaestroMP(ByVal reporteMaestroMP As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_MateriasPrimas.rpt"
        If (LoguearReporte(reporteMaestroMP, nbrereporte)) Then
            Try
                reporteMaestroMP.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ''reporteMaestroMP.MdiParent = MDIPrincipal
                reporteMaestroMP.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    'dg..
    Public Sub MostrarEtiquetaBobina(ByVal idingreso As String, ByVal bobina As String, ByVal reporteEtiquetaBobina As frmReportes)

        Dim nbrereporte As String = "TM3_zgEtiquetasBobinas.rpt"
        If (LoguearReporte(reporteEtiquetaBobina, nbrereporte)) Then

            Try
                reporteEtiquetaBobina.rdReportes.SetParameterValue("@nro_ingreso", idingreso)
                reporteEtiquetaBobina.rdReportes.SetParameterValue("@nro_bobina", bobina)
                'reporteEtiquetaBobina.MdiParent = frmIngresoMateriaPrimaDetalle 'MDIPrincipal
                'reporteEtiquetaBobina.BringToFront()
                reporteEtiquetaBobina.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            End Try
        End If
    End Sub
    'dg..
    Public Sub MostrarFlejesDeOPs(ByVal op As String, ByVal flejesporop As frmReportes)
        Dim nbrereporte As String = "TM3_ListadoDeFlejesPorOP.rpt"
        If (LoguearReporte(flejesporop, nbrereporte)) Then
            Try
                flejesporop.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                flejesporop.rdReportes.SetParameterValue("@op", op)
                ''flejesporop.MdiParent = MDIPrincipal
                flejesporop.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    'dg..
    Public Sub MostrarPaquetesDeOPs(ByVal op As String, ByVal paquetesporop As frmReportes)
        Dim nbrereporte As String = "TM3_ListadoDePaquetesPorOP.rpt"
        If (LoguearReporte(paquetesporop, nbrereporte)) Then
            Try
                paquetesporop.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                paquetesporop.rdReportes.SetParameterValue("@op", op)
                ''paquetesporop.MdiParent = MDIPrincipal
                paquetesporop.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub ImprimirEtiquetaFleje(ByVal EtiquetaFleje As frmReportes, ByVal salida As Integer, ByVal bobina As Long)
        Dim nbrereporte As String = "TM3_zgEtiquetasFlejes 2.rpt"
        If (LoguearReporte(EtiquetaFleje, nbrereporte)) Then
            Try
                EtiquetaFleje.rdReportes.SetParameterValue("@bobina", bobina)
                If salida = 1 Then 'por pantalla...
                    ''EtiquetaFleje.MdiParent = MDIPrincipal
                    EtiquetaFleje.Show()
                Else 'directo a la impresora...
                    EtiquetaFleje.rdReportes.PrintToPrinter(1, False, 0, 0)
                End If
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            End Try
        End If
    End Sub
    'dg..
    Public Sub ImprimirEtiquetaPaquete(ByVal EtiquetaPaquete As frmReportes, ByVal salida As Integer, ByVal paquete As String, ByVal op As String)
        Dim nbrereporte As String = "TM3_zgEtiquetasPaquetes.rpt"
        If (LoguearReporte(EtiquetaPaquete, nbrereporte)) Then
            Try
                EtiquetaPaquete.rdReportes.SetParameterValue("@paq", paquete)
                EtiquetaPaquete.rdReportes.SetParameterValue("@op", op)
                If salida = 1 Then 'por pantalla...
                    ''EtiquetaPaquete.MdiParent = MDIPrincipal
                    EtiquetaPaquete.Show()
                Else 'directo a la impresora...
                    EtiquetaPaquete.rdReportes.PrintToPrinter(1, False, 0, 0)
                End If
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroDeControles(ByVal reportemaestrodecontroles As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_TipoCotroles.rpt"
        If (LoguearReporte(reportemaestrodecontroles, nbrereporte)) Then
            Try
                ''reportemaestrodecontroles.MdiParent = MDIPrincipal
                reportemaestrodecontroles.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub

    Public Sub MostrarMaestroDeControlesPanol(ByVal reportemaestrodecontroles As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_TipoControles.rpt"
        If (LoguearReporte(reportemaestrodecontroles, nbrereporte)) Then
            Try
                ''reportemaestrodecontroles.MdiParent = MDIPrincipal
                reportemaestrodecontroles.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub

    'DG 02-09-2010...
    Public Sub MostrarReporteEstadisticaFallas(ByVal op As String, ByVal reportestockdefle As frmReportes)
        Dim nbrereporte As String = "TM3_EstadisticaMotivos.rpt"


        If (LoguearReporte(reportestockdefle, nbrereporte)) Then
            Try
                reportestockdefle.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                reportestockdefle.rdReportes.SetParameterValue("@op", op)
                ''reportestockdefle.MdiParent = WinreportNet.MdiForm
                reportestockdefle.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
                Exit Sub
            Finally
                filtradopor = ""
            End Try
        End If

    End Sub

    'DG 28-01-11
    Public Sub MostrarMaestroAlmacenes(ByVal codigo As String, ByVal ReporteMaestroAlmacenes As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Almacenes.rpt"
        If (LoguearReporte(ReporteMaestroAlmacenes, nbrereporte)) Then
            Try
                ReporteMaestroAlmacenes.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroAlmacenes.rdReportes.SetParameterValue("@codigo_Almacenes", codigo)
                ReporteMaestroAlmacenes.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub


    Public Sub MostrarMaestroClasificaciones(ByVal ReporteMaestroClasificaciones As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Clasificaciones.rpt"
        If (LoguearReporte(ReporteMaestroClasificaciones, nbrereporte)) Then
            Try
                ReporteMaestroClasificaciones.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub


    Public Sub MostrarMaestroPresupuesto(ByVal codigo As String, ByVal ReporteMaestroPresupuesto As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Presupuesto.rpt"
        If (LoguearReporte(ReporteMaestroPresupuesto, nbrereporte)) Then
            Try
                ReporteMaestroPresupuesto.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroPresupuesto.rdReportes.SetParameterValue("@codigo_Ajustes", codigo)
                ReporteMaestroPresupuesto.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub MostrarMaestroAjustes(ByVal codigo As String, ByVal ReporteMaestroAjustes As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Ajustes.rpt"
        If (LoguearReporte(ReporteMaestroAjustes, nbrereporte)) Then
            Try
                ReporteMaestroAjustes.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroAjustes.rdReportes.SetParameterValue("@codigo_Ajustes", codigo)
                ReporteMaestroAjustes.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub MostrarMaestroRecepciones(ByVal codigo As String, ByVal ReporteMaestroRecepciones As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Recepciones.rpt"
        If (LoguearReporte(ReporteMaestroRecepciones, nbrereporte)) Then
            Try
                ReporteMaestroRecepciones.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroRecepciones.rdReportes.SetParameterValue("@codigo_Recepcion", codigo)
                ReporteMaestroRecepciones.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    Public Sub MostrarMaestroConsumos(ByVal codigo As String, ByVal mat As String, ByVal ret As String, ByVal des As Date, ByVal has As Date, ByVal ReporteMaestroConsumos As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Consumos.rpt"
        If (LoguearReporte(ReporteMaestroConsumos, nbrereporte)) Then
            Try
                ReporteMaestroConsumos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroConsumos.rdReportes.SetParameterValue("@codigo_Consumos", codigo)
                ReporteMaestroConsumos.rdReportes.SetParameterValue("@id_material", mat)
                ReporteMaestroConsumos.rdReportes.SetParameterValue("@codigo_usuario_retira", ret)
                ReporteMaestroConsumos.rdReportes.SetParameterValue("@desde", des)
                ReporteMaestroConsumos.rdReportes.SetParameterValue("@hasta", has)
                ReporteMaestroConsumos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub


    'Public Sub MostrarMaestroRecepciones1(ByVal ReporteMaestroRecepciones As frmReportes)
    '    Dim nbrereporte As String = "Panol_Maestro_Recepciones.rpt"
    '    If (LoguearReporte(ReporteMaestroRecepciones, nbrereporte)) Then
    '        Try
    '            ReporteMaestroRecepciones.Show()
    '        Catch ex As Exception
    '            ControladorDeExepcionesReportes(ex, nbrereporte)
    '        End Try
    '    End If
    'End Sub

    Public Sub MostrarMaestroConsumos1(ByVal ReporteMaestroConsumos As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Consumos.rpt"
        If (LoguearReporte(ReporteMaestroConsumos, nbrereporte)) Then
            Try
                ReporteMaestroConsumos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub

    Public Sub MostrarMaestroPresupuesto_Det(ByVal ReporteMaestroPresupuesto_Det As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Presupuesto_Det.rpt"
        If (LoguearReporte(ReporteMaestroPresupuesto_Det, nbrereporte)) Then
            Try
                ReporteMaestroPresupuesto_Det.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    'dg 06/10/2010
    Public Sub MostrarMaestroDePerfiles(ByVal reporteperfiles As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_Perfiles.rpt"
        If (LoguearReporte(reporteperfiles, nbrereporte)) Then
            Try
                reporteperfiles.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    'dg 06/10/2010
    Public Sub MostrarMaestroDeUsuariosPerfiles(ByVal reporteUsuariosPerfiles As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_UsuariosPerfiles.rpt"
        If (LoguearReporte(reporteUsuariosPerfiles, nbrereporte)) Then
            Try
                reporteUsuariosPerfiles.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    'jpm 01/02/2011
    Public Sub MostrarMaestroDeUsuarios(ByVal reporteUsuarios As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_Usuarios.rpt"
        If (LoguearReporte(reporteUsuarios, nbrereporte)) Then
            Try
                reporteUsuarios.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub

    'dg 07/10/2010
    Public Sub MostrarMaestroDePerfilesAccesos(ByVal reportePerfilesAccesos As frmReportes)
        Dim nbrereporte As String = "TM3_Maestro_PerfilesAccesos.rpt"
        If (LoguearReporte(reportePerfilesAccesos, nbrereporte)) Then
            Try
                reportePerfilesAccesos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarMaestroInstrumentos(ByVal codigo As String, ByVal ReporteMaestroInstrumentos As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_Maestro_Instrumentos.rpt"
        If (LoguearReporte(ReporteMaestroInstrumentos, nbrereporte)) Then
            Try
                ReporteMaestroInstrumentos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroInstrumentos.rdReportes.SetParameterValue("@codigo", codigo)
                ReporteMaestroInstrumentos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarMaestroTipoControles(ByVal ReporteMaestroTipoControles As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_Maestro_TipoControles.rpt"
        If (LoguearReporte(ReporteMaestroTipoControles, nbrereporte)) Then
            Try
                ReporteMaestroTipoControles.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'DG 28-01-11
    Public Sub MostrarMaestroFamilias(ByVal codigo As String, ByVal ReporteMaestroFamilias As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Familias.rpt"
        If (LoguearReporte(ReporteMaestroFamilias, nbrereporte)) Then
            Try
                ReporteMaestroFamilias.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroFamilias.rdReportes.SetParameterValue("@codigo_Familias", codigo)
                ReporteMaestroFamilias.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarMaestroSectores(ByVal ReporteMaestroSectores As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_Maestro_Sectores.rpt"
        If (LoguearReporte(ReporteMaestroSectores, nbrereporte)) Then
            Try
                ReporteMaestroSectores.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarMaestroSectoresPanol(ByVal ReporteMaestroSectoresPanol As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Sectores.rpt"
        If (LoguearReporte(ReporteMaestroSectoresPanol, nbrereporte)) Then
            Try
                ReporteMaestroSectoresPanol.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub


    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarMaestroControlesHerramientas(ByVal codigo As String, ByVal ReporteMaestroControlesHerramientas As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_Maestro_ControlesHerramientas.rpt"
        If (LoguearReporte(ReporteMaestroControlesHerramientas, nbrereporte)) Then
            Try
                ReporteMaestroControlesHerramientas.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroControlesHerramientas.rdReportes.SetParameterValue("@codigo", codigo)
                ReporteMaestroControlesHerramientas.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub



    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarVencimientos(ByVal inicio As String, ByVal fin As String, ByVal ReporteVencimientos As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_Vencimientos.rpt"
        If (LoguearReporte(ReporteVencimientos, nbrereporte)) Then
            Try
                ReporteVencimientos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteVencimientos.rdReportes.SetParameterValue("@inicio", inicio)
                ReporteVencimientos.rdReportes.SetParameterValue("@fin", fin)
                ReporteVencimientos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarPlanillaVaciaVencidos(ByVal inicio As String, ByVal fin As String, ByVal PlanillaVaciaVencidos As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_PlanillaVaciaVencidos.rpt"
        If (LoguearReporte(PlanillaVaciaVencidos, nbrereporte)) Then
            Try
                PlanillaVaciaVencidos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                PlanillaVaciaVencidos.rdReportes.SetParameterValue("@inicio", inicio)
                PlanillaVaciaVencidos.rdReportes.SetParameterValue("@fin", fin)
                PlanillaVaciaVencidos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub


    Public Sub MostrarMaestroUnidades(ByVal ReporteMaestroUnidades As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Unidades.rpt"
        If (LoguearReporte(ReporteMaestroUnidades, nbrereporte)) Then
            Try
                ReporteMaestroUnidades.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub


    'DG 28-01-11
    Public Sub MostrarMaestroGerencias(ByVal codigo As String, ByVal ReporteMaestroGerencias As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Gerencias.rpt"
        If (LoguearReporte(ReporteMaestroGerencias, nbrereporte)) Then
            Try
                ReporteMaestroGerencias.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroGerencias.rdReportes.SetParameterValue("@codigo_Gerencias", codigo)
                ReporteMaestroGerencias.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    'DG 28-01-11
    Public Sub MostrarMaestroCentroCostos(ByVal codigo As String, ByVal ReporteMaestroCentroCostos As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_CentroCostos.rpt"
        If (LoguearReporte(ReporteMaestroCentroCostos, nbrereporte)) Then
            Try
                ReporteMaestroCentroCostos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroCentroCostos.rdReportes.SetParameterValue("@codigo_CentrosCostos", codigo)
                ReporteMaestroCentroCostos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub

    Public Sub MostrarMaestroFamiliasPañol(ByVal ReporteMaestroFamiliasPañol As frmReportes)
        Dim nbrereporte As String = "Panol_Maestro_Familias.rpt"
        If (LoguearReporte(ReporteMaestroFamiliasPañol, nbrereporte)) Then
            Try
                ReporteMaestroFamiliasPañol.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub

  

    ' ************** Cortar y pegar este código en el form frmReportes del Proyecto Utiles ***************** '
    Public Sub MostrarPlanillaVacia(ByVal id As String, ByVal PlanillaVacia As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_PlanillaVacia.rpt"
        If (LoguearReporte(PlanillaVacia, nbrereporte)) Then
            Try
                PlanillaVacia.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                PlanillaVacia.rdReportes.SetParameterValue("@id", id)
                PlanillaVacia.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub


    Public Sub MostrarRegistro(ByVal id As String, ByVal Registro As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_Registro.rpt"
        If (LoguearReporte(Registro, nbrereporte)) Then
            Try
                Registro.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                Registro.rdReportes.SetParameterValue("@id", id)
                Registro.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub


    Public Sub MostrarPlanillaLlena(ByVal id As String, ByVal PlanillaLlena As frmReportes)
        Dim nbrereporte As String = "INSTRUMENTAL_PlanillaLlena.rpt"
        If (LoguearReporte(PlanillaLlena, nbrereporte)) Then
            Try
                PlanillaLlena.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                PlanillaLlena.rdReportes.SetParameterValue("@id", id)
                PlanillaLlena.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    Public Sub MostrarTerminaciones(ByVal reporteterminaciones As frmReportes)
        Dim nbrereporte As String = "TM3_TerminacionPaquetesTM3.rpt"
        If (LoguearReporte(reporteterminaciones, nbrereporte)) Then
            Try
                reporteterminaciones.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            End Try
        End If
    End Sub
    Public Sub MostrarMaestroProveedores(ByVal codigo As String, ByVal ReporteMaestroProveedores As frmReportes)
        Dim nbrereporte As String = "PANOL_Maestro_Proveedores.rpt"
        If (LoguearReporte(ReporteMaestroProveedores, nbrereporte)) Then
            Try
                ReporteMaestroProveedores.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteMaestroProveedores.rdReportes.SetParameterValue("@codigo_Proveedor", codigo)

                ReporteMaestroProveedores.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub




    'DG 16-03-2011
    Public Sub MostrarStockCero(ByVal codigo As String, ByVal ReporteStockCero As frmReportes)
        Dim nbrereporte As String = "Panol_StockCero.rpt"
        If (LoguearReporte(ReporteStockCero, nbrereporte)) Then
            Try
                ReporteStockCero.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteStockCero.rdReportes.SetParameterValue("@codigo_material", codigo)
                ReporteStockCero.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    'DG 16-03-2011
    Public Sub MostrarPlanillaInventario(ByVal codigo As String, ByVal ubi As String, ByVal ReportePlanillaInventario As frmReportes)
        Dim nbrereporte As String = "Panol_PlanillaInventario.rpt"
        If (LoguearReporte(ReportePlanillaInventario, nbrereporte)) Then
            Try
                ReportePlanillaInventario.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReportePlanillaInventario.rdReportes.SetParameterValue("@codigo_familia", codigo)
                ReportePlanillaInventario.rdReportes.SetParameterValue("@ubicacion", ubi)
                ReportePlanillaInventario.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    'DG 17-03-2011
    Public Sub MostrarBajaRotacion(ByVal inicio As String, ByVal fin As String, ByVal ReporteBajaRotacion As frmReportes)
        Dim nbrereporte As String = "Panol_BajaRotacion.rpt"
        If (LoguearReporte(ReporteBajaRotacion, nbrereporte)) Then
            Try
                ReporteBajaRotacion.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteBajaRotacion.rdReportes.SetParameterValue("@Desde", inicio)
                ReporteBajaRotacion.rdReportes.SetParameterValue("@Hasta", fin)
                ReporteBajaRotacion.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub


    'DG 05-04-2011
    Public Sub MostrarMaestroParadas(ByVal maquina As String, ByVal op As String, ByVal motivo As String, ByVal ReporteParadas As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_Paradas.rpt"
        If (LoguearReporte(ReporteParadas, nbrereporte)) Then
            Try
                ReporteParadas.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteParadas.rdReportes.SetParameterValue("@Maquina", maquina)
                ReporteParadas.rdReportes.SetParameterValue("@OP", op)
                ReporteParadas.rdReportes.SetParameterValue("@Motivo", motivo)
                ReporteParadas.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    Public Sub MostrarMaestroFamilias_Instrumental(ByVal ReporteMaestroFamilias As frmReportes)
        Dim nbrereporte As String = "Instrumental_Maestro_Familias.rpt"
        If (LoguearReporte(ReporteMaestroFamilias, nbrereporte)) Then
            Try
                filtradopor = ""
                ''ReporteMaestroFamilias.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ''ReporteMaestroFamilias.rdReportes.SetParameterValue("@codigo_Familias", codigo)
                ReporteMaestroFamilias.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'CP 16-05-2011
    Public Sub MostrarReporteAnalisisDeRendimiento(ByVal periodo As String, ByVal maquina As String, ByVal ReporteAnalisisDeRendimiento As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_AnalisisRendimiento_2.rpt"
        If (LoguearReporte(ReporteAnalisisDeRendimiento, nbrereporte)) Then
            Try
                ReporteAnalisisDeRendimiento.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ''ReporteAnalisisDeRendimiento.rdReportes.SetParameterValue("@Periodo", periodo)
                ''ReporteAnalisisDeRendimiento.rdReportes.SetParameterValue("@Maquina", maquina)
                ReporteAnalisisDeRendimiento.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub


    'DG 13-04-2011
    Public Sub MostrarReporteSeguimientoDeProduccion(ByVal maquina As String, ByVal ReporteSeguimientoDeProduccion As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_SeguimientoProduccion.rpt"
        If (LoguearReporte(ReporteSeguimientoDeProduccion, nbrereporte)) Then
            Try
                ReporteSeguimientoDeProduccion.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                'ReporteAnalisisDeRendimiento.rdReportes.SetParameterValue("@Periodo", periodo)
                ReporteSeguimientoDeProduccion.rdReportes.SetParameterValue("@Maquina", maquina)
                ReporteSeguimientoDeProduccion.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    'DG 13-04-2011
    'Public Sub MostrarReporteTiempoImproductivo(ByVal inicio As Date, ByVal fin As Date, ByVal maquina As String, ByVal op As String, ByVal automatico As Boolean, ByVal ReporteTiemposImproductivos As frmReportes)
	Public Sub MostrarReporteTiempoImproductivo(ByVal inicio As Date, ByVal fin As Date, ByVal maquina As String, ByVal op As String, ByVal ReporteTiemposImproductivos As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_TiemposImproductivos.rpt"
        If (LoguearReporte(ReporteTiemposImproductivos, nbrereporte)) Then
            Try
                ReporteTiemposImproductivos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@FechaInicio", inicio)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@FechaFin", fin)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@Maquina", maquina)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@OP", op)
                'ReporteTiemposImproductivos.rdReportes.SetParameterValue("@automatico", automatico)
                ReporteTiemposImproductivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    'DG 14-04-2011
    Public Sub MostrarReporteAnalisisDeRendimientoPorLínea(ByVal inicio As Date, ByVal fin As Date, ByVal automatico As Boolean, ByVal ReporteTiemposImproductivos As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_AnalisisDeRendimientoPorLinea.rpt"
        If (LoguearReporte(ReporteTiemposImproductivos, nbrereporte)) Then
            Try
                ReporteTiemposImproductivos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@FechaInicio", inicio)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@FechaFin", fin)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@automatico", automatico)
                ReporteTiemposImproductivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    'DG 14-04-2011
    Public Sub MostrarReporteCambioDeMedida(ByVal inicio As Date, ByVal fin As Date, ByVal maquina As String, ByVal ReporteTiemposImproductivos As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_CambioMedida.rpt"
        If (LoguearReporte(ReporteTiemposImproductivos, nbrereporte)) Then
            Try
                ReporteTiemposImproductivos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@fechainicio", inicio)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@fechaFin", fin)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@Maquina", maquina)
                ReporteTiemposImproductivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    'DG 14-04-2011
    Public Sub MostrarDetalleDeParadasCompleto(ByVal inicio As Date, ByVal fin As Date, ByVal maquina As String, ByVal ReporteTiemposImproductivos As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_Paradas_Completo2_new.rpt"
        If (LoguearReporte(ReporteTiemposImproductivos, nbrereporte)) Then
            Try
                ReporteTiemposImproductivos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@fechainicial", inicio)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@fechafinal", fin)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@maquina", maquina)
                ReporteTiemposImproductivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub

    'DG 14-04-2011
    Public Sub MostrarReporteSeguimientoDeProduccionRoscado(ByVal inicio As Date, ByVal fin As Date, ByVal maquina As String, ByVal ReporteTiemposImproductivos As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_SeguimientoProduccionRoscado.rpt"
        If (LoguearReporte(ReporteTiemposImproductivos, nbrereporte)) Then
            Try
                ReporteTiemposImproductivos.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@fechainicio", inicio)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@fechafin", fin)
                ReporteTiemposImproductivos.rdReportes.SetParameterValue("@torno", maquina)
                ReporteTiemposImproductivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    'DG 18-04-2011
    Public Sub MostrarMaestroLugares(ByVal ReporteMaestroLugares As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_Maestro_Lugares.rpt"
        If (LoguearReporte(ReporteMaestroLugares, nbrereporte)) Then
            Try
                ReporteMaestroLugares.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    'DG 18-04-2011
    Public Sub MostrarMaestroMotivos(ByVal ReporteMaestroMotivos As frmReportes)
        Dim nbrereporte As String = "PARADAS_NET_Maestro_Motivos.rpt"
        If (LoguearReporte(ReporteMaestroMotivos, nbrereporte)) Then
            Try
                ReporteMaestroMotivos.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try
        End If
    End Sub
    Public Sub MostrarReporteScrapFlejesTM3(ByVal inicio As Date, ByVal fin As Date, ByVal ReporteScrpaTM3 As frmReportes)
        Dim nbrereporte As String = "TM3_Scrap_Flejes.rpt"
        If (LoguearReporte(ReporteScrpaTM3, nbrereporte)) Then
            Try
                ReporteScrpaTM3.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteScrpaTM3.rdReportes.SetParameterValue("@fechainicio", inicio)
                ReporteScrpaTM3.rdReportes.SetParameterValue("@fechafin", fin)
                ReporteScrpaTM3.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    Public Sub MostrarReporteScrapCanosTM3(ByVal inicio As Date, ByVal fin As Date, ByVal Tipo As String, ByVal ReporteScrpaTM3 As frmReportes)
        Dim nbrereporte As String = "TM3_Scrap_Canos.rpt"
        If (LoguearReporte(ReporteScrpaTM3, nbrereporte)) Then
            Try
                ReporteScrpaTM3.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
                ReporteScrpaTM3.rdReportes.SetParameterValue("@fechainicio", inicio)
                ReporteScrpaTM3.rdReportes.SetParameterValue("@fechafin", fin)
                ReporteScrpaTM3.rdReportes.SetParameterValue("@tipo", Tipo)
                ReporteScrpaTM3.Show()
            Catch ex As Exception
                ControladorDeExepcionesReportes(ex, nbrereporte)
            Finally
                filtradopor = ""
            End Try

        End If
    End Sub
    End Class
