using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly apitokendbContext _context;
        public UnitOfWork(apitokendbContext context)
        {
            _context = context;
        }

        public void Complate()
        {
            _context.SaveChanges();
        }

        public async Task ComplateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
