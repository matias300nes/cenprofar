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
        chkRubro.Checked = False
        chkMarcas.Checked = False
        chkLista.Checked = False
        LlenarcmbRubros()
        LlenarcmbMarcas()
        LLenarPrecioLista()
        cmbRubro.Enabled = False
        cmbMarcas.Enabled = False
        cmbLista.Enabled = False
        chkRubro.AutoCheck = True
        chkMarcas.AutoCheck = True
        chkLista.AutoCheck = True

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

    Private Sub LLenarPrecioLista()
        Dim connection As SqlClient.SqlConnection = Nothing
        Dim ds As Data.DataSet

        llenandoCombo = True

        Try
            connection = SqlHelper.GetConnection(ConnStringSEI)
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try

            ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, "SELECT Codigo , Descripcion FROM Lista_Precios WHERE Eliminado = 0 and Codigo <> 10 ORDER BY Descripcion")
            ds.Dispose()

            With cmbLista
                .DataSource = ds.Tables(0).DefaultView
                .DisplayMember = "Descripcion"
                .ValueMember = "Codigo"
            End With

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

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        desdecancelar = True
        chkRubro.Checked = False
        cmbRubro.Enabled = False
        chkMarcas.Checked = False
        cmbMarcas.Enabled = False
        chkLista.Checked = False
        cmbLista.Enabled = False
        desdecancelar = False
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

        If chkMayorista.Checked = False And chkMinorista.Checked = False Then
            MsgBox("Por favor elija  al menos un tipo de venta de los productos.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If chkLista.Checked = False And chkRubro.Checked = False And chkMarcas.Checked = False Then
            If MessageBox.Show("Desea exportar a un archivo excel sin aplicar un filtro?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim texto As String = ""
        Dim ds_Empresa As Data.DataSet
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
            appXL.Visible = False 'Para que no se muestre mientras se crea
            wbXl = appXL.Workbooks.Add
            shXL = wbXl.ActiveSheet
            ' Añadimos las cabeceras de las columnas con formato en negrita
            Dim formatRange As Excel.Range
            formatRange = shXL.Range("a1")
            formatRange.EntireRow.Font.Bold = True
            'me fijo que rd está seleccionado
            If chkLista.Checked = True Then
                Select Case cmbLista.SelectedValue
                    Case 3
                        shXL.Cells(1, 1).Value = "CODIGO"
                        shXL.Cells(1, 2).Value = "MARCA"
                        shXL.Cells(1, 3).Value = "NOMBRE"
                        shXL.Cells(1, 4).Value = "RUBRO"
                        shXL.Cells(1, 5).Value = "UNIDAD"
                        shXL.Cells(1, 6).Value = "COSTO"
                        shXL.Cells(1, 7).Value = "MAYORISTA"
                        'shXL.Cells(1, 8).Value = "CANTIDAD X PAQUETE"
                        'shXL.Cells(1, 9).Value = "UNIDAD DE REF."
                    Case 4
                        shXL.Cells(1, 1).Value = "CODIGO"
                        shXL.Cells(1, 2).Value = "MARCA"
                        shXL.Cells(1, 3).Value = "NOMBRE"
                        shXL.Cells(1, 4).Value = "RUBRO"
                        shXL.Cells(1, 5).Value = "UNIDAD"
                        shXL.Cells(1, 6).Value = "COSTO"
                        shXL.Cells(1, 7).Value = "REVENDEDOR"
                        'shXL.Cells(1, 8).Value = "CANTIDAD X PAQUETE"
                        'shXL.Cells(1, 9).Value = "UNIDAD DE REF."
                    Case 5
                        shXL.Cells(1, 1).Value = "CODIGO"
                        shXL.Cells(1, 2).Value = "MARCA"
                        shXL.Cells(1, 3).Value = "NOMBRE"
                        shXL.Cells(1, 4).Value = "RUBRO"
                        shXL.Cells(1, 5).Value = "UNIDAD"
                        shXL.Cells(1, 6).Value = "COSTO"
                        shXL.Cells(1, 7).Value = "YAMILA"
                        'shXL.Cells(1, 8).Value = "CANTIDAD X PAQUETE"
                        'shXL.Cells(1, 9).Value = "UNIDAD DE REF."
                    Case Else
                        shXL.Cells(1, 1).Value = "CODIGO"
                        shXL.Cells(1, 2).Value = "MARCA"
                        shXL.Cells(1, 3).Value = "NOMBRE"
                        shXL.Cells(1, 4).Value = "RUBRO"
                        shXL.Cells(1, 5).Value = "UNIDAD"
                        shXL.Cells(1, 6).Value = "COSTO"
                        shXL.Cells(1, 7).Value = cmbLista.Text.ToUpper
                        'shXL.Cells(1, 8).Value = "CANTIDAD X PAQUETE"
                        'shXL.Cells(1, 9).Value = "UNIDAD DE REF."
                End Select

                texto = cmbLista.Text.ToUpper

            Else
                shXL.Cells(1, 1).Value = "CODIGO"
                shXL.Cells(1, 2).Value = "MARCA"
                shXL.Cells(1, 3).Value = "NOMBRE"
                shXL.Cells(1, 4).Value = "RUBRO"
                shXL.Cells(1, 5).Value = "UNIDAD"
                shXL.Cells(1, 6).Value = "COSTO"
                shXL.Cells(1, 7).Value = "MAYORISTA"
                shXL.Cells(1, 8).Value = "REVENDEDOR"
                shXL.Cells(1, 9).Value = "YAMILA"
                shXL.Cells(1, 10).Value = "LISTA 4"
                'shXL.Cells(1, 11).Value = "CANTIDAD X PAQUETE"
                'shXL.Cells(1, 12).Value = "UNIDAD DE REF."

                If chkRubro.Checked = True Then
                    texto = cmbRubro.Text.ToUpper
                End If

                If chkMarcas.Checked Then
                    texto = cmbMarcas.Text.ToUpper
                End If

            End If

            Dim sqlstring As String


            '-------------------------------------------------------------------------------------------------------------------------------------

            If chkLista.Checked = True Then

                Select Case cmbLista.SelectedValue
                    Case "3"
                        sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                                    "PrecioCosto FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                                    "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY A.Nombre ASC,M.Nombre ASC"
                    Case "4"
                        sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                                    "PrecioMayorista FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                                    "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY A.Nombre ASC,M.Nombre ASC"
                    Case "5"
                        sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                                    "PrecioLista3 FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                                    "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY A.Nombre ASC,M.Nombre ASC"
                    Case Else
                        sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                                    "PrecioLista4 FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                                    "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY A.Nombre ASC,M.Nombre ASC"
                End Select

            ElseIf chkRubro.Checked = True Then

                sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                            "PrecioCosto,PrecioMayorista,PrecioLista3,PrecioLista4 " & _
                            "FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                            "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0 AND M.IdFamilia = " & cmbRubro.SelectedValue & "" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY M.Nombre ASC"

            ElseIf chkMarcas.Checked = True Then

                sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                            "PrecioCosto,PrecioMayorista,PrecioLista3,PrecioLista4 " & _
                            "FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                            "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0 AND M.Idmarca = " & cmbMarcas.SelectedValue & "" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY A.Nombre ASC,M.Nombre ASC"

            Else

                sqlstring = "Select M.codigo,Isnull(RTRIM(MC.Nombre),' '),M.nombre,A.nombre,B.Nombre,PrecioCompra," & _
                           "PrecioCosto,PrecioMayorista,PrecioLista3,PrecioLista4 " & _
                           "FROM Materiales M JOIN Unidades B ON B.Codigo = M.IDUnidad " & _
                           "JOIN Familias A ON A.Codigo = M.IDFamilia LEFT JOIN Marcas mc ON mc.codigo = m.idmarca WHERE m.Eliminado = 0" + FiltrarTipoMat() + FiltrarFR() + " ORDER BY A.Nombre ASC,M.Nombre ASC"

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

            Dim Fila As Integer

            Fila = 0

            ' Cargamos la información en el excel
            For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1

                If chkLista.Checked = True Then
                    shXL.Cells(indice, 1).Value = ds_Empresa.Tables(0).Rows(Fila)(0)
                    shXL.Cells(indice, 2).Value = ds_Empresa.Tables(0).Rows(Fila)(1)
                    shXL.Cells(indice, 3).Value = ds_Empresa.Tables(0).Rows(Fila)(2)
                    shXL.Cells(indice, 4).Value = ds_Empresa.Tables(0).Rows(Fila)(3)
                    shXL.Cells(indice, 5).Value = ds_Empresa.Tables(0).Rows(Fila)(4)
                    shXL.Cells(indice, 6).Value = ds_Empresa.Tables(0).Rows(Fila)(5)
                    shXL.Cells(indice, 7).Value = ds_Empresa.Tables(0).Rows(Fila)(6)
                    'shXL.Cells(indice, 8).Value = ds_Empresa.Tables(0).Rows(Fila)(7)
                    'shXL.Cells(indice, 9).Value = ds_Empresa.Tables(0).Rows(Fila)(8)
                Else
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
                    'shXL.Cells(indice, 11).Value = ds_Empresa.Tables(0).Rows(Fila)(10)
                    'shXL.Cells(indice, 12).Value = ds_Empresa.Tables(0).Rows(Fila)(11)
                End If
                indice += 1
            Next

            'ajusto el tamaño de todas las columnas 
            shXL.Columns("A:H").AutoFit()
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
            MessageBox.Show("Error al exportar los datos a excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Cerramos la conexión y ponemos el cursor por defecto de nuevo
            ' conexion.Close()
            Me.Cursor = Cursors.Arrow
        End Try

        'llamo a la funcion que mata los porcesos de excel
        KillAllExcels()

    End Sub

    'Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

    '    'If chkMayorista.Checked = False And chkMinorista.Checked = False Then
    '    '    MsgBox("Por favor elija  al menos un tipo de venta de los productos.", MsgBoxStyle.Information)
    '    '    Exit Sub
    '    'End If

    '    'If rdLista.Checked = False And rdRubro.Checked = False Then
    '    '    If MessageBox.Show("Desea exportar a un archivo excel sin aplicar un filtro?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
    '    '        Exit Sub
    '    '    End If
    '    'End If

    '    Dim texto As String = ""
    '    Dim ds_Empresa As Data.DataSet
    '    ' Cambiamos el cursor por el de carga
    '    Me.Cursor = Cursors.WaitCursor

    '    ' Conexión con la base de datos
    '    Dim connection As SqlClient.SqlConnection = Nothing

    '    Try
    '        connection = SqlHelper.GetConnection(ConnStringSEI)
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try

    '    Try
    '        ' Creamos todo lo necesario para un excel
    '        Dim appXL As Excel.Application
    '        Dim wbXl As Excel.Workbook
    '        Dim shXL As Excel.Worksheet
    '        Dim indice As Integer = 2
    '        appXL = CreateObject("Excel.Application")
    '        appXL.Visible = False 'Para que no se muestre mientras se crea
    '        wbXl = appXL.Workbooks.Add
    '        shXL = wbXl.ActiveSheet
    '        ' Añadimos las cabeceras de las columnas con formato en negrita
    '        Dim formatRange As Excel.Range
    '        formatRange = shXL.Range("a1")
    '        formatRange.EntireRow.Font.Bold = True
    '        'me fijo que rd está seleccionado

    '        shXL.Cells(1, 1).Value = "CODIGO"
    '        shXL.Cells(1, 2).Value = "IDMARCA"
    '        shXL.Cells(1, 3).Value = "IDFAMILIA"
    '        shXL.Cells(1, 4).Value = "IDUNIDAD"
    '        shXL.Cells(1, 5).Value = "NOMBRE"
    '        shXL.Cells(1, 6).Value = "PRECIOCOSTO"
    '        shXL.Cells(1, 7).Value = "PRECIOCOMPRA"
    '        shXL.Cells(1, 8).Value = "PRECIOPERON"
    '        shXL.Cells(1, 9).Value = "GANANCIA"
    '        shXL.Cells(1, 10).Value = "MINIMO"
    '        shXL.Cells(1, 11).Value = "MAXIMO"
    '        shXL.Cells(1, 12).Value = "PRECIOMAYORISTA"
    '        shXL.Cells(1, 13).Value = "PRECIOMAYORISTAPERON"
    '        shXL.Cells(1, 14).Value = "IDLISTAMAYORISTA"
    '        shXL.Cells(1, 15).Value = "IDLISTAMAYORISTAPERON"
    '        shXL.Cells(1, 16).Value = "IDLISTAMINORISTA"
    '        shXL.Cells(1, 17).Value = "IDLISTAMINORISTAPERON"
    '        shXL.Cells(1, 18).Value = "PRECIOLISTA3"
    '        shXL.Cells(1, 19).Value = "IDLISTA3"
    '        shXL.Cells(1, 20).Value = "PRECIOLISTA4"
    '        shXL.Cells(1, 21).Value = "IDLISTA4"
    '        shXL.Cells(1, 22).Value = "UNIDADREF"
    '        shXL.Cells(1, 23).Value = "CODIGOBARRA"
    '        shXL.Cells(1, 24).Value = "PASILLO"
    '        shXL.Cells(1, 25).Value = "ESTANTE"
    '        shXL.Cells(1, 26).Value = "FILA"
    '        shXL.Cells(1, 27).Value = "CONTROLSTOCK"
    '        shXL.Cells(1, 28).Value = "CANTIDADPACK"
    '        shXL.Cells(1, 29).Value = "CAMBIAR1"
    '        shXL.Cells(1, 30).Value = "CAMBIAR2"
    '        shXL.Cells(1, 31).Value = "CAMBIAR3"
    '        shXL.Cells(1, 32).Value = "CAMBIAR4"
    '        shXL.Cells(1, 33).Value = "VENTAMAYORISTA"
    '        shXL.Cells(1, 34).Value = "USERADD"
    '        shXL.Cells(1, 35).Value = "DATEADD"


    '        Dim sqlstring As String = "SELECT CODIGO,IDMARCA,IDFAMILIA,IDUNIDAD,NOMBRE, PRECIOCOSTO,PRECIOCOMPRA,PRECIOPERON,GANANCIA,MINIMO,MAXIMO," & _
    '                                  "PRECIOMAYORISTA,PRECIOMAYORISTAPERON,IDLISTAMAYORISTA,IDLISTAMAYORISTAPERON,IDLISTAMINORISTA,IDLISTAMINORISTAPERON," & _
    '                                  "PRECIOLISTA3,IDLISTA3,PRECIOLISTA4,IDLISTA4,UNIDADREF,CODIGOBARRA,PASILLO,ESTANTE,FILA,CONTROLSTOCK,CANTIDADPACK, " & _
    '                                  "CAMBIAR1,CAMBIAR2,CAMBIAR3,CAMBIAR4,VENTAMAYORISTA,USERADD,convert(varchar(10),DATEADD,103)	 FROM  Materiales WHERE (IDUnidad = 'HORMA' OR IDUnidad = 'TIRA') AND Eliminado = 0	"


    '        ds_Empresa = SqlHelper.ExecuteDataset(connection, CommandType.Text, sqlstring)
    '        ds_Empresa.Dispose()

    '        Dim Fila As Integer

    '        Fila = 0

    '        ' Cargamos la información en el excel
    '        For Fila = 0 To ds_Empresa.Tables(0).Rows.Count - 1

    '            shXL.Cells(indice, 1).Value = ds_Empresa.Tables(0).Rows(Fila)(0)
    '            shXL.Cells(indice, 2).Value = ds_Empresa.Tables(0).Rows(Fila)(1)
    '            shXL.Cells(indice, 3).Value = ds_Empresa.Tables(0).Rows(Fila)(2)
    '            shXL.Cells(indice, 4).Value = ds_Empresa.Tables(0).Rows(Fila)(3)
    '            shXL.Cells(indice, 5).Value = ds_Empresa.Tables(0).Rows(Fila)(4)
    '            shXL.Cells(indice, 6).Value = ds_Empresa.Tables(0).Rows(Fila)(5)
    '            shXL.Cells(indice, 7).Value = ds_Empresa.Tables(0).Rows(Fila)(6)
    '            shXL.Cells(indice, 8).Value = ds_Empresa.Tables(0).Rows(Fila)(7)
    '            shXL.Cells(indice, 9).Value = ds_Empresa.Tables(0).Rows(Fila)(8)
    '            shXL.Cells(indice, 10).Value = ds_Empresa.Tables(0).Rows(Fila)(9)
    '            shXL.Cells(indice, 11).Value = ds_Empresa.Tables(0).Rows(Fila)(10)
    '            shXL.Cells(indice, 12).Value = ds_Empresa.Tables(0).Rows(Fila)(11)
    '            shXL.Cells(indice, 13).Value = ds_Empresa.Tables(0).Rows(Fila)(12)
    '            shXL.Cells(indice, 14).Value = ds_Empresa.Tables(0).Rows(Fila)(13)
    '            shXL.Cells(indice, 15).Value = ds_Empresa.Tables(0).Rows(Fila)(14)
    '            shXL.Cells(indice, 16).Value = ds_Empresa.Tables(0).Rows(Fila)(15)
    '            shXL.Cells(indice, 17).Value = ds_Empresa.Tables(0).Rows(Fila)(16)
    '            shXL.Cells(indice, 18).Value = ds_Empresa.Tables(0).Rows(Fila)(17)
    '            shXL.Cells(indice, 19).Value = ds_Empresa.Tables(0).Rows(Fila)(18)
    '            shXL.Cells(indice, 20).Value = ds_Empresa.Tables(0).Rows(Fila)(19)
    '            shXL.Cells(indice, 21).Value = ds_Empresa.Tables(0).Rows(Fila)(20)
    '            shXL.Cells(indice, 22).Value = ds_Empresa.Tables(0).Rows(Fila)(21)
    '            shXL.Cells(indice, 23).Value = ds_Empresa.Tables(0).Rows(Fila)(22)
    '            shXL.Cells(indice, 24).Value = ds_Empresa.Tables(0).Rows(Fila)(23)
    '            shXL.Cells(indice, 25).Value = ds_Empresa.Tables(0).Rows(Fila)(24)
    '            shXL.Cells(indice, 26).Value = ds_Empresa.Tables(0).Rows(Fila)(25)
    '            shXL.Cells(indice, 27).Value = ds_Empresa.Tables(0).Rows(Fila)(26)
    '            shXL.Cells(indice, 28).Value = ds_Empresa.Tables(0).Rows(Fila)(27)
    '            shXL.Cells(indice, 29).Value = ds_Empresa.Tables(0).Rows(Fila)(28)
    '            shXL.Cells(indice, 30).Value = ds_Empresa.Tables(0).Rows(Fila)(29)
    '            shXL.Cells(indice, 31).Value = ds_Empresa.Tables(0).Rows(Fila)(30)
    '            shXL.Cells(indice, 32).Value = ds_Empresa.Tables(0).Rows(Fila)(31)
    '            shXL.Cells(indice, 33).Value = ds_Empresa.Tables(0).Rows(Fila)(32)
    '            shXL.Cells(indice, 34).Value = ds_Empresa.Tables(0).Rows(Fila)(33)
    '            shXL.Cells(indice, 35).Value = ds_Empresa.Tables(0).Rows(Fila)(34)

    '            indice += 1

    '        Next

    '        'ajusto el tamaño de todas las columnas 
    '        shXL.Columns("A:AG").AutoFit()
    '        ' Mostramos un dialog para que el usuario indique donde quiere guardar el excel
    '        Dim saveFileDialog1 As New SaveFileDialog()
    '        saveFileDialog1.Title = "Guardar documento Excel"
    '        saveFileDialog1.Filter = "Excel File|*.xlsx"
    '        saveFileDialog1.FileName = "Materiales " + "FRAGMENTADOS" + " " + Format(Date.Now, "dd-MM-yyyy").ToString
    '        saveFileDialog1.ShowDialog()
    '        ' Guardamos el excel en la ruta que ha especificado el usuario
    '        wbXl.SaveAs(saveFileDialog1.FileName)
    '        ' Cerramos el workbook
    '        appXL.Workbooks.Close()
    '        ' Eliminamos el objeto excel
    '        appXL.Quit()
    '        'cierro la ventana de filtros
    '        Me.Close()
    '    Catch ex As Exception
    '        MessageBox.Show("Error al exportar los datos a excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        ' Cerramos la conexión y ponemos el cursor por defecto de nuevo
    '        ' conexion.Close()
    '        Me.Cursor = Cursors.Arrow
    '    End Try

    '    'llamo a la funcion que mata los porcesos de excel
    '    KillAllExcels()

    'End Sub

    Private Sub rdRubro_CheckedChanged(sender As Object, e As EventArgs)
        If desdecancelar = False Then
            cmbRubro.Enabled = chkRubro.Checked
            chkLista.Checked = Not chkRubro.Checked
            'rdMarcas.Checked = Not rdRubro.Checked
        End If

    End Sub

    Private Sub rdLista_CheckedChanged(sender As Object, e As EventArgs)
        If desdecancelar = False Then
            cmbLista.Enabled = chkLista.Checked
            chkRubro.Checked = Not chkLista.Checked
            'rdMarcas.Checked = Not rdLista.Checked
        End If

    End Sub

    Private Sub rdMarcas_CheckedChanged(sender As Object, e As EventArgs)
        If desdecancelar = False Then
            cmbMarcas.Enabled = chkMarcas.Checked
            chkRubro.Checked = Not chkMarcas.Checked
            chkLista.Checked = Not chkMarcas.Checked
        End If
    End Sub

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


    Private Sub chkRubro_CheckedChanged(sender As Object, e As EventArgs) Handles chkRubro.CheckedChanged
        If desdecancelar = False Then
            cmbRubro.Enabled = chkRubro.Checked
            If chkRubro.Checked Then
                chkLista.Checked = Not chkRubro.Checked
                chkMarcas.Checked = Not chkRubro.Checked
            End If
        End If
    End Sub

    Private Sub chkMarcas_CheckedChanged(sender As Object, e As EventArgs) Handles chkMarcas.CheckedChanged
        If desdecancelar = False Then
            cmbMarcas.Enabled = chkMarcas.Checked
            If chkMarcas.Checked Then
                chkRubro.Checked = Not chkMarcas.Checked
                chkLista.Checked = Not chkMarcas.Checked
            End If
        End If
    End Sub

    Private Sub chkLista_CheckedChanged(sender As Object, e As EventArgs) Handles chkLista.CheckedChanged
        If desdecancelar = False Then
            cmbLista.Enabled = chkLista.Checked
            If chkLista.Checked = True Then
                chkRubro.Checked = Not chkLista.Checked
                chkMarcas.Checked = Not chkLista.Checked
            End If
        End If
    End Sub
End Class