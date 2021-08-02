<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecepciones_Complejo
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
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtOtrosImp = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblOtrosImp = New System.Windows.Forms.Label
        Me.txtIIBB = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblIIBB = New System.Windows.Forms.Label
        Me.txtidpago = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtIdGasto = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtIdProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtTotalFlete = New System.Windows.Forms.Label
        Me.txtTotal = New System.Windows.Forms.Label
        Me.txtMontoIvaFlete = New System.Windows.Forms.Label
        Me.txtMontoIva = New System.Windows.Forms.Label
        Me.lblTipoFactFlete = New System.Windows.Forms.Label
        Me.cmbTipoFactFlete = New System.Windows.Forms.ComboBox
        Me.lblTipoFact = New System.Windows.Forms.Label
        Me.cmbTipoFact = New System.Windows.Forms.ComboBox
        Me.txtPorcIvaFlete = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblPorcIvaFlete = New System.Windows.Forms.Label
        Me.txtPorcIva = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblPorcIva = New System.Windows.Forms.Label
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblMontoIvaFlete = New System.Windows.Forms.Label
        Me.txtSubtotalFlete = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblSubtotalFlete = New System.Windows.Forms.Label
        Me.lblProveedorFlete = New System.Windows.Forms.Label
        Me.cmbProveedorFlete = New System.Windows.Forms.ComboBox
        Me.chkFleteSaldado = New System.Windows.Forms.CheckBox
        Me.lblTotalFlete = New System.Windows.Forms.Label
        Me.chkFlete = New System.Windows.Forms.CheckBox
        Me.txtFacturaFlete = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblFacturaFlete = New System.Windows.Forms.Label
        Me.chkFacturaCancelada = New System.Windows.Forms.CheckBox
        Me.LblTotal = New System.Windows.Forms.Label
        Me.lblMontoIVA = New System.Windows.Forms.Label
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblSubtotal = New System.Windows.Forms.Label
        Me.chkCargarGasto = New System.Windows.Forms.CheckBox
        Me.txtNroFactura = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblFactura = New System.Windows.Forms.Label
        Me.txtOC = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbProveedor = New System.Windows.Forms.ComboBox
        Me.btnRecibirTodos = New System.Windows.Forms.Button
        Me.txtRemito = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnLlenarGrilla = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbOrdenDeCompra = New System.Windows.Forms.ComboBox
        Me.chkEliminado = New System.Windows.Forms.CheckBox
        Me.grdItems = New System.Windows.Forms.DataGridView
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkPrecioxMt = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkMostarColumnas = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtOtrosImp)
        Me.GroupBox1.Controls.Add(Me.lblOtrosImp)
        Me.GroupBox1.Controls.Add(Me.txtIIBB)
        Me.GroupBox1.Controls.Add(Me.lblIIBB)
        Me.GroupBox1.Controls.Add(Me.txtidpago)
        Me.GroupBox1.Controls.Add(Me.txtIdGasto)
        Me.GroupBox1.Controls.Add(Me.txtIdProveedor)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtTotalFlete)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtMontoIvaFlete)
        Me.GroupBox1.Controls.Add(Me.txtMontoIva)
        Me.GroupBox1.Controls.Add(Me.lblTipoFactFlete)
        Me.GroupBox1.Controls.Add(Me.cmbTipoFactFlete)
        Me.GroupBox1.Controls.Add(Me.lblTipoFact)
        Me.GroupBox1.Controls.Add(Me.cmbTipoFact)
        Me.GroupBox1.Controls.Add(Me.txtPorcIvaFlete)
        Me.GroupBox1.Controls.Add(Me.lblPorcIvaFlete)
        Me.GroupBox1.Controls.Add(Me.txtPorcIva)
        Me.GroupBox1.Controls.Add(Me.lblPorcIva)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblMontoIvaFlete)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalFlete)
        Me.GroupBox1.Controls.Add(Me.lblSubtotalFlete)
        Me.GroupBox1.Controls.Add(Me.lblProveedorFlete)
        Me.GroupBox1.Controls.Add(Me.cmbProveedorFlete)
        Me.GroupBox1.Controls.Add(Me.chkFleteSaldado)
        Me.GroupBox1.Controls.Add(Me.lblTotalFlete)
        Me.GroupBox1.Controls.Add(Me.chkFlete)
        Me.GroupBox1.Controls.Add(Me.txtFacturaFlete)
        Me.GroupBox1.Controls.Add(Me.lblFacturaFlete)
        Me.GroupBox1.Controls.Add(Me.chkFacturaCancelada)
        Me.GroupBox1.Controls.Add(Me.LblTotal)
        Me.GroupBox1.Controls.Add(Me.lblMontoIVA)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.lblSubtotal)
        Me.GroupBox1.Controls.Add(Me.chkCargarGasto)
        Me.GroupBox1.Controls.Add(Me.txtNroFactura)
        Me.GroupBox1.Controls.Add(Me.lblFactura)
        Me.GroupBox1.Controls.Add(Me.txtOC)
        Me.GroupBox1.Controls.Add(Me.txtProveedor)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbProveedor)
        Me.GroupBox1.Controls.Add(Me.btnRecibirTodos)
        Me.GroupBox1.Controls.Add(Me.txtRemito)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbOrdenDeCompra)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkPrecioxMt)
        Me.GroupBox1.Controls.Add(Me.chkMostarColumnas)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(0, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1309, 412)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Enabled = False
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(786, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(316, 36)
        Me.Label1.TabIndex = 229
        Me.Label1.Text = "La modificación del Movimiento del Gasto, solo se puede hacer en la pantalla Admi" & _
            "nistración de Gastos"
        '
        'txtOtrosImp
        '
        Me.txtOtrosImp.AccessibleName = ""
        Me.txtOtrosImp.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtrosImp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOtrosImp.Decimals = CType(2, Byte)
        Me.txtOtrosImp.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOtrosImp.Enabled = False
        Me.txtOtrosImp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtrosImp.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtOtrosImp.Location = New System.Drawing.Point(537, 72)
        Me.txtOtrosImp.MaxLength = 25
        Me.txtOtrosImp.Name = "txtOtrosImp"
        Me.txtOtrosImp.Size = New System.Drawing.Size(68, 20)
        Me.txtOtrosImp.TabIndex = 14
        Me.txtOtrosImp.Text_1 = Nothing
        Me.txtOtrosImp.Text_2 = Nothing
        Me.txtOtrosImp.Text_3 = Nothing
        Me.txtOtrosImp.Text_4 = Nothing
        Me.txtOtrosImp.UserValues = Nothing
        '
        'lblOtrosImp
        '
        Me.lblOtrosImp.AutoSize = True
        Me.lblOtrosImp.Enabled = False
        Me.lblOtrosImp.Location = New System.Drawing.Point(534, 56)
        Me.lblOtrosImp.Name = "lblOtrosImp"
        Me.lblOtrosImp.Size = New System.Drawing.Size(55, 13)
        Me.lblOtrosImp.TabIndex = 228
        Me.lblOtrosImp.Text = "Otros Imp."
        '
        'txtIIBB
        '
        Me.txtIIBB.AccessibleName = ""
        Me.txtIIBB.BackColor = System.Drawing.SystemColors.Window
        Me.txtIIBB.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIIBB.Decimals = CType(2, Byte)
        Me.txtIIBB.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIIBB.Enabled = False
        Me.txtIIBB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIIBB.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtIIBB.Location = New System.Drawing.Point(459, 72)
        Me.txtIIBB.MaxLength = 25
        Me.txtIIBB.Name = "txtIIBB"
        Me.txtIIBB.Size = New System.Drawing.Size(68, 20)
        Me.txtIIBB.TabIndex = 13
        Me.txtIIBB.Text_1 = Nothing
        Me.txtIIBB.Text_2 = Nothing
        Me.txtIIBB.Text_3 = Nothing
        Me.txtIIBB.Text_4 = Nothing
        Me.txtIIBB.UserValues = Nothing
        '
        'lblIIBB
        '
        Me.lblIIBB.AutoSize = True
        Me.lblIIBB.Enabled = False
        Me.lblIIBB.Location = New System.Drawing.Point(458, 56)
        Me.lblIIBB.Name = "lblIIBB"
        Me.lblIIBB.Size = New System.Drawing.Size(52, 13)
        Me.lblIIBB.TabIndex = 226
        Me.lblIIBB.Text = "Perc IIBB"
        '
        'txtidpago
        '
        Me.txtidpago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidpago.Decimals = CType(2, Byte)
        Me.txtidpago.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidpago.Enabled = False
        Me.txtidpago.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidpago.Location = New System.Drawing.Point(1083, 234)
        Me.txtidpago.MaxLength = 8
        Me.txtidpago.Name = "txtidpago"
        Me.txtidpago.Size = New System.Drawing.Size(23, 20)
        Me.txtidpago.TabIndex = 192
        Me.txtidpago.Text_1 = Nothing
        Me.txtidpago.Text_2 = Nothing
        Me.txtidpago.Text_3 = Nothing
        Me.txtidpago.Text_4 = Nothing
        Me.txtidpago.UserValues = Nothing
        Me.txtidpago.Visible = False
        '
        'txtIdGasto
        '
        Me.txtIdGasto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdGasto.Decimals = CType(2, Byte)
        Me.txtIdGasto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdGasto.Enabled = False
        Me.txtIdGasto.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdGasto.Location = New System.Drawing.Point(1054, 234)
        Me.txtIdGasto.MaxLength = 8
        Me.txtIdGasto.Name = "txtIdGasto"
        Me.txtIdGasto.Size = New System.Drawing.Size(23, 20)
        Me.txtIdGasto.TabIndex = 191
        Me.txtIdGasto.Text_1 = Nothing
        Me.txtIdGasto.Text_2 = Nothing
        Me.txtIdGasto.Text_3 = Nothing
        Me.txtIdGasto.Text_4 = Nothing
        Me.txtIdGasto.UserValues = Nothing
        Me.txtIdGasto.Visible = False
        '
        'txtIdProveedor
        '
        Me.txtIdProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdProveedor.Decimals = CType(2, Byte)
        Me.txtIdProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProveedor.Enabled = False
        Me.txtIdProveedor.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdProveedor.Location = New System.Drawing.Point(961, 241)
        Me.txtIdProveedor.MaxLength = 8
        Me.txtIdProveedor.Name = "txtIdProveedor"
        Me.txtIdProveedor.Size = New System.Drawing.Size(23, 20)
        Me.txtIdProveedor.TabIndex = 130
        Me.txtIdProveedor.Text_1 = Nothing
        Me.txtIdProveedor.Text_2 = Nothing
        Me.txtIdProveedor.Text_3 = Nothing
        Me.txtIdProveedor.Text_4 = Nothing
        Me.txtIdProveedor.UserValues = Nothing
        Me.txtIdProveedor.Visible = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1020, 234)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(23, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'txtTotalFlete
        '
        Me.txtTotalFlete.BackColor = System.Drawing.Color.White
        Me.txtTotalFlete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtTotalFlete.Enabled = False
        Me.txtTotalFlete.Location = New System.Drawing.Point(684, 114)
        Me.txtTotalFlete.Name = "txtTotalFlete"
        Me.txtTotalFlete.Size = New System.Drawing.Size(71, 19)
        Me.txtTotalFlete.TabIndex = 24
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.Color.White
        Me.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtTotal.Enabled = False
        Me.txtTotal.Location = New System.Drawing.Point(611, 73)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(71, 19)
        Me.txtTotal.TabIndex = 15
        '
        'txtMontoIvaFlete
        '
        Me.txtMontoIvaFlete.BackColor = System.Drawing.Color.White
        Me.txtMontoIvaFlete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtMontoIvaFlete.Enabled = False
        Me.txtMontoIvaFlete.Location = New System.Drawing.Point(622, 115)
        Me.txtMontoIvaFlete.Name = "txtMontoIvaFlete"
        Me.txtMontoIvaFlete.Size = New System.Drawing.Size(56, 19)
        Me.txtMontoIvaFlete.TabIndex = 23
        '
        'txtMontoIva
        '
        Me.txtMontoIva.BackColor = System.Drawing.Color.White
        Me.txtMontoIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtMontoIva.Enabled = False
        Me.txtMontoIva.Location = New System.Drawing.Point(397, 73)
        Me.txtMontoIva.Name = "txtMontoIva"
        Me.txtMontoIva.Size = New System.Drawing.Size(56, 19)
        Me.txtMontoIva.TabIndex = 12
        '
        'lblTipoFactFlete
        '
        Me.lblTipoFactFlete.AutoSize = True
        Me.lblTipoFactFlete.Enabled = False
        Me.lblTipoFactFlete.Location = New System.Drawing.Point(328, 98)
        Me.lblTipoFactFlete.Name = "lblTipoFactFlete"
        Me.lblTipoFactFlete.Size = New System.Drawing.Size(56, 13)
        Me.lblTipoFactFlete.TabIndex = 220
        Me.lblTipoFactFlete.Text = "Tipo Fact*"
        '
        'cmbTipoFactFlete
        '
        Me.cmbTipoFactFlete.AccessibleName = ""
        Me.cmbTipoFactFlete.DropDownHeight = 500
        Me.cmbTipoFactFlete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoFactFlete.Enabled = False
        Me.cmbTipoFactFlete.FormattingEnabled = True
        Me.cmbTipoFactFlete.IntegralHeight = False
        Me.cmbTipoFactFlete.Items.AddRange(New Object() {"", "A", "B", "C"})
        Me.cmbTipoFactFlete.Location = New System.Drawing.Point(331, 114)
        Me.cmbTipoFactFlete.Name = "cmbTipoFactFlete"
        Me.cmbTipoFactFlete.Size = New System.Drawing.Size(50, 21)
        Me.cmbTipoFactFlete.TabIndex = 19
        '
        'lblTipoFact
        '
        Me.lblTipoFact.AutoSize = True
        Me.lblTipoFact.Enabled = False
        Me.lblTipoFact.Location = New System.Drawing.Point(114, 56)
        Me.lblTipoFact.Name = "lblTipoFact"
        Me.lblTipoFact.Size = New System.Drawing.Size(56, 13)
        Me.lblTipoFact.TabIndex = 218
        Me.lblTipoFact.Text = "Tipo Fact*"
        '
        'cmbTipoFact
        '
        Me.cmbTipoFact.AccessibleName = ""
        Me.cmbTipoFact.DropDownHeight = 500
        Me.cmbTipoFact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoFact.Enabled = False
        Me.cmbTipoFact.FormattingEnabled = True
        Me.cmbTipoFact.IntegralHeight = False
        Me.cmbTipoFact.Items.AddRange(New Object() {"", "A", "B", "C"})
        Me.cmbTipoFact.Location = New System.Drawing.Point(117, 72)
        Me.cmbTipoFact.Name = "cmbTipoFact"
        Me.cmbTipoFact.Size = New System.Drawing.Size(50, 21)
        Me.cmbTipoFact.TabIndex = 8
        '
        'txtPorcIvaFlete
        '
        Me.txtPorcIvaFlete.AccessibleName = ""
        Me.txtPorcIvaFlete.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcIvaFlete.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPorcIvaFlete.Decimals = CType(2, Byte)
        Me.txtPorcIvaFlete.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPorcIvaFlete.Enabled = False
        Me.txtPorcIvaFlete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPorcIvaFlete.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPorcIvaFlete.Location = New System.Drawing.Point(579, 114)
        Me.txtPorcIvaFlete.MaxLength = 25
        Me.txtPorcIvaFlete.Name = "txtPorcIvaFlete"
        Me.txtPorcIvaFlete.Size = New System.Drawing.Size(37, 20)
        Me.txtPorcIvaFlete.TabIndex = 22
        Me.txtPorcIvaFlete.Text_1 = Nothing
        Me.txtPorcIvaFlete.Text_2 = Nothing
        Me.txtPorcIvaFlete.Text_3 = Nothing
        Me.txtPorcIvaFlete.Text_4 = Nothing
        Me.txtPorcIvaFlete.UserValues = Nothing
        '
        'lblPorcIvaFlete
        '
        Me.lblPorcIvaFlete.AutoSize = True
        Me.lblPorcIvaFlete.Enabled = False
        Me.lblPorcIvaFlete.Location = New System.Drawing.Point(578, 98)
        Me.lblPorcIvaFlete.Name = "lblPorcIvaFlete"
        Me.lblPorcIvaFlete.Size = New System.Drawing.Size(35, 13)
        Me.lblPorcIvaFlete.TabIndex = 216
        Me.lblPorcIvaFlete.Text = "% IVA"
        '
        'txtPorcIva
        '
        Me.txtPorcIva.AccessibleName = ""
        Me.txtPorcIva.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcIva.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPorcIva.Decimals = CType(2, Byte)
        Me.txtPorcIva.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPorcIva.Enabled = False
        Me.txtPorcIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPorcIva.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPorcIva.Location = New System.Drawing.Point(354, 72)
        Me.txtPorcIva.MaxLength = 25
        Me.txtPorcIva.Name = "txtPorcIva"
        Me.txtPorcIva.Size = New System.Drawing.Size(37, 20)
        Me.txtPorcIva.TabIndex = 11
        Me.txtPorcIva.Text_1 = Nothing
        Me.txtPorcIva.Text_2 = Nothing
        Me.txtPorcIva.Text_3 = Nothing
        Me.txtPorcIva.Text_4 = Nothing
        Me.txtPorcIva.UserValues = Nothing
        '
        'lblPorcIva
        '
        Me.lblPorcIva.AutoSize = True
        Me.lblPorcIva.Enabled = False
        Me.lblPorcIva.Location = New System.Drawing.Point(353, 56)
        Me.lblPorcIva.Name = "lblPorcIva"
        Me.lblPorcIva.Size = New System.Drawing.Size(35, 13)
        Me.lblPorcIva.TabIndex = 214
        Me.lblPorcIva.Text = "% IVA"
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(656, 30)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(647, 20)
        Me.txtNota.TabIndex = 5
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(653, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'lblMontoIvaFlete
        '
        Me.lblMontoIvaFlete.AutoSize = True
        Me.lblMontoIvaFlete.Enabled = False
        Me.lblMontoIvaFlete.Location = New System.Drawing.Point(621, 98)
        Me.lblMontoIvaFlete.Name = "lblMontoIvaFlete"
        Me.lblMontoIvaFlete.Size = New System.Drawing.Size(57, 13)
        Me.lblMontoIvaFlete.TabIndex = 212
        Me.lblMontoIvaFlete.Text = "Monto IVA"
        '
        'txtSubtotalFlete
        '
        Me.txtSubtotalFlete.AccessibleName = ""
        Me.txtSubtotalFlete.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubtotalFlete.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotalFlete.Decimals = CType(2, Byte)
        Me.txtSubtotalFlete.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalFlete.Enabled = False
        Me.txtSubtotalFlete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotalFlete.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotalFlete.Location = New System.Drawing.Point(502, 114)
        Me.txtSubtotalFlete.MaxLength = 25
        Me.txtSubtotalFlete.Name = "txtSubtotalFlete"
        Me.txtSubtotalFlete.Size = New System.Drawing.Size(71, 20)
        Me.txtSubtotalFlete.TabIndex = 21
        Me.txtSubtotalFlete.Text_1 = Nothing
        Me.txtSubtotalFlete.Text_2 = Nothing
        Me.txtSubtotalFlete.Text_3 = Nothing
        Me.txtSubtotalFlete.Text_4 = Nothing
        Me.txtSubtotalFlete.UserValues = Nothing
        '
        'lblSubtotalFlete
        '
        Me.lblSubtotalFlete.AutoSize = True
        Me.lblSubtotalFlete.Enabled = False
        Me.lblSubtotalFlete.Location = New System.Drawing.Point(499, 98)
        Me.lblSubtotalFlete.Name = "lblSubtotalFlete"
        Me.lblSubtotalFlete.Size = New System.Drawing.Size(50, 13)
        Me.lblSubtotalFlete.TabIndex = 211
        Me.lblSubtotalFlete.Text = "Subtotal*"
        '
        'lblProveedorFlete
        '
        Me.lblProveedorFlete.AutoSize = True
        Me.lblProveedorFlete.Enabled = False
        Me.lblProveedorFlete.Location = New System.Drawing.Point(114, 98)
        Me.lblProveedorFlete.Name = "lblProveedorFlete"
        Me.lblProveedorFlete.Size = New System.Drawing.Size(93, 13)
        Me.lblProveedorFlete.TabIndex = 208
        Me.lblProveedorFlete.Text = "Empresa de Flete*"
        '
        'cmbProveedorFlete
        '
        Me.cmbProveedorFlete.AccessibleName = ""
        Me.cmbProveedorFlete.DropDownHeight = 500
        Me.cmbProveedorFlete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProveedorFlete.Enabled = False
        Me.cmbProveedorFlete.FormattingEnabled = True
        Me.cmbProveedorFlete.IntegralHeight = False
        Me.cmbProveedorFlete.Location = New System.Drawing.Point(117, 114)
        Me.cmbProveedorFlete.Name = "cmbProveedorFlete"
        Me.cmbProveedorFlete.Size = New System.Drawing.Size(208, 21)
        Me.cmbProveedorFlete.TabIndex = 18
        '
        'chkFleteSaldado
        '
        Me.chkFleteSaldado.AutoSize = True
        Me.chkFleteSaldado.Enabled = False
        Me.chkFleteSaldado.Location = New System.Drawing.Point(763, 116)
        Me.chkFleteSaldado.Name = "chkFleteSaldado"
        Me.chkFleteSaldado.Size = New System.Drawing.Size(91, 17)
        Me.chkFleteSaldado.TabIndex = 25
        Me.chkFleteSaldado.Text = "Flete Saldado"
        Me.chkFleteSaldado.UseVisualStyleBackColor = True
        '
        'lblTotalFlete
        '
        Me.lblTotalFlete.AutoSize = True
        Me.lblTotalFlete.Enabled = False
        Me.lblTotalFlete.Location = New System.Drawing.Point(683, 98)
        Me.lblTotalFlete.Name = "lblTotalFlete"
        Me.lblTotalFlete.Size = New System.Drawing.Size(31, 13)
        Me.lblTotalFlete.TabIndex = 205
        Me.lblTotalFlete.Text = "Total"
        '
        'chkFlete
        '
        Me.chkFlete.AutoSize = True
        Me.chkFlete.Enabled = False
        Me.chkFlete.Location = New System.Drawing.Point(15, 116)
        Me.chkFlete.Name = "chkFlete"
        Me.chkFlete.Size = New System.Drawing.Size(83, 17)
        Me.chkFlete.TabIndex = 17
        Me.chkFlete.Text = "Cargar Flete"
        Me.chkFlete.UseVisualStyleBackColor = True
        '
        'txtFacturaFlete
        '
        Me.txtFacturaFlete.AccessibleName = ""
        Me.txtFacturaFlete.BackColor = System.Drawing.SystemColors.Window
        Me.txtFacturaFlete.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFacturaFlete.Decimals = CType(2, Byte)
        Me.txtFacturaFlete.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtFacturaFlete.Enabled = False
        Me.txtFacturaFlete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacturaFlete.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtFacturaFlete.Location = New System.Drawing.Point(392, 114)
        Me.txtFacturaFlete.MaxLength = 25
        Me.txtFacturaFlete.Name = "txtFacturaFlete"
        Me.txtFacturaFlete.Size = New System.Drawing.Size(98, 20)
        Me.txtFacturaFlete.TabIndex = 20
        Me.txtFacturaFlete.Text_1 = Nothing
        Me.txtFacturaFlete.Text_2 = Nothing
        Me.txtFacturaFlete.Text_3 = Nothing
        Me.txtFacturaFlete.Text_4 = Nothing
        Me.txtFacturaFlete.UserValues = Nothing
        '
        'lblFacturaFlete
        '
        Me.lblFacturaFlete.AutoSize = True
        Me.lblFacturaFlete.Enabled = False
        Me.lblFacturaFlete.Location = New System.Drawing.Point(389, 98)
        Me.lblFacturaFlete.Name = "lblFacturaFlete"
        Me.lblFacturaFlete.Size = New System.Drawing.Size(107, 13)
        Me.lblFacturaFlete.TabIndex = 202
        Me.lblFacturaFlete.Text = "Nro Remito / Factura"
        '
        'chkFacturaCancelada
        '
        Me.chkFacturaCancelada.AutoSize = True
        Me.chkFacturaCancelada.Enabled = False
        Me.chkFacturaCancelada.Location = New System.Drawing.Point(688, 75)
        Me.chkFacturaCancelada.Name = "chkFacturaCancelada"
        Me.chkFacturaCancelada.Size = New System.Drawing.Size(92, 17)
        Me.chkFacturaCancelada.TabIndex = 16
        Me.chkFacturaCancelada.Text = "Habilitar Pago"
        Me.chkFacturaCancelada.UseVisualStyleBackColor = True
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.Enabled = False
        Me.LblTotal.Location = New System.Drawing.Point(608, 55)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(31, 13)
        Me.LblTotal.TabIndex = 189
        Me.LblTotal.Text = "Total"
        '
        'lblMontoIVA
        '
        Me.lblMontoIVA.AutoSize = True
        Me.lblMontoIVA.Enabled = False
        Me.lblMontoIVA.Location = New System.Drawing.Point(396, 56)
        Me.lblMontoIVA.Name = "lblMontoIVA"
        Me.lblMontoIVA.Size = New System.Drawing.Size(57, 13)
        Me.lblMontoIVA.TabIndex = 187
        Me.lblMontoIVA.Text = "Monto IVA"
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = ""
        Me.txtSubtotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubtotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotal.Enabled = False
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotal.Location = New System.Drawing.Point(277, 72)
        Me.txtSubtotal.MaxLength = 25
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(71, 20)
        Me.txtSubtotal.TabIndex = 10
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'lblSubtotal
        '
        Me.lblSubtotal.AutoSize = True
        Me.lblSubtotal.Enabled = False
        Me.lblSubtotal.Location = New System.Drawing.Point(274, 56)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(50, 13)
        Me.lblSubtotal.TabIndex = 185
        Me.lblSubtotal.Text = "Subtotal*"
        '
        'chkCargarGasto
        '
        Me.chkCargarGasto.AutoSize = True
        Me.chkCargarGasto.Enabled = False
        Me.chkCargarGasto.Location = New System.Drawing.Point(15, 74)
        Me.chkCargarGasto.Name = "chkCargarGasto"
        Me.chkCargarGasto.Size = New System.Drawing.Size(96, 17)
        Me.chkCargarGasto.TabIndex = 7
        Me.chkCargarGasto.Text = "Cargar Factura"
        Me.chkCargarGasto.UseVisualStyleBackColor = True
        '
        'txtNroFactura
        '
        Me.txtNroFactura.AccessibleName = ""
        Me.txtNroFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroFactura.Decimals = CType(2, Byte)
        Me.txtNroFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroFactura.Enabled = False
        Me.txtNroFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroFactura.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroFactura.Location = New System.Drawing.Point(173, 72)
        Me.txtNroFactura.MaxLength = 25
        Me.txtNroFactura.Name = "txtNroFactura"
        Me.txtNroFactura.Size = New System.Drawing.Size(98, 20)
        Me.txtNroFactura.TabIndex = 9
        Me.txtNroFactura.Text_1 = Nothing
        Me.txtNroFactura.Text_2 = Nothing
        Me.txtNroFactura.Text_3 = Nothing
        Me.txtNroFactura.Text_4 = Nothing
        Me.txtNroFactura.UserValues = Nothing
        '
        'lblFactura
        '
        Me.lblFactura.AutoSize = True
        Me.lblFactura.Enabled = False
        Me.lblFactura.Location = New System.Drawing.Point(170, 56)
        Me.lblFactura.Name = "lblFactura"
        Me.lblFactura.Size = New System.Drawing.Size(63, 13)
        Me.lblFactura.TabIndex = 182
        Me.lblFactura.Text = "Nro Factura"
        '
        'txtOC
        '
        Me.txtOC.AccessibleName = ""
        Me.txtOC.BackColor = System.Drawing.SystemColors.Window
        Me.txtOC.Decimals = CType(2, Byte)
        Me.txtOC.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOC.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtOC.Location = New System.Drawing.Point(438, 30)
        Me.txtOC.MaxLength = 25
        Me.txtOC.Name = "txtOC"
        Me.txtOC.ReadOnly = True
        Me.txtOC.Size = New System.Drawing.Size(89, 20)
        Me.txtOC.TabIndex = 3
        Me.txtOC.Text_1 = Nothing
        Me.txtOC.Text_2 = Nothing
        Me.txtOC.Text_3 = Nothing
        Me.txtOC.Text_4 = Nothing
        Me.txtOC.UserValues = Nothing
        '
        'txtProveedor
        '
        Me.txtProveedor.AccessibleName = ""
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Decimals = CType(2, Byte)
        Me.txtProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProveedor.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtProveedor.Location = New System.Drawing.Point(224, 30)
        Me.txtProveedor.MaxLength = 25
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.Size = New System.Drawing.Size(208, 20)
        Me.txtProveedor.TabIndex = 2
        Me.txtProveedor.Text_1 = Nothing
        Me.txtProveedor.Text_2 = Nothing
        Me.txtProveedor.Text_3 = Nothing
        Me.txtProveedor.Text_4 = Nothing
        Me.txtProveedor.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(222, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 162
        Me.Label9.Text = "Proveedor*"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AccessibleName = "*Proveedor"
        Me.cmbProveedor.DropDownHeight = 500
        Me.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.IntegralHeight = False
        Me.cmbProveedor.Location = New System.Drawing.Point(225, 30)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(208, 21)
        Me.cmbProveedor.TabIndex = 161
        '
        'btnRecibirTodos
        '
        Me.btnRecibirTodos.Enabled = False
        Me.btnRecibirTodos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecibirTodos.Location = New System.Drawing.Point(1089, 130)
        Me.btnRecibirTodos.Name = "btnRecibirTodos"
        Me.btnRecibirTodos.Size = New System.Drawing.Size(102, 23)
        Me.btnRecibirTodos.TabIndex = 6
        Me.btnRecibirTodos.Text = "Recibir Todos"
        Me.btnRecibirTodos.UseVisualStyleBackColor = True
        '
        'txtRemito
        '
        Me.txtRemito.AccessibleName = ""
        Me.txtRemito.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemito.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemito.Decimals = CType(2, Byte)
        Me.txtRemito.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemito.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtRemito.Location = New System.Drawing.Point(534, 31)
        Me.txtRemito.MaxLength = 25
        Me.txtRemito.Name = "txtRemito"
        Me.txtRemito.Size = New System.Drawing.Size(116, 20)
        Me.txtRemito.TabIndex = 4
        Me.txtRemito.Text_1 = Nothing
        Me.txtRemito.Text_2 = Nothing
        Me.txtRemito.Text_3 = Nothing
        Me.txtRemito.Text_4 = Nothing
        Me.txtRemito.UserValues = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(534, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 13)
        Me.Label6.TabIndex = 128
        Me.Label6.Text = "Nro Remito / Factura"
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(1089, 180)
        Me.btnLlenarGrilla.Name = "btnLlenarGrilla"
        Me.btnLlenarGrilla.Size = New System.Drawing.Size(115, 23)
        Me.btnLlenarGrilla.TabIndex = 12
        Me.btnLlenarGrilla.Text = "Llenar Grilla"
        Me.btnLlenarGrilla.UseVisualStyleBackColor = True
        Me.btnLlenarGrilla.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(438, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 125
        Me.Label5.Text = "Orden de Compra"
        '
        'cmbOrdenDeCompra
        '
        Me.cmbOrdenDeCompra.AccessibleName = "*OrdendeCompra"
        Me.cmbOrdenDeCompra.FormattingEnabled = True
        Me.cmbOrdenDeCompra.Location = New System.Drawing.Point(439, 30)
        Me.cmbOrdenDeCompra.Name = "cmbOrdenDeCompra"
        Me.cmbOrdenDeCompra.Size = New System.Drawing.Size(89, 21)
        Me.cmbOrdenDeCompra.TabIndex = 124
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(1232, 14)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
        Me.chkEliminado.TabIndex = 116
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(12, 159)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1291, 247)
        Me.grdItems.TabIndex = 28
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
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 31)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(98, 20)
        Me.txtCODIGO.TabIndex = 0
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Recepción"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(117, 31)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(114, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'chkPrecioxMt
        '
        Me.chkPrecioxMt.Location = New System.Drawing.Point(643, 136)
        Me.chkPrecioxMt.Name = "chkPrecioxMt"
        Me.chkPrecioxMt.Size = New System.Drawing.Size(155, 23)
        Me.chkPrecioxMt.TabIndex = 27
        Me.chkPrecioxMt.Text = "Mostrar PrecioxMt, etc..."
        Me.chkPrecioxMt.TextColor = System.Drawing.Color.Maroon
        '
        'chkMostarColumnas
        '
        Me.chkMostarColumnas.Location = New System.Drawing.Point(508, 136)
        Me.chkMostarColumnas.Name = "chkMostarColumnas"
        Me.chkMostarColumnas.Size = New System.Drawing.Size(129, 23)
        Me.chkMostarColumnas.TabIndex = 26
        Me.chkMostarColumnas.Text = "Mostrar Bonif4, etc..."
        Me.chkMostarColumnas.TextColor = System.Drawing.Color.Maroon
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 73)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(360, 22)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(360, 22)
        Me.BuscarToolStripMenuItem.Text = "Buscar..."
        Me.BuscarToolStripMenuItem.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem
        '
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 21)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'frmRecepciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1321, 481)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmRecepciones"
        Me.Text = "frmRecepciones"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbOrdenDeCompra As System.Windows.Forms.ComboBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Friend WithEvents txtRemito As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIdProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnRecibirTodos As System.Windows.Forms.Button
    Friend WithEvents txtProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents txtOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkMostarColumnas As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkPrecioxMt As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LblTotal As System.Windows.Forms.Label
    Friend WithEvents lblMontoIVA As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents chkCargarGasto As System.Windows.Forms.CheckBox
    Friend WithEvents txtNroFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblFactura As System.Windows.Forms.Label
    Friend WithEvents chkFacturaCancelada As System.Windows.Forms.CheckBox
    Friend WithEvents txtIdGasto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtidpago As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblProveedorFlete As System.Windows.Forms.Label
    Friend WithEvents cmbProveedorFlete As System.Windows.Forms.ComboBox
    Friend WithEvents chkFleteSaldado As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalFlete As System.Windows.Forms.Label
    Friend WithEvents chkFlete As System.Windows.Forms.CheckBox
    Friend WithEvents txtFacturaFlete As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblFacturaFlete As System.Windows.Forms.Label
    Friend WithEvents lblMontoIvaFlete As System.Windows.Forms.Label
    Friend WithEvents txtSubtotalFlete As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblSubtotalFlete As System.Windows.Forms.Label
    Friend WithEvents txtPorcIva As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblPorcIva As System.Windows.Forms.Label
    Friend WithEvents txtPorcIvaFlete As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblPorcIvaFlete As System.Windows.Forms.Label
    Friend WithEvents lblTipoFactFlete As System.Windows.Forms.Label
    Friend WithEvents cmbTipoFactFlete As System.Windows.Forms.ComboBox
    Friend WithEvents lblTipoFact As System.Windows.Forms.Label
    Friend WithEvents cmbTipoFact As System.Windows.Forms.ComboBox
    Friend WithEvents txtMontoIva As System.Windows.Forms.Label
    Friend WithEvents txtMontoIvaFlete As System.Windows.Forms.Label
    Friend WithEvents txtTotalFlete As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.Label
    Friend WithEvents txtOtrosImp As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblOtrosImp As System.Windows.Forms.Label
    Friend WithEvents txtIIBB As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblIIBB As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
