using GoldPortFolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Data
{
    public interface IProfileRepository
    {
        Profile GetProfile();
    }
}
