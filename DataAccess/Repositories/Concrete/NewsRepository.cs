using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class NewsRepository : Repository<LastestNews>, INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LastestNews>> GetAllByDescByDateAsync()
        {
            return await _context.LastestNews
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
