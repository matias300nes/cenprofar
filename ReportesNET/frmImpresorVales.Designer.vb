<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImpresorVales
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.txtNroVale = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnReImprimir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(253, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Esperando por Vales..."
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(106, 122)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(50, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "1"
        '
        'txtNroVale
        '
        Me.txtNroVale.Location = New System.Drawing.Point(219, 122)
        Me.txtNroVale.Name = "txtNroVale"
        Me.txtNroVale.Size = New System.Drawing.Size(100, 20)
        Me.txtNroVale.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Cant. de Copias:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10000
        '
        'btnReImprimir
        '
        Me.btnReImprimir.Location = New System.Drawing.Point(219, 156)
        Me.btnReImprimir.Name = "btnReImprimir"
        Me.btnReImprimir.Size = New System.Drawing.Size(100, 29)
        Me.btnReImprimir.TabIndex = 4
        Me.btnReImprimir.Text = "Re-Imprimir Vales"
        Me.btnReImprimir.UseVisualStyleBackColor = True
        '
        'frmImpresorVales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(351, 197)
        Me.Controls.Add(Me.btnReImprimir)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNroVale)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmImpresorVales"
        Me.Text = "frmImpresorVales"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNroVale As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnReImprimir As System.Windows.Forms.Button
End Class
