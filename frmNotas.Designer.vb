<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotas
    Inherits SEYC.frmBase

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
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.rdTodos = New System.Windows.Forms.RadioButton()
        Me.rdDolar = New System.Windows.Forms.RadioButton()
        Me.rdPeso = New System.Windows.Forms.RadioButton()
        Me.chkEliminados = New System.Windows.Forms.CheckBox()
        Me.txtOrden = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.chkObligatorio = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDNota = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblIDNota = New DevComponents.DotNetBar.LabelX()
        Me.lblNota = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.rdTodos)
        Me.GroupBox1.Controls.Add(Me.rdDolar)
        Me.GroupBox1.Controls.Add(Me.rdPeso)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.txtOrden)
        Me.GroupBox1.Controls.Add(Me.LabelX1)
        Me.GroupBox1.Controls.Add(Me.chkObligatorio)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.txtIDNota)
        Me.GroupBox1.Controls.Add(Me.lblIDNota)
        Me.GroupBox1.Controls.Add(Me.lblNota)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(968, 103)
        '
        '
        '
        Me.GroupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupBox1.Style.BackColorGradientAngle = 90
        Me.GroupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupBox1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderBottomWidth = 1
        Me.GroupBox1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupBox1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderLeftWidth = 1
        Me.GroupBox1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderRightWidth = 1
        Me.GroupBox1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderTopWidth = 1
        Me.GroupBox1.Style.CornerDiameter = 4
        Me.GroupBox1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupBox1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupBox1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupBox1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupBox1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupBox1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupBox1.TabIndex = 63
        '
        'rdTodos
        '
        Me.rdTodos.AutoSize = True
        Me.rdTodos.BackColor = System.Drawing.Color.Transparent
        Me.rdTodos.ForeColor = System.Drawing.Color.Blue
        Me.rdTodos.Location = New System.Drawing.Point(610, 28)
        Me.rdTodos.Name = "rdTodos"
        Me.rdTodos.Size = New System.Drawing.Size(204, 17)
        Me.rdTodos.TabIndex = 25
        Me.rdTodos.TabStop = True
        Me.rdTodos.Text = "Aplicable para todos los Presupuestos"
        Me.rdTodos.UseVisualStyleBackColor = False
        '
        'rdDolar
        '
        Me.rdDolar.AutoSize = True
        Me.rdDolar.BackColor = System.Drawing.Color.Transparent
        Me.rdDolar.ForeColor = System.Drawing.Color.Blue
        Me.rdDolar.Location = New System.Drawing.Point(529, 51)
        Me.rdDolar.Name = "rdDolar"
        Me.rdDolar.Size = New System.Drawing.Size(180, 17)
        Me.rdDolar.TabIndex = 24
        Me.rdDolar.TabStop = True
        Me.rdDolar.Text = "Solo para Presupuestos en Dolar"
        Me.rdDolar.UseVisualStyleBackColor = False
        '
        'rdPeso
        '
        Me.rdPeso.AutoSize = True
        Me.rdPeso.BackColor = System.Drawing.Color.Transparent
        Me.rdPeso.ForeColor = System.Drawing.Color.Blue
        Me.rdPeso.Location = New System.Drawing.Point(529, 74)
        Me.rdPeso.Name = "rdPeso"
        Me.rdPeso.Size = New System.Drawing.Size(184, 17)
        Me.rdPeso.TabIndex = 23
        Me.rdPeso.TabStop = True
        Me.rdPeso.Text = "Solo para Presupuestos en Pesos"
        Me.rdPeso.UseVisualStyleBackColor = False
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.ForeColor = System.Drawing.Color.Red
        Me.chkEliminados.Location = New System.Drawing.Point(850, 77)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(109, 17)
        Me.chkEliminados.TabIndex = 3
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.UseVisualStyleBackColor = False
        '
        'txtOrden
        '
        Me.txtOrden.AccessibleName = ""
        Me.txtOrden.Decimals = CType(2, Byte)
        Me.txtOrden.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOrden.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtOrden.Location = New System.Drawing.Point(836, -3)
        Me.txtOrden.MaxLength = 100
        Me.txtOrden.Name = "txtOrden"
        Me.txtOrden.Size = New System.Drawing.Size(33, 20)
        Me.txtOrden.TabIndex = 22
        Me.txtOrden.Text = "0"
        Me.txtOrden.Text_1 = Nothing
        Me.txtOrden.Text_2 = Nothing
        Me.txtOrden.Text_3 = Nothing
        Me.txtOrden.Text_4 = Nothing
        Me.txtOrden.UserValues = Nothing
        Me.txtOrden.Visible = False
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(771, 2)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(37, 15)
        Me.LabelX1.TabIndex = 21
        Me.LabelX1.Text = "Orden*"
        Me.LabelX1.Visible = False
        '
        'chkObligatorio
        '
        Me.chkObligatorio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chkObligatorio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkObligatorio.Location = New System.Drawing.Point(529, 24)
        Me.chkObligatorio.Name = "chkObligatorio"
        Me.chkObligatorio.Size = New System.Drawing.Size(75, 23)
        Me.chkObligatorio.TabIndex = 1
        Me.chkObligatorio.Text = "Obligatorio"
        Me.chkObligatorio.TextColor = System.Drawing.Color.Blue
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "*NOTA"
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(3, 28)
        Me.txtNota.MaxLength = 400
        Me.txtNota.Multiline = True
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(520, 66)
        Me.txtNota.TabIndex = 0
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'txtIDNota
        '
        Me.txtIDNota.AcceptsReturn = True
        Me.txtIDNota.AcceptsTab = True
        '
        '
        '
        Me.txtIDNota.Border.Class = "TextBoxBorder"
        Me.txtIDNota.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtIDNota.Location = New System.Drawing.Point(704, 7)
        Me.txtIDNota.Name = "txtIDNota"
        Me.txtIDNota.Size = New System.Drawing.Size(40, 20)
        Me.txtIDNota.TabIndex = 18
        Me.txtIDNota.Visible = False
        '
        'lblIDNota
        '
        '
        '
        '
        Me.lblIDNota.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblIDNota.Location = New System.Drawing.Point(484, 1)
        Me.lblIDNota.Name = "lblIDNota"
        Me.lblIDNota.Size = New System.Drawing.Size(20, 23)
        Me.lblIDNota.TabIndex = 17
        Me.lblIDNota.Text = "ID"
        Me.lblIDNota.Visible = False
        '
        'lblNota
        '
        Me.lblNota.AutoSize = True
        Me.lblNota.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblNota.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblNota.Location = New System.Drawing.Point(3, 9)
        Me.lblNota.Name = "lblNota"
        Me.lblNota.Size = New System.Drawing.Size(30, 15)
        Me.lblNota.TabIndex = 10
        Me.lblNota.Text = "Nota*"
        '
        'frmNotas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(992, 396)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmNotas"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtIDNota As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblIDNota As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblNota As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkObligatorio As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtOrden As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkEliminados As System.Windows.Forms.CheckBox
    Friend WithEvents rdTodos As System.Windows.Forms.RadioButton
    Friend WithEvents rdDolar As System.Windows.Forms.RadioButton
    Friend WithEvents rdPeso As System.Windows.Forms.RadioButton

End Class
