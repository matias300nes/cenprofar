
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPromociones

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
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtIdMaterial = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDLista = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.nudMaximo = New System.Windows.Forms.NumericUpDown()
        Me.nudMinimo = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPrecio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDescripción = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbListaPrecio = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.chkEliminados = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudMaximo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMinimo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(243, 4)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(17, 22)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(4, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Código"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(549, 7)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 17)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Producto"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2013
        Me.GroupBox1.Controls.Add(Me.txtIdMaterial)
        Me.GroupBox1.Controls.Add(Me.txtIDLista)
        Me.GroupBox1.Controls.Add(Me.lblCodigo)
        Me.GroupBox1.Controls.Add(Me.nudMaximo)
        Me.GroupBox1.Controls.Add(Me.nudMinimo)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtPrecio)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtDescripción)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbListaPrecio)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1230, 105)
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
        'txtIdMaterial
        '
        Me.txtIdMaterial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdMaterial.Decimals = CType(2, Byte)
        Me.txtIdMaterial.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdMaterial.Enabled = False
        Me.txtIdMaterial.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdMaterial.Location = New System.Drawing.Point(814, 4)
        Me.txtIdMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdMaterial.MaxLength = 8
        Me.txtIdMaterial.Name = "txtIdMaterial"
        Me.txtIdMaterial.Size = New System.Drawing.Size(93, 22)
        Me.txtIdMaterial.TabIndex = 269
        Me.txtIdMaterial.Text_1 = Nothing
        Me.txtIdMaterial.Text_2 = Nothing
        Me.txtIdMaterial.Text_3 = Nothing
        Me.txtIdMaterial.Text_4 = Nothing
        Me.txtIdMaterial.UserValues = Nothing
        Me.txtIdMaterial.Visible = False
        '
        'txtIDLista
        '
        Me.txtIDLista.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDLista.Decimals = CType(2, Byte)
        Me.txtIDLista.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDLista.Enabled = False
        Me.txtIDLista.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIDLista.Location = New System.Drawing.Point(464, 4)
        Me.txtIDLista.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDLista.MaxLength = 8
        Me.txtIDLista.Name = "txtIDLista"
        Me.txtIDLista.Size = New System.Drawing.Size(32, 22)
        Me.txtIDLista.TabIndex = 268
        Me.txtIDLista.Text_1 = Nothing
        Me.txtIDLista.Text_2 = Nothing
        Me.txtIDLista.Text_3 = Nothing
        Me.txtIDLista.Text_4 = Nothing
        Me.txtIDLista.UserValues = Nothing
        Me.txtIDLista.Visible = False
        '
        'lblCodigo
        '
        Me.lblCodigo.BackColor = System.Drawing.Color.White
        Me.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(7, 30)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(75, 23)
        Me.lblCodigo.TabIndex = 0
        '
        'nudMaximo
        '
        Me.nudMaximo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudMaximo.Location = New System.Drawing.Point(1140, 29)
        Me.nudMaximo.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudMaximo.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nudMaximo.Name = "nudMaximo"
        Me.nudMaximo.Size = New System.Drawing.Size(63, 24)
        Me.nudMaximo.TabIndex = 6
        Me.nudMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudMaximo.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nudMinimo
        '
        Me.nudMinimo.AccessibleName = ""
        Me.nudMinimo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudMinimo.Location = New System.Drawing.Point(1069, 29)
        Me.nudMinimo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMinimo.Name = "nudMinimo"
        Me.nudMinimo.Size = New System.Drawing.Size(63, 24)
        Me.nudMinimo.TabIndex = 5
        Me.nudMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudMinimo.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(1137, 7)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 17)
        Me.Label8.TabIndex = 267
        Me.Label8.Text = "Máximo"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(959, 7)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 17)
        Me.Label7.TabIndex = 265
        Me.Label7.Text = "Precio"
        '
        'txtPrecio
        '
        Me.txtPrecio.AccessibleName = "*CANTIDAD"
        Me.txtPrecio.Decimals = CType(2, Byte)
        Me.txtPrecio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecio.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPrecio.Location = New System.Drawing.Point(962, 29)
        Me.txtPrecio.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrecio.MaxLength = 25
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(99, 24)
        Me.txtPrecio.TabIndex = 4
        Me.txtPrecio.Text_1 = Nothing
        Me.txtPrecio.Text_2 = Nothing
        Me.txtPrecio.Text_3 = Nothing
        Me.txtPrecio.Text_4 = Nothing
        Me.txtPrecio.UserValues = Nothing
        '
        'Label6
        '
        Me.Label6.AccessibleName = ""
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(87, 7)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 17)
        Me.Label6.TabIndex = 263
        Me.Label6.Text = "Descripción"
        '
        'txtDescripción
        '
        Me.txtDescripción.AccessibleName = "*CANTIDAD"
        Me.txtDescripción.Decimals = CType(2, Byte)
        Me.txtDescripción.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescripción.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripción.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtDescripción.Location = New System.Drawing.Point(90, 29)
        Me.txtDescripción.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescripción.MaxLength = 25
        Me.txtDescripción.Name = "txtDescripción"
        Me.txtDescripción.Size = New System.Drawing.Size(236, 24)
        Me.txtDescripción.TabIndex = 1
        Me.txtDescripción.Text_1 = Nothing
        Me.txtDescripción.Text_2 = Nothing
        Me.txtDescripción.Text_3 = Nothing
        Me.txtDescripción.Text_4 = Nothing
        Me.txtDescripción.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AccessibleName = ""
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(331, 7)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 17)
        Me.Label5.TabIndex = 261
        Me.Label5.Text = "Lista de Precio"
        '
        'cmbListaPrecio
        '
        Me.cmbListaPrecio.AccessibleName = "*LISTA DE PRECIO"
        Me.cmbListaPrecio.DropDownHeight = 500
        Me.cmbListaPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbListaPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbListaPrecio.FormattingEnabled = True
        Me.cmbListaPrecio.IntegralHeight = False
        Me.cmbListaPrecio.Location = New System.Drawing.Point(334, 28)
        Me.cmbListaPrecio.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbListaPrecio.Name = "cmbListaPrecio"
        Me.cmbListaPrecio.Size = New System.Drawing.Size(210, 26)
        Me.cmbListaPrecio.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(1066, 7)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 17)
        Me.Label4.TabIndex = 259
        Me.Label4.Text = "Mínimo"
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = "*PRODUCTO"
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(552, 27)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(402, 28)
        Me.cmbProducto.TabIndex = 3
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.ForeColor = System.Drawing.Color.Red
        Me.chkEliminados.Location = New System.Drawing.Point(1079, 74)
        Me.chkEliminados.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(138, 21)
        Me.chkEliminados.TabIndex = 7
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(216, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'frmPromociones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1281, 592)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmPromociones"
        Me.Text = "Promociones"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudMaximo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMinimo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub




    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB

    Friend WithEvents Label2 As System.Windows.Forms.Label

    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkEliminados As System.Windows.Forms.CheckBox
    Private WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmbListaPrecio As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDescripción As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPrecio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nudMaximo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudMinimo As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents txtIdMaterial As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIDLista As TextBoxConFormatoVB.FormattedTextBoxVB

End Class
