<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBusquedaCliente
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBusquedaCliente))
        Me.grdCliente = New System.Windows.Forms.DataGridView()
        Me.txtClienteID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtClienteB = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtClienteNroDocB = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblClienteFiltrar = New System.Windows.Forms.Label()
        Me.btnClienteSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnClienteCargar = New DevComponents.DotNetBar.ButtonX()
        Me.PanelClienteBusqueda = New System.Windows.Forms.Panel()
        Me.lblClienteCuitB = New System.Windows.Forms.Label()
        Me.lblClienteNombreB = New System.Windows.Forms.Label()
        Me.btnClienteNuevo = New DevComponents.DotNetBar.ButtonX()
        CType(Me.grdCliente,System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanelClienteBusqueda.SuspendLayout
        Me.SuspendLayout
        '
        'grdCliente
        '
        Me.grdCliente.AllowUserToAddRows = false
        Me.grdCliente.AllowUserToDeleteRows = false
        Me.grdCliente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCliente.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        Me.grdCliente.Size = New System.Drawing.Size(1001, 343)
        Me.grdCliente.TabIndex = 1
        '
        'txtClienteID
        '
        Me.txtClienteID.Decimals = CType(2, Byte)
        Me.txtClienteID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtClienteID.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtClienteID.Location = New System.Drawing.Point(843, 7)
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
        'txtClienteB
        '
        Me.txtClienteB.BackColor = System.Drawing.Color.White
        Me.txtClienteB.Decimals = CType(2, Byte)
        Me.txtClienteB.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtClienteB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClienteB.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtClienteB.Location = New System.Drawing.Point(85, 10)
        Me.txtClienteB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClienteB.Name = "txtClienteB"
        Me.txtClienteB.Size = New System.Drawing.Size(303, 29)
        Me.txtClienteB.TabIndex = 0
        Me.txtClienteB.Text_1 = Nothing
        Me.txtClienteB.Text_2 = Nothing
        Me.txtClienteB.Text_3 = Nothing
        Me.txtClienteB.Text_4 = Nothing
        Me.txtClienteB.UserValues = Nothing
        '
        'txtClienteNroDocB
        '
        Me.txtClienteNroDocB.BackColor = System.Drawing.Color.White
        Me.txtClienteNroDocB.Decimals = CType(2, Byte)
        Me.txtClienteNroDocB.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtClienteNroDocB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClienteNroDocB.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtClienteNroDocB.Location = New System.Drawing.Point(661, 10)
        Me.txtClienteNroDocB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClienteNroDocB.Name = "txtClienteNroDocB"
        Me.txtClienteNroDocB.Size = New System.Drawing.Size(319, 29)
        Me.txtClienteNroDocB.TabIndex = 1
        Me.txtClienteNroDocB.Text_1 = Nothing
        Me.txtClienteNroDocB.Text_2 = Nothing
        Me.txtClienteNroDocB.Text_3 = Nothing
        Me.txtClienteNroDocB.Text_4 = Nothing
        Me.txtClienteNroDocB.UserValues = Nothing
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
        Me.btnClienteSalir.Location = New System.Drawing.Point(16, 458)
        Me.btnClienteSalir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClienteSalir.Name = "btnClienteSalir"
        Me.btnClienteSalir.Size = New System.Drawing.Size(201, 38)
        Me.btnClienteSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClienteSalir.Symbol = ""
        Me.btnClienteSalir.SymbolColor = System.Drawing.Color.Red
        Me.btnClienteSalir.TabIndex = 3
        Me.btnClienteSalir.Text = "Salir [ESC]"
        '
        'btnClienteCargar
        '
        Me.btnClienteCargar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClienteCargar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClienteCargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClienteCargar.Location = New System.Drawing.Point(816, 458)
        Me.btnClienteCargar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClienteCargar.Name = "btnClienteCargar"
        Me.btnClienteCargar.Size = New System.Drawing.Size(201, 38)
        Me.btnClienteCargar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnClienteCargar.Symbol = ""
        Me.btnClienteCargar.SymbolColor = System.Drawing.SystemColors.InfoText
        Me.btnClienteCargar.TabIndex = 2
        Me.btnClienteCargar.Text = "Cargar [ENTER]"
        Me.btnClienteCargar.TextColor = System.Drawing.Color.Black
        '
        'PanelClienteBusqueda
        '
        Me.PanelClienteBusqueda.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelClienteBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelClienteBusqueda.Controls.Add(Me.lblClienteCuitB)
        Me.PanelClienteBusqueda.Controls.Add(Me.lblClienteNombreB)
        Me.PanelClienteBusqueda.Controls.Add(Me.txtClienteB)
        Me.PanelClienteBusqueda.Controls.Add(Me.txtClienteNroDocB)
        Me.PanelClienteBusqueda.Location = New System.Drawing.Point(16, 38)
        Me.PanelClienteBusqueda.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelClienteBusqueda.Name = "PanelClienteBusqueda"
        Me.PanelClienteBusqueda.Size = New System.Drawing.Size(1001, 50)
        Me.PanelClienteBusqueda.TabIndex = 0
        '
        'lblClienteCuitB
        '
        Me.lblClienteCuitB.AutoSize = True
        Me.lblClienteCuitB.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteCuitB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteCuitB.ForeColor = System.Drawing.Color.White
        Me.lblClienteCuitB.Location = New System.Drawing.Point(541, 15)
        Me.lblClienteCuitB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteCuitB.Name = "lblClienteCuitB"
        Me.lblClienteCuitB.Size = New System.Drawing.Size(101, 20)
        Me.lblClienteCuitB.TabIndex = 14
        Me.lblClienteCuitB.Text = "DNI / CUIT"
        '
        'lblClienteNombreB
        '
        Me.lblClienteNombreB.AutoSize = True
        Me.lblClienteNombreB.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteNombreB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteNombreB.ForeColor = System.Drawing.Color.White
        Me.lblClienteNombreB.Location = New System.Drawing.Point(4, 15)
        Me.lblClienteNombreB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteNombreB.Name = "lblClienteNombreB"
        Me.lblClienteNombreB.Size = New System.Drawing.Size(68, 20)
        Me.lblClienteNombreB.TabIndex = 13
        Me.lblClienteNombreB.Text = "Cliente"
        '
        'btnClienteNuevo
        '
        Me.btnClienteNuevo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClienteNuevo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClienteNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClienteNuevo.Location = New System.Drawing.Point(444, 458)
        Me.btnClienteNuevo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClienteNuevo.Name = "btnClienteNuevo"
        Me.btnClienteNuevo.Size = New System.Drawing.Size(201, 38)
        Me.btnClienteNuevo.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnClienteNuevo.Symbol = "59390"
        Me.btnClienteNuevo.SymbolColor = System.Drawing.Color.Green
        Me.btnClienteNuevo.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnClienteNuevo.TabIndex = 14
        Me.btnClienteNuevo.Text = "Nuevo [F3]"
        Me.btnClienteNuevo.Visible = False
        '
        'frmBusquedaCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1031, 501)
        Me.Controls.Add(Me.btnClienteNuevo)
        Me.Controls.Add(Me.btnClienteCargar)
        Me.Controls.Add(Me.btnClienteSalir)
        Me.Controls.Add(Me.lblClienteFiltrar)
        Me.Controls.Add(Me.txtClienteID)
        Me.Controls.Add(Me.PanelClienteBusqueda)
        Me.Controls.Add(Me.grdCliente)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1049, 548)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1049, 548)
        Me.Name = "frmBusquedaCliente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Búsqueda Clientes"
        CType(Me.grdCliente,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelClienteBusqueda.ResumeLayout(false)
        Me.PanelClienteBusqueda.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdCliente As System.Windows.Forms.DataGridView
    Friend WithEvents txtClienteID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtClienteNroDocB As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblClienteFiltrar As System.Windows.Forms.Label
    Friend WithEvents btnClienteSalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnClienteCargar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtClienteB As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PanelClienteBusqueda As System.Windows.Forms.Panel
    Friend WithEvents lblClienteCuitB As System.Windows.Forms.Label
    Friend WithEvents lblClienteNombreB As System.Windows.Forms.Label
    'Friend WithEvents IFiscalC As AxEPSON_Impresora_Fiscal.AxPrinterFiscal
    Friend WithEvents btnClienteNuevo As DevComponents.DotNetBar.ButtonX
End Class
