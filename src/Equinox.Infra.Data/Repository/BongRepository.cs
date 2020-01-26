using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Repository
{
    public class BongRepository : Repository<Bong>, IBongRepository
    {
        public BongRepository(EquinoxContext context) : base(context)
        {
        }

        public Bong GetByReferenceNo(string referenceNo)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.ReferenceNo == referenceNo);
        }
    }
}
