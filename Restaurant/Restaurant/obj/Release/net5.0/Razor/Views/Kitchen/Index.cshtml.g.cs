#pragma checksum "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\Kitchen\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4ac4858c5f298b41e034f5045d9d0d1c844850e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Kitchen_Index), @"mvc.1.0.view", @"/Views/Kitchen/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4ac4858c5f298b41e034f5045d9d0d1c844850e3", @"/Views/Kitchen/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a59b7783d8b0512a9ae3ec4003f04bd353cebbb", @"/Views/_ViewImports.cshtml")]
    public class Views_Kitchen_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\Kitchen\Index.cshtml"
  
    ViewData["Title"] = "Kitchen Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"bg-warning text-center p-2\">ORDERS</h1>\r\n\r\n<div id=\"main\"></div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 11 "D:\myProjects\RestaurantCapstone\Restaurant\Restaurant\Views\Kitchen\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    <script>
        $(document).ready(function () {
            /* recall get messages to keep data up to date through ajax calls */
            setInterval(

                function getMessages() {
                    $.ajax({
                        type: ""Post"",
                        url: ""/Kitchen/KitOrderStatus"",
                        data: {},
                        success: function (data) {
                            loadMessages(data);
                        },
                        error: function (req, status, error) {
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
        ");
                WriteLiteral(@"                for (var j = 0; j < item.length; j++) {
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
                            $(""#main"").append('<div class=""alert alert-info""><h1 class=""bg-info text-center text-light p-2"">Table - ' + ");
                WriteLiteral(@"messages[i].tableId + '<span class=""badge badge-warning p-2 float-right"">Not-Set</span></h1><hr/><h2 class=""alert alert-danger text-dark p-2"">' + modItems + '</h2><hr/><h3><span class=""text-success""><em>Notes:</em></span>  ' + messages[i].notes + '</h3><hr/><button class=""btn btn-danger s-update"" value=""' + messages[i].messageId + '"">in progress</button></div>');
                        }
                        else if (messages[i].status.toLowerCase() === ""in progress"") {
                            $(""#main"").append('<div class=""alert alert-warning""><h1 class=""bg-info text-center text-light p-2"">Table - ' + messages[i].tableId + '<span class=""badge badge-danger p-2 float-right"">In-Progress</span></h1><hr/><h2 class=""text-dark"">' + modItems + '</h2><hr/><h3><span class=""text-success""><em>Notes:</em></span>  ' + messages[i].notes + '</h3><hr/><button class=""btn btn-success s-update"" value=""' + messages[i].messageId + '"">Complete</button></div>');
                        }
                        else if");
                WriteLiteral(@" (messages[i].status.toLowerCase() === ""complete"") {
                            $(""#main"").append('<div class=""alert alert-success""><h1 class=""bg-info text-center text-light p-2"">Table - ' + messages[i].tableId + '<span class=""badge badge-success p-2 float-right"">Ready</span></h1><hr/><h2 class=""text-dark"">' + modItems + '</h2><hr/><h3><span class=""text-success""><em>Notes:</em></span>  ' + messages[i].notes + '</h3><hr/><button class=""btn btn-success"" disabled>Order is Ready</button></div>');
                        }
                      
                    }

                }
            }

            /* Updating the order status */
            $('#main').on('click', '.s-update', function () {
                let messageId = $(this).val();
                let status = $(this).text();
                $.ajax({
                    type: ""Post"",
                    url: ""/Kitchen/SetOrderStatus"",
                    data: { id: messageId, status: status },
                    success: func");
                WriteLiteral("tion (data) {\r\n\r\n                    },\r\n                    error: function (req, status, error) {\r\n                    }\r\n                });\r\n\r\n            });\r\n        });\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
