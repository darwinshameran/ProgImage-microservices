using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ProgImage.Transformation.Helpers
{
    /// <summary>
    ///  Extend .NET/Core's controller to allow identical endpoints varying by query parameters.
    /// </summary>
    public class ExactQueryParamAttribute : Attribute, IActionConstraint
    {
        private readonly string[] _keys;

        public ExactQueryParamAttribute(params string[] keys)
        {
            _keys = keys;
        }

        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            IQueryCollection query = context.RouteContext.HttpContext.Request.Query;
            return query.Count == _keys.Length && _keys.All(key => query.ContainsKey(key));
        }
    }
}