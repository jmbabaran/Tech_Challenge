using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientAssistance.Models;
using PatientAssistance.Common;
using System.IO;
using System.Web.UI;

namespace PatientAssistance.Controllers
{
    public class PatientAssistancesController : Controller
    {
        private PatientAssistanceContext db = new PatientAssistanceContext();

        private static readonly IList<Hospital> _hospitals;
        private static readonly IList<Illness> _illnesses;

        static PatientAssistancesController()
        {
            Utils utils = new Utils();
            _hospitals = utils.GetSortedHospital(1);
            _illnesses = utils.GetIllnessList();


        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Hospitals()
        {
            return Json(_hospitals, JsonRequestBehavior.AllowGet);
        }

        // GET: Hospital Booking
        //public ActionResult BookHospital()
        //{
        //    return View();
        //}

        // GET: PatientAssistances
        public ActionResult Index()
        {
            return View(db.PatientAssistances.ToList());
            //return null;
        }

        // GET: PatientAssistances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientAssistance.Models.PatientAssistance patientAssistance = db.PatientAssistances.Find(id);
            if (patientAssistance == null)
            {
                return HttpNotFound();
            }
            return View(patientAssistance);
        }

        // GET: PatientAssistances/Create
        public ActionResult Create()
        {
            ViewBag._hospitals = _hospitals;
            ViewBag._illnesses = _illnesses;

            return View();
        }

        // POST: PatientAssistances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,LevelOfPain,HospitalName,AveProcessTime,PatientCount,WaitingTime")] PatientAssistance.Models.PatientAssistance patientAssistance)
        {
            if (ModelState.IsValid)
            {
                db.PatientAssistances.Add(patientAssistance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientAssistance);
        }

        // GET: PatientAssistances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientAssistance.Models.PatientAssistance patientAssistance = db.PatientAssistances.Find(id);
            if (patientAssistance == null)
            {
                return HttpNotFound();
            }
            return View(patientAssistance);
        }

        // POST: PatientAssistances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,LevelOfPain,HospitalName,AveProcessTime,PatientCount,WaitingTime")] PatientAssistance.Models.PatientAssistance patientAssistance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientAssistance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientAssistance);
        }

        // GET: PatientAssistances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientAssistance.Models.PatientAssistance patientAssistance = db.PatientAssistances.Find(id);
            if (patientAssistance == null)
            {
                return HttpNotFound();
            }
            return View(patientAssistance);
        }

        // POST: PatientAssistances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientAssistance.Models.PatientAssistance patientAssistance = db.PatientAssistances.Find(id);
            db.PatientAssistances.Remove(patientAssistance);
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
