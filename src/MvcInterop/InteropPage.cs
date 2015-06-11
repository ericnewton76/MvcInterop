using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcInterop
{
	/// <summary>
	/// An InteropPage for providing interoperability of WebForms and ASP.Net MVC
	/// </summary>
	public abstract class InteropPage : System.Web.UI.Page, IViewDataContainer, IView
	{
		protected override void OnPreInit(EventArgs e)
		{
			InitContext();

			base.OnPreInit(e);
		}

		internal virtual void InitContext()
		{
			InitContext(this);
		}
		internal void InitContext(System.Web.UI.Page page)
		{
			// Get the ControllerName if it is not set
			if (String.IsNullOrWhiteSpace(ControllerName))
				ControllerName = Interop.GetDefaultControllerName(page.GetType(), MvcRouteData, HttpContext, ref RequestContext, ref Controller);

			// Init the RequestContext if we don't have one
			if (RequestContext == null)
				Interop.InitRequestContext(ControllerName, MvcRouteData, HttpContext, page.GetType());

			// Create the Controller if we don't have one
			if (Controller == null)
				Controller = Interop.CreateController<ControllerBase>(RequestContext, ControllerName);

			ViewContext = new ViewContext(Controller.ControllerContext, this, ViewData, TempData, HttpContext.Response.Output);

			// Initialize our helpers
			InitHelpers(); 
		}

		#region -- Public MVC Members: HttpContext, Helpers, ViewData, Controller & Model --

		private HttpContextBase _httpContext;

		public HttpContextBase HttpContext
		{
			get
			{
				if (_httpContext != null)
					return _httpContext;

				_httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);

				return _httpContext;
			}
		}

		public HtmlHelper<object> Html { get; set; }

		public UrlHelper Url { get; protected set; }

		private ViewDataDictionary _viewData;

		public ViewDataDictionary ViewData
		{
			get
			{
				if (_viewData == null)
				{
					SetViewData(new ViewDataDictionary());
				}

				return _viewData;
			}
			set
			{
				SetViewData(value);
			}
		}

		private TempDataDictionary _tempData;

		public TempDataDictionary TempData
		{

			get
			{
				if (_tempData == null)
				{
					SetTempData(new TempDataDictionary());
				}

				return _tempData;
			}
			set
			{
				SetTempData(value);
			}
		}

		public AjaxHelper<object> Ajax { get; protected set; }

		public ViewContext ViewContext { get; protected set; }

		#region -- ViewBag --

		private DynamicViewDataDictionary _dynamicViewData;

		public dynamic ViewBag
		{
			get
			{
				if (_dynamicViewData != null) return _dynamicViewData;
				_dynamicViewData = new DynamicViewDataDictionary(() => ViewData);
				return _dynamicViewData;
			}
		}
		
		#endregion

		public object Model { get; protected set; }

		protected ControllerBase Controller;

		protected virtual string ControllerName { get; set; }

		#endregion

		#region -- Init Helpers --

		/// <summary>
		/// Will be Overridden by the Generic Implmentations
		/// </summary>
		public virtual void InitHelpers()
		{
			Ajax = new AjaxHelper<object>(ViewContext, this);
			Html = new HtmlHelper<object>(ViewContext, this);
			Url = new UrlHelper(new RequestContext(HttpContext, MvcRouteData));
		}

		#endregion

		#region -- Internal MVC Members --

		internal RouteData MvcRouteData = new RouteData();

		internal RequestContext RequestContext;

		#endregion

		#region -- Set View / Temp Data --
		
		protected virtual void SetViewData(ViewDataDictionary viewData)
		{
			_viewData = viewData;
		}

		protected virtual void SetTempData(TempDataDictionary tempData)
		{
			_tempData = tempData;
		}

		#endregion

		#region -- IView Implementation --

		public void Render(ViewContext viewContext, TextWriter writer)
		{
			Render(new System.Web.UI.HtmlTextWriter(writer));
		}

		#endregion
	}
}
