using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;
using EasyUKR.Models;

namespace EasyUKR.HTMLHelpers
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

        public static MvcHtmlString List(this HtmlHelper html, Answer[] answers)
        {
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("class", "list-group");
            var innerBuilder=new StringBuilder();
            for (var index = 0; index < answers.Length; index++) //намутити в рядок!!!!
            {
                var item = answers[index];
                innerBuilder.Append("<span>");
                innerBuilder.Append("<p>" + item.GetContent() + "</p>");
                innerBuilder.Append(html.CheckBox(nameof(answers)+$"[{index}]", item.GetAnswer()));
                innerBuilder.Append("</span><br/>");
            }

            div.InnerHtml = innerBuilder.ToString();
            return MvcHtmlString.Create(div.ToString());
        }

        /*public static MvcHtmlString ListFor<TModel, TProperty>(this HtmlHelper htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return null;
        }
        */
        #endregion


    }
}