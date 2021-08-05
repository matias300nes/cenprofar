Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports Utiles
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.ApplicationBlocks.Data
Imports System.Net



Public Class MdiForm

    '    Dim WithEvents WinSockCliente As New SocketServer.SocketClient
    '    Private Nombre_Archivo_Excel As String

    '    ' '' '' ''Private Sub ImproductivosTM16ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImproductivosTM16ToolStripMenuItem.Click
    '    ' '' '' ''    Dim param1 As New frmParametros

    '    ' '' '' ''    ''VARIABLES PARA PASARLE LOS PARAMETROS AL STORE PROCEDURE
    '    ' '' '' ''    Dim fechadesde As String
    '    ' '' '' ''    Dim fechahasta As String
    '    ' '' '' ''    Dim maq As String

    '    ' '' '' ''    ''NUEVO MS 02-07-2010
    '    ' '' '' ''    Dim reporteimprodTM16 As New frmReportes
    '    ' '' '' ''    nbreformreportes = "Tiempos Improductivos TM16"
    '    ' '' '' ''    ''FIN NUEVO

    '    ' '' '' ''    ''En esta Variable le paso el primer dia del mes actual
    '    ' '' '' ''    Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ' '' '' ''    ''En esta Variable le paso la fecha actual
    '    ' '' '' ''    Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ' '' '' ''    ''Variable para pasarle la consulta al Combo

    '    ' '' '' ''    Dim consulta As String = "select idlugar from lugares where isnull(idlugarpadre,'')=''"
    '    ' '' '' ''    param1.AgregarParametros("Fecha Desde :", "DATE", "", False, Inicial)
    '    ' '' '' ''    param1.AgregarParametros("Fecha Hasta :", "DATE", "", False, Final)
    '    ' '' '' ''    param1.AgregarParametros("Maquina :", "STRING", "", True, "", consulta, CnnParadas)

    '    ' '' '' ''    ''NUEVO MS 01/07/2010
    '    ' '' '' ''    ''COMENTADO MS 12-07-2010
    '    ' '' '' ''    ''cantparametrosmasboton = 4
    '    ' '' '' ''    ''FIN COMENTADO
    '    ' '' '' ''    ''FIN NUEVO

    '    ' '' '' ''    ''Muestra el Formulario de Filtro En Pantalla
    '    ' '' '' ''    param1.ShowDialog()
    '    ' '' '' ''    ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '    ' '' '' ''    ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '    ' '' '' ''    ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '    ' '' '' ''    If cerroparametrosconaceptar = True Then
    '    ' '' '' ''        ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '    ' '' '' ''        fechadesde = param1.ObtenerParametros(0)

    '    ' '' '' ''        fechahasta = param1.ObtenerParametros(1)

    '    ' '' '' ''        maq = param1.ObtenerParametros(2)

    '    ' '' '' ''        ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '    ' '' '' ''        ''RESULTADO LO MUESTRE EN EL REPORTE
    '    ' '' '' ''        reporteimprodTM16.ObtenerResultadoStoreLlenaReporte(fechadesde, fechahasta, maq, reporteimprodTM16)
    '    ' '' '' ''        cerroparametrosconaceptar = False
    '    ' '' '' ''        param1 = Nothing
    '    ' '' '' ''        ''Else
    '    ' '' '' ''        ''  cantparametrosmasboton = 1
    '    ' '' '' ''    End If
    '    ' '' '' ''End Sub
    '    '' '' ''Private Sub ConParamYSubreporteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConParamYSubreporteToolStripMenuItem.Click
    '    '' '' ''    Dim Periodo As String
    '    '' '' ''    Dim sql As String
    '    '' '' ''    Dim area As String
    '    '' '' ''    Dim period As String

    '    '' '' ''    ''NUEVO MS 02-07-2010
    '    '' '' ''    Dim reporteconparamysubreport As New frmReportes
    '    '' '' ''    nbreformreportes = "Tiempos Improductivos TM16"
    '    '' '' ''    ''FIN NUEVO

    '    '' '' ''    Dim param As New frmParametros
    '    '' '' ''    Periodo = Mid(Now, 4, 2) & Mid(Now, 7, 4)
    '    '' '' ''    sql = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS MAQUINA from lugares where ((isnull(idlugarpadre,'')=''  AND IDLugar <> '10600') ) OR idlugar in ('10622','10623')"

    '    '' '' ''    param.AgregarParametros("Area :", "STRING", "", False, "", sql, CnnParadas)
    '    '' '' ''    param.AgregarParametros("Periodo :", "INTEGER", "", True, Periodo.ToString)

    '    '' '' ''    ''NUEVO MS 01/07/2010
    '    '' '' ''    ''COMENTADO MS 12-07-2010
    '    '' '' ''    ''cantparametrosmasboton = 3
    '    '' '' ''    ''FIN COMENTADO
    '    '' '' ''    ''FIN NUEVO

    '    '' '' ''    param.ShowDialog()
    '    '' '' ''    ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '    '' '' ''    ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '    '' '' ''    ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '    '' '' ''    If cerroparametrosconaceptar = True Then
    '    '' '' ''        ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '    '' '' ''        area = param.ObtenerParametros(0)

    '    '' '' ''        period = param.ObtenerParametros(1)
    '    '' '' ''        ''LLAMADA A PROCEDIMIENTO QUE LLAMA A STORE QUE GENERA LA TABLA (tmpAnalisisRendimientoRPT2) QUE LUEGO ES USADA POR EL REPORTE
    '    '' '' ''        reporteconparamysubreport.StoreAnalisisDeRendimiento(area, period, reporteconparamysubreport)
    '    '' '' ''        cerroparametrosconaceptar = False
    '    '' '' ''        param = Nothing
    '    '' '' ''    End If
    '    '' '' ''End Sub

    '    Private Sub MdiForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '        'dg 02/09/2011
    '        Dim res As Integer = 0, i As Integer, ip_text As String
    '        ip_text = ""
    '        Dim mensaje As String = ""
    '        Dim ip As System.Net.IPHostEntry
    '        ip = Dns.GetHostEntry(My.Computer.Name)
    '        For i = 0 To UBound(ip.AddressList)
    '            If Mid(ip.AddressList(i).ToString, 1, 3) = "10." Then
    '                ip_text = ip.AddressList(i).ToString
    '                Exit For
    '            End If
    '        Next

    '        res = Util.EliminarSistemaUsuarioActual(EquipoActual, My.Application.Info.AssemblyName.ToUpper, UserActual, ip_text, mensaje)
    '        'If res = -2 Then 'si no existe es porque lo eliminaron a la fuerza
    '        'MsgBox(mensaje, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Atención")
    '        'End If

    '        WinSockCliente.Cerrar()
    '    End Sub

    '    Private Sub MdiForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        '' '' ''path = Application.ExecutablePath.ToString & "\..\..\..\"
    '        '' '' ''Dim d As New System.IO.DirectoryInfo(path)
    '        '' '' ''ObtenerNombrePC()
    '        '' '' ''path = d.FullName.ToString & "INIs\" & UCase(sComputerName) & ".ini"
    '        '' '' '' ''pathini = pathinis & "INIs\" & UCase(sComputerName) & ".ini"
    '        '' '' '' ''AHORA ''EJ: "C:\Mariano\Proyectos\TM3\INIs\DESARROLLO2.ini

    '        '' '' ''Try
    '        '' '' ''    Call FileLen(path)

    '        '' '' ''Catch
    '        '' '' ''    Try
    '        '' '' ''        path = Application.ExecutablePath.ToString & "\..\..\..\..\"
    '        '' '' ''        Dim dd As New System.IO.DirectoryInfo(path)
    '        '' '' ''        path = d.FullName.ToString & "INIs\default.ini"
    '        '' '' ''        '''pathini = pathinis & "INIs\" & UCase(sComputerName) & ".ini"

    '        '' '' ''        Call FileLen(path)
    '        '' '' ''    Catch ex As Exception
    '        '' '' ''        MsgBox("No existe el archivo " & path & vbCrLf & "para conectar con la Base de datos. Por favor comuniquese con el Dpto. de Sistemas", vbCritical + vbOKOnly, "Error de conexión")
    '        '' '' ''    End Try
    '        '' '' ''End Try
    '        ''''''COMENTADO MS 05-07-2010
    '        '' '' '' '' ''path = Application.ExecutablePath.ToString & "\..\"

    '        '' '' '' '' ''Dim d As New System.IO.DirectoryInfo(path)

    '        '' '' '' '' ''If (d.FullName.ToString = "L:\") Then
    '        '' '' '' '' ''    ''Ruta del INI de la PC que esta utilizando el Sistema
    '        '' '' '' '' ''    ''d.FullName.ToString
    '        '' '' '' '' ''    path = d.FullName.ToString & "INIs\" & UCase(SystemInformation.ComputerName.ToString) & ".ini"
    '        '' '' '' '' ''    MsgBox(path)
    '        '' '' '' '' ''Else
    '        '' '' '' '' ''    ''path para C:\Mariano\Proyectos\TM3\Fuentes\WindowsApplication1\WindowsApplication1\bin\Debug\ArchivoEjecutable.exe
    '        '' '' '' '' ''    path = Application.ExecutablePath.ToString & "\..\..\..\"

    '        '' '' '' '' ''    '''''''path = Application.ExecutablePath.ToString & "\..\..\..\..\"
    '        '' '' '' '' ''    Dim d1 As New System.IO.DirectoryInfo(path)
    '        '' '' '' '' ''    path = d1.FullName.ToString & "INIs\" & UCase(SystemInformation.ComputerName.ToString) & ".ini"

    '        '' '' '' '' ''    '''''''''''''''''''''''''MsgBox(path)
    '        '' '' '' '' ''End If

    '        '''''''''''''''''Archivo = path
    '        path = Application.ExecutablePath.ToString & "\..\"

    '        Dim d As New System.IO.DirectoryInfo(path)

    '        Dim codigo As String, nombre As String, pass_actual As String
    '        codigo = "" : nombre = "" : pass_actual = ""

    '        If (d.FullName.ToString = "L:\") Then
    '            ''Ruta del INI de la PC que esta utilizando el Sistema
    '            path = d.FullName.ToString & "INIs\" & UCase(SystemInformation.ComputerName.ToString) & ".ini"
    '            '''''''''''''''''''''''''MsgBox(path)
    '        Else
    '            ''path para C:\Mariano\Proyectos\TM3\Fuentes\WindowsApplication1\WindowsApplication1\bin\Debug\ArchivoEjecutable.exe
    '            path = Application.ExecutablePath.ToString & "\..\..\..\"

    '            '''''''path = Application.ExecutablePath.ToString & "\..\..\..\..\"
    '            Dim d1 As New System.IO.DirectoryInfo(path)
    '            path = d1.FullName.ToString & "INIs\" & UCase(SystemInformation.ComputerName.ToString) & ".ini"

    '            '''''''''''''''''''''''''MsgBox(path)
    '        End If

    '        Archivo = path
    '        '' CompletarAutomaticamente()



    '        '' '' '' '' ''Archivo = path
    '        '' '' '' '' ''Inicia_Conexion()
    '        ''FIN COMENTADO
    '        Dim log As New Utiles.Login
    '        log.ShowDialog()
    '        ''ReportesNet.GlobalComun.ConnStringUSUARIOS = Recursos_Humanos.GlobalComun.ConnStringUSUARIOS
    '        If Not Logueado_OK Then
    '            End
    '        End If
    '        log = Nothing
    '        '''''ToolStripStatusLabel.Text = GetUserID()


    '        If Util.ObtenerDatosDelUserId(UserID, codigo, nombre, pass_actual) > 0 Then
    '            ' ToolStripStatusLabel.Text = "Equipo: " & GetUserID() & " - Usuario: " & codigo & " " & nombre
    '            ' Else
    '            ' ToolStripStatusLabel.Text = GetUserID()
    '        End If



    '        'dg 02/09/2011
    '        If CERRAR_SESIONES = False Then
    '            End
    '        End If

    '        'buscar la ip del socket...
    '        Dim ipsocket As String = ""
    '        Dim res As Integer = BuscarIpSocket(ipsocket)
    '        If res <> 1 Then
    '            MsgBox("No se pudo determinar la Dirección Ip del Socket.", MsgBoxStyle.Critical, "Ateción")
    '        End If

    '        With WinSockCliente
    '            'Determino a donde se quiere conectar el usuario
    '            '.IPDelHost = "10.10.106.114"
    '            .IPDelHost = ipsocket
    '            .PuertoDelHost = "8050"
    '            'Me conecto
    '            .Conectar()
    '        End With

    '        'le aviso al servidor que me conecto..
    '        'WinSockCliente.EnviarDatos("*02" & codigo & "|") 'campo codigo de usuarios
    '        Dim EquipoActual_2 As String = SystemInformation.ComputerName
    '        'WinSockCliente.EnviarDatos("*04" & EquipoActual_2 & "|") 'equipo
    '        'If CERRAR_IP <> "" Then
    '        'le aviso al servidor que equipo debe avisar y cerrar..
    '        Dim ip As System.Net.IPHostEntry, ip_text As String = "", i As Integer
    '        ip = Dns.GetHostEntry(My.Computer.Name)
    '        'WinSockCliente.EnviarDatos("*01" & CERRAR_IP.ToString & "|" & EquipoActual_2 & "||" )
    '        For i = 0 To UBound(ip.AddressList)
    '            If Mid(ip.AddressList(i).ToString, 1, 3) = "10." Then
    '                ip_text = ip.AddressList(i).ToString
    '                Exit For
    '            End If
    '        Next
    '        WinSockCliente.EnviarDatos("*01" & ip_text & "|" & EquipoActual_2 & "||" & My.Application.Info.AssemblyName.ToUpper & "|||" & codigo & "||||")
    '        'End If


    '    End Sub

    '    Private Sub WinSockCliente_DatosRecibidos(ByVal datos As String) Handles WinSockCliente.DatosRecibidos
    '        Dim cadena_mensaje As String = ""
    '        Dim tipomensaje As String = ""
    '        Dim equipo As String = ""
    '        Dim pos As Integer, pos2 As Integer

    '        cadena_mensaje = datos
    '        tipomensaje = Mid(cadena_mensaje, 1, 2)
    '        pos = InStr(cadena_mensaje, "|", CompareMethod.Text)
    '        pos2 = InStr(cadena_mensaje, "||", CompareMethod.Text)

    '        Try
    '            equipo = Mid(cadena_mensaje, pos + 1, pos2 - (pos + 1))
    '        Catch ex As Exception

    '        End Try

    '        Select Case tipomensaje
    '            Case "01" 'cerrar aplicacion..
    '                Timer1.Enabled = True
    '                MsgBox("La Aplicación se cerrará ,ya que otro usuario en el equipo: " & equipo & " ingresó con su clave.", MsgBoxStyle.Information, "Atención")
    '                End
    '            Case "03" 'mensaje para publicar
    '                MsgBox(Mid(cadena_mensaje, 3, Len(cadena_mensaje) - 2), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Mensaje")
    '            Case "05"
    '                Timer1.Enabled = True
    '                MsgBox("La Aplicación se cerrará por instrucción de Sistemas. Intente abrir nuevamente en 10 minutos.", MsgBoxStyle.Information, "Atención")
    '                End
    '            Case Else
    '                MsgBox("mensaje:" & cadena_mensaje, MsgBoxStyle.Critical, "atencion")
    '                End

    '        End Select
    '    End Sub

    '    Private Sub WinSockCliente_ConexionTerminada() Handles WinSockCliente.ConexionTerminada
    '        'MsgBox("Finalizo la conexion con el Servidor")
    '    End Sub
    '    Private Sub KanbanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KanbanToolStripMenuItem.Click
    '        frmKanban.MdiParent = Me
    '        frmKanban.Show()
    '    End Sub

    '    ''cp 16-05-2011
    '    Private Sub AnalisisDeRendimientoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalisisDeRendimientoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim maquina As String
    '        nbreformreportes = "Análisis de Rendimiento"
    '        'Dim automatico As Boolean

    '        Dim periodo As String = Mid$(Now, 4, 2) & Mid$(Now, 7, 4)
    '        ''Dim consulta As String = "select nombrelugarpadre from lugares where eliminado=0 group by nombrelugarpadre order by  nombrelugarpadre"
    '        ''Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"
    '        paramreporte.AgregarParametros("Período: ", "STRING", "", True, periodo, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)
    '        'paramreporte.AgregarParametros("Automatico :", "BOOLEAN", "", True, "", consulta2, CnnParadas)
    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            periodo = paramreporte.ObtenerParametros(0).ToString
    '            maquina = paramreporte.ObtenerParametros(1).ToString
    '            'automatico = paramreporte.ObtenerParametros(2)
    '            ''LLAMADA AL STORE
    '            LlenarTmp(periodo, maquina)
    '            reporte.MostrarReporteAnalisisDeRendimiento(periodo, maquina, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Function LlenarTmp(ByVal periodo As String, ByVal maquina As String) As Integer

    '        Dim connection As SqlClient.SqlConnection = Nothing

    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPARADAS)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End Try

    '            Try
    '                Dim param_periodo As New SqlClient.SqlParameter
    '                param_periodo.ParameterName = "@periodo"
    '                param_periodo.SqlDbType = SqlDbType.VarChar
    '                param_periodo.Size = 6
    '                param_periodo.Value = periodo
    '                param_periodo.Direction = ParameterDirection.Input

    '                Dim param_maquina As New SqlClient.SqlParameter
    '                param_maquina.ParameterName = "@maquina"
    '                param_maquina.SqlDbType = SqlDbType.VarChar
    '                param_maquina.Size = 30
    '                param_maquina.Value = maquina
    '                param_maquina.Direction = ParameterDirection.Input

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spAnalisisRendimiento", param_periodo, param_maquina)

    '                Catch ex As Exception
    '                    '' 
    '                    Throw ex
    '                End Try
    '            Finally
    '                ''
    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try
    '    End Function

    '    ''dg 13-04-2011
    '    Private Sub SeguimientoDeProducciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeguimientoDeProducciónToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim maquina As String

    '        nbreformreportes = "Seguimiento de Producción"

    '        ''Dim consulta As String = "select nombrelugarpadre from lugares where eliminado=0 group by nombrelugarpadre order by  nombrelugarpadre"
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0"

    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            maquina = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarReporteSeguimientoDeProduccion(maquina, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''dg 13-04-2011
    '    ''CP 27-07-2011 split de slitter en estructurales y semielab
    '    Private Sub TiemposImproductivosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TiemposImproductivosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros, paramreporte2 As New frmParametros
    '        Dim reporte As New frmReportes
    '        'nuevo cp 27-5-2011
    '        Dim reporte_setup1 As New frmReportes
    '        Dim reporte_setup2 As New frmReportes
    '        Dim reporte_setup3 As New frmReportes
    '        'fin nuevo cp 27-5-2011
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim op As String, maquina As String, linea As String, diseno As String


    '        nbreformreportes = "Tiempos Improductivos"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"

    '        Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"

    '        Dim consulta4 As String = " SELECT iddiseno FROM rn.dbo.disenos where disenoprueba = 0 and eliminado = 0 and activo = 1 " + _
    '                                    " union ALL SELECT CODIGO AS IDDISENO FROM TM3..DISENOS WHERE eliminado = 0 and activo = 1 "

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", True, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)
    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnParadas)
    '        paramreporte.AgregarParametros("Diseño :", "STRING", "", False, "", consulta4, CnnParadas)

    '        paramreporte.ShowDialog()
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            maquina = paramreporte.ObtenerParametros(2).ToString
    '            op = paramreporte.ObtenerParametros(3).ToString
    '            diseno = paramreporte.ObtenerParametros(4).ToString
    '            '
    '            'Agregado CP 27-7-2011 a pedido de Eduardo Suarez
    '            'Separar el reporte en 2, slitter estructural y el resto tm6/16
    '            '
    '            If maquina = "SLITTER" Then
    '                Dim consulta3 As String = "select 'Estructural' as Linea union all select 'Semielaborado' as Linea"
    '                'Utiles.filtradopor = Mid(Utiles.filtradopor, 1, Len(Utiles.filtradopor) - 1)
    '                paramreporte2.AgregarParametros("Línea :", "STRING", "", True, "Semielaborado", consulta3, CnnParadas)
    '                paramreporte2.ShowDialog()
    '                linea = paramreporte2.ObtenerParametros(0).ToString
    '                'Utiles.filtradopor = Utiles.filtradopor & "'"
    '            Else
    '                linea = ""
    '            End If

    '            Cursor = System.Windows.Forms.Cursors.WaitCursor
    '            'reporte.MdiParent = Me
    '            reporte.MostrarReporteTiempoImproductivo(Inicial, Final, maquina, op, linea, diseno, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '            ''----------------------------------------------------------------------------------------------
    '            '' Agregado CP 26-5-2011
    '            '' Junto al reporte de Tiempos Improductvos sacar los de SETUP
    '            '
    '            ' Lamentablemente no anda...el frmReportes no abre instancias nuevas...
    '            '
    '            'If maquina = "TM3" Or maquina = "TM6" Or maquina = "TM16" Then
    '            '    nbreformreportes = "Tiempos Improductivos Setup Motivo=12"
    '            '    reporte_setup1.MostrarReporteTiempoImproductivoSetup(Inicial, Final, maquina, op, "12", reporte)
    '            '    '----
    '            '    nbreformreportes = "Tiempos Improductivos Setup Motivo=14"
    '            '    reporte_setup2.MostrarReporteTiempoImproductivoSetup(Inicial, Final, maquina, op, "14", reporte)
    '            '    '----
    '            '    nbreformreportes = "Tiempos Improductivos Setup Motivo=15"
    '            '    reporte_setup3.MostrarReporteTiempoImproductivoSetup(Inicial, Final, maquina, op, "17", reporte)
    '            'End If
    '            '----------------------------------------------------------------------------------------------
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''NUEVO DG 14-04-2011
    '    Private Sub AnálisisDeRendimientoPorLíneaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnálisisDeRendimientoPorLíneaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim automatico As Boolean
    '        nbreformreportes = "Análisis de Rendimiento por Línea"
    '        Dim consulta As String = "select 'SI' as si union all select 'NO' as si"
    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Automatico :", "BOOLEAN", "", False, "", consulta, CnnParadas)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            automatico = paramreporte.ObtenerParametros(2)
    '            reporte.MostrarReporteAnalisisDeRendimientoPorLínea(Inicial, Final, automatico, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''NUEVO DG 14-04-2011
    '    Private Sub CambioDeMedidasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambioDeMedidasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim maquina As String

    '        nbreformreportes = "Cambio de Medidas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim consulta As String = "select nombrelugarpadre from lugares where eliminado=0 group by nombrelugarpadre order by  nombrelugarpadre"
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"


    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            maquina = paramreporte.ObtenerParametros(2).ToString
    '            reporte.MostrarReporteCambioDeMedida(Inicial, Final, maquina, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''NUEVO DG 14-04-2011

    '    Private Sub DetalleDeParadasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetalleDeParadasToolStripMenuItem.Click
    '        '' '' ''Dim paramreporte As New frmParametros
    '        '' '' ''Dim reporte As New frmReportes
    '        '' '' ''Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        '' '' ''Dim maquina As String

    '        '' '' ''nbreformreportes = "Detalle de Paradas"

    '        '' '' '' ''En esta Variable le paso la fecha actual
    '        '' '' ''Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        '' '' ''Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        '' '' '' ''Dim consulta As String = "select nombrelugarpadre from lugares where eliminado=0 group by nombrelugarpadre order by  nombrelugarpadre"
    '        '' '' ''Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"


    '        '' '' ''paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnParadas)
    '        '' '' ''paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnParadas)
    '        '' '' ''paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)

    '        '' '' ''paramreporte.ShowDialog()
    '        '' '' ''Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        '' '' ''If cerroparametrosconaceptar = True Then
    '        '' '' ''    Inicial = paramreporte.ObtenerParametros(0).ToString
    '        '' '' ''    Final = paramreporte.ObtenerParametros(1).ToString
    '        '' '' ''    maquina = paramreporte.ObtenerParametros(2).ToString
    '        '' '' ''    reporte.MostrarDetalleDeParadasCompleto(Inicial, Final, maquina, reporte)
    '        '' '' ''    cerroparametrosconaceptar = False
    '        '' '' ''    paramreporte = Nothing
    '        '' '' ''End If
    '        '' '' ''Cursor = System.Windows.Forms.Cursors.Default
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim maquina As String

    '        nbreformreportes = "Detalle de Paradas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim consulta As String = "select nombrelugarpadre from lugares where eliminado=0 group by nombrelugarpadre order by  nombrelugarpadre"
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"


    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            maquina = paramreporte.ObtenerParametros(2).ToString


    '            'nuevo dg 22-07-2011
    '            Select Case maquina
    '                Case "DEVANADORA1"
    '                    maquina = "TMCDEV1"
    '                Case "DEVANADORA2"
    '                    maquina = "TMCDEV2"
    '                Case "DEVANADORA3"
    '                    maquina = "TMCDEV3"
    '                Case "DEVANADORA4"
    '                    maquina = "TMCDEV4"
    '            End Select
    '            'fin nuevo

    '            reporte.MostrarDetalleDeParadasCompleto(Inicial, Final, maquina, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''NUEVO DG 14-04-2011
    '    Private Sub SeguimientoDeProducciónDeRoscadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeguimientoDeProducciónDeRoscadoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim maquina As String

    '        nbreformreportes = "Seguimiento de Producción de Roscado"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        'Dim consulta As String = "select nombre from lugares where nombrelugarpadre='roscado' and eliminado=0 and idpadre=0 order by nombre"
    '        Dim consulta As String = "select '(' + codigo + ') ' +  nombre from lugares where nombrelugarpadre='roscado' and eliminado=0 and idpadre=0 order by nombre"


    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            maquina = paramreporte.ObtenerParametros(2).ToString
    '            maquina = Mid(maquina, 2, 5)
    '            reporte.MostrarReporteSeguimientoDeProduccionRoscado(Inicial, Final, maquina, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''COMENTADO MS 01-11-2011 al PONER PAÑOL NUEVO
    '    ''Private Sub ControlDeCorrelatividad_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlDeCorrelatividad_Item.Click
    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    ''    nbreformreportes = "Control de Correlatividad Pañol"
    '    ''    Dim rpt As New frmReportes
    '    ''    rpt.MostrarControlDeCorrelatividad(rpt)
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub

    '    Private Sub ListadoDePersonal_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListadoDePersonal_Item.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Listado de Personal"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarListadodePersonal(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''COMENTADO MS 01-11-2011 al PONER PAÑOL NUEVO
    '    ''Private Sub MaterialesDeBajaRotacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialesDeBajaRotacionToolStripMenuItem.Click

    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ''    Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '    ''    paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnTM3)


    '    ''    paramreporte.ShowDialog()
    '    ''    nbreformreportes = "Materiales de baja rotación"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        Inicial = paramreporte.ObtenerParametros(0).ToString
    '    ''        Final = paramreporte.ObtenerParametros(1).ToString

    '    ''        rpt.MostrarReporteBajaRotacion(Inicial, Final, rpt)
    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO

    '    'cp 19-05-2011
    '    Private Sub ConsumosPorRubroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        ''COMENTADO MS 01-11-2011
    '        ''Dim paramreporte As New frmParametros
    '        ''Dim rpt As New frmReportes
    '        ''Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        '' ''En esta Variable le paso la fecha actual
    '        ''Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim Familia As String, CodMaterial As String, retiro As String
    '        ''paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '        ''paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnTM3)
    '        ''paramreporte.AgregarParametros("Familia :", "STRING", "", False, "", "", CnnTM3)
    '        ''paramreporte.AgregarParametros("Cód.Material :", "STRING", "", False, "", "", CnnTM3)
    '        ''paramreporte.AgregarParametros("Retirado Por :", "STRING", "", False, "", "", CnnTM3)

    '        ''paramreporte.ShowDialog()

    '        ''nbreformreportes = "Consumos por rubros"

    '        ''Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        ''If cerroparametrosconaceptar = True Then
    '        ''    Inicial = paramreporte.ObtenerParametros(0).ToString
    '        ''    Final = paramreporte.ObtenerParametros(1).ToString
    '        ''    Familia = paramreporte.ObtenerParametros(2).ToString
    '        ''    CodMaterial = paramreporte.ObtenerParametros(3).ToString
    '        ''    retiro = paramreporte.ObtenerParametros(4).ToString

    '        ''    rpt.MostrarReporteConsumosPorRubro(Inicial, Final, Familia, CodMaterial, retiro, rpt)

    '        ''    cerroparametrosconaceptar = False
    '        ''    paramreporte = Nothing
    '        ''End If
    '        ''Cursor = System.Windows.Forms.Cursors.Default
    '        ''FIN COMENTADO
    '        ''NUEVO MS 01-11-2011


    '        ''FIN NUEVO
    '    End Sub
    '    ''COMENTADO MS 01-11-2011
    '    'cp 19-05-2011
    '    ''Private Sub ConsumosPorCentroDeCostoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosPorCentroDeCostoToolStripMenuItem.Click

    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ''    Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ''    Dim Imputacion As String, Cc As String, CodMaterial As String, retiro As String
    '    ''    paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Centro de Costo :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Imputación:", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Cód.Material :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Retirado Por :", "STRING", "", False, "", "", CnnTM3)

    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Consumos por centro de costo"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        Inicial = paramreporte.ObtenerParametros(0).ToString
    '    ''        Final = paramreporte.ObtenerParametros(1).ToString
    '    ''        Cc = paramreporte.ObtenerParametros(2).ToString
    '    ''        Imputacion = paramreporte.ObtenerParametros(3).ToString
    '    ''        CodMaterial = paramreporte.ObtenerParametros(4).ToString
    '    ''        retiro = paramreporte.ObtenerParametros(5).ToString

    '    ''        rpt.MostrarReporteConsumosPorCC(Inicial, Final, Cc, Imputacion, CodMaterial, retiro, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''fin nuevo

    '    ''COMENTADO MS 01-11-2011
    '    'cp 19-5-2011
    '    ''Private Sub ConsumosPorImputacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosPorImputacionToolStripMenuItem.Click
    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ''    Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ''    Dim Imputacion As String, Cc As String, CodMaterial As String, retiro As String
    '    ''    paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Centro de Costo :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Imputación:", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Cód.Material :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Retirado Por :", "STRING", "", False, "", "", CnnTM3)

    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Consumos por imputacion"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        Inicial = paramreporte.ObtenerParametros(0).ToString
    '    ''        Final = paramreporte.ObtenerParametros(1).ToString
    '    ''        Cc = paramreporte.ObtenerParametros(2).ToString
    '    ''        Imputacion = paramreporte.ObtenerParametros(3).ToString
    '    ''        CodMaterial = paramreporte.ObtenerParametros(4).ToString
    '    ''        retiro = paramreporte.ObtenerParametros(5).ToString

    '    ''        rpt.MostrarReporteConsumosPorImputacion(Inicial, Final, Cc, Imputacion, CodMaterial, retiro, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO
    '    ''COMENTADO MS 01-11-2011
    '    'CP 19-5-2011
    '    ' ''Private Sub ConsumosDetalladosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosDetalladosToolStripMenuItem.Click
    '    ' ''    Dim paramreporte As New frmParametros
    '    ' ''    Dim rpt As New frmReportes
    '    ' ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ' ''    ''En esta Variable le paso la fecha actual
    '    ' ''    Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ' ''    Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    ' ''    Dim Imputacion As String, Cc As String, CodMaterial As String, retiro As String

    '    ' ''    paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '    ' ''    paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnTM3)
    '    ' ''    paramreporte.AgregarParametros("Centro de Costo :", "STRING", "", False, "", "", CnnTM3)
    '    ' ''    paramreporte.AgregarParametros("Imputación:", "STRING", "", False, "", "", CnnTM3)
    '    ' ''    paramreporte.AgregarParametros("Cód.Material :", "STRING", "", False, "", "", CnnTM3)
    '    ' ''    paramreporte.AgregarParametros("Retirado Por :", "STRING", "", False, "", "", CnnTM3)

    '    ' ''    paramreporte.ShowDialog()

    '    ' ''    nbreformreportes = "Consumos detallados"

    '    ' ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ' ''    If cerroparametrosconaceptar = True Then
    '    ' ''        Inicial = paramreporte.ObtenerParametros(0).ToString
    '    ' ''        Final = paramreporte.ObtenerParametros(1).ToString
    '    ' ''        Cc = paramreporte.ObtenerParametros(2).ToString
    '    ' ''        Imputacion = paramreporte.ObtenerParametros(3).ToString
    '    ' ''        CodMaterial = paramreporte.ObtenerParametros(4).ToString
    '    ' ''        retiro = paramreporte.ObtenerParametros(5).ToString

    '    ' ''        rpt.MostrarReporteConsumosDetallado(Inicial, Final, Cc, Imputacion, CodMaterial, retiro, rpt)

    '    ' ''        cerroparametrosconaceptar = False
    '    ' ''        paramreporte = Nothing
    '    ' ''    End If
    '    ' ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ' ''End Sub
    '    ''FIN COMENTADO
    '    ''NUEVO MS 01-11-2011
    '    Private Sub ConsumosDetalladosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosDetalladosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanol)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim IdMaterial As String, CodUserRetira As String, CodMaterial As String, cc As String

    '        paramreporte.AgregarParametros("Cód.Material :", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("IdMaterial:", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("CodUserRetira:", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnPanol)
    '        paramreporte.AgregarParametros("Cod CC:", "STRING", "", False, "", "", CnnPanol)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos detallados"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            CodMaterial = paramreporte.ObtenerParametros(0).ToString
    '            IdMaterial = paramreporte.ObtenerParametros(1).ToString
    '            CodUserRetira = paramreporte.ObtenerParametros(2).ToString
    '            Inicial = paramreporte.ObtenerParametros(3).ToString
    '            Final = paramreporte.ObtenerParametros(4).ToString
    '            cc = paramreporte.ObtenerParametros(5).ToString

    '            rpt.MostrarMaestroConsumos(CodMaterial, IdMaterial, CodUserRetira, Inicial, Final, cc, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default

    '    End Sub


    '    ''COMENTADO PAÑOL VIEJO
    '    ''Private Sub MaterialesSinStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialesSinStockToolStripMenuItem.Click
    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    ''    nbreformreportes = "Materiales sin Stock"
    '    ''    Dim rpt As New frmReportes
    '    ''    rpt.MostrarMaterialesSinStock(rpt)
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub

    '    ''COMENTADO PAÑOL VIEJO
    '    'cp 20.05.2011
    '    ''Private Sub PlanillaDeInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlanillaDeInventarioToolStripMenuItem.Click
    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim Rubro As String, Ubicacion As String

    '    ''    paramreporte.AgregarParametros("Rubro:", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Ubicacion :", "STRING", "", False, "", "", CnnTM3)

    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Planilla de Inventarios"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        Rubro = paramreporte.ObtenerParametros(0).ToString
    '    ''        Ubicacion = paramreporte.ObtenerParametros(1).ToString
    '    ''        rpt.MostrarReportePlanillaInventario(Rubro, Ubicacion, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO

    '    ''COMENTADO PAÑOL VIEJO
    '    'cp 20-5-2011
    '    ''Private Sub CentrosDeCostosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CentrosDeCostosToolStripMenuItem.Click
    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim Imputacion As String, Cc As String

    '    ''    paramreporte.AgregarParametros("Centro de Costo :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("Imputación:", "STRING", "", False, "", "", CnnTM3)

    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Consumos detallados"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        Cc = paramreporte.ObtenerParametros(0).ToString
    '    ''        Imputacion = paramreporte.ObtenerParametros(1).ToString

    '    ''        rpt.MostrarReporteCentrosDeCosto(Cc, Imputacion, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO

    '    ''COMENTADO PAÑOL VIEJO
    '    'cp 20-05-2011
    '    ''Private Sub EtiquetasMaterialesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetasMaterialesToolStripMenuItem.Click
    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim rubro As String, codmaterial As String, ubicacion As String

    '    ''    paramreporte.AgregarParametros("rubro :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '    ''    paramreporte.AgregarParametros("ubicacion :", "STRING", "", False, "", "", CnnTM3)

    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Etiquetas de Materiales"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        rubro = paramreporte.ObtenerParametros(0).ToString
    '    ''        codmaterial = paramreporte.ObtenerParametros(1).ToString
    '    ''        ubicacion = paramreporte.ObtenerParametros(2).ToString

    '    ''        rpt.MostrarReporteEtiquetasMAteriales(rubro, codmaterial, ubicacion, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO

    '    ''COMENTADO PAÑOL VIEJO
    '    'cp 20-05-2011
    '    ''Private Sub EtiquetasManoDeObraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetasManoDeObraToolStripMenuItem.Click
    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim legajo As String

    '    ''    paramreporte.AgregarParametros("legajo :", "STRING", "", False, "", "", CnnTM3)


    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Etiquetas de Mano de Obra"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        legajo = paramreporte.ObtenerParametros(0).ToString

    '    ''        rpt.MostrarReporteEtiquetasManoObra(legajo, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO

    '    ''COMENTADO PAÑOL VIEJO
    '    'cp 20-5-2011
    '    ''Private Sub EtiquetasCentrosDeCostoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetasCentrosDeCostoToolStripMenuItem.Click
    '    ''    Dim paramreporte As New frmParametros
    '    ''    Dim rpt As New frmReportes
    '    ''    Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '    ''    ''En esta Variable le paso la fecha actual
    '    ''    Dim codigo As String

    '    ''    paramreporte.AgregarParametros("Cod CC :", "STRING", "", False, "", "", CnnTM3)


    '    ''    paramreporte.ShowDialog()

    '    ''    nbreformreportes = "Etiquetas de Centros de Costos"

    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    ''    If cerroparametrosconaceptar = True Then
    '    ''        codigo = paramreporte.ObtenerParametros(0).ToString

    '    ''        rpt.MostrarReporteEtiquetasCentroCosto(codigo, rpt)

    '    ''        cerroparametrosconaceptar = False
    '    ''        paramreporte = Nothing
    '    ''    End If
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub
    '    ''FIN COMENTADO


    '    ''COMENTADO PAÑOL VIEJO
    '    ''Private Sub ImpresorDeValesAutomáticoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImpresorDeValesAutomáticoToolStripMenuItem.Click
    '    ''    frmImpresorVales.Show()
    '    ''End Sub
    '    ''FIN COMENTADO

    '    'cp 20-5-2011
    '    Private Sub StockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim rubro As String, codmaterial As String, lote As String, pallet As String, ubicacion As String

    '        paramreporte.AgregarParametros("rubro :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Lote Prov. :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Pallet Nro. :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("ubicacion :", "STRING", "", False, "", "", CnnTM3)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Stock Polene"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            rubro = paramreporte.ObtenerParametros(0).ToString
    '            codmaterial = paramreporte.ObtenerParametros(1).ToString
    '            lote = paramreporte.ObtenerParametros(2).ToString
    '            pallet = paramreporte.ObtenerParametros(3).ToString
    '            ubicacion = paramreporte.ObtenerParametros(4).ToString

    '            rpt.MostrarReporteStockPolene(rubro, codmaterial, lote, pallet, ubicacion, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    'cp 20-5-2011
    '    Private Sub StockMaterialesTotalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockMaterialesTotalToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim rubro As String, codmaterial As String, lote As String, pallet As String, ubicacion As String

    '        paramreporte.AgregarParametros("rubro :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Lote Prov. :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Pallet Nro. :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("ubicacion :", "STRING", "", False, "", "", CnnTM3)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Stock Polene"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            rubro = paramreporte.ObtenerParametros(0).ToString
    '            codmaterial = paramreporte.ObtenerParametros(1).ToString
    '            lote = paramreporte.ObtenerParametros(2).ToString
    '            pallet = paramreporte.ObtenerParametros(3).ToString
    '            ubicacion = paramreporte.ObtenerParametros(4).ToString

    '            rpt.MostrarReporteStockPoleneDetallado(rubro, codmaterial, lote, pallet, ubicacion, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ConsumosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosToolStripMenuItem.Click
    '        'Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        'nbreformreportes = "Consumos Polene"
    '        'Dim rpt As New frmReportes
    '        'rpt.MostrarConsumosPolene(rpt)
    '        'Cursor = System.Windows.Forms.Cursors.Default
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        ''En esta Variable le paso la fecha actual
    '        Dim rubro As String, codmaterial As String, cc As String, letracc As String, retiro As String, entrego As String

    '        paramreporte.AgregarParametros("rubro :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("centro costo :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Letra Centro Costo :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Retiro :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Entrego :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnTM3)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnTM3)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos Polene"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            rubro = paramreporte.ObtenerParametros(0).ToString
    '            codmaterial = paramreporte.ObtenerParametros(1).ToString
    '            cc = paramreporte.ObtenerParametros(2).ToString
    '            letracc = paramreporte.ObtenerParametros(3).ToString
    '            retiro = paramreporte.ObtenerParametros(4).ToString
    '            entrego = paramreporte.ObtenerParametros(5).ToString
    '            Inicial = paramreporte.ObtenerParametros(6).ToString
    '            Final = paramreporte.ObtenerParametros(7).ToString

    '            rpt.MostrarConsumosPolene(rubro, codmaterial, cc, letracc, retiro, entrego, Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 20-5-2011
    '    Private Sub ComprasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComprasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        ''En esta Variable le paso la fecha actual
    '        Dim rubro As String, codmaterial As String

    '        paramreporte.AgregarParametros("rubro :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnTM3)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnTM3)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "compras polene"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            rubro = paramreporte.ObtenerParametros(0).ToString
    '            codmaterial = paramreporte.ObtenerParametros(1).ToString
    '            Inicial = paramreporte.ObtenerParametros(2).ToString
    '            Final = paramreporte.ObtenerParametros(3).ToString

    '            rpt.MostrarComprasPolene(rubro, codmaterial, Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Sub MovimientosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        ''En esta Variable le paso la fecha actual
    '        '    Dim rubro As String, 
    '        Dim codmaterial As String

    '        paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnTM3)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnTM3)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "compras polene"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then

    '            codmaterial = paramreporte.ObtenerParametros(0).ToString
    '            Inicial = paramreporte.ObtenerParametros(1).ToString
    '            Final = paramreporte.ObtenerParametros(2).ToString

    '            rpt.MostrarMovimientosPolene(codmaterial, Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Sub CalculoDeReposiciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculoDeReposiciónToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        'Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim CnnPANOL As New SqlConnection(ConnStringPanolNet)

    '        Dim familia As String
    '        Dim vertodos As String
    '        Dim consulta1 As String = ""
    '        Dim consulta2 As String = ""

    '        'consulta1 = "select '' as nombre union all select nombre from familias where eliminado=0 group by nombre order by nombre"
    '        'dg pidio mostrar codigo - nombre
    '        consulta1 = "select '' as nombre union all select codigo + ' - ' + nombre as nombre from familias where eliminado=0 group by codigo,nombre order by nombre"
    '        consulta2 = "select '' as codigo union select 'SI' as codigo union select 'NO' as codigo"

    '        paramreporte.AgregarParametros("Nombre de Familia:", "STRING", "", False, "", consulta1, CnnPANOL)
    '        paramreporte.AgregarParametros("Ver Todos:", "STRING", "", False, "", consulta2, CnnPANOL)
    '        paramreporte.ShowDialog()


    '        nbreformreportes = "Cálculo de Reposición"
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            'familia = paramreporte.ObtenerParametros(0).ToString
    '            familia = Mid(paramreporte.ObtenerParametros(0).ToString, 1, 10) 'ahora obtenemos el codigo..
    '            vertodos = paramreporte.ObtenerParametros(1).ToString
    '            rpt.MostrarCalculoReposicionNet(familia, vertodos, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '        CnnPANOL = Nothing

    '        ''COMENTADO MS 01-11-2011 AL PONER PAÑOL NUEVO
    '        ''Dim excelApp As New Excel.Application()
    '        ''Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        ''Dim xlRange As excel.Range = Nothing
    '        ''Dim xlsheets As excel.Sheets = Nothing
    '        ''Dim xlWorkSheet As excel.Worksheet = Nothing
    '        ''Dim xlWorkBook As excel.Workbook = Nothing
    '        ''Dim missing As Object = Type.Missing
    '        ''Dim FileName As String = ""
    '        ''Dim iRow As Integer = 2
    '        ''Dim iCol As Integer = 1
    '        ''Dim connection As SqlClient.SqlConnection = Nothing

    '        ''OpenFileDialog1.Filter = "Archivos Excel (*.xls)|*.xls|All files (*.*)|*.*"
    '        ''OpenFileDialog1.FilterIndex = 2

    '        ''If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '        ''    FileName = OpenFileDialog1.FileName
    '        ''Else
    '        ''    FileName = ""
    '        ''End If

    '        ''xlWorkBook = excelApp.Workbooks.Open(FileName, 0, True, 5, "", "", True, Excel.XlPlatform.xlWindows, "\t", False, False, 0, True)
    '        ''xlsheets = xlWorkBook.Worksheets
    '        ''xlWorkSheet = CType(xlsheets(1), Excel.Worksheet)

    '        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        ' '' saltear la 1ra. linea que son titulos
    '        ' '' buscar algunos titulos de la segunda linea para ver si excel sigue con las mismas columnas
    '        ' '' de siempre o se modificó el formato.
    '        ''xlRange = xlWorkSheet.Cells(2, 1)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "solicitud" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 1 debe ser 'Solicitud'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 13)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "estado" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 13 debe ser 'Estado'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 15)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "descripción" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 15 debe ser 'Descripción'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 20)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "recepcionó" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 19 debe ser 'Recepcionó'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 21)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "por proveed." Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 20 debe ser 'Por Proveed.'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 22)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "por el usuario" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 21 debe ser 'por el usuario'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 25)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "solicitante" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 23 debe ser 'solicitante'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 32)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "motivo de rectificación" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 24 debe ser 'motivo de rectificación'")
    '        ''    Exit Sub
    '        ''End If
    '        ''xlRange = xlWorkSheet.Cells(2, 43)
    '        ''If xlRange.Text.ToString.ToLower.Trim <> "asignado" Then
    '        ''    MessageBox.Show("Error en el archivo Excel, la columna 26 debe ser 'asigando '")
    '        ''    Exit Sub
    '        ''End If

    '        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        ' '' conectar con la base de la tabla tamporal
    '        ''Try
    '        ''    connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
    '        ''Catch ex As Exception
    '        ''    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ''    Exit Sub
    '        ''End Try

    '        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        ' '' formar los insert para la tabla temporal de pedidos...
    '        ''Dim consulta As String
    '        ''iRow = 3
    '        ''Dim cantFilas As Long = 3000
    '        ''frmEspera.Maximo = cantFilas
    '        ''frmEspera.valor_progress(iRow)
    '        ''frmEspera.Show()
    '        ''xlRange = xlWorkSheet.Cells(iRow, iCol)
    '        ''While xlRange.Text <> ""

    '        ''    consulta = "INSERT INTO PEDIDOS ([TipoSolicitud] ,[NumeroPC],[FechaSolicitud],[FechaConfeccion],[FechaNecesidad],[FechaProbableEntrega],[FechaRecepcionCompra],[FechaCotizacion],[FechaPresupuestado],[FechaOrdenRealizada],[FechaEnviadaProveedor],[FechaUltimoEstado],[Estado],[CodigoMaterial],[CantidadSolicitada],[EntregarPorProveedor],[RecepcionarPorUsuario],[CantidaEnEnvio],[Solicitante],[MotivoRectificacion],[OrdenCompra],[CompradorAsignado]) "
    '        ''    consulta = consulta & "VALUES ("

    '        ''    '----------- Solicitud
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 1)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Numero de PC
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 2)
    '        ''    consulta = consulta & IIf(xlRange.Text = "", "0", xlRange.Text) & ","

    '        ''    '----------- Fecha de Solicitud
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 3)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de Confección
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 4)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de Necesidad
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 5)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de entrega probable
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 6)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de recepción de compra
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 7)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de cotización
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 8)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de presupuesto
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 9)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de realización de la orden
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 10)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de envio del proveedor
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 11)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Fecha de ultimo estado
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 12)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Estado
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 13)
    '        ''    consulta = consulta & "'" & Replace(xlRange.Text, "'", "") & "',"

    '        ''    '----------- Codigo de Material
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 14)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Cantidad Solicitada
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 18)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 19)
    '        ''    consulta = consulta & ConvertirANumero(xlRange.Text) & ","

    '        ''    '----------- A Entregar por Proveedor
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 20)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 21)
    '        ''    consulta = consulta & ConvertirANumero(xlRange.Text) & ","

    '        ''    '----------- A Recepcionar por el usuario
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 21)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 22)
    '        ''    consulta = consulta & ConvertirANumero(xlRange.Text) & ","

    '        ''    '----------- Cantidad en Envío
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 22)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 23)
    '        ''    consulta = consulta & ConvertirANumero(xlRange.Text) & ","

    '        ''    '----------- Solicitante
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 23)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 25)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- Motivo de rectificación
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 24)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 32)
    '        ''    consulta = consulta & "'" & xlRange.Text & "',"

    '        ''    '----------- OC
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 25)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 34)
    '        ''    consulta = consulta & ConvertirANumero(xlRange.Text) & ","

    '        ''    '----------- Comprador
    '        ''    'xlRange = xlWorkSheet.Cells(iRow, 26)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, 43)
    '        ''    consulta = consulta & "'" & Replace(xlRange.Text, "'", "") & "')"

    '        ''    Try
    '        ''        Try
    '        ''            Try
    '        ''                If iRow = 3 Then
    '        ''                    SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "delete from pedidos")
    '        ''                End If
    '        ''                SqlHelper.ExecuteNonQuery(connection, CommandType.Text, consulta)
    '        ''            Catch ex As Exception
    '        ''                '' 
    '        ''                Throw ex
    '        ''            End Try
    '        ''        Finally
    '        ''            ''
    '        ''        End Try
    '        ''    Catch ex As Exception
    '        ''        Dim errMessage As String = ""
    '        ''        Dim tempException As Exception = ex
    '        ''        While (Not tempException Is Nothing)
    '        ''            errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '        ''            tempException = tempException.InnerException
    '        ''        End While
    '        ''        MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '        ''          + Environment.NewLine + "Error en la Fila " & iRow.ToString & " del excel. Por favor revisar", errMessage), _
    '        ''          "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ''    End Try

    '        ''    iRow = iRow + 1
    '        ''    frmEspera.valor_progress(iRow)
    '        ''    xlRange = xlWorkSheet.Cells(iRow, iCol)

    '        ''End While
    '        ''frmEspera.Close()
    '        ''Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''nbreformreportes = "Cálculo de Reposición Stock"
    '        ''Dim rpt As New frmReportes
    '        ''Dim paramreporte As New frmParametros
    '        ''Dim codmaterial As String, rubro As String

    '        ''paramreporte.AgregarParametros("cód. material :", "STRING", "", False, "", "", CnnTM3)
    '        ''paramreporte.AgregarParametros("Familia (Cód.) :", "STRING", "", False, "", "", CnnTM3)
    '        ''paramreporte.ShowDialog()

    '        ''Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        ''If cerroparametrosconaceptar = True Then

    '        ''    codmaterial = paramreporte.ObtenerParametros(0).ToString
    '        ''    rubro = paramreporte.ObtenerParametros(1).ToString

    '        ''    rpt.MostrarCalculoReposicion(codmaterial, rubro, rpt)
    '        ''    cerroparametrosconaceptar = False
    '        ''    paramreporte = Nothing

    '        ''End If

    '        ''Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Function ConvertirANumero(ByVal s As String) As String

    '        If s.Trim = "" Then
    '            ConvertirANumero = "0"
    '            Exit Function
    '        Else
    '            ConvertirANumero = s
    '            If Len(s) >= 3 Then
    '                If Mid$(s, Len(s) - 2, 1) = "." Then
    '                    s = Replace(s, ",", "")
    '                    ConvertirANumero = s
    '                    Exit Function
    '                Else
    '                    If Mid$(s, Len(s) - 2, 1) = "," Then
    '                        s = Replace(s, ".", "")
    '                        s = Replace(s, ",", ".")
    '                        ConvertirANumero = s
    '                        Exit Function
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End Function

    '    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
    '        Dim o As OpenFileDialog = sender
    '        Nombre_Archivo_Excel = o.FileName
    '    End Sub

    '    Private Sub TiemposImproductivosDeSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TiemposImproductivosDeSetupToolStripMenuItem.Click
    '        'PARADAS_NET_TiemposImproductivosSetup.rpt
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim op As String, maquina As String, motivo As String


    '        nbreformreportes = "Tiempos Improductivos Setup"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"

    '        Dim consulta2 As String = "select codigo from motivos where id in (13,15,18)"

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", True, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)
    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnParadas)
    '        paramreporte.AgregarParametros("Cod. de Motivo :", "STRING", "", True, "", consulta2, CnnParadas)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            maquina = paramreporte.ObtenerParametros(2).ToString
    '            op = paramreporte.ObtenerParametros(3).ToString
    '            motivo = paramreporte.ObtenerParametros(4)
    '            'reporte.MostrarReporteTiempoImproductivo(Inicial, Final, maquina, op, reporte)
    '            reporte.MostrarReporteTiempoImproductivoSetup(Inicial, Final, maquina, op, motivo, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default

    '    End Sub

    '    Private Sub ManoDeObraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManoDeObraToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim Cnntm3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim empleado As String, anulado As String, sector As String, tipo As String, abuelo As String, padre As String


    '        nbreformreportes = "Mano de Obra"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"
    '        Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", Cnntm3)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", Cnntm3)
    '        paramreporte.AgregarParametros("Empleado :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Anulado :", "STRING", "", False, "", consulta2, Cnntm3)
    '        paramreporte.AgregarParametros("Sector Resp. :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Tipo :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Abuelo :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Padre :", "STRING", "", False, "", "", Cnntm3)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            empleado = paramreporte.ObtenerParametros(2).ToString
    '            anulado = paramreporte.ObtenerParametros(3).ToString
    '            sector = paramreporte.ObtenerParametros(4)
    '            tipo = paramreporte.ObtenerParametros(5)
    '            abuelo = paramreporte.ObtenerParametros(6)
    '            padre = paramreporte.ObtenerParametros(7)

    '            reporte.MostrarReporteManoDeObra(Inicial, Final, empleado, anulado, sector, tipo, abuelo, padre, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub OTRealizadasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OTRealizadasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim Cnntm3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim anulado As String, ot As String, sector As String, tipo As String, equipo As String, abuelo As String, padre As String


    '        nbreformreportes = "Ordenes de Trabajo Realizadas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"
    '        Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", Cnntm3)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", Cnntm3)
    '        paramreporte.AgregarParametros("OT :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Equipo :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Anulado :", "STRING", "", False, "", consulta2, Cnntm3)
    '        paramreporte.AgregarParametros("Sector Resp. :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Tipo :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Abuelo :", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Padre :", "STRING", "", False, "", "", Cnntm3)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            ot = paramreporte.ObtenerParametros(2).ToString
    '            equipo = paramreporte.ObtenerParametros(3).ToString
    '            anulado = paramreporte.ObtenerParametros(4).ToString
    '            sector = paramreporte.ObtenerParametros(5)
    '            tipo = paramreporte.ObtenerParametros(6)
    '            abuelo = paramreporte.ObtenerParametros(7)
    '            padre = paramreporte.ObtenerParametros(8)

    '            reporte.MostrarReporteOTRealizadas(Inicial, Final, ot, equipo, anulado, sector, tipo, abuelo, padre, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 31-5-2011
    '    Private Sub ReporteDetalladoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteDetalladoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Reporte Detallado"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)
    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            op = paramreporte.ObtenerParametros(2).ToString

    '            reporte.MostrarReporteTuboscopeDetallado(Inicial, Final, op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 31-5-2011
    '    Private Sub ReporteResumidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteResumidoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Reporte Resumido"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)
    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            op = paramreporte.ObtenerParametros(2).ToString

    '            reporte.MostrarReporteTuboscopeResumido(Inicial, Final, op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub DiseñosDeTubosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiseñosDeTubosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Listado de Diseños"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarListadodeDisenos(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ControlesSobreLasMezclasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlesSobreLasMezclasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Controles sobre las mezclas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarControlesSobreMezclas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ControlesSobreProcesosMesa1FibraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlesSobreProcesosMesa1FibraToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Controles sobre Procesos (Fibra)"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarControlesSobreProcesos(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ControlesSobreCañosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlesSobreCañosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Controles sobre Caños"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarControlesSobreCanos(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub DiseñoDeAlertasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiseñoDeAlertasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Diseño de Alertas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarControlesAlerta(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub OrdenDeProducciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdenDeProducciónToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Orden de Producción"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarReporteOrdenProduccion(op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 1-6-2011
    '    Private Sub EstadoDeOrdenesabiertacerradaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstadoDeOrdenesabiertacerradaToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Estado de Ordenes de Producción (abierta/cerrada)"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarEstadoDeOrdenes(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 1-6-2011
    '    Private Sub RecetasParaMezclasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecetasParaMezclasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Proporciones de Recetas "
    '        Dim rpt As New frmReportes
    '        rpt.MostrarRecetas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 1-6-2011
    '    Private Sub ControlesSobreMateriaPrimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlesSobreMateriaPrimaToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Controles sobre MAteria Prima "
    '        Dim rpt As New frmReportes
    '        rpt.MostrarControlesMateriaPrima(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 1-6-2011
    '    Private Sub PlanDeInspecciónCompletoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlanDeInspecciónCompletoToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Plan Completo de Inspección"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarPlanInspeccion(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 2-6-2011
    '    Private Sub UnidadesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnidadesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Unidades"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroUnidadesRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 2-6-2011
    '    Private Sub SectoresPuestosDeControlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SectoresPuestosDeControlToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Sectores"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroSectoresRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 2-6-2011
    '    Private Sub TipoDeControlesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TipoDeControlesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Tipo de Controles"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroTipoControlesRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 2-6-2011
    '    Private Sub HerramientasGenéricasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HerramientasGenéricasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Herramientas Genéricas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroHerramientas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 2-6-2011
    '    Private Sub HerramientasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HerramientasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Herramientas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroHerramientasPArt(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 3-6-2011
    '    Private Sub InstrumentosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstrumentosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim codigo As String

    '        nbreformreportes = "Maestro de Instrumentos"

    '        paramreporte.AgregarParametros("Cód.Instrumento :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            codigo = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarMaestroInstrumentos(codigo, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 3-6-2011
    '    Private Sub ClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Clientes"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroClientes(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProveedoresToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Proveedores"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroProveedores(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 3-6-2011
    '    Private Sub MotivosDeFallaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MotivosDeFallaToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Motivos de Falla"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroMotivosRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 3-6-2011
    '    Private Sub MateriasPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MateriasPToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Materias Primas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroMateriasPrimas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 3-6-2011
    '    Private Sub AlertasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlertasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Alertas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroAlertas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'cp 3-6-2011
    '    Private Sub UsuariosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsuariosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Listado de Usuarios del Sistema"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroUsuariosRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub UsuariosPerfilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsuariosPerfilesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Listado de Perfiles por Usuario"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroUsuariosPerfilesRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub PerfilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerfilesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Listado de Pefiles"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroPerfilesRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub AccesosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccesosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Accesos al Sistema por Perfil"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroAccesosRN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub AccesosYUsuariosPorPerfilToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccesosYUsuariosPorPerfilToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Accesos al Sistema por Perfil y Usuarios"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarMaestroAccesos_RN(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub EgresosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EgresosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String, mp As String, egreso As String

    '        nbreformreportes = "Egresos de Materias Primas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)
    '        paramreporte.AgregarParametros("Nro.Egreso :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Cod. Material :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            egreso = paramreporte.ObtenerParametros(2).ToString
    '            op = paramreporte.ObtenerParametros(3).ToString
    '            mp = paramreporte.ObtenerParametros(4).ToString

    '            reporte.MostrarReporteEgresoMP(Inicial, Final, egreso, op, mp, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub IngresosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IngresosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim tipo As String, mp As String, ingreso As String

    '        nbreformreportes = "Ingresos de Materias Primas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)
    '        paramreporte.AgregarParametros("Nro.Ingreso :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Tipo Material :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Cod. Material :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            ingreso = paramreporte.ObtenerParametros(2).ToString
    '            tipo = paramreporte.ObtenerParametros(3).ToString
    '            mp = paramreporte.ObtenerParametros(4).ToString

    '            reporte.MostrarReporteIngresoMP(Inicial, Final, ingreso, tipo, mp, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub MateriasPrimasStockEnProducciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MateriasPrimasStockEnProducciónToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim un As String, mostrarceros As String
    '        Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"
    '        nbreformreportes = "Stock de Materias Primas (Producción)"

    '        paramreporte.AgregarParametros("Negocio (Fibra/Revestido) :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Mostrar Stocks Ceros :", "STRING", "", False, "", consulta2, CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            un = paramreporte.ObtenerParametros(0).ToString
    '            mostrarceros = paramreporte.ObtenerParametros(1).ToString


    '            reporte.MostrarReporteStockMateriasPrimas(un, mostrarceros, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 23/09/2011
    '    Private Sub StockDeSemielaboradoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeSemielaboradoToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Caños Semielaborado"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockCañosSemielaborado(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 23/09/2011
    '    Private Sub StockDeRoscadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeRoscadosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Caños Roscados"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockCañosRoscados(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 23/09/2011
    '    Private Sub StockDeRIToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeRIToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Caños RI"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockCañosRI(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 23/09/2011
    '    Private Sub StockDeRevestidosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeRevestidosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Caños Revestidos"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockCañosRevestidos(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 23/09/2011
    '    Private Sub StockFlejesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockFlejesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Flejes"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockFlejes(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 23/09/2011
    '    Private Sub StockBobinasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockBobinasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Bobinas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockBobinas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 26/09/2011
    '    Private Sub StockBobinasConTotalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockBobinasConTotalesToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim Cnn As New SqlConnection(ConnStringPERFO)
    '        Dim empresa As String
    '        nbreformreportes = "Stock de bobinas con Totales"

    '        paramreporte.AgregarParametros("E:", "STRING", "", False, "", "", Cnn)
    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            empresa = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarStockBobinasConTotales(empresa, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 26/09/2011
    '    Private Sub StockDepositosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDepositosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim cliente As String, nv As String, nvitem As String, deposito As String

    '        nbreformreportes = "Stock Depositos"

    '        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Nota de Venta :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Item NV :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Deposito :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            cliente = paramreporte.ObtenerParametros(0).ToString
    '            nv = paramreporte.ObtenerParametros(1).ToString
    '            nvitem = paramreporte.ObtenerParametros(2).ToString
    '            deposito = paramreporte.ObtenerParametros(3).ToString


    '            reporte.MostrarReporteStockDepositos(cliente, nv, nvitem, deposito, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 27/09/2011
    '    Private Sub StockDepositosResumidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDepositosResumidoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim cliente As String, nv As String, nvitem As String, deposito As String

    '        nbreformreportes = "Stock Depositos Resumido"

    '        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Nota de Venta :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Item NV :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Deposito :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            cliente = paramreporte.ObtenerParametros(0).ToString
    '            nv = paramreporte.ObtenerParametros(1).ToString
    '            nvitem = paramreporte.ObtenerParametros(2).ToString
    '            deposito = paramreporte.ObtenerParametros(3).ToString


    '            reporte.MostrarReporteStockDepositosResumido(cliente, nv, nvitem, deposito, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 26/09/2011
    '    Private Sub AjustesDeStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjustesDeStockToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim cliente As String, nv As String, nvitem As String, deposito As String

    '        nbreformreportes = "Ajustes de Stock"

    '        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Nota de Venta :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Item NV :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Deposito :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            cliente = paramreporte.ObtenerParametros(0).ToString
    '            nv = paramreporte.ObtenerParametros(1).ToString
    '            nvitem = paramreporte.ObtenerParametros(2).ToString
    '            deposito = paramreporte.ObtenerParametros(3).ToString


    '            reporte.MostrarReporteAjustesDeStock(cliente, nv, nvitem, deposito, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''RN Submenu Despacho
    '    ''ms 27/09/2011
    '    Private Sub CanosDespachadosPorRemitoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanosDespachadosPorRemitoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim sucursal As String, remito As String

    '        nbreformreportes = "Caños Despachados por Remito"

    '        paramreporte.AgregarParametros("Sucursal :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Remito :", "STRING", "", False, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            sucursal = paramreporte.ObtenerParametros(0).ToString
    '            remito = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteDespachadosPorRemito(sucursal, remito, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 30-09-2011
    '    Private Sub CanosDespachadosPorNVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanosDespachadosPorNVToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nv As String

    '        nbreformreportes = "Despachados por NV"

    '        paramreporte.AgregarParametros("Nota de Venta :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            nv = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarReporteDespachadosPorNV(nv, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 28-09-2011
    '    Private Sub DespachosResumidosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DespachosResumidosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        nbreformreportes = "Despachos Resumidos"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteDespachosResumidos(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 03-10-2011
    '    Private Sub EstadosDeNotasDeVentasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstadosDeNotasDeVentasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        nbreformreportes = "Estado de Notas de Ventas"

    '        Dim nvdesde As String = ""
    '        Dim nvhasta As String = ""

    '        paramreporte.AgregarParametros("NV Desde :", "STRING", "", True, nvdesde, "", CnnRN)
    '        paramreporte.AgregarParametros("NV Hasta :", "STRING", "", True, nvhasta, "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            nvdesde = paramreporte.ObtenerParametros(0).ToString
    '            nvhasta = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteEstadoNV(nvdesde, nvhasta, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 03-10-2011
    '    Private Sub PaquetesDeCanosDisponiblesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaquetesDeCanosDisponiblesToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        nbreformreportes = "Paquetes de Caños"
    '        Dim diseno As String


    '        paramreporte.AgregarParametros("Diseno :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            diseno = paramreporte.ObtenerParametros(0).ToString

    '            ''HACER UN STORE PARA QUE LLAME A exec spGenerarReportePaquetes @diseno,@res
    '            GenerarReportePaquetes(diseno)
    '            reporte.MostrarReportePaquetesDeCanosDisponibles(diseno, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 04-10-2011
    '    Private Sub GenerarReportePaquetes(ByVal diseno As String)
    '        Dim connection As SqlClient.SqlConnection = Nothing
    '        Dim res As Integer = 0

    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPERFO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Sub
    '            End Try

    '            Try

    '                Dim param_diseno As New SqlClient.SqlParameter
    '                param_diseno.ParameterName = "@DISENO"
    '                param_diseno.SqlDbType = SqlDbType.VarChar
    '                param_diseno.Size = 15
    '                param_diseno.Value = diseno
    '                param_diseno.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@RESULTADO"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = DBNull.Value
    '                param_res.Direction = ParameterDirection.InputOutput

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spGenerarReportePaquetes", param_diseno, param_res)
    '                    res = param_res.Value
    '                Catch ex As Exception
    '                    Throw ex
    '                End Try
    '            Finally

    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
    '              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try
    '    End Sub


    '    ''ms 28-09-2011
    '    Private Sub CanosTildadosEnPlayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanosTildadosEnPlayToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        nbreformreportes = "Caños Tildados en Playa"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteCanosTildadosEnPlaya(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 29-09-2011
    '    Private Sub DespachoDetalladoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DespachoDetalladoToolStripMenuItem.Click
    '        ''COMENTADO MS 20-12-2011 PARA AGREGARLE PARAMETROS
    '        ' ''Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ' ''nbreformreportes = "Despacho Detallado"
    '        ' ''Dim rpt As New frmReportes
    '        ' ''rpt.MostrarDespachoDetallado(rpt)
    '        ' ''Cursor = System.Windows.Forms.Cursors.Default
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim sucursal As String, remito As String, diseno As String, nv As String

    '        nbreformreportes = "Despacho Detallado"

    '        paramreporte.AgregarParametros("Producto :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("NV :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Sucursal :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Remito :", "STRING", "", False, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            diseno = paramreporte.ObtenerParametros(0).ToString
    '            nv = paramreporte.ObtenerParametros(1).ToString
    '            sucursal = paramreporte.ObtenerParametros(2).ToString
    '            remito = paramreporte.ObtenerParametros(3).ToString

    '            ''reporte.MostrarReporteDespachadosPorRemito(sucursal, remito, reporte)
    '            reporte.MostrarDespachoDetallado(diseno, nv, sucursal, remito, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''ms 29-09-2011
    '    Private Sub NVPendientesYVencidasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NVPendientesYVencidasToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "NV Pendientes y Vencidas"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarNVPendientesyVencidas(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'AL - 10/11/2011
    '    Private Sub NVPendientesDetalleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NVPendientesDetalleToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim Tipo As String

    '        paramreporte.AgregarParametros("Tipo :", "String", "", False, "ACERO", "", CnnRN)

    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            Tipo = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarReporteNVPendientes_Detalle(Tipo, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing

    '        End If

    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''nuevo ms 13-02-2012
    '    Private Sub NVPendientesYVencidasPorSubTipoDeProductoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NVPendientesYVencidasPorSubTipoDeProductoToolStripMenuItem.Click
    '        '' ''Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        '' ''nbreformreportes = "NV Pendientes y Vencidas"
    '        '' ''Dim rpt As New frmReportes
    '        '' ''rpt.MostrarNVPendientesyVencidas(rpt)
    '        '' ''Cursor = System.Windows.Forms.Cursors.Default

    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim SubTipoProducto As String
    '        Dim consulta As String = "select distinct subtipoproducto from disenos"
    '        paramreporte.AgregarParametros("Tipo :", "String", "", False, "", consulta, CnnRN)

    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            SubTipoProducto = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarReporteNVPendientesyVencidasPorSubProd(SubTipoProducto, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing

    '        End If

    '        Cursor = System.Windows.Forms.Cursors.Default


    '    End Sub
    '    ''fin nuevo

    '    'cp 3-6-2011
    '    Private Sub BobinasDatosFísicoQuímicosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BobinasDatosFísicoQuímicosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim bobina As String, colada As String, lote As String

    '        nbreformreportes = "Bobinas (Físico/Químicos)"

    '        paramreporte.AgregarParametros("Nro.Bobina :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Nro.Lote Prov. :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Colada :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            bobina = paramreporte.ObtenerParametros(0).ToString
    '            lote = paramreporte.ObtenerParametros(1).ToString
    '            colada = paramreporte.ObtenerParametros(2).ToString


    '            reporte.MostrarReporteDatosBobinas(bobina, lote, colada, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ' 6-6-2011 cp
    '    Private Sub BobinasDetalleFísicoQuímicosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BobinasDetalleFísicoQuímicosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim bobina As String, calidad As String, lote As String, ancho As String, espesor As String

    '        nbreformreportes = "Bobinas Detalle (Físico/Químicos)"

    '        paramreporte.AgregarParametros("Nro.Bobina :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Nro.Lote Prov. :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Calidad :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Ancho :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Espesor :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            bobina = paramreporte.ObtenerParametros(0).ToString
    '            lote = paramreporte.ObtenerParametros(1).ToString
    '            calidad = paramreporte.ObtenerParametros(2).ToString
    '            ancho = paramreporte.ObtenerParametros(3).ToString
    '            espesor = paramreporte.ObtenerParametros(4).ToString


    '            reporte.MostrarReporteDetalleBobinas(bobina, lote, calidad, ancho, espesor, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ' 6-6-2011 cp
    '    Private Sub NroDeMezclasLoteRemitoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NroDeMezclasLoteRemitoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String, remito As String

    '        nbreformreportes = "Mezclas por Nro. de Lote y Remito"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Remito :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString
    '            remito = paramreporte.ObtenerParametros(1).ToString


    '            reporte.MostrarReporteNrosMezcla(op, remito, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    '6-6-2011 cp
    '    Private Sub LotesDeMateriaPrimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LotesDeMateriaPrimaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Lotes de Materias Primas"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString



    '            reporte.MostrarReporteNrosLotes(op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    '6-6-2011 cp
    '    Private Sub EnsayosSobreMateriasPrimasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnsayosSobreMateriasPrimasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Ensayos de Materias Primas por Lote y OP"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString



    '            reporte.MostrarReporteEnsayosMPLote(op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    '6-6-2011 cp
    '    Private Sub IngresosDeMateriaPrimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IngresosDeMateriaPrimaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Ingresos de Materia Prima por OP"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString



    '            reporte.MostrarReporteIngresosDeMateriaPrima(op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub PreparacionesDeMezclasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreparacionesDeMezclasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Preparaciones de Mezclas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)
    '        paramreporte.AgregarParametros("Nro. Mezcla :", "STRING", "", False, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            nro = paramreporte.ObtenerParametros(2).ToString

    '            reporte.MostrarReportePreparacionDeMezclas(Inicial, Final, nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub






    '    Private Sub PendientesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Ensayos de Procesos Pendientes"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarEnsayosProcesosPendientes(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub AltaDeCañosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AltaDeCañosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Altas de Caños"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarReporteAltaCanos(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '#Region "MENU CUPLAS"
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ''MENU CUPLAS
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    Private Sub StockCuplasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockCuplasToolStripMenuItem.Click
    '        nbreformreportes = "Stock Cuplas"
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        Dim reportestockcuplas As New frmReportes
    '        reportestockcuplas.MostrarStockCuplas(reportestockcuplas)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ProduccionCuplasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProduccionCuplasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnCuplas As New SqlConnection(ConnStringCuplas)

    '        nbreformreportes = "Producción Cuplas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnCuplas)
    '        paramreporte.AgregarParametros("Fin :", "DATE", "", True, Final, "", CnnCuplas)

    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarProduccionCuplas(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default

    '    End Sub

    '    Private Sub ConsumoCuplasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumoCuplasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnCuplas As New SqlConnection(ConnStringCuplas)

    '        nbreformreportes = "Consumo Cuplas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnCuplas)
    '        paramreporte.AgregarParametros("Fin :", "DATE", "", True, Final, "", CnnCuplas)

    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            'reporte.MostrarProduccionCuplas(Inicial, Final, reporte)
    '            reporte.MostrarConsumoCuplas(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ProtectoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProtectoresToolStripMenuItem.Click
    '        nbreformreportes = "Protectores Cuplas"
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        Dim reportesprotectorescuplas As New frmReportes
    '        reportesprotectorescuplas.MostrarProtectoresCuplas(reportesprotectorescuplas)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ProtectoresTotalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProtectoresTotalesToolStripMenuItem.Click
    '        nbreformreportes = "Protectores Totales Cuplas"
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        Dim reportesprotectorestotalescuplas As New frmReportes
    '        reportesprotectorestotalescuplas.MostrarProtectoresTotalesCuplas(reportesprotectorestotalescuplas)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '#End Region


    '    Private Sub ControlDeCalidadPorOPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlDeCalidadPorOPToolStripMenuItem.Click
    '        ''COMENTADO MS 28-07-2011
    '        ''Dim paramreporte As New frmParametros
    '        ''Dim reporte As New frmReportes
    '        ''Dim reporte_setup1 As New frmReportes
    '        ''Dim reporte_setup2 As New frmReportes
    '        ''Dim reporte_setup3 As New frmReportes
    '        ''Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        ''Dim op As String

    '        ''nbreformreportes = "Control de Calidad - Gráficos"

    '        ''Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim consulta As String = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre from lugares where isnull(idlugarpadre,'')='' and eliminado=0 order by substring(Nombre,1,charindex('-',Nombre,1)-2)"
    '        ''Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"

    '        ''paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnParadas)
    '        ''paramreporte.AgregarParametros("Fín :", "DATE", "", True, Final, "", CnnParadas)
    '        ''paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnParadas)

    '        ''paramreporte.ShowDialog()

    '        ''Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''If cerroparametrosconaceptar = True Then

    '        ''    Inicial = paramreporte.ObtenerParametros(0).ToString
    '        ''    Final = paramreporte.ObtenerParametros(1).ToString
    '        ''    op = paramreporte.ObtenerParametros(2).ToString

    '        ''    '---------------------------------------------------------------------------------------------------------------------------
    '        ''    'EJECUCION DEL PROCEDIMIENTO PARA LEVANTAR LA INFORMACION 

    '        ''    Dim connection As SqlClient.SqlConnection = Nothing
    '        ''    Dim res As Integer = 0

    '        ''    Try
    '        ''        Try
    '        ''            connection = SqlHelper.GetConnection(ConnStringPERFO)
    '        ''        Catch ex As Exception
    '        ''            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ''            Exit Sub
    '        ''        End Try

    '        ''        Try
    '        ''            Dim param_op As New SqlClient.SqlParameter
    '        ''            param_op.ParameterName = "@idop"
    '        ''            param_op.SqlDbType = SqlDbType.VarChar
    '        ''            param_op.Size = 10
    '        ''            param_op.Value = op
    '        ''            param_op.Direction = ParameterDirection.Input

    '        ''            Dim param_res As New SqlClient.SqlParameter
    '        ''            param_res.ParameterName = "@res"
    '        ''            param_res.SqlDbType = SqlDbType.Int
    '        ''            param_res.Value = DBNull.Value
    '        ''            param_res.Direction = ParameterDirection.InputOutput

    '        ''            'Dim param_op2 As New SqlClient.SqlParameter
    '        ''            'param_op2.ParameterName = "@idop"
    '        ''            'param_op2.SqlDbType = SqlDbType.VarChar
    '        ''            'param_op2.Size = 10
    '        ''            'param_op2.Value = op
    '        ''            'param_op2.Direction = ParameterDirection.Input

    '        ''            Dim param_fechainicio As New SqlClient.SqlParameter
    '        ''            param_fechainicio.ParameterName = "@inicio"
    '        ''            param_fechainicio.SqlDbType = SqlDbType.DateTime
    '        ''            param_fechainicio.Value = Inicial
    '        ''            param_fechainicio.Direction = ParameterDirection.Input

    '        ''            Dim param_fechafin As New SqlClient.SqlParameter
    '        ''            param_fechafin.ParameterName = "@fin"
    '        ''            param_fechafin.SqlDbType = SqlDbType.DateTime
    '        ''            param_fechafin.Value = Final
    '        ''            param_fechafin.Direction = ParameterDirection.Input

    '        ''            Try
    '        ''                'SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spReporteCalidadGrafico_EjecucionCompleta", param_op, param_fechainicio, param_fechafin, param_res)
    '        ''                Dim SQL As SqlCommand = New SqlCommand("spReporteCalidadGrafico_EjecucionCompleta", connection)

    '        ''                SQL.CommandTimeout = 100
    '        ''                SQL.CommandType = CommandType.StoredProcedure

    '        ''                SQL.Parameters.Add(param_op)
    '        ''                SQL.Parameters.Add(param_res)
    '        ''                SQL.Parameters.Add(param_fechainicio)
    '        ''                SQL.Parameters.Add(param_fechafin)

    '        ''                SQL.ExecuteNonQuery()

    '        ''                'res = CInt(param_res.Value)

    '        ''                'If res = 0 Then
    '        ''                '    MsgBox("Se ha producido un error al generar el reporte. Consulte con el Dpto de Sistemas.")
    '        ''                '    Exit Sub
    '        ''                'End If

    '        ''            Catch ex As Exception
    '        ''                Throw ex
    '        ''            End Try
    '        ''        Finally

    '        ''        End Try
    '        ''    Catch ex As Exception
    '        ''        Dim errMessage As String = ""
    '        ''        Dim tempException As Exception = ex

    '        ''        While (Not tempException Is Nothing)
    '        ''            errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '        ''            tempException = tempException.InnerException
    '        ''        End While

    '        ''        MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
    '        ''          + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
    '        ''          "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        ''        Exit Sub

    '        ''    Finally
    '        ''        If Not connection Is Nothing Then
    '        ''            CType(connection, IDisposable).Dispose()
    '        ''        End If
    '        ''    End Try

    '        ''    reporte.MdiParent = Me
    '        ''    reporte.MostrarReporteControlCalidadGraficos(Inicial, Final, op, reporte)
    '        ''    cerroparametrosconaceptar = False
    '        ''    paramreporte = Nothing

    '        ''End If

    '        ''Cursor = System.Windows.Forms.Cursors.Default
    '        ''FIN COMENTADO
    '        ''NUEVO MS 28-07-2011
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim reporte_setup1 As New frmReportes
    '        Dim reporte_setup2 As New frmReportes
    '        Dim reporte_setup3 As New frmReportes
    '        Dim CnnParadas As New SqlConnection(ConnStringPARADAS)
    '        Dim op, maquina As String

    '        nbreformreportes = "Control de Calidad - Gráficos"

    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String
    '        Dim consulta2 As String = "select 'SI' as si union all select 'NO' as si"

    '        consulta = "select substring(Nombre,1,charindex('-',Nombre,1)-2) AS nombrelugarpadre " & _
    '                " from lugares where isnull(idlugarpadre,'')='' and eliminado=0 " & _
    '                " and idlugar in (14000, 13000, 10200, 10100, 10400, 10500) " & _
    '                " order by substring(Nombre,1,charindex('-',Nombre,1)-2)"

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnParadas)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", True, Final, "", CnnParadas)
    '        paramreporte.AgregarParametros("Máquina :", "STRING", "", True, "", consulta, CnnParadas)
    '        paramreporte.AgregarParametros("OP / OC :", "STRING", "", False, "", "", CnnParadas)

    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            maquina = paramreporte.ObtenerParametros(2).ToString
    '            op = paramreporte.ObtenerParametros(3).ToString

    '            ''COMENTADO MS 23/08/2011 a pedido de AL
    '            ' ''---------------------------------------------------------------------------------------------------------------------------
    '            ' ''EJECUCION DEL PROCEDIMIENTO PARA LEVANTAR LA INFORMACION 

    '            ''Dim connection As SqlClient.SqlConnection = Nothing
    '            ''Dim res As Integer = 0

    '            ''Try
    '            ''    Try
    '            ''        connection = SqlHelper.GetConnection(ConnStringPERFO)
    '            ''    Catch ex As Exception
    '            ''        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            ''        Exit Sub
    '            ''    End Try

    '            ''    Try
    '            ''        Dim param_op As New SqlClient.SqlParameter
    '            ''        param_op.ParameterName = "@idop"
    '            ''        param_op.SqlDbType = SqlDbType.VarChar
    '            ''        param_op.Size = 10
    '            ''        param_op.Value = op
    '            ''        param_op.Direction = ParameterDirection.Input

    '            ''        Dim param_fechainicio As New SqlClient.SqlParameter
    '            ''        param_fechainicio.ParameterName = "@inicio"
    '            ''        param_fechainicio.SqlDbType = SqlDbType.DateTime
    '            ''        param_fechainicio.Value = Inicial
    '            ''        param_fechainicio.Direction = ParameterDirection.Input

    '            ''        Dim param_fechafin As New SqlClient.SqlParameter
    '            ''        param_fechafin.ParameterName = "@fin"
    '            ''        param_fechafin.SqlDbType = SqlDbType.DateTime
    '            ''        param_fechafin.Value = Final
    '            ''        param_fechafin.Direction = ParameterDirection.Input

    '            ''        Dim param_maquina As New SqlClient.SqlParameter
    '            ''        param_maquina.ParameterName = "@equipo"
    '            ''        param_maquina.SqlDbType = SqlDbType.VarChar
    '            ''        param_maquina.Size = 30
    '            ''        param_maquina.Value = maquina
    '            ''        param_maquina.Direction = ParameterDirection.Input

    '            ''        Try
    '            ''            Dim SQL As SqlCommand = New SqlCommand("spReporteCalidadGrafico_EjecucionCompleta", connection)

    '            ''            SQL.CommandTimeout = 200
    '            ''            SQL.CommandType = CommandType.StoredProcedure

    '            ''            SQL.Parameters.Add(param_op)
    '            ''            SQL.Parameters.Add(param_fechainicio)
    '            ''            SQL.Parameters.Add(param_fechafin)
    '            ''            SQL.Parameters.Add(param_maquina)

    '            ''            SQL.ExecuteNonQuery()

    '            ''        Catch ex As Exception
    '            ''            Throw ex
    '            ''        End Try
    '            ''    Finally

    '            ''    End Try
    '            ''Catch ex As Exception
    '            ''    Dim errMessage As String = ""
    '            ''    Dim tempException As Exception = ex

    '            ''    While (Not tempException Is Nothing)
    '            ''        errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '            ''        tempException = tempException.InnerException
    '            ''    End While

    '            ''    MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
    '            ''      + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
    '            ''      "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '            ''    Exit Sub

    '            ''Finally
    '            ''    If Not connection Is Nothing Then
    '            ''        CType(connection, IDisposable).Dispose()
    '            ''    End If
    '            ''End Try
    '            ''FIN NUEVO

    '            'reporte.MdiParent = Me
    '            reporte.MostrarReporteControlCalidadGraficos(Inicial, Final, op, reporte, maquina)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing

    '        End If

    '        Cursor = System.Windows.Forms.Cursors.Default
    '        ''FIN NUEVO

    '    End Sub

    '    Private Sub PendientesToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendientesToolStripMenuItem2.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Ensayos de Procesos Pendientes"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarEnsayosProcesosPendientes(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub RealizadosToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RealizadosToolStripMenuItem2.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)

    '        nbreformreportes = "Ensayos Sobre Procesos"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteEnsayosProcesos(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub RealizadosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RealizadosToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)


    '        nbreformreportes = "Ensayos Sobre Mezclas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteEnsayosMezclas(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Sub PendientesToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendientesToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Ensayos de Mezclas Pendientes"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarEnsayosPendientesMezcla(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub SobreMateriasPrimasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SobreMateriasPrimasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)


    '        nbreformreportes = "Ensayos Sobre Materias Primas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", True, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", True, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarReporteEnsayosMP(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub BookXOPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookXOPToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String
    '        Dim consulta2 As String = "select 'S' as si union all select 'N' as si"
    '        Dim aprobo As String

    '        nbreformreportes = "Book por Número de Orden de Producción"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Aprobó (S/N) :", "STRING", "", False, "", consulta2, CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            If paramreporte.ObtenerParametros(1).ToString = "S" Then
    '                aprobo = "1"
    '            Else
    '                aprobo = "0"
    '            End If
    '            reporte.MostrarBookPorOP(nro, aprobo, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub BookXRemitoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookXRemitoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String, suc As String
    '        nbreformreportes = "Book por Número de Remito"

    '        paramreporte.AgregarParametros("Sucursal :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Remito :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            suc = paramreporte.ObtenerParametros(1).ToString
    '            reporte.MostrarBookPorRemito(nro, suc, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub EnsayosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnsayosToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String, cano As String, sector As String, control As String, frec As String
    '        nbreformreportes = "Ensayos sobre canos"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Caño :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Sector :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Control :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Frecuencia :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            cano = paramreporte.ObtenerParametros(1).ToString
    '            sector = paramreporte.ObtenerParametros(2).ToString
    '            control = paramreporte.ObtenerParametros(3).ToString
    '            frec = paramreporte.ObtenerParametros(4).ToString
    '            reporte.MostrarEnsayos(nro, cano, sector, control, frec, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub SetupMaquinaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetupMaquinaToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Setup de Maquina (Mandriles)"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarSetupDeMaquina(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ReporteDeFallasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteDeFallasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String
    '        nbreformreportes = "Reporte de Fallas"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarReportedeFallas(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub



    '    Private Function LlenarTmpEstadoCano(ByVal cod As String, ByVal op As String, ByVal cano As String) As Integer

    '        Dim connection As SqlClient.SqlConnection = Nothing

    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPERFO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End Try

    '            Try
    '                Dim param_cod As New SqlClient.SqlParameter
    '                param_cod.ParameterName = "@cod"
    '                param_cod.SqlDbType = SqlDbType.VarChar
    '                param_cod.Size = 15
    '                param_cod.Value = cod
    '                param_cod.Direction = ParameterDirection.Input

    '                Dim param_op As New SqlClient.SqlParameter
    '                param_op.ParameterName = "@op"
    '                param_op.SqlDbType = SqlDbType.VarChar
    '                param_op.Size = 10
    '                param_op.Value = op
    '                param_op.Direction = ParameterDirection.Input

    '                Dim param_cano As New SqlClient.SqlParameter
    '                param_cano.ParameterName = "@cano"
    '                param_cano.SqlDbType = SqlDbType.VarChar
    '                param_cano.Size = 10
    '                param_cano.Value = cano
    '                param_cano.Direction = ParameterDirection.Input

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spReporteEstadoDelCano", param_cod, param_op, param_cano)

    '                Catch ex As Exception
    '                    '' 
    '                    Throw ex
    '                End Try
    '            Finally
    '                ''
    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try
    '    End Function

    '    Private Function DevolverCodOrig(ByVal op As String, ByVal cano As String) As String
    '        DevolverCodOrig = ""
    '        Dim connection As SqlClient.SqlConnection = Nothing
    '        Dim cod As String=""

    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPERFO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                DevolverCodOrig = ""
    '                Exit Function
    '            End Try

    '            Try

    '                Dim param_op As New SqlClient.SqlParameter
    '                param_op.ParameterName = "@op"
    '                param_op.SqlDbType = SqlDbType.VarChar
    '                param_op.Size = 10
    '                param_op.Value = op
    '                param_op.Direction = ParameterDirection.Input

    '                Dim param_cano As New SqlClient.SqlParameter
    '                param_cano.ParameterName = "@cano"
    '                param_cano.SqlDbType = SqlDbType.VarChar
    '                param_cano.Size = 10
    '                param_cano.Value = cano
    '                param_cano.Direction = ParameterDirection.Input

    '                Dim param_cod As New SqlClient.SqlParameter
    '                param_cod.ParameterName = "@codorig"
    '                param_cod.SqlDbType = SqlDbType.VarChar
    '                param_cod.Size = 20
    '                param_cod.Value = cod
    '                param_cod.Direction = ParameterDirection.InputOutput

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_rpt_DevolverCodOrig", param_op, param_cano, param_cod)
    '                    DevolverCodOrig = param_cod.Value

    '                Catch ex As Exception
    '                    '' 
    '                    DevolverCodOrig = param_cod.Value
    '                    Throw ex
    '                End Try
    '            Finally
    '                ''
    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try
    '    End Function

    '    Private Sub EstadoDelCañoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstadoDelCañoToolStripMenuItem.Click

    '        Dim paramreporte As New frmParametros, paramreporte2 As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim Cnnrn As New SqlConnection(ConnStringPERFO)
    '        Dim op As String, cano As String, cod As String
    '        nbreformreportes = "Estado del Caño"

    '        Dim consulta As String


    '        paramreporte.AgregarParametros("Nro.Caño :", "STRING", "", True, "", "", Cnnrn)
    '        paramreporte.ShowDialog()

    '        cano = paramreporte.ObtenerParametros(0).ToString
    '        consulta = "select convert(varchar,op) + '-' + convert(varchar,idproducto) as op from canosfibra with(nolock) where idcano = " & cano

    '        paramreporte2.AgregarParametros("OP :", "STRING", "", True, "", consulta, Cnnrn)
    '        paramreporte2.ShowDialog()
    '        op = paramreporte2.ObtenerParametros(0).ToString
    '        op = Mid(op, 1, InStr(1, op, "-") - 1)

    '        If op.Trim = "" Or cano.Trim = "" Then
    '            MessageBox.Show("El número de Caño o de OP no existe.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Exit Sub
    '        End If

    '        cod = DevolverCodOrig(op, cano)

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            ''LLAMADA AL STORE
    '            LlenarTmpEstadoCano(cod, op, cano)
    '            reporte.MostrarReporteEstadoCano(reporte)

    '            'Dim fechaensayo = Nothing, fechaensayohist = Nothing, fechaensayoreing = Nothing, nrocano As String = Nothing
    '            'Dim archivo, archivo1, archivo2 As String

    '            'archivo = "c:\" + op.ToString

    '            'EstadodelCano_PDF(cano, fechaensayo, fechaensayohist, fechaensayoreing, nrocano)

    '            'archivo = archivo + fechaensayo + "_"

    '            'Select Case nrocano.Length
    '            '    Case 1
    '            '        archivo = archivo + "0000" + nrocano + ".pdf"
    '            '    Case 2
    '            '        archivo = archivo + "000" + nrocano + ".pdf"
    '            '    Case 3
    '            '        archivo = archivo + "00" + nrocano + ".pdf"
    '            '    Case 4
    '            '        archivo = archivo + "0" + nrocano + ".pdf"
    '            '    Case 5
    '            '        archivo = archivo + nrocano + ".pdf"
    '            'End Select

    '            'System.Diagnostics.Process.Start(archivo)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing

    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub TasaDeAprobacionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TasaDeAprobacionesToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String
    '        nbreformreportes = "Tasas de Aprobaciones "

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarTasadeAprobaciones(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ProducciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProducciónToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Producción"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStatusCanos(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub StatusDeCañosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusDeCañosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Status Caños"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStatusCanos(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub RegistroControlDeIngresoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegistroControlDeIngresoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String
    '        nbreformreportes = "Registro Control de Ingreso"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarRegistroDeIngreso(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Sub RegControlIngresoRevestPorFechaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegControlIngresoRevestPorFechaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String


    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        nbreformreportes = "Registro Control de Ingreso Por Fecha"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        'paramreporte.AgregarParametros("Fecha :", "DATE", "", True, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            'Final = paramreporte.ObtenerParametros(1).ToString
    '            reporte.MostrarRegistroDeIngreso2(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ResumenCañosProcesadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResumenCañosProcesadosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Resumen Caños Procesados"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarResumenCañosProcesados(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ControlDeOpsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlDeOpsToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Control de Ops"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarControlDeOps(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub SalidaDeCañosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalidaDeCañosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Salida de Caños (Por re-proceso)"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarSalidadeCanos(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Sub SecuenciaPorPuestoYOPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SecuenciaPorPuestoYOPToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String, sector As String

    '        nbreformreportes = "Caños pasados por puesto"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Sector :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            sector = paramreporte.ObtenerParametros(1).ToString
    '            reporte.MostrarPasadosPorPuestoyOP(nro, sector, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub EspesorEmbulteRevestidoraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EspesorEmbulteRevestidoraToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Espesor Embulte Revestidora"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarEmbulteRevestidora(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub PesoCañosFibraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PesoCañosFibraToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Peso de Caños de Fibra"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarPesoCanos(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Function GenerarTablaTemporal(ByVal op As Long) As Integer

    '        Dim connection As SqlClient.SqlConnection = Nothing

    '        Try
    '            Try
    '                ConnStringPERFO_MULETO = ConnStringPERFO_MULETO & ";Connection Timeout=8000"
    '                connection = SqlHelper.GetConnection(ConnStringPERFO_MULETO)

    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                GenerarTablaTemporal = -1
    '                Exit Function
    '            End Try

    '            Try

    '                Dim param_op As New SqlClient.SqlParameter
    '                param_op.ParameterName = "@op"
    '                param_op.SqlDbType = SqlDbType.BigInt
    '                param_op.Value = op
    '                param_op.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@resultado"
    '                param_res.SqlDbType = SqlDbType.Bit
    '                param_res.Value = 0
    '                param_res.Direction = ParameterDirection.InputOutput

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBobinasConsumidasPorOP_A1", param_op, param_res)
    '                    GenerarTablaTemporal = param_res.Value

    '                Catch ex As Exception
    '                    '' 
    '                    GenerarTablaTemporal = -1
    '                    Throw ex
    '                End Try
    '            Finally
    '                ''
    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try
    '    End Function

    '    Private Sub InformeDeScrapToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformeDeScrapToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO_MULETO)
    '        Dim nro As String, res As Integer

    '        nbreformreportes = "Informe de Scrap"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            res = GenerarTablaTemporal(CType(nro, Long))

    '            reporte.MostrarInformeScrap(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub AnalisisDeFallasEnFlejesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalisisDeFallasEnFlejesToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String, falla As String

    '        nbreformreportes = "Analisis de fallas en flejes"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Cód.de Falla :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            falla = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarAnalisisFallasFlejes(nro, falla, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub OrdenesDeCortePendientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdenesDeCortePendientesToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim maq As String

    '        nbreformreportes = "Ordenes de Corte Pendientes"

    '        paramreporte.AgregarParametros("Maq.(FER/DAY):", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            maq = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarOCsPendientesRN(maq, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ListaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListaDeToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Lista de Flejes a Aprobar"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarListaDeFlejesAAprobar(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ListaDeFlejesAprobadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListaDeFlejesAprobadosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Lista de Flejes a Aprobados"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarListaDeFlejesAprobados(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ListaDeFlejesAprobadosProduccionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListaDeFlejesAprobadosProduccionToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Lista de Flejes a Aprobados Producción"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarListaDeFlejesAprobadosPRod(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ConsumoDeFlejesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumoDeFlejesToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Consumo de Flejes"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarConsumosFlejes(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ConsumoDeBobinasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumoDeBobinasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Consumo de Bobinas"

    '        paramreporte.AgregarParametros("Nro.OC :", "STRING", "", True, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarConsumosBobinas(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub OrdenDelEntryLineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdenDelEntryLineToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Orden de Flejes en Entry Line"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarFlejesPorOP(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub BookFibraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookFibraToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN_muleto As New SqlConnection(ConnStringPERFO_MULETO)
    '        Dim nro As String
    '        Dim res As Integer = 0, res2 As Integer = 0
    '        Dim mensaje As String = ""

    '        nbreformreportes = "Book de Fibra"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", False, "", "", CnnRN_muleto)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            'ejecutamos el store 1 y si devuelve -1 salimos...
    '            res = LlenarTmpBookFibra(nro, mensaje)
    '            If res = 1 Then
    '                'ejecutamos el store 2 y si devuelve -1 salimos...
    '                res2 = LlenarTmpBookFibra2b(nro, mensaje)
    '                If res2 = 1 Then
    '                    reporte.MostrarReporteBookFibra(nro, reporte)
    '                Else
    '                    MsgBox("Se produjo un error al tratar de ejecutar el procedimiento B.", MsgBoxStyle.Critical, "Atención")
    '                End If
    '            Else
    '                MsgBox("Se produjo un error al tratar de ejecutar el procedimiento A.", MsgBoxStyle.Critical, "Atención")
    '            End If
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default


    '    End Sub
    '    Public Function LlenarTmpBookFibra(ByVal op As String, ByRef men As String) As Integer

    '        Dim connection As SqlClient.SqlConnection = Nothing
    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPERFO_MULETO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End Try
    '            Try
    '                Dim param_op As New SqlClient.SqlParameter
    '                param_op.ParameterName = "@op"
    '                param_op.SqlDbType = SqlDbType.VarChar
    '                param_op.Size = 25
    '                param_op.Value = op
    '                param_op.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@res"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = 0
    '                param_res.Direction = ParameterDirection.Output

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Book2", param_op, param_res)
    '                    LlenarTmpBookFibra = param_res.Value

    '                Catch ex As Exception
    '                    Throw ex
    '                End Try
    '            Finally

    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex
    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While
    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try

    '        Exit Function

    '    End Function


    '    Public Function LlenarTmpBookFibra2b(ByVal op As String, ByRef men As String) As Integer

    '        Dim connection As SqlClient.SqlConnection = Nothing
    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPERFO_MULETO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End Try
    '            Try
    '                Dim param_op As New SqlClient.SqlParameter
    '                param_op.ParameterName = "@op"
    '                param_op.SqlDbType = SqlDbType.VarChar
    '                param_op.Size = 25
    '                param_op.Value = op
    '                param_op.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@res"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = 0
    '                param_res.Direction = ParameterDirection.Output

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Book2b", param_op, param_res)
    '                    LlenarTmpBookFibra2b = param_res.Value

    '                Catch ex As Exception
    '                    Throw ex
    '                End Try
    '            Finally

    '            End Try
    '        Catch ex As Exception

    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While


    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try

    '        Exit Function
    '    End Function


    '    Private Sub DescartesPorOPYFechaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DescartesPorOPYFechaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Descartes por OP"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4) ''"01/01/2006"
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            Inicial = paramreporte.ObtenerParametros(1).ToString
    '            Final = paramreporte.ObtenerParametros(2).ToString


    '            reporte.MostrarDescartesRuzzi(nro, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub RelacionBobinasCañosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RelacionBobinasCañosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Relacion Bobina Caños"


    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarRelacionBobinaCaño(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub DiferenciaMesa1ProbetasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiferenciaMesa1ProbetasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Diferencia Mesa 1 - Probetas"


    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarDiferenciaMesa1Probetas(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub DegradacionesXOPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DegradacionesXOPToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Degradaciones x OP"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarDegradadosPorOP(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub CañosRechazadosPorSectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CañosRechazadosPorSectorToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Degradaciones x OP"

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarRechazadosPorOP(nro, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub FlejesProcesadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlejesProcesadosToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        ''Dim nro As String

    '        nbreformreportes = "Flejes Procesados"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4) ''"01/01/2007"
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString


    '            reporte.MostrarFlejesProcesados(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ToneladasConsumidasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToneladasConsumidasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Flejes Consumidos"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4) ''"01/01/2007"
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            Inicial = paramreporte.ObtenerParametros(1).ToString
    '            Final = paramreporte.ObtenerParametros(2).ToString

    '            reporte.MostrarFlejesConsumidos(nro, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ToneladasEmbultadasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToneladasEmbultadasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String

    '        nbreformreportes = "Toneladas Embultadas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4) ''"01/01/2007"
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", True, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            Inicial = paramreporte.ObtenerParametros(1).ToString
    '            Final = paramreporte.ObtenerParametros(2).ToString

    '            reporte.MostrarTNEmbultadas(nro, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub StockCañosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockCañosToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Stock Caños"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarStockCaños(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''Private Sub StocksCañosDeFibraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StocksCañosDeFibraToolStripMenuItem.Click
    '    ''    Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    ''    nbreformreportes = "Stock Caños Fibra"
    '    ''    Dim rpt As New frmReportes
    '    ''    rpt.MostrarStockCañosFibra(rpt)
    '    ''    Cursor = System.Windows.Forms.Cursors.Default
    '    ''End Sub

    '    Private Sub ProducciónAñoFiscalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProducciónAñoFiscalToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim maq As String

    '        nbreformreportes = "Producción Año Fiscal"

    '        paramreporte.AgregarParametros("Maquina :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            maq = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarProduccionAnoFiscal(maq, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub InstrumentosDelLaboratorioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstrumentosDelLaboratorioToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Intrumentos empleados en el laboratorio"
    '        Dim rpt As New frmReportes
    '        rpt.MostrarInstrumentosLaboratorio(rpt)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ListaDeBobinasUsadasXOPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListaDeBobinasUsadasXOPToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim op As String

    '        nbreformreportes = "Bobinas usadas x OP"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", True, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            op = paramreporte.ObtenerParametros(0).ToString


    '            reporte.MostrarBobinasUsadasPorOP(op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub CañosPorMotivoDeFallaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CañosPorMotivoDeFallaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim nro As String
    '        Dim tipoprod As String

    '        nbreformreportes = "Caños Rechazados Mot Fallas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim tipoprodsql As String = "select DISTINCT TipoProducto from Disenos"
    '        ''COMBO PARAMETRO OCs frmParametros
    '        paramreporte.AgregarParametros("Tipo Prod :", "STRING", "", True, "", tipoprodsql, CnnRN)
    '        paramreporte.AgregarParametros("Nro.OP :", "STRING", "", False, "", "", CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            tipoprod = paramreporte.ObtenerParametros(0).ToString
    '            nro = paramreporte.ObtenerParametros(1).ToString
    '            Inicial = paramreporte.ObtenerParametros(2).ToString
    '            Final = paramreporte.ObtenerParametros(3).ToString

    '            reporte.MostrarCanosRechazadosMotFallasRev(tipoprod, nro, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub DistribucionEspesoresEntryLineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DistribucionEspesoresEntryLineToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim grado As String
    '        Dim espesor As String

    '        nbreformreportes = "Distribucion de Espesores Entry Line"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim calidad As String = "select distinct calidad from flejes where tienecanos=1 and stock=0 and Tipo='FLEJE'"
    '        Dim espesores As String = "SELECT DISTINCT Espesor FROM Bobinas WHERE Espesor <> 0 ORDER BY Espesor"
    '        ''COMBO PARAMETRO Distribucion de Espesores Entry Line frmParametros
    '        paramreporte.AgregarParametros("Grado :", "STRING", "", True, "", calidad, CnnRN)
    '        paramreporte.AgregarParametros("Espesor :", "STRING", "", True, "", espesores, CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            grado = paramreporte.ObtenerParametros(0).ToString
    '            espesor = paramreporte.ObtenerParametros(1).ToString
    '            Inicial = paramreporte.ObtenerParametros(2).ToString
    '            Final = paramreporte.ObtenerParametros(3).ToString

    '            reporte.MostrarDistribucionEspesorEntryLine(grado, espesor, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub DistribucionAnchosSlitterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DistribucionAnchosSlitterToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim grado As String
    '        Dim anch As String

    '        nbreformreportes = "Distribucion de Anchos Slitter"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        Dim anchos As String = "SELECT DISTINCT Ancho FROM Bobinas WHERE stock=0 and oc<>0 ORDER BY Ancho"
    '        Dim calidad As String = "select distinct calidad from bobinas where stock=0 and oc<>0"

    '        ''COMBO PARAMETRO Distribucion de Espesores Entry Line frmParametros
    '        paramreporte.AgregarParametros("Anchos :", "STRING", "", True, "", anchos, CnnRN)
    '        paramreporte.AgregarParametros("Grado :", "STRING", "", True, "", calidad, CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            anch = paramreporte.ObtenerParametros(0).ToString
    '            grado = paramreporte.ObtenerParametros(1).ToString
    '            Inicial = paramreporte.ObtenerParametros(2).ToString
    '            Final = paramreporte.ObtenerParametros(3).ToString

    '            reporte.MostrarDistribucionAnchosSlitter(anch, grado, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub DistribucionPesosEmbulteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DistribucionPesosEmbulteToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim grado As String
    '        Dim esp As String
    '        Dim diam As String

    '        nbreformreportes = "Distribucion Pesos Embulte"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        Dim diametro As String = "SELECT DISTINCT Diametro FROM Disenos WHERE TipoProducto = 'SEMIELAB' AND Diametro <> 0 ORDER BY diametro"
    '        Dim espesor As String = "SELECT DISTINCT Espesor FROM Bobinas WHERE Espesor <> 0 ORDER BY Espesor"
    '        Dim calidad As String = "select distinct calidad from bobinas where stock=0 and oc<>0"

    '        ''COMBO PARAMETRO Distribucion de Espesores Entry Line frmParametros
    '        paramreporte.AgregarParametros("Grado :", "STRING", "", True, "", calidad, CnnRN)
    '        paramreporte.AgregarParametros("Espesor :", "STRING", "", True, "", espesor, CnnRN)
    '        paramreporte.AgregarParametros("Diametro :", "STRING", "", True, "", diametro, CnnRN)
    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            grado = paramreporte.ObtenerParametros(0).ToString
    '            esp = paramreporte.ObtenerParametros(1).ToString
    '            diam = paramreporte.ObtenerParametros(2).ToString
    '            Inicial = paramreporte.ObtenerParametros(3).ToString
    '            Final = paramreporte.ObtenerParametros(4).ToString

    '            reporte.MostrarDistribucionPesosEmbulte(grado, esp, diam, Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'dg 22-09-2011
    '    Private Sub AnálisisDeOPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnálisisDeOPToolStripMenuItem.Click

    '        'reemplaza a los famosos reportes pedidos por alonso y sesarego...

    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        'Dim CnnRN_muleto As New SqlConnection(ConnStringPERFO_MULETO)
    '        Dim nro As String
    '        Dim res As Integer = 0


    '        nbreformreportes = "Análisis de Orden de Producción"

    '        paramreporte.AgregarParametros("Nro.de OP :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            nro = paramreporte.ObtenerParametros(0).ToString
    '            'ejecutamos el store y si devuelve -1 salimos...
    '            res = LlenarTablaAnalisisOp(nro)
    '            If res = 1 Then
    '                reporte.MostrarReporteAnalisisOp(reporte)
    '                cerroparametrosconaceptar = False
    '                paramreporte = Nothing
    '            Else
    '                MsgBox("Se produjo un error al tratar de ejecutar el procedimiento spAnalisisOP.", MsgBoxStyle.Critical, "Atención")
    '            End If
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'dg 22-09-2011
    '    Public Function LlenarTablaAnalisisOp(ByVal op As String) As Integer

    '        Dim connection As SqlClient.SqlConnection = Nothing
    '        Try
    '            Try
    '                'connection = SqlHelper.GetConnection(ConnStringPERFO_MULETO)
    '                connection = SqlHelper.GetConnection(ConnStringPERFO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Function
    '            End Try
    '            Try
    '                Dim param_op As New SqlClient.SqlParameter
    '                param_op.ParameterName = "@NUMEROOP"
    '                param_op.SqlDbType = SqlDbType.VarChar
    '                param_op.Size = 10
    '                param_op.Value = op
    '                param_op.Direction = ParameterDirection.Input

    '                Dim param_res As New SqlClient.SqlParameter
    '                param_res.ParameterName = "@RESULTADO"
    '                param_res.SqlDbType = SqlDbType.Int
    '                param_res.Value = 0
    '                param_res.Direction = ParameterDirection.Output

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spAnalisisOP", param_op, param_res)
    '                    LlenarTablaAnalisisOp = param_res.Value

    '                Catch ex As Exception
    '                    Throw ex
    '                End Try
    '            Finally

    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex
    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While
    '            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try

    '        Exit Function

    '    End Function

    '#Region "Menu TM3"

    '    ''SubMenu Maestros
    '    Private Sub MaestroDeDisenosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestroDeDisenosToolStripMenuItem.Click
    '        nbreformreportes = "Maestro de Diseños"
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        Dim reportemaestrodedisenos As New frmReportes
    '        reportemaestrodedisenos.MostrarMaestroDeDisenos(reportemaestrodedisenos)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub MaestroDeMotivosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestroDeMotivosToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Motivos"
    '        Dim reportemotivos As New frmReportes
    '        reportemotivos.MostrarMaestroDeMotivos(reportemotivos)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MaestroDeSectoresToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestroDeSectoresToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Sectores"
    '        Dim reportesectores As New frmReportes
    '        reportesectores.MostrarMaestroDeSectores(reportesectores)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MaestroTipoControlesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestroTipoControlesToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Tipo Controles"
    '        Dim reportetipocontroles As New frmReportes
    '        reportetipocontroles.MostrarMaestroTipoDeControles(reportetipocontroles)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MaestrosDeUnidadesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestrosDeUnidadesToolStripMenuItem.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Unidades"
    '        Dim reporteunidades As New frmReportes
    '        reporteunidades.MostrarMaestroDeUnidades(reporteunidades)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MaestroDiseñoFormasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestroDiseñoFormasToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Diseños"
    '        Dim reportemaestrodedisenosformas As New frmReportes
    '        reportemaestrodedisenosformas.MostrarMaestroDeDisenos(reportemaestrodedisenosformas)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MaestroDeMatPrimaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaestroDeMatPrimaToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Maestro de Materias Primas"
    '        Dim reportemaestrodeMP As New frmReportes
    '        reportemaestrodeMP.MostrarMaestroDeMP(reportemaestrodeMP)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''SubMenu Stocks
    '    Private Sub StockDeBobinasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeBobinasToolStripMenuItem1.Click
    '        Dim Periodo As String
    '        Dim paramreportestockbobinas As New frmParametros

    '        Dim reportestockdebob As New frmReportes
    '        nbreformreportes = "Stock Bobinas"
    '        Periodo = Mid(Now, 4, 2) & Mid(Now, 7, 4)
    '        paramreportestockbobinas.AgregarParametros("Periodo :", "INTEGER", "", False, Periodo.ToString)

    '        paramreportestockbobinas.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            Periodo = paramreportestockbobinas.ObtenerParametros(0)

    '            reportestockdebob.MostrarReporteStockBobinas(Periodo, reportestockdebob)
    '            cerroparametrosconaceptar = False
    '            paramreportestockbobinas = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub StockDeFlejesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeFlejesToolStripMenuItem1.Click
    '        Dim Periodo As String
    '        Dim paramreportestockflejes As New frmParametros

    '        Dim reportestockdefle As New frmReportes
    '        nbreformreportes = "Stock Flejes"
    '        Periodo = Mid(Now, 4, 2) & Mid(Now, 7, 4)

    '        paramreportestockflejes.AgregarParametros("Periodo :", "INTEGER", "", False, Periodo.ToString)

    '        paramreportestockflejes.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            Periodo = paramreportestockflejes.ObtenerParametros(0)
    '            reportestockdefle.MostrarReporteStockFlejes(Periodo, reportestockdefle)
    '            cerroparametrosconaceptar = False
    '            paramreportestockflejes = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub StockDeMateriaPrimaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDeMateriaPrimaToolStripMenuItem1.Click
    '        Dim Periodo As String
    '        Dim paramreportestockMP As New frmParametros
    '        Dim reportestockMP As New frmReportes
    '        nbreformreportes = "Stock Materia Prima"
    '        Periodo = Mid(Now, 4, 2) & Mid(Now, 7, 4)

    '        paramreportestockMP.AgregarParametros("Periodo :", "INTEGER", "", False, Periodo.ToString)

    '        paramreportestockMP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            Periodo = paramreportestockMP.ObtenerParametros(0)
    '            reportestockMP.MostrarReporteMP(Periodo, reportestockMP)
    '            cerroparametrosconaceptar = False
    '            paramreportestockMP = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub StockPaquetesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockPaquetesToolStripMenuItem1.Click
    '        Dim paramStockPaquetes As New frmParametros

    '        Dim diseno As String
    '        Dim estado As String
    '        Dim Periodo As String
    '        Dim consulta As String = "SELECT DISTINCT Codigo FROM Disenos ORDER BY Codigo"
    '        Dim reportestockpaq As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        nbreformreportes = "Stock Paquetes"
    '        Periodo = Mid(Now, 4, 2) & Mid(Now, 7, 4)

    '        paramStockPaquetes.AgregarParametros("Diseño :", "STRING", "", True, "", consulta, CnnTM3)
    '        paramStockPaquetes.AgregarParametros("Estado :", "STRING", "", False, "", "", CnnTM3)
    '        paramStockPaquetes.AgregarParametros("Periodo :", "INTEGER", "", False, Periodo.ToString)

    '        paramStockPaquetes.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            diseno = paramStockPaquetes.ObtenerParametros(0)
    '            estado = paramStockPaquetes.ObtenerParametros(1)
    '            Periodo = paramStockPaquetes.ObtenerParametros(2)
    '            reportestockpaq.MostrarReporteStockPaquetes(diseno, estado, Periodo, reportestockpaq)
    '            cerroparametrosconaceptar = False
    '            paramStockPaquetes = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub StockPaquetesTerminacionesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockPaquetesTerminacionesToolStripMenuItem1.Click
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        nbreformreportes = "Terminación de Paquetes"
    '        Dim reporteTerminacion As New frmReportes
    '        reporteTerminacion.MostrarTerminaciones(reporteTerminacion)
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''SubMenu Produccion
    '    Private Sub ReporteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteToolStripMenuItem.Click
    '        Dim paramreporteoc As New frmParametros
    '        Dim reporteoc As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim oc As String
    '        Dim sqloc As String = "select ID from ocs"
    '        nbreformreportes = "Orden de Corte"

    '        ''COMBO PARAMETRO OCs frmParametros
    '        paramreporteoc.AgregarParametros("OCs :", "STRING", "", True, "", sqloc, CnnTM3)

    '        ''Muestra el Formulario de Filtro En Pantalla
    '        paramreporteoc.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            oc = paramreporteoc.ObtenerParametros(0)
    '            ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '            ''RESULTADO LO MUESTRE EN EL REPORTE
    '            reporteoc.ObtenerResultadoReporteOC(oc, reporteoc)

    '            cerroparametrosconaceptar = False
    '            paramreporteoc = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub AsociacionControlesMPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AsociacionControlesMPToolStripMenuItem1.Click
    '        Dim paramAsociacionControlesMP As New frmParametros
    '        Dim reporteasoccontrolesMP As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim codigo As String
    '        Dim consulta As String = "SELECT DISTINCT Codigo FROM MateriaPrima ORDER BY Codigo"
    '        nbreformreportes = "Asociacion Controles Materia Prima"
    '        paramAsociacionControlesMP.AgregarParametros("Codigo MP :", "STRING", "", True, "", consulta, CnnTM3)

    '        ''Muestra el Formulario de Filtro En Pantalla
    '        paramAsociacionControlesMP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            codigo = paramAsociacionControlesMP.ObtenerParametros(0)

    '            ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '            ''RESULTADO LO MUESTRE EN EL REPORTE
    '            reporteasoccontrolesMP.MostrarReporteAsociacionControlesMP(codigo, reporteasoccontrolesMP)
    '            cerroparametrosconaceptar = False
    '            paramAsociacionControlesMP = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub AsociacionControlesPaquetesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AsociacionControlesPaquetesToolStripMenuItem.Click
    '        Dim paramAsociacionControlesPaquetes As New frmParametros
    '        Dim reporteasoccontrolespaq As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim codigo As String
    '        Dim consulta As String = "SELECT DISTINCT Codigo FROM Disenos ORDER BY Codigo"
    '        nbreformreportes = "Asociacion Controles Paquetes"

    '        paramAsociacionControlesPaquetes.AgregarParametros("Diseño :", "STRING", "", True, "", consulta, CnnTM3)

    '        ''Muestra el Formulario de Filtro En Pantalla
    '        paramAsociacionControlesPaquetes.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            codigo = paramAsociacionControlesPaquetes.ObtenerParametros(0)

    '            ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '            ''RESULTADO LO MUESTRE EN EL REPORTE
    '            reporteasoccontrolespaq.MostrarReporteAsociacionControlesPaquetes(codigo, reporteasoccontrolespaq)
    '            cerroparametrosconaceptar = False
    '            paramAsociacionControlesPaquetes = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub FlejesConsumidosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlejesConsumidosToolStripMenuItem1.Click
    '        Dim paramFlejesConsumidos As New frmParametros
    '        Dim op As String
    '        Dim reporteflejescons As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim consulta As String = " Select Distinct IdOP FROM Flejes order by idop desc"
    '        nbreformreportes = "Flejes Consumidos"

    '        paramFlejesConsumidos.AgregarParametros("OP :", "STRING", "", True, "", consulta, CnnTM3)

    '        paramFlejesConsumidos.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramFlejesConsumidos.ObtenerParametros(0)

    '            ''LLAMADA A PROCEDIMIENTO QUE LLAMA A STORE QUE GENERA LA TABLA (tmpAnalisisRendimientoRPT2) QUE LUEGO ES USADA POR EL REPORTE
    '            reporteflejescons.MostrarFlejesConsumidos(op, reporteflejescons)
    '            cerroparametrosconaceptar = False
    '            paramFlejesConsumidos = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub EnsayosMateriaPrimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnsayosMateriaPrimaToolStripMenuItem.Click
    '        Dim paramEnsayosMP As New frmParametros
    '        Dim codmp As String
    '        Dim reporteensayosMP As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim consulta As String = "SELECT DISTINCT Codigo FROM MateriaPrima ORDER BY Codigo"
    '        nbreformreportes = "Ensayos Materia Prima"

    '        paramEnsayosMP.AgregarParametros("Codigo MP :", "STRING", "", True, "", consulta, CnnTM3)

    '        paramEnsayosMP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            codmp = paramEnsayosMP.ObtenerParametros(0)
    '            reporteensayosMP.MostrarEnsayosMP(codmp, reporteensayosMP)
    '            cerroparametrosconaceptar = False
    '            paramEnsayosMP = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub EnsayosPaquetesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnsayosPaquetesToolStripMenuItem1.Click
    '        Dim paramEnsayosPaq As New frmParametros
    '        Dim op As String
    '        Dim paq As String
    '        Dim reporteensayospaq As New frmReportes
    '        nbreformreportes = "Ensayos Paquetes"
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)


    '        Dim consultaop As String = "SELECT DISTINCT idop FROM EnsayosPaquetes ORDER BY idop desc"
    '        Dim consultapaq As String = "SELECT DISTINCT idpaquete FROM EnsayosPaquetes ORDER BY idpaquete desc"

    '        paramEnsayosPaq.AgregarParametros("OP :", "STRING", "", True, "", consultaop, CnnTM3)
    '        paramEnsayosPaq.AgregarParametros("Paquete :", "STRING", "", True, "", consultapaq, CnnTM3)

    '        paramEnsayosPaq.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramEnsayosPaq.ObtenerParametros(0)
    '            paq = paramEnsayosPaq.ObtenerParametros(1)
    '            ''LLAMADA A PROCEDIMIENTO QUE LLAMA A STORE QUE GENERA LA TABLA (tmpAnalisisRendimientoRPT2) QUE LUEGO ES USADA POR EL REPORTE
    '            reporteensayospaq.MostrarEnsayosPaquetes(op, paq, reporteensayospaq)
    '            cerroparametrosconaceptar = False
    '            paramEnsayosPaq = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub IngresoDeMPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IngresoDeMPToolStripMenuItem1.Click
    '        Dim idingreso As String
    '        Dim paramIngresoDeMP As New frmParametros
    '        Dim reporteingresoMP As New frmReportes

    '        nbreformreportes = "Ingreso Materia Prima"

    '        paramIngresoDeMP.AgregarParametros("Nro Ingreso MP :", "LONG", "", True, "")

    '        paramIngresoDeMP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            idingreso = paramIngresoDeMP.ObtenerParametros(0)

    '            reporteingresoMP.MostrarIngresoMP(idingreso, reporteingresoMP)
    '            cerroparametrosconaceptar = False
    '            paramIngresoDeMP = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub GruposDeOPsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GruposDeOPsToolStripMenuItem1.Click
    '        Dim paramGruposDeOPs As New frmParametros
    '        Dim op As String
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim reportegruposdeops As New frmReportes

    '        nbreformreportes = "Grupos de OPs"
    '        paramGruposDeOPs.AgregarParametros("OP :", "LONG", "", True, "", "", CnnTM3)

    '        paramGruposDeOPs.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramGruposDeOPs.ObtenerParametros(0)
    '            reportegruposdeops.MostrarGruposDeOPs(op, reportegruposdeops)
    '            cerroparametrosconaceptar = False
    '            paramGruposDeOPs = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub FallasPaquetesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FallasPaquetesToolStripMenuItem1.Click
    '        Dim paramFallaPaquetes As New frmParametros
    '        Dim op As String
    '        Dim consulta As String = "SELECT DISTINCT IDOP FROM PaquetesMotivos ORDER BY idop desc"

    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim reportefallaspaq As New frmReportes
    '        nbreformreportes = "Fallas Paquetes"

    '        paramFallaPaquetes.AgregarParametros("OP :", "STRING", "", True, "", consulta, CnnTM3)

    '        paramFallaPaquetes.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramFallaPaquetes.ObtenerParametros(0)
    '            reportefallaspaq.MostrarFallasPaquetes(op, reportefallaspaq)
    '            cerroparametrosconaceptar = False
    '            paramFallaPaquetes = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub ProduccionPaquetesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProduccionPaquetesToolStripMenuItem1.Click
    '        Dim paramProduccionPaquetes As New frmParametros
    '        Dim op As String
    '        Dim fechadesde As String
    '        Dim fechahasta As String
    '        Dim reporteprodpaq As New frmReportes

    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        nbreformreportes = "Produccion Paquetes"

    '        ''En esta Variable le paso el primer dia del mes actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4) ''"01/01/2008"
    '        ''En esta Variable le paso la fecha actual
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        Dim consulta As String = "SELECT DISTINCT ID FROM Ops ORDER BY ID DESC"

    '        paramProduccionPaquetes.AgregarParametros("OP :", "STRING", "", False, "", "", CnnTM3)

    '        paramProduccionPaquetes.AgregarParametros("Fecha Desde :", "DATE", "", False, Inicial)
    '        paramProduccionPaquetes.AgregarParametros("Fecha Hasta :", "DATE", "", False, Final)

    '        paramProduccionPaquetes.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramProduccionPaquetes.ObtenerParametros(0)

    '            fechadesde = paramProduccionPaquetes.ObtenerParametros(1).ToUpper

    '            fechahasta = paramProduccionPaquetes.ObtenerParametros(2)
    '            reporteprodpaq.MostrarProduccionPaquetes(op, fechadesde, fechahasta, reporteprodpaq)
    '            cerroparametrosconaceptar = False
    '            paramProduccionPaquetes = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub PaquetesPorOPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaquetesPorOPToolStripMenuItem1.Click
    '        Dim parampaqporop As New frmParametros
    '        Dim reportepaqporop As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim op As String
    '        ''SELECT PARA PASARLE LOS DATOS AL COMBOBOX
    '        Dim sqlop As String = "select DISTINCT idop from PaquetesTm3"
    '        nbreformreportes = "Paquetes por OP"

    '        ''COMBO PARAMETRO OCs frmParametros
    '        parampaqporop.AgregarParametros("Orden Prod. :", "STRING", "", True, "", "", CnnTM3)

    '        ''Muestra el Formulario de Filtro En Pantalla
    '        parampaqporop.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = parampaqporop.ObtenerParametros(0)
    '            ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '            ''RESULTADO LO MUESTRE EN EL REPORTE
    '            reportepaqporop.ObtenerPaquetesPorOP(op, reportepaqporop)
    '            cerroparametrosconaceptar = False
    '            parampaqporop = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub FlejesPorOPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlejesPorOPToolStripMenuItem1.Click
    '        Dim paramfleporop As New frmParametros
    '        ''NUEVO MS 02-07-2010
    '        Dim reportefleporop As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim op As String
    '        ''SELECT PARA PASARLE LOS DATOS AL COMBOBOX
    '        Dim sqlop As String = "select DISTINCT idop from FLEJES"
    '        nbreformreportes = "Paquetes por OP"

    '        ''COMBO PARAMETRO OCs frmParametros
    '        paramfleporop.AgregarParametros("Orden Prod. :", "STRING", "", True, "", "", CnnTM3)

    '        ''Muestra el Formulario de Filtro En Pantalla
    '        paramfleporop.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramfleporop.ObtenerParametros(0)
    '            ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '            ''RESULTADO LO MUESTRE EN EL REPORTE
    '            reportefleporop.ObtenerFlejesPorOP(op, reportefleporop)
    '            cerroparametrosconaceptar = False
    '            paramfleporop = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub OrdenDeCortePendienteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdenDeCortePendienteToolStripMenuItem1.Click
    '        Dim paramocpendiente As New frmParametros
    '        Dim reporteocpendiente As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim ocpendiente As String
    '        ''SELECT PARA PASARLE LOS DATOS AL COMBOBOX
    '        Dim sqlocpendiente As String = "select	O.id from ocs O left join(select oc,count(oc) as tot from bobinas where oc <> 0 group by oc) A on A.oc = O.id"
    '        nbreformreportes = "Orden de Corte Pendiente"

    '        ''COMBO PARAMETRO OCs frmParametros
    '        paramocpendiente.AgregarParametros("OCs :", "STRING", "", True, "", sqlocpendiente, CnnTM3)
    '        ''Muestra el Formulario de Filtro En Pantalla
    '        paramocpendiente.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            ocpendiente = paramocpendiente.ObtenerParametros(0)
    '            ''PASO LOS PARAMETROS AL STORE PARA QUE FILTRE POR ESTOS PARAMETROS Y AL
    '            ''RESULTADO LO MUESTRE EN EL REPORTE
    '            reporteocpendiente.ObtenerResultadoReporteOCPendiente(ocpendiente, reporteocpendiente)
    '            cerroparametrosconaceptar = False
    '            paramocpendiente = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub FlejesBasePorOPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlejesBasePorOPToolStripMenuItem1.Click
    '        Dim paramFlejesBaseOP As New frmParametros
    '        Dim op As String
    '        Dim reporteflejesbaseop As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim consulta As String = "SELECT DISTINCT ID FROM Ops ORDER BY ID DESC"

    '        nbreformreportes = "Flejes Base por OP"
    '        paramFlejesBaseOP.AgregarParametros("OP :", "STRING", "", True, "", consulta, CnnTM3)


    '        paramFlejesBaseOP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramFlejesBaseOP.ObtenerParametros(0)
    '            reporteflejesbaseop.MostrarFlejesBaseOP(op, reporteflejesbaseop)
    '            cerroparametrosconaceptar = False
    '            paramFlejesBaseOP = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub FlejesOptimosPorOPToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlejesOptimosPorOPToolStripMenuItem1.Click
    '        Dim paramFlejesOptimos As New frmParametros
    '        Dim op As String
    '        Dim reporteflejesoptimos As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim consulta As String = "SELECT DISTINCT ID FROM Ops ORDER BY ID DESC"

    '        nbreformreportes = "Flejes Optimos por OP"
    '        paramFlejesOptimos.AgregarParametros("OP :", "STRING", "", True, "", consulta, CnnTM3)

    '        paramFlejesOptimos.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            op = paramFlejesOptimos.ObtenerParametros(0)
    '            reporteflejesoptimos.MostrarFlejesOptimosOP(op, reporteflejesoptimos)
    '            cerroparametrosconaceptar = False
    '            paramFlejesOptimos = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub EstadisticasDeFallaTM3ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstadisticasDeFallaTM3ToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim op As String
    '        nbreformreportes = "Estadísticas de falla"
    '        paramreporte.AgregarParametros("Orden de Prod. : ", "LONG", "", False, "")
    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString
    '            reporte.MostrarReporteEstadisticaFallas(op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub ScrapFlejesTM3ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScrapFlejesTM3ToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        nbreformreportes = "Scrap Flejes TM3 "

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnTM3)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            reporte.MostrarReporteScrapFlejesTM3(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub ScrapCañosTM3ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScrapCañosTM3ToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim Tipo As String


    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String = "select 'scrap' as nombre union all select 'segunda' as nombre"
    '        nbreformreportes = "Scrap Caños TM3"

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", CnnTM3)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", False, Final, "", CnnTM3)
    '        paramreporte.AgregarParametros("Tipo :", "STRING", "", True, "", consulta, CnnTM3)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            Tipo = paramreporte.ObtenerParametros(2).ToString
    '            reporte.MostrarReporteScrapCanosTM3(Inicial, Final, Tipo, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub ScrapTotalToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScrapTotalToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim solocerradas As String, desdeOP As String, hastaOP As String

    '        nbreformreportes = "Scrap por OP total (Graficos)"

    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4) ''"01/01/2008"
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim consulta As String = "select 'Sí' as Resp union all select 'No' as Resp"
    '        paramreporte.AgregarParametros("Fecha Desde :", "DATE", "", False, Inicial)
    '        paramreporte.AgregarParametros("Fecha Hasta :", "DATE", "", False, Final)
    '        paramreporte.AgregarParametros("¿Solo Cerradas? :", "STRING", "", True, "", consulta, CnnTM3)
    '        paramreporte.AgregarParametros("Desde OP :", "STRING", "", False, "", "")
    '        paramreporte.AgregarParametros("Hasta OP :", "STRING", "", False, "", "")


    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0)
    '            Final = paramreporte.ObtenerParametros(1)
    '            solocerradas = paramreporte.ObtenerParametros(2)
    '            desdeOP = paramreporte.ObtenerParametros(3)
    '            hastaOP = paramreporte.ObtenerParametros(4)

    '            reporte.ObtenerScrapTotalTm3(Inicial, Final, solocerradas, desdeOP, hastaOP, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub BobinasFlejadasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BobinasFlejadasToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim op As String ', maquina As String, automatico As Boolean
    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        nbreformreportes = "Bobinas Flejadas Entre Fechas"

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", CnnTM3)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", True, Final, "", CnnTM3)
    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnTM3)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            op = paramreporte.ObtenerParametros(2).ToString
    '            reporte.MostrarReporteBobinasFlejadasEntreFechas(Inicial, Final, op, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''SubMenu Movimientos
    '    Private Sub MovimientosDeStockDePaquetesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosDeStockDePaquetesToolStripMenuItem.Click
    '        Dim paramMovDeStock As New frmParametros
    '        Dim id As String
    '        Dim fechadesde As String
    '        Dim reportemovdestock As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        nbreformreportes = "Movimiento de Stock"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        paramMovDeStock.AgregarParametros("ID Mov :", "STRING", "", False, "", "", CnnTM3)
    '        paramMovDeStock.AgregarParametros("Fecha Desde :", "DATE", "", False, Final)

    '        paramMovDeStock.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            id = paramMovDeStock.ObtenerParametros(0)
    '            fechadesde = paramMovDeStock.ObtenerParametros(1).ToUpper
    '            reportemovdestock.MostrarMovDeStock(id, fechadesde, reportemovdestock)
    '            cerroparametrosconaceptar = False
    '            paramMovDeStock = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MovimientosDeStockBobinasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosDeStockBobinasToolStripMenuItem1.Click
    '        Dim paramMovDeStockBob As New frmParametros
    '        Dim id As String
    '        Dim fechadesde As String
    '        Dim reportemovdestockbob As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        ''En esta Variable le paso la fecha actual
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        nbreformreportes = "Movimiento de Stock Bobinas"
    '        paramMovDeStockBob.AgregarParametros("ID Mov :", "STRING", "", False, "", "", CnnTM3)
    '        paramMovDeStockBob.AgregarParametros("Fecha Desde :", "DATE", "", False, Final)

    '        paramMovDeStockBob.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            id = paramMovDeStockBob.ObtenerParametros(0)
    '            fechadesde = paramMovDeStockBob.ObtenerParametros(1).ToUpper
    '            reportemovdestockbob.MostrarMovDeStockBobinas(id, fechadesde, reportemovdestockbob)
    '            cerroparametrosconaceptar = False
    '            paramMovDeStockBob = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MovimientosDeStockFlejesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosDeStockFlejesToolStripMenuItem.Click
    '        Dim paramMovDeStockFlejes As New frmParametros
    '        Dim id As String
    '        Dim fechadesde As String
    '        Dim reportemovstockfle As New frmReportes
    '        nbreformreportes = "Movimiento de Stock Flejes"
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramMovDeStockFlejes.AgregarParametros("ID Mov :", "STRING", "", False, "", "", CnnTM3)
    '        paramMovDeStockFlejes.AgregarParametros("Fecha Desde :", "DATE", "", False, Final)

    '        paramMovDeStockFlejes.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            id = paramMovDeStockFlejes.ObtenerParametros(0)
    '            fechadesde = paramMovDeStockFlejes.ObtenerParametros(1).ToUpper
    '            reportemovstockfle.MostrarMovDeStockFlejes(id, fechadesde, reportemovstockfle)
    '            cerroparametrosconaceptar = False
    '            paramMovDeStockFlejes = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub MovimientosDeStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosDeStockToolStripMenuItem.Click
    '        Dim paramMovDeStockMP As New frmParametros
    '        Dim id As String
    '        Dim fechadesde As String
    '        Dim reportemovstockMP As New frmReportes
    '        nbreformreportes = "Movimiento de Stock Flejes"
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        paramMovDeStockMP.AgregarParametros("ID Mov :", "STRING", "", False, "", "", CnnTM3)
    '        paramMovDeStockMP.AgregarParametros("Fecha Desde :", "DATE", "", False, Final)

    '        paramMovDeStockMP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        ''cerroparametrosconaceptar VARIABLE GLOBAL PARA SABER SI EL FORMULARIO PARAMETROS
    '        ''SE CERRO DESDE EL BOTON Cerrar o DESDE EL BOTON ACEPTAR
    '        ''SIEMPRE QUE SE UTILICEN PARAMETROS USAR EL IF DE MAS ABAJO
    '        If cerroparametrosconaceptar = True Then
    '            ''OBTENGO LOS PARAMETROS QUE LE VOY A PASAR AL STORE POR MEDIO DEL REPORTE
    '            id = paramMovDeStockMP.ObtenerParametros(0)
    '            fechadesde = paramMovDeStockMP.ObtenerParametros(1).ToUpper
    '            reportemovstockMP.MostrarMovDeStockMP(id, fechadesde, reportemovstockMP)
    '            cerroparametrosconaceptar = False
    '            paramMovDeStockMP = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''SubMenu Despachos
    '    Private Sub PaquetesDespachadosToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaquetesDespachadosToolStripMenuItem2.Click
    '        'Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        'Dim paramPaquetesDespachados As New frmParametros

    '        'Dim reportePaquetesDespachados As New frmReportes
    '        'nbreformreportes = "Paquetes Despachados"
    '        'Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        'reportePaquetesDespachados.MostrarPaquetesDespachados(reportePaquetesDespachados, CnnTM3)
    '        'cerroparametrosconaceptar = False
    '        'paramPaquetesDespachados = Nothing
    '        'CnnTM3 = Nothing
    '        'Cursor = System.Windows.Forms.Cursors.Default

    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim sucursal As String
    '        Dim remito As String
    '        Dim codigo As String

    '        nbreformreportes = "Degradaciones x OP"

    '        paramreporte.AgregarParametros("Nro.Sucursal :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Nro.Remito :", "STRING", "", False, "", "", CnnTM3)
    '        paramreporte.AgregarParametros("Código de Diseño:", "STRING", "", False, "", "", CnnTM3)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            sucursal = paramreporte.ObtenerParametros(0).ToString
    '            remito = paramreporte.ObtenerParametros(1).ToString
    '            codigo = paramreporte.ObtenerParametros(2).ToString

    '            reporte.MostrarPaquetesDespachadosTM3(sucursal, remito, codigo, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub PaquetesParaDespacharToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaquetesParaDespacharToolStripMenuItem2.Click
    '        Dim paramPaquetesDespachados As New frmParametros

    '        Dim sNivel As String

    '        Dim reportePaquetesDespachados As New frmReportes
    '        nbreformreportes = "Paquetes para Despachar"

    '        Dim CnnTM3 As New SqlConnection(ConnStringUSUARIOS)

    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)


    '        paramPaquetesDespachados.AgregarParametros("Nivel", "STRING", "", False, Final, "select nivel from disenos group by nivel UNION ALL select 'PRIMERA TERMINACION' UNION ALL SELECT 'TODOS'", CnnTM3)


    '        paramPaquetesDespachados.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            sNivel = paramPaquetesDespachados.ObtenerParametros(0)
    '            reportePaquetesDespachados.MostrarPaquetesParaDespachar(sNivel, reportePaquetesDespachados, CnnTM3)
    '            cerroparametrosconaceptar = False
    '            paramPaquetesDespachados = Nothing
    '            CnnTM3 = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '#End Region
    '#Region "Menu TM3"
    '    ''MS 06-10-2011
    '    Private Sub CanosIngresadosATrefiladoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanosIngresadosATrefiladoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)

    '        nbreformreportes = "Canos Ingresados a Fase trefilado Entre Fechas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim tipoprodsql As String = "select DISTINCT TipoProducto from Disenos"

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarCanosIngresadosTrefiladoRI(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default

    '    End Sub
    '    ''MS 06-10-2011
    '    Private Sub DetalleTiempoDeEstibadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetalleTiempoDeEstibadoToolStripMenuItem.Click
    '        Dim paramOP As New frmParametros
    '        Dim op As String

    '        Dim reporteTiempoDeEstibado As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        nbreformreportes = "Detalle Tiempo de Estibado"
    '        Dim consulta As String = "select o.op from OPs o inner join Disenos d on o.IDDiseno=d.IDDiseno where d.tipoproducto='RI' order by o.op desc"

    '        paramOP.AgregarParametros("OP :", "STRING", "", True, "", consulta, CnnRN)

    '        paramOP.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            op = paramOP.ObtenerParametros(0)
    '            reporteTiempoDeEstibado.MostrarDetalleTiempoDeEstibado(op, reporteTiempoDeEstibado)
    '            cerroparametrosconaceptar = False
    '            paramOP = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default

    '    End Sub
    '#End Region

    '    Private Sub EtiquetaResiduosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetaResiduosToolStripMenuItem.Click
    '        'Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        'nbreformreportes = "Etiquetas de Residuos"
    '        'Dim rpt As New frmReportes
    '        'rpt.MostrarEtiquetasResiduos(rpt)
    '        'Cursor = System.Windows.Forms.Cursors.Default
    '        frmEtiquetas.MdiParent = Me
    '        frmEtiquetas.Show()
    '    End Sub
    '    ''NUEVO MS 13-10-2011
    '    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim diseno As String

    '        nbreformreportes = "Stock Caños Fibra"
    '        Dim consulta As String = "SELECT IDDiseno FROM Disenos WHERE (TipoProducto = 'FIBRA') AND (Eliminado = 0) ORDER BY IDDiseno"

    '        paramreporte.AgregarParametros("Diseño :", "STRING", "", False, "", consulta, CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            diseno = paramreporte.ObtenerParametros(0).ToString

    '            ''RN_NET_StockCanosFibraFiltroDiseno
    '            reporte.MostrarStockCanosFibraFiltroDiseno(diseno, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub EtiquetasCouplingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetasCouplingToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim Cnntm3 As New SqlConnection(ConnStringUSUARIOS)
    '        Dim rpt As New frmReportes
    '        Dim colada As String = "", qty As String = ""

    '        nbreformreportes = "Etiquetas de Coupling"
    '        paramreporte.AgregarParametros("Nro. de Colada", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.AgregarParametros("Cantidad de Etiquetas", "STRING", "", False, "", "", Cnntm3)
    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then

    '            colada = paramreporte.ObtenerParametros(0).ToString
    '            qty = paramreporte.ObtenerParametros(1).ToString

    '            rpt.EtiquetasCupling(rpt, colada, qty)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If

    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub AvisosDeRiesgoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AvisosDeRiesgoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnSeguridad As New SqlConnection(ConnStringSeguridad)

    '        Dim codigo_avisos As String, gravedad As String, probabilidad As String, gerencia As String, condicion As String, sector As String, lugar As String

    '        Dim consulta_gravedad As String = "select distinct gravedad from avisos order by gravedad"
    '        Dim consulta_probabilidad As String = "select distinct probabilidad from avisos order by probabilidad"
    '        Dim consulta_gerencia As String = "select nombre from gerencias"
    '        Dim consulta_condicion As String = "select nombre from condiciones"
    '        Dim consulta_sector As String = "select nombre from Sectores"
    '        Dim consulta_lugar As String = "SELECT DISTINCT Nombre FROM Lugares"

    '        paramreporte.AgregarParametros("Codigo_avisos :", "STRING", "", False, "", "", CnnSeguridad)
    '        paramreporte.AgregarParametros("Gravedad :", "STRING", "", False, "", consulta_gravedad, CnnSeguridad)
    '        paramreporte.AgregarParametros("Probabilidad :", "STRING", "", False, "", consulta_probabilidad, CnnSeguridad)
    '        paramreporte.AgregarParametros("Gerencia :", "STRING", "", False, "", consulta_gerencia, CnnSeguridad)
    '        paramreporte.AgregarParametros("Condicion :", "STRING", "", False, "", consulta_condicion, CnnSeguridad)
    '        paramreporte.AgregarParametros("Sector :", "STRING", "", False, "", consulta_sector, CnnSeguridad)
    '        paramreporte.AgregarParametros("Lugar :", "STRING", "", False, "", consulta_lugar, CnnSeguridad)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos Polene"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            codigo_avisos = paramreporte.ObtenerParametros(0).ToString
    '            gravedad = paramreporte.ObtenerParametros(1).ToString
    '            probabilidad = paramreporte.ObtenerParametros(2).ToString
    '            gerencia = paramreporte.ObtenerParametros(3).ToString
    '            condicion = paramreporte.ObtenerParametros(4).ToString
    '            sector = paramreporte.ObtenerParametros(5).ToString
    '            lugar = paramreporte.ObtenerParametros(6).ToString


    '            rpt.AvisosDeRiesgo(codigo_avisos, gravedad, probabilidad, gerencia, condicion, sector, lugar, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ConsumosAgrupadosPorFamiliaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosAgrupadosPorFamiliaToolStripMenuItem.Click

    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanolNet)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Codfamilia As String

    '        Dim consulta_familia As String = "select codigo + ' - ' + nombre from familias where eliminado = 0 order by codigo"

    '        paramreporte.AgregarParametros("Cód.Familia :", "STRING", "", False, "", consulta_familia, CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnPanol)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos Agrupados por Familia"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            Codfamilia = Mid(paramreporte.ObtenerParametros(0).ToString, 1, 10)

    '            Inicial = paramreporte.ObtenerParametros(1).ToString
    '            Final = paramreporte.ObtenerParametros(2).ToString

    '            rpt.MostrarMaestroConsumosPorFamilia(Codfamilia, Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub EtiquetaPaqueteFibraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetaPaqueteFibraToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim paquete As String

    '        nbreformreportes = "Etiqueta Paquete Fibra"

    '        paramreporte.AgregarParametros("Paquete :", "STRING", "", False, "", "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            paquete = paramreporte.ObtenerParametros(0).ToString

    '            reporte.MostrarEtiquetaPaquetesFibra(paquete, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub ConsumosDetalladosSistemaAnteriorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumosDetalladosSistemaAnteriorToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanol)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim numeroconsumo As String, codigomaterial As String, cc As String

    '        paramreporte.AgregarParametros("N. de Consumo :", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("Cód. de Material:", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("Cód. CC:", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnPanol)


    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos detallados Sistema Anterior"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            numeroconsumo = paramreporte.ObtenerParametros(0).ToString
    '            codigomaterial = paramreporte.ObtenerParametros(1).ToString
    '            cc = paramreporte.ObtenerParametros(2).ToString
    '            Inicial = paramreporte.ObtenerParametros(3).ToString
    '            Final = paramreporte.ObtenerParametros(4).ToString

    '            rpt.MostrarMaestroConsumosSistemaAnterior(numeroconsumo, codigomaterial, cc, Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub




    '    Private Sub MovimientosSistemaAnteriorToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosSistemaAnteriorToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanol)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim codigomaterial As String


    '        paramreporte.AgregarParametros("Cód. de Material:", "STRING", "", False, "", "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnPanol)


    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Movimientos Sistema Anterior"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            codigomaterial = paramreporte.ObtenerParametros(0).ToString
    '            Inicial = paramreporte.ObtenerParametros(1).ToString
    '            Final = paramreporte.ObtenerParametros(2).ToString

    '            'rpt.MostrarMaestroConsumosSistemaAnterior(codigomaterial, Inicial, Final, rpt)
    '            rpt.MostrarMaestroMovimientosSistemaAnterior(codigomaterial, Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    Private Sub StockFitingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockFitingToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes

    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)

    '        nbreformreportes = "Stock Fitting"

    '        Dim tipo As String = "select 'CODOS' as CODOS union all select 'BRIDAS' as BRIDAS union all select 'NIPLES' as NIPLES"

    '        paramreporte.AgregarParametros("Tipo Producto :", "STRING", "", True, "", tipo, CnnRN)

    '        paramreporte.ShowDialog()

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            tipo = paramreporte.ObtenerParametros(0).ToString

    '            ''reporte.MostrarReporteControlCalidadGraficos(Inicial, Final, op, reporte, maquina)
    '            reporte.MostrarReporteStockFiting(reporte, tipo)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing

    '        End If

    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    ''nuevo MS 16-02-2012
    '    Private Sub EspesoresTMCLABToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EspesoresTMCLABToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)
    '        Dim consulta As String
    '        Dim op As String

    '        consulta = "SELECT top 100 O.op FROM OPs o inner join Disenos d on o.iddiseno=d.iddiseno WHERE (d.TIPOPRODUCTO = 'fibra') order by O.dateadd desc"

    '        paramreporte.AgregarParametros("OP :", "STRING", "", False, "", consulta, CnnRN)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Reporte de Espesores TMC"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            op = paramreporte.ObtenerParametros(0).ToString
    '            rpt.MostrarReporteEspesoresTMC(op, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    ''fin nuevo
    '    Private Sub FactorDeConsumoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FactorDeConsumoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnRN)


    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Factor de Consumo Mezcla"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            rpt.MostrarReporteFactorDeConsumo(Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub CanosIngresadosAMecanizadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanosIngresadosAMecanizadoToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim reporte As New frmReportes
    '        Dim CnnRN As New SqlConnection(ConnStringPERFO)

    '        nbreformreportes = "Canos Ingresados a Fase Mecanizado o Terminado Entre Fechas"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        ''Dim tipoprodsql As String = "select DISTINCT TipoProducto from Disenos"

    '        paramreporte.AgregarParametros("Desde :", "DATE", "", False, Inicial, "", CnnRN)
    '        paramreporte.AgregarParametros("Hasta :", "DATE", "", False, Final, "", CnnRN)

    '        paramreporte.ShowDialog()
    '        Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            reporte.MostrarCanosIngresadosMecanizadoRI(Inicial, Final, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    ' 	'AL - 24-01-2012
    '    'Reporte para obtener los consumos de EEP en el sistema nuevo de Panol
    '    'Solicitado por Cristian Lucero
    '    Private Sub ConsumoDeEPPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumoDeEPPToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanol)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnPanol)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos de EPP"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            rpt.MostrarConsumosEPP(Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    'AL - 24-01-2012
    '    'Reporte para obtener los consumos de EEP en el sistema anterior de panol
    '    'Solicitado por Cristian Lucero
    '    Private Sub ConsumoDeEPPAntes01112011ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsumoDeEPPAntes01112011ToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanol)

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        paramreporte.AgregarParametros("Fecha Inicio :", "DATE", "", False, Inicial, "", CnnPanol)
    '        paramreporte.AgregarParametros("Fecha Fin :", "DATE", "", False, Final, "", CnnPanol)

    '        paramreporte.ShowDialog()

    '        nbreformreportes = "Consumos de EPP antes del 01-11-2011"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString

    '            rpt.MostrarConsumosEPP_antes_01112011(Inicial, Final, rpt)

    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing
    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    'AL - 06-02-2012
    '    'Permite obtener el listado de OPs abiertas
    '    'Solicitado por Juan Marcos Gaydou
    '    Private Sub ResumenDeControlDeOpsAbiertasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResumenDeControlDeOpsAbiertasToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros
    '        Dim rpt As New frmReportes
    '        Dim CnnPanol As New SqlConnection(ConnStringPanol)

    '        ''En esta Variable le paso la fecha actual
    '        Dim NroOP As String = ""
    '        Dim Diseno As String = ""


    '        'paramreporte.AgregarParametros("Nro de Op:", "STRING", "", False, NroOP, "", CnnPanol)
    '        'paramreporte.AgregarParametros("Diseño :", "STRING", "", False, Diseno, "", CnnPanol)

    '        'paramreporte.ShowDialog()

    '        nbreformreportes = "Resumen de Control de OPs - Abiertas"

    '        Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        'If cerroparametrosconaceptar = True Then
    '        'NroOP = paramreporte.ObtenerParametros(0).ToString
    '        'Diseno = paramreporte.ObtenerParametros(1).ToString

    '        rpt.MostrarResumenControlOPsAbiertas(NroOP, Diseno, rpt)

    '        cerroparametrosconaceptar = False
    '        paramreporte = Nothing

    '        'End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub


    '    'AL - 11-04-2012
    '    'Permite obtener datos para mostrar el PDF con la info de los ensayos de PH y UltraSonido
    '    Private Sub EstadodelCano_PDF(ByVal IdCano As String, ByRef FE As String, ByRef FEH As String, ByRef FER As String, ByRef NC As String)
    '        Dim connection As SqlClient.SqlConnection = Nothing
    '        Dim res As Integer = 0

    '        Try
    '            Try
    '                connection = SqlHelper.GetConnection(ConnStringPERFO)
    '            Catch ex As Exception
    '                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                Exit Sub
    '            End Try

    '            Try
    '                Dim param_IdCano As New SqlClient.SqlParameter
    '                param_IdCano.ParameterName = "@idcano"
    '                param_IdCano.SqlDbType = SqlDbType.BigInt
    '                param_IdCano.Value = CType(IdCano, Long)
    '                param_IdCano.Direction = ParameterDirection.Input

    '                Dim param_fechaensayo As New SqlClient.SqlParameter
    '                param_fechaensayo.ParameterName = "@fechaensayo"
    '                param_fechaensayo.SqlDbType = SqlDbType.VarChar
    '                param_fechaensayo.Size = 8
    '                param_fechaensayo.Value = DBNull.Value
    '                param_fechaensayo.Direction = ParameterDirection.Output

    '                Dim param_fechaensayohist As New SqlClient.SqlParameter
    '                param_fechaensayohist.ParameterName = "@fechaensayohist"
    '                param_fechaensayohist.SqlDbType = SqlDbType.VarChar
    '                param_fechaensayohist.Size = 8
    '                param_fechaensayohist.Value = DBNull.Value
    '                param_fechaensayohist.Direction = ParameterDirection.Output

    '                Dim param_fechaensayoreing As New SqlClient.SqlParameter
    '                param_fechaensayoreing.ParameterName = "@fechaensayoreing"
    '                param_fechaensayoreing.SqlDbType = SqlDbType.VarChar
    '                param_fechaensayoreing.Size = 8
    '                param_fechaensayoreing.Value = DBNull.Value
    '                param_fechaensayoreing.Direction = ParameterDirection.Output

    '                Dim param_nrocano As New SqlClient.SqlParameter
    '                param_nrocano.ParameterName = "@nrocano"
    '                param_nrocano.SqlDbType = SqlDbType.VarChar
    '                param_nrocano.Size = 8
    '                param_nrocano.Value = DBNull.Value
    '                param_nrocano.Direction = ParameterDirection.Output

    '                Try
    '                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spReporteEstadodelCano_EnsayosPDF", _
    '                            param_IdCano, param_fechaensayo, param_fechaensayohist, param_fechaensayoreing, param_nrocano)


    '                    FE = param_fechaensayo.Value
    '                    FEH = param_fechaensayohist.Value
    '                    FER = param_fechaensayoreing.Value
    '                    NC = param_nrocano.Value

    '                Catch ex As Exception
    '                    Throw ex
    '                End Try
    '            Finally

    '            End Try
    '        Catch ex As Exception
    '            Dim errMessage As String = ""
    '            Dim tempException As Exception = ex

    '            While (Not tempException Is Nothing)
    '                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '                tempException = tempException.InnerException
    '            End While

    '            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
    '              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
    '              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            If Not connection Is Nothing Then
    '                CType(connection, IDisposable).Dispose()
    '            End If
    '        End Try
    '    End Sub


    '    'AL - 13-04-2012
    '    'Reporte para ver estadisticas de la materia prima utilizada para la produccion
    '    Private Sub EstadisticaDeMateriaPrimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstadisticaDeMateriaPrimaToolStripMenuItem.Click
    '        Dim paramreporte As New frmParametros, paramreporte2 As New frmParametros
    '        Dim reporte As New frmReportes
    '        'nuevo cp 27-5-2011
    '        Dim reporte_setup1 As New frmReportes
    '        Dim reporte_setup2 As New frmReportes
    '        Dim reporte_setup3 As New frmReportes
    '        'fin nuevo cp 27-5-2011
    '        Dim Cnnrn As New SqlConnection(ConnStringPERFO)
    '        Dim Calidad As String

    '        nbreformreportes = "Estadística Materia Prima"

    '        ''En esta Variable le paso la fecha actual
    '        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '        Dim consulta As String = "select DISTINCT CALIDAD from BOBINAS ORDER BY CALIDAD"

    '        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", Cnnrn)
    '        paramreporte.AgregarParametros("Fín :", "DATE", "", True, Final, "", Cnnrn)
    '        paramreporte.AgregarParametros("Calidad Chapa :", "STRING", "", True, "", consulta, Cnnrn)
    '        'paramreporte.AgregarParametros("OP :", "STRING", "", False, "", "", CnnParadas)

    '        paramreporte.ShowDialog()
    '        If cerroparametrosconaceptar = True Then
    '            Inicial = paramreporte.ObtenerParametros(0).ToString
    '            Final = paramreporte.ObtenerParametros(1).ToString
    '            Calidad = paramreporte.ObtenerParametros(2).ToString

    '            Cursor = System.Windows.Forms.Cursors.WaitCursor
    '            'reporte.MdiParent = Me
    '            reporte.MostrarReporteEstadisticaMateriaPrima(Inicial, Final, Calidad, reporte)
    '            cerroparametrosconaceptar = False
    '            paramreporte = Nothing

    '        End If
    '        Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

   
End Class