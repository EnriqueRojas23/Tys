/*
archivo: seguridad.usuarios.listarusuarios
*/

var listarusuario_frmsearch = "#form";

var gridlistausuario = "#gridlistausuario";
var gridlistausuariopager = "#gridlistausuariopager";


$(document).ready(function () {

    $.jgrid.defaults.width = 780;
    $.jgrid.defaults.height = 320;
    $.jgrid.defaults.responsive = true;

    $.jgrid.defaults.responsive = true;
    $('#btnNuevo').click(function (event) { btnNuevo_onclick(this, event); });
    
    
    configurarGrilla();
    mostrarMensajeResultado();
  
});
function configurarChosenSelect() {

    var config = {
        '.chosen-select': {
            max_selected_options: 5,
            placeholder_text_single: "Seleccione Datos",
            no_results_text: 'Oops, no se encontró el dato!'
        }
    }

    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
}
function btnActualizar_onclick(obj, event) {
    var url = $(obj).data("url");
    var dataModelo = $('form').serialize();

    var tipoacceso = $('#Usr_str_tipoacceso').val();
    var usrred = $('#Usr_str_red').val();
    var nombre = $('#Usr_str_nombre').val();
    var apellido = $('#Usr_str_apellidos').val();
    var email = $('#Usr_str_email').val();


    validation($('#Usr_str_email'));
    validation($('#Usr_str_red'));
    validation($('#Usr_str_nombre'));
    validation($('#Usr_str_apellidos'));



    if (tipoacceso == "" || usrred == "" || nombre == "" || apellido == "" ) {
               
        swal({ title: "Error", text: "Debe completar todos los datos correctamente", type: "error", confirmButtonText: "Aceptar" });
        if(tipoacceso == 'EX')
        {
            if(email == ""|| validationEmail($('#Usr_str_email'))==false)
            {
               swal({ title: "Error", text: "Debe completar todos los datos correctamente", type: "error", confirmButtonText: "Aceptar" });
            }
        }
    }
    else {


        swal({
            title: "Actualizar Usuario",
            text: "¿Está seguro que desea actualizar este usuario?",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Actualizar',
            closeOnConfirm: false,
            closeOnCancel: true
        },

        function (isConfirm) {
            if (isConfirm) {
                $.ajax(
                        {
                            type: "POST",
                            async: true,
                            url: url,
                            data: dataModelo,
                            success: function (data) {
                                if (data.res == "3") {
                                    swal({ title: "Error!", text: "El usuario ya existe", type: "error", confirmButtonText: "Aceptar" });
                                }
                                else if (data.res == "1") {
                                    swal({ title: "Correcto", text: "Se actualizó correctamente", type: "success", confirmButtonText: "Aceptar" });
                                    $("#modalcontainer").modal("hide");
                                    reload();

                                }
                            },
                            error: function (request, status, error) {

                                swal({ title: "Error!", text: "Ocurrió un error al registrar", type: "error", confirmButtonText: "Aceptar" });
                            }
                        });
            }
        });
    }

}



function btnRegistrar_onclick(obj, event) {
    var url = $(obj).data("url");
    var dataModelo = $('form').serialize();

    var tipoacceso = $('#Usr_str_tipoacceso').val();
    var usrred = $('#Usr_str_red').val();
    var nombre = $('#Usr_str_nombre').val();
    var apellido = $('#Usr_str_apellidos').val();
    var email = $('#Usr_str_email').val();

    validation($('#Usr_str_email'));

    validation($('#Usr_str_red'));
    validation($('#Usr_str_nombre'));
    validation($('#Usr_str_apellidos'));
       

 if(tipoacceso == 'AD')
  {
             
  
    if (tipoacceso == "" || usrred == "" || nombre == "" || apellido == "" ) {
               
        swal({ title: "Error", text: "Debe completar todos los datos correctamente", type: "error", confirmButtonText: "Aceptar" });
    }
    else {


        swal({
            title: "Registro de Usuario",
            text: "¿Está seguro que desea registrar este usuario?",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Registrar',
            closeOnConfirm: false,
            closeOnCancel: true
        },

        function (isConfirm) {
            if (isConfirm) {
                $.ajax(
                        {
                            type: "POST",
                            async: true,
                            url: url,
                            data: dataModelo,
                            success: function (data) {
                                if (data.res == "3") {
                                    swal({ title: "Error!", text: "El usuario ya existe", type: "error", confirmButtonText: "Aceptar" });
                                }
                                else if(data.res =="2") {
                                    swal({ title: "Correcto", text: "Se registró correctamente, pero no se envió el correo de manera satisfactoria", type: "success", confirmButtonText: "Aceptar" });
                                    $("#modalcontainer").modal("hide");
                                    reload();

                                }
                                else if (data.res == "1") {
                                    swal({ title: "Correcto", text: "Se registró correctamente", type: "success", confirmButtonText: "Aceptar" });
                                    $("#modalcontainer").modal("hide");
                                    reload();

                                }
                            },
                            error: function (request, status, error) {
                           
                                swal({ title: "Error!", text: "Ocurrió un error al registrar", type: "error", confirmButtonText: "Aceptar" });
                            }
                        });
            }
        });
    }
}
    else 
    {

         if (tipoacceso == "" || usrred == "" || nombre == "" || apellido == "" || email == "" || validationEmail($('#Usr_str_email'))==false ) {
                       
                swal({ title: "Error", text: "Debe completar todos los datos correctamente", type: "error", confirmButtonText: "Aceptar" });
            }
            else {


                swal({
                    title: "Registro de Usuario",
                    text: "¿Está seguro que desea registrar este usuario?",
                    type: "warning",
                    showCancelButton: true,
                    cancelButtonText: "Cancelar",
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Registrar',
                    closeOnConfirm: false,
                    closeOnCancel: true
                },

                function (isConfirm) {
                    if (isConfirm) {
                        $.ajax(
                                {
                                    type: "POST",
                                    async: true,
                                    url: url,
                                    data: dataModelo,
                                    success: function (data) {
                                        if (data.res == "3") {
                                            swal({ title: "Error!", text: "El usuario ya existe", type: "error", confirmButtonText: "Aceptar" });
                                        }
                                        else if(data.res =="2") {
                                            swal({ title: "Correcto", text: "Se registró correctamente, pero no se envió el correo de manera satisfactoria", type: "success", confirmButtonText: "Aceptar" });
                                            $("#modalcontainer").modal("hide");
                                            reload();

                                        }
                                        else if (data.res == "1") {
                                            swal({ title: "Correcto", text: "Se registró correctamente", type: "success", confirmButtonText: "Aceptar" });
                                            $("#modalcontainer").modal("hide");
                                            reload();

                                        }
                                    },
                                    error: function (request, status, error) {
                                   
                                        swal({ title: "Error!", text: "Ocurrió un error al registrar", type: "error", confirmButtonText: "Aceptar" });
                                    }
                                });
                    }
                });


    }
}


}
function reload()
{
    var vdataurl = $(gridlistausuario).data("dataurl");
    $(gridlistausuario).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}


function btnNuevo_onclick(obj, event) {
    var url = $(obj).data("url");

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        configurarPopUpNuevo();
        configurarChosenSelect();
        $('#btnRegistrar').click(function (event) { btnRegistrar_onclick(this, event); });
    });
}
function modificarUsuario(obj, id) {
    var grilla = $(gridlistausuario);
    var url = grilla.data("edit") + "?id=" + id;
    //$(window).attr("location", vUrl);


    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        configurarPopUpEditar();
        $('#btnActualizar').click(function (event) { btnActualizar_onclick(this, event); });
    });
}
function mostrarMensajeResultado() {
    if ($("#hdfMensaje").val()) {
        var mensaje = $("#hdfMensaje").val();
        if ($('#hdfexito').val() == "1") {
            swal("¡Registro Correcto!", mensaje, "success");
        }
        else
        {
            swal({ title: "Verificar", text: "El usuario ya existe", type: "error", confirmButtonText: "Aceptar" });
        }
    }
}

function configurarPopUpNuevo() {
    $(function () {
        $('#frmUsuarioNuevo').submit(function () {
            event.preventDefault();
            swal({
                title: "Registro de Usuario",
                text: "¿Está seguro que desea registrar este usuario?",
                type: "warning",
                showCancelButton: true,
                cancelButtonText: "Cancelar",
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Registrar',
                closeOnConfirm: false,
                closeOnCancel: true
            },
               function () {
                   $('#frmUsuarioNuevo').submit();
               });

        });
    });
}
function configurarPopUpEditar() {
    $(function () {
        $('#frmUsuarioModificar').submit(function () {
            event.preventDefault();
            swal({
                title: "Actualizar Usuario",
                text: "¿Está seguro que desea actualizar este usuario?",
                type: "warning",
                showCancelButton: true,
                cancelButtonText: "Cancelar",
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Registrar',
                closeOnConfirm: false,
                closeOnCancel: true
            },
               function () {
                   $('#frmUsuarioModificar').submit();
               });

        });
    });
}

function configurarGrilla() {

    var nombre = $('#NombreCompleto').val()
    var rol = $('#IdRol').val();


    var grilla = $(gridlistausuario);
    var pagergrilla = $(gridlistausuariopager);
    var vdataurl = $(grilla).data("dataurl") + "?nom=" + nombre + "&rol=" + rol;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'POST',
//        Data : getDataForm(),
        colNames: ['','Usuario', 'Nombres', 'Apellidos', 'Correo Electronico',  'Tipo'  , 'Activo', 'Bloqueado','Último Ingreso','Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'usr_int_id', index: 'usr_int_id' },
            { key: false, hidden: false, name: 'usr_str_red', index: 'usr_str_red', width: '120', align: 'left' },
            { key: false, hidden: false, name: 'usr_str_nombre', index: 'usr_str_nombre', width: '140', align: 'left' },
            { key: false, hidden: false, name: 'usr_str_apellidos', index: 'usr_str_apellidos', width: '140', align: 'left' },
            { key: false, hidden: false, name: 'usr_str_email', index: 'usr_str_email', width: '200', align: 'left' },
            { key: false, hidden: false, name: 'usr_str_tipoacceso', index: 'usr_str_tipoacceso', width: '80', align: 'center' , formatter: formattipoacceso},
            { key: false, hidden: false, name: 'Activo', index: 'Activo', width: '100', align: 'center', formatter: formatedit },
            { key: false, hidden: false, name: 'usr_int_bloqueado', index: 'usr_int_bloqueado', width: '80', align: 'center', formatter: formateditb },
            { key: false, hidden: false, name: 'usr_dat_ultfeclogin', index: 'usr_dat_ultfeclogin', width: '120', align: 'center', formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'd/m/Y' } },
            { key: false, hidden: false, name: 'usr_int_id', index: 'usr_int_id', width: '180', align: 'center', formatter: bottonaAcciones_formatter }

        ],
        jsonReader: CONFIG_JQGRID.get('jsonReader'),
        pager: $(gridlistausuariopager),
        //rowNum: CONFIG_JQGRID.get('rownum'),
        //rowList: CONFIG_JQGRID.get('rowlist'),
        rowNum: 40,
        rowList: [50, 100, 150, 200],
        emptyrecords: CONFIG_JQGRID.get('emptyrecords'),
        autoheight: true,
        autowidth: true,
  
        //shrinkToFit: false,
        beforeRequest: function ()
        {
            var $self = $(this);
            var postData = $self.jqGrid('getGridParam', 'postData');
            $.each(postData, function (index, value) {
                if (value.name == "rows") {
                    postData[index].value = postData.rows;
                }
                if (value.name == "page") {
                    postData[index].value = postData.page;
                }
                if (value.name == "sord") {
                    postData[index].value = postData.sord;
                }
                if (value.name == "sidx") {
                    postData[index].value = postData.sidx;
                }
            })
            $self.jqGrid('setGridParam', { postData: postData });
        },
      
    });

}
function formattipoacceso (cellvalue, options, rowObject)
{
    if (cellvalue.trim() == "1")
    {
       return "Interno";
    }
    else if (cellvalue.trim() == "2")
    {
       return "Externo";
    }

  
}
function formateditb (cellvalue, options, rowObject)
{
    if (cellvalue == "0")
    {
       return "<i class='fa fa-square-o'></i>";
    }
    else if (cellvalue == "1")
    {
       return "<i class='fa fa-check-square-o'></i>";
    }

  
}
function formatedit (cellvalue, options, rowObject)
{
    if (cellvalue.trim() == "Activo")
    {
        return "<i class='fa fa-square-o'></i>";
        }   
       
    else if (cellvalue.trim() == "Eliminado")
    {
     
        return "<i class='fa fa-check-square-o'></i>"; 
    }
       
    

  
}
function getDataForm() {
    var form = $('form');
    return form.serializeArray();
}

function btnlistarbusquedasecundario_onclick(searchdefault) {
    var form = $.find("form[data-typeform=search]");
    if (form == null || form == undefined || form.length <= 0) return;

    var controles = $(form).find("[data-ctrlsearch]");
    if (controles == null || controles == undefined || controles.length <= 0) return;

    var strfiltro = "";
    $.each(controles, function () {
        var valor = $.trim($(this).val());
        var nombre = $(this).data("ctrlsearch");

        if (valor != "") {
            strfiltro = strfiltro + " {" + nombre + ":" + valor + "}";
        }
    })

    if (searchdefault != undefined) $(searchdefault).val(strfiltro);
}


function bottonaAcciones_formatter(cellvalue, options, rowObject)
{
    var acciones = $("<div class='btn-group'></div>");

    var control = $("<button></button>");
    control.append("<i class='fa fa-check-circle'></i>")
    control.attr("title", "Asignar Rol");
    control.addClass("btn btn-primary btn-xs btn-outline")
    control.attr("id", "lnk" + cellvalue);
    control.attr("onclick", "irAsignarRol('" + cellvalue + "')");

    var btnGridM = $("#templatemod").clone(false)[0];
    $(btnGridM).attr("id", "btngrid_mod_" + cellvalue);
    $(btnGridM).attr("onclick", "modificarUsuario(this, " + cellvalue + ")");
    $(btnGridM).show();

    //var btnGridR = $("#templatereset").clone(false)[0];
    //$(btnGridR).attr("id", "btngrid_reset_" + cellvalue);
    //$(btnGridR).attr("onclick", "resetearPwd(this, " + cellvalue + ")");
    //$(btnGridR).show();


    var btnGridT = $("#templatetrash").clone(false)[0];
    $(btnGridT).attr("id", "btngrid_trash_" + cellvalue);
    $(btnGridT).attr("onclick", "eliminarUsuario(this, " + cellvalue + ")");
    $(btnGridT).show();


    var btnGridTD= $("#templatedesbloquear").clone(false)[0];
    $(btnGridTD).attr("id", "btngrid_des_" + cellvalue);
    $(btnGridTD).attr("onclick", "desbloquearUsuario(this, " + cellvalue + ")");
    $(btnGridTD).show();


    //var btnGridAS= $("#templateasociar").clone(false)[0];
    //$(btnGridAS).attr("id", "btngrid_des_" + cellvalue);
    //$(btnGridAS).attr("onclick", "asociarcliente(this, " + cellvalue + ")");
    //$(btnGridAS).show();






    acciones.append(control);
    acciones.append(btnGridM.outerHTML);
    //acciones.append(btnGridR.outerHTML);
    acciones.append(btnGridT.outerHTML);
    acciones.append(btnGridTD.outerHTML);
    //acciones.append(btnGridAS.outerHTML);

    var htmlcontrol = acciones[0].outerHTML;
    return htmlcontrol
}
function asociarcliente(obj,id)
{
    //var vUrl = $(obj).data("url");
   

       var vurl = UrlHelper.Action("AsociarCliente", "Usuarios", "seguridad") + "?id=" + id // $("#formsubmit").attr("action");
        $.get(vurl, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        var dlbAccesorios = $('select[name="ClientesSeleccionados"]').bootstrapDualListbox({

            nonSelectedListLabel: 'Disponibles',
            selectedListLabel: 'Seleccionados',
            showFilterInputs: true,
            moveOnSelect: true,
        });

        $(formsubmit).on('submit', function (event) {
            // var ids = $(gridrolesasignados).jqGrid('getDataIDs');
            // $("#idsRolesDestino").val(ids);
        });
        //configurarPopUpNuevo();
        //$('#btnRegistrar').click(function (event) { btnRegistrar_onclick(this, event); });
    });




       alert(vUrl);
}

function configurarGrilla_RolesDisponibles()
{
    var grilla = $(gridrolesdisponibles);
    var vdataurl = $(grilla).data("dataurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'GET',
        colNames: ['', 'Nombre'],
        colModel:
        [
            { key: true, hidden: true, name: 'rol_int_id', index: 'rol_int_id' },
            { key: false, hidden: false, name: 'rol_str_alias', index: 'rol_str_alias', width: '120', align: 'left' },
        
        ],
        jsonReader: CONFIG_JQGRID.get('jsonReader'),
        rowNum: CONFIG_JQGRID.get('rownum'),
        rowList: CONFIG_JQGRID.get('rowlist'),
        emptyrecords: CONFIG_JQGRID.get('emptyrecords'),
        //pager: $(gridrolesdisponiblespager),
        autoheight: true,
        autowidth: true,
        shrinkToFit: false,
        multiselect: true

    });
}

function desbloquearUsuario(obj,id)
{
     var vUrl = $(obj).data("url");
    swal({
        title: "Desbloquear Usuario",
        text: "¿Está seguro que desea desbloquear este usuario?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Desbloquear',
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
                       if (data.success) {
                           swal("¡Se ha desbloqueado correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                            reload();

                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.error.toString());
                   }
               });
           }
     });
}
function eliminarUsuario(obj, id)
{
    var vUrl = $(obj).data("url");
    swal({
        title: "Eliminar Usuario",
        text: "¿Está seguro que desea eliminar este usuario?",
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
                       if (data.success) {
                           swal("¡Se ha activo correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                            reload();

                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.error.toString());
                   }
               });
           }
     });
}

function bottonmodificarusr_formatter(cellvalue, options, rowObject)
{
    var btnGrid = $("#templatemod").clone(false)[0];
    $(btnGrid).attr("id", "btngrid_mod_" + cellvalue);
    $(btnGrid).attr("onclick", "modificarUsuario(this, " + cellvalue + ")");
    $(btnGrid).show();
    return btnGrid.outerHTML;
}


function bottonrestablecerpwd_formatter(cellvalue, options, rowObject)
{
    var btnGrid = $("#templatereset").clone(false)[0];
    $(btnGrid).attr("id", "btngrid_reset_" + cellvalue);
    $(btnGrid).attr("onclick", "resetearPwd(this, " + cellvalue + ")");
    $(btnGrid).show();
    return btnGrid.outerHTML;
}



function resetearPwd(obj, id)
{
    var vUrl = $(obj).data("url");
    swal({
        title: "Generar Contraseña",
        text: "¿Está seguro que desea generar una nueva contraseña?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Generar',
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
                           swal("¡Se ha enviado un correo al usuario!", data.msj, "success");
                       } else {
                           swal({ title: "Error", text: "Ocurrio un error", type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.error.toString());
                   }
               });
           }
     });
}

function irAsignarRol(id)
{
    //var vUrl = $(gridlistausuario).data("urlasig") + "?id=" + id;
    //$(document).attr("location", vUrl);

    var url = $(gridlistausuario).data("urlasig") + "?id=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        configurarGrilla_RolesDisponibles();
        configurarGrilla_RolesAsignados();


        $(formsubmit).on('submit', function (event) {
            var ids = $(gridrolesasignados).jqGrid('getDataIDs');
            $("#idsRolesDestino").val(ids);
        });
        //configurarPopUpNuevo();
        //$('#btnRegistrar').click(function (event) { btnRegistrar_onclick(this, event); });
    });



}

function crearUsuario(obj)
{
    var vUrl = $(obj).data("url");
    $(window).attr("location", vUrl);
}

function buscarusuarios() {

    var nombre = $('#NombreCompleto').val()
    var rol = $('#IdRol').val();
   var grilla = $("#gridlistausuario");
   var vdataurl = $(grilla).data("dataurl") + "?nom=" + nombre + "&rol=" + rol;
   $(grilla).jqGrid('setGridParam', { url: vdataurl  }).trigger('reloadGrid');
   //$(grilla).jqGrid('setGridParam', { url: vdataurl, postData:getDataForm()  }).trigger('reloadGrid');
   reload();
    

   
}
function recargar()
{
    var grilla = $("#gridlistausuario");
    var dataModelo = $('form').serialize();
    var vdataurl = $(grilla).data("dataurl");
    $(grilla).jqGrid('setGridParam', { url: vdataurl, postData:getDataForm()  }).trigger('reloadGrid');
}
