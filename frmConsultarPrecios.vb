Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmConsultarPrecios
    'Dim bolpoliticas As Boolean

    'Private RefrescarGrid As Boolean
    'Private ds_2 As DataSet

    'Dim editando_celda As Boolean

    ''Para el clic derecho sobre la grilla de materiales
    'Dim Cell_X As Integer
    'Dim Cell_Y As Integer

    ''Varible de transaccion
    'Dim tran As SqlClient.SqlTransaction
    'Dim conn_del_form As SqlClient.SqlConnection = Nothing


    'Dim permitir_evento_CellChanged As Boolean


    Dim connection As SqlClient.SqlConnection = Nothing
    Dim ds_producto As Data.DataSet
    Dim seleccionado As Boolean


#Region "Procedimientos Formularios"

    Private Sub frmConsultarPrecios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        configurarform()

        ToolMenu.Visible = False
        lstProductos.Visible = False
        lblPrecioMinorista.Text = "$0.00"
        lblPrecioMayorista.Text = "$0.00"
        txtProducto.Focus()

    End Sub

    Private Sub txtProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProducto.KeyDown

        If e.KeyCode = Keys.Down Then
            If lstProductos.Height > 4 And lstProductos.Visible = True Then
                'hago foco en la lista de tarjetas
                lstProductos.Focus()
            End If
        End If

    End Sub

    Private Sub txtProducto_KeyUp(sender As Object, e As KeyEventArgs) Handles txtProducto.KeyUp
        seleccionado = False
    End Sub

    Private Sub txtProducto_TextChanged(sender As Object, e As EventArgs) Handles txtProducto.TextChanged

        If txtProducto.Text <> "" And txtProducto.Text <> " " And seleccionado = False Then
            lstProductos.Visible = True
            LlenarLista_Productos()
        Else
            lstProductos.Visible = False
            lblPrecioMinorista.Visible = False
            lblPrecioMayorista.Visible = False
            lblPMay.Visible = False
            lblPMin.Visible = False
            lblPrecioMinorista.Text = "$0.00"
            lblPrecioMayorista.Text = "$0.00"
            PictureBoxPorkys.Visible = True
        End If
    End Sub

    Private Sub lstProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles lstProductos.KeyDown

        If e.KeyCode = Keys.Enter Then

            seleccionado = True
            txtProducto.Text = lstProductos.Text
            'Try
            '    connection = SqlHelper.GetConnection(ConnStringSEI)
            'Catch ex As Exception
            '    MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try
            Try
                'me fijo de donde viene la consulta
                ds_producto = SqlHelper.ExecuteDataset(connection, CommandType.Text, " Select PrecioCosto,PrecioMayorista from Materiales where Codigo = " & lstProductos.SelectedValue)
                ds_producto.Dispose()

                If ds_producto.Tables(0).Rows.Count = 0 Then
                    lblPrecioMinorista.Text = "$0.00"
                    lblPrecioMayorista.Text = "$0.00"
                    Exit Sub
                End If

                'paso el precio
                lblPrecioMinorista.Text = "$" + ds_producto.Tables(0).Rows(0).Item(0).ToString
                lblPrecioMayorista.Text = "$" + ds_producto.Tables(0).Rows(0).Item(1).ToString
                lblPrecioMinorista.Visible = True
                lblPrecioMayorista.Visible = True
                lblPMay.Visible = True
                lblPMin.Visible = True
                PictureBoxPorkys.Visible = False
                lstProductos.Visible = False
                txtProducto.Focus()
                txtProducto.SelectAll()
                'txtProducto.Select(txtProducto.Text.Length, 0)

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

            End Try
        End If

        If e.KeyCode = Keys.Escape Then
            txtProducto.Text = ""
            txtProducto.Focus()
        End If

    End Sub

    'Private Sub lblPrecio_TextChanged(sender As Object, e As EventArgs) Handles lblPrecioMinorista.TextChanged
    '    If lblPrecioMinorista.Text = "$0.00" Then
    '        PictureBoxPorkys.Visible = True
    '    Else
    '        PictureBoxPorkys.Visible = False
    '    End If
    'End Sub


#End Region

#Region "Componentes Formulario"

#End Region

#Region "Botones"

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Consultar Precios"

        Me.grd.Location = New Size(GroupBox1.Location.X, GroupBox1.Location.Y + GroupBox1.Size.Height + 5)
        'Dim p As New Size(GroupBox1.Size.Width, 400)
        'Me.grd.Size = New Size(p)

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            Me.Top = ARRIBA
            Me.Left = IZQUIERDA
        Else
            Me.Top = 0
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        Me.WindowState = FormWindowState.Maximized

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 30, Me.Size.Height - 10 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)

    End Sub

    Public Sub LlenarLista_Productos()


        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            'me fijo de donde viene la consulta
            ds_producto = SqlHelper.ExecuteDataset(connection, CommandType.Text, " SELECT M.Codigo,CONCAT(M.Nombre,' - ',Mar.Nombre) AS 'NomMar' FROM Materiales M JOIN Marcas Mar ON M.IdMarca = Mar.Codigo WHERE M.Eliminado = 0 AND CONCAT(M.Nombre,' - ',Mar.Nombre) LIKE  '%" + txtProducto.Text + "%' ")
            ds_producto.Dispose()

            With lstProductos
                .DataSource = ds_producto.Tables(0).DefaultView
                .DisplayMember = "NomMar"
                .ValueMember = "Codigo"
            End With

            Dim cantITems As Integer = CInt(lstProductos.Items.Count.ToString), TamañoLista As Integer = cantITems * 40 + 4 ' 13 = ItemHeight - 4 = Tamaño base del comp.
            'comparo con la altura maxima que le puedo dar dentro de la ventana 
            If TamañoLista < 360 Then
                lstProductos.Size = New Size(lstProductos.Width, TamañoLista)
            Else
                lstProductos.Size = New Size(lstProductos.Width, 360)
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

        End Try

    End Sub

#End Region

#Region "Funciones"

#End Region


    

End Class