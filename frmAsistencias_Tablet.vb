Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Data.OleDb

Public Class frmAsistenciasTablet

    'Declaraciones para leer/ecribir un puerto E/S utilizando inpout32.dll
    Public Declare Function leer Lib "inpout32.dll" Alias "Inp32" _
           (ByVal puerto As Integer) As Short
    Public Declare Sub escribir Lib "inpout32.dll" Alias "Out32" _
           (ByVal puerto As Integer, ByVal Valor As Integer)

    Dim contador As Integer = 0
    Dim documento As String

#Region "Componentes Formulario"

    Private Sub frmIngresos_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtPantalla.Focus()
    End Sub

    Private Sub FrmControlIngreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
        Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)

        Dim codigo As String, nombre As String, pass_actual As String
        codigo = "" : nombre = "" : pass_actual = ""

        If Process.GetProcessesByName(aProcName).Length > 1 Then
            MessageBox.Show("Ya se está ejecutando una instancia del Programa Email.")
            Application.Exit()
        End If

        Util.pass_vencida = False
        Util.pass_repetida = False

        path = Application.ExecutablePath.ToString
        path = Util.TruncarUltimaCarpeta(path)

        Try
            Dim culture As New System.Globalization.CultureInfo("es-ES")
            culture.NumberFormat.NumberDecimalSeparator = "."
            culture.NumberFormat.NumberGroupSeparator = ","
            culture.NumberFormat.CurrencyDecimalSeparator = "."
            culture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture = culture
        Catch ex As Exception
            MsgBox("Se produjo un inconveniente al modificar la configuración regional." & vbCrLf & "Por avise al Dpto. de Sistemas.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Error")
        End Try


        Do While path <> ""

            If Existe(path & "\INIs") Then

                Exit Do
            Else
                path = Util.TruncarUltimaCarpeta(path)
            End If
        Loop

        If path = "" Then
            MsgBox("No se ha Encontrado la carpeta de Inis del Sistema")
            Util.Logueado_OK = False
            Me.Dispose()
        Else
            path_raiz = path
            pathrpt = path_raiz & "\Rpt\"
            pathinis = path_raiz & "\INIs\"
            Archivo = pathinis & UCase(SystemInformation.ComputerName.ToString) & ".ini"
        End If

        If Not (System.IO.File.Exists(Archivo)) Then
            MsgBox("No se ha Encontrado el Archivo Ini de la PC con la que se desea conectar al Sistema")
            Util.Logueado_OK = False
            Me.Dispose()
        End If

        'INICIALIZAR CONEXIONES SEGUN INFO DEL INI DEL USUARIO
        Inicia_Conexion()

        ' Comprobar la clave en la base de datos
        Util.UserID = 1

        txtPantalla.Focus()

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim dt As New DataTable
        Dim sqltxt2 As String

        sqltxt2 = "SELECT InfoPantalla FROM parametros"

        Dim cmd As New SqlCommand(sqltxt2, connection)
        Dim da As New SqlDataAdapter(cmd)

        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            lblInfoPantalla.Text = dt.Rows(0)("InfoPantalla").ToString
        End If

        txtPantalla.Focus()

    End Sub

    'Private Sub FrmControlIngreso_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Enter
    '            IngresoEgreso()
    '    End Select

    'End Sub

    Private Sub tmrControlIngreso_Tick(sender As Object, e As EventArgs) Handles tmrControlIngreso.Tick
        tmrControlIngreso.Enabled = False
        pnlSocio.SendToBack()
        pnlPromocion.BringToFront()
        lblMensaje.Visible = False
    End Sub

    Private Sub TimerFechaHora_Tick(sender As Object, e As EventArgs) Handles TimerFechaHora.Tick
        lblPromocion.Text = Format$(Now, "dddd dd 'de' MMMM 'de' yyyy  " + "hh:mm:ss")
    End Sub

    Private Sub txtPantalla_TextChanged(sender As Object, e As EventArgs) Handles txtPantalla.TextChanged
        If txtPantalla.Text.Length = 8 Then
            IngresoEgreso()
        End If
    End Sub

#End Region

#Region " INFORMACIONES "
    'Dim cnn As New SqlConnection("Server=NBK-CAMPANA;uid=sa;pwd=campana.;database=Cronos")
    'Public Sub ConnectToSql()
    '    Dim conn As New SqlClient.SqlConnection
    '    conn.ConnectionString = "integrated security=SSPI;data source=Cronos;" & _
    '"persist security info=False;initial catalog=northwind"
    '    Try
    '        conn.Open()
    '    Catch ex As Exception
    '        MessageBox.Show("Error al conectar con la base de datos.")
    '    Finally
    '        conn.Close()
    '    End Try
    'End Sub

#End Region

#Region "Procedimientos"

    Private Sub IngresoEgreso()

        documento = txtPantalla.Text

        Dim res As Integer = 0
        Dim connection As SqlClient.SqlConnection = Nothing

        If txtPantalla.Text.Length < 7 Then
            MsgBox("El número de documento ingresado no es válido, por favor corrobore.")
            txtPantalla.Text = ""
            Exit Sub
        End If

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            Dim param_dni As New SqlClient.SqlParameter
            param_dni.ParameterName = "@Dni"
            param_dni.SqlDbType = SqlDbType.BigInt
            param_dni.Value = txtPantalla.Text.Replace(",", "")
            param_dni.Direction = ParameterDirection.Input

            Dim param_empleado As New SqlClient.SqlParameter
            param_empleado.ParameterName = "@empleado"
            param_empleado.SqlDbType = SqlDbType.VarChar
            param_empleado.Size = 100
            param_empleado.Direction = ParameterDirection.Output

            Dim param_jornada As New SqlClient.SqlParameter
            param_jornada.ParameterName = "@jornada"
            param_jornada.SqlDbType = SqlDbType.VarChar
            param_jornada.Size = 100
            param_jornada.Direction = ParameterDirection.Output

            Dim param_fecha As New SqlClient.SqlParameter
            param_fecha.ParameterName = "@fecha"
            param_fecha.SqlDbType = SqlDbType.VarChar
            param_fecha.Size = 100
            param_fecha.Direction = ParameterDirection.Output

            Dim param_hora As New SqlClient.SqlParameter
            param_hora.ParameterName = "@hora"
            param_hora.SqlDbType = SqlDbType.VarChar
            param_hora.Size = 100
            param_hora.Direction = ParameterDirection.Output

            Dim param_mensaje As New SqlClient.SqlParameter
            param_mensaje.ParameterName = "@Mensaje"
            param_mensaje.SqlDbType = SqlDbType.VarChar
            param_mensaje.Size = 200
            param_mensaje.Direction = ParameterDirection.Output

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@Res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.Output

            Try

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[spAsistencias_Empleados_IngresoEgreso]", param_dni, _
                                          param_empleado, param_jornada, param_fecha, param_hora, param_mensaje, param_res)

                Select Case param_res.Value
                    Case -1
                        MsgBox("El documento ingresado no pertenece a un empleado.")
                        Exit Sub
                    Case -2
                        MsgBox("Se produjo un error el procesar el DNI del empleado.")
                        Exit Sub
                    Case -3
                        MsgBox("Usted ya registró su salida a las " & param_mensaje.Value & ".")
                        txtPantalla.Text = ""
                        Exit Sub
                End Select

                lblEmpleado.Text = param_empleado.Value
                lblJornada.Text = param_jornada.Value
                lblFecha.Text = param_fecha.Value
                lblHora.Text = param_hora.Value

                If param_mensaje.Value.ToString = "Ingreso" Then
                    lblMensaje.Text = "Ingreso Registrado - Bienvenid@"
                    lblTituloFecha.Text = "Fecha Ingreso:"
                    lblTituloHora.Text = "Hora Ingreso:"
                Else
                    lblMensaje.Text = "Salida Registrada - Disfrute su día"
                    lblTituloFecha.Text = "Fecha Egreso:"
                    lblTituloHora.Text = "Hora Egreso:"
                End If

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
              + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try

        tmrControlIngreso.Enabled = True

        pnlSocio.BringToFront()
        pnlPromocion.SendToBack()
        lblMensaje.Visible = True

        txtPantalla.Text = ""

    End Sub

#End Region

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

End Class