#pragma checksum "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4005fd189cc9d0b6190d2919974b20ad0cd09482"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Setting_UserManager), @"mvc.1.0.view", @"/Views/Setting/UserManager.cshtml")]
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
#line 1 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
using MobiFiber.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4005fd189cc9d0b6190d2919974b20ad0cd09482", @"/Views/Setting/UserManager.cshtml")]
    public class Views_Setting_UserManager : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MobiFiber.Models.Account>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/select2/css/select2.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Pages/UserManager.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/app-assets/vendors/toastr/toastr.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap-datepicker/css/bootstrap-datepicker3.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/select2/js/select2.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/UserManager.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/app-assets/vendors/toastr/toastr.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap-datepicker/js/bootstrap-datepicker.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/moment/moment.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
  
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
    ViewBag.Title = "Quản lý tài khoản";
    List<RoleGroup> roleGroup = new List<RoleGroup>();
    if (ViewData["roleGroup"] != null)
    {
        roleGroup = (List<RoleGroup>)ViewData["roleGroup"];
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("css", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4005fd189cc9d0b6190d2919974b20ad0cd094827064", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4005fd189cc9d0b6190d2919974b20ad0cd094828242", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4005fd189cc9d0b6190d2919974b20ad0cd094829420", async() => {
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
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4005fd189cc9d0b6190d2919974b20ad0cd0948210598", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    \r\n");
            }
            );
            WriteLiteral(@"
<div class=""card"">
    <div class=""card-header"">
        <h4 class=""card-title"">Quản lý tài khoản</h4>
    </div>
    <div class=""card-body"">
        <div class=""btnCssUserManager"">
            <button class=""btn btn-primary"" id=""btnOpenModalUser""><i class=""fa fa-plus""></i> Tạo mới</button>
        </div>
        <div>
            <table id=""tblUserManager"" class=""table-hover table-striped""></table>
        </div>
    </div>
</div>
<div class=""modal fade"" id=""ChangePassword"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-scrollable""");
            BeginWriteAttribute("id", " id=\"", 1283, "\"", 1288, 0);
            EndWriteAttribute();
            WriteLiteral(@" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Đổi mật khẩu</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
");
#nullable restore
#line 44 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                 using (Html.BeginForm("AdminChangePass", "Setting", FormMethod.Post, new { @class = "form-horizontal form-simple", role = "form", id = "frm" }))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <input type=\"hidden\" name=\"Id\" id=\"IdResetPassword\"");
            BeginWriteAttribute("value", " value=\"", 1960, "\"", 1968, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    <input type=\"hidden\" name=\"UserName\" id=\"UserNameResetPassword\"");
            BeginWriteAttribute("value", " value=\"", 2057, "\"", 2065, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    <div class=""text-danger font-weight-500"" id=""Message"">

                    </div>
                    <div class=""input-group"">
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""NewPassword"" class=""col-form-label"">Mật khẩu mới:</label>
                                    <input type=""password"" name=""NewPassword"" id=""NewPassword""");
            BeginWriteAttribute("value", " value=\"", 2583, "\"", 2591, 0);
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 2592, "\"", 2606, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"">
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""ConfirmPassword"" class=""col-form-label"">Nhập lại mật khẩu mới:</label>
                                    <input type=""password"" name=""ConfirmPassword"" id=""ConfirmPassword""");
            BeginWriteAttribute("value", " value=\"", 3125, "\"", 3133, 0);
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 3134, "\"", 3148, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row float-right"">
                        <input type=""button"" id=""btnresetpassword"" class=""btn btn-primary mr-3"" name=""name"" value=""Đổi mật khẩu"">
                    </div>
");
#nullable restore
#line 72 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
</div>
<div class=""modal fade"" id=""EditUser"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-xs"" id=""FormUserManager"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Sửa thông tin tài khoản</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
");
#nullable restore
#line 87 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                 using (Html.BeginForm("EditRole", "Account", FormMethod.Post, new { @class = "form-horizontal form-simple", role = "form", id = "frm" }))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <input id=""Id"" name=""Id"" type=""hidden"" />
                    <div class=""input-group"">
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label"">Tên tài khoản đăng nhập<span style=""color:red""></span>: </label>
                            <input type=""text"" class=""form-control"" id=""EditUserName"" name=""UserName"" maxlength=""500"" readonly disabled>
                        </div>
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label"">Email người dùng<span style=""color:red""></span>: </label>
                            <input type=""email"" class=""form-control"" id=""EditEmail"" name=""Email"" maxlength=""255"">
                        </div>
                    </div>
                    <div class=""input-group"">
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label"">Tên người dùng :");
            WriteLiteral(@"</label>
                            <input type=""text"" class=""form-control"" id=""EditFirstName"" name=""FirstName"" maxlength=""255"" >
                        </div>
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label"">Họ và tên đệm : </label>
                            <input type=""text"" class=""form-control"" id=""EditLastName"" name=""LastName"" maxlength=""255"">
                        </div>
                    </div>
                    <div class=""input-group"">
                        <div class=""col-md-6"">
                            <label for=""recipient-name"" class=""col-form-label"">Quyền cho tài khoản<span style=""color:red""> (*) </span>: </label>
                            <select name=""Role"" id=""Role"" class=""form-control"">
                                <option value=""0"">--- Chọn quyền ---</option>
");
#nullable restore
#line 115 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                                 foreach (RoleGroup item in roleGroup)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <option");
            BeginWriteAttribute("value", " value=\"", 6445, "\"", 6461, 1);
#nullable restore
#line 117 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
WriteAttributeValue("", 6453, item.Id, 6453, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> ");
#nullable restore
#line 117 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                                                         Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </option>\r\n");
#nullable restore
#line 118 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </select>
                        </div>
                        <div class=""col-md-6 d-inline-flex"">
                            <div class=""col-md-offset-2 mr-1 mt-3"">
                                <input type=""submit"" name=""btnEdit"" value=""Lưu"" id=""btnEdit"" class=""btn btn-primary"">
                            </div>
                            <div class=""col-md-offset-2 mt-3"">
                                <input type=""button"" data-dismiss=""modal"" aria-label=""Close"" name=""btnCloseUser"" value=""Đóng"" id=""btnCloseUser"" class=""btn btn-danger"">
                            </div>
                        </div>
                    </div>
");
#nullable restore
#line 130 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>

        </div>
    </div>
</div>
<div class=""modal fade"" id=""CreateUser"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-scrollable"" id=""FormUserManager"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Tạo mới tài khoản</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <h5 class=""text-danger"">
                    Vui lòng nhập các trường bắt buộc (*)
                </h5>
                <input id=""Id"" value=""0"" hidden />
                <div class=""input-group"">
                    <div class=""col-md-6"">
                        <label for=""recipient-name"" class=""col-form-label"">Tên tài khoản đăng n");
            WriteLiteral(@"hập<span style=""color:red""> (*) </span>: </label>
                        <input type=""text"" class=""form-control"" id=""UserName"" name=""UserName"" maxlength=""500"">
                    </div>
                    <div class=""col-md-6"">
                        <label for=""recipient-name"" class=""col-form-label"">Mật khẩu<span style=""color:red""> (*) </span>: </label>
                        <input type=""password"" class=""form-control"" id=""Password"" name=""Password"" maxlength=""500""><i id=""showpassword"" class=""fa fa-eye""></i>
                    </div>
                </div>
                <div class=""input-group"">
                    <div class=""col-md-6"">
                        <label for=""recipient-name"" class=""col-form-label"">Email người dùng<span style=""color:red""> (*) </span>: </label>
                        <input type=""text"" class=""form-control"" id=""Email"" name=""Email"" maxlength=""500"">
                    </div>
                    <div class=""col-md-6"">
                        <label for=""recipie");
            WriteLiteral(@"nt-name"" class=""col-form-label"">Quyền cho tài khoản<span style=""color:red""> (*) </span>: </label>
                        <select name=""RoleCreate"" id=""RoleCreate"" class=""form-control"">
                            <option value=""0"">--- Chọn quyền ---</option>
");
#nullable restore
#line 169 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                             foreach (RoleGroup item in roleGroup)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <option");
            BeginWriteAttribute("value", " value=\"", 9675, "\"", 9691, 1);
#nullable restore
#line 171 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
WriteAttributeValue("", 9683, item.Id, 9683, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> ");
#nullable restore
#line 171 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                                                     Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </option>\r\n");
#nullable restore
#line 172 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Setting\UserManager.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </select>
                    </div>
                </div>
                <div class=""form-group col-md-12 d-flex mt-2"">
                    <div class=""col-md-offset-2 mr-1"">
                        <input type=""submit"" name=""btnAddUser"" value=""Tạo mới"" id=""btnAddUser"" class=""btn btn-primary"">
                    </div>
                    <div class=""col-md-offset-2"">
                        <input type=""button"" data-dismiss=""modal"" aria-label=""Close"" name=""btnCloseUser"" value=""Đóng"" id=""btnCloseUser"" class=""btn btn-danger"">
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
<div class=""modal fade"" id=""HistoryUser"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-scrollable""");
            BeginWriteAttribute("id", " id=\"", 10590, "\"", 10595, 0);
            EndWriteAttribute();
            WriteLiteral(@" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Lịch sử tài khoản</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <table id=""tblHisUser"" class=""table-hover table-striped""></table>
            </div>
        </div>
    </div>
</div>


");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4005fd189cc9d0b6190d2919974b20ad0cd0948226903", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4005fd189cc9d0b6190d2919974b20ad0cd0948228003", async() => {
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
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4005fd189cc9d0b6190d2919974b20ad0cd0948229103", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4005fd189cc9d0b6190d2919974b20ad0cd0948230203", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4005fd189cc9d0b6190d2919974b20ad0cd0948231303", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MobiFiber.Models.Account> Html { get; private set; }
    }
}
#pragma warning restore 1591
