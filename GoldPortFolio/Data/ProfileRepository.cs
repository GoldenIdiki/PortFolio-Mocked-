using GoldPortFolio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private ApplicationDbContext _db;

        public ProfileRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Profile GetProfile()
        {
            var user = _db.ProfileTbl.Include(x => x.Address).Include(x => x.WorkExperience).FirstOrDefault();
            return user;

        }
    }
}
