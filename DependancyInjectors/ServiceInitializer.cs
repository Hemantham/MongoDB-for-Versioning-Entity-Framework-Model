using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BIG4.Framework.Business.MVC;
using Microsoft.Practices.Unity;

namespace BIG4.Framework.Business.DependancyInjectors
{
    public static class ServiceInitializer
    {
        /// <summary>
        /// gets a service based on the query string parameters.
        /// if version is given it will be loading a service containing a MongoDB context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <returns></returns>
        public static I Get<T, I>() where T : I
        {
            // Declare a Unity Container
            var unityContainer = new UnityContainer();

            int version = BusinessContext.GetPreviewVersion();

            if (version > 0 && BusinessContext.GetPreviewEntity() == typeof(T).Name.Replace("Service", string.Empty))
            {
                unityContainer.RegisterType<I, T>(new InjectionConstructor(version));
            }
            if (version > 0 && BusinessContext.GetPreviewEntity() == typeof(T).Name.Replace("Service", string.Empty))           
            {
                unityContainer.RegisterType<I, T>(new InjectionConstructor(version));
            }
            else
            {
                unityContainer.RegisterType<I, T>(new InjectionConstructor());
            }

            return unityContainer.Resolve<T>();
        }

        /// <summary>
        /// gets a service based version.
        /// it will be loading a service containing a MongoDB context</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="version"></param>
        /// <returns></returns>
        public static I Get<T, I>(int version) where T : I
        {
            // Declare a Unity Container
            var unityContainer = new UnityContainer();

            unityContainer.RegisterType<I, T>(new InjectionConstructor(version));

            return unityContainer.Resolve<T>();
        }

       
    }
}
