
var grilla = $("#grideventos");
var pagergrilla = $("#grideventospager");


$(document).ready(function () {
  $("html, body").animate({ scrollTop: "100px" });

  $("#btnBuscar").click(function(event) {






        reload();
  });


  $("#btnEliminar").click(function(event) {

     eliminarevento();

  });

  configurarGrilla();

});
function eliminarevento()
{
   var url = UrlHelper.Action ("JsonEliminarUltimoEvento", "Monitoreo", "Monitoreo");
   swal({
       title: "Eliminar Último Evento",
       text: "¿Está seguro que desea eliminar el último evento?",
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
                  data: { idorden: $("#idsorden").val() , numcp :   $("#numcp").val() },
                  success: function (data) {
                      if (data.res) {
                          swal("¡Se eliminó correctamete!", "Se ha eliminado  correctamente", "success");
                          reload();
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
function reload()
{
var numcp = $("#numcp").val();
var idmaestroincidencia = $("#idmaestroincidencia").val();
var idmaestroetapa = $("#idmaestroetapa").val();

var vdataurl = UrlHelper.Action("JsonGetListarEventos","Monitoreo", "Monitoreo") + "?numcp=" + numcp
+ "&idmaestroincidencia=" + idmaestroincidencia
+ "&idmaestroetapa=" + idmaestroetapa;


    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}


function  configurarGrilla()
{
  $.jgrid.defaults.width = 200;
  $.jgrid.defaults.height = 300;


  var numcp = $("#numcp").val();
  var idmaestroincidencia = $("#idmaestroincidencia").val();
  var idmaestroetapa = $("#idmaestroetapa").val();


  var url = UrlHelper.Action("JsonGetListarEventos","Monitoreo", "Monitoreo") + "?numcp=" + numcp
  + "&idmaestroincidencia=" + idmaestroincidencia
  + "&idmaestroetapa=" + idmaestroetapa;

    $(grilla).jqGrid({
       url: url,
       datatype: 'json',
       mtype: 'Get',
       colNames: ['', 'OT', 'Usuario','Fecha Evento','Tipo', 'Evento', 'Observación' ,'Recurso', 'Documento','Estación Origen','Fecha Registro'   ],
       colModel: [
          { key: true, hidden: true, name: 'ot', index: 'ot' ,classes:"grid-col" },
          { key: false, hidden: false, editable: false ,name: 'numcp', index: 'numcp', width: '45', align: 'center' , classes:"grid-col",formatter: formatedit },
            { key: false, hidden: false, editable: false ,name: 'usuario', index: 'usuario',   width: '60', align: 'left' , classes:"grid-col",formatter: formatedit},
          { key: false, hidden: false, editable: false ,name: 'fechaevento', index: 'fechaevento', width: '60', align: 'center' , classes:"grid-col",formatter: 'date' ,formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y H:i" } },
          { key: false, hidden: false, editable: false ,name: 'tipoevento', index: 'tipoevento', width: '50', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'evento', index: 'evento', width: '70', align: 'left' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'observacion', index: 'observacion', width: '160', align: 'left' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'recurso', index: 'recurso', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit},
          { key: false, hidden: false, editable: false ,name: 'documento', index: 'documento', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit},
          { key: false, hidden: false, editable: false ,name: 'estacionorigen', index: 'estacionorigen', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit},
          { key: false, hidden: false, editable: false ,name: 'fecharegistro', index: 'fecharegistro', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y H:i" }},
            //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
       ],
       pager: $(pagergrilla),
       rowNum: 80,
       rowList: [160, 240, 320],
       autoResizing : { compact : true},
       emptyrecords: 'No se encontraron registros',
       autowidth: true,
       viewrecords: true,
       autoheight: true,
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
