 btnNuevo = "#btnNuevo";
var gridlistaots = "#gridlistaots";
var gridlistaotspager = "#gridlistaotspager";

$(document).ready(function () {

 $("#btnBuscar").button()
                   .click(function (e) {
                  var grilla = $("#gridasignacion");
                  var criterio = $('#razonsocial').val();
                  var vdataurl =  UrlHelper.Action("JsonGetListarAsignaciones", "Pago", "Pago") + "?criterio=" + criterio;
                  //var vdataurl = $(grilla).data("dataurl") + "?idestado=" + idEstado + "&ini=" + fechaInicial + "&fin=" + fechaFinal;
                  $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
                   });

configurarGrilla();
$("#addrow").click( function() {
    $("#gridasignacion").jqGrid('addRowData',0,1,"first");
    $("#gridasignacion").editRow(0,true);
  });


    $("#razonsocial").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });

});
$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#addrow").click();

});
function reload()
{
    var grilla = $("#gridasignacion");
    var criterio = $('#razonsocial').val();
    var vdataurl =  UrlHelper.Action("JsonGetListarAsignaciones", "Pago", "Pago") + "?criterio=" + criterio;
    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}


function configurarGrilla() {


    $.jgrid.defaults.height = 320;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#gridasignacion");
    var pagergrilla = $("#gridasignacionpager");



    var vdataurl = $(grilla).data("dataurl");
    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'N°','Proveedor', 'Etapa', 'Modo de Transporte', 'Tipo Comprobante', 'Moneda' , 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idasignacion', index: 'idasignacion' ,classes:"grid-col"},
            { key: false, hidden: false, name: 'idasignacion', index: 'idasignacion',align: 'center' ,classes:"grid-col",width: '15'},
            { key: false, hidden: false, editable: true ,name: 'razonsocial', index: 'razonsocial', width: '60', align: 'center',classes:"grid-col" ,formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idproveedor') }, classes: "grid-col" },
            { key: false, hidden: false, editable: true ,name: 'etapa', index: 'etapa', width: '30', align: 'center',classes:"grid-col",formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idetapa') }, classes: "grid-col" },
            { key: false, hidden: false, editable: true, name: 'tipotransporte', index: 'tipotransporte', width: '30', align: 'center', formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idtipotransporte') }, classes: "grid-col" },
            { key: false, hidden: false, editable: true ,name: 'tipocomprobante', index: 'tipocomprobante', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idtipocomprobante') }, classes: "grid-col" },
            { key: false, hidden: false, editable: true ,name: 'moneda', index: 'moneda', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idmoneda') }, classes: "grid-col" },
            { key: false, hidden: false, editable: false ,name: 'idasignacion', width:'30' , index: 'idasignacion' ,  formatter:  displayButtons,classes:"grid-col"}
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        editable:true,
//        addedrow: "last",
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



function formatedit (cellvalue, options, rowObject)
{

    return " "  + cellvalue ;

}
function displayButtons(cellvalue, options, rowObject)
    {
        var editar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridasignacion').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
        //var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridasignacion').saveRow('" + options.rowId + "', successfunc)\";><i class='fa fa-save'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId  + "', '' );\"><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridasignacion').restoreRow('" + options.rowId + "'); reload();\"><i class='fa fa-times-circle'></i> </button>";

        return editar + guardar + control + restore;
    }

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
};
function successfunc ()
{

   $("#btnBuscar").click();

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
function irEliminar(id)
{
   var url = UrlHelper.Action("EliminarAsignacion", "Pago", "Pago");
    swal({
        title: "Eliminar Asignación",
        text: "¿Está seguro que desea eliminar esta asignación?",
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
                   data: { "idasignacion": id },
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
function exportarReporte() {
        var criterio = $('#razonsocial').val();
     var vdataurl =  UrlHelper.Action("ExportarAsignacion", "Pago", "Pago") +  "?criterio=" + criterio ;
    $(window).attr("location", vdataurl);
}
function rowSave(id,str)
{

  $("#gridasignacion").jqGrid('saveRow',id, finalizando);

}
function finalizando(resonsse)
{

  var respuesta =  JSON.parse(resonsse.responseText);

   if(respuesta.res == false)
   {
     swal({ title: "Error", text: respuesta.Mensaje, type: "error", confirmButtonText: "Aceptar" });
     reload();
     return false;
  }
  else {
    reload();
    return true;
  }

}
