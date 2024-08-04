$(document).ready(function () {

    var table = $('#tblPurchaseRequestDetail').DataTable({
        info: false,
        paging: false,
        layout: {
            topEnd: null
        }
    });
    purchaseRequestDetails.forEach(function (detail) {
        addRow(detail);
    });

    $('#addRowBtn').on('click', function () {
        addRow(null);
    });

    // Initial call to set up any existing rows
    initializeSelect2();

    // Reinitialize Select2 after every draw event (optional)
    table.on('draw', function () {
        initializeSelect2();
    });

    function initializeSelect2() {
        $('.select2-dropdown-cell-Product').select2({
            placeholder: 'Select an item',
            allowClear: false,
            ajax: {
                url: '/PurchaseRequest/SearchProduct',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    var exceptList = purchaseRequestDetails.map(m => m.Product.Name);
                    return {
                        term: params.term,
                        except: JSON.stringify(exceptList)
                    };
                },
                processResults: function (data) {
                    return {
                        results: data.map(function (item) {
                            return {
                                id: item.id,
                                text: item.name
                            };
                        })
                    };
                },
                cache: true
            },
            minimumInputLength: 1
        });
    }

    function addRow(detail) {
        var rowCount = table.rows().count();

        var productElement = document.createElement("select");
        productElement.className = "select2-dropdown-cell-Product";
        productElement.style.width = "100%";
        productElement.name = "PurchaseRequestDetails[" + rowCount + "].Product.Id";

        var option = document.createElement("option");
        option.value = detail?.Product.Id;
        option.textContent = detail?.Product.Name;
        option.selected = true;
        productElement.appendChild(option);

        // Quantity input element
        var quantityElement = document.createElement("input");
        quantityElement.type = "number";
        quantityElement.name = "PurchaseRequestDetails[" + rowCount + "].Quantity";
        quantityElement.className = "form-control";
        quantityElement.oninput = updateTotalPrice;

        //// Price input element
        //var priceElement = document.createElement("input");
        //priceElement.type = "number";
        //priceElement.name = "PurchaseRequestDetails[" + rowCount + "].Price";
        //priceElement.className = "form-control";
        //priceElement.oninput = updateTotalPrice;

        //// Total price label element
        //var totalPriceElement = document.createElement("input");
        //totalPriceElement.name = "PurchaseRequestDetails[" + rowCount + "].TotalPrice";
        //totalPriceElement.className = "form-control";
        //totalPriceElement.disabled = true;
        //totalPriceElement.value = "0.00";

        var removeElement = document.createElement("button");
        removeElement.type = "button";
        removeElement.innerText = "Remove";
        removeElement.className ="btn btn-danger"
        removeElement.onclick = function () { removeRow(rowCount); };

        var newRow = table.row.add([
            productElement.outerHTML, // Note: Use outerHTML to render HTML elements correctly
            quantityElement.outerHTML,
            //priceElement.outerHTML,
            //totalPriceElement.outerHTML,
            removeElement
        ]).draw().node();

        // Update references to newly added row elements
        var $newRow = $(newRow);
        /*$newRow.find('select').select2(); // Initialize select2*/
        $newRow.find('input').on('input', updateTotalPrice);
    }

    function updateTotalPrice() {
        //// Find the row that contains the changed input
        //var $row = $(this).closest('tr');

        //// Get the quantity and price inputs within the same row
        //var quantity = parseFloat($row.find('input[name$=".Quantity"]').val()) || 0;
        //var price = parseFloat($row.find('input[name$=".Price"]').val()) || 0;

        //// Calculate the total price
        //var totalPrice = quantity * price;

        //// Update the total price label
        //$row.find('input[name$=".TotalPrice"]').val(totalPrice.toFixed(2).toString());
    }

    function removeRow(rowIndex) {
        // Remove the row from the DataTable
        table.row(rowIndex).remove().draw();

        // Remove the corresponding item from purchaseRequestDetails
        purchaseRequestDetails.splice(rowIndex, 1);

        // Update the names of all rows after the removed row
        updateRowNames();
    }

    function updateRowNames() {
        // Get all rows from the DataTable
        var rows = table.rows().nodes();

        // Loop through each row and update the name attribute for each input/select element
        rows.each(function (row, index) {
            $(row).find('select, input, label').each(function () {
                var inputName = $(this).attr('name');
                if (inputName) {
                    var newName = inputName.replace(/PurchaseRequestDetails\[\d+\]/, 'PurchaseRequestDetails[' + index + ']');
                    $(this).attr('name', newName);
                }
            });
        });
    }

    // Function to add new details (triggered by some event)
    function addNewDetail(newDetail) {
        purchaseRequestDetails.push(newDetail);
        addRow(newDetail);
    }
});





