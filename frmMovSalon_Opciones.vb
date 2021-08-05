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




Public Class frmMovSalon_Opciones

    Public Operacion As String

    Private Sub frmPedidosWEB_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                Me.Close()
        End Select
    End Sub

    Private Sub btnAbrirPack_Click(sender As Object, e As EventArgs) Handles btnAbrirPack.Click
        Operacion = "Abrir Pack"
        Dim AP As New frmMovSalon_Productos
        AP.ShowDialog()
    End Sub

    Private Sub btnTransferir_Click(sender As Object, e As EventArgs) Handles btnTransferir.Click
        Operacion = "Transferencia"
        Dim AP As New frmMovSalon_Productos
        AP.ShowDialog()
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub


End Class