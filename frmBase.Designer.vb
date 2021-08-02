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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBase))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolMenu = New System.Windows.Forms.ToolStrip()
        Me.btnNuevo = New System.Windows.Forms.ToolStripButton()
        Me.btnGuardar = New System.Windows.Forms.ToolStripButton()
        Me.btnCargaContinua = New System.Windows.Forms.ToolStripButton()
        Me.btnEliminar = New System.Windows.Forms.ToolStripButton()
        Me.btnCancelar = New System.Windows.Forms.ToolStripButton()
        Me.btnActivar = New System.Windows.Forms.ToolStripButton()
        Me.btnSalir = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnActualizar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripPagina = New System.Windows.Forms.ToolStripComboBox()
        Me.btnPrimero = New System.Windows.Forms.ToolStripButton()
        Me.btnAnterior = New System.Windows.Forms.ToolStripButton()
        Me.btnSiguiente = New System.Windows.Forms.ToolStripButton()
        Me.btnUltimo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExcel = New System.Windows.Forms.ToolStripButton()
        Me.btnImportarExcel = New System.Windows.Forms.ToolStripButton()
        Me.btnImprimir = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip_lblCodMaterial = New System.Windows.Forms.ToolStripLabel()
        Me.txtBusquedaMAT = New System.Windows.Forms.ToolStripTextBox()
        Me.btnImagenExcel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip_lblCambio = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.PicConexion = New System.Windows.Forms.ToolStripLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Status1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.RestituirLosDatosAnterioresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FiltrarporToolStrip = New System.Windows.Forms.ToolStripTextBox()
        Me.QuitarElFitroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolMovilizar = New System.Windows.Forms.ToolStripMenuItem()
        Me.FiltroPorToolStripMenuItem = New System.Windows.Forms.ToolStripTextBox()
        Me.grd = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolMenu.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolMenu
        '
        Me.ToolMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNuevo, Me.btnGuardar, Me.btnCargaContinua, Me.btnEliminar, Me.btnCancelar, Me.btnActivar, Me.btnSalir, Me.ToolStripSeparator1, Me.btnActualizar, Me.ToolStripSeparator2, Me.ToolStripPagina, Me.btnPrimero, Me.btnAnterior, Me.btnSiguiente, Me.btnUltimo, Me.ToolStripSeparator3, Me.btnExcel, Me.btnImportarExcel, Me.btnImprimir, Me.ToolStrip_lblCodMaterial, Me.txtBusquedaMAT, Me.btnImagenExcel, Me.ToolStrip_lblCambio, Me.ToolStripLabel1, Me.ToolStripLabel3, Me.ToolStripLabel2, Me.PicConexion})
        Me.ToolMenu.Location = New System.Drawing.Point(0, 0)
        Me.ToolMenu.Name = "ToolMenu"
        Me.ToolMenu.Size = New System.Drawing.Size(1816, 28)
        Me.ToolMenu.TabIndex = 2
        Me.ToolMenu.Text = "ToolStrip1"
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Image)
        Me.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(106, 25)
        Me.btnNuevo.Text = "&Nuevo (F3)"
        Me.btnNuevo.ToolTipText = "Agregar un nuevo registro"
        '
        'btnGuardar
        '
        Me.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuardar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(113, 25)
        Me.btnGuardar.Text = "&Guardar(F4)"
        Me.btnGuardar.ToolTipText = "Guardar el registro"
        '
        'btnCargaContinua
        '
        Me.btnCargaContinua.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargaContinua.Image = CType(resources.GetObject("btnCargaContinua.Image"), System.Drawing.Image)
        Me.btnCargaContinua.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCargaContinua.Name = "btnCargaContinua"
        Me.btnCargaContinua.Size = New System.Drawing.Size(169, 25)
        Me.btnCargaContinua.Text = "Carga Contínua (F6)"
        Me.btnCargaContinua.Visible = False
        '
        'btnEliminar
        '
        Me.btnEliminar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(82, 25)
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.ToolTipText = "Eliminar el registro actual"
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(87, 25)
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.ToolTipText = "Cancelar el agregado de un registro nuevo"
        '
        'btnActivar
        '
        Me.btnActivar.Enabled = False
        Me.btnActivar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActivar.Image = Global.PORKYS.My.Resources.Resources.Symbol_Check
        Me.btnActivar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnActivar.Name = "btnActivar"
        Me.btnActivar.Size = New System.Drawing.Size(76, 25)
        Me.btnActivar.Text = "Activar"
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = Global.PORKYS.My.Resources.Resources._exit
        Me.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(99, 25)
        Me.btnSalir.Text = "Salir (F12)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 28)
        '
        'btnActualizar
        '
        Me.btnActualizar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizar.Image = CType(resources.GetObject("btnActualizar.Image"), System.Drawing.Image)
        Me.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(128, 25)
        Me.btnActualizar.Text = "Actualizar (F5)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 28)
        '
        'ToolStripPagina
        '
        Me.ToolStripPagina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripPagina.DropDownWidth = 60
        Me.ToolStripPagina.Name = "ToolStripPagina"
        Me.ToolStripPagina.Size = New System.Drawing.Size(99, 28)
        Me.ToolStripPagina.ToolTipText = "Seleccione la pagina que quiere ver"
        '
        'btnPrimero
        '
        Me.btnPrimero.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrimero.Image = CType(resources.GetObject("btnPrimero.Image"), System.Drawing.Image)
        Me.btnPrimero.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New System.Drawing.Size(82, 25)
        Me.btnPrimero.Text = "Primero"
        Me.btnPrimero.ToolTipText = "Ir al primer registro"
        '
        'btnAnterior
        '
        Me.btnAnterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnterior.Image = CType(resources.GetObject("btnAnterior.Image"), System.Drawing.Image)
        Me.btnAnterior.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(85, 25)
        Me.btnAnterior.Text = "Anterior"
        Me.btnAnterior.ToolTipText = "Ir al registro anterior"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSiguiente.Image = CType(resources.GetObject("btnSiguiente.Image"), System.Drawing.Image)
        Me.btnSiguiente.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(92, 25)
        Me.btnSiguiente.Text = "Siguiente"
        Me.btnSiguiente.ToolTipText = "Ir al siguiente registro"
        '
        'btnUltimo
        '
        Me.btnUltimo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(72, 25)
        Me.btnUltimo.Text = "Último"
        Me.btnUltimo.ToolTipText = "Ir al último registro"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 28)
        '
        'btnExcel
        '
        Me.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(23, 25)
        Me.btnExcel.Text = "Exportar Excel"
        Me.btnExcel.Visible = False
        '
        'btnImportarExcel
        '
        Me.btnImportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImportarExcel.Image = CType(resources.GetObject("btnImportarExcel.Image"), System.Drawing.Image)
        Me.btnImportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImportarExcel.Name = "btnImportarExcel"
        Me.btnImportarExcel.Size = New System.Drawing.Size(23, 25)
        Me.btnImportarExcel.Text = "Importar desde Excel"
        Me.btnImportarExcel.ToolTipText = "Importar Productos desde Excel"
        Me.btnImportarExcel.Visible = False
        '
        'btnImprimir
        '
        Me.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(23, 25)
        Me.btnImprimir.Text = "Imprimir Reporte"
        '
        'ToolStrip_lblCodMaterial
        '
        Me.ToolStrip_lblCodMaterial.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip_lblCodMaterial.Name = "ToolStrip_lblCodMaterial"
        Me.ToolStrip_lblCodMaterial.Size = New System.Drawing.Size(122, 25)
        Me.ToolStrip_lblCodMaterial.Text = "(F11) Cód. MAT."
        Me.ToolStrip_lblCodMaterial.Visible = False
        '
        'txtBusquedaMAT
        '
        Me.txtBusquedaMAT.Name = "txtBusquedaMAT"
        Me.txtBusquedaMAT.Size = New System.Drawing.Size(121, 28)
        Me.txtBusquedaMAT.Visible = False
        '
        'btnImagenExcel
        '
        Me.btnImagenExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImagenExcel.Image = CType(resources.GetObject("btnImagenExcel.Image"), System.Drawing.Image)
        Me.btnImagenExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImagenExcel.Name = "btnImagenExcel"
        Me.btnImagenExcel.Size = New System.Drawing.Size(23, 25)
        Me.btnImagenExcel.Visible = False
        '
        'ToolStrip_lblCambio
        '
        Me.ToolStrip_lblCambio.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip_lblCambio.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip_lblCambio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ToolStrip_lblCambio.Name = "ToolStrip_lblCambio"
        Me.ToolStrip_lblCambio.Size = New System.Drawing.Size(91, 25)
        Me.ToolStrip_lblCambio.Text = " 1 U$$ = $ "
        Me.ToolStrip_lblCambio.Visible = False
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(0, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(74, 20)
        Me.ToolStripLabel3.Text = "Conexión"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(0, 0)
        '
        'PicConexion
        '
        Me.PicConexion.Image = Global.PORKYS.My.Resources.Resources.Red_Ball_icon
        Me.PicConexion.Name = "PicConexion"
        Me.PicConexion.Size = New System.Drawing.Size(16, 16)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status1, Me.Status2, Me.ToolStripDropDownButton1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 422)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip1.Size = New System.Drawing.Size(1816, 22)
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
        Me.RestituirLosDatosAnterioresToolStripMenuItem.Size = New System.Drawing.Size(266, 24)
        Me.RestituirLosDatosAnterioresToolStripMenuItem.Text = "Restituir los datos anteriores"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FToolStripMenuItem, Me.FiltroExcluyendoLaSelecciónToolStripMenuItem, Me.FiltrarporToolStrip, Me.QuitarElFitroToolStripMenuItem, Me.ToolStripTextBox1, Me.ToolMovilizar})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(285, 158)
        '
        'FToolStripMenuItem
        '
        Me.FToolStripMenuItem.Image = CType(resources.GetObject("FToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FToolStripMenuItem.Name = "FToolStripMenuItem"
        Me.FToolStripMenuItem.Size = New System.Drawing.Size(284, 24)
        Me.FToolStripMenuItem.Text = "Filtro por selección"
        Me.FToolStripMenuItem.Visible = False
        '
        'FiltroExcluyendoLaSelecciónToolStripMenuItem
        '
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Name = "FiltroExcluyendoLaSelecciónToolStripMenuItem"
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Size = New System.Drawing.Size(284, 24)
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Text = "Filtro excluyendo la selección"
        Me.FiltroExcluyendoLaSelecciónToolStripMenuItem.Visible = False
        '
        'FiltrarporToolStrip
        '
        Me.FiltrarporToolStrip.Name = "FiltrarporToolStrip"
        Me.FiltrarporToolStrip.Size = New System.Drawing.Size(224, 27)
        Me.FiltrarporToolStrip.Text = "Filtrar por:"
        '
        'QuitarElFitroToolStripMenuItem
        '
        Me.QuitarElFitroToolStripMenuItem.Image = CType(resources.GetObject("QuitarElFitroToolStripMenuItem.Image"), System.Drawing.Image)
        Me.QuitarElFitroToolStripMenuItem.Name = "QuitarElFitroToolStripMenuItem"
        Me.QuitarElFitroToolStripMenuItem.Size = New System.Drawing.Size(284, 24)
        Me.QuitarElFitroToolStripMenuItem.Text = "Quitar el Fitro"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(224, 27)
        Me.ToolStripTextBox1.Text = "Inmovilizar columnas:"
        Me.ToolStripTextBox1.Visible = False
        '
        'ToolMovilizar
        '
        Me.ToolMovilizar.Name = "ToolMovilizar"
        Me.ToolMovilizar.Size = New System.Drawing.Size(284, 24)
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
        Me.grd.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grd.DefaultCellStyle = DataGridViewCellStyle1
        Me.grd.Location = New System.Drawing.Point(0, 122)
        Me.grd.Margin = New System.Windows.Forms.Padding(4)
        Me.grd.MultiSelect = False
        Me.grd.Name = "grd"
        Me.grd.ReadOnly = True
        Me.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grd.Size = New System.Drawing.Size(1800, 292)
        Me.grd.TabIndex = 62
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1816, 444)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
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
    Friend WithEvents btnSalir As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnImagenExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCargaContinua As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnActivar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip_lblCambio As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbBusquedaMAT As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStrip_lblCodMaterial As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtBusquedaMAT As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents PicConexion As System.Windows.Forms.ToolStripLabel
End Class
