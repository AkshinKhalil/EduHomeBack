using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using System.Linq;

namespace EduHomeBackEnd.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;

        public LayoutServices(AppDbContext context)
        {
            _context = context;
        }

        public Setting getSettingDatas()
        {
            Setting data = _context.Settings.FirstOrDefault();
            return data;
        }
    }
}
