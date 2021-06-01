#pragma checksum "D:\Repos\Onym\Onym\Views\User\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aa7bb422ca6681d24c0220e88b1b5854b2591ad2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Profile), @"mvc.1.0.view", @"/Views/User/Profile.cshtml")]
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
#line 1 "D:\Repos\Onym\Onym\Views\_ViewImports.cshtml"
using Onym;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Repos\Onym\Onym\Views\_ViewImports.cshtml"
using Onym.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Repos\Onym\Onym\Views\_ViewImports.cshtml"
using Onym.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Repos\Onym\Onym\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Repos\Onym\Onym\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aa7bb422ca6681d24c0220e88b1b5854b2591ad2", @"/Views/User/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b59ee3af752ca381514ee76e10226ae3d91fe09f", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Onym.ViewModels.User.IndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Аватар пользователя"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
  
    ViewBag.Title = Model.User!.UserName;
    ViewBag.Page = "UserProfile";
    var db = new OnymDbContext<User>();
    var RateUps = db.PublicationRatingTallies.Where(t => t.UserId == Model.User.Id).Count(p => p.PublicationRating);
    var RateDowns = db.PublicationRatingTallies.Where(t => t.UserId == Model.User.Id).Count(p => p.PublicationRating == false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
  
    await Html.RenderPartialAsync("_MainMenuPanelUser");

#line default
#line hidden
#nullable disable
            WriteLiteral("<!-- USER-PROFILE -->\r\n<div class=\"main-section\">\r\n    <div class=\"main-section-item user-profile-header\">\r\n        <!-- AVATAR -->\r\n        <div class=\"user-avatar large\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "aa7bb422ca6681d24c0220e88b1b5854b2591ad24686", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
          WriteLiteral(ViewBag.DefaultAvatar);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("src", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
                                                   Write(Model.User!.UserProfilePictureNavigation.FileLink);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("data-src", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 17 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"user-profile-about\">\r\n            <div class=\"user-profile-nick\">\r\n                ");
#nullable restore
#line 21 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
           Write(Model.User!.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"user-profile-registration-date\">\r\n                Зарегестрировался ");
#nullable restore
#line 24 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
                             Write(Model.User!.RegistrationDate.ToString("dd MMMM yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"separator\"></div>\r\n    <div class=\"main-section-item\">\r\n        <div class=\"user-profile-rates\">\r\n            Поставил <span class=\"positive\">");
#nullable restore
#line 31 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
                                       Write(RateUps);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> плюсов и <span class=\"negative\">");
#nullable restore
#line 31 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
                                                                                       Write(RateDowns);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> минусов\r\n        </div>\r\n    </div>\r\n    <div class=\"separator\"></div>\r\n    <div class=\"main-section-item user-profile-statistic\">\r\n        <div>\r\n            <span class=\"counter\">\r\n                ");
#nullable restore
#line 38 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
           Write(Model.User.RatingTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </span>\r\n            <div class=\"counter-label\">\r\n                Рейтинг\r\n            </div>\r\n        </div>\r\n        <div>\r\n            <span class=\"counter\">\r\n                ");
#nullable restore
#line 46 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
           Write(Model.User.Comments.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </span>\r\n            <div class=\"counter-label\">\r\n                Комментарии\r\n            </div>\r\n        </div>\r\n        <div>\r\n            <span class=\"counter\">\r\n                ");
#nullable restore
#line 54 "D:\Repos\Onym\Onym\Views\User\Profile.cshtml"
           Write(Model.User.Publications.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </span>\r\n            <div class=\"counter-label\">\r\n                Посты\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<User> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<User> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Onym.ViewModels.User.IndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
