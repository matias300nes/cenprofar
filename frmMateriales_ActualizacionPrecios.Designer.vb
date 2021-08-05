<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMateriales_ActualizacionPrecios
    Inherits frmBase

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
        Me.components = New System.ComponentModel.Container()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnAplicarMasivo = New DevComponents.DotNetBar.ButtonX()
        Me.Line1 = New DevComponents.DotNetBar.Controls.Line()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkActMasivo = New System.Windows.Forms.CheckBox()
        Me.txtFiltro = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPorcentaje = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbRubros = New System.Windows.Forms.ComboBox()
        Me.cmbSubrubros = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 84)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.BuscarToolStripMenuItem.Text = "Buscar..."
        Me.BuscarToolStripMenuItem.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem
        '
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 28)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.btnAplicarMasivo)
        Me.GroupBox1.Controls.Add(Me.Line1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkActMasivo)
        Me.GroupBox1.Controls.Add(Me.txtFiltro)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPorcentaje)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbRubros)
        Me.GroupBox1.Controls.Add(Me.cmbSubrubros)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1795, 582)
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
        'btnAplicarMasivo
        '
        Me.btnAplicarMasivo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAplicarMasivo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAplicarMasivo.Location = New System.Drawing.Point(1315, 31)
        Me.btnAplicarMasivo.Name = "btnAplicarMasivo"
        Me.btnAplicarMasivo.Size = New System.Drawing.Size(75, 23)
        Me.btnAplicarMasivo.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005
        Me.btnAplicarMasivo.TabIndex = 150
        Me.btnAplicarMasivo.Text = "Aplicar"
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.Color.Transparent
        Me.Line1.Location = New System.Drawing.Point(948, 4)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(21, 60)
        Me.Line1.TabIndex = 149
        Me.Line1.Text = "Line1"
        Me.Line1.VerticalLine = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(161, 12)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 17)
        Me.Label4.TabIndex = 148
        Me.Label4.Text = "Filtrar"
        '
        'chkActMasivo
        '
        Me.chkActMasivo.AutoSize = True
        Me.chkActMasivo.BackColor = System.Drawing.Color.Transparent
        Me.chkActMasivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActMasivo.Location = New System.Drawing.Point(975, 32)
        Me.chkActMasivo.Name = "chkActMasivo"
        Me.chkActMasivo.Size = New System.Drawing.Size(173, 21)
        Me.chkActMasivo.TabIndex = 3
        Me.chkActMasivo.Text = "Act. Masivo a Costo"
        Me.chkActMasivo.UseVisualStyleBackColor = False
        '
        'txtFiltro
        '
        Me.txtFiltro.AccessibleName = "Nota"
        Me.txtFiltro.BackColor = System.Drawing.Color.Aqua
        Me.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFiltro.Decimals = CType(2, Byte)
        Me.txtFiltro.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtFiltro.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtFiltro.Location = New System.Drawing.Point(164, 32)
        Me.txtFiltro.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFiltro.Name = "txtFiltro"
        Me.txtFiltro.Size = New System.Drawing.Size(473, 22)
        Me.txtFiltro.TabIndex = 1
        Me.txtFiltro.Text_1 = Nothing
        Me.txtFiltro.Text_2 = Nothing
        Me.txtFiltro.Text_3 = Nothing
        Me.txtFiltro.Text_4 = Nothing
        Me.txtFiltro.UserValues = Nothing
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(645, 32)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(299, 22)
        Me.txtNota.TabIndex = 2
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(642, 11)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 17)
        Me.Label8.TabIndex = 136
        Me.Label8.Text = "Nota"
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.ForeColor = System.Drawing.Color.Blue
        Me.chkEliminado.Location = New System.Drawing.Point(1690, 44)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(91, 21)
        Me.chkEliminado.TabIndex = 135
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = False
        Me.chkEliminado.Visible = False
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(21, 73)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1761, 498)
        Me.grdItems.TabIndex = 10
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1752, 4)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(29, 22)
        Me.txtID.TabIndex = 128
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
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(1724, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
        Me.Label1.TabIndex = 129
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.AccessibleName = "CODIGO"
        Me.txtPorcentaje.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentaje.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPorcentaje.Decimals = CType(2, Byte)
        Me.txtPorcentaje.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPorcentaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPorcentaje.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtPorcentaje.Location = New System.Drawing.Point(1167, 31)
        Me.txtPorcentaje.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPorcentaje.MaxLength = 25
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.Size = New System.Drawing.Size(129, 23)
        Me.txtPorcentaje.TabIndex = 4
        Me.txtPorcentaje.Text_1 = Nothing
        Me.txtPorcentaje.Text_2 = Nothing
        Me.txtPorcentaje.Text_3 = Nothing
        Me.txtPorcentaje.Text_4 = Nothing
        Me.txtPorcentaje.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(1163, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 17)
        Me.Label2.TabIndex = 131
        Me.Label2.Text = "Porcentaje"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(21, 32)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 22)
        Me.dtpFECHA.TabIndex = 0
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(18, 12)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 17)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Fecha"
        '
        'cmbRubros
        '
        Me.cmbRubros.AccessibleName = ""
        Me.cmbRubros.DropDownHeight = 450
        Me.cmbRubros.Enabled = False
        Me.cmbRubros.FormattingEnabled = True
        Me.cmbRubros.IntegralHeight = False
        Me.cmbRubros.Location = New System.Drawing.Point(832, 220)
        Me.cmbRubros.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbRubros.Name = "cmbRubros"
        Me.cmbRubros.Size = New System.Drawing.Size(436, 24)
        Me.cmbRubros.TabIndex = 5
        Me.cmbRubros.Visible = False
        '
        'cmbSubrubros
        '
        Me.cmbSubrubros.AccessibleName = ""
        Me.cmbSubrubros.DropDownHeight = 450
        Me.cmbSubrubros.Enabled = False
        Me.cmbSubrubros.FormattingEnabled = True
        Me.cmbSubrubros.IntegralHeight = False
        Me.cmbSubrubros.Location = New System.Drawing.Point(451, 220)
        Me.cmbSubrubros.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbSubrubros.Name = "cmbSubrubros"
        Me.cmbSubrubros.Size = New System.Drawing.Size(373, 24)
        Me.cmbSubrubros.TabIndex = 6
        Me.cmbSubrubros.Visible = False
        '
        'frmMateriales_ActualizacionPrecios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1827, 689)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmMateriales_ActualizacionPrecios"
        Me.Text = "frmAjustes"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPorcentaje As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents cmbSubrubros As System.Windows.Forms.ComboBox
    Public WithEvents cmbRubros As System.Windows.Forms.ComboBox
    Friend WithEvents txtFiltro As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkActMasivo As System.Windows.Forms.CheckBox
    Friend WithEvents Line1 As DevComponents.DotNetBar.Controls.Line
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnAplicarMasivo As DevComponents.DotNetBar.ButtonX
End Class
