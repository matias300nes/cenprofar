Imports System.Windows.Forms
Imports System.Diagnostics
Imports Utiles
Imports System.Data.SqlClient
Imports System.Net
Imports ReportesNet
Imports Microsoft.ApplicationBlocks.Data

Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO

Public Class MDIPrincipal

    Public Shared UltBusquedaMat As String

    Public ImpresorValesAbierto As Boolean = True

    Public sucursal As String
    Public NoActualizar As Boolean = False
    Public DesdePedidos As Boolean = False
    Public actualizarstock As Boolean
    Public NoActualizarBase As Boolean

    Public ConfirResPed As Boolean
    Public RealizarPago_Completo As Boolean
    Public Autorizar As Boolean = False
    Public IDEmpleadoAutoriza As String
    Public EmpleadoLogueado As String
    Public iva As Decimal
    Public OperacionCaja As String
    Dim AbrirNotif As Boolean = False
    'Public desdeVentasWEB As Boolean = False
    'Public comm As New CommManager

    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient


    Private Sub MDIPrincipal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("Desea salir del sistema?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                If sucursal.ToUpper.Contains("PRINCIPAL") And (EmpleadoLogueado = "12" Or EmpleadoLogueado = "13" Or EmpleadoLogueado = "2") Then
                    Dim sqlstring As String = "update NotificacionesWEB set BloqueoT = 0,BloqueoR = 0,BloqueoM = 0,BloqueoL = 0,BloqueoC = 0,BloqueoE = 0"
                    tranWEB.Sql_Set(sqlstring)
                End If
            Catch ex As Exception

            End Try
            End
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub MDIPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
        Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)

        Dim nombre As String, pass_actual As String
        EmpleadoLogueado = "" : nombre = "" : pass_actual = ""

        If Process.GetProcessesByName(aProcName).Length > 1 Then
            MessageBox.Show("Ya se está ejecutando una instancia del Sistema.")
            Application.Exit()
        End If

        ControlFirewall()
        iva = 21

      

        Dim login As New Utiles.Login

logindenuevo:

        login.ShowDialog()

        If Not Util.Logueado_OK Then
            GoTo logindenuevo
        End If

        Util.pass_vencida = False
        Util.pass_repetida = False

        Utiles.path = Application.ExecutablePath.ToString
        Utiles.path = Util.TruncarUltimaCarpeta(Utiles.path)

        ' CP 22-05-2012
        ' Setear la Configuracion Regional y de idiomas
        'Try
        '    Dim culture As New System.Globalization.CultureInfo("es-ES")
        '    culture.NumberFormat.NumberDecimalSeparator = ","
        '    culture.NumberFormat.NumberGroupSeparator = "."
        '    culture.NumberFormat.CurrencyDecimalSeparator = ","
        '    culture.NumberFormat.CurrencyGroupSeparator = "."
        '    System.Threading.Thread.CurrentThread.CurrentCulture = culture
        'Catch ex As Exception
        '    MsgBox("Se produjo un inconveniente al modificar la configuración regional." & vbCrLf & "Por avise al Dpto. de Sistemas.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
        'End Try


        Do While Utiles.path <> ""

            If Existe(Utiles.path & "\INIs") Then

                Exit Do
            Else
                Utiles.path = Util.TruncarUltimaCarpeta(Utiles.path)
            End If
        Loop

        If Utiles.path = "" Then
            MsgBox("No se ha Encontrado la carpeta de Inis del Sistema")
            Util.Logueado_OK = False
            Me.Dispose()
        Else
            path_raiz = Utiles.path
            pathrpt = path_raiz & "\Rpt\"
            pathinis = path_raiz & "\INIs\"
            Archivo = pathinis & UCase(SystemInformation.ComputerName.ToString) & ".ini"
        End If

        If Not (System.IO.File.Exists(Archivo)) Then
            MsgBox("No se ha Encontrado el Archivo Ini de la PC con la que se desea conectar al Sistema")
            Util.Logueado_OK = False
            Me.Dispose()
        End If


        ok_cambio = True
        If pass_vencida = True Then
            ok_cambio = False
            MsgBox("Su contraseña ha caducado.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Atención")
        End If

        If ok_cambio = False Then
            End
        End If
        login.Dispose()


        'habilito el timer segun el usuario 
        If SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Or SystemInformation.ComputerName.ToString.ToUpper = "NACHO-PC" Then
            NoActualizar = True
        End If

        If NoActualizar = False Then
            'me fijo si la maquina es la de deposito para no activar el timer
            If Not SystemInformation.ComputerName.ToString.ToUpper = "PUESTO2" Then
                'Timer1.Enabled = True
            End If
        End If

        Dim connection As SqlClient.SqlConnection = Nothing

        connection = SqlHelper.GetConnection(ConnStringSEI)

        Dim ds As Data.DataSet

        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select NombreEmpresaFactura, ModoPagoPredefinido, CUIT, HOMO, TA, PTOVTA, ISNULL(CorreoContador,''), RazonSocial, a.Codigo as CodigoAlmacen " & _
                                      " FROM parametros p JOIN Almacenes a ON a.nombre = p.Razonsocial COLLATE SQL_Latin1_General_CP1_CI_AS ")

        ds.Dispose()

        Utiles.Empresa = LTrim(RTrim(ds.Tables(0).Rows(0).Item(0)))
        'ModoPagoPredefinido = ds.Tables(0).Rows(0).Item(1)
        'cuitEmpresa = ds.Tables(0).Rows(0).Item(2)
        'HOMO = CBool(ds.Tables(0).Rows(0).Item(3))
        'TicketAcceso = CBool(ds.Tables(0).Rows(0).Item(4))
        'PTOVTA = ds.Tables(0).Rows(0).Item(5)
        'CorreoContador = ds.Tables(0).Rows(0).Item(6)
        sucursal = ds.Tables(0).Rows(0).Item(7).ToString

        Utiles.numero_almacen = ds.Tables(0).Rows(0).Item(8).ToString


        'Controlo que sucursal es para ver que parte del menu muestro y que parte no 
        If sucursal.Contains("PERON") Then

            ProveedoresToolStripMenuItem.Visible = False
            ListaPreciosToolStripMenuItem.Visible = False
            MarcasToolStripMenuItem.Visible = False
            FamiliasToolStripMenuItem.Visible = False
            UnidadesToolStripMenuItem1.Visible = False
            OrdenDeCompraToolStripMenuItem.Visible = False
            RecepcionesDeMaterialToolStripMenuItem.Visible = False
            DepósitoToolStripMenuItem.Visible = False
            PresupuestoToolStripMenuItem.Visible = False
            ContabilidadToolStripMenuItem.Visible = False
            RRHHToolStripMenuItem.Visible = False
            InformesToolStripMenuItem1.Visible = False
            SeguridadToolStripMenuItem.Visible = False
            ContabilidadToolStripMenuItem.Visible = False
            ToolStripSeparator15.Visible = False
            VentasToolStripMenuItem.Text = "Ventas Depósito Perón"
            EnvíosDePedidosToolStripMenuItem.Visible = False
            VentasDepósitoToolStripMenuItem.Visible = False
            ReportesDepositoToolStripMenuItem.Visible = False
            AbrirNotif = True
        Else
            'VentasToolStripMenuItem.Enabled = False
            VentasToolStripMenuItem.Text = "Ventas Salón"
        End If

        If Util.ObtenerDatosDelUserId(UserID, EmpleadoLogueado, nombre, pass_actual, Util.ConnStringSEI) > 0 Then
            ToolStripStatusLabel.Text = "Equipo: " & GetUserID() & " - Usuario: " & EmpleadoLogueado & " " & nombre

            If ControlUsuarioAutorizado(EmpleadoLogueado) = False Then
                InformesToolStripMenuItem1.Enabled = False
                RRHHToolStripMenuItem.Enabled = False
                ContabilidadToolStripMenuItem.Enabled = False
                OrdenDeCompraToolStripMenuItem.Enabled = False
                RecepcionesDeMaterialToolStripMenuItem.Enabled = False
                AjustesDeInventarioToolStripMenuItem.Enabled = False

            Else
                If sucursal.Contains("PERON") Then
                    ContabilidadToolStripMenuItem.Visible = True
                    InformesToolStripMenuItem1.Visible = True
                    RRHHToolStripMenuItem.Visible = True
                    DeudaDeClientesToolStripMenuItem.Enabled = False
                    JornadasDeTrabajoToolStripMenuItem.Enabled = True
                    OrdenDeCompraToolStripMenuItem.Enabled = True
                    ProveedoresToolStripMenuItem.Visible = True
                    ListaPreciosToolStripMenuItem.Visible = True
                    MarcasToolStripMenuItem.Visible = True
                    FamiliasToolStripMenuItem.Visible = True
                    UnidadesToolStripMenuItem1.Visible = True
                    PromocionesToolStripMenuItem.Visible = True
                End If
                AjustesDeInventarioToolStripMenuItem.Enabled = True
                MovimientosDelDíaToolStripMenuItem_Click(sender, e)
                'AbrirNotif = True
            End If

            'habilitación para Maria Jesus
            If EmpleadoLogueado = "12" Then
                ContabilidadToolStripMenuItem.Enabled = True
                AbrirNotif = True
            End If

            'Habilitaciión para Analia 
            If EmpleadoLogueado = "13" Then
                InformesToolStripMenuItem1.Enabled = True
                RRHHToolStripMenuItem.Enabled = True
                ContabilidadToolStripMenuItem.Enabled = True
                OrdenDeCompraToolStripMenuItem.Enabled = True
                RecepcionesDeMaterialToolStripMenuItem.Enabled = True
                AjustesDeInventarioToolStripMenuItem.Enabled = True
                AbrirNotif = True
            End If

            'Habilitacion para Matias y Nahuel (por ahora no)
            If EmpleadoLogueado = "4" Then 'Or EmpleadoLogueado = "11" Then
                InformesToolStripMenuItem1.Enabled = True
                RankingDeProductosPresupuestadosToolStripMenuItem.Enabled = True

                DeudaDeClientesToolStripMenuItem.Enabled = False
                MovimientosDelDíaToolStripMenuItem.Enabled = False
                MovimientosDelDíaToolStripMenuItem1.Enabled = False
                AbrirNotif = False
            End If



        Else
            ToolStripStatusLabel.Text = GetUserID()
        End If

0:
        If UserActual = "marconi" Or UserActual.ToUpper = "MARCONI" Then
            'MenuStrip.Visible = False
            'frmVentaSalon.MdiParent = Me
            AbrirNotif = False
            frmVentaSalon.ShowDialog()
            Exit Sub
        End If

        Try

            Dim sqlstring As String = "update NotificacionesWEB set BloqueoT = 0,BloqueoR = 0,BloqueoM = 0,BloqueoL = 0,BloqueoC = 0,BloqueoE = 0"
            tranWEB.Sql_Set(sqlstring)

        Catch ex As Exception

        End Try

        If AbrirNotif = True Then
            ActualizarSistemaToolStripMenuItem_Click(sender, e)
        End If
       


    End Sub

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripButton.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer = 0

    Private Sub FamiliasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FamiliasToolStripMenuItem.Click
        frmRubros.MdiParent = Me
        frmRubros.Show()
    End Sub

    Private Sub AlmacenesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmAlmacenes.MdiParent = Me
        frmAlmacenes.Show()
    End Sub

    Private Sub GerenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmObras.MdiParent = Me
        frmObras.Show()
    End Sub

    Private Sub MaterialesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialesToolStripMenuItem1.Click
        'me fijo si es la sucursal de peron 
        '        If Not sucursal.Contains("PERON") Then
        '            GoTo CONTINUAR
        '        End If

        '        If UserActual = "administrador" Then
        'CONTINUAR:
        '            frmMateriales.MdiParent = Me
        '            frmMateriales.Show()
        '        Else
        '            frmUsuarioModo.Formulario = "Productos"
        '            frmUsuarioModo.MdiParent = Me
        '            frmUsuarioModo.Show()
        '        End If
        'me fijo si es la sucursal de peron 
        If sucursal.Contains("PERON") Then
         
            If Not UserActual = "administrador" Then
                frmMateriales.btnNuevo.Visible = False
                frmMateriales.btnGuardar.Visible = False
                frmMateriales.btnEliminar.Visible = False
                frmMateriales.btnCancelar.Visible = False
                frmMateriales.btnActivar.Visible = False
                frmMateriales.btnCargaContinua.Enabled = False

                frmMateriales.MdiParent = Me
                frmMateriales.Show()
            Else
                frmMateriales.MdiParent = Me
                frmMateriales.Show()
            End If

        Else
            frmMateriales.MdiParent = Me
            frmMateriales.Show()
        End If
    End Sub

    Private Sub UnidadesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnidadesToolStripMenuItem1.Click
        frmUnidades.MdiParent = Me
        frmUnidades.Show()
    End Sub

    Private Sub PresupuestoDetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PresupuestoDetToolStripMenuItem.Click
        Try
            frmPresupuestos.Origen = 0
            frmPresupuestos.MdiParent = Me
            frmPresupuestos.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RegistrarConsumosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegistrarConsumosToolStripMenuItem.Click
        frmOrdenCompra_Abierta.MdiParent = Me
        frmOrdenCompra_Abierta.Show()
    End Sub

    Private Sub AjustesDeInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AjustesDeInventarioToolStripMenuItem.Click

        '        'me fijo si es la sucursal de peron 
        '        If Not sucursal.Contains("PERON") Then
        '            GoTo CONTINUAR
        '        End If

        '        If UserActual = "administrador" Then
        'CONTINUAR:
        frmAjustes.MdiParent = Me
        frmAjustes.Show()
        '        Else

        'frmUsuarioModo.Formulario = "Ajustes"
        'frmUsuarioModo.MdiParent = Me
        'frmUsuarioModo.Show()
        'End If

    End Sub

    Private Sub RecepcionesDeMaterialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecepcionesDeMaterialToolStripMenuItem.Click
        frmRecepciones.MdiParent = Me
        frmRecepciones.Show()
    End Sub

    Private Sub TransferenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmTransferencias.MdiParent = Me
        frmTransferencias.Show()
    End Sub

    Private Sub DevolucionesAIcysToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DevolucionesAIcysToolStripMenuItem.Click
        frmDevoluciones.MdiParent = Me
        frmDevoluciones.Show()
    End Sub

    Private Sub DevoluciónAProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DevoluciónAProveedoresToolStripMenuItem.Click
        frmDevolucionesProveedor.MdiParent = Me
        frmDevolucionesProveedor.Show()
    End Sub

    Private Sub OrdenDeCompraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdenDeCompraToolStripMenuItem.Click
        frmOrdenCompra.MdiParent = Me
        frmOrdenCompra.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'End

        Timer1.Enabled = False
        'MsgBox("aca")
        'mando el boton de actualizar(Esto se ejecutara cada 5 min)
        ActualizarSistemaToolStripMenuItem_Click(sender, e)

        If Not sucursal.Contains("PERON") Then
            Timer1.Enabled = True
        End If


    End Sub

    Private Sub HorizontalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub VerticalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub CascadaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub BajaRotaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        ''En esta Variable le paso la fecha actual
        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)



        paramreporte.ShowDialog()
        nbreformreportes = "Materiales de baja rotación"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString

            'rpt.MostrarReporteBajaRotacion(Inicial, Final, rpt)
            'rpt.MostrarReporteBajaRotacionNET(Inicial, Final, rpt)
            cerroparametrosconaceptar = False
            paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        Cnn = Nothing

    End Sub

    'DG 23-09-2011
    Private Sub PlanillaInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        ''En esta Variable le paso la fecha actual
        Dim Rubro As String, Ubicacion As String

        paramreporte.AgregarParametros("Rubro:", "STRING", "", False, "", "", Cnn)
        paramreporte.AgregarParametros("Ubicacion :", "STRING", "", False, "", "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Planilla de Inventarios"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Rubro = paramreporte.ObtenerParametros(0).ToString
            Ubicacion = paramreporte.ObtenerParametros(1).ToString
            'rpt.MostrarReportePlanillaInventario(Rubro, Ubicacion, rpt)
            'rpt.MostrarReportePlanillaInventarioNET(Rubro, Ubicacion, rpt)

            cerroparametrosconaceptar = False
            paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        Cnn = Nothing
    End Sub

    'DG 23-09-2011
    Private Sub StockCeroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim codigo As String

        paramreporte.AgregarParametros("Código de Material:", "STRING", "", False, "", "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Materiales Sin Stock"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            codigo = paramreporte.ObtenerParametros(0).ToString
            'rpt.MostrarReportePlanillaInventario(Rubro, Ubicacion, rpt)
            ' rpt.MostrarStockCeroNET(codigo, rpt)

            cerroparametrosconaceptar = False
            paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        Cnn = Nothing
    End Sub

    Private Sub CálculoDeReposiciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim familia As String
        Dim vertodos As String
        Dim consulta1 As String = ""
        Dim consulta2 As String = ""

        'consulta1 = "select '' as nombre union all select nombre from familias where eliminado=0 group by nombre order by nombre"
        'dg pidio mostrar codigo - nombre
        consulta1 = "select '' as nombre union all select codigo + ' - ' + nombre as nombre from familias where eliminado=0 group by codigo,nombre order by nombre"
        consulta2 = "select '' as codigo union select 'SI' as codigo union select 'NO' as codigo"

        paramreporte.AgregarParametros("Nombre de Familia:", "STRING", "", False, "", consulta1, Cnn)
        paramreporte.AgregarParametros("Ver Todos:", "STRING", "", False, "", consulta2, Cnn)
        paramreporte.ShowDialog()


        nbreformreportes = "Cálculo de Reposición"
        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            'familia = paramreporte.ObtenerParametros(0).ToString
            familia = Mid(paramreporte.ObtenerParametros(0).ToString, 1, 10) 'ahora obtenemos el codigo..
            vertodos = paramreporte.ObtenerParametros(1).ToString
            'rpt.MostrarCalculoReposicionNet(familia, vertodos, rpt)

            cerroparametrosconaceptar = False
            paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        Cnn = Nothing
    End Sub

    'dg 03-10-2011
    Private Sub MaterialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim codigo As String

        paramreporte.AgregarParametros("Código de Material:", "STRING", "", False, "", "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Etiqueta de Material"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            codigo = paramreporte.ObtenerParametros(0).ToString
            'rpt.MostrarEtiquetaMaterialNet(codigo, rpt)

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    'dg 03-10-2011
    Private Sub UsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim codigo As String

        paramreporte.AgregarParametros("Código de Usuario:", "STRING", "", False, "", "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Etiqueta de Usuario"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            codigo = paramreporte.ObtenerParametros(0).ToString
            'rpt.MostrarEtiquetaUsuarioNet(codigo, rpt)

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    'dg 03-10-2011
    Private Sub CentroDeCostoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim codigo As String

        paramreporte.AgregarParametros("Id de CC:", "STRING", "", False, "", "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Etiqueta de Centro de Costo"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            codigo = paramreporte.ObtenerParametros(0).ToString
            'rpt.MostrarEtiquetaCCNet(codigo, rpt)

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    'Private Sub MovimientosDeStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim paramreporte As New frmParametros
    '    Dim rpt As New frmReportes
    '    'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
    '    Dim Cnn As New SqlConnection(ConnStringSEI)

    '    Dim codigomaterial As String
    '    'Dim codigoalmacen As String


    '    Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
    '    Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

    '    paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
    '    paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
    '    paramreporte.AgregarParametros("Código Material:", "STRING", "", False, "", "", Cnn)
    '    'paramreporte.AgregarParametros("Código Almacén:", "STRING", "", False, "", "", Cnn)

    '    paramreporte.ShowDialog()

    '    nbreformreportes = "Movimientos de Stock"

    '    Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    If cerroparametrosconaceptar = True Then
    '        Inicial = paramreporte.ObtenerParametros(0).ToString
    '        Final = paramreporte.ObtenerParametros(1).ToString
    '        codigomaterial = paramreporte.ObtenerParametros(2).ToString
    '        'codigoalmacen = paramreporte.ObtenerParametros(3).ToString
    '        rpt.MostrarReporteMovimientoMaterial(Inicial, Final, codigomaterial, rpt)

    '        cerroparametrosconaceptar = False
    '        'paramreporte = Nothing
    '    End If
    '    Cursor = System.Windows.Forms.Cursors.Default
    '    'Cnn = Nothing

    '    CType(paramreporte, IDisposable).Dispose()
    '    CType(Cnn, IDisposable).Dispose()

    'End Sub

    Private Sub StockActualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        nbreformreportes = "Materiales En Stock"
        Dim rpt As New frmReportes
        Dim paramConsumos As New frmParametros
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim codigo_mat As String = ""
        Dim codigo_familia As String = ""

        Utiles.filtradopor = ""

        Dim cadena1() As String
        Dim cadena2() As String

        Dim consulta1 As String = ""
        Dim consulta2 As String = ""
        Dim consulta3 As String = ""

        consulta1 = "select  codigo + ' - '+ nombre as material from materiales where eliminado = 0 order by codigo"
        consulta2 = "select  codigo + ' - '+ nombre as Familia from familias where eliminado = 0 order by codigo"

        paramConsumos.AgregarParametros("Código de material:", "STRING", "", False, , consulta1, Cnn)
        paramConsumos.AgregarParametros("Código de Familia:", "STRING", "", False, , consulta2, Cnn)

        paramConsumos.ShowDialog()
        If cerroparametrosconaceptar = True Then
            cadena1 = Split(paramConsumos.ObtenerParametros(0), "-")
            codigo_mat = cadena1(0).ToString.Trim
            cadena2 = Split(paramConsumos.ObtenerParametros(1), "-")
            codigo_familia = cadena2(0).ToString.Trim

            'rpt.MostrarMaterialesEnStock(rpt, codigo_mat, codigo_familia)
        End If
        Cursor = System.Windows.Forms.Cursors.Default

        CType(paramConsumos, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    'Private Sub SectoresEquiposToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SectoresEquiposToolStripMenuItem.Click
    '    Dim f As New TM3.frmSectoresEquipos()
    '    f.MdiParent = Me
    '    f.Show()
    'End Sub

    Private Sub ImpresorValesAutomáticoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ImpresorVales As New frmImpresorVales
        If ImpresorValesAbierto = True Then
            MsgBox("La ventana ya está abierta en esta PC.")
        Else
            ImpresorVales.Show()
            ImpresorValesAbierto = True
        End If
    End Sub

    Private Sub ProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProveedoresToolStripMenuItem.Click
        frmProveedores.MdiParent = Me
        frmProveedores.Show()
    End Sub

    Private Sub ObrasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmObras.MdiParent = Me
        frmObras.Show()
    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsuariosToolStripMenuItem.Click
        frmUsuarios.MdiParent = Me
        frmUsuarios.Show()
    End Sub

    Private Sub ClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientesToolStripMenuItem.Click
        frmClientes.MdiParent = Me
        frmClientes.Show()
    End Sub

    Private Sub GastosPorObraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GastosPorObraToolStripMenuItem.Click
        frmGastos.MdiParent = Me
        frmGastos.Show()
    End Sub

    Private Sub CierreDeObrasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmIngresos_postTrabajo.MdiParent = Me
        frmIngresos_postTrabajo.Show()
    End Sub

    Private Sub ChequesEnCarteraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChequesEnCarteraToolStripMenuItem.Click
        frmCheques.MdiParent = Me
        frmCheques.Show()
    End Sub

    Private Sub CierreDeObrasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmCierredeObras.MdiParent = Me
        frmCierredeObras.Show()
    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercaDeToolStripMenuItem.Click
        frmAcercade.MdiParent = Me
        frmAcercade.Show()

    End Sub

    Private Sub NotasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotasToolStripMenuItem.Click
        frmNotas.MdiParent = Me
        frmNotas.Show()
    End Sub

    Private Sub GestiónDePresupuestosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GestiónDePresupuestosToolStripMenuItem.Click
        frmRemitos.MdiParent = Me
        frmRemitos.Show()
    End Sub

    Private Sub ListaDePreciosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListaDePreciosToolStripMenuItem.Click
        frmMaterialesPrecios.MdiParent = Me
        frmMaterialesPrecios.WindowState = FormWindowState.Maximized
        frmMaterialesPrecios.Show()
    End Sub

    Private Sub MonedasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonedasToolStripMenuItem.Click
        frmTipoMoneda.MdiParent = Me
        frmTipoMoneda.Show()
        frmTipoMoneda.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ActualizarPresupuestoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualizarPresupuestoToolStripMenuItem.Click
        frmFacturacion.MdiParent = Me
        frmFacturacion.Show()
    End Sub

    Private Sub PagosDeClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PagosDeClientesToolStripMenuItem.Click
        frmPagodeClientes.MdiParent = Me
        frmPagodeClientes.Show()
    End Sub

    '*******************************************************************************************************************************************
    '*******************************************************************************************************************************************
    '*******************************************************************************************************************************************

#Region "Reportes en el Principal"

    Private Sub ReportesVendidos(ByVal Moneda As String)

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Cliente As String, consulta As String ', Moneda As String = "1"

        If Moneda = "1" Then
            consulta = "SELECT '' AS NOMBRE UNION select DISTINCT nombre from Clientes c JOIN PedidosWEB p ON p.idcliente = c.Codigo  order by nombre asc"
        Else
            consulta = "SELECT '' AS NOMBRE UNION select DISTINCT CONCAT(Apellido ,' ', Nombre) AS Nombre from Empleados c JOIN PedidosWEB p ON p.IDEmpleado = c.Codigo  order by nombre asc"
        End If

        'Dim consulta As String = "SELECT '' AS NOMBRE UNION select DISTINCT nombre from Clientes c JOIN Presupuestos p ON p.idcliente = c.id JOIN Presupuestos_Gestion pg ON pg.idpresupuesto = p.id order by nombre asc"
        Dim consulta1 As String = "SELECT '' AS NOMBRE UNION select nombre from Monedas Order by nombre asc"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", True, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", True, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        'paramreporte.AgregarParametros("Moneda :", "STRING", "", False, "", consulta1, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Productos más Vendidos"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString
            ' Moneda = paramreporte.ObtenerParametros(3).ToString

            rpt.RankingProductosVendidos_App(Inicial, Final, Cliente, rpt, My.Application.Info.AssemblyName.ToString, Moneda)

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()
    End Sub

    Private Sub ProductosMásVendidosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductosMásVendidosToolStripMenuItem.Click
        ReportesVendidos("1")
    End Sub

    Private Sub ProductosMásPresupuestadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductosMásPresupuestadosToolStripMenuItem.Click
        ReportesVendidos("2")
        'Dim paramreporte As New frmParametros
        'Dim rpt As New frmReportes
        'Dim Cnn As New SqlConnection(ConnStringSEI)


        'Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        'Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        'paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        'paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)

        'paramreporte.ShowDialog()

        'nbreformreportes = "Productos más Presupuestados"

        'Cursor = System.Windows.Forms.Cursors.WaitCursor

        'If cerroparametrosconaceptar = True Then
        '    Inicial = paramreporte.ObtenerParametros(0).ToString
        '    Final = paramreporte.ObtenerParametros(1).ToString

        '    rpt.RankingProductosPresupuestados_App(Inicial, Final, rpt, My.Application.Info.AssemblyName.ToString)

        '    cerroparametrosconaceptar = False
        '    'paramreporte = Nothing
        'End If
        'Cursor = System.Windows.Forms.Cursors.Default
        ''Cnn = Nothing

        'CType(paramreporte, IDisposable).Dispose()
        'CType(Cnn, IDisposable).Dispose()
    End Sub

    Private Sub ImpuestosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImpuestosToolStripMenuItem.Click
        frmImpuestos.MdiParent = Me
        frmImpuestos.Show()
    End Sub

    Private Sub DeudaDeClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeudaDeClientesToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim Cliente As String, Estado As String

        Dim consulta As String = "select '' as Nombre UNION select nombre from clientes c JOIN PedidosWeb F on f.idcliente = c.codigo where c.eliminado = 0 order by nombre asc"

        'Dim consulta1 As String = "SELECT Nombre FROM (SELECT 0 AS Orden, 'Canceladas' as Nombre UNION select 1 AS Orden, 'Pendientes' AS nombre UNION SELECT 2 AS Orden, 'Anuladas' as Nombre) aa"
        'Dim consulta1 As String = "SELECT Nombre FROM (SELECT 0 AS Orden, 'Todas' as Nombre UNION SELECT 1 AS Orden, 'Canceladas' as Nombre UNION select 2 AS Orden, 'Pendientes' AS nombre UNION SELECT 3 AS Orden, 'Anuladas' as Nombre) aa"
        Dim consulta1 As String = "SELECT Nombre FROM (SELECT 0 AS Orden, 'Todas' as Nombre UNION SELECT 1 AS Orden, 'Canceladas' as Nombre UNION select 2 AS Orden, 'Pendientes' AS nombre UNION SELECT 3 AS Orden, 'Anuladas' as Nombre UNION SELECT 4 AS Orden, 'Resumen de Cuenta' as Nombre) aa"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        paramreporte.AgregarParametros("Estado :", "STRING", "", False, "", consulta1, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Estado de Cuenta"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            If cerroparametrosconaceptar = True Then

                Inicial = paramreporte.ObtenerParametros(0).ToString
                Final = paramreporte.ObtenerParametros(1).ToString
                Cliente = paramreporte.ObtenerParametros(2).ToString
                Estado = paramreporte.ObtenerParametros(3).ToString

                If Estado = "Resumen de Cuenta" Then
                    If Cliente <> "" Then
                        Estado = "Pendientes"
                        rpt.DeudaClientes_App(Inicial, Final, Cliente, Estado, rpt, My.Application.Info.AssemblyName.ToString, True)
                    Else
                        MsgBox("Por favor seleccione un cliente para poder ver su resumen de cuenta", MsgBoxStyle.Exclamation)
                    End If
                Else
                    rpt.DeudaClientes_App(Inicial, Final, Cliente, Estado, rpt, My.Application.Info.AssemblyName.ToString, False)
                End If



            End If
        Catch ex As Exception

        End Try

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

    Private Sub RemitosDuplicadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemitosDuplicadosToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        nbreformreportes = "Remitos Duplicados"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        rpt.RemitosDuplicados_App(rpt, My.Application.Info.AssemblyName.ToString)

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

    Private Sub HistorialDeFacturasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HistorialDeFacturasToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Cliente As String, Cancelada As String

        Dim consulta As String = "SELECT '' AS NOMBRE UNION select nombre from clientes c JOIN Facturacion f ON f.idcliente = c.id where c.eliminado = 0 order by nombre asc"
        Dim consulta2 = "select '' as codigo union select 'SI' as codigo union select 'NO' as codigo"


        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        paramreporte.AgregarParametros("Canceladas:", "STRING", "", False, "", consulta2, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Historial de Facturas"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString
            Cancelada = paramreporte.ObtenerParametros(3).ToString

            rpt.HistorialdeFacturas_App(Inicial, Final, Cliente, Cancelada, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

    Private Sub FacturaciónManualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FacturaciónManualToolStripMenuItem.Click
        frmFacturacion_Manual.MdiParent = Me
        frmFacturacion_Manual.Show()
    End Sub

    Private Sub InformeSobreImpuestosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformeSobreImpuestosToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)
        Dim Cliente As String

        Dim consulta As String = "select nombre from clientes where eliminado = 0 order by nombre asc"


        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Informe sobre Impuestos"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString

            rpt.ACER_InformeImpuestos(Inicial, Final, Cliente, rpt)

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If
        Cursor = System.Windows.Forms.Cursors.Default

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        'Cnn = Nothing
    End Sub

#End Region

#Region "Informes - Stock"

    Private Sub StockporRubroYSubrubroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockporRubroYSubrubroToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim paramreporte2 As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Rubro As String
        Dim Subrubro As String
        Dim consulta As String

        consulta = "select (codigo + '-' + nombre) as Codigo from familias where eliminado = 0 ORDER BY ID"

        paramreporte.AgregarParametros("Rubro:", "String", "", True, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Stock por Rubro y Subrubro"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Rubro = paramreporte.ObtenerParametros(0).ToString

            Dim IdRubro As String
            IdRubro = Mid$(Rubro, 1, InStr(Trim$(Rubro), "-") - 1).ToUpper

            consulta = "select codigo + '-' + nombre from Subrubros where eliminado = 0 and idfamilia = " & CLng(IdRubro) & " order by id"

            paramreporte2.AgregarParametros("SubRubro :", "String", "", True, "", consulta, Cnn)

            paramreporte2.ShowDialog()

            nbreformreportes = "Stock por Rubro y Subrubro"

            Cursor = System.Windows.Forms.Cursors.WaitCursor

            If cerroparametrosconaceptar = True Then
                Subrubro = paramreporte2.ObtenerParametros(0).ToString

                Dim IdSubRubro As String
                IdSubRubro = Mid$(Subrubro, 1, InStr(Trim$(Subrubro), "-") - 1).ToUpper

                rpt.Stock_RubroySubrubro_App(CLng(IdRubro), IdSubRubro, rpt, My.Application.Info.AssemblyName.ToString)

                cerroparametrosconaceptar = False

            End If

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()
    End Sub

    Private Sub StockFiltrosPersonalizadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockFiltrosPersonalizadosToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim paramreporte2 As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Filtro As String
        Dim consulta As String

        consulta = "SELECT Distinct Busqueda FROM CriteriosdeBusquedas ORDER BY BUSQUEDA"

        paramreporte.AgregarParametros("Filtro Personalizado:", "String", "", True, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Stock - Filtro Personalizado"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Filtro = paramreporte.ObtenerParametros(0).ToString

            rpt.Stock_FiltroPersonalizado_App(Filtro, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False

        End If

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()
    End Sub

    Private Sub Stock_Actual_ValorizadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Stock_Actual_ValorizadoToolStripMenuItem.Click

        Dim paramreporte As New frmParametros
        Dim paramreporte2 As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Rubro As String
        Dim Subrubro As String
        Dim consulta As String

        consulta = "select (codigo + '-' + nombre) as Codigo from familias where eliminado = 0 ORDER BY ID"

        paramreporte.AgregarParametros("Rubro:", "String", "", False, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Stock Actual Valorizado"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Rubro = paramreporte.ObtenerParametros(0).ToString

            Dim IdRubro As String

            If Rubro <> "" Then
                IdRubro = Mid$(Rubro, 1, InStr(Trim$(Rubro), "-") - 1).ToUpper
            Else
                rpt.Stock_Actual_Valorizado_App("", "", rpt, My.Application.Info.AssemblyName.ToString)

                cerroparametrosconaceptar = False

                Cursor = System.Windows.Forms.Cursors.Default
                'Cnn = Nothing

                CType(paramreporte, IDisposable).Dispose()
                CType(Cnn, IDisposable).Dispose()

                Exit Sub

            End If

            consulta = "select codigo + '-' + nombre from Subrubros where eliminado = 0 and idfamilia = " & CLng(IdRubro) & " order by id"

            paramreporte2.AgregarParametros("SubRubro :", "String", "", False, "", consulta, Cnn)

            paramreporte2.ShowDialog()

            nbreformreportes = "Stock por Rubro y Subrubro"

            Cursor = System.Windows.Forms.Cursors.WaitCursor

            If cerroparametrosconaceptar = True Then
                Subrubro = paramreporte2.ObtenerParametros(0).ToString

                Dim IdSubRubro As String

                If Subrubro <> "" Then
                    IdSubRubro = Mid$(Subrubro, 1, InStr(Trim$(Subrubro), "-") - 1).ToUpper
                Else
                    IdSubRubro = ""
                End If

                rpt.Stock_Actual_Valorizado_App(CLng(IdRubro), IdSubRubro, rpt, My.Application.Info.AssemblyName.ToString)

                cerroparametrosconaceptar = False

            End If

            cerroparametrosconaceptar = False
            'paramreporte = Nothing
        End If

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

    End Sub

#End Region

#Region "Pendientes - Presupuestos y OC"

    Private Sub OrdenesDeCompraPendientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrdenesDeCompraPendientesToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Proveedor As String

        Dim consulta As String = "SELECT '' AS NOMBRE UNION select DISTINCT nombre from Proveedores p JOIN Ordendecompra oc ON oc.idproveedor = p.id JOIN OrdendeCompra_det ocd ON ocd.idordendecompra = oc.id where ocd.status = 'p' and ocd.eliminado = 0 order by nombre asc"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Proveedor :", "STRING", "", False, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Ordenes de Compra Pendientes"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Proveedor = paramreporte.ObtenerParametros(2).ToString

            rpt.OrdenesdeCompra_Pendientes_App(Inicial, Final, Proveedor, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

    Private Sub PresupuestosAprobadosMatPendientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PresupuestosAprobadosMatPendientesToolStripMenuItem.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Cliente As String, Estado As String

        Dim consulta As String = "SELECT '' AS NOMBRE UNION select DISTINCT nombre from Clientes c JOIN Presupuestos p ON p.idcliente = c.id JOIN Presupuestos_det pd ON pd.idpresupuesto = p.id JOIN Presupuestos_Gestion pg ON pg.idpresupuesto = p.id where pd.status = 'p' and pd.eliminado = 0 order by nombre asc"
        Dim consulta2 As String = "SELECT 'Pendiente' AS Estado UNION SELECT 'Cumplido' as Estado"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        paramreporte.AgregarParametros("Estado :", "STRING", "", False, "", consulta2, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Presupuestos con Entregas Parciales - Mat. Pendientes"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString
            Estado = paramreporte.ObtenerParametros(3).ToString

            rpt.PrespuestosAprobados_Pendientes_App(Inicial, Final, Cliente, Estado, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

#End Region



    Private Sub Resolucion(ByRef form As Form)
        Dim ANCHO As String
        Dim alto As String
        Dim tamano As String

        ANCHO = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width.ToString
        alto = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height.ToString
        tamano = ANCHO + "x" + alto
        Select Case tamano
            'Case "800x600"
            '    cambiarResolucion(form, 110.0F, 110.0F)
            Case "1280x1024"
                cambiarResolucion(form, 96.0F, 110.0F)
            Case Else
                cambiarResolucion(form, 96.0F, 96.0F)
        End Select

    End Sub

    Private Sub cambiarResolucion(ByRef formulario As Form, ByVal ancho As Double, ByVal alto As Double)
        formulario.AutoScaleDimensions = New System.Drawing.SizeF(ancho, alto)
        formulario.PerformAutoScale()
    End Sub

    Private Sub NotasDeCréditoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotasDeCréditoToolStripMenuItem.Click
        frmNotaCredito.MdiParent = Me
        frmNotaCredito.Show()
    End Sub

    Private Sub CondicionesDePagoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CondicionesDePagoToolStripMenuItem.Click
        frmCondiciondePago.MdiParent = Me
        frmCondiciondePago.Show()
    End Sub

    Private Sub PagosAProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PagosAProveedoresToolStripMenuItem.Click
        frmPagodeGastos.MdiParent = Me
        frmPagodeGastos.Show()
    End Sub

    Private Sub HistoriaPresupuestosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HistoriaPresupuestosToolStripMenuItem.Click
        frmPresupuestos_Historia.MdiParent = Me
        frmPresupuestos_Historia.Show()
    End Sub

    Private Sub MarcasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarcasToolStripMenuItem.Click
        frmMarcas.MdiParent = Me
        frmMarcas.Show()
    End Sub

    Private Sub EmpleadosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmpleadosToolStripMenuItem.Click

        '        'me fijo si es la sucursal de peron 
        '        If Not sucursal.Contains("PERON") Then
        '            GoTo CONTINUAR
        '        End If

        '        If UserActual = "administrador" Then
        'CONTINUAR:
        frmEmpleados.MdiParent = Me
        frmEmpleados.Show()
        'Else
        '    frmUsuarioModo.Formulario = "Empleados"
        '    frmUsuarioModo.MdiParent = Me
        '    frmUsuarioModo.Show()
        'End If



    End Sub

    Private Sub InformeDeRetencionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformeDeRetencionesToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Cliente As String

        Dim consulta As String = "select nombre from clientes where eliminado = 0 order by nombre asc"


        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Informe de Retenciones"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString

            rpt.ACER_InformeRetenciones(Inicial, Final, Cliente, rpt)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

    Private Sub EstadoDeDeudaProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstadoDeDeudaProveedoresToolStripMenuItem.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Proveedor As String, TipoGasto As String, Cancelado As String

        Dim consulta As String = "SELECT '' AS Nombre UNION SELECT nombre from Proveedores where eliminado = 0 order by nombre asc"
        Dim consulta2 As String = "SELECT '' AS Nombre UNION SELECT nombre from TipoGastos where eliminado = 0 order by nombre asc"
        Dim consulta3 As String = "SELECT '' as codigo UNION SELECT 'SI' as codigo union select 'NO' as codigo"


        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("TipoGasto :", "STRING", "", False, "", consulta2, Cnn)
        paramreporte.AgregarParametros("Proveedor :", "STRING", "", False, "", consulta, Cnn)
        paramreporte.AgregarParametros("Cancelados:", "STRING", "", False, "", consulta3, Cnn)


        paramreporte.ShowDialog()

        nbreformreportes = "Detalle de Gastos"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            TipoGasto = paramreporte.ObtenerParametros(2).ToString
            Proveedor = paramreporte.ObtenerParametros(3).ToString
            Cancelado = paramreporte.ObtenerParametros(4).ToString

            rpt.Gastos_App(Inicial, Final, Proveedor, TipoGasto, Cancelado, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

    Private Sub HistoriaDePresupuestosReporteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HistoriaDePresupuestosReporteToolStripMenuItem.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Cliente As String, Estado As String

        Dim consulta As String = "SELECT '' as Nombre UNION select nombre from Clientes where eliminado = 0 order by nombre asc"
        Dim consulta2 = "select '' as Nombre UNION select nombre from Estados WHERE nombre = 'Vencido' order by nombre"

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)
        paramreporte.AgregarParametros("Cliente :", "STRING", "", False, "", consulta, Cnn)
        paramreporte.AgregarParametros("Estado:", "STRING", "", False, "", consulta2, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Historia de Presupuestos - Reporte"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString
            Cliente = paramreporte.ObtenerParametros(2).ToString
            Estado = paramreporte.ObtenerParametros(3).ToString

            rpt.HistoriaPresupuestos_APP(Inicial, Final, Cliente, Estado, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing

    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub EtiquetasDeProductosCodBarrasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EtiquetasDeProductosCodBarrasToolStripMenuItem.Click

        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        'Dim CnnTM3 As New SqlConnection(ConnStringTM3)
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim codigo As String ', codigo_almacen As String, critico As String
        Dim consulta As String ', consulta2 As String
        'Dim linea() As String

        'consulta = "SELECT nombre + ' ( ' + codigo + ')' as codigo FROM Almacenes WHERE Eliminado = 0 ORDER BY codigo"

        consulta = "SELECT (CODIGO + ' ** ' + nombre COLLATE Latin1_General_CI_AS ) AS codigo FROM Materiales WHERE Eliminado = 0 ORDER BY codigo"

        'paramreporte.AgregarParametros("Código de Almacén:", "STRING", "", False, "", consulta, CnnPANOL)
        paramreporte.AgregarParametros("Código de Material:", "STRING", "", False, "", consulta, Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Etiqueta de Material"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then

            'codigo = paramreporte.ObtenerParametros(0).ToString
            codigo = Mid$(paramreporte.ObtenerParametros(0).ToString, 1, InStr(paramreporte.ObtenerParametros(0).ToString, "**") - 1).ToUpper
            'linea = Split(Trim(codigo_almacen.Replace(" "c, String.Empty)), "(")

            'If codigo_almacen.Trim <> "" Then codigo_almacen = Mid(linea(1).ToString, 1, Len(linea(1).ToString) - 1)

            rpt.MostrarEtiquetaMaterial(LTrim(RTrim(codigo)), rpt)

            cerroparametrosconaceptar = False
            paramreporte = Nothing
        End If

        Cursor = System.Windows.Forms.Cursors.Default
        Cnn = Nothing

    End Sub

    Private Sub MaquinasYHerramientasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaquinasYHerramientasToolStripMenuItem.Click
        frmMaqHerramientas.MdiParent = Me
        frmMaqHerramientas.Show()
    End Sub

    Private Sub VentasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VentasToolStripMenuItem.Click
        If sucursal.Contains("PERON") Then
            'frmVentas.MdiParent = Me
            'frmVentas.Show()
            frmVentasWEB.MdiParent = Me
            frmVentasWEB.Show()
        Else
            frmVentaSalon.MdiParent = Me
            frmVentaSalon.Show()
        End If
    End Sub

    Private Sub Materiales2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMateriales2.MdiParent = Me
        frmMateriales2.Show()
    End Sub

    Private Sub TablerosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TablerosToolStripMenuItem.Click
        frmTableros.MdiParent = Me
        frmTableros.Show()
    End Sub

    Private Sub EnsayosParaTransformadoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnsayosParaTransformadoresToolStripMenuItem.Click
        frmEnsayos.MdiParent = Me
        frmEnsayos.Show()
    End Sub



#Region "Reportes Contabilidad"

    Private Sub ExportarArchivosSIAPComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarArchivosSIAPComprasToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Try

            SqlHelper.ExecuteNonQuery(Cnn, CommandType.StoredProcedure, "spPeriodosFacturacion_Gastos")

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

            Exit Sub

        End Try


        Dim Periodo As String
        Dim Res As Integer

        Dim consulta As String = "SELECT PERIODO FROM tmpPeriodosFacturacion WHERE Tipo = 'Gastos' ORDER BY ano desc, mes desc"

        paramreporte.AgregarParametros("Periodo :", "STRING", "", False, "", consulta, Cnn)

        paramreporte.ShowDialog()

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Periodo = paramreporte.ObtenerParametros(0).ToString

            Dim connection As SqlClient.SqlConnection = Nothing

            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@Periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 20
                param_periodo.Value = Periodo
                param_periodo.Direction = ParameterDirection.Input

                Dim param_Tipo As New SqlClient.SqlParameter
                param_Tipo.ParameterName = "@Tipo"
                param_Tipo.SqlDbType = SqlDbType.VarChar
                param_Tipo.Size = 50
                param_Tipo.Value = "Gastos"
                param_Tipo.Direction = ParameterDirection.Input

                Dim param_Res As New SqlClient.SqlParameter
                param_Res.ParameterName = "@Res"
                param_Res.SqlDbType = SqlDbType.Int
                param_Res.Value = DBNull.Value
                param_Res.Direction = ParameterDirection.InputOutput

                SqlHelper.ExecuteNonQuery(Cnn, CommandType.StoredProcedure, "spImportarArchivoTXT_AFIPCompras", _
                                              param_periodo, param_Tipo, param_Res)

                Res = param_Res.Value

                If Res = 1 Then
                    MsgBox("Archivos generados correctamente en z:\MIT\SIAP\")
                Else
                    MsgBox("Se produjo un error al procesar los archivos")
                End If

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

                Exit Sub

            End Try

            cerroparametrosconaceptar = False
        End If

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub ExportarExcelImpuestossimilSIAPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarExcelImpuestossimilSIAPToolStripMenuItem.Click

        Dim excel As Microsoft.Office.Interop.Excel.Application

        'Dim workbook As Microsoft.Office.Interop.Excel.Workbook
        Dim oBook As Excel.WorkbookClass
        Dim oBooks As Excel.Workbooks

        Dim oSheet As Excel.Worksheet

        Dim paramreporte As New frmParametros
        Dim Connection As New SqlConnection(ConnStringSEI)

        Dim ds_Empresa As Data.DataSet


        Try

            SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "spPeriodosFacturacion_Gastos")

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

            Exit Sub

        End Try


        Dim Periodo As String = ""
        Dim Res As Integer

        Dim consulta As String = "SELECT PERIODO FROM tmpPeriodosFacturacion WHERE Tipo = 'Gastos' ORDER BY ano desc, mes desc"

        paramreporte.AgregarParametros("Periodo :", "STRING", "", False, "", consulta, Connection)

        paramreporte.ShowDialog()

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Periodo = paramreporte.ObtenerParametros(0).ToString

            'Dim connection As SqlClient.SqlConnection = Nothing

            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@Periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 20
                param_periodo.Value = Periodo
                param_periodo.Direction = ParameterDirection.Input

                Dim param_Tipo As New SqlClient.SqlParameter
                param_Tipo.ParameterName = "@Tipo"
                param_Tipo.SqlDbType = SqlDbType.VarChar
                param_Tipo.Size = 50
                param_Tipo.Value = "Gastos"
                param_Tipo.Direction = ParameterDirection.Input

                Dim param_Res As New SqlClient.SqlParameter
                param_Res.ParameterName = "@Res"
                param_Res.SqlDbType = SqlDbType.Int
                param_Res.Value = DBNull.Value
                param_Res.Direction = ParameterDirection.InputOutput

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "spRPT_IVACompras_Excel", _
                                              param_periodo, param_Tipo, param_Res)

                Res = param_Res.Value

                If Res <> 1 Then
                    MsgBox("Se produjo un error al procesar la solicitud")
                    Exit Sub
                End If

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

                CType(Connection, IDisposable).Dispose()

                Exit Sub

            End Try

            cerroparametrosconaceptar = False
        End If

        CType(paramreporte, IDisposable).Dispose()
        'CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default

        Try

            excel = New Microsoft.Office.Interop.Excel.Application

            If File.Exists(Application.StartupPath.ToString + "\Gastos\" & Util.Empresa & " Gastos - Periodo " & Periodo & ".xlsx") Then File.Delete(Application.StartupPath.ToString + "\Gastos\" & Util.Empresa & " Gastos - Periodo " & Periodo & ".xlsx")


            oBook = excel.Workbooks.Open(Application.StartupPath.ToString + "\Gastos\SEI - Gastos.xlsx")


            oBook.SaveAs(Application.StartupPath.ToString + "\Gastos\" & Util.Empresa & " Gastos - Periodo " & Periodo & ".xlsx")


            Dim celda As String

            oBooks = excel.Workbooks

            oSheet = oBook.Sheets(1)

            celda = "c" + CStr(8)

            'Try
            '    connection = SqlHelper.GetConnection(ConnStringFEAFIP)
            'Catch ex As Exception
            '    MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try

            Try

                Dim sqlstring As String

                sqlstring = " SELECT CONVERT(VARCHAR(10), [fechagasto], 103) AS Fecha, [nombre] ,[CUIT]  ,[DESCRIPCION],[nrofactura] " & _
                        " ,[CantIVA], subtotalExento, [subtotal] ,[iva10] ,[iva21] ,[iva27] ,[CONCEPTOSNOGRAVADOS],[IIBB] ,[IMPUESTOSINTERNOS] ,[OTROS] ,[PERCEPCIONIVA]" & _
                        " ,[PERCEPCIONIIBB] ,[Total], tipogasto FROM tmpLibroIVACompras ORDER BY FechaGasto ASC, NroFactura ASC"

                ds_Empresa = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sqlstring)
                ds_Empresa.Dispose()

                Dim Fila As Integer

                Fila = 0

                oSheet.Range("a2", "k500").Value = ""

                For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1
                    oSheet.Cells(Fila + 2, 1) = CDate(ds_Empresa.Tables(0).Rows(Fila)(0)) 'fecha factura
                    oSheet.Cells(Fila + 2, 2) = ds_Empresa.Tables(0).Rows(Fila)(1) 'Proveedor
                    oSheet.Cells(Fila + 2, 3) = ds_Empresa.Tables(0).Rows(Fila)(2) 'CUIT
                    oSheet.Cells(Fila + 2, 4) = ds_Empresa.Tables(0).Rows(Fila)(3) 'TIPO CMPR
                    oSheet.Cells(Fila + 2, 5) = ds_Empresa.Tables(0).Rows(Fila)(4) 'Nro Comp
                    oSheet.Cells(Fila + 2, 6) = ds_Empresa.Tables(0).Rows(Fila)(5) 'Cant IVA
                    oSheet.Cells(Fila + 2, 7) = ds_Empresa.Tables(0).Rows(Fila)(6) 'SubtotalNogravado
                    oSheet.Cells(Fila + 2, 8) = ds_Empresa.Tables(0).Rows(Fila)(7) 'sUBTOTAL
                    oSheet.Cells(Fila + 2, 9) = ds_Empresa.Tables(0).Rows(Fila)(8) 'IVA10
                    oSheet.Cells(Fila + 2, 10) = ds_Empresa.Tables(0).Rows(Fila)(9) 'iva21
                    oSheet.Cells(Fila + 2, 11) = ds_Empresa.Tables(0).Rows(Fila)(10) 'iva27
                    oSheet.Cells(Fila + 2, 12) = ds_Empresa.Tables(0).Rows(Fila)(11) 'conceptosNoGravados
                    oSheet.Cells(Fila + 2, 13) = ds_Empresa.Tables(0).Rows(Fila)(12) 'IIBB
                    oSheet.Cells(Fila + 2, 14) = ds_Empresa.Tables(0).Rows(Fila)(13) 'IMPUESTOS INTERNOS
                    oSheet.Cells(Fila + 2, 15) = ds_Empresa.Tables(0).Rows(Fila)(14) 'Otros
                    oSheet.Cells(Fila + 2, 16) = ds_Empresa.Tables(0).Rows(Fila)(15) 'Percep IVA
                    oSheet.Cells(Fila + 2, 17) = ds_Empresa.Tables(0).Rows(Fila)(16) 'Percep IIBB
                    oSheet.Cells(Fila + 2, 18) = ds_Empresa.Tables(0).Rows(Fila)(17) 'total
                    oSheet.Cells(Fila + 2, 19) = ds_Empresa.Tables(0).Rows(Fila)(18) 'total
                Next

                oSheet.Cells(Fila + 2, 7).FormulaLocal = "=suma(G1:G" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 8).FormulaLocal = "=suma(H1:H" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 9).FormulaLocal = "=suma(I1:I" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 10).FormulaLocal = "=suma(J1:J" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 11).FormulaLocal = "=suma(K1:K" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 12).FormulaLocal = "=suma(L1:L" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 13).FormulaLocal = "=suma(M1:M" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 14).FormulaLocal = "=suma(N1:N" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 15).FormulaLocal = "=suma(O1:O" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 16).FormulaLocal = "=suma(P1:P" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 17).FormulaLocal = "=suma(Q1:Q" & (Fila + 1).ToString & ")"
                oSheet.Cells(Fila + 2, 18).FormulaLocal = "=suma(R1:R" & (Fila + 1).ToString & ")"

                oSheet.Columns.AutoFit()

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
                If Not Connection Is Nothing Then
                    CType(Connection, IDisposable).Dispose()
                End If
            End Try

            excel.Visible = True

            oBook.Activate()

        Catch ex As COMException
            MessageBox.Show("Error accessing Excel: " + ex.ToString())

        Catch ex As Exception
            MessageBox.Show("Error: " + ex.ToString())

        End Try

        CType(Connection, IDisposable).Dispose()


    End Sub

    Private Sub GastosPorTipoGraficoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GastosPorTipoGraficoToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Gastos por Tipo"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString

            rpt.GastosTipos_App(Inicial, Final, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

    Private Sub GastosPorProveedorGráficoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GastosPorProveedorGráficoToolStripMenuItem.Click
        Dim paramreporte As New frmParametros
        Dim rpt As New frmReportes
        Dim Cnn As New SqlConnection(ConnStringSEI)

        Dim Inicial As String = "01/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)
        Dim Final As String = Mid$(Now, 1, 2) & "/" & Mid$(Now, 4, 2) & "/" & Mid$(Now, 7, 4)

        paramreporte.AgregarParametros("Inicio :", "DATE", "", False, Inicial, "", Cnn)
        paramreporte.AgregarParametros("Fin :", "DATE", "", False, Final, "", Cnn)

        paramreporte.ShowDialog()

        nbreformreportes = "Gastos por Proveedor"

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        If cerroparametrosconaceptar = True Then
            Inicial = paramreporte.ObtenerParametros(0).ToString
            Final = paramreporte.ObtenerParametros(1).ToString

            rpt.GastosProveedores_App(Inicial, Final, rpt, My.Application.Info.AssemblyName.ToString)

            cerroparametrosconaceptar = False
        End If

        'paramreporte = Nothing

        CType(paramreporte, IDisposable).Dispose()
        CType(Cnn, IDisposable).Dispose()

        Cursor = System.Windows.Forms.Cursors.Default
        'Cnn = Nothing
    End Sub

#End Region

    Private Sub DepósitoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepósitoToolStripMenuItem.Click
        frmAlmacenes.MdiParent = Me
        frmAlmacenes.Show()
    End Sub

    Private Sub JornadasDeTrabajoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JornadasDeTrabajoToolStripMenuItem.Click
        frmJornadas.MdiParent = Me
        frmJornadas.Show()
    End Sub

    Private Sub ConsolidacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsolidacionesToolStripMenuItem.Click
        frmConsolidaciones.MdiParent = Me
        frmConsolidaciones.Show()
    End Sub

    Private Sub GeneraciónDeOTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeneraciónDeOTToolStripMenuItem.Click
        frmOrdenTrabajo.MdiParent = Me
        frmOrdenTrabajo.Show()
    End Sub

    Private Sub AsignaciónDeMaterialesAOTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsignaciónDeMaterialesAOTToolStripMenuItem.Click
        frmOrdenTrabajo_Det.MdiParent = Me
        frmOrdenTrabajo_Det.Show()
    End Sub

    Private Sub RequerimientosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequerimientosToolStripMenuItem.Click
        frmRequirimientos.MdiParent = Me
        frmRequirimientos.Show()
    End Sub

    Private Sub GastosPorVencidosPorVencerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GastosPorVencidosPorVencerToolStripMenuItem.Click
        Dim rpt As New frmReportes
        Cursor = Cursors.WaitCursor

        rpt.GastosVencidosPorVencer_App(rpt, My.Application.Info.AssemblyName.ToString)

        Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub RemitosSinPresupuestoMatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemitosSinPresupuestoMatToolStripMenuItem.Click
        frmRemitosSinPresupuesto.MdiParent = Me
        frmRemitosSinPresupuesto.Show()
    End Sub

    Private Sub ListaPreciosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListaPreciosToolStripMenuItem.Click
        frmListaPrecios.MdiParent = Me
        frmListaPrecios.Show()
    End Sub

    Private Sub VentasDepositoPerónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasDepositoPerónToolStripMenuItem.Click
        frmVentas.MdiParent = Me
        frmVentas.Show()
    End Sub

    Public Sub ActualizarSistemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarSistemaToolStripMenuItem.Click
        'If NoActualizar = False Then 'Not SystemInformation.ComputerName.ToString.ToUpper = "SAMBA-PC" Then
        '    ActualizarSistema(False)
        'End If
        frmActuliazarSistema.MdiParent = Me
        frmActuliazarSistema.Show()
    End Sub

    Public Sub ActualizarSistema(ByVal desdePedidos As Boolean)

        Dim ds_Empresa As Data.DataSet
        Dim Conexion As SqlConnection
        'llena la tabla temporal que tendrá solo los materiales nuevos que se han agregado al sistema
        Dim sqlstring As String
        'avisa que esta en la sucursal de peron 
        Dim PERON As Boolean = False
        'esta variable la utilizo para descargar siempre el stock del otro almacen 
        Dim otroAlmacen As Integer

        If sucursal.Contains("PERON") Then
            ' ---PERÓN---
            Conexion = New SqlConnection("Data Source=PORKIS-SERVER;Initial Catalog=Porkys;User ID=sa;Password=industrial")
            otroAlmacen = 1
            PERON = True
        Else
            '---MARCONI---
            Conexion = New SqlConnection("Data Source=servidor;Initial Catalog=Porkys;User ID=sa;Password=industrial")
            otroAlmacen = 2
            PERON = False
        End If

        '---MI MAQUINA---
        'Conexion = New SqlConnection("Data Source=SAMBA-PC;Initial Catalog=Porkys;User ID=sa;Password=industrial")
        'otroAlmacen = 1
        'PERON = True
        'PERON = False
        '---Analia---
        'Conexion = New SqlConnection("Data Source=SilvaAnalia-PC;Initial Catalog=Porkys;User ID=sa;Password=industrial")

        Dim prueba As String

        If desdePedidos = True Then
            GoTo Pedidos
        End If

        '********************************************************STOCK**********************************************************

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


        '*****************************************************************MATERIALES**********************************************
        Dim ds_Marcas As Data.DataSet

        'ME FIJO SI LA SUCURSAL ES PERON
        If PERON = True Then

            If MessageBox.Show("Está seguro que desea Actualizar los Materiales?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Try

                    ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpMateriales_Web")
                    ds_Empresa.Dispose()

                    Dim ds_Clientes As DataSet = tranWEB.Sql_Get("SELECT * FROM Materiales ")

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
                            " [Cambiar1] ,[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista]" & _
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
                                " [UnidadRef],[Cambiar1] ,[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista]) " & _
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
                                IIf(CBool(ds_Empresa.Tables(0).Rows(i)(34)) = True, 1, 0) & "); " & _
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
                            " [PrecioLista3],[IDLista3],[PrecioLista4],[IDLista4],[UnidadRef],[Cambiar1] ,[Cambiar2],[Cambiar3],[Cambiar4],[VentaMayorista] FROM tmpMateriales_Web "

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
                            " [VentaMayorista] = " & IIf(CBool(ds_Empresa.Tables(0).Rows(i)(34)) = True, 1, 0) & " " & _
                            " WHERE Codigo = '" & ds_Empresa.Tables(0).Rows(i)(3) & "'"

                        prueba = ds_Empresa.Tables(0).Rows(i)(3).ToString

                        ds_tmpClientes = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
                        ds_tmpClientes.Dispose()

                    Next

                Catch ex As Exception
                    MsgBox(" Desde Actualización Materiales " + ex.Message)
                End Try

            End If

            ''**************************************************************************LISTA_PRECIOS****************************************************************************

            Dim ds_Lista As Data.DataSet
            'Dim Conexion As SqlConnection
            'llena la tabla temporal que tendrá solo los materiales nuevos que se han agregado al sistema
            'Dim sqlstring As String
            If MessageBox.Show("Está seguro que desea Actualizar la Lista de Precios?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
                End Try

            End If
            'End If

            ''**************************************************************************Unidades****************************************************************************
            Dim ds_Unidades As Data.DataSet
            If MessageBox.Show("Está seguro que desea Actualizar las Unidades?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
                End Try
            End If


            ''**************************************************************************Rubros****************************************************************************
            Dim ds_Rubros As Data.DataSet
            If MessageBox.Show("Está seguro que desea Actualizar los Rubros?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
                End Try
            End If

            ''**************************************************************************Marcas****************************************************************************

            If MessageBox.Show("Está seguro que desea Actualizar las Marcas?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
                End Try
            End If



            '***************************************************************************************clientes**********************************************************************
            Dim ds_Client As Data.DataSet
            If MessageBox.Show("Está seguro que desea Actualizar los Clientes?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
                    MsgBox(sqlstring)
                    MsgBox(" Desde Actualización clientes " + ex.Message)

                End Try
            End If



            '*******************************************************************************empleados**********************************************************************
            Dim ds_Empleado As Data.DataSet

            If MessageBox.Show("Está seguro que desea Actualizar los Empleados?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
                End Try
            End If



        End If

        '***************************************************************************************Pedidos_WEB*******************************************************************

Pedidos:

        If PERON = True Then
            Exit Sub
        Else
            If MessageBox.Show("Está seguro que desea descargar los Pedidos de la WEB?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            Else
                MsgBox("Descarga de pedidos WEB temporalmente inactivo.")
            End If
        End If


        '------------------------------temporalmente inactivo-----------------------------
        'Dim sqlstring2 As String
        'Dim ds_Pedidos As Data.DataSet
        'Dim EncabezadoWEB_Filas As Integer = 0
        'Dim EncabezadoLocal_Filas As Integer = 0

        'Dim DetalleWEB_Filas As Integer = 0
        'Dim DetalleLocal_Filas As Integer = 0

        'Try
        '    'borro las temporales
        '    ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpPedidosWEB_WEB ")
        '    ds_Pedidos.Dispose()
        '    ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "truncate table tmpPedidosWEB_det_WEB ")
        '    ds_Pedidos.Dispose()

        '    'cuento las filas del encabezado web
        '    Dim ds_PedidosWEB As DataSet = tranWEB.Sql_Get("SELECT count(*) FROM PedidosWEB ")
        '    EncabezadoWEB_Filas = CInt(ds_PedidosWEB.Tables(0).Rows(0).Item(0))

        '    'traigo los valores del encabezado
        '    Dim ds_FamiliasWEB As DataSet = tranWEB.Sql_Get("SELECT * FROM PedidosWEB ")
        '    Dim bulk_familias As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)

        '    'alojo los datos en la tabla temporal
        '    Conexion.Open()
        '    bulk_familias.DestinationTableName = "tmpPedidosWEB_WEB"
        '    bulk_familias.WriteToServer(ds_FamiliasWEB.Tables(0))
        '    Conexion.Close()
        '    'cuento las filas locales 
        '    sqlstring = " SELECT count(*) FROM tmpPedidosWEB_WEB"

        '    ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
        '    ds_Pedidos.Dispose()
        '    EncabezadoLocal_Filas = CInt(ds_Pedidos.Tables(0).Rows(0).Item(0))
        '    'comparo la cantidad de filas de la web con las locales 
        '    If EncabezadoWEB_Filas = EncabezadoLocal_Filas Then


        '        'me fijo en el detalle en la web 
        '        ds_PedidosWEB = tranWEB.Sql_Get("SELECT count(*) FROM PedidosWEB_det ")
        '        DetalleWEB_Filas = CInt(ds_PedidosWEB.Tables(0).Rows(0).Item(0))

        '        'traigo los valores de los detalles del a web 
        '        Dim ds_FamiliasWEB_det = tranWEB.Sql_Get("SELECT * FROM PedidosWEB_det ")
        '        Dim bulk_familias_det As New SqlBulkCopy(Conexion, SqlBulkCopyOptions.TableLock, Nothing)
        '        'los guardo en los valores de detalle en la temporal 
        '        Conexion.Open()
        '        bulk_familias_det.DestinationTableName = "tmpPedidosWEB_det_WEB"
        '        bulk_familias_det.WriteToServer(ds_FamiliasWEB_det.Tables(0))
        '        Conexion.Close()

        '        'cuanto las filas de la tabla temporal 
        '        sqlstring = " SELECT count(*) FROM tmpPedidosWEB_det_WEB "

        '        ds_Pedidos = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
        '        ds_Pedidos.Dispose()
        '        DetalleLocal_Filas = CInt(ds_Pedidos.Tables(0).Rows(0).Item(0))

        '        If DetalleWEB_Filas = DetalleLocal_Filas Then

        '            'pedidos que están en la WEB y no están en la sucursal/central
        '            sqlstring = " SELECT [IDAlmacen],[OrdenPedido],[Fecha],[IdCliente],[FechaEntrega]," & _
        '                        " [LugarEntrega],[Status],[Subtotal],[Iva],[Total],[Nota],[Deuda],[Cancelado],[Eliminado],[IDEmpleado]" & _
        '                        " FROM tmpPedidosWEB_WEB WHERE OrdenPedido NOT IN (SELECT OrdenPedido FROM PedidosWEB ) "

        '            ds_Marcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
        '            ds_Marcas.Dispose()

        '            sqlstring2 = "SELECT [IDPedidosWEB],[IDMaterial],[IDMarca],[IDUnidad],[Precio],[QtyPedida],[QtyEnviada],[Subtotal],[IVA]," & _
        '                        " [MontoIVA],[Status],[QtySaldo],[FechaCumplido],[OrdenItem],[Nota_Det],[UnidadFac],[Eliminado] " & _
        '                        " FROM tmpPedidosWEB_det_WEB WHERE IDPedidosWEB NOT IN (SELECT IDPedidosWEB FROM PedidosWEB_det ) "

        '            ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring2)
        '            ds_Empresa.Dispose()

        '            Dim i As Integer = 0
        '            Dim j As Integer = 0
        '            Dim ds_tmpMarcas As Data.DataSet
        '            Dim ds_tmpEmpresa As Data.DataSet
        '            Dim ds_tmpEmpresaInsert As Data.DataSet
        '            Dim ds_Marcas2 As Data.DataSet
        '            Dim ds_Empresa2 As Data.DataSet

        '            Dim sqlstring_pasar As String
        '            Dim sqlstring_pasardet As String
        '            Dim sqlstring_pasardetADD As String
        '            Dim sqlstring_insertADD As String

        '            'me fijo haya datos en el encabezado y el detalle 
        '            If ds_Marcas.Tables(0).Rows.Count > 0 And ds_Empresa.Tables(0).Rows.Count > 0 Then
        '                For i = 0 To ds_Marcas.Tables(0).Rows.Count - 1

        '                    sqlstring_pasar = " BEGIN TRAN; " & _
        '                        " INSERT INTO [dbo].[PedidosWEB] ([IDAlmacen],[OrdenPedido],[Fecha],[IdCliente],[FechaEntrega]," & _
        '                        " [LugarEntrega],[Status],[Subtotal],[Iva],[total],[Nota],[Deuda],[Cancelado],[Eliminado],[IDEmpleado]) " & _
        '                        " VALUES ( " & ds_Marcas.Tables(0).Rows(i)(0) & ", '" & ds_Marcas.Tables(0).Rows(i)(1) & "', '" & _
        '                        Format(ds_Marcas.Tables(0).Rows(i)(2), "dd/MM/yyyy hh:ss") & "','" & ds_Marcas.Tables(0).Rows(i)(3) & "','" & _
        '                        Format(ds_Marcas.Tables(0).Rows(i)(4), "dd/MM/yyyy hh:ss") & "','" & ds_Marcas.Tables(0).Rows(i)(5) & "','" & _
        '                        ds_Marcas.Tables(0).Rows(i)(6) & "'," & ds_Marcas.Tables(0).Rows(i)(7) & "," & ds_Marcas.Tables(0).Rows(i)(8) & "," & _
        '                        ds_Marcas.Tables(0).Rows(i)(9) & ",'" & ds_Marcas.Tables(0).Rows(i)(10) & "'," & ds_Marcas.Tables(0).Rows(i)(11) & "," & _
        '                        IIf(CBool(ds_Marcas.Tables(0).Rows(i)(12)) = True, 1, 0) & " , " & IIf(CBool(ds_Marcas.Tables(0).Rows(i)(13)) = True, 1, 0) & ",'" & _
        '                        ds_Marcas.Tables(0).Rows(i)(14) & "'); " & _
        '                        " COMMIT TRAN;"

        '                    For j = 0 To ds_Empresa.Tables(0).Rows.Count - 1
        '                        'controlo que el numero de orden coincidan y hago el insert del detalle 
        '                        If ds_Marcas.Tables(0).Rows(i)(1).ToString = ds_Empresa.Tables(0).Rows(j)(0).ToString Then

        '                            sqlstring_pasardet = " BEGIN TRAN; " & _
        '                            " INSERT INTO [dbo].[PedidosWEB_det] ([IDPedidosWEB],[IDMaterial],[IDMarca],[IDUnidad],[Precio],[QtyPedida]," & _
        '                            " [QtyEnviada],[Subtotal],[IVA],[MontoIVA],[Status],[QtySaldo],[FechaCumplido],[OrdenItem],[Nota_Det],[UnidadFac],[Eliminado]) " & _
        '                            " VALUES ( '" & ds_Empresa.Tables(0).Rows(j)(0) & "', '" & ds_Empresa.Tables(0).Rows(j)(1) & "', '" & _
        '                             ds_Empresa.Tables(0).Rows(j)(2) & "', '" & ds_Empresa.Tables(0).Rows(j)(3) & "'," & ds_Empresa.Tables(0).Rows(j)(4) & "," & _
        '                             ds_Empresa.Tables(0).Rows(j)(5) & ", " & ds_Empresa.Tables(0).Rows(j)(6) & "," & ds_Empresa.Tables(0).Rows(j)(7) & "," & _
        '                             ds_Empresa.Tables(0).Rows(j)(8) & ", " & ds_Empresa.Tables(0).Rows(j)(9) & ",'" & ds_Empresa.Tables(0).Rows(j)(10) & "'," & _
        '                             ds_Empresa.Tables(0).Rows(j)(11) & ", '" & Format(ds_Empresa.Tables(0).Rows(j)(12), "dd/MM/yyyy hh:ss") & "'," & _
        '                             ds_Empresa.Tables(0).Rows(j)(13) & ",'" & ds_Empresa.Tables(0).Rows(j)(14) & "'," & ds_Empresa.Tables(0).Rows(j)(15) & "," & IIf(CBool(ds_Empresa.Tables(0).Rows(j)(16)) = True, 1, 0) & " ); " & _
        '                            " COMMIT TRAN;"

        '                            ds_tmpEmpresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasardet)
        '                            ds_tmpEmpresa.Dispose()

        '                        End If

        '                    Next

        '                    ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasar)
        '                    ds_tmpMarcas.Dispose()

        '                Next
        '            End If
        '            '-------------------------------------------update de los pedidos--------------------------------------
        '            sqlstring = " SELECT [IDAlmacen],[OrdenPedido],[Fecha],[IdCliente],[FechaEntrega]," & _
        '                       " [LugarEntrega],[Status],[Subtotal],[Iva],[Total],[Nota],[Deuda],[Cancelado],[Eliminado],[IDEmpleado]" & _
        '                       " FROM tmpPedidosWEB_WEB "

        '            ds_Marcas2 = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
        '            ds_Marcas2.Dispose()


        '            sqlstring2 = "SELECT [IDPedidosWEB],[IDMaterial],[IDMarca],[IDUnidad],[Precio],[QtyPedida],[QtyEnviada],[Subtotal],[IVA]," & _
        '                        " [MontoIVA],[Status],[QtySaldo],[FechaCumplido],[OrdenItem],[Nota_Det],[UnidadFac],[Eliminado] " & _
        '                        " FROM tmpPedidosWEB_det_WEB "

        '            ds_Empresa2 = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring2)
        '            ds_Empresa2.Dispose()

        '            'controlo que haya datos en las tablas temporales 
        '            If ds_Marcas2.Tables(0).Rows.Count > 0 And ds_Empresa2.Tables(0).Rows.Count > 0 Then
        '                For i = 0 To ds_Marcas2.Tables(0).Rows.Count - 1

        '                    'sqlstring_pasar =
        '                    '    "UPDATE [dbo].[PedidosWEB] SET  [IDAlmacen] = " & ds_Marcas2.Tables(0).Rows(i)(0) & "," & _
        '                    '    "[Fecha] = '" & Format(ds_Marcas2.Tables(0).Rows(i)(2), "dd/MM/yyyy hh:ss") & "'," & _
        '                    '    "[IdCliente] = '" & ds_Marcas2.Tables(0).Rows(i)(3) & "'," & _
        '                    '    "[FechaEntrega] = '" & Format(ds_Marcas2.Tables(0).Rows(i)(4), "dd/MM/yyyy hh:ss") & "'," & _
        '                    '    "[LugarEntrega] = '" & ds_Marcas2.Tables(0).Rows(i)(5) & "'," & _
        '                    '    "[Status] = '" & ds_Marcas2.Tables(0).Rows(i)(6) & "'," & _
        '                    '    "[Subtotal] = " & ds_Marcas2.Tables(0).Rows(i)(7) & "," & _
        '                    '    "[Iva] = " & ds_Marcas2.Tables(0).Rows(i)(8) & "," & _
        '                    '    "[total] = " & ds_Marcas2.Tables(0).Rows(i)(9) & "," & _
        '                    '    "[Nota] = '" & ds_Marcas2.Tables(0).Rows(i)(10) & "'," & _
        '                    '    "[Deuda] = " & ds_Marcas2.Tables(0).Rows(i)(11) & "," & _
        '                    '    "[Cancelado] = " & IIf(CBool(ds_Marcas2.Tables(0).Rows(i)(12)) = True, 1, 0) & "," & _
        '                    '    "[Eliminado] = " & IIf(CBool(ds_Marcas2.Tables(0).Rows(i)(13)) = True, 1, 0) & "," & _
        '                    '    "[IDEmpleado] = '" & ds_Marcas2.Tables(0).Rows(i)(14) & "'" & _
        '                    '    " WHERE [OrdenPedido] = '" & ds_Marcas2.Tables(0).Rows(i)(1) & "'"

        '                    sqlstring_pasar =
        '                    "UPDATE [dbo].[PedidosWEB] SET  " & _
        '                    "[Eliminado] = " & IIf(CBool(ds_Marcas2.Tables(0).Rows(i)(13)) = True, 1, 0) & " " & _
        '                    " WHERE [OrdenPedido] = '" & ds_Marcas2.Tables(0).Rows(i)(1) & "'"


        '                    For j = 0 To ds_Empresa2.Tables(0).Rows.Count - 1
        '                        'controlo que el numero de orden coincidan y hago el insert del detalle 
        '                        If ds_Marcas2.Tables(0).Rows(i)(1).ToString = ds_Empresa2.Tables(0).Rows(j)(0).ToString Then

        '                            'sqlstring_pasardet = " UPDATE [dbo].[PedidosWEB_det] SET " & _
        '                            '                     "[IDMarca] = '" & ds_Empresa2.Tables(0).Rows(j)(2) & "'," & _
        '                            '                     "[IDUnidad] = '" & ds_Empresa2.Tables(0).Rows(j)(3) & "'," & _
        '                            '                     "[Precio] = " & ds_Empresa2.Tables(0).Rows(j)(4) & "," & _
        '                            '                     "[QtyPedida] = " & ds_Empresa2.Tables(0).Rows(j)(5) & "," & _
        '                            '                     "[QtyEnviada] = " & ds_Empresa2.Tables(0).Rows(j)(6) & "," & _
        '                            '                     "[Subtotal] = " & ds_Empresa2.Tables(0).Rows(j)(7) & "," & _
        '                            '                     "[IVA] = " & ds_Empresa2.Tables(0).Rows(j)(8) & "," & _
        '                            '                     "[MontoIVA] = " & ds_Empresa2.Tables(0).Rows(j)(9) & "," & _
        '                            '                     "[Status] = '" & ds_Empresa2.Tables(0).Rows(j)(10) & "'," & _
        '                            '                     "[QtySaldo] = " & ds_Empresa2.Tables(0).Rows(j)(11) & "," & _
        '                            '                     "[FechaCumplido] = '" & Format(ds_Empresa2.Tables(0).Rows(j)(12), "dd/MM/yyyy hh:ss") & "'," & _
        '                            '                     "[OrdenItem] = " & ds_Empresa2.Tables(0).Rows(j)(13) & "," & _
        '                            '                     "[Nota_Det] = '" & ds_Empresa2.Tables(0).Rows(j)(14) & "'," & _
        '                            '                     "[UnidadFac] = " & ds_Empresa2.Tables(0).Rows(j)(15) & "," & _
        '                            '                     "[Eliminado] = " & IIf(CBool(ds_Empresa2.Tables(0).Rows(j)(16)) = True, 1, 0) & " " & _
        '                            '                     " WHERE [IDPedidosWEB] = '" & ds_Empresa2.Tables(0).Rows(j)(0) & "' AND [IDMaterial] = " & ds_Empresa2.Tables(0).Rows(j)(1) & ""

        '                            sqlstring_pasardet = " UPDATE [dbo].[PedidosWEB_det] SET " & _
        '                                                "[QtyPedida] = " & ds_Empresa2.Tables(0).Rows(j)(5) & "," & _
        '                                                "[Eliminado] = " & IIf(CBool(ds_Empresa2.Tables(0).Rows(j)(16)) = True, 1, 0) & " " & _
        '                                                " WHERE [IDPedidosWEB] = '" & ds_Empresa2.Tables(0).Rows(j)(0) & "' AND [IDMaterial] = " & ds_Empresa2.Tables(0).Rows(j)(1) & ""

        '                            Dim idpedidosweb As String = ds_Empresa2.Tables(0).Rows(j)(0)

        '                            ds_tmpEmpresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasardet)
        '                            ds_tmpEmpresa.Dispose()

        '                            '----------------------------------------------------------- update de los pedidos donde se haya agregado un item--------------------------------------------------

        '                            sqlstring_pasardetADD = "SELECT [IDPedidosWEB],[IDMaterial],[IDMarca],[IDUnidad],[Precio],[QtyPedida],[QtyEnviada],[Subtotal],[IVA],[MontoIVA],[Status],[QtySaldo]," & _
        '                                                    "[FechaCumplido],[OrdenItem],[Nota_Det],[UnidadFac],[Eliminado] FROM tmpPedidosWEB_det_WEB WHERE IDPedidosWEB = '" & idpedidosweb & "'" & _
        '                                                    " AND IdMaterial NOT IN (SELECT IdMaterial FROM PedidosWEB_det where IDPedidosWEB = '" & idpedidosweb & "') "

        '                            ds_tmpEmpresaInsert = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasardetADD)
        '                            ds_tmpEmpresaInsert.Dispose()

        '                            'me fijo si hay algun item para insertar 
        '                            If ds_tmpEmpresaInsert.Tables(0).Rows.Count > 0 Then

        '                                For k As Integer = 0 To ds_tmpEmpresaInsert.Tables(0).Rows.Count - 1

        '                                    sqlstring_insertADD = " BEGIN TRAN; " & _
        '                               " INSERT INTO [dbo].[PedidosWEB_det] ([IDPedidosWEB],[IDMaterial],[IDMarca],[IDUnidad],[Precio],[QtyPedida]," & _
        '                               " [QtyEnviada],[Subtotal],[IVA],[MontoIVA],[Status],[QtySaldo],[FechaCumplido],[OrdenItem],[Nota_Det],[UnidadFac],[Eliminado]) " & _
        '                               " VALUES ( '" & ds_tmpEmpresaInsert.Tables(0).Rows(k)(0) & "', '" & ds_tmpEmpresaInsert.Tables(0).Rows(k)(1) & "', '" & _
        '                                ds_tmpEmpresaInsert.Tables(0).Rows(k)(2) & "', '" & ds_tmpEmpresaInsert.Tables(0).Rows(k)(3) & "'," & ds_tmpEmpresaInsert.Tables(0).Rows(k)(4) & "," & _
        '                                ds_tmpEmpresaInsert.Tables(0).Rows(k)(5) & ", " & ds_tmpEmpresaInsert.Tables(0).Rows(k)(6) & "," & ds_tmpEmpresaInsert.Tables(0).Rows(k)(7) & "," & _
        '                                ds_tmpEmpresaInsert.Tables(0).Rows(k)(8) & ", " & ds_tmpEmpresaInsert.Tables(0).Rows(k)(9) & ",'" & ds_tmpEmpresaInsert.Tables(0).Rows(k)(10) & "'," & _
        '                                ds_tmpEmpresaInsert.Tables(0).Rows(k)(11) & ", '" & Format(ds_tmpEmpresaInsert.Tables(0).Rows(k)(12), "dd/MM/yyyy hh:ss") & "'," & _
        '                                ds_tmpEmpresaInsert.Tables(0).Rows(k)(13) & ",'" & ds_tmpEmpresaInsert.Tables(0).Rows(k)(14) & "'," & ds_tmpEmpresaInsert.Tables(0).Rows(k)(15) & "," & _
        '                                IIf(CBool(ds_tmpEmpresaInsert.Tables(0).Rows(k)(16)) = True, 1, 0) & " ); " & _
        '                               " COMMIT TRAN;"

        '                                    ds_Empresa = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_insertADD)
        '                                    ds_Empresa.Dispose()

        '                                Next

        '                            End If
        '                        End If

        '                    Next

        '                    ds_tmpMarcas = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring_pasar)
        '                    ds_tmpMarcas.Dispose()
        '                Next

        '            End If
        '        Else
        '            MsgBox("No se pudo sincronizar los datos de pedidos (detalle) de la WEB. Por Favor intente más tarde.")
        '            Exit Sub
        '        End If
        '    Else
        '        MsgBox("No se pudo sincronizar los datos de pedidos (encabezado) de la WEB. Por Favor intente más tarde.")
        '        Exit Sub
        '    End If


        'Catch ex As Exception

        '    MsgBox("Desde PedidosWEB " + ex.Message)
        'End Try
        '------------------------------temporalmente inactivo-----------------------------

        ToolStripStatusLabel.BackColor = Color.Lime
        ToolStripStatusLabel.Text = "Status"


    End Sub

    Public Function ControlFirewall() As Boolean

        Try
            Const NET_FW_PROFILE2_DOMAIN = 1
            Const NET_FW_PROFILE2_PRIVATE = 2
            Const NET_FW_PROFILE2_PUBLIC = 4

            Dim fwPolicy2
            fwPolicy2 = CreateObject("HNetCfg.FwPolicy2")
            fwPolicy2.FirewallEnabled(NET_FW_PROFILE2_DOMAIN) = False
            fwPolicy2.FirewallEnabled(NET_FW_PROFILE2_PRIVATE) = False
            fwPolicy2.FirewallEnabled(NET_FW_PROFILE2_PUBLIC) = False

            'MsgBox("FIREWALL DESACTIVADO")
            ControlFirewall = True
        Catch ex As Exception

            'MsgBox("Por favor ejecute el Sistema en Modo Administrador para poder desactivar firewall.")
            ControlFirewall = False
        End Try

    End Function

    Public Function ControlUsuarioAutorizado(ByVal Usuario As String) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select Autoriza from Empleados where Eliminado = 0 and Codigo = '" & Usuario & "'")
        ds.Dispose()

        If ds.Tables(0).Rows(0).Item(0) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub EnvíosDePedidosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvíosDePedidosToolStripMenuItem.Click


        Dim abrir As Boolean = False

        ActualizarSistema(True)

        abrir = True

        If abrir = True Then
            frmPedidosWEB.MdiParent = Me
            frmPedidosWEB.Show()
        End If



    End Sub

    Private Sub ReportesDepositoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportesDepositoToolStripMenuItem.Click

        frmReportesDeposito.MdiParent = Me
        frmReportesDeposito.Show()

    End Sub

    Private Sub VentasDepósitoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasDepósitoToolStripMenuItem.Click

        frmVentasWEB.MdiParent = Me
        frmVentasWEB.Show()

    End Sub

    Private Sub VentasPorClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasPorClientesToolStripMenuItem.Click

        frmventasclientes.MdiParent = Me
        frmventasclientes.Show()

    End Sub

    Private Sub PromocionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PromocionesToolStripMenuItem.Click

        frmPromocionesPorkys.MdiParent = Me
        frmPromocionesPorkys.Show()

    End Sub

    Private Sub TransferenciasToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles TransferenciasToolStripMenuItem.Click

        frmTransferenciasPorkys.MdiParent = Me
        frmTransferenciasPorkys.Show()

    End Sub

    Private Sub MovimientosDelDíaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MovimientosDelDíaToolStripMenuItem.Click
    
        frmMovDia_Caja.MdiParent = Me
        frmMovDia_Caja.Show()
    End Sub

    Private Sub AnticiposYIngresosDepósitoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnticiposYIngresosDepósitoToolStripMenuItem.Click
        frmAnticiposIngresosDep.MdiParent = Me
        frmAnticiposIngresosDep.Show()
    End Sub

    Private Sub TarjetasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TarjetasToolStripMenuItem.Click
        frmTarjetas.MdiParent = Me
        frmTarjetas.Show()
    End Sub

    Private Sub MovimientosDelDíaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MovimientosDelDíaToolStripMenuItem1.Click
        frmMovDia.MdiParent = Me
        frmMovDia.Show()
    End Sub

    Private Sub ProductosMásSolicitadosToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProductosMásSolicitadosToolStripMenuItem1.Click
        frmMateriales_MasSolicitados.MdiParent = Me
        frmMateriales_MasSolicitados.Show()
    End Sub

    Private Sub PresentacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PresentacionesToolStripMenuItem.Click
        frmPresentaciones.MdiParent = Me
        frmPresentaciones.Show()
    End Sub
End Class
