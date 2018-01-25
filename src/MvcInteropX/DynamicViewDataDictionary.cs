using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;

namespace MvcInterop
{
    internal sealed class DynamicViewDataDictionary : DynamicObject
    {
        // Fields
        private readonly Func<ViewDataDictionary> _viewDataThunk;

        // Methods
        public DynamicViewDataDictionary(Func<ViewDataDictionary> viewDataThunk)
        {
            _viewDataThunk = viewDataThunk;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return ViewData.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = ViewData[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            ViewData[binder.Name] = value;
            return true;
        }

        // Properties
        private ViewDataDictionary ViewData
        {
            get
            {
                return _viewDataThunk();
            }
        }
    }
}
