var btnNuevo = "#btnNuevo";

$(document).ready(function () {



 $(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });

 $("#btnBuscar").button()
                   .click(function (e) {
                       oDocumentosTable.draw();
                   });

                   
    CargaListaTipoComprobante();
});

function CargaListaTipoComprobante() {


    
    oDocumentosTable =
       $('.dataTables-tblTipoComprobante').DataTable({
           responsive: true,
           dom: '<"html5buttons"B>lTfgitp',
           "processing": true,
           "serverSide": true,
           "ajax": {
               "url": $('#tblTipoComprobante').data("url"),
               "data": function (d) {
                   d.criterio = $('#tipocomprobante').val();
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idtipocomprobante", "name": "idtipocomprobante", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   {
                       "title": "N°", "data": "idtipocomprobante", "name": "idtipocomprobante", "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "Código", "data": "codigo", "name": "codigo", "autoWidth": true, "class": "text-center" },
                   { "title": "Tipo Comprobante", "data": "tipocomprobante", "name": "tipocomprobante", "autoWidth": true, "class": "text-center" },
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
                       "title": "Acciones", "class": "text-center", "data": "idtipocomprobante", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarproveedor(" + data + ");' href='#' > <i class='fa fa-edit'></i> Editar</button>" 
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='EliminarTipoComprobante(" + data + ");' href='#' > <i class='fa fa-trash'></i> Eliminar</button></div>";
                         
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

function btnBuscarDocumento_onclick(obj, event) {

    
    var url = UrlHelper.Action("AgregarTipoComprobanteModal", "Facturacion", "Facturacion")


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
           text: "Se registró correctamente el Tipo de Comprobante.",
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
function editarproveedor(id)
{
    var url = UrlHelper.Action("EditarTipoComprobanteModal", "Facturacion", "Facturacion") + "?id=" + id;


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

function EliminarTipoComprobante(id)
{
    var vUrl = UrlHelper.Action("EliminarTipoComprobante", "Facturacion", "Facturacion") + "?id=" + id;
    


    swal({
        title: "Eliminar Tipo de Comprobante",
        text: "¿Está seguro que desea eliminar este Tipo de Comprobante?",
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