
var rowsToColor = [];
$(document).ready(function () {

    
      configurarGrilla();
      configurarBotones();
      configurarChosenSelect();
      
});
function configurarGrilla()
{
   
    //$.jgrid.defaults.width = 2000;
    $.jgrid.defaults.height = 400;

    var iddestino = $("#iddestino").val();
    var manifiesto = $("#manifiesto").val();
    var numcp = $("#numcp").val();
    var idestado = $("#idestado").val()
 

    var grilla = $("#gridots");
    var pagergrilla = $("#gridotspager");


    var vdataurl = UrlHelper.Action("JsonListarOTsEntrega", "Monitoreo", "Monitoreo") 
    + "?iddestino=" + iddestino
    + "&nummanifiesto=" + manifiesto
    + "&numcp=" + numcp
    + "&idestado=" + idestado

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Post',
        colNames: ['', '', 'Manifiesto','OT', 'F. Emisión', 'Entrega','F. Entrega', 'H. Entrega' ,'F. Despacho', 'H. Despacho' , 'Remitente', 'Destinatario','Tipo Operación', 'Bultos', 'Peso', 'Volumen', 'Cantidad'],
        colModel:
        [
            
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo' },
            { key: false, hidden: true, name: 'idmaestroetapa', index: 'idmaestroetapa', width: '10', formatter: rowColorFormatter },
            { key: false, hidden: false, editable: true, name: 'nummanifiesto', index: 'nummanifiesto', width: '70', align: 'center', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'numcp', index: 'numcp', width: '70', align: 'center', formatter: formatedit },
            { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fechadespacho', width: '90', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: true, name: 'entrega', index: 'entrega', width: '160', align: 'center', formatter: displayButtons },
            { key: false, hidden: false, editable: false, name: 'fechaentrega', index: 'fechadespacho', width: '90', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: true, name: 'horaentrega', index: 'horaentrega', width: '60', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: false, name: 'fechadespacho', index: 'fechadespacho', width: '90', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: true, name: 'horadespacho', index: 'horadespacho', width: '60', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'remitente', index: 'remitente', width: '90', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'destinatario', index: 'destinatario', width: '90', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'tipooperacion', index: 'tipooperacion', width: '90', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'bultos', index: 'bultos', width: '40', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'peso', index: 'peso', width: '40', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'volumen', index: 'volumen', width: '40', align: 'left', formatter: formatedit },
            { key: false, hidden: false, editable: true, name: 'cantidad', index: 'nroguia', width: '40', align: 'left', formatter: formatedit }
            
        ],
        pager: $(pagergrilla),
        rowNum: 100,
        rowList: [100, 200, 300 , 400 ],
        emptyrecords: 'No se encontraron registros',
        responsive: true,
        viewrecords: true,
        multiselect: true,
        editable: false,
        shrinkToFit: false,
        forceFit: true,
        autowidth:true,
        gridview: true,
        autoencode: true,
        caption: "Monitoreo de Ordenes",
        height: '100%',
        width: 935,
        rownumWidth: 40 ,




         gridComplete: function () {
                for (var i = 0; i < rowsToColor.length; i++) {
                    var status = $("#" + rowsToColor[i]).find("td").eq(2).html();
                    if (status != "5") {
                        $("#" + rowsToColor[i]).find("td").eq(4).css("background-color", "#ee3425");
                        $("#" + rowsToColor[i]).find("td").eq(4).css("color", "white");
                    }
                    else {
                        $("#" + rowsToColor[i]).find("td").eq(4).css("background-color", "#048d06");
                        $("#" + rowsToColor[i]).find("td").eq(4).css("color", "white");
                    }

                }
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
function rowColorFormatter(cellValue, options, rowObject) {
    if (cellValue == "5" ||
        cellValue == "6" ||
        cellValue == "7" || 
        cellValue == "8" || 
        cellValue == "9" || 
        cellValue == "10" || 
        cellValue == "11" || 
        cellValue == "12" || 
        cellValue == "13" 

        )
        rowsToColor[rowsToColor.length] = options.rowId;
    return cellValue;
}
function displayButtons(cellvalue, options, rowObject) {
    if (rowObject.idmaestroetapa == ''){
        return "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs' onclick='entregar(" + rowObject.idordentrabajo + ");' href='#' > Entregar </button>";
    }
    else {
        return cellvalue
    }
}
function configurarChosenSelect()
{

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


function configurarBotones()
{
    $('#btnBuscar').click(function () {
        reload()    
    });
    $('#btnIncidentes').click(function () {
        var item = $('#gridots').jqGrid('getGridParam', 'selarrrow');
        agregarincidentes(item);
    });

}
function agregarincidentes(id) {
    var vUrl = UrlHelper.Action("JsonAgregarIncidentes", "Monitoreo", "Monitoreo") + "?idsorden=" + id;
    let URL_ObtenerIncidencia = UrlHelper.Action("JsonObtenerMaestroIncidencia","Monitoreo","Monitoreo")
    $.get(vUrl, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");


        $("#idmaestroincidencia").change(function () {
        	$.ajax({
        		url: URL_ObtenerIncidencia,
        		type: 'POST',
        		dataType: 'json',
        		data: {idmaestroincidencia:  this.value , tipoincidencia : "M" },
        	})
        	.done(function(response) {
        		if(!response.esfecha)
        		{
        			$("#divFecha").css({
        				visibility : 'hidden',
        			});
        		}
        		else
        		{		$("#divFecha").css({
        				visibility: "visible",
        			});}

        	})
        	.fail(function() {
        		console.log("error");
        	})
        	.always(function() {
        		console.log("complete");
        	});
        	
            
        })

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
function AutoCompleteTextBox() {
    var idcliente = $("#idcliente").val();
    if (idcliente == "") {
        return false;
    }

    var url = UrlHelper.Action("JsonAutocomplete", "Monitoreo", "Monitoreo");
    $.ajax({
        type: "POST",
        async: true,
        url: url,
        dataType: 'json',
        data: { "idcampo": "1", "idcliente": idcliente },
        success: function (data) {
            var options = '';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i] + '" />';
            }
            $("#helps").html(options);
        },
        error: function (request, status, error) {
            swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
        }
    });
}
function reload() {
    var iddestino = $("#iddestino").val();
    var manifiesto = $("#manifiesto").val();
    var numcp = $("#numcp").val();
    var idestado = $("#idestado").val()


    var grilla = $("#gridots");

    var vdataurl = UrlHelper.Action("JsonListarOTsEntrega", "Monitoreo", "Monitoreo")
    + "?iddestino=" + iddestino
    + "&nummanifiesto=" + manifiesto
    + "&numcp=" + numcp
    + "&idestado=" + idestado

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

}
function entregar(id)
{
    let vurl = UrlHelper.Action("ModalEntregaCliente", "Monitoreo", "Monitoreo") + "?idordentrabajo=" + id;


    $.get(vurl, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal("show");

        $("#idmaestroetapa").change(function (event) {
            var idetapa = $("#idmaestroetapa").val();
            if (idetapa == 5 || idetapa == 8 || idetapa == 9 || idetapa == 10) {
                deshabilitarguias();
            }
            else {
                habilitarguias();

            }
        });

        $('#horaetapa').mask('00:00');

        $('#EntregaCliente-form').validate({ // initialize the plugin
            rules: {
                idmaestroetapa: {
                    required: true,
                },
                fechaetapa: {
                    required: true,
                },
                horaetapa: {
                    required: true,
                    minlength: 5
                },
                documento: { required: true },
                recurso: { required: true }
            },
            messages: {
                idmaestroetapa: {
                    required: "Debe seleccionar un tipo entrega.",
                    minlength: "Número de caracteres no permitido"
                },
                fechaetapa: {
                    required: "Debe ingresar una fecha de entrega."
                },
                horaetapa: {
                    required: "Debe ingresar una fecha de entrega.",
                    minlength: "Debe ingresar una hora correcta."
                },
                documento : {
                    required: "Debe ingresar el DNI.",
                    minlength: "Cantidad de caracteres incorrectos",
                    maxlength: "Cantidad de caracteres incorrectos",
                },
                recurso : {
                    required : "Debe ingresar el NOMBRE."
                }
            },
        });

        $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        //let $btnAsignar = $("#btnAsignar")
        //$btnAsignar.click(function () {
        //    let vurl2 = UrlHelper.Action("AsignarTecnico", "Reparacion", "Reparacion");
        //    $.ajax({
        //        type: "POST",
        //        url: vurl2,
        //        data: { "id": id, idtecnico: $("#idtecnico").val() },
        //        dataType: "JSON",
        //        success: function (response) {
        //            reload()
        //            $("#modalcontainer").modal("hide");
        //        }
        //    });

        //})


    });
}

function OnCompleteTransaction(xhr, status) {
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true) {
        $("#modalcontainerL").modal("hide");
        reload()
    }
    else {
        messagebox("No puede continuar", jsonres.msj, "warning");

    }
}
function habilitarguias() {
    $("#guia").val('');
    $("#guia").removeAttr('disabled');

    $("#cantidad").val('0');
    $("#cantidad").removeAttr('disabled');


    reloadgridguias_light();
}
function deshabilitarguias() {
    $("#guia").val('');
    $("#guia").attr('disabled', 'true');

    var grilla = $("#gridguias");
    $(grilla).jqGrid('clearGridData');

    $("#cantidad").val('0');
    $("#cantidad").attr('disabled', 'true');





}
function OnCompleteTransaction_Incidencia(xhr, status) {
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true) {
        swal({
            title: "Registro Exitoso",
            text: "Se registró correctamente el dato.",
            type: "success"
        },
        function () {
            $("#modalcontainer").modal("hide");
            reload()
          
        });
    }
    else {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}