







$(document).ready(function () {
    alert("hhh");
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

    
    
    });
