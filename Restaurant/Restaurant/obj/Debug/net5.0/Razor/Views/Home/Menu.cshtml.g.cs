#pragma checksum "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d994360986d0ed9e423af1c3b0efa3cece16f39b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Menu), @"mvc.1.0.view", @"/Views/Home/Menu.cshtml")]
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
#line 1 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d994360986d0ed9e423af1c3b0efa3cece16f39b", @"/Views/Home/Menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a59b7783d8b0512a9ae3ec4003f04bd353cebbb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Restaurant.Models.Food>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "category", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Menu", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
  
    ViewData["Title"] = "Menu";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d994360986d0ed9e423af1c3b0efa3cece16f39b5370", async() => {
                WriteLiteral("\r\n    <div class=\"form-group\">\r\n        <strong>Filter By Category: &emsp;</strong>\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d994360986d0ed9e423af1c3b0efa3cece16f39b5725", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 9 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.Categories;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n    <div class=\"form-group ml-2\">\r\n        <input type=\"submit\" value=\"Filter\" class=\"btn btn-primary\" /> &emsp;|&emsp;\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d994360986d0ed9e423af1c3b0efa3cece16f39b7631", async() => {
                    WriteLiteral(" All Items");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<hr />\r\n\r\n\r\n");
#nullable restore
#line 19 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"bg-danger text-light text-center p-3 mt-2 mb-2\">Our Food</h1>\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n");
#nullable restore
#line 24 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
             foreach (var item in Model)
            {
                try
                {
                    var imgSrc = "";
                    if (item.FoodImage != null)
                    {
                        var base64 = Convert.ToBase64String(item.FoodImage);
                        imgSrc = string.Format("data:image/jpg;base64,{0}", base64);
                    }


#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-md-6 col-lg-4 mb-1 text-center\">\r\n                        <div class=\"card\" style=\"width: 18rem;\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 1258, "\"", 1271, 1);
#nullable restore
#line 37 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
WriteAttributeValue("", 1264, imgSrc, 1264, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top customZoom\" style=\"width:100%;height:200px;\"");
            BeginWriteAttribute("alt", " alt=\"", 1337, "\"", 1343, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <div class=\"card-body\">\r\n                                <h2 class=\"card-title\">");
#nullable restore
#line 39 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
                                                  Write(item.FoodName.Replace('-', ' '));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                                <h3 class=\"card-text\">$");
#nullable restore
#line 40 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
                                                  Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 44 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"

                }
                catch (Exception) { continue; }

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n");
#nullable restore
#line 51 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 54 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
 if (ViewBag.Drinks != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"bg-danger text-light text-center p-3 mt-2 mb-2\">Our Drinks</h1>\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n\r\n");
#nullable restore
#line 60 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
             foreach (Drink item in ViewBag.Drinks)
            {
                try
                {
                    var imgSrc = "";
                    if (item.DrinkImage != null)
                    {
                        var base64 = Convert.ToBase64String(item.DrinkImage);
                        imgSrc = string.Format("data:image/jpg;base64,{0}", base64);
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-md-6 col-lg-4 mb-1 text-center\">\r\n                        <div class=\"card\" style=\"width: 18rem;\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 2531, "\"", 2544, 1);
#nullable restore
#line 72 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
WriteAttributeValue("", 2537, imgSrc, 2537, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top customZoom\" style=\"width:100%;height:200px;\"");
            BeginWriteAttribute("alt", " alt=\"", 2610, "\"", 2616, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <div class=\"card-body\">\r\n                                <h2 class=\"card-title\">");
#nullable restore
#line 74 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
                                                  Write(item.DrinkName.Replace('-', ' '));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                                <h3 class=\"card-text\">$");
#nullable restore
#line 75 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
                                                  Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 79 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
                }
                catch (Exception)
                {
                    continue;
                }

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n");
#nullable restore
#line 88 "C:\Users\Malba\OneDrive\Documents\RestaurantCapstone\Restaurant\Restaurant\Views\Home\Menu.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Restaurant.Models.Food>> Html { get; private set; }
    }
}
#pragma warning restore 1591