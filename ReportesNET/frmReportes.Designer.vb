<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportes
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
        Me.rdReportes = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        Me.crvReportes = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.btnOutlook = New System.Windows.Forms.Button()
        Me.btnPDF = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'crvReportes
        '
        Me.crvReportes.ActiveViewIndex = -1
        Me.crvReportes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvReportes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvReportes.Location = New System.Drawing.Point(0, 0)
        Me.crvReportes.Name = "crvReportes"
        Me.crvReportes.SelectionFormula = ""
        Me.crvReportes.Size = New System.Drawing.Size(1028, 633)
        Me.crvReportes.TabIndex = 0
        Me.crvReportes.ViewTimeSelectionFormula = ""
        '
        'btnOutlook
        '
        Me.btnOutlook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOutlook.Location = New System.Drawing.Point(534, 6)
        Me.btnOutlook.Name = "btnOutlook"
        Me.btnOutlook.Size = New System.Drawing.Size(163, 19)
        Me.btnOutlook.TabIndex = 1
        Me.btnOutlook.Text = "Abrir Correo Electrónico"
        Me.btnOutlook.UseVisualStyleBackColor = True
        '
        'btnPDF
        '
        Me.btnPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPDF.Location = New System.Drawing.Point(365, 6)
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Size = New System.Drawing.Size(163, 19)
        Me.btnPDF.TabIndex = 2
        Me.btnPDF.Text = "Guardar como PDF"
        Me.btnPDF.UseVisualStyleBackColor = True
        '
        'frmReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 633)
        Me.Controls.Add(Me.btnPDF)
        Me.Controls.Add(Me.btnOutlook)
        Me.Controls.Add(Me.crvReportes)
        Me.Name = "frmReportes"
        Me.Text = "frmReportes"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdReportes As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Friend WithEvents crvReportes As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnOutlook As System.Windows.Forms.Button
    Public WithEvents btnPDF As System.Windows.Forms.Button
End Class
