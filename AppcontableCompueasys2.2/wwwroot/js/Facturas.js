

//function SeleccionarProducto(e) {
//    let _Producto = $(e).find("option:selected");
//    let total = $("#txtTotal").val() || 0;
//    $("#tbProducto").append(`
//      <tr>
//          <td> ${_Producto.val()} </td>
//          <td> ${_Producto.text()} </td>
//          <td> ${_Producto.attr("Precio")} </td>

//     </tr>
//    `);
//    let sumaT = parseInt(total) + parseInt(_Producto.attr("Precio"));
//    $("#txtTotal").text(sumaT);
//    con++;
//}






//var con = 0;
//function SeleccionarProducto(p) {
//    let _Producto = $(this).closest('.DaB').data(p);
//    let total = $("#txtTotal").val() || 0;
//    $("#tbProducto").append(`
//      <tr>
//          <td> ${_Producto.val()} </td>
//          <td> ${_Producto.text()} </td>
//          <td> ${_Producto.attr("Precio")} </td>

//     </tr>
//    `);
//    let sumaT = parseInt(total) + parseInt(_Producto.attr("Precio"));
//    $("#txtTotal").text(sumaT);
//    con++;
//}



$(document).ready(function () {
    
    
    var con = 0;
    alert('hello')
    $('.DaB').on('change', function () {
        
        let _Producto = $(this).find("option:selected").text();
        let total = $("#txtTotal").val() || 0;

        $("#tbProducto").append(`
          <tr>
              <td>${_Producto.val()}</td>
              <td>${_Producto.text()}</td>
              <td>${_Producto.attr("Precio")}</td>

         </tr>
        `);
        let sumaT = parseInt(total) + parseInt(_Producto.attr("Precio"));
        $("#txtTotal").text(sumaT);
        con++;
    });
    
    });
