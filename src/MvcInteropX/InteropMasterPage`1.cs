using System.Web.Mvc;

namespace MvcInterop
{
	public class InteropMasterPage<TModel> : System.Web.UI.MasterPage, IInteropPage<TModel>
	{

		protected override void OnInit(System.EventArgs e)
		{
			var interopPageInterfaceInstance = this.Page as IInteropPage<TModel>;
			if(interopPageInterfaceInstance != null)
			{
				this._InteropPage = interopPageInterfaceInstance;
			}
			else
			{
				//check if its an MVC ViewPage 
				var _ViewPage = this.Page as System.Web.Mvc.ViewPage<TModel>;
				if(_ViewPage != null)
				{
					this._InteropPage = new InteropViewPageWrapper<TModel>(_ViewPage);
				}
				else
				{
					//all-case fallback to a InteropPageWrapper that will hold the page.
					InteropPageWrapper<TModel> pagewrapper = new InteropPageWrapper<TModel>(this.Page);
					pagewrapper.InitContext();
					this._InteropPage = pagewrapper;
				}
			}

			base.OnInit(e);
		}

		object IInteropPage.Model { get { return _InteropPage.Model; } }
		System.Web.Mvc.AjaxHelper<object> IInteropPage.Ajax { get { return (_InteropPage as InteropPage).Ajax; } }
		System.Web.Mvc.HtmlHelper<object> IInteropPage.Html { get { return (_InteropPage as InteropPage).Html; } }

		// Properties
		public AjaxHelper<TModel> Ajax
		{
			get
			{
				return _InteropPage.Ajax;
			}
		}

		public HtmlHelper<TModel> Html
		{
			get
			{
				return _InteropPage.Html;
			}
		}

		public TModel Model
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

		private IInteropPage<TModel> _InteropPage;

		private bool _InteropPageTry = false;
		protected IInteropPage<TModel> InteropPage { get { return _InteropPage; } }

	}



}
