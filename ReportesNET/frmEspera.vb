Public Class frmEspera
    Public Maximo As Long
    Public Solo_Esperar As Boolean = False

    Public Sub SoloEspera(ByVal s As Boolean)
        'si es verdadero oculta el progress bar y muesta el redondelito
        Solo_Esperar = s
    End Sub

    Private Sub frmEspera_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' ocultar/mostrar el progress
        ProgressBar1.Visible = Not Solo_Esperar
        PicBox.Visible = Solo_Esperar

        ProgressBar1.Minimum = 0
        If Maximo <> 0 Then
            ProgressBar1.Maximum = Maximo
        Else
            ProgressBar1.Maximum = 100
        End If
        ProgressBar1.Value = 0
    End Sub

    Public Sub valor_progress(ByVal valor As Long)
        ProgressBar1.Value = valor
        Me.Refresh()
    End Sub
End Class