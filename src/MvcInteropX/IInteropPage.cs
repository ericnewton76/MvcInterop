using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcInterop
{
	public interface IInteropPage
	{

		AjaxHelper<object> Ajax { get; }
		HtmlHelper<object> Html { get; }
		object Model { get; }
		TempDataDictionary TempData { get; }
		UrlHelper Url { get; }
		dynamic ViewBag { get; }
		ViewContext ViewContext { get; }
		ViewDataDictionary ViewData { get; }
	}

	public interface IInteropPage<T> : IInteropPage 
	{
		new AjaxHelper<T> Ajax { get; }
		new HtmlHelper<T> Html { get; }
		new T Model { get; }
	}
}
