<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListaPrecios

    Inherits SEYC.frmBase

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListaPrecios))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPorcentaje = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkEliminados = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkPrincipal = New System.Windows.Forms.CheckBox()
        Me.chkPeron = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(170, 28)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(169, 24)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.chkPeron)
        Me.GroupBox1.Controls.Add(Me.chkPrincipal)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPorcentaje)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDescripcion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(754, 138)
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
        Me.GroupBox1.TabIndex = 0
        '
        'txtCODIGO
        '
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Enabled = False
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(4, 43)
        Me.txtCODIGO.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(129, 22)
        Me.txtCODIGO.TabIndex = 52
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(4, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Código"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(518, 22)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Porcentaje (%)*"
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.AccessibleName = "*PORCENTAJE"
        Me.txtPorcentaje.Decimals = CType(10, Byte)
        Me.txtPorcentaje.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPorcentaje.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPorcentaje.Location = New System.Drawing.Point(520, 43)
        Me.txtPorcentaje.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPorcentaje.MaxLength = 100
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.Size = New System.Drawing.Size(99, 22)
        Me.txtPorcentaje.TabIndex = 3
        Me.txtPorcentaje.Text_1 = Nothing
        Me.txtPorcentaje.Text_2 = Nothing
        Me.txtPorcentaje.Text_3 = Nothing
        Me.txtPorcentaje.Text_4 = Nothing
        Me.txtPorcentaje.UserValues = Nothing
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chkEliminados.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.Location = New System.Drawing.Point(566, 110)
        Me.chkEliminados.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(119, 18)
        Me.chkEliminados.TabIndex = 4
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.TextColor = System.Drawing.Color.Red
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(421, 4)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(27, 22)
        Me.txtID.TabIndex = 8
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(456, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AccessibleName = "*DESCRIPCIÓN"
        Me.txtDescripcion.Decimals = CType(2, Byte)
        Me.txtDescripcion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescripcion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtDescripcion.Location = New System.Drawing.Point(147, 43)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescripcion.MaxLength = 100
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(359, 22)
        Me.txtDescripcion.TabIndex = 0
        Me.txtDescripcion.Text_1 = Nothing
        Me.txtDescripcion.Text_2 = Nothing
        Me.txtDescripcion.Text_3 = Nothing
        Me.txtDescripcion.Text_4 = Nothing
        Me.txtDescripcion.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(144, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Descripción*"
        '
        'chkPrincipal
        '
        Me.chkPrincipal.AutoSize = True
        Me.chkPrincipal.Location = New System.Drawing.Point(645, 30)
        Me.chkPrincipal.Name = "chkPrincipal"
        Me.chkPrincipal.Size = New System.Drawing.Size(84, 21)
        Me.chkPrincipal.TabIndex = 54
        Me.chkPrincipal.Text = "Principal"
        Me.chkPrincipal.UseVisualStyleBackColor = True
        '
        'chkPeron
        '
        Me.chkPeron.AutoSize = True
        Me.chkPeron.Location = New System.Drawing.Point(645, 57)
        Me.chkPeron.Name = "chkPeron"
        Me.chkPeron.Size = New System.Drawing.Size(68, 21)
        Me.chkPeron.TabIndex = 55
        Me.chkPeron.Text = "Perón"
        Me.chkPeron.UseVisualStyleBackColor = True
        '
        'frmListaPrecios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 534)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = false
        Me.Name = "frmListaPrecios"
        Me.Text = "Lista de Precios"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub















    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Private WithEvents chkEliminados As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtPorcentaje As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkPeron As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrincipal As System.Windows.Forms.CheckBox

End Class
