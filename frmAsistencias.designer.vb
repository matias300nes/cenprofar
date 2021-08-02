<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAsistencias
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblJornada = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblEmpleado = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tmrControlIngreso = New System.Windows.Forms.Timer(Me.components)
        Me.pnlSocio = New System.Windows.Forms.Panel()
        Me.lblHora = New System.Windows.Forms.Label()
        Me.lblTituloHora = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblTituloFecha = New System.Windows.Forms.Label()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.pnlPromocion = New System.Windows.Forms.Panel()
        Me.lblInfoPantalla = New System.Windows.Forms.Label()
        Me.lblPromocion = New System.Windows.Forms.Label()
        Me.TimerEspera = New System.Windows.Forms.Timer(Me.components)
        Me.TimerFechaHora = New System.Windows.Forms.Timer(Me.components)
        Me.txtPantalla = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.TimerInfoPantalla = New System.Windows.Forms.Timer(Me.components)
        Me.pnlSocio.SuspendLayout()
        Me.pnlPromocion.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Light", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(53, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(220, 31)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Apellido y Nombre:"
        '
        'lblJornada
        '
        Me.lblJornada.AutoSize = True
        Me.lblJornada.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJornada.ForeColor = System.Drawing.Color.White
        Me.lblJornada.Location = New System.Drawing.Point(51, 155)
        Me.lblJornada.Name = "lblJornada"
        Me.lblJornada.Size = New System.Drawing.Size(58, 41)
        Me.lblJornada.TabIndex = 32
        Me.lblJornada.Text = "    "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Light", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(53, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 31)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Jornada:"
        '
        'lblEmpleado
        '
        Me.lblEmpleado.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpleado.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpleado.ForeColor = System.Drawing.Color.White
        Me.lblEmpleado.Location = New System.Drawing.Point(51, 66)
        Me.lblEmpleado.Name = "lblEmpleado"
        Me.lblEmpleado.Size = New System.Drawing.Size(303, 48)
        Me.lblEmpleado.TabIndex = 30
        Me.lblEmpleado.Text = "LEDESMA ALBERTO"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1342, 32)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Ingrese su número de documento y luego presione la tecla 'Enter'"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tmrControlIngreso
        '
        Me.tmrControlIngreso.Interval = 5000
        '
        'pnlSocio
        '
        Me.pnlSocio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSocio.Controls.Add(Me.lblHora)
        Me.pnlSocio.Controls.Add(Me.lblTituloHora)
        Me.pnlSocio.Controls.Add(Me.lblFecha)
        Me.pnlSocio.Controls.Add(Me.lblTituloFecha)
        Me.pnlSocio.Controls.Add(Me.lblMensaje)
        Me.pnlSocio.Controls.Add(Me.Label6)
        Me.pnlSocio.Controls.Add(Me.lblJornada)
        Me.pnlSocio.Controls.Add(Me.Label4)
        Me.pnlSocio.Controls.Add(Me.lblEmpleado)
        Me.pnlSocio.Location = New System.Drawing.Point(12, 210)
        Me.pnlSocio.Name = "pnlSocio"
        Me.pnlSocio.Size = New System.Drawing.Size(1342, 546)
        Me.pnlSocio.TabIndex = 43
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.Font = New System.Drawing.Font("Segoe UI Light", 27.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.ForeColor = System.Drawing.Color.White
        Me.lblHora.Location = New System.Drawing.Point(1119, 66)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(60, 48)
        Me.lblHora.TabIndex = 47
        Me.lblHora.Text = "    "
        '
        'lblTituloHora
        '
        Me.lblTituloHora.AutoSize = True
        Me.lblTituloHora.Font = New System.Drawing.Font("Segoe UI Light", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lblTituloHora.ForeColor = System.Drawing.Color.White
        Me.lblTituloHora.Location = New System.Drawing.Point(1121, 35)
        Me.lblTituloHora.Name = "lblTituloHora"
        Me.lblTituloHora.Size = New System.Drawing.Size(72, 31)
        Me.lblTituloHora.TabIndex = 46
        Me.lblTituloHora.Text = "Hora:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Segoe UI Light", 27.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.Color.White
        Me.lblFecha.Location = New System.Drawing.Point(893, 66)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(60, 48)
        Me.lblFecha.TabIndex = 45
        Me.lblFecha.Text = "    "
        '
        'lblTituloFecha
        '
        Me.lblTituloFecha.AutoSize = True
        Me.lblTituloFecha.Font = New System.Drawing.Font("Segoe UI Light", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lblTituloFecha.ForeColor = System.Drawing.Color.White
        Me.lblTituloFecha.Location = New System.Drawing.Point(895, 35)
        Me.lblTituloFecha.Name = "lblTituloFecha"
        Me.lblTituloFecha.Size = New System.Drawing.Size(81, 31)
        Me.lblTituloFecha.TabIndex = 44
        Me.lblTituloFecha.Text = "Fecha:"
        '
        'lblMensaje
        '
        Me.lblMensaje.BackColor = System.Drawing.Color.Cyan
        Me.lblMensaje.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.Color.Black
        Me.lblMensaje.Location = New System.Drawing.Point(336, 448)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(670, 76)
        Me.lblMensaje.TabIndex = 43
        Me.lblMensaje.Tag = ""
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMensaje.Visible = False
        '
        'btnSalir
        '
        Me.btnSalir.BackColor = System.Drawing.SystemColors.HotTrack
        Me.btnSalir.FlatAppearance.BorderSize = 0
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.ForeColor = System.Drawing.Color.White
        Me.btnSalir.Location = New System.Drawing.Point(1329, 11)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(28, 31)
        Me.btnSalir.TabIndex = 43
        Me.btnSalir.Text = "X"
        Me.btnSalir.UseVisualStyleBackColor = False
        Me.btnSalir.Visible = False
        '
        'pnlPromocion
        '
        Me.pnlPromocion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPromocion.Controls.Add(Me.lblInfoPantalla)
        Me.pnlPromocion.Controls.Add(Me.lblPromocion)
        Me.pnlPromocion.Location = New System.Drawing.Point(12, 210)
        Me.pnlPromocion.Name = "pnlPromocion"
        Me.pnlPromocion.Size = New System.Drawing.Size(1342, 445)
        Me.pnlPromocion.TabIndex = 43
        '
        'lblInfoPantalla
        '
        Me.lblInfoPantalla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfoPantalla.BackColor = System.Drawing.SystemColors.HotTrack
        Me.lblInfoPantalla.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoPantalla.ForeColor = System.Drawing.Color.White
        Me.lblInfoPantalla.Location = New System.Drawing.Point(17, 230)
        Me.lblInfoPantalla.Name = "lblInfoPantalla"
        Me.lblInfoPantalla.Size = New System.Drawing.Size(1304, 175)
        Me.lblInfoPantalla.TabIndex = 3
        Me.lblInfoPantalla.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPromocion
        '
        Me.lblPromocion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPromocion.BackColor = System.Drawing.SystemColors.HotTrack
        Me.lblPromocion.Font = New System.Drawing.Font("Segoe UI", 34.0!)
        Me.lblPromocion.ForeColor = System.Drawing.Color.White
        Me.lblPromocion.Location = New System.Drawing.Point(17, 35)
        Me.lblPromocion.Name = "lblPromocion"
        Me.lblPromocion.Size = New System.Drawing.Size(1304, 175)
        Me.lblPromocion.TabIndex = 2
        Me.lblPromocion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimerEspera
        '
        Me.TimerEspera.Interval = 1300
        '
        'TimerFechaHora
        '
        Me.TimerFechaHora.Enabled = True
        Me.TimerFechaHora.Interval = 1000
        '
        'txtPantalla
        '
        Me.txtPantalla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPantalla.Decimals = CType(2, Byte)
        Me.txtPantalla.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPantalla.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPantalla.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtPantalla.Location = New System.Drawing.Point(12, 142)
        Me.txtPantalla.MaxLength = 8
        Me.txtPantalla.Name = "txtPantalla"
        Me.txtPantalla.Size = New System.Drawing.Size(1342, 62)
        Me.txtPantalla.TabIndex = 44
        Me.txtPantalla.Text_1 = Nothing
        Me.txtPantalla.Text_2 = Nothing
        Me.txtPantalla.Text_3 = Nothing
        Me.txtPantalla.Text_4 = Nothing
        Me.txtPantalla.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPantalla.UserValues = Nothing
        '
        'frmAsistencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.HotTrack
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.txtPantalla)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlPromocion)
        Me.Controls.Add(Me.pnlSocio)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmAsistencias"
        Me.Text = "FrmControlIngreso"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSocio.ResumeLayout(False)
        Me.pnlSocio.PerformLayout()
        Me.pnlPromocion.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblJornada As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblEmpleado As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmrControlIngreso As System.Windows.Forms.Timer
    Friend WithEvents pnlSocio As System.Windows.Forms.Panel
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents pnlPromocion As System.Windows.Forms.Panel
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents TimerEspera As System.Windows.Forms.Timer
    Friend WithEvents lblPromocion As System.Windows.Forms.Label
    Friend WithEvents TimerFechaHora As System.Windows.Forms.Timer
    Friend WithEvents lblTituloFecha As System.Windows.Forms.Label
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents lblHora As System.Windows.Forms.Label
    Friend WithEvents lblTituloHora As System.Windows.Forms.Label
    Friend WithEvents txtPantalla As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblInfoPantalla As System.Windows.Forms.Label
    Friend WithEvents TimerInfoPantalla As System.Windows.Forms.Timer
End Class
