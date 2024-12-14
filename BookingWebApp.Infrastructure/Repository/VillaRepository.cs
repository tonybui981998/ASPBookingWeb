using BookingWebApp.Application.Common.Interfaces;
using BookingWebApp.Domain.Entities;
using BookingWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingWebApp.Infrastructure.Repository
{
    public class VillaRepository : Repository<Villa> ,IVIllaRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context) : base(context){
            _context = context;
        }
      
      
    

        public void Update(Villa entity)
        {
            _context.Villas.Update(entity);
        }
    }
}
