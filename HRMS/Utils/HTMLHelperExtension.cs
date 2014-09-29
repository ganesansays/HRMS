using GridMvc.Columns;
using GridMvc.Html;
using HRMS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Utils
{
    public static class HTMLHelperExtension
    {
        public static IGridHtmlOptions<T> TemplateGrid<TModel, T>(this HtmlHelper<TModel> helper, IGridHtmlOptions<T> Grid, string Controller) where T : EntityBase
        {
            return Grid.Columns(columns => {
               columns.Add()
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(30)
                    .RenderValueAs(o => "<a onclick='deleteRow(" + o.Id + "); return false;' href='javascript:deleteRow(" + o.Id + "); return false;' class='btn btn-default btn-sm'>" + 
                                            "<span class='glyphicon glyphicon-minus'></span>" +
                                         "</a>");
               columns.Add()
                   .Encoded(false)
                   .Sanitized(false)
                   .SetWidth(30)
                   .RenderValueAs(o => "<a href='/" + Controller + "/Edit/" + o.Id + "' class='btn btn-default btn-sm'>" + 
                                        "<span class='glyphicon glyphicon-edit'></span>" + 
                                        "</a>");
                }).WithPaging(20);
        }

        public static IGridColumn<T> AddLinkColumn<TModel, T>(this GridMvc.Columns.IGridColumn<T> column, string Controller, string name) where T : EntityBase
        {
            return column.Encoded(false).Sanitized(false).Sortable(true).Filterable(true).RenderValueAs(o => "<a href='/" + Controller + "/Edit/" + o.Id + "'><span>@o.Name</span></a>");
        }
    }
}