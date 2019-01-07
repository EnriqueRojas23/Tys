$(document).on("keydown", "input", function(e) {
  if (e.which==13) e.preventDefault();
});




$(document).ready(function () {

  $("html, body").animate({ scrollTop: "100px" });
   $('#btnBuscar').click(function (){
   reload();

    });
    configurarGrilla();
    configurarGrillaDetalle();
    configurarBotones();

});
function configurarBotones()
{
    $("#btnNuevo").click(function(event) {
    var url = UrlHelper.Action("AgregarEmbarqueFluvial","Monitoreo","Monitoreo");

       $.get(url, function (data){
           $("#modalcontent").html(data);
           $("#modalcontainer").modal("show");

          //  $('#frmagregarembarque').validate({ // initialize the plugin
          //     rules: {
          //         conocimientoembarque: {
          //             required: true,
          //             minlength: 3
          //                 },
          //        idtransporte: { required: true }
          //               },
          //      messages: {
          //        conocimientoembarque: {
          //        required: "Valor requerido",
          //        minlength: "Número de caracteres no permitido"
          //       },
          //       idtransporte: {
          //         required: "Debe seleccionar un vehículo."
          //       },
          //       idpuerto : {
          //         required: "Debe seleccionar un puerto."
          //       }
          //   },
          // //   submitHandler:
          // //   $("#frmagregarembarque").on('submit', function () {
          // //     if ($("#frmagregarembarque").valid()) {
          // //       alert("here some code inside $.AJAX({})");
          // //   }
          // //   return false;
          // // })
          // });
         // $.validator.unobtrusive.parse("#frmagregarembarque");
           $('#horainiciocarga').mask('00:00');

           $('#dtpfechacomp .input-group.date').datepicker({
                 todayBtn: "linked",
                 keyboardNavigation: false,
                 forceParse: false,
                 calendarWeeks: true,
                 autoclose: true,
                 format: 'dd/mm/yyyy'

             });



       })


    });
}


function OnCompleteTransaction(xhr, status)
{
   var jsonres = xhr.responseJSON;
    if (jsonres.res == true)
    {
       messagebox("Registro Exitoso.","Se ha registrado con éxito.", "success");
       $("#modalcontainer").modal("hide");
       reload();
    }
    else
    {

      //  CheckValidationErrorResponse(jsonres, null, $("#ValidationSummaryAgregarEmbarque"));
        $("#modalcontainer").modal("hide");
        messagebox("No se puede continuar","El CE ya existe","error");
        //reload();
    }
}

function editarembarque(item)
{

  var url = UrlHelper.Action("EditarEmbarqueFluvial","Monitoreo","Monitoreo") + "?idembarque=" + item;

     $.get(url, function (data){
         $("#modalcontent").html(data);
         $("#modalcontainer").modal("show");

         $('#horainiciocarga').mask('00:00');

         $('#dtpfechacomp .input-group.date').datepicker({
               todayBtn: "linked",
               keyboardNavigation: false,
               forceParse: false,
               calendarWeeks: true,
               autoclose: true,
               format: 'dd/mm/yyyy'

           });

     })
}
function asociarmanifiesto(item)
{
    var url = UrlHelper.Action("AsignarManifiesto","Monitoreo","Monitoreo") + "?idembarque=" + item;

     $.get(url, function (data){
         $("#modalcontent").html(data);
         $("#modalcontainer").modal("show");


        $("#btnGuardarAsignacion").click(function(event) {
          GuardarAsignacion(item);

        });

        $("#agregar").click(function(event) {

          var selectedElms = $('div#jstree_di').jstree("get_selected", true);
          var checked_ids = [];
          if(selectedElms=='')
          {
            messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
            return;
          }




          $.each(selectedElms, function() {
               if(this.data != null)
                 checked_ids.push(this.data.idordentrabajo);
          });

          agregar_manifiestos(checked_ids, item);


        });
        $("#quitar").click(function(event) {
          var selectedElms = $('div#jstree_nod').jstree("get_selected", true);
          var checked_idsa = [];
          if(selectedElms=='')
          {
            messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
            return;
          }


          $.each(selectedElms, function() {
               if(this.data != null)
                 checked_idsa.push(this.data.idordentrabajo);
          });

          quitar_manifiestos(checked_idsa, item);

        });

         cargar_sinasignar(item);
         cargar_asignada(item);
     })
}
function quitar_manifiestos(items, item)
{
  var url =  UrlHelper.Action("JsonQuitarManifiesto","Monitoreo","Monitoreo")  + "?ids="  + items;
  var urlvs =  UrlHelper.Action("JsonListarManifiestosSinEmbarque","Monitoreo","Monitoreo")  + "?idembarque="  + item;
  var urlv =  UrlHelper.Action("JsonListarManifiestosEmbarque","Monitoreo","Monitoreo")  + "?idembarque="  + item;

       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {
              $.ajax({
                url: urlv,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var tree = $("div#jstree_nod").jstree();
                var datas = data;
                tree.settings.core.data = datas;
                tree.refresh();

              })

              $.ajax({
                url: urlvs,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var trees = $("div#jstree_di").jstree();
                var datasa = data;
                trees.settings.core.data = datasa;
                trees.refresh();

              })
       })

}

function agregar_manifiestos(items, item)
{
  var url =  UrlHelper.Action("JsonAgregarManifiesto","Monitoreo","Monitoreo")  + "?ids="  + items;
  var urlvs =  UrlHelper.Action("JsonListarManifiestosSinEmbarque","Monitoreo","Monitoreo")  + "?idembarque="  + item;
  var urlv   =  UrlHelper.Action("JsonListarManifiestosEmbarque","Monitoreo","Monitoreo")  + "?idembarque="  + item;

       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {

              $.ajax({
                url: urlv,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var tree = $("div#jstree_nod").jstree();
                var datas = data;
                tree.settings.core.data = datas;
                tree.refresh();
              })
              $.ajax({
                url: urlvs,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var trees = $("div#jstree_di").jstree();
                var datasa = data;
                trees.settings.core.data = datasa;
                trees.refresh();
              })


       })

}
function cargar_asignada(item)
{
  var url =  UrlHelper.Action("JsonListarManifiestosEmbarque","Monitoreo","Monitoreo")
  + "?idembarque="  + item


       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {
            traer_asignado(data);

       });


}


function cargar_sinasignar(item)
{
    var url =  UrlHelper.Action("JsonListarManifiestosSinEmbarque","Monitoreo","Monitoreo")
    + "?idembarque="  + item
         $.ajax({
           url: url,
           type: 'POST',
           dataType: 'json',
           data: {}
         })
         .done(function(data) {
              tree_sinasignar(data);
         })

}
function seguimientofluvial(item)
{

    //Valida si OTs asociadas
    if(validarasociados(item) == false)
           event.preventdefault();





     var url = UrlHelper.Action("SeguimientoEmbarqueFluvial","Monitoreo","Monitoreo") + "?idembarque=" + item;
     $.get(url, function (data){
         $("#modalcontent").html(data);
         $("#modalcontainer").modal("show");
         $('#horafincarga').mask('00:00');
         $('#horazarpe').mask('00:00');
         $('#horallegada').mask('00:00');
         $('#horaatraque').mask('00:00');
         $('#dtpfechacomp .input-group.date').datepicker({
               todayBtn: "linked",
               keyboardNavigation: false,
               forceParse: false,
               calendarWeeks: true,
               autoclose: true,
               format: 'dd/mm/yyyy'
           });
     })
}
function eliminarembarque(item)
{
   var url = UrlHelper.Action ("JsonEliminarEmbarque", "Monitoreo", "Monitoreo");
   swal({
       title: "Eliminar Embarque",
       text: "¿Está seguro que desea elimianr este Embarque?",
       type: "warning",
       showCancelButton: true,
       cancelButtonText: "Cancelar",
       confirmButtonColor: '#DD6B55',
       confirmButtonText: 'Eliminar',
       closeOnConfirm: false,
       closeOnCancel: true }
     , function (){
              $.ajax({
                  url: url,
                  type: "post",
                  datatype: "json",
                  data: { idembarque: item },
                  success: function (data) {
                      if (data.res) {
                          swal("¡Se eliminó correctamete!", "Se ha eliminado el embarque correctamente", "success");
                          reload();
                          reloaddetalle();
                      }
                      else {
                          swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                      }
                  },
                  error: function (data) {
                      alert(data.Errors.toString());
                  }
            });
          });





}
function verdetalle(item)
{
  $("#idembarque").val(item);
  reloaddetalle();
}

function GuardarAsignacion(idembarque)
{

  ///idembarqueDateTime fechadescarga , string horadescarga , int idpuerto, long idembarque

     var url = UrlHelper.Action("JsonAsignarManifiesto","Monitoreo","Monitoreo") + "?idembarque=" + idembarque;


     $.ajax({
       url: url,
       type: 'POST',
       dataType: 'json',
       data: {}
     })
     .done(function(data) {
      if(data.res)
      {
        messagebox("Registro Exitoso.","Se ha registrado con éxito.", "success");
        $("#modalcontainer").modal("hide");
        reload();

      }
      else
      {
         messagebox("No se asignaron Manifiestos", data.msj, "warning" );
         $("#modalcontainer").modal("hide");
         reload();
      }


    });




}
function validarasociados(item)
{

     var notiene = true;
  var vUrl = UrlHelper.Action("JsonValidarAsociados","Monitoreo","Monitoreo") + "?idembarque=" + item;

  $.ajax({
    url: vUrl,
    type: 'POST',
    dataType: 'json',
    async: false,
    data: {}
  })
  .done(function(data) {
    if(data.res)
    {
      notiene =  true;
    }
    else {
         messagebox("No puede continuar", "No tiene manifiestos asignados.", "warning");
         notiene = false;
    }

  });
  return notiene;

}
