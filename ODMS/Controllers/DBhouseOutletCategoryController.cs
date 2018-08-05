using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ODMS.Models;

namespace ODMS.Controllers
{
    [SessionExpire,SuperAccess]
    public class DBhouseOutletCategoryController : Controller
    {
        private ODMSEntities db = new ODMSEntities();

        // GET: DBHouseOutletCategory
        public ActionResult Index()
        {
            return View(db.tbld_Outlet_category.ToList());
        }

        // GET: DBHouseOutletCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbld_Outlet_category tbldOutletCategory = db.tbld_Outlet_category.Find(id);
            if (tbldOutletCategory == null)
            {
                return HttpNotFound();
            }
            return View(tbldOutletCategory);
        }

        // GET: DBHouseOutletCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DBHouseOutletCategory/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,outlet_category_name,outlet_category_code,outlet_category_description")] tbld_Outlet_category tbld_Outlet_category)
        {
            if (ModelState.IsValid)
            {
                db.tbld_Outlet_category.Add(tbld_Outlet_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbld_Outlet_category);
        }

        // GET: DBHouseOutletCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbld_Outlet_category tbld_Outlet_category = db.tbld_Outlet_category.Find(id);
            if (tbld_Outlet_category == null)
            {
                return HttpNotFound();
            }
            return View(tbld_Outlet_category);
        }

        // POST: DBHouseOutletCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,outlet_category_name,outlet_category_code,outlet_category_description")] tbld_Outlet_category tbld_Outlet_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbld_Outlet_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbld_Outlet_category);
        }

        // GET: DBHouseOutletCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbld_Outlet_category tbld_Outlet_category = db.tbld_Outlet_category.Find(id);
            if (tbld_Outlet_category == null)
            {
                return HttpNotFound();
            }
            return View(tbld_Outlet_category);
        }

        // POST: DBHouseOutletCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbld_Outlet_category tbld_Outlet_category = db.tbld_Outlet_category.Find(id);
            db.tbld_Outlet_category.Remove(tbld_Outlet_category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
