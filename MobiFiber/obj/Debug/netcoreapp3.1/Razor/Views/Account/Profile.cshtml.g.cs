#pragma checksum "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a2379ebda8494c243da6df2872a0c72264584022"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Profile), @"mvc.1.0.view", @"/Views/Account/Profile.cshtml")]
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
#line 1 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
using MobiFiber.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2379ebda8494c243da6df2872a0c72264584022", @"/Views/Account/Profile.cshtml")]
    public class Views_Account_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MobiFiber.Models.Account>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/select2/css/select2.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap-table/css/bootstrap-table.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/app-assets/vendors/toastr/toastr.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/select2/js/select2.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap-table/js/bootstrap-table.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/app-assets/vendors/toastr/toastr.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Profile.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
  
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
    ViewBag.Title = "Thông tin tài khoản";
    RoleGroup roleGroup = new RoleGroup();
    if (ViewData["roleGroup"] != null)
    {
        roleGroup = (RoleGroup)ViewData["roleGroup"];
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("css", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a2379ebda8494c243da6df2872a0c722645840226265", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a2379ebda8494c243da6df2872a0c722645840227443", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a2379ebda8494c243da6df2872a0c722645840228621", async() => {
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
                WriteLiteral("\r\n    <link href=\"/lib/bootstrap-datepicker/css/bootstrap-datepicker3.css\" rel=\"stylesheet\" />\r\n");
            }
            );
            WriteLiteral(@"<div class=""row"">
    <div class=""col-md-6"">
        <div class=""card w-100"">
            <div class=""card-header"">
                <i class=""fa fa-info"" style=""margin-right:5px""></i>Thông tin cá nhân
            </div>
            <div class=""card-body"">
");
#nullable restore
#line 26 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
                 using (Html.BeginForm("EditProfile", "Account", FormMethod.Post, new { @class = "form-horizontal form-simple", role = "form", id = "frm" }))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"text-danger font-weight-500\">\r\n                        ");
#nullable restore
#line 29 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
                   Write(ViewData["MessageProfile"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""FirstName"" class=""col-form-label"">Tên:</label>
                                    <input type=""text"" name=""firstname"" id=""firstname""");
            BeginWriteAttribute("value", " value=\"", 1623, "\"", 1647, 1);
#nullable restore
#line 36 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 1631, Model.FirstName, 1631, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 1648, "\"", 1662, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" />
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""LastName"" class=""col-form-label"">Họ và tên đệm:</label>
                                    <input type=""text"" name=""lastname"" id=""lastname""");
            BeginWriteAttribute("value", " value=\"", 2150, "\"", 2173, 1);
#nullable restore
#line 44 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 2158, Model.LastName, 2158, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 2174, "\"", 2188, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""Role"" class=""col-form-label"">Vai trò:</label>
                                    <input type=""text"" name=""role"" id=""role""");
            BeginWriteAttribute("value", " value=\"", 2725, "\"", 2748, 1);
#nullable restore
#line 54 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 2733, roleGroup.Name, 2733, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 2749, "\"", 2763, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" disabled />
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""email"" class=""col-form-label"">Email:</label>
                                    <input type=""text"" name=""email"" id=""email""");
            BeginWriteAttribute("value", " value=\"", 3243, "\"", 3263, 1);
#nullable restore
#line 62 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 3251, Model.Email, 3251, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 3264, "\"", 3278, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""phone"" class=""col-form-label"">Số điện thoại:</label>
                                    <input type=""tel"" name=""phone"" id=""phone""");
            BeginWriteAttribute("value", " value=\"", 3823, "\"", 3843, 1);
#nullable restore
#line 72 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 3831, Model.Phone, 3831, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 3844, "\"", 3858, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" />
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6"">
                            <div class=""form-group"">
                                <div class=""controls"">
                                    <label for=""birthday"" class=""col-form-label"">Ngày sinh:</label>
                                    <input type=""text"" name=""birthday"" id=""birthday""");
            BeginWriteAttribute("value", " value=\"", 4342, "\"", 4365, 1);
#nullable restore
#line 80 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 4350, Model.Birthday, 4350, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 4366, "\"", 4380, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control round\" />\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                    <input type=\"hidden\" name=\"Id\"");
            BeginWriteAttribute("value", " value=\"", 4599, "\"", 4616, 1);
#nullable restore
#line 85 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 4607, Model.Id, 4607, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    <div class=\"row align-content-center justify-content-center\">\r\n                        <button class=\"btn btn-primary\">Lưu hồ sơ</button>\r\n                    </div>\r\n");
#nullable restore
#line 89 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
    <div class=""col-md-6"">
        <div class=""card w-100"">
            <div class=""card-header"">
                <i class=""fa fa-key"" style=""margin-right:5px""></i>Đổi mật khẩu
            </div>
            <div class=""card-body"">
");
#nullable restore
#line 99 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
                 using (Html.BeginForm("ChangePass", "Account", FormMethod.Post, new { @class = "form-horizontal form-simple", role = "form", id = "frm" }))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"text-danger font-weight-500\">\r\n                        ");
#nullable restore
#line 102 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
                   Write(ViewData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <div class=""controls"">
                                <label for=""oldpassword"" class=""col-form-label"">Mật khẩu cũ:</label>
                                <input type=""password"" name=""oldpassword"" id=""oldpassword""");
            BeginWriteAttribute("value", " value=\"", 5767, "\"", 5775, 0);
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 5776, "\"", 5790, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control round\" />\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
            WriteLiteral(@"                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <div class=""controls"">
                                <label for=""NewPassword"" class=""col-form-label"">Mật khẩu mới:</label>
                                <input type=""password"" name=""NewPassword"" id=""NewPassword""");
            BeginWriteAttribute("value", " value=\"", 6261, "\"", 6269, 0);
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 6270, "\"", 6284, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" />
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <div class=""controls"">
                                <label for=""ConfirmPassword"" class=""col-form-label"">Nhập lại mật khẩu mới:</label>
                                <input type=""password"" name=""ConfirmPassword"" id=""ConfirmPassword""");
            BeginWriteAttribute("value", " value=\"", 6774, "\"", 6782, 0);
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 6783, "\"", 6797, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control round"" />
                            </div>
                        </div>
                    </div>
                    <div class=""row align-content-center justify-content-center"">
                        <input type=""submit"" class=""btn btn-primary"" name=""name"" value=""Đổi mật khẩu"" />
                    </div>
                    <input type=""hidden"" name=""UserName""");
            BeginWriteAttribute("value", " value=\"", 7199, "\"", 7222, 1);
#nullable restore
#line 132 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 7207, Model.UserName, 7207, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                    <input type=\"hidden\" name=\"Id\"");
            BeginWriteAttribute("value", " value=\"", 7278, "\"", 7295, 1);
#nullable restore
#line 133 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
WriteAttributeValue("", 7286, Model.Id, 7286, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 134 "D:\DoAnTotNghiep\Code\Mobifiber\Mobifiber\TemplateNetCore\Views\Account\Profile.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2379ebda8494c243da6df2872a0c7226458402222857", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2379ebda8494c243da6df2872a0c7226458402223957", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2379ebda8494c243da6df2872a0c7226458402225057", async() => {
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
                WriteLiteral("\r\n    <script src=\"/lib/bootstrap-datepicker/js/bootstrap-datepicker.js\"></script>\r\n    <script src=\"/lib/moment/moment.min.js\"></script>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2379ebda8494c243da6df2872a0c7226458402226302", async() => {
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
