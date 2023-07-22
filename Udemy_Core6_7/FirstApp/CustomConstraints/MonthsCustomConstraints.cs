using System.Text.RegularExpressions;

namespace FirstApp.CustomConstraints
{
    public class MonthsCustomConstraints : IRouteConstraint
    {
        //convert sales/{year:int:min(1990)}/{month:regex(^(apr|jul|june)$)}
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //route key is month

            //not matched
            if (!values.ContainsKey(routeKey)) return false;

            Regex monthRegex = new Regex($"^(apr|jul|june)$");
            string? monthValue = Convert.ToString(values[routeKey]);
            //matched
            if (monthRegex.IsMatch(monthValue)) return true;
            return false;
        }
    }
}
