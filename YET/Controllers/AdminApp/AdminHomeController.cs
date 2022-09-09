#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YET.Models;
using YET.Models.UserModel;
using System.IO;

namespace YET.Controllers.AdminApp
{
    public class AdminHomeController : Controller
    {
        private readonly DatabaseContext _context;
        private IWebHostEnvironment webHostEnvironment;

        public AdminHomeController(DatabaseContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            this.webHostEnvironment = _environment;
        }



        // GET: AdminHome
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_Teams.ToListAsync());
        }

        // GET: AdminHome/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Teams = await _context.tbl_Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (tbl_Teams == null)
            {
                return NotFound();
            }

            return View(tbl_Teams);
        }

        // GET: AdminHome/Create
        public IActionResult Create(int id = 0)
        {
            ViewBag.BT = "Create";
            if (id > 0)
            {
                tbl_Teams _editTeam = new tbl_Teams();
                var data = (from a in _context.tbl_Teams where a.TeamId == id select a).ToList();
                _editTeam.TeamId = data[0].TeamId;
                _editTeam.TeamName = data[0].TeamName;
                _editTeam.TeamDesignation = data[0].TeamDesignation;
                _editTeam.TeamDescription = data[0].TeamDescription;
                _editTeam.TeamDOJ = data[0].TeamDOJ;
                ViewBag.BT = "Update";
                return View(_editTeam);
            }
            return View();
        }

        // POST: AdminHome/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamName,TeamDesignation,TeamDescription,ImageFile,TeamCreatedDate,TeamDOJ,TeamEOS,CreatedBy,ModifiedBy")] tbl_Teams _Teams)
        {
            string wwwRootPath, fileName, extension, filesize;

            if (_Teams.TeamId > 0)
            {
                //System.IO.File.Delete(_Teams.TeamImage);
                //Save image to wwwroot/image
                wwwRootPath = webHostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(_Teams.ImageFile.FileName);
                extension = Path.GetExtension(_Teams.ImageFile.FileName);
                filesize = _Teams.ImageFile.Length.ToString();

                if (extension.Contains(".png") || extension.Contains(".jpg") || extension.Contains(".jpeg"))
                {
                    if (filesize.Length <= 100000)
                    {
                        _Teams.TeamImage = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Admin/Images/Teams", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await _Teams.ImageFile.CopyToAsync(fileStream);
                        }

                        _Teams.TeamCreatedDate = DateTime.Now;
                        _Teams.CreatedBy = 1;
                        _Teams.ModifiedBy = 1;
                        _Teams.TeamImage = "/Admin/Images/Teams/" + fileName;
                        _context.Entry(_Teams).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                        //return View("Index", _Teams);

                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Save image to wwwroot/image
                    wwwRootPath = webHostEnvironment.WebRootPath;
                    fileName = Path.GetFileNameWithoutExtension(_Teams.ImageFile.FileName);
                    extension = Path.GetExtension(_Teams.ImageFile.FileName);
                    filesize = _Teams.ImageFile.Length.ToString();

                    if (extension.Contains(".png") || extension.Contains(".jpg") || extension.Contains(".jpeg") || extension.Contains(".PNG") || extension.Contains(".JPG") || extension.Contains(".JPEG"))
                    {
                        if (filesize.Length <= 100000)
                        {
                            _Teams.TeamImage = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Admin/Images/Teams", fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await _Teams.ImageFile.CopyToAsync(fileStream);
                            }

                            _Teams.TeamCreatedDate = DateTime.Now;
                            _Teams.CreatedBy = 1;
                            _Teams.ModifiedBy = 1;
                            _Teams.TeamImage = "/Admin/Images/Teams/" + fileName;
                            _context.Add(_Teams);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                            //return View("Index", _Teams);
                        }


                    }
                }
            }
            return View(_Teams);
        }

 

        // GET: AdminHome/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Teams = await _context.tbl_Teams.FindAsync(id);
            if (tbl_Teams == null)
            {
                return NotFound();
            }
            return View(tbl_Teams);
        }

        // POST: AdminHome/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamName,TeamDescription,TeamImage,TeamCreatedDate,TeamDOJ,TeamEOS,CreatedBy,ModifiedBy")] tbl_Teams tbl_Teams)
        {
            if (id != tbl_Teams.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_Teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_TeamsExists(tbl_Teams.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_Teams);
        }

        // GET: AdminHome/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Teams = await _context.tbl_Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (tbl_Teams == null)
            {
                return NotFound();
            }

            return View(tbl_Teams);
        }

        // POST: AdminHome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_Teams = await _context.tbl_Teams.FindAsync(id);
            _context.tbl_Teams.Remove(tbl_Teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_TeamsExists(int id)
        {
            return _context.tbl_Teams.Any(e => e.TeamId == id);
        }
    }
}
