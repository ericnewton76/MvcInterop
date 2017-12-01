using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace MvcInterop
{
    public abstract class InteropUserControl<TModel> : InteropUserControl
    {
        private ViewDataDictionary<TModel> _viewData;

        public new AjaxHelper<TModel> Ajax
        {
            get;
            set;
        }

        public new HtmlHelper<TModel> Html
        {
            get;
            set;
        }

        public new TModel Model
        {
            get
            {
                return ViewData.Model;
            }
            set
            {
                ViewData.Model = value;
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public new ViewDataDictionary<TModel> ViewData
        {
            get
            {
                if (_viewData == null)
                {
                    SetViewData(new ViewDataDictionary<TModel>());
                }
                return _viewData;
            }
            set
            {
                SetViewData(value);
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            Ajax = new AjaxHelper<TModel>(ViewContext, this);
            Html = new HtmlHelper<TModel>(ViewContext, this);
        }

        protected override void SetViewData(ViewDataDictionary viewData)
        {
            _viewData = new ViewDataDictionary<TModel>(viewData);

            base.SetViewData(_viewData);
        }
    }

    public abstract class ViewUserControl<TModel, TController> : InteropUserControl<TModel> where TController : ControllerBase
    {
        protected override void OnInit(EventArgs e)
        {
            RequestContext = Interop.InitRequestContext(ControllerName, MvcRouteData, HttpContext, GetType());
            Controller = Interop.CreateController<TController>(RequestContext, ControllerName);

            base.OnInit(e);
        }

        private string _controllerName;
        protected override string ControllerName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_controllerName))
                    _controllerName = typeof(TController).Name.Replace("Controller", string.Empty);

                return _controllerName;
            }
        }

        public new TController Controller
        {
            get;
            set;
        }
    }
}
