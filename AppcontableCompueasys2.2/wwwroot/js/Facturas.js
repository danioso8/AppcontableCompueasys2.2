






$(document).ready(function () {
    
    var cont = 0;

    $("#ProductoSeleccionado").on('change', function () {
        var producto = $("#ProductoSeleccionado option:selected");
        let precio = $(producto.attr("precio"));
        let totalNeto = $("#txtTotal").val() || 0;
        $("#txtSubTotal").val(producto.attr("precio"));
        let cantidad = $("#Cantidad").val() || 2;
        let total = precio * cantidad;
        $("#tablaProducto").append(`
                    <tr><td></td>
                    <td>${producto.val()}</td>
                    <td><input type="text" id="Cantidad" name="Cantidad" value="${producto.text()}"/></td>
                    <td><input type="number" id="Cantidad" name="Cantidad" value="1" /></td>
                    <td><input type="text" id="Cantidad" name="Cantidad" value="${producto.attr("precio")}"/></td>
                    <td>${ parseFloat(producto.attr("precio")) }</td>
                   
                    </tr>
                    `)
        
        let sumaTotal = parseInt(totalNeto) + parseInt(producto.attr("precio"));
        $("#txtSubTotal").val(sumaTotal)
        $("#txtTotal").val(sumaTotal)
        cont++;
    });
   
    


    fetch("/TipoDePagos/Get").then(res => {

        return res.ok ? res.json() : Promise.reject(res);

    }).then(resjson => {

        if (resjson.length > 0) {

            resjson.forEach((item) => {

                $("#cboTipoDocumentoVenta").append(
                    $("<option>").val(item.id).text(item.descripcion))



            });
        }
    });





    //$("#ProductoSeleccionado").select2({
    //    ajax: {
    //        url: "https://api.github.com/search/repositories",
    //        dataType: 'json',
    //        delay: 250,
    //        data: function (params) {
    //            return {
    //               busqueda:params.term
    //            };
    //        },
    //        processResults: function (data) {
              
    //           return {
    //                results: data.items,
                    
    //            };
    //        },
    //        cache: true
    //    },
    //    placeholder: 'Search for a repository',
    //    minimumInputLength: 1,
    //    templateResult: formatRepo,
    //    templateSelection: formatRepoSelection
    //});

   
  


});





var inputCantidad = $("#Cantidad");
inputCantidad.keypress(function (event) {
    if (event.keyCode == 13) {
        let sumaTotal = parseInt(precio) * $("#Cantidad").val();
        $("#txtSubTotal").val(sumaTotal)
        $("#txtTotal").val(sumaTotal)
    }
});