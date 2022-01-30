using EduHomeBackEndProject.DAL;
using EduHomeBackEndProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduHomeBackEndProject.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public Setting GetSettingData()
        {
            Setting data = _context.Settings.Include(s=>s.FooterSocialMedias).FirstOrDefault();
                return data;
        }
    }
}
