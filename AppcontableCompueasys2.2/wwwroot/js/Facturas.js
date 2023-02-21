﻿







$(document).ready(function () {

    var con = 0;
    
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
    

    

 $("#ProductoSeleccionado").on('change', function () {
        
    
     let _Producto = $(this).find("option:selected")
     
        
        let total = $("#txtTotal").val() || 0;
     
        $("#tablaProducto").append(`
                                    <tr>
                                      <td>${""}</td>
                                                                      
                                      <td>${_Producto.val()}</td>
                                      <td>${_Producto.text()}</td>
                                      <td>${""}</td>
                                      <td>${_Producto.attr("Precio")}</td>
                                    </tr>`);
        
       
        let sumaT = parseInt(total) + parseInt(_Producto.attr("Precio"));
        $("#txtTotal").text(sumaT);
        con++;
    });
    
    });
