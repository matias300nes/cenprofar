Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports ReportesNet

Public Class frmUsuarioModo

    Dim connection As SqlClient.SqlConnection = Nothing
    Dim ds As Data.DataSet
    Public Formulario As String

    Private Sub frmUsuarioModo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        llenarcmbAutorizados()
        txtPassword.Focus()

    End Sub

    Private Sub cmbAutorizados_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAutorizados.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub llenarcmbAutorizados()


        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT Codigo , CONCAT(Apellido ,' ', Nombre) AS 'Usuario' FROM Empleados WHERE Eliminado = 0 and Autoriza = 1 ORDER BY Usuario")
            ds.Dispose()

            With cmbAutorizados
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Usuario"
                .ValueMember = "Codigo"
            End With

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
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        Dim SHA1 As String = vbNullString
        Dim ds_Autorizado As Data.DataSet
        If e.KeyData = Keys.Enter Then

            If txtPassword.Text.Trim = "" Then
                MessageBox.Show("Debe completar la contraseña", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPassword.Focus()
                Return
            End If

            'comvierto la contraseña
            SHA1 = Util.generarClaveSHA1(txtPassword.Text)

            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try


            ds_Autorizado = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id FROM Empleados WHERE Codigo = '" & cmbAutorizados.SelectedValue & "' and Pass = '" & SHA1 & "'")
            ds_Autorizado.Dispose()

            If ds_Autorizado.Tables(0).Rows.Count = 1 Then
                MDIPrincipal.IDEmpleadoAutoriza = cmbAutorizados.SelectedValue.ToString
                MDIPrincipal.Autorizar = True
                Me.Close()
            Else
                MessageBox.Show("Usuorio o contraseña es incorrecta. Por favor verifique", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPassword.Focus()
            End If

          


        End If

    End Sub

   




    'Private Sub btnLectura_Click(sender As Object, e As EventArgs)

    '    Me.Close()

    '    Select Case Formulario
    '        Case "Ajustes"
    '            frmAjustes.grdItems.ReadOnly = True
    '            frmAjustes.MdiParent = MDIPrincipal
    '            frmAjustes.Show()
    '        Case "Productos"
    '            frmMateriales.btnNuevo.Visible = False
    '            frmMateriales.btnGuardar.Visible = False
    '            frmMateriales.btnEliminar.Visible = False
    '            frmMateriales.btnCancelar.Visible = False
    '            frmMateriales.btnActivar.Visible = False
    '            frmMateriales.btnCargaContinua.Enabled = False
    '            frmMateriales.MdiParent = MDIPrincipal
    '            frmMateriales.Show()
    '    End Select

    'End Sub

    'Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    txtPassword.Visible = True
    '    txtPassword.Focus()

    'End Sub

End Class