

function tree_sinasignar(data)
{

  $('#jstree_di').jstree({
        plugins : [ "checkbox" ],
      'checkbox' : {
       "keep_selected_style" : false
     },
       core: {
           'data': data
       },
       width: 1100,
       height: 300
   }).on('loaded.jstree', function () {

     var tree = $("#jstree_di").jstree(),
     nodename1 = tree.get_node("#").children;
     //$("#jstree_di").jstree('open_node',nodename1);

   }).bind("refresh.jstree", function (e, data)
    {
      $("#jstree_di").jstree('close_all');

      var tree = $("#jstree_di").jstree(),
      nodename1 = tree.get_node("#").children;
    //  $("#jstree_di").jstree('open_node',nodename1);
       $(this).jstree("deselect_all");
   });



}



function traer_asignado(data)
{
  $('#jstree_nod').jstree({
      plugins : [ "checkbox" ],
    'checkbox' : {
     "keep_selected_style" : false
     },
       core: {
           'data': data
       },
       width: 1100,
       height: 300
    }).on('loaded.jstree', function () {

      var tree1 = $("#jstree_nod").jstree(),
       nodename = tree1.get_node("#").children;
      // $("#jstree_nod").jstree('open_node',nodename);

   }).bind("refresh.jstree", function (e, data)
       {
        $("#jstree_nod").jstree('close_all');

         var tree1 = $("#jstree_nod").jstree(),
          nodename = tree1.get_node("#").children;
         //
        //  $("#jstree_nod").jstree('open_node',nodename);
          $(this).jstree("deselect_all");
       });

}


function configurarMonitoreo(data) {
                 var levels = new Array();

                  $("div#jstree").bind("loaded_grid.jstree", function () {
                      $("span#status").text("loaded");
                  }).on("select_cell.jstree-grid", function (e, data) {
                      $("span#clicked").html("clicked " + data.column + " of value " + data.value);
                  }).on('update_cell.jstree-grid', function (e, data) {
                      $("span#changed").html("changed " + data.col + " from " + data.old + " to " + data.value);
                  });


                  $("div#jstree").jstree({
                      plugins: ["wholerow","checkbox", "themes", "json", "grid", "dnd", "contextmenu", "search", "sort"],
                      "checkbox": {
                          real_checkboxes: true,
                          real_checkboxes_names: function (n) {
                              var nid = 0;
                              $(n).each(function (data) {
                                  nid = $(this).attr("nodeid");
                              });
                              return (["check_" + nid, nid]);
                          },
                          two_state: true
                      },
                      core: {
                            data: data
                            , 'themes': {
                            'responsive': true
                          }
                      },
                      grid: {
                          columns: [
                              { width: 200, header: "OT", title: "_DATA_" },

                              { width: 75, cellClass: "col1", value: "fechadespacho", header: "Fecha OT", title: "fechadespacho", valueClass: "spanclass" },
                              { width: 110, cellClass: "col2", value: "tipooperacion", header: "Tip. Operación", title: "tipooperacion", valueClass: "spanclass" },
                              { width: 60, cellClass: "col3", value: "bultos", header: "Bultos", title: "bultos", valueClass: "spanclass" },
                              { width: 60, cellClass: "col4", value: "peso", header: "Peso", title: "peso", valueClass: "spanclass" },
                              { width: 60, cellClass: "col5", value: "volumen", header: "Vol", title: "volumen", valueClass: "spanclass" },
                              { width: 110, cellClass: "col6", value: "origen", header: "Origen", title: "origen", valueClass: "spanclass" },
                              { width: 70, cellClass: "col7", value: "tienda", header: "Tienda", title: "Tienda", valueClass: "spanclass" },
                              { width: 70, cellClass: "col8", value: "modalidad", header: "Modalidad", title: "modalidad", valueClass: "spanclass" },
                              { width: 110, cellClass: "col9", value: "remitente", header: "Remitente", title: "remitente", valueClass: "spanclass" },
                              { width: 110, cellClass: "col10", value: "destinatario", header: "Destinatario", title: "destinatario", valueClass: "spanclass" },
                              { width: 110, cellClass: "col11", value: "direcciondestino", header: "Dir. Destino", title: "direcciondestino", valueClass: "spanclass" },
                              { width: 110, cellClass: "col12", value: "ultimaetapa", header: "Ult. Etapa", title: "ultimaetapa", valueClass: "spanclass" },
                              { width: 110, cellClass: "col13", value: "ultimorecurso", header: "Ult. Recurso", title: "ultimorecurso", valueClass: "spanclass" },
                              { width: 110, cellClass: "col14", value: "ultimodocumento", header: "Ult. Documento", title: "ultimodocumento", valueClass: "spanclass" },
                              { width: 110, cellClass: "col15", value: "grr", header: "GRR", title: "grr", valueClass: "spanclass" },
                              { width: 110, cellClass: "col16", value: "incidencia", header: "Incidencia", title: "Incidencia", valueClass: "spanclass" },
                              { width: 110, cellClass: "col17", value: "reintegro", header: "Reintegro", title: "Reintegro", valueClass: "spanclass" },
                              { width: 110, cellClass: "col18", value: "estacionorigen", header: "Estación Origen", title: "Estación Origen", valueClass: "spanclass" },
                              { width: 25, cellClass: "col19", value: "idordentrabajo", header: "Id", title: "idordentrabajo", valueClass: "spanclass" }

                          ],
                          resizable: true,
                          draggable: true,
                          contextmenu: true,
                          gridcontextmenu: function (grid, tree, node, val, col, t, target) {
                              return {
                                  "edit": {
                                      label: "Change value",
                                      icon: "glyphicon glyphicon-pencil",
                                      "action": function (data) {
                                          var obj = t.get_node(node);
                                          grid._edit(obj, col, target);
                                      }
                                  }
                              }
                          },
                          width: 1100,
                          height: 480
                      },
                      dnd: {
                          drop_finish: function () {
                          },
                          drag_finish: function () {
                          },
                          drag_check: function (data) {
                              return {
                                  after: true,
                                  before: true,
                                  inside: true
                              };
                          }
                      }
                  }).on('loaded.jstree', function () {


                    var tree = $("div#jstree").jstree(),
                    nodename = tree.get_node("#").children;

                    //$("div#jstree").jstree('open_node',nodename);

                  })
                      .bind('open_node.jstree', function (e, data) {
                          $('#jstree').jstree('check_node', 'li[selected=selected]');



                      })
                  $("a#change").click(function () {
                      var tree = $("div#jstree").jstree(),
                          nodename = tree.get_node("#").children[0], node = tree.get_node(nodename),
                          val = parseInt(node.data.size);

                      node.data.size = node.data.size * 2;
                      tree.trigger("change_node.jstree", nodename);

                      return (false);
                  });

                  // $("a#refresh").click(function () {
                  //     var tree = $("div#jstree").jstree();
                  //     var data = [{
                  //         text: "Root 1",
                  //         data: { price: "$5.00", size: "4" }
                  //     }];
                  //     tree.settings.core.data = data;
                  //     tree.refresh();
                  //     return (false);
                  // });

                  $("input#search").keyup(function (e) {
                      var tree = $("div#jstree").jstree();
                      tree.search($(this).val());
                  });

              }

function reloadgridguias_light()
{
    var grilla = $("#gridguias");
    var vdataurl = UrlHelper.Action("JsonGetListarGuias","Monitoreo","Monitoreo");
     $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function configurargrillaguias_light()
{
  $.jgrid.defaults.width = 400;
  $.jgrid.defaults.height = 150;

  var grilla = $("#gridguias");
  var pagergrilla = $("#gridguiaspager");
  var vdataurl = UrlHelper.Action("JsonGetListarGuias", "Monitoreo", "Monitoreo");

  $(grilla).jqGrid({
      url: vdataurl,
      responsive:true,
      datatype: 'json',
      mtype: 'Get',
      colNames: ['','GRR','Cantidad','Acciones'],
      colModel:
      [
          { key: true, hidden: true, name: 'idguia', index: 'idguiaremisioncliente' },
          { key: false, hidden: false, editable: true , name: 'guia', index: 'nroguia', width: '100', align: 'left' ,formatter: formatedit },
          { key: false, hidden: false, editable: true , name: 'cantidad', index: 'nroguia', width: '100', align: 'left' ,formatter: formatedit},
          { key: true, hidden: false, name: 'idguia', index: 'idguia' ,width: '60',formatter:  displayButtons2,align: 'center' }
      ],
      jsonReader: CONFIG_JQGRID.get('jsonReader'),
      pager: $(pagergrilla),
      rowNum: 10,
      rowList: [10, 20],
      emptyrecords: 'No se encontraron registros',
      autoheight: true,
      responsive : true,
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
function displayButtons2 (cellvalue, options, rowObject)
{
  return "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='eliminargrr(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i> </button>";
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
function cargargrilladetalle(id)
{

      $.jgrid.defaults.height = 220;
      $.jgrid.defaults.width = 500;
      $.jgrid.defaults.responsive = true;
      var grilla = $("#griddetalle");
      var pagergrilla = $("#griddetallepager");

      var idorden = $("#idoperacion").val();


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
              { key: false, hidden: false, editable: false ,name: 'numcp', index: 'numcp', width: '30', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'fechaincidencia', index: 'fechaincidencia', width: '40', align: 'center',classes:"grid-col" ,formatter: 'date'  , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'usuario', index: 'usuario', width: '40', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'incidencia', index: 'incidencia', width: '60', align: 'center',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
              { key: false, hidden: false, editable: false ,name: 'descripcion', index: 'descripcion', width: '130', align: 'left',classes:"grid-col" ,formatter: formatedit , classes: "grid-col" },
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
function eliminargrr(id)
{


 var url =  UrlHelper.Action ("JsonEliminarGuia","Monitoreo","Monitoreo");
 $.ajax({
   url: url,
   type: 'POST',
   dataType: 'json',
   data: {'idguia': id  }
 })
 .done(function(data) {
     if(data.res)
     {
        reloadgridguias_light();
     }
     else {
       messagebox("No se puede eliminar",data.msj,"warning");

       }

 })
 .fail(function() {
   console.log("error");
 })
}

function reload()
{
  var numeroembarque = $("#numeroembarque").val();
  var idtransporte = $("#idtransporte").val();


  var vdataurl = UrlHelper.Action("JsonGetListarEmbarqueFluvial","Monitoreo", "Monitoreo") + "?numeroembarque=" + numeroembarque
  + "&idtransporte=" + idtransporte;

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
