#pragma checksum "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\Cashier\CashOrderStatus.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e310a5ea8af0135f1dfc27fe013b13d3f7e80e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cashier_CashOrderStatus), @"mvc.1.0.view", @"/Views/Cashier/CashOrderStatus.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e310a5ea8af0135f1dfc27fe013b13d3f7e80e5", @"/Views/Cashier/CashOrderStatus.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a59b7783d8b0512a9ae3ec4003f04bd353cebbb", @"/Views/_ViewImports.cshtml")]
    public class Views_Cashier_CashOrderStatus : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Restaurant.Models.Message>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\Cashier\CashOrderStatus.cshtml"
  
    ViewData["Title"] = "Order Status";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"bg-warning text-center p-2\">ORDERS</h1>\r\n<div id=\"main\" class=\"mb-5\"></div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 12 "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\Cashier\CashOrderStatus.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral(@"<script>
    $(document).ready(function () {

        /* recall get messages to keep data up to date through ajax calls */
        setInterval(

            function getMessages() {
                $.ajax({
                    type: ""Post"",
                    url: ""/Cashier/GetMessages"",
                    data: {},
                    success: function (data) {
                        loadMessages(data);  // load the messages orders
                    },
                    error: function (req, status, error) {
                        //console.log(msg);
                    }
                });

            }, 1000);

        function loadMessages(messages) {
            if (messages !== null) {
                $(""#main"").html("""");
                for (let i = 0; i < messages.length; i++) {

                    // Calculating the quantity of each item
                    let item = messages[i].items.split(',');
                    let modItems = """";
                    for (");
                WriteLiteral(@"var j = 0; j < item.length; j++) {
                        let count = 0;
                        let g = item[j];
                        if (g.trim() !== """") {
                            for (var c = 0; c < item.length; c++) {
                                if (item[c].trim() === g.trim()) {
                                    count++;
                                }
                            }
                            let y = g + '<span class=""badge badge-warning m-1 p-2"">' + count + '</span><hr/>';
                            if (!modItems.includes(y.trim())) {
                                modItems += y.trim();
                            }
                        }

                    }

                    if (messages[i].status.toLowerCase() === ""not set"") {
                        $(""#main"").append('<div class=""alert alert-info""><h1 class=""bg-info text-center text-light p-2"">Table - ' + messages[i].tableId + '<span class=""badge badge-warning p-2 float-right"">Not-Set</spa");
                WriteLiteral(@"n></h1><hr/><h2 class=""alert alert-danger text-dark p-2"">' + modItems + '</h2><hr/><h3><span class=""text-success""><em>Notes:</em></span>  ' + messages[i].notes + '</h3><hr/><button class=""btn btn-danger cancel"" value=""' + messages[i].messageId + '"">Cancel</button></div>');
                    }
                    else if(messages[i].status.toLowerCase() === ""in progress"") {
                        $(""#main"").append('<div class=""alert alert-warning""><h1 class=""bg-info text-center text-light p-2"">Table - ' + messages[i].tableId + '<span class=""badge badge-danger p-2 float-right"">In-Progress</span></h1><hr/><h2 class=""text-dark"">' + modItems + '</h2><hr/><h3><span class=""text-success""><em>Notes:</em></span>  ' + messages[i].notes + '</h3><hr/><button type=""button"" class=""btn btn-secondary"" disabled>in progress</button></div>');
                    }
                    else if (messages[i].status.toLowerCase() === ""complete""){
                        $(""#main"").append('<div class=""alert alert-success""><h1");
                WriteLiteral(@" class=""bg-info text-center text-light p-2"">Table - ' + messages[i].tableId + '<span class=""badge badge-success p-2 float-right"">Ready</span></h1><hr/><h2 class=""text-dark"">' + modItems + '</h2><hr/><h3><span class=""text-success""><em>Notes:</em></span>  ' + messages[i].notes + '</h3><hr/><button class=""btn btn-success status-update"" value=""' + messages[i].messageId + '"">Picked Up</button></div>');
                    }
                }

            }
        }
        /* Updating the order status on order completed */
        $('#main').on('click', '.status-update', function () {
            let messageId = $(this).val();
            let status = $(this).text();
            $.ajax({
                type: ""Post"",
                url: ""/Cashier/SetOrderStatus"",
                data: { id: messageId, status: status },
                success: function (data) {

                },
                error: function (req, status, error) {
                   // console.log(msg);
                }
");
                WriteLiteral(@"            });

        });

        /* Removing message using cancel button */
        $('#main').on('click', '.cancel', function () {
            if (confirm(""You are about to cancel the order?"")) {
                let messageId = $(this).val();
                $.ajax({
                    type: ""Post"",
                    url: ""/Cashier/RemoveMessage"",
                    data: { id: messageId },
                    success: function (data) {
                        //alert(""Removed Success!"");
                    },
                    error: function (req, status, error) {
                        //console.log(msg);
                    }
                });
            }

        });

    });
</script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Restaurant.Models.Message>> Html { get; private set; }
    }
}
#pragma warning restore 1591
