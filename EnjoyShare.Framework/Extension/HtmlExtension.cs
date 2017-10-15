using System.Web.Mvc;

namespace EnjoyShare.Framework.Extension
{
    public static class HtmlExtension
    {       /// <summary>
            /// 自定义一个@html.Submit()
            /// </summary>
            /// <param name="helper"></param>
            /// <param name="value">value属性</param>
            /// <param name="defaultClass">预设的class</param>
            /// <returns></returns>
        public static MvcHtmlString Submit(this HtmlHelper helper, string value, string defaultClass = "btn btn-default")
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("type", "submit");
            builder.MergeAttribute("value", value);
            builder.MergeAttribute("class", defaultClass);
            builder.ToString(TagRenderMode.EndTag);
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
