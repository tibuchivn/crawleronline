using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InlidersScrapper.Data;
using InlidersScrapper.Helper.Scraper;
using InlidersScrapper.Models;
using InlidersScrapper.Models.Enum;
using InlidersScrapper.Models.ViewModel;

namespace InlidersScrapper.Controllers
{
    public class WebScrapperController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private WebScraper scraper;
        // GET: WebScrapper
        public ActionResult Index()
        {
            return View(db.WebScrappers.ToList().Select(x => new WebScraperDetailViewModel
                                                             {
                                                                 Url = x.Url,
                                                                 SelectorType = (SelectorType) Enum.Parse(typeof(SelectorType), x.SelectorType.ToString()),
                                                                 CreatedDate = x.CreatedDate,
                                                                 SelectorValue = x.SelectorValue,
                                                                 WebContent = x.WebContent,
                                                                 Id = x.Id
                                                             }).ToList());
        }

        // GET: WebScrapper/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebScrapper webScrapper = db.WebScrappers.Find(id);
            if (webScrapper == null)
            {
                return HttpNotFound();
            }
            return View(new WebScraperDetailViewModel
                        {
                            Url = webScrapper.Url,
                            SelectorType =
                                (SelectorType) Enum.Parse(typeof (SelectorType), webScrapper.SelectorType.ToString()),
                            CreatedDate = webScrapper.CreatedDate,
                            WebContent = webScrapper.WebContent,
                            SelectorValue = webScrapper.SelectorValue,
                            Id = webScrapper.Id
                        });
        }

        // GET: WebScrapper/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebScrapper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Url,WebContent,CreatedDate,SelectorType,SelectorValue")] WebScrapper webScrapper)
        {
            if (ModelState.IsValid)
            {
                db.WebScrappers.Add(webScrapper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webScrapper);
        }

        // GET: WebScrapper/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebScrapper webScrapper = db.WebScrappers.Find(id);
            if (webScrapper == null)
            {
                return HttpNotFound();
            }
            return View(webScrapper);
        }

        // POST: WebScrapper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Url,WebContent,CreatedDate,SelectorType,SelectorValue")] WebScrapper webScrapper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webScrapper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webScrapper);
        }

        // GET: WebScrapper/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebScrapper webScrapper = db.WebScrappers.Find(id);
            if (webScrapper == null)
            {
                return HttpNotFound();
            }
            return View(webScrapper);
        }

        // POST: WebScrapper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebScrapper webScrapper = db.WebScrappers.Find(id);
            db.WebScrappers.Remove(webScrapper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PreviewUrl(WebScraperPreviewViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Url))
            {
                scraper = new CsQueryWebScraper(model.Url);
                var content =
                    scraper.Parse((SelectorType) Enum.Parse(typeof (SelectorType), model.SelectorType.ToString()),
                        model.SelectorValue);
                return Json(new {success = true, data = content.ToArray()});
            }
            return Json(new {success = false});
        }

        [HttpPost]
        public ActionResult CreateNewScraper(WebScraperPreviewViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Url))
            {
                scraper = new CsQueryWebScraper(model.Url);
                var content =
                    scraper.Parse((SelectorType)Enum.Parse(typeof(SelectorType), model.SelectorType.ToString()),
                        model.SelectorValue);

                WebScrapper scraperResult = new WebScrapper
                                            {
                                                Url = model.Url,
                                                SelectorValue = model.SelectorValue,
                                                SelectorType = model.SelectorType,
                                                CreatedDate = DateTime.UtcNow,
                                                WebContent = string.Join("", content)
                                            };
                db.WebScrappers.Add(scraperResult);
                db.SaveChanges();
            }
            return Json(true);
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
