using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CustomerSystem.Helpers
{
    public static class MyExtensions
    {
        public static MvcHtmlString NumberBox<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> list, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", propertyName);
            dropdown.MergeAttribute("class", "form-control no-arrow");
            dropdown.MergeAttribute("type", "number");

            dropdown.MergeAttributes(helper.GetUnobtrusiveValidationAttributes(propertyName));  //hata mesajı
            dropdown.MergeAttribute("style", "width:50px");
            StringBuilder options = new StringBuilder();
            foreach (var item in list)
            {
                options = options.Append("<option value='" + item.Value + "'>" + item.Text + "</option>");
            }
            options.Append("<input name='codeNum'  class='form-control no-arrow' style='width:40px' type='number' required='required' minlength='3' maxlength='3'/> ");    //alan kodu
            options.Append("<span class='input-group-addon'>-</span>");
            options.Append("<input name='thirdNum'  class='form-control no-arrow' style='' type='number' required='required' minlength='3' maxlength='3' /> ");    //numaranın geri kalan kısmı
            options.Append("<span class='input-group-addon'>-</span>");
            options.Append("<input name='fourthNum'  class='form-control no-arrow' style='width:50px' type='number' required='required' minlength='4' maxlength='4' />");

            dropdown.InnerHtml = options.ToString();

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                dropdown.MergeAttributes(attributes);
            }

            return new MvcHtmlString(dropdown.ToString());
        }
        public static MvcHtmlString NumberBox2(this HtmlHelper helper)
        {
            
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("name", "phoneNumber");
            div.MergeAttribute("class", "input-group");
            StringBuilder options = new StringBuilder();
            options.Append("<input name='codeNum' class='form-control' type='text'/>");
            options.Append("<input name='thirdNum' class='form-control' type='text'/> ");
            options.Append("<input name='fourthNum' class='form-control' type='text'/>");
            div.InnerHtml = options.ToString();
            return new MvcHtmlString(div.ToString());
        }


    }
}