
Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports ReportesNet

Public Class frmMovDia_Caja

    Dim bolpoliticas As Boolean
    Dim tranWEB As New WS_Porkys.WS_PorkysSoapClient
    Dim conn_del_form As SqlClient.SqlConnection = Nothing
    Dim band As Integer

#Region "Procedimientos Formularios"

    Private Sub frmUnidades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        band = 0
        configurarform()
        btnEliminar.Visible = False
        btnNuevo.Visible = False
        btnActivar.Visible = False
        lblConexion.Visible = False
        btnGuardar.Visible = False
        btnActualizar.Visible = False
        btnCancelar.Visible = False
        PicConexion.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False
        btnPrimero.Visible = False
        btnImprimir.Visible = False
        btnImportarExcel.Visible = False
        'grd.Visible = False
        ToolStripPagina.Visible = False
        ToolStripSeparator1.Visible = False
        ToolStripSeparator2.Visible = False
        btnExcel.Visible = False
        StatusStrip1.Visible = False


        dtpFecha.Value = Date.Now

        'para cargar las cajas------
        SQL = "spMovimientos_Caja '" & dtpFecha.Value & "'"
        LlenarGrilla()
        'CargarCajas()
        'PrepararBotones()
        '---------------------------


        Permitir = True

        RealizarConsulta()


        If grd.Rows.Count = 0 Then
            Util.MsgStatus(Status1, "No se han registrado movimientos en el día.")
        End If

        grd.Columns(0).Visible = False

        band = 1

    End Sub

#End Region

#Region "Procedimientos"

    Private Sub configurarform()
        Me.Text = "Movimientos de Caja"

        Me.grd.Location = New Size(GroupBox1.Location.X + 2, GroupBox1.Location.Y + GroupBox1.Size.Height + 7)

        'Me.Size = New Size(Me.Size.Width + 57, (Screen.PrimaryScreen.WorkingArea.Height - 90) + 17)

        'Dim p As New Size(GroupBox1.Size.Width + 55, Me.Size.Height - StatusStrip1.Height - GroupBox1.Size.Height - ToolMenu.Height - 50)
        'Me.grd.Size = New Size(p)

        'Me.Width = Me.Width + 20

        If LLAMADO_POR_FORMULARIO Then
            LLAMADO_POR_FORMULARIO = False
            'Me.Top = ARRIBA
            'Me.Left = IZQUIERDA
            'Else
            '    Me.Top = 0
            '    Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        End If

        'Me.Top = 0
        'Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2

        Me.WindowState = FormWindowState.Maximized

        Me.grd.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width - 27, Me.Size.Height - 7 - GroupBox1.Size.Height - GroupBox1.Location.Y - 65)


    End Sub

    Private Sub asignarTags()


    End Sub

    Private Sub Verificar_Datos()

        bolpoliticas = False

        bolpoliticas = True

    End Sub

    Private Sub CalcularTotalMov()
        '0
        Dim apertura As Double = 0
        '1
        Dim Ingresos As Double = 0
        '3
        Dim gastos As Double = 0
        '4
        Dim PagosProv As Double = 0
        '5
        Dim Cheques As Double = 0
        '6
        Dim retiros As Double = 0
        '7
        Dim ventas As Double = 0
        '8
        Dim ventasCtaCte As Double = 0


        Dim total As Double = 0

        For i As Integer = 0 To grd.Rows.Count - 1
            If grd.Rows(i).Cells(0).Value = 0 Then
                apertura = apertura + grd.Rows(i).Cells(8).Value
            ElseIf grd.Rows(i).Cells(0).Value = 1 Then
                Ingresos = Ingresos + grd.Rows(i).Cells(8).Value
            ElseIf grd.Rows(i).Cells(0).Value = 2 Then
                gastos = gastos + grd.Rows(i).Cells(8).Value
            ElseIf grd.Rows(i).Cells(0).Value = 3 Then
                PagosProv = PagosProv + grd.Rows(i).Cells(8).Value
            ElseIf grd.Rows(i).Cells(0).Value = 4 Then
                Cheques = Cheques + grd.Rows(i).Cells(8).Value
            ElseIf grd.Rows(i).Cells(0).Value = 5 Then
                retiros = retiros + grd.Rows(i).Cells(8).Value
            ElseIf grd.Rows(i).Cells(0).Value = 6 Then
                ventas = ventas + grd.Rows(i).Cells(8).Value
            Else
                ventasCtaCte = ventasCtaCte + grd.Rows(i).Cells(8).Value
            End If
        Next

        lblApCaja.Text = FormatNumber(apertura, 2)
        lblIngresos.Text = FormatNumber(Ingresos, 2)
        lblGastos.Text = FormatNumber(gastos, 2)
        lblPagosProv.Text = FormatNumber(PagosProv, 2)
        lblCheques.Text = FormatNumber(Cheques, 2)
        lblRetiros.Text = FormatNumber(retiros, 2)
        lblVentas.Text = FormatNumber(ventas, 2)
        lblVentasCtaCte.Text = FormatNumber(ventasCtaCte, 2)
        total = apertura + Ingresos - gastos - PagosProv - retiros + ventas + ventasCtaCte
        lblTotal.Text = FormatNumber(total, 2)

    End Sub

#End Region

#Region "Funciones"

    Private Sub RealizarConsulta()

        Try

            Dim connection As SqlClient.SqlConnection = Nothing
            Try
                connection = SqlHelper.GetConnection(ConnStringSEI)
            Catch ex As Exception
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            SQL = "spMovimientos_Caja '" & dtpFecha.Value & "'"

            LlenarGrilla()

            'Permitir = True
            'CargarCajas()
            'PrepararBotones()

            CalcularTotalMov()

        Catch ex As Exception

            MsgBox(ex.Message + "Desde de filtro")

        End Try


    End Sub

#End Region

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged

        If band = 1 Then
            'Util.MsgStatus(Status1, "Buscanto Registros...", My.Resources.Resources.indicator_white)

            If ReglasNegocio() Then
                RealizarConsulta()
            End If
        End If

    End Sub

End Class

