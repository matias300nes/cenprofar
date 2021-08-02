Public Class CargarLista
    ' declaro la clase Carga, para solo ser usada dentro de la clase CargarLista
    Private Class Carga
        ' declaro la variable que uso para asignar o obtener el valor del Codigo
        ' es de tipo object porque el valor puede ser un valor num�rico o alfanum�rico
        Private mCodigo As Object
        ' declaro la variable que uso para asignar o obtener el valor de la Descripci�n
        Private mDescripcion As String

        ' declaro la propiedad Codigo de la clase Carga
        Public Property Codigo() As Object
            Get
                Return mCodigo
            End Get
            Set(ByVal Value As Object)
                mCodigo = Value
            End Set
        End Property

        ' declaro la propiedad Descripcion de la clase Carga
        Public Property Descripcion() As String
            Get
                Return mDescripcion
            End Get
            Set(ByVal Value As String)
                mDescripcion = Value
            End Set
        End Property

        ' declaro el m�todo New de la clase Carga
        Public Sub New(ByVal InitCodigo As Object, ByVal InitDescripcion As String)
            mCodigo = InitCodigo
            mDescripcion = InitDescripcion
        End Sub
    End Class

    ' la funci�n DatosLista es p�blica y sirve para llenar la carga de objetos de listas
    Public Function DatosLista(ByVal DataTable As DataTable, ByVal Objeto As Object, _
                               ByVal Codigo As String, ByVal Descripcion As String)
        ' declaro la matriz de tipo ArrayList, dimensionandola a la cantidad de registros de la tabla
        Dim Array As New ArrayList(DataTable.Rows.Count)
        ' declaro la variable como tipo DataRow
        Dim Registro As DataRow

        ' asigno a la propiedad DisplayMember el Nombre del campo vinculado del ArrayList
        Objeto.DisplayMember = "Descripcion"
        ' asigno a la propiedad ValueMember el Nombre del campo vinculado del ArrayList
        Objeto.ValueMember = "Codigo"

        ' deshabilita la actualizaci�n en pantalla del control enviado cpmo par�metro (Objeto)
        'Objeto.BeginUpdate()

        ' recorro la Table, registro por registro
        For Each Registro In DataTable.Rows
            ' agrego un nuevo registro dentro del ArrayList
            Array.Add(New Carga(Registro(Codigo), Trim(Registro(Descripcion))))
        Next
        ' asigno al DataSource del control enviado como par�metro el ArrayList
        Objeto.DataSource = Array

        ' habilita la actualizaci�n en pantalla del control enviado como par�metro (Objeto)
        'Objeto.EndUpdate()
    End Function

    ' la funci�n MarcarLista es p�blica y sirve para marcar los datos dentro de las listas
    ' �sta funci�n es solo para los controles ListBox y CheckedListBox
    'Public Function MarcarLista(ByVal DataView As DataView, ByVal Objeto As Object)
    '    Dim a As Integer
    '    Dim b As Integer

    '    ' deshabilita la actualizaci�n en pantalla del control enviado como par�metro (Objeto)
    '    Objeto.BeginUpdate()


    '    ' pregunto por el tipo de objeto
    '    If InStr(Objeto.GetType.FullName, "CheckedListBox") <> 0 Then
    '        ' rutina que sirve solo para desmarcar todos los elementos del control CheckedListBox
    '        For b = 0 To Objeto.datasource.count - 1
    '            Objeto.SetItemCheckState(b, CheckState.Unchecked)
    '        Next
    '    Else
    '        ' anula la selecci�n de todos los elementos del control ListBox
    '        Objeto.ClearSelected()
    '    End If

    '    ' ciclo para recorrer la Vista que viene como par�metro (DataView)
    '    For a = 0 To DataView.Count - 1
    '        ' ciclo para recorrer los datos dentro del objeto enviado como par�metro (Objeto)
    '        For b = 0 To Objeto.DataSource.Count - 1
    '            ' pregunta si el codigo de la lista(Objeto) coincide con el de la vista(DataView)
    '            If Objeto.DataSource.item(b).codigo() = DataView.Item(a).Row(0) Then
    '                If InStr(Objeto.GetType.FullName, "CheckedListBox") <> 0 Then
    '                    ' realiza el check del control CheckedListBox
    '                    Objeto.SetItemCheckState(b, CheckState.Checked)
    '                Else
    '                    ' realiza la selecci�n del control enviado como par�metro (ListBox)
    '                    Objeto.SetSelected(b, True)
    '                End If
    '                Exit For
    '            End If
    '        Next
    '    Next

    '    ' habilita la actualizaci�n en pantalla del control enviado como par�metro (Objeto)
    '    Objeto.EndUpdate()
    'End Function
End Class
