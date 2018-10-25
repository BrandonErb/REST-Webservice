/*************
*Author    : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : 
*FILE           : Global.asax.cs
**************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace A04_REST_Webservice
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("EmporiumCustomer", new WebServiceHostFactory(), typeof(EmporiumCustomer)));
            RouteTable.Routes.Add(new ServiceRoute("EmporiumProduct", new WebServiceHostFactory(), typeof(EmporiumProduct)));
            RouteTable.Routes.Add(new ServiceRoute("EmporiumOrder", new WebServiceHostFactory(), typeof(EmporiumOrder)));
            RouteTable.Routes.Add(new ServiceRoute("EmporiumCart", new WebServiceHostFactory(), typeof(EmporiumCart)));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}