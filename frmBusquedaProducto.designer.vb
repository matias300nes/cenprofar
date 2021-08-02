<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBusquedaProducto
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBusquedaProducto))
        Me.grdCliente = New System.Windows.Forms.DataGridView()
        Me.txtClienteID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNombre = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblClienteFiltrar = New System.Windows.Forms.Label()
        Me.btnClienteSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnCargar = New DevComponents.DotNetBar.ButtonX()
        Me.PanelClienteBusqueda = New System.Windows.Forms.Panel()
        Me.txtCodBarra = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkCodBarra = New System.Windows.Forms.CheckBox()
        Me.lblClienteNombreB = New System.Windows.Forms.Label()
        CType(Me.grdCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelClienteBusqueda.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdCliente
        '
        Me.grdCliente.AllowUserToAddRows = False
        Me.grdCliente.AllowUserToDeleteRows = False
        Me.grdCliente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCliente.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdCliente.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdCliente.Location = New System.Drawing.Point(16, 107)
        Me.grdCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.grdCliente.MultiSelect = False
        Me.grdCliente.Name = "grdCliente"
        Me.grdCliente.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCliente.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCliente.Size = New System.Drawing.Size(929, 343)
        Me.grdCliente.TabIndex = 1
        '
        'txtClienteID
        '
        Me.txtClienteID.Decimals = CType(2, Byte)
        Me.txtClienteID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtClienteID.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtClienteID.Location = New System.Drawing.Point(921, 9)
        Me.txtClienteID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClienteID.Name = "txtClienteID"
        Me.txtClienteID.Size = New System.Drawing.Size(24, 22)
        Me.txtClienteID.TabIndex = 5
        Me.txtClienteID.Text_1 = Nothing
        Me.txtClienteID.Text_2 = Nothing
        Me.txtClienteID.Text_3 = Nothing
        Me.txtClienteID.Text_4 = Nothing
        Me.txtClienteID.UserValues = Nothing
        Me.txtClienteID.Visible = False
        '
        'txtNombre
        '
        Me.txtNombre.BackColor = System.Drawing.Color.White
        Me.txtNombre.Decimals = CType(2, Byte)
        Me.txtNombre.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombre.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNombre.Location = New System.Drawing.Point(112, 9)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(402, 29)
        Me.txtNombre.TabIndex = 0
        Me.txtNombre.Text_1 = Nothing
        Me.txtNombre.Text_2 = Nothing
        Me.txtNombre.Text_3 = Nothing
        Me.txtNombre.Text_4 = Nothing
        Me.txtNombre.UserValues = Nothing
        '
        'lblClienteFiltrar
        '
        Me.lblClienteFiltrar.AutoSize = True
        Me.lblClienteFiltrar.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteFiltrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteFiltrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClienteFiltrar.Location = New System.Drawing.Point(20, 9)
        Me.lblClienteFiltrar.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteFiltrar.Name = "lblClienteFiltrar"
        Me.lblClienteFiltrar.Size = New System.Drawing.Size(125, 25)
        Me.lblClienteFiltrar.TabIndex = 6
        Me.lblClienteFiltrar.Text = "Buscar Por:"
        '
        'btnClienteSalir
        '
        Me.btnClienteSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClienteSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClienteSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClienteSalir.Location = New System.Drawing.Point(16, 462)
        Me.btnClienteSalir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClienteSalir.Name = "btnClienteSalir"
        Me.btnClienteSalir.Size = New System.Drawing.Size(201, 38)
        Me.btnClienteSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClienteSalir.Symbol = ""
        Me.btnClienteSalir.SymbolColor = System.Drawing.Color.Red
        Me.btnClienteSalir.TabIndex = 3
        Me.btnClienteSalir.Text = "Salir [ESC]"
        '
        'btnCargar
        '
        Me.btnCargar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCargar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargar.Location = New System.Drawing.Point(744, 458)
        Me.btnCargar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(201, 38)
        Me.btnCargar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnCargar.Symbol = ""
        Me.btnCargar.SymbolColor = System.Drawing.SystemColors.InfoText
        Me.btnCargar.TabIndex = 2
        Me.btnCargar.Text = "Cargar [ENTER]"
        Me.btnCargar.TextColor = System.Drawing.SystemColors.ActiveCaptionText
        '
        'PanelClienteBusqueda
        '
        Me.PanelClienteBusqueda.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.PanelClienteBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelClienteBusqueda.Controls.Add(Me.txtCodBarra)
        Me.PanelClienteBusqueda.Controls.Add(Me.chkCodBarra)
        Me.PanelClienteBusqueda.Controls.Add(Me.lblClienteNombreB)
        Me.PanelClienteBusqueda.Controls.Add(Me.txtNombre)
        Me.PanelClienteBusqueda.Location = New System.Drawing.Point(16, 38)
        Me.PanelClienteBusqueda.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelClienteBusqueda.Name = "PanelClienteBusqueda"
        Me.PanelClienteBusqueda.Size = New System.Drawing.Size(929, 50)
        Me.PanelClienteBusqueda.TabIndex = 0
        '
        'txtCodBarra
        '
        Me.txtCodBarra.BackColor = System.Drawing.Color.White
        Me.txtCodBarra.Decimals = CType(2, Byte)
        Me.txtCodBarra.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCodBarra.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodBarra.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCodBarra.Location = New System.Drawing.Point(736, 10)
        Me.txtCodBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodBarra.Name = "txtCodBarra"
        Me.txtCodBarra.Size = New System.Drawing.Size(178, 29)
        Me.txtCodBarra.TabIndex = 15
        Me.txtCodBarra.Text_1 = Nothing
        Me.txtCodBarra.Text_2 = Nothing
        Me.txtCodBarra.Text_3 = Nothing
        Me.txtCodBarra.Text_4 = Nothing
        Me.txtCodBarra.UserValues = Nothing
        '
        'chkCodBarra
        '
        Me.chkCodBarra.AutoSize = True
        Me.chkCodBarra.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCodBarra.Location = New System.Drawing.Point(531, 10)
        Me.chkCodBarra.Name = "chkCodBarra"
        Me.chkCodBarra.Size = New System.Drawing.Size(198, 28)
        Me.chkCodBarra.TabIndex = 14
        Me.chkCodBarra.Text = "(F1) Mod. CodBarra"
        Me.chkCodBarra.UseVisualStyleBackColor = True
        '
        'lblClienteNombreB
        '
        Me.lblClienteNombreB.AutoSize = True
        Me.lblClienteNombreB.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteNombreB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteNombreB.ForeColor = System.Drawing.Color.Black
        Me.lblClienteNombreB.Location = New System.Drawing.Point(14, 15)
        Me.lblClienteNombreB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteNombreB.Name = "lblClienteNombreB"
        Me.lblClienteNombreB.Size = New System.Drawing.Size(74, 20)
        Me.lblClienteNombreB.TabIndex = 13
        Me.lblClienteNombreB.Text = "Nombre"
        '
        'frmBusquedaProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(958, 503)
        Me.Controls.Add(Me.btnCargar)
        Me.Controls.Add(Me.btnClienteSalir)
        Me.Controls.Add(Me.lblClienteFiltrar)
        Me.Controls.Add(Me.txtClienteID)
        Me.Controls.Add(Me.PanelClienteBusqueda)
        Me.Controls.Add(Me.grdCliente)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(976, 550)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(976, 550)
        Me.Name = "frmBusquedaProducto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Búsqueda Próductos"
        CType(Me.grdCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelClienteBusqueda.ResumeLayout(False)
        Me.PanelClienteBusqueda.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdCliente As System.Windows.Forms.DataGridView
    Friend WithEvents txtClienteID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblClienteFiltrar As System.Windows.Forms.Label
    Friend WithEvents btnClienteSalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCargar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtNombre As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PanelClienteBusqueda As System.Windows.Forms.Panel
    Friend WithEvents lblClienteNombreB As System.Windows.Forms.Label
    Friend WithEvents txtCodBarra As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkCodBarra As System.Windows.Forms.CheckBox
    'Friend WithEvents IFiscalC As AxEPSON_Impresora_Fiscal.AxPrinterFiscal
End Class
