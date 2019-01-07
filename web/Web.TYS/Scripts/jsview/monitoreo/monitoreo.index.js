$(document).on("keydown", "input", function(e) {
  if (e.which==13) e.preventDefault();
});


$(document).ready(function () {

  configurarBotones();
  //obtenerData();
  configurarChosenSelect();
  var data = {
      "Datos": "Busqueda"
  }
  configurarMonitoreo(data);

   $("html, body").animate({ scrollTop: "300px" }, 1000);

   $('#btnBuscar').click(function (){
     var idcliente =     $("#idcliente").val();
     var iddestino =     $("#iddestino").val();
     var hojaruta =     $("#hojaruta").val();
     var manifiesto =     $("#manifiesto").val();
     var numcp =     $("#numcp").val();
     var grr =     $("#grr").val();
     var documento =     $("#documento").val();
     var tienda =     $("#tienda").val();



     var url =  UrlHelper.Action("JsonListarOtsMonitoreo","Monitoreo","Monitoreo")
     + "?idcliente="  + idcliente
     + "&iddestino=" + iddestino
     + "&numhojaruta=" + hojaruta
     + "&nummanifiesto=" + manifiesto
     + "&numcp=" + numcp
     + "&grr=" + grr
     + "&documento=" + documento
     + "&tienda=" + tienda;


            $.ajax({
              url: url,
              type: 'POST',
              dataType: 'json',
              data: {}
            })
            .done(function(data) {
                var tree = $("div#jstree").jstree();
                var data = data;
                tree.settings.core.data = data;
                tree.refresh();

            });





    });



});

function configurarChosenSelect()
{

     var config = {
            '.chosen-select': {
                  max_selected_options: 5,
                 placeholder_text_single: "Seleccione Datos",
                 no_results_text: 'Oops, no se encontró el dato!'
               }
        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
}
function confirmarRecibo(ids)
{

    var url = UrlHelper.Action("ConfirmarRecibo","Monitoreo","Monitoreo");
    $.get(url, function(data){
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");
       $("#idsorden").val(ids);
       $('#horaetapa').mask('00:00');
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

function agregaretapas(ids)
{
    var url = UrlHelper.Action("AgregarEtapa","Monitoreo","Monitoreo");
    $.get(url, function(data){
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");
       $("#idsorden").val(ids);
       $('#horaetapa').mask('00:00');
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
function agregarincidencia(ids)
{
    var url = UrlHelper.Action("AgregarIncidentes","Monitoreo","Monitoreo");
    $.get(url, function(data){
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");
       $("#idsorden").val(ids);
       $('#horaetapa').mask('00:00');
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
function configurarBotones()
{
    $("#btnEtapas").click(function(event) {
      var selectedElms = $('div#jstree').jstree("get_selected", true);
      var checked_ids = [];
      if(selectedElms=='')
      {
        messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
        return;
      }


      $.each(selectedElms, function() {
           if(this.data.idordentrabajo != "")
             checked_ids.push(this.data.idordentrabajo);
      });
      agregaretapas(checked_ids);
    });
    $("#btnIncidencia").click(function(event) {
      var selectedElms = $('div#jstree').jstree("get_selected", true);
      if(selectedElms=='')
      {
        messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
        return;
      }
      var checked_ids = [];
      $.each(selectedElms, function() {
           if(this.data.idordentrabajo != "")
             checked_ids.push(this.data.idordentrabajo);
      });
      agregarincidencia(checked_ids);
    });
    $("#btnConfirmarRecibo").click(function(event) {
      var selectedElms = $('div#jstree').jstree("get_selected", true);
      if(selectedElms=='')
      {
        messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
        return;
      }
      var checked_ids = [];

      $.each(selectedElms, function() {
           if(this.data.idordentrabajo != "")
             checked_ids.push(this.data.idordentrabajo);
      });
      confirmarRecibo(checked_ids);
    });
    $("#btnEntregaCliente").click(function(event) {
      var selectedElms = $('div#jstree').jstree("get_selected", true);
      if(selectedElms=='')
      {
        messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
        return;
      }
      var checked_ids = [];
      var permitido = true;
      $.each(selectedElms, function() {

           if(String(this.data.tipooperacion) == "Tx a Estación")
           {
             messagebox("No puede continuar", "Una o mas OTs son de tipo Tx a Estación.");
             permitido = false;
           }
           else
           {
           if(this.data.idordentrabajo != "")
             checked_ids.push(this.data.idordentrabajo);
           }


      });
      if(permitido == true)
      {
      EntregaCliente(checked_ids);
      }
      else
     { return ;}
    });


    $("#btnListaEventos").click(function(event) {

      var selectedElms = $('div#jstree').jstree("get_selected", true);

      if(selectedElms=='')
      {
        messagebox("No se puede continuar","Debe seleccionar un elemento","warning");
        return;
      }
      var checked_ids = [];

      $.each(selectedElms, function() {
           if(this.data.idordentrabajo != "")
             checked_ids.push(this.data.idordentrabajo);
      });

       if(checked_ids.length>1)
       {
         messagebox("No se puede continuar","Debe seleccionar solo una OT","warning");
         return;
       }

      var url = UrlHelper.Action("Eventos", "Monitoreo", "Monitoreo") + "?id="  + checked_ids ;
      window.location.href = url;



    });
    $("#btnVerGrr").click(function(event) {

      var selectedElms = $('div#jstree').jstree("get_selected", true);

      if(selectedElms=='')
      {
        messagebox("No se puede continuar","Debe seleccionar un elemento","warning");
        return;
      }
      var checked_ids = [];

      $.each(selectedElms, function() {
           if(this.data.idordentrabajo != "")
             checked_ids.push(this.data.idordentrabajo);
      });

       if(checked_ids.length>1)
       {
         messagebox("No se puede continuar","Debe seleccionar solo una OT","warning");
         return;
       }
         vergrr(checked_ids);

    });




}
function EntregaCliente(items)
{


  var url = UrlHelper.Action("Entregacliente", "Monitoreo", "Monitoreo") + "?items=" + items ;
  window.location.href = url;
}
function OnCompleteTransaction_Incidencia(xhr, status)
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

              var idcliente =     $("#idcliente").val();
              var iddestino =     $("#iddestino").val();
              var hojaruta =     $("#hojaruta").val();
              var manifiesto =     $("#manifiesto").val();
              var numcp =     $("#numcp").val();
              var grr =     $("#grr").val();
              var documento  =     $("#documento").val();
                 var tienda =     $("#tienda").val();





              var url =  UrlHelper.Action("JsonListarOtsMonitoreo","Monitoreo","Monitoreo")
              + "?idcliente="  + idcliente
              + "&iddestino=" + iddestino
              + "&numhojaruta=" + hojaruta
              + "&nummanifiesto =" + manifiesto
              + "&numcp=" + numcp
              + "&grr=" + grr
              + "&documento=" + documento
               + "&tienda=" + tienda;



                     $.ajax({
                       url: url,
                       type: 'POST',
                       dataType: 'json',
                       data: {}
                     })
                     .done(function(data) {
                       var tree = $("div#jstree").jstree();
                       var data = data;
                       tree.settings.core.data = data;
                       tree.refresh();
                     })
       });
    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}

function OnCompleteTransaction_Etapa(xhr, status)
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

              var idcliente =     $("#idcliente").val();
              var iddestino =     $("#iddestino").val();
              var hojaruta =     $("#hojaruta").val();
              var manifiesto =     $("#manifiesto").val();
              var numcp =     $("#numcp").val();
              var grr =     $("#grr").val();
              var idestado  =     $("#idestado").val();
              var documento  =     $("#documento").val();
                 var tienda =     $("#tienda").val();


              var url =  UrlHelper.Action("JsonListarOtsMonitoreo","Monitoreo","Monitoreo")
              + "?idcliente="  + idcliente
              + "&iddestino=" + iddestino
              + "&numhojaruta=" + hojaruta
              + "&nummanifiesto=" + manifiesto
              + "&numcp=" + numcp
              + "&grr=" + grr
              + "&documento=" + documento
               + "&tienda=" + tienda;



                     $.ajax({
                       url: url,
                       type: 'POST',
                       dataType: 'json',
                       data: {}
                     })
                     .done(function(data) {
                       var tree = $("div#jstree").jstree();
                       var data = data;
                       tree.settings.core.data = data;
                       tree.refresh();
                     })
       });
    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}
function obtenerData()
{

  var idcliente =     $("#idcliente").val();
  var iddestino =     $("#iddestino").val();
  var hojaruta =     $("#hojaruta").val();
  var manifiesto =     $("#manifiesto").val();
  var numcp =     $("#numcp").val();
  var grr =     $("#grr").val();
  var documento =     $("#documento").val();
  var tienda  =     $("#tienda").val();


    var url =  UrlHelper.Action("JsonListarOtsMonitoreo","Monitoreo","Monitoreo")
    + "?idcliente="  + idcliente
    + "&iddestino=" + iddestino
    + "&numhojaruta=" + hojaruta
    + "&nummanifiesto=" + manifiesto
    + "&numcp=" + numcp
    + "&grr=" + grr
    + "&documento=" + documento
    + "&tienda=" + tienda
    + "&default=1";


         $.ajax({
           url: url,
           type: 'POST',
           dataType: 'json',
           data: {}
         })
         .done(function(data) {
              configurarMonitoreo(data);

         });

}
function vergrr(id)
{
   var url = UrlHelper.Action("VerDetalleOT", "Liquidacion", "Liquidacion") + "?idorden=" + id;

   $.get(url, function (data) {
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");


   })

}
