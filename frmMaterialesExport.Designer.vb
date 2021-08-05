<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterialesExport
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaterialesExport))
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkRubro = New System.Windows.Forms.CheckBox()
        Me.cmbRubro = New System.Windows.Forms.ComboBox()
        Me.btnExportar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.chkMinorista = New System.Windows.Forms.CheckBox()
        Me.chkMayorista = New System.Windows.Forms.CheckBox()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkMarcas = New System.Windows.Forms.CheckBox()
        Me.cmbMarcas = New System.Windows.Forms.ComboBox()
        Me.chkFR = New System.Windows.Forms.CheckBox()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkStock = New System.Windows.Forms.CheckBox()
        Me.cmbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.GroupPanel1.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.GroupPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupPanel1
        '
        Me.GroupPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.chkRubro)
        Me.GroupPanel1.Controls.Add(Me.cmbRubro)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(11, 13)
        Me.GroupPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(508, 66)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 3
        Me.GroupPanel1.Text = "Rubros"
        '
        'chkRubro
        '
        Me.chkRubro.AutoCheck = False
        Me.chkRubro.AutoSize = True
        Me.chkRubro.Location = New System.Drawing.Point(23, 7)
        Me.chkRubro.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRubro.Name = "chkRubro"
        Me.chkRubro.Size = New System.Drawing.Size(18, 17)
        Me.chkRubro.TabIndex = 9
        Me.chkRubro.UseVisualStyleBackColor = True
        '
        'cmbRubro
        '
        Me.cmbRubro.AccessibleName = "* LISTA 4"
        Me.cmbRubro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbRubro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRubro.DropDownHeight = 300
        Me.cmbRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRubro.FormattingEnabled = True
        Me.cmbRubro.IntegralHeight = False
        Me.cmbRubro.Location = New System.Drawing.Point(51, 4)
        Me.cmbRubro.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbRubro.Name = "cmbRubro"
        Me.cmbRubro.Size = New System.Drawing.Size(438, 24)
        Me.cmbRubro.TabIndex = 7
        '
        'btnExportar
        '
        Me.btnExportar.Location = New System.Drawing.Point(443, 238)
        Me.btnExportar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(75, 23)
        Me.btnExportar.TabIndex = 5
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(341, 238)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(85, 23)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'chkMinorista
        '
        Me.chkMinorista.AutoSize = True
        Me.chkMinorista.Location = New System.Drawing.Point(235, 238)
        Me.chkMinorista.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMinorista.Name = "chkMinorista"
        Me.chkMinorista.Size = New System.Drawing.Size(87, 21)
        Me.chkMinorista.TabIndex = 7
        Me.chkMinorista.Text = "Minorista"
        Me.chkMinorista.UseVisualStyleBackColor = True
        Me.chkMinorista.Visible = False
        '
        'chkMayorista
        '
        Me.chkMayorista.AutoSize = True
        Me.chkMayorista.Location = New System.Drawing.Point(138, 238)
        Me.chkMayorista.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMayorista.Name = "chkMayorista"
        Me.chkMayorista.Size = New System.Drawing.Size(91, 21)
        Me.chkMayorista.TabIndex = 8
        Me.chkMayorista.Text = "Mayorista"
        Me.chkMayorista.UseVisualStyleBackColor = True
        Me.chkMayorista.Visible = False
        '
        'GroupPanel3
        '
        Me.GroupPanel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel3.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.chkMarcas)
        Me.GroupPanel3.Controls.Add(Me.cmbMarcas)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Location = New System.Drawing.Point(11, 92)
        Me.GroupPanel3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(508, 66)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 9
        Me.GroupPanel3.Text = "Marcas"
        '
        'chkMarcas
        '
        Me.chkMarcas.AutoCheck = False
        Me.chkMarcas.AutoSize = True
        Me.chkMarcas.Location = New System.Drawing.Point(23, 7)
        Me.chkMarcas.Margin = New System.Windows.Forms.Padding(4)
        Me.chkMarcas.Name = "chkMarcas"
        Me.chkMarcas.Size = New System.Drawing.Size(18, 17)
        Me.chkMarcas.TabIndex = 10
        Me.chkMarcas.UseVisualStyleBackColor = True
        '
        'cmbMarcas
        '
        Me.cmbMarcas.AccessibleName = "* LISTA 4"
        Me.cmbMarcas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbMarcas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMarcas.DropDownHeight = 300
        Me.cmbMarcas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMarcas.FormattingEnabled = True
        Me.cmbMarcas.IntegralHeight = False
        Me.cmbMarcas.Location = New System.Drawing.Point(51, 4)
        Me.cmbMarcas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbMarcas.Name = "cmbMarcas"
        Me.cmbMarcas.Size = New System.Drawing.Size(439, 24)
        Me.cmbMarcas.TabIndex = 7
        '
        'chkFR
        '
        Me.chkFR.AutoSize = True
        Me.chkFR.ForeColor = System.Drawing.Color.Blue
        Me.chkFR.Location = New System.Drawing.Point(10, 240)
        Me.chkFR.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkFR.Name = "chkFR"
        Me.chkFR.Size = New System.Drawing.Size(99, 21)
        Me.chkFR.TabIndex = 10
        Me.chkFR.Text = "Incluir **FR"
        Me.chkFR.UseVisualStyleBackColor = True
        Me.chkFR.Visible = False
        '
        'GroupPanel4
        '
        Me.GroupPanel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel4.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.VS2005
        Me.GroupPanel4.Controls.Add(Me.chkStock)
        Me.GroupPanel4.Controls.Add(Me.cmbAlmacenes)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Location = New System.Drawing.Point(9, 166)
        Me.GroupPanel4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(508, 66)
        '
        '
        '
        Me.GroupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.GroupPanel4.Style.BackColorGradientAngle = 90
        Me.GroupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground
        Me.GroupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderBottomWidth = 1
        Me.GroupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderLeftWidth = 1
        Me.GroupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderRightWidth = 1
        Me.GroupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderTopWidth = 1
        Me.GroupPanel4.Style.CornerDiameter = 4
        Me.GroupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel4.TabIndex = 12
        Me.GroupPanel4.Text = "Stock"
        '
        'chkStock
        '
        Me.chkStock.AutoCheck = False
        Me.chkStock.AutoSize = True
        Me.chkStock.Location = New System.Drawing.Point(21, 8)
        Me.chkStock.Margin = New System.Windows.Forms.Padding(4)
        Me.chkStock.Name = "chkStock"
        Me.chkStock.Size = New System.Drawing.Size(18, 17)
        Me.chkStock.TabIndex = 12
        Me.chkStock.UseVisualStyleBackColor = True
        '
        'cmbAlmacenes
        '
        Me.cmbAlmacenes.AccessibleName = "*Almacenes"
        Me.cmbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAlmacenes.DropDownHeight = 300
        Me.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmacenes.FormattingEnabled = True
        Me.cmbAlmacenes.IntegralHeight = False
        Me.cmbAlmacenes.Location = New System.Drawing.Point(50, 4)
        Me.cmbAlmacenes.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAlmacenes.Name = "cmbAlmacenes"
        Me.cmbAlmacenes.Size = New System.Drawing.Size(439, 24)
        Me.cmbAlmacenes.TabIndex = 7
        '
        'frmMaterialesExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 265)
        Me.Controls.Add(Me.GroupPanel4)
        Me.Controls.Add(Me.GroupPanel3)
        Me.Controls.Add(Me.chkMayorista)
        Me.Controls.Add(Me.chkMinorista)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Controls.Add(Me.chkFR)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximumSize = New System.Drawing.Size(551, 312)
        Me.MinimumSize = New System.Drawing.Size(551, 312)
        Me.Name = "frmMaterialesExport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filtros de Exportación"
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel3.PerformLayout()
        Me.GroupPanel4.ResumeLayout(False)
        Me.GroupPanel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbRubro As System.Windows.Forms.ComboBox
    Friend WithEvents btnExportar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents chkMinorista As System.Windows.Forms.CheckBox
    Friend WithEvents chkMayorista As System.Windows.Forms.CheckBox
    Private WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbMarcas As System.Windows.Forms.ComboBox
    Friend WithEvents chkRubro As System.Windows.Forms.CheckBox
    Friend WithEvents chkMarcas As System.Windows.Forms.CheckBox
    Friend WithEvents chkFR As System.Windows.Forms.CheckBox
    Private WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents chkStock As System.Windows.Forms.CheckBox
End Class
