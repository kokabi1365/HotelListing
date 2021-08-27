﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;
using HotelListing.IRepository;

namespace HotelListing.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<Country> Countries => _countries ?? new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ?? new GenericRepository<Hotel>(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
