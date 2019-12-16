using IPG_Funcionarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Paginacao
{
    [HtmlTargetElement("div",Attributes ="page-model")]
    public class Paginacao :TagHelper

    {
        private readonly int MaxPagelinks = 10;

        public PaginacaoViewModel PageModel { get; set; }

        public string PageAction { get; set; }
        private IUrlHelperFactory urlHelperFactory;
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public Paginacao(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            var resultDiv = new TagBuilder("div");
            int initialPage = PageModel.CurrentPage - MaxPagelinks / 2;
            if (initialPage < 1) initialPage = 1;

            int finalPage = initialPage + MaxPagelinks - 1;
            if (finalPage > PageModel.NumberPages) finalPage = PageModel.NumberPages;

            for(int p = initialPage; p <= finalPage; p++)
            {
                var link = new TagBuilder("a");
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = p });
                link.AddCssClass("btn");
                if (p == PageModel.CurrentPage)
                {
                    link.AddCssClass("btn-info");
                }
                else
                {
                    link.AddCssClass("btn-default");
                }

                link.InnerHtml.Append(p.ToString());

                resultDiv.InnerHtml.AppendHtml(link);
            }
            output.Content.AppendHtml(resultDiv.InnerHtml);
           // base.Process(context, output);
        }
    }
}
