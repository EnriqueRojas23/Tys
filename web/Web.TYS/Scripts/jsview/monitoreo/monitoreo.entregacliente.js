$(document).on("keydown", "input", function(e) {
//  if (e.which==13) e.preventDefault();
});



$(document).ready(function () {

  $('#EntregaCliente-form').validate({ // initialize the plugin
     rules: {
         idmaestroetapa: {
             required: true,
                 },
        fechaetapa: {
             required: true,
                 },
         horaetapa: {
              required: true,
              minlength: 5
                  },
        documento: { required: true },
        recurso: { required: true }
               },
      messages: {
        idmaestroetapa: {
        required: "Debe seleccionar un tipo entrega.",
        minlength: "Número de caracteres no permitido"
       },
       fechaetapa: {
         required: "Debe ingresar una fecha de entrega."
       },
       horaetapa: {
         required: "Debe ingresar una fecha de entrega.",
         minlength: "Debe ingresar una hora correcta."
       },
       documento : {
         required: "Debe ingresar el DNI.",
         minlength: "Cantidad de caracteres incorrectos",
         maxlength: "Cantidad de caracteres incorrectos",
       },
       recurso : {
         required : "Debe ingresar el NOMBRE."
       }
   },
  //   submitHandler:
  //   $("#frmagregarembarque").on('submit', function () {
  //     if ($("#frmagregarembarque").valid()) {
  //       alert("here some code inside $.AJAX({})");
  //   }
  //   return false;duplicarGuia(item.idordentrabajo, false, item.idguia)
  // })
  });
  $("#idmaestroetapa").change(function(event) {
    var idetapa = $("#idmaestroetapa").val();
     if(idetapa == 5 || idetapa == 8 || idetapa == 9 || idetapa == 10)
     {
    deshabilitarguias();
    }
    else {
        habilitarguias();

    }





  });

  // $(function () {
  // $('#EntregaCliente-form').submit(function (event)
  //   {
  //       event.preventDefault();
  //       swal({
  //         title: "Confirmar Entrega",
  //         text: "¿Esta seguro de que desea confirmar la entrega?",
  //         type: "warning",
  //         showCancelButton: true,
  //         confirmButtonColor: "#DD6B55",
  //         confirmButtonText: "Confirmar",
  //         cancelButtonText: "Cancelar",
  //         showLoaderOnConfirm: true,
  //         closeOnConfirm: false,
  //         closeOnCancel: false
  //       },
  //       function ()
  //       {
  //
  //                 $('#EntregaCliente-form').unbind('submit').submit()
  //       });
  //   });
  // });

$("#btnCancelar").click(function(event) {
  var url = UrlHelper.Action("OperacionesMonitoreo", "Monitoreo", "Monitoreo");
  window.location.href = url;
});
      $("#numcp").blur(function(event) {
        if($("#numcp").val()== '') return;
          buscarot();
      });

      $("#btnAgregarGuia").click(function(event) {

        var guia =  $("#guia").val();

          cargarguia(guia);

      });
      $("html, body").animate({ scrollTop: "100px" });
      $('#horaetapa').mask('00:00');

     $('#dtpfechacomp .input-group.date').datepicker({
           todayBtn: "linked",
           keyboardNavigation: false,
           forceParse: false,
           calendarWeeks: true,
           autoclose: true,
           format: 'dd/mm/yyyy'

       });
     configurargrillaguias_light();

     if($("#idsorden").val() != '')
     {

       if($("#idsorden").val().split(',').length>1)
       {
         $("#numcp").attr('disabled', 'disabled');
         $("#numcp").val('Varios');
         $("#nummanifiesto").val('Varios');
         $("#destinatario").val('Varios');
       }
       else {
             $("#numcp").val($("#numcp_aux").val());
             buscarot();
       }

     }


});

function buscarot()
{
    var orden = $("#numcp").val();

    var url = UrlHelper.Action("JsonBuscarOt","Monitoreo","Monitoreo");

    $.ajax({
      url: url,
      type: 'POST',
      dataType: 'json',
      async: true,
      data: {'numcp' : orden}
    })
    .done(function(data) {
       if(data.res)
       {
          $("#nummanifiesto").val(data.obj.nummanifiesto);
          $("#destinatario").val(data.obj.destinatario);
          $("#idsorden").val(data.obj.idordentrabajo);
       }
       else
       {
             messagebox("La OT ingresada no existe o ya fue entregada", "La OT que intenta buscar no existe o ya fue entregada.", "warning");
       }

    })
    .fail(function() {
      console.log("error");
    });




}
function cargarguia(guia)
{

     var cantidad = $("#cantidad").val();
     if(guia=='')
     {
           messagebox("No se puede agregar", "Ingrese un número de guía","warning");
           return;
     }
     if(cantidad<=0)
     {
           messagebox("No se puede agregar", "Ingrese una cantidad mayor a 0","warning");
           return;
     }


    var url =  UrlHelper.Action ("JsonAgregarGuia","Monitoreo","Monitoreo");
    $.ajax({
      async: true,
      url: url,
      type: 'POST',
      dataType: 'json',
      data: {'numeroguia': guia , "cantidad" : cantidad , idorden : $("#idsorden").val() }
    })
    .done(function(data) {
        if(data.res)
        {
           reloadgridguias_light();
           $("#guia").val('');
           $("#cantidad").val('');
        }
        else {
          messagebox("No se puede agregar",data.msj,"warning");

          }

    })
    .fail(function() {
      console.log("error");
    })
}
function OnCompleteTransaction(xhr, status)
{
  var jsonres = xhr.responseJSON;
    if (jsonres.res == true)
    {
       //messagebox("Registro Exitoso.","Se ha registrado la entrega", "success");

            //  var url = UrlHelper.Action("Entregacliente", "Monitoreo", "Monitoreo");
            //  window.location.href = url;
            //  }
            //  else
            //  {
               var url = UrlHelper.Action("OperacionesMonitoreo", "Monitoreo", "Monitoreo");
               window.location.href = url;
          //    }
          //  });
    }
    else
    {
           messagebox("No puede continuar", jsonres.msj, "warning");

    }
}
function habilitarguias()
{
  $("#guia").val('');
  $("#guia").removeAttr('disabled');

  $("#cantidad").val('0');
  $("#cantidad").removeAttr('disabled');


  reloadgridguias_light();
}
function deshabilitarguias()
{
     $("#guia").val('');
     $("#guia").attr('disabled', 'true');

     var grilla = $("#gridguias");
     $(grilla).jqGrid('clearGridData');

     $("#cantidad").val('0');
     $("#cantidad").attr('disabled', 'true');





}
