using SignalRAPI.Data.Repositories.Implementations;
using SignalRAPI.Data.Repositories.Interfaces;
using SignalRAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<RequestForm> _requestForm;
        private IGenericRepository<FormStatus> _formStatuses;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a new instance of GenericRepositoy<RequestForm> if _requestForm is null
        /// </summary>
        public IGenericRepository<RequestForm> RequestForms => _requestForm ??= new GenericRepository<RequestForm>(_context);

        public IGenericRepository<FormStatus> FormStatuses => _formStatuses ??= new GenericRepository<FormStatus>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
