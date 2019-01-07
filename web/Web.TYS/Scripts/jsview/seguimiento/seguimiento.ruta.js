var btnNuevo = "#btnNuevo";
var DatosComboDepartamento;
var DatosComboTipoTransporte;
var DatosComboEtapas;
var aux_iddepartamento = "";
$(document).ready(function () {
 $(btnNuevo).click(function (event) { btnAgregarRuta_onclick(this, event); });




 $("#btnBuscar").button()
                   .click(function (e) {
                       oClientesTable.draw();
                   });



CargaListaRuta();
configurarGrilla(0);
cargarCombos();

$("#addrow").click( function() {

         var id =  $('#idruta').val()
           if(id=='')
             {
                  sweetAlert("Debe seleccionar un cliente", null, "error");
                return;
              }
             else
               {
                $("#griddetalleruta").jqGrid('addRowData',0,1,"first");
                 $("#griddetalleruta").editRow(0,true);
              }

  });

});

function cargarCombos()
{

        var url = UrlHelper.Action("JsonCargarCombosGrillas", "Seguimiento", "Seguimiento");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   //data: {  },
                   success: function (data) {
                     DatosComboDepartamento =  data.resA;
                     DatosComboTipoTransporte = data.resB;
                     DatosComboEtapas = data.resC;

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
}





$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});

function CargaListaRuta() {


    oClientesTable =
       $('.dataTables-tblRuta').DataTable({
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
                ,"sLengthMenu":  ""  }
                ,


           "ajax": {
               "url": $('#tblRuta').data("url"),
               "data": function (d) {
                   d.criterio = $('#nombre').val();
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idruta", "name": "idruta", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   { "title": "Nombre", "data": "nombre", "name": "nombre", "autoWidth": true, "class": "text-center" },
                   { "title": "Origen", "data": "origen", "name": "origen", "autoWidth": true, "class": "text-center" },
                   { "title": "Destino", "data": "destino", "name": "destino", "autoWidth": true, "class": "text-center" },
                   { "title": "Ruta", "data": "ruta", "name": "ruta", "autoWidth": true, "class": "text-center" },
                   { "title": "KM", "data": "km", "name": "km", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idruta", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarruta(" + data + ");' href='#' > <i class='fa fa-edit'></i> </button>"
                               + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick=\"mostrarrutas(" + data + ",'" +  full.nombre + "')\"; href='#' > <i class='fa fa-search'></i> </button>"
                                   +   "<button type='button' class='btn btn-primary btn-xs btn-outline' onclick='irEliminar(" + data + ")'><i class='fa fa-trash'></i></button></div>";

                        }                   },

           ],
           buttons: [
               { extend: 'excel', title: 'Listado de Clientes', exportOptions: { columns: [ 1, 2, 3 ,4,5,6,7,9 ] } },
               { extend: 'pdf', title: 'Listado de Clientes', exportOptions: { columns: [ 1, 2, 3 ,4,5,6,7,9] } }

           ]

       });
}

function btnAgregarRuta_onclick(obj, event) {

    var url = UrlHelper.Action("AgregarRutaModal", "Seguimiento", "Seguimiento")
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
    });
}

function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    CleanValidationError();

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
           oClientesTable.draw();
       });

    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}



function configurarGrilla(id) {


    $.jgrid.defaults.height = 320;
    $.jgrid.defaults.width = 1220;
    $.jgrid.defaults.responsive = true;

    var grilla = $("#griddetalleruta");
    var pagergrilla = $("#griddetallerutapager");

    var vdataurl = $(grilla).data("dataurl")  + "?idruta=" + id ;
    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Post',
        colNames: ['', 'Orden','Origen','Departamento', 'Provincias','Distrito','km','ModTransp.','Lead Id','Lead Retorno','Lead Doc.','Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'iddetalleruta', index: 'iddetalleruta' ,classes:"grid-col"},
            { key: false, hidden: false, editrules: {integer: true ,required: true}, editable: true, name: 'idorden', index: 'idorden' , width: '40',classes:"grid-col"},
            { key: false, hidden: false, editrules: {required: true} ,editable: true ,name: 'origen'
                              , index: 'origen', width: '160', align: 'center'
                              , classes:"grid-col",formatter: formatedit
                              , edittype: "select"
                              , editoptions: { dataUrl: fcnUrlControlGrid('departamento')}
                      },


            { key: false, hidden: false, editrules: { required: true} , editable: true ,name: 'departamento'
                    , index: 'departamento', width: '160', align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: {
                                  dataUrl: fcnUrlControlGrid('departamento') ,
                                  // buildSelect: function (data) {
                                  //     return data;
                                  // },

                                  dataInit: function (elem,i) {
                                          var v = $(elem).val();
                                          // $(elem).trigger('change');
                                          $(grilla).setColProp('provincia', {
                                             editoptions:
                                             {

                                                 dataUrl: fcnUrlControlGrid('provincia' , i.rowId)
                                             }
                                           });

                                        },
                                         dataEvents: [
                                         {
                                              type: 'change',
                                              fn: function(e) {
                                               var v = parseInt($(e.target).val(), 10);

                                               var iddepartamento = v;
                                                var url = UrlHelper.Action("ListarProvincias", "Seguimiento", "Seguimiento");
                                                $.ajax(
                                                   {
                                                     type: "POST",
                                                     async: true,
                                                     url: url ,
                                                     data: { "iddepartamento": iddepartamento },
                                                     success: function (data) {

                                                      var row = $(e.target).closest('tr.jqgrow');
                                                      var rowId = row.attr('id');

                                                      var $select = $("select#" + rowId + "_provincia", row[0]);
                                                      $select.empty();
                                                      $select.append('<option value="">[Provincia]</option>');
                                                      $.each(data, function (i, state) {
                                                         $('<option>', {
                                                             value: state.Value

                                                         }).html(state.Text).appendTo($select);

                                                      });
                                                     },
                                                     error: function (request, status, error)
                                                     {
                                                         swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                                                     }
                                                 });



                                              }
                                          },

                ]
              }
            },
            { key: false, hidden: false, editable: true ,name: 'provincia'
                    , index: 'provincia', width: '120', align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: {
                                  dataUrl: fcnUrlControlGrid('provincia'),
                                  dataInit: function (elem,i) {
                                          var v = $(elem).val();
                                          $(grilla).setColProp('distrito', {
                                             editoptions:
                                             {

                                                 dataUrl: fcnUrlControlGrid('distrito' , i.rowId)
                                             }
                                           });
                                          //$(grilla).setColProp('direccion', { editoptions: { dataUrl: fcnUrlControlGrid('s') }});
                                        },
                                         dataEvents: [
                                         {
                                              type: 'change',
                                              fn: function(e) {
                                               var v = parseInt($(e.target).val(), 10);

                                               var idprovincia = v;
                                                var url = UrlHelper.Action("ListarDistritos", "Seguimiento", "Seguimiento");
                                                $.ajax(
                                                   {
                                                     type: "POST",
                                                     async: true,
                                                     url: url ,
                                                     data: { "idprovincia": idprovincia },
                                                     success: function (data) {

                                                      var row = $(e.target).closest('tr.jqgrow');
                                                      var rowId = row.attr('id');

                                                      var $select = $("select#" + rowId + "_distrito", row[0]);
                                                      $select.empty();
                                                      $select.append('<option value="">[Distrito]</option>');
                                                      $.each(data, function (i, state) {
                                                         $('<option>', {
                                                             value: state.Value

                                                         }).html(state.Text).appendTo($select);

                                                      });




                                                     },
                                                     error: function (request, status, error)
                                                     {
                                                         swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                                                     }
                                                 });



                                              }
                                          }
                ]
              }
            },
            { key: false, hidden: false, editable: true ,name: 'distrito'
                    , index: 'distrito', width: '120', align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('distrito')}
            },
              { key: false, hidden: false, editrules: {required: true} ,  editable: true, name: 'km', index: 'km' , width: '40',classes:"grid-col"},
              { key: false, hidden: false, editrules: {required: true} ,  editable: true ,name: 'tipotransporte'
                              , index: 'tipotransporte', width: '100', align: 'center'
                              , classes:"grid-col",formatter: formatedit
                              , edittype: "select"
                              , editoptions: { dataUrl: fcnUrlControlGrid('tipotransporte')}
                      },
                      { key: false, hidden: false , editrules: { required: true},  editable: true, name: 'leadida', index: 'leadida' , width: '40',classes:"grid-col"},
                      { key: false, hidden: false, editrules: {required: true},  editable: true, name: 'leadretorno', index: 'leadretorno' , width: '40',classes:"grid-col"},
                      { key: false, hidden: false, editrules: {required: true},  editable: true, name: 'leaddocumentario', index: 'leaddocumentario' , width: '40',classes:"grid-col"},








        { key: false, hidden: false, editable: false ,name: 'iddetalleruta', width:'120' , index: 'iddetalleruta' ,  formatter:  displayButtonsDetalleRuta,classes:"grid-col"}
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        editable:true,
        addedrow: "last",
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
function displayButtons(cellvalue, options, rowObject)
    {

        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridproveedores').saveRow('" + options.rowId + "', successfunc)\";><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';

        return guardar + control;
    }
function formatedit (cellvalue, options, rowObject)
{

    if(cellvalue == null)
    return "";
  else
    return " "  + cellvalue ;
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
function successfunc ()
{
    var id = $("#idruta").val();

   oClientesTable.draw();

    var grilla = $('#griddetalleruta');
    var vdataurl =  UrlHelper.Action("JsonGetListarDetalleRuta", "Seguimiento", "Seguimiento") + "?idruta=" + id;
     $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

}
function mostrarrutas(id,nombre)
{
    $('#lblRuta').text('Ruta seleccionada :  ' +  nombre);
    $("#idruta").val(id);
    var grilla = $('#griddetalleruta');
    var vdataurl =  UrlHelper.Action("JsonGetListarDetalleRuta", "Seguimiento", "Seguimiento") + "?idruta=" + id;
     $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

    //configurarGrilla(id);
}

var lastSelection;
function editRow(id) {


    var grilla = $("#gridasignacion");
    $.each($.find("select[name='idasignacion']"), function(){

        $(this).on( "keydown", function(event) {
          if(event.which == 13)
            $(grilla).saveRow("rowid", false);
        });

    })

    if (id && id !== lastSelection) {
        $("#gridasignacion").jqGrid('restoreRow', lastSelection);
        $("#gridasignacion").jqGrid('editRow', id, true);
        lastSelection = id;
    }
}
function eliminardetalle(id)
{
  var url = UrlHelper.Action("EliminarDetalleRuta", "Seguimiento", "Seguimiento");
    swal({
        title: "Eliminar Detalle de ruta",
        text: "¿Está seguro que desea eliminar este detalle de ruta?",
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
   $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "iddetalleruta": id },
                   success: function (data) {
                       swal("¡Se ha eliminado correctamente!", data.msj, "success");
                           var id =  $("#txtidcliente").val();
                          var grilla = $("#gridproveedores");
                          var vdataurl =  UrlHelper.Action("JsonGetListarProveedorxCliente", "Seguimiento", "Seguimiento") + "?idcliente=" + id;
                          $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                           oClientesTable.draw();
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
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


function cargarProvincias(id)
{
   var iddepartamento =  $("#ddlDepartamento_" + id).val();
        var url = UrlHelper.Action("ListarProvincias", "Seguimiento", "Seguimiento");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "iddepartamento": iddepartamento },
                   success: function (data) {
                       var $select =  $("#ddlProvincia_" + id);
                       $select.empty();
                       $("#ddlProvincia_" + id).append('<option value="">[Provincia]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });


}
function cargarProvinciasByResult(id, position, val)
{
   var iddepartamento =  id;
        var url = UrlHelper.Action("ListarProvincias", "Seguimiento", "Seguimiento");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "iddepartamento": iddepartamento },
                   success: function (data) {
                       var $select =  $("#ddlProvincia_" + position);
                       $select.empty();
                       $("#ddlProvincia_" + position).append('<option value="">[Provincia]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                       //$select[0].selectedIndex = 1;
                       $("#ddlProvincia_" + position + " option[value='" + val + "']").attr("selected","selected");
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });


}
function cargarDistritos(id)
{
   var idprovincia =  $("#ddlProvincia_" + id).val();
        var url = UrlHelper.Action("ListarDistritos", "Seguimiento", "Seguimiento");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idprovincia": idprovincia },
                   success: function (data) {
                       var $select =  $("#ddlDistrito_" + id);
                       $select.empty();
                       $("#ddlDistrito_" + id).append('<option value="">[Distrito]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });


}
function cargarDistritosByResult(id, position, val)
{
   var idprovincia =  id;
        var url = UrlHelper.Action("ListarDistritos", "Seguimiento", "Seguimiento");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idprovincia": idprovincia },
                   success: function (data) {
                       var $select =  $("#ddlDistrito_" + position);
                       $select.empty();
                       $("#ddlDistrito_" + position).append('<option value="">[Distrito]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                       $("#ddlDistrito_" + position + " option[value='" + val + "']").attr("selected","selected");
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });


}
function guardardetalle(id)
{
   var iddistrito = $("#ddlDistrito_" + id).val();
   var idorigen = $("#ddlEvaluacion_" + id).val();
   var km = $("#txtKm_" + id).val();
   var lida = $("#txtlida_" + id).val();
   var lvuelta = $("#txtlvuelta_" + id).val();
   var ldocu = $("#txtldocu_" + id).val();
    var idruta =  $("#idruta").val();
   var idtipotransporte = $("#ddlTipoTransporte_" + id).val();
   var etapas = $('#etapas').val().toString();





    var url = UrlHelper.Action("SaveDetalles", "Seguimiento", "Seguimiento");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: {

                       "iddistrito": iddistrito ,
                       "iddetalleruta": id,
                       "idorigen" : idorigen,
                       "km" : km,
                       "idtipotransporte" : idtipotransporte,
                       "lida" : lida,
                       "lvuelta" :lvuelta,
                       "ldocu" :ldocu,
                       "idruta" : idruta







                    },
                   success: function (data) {

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });


}

function irEliminar(id)
{
   var url = UrlHelper.Action("EliminarRuta", "Seguimiento", "Seguimiento");
    swal({
        title: "Eliminar Ruta",
        text: "¿Está seguro que desea eliminar esta ruta?",
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
   $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "id": id },
                   success: function (data) {
                       swal("¡Se ha eliminado correctamente!", data.msj, "success");
                       $("#btnBuscar").click();
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
          }
     });

}
function displayButtonsDetalleRuta(cellvalue, options, rowObject)
{

        var editar = "<div class='btn-group'><button type='button' title='Editar' class='btn btn-primary btn-xs btn-outline' onclick=\"$('#griddetalleruta').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-primary btn-xs btn-outline' onclick=\"$('#griddetalleruta').saveRow('" + options.rowId + "', successfunc)\")\";><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn  btn-xs btn-primary btn-outline" onclick="irEliminarDetalle(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-primary btn-xs btn-outline' onclick=\"$('#griddetalleruta').restoreRow('" + options.rowId + "'); successfunc(); \"><i class='fa fa-times-circle'></i> </button></div>";


        return editar + guardar + control + restore;
}


function irEliminarDetalle(id)
{

   var url = UrlHelper.Action("EliminarDetalleRuta", "Seguimiento", "Seguimiento");
    swal({
        title: "Eliminar Detalle Ruta",
        text: "¿Está seguro que desea eliminar este detalle de ruta?",
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
   $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "id": id },
                   success: function (data) {
                       swal("¡Se ha eliminado correctamente!", data.msj, "success");
                       var grilla = $("#griddetalleruta");
                       var idruta =  $("#idruta").val();
                      var vdataurl =  UrlHelper.Action("JsonGetListarDetalleRuta", "Seguimiento", "Seguimiento") + "?idruta=" + idruta;
                      $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                   oClientesTable.draw();
                    },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
          }
     });



}

   function editarruta (id)
    {
           var url = UrlHelper.Action("EditarRutaModal", "Seguimiento", "Seguimiento") + "?idruta=" + id;
           $.get(url, function (data) {
           $("#modalcontent").html(data);
           $("#modalcontainer").modal("show");
    });

    }
