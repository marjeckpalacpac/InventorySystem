$(document).ready(function () {
    loadDataTable();

    $(document).ready(function () {
        $('#multiSelect').select2({
            placeholder: 'Select items',
            allowClear: true
        });
    });
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
                "data": null,
                "render": function (data) {
                    let buttons = '<div class="w-75 btn-group" role="group">';

                    if (data.systemOwner) {
                        buttons += `<div class-"col-4">
                                        <a href="/Company/Detail/${data.id}" class="btn btn-secondary mx-2">
                                            <i class="bi bi-eye-fill"></i> View
                                        </a>
                                    </div>
                                    `;
                    } else {
                        buttons += `<a href="/Company/Edit/${data.id}" class="btn btn-primary mx-2">
                                        <i class="bi bi-pencil-square"></i> Edit
                                    </a>
                                    <a href="/Company/Detail/${data.id}" class="btn btn-secondary mx-2">
                                        <i class="bi bi-eye-fill"></i> View
                                    </a>
                                    <a href="/Company/Delete/${data.id}" class="btn btn-danger mx-2">
                                        <i class="bi bi-trash-fill"></i> Delete
                                    </a>`

                            ;
                    }

                    buttons += '</div>';
                    return buttons;
                    //return `<div class="w-75 btn-group" role="group">
                    //    <a href="/Company/Edit/${data}" class="btn btn-primary mx-2">
                    //        <i class="bi bi-pencil-square"></i> Edit
                    //    </a>
                    //    <a href="/Company/Delete/${data}" class="btn btn-danger mx-2">
                    //        <i class="bi bi-trash-fill"></i> Delete
                    //    </a>
                    //    <a href="/Company/Detail/${data}" class="btn btn-danger mx-2">
                    //        <i class="bi bi-eye-fill"></i> View
                    //    </a>
                    //</div>`
                },
                "width": '35%'
            }
        ]
    });
}