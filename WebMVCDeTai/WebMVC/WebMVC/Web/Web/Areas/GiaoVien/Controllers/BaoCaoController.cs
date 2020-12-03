using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.GiaoVien.Controllers
{
    public class BaoCaoController : Controller
    {
        private WebMVCEntities db = new WebMVCEntities();

        // GET: GiaoVien/BaoCao
        public async Task<ActionResult> Index()
        {
            var baoCaos = db.BaoCaos.Include(b => b.Nhom);
            return View(await baoCaos.ToListAsync());
        }

        // GET: GiaoVien/BaoCao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCao baoCao = await db.BaoCaos.FindAsync(id);
            if (baoCao == null)
            {
                return HttpNotFound();
            }
            return View(baoCao);
        }

        // GET: GiaoVien/BaoCao/Create
        public ActionResult Create()
        {
            ViewBag.Nhom_ID = new SelectList(db.Nhoms, "ID", "MaNhom");
            return View();
        }

        // POST: GiaoVien/BaoCao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Tuan,Nhom_ID,TieuDe,NoiDung,ThoiGianBaoCao,FileUpload")] BaoCao baoCao)
        {
            if (ModelState.IsValid)
            {
                db.BaoCaos.Add(baoCao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Nhom_ID = new SelectList(db.Nhoms, "ID", "MaNhom", baoCao.Nhom_ID);
            return View(baoCao);
        }

        // GET: GiaoVien/BaoCao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCao baoCao = await db.BaoCaos.FindAsync(id);
            if (baoCao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nhom_ID = new SelectList(db.Nhoms, "ID", "MaNhom", baoCao.Nhom_ID);
            return View(baoCao);
        }

        // POST: GiaoVien/BaoCao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Tuan,Nhom_ID,TieuDe,NoiDung,ThoiGianBaoCao,FileUpload")] BaoCao baoCao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baoCao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Nhom_ID = new SelectList(db.Nhoms, "ID", "MaNhom", baoCao.Nhom_ID);
            return View(baoCao);
        }

        // GET: GiaoVien/BaoCao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCao baoCao = await db.BaoCaos.FindAsync(id);
            if (baoCao == null)
            {
                return HttpNotFound();
            }
            return View(baoCao);
        }

        // POST: GiaoVien/BaoCao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BaoCao baoCao = await db.BaoCaos.FindAsync(id);
            db.BaoCaos.Remove(baoCao);
            await db.SaveChangesAsync();
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
