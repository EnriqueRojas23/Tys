
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
    AutoCompleteTextBox();
    AutoCompleteTextBoxDescripcion();



});


function preinicio()
{

  $("#btnagregar").click(function(event) {
   var url = UrlHelper.Action("AgregarClienteModal","Orden","Seguimiento");

        $.get(url, function(data) {
           $("#modalcontent").html(data);
           $("#modalcontainer").modal("show") ;
          configurarChosenSelect();

        });


    /* Act on the event */
  });
  if($('#iddestinatario').val() == $('#idcliente').val())
{
    $("#cbox2").attr("checked" , true);
    $('#iddestinatario').attr("disabled", true);
    $('#iddestinatario').trigger("chosen:updated");

}



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

   JsonConceptoCobro();

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
          $("#iddestinatario").prop( "disabled", true );
          $('#iddestinatario').trigger("chosen:updated");
          $("#iddestinatario_selected").val($('#iddestinatario').val());

      }
      else
      {
           // $('#iddestinatario').attr('disabled', false);
            $("#iddestinatario").prop( "disabled", false );
            $('#iddestinatario').trigger("chosen:updated");
            $("#iddestinatario_selected").val();
            $("#rucdestinatario").val("");
            $("#razonsocialdestinatario").val("");

      }


    });
}

function BeginDropDownlistRemitente()
{
   $("#idremitente").change(function ()
    {

        var idcliente = $("#idremitente").val();
        $('#idremitente_selected').val(idcliente);


        var urlinit = UrlHelper.Action("ListarDirecciones", "Orden", "Seguimiento");
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

          $("#idremitente").prop( "disabled", true );
          $("#idremitente_selected").val($('#idremitente').val());




      }
      else
      {
          var urlinit = UrlHelper.Action("JsonListarClientesProveedor", "Orden", "Seguimiento");
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

                       var $select = $('#idremitentedireccion');
                       $select.empty();




                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });

             $('#idremitente').trigger("chosen:updated");
             $("#idremitente").prop( "disabled", false );
             $("#idremitente_selected").val();
      }

    });
}
function BeginDropDownlistCliente()
{
   $("#idcliente").change(function ()
    {

      if($('#cbox2').is(':checked')==true)
      {
             $('#iddestinatario').val($("#idcliente").val());
             $('#iddestinatario').change();
             $("#iddestinatario").prop( "disabled", true );
             $('#iddestinatario').trigger("chosen:updated");
      }

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

            $("#idremitente").prop( "disabled", true );
            $("#idremitente_selected").val($('#idremitente').val());




        }
        else
        {
            var urlinit = UrlHelper.Action("JsonListarClientesProveedor", "Orden", "Seguimiento");
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

                         var $select = $('#idremitentedireccion');
                         $select.empty();




                     },
                     error: function (request, status, error)
                     {
                         swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                     }

                  });

               $('#idremitente').trigger("chosen:updated");
               $("#idremitente").prop( "disabled", false );
               $("#idremitente_selected").val();
        }
             if ( $('#idorigen').val()=="") return;
             JsonListarFormulayTipoTransporte();
             // ///////CARGAR FORNULAS Y MODO TRANSPORTE

             if($('#idformula').val() == '') return;
             if($('#idtipotransporte').val() == '') return;

                JsonConceptoCobro();
                AutoCompleteTextBox();



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
         $('#chofer').val('');
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
                           $("#chofer").val(data.chofer);
                           $("#dni").val(data.dni);

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });
      });


}

function OnCompleteTransaction_NuevoCliente(xhr, status)
{
       var jsonres = xhr.responseJSON;
       if(jsonres.res==true)
       {
           swal("Registro exitoso.", "Se ha registrado el nuevo cliente. ", "success");
           $("#modalcontainer").modal("hide");

            var urlinit = UrlHelper.Action("JsonListarClientes", "Orden", "Seguimiento");
            $.ajax(
                 {
                  type: "POST",
                  async: true,
                  url: urlinit ,
                  data: { },
                  success: function (data) {
                        var $select = $('#idcliente');
                        $select.empty();
                        $("#idcliente").append('<option value="">[Cliente]</option>');
                        $.each(data.listaclientes, function (i, state) {
                            $('<option>', {
                                value: state.Value
                            }).html(state.Text).appendTo($select);
                        });
                         $('#idcliente').trigger("chosen:updated");

                    },
                    error: function (request, status, error)
                    {
                        swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                    }

                 });




       }

}


function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    //CleanValidationError();

    if (jsonres.res == true)
    {

        swal("Se registró con éxito", "OT: " +  jsonres.ot + " ", "success");
        var url = UrlHelper.Action("OrdenesRapidas", "Orden", "Seguimiento");
        window.location.href = url;
        sweetAlert(jsonres.mensaje, null, "error");

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
        rowNum: 20,
        rowList: [20, 40, 60, 80],
        emptyrecords: 'No se encontraron registros',
        autoheight: true,
        responsive : true,
        autowidth: true,
        shrinkToFit: true,
        editable:true,
        footerrow : true,
        addParams: {
              position: "last",
              addRowParams: editOptionsNew
              },
        editParams: editOptionsNew,
        editurl: vdataedit,
        gridComplete : function (){
          var grid = $(gridlistaots);
          var sum = grid.jqGrid('getCol', 'bulto', false, 'sum');
          var sum1 = grid.jqGrid('getCol', 'peso', false, 'sum');
          var cantidad  =   jQuery("#gridguias").jqGrid('getGridParam', 'records')
          grid.jqGrid('footerData', 'set', { 'nroguia': 'TOTAL:', 'bulto': sum.toFixed(2), 'peso': sum1.toFixed(2), 'volumen': 'CANTIDAD:', 'pesovol': cantidad });
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
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId  + "', '' );  \"><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridguias').restoreRow('" + options.rowId + "'); successfunc(); \"><i class='fa fa-times-circle'></i> </button>";


        return editar + guardar + control + restore;
}
function successfunc ()
{
      var grilla = $("#gridguias");
      var vdataurl = UrlHelper.Action("JsonGetListarGuias", "Orden", "Seguimiento");
      $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

    //  SumatoriaGrilla();

}
function SumatoriaGrilla()
{

     var grid = $('#gridguias');
     var rows = grid.jqGrid('getDataIDs');
     var bulto = 0;

      for (i = 0; i < rows.length; i++)
      {

          var rowData = grid.jqGrid('getRowData', rows[i]);

          if(rowData['bulto']!="")
          bulto  = bulto +  parseFloat(rowData['bulto']);
          if(rowData['peso']!="")
          pesogeneral  = pesogeneral +  parseFloat(rowData['peso']);
          if(rowData['volumen']!="")
          volgeneral  = volgeneral + parseFloat(rowData['volumen']);
          if(rowData['bulto']!="")
          bulto = bulto + parseFloat(rowData['bulto']);


      }

              $('#txtpeso').val(pesogeneral);
              $('#txtbultos').val(bulto);
              $('#txtcantidad').val( rows.length);
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




              var url = UrlHelper.Action("AgregarGuiasNumeroInicial", "Orden", "Seguimiento");


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

var bultogeneral = $('#bultogeneral').val();

  var pesovol = $('#pesovol').val();


 if(pesogeneral == ""  && volgeneral == "" )
   {
   pesogeneral = 0;
   volgeneral = 0;
   bultogeneral = 0;
   var grid = $('#gridguias');
   var rows = grid.jqGrid('getDataIDs');

    for (i = 0; i < rows.length; i++)
    {


        var rowData = grid.jqGrid('getRowData', rows[i]);

        if(rowData['peso']!="")
        pesogeneral  = pesogeneral +  parseFloat(rowData['peso']);
        if(rowData['volumen']!="")
        volgeneral  = volgeneral + parseFloat(rowData['volumen']);
        if(rowData['pesovol']!="")
        pesovol = pesovol + parseFloat(rowData['pesovol']);
        if(rowData['bulto']!="")
        bultogeneral = bultogeneral + parseFloat(rowData['bulto']);



        if(volgeneral == "")
          volgeneral = 0;



    }

  $('#txtcantidad').val(rows.length);

   }

  $('#txtpeso').val(pesogeneral);
  $('#txtbultos').val(bultogeneral);

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




  var url = UrlHelper.Action("CalcularPago", "Orden", "Seguimiento");
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
                ,"pesovol" : pesovol
                ,"bulto" : bultogeneral
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
                      $("#txtpeso").val(data.jsondata.peso);
                      $("#txtbultos").val(data.jsondata.bultos);
                      $("#txtcantidad").val(data.jsondata.cantidad);


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
       setcolor ($("#chofer"));
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
     var data =  JSON.parse(resonsse.responseText);
     if(data.res == false)
     {
       swal({ title: "Error", text:  data.msj , type: "error", confirmButtonText: "Aceptar" });
       successfunc();
       return false;
    }
    else {
      $("#txtpeso").val(data.jsondata.peso);
      $("#txtbultos").val(data.jsondata.bultos);
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




       var urlinit = UrlHelper.Action("JsonListarFormulayTipoTransporteOT", "Orden", "Seguimiento");
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


        var urlinit3 = UrlHelper.Action("ListarDireccionesAutocomplete", "Orden", "Seguimiento");

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



    var urlinit4 = UrlHelper.Action("JsonListarConceptoCobroT", "Orden", "Seguimiento");


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
                               $("#idconceptocobro").append('<option value=""></option>');
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
                          if(length == 1)
                          {
                             $("#idconceptocobro").attr('disabled', 'disabled');
                          }
                          else {
                             $("#idconceptocobro").removeAttr('disabled');
                          }
                          $('#idconceptocobro').trigger("chosen:updated");


                           },
                           error: function (request, status, error)
                           {
                               swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                           }

                        });


}
function RegistrarContinuar()
{
    var url = UrlHelper.Action("OrdenesRapidas", "Orden", "Seguimiento");
         window.location.href = url;
}
function AutoCompleteTextBoxDescripcion()
{
     var url = UrlHelper.Action("JsonAutocomplete_Descripcion", "Orden", "Seguimiento");
      $.ajax({
                 type: "POST",
                 async: true,
                 url: url ,
                 datatype : 'json',
                 data: { "idcampo": "3"  },
                 success: function (data) {
                           var options = '';
                           for(var i = 0; i < data.length; i++)
                           {
                             options += '<option value="'+data[i]+'" />';
                           }
                           $("#descripciones").html(options);
                 },
                 error: function (request, status, error)
                 {
                     swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                 }
             });
}

function AutoCompleteTextBox()
{
  var idcliente =  $("#idcliente").val()

  $("#puntopartida").val("");

  if(idcliente == "")
     return ;

    var url = UrlHelper.Action("JsonAutocomplete", "Orden", "Seguimiento");
        $.ajax({
                   type: "POST",
                   async: true,
                   url: url ,
                   datatype : 'json',
                   data: { "idcampo": "2" , "idcliente" : idcliente },
                   success: function (data) {
                             var options = '';
                             for(var i = 0; i < data.length; i++)
                             {
                               options += '<option value="'+data[i]+'" />';
                             }
                             $("#helps").html(options);
                             $("#puntopartida").val(data[0]);


                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
}
