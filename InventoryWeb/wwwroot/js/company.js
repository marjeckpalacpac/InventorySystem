$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        serverSide: true,
        "ajax": {
            url: '/Company/SearchCompany',
            type: "POST",
            dataType: "json"
        },
        "columns": [
            { "data": 'id', "width": '15%' },
            { "data": 'name' },
            { "data": 'telephoneNo' },
            { "data": 'email' },
            {
                "data": 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Company/Edit/${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="/Company/Delete/${data}" class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": '35%'
            }
        ]
    });
}