var btnNuevo = "#btnNuevo";

$(document).ready(function () {



inicializandoEventosModalDocumentos();

 $("#btnBuscar").button()
                   .click(function (e) {
              var grilla = $("#gridtarifa");
              var id =  $('#idcliente').val()

              var vdataurl =  UrlHelper.Action("JsonGetListarTarifas", "Seguimiento", "Seguimiento") + "?id=" + id;
              $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');



   });


 $("#addrow").click( function() {

              var id =  $('#idcliente').val()
              if(id=='')
              {
                 sweetAlert("Debe seleccionar un cliente", null, "error");
                 return;
              }
              else
              {

              $("#gridtarifa").jqGrid('addRowData',0,1,"first");
              $("#gridtarifa").editRow(0,true);
              }
  });

cargargrilla();

    $(window).bind('resize', function () {
         var width = $('.jqGrid_wrapper').width();
      //  alert('xD')
        // $("#gridtarifa").setGridWidth(width);

      setTimeout(function() {
       $("#gridtarifa").setGridWidth(width -10);
       
    }, 200);

     }).trigger('resize')
});
function inicializandoEventosModalDocumentos(id)
{

     var config = {
            '.chosen-select': {max_selected_options: 5 ,
                 allow_single_deselect: false ,
                 no_results_text: 'Oops, no se encontró el ubigeo!' }

        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

        
}
function reload()
{


               var grilla = $("#gridtarifa");
              var id =  $('#idcliente').val()

              var vdataurl =  UrlHelper.Action("JsonGetListarTarifas", "Seguimiento", "Seguimiento") + "?id=" + id;
              $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function formatedit (cellvalue, options, rowObject)
{
    if(cellvalue == null)
    return "";
  else
    return " "  + cellvalue ;

}
function cargargrilla()
{

    $.jgrid.defaults.width = 1800;
    $.jgrid.defaults.height = 1200;

    var grilla = $("#gridtarifa");
    var pagergrilla = $("#gridtarifapager");


   var id =  $('#idcliente').val()





    var vdataurl = $(grilla).data("dataurl")  + "?id=" + id ;
    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Depart.','Provincia', 'Distrito' ,'Zona','Depart.', 'Provincias','Distrito', 'Formula' ,'Mod. Transporte' ,'Cobrar Por' ,'Tip. Unidad','Moneda','Base','Min','Desde','Hasta','Precio','Adicional','Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idtarifa', index: 'idtarifa' ,classes:"grid-col"},

            { key: false, hidden: false, editable: true ,name: 'origen'
                    , index: 'origen', width: '100',editrules: { required: true}, align: 'left'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    //, editoptions: { dataUrl: fcnUrlControlGrid('departamento')}
                    , editoptions: {
                                  dataUrl: fcnUrlControlGrid('departamento'),
                                  dataInit: function (elem, i) {
                                          var v = $(elem).val();
                                           $(grilla).setColProp('origenprovincia', {
                                              editoptions:
                                              {

                                                  dataUrl: fcnUrlControlGrid('origenprovincia' , i.rowId)
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

                                                      var $select = $("select#" + rowId + "_origenprovincia", row[0]);
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
                                          }
                ]
              }
            },
              { key: false, hidden: false, editable: true ,name: 'origenprovincia'
                    , index: 'origenprovincia', width: '100', align: 'left'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: {
                                  dataUrl: fcnUrlControlGrid('origenprovincia'),
                                  dataInit: function (elem, i) {
                                          var v = $(elem).val();

                                           $(grilla).setColProp('origendistrito', {

                                              editoptions:
                                              {



                                                  dataUrl: fcnUrlControlGrid('origendistrito' , i.rowId)
                                              }
                                            });
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

                                                      var $select = $("select#" + rowId + "_origendistrito", row[0]);
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
                { key: false, hidden: false, editable: true ,name: 'origendistrito'
                    , index: 'origendistrito', width: '100', align: 'left'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('distrito')}
            },

            { key: false, hidden: false, editable: true ,name: 'zona'
                    , index: 'zona', width: '90', align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('zona')}
            },




            { key: false, hidden: false, editable: true ,name: 'departamento'
                    , index: 'departamento', width: '100', align: 'left'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: {
                                  dataUrl: fcnUrlControlGrid('departamento'),
                                  dataInit: function (elem,i) {
                                          var v = $(elem).val();
                                           $(grilla).setColProp('provincia', {
                                              editoptions:
                                              {

                                                  dataUrl: fcnUrlControlGrid('provincia' , i.rowId)
                                              }
                                            });

                                          //$(grilla).setColProp('direccion', { editoptions: { dataUrl: fcnUrlControlGrid('s') }});
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
                                          }
                ]
              }
            },
            { key: false, hidden: false, editable: true ,name: 'provincia'
                    , index: 'provincia', width: '100', align: 'left'
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
                    , index: 'distrito', width: '100', align: 'left'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('distrito')}
            },
              { key: false, hidden: false, editable: true ,name: 'formula'
                    , index: 'formula', width: '100',editrules: { required: true}, align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('formula')}
            },
            { key: false, hidden: false, editable: true ,name: 'tipotransporte'
                    , index: 'tipotransporte',editrules: { required: true}, width: '90', align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('tipotransporte')}

            },

            { key: false, hidden: false, editable: true ,name: 'conceptos'
                    , index: 'conceptos', width: '90', align: 'center'
                    , classes:"grid-col",formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('idcobrarpor') ,
                            dataInit: function(element) {
                                 $(element).css("width","140");

                                    }

                    ,  size:5 , multiple: true
                  }
            },



             { key: false, hidden: false, editable: true ,name: 'tipounidad'
                    , index: 'tipounidad', width: '60', align: 'center'
                    , classes:"grid-col",editrules: { required: true},formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('tipounidad')}
            },
             { key: false, hidden: false, editable: true ,name: 'moneda'
                    , index: 'moneda', width: '70', align: 'center'
                    , classes:"grid-col",editrules: { required: true},formatter: formatedit
                    , edittype: "select"
                    , editoptions: { dataUrl: fcnUrlControlGrid('moneda')}
            },
            { key: false, hidden: false, editable: true, editrules: {  required: true} ,  name: 'montobase', index: 'montobase', width: '50', align: 'center'
            ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  validateFloatKeyPress(this,e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }


          },
            { key: false, hidden: false, editable: true,  name: 'minimo', index: 'minimo', width: '50', align: 'center'

 ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  validateFloatKeyPress(this,e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }





          },
            { key: false, hidden: false, editable: true,  name: 'desde', index: 'desde', width: '50', align: 'center'

             ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  validateFloatKeyPress(this,e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }

          },
            { key: false, hidden: false, editable: true,  name: 'hasta', index: 'hasta', width: '50', align: 'center'
             ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  validateFloatKeyPress(this,e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }

          },
            { key: false, hidden: false, editable: true, editrules: { required: true}, name: 'precio', index: 'precio', width: '50', align: 'center'
             ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  validateFloatKeyPress(this,e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }

             },
                { key: false, hidden: false, editable: true, editrules: { required: false}, name: 'adicional', index: 'adicional', width: '50', align: 'center'
             ,editoptions:{
                                dataInit: function(element) {
                                 $(element).keypress(function(e){
                                           var resp =  validateFloatKeyPress(this,e);
                                           if(resp == false)
                                            return false;
                                          else return true;

                                    });
                                  }
                                }

             },

         { key: false, hidden: false, editable: false ,name: 'idtarifa', width:'140' , index: 'idtarifa' ,  formatter:  displayButtonsDirecciones,classes:"grid-col"}
        ],
        pager: $(pagergrilla),
        rowNum: 40,
        rowList: [40, 80, 160],
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable:true,

        autowidth:true, 
        shrinkToFit:false,
        forceFit:true,

        addParams: {
                      position: "last",
                      addRowParams: editOptionsNew
                   },
        editParams: editOptionsNew,
        editurl: vdataedit,
        
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
$(grilla).jqGrid('setGroupHeaders', {
  useColSpanStyle: false,
  groupHeaders:[
  {startColumnName: 'origen', numberOfColumns: 3, titleText: '<em>Origen</em>'},
  {startColumnName: 'departamento', numberOfColumns: 3, titleText: 'Destino'}
  ]
});

$(grilla).jqGrid('gridResize',{minWidth:350,maxWidth:800,minHeight:80, maxHeight:1250});

}
function displayButtonsDirecciones(cellvalue, options, rowObject)
{
        var duplicar = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irduplicar(' + cellvalue + ')"><i class="fa fa-copy"></i></button>';
        var editar = "<button type='button' title='Editar' class='btn btn-success btn-xs btn-outline' onclick=\"$('#gridtarifa').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridtarifa').saveRow('" + options.rowId + "') ;      reload();         \";><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridtarifa').restoreRow('" + options.rowId + "'); reload(); \"><i class='fa fa-times-circle'></i> </button>";

        return  duplicar + editar + guardar + control + restore;
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


function irEliminar(id)
{
   var url = UrlHelper.Action("EliminarTarifa", "Seguimiento", "Seguimiento");
    swal({
        title: "Eliminar Tarifa",
        text: "¿Está seguro que desea eliminar esta tarifa?",
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
                           var idcliente =  $("#idcliente").val();
                           var grilla = $("#gridtarifa");
                           var vdataurl = $(grilla).data("dataurl")  + "?id=" + idcliente ;
                            $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                       swal("¡Se ha eliminado correctamente!", data.msj, "success");

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
          }
     });

}

function irCopiar()
{

    var idcliente =  $("#idclientecopia").val();
    var idoriginal =  $("#idcliente").val();


   var url = UrlHelper.Action("CopiarTarifa", "Seguimiento", "Seguimiento");
    swal({
        title: "Copiar Tarifa",
        text: "¿Está seguro que desea copiar esta tarifa?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Copiar',
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
                   data: {  "id": idcliente, "idoriginal": idoriginal},
                   success: function (data) {

                           var grilla = $("#gridtarifa");
                           var vdataurl = $(grilla).data("dataurl")  + "?id=" + idcliente ;
                            $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                       swal("¡Se ha copiado correctamente!", data.msj, "success");

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

function irduplicar(id)
{
     var idcliente =  $("#idcliente").val();
     var url = UrlHelper.Action("CopiarTarifaIndividual", "Seguimiento", "Seguimiento");
     $.ajax(
                   {
                       type: "POST",
                       async: true,
                       url: url ,
                       data: {  "id": id},
                       success: function (data) {

                               var grilla = $("#gridtarifa");
                               var vdataurl = $(grilla).data("dataurl")  + "?id=" + idcliente ;
                                $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                           swal("¡Se ha duplicado correctamente!", data.msj, "success");

                       },
                       error: function (request, status, error)
                       {
                           swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                       }
                   });
 }
