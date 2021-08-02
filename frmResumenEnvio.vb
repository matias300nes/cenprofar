Option Explicit On

Imports Microsoft.ApplicationBlocks.Data
Imports Utiles
Imports Utiles.Util
Imports Utiles.compartida
Imports System.Data.SqlClient
Imports ReportesNet

Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO




Public Class frmResumenEnvio

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmVentasWEB.chkVentas.Checked = True Then
            If frmVentasWEB.btnXNorte.Checked = True Then
                GroupPanel1.Text = "Norte-Venta"
                lblNro.Text = "03-XXXXXXXX"
            Else
                GroupPanel1.Text = "Venta"
                If MDIPrincipal.sucursal.Contains("PERON") Then
                    lblNro.Text = "04-XXXXXXXX"
                Else
                    lblNro.Text = "02-XXXXXXXX"
                End If
            End If
            'me fijo si el chk de presupuesto esta activado
            If frmVentasWEB.chkPresupuesto.Checked = True And frmVentasWEB.FinalizarPresupuesto = False Then
                GroupPanel1.Text = "Presupuesto"
                chkPagoCompleto.Visible = False
            End If
        Else
            If frmVentasWEB.btnXNorte.Checked = True Then
                GroupPanel1.Text = "Norte-Devolución"
                lblNro.Text = "98-XXXXXXXX"
            Else
                GroupPanel1.Text = "Devolución"
                If MDIPrincipal.sucursal.Contains("PERON") Then
                    lblNro.Text = "97-XXXXXXXX"
                Else
                    lblNro.Text = "99-XXXXXXXX"
                End If         
            End If
            chkPagoCompleto.Checked = False
            chkPagoCompleto.Visible = False
        End If
        lblCliente.Text = frmVentasWEB.cmbClientes.Text.ToUpper
        lblRepartidor.Text = frmVentasWEB.cmbRepartidor.Text.ToUpper
        lblNota.Text = frmVentasWEB.txtNota.Text.ToUpper
        lblSubtotal.Text = "$" + frmVentasWEB.lblSubtotal.Text
        If frmVentasWEB.rdPorcentaje.Checked = True Then
            lblDescuento.Text = frmVentasWEB.txtDescuento.Text + "%"
        Else
            lblDescuento.Text = "$" + frmVentasWEB.txtDescuento.Text
        End If
        lbltotal.Text = "$" + frmVentasWEB.lblTotal.Text
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        MDIPrincipal.ConfirResPed = False
        Me.Close()
    End Sub


    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        MDIPrincipal.ConfirResPed = True
        MDIPrincipal.RealizarPago_Completo = chkPagoCompleto.Checked
        Me.Close()
    End Sub

  
End Class