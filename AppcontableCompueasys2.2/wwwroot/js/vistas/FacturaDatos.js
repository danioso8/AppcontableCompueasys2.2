



(document).ready(function () {
   

$("#btnTerminarVentar").click(function () {
    var Nfactura = $("#NumeroFactura").val();
    var IdCliente = $("#txtDocumentoCliente").val();
    var Total = $("#txtTotal").val();
    var tipoPago = $("#cboTipoDocumentoVenta option:selected").val();
    var Descuento = $("#Descuento").val();
    var iva = $("#txtIGV").val();
    var IdEmpresa = $("#IdEmpresa").val();
    var IdDetalleC = $("#IdEmpresa").val();
    var Observaciones = $("#IdEmpresa").val();
    var Estado = $("#IdEmpresa").val();


    $.ajax({
        url: "/Facturas/CreateFactu",
        type: "POST",
        data: {
            Nfactura: Nfactura,
            IdCliente: IdCliente,
            Total: Total,
            tipoPago: tipoPago,
            Descuento: Descuento,
            iva: iva,
            IdEmpresa: IdEmpresa,
            IdDetalleC: IdDetalleC,
            Observaciones: Observaciones,
            Estado: Estado
        },
        success: function (result) {
            // hacer algo con la respuesta del controlador
        },
        error: function (error) {
            // manejar el error
        }
    });
});
});



