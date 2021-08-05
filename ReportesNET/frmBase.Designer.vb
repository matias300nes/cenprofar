<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBase
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBase))
        Me.ToolMenu = New System.Windows.Forms.ToolStrip
        Me.btnNuevo = New System.Windows.Forms.ToolStripButton
        Me.btnCancelar = New System.Windows.Forms.ToolStripButton
        Me.btnGuardar = New System.Windows.Forms.ToolStripButton
        Me.btnEliminar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnActualizar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripPagina = New System.Windows.Forms.ToolStripComboBox
        Me.btnPrimero = New System.Windows.Forms.ToolStripButton
        Me.btnAnterior = New System.Windows.Forms.ToolStripButton
        Me.btnSiguiente = New System.Windows.Forms.ToolStripButton
        Me.btnUltimo = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnExcel = New System.Windows.Forms.ToolStripButton
        Me.btnImprimir = New System.Windows.Forms.ToolStripButton
        Me.btnImportarExcel = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Status1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Status2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.RestituirLosDatosAnterioresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FiltrarporToolStrip = New System.Windows.Forms.ToolStripTextBox
        Me.QuitarElFitroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox
        Me.ToolMovilizar = New System.Windows.Forms.ToolStripMenuItem
        Me.FiltroPorToolStripMenuItem = New System.Windows.Forms.ToolStripTextBox
        Me.grd = New System.Windows.Forms.DataGridView
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ToolMenu.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolMenu
        '
        Me.ToolMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNuevo, Me.btnCancelar, Me.btnGuardar, Me.btnEliminar, Me.ToolStripSeparator1, Me.btnActualizar, Me.ToolStripSeparator2, Me.ToolStripPagina, Me.btnPrimero, Me.btnAnterior, Me.btnSiguiente, Me.btnUltimo, Me.ToolStripSeparator3, Me.btnExcel, Me.btnImprimir, Me.btnImportarExcel})
        Me.ToolMenu.Location = New System.Drawing.Point(0, 0)
        Me.ToolMenu.Name = "ToolMenu"
        Me.ToolMenu.Size = New System.Drawing.Size(765, 25)
        Me.ToolMenu.TabIndex = 2
        Me.ToolMenu.Text = "ToolStrip1"
        '
        'btnNuevo
        '
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(58, 22)
        Me.btnNuevo.Text = "&Nuevo"
        Me.btnNuevo.ToolTipText = "Agregar un nuevo registro"
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(69, 22)
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.ToolTipText = "Cancelar el agregado de un registro nuevo"
        '
        'btnGuardar
        '
        Me.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(66, 22)
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.ToolTipText = "Guardar el registro"
        '
        'btnEliminar
        '
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(63, 22)
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.ToolTipText = "Eliminar el registro actual"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnActualizar
        '
        Me.btnActualizar.Image = CType(resources.GetObject("btnActualizar.Image"), System.Drawing.Image)
        Me.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(74, 22)
        Me.btnActualizar.Text = "Actualizar"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripPagina
        '
        Me.ToolStripPagina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripPagina.Name = "ToolStripPagina"
        Me.ToolStripPagina.Size = New System.Drawing.Size(75, 25)
        Me.ToolStripPagina.ToolTipText = "Seleccione la pagina que quiere ver"
        '
        'btnPrimero
        '
        Me.btnPrimero.Image = CType(resources.GetObject("btnPrimero.Image"), System.Drawing.Image)
        Me.btnPrimero.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New System.Drawing.Size(63, 22)
        Me.btnPrimero.Text = "Primero"
        Me.btnPrimero.ToolTipText = "Ir al primer registro"
        '
        'btnAnterior
        '
        Me.btnAnterior.Image = CType(resources.GetObject("btnAnterior.Image"), System.Drawing.Image)
        Me.btnAnterior.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(66, 22)
        Me.btnAnterior.Text = "Anterior"
        Me.btnAnterior.ToolTipText = "Ir al registro anterior"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Image = CType(resources.GetObject("btnSiguiente.Image"), System.Drawing.Image)
        Me.btnSiguiente.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(71, 22)
        Me.btnSiguiente.Text = "Siguiente"
        Me.btnSiguiente.ToolTipText = "Ir al siguiente registro"
        '
        'btnUltimo
        '
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(56, 22)
        Me.btnUltimo.Text = "Último"
        Me.btnUltimo.ToolTipText = "Ir al último registro"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnExcel
        '
        Me.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(23, 22)
        Me.btnExcel.Text = "Exportar Excel"
        '
        'btnImprimir
        '
        Me.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(23, 22)
        Me.btnImprimir.Text = "Imprimir Reporte"
        '
        'btnImportarExcel
        '
        Me.btnImportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImportarExcel.Name = "btnImportarExcel"
        Me.btnImportarExcel.Size = New System.Drawing.Size(23, 22)
        Me.btnImportarExcel.Text = "Importar desde Excel"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status1, Me.Status2, Me.ToolStripDropDownButton1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 339)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip1.Size = New System.Drawing.Size(765, 22)
        Me.StatusStrip1.TabIndex = 60
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Status1
        '
        Me.Status1.Name = "Status1"
        Me.Status1.Size = New System.Drawing.Size(0, 17)
        '
        'Status2
        '
        Me.Status2.Name = "Status2"
        Me.Status2.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestituirLosDatosAnterioresToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(29, 20)
        Me.ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
        '
        'RestituirLosDatosAnterioresToolStripMenuItem
        '
        Me.RestituirLosDatosAnterioresToolStripMenuItem.Name = "RestituirLosDatosAnterioresToolStripMenuItem"
        Me.RestituirLosDatosAnterioresToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.RestituirLosDatosAnterioresToolStripMenuItem.Text = "Restituir los datos anteriores"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FToolStripMenuItem, Me.FiltroExcluyendoLaSelecciónToolStripMenuItem, Me.FiltrarporToolStrip, Me.QuitarElFitroToolStripMenuItem, Me.ToolStripTextBox1, Me.ToolMovilizar})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(285, 160)
        '
        'FToolStripMenuItem
        '
        Me.FToolStripMenuItem.Image = CType(resources.GetObject("FToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FToolStripMenuItem.Name = "FToolStripMenuItem"
        Me.FToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.FToolStripMenuItem.Text = "Filtro por selección"
        '
        'FiltroExcluyendoLaSelecciónToolStripMenuItem
        '
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Name = "FiltroExcluyendoLaSelecciónToolStripMenuItem"
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Text = "Filtro excluyendo la selección"
        '
        'FiltrarporToolStrip
        '
        Me.FiltrarporToolStrip.Name = "FiltrarporToolStrip"
        Me.FiltrarporToolStrip.Size = New System.Drawing.Size(224, 21)
        Me.FiltrarporToolStrip.Text = "Filtrar por:"
        '
        'QuitarElFitroToolStripMenuItem
        '
        Me.QuitarElFitroToolStripMenuItem.Image = CType(resources.GetObject("QuitarElFitroToolStripMenuItem.Image"), System.Drawing.Image)
        Me.QuitarElFitroToolStripMenuItem.Name = "QuitarElFitroToolStripMenuItem"
        Me.QuitarElFitroToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.QuitarElFitroToolStripMenuItem.Text = "Quitar el Fitro"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(224, 21)
        Me.ToolStripTextBox1.Text = "Inmovilizar columnas:"
        '
        'ToolMovilizar
        '
        Me.ToolMovilizar.Name = "ToolMovilizar"
        Me.ToolMovilizar.Size = New System.Drawing.Size(284, 22)
        Me.ToolMovilizar.Text = "Movilizar columnas"
        Me.ToolMovilizar.Visible = False
        '
        'FiltroPorToolStripMenuItem
        '
        Me.FiltroPorToolStripMenuItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FiltroPorToolStripMenuItem.Name = "FiltroPorToolStripMenuItem"
        Me.FiltroPorToolStripMenuItem.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.FiltroPorToolStripMenuItem.Size = New System.Drawing.Size(218, 21)
        Me.FiltroPorToolStripMenuItem.Text = "FILTRO POR:"
        '
        'grd
        '
        Me.grd.AllowUserToAddRows = False
        Me.grd.AllowUserToDeleteRows = False
        Me.grd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Location = New System.Drawing.Point(0, 99)
        Me.grd.MultiSelect = False
        Me.grd.Name = "grd"
        Me.grd.ReadOnly = True
        Me.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grd.Size = New System.Drawing.Size(765, 237)
        Me.grd.TabIndex = 62
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(765, 361)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolMenu)
        Me.Name = "frmBase"
        Me.Text = "frmBase"
        Me.ToolMenu.ResumeLayout(False)
        Me.ToolMenu.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNuevo As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnGuardar As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEliminar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrimero As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAnterior As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSiguiente As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUltimo As System.Windows.Forms.ToolStripButton
    Friend WithEvents Status1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status2 As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnActualizar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FiltroExcluyendoLaSelecciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitarElFitroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FiltroPorToolStripMenuItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents FiltrarporToolStrip As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripPagina As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExcel As System.Windows.Forms.ToolStripButton
    Public WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents RestituirLosDatosAnterioresToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnImprimir As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolMovilizar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnImportarExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
