using System.Linq;
using System.Web.Mvc;
using ODMS.Controllers;
using ODMS.Models;
using ODMS.Models.ViewModel;

namespace ODMS.ControllersApi
{
    public class AppsActivityController : Controller
    {

        // GET: AppsActivity/Login

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return PartialView();

        }

        [HttpPost]
        public ActionResult Login(UserinfoVm userinfoVm)
        {
            ODMSEntities Db = new ODMSEntities();

            Session["IpAddress"] = userinfoVm.IpAddress;
            if (ModelState.IsValid)
            {

                var z = Db.DB_User_check(userinfoVm.UserName, userinfoVm.UserPassword).Where(a => a.User_role_id == 8).ToList();//from Procudure for db User

                if (z.Count() == 1)
                {
                    foreach (var i in z)
                    {
                        if (i.active == 1)
                        {
                            var dbName = Db.tbld_distribution_house.Where(a => a.DB_Id == i.DistributionId).Select(p => p.DBName).ToList();


                            Session["User_Name"] = i.login_user_id;
                            Session["user_role_code"] = i.user_role_code;
                            Session["first_name"] = i.Name;
                            Session["User_biz_role_id"] = i.biz_zone_category_id;
                            Session["biz_zone_id"] = i.Zone_id;
                            Session["User_role_id"] = i.User_role_id;
                            Session["DBId"] = i.DistributionId;
                            Session["psrid"] = i.id;
                            Session["dbName"] = dbName[0];

                        }
                        else
                        {
                            ViewBag.Title = "Login";
                            ViewBag.alertbox = "error";
                            ViewBag.alertboxMsg = "Your User is InActive";

                            return PartialView();
                        }
                    }




                    return RedirectToAction("Index", "AppsActivity");
                }
            }

            ViewBag.Title = "Login";
            ViewBag.alertbox = "error";
            ViewBag.alertboxMsg = "Please Enter valid User and Password";
            return PartialView();

        }


        // GET: AppsActivity
        [PSRAccessExpire]
        public ActionResult Index()
        {
            return View();
        }
    }
}