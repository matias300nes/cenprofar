Imports System.Data.SqlClient
Module Modulo_WEB


    '----------------------------------------------------------------------------------------------------
    ' ------- este modulo lo utilizo para crear variables que uso para la funciones WEB -----------------
    '----------------------------------------------------------------------------------------------------

    'esta variable la utilizo para descargar siempre el stock del otro almacen 
    Public otroAlmacen As Integer
    Public ConexionWEB As SqlConnection
    '-----FALTAN TABLAS Y SP DE PEDIDOSWEB----
    'declaro el nombre de las tablas en produccion de la WEB
    Public NameTable_Materiales As String = "Materiales"
    Public NameTable_Stock As String = "Stock"
    Public NameTable_StockMov As String = "StockMov"
    Public NameTable_Unidades As String = "Unidades"
    Public NameTable_Familias As String = "Familias"
    Public NameTable_Marcas As String = "Marcas"
    Public NameTable_ListaPrecios As String = "Lista_Precios"
    Public NameTable_Clientes As String = "Clientes"
    Public NameTable_Empleados As String = "Empleados"
    Public NameTable_NotificacionesWEB As String = "NotificacionesWEB"
    Public NameTable_TransRecepWEB As String = "TransRecep_WEB"
    Public NameTable_TransRecepWEBdet As String = "TransRecep_WEB_det"

    'declaro nombre de las Sp en produccion de la WEB
    Public NameSP_spActualizarPrecioPorLista As String = "spActualizar_PrecioPorLista"
    Public NameSP_spStockInsert As String = "spStock_Insert"
    Public NameSP_spTransRecepWEB_Insert As String = "spTransRecepWEB_Insert"




End Module
