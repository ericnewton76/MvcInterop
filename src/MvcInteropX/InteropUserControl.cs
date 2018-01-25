using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcInterop
{
    /// <summary>
    /// ViewPage for Interoperability of MVC functionality to WebForms 
    /// </summary>
    public abstract class InteropUserControl : System.Web.UI.UserControl, IViewDataContainer, IView
    {
        protected override void OnInit(EventArgs e)
        {
            // Get the ControllerName if it is not set
            if (String.IsNullOrWhiteSpace(ControllerName))
                ControllerName = Interop.GetDefaultControllerName(GetType(), MvcRouteData, HttpContext, ref RequestContext, ref Controller);

            // Create ViewData & TempData
            ViewData = new ViewDataDictionary<object>();
            TempData = new TempDataDictionary();

            // Init the RequestContext if we don't have one
            if (RequestContext == null)
                Interop.InitRequestContext(ControllerName, MvcRouteData, HttpContext, GetType());

            // Create the Controller if we don't have one
            if (Controller == null)
                Controller = Interop.CreateController<ControllerBase>(RequestContext, ControllerName);

            ViewContext = new ViewContext(Controller.ControllerContext, this, ViewData, TempData, HttpContext.Response.Output);

            // Initialize our helpers
            InitHelpers();

            base.OnInit(e);
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

        public TempDataDictionary TempData { get; protected set; }

        public AjaxHelper<object> Ajax { get; protected set; }

        public ViewContext ViewContext { get; protected set; }

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

        protected virtual void SetViewData(ViewDataDictionary viewData)
        {
            _viewData = viewData;
        }

        #region -- IView Implementation --

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            Render(new System.Web.UI.HtmlTextWriter(writer));
        }

        #endregion
    }
}
