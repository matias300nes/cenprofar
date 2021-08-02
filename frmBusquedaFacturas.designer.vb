<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBusquedaFacturas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBusquedaFacturas))
        Me.grdCliente = New System.Windows.Forms.DataGridView()
        Me.txtClienteID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtClienteB = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtClienteNroDocB = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblClienteFiltrar = New System.Windows.Forms.Label()
        Me.btnClienteSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnImprimir = New DevComponents.DotNetBar.ButtonX()
        Me.PanelClienteBusqueda = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblClienteNombreB = New System.Windows.Forms.Label()
        Me.lblClienteCuitB = New System.Windows.Forms.Label()
        Me.btnBuscar = New DevComponents.DotNetBar.ButtonX()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelClienteBusqueda.SuspendLayout()
        Me.Panel1.SuspendLayout()
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
        Me.grdCliente.Size = New System.Drawing.Size(1129, 343)
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
        Me.txtClienteB.Location = New System.Drawing.Point(105, 5)
        Me.txtClienteB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClienteB.Name = "txtClienteB"
        Me.txtClienteB.Size = New System.Drawing.Size(291, 29)
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
        Me.txtClienteNroDocB.Location = New System.Drawing.Point(513, 5)
        Me.txtClienteNroDocB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClienteNroDocB.Name = "txtClienteNroDocB"
        Me.txtClienteNroDocB.Size = New System.Drawing.Size(186, 29)
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
        Me.btnClienteSalir.Location = New System.Drawing.Point(16, 460)
        Me.btnClienteSalir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClienteSalir.Name = "btnClienteSalir"
        Me.btnClienteSalir.Size = New System.Drawing.Size(201, 38)
        Me.btnClienteSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClienteSalir.Symbol = ""
        Me.btnClienteSalir.SymbolColor = System.Drawing.Color.Red
        Me.btnClienteSalir.TabIndex = 3
        Me.btnClienteSalir.Text = "Salir [ESC]"
        '
        'btnImprimir
        '
        Me.btnImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(944, 458)
        Me.btnImprimir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(201, 38)
        Me.btnImprimir.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013
        Me.btnImprimir.Symbol = ""
        Me.btnImprimir.SymbolColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnImprimir.TabIndex = 2
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.TextColor = System.Drawing.Color.Black
        '
        'PanelClienteBusqueda
        '
        Me.PanelClienteBusqueda.BackColor = System.Drawing.Color.RoyalBlue
        Me.PanelClienteBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelClienteBusqueda.Controls.Add(Me.Panel1)
        Me.PanelClienteBusqueda.Controls.Add(Me.btnBuscar)
        Me.PanelClienteBusqueda.Controls.Add(Me.dtpFECHA)
        Me.PanelClienteBusqueda.Controls.Add(Me.Label1)
        Me.PanelClienteBusqueda.Location = New System.Drawing.Point(16, 38)
        Me.PanelClienteBusqueda.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelClienteBusqueda.Name = "PanelClienteBusqueda"
        Me.PanelClienteBusqueda.Size = New System.Drawing.Size(1129, 50)
        Me.PanelClienteBusqueda.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Panel1.Controls.Add(Me.lblClienteNombreB)
        Me.Panel1.Controls.Add(Me.txtClienteNroDocB)
        Me.Panel1.Controls.Add(Me.txtClienteB)
        Me.Panel1.Controls.Add(Me.lblClienteCuitB)
        Me.Panel1.Location = New System.Drawing.Point(400, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(716, 41)
        Me.Panel1.TabIndex = 17
        '
        'lblClienteNombreB
        '
        Me.lblClienteNombreB.AutoSize = True
        Me.lblClienteNombreB.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteNombreB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteNombreB.ForeColor = System.Drawing.Color.Black
        Me.lblClienteNombreB.Location = New System.Drawing.Point(17, 12)
        Me.lblClienteNombreB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteNombreB.Name = "lblClienteNombreB"
        Me.lblClienteNombreB.Size = New System.Drawing.Size(68, 20)
        Me.lblClienteNombreB.TabIndex = 13
        Me.lblClienteNombreB.Text = "Cliente"
        '
        'lblClienteCuitB
        '
        Me.lblClienteCuitB.AutoSize = True
        Me.lblClienteCuitB.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteCuitB.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteCuitB.ForeColor = System.Drawing.Color.Black
        Me.lblClienteCuitB.Location = New System.Drawing.Point(404, 10)
        Me.lblClienteCuitB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteCuitB.Name = "lblClienteCuitB"
        Me.lblClienteCuitB.Size = New System.Drawing.Size(101, 20)
        Me.lblClienteCuitB.TabIndex = 14
        Me.lblClienteCuitB.Text = "DNI / CUIT"
        '
        'btnBuscar
        '
        Me.btnBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBuscar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Location = New System.Drawing.Point(248, 6)
        Me.btnBuscar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(113, 38)
        Me.btnBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnBuscar.Symbol = ""
        Me.btnBuscar.SymbolColor = System.Drawing.SystemColors.InfoText
        Me.btnBuscar.SymbolSize = 15.0!
        Me.btnBuscar.TabIndex = 14
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.TextColor = System.Drawing.Color.Black
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(81, 12)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(3000, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(159, 26)
        Me.dtpFECHA.TabIndex = 16
        Me.dtpFECHA.Tag = "202"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Fecha"
        '
        'frmBusquedaFacturas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1158, 511)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnClienteSalir)
        Me.Controls.Add(Me.lblClienteFiltrar)
        Me.Controls.Add(Me.txtClienteID)
        Me.Controls.Add(Me.PanelClienteBusqueda)
        Me.Controls.Add(Me.grdCliente)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1176, 558)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1176, 558)
        Me.Name = "frmBusquedaFacturas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ver Facturas"
        CType(Me.grdCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelClienteBusqueda.ResumeLayout(False)
        Me.PanelClienteBusqueda.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdCliente As System.Windows.Forms.DataGridView
    Friend WithEvents txtClienteID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtClienteNroDocB As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblClienteFiltrar As System.Windows.Forms.Label
    Friend WithEvents btnClienteSalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnImprimir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtClienteB As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PanelClienteBusqueda As System.Windows.Forms.Panel
    Friend WithEvents lblClienteCuitB As System.Windows.Forms.Label
    Friend WithEvents lblClienteNombreB As System.Windows.Forms.Label
    'Friend WithEvents IFiscalC As AxEPSON_Impresora_Fiscal.AxPrinterFiscal
    Friend WithEvents btnBuscar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
