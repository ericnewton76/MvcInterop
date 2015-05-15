using System.Web.Mvc;

namespace MvcInterop
{
    public class InteropMasterPage<TModel> : InteropMasterPage
    {
        // Fields
        private AjaxHelper<TModel> _ajaxHelper;
        private HtmlHelper<TModel> _htmlHelper;
        private ViewDataDictionary<TModel> _viewData;

        // Properties
        public new AjaxHelper<TModel> Ajax
        {
            get { return _ajaxHelper ?? (_ajaxHelper = new AjaxHelper<TModel>(ViewContext, ViewPage)); }
        }

        public new HtmlHelper<TModel> Html
        {
            get { return _htmlHelper ?? (_htmlHelper = new HtmlHelper<TModel>(ViewContext, ViewPage)); }
        }

        public new TModel Model
        {
            get
            {
                return ViewData.Model;
            }
        }

        public new ViewDataDictionary<TModel> ViewData
        {
            get { return _viewData ?? (_viewData = new ViewDataDictionary<TModel>(ViewPage.ViewData)); }
        }
    }



}
