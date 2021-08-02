<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPresupuestos_Tipos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.rdMateriales = New System.Windows.Forms.RadioButton()
        Me.rdTableros = New System.Windows.Forms.RadioButton()
        Me.rdTrafo = New System.Windows.Forms.RadioButton()
        Me.rdMedia = New System.Windows.Forms.RadioButton()
        Me.rdBaja = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94.52055!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.479452!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(451, 63)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(94, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(13, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Aceptar"
        '
        'rdMateriales
        '
        Me.rdMateriales.AutoSize = True
        Me.rdMateriales.Checked = True
        Me.rdMateriales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdMateriales.ForeColor = System.Drawing.Color.Red
        Me.rdMateriales.Location = New System.Drawing.Point(12, 23)
        Me.rdMateriales.Name = "rdMateriales"
        Me.rdMateriales.Size = New System.Drawing.Size(177, 17)
        Me.rdMateriales.TabIndex = 1
        Me.rdMateriales.TabStop = True
        Me.rdMateriales.Text = "Materiales / Mano de Obra"
        Me.rdMateriales.UseVisualStyleBackColor = True
        '
        'rdTableros
        '
        Me.rdTableros.AutoSize = True
        Me.rdTableros.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTableros.ForeColor = System.Drawing.Color.Green
        Me.rdTableros.Location = New System.Drawing.Point(195, 23)
        Me.rdTableros.Name = "rdTableros"
        Me.rdTableros.Size = New System.Drawing.Size(74, 17)
        Me.rdTableros.TabIndex = 2
        Me.rdTableros.TabStop = True
        Me.rdTableros.Text = "Tableros"
        Me.rdTableros.UseVisualStyleBackColor = True
        '
        'rdTrafo
        '
        Me.rdTrafo.AutoSize = True
        Me.rdTrafo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTrafo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rdTrafo.Location = New System.Drawing.Point(275, 23)
        Me.rdTrafo.Name = "rdTrafo"
        Me.rdTrafo.Size = New System.Drawing.Size(55, 17)
        Me.rdTrafo.TabIndex = 3
        Me.rdTrafo.TabStop = True
        Me.rdTrafo.Text = "Trafo"
        Me.rdTrafo.UseVisualStyleBackColor = True
        '
        'rdMedia
        '
        Me.rdMedia.AutoSize = True
        Me.rdMedia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdMedia.ForeColor = System.Drawing.Color.Blue
        Me.rdMedia.Location = New System.Drawing.Point(336, 23)
        Me.rdMedia.Name = "rdMedia"
        Me.rdMedia.Size = New System.Drawing.Size(108, 17)
        Me.rdMedia.TabIndex = 4
        Me.rdMedia.TabStop = True
        Me.rdMedia.Text = "Media Tensión"
        Me.rdMedia.UseVisualStyleBackColor = True
        '
        'rdBaja
        '
        Me.rdBaja.AutoSize = True
        Me.rdBaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdBaja.Location = New System.Drawing.Point(450, 23)
        Me.rdBaja.Name = "rdBaja"
        Me.rdBaja.Size = New System.Drawing.Size(99, 17)
        Me.rdBaja.TabIndex = 5
        Me.rdBaja.TabStop = True
        Me.rdBaja.Text = "Baja Tensión"
        Me.rdBaja.UseVisualStyleBackColor = True
        '
        'frmPresupuestos_Tipos
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 104)
        Me.ControlBox = False
        Me.Controls.Add(Me.rdBaja)
        Me.Controls.Add(Me.rdMedia)
        Me.Controls.Add(Me.rdTrafo)
        Me.Controls.Add(Me.rdTableros)
        Me.Controls.Add(Me.rdMateriales)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPresupuestos_Tipos"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tipo de Presupuesto"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents rdMateriales As System.Windows.Forms.RadioButton
    Friend WithEvents rdTableros As System.Windows.Forms.RadioButton
    Friend WithEvents rdTrafo As System.Windows.Forms.RadioButton
    Friend WithEvents rdMedia As System.Windows.Forms.RadioButton
    Friend WithEvents rdBaja As System.Windows.Forms.RadioButton

End Class
