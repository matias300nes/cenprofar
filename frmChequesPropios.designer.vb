<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChequesPropios
    Inherits frmBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChequesPropios))
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.cmbBancoPropio = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNroChequeInicio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtNroChequeFin = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkAnulados
        '
        Me.chkAnulados.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.BackColor = System.Drawing.Color.Transparent
        Me.chkAnulados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnulados.ForeColor = System.Drawing.Color.Red
        Me.chkAnulados.Location = New System.Drawing.Point(904, 25)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(109, 17)
        Me.chkAnulados.TabIndex = 3
        Me.chkAnulados.Text = "Ver Eliminados"
        Me.chkAnulados.UseVisualStyleBackColor = False
        '
        'cmbBancoPropio
        '
        Me.cmbBancoPropio.AccessibleName = "*Banco"
        Me.cmbBancoPropio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBancoPropio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBancoPropio.FormattingEnabled = True
        Me.cmbBancoPropio.Location = New System.Drawing.Point(369, 22)
        Me.cmbBancoPropio.Name = "cmbBancoPropio"
        Me.cmbBancoPropio.Size = New System.Drawing.Size(286, 21)
        Me.cmbBancoPropio.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(366, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 143
        Me.Label5.Text = "Banco*"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(699, 4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(16, 20)
        Me.txtID.TabIndex = 50
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
        Me.Label1.Location = New System.Drawing.Point(677, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtNroChequeInicio
        '
        Me.txtNroChequeInicio.AccessibleName = "*NroChequeInicio"
        Me.txtNroChequeInicio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroChequeInicio.Decimals = CType(2, Byte)
        Me.txtNroChequeInicio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroChequeInicio.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroChequeInicio.Location = New System.Drawing.Point(13, 23)
        Me.txtNroChequeInicio.MaxLength = 25
        Me.txtNroChequeInicio.Name = "txtNroChequeInicio"
        Me.txtNroChequeInicio.Size = New System.Drawing.Size(172, 20)
        Me.txtNroChequeInicio.TabIndex = 0
        Me.txtNroChequeInicio.Text_1 = Nothing
        Me.txtNroChequeInicio.Text_2 = Nothing
        Me.txtNroChequeInicio.Text_3 = Nothing
        Me.txtNroChequeInicio.Text_4 = Nothing
        Me.txtNroChequeInicio.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(10, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro de Cheque Inicio*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.txtNroChequeFin)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.txtNroChequeInicio)
        Me.GroupBox1.Controls.Add(Me.cmbBancoPropio)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1022, 65)
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
        Me.GroupBox1.TabIndex = 114
        '
        'txtNroChequeFin
        '
        Me.txtNroChequeFin.AccessibleName = "*NroChequeFin"
        Me.txtNroChequeFin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroChequeFin.Decimals = CType(2, Byte)
        Me.txtNroChequeFin.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroChequeFin.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroChequeFin.Location = New System.Drawing.Point(191, 23)
        Me.txtNroChequeFin.MaxLength = 25
        Me.txtNroChequeFin.Name = "txtNroChequeFin"
        Me.txtNroChequeFin.Size = New System.Drawing.Size(172, 20)
        Me.txtNroChequeFin.TabIndex = 1
        Me.txtNroChequeFin.Text_1 = Nothing
        Me.txtNroChequeFin.Text_2 = Nothing
        Me.txtNroChequeFin.Text_3 = Nothing
        Me.txtNroChequeFin.Text_4 = Nothing
        Me.txtNroChequeFin.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(188, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 147
        Me.Label3.Text = "Nro de Cheque Fin*"
        '
        'frmChequesPropios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1046, 543)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmChequesPropios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chequeras"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNroChequeInicio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbBancoPropio As System.Windows.Forms.ComboBox
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtNroChequeFin As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
