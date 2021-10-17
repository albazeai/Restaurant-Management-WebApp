

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
        //event.preventDefault();
       // window.location.href = "/Cashier";

        /* do ajax request to save & send a record to the order status database */
        let table = isInteger($("#tableSelecter").val());
        if (!table) {
            alert("You will need to select a table first!");
        } else {
            let items = addedItems.toString();
            if (items !== "") {
                console.log("tabel items = " + addedItems.toString())
                let tableId = $("#tableSelecter").val();
                let notes = $("#notes").val();
                if (notes.trim() === "")
                {
                    notes = "...";
                }
                let message = { TableId: tableId, Items: items, Notes: notes, Status: "Not Set" };

                /* Adding a new message */
                $.ajax({
                    type: "POST",
                    url: 'AddMessage',
                    data: message,
                    success: function (msg) {

                        if (msg) {
                            /* Saving the order */
                            let total = $("#total").text();
                            total = Number(total);
                            let order = { TableId: tableId, TableItems: items, Total: total };

                            $.ajax({
                                type: "POST",
                                url: '/Cashier/AddOrder',
                                data: order,
                                success: function (msg) {
                                    window.location.href = "/Cashier/AddOrder";
                                },
                                error: function (req, status, error) {
                                    console.log(msg);
                                }
                            });
                        } else {
                            alert("No Kitchen Items in The Order! Please use the 'Save' option.")
                        }
                       
                    },
                    error: function (req, status, error) {
                        console.log(msg);
                    }
                });
            } else {
                alert("Please Add Some items!");
            }

        }
        return false;
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
    let addedItems = [];

    $(".buttons").click(function () {
        let table = isInteger($("#tableSelecter").val());
        if (!table) {
            alert("You will need to select a table first!");
        } else {
            orders("add", $(this).val()); // add item to order
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

    /**
     * function that will deal with the added or removed items 
     * 
     * flag : string 'remove', 'add'
     * item: string item that need to be added or removed
     * */ 
    function orders(flag, item) {
        if (flag === "remove") {
            const index = addedItems.indexOf(item);
            if (index > -1) {
                addedItems.splice(index, 1);
            }
            $("#custom-left").html("");
            let u = new Set(addedItems);
            for (const value of u.values()) {
                let val = value;
                let count = 0;
                for (let i = 0; i < addedItems.length; i++) {
                    if (addedItems[i] === val) {
                        count++;
                    }
                }
                if (val !== "") {
                    $("#custom-left").append('<div class="items"> <button class="btn btn-success m-2 item" value="' + val + '">' + val.replace(',', '') + '<span class="badge badge-dark m-1 p-2">' + count + '</span></button></div>');
                }
            }
        } else if (flag === "add") {
            $("#custom-left").html("");
            addedItems.push(item.trim()); // add item 
            let u = new Set(addedItems);
            for (const value of u.values()) {
                let val = value;
                let count = 0;
                for (let i = 0; i < addedItems.length; i++) {
                    if (addedItems[i] === val) {
                        count++;
                    }
                }
                if (val !== "") {
                    $("#custom-left").append('<div class="items"> <button class="btn btn-success m-2 item" value="' + val + '">' + val.replace(',', '') + '<span class="badge badge-dark m-1 p-2">' + count + '</span></button></div>');
                }
            }
        }
    }
    /* Removing items on clicking them */
    $('#custom-left').on('click', '.item', function () {
        console.log("removed item value = " + $(this).val());
        orders("remove", $(this).val()); // remove item from order
        console.log("added items after removing: " + addedItems);
        //$(this).remove();
        subTotal($(this).val());  // update total
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
        if (data.tableItems !== null) {
            console.log("table info is not null!")
            loadItems(data.tableItems);
            setTotal(2, data.total);
        } else {
            console.log("table info is null!")
            addedItems = [];
            $("#custom-left").html("");
            setTotal(2, data.total);
        }
    }
    /**
     * load the exist items to the custom-left div. 
     * 
     * @param {any} data
     */
    function loadItems(data) {
        if (data.tableItems !== null) {
            addedItems = [];  // clearing old items
            let items = data.split(',');
            let i = 0;
            while (i < items.length) {
                if (items[i].trim() !== "") {
                    let val = items[i] + ",";
                    orders("add", val);
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
            //let items = $('.items').text();
            let items = addedItems.toString();
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
                    window.location.href = "/Cashier/AddOrder";
                },
                error: function (req, status, error) {
                    console.log(msg);
                }
            });

        }
    });

});
