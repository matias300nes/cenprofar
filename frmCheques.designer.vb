<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheques
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheques))
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.txtMontoCheques = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbBanco = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkUtilizado = New System.Windows.Forms.CheckBox()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNroCheque = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPropietario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUtilizado = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMonto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.GroupBox2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkChequesPropios = New System.Windows.Forms.CheckBox()
        Me.txtNroChequeFin = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNroChequeInicio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbBancoPropio = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Location = New System.Drawing.Point(231, 93)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX9.Size = New System.Drawing.Size(135, 15)
        Me.LabelX9.TabIndex = 141
        Me.LabelX9.Text = "Monto Cheques por Cobrar"
        '
        'chkAnulados
        '
        Me.chkAnulados.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.BackColor = System.Drawing.Color.Transparent
        Me.chkAnulados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnulados.ForeColor = System.Drawing.Color.Red
        Me.chkAnulados.Location = New System.Drawing.Point(1228, 95)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(109, 17)
        Me.chkAnulados.TabIndex = 145
        Me.chkAnulados.Text = "Ver Eliminados"
        Me.chkAnulados.UseVisualStyleBackColor = False
        '
        'txtMontoCheques
        '
        Me.txtMontoCheques.AccessibleName = ""
        Me.txtMontoCheques.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoCheques.Decimals = CType(2, Byte)
        Me.txtMontoCheques.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoCheques.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoCheques.Location = New System.Drawing.Point(372, 90)
        Me.txtMontoCheques.MaxLength = 20
        Me.txtMontoCheques.Name = "txtMontoCheques"
        Me.txtMontoCheques.ReadOnly = True
        Me.txtMontoCheques.Size = New System.Drawing.Size(76, 20)
        Me.txtMontoCheques.TabIndex = 144
        Me.txtMontoCheques.Text_1 = Nothing
        Me.txtMontoCheques.Text_2 = Nothing
        Me.txtMontoCheques.Text_3 = Nothing
        Me.txtMontoCheques.Text_4 = Nothing
        Me.txtMontoCheques.UserValues = Nothing
        '
        'cmbBanco
        '
        Me.cmbBanco.AccessibleName = "*Banco"
        Me.cmbBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBanco.FormattingEnabled = True
        Me.cmbBanco.Location = New System.Drawing.Point(657, 24)
        Me.cmbBanco.Name = "cmbBanco"
        Me.cmbBanco.Size = New System.Drawing.Size(286, 21)
        Me.cmbBanco.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(654, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 143
        Me.Label5.Text = "Banco*"
        '
        'chkUtilizado
        '
        Me.chkUtilizado.AutoSize = True
        Me.chkUtilizado.BackColor = System.Drawing.Color.Transparent
        Me.chkUtilizado.ForeColor = System.Drawing.Color.Blue
        Me.chkUtilizado.Location = New System.Drawing.Point(398, 67)
        Me.chkUtilizado.Name = "chkUtilizado"
        Me.chkUtilizado.Size = New System.Drawing.Size(66, 17)
        Me.chkUtilizado.TabIndex = 7
        Me.chkUtilizado.Text = "Utilizado"
        Me.chkUtilizado.UseVisualStyleBackColor = False
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(184, 25)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(90, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(181, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "Fecha Vencimiento"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(692, 6)
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
        Me.Label1.Location = New System.Drawing.Point(670, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtNroCheque
        '
        Me.txtNroCheque.AccessibleName = "*NroCheque"
        Me.txtNroCheque.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroCheque.Decimals = CType(2, Byte)
        Me.txtNroCheque.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroCheque.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroCheque.Location = New System.Drawing.Point(6, 25)
        Me.txtNroCheque.MaxLength = 25
        Me.txtNroCheque.Name = "txtNroCheque"
        Me.txtNroCheque.Size = New System.Drawing.Size(172, 20)
        Me.txtNroCheque.TabIndex = 0
        Me.txtNroCheque.Text_1 = Nothing
        Me.txtNroCheque.Text_2 = Nothing
        Me.txtNroCheque.Text_3 = Nothing
        Me.txtNroCheque.Text_4 = Nothing
        Me.txtNroCheque.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(3, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro de Cheque*"
        '
        'txtPropietario
        '
        Me.txtPropietario.AccessibleName = ""
        Me.txtPropietario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPropietario.Decimals = CType(2, Byte)
        Me.txtPropietario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPropietario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPropietario.Location = New System.Drawing.Point(372, 25)
        Me.txtPropietario.MaxLength = 50
        Me.txtPropietario.Name = "txtPropietario"
        Me.txtPropietario.Size = New System.Drawing.Size(279, 20)
        Me.txtPropietario.TabIndex = 4
        Me.txtPropietario.Text_1 = Nothing
        Me.txtPropietario.Text_2 = Nothing
        Me.txtPropietario.Text_3 = Nothing
        Me.txtPropietario.Text_4 = Nothing
        Me.txtPropietario.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(369, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Propietario"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservaciones.Decimals = CType(2, Byte)
        Me.txtObservaciones.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtObservaciones.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtObservaciones.Location = New System.Drawing.Point(3, 64)
        Me.txtObservaciones.MaxLength = 100
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(384, 20)
        Me.txtObservaciones.TabIndex = 6
        Me.txtObservaciones.Text_1 = Nothing
        Me.txtObservaciones.Text_2 = Nothing
        Me.txtObservaciones.Text_3 = Nothing
        Me.txtObservaciones.Text_4 = Nothing
        Me.txtObservaciones.UserValues = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(3, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Observaciones"
        '
        'txtUtilizado
        '
        Me.txtUtilizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUtilizado.Decimals = CType(2, Byte)
        Me.txtUtilizado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtUtilizado.Enabled = False
        Me.txtUtilizado.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtUtilizado.Location = New System.Drawing.Point(470, 64)
        Me.txtUtilizado.MaxLength = 100
        Me.txtUtilizado.Name = "txtUtilizado"
        Me.txtUtilizado.Size = New System.Drawing.Size(475, 20)
        Me.txtUtilizado.TabIndex = 8
        Me.txtUtilizado.Text_1 = Nothing
        Me.txtUtilizado.Text_2 = Nothing
        Me.txtUtilizado.Text_3 = Nothing
        Me.txtUtilizado.Text_4 = Nothing
        Me.txtUtilizado.UserValues = Nothing
        '
        'txtMonto
        '
        Me.txtMonto.AccessibleName = "*Monto"
        Me.txtMonto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMonto.Decimals = CType(2, Byte)
        Me.txtMonto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMonto.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMonto.Location = New System.Drawing.Point(284, 25)
        Me.txtMonto.MaxLength = 20
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(82, 20)
        Me.txtMonto.TabIndex = 2
        Me.txtMonto.Text_1 = Nothing
        Me.txtMonto.Text_2 = Nothing
        Me.txtMonto.Text_3 = Nothing
        Me.txtMonto.Text_4 = Nothing
        Me.txtMonto.UserValues = Nothing
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(281, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 13)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Monto*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.txtNroCheque)
        Me.GroupBox1.Controls.Add(Me.txtMontoCheques)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtMonto)
        Me.GroupBox1.Controls.Add(Me.cmbBanco)
        Me.GroupBox1.Controls.Add(Me.txtUtilizado)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkUtilizado)
        Me.GroupBox1.Controls.Add(Me.txtObservaciones)
        Me.GroupBox1.Controls.Add(Me.LabelX9)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.txtPropietario)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1346, 121)
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
        Me.GroupBox1.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox2.Controls.Add(Me.chkChequesPropios)
        Me.GroupBox2.Controls.Add(Me.txtNroChequeFin)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtNroChequeInicio)
        Me.GroupBox2.Controls.Add(Me.cmbBancoPropio)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox2.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1346, 60)
        '
        '
        '
        Me.GroupBox2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupBox2.Style.BackColorGradientAngle = 90
        Me.GroupBox2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupBox2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox2.Style.BorderBottomWidth = 1
        Me.GroupBox2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupBox2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox2.Style.BorderLeftWidth = 1
        Me.GroupBox2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox2.Style.BorderRightWidth = 1
        Me.GroupBox2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox2.Style.BorderTopWidth = 1
        Me.GroupBox2.Style.CornerDiameter = 4
        Me.GroupBox2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupBox2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupBox2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupBox2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupBox2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupBox2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupBox2.TabIndex = 0
        '
        'chkChequesPropios
        '
        Me.chkChequesPropios.AutoSize = True
        Me.chkChequesPropios.BackColor = System.Drawing.Color.Transparent
        Me.chkChequesPropios.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChequesPropios.ForeColor = System.Drawing.Color.Blue
        Me.chkChequesPropios.Location = New System.Drawing.Point(6, 24)
        Me.chkChequesPropios.Name = "chkChequesPropios"
        Me.chkChequesPropios.Size = New System.Drawing.Size(183, 19)
        Me.chkChequesPropios.TabIndex = 154
        Me.chkChequesPropios.Text = "Alta de Cheques Propios"
        Me.chkChequesPropios.UseVisualStyleBackColor = False
        '
        'txtNroChequeFin
        '
        Me.txtNroChequeFin.AccessibleName = ""
        Me.txtNroChequeFin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroChequeFin.Decimals = CType(2, Byte)
        Me.txtNroChequeFin.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroChequeFin.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroChequeFin.Location = New System.Drawing.Point(373, 25)
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
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(370, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 13)
        Me.Label7.TabIndex = 153
        Me.Label7.Text = "Nro de Cheque Fin*"
        '
        'txtNroChequeInicio
        '
        Me.txtNroChequeInicio.AccessibleName = ""
        Me.txtNroChequeInicio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroChequeInicio.Decimals = CType(2, Byte)
        Me.txtNroChequeInicio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroChequeInicio.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroChequeInicio.Location = New System.Drawing.Point(195, 25)
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
        'cmbBancoPropio
        '
        Me.cmbBancoPropio.AccessibleName = ""
        Me.cmbBancoPropio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBancoPropio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBancoPropio.FormattingEnabled = True
        Me.cmbBancoPropio.Location = New System.Drawing.Point(551, 24)
        Me.cmbBancoPropio.Name = "cmbBancoPropio"
        Me.cmbBancoPropio.Size = New System.Drawing.Size(286, 21)
        Me.cmbBancoPropio.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(548, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 152
        Me.Label8.Text = "Banco*"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(192, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(111, 13)
        Me.Label9.TabIndex = 151
        Me.Label9.Text = "Nro de Cheque Inicio*"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(467, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 13)
        Me.Label10.TabIndex = 146
        Me.Label10.Text = "Observaciones Utilizado"
        '
        'frmCheques
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 749)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCheques"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheques en Cartera"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNroCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPropietario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUtilizado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMonto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkUtilizado As System.Windows.Forms.CheckBox
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbBanco As System.Windows.Forms.ComboBox
    Friend WithEvents txtMontoCheques As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupBox2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtNroChequeFin As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNroChequeInicio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbBancoPropio As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkChequesPropios As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
