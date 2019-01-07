var grilla = $("#gridpreliquidacion");
var pagergrilla = $("#gridpreliquidacionpager");
var editOptionsNew = {
        keys: true,
        successfunc: function () {
            var $self = $(this);
            setTimeout(function () {
                $self.trigger("reloadGrid");
            }, 50);
        }
    };

$(document).ready(function() {

 $("html, body").animate({ scrollTop: "100px" });
 configchosen();
 configurarGrilla();
 configurarBotones();

});
function configurarBotones()
{
$("#btnGenerar").click(function(event) {
    var items =  $(grilla).jqGrid('getGridParam', 'selarrrow');
    if(items=='')
      messagebox('No puede continuar.','Debe seleccionar al menos un elemento.','warning');
    else
      GenerarPreliquidacion(items);
});

$("#btnBuscar").click(function(event) {
    reload();
});

$('#btnRecargo').click(function (event){
    var  selIds  = $(grilla).jqGrid('getGridParam', 'selarrrow');
    if(selIds == '')
        messagebox('No puede continuar.','Debe seleccionar al menos un elemento.','warning');
    else if(selIds.length>1)
      messagebox('No puede continuar.','Debe seleccionar solo un elemento.','warning')
    else
      AgregarRecargo(selIds);
});


}
function AgregarRecargo(idorden)
{
    var url = UrlHelper.Action ("AgregarRecargo","Facturacion","Facturacion");
    $.get(url, function (data){
          $("#modalcontent").html(data);
          $("#modalcontainer").modal("show");
          $("#idordentrabajo").val(idorden);

    })
}



function GenerarPreliquidacion(items)
{

  var url = UrlHelper.Action("JsonGenerarPreliquidacion","Facturacion","Facturacion");

  swal({
      title: "Generar Preliquidación",
      text: "¿Está seguro que desea generar la preliquidación?",
      type: "warning",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: '#DD6B55',
      confirmButtonText: 'Generar',
      closeOnConfirm: true,
      closeOnCancel: true
  },
     function (isConfirm) {
         if (isConfirm) {
           $.ajax({
             url: url,
             type: 'POST',
             dataType: 'json',
             data: { 'ordenes' : String(items)}
           })
           .done(function() {
               reload();
               messagebox('Registro Exitoso','Se ha generado la preliquidación.','success');
           })
           .fail(function() {
               messagebox('No se pudo generar','No se pudo registrar la Preliquidación','error');
           })
         }
       });

}
function configchosen()
{
    var config = {
         '.chosen-select': {max_selected_options: 5 ,
              allow_single_deselect: false ,
              no_results_text: 'Oops, no se encontró el ubigeo!' }

     }
     for (var selector in config) {
         $(selector).chosen(config[selector]);
     }
}
function OnCompleteTransaction_Recargo(xhr, status)
{
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true)
    {
       swal({
           title: "Registro Exitoso",
           text: "Se registró correctamente el dato.",
            type: "success"
        },
       function ()
       {
            $("#modalcontainer").modal("hide");
            reload();
       });
    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}
