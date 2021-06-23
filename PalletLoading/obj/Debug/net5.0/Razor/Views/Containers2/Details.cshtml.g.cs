#pragma checksum "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8f7eb4f347f56ac2536747def1f03e7897aebba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Containers2_Details), @"mvc.1.0.view", @"/Views/Containers2/Details.cshtml")]
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
#line 1 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\_ViewImports.cshtml"
using PalletLoading;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\_ViewImports.cshtml"
using PalletLoading.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8f7eb4f347f56ac2536747def1f03e7897aebba", @"/Views/Containers2/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85b146527b5e52069227f21d355af491abfa5380", @"/Views/_ViewImports.cshtml")]
    public class Views_Containers2_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PalletLoading.ViewModels.ContainerDetailsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 style=\"text-align:center\">Details - ");
#nullable restore
#line 7 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                                   Write(Model.Container.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<div>\r\n    <div align=\"center\" style=\"padding:50px 300px 50px 300px\">\r\n        <div align=\"left\">\r\n            ");
#nullable restore
#line 12 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
       Write(Html.DisplayNameFor(model => Model.Type.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <span style=\"float:right\">\r\n                ");
#nullable restore
#line 14 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
           Write(Html.DisplayFor(model => Model.Type.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </span>\r\n        </div>\r\n\r\n        <div align=\"left\">\r\n            Country\r\n            <span style=\"float:right\">\r\n                ");
#nullable restore
#line 21 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
           Write(Html.DisplayFor(model => Model.Country.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n<h3 style=\"text-align:center\">Pallets</h3>\r\n\r\n");
#nullable restore
#line 30 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
  
    var lengthContainer = Model.Container.NoOfColumns;
    var widthContainer = Model.Container.NoOfRows;
    var totalLayer = lengthContainer * widthContainer;
    var palletsAux = Model.Pallets;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n\r\n<table border=\"1\" class=\"table-bordered\" style=\"border:2px solid; border-right-style:dotted; width: 100%; height: 100%; text-align: center; align-content: center;\">\r\n    <tbody>\r\n");
#nullable restore
#line 42 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
             for (int row = 0; row < widthContainer; row++)
            {
                var heightCustom = 100;

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr");
            BeginWriteAttribute("style", " style=\"", 1272, "\"", 1347, 6);
            WriteAttributeValue("", 1280, "width:200px;", 1280, 12, true);
            WriteAttributeValue(" ", 1292, "height:", 1293, 8, true);
#nullable restore
#line 45 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
WriteAttributeValue("", 1300, heightCustom.ToString(), 1300, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1324, "px;", 1324, 3, true);
            WriteAttributeValue(" ", 1327, "vertical-align:", 1328, 16, true);
            WriteAttributeValue(" ", 1343, "top", 1344, 4, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 46 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
             for (int col = 0; col < lengthContainer; col++)
            {
                string idPallet = row.ToString() + "," + col.ToString();
                var currentPallets = palletsAux.Where(x => x.Row == row && x.Column == col).OrderByDescending(x => x.OrderNo);

                if (currentPallets != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td style=\"width:200px; height:200px; text-align:center\">\r\n");
#nullable restore
#line 54 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                         foreach (var palet in currentPallets)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <input type=\"button\"");
            BeginWriteAttribute("id", " id=\"", 1914, "\"", 1928, 1);
#nullable restore
#line 56 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
WriteAttributeValue("", 1919, palet.Id, 1919, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1929, "\"", 1951, 1);
#nullable restore
#line 56 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
WriteAttributeValue("", 1937, palet.OrderNo, 1937, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 1952, "\"", 2024, 5);
            WriteAttributeValue("", 1960, "width:100%;", 1960, 11, true);
            WriteAttributeValue(" ", 1971, "height:", 1972, 8, true);
#nullable restore
#line 56 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
WriteAttributeValue("", 1979, heightCustom.ToString(), 1979, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2003, "px;", 2003, 3, true);
            WriteAttributeValue(" ", 2006, "text-align:center", 2007, 18, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"detailsBtn btn-info\">\r\n");
#nullable restore
#line 57 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n");
#nullable restore
#line 59 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                }
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tr>\r\n");
#nullable restore
#line 62 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>

<br />

<table class=""table-bordered"" border=""1"" style=""width:600px; height:300px; border:2px solid"" align=""center"">
    <tr>
        <div align=""center"">
        <td style=""background-color: antiquewhite; text-align: center; padding: 50px 100px 50px 100px"">
");
#nullable restore
#line 73 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
              
                if (Model.Pallet != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div align=\"left\">\r\n                        ");
#nullable restore
#line 77 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Pallet.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <span style=\"float:right\">");
#nullable restore
#line 78 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                                             Write(Html.DisplayFor(model => model.Pallet.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    </div>\r\n");
            WriteLiteral("                    <br />\r\n");
            WriteLiteral("                    <div align=\"left\">\r\n                        ");
#nullable restore
#line 84 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Pallet.OrderNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <span style=\"float:right\">");
#nullable restore
#line 85 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                                             Write(Html.DisplayFor(model => model.Pallet.OrderNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    </div>\r\n");
#nullable restore
#line 87 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </td>\r\n        </div>\r\n    </tr>\r\n</table>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n    $(\".detailsBtn\").click(function () {\r\n        var palletId = this.id;\r\n        var idContainer = $(\'#container\').val();\r\n        var path = \'");
#nullable restore
#line 99 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Containers2\Details.cshtml"
               Write(Url.Action("Details"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\' + \"?id=\" + idContainer + \"&pallet=\" + palletId;\r\n        window.location.href = path;\r\n    });\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PalletLoading.ViewModels.ContainerDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
