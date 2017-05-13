using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using EasyUkr.WebApi.Models;

namespace EasyUkr.WebApi.HTMLHelpers
{
    
    public static class ImageHelper
    {
        #region Picture
        public static MvcHtmlString Image(this HtmlHelper html, string src, string alt)
        {
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", alt);
            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString Image(this HtmlHelper html, byte[] src, string alt)
        {
            var img = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(src));
            return html.Image(img,alt);
        }
        public static MvcHtmlString ImageForModel<TModel, TProperty>
(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return Image(htmlHelper, name, metadata.Model as string);
        }
        #endregion

        #region List For Model
        #endregion


    }
}