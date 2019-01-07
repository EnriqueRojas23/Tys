var btnNuevo = "#btnNuevo";

$(document).ready(function () {



 $(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });

 $("#btnBuscar").button()
                   .click(function (e) {
                       oDocumentosTable.draw();
                   });

                   
    CargaListaTipoTransporte();
});

function CargaListaTipoTransporte() {


    
    oDocumentosTable =
       $('.dataTables-tblTipoTransporte').DataTable({
           responsive: true,
           dom: '<"html5buttons"B>lTfgitp',
           "processing": true,
           "serverSide": true,
           "ajax": {
               "url": $('#tblTipoTransporte').data("url"),
               "data": function (d) {
                   d.criterio = $('#tipotransporte').val();
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idtipotransporte", "name": "idtipotransporte", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   {
                       "title": "N°", "data": "idtipotransporte", "name": "idtipotransporte", "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "Tipo Transporte", "data": "tipotransporte", "name": "tipotransporte", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "activo", "data": "activo", "name": "activo", "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                       if(data==true)
                                         return "<span class='label label-danger'>" + "activo" + "</span>";
                                      else 
                                         return "<span class='label label-primary'>" + "Activo" + "</span>";
                                  }
                   },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idtipotransporte", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editartipotransporte(" + data + ");' href='#' > <i class='fa fa-edit'></i> Editar</button>" 
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='Eliminartipotransporte(" + data + ");' href='#' > <i class='fa fa-trash'></i> Eliminar</button></div>";
                         
                        }
                   },

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Tipos de Comprobantes' },
               { extend: 'pdf', title: 'Listado de Tipos de Comprobantes' },
               
           ]

       });
}
//4307022
function btnBuscarDocumento_onclick(obj, event) {

    
    var url = UrlHelper.Action("AgregarTipoTransporteModal", "Facturacion", "Facturacion")


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos();
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
           text: "Se registró correctamente el Tipo de Transporte.",
            type: "success"
        },
       function ()
       {
           $("#modalcontainer").modal("hide");
           //var vurl = $('#btnAsociar').data("urlbak");
           //window.location.href = vurl;
           oDocumentosTable.draw();
       });
     
    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}
function editartipotransporte(id)
{
    var url = UrlHelper.Action("EditarTipoTransporteModal", "Facturacion", "Facturacion") + "?id=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos(id);
    });
}
function inicializandoEventosModalDocumentos(id)
{
    $('#hdidtipocomprobante').val(id);




}

function Eliminartipotransporte(id)
{
    var vUrl = UrlHelper.Action("EliminarTipoTransporte", "Facturacion", "Facturacion") + "?id=" + id;
    


    swal({
        title: "Eliminar Tipo de Transporte",
        text: "¿Está seguro que desea eliminar este Tipo de Transporte?",
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
               $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {
                           swal("¡Se ha activo correctamente!", data.msj, "success");
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