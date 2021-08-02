<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTipoMoneda
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTipoMoneda))
        Me.gpNota = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.txtEuro = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtDolar = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtPeso = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.btnGuardar = New DevComponents.DotNetBar.ButtonX
        Me.lblInfo = New DevComponents.DotNetBar.LabelX
        Me.lblEuro = New DevComponents.DotNetBar.LabelX
        Me.lblDolar = New DevComponents.DotNetBar.LabelX
        Me.lblPesos = New DevComponents.DotNetBar.LabelX
        Me.gpNota.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpNota
        '
        Me.gpNota.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpNota.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpNota.Controls.Add(Me.txtEuro)
        Me.gpNota.Controls.Add(Me.txtDolar)
        Me.gpNota.Controls.Add(Me.txtPeso)
        Me.gpNota.Controls.Add(Me.btnGuardar)
        Me.gpNota.Controls.Add(Me.lblInfo)
        Me.gpNota.Controls.Add(Me.lblEuro)
        Me.gpNota.Controls.Add(Me.lblDolar)
        Me.gpNota.Controls.Add(Me.lblPesos)
        Me.gpNota.Location = New System.Drawing.Point(12, 12)
        Me.gpNota.Name = "gpNota"
        Me.gpNota.Size = New System.Drawing.Size(226, 151)
        '
        '
        '
        Me.gpNota.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpNota.Style.BackColorGradientAngle = 90
        Me.gpNota.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpNota.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpNota.Style.BorderBottomWidth = 1
        Me.gpNota.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpNota.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpNota.Style.BorderLeftWidth = 1
        Me.gpNota.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpNota.Style.BorderRightWidth = 1
        Me.gpNota.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpNota.Style.BorderTopWidth = 1
        Me.gpNota.Style.CornerDiameter = 4
        Me.gpNota.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpNota.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.gpNota.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpNota.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        Me.gpNota.TabIndex = 5
        '
        'txtEuro
        '
        Me.txtEuro.Decimals = CType(3, Byte)
        Me.txtEuro.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEuro.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtEuro.Location = New System.Drawing.Point(93, 64)
        Me.txtEuro.MaxLength = 5
        Me.txtEuro.Name = "txtEuro"
        Me.txtEuro.Size = New System.Drawing.Size(65, 20)
        Me.txtEuro.TabIndex = 2
        Me.txtEuro.Text_1 = Nothing
        Me.txtEuro.Text_2 = Nothing
        Me.txtEuro.Text_3 = Nothing
        Me.txtEuro.Text_4 = Nothing
        Me.txtEuro.UserValues = Nothing
        '
        'txtDolar
        '
        Me.txtDolar.Decimals = CType(3, Byte)
        Me.txtDolar.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDolar.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtDolar.Location = New System.Drawing.Point(93, 38)
        Me.txtDolar.MaxLength = 5
        Me.txtDolar.Name = "txtDolar"
        Me.txtDolar.Size = New System.Drawing.Size(65, 20)
        Me.txtDolar.TabIndex = 1
        Me.txtDolar.Text_1 = Nothing
        Me.txtDolar.Text_2 = Nothing
        Me.txtDolar.Text_3 = Nothing
        Me.txtDolar.Text_4 = Nothing
        Me.txtDolar.UserValues = Nothing
        '
        'txtPeso
        '
        Me.txtPeso.Decimals = CType(3, Byte)
        Me.txtPeso.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPeso.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtPeso.Location = New System.Drawing.Point(93, 12)
        Me.txtPeso.MaxLength = 5
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(65, 20)
        Me.txtPeso.TabIndex = 0
        Me.txtPeso.Text_1 = Nothing
        Me.txtPeso.Text_2 = Nothing
        Me.txtPeso.Text_3 = Nothing
        Me.txtPeso.Text_4 = Nothing
        Me.txtPeso.UserValues = Nothing
        '
        'btnGuardar
        '
        Me.btnGuardar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGuardar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGuardar.Location = New System.Drawing.Point(93, 117)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(65, 23)
        Me.btnGuardar.TabIndex = 3
        Me.btnGuardar.Text = "Guardar"
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.BackColor = System.Drawing.Color.Transparent
        Me.lblInfo.Location = New System.Drawing.Point(3, 90)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(214, 15)
        Me.lblInfo.TabIndex = 26
        Me.lblInfo.Text = "Modificar el Valor y Hacer click en Guardar."
        '
        'lblEuro
        '
        Me.lblEuro.AutoSize = True
        Me.lblEuro.BackColor = System.Drawing.Color.Transparent
        Me.lblEuro.Location = New System.Drawing.Point(59, 66)
        Me.lblEuro.Name = "lblEuro"
        Me.lblEuro.Size = New System.Drawing.Size(26, 15)
        Me.lblEuro.TabIndex = 23
        Me.lblEuro.Text = "Euro"
        '
        'lblDolar
        '
        Me.lblDolar.AutoSize = True
        Me.lblDolar.BackColor = System.Drawing.Color.Transparent
        Me.lblDolar.Location = New System.Drawing.Point(58, 40)
        Me.lblDolar.Name = "lblDolar"
        Me.lblDolar.Size = New System.Drawing.Size(29, 15)
        Me.lblDolar.TabIndex = 22
        Me.lblDolar.Text = "Dolar"
        '
        'lblPesos
        '
        Me.lblPesos.AutoSize = True
        Me.lblPesos.BackColor = System.Drawing.Color.Transparent
        Me.lblPesos.Location = New System.Drawing.Point(59, 14)
        Me.lblPesos.Name = "lblPesos"
        Me.lblPesos.Size = New System.Drawing.Size(28, 15)
        Me.lblPesos.TabIndex = 10
        Me.lblPesos.Text = "Peso"
        '
        'frmTipoMoneda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(252, 171)
        Me.Controls.Add(Me.gpNota)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmTipoMoneda"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cotización de Moneda"
        Me.gpNota.ResumeLayout(False)
        Me.gpNota.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpNota As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents lblDolar As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPesos As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblEuro As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblInfo As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGuardar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtEuro As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtDolar As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPeso As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
