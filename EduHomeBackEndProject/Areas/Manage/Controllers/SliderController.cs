using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Extensions;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.HeaderSliders.Count() / 2);
            ViewBag.CurrentPage=page;
            List<HeaderSlider> model = _context.HeaderSliders.Skip((page - 1) * 2).Take(2).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HeaderSlider headerSlider)
        {
            List<HeaderSlider> OrderList = _context.HeaderSliders.Where(hs => hs.Order == headerSlider.Order).ToList();

            if (!ModelState.IsValid) return View();

            if (headerSlider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Choose image file!");
                return View();
            }
            if (!headerSlider.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFile", "Image file size must to be max 2MB");
                return View();
            }
            if (!headerSlider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Choose image file!");
                return View();
            }
            if (headerSlider.Title == null && headerSlider.Description == null)
            {
                ModelState.AddModelError("Title", "Do not leave blank!");
                return View(headerSlider);

            }
            else if (headerSlider.Title == null && headerSlider.Description != null)
            {
                ModelState.AddModelError("Title", "Do not leave blank!");
                return View(headerSlider);
            }
            else if (headerSlider.Title != null && headerSlider.Description == null)
            {
                ModelState.AddModelError("Description", "Do not leave blank!");
                return View(headerSlider);
            }
            foreach (var item in OrderList)
            {
                if (item.Order == headerSlider.Order)
                {
                    ModelState.AddModelError("Order", "You enter same Order.Change other Order");
                    return View(headerSlider);
                }
            }




            headerSlider.Image = headerSlider.ImageFile.SaveImg(_env.WebRootPath, "Assets/img/slider");
            _context.HeaderSliders.Add(headerSlider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            HeaderSlider headerSlider = _context.HeaderSliders.FirstOrDefault(hs => hs.Id == id);
            if (headerSlider == null) return NotFound();
            return View(headerSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HeaderSlider headerSlider,int id)
        {

            HeaderSlider existSlider = _context.HeaderSliders.FirstOrDefault(s => s.Id == headerSlider.Id);
            HeaderSlider Order = _context.HeaderSliders.Where(hs => hs.Order == headerSlider.Order).FirstOrDefault();

            if (existSlider == null) return NotFound();
            if (!ModelState.IsValid) return View(existSlider);

            if (headerSlider.ImageFile != null)
            {
                if (!headerSlider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existSlider);
                }
                if (!headerSlider.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2MB");
                    return View(existSlider);
                }


                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/slider", existSlider.Image);
                existSlider.Image = headerSlider.ImageFile.SaveImg(_env.WebRootPath, "assets/img/slider");
            }
           
            if (headerSlider.Title == null)
            {
                ModelState.AddModelError("Title", "Do not leave blank!");
                return View(existSlider);
            }
            if (headerSlider.Description == null)
            {
                ModelState.AddModelError("Description", "Do not leave blank!");
                return View(existSlider);
            }



            
                if (Order.Id != id)
                {
                    ModelState.AddModelError("Order", "You enter same Order.Change other Order");
                    return View(existSlider);
               }
            


            
            existSlider.Order=headerSlider.Order;
            existSlider.Title = headerSlider.Title;
            existSlider.Description=headerSlider.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            HeaderSlider headerSlider = _context.HeaderSliders.FirstOrDefault(hs => hs.Id == id);
            HeaderSlider existSlider = _context.HeaderSliders.FirstOrDefault(s => s.Id == headerSlider.Id);

            if (existSlider == null) return NotFound();
            if (headerSlider == null) return Json(new { status = 404 });

            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", existSlider.Image);

            _context.HeaderSliders.Remove(headerSlider);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }


    }
}
