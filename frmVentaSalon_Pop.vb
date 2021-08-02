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




Public Class frmVentaSalon_Pop

    Private Sub ResumenEnvío(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmVentaSalon.chkDevolucion.Checked Then
            GroupPanel1.Text = "Devolución"
        Else
            GroupPanel1.Text = "Venta"
        End If
        lblNroFac.Text = frmVentaSalon.ValorFac
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub

   
End Class