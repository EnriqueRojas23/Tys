var grilla = $("#gridembarque");
var pagergrilla = $("#gridembarquepager");

var grilladetalle = $("#griddetalle");
var pagergrilladetalle = $("#griddetallepager");

function  configurarGrillaDetalle()
{
  $.jgrid.defaults.width = 200;
  $.jgrid.defaults.height = 300;


  var idembarque = $("#idembarque").val();

  var url = UrlHelper.Action("JsonGetListarDetalleEmbarqueFluvial","Monitoreo", "Monitoreo") + "?idembarque=" + idembarque;

    $(grilladetalle).jqGrid({
       url: url,
       datatype: 'json',
       mtype: 'Get',
       colNames: ['', 'OT','Manifiesto', 'Origen', 'Destino','Tipo Operación', 'FH Despacho', 'Bultos' ,'Peso'
       , 'Volumen','FH Carga', 'RI' ],
       colModel: [

          { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo' ,classes:"grid-col" },
          { key: false, hidden: false, editable: false ,name: 'numcp', index: 'numcp', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'nummanifiesto', index: 'nummanifiesto', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'origen', index: 'origen', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'distrito', index: 'distrito', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'tipooperacion', index: 'tipooperacion', width: '120', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'fechadespacho', index: 'fechadespacho', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }},
          { key: false, hidden: false, editable: false ,name: 'bulto', index: 'bulto', width: '40', align: 'center' , classes:"grid-col",formatter: formatedit },
          { key: false, hidden: false, editable: false ,name: 'Peso', index: 'Peso', width: '40', align: 'center' , classes:"grid-col",formatter: formatedit},
          { key: false, hidden: false, editable: false ,name: 'Volumen', index: 'Volumen', width: '40', align: 'center' , classes:"grid-col",formatter: formatedit},
          { key: false, hidden: false, editable: false ,name: 'fechadespacho', index: 'fechadespacho', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }},
          { key: false, hidden: false, editable: false ,name: 'reintegrotributario', index: 'reintegrotributario', width: '40', align: 'center' , classes:"grid-col",formatter: semaforo}

        //  { key: false, hidden: false, editable: false ,name: 'idmanifiesto', width:'90' , index: 'idmanifiesto' ,  formatter:  displayButtonsDetalles,classes:"grid-col"}
            //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
       ],
       pager: $(pagergrilla),
       rowNum: 30,
       rowList: [30, 60, 90],
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
function semaforo(cellvalue, options, rowObject)
{
  if( rowObject["reintegrotributario"] == true  )
  {
     return "<span class='label-pill label-danger pull-xs-center'>SI</span>";
  }
  else
  {
     return "<span class='label-pill label-warning pull-xs-center'>NO</span>";
  }
}
function  configurarGrilla()
{

  $.jgrid.defaults.width = 200;
  $.jgrid.defaults.height = 300;

  var numeroembarque = $("#numeroembarque").val();
  var idtransporte = $("#idtransporte").val();

  var vdataedit = $(grilla).data("editurl");

  var url = UrlHelper.Action("JsonGetListarEmbarqueFluvial","Monitoreo", "Monitoreo") + "?numeroembarque=" + numeroembarque
  + "&idtransporte=" + idtransporte;


  $(grilla).jqGrid({
     url: url,
     datatype: 'json',
     mtype: 'Get',
     colNames: ['', 'N° EF', 'CE','Vehículo', 'Puerto','Fec. Inicio Carga', 'Fec. Fin Carga' ,'Fec. Zarpe'
     , 'Fec. Llegada','Fec. Atraque' ,'Acciones'  ],
     colModel: [
        { key: true, hidden: true, name: 'idembarque', index: 'idembarque' ,classes:"grid-col" },
        { key: false, hidden: false, editable: false ,name: 'numeroembarque', index: 'numeroembarque', width: '45', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: false ,name: 'conocimientoembarque', index: 'conocimientoembarque', width: '40', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: false ,name: 'transporte', index: 'transporte', width: '70', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: false ,name: 'puerto', index: 'puerto', width: '60', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: false ,name: 'fechainiciocarga', index: 'fechainiciocarga', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }},
        { key: false, hidden: false, editable: false ,name: 'fechafincarga', index: 'fechafincarga', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y"}},
        { key: false, hidden: false, editable: false ,name: 'fechazarpe', index: 'fechazarpe', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }},
        { key: false, hidden: false, editable: false ,name: 'fechallegada', index: 'fechallegada', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }},
        { key: false, hidden: false, editable: false ,name: 'fechaatraque', index: 'fechaatraque', width: '70', align: 'center' , classes:"grid-col",formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }},
        { key: false, hidden: false, editable: false ,name: 'idembarque', width:'110' , index: 'idembarque' ,  formatter:  displayButtons,classes:"grid-col"}
          //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
     ],
     pager: $(pagergrilla),
     rowNum: 30,
     rowList: [30, 60, 90],
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
function displayButtons(cellvalue, options, rowObject)
{

        var editar = "<button type='button' title='Editar' class='btn btn-primary  btn-xs btn-outline' onclick=\"editarembarque('" + cellvalue  + "');\"><i class='fa fa-edit'></i> </button>";
        var asociar = "<button type='button' title='Asociar/Desasociar' class='btn btn-primary btn-xs btn-outline' onclick=\"asociarmanifiesto('" + cellvalue  + "');\"><i class='fa fa-plus'></i> </button>";
        var descarga = "<button type='button' title='Descarga' class='btn btn-primary btn-xs btn-outline' onclick=\"descargarmanifiesto('" + cellvalue  + "');\"><i class='fa fa-arrow-circle-down'></i> </button>";
        var sunat = "<button type='button' title='Control Sunat' class='btn btn-primary btn-xs btn-outline' onclick=\"controlsunat('" + cellvalue  + "');\"><i class='fa fa-male'></i> </button>";
        var seguimiento = "<button type='button' title='Seguimiento' class='btn btn-primary btn-xs btn-outline' onclick=\"seguimientofluvial('" + cellvalue + "')\"><i class='fa fa-calendar'></i></button>";
        var eliminar = "<button type='button' title='Eliminar' title='Cancelar' class='btn btn-primary btn-xs btn-outline' onclick=\"eliminarembarque('"+ cellvalue + "');\"><i class='fa fa-trash'></i> </button>";
        var verdetalle = "<button type='button' title='Detalle' class='btn btn-primary btn-xs btn-outline' onclick=\"verdetalle('"+ cellvalue + "');\"><i class='fa fa-search'></i> </button>";

        return editar + asociar + seguimiento + descarga + sunat + eliminar + verdetalle;
}
function displayButtonsDetalles(cellvalue, options, rowObject)
{
        var verdetalle = "<button type='button' title='Cancelar' class='btn btn-primary btn-xs btn-outline' onclick=\"verdetalle('"+ cellvalue + "');\"><i class='fa fa-search'></i> </button>";
        return verdetalle;
}
function reloaddetalle()
{

    var idembarque = $("#idembarque").val();
    var vdataurl = UrlHelper.Action("JsonGetListarDetalleEmbarqueFluvial","Monitoreo","Monitoreo") + "?idembarque=" +idembarque
     $(grilladetalle).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function reload()
{
  var numeroembarque = $("#numeroembarque").val();
  var idtransporte = $("#idtransporte").val();

  $("#idembarque").val('');
  reloaddetalle();


  var vdataurl = UrlHelper.Action("JsonGetListarEmbarqueFluvial","Monitoreo", "Monitoreo") + "?numeroembarque=" + numeroembarque
  + "&idtransporte=" + idtransporte;

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
