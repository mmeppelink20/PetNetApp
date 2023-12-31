﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPresentation.Startup))]
namespace MVCPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DataObjects.DataPathInformation.BaseDirectory = System.Web.HttpRuntime.AppDomainAppPath;
            ConfigureAuth(app);
        }
    }
}
