using System.Web;
using System.Web.Mvc;

namespace BaiTap4_65130650
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
