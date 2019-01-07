$(document).on("keydown", "input", function(e) {
  if (e.which==13) e.preventDefault();
});
                    



$(document).ready(function () {
   



     
    configurargrillamonitoreo();
    cargargrilladetalle(null);

    debugger


  $('#dtpfechaini .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'

    });

  $('#dtpfechafin .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'

    });

   $('#btnBuscar').click(function (){
     reloadgrillamonitoreo();
    });

    $('#btnIncidentes').click(function (){
        var item =  $('#gridmonitoreo').jqGrid('getGridParam', 'selrow');  agregarincidentes(item);
    });
    $('#btnCierreOperativo').click(function () {
        var item = $('#gridmonitoreo').jqGrid('getGridParam', 'selrow'); CierreOperativo(item);
    });
    $('#btnConfirmarEntrega').click(function (){
        var item =  $('#gridmonitoreo').jqGrid('getGridParam', 'selrow');  confirmarentrega_manifiesto(item);
    });
        $('#btnVerTimeLime').click(function (){
        var item =  $('#gridmonitoreo').jqGrid('getGridParam', 'selrow');  timelime(item);
    });
  //
  



});

//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////

function agregarincidentes_single(id)
{


   var vUrl = UrlHelper.Action("JsonAgregarIncidentes", "Monitoreo", "Monitoreo")  + "?idmanifiesto=&idorden=" + id;
   var url = UrlHelper.Action("JsonValidarEtapa", "Monitoreo", "Monitoreo")  ;

           $.get(vUrl, function (data) {
                  $("#modalcontent").html(data);
                  $("#modalcontainer").modal("show");

				  $('#idmaestroincidencia option').each(function (index, element) {
				                      if (element.value == 23) $(element).css('background-color', '#a6c3c2')
				                      if (element.value == 24) $(element).css('background-color', '#a6c3c2')
				                      if (element.value == 25) $(element).css('background-color', '#a6c3c2')
				                  })


                  $("#_idetapa").hide();

                  $('#idmaestroincidencia').change(function () {
                         $.ajax({
                               type: "POST",
                               async: true,
                               url: url ,
                               data: { "idmaestroincidencia": $('#idmaestroincidencia').val() },
                               success: function (data) {
                                   
                                if(data.tipo == 'M')
                                {
                                      $("#_idetapa").show(1000);
                                }
                                else
                                {
                                    $("#_idetapa").hide(1000);
                                }
                                


                               },
                               error: function (request, status, error)
                               {
                                   swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                               }
                         });

                  });

                  $('#dtpfechacomp .input-group.date').datepicker({
                  todayBtn: "linked",
                  keyboardNavigation: false,
                  forceParse: false,
                  calendarWeeks: true,
                  autoclose: true,
                  format: 'dd/mm/yyyy'

                   }); 

                  AutoCompleteTextBox();


                });
}

function agregarincidentes(id) 
{
  var vUrl = UrlHelper.Action("JsonAgregarIncidentes", "Monitoreo", "Monitoreo")  + "?idmanifiesto= " +  id + "&idorden=" ;
  var url = UrlHelper.Action("JsonValidarEtapa", "Monitoreo", "Monitoreo")  ;

           $.get(vUrl, function (data) {
                  $("#modalcontent").html(data);
                  $("#modalcontainer").modal("show");

                  $('#idmaestroincidencia option').each(function (index, element) {
                      if (element.value == 23) $(element).css('background-color', '#a6c3c2')
                      if (element.value == 24) $(element).css('background-color', '#a6c3c2')
                      if (element.value == 25) $(element).css('background-color', '#a6c3c2')
                  })

                  $('#horaincidencia').mask('00:00');
                   $("#_idetapa").hide();
                  $('#idmaestroincidencia').change(function () {
                         $.ajax({
                               type: "POST",
                               async: true,
                               url: url ,
                               data: { "idmaestroincidencia": $('#idmaestroincidencia').val() },
                               success: function (data) {
                                if(data.tipo == 'M')
                                {
                                      $("#_idetapa").show(1000);
                                }
                                else
                                {
                                    $("#_idetapa").hide(1000);
                                }
                                


                               },
                               error: function (request, status, error)
                               {
                                   swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                               }
                         });

                  });

                    $('#dtpfechacomp .input-group.date').datepicker({
                  todayBtn: "linked",
                  keyboardNavigation: false,
                  forceParse: false,
                  calendarWeeks: true,
                  autoclose: true,
                  format: 'dd/mm/yyyy'

                   });

                  AutoCompleteTextBox();


                });


}
function CierreOperativoOrden(id)
{
     var url = UrlHelper.Action("CierreOperativoOrden", "Monitoreo", "Monitoreo") ;

    swal({
        title: "Cierre Operativo",
        text: "¿Está seguro que desea cerrar el manifiesto?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Cerrar',
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function () {   
       $.ajax(
                   {
                       type: "POST",
                       async: true,
                       url: url ,
                       data: { "idorden": id },
                       success: function (data) {
                            if(data.res)
                            {
                                reloadgrillamonitoreo();
                               swal("¡Se ha cerrado correctamente!", data.msj, "success");
                            }
                            else
                            {
                                 swal("¡No puede cerrar la orden!", data.msj, "warning");
                            }
                       },
                       error: function (request, status, error)
                       {
                           swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                       }
                   });
              });

}
function CierreOperativo(id)
{
     var url = UrlHelper.Action("CierreOperativoManifiesto", "Monitoreo", "Monitoreo") ;

    swal({
        title: "Cierre Operativo",
        text: "¿Está seguro que desea cerrar el manifiesto?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Cerrar',
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function () {   
       $.ajax(
                   {
                       type: "POST",
                       async: true,
                       url: url ,
                       data: { "idmanifiesto": id },
                       success: function (data) {
                        if(data.res)
                         {
                           reloadgrillamonitoreo();
                           swal("¡Se ha cerrado correctamente!", data.msj, "success");
                         }
                         else
                         {
                           swal("¡No puede cerrar la orden!", data.msj, "warning");
                         }
                       },
                       error: function (request, status, error)
                       {
                           swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                       }
                   });
              });

}
function timelime(id)
{


        var vUrl = UrlHelper.Action("JsonTimeLine", "Monitoreo", "Monitoreo")  + "?ids= " +  id;

           $.get(vUrl, function (data) {
                  $("#modalcontent").html(data);
                  $("#modalcontainer").modal("show");
                  
                });
}
function confirmarentrega_manifiesto(id)
{

 
  var vUrl = UrlHelper.Action("JsonAgregarIncidentes2", "Monitoreo", "Monitoreo")  + "?idorden=&idmanifiesto="  + id;

           $.get(vUrl, function (data) {
                  $("#modalcontent").html(data);
                  $("#modalcontainer").modal("show");

                  $('#horaincidencia').mask('00:00');


                    $('#dtpfechacomp .input-group.date').datepicker({
                  todayBtn: "linked",
                  keyboardNavigation: false,
                  forceParse: false,
                  calendarWeeks: true,
                  autoclose: true,
                  format: 'dd/mm/yyyy'

                   });

                     $("#btnAgregarGuia").click(function () {
                       agregarguia();
                    });




                  
                });

}
function confirmarentrega(id)
{
        var vUrl = UrlHelper.Action("JsonAgregarIncidentes2", "Monitoreo", "Monitoreo")  + "?idorden= " +  id + "&idmanifiesto=" ;

           $.get(vUrl, function (data) {
                  $("#modalcontent").html(data);
                  $("#modalcontainer").modal("show");

                  $('#horaincidencia').mask('00:00');


                    $('#dtpfechacomp .input-group.date').datepicker({
                  todayBtn: "linked",
                  keyboardNavigation: false,
                  forceParse: false,
                  calendarWeeks: true,
                  autoclose: true,
                  format: 'dd/mm/yyyy'

                   });

                     $("#btnAgregarGuia").click(function () {
                       agregarguia();
                    });




                  
                });
}

function agregarguia()
{
   var numeroguia = $("#nroguia").val();
   var cantidad = $("#cantidad").val();

  var url = UrlHelper.Action ("JsonAgregarGuia","Monitoreo","Monitoreo");

    $.ajax({
      type: "POST",
      async:true,
      url: url,
      data: {"numeroguia" : numeroguia , "cantidad" : cantidad},
      success : function (data){
        if(data.res)
        {
             var $select = $('#idguias');
                       $select.empty();
                       $.each(data.data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
        }
        else
        {
             swal("¡No se puede agregar!", data.msj, "warning");
        }
                    
      },
      error:function (request, status,error)
      {

      }

    });


}
function reloadgrillamonitoreo()
{

      var fechainicio = $('#fechainicio').val();
      var fechafin = $('#fechafin').val();
      var idruta = $("#idruta").val();
      var idestacion =$("#idestacion").val();
      var guia =$("#guia").val();
      var chofer =$("#chofer").val();
      var placa = $("#placa").val();


     var vdataurl  =  UrlHelper.Action("JsonGetListarMonitoreoManifiesto", "Monitoreo", "Monitoreo") + "?fechainicio=" +  fechainicio 
      + "&fechafin=" +  fechafin  
      + "&idruta=" + idruta 
      + "&idestacion=" + idestacion 
      + "&guia=" + guia
       + "&chofer=" + chofer
       + "&placa=" + placa





      $("#gridmonitoreo").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
var lastSel = 0;
function configurargrillamonitoreo()
{
      $.jgrid.defaults.height = 320;
      $.jgrid.defaults.width = 2000;
      //$.jgrid.defaults.responsive = true;

      var grilla = $("#gridmonitoreo");
      var pagergrilla = $("#gridmonitoreopager");

      var fechainicio = $('#fechainicio').val();
      var fechafin = $('#fechafin').val();
      var idruta = $("#idruta").val();
      var idestacion =$("#idestacion").val();
      var guia = $("#guia").val();
      var chofer =$("#chofer").val();
      var placa = $("#placa").val();


   //   var vdataurl = 




       var dataurl  =  UrlHelper.Action("JsonGetListarMonitoreoManifiesto", "Monitoreo", "Monitoreo") + "?fechainicio=" +  fechainicio 
      + "&fechafin=" +  fechafin  
      + "&idruta=" + idruta 
      + "&idestacion=" + idestacion 
      + "&guia=" + guia
       + "&chofer=" + chofer
       + "&placa=" + placa



       $(grilla).jqGrid({
         url: dataurl,
         datatype: 'json',
         mtype: 'Get',
         colNames: ['','# Manifiesto', 'Fec. Salida', 'Hora Salida','Tipo Operación','destino','Placa'],
      colModel:
          [
              { key: true, hidden: true, name: 'idmanifiesto', index: 'idmanifiesto' ,classes:"grid-col"},
              { key: false, hidden: false, editable: false ,name: 'nummanifiesto', index: 'nummanifiesto', width: '100', align: 'left',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'fechasalida', index: 'fechasalida', width: '100', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'horasalida', index: 'horasalida', width: '100', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'tipooperacion', index: 'tipooperacion', width: '200', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'destino', index: 'destino', width: '200', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
               { key: false, hidden: false, editable: false ,name: 'placa', index: 'placa', width: '90', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
          ],
          rowNum:8,
         subGrid: true,
        multiselect: false,
        autowidth: true,
        responsive:true,
        shrinkToFit: false,   
                onSelectRow: function (rowId, status, e) {
          if (rowId == lastSel) {
              $(this).jqGrid("resetSelection");
              lastSel = undefined;
              status = false;
          } else {
              lastSel = rowId;
          }
        },
         beforeSelectRow: function (rowId, e) {
            $(this).jqGrid("resetSelection");
            return true;
        },   
        subGridBeforeExpand: function(divid, rowid) {
         // #grid is the id of the grid
          var expanded = jQuery("td.sgexpanded", "#gridmonitoreo")[0];
            if(expanded) {
              setTimeout(function(){
                  $(expanded).trigger("click");
              }, 100);
            }
          },
         subGridOptions: {
            "plusicon"  : "ui-icon-triangle-1-e",
            "minusicon" : "ui-icon-triangle-1-s",
            "openicon"  : "ui-icon-arrowreturn-1-e",
            "expandOnLoad" : false,
            "selectOnExpand" : true,
            "reloadOnExpand" : false,
          },
         subGridRowExpanded: function(subgrid_id, row_id) {
          var subgrid_table_id, pager_id;
          subgrid_table_id = subgrid_id+"_t";
          pager_id = "p_"+subgrid_table_id;
          $("#"+subgrid_id).html("<table id='"+subgrid_table_id+"' class='scroll'></table><div id='"+pager_id+"' class='scroll'></div>");

          jQuery("#"+subgrid_table_id).jqGrid({
              url: UrlHelper.Action("JsonGetListarMonitoreo", "Monitoreo", "Monitoreo") + "?idmanifiesto=" +  row_id ,
              datatype: 'json',
              mtype: 'Get',

              colNames: ['','Acciones', 'Det. ', 'Entregado al Cliente', '# Manifiesto', '# Orden' , 'Botica', 'Origen','Modalidad', 'Tipo de Carga', 'Fec. Registro',  'Remitente' , 'Destinatario',  'GR Cliente' , 'Bulto', 'Peso' , 'Vol','Dirección Destino', 'Recurso', 'Documento' , 'Ult. Recurso', 'Ult. Documento'  ],
              colModel:
              [
                  { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo' ,classes:"grid-col"},
                  { key: false, hidden: false, editable: false ,name: 'idordentrabajo', width:'100' , index: 'idordentrabajo' , align: 'center', formatter:  displayButtonsOperacion,classes:"grid-col"}   ,
                  { key: false, hidden: false, editable: false ,name: 'idordentrabajo', width:'100' , index: 'idordentrabajo' , align: 'center', formatter:  displayButtonsOperacion2,classes:"grid-col"}   ,
                  { key: false, hidden: false, editable: false ,name: 'idordentrabajo', width:'100' , index: 'idordentrabajo' , align: 'center', formatter:  displayButtonsOperacion3,classes:"grid-col"}   ,
                  { key: false, hidden: false, editable: false ,name: 'nummanifiesto', index: 'nummanifiesto', width: '0', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'numcp', index: 'numcp', width: '80', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'coddireccionorigen', index: 'coddireccion', width: '80', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'origen', index: 'origen', width: '80', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'tipotransporte', index: 'tipotransporte', width: '80', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'descripciongeneral', index: 'descripciongeneral', width: '120', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'fecregistro', index: 'fecregistro', width: '90', align: 'center',classes:"grid-col" ,formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "m/d/Y h:i A" } , classes: "grid-col" },
             //     { key: false, hidden: false, editable: false ,name: 'fechasalida', index: 'fechasalida', width: '70', align: 'center',classes:"grid-col" ,formatter: 'date' , classes: "grid-col" },
             //     { key: false, hidden: false, editable: false ,name: 'tipooperacion', index: 'tipooperacion', width: '120', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'remitente', index: 'remitente', width: '90', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'destinatario', index: 'destinatario', width: '90', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
             //     { key: false, hidden: false, editable: false ,name: 'destino', index: 'destino', width: '80', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'nroguia', index: 'nroguia', width: '80', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'bulto', index: 'bulto', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'peso', index: 'peso', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'volumen', index: 'volumen', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'coddirecciondestino', index: 'coddirecciondestino', width: '380', align: 'left',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'recursos', index: 'recursos', width: '90', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'documentos', index: 'documentos', width: '120', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'UltimoRecurso', index: 'recursos', width: '90', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
                  { key: false, hidden: false, editable: false ,name: 'UltimoDocumento', index: 'documentos', width: '120', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" }
                 
                  
              ],
              pager: $(pagergrilla),
              rowNum: 40,
              rowList: [40, 50, 60, 70],
              //autowidth: true,
              emptyrecords: 'No se encontraron registros',
              viewrecords: true,
              height: '100%',
              width : '100%',
              responsive:true,
              shrinkToFit: false,          
              jsonReader:
              {
                  root: "rows",
                  page: "page",
                  total: "total",
                  records: "records",
                  repeatitems: false,
                  id: 0
              },
              beforeSelectRow: function(rowid, e) {
                  return false;
              }
          });
          jQuery("#"+subgrid_table_id).jqGrid('navGrid',"#"+pager_id,{edit:false,add:false,del:false})
      }
   })
}




function cargargrilladetalle(id)
{
     

      $.jgrid.defaults.height = 620;
      $.jgrid.defaults.width = 500;
      $.jgrid.defaults.responsive = true;
      var grilla = $("#griddetalle");
      var pagergrilla = $("#griddetallepager");

      var idorden = $("#idcarga").val();
      

      var vdataurl = $(grilla).data("dataurl") + "?idorden=" + idorden; 
      

      $(grilla).jqGrid({
          url: vdataurl,
          datatype: 'json',
          mtype: 'Get',
          colNames: ['', '' ,'Cod','# Orden', 'Fecha' 
          , 'Usuario', 'Incidencia', 'Descripción' ,  'Acciones'],
          colModel:
          [
              { key: true, hidden: true, name: 'idincidente', index: 'idincidente' ,classes:"grid-col"},
              { key: false, hidden: true, name: 'visible', index: 'visible' ,classes:"grid-col"},
              { key: false, hidden: false, editable: false ,name: 'codincidencia', index: 'codincidencia', width: '20', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'numcp', index: 'numcp', width: '40', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'fechaincidencia', index: 'fechaincidencia', width: '60', align: 'center',classes:"grid-col" ,formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "m/d/Y h:i A" }  , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'usuario', index: 'usuario', width: '40', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'incidencia', index: 'incidencia', width: '100', align: 'left',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'descripcion', index: 'descripcion', width: '160', align: 'left',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'idincidencia', index: 'idincidencia', width: '50', align: 'center',classes:"grid-col" 
              ,formatter: displayButtonAnular 
              , classes: "grid-col" },


          ],
          pager: $(pagergrilla),
          rowNum: 10,
          rowList: [10, 20, 30, 40],
          autowidth: true,
          emptyrecords: 'No se encontraron registros',
          viewrecords: true,
          editable:false,
          jsonReader:
          {
              root: "rows",
              page: "page",
              total: "total",
              records: "records",
              repeatitems: false,
              id: 0
          },
            loadComplete: function (data) {
               setStyleCheckBoxGrid(this);
              
              // for (var i = 0; i < idsOfSelectedRows.length; i++){
              //       $("#gridcargas").setSelection(idsOfSelectedRows[i], true);
              // }
              // var numerofilas = $(this).getGridParam("records");
              //
          },
          beforeSelectRow: function(rowid, e)
          {
            jQuery("#griddespacho").jqGrid('resetSelection');
            return(true);
          }

        
      });



}




function verdetalle(id)
{

    $("html, body").animate({ scrollTop: "800px" });
    $("#idmonitoreo").val(id);
    reloadDetalle(id);



}
function reloadDetalle(id)
{
    var vdataurl = $("#griddetalle").data("dataurl") + "?idorden=" + id;
    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

}


function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
  //  CleanValidationError();

    if (jsonres.res == true)
    {

      $("#modalcontainer").modal("hide");
     reloadgrillamonitoreo();



       swal({
           title: "Registro Exitoso",
           text: jsonres.msj,
            type: "success"
        });
    }
    else
    {
        sweetAlert(jsonres.titulo, jsonres.msj, "warning");
        $("#modalcontainer").modal("hide");
        reloadgrillamonitoreo();
    }

}
function formatedit (cellvalue, options, rowObject)
{
  if(cellvalue == null)
    return "";
  else
    return " "  + cellvalue ;
  
}
function displayButtonVehiculo(cellvalue, options, rowObject)
    {


        var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='AsignarVehiculo(" + cellvalue + ");' href='#' > <i class='fa fa-plus'></i> Asignar</button>";

        return  asignar ;
    }
function displayButtonsOperacion(cellvalue, options, rowObject)
{

        var ver =  "<div class='btn-group'><button type='button' title='Ver Incidencias' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='verdetalle(" + cellvalue + ");' href='#' > <i class='fa fa-magic'></i></button>"  ;
         var incidencia =  "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='agregarincidentes_single(" + cellvalue + ");' href='#' > <i class='fa fa-hand-o-right'></i></button>";
         var cerrar =  "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='CierreOperativoOrden(" + cellvalue + ");' href='#' > <i class='fa fa-forward'></i></button>";
         var verdetalle =  "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-success btn btn-xs btn-outline' onclick='verdetalleot(" + cellvalue + ");' href='#' > <i class='fa fa-pencil'></i></button></div>"
         return ver  + incidencia + verdetalle + cerrar;
}
function displayButtonsOperacion2(cellvalue, options, rowObject)
{

        var ver =  "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='timelime(" + cellvalue + ");' href='#' >Ciclo de Vida</button>"  ;
         return ver  ;
}
function displayButtonsOperacion3(cellvalue, options, rowObject)
{
          if(rowObject.idestado == 12)
          {
             return "<span class='label label-primary'>" + "Confirmado " + "</span>";
          }

         var ver =  "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='confirmarentrega(" + cellvalue + ");' href='#' >Confirmar</button>"  ;
         return ver  ;
}


function displayButtonsEstados(cellvalue, options, rowObject)
{
  if(cellvalue > 0)
                   return "<span class='label label-primary'>" + " Sí " + "</span>";
                else
                  return "<span class='label label-info'>" + " Pendiente " + "</span>";
   

}
function displayButtonsManifiesto(cellvalue, options, rowObject)
{
            if(cellvalue != null)
               return "<span class='label label-primary'>" + " Sí " + "</span>";
            else
              return "<span class='label label-info'>" + " Pendiente " + "</span>";
                                   
   

}
function formatscan(cellvalue, options, rowObject)
{
    if(cellvalue > 0)
               return "<span class='label label-primary'>" + " Sí " + "</span>";
            else
              return "<span class='label label-info'>" + " No " + "</span>";

}
function displayButtonAnular(cellvalue, options, rowObject)
{
  if(rowObject.visible!=false)
  {
        var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='anularincidencia(" + cellvalue + ");' href='#' > <i class='fa fa-chain-broken'></i> Eliminar</button>";
        return  asignar ;
   }
   else 
    return "";
}

function verorden(id)
{

    var url = UrlHelper.Action("AsignarPrecinto", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        var dlbAccesoriosODS = $('select[name="AccesoriosSeleccionados"]').bootstrapDualListbox({

            nonSelectedListLabel: 'Disponibles',
            selectedListLabel: 'Seleccionados',
            showFilterInputs: true,
            moveOnSelect: true,


        });


    });
    
}
function anularincidencia(id)
{

    
     var url = UrlHelper.Action("JsonAnularIncidencia", "Monitoreo", "Monitoreo");
    swal({
        title: "Anular Incidencia",
        text: "¿Está seguro que desea eliminar la incidencia?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Anular',
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function () {
        $.ajax({
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "id": id },
                   success: function (data) {
                       swal("¡Se ha anulado correctamente!", data.msj, "success");

                       var id =  $("#idmonitoreo").val();
                       reloadDetalle(id);
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
          
     });

}

function AutoCompleteTextBox()
{
    

    var url = UrlHelper.Action("JsonAutocomplete", "Monitoreo", "Monitoreo");

        $.ajax({
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idcampo": "1" },
                   success: function (data) {
                             var options = '';
                             for(var i = 0; i < data.length; i++)
                             { 
                               options += '<option value="'+data[i]+'" />';
                             }
                             $("#helps").html(options);
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
}


function Exportar()
{


        var fechainicio = $('#fechainicio').val();
      var fechafin = $('#fechafin').val();
      var idruta = $("#idruta").val();
      var idestacion =$("#idestacion").val();
      var guia = $("#guia").val();
      var chofer =$("#chofer").val();
      var placa = $("#placa").val();
     
      var vdataurl = UrlHelper.Action("ExportarMonitoreo", "Monitoreo", "Monitoreo") + "?fechainicio=" +  fechainicio 
      + "&fechafin=" +  fechafin  
      + "&idruta=" + idruta 
      + "&idestacion=" + idestacion 
      + "&guia=" + guia
       + "&chofer=" + chofer
       + "&placa=" + placa
     $(window).attr("location", vdataurl);

}


function verdetalleot(id)
{

   var url = UrlHelper.Action("VerDetalleOT", "Monitoreo", "Monitoreo") + "?idorden=" +  id; 


  $.get(url, function (data) {
      $("#modalcontent").html(data);
      $("#modalcontainer").modal("show");


  });



}

function configurarGrillaGuias() {

    $.jgrid.defaults.width = 400;
    $.jgrid.defaults.height = 180;

    var grilla = $("#griddetallepu");
    var pagergrilla = $("#griddetallepupager");
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
        loadComplete : function () {
          SumatoriaGrilla();
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
