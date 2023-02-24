







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