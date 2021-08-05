<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActuliazarSistema
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmActuliazarSistema))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.btnTransferencia = New DevComponents.DotNetBar.ButtonX()
        Me.btnRecepciones = New DevComponents.DotNetBar.ButtonX()
        Me.btnMateriales = New DevComponents.DotNetBar.ButtonX()
        Me.btnListaPrecios = New DevComponents.DotNetBar.ButtonX()
        Me.btnStock = New DevComponents.DotNetBar.ButtonX()
        Me.btnClientes = New DevComponents.DotNetBar.ButtonX()
        Me.btnEmpleados = New DevComponents.DotNetBar.ButtonX()
        Me.btnActualizarSistema = New DevComponents.DotNetBar.ButtonX()
        Me.btnDescargarAjuste = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1200000
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(153, 12)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(104, 23)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "Recepciones"
        '
        'LabelX2
        '
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(274, 12)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(89, 23)
        Me.LabelX2.TabIndex = 2
        Me.LabelX2.Text = "Materiales"
        '
        'LabelX3
        '
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(22, 96)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(125, 23)
        Me.LabelX3.TabIndex = 4
        Me.LabelX3.Text = "Lista de Precios"
        '
        'LabelX5
        '
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(275, 96)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(96, 23)
        Me.LabelX5.TabIndex = 14
        Me.LabelX5.Text = "Empleados "
        '
        'LabelX6
        '
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(160, 96)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(101, 23)
        Me.LabelX6.TabIndex = 12
        Me.LabelX6.Text = "Clientes"
        '
        'LabelX9
        '
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.Location = New System.Drawing.Point(22, 12)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(114, 23)
        Me.LabelX9.TabIndex = 16
        Me.LabelX9.Text = "Transferencias"
        '
        'btnTransferencia
        '
        Me.btnTransferencia.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnTransferencia.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnTransferencia.Location = New System.Drawing.Point(53, 48)
        Me.btnTransferencia.Name = "btnTransferencia"
        Me.btnTransferencia.Size = New System.Drawing.Size(35, 34)
        Me.btnTransferencia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnTransferencia.Symbol = ""
        Me.btnTransferencia.SymbolSize = 12.0!
        Me.btnTransferencia.TabIndex = 20
        '
        'btnRecepciones
        '
        Me.btnRecepciones.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRecepciones.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRecepciones.Location = New System.Drawing.Point(173, 48)
        Me.btnRecepciones.Name = "btnRecepciones"
        Me.btnRecepciones.Size = New System.Drawing.Size(35, 34)
        Me.btnRecepciones.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnRecepciones.Symbol = ""
        Me.btnRecepciones.SymbolSize = 12.0!
        Me.btnRecepciones.TabIndex = 21
        '
        'btnMateriales
        '
        Me.btnMateriales.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnMateriales.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnMateriales.Location = New System.Drawing.Point(293, 48)
        Me.btnMateriales.Name = "btnMateriales"
        Me.btnMateriales.Size = New System.Drawing.Size(35, 34)
        Me.btnMateriales.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnMateriales.Symbol = ""
        Me.btnMateriales.SymbolSize = 12.0!
        Me.btnMateriales.TabIndex = 22
        '
        'btnListaPrecios
        '
        Me.btnListaPrecios.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnListaPrecios.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnListaPrecios.Location = New System.Drawing.Point(53, 133)
        Me.btnListaPrecios.Name = "btnListaPrecios"
        Me.btnListaPrecios.Size = New System.Drawing.Size(35, 34)
        Me.btnListaPrecios.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnListaPrecios.Symbol = ""
        Me.btnListaPrecios.SymbolSize = 12.0!
        Me.btnListaPrecios.TabIndex = 23
        '
        'btnStock
        '
        Me.btnStock.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnStock.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnStock.Location = New System.Drawing.Point(390, 37)
        Me.btnStock.Name = "btnStock"
        Me.btnStock.Size = New System.Drawing.Size(120, 45)
        Me.btnStock.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnStock.TabIndex = 25
        Me.btnStock.Text = "Stock"
        '
        'btnClientes
        '
        Me.btnClientes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClientes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClientes.Location = New System.Drawing.Point(174, 133)
        Me.btnClientes.Name = "btnClientes"
        Me.btnClientes.Size = New System.Drawing.Size(35, 34)
        Me.btnClientes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClientes.Symbol = ""
        Me.btnClientes.SymbolSize = 12.0!
        Me.btnClientes.TabIndex = 28
        '
        'btnEmpleados
        '
        Me.btnEmpleados.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEmpleados.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEmpleados.Location = New System.Drawing.Point(295, 133)
        Me.btnEmpleados.Name = "btnEmpleados"
        Me.btnEmpleados.Size = New System.Drawing.Size(35, 34)
        Me.btnEmpleados.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnEmpleados.Symbol = ""
        Me.btnEmpleados.SymbolSize = 12.0!
        Me.btnEmpleados.TabIndex = 29
        '
        'btnActualizarSistema
        '
        Me.btnActualizarSistema.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnActualizarSistema.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnActualizarSistema.Location = New System.Drawing.Point(390, 96)
        Me.btnActualizarSistema.Name = "btnActualizarSistema"
        Me.btnActualizarSistema.Size = New System.Drawing.Size(120, 23)
        Me.btnActualizarSistema.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnActualizarSistema.TabIndex = 30
        Me.btnActualizarSistema.Text = "Actualizar Sistema"
        Me.btnActualizarSistema.Visible = False
        '
        'btnDescargarAjuste
        '
        Me.btnDescargarAjuste.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDescargarAjuste.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDescargarAjuste.Location = New System.Drawing.Point(390, 134)
        Me.btnDescargarAjuste.Name = "btnDescargarAjuste"
        Me.btnDescargarAjuste.Size = New System.Drawing.Size(120, 45)
        Me.btnDescargarAjuste.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnDescargarAjuste.TabIndex = 31
        Me.btnDescargarAjuste.Text = "Ajustes de Stock"
        '
        'frmActuliazarSistema
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 191)
        Me.Controls.Add(Me.btnDescargarAjuste)
        Me.Controls.Add(Me.btnActualizarSistema)
        Me.Controls.Add(Me.btnEmpleados)
        Me.Controls.Add(Me.btnClientes)
        Me.Controls.Add(Me.btnStock)
        Me.Controls.Add(Me.btnListaPrecios)
        Me.Controls.Add(Me.btnMateriales)
        Me.Controls.Add(Me.btnRecepciones)
        Me.Controls.Add(Me.btnTransferencia)
        Me.Controls.Add(Me.LabelX9)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.LabelX6)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(553, 238)
        Me.Name = "frmActuliazarSistema"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notificaciones WEB"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnStock As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnActualizarSistema As DevComponents.DotNetBar.ButtonX
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents btnTransferencia As DevComponents.DotNetBar.ButtonX
    Public WithEvents btnRecepciones As DevComponents.DotNetBar.ButtonX
    Public WithEvents btnMateriales As DevComponents.DotNetBar.ButtonX
    Public WithEvents btnListaPrecios As DevComponents.DotNetBar.ButtonX
    Public WithEvents btnClientes As DevComponents.DotNetBar.ButtonX
    Public WithEvents btnEmpleados As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnDescargarAjuste As DevComponents.DotNetBar.ButtonX
End Class
