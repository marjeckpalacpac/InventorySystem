$(document).ready(function () {
    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        serverSide: true,
        "ajax": {
            url: '/Product/SearchProduct',
            type: "POST",
            dataType: "json"
        },
        "columns": [
            { "data": 'id', "width": '15%' },
            { "data": 'name' },
            { "data": 'productCategory.name' },
            { "data": 'supplier.name' },
            { "data": 'minimumStock' },
            { "data": 'unit' },
            {
                "data": null,
                "render": function (data) {
                    
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Product/Edit/${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="/Product/Detail/${data}" class="btn btn-info mx-2">
                            <i class="bi bi-eye-fill"></i> View
                        </a>
                        <a href="/Product/Delete/${data}" class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                        
                    </div>`
                },
                "width": '35%'
            }
        ]
    });
}