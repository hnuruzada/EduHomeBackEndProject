using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Extensions;
using EduHomeBackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context=context;
            _env=env;
        }


        public IActionResult Index()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }


        public IActionResult Edit()
        {
            
            Setting setting = _context.Settings.Include(s=>s.FooterSocialMedias).FirstOrDefault();
            if (setting == null) return NotFound();
            return View(setting);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {

            Setting settingEdit = _context.Settings.Include(s => s.FooterSocialMedias).FirstOrDefault(s => s.Id == setting.Id);
            if (!ModelState.IsValid) return NotFound();
            if (setting.HeaderLogoFile == null)
            {
                ModelState.AddModelError("HeaderLogoFile", "HeaderLogoFile is required");
            }
            if (setting.SearchIcon == null)
            {
                ModelState.AddModelError("SearchIcon", "SearchIcon is required");
            }
            if (setting.AboutImgFile == null)
            {
                ModelState.AddModelError("AboutImgFile", "AboutImgFile is required");
            }
            if (setting.AboutContentDesc == null)
            {
                ModelState.AddModelError("AboutContentDesc", "AboutContentDesc is required");
            }
           
            if (setting.AboutContentTitle == null)
            {
                ModelState.AddModelError("AboutContentTitle", "AboutContentTitle is required");
            }
            if (setting.FooterLogoFile == null)
            {
                ModelState.AddModelError("FooterLogoFile", "Footer Logo is required");
            }
            if (setting.FooterDescription == null)
            {
                ModelState.AddModelError("FooterDescription", "FooterDescription is required");
            }
            if (setting.HeaderLogoFile != null && setting.FooterLogoFile != null && setting.AboutImgFile != null)
            {
                if (!setting.HeaderLogoFile.IsImage())
                {
                    ModelState.AddModelError("HeaderLogoFile", "File must be image file");
                    return View();
                }
                if (!setting.HeaderLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.FooterLogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FooterLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.AboutImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.AboutImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("AboutImgFile", "file must be max size 2mb ");
                    return View();
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.HeaderLogo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.AboutContentImage);
                settingEdit.HeaderLogo = setting.HeaderLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");
                settingEdit.FooterLogo = setting.FooterLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");
                settingEdit.AboutContentImage = setting.AboutImgFile.SaveImg(_env.WebRootPath, "assets/img/about");

            }
            if (setting.HeaderLogoFile == null && setting.FooterLogoFile != null && setting.AboutImgFile != null)
            {

                if (!setting.FooterLogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FooterLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.AboutImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.AboutImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("AboutImgFile", "file must be max size 2mb ");
                    return View();
                }


                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.AboutContentImage);

                settingEdit.FooterLogo = setting.FooterLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");
                settingEdit.AboutContentImage = setting.AboutImgFile.SaveImg(_env.WebRootPath, "assets/img/about");

            }
            if (setting.HeaderLogoFile != null && setting.FooterLogoFile == null && setting.AboutImgFile != null)
            {
                if (!setting.HeaderLogoFile.IsImage())
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be image file");
                    return View();
                }
                if (!setting.HeaderLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be max size 2mb ");
                    return View();
                }

                if (!setting.AboutImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.AboutImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("AboutImgFile", "file must be max size 2mb ");
                    return View();
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.HeaderLogo);

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.AboutContentImage);
                settingEdit.HeaderLogo = setting.HeaderLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");

                settingEdit.AboutContentImage = setting.AboutImgFile.SaveImg(_env.WebRootPath, "assets/img/about");

            }
            if (setting.HeaderLogoFile != null && setting.FooterLogoFile != null && setting.AboutImgFile == null)
            {
                if (!setting.AboutImgFile.IsImage())
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be image file");
                    return View();
                }
                if (!setting.HeaderLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.FooterLogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FooterLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }


                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.HeaderLogo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);

                settingEdit.HeaderLogo = setting.HeaderLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");
                settingEdit.FooterLogo = setting.FooterLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");


            }
            if (setting.HeaderLogoFile == null && setting.FooterLogoFile == null && setting.AboutImgFile != null)
            {


                if (!setting.AboutImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.AboutImgFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("AboutImgFile", "file must be max size 2mb ");
                    return View();
                }


                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.AboutContentImage);

                settingEdit.AboutContentImage = setting.AboutImgFile.SaveImg(_env.WebRootPath, "assets/img/about");

            }
            if (setting.HeaderLogoFile != null && setting.FooterLogoFile == null && setting.AboutImgFile == null)
            {
                if (!setting.HeaderLogoFile.IsImage())
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be image file");
                    return View();
                }
                if (!setting.HeaderLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("HeaderLogoFile", "file must be max size 2mb ");
                    return View();
                }


                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.HeaderLogo);

                settingEdit.HeaderLogo = setting.HeaderLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");


            }
            if (setting.HeaderLogoFile == null && setting.FooterLogoFile != null && setting.AboutImgFile == null)
            {

                if (!setting.FooterLogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FooterLogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }


                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);


                settingEdit.FooterLogo = setting.FooterLogoFile.SaveImg(_env.WebRootPath, "assets/img/logo");


            }

            settingEdit.AboutContentTitle = setting.AboutContentTitle;
            settingEdit.AboutContentDesc = setting.AboutContentDesc;
            settingEdit.FooterDescription = setting.FooterDescription;
            settingEdit.Address = setting.Address;
            settingEdit.WebAddress = setting.WebAddress;
            settingEdit.Mail = setting.Mail;
            settingEdit.PhoneNumber1 = setting.PhoneNumber1;
            settingEdit.PhoneNumber2 = setting.PhoneNumber2;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
