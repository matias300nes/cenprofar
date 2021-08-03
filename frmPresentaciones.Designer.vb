<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPresentaciones

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
        Me.components = New System.ComponentModel.Container()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTotalAPagar = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdFactura = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIvaTotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbUnidadVenta = New System.Windows.Forms.ToolStripComboBox()
        Me.btnProcesar = New DevComponents.DotNetBar.ButtonX()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 206)
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
        Me.BuscarDescripcionToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 150)
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnProcesar)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtTotalAPagar)
        Me.GroupBox1.Controls.Add(Me.txtIdFactura)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtIvaTotal)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1765, 689)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1297, 662)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 17)
        Me.Label6.TabIndex = 190
        Me.Label6.Text = "Total "
        '
        'txtTotalAPagar
        '
        Me.txtTotalAPagar.AccessibleName = ""
        Me.txtTotalAPagar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotalAPagar.Decimals = CType(2, Byte)
        Me.txtTotalAPagar.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalAPagar.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotalAPagar.Location = New System.Drawing.Point(1349, 659)
        Me.txtTotalAPagar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalAPagar.MaxLength = 50
        Me.txtTotalAPagar.Name = "txtTotalAPagar"
        Me.txtTotalAPagar.ReadOnly = True
        Me.txtTotalAPagar.Size = New System.Drawing.Size(92, 22)
        Me.txtTotalAPagar.TabIndex = 189
        Me.txtTotalAPagar.Text_1 = Nothing
        Me.txtTotalAPagar.Text_2 = Nothing
        Me.txtTotalAPagar.Text_3 = Nothing
        Me.txtTotalAPagar.Text_4 = Nothing
        Me.txtTotalAPagar.UserValues = Nothing
        '
        'txtIdFactura
        '
        Me.txtIdFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdFactura.Decimals = CType(2, Byte)
        Me.txtIdFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdFactura.Enabled = False
        Me.txtIdFactura.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdFactura.Location = New System.Drawing.Point(1490, 196)
        Me.txtIdFactura.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdFactura.MaxLength = 8
        Me.txtIdFactura.Name = "txtIdFactura"
        Me.txtIdFactura.Size = New System.Drawing.Size(101, 22)
        Me.txtIdFactura.TabIndex = 188
        Me.txtIdFactura.Text_1 = Nothing
        Me.txtIdFactura.Text_2 = Nothing
        Me.txtIdFactura.Text_3 = Nothing
        Me.txtIdFactura.Text_4 = Nothing
        Me.txtIdFactura.UserValues = Nothing
        Me.txtIdFactura.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1610, 196)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(13, 657)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(129, 17)
        Me.Label19.TabIndex = 176
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(141, 657)
        Me.lblCantidadFilas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(60, 17)
        Me.lblCantidadFilas.TabIndex = 175
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = ""
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotal.Location = New System.Drawing.Point(1595, 364)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotal.MaxLength = 50
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(92, 22)
        Me.txtTotal.TabIndex = 25
        Me.txtTotal.Text_1 = Nothing
        Me.txtTotal.Text_2 = Nothing
        Me.txtTotal.Text_3 = Nothing
        Me.txtTotal.Text_4 = Nothing
        Me.txtTotal.UserValues = Nothing
        '
        'txtIvaTotal
        '
        Me.txtIvaTotal.AccessibleName = ""
        Me.txtIvaTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIvaTotal.Decimals = CType(2, Byte)
        Me.txtIvaTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIvaTotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIvaTotal.Location = New System.Drawing.Point(1595, 334)
        Me.txtIvaTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIvaTotal.MaxLength = 50
        Me.txtIvaTotal.Name = "txtIvaTotal"
        Me.txtIvaTotal.ReadOnly = True
        Me.txtIvaTotal.Size = New System.Drawing.Size(92, 22)
        Me.txtIvaTotal.TabIndex = 24
        Me.txtIvaTotal.Text_1 = Nothing
        Me.txtIvaTotal.Text_2 = Nothing
        Me.txtIvaTotal.Text_3 = Nothing
        Me.txtIvaTotal.Text_4 = Nothing
        Me.txtIvaTotal.UserValues = Nothing
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = ""
        Me.txtSubtotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtSubtotal.Location = New System.Drawing.Point(1595, 300)
        Me.txtSubtotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSubtotal.MaxLength = 50
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(92, 22)
        Me.txtSubtotal.TabIndex = 23
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1527, 305)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 17)
        Me.Label9.TabIndex = 134
        Me.Label9.Text = "Subtotal"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(1527, 363)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 17)
        Me.Label13.TabIndex = 128
        Me.Label13.Text = "Total "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1527, 337)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(29, 17)
        Me.Label11.TabIndex = 126
        Me.Label11.Text = "IVA"
        '
        'grdItems
        '
        Me.grdItems.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(17, 140)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1451, 510)
        Me.grdItems.TabIndex = 25
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(1647, 167)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(91, 21)
        Me.chkEliminado.TabIndex = 20
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1637, 196)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(101, 22)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbUnidadVenta})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(361, 158)
        '
        'cmbUnidadVenta
        '
        Me.cmbUnidadVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbUnidadVenta.DropDownWidth = 500
        Me.cmbUnidadVenta.Name = "cmbUnidadVenta"
        Me.cmbUnidadVenta.Size = New System.Drawing.Size(300, 150)
        Me.cmbUnidadVenta.Text = "Unidad de Venta"
        '
        'btnProcesar
        '
        Me.btnProcesar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnProcesar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnProcesar.Location = New System.Drawing.Point(1094, 651)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(75, 23)
        Me.btnProcesar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnProcesar.TabIndex = 192
        Me.btnProcesar.Text = "Procesar"
        '
        'frmPresentaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1797, 783)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmPresentaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub










    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIvaTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbUnidadVenta As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents txtIdFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAPagar As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnProcesar As DevComponents.DotNetBar.ButtonX

End Class
