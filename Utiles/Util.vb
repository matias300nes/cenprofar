Imports System.Web
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
'Imports System
'Imports System.ServiceProcess
'Imports System.IO

Public Module Util

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'ESTRUCTURAS                                                                                              '      
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Usadas por los reportes para saber el server,nombre de la base,usuario,password 
    ''ESTA ESTRUCTURA ES PARA MANTENER EL SERVIDOR, BASE DE DATOS, USUARIO Y CONTRASEÑA DE CADA UNA DE LAS 
    ''SECCIONES DEL INI DEL USUARIO, ESTO ES PORQUE PARA LOS REPORTES SE NECESITA ESTA INFO POR SEPARADO.
    Structure TipoConexion
        Public server As String
        Public base As String
        Public usuario As String
        Public contrasena As String
    End Structure

    ' nuevo cp 18-10-2011 agregado para los instrumentos
    Structure Est_Instrumentos
        Public IDControl As String
        Public CodInstrumento As String
        Public IDInstrumento As String
    End Structure


    'dg 14-03-2012
    Public id_stock_ayuda_consumo_prod As Long
    Public lote_ayuda_consumo_prod As Integer
    Public qty_ayuda_consumo_prod As Decimal
    Public status_ayuda_consumo_prod As String
    Public loteprov_ayuda_consumo_prod As String
    Public viene_ayuda_consumo_prod As Boolean

    'cp 19-03-2012 Usadas para la revalidacion de materia prima
    Public Cant_Canos_Revalidados As Integer

    'dg 14-11-2011 usados para pasar los lotes a revalidar vencimiento..
    Public id_stock As Integer


    'dg 08-11-2011 para almacenar el numero de almacen al dar de alta un material...
    Public numero_almacen As Long
    '12-04-2012 sirve para indicar si el form de Impresor de Vales esta abierto
    Public ImpresorValesAbierto As Boolean
    'dg 02/09/2011
    Public CERRAR_SESIONES As Boolean 'pedido por laborde para evitar que un usuario se logue mas de una vez por sistema
    Public CERRAR_IP As String 'direccion ip del equipo
    Public CERRAR_EQUIPO As String 'nombre del equipo
    Public CERRAR_APLICACION As String 'nombre de la aplicacion

    'dg 19-11-10
    Public ensayos_Manuales_IdInstrumento As Long
    Public ensayos_Manuales_CodigoInstrumento As String
    Public ensayos_Manuales_IdRegistro As Long

    'dg 16-09-10 para kanban..
    Public contador_aux As Integer
    Public cantidad_labels As Integer = 0 'ahora debe ser igual al kanban
    Public punto_de_inicio_general As Integer = 180 'distancia desde el borde superior la primera vez
    Public punto_de_inicio As Integer = 0 'distancia desde el borde superior despues de la primera vez
    Public distancia_desde_borde As Integer = 10 'desde el borde izquierdo
    Public alto_de_la_barra As Integer = 30 'altura del label
    Public espacio_entre_barras As Integer = 20 'distancia entre las label de una fila y la siguiente
    Public ancho_de_la_barra As Integer = 0 'ahora debe ser proporcional al kanban
    Public bandera_fuente As Boolean = False 'indica el label donde se escribe la marca
    Public ancho_a_dibujar As Integer = 800 'es la distancia que ocupara el kanban
    Public graficar As Boolean = False 'indica si hay que hacer la rutina que grafica
    'fin kanban.
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'DEFINO UN OBJETO TipoConexion                                                                            '      
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public TipoConexionUSUARIOS As TipoConexion
    Public TipoConexionPERFO As TipoConexion
    Public TipoConexionICYS As TipoConexion
    Public TipoConexionACER As TipoConexion
    Public TipoConexionEMI As TipoConexion
    Public TipoConexionROTI As TipoConexion
    Public TipoConexionFINANCIERA As TipoConexion
    Public TipoConexionSEI As TipoConexion
    Public TipoConexionCRONOS As TipoConexion

    Public TipoConexionHOLLMAN As TipoConexion
    Public TipoConexionMOTO_HOLLMAN As TipoConexion
    Public TipoConexionFEAFIP As TipoConexion
    Public TipoConexionBIANCO As TipoConexion


    Public TipoConexionMIT As TipoConexion

    Public TipoConexionBALANZA As TipoConexion

    Public TipoConexionDONROBERTO As TipoConexion
    Public TipoConexionCLINICA As TipoConexion

    Public TipoConexionPROPIEDADES As TipoConexion

    Public TipoConexionRESTOBAR As TipoConexion



    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'VARIABLES GLOBALES                                                                                       '      
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public path_raiz As String

    Public ConnStringUSUARIOS As String
    Public ConnStringICYS As String
    Public ConnStringPERFO As String
    Public ConnStringACER As String
    Public ConnStringEMI As String
    Public ConnStringROTI As String
    Public ConnStringFINANCIERA As String
    Public ConnStringSEI As String
    Public ConnStringHOLLMAN As String
    Public ConnStringMOTO_HOLLMAN As String
    Public ConnStringCRONOS As String
    Public ConnStringFEAFIP As String
    Public ConnStringMIT As String
    Public ConnStringBALANZA As String
    Public ConnStringDONROBERTO As String
    Public ConnStringCLINICA As String
    Public ConnStringPROPIEDADES As String
    Public ConnStringRESTOBAR As String
    Public ConnStringBIANCO As String

    Public cerroparametrosconaceptar As Boolean = False             ' Variable Global utilizada para saber si el frmParametros se cerro del boton cerrar

    Public cantparametrosmasboton As Integer = 1                    'PARA CONTROLAR LA CANTIDAD DE PARAMETROS EN frmParametros

    Public nbreformreportes As String                               'VARIABLE PARA PSARLE EL NOMBRE DEL FORMULARIO AL REPROTE

    Public pathrpt As String    'Usada para almacenar la ruta de los reportes sacada del ini
    Public pathinis As String   'Usarda para almacenar la ruta de los inis
    Public pathComprobantesAFIP As String   'Usarda para almacenar la ruta de los inis

    Public Empresa As String

    ''VARIABLE PARA PASARLE AL PARAMETRO ?filtradopor DE LOS REPORTES
    ''PARA QUE EL USUARIO SEPA CON QUE CRITERIO FUE FILTRADO EL REPORTE
    Public filtradopor As String = ""
    ''FIN NUEVO

    Public MODO_NUEVO_OCS As Boolean

    Public pass_vencida As Boolean 'dg 29-07-10
    Public pass_repetida As Boolean 'cp 13-10-11
    Public ok_cambio As Boolean 'dg 29-07-10
    Public pass As String  'dg 29-07-10

    Public ImportarExcel As Boolean 'cp 19-11-2010

    Public Logueado_OK As Boolean = False
    Public UserActual As String

    Public EquipoActual As String ' guarda el nombre del equipo

    Public codigo_sector As String ' ejemplo: "FLEJADORA"
    Public descripcion_sector As String ' ejemplo: "SECTOR FLEJADORA DE TUBHIER"
    Public codigo_sector_anterior As String ' ejemplo: "ENTRYLINE"
    Public muestra_listado As Boolean
    Public id_sector As Long ' ejemplo: "1"
    Public id_sectoranterior As Long ' ejemplo: "5"

    Public UserID As Long
    Public NombreUsuario As String
    Public TipoUsuario As String
    Public EmailUsuario As String
    Public CelularUsuario As String

    'variables para la ayuda...
    ''
    Public cadena_ayuda As String
    Public COLUMNAS_AYUDA As Integer
    Public strCod, strCod2, strCod3, strCod4, strCod5, strCod6, strCod7 As String
    'fin variables ayuda.


    Public Recalcular_Seleccionadas As Boolean

    Public texto_del_combo As String

    Public ensayos_Manuales_Tipo As String
    Public ensayos_Manuales_op As String
    'Public ensayos_Manuales_paquete As String
    Public ensayos_Manuales_propio As String
    Public ensayos_Manuales_sector As String
    Public ensayos_Manuales_codigo_deltipo As String

    Public FILTRO_DE_ENSAYOSMP As String = ""

    Public ensayos_Id As String
    Public ensayos_loteqm As Long

    Public ControlEspesor As Boolean   ' Nuevo CP 22-10-2009, verificacion Espesor de la bobina

    'variables para mostrar un formulario llamado desde otro...
    Public LLAMADO_POR_FORMULARIO As Boolean
    Public ARRIBA As Integer
    Public IZQUIERDA As Decimal

    'USADAS PARA PASAR DATOS ENTRE EL FORM DE RECEPCIONES Y EL DE ORDEN DE COMPRA...
    Public ID_ORDEN_DE_COMPRA As Long
    Public ID_ORDEN_DE_COMPRA_DET As Long
    Public ID_MATERIAL As Long
    Public CODIGO_MATERIAL As String
    Public NOMBRE_MATERIAL As String
    Public CANTIDAD_MATERIAL As Decimal
    Public ID_UNIDAD As Long
    Public UNIDAD_MATERIAL As String
    Public MANEJALOTE As Boolean
    Public STATUS_ORDEN_DE_COMPRA_DET As String


    Public Id_Diseno As Long
    Public Texto_Diseno As String



    'variable usada para pasar el numero de ingreso de materia prima desde el maestro al detalle
    Public Codigo_Ingreso_Maestro As Long

    'variable usada para pasar el numero de id de la tabla ocs a la ocsitmes 
    Public ID_OCs_OCsItems As Long

    'variable usada para pasar el numero de id de la tabla presupuesto
    Public ID_Presupuesto_PresupuestoItems As Long

    'variable usada para pasar el numero de id(tabla ingresomateriaprimadetalle) entre el ingresomateriaprimadetalle y bobina
    Public Id_Detalle As Long = 0

    'variable que indica si se debe borrar el ingresomaestro (cuando vale 1)
    Public Borrar_Maestro As Byte = 0

    'variable que indica si se debe borrar la oc (cuando vale 1)
    Public Borrar_OC As Byte = 0

    'variable que indica si se debe borrar el presupuesto (cuando vale 1)
    Public Borrar_Presupuesto As Byte = 0

    ' Variables y funciones para el login integrado con windows
    Dim LOGON32_LOGON_INTERACTIVE As Integer = 2
    Dim LOGON32_PROVIDER_DEFAULT As Integer = 0

    Dim impersonationContext As WindowsImpersonationContext

    Declare Function LogonUserA Lib "advapi32.dll" (ByVal lpszUsername As String, _
                            ByVal lpszDomain As String, _
                            ByVal lpszPassword As String, _
                            ByVal dwLogonType As Integer, _
                            ByVal dwLogonProvider As Integer, _
                            ByRef phToken As IntPtr) As Integer

    Declare Auto Function DuplicateToken Lib "advapi32.dll" ( _
                            ByVal ExistingTokenHandle As IntPtr, _
                            ByVal ImpersonationLevel As Integer, _
                            ByRef DuplicateTokenHandle As IntPtr) As Integer

    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
    Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'MANEJO DE LOS INIS                                                                                          '   
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Const INVALID_HANDLE_VALUE = -1
    Private Const MAX_PATH = 260

    ' UDT para FindFirstFile   
    Structure FILETIME
        Public dwLowDateTime As Long
        Public dwHighDateTime As Long
    End Structure

    Structure WIN32_FIND_DATA
        Public dwFileAttributes As Long
        Public ftCreationTime As FILETIME
        Public ftLastAccessTime As FILETIME
        Public ftLastWriteTime As FILETIME
        Public nFileSizeHigh As Long
        Public nFileSizeLow As Long
        Public dwReserved0 As Long
        Public dwReserved1 As Long
        Public cFileName() As String
        Public cAlternate() As String
    End Structure


    ' Apis para buscar ficheros   
    Private Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Long) As Long
    Private Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFilename As String, ByVal lpFindFileData As WIN32_FIND_DATA) As Long


    Private m_Ini As String
    Private sBuffer As String   ' Para usarla en las funciones GetSection(s)
    Public path As String
    Public sComputerName As String     'Devuelve el nombre del equipo actual

    ' Leer una sección completa
    Private Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" _
        (ByVal lpAppName As String, ByVal lpReturnedString As String, _
        ByVal nSize As Long, ByVal lpFileName As String) As Long
    '
    Private Declare Function GetPrivateProfileStringKey Lib "kernel32" Alias _
    "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal _
    lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString _
    As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    '--- Declaraciones para leer ficheros INI ---
    ' Leer todas las secciones de un fichero INI, esto seguramente no funciona en Win95
    ' Esta función no estaba en las declaraciones del API que se incluye con el VB
    Private Declare Function GetPrivateProfileSectionNames Lib "kernel32" Alias "GetPrivateProfileSectionNamesA" (ByVal lpszReturnBuffer As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    ' Leer una sección completa
    Private Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    ' Leer una clave de un fichero INI
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Integer, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    ' Escribir una clave de un fichero INI (también para borrar claves y secciones)
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Integer, ByVal lpString As Integer, ByVal lpFileName As String) As Integer

#Region "Formulario BASE"

    Property Archivo() As String
        Get
            Archivo = m_Ini
        End Get
        Set(ByVal value As String)
            m_Ini = value
        End Set
    End Property


    Public Sub AsignarPermisos(ByVal user As Long, ByVal form As String, ByRef a As Boolean, ByRef m As Boolean, ByRef b As Boolean, ByRef b_f As Boolean, ByRef d As Boolean, Optional ByVal pConn As String = "")
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                If pConn.Trim <> "" Then
                    connection = SqlHelper.GetConnection(pConn)
                Else
                    connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                End If

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try


                Dim param_iduser As New SqlClient.SqlParameter
                param_iduser.ParameterName = "@iduser"
                param_iduser.SqlDbType = SqlDbType.BigInt
                param_iduser.Value = user
                param_iduser.Direction = ParameterDirection.Input

                Dim param_form As New SqlClient.SqlParameter
                param_form.ParameterName = "@form"
                param_form.SqlDbType = SqlDbType.VarChar
                param_form.Size = 50
                param_form.Value = form
                param_form.Direction = ParameterDirection.Input

                Dim param_alta As New SqlClient.SqlParameter
                param_alta.ParameterName = "@alta"
                param_alta.SqlDbType = SqlDbType.Bit
                param_alta.Value = DBNull.Value
                param_alta.Direction = ParameterDirection.Output

                Dim param_modifica As New SqlClient.SqlParameter
                param_modifica.ParameterName = "@modifica"
                param_modifica.SqlDbType = SqlDbType.Bit
                param_modifica.Value = DBNull.Value
                param_modifica.Direction = ParameterDirection.Output

                Dim param_baja As New SqlClient.SqlParameter
                param_baja.ParameterName = "@baja"
                param_baja.SqlDbType = SqlDbType.Bit
                param_baja.Value = DBNull.Value
                param_baja.Direction = ParameterDirection.Output

                Dim param_bajafisica As New SqlClient.SqlParameter
                param_bajafisica.ParameterName = "@bajafisica"
                param_bajafisica.SqlDbType = SqlDbType.Bit
                param_bajafisica.Value = DBNull.Value
                param_bajafisica.Direction = ParameterDirection.Output

                Dim param_deshacer As New SqlClient.SqlParameter
                param_deshacer.ParameterName = "@deshacer"
                param_deshacer.SqlDbType = SqlDbType.Bit
                param_deshacer.Value = DBNull.Value
                param_deshacer.Direction = ParameterDirection.Output

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPermisos_Select_by_Form", param_iduser, param_form, param_alta, param_modifica, param_baja, param_bajafisica, param_deshacer)
                    a = IIf(param_alta.Value Is DBNull.Value, False, param_alta.Value)
                    m = IIf(param_modifica.Value Is DBNull.Value, False, param_modifica.Value)
                    b = IIf(param_baja.Value Is DBNull.Value, False, param_baja.Value)
                    b_f = IIf(param_bajafisica.Value Is DBNull.Value, False, param_bajafisica.Value)
                    d = IIf(param_deshacer.Value Is DBNull.Value, False, param_deshacer.Value)

                Catch ex As Exception
                    a = False
                    b = False
                    b_f = False
                    m = False
                    d = False
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "AsignarPermisos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Function impersonateValidUser(ByVal userName As String, _
            ByVal domain As String, ByVal password As String) As Boolean

        Dim tempWindowsIdentity As WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero
        impersonateValidUser = False

        If RevertToSelf() Then
            If LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                    tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                    impersonationContext = tempWindowsIdentity.Impersonate()
                    If Not impersonationContext Is Nothing Then
                        impersonateValidUser = True
                    End If
                End If
            End If
        End If
        If Not tokenDuplicate.Equals(IntPtr.Zero) Then
            CloseHandle(tokenDuplicate)
        End If
        If Not token.Equals(IntPtr.Zero) Then
            CloseHandle(token)
        End If
    End Function

    Private Sub undoImpersonation()
        impersonationContext.Undo()
    End Sub

    Public Function Logear(ByVal userName As String, ByVal domain As String, ByVal password As String) As Boolean
        If impersonateValidUser(userName, domain, password) Then
            'Insert your code that runs under the security context of a specific user here.
            undoImpersonation()
            Logear = True
        Else
            Logear = False
        End If

        Return Logear
    End Function

    Public Function GetUserID() As String
        If System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated Then
            GetUserID = System.Threading.Thread.CurrentPrincipal.Identity.Name.Trim
        Else
            GetUserID = ""
        End If
        Return GetUserID
    End Function

    ' Carga datos de los textbox a la grilla
    Public Sub TextBoxAGrilla(ByRef oVControls As Object, ByRef g As DataGridView)
        For Each oControl As Object In oVControls
            Try
                If TypeOf (oControl) Is TextBox Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.Text
                    End If
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.TextBoxX Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.Text
                    End If
                End If
                If TypeOf (oControl) Is CheckBox Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.checked
                    End If
                End If
                If TypeOf (oControl) Is DateTimePicker Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.Text
                    End If
                End If
                If TypeOf (oControl) Is ComboBox Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.Text
                    End If
                End If
                If TypeOf (oControl) Is Label Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.Text
                    End If
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.LabelX Then
                    If oControl.Tag <> "" Then
                        g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value = oControl.Text
                    End If
                End If



                '
                ' llamadas recursivas
                '
                If TypeOf (oControl) Is TabControl Then
                    TextBoxAGrilla(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is TabPage Then
                    TextBoxAGrilla(oControl.Controls, g)
                End If
                If TypeOf oControl Is GroupBox Then
                    TextBoxAGrilla(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabControl Then
                    TextBoxAGrilla(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabControlPanel Then
                    TextBoxAGrilla(oControl.Controls, g)
                End If

            Catch ex As Exception
                MessageBox.Show("Error en TextBoxAGrilla " & ex.Message)
            End Try
        Next
    End Sub

    Function SearchInComboBox(ByVal cmb As ComboBox, _
                              ByVal txtToFind As String, _
                              Optional ByVal colPos As Integer = 0) _
                              As Integer
        Dim I As Integer
        For I = 0 To cmb.Items.Count - 1
            If cmb.Items(I)(colPos).ToString() = txtToFind.ToString Then
                Return I
            End If
        Next I
        Return -1
    End Function

    ' Carga datos de la grilla a los textbox
    Public Sub GrillaATextBox(ByRef oVControls As Object, ByRef g As DataGridView)
        For Each oControl As Object In oVControls
            Try
                If TypeOf (oControl) Is TextBox Then
                    ' MsgBox(oControl.Tag)
                    If oControl.Tag <> "" Then
                        oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Try
                            'SI PUEDO (PORQUE ES DEL TIPO TEXTBOXCONFORMATO) GUARDO EL VALOR ANTERIOR
                            oControl.Text = oControl.Text
                        Catch ex As Exception
                            'MsgBox(ex.Message)
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DateTimePicker Or TypeOf (oControl) Is Label Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                            'MsgBox("2")
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is CheckBox Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, False, g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                            'MsgBox("3")
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is System.Windows.Forms.RadioButton Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, False, g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                            'MsgBox("4")
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DataGridViewComboBoxColumn Then
                    oControl.selectedindex = -1
                End If

                If TypeOf (oControl) Is System.Windows.Forms.ComboBox Then
                    If oControl.Tag <> "" Then
                        If g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value <> "" Then
                            Try
                                oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                            Catch e As Exception
                                ' MsgBox("5")
                            End Try

                        Else
                            oControl.Text = ""
                            Try
                                oControl.selectedindex = -1
                            Catch e As Exception
                                'MsgBox("6")
                            End Try
                        End If
                    End If
                End If

                If TypeOf (oControl) Is DevComponents.Editors.DateTimeAdv.DateTimeInput Then
                    If oControl.Tag <> "" Then
                        oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                    End If
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.TextBoxX Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                            'MsgBox("7")
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.CheckBoxX Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                            'MsgBox("8")
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                            'MsgBox("9")
                        End Try
                    End If
                End If

                '
                ' Llamadas recursivas
                '
                If TypeOf (oControl) Is TabControl Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is TabPage Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf oControl Is GroupBox Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf oControl Is TableLayoutPanel Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabControl Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabControlPanel Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabStrip Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabItem Then
                    GrillaATextBox(oControl.Controls, g)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.GroupPanel Then
                    GrillaATextBox(oControl.Controls, g)
                End If


            Catch ex As Exception
                MessageBox.Show("Error anidado: " & ex.Message, "Util.GrillaATextBox")
            End Try
        Next
    End Sub

    Public Function MyCType(ByRef oValor As TextBox, ByVal t As String) As TextBox
        Select Case t.ToLower
            Case "integer"
                oValor.Text = IIf(oValor.Text.Trim = "", "0", oValor.Text.Trim)
            Case "decimal"
                oValor.Text = IIf(oValor.Text.Trim = "", "0", oValor.Text.Trim)
            Case "long"
                oValor.Text = IIf(oValor.Text.Trim = "", "0", oValor.Text.Trim)
        End Select
        Return oValor
    End Function

    Public Function generarClaveSHA1(ByVal nombre As String) As String
        ' Crear una clave SHA1 como la generada por 
        ' FormsAuthentication.HashPasswordForStoringInConfigFile
        ' Adaptada del ejemplo de la ayuda en la descripción de SHA1 (Clase)
        Dim enc As New UTF8Encoding
        Dim data() As Byte = enc.GetBytes(nombre)
        Dim result() As Byte

        Dim sha As New SHA1CryptoServiceProvider
        ' This is one implementation of the abstract class SHA1.
        result = sha.ComputeHash(data)
        '
        ' Convertir los valores en hexadecimal
        ' cuando tiene una cifra hay que rellenarlo con cero
        ' para que siempre ocupen dos dígitos.
        Dim sb As New StringBuilder
        For i As Integer = 0 To result.Length - 1
            If result(i) < 16 Then
                sb.Append("0")
            End If
            sb.Append(result(i).ToString("x"))
        Next
        '
        Return sb.ToString.ToUpper
    End Function

    ' Función para comprobar si el acceso es correcto
    Public Function comprobarUsuario( _
                ByVal nombre As String, _
                ByVal clave As String) As Boolean

        ' Conectar a la base de datos
        Dim cnn As Data.SqlClient.SqlConnection = Nothing
        '
        Try
            ' Conectar a la base de datos de SQL Server
            ' (la cadena debe estar inicializada previamente)
            ''COMENTADO MS 30-06-2010
            ''cnn = New Data.SqlClient.SqlConnection(GlobalComun.ConnStringRRHH)    
            ''COMENTADO
            ''NUEVO
            cnn = New Data.SqlClient.SqlConnection(Util.ConnStringUSUARIOS)
            'cnn = New Data.SqlClient.SqlConnection(Util.ConnStringPanol)
            ''FIN NUEVO
            cnn.Open()

            ' Definir la cadena que vamos a usar para comprobar
            ' si el usuario y el password son correctos.
            ' Utilizo parámetros para evitar inyección de código.
            Dim sel As New System.Text.StringBuilder

            ' Usando COUNT(*) nos devuelve el total que coincide
            ' con lo indicado en el WHERE,
            ' por tanto, si la clave y el usuario son correctos,
            ' devolverá 1, sino, devolverá 0
            sel.Append("SELECT Count(*) FROM Usuarios ")
            sel.Append("WHERE Codigo = @Nombre AND Pass = @Clave")
            'sel.Append("WHERE alias = @Nombre AND Pass = @Clave")
            ' Definir el comando que vamos a ejecutar
            Dim cmd As New Data.SqlClient.SqlCommand(sel.ToString, cnn)
            ' Creamos los parámetros
            cmd.Parameters.Add("@Nombre", Data.SqlDbType.NVarChar, 25)
            cmd.Parameters.Add("@Clave", Data.SqlDbType.NVarChar, 50)
            '
            ' Asignamos los valores recibidos como parámetro
            cmd.Parameters("@Nombre").Value = nombre
            cmd.Parameters("@Clave").Value = clave
            '
            ' Ejecutamos la consulta
            ' ExecuteScalar devuelve la primera columna de la primera fila
            ' por tanto, devolverá el número de coincidencias halladas,
            ' que si es 1, quiere decir que el usuario y el password son correctos.
            Dim t As Integer = CInt(cmd.ExecuteScalar())
            ' Cerramos la conexión
            cnn.Close()
            '
            ' Si el valor devuelto es cero
            ' es que no es correcto.
            If t = 0 Then
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            If Not cnn Is Nothing Then
                cnn.Dispose()
            End If
        End Try
        '
        ' Si llega aquí es que todo ha ido bien
        Return True
    End Function

    ' Función para comprobar si el acceso es correcto
    Public Function ObtenerUsuario( _
                ByVal nombre As String) As Long

        ' Conectar a la base de datos
        Dim cnn As Data.SqlClient.SqlConnection = Nothing
        Dim t As Long
        '
        Try
            ' Conectar a la base de datos de SQL Server
            ' (la cadena debe estar inicializada previamente)
            ''COMENTADO MS 30-06-2010
            ''cnn = New Data.SqlClient.SqlConnection(ConnStringRRHH)
            ''FIN COMENTADO
            ''NUEVO MS 30-06-2010
            'cnn = New Data.SqlClient.SqlConnection(ConnStringPanol)
            cnn = New Data.SqlClient.SqlConnection(ConnStringUSUARIOS)
            ''FIN NUEVO
            cnn.Open()

            ' Definir la cadena que vamos a usar para comprobar
            ' si el usuario y el password son correctos.
            ' Utilizo parámetros para evitar inyección de código.
            Dim sel As New System.Text.StringBuilder

            ' Usando COUNT(*) nos devuelve el total que coincide
            ' con lo indicado en el WHERE,
            ' por tanto, si la clave y el usuario son correctos,
            ' devolverá 1, sino, devolverá 0
            sel.Append("SELECT TOP 1 ID FROM Usuarios ")
            sel.Append("WHERE codigo = @Nombre")
            'sel.Append("WHERE alias = @Nombre")
            ' Definir el comando que vamos a ejecutar
            Dim cmd As New Data.SqlClient.SqlCommand(sel.ToString, cnn)
            ' Creamos los parámetros
            cmd.Parameters.Add("@Nombre", Data.SqlDbType.NVarChar, 20)
            '
            ' Asignamos los valores recibidos como parámetro
            cmd.Parameters("@Nombre").Value = nombre
            '
            ' Ejecutamos la consulta
            ' ExecuteScalar devuelve la primera columna de la primera fila
            ' por tanto, devolverá el número de coincidencias halladas,
            ' que si es 1, quiere decir que el usuario y el password son correctos.
            t = CLng(cmd.ExecuteScalar())
            ' Cerramos la conexión
            cnn.Close()
            '
            ' Si el valor devuelto es cero
            ' es que no es correcto.
            If t = 0 Then
                Return -1
            End If

        Catch ex As Exception
            Return False
        Finally
            If Not cnn Is Nothing Then
                cnn.Dispose()
            End If
        End Try
        '
        ' Si llega aquí es que todo ha ido bien
        Return t
    End Function

    ' borra un item de la grilla
    Public Sub BorrarGrilla(ByRef g As DataGridView)
        g.Rows.RemoveAt(g.CurrentRow.Index)
    End Sub

    ' Modifica un item de la grilla
    Public Sub ActualizarGrilla(ByRef g As DataGridView, ByRef c As Control)
        TextBoxAGrilla(c.Controls, g)
    End Sub

    ' Agrega un item a la grilla
    Public Sub AgregarGrilla(ByRef g As DataGridView, ByRef c As Control, ByRef per As Boolean)
        Dim j As Integer
        per = False
        g.Rows.Add()

        If Not g.Rows.Count = 1 Then
            For j = 0 To 6
                Try
                    g.CurrentCell = g.Rows(g.Rows.Count - 1).Cells(j)
                    Exit For
                Catch ex As Exception
                End Try
            Next j
        End If

        TextBoxAGrilla(c.Controls, g)
        per = True
    End Sub

    ' Mensajes del status bar
    Public Sub MsgStatus(ByRef s As ToolStripStatusLabel, ByVal texto As String, Optional ByVal imagen As System.Drawing.Image = Nothing, Optional ByVal ES_MSGBOX As Boolean = Nothing, Optional ByVal es_ok As Boolean = Nothing)

        Try
            If ES_MSGBOX = True Then
                If es_ok = True Then
                    MsgBox(texto, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Atención - Proceso OK")
                Else
                    MsgBox(texto, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Atención - Error")
                End If
            Else
                s.Text = " " & texto
                s.Image = imagen
            End If
            ES_MSGBOX = False
            es_ok = False

        Catch ex As Exception
            s.Text = " " & texto
            s.Image = imagen
            ES_MSGBOX = False
            es_ok = False
        End Try

        'CType(Me.MdiParent, MDIParent1).ToolStripStatusLabel.Text = "Mensaje" 
    End Sub

    ' Mensajes del status bar
    Public Sub MsgStatus_Perfo(ByRef s As ToolStripStatusLabel, ByVal texto As String, Optional ByVal imagen As System.Drawing.Image = Nothing)

        s.Text = " " & texto
        s.Image = imagen

        'CType(Me.MdiParent, MDIParent1).ToolStripStatusLabel.Text = "Mensaje" 
    End Sub

    ' Limpiar las cajas de texto de un form
    Public Sub LimpiarTextBox(ByRef oVControls As Object)
        'Dim i As Integer
        For Each oControl As Object In oVControls
            Try
                'i = 1
                If TypeOf (oControl) Is TextBox Then
                    oControl.Text = ""
                End If

                'i = 2
                If TypeOf (oControl) Is ComboBox Then
                    oControl.Text = ""
                    Try
                        oControl.selectedindex = -1
                    Catch ex2 As Exception
                    End Try
                End If

                'i = 3
                If TypeOf (oControl) Is DateTimePicker Then
                    oControl.text = ""
                End If

                'i = 4
                If TypeOf (oControl) Is System.Windows.Forms.RadioButton Then
                    oControl.checked = False
                End If

                'i = 5
                If TypeOf (oControl) Is DevComponents.Editors.DateTimeAdv.DateTimeInput Then
                    oControl.text = ""
                End If

                'i = 6
                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.TextBoxX Then
                    oControl.text = ""
                End If

                'i = 7
                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.CheckBoxX Then
                    oControl.CHECKED = False
                End If

                'i = 4
                If TypeOf (oControl) Is System.Windows.Forms.CheckBox Then
                    oControl.checked = False
                End If


                '
                'llamadas recursivas
                '
                If TypeOf (oControl) Is TabControl Then
                    LimpiarTextBox(oControl.Controls)
                End If
                If TypeOf (oControl) Is TabPage Then
                    LimpiarTextBox(oControl.Controls)
                End If
                If TypeOf oControl Is GroupBox Then
                    LimpiarTextBox(oControl.Controls)
                End If
                If TypeOf oControl Is TableLayoutPanel Then
                    LimpiarTextBox(oControl.Controls)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.GroupPanel Then
                    LimpiarTextBox(oControl.Controls)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabControl Then
                    LimpiarTextBox(oControl.Controls)
                End If
                If TypeOf (oControl) Is DevComponents.DotNetBar.TabControlPanel Then
                    LimpiarTextBox(oControl.Controls)
                End If

            Catch ex As Exception
                'MsgBox(i)
                MessageBox.Show("error anidado" & ex.Message)
            End Try
        Next
    End Sub

    Public Sub LimpiarGridItems(ByVal g As DataGridView)
        Dim i As Integer, n As Integer
        n = g.Rows.Count - 1
        For i = 0 To n
            Try
                g.Rows.RemoveAt(n - i)
            Catch ex As Exception

            End Try
        Next
    End Sub

    ' Limpiar las cajas de texto de un form
    Public Function CamposObligatorios(ByRef oVControls As Object, Optional ByRef msg As String = "") As String
        CamposObligatorios = msg
        For Each oControl As Object In oVControls
            Try
                If TypeOf (oControl) Is TextBox Or TypeOf (oControl) Is ComboBox Then
                    If InStr(oControl.AccessibleName, "*") <> 0 Then
                        If Trim(oControl.Text) = "" Then
                            CamposObligatorios = oControl.AccessibleName
                            msg = oControl.AccessibleName
                            Exit Function
                        End If
                    End If
                End If

                If TypeOf (oControl) Is Label Then
                    If InStr(oControl.AccessibleName, "*") <> 0 Then
                        If Trim(oControl.Text) = "" Then
                            CamposObligatorios = oControl.AccessibleName
                            msg = oControl.AccessibleName
                            Exit Function
                        End If
                    End If
                End If

                '
                'Llamada recursiva
                '
                If TypeOf oControl Is GroupBox Then
                    CamposObligatorios(oControl.Controls, msg)
                    If msg <> "" Then
                        CamposObligatorios = msg
                        Exit Function
                    End If
                End If

                If TypeOf oControl Is DevComponents.DotNetBar.Controls.GroupPanel Then
                    CamposObligatorios(oControl.Controls, msg)
                    If msg <> "" Then
                        CamposObligatorios = msg
                        Exit Function
                    End If
                End If

                If TypeOf oControl Is TabControl Then
                    CamposObligatorios(oControl.Controls, msg)
                    If msg <> "" Then
                        CamposObligatorios = msg
                        Exit Function
                    End If
                End If
                If TypeOf oControl Is TabPage Then
                    CamposObligatorios(oControl.Controls, msg)
                    If msg <> "" Then
                        CamposObligatorios = msg
                        Exit Function
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("error anidado" & ex.Message)
            End Try
        Next
    End Function

    ' Permite obtener datos generales de la tabla MateriaPrima
    Public Function comprobarUsuario2(ByRef ID As Long, ByVal codigo As String, _
                                            ByVal pass As String, _
                                            ByVal SHA1 As String, _
                                            ByRef OK As Integer, _
                                            ByRef nombre As String, _
                                            ByRef vencida As Boolean, _
                                            ByRef repetida As Boolean, _
                                            Optional ByVal pConn As String = "") As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(pConn)
                pass = SHA1 'Encriptada

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@iduser"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@usuario"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_mail As New SqlClient.SqlParameter
                param_mail.ParameterName = "@Email"
                param_mail.SqlDbType = SqlDbType.VarChar
                param_mail.Size = 150
                param_mail.Value = DBNull.Value
                param_mail.Direction = ParameterDirection.InputOutput

                Dim param_celular As New SqlClient.SqlParameter
                param_celular.ParameterName = "@celular"
                param_celular.SqlDbType = SqlDbType.VarChar
                param_celular.Size = 50
                param_celular.Value = DBNull.Value
                param_celular.Direction = ParameterDirection.InputOutput

                Dim param_pass As New SqlClient.SqlParameter
                param_pass.ParameterName = "@pass"
                param_pass.SqlDbType = SqlDbType.VarChar
                param_pass.Size = 50
                param_pass.Value = pass
                param_pass.Direction = ParameterDirection.Input

                Dim param_ok As New SqlClient.SqlParameter
                param_ok.ParameterName = "@ok"
                param_ok.SqlDbType = SqlDbType.Int
                param_ok.Value = DBNull.Value
                param_ok.Direction = ParameterDirection.InputOutput

                Dim param_vencida As New SqlClient.SqlParameter
                param_vencida.ParameterName = "@vencida"
                param_vencida.SqlDbType = SqlDbType.Bit
                param_vencida.Value = DBNull.Value
                param_vencida.Direction = ParameterDirection.InputOutput

                Dim param_repetida As New SqlClient.SqlParameter
                param_repetida.ParameterName = "@repetida"
                param_repetida.SqlDbType = SqlDbType.Bit
                param_repetida.Value = DBNull.Value
                param_repetida.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spUsuariosComprobar2", param_id, _
                                              param_codigo, param_nombre, param_celular, param_mail, _
                                              param_pass, param_ok, param_vencida, param_repetida)

                    If Not param_id.Value Is DBNull.Value Then
                        ID = param_id.Value
                    End If
                    If Not param_nombre.Value Is DBNull.Value Then
                        nombre = param_nombre.Value
                    End If
                    If Not param_ok.Value Is DBNull.Value Then
                        OK = param_ok.Value
                    End If
                    If Not param_vencida.Value Is DBNull.Value Then
                        vencida = param_vencida.Value
                    End If
                    If Not param_repetida.Value Is DBNull.Value Then
                        repetida = param_repetida.Value
                    End If
                    If Not param_nombre.Value Is DBNull.Value Then
                        nombre = param_nombre.Value
                        NombreUsuario = nombre
                    End If
                    If Not param_mail.Value Is DBNull.Value Then
                        EmailUsuario = param_mail.Value
                    End If
                    If Not param_celular.Value Is DBNull.Value Then
                        CelularUsuario = param_celular.Value
                    End If

                    comprobarUsuario2 = OK


                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    ' Permite obtener datos generales de la tabla MateriaPrima
    Public Function cambiarContrasena(ByRef ID As Long, _
                                            ByVal pass As String, Conexion As String) As Boolean

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try


                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@iduser"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = ID
                param_id.Direction = ParameterDirection.Input

                Dim param_pass As New SqlClient.SqlParameter
                param_pass.ParameterName = "@pass"
                param_pass.SqlDbType = SqlDbType.VarChar
                param_pass.Size = 50
                param_pass.Value = pass
                param_pass.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spUsuariosCambiarContrasena", param_id, param_pass, param_res)


                    cambiarContrasena = param_res.Value > 0


                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    'Leer una llave de un archivo .ini
    Public Function LeeIni(ByVal Seccion As String, ByVal Llave As String) As String
        Dim lret As Long
        Dim ret As String
        ret = New String(CChar(" "), 255)
        lret = GetPrivateProfileStringKey(Seccion, Llave, "", ret, Len(ret), m_Ini)
        If InStr(ret, Chr(0)) Then
            ret = Left$(ret, lret)
            ''ret = Left$(ret, Len(ret)-1)
        End If
        LeeIni = ret
    End Function

    Public Function IniGetSection(ByVal sFileName As String, ByVal sSection As String) As String()

        Dim aSeccion() As String
        Dim n As Integer
        '
        ReDim aSeccion(0)
        '
        ' El tamaño máximo para Windows 95
        sBuffer = New String(ChrW(0), 32767)
        '
        n = GetPrivateProfileSection(sSection, sBuffer, sBuffer.Length, sFileName)
        '
        If n > 0 Then
            '
            ' Cortar la cadena al número de caracteres devueltos
            ' menos los dos últimos que indican el final de la cadena
            sBuffer = sBuffer.Substring(0, n - 2).TrimEnd()
            ' Cada elemento estará separado por un Chr(0)
            ' y cada valor estará en la forma: clave = valor
            aSeccion = sBuffer.Split(New Char() {ChrW(0), "="c})
        End If
        ' Devolver el array
        Return aSeccion
    End Function

    Private Function Clave_Segun_Server(Optional ByVal server As String = "") As String
        'Clave_Segun_Server = ""
        'Select Case UCase(Trim$(server.ToString))
        '    Case "NBTAAL\SQL2014"
        '        Clave_Segun_Server = "industrial"
        '    Case "NBTAAL\MSSQLSERVER12"
        '        Clave_Segun_Server = "industrial"
        '    Case "SERVER\SERVER"
        '            Clave_Segun_Server = "industrial"
        '    Case "SERVERMIT\SERVERMIT"
        '        Clave_Segun_Server = "industrial"
        '    Case "NBK-CAMPANA\NBK_CAMPANA"
        '           Clave_Segun_Server = "campana."
        '    Case "OFICINA"
        '            Clave_Segun_Server = "industrial"
        '    Case "MAGENTA"
        '            Clave_Segun_Server = "industrial"
        'End Select

        Clave_Segun_Server = "industrial"

    End Function

    Public Sub ConfigurarCadenaConexion()

        Dim aux() As String
        Dim temp As String

        'PATH DE LOS REPORTES QUE SE ENCUENTRA EN EL INI DEL USUARIO

        '' CADENA DE CONEXION A TM3
        'aux = IniGetSection(Archivo, "Conexion_Usuarios")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_Usuarios", "Server")
        '    ConnStringUSUARIOS = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
        '    ConnStringUSUARIOS = ConnStringUSUARIOS & LeeIni("Conexion_Usuarios", "Origen")
        '    ConnStringUSUARIOS = ConnStringUSUARIOS & ";Data Source="
        '    ConnStringUSUARIOS = ConnStringUSUARIOS & temp
        '    TipoConexionUSUARIOS.server = temp
        '    TipoConexionUSUARIOS.base = LeeIni("Conexion_Usuarios", "Origen")
        '    TipoConexionUSUARIOS.usuario = "sa"
        '    TipoConexionUSUARIOS.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringUSUARIOS = ""
        'End If

        ' '' CADENA DE CONEXION A RN
        'aux = IniGetSection(Archivo, "Conexion_PERFO")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_PERFO", "Server")
        '    ConnStringPERFO = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
        '    ConnStringPERFO = ConnStringPERFO & LeeIni("Conexion_PERFO", "Origen")
        '    ConnStringPERFO = ConnStringPERFO & ";Data Source="
        '    ConnStringPERFO = ConnStringPERFO & temp

        '    TipoConexionPERFO.server = temp
        '    TipoConexionPERFO.base = LeeIni("Conexion_PERFO", "Origen")
        '    TipoConexionPERFO.usuario = "sa"
        '    TipoConexionPERFO.contrasena = Clave_Segun_Server(temp)
        'Else
        '    ConnStringPERFO = ""
        'End If

        ' '' CADENA DE CONEXION A PARADAS
        'aux = IniGetSection(Archivo, "Conexion_ICYS")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_ICYS", "Server")
        '    ConnStringICYS = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
        '    ConnStringICYS = ConnStringICYS & LeeIni("Conexion_ICYS", "Origen")
        '    ConnStringICYS = ConnStringICYS & ";Data Source="
        '    ConnStringICYS = ConnStringICYS & temp

        '    TipoConexionICYS.server = temp
        '    TipoConexionICYS.base = LeeIni("Conexion_ICYS", "Origen")
        '    TipoConexionICYS.usuario = "sa"
        '    TipoConexionICYS.contrasena = Clave_Segun_Server(temp)
        'Else
        '    ConnStringICYS = ""
        'End If

        '' CADENA DE CONEXION A TM3
        'aux = IniGetSection(Archivo, "Conexion_EMI")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_Usuarios", "Server")
        '    ConnStringEMI = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
        '    ConnStringEMI = ConnStringEMI & LeeIni("Conexion_EMI", "Origen")
        '    ConnStringEMI = ConnStringEMI & ";Data Source="
        '    ConnStringEMI = ConnStringEMI & temp
        '    TipoConexionEMI.server = temp
        '    TipoConexionEMI.base = LeeIni("Conexion_EMI", "Origen")
        '    TipoConexionEMI.usuario = "sa"
        '    TipoConexionEMI.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringUSUARIOS = ""
        'End If

        '' CADENA DE CONEXION A PARADAS
        aux = IniGetSection(Archivo, "Conexion_Acer")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Acer", "Server")
            ConnStringAcer = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringACER = ConnStringACER & LeeIni("Conexion_Acer", "Origen")
            ConnStringACER = ConnStringACER & ";Data Source="
            ConnStringACER = ConnStringACER & temp

            TipoConexionACER.server = temp
            TipoConexionACER.base = LeeIni("Conexion_Acer", "Origen")
            TipoConexionACER.usuario = "sa"
            TipoConexionACER.contrasena = Clave_Segun_Server(temp)
        Else
            ConnStringACER = ""
        End If

        '' CADENA DE CONEXION A TM3
        'aux = IniGetSection(Archivo, "Conexion_Rotiseria")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_Rotiseria", "Server")
        '    ConnStringROTI = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
        '    ConnStringROTI = ConnStringROTI & LeeIni("Conexion_Rotiseria", "Origen")
        '    ConnStringROTI = ConnStringROTI & ";Data Source="
        '    ConnStringROTI = ConnStringROTI & temp
        '    TipoConexionROTI.server = temp
        '    TipoConexionROTI.base = LeeIni("Conexion_Rotiseria", "Origen")
        '    TipoConexionROTI.usuario = "sa"
        '    TipoConexionROTI.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringROTI = ""
        'End If

        ' '' CADENA DE CONEXION A TM3
        'aux = IniGetSection(Archivo, "Conexion_Financiera")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_Financiera", "Server")
        '    ConnStringFINANCIERA = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
        '    ConnStringFINANCIERA = ConnStringFINANCIERA & LeeIni("Conexion_Financiera", "Origen")
        '    ConnStringFINANCIERA = ConnStringFINANCIERA & ";Data Source="
        '    ConnStringFINANCIERA = ConnStringFINANCIERA & temp
        '    TipoConexionFinanciera.server = temp
        '    TipoConexionFinanciera.base = LeeIni("Conexion_Financiera", "Origen")
        '    TipoConexionFinanciera.usuario = "sa"
        '    TipoConexionFinanciera.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringFINANCIERA = ""
        'End If

        '' CADENA DE CONEXION A TM3
        aux = IniGetSection(Archivo, "Conexion_SEI")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_SEI", "Server")
            ConnStringSEI = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0;Initial Catalog="
            ConnStringSEI = ConnStringSEI & LeeIni("Conexion_SEI", "Origen")
            ConnStringSEI = ConnStringSEI & ";Data Source="
            ConnStringSEI = ConnStringSEI & temp
            TipoConexionSEI.server = temp
            TipoConexionSEI.base = LeeIni("Conexion_SEI", "Origen")
            TipoConexionSEI.usuario = "sa"
            TipoConexionSEI.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringSEI = ""
        End If

        aux = IniGetSection(Archivo, "Conexion_HOLLMAN")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_HOLLMAN", "Server")
            ConnStringHOLLMAN = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringHOLLMAN = ConnStringHOLLMAN & LeeIni("Conexion_HOLLMAN", "Origen")
            ConnStringHOLLMAN = ConnStringHOLLMAN & ";Data Source="
            ConnStringHOLLMAN = ConnStringHOLLMAN & temp
            TipoConexionHOLLMAN.server = temp
            TipoConexionHOLLMAN.base = LeeIni("Conexion_HOLLMAN", "Origen")
            TipoConexionHOLLMAN.usuario = "sa"
            TipoConexionHOLLMAN.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringHOLLMAN = ""
        End If

        'aux = IniGetSection(Archivo, "Conexion_MOTO_HOLLMAN")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_MOTO_HOLLMAN", "Server")
        '    ConnStringMOTO_HOLLMAN = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
        '    ConnStringMOTO_HOLLMAN = ConnStringMOTO_HOLLMAN & LeeIni("Conexion_MOTO_HOLLMAN", "Origen")
        '    ConnStringMOTO_HOLLMAN = ConnStringMOTO_HOLLMAN & ";Data Source="
        '    ConnStringMOTO_HOLLMAN = ConnStringMOTO_HOLLMAN & temp
        '    TipoConexionMOTO_HOLLMAN.server = temp
        '    TipoConexionMOTO_HOLLMAN.base = LeeIni("Conexion_MOTO_HOLLMAN", "Origen")
        '    TipoConexionMOTO_HOLLMAN.usuario = "sa"
        '    TipoConexionMOTO_HOLLMAN.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringMOTO_HOLLMAN = ""
        'End If

        'aux = IniGetSection(Archivo, "Conexion_CRONOS")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_CRONOS", "Server")
        '    ConnStringCRONOS = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
        '    ConnStringCRONOS = ConnStringCRONOS & LeeIni("Conexion_CRONOS", "Origen")
        '    ConnStringCRONOS = ConnStringCRONOS & ";Data Source="
        '    ConnStringCRONOS = ConnStringCRONOS & temp
        '    TipoConexionCRONOS.server = temp
        '    TipoConexionCRONOS.base = LeeIni("Conexion_CRONOS", "Origen")
        '    TipoConexionCRONOS.usuario = "sa"
        '    TipoConexionCRONOS.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringCRONOS = ""
        'End If

        aux = IniGetSection(Archivo, "Conexion_FEAFIP")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_FEAFIP", "Server")
            ConnStringFEAFIP = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringFEAFIP = ConnStringFEAFIP & LeeIni("Conexion_FEAFIP", "Origen")
            ConnStringFEAFIP = ConnStringFEAFIP & ";Data Source="
            ConnStringFEAFIP = ConnStringFEAFIP & temp
            TipoConexionFEAFIP.server = temp
            TipoConexionFEAFIP.base = LeeIni("Conexion_FEAFIP", "Origen")
            TipoConexionFEAFIP.usuario = "sa"
            TipoConexionFEAFIP.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringFEAFIP = ""
        End If

        aux = IniGetSection(Archivo, "Conexion_MIT")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_MIT", "Server")
            ConnStringMIT = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringMIT = ConnStringMIT & LeeIni("Conexion_MIT", "Origen")
            ConnStringMIT = ConnStringMIT & ";Data Source="
            ConnStringMIT = ConnStringMIT & temp
            TipoConexionMIT.server = temp
            TipoConexionMIT.base = LeeIni("Conexion_MIT", "Origen")
            TipoConexionMIT.usuario = "sa"
            TipoConexionMIT.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringMIT = ""
        End If

        'aux = IniGetSection(Archivo, "Conexion_BALANZA")
        'If aux(0) <> "" Then
        '    temp = LeeIni("Conexion_BALANZA", "Server")
        '    ConnStringBALANZA = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
        '    ConnStringBALANZA = ConnStringBALANZA & LeeIni("Conexion_BALANZA", "Origen")
        '    ConnStringBALANZA = ConnStringBALANZA & ";Data Source="
        '    ConnStringBALANZA = ConnStringBALANZA & temp
        '    TipoConexionBALANZA.server = temp
        '    TipoConexionBALANZA.base = LeeIni("Conexion_BALANZA", "Origen")
        '    TipoConexionBALANZA.usuario = "sa"
        '    TipoConexionBALANZA.contrasena = Clave_Segun_Server(temp)

        'Else
        '    ConnStringBALANZA = ""
        'End If

        aux = IniGetSection(Archivo, "Conexion_DONROBERTO")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_DONROBERTO", "Server")
            ConnStringDONROBERTO = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringDONROBERTO = ConnStringDONROBERTO & LeeIni("Conexion_DONROBERTO", "Origen")
            ConnStringDONROBERTO = ConnStringDONROBERTO & ";Data Source="
            ConnStringDONROBERTO = ConnStringDONROBERTO & temp
            TipoConexionDONROBERTO.server = temp
            TipoConexionDONROBERTO.base = LeeIni("Conexion_DONROBERTO", "Origen")
            TipoConexionDONROBERTO.usuario = "sa"
            TipoConexionDONROBERTO.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringDONROBERTO = ""
        End If

        aux = IniGetSection(Archivo, "Conexion_CLINICA")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_CLINICA", "Server")
            ConnStringCLINICA = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringCLINICA = ConnStringCLINICA & LeeIni("Conexion_CLINICA", "Origen")
            ConnStringCLINICA = ConnStringCLINICA & ";Data Source="
            ConnStringCLINICA = ConnStringCLINICA & temp
            TipoConexionCLINICA.server = temp
            TipoConexionCLINICA.base = LeeIni("Conexion_CLINICA", "Origen")
            TipoConexionCLINICA.usuario = "sa"
            TipoConexionCLINICA.contrasena = Clave_Segun_Server(temp)
        Else
            ConnStringCLINICA = ""
        End If

        aux = IniGetSection(Archivo, "Conexion_Propiedades")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Propiedades", "Server")
            ConnStringPROPIEDADES = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringPROPIEDADES = ConnStringPROPIEDADES & LeeIni("Conexion_Propiedades", "Origen")
            ConnStringPROPIEDADES = ConnStringPROPIEDADES & ";Data Source="
            ConnStringPROPIEDADES = ConnStringPROPIEDADES & temp
            TipoConexionPROPIEDADES.server = temp
            TipoConexionPROPIEDADES.base = LeeIni("Conexion_Propiedades", "Origen")
            TipoConexionPROPIEDADES.usuario = "sa"
            TipoConexionPROPIEDADES.contrasena = Clave_Segun_Server(temp)
        Else
            ConnStringPROPIEDADES = ""
        End If

        aux = IniGetSection(Archivo, "Conexion_RestoBar")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Propiedades", "Server")
            ConnStringRESTOBAR = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Connect Timeout=0; Initial Catalog="
            ConnStringRESTOBAR = ConnStringRESTOBAR & LeeIni("Conexion_RestoBar", "Origen")
            ConnStringRESTOBAR = ConnStringRESTOBAR & ";Data Source="
            ConnStringRESTOBAR = ConnStringRESTOBAR & temp
            TipoConexionRESTOBAR.server = temp
            TipoConexionRESTOBAR.base = LeeIni("Conexion_RestoBar", "Origen")
            TipoConexionRESTOBAR.usuario = "sa"
            TipoConexionRESTOBAR.contrasena = Clave_Segun_Server(temp)
        Else
            ConnStringRESTOBAR = ""
        End If

    End Sub

    '    Public Function ControlarEstadoServidor(ByVal Servicio As String) As Boolean
    '        Dim nrointento As Integer = 0

    '        Try
    '            Dim myController As New System.ServiceProcess.ServiceController(Servicio) 'MSSQLSERVER

    'IntentarIniciar:

    '            If myController.Status.ToString = "Stopped" Then
    '                myController.Start()
    '            End If

    '            myController.Refresh()

    '            If myController.Status.ToString = "Stopped" Then
    '                If nrointento = 3 Then
    '                    MsgBox("Despues de 3 intentos, no se puede inicar el motor de la base de datos. Consulte con su Administrador", MsgBoxStyle.Critical, "Motor de la Base de Datos")
    '                    Return False
    '                Else
    '                    nrointento += 1
    '                    GoTo IntentarIniciar
    '                End If
    '            End If

    '            Return True

    '        Catch ex As Exception
    '            If nrointento = 3 Then
    '                MsgBox("Despues de 3 intentos, no se puede inicar el motor de la base de datos. Consulte con su Administrador", MsgBoxStyle.Critical, "Motor de la Base de Datos")
    '                Return False
    '            Else
    '                nrointento += 1
    '                GoTo IntentarIniciar
    '            End If
    '        End Try

    '    End Function

    Public Function NombreServidor() As String
        Dim temp As String
        temp = LeeIni("Conexion_FEAFIP", "Server")
        NombreServidor = temp
    End Function

    Public Sub Inicia_Conexion()
        ConfigurarCadenaConexion()
    End Sub

    Public Sub ObtenerNombrePC()
        sComputerName = SystemInformation.ComputerName.ToString
    End Sub

    Public Function ExecuteConsulta(ByVal consulta As String, ByVal Cnn As SqlClient.SqlConnection) As DataSet
        Try
            Dim ds As DataSet = New DataSet
            Dim command As SqlCommand = New SqlCommand(consulta, Cnn)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Cnn.Open()
            adapter.Fill(ds)
            ''Cnn.Close()
            Return ds
        Catch ex As Exception
            MsgBox("No se pudo conectar a la Base de Datos " + ex.Message, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Advertencia")
            Return Nothing
        Finally
            Cnn.Close()
        End Try
    End Function

    'Permite saber si el dato pasado existe en el combo
    Public Function ExisteDatoEnCombo(ByVal combo As ComboBox, ByVal cadena As String, ByVal campo As String) As Boolean

        For ni As Int32 = 0 To combo.Items.Count - 1
            Dim dr As DataRowView = CType(combo.Items.Item(ni), DataRowView)
            If cadena.ToUpperInvariant = dr(campo).ToString.ToUpperInvariant Then
                ExisteDatoEnCombo = True
                Exit Function
            End If
        Next ni

        ExisteDatoEnCombo = False

    End Function

    Public Sub AgregarIcono(ByRef grilla As DataGridView)

        'Dim treeIcon As New Icon(Recursos_Humanos.My.Resources.Resources.alert.GetHbitmap)
        'Dim iconColumn As New DataGridViewImageColumn()
        Dim iconColumn As New DataGridViewImageCell

        'With iconColumn
        '    .ImageLayout
        '    .Image = Recursos_Humanos.My.Resources.Resources.alert 'treeIcon.ToBitmap()
        '    .Name = "Tree"
        '    .HeaderText = "Nice tree"
        'End With

        'grilla.Columns.Insert(2, iconColumn)

    End Sub

    Public Function Existe(ByVal strFile As String) As Boolean

        Existe = System.IO.Directory.Exists(strFile)
    End Function

    Public Function Reverse(ByVal value As String) As String
        If value.Length > 1 Then
            Dim workingValue As New System.Text.StringBuilder
            For position As Int32 = value.Length - 1 To 0 Step -1
                workingValue.Append(value.Chars(position))
            Next
            Return workingValue.ToString
        Else
            Return value
        End If
    End Function

    Public Function TruncarUltimaCarpeta(ByVal path As String) As String
        Dim pos As Integer
        path = Reverse(path)

        pos = InStr(path, "\")

        If pos <> 0 Then
            path = Mid(path, pos + 1, path.Length)
            path = Reverse(path)
        Else
            path = ""
        End If
        TruncarUltimaCarpeta = path
    End Function

    Public Function CuentaAparicionesDeCaracter(ByVal cadena As String, ByVal caracter As Char)

        Dim contador As Integer
        contador = 0

        For Each c As Char In cadena.ToCharArray
            If c.ToString.Equals(caracter.ToString) Then
                contador = contador + 1
            End If
        Next

        Return contador

    End Function

    Public Sub LoguearUsoDelReporte(ByVal Nombre As String, ByVal usu As String)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Loguea el uso de un reporte
        '
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Nombre = Replace(Nombre, ".rpt", "")

        Dim connection As SqlClient.SqlConnection = Nothing
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Try

                Dim param_idreporte As New SqlClient.SqlParameter
                param_idreporte.ParameterName = "@IDreporte"
                param_idreporte.SqlDbType = SqlDbType.VarChar
                param_idreporte.Size = 50
                param_idreporte.Value = Nombre
                param_idreporte.Direction = ParameterDirection.Input

                Dim param_usu As New SqlClient.SqlParameter
                param_usu.ParameterName = "@User"
                param_usu.SqlDbType = SqlDbType.Int
                param_usu.Value = UserID ''CType(Util.MyCType(txtORDEN, "integer").Text, Integer)
                param_usu.Direction = ParameterDirection.Input

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spLoguearUsoDelReporte", param_idreporte, param_usu)
                    ''res = param_res.Value
                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        Exit Sub

    End Sub

    Public Function ExisteEntradaCombo(ByRef sender As ComboBox, ByRef e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        If Char.IsControl(e.KeyChar) Then
            ExisteEntradaCombo = True
            Exit Function
        End If
        Dim box As ComboBox = sender, i As Integer
        Dim nonSelected As String = box.Text.Substring(0, box.Text.Length - box.SelectionLength)
        Dim text As String = nonSelected + e.KeyChar
        Dim matched As Boolean = False, a As String
        For i = 0 To box.Items.Count - 1
            a = CType(box.Items(i), DataRowView)(box.ValueMember).ToString.ToUpperInvariant
            If a.StartsWith(text.ToUpperInvariant) Then
                matched = True
                Exit For
            End If
        Next
        ExisteEntradaCombo = matched
    End Function

    Public Function SacarEspaciosCadena(ByVal cadena As String)

        Dim words As String() = cadena.Split(New Char() {" "c})
        cadena = ""
        Dim word As String
        For Each word In words
            If Not (word.Trim = "") Then
                cadena = cadena & word.Trim + " "
            End If

        Next

        cadena = cadena.Trim

        Return cadena

    End Function

    Public Function VerificarSistemaUsuario(ByVal equipo As String, _
                                            ByVal sistema As String, _
                                            ByVal usuario As String, _
                                            ByVal ip As String, _
                                            ByRef mensaje As String, _
                                            ByRef equipousado As String, _
                                            ByRef ipusado As String) As Integer


        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_equipo As New SqlClient.SqlParameter
                param_equipo.ParameterName = "@equipo"
                param_equipo.SqlDbType = SqlDbType.VarChar
                param_equipo.Size = 50
                param_equipo.Value = equipo
                param_equipo.Direction = ParameterDirection.Input

                Dim param_sistema As New SqlClient.SqlParameter
                param_sistema.ParameterName = "@sistema"
                param_sistema.SqlDbType = SqlDbType.VarChar
                param_sistema.Size = 50
                param_sistema.Value = sistema
                param_sistema.Direction = ParameterDirection.Input

                Dim param_usuario As New SqlClient.SqlParameter
                param_usuario.ParameterName = "@usuario"
                param_usuario.SqlDbType = SqlDbType.VarChar
                param_usuario.Size = 50
                param_usuario.Value = usuario
                param_usuario.Direction = ParameterDirection.Input

                Dim param_ip As New SqlClient.SqlParameter
                param_ip.ParameterName = "@ip"
                param_ip.SqlDbType = SqlDbType.VarChar
                param_ip.Size = 50
                param_ip.Value = ip
                param_ip.Direction = ParameterDirection.Input

                Dim param_mensaje As New SqlClient.SqlParameter
                param_mensaje.ParameterName = "@mensaje"
                param_mensaje.SqlDbType = SqlDbType.VarChar
                param_mensaje.Size = 300
                param_mensaje.Value = DBNull.Value
                param_mensaje.Direction = ParameterDirection.Output




                Dim param_equipousado As New SqlClient.SqlParameter
                param_equipousado.ParameterName = "@equipo_usado"
                param_equipousado.SqlDbType = SqlDbType.VarChar
                param_equipousado.Size = 50
                param_equipousado.Value = equipousado
                param_equipousado.Direction = ParameterDirection.Output

                Dim param_ipusado As New SqlClient.SqlParameter
                param_ipusado.ParameterName = "@ip_usado"
                param_ipusado.SqlDbType = SqlDbType.VarChar
                param_ipusado.Size = 50
                param_ipusado.Value = ipusado
                param_ipusado.Direction = ParameterDirection.Output



                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_SistemaUsuario_Verificar", param_equipo, param_sistema, param_usuario, param_ip, param_mensaje, param_equipousado, param_ipusado, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If Not param_mensaje.Value Is DBNull.Value Then
                            mensaje = param_mensaje.Value
                        End If
                        If Not param_equipousado.Value Is DBNull.Value Then
                            equipousado = param_equipousado.Value
                        End If
                        If Not param_ipusado.Value Is DBNull.Value Then
                            ipusado = param_ipusado.Value
                        End If
                    End If

                    VerificarSistemaUsuario = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function EliminarSistemaUsuario(ByVal equipo As String, _
                                            ByRef sistema As String, _
                                            ByRef usuario As String, _
                                            ByVal ip As String, _
                                            ByRef mensaje As String) As Integer


        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_equipo As New SqlClient.SqlParameter
                param_equipo.ParameterName = "@equipo"
                param_equipo.SqlDbType = SqlDbType.VarChar
                param_equipo.Size = 50
                param_equipo.Value = equipo
                param_equipo.Direction = ParameterDirection.Input

                Dim param_sistema As New SqlClient.SqlParameter
                param_sistema.ParameterName = "@sistema"
                param_sistema.SqlDbType = SqlDbType.VarChar
                param_sistema.Size = 50
                param_sistema.Value = sistema
                param_sistema.Direction = ParameterDirection.Input

                Dim param_usuario As New SqlClient.SqlParameter
                param_usuario.ParameterName = "@usuario"
                param_usuario.SqlDbType = SqlDbType.VarChar
                param_usuario.Size = 50
                param_usuario.Value = usuario
                param_usuario.Direction = ParameterDirection.Input


                Dim param_ip As New SqlClient.SqlParameter
                param_ip.ParameterName = "@ip"
                param_ip.SqlDbType = SqlDbType.VarChar
                param_ip.Size = 50
                param_ip.Value = ip
                param_ip.Direction = ParameterDirection.Input

                Dim param_mensaje As New SqlClient.SqlParameter
                param_mensaje.ParameterName = "@mensaje"
                param_mensaje.SqlDbType = SqlDbType.VarChar
                param_mensaje.Size = 300
                param_mensaje.Value = DBNull.Value
                param_mensaje.Direction = ParameterDirection.Output

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_SistemaUsuario_Eliminar", param_equipo, param_sistema, param_usuario, param_ip, param_mensaje, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If Not param_mensaje.Value Is DBNull.Value Then
                            mensaje = param_mensaje.Value
                        End If
                    End If

                    EliminarSistemaUsuario = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function EliminarSistemaUsuarioActual(ByVal equipo As String, _
                                            ByRef sistema As String, _
                                            ByRef usuario As String, _
                                            ByVal ip As String, _
                                            ByRef mensaje As String) As Integer


        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_equipo As New SqlClient.SqlParameter
                param_equipo.ParameterName = "@equipo"
                param_equipo.SqlDbType = SqlDbType.VarChar
                param_equipo.Size = 50
                param_equipo.Value = equipo
                param_equipo.Direction = ParameterDirection.Input

                Dim param_sistema As New SqlClient.SqlParameter
                param_sistema.ParameterName = "@sistema"
                param_sistema.SqlDbType = SqlDbType.VarChar
                param_sistema.Size = 50
                param_sistema.Value = sistema
                param_sistema.Direction = ParameterDirection.Input

                Dim param_usuario As New SqlClient.SqlParameter
                param_usuario.ParameterName = "@usuario"
                param_usuario.SqlDbType = SqlDbType.VarChar
                param_usuario.Size = 50
                param_usuario.Value = usuario
                param_usuario.Direction = ParameterDirection.Input


                Dim param_ip As New SqlClient.SqlParameter
                param_ip.ParameterName = "@ip"
                param_ip.SqlDbType = SqlDbType.VarChar
                param_ip.Size = 50
                param_ip.Value = ip
                param_ip.Direction = ParameterDirection.Input

                Dim param_mensaje As New SqlClient.SqlParameter
                param_mensaje.ParameterName = "@mensaje"
                param_mensaje.SqlDbType = SqlDbType.VarChar
                param_mensaje.Size = 300
                param_mensaje.Value = DBNull.Value
                param_mensaje.Direction = ParameterDirection.Output

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_SistemaUsuario_Eliminar_Actual", param_equipo, param_sistema, param_usuario, param_ip, param_mensaje, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If Not param_mensaje.Value Is DBNull.Value Then
                            mensaje = param_mensaje.Value
                        End If
                    End If

                    EliminarSistemaUsuarioActual = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function BuscarIpSocket(ByRef ip As String) As Integer



        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_ipsocket As New SqlClient.SqlParameter
                param_ipsocket.ParameterName = "@ipsocket"
                param_ipsocket.SqlDbType = SqlDbType.VarChar
                param_ipsocket.Size = 50
                param_ipsocket.Value = DBNull.Value 'ip
                param_ipsocket.Direction = ParameterDirection.Output

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBuscarIpSocket", param_ipsocket, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 1 Then
                            If Not param_ipsocket.Value Is DBNull.Value Then
                                ip = param_ipsocket.Value
                            End If
                        End If
                    End If

                    BuscarIpSocket = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function


#End Region

#Region "Rotiseria"

    Public Function ObtenerMaterial_Roti(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef qtystock As Decimal, _
                                            ByRef Precio As Decimal) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringROTI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciocompra"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_idunidad, param_unidad, param_qtystock, _
                                              param_precio, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                        End If
                    End If

                    ObtenerMaterial_Roti = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Public Function ObtenerProducto_Roti(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef TiempoUnitario As Decimal, _
                                            ByRef Precio As Decimal, _
                                            ByRef Cod_Unidad As String) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringROTI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 300
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidadventa"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciovta"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_Tiempo As New SqlClient.SqlParameter
                param_Tiempo.ParameterName = "@tiempo"
                param_Tiempo.SqlDbType = SqlDbType.Decimal
                param_Tiempo.Precision = 18
                param_Tiempo.Scale = 2
                param_Tiempo.Value = DBNull.Value
                param_Tiempo.Direction = ParameterDirection.InputOutput

                Dim param_codUnidad As New SqlClient.SqlParameter
                param_codUnidad.ParameterName = "@cod_unidad"
                param_codUnidad.SqlDbType = SqlDbType.VarChar
                param_codUnidad.Size = 25
                param_codUnidad.Value = DBNull.Value
                param_codUnidad.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Productos_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_idunidad, param_unidad, param_precio, param_Tiempo, param_codUnidad, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                TiempoUnitario = param_Tiempo.Value
                            End If
                            If Not param_codUnidad.Value Is DBNull.Value Then
                                Cod_Unidad = param_codUnidad.Value
                            End If
                        End If
                    End If

                    ObtenerProducto_Roti = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerProveedor_Roti(ByVal Codigo As String, _
                                            ByRef ID_prov As Long, _
                                            ByRef nombre_prov As String) As Integer


        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringROTI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id_prov"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre_prov"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Proveedor_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID_prov = param_id.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre_prov = param_nombre.Value
                            End If
                        End If
                    End If

                    ObtenerProveedor_Roti = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerUnidad_Roti(ByVal Cod_Unidad As String, ByRef IDUnidad As Long, ByRef IdProducto As Long, _
                                       ByRef nombreunidad As String, ByRef tiempopreparacion As Decimal, _
                                       ByRef precio As Decimal) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringROTI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Cod_Unidad
                param_codigo.Direction = ParameterDirection.Input

                Dim param_idproducto As New SqlClient.SqlParameter
                param_idproducto.ParameterName = "@idproducto"
                param_idproducto.SqlDbType = SqlDbType.BigInt
                param_idproducto.Value = IdProducto
                param_idproducto.Direction = ParameterDirection.Input

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@unidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_tiempo As New SqlClient.SqlParameter
                param_tiempo.ParameterName = "@tiempo"
                param_tiempo.SqlDbType = SqlDbType.Decimal
                param_tiempo.Precision = 18
                param_tiempo.Scale = 2
                param_tiempo.Value = DBNull.Value
                param_tiempo.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@precio"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Unidades_Select_By_Codigo", param_codigo, _
                                              param_idunidad, param_idproducto, param_unidad, param_tiempo, param_precio, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IDUnidad = param_idunidad.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                nombreunidad = param_unidad.Value
                            End If
                            If Not param_tiempo.Value Is DBNull.Value Then
                                tiempopreparacion = param_tiempo.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                precio = param_precio.Value
                            End If
                        End If
                    End If

                    ObtenerUnidad_Roti = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

#Region "Funciones y Procedimientos APP"

    Public Function ObtenerMaterial_App(ByVal Codigo As String, _
                                          ByRef Codigo_Mat_Prov As String, _
                                          ByRef ID As Long, _
                                          ByRef nombre As String, _
                                          ByRef IdUnidad As Long, _
                                          ByRef NombreUnidad As String, _
                                          ByRef CodigoUnidad As String, _
                                          ByRef qtystock As Decimal, _
                                          ByRef minimo As Decimal, _
                                          ByRef maximo As Decimal, _
                                          ByRef PrecioLista As Decimal, _
                                          ByRef Ganancia As Decimal, _
                                          ByRef PrecioVta As Decimal, _
                                          ByRef PrecioVtaOrig As Decimal, _
                                          ByRef GananciaOrig As Decimal, _
                                          ByRef Iva As Decimal, _
                                          ByRef DateUpd As String, _
                                          ByRef IdProveedor As Long, _
                                          ByRef Proveedor As String, _
                                          ByRef IdMarca As Long, _
                                          ByRef Marca As String, _
                                          ByRef PlazoEntrega As String, _
                                          ByRef IdMoneda As Long, _
                                          ByRef CodMoneda As String, _
                                          ByRef ValorCambio As Decimal, _
                                          ByRef MontoIva As Decimal, _
                                          ByRef IdMat_Prov As Decimal, _
                                          ByVal Tablerista As Boolean, _
                                          ByVal Conexion As String, _
                                          Optional ByRef CodBarra As String = "", Optional ByRef Pasillo As String = "", _
                                          Optional ByRef Estante As String = "", Optional ByRef Fila As String = ""
                                      ) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_codigo_mat_prov As New SqlClient.SqlParameter
                param_codigo_mat_prov.ParameterName = "@codigo_mat_prov"
                param_codigo_mat_prov.SqlDbType = SqlDbType.VarChar
                param_codigo_mat_prov.Size = 25
                param_codigo_mat_prov.Value = Codigo_Mat_Prov
                param_codigo_mat_prov.Direction = ParameterDirection.InputOutput

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 300
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codigounidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 50
                param_codunidad.Value = DBNull.Value
                param_codunidad.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 2
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 2
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_preciolista As New SqlClient.SqlParameter
                param_preciolista.ParameterName = "@preciolista"
                param_preciolista.SqlDbType = SqlDbType.Decimal
                param_preciolista.Precision = 18
                param_preciolista.Scale = 2
                param_preciolista.Value = DBNull.Value
                param_preciolista.Direction = ParameterDirection.InputOutput

                Dim param_ganancia As New SqlClient.SqlParameter
                param_ganancia.ParameterName = "@ganancia"
                param_ganancia.SqlDbType = SqlDbType.Decimal
                param_ganancia.Precision = 18
                param_ganancia.Scale = 2
                param_ganancia.Value = DBNull.Value
                param_ganancia.Direction = ParameterDirection.InputOutput

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@Iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = Iva
                param_iva.Direction = ParameterDirection.InputOutput

                Dim param_preciovta As New SqlClient.SqlParameter
                param_preciovta.ParameterName = "@preciovta"
                param_preciovta.SqlDbType = SqlDbType.Decimal
                param_preciovta.Precision = 18
                param_preciovta.Scale = 2
                param_preciovta.Value = DBNull.Value
                param_preciovta.Direction = ParameterDirection.InputOutput

                Dim param_preciovtaorig As New SqlClient.SqlParameter
                param_preciovtaorig.ParameterName = "@preciovtaorig"
                param_preciovtaorig.SqlDbType = SqlDbType.Decimal
                param_preciovtaorig.Precision = 18
                param_preciovtaorig.Scale = 2
                param_preciovtaorig.Value = DBNull.Value
                param_preciovtaorig.Direction = ParameterDirection.InputOutput

                Dim param_gananciaorig As New SqlClient.SqlParameter
                param_gananciaorig.ParameterName = "@gananciaorig"
                param_gananciaorig.SqlDbType = SqlDbType.Decimal
                param_gananciaorig.Precision = 18
                param_gananciaorig.Scale = 2
                param_gananciaorig.Value = DBNull.Value
                param_gananciaorig.Direction = ParameterDirection.InputOutput

                Dim param_dateupd As New SqlClient.SqlParameter
                param_dateupd.ParameterName = "@dateupd"
                param_dateupd.SqlDbType = SqlDbType.VarChar
                param_dateupd.Size = 10
                param_dateupd.Value = DBNull.Value
                param_dateupd.Direction = ParameterDirection.InputOutput

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.BigInt
                param_idproveedor.Value = DBNull.Value
                param_idproveedor.Direction = ParameterDirection.InputOutput

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@proveedor"
                param_proveedor.SqlDbType = SqlDbType.VarChar
                param_proveedor.Size = 150
                param_proveedor.Value = DBNull.Value
                param_proveedor.Direction = ParameterDirection.InputOutput

                Dim param_idmarca As New SqlClient.SqlParameter
                param_idmarca.ParameterName = "@idmarca"
                param_idmarca.SqlDbType = SqlDbType.BigInt
                param_idmarca.Value = DBNull.Value
                param_idmarca.Direction = ParameterDirection.InputOutput

                Dim param_marca As New SqlClient.SqlParameter
                param_marca.ParameterName = "@marca"
                param_marca.SqlDbType = SqlDbType.VarChar
                param_marca.Size = 150
                param_marca.Value = DBNull.Value
                param_marca.Direction = ParameterDirection.InputOutput

                Dim param_idmoneda As New SqlClient.SqlParameter
                param_idmoneda.ParameterName = "@idmoneda"
                param_idmoneda.SqlDbType = SqlDbType.BigInt
                param_idmoneda.Value = DBNull.Value
                param_idmoneda.Direction = ParameterDirection.InputOutput

                Dim param_codMoneda As New SqlClient.SqlParameter
                param_codMoneda.ParameterName = "@codmoneda"
                param_codMoneda.SqlDbType = SqlDbType.VarChar
                param_codMoneda.Size = 10
                param_codMoneda.Value = DBNull.Value
                param_codMoneda.Direction = ParameterDirection.InputOutput

                Dim param_valorcambio As New SqlClient.SqlParameter
                param_valorcambio.ParameterName = "@valorcambio"
                param_valorcambio.SqlDbType = SqlDbType.Decimal
                param_valorcambio.Precision = 18
                param_valorcambio.Scale = 2
                param_valorcambio.Value = DBNull.Value
                param_valorcambio.Direction = ParameterDirection.InputOutput

                Dim param_plazoentrega As New SqlClient.SqlParameter
                param_plazoentrega.ParameterName = "@plazoentrega"
                param_plazoentrega.SqlDbType = SqlDbType.VarChar
                param_plazoentrega.Size = 50
                param_plazoentrega.Value = DBNull.Value
                param_plazoentrega.Direction = ParameterDirection.InputOutput

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = DBNull.Value
                param_montoiva.Direction = ParameterDirection.InputOutput

                Dim param_idmat_prov As New SqlClient.SqlParameter
                param_idmat_prov.ParameterName = "@IdMat_prov"
                param_idmat_prov.SqlDbType = SqlDbType.BigInt
                param_idmat_prov.Value = DBNull.Value
                param_idmat_prov.Direction = ParameterDirection.InputOutput

                Dim param_tablerista As New SqlClient.SqlParameter
                param_tablerista.ParameterName = "@Tablerista"
                param_tablerista.SqlDbType = SqlDbType.Bit
                param_tablerista.Value = Tablerista
                param_tablerista.Direction = ParameterDirection.Input

                Dim param_CodBarra As New SqlClient.SqlParameter
                param_CodBarra.ParameterName = "@CodBarra"
                param_CodBarra.SqlDbType = SqlDbType.VarChar
                param_CodBarra.Size = 50
                param_CodBarra.Value = DBNull.Value
                param_CodBarra.Direction = ParameterDirection.InputOutput

                Dim param_Pasillo As New SqlClient.SqlParameter
                param_Pasillo.ParameterName = "@Pasillo"
                param_Pasillo.SqlDbType = SqlDbType.VarChar
                param_Pasillo.Size = 50
                param_Pasillo.Value = DBNull.Value
                param_Pasillo.Direction = ParameterDirection.InputOutput

                Dim param_Fila As New SqlClient.SqlParameter
                param_Fila.ParameterName = "@Fila"
                param_Fila.SqlDbType = SqlDbType.VarChar
                param_Fila.Size = 50
                param_Fila.Value = DBNull.Value
                param_Fila.Direction = ParameterDirection.InputOutput

                Dim param_Estante As New SqlClient.SqlParameter
                param_Estante.ParameterName = "@Estante"
                param_Estante.SqlDbType = SqlDbType.VarChar
                param_Estante.Size = 50
                param_Estante.Value = DBNull.Value
                param_Estante.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Select_By_Codigo", param_codigo, _
                                              param_codigo_mat_prov, param_id, param_nombre, param_idunidad, param_unidad, param_codunidad, param_qtystock, _
                                              param_minimo, param_maximo, param_preciolista, param_ganancia, param_preciovta, _
                                              param_preciovtaorig, param_gananciaorig, param_dateupd, param_iva, _
                                              param_proveedor, param_idproveedor, param_idmarca, param_marca, _
                                              param_idmoneda, param_codMoneda, param_valorcambio, param_plazoentrega, _
                                              param_montoiva, param_idmat_prov, param_tablerista, param_CodBarra, _
                                              param_Pasillo, param_Fila, param_Estante, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_codigo_mat_prov.Value Is DBNull.Value Then
                                Codigo_Mat_Prov = param_codigo_mat_prov.Value
                            End If
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodigoUnidad = param_codunidad.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If
                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            If Not param_preciolista.Value Is DBNull.Value Then
                                PrecioLista = param_preciolista.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                Ganancia = param_ganancia.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                PrecioVta = param_preciovta.Value
                            End If
                            If Not param_preciovtaorig.Value Is DBNull.Value Then
                                PrecioVtaOrig = param_preciovtaorig.Value
                            End If
                            If Not param_gananciaorig.Value Is DBNull.Value Then
                                GananciaOrig = param_gananciaorig.Value
                            End If
                            If Not param_iva.Value Is DBNull.Value Then
                                Iva = param_iva.Value
                            End If
                            If Not param_dateupd.Value Is DBNull.Value Then
                                DateUpd = param_dateupd.Value
                            End If
                            If Not param_idproveedor.Value Is DBNull.Value Then
                                IdProveedor = param_idproveedor.Value
                            End If
                            If Not param_proveedor.Value Is DBNull.Value Then
                                Proveedor = param_proveedor.Value
                            End If
                            If Not param_idmarca.Value Is DBNull.Value Then
                                IdMarca = param_idmarca.Value
                            End If
                            If Not param_marca.Value Is DBNull.Value Then
                                Marca = param_marca.Value
                            End If
                            If Not param_idmoneda.Value Is DBNull.Value Then
                                IdMoneda = param_idmoneda.Value
                            End If
                            If Not param_codMoneda.Value Is DBNull.Value Then
                                CodMoneda = param_codMoneda.Value
                            End If
                            If Not param_valorcambio.Value Is DBNull.Value Then
                                ValorCambio = param_valorcambio.Value
                            End If
                            If Not param_plazoentrega.Value Is DBNull.Value Then
                                PlazoEntrega = param_plazoentrega.Value
                            End If
                            If Not param_montoiva.Value Is DBNull.Value Then
                                MontoIva = param_montoiva.Value
                            End If
                            If Not param_idmat_prov.Value Is DBNull.Value Then
                                IdMat_Prov = param_idmat_prov.Value
                            End If
                            If Not param_CodBarra.Value Is DBNull.Value Then
                                CodBarra = param_CodBarra.Value
                            End If
                            If Not param_Pasillo.Value Is DBNull.Value Then
                                Pasillo = param_Pasillo.Value
                            End If
                            If Not param_Estante.Value Is DBNull.Value Then
                                Estante = param_Estante.Value
                            End If
                            If Not param_Fila.Value Is DBNull.Value Then
                                Fila = param_Fila.Value
                            End If
                        End If
                    End If

                    ObtenerMaterial_App = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerProveedor_App(ByVal Codigo As String, _
                                            ByRef ID_prov As Long, _
                                            ByRef nombre_prov As String, _
                                            ByRef ganancia As Decimal, _
                                            ByRef bonificacion As Decimal, _
                                            ByVal Conexion As String) As Integer


        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion) '(ConnStringACER)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id_prov"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre_prov"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_ganancia As New SqlClient.SqlParameter
                param_ganancia.ParameterName = "@ganancia"
                param_ganancia.SqlDbType = SqlDbType.Decimal
                param_ganancia.Precision = 18
                param_ganancia.Scale = 2
                param_ganancia.Value = DBNull.Value
                param_ganancia.Direction = ParameterDirection.InputOutput

                Dim param_bonificacion As New SqlClient.SqlParameter
                param_bonificacion.ParameterName = "@bonificacion"
                param_bonificacion.SqlDbType = SqlDbType.Decimal
                param_bonificacion.Precision = 18
                param_bonificacion.Scale = 2
                param_bonificacion.Value = DBNull.Value
                param_bonificacion.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Proveedor_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_ganancia, param_bonificacion, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID_prov = param_id.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre_prov = param_nombre.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                ganancia = param_ganancia.Value
                            End If
                            If Not param_bonificacion.Value Is DBNull.Value Then
                                bonificacion = param_bonificacion.Value
                            End If
                        End If
                    End If

                    ObtenerProveedor_App = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerUnidad_App(ByVal Cod_Unidad As String, ByRef IDUnidad As Long, _
                                       ByRef nombreunidad As String, ByRef CodUnidad As String, ByVal Conexion As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion) '(ConnStringACER)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Cod_Unidad
                param_codigo.Direction = ParameterDirection.Input

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codunidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 25
                param_codunidad.Value = CodUnidad
                param_codunidad.Direction = ParameterDirection.Output

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@unidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spUnidades_Select_By_Codigo", param_codigo, _
                                              param_idunidad, param_unidad, param_codunidad, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IDUnidad = param_idunidad.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                nombreunidad = param_unidad.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodUnidad = param_codunidad.Value
                            End If
                        End If
                    End If

                    ObtenerUnidad_App = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerMoneda_App(ByVal Cod_Moneda As String, ByRef IDMoneda As Long, _
                                       ByRef nombremoneda As String, ByRef CodMoneda As String, ByRef ValorCambio As Double, ByVal Conexion As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Cod_Moneda
                param_codigo.Direction = ParameterDirection.Input

                Dim param_codmoneda As New SqlClient.SqlParameter
                param_codmoneda.ParameterName = "@codmoneda"
                param_codmoneda.SqlDbType = SqlDbType.VarChar
                param_codmoneda.Size = 25
                param_codmoneda.Value = CodMoneda
                param_codmoneda.Direction = ParameterDirection.Output

                Dim param_idmoneda As New SqlClient.SqlParameter
                param_idmoneda.ParameterName = "@idmoneda"
                param_idmoneda.SqlDbType = SqlDbType.BigInt
                param_idmoneda.Value = DBNull.Value
                param_idmoneda.Direction = ParameterDirection.InputOutput

                Dim param_moneda As New SqlClient.SqlParameter
                param_moneda.ParameterName = "@moneda"
                param_moneda.SqlDbType = SqlDbType.VarChar
                param_moneda.Size = 50
                param_moneda.Value = DBNull.Value
                param_moneda.Direction = ParameterDirection.InputOutput

                Dim param_valorcambio As New SqlClient.SqlParameter
                param_valorcambio.ParameterName = "@valorcambio"
                param_valorcambio.SqlDbType = SqlDbType.Decimal
                param_valorcambio.Precision = 18
                param_valorcambio.Scale = 2
                param_valorcambio.Value = DBNull.Value
                param_valorcambio.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMonedas_Select_By_Codigo", param_codigo, _
                                              param_idmoneda, param_moneda, param_codmoneda, param_valorcambio, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_idmoneda.Value Is DBNull.Value Then
                                IDMoneda = param_idmoneda.Value
                            End If
                            If Not param_moneda.Value Is DBNull.Value Then
                                nombremoneda = param_moneda.Value
                            End If
                            If Not param_codmoneda.Value Is DBNull.Value Then
                                CodMoneda = param_codmoneda.Value
                            End If
                            If Not param_valorcambio.Value Is DBNull.Value Then
                                ValorCambio = param_valorcambio.Value
                            End If
                        End If
                    End If

                    ObtenerMoneda_App = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerMarca_App(ByVal Cod_Marca As String, ByRef IDMarca As Long, _
                                      ByRef nombremarca As String, ByRef CodMarca As String, ByVal Conexion As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Cod_Marca
                param_codigo.Direction = ParameterDirection.Input

                Dim param_codmarca As New SqlClient.SqlParameter
                param_codmarca.ParameterName = "@codmarca"
                param_codmarca.SqlDbType = SqlDbType.VarChar
                param_codmarca.Size = 25
                param_codmarca.Value = CodMarca
                param_codmarca.Direction = ParameterDirection.Output

                Dim param_idmarca As New SqlClient.SqlParameter
                param_idmarca.ParameterName = "@idmarca"
                param_idmarca.SqlDbType = SqlDbType.BigInt
                param_idmarca.Value = DBNull.Value
                param_idmarca.Direction = ParameterDirection.InputOutput

                Dim param_marca As New SqlClient.SqlParameter
                param_marca.ParameterName = "@marca"
                param_marca.SqlDbType = SqlDbType.VarChar
                param_marca.Size = 50
                param_marca.Value = DBNull.Value
                param_marca.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMarcas_Select_By_Codigo", param_codigo, _
                                              param_idmarca, param_marca, param_codmarca, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_idmarca.Value Is DBNull.Value Then
                                IDMarca = param_idmarca.Value
                            End If
                            If Not param_marca.Value Is DBNull.Value Then
                                nombremarca = param_marca.Value
                            End If
                            If Not param_codmarca.Value Is DBNull.Value Then
                                CodMarca = param_codmarca.Value
                            End If
                        End If
                    End If

                    ObtenerMarca_App = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerMoneda_ValorCambioDolar(cnn As String) As String
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ObtenerMoneda_ValorCambioDolar = ""
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

                'Me.ToolStrip_lblCambio.Text = " 1 u$$ = $ " & CDec(param_dolar.Value)
                'euro = CDec(param_euro.Value)

                ObtenerMoneda_ValorCambioDolar = param_dolar.Value


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
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ObtenerMoneda_ValorCambioDolar = ""

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Public Function ObtenerDatosDelUserId(ByVal Id As Long, _
                                            ByRef codigo As String, _
                                            ByRef nombre As String, _
                                            ByRef pass As String, _
                                            ByVal Conexion As String) As Integer


        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, nombre As String, pass_actual As String
        'codigo = "" : nombre = "" : pass_actual = "" 
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try

                'connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                connection = SqlHelper.GetConnection(Conexion)

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = Id
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 30
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 60
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_pass As New SqlClient.SqlParameter
                param_pass.ParameterName = "@pass"
                param_pass.SqlDbType = SqlDbType.VarChar
                param_pass.Size = 50
                param_pass.Value = DBNull.Value
                param_pass.Direction = ParameterDirection.InputOutput


                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spObtenerDatosDelUserId", param_id, param_codigo, param_nombre, param_pass, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    End If
                    If Not param_codigo.Value Is DBNull.Value Then
                        codigo = param_codigo.Value
                    End If
                    If Not param_nombre.Value Is DBNull.Value Then
                        nombre = param_nombre.Value
                    End If
                    If Not param_pass.Value Is DBNull.Value Then
                        pass = param_pass.Value
                    End If

                    ObtenerDatosDelUserId = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ControlarNombreProducto(ByVal Nombre As String, ByVal Conexion As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(Conexion)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_nombre As New SqlClient.SqlParameter
            param_nombre.ParameterName = "@nombre"
            param_nombre.SqlDbType = SqlDbType.VarChar
            param_nombre.Size = 255
            param_nombre.Value = RTrim(LTrim(Nombre))
            param_nombre.Direction = ParameterDirection.Input

            Dim param_Cant As New SqlClient.SqlParameter
            param_Cant.ParameterName = "@Cant"
            param_Cant.SqlDbType = SqlDbType.Int
            param_Cant.Value = DBNull.Value
            param_Cant.Direction = ParameterDirection.InputOutput

            Try

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_ControlarNombre", _
                                param_Cant, param_nombre)

                ControlarNombreProducto = param_Cant.Value

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
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

    Public Function BuscarFactura(ByVal ConexionBD As String, ByVal NroFactura As String, ByVal TipoComprobante As String, ByVal Sistema As String, ByRef CodigoBarra As String, ByRef IdFactura As Long) As Boolean

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConexionBD)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try

            If Sistema = "FEAFIP" Or Sistema.Contains("Magenta2") Then
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select c.codigo, c.comprobantenro, CodigoBarra, c.id from Consumos c JOIN Comprobantes cp ON convert(int, cp.codigo) = c.comprobantetipo where cp.descripcion = '" & TipoComprobante & "' and comprobantenro =  " & NroFactura)
            Else
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select f.codigo, f.nrofactura, ISNULL(CodigoBarra,''), f.id from Facturacion f JOIN Comprobantes cp ON convert(int, cp.codigo) = f.comprobantetipo where (CAE <> '' OR CAE IS NOT NULL) AND cp.descripcion = '" & TipoComprobante & "' and nrofactura =  " & NroFactura)
            End If

            ds.Dispose()

            If ds.Tables(0).Rows.Count = 0 Then
                BuscarFactura = False
            Else
                CodigoBarra = ds.Tables(0).Rows(0).Item(2)
                IdFactura = ds.Tables(0).Rows(0).Item(3)
                BuscarFactura = True
            End If

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            BuscarFactura = False

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Function

#End Region

#Region "Acer"

    Public Function ObtenerMaterial_Acer(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef CodigoUnidad As String, _
                                            ByRef qtystock As Decimal, _
                                            ByRef minimo As Decimal, _
                                            ByRef maximo As Decimal, _
                                            ByRef Precio As Decimal, _
                                            ByRef Ganancia As Decimal, _
                                            ByRef PrecioVta As Decimal, _
                                            ByRef PrecioVtaOrig As Decimal, _
                                            ByRef GananciaOrig As Decimal, _
                                            ByRef Iva As Decimal, _
                                            ByRef DateUpd As String, _
                                            ByRef IdProveedor As Long, _
                                            ByRef Proveedor As String _
                                        ) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringACER)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codigounidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 50
                param_codunidad.Value = DBNull.Value
                param_codunidad.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 2
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 2
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciolista"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_ganancia As New SqlClient.SqlParameter
                param_ganancia.ParameterName = "@ganancia"
                param_ganancia.SqlDbType = SqlDbType.Decimal
                param_ganancia.Precision = 18
                param_ganancia.Scale = 2
                param_ganancia.Value = DBNull.Value
                param_ganancia.Direction = ParameterDirection.InputOutput

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@Iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = Iva
                param_iva.Direction = ParameterDirection.Input

                Dim param_preciovta As New SqlClient.SqlParameter
                param_preciovta.ParameterName = "@preciovta"
                param_preciovta.SqlDbType = SqlDbType.Decimal
                param_preciovta.Precision = 18
                param_preciovta.Scale = 2
                param_preciovta.Value = DBNull.Value
                param_preciovta.Direction = ParameterDirection.InputOutput

                Dim param_preciovtaorig As New SqlClient.SqlParameter
                param_preciovtaorig.ParameterName = "@preciovtaorig"
                param_preciovtaorig.SqlDbType = SqlDbType.Decimal
                param_preciovtaorig.Precision = 18
                param_preciovtaorig.Scale = 2
                param_preciovtaorig.Value = DBNull.Value
                param_preciovtaorig.Direction = ParameterDirection.InputOutput

                Dim param_gananciaorig As New SqlClient.SqlParameter
                param_gananciaorig.ParameterName = "@gananciaorig"
                param_gananciaorig.SqlDbType = SqlDbType.Decimal
                param_gananciaorig.Precision = 18
                param_gananciaorig.Scale = 2
                param_gananciaorig.Value = DBNull.Value
                param_gananciaorig.Direction = ParameterDirection.InputOutput

                Dim param_dateupd As New SqlClient.SqlParameter
                param_dateupd.ParameterName = "@dateupd"
                param_dateupd.SqlDbType = SqlDbType.VarChar
                param_dateupd.Size = 10
                param_dateupd.Value = DBNull.Value
                param_dateupd.Direction = ParameterDirection.InputOutput

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.BigInt
                param_idproveedor.Value = DBNull.Value
                param_idproveedor.Direction = ParameterDirection.InputOutput

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@proveedor"
                param_proveedor.SqlDbType = SqlDbType.VarChar
                param_proveedor.Size = 50
                param_proveedor.Value = DBNull.Value
                param_proveedor.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_idunidad, param_unidad, param_codunidad, param_qtystock, _
                                              param_minimo, param_maximo, param_precio, param_ganancia, param_preciovta, _
                                              param_preciovtaorig, param_gananciaorig, param_dateupd, param_iva, _
                                              param_proveedor, param_idproveedor, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodigoUnidad = param_codunidad.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If
                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                Ganancia = param_ganancia.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                PrecioVta = param_preciovta.Value
                            End If
                            If Not param_preciovtaorig.Value Is DBNull.Value Then
                                PrecioVtaOrig = param_preciovtaorig.Value
                            End If
                            If Not param_gananciaorig.Value Is DBNull.Value Then
                                GananciaOrig = param_gananciaorig.Value
                            End If
                            If Not param_dateupd.Value Is DBNull.Value Then
                                DateUpd = param_dateupd.Value
                            End If
                            If Not param_idproveedor.Value Is DBNull.Value Then
                                IdProveedor = param_idproveedor.Value
                            End If
                            If Not param_proveedor.Value Is DBNull.Value Then
                                Proveedor = param_proveedor.Value
                            End If

                        End If
                    End If

                    ObtenerMaterial_Acer = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function BuscarDatosMaterialPorID_ACER(ByVal id As Long, _
                                          ByRef codigo As String, _
                                          ByRef nombre As String, _
                                          ByRef idunidad As Long) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringACER)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = id
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.Output

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.Output

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.Output

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBuscarDatosMaterialPorID", _
                                              param_id, param_codigo, param_nombre, _
                                            param_idunidad, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 1 Then
                            If Not param_codigo.Value Is DBNull.Value Then
                                codigo = param_codigo.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                idunidad = param_idunidad.Value
                            End If
                        End If
                    End If

                    BuscarDatosMaterialPorID_ACER = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

#Region "Financiera"

    Public Function ObtenerMaterial_Financiera(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef qtystock As Decimal, _
                                            ByRef Precio As Decimal, _
                                            ByRef PrecioCompra As Decimal, _
                                            ByRef IDunidad As Long) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringFINANCIERA)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciosiniva"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_preciocompra As New SqlClient.SqlParameter
                param_preciocompra.ParameterName = "@preciocompra"
                param_preciocompra.SqlDbType = SqlDbType.Decimal
                param_preciocompra.Precision = 18
                param_preciocompra.Scale = 2
                param_preciocompra.Value = DBNull.Value
                param_preciocompra.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_qtystock, param_preciocompra, _
                                              param_precio, param_idunidad, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_preciocompra.Value Is DBNull.Value Then
                                PrecioCompra = param_preciocompra.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IDunidad = param_idunidad.Value
                            End If
                        End If
                    End If

                    ObtenerMaterial_Financiera = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerGastoCorriente_Financiera(ByRef ID As Long, ByRef nombre As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringFINANCIERA)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = ID
                param_id.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spGastosCorrientes_Select_By_Id", _
                                              param_id, param_nombre, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 1 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If

                        End If
                    End If

                    ObtenerGastoCorriente_Financiera = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    Public Function ObtenerEmpleado_Financiera(ByRef ID As Long, ByRef nombre As String) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringFINANCIERA)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = ID
                param_id.Direction = ParameterDirection.Input

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spEmpleados_Select_By_Id", _
                                              param_id, param_nombre, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 1 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If

                        End If
                    End If

                    ObtenerEmpleado_Financiera = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

#Region "Cronos"

    Public Function ObtenerMaterial_Cronos(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef CodigoUnidad As String, _
                                            ByRef qtystock As Decimal, _
                                            ByRef minimo As Decimal, _
                                            ByRef maximo As Decimal, _
                                            ByRef Precio As Decimal, _
                                            ByRef Ganancia As Decimal, _
                                            ByRef PrecioVta As Decimal, _
                                            ByRef PrecioVtaOrig As Decimal, _
                                            ByRef GananciaOrig As Decimal, _
                                            ByRef Iva As Decimal, _
                                            ByRef DateUpd As String, _
                                            ByRef IdProveedor As Long, _
                                            ByRef Proveedor As String, _
                                            ByRef idmoneda As Long, _
                                            ByRef codigoMoneda As String, _
                                            ByRef nombreMoneda As String, _
                                            ByRef qtyxBulto As Decimal, _
                                            ByVal conexion As String, _
                                            Optional ByVal IdProveedorOc As Long = 0 _
                                        ) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codigounidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 50
                param_codunidad.Value = DBNull.Value
                param_codunidad.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 2
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 2
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciolista"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_ganancia As New SqlClient.SqlParameter
                param_ganancia.ParameterName = "@ganancia"
                param_ganancia.SqlDbType = SqlDbType.Decimal
                param_ganancia.Precision = 18
                param_ganancia.Scale = 2
                param_ganancia.Value = DBNull.Value
                param_ganancia.Direction = ParameterDirection.InputOutput

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@Iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = Iva
                param_iva.Direction = ParameterDirection.Input

                Dim param_preciovta As New SqlClient.SqlParameter
                param_preciovta.ParameterName = "@preciovta"
                param_preciovta.SqlDbType = SqlDbType.Decimal
                param_preciovta.Precision = 18
                param_preciovta.Scale = 2
                param_preciovta.Value = DBNull.Value
                param_preciovta.Direction = ParameterDirection.InputOutput

                Dim param_preciovtaorig As New SqlClient.SqlParameter
                param_preciovtaorig.ParameterName = "@preciovtaorig"
                param_preciovtaorig.SqlDbType = SqlDbType.Decimal
                param_preciovtaorig.Precision = 18
                param_preciovtaorig.Scale = 2
                param_preciovtaorig.Value = DBNull.Value
                param_preciovtaorig.Direction = ParameterDirection.InputOutput

                Dim param_gananciaorig As New SqlClient.SqlParameter
                param_gananciaorig.ParameterName = "@gananciaorig"
                param_gananciaorig.SqlDbType = SqlDbType.Decimal
                param_gananciaorig.Precision = 18
                param_gananciaorig.Scale = 2
                param_gananciaorig.Value = DBNull.Value
                param_gananciaorig.Direction = ParameterDirection.InputOutput

                Dim param_dateupd As New SqlClient.SqlParameter
                param_dateupd.ParameterName = "@dateupd"
                param_dateupd.SqlDbType = SqlDbType.VarChar
                param_dateupd.Size = 10
                param_dateupd.Value = DBNull.Value
                param_dateupd.Direction = ParameterDirection.InputOutput

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.BigInt
                param_idproveedor.Value = DBNull.Value
                param_idproveedor.Direction = ParameterDirection.InputOutput

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@proveedor"
                param_proveedor.SqlDbType = SqlDbType.VarChar
                param_proveedor.Size = 50
                param_proveedor.Value = DBNull.Value
                param_proveedor.Direction = ParameterDirection.InputOutput

                Dim param_proveedorOc As New SqlClient.SqlParameter
                param_proveedorOc.ParameterName = "@IdProveedorOc"
                param_proveedorOc.SqlDbType = SqlDbType.BigInt
                param_proveedorOc.Value = IdProveedorOc
                param_proveedorOc.Direction = ParameterDirection.Input

                Dim param_idmoneda As New SqlClient.SqlParameter
                param_idmoneda.ParameterName = "@idmoneda"
                param_idmoneda.SqlDbType = SqlDbType.BigInt
                param_idmoneda.Value = DBNull.Value
                param_idmoneda.Direction = ParameterDirection.InputOutput

                Dim param_codigoMoneda As New SqlClient.SqlParameter
                param_codigoMoneda.ParameterName = "@codigoMoneda"
                param_codigoMoneda.SqlDbType = SqlDbType.VarChar
                param_codigoMoneda.Size = 50
                param_codigoMoneda.Value = DBNull.Value
                param_codigoMoneda.Direction = ParameterDirection.InputOutput

                Dim param_nombreMoneda As New SqlClient.SqlParameter
                param_nombreMoneda.ParameterName = "@nombreMoneda"
                param_nombreMoneda.SqlDbType = SqlDbType.VarChar
                param_nombreMoneda.Size = 50
                param_nombreMoneda.Value = DBNull.Value
                param_nombreMoneda.Direction = ParameterDirection.InputOutput

                Dim param_qtyxBulto As New SqlClient.SqlParameter
                param_qtyxBulto.ParameterName = "@qtyxBulto"
                param_qtyxBulto.SqlDbType = SqlDbType.Decimal
                param_qtyxBulto.Precision = 18
                param_qtyxBulto.Scale = 2
                param_qtyxBulto.Value = DBNull.Value
                param_qtyxBulto.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_idunidad, param_unidad, param_codunidad, param_qtystock, _
                                              param_minimo, param_maximo, param_precio, param_ganancia, param_preciovta, _
                                              param_preciovtaorig, param_gananciaorig, param_dateupd, param_iva, _
                                              param_proveedor, param_idproveedor, param_idmoneda, param_codigoMoneda, param_nombreMoneda, param_qtyxBulto, param_res, param_proveedorOc)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodigoUnidad = param_codunidad.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If
                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                Ganancia = param_ganancia.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                PrecioVta = param_preciovta.Value
                            End If
                            If Not param_preciovtaorig.Value Is DBNull.Value Then
                                PrecioVtaOrig = param_preciovtaorig.Value
                            End If
                            If Not param_gananciaorig.Value Is DBNull.Value Then
                                GananciaOrig = param_gananciaorig.Value
                            End If
                            If Not param_dateupd.Value Is DBNull.Value Then
                                DateUpd = param_dateupd.Value
                            End If
                            If Not param_idproveedor.Value Is DBNull.Value Then
                                IdProveedor = param_idproveedor.Value
                            End If
                            If Not param_proveedor.Value Is DBNull.Value Then
                                Proveedor = param_proveedor.Value
                            End If
                            If Not param_idmoneda.Value Is DBNull.Value Then
                                idmoneda = param_idmoneda.Value
                            End If
                            If Not param_codigoMoneda.Value Is DBNull.Value Then
                                codigoMoneda = param_codigoMoneda.Value
                            End If
                            If Not param_nombreMoneda.Value Is DBNull.Value Then
                                nombreMoneda = param_nombreMoneda.Value
                            End If
                            If Not param_qtyxBulto.Value Is DBNull.Value Then
                                qtyxBulto = param_qtyxBulto.Value
                            End If

                        End If
                    End If

                    ObtenerMaterial_Cronos = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

#Region "FEAFIP"

    Public Function ObtenerMaterial_FEAFIP(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef descripcion As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef CodigoUnidad As String, _
                                            ByRef NombreUnidad As String, _
                                            ByRef Precio As Decimal, _
                                            ByRef iva As String, _
                                            ByRef montoiva As Decimal, _
                                            ByVal conexion As String
                                        ) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_descripcion As New SqlClient.SqlParameter
                param_descripcion.ParameterName = "@descripcion"
                param_descripcion.SqlDbType = SqlDbType.VarChar
                param_descripcion.Size = 70
                param_descripcion.Value = DBNull.Value
                param_descripcion.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codigounidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 50
                param_codunidad.Value = DBNull.Value
                param_codunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@precio"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@Iva"
                param_iva.SqlDbType = SqlDbType.VarChar
                param_iva.Size = 10
                param_iva.Value = DBNull.Value
                param_iva.Direction = ParameterDirection.InputOutput

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = DBNull.Value
                param_montoiva.Direction = ParameterDirection.InputOutput

                'Dim param_idmoneda As New SqlClient.SqlParameter
                'param_idmoneda.ParameterName = "@idmoneda"
                'param_idmoneda.SqlDbType = SqlDbType.BigInt
                'param_idmoneda.Value = DBNull.Value
                'param_idmoneda.Direction = ParameterDirection.InputOutput

                'Dim param_codigoMoneda As New SqlClient.SqlParameter
                'param_codigoMoneda.ParameterName = "@CodMoneda"
                'param_codigoMoneda.SqlDbType = SqlDbType.VarChar
                'param_codigoMoneda.Size = 10
                'param_codigoMoneda.Value = DBNull.Value
                'param_codigoMoneda.Direction = ParameterDirection.InputOutput

                'Dim param_nombreMoneda As New SqlClient.SqlParameter
                'param_nombreMoneda.ParameterName = "@nombreMoneda"
                'param_nombreMoneda.SqlDbType = SqlDbType.VarChar
                'param_nombreMoneda.Size = 50
                'param_nombreMoneda.Value = DBNull.Value
                'param_nombreMoneda.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_1_Select_By_Codigo", param_codigo, _
                                              param_id, param_descripcion, param_idunidad, param_codunidad, param_unidad, _
                                              param_precio, param_iva, param_montoiva, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_descripcion.Value Is DBNull.Value Then
                                descripcion = param_descripcion.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodigoUnidad = param_codunidad.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_iva.Value Is DBNull.Value Then
                                iva = param_iva.Value
                            End If
                            If Not param_montoiva.Value Is DBNull.Value Then
                                montoiva = param_montoiva.Value
                            End If
                            'If Not param_idmoneda.Value Is DBNull.Value Then
                            '    idmoneda = param_idmoneda.Value
                            'End If
                            'If Not param_codigoMoneda.Value Is DBNull.Value Then
                            '    codigoMoneda = param_codigoMoneda.Value
                            'End If
                            'If Not param_nombreMoneda.Value Is DBNull.Value Then
                            '    nombreMoneda = param_nombreMoneda.Value
                            'End If

                        End If
                    End If

                    ObtenerMaterial_FEAFIP = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

#Region "Moto_HOLLMAN"

    Public Function ObtenerMaterial_MOTO_HOLLMAN(ByVal Codigo As String, _
                                           ByRef ID As Long, _
                                           ByRef nombre As String, _
                                           ByRef IdUnidad As Long, _
                                           ByRef CodigoUnidad As String, _
                                           ByRef NombreUnidad As String, _
                                           ByRef qtystock As Decimal, _
                                           ByRef minimo As Decimal, _
                                           ByRef maximo As Decimal, _
                                           ByRef Precio As Decimal, _
                                           ByRef Ganancia As Decimal, _
                                           ByRef PrecioVta As Decimal, _
                                           ByRef PrecioVtaOrig As Decimal, _
                                           ByRef GananciaOrig As Decimal, _
                                           ByRef DateUpd As String, _
                                           ByRef IdProveedor As Long, _
                                           ByRef Proveedor As String, _
                                           ByRef idmoneda As Long, _
                                           ByRef codigoMoneda As String, _
                                           ByRef nombreMoneda As String, _
                                           ByRef valorcambio As Decimal, _
                                           ByVal conexion As String
                                       ) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 70
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codigounidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 50
                param_codunidad.Value = DBNull.Value
                param_codunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 2
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 2
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciolista"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_ganancia As New SqlClient.SqlParameter
                param_ganancia.ParameterName = "@ganancia"
                param_ganancia.SqlDbType = SqlDbType.Decimal
                param_ganancia.Precision = 18
                param_ganancia.Scale = 2
                param_ganancia.Value = DBNull.Value
                param_ganancia.Direction = ParameterDirection.InputOutput

                Dim param_preciovta As New SqlClient.SqlParameter
                param_preciovta.ParameterName = "@preciovta"
                param_preciovta.SqlDbType = SqlDbType.Decimal
                param_preciovta.Precision = 18
                param_preciovta.Scale = 2
                param_preciovta.Value = DBNull.Value
                param_preciovta.Direction = ParameterDirection.InputOutput

                Dim param_preciovtaorig As New SqlClient.SqlParameter
                param_preciovtaorig.ParameterName = "@preciovtaorig"
                param_preciovtaorig.SqlDbType = SqlDbType.Decimal
                param_preciovtaorig.Precision = 18
                param_preciovtaorig.Scale = 2
                param_preciovtaorig.Value = DBNull.Value
                param_preciovtaorig.Direction = ParameterDirection.InputOutput

                Dim param_gananciaorig As New SqlClient.SqlParameter
                param_gananciaorig.ParameterName = "@gananciaorig"
                param_gananciaorig.SqlDbType = SqlDbType.Decimal
                param_gananciaorig.Precision = 18
                param_gananciaorig.Scale = 2
                param_gananciaorig.Value = DBNull.Value
                param_gananciaorig.Direction = ParameterDirection.InputOutput

                'Dim param_iva As New SqlClient.SqlParameter
                'param_iva.ParameterName = "@Iva"
                'param_iva.SqlDbType = SqlDbType.Decimal
                'param_iva.Precision = 18
                'param_iva.Scale = 2
                'param_iva.Value = Iva
                'param_iva.Direction = ParameterDirection.Input

                Dim param_dateupd As New SqlClient.SqlParameter
                param_dateupd.ParameterName = "@dateupd"
                param_dateupd.SqlDbType = SqlDbType.VarChar
                param_dateupd.Size = 10
                param_dateupd.Value = DBNull.Value
                param_dateupd.Direction = ParameterDirection.InputOutput

                Dim param_idproveedor As New SqlClient.SqlParameter
                param_idproveedor.ParameterName = "@idproveedor"
                param_idproveedor.SqlDbType = SqlDbType.BigInt
                param_idproveedor.Value = DBNull.Value
                param_idproveedor.Direction = ParameterDirection.InputOutput

                Dim param_proveedor As New SqlClient.SqlParameter
                param_proveedor.ParameterName = "@proveedor"
                param_proveedor.SqlDbType = SqlDbType.VarChar
                param_proveedor.Size = 50
                param_proveedor.Value = DBNull.Value
                param_proveedor.Direction = ParameterDirection.InputOutput

                Dim param_idmoneda As New SqlClient.SqlParameter
                param_idmoneda.ParameterName = "@idmoneda"
                param_idmoneda.SqlDbType = SqlDbType.BigInt
                param_idmoneda.Value = DBNull.Value
                param_idmoneda.Direction = ParameterDirection.InputOutput

                Dim param_codigoMoneda As New SqlClient.SqlParameter
                param_codigoMoneda.ParameterName = "@CodMoneda"
                param_codigoMoneda.SqlDbType = SqlDbType.VarChar
                param_codigoMoneda.Size = 10
                param_codigoMoneda.Value = DBNull.Value
                param_codigoMoneda.Direction = ParameterDirection.InputOutput

                Dim param_nombreMoneda As New SqlClient.SqlParameter
                param_nombreMoneda.ParameterName = "@nombreMoneda"
                param_nombreMoneda.SqlDbType = SqlDbType.VarChar
                param_nombreMoneda.Size = 50
                param_nombreMoneda.Value = DBNull.Value
                param_nombreMoneda.Direction = ParameterDirection.InputOutput

                Dim param_valorcambio As New SqlClient.SqlParameter
                param_valorcambio.ParameterName = "@valorcambio"
                param_valorcambio.SqlDbType = SqlDbType.Decimal
                param_valorcambio.Precision = 18
                param_valorcambio.Scale = 2
                param_valorcambio.Value = DBNull.Value
                param_valorcambio.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Select_By_Codigo", param_codigo, _
                                              param_id, param_nombre, param_idunidad, param_codunidad, param_unidad, param_qtystock, _
                                              param_minimo, param_maximo, param_precio, param_ganancia, param_preciovta, _
                                              param_preciovtaorig, param_gananciaorig, param_dateupd, _
                                              param_idproveedor, param_proveedor, param_idmoneda, param_codigoMoneda, _
                                              param_nombreMoneda, param_valorcambio, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodigoUnidad = param_codunidad.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If
                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_ganancia.Value Is DBNull.Value Then
                                Ganancia = param_ganancia.Value
                            End If
                            If Not param_preciovta.Value Is DBNull.Value Then
                                PrecioVta = param_preciovta.Value
                            End If
                            If Not param_preciovtaorig.Value Is DBNull.Value Then
                                PrecioVtaOrig = param_preciovtaorig.Value
                            End If
                            If Not param_gananciaorig.Value Is DBNull.Value Then
                                GananciaOrig = param_gananciaorig.Value
                            End If
                            If Not param_dateupd.Value Is DBNull.Value Then
                                DateUpd = param_dateupd.Value
                            End If
                            If Not param_idproveedor.Value Is DBNull.Value Then
                                IdProveedor = param_idproveedor.Value
                            End If
                            If Not param_proveedor.Value Is DBNull.Value Then
                                Proveedor = param_proveedor.Value
                            End If
                            If Not param_idmoneda.Value Is DBNull.Value Then
                                idmoneda = param_idmoneda.Value
                            End If
                            If Not param_codigoMoneda.Value Is DBNull.Value Then
                                codigoMoneda = param_codigoMoneda.Value
                            End If
                            If Not param_nombreMoneda.Value Is DBNull.Value Then
                                nombreMoneda = param_nombreMoneda.Value
                            End If
                            If Not param_valorcambio.Value Is DBNull.Value Then
                                valorcambio = param_valorcambio.Value
                            End If

                        End If
                    End If

                    ObtenerMaterial_MOTO_HOLLMAN = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

#Region "SEI"

    Public Sub BuscarMoneda(ByVal Moneda As String, ByRef txtcodMonedaOC As TextBoxConFormatoVB.FormattedTextBoxVB, ByRef ValorCambioDO As Decimal)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select codigo, valorcambio from Monedas where nombre = '" & Moneda & "'")

            ds.Dispose()

            'txtCodMonedaOC.Text = ds.Tables(0).Rows(0).Item(0).ToString
            'MsgBox(ds.Tables(0).Rows(0).Item(0).ToString)
            txtcodMonedaOC.Text = ds.Tables(0).Rows(0).Item(0).ToString

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select Valorcambio from Monedas where nombre = 'DOLAR'")

            'ds.Dispose()

            ValorCambioDO = CDbl(ds.Tables(0).Rows(0).Item(1))

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

    End Sub

#End Region

#Region "Camelias"

    Public Function ObtenerMaterial_Camelias(ByVal Codigo As String, _
                                          ByRef Codigo_Mat_Prov As String, _
                                          ByRef ID As Long, _
                                          ByRef nombre As String, _
                                          ByRef IdUnidad As Long, _
                                          ByRef CodigoUnidad As String, _
                                          ByRef NombreUnidad As String, _
                                          ByRef qtystock As Decimal, _
                                          ByRef minimo As Decimal, _
                                          ByRef maximo As Decimal, _
                                          ByRef Precio As Decimal, _
                                          ByRef PreciosinIVA As Decimal, _
                                          ByRef Iva As Decimal, _
                                          ByRef MontoIva As Decimal, _
                                          ByRef PrecioOrig As Decimal, _
                                          ByRef ConsumoDiario As Boolean, _
                                          ByRef IdFranquicia As Long, _
                                          ByRef MateriaPrima As Boolean, _
                                          ByRef DateUpd As String, _
                                          ByVal Conexion As String, _
                                          Optional ByRef CodBarra As String = ""
                                      ) As Integer

        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(Conexion)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = Codigo
                param_codigo.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 300
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.InputOutput

                Dim param_codunidad As New SqlClient.SqlParameter
                param_codunidad.ParameterName = "@codigounidad"
                param_codunidad.SqlDbType = SqlDbType.VarChar
                param_codunidad.Size = 50
                param_codunidad.Value = DBNull.Value
                param_codunidad.Direction = ParameterDirection.InputOutput

                Dim param_unidad As New SqlClient.SqlParameter
                param_unidad.ParameterName = "@nombreunidad"
                param_unidad.SqlDbType = SqlDbType.VarChar
                param_unidad.Size = 50
                param_unidad.Value = DBNull.Value
                param_unidad.Direction = ParameterDirection.InputOutput

                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 2
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 2
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 2
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@precio"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 2
                param_precio.Value = DBNull.Value
                param_precio.Direction = ParameterDirection.InputOutput

                Dim param_precioSINIVA As New SqlClient.SqlParameter
                param_precioSINIVA.ParameterName = "@precioSINIVA"
                param_precioSINIVA.SqlDbType = SqlDbType.Decimal
                param_precioSINIVA.Precision = 18
                param_precioSINIVA.Scale = 2
                param_precioSINIVA.Value = DBNull.Value
                param_precioSINIVA.Direction = ParameterDirection.InputOutput

                Dim param_precioorig As New SqlClient.SqlParameter
                param_precioorig.ParameterName = "@precioorig"
                param_precioorig.SqlDbType = SqlDbType.Decimal
                param_precioorig.Precision = 18
                param_precioorig.Scale = 2
                param_precioorig.Value = DBNull.Value
                param_precioorig.Direction = ParameterDirection.InputOutput

                Dim param_iva As New SqlClient.SqlParameter
                param_iva.ParameterName = "@Iva"
                param_iva.SqlDbType = SqlDbType.Decimal
                param_iva.Precision = 18
                param_iva.Scale = 2
                param_iva.Value = Iva
                param_iva.Direction = ParameterDirection.InputOutput

                Dim param_montoiva As New SqlClient.SqlParameter
                param_montoiva.ParameterName = "@montoiva"
                param_montoiva.SqlDbType = SqlDbType.Decimal
                param_montoiva.Precision = 18
                param_montoiva.Scale = 2
                param_montoiva.Value = DBNull.Value
                param_montoiva.Direction = ParameterDirection.InputOutput

                Dim param_ConsumoDiario As New SqlClient.SqlParameter
                param_ConsumoDiario.ParameterName = "@ConsumoDiario"
                param_ConsumoDiario.SqlDbType = SqlDbType.Bit
                param_ConsumoDiario.Value = ConsumoDiario
                param_ConsumoDiario.Direction = ParameterDirection.Input

                Dim param_IdFranquicia As New SqlClient.SqlParameter
                param_IdFranquicia.ParameterName = "@IdFranquicia"
                param_IdFranquicia.SqlDbType = SqlDbType.BigInt
                param_IdFranquicia.Value = IdFranquicia
                param_IdFranquicia.Direction = ParameterDirection.Input

                Dim param_MateriaPrima As New SqlClient.SqlParameter
                param_MateriaPrima.ParameterName = "@MateriaPrima"
                param_MateriaPrima.SqlDbType = SqlDbType.BigInt
                param_MateriaPrima.Value = MateriaPrima
                param_MateriaPrima.Direction = ParameterDirection.Input

                Dim param_dateupd As New SqlClient.SqlParameter
                param_dateupd.ParameterName = "@dateupd"
                param_dateupd.SqlDbType = SqlDbType.VarChar
                param_dateupd.Size = 10
                param_dateupd.Value = DBNull.Value
                param_dateupd.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMateriales_Select_By_Codigo_Camelias", param_codigo, _
                                              param_id, param_nombre, param_idunidad, param_codunidad, param_unidad, param_qtystock, _
                                              param_minimo, param_maximo, param_precio, param_precioSINIVA, param_precioorig, param_iva, _
                                              param_montoiva, param_ConsumoDiario, param_IdFranquicia, _
                                              param_MateriaPrima, param_dateupd, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 0 Then
                            If Not param_id.Value Is DBNull.Value Then
                                ID = param_id.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                IdUnidad = param_idunidad.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_codunidad.Value Is DBNull.Value Then
                                CodigoUnidad = param_codunidad.Value
                            End If
                            If Not param_unidad.Value Is DBNull.Value Then
                                NombreUnidad = param_unidad.Value
                            End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If
                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If
                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                            If Not param_precioorig.Value Is DBNull.Value Then
                                PrecioOrig = param_precioorig.Value
                            End If
                            If Not param_iva.Value Is DBNull.Value Then
                                Iva = param_iva.Value
                            End If
                            If Not param_montoiva.Value Is DBNull.Value Then
                                MontoIva = param_montoiva.Value
                            End If
                            If Not param_precioSINIVA.Value Is DBNull.Value Then
                                PreciosinIVA = param_precioSINIVA.Value
                            End If
                            If Not param_dateupd.Value Is DBNull.Value Then
                                DateUpd = param_dateupd.Value
                            End If

                        End If
                    End If

                    ObtenerMaterial_Camelias = res

                Catch ex As Exception
                    Throw ex
                End Try
            Finally

            End Try
        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            While (Not tempException Is Nothing)
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
                tempException = tempException.InnerException
            End While

            MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "Si el problema persiste contáctese con Kaizen Software Factory a través del correo soporte@kaizensoftware.com.ar", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

#End Region

End Module
