<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVentasWEB_PedirOrden
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVentasWEB_PedirOrden))
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNro = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPtoVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSeleccionar = New DevComponents.DotNetBar.ButtonX()
        Me.lblHora = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblNota = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblNro = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.lblRepartidor = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnConfirmar = New System.Windows.Forms.Button()
        Me.btnOmitir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lblIDCliente = New System.Windows.Forms.Label()
        Me.GroupPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupPanel1
        '
        Me.GroupPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.lblIDCliente)
        Me.GroupPanel1.Controls.Add(Me.Label11)
        Me.GroupPanel1.Controls.Add(Me.txtNro)
        Me.GroupPanel1.Controls.Add(Me.txtPtoVta)
        Me.GroupPanel1.Controls.Add(Me.Label10)
        Me.GroupPanel1.Controls.Add(Me.btnSeleccionar)
        Me.GroupPanel1.Controls.Add(Me.lblHora)
        Me.GroupPanel1.Controls.Add(Me.Label9)
        Me.GroupPanel1.Controls.Add(Me.lblFecha)
        Me.GroupPanel1.Controls.Add(Me.Label8)
        Me.GroupPanel1.Controls.Add(Me.lblNota)
        Me.GroupPanel1.Controls.Add(Me.Label7)
        Me.GroupPanel1.Controls.Add(Me.lblNro)
        Me.GroupPanel1.Controls.Add(Me.Label6)
        Me.GroupPanel1.Controls.Add(Me.lbltotal)
        Me.GroupPanel1.Controls.Add(Me.lblDescuento)
        Me.GroupPanel1.Controls.Add(Me.lblSubtotal)
        Me.GroupPanel1.Controls.Add(Me.lblRepartidor)
        Me.GroupPanel1.Controls.Add(Me.lblCliente)
        Me.GroupPanel1.Controls.Add(Me.Label5)
        Me.GroupPanel1.Controls.Add(Me.Label4)
        Me.GroupPanel1.Controls.Add(Me.Label3)
        Me.GroupPanel1.Controls.Add(Me.Label2)
        Me.GroupPanel1.Controls.Add(Me.Label1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(13, 13)
        Me.GroupPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(607, 238)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.GroupPanel1.Style.BackColor2 = System.Drawing.SystemColors.ButtonFace
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(111, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 25)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "-"
        '
        'txtNro
        '
        Me.txtNro.AccessibleName = ""
        Me.txtNro.Decimals = CType(2, Byte)
        Me.txtNro.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNro.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNro.Location = New System.Drawing.Point(135, 34)
        Me.txtNro.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNro.MaxLength = 100
        Me.txtNro.Name = "txtNro"
        Me.txtNro.Size = New System.Drawing.Size(136, 30)
        Me.txtNro.TabIndex = 1
        Me.txtNro.Text_1 = Nothing
        Me.txtNro.Text_2 = Nothing
        Me.txtNro.Text_3 = Nothing
        Me.txtNro.Text_4 = Nothing
        Me.txtNro.UserValues = Nothing
        '
        'txtPtoVta
        '
        Me.txtPtoVta.AccessibleName = ""
        Me.txtPtoVta.Decimals = CType(2, Byte)
        Me.txtPtoVta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPtoVta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPtoVta.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtPtoVta.Location = New System.Drawing.Point(27, 34)
        Me.txtPtoVta.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPtoVta.MaxLength = 100
        Me.txtPtoVta.Name = "txtPtoVta"
        Me.txtPtoVta.Size = New System.Drawing.Size(79, 30)
        Me.txtPtoVta.TabIndex = 0
        Me.txtPtoVta.Text_1 = Nothing
        Me.txtPtoVta.Text_2 = Nothing
        Me.txtPtoVta.Text_3 = Nothing
        Me.txtPtoVta.Text_4 = Nothing
        Me.txtPtoVta.UserValues = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(23, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(213, 20)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Escriba el nro. de la venta :"
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSeleccionar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSeleccionar.Location = New System.Drawing.Point(278, 34)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(97, 32)
        Me.btnSeleccionar.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005
        Me.btnSeleccionar.TabIndex = 2
        Me.btnSeleccionar.Text = "Seleccionar"
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHora.Location = New System.Drawing.Point(94, 112)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(50, 20)
        Me.lblHora.TabIndex = 18
        Me.lblHora.Text = "Hora"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(23, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 20)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Hora :"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFecha.Location = New System.Drawing.Point(94, 92)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(60, 20)
        Me.lblFecha.TabIndex = 16
        Me.lblFecha.Text = "Fecha"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(23, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 20)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Fecha :"
        '
        'lblNota
        '
        Me.lblNota.AutoSize = True
        Me.lblNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNota.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNota.Location = New System.Drawing.Point(83, 173)
        Me.lblNota.Name = "lblNota"
        Me.lblNota.Size = New System.Drawing.Size(48, 20)
        Me.lblNota.TabIndex = 13
        Me.lblNota.Text = "Nota"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(23, 173)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 20)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Nota :"
        '
        'lblNro
        '
        Me.lblNro.AutoSize = True
        Me.lblNro.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNro.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNro.Location = New System.Drawing.Point(83, 72)
        Me.lblNro.Name = "lblNro"
        Me.lblNro.Size = New System.Drawing.Size(74, 20)
        Me.lblNro.TabIndex = 11
        Me.lblNro.Text = "Numero"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(23, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Nro. :"
        '
        'lbltotal
        '
        Me.lbltotal.AutoSize = True
        Me.lbltotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbltotal.Location = New System.Drawing.Point(505, 202)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(51, 20)
        Me.lbltotal.TabIndex = 9
        Me.lbltotal.Text = "Total"
        '
        'lblDescuento
        '
        Me.lblDescuento.AutoSize = True
        Me.lblDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescuento.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescuento.Location = New System.Drawing.Point(331, 202)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(99, 20)
        Me.lblDescuento.TabIndex = 8
        Me.lblDescuento.Text = "Descuento"
        '
        'lblSubtotal
        '
        Me.lblSubtotal.AutoSize = True
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSubtotal.Location = New System.Drawing.Point(103, 202)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(78, 20)
        Me.lblSubtotal.TabIndex = 7
        Me.lblSubtotal.Text = "Subtotal"
        '
        'lblRepartidor
        '
        Me.lblRepartidor.AutoSize = True
        Me.lblRepartidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRepartidor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRepartidor.Location = New System.Drawing.Point(125, 153)
        Me.lblRepartidor.Name = "lblRepartidor"
        Me.lblRepartidor.Size = New System.Drawing.Size(97, 20)
        Me.lblRepartidor.TabIndex = 6
        Me.lblRepartidor.Text = "Repartidor"
        '
        'lblCliente
        '
        Me.lblCliente.AutoSize = True
        Me.lblCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCliente.Location = New System.Drawing.Point(104, 133)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(74, 20)
        Me.lblCliente.TabIndex = 5
        Me.lblCliente.Text = "Cliente "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(208, 202)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Descuento G.:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(22, 202)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Subtotal:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(448, 202)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Total:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(23, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Repartidor:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(23, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cliente :"
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirmar.Location = New System.Drawing.Point(515, 265)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(105, 33)
        Me.btnConfirmar.TabIndex = 2
        Me.btnConfirmar.Text = "Confirmar"
        Me.btnConfirmar.UseVisualStyleBackColor = True
        '
        'btnOmitir
        '
        Me.btnOmitir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOmitir.Location = New System.Drawing.Point(404, 265)
        Me.btnOmitir.Name = "btnOmitir"
        Me.btnOmitir.Size = New System.Drawing.Size(105, 33)
        Me.btnOmitir.TabIndex = 1
        Me.btnOmitir.Text = "Omitir"
        Me.btnOmitir.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Location = New System.Drawing.Point(293, 265)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(105, 33)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'lblIDCliente
        '
        Me.lblIDCliente.AutoSize = True
        Me.lblIDCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDCliente.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIDCliente.Location = New System.Drawing.Point(495, 133)
        Me.lblIDCliente.Name = "lblIDCliente"
        Me.lblIDCliente.Size = New System.Drawing.Size(95, 20)
        Me.lblIDCliente.TabIndex = 24
        Me.lblIDCliente.Text = "lblIDCliente"
        Me.lblIDCliente.Visible = False
        '
        'frmVentasWEB_PedirOrden
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 310)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnOmitir)
        Me.Controls.Add(Me.btnConfirmar)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(651, 328)
        Me.MinimumSize = New System.Drawing.Size(651, 328)
        Me.Name = "frmVentasWEB_PedirOrden"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents lblNro As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblRepartidor As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNota As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOmitir As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSeleccionar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblHora As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNro As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPtoVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblIDCliente As System.Windows.Forms.Label
End Class
