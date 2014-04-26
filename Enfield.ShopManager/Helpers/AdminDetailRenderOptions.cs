using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace Enfield.ShopManager.Helpers
{
    public class AdminDetailRenderOptions : PagedListRenderOptions
    {
        public AdminDetailRenderOptions()
		{
			DisplayLinkToFirstPage = true;
            DisplayLinkToLastPage = true;
			DisplayLinkToPreviousPage = true;
			DisplayLinkToNextPage = true;
			DisplayLinkToIndividualPages = false;
			DisplayPageCountAndCurrentLocation = true;
            DisplayItemSliceAndTotal = false;
			MaximumPageNumbersToDisplay = 10;
			DisplayEllipsesWhenNotShowingAllPageNumbers = true;
			EllipsesFormat = "&#8230;";
            LinkToFirstPageFormat = "<span class='ui-icon ui-icon-seek-start'></span>";
            LinkToPreviousPageFormat = "<span class='ui-icon ui-icon-seek-prev'></span>";
			LinkToIndividualPageFormat = "{0}";
            LinkToNextPageFormat = "<span class='ui-icon ui-icon-seek-next'></span>";
            LinkToLastPageFormat = "<span class='ui-icon ui-icon-seek-end'></span>";
			PageCountAndCurrentLocationFormat = "{0} of {1}";
			ItemSliceAndTotalFormat = "{0} - {1} of {2}";
			FunctionToDisplayEachPageNumber = null;
			ClassToApplyToLastListItemInPager = "next";
		}
    }
}