#pragma checksum "D:\Repos\Onym\Onym\Views\Shared\_TinyMCE.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a417f7a5fc7b594c93f206cd8bcfcba77e709154"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__TinyMCE), @"mvc.1.0.view", @"/Views/Shared/_TinyMCE.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a417f7a5fc7b594c93f206cd8bcfcba77e709154", @"/Views/Shared/_TinyMCE.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b59ee3af752ca381514ee76e10226ae3d91fe09f", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__TinyMCE : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script src=""https://cdn.tiny.cloud/1/0whn45accxwc58bwqq1f0syy9nlulcohla6j26t05lbimrlm/tinymce/5/tinymce.min.js"" referrerpolicy=""origin""></script>
<script>
    tinymce.init({
    selector: '.form-input-textarea',
    menubar: false,
    inline: true,
    plugins: [
      'autolink',
      'link',
      'media',
      'image',
      'quickbars',
    ],
    placeholder: 'Текст...',
    toolbar: false,
    toolbar_location: 'bottom',
    quickbars_insert_toolbar: false,
    quickbars_selection_toolbar: 'bold italic underline strikethrough | quicklink blockquote | removeformat',
    contextmenu: false,
   });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
