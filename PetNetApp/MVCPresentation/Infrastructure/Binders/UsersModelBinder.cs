using LogicLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Infrastructure.Binders
{
    public class UsersModelBinder : IModelBinder
    {
        private const string sessionKey = "User";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DataObjects.Users user = null;
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (controllerContext.HttpContext.Session != null)
                {
                    user = (DataObjects.Users)controllerContext.HttpContext.Session[sessionKey];
                    if (user == null)
                    {
                        try
                        {
                            user = MasterManager.GetMasterManager().UsersManager.RetrieveUserByUserEmail(controllerContext.HttpContext.User.Identity.GetUserName());
                            controllerContext.HttpContext.Session[sessionKey] = user;
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return user;
        }
    }
}
