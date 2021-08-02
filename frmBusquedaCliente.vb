Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient

Public Class frmBusquedaCliente


    Dim A As Boolean
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

        'If e.KeyCode = Keys.F3 Then
        '    'abro la ventana para cargar un nuevo cliente 
        '    btnClienteNuevo_Click(sender, e)
        'End If

    End Sub

    Private Sub frmCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'consulta para llenar la grilla
        ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, "EXEC spClientes_Select_All 0")
        ds.Dispose()
        'lleno la grilla con la consulta dada
        grdCliente.DataSource = ds.Tables(0).DefaultView
        'igualo el dv con el dataset
        dv = ds.Tables(0).DefaultView

        'oculto el id, tipo de documento y el mail en la parte visual de la abm
        grdCliente.Columns(0).Visible = False
        grdCliente.Columns(1).Visible = False
        grdCliente.Columns(3).Visible = False
        grdCliente.Columns(5).Visible = False
        grdCliente.Columns(6).Visible = False
        'grdCliente.Columns(8).Visible = False
        grdCliente.Columns(9).Visible = False
        grdCliente.Columns(10).Visible = False
        grdCliente.Columns(11).Visible = False
        grdCliente.Columns(12).Visible = False
        grdCliente.Columns(13).Visible = False
        grdCliente.Columns(14).Visible = False
        grdCliente.Columns(15).Visible = False
        grdCliente.Columns(16).Visible = False
        grdCliente.Columns(17).Visible = False
        grdCliente.Columns(18).Visible = False
        grdCliente.Columns(19).Visible = False
        grdCliente.Columns(20).Visible = False
        grdCliente.Columns(21).Visible = False


        'establezco los tamaños
        grdCliente.Columns(2).Width = 210
        'grdCliente.Columns(4).Width = 200
        ''grdCliente.Columns(6).Width = 100
        grdCliente.Columns(7).Width = 100
        'grdCliente.Columns(8).Width = 300

        'grdCliente.Columns(2).Width = AutoSize
        'grdCliente.Columns(4).Width = AutoSize
        'grdCliente.Columns(6).Width = AutoSize
        'grdCliente.Columns(7).Width = AutoSize


        grdCliente.Rows(0).Selected = False
        'envio F7 para hacer foco en txtnrodoc(desactivo-activo)
        'My.Computer.Keyboard.SendKeys("{F7}")
        btnClienteCargar.Enabled = False

        txtClienteB.Focus()

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

    Private Sub txtClienteB_LostFocus(sender As Object, e As EventArgs) Handles txtClienteB.LostFocus
        txtClienteB.BackColor = Color.White
        txtClienteB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
    End Sub

    Private Sub txtClienteB_GotFocus(sender As Object, e As EventArgs) Handles txtClienteB.GotFocus
        llenarcampos = False
        txtClienteB.BackColor = Color.Aqua
        'aviso que esta haciendo foco en este txt
        foco_cliente = True
        foco_nrodoc = False
    End Sub

    Private Sub txtClienteB_KeyDown(sender As Object, e As KeyEventArgs) Handles txtClienteB.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            Try
                If grdCliente.Rows.Count > 0 Then
                    grdCliente.Focus()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtClienteB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClienteB.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtClienteB_KeyUp(sender As Object, e As KeyEventArgs) Handles txtClienteB.KeyUp
        'llamo al procedimineto para aplica el filtro
        aplicar_busqueda()
    End Sub

    Private Sub txtClienteNroDocB_LostFocus(sender As Object, e As EventArgs) Handles txtClienteNroDocB.LostFocus
        txtClienteNroDocB.BackColor = Color.White
        txtClienteNroDocB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular)
    End Sub

    Private Sub txtClienteNroDocB_GotFocus(sender As Object, e As EventArgs) Handles txtClienteNroDocB.GotFocus

        'cambio el color del txt para ver donde estoy parado
        txtClienteNroDocB.BackColor = Color.Aqua
        txtClienteNroDocB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold)
        llenarcampos = False

        'aviso que esta haciendo foco en este txt
        foco_cliente = False
        foco_nrodoc = True

    End Sub

    Private Sub txtClienteNroDocB_KeyDown(sender As Object, e As KeyEventArgs) Handles txtClienteNroDocB.KeyDown
      
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            Try
                If grdCliente.Rows.Count > 0 Then
                    grdCliente.Focus()
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub txtClienteNroDocB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClienteNroDocB.KeyPress
        'control para sacar el sonido cada vez que precionana enter
        If Asc(e.KeyChar) = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtClienteNroDoc_KeyUp(sender As Object, e As KeyEventArgs) Handles txtClienteNroDocB.KeyUp
        'llamo al procedimiento para aplicar el filtro
        aplicar_busqueda()
    End Sub

    Private Sub grdCliente_GotFocus(sender As Object, e As EventArgs) Handles grdCliente.GotFocus
        If grdCliente.Rows.Count > 0 Then
            btnClienteCargar.Enabled = True
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
                txtClienteB.Focus()
            ElseIf foco_nrodoc = True Then
                'hago foco txtnrodoc
                txtClienteNroDocB.Focus()
            End If

        End If
    End Sub

#End Region

#Region "Botones"

    'Private Sub btnClienteSalir_Click(sender As Object, e As EventArgs) Handles btnClienteSalir.Click

    '    'limpio los txt
    '    txtClienteB.Text = ""
    '    txtClienteNroDocB.Text = ""
    '    'cierro la ventana
    '    Me.Close()

    'End Sub

    Private Sub btnClienteCargar_Click(sender As Object, e As EventArgs) Handles btnClienteCargar.Click
        Dim bloquear As Boolean = True
        If grdCliente.Rows.Count > 0 Then
            'controlo la condicion de iva - si es CF no controlo los datos
            If Not grdCliente.CurrentRow.Cells(3).Value = "5" Then
                'controlo el cuit 
                If grdCliente.CurrentRow.Cells(7).Value.ToString <> "" Then
                    If grdCliente.CurrentRow.Cells(7).Value.ToString.Length <> 11 Then
                        MsgBox("Por favor modifique un nro. de CUIT válido para cliente.")
                        txtClienteNroDocB.Focus()
                        Exit Sub
                    End If
                Else
                    MsgBox("Por favor ingrese el nro. de CUIT del cliente.")
                    txtClienteNroDocB.Focus()
                    Exit Sub
                End If
                'controlo la direccion 
                If grdCliente.CurrentRow.Cells(8).Value.ToString = "" Then
                    MsgBox("Por favor modifique la dirección del cliente.")
                    'hago foco en txtcliente
                    txtClienteB.Focus()
                    Exit Sub
                End If
            End If
            'limpio la seccion de formulario
            frmVentaSalon.txtCuit.Text = ""
            frmVentaSalon.txtCliente.Text = ""
            'cargo los valores del items seleccionado
            frmVentaSalon.txtIdCliente.Text = grdCliente.CurrentRow.Cells(1).Value.ToString
            frmVentaSalon.txtCliente.Text = IIf(grdCliente.CurrentRow.Cells(2).Value.ToString.ToUpper.Contains("CONSUMIDOR FINAL"), "", grdCliente.CurrentRow.Cells(2).Value.ToString)
            'MsgBox(frmVentaSalon.txtCliente.Text)
            If frmVentaSalon.txtCliente.Text = "" Then
                bloquear = False
            End If
            frmVentaSalon.txtCuit.Text = IIf(grdCliente.CurrentRow.Cells(7).Value.ToString = "0", "", grdCliente.CurrentRow.Cells(7).Value.ToString)
            frmVentaSalon.txtClienteDireccion.Text = grdCliente.CurrentRow.Cells(8).Value.ToString
            'paso el codigo de condicioniva (esto lo hago para despues a la hora de imprimir un ticket sepa que tipo de categoria tiene el cliente)
            frmVentaSalon.txtIDCondicionIVA.Text = grdCliente.CurrentRow.Cells(3).Value.ToString
            frmVentaSalon.cmbCondicionIVA.SelectedValue = grdCliente.CurrentRow.Cells(3).Value.ToString
            frmVentaSalon.chkCtaCte.Checked = grdCliente.CurrentRow.Cells(21).Value.ToString
            'lo paso en modo solo lectura
            frmVentaSalon.txtCuit.ReadOnly = bloquear
            frmVentaSalon.txtCliente.ReadOnly = bloquear
            frmVentaSalon.txtClienteDireccion.ReadOnly = bloquear
            frmVentaSalon.cmbCondicionIVA.Enabled = Not bloquear
            frmVentaSalon.cmbTipoComprobante.Enabled = Not bloquear

            Me.Close()

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

    Private Sub btnClienteSalir_Click(sender As Object, e As EventArgs) Handles btnClienteSalir.Click
        Me.Close()
    End Sub

#End Region

#Region "Procedimientos"

    Private Sub aplicar_busqueda()

        Try
            'limpiar filtro
            dv.RowFilter = ""

            Dim sqlstring As String

            If txtClienteB.Text.ToString = "" Then
                ' sqlstring = " [apellido] = [apellido] And [nombre] = [nombre]"
                sqlstring = " [Nombre] = [Nombre]"
                'sqlstring = sqlstring + " and [Contacto] = [Contacto]"
            Else
                sqlstring = " [Nombre] Like '%" & txtClienteB.Text & "%'"
                'sqlstring = sqlstring + " Or [Contacto] Like '" & txtClienteB.Text & "%'"
                'sqlstring = " [apellido] Like '" & txtClienteB.Text & "%' Or [nombre] Like '" & txtClienteB.Text & "%'"
            End If

            If txtClienteNroDocB.Text.ToString = "" Then
                sqlstring = sqlstring + " and [NroDoc] = [NroDoc] "
            Else
                'Dim stringnum As String = txtClienteNroDocB.Text.ToString
                sqlstring = sqlstring + String.Format(" And Convert([NroDoc],'System.String') Like '" & txtClienteNroDocB.Text & "%'")
                'sqlstring = sqlstring + " and [Nro. Doc.] Like '%" & stringnum & "%'"
            End If
            'aplico el filtro al final
            dv.RowFilter = sqlstring

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#Region "Funciones"

#End Region







  
End Class