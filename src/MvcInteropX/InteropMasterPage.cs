using System.Web.Mvc;
using System.Web.UI;

namespace MvcInterop
{
    public class InteropMasterPage : MasterPage
    {

		protected override void OnInit(System.EventArgs e)
		{
			var interopPageInterfaceInstance = this.Page as IInteropPage;
			if(interopPageInterfaceInstance != null)
			{
				this._InteropPage = interopPageInterfaceInstance;
			}
			else
			{
				//check if its an MVC ViewPage 
				var _ViewPage = this.Page as System.Web.Mvc.ViewPage;
				if(_ViewPage != null)
				{
					this._InteropPage = new InteropViewPageWrapper(_ViewPage);
				}
				else
				{
					//all-case fallback to a InteropPageWrapper that will hold the page.
					InteropPageWrapper pagewrapper = new InteropPageWrapper(this.Page);
					pagewrapper.InitContext();
					this._InteropPage = pagewrapper;
				}
			}

			base.OnInit(e);
		}

        // Properties
        public AjaxHelper<object> Ajax
        {
            get
            {
				return _InteropPage.Ajax;
            }
        }

        public HtmlHelper<object> Html
        {
            get
            {
				return _InteropPage.Html;
            }
        }

        public object Model
        {
            get
            {
				return _InteropPage.Model;
            }
        }

        public TempDataDictionary TempData
        {
            get
            {
				return _InteropPage.TempData;
            }
        }

        public UrlHelper Url
        {
            get
            {
				return _InteropPage.Url;
            }
        }


        public dynamic ViewBag
        {
            get
            {
				return _InteropPage.ViewBag;
            }
        }

        public ViewContext ViewContext
        {
            get
            {
				return _InteropPage.ViewContext;
            }
        }

        public ViewDataDictionary ViewData
        {
            get
            {
				return _InteropPage.ViewData;
            }
        }

		private IInteropPage _InteropPage;

		private bool _InteropPageTry = false;
		protected IInteropPage InteropPage { get { return _InteropPage; } }

    }



}
