var btnNuevo = "#btnNuevo";

$(document).ready(function () {
 $("#btnConfirmar").click( function (e)
 {
        var items_seleccionados = oOperacionesTable.column(0).checkboxes.selected();
        if(items_seleccionados.length > 1)
        {
            swal({
                       title: "Seleccione solo un elemento",
                       text: "Esta opción solo corresponde a un elemento seleccionado.",
                        type: "success"
            },
           function ()
           {
           });
          return ;

        }
         $.each(items_seleccionados, function (index, rowId) {
          // confirmar(rowId);
        });
 });

 $("#btnVerDetalle").click( function (e)
 {

    var items_seleccionados = oOperacionesTable.column(0).checkboxes.selected();
        if(items_seleccionados.length > 1)
        {
            swal({
                       title: "Seleccione solo un elemento",
                       text: "Esta opción solo corresponde a un elemento seleccionado.",
                        type: "success"
            },
           function ()
           {
           });
          return ;

        }
         $.each(items_seleccionados, function (index, rowId) {
           verdetallepopup(rowId);
        });
 });


 $("#btnAsignarServicio").click( function (e)
 {

    var items_seleccionados = oOperacionesTable.column(0).checkboxes.selected();
        if(items_seleccionados.length > 1)
        {
            swal({
                       title: "Seleccione solo un elemento",
                       text: "Esta opción solo corresponde a un elemento seleccionado.",
                        type: "success"
            },
           function ()
           {
           });
          return ;

        }

         $.each(items_seleccionados, function (index, rowId) {
           
         //  AsignarServicios(rowId);
         
        });


 });


 $("#btnAsignarVehiculo").click( function (e)
 { 
   var items_seleccionados = oOperacionesTable.column(0).checkboxes.selected();
    if(items_seleccionados.length > 1)
    {
        swal({
                   title: "Seleccione solo un elemento",
                   text: "Esta opción solo corresponde a un elemento seleccionado.",
                    type: "success"
        },
       function ()
       {
       });
      return ;

    }

     $.each(items_seleccionados, function (index, rowId) {
       
      // AsignarVehiculo(rowId);
     
    });

 

 });

     $("#btnBuscar").button()
                       .click(function (e) {
                 reload1();
       });


     cargargrilla();
    cargargrilladetalle(null);
     

        



});

function cargargrilla()
{

  
    
    oOperacionesTable =
       $('.dataTables-tblCargas').DataTable({
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
                ,"sLengthMenu":  "Paginación :  _MENU_"
                },



           "ajax": {
               "url": $('#tblCargas').data("url"),
               "data": function (d) {
                   d.numcp = $('#numcp').val();
                   d.numcarga = $('#numcarga').val();
               },
               "type": "POST",
               "datatype": "json"
           },
             'columnDefs': [
             {
                 'targets': 0,
                 'checkboxes': { 'selectRow': true }
             }],
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idcarga", "name": "idcarga", visible: true, "autoWidth": true, "class": "text-center"
                  
                   },
                   {
                       "title": "Item", "data": "idcarga", "name": "idcarga",visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "N° Manifiesto", "data": "numcarga", "name": "numcarga", "autoWidth": true, "class": "text-center" },
                   { "title": "F. Registro","format" :   'dd-MM-YYYY', "data": "fechacreacion", "name": "fechacreacion", "autoWidth": true, "class": "text-center" },
                   { "title": "Vol m3", "data": "vol", "name": "vol", "autoWidth": true, "class": "text-center" },
                   { "title": "Peso Kg", "data": "peso", "name": "peso", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idcarga", "Width": "10px", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='verdetalle(" + data + ");' href='#' > <i class='fa fa-magic'></i> Ver Detalle</button>" 
                           + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='anularoperacion(" + data + ");' href='#' > <i class='fa fa-ban'></i>Anular</button></div>";
                         
                        }
                   },

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Comprobantes', exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               { extend: 'pdf', title: 'Listado de Comprobantes' , exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               
           ]

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
           text: "Se registró correctamente el cambio.",
            type: "success"
        },
       function ()
       {
           $("#modalcontainer").modal("hide");
           oOperacionesTable.draw();
       });
     
    }
    else
    {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }

}

function planificarcarga()
{

         var url = UrlHelper.Action("PlanificarCarga", "Seguimiento", "Seguimiento");
         window.location.href = url;

}

function verdetalle(id)
{

  $("#idcarga").val(id);
  oOperacionesDetalleTable.draw();
}

function cargargrilladetalle(id)
{

    


    oOperacionesDetalleTable =
       $('.dataTables-tblDetalle').DataTable({
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
                ,"sLengthMenu":  "Paginación :  _MENU_"
                },



           "ajax": {
               "url": $('#tblDetalle').data("url"),
               "data": function (d) {
                   d.idcarga = $("#idcarga").val();
               },
               "type": "POST",
               "datatype": "json"
           },
             'columnDefs': [
             {
                 'targets': 0,
                 'checkboxes': { 'selectRow': true }
             }],
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idordentrabajo", "name": "idordentrabajo", visible: true, "autoWidth": true, "class": "text-center"
                  
                   },
                   {
                       "title": "Item", "data": "idordentrabajo", "name": "idordentrabajo",visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "N° Operación", "data": "numcp", "name": "numcp", "autoWidth": true, "class": "text-center" },
                   { "title": "Origen", "data": "origen", "name": "origen", "autoWidth": true, "class": "text-center" },
                   { "title": "Destino", "data": "destino", "name": "destino", "autoWidth": true, "class": "text-center" },
                   { "title": "Remitente", "data": "remitente", "name": "remitente", "autoWidth": true, "class": "text-center" },
                   { "title": "Destinatario", "data": "destinatario", "name": "destinatario", "autoWidth": true, "class": "text-center" },
                   { "title": "Bulto", "data": "bulto", "name": "bulto", "autoWidth": true, "class": "text-center" },
                   { "title": "Peso", "data": "peso", "name": "peso", "autoWidth": true, "class": "text-center" },
                   { "title": "Volumen", "data": "volumen", "name": "volumen", "autoWidth": true, "class": "text-center" }
                  
           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Comprobantes', exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               { extend: 'pdf', title: 'Listado de Comprobantes' , exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               
           ]

       });
}
function anularoperacion(id)
{

   var vUrl = UrlHelper.Action("AnularOperacion", "Seguimiento", "Seguimiento") + "?idcarga=" + id;
    swal({
        title: "Anular Operación",
        text: "¿Está seguro que desea anular esta operación?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Anular',
        closeOnConfirm: false,
        closeOnCancel: true
    },
       function (isConfirm) {
           if (isConfirm) {
               $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {
                            swal("¡Se ha anulado correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                            oOperacionesTable.draw();

                        $("#idcarga").val('');
                        oOperacionesDetalleTable.draw();

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
function AsignarVehiculo(id)
{

    var url = UrlHelper.Action("AsignarVehiculo", "Seguimiento", "Seguimiento") + "?idcarga=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $("#idvehiculo").change(function() {
        
        var idvehiculo = $("#idvehiculo").val();
        var urlinit = UrlHelper.Action ("ObtenerDatosVehiculo","Seguimiento","Seguimiento");
        
          $.ajax(
                { 
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: { "idvehiculo": idvehiculo },
                 success: function (data) {
                           $("#proveedor").html(data.proveedor); 
                           $("#chofer").html(data.nombrechofer);         

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });



        });
        


    });
}

function AsignarServicios(id)
{

   var url = UrlHelper.Action("AsignarServicios", "Seguimiento", "Seguimiento") + "?idcarga=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");


        

        configurarGrilla(id);
        $("#addrow").click( function() {
                $("#gridservicios").jqGrid('addRowData',0,1,"first");
                $("#gridservicios").editRow(0,true,null);
          });


    });

}
function configurarGrilla(id) {

    
    $.jgrid.defaults.height = 220;
     $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#gridservicios");
    var pagergrilla = $("#gridserviciospager");



    var vdataurl = $(grilla).data("dataurl") + "?idcarga=" + id;
    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Servicio','Cantidad', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idserviciooperacion', index: 'idserviciooperacion' ,classes:"grid-col"},
            { key: false, hidden: false, editable: true ,name: 'servicio', index: 'servicio', width: '60', align: 'center',classes:"grid-col" ,formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idservicio') }, classes: "grid-col" },
            { key: false, hidden: false, editable: true ,name: 'cantidad', index: 'cantidad', width: '30', align: 'center',classes:"grid-col",

             editoptions : {
               aftersavefunc: function (id) {  },
                keys: true,
              }
          },
            { key: false, hidden: false, editable: false ,name: 'idservicio', width:'30' , index: 'idservicio' ,  formatter:  displayButtons,classes:"grid-col"}   
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
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

function formatedit (cellvalue, options, rowObject)
{

    return " "  + cellvalue ;
  
}
function displayButtons(cellvalue, options, rowObject)
    {
        var editar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridasignacion').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId  + "', '' );\"><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridasignacion').restoreRow('" + options.rowId + "'); reload();\"><i class='fa fa-times-circle'></i> </button>"; 

        return editar + guardar + control + restore;
    }

var editOptionsNew = {
        keys: true,
        successfunc: function () {
          alert('xD');
            var $self = $(this);
            setTimeout(function () {
                $self.trigger("reloadGrid");
            }, 50);
        }
    };

function rowSave(id,str)
{ 
  $("#gridservicios").jqGrid('saveRow',id);

}
function verdetallepopup(id)
{
    var url = UrlHelper.Action("VerDetalleOperacion", "Seguimiento", "Seguimiento") + "?idcarga=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal("show");


        

        configurarGrillaP(id);
        $("#addrow").click( function() {
                $("#gridservicios").jqGrid('addRowData',0,1,"first");
                $("#gridservicios").editRow(0,true,null);
          });


    });
}
function configurarGrillaP(id)
{

    


    oOperacionesDetalleTable =
       $('.dataTables-tblDetalleCarga').DataTable({
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
                ,"sLengthMenu":  "Paginación :  _MENU_"
                },



           "ajax": {
               "url": $('#tblDetalleCarga').data("url"),
               "data": function (d) {
                   d.idcarga = id;
               },
               "type": "POST",
               "datatype": "json"
           },
           
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idordentrabajo", "name": "idordentrabajo", visible: true, "autoWidth": true, "class": "text-center"
                  
                   },
                   {
                       "title": "Item", "data": "idordentrabajo", "name": "idordentrabajo",visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "N° Operación", "data": "numcp", "name": "numcp", "autoWidth": true, "class": "text-center" },
                   { "title": "Origen", "data": "origen", "name": "origen", "autoWidth": true, "class": "text-center" },
                   { "title": "Destino", "data": "destino", "name": "destino", "autoWidth": true, "class": "text-center" },
                   { "title": "Remitente", "data": "remitente", "name": "remitente", "autoWidth": true, "class": "text-center" },
                   { "title": "Destinatario", "data": "destinatario", "name": "destinatario", "autoWidth": true, "class": "text-center" },
                   { "title": "Bulto", "data": "bulto", "name": "bulto", "autoWidth": true, "class": "text-center" },
                   { "title": "Peso", "data": "peso", "name": "peso", "autoWidth": true, "class": "text-center" },
                   { "title": "Volumen", "data": "volumen", "name": "volumen", "autoWidth": true, "class": "text-center" }
                  
           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Comprobantes', exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               { extend: 'pdf', title: 'Listado de Comprobantes' , exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               
           ]

       });



}
function confirmar(id)
{

  var url = UrlHelper.Action("ConfirmarCarga", "Seguimiento", "Seguimiento") + "?idcarga=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

     $('#hora').mask('00:00:00');
     

  $('#dtpfechacomp .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'

    });


    
    });

}