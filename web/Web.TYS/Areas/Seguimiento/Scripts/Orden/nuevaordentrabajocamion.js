var btnNuevo = "#btnNuevo";
var gridlistaots = "#gridguias";
var gridlistaotspager = "#gridguiaspager";

$(document).keydown(function(event) {
    if (event.keyCode == 9) {  //tab pressed

       event.stopPropagation();

     };
})

$(document).ready(function () {

//Configuraciones
    preinicio();
    configurarChosenSelect();
    focusstart();
    BeginDropDownlist();
    configurarGrillaOT();


    $("#idvehiculo").change();
   // CargaListaots();
   // focusstart();

});




function preinicio()
{


   $("html, body").animate({ scrollTop: "100px" });
   $("#idvehiculo").focus();

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
        $("#gridguias").editRow(0,true);
  });


    var $select = $('#identregara');
   $select[0].selectedIndex = 1;
   $select.change();


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

function BeginDropDownlist()
{

   BeginDropDownlistVehiculo();
   BeginDropDownlistOrigen();
   BeginDropDownlistDestino();
   BeginDropDownlistCliente();
   BeginCheckbox1();
   BeginDropDownlistRemitente();
   BeginCheckbox2();
   BeginDropDownlistDestinatario();




    $("#idtipotransporte").change(function ()
    {
        JsonConceptoCobro();

    });

     $("#idformula").change(function () {

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



  })

}

function BeginDropDownlistDestinatario()
{


    $("#iddestinatario").change(function ()
    {

        var iddestinatario = $("#iddestinatario").val();
        var iddestino = $("#iddestino").val();
        $("#iddestinatario_selected").val($('#iddestinatario').val());


        if(iddestinatario=='')
        {
            $('#rucdestinatario').attr('disabled', false);
            $('#razonsocialdestinatario').attr('disabled', false);
            $('#rucdestinatario').val('');
            $('#razonsocialdestinatario').val('');
            return ;

        }



        if(iddestinatario=='')
        {
           swal({ title: "¡Registro OT!", text: "¡Debe seleccionar a un Destinatario!", type: "error", confirmButtonText: "Aceptar" });
           return ;
        }
        JsonListarDireccionesAutoComplete();

        //////////////////////77

        var urlcliente = UrlHelper.Action ("JsonGetCliente","Orden","Seguimiento");
        $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlcliente ,
                 data: { "idcliente": iddestinatario },
                 success: function (data) {

                      $('#rucdestinatario').val(data.ruc);
                      $('#razonsocialdestinatario').val(data.razonsocial);

                      $('#rucdestinatario').attr('disabled', true);
                      $('#razonsocialdestinatario').attr('disabled', true);



                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });






      });

}

function BeginCheckbox2()
{
     $("#cbox2").change(function ()
    {

      if($('#cbox2').is(':checked')==true)
      {
          //$('#iddestinatario').attr('disabled', true);
          $('#iddestinatario').val($("#idcliente").val());
          $('#iddestinatario').change();
          $('#iddestinatario').trigger("chosen:updated");

      }
      else
      {
           // $('#iddestinatario').attr('disabled', false);
            $('#iddestinatario').trigger("chosen:updated");
      }


    });
}

function BeginDropDownlistRemitente()
{
   $("#idremitente").change(function ()
    {

        var idcliente = $("#idremitente").val();
        $('#idremitente_selected').val(idcliente);


        var urlinit = UrlHelper.Action ("ListarDirecciones","Orden","Seguimiento");
        if(idcliente=='')
        {
           swal({ title: "¡Registro OT!", text: "¡Debe seleccionar a un remitente!", type: "error", confirmButtonText: "Aceptar" });
           return ;
        }

        $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: { "idcliente": idcliente

                    },
                 success: function (data) {
                       var $select = $('#idremitentedireccion');
                       $select.empty();
                       $("#idremitentedireccion").append('<option value="">[Direcciones]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });

                           $select[0].selectedIndex = 1;
                           $select.change();



                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });
      });
}
function BeginCheckbox1()
{
     $("#cbox1").change(function ()
    {

      if($('#cbox1').is(':checked')==true)
      {
           var $select = $('#idremitente');
           $select.empty();
           $("#idremitente").append('<option value="">[Remitente]</option>');
               $('<option>', {
                   value: $("#idcliente").val()
               }).html($('select[name=idcliente] option:selected').text()).appendTo($select);

          $('#idremitente').val($("#idcliente").val());
          $('#idremitente').change();
          $('#idremitente').trigger("chosen:updated");

      }
      else
      {
            var urlinit = UrlHelper.Action ("JsonListarClientesProveedor","Orden","Seguimiento");
           $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: { "id": $("#idcliente").val() },
                 success: function (data) {
                       var $select = $('#idremitente');
                       $select.empty();
                       $("#idremitente").append('<option value="">[Remitente]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                        $('#idremitente').trigger("chosen:updated");


                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });

             $('#idremitente').trigger("chosen:updated");
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


           var urlinit = UrlHelper.Action ("JsonListarClientesProveedor","Orden","Seguimiento");
           $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: { "id": $("#idcliente").val() },
                 success: function (data) {
                       var $select = $('#idremitente');
                       $select.empty();
                       $("#idremitente").append('<option value="">[Remitente]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                        $('#idremitente').trigger("chosen:updated");


                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });


      if($('#cbox1').is(':checked')==true)
      {
             $('#idremitente').val($("#idcliente").val());
             $('#idremitente').change();

      }
      if($('#cbox2').is(':checked')==true)
      {
             $('#iddestinatario').val($("#idcliente").val());
             $('#iddestinatario').change();
             $('#iddestinatario').trigger("chosen:updated");

      }

     ///////CARGAR FORNULAS Y MODO TRANSPORTE



               if($('#idformula').val() == '') return;
               if($('#idtipotransporte').val() == '') return;

               JsonConceptoCobro();

   });
}

function BeginDropDownlistDestino()
{



    $('#iddestino').change(  function(){


        if($("#iddestinatario").val() != "")
        {
            JsonListarDireccionesAutoComplete();
        }

        if($("#idcliente").val() == "") { return ;}

        JsonListarFormulayTipoTransporte();





  })

}
function BeginDropDownlistVehiculo()
{


    $("#idvehiculo").change(function ()
    {
        var idvehiculo = $("#idvehiculo").val();
        var urlinit = UrlHelper.Action ("ObtenerDatosVehiculo","Orden","Seguimiento");

        if(idvehiculo=='')
        {
         $('#guiarecojo').attr('required',false );
         $('#placa').val('');
         $('#dni').val('');
         $('#nombrechofer').val('');
         return ;
        }
        else
           $('#guiarecojo').attr('required', true);



        $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: { "idvehiculo": idvehiculo },
                 success: function (data) {
                           $("#placa").val(data.placa);
                           $("#nombrechofer").val(data.nombrechofer);
                           $("#dni").val(data.dni);

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });
      });


}


function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true)
    {

        swal({
              title: "¡Registro Exitoso!",
              text: "¿Desea registrar otra Orden para este mismo cliente?",
              type: "warning",
              showCancelButton: true,
              confirmButtonColor: "#DD6B55",
              confirmButtonText: "Si, agregar otro!",
              cancelButtonText: "No!",
              closeOnConfirm: false,
              closeOnCancel: false
            },
            function(isConfirm){
              if (isConfirm) {


                     $("html, body").animate({ scrollTop: "100px" });
                     $("#idvehiculo").focus();

                     $("#iddestino").prop('selectedIndex', 0)

                     $('#iddestino').trigger("chosen:updated");


                     JsonListarDireccionesAutoComplete();
                     JsonListarFormulayTipoTransporte();

                     $("#puntopartida").focus();






                     $("#iddestinatario").prop('selectedIndex', 0)
                     $("#cbox2").prop('checked' , false);



                     $('#iddestinatario').trigger("chosen:updated");
                          var iddestinatario = $("#iddestinatario").val();
                          var iddestino = $("#iddestino").val();
                          if(iddestinatario=='')
                          {
                              $('#rucdestinatario').attr('disabled', false);
                              $('#razonsocialdestinatario').attr('disabled', false);
                              $('#rucdestinatario').val('');
                              $('#razonsocialdestinatario').val('');
                              $('#destinatariodireccion').val('');
                              $('#descripciongeneral').val('');
                              $('#numeroinicial').val('');
                              $('#bultogeneral').val('');
                              $('#pesogeneral').val('');

                              $('#volgeneral').val('');
                              $('#documentosgeneral').val('');





                          }

                      var grilla = $("#gridguias");
                      var vdataurl = $(grilla).data("dataurl") ;
                      $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');







                     swal("Listo!", "Ud. Puede registrar ahora una nueva Orden.", "success");

              } else {


                var url =  UrlHelper.Action ("Ordenes","Orden","Seguimiento");
                 window.location.href = url;
              }
            });

    }
    else
    {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }

}



function btnBuscarPersona_onclick(obj, event) {

  var url = UrlHelper.Action("AgregarOTModal", "Facturacion", "Facturacion") ;
    $.get(url, function (data)
    {
        $("#modalcontentot").html(data);
        $("#modalcontainerot").modal("show");
        configurarGrillaOT();

    });

}
function eliminarcomprobante(id)
{
    var vUrl = UrlHelper.Action("EliminarComprobante", "Facturacion", "Facturacion") + "?id=" + id;
    swal({
        title: "Eliminar Comprobante",
        text: "¿Está seguro que desea eliminar este Comprobante?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Eliminar',
        closeOnConfirm: false,
        closeOnCancel: true
    },
       function (isConfirm) {
           if (isConfirm) {
               $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {
                            swal("¡Se ha eliminado correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                            oDocumentosTable.draw();

                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.Errors.toString());
                   }
               });
           }
     });
}
function cerrarAgregarOT()
{
   $("#modalcontainerot").modal("hide");
}
function configurarGrillaOT() {

    $.jgrid.defaults.width = 400;
    $.jgrid.defaults.height = 180;

    var grilla = $(gridlistaots);
    var pagergrilla = $(gridlistaotspager);
    //var nombre = $('#NombreRol').val();
    var vdataurl = $(grilla).data("dataurl") + '?id='  ;
    var vdataedit = $(grilla).data("editurl");



    $(grilla).jqGrid({
        url: vdataurl,
        responsive:true,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['','GR','Bulto', 'Peso Kg.', 'Volumen m3 ','Peso Volm ','Documento','Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idguiaremisioncliente', index: 'idguiaremisioncliente' },
            { key: false, hidden: false, editable: true ,editrules: { required: true}, name: 'nroguia', index: 'nroguia', width: '100', align: 'left' },

            { key: false, hidden: false, editable: true ,editrules: { required: true}, name: 'bulto', index: 'bulto', width: '100', align: 'left',editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  SoloNumerico(e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }
          },

            { key: false, hidden: false, editable: true , editrules: { required: true}, name: 'peso', index: 'peso', width: '100', align: 'left'
                      ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  SoloDecimal(e,this.value);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }
          },





            { key: false, hidden: false, editable: true  , name: 'volumen', index: 'volumen', width: '100', align: 'left'  ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  SoloDecimal(e,this.value);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }
          },
            { key: false, hidden: false, editable: true ,name: 'pesovol', index: 'pesovol', width: '100', align: 'left' ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  SoloDecimal(e,this.value);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }
          },
            { key: false, hidden: false, editable: true , name: 'documento', index: 'documento', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'idguiaremisioncliente', index: 'idguiaremisioncliente', width: '100', align: 'center', formatter: bottonasignarrol_formatter }

        ],
        jsonReader: CONFIG_JQGRID.get('jsonReader'),
        pager: $(gridlistaotspager),
        rowNum: 10,
        rowList: [10, 20],
        emptyrecords: 'No se encontraron registros',
        autoheight: true,
        responsive : true,
        autowidth: true,
        shrinkToFit: true,
        editable:true,
        addParams: {
              position: "last",
              addRowParams: editOptionsNew
              },
        editParams: editOptionsNew,
        editurl: vdataedit,
       onSelectRow: function (rowid, status) {
            //updateIdsOfSelectedRows(rowid, status);


        },

        afterInsertRow: function(id, currentData, jsondata) {

        },
         beforeSubmit: function () {
          },



        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: 0
        },


    });
}
var editOptionsNew = {
        keys: true,
        successfunc: function () {
            var $self = $(this);
            setTimeout(function () {
                $self.trigger("reloadGrid");
            }, 50);
        }
    };


function bottonasignarrol_formatter(cellvalue, options, rowObject) {

        var editar = "<button type='button' title='Editar' class='btn btn-success btn-xs btn-outline' onclick=\"$('#gridguias').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId  + "', '' );\"><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridguias').restoreRow('" + options.rowId + "'); successfunc(); \"><i class='fa fa-times-circle'></i> </button>";


        return editar + guardar + control + restore;
}
function successfunc ()
{
      var grilla = $("#gridguias");
      var vdataurl =  UrlHelper.Action("JsonGetListarGuias", "Orden", "Seguimiento") ;
      $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function formatedit (cellvalue, options, rowObject)
{

    return " "  + cellvalue ;

}


function BuscarOTPopUp_onClick() {


   var id = $('#txtnumcp').val();
  var vUrl = UrlHelper.Action("AgregarOT", "Facturacion", "Facturacion");
   $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {

                    var grilla = $("#gridlistaots");
                    //var tipodocumento = $("#txtnumcp").val().trim();
                    var numdocumento = $("#txtnumcp").val();
                    var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numdocumento ;

                    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                    $('#ots').append( $('<option></option>').val(data.ids).html(data.value) ) ;
                    $('#txtnumcp').val("");


                       } else {
                           swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.Errors.toString());
                   }
               });

}

function CancelarOTP_onClick()
{
      $("#modalcontainernew").modal("hide");
}
function GuardarOTPopUp_onClick()
{
     $("#modalcontainerot").modal("hide");

}




function agregarProveedor()
{
  var url = UrlHelper.Action("AgregarProveedorModalHelp", "Facturacion", "Facturacion") ;
    $.get(url, function (data)
    {
        $("#modalcontentnew").html(data);
        $("#modalcontainernew").modal("show");
        //nfigurarGrillaOT();

    });
}

function Guardar_NuevoProveedor()
{
        var ruc=$("#ruc").val();
        var razon   = $("#razonsocial").val();
        var url = UrlHelper.Action("AgregarProveedorAux", "Facturacion", "Facturacion") ;


        $.ajax(
            {
                type: "POST",
                async: true,
                url: url,
                data: { "ruc": ruc , "razonsocial" : razon },
                success: function (data) {
                        $("#modalcontentnew").modal("hide");

                },
                error: function (request, status, error) {
                    swal({ title: "Error!", text: "¡No se pudo cargar los datos de la dirección!", type: "error", confirmButtonText: "Aceptar" });
                }
            });
}



function cargarguias()
{


        var cantidad =  $("#cantidad").val();
        var numeroinicial   = $("#numeroinicial").val();

        if(numeroinicial=='')
        {
           swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un número inicial!", type: "error", confirmButtonText: "Aceptar" });
           return;
        }
              if(cantidad=='')
        {
           swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar una cantidad!", type: "error", confirmButtonText: "Aceptar" });
           return;
        }




        var url = UrlHelper.Action("AgregarGuiasNumeroInicial", "Orden", "Seguimiento") ;


        $.ajax(
            {
                type: "POST",
                async: true,
                url: url,
                data: { "numeroinicial": numeroinicial , "cantidad" : cantidad },
                success: function (data) {

                      if(data.res == false)
                      {
                        swal({ title: "¡Ya existe!", text: "¡Uno o mas números de guias que intenta ingresar ya existe.!", type: "error", confirmButtonText: "Aceptar" });
                      }
                      else
                      {
                          $('#txtpeso').val(data.peso);
                          $('#txtbultos').val(data.bulto);
                          $('#txtcantidad').val(data.cantidad);
                      }
                       var vdataurl = $(gridlistaots).data("dataurl")  ;
                       $(gridlistaots).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                },
                error: function (request, status, error) {
                    swal({ title: "Error!", text: "¡No se pudo generar las guías!", type: "error", confirmButtonText: "Aceptar" });
                }
            });





}




function CargaListaots() {

    oDocumentosTable =
       $('.dataTables-tblOrdenes').DataTable({
           responsive: true,
           dom: '<"html5buttons"B>lTfgitp',
           "processing": true,
           "serverSide": true,
           "oLanguage": {
                "oPaginate": {
                    "sPrevious": "<< Atrás",
                    "sNext" : "Siguiente >>",
                    "sFirst": "<<",
                    "sLast": ">>"
                    },
                "sSearch" : "Búsqueda:"
                ,"sInfo": "_START_ de _END_"
                ,"sLengthMenu":  "Paginación :  _MENU_"
                },



           "ajax": {
               "url": $('#tblOrdenes').data("url"),
               "data": function (d) {
                   d.numcp = $('#numcp').val();

               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idordentrabajo", "name": "idordentrabajo", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   {
                       "title": "Número OT", "data": "numcp", "name": "numcp",visible: true, "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "Cliente", "data": "razonsocial", "name": "razonsocial", "autoWidth": true, "class": "text-center" },
                   { "title": "Modo Transporte", "data": "idtipotransporte", "name": "idtipotransporte", "autoWidth": true, "class": "text-center" },
                   { "title": "Concepto Cobro", "data": "idconceptocobro", "name": "idconceptocobro", "autoWidth": true, "class": "text-center" },
                   { "title": "Destino", "data": "destino", "name": "destino", "autoWidth": true, "class": "text-center" },
                   { "title": "Remitente", "data": "remitente", "name": "remitente", "autoWidth": true, "class": "text-center" },
                   { "title": "Destinatario", "data": "destinatario", "name": "destinatario", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idcomprobante", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarcomprobante(" + data + ");' href='#' > <i class='fa fa-edit'></i> </button>"
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminarcomprobante(" + data + ");' href='#' > <i class='fa fa-trash'></i> </button></div>";

                        }
                   },

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Comprobantes', exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               { extend: 'pdf', title: 'Listado de Comprobantes' , exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },

           ]

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
  var idtipotransporte = $('#idtipotransporte').val();
  if(idtipotransporte == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un tipo transporte!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }

  var pesogeneral = $('#pesogeneral').val();
  var volgeneral = $('#volgeneral').val();


 if(pesogeneral == ""  && volgeneral == "" )
   {
   pesogeneral = 0;
   volgeneral = 0;
   var grid = $('#gridguias');
   var rows = grid.jqGrid('getDataIDs');

    for (i = 0; i < rows.length; i++)
    {


        var rowData = grid.jqGrid('getRowData', rows[i]);

        if(rowData['peso']!="")
        pesogeneral  = pesogeneral +  parseFloat(rowData['peso']);
        if(rowData['volumen']!="")
        volgeneral  = volgeneral + parseFloat(rowData['volumen']);
        if(volgeneral == "")
          volgeneral = 0;



    }


   }



  if(pesogeneral == ""  || pesogeneral == null )
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un peso general!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }

  if(volgeneral.toString() == "")
  {
     swal({ title: "¡Orden Transporte!", text: "¡Debe ingresar un volumen general!", type: "error", confirmButtonText: "Aceptar" });
     return ;
  }


  var idconceptocobro =  $('#idconceptocobro').val();
  if(idconceptocobro != null)
   idconceptocobro = idconceptocobro.toString();




   var url = UrlHelper.Action("CalcularPago", "Orden", "Seguimiento") ;
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
                , "tipotransporte" : idtipotransporte
                , "peso" : pesogeneral
                ,"volumen" : volgeneral
                ,"idconceptocobro" : idconceptocobro
               },
                success: function (data) {

                      if(data.res == false)
                      {
                         swal({ title: "Error!", text: data.mensaje , type: "error", confirmButtonText: "Aceptar" });
                       $('#subtotal').val('');
                       $('#impuesto').val('');
                       $('#total').val('');

                       $('#base1').val('');
                       $('#tarifa').val('');
                       $('#minimo').val('');

                      }
                      else{

                       $('#subtotal').val(data.subtotal);
                       $('#impuesto').val(data.igv);
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

function irEliminar(id)
{

  var vUrl = UrlHelper.Action("Elimnarguia", "Orden", "Seguimiento");
   $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {

                      var grilla = $("#gridguias");
                      var vdataurl = $(grilla).data("dataurl") ;
                      $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

//                      event.preventDefault();


                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {

                   }
               });




}
function focusstart()
{
       setcolor ($("#placa"));
       setcolor ($("#dni"));
       setcolor ($("#nombrechofer"));
       setcolor ($("#guiarecojo"));
       setcolor ($("#idestacionorigen"));
       setcolor ($("#idcargadaen"));
       setcolor ($("#idremitentedireccion"));
       setcolor ($("#rucdestinatario"));
       setcolor ($("#razonsocialdestinatario"));
       setcolor ($("#destinatariodireccion"));
       setcolor ($("#identregara"));
       setcolor ($("#personarecojo"));
       setcolor ($("#idformula"));
       setcolor ($("#idtipotransporte"));
       setcolor ($("#idtipomercaderia"));
       setcolor ($("#dnipersonarecojo"));

       setcolor ($("#descripciongeneral"));
       setcolor ($("#numeroinicial"));
       setcolor ($("#cantidad"));
       setcolor ($("#bultogeneral"));
       setcolor ($("#pesogeneral"));
       setcolor ($("#volgeneral"));
       setcolor ($("#documentosgeneral"));













}
function setcolor (control)
{
    $(control).focus(function() {
        $(control).css("background-color","rgb(255, 248, 177)");
       });
     $(control).blur(function() {
        $(control).css("background-color","#ffffff");
       });

}




function rowSave(id,str)
{

  $("#gridguias").jqGrid('saveRow',id, finalizando);

}
function finalizando(resonsse)
{

  var respuesta =  JSON.parse(resonsse.responseText);

   if(respuesta.res == false)
   {
     swal({ title: "Error", text: "Guía duplicada", type: "error", confirmButtonText: "Aceptar" });
     successfunc();
     return false;
  }
  else {
    successfunc();
    return true;
  }

}



function JsonListarFormulayTipoTransporte()
{




       if($("#iddestino").val() == "")
       {
            var $select = $('#idformula');
            $select.empty();

            var $select2 = $('#idtipotransporte');
            $select2.empty();

            var $select3 = $('#idconceptocobro');
            $select3.empty();
            $('#idconceptocobro').trigger("chosen:updated");

            return;

       }




       var urlinit = UrlHelper.Action ("JsonListarFormulayTipoTransporteOT","Orden","Seguimiento");
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


                        var $select = $('#idtipotransporte');
                         $select.empty();
                         $("#idtipotransporte").append('<option value="">[TipoTransporte]</option>');
                         $.each(data.listatipotransporte, function (i, state) {
                             $('<option>', {
                                 value: state.Value
                             }).html(state.Text).appendTo($select);
                         });
                          $('#idtipotransporte').trigger("chosen:updated");

                        var length = $('#idtipotransporte > option').length;
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
function JsonListarDireccionesAutoComplete()
{

        var iddestinatario = $("#iddestinatario").val();
        var iddestino = $("#iddestino").val();


        var urlinit3 = UrlHelper.Action ("ListarDireccionesAutocomplete","Orden","Seguimiento");

        $.ajax(
                {
                 type: "POST",
                 async: true,
                 url: urlinit3 ,
                 data: { "idcliente": iddestinatario , "iddestino" : iddestino },
                 success: function (data) {
                    var options = '';
                    for(var i = 0; i < data.length; i++)
                    options += '<option value="'+data[i]+'" />';

                    document.getElementById('languages').innerHTML = options;

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
