var btnNuevo = "#btnNuevo";

$(document).ready(function () {
  preinicio();
  configurarChosenSelect();


    BeginDropDownlist();




    $("#idtipovehiculo").change(function ()
    {
        JsonConceptoCobro();

    });

     $("#idformula").change(function () {

         JsonConceptoCobro();

     });
     if ( $('#iddestino').val()!="")
     JsonListarFormulayTipoTransporte();



});
function BeginDropDownlist()
{
  BeginDropDownlistDestino();
  BeginDropDownlistOrigen();
  BeginDropDownlistCliente();
}

function configurarChosenSelect()
{

     var config = {
            '.chosen-select': {
                  max_selected_options: 5,
                 placeholder_text_single: "Seleccione Conceptos",
                 no_results_text: 'Oops, no se encontró el dato!'
               }
        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
}

function preinicio()
{

  $("#btnagregar").click(function(event) {
   var url = UrlHelper.Action("AgregarClienteModal","Orden","Seguimiento");

        $.get(url, function(data) {
           $("#modalcontent").html(data);
           $("#modalcontainer").modal("show") ;
          configurarChosenSelect();

        })


    /* Act on the event */
  });


   $("html, body").animate({ scrollTop: "100px" });

   $("#cantidad").keypress(function (event) {
                if (event.which == 13) {

                    cargarguias();
                    $("#numeroinicial").val('');
                    $("#cantidad").val('1');

                }
            });
    $("#numeroinicial").keypress(function (event) {
                if (event.which == 13) {

                     cargarguias();
                     $("#numeroinicial").val('');
                     $("#cantidad").val('1');


                }
            });



 $("#addrow").click( function() {
        $("#gridguias").jqGrid('addRowData',0,1,"first");
        $("#gridguias").editRow(0,false);
  });




    $('input').keypress(function(event){
        var enterOkClass =  $(this).attr('class');
        if (event.which == 13 && enterOkClass != 'enterSubmit') {
            event.preventDefault();
            return false;
        }
    });

     $('div').keypress(function(event){
        var enterOkClass =  $(this).attr('class');
        if (event.which == 13 && enterOkClass != 'enterSubmit') {
            event.preventDefault();
            return false;
        }
    });
}


function BeginDropDownlistCliente()
{


     $("#idcliente").change(function ()
    {

          if ( $('#idorigen').val()=="")
             return ;

           JsonListarFormulayTipoTransporte();
             JsonTipoUnidad();




     ///////CARGAR FORNULAS Y MODO TRANSPORTEOnCompleteTransaction



               if($('#idformula').val() == '') return;
               if($('#idtipovehiculo').val() == '') return;

               JsonConceptoCobro();


   });
}

function BeginDropDownlistOrigen()
{

    $('#idorigen').change(function(){
        if ( $("#idcliente").val() == "") return "";
        if ( $("#idorigen").val() == "") return "";
        if ( $("#iddestino").val() == "") return "";
         JsonListarFormulayTipoTransporte();
         JsonTipoUnidad();
  })

}
function BeginDropDownlistDestino()
{
    $('#iddestino').change(  function(){
        if($("#idcliente").val() == "") { return ;}
        JsonListarFormulayTipoTransporte();
        JsonTipoUnidad();
     })
}
function   configurarChosenSelect()
{

     var config = {
            '.chosen-select': {
                  max_selected_options: 5,
                 placeholder_text_multiple: "Seleccione Conceptos",
                 no_results_text: 'Oops, no se encontró el Concepto!' }
        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

}






function validateFloatKeyPress(el, evt)
{

   var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1)
    {
        var range = document.selection.createRange();

        if (range.text != ""){
        }
        else
        {
            var number = el.value.split('.');
            if (number.length == 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
}


function JsonListarFormulayTipoTransporte()
{




       if($("#iddestino").val() == "")
       {
            var $select = $('#idformula');
            $select.empty();

            var $select2 = $('#idtipovehiculo');
            $select2.empty();

            var $select3 = $('#idconceptocobro');
            $select3.empty();
            $('#idconceptocobro').trigger("chosen:updated");

            return;

       }




       var urlinit = UrlHelper.Action ("JsonListarFormulayTipoTransporteCamion","Orden","Seguimiento");
           $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: {
                   "idcliente": $("#idcliente").val(),
                   "idorigen": $("#idorigen").val(),
                   "iddestino": $("#iddestino").val()


                  },
                 success: function (data) {
                       var $select = $('#idformula');
                       $select.empty();
                       $("#idformula").append('<option value="">[Formula]</option>');
                       $.each(data.listaformula, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                        $('#idformula').trigger("chosen:updated");


                        var length = $('#idformula > option').length;
                        if(length==2)
                        {
                           $select[0].selectedIndex = 1;
                           $select.change();
                        }


                        var $select = $('#idtipovehiculo');
                         $select.empty();
                         $("#idtipovehiculo").append('<option value="">[TipoTransporte]</option>');
                         $.each(data.listatipotransporte, function (i, state) {
                             $('<option>', {
                                 value: state.Value
                             }).html(state.Text).appendTo($select);
                         });
                          $('#idtipovehiculo').trigger("chosen:updated");

                        var length = $('#idtipovehiculo > option').length;
                        if(length==2)
                        {
                           $select[0].selectedIndex = 1;
                           $select.change();
                        }



                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });


}

function JsonConceptoCobro()
{



           var urlinit4 = UrlHelper.Action ("JsonListarConceptoCobroT","Orden","Seguimiento");


            $.ajax(
                        {
                         type: "POST",
                         async: true,
                         url: urlinit4,
                         data: {
                           "idcliente": $("#idcliente").val(),
                           "idorigen": $("#idorigen").val(),
                           "iddestino": $("#iddestino").val(),
                            "idformula" : $("#idformula").val(),
                           "idtipotransporte" :  $("#idtipotransporte").val()

                          },
                         success: function (data) {
                               var $select = $('#idconceptocobro');
                               $select.empty();
                               $("#idconceptocobro").append('<option value="">[ConceptoCobro]</option>');
                               $.each(data, function (i, state) {
                                   $('<option>', {
                                       value: state.Value
                                   }).html(state.Text).appendTo($select);
                               });

                          var length = $('#idconceptocobro > option').length;

                          if(length==2)
                          {
                             $select[0].selectedIndex = 1;
                             $select.change();
                          }


                                $('#idconceptocobro').trigger("chosen:updated");


                           },
                           error: function (request, status, error)
                           {
                               swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                           }

                        });


}
function Recalcular()
{

  var formula = $('#idformula').val();
  if(formula == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe seleccionar una fórmula!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }


  var idorigen = $('#idorigen').val();
  if(idorigen == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un origen!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }
  var iddestino = $('#iddestino').val();
  if(iddestino == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un destino!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }
  var idcliente = $('#idcliente').val();
  if(idcliente == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un cliente!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }
  var idunidad = $('#idclientetipounidad').val();
  if(idunidad == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un tipo de unidad!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }







  var idconceptocobro =  $('#idconceptocobro').val();
  if(idconceptocobro != null)
   idconceptocobro = idconceptocobro.toString();


 var sobreestadia = $('#costoextra').val();



   var url = UrlHelper.Action("CalcularPagoCamionCompleto", "Orden", "Seguimiento") ;
        $.ajax(
            {
                type: "GET",
                async: true,
                url: url,
                data: {
                 "formula" : formula
                , "destino" : iddestino
                , "origen" : idorigen
                , "cliente" : idcliente
                , "unidad" : idunidad
                ,"idconceptocobro" : idconceptocobro
                ,"sobreestadia" : sobreestadia
               },
                success: function (data) {

                      if(data.res == false)
                      {
                         swal({ title: "Error!", text: data.mensaje , type: "error", confirmButtonText: "Aceptar" });
                       $('#subtotal').val('');
                       $('#igv').val('');
                       $('#total').val('');

                       $('#base1').val('');
                       $('#tarifa').val('');
                       $('#minimo').val('');

                      }
                      else{

                       $('#subtotal').val(data.subtotal);
                       $('#igv').val(data.igv);
                       $('#total').val(data.total);

                       $('#base1').val(data.base1);
                       $('#tarifa').val(data.tarifa);
                       $('#minimo').val(data.minimo);

                     }

                },
                error: function (request, status, error) {
                    swal({ title: "Error!", text: "Ocurrio un error." , type: "error", confirmButtonText: "Aceptar" });
                }
            });

}
function JsonTipoUnidad()
{



           var urlinit4 = UrlHelper.Action ("JsonListarUnidadCamion","Orden","Seguimiento");


            $.ajax(
                        {
                         type: "POST",
                         async: true,
                         url: urlinit4,
                         data: {
                           "idcliente": $("#idcliente").val(),
                           "idorigen": $("#idorigen").val(),
                           "iddestino": $("#iddestino").val(),

                          },
                         success: function (data) {
                               var $select = $('#idclientetipounidad');
                               $select.empty();
                               $("#idclientetipounidad").append('<option value="">[Tipo Unidad]</option>');
                               $.each(data.listatipounidad, function (i, state) {
                                   $('<option>', {
                                       value: state.Value
                                   }).html(state.Text).appendTo($select);
                               });

                          var length = $('#idclientetipounidad > option').length;
                          if(length==2)
                          {
                             $select[0].selectedIndex = 1;
                             $select.change();
                          }


                          $('#idclientetipounidad').trigger("chosen:updated");


                           },
                           error: function (request, status, error)
                           {
                               swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                           }

                        });


}
function OnCompleteTransaction(xhr, status)
{
   var jsonres = xhr.responseJSON;
    //CleanValidationError();

    if (jsonres.res == true)
    {
            var url = UrlHelper.Action("OrdenesRapidas", "Orden", "Seguimiento");
            window.location.href = url;
    }
      else
    {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }
}
function RegistrarContinuar()
{
    var url = UrlHelper.Action("OrdenesRapidas", "Orden", "Seguimiento");
         window.location.href = url;
}
