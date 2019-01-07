 btnBuscar = "#btnBuscar";
var gridlistaots = "#gridlistaots";
var gridlistaotspager = "#gridlistaotspager";

$(document).ready(function () {
 
configurarChosenSelect();
 $(btnBuscar).click(function () {

       var iddestino =    $('#iddestino').val();
       var idproveedor =    $('#idproveedor').val();
       var fechainicio =    $('#fechainicio').val();
       var fechafin =    $('#fechafin').val();

    
      var url = UrlHelper.Action("BuscarReporteGeneral", "facturacion", "facturacion") + "?iddestino=" +  iddestino +"&idproveedor=" + idproveedor +"&fechainicio=" + fechainicio+"&fechafin=" + fechafin


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal("show");


    });

 });



  $('#dtpfechaini .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'
    });


    $('#dtpfechafin .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'
    });


    
});



function OnCompleteTransaction_RegistrarIncidencia(xhr, status)
{
    var jsonres = xhr.responseJSON;


}


function configurarChosenSelect()
{
    
     var config = {
            '.chosen-select': {
                  max_selected_options: 5,
                 placeholder_text_multiple: "Seleccione Conceptos",
                 no_results_text: 'Oops, no se encontró el dato!' 
               }
        }

        for (var selector in config) {

            $(selector).chosen(config[selector]);
        }


       

        
}
