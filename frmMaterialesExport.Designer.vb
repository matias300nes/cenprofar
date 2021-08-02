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
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkLista = New System.Windows.Forms.CheckBox()
        Me.cmbLista = New System.Windows.Forms.ComboBox()
        Me.btnExportar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.chkMinorista = New System.Windows.Forms.CheckBox()
        Me.chkMayorista = New System.Windows.Forms.CheckBox()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkMarcas = New System.Windows.Forms.CheckBox()
        Me.cmbMarcas = New System.Windows.Forms.ComboBox()
        Me.chkFR = New System.Windows.Forms.CheckBox()
        Me.GroupPanel1.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
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
        Me.GroupPanel1.Location = New System.Drawing.Point(13, 14)
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
        'GroupPanel2
        '
        Me.GroupPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.chkLista)
        Me.GroupPanel2.Controls.Add(Me.cmbLista)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(13, 161)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(508, 66)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 4
        Me.GroupPanel2.Text = "Listas"
        '
        'chkLista
        '
        Me.chkLista.AutoCheck = False
        Me.chkLista.AutoSize = True
        Me.chkLista.Location = New System.Drawing.Point(21, 7)
        Me.chkLista.Margin = New System.Windows.Forms.Padding(4)
        Me.chkLista.Name = "chkLista"
        Me.chkLista.Size = New System.Drawing.Size(18, 17)
        Me.chkLista.TabIndex = 11
        Me.chkLista.UseVisualStyleBackColor = True
        '
        'cmbLista
        '
        Me.cmbLista.AccessibleName = "* LISTA 4"
        Me.cmbLista.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLista.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLista.DropDownHeight = 300
        Me.cmbLista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLista.FormattingEnabled = True
        Me.cmbLista.IntegralHeight = False
        Me.cmbLista.Location = New System.Drawing.Point(51, 4)
        Me.cmbLista.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbLista.Name = "cmbLista"
        Me.cmbLista.Size = New System.Drawing.Size(438, 24)
        Me.cmbLista.TabIndex = 7
        '
        'btnExportar
        '
        Me.btnExportar.Location = New System.Drawing.Point(445, 236)
        Me.btnExportar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(75, 23)
        Me.btnExportar.TabIndex = 5
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(354, 236)
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
        Me.chkMinorista.Location = New System.Drawing.Point(132, 236)
        Me.chkMinorista.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMinorista.Name = "chkMinorista"
        Me.chkMinorista.Size = New System.Drawing.Size(87, 21)
        Me.chkMinorista.TabIndex = 7
        Me.chkMinorista.Text = "Minorista"
        Me.chkMinorista.UseVisualStyleBackColor = True
        '
        'chkMayorista
        '
        Me.chkMayorista.AutoSize = True
        Me.chkMayorista.Location = New System.Drawing.Point(13, 236)
        Me.chkMayorista.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkMayorista.Name = "chkMayorista"
        Me.chkMayorista.Size = New System.Drawing.Size(91, 21)
        Me.chkMayorista.TabIndex = 8
        Me.chkMayorista.Text = "Mayorista"
        Me.chkMayorista.UseVisualStyleBackColor = True
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
        Me.GroupPanel3.Location = New System.Drawing.Point(12, 87)
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
        Me.chkFR.Location = New System.Drawing.Point(244, 236)
        Me.chkFR.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkFR.Name = "chkFR"
        Me.chkFR.Size = New System.Drawing.Size(99, 21)
        Me.chkFR.TabIndex = 10
        Me.chkFR.Text = "Incluir **FR"
        Me.chkFR.UseVisualStyleBackColor = True
        Me.chkFR.Visible = False
        '
        'frmMaterialesExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 265)
        Me.Controls.Add(Me.chkFR)
        Me.Controls.Add(Me.GroupPanel3)
        Me.Controls.Add(Me.chkMayorista)
        Me.Controls.Add(Me.chkMinorista)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.GroupPanel2)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmMaterialesExport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filtros de Exportación"
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbRubro As System.Windows.Forms.ComboBox
    Private WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbLista As System.Windows.Forms.ComboBox
    Friend WithEvents btnExportar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents chkMinorista As System.Windows.Forms.CheckBox
    Friend WithEvents chkMayorista As System.Windows.Forms.CheckBox
    Private WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbMarcas As System.Windows.Forms.ComboBox
    Friend WithEvents chkRubro As System.Windows.Forms.CheckBox
    Friend WithEvents chkLista As System.Windows.Forms.CheckBox
    Friend WithEvents chkMarcas As System.Windows.Forms.CheckBox
    Friend WithEvents chkFR As System.Windows.Forms.CheckBox
End Class
