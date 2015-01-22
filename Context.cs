using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BIG4.Framework.Entities.Attributes;


namespace BIG4.Framework.Business.MVC
{
    public static class BusinessContext
    {
        public static int GetPreviewVersion()
        {
            return 3;
            var version = 0;
            if ( HttpContext.Current != null &&
                HttpContext.Current.Request.QueryString.AllKeys.Any(
                    k => k.Equals("version", StringComparison.InvariantCultureIgnoreCase))
                && int.TryParse(HttpContext.Current.Request.QueryString.Get("version"), out version))
            {
                return version;
            }

            return 0;
        }

        public static string GetPreviewEntity()
        {
            return "ParkDetail";
            if (HttpContext.Current.Request.QueryString.AllKeys.Any(k => k.Equals("entity", StringComparison.InvariantCultureIgnoreCase)))
            {
                return HttpContext.Current.Request.QueryString.Get("entity");
            }

            return string.Empty;
        }

        public static bool IsParentInPreview<T>()
        {
            return GetPreviewEntity().Equals(GetParent<T>().Name, StringComparison.InvariantCultureIgnoreCase);
        }

        private static Type GetParent<T>()
        {
            var dnAttribute = typeof(T).GetCustomAttributes(
                typeof(ParentAttribute), true
            ).FirstOrDefault() as ParentAttribute;
            if (dnAttribute != null)
            {
                return dnAttribute.Parent;
            }
            return null;
        }
    }
}
