Public Class LabelArray
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

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        ' '' '' '' '' ''MessageBox.Show("you have clicked Label " & CType(CType(sender, System.Windows.Forms.Label).Tag, String))
    End Sub
    
    Public Function AddNewLabel(ByVal textoDelLabel As String) As System.Windows.Forms.Label
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
        aLabel.Left = 25
        aLabel.Tag = Me.Count
        aLabel.AutoSize = True
        ''aLabel.Text = "Label " & Me.Count.ToString
        AddHandler aLabel.Click, AddressOf ClickHandler
        Return aLabel
    End Function

    Public Sub New(ByVal host As System.Windows.Forms.Form, ByVal tipocontrol As String, ByVal parametro As String)
        HostForm = host
        Me.AddNewLabel(parametro)
    End Sub
End Class
