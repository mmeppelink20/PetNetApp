using MVCPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.HtmlHelpers
{
    public static class HelperMethods
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            StringBuilder result = new StringBuilder();
            bool previousPageButtonVisible = pagingInfo.CurrentPage > 1;
            bool nextPageButtonVisible = pagingInfo.CurrentPage < pagingInfo.TotalPages;
            bool firstPageButtonVisible;
            bool lastPageButtonVisible;
            if (pagingInfo.TotalPages <= 5)
            {
                firstPageButtonVisible = false;
                lastPageButtonVisible = false;
            }
            else
            {
                firstPageButtonVisible = pagingInfo.CurrentPage > 3;
                lastPageButtonVisible = pagingInfo.CurrentPage <= pagingInfo.TotalPages - 3;
            }

            // First Page Link

            if (firstPageButtonVisible)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(1));
                tag.InnerHtml = "<<";
                tag.AddCssClass("default-button nav-button");
                result.Append(tag.ToString());
            }

            // Previous Page Link
            if (previousPageButtonVisible)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                tag.InnerHtml = "<";
                tag.AddCssClass("default-button nav-button");
                result.Append(tag.ToString());
            }

            // Populate Number Buttons
            int startPage = pagingInfo.CurrentPage - 2;
            int endPage = pagingInfo.CurrentPage + 2;
            if (endPage > pagingInfo.TotalPages)
            {
                int newStartPage = startPage - (endPage - pagingInfo.TotalPages);
                startPage = newStartPage < 1 ? 1 : newStartPage;
                endPage = pagingInfo.TotalPages;
            }
            if (startPage < 1)
            {
                int newEndPage = endPage + (1 - startPage);
                endPage = newEndPage > pagingInfo.TotalPages ? pagingInfo.TotalPages : newEndPage;
                startPage = 1;
            }

            for (int currentPage = startPage; currentPage <= endPage; currentPage++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(currentPage));
                tag.InnerHtml = currentPage.ToString();
                if (currentPage == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("alternate-default-button");
                }
                else
                {
                    tag.AddCssClass("default-button");
                }
                tag.AddCssClass("nav-button");
                result.Append(tag.ToString());
            }

            // Next Page Link
            if (nextPageButtonVisible)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                tag.InnerHtml = ">";
                tag.AddCssClass("default-button nav-button");
                result.Append(tag.ToString());
            }
            // Last Page Link

            if (lastPageButtonVisible)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
                tag.InnerHtml = ">>";
                tag.AddCssClass("default-button nav-button");
                result.Append(tag.ToString());
            }


            return MvcHtmlString.Create(result.ToString());
        }
    }
}