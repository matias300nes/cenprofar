#Region "Enumeraci�n de formatos"

'Listado de todos los formatos que es capaz de gestionar la
'caja de texto
Public Enum tbFormats
    'Todos los caracteres y n�meros con espacios. Valor por
    'defecto de la propiedad Format
    SpacedAlphaNumeric

    'Todos los caracteres y n�meros sin espacios
    NoSpacedAlphaNumeric

    'S�lo las letras con espacios
    SpacedAlphabetic

    'S�lo las letras sin espacios
    NoSpacedAlphabetic

    'S�lo n�meros enteros sin signo
    UnsignedNumber

    'S�lo n�meros enteros con signo
    SignedNumber

    'S�lo n�meros con coma decimal flotante sin signo
    UnsignedFloatingPointNumber

    'S�lo n�meros con coma decimal flotante con signo
    SignedFloatingPointNumber

    'S�lo n�meros con coma decimal fija sin signo. El n�mero
    'de decimales se debe especificar en la propiedad Decimals
    UnsignedFixedPointNumber

    'S�lo n�meros con coma decimal fija con signo. El n�mero
    'de decimales se debe especificar en la propiedad Decimals
    SignedFixedPointNumber

    'S�lo n�meros en formato hexadecimal
    HexadecNumber

    'S�lo n�meros en formato octal
    OctalNumber

    'S�lo n�meros en formato binario
    BynaryNumber

    'Definido por usuario
    UserDefined

    ' Un tipo nuevo de TEST
    OtroQueNoSeUsa3
End Enum
#End Region

'Caja de texto que permite el control autom�to del formato 
'de entrada del texto
Public Class FormattedTextBoxVB
    Inherits System.Windows.Forms.TextBox



#Region "Campos protected y private"
    'Almacena el valor de la propiedad Format
    Protected mFormat As tbFormats = tbFormats.NoSpacedAlphaNumeric

    'Mete las teclas TAB, RETORNO e INTRO para aceptar siempre
    'estas teclas
    Protected ControlKeys As String = Chr(8) & Chr(9) & Chr(13)

    'Almacena el valor de la propiedad UserValues
    Protected mUserValues As String

    'Almacena el valor de la propiedad DecSeparator
    Protected mDecSeparator As Char = "."

    'Almacena el valor de la propiedad Decimals
    Protected mDecimals As Byte = 2

    Protected mText_1 As String
    Protected mText_2 As String
    Protected mText_3 As String
    Protected mText_4 As String


    'Almacena los d�gitos v�lidos para algunos formatos
    Private okValues As String
#End Region

#Region "Constructor y m�todo protected Dispose"
    Public Sub New()
        MyBase.New()

        'El Dise�ador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicializaci�n despu�s de la llamada a InitializeComponent()
        mFormat = tbFormats.SpacedAlphaNumeric
        Dim j As Integer
        j = "0.25" * 4
        If j = 1 Then
            Me.mDecSeparator = "."
        Else
            Me.mDecSeparator = ","
        End If
      
    End Sub

    'UserControl1 reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
#End Region

#Region " C�digo generado por el Dise�ador de Windows Forms "
    'Requerido por el Dise�ador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Dise�ador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Dise�ador de Windows Forms. 
    'No lo modifique con el editor de c�digo.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

#Region "M�todos privados de apoyo"
    'Valida el mensaje WM_CHAR recibido desde WndProc.
    'Devuelve true si es v�lido y false si no lo es
    ''COMENTADO MS 30-06-2010 POR WARNING FUNCION SIN As
    Private Function validar(ByRef m As Message) As Boolean

        Dim values As String = ""

        Select Case mFormat
            Case tbFormats.SpacedAlphaNumeric
                Return True

            Case tbFormats.NoSpacedAlphaNumeric
                If m.WParam.ToInt32 <> Asc(" ") Then
                    Return True
                Else
                    Return False
                End If

            Case tbFormats.SignedNumber
                values = "0123456789" & Me.ControlKeys

                Me.ComprobarSigno(values)

            Case tbFormats.UnsignedFloatingPointNumber
                values = "0123456789" & Me.ControlKeys

                'Si es una coma o un punto se convierte al
                'separador decimal establecido en la propiedad
                'DecSeparator
                If m.WParam.ToInt32 = CInt(Asc(".")) OrElse _
                   m.WParam.ToInt32 = CInt(Asc(",")) Then
                    m.WParam = New IntPtr(CInt(Asc(Me.DecSeparator)))
                End If

                Me.ComprobarComa(values)

            Case tbFormats.SignedFloatingPointNumber
                values = "0123456789" & Me.ControlKeys

                If m.WParam.ToInt32 = CInt(Asc(".")) OrElse _
                   m.WParam.ToInt32 = CInt(Asc(",")) Then
                    m.WParam = New IntPtr(CInt(Asc(Me.DecSeparator)))
                End If

                Me.ComprobarComa(values)
                Me.ComprobarSigno(values)

            Case tbFormats.UnsignedFixedPointNumber
                values = Me.ControlKeys

                If m.WParam.ToInt32 = CInt(Asc(".")) OrElse _
                   m.WParam.ToInt32 = CInt(Asc(",")) Then
                    m.WParam = New IntPtr(CInt(Asc(Me.DecSeparator)))
                End If

                Me.ComprobarPosicion(values)

            Case tbFormats.SignedFixedPointNumber
                values = Me.ControlKeys

                If m.WParam.ToInt32 = CInt(Asc(".")) OrElse _
                   m.WParam.ToInt32 = CInt(Asc(",")) Then
                    m.WParam = New IntPtr(CInt(Asc(Me.DecSeparator)))
                End If

                Me.ComprobarSigno(values)
                Me.ComprobarPosicion(values)

            Case Else
                values = okValues

        End Select

        'dg
        If values.IndexOf(Chr(m.WParam.ToInt32())) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Comprueba si el formato es de alg�n tipo de n�mero decimal
    'Retorna true si es decimal, false si no lo es
    Private Function EsFormatoDecimal() As Boolean
        Return (CInt(mFormat) >= 6 AndAlso CInt(mFormat) <= 9)
    End Function

    'Comprueba si se puede poner la coma en un formato decimal
    Private Sub ComprobarComa(ByRef values As String)
        If MyBase.Text.IndexOf(mDecSeparator) >= 0 Then

            If MyBase.SelectedText.IndexOf(mDecSeparator) >= 0 Then
                values &= mDecSeparator
            End If
        Else
            values &= mDecSeparator

        End If
    End Sub

    'Comprueba si se puede poner el signo con un formato Signed
    'seg�n la posici�n del cursor
    Private Sub ComprobarSigno(ByRef values As String)

        If MyBase.Text.IndexOf("-") >= 0 Then
            If MyBase.SelectedText.IndexOf("-") >= 0 Then
                values &= "-"
            End If
        Else
            If MyBase.SelectionStart = 0 Then
                values &= "-"
            End If
        End If

    End Sub

    'Comprueba si se pueden seguir escribiendo n�meros
    'en un formato FixedPointNumber seg�n la posici�n del cursor
    Private Sub ComprobarPosicion(ByRef values As String)

        If MyBase.Text.Length - (MyBase.SelectionStart + MyBase.SelectionLength) <= Me.mDecimals Then
            Me.ComprobarComa(values)
        End If

        Dim pos As Integer = MyBase.Text.IndexOf(Me.mDecSeparator)

        If pos >= 0 Then
            If MyBase.SelectionStart > pos Then

                If MyBase.SelectionLength > 0 OrElse MyBase.Text.Length - pos <= Me.mDecimals Then
                    values &= "0123456789"
                End If
            Else
                values &= "0123456789"
            End If
        Else
            values &= "0123456789"
        End If

    End Sub

    'Actualiza el separador decimal
    Private Sub ActualizarSeparador()
        Dim s() As Char = MyBase.Text.ToCharArray()
        Dim i As Integer

        MyBase.Text = ""

        For i = 0 To s.Length - 1

            Dim j As Integer
            j = "0.25" * 4
            If j = 1 Then
                Me.mDecSeparator = "."
            Else
                Me.mDecSeparator = ","
            End If

            If s(i) = "." OrElse s(i) = "," Then
                s(i) = Me.mDecSeparator
            End If

            MyBase.Text &= s(i).ToString()
        Next i

    End Sub

    'Cambia a base decimal un n�mero escrito en cualquier base
    Private Function BaseADecimal(ByVal base As Integer) As Long
        Dim s() As Char = Me.Text.ToUpper().ToCharArray()
        Dim i As Integer
        Dim res As Long = 0
        Dim digito As Double

        For i = s.Length - 1 To 0 Step -1
            Try
                'Si el d�gito es un n�mero lo obtenemos
                digito = Double.Parse(s(i).ToString())
            Catch
                'Si el d�gito es una letra, calculamos su valor
                digito = CDbl(Asc(s(i)) - Asc("A") + 10)
            End Try

            res += CLng(Math.Pow(CDbl(base), CDbl(s.Length - 1 - i)) * digito)

        Next i

        Return res

    End Function

    'Cambia a cualquier base un n�mero escrito en base decimal
    Private Function DecimalABase(ByVal num As Long, ByVal base As Integer) As String
        Dim resto As Integer
        Dim res As String = ""

        Do
            resto = CInt(num Mod CLng(base))

            If resto < 10 Then
                'Si es menor que 10 lo ponemos en la cadena
                res = resto.ToString() & res
            Else
                'Si es mayor o igual 10 calculamos la letra
                res = Chr(Asc("A") - 10 + resto).ToString() & res
            End If

            num \= base
        Loop Until num = 0

        Return res
    End Function
#End Region

#Region "Propiedad Text sobreescrita"
    'Devuelve o establece el texto del control
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
            Set(ByVal Value As String)
            'En lugar de poner la cadena de un s�lo golpe
            'se valida d�gito a d�gito seg�n el formato
            'establecido
            Const WM_CHAR As Integer = 258
            Dim s() As Char = Value.ToCharArray()
            Dim c As Char
            Dim wParam As IntPtr
            Dim lParam As New IntPtr(0)
            Dim m As Message

            MyBase.Text = ""

            For Each c In s
                wParam = New IntPtr(Asc(c))
                m = Message.Create(Me.Handle, WM_CHAR, wParam, lParam)

                'dg
                If Me.validar(m) Then
                    MyBase.Text &= c.ToString()
                End If
            Next
        End Set
    End Property

    'Devuelve o establece el texto del control
    Public Property Text_1() As String
        Get
            Return mText_1
        End Get
        Set(ByVal Value As String)
            
            mText_1 = Value
        End Set
    End Property
    'Devuelve o establece el texto del control
    Public Property Text_2() As String
        Get
            Return mText_2
        End Get
        Set(ByVal Value As String)

            mText_2 = Value
        End Set
    End Property
    'Devuelve o establece el texto del control
    Public Property Text_3() As String
        Get
            Return mText_3
        End Get
        Set(ByVal Value As String)

            mText_3 = Value
        End Set
    End Property
    'Devuelve o establece el texto del control
    Public Property Text_4() As String
        Get
            Return mText_4
        End Get
        Set(ByVal Value As String)

            mText_4 = Value
        End Set
    End Property

#End Region

#Region "M�todo WndProc sobreescrito"
    'Procesa los mensajes recibidos del sistema operativo
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_CHAR As Integer = 258

        If m.Msg = WM_CHAR Then

            If Me.validar(m) Then
                MyBase.WndProc(m)
            End If

        Else
            MyBase.WndProc(m)
        End If

    End Sub
#End Region

#Region "Propiedades p�blicas nuevas"
    'Devuelve o establece el n�mero de decimales para un formato
    'FixedPointNumber
    Public Property Decimals() As Byte
        Get
            Return Me.mDecimals
        End Get
        Set(ByVal Value As Byte)
            Me.mDecimals = Value
        End Set
    End Property

    'Devuelve o establece el separador decimal
    Public Property DecSeparator() As Char
        Get
            Return Me.mDecSeparator
        End Get
        Set(ByVal Value As Char)
           

            If Value = "." OrElse Value = "," Then
                Me.mDecSeparator = Value
            End If

            If Me.EsFormatoDecimal() Then
                Me.ActualizarSeparador()
            End If
        End Set
    End Property

    'Devuelve o establece el formato de entrada en la caja
    'de texto
    Public Property Format() As tbFormats
        Get
            Return Me.mFormat
        End Get
        Set(ByVal Value As tbFormats)
            Me.mFormat = Value

            Select Case Value
                Case tbFormats.BynaryNumber
                    okValues = "01"
                Case tbFormats.HexadecNumber
                    okValues = "0123456789AaBbCcDdEeFf"
                Case tbFormats.NoSpacedAlphabetic
                    okValues = "abcdefghijklmn�opqrstuvwxyz��������������������"
                    okValues &= okValues.ToUpper()
                Case tbFormats.OctalNumber
                    okValues = "01234567"
                Case tbFormats.SpacedAlphabetic
                    okValues = "abcdefghijklmn�opqrstuvwxyz��������������������"
                    okValues &= okValues.ToUpper() & " "
                Case tbFormats.UnsignedNumber
                    okValues = "0123456789"
                Case tbFormats.UserDefined
                    okValues = Me.mUserValues
                Case Else
                    okValues = ""
            End Select

            okValues &= Me.ControlKeys

            Me.Text = MyBase.Text
        End Set
    End Property

    'Devuelve o establece la cadena de d�gitos v�lidos para
    'un formato UserDefined
    Public Property UserValues() As String
        Get
            Return Me.mUserValues
        End Get
        Set(ByVal Value As String)
            Me.mUserValues = Value

            If Me.Format = tbFormats.UserDefined Then
                okValues = Value & Me.ControlKeys
                Me.Text = MyBase.Text
            End If
        End Set
    End Property

#End Region

#Region "M�todos nuevos de conversi�n"
    'Devuelve el contenido de la caja de texto como un valor
    'de tipo Double, considerando el formato actual de entrada
    Public Function ToDouble() As Double
        Select Case Me.Format
            Case tbFormats.HexadecNumber
                Return CDbl(Me.BaseADecimal(16))

            Case tbFormats.OctalNumber
                Return CDbl(Me.BaseADecimal(8))

            Case tbFormats.BynaryNumber
                Return CDbl(Me.BaseADecimal(2))

            Case tbFormats.NoSpacedAlphabetic, _
                 tbFormats.NoSpacedAlphaNumeric, _
                 tbFormats.SpacedAlphabetic, _
                 tbFormats.SpacedAlphaNumeric, tbFormats.UserDefined

                Try
                    Return Double.Parse(Me.Text)
                Catch
                    Return 0D
                End Try

            Case tbFormats.SignedNumber, tbFormats.UnsignedNumber
                Return CDbl(Me.Text)

            Case Else
                Dim s() As Char = Me.Text.ToCharArray()
                Dim stext As String = ""
                Dim i As Integer

                For i = 0 To s.Length - 1
                    If s(i) = "." Then
                        s(i) = ","
                    End If

                    stext &= s(i).ToString()
                Next i

                Return Double.Parse(stext, Globalization.NumberStyles.Float)
        End Select
    End Function

    'Devuelve el contenido del TextBox como un valor de tipo
    'Long, considerando el formato actual de entrada
    Public Function ToInt64() As Long
        Return CLng(Me.ToDouble())
    End Function

    'Devuelve una cadena con el contenido del TextBox en base
    'octal, considerando el formato actual de entrada
    Public Function ToOctal() As String
        Return Me.DecimalABase(Me.ToInt64(), 8)
    End Function

    'Devuelve una cadena con el contenido del TextBox en base
    'hexadecimal, considerando el formato actual de entrada
    Public Function ToHexadecimal() As String
        Return Me.DecimalABase(Me.ToInt64(), 16)
    End Function

    'Devuelve una cadena con el contenido del TextBox en base
    'binaria, considerando el formato actual de entrada
    Public Function ToBynary() As String
        Return Me.DecimalABase(Me.ToInt64(), 2)
    End Function
#End Region

    Private Sub FormattedTextBoxVB_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim j As Integer
        j = "0.25" * 4
        If j = 1 Then
            Me.mDecSeparator = "." '.
        Else
            Me.mDecSeparator = "," ',
        End If
    End Sub

End Class
