'Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Windows.Forms

Imports GMap.NET
Imports GMap.NET.WindowsForms
Imports GMap.NET.GMaps
Imports GMap.NET.MapProviders
Imports GMap.NET.WindowsForms.Markers

Public Module compartida
    ''Definicion de la Cadena de Conexion de la Base de Datos

    Public pathrptcompartida As String = "C:\MIT\Rpt\"    'Usada para almacenar la ruta de los reportes

    Public dslstDetalles As DataSet   'Dataset usado para llenar las grillas de detalles
    Public SQL As String = ""         'Consulta SQL o nombre de store procedure pasado como string

    Public idgastovehiculo As Long = 0


    Public Valor_Busqueda As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda1 As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda2 As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda3 As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda4 As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda5 As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo
    Public Detalle_Busqueda6 As String = "" 'Variables para el manejo del formulario frmAyudaDatosPerfo

    'Public nbreformreportes As String   'VARIABLE PARA PSARLE EL NOMBRE DEL FORMULARIO AL REPROTE

    Public ayuda As String = "" 'variable que nos indica que ayuda esta activa en un momento dado en frmDatosPerforacion
    'la utilizo para que no explote el fitro de la ayuda ya que utilizo el mismo furmulario para diferentes ayudas
    Public SQLBuscarAux As String 'variable utilizada en ayuda activa en un momento dado en frmDatosPerforacion
    'es utilizada en el change del filtro de la ayuda

    Public objetosMapa As GMap.NET.WindowsForms.GMapOverlay

    'Public bordeMapa As GMap.NET.WindowsForms.Markers.GMapMarkerCross
    'Public marcaMapa As GMap.NET.WindowsForms.GMapMarker


    Public Sub ObtenerDatasetDetalles()
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            dslstDetalles = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, SQL)
            dslstDetalles.Dispose()

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

    End Sub

    Public Sub ControladorDeExepcionesReportes(ByRef ex As Exception, ByVal nbrereporte As String)

        If System.Runtime.InteropServices.Marshal.GetHRForException(ex) = -2147352565 Then
            MessageBox.Show("El Nombre del o los Parametro/s del Reporte " & nbrereporte & vbCrLf & "difiere del Parametro definido en el Sistema" & vbCrLf & "o no se ha definido el Campo Formula @filtradopor en el Reporte.", "Error", MessageBoxButtons.OK)
        Else
            ex.Message().ToString()
        End If
    End Sub

    Public Function ReverseString(ByRef inputStr As String) As String
        If String.IsNullOrEmpty(inputStr) Then
            Return String.Empty
        End If

        Dim output As Char() = inputStr.ToCharArray()

        Array.Reverse(output)

        Return New String(output)
    End Function

#Region "Llenas Combos Comunes"

    Public Sub LlenarcmbProvincia_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT IdProvincia, Provincia FROM Provincias")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Provincia"
                .ValueMember = "IdProvincia"
            End With

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

    End Sub

    Public Sub LlenarcmbLocalidad_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal idprovincia As Integer, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT IdLocalidad, Localidad FROM Localidades l " & _
                                        " JOIN Provincias p ON p.idprovincia = l.idprovincia " & _
                                        " WHERE l.idprovincia = " & idprovincia)
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Localidad"
                .ValueMember = "IdLocalidad"
            End With

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

    End Sub

    Public Sub LlenarcmbClientes_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByRef llenarcombo As Boolean = False)
        Dim ds_Clientes As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        llenarcombo = True

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenarcombo = False
            Exit Sub
        End Try

        Try
            ds_Clientes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre FROM Clientes WHERE Eliminado = 0 ORDER BY nombre")
            ds_Clientes.Dispose()

            With cmb
                .DataSource = ds_Clientes.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .TabStop = True
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenarcombo = False

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

        llenarcombo = False

    End Sub

    Public Sub LlenarcmbClientesParticulares_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select IdCliente, (Apellido + ', ' + Nombre) as Cliente From Clientes Where tipocliente = 0 AND eliminado = 0 ORDER BY Apellido, Nombre")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Cliente"
                .ValueMember = "IdCliente"
            End With

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

    End Sub

    Public Sub LlenarcmbMarcas_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByRef llenarcombo As Boolean = False)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        llenarcombo = True

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenarcombo = False
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select Id, Nombre From Marcas ORDER BY Nombre")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Id"
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenarcombo = False

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

        llenarcombo = False

    End Sub

    Public Sub LlenarcmbTiposPresupuestos_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select IdtipoPresupuesto, TipoPresupuesto From TiposPresupuestos")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "TipoPresupuesto"
                .ValueMember = "IdtipoPresupuesto"
            End With

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

    End Sub

    Public Sub LlenarcmbMaquinas_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select Idmaquina, nombre From Maquinas Where eliminado = 0 ORDER BY nombre")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "idmaquina"
            End With

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

    End Sub

    Public Sub LlenarEmpleados_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select IdEmpleado, (nombre + ', ' + apellido) as 'Empleado' From Empleados Where eliminado = 0 ORDER BY Empleado")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Empleado"
                .ValueMember = "idempleado"
            End With

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

    End Sub

    Public Sub LlenarcmbEmpleados2_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select Id, (nombre + ', ' + apellido) as 'Empleado' From Empleados Where eliminado = 0 ORDER BY Empleado")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Empleado"
                .ValueMember = "id"
            End With

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

    End Sub

    Public Sub LlenarcmbEmpleados3_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByVal sql As String = "")
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            If sql = "" Then
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select Id, (Nombre  + ', ' + Apellido) as 'Empleado' From Empleados Where eliminado = 0 ORDER BY Empleado")
                ds.Dispose()
            Else
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, sql)
                ds.Dispose()
            End If


            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Empleado"
                .ValueMember = "id"
            End With

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

    End Sub

    Public Sub LlenarcmbBombas(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Idbomba, (CodigoBomba + ' - ' + PotenciaBomba + ' - ' + TensionBomba) as Descripcion " & _
                                        " FROM Bombas b WHERE Eliminado = 0" & _
                                        " ORDER BY TENSIONBOMBA, CODIGOBOMBA, POTENCIABOMBA")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "IdBomba"
            End With

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

    End Sub

    Public Sub LlenarcmbBancos_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT DISTINCT BANCO FROM CHEQUES ORDER BY BANCO")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "banco"
                '.ValueMember = "id"
            End With

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
    End Sub

    Public Sub LlenarcmbMonedas_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim dsMoneda As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            dsMoneda = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Id, Nombre FROM Monedas")
            dsMoneda.Dispose()

            With cmb
                .DataSource = dsMoneda.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Id"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                .SelectedIndex = 0
            End With

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

    End Sub

    Public Sub LlenarcmbMoneda_2(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, (Nombre + ' - ' + Convert(VARCHAR(10), ValorCambio)) as Moneda FROM MONEDAS")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "moneda"
                .ValueMember = "id"
            End With

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

    End Sub

    Public Sub LlenarcmbTarjetas_App(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT distinct NombreTarjeta FROM Ingresos_Detalles ORDER BY NombreTarjeta")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "NombreTarjeta"
                '.ValueMember = "idtipomoneda"
            End With

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
    End Sub

    Public Sub LlenarcmbSuelos(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT IdTipoSuelo, TipoSuelo FROM TipoSuelos WHERE Eliminado = 0 ORDER BY TipoSuelo")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "TipoSuelo"
                .ValueMember = "IdTipoSuelo"
            End With

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

    End Sub

    Public Sub LlenarcmbTipoGastosGrales(ByVal cmb As DevComponents.DotNetBar.Controls.ComboBoxEx, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select IdGastoCte, Nombre From GastosCorrientes Where eliminado = 0 order by nombre")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "IdGastoCte"
            End With

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

    End Sub

    Public Sub LlenarcmbProveedores_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, ByVal servicio As Integer, ByVal flete As Integer, Optional ByRef llenarcombo As Boolean = False)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        llenarcombo = True

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenarcombo = False
            Exit Sub
        End Try

        Try
            'If flete = 1 Then
            '    ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Id, Nombre FROM Proveedores WHERE Eliminado = 0 AND flete = " & flete & " ORDER BY NOMBRE")
            'Else
            '    ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Id, Nombre FROM Proveedores WHERE Eliminado = 0 AND deservicio = " & servicio & " AND flete = " & flete & "  ORDER BY Nombre")
            'End If

            'ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, ltrim(rtrim(Nombre)) as Nombre FROM Proveedores WHERE TIPODOCUMENTO IS NOT NULL AND CUIT < 99999900000 AND LEN(CUIT) = 11 AND Eliminado = 0 ORDER BY NOMBRE")
            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Id, ltrim(rtrim(Nombre)) as Nombre FROM Proveedores WHERE TIPODOCUMENTO IS NOT NULL AND CUIT < 99999900000 AND LEN(CUIT) = 11 AND Eliminado = 0 ORDER BY NOMBRE")

            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "id"
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenarcombo = False

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

        llenarcombo = False

    End Sub

    Public Sub LlenarcmbUnidades_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByRef llenarcombo As Boolean = False)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        llenarcombo = True

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenarcombo = False
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, (codigo + ' - ' + nombre) as codigo FROM Unidades WHERE Eliminado = 0 ORDER BY codigo")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenarcombo = False

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

        llenarcombo = False

    End Sub

    Public Sub LlenarcmbFamilias_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        'DevComponents.DotNetBar.Controls.ComboBoxEx
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, (codigo + ' - ' + nombre) codigo FROM Familias WHERE Eliminado = 0 ORDER BY codigo")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
            End With

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

    End Sub

    Public Sub LlenarcmbColores(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT distinct color FROM Materiales ORDER BY color")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "color"
                '.ValueMember = "id"
            End With

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

    End Sub

    Public Sub LlenarcmbEmbalaje(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT distinct tipoembalaje FROM Materiales ORDER BY tipoembalaje")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "tipoembalaje"
                '.ValueMember = "id"
            End With

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

    End Sub

    Public Sub LlenarcmbObras(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByVal idcliente As Integer = 0)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            If idcliente = 0 Then
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, (CODIGO + ' - ' +  NOMBRE) AS CODIGO FROM OBRAS WHERE FINALIZADO = 0 ORDER BY codigo")
                ds.Dispose()
            Else
                ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, (CODIGO + ' - ' +  NOMBRE) AS CODIGO FROM OBRAS WHERE IDCliente = " & idcliente & " AND FINALIZADO = 0 ORDER BY codigo")
                ds.Dispose()
            End If

            cmb.Text = ""

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .TabStop = True
            End With

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

    End Sub

    Public Sub LlenarcmbRetira(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, apellido + ' ' + nombre + ' (' + codigo + ')'  as codigo FROM Usuarios WHERE Eliminado = 0 and Tipo in ('0','1') ORDER BY codigo")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
                .TabStop = True
            End With

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

    End Sub

    Public Sub LlenarcmbCondicionDePago_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre as codigo FROM CONDICIONdepago WHERE Eliminado = 0 ORDER BY NOMBRE ASC")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DropDownStyle = ComboBoxStyle.DropDown
            End With

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

    End Sub

    Public Sub LlenarcmbProductos(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByRef llenarcombo As Boolean = False)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        llenarcombo = True

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            llenarcombo = False
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT id, nombre as codigo FROM Productos WHERE Eliminado = 0 ORDER BY codigo")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "codigo"
                .ValueMember = "id"
            End With

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenarcombo = False

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

        llenarcombo = False

    End Sub

    Public Sub LlenarcmbAlmacenes(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String, Optional ByRef llenarcombo As Boolean = False)
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Almacenes As Data.DataSet

        llenarcombo = True

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            llenarcombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Almacenes = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Id, (codigo + ' - ' +  rtrim(nombre)) as Nombre FROM Almacenes WHERE Eliminado = 0 ORDER BY nombre")
            ds_Almacenes.Dispose()

            If ds_Almacenes.Tables(0).Rows.Count > 0 Then
                With cmb
                    .DataSource = ds_Almacenes.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .ValueMember = "id"
                    .AutoCompleteMode = AutoCompleteMode.Suggest
                    .AutoCompleteSource = AutoCompleteSource.ListItems
                    .DropDownStyle = ComboBoxStyle.DropDownList
                    .SelectedIndex = 0
                End With
            End If

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenarcombo = False

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

        llenarcombo = False

    End Sub

    Public Sub LlenarcmbTipoFacturas_APP(ByVal cmbTipoComprobante As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ltrim(rtrim(Codigo)) as Codigo, ltrim(rtrim(Descripcion)) as Descripcion FROM Comprobantes WHERE Habilitado = 1")
            ds.Dispose()

            With cmbTipoComprobante
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                .SelectedIndex = 0
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

    Public Sub LlenarcmbConceptos_APP(ByVal cmbConceptos As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, UPPER(Descripcion) AS Descripcion FROM Conceptos WHERE Habilitado = 1")
            ds.Dispose()

            With cmbConceptos
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                .SelectedIndex = 0
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

    Public Sub LlenarcmbTipoDocumento_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String)
        Dim ds As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ltrim(rtrim(Codigo)) as Codigo, ltrim(rtrim(Descripcion)) as Descripcion FROM TipoDocumento WHERE Habilitado = 1")
            ds.Dispose()

            With cmb
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                '.AutoCompleteSource = AutoCompleteSource.ListItems
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

#End Region

    Public Function PeriodoCorrecto(ByVal periodo As String) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 6
                param_periodo.Value = periodo
                param_periodo.Direction = ParameterDirection.Input


                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Bit
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPeriodoCorrecto", param_periodo, param_res)
                    res = param_res.Value
                    ''If res = 1 Then
                    ''    ''AgregarGrilla(grd, Me, Permitir)
                    ''    ''modo = False
                    ''End If
                    PeriodoCorrecto = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Function PeriodoEmpleadoYaCargado(ByVal periodo As String, ByVal idempleado As Integer) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 6
                param_periodo.Value = periodo
                param_periodo.Direction = ParameterDirection.Input

                Dim param_idempleado As New SqlClient.SqlParameter
                param_idempleado.ParameterName = "@idempleado"
                param_idempleado.SqlDbType = SqlDbType.Int
                param_idempleado.Value = idempleado
                param_idempleado.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Bit
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPeriodoEmpleadoYaCargado", param_periodo, param_idempleado, param_res)
                    res = param_res.Value
                    PeriodoEmpleadoYaCargado = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Function PeriodoYaCargadoGastosEnGral(ByVal periodo As String) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 6
                param_periodo.Value = periodo
                param_periodo.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Bit
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPeriodoYaCargadoGastosEnGral", param_periodo, param_res)
                    res = param_res.Value
                    PeriodoYaCargadoGastosEnGral = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Function PeriodoYaCargadoSeguros(ByVal periodo As String) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 6
                param_periodo.Value = periodo
                param_periodo.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Bit
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPeriodoYaCargadoSeguros", param_periodo, param_res)
                    res = param_res.Value
                    PeriodoYaCargadoSeguros = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Function PeriodoYaCargadoFlota(ByVal periodo As String) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_periodo As New SqlClient.SqlParameter
                param_periodo.ParameterName = "@periodo"
                param_periodo.SqlDbType = SqlDbType.VarChar
                param_periodo.Size = 6
                param_periodo.Value = periodo
                param_periodo.Direction = ParameterDirection.Input

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Bit
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spPeriodoYaCargadoFlota", param_periodo, param_res)
                    res = param_res.Value
                    PeriodoYaCargadoFlota = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Function TarjetaVerdeYaAsignada(ByVal nrotarjetaverde As String) As Boolean
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_nrotarjetaverde As New SqlClient.SqlParameter
                param_nrotarjetaverde.ParameterName = "@nrotarjetaverde"
                param_nrotarjetaverde.SqlDbType = SqlDbType.VarChar
                param_nrotarjetaverde.Size = 15
                param_nrotarjetaverde.Value = nrotarjetaverde
                param_nrotarjetaverde.Direction = ParameterDirection.Input


                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@res"
                param_res.SqlDbType = SqlDbType.Bit
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spTarjetaVerdeYaAsignada", param_nrotarjetaverde, param_res)
                    res = param_res.Value
                    ''If res = 1 Then
                    ''    ''AgregarGrilla(grd, Me, Permitir)
                    ''    ''modo = False
                    ''End If
                    TarjetaVerdeYaAsignada = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Function ObtenerProxValorCampoIdentidadTablaEgresos() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim res As Integer = 0
        Try
            Try
                connection = SqlHelper.GetConnection(ConnStringPERFO)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            Try

                Dim param_res As New SqlClient.SqlParameter
                param_res.ParameterName = "@proxidegreso"
                param_res.SqlDbType = SqlDbType.Int
                param_res.Value = 0
                param_res.Direction = ParameterDirection.InputOutput

                Try
                    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spObtenerProxValorCampoIdentidadTablaEgresos", param_res)
                    res = param_res.Value
                    ObtenerProxValorCampoIdentidadTablaEgresos = res
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
            MessageBox.Show(String.Format("Se produjo el siguiente error al intentar realizar la operación: " _
              + ex.Message + Environment.NewLine + ". POR FAVOR, COPIE LA PANTALLA Y ENVIELA AL DPTO. DE SISTEMAS.", errMessage), _
              "Error en la Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not connection Is Nothing Then
                CType(connection, IDisposable).Dispose()
            End If
        End Try
    End Function

    Public Sub LlenarcmbSucursales_App(ByVal cmb As System.Windows.Forms.ComboBox, ByVal cnn As String) ' Optional ByVal Eliminado As Boolean = True)
        Dim ds_Cli As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            'If Eliminado = True Then 'si está activo, indica que vamos a controlar y mostrar solo los que NO están eliminados
            ' ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select numero, nombre FROM Sucursales where numero > 0 and Eliminado = 0 order by nombre")
            ' Else
            ds_Cli = SqlHelper.ExecuteDataset(connection, CommandType.Text, "select numero, nombre FROM Sucursales where numero > 0 order by nombre")
            'End If

            ds_Cli.Dispose()

            With cmb
                .DataSource = ds_Cli.Tables(0).DefaultView
                .DisplayMember = "nombre"
                .ValueMember = "numero"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
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


#Region "Manejo del Mapa"

    Public Sub CargarMapa(ByRef map As Object)

        GMapProvider.Language = LanguageType.Spanish

        map.Manager.UseGeocoderCache = True
        map.Manager.UsePlacemarkCache = True

        map.Manager.Mode = AccessMode.ServerAndCache

        map.MapProvider = GMapProviders.GoogleMap
        'map.SetCurrentPositionByKeywords("Buenos Aires, Argentina")
        map.Position = New PointLatLng(-33.6758095916858, -65.4633557796478)
        map.MaxZoom = 18
        map.MinZoom = 13
        map.Zoom = 15

        'Try
        objetosMapa = New GMapOverlay("objects")
        map.Overlays.Add(objetosMapa)
        '    'Dim pos As GMap.NET.PointLatLng
        '    Dim marcaPerforacion As GMap.NET.WindowsForms.GMapMarker

        '    'MainMap.Position = New PointLatLng(Lat, Lng)
        '    'MainMap.Refresh()

        '    'pos.Lat = Lat
        '    'pos.Lng = Lng
        '    marcaPerforacion = New WindowsForms.Markers.GMapMarkerGoogleGreen(map.Position)
        '    'marcaPerforacion.ToolTipText = tipo & " - " & Cliente & " - " & Mts & " mts."
        '    objetosMapa.Markers.Add(marcaPerforacion)
        '    map.Position = marcaPerforacion.Position
        map.Refresh()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub

    'Public Sub UbicacionPerforacion_Mapa(ByVal Lat As Double, ByVal Lng As Double, ByRef frm As Object, ByVal Cliente As String, ByVal Mts As String, ByVal tipo As String)

    '    Dim pos As GMap.NET.PointLatLng

    '    objetosMapa.Markers.Clear()

    '    frm.mainmap.CurrentPosition = New PointLatLng(Lat, Lng)
    '    frm.mainmap.ReloadMap()

    '    pos.Lat = Lat
    '    pos.Lng = Lng
    '    marcaMapa = New WindowsForms.Markers.GMapMarkerGoogleGreen(pos)
    '    marcaMapa.ToolTipText = tipo & " - " & Cliente & " - " & Mts & " mts."
    '    objetosMapa.Markers.Add(marcaMapa)

    'End Sub

    'Public Sub CargarPerforaciones(ByVal Lat As Double, ByVal Lng As Double, ByRef Map As Object, ByVal Cliente As String, ByVal Mts As String, ByVal tipo As String, ByRef obj As GMapOverlay)

    '    Dim pos As GMap.NET.PointLatLng
    '    Dim marcaPerforacion As GMap.NET.WindowsForms.GMapMarker

    '    Map.Position = New PointLatLng(Lat, Lng)

    '    'Map.refresh()

    '    pos.Lat = Lat
    '    pos.Lng = Lng
    '    marcaPerforacion = New WindowsForms.Markers.GMapMarkerGoogleGreen(Map.position)
    '    'marcaPerforacion.ToolTipText = tipo & " - " & Cliente & " - " & Mts & " mts."
    '    obj.Markers.Add(marcaPerforacion)
    '    Map.position = marcaPerforacion.Position
    '    Map.refresh()
    'End Sub

    Public Sub CambiarMarcador(ByVal Lat As Double, ByVal Lng As Double, ByRef frm As Object, ByVal item As GMap.NET.WindowsForms.GMapMarker, ByVal Cliente As String, ByVal Mts As String, ByVal tipo As String)
        objetosMapa.Markers.Remove(item)
        Dim pos As GMap.NET.PointLatLng
        Dim marcaPerforacion As GMap.NET.WindowsForms.GMapMarker

        pos.Lat = Lat
        pos.Lng = Lng
        marcaPerforacion = New GMarkerGoogle(pos, GMarkerGoogleType.red)
        marcaPerforacion.ToolTipText = tipo
        objetosMapa.Markers.Add(marcaPerforacion)
    End Sub

    Public Sub Zoom(ByRef frm As Object)
        frm.Mainmap.Zoom = frm.barrazoom.Value + 13
        frm.Mainmap.ReloadMap()
    End Sub

#End Region


    Public Function Letras(ByVal numero As String) As String
        '********Declara variables de tipo cadena************
        Dim palabras, entero, dec, flag As String

        entero = Nothing
        dec = Nothing
        palabras = Nothing

        '********Declara variables de tipo entero***********
        Dim num, x, y As Integer

        flag = "N"

        '**********Número Negativo***********
        If Mid(numero, 1, 1) = "-" Then
            numero = Mid(numero, 2, numero.ToString.Length - 1).ToString
            palabras = "menos "
        End If

        '**********Si tiene ceros a la izquierda*************
        For x = 1 To numero.ToString.Length
            If Mid(numero, 1, 1) = "0" Then
                numero = Trim(Mid(numero, 2, numero.ToString.Length).ToString)
                If Trim(numero.ToString.Length) = 0 Then palabras = ""
            Else
                Exit For
            End If
        Next

        '*********Dividir parte entera y decimal************
        For y = 1 To Len(numero)
            If Mid(numero, y, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, y, 1)
                Else
                    dec = dec + Mid(numero, y, 1)
                End If
            End If
        Next y

        If Len(dec) = 1 Then dec = dec & "0"

        '**********proceso de conversión***********
        flag = "N"

        If Val(numero) <= 999999999 Then
            For y = Len(entero) To 1 Step -1
                num = Len(entero) - (y - 1)
                Select Case y
                    Case 3, 6, 9
                        '**********Asigna las palabras para las centenas***********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" And Mid(entero, num + 2, 1) = "0" Then
                                    palabras = palabras & "cien "
                                Else
                                    palabras = palabras & "ciento "
                                End If
                            Case "2"
                                palabras = palabras & "doscientos "
                            Case "3"
                                palabras = palabras & "trescientos "
                            Case "4"
                                palabras = palabras & "cuatrocientos "
                            Case "5"
                                palabras = palabras & "quinientos "
                            Case "6"
                                palabras = palabras & "seiscientos "
                            Case "7"
                                palabras = palabras & "setecientos "
                            Case "8"
                                palabras = palabras & "ochocientos "
                            Case "9"
                                palabras = palabras & "novecientos "
                        End Select
                    Case 2, 5, 8
                        '*********Asigna las palabras para las decenas************
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    flag = "S"
                                    palabras = palabras & "diez "
                                End If
                                If Mid(entero, num + 1, 1) = "1" Then
                                    flag = "S"
                                    palabras = palabras & "once "
                                End If
                                If Mid(entero, num + 1, 1) = "2" Then
                                    flag = "S"
                                    palabras = palabras & "doce "
                                End If
                                If Mid(entero, num + 1, 1) = "3" Then
                                    flag = "S"
                                    palabras = palabras & "trece "
                                End If
                                If Mid(entero, num + 1, 1) = "4" Then
                                    flag = "S"
                                    palabras = palabras & "catorce "
                                End If
                                If Mid(entero, num + 1, 1) = "5" Then
                                    flag = "S"
                                    palabras = palabras & "quince "
                                End If
                                If Mid(entero, num + 1, 1) > "5" Then
                                    flag = "N"
                                    palabras = palabras & "dieci"
                                End If
                            Case "2"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "veinte "
                                    flag = "S"
                                Else
                                    palabras = palabras & "veinti"
                                    flag = "N"
                                End If
                            Case "3"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "treinta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "treinta y "
                                    flag = "N"
                                End If
                            Case "4"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cuarenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cuarenta y "
                                    flag = "N"
                                End If
                            Case "5"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cincuenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cincuenta y "
                                    flag = "N"
                                End If
                            Case "6"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "sesenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "sesenta y "
                                    flag = "N"
                                End If
                            Case "7"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "setenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "setenta y "
                                    flag = "N"
                                End If
                            Case "8"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "ochenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "ochenta y "
                                    flag = "N"
                                End If
                            Case "9"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "noventa "
                                    flag = "S"
                                Else
                                    palabras = palabras & "noventa y "
                                    flag = "N"
                                End If
                        End Select
                    Case 1, 4, 7
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If flag = "N" Then
                                    If y = 1 Then
                                        palabras = palabras & "uno "
                                    Else
                                        palabras = palabras & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then palabras = palabras & "dos "
                            Case "3"
                                If flag = "N" Then palabras = palabras & "tres "
                            Case "4"
                                If flag = "N" Then palabras = palabras & "cuatro "
                            Case "5"
                                If flag = "N" Then palabras = palabras & "cinco "
                            Case "6"
                                If flag = "N" Then palabras = palabras & "seis "
                            Case "7"
                                If flag = "N" Then palabras = palabras & "siete "
                            Case "8"
                                If flag = "N" Then palabras = palabras & "ocho "
                            Case "9"
                                If flag = "N" Then palabras = palabras & "nueve "
                        End Select
                End Select

                '***********Asigna la palabra mil***************
                If y = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or _
                    (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And _
                    Len(entero) <= 6) Then palabras = palabras & "mil "
                End If

                '**********Asigna la palabra millón*************
                If y = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        palabras = palabras & "millón "
                    Else
                        palabras = palabras & "millones "
                    End If
                End If
            Next y

            '**********Une la parte entera y la parte decimal*************
            If dec <> "" Then
                Letras = palabras & "con " & dec & " centavos"
            Else
                Letras = palabras
            End If
        Else
            Letras = ""
        End If
    End Function

#Region "SEI"



#End Region

    Public Function BuscarDiferenciaCambio(ByVal cnn As String) As Double
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(cnn)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "Select ValorCambio - ISNULL(DiferenciaCambio,0)/100 * ValorCambio From Parametros Cross JOIN Monedas WHERE Codigo = 'Do'")
            ds.Dispose()

            BuscarDiferenciaCambio = ds.Tables(0).Rows(0).Item(0)

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

    Public Function DigitoVerificador(ByVal Numero As String) As String

        Dim i As Integer, par As Integer = 0, impar As Integer = 0, sum As Integer

        For i = 0 To Numero.Length - 1
            If (i + 1) Mod 2 <> 0 Then
                ' MsgBox(Numero.ToString.Substring(i, 1))
                impar = impar + CInt(Numero.ToString.Substring(i, 1))
            Else
                par = par + CInt(Numero.ToString.Substring(i, 1))
                'impar = impar + CInt(Numero.IndexOf(i))
            End If
        Next

        impar = impar * 3
        sum = impar + par

        For i = 0 To 9
            If (sum + i) Mod 10 = 0 Then
                Return Numero + i.ToString 'Result:= IntToStr(i); (si queres solo el díg.)
            End If
        Next

        Return Nothing

    End Function

End Module
