var grilla = $("#gridliquidacion");
var pagergrilla = $("#gridliquidacionpager");


$(document).ready(function (){

    configurarChosenSelect();
    cargargrilladetalle(null);
    configurargrilla();
    configurarfechas();


    $('#btnIncidentes').click(function (){
        var item =  $('#gridliquidacion').jqGrid('getGridParam', 'selrow');  agregarincidentes(item);
    });
     $('#btnAgregarArchivo').click(function (){
        var item =  $('#gridliquidacion').jqGrid('getGridParam', 'selrow');  agregararchivo(item);
    });
     $('#btnExportar').click(function () {
         imprimir();

     })

     $('#btnBuscar').click(function () {
     	reloadgrid();

     })
})

function configurarfechas()
{

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


function configurargrilla()
{

      $.jgrid.defaults.responsive = true;

      $.jgrid.defaults.height = 620;
      //$.jgrid.defaults.width = 200;

    var fechainicio = $('#fechainicio').val();
    var fechafin = $('#fechafin').val();
    var idcliente = $("#idcliente").val();
    var iddestinatario = $("#iddestinatario").val();
	  //var iddestino =$("#iddestino").val();
	  var diastranscurridos = $("#diastranscurridos").val();

	  var vdataurl = $(grilla).data("dataurl") + "?fechainicio=" + fechainicio
      + "&fechafin=" + fechafin
      + "&idcliente=" + idcliente
      //+ "&iddestino=" + iddestino
      + "&iddestinatario=" + iddestinatario
      + "&diastranscurridos=" + diastranscurridos;




     $(grilla).jqGrid({
        url : vdataurl,
        datatype: 'json',
        mtype : 'Get',
        colNames: ['', 'GRT','O/T',  'Remitente', 'Destino','Destinatario' ,'Fec. Recojo','Fec. Despacho ' ,  'Lead Documentario' , 'Días Transcurridos.' , 'Ultima Etapa','Acciones'],
        colModel:
          [
              { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo' ,classes:"grid-col"},
              { key: false, hidden: false, editable: false ,name: 'guiatransportista', index: 'guiatransportista', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'numcp', index: 'numcp', width: '40', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'remitente', index: 'remitente', width: '40', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
              { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'fecharecojo', index: 'fecharecojo', width: '40', align: 'center',classes:"grid-col" ,formatter: 'date' , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'fechadespacho', index: 'fechadespacho', width: '40', align: 'center',classes:"grid-col" ,formatter: 'date' , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'LeadDocumentario', index: 'LeadDocumentario', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'DiasTranscurridos', index: 'DiasTranscurridos', width: '30', align: 'center',classes:"grid-col" ,formatter: semaforo , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'UltimaIncidencia', index: 'UltimaIncidencia', width: '60', align: 'left',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'idordentrabajo', width:'50' , index: 'idordentrabajo' , align: 'center', formatter:  displayButtonLiquidacion,classes:"grid-col"}
          ],
          pager: $(pagergrilla),
          rowNum: 20,
          rowList: [20, 40, 60, 80],
          autowidth: true,
          emptyrecords: 'No se encontraron registros',
          viewrecords: true,
          multiselect: false,
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
function formatedit (cellvalue, options, rowObject)
{
  if(cellvalue == null)
    return "";
  else
    return " "  + cellvalue ;

}
function semaforo(cellvalue, options, rowObject)
{
  if(parseInt(rowObject["LeadDocumentario"])  < parseInt(rowObject["DiasTranscurridos"]))
  {
     return "<span class='label-pill label-danger pull-xs-center'>" +  cellvalue + "</span>";
  }
  else if  (parseInt(rowObject["LeadDocumentario"])  == parseInt(rowObject["DiasTranscurridos"]))
  {
     return "<span class='label-pill label-warning pull-xs-center'>" +  cellvalue + "</span>";
  }
  else {
       return "<span class='label-pill label-primary pull-xs-center'>" +  cellvalue + "</span>";
  }




  if(cellvalue == null)
    return "";
  else
    return "<span class='label-pill label-success pull-xs-center'>" +  cellvalue + "</span>";
}
function displayButtonLiquidacion(cellvalue, options, rowObject)
{

    var verarchivos = "<button type='button'  data-toggle='tooltip' data-placement='top' title='Ver Archivos'  class='btn btn-primary btn-xs btn-outline'  onclick='verarchivos(" + cellvalue + ");' href='#' > <i class='fa fa-cloud-upload'></i></button>";
    var verdetalle = "<button type='button' data-toggle='tooltip' data-placement='top' title='Ver Eventos'  class='btn btn-primary btn-xs btn-outline' onclick='verdetalle(" + cellvalue + ");' href='#' > <i class='fa fa-check'></i></button>";
    var vergrr = "<button type='button' data-toggle='tooltip' data-placement='top' title='Ver GRR'  class='btn btn-primary btn-xs btn-outline' onclick='vergrr(" + cellvalue + ");' href='#' > <i class='fa fa-newspaper-o'></i></button>";
    var editar = "<button type='button' data-toggle='tooltip' data-placement='top'  title='Confirmar' class='btn btn-primary btn-xs btn-outline' onclick='editar(" + cellvalue + ");' href='#' > <i class='fa fa-pencil'></i></button>";
 	return  verarchivos +  verdetalle + vergrr + editar ;
}
function vergrr(id)
{
   var url = UrlHelper.Action("VerDetalleOT", "Liquidacion", "Liquidacion") + "?idorden=" + id;

   $.get(url, function (data) {
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");


   })

}
function editarliquidacion()
{

     var fechaentrega = $("#fechaentregaconciliacion").val();
     var horaentrega = $("#horaentregaconciliacion").val();
     var idordentrabajo = $("#idordentrabajo").val();
     var archivado = $("#archivado").is(":checked");

    if(fechaentrega == "" || horaentrega == "")
    {
                swal({ title: "No puede continuar" ,  text : "Debe ingresar la fecha y hora de la entrega." , type : "error" });
                return ;
    }


     var urlvalidar = UrlHelper.Action("JsonValidarFecha" , "Liquidacion" , "Liquidacion")  ;
     var url = UrlHelper.Action ("JsonEditarLiquidacion","Liquidacion","Liquidacion")
     $.ajax({
         url: urlvalidar,
         type: 'POST',
         dataType: 'json',
         async: true,
         data: { "fechaentrega" : fechaentrega , "horaentrega" : horaentrega , "idorden" : idordentrabajo  }
     }).done(function(data) {
            if(data.res)
            {
               swal({
                  title: "¿Desea continuar con el registro?"
                 , text: data.msj
                 , type: "warning"
                 , closeOnConfirm: false
                 , showCancelButton: true
                 , confirmButtonText: "Si, registrar."}
                 , function(){
                          $.ajax({
                            url: url,
                            type: 'POST',
                            dataType: 'json',
                               data: { "fechaentrega" : fechaentrega , "horaentrega" : horaentrega , "idorden" : idordentrabajo, "archivado" : archivado }
                          })
                          .done(function(data) {
                               if(data.res)
                               {
                                 swal("Se registró con éxito!", "La fecha y la hora han sido registrados con exito", "success");
                                 $("#modalcontainer").modal("hide");
                                 reloadgrid();
                               }

                          })
                          .fail(function() {
                            console.log("error");
                          })
                  });
             }
             else{
                   $.ajax({
                     url: url,
                     type: 'POST',
                     dataType: 'json',
                        data: { "fechaentrega" : fechaentrega , "horaentrega" : horaentrega , "idorden" : idordentrabajo, "archivado" : archivado }
                   })
                   .done(function(data) {
                        if(data.res)
                        {
                          swal("Se registró con éxito!", "La fecha y la hora han sido registrados con exito", "success");
                          $("#modalcontainer").modal("hide");
                          reloadgrid();
                        }

                   })
                   .fail(function() {
                     console.log("error");
                   })
             }
      }) .fail(function() {
          console.log("error");
     })
}

function editar(id)
{
    var url =  UrlHelper.Action("EditarLiquidacion","Liquidacion","Liquidacion") + "?idorden=" + id;
    $.get(url, function (data ) {

          $("#modalcontent").html(data);
          $("#modalcontainer").modal("show");

          $('#horaentregaconciliacion').mask('00:00');

          $("#btnEditarLiquidacion").click(function(event) {
           editarliquidacion();

          });

          $('#dtpfechaentrega .input-group.date').datepicker({
              todayBtn: "linked",
              keyboardNavigation: false,
              forceParse: false,
              calendarWeeks: true,
              autoclose: true,
              format: 'dd/mm/yyyy'

         });


    } )



}
function verdetalle(id)
{
    $("html, body").animate({ scrollTop: "800px" });
    $("#idorden").val(id);
    reloadDetalle(id);
}
function reloadDetalle(id)
{
    var vdataurl = $("#gridliquidaciondetalle").data("dataurl") + "?idorden=" + id;
    $("#gridliquidaciondetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function agregarincidentes(id)
{

    
  var vUrl = UrlHelper.Action("JsonAgregarIncidentes", "Liquidacion", "Liquidacion")  + "?idorden=" +  id;

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

                  AutoCompleteTextBox();


                });


}
function AutoCompleteTextBox()
{
    var idcliente = $("#idcliente").val();
    if(idcliente == "")
    {
      return false;
    }



    var url = UrlHelper.Action("JsonAutocomplete", "Monitoreo", "Monitoreo");


          $.ajax({
                   type: "POST",
                   async: true,
                   url: url ,
                   dataType : 'json',
                   data: { "idcampo": "1"  , "idcliente" : idcliente},
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

function cargargrillaarchivos(id)
{
    // Estas dos lineas, son por defecto 
   $.jgrid.defaults.responsive = true;
   $.jgrid.defaults.height = 220;

   var grilla = $("#gridarchivos"); // define el ID de la grilla 
   var pagergrilla = $("#gridarchivospager"); // define el paginador 

   var vdataurl = $(grilla).data("dataurl") + "?idorden=" + id; // recoge el URL 
    //Arma la grilla
   $(grilla).jqGrid({
     url: vdataurl,  // le pasas el URL 
     datatype: 'json', // el tipo de dato
     mtype: 'Get', // el metodo 
     colNames: ['','Nombre de Archivo','Extensión', 'Acciones'], // los nombres que tendran las columnas
     colModel: // las columnas
     [
         { key: true, hidden: true, name: 'idarchivo', index: 'idarchivo' ,classes:"grid-col"},
         { key: false, hidden: false, name: 'nombrearchivo', index: 'nombrearchivo' ,classes:"grid-col"},
         { key: false, hidden: false, name: 'extension', index: 'extension', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
         { key: false, hidden: false, name: 'idarchivo', index: 'idarchivo',  align: 'center',classes:"grid-col" ,formatter: displayButtonAnular , classes: "grid-col" } // acalos botones ok? ok
     ],
     pager: $(pagergrilla), // el paginador 
     rowNum: 10,
     rowList: [10, 20, 30, 40],  // el resto copias y pegas
     width: "100%", 
     emptyrecords: 'No se encontraron registros',
     viewrecords: true,
     shrinktofit : true,
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
   });

}

function cargargrilladetalle(id)
{

      $.jgrid.defaults.responsive = true;
      $.jgrid.defaults.height = 220;
      //$.jgrid.defaults.width = 200;

      var grilla = $("#gridliquidaciondetalle");
      var pagergrilla = $("#gridliquidaciondetallepager");

      var idorden = $("#idcarga").val();
      var vdataurl = $(grilla).data("dataurl") + "?idorden=" + id;


      $(grilla).jqGrid({
          url: vdataurl,
          datatype: 'json',
          mtype: 'Get',
          colNames: ['', 'OT', 'Usuario', 'Fecha Evento', 'Tipo', 'Evento', 'Observación', 'Recurso', 'Documento', 'Fecha Registro'],
          colModel: [
             { key: true, hidden: true, name: 'ot', index: 'ot', classes: "grid-col" },
             { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '45', align: 'center', classes: "grid-col", formatter: formatedit },
               { key: false, hidden: false, editable: false, name: 'usuario', index: 'usuario', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'fechaevento', index: 'fechaevento', width: '60', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y H:i" } },
             { key: false, hidden: false, editable: false, name: 'tipoevento', index: 'tipoevento', width: '50', align: 'center', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'evento', index: 'evento', width: '100', align: 'left', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'observacion', index: 'observacion', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'recurso', index: 'recurso', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'documento', index: 'documento', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '70', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y H:i" } },
               //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
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
function displayButtonAnular(cellvalue, options, rowObject)
{
    return "<div class='btn-group'><button type='button' class='btn-primary btn btn-xs btn-outline' onclick='downloadFile(" + cellvalue + ");return false;' href='#' > Descargar  <i class='fa fa-download'></i></button>";
    
}
function verarchivos(id)
{

    var vUrl = UrlHelper.Action("ListarArchivos", "Liquidacion", "Liquidacion")  + "?idorden=" +  id;

    $.get(vUrl, function(data) {
         $("#modalcontent").html(data);
         $("#modalcontainer").modal("show");

         cargargrillaarchivos(id);

    });


}

function agregararchivo(id)
{
	 var vUrl = UrlHelper.Action("SubirArchivo", "Liquidacion", "Liquidacion")  + "?idorden=" +  id;
           $.get(vUrl, function (data) {
                  $("#modalcontent").html(data);
                  $("#modalcontainer").modal("show");
                  $("#error").hide();


                  $(function () {
                      $('#frmDocumentacion').submit(function (event)
                      {
                           var dataString;
                           event.preventDefault();
                           var action = $("#frmDocumentacion").attr("action");
                           if ($("#frmDocumentacion").attr("enctype") == "multipart/form-data") {
                                dataString = new FormData($("#frmDocumentacion").get(0));
                                contentType = false;
                                processData = false;
                            }
                            $.ajax({
                              url: action,
                              type: 'POST',
                              dataType: 'json',
                              data: dataString,
                              contentType : contentType,
                              processData : processData,
                             }).done(function(data) {
                                          if(data.res)
                                          {
                                            $("#modalcontainer").modal("hide");
                                            swal({
                                            title: "Registro Exitoso",
                                            text: data.msj,
                                            type: "success"
                                            });
                                          }
                                          else {
                                             $("#error").fadeIn(2000).fadeOut(5000);
                                          }
                                   }).fail(function() {
                                     console.log("error");
                                   }).always(function() {
                                     console.log("complete");
                                   });
                      });
                  });
          });
}
function reloadgrid()
{

    var idcliente = $("#idcliente").val();
    var numcp = $("#numcp").val();
    var grr = $("#grr").val();
    var fecinicio = $("#fechainicio").val();
    var fecfin = $("#fechafin").val();
    var diastranscurridos = $("#diastranscurridos").val();



    var vdataurl = $(grilla).data("dataurl") + "?idcliente=" + idcliente + "&numcp=" + numcp + "&grr=" + grr + "&fechainicio=" + fecinicio + "&fechafin=" + fecfin + "&diastranscurridos=" + diastranscurridos;
    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true)
    {
      $("#modalcontainer").modal("hide");
       swal({
           title: "Registro Exitoso",
           text: jsonres.msj,
            type: "success"
        });
    }
    else
    {
        //sweetAlert(jsonres.titulo, jsonres.msj, "warning");
       /// $("#modalcontainer").modal("hide");
    }
}
function imprimir()
{
    var fechainicio = $('#fechainicio').val();
    var fechafin = $('#fechafin').val();
    var idcliente = $("#idcliente").val();
    var iddestino = $("#iddestino").val();


    var url = UrlHelper.Action("ExportarLiquidacion", "Liquidacion", "Liquidacion")   + "?fechainicio=" + fechainicio
    + "&fechafin=" + fechafin
    + "&idcliente=" + idcliente
    + "&iddestino=" + iddestino
    + "&iddestinatario=" + iddestinatario
    

     $(window).attr("location", url);
    
}
function downloadFile(archivo) {

    var url = UrlHelper.Action("DownloadArchivo", "Liquidacion", "Liquidacion") + "?idarchivo=" + archivo;
    //var url = $('#tblDocumentos').data("urldwn") + "?archivo=" + archivo;
    window.location = url;
}