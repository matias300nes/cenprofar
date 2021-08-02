<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAjustes
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
        Me.chkFiltrar = New System.Windows.Forms.CheckBox()
        Me.txtFiltro = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtidAlmacen = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.chkPorCodigo = New System.Windows.Forms.CheckBox()
        Me.rdPersonalizado = New System.Windows.Forms.RadioButton()
        Me.rdRubros = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbNombre = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbMotivo = New System.Windows.Forms.ComboBox()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
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
        Me.GroupBox1.Controls.Add(Me.chkFiltrar)
        Me.GroupBox1.Controls.Add(Me.txtFiltro)
        Me.GroupBox1.Controls.Add(Me.txtidAlmacen)
        Me.GroupBox1.Controls.Add(Me.cmbAlmacen)
        Me.GroupBox1.Controls.Add(Me.chkPorCodigo)
        Me.GroupBox1.Controls.Add(Me.rdPersonalizado)
        Me.GroupBox1.Controls.Add(Me.rdRubros)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbNombre)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbMotivo)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
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
        Me.GroupBox1.TabIndex = 67
        '
        'chkFiltrar
        '
        Me.chkFiltrar.AutoSize = True
        Me.chkFiltrar.BackColor = System.Drawing.Color.Transparent
        Me.chkFiltrar.Location = New System.Drawing.Point(21, 68)
        Me.chkFiltrar.Name = "chkFiltrar"
        Me.chkFiltrar.Size = New System.Drawing.Size(66, 21)
        Me.chkFiltrar.TabIndex = 146
        Me.chkFiltrar.Text = "Filtrar"
        Me.chkFiltrar.UseVisualStyleBackColor = False
        '
        'txtFiltro
        '
        Me.txtFiltro.AccessibleName = "Nota"
        Me.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFiltro.Decimals = CType(2, Byte)
        Me.txtFiltro.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtFiltro.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtFiltro.Location = New System.Drawing.Point(95, 66)
        Me.txtFiltro.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFiltro.Name = "txtFiltro"
        Me.txtFiltro.Size = New System.Drawing.Size(535, 22)
        Me.txtFiltro.TabIndex = 145
        Me.txtFiltro.Text_1 = Nothing
        Me.txtFiltro.Text_2 = Nothing
        Me.txtFiltro.Text_3 = Nothing
        Me.txtFiltro.Text_4 = Nothing
        Me.txtFiltro.UserValues = Nothing
        '
        'txtidAlmacen
        '
        Me.txtidAlmacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidAlmacen.Decimals = CType(2, Byte)
        Me.txtidAlmacen.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidAlmacen.Enabled = False
        Me.txtidAlmacen.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidAlmacen.Location = New System.Drawing.Point(197, 4)
        Me.txtidAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.txtidAlmacen.MaxLength = 8
        Me.txtidAlmacen.Name = "txtidAlmacen"
        Me.txtidAlmacen.Size = New System.Drawing.Size(29, 22)
        Me.txtidAlmacen.TabIndex = 143
        Me.txtidAlmacen.Text_1 = Nothing
        Me.txtidAlmacen.Text_2 = Nothing
        Me.txtidAlmacen.Text_3 = Nothing
        Me.txtidAlmacen.Text_4 = Nothing
        Me.txtidAlmacen.UserValues = Nothing
        Me.txtidAlmacen.Visible = False
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.AccessibleName = ""
        Me.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(21, 34)
        Me.cmbAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(205, 24)
        Me.cmbAlmacen.TabIndex = 0
        '
        'chkPorCodigo
        '
        Me.chkPorCodigo.AccessibleName = "Eliminado"
        Me.chkPorCodigo.AutoSize = True
        Me.chkPorCodigo.BackColor = System.Drawing.Color.Transparent
        Me.chkPorCodigo.Enabled = False
        Me.chkPorCodigo.ForeColor = System.Drawing.Color.Blue
        Me.chkPorCodigo.Location = New System.Drawing.Point(43, 225)
        Me.chkPorCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPorCodigo.Name = "chkPorCodigo"
        Me.chkPorCodigo.Size = New System.Drawing.Size(181, 21)
        Me.chkPorCodigo.TabIndex = 4
        Me.chkPorCodigo.Text = "Por Código de Producto"
        Me.chkPorCodigo.UseVisualStyleBackColor = False
        Me.chkPorCodigo.Visible = False
        '
        'rdPersonalizado
        '
        Me.rdPersonalizado.AutoSize = True
        Me.rdPersonalizado.BackColor = System.Drawing.Color.Transparent
        Me.rdPersonalizado.Enabled = False
        Me.rdPersonalizado.ForeColor = System.Drawing.Color.Blue
        Me.rdPersonalizado.Location = New System.Drawing.Point(52, 247)
        Me.rdPersonalizado.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPersonalizado.Name = "rdPersonalizado"
        Me.rdPersonalizado.Size = New System.Drawing.Size(154, 21)
        Me.rdPersonalizado.TabIndex = 6
        Me.rdPersonalizado.Text = "Filtro Personalizado"
        Me.rdPersonalizado.UseVisualStyleBackColor = False
        Me.rdPersonalizado.Visible = False
        '
        'rdRubros
        '
        Me.rdRubros.AutoSize = True
        Me.rdRubros.BackColor = System.Drawing.Color.Transparent
        Me.rdRubros.Enabled = False
        Me.rdRubros.ForeColor = System.Drawing.Color.Blue
        Me.rdRubros.Location = New System.Drawing.Point(275, 224)
        Me.rdRubros.Margin = New System.Windows.Forms.Padding(4)
        Me.rdRubros.Name = "rdRubros"
        Me.rdRubros.Size = New System.Drawing.Size(156, 21)
        Me.rdRubros.TabIndex = 5
        Me.rdRubros.Text = "Rubros y Subrubros"
        Me.rdRubros.UseVisualStyleBackColor = False
        Me.rdRubros.Visible = False
        '
        'Label5
        '
        Me.Label5.AccessibleName = "*Deposito"
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(19, 14)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 17)
        Me.Label5.TabIndex = 141
        Me.Label5.Text = "Depósito*"
        '
        'cmbNombre
        '
        Me.cmbNombre.AccessibleName = ""
        Me.cmbNombre.DropDownHeight = 450
        Me.cmbNombre.FormattingEnabled = True
        Me.cmbNombre.IntegralHeight = False
        Me.cmbNombre.Location = New System.Drawing.Point(275, 252)
        Me.cmbNombre.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbNombre.Name = "cmbNombre"
        Me.cmbNombre.Size = New System.Drawing.Size(541, 24)
        Me.cmbNombre.TabIndex = 9
        Me.cmbNombre.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(529, 16)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 17)
        Me.Label4.TabIndex = 139
        Me.Label4.Text = "Motivo*"
        '
        'cmbMotivo
        '
        Me.cmbMotivo.AccessibleName = "*Motivo"
        Me.cmbMotivo.FormattingEnabled = True
        Me.cmbMotivo.Location = New System.Drawing.Point(532, 34)
        Me.cmbMotivo.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbMotivo.Name = "cmbMotivo"
        Me.cmbMotivo.Size = New System.Drawing.Size(433, 24)
        Me.cmbMotivo.TabIndex = 3
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(979, 34)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(535, 22)
        Me.txtNota.TabIndex = 4
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
        Me.Label8.Location = New System.Drawing.Point(975, 16)
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
        Me.chkEliminado.Location = New System.Drawing.Point(1415, 220)
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
        Me.grdItems.Location = New System.Drawing.Point(21, 96)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1761, 475)
        Me.grdItems.TabIndex = 10
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1477, 2)
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
        Me.Label1.Location = New System.Drawing.Point(1449, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
        Me.Label1.TabIndex = 129
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = "CODIGO"
        Me.txtCODIGO.BackColor = System.Drawing.SystemColors.Window
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(240, 34)
        Me.txtCODIGO.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(129, 23)
        Me.txtCODIGO.TabIndex = 1
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
        Me.Label2.Location = New System.Drawing.Point(237, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 17)
        Me.Label2.TabIndex = 131
        Me.Label2.Text = "Ajuste"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(383, 34)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 22)
        Me.dtpFECHA.TabIndex = 2
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(380, 14)
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
        'frmAjustes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1827, 689)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmAjustes"
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
    Friend WithEvents rdPersonalizado As System.Windows.Forms.RadioButton
    Friend WithEvents rdRubros As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbNombre As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbMotivo As System.Windows.Forms.ComboBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkPorCodigo As System.Windows.Forms.CheckBox
    Public WithEvents cmbSubrubros As System.Windows.Forms.ComboBox
    Public WithEvents cmbRubros As System.Windows.Forms.ComboBox
    Friend WithEvents txtidAlmacen As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents chkFiltrar As System.Windows.Forms.CheckBox
    Friend WithEvents txtFiltro As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
