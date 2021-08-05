<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferenciasPorkys
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblAutorizado = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkEliminados = New System.Windows.Forms.CheckBox()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.txtIDDestino = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbDestino = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnActualizarMat = New System.Windows.Forms.Button()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.PicEncargado = New System.Windows.Forms.PictureBox()
        Me.txtMarca = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDMarca = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCantidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtIDEncargado = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbEncargado = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtIDOrigen = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblNroTrans = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbOrigen = New System.Windows.Forms.ComboBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnLlenarGrilla = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStripIVA = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItemIVA = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerDescargas = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrdenItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodMaterial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EquipoHerramienta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodMarca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc_Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nota = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eliminado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicEncargado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStripIVA.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lblAutorizado)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.txtIDDestino)
        Me.GroupBox1.Controls.Add(Me.cmbDestino)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.btnActualizarMat)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.PicEncargado)
        Me.GroupBox1.Controls.Add(Me.txtMarca)
        Me.GroupBox1.Controls.Add(Me.txtUnidad)
        Me.GroupBox1.Controls.Add(Me.txtIDMarca)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.lblStock)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtIDEncargado)
        Me.GroupBox1.Controls.Add(Me.cmbEncargado)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtIDOrigen)
        Me.GroupBox1.Controls.Add(Me.lblNroTrans)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbOrigen)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(9, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1274, 511)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblAutorizado
        '
        Me.lblAutorizado.AutoSize = True
        Me.lblAutorizado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutorizado.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblAutorizado.Location = New System.Drawing.Point(804, 92)
        Me.lblAutorizado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAutorizado.Name = "lblAutorizado"
        Me.lblAutorizado.Size = New System.Drawing.Size(91, 20)
        Me.lblAutorizado.TabIndex = 356
        Me.lblAutorizado.Text = "Empleado"
        Me.lblAutorizado.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(697, 92)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 20)
        Me.Label5.TabIndex = 355
        Me.Label5.Text = "Autorizado: "
        Me.Label5.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 14)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 20)
        Me.Label3.TabIndex = 354
        Me.Label3.Text = "Fecha"
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.ForeColor = System.Drawing.Color.Red
        Me.chkEliminados.Location = New System.Drawing.Point(1120, 156)
        Me.chkEliminados.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(143, 24)
        Me.chkEliminados.TabIndex = 353
        Me.chkEliminados.Text = "Ver Anulados"
        Me.chkEliminados.UseVisualStyleBackColor = False
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(17, 37)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 27)
        Me.dtpFECHA.TabIndex = 0
        Me.dtpFECHA.Tag = "202"
        Me.dtpFECHA.Value = New Date(2019, 4, 24, 16, 13, 17, 0)
        '
        'txtIDDestino
        '
        Me.txtIDDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDDestino.Decimals = CType(2, Byte)
        Me.txtIDDestino.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDDestino.Enabled = False
        Me.txtIDDestino.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIDDestino.Location = New System.Drawing.Point(657, 10)
        Me.txtIDDestino.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDDestino.MaxLength = 8
        Me.txtIDDestino.Name = "txtIDDestino"
        Me.txtIDDestino.Size = New System.Drawing.Size(29, 22)
        Me.txtIDDestino.TabIndex = 351
        Me.txtIDDestino.Text_1 = Nothing
        Me.txtIDDestino.Text_2 = Nothing
        Me.txtIDDestino.Text_3 = Nothing
        Me.txtIDDestino.Text_4 = Nothing
        Me.txtIDDestino.UserValues = Nothing
        Me.txtIDDestino.Visible = False
        '
        'cmbDestino
        '
        Me.cmbDestino.AccessibleName = "*Origen"
        Me.cmbDestino.DropDownHeight = 500
        Me.cmbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDestino.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDestino.FormattingEnabled = True
        Me.cmbDestino.IntegralHeight = False
        Me.cmbDestino.Location = New System.Drawing.Point(501, 36)
        Me.cmbDestino.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbDestino.Name = "cmbDestino"
        Me.cmbDestino.Size = New System.Drawing.Size(184, 28)
        Me.cmbDestino.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AccessibleName = ""
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(499, 12)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 20)
        Me.Label9.TabIndex = 349
        Me.Label9.Text = "Destino*"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Blue
        Me.Label20.Location = New System.Drawing.Point(1116, 80)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(0, 20)
        Me.Label20.TabIndex = 345
        '
        'btnActualizarMat
        '
        Me.btnActualizarMat.BackgroundImage = Global.SEYC.My.Resources.Resources.Sincro
        Me.btnActualizarMat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnActualizarMat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizarMat.Location = New System.Drawing.Point(636, 153)
        Me.btnActualizarMat.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnActualizarMat.Name = "btnActualizarMat"
        Me.btnActualizarMat.Size = New System.Drawing.Size(35, 26)
        Me.btnActualizarMat.TabIndex = 340
        Me.btnActualizarMat.UseVisualStyleBackColor = True
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtID.Location = New System.Drawing.Point(416, 465)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(43, 22)
        Me.txtID.TabIndex = 339
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'PicEncargado
        '
        Me.PicEncargado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicEncargado.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicEncargado.Image = Global.SEYC.My.Resources.Resources.Info
        Me.PicEncargado.Location = New System.Drawing.Point(1020, 38)
        Me.PicEncargado.Margin = New System.Windows.Forms.Padding(4)
        Me.PicEncargado.Name = "PicEncargado"
        Me.PicEncargado.Size = New System.Drawing.Size(24, 25)
        Me.PicEncargado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicEncargado.TabIndex = 338
        Me.PicEncargado.TabStop = False
        '
        'txtMarca
        '
        Me.txtMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMarca.Decimals = CType(2, Byte)
        Me.txtMarca.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMarca.Enabled = False
        Me.txtMarca.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtMarca.Location = New System.Drawing.Point(13, 468)
        Me.txtMarca.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMarca.MaxLength = 8
        Me.txtMarca.Name = "txtMarca"
        Me.txtMarca.Size = New System.Drawing.Size(47, 22)
        Me.txtMarca.TabIndex = 334
        Me.txtMarca.Text_1 = Nothing
        Me.txtMarca.Text_2 = Nothing
        Me.txtMarca.Text_3 = Nothing
        Me.txtMarca.Text_4 = Nothing
        Me.txtMarca.UserValues = Nothing
        Me.txtMarca.Visible = False
        '
        'txtUnidad
        '
        Me.txtUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUnidad.Decimals = CType(2, Byte)
        Me.txtUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtUnidad.Enabled = False
        Me.txtUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtUnidad.Location = New System.Drawing.Point(69, 468)
        Me.txtUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUnidad.MaxLength = 8
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.Size = New System.Drawing.Size(47, 22)
        Me.txtUnidad.TabIndex = 333
        Me.txtUnidad.Text_1 = Nothing
        Me.txtUnidad.Text_2 = Nothing
        Me.txtUnidad.Text_3 = Nothing
        Me.txtUnidad.Text_4 = Nothing
        Me.txtUnidad.UserValues = Nothing
        Me.txtUnidad.Visible = False
        '
        'txtIDMarca
        '
        Me.txtIDMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDMarca.Decimals = CType(2, Byte)
        Me.txtIDMarca.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDMarca.Enabled = False
        Me.txtIDMarca.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDMarca.Location = New System.Drawing.Point(124, 468)
        Me.txtIDMarca.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDMarca.MaxLength = 8
        Me.txtIDMarca.Name = "txtIDMarca"
        Me.txtIDMarca.Size = New System.Drawing.Size(47, 22)
        Me.txtIDMarca.TabIndex = 332
        Me.txtIDMarca.Text_1 = Nothing
        Me.txtIDMarca.Text_2 = Nothing
        Me.txtIDMarca.Text_3 = Nothing
        Me.txtIDMarca.Text_4 = Nothing
        Me.txtIDMarca.UserValues = Nothing
        Me.txtIDMarca.Visible = False
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Enabled = False
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdUnidad.Location = New System.Drawing.Point(179, 468)
        Me.txtIdUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUnidad.MaxLength = 8
        Me.txtIdUnidad.Name = "txtIdUnidad"
        Me.txtIdUnidad.Size = New System.Drawing.Size(44, 22)
        Me.txtIdUnidad.TabIndex = 330
        Me.txtIdUnidad.Text_1 = Nothing
        Me.txtIdUnidad.Text_2 = Nothing
        Me.txtIdUnidad.Text_3 = Nothing
        Me.txtIdUnidad.Text_4 = Nothing
        Me.txtIdUnidad.UserValues = Nothing
        Me.txtIdUnidad.Visible = False
        '
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.grdItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.OrdenItem, Me.CodMaterial, Me.EquipoHerramienta, Me.IdProducto, Me.CodMarca, Me.IdUnidad, Me.Unidad, Me.Desc_Unit, Me.Nota, Me.Stock, Me.Eliminado, Me.Eliminar})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdItems.Location = New System.Drawing.Point(16, 188)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1246, 266)
        Me.grdItems.TabIndex = 8
        '
        'lblStock
        '
        Me.lblStock.BackColor = System.Drawing.Color.Red
        Me.lblStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.ForeColor = System.Drawing.Color.White
        Me.lblStock.Location = New System.Drawing.Point(677, 154)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(61, 25)
        Me.lblStock.TabIndex = 325
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(677, 130)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 20)
        Me.Label4.TabIndex = 324
        Me.Label4.Text = "Stock"
        '
        'txtCantidad
        '
        Me.txtCantidad.AccessibleName = ""
        Me.txtCantidad.Decimals = CType(2, Byte)
        Me.txtCantidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCantidad.Location = New System.Drawing.Point(747, 154)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.MaxLength = 100
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(79, 26)
        Me.txtCantidad.TabIndex = 7
        Me.txtCantidad.Text_1 = Nothing
        Me.txtCantidad.Text_2 = Nothing
        Me.txtCantidad.Text_3 = Nothing
        Me.txtCantidad.Text_4 = Nothing
        Me.txtCantidad.UserValues = Nothing
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(747, 130)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 20)
        Me.Label16.TabIndex = 320
        Me.Label16.Text = "Cantidad"
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = ""
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(16, 151)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(612, 28)
        Me.cmbProducto.TabIndex = 6
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Blue
        Me.Label17.Location = New System.Drawing.Point(13, 127)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 20)
        Me.Label17.TabIndex = 321
        Me.Label17.Text = "Producto"
        '
        'txtIDEncargado
        '
        Me.txtIDEncargado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDEncargado.Decimals = CType(2, Byte)
        Me.txtIDEncargado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDEncargado.Enabled = False
        Me.txtIDEncargado.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDEncargado.Location = New System.Drawing.Point(983, 11)
        Me.txtIDEncargado.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDEncargado.MaxLength = 8
        Me.txtIDEncargado.Name = "txtIDEncargado"
        Me.txtIDEncargado.Size = New System.Drawing.Size(29, 22)
        Me.txtIDEncargado.TabIndex = 314
        Me.txtIDEncargado.Text_1 = Nothing
        Me.txtIDEncargado.Text_2 = Nothing
        Me.txtIDEncargado.Text_3 = Nothing
        Me.txtIDEncargado.Text_4 = Nothing
        Me.txtIDEncargado.UserValues = Nothing
        Me.txtIDEncargado.Visible = False
        '
        'cmbEncargado
        '
        Me.cmbEncargado.AccessibleName = "*Encargado"
        Me.cmbEncargado.DropDownHeight = 500
        Me.cmbEncargado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEncargado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEncargado.FormattingEnabled = True
        Me.cmbEncargado.IntegralHeight = False
        Me.cmbEncargado.Location = New System.Drawing.Point(701, 36)
        Me.cmbEncargado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEncargado.Name = "cmbEncargado"
        Me.cmbEncargado.Size = New System.Drawing.Size(311, 28)
        Me.cmbEncargado.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(699, 14)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 20)
        Me.Label10.TabIndex = 312
        Me.Label10.Text = "Encargado*"
        '
        'txtIDOrigen
        '
        Me.txtIDOrigen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDOrigen.Decimals = CType(2, Byte)
        Me.txtIDOrigen.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDOrigen.Enabled = False
        Me.txtIDOrigen.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIDOrigen.Location = New System.Drawing.Point(459, 11)
        Me.txtIDOrigen.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDOrigen.MaxLength = 8
        Me.txtIDOrigen.Name = "txtIDOrigen"
        Me.txtIDOrigen.Size = New System.Drawing.Size(29, 22)
        Me.txtIDOrigen.TabIndex = 293
        Me.txtIDOrigen.Text_1 = Nothing
        Me.txtIDOrigen.Text_2 = Nothing
        Me.txtIDOrigen.Text_3 = Nothing
        Me.txtIDOrigen.Text_4 = Nothing
        Me.txtIDOrigen.UserValues = Nothing
        Me.txtIDOrigen.Visible = False
        '
        'lblNroTrans
        '
        Me.lblNroTrans.BackColor = System.Drawing.Color.White
        Me.lblNroTrans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNroTrans.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNroTrans.Location = New System.Drawing.Point(163, 37)
        Me.lblNroTrans.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNroTrans.Name = "lblNroTrans"
        Me.lblNroTrans.Size = New System.Drawing.Size(133, 28)
        Me.lblNroTrans.TabIndex = 1
        Me.lblNroTrans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AccessibleName = ""
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(299, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 289
        Me.Label1.Text = "Origen*"
        '
        'cmbOrigen
        '
        Me.cmbOrigen.AccessibleName = "*Origen"
        Me.cmbOrigen.DropDownHeight = 500
        Me.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrigen.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrigen.FormattingEnabled = True
        Me.cmbOrigen.IntegralHeight = False
        Me.cmbOrigen.Location = New System.Drawing.Point(303, 36)
        Me.cmbOrigen.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbOrigen.Name = "cmbOrigen"
        Me.cmbOrigen.Size = New System.Drawing.Size(184, 28)
        Me.cmbOrigen.TabIndex = 2
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(1085, 465)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(85, 36)
        Me.lblTotal.TabIndex = 282
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(860, 474)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 20)
        Me.Label7.TabIndex = 283
        Me.Label7.Text = "Cant. total de productos:"
        '
        'chkGrillaInferior
        '
        Me.chkGrillaInferior.AutoSize = True
        Me.chkGrillaInferior.Location = New System.Drawing.Point(256, 625)
        Me.chkGrillaInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGrillaInferior.Name = "chkGrillaInferior"
        Me.chkGrillaInferior.Size = New System.Drawing.Size(176, 21)
        Me.chkGrillaInferior.TabIndex = 272
        Me.chkGrillaInferior.Text = "Aumentar Grilla Inferior"
        Me.chkGrillaInferior.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 628)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(129, 17)
        Me.Label19.TabIndex = 271
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(17, 92)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(669, 27)
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
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 68)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 20)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(467, 462)
        Me.btnLlenarGrilla.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLlenarGrilla.Name = "btnLlenarGrilla"
        Me.btnLlenarGrilla.Size = New System.Drawing.Size(153, 28)
        Me.btnLlenarGrilla.TabIndex = 12
        Me.btnLlenarGrilla.Text = "Llenar Grilla"
        Me.btnLlenarGrilla.UseVisualStyleBackColor = True
        Me.btnLlenarGrilla.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(157, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro. Trans."
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 84)
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
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 28)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'ContextMenuStripIVA
        '
        Me.ContextMenuStripIVA.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItemIVA})
        Me.ContextMenuStripIVA.Name = "ContextMenuStrip1"
        Me.ContextMenuStripIVA.Size = New System.Drawing.Size(170, 28)
        '
        'BorrarElItemToolStripMenuItemIVA
        '
        Me.BorrarElItemToolStripMenuItemIVA.Name = "BorrarElItemToolStripMenuItemIVA"
        Me.BorrarElItemToolStripMenuItemIVA.Size = New System.Drawing.Size(169, 24)
        Me.BorrarElItemToolStripMenuItemIVA.Text = "Borrar el Item"
        '
        'TimerDescargas
        '
        Me.TimerDescargas.Interval = 18000
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        '
        'OrdenItem
        '
        Me.OrdenItem.HeaderText = "Item"
        Me.OrdenItem.Name = "OrdenItem"
        Me.OrdenItem.ReadOnly = True
        Me.OrdenItem.Width = 70
        '
        'CodMaterial
        '
        Me.CodMaterial.HeaderText = "CodMaterial"
        Me.CodMaterial.Name = "CodMaterial"
        Me.CodMaterial.ReadOnly = True
        Me.CodMaterial.Visible = False
        '
        'EquipoHerramienta
        '
        Me.EquipoHerramienta.HeaderText = "Producto"
        Me.EquipoHerramienta.Name = "EquipoHerramienta"
        Me.EquipoHerramienta.ReadOnly = True
        Me.EquipoHerramienta.Width = 280
        '
        'IdProducto
        '
        Me.IdProducto.HeaderText = "CodMarca"
        Me.IdProducto.Name = "IdProducto"
        Me.IdProducto.ReadOnly = True
        Me.IdProducto.Visible = False
        '
        'CodMarca
        '
        Me.CodMarca.HeaderText = "Marca"
        Me.CodMarca.Name = "CodMarca"
        Me.CodMarca.ReadOnly = True
        Me.CodMarca.Width = 130
        '
        'IdUnidad
        '
        Me.IdUnidad.HeaderText = "CodUnidad"
        Me.IdUnidad.Name = "IdUnidad"
        Me.IdUnidad.ReadOnly = True
        Me.IdUnidad.Visible = False
        '
        'Unidad
        '
        Me.Unidad.HeaderText = "Unidad"
        Me.Unidad.Name = "Unidad"
        Me.Unidad.ReadOnly = True
        Me.Unidad.Width = 80
        '
        'Desc_Unit
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.Desc_Unit.DefaultCellStyle = DataGridViewCellStyle3
        Me.Desc_Unit.HeaderText = "Cantidad"
        Me.Desc_Unit.Name = "Desc_Unit"
        Me.Desc_Unit.ReadOnly = True
        Me.Desc_Unit.Width = 70
        '
        'Nota
        '
        Me.Nota.HeaderText = "Nota"
        Me.Nota.Name = "Nota"
        Me.Nota.Width = 180
        '
        'Stock
        '
        Me.Stock.HeaderText = "Stock"
        Me.Stock.Name = "Stock"
        Me.Stock.ReadOnly = True
        Me.Stock.Visible = False
        Me.Stock.Width = 50
        '
        'Eliminado
        '
        Me.Eliminado.HeaderText = "Eliminado"
        Me.Eliminado.Name = "Eliminado"
        Me.Eliminado.ReadOnly = True
        Me.Eliminado.Visible = False
        Me.Eliminado.Width = 50
        '
        'Eliminar
        '
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.ReadOnly = True
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.ToolTipText = "Eliminar Registro"
        Me.Eliminar.UseColumnTextForButtonValue = True
        Me.Eliminar.Width = 80
        '
        'frmTransferenciasPorkys
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 750)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmTransferenciasPorkys"
        Me.Text = "frmTransferencias"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicEncargado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStripIVA.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStripIVA As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItemIVA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbOrigen As System.Windows.Forms.ComboBox
    Friend WithEvents txtIDOrigen As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIDEncargado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents TimerDescargas As System.Windows.Forms.Timer
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMarca As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIDMarca As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PicEncargado As System.Windows.Forms.PictureBox
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents lblTotal As System.Windows.Forms.Label
    Public WithEvents lblNroTrans As System.Windows.Forms.Label
    Public WithEvents cmbEncargado As System.Windows.Forms.ComboBox
    Friend WithEvents btnActualizarMat As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbDestino As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtIDDestino As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkEliminados As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblAutorizado As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrdenItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodMaterial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EquipoHerramienta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodMarca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Desc_Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nota As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewButtonColumn
End Class
