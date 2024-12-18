﻿using BookingWebApp.Application.Common.Interfaces;
using BookingWebApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWebApp.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IVIllaRepository Villa {get ; private set;}
        public IVillaNumberRepository VillaNumber {get ; private set;}

        public IAmenityRepository Amenity { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepository(_db);
            VillaNumber = new VillaNumberRepository(_db);
            Amenity = new AmenityRepository(_db);
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}