Imports System.Web
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms


Public Module Util

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'ESTRUCTURAS                                                                                              '      
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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



    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'VARIABLES GLOBALES                                                                                       '      
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public path_raiz As String

    Public ConnStringUSUARIOS As String                                  ' String de conexion a TM3
    Public ConnStringICYS As String                            ' String de conexion a Seguridad
    Public ConnStringPERFO As String                                   ' String de conexion a RN
    Public ConnStringACER As String                                   ' String de conexion a RN
    Public ConnStringEMI As String                                   ' String de conexion a RN
    Public ConnStringROTI As String                                   ' String de conexion a RN
    Public ConnStringFINANCIERA As String                                   ' String de conexion a RN


    Public cerroparametrosconaceptar As Boolean = False             ' Variable Global utilizada para saber si el frmParametros se cerro del boton cerrar

    Public cantparametrosmasboton As Integer = 1                    'PARA CONTROLAR LA CANTIDAD DE PARAMETROS EN frmParametros

    Public nbreformreportes As String                               'VARIABLE PARA PSARLE EL NOMBRE DEL FORMULARIO AL REPROTE

    Public pathrpt As String    'Usada para almacenar la ruta de los reportes sacada del ini
    Public pathinis As String   'Usarda para almacenar la ruta de los inis


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

    Public Logueado_OK As Boolean
    Public UserActual As String

    Public EquipoActual As String ' guarda el nombre del equipo

    Public codigo_sector As String ' ejemplo: "FLEJADORA"
    Public descripcion_sector As String ' ejemplo: "SECTOR FLEJADORA DE TUBHIER"
    Public codigo_sector_anterior As String ' ejemplo: "ENTRYLINE"
    Public muestra_listado As Boolean
    Public id_sector As Long ' ejemplo: "1"
    Public id_sectoranterior As Long ' ejemplo: "5"


    Public UserID As Long

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

    Property Archivo() As String
        Get
            Archivo = m_Ini
        End Get
        Set(ByVal value As String)
            m_Ini = value
        End Set
    End Property

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
        '--------------------------------------------------------------------------
        ' Lee una sección entera de un fichero INI                      (27/Feb/99)
        ' Adaptada para devolver un array de string                     (04/Abr/01)
        '
        ' Esta función devolverá un array de índice cero
        ' con las claves y valores de la sección
        '
        ' Parámetros de entrada:
        '   sFileName   Nombre del fichero INI
        '   sSection    Nombre de la sección a leer
        ' Devuelve:
        '   Un array con el nombre de la clave y el valor
        '   Para leer los datos:
        '       For i = 0 To UBound(elArray) -1 Step 2
        '           sClave = elArray(i)
        '           sValor = elArray(i+1)
        '       Next
        '
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

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'MANEJO DE LAS CONEXIONES                                                                                       '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '---------------------------------------------------------------------------------------
    ' Procedure : Clave_Segun_Server
    ' DateTime  : 19/05/2008 10:38
    ' Author    : cponte
    ' Purpose   : Configura la clave de la base de datos
    '             segun la variable global "ambito"
    '---------------------------------------------------------------------------------------
    '
    Private Function Clave_Segun_Server(Optional ByVal server As String = "") As String
        Clave_Segun_Server = ""
        Dim ambito As String
        If Trim$(server) <> "" Then
            Select Case UCase(Trim$(server))
                Case "NBTAAL"
                    Clave_Segun_Server = "industrial"
                Case "SERVER"
                    Clave_Segun_Server = "industrial"
                Case "DESARROLLO05"
                    Clave_Segun_Server = "avestruz"
            End Select

            'Clave_Segun_Server = "industrial"
        Else
            ambito = LeeIni("varios", "ambito")

            'Select Case UCase(ambito)
            '    Case "TUBHIER"
            '        Clave_Segun_Server = "pelicano"
            '    Case "FORMAR"
            '        Clave_Segun_Server = "pelicano"
            '    Case "FORMARPRUEBA"
            '        Clave_Segun_Server = "pelicano"
            '    Case "CPONTE"
            '        Clave_Segun_Server = "avestruz"
            '    Case "DARIO"
            '        Clave_Segun_Server = "avestruz"
            '    Case "MARIANO"
            '        Clave_Segun_Server = "avestruz"
            '    Case "SEQUELDR"
            '        Clave_Segun_Server = "pelicano"
            '    Case "BLP"
            '        Clave_Segun_Server = "pelicano"
            'End Select
        End If
    End Function

    '---------------------------------------------------------------------------------------
    ' Procedure : ConfigurarCadenaConexion
    ' DateTime  : 01/10/2006 08:42
    ' Author    : cponte
    ' Purpose   : Configurar las cadenas de conexion y variables de acuerdo
    '             a donde esté parado.
    '---------------------------------------------------------------------------------------
    '
    Public Sub ConfigurarCadenaConexion()

        Dim aux() As String
        Dim temp As String

        'PATH DE LOS REPORTES QUE SE ENCUENTRA EN EL INI DEL USUARIO

        '' CADENA DE CONEXION A TM3
        aux = IniGetSection(Archivo, "Conexion_Usuarios")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Usuarios", "Server")
            ConnStringUSUARIOS = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringUSUARIOS = ConnStringUSUARIOS & LeeIni("Conexion_Usuarios", "Origen")
            ConnStringUSUARIOS = ConnStringUSUARIOS & ";Data Source="
            ConnStringUSUARIOS = ConnStringUSUARIOS & temp
            TipoConexionUSUARIOS.server = temp
            TipoConexionUSUARIOS.base = LeeIni("Conexion_Usuarios", "Origen")
            TipoConexionUSUARIOS.usuario = "sa"
            TipoConexionUSUARIOS.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringUSUARIOS = ""
        End If

        '' CADENA DE CONEXION A RN
        aux = IniGetSection(Archivo, "Conexion_PERFO")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_PERFO", "Server")
            ConnStringPERFO = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringPERFO = ConnStringPERFO & LeeIni("Conexion_PERFO", "Origen")
            ConnStringPERFO = ConnStringPERFO & ";Data Source="
            ConnStringPERFO = ConnStringPERFO & temp

            TipoConexionPERFO.server = temp
            TipoConexionPERFO.base = LeeIni("Conexion_PERFO", "Origen")
            TipoConexionPERFO.usuario = "sa"
            TipoConexionPERFO.contrasena = Clave_Segun_Server(temp)
        Else
            ConnStringPERFO = ""
        End If

        '' CADENA DE CONEXION A PARADAS
        aux = IniGetSection(Archivo, "Conexion_ICYS")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_ICYS", "Server")
            ConnStringICYS = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringICYS = ConnStringICYS & LeeIni("Conexion_ICYS", "Origen")
            ConnStringICYS = ConnStringICYS & ";Data Source="
            ConnStringICYS = ConnStringICYS & temp

            TipoConexionICYS.server = temp
            TipoConexionICYS.base = LeeIni("Conexion_ICYS", "Origen")
            TipoConexionICYS.usuario = "sa"
            TipoConexionICYS.contrasena = Clave_Segun_Server(temp)
        Else
            ConnStringICYS = ""
        End If

        '' CADENA DE CONEXION A TM3
        aux = IniGetSection(Archivo, "Conexion_EMI")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Usuarios", "Server")
            ConnStringEMI = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringEMI = ConnStringEMI & LeeIni("Conexion_EMI", "Origen")
            ConnStringEMI = ConnStringEMI & ";Data Source="
            ConnStringEMI = ConnStringEMI & temp
            TipoConexionEMI.server = temp
            TipoConexionEMI.base = LeeIni("Conexion_EMI", "Origen")
            TipoConexionEMI.usuario = "sa"
            TipoConexionEMI.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringUSUARIOS = ""
        End If

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
        aux = IniGetSection(Archivo, "Conexion_Rotiseria")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Rotiseria", "Server")
            ConnStringROTI = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringROTI = ConnStringROTI & LeeIni("Conexion_Rotiseria", "Origen")
            ConnStringROTI = ConnStringROTI & ";Data Source="
            ConnStringROTI = ConnStringROTI & temp
            TipoConexionROTI.server = temp
            TipoConexionROTI.base = LeeIni("Conexion_Rotiseria", "Origen")
            TipoConexionROTI.usuario = "sa"
            TipoConexionROTI.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringROTI = ""
        End If

        '' CADENA DE CONEXION A TM3
        aux = IniGetSection(Archivo, "Conexion_Financiera")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_Financiera", "Server")
            ConnStringFINANCIERA = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringFINANCIERA = ConnStringFINANCIERA & LeeIni("Conexion_Financiera", "Origen")
            ConnStringFINANCIERA = ConnStringFINANCIERA & ";Data Source="
            ConnStringFINANCIERA = ConnStringFINANCIERA & temp
            TipoConexionFinanciera.server = temp
            TipoConexionFinanciera.base = LeeIni("Conexion_Financiera", "Origen")
            TipoConexionFinanciera.usuario = "sa"
            TipoConexionFinanciera.contrasena = Clave_Segun_Server(temp)

        Else
            ConnStringFINANCIERA = ""
        End If

    End Sub



    '---------------------------------------------------------------------------------------
    ' Procedure : Inicia_Conexion
    ' DateTime  : 01/10/2007 08:44
    ' Author    : cponte
    ' Purpose   : Configurar las conexiones de acuerdo al ambito elegido
    '             dependiendo de la base de datos que se quiera usar.
    '---------------------------------------------------------------------------------------
    '
    Public Sub Inicia_Conexion()
        ConfigurarCadenaConexion()
    End Sub

    Public Sub ObtenerNombrePC()
        sComputerName = SystemInformation.ComputerName.ToString
    End Sub

    ''NUEVO MS 23-06-2010
    ''STORE PROCEDRE QUE DEVUELVE UN DATASET CON LOS DATOS PARA EL SELECT PASADO EN LA CONSULTA PARA COMBO DE PARAMETROS
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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spGetPermisos2", param_iduser, param_form, param_alta, param_modifica, param_baja, param_bajafisica, param_deshacer)
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
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


    '****************************************************************************
    '* BUSCA UN ELEMENTO EN UN COMBO QUE FUE LLENADO CON UN DATASOURCE
    '* Parámetros
    '*    cmb         objeto combobox
    '*    txtToFind   texto a encontrar
    '*    colPos      posicion de la columa a comparar en el datasource
    '****************************************************************************
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
                    If oControl.Tag <> "" Then
                        oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Try
                            'SI PUEDO (PORQUE ES DEL TIPO TEXTBOXCONFORMATO) GUARDO EL VALOR ANTERIOR
                            oControl.Text_1 = oControl.Text
                        Catch ex As Exception
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DateTimePicker Or TypeOf (oControl) Is Label Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is CheckBox Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, False, g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is System.Windows.Forms.RadioButton Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, False, g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
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
                            End Try
                          
                        Else
                            oControl.Text = ""
                            Try
                                oControl.selectedindex = -1
                            Catch e As Exception
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
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.CheckBoxX Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception
                        End Try
                    End If
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.ComboBoxEx Then
                    If oControl.Tag <> "" Then
                        Try
                            oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                        Catch ex As Exception

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
        For Each oControl As Object In oVControls
            Try
                If TypeOf (oControl) Is TextBox Then
                    oControl.Text = ""
                End If
                If TypeOf (oControl) Is ComboBox Then
                    oControl.Text = ""
                    Try
                        oControl.selectedindex = -1
                    Catch ex2 As Exception
                    End Try
                End If
                If TypeOf (oControl) Is DateTimePicker Then
                    oControl.text = ""
                End If

                If TypeOf (oControl) Is System.Windows.Forms.RadioButton Then
                    oControl.checked = False
                End If

                If TypeOf (oControl) Is DevComponents.Editors.DateTimeAdv.DateTimeInput Then
                    oControl.text = ""
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.TextBoxX Then
                    oControl.text = ""
                End If

                If TypeOf (oControl) Is DevComponents.DotNetBar.Controls.CheckBoxX Then
                    oControl.CHECKED = False
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

            Catch ex As Exception
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
                                            ByRef OK As Boolean, _
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
                param_codigo.ParameterName = "@codigo"
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

                Dim param_pass As New SqlClient.SqlParameter
                param_pass.ParameterName = "@pass"
                param_pass.SqlDbType = SqlDbType.VarChar
                param_pass.Size = 50
                param_pass.Value = pass
                param_pass.Direction = ParameterDirection.Input

                Dim param_ok As New SqlClient.SqlParameter
                param_ok.ParameterName = "@ok"
                param_ok.SqlDbType = SqlDbType.Bit
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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_UsuariosComprobar2", param_id, param_codigo, param_nombre, param_pass, param_ok, param_vencida, param_repetida)

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    ' Permite obtener datos generales de la tabla MateriaPrima
    Public Function cambiarContrasena(ByRef ID As Long, _
                                            ByVal pass As String _
                                            ) As Boolean

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_UsuariosCambiarContrasena", param_id, param_pass, param_res)


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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function


    ' Permite obtener datos generales de la tabla MateriaPrima
    Public Function MateriaPrimaPorIdGeneral(ByVal Id As Long, _
                                            Optional ByRef codigo As String = "", _
                                            Optional ByRef nombre As String = "", _
                                            Optional ByRef um As String = "", _
                                            Optional ByRef tipomp As String = "", _
                                            Optional ByRef espesor As Decimal = 0, _
                                            Optional ByRef ancho As Decimal = 0, _
                                            Optional ByRef calidad As String = "", _
                                            Optional ByRef un As String = "", _
                                            Optional ByRef codigodiseno As String = "") As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                ''COMENTADO MS 30-06-2010
                ''connection = SqlHelper.GetConnection(ConnStringRRHH)
                ''FIN COMENTADO
                ''NUEVO MS 30-06-2010
                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                ''FIN NUEVO
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try


                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = Id 'CType(txtID.Text, Long)
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 15
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 100
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_um As New SqlClient.SqlParameter
                param_um.ParameterName = "@um"
                param_um.SqlDbType = SqlDbType.VarChar
                param_um.Size = 3
                param_um.Value = DBNull.Value
                param_um.Direction = ParameterDirection.InputOutput

                Dim param_tipomp As New SqlClient.SqlParameter
                param_tipomp.ParameterName = "@tipomp"
                param_tipomp.SqlDbType = SqlDbType.VarChar
                param_tipomp.Size = 10
                param_tipomp.Value = DBNull.Value
                param_tipomp.Direction = ParameterDirection.InputOutput

                Dim param_espesor As New SqlClient.SqlParameter
                param_espesor.ParameterName = "@espesor"
                param_espesor.SqlDbType = SqlDbType.Decimal
                param_espesor.Precision = 18
                param_espesor.Scale = 4
                param_espesor.Value = DBNull.Value
                param_espesor.Direction = ParameterDirection.InputOutput

                Dim param_ancho As New SqlClient.SqlParameter
                param_ancho.ParameterName = "@ancho"
                param_ancho.SqlDbType = SqlDbType.Decimal
                param_ancho.Precision = 18
                param_ancho.Scale = 4
                param_ancho.Value = DBNull.Value
                param_ancho.Direction = ParameterDirection.InputOutput

                Dim param_calidad As New SqlClient.SqlParameter
                param_calidad.ParameterName = "@calidad"
                param_calidad.SqlDbType = SqlDbType.VarChar
                param_calidad.Size = 15
                param_calidad.Value = DBNull.Value
                param_calidad.Direction = ParameterDirection.InputOutput

                Dim param_un As New SqlClient.SqlParameter
                param_un.ParameterName = "@un"
                param_un.SqlDbType = SqlDbType.VarChar
                param_un.Size = 15
                param_un.Value = DBNull.Value
                param_un.Direction = ParameterDirection.InputOutput

                Dim param_codigodiseno As New SqlClient.SqlParameter
                param_codigodiseno.ParameterName = "@codigodiseno"
                param_codigodiseno.SqlDbType = SqlDbType.VarChar
                param_codigodiseno.Size = 25
                param_codigodiseno.Value = DBNull.Value
                param_codigodiseno.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_MateriaPrima_Select_By_ID_General", param_id, param_codigo, param_nombre, param_um, param_tipomp, param_espesor, param_ancho, param_calidad, param_un, param_codigodiseno, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    End If
                    If Not param_codigo.Value Is DBNull.Value Then
                        codigo = param_codigo.Value
                    End If
                    If Not param_nombre.Value Is DBNull.Value Then
                        nombre = param_nombre.Value
                    End If
                    If Not param_um.Value Is DBNull.Value Then
                        um = param_um.Value
                    End If
                    If Not param_tipomp.Value Is DBNull.Value Then
                        tipomp = param_tipomp.Value
                    End If
                    If Not param_espesor.Value Is DBNull.Value Then
                        espesor = param_espesor.Value
                    End If
                    If Not param_ancho.Value Is DBNull.Value Then
                        ancho = param_ancho.Value
                    End If
                    If Not param_calidad.Value Is DBNull.Value Then
                        calidad = param_calidad.Value
                    End If
                    If Not param_un.Value Is DBNull.Value Then
                        un = param_un.Value
                    End If
                    If Not param_codigodiseno.Value Is DBNull.Value Then
                        codigodiseno = param_codigodiseno.Value
                    End If


                    MateriaPrimaPorIdGeneral = res


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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    ' Permite obtener datos generales de la tabla Disenos
    Public Function DisenosPorIdGeneral(ByVal Id As Long, _
                                            Optional ByRef codigo As String = "", _
                                            Optional ByRef nombre As String = "", _
                                            Optional ByRef diametro As Decimal = 0, _
                                            Optional ByRef espesor As Decimal = 0, _
                                            Optional ByRef tolespmin As Decimal = 0, _
                                            Optional ByRef tolespmax As Decimal = 0, _
                                            Optional ByRef norma As String = "", _
                                            Optional ByRef activo As Boolean = 0, _
                                            Optional ByRef tipoproducto As String = "", _
                                            Optional ByRef desarrollo As Decimal = 0, _
                                            Optional ByRef peso As Integer = 0, _
                                            Optional ByRef tolpesomin As Decimal = 0, _
                                            Optional ByRef tolpesomax As Decimal = 0, _
                                            Optional ByRef largo As Decimal = 0, _
                                            Optional ByRef largomin As Decimal = 0, _
                                            Optional ByRef largomax As Decimal = 0, _
                                            Optional ByRef velocidad As Decimal = 0, _
                                            Optional ByRef alturapaquete As Integer = 0, _
                                            Optional ByRef piezaspaquete As Integer = 0, _
                                            Optional ByRef hilerainf As Integer = 0, _
                                            Optional ByRef hileracentral As Integer = 0, _
                                            Optional ByRef hilerasuperior As Integer = 0, _
                                            Optional ByRef formato As String = "", _
                                            Optional ByRef tipo As String = "") As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                ''COMENTADO MS 30-06-2010
                ''connection = SqlHelper.GetConnection(ConnStringRRHH)
                ''FIN COMENTADO
                ''NUEVO MS 30-06-2010
                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                ''FIN NUEVO
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try


                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@Id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = Id
                param_id.Direction = ParameterDirection.Input

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@Codigo"
                param_codigo.SqlDbType = SqlDbType.VarChar
                param_codigo.Size = 25
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_nombre As New SqlClient.SqlParameter
                param_nombre.ParameterName = "@Nombre"
                param_nombre.SqlDbType = SqlDbType.VarChar
                param_nombre.Size = 150
                param_nombre.Value = DBNull.Value
                param_nombre.Direction = ParameterDirection.InputOutput

                Dim param_diametro As New SqlClient.SqlParameter
                param_diametro.ParameterName = "@Diametro"
                param_diametro.SqlDbType = SqlDbType.Decimal
                param_diametro.Precision = 18
                param_diametro.Scale = 4
                param_diametro.Value = DBNull.Value
                param_diametro.Direction = ParameterDirection.InputOutput

                Dim param_espesor As New SqlClient.SqlParameter
                param_espesor.ParameterName = "@Espesor"
                param_espesor.SqlDbType = SqlDbType.Decimal
                param_espesor.Precision = 18
                param_espesor.Scale = 4
                param_espesor.Value = DBNull.Value
                param_espesor.Direction = ParameterDirection.InputOutput

                Dim param_tolespmin As New SqlClient.SqlParameter
                param_tolespmin.ParameterName = "@ToleEspeMin"
                param_tolespmin.SqlDbType = SqlDbType.Decimal
                param_tolespmin.Precision = 18
                param_tolespmin.Scale = 4
                param_tolespmin.Value = DBNull.Value
                param_tolespmin.Direction = ParameterDirection.InputOutput

                Dim param_tolespmax As New SqlClient.SqlParameter
                param_tolespmax.ParameterName = "@ToleEspeMax"
                param_tolespmax.SqlDbType = SqlDbType.Decimal
                param_tolespmax.Precision = 18
                param_tolespmax.Scale = 4
                param_tolespmax.Value = DBNull.Value
                param_tolespmax.Direction = ParameterDirection.InputOutput

                Dim param_norma As New SqlClient.SqlParameter
                param_norma.ParameterName = "@Norma"
                param_norma.SqlDbType = SqlDbType.VarChar
                param_norma.Size = 50
                param_norma.Value = DBNull.Value
                param_norma.Direction = ParameterDirection.InputOutput

                Dim param_activo As New SqlClient.SqlParameter
                param_activo.ParameterName = "@Activo"
                param_activo.SqlDbType = SqlDbType.Bit
                param_activo.Value = DBNull.Value
                param_activo.Direction = ParameterDirection.InputOutput

                Dim param_tipoproducto As New SqlClient.SqlParameter
                param_tipoproducto.ParameterName = "@TipoProducto"
                param_tipoproducto.SqlDbType = SqlDbType.VarChar
                param_tipoproducto.Size = 20
                param_tipoproducto.Value = DBNull.Value
                param_tipoproducto.Direction = ParameterDirection.InputOutput

                Dim param_desarrollo As New SqlClient.SqlParameter
                param_desarrollo.ParameterName = "@Desarrollo"
                param_desarrollo.SqlDbType = SqlDbType.Decimal
                param_desarrollo.Precision = 18
                param_desarrollo.Scale = 4
                param_desarrollo.Value = DBNull.Value
                param_desarrollo.Direction = ParameterDirection.InputOutput

                Dim param_peso As New SqlClient.SqlParameter
                param_peso.ParameterName = "@Peso"
                param_peso.SqlDbType = SqlDbType.Int
                param_peso.Value = DBNull.Value
                param_peso.Direction = ParameterDirection.InputOutput

                Dim param_toleranciapesomax As New SqlClient.SqlParameter
                param_toleranciapesomax.ParameterName = "@ToleranciaPesoMax"
                param_toleranciapesomax.SqlDbType = SqlDbType.Decimal
                param_toleranciapesomax.Precision = 18
                param_toleranciapesomax.Scale = 4
                param_toleranciapesomax.Value = DBNull.Value
                param_toleranciapesomax.Direction = ParameterDirection.InputOutput

                Dim param_toleranciapesomin As New SqlClient.SqlParameter
                param_toleranciapesomin.ParameterName = "@ToleranciaPesoMin"
                param_toleranciapesomin.SqlDbType = SqlDbType.Decimal
                param_toleranciapesomin.Precision = 18
                param_toleranciapesomin.Scale = 4
                param_toleranciapesomin.Value = DBNull.Value
                param_toleranciapesomin.Direction = ParameterDirection.InputOutput

                Dim param_largo As New SqlClient.SqlParameter
                param_largo.ParameterName = "@Largo"
                param_largo.SqlDbType = SqlDbType.Decimal
                param_largo.Precision = 18
                param_largo.Scale = 4
                param_largo.Value = DBNull.Value
                param_largo.Direction = ParameterDirection.InputOutput

                Dim param_largomin As New SqlClient.SqlParameter
                param_largomin.ParameterName = "@LargoMin"
                param_largomin.SqlDbType = SqlDbType.Decimal
                param_largomin.Precision = 18
                param_largomin.Scale = 4
                param_largomin.Value = DBNull.Value
                param_largomin.Direction = ParameterDirection.InputOutput

                Dim param_largomax As New SqlClient.SqlParameter
                param_largomax.ParameterName = "@LargoMax"
                param_largomax.SqlDbType = SqlDbType.Decimal
                param_largomax.Precision = 18
                param_largomax.Scale = 4
                param_largomax.Value = DBNull.Value
                param_largomax.Direction = ParameterDirection.InputOutput

                Dim param_velocidad As New SqlClient.SqlParameter
                param_velocidad.ParameterName = "@Velocidad"
                param_velocidad.SqlDbType = SqlDbType.Decimal
                param_velocidad.Precision = 18
                param_velocidad.Scale = 4
                param_velocidad.Value = DBNull.Value
                param_velocidad.Direction = ParameterDirection.InputOutput

                Dim param_alturapaquete As New SqlClient.SqlParameter
                param_alturapaquete.ParameterName = "@AlturaPaquete"
                param_alturapaquete.SqlDbType = SqlDbType.Int
                param_alturapaquete.Value = DBNull.Value
                param_alturapaquete.Direction = ParameterDirection.InputOutput

                Dim param_piezaspaquete As New SqlClient.SqlParameter
                param_piezaspaquete.ParameterName = "@PiezasPaquete"
                param_piezaspaquete.SqlDbType = SqlDbType.Int
                param_piezaspaquete.Value = DBNull.Value
                param_piezaspaquete.Direction = ParameterDirection.InputOutput

                Dim param_hilerainferior As New SqlClient.SqlParameter
                param_hilerainferior.ParameterName = "@HileraInf"
                param_hilerainferior.SqlDbType = SqlDbType.Int
                param_hilerainferior.Value = DBNull.Value
                param_hilerainferior.Direction = ParameterDirection.InputOutput

                Dim param_hileracentral As New SqlClient.SqlParameter
                param_hileracentral.ParameterName = "@HileraCentral"
                param_hileracentral.SqlDbType = SqlDbType.Int
                param_hileracentral.Value = DBNull.Value
                param_hileracentral.Direction = ParameterDirection.InputOutput

                Dim param_hilerasuperior As New SqlClient.SqlParameter
                param_hilerasuperior.ParameterName = "@HileraSup"
                param_hilerasuperior.SqlDbType = SqlDbType.Int
                param_hilerasuperior.Value = DBNull.Value
                param_hilerasuperior.Direction = ParameterDirection.InputOutput

                Dim param_formato As New SqlClient.SqlParameter
                param_formato.ParameterName = "@Formato"
                param_formato.SqlDbType = SqlDbType.VarChar
                param_formato.Size = 20
                param_formato.Value = DBNull.Value
                param_formato.Direction = ParameterDirection.InputOutput

                Dim param_tipo As New SqlClient.SqlParameter
                param_tipo.ParameterName = "@Tipo"
                param_tipo.SqlDbType = SqlDbType.VarChar
                param_tipo.Size = 10
                param_tipo.Value = DBNull.Value
                param_tipo.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Disenos_Select_By_ID_General", param_id, param_codigo, param_nombre, param_diametro, param_espesor, param_tolespmin, param_tolespmax, param_norma, param_activo, param_tipoproducto, param_desarrollo, param_peso, param_toleranciapesomax, param_toleranciapesomin, param_largo, param_largomin, param_largomax, param_velocidad, param_alturapaquete, param_piezaspaquete, param_hilerainferior, param_hileracentral, param_hilerasuperior, param_formato, param_tipo, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    End If
                    If Not param_codigo.Value Is DBNull.Value Then
                        codigo = param_codigo.Value
                    End If
                    If Not param_nombre.Value Is DBNull.Value Then
                        nombre = param_nombre.Value
                    End If
                    If Not param_diametro.Value Is DBNull.Value Then
                        diametro = param_diametro.Value
                    End If
                    If Not param_espesor.Value Is DBNull.Value Then
                        espesor = param_espesor.Value
                    End If
                    If Not param_tolespmin.Value Is DBNull.Value Then
                        tolespmin = param_tolespmin.Value
                    End If
                    If Not param_tolespmax.Value Is DBNull.Value Then
                        tolespmax = param_tolespmax.Value
                    End If
                    If Not param_norma.Value Is DBNull.Value Then
                        norma = param_norma.Value
                    End If
                    If Not param_activo.Value Is DBNull.Value Then
                        activo = param_activo.Value
                    End If
                    If Not param_tipoproducto.Value Is DBNull.Value Then
                        tipoproducto = param_tipoproducto.Value
                    End If
                    If Not param_desarrollo.Value Is DBNull.Value Then
                        desarrollo = param_desarrollo.Value
                    End If
                    If Not param_peso.Value Is DBNull.Value Then
                        peso = param_peso.Value
                    End If
                    If Not param_toleranciapesomax.Value Is DBNull.Value Then
                        tolpesomax = param_toleranciapesomax.Value
                    End If
                    If Not param_toleranciapesomin.Value Is DBNull.Value Then
                        tolpesomin = param_toleranciapesomin.Value
                    End If
                    If Not param_largo.Value Is DBNull.Value Then
                        largo = param_largo.Value
                    End If
                    If Not param_largomin.Value Is DBNull.Value Then
                        largomin = param_largomin.Value
                    End If
                    If Not param_largomax.Value Is DBNull.Value Then
                        largomax = param_largomax.Value
                    End If
                    If Not param_velocidad.Value Is DBNull.Value Then
                        velocidad = param_velocidad.Value
                    End If
                    If Not param_alturapaquete.Value Is DBNull.Value Then
                        alturapaquete = param_alturapaquete.Value
                    End If
                    If Not param_piezaspaquete.Value Is DBNull.Value Then
                        piezaspaquete = param_piezaspaquete.Value
                    End If
                    If Not param_hilerainferior.Value Is DBNull.Value Then
                        hilerainf = param_hilerainferior.Value
                    End If
                    If Not param_hileracentral.Value Is DBNull.Value Then
                        hileracentral = param_hileracentral.Value
                    End If
                    If Not param_hilerasuperior.Value Is DBNull.Value Then
                        hilerasuperior = param_hilerasuperior.Value
                    End If
                    If Not param_formato.Value Is DBNull.Value Then
                        formato = param_formato.Value
                    End If
                    If Not param_tipo.Value Is DBNull.Value Then
                        tipo = param_tipo.Value
                    End If

                    DisenosPorIdGeneral = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
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


    ''Permite saber si el dato pasado existe en el combo
    'Public Function ExisteDatoEnCombo(ByRef index As Int32, ByVal combo As ComboBox, ByVal cadena As String, ByVal campo As String) As Boolean

    '    For ni As Int32 = 0 To combo.Items.Count - 1
    '        Dim dr As DataRowView = CType(combo.Items.Item(ni), DataRowView)
    '        'If cadena.ToUpperInvariant = dr(campo).ToString.ToUpperInvariant Then
    '        If cadena.ToUpperInvariant = Mid(dr(campo).ToString.ToUpperInvariant, 1, Len(cadena.ToUpperInvariant)) Then
    '            ExisteDatoEnCombo = True
    '            index = ni
    '            Exit Function
    '        End If
    '    Next ni

    '    ExisteDatoEnCombo = False

    'End Function



    ' Permite obtener datos generales de la tabla bobinas a travez del id
    Public Function BobinaPorIdGeneral(ByVal Id As Long, _
                                            Optional ByRef Iddetalle As Long = 0, _
                                            Optional ByRef LoteProv As String = "", _
                                            Optional ByRef IdMateriaPrima As String = "", _
                                            Optional ByRef ancho As Decimal = 0, _
                                            Optional ByRef espesor As Decimal = 0, _
                                            Optional ByRef Peso As Decimal = 0, _
                                            Optional ByRef Colada As String = "", _
                                            Optional ByRef Calidad As String = "", _
                                            Optional ByRef OC As Long = 0, _
                                            Optional ByRef Stock As Boolean = False, _
                                            Optional ByRef NroCertificado As Long = 0, _
                                            Optional ByRef Observaciones As String = "", _
                                            Optional ByRef AnchoMedido As Decimal = 0, _
                                            Optional ByRef E As String = "", _
                                            Optional ByRef NroAlternativo As Long = 0, _
                                            Optional ByRef ubicacion As String = "", _
                                            Optional ByRef status As String = "", _
                                            Optional ByRef motivo As Long = 0) As Integer

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                ''COMENTADO MS 30-06-2010
                ''connection = SqlHelper.GetConnection(ConnStringRRHH)
                ''FIN COMENTADO
                ''NUEVO MS 30-06-2010
                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                ''FIN NUEVO
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

                Dim param_iddetalle As New SqlClient.SqlParameter
                param_iddetalle.ParameterName = "@IdDetalle"
                param_iddetalle.SqlDbType = SqlDbType.BigInt
                param_iddetalle.Value = DBNull.Value
                param_iddetalle.Direction = ParameterDirection.InputOutput

                Dim param_loteprov As New SqlClient.SqlParameter
                param_loteprov.ParameterName = "@LoteProv"
                param_loteprov.SqlDbType = SqlDbType.VarChar
                param_loteprov.Size = 30
                param_loteprov.Value = DBNull.Value
                param_loteprov.Direction = ParameterDirection.InputOutput

                Dim param_matpri As New SqlClient.SqlParameter
                param_matpri.ParameterName = "@IdMateriaPrima"
                param_matpri.SqlDbType = SqlDbType.VarChar
                param_matpri.Size = 15
                param_matpri.Value = DBNull.Value
                param_matpri.Direction = ParameterDirection.InputOutput

                Dim param_ancho As New SqlClient.SqlParameter
                param_ancho.ParameterName = "@Ancho"
                param_ancho.SqlDbType = SqlDbType.Decimal
                param_ancho.Precision = 18
                param_ancho.Scale = 4
                param_ancho.Value = DBNull.Value
                param_ancho.Direction = ParameterDirection.InputOutput

                Dim param_espesor As New SqlClient.SqlParameter
                param_espesor.ParameterName = "@Espesor"
                param_espesor.SqlDbType = SqlDbType.Decimal
                param_espesor.Precision = 18
                param_espesor.Scale = 4
                param_espesor.Value = DBNull.Value
                param_espesor.Direction = ParameterDirection.InputOutput

                Dim param_peso As New SqlClient.SqlParameter
                param_peso.ParameterName = "@Peso"
                param_peso.SqlDbType = SqlDbType.Decimal
                param_peso.Precision = 18
                param_peso.Scale = 4
                param_peso.Value = DBNull.Value
                param_peso.Direction = ParameterDirection.InputOutput

                Dim param_colada As New SqlClient.SqlParameter
                param_colada.ParameterName = "@Colada"
                param_colada.SqlDbType = SqlDbType.VarChar
                param_colada.Size = 20
                param_colada.Value = DBNull.Value
                param_colada.Direction = ParameterDirection.InputOutput

                Dim param_calidad As New SqlClient.SqlParameter
                param_calidad.ParameterName = "@Calidad"
                param_calidad.SqlDbType = SqlDbType.VarChar
                param_calidad.Size = 15
                param_calidad.Value = DBNull.Value
                param_calidad.Direction = ParameterDirection.InputOutput

                Dim param_oc As New SqlClient.SqlParameter
                param_oc.ParameterName = "@OC"
                param_oc.SqlDbType = SqlDbType.BigInt
                param_oc.Value = DBNull.Value
                param_oc.Direction = ParameterDirection.InputOutput

                Dim param_stock As New SqlClient.SqlParameter
                param_stock.ParameterName = "@Stock"
                param_stock.SqlDbType = SqlDbType.Bit
                param_stock.Value = DBNull.Value
                param_stock.Direction = ParameterDirection.InputOutput

                Dim param_numerocertificado As New SqlClient.SqlParameter
                param_numerocertificado.ParameterName = "@NroCertificado"
                param_numerocertificado.SqlDbType = SqlDbType.BigInt
                param_numerocertificado.Value = DBNull.Value
                param_numerocertificado.Direction = ParameterDirection.InputOutput

                Dim param_observ As New SqlClient.SqlParameter
                param_observ.ParameterName = "@Observaciones"
                param_observ.SqlDbType = SqlDbType.VarChar
                param_observ.Size = 150
                param_observ.Value = DBNull.Value
                param_observ.Direction = ParameterDirection.InputOutput

                Dim param_anchomedido As New SqlClient.SqlParameter
                param_anchomedido.ParameterName = "@AnchoMedido"
                param_anchomedido.SqlDbType = SqlDbType.Decimal
                param_anchomedido.Precision = 18
                param_anchomedido.Scale = 4
                param_anchomedido.Value = DBNull.Value
                param_anchomedido.Direction = ParameterDirection.InputOutput

                Dim param_e As New SqlClient.SqlParameter
                param_e.ParameterName = "@E"
                param_e.SqlDbType = SqlDbType.VarChar
                param_e.Size = 2
                param_e.Value = DBNull.Value
                param_e.Direction = ParameterDirection.InputOutput

                Dim param_alternativo As New SqlClient.SqlParameter
                param_alternativo.ParameterName = "@NroAlternativo"
                param_alternativo.SqlDbType = SqlDbType.BigInt
                param_alternativo.Value = DBNull.Value
                param_alternativo.Direction = ParameterDirection.InputOutput

                Dim param_ubicacion As New SqlClient.SqlParameter
                param_ubicacion.ParameterName = "@ubicacion"
                param_ubicacion.SqlDbType = SqlDbType.VarChar
                param_ubicacion.Size = 10
                param_ubicacion.Value = DBNull.Value
                param_ubicacion.Direction = ParameterDirection.InputOutput

                Dim param_statusqm As New SqlClient.SqlParameter
                param_statusqm.ParameterName = "@StatusQm"
                param_statusqm.SqlDbType = SqlDbType.VarChar
                param_statusqm.Size = 1
                param_statusqm.Value = DBNull.Value
                param_statusqm.Direction = ParameterDirection.InputOutput

                Dim param_idmotivo As New SqlClient.SqlParameter
                param_idmotivo.ParameterName = "@idmotivo"
                param_idmotivo.SqlDbType = SqlDbType.BigInt
                param_idmotivo.Value = DBNull.Value
                param_idmotivo.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Bobinas_Select_By_ID_General", param_id, param_iddetalle, param_loteprov, param_matpri, param_ancho, param_espesor, param_peso, param_colada, param_calidad, param_oc, param_stock, param_numerocertificado, param_observ, param_anchomedido, param_e, param_alternativo, param_ubicacion, param_statusqm, param_idmotivo, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    End If
                    If Not param_iddetalle.Value Is DBNull.Value Then
                        Iddetalle = param_iddetalle.Value
                    End If
                    If Not param_loteprov.Value Is DBNull.Value Then
                        LoteProv = param_loteprov.Value
                    End If
                    If Not param_matpri.Value Is DBNull.Value Then
                        IdMateriaPrima = param_matpri.Value
                    End If
                    If Not param_ancho.Value Is DBNull.Value Then
                        ancho = param_ancho.Value
                    End If
                    If Not param_espesor.Value Is DBNull.Value Then
                        espesor = param_espesor.Value
                    End If
                    If Not param_peso.Value Is DBNull.Value Then
                        Peso = param_peso.Value
                    End If
                    If Not param_colada.Value Is DBNull.Value Then
                        Colada = param_colada.Value
                    End If
                    If Not param_calidad.Value Is DBNull.Value Then
                        Calidad = param_calidad.Value
                    End If
                    If Not param_oc.Value Is DBNull.Value Then
                        OC = param_oc.Value
                    End If
                    If Not param_stock.Value Is DBNull.Value Then
                        Stock = param_stock.Value
                    End If
                    If Not param_numerocertificado.Value Is DBNull.Value Then
                        NroCertificado = param_numerocertificado.Value
                    End If
                    If Not param_observ.Value Is DBNull.Value Then
                        Observaciones = param_observ.Value
                    End If
                    If Not param_anchomedido.Value Is DBNull.Value Then
                        AnchoMedido = param_anchomedido.Value
                    End If
                    If Not param_e.Value Is DBNull.Value Then
                        E = param_e.Value
                    End If
                    If Not param_alternativo.Value Is DBNull.Value Then
                        NroAlternativo = param_alternativo.Value
                    End If
                    If Not param_ubicacion.Value Is DBNull.Value Then
                        ubicacion = param_ubicacion.Value
                    End If
                    If Not param_statusqm.Value Is DBNull.Value Then
                        status = param_statusqm.Value
                    End If
                    If Not param_idmotivo.Value Is DBNull.Value Then
                        motivo = param_idmotivo.Value
                    End If

                    BobinaPorIdGeneral = res


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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    ' Permite obtener datos generales de la tabla bobinas a travez del idDetalle (id del ingresodetalle)
    Public Function BobinaPorIdDetalleGeneral(ByVal IdDetalle As Long, _
                                            Optional ByRef Id As Long = 0, _
                                            Optional ByRef codigo As Long = 0, _
                                            Optional ByRef LoteProv As String = "", _
                                            Optional ByRef IdMateriaPrima As String = "", _
                                            Optional ByRef ancho As Decimal = 0, _
                                            Optional ByRef espesor As Decimal = 0, _
                                            Optional ByRef Peso As Decimal = 0, _
                                            Optional ByRef Colada As String = "", _
                                            Optional ByRef Calidad As String = "", _
                                            Optional ByRef OC As Long = 0, _
                                            Optional ByRef Stock As Boolean = False, _
                                            Optional ByRef NroCertificado As Long = 0, _
                                            Optional ByRef Cumplido As Boolean = False, _
                                            Optional ByRef Aprobado As Boolean = False, _
                                            Optional ByRef Observaciones As String = "", _
                                            Optional ByRef AnchoMedido As Decimal = 0, _
                                            Optional ByRef E As String = "", _
                                            Optional ByRef NroAlternativo As Long = 0) As Integer


        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim id_bobina, codigo_bobina, orden_corte, certificado, alternativo As Long
        'Dim lote, materia, colada, cali, observa, empresa As String
        'Dim ancho, espesor, peso, anchomedido As Decimal
        'Dim stock, cumplida, aprobada As Boolean

        'id_bobina = 0 : codigo_bobina = 0 : orden_corte = 0 : certificado = 0 : alternativo = 0
        'lote = "" : materia = "" : colada = "" : cali = "" : observa = "" : empresa = ""
        'ancho = 0 : espesor = 0 : peso = 0 : anchomedido = 0
        'stock = False : cumplida = False : aprobada = False
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                ''COMENTADO MS 30-06-2010
                ''connection = SqlHelper.GetConnection(ConnStringRRHH)
                ''FIN COMENTADO
                ''NUEVO MS 30-06-2010
                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                ''FIN NUEVO
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try

            Try

                Dim param_iddetalle As New SqlClient.SqlParameter
                param_iddetalle.ParameterName = "@IdDetalle"
                param_iddetalle.SqlDbType = SqlDbType.BigInt
                param_iddetalle.Value = IdDetalle
                param_iddetalle.Direction = ParameterDirection.Input

                Dim param_id As New SqlClient.SqlParameter
                param_id.ParameterName = "@id"
                param_id.SqlDbType = SqlDbType.BigInt
                param_id.Value = DBNull.Value
                param_id.Direction = ParameterDirection.InputOutput

                Dim param_codigo As New SqlClient.SqlParameter
                param_codigo.ParameterName = "@Codigo"
                param_codigo.SqlDbType = SqlDbType.BigInt
                param_codigo.Value = DBNull.Value
                param_codigo.Direction = ParameterDirection.InputOutput

                Dim param_loteprov As New SqlClient.SqlParameter
                param_loteprov.ParameterName = "@LoteProv"
                param_loteprov.SqlDbType = SqlDbType.VarChar
                param_loteprov.Size = 30
                param_loteprov.Value = DBNull.Value
                param_loteprov.Direction = ParameterDirection.InputOutput

                Dim param_matpri As New SqlClient.SqlParameter
                param_matpri.ParameterName = "@IdMateriaPrima"
                param_matpri.SqlDbType = SqlDbType.VarChar
                param_matpri.Size = 15
                param_matpri.Value = DBNull.Value
                param_matpri.Direction = ParameterDirection.InputOutput

                Dim param_ancho As New SqlClient.SqlParameter
                param_ancho.ParameterName = "@Ancho"
                param_ancho.SqlDbType = SqlDbType.Decimal
                param_ancho.Precision = 18
                param_ancho.Scale = 4
                param_ancho.Value = DBNull.Value
                param_ancho.Direction = ParameterDirection.InputOutput

                Dim param_espesor As New SqlClient.SqlParameter
                param_espesor.ParameterName = "@Espesor"
                param_espesor.SqlDbType = SqlDbType.Decimal
                param_espesor.Precision = 18
                param_espesor.Scale = 4
                param_espesor.Value = DBNull.Value
                param_espesor.Direction = ParameterDirection.InputOutput

                Dim param_peso As New SqlClient.SqlParameter
                param_peso.ParameterName = "@Peso"
                param_peso.SqlDbType = SqlDbType.Decimal
                param_peso.Precision = 18
                param_peso.Scale = 4
                param_peso.Value = DBNull.Value
                param_peso.Direction = ParameterDirection.InputOutput

                Dim param_colada As New SqlClient.SqlParameter
                param_colada.ParameterName = "@Colada"
                param_colada.SqlDbType = SqlDbType.VarChar
                param_colada.Size = 20
                param_colada.Value = DBNull.Value
                param_colada.Direction = ParameterDirection.InputOutput

                Dim param_calidad As New SqlClient.SqlParameter
                param_calidad.ParameterName = "@Calidad"
                param_calidad.SqlDbType = SqlDbType.VarChar
                param_calidad.Size = 15
                param_calidad.Value = DBNull.Value
                param_calidad.Direction = ParameterDirection.InputOutput

                Dim param_oc As New SqlClient.SqlParameter
                param_oc.ParameterName = "@OC"
                param_oc.SqlDbType = SqlDbType.BigInt
                param_oc.Value = DBNull.Value
                param_oc.Direction = ParameterDirection.InputOutput

                Dim param_stock As New SqlClient.SqlParameter
                param_stock.ParameterName = "@Stock"
                param_stock.SqlDbType = SqlDbType.Bit
                param_stock.Value = DBNull.Value
                param_stock.Direction = ParameterDirection.InputOutput

                Dim param_numerocertificado As New SqlClient.SqlParameter
                param_numerocertificado.ParameterName = "@NroCertificado"
                param_numerocertificado.SqlDbType = SqlDbType.BigInt
                param_numerocertificado.Value = DBNull.Value
                param_numerocertificado.Direction = ParameterDirection.InputOutput

                Dim param_cumpli As New SqlClient.SqlParameter
                param_cumpli.ParameterName = "@Cumplido"
                param_cumpli.SqlDbType = SqlDbType.Bit
                param_cumpli.Value = DBNull.Value
                param_cumpli.Direction = ParameterDirection.InputOutput

                Dim param_aprob As New SqlClient.SqlParameter
                param_aprob.ParameterName = "@Aprobado"
                param_aprob.SqlDbType = SqlDbType.Bit
                param_aprob.Value = DBNull.Value
                param_aprob.Direction = ParameterDirection.InputOutput

                Dim param_observ As New SqlClient.SqlParameter
                param_observ.ParameterName = "@Observaciones"
                param_observ.SqlDbType = SqlDbType.VarChar
                param_observ.Size = 150
                param_observ.Value = DBNull.Value
                param_observ.Direction = ParameterDirection.InputOutput

                Dim param_anchomedido As New SqlClient.SqlParameter
                param_anchomedido.ParameterName = "@AnchoMedido"
                param_anchomedido.SqlDbType = SqlDbType.Decimal
                param_anchomedido.Precision = 18
                param_anchomedido.Scale = 4
                param_anchomedido.Value = DBNull.Value
                param_anchomedido.Direction = ParameterDirection.InputOutput

                Dim param_e As New SqlClient.SqlParameter
                param_e.ParameterName = "@E"
                param_e.SqlDbType = SqlDbType.VarChar
                param_e.Size = 2
                param_e.Value = DBNull.Value
                param_e.Direction = ParameterDirection.InputOutput

                Dim param_alternativo As New SqlClient.SqlParameter
                param_alternativo.ParameterName = "@NroAlternativo"
                param_alternativo.SqlDbType = SqlDbType.BigInt
                param_alternativo.Value = DBNull.Value
                param_alternativo.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Bobinas_Select_By_IDDetalle_General", param_iddetalle, param_id, param_codigo, param_loteprov, param_matpri, param_ancho, param_espesor, param_peso, param_colada, param_calidad, param_oc, param_stock, param_numerocertificado, param_cumpli, param_aprob, param_observ, param_anchomedido, param_e, param_alternativo, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                    End If
                    If Not param_id.Value Is DBNull.Value Then
                        Id = param_id.Value
                    End If
                    If Not param_codigo.Value Is DBNull.Value Then
                        codigo = param_codigo.Value
                    End If
                    If Not param_loteprov.Value Is DBNull.Value Then
                        LoteProv = param_loteprov.Value
                    End If
                    If Not param_matpri.Value Is DBNull.Value Then
                        IdMateriaPrima = param_matpri.Value
                    End If
                    If Not param_ancho.Value Is DBNull.Value Then
                        ancho = param_ancho.Value
                    End If
                    If Not param_espesor.Value Is DBNull.Value Then
                        espesor = param_espesor.Value
                    End If
                    If Not param_peso.Value Is DBNull.Value Then
                        Peso = param_peso.Value
                    End If
                    If Not param_colada.Value Is DBNull.Value Then
                        Colada = param_colada.Value
                    End If
                    If Not param_calidad.Value Is DBNull.Value Then
                        Calidad = param_calidad.Value
                    End If
                    If Not param_oc.Value Is DBNull.Value Then
                        OC = param_oc.Value
                    End If
                    If Not param_stock.Value Is DBNull.Value Then
                        Stock = param_stock.Value
                    End If
                    If Not param_numerocertificado.Value Is DBNull.Value Then
                        NroCertificado = param_numerocertificado.Value
                    End If
                    If Not param_cumpli.Value Is DBNull.Value Then
                        Cumplido = param_cumpli.Value
                    End If
                    If Not param_aprob.Value Is DBNull.Value Then
                        Aprobado = param_aprob.Value
                    End If
                    If Not param_observ.Value Is DBNull.Value Then
                        Observaciones = param_observ.Value
                    End If
                    If Not param_anchomedido.Value Is DBNull.Value Then
                        AnchoMedido = param_anchomedido.Value
                    End If
                    If Not param_e.Value Is DBNull.Value Then
                        E = param_e.Value
                    End If
                    If Not param_alternativo.Value Is DBNull.Value Then
                        NroAlternativo = param_alternativo.Value
                    End If

                    BobinaPorIdDetalleGeneral = res


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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Sub ObtenerSectores(ByVal equipo As String, _
                                ByRef codigo As String, _
                                ByRef nombre As String, _
                                ByRef codigosectoranterior As String, _
                                ByRef mostrarlistado As Boolean, _
                                ByRef idsector As Long, _
                                ByRef idsectoranterior As Long)

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            Try
                ''COMENTADO MS 30-06-2010
                ''connection = SqlHelper.GetConnection(ConnStringRRHH)
                ''FIN COMENTADO
                ''NUEVO MS 30-06-2010.
                connection = SqlHelper.GetConnection(ConnStringUSUARIOS)
                ''FIN NUEVO

            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Try

                Dim param_Equipo As New SqlClient.SqlParameter
                param_Equipo.ParameterName = "@Equipo"
                param_Equipo.SqlDbType = SqlDbType.VarChar
                param_Equipo.Size = 20
                param_Equipo.Value = EquipoActual
                param_Equipo.Direction = ParameterDirection.Input

                Dim param_Codigo As New SqlClient.SqlParameter
                param_Codigo.ParameterName = "@Codigo"
                param_Codigo.SqlDbType = SqlDbType.VarChar
                param_Codigo.Size = 25
                param_Codigo.Value = ""
                param_Codigo.Direction = ParameterDirection.InputOutput

                Dim param_Nombre As New SqlClient.SqlParameter
                param_Nombre.ParameterName = "@Nombre"
                param_Nombre.SqlDbType = SqlDbType.VarChar
                param_Nombre.Size = 100
                param_Nombre.Value = ""
                param_Nombre.Direction = ParameterDirection.InputOutput

                Dim param_CodigoSectorAnterior As New SqlClient.SqlParameter
                param_CodigoSectorAnterior.ParameterName = "@CodigoSectorAnterior"
                param_CodigoSectorAnterior.SqlDbType = SqlDbType.VarChar
                param_CodigoSectorAnterior.Size = 25
                param_CodigoSectorAnterior.Value = ""
                param_CodigoSectorAnterior.Direction = ParameterDirection.InputOutput

                Dim param_MostrarListado As New SqlClient.SqlParameter
                param_MostrarListado.ParameterName = "@MostrarListado"
                param_MostrarListado.SqlDbType = SqlDbType.Bit
                param_MostrarListado.Value = False
                param_MostrarListado.Direction = ParameterDirection.InputOutput


                Dim param_idSector As New SqlClient.SqlParameter
                param_idSector.ParameterName = "@idSector"
                param_idSector.SqlDbType = SqlDbType.BigInt
                param_idSector.Value = 0
                param_idSector.Direction = ParameterDirection.InputOutput

                Dim param_idSectorAnterior As New SqlClient.SqlParameter
                param_idSectorAnterior.ParameterName = "@idSectorAnterior"
                param_idSectorAnterior.SqlDbType = SqlDbType.BigInt
                param_idSectorAnterior.Value = 0
                param_idSectorAnterior.Direction = ParameterDirection.InputOutput


                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spObtenerSectores", param_Equipo, param_Codigo, param_Nombre, param_CodigoSectorAnterior, param_MostrarListado, param_idSector, param_idSectorAnterior)
                    codigo = IIf(param_Codigo.Value Is DBNull.Value, False, param_Codigo.Value)
                    nombre = IIf(param_Nombre.Value Is DBNull.Value, False, param_Nombre.Value)
                    codigosectoranterior = IIf(param_CodigoSectorAnterior.Value Is DBNull.Value, False, param_CodigoSectorAnterior.Value)
                    mostrarlistado = IIf(param_MostrarListado.Value Is DBNull.Value, False, param_MostrarListado.Value)
                    idsector = IIf(param_idSector.Value Is DBNull.Value, False, param_idSector.Value)
                    idsectoranterior = IIf(param_idSectorAnterior.Value Is DBNull.Value, False, param_idSectorAnterior.Value)

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "AsignarPermisos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub

    'Public Sub ObtenerSectoresPanol(ByVal equipo As String, _
    '                                ByRef codigo As String, _
    '                                ByRef nombre As String, _
    '                                ByRef codigosectoranterior As String, _
    '                                ByRef mostrarlistado As Boolean, _
    '                                ByRef idsector As Long, _
    '                                ByRef idsectoranterior As Long)

    '    Dim connection As SqlClient.SqlConnection = Nothing
    '    Dim res As Integer = 0

    '    Try
    '        Try

    '            connection = SqlHelper.GetConnection(ConnStringPanolNet)


    '        Catch ex As Exception
    '            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End Try

    '        Try

    '            Dim param_Equipo As New SqlClient.SqlParameter
    '            param_Equipo.ParameterName = "@Equipo"
    '            param_Equipo.SqlDbType = SqlDbType.VarChar
    '            param_Equipo.Size = 20
    '            param_Equipo.Value = EquipoActual
    '            param_Equipo.Direction = ParameterDirection.Input

    '            Dim param_Codigo As New SqlClient.SqlParameter
    '            param_Codigo.ParameterName = "@Codigo"
    '            param_Codigo.SqlDbType = SqlDbType.VarChar
    '            param_Codigo.Size = 25
    '            param_Codigo.Value = ""
    '            param_Codigo.Direction = ParameterDirection.InputOutput

    '            Dim param_Nombre As New SqlClient.SqlParameter
    '            param_Nombre.ParameterName = "@Nombre"
    '            param_Nombre.SqlDbType = SqlDbType.VarChar
    '            param_Nombre.Size = 100
    '            param_Nombre.Value = ""
    '            param_Nombre.Direction = ParameterDirection.InputOutput

    '            Dim param_CodigoSectorAnterior As New SqlClient.SqlParameter
    '            param_CodigoSectorAnterior.ParameterName = "@CodigoSectorAnterior"
    '            param_CodigoSectorAnterior.SqlDbType = SqlDbType.VarChar
    '            param_CodigoSectorAnterior.Size = 25
    '            param_CodigoSectorAnterior.Value = ""
    '            param_CodigoSectorAnterior.Direction = ParameterDirection.InputOutput

    '            Dim param_MostrarListado As New SqlClient.SqlParameter
    '            param_MostrarListado.ParameterName = "@MostrarListado"
    '            param_MostrarListado.SqlDbType = SqlDbType.Bit
    '            param_MostrarListado.Value = False
    '            param_MostrarListado.Direction = ParameterDirection.InputOutput


    '            Dim param_idSector As New SqlClient.SqlParameter
    '            param_idSector.ParameterName = "@idSector"
    '            param_idSector.SqlDbType = SqlDbType.BigInt
    '            param_idSector.Value = 0
    '            param_idSector.Direction = ParameterDirection.InputOutput

    '            Dim param_idSectorAnterior As New SqlClient.SqlParameter
    '            param_idSectorAnterior.ParameterName = "@idSectorAnterior"
    '            param_idSectorAnterior.SqlDbType = SqlDbType.BigInt
    '            param_idSectorAnterior.Value = 0
    '            param_idSectorAnterior.Direction = ParameterDirection.InputOutput


    '            Try
    '                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Sectores_Obtener", param_Equipo, param_Codigo, param_Nombre, param_CodigoSectorAnterior, param_MostrarListado, param_idSector, param_idSectorAnterior)
    '                codigo = IIf(param_Codigo.Value Is DBNull.Value, False, param_Codigo.Value)
    '                nombre = IIf(param_Nombre.Value Is DBNull.Value, False, param_Nombre.Value)
    '                codigosectoranterior = IIf(param_CodigoSectorAnterior.Value Is DBNull.Value, False, param_CodigoSectorAnterior.Value)
    '                mostrarlistado = IIf(param_MostrarListado.Value Is DBNull.Value, False, param_MostrarListado.Value)
    '                idsector = IIf(param_idSector.Value Is DBNull.Value, False, param_idSector.Value)
    '                idsectoranterior = IIf(param_idSectorAnterior.Value Is DBNull.Value, False, param_idSectorAnterior.Value)

    '            Catch ex As Exception
    '                Throw ex
    '            End Try
    '        Finally

    '        End Try
    '    Catch ex As Exception
    '        Dim errMessage As String = ""
    '        Dim tempException As Exception = ex

    '        While (Not tempException Is Nothing)
    '            errMessage += tempException.Message + Environment.NewLine + Environment.NewLine
    '            tempException = tempException.InnerException
    '        End While

    '        MessageBox.Show(String.Format("Se provocó un problema al procesar la información en la Base de Datos, por favor, valide el siguiente mensaje de error: {0}" _
    '          + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
    '          "AsignarPermisos", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally
    '        If Not connection Is Nothing Then
    '            CType(connection, IDisposable).Dispose()
    '        End If
    '    End Try
    'End Sub


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



    ' Permite obtener datos generales de la tabla materiales a traves del codigo
    Public Function ObtenerMaterial_Icys(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef qtystock As Decimal, _
                                            ByRef minimo As Decimal, _
                                            ByRef maximo As Decimal, _
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
                connection = SqlHelper.GetConnection(ConnStringICYS)
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
                param_qtystock.Scale = 4
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 4
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 4
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_precio As New SqlClient.SqlParameter
                param_precio.ParameterName = "@preciosiniva"
                param_precio.SqlDbType = SqlDbType.Decimal
                param_precio.Precision = 18
                param_precio.Scale = 4
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
                                              param_minimo, param_maximo, param_precio, param_res)

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

                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If
                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            If Not param_precio.Value Is DBNull.Value Then
                                Precio = param_precio.Value
                            End If
                        End If
                    End If

                    ObtenerMaterial_Icys = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
    Public Function ObtenerProveedor_Acer(ByVal Codigo As String, _
                                            ByRef ID_prov As Long, _
                                            ByRef nombre_prov As String, _
                                            ByRef ganancia As Decimal, _
                                            ByRef bonificacion As Decimal) As Integer


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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Proveedor_Select_By_Codigo", param_codigo, _
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

                    ObtenerProveedor_Acer = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function


    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
    Public Function ObtenerUnidad_Acer(ByVal Cod_Unidad As String, ByRef IDUnidad As Long, _
                                       ByRef nombreunidad As String, ByRef CodUnidad As String) As Integer

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
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Unidades_Select_By_Codigo", param_codigo, _
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

                    ObtenerUnidad_Acer = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
    Public Function ObtenerMoneda_Acer(ByVal Cod_Moneda As String, ByRef IDMoneda As Long, _
                                       ByRef nombremoneda As String, ByRef CodMoneda As String, ByRef ValorCambio As Double) As Integer

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
                param_valorcambio.Size = 18
                param_valorcambio.Value = DBNull.Value
                param_valorcambio.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Monedas_Select_By_Codigo", param_codigo, _
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

                    ObtenerMoneda_Acer = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
    Public Function ObtenerMaterialAlmacen(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef qtystock As Decimal, _
                                            ByRef minimo As Decimal, _
                                            ByRef maximo As Decimal) As Integer

        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringICYS)
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

                'Dim param_ManejaLote As New SqlClient.SqlParameter
                'param_ManejaLote.ParameterName = "@ManejaLote"
                'param_ManejaLote.SqlDbType = SqlDbType.Bit
                'param_ManejaLote.Value = DBNull.Value
                'param_ManejaLote.Direction = ParameterDirection.InputOutput


                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 4
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 4
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 4
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                'Dim param_ubicacion As New SqlClient.SqlParameter
                'param_ubicacion.ParameterName = "@ubicacion"
                'param_ubicacion.SqlDbType = SqlDbType.VarChar
                'param_ubicacion.Size = 50
                'param_ubicacion.Value = DBNull.Value
                'param_ubicacion.Direction = ParameterDirection.InputOutput

                'Dim param_almacen As New SqlClient.SqlParameter
                'param_almacen.ParameterName = "@almacen"
                'param_almacen.SqlDbType = SqlDbType.BigInt
                'param_almacen.Value = idalmacen
                'param_almacen.Direction = ParameterDirection.Input


                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Select_By_Codigo_Almacen", _
                                              param_codigo, param_id, param_nombre, param_idunidad, param_unidad, param_qtystock, _
                                              param_minimo, param_maximo, param_res)

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
                            'If Not param_ManejaLote.Value Is DBNull.Value Then
                            '    ManejaLote = param_ManejaLote.Value
                            'End If
                            If Not param_qtystock.Value Is DBNull.Value Then
                                qtystock = param_qtystock.Value
                            End If


                            If Not param_minimo.Value Is DBNull.Value Then
                                minimo = param_minimo.Value
                            End If

                            If Not param_maximo.Value Is DBNull.Value Then
                                maximo = param_maximo.Value
                            End If
                            'If Not param_ubicacion.Value Is DBNull.Value Then
                            '    ubicacion = param_ubicacion.Value
                            'End If


                        End If
                    End If


                    ObtenerMaterialAlmacen = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function


    ' Nueva cp 17/10/2011 reemplazará a la ObtenerMaterial porque se necesita el almacen 
    ' y el lote para saber cuanto stock hay 
    ' Permite obtener datos generales de la tabla materiales a traves del codigo
    Public Function ObtenerMaterialAlmacenLote(ByVal Codigo As String, _
                                            ByRef ID As Long, _
                                            ByRef nombre As String, _
                                            ByRef IdUnidad As Long, _
                                            ByRef NombreUnidad As String, _
                                            ByRef ManejaLote As Boolean, _
                                            ByRef qtystock As Decimal, _
                                            ByRef minimo As Decimal, _
                                            ByRef maximo As Decimal, _
                                            ByRef ubicacion As String, _
                                            ByRef idalmacen As Long, _
                                            ByRef status As String, _
                                            ByRef lote As Long) As Integer


        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, id As Long, idunidad As Long, unidad As String = "", nombre As String = "", ManejaLote As Boolean, ubicacion As String = ""
        'Dim stock As Decimal = 0, minimo As Decimal = 0, maximo As Decimal = 0
        '**************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringICYS)
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

                Dim param_ManejaLote As New SqlClient.SqlParameter
                param_ManejaLote.ParameterName = "@ManejaLote"
                param_ManejaLote.SqlDbType = SqlDbType.Bit
                param_ManejaLote.Value = DBNull.Value
                param_ManejaLote.Direction = ParameterDirection.InputOutput


                Dim param_qtystock As New SqlClient.SqlParameter
                param_qtystock.ParameterName = "@qtyStock"
                param_qtystock.SqlDbType = SqlDbType.Decimal
                param_qtystock.Precision = 18
                param_qtystock.Scale = 4
                param_qtystock.Value = DBNull.Value
                param_qtystock.Direction = ParameterDirection.InputOutput

                Dim param_minimo As New SqlClient.SqlParameter
                param_minimo.ParameterName = "@minimo"
                param_minimo.SqlDbType = SqlDbType.Decimal
                param_minimo.Precision = 18
                param_minimo.Scale = 4
                param_minimo.Value = DBNull.Value
                param_minimo.Direction = ParameterDirection.InputOutput

                Dim param_maximo As New SqlClient.SqlParameter
                param_maximo.ParameterName = "@maximo"
                param_maximo.SqlDbType = SqlDbType.Decimal
                param_maximo.Precision = 18
                param_maximo.Scale = 4
                param_maximo.Value = DBNull.Value
                param_maximo.Direction = ParameterDirection.InputOutput

                Dim param_ubicacion As New SqlClient.SqlParameter
                param_ubicacion.ParameterName = "@ubicacion"
                param_ubicacion.SqlDbType = SqlDbType.VarChar
                param_ubicacion.Size = 50
                param_ubicacion.Value = DBNull.Value
                param_ubicacion.Direction = ParameterDirection.InputOutput

                Dim param_almacen As New SqlClient.SqlParameter
                param_almacen.ParameterName = "@almacen"
                param_almacen.SqlDbType = SqlDbType.BigInt
                param_almacen.Value = idalmacen
                param_almacen.Direction = ParameterDirection.Input


                Dim param_lote As New SqlClient.SqlParameter
                param_lote.ParameterName = "@lote"
                param_lote.SqlDbType = SqlDbType.BigInt
                param_lote.Value = lote
                param_lote.Direction = ParameterDirection.Input

                Dim param_status As New SqlClient.SqlParameter
                param_status.ParameterName = "@status"
                param_status.SqlDbType = SqlDbType.VarChar
                param_status.Size = 1
                param_status.Value = status
                param_status.Direction = ParameterDirection.InputOutput

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Materiales_Select_By_Codigo_Almacen_Lote", param_codigo, param_id, param_nombre, param_idunidad, param_unidad, param_ManejaLote, param_qtystock, param_minimo, param_maximo, param_ubicacion, param_almacen, param_lote, param_status, param_res)

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
                            If Not param_ManejaLote.Value Is DBNull.Value Then
                                ManejaLote = param_ManejaLote.Value
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
                            If Not param_ubicacion.Value Is DBNull.Value Then
                                ubicacion = param_ubicacion.Value
                            End If
                            If Not param_status.Value Is DBNull.Value Then
                                status = param_status.Value
                            End If


                        End If
                    End If


                    ObtenerMaterialAlmacenLote = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla usuarios a travez del campo id
    Public Function ObtenerDatosDelUserId(ByVal Id As Long, _
                                            ByRef codigo As String, _
                                            ByRef nombre As String, _
                                            ByRef pass As String) As Integer


        'COPIAR DECLARACIONES/SETEOS Y LLEVAR A DONDE SE NECESITE.......
        '**************************************************************************************
        'Dim codigo As String, nombre As String, pass_actual As String
        'codigo = "" : nombre = "" : pass_actual = "" 
        '**************************************************************************************

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function


    ' Permite obtener datos generales de la tabla materiales a traves del codigo
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function


    ' retorna Verdadero si el archivo existe   
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

    'Public Sub ControladorDeExepcionesReportes(ByRef ex As Exception, ByVal nbrereporte As String)

    '    If System.Runtime.InteropServices.Marshal.GetHRForException(ex) = -2147352565 Then
    '        MessageBox.Show("El Nombre del o los Parametro/s del Reporte " & nbrereporte & vbCrLf & "difiere del Parametro definido en el Sistema" & vbCrLf & "o no se ha definido el Campo Formula @filtradopor en el Reporte.", "Error", MessageBoxButtons.OK)
    '    Else
    '        ex.Message().ToString()
    '    End If
    'End Sub

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

    ''NUEVO MS 02-09-2010
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        Exit Sub

    End Sub
    ''FIN NUEVO

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

    'dg 20-5-2011
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






    ' Permite obtener datos generales de la tabla materiales a traves del id
    Public Function BuscarDatosMaterialPorID(ByVal id As Long, _
                                            ByRef codigo As String, _
                                            ByRef nombre As String, _
                                            ByRef nombrelargo As String, _
                                            ByRef idfamilia As Long, _
                                            ByRef idunidad As Long) As Integer

        'ByRef tipo As String, _
        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String, _
        'ByRef observaciones As String, _
        'ByRef eninventario As Boolean


        'COPIAR LAS DECLARACIONES/SETEOS Y LLAMADA DE LA FUNCION Y LLEVAR A DONDE SE NECESITE.......
        '***********************************************************************************************************************
        'Dim id, idunidad, idfamilia As Long
        'Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String
        'Dim ManejaLote, eninventario As Boolean
        'codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : ubicacion = "" : observaciones = ""

        'res = BuscarDatosMaterialPorID(id, codigo, nombre, nombrelargo, tipo, idfamilia, idunidad, ManejaLote, ubicacion, observaciones, eninventario)
        '***********************************************************************************************************************

        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringICYS)
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

                Dim param_nombrelargo As New SqlClient.SqlParameter
                param_nombrelargo.ParameterName = "@nombrelargo"
                param_nombrelargo.SqlDbType = SqlDbType.VarChar
                param_nombrelargo.Size = 70
                param_nombrelargo.Value = DBNull.Value
                param_nombrelargo.Direction = ParameterDirection.Output

                'Dim param_tipo As New SqlClient.SqlParameter
                'param_tipo.ParameterName = "@tipo"
                'param_tipo.SqlDbType = SqlDbType.VarChar
                'param_tipo.Size = 1
                'param_tipo.Value = DBNull.Value
                'param_tipo.Direction = ParameterDirection.Output

                Dim param_idfamilia As New SqlClient.SqlParameter
                param_idfamilia.ParameterName = "@idfamilia"
                param_idfamilia.SqlDbType = SqlDbType.BigInt
                param_idfamilia.Value = DBNull.Value
                param_idfamilia.Direction = ParameterDirection.Output

                Dim param_idunidad As New SqlClient.SqlParameter
                param_idunidad.ParameterName = "@idunidad"
                param_idunidad.SqlDbType = SqlDbType.BigInt
                param_idunidad.Value = DBNull.Value
                param_idunidad.Direction = ParameterDirection.Output

                'Dim param_ManejaLote As New SqlClient.SqlParameter
                'param_ManejaLote.ParameterName = "@manejalote"
                'param_ManejaLote.SqlDbType = SqlDbType.Bit
                'param_ManejaLote.Value = DBNull.Value
                'param_ManejaLote.Direction = ParameterDirection.Output

                'Dim param_ubicacion As New SqlClient.SqlParameter
                'param_ubicacion.ParameterName = "@ubicacion"
                'param_ubicacion.SqlDbType = SqlDbType.VarChar
                'param_ubicacion.Size = 50
                'param_ubicacion.Value = DBNull.Value
                'param_ubicacion.Direction = ParameterDirection.Output

                'Dim param_observaciones As New SqlClient.SqlParameter
                'param_observaciones.ParameterName = "@observaciones"
                'param_observaciones.SqlDbType = SqlDbType.VarChar
                'param_observaciones.Size = 200
                'param_observaciones.Value = DBNull.Value
                'param_observaciones.Direction = ParameterDirection.Output

                'Dim param_eninventario As New SqlClient.SqlParameter
                'param_eninventario.ParameterName = "@eninventario"
                'param_eninventario.SqlDbType = SqlDbType.Bit
                'param_eninventario.Value = DBNull.Value
                'param_eninventario.Direction = ParameterDirection.Output

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = DBNull.Value
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spBuscarDatosMaterialPorID", _
                                              param_id, param_codigo, param_nombre, param_nombrelargo, _
                                                param_idfamilia, param_idunidad, param_res)

                    If Not param_res.Value Is DBNull.Value Then
                        res = param_res.Value
                        If res = 1 Then
                            If Not param_codigo.Value Is DBNull.Value Then
                                codigo = param_codigo.Value
                            End If
                            If Not param_nombre.Value Is DBNull.Value Then
                                nombre = param_nombre.Value
                            End If
                            If Not param_nombrelargo.Value Is DBNull.Value Then
                                nombrelargo = param_nombrelargo.Value
                            End If
                            'If Not param_tipo.Value Is DBNull.Value Then
                            '    tipo = param_tipo.Value
                            'End If
                            If Not param_idfamilia.Value Is DBNull.Value Then
                                idfamilia = param_idfamilia.Value
                            End If
                            If Not param_idunidad.Value Is DBNull.Value Then
                                idunidad = param_idunidad.Value
                            End If
                            'If Not param_ManejaLote.Value Is DBNull.Value Then
                            '    MANEJALOTE = param_ManejaLote.Value
                            'End If
                            'If Not param_ubicacion.Value Is DBNull.Value Then
                            '    ubicacion = param_ubicacion.Value
                            'End If
                            'If Not param_observaciones.Value Is DBNull.Value Then
                            '    observaciones = param_observaciones.Value
                            'End If
                            'If Not param_eninventario.Value Is DBNull.Value Then
                            '    eninventario = param_eninventario.Value
                            'End If
                        End If
                    End If

                    BuscarDatosMaterialPorID = res

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
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

        'ByRef tipo As String, _
        'ByRef ManejaLote As Boolean, _
        'ByRef ubicacion As String, _
        'ByRef observaciones As String, _
        'ByRef eninventario As Boolean


        'COPIAR LAS DECLARACIONES/SETEOS Y LLAMADA DE LA FUNCION Y LLEVAR A DONDE SE NECESITE.......
        '***********************************************************************************************************************
        'Dim id, idunidad, idfamilia As Long
        'Dim codigo, nombre, nombrelargo, tipo, ubicacion, observaciones As String
        'Dim ManejaLote, eninventario As Boolean
        'codigo = "" : nombre = "" : nombrelargo = "" : tipo = "" : ubicacion = "" : observaciones = ""

        'res = BuscarDatosMaterialPorID(id, codigo, nombre, nombrelargo, tipo, idfamilia, idunidad, ManejaLote, ubicacion, observaciones, eninventario)
        '***********************************************************************************************************************

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    'dg 02/09/2011
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function


    ' dg 02/09/2011
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function

    ' dg 02/09/2011
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function




    ' Permite buscar la ip del socket...
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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try


    End Function





End Module
