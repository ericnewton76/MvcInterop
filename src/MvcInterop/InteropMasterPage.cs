using System.Web.Mvc;
using System.Web.UI;

namespace MvcInterop
{
    public class InteropMasterPage : MasterPage
    {

		protected override void OnInit(System.EventArgs e)
		{
			base.OnInit(e);
		}

        // Properties
        public AjaxHelper<object> Ajax
        {
            get
            {
                return MvcViewPage != null ? MvcViewPage.Ajax : ViewPage.Ajax;
            }
        }

        public HtmlHelper<object> Html
        {
            get
            {
                return MvcViewPage != null ? MvcViewPage.Html : ViewPage.Html;
            }
        }

        public object Model
        {
            get
            {
                return MvcViewPage != null ? MvcViewPage.Model : ViewData.Model;
            }
        }

        public TempDataDictionary TempData
        {
            get
            {
                if (MvcViewPage != null) return MvcViewPage.TempData;
                return ViewPage.TempData;
            }
        }

        public UrlHelper Url
        {
            get
            {
                if (MvcViewPage != null) return MvcViewPage.Url;
                return ViewPage.Url;
            }
        }


        public dynamic ViewBag
        {
            get
            {
                if (MvcViewPage != null) return MvcViewPage.ViewBag;
                return ViewPage.ViewBag;
            }
        }

        public ViewContext ViewContext
        {
            get
            {
                if (MvcViewPage != null) return MvcViewPage.ViewContext;
                return ViewPage.ViewContext;
            }
        }

        public ViewDataDictionary ViewData
        {
            get
            {
                if (MvcViewPage != null) return MvcViewPage.ViewData;
                return ViewPage.ViewData;
            }
        }

        internal InteropPage ViewPage
        {
            get
            {
                return Page as InteropPage;
            }
        }

        internal System.Web.Mvc.ViewPage MvcViewPage
        {
            get
            {
                return Page as System.Web.Mvc.ViewPage;
            }
        }
    }



}
