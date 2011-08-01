using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Impulse.Web.Mvc
{
  public static class MobileHtmlHelpers
  {
    public static IHtmlString ActionButton(this HtmlHelper htmlHelper, string text = null, string url = null, bool dataAjax = true, string dataIcon = null, string dataIconPosition = null, bool dataInline = true, string dataRole = "button", string dataTheme = null, string dataTransition = null, bool isModalDialog = false)
    {
      TagBuilder tb = new TagBuilder("a");
      tb.InnerHtml = HttpUtility.HtmlEncode(text);
      tb.AddNonEmptyAttribute("href", url);

      if (!dataAjax) {
        tb.Attributes.Add("data-ajax", "false");
      }

      tb.AddNonEmptyAttribute("data-icon", dataIcon);
      tb.AddNonEmptyAttribute("data-iconpos", dataIconPosition);
      
      if (dataInline)
      {
        tb.Attributes.Add("data-inline", "true");
      }
      if (isModalDialog)
      {
        tb.Attributes.Add("data-rel", "dialog");
      }
      tb.AddNonEmptyAttribute("data-role", dataRole);
      tb.AddNonEmptyAttribute("data-theme", dataTheme);
      tb.AddNonEmptyAttribute("data-transition", dataTransition);

      return new MvcHtmlString(tb.ToString(TagRenderMode.Normal));
    }

    public static IHtmlString ListView(this HtmlHelper htmlHelper,
                                        IEnumerable<dynamic> source = null,
                                        bool filterable = false,
                                        bool inset = false,
                                        bool numbered = false,
                                        string dataTheme = null,
                                        string dataCountTheme = null,
                                        string dataDividerTheme = null,
                                        Func<dynamic, ListViewItem> template = null,
                                        Func<dynamic, object> customTemplate = null)
    {
      var listView = new jQueryMobileListView(source, filterable, inset, numbered, dataTheme, dataCountTheme, dataDividerTheme, template, customTemplate);
      return listView.GetHtml();
    }

    private static void AddNonEmptyAttribute(this TagBuilder tagBuilder, string attribute, string input)
    {
      if (!string.IsNullOrWhiteSpace(input))
      {
        tagBuilder.Attributes.Add(attribute, input);
      }
    }
  }
}
