#pragma checksum "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Package\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36b66a56864a77117f5f46ba2921b2030b1f2c39"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Package_Index), @"mvc.1.0.view", @"/Views/Package/Index.cshtml")]
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
#line 1 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Package\Index.cshtml"
using Micro.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36b66a56864a77117f5f46ba2921b2030b1f2c39", @"/Views/Package/Index.cshtml")]
    public class Views_Package_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/select2/css/select2.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Pages/Package.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/app-assets/vendors/toastr/toastr.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/select2/js/select2.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/app-assets/vendors/toastr/toastr.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/moment/moment.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 2 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Package\Index.cshtml"
  

    Dictionary<int, bool> dicRole = GetUserRole.GetRoleAddByUser();
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
    ViewBag.Title = "Quản lý gói cước";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("css", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "36b66a56864a77117f5f46ba2921b2030b1f2c395750", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "36b66a56864a77117f5f46ba2921b2030b1f2c396928", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "36b66a56864a77117f5f46ba2921b2030b1f2c398106", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral(@"
<div class=""card"">
    <div class=""card-header"">
        <h4 class=""card-title "">Quản lý gói cước</h4>
    </div>
    <div class=""card-body"">
        <div class=""row mb-2"">
            <div class=""col-md-4"">
                <input class=""form-control"" placeholder=""Tìm kiếm"" id=""txtsearch"" />
            </div>
            <div class=""col-md-4"">
                <select class=""form-control"" id=""filterstatus"">
                    <option value=""-1"">Tất cả gói cước</option>
                    <option value=""0"">Còn hiệu lực</option>
                    <option value=""1"">Hết hiệu lực</option>
                </select>
            </div>
            <div class=""col-md-4"">
                <button class=""btn btn-primary"" id=""btnsearch""><i class=""fa fa-search""></i> Tìm kiếm</button>
                <button class=""btn btn-primary"" id=""btnOpenModalPackage"" style=""margin-left:2px"" data-toggle=""modal"" data-target=""#CreatePackage"" ");
#nullable restore
#line 33 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Package\Index.cshtml"
                                                                                                                                              Write(dicRole[(int)MobiFiber.Code.ActionModule.PackageManager] == true ? "" : "disabled");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"><i class=""fa fa-plus""></i> Tạo mới</button>
            </div>
        </div>
        <div class=""btnCssPackage"">

        </div>
        <div>
            <table id=""tblPackage"" class=""table-hover table-striped ""></table>
        </div>
    </div>
</div>
<div");
            BeginWriteAttribute("class", " class=\"", 1724, "\"", 1732, 0);
            EndWriteAttribute();
            WriteLiteral(@" id=""loadingpage""></div>
<div class=""modal fade"" id=""CreatePackage"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"" data-backdrop=""static"" data-keyboard=""false"">
    <div class=""modal-dialog modal-dialog-scrollable"" id=""FormPackage"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Tạo mới gói cước</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
                <input id=""Packageid"" value=""0"" hidden />
            </div>
            <ul class=""nav nav-tabs"" id=""tabsPackage"" role=""tablist"">
                <li class=""nav-item"">
                    <a class=""nav-link active"" id=""btn-step1"" data-toggle=""tab"" aria-controls=""step_1"" href=""#step_1"" role=""tab"" aria-selected=""false"">
                        Thêm mới
                        <span");
            WriteLiteral(@" class=""badge badge-success badge-pill badge-round float-right""></span>
                    </a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" id=""btn-step2"" data-toggle=""tab"" aria-controls=""step_2"" href=""#step_2"" role=""tab"" aria-selected=""false"">
                        Thêm mới bằng File
                        <span class=""badge badge-warning badge-pill badge-round totalComment float-right""></span>
                    </a>
                </li>
            </ul>
            <div class=""tab-content"">
                <div class=""tab-pane active"" id=""step_1"" role=""tabpanel"" aria-labelledby=""step_1"">
                    <div class=""modal-body"">
                        <h5 class=""text-danger"">
                            Vui lòng nhập các trường bắt buộc (*)
                        </h5>
                        <div class=""input-group"">
                            <div class=""col-md-6"">
                                <label for=""recipient-");
            WriteLiteral(@"name"" class=""col-form-label"">Tên gói cước<span style=""color:red""> (*) </span>: </label>
                                <input type=""text"" class=""form-control"" id=""AddPackageName"" name=""Title"" maxlength=""100"">
                            </div>
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Mã gói cước <span style=""color:red"">(*) </span>: </label>
                                <input type=""text"" class=""form-control"" id=""PackageNumber"" name=""PackageNumber"" maxlength=""50"">
                            </div>
                        </div>
                        <div class=""input-group"">
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Số công văn quyết định <span style=""color:red"">(*) </span>: </label>
                                <input type=""text"" class=""form-control"" id=""Decision"" name=""Decision"" maxlength=""100"">
         ");
            WriteLiteral(@"                   </div>
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Thời gian sử dụng <span style=""color:red""> ( Đơn vị tháng *) </span>: </label>
                                <input class=""form-control"" id=""TimeUsed"" name=""TimeUsed"" maxlength=""4"" onkeypress='return event.charCode >= 48 && event.charCode <= 57' onblur=""common.RemoveSpecialChar(this)"">
                            </div>
                        </div>
                        <div class=""input-group"">
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Thời gian khuyến mãi <span style=""color:red""> ( Đơn vị tháng *) </span>: </label>
                                <input class=""form-control"" id=""PromotionTime"" name=""PromotionTime"" maxlength=""4"" onkeypress='return event.charCode >= 48 && event.charCode <= 57' onblur=""common.RemoveSpecialChar(this)"">
             ");
            WriteLiteral(@"               </div>
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Giá gói cước <span style=""color:red""> ( Chưa bao gồm VAT *) </span>: </label>
                                <input class=""form-control"" id=""PricePackage"" name=""PricePackage"" maxlength=""16"" onkeypress='return event.charCode >= 48 && event.charCode <= 57' onblur=""common.RemoveSpecialChar(this)"">
                            </div>
                        </div>
                        <div class=""input-group"">
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Thuế VAT<span style=""color:red""> ( Đơn vị phần trăm % *) </span>: </label>
                                <input class=""form-control"" id=""PackageVAT"" name=""PackageVAT"" maxlength=""5"" type=""number"">
                            </div>
                            <div class=""col-md-6"">
                         ");
            WriteLiteral(@"       <label for=""recipient-name"" class=""col-form-label"">Giá gói cước<span style=""color:red""> ( Bao gồm VAT *) </span>: </label>
                                <input type=""number"" min=""0"" class=""form-control"" id=""PricePackageVAT"" name=""PricePackageVAT"" maxlength=""16"" onkeypress='return event.charCode >= 48 && event.charCode <= 57' readonly>
                            </div>
                        </div>
                        <div class=""input-group"">
                            <div class=""col-md-6"">
                                <label for=""recipient-name"" class=""col-form-label"">Trạng thái <span style=""color:red""> (*) </span>: </label>
                                <select name=""PackageStatus"" id=""PackageStatus"" class=""form-control"">
                                    <option value=""0"">Còn hiệu lực</option>
                                    <option value=""1"">Hết hiệu lực</option>
                                </select>
                            </div>
                        </d");
            WriteLiteral(@"iv>
                        <div class=""form-group col-md-12 d-flex mt-2"">
                            <div class=""col-md-offset-2 mr-1"">
                                <button name=""btnAddPackage"" id=""btnAddPackage"" class=""btn btn-primary"">Tạo</button>
                            </div>
                            <div class=""col-md-offset-2"">
                                <input type=""button"" data-dismiss=""modal"" aria-label=""Close"" name=""btnClosePackage"" value=""Đóng"" id=""btnClosePackage"" class=""btn btn-danger"">
                            </div>
                        </div>
                    </div>

                </div>
                <div class=""tab-pane"" id=""step_2"" role=""tabpanel"" aria-labelledby=""step_2"">
                    <div class=""modal-body"">
                        <div id=""divUpload"">

                            <div class=""mb-1 d-flex"">
                                <div class=""col-md-10"">
                                    <input type=""file"" id=""fileupload"" nam");
            WriteLiteral(@"e=""files"" class=""form-control mb-1"" />
                                    <span class=""text-danger""> ( Vui lòng không sửa dụng công thức trong file Exel )</span>

                                </div>
                                <div class=""col-md-2"">
                                    <input type=""button"" onclick=""window.location.href='/Package/DownloadTemplateDevice'"" name=""DownloadTemplate"" value=""Tải file mẫu"" id=""btnDownloadTemplate"" class=""btn btn-primary "" />
                                </div>
                            </div>
                            <div class=""col-md-4"">
                                <button name=""Upload"" id=""btnupload"" class=""btn btn-primary"">Tải lên</button>
                                <input type=""button"" data-dismiss=""modal"" aria-label=""Close"" value=""Đóng"" class=""btn btn-danger"">
                            </div>
                        </div>
                        <div id=""divPreview"" style=""display:none"">
                            <div c");
            WriteLiteral(@"lass=""tabstablecss"">
                                <table id=""tblPackageValid"" class=""table-hover table-striped""></table>
                            </div>
                            <button name=""SaveData"" id=""btnSaveData"" class=""btn btn-primary my-1"">Lưu</button>
                            <div class=""tabstablecss"">
                                <table id=""tblPackageInValid"" class=""table-hover table-striped""></table>
                            </div>
                            <input type=""button"" name=""DownloadError"" value=""Download Error"" id=""btnDownloadError"" class=""btn btn-primary my-1"" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""modal fade"" id=""ViewDetailPackage"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-scrollable"" id=""FormViewDetailPackage"" role=""document"">
        <div class=""modal-content"">
          ");
            WriteLiteral(@"  <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Lịch sử sử dụng gói cước gói cước</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
                <input id=""Packageid"" value=""0"" hidden />
            </div>
            <div class=""modal-body"">
                <form>
                    <div class=""input-group"">
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label""><b>Tên gói cước : </b></label>
                            <span id=""PackageNameDetail""></span>
                        </div>
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label""><b>Gía gói cước ( Chưa bao gồm VAT ) : </b></label>
                            <span id=""PriceDetail""></span>
                        ");
            WriteLiteral(@"</div>
                    </div>
                    <div class=""input-group"">
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label""><b>Gía gói cước ( Bao gồm VAT ) : </b></label>
                            <span id=""PriceVATDetail""></span>
                        </div>
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label""><b>Thời gian sử dụng : </b></label>
                            <span id=""TmeUsedDetail""></span>
                        </div>
                    </div>
                    <div class=""input-group"">
                        <div class=""col-md-12"">
                            <label for=""recipient-name"" class=""col-form-label""><b>Các hợp đồng sử dụng gói cước : </b></label>
                            <table id=""tblPackageDetail"" class=""table-hover mb0 table-borderless""></table>
                        </div>
                    <");
            WriteLiteral(@"/div>
                    <div class=""form-group col-md-12 d-flex mt-2"">
                        <div class=""col-md-offset-2"">
                            <input type=""button"" data-dismiss=""modal"" aria-label=""Close"" name=""btnCloseViewDetail"" value=""Đóng"" id=""btnCloseViewDetail"" class=""btn btn-danger"">
                        </div>
                    </div>

                </form>
            </div>

        </div>
    </div>
</div>

");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36b66a56864a77117f5f46ba2921b2030b1f2c3923857", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36b66a56864a77117f5f46ba2921b2030b1f2c3924957", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 13544, "~/js/Package.js?v=", 13544, 18, true);
#nullable restore
#line 223 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Package\Index.cshtml"
AddHtmlAttributeValue("", 13562, DateTime.Now.ToString("yyyyMMddHHmmss"), 13562, 40, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36b66a56864a77117f5f46ba2921b2030b1f2c3926563", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36b66a56864a77117f5f46ba2921b2030b1f2c3927663", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
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
