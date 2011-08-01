using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Impulse.Web.Mvc
{
  internal class jQueryMobileListView
  {
    private IEnumerable<dynamic> _source;
    private bool _filterable;
    private bool _inset;
    private bool _numbered;
    private string _dataTheme;
    private string _dataCountTheme;
    private string _dataDividerTheme;
    //private ListViewItem _listViewItem;
    private Func<object, ListViewItem> _template;
    private Func<dynamic, object> _customTemplate;

    public jQueryMobileListView(IEnumerable<dynamic> source = null,
                                bool filterable = false,
                                bool inset = false,
                                bool numbered = false,
                                string dataTheme = null,
                                string dataCountTheme = null,
                                string dataDividerTheme = null,
                                Func<dynamic, ListViewItem> template = null,
                                Func<dynamic, object> customTemplate = null
      )
    {
      _source = source;
      _filterable = filterable;
      _inset = inset;
      _numbered = numbered;
      _dataTheme = dataTheme;
      _dataCountTheme = dataCountTheme;
      _dataDividerTheme = dataDividerTheme;
      _template = template;
      _customTemplate = customTemplate;
    }

    public IHtmlString GetHtml()
    {
      TagBuilder list = GetList(_filterable, _inset, _numbered, _dataTheme, _dataCountTheme, _dataDividerTheme);
      string listItems = GetListItems(_source, _template, _customTemplate);
      list.InnerHtml += listItems;
      return new HtmlString(list.ToString(TagRenderMode.Normal));
    }

    public static TagBuilder GetList(bool filterable, bool inset, bool numbered, string dataTheme, string dataCountTheme, string dataDividerTheme)
    {
      TagBuilder list = new TagBuilder(numbered ? "ol" : "ul"); // use "ol" if numbered
      list.Attributes.Add("data-role", "listview");

      if (filterable)
        list.Attributes.Add("data-filter", "true");

      if (inset)
        list.Attributes.Add("data-inset", "true");

      if (dataTheme != null)
        list.Attributes.Add("data-theme", dataTheme);

      if (!string.IsNullOrWhiteSpace(dataCountTheme))
        list.Attributes.Add("data-count-theme", dataCountTheme);

      if (!string.IsNullOrWhiteSpace(dataDividerTheme)) // better than simple null check?
        list.Attributes.Add("data-divider-theme", dataDividerTheme);

      return list;
    }

    public static string GetListItems(IEnumerable<dynamic> source, Func<dynamic, ListViewItem> template, Func<dynamic, object> customTemplate)
    {
      StringBuilder sb = new StringBuilder();

      foreach (var dataItem in source)
      {
        if (customTemplate != null)
        {
          sb.AppendFormat("<li>{0}</li>", customTemplate(dataItem));
          //var content = _customTemplate.Invoke();
        }
        else if (template != null)
        {
          var li = GetListItem(template.Invoke(dataItem));
          sb.Append(li);
        }
        else
          sb.Append(string.Format("<li><p>{0}</p></li>", HttpUtility.HtmlEncode(dataItem.ToString())));
        //li.InnerHtml = sourc
        //if (!String.IsNullOrEmpty(style))
        //{
        //  tr.MergeAttribute("class", style);
        //}
        //foreach (var column in columns)
        //{
        //  var value = (column.Format == null) ?
        //      HttpUtility.HtmlEncode(row[column.ColumnName]) : Format(column.Format, row).ToString();
        //  tr.InnerHtml += GetTableCellHtml(column, value ?? string.Empty);
        //}

        //var value = (ListViewItem.Format == null)
        //              ? HttpUtility.HtmlEncode()
        //              : Format(ListViewItem.Format, row).ToString();
        //sb.Append(li.ToString());
        //r++;
      }
      return sb.ToString();
    }

    public static string GetListItem(ListViewItem listViewItem)
    {
      TagBuilder li = new TagBuilder("li");

      // DataDivider: <li data-role="list-divider">Friday, October 8, 2010 <span class="ui-li-count">2</span></li>
      if (listViewItem.DataDivider)
        li.Attributes.Add("data-role", "list-divider");

      if (!string.IsNullOrEmpty(listViewItem.DataIcon))
        li.Attributes.Add("data-icon", listViewItem.DataIcon);

      string content = string.Empty;

      // <img src="images/album-ws.jpg" />
      if (!string.IsNullOrEmpty(listViewItem.ImageUrl))
      {
        TagBuilder img = new TagBuilder("img");
        img.Attributes.Add("src", listViewItem.ImageUrl); // TODO: encode and resolve properly!
        content += img.ToString(TagRenderMode.Normal);
      }

      // <h3>Stephen Weber</h3>
      if (!string.IsNullOrEmpty(listViewItem.HeaderText))
      {
        TagBuilder header = new TagBuilder("h3")
                              {
                                InnerHtml = HttpUtility.HtmlEncode(listViewItem.HeaderText)
                              };
        content += header.ToString(TagRenderMode.Normal);
      }

      // Text: <p>Hey Stephen, if you're available at 10am tomorrow, we've got a meeting with the jQuery team.</p>
      if (!string.IsNullOrEmpty(listViewItem.Text))
      {
        TagBuilder p = new TagBuilder("p");
        p.InnerHtml = HttpUtility.HtmlEncode(listViewItem.Text);
        content += p.ToString(TagRenderMode.Normal);
      }

      // AsideText: <p class="ui-li-aside"><strong>4:48</strong>PM</p>
      if (!string.IsNullOrEmpty(listViewItem.AsideText))
      {
        TagBuilder p = new TagBuilder("p");
        p.AddCssClass("ui-li-aside"); ;
        p.InnerHtml = HttpUtility.HtmlEncode(listViewItem.AsideText);
        content += p.ToString(TagRenderMode.Normal);
      }

      // <span class="ui-li-count">0</span>
      if (listViewItem.Count != null)
      {
        TagBuilder span = new TagBuilder("span")
                            {
                              InnerHtml = listViewItem.Count.ToString()
                            };
        span.AddCssClass("ui-li-count");
        content += span.ToString(TagRenderMode.Normal);
      }
      //li.InnerHtml += string.Format("<h3>{0}</h3>", listViewItem.HeaderText);

      // Wrap content in anchor tag if there is a url
      if (!string.IsNullOrEmpty(listViewItem.Url))
      {
        TagBuilder a = new TagBuilder("a");
        a.Attributes["href"] = listViewItem.Url;
        a.InnerHtml += content;
        li.InnerHtml += a.ToString(TagRenderMode.Normal);
      }
      else
      {
        li.InnerHtml += content;
      }

      return li.ToString();
    }

    private static HelperResult Format(Func<dynamic, object> format, dynamic arg)
    {
      var result = format(arg);
      return new HelperResult(tw =>
                                {
                                  var helper = result as HelperResult;
                                  if (helper != null)
                                  {
                                    helper.WriteTo(tw);
                                    return;
                                  }
                                  IHtmlString htmlString = result as IHtmlString;
                                  if (htmlString != null)
                                  {
                                    tw.Write(htmlString);
                                    return;
                                  }
                                  if (result != null)
                                  {
                                    tw.Write(HttpUtility.HtmlEncode(result));
                                  }
                                });
    }

  }
}