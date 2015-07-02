using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcInterop
{
	public class InteropPageWrapper<T> : InteropPage<T>
	{
		public InteropPageWrapper(System.Web.UI.Page page)
		{
			_Page = page;
		}
		private System.Web.UI.Page _Page;

		internal override void InitContext()
		{
			base.InitContext(this._Page);
		}
	}
}
