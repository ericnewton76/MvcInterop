using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcInterop
{
	/// <summary>
	/// Supports the masterpage to have access to ViewPage or InteropPage properties like Ajax/Html/Model/etc
	/// </summary>
	internal class InteropViewPageWrapper : IInteropPage
	{
		private System.Web.Mvc.ViewPage _ViewPage;

		public InteropViewPageWrapper(System.Web.Mvc.ViewPage viewPage)
		{
			// TODO: Complete member initialization
			this._ViewPage = viewPage;
		}

		public System.Web.Mvc.AjaxHelper<object> Ajax
		{
			get { return _ViewPage.Ajax; }
		}

		public System.Web.Mvc.HtmlHelper<object> Html
		{
			get { return _ViewPage.Html; }
		}

		public object Model
		{
			get { return _ViewPage.Model; }
		}

		public System.Web.Mvc.TempDataDictionary TempData
		{
			get { return _ViewPage.TempData; }
		}

		public System.Web.Mvc.UrlHelper Url
		{
			get { return _ViewPage.Url; }
		}

		public dynamic ViewBag
		{
			get { return _ViewPage.ViewBag; }
		}

		public System.Web.Mvc.ViewContext ViewContext
		{
			get { return _ViewPage.ViewContext; }
		}

		public System.Web.Mvc.ViewDataDictionary ViewData
		{
			get { return _ViewPage.ViewData; }
		}
	}

	/// <summary>
	/// Supports the masterpage to have access to ViewPage or InteropPage properties like Ajax/Html/Model/etc
	/// </summary>
	internal class InteropViewPageWrapper<T> : IInteropPage<T>
	{
		private System.Web.Mvc.ViewPage<T> _ViewPage;

		public InteropViewPageWrapper(System.Web.Mvc.ViewPage<T> viewPage)
		{
			// TODO: Complete member initialization
			this._ViewPage = viewPage;
		}

		object IInteropPage.Model { get { return _ViewPage.Model; }}
		System.Web.Mvc.AjaxHelper<object> IInteropPage.Ajax { get { return (_ViewPage as InteropPage).Ajax; } }
		System.Web.Mvc.HtmlHelper<object> IInteropPage.Html { get { return (_ViewPage as InteropPage).Html; } }

		public System.Web.Mvc.AjaxHelper<T> Ajax
		{
			get { return _ViewPage.Ajax; }
		}

		public System.Web.Mvc.HtmlHelper<T> Html
		{
			get { return _ViewPage.Html; }
		}

		public T Model
		{
			get { return _ViewPage.Model; }
		}

		public System.Web.Mvc.TempDataDictionary TempData
		{
			get { return _ViewPage.TempData; }
		}

		public System.Web.Mvc.UrlHelper Url
		{
			get { return _ViewPage.Url; }
		}

		public dynamic ViewBag
		{
			get { return _ViewPage.ViewBag; }
		}

		public System.Web.Mvc.ViewContext ViewContext
		{
			get { return _ViewPage.ViewContext; }
		}

		public System.Web.Mvc.ViewDataDictionary ViewData
		{
			get { return _ViewPage.ViewData; }
		}
	}

}
