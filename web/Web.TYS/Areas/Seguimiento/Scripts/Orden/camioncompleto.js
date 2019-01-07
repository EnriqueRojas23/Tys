var btnNuevo = "#btnNuevo";

$(document).ready(function () {

 $("#btnBuscar").button()
                   .click(function (e) {
                       oClientesTable.draw();
                   
                   });

      $("#criterio").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });

    inicializandoCombos();
    CargaCambioCompleto();


});
function inicializandoCombos()
{
    
     var config = {
            '.chosen-select': {
                  max_selected_options: 5,
                 disable_search_threshold: 10 ,
                 placeholder_text_multiple: "Seleccione Conceptos",
                 no_results_text: 'Oops, no se encontró el Concepto!' }
        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

}

function CargaCambioCompleto() {

    
    oClientesTable =
       $('.dataTables-tblCamionCompleto').DataTable({
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
               "url": $('#tblCamionCompleto').data("url"),
               "data": function (d) {
                   d.codigo = $('#codigocamioncompleto').val();
                   d.iddestino  = $("#iddestino").val();
                   d.idestacion  = $("#idestacion").val();

               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idcamioncompleto", "name": "idcamioncompleto", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   { "title": "Código", "data": "codigocamioncompleto", "name": "codigocamioncompleto", "autoWidth": true, "class": "text-center" },
                   { "title": "Fecha", "data": "fecharegistro", "name": "fecharegistro", "autoWidth": true, "class": "text-center" },
                   { "title": "Medio Transporte", "data": "tipotransporte", "name": "tipotransporte", "autoWidth": true, "class": "text-center" },
                   { "title": "Origen", visible: true, "data": "Origen", "name": "razonsocial", "Origen": true, "class": "text-center" },
                   { "title": "Ruta", "data": "ruta", "name": "ruta", "autoWidth": true, "class": "text-center" },
                   { "title": "Placa", "data": "placa", "name": "placa", "autoWidth": true, "class": "text-center" },
                   { "title": "Cliente", visible: false ,"data": "razonsocial", "name": "razonsocial", "autoWidth": true, "class": "text-center" },
                   { "title": "Formula", visible: false ,"data": "formula", "name": "formula", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idcamioncompleto", "Width": "15%", "mRender":
                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top' title='Editar'  class='btn-primary btn btn-xs btn-outline' onclick='editarcamion(" + data + ");' href='#' > <i class='fa fa-edit'></i> </button>" 
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' title='Eliminar' onclick='eliminarcliente(" + data + ");' href='#' > <i class='fa fa-trash'></i> </button></div>" 
                           + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' title='Agregar OT' onclick=agregarot(" + data + "); href='#' > <i class='fa fa-plus  '></i> </button></div>";
                         
                        }
                   },

           ],
           buttons: [
               { extend: 'excel', title: 'Listado de Clientes', exportOptions: { columns: [ 1, 2, 3 ,4,5,6,7,9 ] } },
               { extend: 'pdf', title: 'Listado de Clientes', exportOptions: { columns: [ 1, 2, 3 ,4,5,6,7,9] } }
               
           ]

       });
}








$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});
$(function() {
    $('.focus :input').focus();
});


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
                 var url =  UrlHelper.Action ("CamionCompleto","Seguimiento","Seguimiento");
                 window.location.href = url;
       });
     
    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}


function formatedit (cellvalue, options, rowObject)
{
  if(cellvalue == null)
    return "";
  else
    return " "  + cellvalue ;
  
}

function displayButtonsDirecciones(cellvalue, options, rowObject)
{
        

        
        var editar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddirecciones').editRow('" + options.rowId  + "', successfuncdir)\";><i class='fa fa-edit'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId  + "', '' );\"><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminarDireccion(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddirecciones').restoreRow('" + options.rowId + "'); successfuncdir();\"><i class='fa fa-times-circle'></i> </button>"; 
                                                                                                                //$("#griddirecciones").jqGrid('saveRow',0,  { 
                                                                                                                         

        return editar + guardar + control + restore;
}





function configurarGrilla(id) {

    
    
    $.jgrid.defaults.height = 60;
    $.jgrid.defaults.responsive = true;

    var grilla = $("#gridproveedores");
    var pagergrilla = $("#gridproveedorespager");



    var vdataurl = $(grilla).data("dataurl")  ;
    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', '','Proveedor','Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idproveedorcliente', index: 'idproveedorcliente' ,classes:"grid-col"},
            { key: false, hidden: true,  editable: true, name: 'idcliente', index: 'idcliente' ,classes:"grid-col"},
            { key: false, hidden: false, editable: true ,name: 'razonsocial', index: 'razonsocial', width: '100', align: 'center',classes:"grid-col",formatter: formatedit, edittype: "select", classes: "grid-col" },
            { key: false, hidden: false, editable: false ,name: 'idproveedorcliente', width:'20' , index: 'idproveedorcliente' ,  formatter:  displayButtons,classes:"grid-col"}   
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        editable:true,
         onSelectRow: function (rowid, status) {

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
     $("#grid").jqGrid('bindKeys', {
                onEnter: function(rowid) {
                    doeditRow(rowid);
                }
            }  );

    
}

function displayButtons(cellvalue, options, rowObject)
{
        
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridproveedores').saveRow('" + options.rowId + "', successfunc)\";><i class='fa fa-save'></i> </button>";
        var control = "<button type='button' title='Eliminar' class='btn btn-warning btn-xs btn-outline' onclick='irEliminar(" + cellvalue + ")''><i class='fa fa-trash'></i></button>";
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridproveedores').restoreRow('" + options.rowId + "'); mostrarproveedores('" + $("#txtidcliente").val() + "'); \"><i class='fa fa-times-circle'></i> </button>"; 

        return guardar + control + restore;
}
function formatedit (cellvalue, options, rowObject)
{

    return " "  + cellvalue ;
  
}


function irEliminar(id)
{
   var url = UrlHelper.Action("EliminarProveedor", "Seguimiento", "Seguimiento");
    swal({
        title: "Eliminar Proveedor Autorizado",
        text: "¿Está seguro que desea eliminar esta Proveedor Autorizado?",
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
                   data: { "idproveedor": id },
                   success: function (data) {
                       swal("¡Se ha eliminado correctamente!", data.msj, "success");
                           var id =  $("#txtidcliente").val();
                          var grilla = $("#gridproveedores");
                          var vdataurl =  UrlHelper.Action("JsonGetListarProveedorxCliente", "Seguimiento", "Seguimiento") + "?idcliente=" + id;
                          $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
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

    if (charCode !== 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode === 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1)
    {
        var range = document.selection.createRange();

        if (range.text !== "") {

        }
        else
        {
            var number = el.value.split('.');
            if (number.length === 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
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
  
   var url = UrlHelper.Action("CalcularPago", "Seguimiento", "Seguimiento") ;
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
                , "peso" : pesogeneral
                ,"volumen" : volgeneral 
               },
                success: function (data) {
                      
                      if(data.res == false)
                      {
                         swal({ title: "Error!", text: data.mensaje , type: "error", confirmButtonText: "Aceptar" });    
                       $('#subtotal').val('');
                       $('#impuesto').val('');
                       $('#total').val('');
                      }
                      else{

                       $('#subtotal').val(data.subtotal);
                       $('#impuesto').val(data.igv);
                       $('#total').val(data.total);
                     }

                },
                error: function (request, status, error) {
                    swal({ title: "Error!", text: "Ocurrio un error." , type: "error", confirmButtonText: "Aceptar" });
                }
            });
  
}

function agregarot (id)
{
                var url =  UrlHelper.Action ("NuevaOrdenCamionTrabajo","Orden","Seguimiento") + "?id="  +  id;
                 window.location.href = url;
}
function editarcamion(id)
{
                 var url =  UrlHelper.Action ("EditarCamionCompleto","Orden","Seguimiento") + "?id="  +  id;
                 window.location.href = url;
}

function eliminarcliente(id)
{
  var url = UrlHelper.Action("EliminarProveedor", "Seguimiento", "Seguimiento");
    swal({
        title: "Eliminar Camión Completo",
        text: "¿Está seguro que desea eliminar el camión completo?",
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
               swal({ title: "¡Error!", text: "¡No puede eliminar mientras existan ordenes asociadas!", type: "error", confirmButtonText: "Aceptar" });
                  
          }
     });
}