


'ESTO VA EN EL ARCHIVO Designer.vb DEL MISMO FORMULARIO.....

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsultarPrecios

    Inherits frmBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lstProductos = New System.Windows.Forms.ListBox()
        Me.txtProducto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.PanelContenedor = New System.Windows.Forms.Panel()
        Me.lblPMay = New System.Windows.Forms.Label()
        Me.lblPMin = New System.Windows.Forms.Label()
        Me.lblPrecioMayorista = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPrecioMinorista = New System.Windows.Forms.Label()
        Me.PictureBoxPorkys = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.PanelContenedor.SuspendLayout()
        CType(Me.PictureBoxPorkys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.lstProductos)
        Me.GroupBox1.Controls.Add(Me.txtProducto)
        Me.GroupBox1.Controls.Add(Me.PanelContenedor)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(0, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1460, 804)
        '
        '
        '
        Me.GroupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupBox1.Style.BackColorGradientAngle = 90
        Me.GroupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor2
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
        Me.GroupBox1.TabIndex = 0
        '
        'lstProductos
        '
        Me.lstProductos.BackColor = System.Drawing.Color.White
        Me.lstProductos.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProductos.FormattingEnabled = True
        Me.lstProductos.ItemHeight = 31
        Me.lstProductos.Location = New System.Drawing.Point(27, 319)
        Me.lstProductos.Margin = New System.Windows.Forms.Padding(4)
        Me.lstProductos.Name = "lstProductos"
        Me.lstProductos.Size = New System.Drawing.Size(818, 35)
        Me.lstProductos.TabIndex = 1
        '
        'txtProducto
        '
        Me.txtProducto.AccessibleName = ""
        Me.txtProducto.Decimals = CType(2, Byte)
        Me.txtProducto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducto.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtProducto.Location = New System.Drawing.Point(27, 281)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProducto.MaxLength = 50
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(818, 38)
        Me.txtProducto.TabIndex = 0
        Me.txtProducto.Text_1 = Nothing
        Me.txtProducto.Text_2 = Nothing
        Me.txtProducto.Text_3 = Nothing
        Me.txtProducto.Text_4 = Nothing
        Me.txtProducto.UserValues = Nothing
        '
        'PanelContenedor
        '
        Me.PanelContenedor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelContenedor.AutoSize = True
        Me.PanelContenedor.BackColor = System.Drawing.SystemColors.Control
        Me.PanelContenedor.Controls.Add(Me.lblPMay)
        Me.PanelContenedor.Controls.Add(Me.lblPMin)
        Me.PanelContenedor.Controls.Add(Me.lblPrecioMayorista)
        Me.PanelContenedor.Controls.Add(Me.Label3)
        Me.PanelContenedor.Controls.Add(Me.lblPrecioMinorista)
        Me.PanelContenedor.Controls.Add(Me.PictureBoxPorkys)
        Me.PanelContenedor.Font = New System.Drawing.Font("Times New Roman", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelContenedor.Location = New System.Drawing.Point(9, 52)
        Me.PanelContenedor.Name = "PanelContenedor"
        Me.PanelContenedor.Size = New System.Drawing.Size(1436, 721)
        Me.PanelContenedor.TabIndex = 120
        '
        'lblPMay
        '
        Me.lblPMay.AutoSize = True
        Me.lblPMay.BackColor = System.Drawing.Color.Transparent
        Me.lblPMay.Font = New System.Drawing.Font("Elephant", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMay.ForeColor = System.Drawing.Color.Black
        Me.lblPMay.Location = New System.Drawing.Point(892, 375)
        Me.lblPMay.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPMay.Name = "lblPMay"
        Me.lblPMay.Size = New System.Drawing.Size(312, 42)
        Me.lblPMay.TabIndex = 122
        Me.lblPMay.Text = "Precio Mayorista:"
        Me.lblPMay.Visible = False
        '
        'lblPMin
        '
        Me.lblPMin.AutoSize = True
        Me.lblPMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPMin.Font = New System.Drawing.Font("Elephant", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMin.ForeColor = System.Drawing.Color.Black
        Me.lblPMin.Location = New System.Drawing.Point(895, 13)
        Me.lblPMin.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPMin.Name = "lblPMin"
        Me.lblPMin.Size = New System.Drawing.Size(309, 42)
        Me.lblPMin.TabIndex = 120
        Me.lblPMin.Text = "Precio Minorista:"
        Me.lblPMin.Visible = False
        '
        'lblPrecioMayorista
        '
        Me.lblPrecioMayorista.BackColor = System.Drawing.Color.White
        Me.lblPrecioMayorista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrecioMayorista.Font = New System.Drawing.Font("Microsoft Sans Serif", 61.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrecioMayorista.Location = New System.Drawing.Point(899, 431)
        Me.lblPrecioMayorista.Name = "lblPrecioMayorista"
        Me.lblPrecioMayorista.Size = New System.Drawing.Size(444, 278)
        Me.lblPrecioMayorista.TabIndex = 121
        Me.lblPrecioMayorista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPrecioMayorista.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Elephant", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(11, 185)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(433, 42)
        Me.Label3.TabIndex = 117
        Me.Label3.Text = "BUSCAR PRODUCTO :"
        '
        'lblPrecioMinorista
        '
        Me.lblPrecioMinorista.BackColor = System.Drawing.Color.White
        Me.lblPrecioMinorista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrecioMinorista.Font = New System.Drawing.Font("Microsoft Sans Serif", 61.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrecioMinorista.Location = New System.Drawing.Point(899, 72)
        Me.lblPrecioMinorista.Name = "lblPrecioMinorista"
        Me.lblPrecioMinorista.Size = New System.Drawing.Size(444, 278)
        Me.lblPrecioMinorista.TabIndex = 118
        Me.lblPrecioMinorista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPrecioMinorista.Visible = False
        '
        'PictureBoxPorkys
        '
        Me.PictureBoxPorkys.Image = Global.PORKYS.My.Resources.Resources.logo1
        Me.PictureBoxPorkys.Location = New System.Drawing.Point(857, 129)
        Me.PictureBoxPorkys.Name = "PictureBoxPorkys"
        Me.PictureBoxPorkys.Size = New System.Drawing.Size(513, 367)
        Me.PictureBoxPorkys.TabIndex = 119
        Me.PictureBoxPorkys.TabStop = False
        '
        'frmConsultarPrecios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1460, 851)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConsultarPrecios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consultar Precios"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PanelContenedor.ResumeLayout(False)
        Me.PanelContenedor.PerformLayout()
        CType(Me.PictureBoxPorkys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtProducto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstProductos As System.Windows.Forms.ListBox
    Friend WithEvents lblPrecioMinorista As System.Windows.Forms.Label
    Friend WithEvents PictureBoxPorkys As System.Windows.Forms.PictureBox
    Friend WithEvents PanelContenedor As System.Windows.Forms.Panel
    Friend WithEvents lblPMin As System.Windows.Forms.Label
    Friend WithEvents lblPrecioMayorista As System.Windows.Forms.Label
    Friend WithEvents lblPMay As System.Windows.Forms.Label







End Class
