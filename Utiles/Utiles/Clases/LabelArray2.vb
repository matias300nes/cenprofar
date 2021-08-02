Imports Utiles
Public Class LabelArray2
    Inherits System.Collections.CollectionBase
    ' Visual Basic
    Private ReadOnly HostForm As System.Windows.Forms.Form

    Default Public ReadOnly Property Item(ByVal Index As Integer) As System.Windows.Forms.Label
        Get
            Return CType(Me.List.Item(Index), System.Windows.Forms.Label)
        End Get
    End Property

    Public Sub Remove()
        ' Check to be sure there is a button to remove.
        If Me.Count > 0 Then
            ' Remove the last button added to the array from the host form 
            ' controls collection. Note the use of the default property in 
            ' accessing the array.

            HostForm.Controls.Remove(Me(Me.Count - 1))
            Me.List.RemoveAt(Me.Count - 1)
        End If
    End Sub

    Public Sub RemoveAll()
        ' Check to be sure there is a button to remove.
        If Me.Count > 0 Then
            ' Remove the last button added to the array from the host form 
            ' controls collection. Note the use of the default property in 
            ' accessing the array.
            While Me.Count <> 0
                HostForm.Controls.Remove(Me(Me.Count - 1))
                Me.List.RemoveAt(Me.Count - 1)
            End While

            'HostForm.Controls.Remove(Me(Me.Count - 1))
            'Me.List.RemoveAt(Me.Count - 1)
        End If
    End Sub

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        ' '' '' '' '' ''MessageBox.Show("you have clicked Label " & CType(CType(sender, System.Windows.Forms.Label).Tag, String))
    End Sub

    Public Function AddNewLabel(ByVal textoDelLabel As String, _
                                ByVal top As Integer, _
                                ByVal color As Integer) As System.Windows.Forms.Label


        ' Create a new instance of the Button class.
        Dim aLabel As New System.Windows.Forms.Label()
        ' Add the button to the collection's internal list.
        Me.List.Add(aLabel)
        ' Add the button to the controls collection of the form 
        ' referenced by the HostForm field.
        HostForm.Controls.Add(aLabel)

        ' Set intial properties for the button object.
        aLabel.AutoSize = False
        'aLabel.Text = "" 'textoDelLabel 'Count.ToString
        aLabel.Width = Int(ancho_de_la_barra)
        aLabel.Height = alto_de_la_barra
        aLabel.Top = top
        aLabel.Left = distancia_desde_borde + (ancho_de_la_barra * (contador_aux - 1)) 'contador_aux esta declarada en la util
        'aLabel.BringToFront()
        aLabel.BorderStyle = Windows.Forms.BorderStyle.FixedSingle

        If bandera_fuente = True Then
            aLabel.Font = New System.Drawing.Font("Arial", 9, Drawing.FontStyle.Bold)
            aLabel.ForeColor = Drawing.Color.Black
            'aLabel.Image = My.Resources.flecha_celeste
            aLabel.Text = textoDelLabel
            aLabel.TextAlign = Drawing.ContentAlignment.MiddleCenter
            bandera_fuente = False
        End If

        If color = 1 Then
            aLabel.BackColor = Drawing.Color.Green
        ElseIf color = 2 Then
            aLabel.BackColor = Drawing.Color.Yellow
        Else
            aLabel.BackColor = Drawing.Color.Red
        End If

        AddHandler aLabel.Click, AddressOf ClickHandler
        Return aLabel

    End Function

    Public Sub New(ByVal host As System.Windows.Forms.Form, ByVal tipocontrol As String, ByVal parametro As String, ByVal parametro2 As Integer, ByVal parametro3 As Integer)
        HostForm = host
        Me.AddNewLabel(parametro, parametro2, parametro3)
    End Sub

    Public Sub New(ByVal host As System.Windows.Forms.Form, ByVal tipocontrol As String, ByVal parametro As String)
        HostForm = host
        Me.AddNewLabel2("")
    End Sub

    Public Function AddNewLabel2(ByVal textoDelLabel As String) As System.Windows.Forms.Label

        ' Create a new instance of the Button class.
        Dim aLabel As New System.Windows.Forms.Label()


        ' Add the button to the collection's internal list.
        'Me.List.Add(aLabel)


        ' Add the button to the controls collection of the form 
        ' referenced by the HostForm field.
        'HostForm.Controls.Add(aLabel)

        'aLabel.Width = 300
        'aLabel.Text = "d"
        'aLabel.Top = 500


        AddHandler aLabel.Click, AddressOf ClickHandler
        Return aLabel

    End Function

End Class
