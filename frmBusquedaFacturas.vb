Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports ReportesNet
Imports System.Data.SqlClient

Public Class frmBusquedaFacturas


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
    Dim tran As SqlClient.SqlTransaction






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

        dtpFECHA.MaxDate = Date.Today
        dtpFECHA.MinDate = Today.Date.AddDays(-15)

        btnBuscar_Click(sender, e)

        txtClienteB.Focus()

    End Sub

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
        aplicar_filtro()
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
        aplicar_filtro()
    End Sub

    Private Sub grdCliente_GotFocus(sender As Object, e As EventArgs) Handles grdCliente.GotFocus
        If grdCliente.Rows.Count > 0 Then
            btnImprimir.Enabled = True
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
                btnImprimir_Click(sender, e)
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
            Else
                dtpFECHA.Focus()
            End If

        End If
    End Sub

#End Region

#Region "Botones"

    Private Sub btnClienteSalir_Click(sender As Object, e As EventArgs) Handles btnClienteSalir.Click
        Me.Close()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            If grdCliente.CurrentRow.Cells(4).Value.ToString() = " " Then
                If frmVentaSalon.chkConexion.Checked Then
                    Dim tipoDoc As Integer
                    Dim resbool As Boolean = False
                    Dim Total As String
                    Dim IVA As String
                    Dim Subtotal As String
                    'Me fijo que tipo de responsabilidad tiene el cliente voy a generar 
                    If grdCliente.CurrentRow.Cells(12).Value.ToString() = 5 Then
                        tipoDoc = 96
                        Subtotal = grdCliente.CurrentRow.Cells(9).Value.ToString
                        IVA = "0,00"
                        Total = grdCliente.CurrentRow.Cells(9).Value.ToString
                    Else
                        tipoDoc = 80
                        Subtotal = grdCliente.CurrentRow.Cells(7).Value.ToString
                        IVA = grdCliente.CurrentRow.Cells(8).Value.ToString
                        Total = grdCliente.CurrentRow.Cells(9).Value.ToString
                    End If
                    ' Cambiamos el cursor por el de carga
                    Me.Cursor = Cursors.WaitCursor
                    Dim connection As SqlClient.SqlConnection = Nothing
                    Try
                        connection = SqlHelper.GetConnection(ConnStringSEI)
                    Catch ex As Exception
                        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    If Abrir_Tran(connection) = False Then
                        MessageBox.Show("No se pudo abrir una transaccion", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    resbool = frmVentaSalon.GenerarFE(sender, e, CInt(grdCliente.CurrentRow.Cells(10).Value), CInt(grdCliente.CurrentRow.Cells(11).Value), tipoDoc, grdCliente.CurrentRow.Cells(2).Value.ToString, IVA, Subtotal, Total, 1, grdCliente.CurrentRow.Cells(0).Value)
                    If resbool = False Then
                        Cerrar_Tran()
                        MsgBox("No se pudo generar la factura electrónica. Por favor verifique los datos.", MsgBoxStyle.Exclamation)
                        'dejo el cursor en flecha
                        Me.Cursor = Cursors.Arrow
                        Exit Sub
                    Else
                        Dim nrofac As String = frmVentaSalon.ValorFac
                        Dim Comprobante As String = grdCliente.CurrentRow.Cells(13).Value.ToString
                        frmVentaSalon.Imprimir(nrofac, Comprobante)
                        frmVentaSalon.ValorCae = ""
                        frmVentaSalon.ValorFac = ""
                        frmVentaSalon.ValorVen = ""
                    End If
                Else
                    MsgBox("Se perdió la conexión con Servidor de AFIP. Por favor intente más tarde.", MsgBoxStyle.Exclamation)
                    Exit Sub
                    'Imprimir_Factura = True
                End If
            Else
                frmVentaSalon.Imprimir(grdCliente.CurrentRow.Cells(4).Value.ToString, grdCliente.CurrentRow.Cells(13).Value.ToString)
            End If
            Me.Close()
        Catch ex As Exception
            'dejo el cursor en flecha
            Me.Cursor = Cursors.Arrow
        End Try
       
    End Sub
     
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Dim sqlstring As String = "EXEC spVentas_Salon_Select_All '" & dtpFECHA.Value.ToShortDateString & "'"
            'MsgBox(sqlstring)
            'consulta para llenar la grilla
            ds = SqlHelper.ExecuteDataset(ConnStringSEI, CommandType.Text, sqlstring)
            ds.Dispose()
            'lleno la grilla con la consulta dada
            grdCliente.DataSource = ds.Tables(0).DefaultView
            'igualo el dv con el dataset
            dv = ds.Tables(0).DefaultView

            grdCliente.Columns(0).Visible = False
            grdCliente.Columns(7).Visible = False
            grdCliente.Columns(8).Visible = False
            'grdCliente.Columns(9).Visible = False
            grdCliente.Columns(10).Visible = False
            grdCliente.Columns(11).Visible = False
            grdCliente.Columns(12).Visible = False
            grdCliente.Columns(13).Visible = False
            'establezco los tamaños
            grdCliente.Columns(1).Width = 230
            grdCliente.Columns(2).Width = 110
            grdCliente.Columns(3).Width = 50
            grdCliente.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grdCliente.Columns(4).Width = 120
            grdCliente.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            grdCliente.Columns(5).Width = 80
            grdCliente.Columns(6).Width = 70
            grdCliente.Columns(7).Width = 70
            'grdCliente.Columns(8).Width = 70
            grdCliente.Columns(9).Width = 100
            grdCliente.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            If grdCliente.Rows.Count > 0 Then
                grdCliente.Rows(0).Selected = False
                AplicarColor_Filas()
            End If
        Catch ex As Exception

        End Try
 

    End Sub

#End Region

#Region "Procedimientos"

    Private Sub AplicarColor_Filas()
        'Cambiamos de color las filas según si están cobradas totalmente o no.
        For Each fila As DataGridViewRow In grdCliente.Rows
            If fila.Cells("Nro.Fac").Value <> " " Then
                fila.DefaultCellStyle.BackColor = Color.LightGreen
            End If
        Next
    End Sub

    Private Sub Cerrar_Tran()
        'Cierra o finaliza la transaccion
        If Not (tran Is Nothing) Then
            tran.Commit()
            tran.Dispose()
            tran = Nothing
        End If
    End Sub

    Private Sub Cancelar_Tran()
        'Cancela la transaccion
        If Not (tran Is Nothing) Then
            tran.Rollback()
            tran.Dispose()
            tran = Nothing
        End If
    End Sub

    Private Sub aplicar_filtro()

        Try
            'limpiar filtro
            dv.RowFilter = ""

            Dim sqlstring As String

            If txtClienteB.Text.ToString = "" Then
                ' sqlstring = " [apellido] = [apellido] And [nombre] = [nombre]"
                sqlstring = " [Cliente] = [Cliente]"
                'sqlstring = sqlstring + " and [Contacto] = [Contacto]"
            Else
                sqlstring = " [Cliente] Like '%" & txtClienteB.Text & "%'"
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

            AplicarColor_Filas()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#Region "Funciones"

    Private Function Abrir_Tran(ByRef cnn As SqlClient.SqlConnection) As Boolean
        If tran Is Nothing Then
            Try
                tran = cnn.BeginTransaction
                Abrir_Tran = True
            Catch ex As Exception
                Abrir_Tran = False
                Exit Function
            End Try
        End If
    End Function

#End Region









End Class