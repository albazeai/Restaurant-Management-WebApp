

$(document).ready(function () {

    // hide notes form:
    $("#notesForm").hide();

    /* To Kitchen Order Copy */
    $("#toKitchenBtn").click(function () {
        $(this).hide();
        $("#notesForm").show();
    });

    /* Kitchen Form Submit */
    $("#kitchenForm").submit(function () {
        event.preventDefault();
       // window.location.href = "/Cashier";

        /* do ajax request to save & send a record to the order status database */
        let table = isInteger($("#tableSelecter").val());
        if (!table) {
            alert("You will need to select a table first!");
        } else {
            let items = $('.items').text();
            if (items !== "") {
                let tableId = $("#tableSelecter").val();
                let notes = $("#notes").val();
                let message = { TableId: tableId, Items: items, Notes: notes, Status: "Not Set" };

                /* Adding a new message */
                $.ajax({
                    type: "POST",
                    url: 'AddMessage',
                    data: message,
                    success: function (msg) {

                        /* Saving the order */
                        let total = $("#total").text();
                        total = Number(total);
                        let order = { TableId: tableId, TableItems: items, Total: total };

                        $.ajax({
                            type: "POST",
                            url: '/Cashier/AddOrder',
                            data: order,
                            success: function (msg) {
                                window.location.href = "/Cashier";
                            },
                            error: function (req, status, error) {
                                console.log(msg);
                            }
                        });
                    },
                    error: function (req, status, error) {
                        console.log(msg);
                    }
                });
            } else {
                alert("No items!");
            }

        }
    });
    /* Menu Selecter */
    $("#menuOptions").change(function () {
        let option = $("#menuOptions").val();
        if (option === "foods") {
            $("#foodsSection").css("display", "block");
            $("#drinksSection").css("display", "none");
        } else if (option === "drinks") {
            $("#drinksSection").css("display", "block");
            $("#foodsSection").css("display", "none");
        }else {
            $("#drinksSection").css("display", "block");
            $("#foodsSection").css("display", "block");
        }

    });
    /* Adding items */
    $(".buttons").click(function () {
        let table = isInteger($("#tableSelecter").val());

        if (!table) {
            alert("You will need to select a table first!");
        } else {
            $("#custom-left").append('<div class="items"> <button class="btn btn-success m-2 item" value="' + $(this).val() + '">' + $(this).text() + '<span class="d-none">,</span></button></div>');
            addTotal($(this).val()); // update total
        }

    });
    /**
     *  Check if the table value is a number.
     *  
     * @param {any} value
     */
    function isInteger(value) {
        return /^\d+$/.test(value);
    }

    /* Removing items on clicking them */
    $('#custom-left').on('click', '.items', function () {
        $(this).remove();
        subTotal($(this).text());  // update total
    });

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

            setTotal(1,total);    // updating the total
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

/* Loading the selected table items if exists */

    $("#tableSelecter").change(function () {
        let tableId = isInteger($("#tableSelecter").val());
        if (tableId) {
            let id = $("#tableSelecter").val();
            $.ajax({
                type: "Post",
                url: "/Cashier/TableDetails",
                data: { id: id },
                success: function (data) {
                    tableInfo(data);
                },
                error: function (req, status, error) {
                    console.log(msg);
                }
            });
        } else {
            $("#custom-left").html("");
            setTotal(2, 0.00);

        }

    });

    function tableInfo(data) {
        if (data !== null) {
            loadItems(data.tableItems);
            setTotal(2,data.total);
        }
    }
    /**
     * load the exist items to the custom-left div. 
     * 
     * @param {any} data
     */
    function loadItems(data) {
        $("#custom-left").html("");
        let item = "";

        if (data !== null) {
            let items = data.split(',');
            let i = 0;
            while (i < items.length) {
                if (items[i].trim() !== "") {
                    let val = items[i] + ",";
                    $("#custom-left").append('<div class="items"> <button class="btn btn-success m-2 item" value="' + val + '">' + items[i] + '<span class="d-none">,</span></button></div>');
                }
                i++;
            }
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
            let currentTotal = $("#total").text();
            currentTotal = Number(currentTotal)

            let total = 0;
            if (key === 0) {
                if (currentTotal > 0) {
                    total = (currentTotal - amount).toFixed(2);;
                    if (total <= 0) {
                        $("#total").text("0.00");
                    } else {
                        $("#total").text(total);
                    }
                }

            } else if (key === 1) {
                total = (currentTotal + amount).toFixed(2);
                $("#total").text(total);
            } else if (key === 2) {
                amount = parseFloat(amount).toFixed(2);
                $("#total").text(amount);
            }

        }
    }



    /* Saveing the order using Save Button on click */

    $("#saveBtn").click(function () {
        let table = isInteger($("#tableSelecter").val());
        if (!table) {
            alert("You will need to select a table first!");
        } else {
            let items = $('.items').text();
            let tableId = $("#tableSelecter").val();
            let total = $("#total").text();
            total = Number(total);
            let order = { TableId: tableId, TableItems: items, Total: total };

            /* sending the updating request */
            $.ajax({
                type: "POST",
                url: 'AddOrder',
                data: order,
                success: function (msg) {
                    window.location.href = "/Cashier";
                },
                error: function (req, status, error) {
                    console.log(msg);
                }
            });

        }
    });

});
