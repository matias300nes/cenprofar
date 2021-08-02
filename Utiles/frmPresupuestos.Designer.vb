<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPresupuestos

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPresupuestos))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCopiarPres = New System.Windows.Forms.Button()
        Me.chkAmpliarGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.lblTrafo_CantHoras = New System.Windows.Forms.Label()
        Me.txtTrafo_CantHoras = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTrafo_SubtotalEnsayos = New System.Windows.Forms.Label()
        Me.txtTrafo_SubtotalEnsayos = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTrafo_Observaciones = New System.Windows.Forms.Label()
        Me.lblTrafo_Cabecera = New System.Windows.Forms.Label()
        Me.txtTrafo_Observaciones = New System.Windows.Forms.TextBox()
        Me.txtTrafo_Cabecera = New System.Windows.Forms.TextBox()
        Me.grdTrafos_Det = New System.Windows.Forms.DataGridView()
        Me.grdTrafos_Ensayos = New System.Windows.Forms.DataGridView()
        Me.chkPresupuestosCumplidos = New System.Windows.Forms.CheckBox()
        Me.cmbEstado = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkMostrarCodigoMaterial = New System.Windows.Forms.CheckBox()
        Me.chkSubtotalOferta = New System.Windows.Forms.CheckBox()
        Me.btnAsignarTablero = New System.Windows.Forms.Button()
        Me.lstNotas = New System.Windows.Forms.ListView()
        Me.Nota = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmbAjustes = New System.Windows.Forms.ComboBox()
        Me.txtSubtotalOferta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.rdBaja = New System.Windows.Forms.RadioButton()
        Me.rdMedia = New System.Windows.Forms.RadioButton()
        Me.rdTrafo = New System.Windows.Forms.RadioButton()
        Me.rdTableros = New System.Windows.Forms.RadioButton()
        Me.rdMateriales = New System.Windows.Forms.RadioButton()
        Me.txtMontoIvaOferta = New System.Windows.Forms.Label()
        Me.txtTotalOferta = New System.Windows.Forms.Label()
        Me.lblIvaOferta = New System.Windows.Forms.Label()
        Me.txtIvaOferta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkPlazoEntrega = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkAjustes = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cmbCertificaciones = New System.Windows.Forms.ComboBox()
        Me.chkCertificaciones = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cmbPlazoEntregaProvision = New System.Windows.Forms.ComboBox()
        Me.chkPlazoEntregaProvision = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cmbPlazoEntrega = New System.Windows.Forms.ComboBox()
        Me.lblTotalOferta = New System.Windows.Forms.Label()
        Me.lblMontoIvaOferta = New System.Windows.Forms.Label()
        Me.grdOfertaComercial = New System.Windows.Forms.DataGridView()
        Me.chkAgregarOfertaComercial = New System.Windows.Forms.CheckBox()
        Me.chkOC = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTotalDO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTotalDO = New System.Windows.Forms.Label()
        Me.txtTotalPE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTotalPE = New System.Windows.Forms.Label()
        Me.txtIva21DO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblIVA21DO = New System.Windows.Forms.Label()
        Me.txtIva21PE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblIVA21PE = New System.Windows.Forms.Label()
        Me.txtIva10DO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblIVA10DO = New System.Windows.Forms.Label()
        Me.txtSubtotalDO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblSubtotalDO = New System.Windows.Forms.Label()
        Me.chkAmpliarNotas = New System.Windows.Forms.CheckBox()
        Me.txtIdFormaPago = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkOCA = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkPrecioDistribuidor = New System.Windows.Forms.CheckBox()
        Me.lblListaPrecio = New System.Windows.Forms.Label()
        Me.txtCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdComprador = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdUsuario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.BtnFactura = New System.Windows.Forms.Button()
        Me.BtnRemito = New System.Windows.Forms.Button()
        Me.chkBuscarClientes = New System.Windows.Forms.CheckBox()
        Me.cmbCliente2 = New System.Windows.Forms.ComboBox()
        Me.txtAnticipo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkRecDescGlobal = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtNroOC = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtNombre = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNotaGestion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbVendedor = New System.Windows.Forms.ComboBox()
        Me.txtporcrecargo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtRevision = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtReq = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbAutoriza = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtValidez = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbFormaPago = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtIva10PE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtSubtotalPE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbEntregaren = New System.Windows.Forms.ComboBox()
        Me.cmbUsuario = New System.Windows.Forms.ComboBox()
        Me.cmbComprador = New System.Windows.Forms.ComboBox()
        Me.PicNotas = New System.Windows.Forms.PictureBox()
        Me.chkNotas = New System.Windows.Forms.CheckBox()
        Me.lblSubtotalPE = New System.Windows.Forms.Label()
        Me.lblIVA10PE = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.PicClientes = New System.Windows.Forms.PictureBox()
        Me.chkUsuario = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkEntrega = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.chkComprador = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtID_OfertaComercial = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkOcultarGanancia = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.picGanancia = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkAnticipo = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PicFormaPago = New System.Windows.Forms.PictureBox()
        Me.PicEmpleados = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbUnidadVenta = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItem_OfertaComercial_ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdTrafos_Det, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTrafos_Ensayos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOfertaComercial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGanancia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicFormaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicEmpleados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnCopiarPres)
        Me.GroupBox1.Controls.Add(Me.chkAmpliarGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.lblTrafo_CantHoras)
        Me.GroupBox1.Controls.Add(Me.txtTrafo_CantHoras)
        Me.GroupBox1.Controls.Add(Me.lblTrafo_SubtotalEnsayos)
        Me.GroupBox1.Controls.Add(Me.txtTrafo_SubtotalEnsayos)
        Me.GroupBox1.Controls.Add(Me.lblTrafo_Observaciones)
        Me.GroupBox1.Controls.Add(Me.lblTrafo_Cabecera)
        Me.GroupBox1.Controls.Add(Me.txtTrafo_Observaciones)
        Me.GroupBox1.Controls.Add(Me.txtTrafo_Cabecera)
        Me.GroupBox1.Controls.Add(Me.grdTrafos_Det)
        Me.GroupBox1.Controls.Add(Me.grdTrafos_Ensayos)
        Me.GroupBox1.Controls.Add(Me.chkPresupuestosCumplidos)
        Me.GroupBox1.Controls.Add(Me.cmbEstado)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkMostrarCodigoMaterial)
        Me.GroupBox1.Controls.Add(Me.chkSubtotalOferta)
        Me.GroupBox1.Controls.Add(Me.btnAsignarTablero)
        Me.GroupBox1.Controls.Add(Me.lstNotas)
        Me.GroupBox1.Controls.Add(Me.cmbAjustes)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalOferta)
        Me.GroupBox1.Controls.Add(Me.rdBaja)
        Me.GroupBox1.Controls.Add(Me.rdMedia)
        Me.GroupBox1.Controls.Add(Me.rdTrafo)
        Me.GroupBox1.Controls.Add(Me.rdTableros)
        Me.GroupBox1.Controls.Add(Me.rdMateriales)
        Me.GroupBox1.Controls.Add(Me.txtMontoIvaOferta)
        Me.GroupBox1.Controls.Add(Me.txtTotalOferta)
        Me.GroupBox1.Controls.Add(Me.lblIvaOferta)
        Me.GroupBox1.Controls.Add(Me.txtIvaOferta)
        Me.GroupBox1.Controls.Add(Me.chkPlazoEntrega)
        Me.GroupBox1.Controls.Add(Me.chkAjustes)
        Me.GroupBox1.Controls.Add(Me.cmbCertificaciones)
        Me.GroupBox1.Controls.Add(Me.chkCertificaciones)
        Me.GroupBox1.Controls.Add(Me.cmbPlazoEntregaProvision)
        Me.GroupBox1.Controls.Add(Me.chkPlazoEntregaProvision)
        Me.GroupBox1.Controls.Add(Me.cmbPlazoEntrega)
        Me.GroupBox1.Controls.Add(Me.lblTotalOferta)
        Me.GroupBox1.Controls.Add(Me.lblMontoIvaOferta)
        Me.GroupBox1.Controls.Add(Me.grdOfertaComercial)
        Me.GroupBox1.Controls.Add(Me.chkAgregarOfertaComercial)
        Me.GroupBox1.Controls.Add(Me.chkOC)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtTotalDO)
        Me.GroupBox1.Controls.Add(Me.lblTotalDO)
        Me.GroupBox1.Controls.Add(Me.txtTotalPE)
        Me.GroupBox1.Controls.Add(Me.lblTotalPE)
        Me.GroupBox1.Controls.Add(Me.txtIva21DO)
        Me.GroupBox1.Controls.Add(Me.lblIVA21DO)
        Me.GroupBox1.Controls.Add(Me.txtIva21PE)
        Me.GroupBox1.Controls.Add(Me.lblIVA21PE)
        Me.GroupBox1.Controls.Add(Me.txtIva10DO)
        Me.GroupBox1.Controls.Add(Me.lblIVA10DO)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalDO)
        Me.GroupBox1.Controls.Add(Me.lblSubtotalDO)
        Me.GroupBox1.Controls.Add(Me.chkAmpliarNotas)
        Me.GroupBox1.Controls.Add(Me.txtIdFormaPago)
        Me.GroupBox1.Controls.Add(Me.chkOCA)
        Me.GroupBox1.Controls.Add(Me.chkPrecioDistribuidor)
        Me.GroupBox1.Controls.Add(Me.lblListaPrecio)
        Me.GroupBox1.Controls.Add(Me.txtCliente)
        Me.GroupBox1.Controls.Add(Me.txtIdComprador)
        Me.GroupBox1.Controls.Add(Me.txtIdUsuario)
        Me.GroupBox1.Controls.Add(Me.lblEstado)
        Me.GroupBox1.Controls.Add(Me.BtnFactura)
        Me.GroupBox1.Controls.Add(Me.BtnRemito)
        Me.GroupBox1.Controls.Add(Me.chkBuscarClientes)
        Me.GroupBox1.Controls.Add(Me.cmbCliente2)
        Me.GroupBox1.Controls.Add(Me.txtAnticipo)
        Me.GroupBox1.Controls.Add(Me.chkRecDescGlobal)
        Me.GroupBox1.Controls.Add(Me.txtNroOC)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtNombre)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtNotaGestion)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cmbVendedor)
        Me.GroupBox1.Controls.Add(Me.txtporcrecargo)
        Me.GroupBox1.Controls.Add(Me.txtRevision)
        Me.GroupBox1.Controls.Add(Me.txtReq)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.cmbAutoriza)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtValidez)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cmbFormaPago)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtIva10PE)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalPE)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbEntregaren)
        Me.GroupBox1.Controls.Add(Me.cmbUsuario)
        Me.GroupBox1.Controls.Add(Me.cmbComprador)
        Me.GroupBox1.Controls.Add(Me.PicNotas)
        Me.GroupBox1.Controls.Add(Me.chkNotas)
        Me.GroupBox1.Controls.Add(Me.lblSubtotalPE)
        Me.GroupBox1.Controls.Add(Me.lblIVA10PE)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.PicClientes)
        Me.GroupBox1.Controls.Add(Me.chkUsuario)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.chkEntrega)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.chkComprador)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtID_OfertaComercial)
        Me.GroupBox1.Controls.Add(Me.chkOcultarGanancia)
        Me.GroupBox1.Controls.Add(Me.picGanancia)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkAnticipo)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.PicFormaPago)
        Me.GroupBox1.Controls.Add(Me.PicEmpleados)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(7, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1345, 540)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnCopiarPres
        '
        Me.btnCopiarPres.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopiarPres.Location = New System.Drawing.Point(1225, 74)
        Me.btnCopiarPres.Name = "btnCopiarPres"
        Me.btnCopiarPres.Size = New System.Drawing.Size(104, 23)
        Me.btnCopiarPres.TabIndex = 266
        Me.btnCopiarPres.Text = "Copiar Pres."
        Me.btnCopiarPres.UseVisualStyleBackColor = True
        '
        'chkAmpliarGrillaInferior
        '
        Me.chkAmpliarGrillaInferior.AutoSize = True
        Me.chkAmpliarGrillaInferior.Location = New System.Drawing.Point(13, 519)
        Me.chkAmpliarGrillaInferior.Name = "chkAmpliarGrillaInferior"
        Me.chkAmpliarGrillaInferior.Size = New System.Drawing.Size(121, 17)
        Me.chkAmpliarGrillaInferior.TabIndex = 186
        Me.chkAmpliarGrillaInferior.Text = "Ampliar Grilla Inferior"
        Me.chkAmpliarGrillaInferior.UseVisualStyleBackColor = True
        '
        'chkAnulados
        '
        Me.chkAnulados.AccessibleName = "Eliminado"
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnulados.ForeColor = System.Drawing.Color.Red
        Me.chkAnulados.Location = New System.Drawing.Point(150, 519)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(166, 17)
        Me.chkAnulados.TabIndex = 28
        Me.chkAnulados.Text = "Presupuestos Eliminados"
        Me.chkAnulados.UseVisualStyleBackColor = True
        '
        'lblTrafo_CantHoras
        '
        Me.lblTrafo_CantHoras.AutoSize = True
        Me.lblTrafo_CantHoras.Location = New System.Drawing.Point(1112, 437)
        Me.lblTrafo_CantHoras.Name = "lblTrafo_CantHoras"
        Me.lblTrafo_CantHoras.Size = New System.Drawing.Size(60, 13)
        Me.lblTrafo_CantHoras.TabIndex = 265
        Me.lblTrafo_CantHoras.Text = "Cant Horas"
        Me.lblTrafo_CantHoras.Visible = False
        '
        'txtTrafo_CantHoras
        '
        Me.txtTrafo_CantHoras.AccessibleName = ""
        Me.txtTrafo_CantHoras.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrafo_CantHoras.Decimals = CType(0, Byte)
        Me.txtTrafo_CantHoras.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTrafo_CantHoras.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtTrafo_CantHoras.Location = New System.Drawing.Point(1115, 453)
        Me.txtTrafo_CantHoras.MaxLength = 50
        Me.txtTrafo_CantHoras.Name = "txtTrafo_CantHoras"
        Me.txtTrafo_CantHoras.Size = New System.Drawing.Size(70, 20)
        Me.txtTrafo_CantHoras.TabIndex = 264
        Me.txtTrafo_CantHoras.Text_1 = Nothing
        Me.txtTrafo_CantHoras.Text_2 = Nothing
        Me.txtTrafo_CantHoras.Text_3 = Nothing
        Me.txtTrafo_CantHoras.Text_4 = Nothing
        Me.txtTrafo_CantHoras.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTrafo_CantHoras.UserValues = Nothing
        Me.txtTrafo_CantHoras.Visible = False
        '
        'lblTrafo_SubtotalEnsayos
        '
        Me.lblTrafo_SubtotalEnsayos.AutoSize = True
        Me.lblTrafo_SubtotalEnsayos.Location = New System.Drawing.Point(1112, 388)
        Me.lblTrafo_SubtotalEnsayos.Name = "lblTrafo_SubtotalEnsayos"
        Me.lblTrafo_SubtotalEnsayos.Size = New System.Drawing.Size(46, 13)
        Me.lblTrafo_SubtotalEnsayos.TabIndex = 263
        Me.lblTrafo_SubtotalEnsayos.Text = "Subtotal"
        Me.lblTrafo_SubtotalEnsayos.Visible = False
        '
        'txtTrafo_SubtotalEnsayos
        '
        Me.txtTrafo_SubtotalEnsayos.AccessibleName = ""
        Me.txtTrafo_SubtotalEnsayos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrafo_SubtotalEnsayos.Decimals = CType(2, Byte)
        Me.txtTrafo_SubtotalEnsayos.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTrafo_SubtotalEnsayos.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTrafo_SubtotalEnsayos.Location = New System.Drawing.Point(1115, 404)
        Me.txtTrafo_SubtotalEnsayos.MaxLength = 50
        Me.txtTrafo_SubtotalEnsayos.Name = "txtTrafo_SubtotalEnsayos"
        Me.txtTrafo_SubtotalEnsayos.ReadOnly = True
        Me.txtTrafo_SubtotalEnsayos.Size = New System.Drawing.Size(70, 20)
        Me.txtTrafo_SubtotalEnsayos.TabIndex = 262
        Me.txtTrafo_SubtotalEnsayos.Text_1 = Nothing
        Me.txtTrafo_SubtotalEnsayos.Text_2 = Nothing
        Me.txtTrafo_SubtotalEnsayos.Text_3 = Nothing
        Me.txtTrafo_SubtotalEnsayos.Text_4 = Nothing
        Me.txtTrafo_SubtotalEnsayos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTrafo_SubtotalEnsayos.UserValues = Nothing
        Me.txtTrafo_SubtotalEnsayos.Visible = False
        '
        'lblTrafo_Observaciones
        '
        Me.lblTrafo_Observaciones.AutoSize = True
        Me.lblTrafo_Observaciones.Location = New System.Drawing.Point(10, 438)
        Me.lblTrafo_Observaciones.Name = "lblTrafo_Observaciones"
        Me.lblTrafo_Observaciones.Size = New System.Drawing.Size(78, 13)
        Me.lblTrafo_Observaciones.TabIndex = 260
        Me.lblTrafo_Observaciones.Text = "Observaciones"
        Me.lblTrafo_Observaciones.Visible = False
        '
        'lblTrafo_Cabecera
        '
        Me.lblTrafo_Cabecera.AutoSize = True
        Me.lblTrafo_Cabecera.Location = New System.Drawing.Point(11, 389)
        Me.lblTrafo_Cabecera.Name = "lblTrafo_Cabecera"
        Me.lblTrafo_Cabecera.Size = New System.Drawing.Size(97, 13)
        Me.lblTrafo_Cabecera.TabIndex = 259
        Me.lblTrafo_Cabecera.Text = "Texto de cabecera"
        Me.lblTrafo_Cabecera.Visible = False
        '
        'txtTrafo_Observaciones
        '
        Me.txtTrafo_Observaciones.Location = New System.Drawing.Point(13, 453)
        Me.txtTrafo_Observaciones.Multiline = True
        Me.txtTrafo_Observaciones.Name = "txtTrafo_Observaciones"
        Me.txtTrafo_Observaciones.Size = New System.Drawing.Size(1082, 30)
        Me.txtTrafo_Observaciones.TabIndex = 258
        Me.txtTrafo_Observaciones.Visible = False
        '
        'txtTrafo_Cabecera
        '
        Me.txtTrafo_Cabecera.Location = New System.Drawing.Point(13, 404)
        Me.txtTrafo_Cabecera.Multiline = True
        Me.txtTrafo_Cabecera.Name = "txtTrafo_Cabecera"
        Me.txtTrafo_Cabecera.Size = New System.Drawing.Size(1082, 30)
        Me.txtTrafo_Cabecera.TabIndex = 257
        Me.txtTrafo_Cabecera.Visible = False
        '
        'grdTrafos_Det
        '
        Me.grdTrafos_Det.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdTrafos_Det.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTrafos_Det.Location = New System.Drawing.Point(13, 100)
        Me.grdTrafos_Det.Name = "grdTrafos_Det"
        Me.grdTrafos_Det.Size = New System.Drawing.Size(398, 286)
        Me.grdTrafos_Det.TabIndex = 255
        Me.grdTrafos_Det.Visible = False
        '
        'grdTrafos_Ensayos
        '
        Me.grdTrafos_Ensayos.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdTrafos_Ensayos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTrafos_Ensayos.Location = New System.Drawing.Point(411, 100)
        Me.grdTrafos_Ensayos.Name = "grdTrafos_Ensayos"
        Me.grdTrafos_Ensayos.Size = New System.Drawing.Size(798, 286)
        Me.grdTrafos_Ensayos.TabIndex = 254
        Me.grdTrafos_Ensayos.Visible = False
        '
        'chkPresupuestosCumplidos
        '
        Me.chkPresupuestosCumplidos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPresupuestosCumplidos.AutoSize = True
        Me.chkPresupuestosCumplidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPresupuestosCumplidos.Location = New System.Drawing.Point(322, 519)
        Me.chkPresupuestosCumplidos.Name = "chkPresupuestosCumplidos"
        Me.chkPresupuestosCumplidos.Size = New System.Drawing.Size(167, 17)
        Me.chkPresupuestosCumplidos.TabIndex = 253
        Me.chkPresupuestosCumplidos.Text = " Presupuestos Cumplidos"
        Me.chkPresupuestosCumplidos.UseVisualStyleBackColor = True
        '
        'cmbEstado
        '
        Me.cmbEstado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbEstado.FormattingEnabled = True
        Me.cmbEstado.Location = New System.Drawing.Point(1221, 31)
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Size = New System.Drawing.Size(108, 21)
        Me.cmbEstado.TabIndex = 252
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1218, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 251
        Me.Label1.Text = "Estado Presup."
        '
        'chkMostrarCodigoMaterial
        '
        Me.chkMostrarCodigoMaterial.AccessibleName = "Eliminado"
        Me.chkMostrarCodigoMaterial.AutoSize = True
        Me.chkMostrarCodigoMaterial.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostrarCodigoMaterial.ForeColor = System.Drawing.Color.Blue
        Me.chkMostrarCodigoMaterial.Location = New System.Drawing.Point(819, 511)
        Me.chkMostrarCodigoMaterial.Name = "chkMostrarCodigoMaterial"
        Me.chkMostrarCodigoMaterial.Size = New System.Drawing.Size(252, 17)
        Me.chkMostrarCodigoMaterial.TabIndex = 249
        Me.chkMostrarCodigoMaterial.Text = "Mostrar Código de Material en la Oferta Técnica"
        Me.chkMostrarCodigoMaterial.UseVisualStyleBackColor = True
        '
        'chkSubtotalOferta
        '
        Me.chkSubtotalOferta.AccessibleName = "Eliminado"
        Me.chkSubtotalOferta.AutoSize = True
        Me.chkSubtotalOferta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSubtotalOferta.ForeColor = System.Drawing.Color.Blue
        Me.chkSubtotalOferta.Location = New System.Drawing.Point(466, 493)
        Me.chkSubtotalOferta.Name = "chkSubtotalOferta"
        Me.chkSubtotalOferta.Size = New System.Drawing.Size(92, 17)
        Me.chkSubtotalOferta.TabIndex = 248
        Me.chkSubtotalOferta.Text = "Subtotal ($)"
        Me.chkSubtotalOferta.UseVisualStyleBackColor = True
        Me.chkSubtotalOferta.Visible = False
        '
        'btnAsignarTablero
        '
        Me.btnAsignarTablero.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAsignarTablero.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAsignarTablero.ForeColor = System.Drawing.Color.Red
        Me.btnAsignarTablero.Location = New System.Drawing.Point(1225, 129)
        Me.btnAsignarTablero.Name = "btnAsignarTablero"
        Me.btnAsignarTablero.Size = New System.Drawing.Size(104, 23)
        Me.btnAsignarTablero.TabIndex = 247
        Me.btnAsignarTablero.Text = "Tableros (F8)"
        Me.btnAsignarTablero.UseVisualStyleBackColor = True
        '
        'lstNotas
        '
        Me.lstNotas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstNotas.CheckBoxes = True
        Me.lstNotas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nota})
        Me.lstNotas.Enabled = False
        Me.lstNotas.Location = New System.Drawing.Point(1215, 180)
        Me.lstNotas.Name = "lstNotas"
        Me.lstNotas.Size = New System.Drawing.Size(124, 307)
        Me.lstNotas.TabIndex = 30
        Me.lstNotas.UseCompatibleStateImageBehavior = False
        Me.lstNotas.View = System.Windows.Forms.View.Details
        '
        'Nota
        '
        Me.Nota.Text = "Nota"
        Me.Nota.Width = 248
        '
        'cmbAjustes
        '
        Me.cmbAjustes.AccessibleName = ""
        Me.cmbAjustes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAjustes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAjustes.DropDownHeight = 300
        Me.cmbAjustes.Enabled = False
        Me.cmbAjustes.FormattingEnabled = True
        Me.cmbAjustes.IntegralHeight = False
        Me.cmbAjustes.Location = New System.Drawing.Point(819, 484)
        Me.cmbAjustes.Name = "cmbAjustes"
        Me.cmbAjustes.Size = New System.Drawing.Size(343, 21)
        Me.cmbAjustes.TabIndex = 234
        Me.cmbAjustes.Visible = False
        '
        'txtSubtotalOferta
        '
        Me.txtSubtotalOferta.AccessibleName = ""
        Me.txtSubtotalOferta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotalOferta.Decimals = CType(2, Byte)
        Me.txtSubtotalOferta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalOferta.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtSubtotalOferta.Location = New System.Drawing.Point(559, 491)
        Me.txtSubtotalOferta.MaxLength = 50
        Me.txtSubtotalOferta.Name = "txtSubtotalOferta"
        Me.txtSubtotalOferta.Size = New System.Drawing.Size(70, 20)
        Me.txtSubtotalOferta.TabIndex = 246
        Me.txtSubtotalOferta.Text = "0"
        Me.txtSubtotalOferta.Text_1 = Nothing
        Me.txtSubtotalOferta.Text_2 = Nothing
        Me.txtSubtotalOferta.Text_3 = Nothing
        Me.txtSubtotalOferta.Text_4 = Nothing
        Me.txtSubtotalOferta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotalOferta.UserValues = Nothing
        Me.txtSubtotalOferta.Visible = False
        '
        'rdBaja
        '
        Me.rdBaja.AutoSize = True
        Me.rdBaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdBaja.Location = New System.Drawing.Point(453, 186)
        Me.rdBaja.Name = "rdBaja"
        Me.rdBaja.Size = New System.Drawing.Size(99, 17)
        Me.rdBaja.TabIndex = 245
        Me.rdBaja.TabStop = True
        Me.rdBaja.Text = "Baja Tensión"
        Me.rdBaja.UseVisualStyleBackColor = True
        Me.rdBaja.Visible = False
        '
        'rdMedia
        '
        Me.rdMedia.AutoSize = True
        Me.rdMedia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdMedia.ForeColor = System.Drawing.Color.Blue
        Me.rdMedia.Location = New System.Drawing.Point(339, 186)
        Me.rdMedia.Name = "rdMedia"
        Me.rdMedia.Size = New System.Drawing.Size(108, 17)
        Me.rdMedia.TabIndex = 244
        Me.rdMedia.TabStop = True
        Me.rdMedia.Text = "Media Tensión"
        Me.rdMedia.UseVisualStyleBackColor = True
        Me.rdMedia.Visible = False
        '
        'rdTrafo
        '
        Me.rdTrafo.AutoSize = True
        Me.rdTrafo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTrafo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rdTrafo.Location = New System.Drawing.Point(50, 50)
        Me.rdTrafo.Name = "rdTrafo"
        Me.rdTrafo.Size = New System.Drawing.Size(55, 17)
        Me.rdTrafo.TabIndex = 243
        Me.rdTrafo.TabStop = True
        Me.rdTrafo.Text = "Trafo"
        Me.rdTrafo.UseVisualStyleBackColor = True
        Me.rdTrafo.Visible = False
        '
        'rdTableros
        '
        Me.rdTableros.AutoSize = True
        Me.rdTableros.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTableros.ForeColor = System.Drawing.Color.Green
        Me.rdTableros.Location = New System.Drawing.Point(198, 186)
        Me.rdTableros.Name = "rdTableros"
        Me.rdTableros.Size = New System.Drawing.Size(74, 17)
        Me.rdTableros.TabIndex = 242
        Me.rdTableros.TabStop = True
        Me.rdTableros.Text = "Tableros"
        Me.rdTableros.UseVisualStyleBackColor = True
        Me.rdTableros.Visible = False
        '
        'rdMateriales
        '
        Me.rdMateriales.AutoSize = True
        Me.rdMateriales.Checked = True
        Me.rdMateriales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdMateriales.ForeColor = System.Drawing.Color.Red
        Me.rdMateriales.Location = New System.Drawing.Point(109, 186)
        Me.rdMateriales.Name = "rdMateriales"
        Me.rdMateriales.Size = New System.Drawing.Size(83, 17)
        Me.rdMateriales.TabIndex = 241
        Me.rdMateriales.TabStop = True
        Me.rdMateriales.Text = "Materiales"
        Me.rdMateriales.UseVisualStyleBackColor = True
        Me.rdMateriales.Visible = False
        '
        'txtMontoIvaOferta
        '
        Me.txtMontoIvaOferta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtMontoIvaOferta.Location = New System.Drawing.Point(694, 514)
        Me.txtMontoIvaOferta.Name = "txtMontoIvaOferta"
        Me.txtMontoIvaOferta.Size = New System.Drawing.Size(70, 20)
        Me.txtMontoIvaOferta.TabIndex = 240
        Me.txtMontoIvaOferta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.txtMontoIvaOferta.Visible = False
        '
        'txtTotalOferta
        '
        Me.txtTotalOferta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtTotalOferta.Location = New System.Drawing.Point(559, 514)
        Me.txtTotalOferta.Name = "txtTotalOferta"
        Me.txtTotalOferta.Size = New System.Drawing.Size(70, 20)
        Me.txtTotalOferta.TabIndex = 239
        Me.txtTotalOferta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.txtTotalOferta.Visible = False
        '
        'lblIvaOferta
        '
        Me.lblIvaOferta.AutoSize = True
        Me.lblIvaOferta.Location = New System.Drawing.Point(664, 494)
        Me.lblIvaOferta.Name = "lblIvaOferta"
        Me.lblIvaOferta.Size = New System.Drawing.Size(24, 13)
        Me.lblIvaOferta.TabIndex = 237
        Me.lblIvaOferta.Text = "IVA"
        Me.lblIvaOferta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblIvaOferta.Visible = False
        '
        'txtIvaOferta
        '
        Me.txtIvaOferta.AccessibleName = ""
        Me.txtIvaOferta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIvaOferta.Decimals = CType(2, Byte)
        Me.txtIvaOferta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIvaOferta.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIvaOferta.Location = New System.Drawing.Point(694, 491)
        Me.txtIvaOferta.MaxLength = 50
        Me.txtIvaOferta.Name = "txtIvaOferta"
        Me.txtIvaOferta.Size = New System.Drawing.Size(70, 20)
        Me.txtIvaOferta.TabIndex = 236
        Me.txtIvaOferta.Text_1 = Nothing
        Me.txtIvaOferta.Text_2 = Nothing
        Me.txtIvaOferta.Text_3 = Nothing
        Me.txtIvaOferta.Text_4 = Nothing
        Me.txtIvaOferta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIvaOferta.UserValues = Nothing
        Me.txtIvaOferta.Visible = False
        '
        'chkPlazoEntrega
        '
        Me.chkPlazoEntrega.AutoSize = True
        Me.chkPlazoEntrega.Location = New System.Drawing.Point(819, 327)
        Me.chkPlazoEntrega.Name = "chkPlazoEntrega"
        Me.chkPlazoEntrega.Size = New System.Drawing.Size(190, 15)
        Me.chkPlazoEntrega.TabIndex = 235
        Me.chkPlazoEntrega.Text = "Plazo Entrega (cumplimiento de...)"
        Me.chkPlazoEntrega.TextColor = System.Drawing.Color.Blue
        Me.chkPlazoEntrega.Visible = False
        '
        'chkAjustes
        '
        Me.chkAjustes.AutoSize = True
        Me.chkAjustes.Location = New System.Drawing.Point(819, 465)
        Me.chkAjustes.Name = "chkAjustes"
        Me.chkAjustes.Size = New System.Drawing.Size(58, 15)
        Me.chkAjustes.TabIndex = 233
        Me.chkAjustes.Text = "Ajustes"
        Me.chkAjustes.TextColor = System.Drawing.Color.Blue
        Me.chkAjustes.Visible = False
        '
        'cmbCertificaciones
        '
        Me.cmbCertificaciones.AccessibleName = ""
        Me.cmbCertificaciones.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCertificaciones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCertificaciones.DropDownHeight = 300
        Me.cmbCertificaciones.Enabled = False
        Me.cmbCertificaciones.FormattingEnabled = True
        Me.cmbCertificaciones.IntegralHeight = False
        Me.cmbCertificaciones.Location = New System.Drawing.Point(819, 438)
        Me.cmbCertificaciones.Name = "cmbCertificaciones"
        Me.cmbCertificaciones.Size = New System.Drawing.Size(343, 21)
        Me.cmbCertificaciones.TabIndex = 232
        Me.cmbCertificaciones.Visible = False
        '
        'chkCertificaciones
        '
        Me.chkCertificaciones.AutoSize = True
        Me.chkCertificaciones.Location = New System.Drawing.Point(819, 419)
        Me.chkCertificaciones.Name = "chkCertificaciones"
        Me.chkCertificaciones.Size = New System.Drawing.Size(94, 15)
        Me.chkCertificaciones.TabIndex = 231
        Me.chkCertificaciones.Text = "Certificaciones"
        Me.chkCertificaciones.TextColor = System.Drawing.Color.Blue
        Me.chkCertificaciones.Visible = False
        '
        'cmbPlazoEntregaProvision
        '
        Me.cmbPlazoEntregaProvision.AccessibleName = ""
        Me.cmbPlazoEntregaProvision.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbPlazoEntregaProvision.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPlazoEntregaProvision.DropDownHeight = 300
        Me.cmbPlazoEntregaProvision.Enabled = False
        Me.cmbPlazoEntregaProvision.FormattingEnabled = True
        Me.cmbPlazoEntregaProvision.IntegralHeight = False
        Me.cmbPlazoEntregaProvision.Location = New System.Drawing.Point(819, 392)
        Me.cmbPlazoEntregaProvision.Name = "cmbPlazoEntregaProvision"
        Me.cmbPlazoEntregaProvision.Size = New System.Drawing.Size(343, 21)
        Me.cmbPlazoEntregaProvision.TabIndex = 230
        Me.cmbPlazoEntregaProvision.Visible = False
        '
        'chkPlazoEntregaProvision
        '
        Me.chkPlazoEntregaProvision.AutoSize = True
        Me.chkPlazoEntregaProvision.Location = New System.Drawing.Point(819, 373)
        Me.chkPlazoEntregaProvision.Name = "chkPlazoEntregaProvision"
        Me.chkPlazoEntregaProvision.Size = New System.Drawing.Size(139, 15)
        Me.chkPlazoEntregaProvision.TabIndex = 229
        Me.chkPlazoEntregaProvision.Text = "Plazo Entrega Provisión"
        Me.chkPlazoEntregaProvision.TextColor = System.Drawing.Color.Blue
        Me.chkPlazoEntregaProvision.Visible = False
        '
        'cmbPlazoEntrega
        '
        Me.cmbPlazoEntrega.AccessibleName = ""
        Me.cmbPlazoEntrega.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbPlazoEntrega.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPlazoEntrega.DropDownHeight = 300
        Me.cmbPlazoEntrega.Enabled = False
        Me.cmbPlazoEntrega.FormattingEnabled = True
        Me.cmbPlazoEntrega.IntegralHeight = False
        Me.cmbPlazoEntrega.Location = New System.Drawing.Point(819, 346)
        Me.cmbPlazoEntrega.Name = "cmbPlazoEntrega"
        Me.cmbPlazoEntrega.Size = New System.Drawing.Size(343, 21)
        Me.cmbPlazoEntrega.TabIndex = 227
        Me.cmbPlazoEntrega.Visible = False
        '
        'lblTotalOferta
        '
        Me.lblTotalOferta.AutoSize = True
        Me.lblTotalOferta.Location = New System.Drawing.Point(510, 517)
        Me.lblTotalOferta.Name = "lblTotalOferta"
        Me.lblTotalOferta.Size = New System.Drawing.Size(46, 13)
        Me.lblTotalOferta.TabIndex = 224
        Me.lblTotalOferta.Text = "Total ($)"
        Me.lblTotalOferta.Visible = False
        '
        'lblMontoIvaOferta
        '
        Me.lblMontoIvaOferta.AutoSize = True
        Me.lblMontoIvaOferta.Location = New System.Drawing.Point(635, 517)
        Me.lblMontoIvaOferta.Name = "lblMontoIvaOferta"
        Me.lblMontoIvaOferta.Size = New System.Drawing.Size(57, 13)
        Me.lblMontoIvaOferta.TabIndex = 222
        Me.lblMontoIvaOferta.Text = "Monto IVA"
        Me.lblMontoIvaOferta.Visible = False
        '
        'grdOfertaComercial
        '
        Me.grdOfertaComercial.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdOfertaComercial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOfertaComercial.Location = New System.Drawing.Point(13, 327)
        Me.grdOfertaComercial.Name = "grdOfertaComercial"
        Me.grdOfertaComercial.Size = New System.Drawing.Size(800, 160)
        Me.grdOfertaComercial.TabIndex = 218
        Me.grdOfertaComercial.Visible = False
        '
        'chkAgregarOfertaComercial
        '
        Me.chkAgregarOfertaComercial.AutoSize = True
        Me.chkAgregarOfertaComercial.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgregarOfertaComercial.Location = New System.Drawing.Point(868, 74)
        Me.chkAgregarOfertaComercial.Name = "chkAgregarOfertaComercial"
        Me.chkAgregarOfertaComercial.Size = New System.Drawing.Size(120, 17)
        Me.chkAgregarOfertaComercial.TabIndex = 217
        Me.chkAgregarOfertaComercial.Text = "Oferta Comercial"
        Me.chkAgregarOfertaComercial.UseVisualStyleBackColor = True
        '
        'chkOC
        '
        Me.chkOC.AutoSize = True
        Me.chkOC.Enabled = False
        Me.chkOC.Location = New System.Drawing.Point(1253, 59)
        Me.chkOC.Name = "chkOC"
        Me.chkOC.Size = New System.Drawing.Size(38, 15)
        Me.chkOC.TabIndex = 216
        Me.chkOC.Text = "OC"
        Me.chkOC.TextColor = System.Drawing.Color.Blue
        Me.chkOC.Visible = False
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(708, 136)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(40, 20)
        Me.txtIdCliente.TabIndex = 213
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(1133, 15)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(50, 13)
        Me.Label25.TabIndex = 212
        Me.Label25.Text = "Nro Req."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(1030, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 13)
        Me.Label13.TabIndex = 211
        Me.Label13.Text = "Nro OC"
        '
        'txtTotalDO
        '
        Me.txtTotalDO.AccessibleName = ""
        Me.txtTotalDO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotalDO.Decimals = CType(2, Byte)
        Me.txtTotalDO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalDO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotalDO.Location = New System.Drawing.Point(957, 514)
        Me.txtTotalDO.MaxLength = 50
        Me.txtTotalDO.Name = "txtTotalDO"
        Me.txtTotalDO.ReadOnly = True
        Me.txtTotalDO.Size = New System.Drawing.Size(57, 20)
        Me.txtTotalDO.TabIndex = 210
        Me.txtTotalDO.Text = "0"
        Me.txtTotalDO.Text_1 = Nothing
        Me.txtTotalDO.Text_2 = Nothing
        Me.txtTotalDO.Text_3 = Nothing
        Me.txtTotalDO.Text_4 = Nothing
        Me.txtTotalDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalDO.UserValues = Nothing
        '
        'lblTotalDO
        '
        Me.lblTotalDO.AutoSize = True
        Me.lblTotalDO.Location = New System.Drawing.Point(893, 517)
        Me.lblTotalDO.Name = "lblTotalDO"
        Me.lblTotalDO.Size = New System.Drawing.Size(58, 13)
        Me.lblTotalDO.TabIndex = 209
        Me.lblTotalDO.Text = "Total (u$$)"
        '
        'txtTotalPE
        '
        Me.txtTotalPE.AccessibleName = ""
        Me.txtTotalPE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotalPE.Decimals = CType(2, Byte)
        Me.txtTotalPE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalPE.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotalPE.Location = New System.Drawing.Point(694, 514)
        Me.txtTotalPE.MaxLength = 50
        Me.txtTotalPE.Name = "txtTotalPE"
        Me.txtTotalPE.ReadOnly = True
        Me.txtTotalPE.Size = New System.Drawing.Size(57, 20)
        Me.txtTotalPE.TabIndex = 208
        Me.txtTotalPE.Text = "0"
        Me.txtTotalPE.Text_1 = Nothing
        Me.txtTotalPE.Text_2 = Nothing
        Me.txtTotalPE.Text_3 = Nothing
        Me.txtTotalPE.Text_4 = Nothing
        Me.txtTotalPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPE.UserValues = Nothing
        '
        'lblTotalPE
        '
        Me.lblTotalPE.AutoSize = True
        Me.lblTotalPE.Location = New System.Drawing.Point(642, 517)
        Me.lblTotalPE.Name = "lblTotalPE"
        Me.lblTotalPE.Size = New System.Drawing.Size(46, 13)
        Me.lblTotalPE.TabIndex = 207
        Me.lblTotalPE.Text = "Total ($)"
        '
        'txtIva21DO
        '
        Me.txtIva21DO.AccessibleName = ""
        Me.txtIva21DO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIva21DO.Decimals = CType(2, Byte)
        Me.txtIva21DO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIva21DO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIva21DO.Location = New System.Drawing.Point(1097, 491)
        Me.txtIva21DO.MaxLength = 50
        Me.txtIva21DO.Name = "txtIva21DO"
        Me.txtIva21DO.ReadOnly = True
        Me.txtIva21DO.Size = New System.Drawing.Size(57, 20)
        Me.txtIva21DO.TabIndex = 206
        Me.txtIva21DO.Text_1 = Nothing
        Me.txtIva21DO.Text_2 = Nothing
        Me.txtIva21DO.Text_3 = Nothing
        Me.txtIva21DO.Text_4 = Nothing
        Me.txtIva21DO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIva21DO.UserValues = Nothing
        '
        'lblIVA21DO
        '
        Me.lblIVA21DO.AutoSize = True
        Me.lblIVA21DO.Location = New System.Drawing.Point(1029, 494)
        Me.lblIVA21DO.Name = "lblIVA21DO"
        Me.lblIVA21DO.Size = New System.Drawing.Size(66, 13)
        Me.lblIVA21DO.TabIndex = 205
        Me.lblIVA21DO.Text = "IVA 21 (u$$)"
        '
        'txtIva21PE
        '
        Me.txtIva21PE.AccessibleName = ""
        Me.txtIva21PE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIva21PE.Decimals = CType(2, Byte)
        Me.txtIva21PE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIva21PE.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIva21PE.Location = New System.Drawing.Point(815, 491)
        Me.txtIva21PE.MaxLength = 50
        Me.txtIva21PE.Name = "txtIva21PE"
        Me.txtIva21PE.ReadOnly = True
        Me.txtIva21PE.Size = New System.Drawing.Size(57, 20)
        Me.txtIva21PE.TabIndex = 204
        Me.txtIva21PE.Text_1 = Nothing
        Me.txtIva21PE.Text_2 = Nothing
        Me.txtIva21PE.Text_3 = Nothing
        Me.txtIva21PE.Text_4 = Nothing
        Me.txtIva21PE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIva21PE.UserValues = Nothing
        '
        'lblIVA21PE
        '
        Me.lblIVA21PE.AutoSize = True
        Me.lblIVA21PE.Location = New System.Drawing.Point(760, 494)
        Me.lblIVA21PE.Name = "lblIVA21PE"
        Me.lblIVA21PE.Size = New System.Drawing.Size(54, 13)
        Me.lblIVA21PE.TabIndex = 203
        Me.lblIVA21PE.Text = "IVA 21 ($)"
        '
        'txtIva10DO
        '
        Me.txtIva10DO.AccessibleName = ""
        Me.txtIva10DO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIva10DO.Decimals = CType(2, Byte)
        Me.txtIva10DO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIva10DO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIva10DO.Location = New System.Drawing.Point(1097, 514)
        Me.txtIva10DO.MaxLength = 50
        Me.txtIva10DO.Name = "txtIva10DO"
        Me.txtIva10DO.ReadOnly = True
        Me.txtIva10DO.Size = New System.Drawing.Size(57, 20)
        Me.txtIva10DO.TabIndex = 202
        Me.txtIva10DO.Text_1 = Nothing
        Me.txtIva10DO.Text_2 = Nothing
        Me.txtIva10DO.Text_3 = Nothing
        Me.txtIva10DO.Text_4 = Nothing
        Me.txtIva10DO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIva10DO.UserValues = Nothing
        '
        'lblIVA10DO
        '
        Me.lblIVA10DO.AutoSize = True
        Me.lblIVA10DO.Location = New System.Drawing.Point(1020, 517)
        Me.lblIVA10DO.Name = "lblIVA10DO"
        Me.lblIVA10DO.Size = New System.Drawing.Size(75, 13)
        Me.lblIVA10DO.TabIndex = 201
        Me.lblIVA10DO.Text = "IVA 10.5 (u$$)"
        '
        'txtSubtotalDO
        '
        Me.txtSubtotalDO.AccessibleName = ""
        Me.txtSubtotalDO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotalDO.Decimals = CType(2, Byte)
        Me.txtSubtotalDO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalDO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtSubtotalDO.Location = New System.Drawing.Point(957, 491)
        Me.txtSubtotalDO.MaxLength = 50
        Me.txtSubtotalDO.Name = "txtSubtotalDO"
        Me.txtSubtotalDO.ReadOnly = True
        Me.txtSubtotalDO.Size = New System.Drawing.Size(57, 20)
        Me.txtSubtotalDO.TabIndex = 200
        Me.txtSubtotalDO.Text = "0"
        Me.txtSubtotalDO.Text_1 = Nothing
        Me.txtSubtotalDO.Text_2 = Nothing
        Me.txtSubtotalDO.Text_3 = Nothing
        Me.txtSubtotalDO.Text_4 = Nothing
        Me.txtSubtotalDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotalDO.UserValues = Nothing
        '
        'lblSubtotalDO
        '
        Me.lblSubtotalDO.AutoSize = True
        Me.lblSubtotalDO.Location = New System.Drawing.Point(878, 494)
        Me.lblSubtotalDO.Name = "lblSubtotalDO"
        Me.lblSubtotalDO.Size = New System.Drawing.Size(73, 13)
        Me.lblSubtotalDO.TabIndex = 199
        Me.lblSubtotalDO.Text = "Subtotal (u$$)"
        '
        'chkAmpliarNotas
        '
        Me.chkAmpliarNotas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAmpliarNotas.AutoSize = True
        Me.chkAmpliarNotas.Location = New System.Drawing.Point(1215, 495)
        Me.chkAmpliarNotas.Name = "chkAmpliarNotas"
        Me.chkAmpliarNotas.Size = New System.Drawing.Size(116, 17)
        Me.chkAmpliarNotas.TabIndex = 198
        Me.chkAmpliarNotas.Text = "Ampliar Lista Notas"
        Me.chkAmpliarNotas.UseVisualStyleBackColor = True
        '
        'txtIdFormaPago
        '
        Me.txtIdFormaPago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdFormaPago.Decimals = CType(2, Byte)
        Me.txtIdFormaPago.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdFormaPago.Enabled = False
        Me.txtIdFormaPago.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdFormaPago.Location = New System.Drawing.Point(708, 209)
        Me.txtIdFormaPago.MaxLength = 8
        Me.txtIdFormaPago.Name = "txtIdFormaPago"
        Me.txtIdFormaPago.Size = New System.Drawing.Size(40, 20)
        Me.txtIdFormaPago.TabIndex = 197
        Me.txtIdFormaPago.Text_1 = Nothing
        Me.txtIdFormaPago.Text_2 = Nothing
        Me.txtIdFormaPago.Text_3 = Nothing
        Me.txtIdFormaPago.Text_4 = Nothing
        Me.txtIdFormaPago.UserValues = Nothing
        Me.txtIdFormaPago.Visible = False
        '
        'chkOCA
        '
        Me.chkOCA.AutoSize = True
        Me.chkOCA.Enabled = False
        Me.chkOCA.Location = New System.Drawing.Point(1202, 59)
        Me.chkOCA.Name = "chkOCA"
        Me.chkOCA.Size = New System.Drawing.Size(45, 15)
        Me.chkOCA.TabIndex = 196
        Me.chkOCA.Text = "OCA"
        Me.chkOCA.TextColor = System.Drawing.Color.Blue
        Me.chkOCA.Visible = False
        '
        'chkPrecioDistribuidor
        '
        Me.chkPrecioDistribuidor.AutoSize = True
        Me.chkPrecioDistribuidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrecioDistribuidor.Location = New System.Drawing.Point(43, 223)
        Me.chkPrecioDistribuidor.Name = "chkPrecioDistribuidor"
        Me.chkPrecioDistribuidor.Size = New System.Drawing.Size(160, 17)
        Me.chkPrecioDistribuidor.TabIndex = 208
        Me.chkPrecioDistribuidor.Text = "Usar Precio Distribuidor"
        Me.chkPrecioDistribuidor.UseVisualStyleBackColor = True
        Me.chkPrecioDistribuidor.Visible = False
        '
        'lblListaPrecio
        '
        Me.lblListaPrecio.AutoSize = True
        Me.lblListaPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListaPrecio.ForeColor = System.Drawing.Color.Red
        Me.lblListaPrecio.Location = New System.Drawing.Point(150, 495)
        Me.lblListaPrecio.Name = "lblListaPrecio"
        Me.lblListaPrecio.Size = New System.Drawing.Size(182, 13)
        Me.lblListaPrecio.TabIndex = 197
        Me.lblListaPrecio.Text = "Lista de Precios: TABLERISTA"
        '
        'txtCliente
        '
        Me.txtCliente.AccessibleName = ""
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCliente.Decimals = CType(2, Byte)
        Me.txtCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCliente.Location = New System.Drawing.Point(436, 32)
        Me.txtCliente.MaxLength = 25
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(202, 20)
        Me.txtCliente.TabIndex = 194
        Me.txtCliente.Text_1 = Nothing
        Me.txtCliente.Text_2 = Nothing
        Me.txtCliente.Text_3 = Nothing
        Me.txtCliente.Text_4 = Nothing
        Me.txtCliente.UserValues = Nothing
        '
        'txtIdComprador
        '
        Me.txtIdComprador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdComprador.Decimals = CType(2, Byte)
        Me.txtIdComprador.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdComprador.Enabled = False
        Me.txtIdComprador.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdComprador.Location = New System.Drawing.Point(873, 209)
        Me.txtIdComprador.MaxLength = 8
        Me.txtIdComprador.Name = "txtIdComprador"
        Me.txtIdComprador.Size = New System.Drawing.Size(40, 20)
        Me.txtIdComprador.TabIndex = 193
        Me.txtIdComprador.Text_1 = Nothing
        Me.txtIdComprador.Text_2 = Nothing
        Me.txtIdComprador.Text_3 = Nothing
        Me.txtIdComprador.Text_4 = Nothing
        Me.txtIdComprador.UserValues = Nothing
        Me.txtIdComprador.Visible = False
        '
        'txtIdUsuario
        '
        Me.txtIdUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdUsuario.Decimals = CType(2, Byte)
        Me.txtIdUsuario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUsuario.Enabled = False
        Me.txtIdUsuario.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdUsuario.Location = New System.Drawing.Point(932, 209)
        Me.txtIdUsuario.MaxLength = 8
        Me.txtIdUsuario.Name = "txtIdUsuario"
        Me.txtIdUsuario.Size = New System.Drawing.Size(40, 20)
        Me.txtIdUsuario.TabIndex = 192
        Me.txtIdUsuario.Text_1 = Nothing
        Me.txtIdUsuario.Text_2 = Nothing
        Me.txtIdUsuario.Text_3 = Nothing
        Me.txtIdUsuario.Text_4 = Nothing
        Me.txtIdUsuario.UserValues = Nothing
        Me.txtIdUsuario.Visible = False
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.BackColor = System.Drawing.Color.White
        Me.lblEstado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstado.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblEstado.Location = New System.Drawing.Point(370, 492)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(203, 16)
        Me.lblEstado.TabIndex = 191
        Me.lblEstado.Text = "PRESUPUESTO CUMPLIDO"
        Me.lblEstado.Visible = False
        '
        'BtnFactura
        '
        Me.BtnFactura.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFactura.Enabled = False
        Me.BtnFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFactura.ForeColor = System.Drawing.Color.Red
        Me.BtnFactura.Location = New System.Drawing.Point(1097, 180)
        Me.BtnFactura.Name = "BtnFactura"
        Me.BtnFactura.Size = New System.Drawing.Size(104, 23)
        Me.BtnFactura.TabIndex = 190
        Me.BtnFactura.Text = "Factura (F7) >>"
        Me.BtnFactura.UseVisualStyleBackColor = True
        Me.BtnFactura.Visible = False
        '
        'BtnRemito
        '
        Me.BtnRemito.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRemito.Enabled = False
        Me.BtnRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRemito.ForeColor = System.Drawing.Color.Red
        Me.BtnRemito.Location = New System.Drawing.Point(1225, 100)
        Me.BtnRemito.Name = "BtnRemito"
        Me.BtnRemito.Size = New System.Drawing.Size(104, 23)
        Me.BtnRemito.TabIndex = 189
        Me.BtnRemito.Text = "Remito (F6) >>"
        Me.BtnRemito.UseVisualStyleBackColor = True
        '
        'chkBuscarClientes
        '
        Me.chkBuscarClientes.AutoSize = True
        Me.chkBuscarClientes.Location = New System.Drawing.Point(343, 138)
        Me.chkBuscarClientes.Name = "chkBuscarClientes"
        Me.chkBuscarClientes.Size = New System.Drawing.Size(117, 17)
        Me.chkBuscarClientes.TabIndex = 188
        Me.chkBuscarClientes.Text = "Buscar por Clientes"
        Me.chkBuscarClientes.UseVisualStyleBackColor = True
        Me.chkBuscarClientes.Visible = False
        '
        'cmbCliente2
        '
        Me.cmbCliente2.AccessibleName = ""
        Me.cmbCliente2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente2.DropDownHeight = 500
        Me.cmbCliente2.Enabled = False
        Me.cmbCliente2.FormattingEnabled = True
        Me.cmbCliente2.IntegralHeight = False
        Me.cmbCliente2.Location = New System.Drawing.Point(488, 138)
        Me.cmbCliente2.Name = "cmbCliente2"
        Me.cmbCliente2.Size = New System.Drawing.Size(187, 21)
        Me.cmbCliente2.TabIndex = 187
        Me.cmbCliente2.Visible = False
        '
        'txtAnticipo
        '
        Me.txtAnticipo.AccessibleName = ""
        Me.txtAnticipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAnticipo.Decimals = CType(2, Byte)
        Me.txtAnticipo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtAnticipo.Enabled = False
        Me.txtAnticipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnticipo.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtAnticipo.Location = New System.Drawing.Point(279, 73)
        Me.txtAnticipo.Name = "txtAnticipo"
        Me.txtAnticipo.Size = New System.Drawing.Size(41, 20)
        Me.txtAnticipo.TabIndex = 22
        Me.txtAnticipo.Text_1 = Nothing
        Me.txtAnticipo.Text_2 = Nothing
        Me.txtAnticipo.Text_3 = Nothing
        Me.txtAnticipo.Text_4 = Nothing
        Me.txtAnticipo.UserValues = Nothing
        '
        'chkRecDescGlobal
        '
        Me.chkRecDescGlobal.AutoSize = True
        Me.chkRecDescGlobal.Location = New System.Drawing.Point(638, 12)
        Me.chkRecDescGlobal.Name = "chkRecDescGlobal"
        Me.chkRecDescGlobal.Size = New System.Drawing.Size(75, 15)
        Me.chkRecDescGlobal.TabIndex = 5
        Me.chkRecDescGlobal.Text = "Rec / Desc"
        Me.chkRecDescGlobal.TextColor = System.Drawing.Color.Blue
        '
        'txtNroOC
        '
        Me.txtNroOC.AccessibleName = ""
        Me.txtNroOC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroOC.Decimals = CType(2, Byte)
        Me.txtNroOC.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroOC.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroOC.Location = New System.Drawing.Point(1033, 32)
        Me.txtNroOC.Name = "txtNroOC"
        Me.txtNroOC.Size = New System.Drawing.Size(97, 20)
        Me.txtNroOC.TabIndex = 15
        Me.txtNroOC.Text_1 = Nothing
        Me.txtNroOC.Text_2 = Nothing
        Me.txtNroOC.Text_3 = Nothing
        Me.txtNroOC.Text_4 = Nothing
        Me.txtNroOC.UserValues = Nothing
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 494)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 176
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(106, 494)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(46, 13)
        Me.lblCantidadFilas.TabIndex = 175
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'Label14
        '
        Me.Label14.AccessibleName = ""
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(195, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(118, 13)
        Me.Label14.TabIndex = 174
        Me.Label14.Text = "Nombre de Referencia*"
        '
        'txtNombre
        '
        Me.txtNombre.AccessibleName = "*Nombre de Referencia"
        Me.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNombre.Decimals = CType(2, Byte)
        Me.txtNombre.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombre.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNombre.Location = New System.Drawing.Point(198, 32)
        Me.txtNombre.MaxLength = 75
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(232, 20)
        Me.txtNombre.TabIndex = 3
        Me.txtNombre.Text_1 = Nothing
        Me.txtNombre.Text_2 = Nothing
        Me.txtNombre.Text_3 = Nothing
        Me.txtNombre.Text_4 = Nothing
        Me.txtNombre.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(991, 58)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 13)
        Me.Label18.TabIndex = 172
        Me.Label18.Text = "Nota de Gestión"
        '
        'txtNotaGestion
        '
        Me.txtNotaGestion.AccessibleName = ""
        Me.txtNotaGestion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNotaGestion.Decimals = CType(2, Byte)
        Me.txtNotaGestion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNotaGestion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotaGestion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNotaGestion.Location = New System.Drawing.Point(994, 74)
        Me.txtNotaGestion.Name = "txtNotaGestion"
        Me.txtNotaGestion.Size = New System.Drawing.Size(221, 20)
        Me.txtNotaGestion.TabIndex = 26
        Me.txtNotaGestion.Text_1 = Nothing
        Me.txtNotaGestion.Text_2 = Nothing
        Me.txtNotaGestion.Text_3 = Nothing
        Me.txtNotaGestion.Text_4 = Nothing
        Me.txtNotaGestion.UserValues = Nothing
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(557, 59)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 13)
        Me.Label17.TabIndex = 170
        Me.Label17.Text = "Vendedor*"
        '
        'cmbVendedor
        '
        Me.cmbVendedor.AccessibleName = "*VENDEDOR"
        Me.cmbVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbVendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbVendedor.DropDownHeight = 300
        Me.cmbVendedor.FormattingEnabled = True
        Me.cmbVendedor.IntegralHeight = False
        Me.cmbVendedor.Location = New System.Drawing.Point(558, 73)
        Me.cmbVendedor.Name = "cmbVendedor"
        Me.cmbVendedor.Size = New System.Drawing.Size(149, 21)
        Me.cmbVendedor.TabIndex = 24
        '
        'txtporcrecargo
        '
        Me.txtporcrecargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtporcrecargo.Decimals = CType(2, Byte)
        Me.txtporcrecargo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtporcrecargo.Enabled = False
        Me.txtporcrecargo.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtporcrecargo.Location = New System.Drawing.Point(644, 32)
        Me.txtporcrecargo.MaxLength = 20
        Me.txtporcrecargo.Name = "txtporcrecargo"
        Me.txtporcrecargo.Size = New System.Drawing.Size(46, 20)
        Me.txtporcrecargo.TabIndex = 8
        Me.txtporcrecargo.Text_1 = Nothing
        Me.txtporcrecargo.Text_2 = Nothing
        Me.txtporcrecargo.Text_3 = Nothing
        Me.txtporcrecargo.Text_4 = Nothing
        Me.txtporcrecargo.UserValues = Nothing
        '
        'txtRevision
        '
        Me.txtRevision.AccessibleName = "CODIGO"
        Me.txtRevision.BackColor = System.Drawing.SystemColors.Window
        Me.txtRevision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRevision.Decimals = CType(2, Byte)
        Me.txtRevision.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRevision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRevision.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtRevision.Location = New System.Drawing.Point(79, 32)
        Me.txtRevision.MaxLength = 25
        Me.txtRevision.Name = "txtRevision"
        Me.txtRevision.ReadOnly = True
        Me.txtRevision.Size = New System.Drawing.Size(20, 20)
        Me.txtRevision.TabIndex = 165
        Me.txtRevision.Text_1 = Nothing
        Me.txtRevision.Text_2 = Nothing
        Me.txtRevision.Text_3 = Nothing
        Me.txtRevision.Text_4 = Nothing
        Me.txtRevision.UserValues = Nothing
        '
        'txtReq
        '
        Me.txtReq.AccessibleName = ""
        Me.txtReq.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReq.Decimals = CType(2, Byte)
        Me.txtReq.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtReq.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReq.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtReq.Location = New System.Drawing.Point(1136, 32)
        Me.txtReq.Name = "txtReq"
        Me.txtReq.Size = New System.Drawing.Size(79, 20)
        Me.txtReq.TabIndex = 17
        Me.txtReq.Text_1 = Nothing
        Me.txtReq.Text_2 = Nothing
        Me.txtReq.Text_3 = Nothing
        Me.txtReq.Text_4 = Nothing
        Me.txtReq.UserValues = Nothing
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(710, 59)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(79, 13)
        Me.Label16.TabIndex = 163
        Me.Label16.Text = "Autorizado por*"
        '
        'cmbAutoriza
        '
        Me.cmbAutoriza.AccessibleName = "*AUTORIZADO_POR"
        Me.cmbAutoriza.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAutoriza.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAutoriza.DropDownHeight = 300
        Me.cmbAutoriza.FormattingEnabled = True
        Me.cmbAutoriza.IntegralHeight = False
        Me.cmbAutoriza.Location = New System.Drawing.Point(713, 73)
        Me.cmbAutoriza.Name = "cmbAutoriza"
        Me.cmbAutoriza.Size = New System.Drawing.Size(149, 21)
        Me.cmbAutoriza.TabIndex = 25
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(245, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 161
        Me.Label8.Text = "días"
        '
        'txtValidez
        '
        Me.txtValidez.AccessibleName = "*VALIDEZ"
        Me.txtValidez.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValidez.Decimals = CType(2, Byte)
        Me.txtValidez.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtValidez.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValidez.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtValidez.Location = New System.Drawing.Point(210, 74)
        Me.txtValidez.Name = "txtValidez"
        Me.txtValidez.Size = New System.Drawing.Size(29, 20)
        Me.txtValidez.TabIndex = 20
        Me.txtValidez.Text_1 = Nothing
        Me.txtValidez.Text_2 = Nothing
        Me.txtValidez.Text_3 = Nothing
        Me.txtValidez.Text_4 = Nothing
        Me.txtValidez.UserValues = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(343, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Forma de Pago (Saldo)*"
        '
        'cmbFormaPago
        '
        Me.cmbFormaPago.AccessibleName = "*FORMA_DE_PAGO"
        Me.cmbFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFormaPago.DropDownHeight = 300
        Me.cmbFormaPago.FormattingEnabled = True
        Me.cmbFormaPago.IntegralHeight = False
        Me.cmbFormaPago.Location = New System.Drawing.Point(346, 73)
        Me.cmbFormaPago.Name = "cmbFormaPago"
        Me.cmbFormaPago.Size = New System.Drawing.Size(206, 21)
        Me.cmbFormaPago.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(207, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 157
        Me.Label6.Text = "Validez*"
        '
        'txtIva10PE
        '
        Me.txtIva10PE.AccessibleName = ""
        Me.txtIva10PE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIva10PE.Decimals = CType(2, Byte)
        Me.txtIva10PE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIva10PE.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIva10PE.Location = New System.Drawing.Point(815, 514)
        Me.txtIva10PE.MaxLength = 50
        Me.txtIva10PE.Name = "txtIva10PE"
        Me.txtIva10PE.ReadOnly = True
        Me.txtIva10PE.Size = New System.Drawing.Size(57, 20)
        Me.txtIva10PE.TabIndex = 154
        Me.txtIva10PE.Text_1 = Nothing
        Me.txtIva10PE.Text_2 = Nothing
        Me.txtIva10PE.Text_3 = Nothing
        Me.txtIva10PE.Text_4 = Nothing
        Me.txtIva10PE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIva10PE.UserValues = Nothing
        '
        'txtSubtotalPE
        '
        Me.txtSubtotalPE.AccessibleName = ""
        Me.txtSubtotalPE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotalPE.Decimals = CType(2, Byte)
        Me.txtSubtotalPE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalPE.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotalPE.Location = New System.Drawing.Point(694, 491)
        Me.txtSubtotalPE.MaxLength = 50
        Me.txtSubtotalPE.Name = "txtSubtotalPE"
        Me.txtSubtotalPE.ReadOnly = True
        Me.txtSubtotalPE.Size = New System.Drawing.Size(57, 20)
        Me.txtSubtotalPE.TabIndex = 153
        Me.txtSubtotalPE.Text = "0"
        Me.txtSubtotalPE.Text_1 = Nothing
        Me.txtSubtotalPE.Text_2 = Nothing
        Me.txtSubtotalPE.Text_3 = Nothing
        Me.txtSubtotalPE.Text_4 = Nothing
        Me.txtSubtotalPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotalPE.UserValues = Nothing
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = ""
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtIVA.Location = New System.Drawing.Point(279, 139)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(41, 20)
        Me.txtIVA.TabIndex = 2
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        Me.txtIVA.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(255, 139)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 13)
        Me.Label15.TabIndex = 151
        Me.Label15.Text = "IVA*"
        Me.Label15.Visible = False
        '
        'cmbEntregaren
        '
        Me.cmbEntregaren.AccessibleName = ""
        Me.cmbEntregaren.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbEntregaren.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEntregaren.DropDownHeight = 300
        Me.cmbEntregaren.Enabled = False
        Me.cmbEntregaren.FormattingEnabled = True
        Me.cmbEntregaren.IntegralHeight = False
        Me.cmbEntregaren.Location = New System.Drawing.Point(13, 73)
        Me.cmbEntregaren.Name = "cmbEntregaren"
        Me.cmbEntregaren.Size = New System.Drawing.Size(191, 21)
        Me.cmbEntregaren.TabIndex = 19
        '
        'cmbUsuario
        '
        Me.cmbUsuario.AccessibleName = ""
        Me.cmbUsuario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbUsuario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbUsuario.DropDownHeight = 300
        Me.cmbUsuario.Enabled = False
        Me.cmbUsuario.FormattingEnabled = True
        Me.cmbUsuario.IntegralHeight = False
        Me.cmbUsuario.Location = New System.Drawing.Point(878, 32)
        Me.cmbUsuario.Name = "cmbUsuario"
        Me.cmbUsuario.Size = New System.Drawing.Size(149, 21)
        Me.cmbUsuario.TabIndex = 12
        '
        'cmbComprador
        '
        Me.cmbComprador.AccessibleName = ""
        Me.cmbComprador.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbComprador.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbComprador.DropDownHeight = 300
        Me.cmbComprador.Enabled = False
        Me.cmbComprador.FormattingEnabled = True
        Me.cmbComprador.IntegralHeight = False
        Me.cmbComprador.Location = New System.Drawing.Point(723, 32)
        Me.cmbComprador.Name = "cmbComprador"
        Me.cmbComprador.Size = New System.Drawing.Size(149, 21)
        Me.cmbComprador.TabIndex = 10
        '
        'PicNotas
        '
        Me.PicNotas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicNotas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicNotas.Image = CType(resources.GetObject("PicNotas.Image"), System.Drawing.Image)
        Me.PicNotas.Location = New System.Drawing.Point(1308, 157)
        Me.PicNotas.Name = "PicNotas"
        Me.PicNotas.Size = New System.Drawing.Size(18, 20)
        Me.PicNotas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicNotas.TabIndex = 142
        Me.PicNotas.TabStop = False
        '
        'chkNotas
        '
        Me.chkNotas.AccessibleName = "Eliminado"
        Me.chkNotas.AutoSize = True
        Me.chkNotas.Location = New System.Drawing.Point(1215, 158)
        Me.chkNotas.Name = "chkNotas"
        Me.chkNotas.Size = New System.Drawing.Size(91, 17)
        Me.chkNotas.TabIndex = 29
        Me.chkNotas.Text = "Incluye Notas"
        Me.chkNotas.UseVisualStyleBackColor = True
        '
        'lblSubtotalPE
        '
        Me.lblSubtotalPE.AutoSize = True
        Me.lblSubtotalPE.Location = New System.Drawing.Point(627, 494)
        Me.lblSubtotalPE.Name = "lblSubtotalPE"
        Me.lblSubtotalPE.Size = New System.Drawing.Size(61, 13)
        Me.lblSubtotalPE.TabIndex = 134
        Me.lblSubtotalPE.Text = "Subtotal ($)"
        '
        'lblIVA10PE
        '
        Me.lblIVA10PE.AutoSize = True
        Me.lblIVA10PE.Location = New System.Drawing.Point(751, 517)
        Me.lblIVA10PE.Name = "lblIVA10PE"
        Me.lblIVA10PE.Size = New System.Drawing.Size(63, 13)
        Me.lblIVA10PE.TabIndex = 126
        Me.lblIVA10PE.Text = "IVA 10.5 ($)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(433, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "Cliente*"
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
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 32)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(60, 20)
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
        Me.Label2.Location = New System.Drawing.Point(10, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro Presup."
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(102, 32)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(90, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(102, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*CLIENTE"
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.DropDownHeight = 500
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(436, 32)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(202, 21)
        Me.cmbCliente.TabIndex = 4
        '
        'PicClientes
        '
        Me.PicClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicClientes.Image = CType(resources.GetObject("PicClientes.Image"), System.Drawing.Image)
        Me.PicClientes.Location = New System.Drawing.Point(482, 12)
        Me.PicClientes.Name = "PicClientes"
        Me.PicClientes.Size = New System.Drawing.Size(18, 20)
        Me.PicClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicClientes.TabIndex = 105
        Me.PicClientes.TabStop = False
        '
        'chkUsuario
        '
        Me.chkUsuario.Location = New System.Drawing.Point(878, 12)
        Me.chkUsuario.Name = "chkUsuario"
        Me.chkUsuario.Size = New System.Drawing.Size(75, 23)
        Me.chkUsuario.TabIndex = 11
        Me.chkUsuario.Text = "Usuario"
        Me.chkUsuario.TextColor = System.Drawing.Color.Blue
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(78, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 13)
        Me.Label10.TabIndex = 166
        Me.Label10.Text = "Rev."
        '
        'chkEntrega
        '
        Me.chkEntrega.Location = New System.Drawing.Point(13, 53)
        Me.chkEntrega.Name = "chkEntrega"
        Me.chkEntrega.Size = New System.Drawing.Size(100, 23)
        Me.chkEntrega.TabIndex = 18
        Me.chkEntrega.Text = "Entregar en..."
        Me.chkEntrega.TextColor = System.Drawing.Color.Blue
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(868, 53)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
        Me.chkEliminado.TabIndex = 20
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'chkComprador
        '
        Me.chkComprador.Location = New System.Drawing.Point(723, 11)
        Me.chkComprador.Name = "chkComprador"
        Me.chkComprador.Size = New System.Drawing.Size(75, 23)
        Me.chkComprador.TabIndex = 9
        Me.chkComprador.Text = "Comprador"
        Me.chkComprador.TextColor = System.Drawing.Color.Blue
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(109, 51)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(40, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        '
        'txtID_OfertaComercial
        '
        Me.txtID_OfertaComercial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID_OfertaComercial.Decimals = CType(2, Byte)
        Me.txtID_OfertaComercial.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID_OfertaComercial.Enabled = False
        Me.txtID_OfertaComercial.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID_OfertaComercial.Location = New System.Drawing.Point(774, 209)
        Me.txtID_OfertaComercial.MaxLength = 8
        Me.txtID_OfertaComercial.Name = "txtID_OfertaComercial"
        Me.txtID_OfertaComercial.Size = New System.Drawing.Size(40, 20)
        Me.txtID_OfertaComercial.TabIndex = 50
        Me.txtID_OfertaComercial.Text_1 = Nothing
        Me.txtID_OfertaComercial.Text_2 = Nothing
        Me.txtID_OfertaComercial.Text_3 = Nothing
        Me.txtID_OfertaComercial.Text_4 = Nothing
        Me.txtID_OfertaComercial.UserValues = Nothing
        Me.txtID_OfertaComercial.Visible = False
        '
        'chkOcultarGanancia
        '
        Me.chkOcultarGanancia.Checked = True
        Me.chkOcultarGanancia.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOcultarGanancia.CheckValue = "Y"
        Me.chkOcultarGanancia.Location = New System.Drawing.Point(956, 148)
        Me.chkOcultarGanancia.Name = "chkOcultarGanancia"
        Me.chkOcultarGanancia.Size = New System.Drawing.Size(114, 23)
        Me.chkOcultarGanancia.TabIndex = 27
        Me.chkOcultarGanancia.Text = "Ocultar PL y GC"
        Me.chkOcultarGanancia.TextColor = System.Drawing.Color.Maroon
        Me.chkOcultarGanancia.Visible = False
        '
        'picGanancia
        '
        Me.picGanancia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGanancia.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picGanancia.Image = CType(resources.GetObject("picGanancia.Image"), System.Drawing.Image)
        Me.picGanancia.Location = New System.Drawing.Point(1090, 100)
        Me.picGanancia.Name = "picGanancia"
        Me.picGanancia.Size = New System.Drawing.Size(18, 20)
        Me.picGanancia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGanancia.TabIndex = 178
        Me.picGanancia.TabStop = False
        Me.picGanancia.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(689, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 13)
        Me.Label4.TabIndex = 181
        Me.Label4.Text = "%"
        Me.Label4.Visible = False
        '
        'chkAnticipo
        '
        Me.chkAnticipo.Location = New System.Drawing.Point(276, 53)
        Me.chkAnticipo.Name = "chkAnticipo"
        Me.chkAnticipo.Size = New System.Drawing.Size(61, 23)
        Me.chkAnticipo.TabIndex = 21
        Me.chkAnticipo.Text = "Anticipo"
        Me.chkAnticipo.TextColor = System.Drawing.Color.Blue
        '
        'grdItems
        '
        Me.grdItems.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(13, 100)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1196, 385)
        Me.grdItems.TabIndex = 26
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(320, 76)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(15, 13)
        Me.Label12.TabIndex = 185
        Me.Label12.Text = "%"
        '
        'PicFormaPago
        '
        Me.PicFormaPago.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicFormaPago.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicFormaPago.Image = CType(resources.GetObject("PicFormaPago.Image"), System.Drawing.Image)
        Me.PicFormaPago.Location = New System.Drawing.Point(468, 54)
        Me.PicFormaPago.Name = "PicFormaPago"
        Me.PicFormaPago.Size = New System.Drawing.Size(18, 20)
        Me.PicFormaPago.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicFormaPago.TabIndex = 215
        Me.PicFormaPago.TabStop = False
        '
        'PicEmpleados
        '
        Me.PicEmpleados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicEmpleados.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicEmpleados.Image = CType(resources.GetObject("PicEmpleados.Image"), System.Drawing.Image)
        Me.PicEmpleados.Location = New System.Drawing.Point(620, 54)
        Me.PicEmpleados.Name = "PicEmpleados"
        Me.PicEmpleados.Size = New System.Drawing.Size(18, 20)
        Me.PicEmpleados.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicEmpleados.TabIndex = 214
        Me.PicEmpleados.TabStop = False
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
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItem_OfertaComercial_ToolStripMenuItem})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(146, 26)
        '
        'BorrarElItem_OfertaComercial_ToolStripMenuItem
        '
        Me.BorrarElItem_OfertaComercial_ToolStripMenuItem.Name = "BorrarElItem_OfertaComercial_ToolStripMenuItem"
        Me.BorrarElItem_OfertaComercial_ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.BorrarElItem_OfertaComercial_ToolStripMenuItem.Text = "Borrar el Item"
        '
        'frmPresupuestos
        '
        Me.AccessibleName = "Presupuestos"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPresupuestos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdTrafos_Det, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTrafos_Ensayos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOfertaComercial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicNotas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGanancia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicFormaPago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicEmpleados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub










    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEntregaren As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUsuario As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComprador As System.Windows.Forms.ComboBox
    Friend WithEvents PicNotas As System.Windows.Forms.PictureBox
    Friend WithEvents lstNotas As System.Windows.Forms.ListView
    Friend WithEvents Nota As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkNotas As System.Windows.Forms.CheckBox
    Friend WithEvents lblSubtotalPE As System.Windows.Forms.Label
    Friend WithEvents lblIVA10PE As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtID_OfertaComercial As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents PicClientes As System.Windows.Forms.PictureBox
    Friend WithEvents txtSubtotalPE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIva10PE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbFormaPago As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtValidez As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbAutoriza As System.Windows.Forms.ComboBox
    Friend WithEvents chkUsuario As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtReq As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkEntrega As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtRevision As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtporcrecargo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNotaGestion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbVendedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtNombre As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbUnidadVenta As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents chkComprador As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtNroOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkOcultarGanancia As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents picGanancia As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkRecDescGlobal As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnticipo As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtAnticipo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkAmpliarGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCliente2 As System.Windows.Forms.ComboBox
    Friend WithEvents chkBuscarClientes As System.Windows.Forms.CheckBox
    Friend WithEvents BtnFactura As System.Windows.Forms.Button
    Friend WithEvents BtnRemito As System.Windows.Forms.Button
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents txtIdComprador As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdUsuario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkOCA As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtIdFormaPago As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkAmpliarNotas As System.Windows.Forms.CheckBox
    Friend WithEvents txtSubtotalDO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblSubtotalDO As System.Windows.Forms.Label
    Friend WithEvents txtIva10DO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblIVA10DO As System.Windows.Forms.Label
    Friend WithEvents txtIva21PE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblIVA21PE As System.Windows.Forms.Label
    Friend WithEvents txtIva21DO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblIVA21DO As System.Windows.Forms.Label
    Friend WithEvents txtTotalDO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTotalDO As System.Windows.Forms.Label
    Friend WithEvents txtTotalPE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTotalPE As System.Windows.Forms.Label
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PicEmpleados As System.Windows.Forms.PictureBox
    Friend WithEvents PicFormaPago As System.Windows.Forms.PictureBox
    Friend WithEvents chkOC As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblListaPrecio As System.Windows.Forms.Label
    Friend WithEvents chkPrecioDistribuidor As System.Windows.Forms.CheckBox
    Friend WithEvents grdOfertaComercial As System.Windows.Forms.DataGridView
    Friend WithEvents chkAgregarOfertaComercial As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalOferta As System.Windows.Forms.Label
    Friend WithEvents lblMontoIvaOferta As System.Windows.Forms.Label
    Friend WithEvents cmbCertificaciones As System.Windows.Forms.ComboBox
    Friend WithEvents chkCertificaciones As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cmbPlazoEntregaProvision As System.Windows.Forms.ComboBox
    Friend WithEvents chkPlazoEntregaProvision As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cmbPlazoEntrega As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAjustes As System.Windows.Forms.ComboBox
    Friend WithEvents chkAjustes As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkPlazoEntrega As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblIvaOferta As System.Windows.Forms.Label
    Friend WithEvents txtIvaOferta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMontoIvaOferta As System.Windows.Forms.Label
    Friend WithEvents txtTotalOferta As System.Windows.Forms.Label
    Friend WithEvents rdBaja As System.Windows.Forms.RadioButton
    Friend WithEvents rdMedia As System.Windows.Forms.RadioButton
    Friend WithEvents rdTrafo As System.Windows.Forms.RadioButton
    Friend WithEvents rdTableros As System.Windows.Forms.RadioButton
    Friend WithEvents rdMateriales As System.Windows.Forms.RadioButton
    Friend WithEvents txtSubtotalOferta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItem_OfertaComercial_ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAsignarTablero As System.Windows.Forms.Button
    Friend WithEvents chkSubtotalOferta As System.Windows.Forms.CheckBox
    Friend WithEvents chkMostrarCodigoMaterial As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbEstado As System.Windows.Forms.ComboBox
    Friend WithEvents chkPresupuestosCumplidos As System.Windows.Forms.CheckBox
    Friend WithEvents grdTrafos_Ensayos As System.Windows.Forms.DataGridView
    Friend WithEvents grdTrafos_Det As System.Windows.Forms.DataGridView
    Friend WithEvents lblTrafo_Cabecera As System.Windows.Forms.Label
    Friend WithEvents txtTrafo_Observaciones As System.Windows.Forms.TextBox
    Friend WithEvents txtTrafo_Cabecera As System.Windows.Forms.TextBox
    Friend WithEvents lblTrafo_Observaciones As System.Windows.Forms.Label
    Friend WithEvents txtTrafo_SubtotalEnsayos As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTrafo_CantHoras As System.Windows.Forms.Label
    Friend WithEvents txtTrafo_CantHoras As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTrafo_SubtotalEnsayos As System.Windows.Forms.Label
    Friend WithEvents btnCopiarPres As System.Windows.Forms.Button

End Class
