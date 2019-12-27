using IPG_Funcionarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;

using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Paginacao
{
    [HtmlTargetElement("div", Attributes = "modelo-pagina")]
    public class Paginacao : TagHelper
    {/*
        private readonly int MaxLinkPorPag = 10;

        public PaginacaoViewModel ModeloPagina { get; set; }

        public string AccaoDaPagin { get; set; }

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

            var resultaDaDiv = new TagBuilder("div");


            int pagInicial = ModeloPagina.PaginaCorrente - MaxLinkPorPag / 2;
            if (pagInicial < 1) pagInicial = 1;


            int pagfinal = pagInicial + MaxLinkPorPag - 1;
            if (pagfinal > ModeloPagina.NumeroPagina) pagfinal = ModeloPagina.NumeroPagina;


            for (int p = pagInicial; p <= pagfinal; p++)
            {
                var ligacao = new TagBuilder("a");

                ligacao.Attributes["href"] = urlHelper.Action(AccaoDaPagin, new { pagina = p });

                ligacao.AddCssClass("btn");

                if (p == ModeloPagina.PaginaCorrente)
                {
                    ligacao.AddCssClass("btn-info");
                }
                else
                {
                    ligacao.AddCssClass("btn-primary");
                }

                ligacao.InnerHtml.Append(p.ToString());
                resultaDaDiv.InnerHtml.AppendHtml(ligacao);
            }

            output.Content.AppendHtml(resultaDaDiv.InnerHtml);
        }*/
    }
}