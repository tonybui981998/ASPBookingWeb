using BookingWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BookingWebApp.Application.Common.Interfaces
{
    public interface IVIllaRepository :IRepository<Villa>
    {
      

     
        void Update(Villa entity);
     


    }
}
