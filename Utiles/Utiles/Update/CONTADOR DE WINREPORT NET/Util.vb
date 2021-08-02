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
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'DEFINO UN OBJETO TipoConexion                                                                            '      
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Public TipoConexionTM3 As TipoConexion
    Public TipoConexionWinreportNet As TipoConexion


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'VARIABLES GLOBALES                                                                                       '      
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public path_raiz As String

    Public ConnStringTM3 As String                                  ' String de conexion a TM3
    ''NUEVO MS 02-09-2010
    Public ConnStringRN As String                                  ' String de conexion a RN
    ''FIN NUEVO
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
    Public ok_cambio As Boolean 'dg 29-07-10
    Public pass As String  'dg 29-07-10


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


    Public Id_Diseno As Long
    Public Texto_Diseno As String



    'variable usada para pasar el numero de ingreso de materia prima desde el maestro al detalle
    Public Codigo_Ingreso_Maestro As Long

    'variable usada para pasar el numero de id de la tabla ocs a la ocsitmes 
    Public ID_OCs_OCsItems As Long

    'variable usada para pasar el numero de id(tabla ingresomateriaprimadetalle) entre el ingresomateriaprimadetalle y bobina
    Public Id_Detalle As Long = 0

    'variable que indica si se debe borrar el ingresomaestro (cuando vale 1)
    Public Borrar_Maestro As Byte = 0
    'variable que indica si se debe borrar la oc (cuando vale 1)
    Public Borrar_OC As Byte = 0

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
        Dim ambito As String
        If Trim$(server) <> "" Then
            Select Case UCase(Trim$(server))

                Case "ARES"
                    Clave_Segun_Server = "pelicano"

                Case "10.10.106.39"
                    Clave_Segun_Server = "pelicano"

                Case "10.10.105.34"
                    Clave_Segun_Server = "pelicano"

                Case "CPONTE\TEST"
                    Clave_Segun_Server = "avestruz"

                Case "10.10.105.28\FORMAR"
                    Clave_Segun_Server = "pelicano"

                Case "SVRF02\FORMAR"
                    Clave_Segun_Server = "pelicano"


                Case "DESARROLLO04\TEST"
                    Clave_Segun_Server = "avestruz"

                Case "SRVT03"
                    Clave_Segun_Server = "pelicano"

                Case "DESARROLLO2"
                    Clave_Segun_Server = "avestruz"

                Case "DESARROLLO03\TEST"
                    Clave_Segun_Server = "avestruz"

                Case "BLP"
                    Clave_Segun_Server = "pelicano"

            End Select
        Else
            ambito = LeeIni("varios", "ambito")

            Select Case UCase(ambito)
                Case "TUBHIER"
                    Clave_Segun_Server = "pelicano"
                Case "FORMAR"
                    Clave_Segun_Server = "pelicano"
                Case "FORMARPRUEBA"
                    Clave_Segun_Server = "pelicano"
                Case "CPONTE"
                    Clave_Segun_Server = "avestruz"
                Case "DARIO"
                    Clave_Segun_Server = "avestruz"
                Case "MARIANO"
                    Clave_Segun_Server = "avestruz"
                Case "SEQUELDR"
                    Clave_Segun_Server = "pelicano"
                Case "BLP"
                    Clave_Segun_Server = "pelicano"
            End Select
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

        ' CADENA DE CONEXION A RN
        aux = IniGetSection(Archivo, "Conexion_TM3")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_TM3", "Server")
            ConnStringTM3 = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringTM3 = ConnStringTM3 & LeeIni("Conexion_TM3", "Origen")
            ConnStringTM3 = ConnStringTM3 & ";Data Source="
            ConnStringTM3 = ConnStringTM3 & temp
            ''If (My.Application.Info.Title.ToString() = "WinreportNet") Then
            TipoConexionWinreportNet.server = temp
            TipoConexionWinreportNet.base = LeeIni("Conexion_TM3", "Origen")
            TipoConexionWinreportNet.usuario = "sa"
            TipoConexionWinreportNet.contrasena = Clave_Segun_Server(temp)
            ''Else
            ''TipoConexionTM3.server = temp
            ''TipoConexionTM3.base = LeeIni("Conexion_TM3", "Origen")
            '' TipoConexionTM3.usuario = "sa"
            ''TipoConexionTM3.contrasena = Clave_Segun_Server(temp)
            ''End If

        Else
            ConnStringTM3 = ""
        End If

        ''NUEVO MS 02-09-2010
        aux = IniGetSection(Archivo, "Conexion_RN")
        If aux(0) <> "" Then
            temp = LeeIni("Conexion_RN", "Server")
            ConnStringRN = "Persist Security Info=False;User ID=sa;Password=" & Clave_Segun_Server(temp) & ";Initial Catalog="
            ConnStringRN = ConnStringRN & LeeIni("Conexion_RN", "Origen")
            ConnStringRN = ConnStringRN & ";Data Source="
            ConnStringRN = ConnStringRN & temp

            ''COMENTADO MS 02-09-2010
            '' ''TipoConexionWinreportNet.server = temp
            '' ''TipoConexionWinreportNet.base = LeeIni("Conexion_RN", "Origen")
            '' ''TipoConexionWinreportNet.usuario = "sa"
            '' ''TipoConexionWinreportNet.contrasena = Clave_Segun_Server(temp)
            ''FIN COMENTADO
         

        Else
            ConnStringRN = ""
        End If
        ''FIN NUEVO







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
        Finally
            Cnn.Close()
        End Try
    End Function

    Public Sub AsignarPermisos(ByVal user As Long, ByVal form As String, ByRef a As Boolean, ByRef m As Boolean, ByRef b As Boolean, ByRef b_f As Boolean)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                ''COMENTADO MS 25-06-2010    
                ''connection = SqlHelper.GetConnection(cnnString_RRHH)
                ''FIN COMENTADO
                ''NUEVO MS 25-06-2010
                connection = SqlHelper.GetConnection(ConnStringTM3)
                ''FIN NUEVO
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

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spGetPermisos", param_iduser, param_form, param_alta, param_modifica, param_baja, param_bajafisica)
                    a = IIf(param_alta.Value Is DBNull.Value, False, param_alta.Value)
                    m = IIf(param_modifica.Value Is DBNull.Value, False, param_modifica.Value)
                    b = IIf(param_baja.Value Is DBNull.Value, False, param_baja.Value)
                    b_f = IIf(param_bajafisica.Value Is DBNull.Value, False, param_bajafisica.Value)

                Catch ex As Exception
                    a = False
                    b = False
                    b_f = False
                    m = False
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
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
            Catch ex As Exception
                MessageBox.Show("Error en TextBoxAGrilla " & ex.Message)
            End Try
        Next
    End Sub

    ' Carga datos de la grilla a los textbox
    Public Sub GrillaATextBox(ByRef oVControls As Object, ByRef g As DataGridView)
        For Each oControl As Object In oVControls
            Try
                If TypeOf (oControl) Is TextBox Or TypeOf (oControl) Is DateTimePicker Or TypeOf (oControl) Is Label Then
                    If oControl.Tag <> "" Then
                        oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                    End If
                End If
                If TypeOf (oControl) Is CheckBox Then
                    If oControl.Tag <> "" Then
                        oControl.checked = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, False, g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                    End If
                End If
                If TypeOf (oControl) Is ComboBox Then
                    If oControl.Tag <> "" Then
                        If g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value <> "" Then
                            'If oControl.dropdownstyle = 2 Then
                            '    oControl.dropdownstyle = 1
                            '    oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                            '    oControl.dropdownstyle = 2
                            'Else
                            '    oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)
                            'End If
                            oControl.Text = IIf(g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value, "", g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value)

                            'If g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value Is DBNull.Value Then
                            '    oControl.Text = ""
                            'Else
                            '    oControl.Text = g.CurrentRow.Cells(CType(oControl.Tag, Integer)).Value.ToString
                            'End If
                        Else
                            oControl.selectedindex = -1
                        End If
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
            Catch ex As Exception
                MessageBox.Show("error anidado" & ex.Message)
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
            cnn = New Data.SqlClient.SqlConnection(Util.ConnStringTM3)
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
            cmd.Parameters.Add("@Nombre", Data.SqlDbType.NVarChar, 20)
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
            cnn = New Data.SqlClient.SqlConnection(ConnStringTM3)
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
        per = False
        g.Rows.Add()

        If Not g.Rows.Count = 1 Then
            g.CurrentCell = g.Rows(g.Rows.Count - 1).Cells(0)
        End If

        TextBoxAGrilla(c.Controls, g)
        per = True
    End Sub

    ' Mensajes del status bar
    Public Sub MsgStatus(ByRef s As ToolStripStatusLabel, ByVal texto As String, Optional ByVal imagen As System.Drawing.Image = Nothing)
        s.Text = " " & texto
        s.Image = imagen
    End Sub

    ' Limpiar las cajas de texto de un form
    Public Sub LimpiarTextBox(ByRef oVControls As Object)
        For Each oControl As Object In oVControls
            Try
                If TypeOf (oControl) Is TextBox Or TypeOf (oControl) Is ComboBox Then
                    oControl.Text = ""
                End If
                If TypeOf (oControl) Is DateTimePicker Then
                    oControl.text = ""
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
            Catch ex As Exception
                MessageBox.Show("error anidado" & ex.Message)
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
                connection = SqlHelper.GetConnection(ConnStringTM3)
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
                connection = SqlHelper.GetConnection(ConnStringTM3)
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
            If cadena.ToUpper = dr(campo).ToString.ToUpper Then
                ExisteDatoEnCombo = True
                Exit Function
            End If
        Next ni

        ExisteDatoEnCombo = False

    End Function

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
                connection = SqlHelper.GetConnection(ConnStringTM3)
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
                connection = SqlHelper.GetConnection(ConnStringTM3)
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
                connection = SqlHelper.GetConnection(ConnStringTM3)
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "AsignarPermisos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Sub


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

                connection = SqlHelper.GetConnection(ConnStringTM3)

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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
    Public Sub ControladorDeExepcionesReportes(ByRef ex As Exception, ByVal nbrereporte As String)

        If System.Runtime.InteropServices.Marshal.GetHRForException(ex) = -2147352565 Then
            MessageBox.Show("El Nombre del o los Parametro/s del Reporte " & nbrereporte & vbCrLf & "difiere del Parametro definido en el Sistema" & vbCrLf & "o no se ha definido el Campo Formula @filtradopor en el Reporte.", "Error", MessageBoxButtons.OK)
        Else
            ex.Message().ToString()
        End If
    End Sub
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
                connection = SqlHelper.GetConnection(ConnStringRN)
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
                param_usu.Value = CInt(usu) ''CType(Util.MyCType(txtORDEN, "integer").Text, Integer)
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

            MessageBox.Show(String.Format("Se provocó un problema al usar el bloque de  Aplicacion, por favor valide el siguiente mensaje de error: {0}" _
              + Environment.NewLine + "This test requires some modifications to the Northwind database. Please make sure the database has been initialized using the SetUpDataBase.bat database script, or from the  Install Quickstart option on the Start menu.", errMessage), _
              "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        Exit Sub

    End Sub
    ''FIN NUEVO
End Module
