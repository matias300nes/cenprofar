<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbriPack
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
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCantidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblProductoUnitario = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtIdProductoUnitario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnAbrirPack = New DevComponents.DotNetBar.ButtonX()
        Me.btnCancelar = New DevComponents.DotNetBar.ButtonX()
        Me.txtStockUnitario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtAlmacenUnitario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtUnidadUnitario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUnidades = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblUnidadesNew = New System.Windows.Forms.Label()
        Me.txtUnidades = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.SuspendLayout()
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = ""
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(199, 46)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(416, 28)
        Me.cmbProducto.TabIndex = 0
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.Location = New System.Drawing.Point(45, 49)
        Me.UsernameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(131, 20)
        Me.UsernameLabel.TabIndex = 2
        Me.UsernameLabel.Text = "Producto Pack"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStock
        '
        Me.lblStock.BackColor = System.Drawing.Color.Red
        Me.lblStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.ForeColor = System.Drawing.Color.White
        Me.lblStock.Location = New System.Drawing.Point(682, 50)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(71, 25)
        Me.lblStock.TabIndex = 258
        Me.lblStock.Text = "Stock"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(624, 52)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 20)
        Me.Label11.TabIndex = 257
        Me.Label11.Text = "Stock"
        '
        'txtCantidad
        '
        Me.txtCantidad.AccessibleName = ""
        Me.txtCantidad.Decimals = CType(2, Byte)
        Me.txtCantidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCantidad.Location = New System.Drawing.Point(857, 49)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.MaxLength = 100
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(79, 26)
        Me.txtCantidad.TabIndex = 1
        Me.txtCantidad.Text_1 = Nothing
        Me.txtCantidad.Text_2 = Nothing
        Me.txtCantidad.Text_3 = Nothing
        Me.txtCantidad.Text_4 = Nothing
        Me.txtCantidad.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(760, 52)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 18)
        Me.Label9.TabIndex = 256
        Me.Label9.Text = "Cant. Pack"
        '
        'lblProductoUnitario
        '
        Me.lblProductoUnitario.BackColor = System.Drawing.Color.Transparent
        Me.lblProductoUnitario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductoUnitario.ForeColor = System.Drawing.Color.Blue
        Me.lblProductoUnitario.Location = New System.Drawing.Point(195, 11)
        Me.lblProductoUnitario.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProductoUnitario.Name = "lblProductoUnitario"
        Me.lblProductoUnitario.Size = New System.Drawing.Size(324, 25)
        Me.lblProductoUnitario.TabIndex = 260
        Me.lblProductoUnitario.Text = "Producto"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(39, 11)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(139, 20)
        Me.Label10.TabIndex = 259
        Me.Label10.Text = "Producto Unitario"
        '
        'txtIdProductoUnitario
        '
        Me.txtIdProductoUnitario.AccessibleName = ""
        Me.txtIdProductoUnitario.Decimals = CType(2, Byte)
        Me.txtIdProductoUnitario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProductoUnitario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdProductoUnitario.Location = New System.Drawing.Point(49, 118)
        Me.txtIdProductoUnitario.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdProductoUnitario.MaxLength = 100
        Me.txtIdProductoUnitario.Name = "txtIdProductoUnitario"
        Me.txtIdProductoUnitario.Size = New System.Drawing.Size(52, 22)
        Me.txtIdProductoUnitario.TabIndex = 261
        Me.txtIdProductoUnitario.Text_1 = Nothing
        Me.txtIdProductoUnitario.Text_2 = Nothing
        Me.txtIdProductoUnitario.Text_3 = Nothing
        Me.txtIdProductoUnitario.Text_4 = Nothing
        Me.txtIdProductoUnitario.UserValues = Nothing
        Me.txtIdProductoUnitario.Visible = False
        '
        'btnAbrirPack
        '
        Me.btnAbrirPack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAbrirPack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAbrirPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbrirPack.Location = New System.Drawing.Point(628, 118)
        Me.btnAbrirPack.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAbrirPack.Name = "btnAbrirPack"
        Me.btnAbrirPack.Size = New System.Drawing.Size(145, 28)
        Me.btnAbrirPack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAbrirPack.TabIndex = 2
        Me.btnAbrirPack.Text = "Abrir Pack"
        '
        'btnCancelar
        '
        Me.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Location = New System.Drawing.Point(792, 118)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(145, 28)
        Me.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "Cancelar"
        '
        'txtStockUnitario
        '
        Me.txtStockUnitario.AccessibleName = ""
        Me.txtStockUnitario.Decimals = CType(2, Byte)
        Me.txtStockUnitario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtStockUnitario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtStockUnitario.Location = New System.Drawing.Point(109, 118)
        Me.txtStockUnitario.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStockUnitario.MaxLength = 100
        Me.txtStockUnitario.Name = "txtStockUnitario"
        Me.txtStockUnitario.Size = New System.Drawing.Size(52, 22)
        Me.txtStockUnitario.TabIndex = 264
        Me.txtStockUnitario.Text_1 = Nothing
        Me.txtStockUnitario.Text_2 = Nothing
        Me.txtStockUnitario.Text_3 = Nothing
        Me.txtStockUnitario.Text_4 = Nothing
        Me.txtStockUnitario.UserValues = Nothing
        Me.txtStockUnitario.Visible = False
        '
        'txtAlmacenUnitario
        '
        Me.txtAlmacenUnitario.AccessibleName = ""
        Me.txtAlmacenUnitario.Decimals = CType(2, Byte)
        Me.txtAlmacenUnitario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtAlmacenUnitario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtAlmacenUnitario.Location = New System.Drawing.Point(169, 118)
        Me.txtAlmacenUnitario.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAlmacenUnitario.MaxLength = 100
        Me.txtAlmacenUnitario.Name = "txtAlmacenUnitario"
        Me.txtAlmacenUnitario.Size = New System.Drawing.Size(52, 22)
        Me.txtAlmacenUnitario.TabIndex = 265
        Me.txtAlmacenUnitario.Text_1 = Nothing
        Me.txtAlmacenUnitario.Text_2 = Nothing
        Me.txtAlmacenUnitario.Text_3 = Nothing
        Me.txtAlmacenUnitario.Text_4 = Nothing
        Me.txtAlmacenUnitario.UserValues = Nothing
        Me.txtAlmacenUnitario.Visible = False
        '
        'txtUnidadUnitario
        '
        Me.txtUnidadUnitario.AccessibleName = ""
        Me.txtUnidadUnitario.Decimals = CType(2, Byte)
        Me.txtUnidadUnitario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtUnidadUnitario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtUnidadUnitario.Location = New System.Drawing.Point(229, 118)
        Me.txtUnidadUnitario.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUnidadUnitario.MaxLength = 100
        Me.txtUnidadUnitario.Name = "txtUnidadUnitario"
        Me.txtUnidadUnitario.Size = New System.Drawing.Size(52, 22)
        Me.txtUnidadUnitario.TabIndex = 266
        Me.txtUnidadUnitario.Text_1 = Nothing
        Me.txtUnidadUnitario.Text_2 = Nothing
        Me.txtUnidadUnitario.Text_3 = Nothing
        Me.txtUnidadUnitario.Text_4 = Nothing
        Me.txtUnidadUnitario.UserValues = Nothing
        Me.txtUnidadUnitario.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(760, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 17)
        Me.Label1.TabIndex = 267
        Me.Label1.Text = "U/Pack"
        '
        'lblUnidades
        '
        Me.lblUnidades.AutoSize = True
        Me.lblUnidades.BackColor = System.Drawing.Color.Transparent
        Me.lblUnidades.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidades.ForeColor = System.Drawing.Color.Blue
        Me.lblUnidades.Location = New System.Drawing.Point(853, 16)
        Me.lblUnidades.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUnidades.Name = "lblUnidades"
        Me.lblUnidades.Size = New System.Drawing.Size(87, 20)
        Me.lblUnidades.TabIndex = 268
        Me.lblUnidades.Text = "Unidades"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Green
        Me.Label2.Location = New System.Drawing.Point(666, 84)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(184, 20)
        Me.Label2.TabIndex = 269
        Me.Label2.Text = "Cant. nueva del unitario"
        '
        'lblUnidadesNew
        '
        Me.lblUnidadesNew.AutoSize = True
        Me.lblUnidadesNew.BackColor = System.Drawing.Color.Transparent
        Me.lblUnidadesNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidadesNew.ForeColor = System.Drawing.Color.Green
        Me.lblUnidadesNew.Location = New System.Drawing.Point(872, 84)
        Me.lblUnidadesNew.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUnidadesNew.Name = "lblUnidadesNew"
        Me.lblUnidadesNew.Size = New System.Drawing.Size(44, 20)
        Me.lblUnidadesNew.TabIndex = 270
        Me.lblUnidadesNew.Text = "0.00"
        '
        'txtUnidades
        '
        Me.txtUnidades.AccessibleName = ""
        Me.txtUnidades.Decimals = CType(2, Byte)
        Me.txtUnidades.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtUnidades.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnidades.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtUnidades.Location = New System.Drawing.Point(857, 10)
        Me.txtUnidades.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUnidades.MaxLength = 100
        Me.txtUnidades.Name = "txtUnidades"
        Me.txtUnidades.Size = New System.Drawing.Size(79, 26)
        Me.txtUnidades.TabIndex = 271
        Me.txtUnidades.Text_1 = Nothing
        Me.txtUnidades.Text_2 = Nothing
        Me.txtUnidades.Text_3 = Nothing
        Me.txtUnidades.Text_4 = Nothing
        Me.txtUnidades.UserValues = Nothing
        Me.txtUnidades.Visible = False
        '
        'frmAbriPack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 161)
        Me.Controls.Add(Me.lblUnidadesNew)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblUnidades)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUnidadUnitario)
        Me.Controls.Add(Me.txtAlmacenUnitario)
        Me.Controls.Add(Me.txtStockUnitario)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAbrirPack)
        Me.Controls.Add(Me.txtIdProductoUnitario)
        Me.Controls.Add(Me.lblProductoUnitario)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblStock)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbProducto)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.txtUnidades)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbriPack"
        Me.Text = "Abrir PACK"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblProductoUnitario As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtIdProductoUnitario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnAbrirPack As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCancelar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtStockUnitario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtAlmacenUnitario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtUnidadUnitario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUnidades As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblUnidadesNew As System.Windows.Forms.Label
    Friend WithEvents txtUnidades As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
