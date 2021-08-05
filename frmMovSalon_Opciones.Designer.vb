<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovSalon_Opciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovSalon_Opciones))
        Me.btnTransferir = New DevComponents.DotNetBar.ButtonX()
        Me.btnAbrirPack = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnVolver = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnTransferir
        '
        Me.btnTransferir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnTransferir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnTransferir.Location = New System.Drawing.Point(30, 81)
        Me.btnTransferir.Name = "btnTransferir"
        Me.btnTransferir.Size = New System.Drawing.Size(253, 40)
        Me.btnTransferir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnTransferir.Symbol = ""
        Me.btnTransferir.SymbolSize = 20.0!
        Me.btnTransferir.TabIndex = 1
        Me.btnTransferir.Text = "Transferir"
        '
        'btnAbrirPack
        '
        Me.btnAbrirPack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAbrirPack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAbrirPack.Location = New System.Drawing.Point(30, 23)
        Me.btnAbrirPack.Name = "btnAbrirPack"
        Me.btnAbrirPack.Size = New System.Drawing.Size(253, 40)
        Me.btnAbrirPack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAbrirPack.Symbol = ""
        Me.btnAbrirPack.SymbolSize = 20.0!
        Me.btnAbrirPack.TabIndex = 0
        Me.btnAbrirPack.Text = "Abrir Pack"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.btnAbrirPack)
        Me.GroupPanel1.Controls.Add(Me.btnTransferir)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(14, 13)
        Me.GroupPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(319, 169)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
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
        Me.GroupPanel1.Text = "Movimientos Salón"
        '
        'btnVolver
        '
        Me.btnVolver.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnVolver.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnVolver.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVolver.Location = New System.Drawing.Point(14, 186)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(88, 40)
        Me.btnVolver.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnVolver.Symbol = ""
        Me.btnVolver.SymbolSize = 20.0!
        Me.btnVolver.TabIndex = 1
        Me.btnVolver.Text = "F12"
        '
        'frmMovSalon_Opciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 230)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximumSize = New System.Drawing.Size(364, 248)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(364, 248)
        Me.Name = "frmMovSalon_Opciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTransferir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnAbrirPack As DevComponents.DotNetBar.ButtonX
    Private WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnVolver As DevComponents.DotNetBar.ButtonX
End Class
