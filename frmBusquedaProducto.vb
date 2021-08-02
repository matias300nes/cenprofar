Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient

Public Class frmBusquedaProducto


    Dim A As Boolean
    'Dim CadenaConexion As String = "Data Source=USER-PC\SQLEXPRESS;Initial Catalog=ARAB;User ID=sa;Password=industrial"
    'Dim CadenaConexion As String = "Data Source=samba-PC;Initial Catalog=ARAB;User ID=sa;Password=industrial"
    'Dim Conn As SqlConnection = New SqlConnection(CadenaConexion)
    Dim llenarcampos As Boolean
    Dim ds As Data.DataSet
    Dim dv As DataView
    Dim B As Boolean
    'uso este booleano para saber si estoy en el boton de 
    Dim Boton_Seleccionado As Boolean
    Dim cuit_controlado As String
    Dim nError As Boolean
    Dim fecha_hora As DateTime
    Dim cadena As String
    Dim parametro_control As String
    Dim idcliente_reciente As Integer
    Dim Foco_Grilla As Boolean
    'variables que uso para saber cual fue el ultimo foco
    Dim foco_nrodoc As Boolean
    Dim foco_cliente As Boolean






#Region "Componentes del Formulario"

    Private Sub frmCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then 'Salir
            If Foco_Grilla = False Then
                btnClienteSalir.Focus()
                btnClienteSalir_Click(sender, e)
            End If
        End If

        If e.KeyCode = Keys.F1 Then
            If chkCodBarra.Checked = False Then
                chkCodBarra.Checked = True
            Else
                chkCodBarra.Checked = False
            End If
        End If

    End Sub

    Private Sub frmCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim sqlstring As String = " SELECT DISTINCT M.Codigo,CodigoBarra,M.Nombre,Isnull(RTRIM(MC.Nombre),' ') Marca,A.nombre AS Rubro ,PrecioLista4 Minorista,M.Codigo,M.IDunidad,PrecioCosto Mayorista" & _
                                  " FROM Materiales M JOIN Familias A ON A.Codigo = M.IDFamilia" & _
                                  " LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0 AND LEN(CodigoBarra) <> 4" & _
                                  " ORDER BY M.Nombre ASC"

        'consulta para llenar la grilla
        ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
        ds.Dispose()
        'lleno la grilla con la consulta dada
        grdCliente.DataSource = ds.Tables(0).DefaultView
        'igualo el dv con el dataset
        dv = ds.Tables(0).DefaultView

        'oculto el id, tipo de documento y el mail en la parte visual de la abm
        grdCliente.Columns(0).Visible = False
        'grdCliente.Columns(1).Visible = False
        grdCliente.Columns(3).Visible = False
        grdCliente.Columns(4).Visible = False
        'grdCliente.Columns(5).Visible = False
        grdCliente.Columns(6).Visible = False
        grdCliente.Columns(7).Visible = False
        'grdCliente.Columns(8).Visible = False
        'grdCliente.Columns(9).Visible = False
        'grdCliente.Columns(10).Visible = False


        'grdCliente.Columns(0).Width = 20
        grdCliente.Columns(1).Width = 130
        grdCliente.Columns(2).Width = 320
        'grdCliente.Columns(3).Width = 20
        'grdCliente.Columns(4).Width = 20
        grdCliente.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grdCliente.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

        grdCliente.Rows(0).Selected = False
        btnCargar.Enabled = False
        txtCodBarra.Enabled = False
        txtNombre.Focus()

    End Sub

    'Private Sub rdClienteNroDoc_CheckedChanged(sender As Object, e As EventArgs)

    '    txtClienteNroDocB.Enabled = rdClienteNroDocB.Checked
    '    If rdClienteNroDocB.Checked = True Then
    '        txtClienteNroDocB.Focus()
    '        txtClienteB.Text = ""
    '    End If

    'End Sub

    'Private Sub rdClienteNombreB_CheckedChanged(sender As Object, e As EventArgs)

    '    txtClienteB.Enabled = rdClienteNombreB.Checked
    '    If rdClienteNombreB.Checked = True Then
    '        'hago foco en apellido
    '        txtClienteB.Focus()
    '        txtClienteNroDocB.Text = ""
    '    End If
    'End Sub

    Private Sub txtClienteB_LostFocus(sender As Object, e As EventArgs) Handles txtNombre.LostFocus
        txtNombre.BackColor = Color.White
        txtNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
    End Sub

    Private Sub txtClienteB_GotFocus(sender As Object, e As EventArgs) Handles txtNombre.GotFocus
        llenarcampos = False
        txtNombre.BackColor = Color.Aqua
        'aviso que esta haciendo foco en este txt
        foco_cliente = True
        foco_nrodoc = False
    End Sub

    Private Sub txtClienteB_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNombre.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            Try
                If grdCliente.Rows.Count > 0 Then
                    grdCliente.Focus()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtClienteB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombre.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtClienteB_KeyUp(sender As Object, e As KeyEventArgs) Handles txtNombre.KeyUp
        'llamo al procedimineto para aplica el filtro
        aplicar_busqueda()
    End Sub

    'Private Sub txtClienteNroDocB_LostFocus(sender As Object, e As EventArgs)
    '    txtClienteNroDocB.BackColor = Color.White
    '    txtClienteNroDocB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
    'End Sub

    'Private Sub txtClienteNroDocB_GotFocus(sender As Object, e As EventArgs)

    '    'cambio el color del txt para ver donde estoy parado
    '    txtClienteNroDocB.BackColor = Color.Aqua
    '    txtClienteNroDocB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
    '    llenarcampos = False

    '    'aviso que esta haciendo foco en este txt
    '    foco_cliente = False
    '    foco_nrodoc = True

    'End Sub

    Private Sub txtClienteNroDocB_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            Try
                If grdCliente.Rows.Count > 0 Then
                    grdCliente.Focus()
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub txtClienteNroDocB_KeyPress(sender As Object, e As KeyPressEventArgs)
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtClienteNroDoc_KeyUp(sender As Object, e As KeyEventArgs)
        'llamo al procedimiento para aplicar el filtro
        aplicar_busqueda()
    End Sub

    Private Sub grdCliente_CurrentCellChanged(sender As Object, e As EventArgs) Handles grdCliente.CurrentCellChanged
        Try
            txtCodBarra.Text = grdCliente.CurrentRow.Cells(1).Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCliente_GotFocus(sender As Object, e As EventArgs) Handles grdCliente.GotFocus
        If grdCliente.Rows.Count > 0 Then
            btnCargar.Enabled = True
        End If

        llenarcampos = True
        'aviso que estoy haciendo foco en la grilla
        Foco_Grilla = True
    End Sub

    Private Sub grdCliente_LostFocus(sender As Object, e As EventArgs) Handles grdCliente.LostFocus

        'If grdCliente.Rows.Count = 0 Then
        '    btnClienteCargar.Enabled = False
        'End If

        llenarcampos = False

        'aviso que se salio de la grilla
        Foco_Grilla = False

    End Sub

    Private Sub grdClientes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCliente.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles grdCliente.KeyDown
        'preciono enter para cargar cliente en pantalla principal
        If e.KeyCode = Keys.Enter Then
            If grdCliente.Rows.Count > 0 Then
                btnClienteCargar_Click(sender, e)
            End If
        End If
        'preciono escape para salir de la grilla
        If e.KeyCode = Keys.Escape Then
            If foco_cliente = True Then
                'hago foco en txtcliente
                txtNombre.Focus()
                'ElseIf foco_nrodoc = True Then
                '    'hago foco txtnrodoc
                '    txtClienteNroDocB.Focus()
            End If

        End If
    End Sub

#End Region

#Region "Botones"

      Private Sub btnClienteSalir_Click(sender As Object, e As EventArgs) Handles btnClienteSalir.Click
        Me.Close()
    End Sub

    Private Sub btnClienteCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click

        If grdCliente.Rows.Count > 0 Then
            If CDbl(grdCliente.CurrentRow.Cells(5).Value) <> 0 And CDbl(grdCliente.CurrentRow.Cells(8).Value) <> 0 Then
                If chkCodBarra.Checked Then
                    If MessageBox.Show("Está seguro que desea modificar el código de barra del Producto " & grdCliente.CurrentRow.Cells(2).Value.ToString & "?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        chkCodBarra.Checked = False
                        Exit Sub
                    Else
                        Dim res As Integer = ActualizarRegistro()
                        Select Case res
                            Case -4
                                MsgBox("El Código de Barra debe contener más de 4 carateres.", MsgBoxStyle.Exclamation)
                                Exit Sub
                            Case -3
                                MsgBox("Ya existe otro Registro con este mismo Código de Barra.", MsgBoxStyle.Exclamation)
                                Exit Sub
                            Case -2
                                MsgBox("El registro ya existe.", MsgBoxStyle.Exclamation)
                                Exit Sub
                            Case -1
                                MsgBox("No se pudo actualizar el registro.", MsgBoxStyle.Exclamation)
                                Exit Sub
                            Case 0
                                MsgBox("No se pudo agregar el registro.", MsgBoxStyle.Exclamation)
                                Exit Sub
                                'Case Else
                                'MsgBox("Se ha actualizado el registro.", MsgBoxStyle.Exclamation)
                        End Select
                    End If
                End If

                'lo paso el codigo de barra al txt
                If txtCodBarra.Text.ToString <> "" Then
                    frmVentaSalon.txtCodigoBarra.Text = IIf(grdCliente.CurrentRow.Cells(1).Value.ToString = "", txtCodBarra.Text, grdCliente.CurrentRow.Cells(1).Value.ToString)
                Else
                    frmVentaSalon.txtIdProducto.Text = grdCliente.CurrentRow.Cells(0).Value.ToString
                End If
                Me.Close()
            Else
                MsgBox("No se puede cargar un producto cuyo Precio no es correcto. Por favor verifique el dato.", MsgBoxStyle.Exclamation)
            End If
        End If

    End Sub

    'Private Sub btnClienteNuevo_Click(sender As Object, e As EventArgs) Handles btnClienteNuevo.Click
    '    Dim C As New frmClientes
    '    C.ShowDialog()
    '    'si se agrego un cliente cierro la ventana
    '    If frmVentaSalon.ClienteAgregado = True Then
    '        Me.Close()
    '    Else
    '        'hago foco en el txt clienteBusqueda
    '        txtClienteB.Focus()
    '    End If
    'End Sub

#End Region

#Region "Procedimientos"

    Private Sub aplicar_busqueda()

        Try
            'limpiar filtro
            dv.RowFilter = ""

            Dim sqlstring As String

            If txtNombre.Text.ToString = "" Then
                ' sqlstring = " [apellido] = [apellido] And [nombre] = [nombre]"
                sqlstring = " [Nombre] = [Nombre]"
                'sqlstring = sqlstring + " and [Contacto] = [Contacto]"
            Else
                sqlstring = " [Nombre] Like '%" & txtNombre.Text & "%'"
                'sqlstring = sqlstring + " Or [Contacto] Like '" & txtClienteB.Text & "%'"
                'sqlstring = " [apellido] Like '" & txtClienteB.Text & "%' Or [nombre] Like '" & txtClienteB.Text & "%'"
            End If

            'If txtClienteNroDocB.Text.ToString = "" Then
            '    sqlstring = sqlstring + " and [NroDoc] = [NroDoc] "
            'Else
            '    'Dim stringnum As String = txtClienteNroDocB.Text.ToString
            '    sqlstring = sqlstring + String.Format(" And Convert([NroDoc],'System.String') Like '" & txtClienteNroDocB.Text & "%'")
            '    'sqlstring = sqlstring + " and [Nro. Doc.] Like '%" & stringnum & "%'"
            'End If

            'aplico el filtro al final
            dv.RowFilter = sqlstring

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#Region "Funciones"

#End Region







  
    Private Sub chkCodBarra_CheckedChanged(sender As Object, e As EventArgs) Handles chkCodBarra.CheckedChanged
        txtCodBarra.Enabled = chkCodBarra.Checked
        If chkCodBarra.Checked Then
            txtCodBarra.Focus()
        End If
    End Sub

    Private Sub txtCodBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodBarra.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            Try

                If grdCliente.Rows.Count > 0 Then
                    grdCliente.Focus()
                    'btnClienteCargar_Click(e, sender)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtCodBarra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodBarra.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Function ActualizarRegistro() As Integer

        Try

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = grdCliente.CurrentRow.Cells(0).Value.ToString
            param_codigo.Direction = ParameterDirection.Input

            Dim param_codigobarra As New SqlClient.SqlParameter
            param_codigobarra.ParameterName = "@codigobarra"
            param_codigobarra.SqlDbType = SqlDbType.VarChar
            param_codigobarra.Size = 25
            param_codigobarra.Value = txtCodBarra.Text
            param_codigobarra.Direction = ParameterDirection.Input

            Dim param_useradd As New SqlClient.SqlParameter
            param_useradd.ParameterName = "@userupd"
            param_useradd.SqlDbType = SqlDbType.Int
            param_useradd.Value = UserID
            param_useradd.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.Int
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(ConnStringSEI, CommandType.StoredProcedure, "spMateriales_CodBarra_Update", param_codigo, param_codigobarra, param_useradd, param_res)

                ActualizarRegistro = param_res.Value

            Catch ex As Exception
                Throw ex
            End Try
        Finally

        End Try
    End Function
End Class