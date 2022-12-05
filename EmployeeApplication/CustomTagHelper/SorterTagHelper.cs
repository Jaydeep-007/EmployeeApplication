using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EmployeeApplication.CustomTagHelper
{
    [HtmlTargetElement("tr", Attributes = "isAsc,sortby,pagesize,search")]
    public class SorterTagHelper : TagHelper
    {
        private const int V = 0;
        private IUrlHelperFactory urlHelperFactory;
        public SorterTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        #region Input Attributes
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("isAsc")]
        public bool isAsc { get; set; }

        [HtmlAttributeName("sortby")]
        public int sortby { get; set; }

        [HtmlAttributeName("pagesize")]
        public int? pagesize { get; set; }

        [HtmlAttributeName("search")]
        public string search { get; set; }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            List<string> li = new List<string>()
            {
                "Id","EmployeeName","Email","Gender","DateOfJoining"
            };

            TagBuilder tr = new TagBuilder("tr");
            int headerid = V;
            for (int row = 1; row <= 3; row++)
            {
                TagBuilder th = new TagBuilder("th");
                TagBuilder tag = new TagBuilder("a");
                var togglesort = (row == sortby ? (!isAsc).ToString() : "true");

                tag.Attributes["href"] = urlHelper.Action("Grid", "Employee", new { page = pagesize, sortby = row, isAsc = togglesort, Search = search });
                tag.InnerHtml.Append(li[headerid]);

                if (sortby != 0)
                {
                    if (row == sortby)
                    {
                        TagBuilder tagspan = new TagBuilder("span");
                        tagspan.AddCssClass($"arrow {(isAsc ? "up" : "down")}");
                        th.InnerHtml.AppendHtml(tagspan);
                    }
                }
                th.InnerHtml.AppendHtml(tag);
                tr.InnerHtml.AppendHtml(th);
                headerid += 1;
            }
            output.Content.AppendHtml(tr.InnerHtml);
        }
    }
}