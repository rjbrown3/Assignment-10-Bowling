using BowlingApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-info")] 
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;

        public PaginationTagHelper (IUrlHelperFactory urlin)
        {
            urlInfo = urlin;
        }

        public PageNumberingInfo PageInfo { get; set; } //stores paginginfo from div page-info tag in index.cshtml

        //our own dictionary (key value pairs) that we are creating
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        //public PageNumberingInfo PageModel { get; set; }
        //public bool PageClassesEnabled { get; set; } = false;

        //public string PageClass { get; set; }
        //public string PageClassNormal { get; set; }
        //public string PageClassSelected { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //process method: what happens when the tag is referred to
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder finishedTag = new TagBuilder("div");  //build a tag for a div

            for (int i = 1; i <= PageInfo.NumPages; i++)
            {
                IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);
                TagBuilder individualTag = new TagBuilder("a");  //build a tag every time it loops through

                KeyValuePairs["pageNum"] = i;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);

                //if (PageClassesEnabled)
                //{
                //    individualTag.AddCssClass(PageClass);
                //    individualTag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                //}

                individualTag.InnerHtml.AppendHtml(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);  //add in the a tag
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);  //append all of it to html
        }
    }
}
