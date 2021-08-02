Imports Utiles.Util
Public Class frmParametros
    ' Declare a new ButtonArray object.
    Dim MyControlArray As ButtonArray
    Dim MyControlArrayLabel As LabelArray

    ''Conexion a Base de Datos PARADAS
    Structure Estructura
        Public Label As String
        Public tipo As String
        Public formula As String
        Public obligatorio As Boolean
        Public Default1 As String
        Public consulta As String
        Public valor As String

    End Structure
    Dim Contador As Integer = 0
    '
    ' Array de estructura con los parametros
    ' maximo 10 parametros por reporte
    '
    Dim DATOS(10) As Estructura

    Public Sub frmParametros_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Contador = 0
        ''COMENTADO MS 12-07-2010
        '' cantparametrosmasboton = 0
        ''FIN COMENTADO
        ''NUEVO MS 12-10-2010
        cantparametrosmasboton = 1
        ''FIN NUEVO
    End Sub

    '---------------------------------------------------------------------------------------
    ' Module    : frmParametros
    ' DateTime  : 26/06/2010 
    ' Author    : ms
    ' Purpose   : Tomar parametros de otro formulario
    '             y pasarselos a crystal para que gestione los filtros
    '
    '---------------------------------------------------------------------------------------
    '
    ' Estructura para guardar los datos de los
    ' parametros usados en el reporte
    '   
    Private Sub AgregarLabel(ByVal textoDelLabel As String)
        If MyControlArrayLabel Is Nothing Then
            MyControlArrayLabel = New LabelArray(Me, "System.Windows.Forms.Label", textoDelLabel)
        Else
            ' Call the AddNewTextBox method of MyControlArray.
            MyControlArrayLabel.AddNewLabel(textoDelLabel)
        End If
    End Sub
    Private Sub AgregarBoton(ByVal textoboton As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal consulta As String = "", Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        If MyControlArray Is Nothing Then
            MyControlArray = New ButtonArray(Me, "System.Windows.Forms.Button", tipo, textoboton, "", False)
        Else
            ' Llama al metodo AddNewButton MyControlArray.
            MyControlArray.AddNewButton(textoboton, tipo)
        End If
        DATOS(Contador).Label = textoboton
        DATOS(Contador).tipo = tipo
        DATOS(Contador).formula = ""
        DATOS(Contador).obligatorio = False
        DATOS(Contador).Default1 = ""
        DATOS(Contador).consulta = consulta
        DATOS(Contador).valor = ""
        '
        ' Incrementar el contador
        '
        Contador = Contador + 1
    End Sub

    Public Sub AgregarTextBox(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valorpordefault As String = "", Optional ByVal consulta As String = "", Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        If MyControlArray Is Nothing Then
            MyControlArray = New ButtonArray(Me, "System.Windows.Forms.TextBox", label, tipo, "", obligatorio, valorpordefault)
        Else
            ' Call the AddNewTextBox method of MyControlArray.
            MyControlArray.AddNewTextBox(label, tipo, formula, obligatorio, valorpordefault)
        End If
        DATOS(Contador).Label = label
        DATOS(Contador).tipo = tipo
        DATOS(Contador).formula = formula

        DATOS(Contador).obligatorio = obligatorio
        DATOS(Contador).Default1 = valorpordefault
        DATOS(Contador).consulta = ""
        DATOS(Contador).valor = ""
        '
        ' Incrementar el contador
        '
        Contador = Contador + 1
    End Sub
    Private Sub AgregarCombo(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valorpordefault As String = "", Optional ByVal consulta As String = "", Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        If MyControlArray Is Nothing Then
            MyControlArray = New ButtonArray(Me, "System.Windows.Forms.ComboBox", label, tipo, formula, obligatorio, valorpordefault, consulta, conexion)
        Else
            ' Call the AddNewTextBox method of MyControlArray.
            MyControlArray.AddNewComboBox(label, tipo, formula, obligatorio, valorpordefault, consulta, conexion)
        End If
        DATOS(Contador).Label = label
        DATOS(Contador).tipo = tipo
        DATOS(Contador).formula = formula
        DATOS(Contador).obligatorio = obligatorio
        DATOS(Contador).consulta = consulta
        DATOS(Contador).Default1 = valorpordefault
        DATOS(Contador).valor = ""
        '
        ' Incrementar el contador
        '
        Contador = Contador + 1
    End Sub

    Private Sub AgregarDateTimiPicker(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal fechainicialyfinal As String = Nothing, Optional ByVal consulta As String = "", Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        If MyControlArray Is Nothing Then
            MyControlArray = New ButtonArray(Me, "System.Windows.Forms.DateTimePicker", label, tipo, "", obligatorio, fechainicialyfinal)
        Else
            ' Call the AddNewTextBox method of MyControlArray.
            MyControlArray.AddNewDateTimePicker(label, tipo, "", obligatorio, fechainicialyfinal)
        End If
        DATOS(Contador).Label = label
        DATOS(Contador).tipo = tipo
        DATOS(Contador).formula = formula
        DATOS(Contador).obligatorio = obligatorio
        DATOS(Contador).Default1 = fechainicialyfinal
        DATOS(Contador).consulta = ""
        DATOS(Contador).valor = ""
        '
        ' Incrementar el contador
        '
        Contador = Contador + 1
    End Sub
    Public Sub AgregarParametros(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valordefault As String = "", Optional ByVal consulta As String = "", Optional ByVal conexion As SqlClient.SqlConnection = Nothing)

        If (tipo.ToUpper = "BUTTON") Then
            AgregarBoton(label, tipo, formula, obligatorio, consulta, conexion)
        Else
            ''"LABEL"
            AgregarLabel(label)
            Select Case tipo.ToUpper

                Case "LONG"
                    ''"TEXTBOX"
                    AgregarTextBox(label, tipo, formula, obligatorio, valordefault)
                Case "INTEGER"
                    ''"TEXTBOX"
                    AgregarTextBox(label, tipo, formula, obligatorio, valordefault)
                Case "DOUBLE"
                    ''"TEXTBOX"
                    AgregarTextBox(label, tipo, formula, obligatorio, valordefault)
                Case "STRING"
                    If consulta = "" Then
                        ''"TEXTBOX"
                        AgregarTextBox(label, tipo, formula, obligatorio, valordefault)
                    Else
                        ''"COMBO"
                        AgregarCombo(label, tipo, formula, obligatorio, valordefault, consulta, conexion)
                    End If

                Case "DATE"
                    ''"DATETIMEPICKER"
                    AgregarDateTimiPicker(label, tipo, formula, obligatorio, valordefault)
                    ''NUEVO MS 06-05-2011
                Case "BOOLEAN"

                    If consulta = "" Then
                        ''"TEXTBOX"
                        AgregarTextBox(label, tipo, formula, obligatorio, valordefault)
                    Else
                        ''"COMBO"
                        AgregarCombo(label, tipo, formula, obligatorio, valordefault, consulta, conexion)
                    End If


            End Select
        End If
        ''NUEVO MS 12-07-2010
        cantparametrosmasboton = cantparametrosmasboton + 1
        ''FIN NUEVO
    End Sub
    Public Function ObtenerParametros(ByVal indice As Integer) As String
        ObtenerParametros = DATOS(indice).valor
    End Function


    Private Function AgregarFiltros(ByVal s As String) As String

        Dim aux As String
        aux = filtradopor

        If aux = "" Then
            aux = s
        Else
            If Mid(aux, 1, 14) = "'Filtrado por:" Then
                aux = Mid(aux, 1, Len(aux) - 1) & ", " & s & "'"
            Else
                aux = aux & ", " & s
            End If
        End If
        Return aux
    End Function

    Public Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click


        Dim i As Integer = 0
        ''DESDE i=0 HASTA LA CANTIDAD DE CONTROLES EN EL ARRAY SIN CONTAR LOS LABEL
        For i = 0 To Contador - 1 ''MyControlArray.Count - 1 
            '' ''Dim pru As String
            '' ''pru = MyControlArray.ObtenerParametros(i)

            '' ''Dim pru1 As String
            '' ''pru1 = pru
            Select Case DATOS(i).tipo.ToUpper
                Case "STRING"
                    If DATOS(i).consulta = "" Then
                        ''"TEXTBOX"
                        Dim auxstring As String
                        auxstring = MyControlArray.ObtenerParametros(i)
                        ''DE ESTA FORMA CUANDO ES UN TEXTBOX PQ APARTE DEL VALOR ME TRAE UNA CADENA
                        ''DATOS(i).valor = Mid(auxstring, 47, auxstring.Length)
                        If Mid(auxstring, 47, auxstring.Length) = "" Then
                            If DATOS(i).obligatorio = True Then
                                MsgBox("El campo " & DATOS(i).Label & " está vacio y es obligatorio.", vbOKOnly + vbExclamation, "Ingresar Datos")
                                ''VACIO LA VARIABLE PARA QUE NO QUEDE CARGADA CON UN PARAMETRO INCORRECTO
                                filtradopor = ""
                                Exit Sub
                            End If
                        Else
                            DATOS(i).valor = Mid(auxstring, 47, auxstring.Length)

                            'nuevo cp 27-7-2011
                            'filtradopor = filtradopor & DATOS(i).Label & " " & DATOS(i).valor & ", "
                            filtradopor = AgregarFiltros(DATOS(i).Label & " " & DATOS(i).valor)



                        End If
                    Else
                        ''"COMBO"
                        Dim auxstringcombo As String
                        auxstringcombo = MyControlArray.ObtenerParametros(i)
                        ''DE ESTA FORMA CUANDO ES UN COMBO PQ APARTE DEL VALOR ME TRAE UNA CADENA
                        ''DATOS(i).valor = Mid(auxstring, 47, auxstring.Length)
                        If Mid(auxstringcombo, 1, auxstringcombo.Length) = "" Then
                            If DATOS(i).obligatorio = True Then
                                MsgBox("El campo " & DATOS(i).Label & " está vacio y es obligatorio.", vbOKOnly + vbExclamation, "Ingresar Datos")
                                ''VACIO LA VARIABLE PARA QUE NO QUEDE CARGADA CON UN PARAMETRO INCORRECTO
                                filtradopor = ""
                                Exit Sub
                            End If
                        Else
                            DATOS(i).valor = MyControlArray.ObtenerParametros(i).ToString

                            'nuevo cp 27-7-2011                            
                            'filtradopor = filtradopor & DATOS(i).Label & " " & DATOS(i).valor & ", "
                            filtradopor = AgregarFiltros(DATOS(i).Label & " " & DATOS(i).valor)

                        End If
                       
                    End If
                Case "BOOLEAN"
                    If DATOS(i).consulta = "" Then
                        Dim auxstring As String
                        auxstring = MyControlArray.ObtenerParametros(i)
                        ''DE ESTA FORMA CUANDO ES UN TEXTBOX PQ APARTE DEL VALOR ME TRAE UNA CADENA
                        ''DATOS(i).valor = Mid(auxstring, 47, auxstring.Length)
                        If Mid(auxstring, 47, auxstring.Length) = "" Then
                            If DATOS(i).obligatorio = True Then
                                MsgBox("El campo " & DATOS(i).Label & " está vacio y es obligatorio.", vbOKOnly + vbExclamation, "Ingresar Datos")
                                ''VACIO LA VARIABLE PARA QUE NO QUEDE CARGADA CON UN PARAMETRO INCORRECTO
                                filtradopor = ""
                                Exit Sub
                            End If
                        Else
                            DATOS(i).valor = Mid(auxstring, 47, auxstring.Length)

                            'nuevo cp 27-7-2011   
                            'filtradopor = filtradopor & DATOS(i).Label & " " & DATOS(i).valor & ", "
                            filtradopor = AgregarFiltros(DATOS(i).Label & " " & DATOS(i).valor)

                        End If
                    Else
                        ''"COMBO"
                        ''PARA EL RESTO TEXTBOX, COMBO, ETC SE HACE DE ESTA FORMA
                        If MyControlArray.ObtenerParametros(i).ToString.ToUpper = "S" Or MyControlArray.ObtenerParametros(i).ToString.ToUpper = "SI" Or MyControlArray.ObtenerParametros(i).ToString.ToUpper = "OK" Or MyControlArray.ObtenerParametros(i).ToString.ToUpper = "A" Or MyControlArray.ObtenerParametros(i).ToString.ToUpper = "TRUE" Then
                            DATOS(i).valor = "true"
                        Else
                            DATOS(i).valor = "false"
                        End If
                        'nuevo cp 27-7-2011 
                        'filtradopor = filtradopor & DATOS(i).Label & " " & IIf(DATOS(i).valor = "true", "SI", "NO") & ", "
                        filtradopor = AgregarFiltros(DATOS(i).Label & " " & IIf(DATOS(i).valor = "true", "SI", "NO"))
                        
                    End If

                Case "DATE"
                    ''"DATETIMEPICKER"
                    Dim auxdate As String
                    auxdate = MyControlArray.ObtenerParametros(i)
                    ''DE ESTA FORMA CUANDO ES UN DATETIMEPICKER PQ APARTE DE LA FECHA ME TRAE UNA CADENA
                    DATOS(i).valor = Mid(auxdate, 45, auxdate.Length)

                    'nuevo cp 27-7-2011 
                    'filtradopor = filtradopor & DATOS(i).Label & " " & CType(DATOS(i).valor.ToString, Date).ToString("dd/MM/yyyy") & ", "
                    filtradopor = AgregarFiltros(DATOS(i).Label & " " & CType(DATOS(i).valor.ToString, Date).ToString("dd/MM/yyyy"))

                Case Else
                    '''''LONG,INTEGER,DOUBLE
                    Dim auxintlongdouble As String
                    auxintlongdouble = MyControlArray.ObtenerParametros(i)
                    ''DE ESTA FORMA CUANDO ES UN TEXTBOX PQ APARTE DEL VALOR ME TRAE UNA CADENA
                    ''DATOS(i).valor = Mid(auxstring, 47, auxstring.Length)
                    If Mid(auxintlongdouble, 47, auxintlongdouble.Length) = "" Then
                        If DATOS(i).obligatorio = True Then
                            MsgBox("El campo " & DATOS(i).Label & " está vacio y es obligatorio.", vbOKOnly + vbExclamation, "Ingresar Datos")
                            ''VACIO LA VARIABLE PARA QUE NO QUEDE CARGADA CON UN PARAMETRO INCORRECTO
                            filtradopor = ""
                            Exit Sub
                        End If
                    Else
                        DATOS(i).valor = Mid(auxintlongdouble, 47, auxintlongdouble.Length)

                        'nuevo cp 27-7-2011 
                        'filtradopor = filtradopor & DATOS(i).Label & DATOS(i).valor & ", "
                        filtradopor = AgregarFiltros(DATOS(i).Label & DATOS(i).valor)


                    End If
            End Select
        Next i
        
        If Mid(filtradopor, 1, 14) <> "'Filtrado por:" Then
            filtradopor = "'" & "Filtrado por: " & filtradopor & "'"
        End If
        ''SI AL frmParametros SE LE HIZO CLICK EN Aceptar PARA FILTRAR ENTONCES cerroparametrosconaceptar = True
        ''Y CIERRO O DESTRUYO EL frmParametros MEDIANTE Me.Dispose()
        cerroparametrosconaceptar = True
        Me.Dispose()
    End Sub

    ''NUEVO MS 13-07-2010
    Public Sub frmParametros_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If e.CloseReason = Windows.Forms.CloseReason.UserClosing Then
        'cantparametrosmasboton = 0
        'Else
        cantparametrosmasboton = 1
        'End If
    End Sub

    ''FIN NUEVO 13-07-2010
    Public Sub frmParametros_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''NUEVO MS 01-07-2010
        ''UBICA EL BOTON ACEPTAR DESPUES DEL ULTIMO CONTROL AGREGADO DINAMICAMENTE
        btnAceptar.Width = 150
        btnAceptar.Height = 20
        btnAceptar.Top = cantparametrosmasboton * 25
        btnAceptar.Left = 150  ''ESTO DEBE TENER EL MISMO VALOR QUE LA VARIABLE desplazamientodontroles DE 
        ''LA CLASE ButtonArray

        ''AGRANDA O ACHICA DINAMICAMENTE EL FORMULARIO PARAMETROS DE ACUERDO A LA CANTIDAD DE CONTROLES QUE 
        ''SE LE HALLAN AGREGADO DINAMICAMENTE
        If cantparametrosmasboton <= 3 Then
            Me.Height = cantparametrosmasboton * 55 ''ALTURA DEL FORM PARAMETROS (VARIABLE SEGUN LA CANT DE CONTROLES AGREGADOS DINAMICAMENTE)
        Else
            Me.Height = cantparametrosmasboton * 40 ''ALTURA DEL FORM PARAMETROS (VARIABLE SEGUN LA CANT DE CONTROLES AGREGADOS DINAMICAMENTE)
        End If
        Me.Width = 400      ''ANCHO DEL FORM PARAMETROS (FIJO)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D  ''PONE EL BORDE DEL FORMULARIO FIJO
        ''FIN NUEVO

        ''LE DA EL FOCO AL PRIMER CONTROL DE DATOS QUE APAREZCA EN frmParametros (TextBox,ComboBox,DateTimePicker, etc)
        Me.Controls(2).Select()

    End Sub
End Class