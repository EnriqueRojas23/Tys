var idsOfSelectedRows = new Array();
const numberWithCommas = (x) => {
    return x.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
  
//contantes para el JQGrid.
var CONFIG_JQGRID = (function () {
    var private = {
        'formatoptions_date': { srcformat: 'ISO8601Long', newformat: 'm/d/Y', defaultValue: null },
        'formatoptions_currency_pen': { prefix: '(S/. ', suffix: ')', thousandsSeparator: ',', decimalPlaces: 2 },
        'formatoptions_currency_dol': { prefix: '($ ', suffix: ')', thousandsSeparator: ',', decimalPlaces: 2 },
        'formatoptions_currency': { thousandsSeparator: ',', decimalPlaces: 2 },
        'rownum': 50,
        'rowlist': [50, 100, 150],
        'emptyrecords': 'No se encontraron registros',
        'jsonReader': { root: "rows", page: "page", total: "total", records: "records", repeatitems: false, id: 0 }
    };
    return { get: function (name) { return private[name]; } }
})();

var MENSAJES = (function () {
    var private = {
        'err_nullarg': "el argumento no esta definido",
    };

    return {
        getError: function (fnc, typerr) {
            return format("Hubo un error en la funcion {fnc} de tipo: {arg}", { fnc: fnc, arg: private[typerr] });
        },
        get: function (name) { return private[name]; }
    }
})();

var format = function (str, col) {
    col = typeof col === 'object' ? col : Array.prototype.slice.call(arguments, 1);
    return str.replace(/\{\{|\}\}|\{(\w+)\}/g, function (m, n) {
        if (m == '{{') { return "{"; }
        if (m == '}}') { return "}"; }
        return col[n];
    });
};

var showEmptyRecordsGrid = function (grid) {

    var myGrid = $(grid);
    if (myGrid.getGridParam('reccount') === 0) {
        var pgboxes = myGrid.getGridParam('pager');
        $(".ui-paging-info", pgboxes).html(myGrid.getGridParam('emptyrecords'));
    }
}

$(document).ready(function () {
    //funcion que detiene el evento del onclick en un combo dentro de un panel dinamico
    $('select.form-control').on('click', function (event) {
        event.stopPropagation();
    });
});

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

//funcion que limpia el valor de un objeto, su funcionamiento esta ligado a la funcion $.each()
function setCleanControlText(obj) {
    if (obj != undefined) {
        switch ($(obj).prop("type")) {
            case "select-one":
                $(obj).prop('selectedIndex', 0);
                break;
            case "text":
                $(obj).val("");
                break;
            case "textarea":
                $(obj).val("");
                break;
            case "checkbox":
                $('.myCheckbox').prop('checked', false);
                break;
            case "hidden":
                $(obj).val("");
                break;
            default:
                $(obj).val("");
                break;
        }
    }
}

//funcion que establecer el estilo del checkbox dentro de una grilla.
function setStyleCheckBoxGrid(grilla) {
    var controles = $(grilla).find("input[type=checkbox]");
    var div = $('<div class="checkbox checkbox-danger"></div>');

    $.each(controles, function () {

        //obteniendo objetos
        var checkboxHtml = $(this)[0].outerHTML;
        var checkparent = $(this).parent();
        var divclone = $(div).clone(false)[0];

        //estableciendo valores
        $(divclone).html(checkboxHtml + '<label />');
        checkparent.html("");
        checkparent.html(divclone);

    });
}

//muestra el panel de cargando de la pagina.
function showLoading() {
    $.blockUI({
        message: '<div class="sk-spinner sk-spinner-circle"> <div class="sk-circle1 sk-circle"></div> <div class="sk-circle2 sk-circle"></div> <div class="sk-circle3 sk-circle"></div> <div class="sk-circle4 sk-circle"></div>  <div class="sk-circle5 sk-circle"></div> <div class="sk-circle6 sk-circle"></div>  <div class="sk-circle7 sk-circle"></div> <div class="sk-circle8 sk-circle"></div><div class="sk-circle9 sk-circle"></div> <div class="sk-circle10 sk-circle"></div><div class="sk-circle11 sk-circle"></div><div class="sk-circle12 sk-circle"></div></div>',
        theme: false,
        baseZ: 100000,
        css: {
            padding: 0,
            margin: 0,
            width: '30%',
            top: '40%',
            left: '35%',
            textAlign: 'center',
            cursor: 'wait',
            border: '0px',
            backgroundColor: '#efeded',
            opacity: 0.4
        },
        overlayCSS: { backgroundColor: '#efeded', opacity: 0.4, cursor: 'wait' }
    });
}

function hideLoading() {
    $.unblockUI();
}

$(document).ajaxStart(function () {
    showLoading();
});

$(document).ajaxComplete(function () {
    hideLoading();
});

function getValidationSummary() {
    var $el = $(".validation-summary-errors > ul");
    if ($el.length == 0) {
        $el = $("<div class='validation-summary-errors'><ul></ul></div>")
                 .hide()
                 .insertBefore('fieldset:first')
                 .find('ul');
    }
    return $el;
}

function getResponseValidationObject(response) {
    if (response && (response.Tag && response.Tag == "ValidationError") || response.res == false)
        return response;
    return null;
}

function CheckValidationErrorResponse(response, form, summaryElement) {

    var $list, data = getResponseValidationObject(response);
    if (!data) return false;

    $list = summaryElement || getValidationSummary();
    $list.html("");
    $.each(data.Errors, function (i, item) {

        var $val, lblTxt, errorList = "";

        if (item.Key) {
            $val = $(".field-validation-valid,.field-validation-error")
                        .first("[data-valmsg-for=" + item.Key + "]")
                        .removeClass("field-validation-valid")
                        .addClass("field-validation-error");
            $("input[name=" + item.Key + "]").addClass("input-validation-error")
            lblTxt = $("label[for=" + item.Key + "]").text();
            if (lblTxt) { lblTxt += ": "; }
        }
        if ($val != undefined) {
            if ($val.length) {
                $val.text(item.Value.shift());
                if (!item.Value.length) { return; }
            }
        }

        $.each(item.Value, function (c, val) {
            if (lblTxt == undefined) lblTxt = "";
            errorList += "<li>" + lblTxt + val + "</li>";
        });

        $list.append(errorList);
    });
    if ($list.find("li:first").length) { $list.closest("div").show(); }
    return true;
}
function CheckValidationErrorResponse(response, form, summaryElement) {

    var $list, data = getResponseValidationObject(response);
    if (!data) return false;

    $list = summaryElement || getValidationSummary();
    $list.html('');
    $.each(data.Errors, function (i, item) {

        var $val, lblTxt, errorList = "";

        if (item.Key) {
            $val = $(".field-validation-valid,.field-validation-error")
                        .first("[data-valmsg-for=" + item.Key + "]")
                        .removeClass("field-validation-valid")
                        .addClass("field-validation-error");
            $("input[name=" + item.Key + "]").addClass("input-validation-error")
            lblTxt = $("label[for=" + item.Key + "]").text();
            if (lblTxt) { lblTxt += ": "; }
        }
        if ($val != undefined) {
            if ($val.length) {
                $val.text(item.Value.shift());
                if (!item.Value.length) { return; }
            }
        }
                
        $.each(item.Value, function (c, val) {
            if (lblTxt == undefined) lblTxt = "";
            errorList += "<li>" + lblTxt + val + "</li>";
        });

        $list.append(errorList);
    });
    if ($list.find("li:first").length) { $list.closest("div").show(); }
    return true;
}
function CleanValidationError()
{
    $(".validation-summary-errors").html("");
}

function CheckValidationErrorWindowAlert(response) {
    var $list, data = getResponseValidationObject(response);
    $list = getValidationSummary();

    $list.html('');
    $.each(data.Errors, function (i, item) {
        var $val, lblTxt, errorList = "";

        $.each(item.Value, function (c, val) {
            errorList += val;
        });
        $list.append(errorList);
    });
    
}

function setModalMaxHeight(element) {
    this.$element = $(element);
    this.$content = this.$element.find('.modal-content');
    var borderWidth = this.$content.outerHeight() - this.$content.innerHeight();
    var dialogMargin = $(window).width() < 768 ? 20 : 60;
    var contentHeight = $(window).height() - (dialogMargin + borderWidth);
    var headerHeight = this.$element.find('.modal-header').outerHeight() || 0;
    var footerHeight = this.$element.find('.modal-footer').outerHeight() || 0;
    var maxHeight = contentHeight - (headerHeight + footerHeight);

    //$('.modal-content').css('width', $(window).width() * 0.8);

    this.$content.css({
        'overflow': 'hidden',
        'width': $(window).width() * 0.7,
        'left': 1
    });

    this.$element
      .find('.modal-body').css({
          'max-height': maxHeight,
          'overflow-y': 'auto'
      });
}

$(window).resize(function () {
    //if ($('.modal.in').length != 0) {
    //    setModalMaxHeight($('.modal.in'));
    //}
});

var ie = (navigator.appName.indexOf("Microsoft") >= 0);
function SoloNumerico(e) { var tecla = (ie) ? e.keyCode : e.which; var patron = /^[0-9]*$/; te = String.fromCharCode(tecla); if (!patron.test(te)) { (ie) ? e.keyCode = 0 : e.which = 0; return false; } else return true; }
function SoloDecimal(e, valor) { debugger; var tecla = (ie) ? e.keyCode : e.which; var patron = /^[-+]?[0-9]*\.?[0-9]*$/; te = String.fromCharCode(tecla); if (!patron.test(valor + te)) { (ie) ? e.keyCode = 0 : e.which = 0; return false; } else return true; }
function SoloTexto(e) { var tecla = (document.all) ? e.keyCode : e.which; if (tecla == 8) return true; var patron = /[A-Za-zñÑ\s]/; te = String.fromCharCode(tecla); if (!patron.test(te)) { (document.all) ? e.keyCode = 0 : e.which = 0; return false; } else return true; }

function validateFloatKeyPress(el, evt) {

    

    var re = /^-?\d*\.?\d{0,2}$/;

    var text = $(el).val();
    
    var isValid = (text.match(re) !== null);
    

    if (!isValid)
        return false;

    
  

    //var charCode = (evt.which) ? evt.which : event.keyCode;

    ////if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
    ////    return false;
    ////}

    ////if (charCode == 46 && el.value.indexOf(".") !== -1) {
    ////    return false;
    ////}

    //if (el.value.indexOf(".") !== -1) {
    //    var range = document.selection.createRange();

    //    if (range.text != "") {
    //    }
    //    else {
    //        var number = el.value.split('.');
    //        if (number.length == 2 && number[1].length > 1)
    //            return false;
    //    }
    //}

    return true;
}

function SoloTextoNumero(e) {
    var tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    var patron = /^[a-z\d\u00C0-\u00ff]+$/i; // expresión regular para letras(máy o minus), acentuadas o no, y números
    te = String.fromCharCode(tecla);
    if (!patron.test(te)) {
        (document.all) ? e.keyCode = 0 : e.which = 0; return false;
    }
    else return true;
}

//Determina el formato de los valores en los JQGRID
function formatedit(cellvalue, options, rowObject) {
    if (cellvalue == null)
        return "";
    else
        return " " + cellvalue;

}
function ToFixed(cellvalue, options, rowObject)
{
    if (cellvalue == null)
        return "";
    else
        return " " + String(parseFloat(cellvalue).toFixed(2));
}
function messagebox(title, msj, type)
{

    swal(title, msj, type);
}

