
var dataTable;

$(function () {
    cargarDatatable();
});

function cargarDatatable() {
    var controller = "Sliders";
    dataTable = $("#tblSliders").DataTable({
        "ajax": {
            "url": `/Admin/${controller}/GetAll`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "20%" },
            {
                "data": "urlImagen", "width": "10%", "render": function (imagen) {
                    return `<img src="../${imagen}" width="200" />`;
                }
            },
            {
                "data": "estado", "width": "15%", "render": function (data) {
                    return data ? "<input type='checkbox' class='form-control' checked='checked' value='true' disabled='disabled' />"
                        : "<input type='checkbox' class='form-control' value='false' disabled='disabled' />";
                }
            },
            {
                "data": "fechaCreacion", "width": "15%", "class": "text-center", "render": function (data) {
                    return `<span >${data.substring(0,10)}</span>`;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href='/Admin/${controller}/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px' >
                                    <i class="fas fa-edit"></i> Editar 
                                </a>
                                &nbsp;
                                <a onclick ="Delete('/Admin/${controller}/Delete/${data}')" class='btn btn-danger text-white' style='cursor:pointer; width:100px' >
                                    <i class="fas fa-trash-alt"></i> Borrar 
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "¿Esta seguro de borrar?",
        text: "¡Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "¡Sí, borrar!",
        closeOnconfirm: true
    }, function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.success(data.message);
                    }
                }
            });
    });
}