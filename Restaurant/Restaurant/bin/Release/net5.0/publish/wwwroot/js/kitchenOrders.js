

/* getting the messages */
//$.ajax({
//    type: "Post",
//    url: "/Cashier/GetMessages",
//    data: {},
//    success: function (data) {
//        loadMessages(data);
//    },
//    error: function (req, status, error) {
//        console.log(msg);
//    }
//});

/* recall get messages to keep data up to date through ajax calls */
//setInterval(

//    function getMessages() {
//        $.ajax({
//            type: "Post",
//            url: "/Kitchen/KitOrderStatus",
//            data: {},
//            success: function (data) {
//                loadMessages(data);
//            },
//            error: function (req, status, error) {
//                //console.log(msg);
//            }
//        });

//    }, 1000);

//function loadMessages(messages) {
//    if (messages !== null) {
//        $("#main").html("");
//        for (let i = 0; i < messages.length; i++) {
//            if (messages[i].status.toLowerCase() === "not set") {
//                $("#main").append('<div class="alert alert-info"><h1 class="bg-info text-center text-light p-2">Table - ' + messages[i].tableId + '</h1><h2 class="alert alert-danger p-2">' + messages[i].items + '</h2><h3>Notes: ' + messages[i].notes + '</h3><button class="btn btn-danger s-update" value="' + messages[i].messageId + '">in progress</button></div>');
//            }
//            else if (messages[i].status.toLowerCase() === "in progress") {
//                $("#main").append('<div class="alert alert-warning"><h1>Table - ' + messages[i].tableId + '</h1><h2>' + messages[i].items + '</h2><h3>Notes: ' + messages[i].notes + '</h3><button class="btn btn-success s-update" value="' + messages[i].messageId + '">Complete</button></div>');
//            }
//            else if (messages[i].status.toLowerCase() === "complete") {
//                $("#main").append('<div class="alert alert-success"><h1>Table - ' + messages[i].tableId + '</h1><h2 class="">' + messages[i].items + '</h2><h3>Notes: ' + messages[i].notes + '</h3><button class="btn btn-success" disabled>Order Completed</button></div>');
//            }
//        }

//    }
//}

///* Updating the order status */
//$('#main').on('click', '.s-update', function () {
//    let messageId = $(this).val();
//    let status = $(this).text();
//    $.ajax({
//        type: "Post",
//        url: "/Kitchen/SetOrderStatus",
//        data: { id: messageId, status: status },
//        success: function (data) {

//        },
//        error: function (req, status, error) {
//            //console.log(msg);
//        }
//    });

//});