Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO




Public Class frmMaterialesExport

    Dim llenandoCombo As Boolean = False
    Dim desdecancelar As Boolean = False

    Private Sub FiltroExpo(sender As Object, e As EventArgs) Handles MyBase.Load


        chkMayorista.Checked = True
        chkMinorista.Checked = True
        chkRubro.Checked = False
        chkMarcas.Checked = False
        chkStock.Checked = False

        LlenarcmbRubros()
        LlenarcmbMarcas()
        LlenarcmbAlmacenes()

        cmbRubro.Enabled = False
        cmbMarcas.Enabled = False
        cmbAlmacenes.Enabled = False

        chkRubro.AutoCheck = True
        chkMarcas.AutoCheck = True
        chkStock.AutoCheck = True
    End Sub

    Private Sub LlenarcmbRubros()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, Nombre FROM Familias WHERE Eliminado = 0 ORDER BY Nombre")
            ds_Rubros.Dispose()

            If ds_Rubros.Tables(0).Rows.Count > 0 Then
                With cmbRubro
                    .DataSource = ds_Rubros.Tables(0).DefaultView
                    .DisplayMember = "Nombre"
                    .ValueMember = "Codigo"
                    '.AutoCompleteMode = AutoCompleteMode.Suggest
                    '.AutoCompleteSource = AutoCompleteSource.ListItems
                    '.DropDownStyle = ComboBoxStyle.DropDown
                    .SelectedIndex = 0
                End With
            End If

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenandoCombo = False

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

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbMarcas()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Rubros As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            llenandoCombo = False
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Rubros = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(nombre) as Nombre FROM Marcas WHERE Eliminado = 0 ORDER BY nombre")
            ds_Rubros.Dispose()

            If ds_Rubros.Tables(0).Rows.Count > 0 Then
                With cmbMarcas
                    .DataSource = ds_Rubros.Tables(0).DefaultView
                    .DisplayMember = "nombre"
                    .ValueMember = "Codigo"
                    '.AutoCompleteMode = AutoCompleteMode.Suggest
                    '.AutoCompleteSource = AutoCompleteSource.ListItems
                    '.DropDownStyle = ComboBoxStyle.DropDown
                    .SelectedIndex = 0
                End With
            End If

        Catch ex As Exception
            Dim errMessage As String = ""
            Dim tempException As Exception = ex

            llenandoCombo = False

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

        llenandoCombo = False

    End Sub

    Private Sub LlenarcmbAlmacenes()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds_Marcas As Data.DataSet

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds_Marcas = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo, rtrim(Nombre) as Nombre FROM Almacenes WHERE Eliminado = 0 and Codigo = 1 or Codigo = 2 ORDER BY Nombre")
            ds_Marcas.Dispose()

            With Me.cmbAlmacenes
                .DataSource = ds_Marcas.Tables(0).DefaultView
                .DisplayMember = "Nombre"
                .ValueMember = "Codigo"
                '.AutoCompleteMode = AutoCompleteMode.Suggest
                '.AutoCompleteSource = AutoCompleteSource.ListItems
                '.DropDownStyle = ComboBoxStyle.DropDown
                '.BindingContext = Me.BindingContext
                '.SelectedIndex = 0
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

    Private Function FiltrarTipoMat() As String

        If chkMayorista.Checked = True And chkMinorista.Checked = True Then
            Return ""
        ElseIf chkMayorista.Checked = True And chkMinorista.Checked = False Then
            Return " AND m.VentaMayorista = 1"
        Else
            Return " AND m.VentaMayorista = 0"
        End If

    End Function

    Private Function FiltrarFR() As String
        If chkFR.Checked Then
            Return ""
        Else
            Return " AND m.Nombre not Like '%**FR%'"
        End If
    End Function

    Private Sub KillAllExcels()
        Try

            Dim proc As System.Diagnostics.Process

            For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
                If proc.MainWindowTitle.Trim.Length = 0 Then
                    'proc.GetCurrentProcess.StartInfo
                    proc.Kill()
                End If
            Next
        Catch ex As Exception
            My.Computer.FileSystem.WriteAllText("C:\errores.log", Format(Now, "01/MM/yyy HH:mm") & " - " & ex.Message & vbCrLf, True)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        desdecancelar = True
        chkRubro.Checked = False
        cmbRubro.Enabled = False
        chkMarcas.Checked = False
        cmbMarcas.Enabled = False
        chkStock.Checked = False
        cmbAlmacenes.Enabled = False
        desdecancelar = False
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

        Dim texto As String = ""
        Dim ds_Empresa As Data.DataSet


        If chkMayorista.Checked = False And chkMinorista.Checked = False Then
            MsgBox("Por favor elija  al menos un tipo de venta de los productos.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If chkStock.Checked Then
            If cmbAlmacenes.Text = "" Then
                MsgBox("Por favor elija  un almacen.", MsgBoxStyle.Information)
                Exit Sub
            End If
            ''actualizo el stock local extraido de la WEB (es para tener el stock de los almacenes que no sean el principal)
            'MDIPrincipal.ActualizarSistema(False, False, False, False, False, False, True)
        End If
        ' Cambiamos el cursor por el de carga
        Me.Cursor = Cursors.WaitCursor

        ' Conexión con la base de datos
        Dim connection As SqlClient.SqlConnection = Nothing

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ' Creamos todo lo necesario para un excel
            Dim appXL As Excel.Application
            Dim wbXl As Excel.Workbook
            Dim shXL As Excel.Worksheet
            Dim indice As Integer = 2
            appXL = CreateObject("Excel.Application")
            appXL.Visible = True 'Para que no se muestre mientras se crea
            wbXl = appXL.Workbooks.Add
            shXL = wbXl.ActiveSheet
            ' Añadimos las cabeceras de las columnas con formato en negrita
            Dim formatRange As Excel.Range
            formatRange = shXL.Range("a1")
            formatRange.EntireRow.Font.Bold = True
            'declaro string para realizar consultas a la base de datos
            Dim sqlstring As String = ""
            Dim columnaQty As String = ""
            Dim JoinStock As String = ""
            Dim WhereStock As String = ""

            Dim Fila As Integer = 0

            shXL.Cells(1, 1).Value = "CODIGO"
            shXL.Cells(1, 2).Value = "MARCA"
            shXL.Cells(1, 3).Value = "NOMBRE"
            shXL.Cells(1, 4).Value = "RUBRO"
            shXL.Cells(1, 5).Value = "UNIDAD"
            shXL.Cells(1, 6).Value = "COSTO"
            shXL.Cells(1, 7).Value = "GANANCIA 1(%)"
            shXL.Cells(1, 8).Value = "VENTAS 1"
            shXL.Cells(1, 9).Value = "GANANCIA 2(%)"
            shXL.Cells(1, 10).Value = "VENTAS 2"
            If chkStock.Checked Then shXL.Cells(1, 11).Value = "STOCK"
            'shXL.Cells(1, 11).Value = "CANTIDAD X PAQUETE"
            'shXL.Cells(1, 12).Value = "UNIDAD DE REF."

            If chkRubro.Checked = True Then
                texto = cmbRubro.Text.ToUpper
            End If

            If chkMarcas.Checked Then
                texto = cmbMarcas.Text.ToUpper
            End If


            If chkStock.Checked Then
                texto = texto + "- Stock - " + cmbAlmacenes.Text.ToUpper
                'paso la columna de qty
                columnaQty = ",S.Qty"
                'paso el join del stock
                JoinStock = " JOIN Stock S ON S.IDMaterial = M.Codigo "
                'paso el where del stock ()
                WhereStock = " And s.IDAlmacen = " & IIf(cmbAlmacenes.SelectedValue <> 1, cmbAlmacenes.SelectedValue.ToString + " And Convert(Varchar(10),S.DateUpd,103) = Convert(Varchar(10),GetDate(),103)", cmbAlmacenes.SelectedValue.ToString)
            End If

            '-------------------------------------------------------------------------------------------------------------------------------------

            If chkRubro.Checked = True Then


                sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                       "Ganancia,PrecioCosto,Ganancia2,PrecioMayorista " + columnaQty + " " & _
                       "FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                        JoinStock + " JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0 AND M.IdFamilia = " & cmbRubro.SelectedValue & "" + FiltrarTipoMat() + FiltrarFR() + WhereStock + " ORDER BY M.Nombre ASC"


            ElseIf chkMarcas.Checked = True Then

                sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                            "Ganancia,PrecioCosto,Ganancia2,PrecioMayorista" + columnaQty + " " & _
                            "FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                            JoinStock + "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0 AND M.Idmarca = " & cmbMarcas.SelectedValue & "" + FiltrarTipoMat() + FiltrarFR() + WhereStock + " ORDER BY A.Nombre ASC,M.Nombre ASC"

            Else

                sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                           "Ganancia,PrecioCosto,Ganancia2,PrecioMayorista " + columnaQty + " " & _
                           "FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                           JoinStock + "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0" + FiltrarTipoMat() + FiltrarFR() + WhereStock + " ORDER BY A.Nombre ASC,M.Nombre ASC"

            End If


            '-------------------------------------------------------------------------------------------------------------------------------------

            ' Obtenemos los datos que queremos exportar desde base de datos.
            'sqlstring  = "exec spMateriales_Select_All_Excel @BitRubro = " & IIf(rdRubro.Checked = True, 1, 0) & ",@Rubro = " & cmbRubro.SelectedValue & "," & _
            '                           "@BitLista = " & IIf(rdLista.Checked = True, 1, 0) & ",@Lista = " & cmbLista.SelectedValue

            ds_Empresa = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
            ds_Empresa.Dispose()


            If ds_Empresa.Tables(0).Rows.Count = 0 Then
                MsgBox("El filtro realizado no ha entregado ningún resultado. Por favor verifique el dato.", MsgBoxStyle.Information)
                Exit Sub
            End If

            ' Cargamos la información en el excel
            For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1


                shXL.Cells(indice, 1).Value = ds_Empresa.Tables(0).Rows(Fila)(0)
                shXL.Cells(indice, 2).Value = ds_Empresa.Tables(0).Rows(Fila)(1)
                ' If ds_Empresa.Tables(0).Rows(Fila)(2).ToString.Length > 18 Then
                'shXL.Cells(indice, 3).Value = ds_Empresa.Tables(0).Rows(Fila)(2).ToString.Substring(0, 18)
                ' Else
                shXL.Cells(indice, 3).Value = ds_Empresa.Tables(0).Rows(Fila)(2)
                ' End If
                shXL.Cells(indice, 4).Value = ds_Empresa.Tables(0).Rows(Fila)(3)
                shXL.Cells(indice, 5).Value = ds_Empresa.Tables(0).Rows(Fila)(4)
                shXL.Cells(indice, 6).Value = ds_Empresa.Tables(0).Rows(Fila)(5)
                shXL.Cells(indice, 7).Value = ds_Empresa.Tables(0).Rows(Fila)(6)
                shXL.Cells(indice, 8).Value = ds_Empresa.Tables(0).Rows(Fila)(7)
                shXL.Cells(indice, 9).Value = ds_Empresa.Tables(0).Rows(Fila)(8)
                shXL.Cells(indice, 10).Value = ds_Empresa.Tables(0).Rows(Fila)(9)
                If chkStock.Checked Then shXL.Cells(indice, 11).Value = ds_Empresa.Tables(0).Rows(Fila)(10)
                'shXL.Cells(indice, 12).Value = ds_Empresa.Tables(0).Rows(Fila)(11)
                indice += 1
            Next
            'ajusto el tamaño de todas las columnas 
            shXL.Columns("A:J").AutoFit()

            ' Mostramos un dialog para que el usuario indique donde quiere guardar el excel
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Title = "Guardar documento Excel"
            saveFileDialog1.Filter = "Excel File|*.xlsx"
            saveFileDialog1.FileName = "Materiales " + texto + " " + Format(Date.Now, "dd-MM-yyyy").ToString
            saveFileDialog1.ShowDialog()
            ' Guardamos el excel en la ruta que ha especificado el usuario
            wbXl.SaveAs(saveFileDialog1.FileName)
            ' Cerramos el workbook
            appXL.Workbooks.Close()
            ' Eliminamos el objeto excel
            appXL.Quit()
            'cierro la ventana de filtros
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error al exportar los datos a excel." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Cerramos la conexión y ponemos el cursor por defecto de nuevo
            ' conexion.Close()
            Me.Cursor = Cursors.Arrow
        End Try

        'llamo a la funcion que mata los porcesos de excel
        KillAllExcels()

    End Sub

    Private Sub chkRubro_CheckedChanged(sender As Object, e As EventArgs) Handles chkRubro.CheckedChanged
        If desdecancelar = False Then
            cmbRubro.Enabled = chkRubro.Checked
            If chkRubro.Checked Then
                chkMarcas.Checked = Not chkRubro.Checked
            End If
        End If
    End Sub

    Private Sub chkMarcas_CheckedChanged(sender As Object, e As EventArgs) Handles chkMarcas.CheckedChanged
        If desdecancelar = False Then
            cmbMarcas.Enabled = chkMarcas.Checked
            If chkMarcas.Checked Then
                chkRubro.Checked = Not chkMarcas.Checked
            End If
        End If
    End Sub

    Private Sub chkStock_CheckedChanged(sender As Object, e As EventArgs) Handles chkStock.CheckedChanged
        If desdecancelar = False Then
            cmbAlmacenes.Enabled = chkStock.Checked
            If chkStock.Checked Then
                chkMayorista.Checked = chkStock.Checked
                chkMinorista.Checked = chkStock.Checked
            End If
        End If
    End Sub




End Class