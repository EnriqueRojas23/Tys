$(document).ready(function ()
{

     cargagrilla();

});

function cargagrilla()
{

     oPrecintoTable =
       $('.dataTables-tblPrecinto').DataTable({
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
                "sSearch" : ""
                 ,"sInfo":  ""
                ,"sLengthMenu":  "Paginación :  _MENU_"
                },



           "ajax": {
               "url": $('#tblPrecinto').data("url"),
               "data": function (d) {

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
                       "key": true, "title": "Id", "data": "idprecinto", "name": "idprecinto", visible: true, "autoWidth": true, "class": "text-center"

                   },
                   {
                       "title": "Precinto", "data": "precinto", "name": "precinto",visible: true, "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idprecinto", "Width": "10px", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminapercinto(" + data + ");' href='#' > <i class='fa fa-ban'></i>Eliminar</button></div>";

                        }
                   }

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Precintos', exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               { extend: 'pdf', title: 'Listado de Precintos' , exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },

           ]

       });

}
function eliminapercinto(item)
{

    var vUrl = UrlHelper.Action("EliminarPrecinto" , "Seguimiento" , "Seguimiento");
  swal({
      title: "Eliminar Precinto",
      text: "¿Está seguro que desea eliminar este precinto?",
      type: "warning",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: '#DD6B55',
      confirmButtonText: 'Eliminar',
      closeOnConfirm: true,
      closeOnCancel: true
  },
     function (isConfirm) {
         if (isConfirm) {
             $.ajax({

                 url: vUrl,
                 type: "post",
                 datatype: "json",
                 data: { "idprecinto": item },
                 success: function (data) {
                     if (data.res) {
                         swal("¡Se eliminó correctamente!", "El precinto se eliminó correctamente", "success");
                         $("#modalcontainer").modal("hide");
                          oPrecintoTable.draw();

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
function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true)
    {
       swal({
           title: "Registro Exitoso",
           text: "Se registró correctamente el dato.",
            type: "success"
        },
       function ()
       {
           $("#modalcontainer").modal("hide");
           oPrecintoTable.draw();
       });

    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}
