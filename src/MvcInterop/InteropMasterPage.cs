using System.Web.Mvc;
using System.Web.UI;

namespace MvcInterop
{
    public class InteropMasterPage : MasterPage
    {

		protected override void OnInit(System.EventArgs e)
		{
			this._InteropPage = this.Page as InteropPage;
			if (this._InteropPage == null)
			{
				InteropPageWrapper pagewrapper = new InteropPageWrapper(this.Page);
				pagewrapper.InitContext();
				this._InteropPage = pagewrapper;
			}

			base.OnInit(e);
		}

        // Properties
        public AjaxHelper<object> Ajax
        {
            get
            {
				return InteropPage.Ajax;
            }
        }

        public HtmlHelper<object> Html
        {
            get
            {
				return InteropPage.Html;
            }
        }

        public object Model
        {
            get
            {
				return InteropPage.Model;
            }
        }

        public TempDataDictionary TempData
        {
            get
            {
				return InteropPage.TempData;
            }
        }

        public UrlHelper Url
        {
            get
            {
				return InteropPage.Url;
            }
        }


        public dynamic ViewBag
        {
            get
            {
				return InteropPage.ViewBag;
            }
        }

        public ViewContext ViewContext
        {
            get
            {
				return InteropPage.ViewContext;
            }
        }

        public ViewDataDictionary ViewData
        {
            get
            {
				return this.InteropPage.ViewData;
            }
        }

		private InteropPage _InteropPage;
		private bool _InteropPageTry = false;
		protected InteropPage InteropPage { get { return _InteropPage; } }

    }



}
