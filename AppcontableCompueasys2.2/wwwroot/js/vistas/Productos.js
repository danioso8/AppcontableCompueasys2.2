$(document).ready(function () {

   
   

    fetch("/Marcas/Get").then(res => {

        return res.ok ? res.json() : Promise.reject(res);

    }).then(resjson => {

        if (resjson.length > 0) {

            resjson.forEach((item) => {

                $("#IdMarca").append(
                    $("<option>").val(item.idMarca).text(item.descripcion));
              
            });
        }
    });




    fetch("/Categorias/Get").then(res => {

        return res.ok ? res.json() : Promise.reject(res);

    }).then(resjson => {

        if (resjson.length > 0) {

            resjson.forEach((item) => {

                $("#IdCategoria").append(
                    $("<option>").val(item.idMarca).text(item.descripcion));

            });
        }
    });


});