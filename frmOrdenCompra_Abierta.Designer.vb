<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrdenCompra_Abierta

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrdenCompra_Abierta))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnHabilitarOCA = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtOCasociado = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkFacturado = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.btnCerrarPCE = New System.Windows.Forms.Button
        Me.btnCerrarPresup = New System.Windows.Forms.Button
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.chkCerrado = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkRecDescGlobal = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkDesc = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkRecargo = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblCantidadFilas = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtNotaGestion = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label17 = New System.Windows.Forms.Label
        Me.cmbVendedor = New System.Windows.Forms.ComboBox
        Me.txtporcrecargo = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtIvaTotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label15 = New System.Windows.Forms.Label
        Me.PicNotas = New System.Windows.Forms.PictureBox
        Me.lstNotas = New System.Windows.Forms.ListView
        Me.Nota = New System.Windows.Forms.ColumnHeader
        Me.chkNotas = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.grdItems = New System.Windows.Forms.DataGridView
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbCliente = New System.Windows.Forms.ComboBox
        Me.PicClientes = New System.Windows.Forms.PictureBox
        Me.chkEliminado = New System.Windows.Forms.CheckBox
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.chkOcultarGanancia = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.picGanancia = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbUnidadVenta = New System.Windows.Forms.ToolStripComboBox
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGanancia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
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
        Me.GroupBox1.Controls.Add(Me.btnHabilitarOCA)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtOCasociado)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.chkFacturado)
        Me.GroupBox1.Controls.Add(Me.btnCerrarPCE)
        Me.GroupBox1.Controls.Add(Me.btnCerrarPresup)
        Me.GroupBox1.Controls.Add(Me.btnCerrar)
        Me.GroupBox1.Controls.Add(Me.chkCerrado)
        Me.GroupBox1.Controls.Add(Me.chkRecDescGlobal)
        Me.GroupBox1.Controls.Add(Me.chkDesc)
        Me.GroupBox1.Controls.Add(Me.chkRecargo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtNotaGestion)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cmbVendedor)
        Me.GroupBox1.Controls.Add(Me.txtporcrecargo)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtIvaTotal)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.PicNotas)
        Me.GroupBox1.Controls.Add(Me.lstNotas)
        Me.GroupBox1.Controls.Add(Me.chkNotas)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.PicClientes)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.chkOcultarGanancia)
        Me.GroupBox1.Controls.Add(Me.picGanancia)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1244, 560)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnHabilitarOCA
        '
        Me.btnHabilitarOCA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHabilitarOCA.ForeColor = System.Drawing.Color.Blue
        Me.btnHabilitarOCA.Location = New System.Drawing.Point(756, 93)
        Me.btnHabilitarOCA.Name = "btnHabilitarOCA"
        Me.btnHabilitarOCA.Size = New System.Drawing.Size(213, 23)
        Me.btnHabilitarOCA.TabIndex = 201
        Me.btnHabilitarOCA.Text = "Habilitar Orden de Compra Abierta"
        Me.btnHabilitarOCA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHabilitarOCA.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(1072, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(139, 28)
        Me.Label8.TabIndex = 204
        Me.Label8.Text = "Cierra la OCA y cancela la misma con un pago Contado Efectivo"
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(929, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(268, 28)
        Me.Label7.TabIndex = 205
        Me.Label7.Text = "Cierra la OCA y la deja preparada para ser cancelada o para convertir en presupue" & _
            "sto"
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(1082, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(142, 31)
        Me.Label10.TabIndex = 203
        Me.Label10.Text = "Cierra la OCA y convierte la misma en un Presupuesto"
        '
        'txtIdCliente
        '
        Me.txtIdCliente.AccessibleName = ""
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIdCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(427, 88)
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(35, 20)
        Me.txtIdCliente.TabIndex = 196
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'txtOCasociado
        '
        Me.txtOCasociado.AccessibleName = ""
        Me.txtOCasociado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOCasociado.Decimals = CType(2, Byte)
        Me.txtOCasociado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtOCasociado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOCasociado.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtOCasociado.Location = New System.Drawing.Point(519, 32)
        Me.txtOCasociado.Name = "txtOCasociado"
        Me.txtOCasociado.Size = New System.Drawing.Size(118, 20)
        Me.txtOCasociado.TabIndex = 194
        Me.txtOCasociado.Text_1 = Nothing
        Me.txtOCasociado.Text_2 = Nothing
        Me.txtOCasociado.Text_3 = Nothing
        Me.txtOCasociado.Text_4 = Nothing
        Me.txtOCasociado.UserValues = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(516, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 195
        Me.Label6.Text = "Nro OC Asociado"
        '
        'chkFacturado
        '
        Me.chkFacturado.AutoSize = True
        Me.chkFacturado.Location = New System.Drawing.Point(1234, 70)
        Me.chkFacturado.Name = "chkFacturado"
        Me.chkFacturado.Size = New System.Drawing.Size(71, 15)
        Me.chkFacturado.TabIndex = 193
        Me.chkFacturado.Text = "Facturado"
        Me.chkFacturado.TextColor = System.Drawing.Color.Blue
        Me.chkFacturado.Visible = False
        '
        'btnCerrarPCE
        '
        Me.btnCerrarPCE.Enabled = False
        Me.btnCerrarPCE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrarPCE.ForeColor = System.Drawing.Color.Red
        Me.btnCerrarPCE.Location = New System.Drawing.Point(756, 40)
        Me.btnCerrarPCE.Name = "btnCerrarPCE"
        Me.btnCerrarPCE.Size = New System.Drawing.Size(311, 23)
        Me.btnCerrarPCE.TabIndex = 192
        Me.btnCerrarPCE.Text = "Cerrar OC Abierta / Pago Contado-Efectivo (F7) >>"
        Me.btnCerrarPCE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrarPCE.UseVisualStyleBackColor = True
        '
        'btnCerrarPresup
        '
        Me.btnCerrarPresup.Enabled = False
        Me.btnCerrarPresup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrarPresup.ForeColor = System.Drawing.Color.Red
        Me.btnCerrarPresup.Location = New System.Drawing.Point(756, 66)
        Me.btnCerrarPresup.Name = "btnCerrarPresup"
        Me.btnCerrarPresup.Size = New System.Drawing.Size(320, 23)
        Me.btnCerrarPresup.TabIndex = 191
        Me.btnCerrarPresup.Text = "Cerrar OC Abierta / Convertir en Presupuesto (F8) >>"
        Me.btnCerrarPresup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrarPresup.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Enabled = False
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.ForeColor = System.Drawing.Color.Red
        Me.btnCerrar.Location = New System.Drawing.Point(756, 13)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(167, 23)
        Me.btnCerrar.TabIndex = 190
        Me.btnCerrar.Text = "Cerrar OC Abierta (F6) >>"
        Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'chkCerrado
        '
        Me.chkCerrado.AutoSize = True
        Me.chkCerrado.Location = New System.Drawing.Point(1238, 100)
        Me.chkCerrado.Name = "chkCerrado"
        Me.chkCerrado.Size = New System.Drawing.Size(61, 15)
        Me.chkCerrado.TabIndex = 184
        Me.chkCerrado.Text = "Cerrado"
        Me.chkCerrado.TextColor = System.Drawing.Color.Blue
        Me.chkCerrado.Visible = False
        '
        'chkRecDescGlobal
        '
        Me.chkRecDescGlobal.AutoSize = True
        Me.chkRecDescGlobal.Location = New System.Drawing.Point(561, 233)
        Me.chkRecDescGlobal.Name = "chkRecDescGlobal"
        Me.chkRecDescGlobal.Size = New System.Drawing.Size(110, 15)
        Me.chkRecDescGlobal.TabIndex = 6
        Me.chkRecDescGlobal.Text = "Rec / Desc Global"
        Me.chkRecDescGlobal.TextColor = System.Drawing.Color.Blue
        Me.chkRecDescGlobal.Visible = False
        '
        'chkDesc
        '
        Me.chkDesc.AutoSize = True
        Me.chkDesc.Enabled = False
        Me.chkDesc.Location = New System.Drawing.Point(561, 261)
        Me.chkDesc.Name = "chkDesc"
        Me.chkDesc.Size = New System.Drawing.Size(50, 15)
        Me.chkDesc.TabIndex = 8
        Me.chkDesc.Text = "Desc."
        Me.chkDesc.TextColor = System.Drawing.Color.Blue
        Me.chkDesc.Visible = False
        '
        'chkRecargo
        '
        Me.chkRecargo.AutoSize = True
        Me.chkRecargo.Enabled = False
        Me.chkRecargo.Location = New System.Drawing.Point(561, 246)
        Me.chkRecargo.Name = "chkRecargo"
        Me.chkRecargo.Size = New System.Drawing.Size(44, 15)
        Me.chkRecargo.TabIndex = 7
        Me.chkRecargo.Text = "Rec."
        Me.chkRecargo.TextColor = System.Drawing.Color.Blue
        Me.chkRecargo.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1206, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 534)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 176
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(106, 534)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(46, 13)
        Me.lblCantidadFilas.TabIndex = 175
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(195, 55)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 13)
        Me.Label18.TabIndex = 172
        Me.Label18.Text = "Nota de Gestión"
        '
        'txtNotaGestion
        '
        Me.txtNotaGestion.AccessibleName = ""
        Me.txtNotaGestion.Decimals = CType(2, Byte)
        Me.txtNotaGestion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNotaGestion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotaGestion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNotaGestion.Location = New System.Drawing.Point(198, 70)
        Me.txtNotaGestion.Name = "txtNotaGestion"
        Me.txtNotaGestion.Size = New System.Drawing.Size(530, 20)
        Me.txtNotaGestion.TabIndex = 21
        Me.txtNotaGestion.Text_1 = Nothing
        Me.txtNotaGestion.Text_2 = Nothing
        Me.txtNotaGestion.Text_3 = Nothing
        Me.txtNotaGestion.Text_4 = Nothing
        Me.txtNotaGestion.UserValues = Nothing
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(11, 55)
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
        Me.cmbVendedor.Location = New System.Drawing.Point(14, 70)
        Me.cmbVendedor.Name = "cmbVendedor"
        Me.cmbVendedor.Size = New System.Drawing.Size(178, 21)
        Me.cmbVendedor.TabIndex = 20
        '
        'txtporcrecargo
        '
        Me.txtporcrecargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtporcrecargo.Decimals = CType(2, Byte)
        Me.txtporcrecargo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtporcrecargo.Enabled = False
        Me.txtporcrecargo.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtporcrecargo.Location = New System.Drawing.Point(617, 253)
        Me.txtporcrecargo.MaxLength = 20
        Me.txtporcrecargo.Name = "txtporcrecargo"
        Me.txtporcrecargo.Size = New System.Drawing.Size(46, 20)
        Me.txtporcrecargo.TabIndex = 9
        Me.txtporcrecargo.Text_1 = Nothing
        Me.txtporcrecargo.Text_2 = Nothing
        Me.txtporcrecargo.Text_3 = Nothing
        Me.txtporcrecargo.Text_4 = Nothing
        Me.txtporcrecargo.UserValues = Nothing
        Me.txtporcrecargo.Visible = False
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = ""
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotal.Location = New System.Drawing.Point(929, 534)
        Me.txtTotal.MaxLength = 50
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(70, 20)
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
        Me.txtIvaTotal.Location = New System.Drawing.Point(813, 534)
        Me.txtIvaTotal.MaxLength = 50
        Me.txtIvaTotal.Name = "txtIvaTotal"
        Me.txtIvaTotal.ReadOnly = True
        Me.txtIvaTotal.Size = New System.Drawing.Size(70, 20)
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
        Me.txtSubtotal.Location = New System.Drawing.Point(707, 534)
        Me.txtSubtotal.MaxLength = 50
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(70, 20)
        Me.txtSubtotal.TabIndex = 23
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = "*IVA"
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtIVA.Location = New System.Drawing.Point(198, 32)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(41, 20)
        Me.txtIVA.TabIndex = 4
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(195, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 13)
        Me.Label15.TabIndex = 151
        Me.Label15.Text = "IVA*"
        '
        'PicNotas
        '
        Me.PicNotas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicNotas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicNotas.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicNotas.Location = New System.Drawing.Point(1204, 111)
        Me.PicNotas.Name = "PicNotas"
        Me.PicNotas.Size = New System.Drawing.Size(18, 20)
        Me.PicNotas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicNotas.TabIndex = 142
        Me.PicNotas.TabStop = False
        Me.PicNotas.Visible = False
        '
        'lstNotas
        '
        Me.lstNotas.CheckBoxes = True
        Me.lstNotas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nota})
        Me.lstNotas.Enabled = False
        Me.lstNotas.Location = New System.Drawing.Point(1107, 137)
        Me.lstNotas.Name = "lstNotas"
        Me.lstNotas.Size = New System.Drawing.Size(282, 391)
        Me.lstNotas.TabIndex = 24
        Me.lstNotas.UseCompatibleStateImageBehavior = False
        Me.lstNotas.View = System.Windows.Forms.View.Details
        Me.lstNotas.Visible = False
        '
        'Nota
        '
        Me.Nota.Text = "Nota"
        Me.Nota.Width = 248
        '
        'chkNotas
        '
        Me.chkNotas.AccessibleName = "Eliminado"
        Me.chkNotas.AutoSize = True
        Me.chkNotas.Location = New System.Drawing.Point(1107, 114)
        Me.chkNotas.Name = "chkNotas"
        Me.chkNotas.Size = New System.Drawing.Size(91, 17)
        Me.chkNotas.TabIndex = 23
        Me.chkNotas.Text = "Incluye Notas"
        Me.chkNotas.UseVisualStyleBackColor = True
        Me.chkNotas.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(660, 538)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 134
        Me.Label9.Text = "Subtotal"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(889, 538)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 128
        Me.Label13.Text = "Total "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(783, 538)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 126
        Me.Label11.Text = "IVA"
        '
        'grdItems
        '
        Me.grdItems.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(13, 121)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1225, 407)
        Me.grdItems.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(242, 14)
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
        Me.txtCODIGO.Size = New System.Drawing.Size(83, 20)
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
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro OC Abierta"
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
        Me.cmbCliente.DropDownHeight = 300
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(245, 32)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(244, 21)
        Me.cmbCliente.TabIndex = 5
        '
        'PicClientes
        '
        Me.PicClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicClientes.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicClientes.Location = New System.Drawing.Point(495, 32)
        Me.PicClientes.Name = "PicClientes"
        Me.PicClientes.Size = New System.Drawing.Size(18, 20)
        Me.PicClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicClientes.TabIndex = 105
        Me.PicClientes.TabStop = False
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(1228, 35)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
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
        Me.txtID.Location = New System.Drawing.Point(1228, 9)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(77, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'chkOcultarGanancia
        '
        Me.chkOcultarGanancia.Location = New System.Drawing.Point(598, 93)
        Me.chkOcultarGanancia.Name = "chkOcultarGanancia"
        Me.chkOcultarGanancia.Size = New System.Drawing.Size(114, 23)
        Me.chkOcultarGanancia.TabIndex = 22
        Me.chkOcultarGanancia.Text = "Ocultar PL y GC"
        Me.chkOcultarGanancia.TextColor = System.Drawing.Color.Maroon
        '
        'picGanancia
        '
        Me.picGanancia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGanancia.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picGanancia.Image = Global.PORKYS.My.Resources.Resources.icono_ayuda
        Me.picGanancia.Location = New System.Drawing.Point(712, 95)
        Me.picGanancia.Name = "picGanancia"
        Me.picGanancia.Size = New System.Drawing.Size(18, 20)
        Me.picGanancia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGanancia.TabIndex = 178
        Me.picGanancia.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(663, 257)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 13)
        Me.Label4.TabIndex = 181
        Me.Label4.Text = "%"
        Me.Label4.Visible = False
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
        'frmOrdenCompra_Abierta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1268, 636)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmOrdenCompra_Abierta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicNotas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGanancia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub










    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents PicClientes As System.Windows.Forms.PictureBox
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIvaTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents txtporcrecargo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNotaGestion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbVendedor As System.Windows.Forms.ComboBox
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbUnidadVenta As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents chkOcultarGanancia As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents picGanancia As System.Windows.Forms.PictureBox
    Friend WithEvents chkDesc As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkRecargo As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkRecDescGlobal As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkCerrado As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents PicNotas As System.Windows.Forms.PictureBox
    Friend WithEvents lstNotas As System.Windows.Forms.ListView
    Friend WithEvents Nota As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkNotas As System.Windows.Forms.CheckBox
    Friend WithEvents btnCerrarPCE As System.Windows.Forms.Button
    Friend WithEvents btnCerrarPresup As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents chkFacturado As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtOCasociado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnHabilitarOCA As System.Windows.Forms.Button

End Class
