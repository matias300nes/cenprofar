Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports System.Data.SqlClient
Imports ReportesNet


Public Class frmPresupuestos_Historia

    Dim xPre As Integer = 100
    Dim xRem As Integer = 500
    Dim xFac As Integer = 900

    Dim band As Integer = 0
    Dim LineaPreX As Integer
    Dim LineaPreY As Integer

    Dim txtNroPresupuesto As New TextBox
    Dim shape As New Microsoft.VisualBasic.PowerPacks.ShapeContainer


    Private Sub frmPresupuestos_Historia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        shape.Location = New System.Drawing.Point(0, 0)
        shape.Margin = New System.Windows.Forms.Padding(0)
        shape.Name = "ShapeContainer1"
        shape.Size = New System.Drawing.Size(Me.Width, Me.Height)
        shape.TabIndex = 2
        shape.TabStop = False

        Dim lblPresupuesto As New Label

        With lblPresupuesto
            .Location = New Point(100, 50)
            .Size = New Size(120, 15)
            .Text = "PRESUPUESTOS"
            .ForeColor = Color.Blue
            .BackColor = Color.AliceBlue
            .TextAlign = ContentAlignment.MiddleCenter
        End With

        Dim lblRemito As New Label

        With lblRemito
            .Location = New Point(500, 50)
            .Size = New Size(120, 15)
            .Text = "REMITOS"
            .ForeColor = Color.Blue
            .BackColor = Color.AliceBlue
            .TextAlign = ContentAlignment.MiddleCenter
        End With

        Dim lblFactura As New Label

        With lblFactura
            .Location = New Point(900, 50)
            .Size = New Size(120, 15)
            .Text = "FACTURAS"
            .ForeColor = Color.Blue
            .BackColor = Color.AliceBlue
            .TextAlign = ContentAlignment.MiddleCenter
        End With

        Dim btnBuscar As New Button

        With btnBuscar
            .Location = New Point(214, 9)
            .Size = New Size(128, 23)
            .TabIndex = 1
            .Text = "Buscar Presupuesto"
            .Name = "btnBuscar"
        End With

        Controls.Add(btnBuscar)

        AddHandler btnBuscar.Click, AddressOf btnBuscar_Click

        With txtNroPresupuesto
            .Location = New Point(108, 12)
            .Size = New Size(100, 20)
            .TabIndex = 0
            .Name = "txtPresupuesto"
        End With

        Controls.Add(txtNroPresupuesto)
        Controls.Add(lblPresupuesto)
        Controls.Add(lblRemito)
        Controls.Add(lblFactura)

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyButton As New Button()
        Dim RES As Long

        If band = 1 Then
            btnLimpiar()
        End If

        band = 1

        RES = CargarTMP()

        If RES > 0 Then

            'Set the button properties...
            With MyButton
                '.Location = New Point(xPre, yPre)
                .Location = New Point(xPre, Me.Height / 2 - 15)
                .Size = New Size(120, 30)
                .TabIndex = 2
                .Text = txtNroPresupuesto.Text + " - $ " + ObtenerMontoPre()
                .Name = "P" + txtNroPresupuesto.Text
                .Tag = txtNroPresupuesto.Text
            End With
            Controls.Add(MyButton)

            AddHandler MyButton.Click, AddressOf MyButton_Click

            LineaPreX = xPre + 120
            LineaPreY = Me.Height / 2

            GenerarRemitos()

            GenerarFacturas()

        Else

        End If

    End Sub

    Private Sub MyButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        nbreformreportes = "Presupuesto"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes

        Rpt.Presupuesto_App(btn.Tag, Rpt, My.Application.Info.AssemblyName.ToString)

        cnn = Nothing

    End Sub

    Private Sub BotonRemito_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        nbreformreportes = "Remito"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes
        Dim ds As Data.DataSet

        ds = SqlHelper.ExecuteDataset(cnn, CommandType.Text, "SELECT codigo FROM Presupuestos_Gestion WHERE id = " & btn.Tag)
        ds.Dispose()

        Rpt.Remito_App(ds.Tables(0).Rows(0).Item(0), Rpt, My.Application.Info.AssemblyName.ToString, "Material", False)

        cnn = Nothing
    End Sub

    Private Sub BotonFactura_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        nbreformreportes = "Factura"

        Dim cnn As New SqlConnection(ConnStringSEI)
        Dim Rpt As New frmReportes
        Dim ds As Data.DataSet

        ds = SqlHelper.ExecuteDataset(cnn, CommandType.Text, "SELECT codigo FROM Facturacion WHERE id = " & btn.Tag)
        ds.Dispose()

        'Rpt.Factura_App(ds.Tables(0).Rows(0).Item(0), Rpt, 0, Application.ProductName)

        cnn = Nothing
    End Sub

#Region "Funciones"

    Private Function CargarTMP() As Integer
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la Base de Datos. Consulte con su Administrador.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Try

            Dim param_codigo As New SqlClient.SqlParameter
            param_codigo.ParameterName = "@codigo"
            param_codigo.SqlDbType = SqlDbType.VarChar
            param_codigo.Size = 25
            param_codigo.Value = txtNroPresupuesto.Text
            param_codigo.Direction = ParameterDirection.Input

            Dim param_res As New SqlClient.SqlParameter
            param_res.ParameterName = "@res"
            param_res.SqlDbType = SqlDbType.BigInt
            param_res.Value = DBNull.Value
            param_res.Direction = ParameterDirection.InputOutput

            Try
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[spPresupuesto_Historia]", _
                                          param_codigo, param_res)

                CargarTMP = param_res.Value

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

    End Function

    Private Function ObtenerMontoPre() As String
        Dim ds As Data.DataSet

        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ObtenerMontoPre = ""
            Exit Function
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Monto FROM TMP_Presupuesto_Historia WHERE TIPO = 'Presupuesto'")
            ds.Dispose()

            ObtenerMontoPre = ds.Tables(0).Rows(0).Item(0).ToString

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex
            ObtenerMontoPre = ""
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
    End Function


#End Region

#Region "Procedimientos"

    Private Sub GenerarRemitos()

        Dim ds As Data.DataSet
        Dim dsUpd As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, NRODOC FROM TMP_Presupuesto_Historia WHERE TIPO = 'REMITO'")
            ds.Dispose()

            Dim j As Integer, Cant As Integer, Pos As Integer, PosLineaX As Integer, PosLineaY As Integer

            Cant = ds.Tables(0).Rows.Count

            If Cant > 1 Then
                Pos = (Me.Height - (Cant * 30) - ((Cant - 1) * 20)) / 2
            End If

            PosLineaX = xRem

            For j = 0 To ds.Tables(0).Rows.Count - 1
                Dim BotonRemito As New Button()
                'Set the button properties...
                With BotonRemito
                    '.Location = New Point(xPre, yPre)
                    If ds.Tables(0).Rows.Count = 1 Then
                        .Location = New Point(xRem, Me.Height / 2 - 15)
                        PosLineaY = Me.Height / 2
                    Else
                        .Location = New Point(xRem, Pos)
                        PosLineaY = Pos + 15
                        'dsUpd = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE TMP_Presupuesto_Historia SET XOrigen = " & PosLineaX.ToString & ", YOrigen = " & PosLineaY.ToString & " WHERE ID = " & ds.Tables(0).Rows(j).Item(0).ToString)
                        'dsUpd.Dispose()
                    End If
                    .Size = New Size(120, 30)
                    '.TabIndex = j
                    .Text = ds.Tables(0).Rows(j).Item(1)
                    .Name = "R" + ds.Tables(0).Rows(j).Item(1).ToString
                    .Tag = ds.Tables(0).Rows(j).Item(0).ToString
                    .Visible = True
                End With

                dsUpd = SqlHelper.ExecuteDataset(connection, CommandType.Text, "UPDATE TMP_Presupuesto_Historia SET XOrigen = " & PosLineaX.ToString & ", YOrigen = " & PosLineaY.ToString & " WHERE ID = " & ds.Tables(0).Rows(j).Item(0).ToString)
                dsUpd.Dispose()

                Controls.Add(BotonRemito)

                AddHandler BotonRemito.Click, AddressOf BotonRemito_Click


                Dim line As New Microsoft.VisualBasic.PowerPacks.LineShape

                shape.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {line})

                line.Name = "LineShape1"
                line.X1 = LineaPreX
                line.X2 = PosLineaX
                line.Y1 = LineaPreY
                line.Y2 = PosLineaY

                line.BorderWidth = 3
                line.BorderColor = Color.Red

                Controls.Add(shape)
                Pos = Pos + 50

            Next

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

    Private Sub GenerarFacturas()

        Dim ds As Data.DataSet
        Dim dsSel As Data.DataSet
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT ID, NRODOC, Cancelada, Monto, Deuda FROM TMP_Presupuesto_Historia WHERE TIPO = 'FACTURA'")
            ds.Dispose()

            Dim j As Integer, Cant As Integer, Pos As Integer, PosLineaX As Integer, PosLineaY As Integer

            Cant = ds.Tables(0).Rows.Count

            If Cant > 1 Then
                Pos = (Me.Height - (Cant * 30) - ((Cant - 1) * 20)) / 2
            End If

            PosLineaX = xFac

            For j = 0 To ds.Tables(0).Rows.Count - 1
                Dim BotonFactura As New Button()
                'Set the button properties...'
                With BotonFactura
                    '.Location = New Point(xPre, yPre)
                    If ds.Tables(0).Rows.Count = 1 Then
                        .Location = New Point(xFac, Me.Height / 2 - 15)
                        PosLineaY = Me.Height / 2
                    Else
                        .Location = New Point(xFac, Pos)
                        PosLineaY = Pos + 15
                    End If
                    .Size = New Size(200, 30)
                    '.TabIndex = j
                    .Text = ds.Tables(0).Rows(j).Item(1) + " - $ " + ds.Tables(0).Rows(j).Item(3).ToString + " - Deuda $ " + ds.Tables(0).Rows(j).Item(4).ToString
                    .Name = "F" + ds.Tables(0).Rows(j).Item(1).ToString
                    .Tag = ds.Tables(0).Rows(j).Item(0).ToString
                    .Visible = True
                    If CDbl(ds.Tables(0).Rows(j).Item(4)) = 0 Then
                        .BackColor = Color.Green
                    Else
                        .BackColor = Color.Orange
                    End If
                End With

                Controls.Add(BotonFactura)

                AddHandler BotonFactura.Click, AddressOf BotonFactura_Click

                Dim line As New Microsoft.VisualBasic.PowerPacks.LineShape

                shape.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {line})

                dsSel = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT XORIGEN, YORIGEN FROM TMP_PRESUPUESTO_HISTORIA WHERE ID IN( " & _
                                    " SELECT IDORIGEN FROM TMP_PRESUPUESTO_HISTORIA WHERE ID = " & ds.Tables(0).Rows(j).Item(0).ToString & ")")
                dsSel.Dispose()

                line.Name = "LineShape1"
                line.X1 = dsSel.Tables(0).Rows(0).Item(0).ToString + 120
                line.X2 = PosLineaX
                line.Y1 = dsSel.Tables(0).Rows(0).Item(1).ToString
                line.Y2 = PosLineaY

                line.BorderWidth = 3
                line.BorderColor = Color.Red

                Controls.Add(shape)
                Pos = Pos + 50

            Next

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

    Private Sub btnLimpiar()

        Controls.Clear()
        shape.Shapes.Clear()

        Dim lblPresupuesto As New Label

        With lblPresupuesto
            .Location = New Point(100, 50)
            .Size = New Size(120, 15)
            .Text = "PRESUPUESTOS"
            .ForeColor = Color.Blue
            .BackColor = Color.AliceBlue
            .TextAlign = ContentAlignment.MiddleCenter
        End With

        Dim lblRemito As New Label

        With lblRemito
            .Location = New Point(500, 50)
            .Size = New Size(120, 15)
            .Text = "REMITOS"
            .ForeColor = Color.Blue
            .BackColor = Color.AliceBlue
            .TextAlign = ContentAlignment.MiddleCenter
        End With

        Dim lblFactura As New Label

        With lblFactura
            .Location = New Point(900, 50)
            .Size = New Size(120, 15)
            .Text = "FACTURAS"
            .ForeColor = Color.Blue
            .BackColor = Color.AliceBlue
            .TextAlign = ContentAlignment.MiddleCenter
        End With

        Dim btnBuscar As New Button

        With btnBuscar
            .Location = New Point(214, 9)
            .Size = New Size(128, 23)
            .TabIndex = 1
            .Text = "Buscar Presupuesto"
            .Name = "btnBuscar"
        End With

        Controls.Add(btnBuscar)
        Controls.Add(lblPresupuesto)
        Controls.Add(lblRemito)
        Controls.Add(lblFactura)

        AddHandler btnBuscar.Click, AddressOf btnBuscar_Click

        With txtNroPresupuesto
            .Location = New Point(108, 12)
            .Size = New Size(100, 20)
            .TabIndex = 0
            .Name = "txtPresupuesto"
        End With

        Controls.Add(txtNroPresupuesto)
    End Sub

#End Region

    'Private Sub btnPresupuesto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPresupuesto.Click
    '    Dim MyButton As New Button()
    '    Dim RES As Long

    '    RES = CargarTMP()

    '    If RES > 0 Then

    '        'Set the button properties...
    '        With MyButton
    '            '.Location = New Point(xPre, yPre)
    '            .Location = New Point(xPre, Me.Height / 2 - 15)
    '            .Size = New Size(120, 30)
    '            .TabIndex = 0
    '            .Text = txtNroPresupuesto.Text
    '            .Name = "P" + txtNroPresupuesto.Text
    '            .Tag = RES
    '        End With
    '        Controls.Add(MyButton)

    '        AddHandler MyButton.Click, AddressOf MyButton_Click

    '        LineaPreX = xPre + 120
    '        LineaPreY = Me.Height / 2

    '        GenerarRemitos()

    '        GenerarFacturas()

    '    Else

    '    End If

    'End Sub

End Class