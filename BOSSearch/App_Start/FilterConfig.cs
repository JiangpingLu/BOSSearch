using System.Web;
using System.Web.Mvc;

namespace PWC.US.USTO.BOSSearch
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
