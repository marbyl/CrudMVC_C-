function Inicio() {
    //$('#listClima').jqGrid("GridUnload");
    var url = urlConsultarClimas;
    var urlCrud = urlCrudClimas;
    $('#listClima').jqGrid("GridUnload");
    var modelo = [
        {
            name: 'id', index: 'id', label: 'Id',
            width: 150, sorttype: 'int', fixed: true
        },
        {
            name: 'Ciudad', index: 'Ciudad', label: 'Ciudad', editable: true, editrules: { required: true }, edittype: 'select', editoptions: {
                dataUrl: urlTraerCiudades, defaultValue: 'Intime',
            dataEvents: [
                {
                    type: 'change', // keydown
                    fn: function (e) {
                        var thisval = $(e.target).val();
                    }
                }
            ]
            },
            width: 200, sorttype: 'int', editable: true, editrules: { required: true },  formoptions: { rowpos: 1, label: "Ciudad", elmsuffix: "(*)" }
        },
        {
            name: 'Temperatura', index: 'Temperatura', label: 'Temperatura',
            width: 200, sorttype: 'int', editable: true, editrules: { required: true }, edittype: 'text', formoptions: { rowpos: 2, label: "Temperatura", elmsuffix: "(*)" }
        },
        {
            name: 'Descripcion', index: 'Descripcion', label: 'Descripcion',
            width: 200, sorttype: 'int', editable: true, editrules: { required: true }, edittype: 'text', formoptions: { rowpos: 3, label: "Descripcion", elmsuffix: "(*)" }
        },
        {
            name: 'Fecha', index: 'Fecha', label: 'Fecha',
            width: 200, sorttype: 'int', editable: true, editrules: { required: true }, edittype: 'text', formoptions: { rowpos: 4, label: "Fecha", elmsuffix: "(*)" }
        },
    ];
    registerGridBaseSinExcel('#listClima', '#pager', 'Listado de climas', 'adsfasdf', url, urlCrud, modelo, false, true, false, false, 'asc', null, null);
}

/// <summary>
/// Metodo para generar un plantilla de grilla
/// </summary>
/// <param name="grid_t">Nomber del objeto donde se ubicara la grilla</param>
/// <param name="pager_t">Nombre del objeto donde se ubicara el paginador de la grilla</param>
/// <param name="caption_t">Titulo de la grilla</param>
/// <param name="sort_t">Indica el campo base por el cual seran ordenados los registros</param>
/// <param name="actionread_t">Url donde seran enviadas todas las solicitudes de consulta</param>
/// <param name="actionedit_t">url donde seran enviadas las solicitudes de adicion, actualizacion y eliminacion de registros</param>
/// <param name="colmodel_t">Arreglo con la informacion de las columnas que va a contener la grilla </param>
/// <param name="bview">Indica si la grilla permite la visualizacion de registros</param>
/// <param name="badd">Indica si la grilla permite realizar la adicion de registros</param>
/// <param name="bedit">Indica si la grilla permite realizar la edicion de registros</param>
/// <param name="bdel">Indica si la grilla permite realizar la eliminacion de registros</param>
/// <param name="sorder">Tipo de ordenamiento</param>
/// <returns></returns>
function registerGridBaseSinExcel(grid_t, pager_t, caption_t, sort_t, actionread_t, actionedit_t, colmodel_t, bview, badd, bedit, bdel, sorder, t_onSelectRow, verificainicioform) {
    var lastsel2
    $(grid_t).jqGrid({
        cmTemplate: { sortable: false },
        url: actionread_t,
        editurl: actionedit_t,
        datatype: 'json',
        mtype: 'post',
        colModel: colmodel_t,
        pager: pager_t,
        rowNum: 50,
        rowList: [50, 100, 200],
        sortname: sort_t,
        sortorder: sorder,
        viewrecords: true,
        gridview: true,
        width: $(window).width() - 300,
        height: "100%",
        caption: caption_t,
        loadError: function (jqXHR, textStatus, errorThrown) {
            alert('Error: ' + $('i', jqXHR.responseText).html());
        },
        onSelectRow: function (id) {
            if (id && id !== lastsel2) {
                jQuery('#listEstrcutura').jqGrid('restoreRow', lastsel2, false, 'clientArray');
                jQuery('#listEstrcutura').jqGrid('editRow', id, true);
                lastsel2 = id;
            }
        }
    });
    $(grid_t).jqGrid('navGrid', pager_t,
        { view: bview, add: badd, edit: bedit, del: bdel, search: false, refresh: false },
        {

            width: 800, closeOnEscape: true, closeAfterAdd: true, closeAfterEdit: true, bottominfo: "Registros marcados con (*) son requeridos",
            afterSubmit: function (response, postdata) {

                if (response.status == 200) {
                    alert('Operación realizada con éxito');
                    return [true];
                }
                else {
                    alert(response.responseJSON.Messages[0].Text);
                    return [false, response.responseJSON.Messages[0].Text];
                }
            },
            recreateForm: true,
            beforeShowForm: verificainicioform,
            errorTextFormat: function (response) {
                if (response.status != '200') {
                    return 'Error: ' + response.responseJSON.Messages[0].Text;
                }
                else {
                    return 'Save ok!'
                }
            }
        }, // prmEdit
        {
            width: 800, closeOnEscape: true, closeAfterAdd: true, closeAfterEdit: true, bottominfo: "Registros marcados con (*) son requeridos",
            afterSubmit: function (response, postdata) {

                if (response.status == 200) {
                    alert('Operación realizada con éxito');
                    return [true];
                }
                else {
                    alert(response.responseJSON.Messages[0].Text);
                    return [false, response.responseJSON.Messages[0].Text];
                }
            },
            recreateForm: true,
            beforeShowForm: verificainicioform,
            errorTextFormat: function (response) {

                if (response.status != '200') {
                    return 'Error: ' + response.responseJSON.Messages[0].Text;
                }
                else {
                    return 'Save ok!'
                }
            }
        }, // prmAdd
        {
            width: 800, closeOnEscape: true,
            afterSubmit: function (response, postdata) {

                if (response.status == 200) {
                    alert('Operación realizada con éxito');
                    return [true];
                }
                else {
                    alert(response.responseJSON.Messages[0].Text);
                    return [false, response.responseJSON.Messages[0].Text];
                }
            },
            recreateForm: true,
            beforeShowForm: verificainicioform,
            errorTextFormat: function (response) {

                if (response.status != '200') {
                    return 'Error: ' + response.responseJSON.Messages[0].Text;
                    //return 'error: ' + response.responseText;
                }
                else {
                    return 'Save ok!'
                }
            }
        }, // prmDel
        {
            width: 800, closeOnEscape: true, clearSearch: false, recreateForm: true,
            afterShowSearch: function ($form) {

                // the idea to fix the problem is the same as in the
                // http://www.trirand.com/blog/?page_id=393/bugs/heightauto-works-wrong-in-ie-height100-works-correct/#p22557
                var browserVersionParts = $.browser.version.split('.');
                if ($.browser.msie && (browserVersionParts.length > 1 && browserVersionParts[0] === '9' ||
                    document.documentMode === 9)) {
                    // $form.css('height', '100%');
                }
            },
            errorTextFormat: function (response) {

                if (response.status != '200') {
                    return 'Error: ' + response.responseJSON.Messages[0].Text;
                }
                else {
                    return 'Save ok!'
                }
            }
        }, // prmSearch
        { width: 500, closeOnEscape: true, clearSearch: true }  // prmView
    );
    $(grid_t).jqGrid('saveRow', "rowid", false, 'clientArray');

}