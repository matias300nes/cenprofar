Imports System.Windows.Forms
Imports System.Net
Imports Microsoft.ApplicationBlocks.Data

Public Class Login

    Private Veces As Integer
    Public Conexion As String

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        Dim SHA1 As String = vbNullString
        Dim tmp_ID As Long
        Dim tmp_nombre As String = vbNullString
        Dim tmp_ok As Boolean
        Dim tmp_vencida As Boolean
        Dim tmp_repetida As Boolean 'cp 13-10-2011
        ''Dim tmp_pass As String

        'dg 02/09/2011
        'Dim res As Integer
        Dim mensaje As String = ""
        Dim equipousado As String = ""
        Dim ipusado As String = ""


        If Util.pass_vencida Then
            'la clave tiene que ser distinta a la que tenia antes
            If Util.pass.Trim.ToUpper = txtpassword.Text.Trim.ToUpper Then
                MessageBox.Show("Las contraseña debe ser distinta de la contraseña original.", "Cambio de Contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtpassword.Focus()
                Return
            Else
                If txtpassword.Text.Length < 6 Or txtpassword.Text.Length > 14 Then
                    MessageBox.Show("Las contraseña debe tener un mínimo de 6 caracteres y un máximo de 14", "Cambio de Contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtpassword.Focus()
                    Return
                Else
                    If txtpassword.Text.Trim.ToUpper = txtpasswordConf.Text.Trim.ToUpper Then
                        'actualizar la nueva clave
                        SHA1 = Util.generarClaveSHA1(txtpassword.Text)
                        Util.cambiarContrasena(Util.UserID, SHA1, ConnStringSEI)
                        Util.Logueado_OK = True
                        CERRAR_SESIONES = True
                        Me.Dispose()
                        Return
                    Else
                        MessageBox.Show("Las contraseñas escritas no son identicas", "Cambio de Contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtpassword.Focus()
                        Return
                    End If
                End If
            End If
        ElseIf Util.pass_repetida Then
            'la clave tiene que ser distinta a la que tenia antes
            If Util.pass.Trim.ToUpper = txtpassword.Text.Trim.ToUpper Then
                MessageBox.Show("Las contraseña debe ser distinta de la contraseña original.", "Cambio de Contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtpassword.Focus()
                Return
            Else
                If txtpassword.Text.Length < 6 Or txtpassword.Text.Length > 14 Then
                    MessageBox.Show("Las contraseña debe tener un mínimo de 6 caracteres y un máximo de 14", "Cambio de Contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtpassword.Focus()
                    Return
                Else
                    If txtpassword.Text.Trim.ToUpper = txtpasswordConf.Text.Trim.ToUpper Then
                        'actualizar la nueva clave
                        SHA1 = Util.generarClaveSHA1(txtpassword.Text)

                        Util.cambiarContrasena(Util.UserID, SHA1, ConnStringSEI)

                        Util.Logueado_OK = True
                        CERRAR_SESIONES = True
                        Me.Dispose()
                        Return
                    Else
                        MessageBox.Show("Las contraseñas escritas no son identicas", "Cambio de Contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtpassword.Focus()
                        Return
                    End If
                End If
            End If
        Else
            PasswordLabel.Text = "&Contraseña"
            Label2.Visible = False
            txtpasswordConf.Visible = False
            OK.Text = "&Ingresar"
        End If



        'Verificar que no falte ningún dato
        If txtuser.Text.Trim = "" Then
            MessageBox.Show("Debe completar el nombre de usuario", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtuser.Focus()
            Return
        Else
            If txtpassword.Text.Trim = "" Then
                MessageBox.Show("Debe completar la contraseña", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtpassword.Focus()
                Return
            Else
                If txtDominio.Text.Trim = "" And CheckBox1.Checked Then
                    MessageBox.Show("Debe completar el dominio", "Dominio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtDominio.Focus()
                    Return
                End If
            End If
        End If

        '
        ' Logon con windows
        '
        If CheckBox1.Checked Then
            '
            ' Ejecutar el logeo
            '
            Util.Logueado_OK = Util.Logear(txtuser.Text.Trim, txtDominio.Text.Trim, txtpassword.Text.Trim)
        Else
            '
            ' Logon con el sistema
            '
            ' Encriptar la clave para buscarla
            SHA1 = Util.generarClaveSHA1(txtpassword.Text)

            'INICIALIZAR CONEXIONES SEGUN INFO DEL INI DEL USUARIO
            'Inicia_Conexion()

            ' Comprobar la clave en la base de datos
            'Comentado CP 28-1-2011 Util.Logueado_OK = Util.comprobarUsuario(txtuser.Text.ToLower.Trim, SHA1)
            'If My.Application.Info.AssemblyName.ToString = "SEI" Or My.Application.Info.AssemblyName.ToString = "CAMELIAS" Or My.Application.Info.AssemblyName.ToString = "SAMBA" _
            '     Or My.Application.Info.AssemblyName.ToString = "ROCHA" Or My.Application.Info.AssemblyName.ToString = "BIANCO" Or My.Application.Info.AssemblyName.ToString = "PORKYS" _
            '     Or My.Application.Info.AssemblyName.ToString = "MAGENTA" Then
            'Else
            If My.Application.Info.AssemblyName.ToString = "TURNOS" Then
                Util.Logueado_OK = Util.comprobarUsuario2(tmp_ID, txtuser.Text.ToLower.Trim, txtpassword.Text, SHA1, tmp_ok, tmp_nombre, tmp_vencida, tmp_repetida, ConnStringCLINICA)
            Else
                If My.Application.Info.AssemblyName.ToString = "MIT" Then
                    Util.Logueado_OK = Util.comprobarUsuario2(tmp_ID, txtuser.Text.ToLower.Trim, txtpassword.Text, SHA1, tmp_ok, tmp_nombre, tmp_vencida, tmp_repetida, ConnStringMIT)
                Else
                    If My.Application.Info.AssemblyName.ToString = "CARTOCOR_PERSONAL" Then
                        Util.Logueado_OK = Util.comprobarUsuario2(tmp_ID, txtuser.Text.ToLower.Trim, txtpassword.Text, SHA1, tmp_ok, tmp_nombre, tmp_vencida, tmp_repetida, ConnStringHOLLMAN)
                    Else
                        Util.Logueado_OK = Util.comprobarUsuario2(tmp_ID, txtuser.Text.ToLower.Trim, txtpassword.Text, SHA1, tmp_ok, tmp_nombre, tmp_vencida, tmp_repetida, ConnStringSEI)
                    End If
                End If
            End If
            'End If
        End If

        '
        ' Ver si logueo correctamente
        '
        If Util.Logueado_OK Then
            UserActual = txtuser.Text.Trim
            EquipoActual = SystemInformation.ComputerName
            ' Comprobar la clave en la base de datos
            Util.UserID = tmp_ID
            Util.pass_vencida = tmp_vencida
            Util.pass_repetida = tmp_repetida
            Util.pass = txtpassword.Text
            If Util.UserID < 0 Then
                Veces = Veces + 1
                txtpassword.Text = ""
                txtpassword.Focus()
                If Veces >= 3 Then
                    MessageBox.Show("No puede ingresar al sistema", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Logueado_OK = False
                    Me.Dispose()
                Else
                    MessageBox.Show("Contraseña incorrecta. Usted a efectuado " & Veces.ToString & " intentos para ingresar, le quedan " & (3 - Veces).ToString & " mas.", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Logueado_OK = False
                    Return
                End If
                Return
            End If

            'nuevo cp 28-1-2010 verificar validez de contraseña
            If Util.pass_vencida = True And My.Application.Info.AssemblyName.ToString = "SEI" Then
                PasswordLabel.Text = "Nueva"
                txtpassword.Text = ""
                Label2.Text = "Repetir"
                Label2.Visible = True
                txtpasswordConf.Text = ""
                txtpasswordConf.Visible = True
                OK.Text = "Guardar"
                MessageBox.Show("Su contraseña está vencida, por favor ingrese una nueva", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtpassword.Focus()
                Util.Logueado_OK = False
                Return
            End If

            CERRAR_SESIONES = True
            CERRAR_IP = ipusado
            CERRAR_EQUIPO = equipousado
            CERRAR_APLICACION = Utiles.My.Application.Info.AssemblyName.ToUpper

            'Me.Dispose()
            Me.Hide()

        Else
            If tmp_ok = 0 Then
                MessageBox.Show("Usuario no autorizado, o inexistente, para usar el sistema", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return
            ElseIf tmp_ok = 2 Then
                MessageBox.Show("No puede ingresar al sistema", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Logueado_OK = False
                Me.Dispose()
            End If

            Veces = Veces + 1
            If Veces >= 3 Then
                MessageBox.Show("Usted a efectuado " & Veces.ToString & " intentos para ingresar, le quedan " & (3 - Veces).ToString & " mas.", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'PictureBox1.Visible = False
            'PictureBox2.Visible = True
            txtpassword.Text = ""
            txtpassword.Focus()
        End If

    End Sub

    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Util.Logueado_OK = False Then
            Application.Exit()
        End If
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim tmp As Long
        ''Dim pos As Integer
        Util.pass_vencida = False
        Util.pass_repetida = False
        'txtuser.Text = ""
        'txtpassword.Text = ""
        txtDominio.Text = ""

        'PictureBox1.Visible = True
        'PictureBox2.Visible = False

        path = Application.ExecutablePath.ToString
        path = Util.TruncarUltimaCarpeta(path)

        ' CP 22-05-2012
        ' Setear la Configuracion Regional y de idiomas
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

        Inicia_Conexion()

        'CompletarAutomaticamente()
        'If txtuser.Text = "cponte" Then
        '    txtpassword.Text = "febrero10"
        '    OK_Click(sender, e)
        'End If
        'CheckBox1.Checked = False

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            txtDominio.Enabled = True
            CompletarAutomaticamente()
            txtpassword.Focus()
        Else
            txtuser.Text = ""
            txtDominio.Text = ""
            txtDominio.Enabled = False
            txtuser.Focus()
        End If
    End Sub

    Private Sub CompletarAutomaticamente()
        Dim s() As String
        s = Split(GetUserID(), "\")
        Try
            txtDominio.Text = s(0)
            txtuser.Text = s(1)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub txtuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtuser.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtpassword.Focus()
        End If
    End Sub

    Private Sub txtpassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            If CheckBox1.Checked Then
                txtDominio.Focus()
            Else
                If txtpasswordConf.Visible = False Then
                    Me.OK_Click(sender, e)
                    'OK.Focus()
                Else
                    txtpasswordConf.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtDominio_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDominio.KeyDown
        If e.KeyCode = Keys.Enter Then
            OK.Focus()
        End If
    End Sub

    Private Sub txtpasswordConf_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpasswordConf.KeyDown
        If e.KeyCode = Keys.Enter Then
            OK.Focus()
        End If
    End Sub

    Private Sub txtpassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpassword.KeyPress
        txtpasswordConf.Text = ""
    End Sub

    Private Sub btnContrasena_Click(sender As Object, e As EventArgs) Handles btnContrasena.Click

        If txtuser.Text = "" Then
            MsgBox("Debe ingresar el nombre de Usuario (primer letra del nombre y apellido completo, sin espacios)", MsgBoxStyle.Information, "Control de Errores")
            txtuser.Focus()
            Exit Sub
        End If

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            Dim param_Usuario As New SqlClient.SqlParameter
            param_Usuario.ParameterName = "@Usuario"
            param_Usuario.SqlDbType = SqlDbType.VarChar
            param_Usuario.Size = 50
            param_Usuario.Value = txtuser.Text
            param_Usuario.Direction = ParameterDirection.Input

            Try

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spMail_ContrasenaSistema", param_Usuario)

                MsgBox("Se ha enviado un correo electrónico con la nueva contraseña. El sistema luego le pedirá que la cambie", MsgBoxStyle.Information)

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

            MessageBox.Show(String.Format("Se produjo un problema al procesar la información en la Base de Datos, por favor valide el siguiente mensaje de error: {0}" _
                + Environment.NewLine + "Si el problema persiste contáctese con MercedesIt a través del correo soporte@mercedesit.com", errMessage), _
                "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class
