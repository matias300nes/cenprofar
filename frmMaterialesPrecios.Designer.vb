
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterialesPrecios

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaterialesPrecios))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbUnidadCompra = New System.Windows.Forms.ToolStripComboBox
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbMonedasCompra = New System.Windows.Forms.ToolStripComboBox
        Me.cmbFAMILIAS = New System.Windows.Forms.ComboBox
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.cmbSubRubro = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkOcultarGanancia = New System.Windows.Forms.CheckBox
        Me.PicAplicarAumento = New System.Windows.Forms.PictureBox
        Me.PicOrden = New System.Windows.Forms.PictureBox
        Me.picAumento = New System.Windows.Forms.PictureBox
        Me.chkProveedor = New System.Windows.Forms.CheckBox
        Me.cmbProveedor = New System.Windows.Forms.ComboBox
        Me.chkPrecioLista = New System.Windows.Forms.CheckBox
        Me.lblMsjGanancia = New System.Windows.Forms.Label
        Me.lblMsjBonif5 = New System.Windows.Forms.Label
        Me.lblMsjBonif4 = New System.Windows.Forms.Label
        Me.lblMsjBonif3 = New System.Windows.Forms.Label
        Me.lblMsjBonif2 = New System.Windows.Forms.Label
        Me.lblMsjBonif1 = New System.Windows.Forms.Label
        Me.lblMsjProv = New System.Windows.Forms.Label
        Me.chkGanancia = New System.Windows.Forms.CheckBox
        Me.chkBonif2 = New System.Windows.Forms.CheckBox
        Me.chkBonif3 = New System.Windows.Forms.CheckBox
        Me.chkBonif4 = New System.Windows.Forms.CheckBox
        Me.chkBonif5 = New System.Windows.Forms.CheckBox
        Me.chkBonif1 = New System.Windows.Forms.CheckBox
        Me.txtGanancia = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtBonif2 = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtBonif3 = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtBonif4 = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtBonif5 = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtBonif1 = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.rdPrecioTabl = New System.Windows.Forms.RadioButton
        Me.rdPrecioDist = New System.Windows.Forms.RadioButton
        Me.chkAumento = New System.Windows.Forms.CheckBox
        Me.btnGuardarCriterio = New System.Windows.Forms.Button
        Me.btnEliminarCriterio = New System.Windows.Forms.Button
        Me.cmbNombre = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkModificarOrden = New System.Windows.Forms.CheckBox
        Me.btnCancelarAumento = New System.Windows.Forms.Button
        Me.btnAplicarAumento = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAumento = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.chkSubRubro = New System.Windows.Forms.CheckBox
        Me.chkRubro = New System.Windows.Forms.CheckBox
        Me.chkNombre = New System.Windows.Forms.CheckBox
        Me.chkCodigo = New System.Windows.Forms.CheckBox
        Me.lblAyudaBonif = New System.Windows.Forms.Label
        Me.lblAyudaPrecioLista = New System.Windows.Forms.Label
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicAplicarAumento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicOrden, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAumento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 202)
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
        Me.BuscarDescripcionToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 150)
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbUnidadCompra})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(361, 158)
        '
        'cmbUnidadCompra
        '
        Me.cmbUnidadCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbUnidadCompra.DropDownWidth = 500
        Me.cmbUnidadCompra.Name = "cmbUnidadCompra"
        Me.cmbUnidadCompra.Size = New System.Drawing.Size(300, 150)
        Me.cmbUnidadCompra.Text = "Buscar Unidad"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbMonedasCompra})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(361, 158)
        '
        'cmbMonedasCompra
        '
        Me.cmbMonedasCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbMonedasCompra.DropDownWidth = 500
        Me.cmbMonedasCompra.Name = "cmbMonedasCompra"
        Me.cmbMonedasCompra.Size = New System.Drawing.Size(300, 150)
        Me.cmbMonedasCompra.Text = "Buscar Moneda"
        '
        'cmbFAMILIAS
        '
        Me.cmbFAMILIAS.AccessibleName = ""
        Me.cmbFAMILIAS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFAMILIAS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFAMILIAS.Enabled = False
        Me.cmbFAMILIAS.FormattingEnabled = True
        Me.cmbFAMILIAS.Location = New System.Drawing.Point(502, 40)
        Me.cmbFAMILIAS.Name = "cmbFAMILIAS"
        Me.cmbFAMILIAS.Size = New System.Drawing.Size(249, 21)
        Me.cmbFAMILIAS.TabIndex = 7
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = ""
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Enabled = False
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 39)
        Me.txtCODIGO.MaxLength = 8
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.Size = New System.Drawing.Size(77, 20)
        Me.txtCODIGO.TabIndex = 1
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        '
        'cmbSubRubro
        '
        Me.cmbSubRubro.AccessibleName = ""
        Me.cmbSubRubro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSubRubro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSubRubro.Enabled = False
        Me.cmbSubRubro.FormattingEnabled = True
        Me.cmbSubRubro.Location = New System.Drawing.Point(757, 39)
        Me.cmbSubRubro.Name = "cmbSubRubro"
        Me.cmbSubRubro.Size = New System.Drawing.Size(249, 21)
        Me.cmbSubRubro.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkOcultarGanancia)
        Me.GroupBox1.Controls.Add(Me.PicAplicarAumento)
        Me.GroupBox1.Controls.Add(Me.PicOrden)
        Me.GroupBox1.Controls.Add(Me.picAumento)
        Me.GroupBox1.Controls.Add(Me.chkProveedor)
        Me.GroupBox1.Controls.Add(Me.cmbProveedor)
        Me.GroupBox1.Controls.Add(Me.chkPrecioLista)
        Me.GroupBox1.Controls.Add(Me.lblMsjGanancia)
        Me.GroupBox1.Controls.Add(Me.lblMsjBonif5)
        Me.GroupBox1.Controls.Add(Me.lblMsjBonif4)
        Me.GroupBox1.Controls.Add(Me.lblMsjBonif3)
        Me.GroupBox1.Controls.Add(Me.lblMsjBonif2)
        Me.GroupBox1.Controls.Add(Me.lblMsjBonif1)
        Me.GroupBox1.Controls.Add(Me.lblMsjProv)
        Me.GroupBox1.Controls.Add(Me.chkGanancia)
        Me.GroupBox1.Controls.Add(Me.chkBonif2)
        Me.GroupBox1.Controls.Add(Me.chkBonif3)
        Me.GroupBox1.Controls.Add(Me.chkBonif4)
        Me.GroupBox1.Controls.Add(Me.chkBonif5)
        Me.GroupBox1.Controls.Add(Me.chkBonif1)
        Me.GroupBox1.Controls.Add(Me.txtGanancia)
        Me.GroupBox1.Controls.Add(Me.txtBonif2)
        Me.GroupBox1.Controls.Add(Me.txtBonif3)
        Me.GroupBox1.Controls.Add(Me.txtBonif4)
        Me.GroupBox1.Controls.Add(Me.txtBonif5)
        Me.GroupBox1.Controls.Add(Me.txtBonif1)
        Me.GroupBox1.Controls.Add(Me.chkAumento)
        Me.GroupBox1.Controls.Add(Me.btnGuardarCriterio)
        Me.GroupBox1.Controls.Add(Me.btnEliminarCriterio)
        Me.GroupBox1.Controls.Add(Me.cmbNombre)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkModificarOrden)
        Me.GroupBox1.Controls.Add(Me.btnCancelarAumento)
        Me.GroupBox1.Controls.Add(Me.btnAplicarAumento)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.rdPrecioDist)
        Me.GroupBox1.Controls.Add(Me.rdPrecioTabl)
        Me.GroupBox1.Controls.Add(Me.txtAumento)
        Me.GroupBox1.Controls.Add(Me.chkSubRubro)
        Me.GroupBox1.Controls.Add(Me.chkRubro)
        Me.GroupBox1.Controls.Add(Me.chkNombre)
        Me.GroupBox1.Controls.Add(Me.chkCodigo)
        Me.GroupBox1.Controls.Add(Me.cmbSubRubro)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.cmbFAMILIAS)
        Me.GroupBox1.Controls.Add(Me.lblAyudaBonif)
        Me.GroupBox1.Controls.Add(Me.lblAyudaPrecioLista)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1322, 193)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkOcultarGanancia
        '
        Me.chkOcultarGanancia.AutoSize = True
        Me.chkOcultarGanancia.Location = New System.Drawing.Point(727, 171)
        Me.chkOcultarGanancia.Name = "chkOcultarGanancia"
        Me.chkOcultarGanancia.Size = New System.Drawing.Size(124, 17)
        Me.chkOcultarGanancia.TabIndex = 220
        Me.chkOcultarGanancia.Text = "Mostrar / Ocultar GC"
        Me.chkOcultarGanancia.UseVisualStyleBackColor = True
        '
        'PicAplicarAumento
        '
        Me.PicAplicarAumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicAplicarAumento.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicAplicarAumento.Image = Global.SEYC.My.Resources.Resources.icono_ayuda
        Me.PicAplicarAumento.Location = New System.Drawing.Point(966, 109)
        Me.PicAplicarAumento.Name = "PicAplicarAumento"
        Me.PicAplicarAumento.Size = New System.Drawing.Size(18, 20)
        Me.PicAplicarAumento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicAplicarAumento.TabIndex = 218
        Me.PicAplicarAumento.TabStop = False
        '
        'PicOrden
        '
        Me.PicOrden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicOrden.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicOrden.Image = Global.SEYC.My.Resources.Resources.icono_ayuda
        Me.PicOrden.Location = New System.Drawing.Point(690, 169)
        Me.PicOrden.Name = "PicOrden"
        Me.PicOrden.Size = New System.Drawing.Size(18, 20)
        Me.PicOrden.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicOrden.TabIndex = 217
        Me.PicOrden.TabStop = False
        '
        'picAumento
        '
        Me.picAumento.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picAumento.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picAumento.Image = Global.SEYC.My.Resources.Resources.icono_ayuda
        Me.picAumento.Location = New System.Drawing.Point(123, 106)
        Me.picAumento.Name = "picAumento"
        Me.picAumento.Size = New System.Drawing.Size(18, 20)
        Me.picAumento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAumento.TabIndex = 216
        Me.picAumento.TabStop = False
        '
        'chkProveedor
        '
        Me.chkProveedor.AutoSize = True
        Me.chkProveedor.Location = New System.Drawing.Point(1012, 19)
        Me.chkProveedor.Name = "chkProveedor"
        Me.chkProveedor.Size = New System.Drawing.Size(75, 17)
        Me.chkProveedor.TabIndex = 214
        Me.chkProveedor.Text = "Proveedor"
        Me.chkProveedor.UseVisualStyleBackColor = True
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AccessibleName = ""
        Me.cmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedor.Enabled = False
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(1012, 38)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(249, 21)
        Me.cmbProveedor.TabIndex = 213
        '
        'chkPrecioLista
        '
        Me.chkPrecioLista.AutoSize = True
        Me.chkPrecioLista.Enabled = False
        Me.chkPrecioLista.Location = New System.Drawing.Point(644, 90)
        Me.chkPrecioLista.Name = "chkPrecioLista"
        Me.chkPrecioLista.Size = New System.Drawing.Size(96, 17)
        Me.chkPrecioLista.TabIndex = 212
        Me.chkPrecioLista.Text = "Precio de Lista"
        Me.chkPrecioLista.UseVisualStyleBackColor = True
        '
        'lblMsjGanancia
        '
        Me.lblMsjGanancia.AutoSize = True
        Me.lblMsjGanancia.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjGanancia.Location = New System.Drawing.Point(439, 132)
        Me.lblMsjGanancia.Name = "lblMsjGanancia"
        Me.lblMsjGanancia.Size = New System.Drawing.Size(68, 13)
        Me.lblMsjGanancia.TabIndex = 211
        Me.lblMsjGanancia.Text = "+1 Ganancia"
        Me.lblMsjGanancia.Visible = False
        '
        'lblMsjBonif5
        '
        Me.lblMsjBonif5.AutoSize = True
        Me.lblMsjBonif5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjBonif5.Location = New System.Drawing.Point(380, 132)
        Me.lblMsjBonif5.Name = "lblMsjBonif5"
        Me.lblMsjBonif5.Size = New System.Drawing.Size(52, 13)
        Me.lblMsjBonif5.TabIndex = 210
        Me.lblMsjBonif5.Text = "+1 Bonif5"
        Me.lblMsjBonif5.Visible = False
        '
        'lblMsjBonif4
        '
        Me.lblMsjBonif4.AutoSize = True
        Me.lblMsjBonif4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjBonif4.Location = New System.Drawing.Point(321, 132)
        Me.lblMsjBonif4.Name = "lblMsjBonif4"
        Me.lblMsjBonif4.Size = New System.Drawing.Size(52, 13)
        Me.lblMsjBonif4.TabIndex = 209
        Me.lblMsjBonif4.Text = "+1 Bonif4"
        Me.lblMsjBonif4.Visible = False
        '
        'lblMsjBonif3
        '
        Me.lblMsjBonif3.AutoSize = True
        Me.lblMsjBonif3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjBonif3.Location = New System.Drawing.Point(262, 132)
        Me.lblMsjBonif3.Name = "lblMsjBonif3"
        Me.lblMsjBonif3.Size = New System.Drawing.Size(52, 13)
        Me.lblMsjBonif3.TabIndex = 208
        Me.lblMsjBonif3.Text = "+1 Bonif3"
        Me.lblMsjBonif3.Visible = False
        '
        'lblMsjBonif2
        '
        Me.lblMsjBonif2.AutoSize = True
        Me.lblMsjBonif2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjBonif2.Location = New System.Drawing.Point(203, 132)
        Me.lblMsjBonif2.Name = "lblMsjBonif2"
        Me.lblMsjBonif2.Size = New System.Drawing.Size(52, 13)
        Me.lblMsjBonif2.TabIndex = 207
        Me.lblMsjBonif2.Text = "+1 Bonif2"
        Me.lblMsjBonif2.Visible = False
        '
        'lblMsjBonif1
        '
        Me.lblMsjBonif1.AutoSize = True
        Me.lblMsjBonif1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjBonif1.Location = New System.Drawing.Point(144, 132)
        Me.lblMsjBonif1.Name = "lblMsjBonif1"
        Me.lblMsjBonif1.Size = New System.Drawing.Size(52, 13)
        Me.lblMsjBonif1.TabIndex = 206
        Me.lblMsjBonif1.Text = "+1 Bonif1"
        Me.lblMsjBonif1.Visible = False
        '
        'lblMsjProv
        '
        Me.lblMsjProv.AutoSize = True
        Me.lblMsjProv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsjProv.Location = New System.Drawing.Point(41, 132)
        Me.lblMsjProv.Name = "lblMsjProv"
        Me.lblMsjProv.Size = New System.Drawing.Size(71, 13)
        Me.lblMsjProv.TabIndex = 203
        Me.lblMsjProv.Text = "+1 Proveedor"
        Me.lblMsjProv.Visible = False
        '
        'chkGanancia
        '
        Me.chkGanancia.AutoSize = True
        Me.chkGanancia.Enabled = False
        Me.chkGanancia.Location = New System.Drawing.Point(442, 90)
        Me.chkGanancia.Name = "chkGanancia"
        Me.chkGanancia.Size = New System.Drawing.Size(72, 17)
        Me.chkGanancia.TabIndex = 21
        Me.chkGanancia.Text = "Ganancia"
        Me.chkGanancia.UseVisualStyleBackColor = True
        '
        'chkBonif2
        '
        Me.chkBonif2.AutoSize = True
        Me.chkBonif2.Enabled = False
        Me.chkBonif2.Location = New System.Drawing.Point(206, 90)
        Me.chkBonif2.Name = "chkBonif2"
        Me.chkBonif2.Size = New System.Drawing.Size(56, 17)
        Me.chkBonif2.TabIndex = 13
        Me.chkBonif2.Text = "Bonif2"
        Me.chkBonif2.UseVisualStyleBackColor = True
        '
        'chkBonif3
        '
        Me.chkBonif3.AutoSize = True
        Me.chkBonif3.Enabled = False
        Me.chkBonif3.Location = New System.Drawing.Point(265, 90)
        Me.chkBonif3.Name = "chkBonif3"
        Me.chkBonif3.Size = New System.Drawing.Size(56, 17)
        Me.chkBonif3.TabIndex = 15
        Me.chkBonif3.Text = "Bonif3"
        Me.chkBonif3.UseVisualStyleBackColor = True
        Me.chkBonif3.Visible = False
        '
        'chkBonif4
        '
        Me.chkBonif4.AutoSize = True
        Me.chkBonif4.Enabled = False
        Me.chkBonif4.Location = New System.Drawing.Point(324, 90)
        Me.chkBonif4.Name = "chkBonif4"
        Me.chkBonif4.Size = New System.Drawing.Size(56, 17)
        Me.chkBonif4.TabIndex = 17
        Me.chkBonif4.Text = "Bonif4"
        Me.chkBonif4.UseVisualStyleBackColor = True
        Me.chkBonif4.Visible = False
        '
        'chkBonif5
        '
        Me.chkBonif5.AutoSize = True
        Me.chkBonif5.Enabled = False
        Me.chkBonif5.Location = New System.Drawing.Point(383, 90)
        Me.chkBonif5.Name = "chkBonif5"
        Me.chkBonif5.Size = New System.Drawing.Size(56, 17)
        Me.chkBonif5.TabIndex = 19
        Me.chkBonif5.Text = "Bonif5"
        Me.chkBonif5.UseVisualStyleBackColor = True
        Me.chkBonif5.Visible = False
        '
        'chkBonif1
        '
        Me.chkBonif1.AutoSize = True
        Me.chkBonif1.Enabled = False
        Me.chkBonif1.Location = New System.Drawing.Point(147, 90)
        Me.chkBonif1.Name = "chkBonif1"
        Me.chkBonif1.Size = New System.Drawing.Size(56, 17)
        Me.chkBonif1.TabIndex = 11
        Me.chkBonif1.Text = "Bonif1"
        Me.chkBonif1.UseVisualStyleBackColor = True
        '
        'txtGanancia
        '
        Me.txtGanancia.AccessibleName = ""
        Me.txtGanancia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGanancia.Decimals = CType(2, Byte)
        Me.txtGanancia.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtGanancia.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtGanancia.Location = New System.Drawing.Point(442, 109)
        Me.txtGanancia.MaxLength = 8
        Me.txtGanancia.Name = "txtGanancia"
        Me.txtGanancia.ReadOnly = True
        Me.txtGanancia.Size = New System.Drawing.Size(53, 20)
        Me.txtGanancia.TabIndex = 22
        Me.txtGanancia.Text_1 = Nothing
        Me.txtGanancia.Text_2 = Nothing
        Me.txtGanancia.Text_3 = Nothing
        Me.txtGanancia.Text_4 = Nothing
        Me.txtGanancia.UserValues = Nothing
        '
        'txtBonif2
        '
        Me.txtBonif2.AccessibleName = ""
        Me.txtBonif2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBonif2.Decimals = CType(2, Byte)
        Me.txtBonif2.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtBonif2.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtBonif2.Location = New System.Drawing.Point(206, 109)
        Me.txtBonif2.MaxLength = 8
        Me.txtBonif2.Name = "txtBonif2"
        Me.txtBonif2.ReadOnly = True
        Me.txtBonif2.Size = New System.Drawing.Size(53, 20)
        Me.txtBonif2.TabIndex = 14
        Me.txtBonif2.Text_1 = Nothing
        Me.txtBonif2.Text_2 = Nothing
        Me.txtBonif2.Text_3 = Nothing
        Me.txtBonif2.Text_4 = Nothing
        Me.txtBonif2.UserValues = Nothing
        '
        'txtBonif3
        '
        Me.txtBonif3.AccessibleName = ""
        Me.txtBonif3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBonif3.Decimals = CType(2, Byte)
        Me.txtBonif3.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtBonif3.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtBonif3.Location = New System.Drawing.Point(265, 109)
        Me.txtBonif3.MaxLength = 8
        Me.txtBonif3.Name = "txtBonif3"
        Me.txtBonif3.ReadOnly = True
        Me.txtBonif3.Size = New System.Drawing.Size(53, 20)
        Me.txtBonif3.TabIndex = 16
        Me.txtBonif3.Text_1 = Nothing
        Me.txtBonif3.Text_2 = Nothing
        Me.txtBonif3.Text_3 = Nothing
        Me.txtBonif3.Text_4 = Nothing
        Me.txtBonif3.UserValues = Nothing
        Me.txtBonif3.Visible = False
        '
        'txtBonif4
        '
        Me.txtBonif4.AccessibleName = ""
        Me.txtBonif4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBonif4.Decimals = CType(2, Byte)
        Me.txtBonif4.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtBonif4.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtBonif4.Location = New System.Drawing.Point(324, 109)
        Me.txtBonif4.MaxLength = 8
        Me.txtBonif4.Name = "txtBonif4"
        Me.txtBonif4.ReadOnly = True
        Me.txtBonif4.Size = New System.Drawing.Size(53, 20)
        Me.txtBonif4.TabIndex = 18
        Me.txtBonif4.Text_1 = Nothing
        Me.txtBonif4.Text_2 = Nothing
        Me.txtBonif4.Text_3 = Nothing
        Me.txtBonif4.Text_4 = Nothing
        Me.txtBonif4.UserValues = Nothing
        Me.txtBonif4.Visible = False
        '
        'txtBonif5
        '
        Me.txtBonif5.AccessibleName = ""
        Me.txtBonif5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBonif5.Decimals = CType(2, Byte)
        Me.txtBonif5.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtBonif5.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtBonif5.Location = New System.Drawing.Point(383, 109)
        Me.txtBonif5.MaxLength = 8
        Me.txtBonif5.Name = "txtBonif5"
        Me.txtBonif5.ReadOnly = True
        Me.txtBonif5.Size = New System.Drawing.Size(53, 20)
        Me.txtBonif5.TabIndex = 20
        Me.txtBonif5.Text_1 = Nothing
        Me.txtBonif5.Text_2 = Nothing
        Me.txtBonif5.Text_3 = Nothing
        Me.txtBonif5.Text_4 = Nothing
        Me.txtBonif5.UserValues = Nothing
        Me.txtBonif5.Visible = False
        '
        'txtBonif1
        '
        Me.txtBonif1.AccessibleName = ""
        Me.txtBonif1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBonif1.Decimals = CType(2, Byte)
        Me.txtBonif1.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtBonif1.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtBonif1.Location = New System.Drawing.Point(147, 109)
        Me.txtBonif1.MaxLength = 8
        Me.txtBonif1.Name = "txtBonif1"
        Me.txtBonif1.ReadOnly = True
        Me.txtBonif1.Size = New System.Drawing.Size(53, 20)
        Me.txtBonif1.TabIndex = 12
        Me.txtBonif1.Text_1 = Nothing
        Me.txtBonif1.Text_2 = Nothing
        Me.txtBonif1.Text_3 = Nothing
        Me.txtBonif1.Text_4 = Nothing
        Me.txtBonif1.UserValues = Nothing
        '
        'chkAumento
        '
        Me.chkAumento.AutoSize = True
        Me.chkAumento.Location = New System.Drawing.Point(13, 111)
        Me.chkAumento.Name = "chkAumento"
        Me.chkAumento.Size = New System.Drawing.Size(109, 17)
        Me.chkAumento.TabIndex = 10
        Me.chkAumento.Text = "Habilitar Aumento"
        Me.chkAumento.UseVisualStyleBackColor = True
        '
        'btnGuardarCriterio
        '
        Me.btnGuardarCriterio.Location = New System.Drawing.Point(376, 37)
        Me.btnGuardarCriterio.Name = "btnGuardarCriterio"
        Me.btnGuardarCriterio.Size = New System.Drawing.Size(56, 25)
        Me.btnGuardarCriterio.TabIndex = 4
        Me.btnGuardarCriterio.Text = "Guardar"
        Me.btnGuardarCriterio.UseVisualStyleBackColor = True
        '
        'btnEliminarCriterio
        '
        Me.btnEliminarCriterio.Location = New System.Drawing.Point(438, 37)
        Me.btnEliminarCriterio.Name = "btnEliminarCriterio"
        Me.btnEliminarCriterio.Size = New System.Drawing.Size(58, 25)
        Me.btnEliminarCriterio.TabIndex = 5
        Me.btnEliminarCriterio.Text = "Eliminar"
        Me.btnEliminarCriterio.UseVisualStyleBackColor = True
        '
        'cmbNombre
        '
        Me.cmbNombre.AccessibleName = ""
        Me.cmbNombre.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNombre.DropDownHeight = 600
        Me.cmbNombre.Enabled = False
        Me.cmbNombre.FormattingEnabled = True
        Me.cmbNombre.IntegralHeight = False
        Me.cmbNombre.Location = New System.Drawing.Point(96, 39)
        Me.cmbNombre.Name = "cmbNombre"
        Me.cmbNombre.Size = New System.Drawing.Size(274, 21)
        Me.cmbNombre.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(93, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(792, 17)
        Me.Label3.TabIndex = 180
        Me.Label3.Text = "El campo Criterio de Búsqueda, permite el uso de la letra ? como comodín. Esto pe" & _
            "rmite efectuar búsquedas por tres criterios para el mismo Nombre. Presione ENTER" & _
            " al finalizar"
        '
        'chkModificarOrden
        '
        Me.chkModificarOrden.AutoSize = True
        Me.chkModificarOrden.Location = New System.Drawing.Point(453, 172)
        Me.chkModificarOrden.Name = "chkModificarOrden"
        Me.chkModificarOrden.Size = New System.Drawing.Size(231, 17)
        Me.chkModificarOrden.TabIndex = 26
        Me.chkModificarOrden.Text = "MODIFICAR SOLO LA COLUMNA ORDEN"
        Me.chkModificarOrden.UseVisualStyleBackColor = True
        '
        'btnCancelarAumento
        '
        Me.btnCancelarAumento.Enabled = False
        Me.btnCancelarAumento.Location = New System.Drawing.Point(853, 108)
        Me.btnCancelarAumento.Name = "btnCancelarAumento"
        Me.btnCancelarAumento.Size = New System.Drawing.Size(107, 23)
        Me.btnCancelarAumento.TabIndex = 25
        Me.btnCancelarAumento.Text = "Cancelar Aumento"
        Me.btnCancelarAumento.UseVisualStyleBackColor = True
        '
        'btnAplicarAumento
        '
        Me.btnAplicarAumento.Enabled = False
        Me.btnAplicarAumento.Location = New System.Drawing.Point(740, 108)
        Me.btnAplicarAumento.Name = "btnAplicarAumento"
        Me.btnAplicarAumento.Size = New System.Drawing.Size(107, 23)
        Me.btnAplicarAumento.TabIndex = 24
        Me.btnAplicarAumento.Text = "Aplicar Aumento"
        Me.btnAplicarAumento.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(702, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 13)
        Me.Label1.TabIndex = 177
        Me.Label1.Text = "%"
        '
        'txtAumento
        '
        Me.txtAumento.AccessibleName = ""
        Me.txtAumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAumento.Decimals = CType(2, Byte)
        Me.txtAumento.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtAumento.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtAumento.Location = New System.Drawing.Point(643, 109)
        Me.txtAumento.MaxLength = 8
        Me.txtAumento.Name = "txtAumento"
        Me.txtAumento.ReadOnly = True
        Me.txtAumento.Size = New System.Drawing.Size(53, 20)
        Me.txtAumento.TabIndex = 23
        Me.txtAumento.Text_1 = Nothing
        Me.txtAumento.Text_2 = Nothing
        Me.txtAumento.Text_3 = Nothing
        Me.txtAumento.Text_4 = Nothing
        Me.txtAumento.UserValues = Nothing
        '
        'chkSubRubro
        '
        Me.chkSubRubro.AutoSize = True
        Me.chkSubRubro.Location = New System.Drawing.Point(757, 19)
        Me.chkSubRubro.Name = "chkSubRubro"
        Me.chkSubRubro.Size = New System.Drawing.Size(74, 17)
        Me.chkSubRubro.TabIndex = 8
        Me.chkSubRubro.Text = "SubRubro"
        Me.chkSubRubro.UseVisualStyleBackColor = True
        '
        'chkRubro
        '
        Me.chkRubro.AutoSize = True
        Me.chkRubro.Location = New System.Drawing.Point(502, 19)
        Me.chkRubro.Name = "chkRubro"
        Me.chkRubro.Size = New System.Drawing.Size(55, 17)
        Me.chkRubro.TabIndex = 6
        Me.chkRubro.Text = "Rubro"
        Me.chkRubro.UseVisualStyleBackColor = True
        '
        'chkNombre
        '
        Me.chkNombre.AutoSize = True
        Me.chkNombre.Location = New System.Drawing.Point(96, 19)
        Me.chkNombre.Name = "chkNombre"
        Me.chkNombre.Size = New System.Drawing.Size(124, 17)
        Me.chkNombre.TabIndex = 2
        Me.chkNombre.Text = "Criterio de Búsqueda"
        Me.chkNombre.UseVisualStyleBackColor = True
        '
        'chkCodigo
        '
        Me.chkCodigo.AutoSize = True
        Me.chkCodigo.Location = New System.Drawing.Point(13, 19)
        Me.chkCodigo.Name = "chkCodigo"
        Me.chkCodigo.Size = New System.Drawing.Size(59, 17)
        Me.chkCodigo.TabIndex = 0
        Me.chkCodigo.Text = "Codigo"
        Me.chkCodigo.UseVisualStyleBackColor = True
        '
        'lblAyudaBonif
        '
        Me.lblAyudaBonif.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAyudaBonif.Location = New System.Drawing.Point(10, 153)
        Me.lblAyudaBonif.Name = "lblAyudaBonif"
        Me.lblAyudaBonif.Size = New System.Drawing.Size(383, 29)
        Me.lblAyudaBonif.TabIndex = 219
        Me.lblAyudaBonif.Text = "Si debajo de las bonificaciones aparece el código +1 significa que dentro de la l" & _
            "ista de materiales seleccionados existen items con diferentes bonificaciones."
        Me.lblAyudaBonif.Visible = False
        '
        'lblAyudaPrecioLista
        '
        Me.lblAyudaPrecioLista.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAyudaPrecioLista.Location = New System.Drawing.Point(641, 132)
        Me.lblAyudaPrecioLista.Name = "lblAyudaPrecioLista"
        Me.lblAyudaPrecioLista.Size = New System.Drawing.Size(287, 29)
        Me.lblAyudaPrecioLista.TabIndex = 221
        Me.lblAyudaPrecioLista.Text = "Valores Positivos indican Aumento por ejemplo 10%  Valores Negativos indican Desc" & _
            "uentos, por ejemplo -10%"
        Me.lblAyudaPrecioLista.Visible = False
        '
        'rdPrecioTabl
        '
        Me.rdPrecioTabl.AutoSize = True
        Me.rdPrecioTabl.Location = New System.Drawing.Point(1050, 90)
        Me.rdPrecioTabl.Name = "rdPrecioTabl"
        Me.rdPrecioTabl.Size = New System.Drawing.Size(96, 17)
        Me.rdPrecioTabl.TabIndex = 183
        Me.rdPrecioTabl.TabStop = True
        Me.rdPrecioTabl.Text = "Lista de Precios TABLERISTA"
        Me.rdPrecioTabl.UseVisualStyleBackColor = True
        Me.rdPrecioTabl.Checked = True
        '
        'rdPrecioDist
        '
        Me.rdPrecioDist.AutoSize = True
        Me.rdPrecioDist.Location = New System.Drawing.Point(1050, 120)
        Me.rdPrecioDist.Name = "rdPrecioDist"
        Me.rdPrecioDist.Size = New System.Drawing.Size(87, 17)
        Me.rdPrecioDist.TabIndex = 182
        Me.rdPrecioDist.TabStop = True
        Me.rdPrecioDist.Text = "Lista de Precios DISTRIBUIDOR"
        Me.rdPrecioDist.UseVisualStyleBackColor = True
        '
        'frmMaterialesPrecios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1346, 640)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMaterialesPrecios"
        Me.Text = "Lista de Materiales"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicAplicarAumento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicOrden, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAumento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbUnidadCompra As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbMonedasCompra As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmbFAMILIAS As System.Windows.Forms.ComboBox
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbSubRubro As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkRubro As System.Windows.Forms.CheckBox
    Friend WithEvents chkNombre As System.Windows.Forms.CheckBox
    Friend WithEvents chkCodigo As System.Windows.Forms.CheckBox
    Friend WithEvents chkSubRubro As System.Windows.Forms.CheckBox
    Friend WithEvents txtAumento As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnAplicarAumento As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancelarAumento As System.Windows.Forms.Button
    Friend WithEvents chkModificarOrden As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbNombre As System.Windows.Forms.ComboBox
    Friend WithEvents btnEliminarCriterio As System.Windows.Forms.Button
    Friend WithEvents btnGuardarCriterio As System.Windows.Forms.Button
    Friend WithEvents chkAumento As System.Windows.Forms.CheckBox
    Friend WithEvents chkBonif1 As System.Windows.Forms.CheckBox
    Friend WithEvents txtGanancia As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtBonif2 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtBonif3 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtBonif4 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtBonif5 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtBonif1 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkBonif2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBonif3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBonif4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBonif5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkGanancia As System.Windows.Forms.CheckBox
    Friend WithEvents lblMsjProv As System.Windows.Forms.Label
    Friend WithEvents lblMsjGanancia As System.Windows.Forms.Label
    Friend WithEvents lblMsjBonif5 As System.Windows.Forms.Label
    Friend WithEvents lblMsjBonif4 As System.Windows.Forms.Label
    Friend WithEvents lblMsjBonif3 As System.Windows.Forms.Label
    Friend WithEvents lblMsjBonif2 As System.Windows.Forms.Label
    Friend WithEvents lblMsjBonif1 As System.Windows.Forms.Label
    Friend WithEvents chkPrecioLista As System.Windows.Forms.CheckBox
    Friend WithEvents chkProveedor As System.Windows.Forms.CheckBox
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents picAumento As System.Windows.Forms.PictureBox
    Friend WithEvents PicOrden As System.Windows.Forms.PictureBox
    Friend WithEvents PicAplicarAumento As System.Windows.Forms.PictureBox
    Friend WithEvents lblAyudaBonif As System.Windows.Forms.Label
    Friend WithEvents chkOcultarGanancia As System.Windows.Forms.CheckBox
    Friend WithEvents lblAyudaPrecioLista As System.Windows.Forms.Label
    Friend WithEvents rdPrecioDist As System.Windows.Forms.RadioButton
    Friend WithEvents rdPrecioTabl As System.Windows.Forms.RadioButton
End Class
