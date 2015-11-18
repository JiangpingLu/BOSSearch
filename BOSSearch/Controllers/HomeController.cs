//-------------------------------------------------------------------------------
// <Copyright file="HomeController.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the homepage controller of API 
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 11, 2015
//	Author			    : <Haley Qu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add description
//	Date Modified		: Nov 17, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.o
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BOSSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();


        }

      
    }
}