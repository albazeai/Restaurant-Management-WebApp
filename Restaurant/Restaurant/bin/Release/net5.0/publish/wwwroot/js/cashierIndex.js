

$(document).ready(function () {

    $("#tableSelecterIndex").change(function () {
        let tableId = isInteger($("#tableSelecterIndex").val());
        if (tableId) {
            let id = $("#tableSelecterIndex").val();
            $.ajax({
                type: "Post",
                url: "/Cashier/TableDetails",
                data: { id: id },
                success: function (data) {
                    loadItems(data);
                },
                error: function (req, status, error) {
                    //console.log(msg);
                }
            });
        } else {
            $(".payContainer").hide();
            $("#table-info").html("");
            $("#itemsContainer").html("");
            setTotal(2, 0.00);
        }

    });

    /**
     * 
     *  validation method that validate the any integer value. (mostly used for table Id)
     */
    function isInteger(value) {
        return /^\d+$/.test(value);
    }

    /**
     * loading the table items.
     * @param {any} data1
     */
    function loadItems(data1) {
        if (data1 !== null) {
            $("#table-info").html("");
            $("#itemsContainer").html("");
            setTotal(2, 0.00); // reset total
            $("#table-info").append('<h2 class="bg-info text-center text-light p-2">Table - ' + data1.tableId + '</h2>');
            $("#itemsContainer").append('<h2 class="bg-info text-center text-light p-2">Table - ' + data1.tableId + '</h2>');
            $('#paidItemsInput').val("");
            $(".payContainer").hide();
            $("#flagBtn").val('0');
            $("#tableId").val(data1.tableId);  // setting up the current table id for separate pay form. 
            let item = "";
            let data = data1.tableItems;
            if (data !== null) {
                let i = 0;
                while (i < data.length) {
                    if (data[i] !== ',') {
                        item += data[i];
                        i++;
                    } else {
                        let itemValue = item + ',';
                        $("#table-info").append('<div class="items"> <button class="btn btn-success m-2 buttons2" value = "' + itemValue + '">' + item + '</button></div>');
                        item = "";
                        i++;
                    }
                }

            }
            $("#table-info").append('<h3 class="alert alert-success">Total: $ ' + (data1.total.toFixed(2)) + '</h3><button class="btn btn-danger m-2 removeTable" value="' + data1.tableId + '">Clear Table</button><br><hr/><button class="btn btn-primary m-2 paySeparate" value="' + data1.tableId + '">Pay Individually</button>');
        }
    }

    /* Clearing tables on clear button clicks */

    $("#table-info").on("click", ".removeTable", function () {

        if (confirm("Confirm Delete?")) {
            let tableId = isInteger($(this).val());
            if (tableId) {
                let id = $(this).val();
                /* sending request to clear the table */
                $.ajax({
                    type: "Post",
                    url: "/Cashier/ClearTable",
                    data: { id: id },
                    success: function (data) {
                        window.location.href = "/Cashier";
                    },
                    error: function (req, status, error) {
                        //console.log(msg);
                    }
                });
            }
        } else {

        }

    });

    /* Pay Separatly button event */
    $(".payContainer").hide();
    $("#table-info").on("click", ".paySeparate", function () {
        $(".payContainer").show();
        $("#flagBtn").val(1);
    });


/* Adding items */
    $("#table-info").on("click", ".buttons2", function () {
        let flag = $("#flagBtn").val();
        if (flag === '1') {
            $("#itemsContainer").append('<div class="items3"> <button class="btn btn-success m-2 item2" value="' + $(this).val() + '">' + $(this).text() + '</button><span class = "d-none">,</span></div>');
            addTotal($(this).val()); // update total paidItemsInput
            $('#paidItemsInput').val($('.items3').text());
            //console.log("table id = " + $("#tableId").val())
            //console.log("paidItemsInput = " + $("#paidItemsInput").val())
        }

    });


    /* Removing items on clicking them */
    $('#itemsContainer').on('click', '.items3', function () {
        $(this).remove();
        subTotal($(this).text()); // update total
        $('#paidItemsInput').val($('.items3').text());
    });

    ///**
    // * process pay button on click event.
    // * @param {any} item
    // */
    //$("#processPay").click(function () {
    //    let items = $('.items3').text();
    //    $('#paidItemsInput').val(items);
    //    alert("items3 = " + items);
    //});

    /* CALCULATING THE TOTAL */

    /**
     * Adding to total on adding item
     * 
     * @param {any} item
     */
    function addTotal(item) {
        let total = 0;
        let amount = "";

        if (item.length !== 0) {
            let i = 0;
            while (i < item.length) {
                if (item[i] === '$') {
                    amount = "";
                    i++;
                    while (item[i] !== " " && item[i] !== '$' && i < item.length) {
                        amount += item[i];
                        i++;
                    }
                    total = parseFloat(amount);
                } else {
                    i++;
                }
            }

            setTotal(1, total);    // updating the total
        }

    }

    /**
     * Updating the total amount on item removed.
     * 
     * @param {any} item
     */
    function subTotal(item) {
        let total = 0;
        let amount = "";

        if (item.length !== 0) {
            let i = 0;
            while (i < item.length) {
                if (item[i] === '$') {
                    amount = "";
                    i++;
                    while (item[i] !== " " && item[i] !== '$' && i < item.length) {
                        amount += item[i];
                        i++;
                    }
                    total = parseFloat(amount);
                } else {
                    i++;
                }
            }

            setTotal(0, total);    // updating the total
        }

    }

    /* calculating the total amount */
    /**
     * Set total will update the total amount.
     * 
     * @param {any} key 0 == - , 1 == + , 2 == set as new Value 
     * @param {any} value amount
     */
    function setTotal(key, value) {
        if (value != null && key != null) {
            let amount = Number(value);
            let currentTotal = $("#totalSep").text();
            currentTotal = Number(currentTotal)

            let total = 0;
            if (key === 0) {
                if (currentTotal > 0) {
                    total = (currentTotal - amount).toFixed(2);;
                    if (total <= 0) {
                        $("#totalSep").text("0.00");
                        $("#finalSepTotal").val(Number($("#totalSep").text()));
                    } else {
                        $("#totalSep").text(total);
                        $("#finalSepTotal").val(Number($("#totalSep").text()));
                    }
                }

            } else if (key === 1) {
                total = (currentTotal + amount).toFixed(2);
                $("#totalSep").text(total);
                $("#finalSepTotal").val(Number($("#totalSep").text()));
            } else if (key === 2) {
                amount = parseFloat(amount).toFixed(2);
                $("#totalSep").text(amount)
                $("#finalSepTotal").val(Number($("#totalSep").text()));;
            }

        }
    }

});