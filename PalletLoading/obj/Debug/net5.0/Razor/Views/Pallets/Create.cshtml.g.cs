#pragma checksum "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bc6772e322cd4b18dcbab083866ddeedf035dd2b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pallets_Create), @"mvc.1.0.view", @"/Views/Pallets/Create.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc6772e322cd4b18dcbab083866ddeedf035dd2b", @"/Views/Pallets/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85b146527b5e52069227f21d355af491abfa5380", @"/Views/_ViewImports.cshtml")]
    public class Views_Pallets_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PalletLoading.ViewModels.AddContainerViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("control-label"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("palletName"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:200px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
  
    ViewData["Title"] = "Create";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 style=\"text-align:center\">Add pallets</h1>\r\n\r\n");
#nullable restore
#line 8 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
  
    var type = Model.Type;
    var lengthContainer = Model.Container.NoOfColumns;
    var widthContainer = Model.Container.NoOfRows;

    var totalLayer = lengthContainer * widthContainer;
    var palletsAux = Model.Pallets;

    var containerName = Model.Container.Name;
    var countryName = Model.Container.Country.Abbreviation;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n<h5 style=\"text-align:right\">Max pallets per layer: ");
#nullable restore
#line 21 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                                               Write(totalLayer);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n<br />\r\n\r\n<input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 607, "\"", 634, 1);
#nullable restore
#line 24 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 615, Model.Container.Id, 615, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"container\" />\r\n<table border=\"1\" class=\"table-bordered\" style=\"border:2px solid; border-right-style:dotted; width: 100%; height: 100%; text-align: center; align-content: center;\">\r\n    <tbody>\r\n");
#nullable restore
#line 28 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
             for (int row = 0; row < widthContainer; row++)
            {
                var heightCustom = 100;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr");
            BeginWriteAttribute("style", " style=\"", 982, "\"", 1057, 6);
            WriteAttributeValue("", 990, "width:200px;", 990, 12, true);
            WriteAttributeValue(" ", 1002, "height:", 1003, 8, true);
#nullable restore
#line 31 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 1010, heightCustom.ToString(), 1010, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1034, "px;", 1034, 3, true);
            WriteAttributeValue(" ", 1037, "vertical-align:", 1038, 16, true);
            WriteAttributeValue(" ", 1053, "top", 1054, 4, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 32 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                     for (int col = 0; col < lengthContainer; col++)
                    {
                        string idPallet = row.ToString() + "," + col.ToString();
                        var currentPallets = palletsAux.Where(x => x.Row == row && x.Column == col).OrderByDescending(x => x.OrderNo);

                        if (currentPallets != null)
                        {
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <td ondrop=\"drop(event)\" ondragover=\"allowDrop(event)\"");
            BeginWriteAttribute("style", " style=\"", 2272, "\"", 2348, 6);
            WriteAttributeValue("", 2280, "width:200px;", 2280, 12, true);
            WriteAttributeValue(" ", 2292, "height:", 2293, 8, true);
#nullable restore
#line 46 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 2300, heightCustom.ToString(), 2300, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2324, "px;", 2324, 3, true);
            WriteAttributeValue(" ", 2327, "vertical-align:", 2328, 16, true);
            WriteAttributeValue(" ", 2343, "top;", 2344, 5, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                <input type=\"button\"");
            BeginWriteAttribute("id", " id=\"", 2404, "\"", 2418, 1);
#nullable restore
#line 47 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 2409, idPallet, 2409, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" value=\"Add\"");
            BeginWriteAttribute("style", " style=\"", 2431, "\"", 2485, 4);
            WriteAttributeValue("", 2439, "width:100%;", 2439, 11, true);
            WriteAttributeValue(" ", 2450, "height:", 2451, 8, true);
#nullable restore
#line 47 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 2458, heightCustom.ToString(), 2458, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2482, "px;", 2482, 3, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"addBtn btn-secondary\">\r\n");
#nullable restore
#line 48 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                                 foreach (var palet in currentPallets)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <input draggable=\"true\" ondragstart=\"drag(event)\" type=\"button\"");
            BeginWriteAttribute("id", " id=\"", 2724, "\"", 2738, 1);
#nullable restore
#line 50 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 2729, palet.Id, 2729, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2739, "\"", 2761, 1);
#nullable restore
#line 50 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 2747, palet.OrderNo, 2747, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 2762, "\"", 2816, 4);
            WriteAttributeValue("", 2770, "width:100%;", 2770, 11, true);
            WriteAttributeValue(" ", 2781, "height:", 2782, 8, true);
#nullable restore
#line 50 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 2789, heightCustom.ToString(), 2789, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2813, "px;", 2813, 3, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"removeBtn btn-danger\">\r\n");
#nullable restore
#line 51 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </td>\r\n");
#nullable restore
#line 53 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <input type=\"button\"");
            BeginWriteAttribute("id", " id=\"", 3051, "\"", 3065, 1);
#nullable restore
#line 56 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
WriteAttributeValue("", 3056, idPallet, 3056, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" value=\"Add\" style=\"width:100%; height:100%; text-align:center\" class=\"addBtn btn-secondary\">\r\n");
#nullable restore
#line 57 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                        }



                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                                                            
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tr>\r\n");
#nullable restore
#line 79 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>
<br />

<div style=""display:flex; justify-content:space-between"">
    <div class=""mb-2 mr-2"">
        <input type=""button"" value=""New row"" class=""btn btn-dark"" id=""newRow"" />
        <input type=""button"" value=""Remove row"" class=""btn btn-danger"" id=""removeRow"" />
    </div>

    <div class=""mb-2 mr-2"">
        <input type=""button"" value=""New column"" class=""btn btn-dark"" id=""newColumn"" />
        <input type=""button"" value=""Remove column"" class=""btn btn-danger"" id=""removeColumn"" />
    </div>
</div>

<br />

<div style=""width:100%"" align=""center"">
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bc6772e322cd4b18dcbab083866ddeedf035dd2b15465", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bc6772e322cd4b18dcbab083866ddeedf035dd2b15732", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 101 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bc6772e322cd4b18dcbab083866ddeedf035dd2b17458", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 103 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Pallet.Name);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bc6772e322cd4b18dcbab083866ddeedf035dd2b19062", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 104 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Pallet.Name);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bc6772e322cd4b18dcbab083866ddeedf035dd2b20747", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 105 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Pallet.Name);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </div>\r\n        <!--<div class=\"form-group\">\r\n            <label asp-for=\"Pallet.PalletType\" class=\"control-label\"></label>-->\r\n");
                WriteLiteral(@"        <!--<select asp-for=""Pallet.PalletType"" class=""form-control"" asp-items=""ViewBag.PalletId""></select>
        </div>
        <div class=""form-group"" style=""text-align:right"">
            <input type=""submit"" value=""Add"" class=""btn btn-success"" />
        </div>-->
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n<input type=\"button\" value=\"Sincronizare paleti\" class=\"btn btn-dark\" id=\"sincData\" />\r\n<div id=\"tableSection\">\r\n\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 124 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    <script>
    function allowDrop(ev) {
        ev.preventDefault();
    }

    function drag(ev) {
        ev.dataTransfer.setData(""drag"", ev.target.id);
    }

    function drop(ev) {
        ev.preventDefault();
        var draggedPallet = ev.dataTransfer.getData(""drag"");
        ev.target.appendChild(document.getElementById(draggedPallet));

        var idContainer = $('#container').val();
        var droppedPallet = ev.target.id;
        var path = '");
#nullable restore
#line 142 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
               Write(Url.Action("SwitchPallets"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' + ""?idContainer="" + idContainer + ""&draggedPallet="" + draggedPallet + ""&droppedPallet="" + droppedPallet;
        window.location.href = path;
    }

    $(""#newRow"").click(function () {
        var idContainer = $('#container').val();
        var path = '");
#nullable restore
#line 148 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
               Write(Url.Action("AddRow"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\' + \"?idContainer=\" + idContainer;\r\n        window.location.href = path;\r\n    });\r\n\r\n    $(\"#sincData\").click(function () {\r\n        var idContainer = $(\'#container\').val();\r\n        var containerName = \"");
#nullable restore
#line 154 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                        Write(containerName);

#line default
#line hidden
#nullable disable
                WriteLiteral("\";\r\n        var countryName = \"");
#nullable restore
#line 155 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
                      Write(countryName);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""";
        $.ajax({
            url: ""/Pallets/GetPallets"",

            data: {
                country: ''+countryName, container: ''+containerName, containerId: idContainer
            },

            cache: false,
            contentType: ""application/json;charset=UTF-8"",
            dataType: ""json"",
            success: function (data) {
                //console.log(data)
                var noPallets = data.listPallets.length;
                var tableTxt = ""<table class='table' style='margin-top:25px;'><tr><th>Order no</th><td>Sales part</td><td>Pallet no</td><td>Serial from</td><td>Serial to</td><td>Quantity</td><td>Weight</td></tr>""
                for (var i = 0; i < noPallets; i++) {
                    var orderNo = data.listPallets[i].orderNoApp;
                    if (orderNo == -1) orderNo = ""N/A""
                    tableTxt = tableTxt + ""<tr><td>"" + orderNo + ""</td>"" 
                        + ""<td>"" + data.listPallets[i].palletMap.salse_part +""</td>""
                  ");
                WriteLiteral(@"      + ""<td>"" + data.listPallets[i].palletMap.pallet_no +""</td>""
                        + ""<td>"" + data.listPallets[i].palletMap.serial_from +""</td>""
                        + ""<td>"" + data.listPallets[i].palletMap.serial_to +""</td>""
                        + ""<td>"" + data.listPallets[i].palletMap.picking_qty +""</td>""
                        + ""<td>"" + data.listPallets[i].palletMap.weight +""</td></tr>""
                }
                tableTxt = tableTxt + ""</table>""
                $(""#tableSection"").append(tableTxt);
            }
        });
    })

    $(""#removeRow"").click(function () {
    var idContainer = $('#container').val();
    var path = '");
#nullable restore
#line 189 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
           Write(Url.Action("RemoveRow"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\' + \"?idContainer=\" + idContainer;\r\n    window.location.href = path;\r\n    });\r\n\r\n    $(\"#newColumn\").click(function () {\r\n    var idContainer = $(\'#container\').val();\r\n    var path = \'");
#nullable restore
#line 195 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
           Write(Url.Action("AddColumn"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\' + \"?idContainer=\" + idContainer;\r\n    window.location.href = path;\r\n    });\r\n\r\n    $(\"#removeColumn\").click(function () {\r\n    var idContainer = $(\'#container\').val();\r\n    var path = \'");
#nullable restore
#line 201 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
           Write(Url.Action("RemoveColumn"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' + ""?idContainer="" + idContainer;
    window.location.href = path;
    });

    $("".addBtn"").click(function () {
        var position = this.id;
        var idContainer = $('#container').val();
        var palletName = $('#palletName').val();
        var path = '");
#nullable restore
#line 209 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
               Write(Url.Action("AddPallet"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' + ""?idContainer="" + idContainer + ""&position="" + position + ""&palletName="" + palletName;
        window.location.href = path;
    });

    $("".removeBtn"").click(function () {
        var palletId = this.id;
        var idContainer = $('#container').val();
        var path = '");
#nullable restore
#line 216 "C:\Users\r.tanase\Source\Repos\PalletLoading\PalletLoading\Views\Pallets\Create.cshtml"
               Write(Url.Action("RemovePallet"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\' + \"?idContainer=\" + idContainer + \"&palletId=\" + palletId;\r\n        window.location.href = path;\r\n    });\r\n    </script>\r\n");
            }
            );
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PalletLoading.ViewModels.AddContainerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
