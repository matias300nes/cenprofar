Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports System.Windows.Forms

Public Class frmReportes
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
        Dim nbrereporte As String = "TM3_ConsumosFlejes.rpt"
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
    '' '' ''Public Sub ObtenerResultadoStoreLlenaReporte(ByVal fechadesde As String, ByVal fechahasta As String, ByVal maquina As String, ByVal reporteimprodTM16 As frmReportes)
    '' '' ''    ''VARIABLE DE PARAMETROS DE CONECCION
    '' '' ''    Dim myConnectionInfo As New ConnectionInfo
    '' '' ''    ''RUTA DEL REPORTE
    '' '' ''    Dim reportFilePath As String = "C:\Mariano\Proyectos\WINREPORT NET\Comun\RPT\paradas.rpt"

    '' '' ''    ''DEFINO LOS PARAMETROS DE CONECCION
    '' '' ''    With myConnectionInfo
    '' '' ''        .ServerName = "SRVT03"
    '' '' ''        ''.ServerName = "DESARROLLO2"
    '' '' ''        .DatabaseName = "PARADAS"
    '' '' ''        .UserID = "sa"
    '' '' ''        .Password = "pelicano"
    '' '' ''        ''.Password = "avestruz"
    '' '' ''    End With

    '' '' ''    Dim crtableLogoninfos As New TableLogOnInfos
    '' '' ''    Dim crtableLogoninfo As New TableLogOnInfo
    '' '' ''    Dim CrTables As Tables
    '' '' ''    Dim CrTable As Table

    '' '' ''    ''CARGA EL REPORTE
    '' '' ''    reporteimprodTM16.rdReportes.Load(reportFilePath)

    '' '' ''    CrTables = reporteimprodTM16.rdReportes.Database.Tables ''formreporte.rd1.Database.Tables 

    '' '' ''    For Each CrTable In CrTables
    '' '' ''        crtableLogoninfo = CrTable.LogOnInfo
    '' '' ''        crtableLogoninfo.ConnectionInfo = myConnectionInfo
    '' '' ''        CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '' '' ''    Next
    '' '' ''    'SETEA EL LOGON INFO PARA CUALQUIER SUBREPORTE DEL REPORTE PRINCIPAL
    '' '' ''    For Each subReportDocument As ReportDocument In reporteimprodTM16.rdReportes.Subreports ''formreporte.rd1.Subreports 
    '' '' ''        For Each subReportTable As CrystalDecisions.CrystalReports.Engine.Table In subReportDocument.Database.Tables
    '' '' ''            Try
    '' '' ''                subReportTable.ApplyLogOnInfo(crtableLogoninfo)
    '' '' ''            Catch ex As Exception
    '' '' ''                '
    '' '' ''            End Try
    '' '' ''        Next
    '' '' ''    Next
    '' '' ''    ''SI NO SE PONE EL VERIFIDATABASE NO FILTRA Y APARECE EL REPORTE EN BLANCO
    '' '' ''    reporteimprodTM16.rdReportes.VerifyDatabase()

    '' '' ''    ''LE ASIGNA EL DATA SOURCE  AL CRYSTAL REPORT VIEWER
    '' '' ''    reporteimprodTM16.crvReportes.ReportSource = reporteimprodTM16.rdReportes

    '' '' ''    ''LA PASA EL PARAMETRO fechadesde, fechahasta y maquina
    '' '' ''    reporteimprodTM16.rdReportes.SetParameterValue("@fechadesde", Convert.ToDateTime(fechadesde))

    '' '' ''    reporteimprodTM16.rdReportes.SetParameterValue("@fechahasta", Convert.ToDateTime(fechahasta))

    '' '' ''    reporteimprodTM16.rdReportes.SetParameterValue("@maquina", maquina)

    '' '' ''    Try
    '' '' ''        reporteimprodTM16.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
    '' '' ''    Catch
    '' '' ''        MessageBox.Show("Falta poner la Formula 'filtradopor' en el Reporte", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '' '' ''        Exit Sub
    '' '' ''    Finally
    '' '' ''        filtradopor = ""
    '' '' ''    End Try
    '' '' ''    Try
    '' '' ''        reporteimprodTM16.MdiParent = WinreportNet.MdiForm
    '' '' ''        reporteimprodTM16.Show()
    '' '' ''    Catch ex As Exception
    '' '' ''        ex.Message().ToString()
    '' '' ''    End Try
    '' '' ''End Sub
    '' '' ''Public Sub StoreAnalisisDeRendimiento(ByVal area As String, ByVal periodo As String, ByVal reporteconparamysubreport As frmReportes)
    '' '' ''    ''LLAMADA ASTORE QUE GENERA LA TABLA (tmpAnalisisRendimientoRPT2) QUE LUEGO ES USADA POR EL REPORTE
    '' '' ''    LlamadaAStoreQueGeneraTablaQueUsaElReporte(area, periodo)
    '' '' ''    Dim myConnectionInfo As New ConnectionInfo
    '' '' ''    ''RUTA DEL REPORTE
    '' '' ''    Dim reportFilePath As String = "C:\Mariano\Proyectos\WINREPORT NET\Comun\RPT\AnalisisRendimientoConSubReporte.rpt"
    '' '' ''    ''DEFINO LOS PARAMETROS DE CONECCION
    '' '' ''    With myConnectionInfo
    '' '' ''        .ServerName = "SRVT03"
    '' '' ''        ''.ServerName = "DESARROLLO2"
    '' '' ''        .DatabaseName = "PARADAS"
    '' '' ''        .UserID = "sa"
    '' '' ''        .Password = "pelicano"
    '' '' ''        ''.Password = "avestruz"
    '' '' ''    End With
    '' '' ''    Dim crtableLogoninfos As New TableLogOnInfos
    '' '' ''    Dim crtableLogoninfo As New TableLogOnInfo
    '' '' ''    Dim CrTables As Tables
    '' '' ''    Dim CrTable As Table

    '' '' ''    ''CARGA EL REPORTE
    '' '' ''    reporteconparamysubreport.rdReportes.Load(reportFilePath)

    '' '' ''    CrTables = reporteconparamysubreport.rdReportes.Database.Tables

    '' '' ''    For Each CrTable In CrTables
    '' '' ''        crtableLogoninfo = CrTable.LogOnInfo
    '' '' ''        crtableLogoninfo.ConnectionInfo = myConnectionInfo
    '' '' ''        CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '' '' ''    Next
    '' '' ''    'SETEA EL LOGON INFO PARA CUALQUIER SUBREPORTE DEL REPORTE PRINCIPAL
    '' '' ''    For Each subReportDocument As ReportDocument In reporteconparamysubreport.rdReportes.Subreports
    '' '' ''        For Each subReportTable As CrystalDecisions.CrystalReports.Engine.Table In subReportDocument.Database.Tables
    '' '' ''            Try
    '' '' ''                subReportTable.ApplyLogOnInfo(crtableLogoninfo)
    '' '' ''            Catch ex As Exception
    '' '' ''                '
    '' '' ''            End Try
    '' '' ''        Next
    '' '' ''    Next
    '' '' ''    ''SI NO SE PONE EL VERIFIDATABASE NO FILTRA Y APARECE EL REPORTE EN BLANCO
    '' '' ''    reporteconparamysubreport.rdReportes.VerifyDatabase()

    '' '' ''    ''VARIABLE QUE SE LE PASA AL REPORTE PARA QUE INFORME AL USUARIO
    '' '' ''    ''POR CUAL/ES CAMPO/S ESTA FILTRADO EL REPORTE
    '' '' ''    Try
    '' '' ''        reporteconparamysubreport.rdReportes.DataDefinition.FormulaFields("filtradopor").Text = filtradopor
    '' '' ''    Catch
    '' '' ''        MessageBox.Show("Falta poner la Formula 'filtradopor' en el Reporte", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '' '' ''        Exit Sub
    '' '' ''    Finally
    '' '' ''        filtradopor = ""
    '' '' ''    End Try
    '' '' ''    ''LE ASIGNA EL DATA SOURCE  AL CRYSTAL REPORT VIEWER
    '' '' ''    reporteconparamysubreport.crvReportes.ReportSource = reporteconparamysubreport.rdReportes

    '' '' ''    reporteconparamysubreport.rdReportes = Nothing
    '' '' ''    Try
    '' '' ''        frmParametros.Dispose()
    '' '' ''        reporteconparamysubreport.MdiParent = WinreportNet.MdiForm
    '' '' ''        reporteconparamysubreport.Show()
    '' '' ''    Catch ex As Exception
    '' '' ''        ex.Message().ToString()
    '' '' ''    End Try
    '' '' ''End Sub
    ' '' ''Public Sub LlamadaAStoreQueGeneraTablaQueUsaElReporte(ByVal area As String, ByVal periodo As String)
    ' '' ''    Try
    ' '' ''        CnnParadas.Open()
    ' '' ''        Dim param As SqlParameter
    ' '' ''        Dim commandcntanterior As SqlCommand = New SqlCommand("spAnalisisRendimiento", CnnParadas)
    ' '' ''        commandcntanterior.CommandType = CommandType.StoredProcedure

    ' '' ''        Dim adapter As SqlDataAdapter = New SqlDataAdapter(commandcntanterior)

    ' '' ''        param = New SqlParameter
    ' '' ''        param.ParameterName = "@Periodo"
    ' '' ''        param.Value = periodo
    ' '' ''        param.Size = 6
    ' '' ''        commandcntanterior.Parameters.Add(param)

    ' '' ''        param = New SqlParameter
    ' '' ''        param.ParameterName = "@Maquina"
    ' '' ''        param.Value = area
    ' '' ''        param.Size = 30
    ' '' ''        commandcntanterior.Parameters.Add(param)

    ' '' ''        commandcntanterior.ExecuteNonQuery()

    ' '' ''    Catch ex As Exception
    ' '' ''        MsgBox("No se pudo conectar a la Base de Datos " + ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Advertencia")
    ' '' ''    Finally
    ' '' ''        ''cierro la conexion                                                
    ' '' ''        CnnParadas.Close()
    ' '' ''    End Try
    ' '' ''End Sub
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
        With myConnectionInfo
            .ServerName = TipoConexionWinreportNet.server
            .DatabaseName = TipoConexionWinreportNet.base
            .UserID = TipoConexionWinreportNet.usuario
            .Password = TipoConexionWinreportNet.contrasena
        End With

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
            reporte.rdReportes.VerifyDatabase()

            ''LE ASIGNA EL DATA SOURCE  AL CRYSTAL REPORT VIEWER
            reporte.crvReportes.ReportSource = reporte.rdReportes
            ''NUEVO MS 02-09-2010
            Utiles.LoguearUsoDelReporte(nbrereporte, UserActual)
            ''FIN NUEVO



            Return True
        Catch ex2 As Exception
            MsgBox(ex2.Message.ToString)
        End Try
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



End Class
