using GridMvc.Columns;
using GridMvc.Html;
using Hrms.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Utilities
{
    public static class HTMLHelperExtension
    {
        public static IGridHtmlOptions<T> TemplateGrid<TModel, T>(
                this HtmlHelper<TModel> helper, 
                IGridHtmlOptions<T> Grid, 
                string Controller) 
            where T : EntityBase<T>, new()
        {
            if (Grid == null) return null;

            return Grid.Columns(columns => {
               columns.Add()
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(30)
                    .RenderValueAs(o => "<a onclick='deleteRow(" + o.Id + "); return false;' " + 
                                                "href='javascript:deleteRow(" + o.Id + "); return false;' class='btn btn-default btn-sm'>" + 
                                            "<span class='glyphicon glyphicon-minus' title='Click to Delete'></span>" +
                                         "</a>");
               columns.Add()
                   .Encoded(false)
                   .Sanitized(false)
                   .SetWidth(30)
                   .RenderValueAs(o => "<a href='/" + Controller + "/Edit/" + o.Id + "' class='btn btn-default btn-sm'>" +
                                        "<span class='glyphicon glyphicon-edit' title='Click to Edit'></span>" + 
                                        "</a>");
                }).WithPaging(20);
        }

        public static IGridColumn<T> AddEditLink<T>(
                this IGridColumn<T> column, 
                Func<T, Func<object, 
                System.Web.WebPages.HelperResult>> constraint
            )
        {
            if (column == null) return null;

            return column
                    .Encoded(false)
                    .Sanitized(false)
                    .Sortable(true)
                    .Filterable(true)
                    .RenderValueAs(constraint);
        }

        public static IEnumerable<SelectListItem> GetSelectList<TModel>(this ViewDataDictionary<TModel> viewData, string key)
        {
            if (viewData == null || string.IsNullOrEmpty(key)) return null;

            Dictionary<string, IEnumerable<SelectListItem>> DomainValueDictionary = (Dictionary<string, IEnumerable<SelectListItem>>)viewData["DomainValue"];
            IEnumerable<SelectListItem> Value = null;
            DomainValueDictionary.TryGetValue(key, out Value);
            return Value;
        }
    }
}