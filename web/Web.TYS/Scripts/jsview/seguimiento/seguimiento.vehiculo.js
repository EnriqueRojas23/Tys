var btnNuevo = "#btnNuevo";
var idsOfSelectedRows = [];

$(document).ready(function () {


 $(btnNuevo).click(function (event) { btnNuevo_onclick(this, event); });
 $("#btnBuscar").click(
   function (e) {
     oDocumentosTable.draw();
   });
 CargaGrilla();


 $("#criterio").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });
$("#btnNuevoChofer").click(function(event) {

   var url = UrlHelper.Action("ModalNuevoChofer","Seguimiento","Seguimiento");
   $.get(url, function(data){

       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");

   });


});


});

$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});

function btnNuevo_onclick(obj , event)
{


    var url = UrlHelper.Action("AgregarVehiculoModal", "Seguimiento", "Seguimiento")
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
       // inicializandoEventosModalDocumentos();
        //BeginDropDownlist();
        var config = {
               '.chosen-select': {max_selected_options: 5 ,
                    allow_single_deselect: false ,
                    no_results_text: 'Oops, no se encontró el ubigeo!' }

           }

           for (var selector in config) {
               $(selector).chosen(config[selector]);
           }

    });
}
function editarvehiculo(id)
{



    var url = UrlHelper.Action("EditarVehiculoModal", "Seguimiento", "Seguimiento") + "?id="  + id;
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        var config = {
               '.chosen-select': {max_selected_options: 5 ,
                    allow_single_deselect: false ,
                    no_results_text: 'Oops, no se encontró el ubigeo!' }

           }

           for (var selector in config) {
               $(selector).chosen(config[selector]);
           }

       // inicializandoEventosModalDocumentos();
        //BeginDropDownlist();
    });

}

function CargaGrilla() {

    oDocumentosTable =
       $('.dataTables-tblVehiculo').DataTable({
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
               "url": $('#tblVehiculo').data("url"),
               "data": function (d) {
                   d.placa = $('#placa').val();
                   d.idestado = $('#idestado').val();

               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idvehiculo", "name": "idvehiculo", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   { "title": "Placa", "data": "placa", "name": "placa", "autoWidth": true, "class": "text-center" },
                   { "title": "Proveedor", "data": "razonsocial", "name": "razonsocial", "autoWidth": true, "class": "text-center" },
                   { "title": "Estado", "data": "estado", "name": "estado", "autoWidth": true, "class": "text-center" },
                   { "title": "Chofer", "data": "chofer", "name": "chofer", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "Asignación", "class": "text-center", "data": "idvehiculo", "Width": "15%", "mRender":

                        function (data, type, full) {
                                return     "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='asignarchofer(" + data + ");' href='#' > <i class='fa fa-edit'></i> Asignar Chofer</button></div>" ;


                        }
                   },
                    {
                       "title": "Inspeccionar", "class": "text-center", "data": "idvehiculo", "Width": "15%", "mRender":

                        function (data, type, full) {
                                return     "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-warning btn btn-xs btn-outline' onclick=\"inspeccionarvehiculo(" + data + " , '" + full.estado + "');\" href='#' > <i class='fa fa-edit'></i> Inspeccionar</button></div>" ;


                        }
                   },
                   {
                      "title": "Liberar", "class": "text-center", "data": "idvehiculo", "Width": "15%", "mRender":

                       function (data, type, full) {
                               return     "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-warning btn btn-xs btn-outline' onclick=\"liberar(" + data + " , '" + full.estado + "');\" href='#' > <i class='fa fa-edit'></i> Liberar</button></div>" ;


                       }
                  },

                   {
                       "title": "Acciones", "class": "text-center", "data": "idvehiculo", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarvehiculo(" + data + ");' href='#' > <i class='fa fa-edit'></i></button>"
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminarvehiculo(" + data + ");' href='#' > <i class='fa fa-trash'></i></button></div>";

                        }
                   },

           ],
           buttons: [
               { extend: 'excel', title: 'Listado de Vehículos', exportOptions: { columns: [ 1, 2, 3 ,5 ] } },
               { extend: 'pdf', title: 'Listado de Vehículos', exportOptions: { columns: [ 1, 2, 3 ,5 ] } }

           ]

       });
}

function eliminarvehiculo(item)
{

  var url = UrlHelper.Action("EliminarVehiculo","Seguimiento","Seguimiento") + "?idvehiculo=" + item;

  

  swal({
     title: "Deshabilitar Vehículo",
     text: "¿Esta seguro que desea deshabilitar este vehículo.?",
     type: "warning",
     showCancelButton: true,
     cancelButtonText: "Cancelar",
     confirmButtonColor: '#DD6B55',
     confirmButtonText: 'Deshabilitar',
     closeOnConfirm: true,
     closeOnCancel: true
 },
  function (isConfirm) {
        if (isConfirm) {
            $.ajax({

                url: url,
                type: "post",
                datatype: "json",

                success: function (data) {
                    if (data.res) {
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

function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true)
    {
       swal({
           title: "Registro Exitoso",
           text: "Se registró correctamente el Vehículo.",
            type: "success"
        },
       function ()
       {

            $("#modalcontainer").modal("hide");
             oDocumentosTable.draw();
       });

    }
    else
    {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }

}
function asignarchofer(id)
{

  var url = UrlHelper.Action("VincularChoferModal", "Seguimiento", "Seguimiento") + "?id=" + id;
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        var config = {
               '.chosen-select': {max_selected_options: 5 ,
                    allow_single_deselect: false ,
                    no_results_text: 'Oops, no se encontró el ubigeo!' }

           }

           for (var selector in config) {
               $(selector).chosen(config[selector]);
           }
    });
}

function liberar(id, estado)
{
  //Validar si tiene carga pendiente de salida
  var url = UrlHelper.Action("JsonLiberarVehiculo", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;
  swal({
     title: "Liberar Vehículo",
     text: "¿Esta seguro que desea liberar este vehículo.?",
     type: "warning",
     showCancelButton: true,
     cancelButtonText: "Cancelar",
     confirmButtonColor: '#DD6B55',
     confirmButtonText: 'Liberar',
     closeOnConfirm: true,
     closeOnCancel: true
 },
  function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: url,
                type: "post",
                datatype: "json",
                success: function (data) {
                    if (data.res) {
                          if(data.pendiente)
                          {
                             swal({ title: "No se puede continuar", text: "El vehículo tiene carga pendiente de despacho", type: "warning", confirmButtonText: "Aceptar" });
                          }
                          else {

                            swal({ title: "Se liberó el vehículo", text: "El vehículo se liberó con éxito", type: "success", confirmButtonText: "Aceptar" });
                            oDocumentosTable.draw();
                          }

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
function inspeccionarvehiculo(id, estado)
{

  if(estado != 'Asignado')
  {

    var url = UrlHelper.Action("InspeccionarVehiculoModal", "Seguimiento", "Seguimiento") + "?id=" + id;
      $.get(url, function (data) {
          $("#modalcontent").html(data);
          $("#modalcontainer").modal("show");
         // inicializandoEventosModalDocumentos();
          //BeginDropDownlist();
           idsOfSelectedRows = $('#inspecciones').val().split(',');
           $("#btnguardar").click(function(event) {btnguardar_onclick(this,event);});
          iniciandoGrillaInspeccion(id);
      });

  }
  else
 {
    sweetAlert("El Vehículo se encuentra Asignado", null, "error");
 }





}

function iniciandoGrillaInspeccion(id)
{

    $.jgrid.defaults.height = 420;
    //$.jgrid.defaults.width = 420;
    $.jgrid.defaults.responsive = true;


    var grilla = $("#gridinspeccion");
    var pagergrilla = $("#gridinspeccionpager");

    var vdataurl = $(grilla).data("dataurl")  + "?idvehiculo=" + id ;
    var vdataedit = $(grilla).data("editurl");


    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Concepto','Detalle'],
        colModel:
        [
            { key: true, hidden: true, name: 'idinspeccion', index: 'idinspeccion' ,classes:"grid-col"},
            { key: false, hidden: false, editrules: {required: false} , editable: true ,name: 'concepto', index: 'concepto', width: '100', align: 'center',classes:"grid-col" },
            { key: false, hidden: false, editrules: {required: false} , editable: true ,name: 'detalle', index: 'detalle', width: '350', align: 'center',classes:"grid-col" },
          //  { key: false, hidden: false, editrules: {required: false} , editable: true,  formatter: VerCheckBox ,name: 'checkeado', index: 'checkeado', width: '150', align: 'center',classes:"grid-col" },


        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        editable:true,
      // addedrow: "last",
        addParams: {
            position: "last",
            addRowParams: editOptionsNew
            },
        editParams: editOptionsNew,
        editurl: vdataedit,
        grouping:true,
        multiselect: true,
        groupingView : {

                groupField : ['concepto'],
                groupColumnShow: [false],
                groupCollapse: false,
                //groupText : ['<b>{0} - {1} Elemento(s)</b>'],
                //groupText : ['<b>{0}</b> Costo Total: {costo}', '{0} Costo Total: {costo}'],
                groupSummary : [false],
                showSummaryOnHide: true

         },


         onSelectRow: function (rowid, status) {
             updateIdsOfSelectedRows(rowid, status);
         },
         onSelectAll: function (aRowids, status) {
             var i, count, id;
             for (i = 0, count = aRowids.length; i < count; i++) {
                 id = aRowids[i];
                 updateIdsOfSelectedRows(id, status);
             }
          },
         afterSubmit: function(response,postdata){

               if(response.responseText=="ok")
                    success=true;
                else success = false;

                return [success,response.responseText]
          },
          loadComplete: function (data) {
     //   setStyleCheckBoxGrid(this);
            setStyleCheckBoxGrid(this);
            for (var i = 0; i < idsOfSelectedRows.length; i++){
                  $("#gridinspeccion").setSelection(idsOfSelectedRows[i], true);
            }
            var numerofilas = $(this).getGridParam("records");

            //
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
        afterSubmit: function(response,postdata){

               if(response.responseText=="ok")
                    success=true;
                else success = false;

                return [success,response.responseText]
    },



    });


}
function VerCheckBox(cellvalue, options, rowObject) {
    var control = $("<input type='checkbox' onclick=updateIdsOfSelectedRows('" +  cellvalue + "') value='"+ cellvalue + "'> <br>");
    var htmlcontrol = control[0].outerHTML;
    return htmlcontrol
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
function OnlyNumeric(e) {
            if ((e.which < 48 || e.which > 57)) {
                if (e.which == 8 || e.which == 46 || e.which == 0) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

function displayButtonsDirecciones(cellvalue, options, rowObject)
{



        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId  + "', '' );\"><i class='fa fa-save'></i> </button>";


                                                                                                                //$("#griddirecciones").jqGrid('saveRow',0,  {


        return guardar ;
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

var updateIdsOfSelectedRows = function (id, isSelected) {



 var contains = $.inArray(id, idsOfSelectedRows) == -1 ? false : true; //.contains(id);
    if (!isSelected && contains) {
        for (var i = 0; i < idsOfSelectedRows.length; i++) {
            if (idsOfSelectedRows[i] == id) {
                idsOfSelectedRows.splice(i, 1);
                break;
            }
        }
    }
    else if (!contains) {
        idsOfSelectedRows.push(id);
    }
    var total =  jQuery('#gridinspeccion').jqGrid('getGridParam', 'selarrrow');
    $('#totalinspeecciones').val(total.length -  1);

    $('#inspecciones').val(idsOfSelectedRows.toString());
};


function btnguardar_onclick(obj , event)
{


   var seleccionados  =  $('#totalinspeecciones').val();
   var total = $("#gridinspeccion").getGridParam("reccount")
    var url = UrlHelper.Action("GuardarInspeccionarVehiculo", "Seguimiento", "Seguimiento") + "?inspeccionados=" + $('#inspecciones').val() + "&idvehiculo= " +  $("#idvehiculo").val();

   if(total != seleccionados)
   {
         swal({
            title: "Inspeccionar Vehiculo",
            text: "No ha inspeccionado el total de elementos. ¿Desea continuar?",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Continuar',
            closeOnConfirm: true,
            closeOnCancel: true
        },
         function (isConfirm) {
               if (isConfirm) {
                   $.ajax({

                       url: url,
                       type: "post",
                       datatype: "json",

                       success: function (data) {
                           if (data.res) {
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
   else
   {
         $.ajax({

                       url: url,
                       type: "post",
                       datatype: "json",

                       success: function (data) {
                           if (data.res) {
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








}
