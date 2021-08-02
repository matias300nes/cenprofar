Imports System.Data
Imports System.Data.SqlClient
Imports TextBoxConFormatoVB

Public Class ButtonArray
    Inherits System.Collections.CollectionBase
    Private ReadOnly HostForm As System.Windows.Forms.Form
    Const desplazamientolabel = 20
    Const desplazamientocontroles = 150

    Default Public ReadOnly Property Item(ByVal Index As Integer) As System.Windows.Forms.Control
        Get
            Dim a As String
            a = Me.List.Item(Index).GetType().ToString
            Select Case a
                Case "System.Windows.Forms.Button"
                    Return CType(Me.List.Item(Index), System.Windows.Forms.Button)
                Case "System.Windows.Forms.TextBox"
                    Return CType(Me.List.Item(Index), System.Windows.Forms.TextBox)
                Case "System.Windows.Forms.ComboBox"
                    Return CType(Me.List.Item(Index), System.Windows.Forms.ComboBox)
                Case "System.Windows.Forms.DateTimePicker"
                    Return CType(Me.List.Item(Index), System.Windows.Forms.DateTimePicker)
            End Select
            Return Nothing
        End Get
    End Property

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Public Function AddNewDateTimePicker(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valordadopordefault As String = Nothing, Optional ByVal consulta As String = "", Optional ByVal cnn As SqlClient.SqlConnection = Nothing) As System.Windows.Forms.DateTimePicker
        ' Create a new instance of the Button class.
        Dim aDateTimePicker As New System.Windows.Forms.DateTimePicker()
        ' Add the datetimepicker to the collection's internal list.
        Me.List.Add(aDateTimePicker)
        ' Add the datetimepicker to the controls collection of the form 
        ' referenced by the HostForm field.
        HostForm.Controls.Add(aDateTimePicker)
        ' Set intial properties for the datetimepicker object.
        aDateTimePicker.Value = valordadopordefault
        aDateTimePicker.Width = 150
        aDateTimePicker.Height = 20
        aDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short
        aDateTimePicker.Top = Count * 25
        aDateTimePicker.Left = desplazamientocontroles
        aDateTimePicker.Tag = Me.Count
        ''aDateTimePicker.Text = "DateTimePicker " & Me.Count.ToString
        AddHandler aDateTimePicker.Click, AddressOf ClickHandler
        ''AddHandler aDateTimePicker.KeyPress, AddressOf ClickHandler()


        Return aDateTimePicker
    End Function
    Public Function AddNewButton(ByVal nombreboton As String, ByVal tipo As String) As System.Windows.Forms.Button
        ' Create a new instance of the Button class.
        Dim aButton As New System.Windows.Forms.Button()
        ' Add the button to the collection's internal list.
        Me.List.Add(aButton)
        ' Add the button to the controls collection of the form 
        ' referenced by the HostForm field.
        HostForm.Controls.Add(aButton)

        ' Set intial properties for the button object.
        aButton.Text = nombreboton
        aButton.Width = 150
        aButton.Height = 20
        aButton.Top = Count * 25
        aButton.Left = desplazamientocontroles
        aButton.Tag = Me.Count
        ''aButton.Text = "Button " & Me.Count.ToString
        AddHandler aButton.Click, AddressOf ClickHandler
        Return aButton
    End Function

    Public Function AddNewComboBox(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valorpordefault As String = "", Optional ByVal consulta As String = "", Optional ByVal cnn As SqlClient.SqlConnection = Nothing) As System.Windows.Forms.ComboBox
        Dim filtro As String
        Dim valoresdevueltos As New DataSet
        ' Create a new instance of the Button class.
        Dim aComboBox As New System.Windows.Forms.ComboBox()
        ' Add the combobox to the collection's internal list.
        Me.List.Add(aComboBox)
        ' Add the combobox to the controls collection of the form 
        ' referenced by the HostForm field.
        HostForm.Controls.Add(aComboBox)
        ' Set intial properties for the combobox object.
        aComboBox.Width = 200
        aComboBox.Height = 20
        aComboBox.DropDownHeight = 350
        aComboBox.Top = Count * 25
        aComboBox.Left = desplazamientocontroles
        aComboBox.Tag = Me.Count

        'dg 27-10-2011
        'aComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        aComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        aComboBox.AutoCompleteMode = Windows.Forms.AutoCompleteMode.Suggest
        aComboBox.AutoCompleteSource = Windows.Forms.AutoCompleteSource.ListItems


        aComboBox.Text = "ComboBox " & Me.Count.ToString



        filtro = aComboBox.Text
        ''llama a LlenarComboCodProd()que devuelve un dataset con todos los
        ''codigos de  productos de la tabla Stock
        valoresdevueltos = ExecuteConsulta(consulta, cnn)
        ' Añade los codigos de productos al combo cboCodProd 
        With aComboBox
            .Items.Clear()
            .DataSource = valoresdevueltos.Tables(0)
            .DisplayMember = valoresdevueltos.Tables(0).Columns(0).Caption.ToString
            '.SelectedIndex = 0
        End With
        AddHandler aComboBox.Click, AddressOf ClickHandler
        Return aComboBox
    End Function

    Public Function AddNewTextBox(ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valorpordefault As String = "", Optional ByVal consulta As String = "", Optional ByVal cnn As SqlClient.SqlConnection = Nothing) As System.Windows.Forms.TextBox
        ' Create a new instance of the Button class.
        ''Dim aText As New System.Windows.Forms.TextBox()
        Dim aText As New TextBoxConFormatoVB.FormattedTextBoxVB
        ' Add the TextBox to the collection's internal list.
        Me.List.Add(aText)
        ' Add the TextBox to the controls collection of the form 
        ' referenced by the HostForm field.
        HostForm.Controls.Add(aText)
        ' Set intial properties for the TextBox object.
        aText.Text = valorpordefault
        aText.Top = Count * 25
        aText.Left = desplazamientocontroles
        aText.Tag = Me.Count
        Select Case UCase(tipo)
            Case "STRING"
                ''.TypeText = TIPOS2.txtTextAndNumber
                ''.UpperCase = True
                ''ALFANUMERICO
                aText.Format = tbFormats.SpacedAlphaNumeric
            Case "DOUBLE"
                ''.TypeText = TIPOS2.txtonlynumber
                ''ENTERO
                aText.Format = tbFormats.UnsignedNumber
            Case "LONG"
                ''.TypeText = TIPOS2.txtonlynumber
                ''ENTERO
                aText.Format = tbFormats.UnsignedNumber
            Case "INTEGER"
                ''.TypeText = TIPOS2.txtonlynumber
                ''Case "DATE"
                ''  .TypeText = TIPOS2.txtDate
                aText.Format = tbFormats.UnsignedNumber
            Case Else
                ''.TypeText = TIPOS2.txtTextAndNumber
                ''ALFANUMERICO
                aText.Format = tbFormats.SpacedAlphaNumeric
        End Select

        ''aText.Text = "Text " & Me.Count.ToString
        AddHandler aText.Click, AddressOf ClickHandler
        Return aText
    End Function
    Public Function AddNewLabel(ByVal textoDelLabel As String, ByVal tipo As String) As System.Windows.Forms.Label
        ' Create a new instance of the Button class.
        Dim aLabel As New System.Windows.Forms.Label()
        ' Add the button to the collection's internal list.
        Me.List.Add(aLabel)
        ' Add the button to the controls collection of the form 
        ' referenced by the HostForm field.
        HostForm.Controls.Add(aLabel)
        ' Set intial properties for the button object.
        aLabel.Text = textoDelLabel
        aLabel.Top = Count * 25
        aLabel.Left = desplazamientolabel
        aLabel.Tag = Me.Count
        ''aLabel.Text = "Label " & Me.Count.ToString
        AddHandler aLabel.Click, AddressOf ClickHandler
        Return aLabel
    End Function
    Public Function ObtenerParametros(ByVal indice As Integer) As String
        ObtenerParametros = ""
        Dim a As String
        Dim combo As System.Windows.Forms.ComboBox
        Dim valorcombo As String

        a = Me.List.Item(indice).GetType().ToString
        Select Case a
            Case "TextBoxConFormatoVB.FormattedTextBoxVB" ''"System.Windows.Forms.TextBox"
                Return Me.List.Item(indice).ToString
            Case "System.Windows.Forms.ComboBox"

                combo = Me.List.Item(indice)
                valorcombo = combo.Text

                Return valorcombo

            Case "System.Windows.Forms.DateTimePicker"
                Return Me.List.Item(indice).ToString
        End Select
    End Function
    Public Sub New(ByVal host As System.Windows.Forms.Form, ByVal tipocontrol As String, ByVal label As String, ByVal tipo As String, ByVal formula As String, ByVal obligatorio As Boolean, Optional ByVal valordadopordefault As String = Nothing, Optional ByVal consulta As String = "", Optional ByVal conexion As SqlClient.SqlConnection = Nothing)
        HostForm = host
        Select Case tipocontrol
            Case "System.Windows.Forms.Button"
                Me.AddNewButton(valordadopordefault, tipo)
            Case "System.Windows.Forms.TextBox"
                Me.AddNewTextBox(label, tipo, formula, obligatorio, valordadopordefault)
                ''Case "System.Windows.Forms.Label"
                ''    Me.AddNewLabel()
            Case "System.Windows.Forms.ComboBox"
                Me.AddNewComboBox(label, tipo, formula, obligatorio, valordadopordefault, consulta, conexion)
            Case "System.Windows.Forms.DateTimePicker"
                Me.AddNewDateTimePicker(label, tipo, formula, obligatorio, valordadopordefault)
        End Select
    End Sub
End Class
