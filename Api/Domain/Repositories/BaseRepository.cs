using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly apitokendbContext _context;

        public BaseRepository(apitokendbContext context)
        {
            _context = context;
        }
    }
}
