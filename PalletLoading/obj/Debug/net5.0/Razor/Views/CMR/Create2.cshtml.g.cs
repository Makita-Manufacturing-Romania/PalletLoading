#pragma checksum "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6270cc990a7cf80783f9856664a1eff2abca1cbc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CMR_Create2), @"mvc.1.0.view", @"/Views/CMR/Create2.cshtml")]
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
#line 1 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\_ViewImports.cshtml"
using PalletLoading;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\_ViewImports.cshtml"
using PalletLoading.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6270cc990a7cf80783f9856664a1eff2abca1cbc", @"/Views/CMR/Create2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85b146527b5e52069227f21d355af491abfa5380", @"/Views/_ViewImports.cshtml")]
    public class Views_CMR_Create2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PalletLoading.Models.CmrData>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("fieldFromModel"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Images/Stamp1.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString(" top: 5px; left: 310px; width: 150px; height: 60px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Images/Stamp2.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString(" top: 5px; left: 110px; width: 150px; height: 60px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
   
    var today = DateTime.Now.ToString("dd/MM/yyyy");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row redTr parent"" style=""height:70px"">
    <div class=""col-md-1 number"">1</div>
    <div class=""col-md-5 verticalLine content currentCell"">
        Sender (name, address, country)
        <br />
        Absender (Name, Anschrift, Land)
        <br />
        Nadawca (nazwisko lub nazwa, adres, kraj)
    </div>
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6270cc990a7cf80783f9856664a1eff2abca1cbc5590", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <br />\r\n");
            WriteLiteral(@"
<table style="" table-layout:fixed; width:100%; left:0px; border:1px solid red; text-align:left;"">
    <tr class=""currentCell"">
        <td class=""content"">
            INTERNATIONAL CONSIGNMENT
            <br />

        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr class=""currentCell"">
    </tr>
    <tr class=""currentCell"">
    </tr>
    <tr class=""currentCell"">
    </tr>
</table>
</div>

<div class=""row redTr parent"" style=""height:150px"">
    <div class=""col-md-1 number"">2</div>
    <div class=""col-md-5 verticalLine content currentCell"">
        Consignee (name, address, country)
        <br />
        Empfanger (Name, Anschrift, Land)
        <br />
        Odbiorca (nazwisko lub nazwa, adres, kraj)
    </div>
    <div class=""fieldFromModel"" style=""top: 1px; left: 280px; width: 200px; height: 80px;"">
        ");
#nullable restore
#line 56 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
   Write(Model.Adress);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <div class=""col-md-1 number"" style=""border-top:2px solid; border-left:2px solid"">16</div>
    <div class=""col-md-5 content"" style=""border-top:2px solid; border-right:2px solid"">
        Carrier (name, address, country)
        <br />
        Frachtfuhrer (Name, Anschrift, Land)
        <br />
        Przewoznik (nazwisko lub nazwa, adres, kraj)
        <br />
        <br />
        <div class=""number"">REG. NO.:</div>
    </div>
</div>

<div class=""row redTr parent"" style=""height:150px"">
    <div class=""col-md-1 number"">3</div>
    <div class=""col-md-5 content verticalLine currentCell"">
        Place of delivery of the goods (place, country)
        <br />
        Auslieferungsort des Gutes (Ort, Land)
        <br />
        Miejsce przeznaczenia (miejscowosc, kraj)
    </div>
    <div class=""fieldFromModel"" style=""top: 1px; left: 280px; width: 200px; height: 50px;"">
        ");
#nullable restore
#line 81 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
   Write(Model.Adress);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <div class=""col-md-1 number"" style=""border-left: 2px solid"">17</div>
    <div class=""col-md-5 content"" style=""border-right: 2px solid"">
        Successive carriers (name, address, country)
        <br />
        Nachfolgende Frachtfuhrer (Name, Anschrift, Land)
        <br />
        Kolejni przewoznicy (nazwisko lub nazwa, adres, kraj)
    </div>
</div>

<div class=""row redTr parent"" style=""height:140px"">
    <div class=""col-md-1 number"">4</div>
    <div class=""col-md-5 content verticalLine currentCell"">
        Place and date taking over the goods (place, country, data)
        <br />
        Ort und Tag der Ubernahme das Gutes (Ort, Land, Datum)
        <br />
        Miejsce i data zaladowania (miejscowosc, kraj, data)
    </div>
    <div class=""fieldFromModel"" style=""top: 1px; left: 345px; width: 90px; height: 20px;"">
        BRANESTI ROMANIA - ");
#nullable restore
#line 103 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                      Write(today);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <br />
    <div class=""col-md-1 number"" style=""border-left: 2px solid;"">18</div>
    <div class=""col-md-5 content"" style=""border-right: 2px solid"">
        Carrier’s reservations and observations
        <br />
        Vorbehalte und Bemerkungen der Frachtfuhrer
        <br />
        Zastrzezenia i uwagi przewoznika
    </div>
    <div class=""fieldFromModel"" style=""top: 70px; left: 600px;"">
        AUTO: ");
#nullable restore
#line 115 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
         Write(Model.LicensePlate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <br />\r\n        DRIVER: ");
#nullable restore
#line 117 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
           Write(Model.Driver);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <div class=""col-md-1 number"" style=""border-top-style:solid;"">5</div>
    <div class=""col-md-5 content verticalLine currentCell"" style=""border-top-style:solid; border-right:2px solid"">
        Documents attached
        <br />
        Beigefugte Dokumente
        <br />
        Zalaczone dokumenty
    </div>
    <div class=""fieldFromModel"" style=""top: 70px; left: 200px; width: 230px; height: 20px;"">
        PACKING LIST - ");
#nullable restore
#line 128 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                  Write(Model.PackingList);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
</div>

<div class=""row redTr parent"" style=""height:200px"">
    <table style="" table-layout:fixed; width:100%; left:0px; border:1px solid red; text-align:left;"">
        <tr class=""currentCell"">
            <td class=""number"">6</td>
            <td class=""content"" style=""text-align:left"">
                Marks and Nos
                <br />
                Kennzeichen und Nammern
                <br />
                Cechy i numery
            </td>
            <div class=""fieldFromModel"" style=""top:70px; left:20px"">
                ");
#nullable restore
#line 144 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
           Write(Model.NoOfTools);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <br />\r\n");
#nullable restore
#line 146 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                  
                    var number = Model.NoOfSpr.Split(" ").First();
                    if (number == "0")
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div>\r\n\r\n                        </div>\r\n");
#nullable restore
#line 153 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 156 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                   Write(Model.NoOfSpr);

#line default
#line hidden
#nullable disable
#nullable restore
#line 156 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                                      
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <br />\r\n                <br />\r\n                <br />\r\n                SEAL NO: ");
#nullable restore
#line 163 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
                    Write(Model.SerialNo);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>

            <td class=""number"">7</td>
            <td class=""content"" style=""text-align:left"">
                Number of packages
                <br />
                Anzahl der Packstucke
                <br />
                Ilosc sztuk
            </td>
            <td class=""number"">8</td>
            <td class=""content"" style=""text-align:left"">
                Method of packing
                <br />
                Art der Verpackung
                <br />
                Sposob opakowania
            </td>
            <td class=""number"">9</td>
            <td class=""content verticalLine"" style=""text-align:left"">
                Nature of the goods
                <br />
                Bezeichnung des Gutes
                <br />
                Rodzaj towaru
            </td>
            <td class=""number"">10</td>
            <td class=""content verticalLine"" style=""text-align:left"">
                Statistical number
                <br />
       ");
            WriteLiteral(@"         Statistiknummer
                <br />
                Numer statystyczny
            </td>
            <td class=""number"">11</td>
            <td class=""content verticalLine"" style=""text-align:left"">
                Gross weight in kg
                <br />
                Bruttogewicht in kg
                <br />
                Waga brutto w kg
            </td>
            <div class=""fieldFromModel"" style=""top: 130px; left: 700px;"">
                ");
#nullable restore
#line 207 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
           Write(Model.Weight);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" KG
            </div>
            <td class=""number"">12</td>
            <td class=""content verticalLine"" style=""text-align:left"">
                Volume in m3
                <br />
                Umfang m3
                <br />
                Objetosc w m3
            </td>
        </tr>
    </table>
</div>

<div class=""row redTr"">
    <div class=""col-md-1 content"">
        Class
        <br />
        Klasse
        <br />
        Klasa
    </div>

    <div class=""col-md-1""></div>

    <div class=""col-md-1 content"">
        Number
        <br />
        Ziffer
        <br />
        Liczba
    </div>

    <div class=""col-md-1""></div>

    <div class=""col-md-auto content"">
        Letter
        <br />
        Buchstabe
        <br />
        Litera
    </div>

    <div class=""col-md-1""></div>

    <div class=""col-md-auto content"">
        (ADR*)
    </div>
</div>

<div class=""row redTr"">
    <div class=""col-md-1 number"">13</div>
    <div class=""col-md");
            WriteLiteral(@"-5 content"" style=""border-right:2px solid"">
        Senders instructions
        <br />
        Anweisungen des Absenders
        <br />
        Instrukcje nadawcy
    </div>

    <br />

    <div class=""col-md-1 number"" style=""border-bottom:2px solid"">19</div>
    <div class=""col-md-5 content"" style=""border-bottom:2px solid"">
        Special agreements
        <br />
        Besondere Vereinbarungen
        <br />
        Postanowienia specjalne
    </div>

    <div class=""col-md-1 number"" style=""border-top:2px solid"">14</div>
    <div class=""col-md-5 content"" style=""border-right:2px solid; border-top:2px solid"">
        Instruction as to payment for carriage
        <br />
        Frechtzahlungsanweisungen
        <br />
        Postanowienia odnosnie przewoznego
        <br />
        <br />
        Carriage paid / Przewozne zaplacone / Frei
        <br />
        Carriage forward / Przewozne nieoplacone / Unfrei
    </div>

    <div class=""col-md-6"" style=""border: 2px soli");
            WriteLiteral(@"d red;"">
        <table class=""table-bordered"" style=""text-align:center; table-layout:fixed; width:100%; left:0px; border:1px solid red"">
            <tbody>
                <tr>
                    <td>
                        <div class=""number"">20</div>
                        <div class=""content"">
                            To be paid by
                            <br />
                            Zu zahlen vom
                            <br />
                            Do zaplacenias
                        </div>
                    </td>
                    <td>
                        <div class=""content"">
                            Sender
                            <br />
                            Absender
                            <br />
                            Nadawca
                        </div>
                    </td>
                    <td></td>
                    <td colspan=""2"">
                        <div class=""content"">
                      ");
            WriteLiteral(@"      Currency
                            <br />
                            Wahrung
                            <br />
                            Waluta
                        </div>
                    </td>
                    <td>
                        <div class=""content"">
                            Consignee
                            <br />
                            Empfanger
                            <br />
                            Odbiorca
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td rowspan=""2"">
                        <div class=""content"">
                            Carriage charges
                            <br />
                            / Przewozne / Fracht
                            <br />
                            Deductions / Ermassigungen
                            / Bonifikaty
                        </div>
                    </td>");
            WriteLiteral(@"
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td rowspan=""3"">
                        <div class=""content"">
                            Balance / Zuschlage
                            <br />
                            / Saldo
                            <br />
                            Supplem. charges /
                            <br />
                            Nebengebuhren / Doplaty
                            <br />
                            Miscellaneous / Sonstiges /
                            <br />
                            Koszty dodatkowe
     ");
            WriteLiteral(@"                   </div>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class=""content"">Insurance</div>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
       ");
            WriteLiteral(@"             <td></td>
                </tr>
                <tr>
                    <td>
                        <div class=""content"">
                            Total to be paid /
                            <br />
                            Gesamtsumme / Razem
                        </div>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>

</div>

<div class=""row redTr parent"">
    <div class=""col-md-1 number"">21</div>
    <div class=""col-md-3 content currentCell"">
        Established in
        <br />
        Ausgefertigt in
        <br />
        Wystawiono w
    </div>
    <div class=""fieldFromModel"" style=""left:200px"">
        BRANESTI
    </div>
    <div class=""fieldFromModel"" style=""left:370px"">
        ");
#nullable restore
#line 446 "C:\Users\r.cioroba\source\repos\PalletLoading\PalletLoading\Views\CMR\Create2.cshtml"
   Write(today);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <div class=""col-md-2 content verticalLine"">
        on
        <br />
        am
        <br />
        dnia
    </div>

    <div class=""col-md-1 number"">15</div>
    <div class=""col-md-5 content"">
        Cash on delivery
        <br />
        Ruckerstattung
        <br />
        Zaplata
    </div>
</div>

<div class=""row redTr parent"" style=""border-bottom:2px solid"">
    <div class=""col-md-4 verticalLine"">
        <div class=""col-md-1 number currentCell"">22</div>
        <br />
        <br />
        <div class=""content"">
            Signature and stamp of the sender
            <br />
            Unterschrift und Stempel des Absenders
            <br />
            Podpis i stempel nadawcy
        </div>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6270cc990a7cf80783f9856664a1eff2abca1cbc24560", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>

    <div class=""col-md-4 verticalLine"" style=""border:2px solid red"">
        <div class=""col-md-auto number"">23</div>
        <br />
        <br />
        <div class=""content"">
            Signature and stamp of the carrier
            <br />
            Unterschrift und Stempel des Frachtfuhrers
            <br />
            Podpis i stempel przewoznika
        </div>
    </div>

    <div class=""col-md-4"">
        <div class=""col-md-1 number"">24</div>
        <div class=""col-md-3 content"">
            Goods received / Gut empfangen
            <br />
            / Przesylke otrzymano
        </div>
        <div class=""content"">

        </div>
    </div>
</div>








<br />

");
            WriteLiteral(@"


<style>
    .redTable {
        max-width: 2480px;
        width: 100%;
        border: 2px solid;
        border-color: red;
        color:red;
        border:solid;
    }

    .redTr {
        border-top-style: solid;
        border-right-style:solid;
        border-left-style:solid;
        border-bottom-style:hidden;
        border-color: red;
        text-decoration-color: red;
        color:red;
    }

    .redTd {
        border: 2px solid;
        border-color: red;
        text-decoration-color: red;
        color: red;
    }

    .verticalLine {
        border-right: 2px solid red;
    }

    .number {
        font-size: x-large;
        font-weight: 700;
    }

    .content {
        font-size:9px;
    }
    .parent {
        position: relative;
        top: 0;
        left: 0;
    }

    .currentCell {
        position: relative;
        top: 0;
        left: 0;
    }

    .fieldFromModel {
        position: absolute;
        color:black;
            WriteLiteral("\n        font-weight:bold;\r\n    }\r\n</style>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PalletLoading.Models.CmrData> Html { get; private set; }
    }
}
#pragma warning restore 1591