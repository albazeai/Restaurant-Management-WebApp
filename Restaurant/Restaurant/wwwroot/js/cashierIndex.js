﻿

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

                // Calculating the quantity of each item
                let item = data.split(',');
                let modItems = [];
                let parallelItems = [];
                for (var j = 0; j < item.length; j++) {
                    let count = 0;
                    let g = item[j];
                    for (var c = 0; c < item.length; c++) {
                        if (item[c].trim() === g.trim()) {
                            count++;
                        }
                    }
                    let y = g + '<span class="bg-dark m-2 p-2"> [ ' + count + " ]</span>";
                    if (!modItems.includes(y.trim()) && g.trim() !== "") {
                        modItems.push(y.trim()); 
                        parallelItems.push(g + ','); 
                    }

                }

                let i = 0;
                while (i < modItems.length) {
                    let val = parallelItems[i]; // array declared up ^ to store items with comma and use them as the button values.
                    $("#table-info").append('<div class="items"> <button class="btn btn-success m-2 buttons2" value = "' + val + '">' + modItems[i] + '</button></div>');
                    i++;
                }

            }
            // appending the Total html elements:
            $("#table-info").append('<div class="alert alert-success">' +
                                        '<h3>Total: $ ' + (data1.total.toFixed(2)) + '</h3>' +
                                        '<h3 class="">HST(13%): $ ' + (data1.total * 0.13).toFixed(2) + '</h3><hr/>' +
                                        '<h2 class="">Subtotal: $ ' + (data1.total + (data1.total * 0.13)).toFixed(2) + '</h2>' +
                                        '</div>');
            //let d = $('<div class="bg-info">< button class= "btn btn-danger m-2 removeTable" value = "' + data1.tableId + '" > Clear Table</button > <br><hr />< button class="btn btn-primary m-2 paySeparate" value="' + data1.tableId + '" > Pay Individually</button ></div>');
            //d.appendTo($("#table-info"));
            $("#table-info").append('<div>' + 
                '<button class= "btn btn-danger m-2 removeTable" value = "' + data1.tableId + '" > Clear Table</button > <br><hr /> ' +
                '<button class="btn btn-primary m-2 paySeparate" value="' + data1.tableId + '" > Pay Individually</button >' +
                '</div > ');
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
            let t = $(this).val();
            let text = t.replace(",", "");
            $("#itemsContainer").append('<div class="items3"> <button class="btn btn-success m-2 item2" value="' + $(this).val() + '">' + text + '</button><span class="d-none">,</span></div>');
            addTotal($(this).val()); // update total paidItemsInput
            $('#paidItemsInput').val($('.items3').text());
            
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
                        // separate pay contents
                        $('#hst').text("0.00");
                        $('#totalAmount').text("0.00");
                    } else {
                        $("#totalSep").text(total);
                        $("#finalSepTotal").val(Number($("#totalSep").text()));
                        // separate pay contents
                        let hst = Number($("#totalSep").text()) * 0.13;
                        $('#hst').text((hst.toFixed(2)));
                        let t = (Number($("#totalSep").text()) + hst).toFixed(2);
                        $('#totalAmount').text(t);
                    }
                }

            } else if (key === 1) {
                total = (currentTotal + amount).toFixed(2);
                $("#totalSep").text(total);
                $("#finalSepTotal").val(Number($("#totalSep").text()));
                // separate pay contents
                let hst = Number($("#totalSep").text()) * 0.13;
                $('#hst').text((hst.toFixed(2)));
                let t = (Number($("#totalSep").text()) + hst).toFixed(2);
                $('#totalAmount').text(t);
            } else if (key === 2) {
                amount = parseFloat(amount).toFixed(2);
                $("#totalSep").text(amount)
                $("#finalSepTotal").val(Number($("#totalSep").text()));
                // separate pay contents
                let hst = Number($("#totalSep").text()) * 0.13;
                $('#hst').text((hst.toFixed(2)));
                let t = (Number($("#totalSep").text()) + hst).toFixed(2);
                $('#totalAmount').text(t);
            }

        }
    }

});