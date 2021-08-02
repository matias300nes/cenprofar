Imports System.Windows.Forms

Public Class frmPresupuestos_Tipos

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        frmPresupuestos.rdBaja.Checked = rdBaja.Checked
        frmPresupuestos.rdMateriales.Checked = rdMateriales.Checked
        frmPresupuestos.rdMedia.Checked = rdMedia.Checked
        frmPresupuestos.rdTableros.Checked = rdTableros.Checked
        frmPresupuestos.rdTrafo.Checked = rdTrafo.Checked
        frmPresupuestos.chkManoObra.Enabled = rdMateriales.Checked
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        frmPresupuestos.rdBaja.Checked = rdBaja.Checked
        frmPresupuestos.rdMateriales.Checked = rdMateriales.Checked
        frmPresupuestos.rdMedia.Checked = rdMedia.Checked
        frmPresupuestos.rdTableros.Checked = rdTableros.Checked
        frmPresupuestos.rdTrafo.Checked = rdTrafo.Checked
        frmPresupuestos.chkManoObra.Enabled = rdMateriales.Checked
        Me.Close()
    End Sub

  
End Class
